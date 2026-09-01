using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public sealed class RandomUnityEventScheduler : MonoBehaviour
{
	public enum IntervalMode
	{
		FixedInterval,
		RandomRangePerAttempt
	}

	public enum Clock
	{
		ScaledTime,
		UnscaledTime
	}

	private sealed class _003CRun_003Ed__38 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RandomUnityEventScheduler _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CRun_003Ed__38(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_009e: Expected I4, but got I8
			//IL_0363: Expected I4, but got O
			//IL_0039: Expected O, but got I4
			//IL_008a: Expected I4, but got I8
			//IL_0100: Invalid comparison between F4 and I4
			//IL_0076: Expected I4, but got I8
			//IL_0199: Invalid comparison between F4 and I4
			RandomUnityEventScheduler randomUnityEventScheduler = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					if ((nint)obj == 1)
					{
						_003C_003E1__state = -1;
						goto IL_01ad;
					}
					goto IL_02e4;
				}
				_003C_003E1__state = -1;
			}
			else
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_0355;
				}
				if (randomUnityEventScheduler.onStarted != null)
				{
					randomUnityEventScheduler.onStarted.Invoke();
				}
				if (randomUnityEventScheduler.startDelaySeconds > 0f)
				{
					object obj2 = _003C_003E4__this.WaitForSecondsDynamic(randomUnityEventScheduler.startDelaySeconds);
					_003C_003E2__current = obj2;
					_003C_003E1__state = 1;
					return true;
				}
			}
			if ((object)_003C_003E4__this != null)
			{
				goto IL_0133;
			}
			goto IL_0355;
			IL_0355:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0222:
			if (!randomUnityEventScheduler.unlimitedTriggers && randomUnityEventScheduler._successfulTriggers >= randomUnityEventScheduler.maxTriggers && !randomUnityEventScheduler._hasNotifiedMax)
			{
				randomUnityEventScheduler._hasNotifiedMax = true;
				if (randomUnityEventScheduler.onMaxedOut != null)
				{
					randomUnityEventScheduler.onMaxedOut.Invoke();
				}
			}
			randomUnityEventScheduler._runner = null;
			goto IL_02e4;
			IL_0133:
			if (randomUnityEventScheduler.unlimitedTriggers || randomUnityEventScheduler._successfulTriggers < randomUnityEventScheduler.maxTriggers)
			{
				goto IL_0181;
			}
			goto IL_0222;
			IL_02e4:
			return false;
			IL_0181:
			float num = _003C_003E4__this.NextIntervalSeconds();
			if (!(num > 0f))
			{
				goto IL_01ad;
			}
			object obj3 = _003C_003E4__this.WaitForSecondsDynamic(num);
			_003C_003E2__current = obj3;
			_003C_003E1__state = 2;
			return true;
			IL_01ad:
			if ((object)_003C_003E4__this != null)
			{
				_003C_003E4__this.InternalAttempt();
				if (randomUnityEventScheduler.unlimitedTriggers)
				{
					goto IL_0181;
				}
				if (randomUnityEventScheduler._successfulTriggers < randomUnityEventScheduler.maxTriggers)
				{
					goto IL_0133;
				}
				goto IL_0222;
			}
			goto IL_0355;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private bool autoStartOnEnable = true;

	private float startDelaySeconds;

	private Clock clock;

	private bool restartAfterReset = true;

	private IntervalMode intervalMode;

	private float fixedIntervalSeconds = 5f;

	private float randomIntervalMinSeconds = 3f;

	private float randomIntervalMaxSeconds = 7f;

	private float triggerChancePercent = 50f;

	private bool unlimitedTriggers = true;

	private int maxTriggers = 3;

	private UnityEvent onStarted;

	private UnityEvent onAttempt;

	private UnityEvent onTriggered;

	private UnityEvent onMaxedOut;

	private UnityEvent onReset;

	private Coroutine _runner;

	private int _successfulTriggers;

	private int _attempts;

	private bool _hasNotifiedMax;

	private bool _rangeWarningLogged;

	public bool IsRunning
	{
		get
		{
			bool flag = (nint)_runner < 0;
			bool flag2 = _runner == null;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			return flag4 & flag3;
		}
	}

	public int SuccessfulTriggers => _successfulTriggers;

	public int Attempts => _attempts;

	private void OnEnable()
	{
		if (autoStartOnEnable)
		{
			StartSchedule();
		}
	}

	private void OnDisable()
	{
		if (_runner != null)
		{
			StopCoroutine(_runner);
			_runner = null;
		}
	}

	private void OnValidate()
	{
		//IL_00b5: Invalid comparison between I4 and F4
		//IL_008b: Expected F4, but got I4
		//IL_00de: Invalid comparison between I4 and F4
		if (!unlimitedTriggers && maxTriggers < 1)
		{
			maxTriggers = 1;
		}
		float num = triggerChancePercent;
		if (!(0f > triggerChancePercent))
		{
			if (num > 100f)
			{
				num = 100f;
			}
		}
		else
		{
			num = 0f;
		}
		triggerChancePercent = num;
		if (0f > fixedIntervalSeconds)
		{
			fixedIntervalSeconds = 0f;
		}
	}

	public void StartSchedule()
	{
		if ((nint)_runner <= 0)
		{
			_003CRun_003Ed__38 obj = new _003CRun_003Ed__38(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine runner = StartCoroutine(obj);
			_runner = runner;
		}
	}

	public void StopSchedule()
	{
		if (_runner != null)
		{
			StopCoroutine(_runner);
			_runner = null;
		}
	}

	public void ResetSchedule()
	{
		if (_runner != null)
		{
			StopCoroutine(_runner);
			_runner = null;
		}
		_successfulTriggers = 0;
		_hasNotifiedMax = false;
		if (onReset != null)
		{
			onReset.Invoke();
		}
		if (restartAfterReset && base.isActiveAndEnabled && (nint)_runner <= 0)
		{
			_003CRun_003Ed__38 obj = new _003CRun_003Ed__38(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine runner = StartCoroutine(obj);
			_runner = runner;
		}
	}

	public void ForceAttempt()
	{
		InternalAttempt();
	}

	public void ForceTrigger()
	{
		int successfulTriggers = _successfulTriggers + 1;
		_successfulTriggers = successfulTriggers;
		if (onTriggered != null)
		{
			onTriggered.Invoke();
		}
		if (unlimitedTriggers || _successfulTriggers < maxTriggers)
		{
			return;
		}
		if (!_hasNotifiedMax)
		{
			_hasNotifiedMax = true;
			if (onMaxedOut != null)
			{
				onMaxedOut.Invoke();
			}
		}
		if (_runner != null)
		{
			StopCoroutine(_runner);
			_runner = null;
		}
	}

	public void ForceTrigger(bool countAsTrigger)
	{
		if (!countAsTrigger)
		{
			if (onTriggered != null)
			{
				onTriggered.Invoke();
			}
			return;
		}
		int successfulTriggers = _successfulTriggers + 1;
		_successfulTriggers = successfulTriggers;
		if (onTriggered != null)
		{
			onTriggered.Invoke();
		}
		if (unlimitedTriggers || _successfulTriggers < maxTriggers)
		{
			return;
		}
		if (!_hasNotifiedMax)
		{
			_hasNotifiedMax = true;
			if (onMaxedOut != null)
			{
				onMaxedOut.Invoke();
			}
		}
		if (_runner != null)
		{
			StopCoroutine(_runner);
			_runner = null;
		}
	}

	private IEnumerator Run()
	{
		_003CRun_003Ed__38 obj = new _003CRun_003Ed__38(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private void InternalAttempt()
	{
		//IL_0054: Invalid comparison between I4 and F4
		int attempts = _attempts + 1;
		_attempts = attempts;
		if (onAttempt != null)
		{
			onAttempt.Invoke();
		}
		if (0f < triggerChancePercent)
		{
			if (triggerChancePercent < 100f)
			{
				float value = UnityEngine.Random.value;
				float num = triggerChancePercent * 0.01f;
				if (num < value)
				{
					goto IL_010d;
				}
			}
			int successfulTriggers = _successfulTriggers + 1;
			_successfulTriggers = successfulTriggers;
			if (onTriggered != null)
			{
				onTriggered.Invoke();
			}
		}
		goto IL_010d;
		IL_010d:
		if (unlimitedTriggers || _successfulTriggers < maxTriggers)
		{
			return;
		}
		if (!_hasNotifiedMax)
		{
			_hasNotifiedMax = true;
			if (onMaxedOut != null)
			{
				onMaxedOut.Invoke();
			}
		}
		if (_runner != null)
		{
			StopCoroutine(_runner);
			_runner = null;
		}
	}

	private bool HasReachedMax()
	{
		//IL_0033: Expected O, but got I4
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Expected I4, but got Unknown
		if (!unlimitedTriggers)
		{
			object obj = _successfulTriggers - maxTriggers;
			int num = _successfulTriggers ^ maxTriggers;
			int num2 = _successfulTriggers ^ obj;
			int num3 = num & num2;
			bool flag = num3 < 0;
			bool flag2 = (nint)obj < 0;
			return flag2 == flag;
		}
		return false;
	}

	private void NotifyMaxedOutOnce()
	{
		if (!_hasNotifiedMax)
		{
			_hasNotifiedMax = true;
			if (onMaxedOut != null)
			{
				onMaxedOut.Invoke();
			}
		}
	}

	private float NextIntervalSeconds()
	{
		//IL_0010: Invalid comparison between I4 and F4
		//IL_001f: Expected F4, but got I4
		//IL_0061: Invalid comparison between I4 and F4
		//IL_0073: Expected F4, but got I4
		//IL_0487: Invalid comparison between I4 and F4
		//IL_0499: Expected F4, but got I4
		//IL_0127: Expected O, but got I4
		//IL_0135: Expected I, but got O
		//IL_015c: Expected I, but got O
		//IL_0172: Expected I, but got O
		//IL_0182: Expected O, but got I
		//IL_01a4: Expected I, but got O
		//IL_01d2: Expected O, but got I
		//IL_01bb: Expected I, but got O
		//IL_0210: Expected I, but got O
		//IL_0220: Expected O, but got I
		//IL_02c1: Expected I, but got O
		//IL_02d1: Expected O, but got I
		//IL_035b: Expected O, but got I4
		//IL_0360: Expected I, but got O
		//IL_03a0: Expected I, but got O
		//IL_03b0: Expected O, but got I
		float result;
		float num;
		if (intervalMode == IntervalMode.FixedInterval)
		{
			bool flag = 0f > fixedIntervalSeconds;
			result = 0f;
			if (flag)
			{
				goto IL_0477;
			}
		}
		else if (intervalMode == IntervalMode.RandomRangePerAttempt)
		{
			bool flag2 = !(0f < randomIntervalMinSeconds);
			num = 0f;
			if (!flag2)
			{
				num = randomIntervalMinSeconds;
			}
			bool flag3 = !(0f < randomIntervalMaxSeconds);
			float num2 = 0f;
			if (!flag3)
			{
				num2 = randomIntervalMaxSeconds;
			}
			if (!(num > num2))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
				object obj = default(object);
				if (obj == null)
				{
					result = UnityEngine.Random.Range(num, num2);
					goto IL_0477;
				}
			}
			else if (!_rangeWarningLogged)
			{
				object[] array = new object[4];
				bool flag4 = array == null;
				object obj2 = 4;
				nint num3 = (nint)typeof(object[]);
				if (!flag4)
				{
					bool flag5 = "RandomUnityEventScheduler" == null;
					nint num4 = unchecked((nint)"RandomUnityEventScheduler");
					if (!flag5)
					{
						nint num5 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v369 @ rdx_v35 (Il2CppClass<System.Object[]>)+40]");
						obj2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj3 = default(object);
						bool flag6 = obj3 == null;
						num3 = unchecked((nint)"RandomUnityEventScheduler");
						if (flag6)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							object obj4 = default(object);
							throw obj4;
						}
						num4 = unchecked((nint)"RandomUnityEventScheduler");
					}
					if (array.Length > 0)
					{
						array[0] = num4;
						float num6 = randomIntervalMinSeconds;
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object obj5 = default(object);
						if (obj5 != null)
						{
							nint num7 = (nint)array;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v509 @ rdx_v33 (Il2CppClass<System.Object[]>)+40]");
							object obj6 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj7 = default(object);
							bool flag7 = obj7 == null;
							object obj8 = obj5;
							if (flag7)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								object obj9 = default(object);
								throw obj9;
							}
						}
						if (array.Length > 1)
						{
							array[1] = obj5;
							num6 = randomIntervalMaxSeconds;
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							object obj10 = default(object);
							if (obj10 != null)
							{
								nint num8 = (nint)array;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v604 @ rdx_v31 (Il2CppClass<System.Object[]>)+40]");
								object obj11 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								object obj12 = default(object);
								bool flag8 = obj12 == null;
								object obj13 = obj10;
								if (flag8)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
									string text = default(string);
									throw text;
								}
							}
							if (array.Length > 2)
							{
								array[2] = obj10;
								GameObject gameObject = base.gameObject;
								bool flag9 = (object)gameObject == null;
								obj2 = 0;
								num3 = (nint)this;
								if (flag9)
								{
									goto IL_0449;
								}
								string text2 = gameObject.name;
								if (text2 != null)
								{
									nint num9 = (nint)array;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v652 @ rdx_v29 (Il2CppClass<System.Object[]>)+40]");
									object obj14 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
									object obj15 = default(object);
									bool flag10 = obj15 == null;
									string text3 = text2;
									if (flag10)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
										object obj16 = default(object);
										throw obj16;
									}
								}
								if (array.Length > 3)
								{
									array[3] = text2;
									string message = string.Format("[{0}] Min ({1}) > Max ({2}) on '{3}'. Using Min as fixed interval until corrected.", array);
									Debug.LogWarning(message, this);
									_rangeWarningLogged = true;
									goto IL_04a7;
								}
							}
						}
					}
					throw new IndexOutOfRangeException();
				}
				goto IL_0449;
			}
			goto IL_04a7;
		}
		return fixedIntervalSeconds;
		IL_0477:
		return result;
		IL_0449:
		throw new NullReferenceException();
		IL_04a7:
		result = num;
		goto IL_0477;
	}

	private bool TryPassChance(float percent)
	{
		//IL_0009: Invalid comparison between I4 and F4
		if (0f < percent)
		{
			if (percent < 100f)
			{
				float value = UnityEngine.Random.value;
				float num = percent * 0.01f;
				bool flag = num < value;
				return !flag;
			}
			return true;
		}
		return false;
	}

	private object WaitForSecondsDynamic(float seconds)
	{
		//IL_005f: Invalid comparison between I4 and F4
		if (0f < seconds)
		{
			if (clock != Clock.UnscaledTime)
			{
				return new WaitForSeconds(seconds);
			}
			return new WaitForSecondsRealtime(seconds);
		}
		return null;
	}

	public RandomUnityEventScheduler()
	{
		UnityEvent unityEvent = new UnityEvent();
		onStarted = unityEvent;
		onAttempt = new UnityEvent();
		onTriggered = new UnityEvent();
		onMaxedOut = new UnityEvent();
		onReset = new UnityEvent();
		base._002Ector();
	}
}
