using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Shapes
{
	[ExecuteAlways]
	[AddComponentMenu("Shapes/RegularPolygon")]
	public class RegularPolygon : ShapeRenderer, IDashable, IFillable
	{
		[FormerlySerializedAs("hollow")]
		[SerializeField]
		private bool border;

		[SerializeField]
		private int sides;

		[SerializeField]
		[Range(0f, 1f)]
		private float roundness;

		[SerializeField]
		private float angle;

		[SerializeField]
		private float radius;

		[SerializeField]
		private AngularUnit angUnitInput;

		[SerializeField]
		private RegularPolygonGeometry geometry;

		[SerializeField]
		private ThicknessSpace radiusSpace;

		[SerializeField]
		private float thickness;

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

		public bool Border
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		[Obsolete("Please use RegularPolygon.Border instead", true)]
		public bool Hollow
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public int Sides
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public float Roundness
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float Angle
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

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

		public RegularPolygonGeometry Geometry
		{
			get
			{
				return default(RegularPolygonGeometry);
			}
			set
			{
			}
		}

		public ThicknessSpace RadiusSpace
		{
			get
			{
				return default(ThicknessSpace);
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
