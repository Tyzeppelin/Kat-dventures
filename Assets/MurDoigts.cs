using UnityEngine;
using System.Collections;

public class MurDoigts : MonoBehaviour {

	public Transform[] TableauDePrises;
	public Transform DebutMembre;
	public Transform PriseDeDebut;
	public int epsilon; //Correspond à la distance limite entre les doigts et la prise

	Transform priseEnCours;


	// Use this for initialization
	void Start () {
		priseEnCours = PriseDeDebut;
		CCD3d test = (CCD3d)(transform.GetComponent ("CCD3d"));
		//test.target=PriseDeDebut;
	}
	
	// Update is called once per frame
	void Update () {

		//Vérification de la distance 
		if (Vector3.Distance (transform.position, priseEnCours.position) > epsilon) {
			priseEnCours=getPrise ();
		}

	    //Ensuite, il faut changer la target pour le script CCD
		CCD3d test = (CCD3d)(transform.GetComponent ("CCD3d"));
	    test.target=priseEnCours;
	}
	
	/// <summary>
	/// Method to get the best target to reach ; Steve's method
	/// </summary>
	/// <returns>The prise.</returns>
	Transform getPrise() {
		Transform priseProche = priseEnCours; // TODO a revoir 
		//Recherche de la prise la plus proche 
		for (int i =0;i<TableauDePrises.Length;i++) {
			if (Vector3.Distance(transform.position,TableauDePrises[i].position) <  Vector3.Distance(transform.position,priseProche.position)) 
				priseProche = TableauDePrises[i];
		}
		return priseProche;
	}
}
