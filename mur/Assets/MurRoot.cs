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
		Vector2 move = new Vector2(0,0,0);

		if (Input.GetKey(KeyCode.UpArrow))
			move = move + 0.1*mur.up;
		if (Input.GetKey (KeyCode.DownArrow)) 
						move = move - 0.1 * mur.up;

		if (Input.GetKey (KeyCode.LeftArrow))
						move = move - 0.1 * mur.forward;
		if (Input.GetKey (KeyCode.RightArrow))
			move = move + 0.1 * mur.forward;

		transform.position += move;

		//Le passage à une prise ou à une autre est faite par les scripts MurDoigts
	}
}
