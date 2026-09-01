using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Shapes
{
	[ExecuteAlways]
	[AddComponentMenu("Shapes/Polygon")]
	public class Polygon : ShapeRenderer, IFillable
	{
		[FormerlySerializedAs("polyPoints")]
		[SerializeField]
		public List<Vector2> points;

		[SerializeField]
		private PolygonTriangulation triangulation;

		[SerializeField]
		private protected GradientFill fill;

		[SerializeField]
		private protected bool useFill;

		public PolygonTriangulation Triangulation
		{
			get
			{
				return default(PolygonTriangulation);
			}
			set
			{
			}
		}

		public int Count => 0;

		public Vector2 this[int i]
		{
			get
			{
				return default(Vector2);
			}
			set
			{
			}
		}

		private protected override bool UseCamOnPreCull => false;

		internal override bool HasScaleModes => false;

		internal override bool HasDetailLevels => false;

		private protected override MeshUpdateMode MeshUpdateMode => default(MeshUpdateMode);

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

		public void SetPointPosition(int index, Vector2 position)
		{
		}

		public void SetPoints(IEnumerable<Vector2> points)
		{
		}

		public void AddPoints(IEnumerable<Vector2> points)
		{
		}

		public void AddPoint(Vector2 point)
		{
		}

		internal override void CamOnPreCull()
		{
		}

		private protected override void SetAllMaterialProperties()
		{
		}

		private protected override void GetMaterials(Material[] mats)
		{
		}

		private protected override void GenerateMesh()
		{
		}

		private protected override Bounds GetUnpaddedLocalBounds_Internal()
		{
			return default(Bounds);
		}

		private void SetFillProperties()
		{
		}
	}
}
