using System;
using UnityEngine;

namespace Shapes
{
	[Serializable]
	public struct PolylinePoint
	{
		public Vector3 point;

		[ShapesColorField(true)]
		public Color color;

		public float thickness;

		public static PolylinePoint operator +(PolylinePoint a, PolylinePoint b)
		{
			return default(PolylinePoint);
		}

		public static PolylinePoint operator *(PolylinePoint a, float b)
		{
			return default(PolylinePoint);
		}

		public static PolylinePoint operator *(float b, PolylinePoint a)
		{
			return default(PolylinePoint);
		}

		public static PolylinePoint Lerp(PolylinePoint a, PolylinePoint b, float t)
		{
			return default(PolylinePoint);
		}

		public PolylinePoint(Vector3 point)
		{
			this.point = default(Vector3);
			color = default(Color);
			thickness = 0f;
		}

		public PolylinePoint(Vector2 point)
		{
			this.point = default(Vector3);
			color = default(Color);
			thickness = 0f;
		}

		public PolylinePoint(Vector3 point, Color color)
		{
			this.point = default(Vector3);
			this.color = default(Color);
			thickness = 0f;
		}

		public PolylinePoint(Vector2 point, Color color)
		{
			this.point = default(Vector3);
			this.color = default(Color);
			thickness = 0f;
		}

		public PolylinePoint(Vector3 point, Color color, float thickness)
		{
			this.point = default(Vector3);
			this.color = default(Color);
			this.thickness = 0f;
		}

		public PolylinePoint(Vector2 point, Color color, float thickness)
		{
			this.point = default(Vector3);
			this.color = default(Color);
			this.thickness = 0f;
		}
	}
}
