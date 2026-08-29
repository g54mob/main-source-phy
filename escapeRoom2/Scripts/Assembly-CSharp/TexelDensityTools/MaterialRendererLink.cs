using System;
using UnityEngine;

namespace TexelDensityTools
{
	[Serializable]
	public struct MaterialRendererLink
	{
		public Material Material;

		public Material[] Materials;

		public MeshRenderer Renderer;
	}
}
