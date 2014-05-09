using UnityEngine;
using System.Collections;

public class Name_Start : MonoBehaviour {

	private int screenWidth;

	// Use this for initialization
	void Start () {
		screenWidth = Screen.width;
		// Vector3 pos = new Vector3(Mathf.Atan(screenRes.width)/(4*Mathf.PI),Mathf.Atan(screenRes.height)/(2*Mathf.PI),0);
		// this.transform.position = pos;
		print(screenWidth);
		this.guiText.fontSize = (int)(screenWidth/13.6);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
