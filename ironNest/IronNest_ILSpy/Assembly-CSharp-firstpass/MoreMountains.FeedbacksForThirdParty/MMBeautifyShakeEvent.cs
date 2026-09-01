using System;
using System.Runtime.InteropServices;
using Cpp2ILInjected;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.FeedbacksForThirdParty;

[StructLayout((LayoutKind)0, Size = 1)]
public struct MMBeautifyShakeEvent
{
	public delegate void Delegate(AnimationCurve bloomIntensityCurve, float remapBloomIntensityZero, float remapBloomIntensityOne, AnimationCurve bloomThresholdCurve, float remapBloomThresholdZero, float remapBloomThresholdOne, AnimationCurve chromaticCurve, float remapChromaticZero, float remapChromaticOne, AnimationCurve blurCurve, float remapBlurZero, float remapBlurOne, AnimationCurve anamorphicFlaresCurve, float remapAnamorphicFlaresZero, float remapAnamorphicFlaresOne, float duration, bool relativeValues = false, float attenuation = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false);

	private static Delegate m_OnEvent;

	private static event Delegate OnEvent
	{
		add
		{
			//IL_004f: Expected I, but got O
			System.Delegate obj = MMBeautifyShakeEvent.m_OnEvent;
			System.Delegate obj4 = default(System.Delegate);
			while (true)
			{
				System.Delegate obj2 = System.Delegate.Combine(obj, value);
				bool flag = (object)obj2 == null;
				System.Delegate obj3 = null;
				if (!flag)
				{
					bool flag2 = (object)obj2.GetType() != typeof(Delegate);
					obj3 = null;
					if (!flag2)
					{
						obj3 = obj2;
					}
					if ((object)obj3 == null)
					{
						break;
					}
				}
				nint num = (nint)typeof(MMBeautifyShakeEvent);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj4 != obj;
				obj = obj4;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_004f: Expected I, but got O
			System.Delegate obj = MMBeautifyShakeEvent.m_OnEvent;
			System.Delegate obj4 = default(System.Delegate);
			while (true)
			{
				System.Delegate obj2 = System.Delegate.Remove(obj, value);
				bool flag = (object)obj2 == null;
				System.Delegate obj3 = null;
				if (!flag)
				{
					bool flag2 = (object)obj2.GetType() != typeof(Delegate);
					obj3 = null;
					if (!flag2)
					{
						obj3 = obj2;
					}
					if ((object)obj3 == null)
					{
						break;
					}
				}
				nint num = (nint)typeof(MMBeautifyShakeEvent);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj4 != obj;
				obj = obj4;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	private static void RuntimeInitialization()
	{
		MMBeautifyShakeEvent.m_OnEvent = null;
	}

	public static void Register(Delegate callback)
	{
		//IL_004f: Expected I, but got O
		System.Delegate obj = MMBeautifyShakeEvent.m_OnEvent;
		System.Delegate obj4 = default(System.Delegate);
		while (true)
		{
			System.Delegate obj2 = System.Delegate.Combine(obj, callback);
			bool flag = (object)obj2 == null;
			System.Delegate obj3 = null;
			if (!flag)
			{
				bool flag2 = (object)obj2.GetType() != typeof(Delegate);
				obj3 = null;
				if (!flag2)
				{
					obj3 = obj2;
				}
				if ((object)obj3 == null)
				{
					break;
				}
			}
			nint num = (nint)typeof(MMBeautifyShakeEvent);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
			bool flag3 = (object)obj4 != obj;
			obj = obj4;
			if (!flag3)
			{
				return;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	public static void Unregister(Delegate callback)
	{
		//IL_004f: Expected I, but got O
		System.Delegate obj = MMBeautifyShakeEvent.m_OnEvent;
		System.Delegate obj4 = default(System.Delegate);
		while (true)
		{
			System.Delegate obj2 = System.Delegate.Remove(obj, callback);
			bool flag = (object)obj2 == null;
			System.Delegate obj3 = null;
			if (!flag)
			{
				bool flag2 = (object)obj2.GetType() != typeof(Delegate);
				obj3 = null;
				if (!flag2)
				{
					obj3 = obj2;
				}
				if ((object)obj3 == null)
				{
					break;
				}
			}
			nint num = (nint)typeof(MMBeautifyShakeEvent);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
			bool flag3 = (object)obj4 != obj;
			obj = obj4;
			if (!flag3)
			{
				return;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	public static void Trigger(AnimationCurve bloomIntensityCurve, float remapBloomIntensityZero, float remapBloomIntensityOne, AnimationCurve bloomThresholdCurve, float remapBloomThresholdZero, float remapBloomThresholdOne, AnimationCurve chromaticCurve, float remapChromaticZero, float remapChromaticOne, AnimationCurve blurCurve, float remapBlurZero, float remapBlurOne, AnimationCurve anamorphicFlaresCurve, float remapAnamorphicFlaresZero, float remapAnamorphicFlaresOne, float duration, bool relativeValues = false, float attenuation = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false)
	{
		Delegate onEvent = MMBeautifyShakeEvent.m_OnEvent;
		if (MMBeautifyShakeEvent.m_OnEvent != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v36.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
	}
}
