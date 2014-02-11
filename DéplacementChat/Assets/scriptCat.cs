using UnityEngine;
using System.Collections;

public class scriptCat : MonoBehaviour {

	// Inspector Variables
	public float catWalkingSpeed 	= 1F;		// Cat walking speed
	public float catRunningSpeed 	= 4F;		// Cat running speed
	public float catRotationSpeed 	= 80F;		// Cat turning speed
	
	// Private Variables
	private float catSpeedCoef		= 0.3F;		// Necessary coefficient to the cat translation matchs the animation
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		float catTranslationSpeed = 0;
		
		// Turn the cat
		transform.Rotate(0, Input.GetAxis("Horizontal") * catRotationSpeed * Time.deltaTime, 0);	// ~ (Time.deltaTime : coef to transfom m/frame into m/sec)
		
		// Move cat forward and backward
		if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.1)												// If one of the vertical directionnal key is down
		{
			// Calculate the default cat speed
			catTranslationSpeed = Time.deltaTime * Input.GetAxis("Vertical") * catSpeedCoef;		// ~ (Input.GetAxis("Vertical") can be negative)
			
			// Running mode
			if(Input.GetAxis("Fire1")>0.1)															// If the Ctrl key is down
			{
				animation["Walking"].speed = catRunningSpeed;										// the cat speed and the cat animation speed
				catTranslationSpeed *= catRunningSpeed;												// are multiply by the catRunningSpeed
			}
			// Walking mode
			else																					// If it isn't
			{
				animation["Walking"].speed = catWalkingSpeed;										// the cat speed and the cat animation speed
				catTranslationSpeed *= catWalkingSpeed;												// are multiply by the catWalkingSpeed
			}
			// Backward movement
			if(Input.GetAxis("Vertical") < 0)														// If it's a backward movement
			{
				animation["Walking"].speed *= -1;													// Set the animation to play it backward : 
				if(animation["Walking"].time == 0)													// - set the speed animation to negative one
				{																					// - set the beginning of the animation to its end
					animation["Walking"].time = animation["Walking"].length;						//
				}
			}
			
			animation.Play("Walking");																// Play the walking animation
			transform.Translate(0, 0, catTranslationSpeed);											// Move the cat on the axis
		}
		else																						// If no vertical directionnal key is down
		{
			animation.Play("repos");																// Stop the animation and play the default animation
		}
	}
}
