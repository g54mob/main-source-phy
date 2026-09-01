using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class EspressoBrewingController : MonoBehaviour
{
	public enum BrewState
	{
		Idle,
		Ready,
		Brewing,
		Complete
	}

	private ItemSlot groundsSlot;

	private ItemSlot cupSlot;

	private DialInteractable temperatureDial;

	private DialInteractable pressureDial;

	private LookAtTarget brewButton;

	private DialGaugeDisplay temperatureGaugeDisplay;

	private DialGaugeDisplay pressureGaugeDisplay;

	private DialGaugeDisplay brewTimerDial;

	private float timerDialSecondsPerRevolution = 60f;

	private float tempDecayRateCold = 2.5f;

	private float tempDecayRateWarmed = 0.8f;

	private float thermalWarmupDuration = 20f;

	private float tempInputScale = 25f;

	private float tempMax = 120f;

	private float pressureDecayRate = 2f;

	private float pressureInputScale = 18f;

	private float pressureMax = 15f;

	private float tempToPressureCoupling = 0.05f;

	private float pressureToTempCoupling = 0.02f;

	private float tempNoiseMagnitude = 0.3f;

	private float pressureNoiseMagnitude = 0.15f;

	private float noiseScrollSpeed = 0.03f;

	private float tempSpikeChancePerSecond;

	private float pressureSpikeChancePerSecond;

	private float spikeMagnitudeMax;

	private float tempDialMaxOutput = 110f;

	private float pressureDialMaxOutput = 15f;

	private float idealPressure = 9f;

	private float idealTemperature = 93f;

	private float scoringCurvePower = 2f;

	private float pressureWeight = 0.6f;

	private float temperatureWeight = 0.4f;

	private float timingWeight = 0.25f;

	private float idealBrewSeconds = 28f;

	private float timingWindowSeconds = 10f;

	public UnityEvent OnMachineReady;

	public UnityEvent OnMachineUnloaded;

	public UnityEvent<float> OnBrewStarted;

	public UnityEvent<float> OnBrewTick;

	public UnityEvent<float> OnBrewComplete;

	public UnityEvent<EspressoCup> OnCupCollected;

	public UnityEvent<CoffeeGroundsCan> OnCanExhausted;

	public UnityEvent<CoffeeGroundsCan> OnEmptyCanRejected;

	private bool debugLogs;

	private bool debugSimTick;

	private BrewState currentState;

	private float runningDialScore;

	private float finalQuality;

	private CoffeeGroundsCan _loadedCan;

	private EspressoCup _loadedCup;

	private int _sampleCount;

	private double _scoreAccumulator;

	private double _pressureScoreAccumulator;

	private double _temperatureScoreAccumulator;

	private float _simRunningTime;

	private float _tempNoiseOffset;

	private float _pressureNoiseOffset;

	private bool _timerDialFrozen;

	private float simTemperature;

	private float simPressure;

	private float mappedTemperature;

	private float mappedPressure;

	private float brewElapsedSeconds;

	public BrewState CurrentState => currentState;

	public float SimTemperature => simTemperature;

	public float SimPressure => simPressure;

	public float MappedTemperature => mappedTemperature;

	public float MappedPressure => mappedPressure;

	public float IdealTempMapped
	{
		get
		{
			bool flag = !(0.001f < tempMax);
			float num = 0.001f;
			if (!flag)
			{
				num = tempMax;
			}
			return idealTemperature / num;
		}
	}

	public float IdealPressureMapped
	{
		get
		{
			bool flag = !(0.001f < pressureMax);
			float num = 0.001f;
			if (!flag)
			{
				num = pressureMax;
			}
			return idealPressure / num;
		}
	}

	public float BrewElapsedSeconds => brewElapsedSeconds;

	public float FinalQuality => finalQuality;

	private void Awake()
	{
		float tempNoiseOffset = UnityEngine.Random.Range(0f, 1000f);
		_tempNoiseOffset = tempNoiseOffset;
		float pressureNoiseOffset = UnityEngine.Random.Range(0f, 1000f);
		_pressureNoiseOffset = pressureNoiseOffset;
		_simRunningTime = 0f;
		simTemperature = 0f;
		float tempNoiseOffset2 = UnityEngine.Random.Range(0f, 1000f);
		_tempNoiseOffset = tempNoiseOffset2;
		float pressureNoiseOffset2 = UnityEngine.Random.Range(0f, 1000f);
		_pressureNoiseOffset = pressureNoiseOffset2;
		if (brewButton != null)
		{
			brewButton.SetActive(active: false);
		}
		ResetTimerDial();
	}

	private void OnEnable()
	{
		if (groundsSlot != null)
		{
			ItemSlot itemSlot = groundsSlot;
			UnityAction<GameObject> call = HandleGroundsAdded;
			itemSlot.onItemAdded.AddListener(call);
			ItemSlot itemSlot2 = groundsSlot;
			UnityAction<GameObject> call2 = HandleGroundsRemoved;
			itemSlot2.onItemRemoved.AddListener(call2);
		}
		if (cupSlot != null)
		{
			ItemSlot itemSlot3 = cupSlot;
			UnityAction<GameObject> call3 = HandleCupAdded;
			itemSlot3.onItemAdded.AddListener(call3);
			ItemSlot itemSlot4 = cupSlot;
			UnityAction<GameObject> call4 = HandleCupRemoved;
			itemSlot4.onItemRemoved.AddListener(call4);
		}
	}

	private void OnDisable()
	{
		if (groundsSlot != null)
		{
			ItemSlot itemSlot = groundsSlot;
			UnityAction<GameObject> call = HandleGroundsAdded;
			itemSlot.onItemAdded.RemoveListener(call);
			ItemSlot itemSlot2 = groundsSlot;
			UnityAction<GameObject> call2 = HandleGroundsRemoved;
			itemSlot2.onItemRemoved.RemoveListener(call2);
		}
		if (cupSlot != null)
		{
			ItemSlot itemSlot3 = cupSlot;
			UnityAction<GameObject> call3 = HandleCupAdded;
			itemSlot3.onItemAdded.RemoveListener(call3);
			ItemSlot itemSlot4 = cupSlot;
			UnityAction<GameObject> call4 = HandleCupRemoved;
			itemSlot4.onItemRemoved.RemoveListener(call4);
		}
	}

	private unsafe void Update()
	{
		//IL_03a4: Invalid comparison between I4 and F4
		//IL_004b: Expected F4, but got I4
		//IL_04ba: Invalid comparison between I4 and F4
		//IL_0096: Expected F4, but got I4
		//IL_0167: Expected I, but got O
		//IL_022d: Expected F4, but got I4
		//IL_01f5: Expected F4, but got Ref
		//IL_018f: Expected I, but got O
		//IL_046a: Expected I, but got O
		float deltaTime = Time.deltaTime;
		float simRunningTime = deltaTime + _simRunningTime;
		_simRunningTime = simRunningTime;
		StepSimulation(deltaTime);
		bool flag = !(0.001f < tempMax);
		float num = 0.001f;
		if (!flag)
		{
			num = tempMax;
		}
		float num2 = simTemperature / num;
		if (!(0f > num2))
		{
			if (num2 > 1f)
			{
				num2 = 1f;
			}
		}
		else
		{
			num2 = 0f;
		}
		mappedTemperature = num2;
		bool flag2 = !(0.001f < pressureMax);
		float num3 = 0.001f;
		if (!flag2)
		{
			num3 = pressureMax;
		}
		float num4 = simPressure / num3;
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
		mappedPressure = num4;
		if (temperatureGaugeDisplay != null)
		{
			DialGaugeDisplay dialGaugeDisplay = temperatureGaugeDisplay;
			dialGaugeDisplay.targetNumber = mappedTemperature;
		}
		if (pressureGaugeDisplay != null)
		{
			DialGaugeDisplay dialGaugeDisplay2 = pressureGaugeDisplay;
			dialGaugeDisplay2.targetNumber = mappedPressure;
		}
		if (currentState != BrewState.Brewing)
		{
			return;
		}
		float x = (brewElapsedSeconds = deltaTime + brewElapsedSeconds);
		bool flag3 = _timerDialFrozen;
		nint num5 = unchecked((nint)null);
		if (!flag3)
		{
			bool flag4 = brewTimerDial != null;
			num5 = unchecked((nint)null);
			if (flag4)
			{
				DialGaugeDisplay dialGaugeDisplay3 = brewTimerDial;
				bool flag5 = !(1f < timerDialSecondsPerRevolution);
				float num6 = 1f;
				if (!flag5)
				{
					num6 = timerDialSecondsPerRevolution;
				}
				float targetNumber = MathF.FMod(x, num6);
				dialGaugeDisplay3.targetNumber = targetNumber;
				num5 = unchecked((nint)null);
				num = num6;
			}
		}
		if (OnBrewTick != null)
		{
			float num7 = default(float);
			OnBrewTick.Invoke((nint)(&num7));
			num7 = brewElapsedSeconds;
			num5 = 0;
		}
		AccumulateScore();
		bool flag6 = _sampleCount <= 0;
		float num8 = 0f;
		if (!flag6)
		{
			num = (float)_scoreAccumulator;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
			num8 = (float)_scoreAccumulator;
		}
		runningDialScore = num8;
		if (debugSimTick)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string text = $"[BrewTick] t={arg:F2}s  ";
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg2 = default(object);
			object arg3 = default(object);
			string text2 = $"T={arg2:F2}°(m={arg3:F3})  ";
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg4 = default(object);
			object arg5 = default(object);
			string text3 = $"P={arg4:F2}bar(m={arg5:F3})  ";
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg6 = default(object);
			string text4 = $"dialScore={arg6:F3}";
			string message = text + text2 + text3 + text4;
			Debug.Log(message, this);
		}
	}

	private void HandleGroundsAdded(GameObject itemGO)
	{
		//IL_01f4: Expected O, but got I
		//IL_00bc: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		UnityEngine.Object obj = default(UnityEngine.Object);
		bool flag = obj == null;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ stack_-28_v2 (UnityEngine.Object)+30]");
			if ((nint)0 >= (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ stack_-28_v2 (UnityEngine.Object)+34]");
				if ((nint)0 <= (nint)0)
				{
					if (debugLogs != flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ stack_-28_v2 (UnityEngine.Object)+28]");
						object arg = default(object);
						string message = $"[EspressoBrewingController] Empty can '{0}' (remainingUses={arg}) placed in grounds slot — ejecting.";
						Debug.LogWarning(message, this);
					}
					EjectItem(itemGO, groundsSlot);
					if (OnEmptyCanRejected != null)
					{
						OnEmptyCanRejected.Invoke((CoffeeGroundsCan)obj);
					}
					return;
				}
			}
			_loadedCan = (CoffeeGroundsCan)obj;
			CoffeeGroundsCan loadedCan = _loadedCan;
			if (!loadedCan.IsLoaded)
			{
				loadedCan.IsLoaded = true;
				if (loadedCan.OnLoaded != null)
				{
					loadedCan.OnLoaded.Invoke();
				}
			}
			if (debugLogs)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ stack_-28_v2 (UnityEngine.Object)+28]");
				object arg2 = default(object);
				object arg3 = default(object);
				string message2 = $"[EspressoBrewingController] Can loaded: '{0}' quality={arg2:F2} uses={arg3}";
				Debug.Log(message2, this);
			}
			EvaluateReadyState();
			return;
		}
		if (debugLogs)
		{
			Debug.LogWarning("[EspressoBrewingController] Non-can item in grounds slot — ejecting.", this);
		}
		if (groundsSlot != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			UnityEngine.Object obj2 = default(UnityEngine.Object);
			if (obj2 != null)
			{
				groundsSlot.RemoveItem((DraggableItem)obj2, autoEject: true);
			}
		}
	}

	private void HandleGroundsRemoved(GameObject itemGO)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		UnityEngine.Object obj = default(UnityEngine.Object);
		if (!(obj != null) || !(obj == _loadedCan))
		{
			return;
		}
		CoffeeGroundsCan loadedCan = _loadedCan;
		if (loadedCan.IsLoaded)
		{
			loadedCan.IsLoaded = false;
			if (loadedCan.OnUnloaded != null)
			{
				loadedCan.OnUnloaded.Invoke();
			}
		}
		_loadedCan = null;
		if (debugLogs)
		{
			Debug.Log("[EspressoBrewingController] Can removed.", this);
		}
		HandleSlotRemovedDuringActiveState();
	}

	private void HandleCupAdded(GameObject itemGO)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		UnityEngine.Object obj = default(UnityEngine.Object);
		bool flag = obj == null;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ stack_10_v2 (UnityEngine.Object)+20]");
			if ((nint)0 == (flag ? 1 : 0))
			{
				_loadedCup = (EspressoCup)obj;
				if (debugLogs)
				{
					Debug.Log("[EspressoBrewingController] Empty cup loaded.", this);
				}
				EvaluateReadyState();
			}
			else
			{
				if (debugLogs)
				{
					Debug.LogWarning("[EspressoBrewingController] Full cup in cup slot — ejecting.", this);
				}
				EjectItem(itemGO, cupSlot);
			}
			return;
		}
		if (debugLogs)
		{
			Debug.LogWarning("[EspressoBrewingController] Non-cup item in cup slot — ejecting.", this);
		}
		if (cupSlot != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			if (obj != null)
			{
				cupSlot.RemoveItem((DraggableItem)obj, autoEject: true);
			}
		}
	}

	private void HandleCupRemoved(GameObject itemGO)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		UnityEngine.Object obj = default(UnityEngine.Object);
		if (obj != null && obj == _loadedCup)
		{
			_loadedCup = null;
			if (debugLogs)
			{
				Debug.Log("[EspressoBrewingController] Cup removed.", this);
			}
			HandleSlotRemovedDuringActiveState();
		}
	}

	private void EvaluateReadyState()
	{
		if (currentState == BrewState.Brewing || currentState == BrewState.Complete)
		{
			return;
		}
		if (_loadedCan != null && _loadedCup != null)
		{
			if (currentState == BrewState.Idle)
			{
				SetState(BrewState.Ready);
				if (OnMachineReady != null)
				{
					OnMachineReady.Invoke();
				}
				if (debugLogs)
				{
					Debug.Log("[EspressoBrewingController] Machine ready.", this);
				}
			}
		}
		else if (currentState == BrewState.Ready)
		{
			SetState(BrewState.Idle);
			if (OnMachineUnloaded != null)
			{
				OnMachineUnloaded.Invoke();
			}
		}
	}

	private void HandleSlotRemovedDuringActiveState()
	{
		if (currentState != BrewState.Brewing)
		{
			if (currentState == BrewState.Ready)
			{
				SetState(BrewState.Idle);
				if (OnMachineUnloaded != null)
				{
					OnMachineUnloaded.Invoke();
				}
			}
			return;
		}
		if (debugLogs)
		{
			Debug.Log("[EspressoBrewingController] Slot cleared mid-brew — aborting.", this);
		}
		brewElapsedSeconds = 0f;
		_scoreAccumulator = 0.0;
		_pressureScoreAccumulator = 0.0;
		_temperatureScoreAccumulator = 0.0;
		_sampleCount = 0;
		ResetTimerDial();
		SetState(BrewState.Idle);
	}

	private void InitialiseSimulation()
	{
		_simRunningTime = 0f;
		simTemperature = 0f;
		float tempNoiseOffset = UnityEngine.Random.Range(0f, 1000f);
		_tempNoiseOffset = tempNoiseOffset;
		float pressureNoiseOffset = UnityEngine.Random.Range(0f, 1000f);
		_pressureNoiseOffset = pressureNoiseOffset;
	}

	private void StepSimulation(float dt)
	{
		//IL_00d4: Expected O, but got I4
		//IL_06da: Expected F4, but got I4
		//IL_03e5: Invalid comparison between I4 and F4
		//IL_03f4: Expected O, but got I4
		//IL_0182: Expected F4, but got I4
		//IL_00a6: Expected O, but got I4
		//IL_06f8: Invalid comparison between O and F4
		//IL_01cd: Expected F4, but got I4
		//IL_0423: Invalid comparison between O and F4
		//IL_00bd: Expected O, but got I4
		//IL_046a: Invalid comparison between O and F4
		//IL_0209: Expected F4, but got I4
		//IL_0564: Invalid comparison between F4 and O
		//IL_0576: Expected F4, but got I4
		//IL_057f: Expected F4, but got I4
		//IL_05ca: Invalid comparison between F4 and O
		//IL_0785: Invalid comparison between F4 and O
		//IL_0218: Invalid comparison between F4 and O
		//IL_022a: Expected F4, but got I4
		//IL_0342: Expected F4, but got I4
		//IL_0325: Expected F4, but got I4
		//IL_05e8: Invalid comparison between F4 and O
		//IL_05fa: Expected F4, but got I4
		//IL_066c: Invalid comparison between O and F4
		//IL_026c: Expected F4, but got I4
		//IL_0380: Expected F4, but got I4
		//IL_02de: Expected F4, but got I4
		//IL_06b2: Invalid comparison between O and F4
		//IL_03be: Expected F4, but got I4
		float num2;
		object obj;
		if (temperatureDial != null)
		{
			DialInteractable dialInteractable = temperatureDial;
			bool flag = !(0.001f < tempDialMaxOutput);
			float num = 0.001f;
			if (!flag)
			{
				num = tempDialMaxOutput;
			}
			num2 = dialInteractable.accumulatedValue / num;
			bool flag2 = 0f > num2;
			obj = 0;
			if (!flag2)
			{
				bool flag3 = !(num2 > 1f);
				obj = 0;
				if (!flag3)
				{
					obj = 0;
					num2 = 1f;
				}
				goto IL_00d9;
			}
		}
		else
		{
			obj = 0;
		}
		num2 = 0f;
		goto IL_00d9;
		IL_0437:
		bool flag4 = !(0.001f < thermalWarmupDuration);
		float num3 = 0.001f;
		if (!flag4)
		{
			num3 = thermalWarmupDuration;
		}
		float num4 = _simRunningTime / num3;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num4))
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
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num4))
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
		float num5 = tempDecayRateWarmed - tempDecayRateCold;
		float num6 = _simRunningTime * noiseScrollSpeed;
		float num7 = num5 * num4;
		float num8 = num7 + tempDecayRateCold;
		float num9 = Mathf.PerlinNoise(num6, _tempNoiseOffset);
		float x = num6 + 17.3f;
		float num10 = num9 + num9;
		float num11 = num10 - 1f;
		float num12 = num11 * tempNoiseMagnitude;
		float num13 = Mathf.PerlinNoise(x, _pressureNoiseOffset);
		float num14 = num13 + num13;
		float num15 = num14 - 1f;
		float num16 = num15 * pressureNoiseMagnitude;
		float num17 = spikeMagnitudeMax;
		bool flag5 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num17) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
		float num18 = 0f;
		float num19 = 0f;
		if (!flag5)
		{
			float num20 = tempSpikeChancePerSecond;
			bool flag6 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num20) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
			num19 = 0f;
			if (!flag6)
			{
				float value = UnityEngine.Random.value;
				float num21 = dt * tempSpikeChancePerSecond;
				bool flag7 = !(num21 > value);
				num19 = 0f;
				if (!flag7)
				{
					float minInclusive = spikeMagnitudeMax ^ -0f;
					float num22 = UnityEngine.Random.Range(minInclusive, spikeMagnitudeMax);
					num19 = num22;
				}
			}
			float num23 = pressureSpikeChancePerSecond;
			bool flag8 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num23) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
			num18 = 0f;
			if (!flag8)
			{
				float value2 = UnityEngine.Random.value;
				float num24 = dt * pressureSpikeChancePerSecond;
				bool flag9 = !(num24 > value2);
				num18 = 0f;
				if (!flag9)
				{
					float minInclusive2 = spikeMagnitudeMax ^ -0f;
					float num25 = UnityEngine.Random.Range(minInclusive2, spikeMagnitudeMax);
					num18 = num25;
				}
			}
		}
		float num27;
		float num26 = num27 * pressureToTempCoupling;
		float num28 = num2 * tempInputScale;
		float num29 = num26 * tempInputScale;
		float num30 = simTemperature;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num30) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
		{
			num8 = 0f;
		}
		float num31 = num2 * tempToPressureCoupling;
		float num32 = num28 + num29;
		float num33 = num27 * pressureInputScale;
		float num34 = num32 - num8;
		float num35 = num31 * pressureInputScale;
		float num36 = num34 + num12;
		float num37 = num36 + num19;
		float num38 = simPressure;
		float num39 = ((System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num38) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj)) ? 0f : pressureDecayRate);
		float num40 = num33 + num35;
		float num41 = num37 * dt;
		float num42 = num41 + simTemperature;
		float num43 = num40 - num39;
		float num44 = num43 + num16;
		float num45 = num44 + num18;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num42))
		{
			if (num42 > tempMax)
			{
				num42 = tempMax;
			}
		}
		else
		{
			num42 = 0f;
		}
		float num46 = num45 * dt;
		simTemperature = num42;
		float num47 = num46 + simPressure;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num47))
		{
			if (num47 > pressureMax)
			{
				num47 = pressureMax;
			}
		}
		else
		{
			num47 = 0f;
		}
		simPressure = num47;
		return;
		IL_00d9:
		if (pressureDial != null)
		{
			DialInteractable dialInteractable2 = pressureDial;
			bool flag10 = !(0.001f < pressureDialMaxOutput);
			float num48 = 0.001f;
			if (!flag10)
			{
				num48 = pressureDialMaxOutput;
			}
			num27 = dialInteractable2.accumulatedValue / num48;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num27))
			{
				if (num27 > 1f)
				{
					num27 = 1f;
				}
				goto IL_0437;
			}
		}
		num27 = 0f;
		goto IL_0437;
	}

	private void ComputeMappedValues()
	{
		//IL_00df: Invalid comparison between I4 and F4
		//IL_0071: Expected F4, but got I4
		//IL_0142: Invalid comparison between I4 and F4
		bool flag = !(0.001f < tempMax);
		float num = 0.001f;
		if (!flag)
		{
			num = tempMax;
		}
		float num2 = simTemperature / num;
		if (!(0f > num2))
		{
			if (num2 > 1f)
			{
				num2 = 1f;
			}
		}
		else
		{
			num2 = 0f;
		}
		mappedTemperature = num2;
		bool flag2 = !(0.001f < pressureMax);
		float num3 = 0.001f;
		if (!flag2)
		{
			num3 = pressureMax;
		}
		float num4 = simPressure / num3;
		if (!(0f > num4))
		{
			if (!(num4 > 1f))
			{
				mappedPressure = num4;
			}
			else
			{
				mappedPressure = 1f;
			}
		}
		else
		{
			mappedPressure = 0f;
		}
	}

	private void WriteGaugeOutputs()
	{
		if (temperatureGaugeDisplay != null)
		{
			DialGaugeDisplay dialGaugeDisplay = temperatureGaugeDisplay;
			dialGaugeDisplay.targetNumber = mappedTemperature;
		}
		if (pressureGaugeDisplay != null)
		{
			DialGaugeDisplay dialGaugeDisplay2 = pressureGaugeDisplay;
			dialGaugeDisplay2.targetNumber = mappedPressure;
		}
	}

	private float ScoreMappedValue(float mapped, float idealMapped)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		//IL_0112: Invalid comparison between I4 and F4
		//IL_0092: Expected F4, but got I4
		float num2 = default(float);
		float num = mapped - num2;
		float num3 = 1f - num2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num & 0;
		float num4;
		if (!(num2 < num3))
		{
			bool flag = !(0.001f < num3);
			num4 = 0.001f;
			if (flag)
			{
				goto IL_00fa;
			}
		}
		num4 = num3;
		goto IL_00fa;
		IL_00fa:
		float num5 = (float)obj / num4;
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
		bool flag2 = !(0.01f < scoringCurvePower);
		float num6 = 0.01f;
		if (!flag2)
		{
			num6 = scoringCurvePower;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		return 1f - num5;
	}

	private float ScoreBrewTiming(float elapsed)
	{
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		//IL_00c3: Invalid comparison between I4 and F4
		//IL_0082: Expected F4, but got I4
		float num = elapsed - idealBrewSeconds;
		bool flag = !(0.001f < timingWindowSeconds);
		float num2 = 0.001f;
		if (!flag)
		{
			num2 = timingWindowSeconds;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num & 0;
		float num3 = (float)obj / num2;
		if (!(0f > num3))
		{
			if (num3 > 1f)
			{
				num3 = 1f;
			}
		}
		else
		{
			num3 = 0f;
		}
		bool flag2 = !(0.01f < scoringCurvePower);
		float num4 = 0.01f;
		if (!flag2)
		{
			num4 = scoringCurvePower;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		return 1f - num3;
	}

	private void AccumulateScore()
	{
		//IL_003f: Invalid comparison between I4 and F4
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Expected O, but got Unknown
		//IL_0325: Invalid comparison between I4 and F4
		//IL_00c1: Expected F4, but got I4
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Expected O, but got Unknown
		//IL_0268: Invalid comparison between I4 and F4
		//IL_0135: Expected F4, but got I4
		float num = temperatureWeight + pressureWeight;
		if (!(0f < num))
		{
			num = 1f;
		}
		float num2 = pressureWeight / num;
		float num3 = temperatureWeight / num;
		bool flag = !(0.001f < pressureMax);
		float num4 = 0.001f;
		if (!flag)
		{
			num4 = pressureMax;
		}
		float num5 = idealPressure / num4;
		float num6 = mappedPressure - num5;
		float num7 = 1f - num5;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num6 & 0;
		if (num5 < num7)
		{
			num5 = num7;
		}
		bool flag2 = !(0.001f < num5);
		float num8 = 0.001f;
		if (!flag2)
		{
			num8 = num5;
		}
		float num9 = (float)obj / num8;
		if (!(0f > num9))
		{
			if (num9 > 1f)
			{
				num9 = 1f;
			}
		}
		else
		{
			num9 = 0f;
		}
		bool flag3 = !(0.01f < scoringCurvePower);
		float num10 = 0.01f;
		if (!flag3)
		{
			num10 = scoringCurvePower;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		float num11 = 1f - num9;
		bool flag4 = !(0.001f < tempMax);
		float num12 = 0.001f;
		if (!flag4)
		{
			num12 = tempMax;
		}
		float num13 = idealTemperature / num12;
		float num14 = mappedTemperature - num13;
		float num15 = 1f - num13;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj2 = num14 & 0;
		if (num13 < num15)
		{
			num13 = num15;
		}
		bool flag5 = !(0.001f < num13);
		float num16 = 0.001f;
		if (!flag5)
		{
			num16 = num13;
		}
		float num17 = (float)obj2 / num16;
		if (!(0f > num17))
		{
			if (num17 > 1f)
			{
				num17 = 1f;
			}
		}
		else
		{
			num17 = 0f;
		}
		bool flag6 = !(0.01f < scoringCurvePower);
		float num18 = 0.01f;
		if (!flag6)
		{
			num18 = scoringCurvePower;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		int sampleCount = _sampleCount + 1;
		_sampleCount = sampleCount;
		float num19 = 1f - num17;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,qword ptr [rbx+140h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,qword ptr [rbx+148h]\"");
		float num20 = num19 * num3;
		float num21 = num11 * num2;
		_temperatureScoreAccumulator = num19;
		float num22 = num20 + num21;
		_pressureScoreAccumulator = num11;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,qword ptr [rbx+138h]\"");
		_scoreAccumulator = num22;
	}

	private unsafe void CompleteBrew()
	{
		//IL_008f: Expected F8, but got I4
		//IL_0098: Expected F8, but got I4
		//IL_00a1: Expected F8, but got I4
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Expected O, but got Unknown
		//IL_05a4: Invalid comparison between I4 and F4
		//IL_00ec: Expected F4, but got I4
		//IL_061d: Invalid comparison between I4 and F4
		//IL_0137: Expected F4, but got I4
		//IL_047e: Invalid comparison between I4 and F4
		//IL_0173: Expected F4, but got I4
		//IL_04cd: Invalid comparison between I4 and F4
		//IL_020c: Expected F4, but got I4
		//IL_03d9: Expected F4, but got Ref
		if (currentState != BrewState.Brewing)
		{
			return;
		}
		double num;
		double num2;
		double num3;
		if (_sampleCount > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,dword ptr [rsi+130h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,dword ptr [rsi+130h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,dword ptr [rsi+130h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
			num = _temperatureScoreAccumulator;
			num2 = _pressureScoreAccumulator;
			num3 = _scoreAccumulator;
		}
		else
		{
			num = 0.0;
			num2 = 0.0;
			num3 = 0.0;
		}
		float num4 = brewElapsedSeconds - idealBrewSeconds;
		bool flag = !(0.001f < timingWindowSeconds);
		float num5 = 0.001f;
		if (!flag)
		{
			num5 = timingWindowSeconds;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num4 & 0;
		float num6 = (float)obj / num5;
		if (!(0f > num6))
		{
			if (num6 > 1f)
			{
				num6 = 1f;
			}
		}
		else
		{
			num6 = 0f;
		}
		bool flag2 = !(0.01f < scoringCurvePower);
		float num7 = 0.01f;
		if (!flag2)
		{
			num7 = scoringCurvePower;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		float num8 = 1f - num6;
		float pressureScorePct = (float)num2 * 100f;
		float temperatureScorePct = (float)num * 100f;
		float num9 = 1f - timingWeight;
		float num10 = num8 * 100f;
		if (!(0f > num9))
		{
			if (num9 > 1f)
			{
				num9 = 1f;
			}
		}
		else
		{
			num9 = 0f;
		}
		float num11 = timingWeight;
		if (!(0f > timingWeight))
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
		float num12 = num9 * (float)num3;
		float num13 = num11 * num8;
		float num14 = num12 + num13;
		float num15;
		if (_loadedCan != null)
		{
			CoffeeGroundsCan loadedCan = _loadedCan;
			num15 = loadedCan.baseQuality;
		}
		else
		{
			num15 = 1f;
		}
		float num16;
		if (!(0f > num14))
		{
			bool flag3 = num14 > 1f;
			num16 = 1f;
			if (!flag3)
			{
				num16 = num14;
			}
		}
		else
		{
			num16 = 0f;
		}
		float num17 = num16 * num15;
		float num18 = num17 * 10000f;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		bool flag4 = !debugLogs;
		float num19 = num18 / 100f;
		finalQuality = num19;
		float timingScorePct;
		if (!flag4)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			object arg2 = default(object);
			object arg3 = default(object);
			string text = $"  pressureScore={arg:F2}%  temperatureScore={arg2:F2}%  timingScore={arg3:F2}%\n";
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg4 = default(object);
			object arg5 = default(object);
			object arg6 = default(object);
			string text2 = $"  dialScore={arg4:F4}  dialWeight={arg5:F2}  timingWeight={arg6:F2}\n";
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg7 = default(object);
			object arg8 = default(object);
			string text3 = $"  combined={arg7:F4}  canQuality={arg8:F2}\n";
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg9 = default(object);
			object arg10 = default(object);
			string text4 = $"  finalQuality={arg9:F2}%  (elapsed={arg10:F2}s)";
			string message = "[EspressoBrewingController] Brew complete!\n" + text + text2 + text3 + text4;
			Debug.Log(message, this);
			timingScorePct = num10;
		}
		else
		{
			timingScorePct = num10;
		}
		SetState(BrewState.Complete);
		_timerDialFrozen = true;
		FillCupInSlot(pressureScorePct, temperatureScorePct, timingScorePct);
		ConsumeCanUse();
		if (OnBrewComplete != null)
		{
			float num20 = default(float);
			OnBrewComplete.Invoke((nint)(&num20));
		}
	}

	private unsafe void ConsumeCanUse()
	{
		//IL_02cd: Expected O, but got I4
		//IL_02d2: Expected I, but got O
		//IL_0101: Expected O, but got I4
		//IL_0306: Expected I, but got O
		//IL_00f3: Expected O, but got I4
		if (!(_loadedCan != null))
		{
			return;
		}
		CoffeeGroundsCan loadedCan = _loadedCan;
		int num = loadedCan.maxUses ^ loadedCan.maxUses;
		int num2 = loadedCan.maxUses & num;
		bool flag = num2 < 0;
		bool flag2 = loadedCan.maxUses < 0;
		bool flag3 = flag2 == flag;
		object obj = !flag3;
		nint num3 = unchecked((nint)null);
		object obj2;
		if (obj == null)
		{
			int num4 = loadedCan.remainingUses - 1;
			int num5 = 0;
			if (!flag2)
			{
				num5 = num4;
			}
			loadedCan.remainingUses = num5;
			bool flag4 = loadedCan.OnUseConsumed == null;
			num3 = unchecked((nint)null);
			if (!flag4)
			{
				int num6 = default(int);
				loadedCan.OnUseConsumed.Invoke((int)(&num6));
				num6 = num5;
				num3 = 0;
			}
			if (loadedCan.remainingUses <= 0)
			{
				if (loadedCan.OnEmpty != null)
				{
					loadedCan.OnEmpty.Invoke();
				}
				obj2 = 1;
				goto IL_0314;
			}
		}
		obj2 = 0;
		goto IL_0314;
		IL_0314:
		if (debugLogs)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string text = $"[EspressoBrewingController] Can use consumed — remaining={arg}";
			bool flag5 = obj2 == null;
			string text2 = ".";
			if (!flag5)
			{
				text2 = " — can exhausted, ejecting.";
			}
			string message = text + text2;
			Debug.Log(message, this);
		}
		if (obj2 == null)
		{
			return;
		}
		if (_loadedCan != null)
		{
			bool flag6 = groundsSlot == null;
			if (!flag6)
			{
				if (debugLogs != flag6)
				{
					Debug.Log("[EspressoBrewingController] Ejecting exhausted can.", this);
				}
				CoffeeGroundsCan loadedCan2 = _loadedCan;
				groundsSlot.RemoveItem(loadedCan2._003CDraggableItem_003Ek__BackingField, autoEject: true);
			}
		}
		if (OnCanExhausted != null)
		{
			OnCanExhausted.Invoke(_loadedCan);
		}
	}

	private void EjectLoadedCan()
	{
		if (!(_loadedCan != null))
		{
			return;
		}
		bool flag = groundsSlot == null;
		if (!flag)
		{
			if (debugLogs != flag)
			{
				Debug.Log("[EspressoBrewingController] Ejecting exhausted can.", this);
			}
			CoffeeGroundsCan loadedCan = _loadedCan;
			groundsSlot.RemoveItem(loadedCan._003CDraggableItem_003Ek__BackingField, autoEject: true);
		}
	}

	private void WriteTimerDial(float elapsed)
	{
		if (brewTimerDial != null)
		{
			DialGaugeDisplay dialGaugeDisplay = brewTimerDial;
			bool flag = !(1f < timerDialSecondsPerRevolution);
			float y = 1f;
			if (!flag)
			{
				y = timerDialSecondsPerRevolution;
			}
			float targetNumber = MathF.FMod(elapsed, y);
			dialGaugeDisplay.targetNumber = targetNumber;
		}
	}

	private void ResetTimerDial()
	{
		_timerDialFrozen = false;
		if (brewTimerDial != null)
		{
			DialGaugeDisplay dialGaugeDisplay = brewTimerDial;
			dialGaugeDisplay.targetNumber = 0f;
		}
	}

	private void FreezeTimerDial()
	{
		_timerDialFrozen = true;
	}

	public unsafe void ToggleBrew()
	{
		//IL_009f: Expected I4, but got O
		//IL_0019: Expected F4, but got Ref
		object obj = default(object);
		if (currentState == BrewState.Ready)
		{
			brewElapsedSeconds = 0f;
			_sampleCount = 0;
			_scoreAccumulator = 0.0;
			_pressureScoreAccumulator = 0.0;
			_temperatureScoreAccumulator = 0.0;
			runningDialScore = 0f;
			ResetTimerDial();
			SetState(BrewState.Brewing);
			if (OnBrewStarted != null)
			{
				OnBrewStarted.Invoke((nint)(&obj));
			}
			if (debugLogs)
			{
				Debug.Log("[EspressoBrewingController] Brew started.", this);
			}
		}
		else if (currentState == BrewState.Brewing)
		{
			CompleteBrew();
		}
		else if (debugLogs)
		{
			object arg = (BrewState)obj;
			string message = $"[EspressoBrewingController] ToggleBrew ignored — state is {arg}.";
			Debug.Log(message, this);
		}
	}

	public void CollectCup()
	{
		//IL_006f: Expected O, but got I
		//IL_009f: Expected O, but got I
		if (currentState == BrewState.Complete)
		{
			UnityEngine.Object loadedCup = _loadedCup;
			_loadedCup = null;
			if (_loadedCup != null)
			{
				UnityAction call = CollectCup;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rdi_v2 (UnityEngine.Object)+38]");
				((UnityEvent)0).RemoveListener(call);
				UnityAction call2 = CollectCupIfComplete;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rdi_v2 (UnityEngine.Object)+50]");
				((UnityEvent)0).RemoveListener(call2);
			}
			SetState(BrewState.Idle);
			ResetTimerDial();
			if (temperatureDial != null)
			{
				temperatureDial.ResetToMinimum();
			}
			if (pressureDial != null)
			{
				pressureDial.ResetToMinimum();
			}
			if (debugLogs)
			{
				Debug.Log("[EspressoBrewingController] Cup collected.", this);
			}
			if (_loadedCup != null && OnCupCollected != null)
			{
				OnCupCollected.Invoke(_loadedCup);
			}
		}
	}

	private void CollectCupIfComplete()
	{
		if (currentState == BrewState.Complete)
		{
			CollectCup();
		}
	}

	private unsafe void StartBrew()
	{
		//IL_0014: Expected F4, but got Ref
		brewElapsedSeconds = 0f;
		_sampleCount = 0;
		_scoreAccumulator = 0.0;
		_pressureScoreAccumulator = 0.0;
		_temperatureScoreAccumulator = 0.0;
		runningDialScore = 0f;
		ResetTimerDial();
		SetState(BrewState.Brewing);
		if (OnBrewStarted != null)
		{
			object obj = default(object);
			OnBrewStarted.Invoke((nint)(&obj));
		}
		if (debugLogs)
		{
			Debug.Log("[EspressoBrewingController] Brew started.", this);
		}
	}

	private void AbortBrew()
	{
		brewElapsedSeconds = 0f;
		_scoreAccumulator = 0.0;
		_pressureScoreAccumulator = 0.0;
		_temperatureScoreAccumulator = 0.0;
		_sampleCount = 0;
		ResetTimerDial();
		SetState(BrewState.Idle);
	}

	private unsafe void FillCupInSlot(float pressureScorePct, float temperatureScorePct, float timingScorePct)
	{
		//IL_0145: Expected F4, but got Ref
		if (_loadedCup != null)
		{
			EspressoCup loadedCup = _loadedCup;
			string coffeeLabel;
			if (_loadedCan != null)
			{
				CoffeeGroundsCan loadedCan = _loadedCan;
				coffeeLabel = loadedCan.coffeeLabel;
			}
			else
			{
				coffeeLabel = "Unknown";
			}
			float num = finalQuality * 100f;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			float quality = num / 100f;
			float num2 = pressureScorePct * 100f;
			loadedCup.quality = quality;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			float pressureScore = num2 / 100f;
			float num3 = temperatureScorePct * 100f;
			loadedCup.pressureScore = pressureScore;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			float temperatureScore = num3 / 100f;
			float num4 = timingScorePct * 100f;
			loadedCup.temperatureScore = temperatureScore;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			float timingScore = num4 / 100f;
			loadedCup.coffeeLabel = coffeeLabel;
			loadedCup.timingScore = timingScore;
			loadedCup.isFull = true;
			loadedCup.isInitialised = true;
			if (loadedCup.OnResultInitialised != null)
			{
				float quality2 = default(float);
				loadedCup.OnResultInitialised.Invoke((nint)(&quality2));
				quality2 = loadedCup.quality;
			}
			if (loadedCup.OnCupFilled != null)
			{
				loadedCup.OnCupFilled.Invoke();
			}
			EspressoCup loadedCup2 = _loadedCup;
			UnityAction call = CollectCup;
			loadedCup2.OnCupPickedUp.AddListener(call);
			EspressoCup loadedCup3 = _loadedCup;
			UnityAction call2 = CollectCupIfComplete;
			loadedCup3.OnCupEmptied.AddListener(call2);
			if (debugLogs)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string message = $"[EspressoBrewingController] Cup filled — quality {arg:F2}%.";
				Debug.Log(message, this);
			}
		}
		else
		{
			Debug.LogWarning("[EspressoBrewingController] FillCupInSlot — _loadedCup is null.", this);
		}
	}

	private void ResetInputDials()
	{
		if (temperatureDial != null)
		{
			temperatureDial.ResetToMinimum();
		}
		if (pressureDial != null)
		{
			pressureDial.ResetToMinimum();
		}
	}

	private void EjectItem(GameObject itemGO, ItemSlot sourceSlot)
	{
		if (sourceSlot != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			UnityEngine.Object obj = default(UnityEngine.Object);
			if (obj != null)
			{
				sourceSlot.RemoveItem((DraggableItem)obj, autoEject: true);
			}
		}
	}

	private void SetState(BrewState newState)
	{
		//IL_002c: Expected I4, but got O
		//IL_0039: Expected I4, but got O
		//IL_00e4: Expected O, but got I4
		if (debugLogs && newState != currentState)
		{
			object obj = default(object);
			object arg = (BrewState)obj;
			object obj2 = default(object);
			object arg2 = (BrewState)obj2;
			string message = $"[EspressoBrewingController] {arg} → {arg2}";
			Debug.Log(message, this);
		}
		currentState = newState;
		if (brewButton != null)
		{
			bool active;
			if (currentState == BrewState.Ready)
			{
				active = true;
			}
			else
			{
				object obj3 = currentState - 2;
				bool flag = obj3 == null;
				active = flag;
			}
			brewButton.SetActive(active);
		}
	}

	public EspressoBrewingController()
	{
		UnityEvent onMachineReady = new UnityEvent();
		OnMachineReady = onMachineReady;
		OnMachineUnloaded = new UnityEvent();
		OnBrewStarted = new UnityEvent<float>();
		OnBrewTick = new UnityEvent<float>();
		OnBrewComplete = new UnityEvent<float>();
		OnCupCollected = new UnityEvent<EspressoCup>();
		OnCanExhausted = new UnityEvent<CoffeeGroundsCan>();
		OnEmptyCanRejected = new UnityEvent<CoffeeGroundsCan>();
		base._002Ector();
	}
}
