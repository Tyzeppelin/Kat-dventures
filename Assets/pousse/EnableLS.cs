using UnityEngine;
using System.Collections;

public class EnableLS : MonoBehaviour {
	public Transform target;
	public Transform patte;

	private bool ena = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp("p")) {
			if(ena) {
				gameObject.animation.enabled = false;
				gameObject.GetComponent<LegAnimator>().enabled = false;
				gameObject.GetComponent<NormalCharacterMotor>().enabled = false;
				gameObject.GetComponent<PlatformCharacterController>().enabled = false;

				target.GetComponent<TargetController>().enabled = true;
				patte.GetComponent<CCD3d>().enabled = true;
				ena = false;
			}
			else {
				gameObject.animation.enabled = true;
				gameObject.GetComponent<LegAnimator>().enabled = true;
				gameObject.GetComponent<NormalCharacterMotor>().enabled = true;
				gameObject.GetComponent<PlatformCharacterController>().enabled = true;
				
				target.GetComponent<TargetController>().enabled = false;
				patte.GetComponent<CCD3d>().enabled = false;
				ena = true;
			}
		}
	}
}
