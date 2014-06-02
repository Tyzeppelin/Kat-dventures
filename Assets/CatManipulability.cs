using UnityEngine;
using System.Collections;

/// \class CatManipulability
/// Classe de calcul pour la meilleure position pour le choix des prises


public class CatManipulability{
	private const float epsilon = 0.001f;
	private Transform from_;
	private Transform effector_;
	private int dim;
	// Use this for initialization
	public CatManipulability () {
		/*this.from_ = from;
		// count number of elements
		dim = 0;
		Transform temp = from; // assumes simple trees
		while (temp.childCount>0)
		{
			++ dim;
			temp = temp.GetChild(0);
		}
		dim = dim * 3;
		effector_ = temp;*/
	}
	
	/// \brief Méthode de calcul pour trouver la meilleure prise.
	/// \param direction la direction voulue par le joueur (vers le haut, le bas,...)
	/// \param from la Transform sur laquelle le calcul va se faire
	/// \return une valeur de comparaison exprimant si la prise est un bon choix.
	public float ftr(Vector3 direction, Transform from)
	{
		float [,] jacobian = new float[3,dim];		
		float [,] jacobianTr = new float[dim,3];	
		float [,] res = new float[3,3];
		float [,] restmp = new float[1,3];
		float [,] value = new float[1,1];
		float [,] dir = new float[3,1];
		float [,] dirT = new float[1,3];
		// init directional vector
		dir[0,0] = direction.x; dirT[0,0] = direction.x;
		dir[1,0] = direction.y; dirT[0,1] = direction.z;
		dir[2,0] = direction.z; dirT[0,2] = direction.z;
		// computing jacobian matrix
		ComputeJacobian(jacobian);
		// jacobian product
		Multiply(jacobian, jacobianTr, res);
		Multiply(dirT, res, restmp);
		Multiply(restmp, dir, value);
		return value[0,0];
	}

	private Vector3 partialDerivate(int axis, Transform current, Transform effector)
	{
		Vector3 position = effector.position;
		Vector3 lAngles = current.localEulerAngles;
		Vector3 minus = new Vector3(lAngles.x, lAngles.y, lAngles.z);
		Vector3 maxi = new Vector3(lAngles.x, lAngles.y, lAngles.z);
		switch(axis)
		{
			// z  x  y
		case 1:
		{
			minus.x -= epsilon;
			maxi.x += epsilon;
			break;
		}
		case 2:
		{
			minus.y -= epsilon;
			maxi.y += epsilon;
			break;
		}
		case 0:
		{
			minus.z -= epsilon;
			maxi.z += epsilon;
			break;
		}
		default:break;
		}
		current.localEulerAngles = minus;
		Vector3 posMinus = effector.position;
		current.localEulerAngles = maxi;
		Vector3 posMaxi = effector.position;
		return (posMaxi - posMinus) / 2 * epsilon;
	}

	private void ComputeJacobian(float [,] jacobian)
	{
		Transform tr = from_;
		int col = 0;
		while(tr != effector_)
		{
			for(int i = 0; i< 3; ++i)
			{
				Vector3 res = partialDerivate(i, tr, effector_);
				jacobian[0, col+i] = res.x;
				jacobian[1, col+i] = res.y;
				jacobian[2, col+i] = res.z;
			}
			if(tr.childCount > 0)
        	{
				tr = tr.GetChild(0);
				col += 3;
         	}
		}
	}

	private void Multiply(float[,] jac, float[,] jacT, float[,] res)
	{
		for(int ligne = 0; ligne < jac.GetLength(0); ++ ligne)
		{
			for( int col = 0; col < jacT.GetLength(1); ++ col)
			{
				res[ligne, col] = 0;
				for(int i =0; i < jac.GetLength(1); ++i)
				{
					res[ligne, col] += jac[ligne, i] * jacT[i, col];
				}
			}
		}
	}
}
