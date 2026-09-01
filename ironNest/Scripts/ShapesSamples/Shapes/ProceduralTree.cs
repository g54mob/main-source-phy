using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shapes
{
	[ExecuteAlways]
	public class ProceduralTree : ImmediateModeShapeDrawer
	{
		[Header("Line Style")]
		[Range(0f, 0.1f)]
		public float lineThickness;

		public Color lineColor;

		[Header("Tree shape")]
		public int seed;

		[Range(1f, 2000f)]
		public int lineCount;

		[Range(0f, 4f)]
		public int branchesMin;

		[Range(1f, 5f)]
		public int branchesMax;

		[Range(0f, 1f)]
		public float branchLengthMin;

		[Range(0f, 1f)]
		public float branchLengthMax;

		[Range(0f, (float)Math.PI)]
		public float maxAngDeviation;

		public bool use3D;

		private int currentLineCount;

		private readonly Queue<Matrix4x4> mtxQueue;

		public override void DrawShapes(Camera cam)
		{
		}

		private void BranchFrom(Matrix4x4 mtx)
		{
		}
	}
}
