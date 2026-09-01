using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMUIFollowMouse : MonoBehaviour
	{
		protected Vector2 _newPosition;

		protected Vector2 _mousePosition;

		public virtual Canvas TargetCanvas { get; set; }

		protected virtual void LateUpdate()
		{
		}
	}
}
