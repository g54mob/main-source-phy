using UnityEngine;

namespace MoreMountains.Tools
{
	public struct MMFadeEvent
	{
		public int ID;

		public float Duration;

		public float TargetAlpha;

		public MMTweenType Curve;

		public bool IgnoreTimeScale;

		public Vector3 WorldPosition;

		private static MMFadeEvent e;

		public MMFadeEvent(float duration, float targetAlpha, MMTweenType tween, int id = 0, bool ignoreTimeScale = true, Vector3 worldPosition = default(Vector3))
		{
			ID = 0;
			Duration = 0f;
			TargetAlpha = 0f;
			Curve = null;
			IgnoreTimeScale = false;
			WorldPosition = default(Vector3);
		}

		public static void Trigger(float duration, float targetAlpha)
		{
		}

		public static void Trigger(float duration, float targetAlpha, MMTweenType tween, int id = 0, bool ignoreTimeScale = true, Vector3 worldPosition = default(Vector3))
		{
		}
	}
}
