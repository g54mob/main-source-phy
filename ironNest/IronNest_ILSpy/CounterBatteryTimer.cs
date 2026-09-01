using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class CounterBatteryTimer : MonoBehaviour
{
	[Serializable]
	public class SecondsChangedEvent : UnityEvent<float>
	{
	}

	public float totalDurationSeconds = 300f;

	public UnityEvent onTimerStarted;

	public SecondsChangedEvent onTimerTick;

	public UnityEvent onTimerExpired;

	public UnityEvent onTimerPermanentlyStopped;

	public UnityEvent onTimerPaused;

	public UnityEvent onTimerUnpaused;

	public bool verbose;

	private static CounterBatteryTimer _003CInstance_003Ek__BackingField;

	private float _remainingSeconds;

	private bool _running;

	private bool _expired;

	private bool _permanentlyStopped;

	private double endTime;

	public static CounterBatteryTimer Instance
	{
		get
		{
			return _003CInstance_003Ek__BackingField;
		}
		private set
		{
			_003CInstance_003Ek__BackingField = value;
		}
	}

	public float TimeRemaining
	{
		get
		{
			//IL_0081: Invalid comparison between I4 and F8
			//IL_0093: Expected F4, but got I4
			if (_running && !_expired && !_permanentlyStopped)
			{
				double timeAsDouble = Time.timeAsDouble;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm0\"");
				bool flag = !(0.0 < endTime);
				float result = 0f;
				if (!flag)
				{
					result = (float)endTime;
				}
				return result;
			}
			return _remainingSeconds;
		}
	}

	public bool IsRunning => _running;

	public bool IsExpired => _expired;

	public bool IsPermanentlyStopped => _permanentlyStopped;

	private void Awake()
	{
		_003CInstance_003Ek__BackingField = this;
	}

	private void OnDestroy()
	{
		if (_003CInstance_003Ek__BackingField == this)
		{
			_003CInstance_003Ek__BackingField = null;
		}
	}

	private unsafe void OnEnable()
	{
		//IL_0059: Invalid comparison between I4 and F4
		//IL_006b: Expected F4, but got I4
		//IL_0023: Expected F4, but got Ref
		_running = false;
		_permanentlyStopped = false;
		endTime = 0.0;
		bool flag = !(0f < totalDurationSeconds);
		float remainingSeconds = 0f;
		if (!flag)
		{
			remainingSeconds = totalDurationSeconds;
		}
		_remainingSeconds = remainingSeconds;
		if (onTimerTick != null)
		{
			object obj = default(object);
			onTimerTick.Invoke((nint)(&obj));
		}
	}

	private unsafe void ResetRuntimeState()
	{
		//IL_0059: Invalid comparison between I4 and F4
		//IL_006b: Expected F4, but got I4
		//IL_0023: Expected F4, but got Ref
		_running = false;
		_permanentlyStopped = false;
		endTime = 0.0;
		bool flag = !(0f < totalDurationSeconds);
		float remainingSeconds = 0f;
		if (!flag)
		{
			remainingSeconds = totalDurationSeconds;
		}
		_remainingSeconds = remainingSeconds;
		if (onTimerTick != null)
		{
			object obj = default(object);
			onTimerTick.Invoke((nint)(&obj));
		}
	}

	private unsafe void Update()
	{
		//IL_0067: Invalid comparison between I4 and F8
		//IL_0079: Expected F8, but got I4
		//IL_021d: Invalid comparison between I4 and F8
		//IL_00c5: Expected O, but got I
		//IL_01e5: Expected F4, but got Ref
		if (!_running || _expired || _permanentlyStopped)
		{
			return;
		}
		double timeAsDouble = Time.timeAsDouble;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm0\"");
		bool flag = !(0.0 < endTime);
		double num = 0.0;
		if (!flag)
		{
			num = endTime;
		}
		_remainingSeconds = (float)num;
		if (!(0.0 < num))
		{
			bool flag2 = !verbose;
			_remainingSeconds = 0f;
			_expired = true;
			IntPtr intPtr = default(IntPtr);
			UnityEngine.Object obj = (UnityEngine.Object)(nint)intPtr;
			if (!flag2)
			{
				Debug.Log("[CounterBatteryTimer] Timer expired.", this);
				obj = this;
			}
			if (onTimerExpired != null)
			{
				onTimerExpired.Invoke();
				obj = null;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407BF0");
			UnityEngine.Object obj2 = default(UnityEngine.Object);
			if (obj2 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407BF0");
				FireMission fireMission = default(FireMission);
				fireMission.ProcessNotification("TimerExpired");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407BF0");
				EventData_CounterBatteryEvent eventData_CounterBatteryEvent = new EventData_CounterBatteryEvent();
				eventData_CounterBatteryEvent.EventType = EventData_CounterBatteryEvent.EventTypes.Expired;
				FireMission fireMission2 = default(FireMission);
				fireMission2.ProcessEvent(eventData_CounterBatteryEvent);
			}
		}
		if (onTimerTick != null)
		{
			object obj3 = default(object);
			onTimerTick.Invoke((nint)(&obj3));
		}
	}

	public unsafe void Init(float InitalTime)
	{
		//IL_01a4: Invalid comparison between I4 and F4
		//IL_01b6: Expected F4, but got I4
		//IL_002d: Expected F4, but got Ref
		//IL_0072: Invalid comparison between I4 and F4
		//IL_0084: Expected F4, but got I4
		//IL_0143: Expected F4, but got Ref
		//IL_00e0: Expected I, but got O
		totalDurationSeconds = InitalTime;
		_running = false;
		_permanentlyStopped = false;
		endTime = 0.0;
		bool flag = !(0f < totalDurationSeconds);
		float num = 0f;
		if (!flag)
		{
			num = totalDurationSeconds;
		}
		_remainingSeconds = num;
		bool flag2 = onTimerTick == null;
		IntPtr intPtr = default(IntPtr);
		nint num2 = intPtr;
		float num3 = default(float);
		if (!flag2)
		{
			onTimerTick.Invoke((nint)(&num3));
			num3 = num;
			num2 = 0;
		}
		if (!_running && !_permanentlyStopped)
		{
			bool flag3 = !(0f < totalDurationSeconds);
			float remainingSeconds = 0f;
			if (!flag3)
			{
				remainingSeconds = totalDurationSeconds;
			}
			_remainingSeconds = remainingSeconds;
			double timeAsDouble = Time.timeAsDouble;
			bool flag4 = !verbose;
			_running = true;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,xmm0\"");
			endTime = _remainingSeconds;
			if (!flag4)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string message = $"[CounterBatteryTimer] Timer started: {arg} seconds (max).";
				Debug.Log(message, this);
				num3 = _remainingSeconds;
				num2 = unchecked((nint)null);
			}
			if (onTimerStarted != null)
			{
				onTimerStarted.Invoke();
			}
			if (onTimerTick != null)
			{
				onTimerTick.Invoke((nint)(&num3));
			}
			EventData_CounterBatteryEvent eventData_CounterBatteryEvent = new EventData_CounterBatteryEvent();
			eventData_CounterBatteryEvent.EventType = EventData_CounterBatteryEvent.EventTypes.Started;
			FireMission._003CInstance_003Ek__BackingField.ProcessEvent(eventData_CounterBatteryEvent);
		}
	}

	public unsafe void StartTimer()
	{
		//IL_0032: Invalid comparison between I4 and F4
		//IL_0044: Expected F4, but got I4
		//IL_0102: Expected F4, but got Ref
		//IL_0096: Expected I, but got O
		if (!_running && !_permanentlyStopped)
		{
			bool flag = !(0f < totalDurationSeconds);
			float remainingSeconds = 0f;
			if (!flag)
			{
				remainingSeconds = totalDurationSeconds;
			}
			_remainingSeconds = remainingSeconds;
			double timeAsDouble = Time.timeAsDouble;
			bool flag2 = !verbose;
			_running = true;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,xmm0\"");
			endTime = _remainingSeconds;
			float remainingSeconds2 = default(float);
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string message = $"[CounterBatteryTimer] Timer started: {arg} seconds (max).";
				Debug.Log(message, this);
				nint num = unchecked((nint)null);
				remainingSeconds2 = _remainingSeconds;
			}
			if (onTimerStarted != null)
			{
				onTimerStarted.Invoke();
			}
			if (onTimerTick != null)
			{
				onTimerTick.Invoke((nint)(&remainingSeconds2));
				nint num = 0;
			}
			EventData_CounterBatteryEvent eventData_CounterBatteryEvent = new EventData_CounterBatteryEvent();
			eventData_CounterBatteryEvent.EventType = EventData_CounterBatteryEvent.EventTypes.Started;
			FireMission._003CInstance_003Ek__BackingField.ProcessEvent(eventData_CounterBatteryEvent);
		}
	}

	public void PermanentlyStop()
	{
		if (!_permanentlyStopped)
		{
			bool flag = !verbose;
			_permanentlyStopped = true;
			_running = false;
			if (!flag)
			{
				Debug.Log("[CounterBatteryTimer] Permanently stopped.", this);
			}
			if (onTimerPermanentlyStopped != null)
			{
				onTimerPermanentlyStopped.Invoke();
			}
		}
	}

	public unsafe void AddTime(float seconds)
	{
		//IL_0048: Expected F8, but got I4
		//IL_0430: Invalid comparison between F4 and I4
		//IL_043f: Invalid comparison between F4 and I4
		//IL_0074: Invalid comparison between I4 and F8
		//IL_0086: Expected F8, but got I4
		//IL_0484: Invalid comparison between F4 and I4
		//IL_00ac: Expected F4, but got I4
		//IL_04ae: Invalid comparison between F4 and I4
		//IL_04bd: Invalid comparison between F4 and I4
		//IL_04d0: Expected O, but got I4
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Expected I4, but got Unknown
		//IL_0145: Invalid comparison between F4 and I4
		//IL_03ef: Expected F4, but got Ref
		//IL_0198: Expected I, but got O
		//IL_01ba: Expected O, but got I4
		//IL_020e: Expected I, but got O
		//IL_021e: Expected O, but got I
		//IL_0240: Expected O, but got I4
		//IL_029c: Expected I, but got O
		//IL_02ac: Expected O, but got I
		//IL_02ce: Expected O, but got I4
		//IL_0334: Expected I, but got O
		//IL_0344: Expected O, but got I
		//IL_0366: Expected O, but got I4
		if (_permanentlyStopped)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj = default(object);
		if (obj != null)
		{
			return;
		}
		bool flag = (_running ? 1 : 0) == (nint)obj;
		double num = 0.0;
		if (!flag)
		{
			double timeAsDouble = Time.timeAsDouble;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm0\"");
			bool flag2 = !(0.0 < endTime);
			double num2 = 0.0;
			if (!flag2)
			{
				num2 = endTime;
			}
			_remainingSeconds = (float)num2;
			num = endTime;
		}
		float num3 = seconds + _remainingSeconds;
		bool flag3 = num3 == 0f;
		if (num3 < 0f)
		{
			num3 = 0f;
		}
		_remainingSeconds = num3;
		float num4 = (float)num;
		if (!flag3)
		{
			double timeAsDouble2 = Time.timeAsDouble;
			num4 = _remainingSeconds;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,xmm0\"");
			endTime = _remainingSeconds;
		}
		float remainingSeconds = _remainingSeconds;
		if (_remainingSeconds > 0f)
		{
			_expired = false;
		}
		EventData_CounterBatteryEvent eventData_CounterBatteryEvent = new EventData_CounterBatteryEvent();
		bool flag4 = seconds < 0f;
		bool flag5 = seconds == 0f;
		object obj2 = flag4 | flag5;
		EventData_CounterBatteryEvent.EventTypes eventType = (EventData_CounterBatteryEvent.EventTypes)(obj2 + 4);
		eventData_CounterBatteryEvent.EventType = eventType;
		FireMission._003CInstance_003Ek__BackingField.ProcessEvent(eventData_CounterBatteryEvent);
		float num9 = default(float);
		if (verbose)
		{
			object[] array = new object[4];
			bool flag6 = !(seconds < 0f);
			EventData_CounterBatteryEvent eventData_CounterBatteryEvent2 = (EventData_CounterBatteryEvent)(object)"added";
			if (!flag6)
			{
				eventData_CounterBatteryEvent2 = (EventData_CounterBatteryEvent)(object)"removed";
			}
			if (eventData_CounterBatteryEvent2 != null)
			{
				nint num5 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj3 = default(object);
				bool flag7 = obj3 == null;
				object obj4 = 0;
				if (flag7)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj5 = default(object);
					throw obj5;
				}
			}
			array[0] = eventData_CounterBatteryEvent2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj6 = default(object);
			if (obj6 != null)
			{
				nint num6 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v732 @ rdx_v36 (Il2CppClass<System.Object[]>)+40]");
				object obj7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj8 = default(object);
				bool flag8 = obj8 == null;
				object obj4 = 0;
				object obj9 = obj6;
				if (flag8)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj10 = default(object);
					throw obj10;
				}
			}
			array[1] = obj6;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj11 = default(object);
			if (obj11 != null)
			{
				nint num7 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v777 @ rdx_v34 (Il2CppClass<System.Object[]>)+40]");
				object obj12 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj13 = default(object);
				bool flag9 = obj13 == null;
				object obj4 = 0;
				object obj14 = obj11;
				if (flag9)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj15 = default(object);
					throw obj15;
				}
			}
			array[2] = obj11;
			remainingSeconds = _remainingSeconds;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj16 = default(object);
			if (obj16 != null)
			{
				nint num8 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v805 @ rdx_v32 (Il2CppClass<System.Object[]>)+40]");
				object obj17 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj18 = default(object);
				bool flag10 = obj18 == null;
				object obj4 = 0;
				object obj19 = obj16;
				if (flag10)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj20 = default(object);
					throw obj20;
				}
			}
			array[3] = obj16;
			string message = string.Format("[CounterBatteryTimer] Time {0}: {1:0.##}s (was {2:0.##}s, now {3:0.##}s).", array);
			Debug.Log(message, this);
			num9 = seconds;
		}
		if (onTimerTick != null)
		{
			onTimerTick.Invoke((nint)(&num9));
		}
	}

	public unsafe void SetTime(float seconds)
	{
		//IL_0038: Invalid comparison between F4 and I4
		//IL_0179: Expected F4, but got I4
		//IL_01b2: Invalid comparison between F4 and I4
		//IL_0060: Expected F4, but got I4
		//IL_012f: Expected F4, but got Ref
		if (_permanentlyStopped)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj = default(object);
		if (obj == null)
		{
			bool flag = !(seconds < 0f);
			float remainingSeconds = seconds;
			if (!flag)
			{
				remainingSeconds = 0f;
			}
			_remainingSeconds = remainingSeconds;
			bool flag2 = (_running ? 1 : 0) == (nint)obj;
			float num = 0f;
			if (!flag2)
			{
				double timeAsDouble = Time.timeAsDouble;
				num = _remainingSeconds;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,xmm0\"");
				endTime = _remainingSeconds;
			}
			if (_remainingSeconds > 0f && _expired)
			{
				_expired = false;
			}
			float num2 = default(float);
			if (verbose)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				object arg2 = default(object);
				object arg3 = default(object);
				string message = $"[CounterBatteryTimer] Time Set: {arg:0.##}s (was {arg2:0.##}s, now {arg3:0.##}s).";
				Debug.Log(message, this);
				num2 = seconds;
			}
			if (onTimerTick != null)
			{
				onTimerTick.Invoke((nint)(&num2));
			}
		}
	}

	public void PauseTimer()
	{
		//IL_0045: Invalid comparison between I4 and F8
		//IL_0057: Expected F8, but got I4
		if (_running && !_permanentlyStopped)
		{
			double timeAsDouble = Time.timeAsDouble;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm0\"");
			bool flag = !(0.0 < endTime);
			double num = 0.0;
			if (!flag)
			{
				num = endTime;
			}
			bool flag2 = !verbose;
			_remainingSeconds = (float)num;
			_running = false;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string message = $"[CounterBatteryTimer] Timer paused: {arg} seconds remaining.";
				Debug.Log(message, this);
			}
			if (onTimerPaused != null)
			{
				onTimerPaused.Invoke();
			}
			EventData_CounterBatteryEvent eventData_CounterBatteryEvent = new EventData_CounterBatteryEvent();
			eventData_CounterBatteryEvent.EventType = EventData_CounterBatteryEvent.EventTypes.Paused;
			FireMission._003CInstance_003Ek__BackingField.ProcessEvent(eventData_CounterBatteryEvent);
		}
	}

	public void UnpauseTimer()
	{
		if (!_running && !_permanentlyStopped)
		{
			double timeAsDouble = Time.timeAsDouble;
			bool flag = !verbose;
			_running = true;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,xmm0\"");
			endTime = _remainingSeconds;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string message = $"[CounterBatteryTimer] Timer unpaused: {arg} seconds remaining.";
				Debug.Log(message, this);
			}
			if (onTimerUnpaused != null)
			{
				onTimerUnpaused.Invoke();
			}
			EventData_CounterBatteryEvent eventData_CounterBatteryEvent = new EventData_CounterBatteryEvent();
			eventData_CounterBatteryEvent.EventType = EventData_CounterBatteryEvent.EventTypes.Unpaused;
			FireMission._003CInstance_003Ek__BackingField.ProcessEvent(eventData_CounterBatteryEvent);
		}
	}
}
