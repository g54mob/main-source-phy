using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.FeedbacksForThirdParty
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public struct MMBeautifyShakeEvent
	{
		public delegate void Delegate(AnimationCurve bloomIntensityCurve, float remapBloomIntensityZero, float remapBloomIntensityOne, AnimationCurve bloomThresholdCurve, float remapBloomThresholdZero, float remapBloomThresholdOne, AnimationCurve chromaticCurve, float remapChromaticZero, float remapChromaticOne, AnimationCurve blurCurve, float remapBlurZero, float remapBlurOne, AnimationCurve anamorphicFlaresCurve, float remapAnamorphicFlaresZero, float remapAnamorphicFlaresOne, float duration, bool relativeValues = false, float attenuation = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false);

		private static event Delegate OnEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void RuntimeInitialization()
		{
		}

		public static void Register(Delegate callback)
		{
		}

		public static void Unregister(Delegate callback)
		{
		}

		public static void Trigger(AnimationCurve bloomIntensityCurve, float remapBloomIntensityZero, float remapBloomIntensityOne, AnimationCurve bloomThresholdCurve, float remapBloomThresholdZero, float remapBloomThresholdOne, AnimationCurve chromaticCurve, float remapChromaticZero, float remapChromaticOne, AnimationCurve blurCurve, float remapBlurZero, float remapBlurOne, AnimationCurve anamorphicFlaresCurve, float remapAnamorphicFlaresZero, float remapAnamorphicFlaresOne, float duration, bool relativeValues = false, float attenuation = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false)
		{
		}
	}
}
