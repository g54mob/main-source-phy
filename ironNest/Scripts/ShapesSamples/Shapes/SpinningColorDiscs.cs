using UnityEngine;

namespace Shapes
{
	[ExecuteAlways]
	public class SpinningColorDiscs : ImmediateModeShapeDrawer
	{
		[Range(3f, 32f)]
		public int discCount;

		[Range(0f, 1f)]
		public float discRadius;

		public override void DrawShapes(Camera cam)
		{
		}

		private Vector2 GetDiscPosition(float t)
		{
			return default(Vector2);
		}
	}
}
