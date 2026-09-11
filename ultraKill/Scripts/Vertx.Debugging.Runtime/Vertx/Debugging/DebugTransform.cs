using UnityEngine;

namespace Vertx.Debugging
{
	[AddComponentMenu("Debugging/Debug Transform")]
	public sealed class DebugTransform : DebugComponentBase
	{
		[SerializeField]
		private float _scale = 1f;

		[SerializeField]
		private Shape.Axes _axes = Shape.Axes.All;

		protected override bool ShouldDraw()
		{
			return true;
		}

		protected override void Draw()
		{
			_ = base.transform;
		}
	}
}
