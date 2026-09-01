using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class LookAtTarget : MonoBehaviour, ILookHandler, IClickHandler
{
	private enum MalfunctionType
	{
		None,
		DeadPress,
		DoublePress
	}

	private sealed class _003CAutoFindCursorManagerRoutine_003Ed__41 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public LookAtTarget _003C_003E4__this;

		private WaitForSecondsRealtime _003Cwait_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CAutoFindCursorManagerRoutine_003Ed__41(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_00a9: Expected I4, but got I8
			//IL_0201: Expected I4, but got O
			//IL_0181: Unknown result type (might be due to invalid IL or missing references)
			//IL_0186: Expected Ref, but got Unknown
			LookAtTarget lookAtTarget = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					bool flag = !(0.05f < lookAtTarget.autoFindRetrySeconds);
					float time = 0.05f;
					if (!flag)
					{
						time = lookAtTarget.autoFindRetrySeconds;
					}
					WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(time);
					_003Cwait_003E5__2 = waitForSecondsRealtime;
					goto IL_00c8;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_01c0;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_00c8;
				}
			}
			goto IL_01f3;
			IL_01c0:
			return false;
			IL_01f3:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_00c8:
			if (lookAtTarget.useCursorManagerIntegration && lookAtTarget.cursorManager == null)
			{
				GameObject gameObject = GameObject.FindWithTag(lookAtTarget.cursorManagerTag);
				if (gameObject != null)
				{
					if ((object)gameObject == null)
					{
						goto IL_01f3;
					}
					if (gameObject.TryGetComponent<DynamicCursorManager>(out *(DynamicCursorManager*)(_003C_003E4__this + 40)))
					{
						_003C_003E4__this.SubscribeToCursorManager();
						goto IL_01c0;
					}
				}
				_003C_003E2__current = _003Cwait_003E5__2;
				_003C_003E1__state = 1;
				return true;
			}
			lookAtTarget._findRoutine = null;
			return false;
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

	private bool useCursorManagerIntegration;

	private DynamicCursorManager cursorManager;

	private bool autoFindCursorManagerByTag;

	private string cursorManagerTag;

	private float autoFindRetrySeconds;

	private Interactable interactable;

	private bool alwaysReleaseToSameTarget;

	private bool _subscribedToManager;

	private Coroutine _findRoutine;

	private bool _isHovered;

	private bool _pressBeganHere;

	public UnityEvent onLookAt;

	public UnityEvent onLookAway;

	public UnityEvent onClickDown;

	public UnityEvent onClickUp;

	public UnityEvent onActivated;

	public UnityEvent onDeactivated;

	private Animator animator;

	private bool isActive;

	private bool debugLogs;

	private bool useClickCooldown;

	private float clickCooldownSeconds;

	private float nextAllowedClickTime;

	private bool isClicked;

	private bool useMalfunctions;

	private bool linkToHighPressureSystem;

	private HighPressureSystemManager pressureSystemManager;

	private string pressureSystemId;

	private bool debugMalfunctions;

	private AnimationCurve globalMalfunctionChanceCurve;

	private AnimationCurve deadPressChanceCurve;

	private AnimationCurve doublePressChanceCurve;

	private UnityEvent onDeadPress;

	private UnityEvent onDoublePress;

	private MalfunctionType currentMalfunction;

	private void Awake()
	{
		if (this.interactable == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696290");
			Interactable interactable = default(Interactable);
			this.interactable = interactable;
		}
	}

	private unsafe void OnEnable()
	{
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected Ref, but got Unknown
		if (!useCursorManagerIntegration)
		{
			return;
		}
		if (cursorManager == null && autoFindCursorManagerByTag && !string.IsNullOrWhiteSpace(cursorManagerTag))
		{
			GameObject gameObject = GameObject.FindWithTag(cursorManagerTag);
			if (gameObject != null)
			{
				bool flag = gameObject.TryGetComponent<DynamicCursorManager>(out *(DynamicCursorManager*)(this + 40));
			}
		}
		bool flag2 = cursorManager != null;
		if (!flag2)
		{
			if (_findRoutine == null && autoFindCursorManagerByTag != flag2)
			{
				_003CAutoFindCursorManagerRoutine_003Ed__41 obj = new _003CAutoFindCursorManagerRoutine_003Ed__41(0);
				obj._003C_003E4__this = this;
				Coroutine findRoutine = StartCoroutine(obj);
				_findRoutine = findRoutine;
			}
		}
		else
		{
			SubscribeToCursorManager();
		}
	}

	private void OnDisable()
	{
		UnsubscribeFromCursorManager();
		if (_pressBeganHere)
		{
			_pressBeganHere = false;
			if (onClickUp != null)
			{
				onClickUp.Invoke();
			}
		}
	}

	private void OnDestroy()
	{
		UnsubscribeFromCursorManager();
	}

	private unsafe void EnsureCursorManagerSubscription()
	{
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected Ref, but got Unknown
		if (cursorManager == null && autoFindCursorManagerByTag && !string.IsNullOrWhiteSpace(cursorManagerTag))
		{
			GameObject gameObject = GameObject.FindWithTag(cursorManagerTag);
			if (gameObject != null)
			{
				bool flag = gameObject.TryGetComponent<DynamicCursorManager>(out *(DynamicCursorManager*)(this + 40));
			}
		}
		bool flag2 = cursorManager != null;
		if (!flag2)
		{
			if (_findRoutine == null && autoFindCursorManagerByTag != flag2)
			{
				_003CAutoFindCursorManagerRoutine_003Ed__41 obj = new _003CAutoFindCursorManagerRoutine_003Ed__41(0);
				obj._003C_003E4__this = this;
				Coroutine findRoutine = StartCoroutine(obj);
				_findRoutine = findRoutine;
			}
		}
		else
		{
			SubscribeToCursorManager();
		}
	}

	private IEnumerator AutoFindCursorManagerRoutine()
	{
		_003CAutoFindCursorManagerRoutine_003Ed__41 obj = new _003CAutoFindCursorManagerRoutine_003Ed__41(0);
		if (obj != null)
		{
			obj._003C_003E4__this = this;
			return obj;
		}
		return (IEnumerator)new NullReferenceException();
	}

	private void SubscribeToCursorManager()
	{
		if (!_subscribedToManager && cursorManager != null)
		{
			Action<Interactable> value = HandleHoverChangedFromManager;
			cursorManager.OnCursorTargetChanged += value;
			Action<Interactable> value2 = HandleClickDownFromManager;
			cursorManager.OnPrimaryClickDown += value2;
			Action<Interactable> value3 = HandleClickUpFromManager;
			cursorManager.OnPrimaryClickUp += value3;
			_subscribedToManager = true;
			bool flag = interactable != null;
			if (flag)
			{
				DynamicCursorManager dynamicCursorManager = cursorManager;
				flag = dynamicCursorManager._currentHover == interactable;
			}
			UpdateHover(flag, force: true);
		}
	}

	private void UnsubscribeFromCursorManager()
	{
		if (_subscribedToManager && cursorManager != null)
		{
			Action<Interactable> value = HandleHoverChangedFromManager;
			cursorManager.OnCursorTargetChanged -= value;
			Action<Interactable> value2 = HandleClickDownFromManager;
			cursorManager.OnPrimaryClickDown -= value2;
			Action<Interactable> value3 = HandleClickUpFromManager;
			cursorManager.OnPrimaryClickUp -= value3;
			bool flag = _findRoutine == null;
			_subscribedToManager = false;
			if (!flag)
			{
				StopCoroutine(_findRoutine);
				_findRoutine = null;
			}
		}
	}

	private void HandleHoverChangedFromManager(Interactable current)
	{
		bool flag = interactable != null;
		if (flag)
		{
			flag = current == interactable;
		}
		UpdateHover(flag, force: false);
	}

	private void UpdateHover(bool nowHovered, bool force)
	{
		if (!force && nowHovered == _isHovered)
		{
			return;
		}
		_isHovered = nowHovered;
		UnityEvent unityEvent;
		if (nowHovered)
		{
			if (debugLogs)
			{
				GameObject gameObject = base.gameObject;
				string text = gameObject.name;
				string message = text + " - Looked At";
				Debug.Log(message);
			}
			unityEvent = onLookAt;
		}
		else
		{
			if (debugLogs)
			{
				GameObject gameObject2 = base.gameObject;
				string text2 = gameObject2.name;
				string message2 = text2 + " - Looked Away";
				Debug.Log(message2);
			}
			unityEvent = onLookAway;
		}
		unityEvent?.Invoke();
	}

	private void HandleClickDownFromManager(Interactable pressTarget)
	{
		if (isActive && interactable != null && pressTarget == interactable)
		{
			OnClickDown();
			_pressBeganHere = true;
		}
	}

	private void HandleClickUpFromManager(Interactable pressSourceTarget)
	{
		if (_pressBeganHere)
		{
			if (alwaysReleaseToSameTarget || _isHovered)
			{
				OnClickUp();
			}
			_pressBeganHere = false;
		}
	}

	public void SetActive(bool active)
	{
		bool flag = animator != null;
		bool flag2 = !flag;
		bool flag3 = false;
		if (!flag2)
		{
			animator.SetBool("IsActive", active);
			flag3 = active;
		}
		if (isActive != active)
		{
			isActive = active;
			if (~(debugLogs ? 1u : 0u) == 0)
			{
				GameObject gameObject = base.gameObject;
				string arg = gameObject.name;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg2 = default(object);
				string message = $"{arg} - IsActive set to {arg2}";
				Debug.Log(message);
			}
			(isActive ? onActivated : onDeactivated)?.Invoke();
		}
		else if (~(debugLogs ? 1u : 0u) == 0)
		{
			GameObject gameObject2 = base.gameObject;
			string arg3 = gameObject2.name;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg4 = default(object);
			string message2 = $"{arg3} - IsActive set to {arg4} (no change)";
			Debug.Log(message2);
		}
	}

	public bool GetActive()
	{
		return isActive;
	}

	public void RegisterOnClickDown(UnityAction action)
	{
		onClickDown.AddListener(action);
	}

	public void RegisterOnClickUp(UnityAction action)
	{
		onClickUp.AddListener(action);
	}

	public void RegisterOnDeadPress(UnityAction action)
	{
		onDeadPress.AddListener(action);
	}

	public void RegisterOnDoublePress(UnityAction action)
	{
		onDoublePress.AddListener(action);
	}

	public void RegisterOnActivated(UnityAction action)
	{
		onActivated.AddListener(action);
	}

	public void RegisterOnDeactivated(UnityAction action)
	{
		onDeactivated.AddListener(action);
	}

	public void OnLookAt()
	{
		if (debugLogs)
		{
			GameObject gameObject = base.gameObject;
			string text = gameObject.name;
			string message = text + " - Looked At";
			Debug.Log(message);
		}
		if (onLookAt != null)
		{
			onLookAt.Invoke();
		}
	}

	public void OnLookAway()
	{
		if (debugLogs)
		{
			GameObject gameObject = base.gameObject;
			string text = gameObject.name;
			string message = text + " - Looked Away";
			Debug.Log(message);
		}
		if (onLookAway != null)
		{
			onLookAway.Invoke();
		}
	}

	public void OnClickDown()
	{
		//IL_00f9: Expected O, but got I4
		string text;
		string text2;
		if (useClickCooldown)
		{
			float unscaledTime = Time.unscaledTime;
			if (nextAllowedClickTime > unscaledTime)
			{
				if (debugLogs)
				{
					GameObject gameObject = base.gameObject;
					text = gameObject.name;
					text2 = " - Click Down ignored due to cooldown";
					goto IL_0322;
				}
				return;
			}
		}
		UnityEvent unityEvent;
		if (isActive)
		{
			bool flag = !useMalfunctions;
			currentMalfunction = MalfunctionType.None;
			if (!flag)
			{
				MalfunctionType malfunctionType = EvaluateMalfunction();
				currentMalfunction = malfunctionType;
			}
			isClicked = true;
			bool flag2 = currentMalfunction == MalfunctionType.None;
			if (flag2)
			{
				goto IL_01ff;
			}
			object obj = currentMalfunction - 1;
			if (!flag2)
			{
				if ((nint)obj != 1)
				{
					goto IL_01ff;
				}
				if (debugMalfunctions)
				{
					string text3 = base.name;
					string message = "[LookAtTarget:" + text3 + "] Double Press malfunction triggered.";
					Debug.Log(message);
				}
				unityEvent = onDoublePress;
			}
			else
			{
				if (debugMalfunctions)
				{
					string text4 = base.name;
					string message2 = "[LookAtTarget:" + text4 + "] Dead Press malfunction triggered.";
					Debug.Log(message2);
				}
				unityEvent = onDeadPress;
			}
			goto IL_0305;
		}
		if (!debugLogs)
		{
			return;
		}
		GameObject gameObject2 = base.gameObject;
		text = gameObject2.name;
		text2 = " - Click Down ignored (inactive)";
		goto IL_0322;
		IL_01ff:
		if (debugLogs)
		{
			GameObject gameObject3 = base.gameObject;
			string text5 = gameObject3.name;
			string message3 = text5 + " - Click Down";
			Debug.Log(message3);
		}
		unityEvent = onClickDown;
		goto IL_0305;
		IL_0322:
		string message4 = text + text2;
		Debug.Log(message4);
		return;
		IL_0305:
		unityEvent?.Invoke();
	}

	public void OnClickUp()
	{
		//IL_00f8: Expected I4, but got O
		//IL_0159: Invalid comparison between I4 and F4
		//IL_016b: Expected F4, but got I4
		if (!isClicked)
		{
			return;
		}
		if (currentMalfunction == MalfunctionType.None)
		{
			if (debugLogs)
			{
				GameObject gameObject = base.gameObject;
				string text = gameObject.name;
				string message = text + " - Click Up";
				Debug.Log(message);
			}
			isClicked = false;
			if (onClickUp != null)
			{
				onClickUp.Invoke();
			}
		}
		else
		{
			if (debugMalfunctions)
			{
				string arg = base.name;
				object obj = default(object);
				object arg2 = (MalfunctionType)obj;
				string message2 = $"[LookAtTarget:{arg}] Click Up suppressed due to malfunction ({arg2}).";
				Debug.Log(message2);
			}
			isClicked = false;
			currentMalfunction = MalfunctionType.None;
		}
		if (useClickCooldown)
		{
			float unscaledTime = Time.unscaledTime;
			bool flag = !(0f < clickCooldownSeconds);
			float num = 0f;
			if (!flag)
			{
				num = clickCooldownSeconds;
			}
			float num2 = num + unscaledTime;
			nextAllowedClickTime = num2;
		}
	}

	public void ResetButton()
	{
		if (isClicked)
		{
			isClicked = false;
			currentMalfunction = MalfunctionType.None;
			if (onClickUp != null)
			{
				onClickUp.Invoke();
			}
		}
	}

	private unsafe MalfunctionType EvaluateMalfunction()
	{
		//IL_0a7b: Expected F4, but got I4
		//IL_014d: Invalid comparison between I4 and F4
		//IL_01d9: Expected F4, but got I4
		//IL_01ea: Expected O, but got I4
		//IL_018c: Expected O, but got I4
		//IL_00a5: Expected O, but got I4
		//IL_068b: Expected F4, but got I4
		//IL_0655: Invalid comparison between I4 and F4
		//IL_0667: Expected F4, but got I4
		//IL_01b4: Expected O, but got I4
		//IL_06e2: Expected F4, but got I4
		//IL_0b82: Expected O, but got I4
		//IL_00dd: Invalid comparison between I4 and F4
		//IL_0c7a: Invalid comparison between I4 and F4
		//IL_06ac: Invalid comparison between I4 and F4
		//IL_06be: Expected F4, but got I4
		//IL_0262: Expected I, but got O
		//IL_0272: Expected O, but got I
		//IL_029c: Expected O, but got I4
		//IL_01c2: Expected F4, but got I4
		//IL_01cb: Expected O, but got I4
		//IL_0b98: Expected O, but got I4
		//IL_02f6: Expected O, but got I
		//IL_010c: Expected O, but got I4
		//IL_031c: Expected O, but got Ref
		//IL_09ad: Invalid comparison between F4 and I4
		//IL_09c0: Expected O, but got I4
		//IL_09c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ce: Expected I4, but got Unknown
		//IL_012c: Expected O, but got I4
		//IL_0332: Expected I, but got O
		//IL_0342: Expected O, but got I
		//IL_0383: Expected O, but got I
		//IL_03e5: Expected O, but got I
		//IL_0bab: Expected I4, but got O
		//IL_040b: Expected O, but got Ref
		//IL_0421: Expected I, but got O
		//IL_0431: Expected O, but got I
		//IL_0472: Expected O, but got I
		//IL_077d: Expected I, but got O
		//IL_078d: Expected O, but got I
		//IL_04d4: Expected O, but got I
		//IL_04fa: Expected O, but got Ref
		//IL_0802: Expected I, but got O
		//IL_0812: Expected O, but got I
		//IL_0510: Expected I, but got O
		//IL_0520: Expected O, but got I
		//IL_0561: Expected O, but got I
		//IL_0887: Expected I, but got O
		//IL_0897: Expected O, but got I
		//IL_090c: Expected I, but got O
		//IL_091c: Expected O, but got I
		//IL_05f9: Expected O, but got I4
		bool flag = !linkToHighPressureSystem;
		float num = 1f;
		object obj = default(object);
		if (!flag)
		{
			if (pressureSystemManager == null && !string.IsNullOrWhiteSpace(pressureSystemId))
			{
				HighPressureSystemManager highPressureSystemManager = HighPressureSystemManager.FindBySystemId(pressureSystemId);
				pressureSystemManager = highPressureSystemManager;
			}
			bool flag2 = pressureSystemManager != null;
			bool flag3 = !flag2;
			num = 1f;
			obj = 0;
			if (!flag3)
			{
				HighPressureSystemManager highPressureSystemManager2 = pressureSystemManager;
				num = highPressureSystemManager2.currentHealth01;
				if (!(0f > highPressureSystemManager2.currentHealth01))
				{
					bool flag4 = !(num > 1f);
					obj = 0;
					if (!flag4)
					{
						num = 1f;
						obj = 0;
					}
				}
				else
				{
					num = 0f;
					obj = 0;
				}
			}
		}
		bool flag5 = globalMalfunctionChanceCurve == null;
		float num2 = 0f;
		float num4 = default(float);
		float num3 = num4;
		if (!flag5)
		{
			float num5 = globalMalfunctionChanceCurve.Evaluate(num);
			if (!(0f > num5))
			{
				bool flag6 = !(num5 > 1f);
				num2 = num5;
				num3 = num;
				obj = 0;
				if (!flag6)
				{
					num2 = 1f;
					num3 = num;
					obj = 0;
				}
			}
			else
			{
				num2 = 0f;
				num3 = num;
				obj = 0;
			}
		}
		float value = UnityEngine.Random.value;
		if (debugMalfunctions)
		{
			object[] array = new object[4];
			string text = base.name;
			bool flag7 = array == null;
			UnityEngine.Object obj2 = null;
			UnityEngine.Object obj3 = this;
			if (!flag7)
			{
				bool flag8 = text == null;
				obj2 = null;
				string text2 = (string)(object)this;
				if (!flag8)
				{
					nint num6 = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v668 @ rdx_v73 (Il2CppClass<System.Object[]>)+40]");
					obj2 = (UnityEngine.Object)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj4 = default(object);
					bool flag9 = obj4 == null;
					text2 = text;
					object obj5 = 0;
					if (flag9)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						UnityEngine.Object obj6 = default(UnityEngine.Object);
						throw obj6;
					}
				}
				bool flag10 = array.Length <= 0;
				obj3 = (UnityEngine.Object)(object)text2;
				if (!flag10)
				{
					array[0] = text;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
					obj3 = (UnityEngine.Object)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					UnityEngine.Object obj7 = default(UnityEngine.Object);
					bool flag11 = (object)obj7 == null;
					float num7 = default(float);
					obj2 = (UnityEngine.Object)(&num7);
					if (!flag11)
					{
						nint num8 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v932 @ rdx_v71 (Il2CppClass<System.Object[]>)+40]");
						obj2 = (UnityEngine.Object)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj8 = default(object);
						bool flag12 = obj8 == null;
						obj3 = obj7;
						float num9 = value;
						num4 = num3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v932 @ rdx_v71 (Il2CppClass<System.Object[]>)+40]");
						UnityEngine.Object obj9 = (UnityEngine.Object)0;
						object obj5 = obj;
						UnityEngine.Object obj10 = obj7;
						if (flag12)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							UnityEngine.Object obj11 = default(UnityEngine.Object);
							throw obj11;
						}
					}
					if (array.Length > 1)
					{
						array[1] = obj7;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
						obj3 = (UnityEngine.Object)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						UnityEngine.Object obj12 = default(UnityEngine.Object);
						bool flag13 = (object)obj12 == null;
						float num10 = default(float);
						obj2 = (UnityEngine.Object)(&num10);
						if (!flag13)
						{
							nint num11 = (nint)array;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1229 @ rdx_v69 (Il2CppClass<System.Object[]>)+40]");
							obj2 = (UnityEngine.Object)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj13 = default(object);
							bool flag14 = obj13 == null;
							obj3 = obj12;
							float num9 = value;
							num4 = num3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1229 @ rdx_v69 (Il2CppClass<System.Object[]>)+40]");
							UnityEngine.Object obj14 = (UnityEngine.Object)0;
							object obj5 = obj;
							UnityEngine.Object obj15 = obj12;
							if (flag14)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								UnityEngine.Object obj16 = default(UnityEngine.Object);
								throw obj16;
							}
						}
						if (array.Length > 2)
						{
							array[2] = obj12;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
							obj3 = (UnityEngine.Object)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							UnityEngine.Object obj17 = default(UnityEngine.Object);
							bool flag15 = (object)obj17 == null;
							float num12 = default(float);
							obj2 = (UnityEngine.Object)(&num12);
							if (!flag15)
							{
								nint num13 = (nint)array;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1410 @ rdx_v67 (Il2CppClass<System.Object[]>)+40]");
								obj2 = (UnityEngine.Object)0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								object obj18 = default(object);
								bool flag16 = obj18 == null;
								obj3 = obj17;
								float num9 = value;
								num4 = num3;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1410 @ rdx_v67 (Il2CppClass<System.Object[]>)+40]");
								UnityEngine.Object obj19 = (UnityEngine.Object)0;
								object obj5 = obj;
								UnityEngine.Object obj20 = obj17;
								if (flag16)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
									UnityEngine.Object obj21 = default(UnityEngine.Object);
									throw obj21;
								}
							}
							if (array.Length > 3)
							{
								array[3] = obj17;
								string message = string.Format("[LookAtTarget:{0}] Health={1:0.###} GlobalChance={2:0.###} Roll={3:0.###}", array);
								Debug.Log(message);
								num12 = value;
								num10 = num2;
								num7 = num;
								obj = 0;
								goto IL_05fe;
							}
						}
					}
				}
				throw new IndexOutOfRangeException();
			}
			throw new NullReferenceException();
		}
		goto IL_05fe;
		IL_05fe:
		if (!(value > num2))
		{
			float num15;
			if (deadPressChanceCurve != null)
			{
				float num14 = deadPressChanceCurve.Evaluate(num);
				bool flag17 = !(0f < num14);
				num15 = 0f;
				if (!flag17)
				{
					num15 = num14;
				}
				num3 = num;
				obj = 0;
			}
			else
			{
				num15 = 0f;
			}
			float num17;
			if (doublePressChanceCurve != null)
			{
				float num16 = doublePressChanceCurve.Evaluate(num);
				bool flag18 = !(0f < num16);
				num17 = 0f;
				if (!flag18)
				{
					num17 = num16;
				}
				num3 = num;
				obj = 0;
			}
			else
			{
				num17 = 0f;
			}
			float num18 = num17 + num15;
			if (0f < num18)
			{
				value = UnityEngine.Random.value;
				bool flag19 = !debugMalfunctions;
				float num19 = value * num18;
				if (!flag19)
				{
					object[] array2 = new object[4];
					string text3 = base.name;
					if (array2 == null)
					{
						NullReferenceException ex = new NullReferenceException();
						return (MalfunctionType)ex;
					}
					if (text3 != null)
					{
						nint num20 = (nint)array2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1245 @ rdx_v50 (Il2CppClass<System.Object[]>)+40]");
						UnityEngine.Object obj2 = (UnityEngine.Object)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj22 = default(object);
						bool flag20 = obj22 == null;
						UnityEngine.Object obj3 = (UnityEngine.Object)(object)text3;
						if (flag20)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							object obj23 = default(object);
							throw obj23;
						}
					}
					array2[0] = text3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object obj24 = default(object);
					if (obj24 != null)
					{
						nint num21 = (nint)array2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1423 @ rdx_v48 (Il2CppClass<System.Object[]>)+40]");
						object obj25 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj26 = default(object);
						bool flag21 = obj26 == null;
						object obj27 = obj24;
						if (flag21)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							object obj28 = default(object);
							throw obj28;
						}
					}
					array2[1] = obj24;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object obj29 = default(object);
					if (obj29 != null)
					{
						nint num22 = (nint)array2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1459 @ rdx_v46 (Il2CppClass<System.Object[]>)+40]");
						object obj30 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj31 = default(object);
						bool flag22 = obj31 == null;
						object obj32 = obj29;
						if (flag22)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							object obj33 = default(object);
							throw obj33;
						}
					}
					array2[2] = obj29;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object obj34 = default(object);
					if (obj34 != null)
					{
						nint num23 = (nint)array2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1487 @ rdx_v44 (Il2CppClass<System.Object[]>)+40]");
						object obj35 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj36 = default(object);
						bool flag23 = obj36 == null;
						object obj37 = obj34;
						if (flag23)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							object obj38 = default(object);
							throw obj38;
						}
					}
					array2[3] = obj34;
					string message2 = string.Format("[LookAtTarget:{0}] DeadW={1:0.###} DoubleW={2:0.###} Pick={3:0.###}", array2);
					Debug.Log(message2);
				}
				bool flag24 = num15 < num19;
				float num24 = num15 - num19;
				bool flag25 = num24 == 0f;
				object obj39 = flag24 | flag25;
				return (MalfunctionType)(obj39 + 1);
			}
			if (debugMalfunctions)
			{
				string text4 = base.name;
				string message3 = "[LookAtTarget:" + text4 + "] Malfunction aborted (both type weights zero).";
				Debug.Log(message3);
			}
		}
		return MalfunctionType.None;
	}

	public LookAtTarget()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3ADDD]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		useCursorManagerIntegration = true;
		autoFindCursorManagerByTag = true;
		cursorManagerTag = "CursorManager";
		autoFindRetrySeconds = 0.5f;
		alwaysReleaseToSameTarget = true;
		isActive = true;
		useClickCooldown = true;
		clickCooldownSeconds = 0.2f;
		pressureSystemId = "Default";
		AnimationCurve animationCurve = AnimationCurve.Linear(1f, 0f, 0f, 0.5f);
		globalMalfunctionChanceCurve = animationCurve;
		deadPressChanceCurve = AnimationCurve.Linear(1f, 0f, 0f, 0.3f);
		doublePressChanceCurve = AnimationCurve.Linear(1f, 0f, 0f, 0.2f);
		base._002Ector();
	}
}
