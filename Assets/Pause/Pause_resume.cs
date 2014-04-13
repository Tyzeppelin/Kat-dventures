using UnityEngine;
using System.Collections;

public class Pause_resume : MonoBehaviour {
	
	private Color s_color_idle;
	private Color s_color_active;
	
	public static int s_size_idle = 200;
	private int s_size_active = (int)(s_size_idle*1.2);
	
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