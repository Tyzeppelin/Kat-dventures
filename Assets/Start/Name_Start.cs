using UnityEngine;
using System.Collections;

public class Name_Start : MonoBehaviour {

	private int screenWidth;

	// Use this for initialization
	void Start () {
		screenWidth = Screen.width;
		print(screenWidth);
		this.guiText.fontSize = (int)(screenWidth/13.6);
	}
	
	// Update is called once per frame
	void Update () {
		if (screenWidth != Screen.width) {
			screenWidth = Screen.width;
			this.guiText.fontSize = (int)(screenWidth/13.6);
		}
	}
}
