using System;

namespace MagicaCloth2
{
	[Serializable]
	public class ClothDebugSettings
	{
		public enum DebugAxis
		{
			None = 0,
			Normal = 1,
			All = 2
		}

		public bool enable;

		public bool ztest;

		public bool position = true;

		public DebugAxis axis;

		public bool shape;

		public bool baseLine;

		public bool depth;

		public bool collider = true;

		public bool animatedPosition;

		public DebugAxis animatedAxis;

		public bool animatedShape;

		public bool inertiaCenter = true;

		public bool customSkinningBone = true;

		public bool CheckParticleDrawing(int index)
		{
			return true;
		}

		public bool CheckTriangleDrawing(int index)
		{
			return true;
		}

		public bool CheckRadiusDrawing()
		{
			return true;
		}

		public float GetPointSize()
		{
			return 0.01f;
		}

		public float GetLineSize()
		{
			return 0.05f;
		}

		public float GetInertiaCenterRadius()
		{
			return 0.01f;
		}

		public float GetCustomSkinningRadius()
		{
			return 0.02f;
		}

		public bool IsReferOldPos()
		{
			return false;
		}
	}
}
