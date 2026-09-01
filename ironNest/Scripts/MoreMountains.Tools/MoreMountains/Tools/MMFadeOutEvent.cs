using UnityEngine;

namespace MoreMountains.Tools
{
	public struct MMFadeOutEvent
	{
		public int ID;

		public float Duration;

		public MMTweenType Curve;

		public bool IgnoreTimeScale;

		public Vector3 WorldPosition;

		private static MMFadeOutEvent e;

		public MMFadeOutEvent(float duration, MMTweenType tween, int id = 0, bool ignoreTimeScale = true, Vector3 worldPosition = default(Vector3))
		{
			ID = 0;
			Duration = 0f;
			Curve = null;
			IgnoreTimeScale = false;
			WorldPosition = default(Vector3);
		}

		public static void Trigger(float duration, MMTweenType tween, int id = 0, bool ignoreTimeScale = true, Vector3 worldPosition = default(Vector3))
		{
		}
	}
}
