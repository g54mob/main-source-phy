using System;
using UnityEngine;

namespace TFBGames
{
	[Serializable]
	public class LodMaterialContainer
	{
		public Material material;

		[HideInInspector]
		public Material customMaterial;

		public Renderer lod0Renderer;

		public Renderer lod1Renderer;

		public Renderer lod2Renderer;

		public int lod0Index;

		public int lod1Index;

		public int lod2Index = -1;
	}
}
