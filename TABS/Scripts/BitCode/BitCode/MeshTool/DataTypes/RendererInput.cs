using System;
using UnityEngine;

namespace BitCode.MeshTool.DataTypes
{
	[Serializable]
	public struct RendererInput
	{
		public Renderer Renderer;

		public Transform OverrideBone;

		public bool ReplaceVertColor;

		public Color OverrideColor;
	}
}
