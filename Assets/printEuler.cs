using UnityEngine;
using System.Collections;

public class printEuler : MonoBehaviour {

	public Vector3 localEuler;
	public Vector3 globalEuler;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		localEuler=transform.localEulerAngles;
		globalEuler=transform.eulerAngles;
	}
}
