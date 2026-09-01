using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Shapes
{
	public class PolylinePath : PointPath<PolylinePoint>
	{
		private const MethodImplOptions INLINE = MethodImplOptions.AggressiveInlining;

		private bool lastUsedClosed;

		private PolylineJoins lastUsedJoins;

		public void SetPoint(int index, Vector3 point)
		{
		}

		public void SetPoint(int index, Vector2 point)
		{
		}

		public void SetColor(int index, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoint(float x, float y)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoint(float x, float y, float z)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoint(float x, float y, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoint(float x, float y, float z, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoint(Vector3 pos)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoint(Vector3 pos, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoint(Vector3 pos, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoint(Vector3 pos, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoint(Vector2 pos)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoint(Vector2 pos, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoint(Vector2 pos, float thickness)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoint(Vector2 pos, float thickness, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoints(IEnumerable<Vector3> pts)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoints(params Vector3[] pts)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoints(IEnumerable<Vector2> pts)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoints(params Vector2[] pts)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoints(IEnumerable<Vector3> pts, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoints(IEnumerable<Vector2> pts, Color color)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoints(IEnumerable<Vector3> pts, IEnumerable<Color> colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoints(IEnumerable<Vector2> pts, IEnumerable<Color> colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoints(IEnumerable<Vector3> pts, IEnumerable<float> thicknesses)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoints(IEnumerable<Vector2> pts, IEnumerable<float> thicknesses)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoints(IEnumerable<Vector3> pts, IEnumerable<float> thicknesses, IEnumerable<Color> colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoints(IEnumerable<Vector2> pts, IEnumerable<float> thicknesses, IEnumerable<Color> colors)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void BezierTo(Vector3 startTangent, Vector3 endTangent, Vector3 end)
		{
		}

		public void BezierTo(Vector3 startTangent, Vector3 endTangent, Vector3 end, float pointsPerTurn)
		{
		}

		public void BezierTo(Vector3 startTangent, Vector3 endTangent, Vector3 end, int pointCount)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void BezierTo(Vector3 startTangent, Vector3 endTangent, PolylinePoint end)
		{
		}

		public void BezierTo(Vector3 startTangent, Vector3 endTangent, PolylinePoint end, float pointsPerTurn)
		{
		}

		public void BezierTo(Vector3 startTangent, Vector3 endTangent, PolylinePoint end, int pointCount)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void BezierTo(PolylinePoint startTangent, PolylinePoint endTangent, PolylinePoint end)
		{
		}

		public void BezierTo(PolylinePoint startTangent, PolylinePoint endTangent, PolylinePoint end, float pointsPerTurn)
		{
		}

		public void BezierTo(PolylinePoint startTangent, PolylinePoint endTangent, PolylinePoint end, int pointCount)
		{
		}

		private static int CalcBezierPointCount(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float pointsPerTurn)
		{
			return 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ArcTo(Vector3 corner, Vector3 next, float radius, int pointCount)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ArcTo(Vector3 corner, PolylinePoint next, float radius, int pointCount)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ArcTo(Vector3 corner, Vector3 next, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ArcTo(Vector3 corner, PolylinePoint next, float radius)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ArcTo(Vector3 corner, Vector3 next, float radius, float pointsPerTurn)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ArcTo(Vector3 corner, PolylinePoint next, float radius, float pointsPerTurn)
		{
		}

		private void AddArcPoints(Vector3 corner, Vector3 next, float radius, bool useDensity, int targetPointCount, float pointsPerTurn)
		{
		}

		private void AddArcPoints(Vector3 corner, PolylinePoint next, float radius, bool useDensity, int targetPointCount, float pointsPerTurn)
		{
		}

		public bool EnsureMeshIsReadyToRender(bool closed, PolylineJoins renderJoins, out Mesh outMesh)
		{
			outMesh = null;
			return false;
		}

		private void TryUpdateMesh(bool closed, PolylineJoins joins)
		{
		}

		[Obsolete("This function no longer exists - either use the overload without a color, where the color will match the previous point, or the one with a PolylinePoint endpoint, where the color will blend between previous point and the target point", true)]
		public void ArcTo(Vector3 corner, Vector3 next, float radius, int pointCount, Color color)
		{
		}

		[Obsolete("This function no longer exists - either use the overload without a color, where the color will match the previous point, or the one with a PolylinePoint endpoint, where the color will blend between previous point and the target point", true)]
		public void ArcTo(Vector3 corner, Vector3 next, float radius, Color color)
		{
		}

		[Obsolete("This function no longer exists - either use the overload without a color, where the color will match the previous point, or the one with a PolylinePoint endpoint, where the color will blend between previous point and the target point", true)]
		public void ArcTo(Vector3 corner, Vector3 next, float radius, float pointsPerTurn, Color color)
		{
		}

		[Obsolete("This function no longer exists - either use the overload without a color, where the color will match the previous point, or the one with a PolylinePoint endpoint, where the color will blend between previous point and the target point", true)]
		public void BezierTo(Vector3 startTangent, Vector3 endTangent, Vector3 end, float pointsPerTurn, Color color)
		{
		}

		[Obsolete("This function no longer exists - either use the overload without a color, where the color will match the previous point, or the one with a PolylinePoint endpoint, where the color will blend between previous point and the target point", true)]
		public void BezierTo(Vector3 startTangent, Vector3 endTangent, Vector3 end, int pointCount, Color color)
		{
		}

		[Obsolete("This function no longer exists - either use the overload without a color, where the color will match the previous point, or the one with a PolylinePoint endpoint, where the color will blend between previous point and the target point", true)]
		public void BezierTo(Vector3 startTangent, Vector3 endTangent, Vector3 end, Color color)
		{
		}
	}
}
