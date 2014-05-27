﻿using UnityEngine;
using System.Collections;

public class CatManipulability{
	private const float epsilon = 0.001f;
	private const float epsilon2 = 0.0000000000001f;
	private Transform from_;
	private Transform effector_;
	private int dim;
	// Use this for initialization
	public CatManipulability (Transform from) {
		this.from_ = from;
		// count number of elements
		dim = 0;
		Transform temp = from; // assumes simple trees
		while (temp.childCount>0)
		{
			++ dim;
			temp = temp.GetChild(0);
		}
		dim = dim * 3;
		effector_ = temp;
	}
	
	public float ftr(Vector3 direction)
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
		dir[1,0] = direction.y; dirT[0,1] = direction.y;
		dir[2,0] = direction.z; dirT[0,2] = direction.z;
		// computing jacobian matrix
		ComputeJacobian(jacobian, jacobianTr);
		// jacobian product
		Multiply(jacobian, jacobianTr, res);
		Multiply(dirT, res, restmp);
		Multiply(restmp, dir, value);
		return 1f / (value[0,0] + epsilon2)  ;
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
		Vector3 val = current.localEulerAngles;
		val.x = minus.x; val.y = minus.y; val.z = minus.z;
		current.localEulerAngles = val;
		Transform tmp = current;
		while(tmp.childCount > 0)
		{
			tmp = tmp.GetChild(0);
		}
		Vector3 posMinus = new Vector3(tmp.position.x, tmp.position.y, tmp.position.z);
		val = current.localEulerAngles;
		val.x = maxi.x; val.y = maxi.y; val.z = maxi.z;
		current.localEulerAngles = val;
		tmp = current;
		while(tmp.childCount > 0)
		{
			tmp = tmp.GetChild(0);
		}
		Vector3 posMaxi = new Vector3(tmp.position.x, tmp.position.y, tmp.position.z);

		val = current.localEulerAngles;
		val.x = lAngles.x; val.y = lAngles.y; val.z = lAngles.z;
		current.localEulerAngles = val;

		//current.eulerAngles.Set(lAngles.x, lAngles.y, lAngles.z);
		return (posMaxi - posMinus) / (2 * epsilon);
	}

	private void ComputeJacobian(float [,] jacobian, float [,] jacobianTr)
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
				jacobianTr[col+i,0] = res.x;
				jacobianTr[col+i,1] = res.y;
				jacobianTr[col+i,2] = res.z;
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
