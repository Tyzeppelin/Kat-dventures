using UnityEngine;
using System.Collections;

/// \class Clone
/// Classe comprenant le clone d'un Transform, afin de calculer la valeur de ftr 
public class Clone{

	public Transform trans;
	InfoMembre info;

	public Clone(Transform t, InfoMembre infomembre) {
		trans = t;
		//info = infomembre;

	}
	

	public void CCDStep3D(Transform joint, Transform effector, Transform target)
	{
		Vector3 qipe = new Vector3 (effector.position.x - joint.position.x, effector.position.y - joint.position.y, effector.position.z - joint.position.z);
		Vector3 qipt = new Vector3 (target.position.x - joint.position.x, target.position.y - joint.position.y, target.position.z - joint.position.z);
		float alpha = ComputeAngle3D (qipe, qipt);
		Vector3 axe = Vector3.Cross (qipe, qipt);
		joint.Rotate (axe, alpha, Space.World);

		//Verification des angles d'euler pour chaque membre
		//verifAngles(joint);
		Debug.Log ("CCD clone");

	}

	/// \brief Va vérifier si le membre est bien dans la limite fixée par les contraintes. 
	/// Contrairement à la méthode normale, celle-ci ne s'applique qu'au membre uniquement, les parents sont gérés individuellement.
	/// \param joint l'articulation en question

	private void verifAngles (Transform joint) {
		//Check the maximum limit
		Vector3 tmp = joint.localEulerAngles;
		if (joint.localEulerAngles.x > info.anglesEulerMax.x) 
		{	
			tmp.x =info.anglesEulerMax.x;
		}
		if (joint.localEulerAngles.y > info.anglesEulerMax.y)
		{
			tmp.y =info.anglesEulerMax.y;
		}
		
		if (joint.localEulerAngles.z > info.anglesEulerMax.z) 
		{
			tmp.z =info.anglesEulerMax.z;
		}
		
		
		//Check the minimum limit
		if (joint.localEulerAngles.x < info.anglesEulerMin.x) 
		{
			tmp.x =info.anglesEulerMin.x;
		}
		if (joint.localEulerAngles.y < info.anglesEulerMin.y)
		{
			tmp.y =info.anglesEulerMin.y;
		}
		
		if (joint.localEulerAngles.z < info.anglesEulerMin.z) 
		{
			tmp.z =info.anglesEulerMin.z;
		}
		joint.localEulerAngles = tmp;
	}

	/// <summary>
	/// Pour faire des calculs, quoi... (produit scalaire)
	/// </summary>
	/// <returns>The angle3 d.</returns>
	/// <param name="a">The alpha component.</param>
	/// <param name="b">The blue component.</param>
	private float ComputeAngle3D(Vector3 a, Vector3 b)
	{
		float alpha = 0;
		float prodScal = Vector3.Dot (a, b);
		alpha = Vector3.Angle(a,b);
		return alpha;
	}
}