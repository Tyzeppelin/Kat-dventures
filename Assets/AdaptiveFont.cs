/// \class AdaptiveFont
/// Script pour modifier dynamiquement la taille de la police du bouton.
/// Ce script sert aussi à charger les scènes lorsque le bouton est pressé.
public class AdaptiveFont : MonoBehaviour {
	#region script_parameters	
	/// la taille de la police au repos
	private int s_size_idle;
	/// la taille de la police lorsque le bouton est survolé par la souris
	private int s_size_active;
	/// l'identificateur du niveau à charger au clic
	public string levelName;
	/// la largeur actuelle de la fenêtre
	private int screenRes;
	#endregion script_parameters
	// Use this for initialization
	void Start () {
	screenRes = Screen.width;
		s_size_idle = (int)(screenRes/13.6);
		s_size_active = s_size_idle+10;
		this.guiText.fontSize = s_size_idle
	}

	// Update is called once per frame
	void Update () {
	}

	/// \brief Lorsque la souris arrive au-dessus du bouton, 
	/// la police passe à la taille s_size_active
	void OnMouseEnter() {
		this.guiText.fontSize = s_size_active;
	}

	/// \brief Lorsque la souris quitte le bouton, 
	/// la police passe à la taille s_size_idle
	public void OnMouseExit() {
		this.guiText.fontSize = s_size_idle;
	}

  /// \brief Lorsque l'utilisateur relache le clic, 
  /// on charge le niveau "levelName"
	void OnMouseUp() {
		Application.LoadLevel(levelName);

	}
}
