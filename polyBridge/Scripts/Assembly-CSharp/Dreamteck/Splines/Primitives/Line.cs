using UnityEngine;

namespace Dreamteck.Splines.Primitives
{
	public class Line : SplinePrimitive
	{
		public bool mirror = true;

		public float length = 1f;

		public int segments = 1;

		protected override void Generate()
		{
			base.Generate();
			type = Spline.Type.Linear;
			closed = false;
			CreatePoints(segments + 1, SplinePoint.Type.SmoothMirrored);
			Vector3 vector = Quaternion.Euler(rotation) * Vector3.forward;
			Vector3 vector2 = Vector3.zero;
			if (mirror)
			{
				vector2 = -vector * length * 0.5f;
			}
			for (int i = 0; i < points.Length; i++)
			{
				points[i].position = vector2 + vector * length * ((float)i / (float)(points.Length - 1));
			}
		}
	}
}
