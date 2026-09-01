using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class DialInteractable : MonoBehaviour, ICursorDraggable
{
	public enum DialMode
	{
		Unlimited,
		Limited
	}

	[Serializable]
	public class FloatEvent : UnityEvent<float>
	{
	}

	private sealed class _003CAutoFindCursorManagerRoutine_003Ed__121 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DialInteractable _003C_003E4__this;

		private WaitForSecondsRealtime _003Cwait_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CAutoFindCursorManagerRoutine_003Ed__121(int _003C_003E1__state)
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
			DialInteractable dialInteractable = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					bool flag = !(0.05f < dialInteractable.autoFindRetrySeconds);
					float time = 0.05f;
					if (!flag)
					{
						time = dialInteractable.autoFindRetrySeconds;
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
			if (dialInteractable.useCursorManagerIntegration && dialInteractable.cursorManager == null)
			{
				GameObject gameObject = GameObject.FindWithTag(dialInteractable.cursorManagerTag);
				if (gameObject != null)
				{
					if ((object)gameObject == null)
					{
						goto IL_01f0;
					}
					if (gameObject.TryGetComponent<DynamicCursorManager>(out var component))
					{
						dialInteractable.cursorManager = component;
						_003C_003E4__this.SubscribeToCursorManager();
						goto IL_01bd;
					}
				}
				_003C_003E2__current = _003Cwait_003E5__2;
				_003C_003E1__state = 1;
				return true;
			}
			dialInteractable._findRoutine = null;
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

	private sealed class _003CAutoFindSystemManagerRoutine_003Ed__128 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DialInteractable _003C_003E4__this;

		private WaitForSecondsRealtime _003Cwait_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CAutoFindSystemManagerRoutine_003Ed__128(int _003C_003E1__state)
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
			//IL_01b1: Expected I4, but got O
			DialInteractable dialInteractable = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					bool flag = !(0.05f < dialInteractable.autoFindHpsRetrySeconds);
					float time = 0.05f;
					if (!flag)
					{
						time = dialInteractable.autoFindHpsRetrySeconds;
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
					goto IL_018d;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_00c8;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_00c8:
			if (dialInteractable.constrainOutputBySystemHealth && dialInteractable.highPressureSystemManager == null)
			{
				HighPressureSystemManager highPressureSystemManager = HighPressureSystemManager.FindBySystemId(dialInteractable.systemIdForAutoFind);
				if (!(highPressureSystemManager != null))
				{
					_003C_003E2__current = _003Cwait_003E5__2;
					_003C_003E1__state = 1;
					return true;
				}
				dialInteractable.highPressureSystemManager = highPressureSystemManager;
				_003C_003E4__this.SubscribeToSystemManager();
				goto IL_018d;
			}
			dialInteractable._findHpsRoutine = null;
			return false;
			IL_018d:
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

	public DialMode dialMode;

	public Vector3 rotationAxis;

	private float minRotationAngle;

	private float maxRotationAngle;

	private float minOutputValue;

	private float maxOutputValue;

	private bool useDetents;

	private float detentStepSize;

	private float detentSmoothTime;

	private AnimationCurve valueCurve;

	private bool deadZoneEnabled;

	private float deadZoneMinAngle;

	private float deadZoneMaxAngle;

	private float accumulatedValue;

	private float currentRotationAngle;

	private bool useBrokerLockWhileDragging;

	private string lockBrokerTag;

	private string brokerDebugLabel;

	private bool constrainOutputBySystemHealth;

	private HighPressureSystemManager highPressureSystemManager;

	private bool autoFindSystemManagerById;

	private string systemIdForAutoFind;

	private float autoFindHpsRetrySeconds;

	private AnimationCurve healthToRangeScale;

	private float clampCenterValue;

	private bool logHealthClamping;

	private float maxExpectedDegreesPerSecond;

	private float inspectorMeasuredRotationSpeed;

	private float inspectorNormalizedRotationSpeed;

	private bool ClampGamepadCursorToValve;

	private bool ResetToDefaultValueWithoutNoInput;

	private float CursorDistanceMultiplierFromCenter;

	private float ClampedMinRotationAngle;

	private float ClampedMaxRotationAngle;

	private Quaternion _speedPrevRotation;

	private bool _speedPrevRotationValid;

	private float _003CMeasuredRotationSpeed_003Ek__BackingField;

	private float _003CNormalizedRotationSpeed_003Ek__BackingField;

	public FloatEvent OnValueChanged;

	public UnityEvent OnEnterDeadZone;

	public UnityEvent OnExitDeadZone;

	public UnityEvent OnGrab;

	public UnityEvent OnRelease;

	private Action m_OnBeginDialDrag;

	private Action m_OnEndDialDrag;

	private bool isDragging;

	private Vector3 dragStart;

	private float lastAngle;

	private float lastRawAngle;

	private bool _subscribedToManager;

	private bool _pressBeganHere;

	private Coroutine _findRoutine;

	private float lastQuantizedValue;

	private float detentTargetAngle;

	private float detentCurrentAngle;

	private float detentVelocity;

	private bool _wasInDeadZone;

	private InteractionLockBroker _broker;

	private InteractionLockBroker.LockHandle _dragHandle;

	private Coroutine _findHpsRoutine;

	private bool _subscribedToHps;

	private float effectiveMinOutputValue;

	private float effectiveMaxOutputValue;

	private float _lastAnnouncedEffectiveMin;

	private float _lastAnnouncedEffectiveMax;

	public float MeasuredRotationSpeed
	{
		get
		{
			return _003CMeasuredRotationSpeed_003Ek__BackingField;
		}
		private set
		{
			_003CMeasuredRotationSpeed_003Ek__BackingField = value;
		}
	}

	public float NormalizedRotationSpeed
	{
		get
		{
			return _003CNormalizedRotationSpeed_003Ek__BackingField;
		}
		private set
		{
			_003CNormalizedRotationSpeed_003Ek__BackingField = value;
		}
	}

	public float AccumulatedValue => accumulatedValue;

	public bool IsDragging => isDragging;

	public bool UseLegacyMouseCallbacks => useLegacyMouseCallbacks;

	public bool IsInDeadZone
	{
		get
		{
			if (dialMode == DialMode.Limited && deadZoneEnabled)
			{
				return _wasInDeadZone;
			}
			return false;
		}
	}

	public event Action OnBeginDialDrag
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 336;
			Delegate obj2 = this.m_OnBeginDialDrag;
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
			object obj = this + 336;
			Delegate obj2 = this.m_OnBeginDialDrag;
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

	public event Action OnEndDialDrag
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 344;
			Delegate obj2 = this.m_OnEndDialDrag;
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
			object obj = this + 344;
			Delegate obj2 = this.m_OnEndDialDrag;
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
			object obj = this + 336;
			Delegate obj2 = this.m_OnBeginDialDrag;
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
			object obj = this + 336;
			Delegate obj2 = this.m_OnBeginDialDrag;
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
			object obj = this + 344;
			Delegate obj2 = this.m_OnEndDialDrag;
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
			object obj = this + 344;
			Delegate obj2 = this.m_OnEndDialDrag;
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

	private unsafe void Awake()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_001c: Expected O, but got I4
		//IL_0025: Expected O, but got I4
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_0296: Expected O, but got Ref
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Expected O, but got Unknown
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Expected O, but got Unknown
		//IL_0509: Expected F4, but got O
		//IL_051d: Invalid comparison between F4 and O
		//IL_0461: Expected O, but got F4
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Expected O, but got Unknown
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Expected O, but got Unknown
		//IL_0339: Invalid comparison between O and F4
		//IL_05b4: Expected F4, but got O
		//IL_05c8: Invalid comparison between F4 and O
		//IL_037e: Invalid comparison between O and F4
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
				DialInteractableColliderHelper dialInteractableColliderHelper = gameObject2.AddComponent<DialInteractableColliderHelper>();
				dialInteractableColliderHelper.parentDial = this;
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
		bool flag = dialMode == DialMode.Limited;
		effectiveMinOutputValue = minOutputValue;
		effectiveMaxOutputValue = maxOutputValue;
		float num;
		if (!flag)
		{
			num = accumulatedValue;
			goto IL_0497;
		}
		float num2 = minRotationAngle;
		float num3 = currentRotationAngle;
		if (!(minRotationAngle > currentRotationAngle))
		{
			num2 = maxRotationAngle;
			if (!(currentRotationAngle > maxRotationAngle))
			{
				goto IL_04d4;
			}
		}
		num3 = num2;
		goto IL_04d4;
		IL_0563:
		float num4;
		if (!(minRotationAngle > num4))
		{
			if (num4 > maxRotationAngle)
			{
				num4 = maxRotationAngle;
			}
		}
		else
		{
			num4 = minRotationAngle;
		}
		float num5;
		if (num4 < num5)
		{
			goto IL_03ff;
		}
		float num6;
		bool flag2 = num6 < num4;
		bool wasInDeadZone = !flag2;
		goto IL_059d;
		IL_0497:
		detentTargetAngle = num;
		detentCurrentAngle = num;
		bool flag3 = !deadZoneEnabled;
		num4 = currentRotationAngle;
		if (flag3 || dialMode != DialMode.Limited)
		{
			goto IL_03ff;
		}
		object obj5 = this + 148;
		object obj6 = this + 152;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5))
		{
			obj5 = obj6;
		}
		num5 = (float)obj5;
		float num7 = minRotationAngle;
		float num8 = minRotationAngle;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num8) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5))
		{
			num7 = maxRotationAngle;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)maxRotationAngle))
			{
				goto IL_0531;
			}
		}
		num5 = num7;
		goto IL_0531;
		IL_0531:
		object obj7 = this + 148;
		object obj8 = this + 152;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj8))
		{
			obj7 = obj8;
		}
		num6 = (float)obj7;
		float num9 = minRotationAngle;
		float num10 = minRotationAngle;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num10) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7))
		{
			num9 = maxRotationAngle;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)maxRotationAngle))
			{
				goto IL_0563;
			}
		}
		num6 = num9;
		goto IL_0563;
		IL_03ff:
		wasInDeadZone = false;
		goto IL_059d;
		IL_059d:
		_wasInDeadZone = wasInDeadZone;
		InteractionLockBroker broker = InteractionLockBroker.FindOrNull(lockBrokerTag);
		_broker = broker;
		Transform transform = base.transform;
		Quaternion localRotation = transform.localRotation;
		_speedPrevRotationValid = true;
		_speedPrevRotation = (Quaternion)localRotation.x;
		return;
		IL_04d4:
		currentRotationAngle = num3;
		Transform transform2 = base.transform;
		Vector3 axis = default(Vector3);
		Quaternion quaternion = Quaternion.Internal_AngleAxis(currentRotationAngle, ref axis);
		transform2.localRotation = (Quaternion)(&axis);
		float num11 = MapRotationToValue(currentRotationAngle);
		accumulatedValue = num11;
		num = currentRotationAngle;
		goto IL_0497;
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
			if (cursorManager == null)
			{
				if (_findRoutine == null && useCursorManagerIntegration)
				{
					_003CAutoFindCursorManagerRoutine_003Ed__121 obj = new _003CAutoFindCursorManagerRoutine_003Ed__121(0);
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
		if (!constrainOutputBySystemHealth)
		{
			SetEffectiveRange(minOutputValue, maxOutputValue, forceNotify: true);
		}
		else
		{
			if (this.highPressureSystemManager == null && autoFindSystemManagerById)
			{
				HighPressureSystemManager highPressureSystemManager = HighPressureSystemManager.FindBySystemId(systemIdForAutoFind);
				this.highPressureSystemManager = highPressureSystemManager;
			}
			if (this.highPressureSystemManager == null)
			{
				if (_findHpsRoutine == null && autoFindSystemManagerById)
				{
					_003CAutoFindSystemManagerRoutine_003Ed__128 obj2 = new _003CAutoFindSystemManagerRoutine_003Ed__128(0);
					obj2._003C_003E1__state = 0;
					obj2._003C_003E4__this = this;
					Coroutine findHpsRoutine = StartCoroutine(obj2);
					_findHpsRoutine = findHpsRoutine;
				}
			}
			else
			{
				SubscribeToSystemManager();
			}
			float health;
			if (!(this.highPressureSystemManager != null))
			{
				health = 1f;
			}
			else
			{
				HighPressureSystemManager highPressureSystemManager2 = this.highPressureSystemManager;
				health = highPressureSystemManager2.currentHealth01;
			}
			RecomputeEffectiveRangeFromHealth(health, forceNotify: true);
		}
		if (_broker == null)
		{
			TryFindBroker();
		}
	}

	private void OnDisable()
	{
		//IL_0023: Expected O, but got I4
		//IL_017a: Expected O, but got I4
		//IL_0109: Expected O, but got I4
		//IL_0150: Expected O, but got I4
		//IL_0159: Expected O, but got I4
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
		if (_subscribedToHps)
		{
			bool flag3 = highPressureSystemManager != null;
			object obj = 0;
			if (flag3)
			{
				Action<float> value3 = HandleSystemHealthChanged01;
				highPressureSystemManager.SystemHealthChanged01 -= value3;
				_subscribedToHps = false;
				object obj2 = 0;
				obj = 0;
			}
		}
		if (_findHpsRoutine != null)
		{
			StopCoroutine(_findHpsRoutine);
			_findHpsRoutine = null;
			object obj = 0;
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
			Action onEndDialDrag = this.m_OnEndDialDrag;
			if (this.m_OnEndDialDrag != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v282.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
			}
		}
	}

	private unsafe void Update()
	{
		//IL_0008: Expected O, but got Ref
		//IL_06ed: Invalid comparison between F4 and I4
		//IL_090d: Expected I, but got O
		//IL_0934: Unknown result type (might be due to invalid IL or missing references)
		//IL_0939: Expected O, but got Unknown
		//IL_0949: Unknown result type (might be due to invalid IL or missing references)
		//IL_094e: Expected O, but got Unknown
		//IL_096b: Expected O, but got I
		//IL_09b5: Invalid comparison between F4 and O
		//IL_0846: Expected O, but got Ref
		//IL_070a: Unknown result type (might be due to invalid IL or missing references)
		//IL_070f: Expected Ref, but got Unknown
		//IL_003b: Expected I, but got O
		//IL_09f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f5: Expected O, but got Unknown
		//IL_0a05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0a: Expected O, but got Unknown
		//IL_078f: Expected O, but got Ref
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		//IL_00e3: Expected O, but got Ref
		//IL_0116: Expected O, but got Ref
		//IL_0124: Expected O, but got Ref
		//IL_0164: Expected O, but got I
		//IL_0181: Expected O, but got I
		//IL_019e: Expected O, but got I
		//IL_01bb: Expected O, but got I
		//IL_01e5: Expected O, but got I
		//IL_0202: Expected O, but got I
		//IL_0282: Invalid comparison between F4 and I4
		//IL_0aef: Invalid comparison between I4 and F4
		//IL_02e9: Expected F4, but got I4
		//IL_05df: Invalid comparison between F4 and I4
		//IL_0389: Invalid comparison between F4 and I4
		//IL_04f3: Expected F4, but got I4
		//IL_0be3: Invalid comparison between I4 and F4
		//IL_04a8: Invalid comparison between I4 and F4
		//IL_052f: Expected F4, but got I4
		object obj2 = default(object);
		object obj = (object)(&obj2);
		float num22;
		float num27;
		float num30;
		UnityEvent<float> onValueChanged;
		if (isDragging)
		{
			Vector3 pointerWorldPointOnDialPlane = GetPointerWorldPointOnDialPlane();
			_ = pointerWorldPointOnDialPlane.x;
			_ = dragStart;
			nint num = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v212 @ rdx_v13 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num2 = 0;
			_ = Vector3.zeroVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-39]");
			object obj3 = 0 - Vector3.zeroVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-35]");
			object obj5 = default(object);
			object obj4 = 0 - obj5;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (DialInteractable)+16C]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v213 @ rax_v21 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			object obj6 = num3 - 0;
			object obj7 = obj4 * obj4;
			object obj8 = obj3 * obj3;
			object obj9 = obj6 * obj6;
			object obj10 = obj7 + obj8;
			object obj11 = obj10 + obj9;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)9.9999994E-11f) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj11))
			{
				return;
			}
			nint num4 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v307 @ rdx_v14 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num5 = 0;
			_ = Vector3.zeroVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-49]");
			object obj12 = 0 - Vector3.zeroVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-45]");
			object obj13 = 0 - obj5;
			float num6 = pointerWorldPointOnDialPlane.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v312 @ rax_v23 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			float num7 = num6 - 0f;
			object obj14 = obj13 * obj13;
			object obj15 = obj12 * obj12;
			float num8 = num7 * num7;
			object obj16 = obj14 + obj15;
			float num9 = (float)obj16 + num8;
			if (9.9999994E-11f > num9)
			{
				return;
			}
			_ = dragStart;
			Transform transform = base.transform;
			Vector3 position = transform.position;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (DialInteractable)+16C]");
			object obj17 = 0 - position.z;
			_ = position.x;
			Transform transform2 = base.transform;
			Vector3 position2 = transform2.position;
			float num10 = pointerWorldPointOnDialPlane.z - position2.z;
			_ = position2.x;
			Transform transform3 = base.transform;
			Vector3 direction = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (DialInteractable)+68]");
			_ = 0;
			_ = rotationAxis;
			Vector3 vector = transform3.TransformDirection(direction);
			object obj18 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
			object obj19 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803F3260");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-45]");
			nint num11 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-31]");
			object obj20 = num11 * 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-49]");
			nint num12 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-31]");
			object obj21 = num12 * 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-41]");
			nint num13 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-35]");
			object obj22 = num13 * 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-49]");
			nint num14 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-35]");
			object obj23 = num14 * 0;
			object obj24 = obj22 - obj20;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-45]");
			nint num15 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-39]");
			object obj25 = num15 * 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-41]");
			nint num16 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-39]");
			object obj26 = num16 * 0;
			float num17 = (float)obj24 * vector.x;
			object obj27 = obj25 - obj23;
			object obj28 = obj21 - obj26;
			_ = vector.x;
			object obj29 = obj28 * obj5;
			float num18 = (float)obj27 * vector.z;
			float num19 = num17 + (float)obj29;
			float num20 = num19 + num18;
			float num21 = ((num20 < 0f) ? (-1f) : 1f);
			num22 = num21 * (float)rotationAxis;
			float num23 = num22 - lastAngle;
			float x = num23 / 360f;
			float num24 = MathF.Floor(x);
			float num25 = num24 * 360f;
			float num26 = num23 - num25;
			if (!(0f > num26))
			{
				if (num26 > 360f)
				{
					num26 = 360f;
				}
			}
			else
			{
				num26 = 0f;
			}
			if (num26 > 180f)
			{
				num26 -= 360f;
			}
			if (dialMode != DialMode.Unlimited)
			{
				num27 = num26 + lastRawAngle;
				float num28 = minRotationAngle;
				lastRawAngle = num27;
				if (!(minRotationAngle > num27))
				{
					num28 = maxRotationAngle;
					if (!(num27 > maxRotationAngle))
					{
						goto IL_0b23;
					}
				}
				num27 = num28;
				goto IL_0b23;
			}
			if (useDetents && detentStepSize > 0f)
			{
				float num29 = (accumulatedValue = num26 + accumulatedValue) / detentStepSize;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
				num30 = num29 * detentStepSize;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
				object obj30 = default(object);
				if (obj30 == null)
				{
					detentTargetAngle = num30;
					goto IL_0c5b;
				}
			}
			else
			{
				float num31 = num26 + accumulatedValue;
				onValueChanged = OnValueChanged;
				accumulatedValue = num31;
				detentTargetAngle = num31;
				if (OnValueChanged != null)
				{
					goto IL_0c39;
				}
			}
			goto IL_06d3;
		}
		goto IL_0cd9;
		IL_0bcc:
		float num33;
		float num32 = InverseCurveEvaluate(num33);
		if (!(0f > num32))
		{
			if (num32 > 1f)
			{
				num32 = 1f;
			}
		}
		else
		{
			num32 = 0f;
		}
		float num34 = maxRotationAngle - minRotationAngle;
		float num35 = num34 * num32;
		float num36 = num35 + minRotationAngle;
		detentTargetAngle = num36;
		goto IL_0c5b;
		IL_0c5b:
		accumulatedValue = num30;
		if (OnValueChanged != null)
		{
			float arg = (float)(ref obj2) + 103f;
			OnValueChanged.Invoke(arg);
		}
		lastQuantizedValue = num30;
		goto IL_06d3;
		IL_0cd9:
		if (useDetents && detentStepSize > 0f)
		{
			float num37 = Mathf.SmoothDamp(detentCurrentAngle, detentTargetAngle, ref *(float*)(this + 404), detentSmoothTime);
			detentCurrentAngle = num37;
			Transform transform4 = base.transform;
			ref Vector3 axis = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (DialInteractable)+68]");
			_ = 0;
			_ = rotationAxis;
			Quaternion quaternion = Quaternion.Internal_AngleAxis(detentCurrentAngle, ref axis);
			Quaternion localRotation = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
			_ = quaternion.x;
			transform4.localRotation = localRotation;
			if (dialMode != DialMode.Limited)
			{
				accumulatedValue = detentCurrentAngle;
			}
			else
			{
				currentRotationAngle = detentCurrentAngle;
			}
		}
		else
		{
			Transform transform5;
			float angle;
			if (dialMode != DialMode.Limited)
			{
				transform5 = base.transform;
				angle = accumulatedValue;
			}
			else
			{
				transform5 = base.transform;
				angle = currentRotationAngle;
			}
			ref Vector3 axis2 = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (DialInteractable)+68]");
			_ = 0;
			_ = rotationAxis;
			Quaternion quaternion2 = Quaternion.Internal_AngleAxis(angle, ref axis2);
			Quaternion localRotation2 = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
			_ = quaternion2.x;
			transform5.localRotation = localRotation2;
		}
		bool flag = IsAngleWithinDeadZone(currentRotationAngle);
		if (flag != _wasInDeadZone)
		{
			_wasInDeadZone = flag;
			(flag ? OnEnterDeadZone : OnExitDeadZone)?.Invoke();
		}
		MeasureRotationSpeed();
		return;
		IL_06d3:
		lastAngle = num22;
		goto IL_0cd9;
		IL_0c39:
		float arg2 = (float)(ref obj2) + 103f;
		onValueChanged.Invoke(arg2);
		goto IL_06d3;
		IL_0b23:
		currentRotationAngle = num27;
		float num38 = MapRotationToValue(num27);
		if (useDetents && detentStepSize > 0f)
		{
			float num39;
			float num40;
			if (dialMode == DialMode.Limited && constrainOutputBySystemHealth)
			{
				num39 = effectiveMaxOutputValue;
				num40 = effectiveMinOutputValue;
			}
			else
			{
				num39 = maxOutputValue;
				num40 = minOutputValue;
			}
			float num41 = num38 / detentStepSize;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
			num30 = num41 * detentStepSize;
			if (!(num40 > num30))
			{
				if (num30 > num39)
				{
					num30 = num39;
				}
			}
			else
			{
				num30 = num40;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj31 = default(object);
			if (obj31 == null)
			{
				bool flag2 = num40 == num39;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001805A1949h\"");
				if (!flag2)
				{
					float num42 = num39 - num40;
					float num43 = num30 - num40;
					num33 = num43 / num42;
					if (!(0f > num33))
					{
						if (num33 > 1f)
						{
							num33 = 1f;
						}
						goto IL_0bcc;
					}
				}
				num33 = 0f;
				goto IL_0bcc;
			}
		}
		else
		{
			onValueChanged = OnValueChanged;
			accumulatedValue = num38;
			detentTargetAngle = currentRotationAngle;
			if (OnValueChanged != null)
			{
				goto IL_0c39;
			}
		}
		goto IL_06d3;
	}

	private float MapRotationToValue(float rotationAngle)
	{
		//IL_00d4: Invalid comparison between I4 and F4
		//IL_00e3: Expected O, but got I4
		//IL_01f7: Expected F4, but got I4
		//IL_0036: Expected O, but got I4
		//IL_010c: Expected O, but got I4
		//IL_0056: Invalid comparison between O and F4
		//IL_0123: Expected O, but got I4
		//IL_0223: Invalid comparison between O and F4
		//IL_013a: Expected F4, but got I4
		//IL_01c7: Expected F4, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018059F7C0h\"");
		object obj;
		float num3;
		if (minRotationAngle == maxRotationAngle)
		{
			obj = 0;
		}
		else
		{
			float num = rotationAngle - minRotationAngle;
			float num2 = maxRotationAngle - minRotationAngle;
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
				goto IL_01fc;
			}
		}
		num3 = 0f;
		goto IL_01fc;
		IL_01fc:
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
		float num4;
		float num5;
		if (dialMode == DialMode.Limited && constrainOutputBySystemHealth)
		{
			num4 = effectiveMaxOutputValue;
			num5 = effectiveMinOutputValue;
		}
		else
		{
			num4 = maxOutputValue;
			num5 = minOutputValue;
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
		float num6 = num4 - num5;
		float num7 = num6 * num3;
		return num7 + num5;
	}

	public unsafe void BeginDialDrag()
	{
		//IL_02ac: Expected O, but got F4
		//IL_030c: Expected O, but got I4
		//IL_03d4: Expected F4, but got I
		//IL_03e6: Expected F4, but got I
		//IL_02e2: Expected O, but got I4
		//IL_03b3: Expected O, but got Ref
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
				Debug.LogError("[DialInteractable] No VirtualCursor assigned or found. Drag cannot begin.", this);
				return;
			}
		}
		if (raycastCamera == null)
		{
			Camera main = Camera.main;
			raycastCamera = main;
		}
		TryAcquireBrokerDragLockIfNeeded();
		if (!cursorManager.IsCurrentDeviceGamepad() || !ClampGamepadCursorToValve)
		{
			DynamicCursorManager dynamicCursorManager3 = cursorManager;
			if (!dynamicCursorManager3.ClampMouseToValveSetting)
			{
				goto IL_0277;
			}
		}
		bool flag = cursorManager.IsCurrentDeviceGamepad();
		bool isClampingMouse = (byte)((flag ? 1u : 0u) ^ 1u) != 0;
		float x;
		if (dialMode != DialMode.Limited)
		{
			Transform transform = base.transform;
			x = transform.position.x;
		}
		else
		{
			Transform transform2 = base.transform;
			x = transform2.position.x;
		}
		bool angleConstraint = default(bool);
		bool resetToDefault = default(bool);
		float minAngle = default(float);
		float maxAngle = default(float);
		cursorManager.ClampCursorToValve((Vector3)(&x), CursorDistanceMultiplierFromCenter, isClampingMouse, angleConstraint, resetToDefault, minAngle, maxAngle);
		goto IL_0277;
		IL_0277:
		isDragging = true;
		Vector3 pointerWorldPointOnDialPlane = GetPointerWorldPointOnDialPlane();
		bool flag2 = dialMode == DialMode.Limited;
		dragStart = (Vector3)pointerWorldPointOnDialPlane.x;
		_ = pointerWorldPointOnDialPlane.z;
		lastAngle = 0f;
		float num;
		if (!flag2)
		{
			num = accumulatedValue;
			object obj = 156;
		}
		else
		{
			lastRawAngle = currentRotationAngle;
			num = MapRotationToValue(currentRotationAngle);
			object obj = 160;
		}
		lastQuantizedValue = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v662 @ rax_v15+this @ rcx (DialInteractable)]");
		detentCurrentAngle = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v662 @ rax_v15+this @ rcx (DialInteractable)]");
		detentTargetAngle = 0f;
		detentVelocity = 0f;
		bool flag3 = IsAngleWithinDeadZone(currentRotationAngle);
		if (flag3 != _wasInDeadZone)
		{
			_wasInDeadZone = flag3;
		}
		if (OnGrab != null)
		{
			OnGrab.Invoke();
		}
		Action onBeginDialDrag = this.m_OnBeginDialDrag;
		if (this.m_OnBeginDialDrag != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v548.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
	}

	public void EndDialDrag()
	{
		cursorManager.DisableValveClamping();
		if (isDragging)
		{
			isDragging = false;
			ReleaseBrokerDragLockIfHeld();
			if (OnRelease != null)
			{
				OnRelease.Invoke();
			}
			Action onEndDialDrag = this.m_OnEndDialDrag;
			if (this.m_OnEndDialDrag != null)
			{
				IntPtr invoke_impl = ((Delegate)onEndDialDrag).invoke_impl;
				IntPtr method = ((Delegate)onEndDialDrag).method;
				IntPtr method_code = ((Delegate)onEndDialDrag).method_code;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v65 @ rax_v6 (System.IntPtr) (should have been resolved before IL gen)");
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
				string message = "[DialInteractable] InteractionLockBroker not found (tag='" + lockBrokerTag + "'). Drag lock not acquired.";
				Debug.LogWarning(message, this);
			}
		}
	}

	private unsafe void ReleaseBrokerDragLockIfHeld()
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_0091: Expected O, but got I4
		InteractionLockBroker.LockHandle lockHandle = (InteractionLockBroker.LockHandle)(this + 424);
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

	private unsafe Vector3 GetPointerWorldPointOnDialPlane()
	{
		//IL_0008: Expected O, but got Ref
		//IL_04e7: Expected I, but got O
		//IL_0505: Expected F4, but got O
		//IL_0500: Expected native int or pointer, but got O
		//IL_051a: Expected F4, but got I
		//IL_0515: Expected native int or pointer, but got O
		//IL_0082: Expected O, but got Ref
		//IL_00cd: Expected O, but got Ref
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Expected O, but got Unknown
		//IL_012a: Expected F4, but got I4
		//IL_0133: Expected F4, but got I4
		//IL_013c: Expected F4, but got I4
		//IL_01b9: Expected O, but got Ref
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Expected O, but got Unknown
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Expected O, but got Unknown
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Expected O, but got Unknown
		//IL_047f: Invalid comparison between F4 and O
		//IL_02b7: Expected O, but got I4
		//IL_04a1: Expected I, but got O
		//IL_04bf: Expected F4, but got O
		//IL_04ba: Expected native int or pointer, but got O
		//IL_04d4: Expected F4, but got I
		//IL_04cf: Expected native int or pointer, but got O
		//IL_02e1: Invalid comparison between F4 and I4
		//IL_02f0: Invalid comparison between F4 and I4
		//IL_0319: Expected O, but got I4
		//IL_0364: Expected native int or pointer, but got O
		//IL_0371: Expected native int or pointer, but got O
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		Vector3 vector2 = default(Vector3);
		if (virtualCursor != null && raycastCamera != null)
		{
			Transform transform = base.transform;
			if ((object)transform != null)
			{
				object obj3 = default(object);
				Vector3 vector = transform.TransformDirection((Vector3)(&obj3));
				Transform transform2 = base.transform;
				if ((object)transform2 != null)
				{
					Vector3 position = transform2.position;
					object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
					_ = position.x;
					_ = vector.x;
					_ = vector.z;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
					float num;
					float num2;
					float num3;
					if (!(position.x > 1E-05f))
					{
						num = 0f;
						num2 = 0f;
						num3 = 0f;
					}
					else
					{
						num3 = vector.x / position.x;
						object obj5 = default(object);
						num2 = (float)obj5 / position.x;
						num = vector.z / position.x;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-75]");
					float num4 = 0f * num2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-79]");
					float num5 = 0f * num3;
					float num6 = position.z * num;
					float num7 = num4 + num5;
					float num8 = num7 + num6;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
					object obj6 = num8 ^ 0;
					if ((object)virtualCursor != null && (object)raycastCamera != null)
					{
						Vector3 pos = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 121));
						_ = 0;
						Ray ray = raycastCamera.ScreenPointToRay(pos);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v448 @ rax_v22 (UnityEngine.Ray)+10]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v448 @ rax_v22 (UnityEngine.Ray)+10]");
						float num9 = 0f * num2;
						float num11 = default(float);
						float num10 = num11 * num2;
						float num12 = num11 * num3;
						float num13 = num11 * num;
						float num14 = num9 + num12;
						float num15 = (float)ray.m_Origin * num3;
						float num16 = num11 * num;
						float num17 = num10 + num15;
						float num18 = num14 + num13;
						float num19 = num17 + num16;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
						object obj7 = num19 ^ 0;
						object obj8 = obj7 - obj6;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
						object obj9 = num18 & 0;
						float num20 = 0f - num18;
						if ((nint)obj9 < 0)
						{
							obj9 = 0;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
						object obj10 = num20 & 0;
						float num21 = Mathf.Epsilon * 8f;
						float num22 = (float)obj9 * 1E-06f;
						if (num22 < num21)
						{
							num22 = num21;
						}
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num22) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj10))
						{
							float num23 = (float)obj8 / num18;
							bool flag = num23 < 0f;
							bool flag2 = num23 == 0f;
							bool flag3 = !flag;
							bool flag4 = !flag2;
							object obj11 = flag4 & flag3;
							if (obj11 != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-35]");
								float num24 = 0f * num23;
								float z = num24 + num11;
								((Vector3*)(nint)vector2)->x = num11;
								((Vector3*)(nint)vector2)->z = z;
								goto IL_0574;
							}
						}
						nint num25 = (nint)typeof(Vector3);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v648 @ rax_v28 (Il2CppClass<UnityEngine.Vector3>)+B8]");
						nint num26 = 0;
						((Vector3*)(nint)vector2)->x = (float)Vector3.zeroVector;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v649 @ rax_v29 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
						((Vector3*)(nint)vector2)->z = 0f;
						goto IL_0574;
					}
				}
			}
			return (Vector3)new NullReferenceException();
		}
		nint num27 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rax_v8 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num28 = 0;
		((Vector3*)(nint)vector2)->x = (float)Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v128 @ rax_v9 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		((Vector3*)(nint)vector2)->z = 0f;
		goto IL_0574;
		IL_0574:
		return vector2;
	}

	public unsafe void ResetToMinimum()
	{
		//IL_0084: Expected O, but got Ref
		//IL_00b2: Expected F4, but got Ref
		if (dialMode == DialMode.Limited)
		{
			currentRotationAngle = minRotationAngle;
			lastRawAngle = minRotationAngle;
			float num = MapRotationToValue(minRotationAngle);
			detentTargetAngle = minRotationAngle;
			detentCurrentAngle = minRotationAngle;
			accumulatedValue = num;
			Transform transform = base.transform;
			Vector3 axis = default(Vector3);
			Quaternion quaternion = Quaternion.Internal_AngleAxis(minRotationAngle, ref axis);
			transform.localRotation = (Quaternion)(&axis);
			if (OnValueChanged != null)
			{
				object obj = default(object);
				OnValueChanged.Invoke((nint)(&obj));
			}
			lastQuantizedValue = accumulatedValue;
			detentVelocity = 0f;
			bool flag = IsAngleWithinDeadZone(currentRotationAngle);
			if (flag != _wasInDeadZone)
			{
				_wasInDeadZone = flag;
				(flag ? OnEnterDeadZone : OnExitDeadZone)?.Invoke();
			}
		}
	}

	public unsafe void SetDialValue(float value)
	{
		//IL_010c: Invalid comparison between I4 and F4
		//IL_011b: Expected O, but got I4
		//IL_0336: Expected F4, but got I4
		//IL_00a3: Expected O, but got I4
		//IL_0351: Invalid comparison between O and F4
		//IL_0144: Expected O, but got I4
		//IL_0172: Expected F4, but got I4
		//IL_0164: Expected O, but got I4
		//IL_0184: Expected O, but got Ref
		//IL_01bc: Expected F4, but got Ref
		if (dialMode != DialMode.Limited)
		{
			return;
		}
		float num;
		float num2;
		if (!constrainOutputBySystemHealth)
		{
			num = maxOutputValue;
			num2 = minOutputValue;
		}
		else
		{
			num = effectiveMaxOutputValue;
			num2 = effectiveMinOutputValue;
		}
		float num3;
		if (!(num2 > value))
		{
			bool flag = !(value > num);
			num3 = value;
			if (!flag)
			{
				num3 = num;
			}
		}
		else
		{
			num3 = num2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001805A091Dh\"");
		object obj;
		float num6;
		if (num2 == num)
		{
			obj = 0;
		}
		else
		{
			float num4 = num - num2;
			float num5 = num3 - num2;
			num6 = num5 / num4;
			bool flag2 = 0f > num6;
			obj = 0;
			if (!flag2)
			{
				bool flag3 = !(num6 > 1f);
				obj = 0;
				if (!flag3)
				{
					num6 = 1f;
					obj = 0;
				}
				goto IL_033b;
			}
		}
		num6 = 0f;
		goto IL_033b;
		IL_033b:
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
		float num8 = maxRotationAngle - minRotationAngle;
		float num9 = num8 * num7;
		detentCurrentAngle = (detentTargetAngle = (lastRawAngle = (currentRotationAngle = num9 + minRotationAngle)));
		Transform transform = base.transform;
		Vector3 axis = default(Vector3);
		Quaternion quaternion = Quaternion.Internal_AngleAxis(currentRotationAngle, ref axis);
		transform.localRotation = (Quaternion)(&axis);
		accumulatedValue = num3;
		if (OnValueChanged != null)
		{
			object obj2 = default(object);
			OnValueChanged.Invoke((nint)(&obj2));
		}
		lastQuantizedValue = num3;
		detentVelocity = 0f;
		bool flag4 = IsAngleWithinDeadZone(currentRotationAngle);
		if (flag4 != _wasInDeadZone)
		{
			_wasInDeadZone = flag4;
			(flag4 ? OnEnterDeadZone : OnExitDeadZone)?.Invoke();
		}
	}

	public unsafe void SetAccumulatedValueUnlimited(float angleDegrees, bool fireValueChangedEvent = false, bool smoothToTarget = true)
	{
		//IL_0039: Invalid comparison between F4 and I4
		//IL_00b1: Expected O, but got Ref
		//IL_00fd: Expected F4, but got Ref
		if (dialMode != DialMode.Unlimited)
		{
			return;
		}
		bool flag = !useDetents;
		accumulatedValue = angleDegrees;
		Transform transform;
		float angle;
		if (!flag && detentStepSize > 0f)
		{
			detentTargetAngle = angleDegrees;
			if (smoothToTarget)
			{
				goto IL_00b6;
			}
			detentCurrentAngle = angleDegrees;
			transform = base.transform;
			angle = detentCurrentAngle;
		}
		else
		{
			transform = base.transform;
			angle = accumulatedValue;
		}
		Vector3 axis = default(Vector3);
		Quaternion quaternion = Quaternion.Internal_AngleAxis(angle, ref axis);
		transform.localRotation = (Quaternion)(&axis);
		goto IL_00b6;
		IL_00b6:
		if (fireValueChangedEvent && OnValueChanged != null)
		{
			object obj = default(object);
			OnValueChanged.Invoke((nint)(&obj));
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

	public void InvalidateRotationSpeed()
	{
		_speedPrevRotationValid = false;
	}

	private void MeasureRotationSpeed()
	{
		//IL_014b: Expected O, but got F4
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Expected O, but got Unknown
		//IL_01f5: Invalid comparison between I4 and F4
		//IL_0116: Expected F4, but got I4
		//IL_01d7: Expected O, but got F4
		Transform transform = base.transform;
		Quaternion localRotation = transform.localRotation;
		if (_speedPrevRotationValid)
		{
			float deltaTime = Time.deltaTime;
			if (1E-06f > deltaTime)
			{
				return;
			}
			Quaternion rotation = default(Quaternion);
			Quaternion quaternion = Quaternion.Internal_Inverse(ref rotation);
			Quaternion q = default(Quaternion);
			Quaternion.Internal_ToAxisAngleRad(ref q, out Vector3 _, out float angle);
			float num = angle * 57.29578f;
			if (num > 180f)
			{
				float num2 = 360f - num;
				num = num2;
			}
			float num3 = maxExpectedDegreesPerSecond;
			float num4 = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = num4 & 0;
			float num5 = (float)obj / deltaTime;
			if (1f > maxExpectedDegreesPerSecond)
			{
				num3 = 1f;
			}
			float num6 = num5 / num3;
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
			_003CMeasuredRotationSpeed_003Ek__BackingField = num5;
			_003CNormalizedRotationSpeed_003Ek__BackingField = num6;
			inspectorMeasuredRotationSpeed = num5;
			inspectorNormalizedRotationSpeed = num6;
			_speedPrevRotation = (Quaternion)localRotation.x;
		}
		else
		{
			_speedPrevRotationValid = true;
			_003CMeasuredRotationSpeed_003Ek__BackingField = 0f;
			inspectorMeasuredRotationSpeed = 0f;
			_speedPrevRotation = (Quaternion)localRotation.x;
		}
	}

	private bool IsAngleWithinDeadZone(float angle)
	{
		if (!deadZoneEnabled || dialMode != DialMode.Limited)
		{
			goto IL_0159;
		}
		float num = deadZoneMinAngle;
		if (deadZoneMinAngle > deadZoneMaxAngle)
		{
			num = deadZoneMaxAngle;
		}
		float num2 = minRotationAngle;
		if (!(minRotationAngle > num))
		{
			num2 = maxRotationAngle;
			if (!(num > maxRotationAngle))
			{
				goto IL_0187;
			}
		}
		num = num2;
		goto IL_0187;
		IL_01b4:
		float num3 = default(float);
		if (!(minRotationAngle > num3))
		{
			if (num3 > maxRotationAngle)
			{
				num3 = maxRotationAngle;
			}
		}
		else
		{
			num3 = minRotationAngle;
		}
		float num4;
		if (!(num3 < num))
		{
			bool flag = num4 < num3;
			return !flag;
		}
		goto IL_0159;
		IL_0159:
		return false;
		IL_0187:
		num4 = deadZoneMinAngle;
		if (deadZoneMinAngle < deadZoneMaxAngle)
		{
			num4 = deadZoneMaxAngle;
		}
		float num5 = minRotationAngle;
		if (!(minRotationAngle > num4))
		{
			num5 = maxRotationAngle;
			if (!(num4 > maxRotationAngle))
			{
				goto IL_01b4;
			}
		}
		num4 = num5;
		goto IL_01b4;
	}

	private void EvaluateDeadZoneTransition(bool fireEvents)
	{
		bool flag = IsAngleWithinDeadZone(currentRotationAngle);
		if (flag != _wasInDeadZone)
		{
			_wasInDeadZone = flag;
			if (fireEvents)
			{
				(flag ? OnEnterDeadZone : OnExitDeadZone)?.Invoke();
			}
		}
	}

	private void OnValidate()
	{
		if (minRotationAngle > maxRotationAngle)
		{
			maxRotationAngle = minRotationAngle;
		}
		float num = deadZoneMinAngle;
		bool flag = minRotationAngle > deadZoneMinAngle;
		float num2 = minRotationAngle;
		if (!flag)
		{
			num2 = maxRotationAngle;
			if (!(deadZoneMinAngle > maxRotationAngle))
			{
				goto IL_00f9;
			}
		}
		num = num2;
		goto IL_00f9;
		IL_00f9:
		float num3 = minRotationAngle;
		float num4 = deadZoneMaxAngle;
		deadZoneMinAngle = num;
		if (!(minRotationAngle > deadZoneMaxAngle))
		{
			num3 = maxRotationAngle;
			if (!(deadZoneMaxAngle > maxRotationAngle))
			{
				goto IL_0137;
			}
		}
		num4 = num3;
		goto IL_0137;
		IL_0137:
		deadZoneMaxAngle = num4;
		if (num > num4)
		{
			deadZoneMaxAngle = num;
		}
		SetEffectiveRange(minOutputValue, maxOutputValue, forceNotify: true);
		if (constrainOutputBySystemHealth && highPressureSystemManager != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 185 Invalid \"Jump target not found in method: 0x1805A03D0\"");
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
				_003CAutoFindCursorManagerRoutine_003Ed__121 obj = new _003CAutoFindCursorManagerRoutine_003Ed__121(0);
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
		_003CAutoFindCursorManagerRoutine_003Ed__121 obj = new _003CAutoFindCursorManagerRoutine_003Ed__121(0);
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
			BeginDialDrag();
		}
	}

	private void HandlePrimaryClickUp(Interactable pressSourceTarget)
	{
		if (useCursorManagerIntegration && _pressBeganHere)
		{
			EndDialDrag();
			_pressBeganHere = false;
		}
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

	private void EnsureSystemManagerSubscription()
	{
		if (this.highPressureSystemManager == null && autoFindSystemManagerById)
		{
			HighPressureSystemManager highPressureSystemManager = HighPressureSystemManager.FindBySystemId(systemIdForAutoFind);
			this.highPressureSystemManager = highPressureSystemManager;
		}
		bool flag = this.highPressureSystemManager != null;
		if (!flag)
		{
			if (_findHpsRoutine == null && autoFindSystemManagerById != flag)
			{
				_003CAutoFindSystemManagerRoutine_003Ed__128 obj = new _003CAutoFindSystemManagerRoutine_003Ed__128(0);
				obj._003C_003E1__state = 0;
				obj._003C_003E4__this = this;
				Coroutine findHpsRoutine = StartCoroutine(obj);
				_findHpsRoutine = findHpsRoutine;
			}
		}
		else
		{
			SubscribeToSystemManager();
		}
	}

	private IEnumerator AutoFindSystemManagerRoutine()
	{
		_003CAutoFindSystemManagerRoutine_003Ed__128 obj = new _003CAutoFindSystemManagerRoutine_003Ed__128(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private void SubscribeToSystemManager()
	{
		if (!_subscribedToHps && this.highPressureSystemManager != null)
		{
			Action<float> value = HandleSystemHealthChanged01;
			this.highPressureSystemManager.SystemHealthChanged01 += value;
			HighPressureSystemManager highPressureSystemManager = this.highPressureSystemManager;
			_subscribedToHps = true;
			RecomputeEffectiveRangeFromHealth(highPressureSystemManager.currentHealth01, forceNotify: true);
		}
	}

	private void UnsubscribeFromSystemManager()
	{
		if (_subscribedToHps && highPressureSystemManager != null)
		{
			Action<float> value = HandleSystemHealthChanged01;
			highPressureSystemManager.SystemHealthChanged01 -= value;
			_subscribedToHps = false;
		}
	}

	private void HandleSystemHealthChanged01(float health01)
	{
		if (constrainOutputBySystemHealth)
		{
			RecomputeEffectiveRangeFromHealth(health01, forceNotify: true);
		}
	}

	private void RecomputeEffectiveRangeFromHealth(float health01, bool forceNotify)
	{
		//IL_0134: Invalid comparison between I4 and F4
		//IL_01d1: Expected F4, but got I4
		//IL_0069: Invalid comparison between I4 and F4
		//IL_02f3: Invalid comparison between I4 and F4
		//IL_0166: Expected O, but got F4
		//IL_0173: Expected O, but got F4
		//IL_0198: Invalid comparison between F4 and I4
		//IL_01a7: Invalid comparison between F4 and I4
		//IL_00bc: Expected F4, but got I4
		//IL_0217: Expected F4, but got I4
		//IL_02cf: Expected O, but got I4
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Expected O, but got Unknown
		//IL_02a2: Invalid comparison between I4 and F4
		//IL_0343: Invalid comparison between I4 and F4
		//IL_0253: Expected F4, but got I4
		//IL_00df: Expected O, but got F4
		//IL_00ec: Expected O, but got F4
		//IL_0111: Invalid comparison between F4 and I4
		//IL_0120: Invalid comparison between F4 and I4
		float num;
		if (constrainOutputBySystemHealth && dialMode == DialMode.Limited)
		{
			bool flag2;
			bool flag3;
			bool flag4;
			if (healthToRangeScale != null)
			{
				float time;
				if (!(0f > health01))
				{
					bool flag = !(health01 > 1f);
					time = health01;
					if (!flag)
					{
						time = 1f;
					}
				}
				else
				{
					time = 0f;
				}
				num = healthToRangeScale.Evaluate(time);
				if (0f > num)
				{
					goto IL_01c8;
				}
				float num2 = num - 1f;
				object obj = num ^ 1f;
				object obj2 = num ^ num2;
				object obj3 = obj & obj2;
				flag2 = (nint)obj3 < 0;
				flag3 = num2 < 0f;
				flag4 = num2 == 0f;
			}
			else
			{
				if (0f > health01)
				{
					goto IL_01c8;
				}
				float num3 = health01 - 1f;
				object obj4 = health01 ^ 1f;
				object obj5 = health01 ^ num3;
				object obj6 = obj4 & obj5;
				flag2 = (nint)obj6 < 0;
				flag3 = num3 < 0f;
				flag4 = num3 == 0f;
				num = health01;
			}
			bool flag5 = flag3 == flag2;
			object obj7 = !flag5;
			object obj8 = obj7 | flag4;
			if (obj8 == null)
			{
				num = 1f;
			}
			goto IL_02ea;
		}
		float newMin = minOutputValue;
		float num4 = maxOutputValue;
		goto IL_03b1;
		IL_02ea:
		float num5 = ((0f > num) ? 0f : ((num > 1f) ? 1f : num));
		float num6 = minOutputValue - clampCenterValue;
		float num7 = num6 * num5;
		float num8 = num7 + clampCenterValue;
		if (!(0f > num))
		{
			if (num > 1f)
			{
				num = 1f;
			}
		}
		else
		{
			num = 0f;
		}
		float num9 = maxOutputValue - clampCenterValue;
		float num10 = num9 * num;
		num4 = num10 + clampCenterValue;
		bool flag6 = !(num8 > num4);
		newMin = num8;
		if (!flag6)
		{
			newMin = num4;
			num4 = num8;
		}
		goto IL_03b1;
		IL_03b1:
		SetEffectiveRange(newMin, num4, forceNotify);
		return;
		IL_01c8:
		num = 0f;
		goto IL_02ea;
	}

	private unsafe void SetEffectiveRange(float newMin, float newMax, bool forceNotify)
	{
		//IL_0052: Expected O, but got I4
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		//IL_0120: Expected F4, but got Ref
		object obj2;
		if (!forceNotify)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj = default(object);
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
				object obj3 = default(object);
				obj2 = obj3 ^ 1;
				goto IL_0144;
			}
		}
		obj2 = 1;
		goto IL_0144;
		IL_0144:
		bool flag = !logHealthClamping;
		effectiveMinOutputValue = newMin;
		effectiveMaxOutputValue = newMax;
		if (!flag)
		{
			bool isPlaying = Application.isPlaying;
		}
		if (obj2 == null || dialMode != DialMode.Limited)
		{
			return;
		}
		float num = MapRotationToValue(currentRotationAngle);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj4 = default(object);
		if (obj4 == null)
		{
			accumulatedValue = num;
			if (OnValueChanged != null)
			{
				object obj5 = default(object);
				OnValueChanged.Invoke((nint)(&obj5));
			}
			lastQuantizedValue = accumulatedValue;
		}
	}

	private unsafe void GetActiveOutputRange(out float minV, out float maxV)
	{
		//IL_0060: Expected Ref, but got F4
		//IL_006a: Expected Ref, but got F4
		//IL_004b: Expected Ref, but got F4
		//IL_0055: Expected Ref, but got F4
		if (dialMode == DialMode.Limited && constrainOutputBySystemHealth)
		{
			ref float reference = ref *(float*)effectiveMinOutputValue;
			ref float reference2 = ref *(float*)effectiveMaxOutputValue;
		}
		else
		{
			ref float reference = ref *(float*)minOutputValue;
			ref float reference2 = ref *(float*)maxOutputValue;
		}
	}

	public DialInteractable()
	{
		//IL_0108: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AD91]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		useCursorManagerIntegration = true;
		cursorManagerTag = "CursorManager";
		autoFindRetrySeconds = 0.5f;
		alwaysReleaseToSameTarget = true;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v5 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		rotationAxis = Vector3.forwardVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ rcx_v4 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
		_ = 0;
		maxRotationAngle = 180f;
		maxOutputValue = 100f;
		detentStepSize = 1f;
		detentSmoothTime = 0.08f;
		deadZoneMinAngle = 80f;
		deadZoneMaxAngle = 100f;
		useBrokerLockWhileDragging = true;
		lockBrokerTag = "LockBroker";
		brokerDebugLabel = "DialInteractable:Drag";
		autoFindSystemManagerById = true;
		systemIdForAutoFind = "Default";
		autoFindHpsRetrySeconds = 0.5f;
		healthToRangeScale = AnimationCurve.Linear(0f, 0f, 1f, 1f);
		maxExpectedDegreesPerSecond = 180f;
		ClampGamepadCursorToValve = true;
		CursorDistanceMultiplierFromCenter = 0.05f;
		ClampedMinRotationAngle = 45f;
		ClampedMaxRotationAngle = 135f;
		lastQuantizedValue = -3.4028235E+38f;
		_lastAnnouncedEffectiveMin = 0f / 0f;
		_lastAnnouncedEffectiveMax = 0f / 0f;
		base._002Ector();
	}
}
