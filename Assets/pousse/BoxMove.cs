using UnityEngine;
using System.Collections;

public class BoxMove : MonoBehaviour {
	public bool moving = false;
	public Transform patteGauche;
	public Transform patteDroite;
	public float e;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 v = patteDroite.position - gameObject.transform.position;
		Vector3 v2 = patteGauche.position - gameObject.transform.position;
		if (v.sqrMagnitude < e || v2.sqrMagnitude < e) 
		{
			gameObject.transform.rigidbody.AddForce(GameObject.Find("cat").transform.forward / 6);
		}
	}
}
