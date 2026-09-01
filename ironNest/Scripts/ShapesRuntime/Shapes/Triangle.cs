using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Shapes
{
	[ExecuteAlways]
	[AddComponentMenu("Shapes/Triangle")]
	public class Triangle : ShapeRenderer, IDashable
	{
		public enum TriangleColorMode
		{
			Single = 0,
			PerCorner = 1
		}

		[SerializeField]
		private TriangleColorMode colorMode;

		[SerializeField]
		private Vector3 a;

		[SerializeField]
		private Vector3 b;

		[SerializeField]
		private Vector3 c;

		[FormerlySerializedAs("hollow")]
		[SerializeField]
		private bool border;

		[SerializeField]
		private float thickness;

		[SerializeField]
		private ThicknessSpace thicknessSpace;

		[SerializeField]
		[Range(0f, 1f)]
		private float roundness;

		[SerializeField]
		[ShapesColorField(true)]
		private Color colorB;

		[SerializeField]
		[ShapesColorField(true)]
		private Color colorC;

		[SerializeField]
		private bool matchDashSpacingToSize;

		[SerializeField]
		private bool dashed;

		[SerializeField]
		private DashStyle dashStyle;

		public Vector3 this[int index]
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		public TriangleColorMode ColorMode
		{
			get
			{
				return default(TriangleColorMode);
			}
			set
			{
			}
		}

		public Vector3 A
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		public Vector3 B
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		public Vector3 C
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

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

		[Obsolete("Please use Triangle.Border instead", true)]
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

		public Color ColorA
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public Color ColorB
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public Color ColorC
		{
			get
			{
				return default(Color);
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

		public Vector3 GetTriangleVertex(int index)
		{
			return default(Vector3);
		}

		public Vector3 SetTriangleVertex(int index, Vector3 value)
		{
			return default(Vector3);
		}

		public Color GetTriangleColor(int index)
		{
			return default(Color);
		}

		public void SetTriangleColor(int index, Color color)
		{
		}

		private protected override void SetAllMaterialProperties()
		{
		}

		private protected override Mesh GetInitialMeshAsset()
		{
			return null;
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
