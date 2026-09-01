using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMControlsTestInputManager : MonoBehaviour, MMEventListener<MMSwipeEvent>, MMEventListenerBase
	{
		protected virtual void Start()
		{
		}

		public virtual void LeftJoystickMovement(Vector2 movement)
		{
		}

		public virtual void RightJoystickMovement(Vector2 movement)
		{
		}

		public virtual void RepositionableJoystickMovement(Vector2 movement)
		{
		}

		public virtual void FollowerJoystickMovement(Vector2 movement)
		{
		}

		public virtual void APressed()
		{
		}

		public virtual void BPressed()
		{
		}

		public virtual void XPressed()
		{
		}

		public virtual void YPressed()
		{
		}

		public virtual void RTPressed()
		{
		}

		public virtual void APressedFirstTime()
		{
		}

		public virtual void BPressedFirstTime()
		{
		}

		public virtual void XPressedFirstTime()
		{
		}

		public virtual void YPressedFirstTime()
		{
		}

		public virtual void RTPressedFirstTime()
		{
		}

		public virtual void AReleased()
		{
		}

		public virtual void BReleased()
		{
		}

		public virtual void XReleased()
		{
		}

		public virtual void YReleased()
		{
		}

		public virtual void RTReleased()
		{
		}

		public virtual void HorizontalAxisPressed(float value)
		{
		}

		public virtual void VerticalAxisPressed(float value)
		{
		}

		public virtual void LeftPressedFirstTime()
		{
		}

		public virtual void UpPressedFirstTime()
		{
		}

		public virtual void DownPressedFirstTime()
		{
		}

		public virtual void RightPressedFirstTime()
		{
		}

		public virtual void LeftReleased()
		{
		}

		public virtual void UpReleased()
		{
		}

		public virtual void DownReleased()
		{
		}

		public virtual void RightReleased()
		{
		}

		public virtual void StickDragged()
		{
		}

		public virtual void StickPointerUp()
		{
		}

		public virtual void StickPointerDown()
		{
		}

		public virtual void OnMMEvent(MMSwipeEvent swipeEvent)
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}
	}
}
