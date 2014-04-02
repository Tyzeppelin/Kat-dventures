using UnityEngine;
using System.Collections;

public class scriptSceneManager : MonoBehaviour {
	// Inspector Variables
	public Camera mainCamera;			// Main camera of the scene
	public Camera thirdViewCamera;		// Camera for the third person view
	
	// Private Variables

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		// 1 : Main view
		if(Input.GetKeyDown("1"))
		{
			mainCamera.enabled = true;
			thirdViewCamera.enabled = false;
		}
		// 2 : Third person view
		if(Input.GetKeyDown("2"))
		{
			mainCamera.enabled = false;
			thirdViewCamera.enabled = true;
		}
	
	}
}
