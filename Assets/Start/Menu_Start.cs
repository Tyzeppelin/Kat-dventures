using UnityEngine;
using System.Collections;

public class Menu_Start : MonoBehaviour {

	private int s_size_idle;
	private int s_size_active;

	public string levelName;

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
		screenRes = Screen.width;
		s_size_idle = (int)(screenRes/13.6);
		s_size_active = s_size_idle+10;
		this.guiText.fontSize = s_size_idle;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
