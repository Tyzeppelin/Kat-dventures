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
	Transform clone;


	// Use this for initialization
	void Start () {
		priseEnCours = PriseDeDebut;
		CCD3d test = (CCD3d)(transform.GetComponent ("CCD3d"));
		test.target=PriseDeDebut;
		//clone = (Transform)Instantiate (GetComponent<CCD3d> ().armStart);
		/*GameObject clone = new GameObject ();
		clone.transform.position = GetComponent<CCD3d> ().armStart.position;
		clone.transform.rotation = GetComponent<CCD3d> ().armStart.rotation;
		clone.AddComponent ("CCD3d");*/

		catm = new CatManipulability ();
	}
	
	// Update is called once per frame
	void Update () {

		//Vérification de la distance : quand la main est trop loin de la prise, on change
		if (Vector3.Distance (transform.position, priseEnCours.position) > epsilonDoigtPrise) {
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
		Transform[] TabPrisesProches=new Transform[5]; //Mis à 10 par défaut
		foreach (Transform t in TableauDePrises) 
		{
			if (Vector3.Distance(t.position,DebutMembre.position) < epsilonEpaulePrise) {
				//La prise est à retenir
				TabPrisesProches[iTabP]=t;
				iTabP++;
			}
		}

		// 2: Set up
		Transform priseProche = null;
		float bestFTR = -1000000000000;


		//3: 
		foreach (Transform p in TabPrisesProches) 
		{
			//3a : trouver une bonne config avec IK
				//TODO revoir l'ensemble de 3a
				//Avec le clone, les scripts sont aussi copiés : on ne garde que celui de CCD, que l'on paramètre bien
			Transform clone=Instantiate(GetComponent<CCD3d>().armStart) as Transform;
			Transform doigt = clone; //Desactiver les scripts sur les clones ! 
			while (doigt.childCount != 0) doigt=doigt.GetChild(0);
			Debug.Log(doigt);
			doigt.GetComponent<CCD3d>().target=p;
			doigt.GetComponent<CCD3d>().enabled=true;
				//On fait tourner l'algo de CCD
			int i = 0;
			/*while ((Vector3.Distance(doigt.position,p.position) > epsilonDoigtPrise) && i<10) {
				Debug.Log("CCD3dStep");
				GetComponent<CCD3d>().CCDStep3D(doigt,doigt,p);
				i++;
			}*/

			//3b : calcul du FTR
			float currentFTR = catm.ftr(directionDNT,doigt); //TODO verif si c'est le bon transform 
			if (currentFTR > bestFTR) 
			{
				bestFTR=currentFTR;
				priseProche=p;
			}
			Destroy(clone,0.001f);
		}
		return priseProche;
	}
}

