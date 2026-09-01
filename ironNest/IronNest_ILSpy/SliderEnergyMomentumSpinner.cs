using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public sealed class SliderEnergyMomentumSpinner : MonoBehaviour
{
	public enum DecayMode
	{
		ExponentialHalfLife,
		ConstantDrain
	}

	public enum GaugeOutputMode
	{
		Energy,
		Normalized01,
		Percent0100
	}

	private LinearSliderInteractable sliderSource;

	private Transform spinTarget;

	private float fixedStepSeconds;

	private int maxStepsPerFrame;

	private bool skipSimulationWhenIdle;

	private bool requireDraggingToAddEnergy;

	private float minPositiveDeltaValue;

	private float energyPerValue;

	private float neutralPullSpeedValuePerSecond;

	private AnimationCurve speedMultiplierCurve;

	private float minSpeedMultiplier;

	private float maxSpeedMultiplier;

	private float maxEnergy;

	private DecayMode decayMode;

	private float halfLifeSeconds;

	private float constantDrainPerSecond;

	private bool quantizeEnergy;

	private int energyDecimalPlaces;

	private float snapZeroAtOrBelow;

	private bool enableEnergyRangeEvent;

	private float energyRangeMin;

	private float energyRangeMax;

	public UnityEvent<float> OnEnterEnergyRange;

	private Vector3 localSpinAxis;

	private float visualEnergyMin;

	private float visualEnergyMax;

	private float minVisualAngularSpeedDegPerSec;

	private float maxVisualAngularSpeedDegPerSec;

	private AnimationCurve visualSpeedCurve;

	private bool smoothVisualSpeed;

	private float visualSpeedSmoothTimeSeconds;

	private GaugeOutputMode gaugeOutputMode;

	private float gaugeEnergyMin;

	private float gaugeEnergyMax;

	private float energy;

	private float visualAngularSpeedDegPerSec;

	private float previousFrameSliderValue;

	private bool _initialized;

	private float _accumulatedTime;

	private float _framePositiveDeltaRemaining;

	private float _frameDtForDeltaDistribution;

	private bool _rangeInitialized;

	private bool _wasInRangeLastStep;

	public float CurrentValue
	{
		get
		{
			//IL_002f: Expected O, but got I4
			bool flag = gaugeOutputMode == GaugeOutputMode.Energy;
			if (!flag)
			{
				object obj = gaugeOutputMode - 1;
				if (!flag)
				{
					if ((nint)obj == 1)
					{
						float energyNormalized = EnergyNormalized;
						return energyNormalized * 100f;
					}
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 34 Invalid \"Jump target not found in method: 0x1804A1390\"");
				}
			}
			return energy;
		}
	}

	public float Energy => energy;

	public float EnergyNormalized
	{
		get
		{
			//IL_0115: Expected F4, but got I4
			//IL_00ae: Invalid comparison between I4 and F4
			float num = gaugeEnergyMax;
			bool flag = !(gaugeEnergyMin > gaugeEnergyMax);
			float num2 = gaugeEnergyMin;
			if (!flag)
			{
				num2 = gaugeEnergyMax;
				num = gaugeEnergyMin;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj = default(object);
			float num5;
			if (obj == null)
			{
				bool flag2 = num2 == num;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001804A13E2h\"");
				if (!flag2)
				{
					float num3 = energy - num2;
					float num4 = num - num2;
					num5 = num3 / num4;
					if (!(0f > num5))
					{
						if (num5 > 1f)
						{
							return 1f;
						}
						goto IL_0144;
					}
				}
			}
			else if (!(energy < num))
			{
				return 1f;
			}
			num5 = 0f;
			goto IL_0144;
			IL_0144:
			return num5;
		}
	}

	public float EnergyPercent
	{
		get
		{
			float energyNormalized = EnergyNormalized;
			return energyNormalized * 100f;
		}
	}

	public float VisualAngularSpeedDegPerSec => visualAngularSpeedDegPerSec;

	private void Awake()
	{
		//IL_0075: Expected O, but got I
		//IL_0092: Expected O, but got I
		//IL_00b5: Invalid comparison between F4 and O
		//IL_026b: Expected I, but got O
		if (spinTarget == null)
		{
			Transform transform = base.transform;
			spinTarget = transform;
		}
		object obj = (object)localSpinAxis * (object)localSpinAxis;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SliderEnergyMomentumSpinner)+8C]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SliderEnergyMomentumSpinner)+8C]");
		object obj2 = num * 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SliderEnergyMomentumSpinner)+90]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SliderEnergyMomentumSpinner)+90]");
		object obj3 = num2 * 0;
		object obj4 = obj + obj2;
		object obj5 = obj4 + obj3;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-06f) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5))
		{
			nint num3 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v132 @ rax_v20 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num4 = 0;
			localSpinAxis = Vector3.upVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rcx_v16 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
			_ = 0;
		}
		if (speedMultiplierCurve == null || speedMultiplierCurve.length == 0)
		{
			AnimationCurve animationCurve = AnimationCurve.Linear(0f, 0f, 2f, 2f);
			speedMultiplierCurve = animationCurve;
		}
		if (visualSpeedCurve == null || visualSpeedCurve.length == 0)
		{
			AnimationCurve animationCurve2 = AnimationCurve.Linear(0f, 0f, 1f, 1f);
			visualSpeedCurve = animationCurve2;
		}
		InitializeSliderSampling();
		float num5 = energyRangeMax;
		_rangeInitialized = true;
		bool flag = !(energyRangeMin > energyRangeMax);
		float num6 = energyRangeMin;
		if (!flag)
		{
			num6 = energyRangeMax;
			num5 = energyRangeMin;
		}
		if (energy < num6)
		{
			_wasInRangeLastStep = false;
			return;
		}
		bool flag2 = num5 < energy;
		bool wasInRangeLastStep = !flag2;
		_wasInRangeLastStep = wasInRangeLastStep;
	}

	private void OnEnable()
	{
		InitializeSliderSampling();
		float num = energyRangeMin;
		_rangeInitialized = true;
		bool flag = !(energyRangeMin > energyRangeMax);
		float num2 = energyRangeMax;
		if (!flag)
		{
			num2 = energyRangeMin;
			num = energyRangeMax;
		}
		bool wasInRangeLastStep;
		if (energy < num)
		{
			wasInRangeLastStep = false;
		}
		else
		{
			bool flag2 = num2 < energy;
			wasInRangeLastStep = !flag2;
		}
		_wasInRangeLastStep = wasInRangeLastStep;
		_accumulatedTime = 0f;
		_frameDtForDeltaDistribution = 0f;
	}

	private void Update()
	{
		//IL_0058: Invalid comparison between I4 and F4
		//IL_010c: Invalid comparison between I4 and F4
		//IL_012c: Expected F4, but got I4
		//IL_0372: Expected O, but got I4
		//IL_037b: Expected O, but got I4
		//IL_019e: Invalid comparison between F4 and I4
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Expected O, but got Unknown
		//IL_01bd: Invalid comparison between I4 and F4
		if (!(sliderSource != null) || !(spinTarget != null))
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		if (!(0f < deltaTime))
		{
			return;
		}
		LinearSliderInteractable linearSliderInteractable = sliderSource;
		if (_initialized)
		{
			bool flag = !requireDraggingToAddEnergy;
			float num = linearSliderInteractable.accumulatedValue - previousFrameSliderValue;
			previousFrameSliderValue = linearSliderInteractable.accumulatedValue;
			if ((!flag && !linearSliderInteractable.isDragging) || 0f > num)
			{
				num = 0f;
			}
			bool flag2 = !skipSimulationWhenIdle;
			float framePositiveDeltaRemaining = num + _framePositiveDeltaRemaining;
			_frameDtForDeltaDistribution = deltaTime;
			_framePositiveDeltaRemaining = framePositiveDeltaRemaining;
			if (!flag2)
			{
				if (sliderSource != null)
				{
					LinearSliderInteractable linearSliderInteractable2 = sliderSource;
					if (linearSliderInteractable2.isDragging)
					{
						goto IL_01dc;
					}
				}
				if (!(_framePositiveDeltaRemaining > 0f) && !(0f < energy))
				{
					goto IL_0327;
				}
			}
			goto IL_01dc;
		}
		previousFrameSliderValue = linearSliderInteractable.accumulatedValue;
		_initialized = true;
		return;
		IL_0327:
		UpdateVisualSpeed(deltaTime);
		ApplyRotation(deltaTime);
		return;
		IL_028b:
		float num2;
		if (!(_accumulatedTime < num2))
		{
			_accumulatedTime = 0f;
		}
		goto IL_0327;
		IL_026d:
		object obj;
		if ((nint)obj >= maxStepsPerFrame)
		{
			goto IL_028b;
		}
		goto IL_0327;
		IL_01dc:
		bool flag3 = 0.001f > fixedStepSeconds;
		num2 = 0.001f;
		if (!flag3)
		{
			num2 = fixedStepSeconds;
		}
		bool flag4 = (_accumulatedTime = deltaTime + _accumulatedTime) < num2;
		obj = 0;
		object obj2 = 0;
		if (flag4)
		{
			goto IL_026d;
		}
		while ((nint)obj2 < maxStepsPerFrame)
		{
			SimulateStep(num2);
			obj = obj2 + 1;
			bool flag5 = !((_accumulatedTime -= num2) < num2);
			obj2 = obj;
			if (flag5)
			{
				continue;
			}
			goto IL_026d;
		}
		goto IL_028b;
	}

	public void StopAndReset()
	{
		//IL_0099: Invalid comparison between I4 and F4
		//IL_007a: Invalid comparison between F4 and I4
		float num = energyRangeMin;
		energy = 0f;
		_accumulatedTime = 0f;
		_rangeInitialized = true;
		bool flag = !(energyRangeMin > energyRangeMax);
		float num2 = energyRangeMax;
		if (!flag)
		{
			num2 = energyRangeMin;
			num = energyRangeMax;
		}
		bool flag2 = 0f < num;
		bool wasInRangeLastStep = false;
		if (!flag2)
		{
			bool flag3 = num2 < 0f;
			wasInRangeLastStep = !flag3;
		}
		_wasInRangeLastStep = wasInRangeLastStep;
	}

	public void AddEnergy(float deltaEnergy)
	{
		//IL_000b: Invalid comparison between I4 and F4
		//IL_001d: Expected F4, but got I4
		//IL_008e: Invalid comparison between I4 and F4
		//IL_006f: Expected F4, but got I4
		bool flag = !(0f < maxEnergy);
		float num = 0f;
		if (!flag)
		{
			num = maxEnergy;
		}
		float num2 = deltaEnergy + energy;
		if (!(0f > num2))
		{
			if (num2 > num)
			{
				num2 = num;
			}
		}
		else
		{
			num2 = 0f;
		}
		energy = num2;
		QuantizeAndSnapEnergy();
	}

	public unsafe void ForceEvaluateRangeNow()
	{
		//IL_00d1: Expected F4, but got Ref
		float num = energyRangeMin;
		bool flag = !(energyRangeMin > energyRangeMax);
		float num2 = energyRangeMax;
		if (!flag)
		{
			num2 = energyRangeMin;
			num = energyRangeMax;
		}
		bool flag2;
		if (energy < num)
		{
			flag2 = false;
		}
		else
		{
			bool flag3 = num2 < energy;
			flag2 = !flag3;
		}
		if (_rangeInitialized)
		{
			bool flag4 = _wasInRangeLastStep;
			bool flag5 = false;
			if (!flag4)
			{
				flag5 = flag2;
			}
			if (flag5 && enableEnergyRangeEvent && OnEnterEnergyRange != null)
			{
				object obj = default(object);
				OnEnterEnergyRange.Invoke((nint)(&obj));
				_wasInRangeLastStep = flag2;
				return;
			}
		}
		else
		{
			_rangeInitialized = true;
		}
		_wasInRangeLastStep = flag2;
	}

	private bool IsIdle()
	{
		//IL_0087: Invalid comparison between F4 and I4
		//IL_00a6: Invalid comparison between I4 and F4
		//IL_00d0: Expected I4, but got O
		if (sliderSource != null)
		{
			LinearSliderInteractable linearSliderInteractable = sliderSource;
			if ((object)sliderSource == null)
			{
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			if (linearSliderInteractable.isDragging)
			{
				goto IL_00bc;
			}
		}
		if (!(_framePositiveDeltaRemaining > 0f))
		{
			bool flag = 0f < energy;
			return !flag;
		}
		goto IL_00bc;
		IL_00bc:
		return false;
	}

	private void InitializeSliderSampling()
	{
		if (sliderSource != null)
		{
			LinearSliderInteractable linearSliderInteractable = sliderSource;
			previousFrameSliderValue = linearSliderInteractable.accumulatedValue;
			_initialized = true;
		}
		else
		{
			_initialized = false;
			previousFrameSliderValue = 0f;
		}
	}

	private unsafe void SimulateStep(float dt)
	{
		//IL_000b: Invalid comparison between F4 and I4
		//IL_001d: Expected F4, but got I4
		//IL_06b4: Invalid comparison between I4 and F4
		//IL_06c6: Expected F4, but got I4
		//IL_0036: Invalid comparison between F4 and I4
		//IL_0048: Expected F4, but got I4
		//IL_0303: Invalid comparison between I4 and F4
		//IL_0315: Expected F4, but got I4
		//IL_04ce: Invalid comparison between I4 and F4
		//IL_04e0: Expected F4, but got I4
		//IL_0464: Invalid comparison between I4 and F4
		//IL_0602: Invalid comparison between I4 and F4
		//IL_0614: Expected F4, but got I4
		//IL_06df: Invalid comparison between I4 and F4
		//IL_06f1: Expected F4, but got I4
		//IL_015b: Expected O, but got I4
		//IL_00e9: Expected F4, but got I4
		//IL_01b5: Expected O, but got I4
		//IL_0540: Invalid comparison between F4 and I4
		//IL_0233: Invalid comparison between I4 and F4
		//IL_0245: Expected F4, but got I4
		//IL_0571: Invalid comparison between I4 and F4
		//IL_0297: Expected F4, but got I4
		//IL_0423: Expected F4, but got Ref
		bool flag = !(_framePositiveDeltaRemaining > 0f);
		float num = 0f;
		if (!flag)
		{
			bool flag2 = !(_frameDtForDeltaDistribution > 0f);
			num = 0f;
			if (!flag2)
			{
				num = _framePositiveDeltaRemaining;
				float num2 = dt / _frameDtForDeltaDistribution;
				float num3 = num2 * _framePositiveDeltaRemaining;
				if (_framePositiveDeltaRemaining > num3)
				{
					num = num3;
				}
				if (!(0f > num))
				{
					if (num > _framePositiveDeltaRemaining)
					{
						num = _framePositiveDeltaRemaining;
					}
				}
				else
				{
					num = 0f;
				}
				float framePositiveDeltaRemaining = _framePositiveDeltaRemaining - num;
				_framePositiveDeltaRemaining = framePositiveDeltaRemaining;
			}
		}
		bool flag3 = num < minPositiveDeltaValue;
		SliderEnergyMomentumSpinner sliderEnergyMomentumSpinner = this;
		if (!flag3)
		{
			bool flag4 = !(0.0001f < neutralPullSpeedValuePerSecond);
			float num4 = 0.0001f;
			if (!flag4)
			{
				num4 = neutralPullSpeedValuePerSecond;
			}
			bool flag5 = speedMultiplierCurve == null;
			float num5 = 1f;
			sliderEnergyMomentumSpinner = this;
			if (!flag5)
			{
				int length = speedMultiplierCurve.length;
				bool flag6 = length <= 0;
				num5 = 1f;
				object obj = 0;
				sliderEnergyMomentumSpinner = (SliderEnergyMomentumSpinner)(object)speedMultiplierCurve;
				if (!flag6)
				{
					float num6 = num / dt;
					float time = num6 / num4;
					float num7 = speedMultiplierCurve.Evaluate(time);
					num5 = num7;
					obj = 0;
					sliderEnergyMomentumSpinner = (SliderEnergyMomentumSpinner)(object)speedMultiplierCurve;
				}
			}
			bool flag7 = !(0f < num5);
			float num8 = 0f;
			if (!flag7)
			{
				num8 = num5;
			}
			bool flag8 = !(0f < minSpeedMultiplier);
			float num9 = 0f;
			if (!flag8)
			{
				num9 = minSpeedMultiplier;
			}
			bool flag9 = !(num9 < maxSpeedMultiplier);
			float num10 = num9;
			if (!flag9)
			{
				num10 = maxSpeedMultiplier;
			}
			if (!(num9 > num8))
			{
				if (num8 > num10)
				{
					num8 = num10;
				}
			}
			else
			{
				num8 = num9;
			}
			float num11 = num * energyPerValue;
			float num12 = num11 * num8;
			if (num12 > 0f)
			{
				bool flag10 = !(0f < maxEnergy);
				float num13 = 0f;
				if (!flag10)
				{
					num13 = maxEnergy;
				}
				float num14 = num12 + energy;
				if (!(0f > num14))
				{
					if (num14 > num13)
					{
						num14 = num13;
					}
				}
				else
				{
					num14 = 0f;
				}
				energy = num14;
			}
		}
		bool flag11 = !(0f < energy);
		float num15 = 0f;
		if (!flag11)
		{
			if (decayMode == DecayMode.ExponentialHalfLife)
			{
				bool flag12 = !(0.0001f < halfLifeSeconds);
				float num16 = 0.0001f;
				if (!flag12)
				{
					num16 = halfLifeSeconds;
				}
				float num17 = dt / num16;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
				num15 = 0.5f * energy;
			}
			else
			{
				bool flag13 = !(0f < constantDrainPerSecond);
				float num18 = 0f;
				if (!flag13)
				{
					num18 = constantDrainPerSecond;
				}
				float num19 = num18 * dt;
				float num20 = energy - num19;
				bool flag14 = !(0f < num20);
				num15 = 0f;
				if (!flag14)
				{
					num15 = num20;
				}
			}
		}
		energy = num15;
		QuantizeAndSnapEnergy();
		if (!enableEnergyRangeEvent)
		{
			return;
		}
		float num21 = energyRangeMin;
		bool flag15 = !(energyRangeMin > energyRangeMax);
		float num22 = energyRangeMax;
		if (!flag15)
		{
			num22 = energyRangeMin;
			num21 = energyRangeMax;
		}
		bool flag16;
		if (energy < num21)
		{
			flag16 = false;
		}
		else
		{
			bool flag17 = num22 < energy;
			flag16 = !flag17;
		}
		if (_rangeInitialized)
		{
			bool flag18 = _wasInRangeLastStep;
			bool flag19 = false;
			if (!flag18)
			{
				flag19 = flag16;
			}
			if (flag19 && OnEnterEnergyRange != null)
			{
				object obj2 = default(object);
				OnEnterEnergyRange.Invoke((nint)(&obj2));
			}
		}
		else
		{
			_rangeInitialized = true;
		}
		_wasInRangeLastStep = flag16;
	}

	private float EvaluateSpeedMultiplier(float pullSpeedValuePerSec)
	{
		//IL_0136: Invalid comparison between I4 and F4
		//IL_0148: Expected F4, but got I4
		//IL_018f: Invalid comparison between I4 and F4
		//IL_01a1: Expected F4, but got I4
		bool flag = !(0.0001f < neutralPullSpeedValuePerSecond);
		float num = 0.0001f;
		if (!flag)
		{
			num = neutralPullSpeedValuePerSecond;
		}
		bool flag2 = speedMultiplierCurve == null;
		float num2 = 1f;
		if (!flag2)
		{
			int length = speedMultiplierCurve.length;
			bool flag3 = length <= 0;
			num2 = 1f;
			if (!flag3)
			{
				float time = pullSpeedValuePerSec / num;
				float num3 = speedMultiplierCurve.Evaluate(time);
				num2 = num3;
			}
		}
		bool flag4 = !(0f < num2);
		float num4 = 0f;
		if (!flag4)
		{
			num4 = num2;
		}
		bool flag5 = !(0f < minSpeedMultiplier);
		float num5 = 0f;
		if (!flag5)
		{
			num5 = minSpeedMultiplier;
		}
		bool flag6 = !(num5 < maxSpeedMultiplier);
		float num6 = num5;
		if (!flag6)
		{
			num6 = maxSpeedMultiplier;
		}
		if (!(num5 > num4))
		{
			if (num4 > num6)
			{
				num4 = num6;
			}
		}
		else
		{
			num4 = num5;
		}
		return num4;
	}

	private void ApplyDecay(float dt)
	{
		//IL_000b: Invalid comparison between I4 and F4
		//IL_001d: Expected F4, but got I4
		//IL_0092: Invalid comparison between I4 and F4
		//IL_00a4: Expected F4, but got I4
		//IL_0138: Invalid comparison between I4 and F4
		//IL_014a: Expected F4, but got I4
		bool flag = !(0f < energy);
		float num = 0f;
		if (!flag)
		{
			if (decayMode == DecayMode.ExponentialHalfLife)
			{
				bool flag2 = !(0.0001f < halfLifeSeconds);
				float num2 = 0.0001f;
				if (!flag2)
				{
					num2 = halfLifeSeconds;
				}
				float num3 = dt / num2;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
				float num4 = 0.5f * energy;
				energy = num4;
				return;
			}
			bool flag3 = !(0f < constantDrainPerSecond);
			float num5 = 0f;
			if (!flag3)
			{
				num5 = constantDrainPerSecond;
			}
			float num6 = num5 * dt;
			float num7 = energy - num6;
			bool flag4 = !(0f < num7);
			num = 0f;
			if (!flag4)
			{
				num = num7;
			}
		}
		energy = num;
	}

	private void QuantizeAndSnapEnergy()
	{
		//IL_0230: Invalid comparison between I4 and F4
		//IL_028d: Invalid comparison between F4 and I4
		//IL_013f: Invalid comparison between F4 and I4
		//IL_00a1: Invalid comparison between F4 and I4
		//IL_01ea: Invalid comparison between I4 and F4
		//IL_01fc: Expected F4, but got I4
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		if (quantizeEnergy)
		{
			int num = energyDecimalPlaces;
			if (energyDecimalPlaces >= 0)
			{
				if (num > 4)
				{
					num = 4;
				}
			}
			else
			{
				num = 0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
			float num2 = 10f * energy;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EEB0");
			float num3 = default(float);
			float num4;
			if (!(num2 < 0f))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,qword ptr [182206D18h]\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001804A0A6Dh\"");
				if (num2 == 0f)
				{
					object obj = num3 & 1;
					bool flag = obj == null;
					num4 = num3;
					if (!flag)
					{
						num4 = num3 + 1f;
					}
				}
				else
				{
					float x = num2 + 0.5f;
					num4 = MathF.Floor(x);
				}
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,qword ptr [182206D70h]\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001804A0AA8h\"");
				if (num2 == 0f)
				{
					object obj2 = num3 & 1;
					bool flag2 = obj2 == null;
					num4 = num3;
					if (!flag2)
					{
						num4 = num3 - 1f;
					}
				}
				else
				{
					float num5 = num2 - 0.5f;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F1C0");
					num4 = num5;
				}
			}
			float num6 = (energy = num4 / 10f);
			bool flag3 = !(0f < snapZeroAtOrBelow);
			float num7 = 0f;
			if (!flag3)
			{
				num7 = snapZeroAtOrBelow;
			}
			if (!(num7 < num6))
			{
				energy = 0f;
			}
		}
		else if (!(0f < energy))
		{
			energy = 0f;
		}
	}

	private void UpdateVisualSpeed(float dtFrame)
	{
		//IL_007a: Expected F4, but got I4
		//IL_0311: Expected O, but got I4
		//IL_0157: Invalid comparison between I4 and F4
		//IL_02e3: Invalid comparison between I4 and F4
		//IL_01e7: Expected F4, but got I4
		//IL_00a5: Invalid comparison between I4 and F4
		//IL_01a2: Expected O, but got I4
		//IL_01ab: Expected F4, but got I4
		//IL_00d4: Expected O, but got I4
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Expected O, but got Unknown
		//IL_03a8: Invalid comparison between I4 and F4
		//IL_00eb: Expected O, but got I4
		//IL_0272: Expected F4, but got I4
		float num = visualEnergyMax;
		bool flag = !(visualEnergyMin > visualEnergyMax);
		float num2 = visualEnergyMin;
		if (!flag)
		{
			num2 = visualEnergyMax;
			num = visualEnergyMin;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj = default(object);
		float num5;
		if (obj != null)
		{
			if (!(energy < num))
			{
				goto IL_018b;
			}
		}
		else
		{
			bool flag2 = num2 == num;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001804A0ED0h\"");
			if (!flag2)
			{
				float num3 = energy - num2;
				float num4 = num - num2;
				num5 = num3 / num4;
				if (!(0f > num5))
				{
					if (num5 > 1f)
					{
						goto IL_018b;
					}
					goto IL_02f7;
				}
			}
		}
		num5 = 0f;
		goto IL_02f7;
		IL_02f7:
		bool flag3 = visualSpeedCurve == null;
		object obj2 = 0;
		AnimationCurve animationCurve = (AnimationCurve)(object)this;
		if (!flag3)
		{
			animationCurve = visualSpeedCurve;
			num5 = visualSpeedCurve.Evaluate(num5);
			if (!(0f > num5))
			{
				bool flag4 = !(num5 > 1f);
				obj2 = 0;
				if (!flag4)
				{
					obj2 = 0;
					num5 = 1f;
				}
			}
			else
			{
				obj2 = 0;
				num5 = 0f;
			}
		}
		if (!(0f > num5))
		{
			if (num5 > 1f)
			{
				num5 = 1f;
			}
		}
		else
		{
			num5 = 0f;
		}
		bool flag5 = !smoothVisualSpeed;
		float num6 = maxVisualAngularSpeedDegPerSec - minVisualAngularSpeedDegPerSec;
		float num7 = num6 * num5;
		float num8 = num7 + minVisualAngularSpeedDegPerSec;
		if (!flag5)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj3 = dtFrame ^ 0;
			bool flag6 = !(0.001f < visualSpeedSmoothTimeSeconds);
			float num9 = 0.001f;
			if (!flag6)
			{
				num9 = visualSpeedSmoothTimeSeconds;
			}
			float num10 = (float)obj3 / num9;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F3D0");
			float num11 = 1f - num10;
			if (!(0f > num11))
			{
				if (num11 > 1f)
				{
					num11 = 1f;
				}
			}
			else
			{
				num11 = 0f;
			}
			float num12 = num8 - visualAngularSpeedDegPerSec;
			float num13 = num12 * num11;
			if (0.01f > (visualAngularSpeedDegPerSec = num13 + visualAngularSpeedDegPerSec) && !(0.01f < num8))
			{
				visualAngularSpeedDegPerSec = 0f;
			}
		}
		else
		{
			visualAngularSpeedDegPerSec = num8;
		}
		return;
		IL_018b:
		num5 = 1f;
		goto IL_02f7;
	}

	private unsafe void ApplyRotation(float dtFrame)
	{
		//IL_015e: Expected I, but got O
		//IL_004c: Expected O, but got I
		//IL_007a: Expected O, but got I
		//IL_00fa: Invalid comparison between I4 and F4
		//IL_0145: Expected O, but got Ref
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj = default(object);
		if (obj == null)
		{
			nint num = (nint)typeof(Math);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SliderEnergyMomentumSpinner)+8C]");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SliderEnergyMomentumSpinner)+8C]");
			object obj2 = num2 * 0;
			object obj3 = (object)localSpinAxis * (object)localSpinAxis;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SliderEnergyMomentumSpinner)+90]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SliderEnergyMomentumSpinner)+90]");
			object obj4 = num3 * 0;
			object obj5 = obj2 + obj3;
			double d = (double)obj5 + (double)obj4;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v204 @ rcx_v4 (Il2CppClass<System.Math>)+E4]");
			if ((nint)0 <= (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
			}
			else
			{
				double num4 = Math.Sqrt(d);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm3,xmm0\"");
			if (0f > 1E-05f)
			{
			}
			float angle = dtFrame * visualAngularSpeedDegPerSec;
			Vector3 vector = default(Vector3);
			spinTarget.Rotate((Vector3)(&vector), angle, Space.Self);
		}
	}

	private void InitializeRangeState()
	{
		float num = energyRangeMin;
		_rangeInitialized = true;
		bool flag = !(energyRangeMin > energyRangeMax);
		float num2 = energyRangeMax;
		if (!flag)
		{
			num2 = energyRangeMin;
			num = energyRangeMax;
		}
		if (energy < num)
		{
			_wasInRangeLastStep = false;
			return;
		}
		bool flag2 = num2 < energy;
		bool wasInRangeLastStep = !flag2;
		_wasInRangeLastStep = wasInRangeLastStep;
	}

	private unsafe void EvaluateEnergyRangeEntry()
	{
		//IL_00e9: Expected F4, but got Ref
		if (!enableEnergyRangeEvent)
		{
			return;
		}
		float num = energyRangeMin;
		bool flag = !(energyRangeMin > energyRangeMax);
		float num2 = energyRangeMax;
		if (!flag)
		{
			num2 = energyRangeMin;
			num = energyRangeMax;
		}
		bool flag2;
		if (energy < num)
		{
			flag2 = false;
		}
		else
		{
			bool flag3 = num2 < energy;
			flag2 = !flag3;
		}
		if (_rangeInitialized)
		{
			bool flag4 = _wasInRangeLastStep;
			bool flag5 = false;
			if (!flag4)
			{
				flag5 = flag2;
			}
			if (flag5 && OnEnterEnergyRange != null)
			{
				object obj = default(object);
				OnEnterEnergyRange.Invoke((nint)(&obj));
				_wasInRangeLastStep = flag2;
				return;
			}
		}
		else
		{
			_rangeInitialized = true;
		}
		_wasInRangeLastStep = flag2;
	}

	private bool IsEnergyInConfiguredRange(float e)
	{
		float num = energyRangeMin;
		bool flag = !(energyRangeMin > energyRangeMax);
		float num2 = energyRangeMax;
		if (!flag)
		{
			num2 = energyRangeMin;
			num = energyRangeMax;
		}
		if (e < num)
		{
			return false;
		}
		bool flag2 = num2 < e;
		return !flag2;
	}

	public SliderEnergyMomentumSpinner()
	{
		//IL_00ce: Expected I, but got O
		fixedStepSeconds = 1f / 60f;
		maxStepsPerFrame = 10;
		skipSimulationWhenIdle = true;
		minPositiveDeltaValue = 0.02f;
		energyPerValue = 1.2f;
		neutralPullSpeedValuePerSecond = 117.65f;
		minSpeedMultiplier = 0.1f;
		maxSpeedMultiplier = 2.5f;
		maxEnergy = 200f;
		halfLifeSeconds = 0.75f;
		constantDrainPerSecond = 25f;
		quantizeEnergy = true;
		energyDecimalPlaces = 2;
		snapZeroAtOrBelow = 0.01f;
		enableEnergyRangeEvent = true;
		energyRangeMin = 120f;
		energyRangeMax = 99999f;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		localSpinAxis = Vector3.upVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rcx_v2 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
		_ = 0;
		visualEnergyMax = 120f;
		maxVisualAngularSpeedDegPerSec = 2200f;
		smoothVisualSpeed = true;
		visualSpeedSmoothTimeSeconds = 0.08f;
		gaugeEnergyMax = 120f;
		base._002Ector();
	}
}
