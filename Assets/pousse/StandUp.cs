using UnityEngine;
using System.Collections;

public class StandUp : MonoBehaviour {
	#region script_parameters	
	// the target we want the end-effector to reach
	// In our case the cube
	public Transform start;
	public Transform[] squelette = new Transform[5];
	#endregion script_parameters
	private int maxY = 500;
	private int maxZ = 200;
	private int y = 1;
	private int z = 1;

	// Use this for initialization
	void Start () {
		int i = 1;
		Vector3 target = new Vector3 (start.position.x, start.position.y + 1, start.position.z + 1);
		CCDStep3D(squelette[1], squelette[0], target, i);
	}

	// Update is called once per frame
	void Update () {

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
	private void CCDStep3D(Transform joint, Transform effector, Vector3 target, int i)
	{
		Vector3 qipe = effector.position - joint.position;
		Vector3 qipt = new Vector3 (start.position.x - joint.position.x, start.position.y - joint.position.y, start.position.z - joint.position.z);
		//Vector3 qipt = start.position - target; 
		float alpha = ComputeAngle3D (qipe, qipt);
		Vector3 axe = Vector3.Cross (qipe, qipt);
		joint.Rotate (axe , alpha , Space.World);
		if (joint.parent != null && !joint.Equals (start) && i < squelette.Length-1)
		{
			i = i + 1;
			CCDStep3D (squelette[i], effector, target, i);
		}
		else 
		{
			i = 1;
			//if (y < maxY) target = new Vector3 (target.x, target.y + 1, target.z);
			//if (z < maxZ) target = new Vector3 (target.x, target.y, target.z + 1);
			//if (y < maxY && z < maxZ) CCDStep3D(squelette[1], squelette[0], target, i);
		}
	}
}
