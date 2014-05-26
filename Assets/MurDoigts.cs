using UnityEngine;
using System.Collections;

public class MurDoigts : MonoBehaviour {

	public Transform[] TableauDePrises;
	public Transform DebutMembre;
	public Transform PriseDeDebut;
	public int epsilonDoigtPrise; //Correspond à la distance limite entre les doigts et la prise
	public int epsilonEpaulePrise; //Correspond à la distance minimale entre l'épaule et la prise (TECCC)
	public Vector3 directionDNT; //Direction voulue par le joueur

	Transform priseEnCours;


	// Use this for initialization
	void Start () {
		priseEnCours = PriseDeDebut;
		CCD3d test = (CCD3d)(transform.GetComponent ("CCD3d"));
		test.target=PriseDeDebut;
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

	/// \brief Method to get the best target to reach ; TECCC
	///
	/// \return The prise
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
			CCD3d ccd = (CCD3d) transform.GetComponent("CCD3d");
			Transform armStart=ccd.armStart;
				//Création du clone
			Transform cloneBras = (Transform) Instantiate(armStart);
			cloneBras.renderer.enabled=false; //on le rend invisible
				//Avec le clone, les scripts sont aussi copiés : on ne garde que celui de CCD, que l'on paramètre bien
			Transform doigt = armStart;
			while (doigt.childCount != 0) doigt=doigt.GetChild(0);
			doigt.GetComponent<MurDoigts>().enabled=false;
			doigt.GetComponent<CatManipulability>().enabled=false;
			doigt.GetComponent<CCD3d>().target=p;
			doigt.GetComponent<CCD3d>().enabled=true;
				//On fait tourner l'algo de CCD
			// TODO

			//3b : calcul du FTR
			CatManipulability script = (CatManipulability) transform.GetComponent("CatManipulability");
			float currentFTR=script.ftr(directionDNT); 
			if (currentFTR > bestFTR) 
			{
				bestFTR=currentFTR;
				priseProche=p;
			}
		}
		return priseProche;
	}
}
