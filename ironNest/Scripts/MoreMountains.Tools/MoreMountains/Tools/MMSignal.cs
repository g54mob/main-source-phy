using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMSignal
	{
		public enum SignalType
		{
			Sine = 0,
			Pulse = 1,
			Sawtooth = 2,
			Square = 3,
			Triangle = 4,
			DigitalNoise = 5,
			WhiteNoise = 6,
			PerlinNoise = 7,
			ValueNoise = 8,
			AnimationCurve = 9,
			MMTween = 10
		}

		private static int[] hash;

		private const int hashMask = 255;

		public static float GetValue(float time, SignalType signalType, float phase, float amplitude, float frequency, float offset, bool Invert = false, AnimationCurve curve = null, MMTween.MMTweenCurve tweenCurve = MMTween.MMTweenCurve.LinearTween)
		{
			return 0f;
		}

		public static float GetValueNormalized(float time, SignalType signalType, float phase, float amplitude, float frequency, float offset, bool Invert = false, AnimationCurve curve = null, MMTween.MMTweenCurve tweenCurve = MMTween.MMTweenCurve.LinearTween, bool clamp = true, float clampMin = 0f, float clampMax = 1f, bool backAndForth = false, float backAndForthTippingPoint = 0.5f)
		{
			return 0f;
		}

		protected static float ValueNoise(float time, float frequency)
		{
			return 0f;
		}
	}
}
