using UnityEngine;

namespace Landfall.TABS
{
	[RequireComponent(typeof(UIMovementAnimation))]
	public class MovingUIElement : MonoBehaviour
	{
		private UIMovementAnimation movementAnimation;

		private void Awake()
		{
			movementAnimation = GetComponent<UIMovementAnimation>();
		}

		public void Show()
		{
			if (movementAnimation != null)
			{
				movementAnimation.SetState(UIMovementAnimation.State.State01);
			}
		}

		public void Hide()
		{
			if (movementAnimation != null)
			{
				movementAnimation.SetState(UIMovementAnimation.State.State02);
			}
		}
	}
}
