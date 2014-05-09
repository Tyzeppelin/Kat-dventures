using UnityEngine;
using System.Collections;

public class Walk : MonoBehaviour {
	public Vector3[] trajectoire = new Vector3[4];
	public Transform armStart;

	private int i = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.deltaTime >= 1) {
		gameObject.transform.position = gameObject.transform.position + trajectoire[i];
		//target.position = target.position + trajectoire[i];

		//CCDStep3D(gameObject.transform.parent, gameObject.transform, target);
		i++;
			}
	}
}
