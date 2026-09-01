using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Object Bounds/MMObjectBounds")]
	public class MMObjectBounds : MonoBehaviour
	{
		public enum WaysToDetermineBounds
		{
			Collider = 0,
			Collider2D = 1,
			Renderer = 2,
			Undefined = 3
		}

		[Header("Bounds")]
		public WaysToDetermineBounds BoundsBasedOn;

		public virtual Vector3 Size { get; set; }

		protected virtual void Reset()
		{
		}

		protected virtual void DefineBoundsChoice()
		{
		}

		public virtual Bounds GetBounds()
		{
			return default(Bounds);
		}
	}
}
