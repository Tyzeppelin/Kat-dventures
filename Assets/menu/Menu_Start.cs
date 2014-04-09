using UnityEngine;
using System.Collections;

public class Menu_Start : MonoBehaviour {

	private Color s_color_idle;
	private Color s_color_active;

	private int s_size_idle = 150;
	private int s_size_active = 160;

	public string levelName;

	private GUIText menu;

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
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
