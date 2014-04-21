using UnityEngine;
using System.Collections;

public class Name_Start : MonoBehaviour {

	// Use this for initialization
	void Start () {
		screenRes = Screen.currentResolution;
		Vector3 pos = new Vector3(Mathf.Atan(screenRes.width)/(2*Mathf.PI),Mathf.Atan(screenRes.height)/(2*Mathf.PI),0);
		this.transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
