using UnityEngine;
using System.Collections;

/// \class MurRoot
/// Script appliqué à la racine du chat : gestion des directions voulues par le joueur sur le mur.
public class MurRoot : MonoBehaviour {

	public Transform mur; ///le mur d'escalade
	public int distanceMinimale; ///la distance minimale pour que le chat s'aggripe au mur
	public Transform[] tabDoigts; ///tableau des doigts du chat



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

	/// \brief Active ou désactive le script MurDoigt pour les 4 membres du chat
	/// \param b état d'activation des scripts MurDoigt des membres
	private void activateScript (bool b) {
		for (int i=0; i<tabDoigts.Length; i++) {
			tabDoigts[i].GetComponent<MurDoigts>().enabled = b;
		}
	}

	/// \brief Envoie la direction du mouvement voulu aux scripts MurDoigt (dans la variable directionDNT)
	/// \param dir la direction voulue par le joueur
	private void passageDir(Vector3 dir) {
		for (int i=0; i<tabDoigts.Length; i++) {
			MurDoigts compon = (MurDoigts) (tabDoigts[i].GetComponent("MurDoigts"));
			compon.directionDNT=dir;
				}
				
	}
}
