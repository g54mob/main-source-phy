using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.FeedbacksForThirdParty
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public struct MMDepthOfFieldShakeEvent_HDRP
	{
		public delegate void Delegate(float duration, float attenuation = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false, bool animateFocusDistance = false, AnimationCurve shakeFocusDistance = null, float remapFocusDistanceZero = 0f, float remapFocusDistanceOne = 1f, bool animateNearRangeStart = false, AnimationCurve shakeNearRangeStart = null, float remapNearRangeStartZero = 0f, float remapNearRangeStartOne = 0f, bool animateNearRangeEnd = false, AnimationCurve shakeNearRangeEnd = null, float remapNearRangeEndZero = 0f, float remapNearRangeEndOne = 0f, bool animateFarRangeStart = false, AnimationCurve shakeFarRangeStart = null, float remapFarRangeStartZero = 0f, float remapFarRangeStartOne = 0f, bool animateFarRangeEnd = false, AnimationCurve shakeFarRangeEnd = null, float remapFarRangeEndZero = 0f, float remapFarRangeEndOne = 0f);

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

		public static void Trigger(float duration, float attenuation = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false, bool animateFocusDistance = false, AnimationCurve shakeFocusDistance = null, float remapFocusDistanceZero = 0f, float remapFocusDistanceOne = 1f, bool animateNearRangeStart = false, AnimationCurve shakeNearRangeStart = null, float remapNearRangeStartZero = 0f, float remapNearRangeStartOne = 0f, bool animateNearRangeEnd = false, AnimationCurve shakeNearRangeEnd = null, float remapNearRangeEndZero = 0f, float remapNearRangeEndOne = 0f, bool animateFarRangeStart = false, AnimationCurve shakeFarRangeStart = null, float remapFarRangeStartZero = 0f, float remapFarRangeStartOne = 0f, bool animateFarRangeEnd = false, AnimationCurve shakeFarRangeEnd = null, float remapFarRangeEndZero = 0f, float remapFarRangeEndOne = 0f)
		{
		}
	}
}
