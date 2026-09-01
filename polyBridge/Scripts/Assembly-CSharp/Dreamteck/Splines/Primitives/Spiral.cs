using UnityEngine;

namespace Dreamteck.Splines.Primitives
{
	public class Spiral : SplinePrimitive
	{
		public float startRadius = 1f;

		public float endRadius = 1f;

		public float stretch = 1f;

		public int iterations = 3;

		public AnimationCurve curve = new AnimationCurve();

		protected override void Generate()
		{
			base.Generate();
			type = Spline.Type.Bezier;
			closed = false;
			CreatePoints(iterations * 4 + 1, SplinePoint.Type.SmoothMirrored);
			float num = Mathf.Abs(endRadius - startRadius) / Mathf.Max(Mathf.Abs(endRadius), Mathf.Abs(startRadius));
			float num2 = 1f;
			if (endRadius > startRadius)
			{
				num2 = -1f;
			}
			float num3 = 0f;
			float num4 = 0f;
			for (int i = 0; i <= iterations * 4; i++)
			{
				float num5 = curve.Evaluate((float)i / (float)(iterations * 4));
				float num6 = Mathf.Lerp(startRadius, endRadius, num5);
				Quaternion quaternion = Quaternion.AngleAxis(num3, Vector3.forward);
				points[i].position = quaternion * Vector3.up / 2f * num6 + Vector3.forward * num4;
				Quaternion identity = Quaternion.identity;
				identity = ((!(num2 > 0f)) ? Quaternion.AngleAxis(Mathf.Lerp(0f, -14.4f, (1f - num5) * num), Vector3.forward) : Quaternion.AngleAxis(Mathf.Lerp(0f, 14.4f, num * num5), Vector3.forward));
				points[i].tangent = points[i].position + (identity * quaternion * Vector3.right * num6 - Vector3.forward * stretch / 4f) * 2f * (Mathf.Sqrt(2f) - 1f) / 3f;
				points[i].tangent2 = points[i].position + (points[i].tangent - points[i].position);
				num4 += stretch / 4f;
				num3 += 90f;
			}
		}
	}
}
