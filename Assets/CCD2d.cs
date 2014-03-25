using UnityEngine;
using System.Collections;

public class CCD2d : MonoBehaviour {
	#region script_parameters	
	// the target we want the end-effector to reach
	// In our case the cube
	public Transform target;	
	#endregion script_parameters
	
	// the z vector around which the joints are rotated in 2d
	private Vector3 z = new Vector3(0,0,1);
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		CCDStep2D(gameObject.transform.parent, gameObject.transform, target);
	}
	
	
	/// \brief method for computing a signed angle value between two 2d vectors using
	/// their dot product. Signed is determined using the trigonometric direct way.
	/// for instance if we have a = new Vector2(1,0) and b = new Vector2(0,1)
	/// then ComputeAngle2D(a, b) = Pi / 2 and ComputeAngle2D(b, a) = -Pi / 2
	/// \return the value of the signed angle existing between a and b.
	private float ComputeAngle2D(Vector2 a, Vector2 b)
	{
		float alpha = 0;
		if (a.magnitude <= 0.001 || b.magnitude <= 0.001) 
		{
			return alpha;
		}
		a.Normalize ();
		b.Normalize ();
		float prodScal = Vector2.Dot (a, b);
		float signe = (a.x * b.y - a.y * b.x);
		prodScal = Mathf.Clamp(prodScal, -1, 1);
		return Mathf.Sign (signe) * Mathf.Acos (prodScal);
	}
	
	/// \brief performs one step of the CCD algorithm in 2d. For each joint in the kinematic chain,
	/// we compute the signed angle theta = (effector.joint.target). Since unity transforms work in 3d,
	/// the coordinates have to be projected in 2d.
	/// We then apply the rotation theta around the z axis to drive 
	/// the effector towards the target.The method is recursive
	/// and calls itself by going up the joint hierarchy.
	/// \param joint the current joint that will be rotated towards the target.
	/// \param effector the end effector transform.
	/// \param target the transform containing the position we want the end-effector to reach.
	private void CCDStep2D(Transform joint, Transform effector, Transform target)
	{
		Vector2 qipe = new Vector2 (effector.position.x - joint.position.x, effector.position.y - joint.position.y);
		Vector2 qipt = new Vector2 (target.position.x - joint.position.x, target.position.y - joint.position.y);
		float alpha = ComputeAngle2D (qipe, qipt);
	    joint.Rotate (z, alpha);
		if(joint.parent != null)
		{
			CCDStep2D(joint.parent, effector, target);
		}
	}	
}
