using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class LinearSliderInteractable : MonoBehaviour, ICursorDraggable
{
	private sealed class _003CAutoFindCursorManagerRoutine_003Ed__94 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public LinearSliderInteractable _003C_003E4__this;

		private WaitForSecondsRealtime _003Cwait_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CAutoFindCursorManagerRoutine_003Ed__94(int _003C_003E1__state)
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
			//IL_00a9: Expected I4, but got I8
			//IL_01fe: Expected I4, but got O
			LinearSliderInteractable linearSliderInteractable = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					bool flag = !(0.05f < linearSliderInteractable.autoFindRetrySeconds);
					float time = 0.05f;
					if (!flag)
					{
						time = linearSliderInteractable.autoFindRetrySeconds;
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
					goto IL_01bd;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_00c8;
				}
			}
			goto IL_01f0;
			IL_01bd:
			return false;
			IL_01f0:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_00c8:
			if (linearSliderInteractable.useCursorManagerIntegration && linearSliderInteractable.cursorManager == null)
			{
				GameObject gameObject = GameObject.FindWithTag(linearSliderInteractable.cursorManagerTag);
				if (gameObject != null)
				{
					if ((object)gameObject == null)
					{
						goto IL_01f0;
					}
					if (gameObject.TryGetComponent<DynamicCursorManager>(out var component))
					{
						linearSliderInteractable.cursorManager = component;
						_003C_003E4__this.SubscribeToCursorManager();
						goto IL_01bd;
					}
				}
				_003C_003E2__current = _003Cwait_003E5__2;
				_003C_003E1__state = 1;
				return true;
			}
			linearSliderInteractable._findRoutine = null;
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

	private string cursorManagerTag;

	private float autoFindRetrySeconds;

	private VirtualCursor virtualCursor;

	private Camera raycastCamera;

	private Interactable interactable;

	private bool alwaysReleaseToSameTarget;

	private bool useLegacyMouseCallbacks;

	private Vector3 movementAxis;

	private float minDistance;

	private float maxDistance;

	private float minOutputValue;

	private float maxOutputValue;

	private AnimationCurve valueCurve;

	private bool useDetents;

	private float detentStepSize;

	private float detentSmoothTime;

	private bool useRelativeDrag;

	private float dragSensitivity;

	private float accumulatedValue;

	private float currentDistance;

	private bool useBrokerLockWhileDragging;

	private string lockBrokerTag;

	private string brokerDebugLabel;

	private float maxExpectedUnitsPerSecond;

	private float inspectorMeasuredLinearSpeed;

	private float inspectorNormalizedLinearSpeed;

	private Vector3 _speedPrevLocalPosition;

	private bool _speedPrevLocalPositionValid;

	private float _003CMeasuredLinearSpeed_003Ek__BackingField;

	private float _003CNormalizedLinearSpeed_003Ek__BackingField;

	public UnityEvent<float> OnValueChanged;

	public UnityEvent OnGrab;

	public UnityEvent OnRelease;

	private Action m_OnBeginSliderDrag;

	private Action m_OnEndSliderDrag;

	private bool isDragging;

	private bool _subscribedToManager;

	private bool _pressBeganHere;

	private Coroutine _findRoutine;

	private Vector3 baseLocalPosition;

	private float lastQuantizedValue;

	private float detentTargetDistance;

	private float detentCurrentDistance;

	private float detentVelocity;

	private Vector3 dragPlaneOriginWorld;

	private Vector3 dragStartHitWorld;

	private float dragStartDistance;

	private InteractionLockBroker _broker;

	private InteractionLockBroker.LockHandle _dragHandle;

	public float MeasuredLinearSpeed
	{
		get
		{
			return _003CMeasuredLinearSpeed_003Ek__BackingField;
		}
		private set
		{
			_003CMeasuredLinearSpeed_003Ek__BackingField = value;
		}
	}

	public float NormalizedLinearSpeed
	{
		get
		{
			return _003CNormalizedLinearSpeed_003Ek__BackingField;
		}
		private set
		{
			_003CNormalizedLinearSpeed_003Ek__BackingField = value;
		}
	}

	public float Value => accumulatedValue;

	public float CurrentDistance => currentDistance;

	public bool IsDragging => isDragging;

	public bool UseLegacyMouseCallbacks => useLegacyMouseCallbacks;

	public event Action OnBeginSliderDrag
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 240;
			Delegate obj2 = this.m_OnBeginSliderDrag;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
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
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 240;
			Delegate obj2 = this.m_OnBeginSliderDrag;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
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
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action OnEndSliderDrag
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 248;
			Delegate obj2 = this.m_OnEndSliderDrag;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
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
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 248;
			Delegate obj2 = this.m_OnEndSliderDrag;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
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
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action DragStarted
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 240;
			Delegate obj2 = this.m_OnBeginSliderDrag;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
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
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 240;
			Delegate obj2 = this.m_OnBeginSliderDrag;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
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
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action DragEnded
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 248;
			Delegate obj2 = this.m_OnEndSliderDrag;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
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
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 248;
			Delegate obj2 = this.m_OnEndSliderDrag;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
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
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	private void Awake()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_001c: Expected O, but got I4
		//IL_0025: Expected O, but got I4
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_0200: Expected O, but got F4
		//IL_0238: Expected O, but got I
		//IL_0255: Expected O, but got I
		//IL_0278: Invalid comparison between F4 and O
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Expected O, but got Unknown
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Expected O, but got Unknown
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Expected O, but got Unknown
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Expected O, but got Unknown
		//IL_03f0: Invalid comparison between O and F4
		//IL_03bb: Expected I, but got O
		//IL_02e2: Expected F4, but got O
		//IL_02b6: Invalid comparison between F4 and O
		//IL_0303: Expected O, but got F4
		//IL_02d5: Expected F4, but got O
		Collider[] componentsInChildren = GetComponentsInChildren<Collider>(includeInactive: true);
		object obj = componentsInChildren + 32;
		object obj2 = 0;
		object obj3 = 0;
		UnityEngine.Object obj4 = default(UnityEngine.Object);
		while ((nint)obj3 < componentsInChildren.Length)
		{
			GameObject gameObject = ((Component)obj).gameObject;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			if (obj4 == null)
			{
				GameObject gameObject2 = ((Component)obj).gameObject;
				LinearSliderInteractableColliderHelper linearSliderInteractableColliderHelper = gameObject2.AddComponent<LinearSliderInteractableColliderHelper>();
				linearSliderInteractableColliderHelper.parentSlider = this;
			}
			obj2++;
			obj += 8;
			obj3 = obj2;
		}
		if (valueCurve == null || valueCurve.length == 0)
		{
			AnimationCurve animationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
			valueCurve = animationCurve;
		}
		if (this.interactable == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696290");
			Interactable interactable = default(Interactable);
			this.interactable = interactable;
		}
		if (raycastCamera == null)
		{
			Camera main = Camera.main;
			raycastCamera = main;
		}
		Transform transform = base.transform;
		Vector3 localPosition = transform.localPosition;
		baseLocalPosition = (Vector3)localPosition.x;
		_ = localPosition.z;
		object obj5 = (object)movementAxis * (object)movementAxis;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (LinearSliderInteractable)+60]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (LinearSliderInteractable)+60]");
		object obj6 = num * 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (LinearSliderInteractable)+64]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (LinearSliderInteractable)+64]");
		object obj7 = num2 * 0;
		object obj8 = obj5 + obj6;
		object obj9 = obj8 + obj7;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-06f) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj9))
		{
			nint num3 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v32 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num4 = 0;
			movementAxis = Vector3.rightVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v531 @ rcx_v34 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
			_ = 0;
		}
		float num5 = currentDistance;
		object obj10 = this + 104;
		object obj11 = this + 108;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj11) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj10))
		{
			obj10 = obj11;
		}
		object obj12 = this + 104;
		object obj13 = this + 108;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj12) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj13))
		{
			obj12 = obj13;
		}
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj10) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num5))
		{
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num5) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj12))
			{
				num5 = (float)obj12;
			}
		}
		else
		{
			num5 = (float)obj10;
		}
		currentDistance = num5;
		ApplyLocalPosition(num5);
		float num6 = MapDistanceToValue(currentDistance);
		detentTargetDistance = currentDistance;
		detentCurrentDistance = currentDistance;
		accumulatedValue = num6;
		lastQuantizedValue = num6;
		detentVelocity = 0f;
		Transform transform2 = base.transform;
		Vector3 localPosition2 = transform2.localPosition;
		_speedPrevLocalPosition = (Vector3)localPosition2.x;
		_ = localPosition2.z;
		_speedPrevLocalPositionValid = true;
		InteractionLockBroker broker = InteractionLockBroker.FindOrNull(lockBrokerTag);
		_broker = broker;
	}

	private void OnEnable()
	{
		if (useCursorManagerIntegration)
		{
			bool flag = cursorManager == null;
			bool flag2 = !flag;
			DynamicCursorManager dynamicCursorManager = null;
			if (!flag2)
			{
				GameObject gameObject = GameObject.FindWithTag(cursorManagerTag);
				bool flag3 = gameObject != null;
				bool flag4 = !flag3;
				dynamicCursorManager = null;
				if (!flag4 && gameObject.TryGetComponent<DynamicCursorManager>(out dynamicCursorManager))
				{
					cursorManager = dynamicCursorManager;
				}
			}
			bool flag5 = cursorManager != null;
			if (!flag5)
			{
				if (_findRoutine == null && useCursorManagerIntegration != flag5)
				{
					_003CAutoFindCursorManagerRoutine_003Ed__94 obj = new _003CAutoFindCursorManagerRoutine_003Ed__94(0);
					obj._003C_003E1__state = 0;
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
		if (this.virtualCursor == null)
		{
			if (cursorManager != null)
			{
				DynamicCursorManager dynamicCursorManager2 = cursorManager;
				if (dynamicCursorManager2.virtualCursor != null)
				{
					DynamicCursorManager dynamicCursorManager3 = cursorManager;
					this.virtualCursor = dynamicCursorManager3.virtualCursor;
				}
			}
			if (this.virtualCursor == null)
			{
				VirtualCursor virtualCursor = UnityEngine.Object.FindObjectOfType<VirtualCursor>();
				this.virtualCursor = virtualCursor;
			}
		}
		if (_broker == null)
		{
			TryFindBroker();
		}
	}

	private void OnDisable()
	{
		//IL_0023: Expected O, but got I4
		//IL_00a5: Expected O, but got I4
		//IL_00ae: Expected O, but got I4
		//IL_00d8: Expected O, but got I4
		//IL_00e1: Expected O, but got I4
		if (_subscribedToManager)
		{
			bool flag = cursorManager != null;
			object obj = 0;
			if (flag)
			{
				Action<Interactable> value = HandlePrimaryClickDown;
				cursorManager.OnPrimaryClickDown -= value;
				Action<Interactable> value2 = HandlePrimaryClickUp;
				cursorManager.OnPrimaryClickUp -= value2;
				bool flag2 = _findRoutine == null;
				_subscribedToManager = false;
				object obj2 = 0;
				obj = 0;
				if (!flag2)
				{
					StopCoroutine(_findRoutine);
					_findRoutine = null;
					obj2 = 0;
					obj = 0;
				}
			}
		}
		ReleaseBrokerDragLockIfHeld();
		if (_pressBeganHere)
		{
			_pressBeganHere = false;
			isDragging = false;
			if (OnRelease != null)
			{
				OnRelease.Invoke();
			}
			Action onEndSliderDrag = this.m_OnEndSliderDrag;
			if (this.m_OnEndSliderDrag != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v148.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
			}
		}
	}

	private unsafe void Update()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		//IL_04bc: Invalid comparison between F4 and I4
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_0726: Expected I, but got O
		//IL_074d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0752: Expected O, but got Unknown
		//IL_0762: Unknown result type (might be due to invalid IL or missing references)
		//IL_0767: Expected O, but got Unknown
		//IL_06c9: Expected O, but got F4
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Expected Ref, but got Unknown
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Expected O, but got Unknown
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Expected O, but got Unknown
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Expected O, but got Unknown
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Expected O, but got Unknown
		//IL_00d2: Expected O, but got I
		//IL_00ef: Expected O, but got I
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Expected O, but got Unknown
		//IL_099c: Expected I, but got O
		//IL_09b9: Expected O, but got I
		//IL_09d6: Expected O, but got I
		//IL_0a0f: Invalid comparison between I4 and F4
		//IL_06b5: Expected F4, but got I4
		//IL_0299: Invalid comparison between F4 and I4
		//IL_0a32: Expected O, but got F4
		//IL_0400: Expected F4, but got I4
		//IL_08bb: Invalid comparison between I4 and F4
		//IL_03b5: Invalid comparison between I4 and F4
		//IL_043c: Expected F4, but got I4
		object obj2 = default(object);
		object obj = obj2 - 95;
		float num22;
		float num25;
		UnityEvent<float> onValueChanged;
		if (isDragging)
		{
			Vector3 axisWorld = GetAxisWorld();
			_ = axisWorld.x;
			Vector3 lineOriginWorld = GetLineOriginWorld();
			Vector3 planePoint = (Vector3)(obj - 121);
			_ = lineOriginWorld.x;
			_ = dragPlaneOriginWorld;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (LinearSliderInteractable)+134]");
			_ = 0;
			Vector3 pointerWorldPointOnDragPlane = GetPointerWorldPointOnDragPlane(planePoint);
			_ = pointerWorldPointOnDragPlane.x;
			_ = pointerWorldPointOnDragPlane.x;
			nint num = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v173 @ rax_v20 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num2 = 0;
			_ = Vector3.zeroVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-59]");
			object obj3 = 0 - Vector3.zeroVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-55]");
			object obj5 = default(object);
			object obj4 = 0 - obj5;
			float num3 = pointerWorldPointOnDragPlane.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v273 @ rcx_v17 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			float num4 = num3 - 0f;
			object obj6 = obj4 * obj4;
			object obj7 = obj3 * obj3;
			float num5 = num4 * num4;
			object obj8 = obj6 + obj7;
			float num6 = (float)obj8 + num5;
			if (9.9999994E-11f > num6)
			{
				return;
			}
			float num11;
			if (!useRelativeDrag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-75]");
				nint num7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-45]");
				object obj9 = num7 - 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-79]");
				nint num8 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
				object obj10 = num8 - 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-65]");
				object obj11 = obj9 * 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-69]");
				object obj12 = obj10 * 0;
				object obj13 = obj11 + obj12;
				float num9 = pointerWorldPointOnDragPlane.z - lineOriginWorld.z;
				float num10 = num9 * axisWorld.z;
				num11 = (float)obj13 + num10;
			}
			else
			{
				float num12 = pointerWorldPointOnDragPlane.z;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (LinearSliderInteractable)+140]");
				float num13 = num12 - 0f;
				_ = dragStartHitWorld;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-75]");
				object obj14 = 0 - obj5;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-79]");
				object obj15 = 0 - dragStartHitWorld;
				float num10 = num13 * axisWorld.z;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-65]");
				object obj16 = obj14 * 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-69]");
				object obj17 = obj15 * 0;
				object obj18 = obj16 + obj17;
				float num14 = (float)obj18 + num10;
				float num15 = num14 * dragSensitivity;
				num11 = num15 + dragStartDistance;
			}
			float num16 = minDistance;
			if (minDistance > maxDistance)
			{
				num16 = maxDistance;
			}
			float num17 = minDistance;
			if (minDistance < maxDistance)
			{
				num17 = maxDistance;
			}
			if (!(num16 > num11))
			{
				if (num11 > num17)
				{
					num11 = num17;
				}
			}
			else
			{
				num11 = num16;
			}
			float num18 = MapDistanceToValue(num11);
			if (useDetents && detentStepSize > 0f)
			{
				float num19 = num18 / detentStepSize;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
				float num20 = minOutputValue;
				if (minOutputValue > maxOutputValue)
				{
					num20 = maxOutputValue;
				}
				float num21 = minOutputValue;
				if (minOutputValue < maxOutputValue)
				{
					num21 = maxOutputValue;
				}
				num22 = num19 * detentStepSize;
				if (!(num20 > num22))
				{
					if (num22 > num21)
					{
						num22 = num21;
					}
				}
				else
				{
					num22 = num20;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
				object obj19 = default(object);
				if (obj19 == null)
				{
					bool flag = minOutputValue == maxOutputValue;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001805AB6CDh\"");
					if (!flag)
					{
						float num23 = maxOutputValue - minOutputValue;
						float num24 = num22 - minOutputValue;
						num25 = num24 / num23;
						if (!(0f > num25))
						{
							if (num25 > 1f)
							{
								num25 = 1f;
							}
							goto IL_08b2;
						}
					}
					num25 = 0f;
					goto IL_08b2;
				}
			}
			else
			{
				onValueChanged = OnValueChanged;
				accumulatedValue = num18;
				detentTargetDistance = num11;
				if (OnValueChanged != null)
				{
					goto IL_0949;
				}
			}
		}
		goto IL_0492;
		IL_0949:
		float arg = (float)obj + 103f;
		onValueChanged.Invoke(arg);
		goto IL_0492;
		IL_08b2:
		if (!(0f > num25))
		{
			if (num25 > 1f)
			{
				num25 = 1f;
			}
		}
		else
		{
			num25 = 0f;
		}
		onValueChanged = OnValueChanged;
		float num26 = maxDistance - minDistance;
		accumulatedValue = num22;
		lastQuantizedValue = num22;
		float num27 = num26 * num25;
		float num28 = num27 + minDistance;
		detentTargetDistance = num28;
		if (OnValueChanged == null)
		{
			goto IL_0492;
		}
		goto IL_0949;
		IL_0492:
		float num29;
		float num30;
		if (useDetents && detentStepSize > 0f)
		{
			num29 = (detentCurrentDistance = Mathf.SmoothDamp(detentCurrentDistance, detentTargetDistance, ref *(float*)(this + 296), detentSmoothTime));
			num30 = num29;
		}
		else
		{
			num29 = detentTargetDistance;
			num30 = detentTargetDistance;
		}
		currentDistance = num30;
		ApplyLocalPosition(num29);
		Transform transform = base.transform;
		Vector3 localPosition = transform.localPosition;
		_ = localPosition.x;
		if (_speedPrevLocalPositionValid)
		{
			float deltaTime = Time.deltaTime;
			if (1E-06f > deltaTime)
			{
				return;
			}
			_ = _speedPrevLocalPosition;
			nint num31 = (nint)typeof(Math);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
			nint num32 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-59]");
			object obj20 = num32 - 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-45]");
			nint num33 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-55]");
			object obj21 = num33 - 0;
			float num34 = localPosition.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (LinearSliderInteractable)+C4]");
			float num35 = num34 - 0f;
			object obj22 = obj21 * obj21;
			object obj23 = obj20 * obj20;
			float num36 = num35 * num35;
			object obj24 = obj22 + obj23;
			float num37 = (float)obj24 + num36;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v609 @ rcx_v11 (Il2CppClass<System.Math>)+E4]");
			if ((nint)0 <= (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
			}
			else
			{
				double num38 = Math.Sqrt(num37);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm2,xmm0\"");
			float num39 = 0f / deltaTime;
			bool flag2 = !(0.001f < maxExpectedUnitsPerSecond);
			float num40 = 0.001f;
			if (!flag2)
			{
				num40 = maxExpectedUnitsPerSecond;
			}
			float num41 = num39 / num40;
			if (!(0f > num41))
			{
				if (num41 > 1f)
				{
					num41 = 1f;
				}
			}
			else
			{
				num41 = 0f;
			}
			_speedPrevLocalPosition = (Vector3)localPosition.x;
			_ = localPosition.z;
			_003CMeasuredLinearSpeed_003Ek__BackingField = num39;
			_003CNormalizedLinearSpeed_003Ek__BackingField = num41;
			inspectorMeasuredLinearSpeed = num39;
			inspectorNormalizedLinearSpeed = num41;
		}
		else
		{
			_speedPrevLocalPosition = (Vector3)localPosition.x;
			_ = localPosition.z;
			_speedPrevLocalPositionValid = true;
			_003CMeasuredLinearSpeed_003Ek__BackingField = 0f;
			inspectorMeasuredLinearSpeed = 0f;
		}
	}

	private unsafe void ApplyLocalPosition(float distance)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_0036: Invalid comparison between O and F4
		//IL_0017: Expected O, but got Ref
		object obj = this + 92;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		object obj2 = default(object);
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
		{
		}
		Transform transform = base.transform;
		Vector3 vector = default(Vector3);
		transform.localPosition = (Vector3)(&vector);
	}

	private float MapDistanceToValue(float distance)
	{
		//IL_00d4: Invalid comparison between I4 and F4
		//IL_00e3: Expected O, but got I4
		//IL_01a0: Expected F4, but got I4
		//IL_0036: Expected O, but got I4
		//IL_010c: Expected O, but got I4
		//IL_0183: Invalid comparison between O and F4
		//IL_0176: Expected F4, but got I4
		//IL_0056: Invalid comparison between O and F4
		//IL_0123: Expected O, but got I4
		//IL_013a: Expected F4, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001805AA2BAh\"");
		object obj;
		float num3;
		if (minDistance == maxDistance)
		{
			obj = 0;
		}
		else
		{
			float num = distance - minDistance;
			float num2 = maxDistance - minDistance;
			num3 = num / num2;
			bool flag = 0f > num3;
			obj = 0;
			if (!flag)
			{
				bool flag2 = !(num3 > 1f);
				obj = 0;
				if (!flag2)
				{
					obj = 0;
					num3 = 1f;
				}
				goto IL_01a5;
			}
		}
		num3 = 0f;
		goto IL_01a5;
		IL_01a5:
		if (valueCurve != null)
		{
			num3 = valueCurve.Evaluate(num3);
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3))
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
		}
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3))
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
		float num4 = maxOutputValue - minOutputValue;
		float num5 = num4 * num3;
		return num5 + minOutputValue;
	}

	public unsafe void BeginSliderDrag()
	{
		//IL_0178: Expected O, but got F4
		//IL_018c: Expected O, but got Ref
		//IL_019f: Expected O, but got F4
		//IL_0244: Expected I, but got O
		//IL_01bd: Expected O, but got F4
		if (isDragging)
		{
			return;
		}
		if (virtualCursor == null)
		{
			if (cursorManager != null)
			{
				DynamicCursorManager dynamicCursorManager = cursorManager;
				if (dynamicCursorManager.virtualCursor != null)
				{
					DynamicCursorManager dynamicCursorManager2 = cursorManager;
					virtualCursor = dynamicCursorManager2.virtualCursor;
				}
			}
			if (virtualCursor == null)
			{
				Debug.LogError("[LinearSliderInteractable] No VirtualCursor assigned or found. Drag cannot begin.", this);
				return;
			}
		}
		if (raycastCamera == null)
		{
			Camera main = Camera.main;
			raycastCamera = main;
		}
		TryAcquireBrokerDragLockIfNeeded();
		isDragging = true;
		Vector3 lineOriginWorld = GetLineOriginWorld();
		dragPlaneOriginWorld = (Vector3)lineOriginWorld.x;
		_ = lineOriginWorld.z;
		object obj = default(object);
		Vector3 pointerWorldPointOnDragPlane = GetPointerWorldPointOnDragPlane((Vector3)(&obj));
		dragStartHitWorld = (Vector3)pointerWorldPointOnDragPlane.x;
		_ = pointerWorldPointOnDragPlane.z;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v430 @ rax_v13 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		float num3 = pointerWorldPointOnDragPlane.x - (float)Vector3.zeroVector;
		object obj3 = default(object);
		object obj4 = default(object);
		object obj2 = obj3 - obj4;
		float num4 = pointerWorldPointOnDragPlane.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v434 @ rcx_v13 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		float num5 = num4 - 0f;
		object obj5 = obj2 * obj2;
		float num6 = num3 * num3;
		float num7 = num5 * num5;
		float num8 = (float)obj5 + num6;
		float num9 = num8 + num7;
		if (9.9999994E-11f > num9)
		{
			dragStartHitWorld = (Vector3)lineOriginWorld.x;
			_ = lineOriginWorld.z;
		}
		dragStartDistance = currentDistance;
		detentCurrentDistance = currentDistance;
		detentTargetDistance = currentDistance;
		detentVelocity = 0f;
		float num10 = MapDistanceToValue(currentDistance);
		lastQuantizedValue = num10;
		if (OnGrab != null)
		{
			OnGrab.Invoke();
		}
		Action onBeginSliderDrag = this.m_OnBeginSliderDrag;
		if (this.m_OnBeginSliderDrag != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v477.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
	}

	public void EndSliderDrag()
	{
		if (isDragging)
		{
			isDragging = false;
			ReleaseBrokerDragLockIfHeld();
			if (OnRelease != null)
			{
				OnRelease.Invoke();
			}
			Action onEndSliderDrag = this.m_OnEndSliderDrag;
			if (this.m_OnEndSliderDrag != null)
			{
				IntPtr invoke_impl = ((Delegate)onEndSliderDrag).invoke_impl;
				IntPtr method = ((Delegate)onEndSliderDrag).method;
				IntPtr method_code = ((Delegate)onEndSliderDrag).method_code;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v58 @ rax_v4 (System.IntPtr) (should have been resolved before IL gen)");
			}
		}
	}

	private unsafe void TryAcquireBrokerDragLockIfNeeded()
	{
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected Ref, but got Unknown
		//IL_0174: Expected O, but got Ref
		if (!useBrokerLockWhileDragging)
		{
			return;
		}
		if (cursorManager == null && cursorManager == null)
		{
			GameObject gameObject = GameObject.FindWithTag(cursorManagerTag);
			if (gameObject != null)
			{
				bool flag = gameObject.TryGetComponent<DynamicCursorManager>(out *(DynamicCursorManager*)(this + 40));
			}
		}
		if (!(cursorManager != null))
		{
			return;
		}
		DynamicCursorManager dynamicCursorManager = cursorManager;
		if (dynamicCursorManager._currentMode == DynamicCursorManager.PresentationMode.FPSLocked)
		{
			if (_broker == null)
			{
				TryFindBroker();
			}
			if (!(_broker == null))
			{
				object obj = default(object);
				InteractionLockBroker.LockHandle dragHandle = _broker.Acquire((InteractionLockBroker.LockRequest)(&obj));
				_dragHandle = dragHandle;
			}
			else
			{
				string message = "[LinearSliderInteractable] InteractionLockBroker not found (tag='" + lockBrokerTag + "'). Drag lock not acquired.";
				Debug.LogWarning(message, this);
			}
		}
	}

	private unsafe void ReleaseBrokerDragLockIfHeld()
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_0091: Expected O, but got I4
		InteractionLockBroker.LockHandle lockHandle = (InteractionLockBroker.LockHandle)(this + 336);
		if (((InteractionLockBroker.LockHandle*)lockHandle)->IsValid)
		{
			if (_broker == null)
			{
				TryFindBroker();
			}
			if (_broker != null)
			{
				bool flag = _broker.Release(_dragHandle);
			}
			_dragHandle = (InteractionLockBroker.LockHandle)0;
		}
	}

	private void TryFindBroker()
	{
		InteractionLockBroker broker = InteractionLockBroker.FindOrNull(lockBrokerTag);
		_broker = broker;
	}

	private unsafe Vector3 GetAxisWorld()
	{
		//IL_0040: Expected O, but got I
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Expected O, but got Unknown
		//IL_00f8: Expected I, but got O
		//IL_0121: Expected F4, but got I
		//IL_024c: Expected I, but got O
		//IL_0275: Expected F4, but got I
		//IL_01f7: Expected F4, but got O
		//IL_01f2: Expected native int or pointer, but got O
		//IL_01ff: Expected native int or pointer, but got O
		//IL_0177: Expected O, but got Ref
		//IL_01bc: Expected I, but got O
		//IL_01e5: Expected F4, but got I
		//IL_02f9: Expected F4, but got O
		//IL_02f4: Expected native int or pointer, but got O
		//IL_0301: Expected native int or pointer, but got O
		Transform transform = base.transform;
		if ((object)transform != null)
		{
			Transform parent = transform.parent;
			object obj = (object)movementAxis * (object)movementAxis;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (LinearSliderInteractable)+60]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (LinearSliderInteractable)+60]");
			object obj2 = num * 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (LinearSliderInteractable)+64]");
			float num2 = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (LinearSliderInteractable)+64]");
			float num3 = num2 * 0f;
			object obj3 = obj + obj2;
			float num4 = (float)obj3 + num3;
			Vector3 vector;
			float z;
			Vector3 vector3 = default(Vector3);
			if (1E-06f > num4)
			{
				nint num5 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v216 @ rax_v26 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num6 = 0;
				vector = Vector3.rightVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v217 @ rcx_v21 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
				z = 0f;
			}
			else
			{
				object obj4 = this + 92;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
				Vector3 vector2;
				if (1E-06f > 1E-05f)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (LinearSliderInteractable)+60]");
					num3 = 0f / 1E-06f;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (LinearSliderInteractable)+64]");
					num4 = 0f / 1E-06f;
					vector2 = vector3;
					z = num4;
				}
				else
				{
					nint num7 = (nint)typeof(Vector3);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v295 @ rax_v23 (Il2CppClass<UnityEngine.Vector3>)+B8]");
					nint num8 = 0;
					vector2 = Vector3.zeroVector;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v296 @ rcx_v18 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
					z = 0f;
				}
				vector = vector2;
			}
			Vector3 vector7 = default(Vector3);
			if (parent != null)
			{
				if ((object)parent == null)
				{
					goto IL_0209;
				}
				Vector3 vector5 = default(Vector3);
				Vector3 vector4 = parent.TransformDirection((Vector3)(&vector5));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
				Vector3 vector6;
				float z2;
				if (vector4.x > 1E-05f)
				{
					float num9 = vector4.z / vector4.x;
					vector6 = vector3;
					z2 = num9;
				}
				else
				{
					nint num10 = (nint)typeof(Vector3);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v377 @ rax_v15 (Il2CppClass<UnityEngine.Vector3>)+B8]");
					nint num11 = 0;
					vector6 = Vector3.zeroVector;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v378 @ rcx_v13 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
					z2 = 0f;
				}
				((Vector3*)(nint)vector7)->x = (float)vector6;
				((Vector3*)(nint)vector7)->z = z2;
			}
			else
			{
				((Vector3*)(nint)vector7)->x = (float)vector;
				((Vector3*)(nint)vector7)->z = z;
			}
			return vector7;
		}
		goto IL_0209;
		IL_0209:
		return (Vector3)new NullReferenceException();
	}

	private unsafe Vector3 GetLineOriginWorld()
	{
		//IL_0043: Expected F4, but got I
		//IL_0052: Expected F4, but got O
		//IL_004d: Expected native int or pointer, but got O
		//IL_0081: Expected O, but got Ref
		//IL_009f: Expected native int or pointer, but got O
		//IL_00e6: Expected native int or pointer, but got O
		Transform transform = base.transform;
		if ((object)transform != null)
		{
			Transform parent = transform.parent;
			float z;
			Vector3 vector = default(Vector3);
			if (!(parent != null))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (LinearSliderInteractable)+118]");
				z = 0f;
				((Vector3*)(nint)vector)->x = (float)baseLocalPosition;
			}
			else
			{
				if ((object)parent == null)
				{
					goto IL_00a9;
				}
				object obj = default(object);
				Vector3 vector2 = parent.TransformPoint((Vector3)(&obj));
				z = vector2.z;
				((Vector3*)(nint)vector)->x = vector2.x;
			}
			((Vector3*)(nint)vector)->z = z;
			return vector;
		}
		goto IL_00a9;
		IL_00a9:
		return (Vector3)new NullReferenceException();
	}

	private unsafe Vector3 GetPointerWorldPointOnDragPlane(Vector3 planePoint)
	{
		//IL_0456: Expected I, but got O
		//IL_0474: Expected F4, but got O
		//IL_046f: Expected native int or pointer, but got O
		//IL_0489: Expected F4, but got I
		//IL_0484: Expected native int or pointer, but got O
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Expected O, but got Unknown
		//IL_00d8: Expected F4, but got I4
		//IL_00e1: Expected F4, but got I4
		//IL_00ea: Expected F4, but got I4
		//IL_0168: Expected O, but got Ref
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Expected O, but got Unknown
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Expected O, but got Unknown
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Expected O, but got Unknown
		//IL_03ee: Invalid comparison between F4 and O
		//IL_023f: Expected O, but got I4
		//IL_0410: Expected I, but got O
		//IL_042e: Expected F4, but got O
		//IL_0429: Expected native int or pointer, but got O
		//IL_0443: Expected F4, but got I
		//IL_043e: Expected native int or pointer, but got O
		//IL_0269: Invalid comparison between F4 and I4
		//IL_0278: Invalid comparison between F4 and I4
		//IL_02a1: Expected O, but got I4
		//IL_02e4: Expected native int or pointer, but got O
		//IL_02f1: Expected native int or pointer, but got O
		Vector3 vector = default(Vector3);
		if (virtualCursor != null && raycastCamera != null)
		{
			if ((object)raycastCamera != null)
			{
				Transform transform = raycastCamera.transform;
				if ((object)transform != null)
				{
					Vector3 forward = transform.forward;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
					float num;
					float num2;
					float num3;
					if (!(planePoint.x > 1E-05f))
					{
						num = 0f;
						num2 = 0f;
						num3 = 0f;
					}
					else
					{
						num3 = forward.x / planePoint.x;
						object obj = default(object);
						num = (float)obj / planePoint.x;
						num2 = forward.z / planePoint.x;
					}
					object obj2 = default(object);
					float num4 = (float)obj2 * num;
					float num5 = planePoint.x * num3;
					float num6 = planePoint.z * num2;
					float num7 = num4 + num5;
					float num8 = num7 + num6;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
					object obj3 = num8 ^ 0;
					if ((object)virtualCursor != null && (object)raycastCamera != null)
					{
						float num9 = default(float);
						Ray ray = raycastCamera.ScreenPointToRay((Vector3)(&num9));
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v448 @ rax_v20 (UnityEngine.Ray)+10]");
						float num10 = 0f * num;
						float num12 = default(float);
						float num11 = num12 * num;
						float num13 = num12 * num3;
						float num14 = num12 * num2;
						float num15 = num10 + num13;
						float num16 = (float)ray.m_Origin * num3;
						float num17 = num12 * num2;
						float num18 = num11 + num16;
						float num19 = num15 + num14;
						float num20 = num18 + num17;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
						object obj4 = num20 ^ 0;
						object obj5 = obj4 - obj3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
						object obj6 = num19 & 0;
						float num21 = 0f - num19;
						if ((nint)obj6 < 0)
						{
							obj6 = 0;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
						object obj7 = num21 & 0;
						float num22 = Mathf.Epsilon * 8f;
						float num23 = (float)obj6 * 1E-06f;
						if (num23 < num22)
						{
							num23 = num22;
						}
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num23) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7))
						{
							float num24 = (float)obj5 / num19;
							bool flag = num24 < 0f;
							bool flag2 = num24 == 0f;
							bool flag3 = !flag;
							bool flag4 = !flag2;
							object obj8 = flag4 & flag3;
							if (obj8 != null)
							{
								object obj9 = default(object);
								float num25 = (float)obj9 * num24;
								float z = num25 + num12;
								((Vector3*)(nint)vector)->x = num12;
								((Vector3*)(nint)vector)->z = z;
								goto IL_04e3;
							}
						}
						nint num26 = (nint)typeof(Vector3);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v649 @ rax_v26 (Il2CppClass<UnityEngine.Vector3>)+B8]");
						nint num27 = 0;
						((Vector3*)(nint)vector)->x = (float)Vector3.zeroVector;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v650 @ rax_v27 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
						((Vector3*)(nint)vector)->z = 0f;
						goto IL_04e3;
					}
				}
			}
			return (Vector3)new NullReferenceException();
		}
		nint num28 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rax_v8 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num29 = 0;
		((Vector3*)(nint)vector)->x = (float)Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v128 @ rax_v9 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		((Vector3*)(nint)vector)->z = 0f;
		goto IL_04e3;
		IL_04e3:
		return vector;
	}

	public unsafe void ResetToMinimum()
	{
		//IL_0023: Expected F4, but got Ref
		float distance = minDistance;
		if (minDistance > maxDistance)
		{
			distance = maxDistance;
		}
		currentDistance = distance;
		detentTargetDistance = distance;
		detentCurrentDistance = distance;
		ApplyLocalPosition(distance);
		accumulatedValue = minOutputValue;
		lastQuantizedValue = minOutputValue;
		detentVelocity = 0f;
		if (OnValueChanged != null)
		{
			object obj = default(object);
			OnValueChanged.Invoke((nint)(&obj));
		}
	}

	public unsafe void SetSliderValue(float value)
	{
		//IL_00dc: Invalid comparison between I4 and F4
		//IL_00eb: Expected O, but got I4
		//IL_0274: Expected F4, but got I4
		//IL_006d: Expected O, but got I4
		//IL_028f: Invalid comparison between O and F4
		//IL_0114: Expected O, but got I4
		//IL_0142: Expected F4, but got I4
		//IL_012b: Expected O, but got I4
		//IL_0156: Expected F4, but got Ref
		float num = minOutputValue;
		if (minOutputValue > maxOutputValue)
		{
			num = maxOutputValue;
		}
		float num2 = minOutputValue;
		if (minOutputValue < maxOutputValue)
		{
			num2 = maxOutputValue;
		}
		float num3;
		if (!(num > value))
		{
			bool flag = !(value > num2);
			num3 = value;
			if (!flag)
			{
				num3 = num2;
			}
		}
		else
		{
			num3 = num;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001805AAC2Bh\"");
		object obj;
		float num6;
		if (minOutputValue == maxOutputValue)
		{
			obj = 0;
		}
		else
		{
			float num4 = maxOutputValue - minOutputValue;
			float num5 = num3 - minOutputValue;
			num6 = num5 / num4;
			bool flag2 = 0f > num6;
			obj = 0;
			if (!flag2)
			{
				bool flag3 = !(num6 > 1f);
				obj = 0;
				if (!flag3)
				{
					obj = 0;
					num6 = 1f;
				}
				goto IL_0279;
			}
		}
		num6 = 0f;
		goto IL_0279;
		IL_0279:
		float num7 = InverseCurveEvaluate(num6);
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num7))
		{
			if (num7 > 1f)
			{
				num7 = 1f;
			}
		}
		else
		{
			num7 = 0f;
		}
		float num8 = maxDistance - minDistance;
		float num9 = num8 * num7;
		float distance = (detentCurrentDistance = (detentTargetDistance = num9 + minDistance));
		ApplyLocalPosition(distance);
		currentDistance = distance;
		accumulatedValue = num3;
		lastQuantizedValue = num3;
		detentVelocity = 0f;
		if (OnValueChanged != null)
		{
			object obj2 = default(object);
			OnValueChanged.Invoke((nint)(&obj2));
		}
	}

	private unsafe float InverseCurveEvaluate(float normalizedValue)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0053: Expected F4, but got I4
		//IL_008b: Expected F4, but got I4
		//IL_0316: Expected O, but got I4
		//IL_00a7: Expected O, but got Ref
		//IL_00bf: Expected O, but got Ref
		//IL_00cd: Expected O, but got Ref
		//IL_00f6: Expected F4, but got I4
		//IL_0112: Expected O, but got Ref
		//IL_013e: Expected O, but got I
		//IL_014c: Expected O, but got Ref
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Expected O, but got Unknown
		//IL_01b1: Expected O, but got I
		//IL_01bf: Expected O, but got Ref
		//IL_0224: Expected O, but got I
		//IL_0232: Expected O, but got Ref
		//IL_029f: Expected O, but got I
		//IL_02ad: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		if (valueCurve == null || valueCurve.length == 0)
		{
			goto IL_03a6;
		}
		bool flag = valueCurve == null;
		float num = 0f;
		if (!flag)
		{
			int length = valueCurve.length;
			bool flag2 = length != 2;
			num = 0f;
			if (!flag2)
			{
				object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808BF2F0");
				object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
				object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808BF690");
				bool flag3 = valueCurve == null;
				num = 0f;
				if (flag3)
				{
					goto IL_03b3;
				}
				Span<Keyframe> keys = (Span<Keyframe>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-9]");
				_ = 0;
				valueCurve.GetKeys(keys);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-19]");
				object obj6 = 0;
				object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v368 @ rax_v14+10]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v368 @ rax_v14+18]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
				object obj8 = default(object);
				if (obj8 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-19]");
					object obj9 = 0;
					object obj10 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v374 @ rax_v17+10]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v374 @ rax_v17+18]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D38490");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
					object obj11 = default(object);
					if (obj11 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-19]");
						object obj12 = 0;
						object obj13 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v377 @ rax_v20+1C]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v377 @ rax_v20+2C]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v377 @ rax_v20+34]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
						object obj14 = default(object);
						if (obj14 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-19]");
							object obj15 = 0;
							object obj16 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v381 @ rax_v23+1C]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v381 @ rax_v23+2C]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v381 @ rax_v23+34]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D38490");
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
							object obj17 = default(object);
							if (obj17 != null)
							{
								goto IL_03a6;
							}
						}
					}
				}
			}
			object obj18 = 0;
			while (valueCurve != null)
			{
				float num2 = (float)obj18 / 100f;
				num = valueCurve.Evaluate(num2);
				if (num < normalizedValue)
				{
					obj18++;
					if ((nint)obj18 > 100)
					{
						return 1f;
					}
					continue;
				}
				return num2;
			}
		}
		goto IL_03b3;
		IL_03b3:
		throw new NullReferenceException();
		IL_03a6:
		return normalizedValue;
	}

	private void MeasureLinearSpeed()
	{
		//IL_019e: Expected O, but got F4
		//IL_01dc: Expected I, but got O
		//IL_0238: Invalid comparison between I4 and F4
		//IL_018a: Expected F4, but got I4
		//IL_025b: Expected O, but got F4
		Transform transform = base.transform;
		Vector3 localPosition = transform.localPosition;
		if (_speedPrevLocalPositionValid)
		{
			float deltaTime = Time.deltaTime;
			if (1E-06f > deltaTime)
			{
				return;
			}
			nint num = (nint)typeof(Math);
			float num2 = localPosition.x - (float)_speedPrevLocalPosition;
			object obj2 = default(object);
			object obj3 = default(object);
			object obj = obj2 - obj3;
			float num3 = localPosition.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (LinearSliderInteractable)+C4]");
			float num4 = num3 - 0f;
			object obj4 = obj * obj;
			float num5 = num2 * num2;
			float num6 = num4 * num4;
			float num7 = (float)obj4 + num5;
			float num8 = num7 + num6;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v195 @ rcx_v6 (Il2CppClass<System.Math>)+E4]");
			if ((nint)0 <= (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
			}
			else
			{
				double num9 = Math.Sqrt(num8);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm2,xmm0\"");
			float num10 = 0f / deltaTime;
			bool flag = !(0.001f < maxExpectedUnitsPerSecond);
			float num11 = 0.001f;
			if (!flag)
			{
				num11 = maxExpectedUnitsPerSecond;
			}
			float num12 = num10 / num11;
			if (!(0f > num12))
			{
				if (num12 > 1f)
				{
					num12 = 1f;
				}
			}
			else
			{
				num12 = 0f;
			}
			_speedPrevLocalPosition = (Vector3)localPosition.x;
			_ = localPosition.z;
			_003CMeasuredLinearSpeed_003Ek__BackingField = num10;
			_003CNormalizedLinearSpeed_003Ek__BackingField = num12;
			inspectorMeasuredLinearSpeed = num10;
			inspectorNormalizedLinearSpeed = num12;
		}
		else
		{
			_speedPrevLocalPosition = (Vector3)localPosition.x;
			_003CMeasuredLinearSpeed_003Ek__BackingField = 0f;
			inspectorMeasuredLinearSpeed = 0f;
			_ = localPosition.z;
			_speedPrevLocalPositionValid = true;
		}
	}

	private void EnsureCursorManagerSubscription()
	{
		bool flag = cursorManager == null;
		bool flag2 = !flag;
		DynamicCursorManager dynamicCursorManager = null;
		if (!flag2)
		{
			GameObject gameObject = GameObject.FindWithTag(cursorManagerTag);
			bool flag3 = gameObject != null;
			bool flag4 = !flag3;
			dynamicCursorManager = null;
			if (!flag4 && gameObject.TryGetComponent<DynamicCursorManager>(out dynamicCursorManager))
			{
				cursorManager = dynamicCursorManager;
			}
		}
		bool flag5 = cursorManager != null;
		if (!flag5)
		{
			if (_findRoutine == null && useCursorManagerIntegration != flag5)
			{
				_003CAutoFindCursorManagerRoutine_003Ed__94 obj = new _003CAutoFindCursorManagerRoutine_003Ed__94(0);
				obj._003C_003E1__state = 0;
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
		_003CAutoFindCursorManagerRoutine_003Ed__94 obj = new _003CAutoFindCursorManagerRoutine_003Ed__94(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private void SubscribeToCursorManager()
	{
		if (_subscribedToManager || !(cursorManager != null))
		{
			return;
		}
		Action<Interactable> value = HandlePrimaryClickDown;
		cursorManager.OnPrimaryClickDown += value;
		Action<Interactable> value2 = HandlePrimaryClickUp;
		cursorManager.OnPrimaryClickUp += value2;
		_subscribedToManager = true;
		if (virtualCursor == null)
		{
			DynamicCursorManager dynamicCursorManager = cursorManager;
			if (dynamicCursorManager.virtualCursor != null)
			{
				DynamicCursorManager dynamicCursorManager2 = cursorManager;
				virtualCursor = dynamicCursorManager2.virtualCursor;
			}
		}
	}

	private void UnsubscribeFromCursorManager()
	{
		if (_subscribedToManager && cursorManager != null)
		{
			Action<Interactable> value = HandlePrimaryClickDown;
			cursorManager.OnPrimaryClickDown -= value;
			Action<Interactable> value2 = HandlePrimaryClickUp;
			cursorManager.OnPrimaryClickUp -= value2;
			bool flag = _findRoutine == null;
			_subscribedToManager = false;
			if (!flag)
			{
				StopCoroutine(_findRoutine);
				_findRoutine = null;
			}
		}
	}

	private void HandlePrimaryClickDown(Interactable pressTarget)
	{
		if (useCursorManagerIntegration && interactable != null && pressTarget == interactable)
		{
			_pressBeganHere = true;
			BeginSliderDrag();
		}
	}

	private void HandlePrimaryClickUp(Interactable pressSourceTarget)
	{
		if (!useCursorManagerIntegration || !_pressBeganHere)
		{
			return;
		}
		if (isDragging)
		{
			isDragging = false;
			ReleaseBrokerDragLockIfHeld();
			if (OnRelease != null)
			{
				OnRelease.Invoke();
			}
			Action onEndSliderDrag = this.m_OnEndSliderDrag;
			if (this.m_OnEndSliderDrag != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v65.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
			}
		}
		_pressBeganHere = false;
	}

	private unsafe void TryEnsureCursorManager()
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected Ref, but got Unknown
		if (cursorManager == null)
		{
			GameObject gameObject = GameObject.FindWithTag(cursorManagerTag);
			if (gameObject != null)
			{
				bool flag = gameObject.TryGetComponent<DynamicCursorManager>(out *(DynamicCursorManager*)(this + 40));
			}
		}
	}

	public LinearSliderInteractable()
	{
		//IL_0073: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3ADCC]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		useCursorManagerIntegration = true;
		cursorManagerTag = "CursorManager";
		autoFindRetrySeconds = 0.5f;
		alwaysReleaseToSameTarget = true;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rax_v5 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		movementAxis = Vector3.rightVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rcx_v4 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
		_ = 0;
		maxDistance = 0.5f;
		maxOutputValue = 100f;
		detentStepSize = 1f;
		detentSmoothTime = 0.08f;
		useRelativeDrag = true;
		dragSensitivity = 1f;
		useBrokerLockWhileDragging = true;
		lockBrokerTag = "LockBroker";
		brokerDebugLabel = "LinearSliderInteractable:Drag";
		maxExpectedUnitsPerSecond = 0.5f;
		lastQuantizedValue = -3.4028235E+38f;
		base._002Ector();
	}
}
