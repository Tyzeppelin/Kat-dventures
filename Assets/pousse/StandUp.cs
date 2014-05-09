using UnityEngine;
using System.Collections;

public class StandUp : MonoBehaviour {
	#region script_parameters	
	// the target we want the end-effector to reach
	// In our case the cube
	public Transform start;
	#endregion script_parameters
	//Tete du nouveau squelette
	public Transform[] i = new Transform[5];
	private int a = 1;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		CCDStep3D(i[1], i[0]);
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
	private void CCDStep3D(Transform joint, Transform effector)
	{
		if (a < 5) 
		{
			//Transform joint = maillonJoint.transform;
			Vector3 qipe = new Vector3 (effector.position.x - joint.position.x, effector.position.y - joint.position.y, effector.position.z - joint.position.z);
			Vector3 qipt = new Vector3 (start.position.x - joint.position.x, start.position.y + 500 - joint.position.y, start.position.z + 200 - joint.position.z);
			float alpha = ComputeAngle3D (qipe, qipt);
			Vector3 axe = Vector3.Cross (qipe, qipt);
			joint.Rotate (axe, alpha, Space.World);

			//if (maillonJoint.pere != null && !joint.Equals (start)) {
			a = a + 1;
					CCDStep3D (i[a], effector);
			//}
		}
	}
}
