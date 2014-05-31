using UnityEngine;
using System.Collections;

/// \class EnablePush
/// Script pour passer le chat en mode poussée ou en mode marche en appuyant sur la touche P.
public class EnablePush : MonoBehaviour {
	/// vrai si le chat est en mode poussée, faux sinon
	private bool isPushing = false;

	// Use this for initialization
	void Start () {
		gameObject.animation["marcherDebout"].wrapMode = WrapMode.Loop;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp("p")) {
			if(isPushing) {
				GameObject.Find("PatteAvantDroiteDoigts").GetComponent<CCD3d>().enabled = false;
				GameObject.Find("PatteAvantGaucheDoigts").GetComponent<CCD3d>().enabled = false;
				gameObject.animation["Lever"].speed = -1;
				gameObject.animation["Lever"].time = gameObject.animation["Lever"].length;
				gameObject.animation.Play("Lever");
				gameObject.GetComponent<LegAnimator>().enabled = true;
				isPushing = false;
			}
			else {
				gameObject.GetComponent<LegAnimator>().enabled = false;
				gameObject.animation["Lever"].speed = 1;
				gameObject.animation.Play("Lever");
				GameObject.Find("PatteAvantDroiteDoigts").GetComponent<CCD3d>().enabled = true;
				GameObject.Find("PatteAvantGaucheDoigts").GetComponent<CCD3d>().enabled = true;
				isPushing = true;
			}
		}
		else if(Input.GetKeyDown(KeyCode.UpArrow) && isPushing) {
			gameObject.animation.Play("marcherDebout");
		}
		else if(Input.GetKeyUp(KeyCode.UpArrow) && isPushing) {
			gameObject.animation.Stop();
		}
	}
}
