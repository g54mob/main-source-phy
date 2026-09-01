using UnityEngine;

namespace Shapes
{
	[ExecuteAlways]
	[AddComponentMenu("Shapes/Quad")]
	public class Quad : ShapeRenderer
	{
		public enum QuadColorMode
		{
			Single = 0,
			Horizontal = 1,
			Vertical = 2,
			PerCorner = 3
		}

		[SerializeField]
		private QuadColorMode colorMode;

		[SerializeField]
		private Vector3 a;

		[SerializeField]
		private Vector3 b;

		[SerializeField]
		private Vector3 c;

		[SerializeField]
		private Vector3 d;

		[SerializeField]
		private bool autoSetD;

		[SerializeField]
		[ShapesColorField(true)]
		private Color colorB;

		[SerializeField]
		[ShapesColorField(true)]
		private Color colorC;

		[SerializeField]
		[ShapesColorField(true)]
		private Color colorD;

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

		public QuadColorMode ColorMode
		{
			get
			{
				return default(QuadColorMode);
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

		public Vector3 D
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		public bool IsUsingAutoD
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public Vector3 DAuto => default(Vector3);

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

		public Color ColorLeft
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public Color ColorTop
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public Color ColorRight
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public Color ColorBottom
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

		public Color ColorD
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

		internal override bool HasScaleModes => false;

		public Vector3 GetQuadVertex(int index)
		{
			return default(Vector3);
		}

		public Vector3 SetQuadVertex(int index, Vector3 value)
		{
			return default(Vector3);
		}

		public Color GetQuadColor(int index)
		{
			return default(Color);
		}

		public void SetQuadColor(int index, Color color)
		{
		}

		private void AutoSetD()
		{
		}

		private void CheckAutoSetD()
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
	}
}
