using Cpp2ILInjected;
using UnityEngine;

public class PressureParticleEmissionBridge : MonoBehaviour
{
	private EspressoBrewingController brewingController;

	private ParticleSystem targetParticleSystem;

	private float pressureMin;

	private float pressureMax = 15f;

	private float minEmissionRate;

	private float maxEmissionRate = 50f;

	private float minStartSize = 0.05f;

	private float maxStartSize = 0.25f;

	private float emissionSmoothTime = 0.15f;

	private float sizeSmoothTime = 0.15f;

	private bool debugLogs;

	private ParticleSystem.EmissionModule _emission;

	private ParticleSystem.MainModule _main;

	private float _currentEmissionRate;

	private float _currentStartSize;

	private float _emissionVelocity;

	private float _sizeVelocity;

	private bool _isReady;

	private void Awake()
	{
		object message;
		if (brewingController != null)
		{
			if (targetParticleSystem != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049AB00");
				ParticleSystem.EmissionModule emission = default(ParticleSystem.EmissionModule);
				_emission = emission;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049AB00");
				ParticleSystem.MainModule main = default(ParticleSystem.MainModule);
				_main = main;
				_currentEmissionRate = minEmissionRate;
				_currentStartSize = minStartSize;
				_isReady = true;
				return;
			}
			message = "[PressureParticleEmissionBridge] targetParticleSystem is not assigned.";
		}
		else
		{
			message = "[PressureParticleEmissionBridge] brewingController is not assigned.";
		}
		Debug.LogWarning(message, this);
	}

	private unsafe void Update()
	{
		//IL_0365: Invalid comparison between I4 and F4
		//IL_00aa: Expected F4, but got I4
		//IL_0382: Invalid comparison between I4 and F4
		//IL_00f0: Expected F4, but got I4
		//IL_03d2: Invalid comparison between I4 and F4
		//IL_0131: Expected F4, but got I4
		//IL_0424: Invalid comparison between F4 and I4
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected Ref, but got Unknown
		//IL_018f: Invalid comparison between F4 and I4
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Expected Ref, but got Unknown
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Expected O, but got Unknown
		//IL_0219: Expected O, but got Ref
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Expected O, but got Unknown
		//IL_0245: Expected O, but got Ref
		if (!_isReady)
		{
			return;
		}
		EspressoBrewingController espressoBrewingController = brewingController;
		float num = pressureMax - pressureMin;
		float num2 = espressoBrewingController.simPressure - pressureMin;
		bool flag = !(0.001f < num);
		float num3 = 0.001f;
		if (!flag)
		{
			num3 = num;
		}
		float num4 = num2 / num3;
		if (!(0f > num4))
		{
			if (num4 > 1f)
			{
				num4 = 1f;
			}
		}
		else
		{
			num4 = 0f;
		}
		float num5 = ((0f > num4) ? 0f : ((num4 > 1f) ? 1f : num4));
		float num6 = maxEmissionRate - minEmissionRate;
		float num7 = num6 * num5;
		float num8 = num7 + minEmissionRate;
		float num9;
		if (!(0f > num4))
		{
			bool flag2 = num4 > 1f;
			num9 = 1f;
			if (!flag2)
			{
				num9 = num4;
			}
		}
		else
		{
			num9 = 0f;
		}
		float num10 = maxStartSize - minStartSize;
		float num11 = num10 * num9;
		float num12 = num11 + minStartSize;
		float currentEmissionRate;
		if (!(emissionSmoothTime > 0f))
		{
			currentEmissionRate = num8;
		}
		else
		{
			currentEmissionRate = Mathf.SmoothDamp(_currentEmissionRate, num8, ref *(float*)(this + 112), emissionSmoothTime);
			float num13 = emissionSmoothTime;
		}
		_currentEmissionRate = currentEmissionRate;
		float currentStartSize;
		if (!(sizeSmoothTime > 0f))
		{
			currentStartSize = num12;
		}
		else
		{
			currentStartSize = Mathf.SmoothDamp(_currentStartSize, num12, ref *(float*)(this + 116), sizeSmoothTime);
			float num13 = sizeSmoothTime;
		}
		_currentStartSize = currentStartSize;
		ParticleSystem.MinMaxCurve minMaxCurve = _currentEmissionRate;
		ParticleSystem.EmissionModule emissionModule = (ParticleSystem.EmissionModule)(this + 88);
		ParticleSystemCurveMode particleSystemCurveMode = default(ParticleSystemCurveMode);
		((ParticleSystem.EmissionModule*)emissionModule)->rateOverTime = (ParticleSystem.MinMaxCurve)(&particleSystemCurveMode);
		ParticleSystem.MinMaxCurve minMaxCurve2 = _currentStartSize;
		ParticleSystem.MainModule mainModule = (ParticleSystem.MainModule)(this + 96);
		((ParticleSystem.MainModule*)mainModule)->startSize = (ParticleSystem.MinMaxCurve)(&particleSystemCurveMode);
		if (debugLogs)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			object arg2 = default(object);
			string text = $"pressure={arg:F2}  t={arg2:F3}  ";
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg3 = default(object);
			object arg4 = default(object);
			string text2 = $"emission={arg3:F2} (target {arg4:F2})  ";
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg5 = default(object);
			object arg6 = default(object);
			string text3 = $"size={arg5:F4} (target {arg6:F4})";
			string message = "[PressureParticleEmissionBridge] " + text + text2 + text3;
			Debug.Log(message, this);
		}
	}
}
