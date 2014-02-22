using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour {

	public Transform target;
	/*Control target with directional keys + V and B*/
	void Update () {
		float delta = Time.deltaTime * 10;
		gameObject.transform.Translate(Input.GetAxis("Horizontal") * delta, 0, 0);
		gameObject.transform.Translate(0, Input.GetAxis("Vertical") * delta, 0);
		if(Input.GetKey(KeyCode.V))
		{
			gameObject.transform.Translate(0, 0, delta);
		}
		if(Input.GetKey(KeyCode.B))
		{
			gameObject.transform.Translate(0, 0, -delta);
		}
	}
}
	