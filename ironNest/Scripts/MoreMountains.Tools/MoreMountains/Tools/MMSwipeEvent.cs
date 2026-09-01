using UnityEngine;

namespace MoreMountains.Tools
{
	public struct MMSwipeEvent
	{
		public MMPossibleSwipeDirections SwipeDirection;

		public float SwipeAngle;

		public float SwipeLength;

		public Vector2 SwipeOrigin;

		public Vector2 SwipeDestination;

		public float SwipeDuration;

		private static MMSwipeEvent e;

		public MMSwipeEvent(MMPossibleSwipeDirections direction, float angle, float length, Vector2 origin, Vector2 destination, float swipeDuration)
		{
			SwipeDirection = default(MMPossibleSwipeDirections);
			SwipeAngle = 0f;
			SwipeLength = 0f;
			SwipeOrigin = default(Vector2);
			SwipeDestination = default(Vector2);
			SwipeDuration = 0f;
		}

		public static void Trigger(MMPossibleSwipeDirections direction, float angle, float length, Vector2 origin, Vector2 destination, float swipeDuration)
		{
		}
	}
}
