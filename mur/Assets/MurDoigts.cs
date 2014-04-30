using UnityEngine;
using System.Collections;

public class MurDoigts : MonoBehaviour {

	public Transform[] TableauDePrises;
	public Transform DebutMembre;
	public Transform PriseDeDebut;
	public int epsilon; //Correspond à la distance limite entre les doigts et la prise

	private Transform priseEnCours;


	// Use this for initialization
	void Start () {
		priseEnCours = PriseDeDebut;

	}
	
	// Update is called once per frame
	void Update () {

		//Vérification de la distance 
		if (Vector3.Distance (transform.position, priseEnCours.position) > epsilon) {
			Transform priseProche = priseEnCours;
			//Recherche de la prise la plus proche 
			for (int i =0;i<TableauDePrises.Length;i++) {
				if (Vector3.Distance(transform.position,TableauDePrises[i].position) <  Vector3.Distance(transform.position,priseEnCours.position) 
				    priseEnCours = TableauDePrises[i];
			}
		}

	    //Ensuite, il faut changer la target pour le script CCD
		//...
	}
}
