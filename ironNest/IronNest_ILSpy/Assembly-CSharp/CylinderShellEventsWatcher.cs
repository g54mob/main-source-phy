using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class CylinderShellEventsWatcher : MonoBehaviour
{
	private sealed class _003CPeriodicCheckLoop_003Ed__28 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CylinderShellEventsWatcher _003C_003E4__this;

		private WaitForSeconds _003Cwait_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CPeriodicCheckLoop_003Ed__28(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_00f1: Expected I4, but got I8
			//IL_0573: Expected I4, but got O
			//IL_018d: Invalid comparison between I4 and F4
			//IL_0375: Invalid comparison between I4 and F4
			CylinderShellEventsWatcher cylinderShellEventsWatcher = _003C_003E4__this;
			bool flag2;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					if ((cylinderShellEventsWatcher._periodicInitialized ? 1 : 0) == _003C_003E1__state)
					{
						bool lastEmpty_Periodic = _003C_003E4__this.IsCylinderEmpty();
						cylinderShellEventsWatcher._lastEmpty_Periodic = lastEmpty_Periodic;
						int lastWatchedCount_Periodic = _003C_003E4__this.CountWatchedTypeRemaining();
						cylinderShellEventsWatcher._lastWatchedCount_Periodic = lastWatchedCount_Periodic;
						cylinderShellEventsWatcher._periodicInitialized = true;
					}
					bool flag = !(0.02f < cylinderShellEventsWatcher.periodicIntervalSeconds);
					float seconds = 0.02f;
					if (!flag)
					{
						seconds = cylinderShellEventsWatcher.periodicIntervalSeconds;
					}
					WaitForSeconds waitForSeconds = new WaitForSeconds(seconds);
					_003Cwait_003E5__2 = waitForSeconds;
					goto IL_05cd;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_0557;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					flag2 = _003C_003E4__this.IsCylinderEmpty();
					bool flag3 = cylinderShellEventsWatcher._lastEmpty_Periodic;
					bool flag4 = false;
					if (!flag3)
					{
						flag4 = flag2;
					}
					if (flag4)
					{
						if (cylinderShellEventsWatcher._pendingPeriodicEmpty)
						{
							goto IL_0238;
						}
						if (0f < cylinderShellEventsWatcher.periodicEventsDelaySeconds)
						{
							cylinderShellEventsWatcher._pendingPeriodicEmpty = true;
							float time = Time.time;
							float pendingPeriodicEmptyDeadline = time + cylinderShellEventsWatcher.periodicEventsDelaySeconds;
							cylinderShellEventsWatcher._pendingPeriodicEmptyDeadline = pendingPeriodicEmptyDeadline;
						}
						else if (cylinderShellEventsWatcher.onPeriodicEmpty != null)
						{
							cylinderShellEventsWatcher.onPeriodicEmpty.Invoke();
						}
					}
					if (cylinderShellEventsWatcher._pendingPeriodicEmpty)
					{
						goto IL_0238;
					}
					goto IL_060e;
				}
			}
			goto IL_0565;
			IL_0238:
			if (flag2)
			{
				float time2 = Time.time;
				if (time2 < cylinderShellEventsWatcher._pendingPeriodicEmptyDeadline)
				{
					goto IL_060e;
				}
				if (cylinderShellEventsWatcher.onPeriodicEmpty != null)
				{
					cylinderShellEventsWatcher.onPeriodicEmpty.Invoke();
				}
			}
			cylinderShellEventsWatcher._pendingPeriodicEmpty = false;
			goto IL_060e;
			IL_0557:
			return false;
			IL_0420:
			int num;
			if (num <= 0)
			{
				float time3 = Time.time;
				if (time3 < cylinderShellEventsWatcher._pendingPeriodicWatchedDepletedDeadline)
				{
					goto IL_0620;
				}
				if (cylinderShellEventsWatcher.onPeriodicWatchedTypeDepleted != null)
				{
					cylinderShellEventsWatcher.onPeriodicWatchedTypeDepleted.Invoke();
				}
			}
			cylinderShellEventsWatcher._pendingPeriodicWatchedDepleted = false;
			goto IL_0620;
			IL_0565:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_060e:
			cylinderShellEventsWatcher._lastEmpty_Periodic = flag2;
			if (!(cylinderShellEventsWatcher.watchedShellBlueprintPrefab != null))
			{
				goto IL_05cd;
			}
			num = _003C_003E4__this.CountWatchedTypeRemaining();
			if (cylinderShellEventsWatcher._lastWatchedCount_Periodic > 0 && num == 0)
			{
				if ((cylinderShellEventsWatcher._pendingPeriodicWatchedDepleted ? 1 : 0) != num)
				{
					goto IL_0420;
				}
				if (0f < cylinderShellEventsWatcher.periodicEventsDelaySeconds)
				{
					cylinderShellEventsWatcher._pendingPeriodicWatchedDepleted = true;
					float time4 = Time.time;
					float pendingPeriodicWatchedDepletedDeadline = time4 + cylinderShellEventsWatcher.periodicEventsDelaySeconds;
					cylinderShellEventsWatcher._pendingPeriodicWatchedDepletedDeadline = pendingPeriodicWatchedDepletedDeadline;
				}
				else if (cylinderShellEventsWatcher.onPeriodicWatchedTypeDepleted != null)
				{
					cylinderShellEventsWatcher.onPeriodicWatchedTypeDepleted.Invoke();
				}
			}
			if (cylinderShellEventsWatcher._pendingPeriodicWatchedDepleted)
			{
				goto IL_0420;
			}
			goto IL_0620;
			IL_05cd:
			if (_003C_003E4__this.enabled)
			{
				GameObject gameObject = _003C_003E4__this.gameObject;
				if ((object)gameObject == null)
				{
					goto IL_0565;
				}
				if (gameObject.activeInHierarchy && cylinderShellEventsWatcher.enablePeriodicChecks)
				{
					_003C_003E2__current = _003Cwait_003E5__2;
					_003C_003E1__state = 1;
					return true;
				}
			}
			cylinderShellEventsWatcher._periodicRoutine = null;
			goto IL_0557;
			IL_0620:
			cylinderShellEventsWatcher._lastWatchedCount_Periodic = num;
			goto IL_05cd;
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

	private sealed class _003CWaitForRotationThenCheck_003Ed__32 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CylinderShellEventsWatcher _003C_003E4__this;

		private int[] _003Cbefore_003E5__2;

		private float _003Cdeadline_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CWaitForRotationThenCheck_003Ed__32(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0370: Expected I4, but got I8
			//IL_0039: Expected O, but got I4
			//IL_01e5: Expected I4, but got I8
			//IL_0076: Expected I4, but got I8
			//IL_026e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0273: Expected O, but got Unknown
			//IL_028b: Expected O, but got I4
			//IL_0294: Expected O, but got I4
			//IL_0440: Expected I4, but got O
			//IL_030b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0310: Expected O, but got Unknown
			//IL_0319: Unknown result type (might be due to invalid IL or missing references)
			//IL_031e: Expected O, but got Unknown
			CylinderShellEventsWatcher cylinderShellEventsWatcher = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					if ((nint)obj == 1)
					{
						_003C_003E1__state = -1;
						if ((bool)cylinderShellEventsWatcher.selector)
						{
							if (cylinderShellEventsWatcher.IsCylinderEmpty() && cylinderShellEventsWatcher.onRotatedAndEmpty != null)
							{
								cylinderShellEventsWatcher.onRotatedAndEmpty.Invoke();
							}
							if (cylinderShellEventsWatcher.watchedShellBlueprintPrefab != null)
							{
								int num = cylinderShellEventsWatcher.CountWatchedTypeRemaining();
								if (cylinderShellEventsWatcher._previousWatchedCount_Rotation > 0 && num == 0 && cylinderShellEventsWatcher.onWatchedTypeDepleted != null)
								{
									cylinderShellEventsWatcher.onWatchedTypeDepleted.Invoke();
								}
								cylinderShellEventsWatcher._previousWatchedCount_Rotation = num;
							}
						}
						cylinderShellEventsWatcher._rotationWaitRoutine = null;
					}
					return false;
				}
				_003C_003E1__state = -1;
				int[] array = SnapshotBullets(cylinderShellEventsWatcher.selector);
				int[] array2 = _003Cbefore_003E5__2;
				if (_003Cbefore_003E5__2 != null)
				{
					if (array == null || array2.Length != array.Length)
					{
						goto IL_0349;
					}
					object obj2 = array + 32;
					object obj3 = (object)_003Cbefore_003E5__2 - (object)array;
					object obj4 = 0;
					object obj5 = 0;
					while ((nint)obj4 < array2.Length)
					{
						if ((nint)obj5 < array2.Length && (nint)obj5 < array.Length)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v237 @ rdx_v10+v236 @ r8_v5]");
							if (0 == (nint)obj2)
							{
								obj5++;
								obj2 += 4;
								obj4 = obj5;
								continue;
							}
							goto IL_0349;
						}
						IndexOutOfRangeException ex = new IndexOutOfRangeException();
						return (byte)(int)ex != 0;
					}
				}
				else if (_003Cbefore_003E5__2 != array)
				{
					goto IL_0349;
				}
			}
			else
			{
				_003C_003E1__state = -1;
				int[] array3 = SnapshotBullets(cylinderShellEventsWatcher.selector);
				_003Cbefore_003E5__2 = array3;
				float time = Time.time;
				bool flag2 = 0.05f > cylinderShellEventsWatcher.rotationTimeoutSeconds;
				float num2 = 0.05f;
				if (!flag2)
				{
					num2 = cylinderShellEventsWatcher.rotationTimeoutSeconds;
				}
				float num3 = num2 + time;
				_003Cdeadline_003E5__3 = num3;
			}
			float time2 = Time.time;
			if (_003Cdeadline_003E5__3 > time2)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			goto IL_0349;
			IL_0349:
			_003C_003E2__current = null;
			_003C_003E1__state = 2;
			return true;
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

	public CylinderShellSelector selector;

	public bool useTagLookup;

	public string selectorTag;

	public ShellBlueprint watchedShellBlueprintPrefab;

	public bool matchByVisualPrefab;

	public UnityEvent onRotatedAndEmpty;

	public UnityEvent onWatchedTypeDepleted;

	public float rotationTimeoutSeconds;

	public bool enablePeriodicChecks;

	public float periodicIntervalSeconds;

	public UnityEvent onPeriodicEmpty;

	public UnityEvent onPeriodicWatchedTypeDepleted;

	public float periodicEventsDelaySeconds;

	private int _previousWatchedCount_Rotation;

	private Coroutine _rotationWaitRoutine;

	private Coroutine _periodicRoutine;

	private bool _lastEmpty_Periodic;

	private int _lastWatchedCount_Periodic;

	private bool _periodicInitialized;

	private bool _pendingPeriodicEmpty;

	private float _pendingPeriodicEmptyDeadline;

	private bool _pendingPeriodicWatchedDepleted;

	private float _pendingPeriodicWatchedDepletedDeadline;

	private void Awake()
	{
		ResolveSelectorReference();
		SubscribeToSelectorMove();
	}

	private void OnEnable()
	{
		if (!selector)
		{
			ResolveSelectorReference();
			SubscribeToSelectorMove();
		}
		TryStartPeriodic();
	}

	private void OnDisable()
	{
		if (_rotationWaitRoutine != null)
		{
			StopCoroutine(_rotationWaitRoutine);
			_rotationWaitRoutine = null;
		}
		if (_periodicRoutine != null)
		{
			StopCoroutine(_periodicRoutine);
			_periodicRoutine = null;
		}
		_periodicInitialized = false;
		_pendingPeriodicWatchedDepleted = false;
	}

	private void Start()
	{
		int previousWatchedCount_Rotation = CountWatchedTypeRemaining();
		_previousWatchedCount_Rotation = previousWatchedCount_Rotation;
		TryStartPeriodic();
	}

	private void TryStartPeriodic()
	{
		if (enablePeriodicChecks && _periodicRoutine == null)
		{
			_003CPeriodicCheckLoop_003Ed__28 obj = new _003CPeriodicCheckLoop_003Ed__28(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine periodicRoutine = StartCoroutine(obj);
			_periodicRoutine = periodicRoutine;
		}
	}

	private IEnumerator PeriodicCheckLoop()
	{
		_003CPeriodicCheckLoop_003Ed__28 obj = new _003CPeriodicCheckLoop_003Ed__28(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private void ResolveSelectorReference()
	{
		CylinderShellSelector cylinderShellSelector = default(CylinderShellSelector);
		if (!selector)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			selector = cylinderShellSelector;
		}
		bool flag = selector;
		if (flag || useTagLookup == flag || string.IsNullOrEmpty(selectorTag))
		{
			return;
		}
		GameObject gameObject = GameObject.FindWithTag(selectorTag);
		if (!gameObject)
		{
			string message = "CylinderShellEventsWatcher: No GameObject found with tag '" + selectorTag + "'.";
			Debug.LogWarning(message);
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		selector = cylinderShellSelector;
		if (!selector)
		{
			string message2 = "CylinderShellEventsWatcher: Tagged object '" + selectorTag + "' has no CylinderShellSelector component.";
			Debug.LogWarning(message2);
		}
	}

	private void SubscribeToSelectorMove()
	{
		if ((bool)selector)
		{
			CylinderShellSelector cylinderShellSelector = selector;
			if (!(cylinderShellSelector.moveButton != null))
			{
				Debug.LogWarning("CylinderShellEventsWatcher: selector.moveButton is not assigned; cannot detect rotation start.");
				return;
			}
			CylinderShellSelector cylinderShellSelector2 = selector;
			UnityAction action = OnSelectorRotateRequested;
			cylinderShellSelector2.moveButton.RegisterOnClickDown(action);
		}
	}

	private void OnSelectorRotateRequested()
	{
		if (_rotationWaitRoutine != null)
		{
			StopCoroutine(_rotationWaitRoutine);
		}
		_003CWaitForRotationThenCheck_003Ed__32 obj = new _003CWaitForRotationThenCheck_003Ed__32(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine rotationWaitRoutine = StartCoroutine(obj);
		_rotationWaitRoutine = rotationWaitRoutine;
	}

	private IEnumerator WaitForRotationThenCheck()
	{
		_003CWaitForRotationThenCheck_003Ed__32 obj = new _003CWaitForRotationThenCheck_003Ed__32(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private void DoRotationChecks()
	{
		if (!selector)
		{
			return;
		}
		if (IsCylinderEmpty() && onRotatedAndEmpty != null)
		{
			onRotatedAndEmpty.Invoke();
		}
		if (watchedShellBlueprintPrefab != null)
		{
			int num = CountWatchedTypeRemaining();
			if (_previousWatchedCount_Rotation > 0 && num == 0 && onWatchedTypeDepleted != null)
			{
				onWatchedTypeDepleted.Invoke();
			}
			_previousWatchedCount_Rotation = num;
		}
	}

	private static int[] SnapshotBullets(CylinderShellSelector sel)
	{
		//IL_00d3: Expected O, but got I4
		//IL_00dd: Expected O, but got I4
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Expected O, but got Unknown
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Expected O, but got Unknown
		int[] array;
		if (sel != null)
		{
			if ((object)sel == null)
			{
				goto IL_01e3;
			}
			if (sel.slots != null)
			{
				Transform[] slots = sel.slots;
				if (slots.Length != 0)
				{
					array = new int[slots.Length];
					List<GameObject> bullets = sel.bullets;
					if (slots.Length > 0)
					{
						object obj = 0;
						object obj2 = 0;
						UnityEngine.Object obj3 = default(UnityEngine.Object);
						UnityEngine.Object obj4 = default(UnityEngine.Object);
						int[] array2 = default(int[]);
						while (true)
						{
							int num;
							if (sel.bullets != null && (nint)obj2 < bullets._size)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								if (obj3 != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
									if ((object)obj4 == null)
									{
										break;
									}
									num = obj4.GetInstanceID();
									if (array == null)
									{
										break;
									}
									goto IL_01f1;
								}
							}
							if (array == null)
							{
								break;
							}
							num = 0;
							goto IL_01f1;
							IL_01f1:
							obj2++;
							array2[obj] = num;
							obj++;
							if ((nint)obj < slots.Length)
							{
								continue;
							}
							goto IL_01d9;
						}
						goto IL_01e3;
					}
					goto IL_01d9;
				}
			}
		}
		array = new int[0];
		goto IL_01d9;
		IL_01d9:
		return array;
		IL_01e3:
		return (int[])(object)new NullReferenceException();
	}

	private static bool SequenceEqual(int[] a, int[] b)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Expected O, but got Unknown
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_008f: Expected O, but got I4
		//IL_0157: Expected I4, but got O
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Expected O, but got Unknown
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Expected O, but got Unknown
		if (a != null && b != null)
		{
			if (a.Length == b.Length)
			{
				object obj = a + 24;
				object obj2 = b + 32;
				object obj3 = (object)a - (object)b;
				object obj4 = 0;
				while (true)
				{
					if ((nint)obj4 < a.Length)
					{
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) && (nint)obj4 < b.Length)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v129 @ r8_v3+v63 @ rdx_v3]");
							if (0 != (nint)obj2)
							{
								break;
							}
							obj4++;
							obj2 += 4;
							continue;
						}
						IndexOutOfRangeException ex = new IndexOutOfRangeException();
						return (byte)(int)ex != 0;
					}
					return true;
				}
			}
			return false;
		}
		object obj5 = (object)a - (object)b;
		return obj5 == null;
	}

	private bool IsCylinderEmpty()
	{
		//IL_018e: Expected I4, but got O
		//IL_00b7: Expected I4, but got O
		//IL_00c0: Expected O, but got I4
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		if ((bool)selector)
		{
			CylinderShellSelector cylinderShellSelector = selector;
			if ((object)selector == null)
			{
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			if (cylinderShellSelector.slots != null)
			{
				List<GameObject> bullets = cylinderShellSelector.bullets;
				UnityEngine.Object slots = (UnityEngine.Object)(object)cylinderShellSelector.slots;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ rcx_v7 (UnityEngine.Object)+18]");
				bool flag = (nint)0 <= (nint)0;
				bool flag2 = (byte)(int)selector != 0;
				object obj = 0;
				if (!flag)
				{
					UnityEngine.Object obj2 = default(UnityEngine.Object);
					object obj3;
					do
					{
						if (cylinderShellSelector.bullets != null && (nint)obj < bullets._size)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							if (obj2 != null)
							{
								return false;
							}
						}
						obj++;
						obj3 = obj;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ rcx_v7 (UnityEngine.Object)+18]");
					}
					while ((nint)obj3 < 0);
				}
			}
		}
		return true;
	}

	private int CountWatchedTypeRemaining()
	{
		//IL_0298: Expected I4, but got O
		//IL_011a: Expected O, but got I4
		//IL_0123: Expected O, but got I4
		//IL_014d: Expected O, but got I4
		//IL_013f: Expected O, but got I4
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Expected O, but got Unknown
		if ((bool)selector)
		{
			CylinderShellSelector cylinderShellSelector = selector;
			if ((object)selector == null)
			{
				goto IL_028a;
			}
			if (cylinderShellSelector.slots != null)
			{
				Transform[] slots = cylinderShellSelector.slots;
				if (slots.Length != 0 && watchedShellBlueprintPrefab != null)
				{
					CylinderShellSelector cylinderShellSelector2 = selector;
					if ((object)selector != null)
					{
						List<GameObject> bullets = cylinderShellSelector2.bullets;
						CylinderShellSelector cylinderShellSelector3 = selector;
						int num = 0;
						object obj = 0;
						object obj2 = 0;
						UnityEngine.Object obj4 = default(UnityEngine.Object);
						UnityEngine.Object obj5 = default(UnityEngine.Object);
						while (true)
						{
							object obj3;
							if (cylinderShellSelector3.slots != null)
							{
								Transform[] slots2 = cylinderShellSelector3.slots;
								obj3 = slots2.Length;
							}
							else
							{
								obj3 = 0;
							}
							if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
							{
								if (cylinderShellSelector2.bullets != null && (nint)obj < bullets._size)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
									if ((bool)obj4)
									{
										if ((object)obj4 == null)
										{
											break;
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D90C0");
										if (obj5 != null && IsWatchedType((ShellBlueprint)obj5))
										{
											num++;
										}
									}
								}
								cylinderShellSelector3 = selector;
								obj++;
								if ((object)selector == null)
								{
									break;
								}
								obj2 = obj;
								continue;
							}
							return num;
						}
					}
					goto IL_028a;
				}
			}
		}
		return 0;
		IL_028a:
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	private bool IsWatchedType(ShellBlueprint instanceBlueprint)
	{
		//IL_028d: Expected I4, but got O
		string text2;
		if ((bool)instanceBlueprint && (bool)watchedShellBlueprintPrefab)
		{
			if ((object)instanceBlueprint != null)
			{
				string text = instanceBlueprint.name;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AB54]");
				if ((nint)0 == 0)
				{
					_ = 1;
				}
				if (text != null)
				{
					bool flag = text.EndsWith("(Clone)");
					bool flag2 = !flag;
					text2 = text;
					if (flag2)
					{
						goto IL_02af;
					}
					object obj = "(Clone)";
					if ("(Clone)" != null)
					{
						int stringLength = text._stringLength;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v192 @ rax_v23+10]");
						int length = (int)((nint)stringLength - (nint)0);
						string text3 = text.Substring(0, length);
						if (text3 != null)
						{
							string text4 = text3.TrimEnd();
							text2 = text4;
							goto IL_02af;
						}
					}
				}
			}
			goto IL_027f;
		}
		return false;
		IL_02eb:
		string text5;
		return text2 == text5;
		IL_027f:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_02af:
		if ((object)watchedShellBlueprintPrefab != null)
		{
			string text6 = watchedShellBlueprintPrefab.name;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AB54]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			if (text6 != null)
			{
				bool flag3 = text6.EndsWith("(Clone)");
				bool flag4 = !flag3;
				text5 = text6;
				if (flag4)
				{
					goto IL_02eb;
				}
				object obj2 = "(Clone)";
				if ("(Clone)" != null)
				{
					int stringLength2 = text6._stringLength;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v194 @ rax_v19+10]");
					int length2 = (int)((nint)stringLength2 - (nint)0);
					string text7 = text6.Substring(0, length2);
					if (text7 != null)
					{
						string text8 = text7.TrimEnd();
						text5 = text8;
						goto IL_02eb;
					}
				}
			}
		}
		goto IL_027f;
	}

	private static string StripClone(string name)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AB54]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (name != null)
		{
			if (!name.EndsWith("(Clone)"))
			{
				return name;
			}
			object obj = "(Clone)";
			if ("(Clone)" != null)
			{
				int stringLength = name._stringLength;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v6+10]");
				int length = (int)((nint)stringLength - (nint)0);
				string text = name.Substring(0, length);
				if (text != null)
				{
					return text.TrimEnd();
				}
			}
		}
		return (string)(object)new NullReferenceException();
	}

	public CylinderShellEventsWatcher()
	{
		//IL_0079: Expected I4, but got I8
		//IL_0088: Expected I4, but got I8
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AB55]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		selectorTag = "CylinderShellSelector";
		matchByVisualPrefab = true;
		rotationTimeoutSeconds = 1f;
		periodicIntervalSeconds = 0.5f;
		periodicEventsDelaySeconds = 6f;
		_previousWatchedCount_Rotation = -1;
		_lastWatchedCount_Periodic = -1;
		base._002Ector();
	}
}
