using UnityEngine;

namespace Shapes
{
	[ExecuteAlways]
	[AddComponentMenu("Shapes/Line")]
	public class Line : ShapeRenderer, IDashable
	{
		public enum LineColorMode
		{
			Single = 0,
			Double = 1
		}

		[SerializeField]
		private LineGeometry geometry;

		[SerializeField]
		private LineColorMode colorMode;

		[SerializeField]
		[ShapesColorField(true)]
		private Color colorEnd;

		[SerializeField]
		private Vector3 start;

		[SerializeField]
		private Vector3 end;

		[SerializeField]
		private float thickness;

		[SerializeField]
		private ThicknessSpace thicknessSpace;

		[SerializeField]
		private LineEndCap endCaps;

		[SerializeField]
		private bool matchDashSpacingToSize;

		[SerializeField]
		private bool dashed;

		[SerializeField]
		private DashStyle dashStyle;

		public Vector3 this[int i]
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		public LineGeometry Geometry
		{
			get
			{
				return default(LineGeometry);
			}
			set
			{
			}
		}

		public LineColorMode ColorMode
		{
			get
			{
				return default(LineColorMode);
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

		public Vector3 Start
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		public Vector3 End
		{
			get
			{
				return default(Vector3);
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

		public LineEndCap EndCaps
		{
			get
			{
				return default(LineEndCap);
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

		private protected override Bounds GetUnpaddedLocalBounds_Internal()
		{
			return default(Bounds);
		}

		private protected override void GetMaterials(Material[] mats)
		{
		}

		private protected override Mesh GetInitialMeshAsset()
		{
			return null;
		}

		private protected override void ShapeClampRanges()
		{
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
