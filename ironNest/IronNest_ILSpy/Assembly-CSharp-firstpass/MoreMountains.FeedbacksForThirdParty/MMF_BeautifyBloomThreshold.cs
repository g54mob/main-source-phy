using Cpp2ILInjected;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.FeedbacksForThirdParty;

public class MMF_BeautifyBloomThreshold : MMF_Feedback
{
	public static bool FeedbackTypeAuthorized = true;

	public float ShakeDuration = 0.2f;

	public bool ResetShakerValuesAfterShake = true;

	public bool ResetTargetValuesAfterShake;

	public bool RelativeValues;

	public AnimationCurve ShakeThreshold;

	public float RemapThresholdZero;

	public float RemapThresholdOne;

	private static readonly AnimationCurve _flatCurve;

	public override float FeedbackDuration
	{
		get
		{
			//IL_0005: Expected I, but got O
			//IL_001f: Expected O, but got I
			//IL_002f: Expected O, but got I
			nint num = (nint)this;
			float shakeDuration = ShakeDuration;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<MoreMountains.FeedbacksForThirdParty.MMF_BeautifyBloomThreshold>)+648]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<MoreMountains.FeedbacksForThirdParty.MMF_BeautifyBloomThreshold>)+650]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v3 @ rax_v1 (should have been resolved before IL gen)");
			/*Error: End of method reached without returning.*/;
		}
		set
		{
			ShakeDuration = value;
		}
	}

	public override bool HasChannel => true;

	public override bool HasRandomness => true;

	protected unsafe override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
	{
		//IL_0018: Expected O, but got Ref
		//IL_0109: Expected I4, but got O
		//IL_0109: Expected I4, but got F4
		//IL_0109: Expected I4, but got F4
		//IL_0109: Expected I4, but got O
		//IL_0109: Expected F4, but got O
		//IL_0109: Expected F4, but got O
		//IL_0109: Expected O, but got F4
		if (Active && FeedbackTypeAuthorized)
		{
			object obj = default(object);
			float num = base.ComputeIntensity(feedbacksIntensity, (Vector3)(&obj));
			float feedbackDuration = FeedbackDuration;
			MMChannelData channelData = base.ChannelData;
			bool normalPlayDirection = base.NormalPlayDirection;
			TimescaleModes computedTimescaleMode = base.ComputedTimescaleMode;
			float remapBloomThresholdZero = default(float);
			float remapBloomThresholdOne = default(float);
			AnimationCurve chromaticCurve = default(AnimationCurve);
			float remapChromaticZero = default(float);
			MMBeautifyShakeEvent.Trigger(_flatCurve, 0f, 0f, ShakeThreshold, remapBloomThresholdZero, remapBloomThresholdOne, chromaticCurve, remapChromaticZero, RemapThresholdZero, (AnimationCurve)RemapThresholdOne, (float)_flatCurve, 0f, null, (float)_flatCurve, 0f, 0f, (byte)(int)_flatCurve != 0, 0f, null, (byte)(int)position.x != 0, RelativeValues, (byte)(int)position.x != 0, (TimescaleModes)channelData, ResetShakerValuesAfterShake, ResetTargetValuesAfterShake);
		}
	}

	protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
	{
		//IL_006a: Expected I4, but got O
		//IL_00e7: Expected I4, but got F4
		//IL_00e7: Expected I4, but got F4
		//IL_00e7: Expected I4, but got O
		//IL_00e7: Expected F4, but got O
		//IL_00e7: Expected F4, but got O
		//IL_00e7: Expected O, but got F4
		if (Active && FeedbackTypeAuthorized)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
			float feedbackDuration = FeedbackDuration;
			TimescaleModes timescaleMode = (TimescaleModes)base.ChannelData;
			float remapBloomThresholdZero = default(float);
			float remapBloomThresholdOne = default(float);
			AnimationCurve chromaticCurve = default(AnimationCurve);
			float remapChromaticZero = default(float);
			MMBeautifyShakeEvent.Trigger(_flatCurve, 0f, 0f, ShakeThreshold, remapBloomThresholdZero, remapBloomThresholdOne, chromaticCurve, remapChromaticZero, RemapThresholdZero, (AnimationCurve)RemapThresholdOne, (float)_flatCurve, 0f, null, (float)_flatCurve, 0f, 0f, (byte)(int)_flatCurve != 0, 0f, null, (byte)(int)position.x != 0, RelativeValues, forwardDirection: true, timescaleMode, stop: true, restore: true);
		}
	}

	protected override void CustomRestoreInitialValues()
	{
		//IL_0060: Expected I4, but got O
		//IL_00d8: Expected I4, but got F4
		//IL_00d8: Expected I4, but got O
		//IL_00d8: Expected F4, but got O
		//IL_00d8: Expected F4, but got O
		//IL_00d8: Expected O, but got F4
		if (Active && FeedbackTypeAuthorized)
		{
			float feedbackDuration = FeedbackDuration;
			TimescaleModes timescaleMode = (TimescaleModes)base.ChannelData;
			float remapBloomThresholdZero = default(float);
			float remapBloomThresholdOne = default(float);
			AnimationCurve chromaticCurve = default(AnimationCurve);
			float remapChromaticZero = default(float);
			bool resetShakerValuesAfterShake = default(bool);
			MMBeautifyShakeEvent.Trigger(_flatCurve, 0f, 0f, ShakeThreshold, remapBloomThresholdZero, remapBloomThresholdOne, chromaticCurve, remapChromaticZero, RemapThresholdZero, (AnimationCurve)RemapThresholdOne, (float)_flatCurve, 0f, null, (float)_flatCurve, 0f, 0f, (byte)(int)_flatCurve != 0, 0f, null, resetShakerValuesAfterShake, RelativeValues, forwardDirection: true, timescaleMode, stop: true, restore: true);
		}
	}

	public MMF_BeautifyBloomThreshold()
	{
		Keyframe[] keys = new Keyframe[3];
		Keyframe keyframe = new Keyframe(0f, 0f);
		_ = 0;
		_ = 0;
		_ = 0;
		Keyframe keyframe2 = new Keyframe(0.5f, 1f);
		_ = 0;
		_ = 0;
		_ = 0;
		Keyframe keyframe3 = new Keyframe(1f, 0f);
		_ = 0;
		_ = 0;
		_ = 0;
		ShakeThreshold = new AnimationCurve(keys);
		RemapThresholdZero = 0.75f;
		RemapThresholdOne = 0.1f;
		base._002Ector();
	}

	static MMF_BeautifyBloomThreshold()
	{
		Keyframe[] keys = new Keyframe[2];
		Keyframe keyframe = new Keyframe(0f, 0f);
		_ = 0;
		_ = 0;
		_ = 0;
		Keyframe keyframe2 = new Keyframe(1f, 0f);
		_ = 0;
		_ = 0;
		_ = 0;
		AnimationCurve flatCurve = new AnimationCurve(keys);
		_flatCurve = flatCurve;
	}
}
