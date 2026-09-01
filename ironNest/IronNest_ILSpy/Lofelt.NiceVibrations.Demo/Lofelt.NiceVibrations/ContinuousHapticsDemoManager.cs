using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.UI;

namespace Lofelt.NiceVibrations;

public class ContinuousHapticsDemoManager : DemoManager
{
	public float ContinuousAmplitude = 1f;

	public float ContinuousFrequency = 1f;

	public float ContinuousDuration = 3f;

	public Text ContinuousAmplitudeText;

	public Text ContinuousFrequencyText;

	public Text ContinuousDurationText;

	public Text ContinuousButtonText;

	public MMTouchButton ContinuousButton;

	public MMProgressBar AmplitudeProgressBar;

	public MMProgressBar FrequencyProgressBar;

	public MMProgressBar DurationProgressBar;

	public MMProgressBar ContinuousProgressBar;

	public HapticCurve TargetCurve;

	public Slider DurationSlider;

	protected float _timeLeft;

	protected Color _continuousButtonOnColor;

	protected Color _continuousButtonOffColor;

	protected bool _continuousActive;

	protected float _amplitudeLastFrame = -1f;

	protected float _frequencyLastFrame = -1f;

	protected unsafe virtual void Awake()
	{
		//IL_0033: Expected Ref, but got F4
		//IL_0061: Expected Ref, but got F4
		//IL_008f: Expected Ref, but got F4
		MMTouchButton continuousButton = ContinuousButton;
		continuousButton._003CReturnToInitialSpriteAutomatically_003Ek__BackingField = false;
		float num = (float)this + 56f;
		string text = ((float*)num)->ToString();
		ContinuousAmplitudeText.text = text;
		float num2 = (float)this + 60f;
		string text2 = ((float*)num2)->ToString();
		ContinuousFrequencyText.text = text2;
		float num3 = (float)this + 64f;
		string text3 = ((float*)num3)->ToString();
		ContinuousDurationText.text = text3;
		AmplitudeProgressBar.UpdateBar(ContinuousAmplitude, 0f, 1f);
		FrequencyProgressBar.UpdateBar(ContinuousFrequency, 0f, 1f);
		DurationProgressBar.UpdateBar(ContinuousDuration, 0f, 5f);
	}

	protected virtual void Update()
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Lofelt.NiceVibrations.ContinuousHapticsDemoManager>)+198]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Lofelt.NiceVibrations.ContinuousHapticsDemoManager>)+1A0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	protected virtual void UpdateContinuousDemo()
	{
		//IL_01d4: Invalid comparison between F4 and I4
		if (!(_timeLeft > 0f))
		{
			ContinuousProgressBar.UpdateBar(0f, 0f, ContinuousDuration);
			MMUIShaker logo = Logo;
			logo.Shaking = false;
			HapticCurve targetCurve = TargetCurve;
			targetCurve.Move = false;
			if (_continuousActive)
			{
				HapticController.Stop();
			}
		}
		else
		{
			ContinuousProgressBar.UpdateBar(_timeLeft, 0f, ContinuousDuration);
			float deltaTime = Time.deltaTime;
			MMUIShaker logo2 = Logo;
			float timeLeft = _timeLeft - deltaTime;
			_timeLeft = timeLeft;
			logo2.Shaking = true;
			HapticCurve targetCurve2 = TargetCurve;
			targetCurve2.Move = true;
			MMUIShaker logo3 = Logo;
			float num = ContinuousAmplitude * 7f;
			float amplitude = num + 1f;
			logo3.Amplitude = amplitude;
			MMUIShaker logo4 = Logo;
			float num2 = ContinuousFrequency * 15f;
			float frequency = num2 + 10f;
			logo4.Frequency = frequency;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180A8471Ch\"");
		if (_frequencyLastFrame == ContinuousFrequency)
		{
			bool flag = _amplitudeLastFrame == ContinuousAmplitude;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180A8471Ch\"");
			if (flag)
			{
				goto IL_0215;
			}
		}
		TargetCurve.UpdateCurve(ContinuousAmplitude, ContinuousFrequency);
		goto IL_0215;
		IL_0215:
		_amplitudeLastFrame = ContinuousAmplitude;
		_frequencyLastFrame = ContinuousFrequency;
	}

	public virtual void UpdateContinuousAmplitude(float newAmplitude)
	{
		//IL_0021: Expected I, but got O
		ContinuousAmplitude = newAmplitude;
		MMProgressBar amplitudeProgressBar = AmplitudeProgressBar;
		nint num = (nint)amplitudeProgressBar;
		amplitudeProgressBar.UpdateBar(newAmplitude, 0f, 1f);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		float num2 = 10f * newAmplitude;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		float num3 = default(float);
		string text = num3.ToString();
		ContinuousAmplitudeText.text = text;
		UpdateContinuous();
	}

	public virtual void UpdateContinuousFrequency(float newFrequency)
	{
		//IL_0021: Expected I, but got O
		ContinuousFrequency = newFrequency;
		MMProgressBar frequencyProgressBar = FrequencyProgressBar;
		nint num = (nint)frequencyProgressBar;
		frequencyProgressBar.UpdateBar(newFrequency, 0f, 1f);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		float num2 = 10f * newFrequency;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		float num3 = default(float);
		string text = num3.ToString();
		ContinuousFrequencyText.text = text;
		UpdateContinuous();
	}

	public virtual void UpdateContinuousDuration(float newDuration)
	{
		//IL_0021: Expected I, but got O
		ContinuousDuration = newDuration;
		MMProgressBar durationProgressBar = DurationProgressBar;
		nint num = (nint)durationProgressBar;
		durationProgressBar.UpdateBar(newDuration, 0f, 5f);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		float num2 = 10f * newDuration;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		float num3 = default(float);
		string text = num3.ToString();
		ContinuousDurationText.text = text;
	}

	protected virtual void UpdateContinuous()
	{
		if (_continuousActive)
		{
			HapticController.clipLevel = ContinuousAmplitude;
			HapticController.clipFrequencyShift = ContinuousFrequency;
			DebugAudioContinuous.volume = ContinuousAmplitude;
			float num = ContinuousFrequency * 0.5f;
			float pitch = num + 0.5f;
			DebugAudioContinuous.pitch = pitch;
		}
	}

	public virtual void ContinuousHapticsButton()
	{
		//IL_0014: Expected I, but got O
		//IL_0024: Expected O, but got I
		//IL_0034: Expected O, but got I
		if (_continuousActive)
		{
			HapticController.Stop();
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ rdx_v5 (Il2CppClass<Lofelt.NiceVibrations.ContinuousHapticsDemoManager>)+208]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ rdx_v5 (Il2CppClass<Lofelt.NiceVibrations.ContinuousHapticsDemoManager>)+210]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v88 @ rax_v21 (should have been resolved before IL gen)");
		}
		HapticController._fallbackPreset = HapticPatterns.PresetType.LightImpact;
		HapticPatterns.PlayConstant(ContinuousAmplitude, ContinuousFrequency, ContinuousDuration);
		_timeLeft = ContinuousDuration;
		ContinuousButtonText.text = "Stop continuous haptic pattern";
		DurationSlider.interactable = false;
		_continuousActive = true;
		DebugAudioContinuous.Play();
	}

	protected virtual void OnHapticsStopped()
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Lofelt.NiceVibrations.ContinuousHapticsDemoManager>)+208]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Lofelt.NiceVibrations.ContinuousHapticsDemoManager>)+210]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	protected virtual void ResetPlayState()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3F51E]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		_timeLeft = 0f;
		ContinuousButtonText.text = "Play continuous haptic pattern";
		_continuousActive = false;
		if ((object)DebugAudioContinuous != null)
		{
			DebugAudioContinuous.Stop();
		}
		DurationSlider.interactable = true;
	}

	protected virtual void OnEnable()
	{
		//IL_000a: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ r8_v3 (Il2CppClass<Lofelt.NiceVibrations.ContinuousHapticsDemoManager>)+200]");
		Action action = new Action(this, (IntPtr)0);
		NullReferenceException typeFromHandle;
		if ((object)this != null)
		{
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ r8_v3 (Il2CppClass<Lofelt.NiceVibrations.ContinuousHapticsDemoManager>)+200]");
			action._002Ector(this, (IntPtr)0);
			Delegate obj = Delegate.Combine(HapticController.PlaybackStopped, action);
			if ((object)obj == null)
			{
				HapticController.PlaybackStopped = null;
				return;
			}
			bool flag = (object)obj.GetType() != typeof(Action);
			Delegate obj2 = null;
			if (!flag)
			{
				obj2 = obj;
			}
			Delegate obj4;
			if ((object)obj2 != null)
			{
				HapticController.PlaybackStopped = (Action)obj2;
				bool flag2 = (object)obj.GetType() != typeof(Action);
				Delegate obj3 = null;
				if (!flag2)
				{
					obj3 = obj;
				}
				bool flag3 = (object)obj3 == null;
				obj4 = obj;
				typeFromHandle = (NullReferenceException)(object)typeof(Action);
				if (!flag3)
				{
					return;
				}
				goto IL_0190;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			obj4 = obj;
		}
		typeFromHandle = new NullReferenceException();
		goto IL_0190;
		IL_0190:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	protected virtual void OnDisable()
	{
		//IL_000a: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ r8_v3 (Il2CppClass<Lofelt.NiceVibrations.ContinuousHapticsDemoManager>)+200]");
		Action action = new Action(this, (IntPtr)0);
		NullReferenceException typeFromHandle;
		if ((object)this != null)
		{
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ r8_v3 (Il2CppClass<Lofelt.NiceVibrations.ContinuousHapticsDemoManager>)+200]");
			action._002Ector(this, (IntPtr)0);
			Delegate obj = Delegate.Remove(HapticController.PlaybackStopped, action);
			if ((object)obj == null)
			{
				HapticController.PlaybackStopped = null;
				return;
			}
			bool flag = (object)obj.GetType() != typeof(Action);
			Delegate obj2 = null;
			if (!flag)
			{
				obj2 = obj;
			}
			Delegate obj4;
			if ((object)obj2 != null)
			{
				HapticController.PlaybackStopped = (Action)obj2;
				bool flag2 = (object)obj.GetType() != typeof(Action);
				Delegate obj3 = null;
				if (!flag2)
				{
					obj3 = obj;
				}
				bool flag3 = (object)obj3 == null;
				obj4 = obj;
				typeFromHandle = (NullReferenceException)(object)typeof(Action);
				if (!flag3)
				{
					return;
				}
				goto IL_0190;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			obj4 = obj;
		}
		typeFromHandle = new NullReferenceException();
		goto IL_0190;
		IL_0190:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	public ContinuousHapticsDemoManager()
	{
		Color color = default(Color);
		_continuousButtonOnColor = color;
		_continuousButtonOffColor = color;
		((MonoBehaviour)this)._002Ector();
	}
}
