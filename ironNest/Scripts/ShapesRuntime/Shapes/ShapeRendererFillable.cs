using System;
using UnityEngine;

namespace Shapes
{
	[Obsolete("Shapes now use the IFillable interface instead of inheriting from ShapeRendererFillable", true)]
	public abstract class ShapeRendererFillable : ShapeRenderer
	{
		private const string OBSOLETE = "Shapes now use the IFillable interface instead of inheriting from ShapeRendererFillable";

		[Obsolete("Shapes now use the IFillable interface instead of inheriting from ShapeRendererFillable", true)]
		private protected GradientFill fill;

		[Obsolete("Shapes now use the IFillable interface instead of inheriting from ShapeRendererFillable", true)]
		private protected bool useFill;

		[Obsolete("Shapes now use the IFillable interface instead of inheriting from ShapeRendererFillable", true)]
		private int FillTypeShaderInt => 0;

		[Obsolete("Shapes now use the IFillable interface instead of inheriting from ShapeRendererFillable", true)]
		public bool UseFill
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		[Obsolete("Shapes now use the IFillable interface instead of inheriting from ShapeRendererFillable", true)]
		public FillType FillType
		{
			get
			{
				return default(FillType);
			}
			set
			{
			}
		}

		[Obsolete("Shapes now use the IFillable interface instead of inheriting from ShapeRendererFillable", true)]
		public FillSpace FillSpace
		{
			get
			{
				return default(FillSpace);
			}
			set
			{
			}
		}

		[Obsolete("Shapes now use the IFillable interface instead of inheriting from ShapeRendererFillable", true)]
		public Vector3 FillRadialOrigin
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		[Obsolete("Shapes now use the IFillable interface instead of inheriting from ShapeRendererFillable", true)]
		public float FillRadialRadius
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[Obsolete("Shapes now use the IFillable interface instead of inheriting from ShapeRendererFillable", true)]
		public Vector3 FillLinearStart
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		[Obsolete("Shapes now use the IFillable interface instead of inheriting from ShapeRendererFillable", true)]
		public Vector3 FillLinearEnd
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		[Obsolete("Shapes now use the IFillable interface instead of inheriting from ShapeRendererFillable", true)]
		public Color FillColorStart
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		[Obsolete("Shapes now use the IFillable interface instead of inheriting from ShapeRendererFillable", true)]
		public Color FillColorEnd
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		[Obsolete("Shapes now use the IFillable interface instead of inheriting from ShapeRendererFillable", true)]
		private protected void SetFillProperties()
		{
		}
	}
}
