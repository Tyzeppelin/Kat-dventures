using UnityEngine;
using System.Collections;



public class Move : MonoBehaviour {

	public Vector3 target1 = new Vector3(0,0,0);
	public Vector3 target2 = new Vector3(0,0,0);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Déplacement au clavier du cube
		transform.Translate(Input.GetAxis("Horizontal"),0,0);

	}
}