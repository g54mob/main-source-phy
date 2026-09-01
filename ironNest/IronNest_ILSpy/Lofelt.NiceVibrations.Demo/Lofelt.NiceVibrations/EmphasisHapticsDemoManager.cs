using System.Collections;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.UI;

namespace Lofelt.NiceVibrations;

public class EmphasisHapticsDemoManager : DemoManager
{
	public MMProgressBar AmplitudeProgressBar;

	public MMProgressBar FrequencyProgressBar;

	public HapticCurve TargetCurve;

	public float EmphasisAmplitude = 1f;

	public float EmphasisFrequency = 1f;

	public Text EmphasisAmplitudeText;

	public Text EmphasisFrequencyText;

	protected virtual void Start()
	{
		FrequencyProgressBar.UpdateBar(1f, 0f, 1f);
		AmplitudeProgressBar.UpdateBar(1f, 0f, 1f);
		TargetCurve.UpdateCurve(EmphasisAmplitude, EmphasisFrequency);
		HapticController._fallbackPreset = HapticPatterns.PresetType.RigidImpact;
	}

	public virtual void UpdateEmphasisAmplitude(float newAmplitude)
	{
		EmphasisAmplitude = newAmplitude;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		float num = 10f * newAmplitude;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		float num2 = default(float);
		string text = num2.ToString();
		EmphasisAmplitudeText.text = text;
		AmplitudeProgressBar.UpdateBar(EmphasisAmplitude, 0f, 1f);
		TargetCurve.UpdateCurve(EmphasisAmplitude, EmphasisFrequency);
	}

	public virtual void UpdateEmphasisFrequency(float newFrequency)
	{
		EmphasisFrequency = newFrequency;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		float num = 10f * newFrequency;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		float num2 = default(float);
		string text = num2.ToString();
		EmphasisFrequencyText.text = text;
		FrequencyProgressBar.UpdateBar(EmphasisFrequency, 0f, 1f);
		TargetCurve.UpdateCurve(EmphasisAmplitude, EmphasisFrequency);
	}

	public virtual void EmphasisHapticsButton()
	{
		HapticPatterns.PlayEmphasis(EmphasisAmplitude, EmphasisFrequency);
		IEnumerator routine = Logo.Shake(0.2f);
		Coroutine coroutine = StartCoroutine(routine);
		DebugAudioEmphasis.volume = EmphasisAmplitude;
		float num = EmphasisFrequency * 0.5f;
		float pitch = num + 0.5f;
		DebugAudioEmphasis.pitch = pitch;
		DebugAudioEmphasis.Play();
	}
}
