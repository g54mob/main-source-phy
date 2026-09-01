using System;
using UnityEngine;

namespace Shapes
{
	[ExecuteAlways]
	[AddComponentMenu("Shapes/Disc")]
	public class Disc : ShapeRenderer, IDashable
	{
		public enum DiscColorMode
		{
			Single = 0,
			Radial = 1,
			Angular = 2,
			Bilinear = 3
		}

		[SerializeField]
		private DiscType type;

		[SerializeField]
		private DiscColorMode colorMode;

		[SerializeField]
		[ShapesColorField(true)]
		private Color colorOuterStart;

		[SerializeField]
		[ShapesColorField(true)]
		private Color colorInnerEnd;

		[SerializeField]
		[ShapesColorField(true)]
		private Color colorOuterEnd;

		[SerializeField]
		private DiscGeometry geometry;

		[SerializeField]
		private AngularUnit angUnitInput;

		[SerializeField]
		private float angRadiansStart;

		[SerializeField]
		private float angRadiansEnd;

		[SerializeField]
		private float radius;

		[SerializeField]
		private ThicknessSpace radiusSpace;

		[SerializeField]
		private float thickness;

		[SerializeField]
		private ThicknessSpace thicknessSpace;

		[SerializeField]
		private ArcEndCap arcEndCaps;

		[SerializeField]
		private bool matchDashSpacingToSize;

		[SerializeField]
		private bool dashed;

		[SerializeField]
		private DashStyle dashStyle;

		public bool HasThickness => false;

		public bool HasSector => false;

		public DiscType Type
		{
			get
			{
				return default(DiscType);
			}
			set
			{
			}
		}

		public DiscColorMode ColorMode
		{
			get
			{
				return default(DiscColorMode);
			}
			set
			{
			}
		}

		public override Color Color
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public Color ColorInnerStart
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public Color ColorOuterStart
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public Color ColorInnerEnd
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public Color ColorOuterEnd
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public Color ColorOuter
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public Color ColorInner
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public Color ColorStart
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public Color ColorEnd
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public DiscGeometry Geometry
		{
			get
			{
				return default(DiscGeometry);
			}
			set
			{
			}
		}

		public float AngRadiansStart
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float AngRadiansEnd
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

		[Obsolete("this property is obsolete, this was a typo! please use Thickness instead!", true)]
		public float RadiusInner
		{
			get
			{
				return 0f;
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

		public ArcEndCap ArcEndCaps
		{
			get
			{
				return default(ArcEndCap);
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
	}
}
