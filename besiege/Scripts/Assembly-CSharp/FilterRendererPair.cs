using System;
using UnityEngine;

[Serializable]
public class FilterRendererPair
{
	public bool active = true;

	public MeshFilter filter;

	public MeshRenderer renderer;

	public FilterRendererPair()
	{
	}

	public FilterRendererPair(MeshFilter f, MeshRenderer r)
	{
		filter = f;
		renderer = r;
	}
}
