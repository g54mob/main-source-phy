using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy
{
	public static class CurvyGizmoHelper
	{
		public static Matrix4x4 Matrix = Matrix4x4.identity;

		public static void SegmentCurveGizmo(CurvySplineSegment seg, Color col, float stepSize = 0.05f)
		{
			Matrix4x4 matrix = Gizmos.matrix;
			Gizmos.matrix = Matrix;
			Gizmos.color = col;
			if (seg.Spline.Interpolation == CurvyInterpolation.Linear)
			{
				Gizmos.DrawLine(seg.Interpolate(0f), seg.Interpolate(1f));
				return;
			}
			Vector3 vector = seg.Interpolate(0f);
			for (float num = stepSize; num < 1f; num += stepSize)
			{
				Vector3 vector2 = seg.Interpolate(num);
				Gizmos.DrawLine(vector, vector2);
				vector = vector2;
			}
			Gizmos.DrawLine(vector, seg.Interpolate(1f));
			Gizmos.matrix = matrix;
		}

		public static void SegmentApproximationGizmo(CurvySplineSegment seg, Color col)
		{
			Matrix4x4 matrix = Gizmos.matrix;
			Gizmos.matrix = Matrix;
			Gizmos.color = col;
			Vector3 vector = new Vector3(0.1f / seg.Spline.transform.localScale.x, 0.1f / seg.Spline.transform.localScale.y, 0.1f / seg.Spline.transform.localScale.z);
			for (int i = 0; i < seg.Approximation.Length; i++)
			{
				Vector3 vector2 = seg.Approximation[i];
				Gizmos.DrawCube(vector2, DTUtility.GetHandleSize(vector2) * vector);
			}
			Gizmos.matrix = matrix;
		}

		public static void SegmentOrientationAnchorGizmo(CurvySplineSegment seg, Color col)
		{
			if (seg.ApproximationUp.Length != 0)
			{
				Matrix4x4 matrix = Gizmos.matrix;
				Gizmos.matrix = Matrix;
				Gizmos.color = col;
				Vector3 vector = new Vector3(1f / seg.Spline.transform.localScale.x, 1f / seg.Spline.transform.localScale.y, 1f / seg.Spline.transform.localScale.z);
				Vector3 vector2 = seg.ApproximationUp[0];
				vector2.Set(vector2.x * vector.x, vector2.y * vector.y, vector2.z * vector.z);
				Gizmos.DrawRay(seg.Approximation[0], vector2 * CurvyGlobalManager.GizmoOrientationLength * 1.75f);
				Gizmos.matrix = matrix;
			}
		}

		public static void SegmentOrientationGizmo(CurvySplineSegment seg, Color col)
		{
			Matrix4x4 matrix = Gizmos.matrix;
			Gizmos.matrix = Matrix;
			Gizmos.color = col;
			Vector3 vector = new Vector3(1f / seg.Spline.transform.localScale.x, 1f / seg.Spline.transform.localScale.y, 1f / seg.Spline.transform.localScale.z);
			for (int i = 0; i < seg.ApproximationUp.Length; i++)
			{
				Vector3 vector2 = seg.ApproximationUp[i];
				vector2.Set(vector2.x * vector.x, vector2.y * vector.y, vector2.z * vector.z);
				Gizmos.DrawRay(seg.Approximation[i], vector2 * CurvyGlobalManager.GizmoOrientationLength);
			}
			Gizmos.matrix = matrix;
		}

		public static void SegmentTangentGizmo(CurvySplineSegment seg, Color col)
		{
			Matrix4x4 matrix = Gizmos.matrix;
			Gizmos.matrix = Matrix;
			Gizmos.color = col;
			for (int i = 0; i < seg.ApproximationT.Length; i++)
			{
				Gizmos.color = ((i == seg.CacheSize) ? Color.black : ((i == 0) ? Color.blue : col));
				Vector3 vector = seg.Approximation[i];
				Vector3 normalized = seg.ApproximationT[i].normalized;
				Gizmos.DrawRay(vector, normalized * CurvyGlobalManager.GizmoOrientationLength);
			}
			Gizmos.matrix = matrix;
		}

		public static void ControlPointGizmo(CurvySplineSegment cp, bool selected, Color col)
		{
			Matrix4x4 matrix = Gizmos.matrix;
			Gizmos.matrix = Matrix4x4.identity;
			Gizmos.color = col;
			Vector3 vector = Matrix.MultiplyPoint(cp.transform.localPosition);
			float num = (selected ? 1f : 0.7f);
			if ((bool)cp.Spline && cp.Spline.RestrictTo2D)
			{
				Gizmos.DrawCube(vector, Vector3.one * DTUtility.GetHandleSize(vector) * num * CurvyGlobalManager.GizmoControlPointSize);
			}
			else
			{
				Gizmos.DrawSphere(vector, DTUtility.GetHandleSize(vector) * num * CurvyGlobalManager.GizmoControlPointSize);
			}
			Gizmos.matrix = matrix;
		}

		public static void BoundsGizmo(CurvySplineSegment cp, Color col)
		{
			Matrix4x4 matrix = Gizmos.matrix;
			Gizmos.matrix = Matrix;
			Gizmos.color = col;
			Gizmos.DrawWireCube(cp.Bounds.center, cp.Bounds.size);
			Gizmos.matrix = matrix;
		}
	}
}
