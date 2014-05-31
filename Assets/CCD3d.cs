using UnityEngine;
using System.Collections;

[System.Serializable]
public class InfoMembre {
	public Transform membre;
	public Vector3 anglesEuler;
	public Vector3 anglesEulerMin;
	public Vector3 anglesEulerMax;
}

[System.Serializable]
public class CCD3d : MonoBehaviour {
<<<<<<< HEAD
	#region script_parameters	
=======
	#region script_parameters
>>>>>>> pousseCaisse
	// the target we want the end-effector to reach
	// In our case the cube
	public Transform target;
	public Transform armStart;
	public Transform displayEuler;
	/*public Transform membre1;
<<<<<<< HEAD
	public Vector3 angles1;
	public Transform membre2;
	public Vector3 angles2;
	public Transform membre3;
	public Vector3 angles3;
	public Transform membre4;
	public Vector3 angles4;*/
	public InfoMembre[] tabMembre;
	#endregion script_parameters
		
	// Use this for initialization
	void Start () {
	
=======
public Vector3 angles1;
public Transform membre2;
public Vector3 angles2;
public Transform membre3;
public Vector3 angles3;
public Transform membre4;
public Vector3 angles4;*/
	public InfoMembre[] tabMembre;
	#endregion script_parameters
	
	// Use this for initialization
	void Start () {
		
>>>>>>> pousseCaisse
	}
	
	// Update is called once per frame
	void Update () {
		// in each frame we make one iteration
		CCDStep3D(gameObject.transform, gameObject.transform, target);
	}
	
	/// \brief method for computing an angle value between two 3d vectors using
	/// their dot product.
	/// \return the value of the angle existing between a and b.
	private float ComputeAngle3D(Vector3 a, Vector3 b)
	{
<<<<<<< HEAD
        float alpha = 0;
=======
		float alpha = 0;
>>>>>>> pousseCaisse
		float prodScal = Vector3.Dot (a, b);
		alpha = Vector3.Angle(a,b);
		return alpha;
	}
	
	/// \brief performs one step of the CCD algorithm in 3d. For each joint in the kinematic chain,
	/// we compute the angle theta = (effector.joint.target). We then compute the axis
<<<<<<< HEAD
	/// u =  [target-joint] ^ [effector-joint] and apply the rotation theta around this axis to drive 
=======
	/// u = [target-joint] ^ [effector-joint] and apply the rotation theta around this axis to drive
>>>>>>> pousseCaisse
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
<<<<<<< HEAD



=======
		
		
		
>>>>>>> pousseCaisse
		if(joint.parent != null && joint != armStart)
		{
			CCDStep3D(joint.parent, effector, target);
		}
<<<<<<< HEAD

		
		//Verification des angles d'euler pour chaque membre
		verifAngles(joint);

		/*print(joint.name+" : "+joint.localEulerAngles);
		for (int i = 0; i<tabMembre.Length; i++) {
			if (tabMembre[i].membre == joint) {
				tabMembre[i].anglesEuler = joint.localEulerAngles;
			}
				}*/
	}

	/// \brief allows to check if angles are ok according to values setted up in Inspector
	/// In the case that the angles are not correct, their values are imposed 
	/// \param joint the joint for which angles have to be checked
	private void verifAngles (Transform joint) {
		//Looking for the referenced joint 
		for (int i =tabMembre.Length -1; i>0; --i) {
			if (joint == tabMembre[i].membre) {

				//Check the maximum limit
				Vector3 tmp = joint.localEulerAngles;
				if (joint.localEulerAngles.x > tabMembre[i].anglesEulerMax.x) 
				{
					//Debug.Log( joint.localEulerAngles.x);

=======
		
		
		//Verification des angles d'euler pour chaque membre
		verifAngles(joint);
		
		/*print(joint.name+" : "+joint.localEulerAngles);
for (int i = 0; i<tabMembre.Length; i++) {
if (tabMembre[i].membre == joint) {
tabMembre[i].anglesEuler = joint.localEulerAngles;
}
}*/
	}
	
	/// \brief allows to check if angles are ok according to values setted up in Inspector
	/// In the case that the angles are not correct, their values are imposed
	/// \param joint the joint for which angles have to be checked
	private void verifAngles (Transform joint) {
		//Looking for the referenced joint
		for (int i =tabMembre.Length -1; i>0; --i) {
			if (joint == tabMembre[i].membre) {
				
				//Check the maximum limit
				Vector3 tmp = joint.localEulerAngles;
				if (joint.localEulerAngles.x > tabMembre[i].anglesEulerMax.x)
				{
					//Debug.Log( joint.localEulerAngles.x);
					
>>>>>>> pousseCaisse
					tmp.x =tabMembre[i].anglesEulerMax.x;
					//Debug.Log( joint.localEulerAngles.x);
				}
				if (joint.localEulerAngles.y > tabMembre[i].anglesEulerMax.y)
				{
					tmp.y =tabMembre[i].anglesEulerMax.y;
				}
<<<<<<< HEAD

				if (joint.localEulerAngles.z > tabMembre[i].anglesEulerMax.z) 
				{
					tmp.z =tabMembre[i].anglesEulerMax.z;
				}


				//Check the minimum limit
				if (joint.localEulerAngles.x < tabMembre[i].anglesEulerMin.x) 
=======
				
				if (joint.localEulerAngles.z > tabMembre[i].anglesEulerMax.z)
				{
					tmp.z =tabMembre[i].anglesEulerMax.z;
				}
				
				
				//Check the minimum limit
				if (joint.localEulerAngles.x < tabMembre[i].anglesEulerMin.x)
>>>>>>> pousseCaisse
				{
					tmp.x =tabMembre[i].anglesEulerMin.x;
				}
				if (joint.localEulerAngles.y < tabMembre[i].anglesEulerMin.y)
				{
					tmp.y =tabMembre[i].anglesEulerMin.y;
				}
				
<<<<<<< HEAD
				if (joint.localEulerAngles.z < tabMembre[i].anglesEulerMin.z) 
=======
				if (joint.localEulerAngles.z < tabMembre[i].anglesEulerMin.z)
>>>>>>> pousseCaisse
				{
					tmp.z =tabMembre[i].anglesEulerMin.z;
				}
				joint.localEulerAngles = tmp;
			}
		}
	}
}