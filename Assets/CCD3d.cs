using UnityEngine;
using System.Collections;

public class CCD3d : MonoBehaviour {
	#region script_parameters	
	// the target we want the end-effector to reach
	// In our case the cube
	public Transform target;
	public Transform armStart;
	public Transform displayEuler;
	#endregion script_parameters
		
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// in each frame we make one iteration
		CCDStep3D(gameObject.transform.parent, gameObject.transform, target);
	}
	
	/// \brief method for computing an angle value between two 3d vectors using
	/// their dot product.
	/// \return the value of the angle existing between a and b.
	private float ComputeAngle3D(Vector3 a, Vector3 b)
	{
        float alpha = 0;
		float prodScal = Vector3.Dot (a, b);
		alpha = Vector3.Angle(a,b);
		return alpha;
	}
	
	/// \brief performs one step of the CCD algorithm in 3d. For each joint in the kinematic chain,
	/// we compute the angle theta = (effector.joint.target). We then compute the axis
	/// u =  [target-joint] ^ [effector-joint] and apply the rotation theta around this axis to drive 
	/// the effector towards the target.The method is recursive
	/// and calls itself by going up the joint hierarchy.
	/// \param joint the current joint that will be rotated towards the target.
	/// \param effector the end effector transform.
	/// \param target the transform containing the position we want the end-effector to reach.
	private void CCDStep3D(Transform joint, Transform effector, Transform target)
	{
		Vector3 qipe = new Vector3 (effector.position.x - joint.position.x, effector.position.y - joint.position.y, effector.position.z - joint.position.z);
		Vector3 qipt = new Vector3 (target.position.x - joint.position.x, target.position.y - joint.position.y, target.position.z - joint.position.z);
		float alpha = ComputeAngle3D (qipe, qipt);
		Vector3 axe = Vector3.Cross (qipe, qipt);
		joint.Rotate (axe, alpha, Space.World);

		if(joint.parent != null && joint != armStart)
		{
			CCDStep3D(joint.parent, effector, target);
		}


			print(joint.name+" : "+transform.localEulerAngles);

	}
}
