using System;
using UnityEngine;

namespace TexelDensityTools
{
	[Serializable]
	public struct MaterialTerrainLink
	{
		public Material Material;

		public Material[] Materials;

		public Terrain Terrain;
	}
}
