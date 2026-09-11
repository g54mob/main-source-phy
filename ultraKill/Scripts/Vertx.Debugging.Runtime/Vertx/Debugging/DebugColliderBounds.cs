using UnityEngine;

namespace Vertx.Debugging
{
	[AddComponentMenu("Debugging/Debug Collider Bounds")]
	public sealed class DebugColliderBounds : DebugComponentBase
	{
		[SerializeField]
		private Color _color = Shape.CastColor;

		[SerializeField]
		private Collider _collider;

		[SerializeField]
		private Collider2D _collider2D;

		private void Reset()
		{
			_collider = GetComponent<Collider>();
			_collider2D = GetComponent<Collider2D>();
		}

		protected override bool ShouldDraw()
		{
			if (!(_collider != null))
			{
				return _collider2D != null;
			}
			return true;
		}

		protected override void Draw()
		{
			_ = _collider != null;
			_ = _collider2D != null;
		}
	}
}
