using System;
using UnityEngine;

namespace Shapes
{
	[Serializable]
	public struct GradientFill
	{
		internal const int FILL_NONE = -1;

		public static readonly GradientFill defaultFill;

		public FillType type;

		public FillSpace space;

		[ShapesColorField(true)]
		public Color colorStart;

		[ShapesColorField(true)]
		public Color colorEnd;

		public Vector3 linearStart;

		public Vector3 linearEnd;

		public Vector3 radialOrigin;

		public float radialRadius;

		public static GradientFill Linear(Vector3 start, Vector3 end, Color colorStart, Color colorEnd, FillSpace space = FillSpace.Local)
		{
			return default(GradientFill);
		}

		public static GradientFill Radial(Vector3 origin, float radius, Color colorInner, Color colorOuter, FillSpace space = FillSpace.Local)
		{
			return default(GradientFill);
		}

		internal Vector4 GetShaderStartVector()
		{
			return default(Vector4);
		}

		internal int GetShaderFillTypeInt(bool use)
		{
			return 0;
		}

		[Obsolete("Use GradientFill.Linear instead", true)]
		public static GradientFill CreateLinear(Vector3 start, Vector3 end, Color colorStart, Color colorEnd, FillSpace space)
		{
			return default(GradientFill);
		}

		[Obsolete("Use GradientFill.Radial instead", true)]
		public static GradientFill CreateRadial(Vector3 origin, float radius, Color colorInner, Color colorOuter, FillSpace space)
		{
			return default(GradientFill);
		}
	}
}
