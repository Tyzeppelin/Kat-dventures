using System;
using UnityEngine;
using System.Collections;

/// \class MurDoigts
/// Script s'appliquant aux doigts du chat
public class MurDoigts : MonoBehaviour {

	public Transform[] TableauDePrises; ///Toutes les prises du mur
	public Transform DebutMembre; ///Racine du membre (en général épaule)
	public Transform PriseDeDebut; ///Prise à la première frame
	public int epsilonDoigtPrise; ///Correspond à la distance limite entre les doigts et la prise
	public int epsilonEpaulePrise; ///Correspond à la distance minimale entre l'épaule et la prise (TECCC)
	public Vector3 directionDNT; ///Direction voulue par le joueur
	private CatManipulability catm; ///L'objet CatManipulability pour accéder aux méthodes de calcul

	Transform priseEnCours;


	// Use this for initialization
	void Start () {
		priseEnCours = PriseDeDebut;
		CCD3d test = (CCD3d)(transform.GetComponent ("CCD3d"));
		test.target=PriseDeDebut;

		catm = new CatManipulability ();
	}
	
	// Update is called once per frame
	void Update () {

		//Vérification de la distance : quand la main est trop loin de la prise, on change
		if (Vector3.Distance (transform.position, priseEnCours.position) > epsilonDoigtPrise) {
			//Debug.Log ("Changement de prise en cours");
			priseEnCours=getPrise ();
		}

	    //Ensuite, il faut changer la target pour le script CCD
		CCD3d test = (CCD3d)(transform.GetComponent ("CCD3d"));
	    test.target=priseEnCours;
	}

	/// \brief Méthode pour obtenir la meilleure prise ; application de l'algorithme pour TECCC
	///
	/// \return la meilleure prise selon TECCC
	Transform getPrise() {
		/*
		 * Transform priseProche = priseEnCours; // TODO a revoir 
		//Recherche de la prise la plus proche 
		for (int i =0;i<TableauDePrises.Length;i++) {
			if (Vector3.Distance(transform.position,TableauDePrises[i].position) <  Vector3.Distance(transform.position,priseProche.position)) 
				priseProche = TableauDePrises[i];
		}
		return priseProche;*/
		/*-----------*/

		// 1: choix des prises proches
		int iTabP = 0;
		//Transform[] TabPrisesProches=new Transform[5]; //Mis à 10 par défaut
		ArrayList TabPrisesProches = new ArrayList (0);
		foreach (Transform t in TableauDePrises) 
		{
			if (Vector3.Distance(t.position,DebutMembre.position) < epsilonEpaulePrise) {
				//Debug.Log("Création du tab de prises");
				//La prise est à retenir
				TabPrisesProches.Add (t);
				iTabP++;
			}
		}


		// 2: Set up
		Transform priseProche = null;
		float bestFTR = -1000000000000;


		//3: 
		foreach (Transform p in TabPrisesProches) 
		{
			Debug.Log("Prise p : "+p);
			//3a : trouver une bonne config avec IK
			//ALORS : Attention le code qui va suivre est vraiment pas tip top. C'est sale, mais ça devrait marcher ; de toute façon, on n'a plus le temps de faire dans la dentelle.
			// Pour chaque membre, on a dit qu'il avait des parents sur 3 générations au-dessus. On va rentrer les clones à la main.
			CCD3d ccdScript = GetComponent<CCD3d>();
			Clone doigt = new Clone(transform,ccdScript.tabMembre[0]);
			Clone bas = new Clone(transform.parent,ccdScript.tabMembre[1]);
			Clone haut = new Clone(transform.parent.parent,ccdScript.tabMembre[2]);
			Clone epaule = new Clone(transform.parent.parent.parent,ccdScript.tabMembre[3]);
			//Je vous l'avais dis...
			//Réglage de l'hérédité : 
			doigt.trans.parent = bas.trans;
			bas.trans.parent = haut.trans;
			haut.trans.parent = epaule.trans;
			epaule.trans.parent = DebutMembre.transform.parent;
			//Debug.Log(DebutMembre.transform.parent);
			//Debug.Log("-----------" + doigt.trans);


				//On fait tourner l'algo de CCD
				doigt.CCDStep3D(doigt.trans, doigt.trans,p);
				bas.CCDStep3D(bas.trans, doigt.trans,p);
				haut.CCDStep3D(haut.trans, doigt.trans,p);
				epaule.CCDStep3D(epaule.trans, doigt.trans,p);

			//3b : calcul du FTR
			float currentFTR = catm.ftr(directionDNT,doigt.trans); //TODO verif si c'est le bon transform 
			Debug.Log("FTR = " + currentFTR);
			if (currentFTR > bestFTR) 
			{
				bestFTR=currentFTR;
				priseProche=p;
			}
		}
		return priseProche;
	}
}

