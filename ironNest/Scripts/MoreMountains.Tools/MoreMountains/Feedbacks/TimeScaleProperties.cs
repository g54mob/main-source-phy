using MoreMountains.Tools;

namespace MoreMountains.Feedbacks
{
	public struct TimeScaleProperties
	{
		public float TimeScale;

		public float Duration;

		public bool TimeScaleLerp;

		public float LerpSpeed;

		public bool Infinite;

		public MMTimeScaleLerpModes TimeScaleLerpMode;

		public MMTweenType TimeScaleLerpCurve;

		public float TimeScaleLerpDuration;

		public bool TimeScaleLerpOnReset;

		public MMTweenType TimeScaleLerpCurveOnReset;

		public float TimeScaleLerpDurationOnReset;

		public override string ToString()
		{
			return null;
		}
	}
}
