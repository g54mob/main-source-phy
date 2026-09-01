using System;
using UnityEngine;

[Serializable]
public struct ObjectToDissolve
{
	public MeshRenderer renderer;

	public Transform initialTransform;

	public ObjectToDissolve(MeshRenderer rendy, Transform initialTransf)
	{
		renderer = rendy;
		initialTransform = initialTransf;
	}
}
