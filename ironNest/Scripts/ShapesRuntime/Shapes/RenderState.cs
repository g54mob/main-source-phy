using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Shapes
{
	internal struct RenderState : IEquatable<RenderState>
	{
		public Shader shader;

		public string[] keywords;

		public bool isTextMaterial;

		public CompareFunction zTest;

		public float zOffsetFactor;

		public int zOffsetUnits;

		public ColorWriteMask colorMask;

		public CompareFunction stencilComp;

		public StencilOp stencilOpPass;

		public byte stencilRefID;

		public byte stencilReadMask;

		public byte stencilWriteMask;

		public Material CreateMaterial()
		{
			return null;
		}

		private static bool StrArrEquals(string[] a, string[] b)
		{
			return false;
		}

		public bool Equals(RenderState other)
		{
			return false;
		}

		public override bool Equals(object obj)
		{
			return false;
		}

		public override int GetHashCode()
		{
			return 0;
		}
	}
}
