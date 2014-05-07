using UnityEngine;
using System.Collections;


public class Menu_Start : MonoBehaviour {

	private Color s_color_idle;
	private Color s_color_active;

	private int s_size_idle = 100;
	private int s_size_active = 110;

	public string levelName;

	private GUIText menu;

	private int screenRes;

	void OnMouseEnter() {
		this.guiText.fontSize = s_size_active;
	}

	void OnMouseExit() {
		this.guiText.fontSize = s_size_idle;
	}

	void OnMouseUp() {
		Application.LoadLevel(levelName);

	}

	// Use this for initialization
	void Start () {
		this.guiText.fontSize = s_size_idle;
		screenRes = Screen.width;
		//Vector3 pos = new Vector3(Mathf.Atan(screenRes)/(2*Mathf.PI),Mathf.Atan(screenRes)/(2*Mathf.PI),0);
		this.transform.position = new Vector3(Mathf.Atan(screenRes)/(2*Mathf.PI),(float)0.20,0);

	}
	
	// Update is called once per frame
	void Update () {
		if (screenRes!= Screen.width) {
			this.guiText.fontSize = s_size_idle;
		    screenRes = Screen.width; //Vector3 pos = new Vector3(Mathf.Atan(screenRes)/(2*Mathf.PI),Mathf.Atan(screenRes)/(2*Mathf.PI),0);
		    this.transform.position = new Vector3(Mathf.Atan(screenRes)/(2*Mathf.PI),(float)0.25,0);

		}
	}
}
