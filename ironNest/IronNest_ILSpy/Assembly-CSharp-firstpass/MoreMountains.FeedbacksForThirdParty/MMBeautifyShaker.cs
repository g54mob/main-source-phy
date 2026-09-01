using System;
using System.Runtime.CompilerServices;
using Beautify.Universal;
using Cpp2ILInjected;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Rendering;

namespace MoreMountains.FeedbacksForThirdParty;

public class MMBeautifyShaker : MMShaker
{
	public bool RelativeValues;

	public AnimationCurve ShakeBloomIntensity;

	public float RemapBloomIntensityZero;

	public float RemapBloomIntensityOne;

	public AnimationCurve ShakeBloomThreshold;

	public float RemapBloomThresholdZero;

	public float RemapBloomThresholdOne;

	public AnimationCurve ShakeChromaticAberration;

	public float RemapChromaticAberrationZero;

	public float RemapChromaticAberrationOne;

	public AnimationCurve ShakeCreativeBlur;

	public float RemapCreativeBlurZero;

	public float RemapCreativeBlurOne;

	public AnimationCurve ShakeAnamorphicFlaresIntensity;

	public float RemapAnamorphicFlaresIntensityZero;

	public float RemapAnamorphicFlaresIntensityOne;

	protected Volume _volume;

	protected Beautify.Universal.Beautify _beautify;

	protected float _initialBloomIntensity;

	protected float _initialBloomThreshold;

	protected float _initialChromaticAberration;

	protected float _initialCreativeBlur;

	protected float _initialAnamorphicFlaresIntensity;

	protected float _originalShakeDuration;

	protected bool _originalRelativeValues;

	protected AnimationCurve _originalShakeBloomIntensity;

	protected float _originalRemapBloomIntensityZero;

	protected float _originalRemapBloomIntensityOne;

	protected AnimationCurve _originalShakeBloomThreshold;

	protected float _originalRemapBloomThresholdZero;

	protected float _originalRemapBloomThresholdOne;

	protected AnimationCurve _originalShakeChromaticAberration;

	protected float _originalRemapChromaticAberrationZero;

	protected float _originalRemapChromaticAberrationOne;

	protected AnimationCurve _originalShakeCreativeBlur;

	protected float _originalRemapCreativeBlurZero;

	protected float _originalRemapCreativeBlurOne;

	protected AnimationCurve _originalShakeAnamorphicFlaresIntensity;

	protected float _originalRemapAnamorphicFlaresIntensityZero;

	protected float _originalRemapAnamorphicFlaresIntensityOne;

	protected unsafe override void Initialization()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected Ref, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		GameObject gameObject = base.gameObject;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		Volume volume = default(Volume);
		_volume = volume;
		VolumeProfile profile = _volume.profile;
		if (!profile.TryGet<Beautify.Universal.Beautify>(out *(Beautify.Universal.Beautify*)(this + 192)))
		{
			GameObject gameObject2 = base.gameObject;
			string text = gameObject2.name;
			string message = "[MMBeautifyShaker] No Beautify effect found in the Volume profile on " + text + ". Add Beautify as an override in the profile.";
			Debug.LogError(message, this);
		}
	}

	protected unsafe override void Shake()
	{
		//IL_0017: Expected F4, but got Ref
		//IL_005b: Expected F4, but got Ref
		//IL_009f: Expected F4, but got Ref
		//IL_00e3: Expected F4, but got Ref
		//IL_0126: Expected F4, but got Ref
		float num = base.ShakeFloat(ShakeBloomIntensity, RemapBloomIntensityZero, RemapBloomIntensityOne, relativeIntensity: false, 0f);
		Beautify.Universal.Beautify beautify = _beautify;
		float num2 = default(float);
		beautify.bloomIntensity.Override((nint)(&num2));
		float num3 = base.ShakeFloat(ShakeBloomThreshold, RemapBloomThresholdZero, RemapBloomThresholdOne, relativeIntensity: false, 0f);
		Beautify.Universal.Beautify beautify2 = _beautify;
		beautify2.bloomThreshold.Override((nint)(&num2));
		float num4 = base.ShakeFloat(ShakeChromaticAberration, RemapChromaticAberrationZero, RemapChromaticAberrationOne, relativeIntensity: false, 0f);
		Beautify.Universal.Beautify beautify3 = _beautify;
		beautify3.chromaticAberrationIntensity.Override((nint)(&num2));
		float num5 = base.ShakeFloat(ShakeCreativeBlur, RemapCreativeBlurZero, RemapCreativeBlurOne, relativeIntensity: false, 0f);
		Beautify.Universal.Beautify beautify4 = _beautify;
		beautify4.blurIntensity.Override((nint)(&num2));
		float num6 = base.ShakeFloat(ShakeAnamorphicFlaresIntensity, RemapAnamorphicFlaresIntensityZero, RemapAnamorphicFlaresIntensityOne, relativeIntensity: false, 0f);
		Beautify.Universal.Beautify beautify5 = _beautify;
		beautify5.anamorphicFlaresIntensity.Override((nint)(&num2));
	}

	protected override void GrabInitialValues()
	{
		Beautify.Universal.Beautify beautify = _beautify;
		float value = beautify.bloomIntensity.value;
		Beautify.Universal.Beautify beautify2 = _beautify;
		float num = default(float);
		_initialBloomIntensity = num;
		float value2 = beautify2.bloomThreshold.value;
		Beautify.Universal.Beautify beautify3 = _beautify;
		_initialBloomThreshold = num;
		float value3 = beautify3.chromaticAberrationIntensity.value;
		Beautify.Universal.Beautify beautify4 = _beautify;
		_initialChromaticAberration = num;
		float value4 = beautify4.blurIntensity.value;
		Beautify.Universal.Beautify beautify5 = _beautify;
		_initialCreativeBlur = num;
		float value5 = beautify5.anamorphicFlaresIntensity.value;
		_initialAnamorphicFlaresIntensity = num;
	}

	public virtual void OnMMBeautifyShakeEvent(AnimationCurve bloomIntensityCurve, float remapBloomIntensityZero, float remapBloomIntensityOne, AnimationCurve bloomThresholdCurve, float remapBloomThresholdZero, float remapBloomThresholdOne, AnimationCurve chromaticCurve, float remapChromaticZero, float remapChromaticOne, AnimationCurve blurCurve, float remapBlurZero, float remapBlurOne, AnimationCurve anamorphicFlaresCurve, float remapAnamorphicFlaresZero, float remapAnamorphicFlaresOne, float duration, bool relativeValues = false, float attenuation = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool forwardDirection = true, TimescaleModes timescaleMode = TimescaleModes.Scaled, bool stop = false, bool restore = false)
	{
		//IL_0005: Expected I, but got O
		//IL_001a: Expected O, but got I4
		//IL_001a: Expected O, but got I4
		//IL_0340: Expected I, but got O
		//IL_0350: Expected O, but got I
		//IL_0360: Expected O, but got I
		//IL_0369: Expected F4, but got I4
		//IL_030d: Expected I, but got O
		//IL_031d: Expected O, but got I
		//IL_032d: Expected O, but got I
		//IL_0336: Expected F4, but got I4
		//IL_038c: Expected F4, but got I4
		//IL_03ae: Expected I, but got O
		//IL_03be: Expected O, but got I
		//IL_03ce: Expected O, but got I
		//IL_01eb: Expected F4, but got I4
		//IL_020e: Expected O, but got F4
		//IL_0240: Expected F4, but got O
		//IL_0254: Expected O, but got F4
		//IL_026d: Expected O, but got F4
		//IL_02ae: Expected O, but got F4
		nint num = (nint)this;
		if (!base.CheckEventAllowed((MMChannelData)stop, useRange: false, 0f, (Vector3)0) || (!Interruptible && Shaking))
		{
			return;
		}
		object obj = default(object);
		if (obj == null)
		{
			object obj2 = default(object);
			if (obj2 == null)
			{
				_resetShakerValuesAfterShake = restore;
				IntPtr intPtr = default(IntPtr);
				_resetTargetValuesAfterShake = (byte)(nint)intPtr != 0;
				if (restore)
				{
					_originalShakeDuration = ShakeDuration;
					_originalRelativeValues = RelativeValues;
					_originalShakeBloomIntensity = ShakeBloomIntensity;
					_originalRemapBloomIntensityZero = RemapBloomIntensityZero;
					_originalRemapBloomIntensityOne = RemapBloomIntensityOne;
					_originalShakeBloomThreshold = ShakeBloomThreshold;
					_originalRemapBloomThresholdZero = RemapBloomThresholdZero;
					_originalRemapBloomThresholdOne = RemapBloomThresholdOne;
					_originalShakeChromaticAberration = ShakeChromaticAberration;
					_originalRemapChromaticAberrationZero = RemapChromaticAberrationZero;
					_originalRemapChromaticAberrationOne = RemapChromaticAberrationOne;
					_originalShakeCreativeBlur = ShakeCreativeBlur;
					_originalRemapCreativeBlurZero = RemapCreativeBlurZero;
					_originalRemapCreativeBlurOne = RemapCreativeBlurOne;
					_originalShakeAnamorphicFlaresIntensity = ShakeAnamorphicFlaresIntensity;
					_originalRemapAnamorphicFlaresIntensityZero = RemapAnamorphicFlaresIntensityZero;
					_originalRemapAnamorphicFlaresIntensityOne = RemapAnamorphicFlaresIntensityOne;
				}
				bool flag = OnlyUseShakerValues;
				float num2 = 0f;
				if (!flag)
				{
					TimescaleModes timescaleMode2 = default(TimescaleModes);
					TimescaleMode = timescaleMode2;
					RelativeValues = forwardDirection;
					bool forwardDirection2 = default(bool);
					ForwardDirection = forwardDirection2;
					ShakeDuration = (resetTargetValuesAfterShake ? 1 : 0);
					ShakeBloomIntensity = bloomIntensityCurve;
					float remapBloomIntensityZero2 = remapBloomIntensityZero * (float)timescaleMode;
					ShakeBloomThreshold = (AnimationCurve)remapChromaticOne;
					float remapBloomIntensityOne2 = remapBloomIntensityOne * (float)timescaleMode;
					RemapBloomIntensityZero = remapBloomIntensityZero2;
					RemapBloomIntensityOne = remapBloomIntensityOne2;
					RemapBloomThresholdZero = (float)blurCurve;
					RemapBloomThresholdOne = remapBlurZero;
					ShakeChromaticAberration = (AnimationCurve)remapBlurOne;
					float remapChromaticAberrationZero = (float)anamorphicFlaresCurve * (float)timescaleMode;
					ShakeCreativeBlur = (AnimationCurve)remapAnamorphicFlaresOne;
					float remapChromaticAberrationOne = remapAnamorphicFlaresZero * (float)timescaleMode;
					RemapChromaticAberrationZero = remapChromaticAberrationZero;
					RemapChromaticAberrationOne = remapChromaticAberrationOne;
					float remapCreativeBlurZero = duration * (float)timescaleMode;
					ShakeAnamorphicFlaresIntensity = (AnimationCurve)attenuation;
					float remapCreativeBlurOne = (float)(relativeValues ? 1 : 0) * (float)timescaleMode;
					RemapCreativeBlurZero = remapCreativeBlurZero;
					RemapCreativeBlurOne = remapCreativeBlurOne;
					num2 = (float)channelData * (float)timescaleMode;
					float remapAnamorphicFlaresIntensityOne = (float)(resetShakerValuesAfterShake ? 1 : 0) * (float)timescaleMode;
					RemapAnamorphicFlaresIntensityZero = num2;
					RemapAnamorphicFlaresIntensityOne = remapAnamorphicFlaresIntensityOne;
				}
				nint num3 = (nint)this;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ rdx_v10 (Il2CppClass<MoreMountains.FeedbacksForThirdParty.MMBeautifyShaker>)+2B8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ rdx_v10 (Il2CppClass<MoreMountains.FeedbacksForThirdParty.MMBeautifyShaker>)+2C0]");
				object obj4 = 0;
			}
			else
			{
				nint num4 = (nint)this;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v165 @ rdx_v6 (Il2CppClass<MoreMountains.FeedbacksForThirdParty.MMBeautifyShaker>)+258]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v165 @ rdx_v6 (Il2CppClass<MoreMountains.FeedbacksForThirdParty.MMBeautifyShaker>)+260]");
				object obj4 = 0;
				float num2 = 0f;
			}
		}
		else
		{
			nint num5 = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v148 @ rdx_v4 (Il2CppClass<MoreMountains.FeedbacksForThirdParty.MMBeautifyShaker>)+2C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v148 @ rdx_v4 (Il2CppClass<MoreMountains.FeedbacksForThirdParty.MMBeautifyShaker>)+2D0]");
			object obj4 = 0;
			float num2 = 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v184 @ rax_v6 (should have been resolved before IL gen)");
	}

	protected unsafe override void ResetTargetValues()
	{
		//IL_0018: Expected F4, but got Ref
		//IL_003a: Expected F4, but got Ref
		//IL_005c: Expected F4, but got Ref
		//IL_007e: Expected F4, but got Ref
		//IL_009f: Expected F4, but got Ref
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		Beautify.Universal.Beautify beautify = _beautify;
		float num = default(float);
		beautify.bloomIntensity.Override((nint)(&num));
		Beautify.Universal.Beautify beautify2 = _beautify;
		beautify2.bloomThreshold.Override((nint)(&num));
		Beautify.Universal.Beautify beautify3 = _beautify;
		beautify3.chromaticAberrationIntensity.Override((nint)(&num));
		Beautify.Universal.Beautify beautify4 = _beautify;
		beautify4.blurIntensity.Override((nint)(&num));
		Beautify.Universal.Beautify beautify5 = _beautify;
		beautify5.anamorphicFlaresIntensity.Override((nint)(&num));
	}

	protected override void ResetShakerValues()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		ShakeDuration = _originalShakeDuration;
		RelativeValues = _originalRelativeValues;
		ShakeBloomIntensity = _originalShakeBloomIntensity;
		RemapBloomIntensityZero = _originalRemapBloomIntensityZero;
		RemapBloomIntensityOne = _originalRemapBloomIntensityOne;
		ShakeBloomThreshold = _originalShakeBloomThreshold;
		RemapBloomThresholdZero = _originalRemapBloomThresholdZero;
		RemapBloomThresholdOne = _originalRemapBloomThresholdOne;
		ShakeChromaticAberration = _originalShakeChromaticAberration;
		RemapChromaticAberrationZero = _originalRemapChromaticAberrationZero;
		RemapChromaticAberrationOne = _originalRemapChromaticAberrationOne;
		ShakeCreativeBlur = _originalShakeCreativeBlur;
		RemapCreativeBlurZero = _originalRemapCreativeBlurZero;
		RemapCreativeBlurOne = _originalRemapCreativeBlurOne;
		ShakeAnamorphicFlaresIntensity = _originalShakeAnamorphicFlaresIntensity;
		RemapAnamorphicFlaresIntensityZero = _originalRemapAnamorphicFlaresIntensityZero;
		RemapAnamorphicFlaresIntensityOne = _originalRemapAnamorphicFlaresIntensityOne;
	}

	public override void StartListening()
	{
		//IL_000a: Expected I, but got O
		//IL_006f: Expected I, but got O
		base.StartListening();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r8_v2 (Il2CppClass<MoreMountains.FeedbacksForThirdParty.MMBeautifyShaker>)+320]");
		MMBeautifyShakeEvent.Delegate obj = new MMBeautifyShakeEvent.Delegate(this, (IntPtr)0);
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r8_v2 (Il2CppClass<MoreMountains.FeedbacksForThirdParty.MMBeautifyShaker>)+320]");
		obj._002Ector(this, (IntPtr)0);
		Delegate obj2 = MMBeautifyShakeEvent.OnEvent;
		Delegate obj5 = default(Delegate);
		while (true)
		{
			Delegate obj3 = Delegate.Combine(obj2, obj);
			bool flag = (object)obj3 == null;
			Delegate obj4 = null;
			if (!flag)
			{
				bool flag2 = (object)obj3.GetType() != typeof(MMBeautifyShakeEvent.Delegate);
				obj4 = null;
				if (!flag2)
				{
					obj4 = obj3;
				}
				if ((object)obj4 == null)
				{
					break;
				}
			}
			nint num2 = (nint)typeof(MMBeautifyShakeEvent);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
			bool flag3 = (object)obj5 != obj2;
			obj2 = obj5;
			if (!flag3)
			{
				return;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		throw new NullReferenceException();
	}

	public override void StopListening()
	{
		//IL_000a: Expected I, but got O
		//IL_006f: Expected I, but got O
		base.StopListening();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r8_v2 (Il2CppClass<MoreMountains.FeedbacksForThirdParty.MMBeautifyShaker>)+320]");
		MMBeautifyShakeEvent.Delegate obj = new MMBeautifyShakeEvent.Delegate(this, (IntPtr)0);
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r8_v2 (Il2CppClass<MoreMountains.FeedbacksForThirdParty.MMBeautifyShaker>)+320]");
		obj._002Ector(this, (IntPtr)0);
		Delegate obj2 = MMBeautifyShakeEvent.OnEvent;
		Delegate obj5 = default(Delegate);
		while (true)
		{
			Delegate obj3 = Delegate.Remove(obj2, obj);
			bool flag = (object)obj3 == null;
			Delegate obj4 = null;
			if (!flag)
			{
				bool flag2 = (object)obj3.GetType() != typeof(MMBeautifyShakeEvent.Delegate);
				obj4 = null;
				if (!flag2)
				{
					obj4 = obj3;
				}
				if ((object)obj4 == null)
				{
					break;
				}
			}
			nint num2 = (nint)typeof(MMBeautifyShakeEvent);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
			bool flag3 = (object)obj5 != obj2;
			obj2 = obj5;
			if (!flag3)
			{
				return;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		throw new NullReferenceException();
	}

	public unsafe MMBeautifyShaker()
	{
		//IL_0008: Expected O, but got Ref
		//IL_00bc: Expected O, but got Ref
		//IL_00dc: Expected native int or pointer, but got O
		//IL_00f4: Expected O, but got Ref
		//IL_013b: Expected native int or pointer, but got O
		//IL_0153: Expected O, but got Ref
		//IL_019a: Expected native int or pointer, but got O
		//IL_021f: Expected O, but got Ref
		//IL_023f: Expected native int or pointer, but got O
		//IL_0279: Expected native int or pointer, but got O
		//IL_0287: Expected native int or pointer, but got O
		//IL_0295: Expected native int or pointer, but got O
		//IL_02c0: Expected O, but got Ref
		//IL_02fe: Expected native int or pointer, but got O
		//IL_0378: Expected O, but got Ref
		//IL_0398: Expected native int or pointer, but got O
		//IL_03b0: Expected O, but got Ref
		//IL_03f7: Expected native int or pointer, but got O
		//IL_040f: Expected O, but got Ref
		//IL_0456: Expected native int or pointer, but got O
		//IL_04d0: Expected O, but got Ref
		//IL_04f0: Expected native int or pointer, but got O
		//IL_0508: Expected O, but got Ref
		//IL_054f: Expected native int or pointer, but got O
		//IL_0567: Expected O, but got Ref
		//IL_05ae: Expected native int or pointer, but got O
		Keyframe keyframe2 = default(Keyframe);
		Keyframe keyframe = (Keyframe)(&keyframe2);
		RelativeValues = true;
		Keyframe[] keys = new Keyframe[3];
		Keyframe keyframe3 = new Keyframe(0f, 0f);
		_ = 0;
		_ = 0;
		_ = 0;
		Keyframe keyframe4 = new Keyframe(0.5f, 1f);
		_ = 0;
		_ = 0;
		_ = 0;
		Keyframe keyframe5 = new Keyframe(1f, 0f);
		_ = 0;
		_ = 0;
		_ = 0;
		ShakeBloomIntensity = new AnimationCurve(keys);
		RemapBloomIntensityOne = 2f;
		Keyframe[] keys2 = new Keyframe[3];
		Keyframe keyframe6 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref keyframe2, 128));
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe6 = new Keyframe(0f, 0f);
		Keyframe keyframe7 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref keyframe2, 96));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)-80]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)-70]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)-68]");
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe7 = new Keyframe(0.5f, 1f);
		Keyframe keyframe8 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref keyframe2, 64));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)-60]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)-50]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)-48]");
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe8 = new Keyframe(1f, 0f);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)-40]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)-30]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)-28]");
		_ = 0;
		ShakeBloomThreshold = new AnimationCurve(keys2);
		RemapBloomThresholdZero = 0.75f;
		RemapBloomThresholdOne = 0.1f;
		Keyframe[] keys3 = new Keyframe[3];
		Keyframe keyframe9 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref keyframe2, 32));
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe9 = new Keyframe(0f, 0f);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)-20]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)-10]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)-8]");
		_ = 0;
		((Keyframe*)(nint)keyframe)->m_WeightedMode = 0;
		((Keyframe*)(nint)keyframe)->m_OutWeight = 0f;
		((Keyframe*)(nint)keyframe)->m_Time = 0f;
		keyframe2 = new Keyframe(0.5f, 1f);
		Keyframe keyframe10 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref keyframe2, 32));
		_ = keyframe.m_Time;
		_ = keyframe.m_WeightedMode;
		_ = keyframe.m_OutWeight;
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe10 = new Keyframe(1f, 0f);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)+20]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)+30]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)+38]");
		_ = 0;
		ShakeChromaticAberration = new AnimationCurve(keys3);
		RemapChromaticAberrationOne = 0.05f;
		Keyframe[] keys4 = new Keyframe[3];
		Keyframe keyframe11 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref keyframe2, 64));
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe11 = new Keyframe(0f, 0f);
		Keyframe keyframe12 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref keyframe2, 96));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)+40]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)+50]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)+58]");
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe12 = new Keyframe(0.5f, 1f);
		Keyframe keyframe13 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref keyframe2, 128));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)+60]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)+70]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)+78]");
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe13 = new Keyframe(1f, 0f);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)+80]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)+90]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)+98]");
		_ = 0;
		ShakeCreativeBlur = new AnimationCurve(keys4);
		RemapCreativeBlurOne = 8f;
		Keyframe[] keys5 = new Keyframe[3];
		Keyframe keyframe14 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref keyframe2, 160));
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe14 = new Keyframe(0f, 0f);
		Keyframe keyframe15 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref keyframe2, 192));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)+A0]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)+B0]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)+B8]");
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe15 = new Keyframe(0.5f, 1f);
		Keyframe keyframe16 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref keyframe2, 224));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)+C0]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)+D0]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)+D8]");
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe16 = new Keyframe(1f, 0f);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)+E0]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)+F0]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Keyframe)+F8]");
		_ = 0;
		ShakeAnamorphicFlaresIntensity = new AnimationCurve(keys5);
		RemapAnamorphicFlaresIntensityOne = 1f;
		base._002Ector();
	}
}
