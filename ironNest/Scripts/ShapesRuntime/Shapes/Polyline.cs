using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Shapes
{
	[ExecuteAlways]
	[AddComponentMenu("Shapes/Polyline")]
	public class Polyline : ShapeRenderer
	{
		[FormerlySerializedAs("polyPoints")]
		[SerializeField]
		public List<PolylinePoint> points;

		[SerializeField]
		private PolylineGeometry geometry;

		[SerializeField]
		private PolylineJoins joins;

		[SerializeField]
		private bool closed;

		[SerializeField]
		private float thickness;

		[SerializeField]
		private ThicknessSpace thicknessSpace;

		public PolylineGeometry Geometry
		{
			get
			{
				return default(PolylineGeometry);
			}
			set
			{
			}
		}

		public PolylineJoins Joins
		{
			get
			{
				return default(PolylineJoins);
			}
			set
			{
			}
		}

		public bool Closed
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

		public int Count => 0;

		public PolylinePoint this[int i]
		{
			get
			{
				return default(PolylinePoint);
			}
			set
			{
			}
		}

		private protected override bool UseCamOnPreCull => false;

		private protected override MeshUpdateMode MeshUpdateMode => default(MeshUpdateMode);

		private protected override int MaterialCount => 0;

		public void SetPointPosition(int index, Vector3 position)
		{
		}

		public void SetPointColor(int index, Color color)
		{
		}

		public void SetPointThickness(int index, float thickness)
		{
		}

		public void SetPoints(IReadOnlyCollection<Vector3> points, IReadOnlyCollection<Color> colors = null)
		{
		}

		public void SetPoints(IReadOnlyCollection<Vector2> points, IReadOnlyCollection<Color> colors = null)
		{
		}

		public void SetPoints(IEnumerable<PolylinePoint> points)
		{
		}

		public void AddPoints(IEnumerable<PolylinePoint> points)
		{
		}

		public void AddPoint(Vector3 position)
		{
		}

		public void AddPoint(Vector3 position, Color color)
		{
		}

		public void AddPoint(Vector3 position, Color color, float thickness)
		{
		}

		public void AddPoint(Vector3 position, float thickness)
		{
		}

		public void AddPoint(PolylinePoint point)
		{
		}

		internal override void CamOnPreCull()
		{
		}

		private protected override void GenerateMesh()
		{
		}

		private protected override void SetAllMaterialProperties()
		{
		}

		private protected override void ShapeClampRanges()
		{
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
