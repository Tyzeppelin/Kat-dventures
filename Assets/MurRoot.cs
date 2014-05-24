using UnityEngine;
using System.Collections;

public class MurRoot : MonoBehaviour {

	public Transform mur;
	public int distanceMinimale;
	public Transform[] tabDoigts;



	// Use this for initialization
	void Start () {
		activateScript (false);
	
	}
	
	// Update is called once per frame
	void Update () {
		//Gestion du déplacement du root

		//Vecteur mouvement
		Vector3 move = new Vector3(0,0,0);

		//Vector3 val;
		if (Input.GetKey (KeyCode.UpArrow)) {
			//val = mur.up; val.Scale(0.1);
						move = move + (0.1f * mur.up);
						passageDir (mur.up);
				}
		if (Input.GetKey (KeyCode.DownArrow)) {
						move = move - (0.1f * mur.up);
						passageDir(Vector3.zero-mur.up);
				}
		if (Input.GetKey (KeyCode.LeftArrow)) {
						move = move - (0.1f * mur.forward);
						passageDir(Vector3.zero-mur.forward);
				}
		if (Input.GetKey (KeyCode.RightArrow)) {
						move = move + (0.1f * mur.forward);
						passageDir(mur.forward);
				}
		transform.position += move;

		//Le passage à une prise ou à une autre est faite par les scripts MurDoigts

		//Activation ou non du script murDoigts : quand on est assez près
		if (Vector3.Distance (transform.position, mur.position) < distanceMinimale) 
						activateScript (true);
	}

	/// <summary>
	/// Activates the script for the 4 members of the cat
	/// </summary>
	/// <param name="b">If set to <c>true</c> b.</param>
	private void activateScript (bool b) {
		for (int i=0; i<tabDoigts.Length; i++) {
			tabDoigts[i].GetComponent<MurDoigts>().enabled = b;
		}
	}

	private void passageDir(Vector3 dir) {
		for (int i=0; i<tabDoigts.Length; i++) {
			MurDoigts compon = (MurDoigts) (tabDoigts[i].GetComponent("MurDoigts"));
			compon.directionDNT=dir;
				}
				
	}
}
