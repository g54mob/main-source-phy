using System;
using UnityEngine;

namespace Shapes
{
	[ExecuteAlways]
	[AddComponentMenu("Shapes/Rectangle")]
	public class Rectangle : ShapeRenderer, IDashable, IFillable
	{
		public enum RectangleType
		{
			HardSolid = 0,
			RoundedSolid = 1,
			HardBorder = 2,
			RoundedBorder = 3
		}

		public enum RectangleCornerRadiusMode
		{
			Uniform = 0,
			PerCorner = 1
		}

		[SerializeField]
		private RectPivot pivot;

		[SerializeField]
		private float width;

		[SerializeField]
		private float height;

		[SerializeField]
		private RectangleType type;

		[SerializeField]
		private RectangleCornerRadiusMode cornerRadiusMode;

		[SerializeField]
		private Vector4 cornerRadii;

		[Tooltip("The thickness of the rectangle, in the given thickness space")]
		[SerializeField]
		private float thickness;

		[Tooltip("The space in which thickness is defined")]
		[SerializeField]
		private ThicknessSpace thicknessSpace;

		[SerializeField]
		private bool matchDashSpacingToSize;

		[SerializeField]
		private bool dashed;

		[SerializeField]
		private DashStyle dashStyle;

		[SerializeField]
		private protected GradientFill fill;

		[SerializeField]
		private protected bool useFill;

		public bool IsBorder => false;

		[Obsolete("Please use IsBorder instead", true)]
		public bool IsHollow => false;

		public bool IsRounded => false;

		public RectPivot Pivot
		{
			get
			{
				return default(RectPivot);
			}
			set
			{
			}
		}

		public float Width
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float Height
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public RectangleType Type
		{
			get
			{
				return default(RectangleType);
			}
			set
			{
			}
		}

		public RectangleCornerRadiusMode CornerRadiusMode
		{
			get
			{
				return default(RectangleCornerRadiusMode);
			}
			set
			{
			}
		}

		[Obsolete("Radius is deprecated, please use CornerRadius instead", true)]
		public float Radius
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float CornerRadius
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public Vector4 CornerRadii
		{
			get
			{
				return default(Vector4);
			}
			set
			{
			}
		}

		[Obsolete("Please use CornerRadii instead because I did a typo~", true)]
		public Vector4 CornerRadiii
		{
			get
			{
				return default(Vector4);
			}
			set
			{
			}
		}

		public float Thickness
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public ThicknessSpace ThicknessSpace
		{
			get
			{
				return default(ThicknessSpace);
			}
			set
			{
			}
		}

		internal override bool HasDetailLevels => false;

		public bool MatchDashSpacingToSize
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public bool Dashed
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public float DashSize
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float DashSpacing
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float DashOffset
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public DashSpace DashSpace
		{
			get
			{
				return default(DashSpace);
			}
			set
			{
			}
		}

		public DashSnapping DashSnap
		{
			get
			{
				return default(DashSnapping);
			}
			set
			{
			}
		}

		public DashType DashType
		{
			get
			{
				return default(DashType);
			}
			set
			{
			}
		}

		public float DashShapeModifier
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public GradientFill Fill
		{
			get
			{
				return default(GradientFill);
			}
			set
			{
			}
		}

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

		private void UpdateRectPositioningNow()
		{
		}

		private void UpdateRectPositioning()
		{
		}

		private Vector4 GetPositioningRect()
		{
			return default(Vector4);
		}

		private protected override void SetAllMaterialProperties()
		{
		}

		private protected override void GetMaterials(Material[] mats)
		{
		}

		private protected override Bounds GetUnpaddedLocalBounds_Internal()
		{
			return default(Bounds);
		}

		private void SetAllDashValues(bool now)
		{
		}

		private float GetNetDashSpacing()
		{
			return 0f;
		}

		private void SetFillProperties()
		{
		}
	}
}
