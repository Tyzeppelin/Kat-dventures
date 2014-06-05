using UnityEngine;
using System.Collections;

/// \class BoxMove
/// Script pour déplacer un objet lorsqu'il est poussé. 
/// A chaque frame, on teste si la distance entre les pattes du chat et l'objet à pousser est inférieure à un epsilon petit. Si oui on applique une force sur l'objet.
public class BoxMove : MonoBehaviour {
	/// patte gauche du chat
	public Transform patteGauche;
	/// patte droite du chat
	public Transform patteDroite;

	public Transform ciblegauche;
	public Transform cibledroit;

	/// epsilon 
	public float e;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 v = patteDroite.position - cibledroit.transform.position;
		Vector3 v2 = patteGauche.position - ciblegauche.transform.position;
		Debug.Log (v.magnitude+"");
		if (v.magnitude < e || v2.magnitude < e) 
		{

			gameObject.transform.rigidbody.AddForce(GameObject.Find("cat").transform.forward * 10);
		}
	}
}
