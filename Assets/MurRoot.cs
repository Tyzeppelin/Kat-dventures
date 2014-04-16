using UnityEngine;
using System.Collections;

public class MurRoot : MonoBehaviour {

	public Transform mur;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Gestion du déplacement du root

		//Vecteur mouvement
		Vector3 move = new Vector3(0,0,0);

		Vector3 val;
		if (Input.GetKey(KeyCode.UpArrow))
		{
			//val = mur.up; val.Scale(0.1);
			move = move + (0.1f * mur.up);
		}
		/*if (Input.GetKey (KeyCode.DownArrow)) 
						move = move - 0.1 * mur.up;

		if (Input.GetKey (KeyCode.LeftArrow))
						move = move - 0.1 * mur.forward;
		if (Input.GetKey (KeyCode.RightArrow))
			move = move + 0.1 * mur.forward;*/

		transform.position += move;

		//Le passage à une prise ou à une autre est faite par les scripts MurDoigts
	}
}
