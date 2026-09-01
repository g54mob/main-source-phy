using System;
using UnityEngine;

[Serializable]
public class FilterRendererPairT : FilterRendererPair
{
	public Transform transform;

	public FilterRendererPairT()
	{
	}

	public FilterRendererPairT(MeshFilter f, MeshRenderer r, Transform t)
	{
		filter = f;
		renderer = r;
		transform = t;
	}
}
