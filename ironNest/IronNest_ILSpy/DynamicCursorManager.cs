using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.InputSystem;

public class DynamicCursorManager : MonoBehaviour
{
	public enum PresentationMode
	{
		FPSLocked,
		FreeMouse
	}

	public enum CursorVisualState
	{
		Default,
		Hover,
		Grab
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Comparison<RaycastHit> _003C_003E9__76_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal unsafe int _003CPerformHoverDetection_003Eb__76_0(RaycastHit a, RaycastHit b)
		{
			float distance = ((RaycastHit*)a)->distance;
			float distance2 = ((RaycastHit*)b)->distance;
			float num = default(float);
			return num.CompareTo(distance2);
		}
	}

	private Action<CursorVisualState> m_OnCursorVisualStateChanged;

	private Action<Interactable> m_OnCursorTargetChanged;

	private Action<Interactable> m_OnPassiveTargetChanged;

	private Action<Interactable> m_OnPrimaryClickDown;

	private Action<Interactable> m_OnPrimaryClickUp;

	private Action<bool> m_OnSuppressedByLockBrokerChanged;

	private PresentationMode initialMode;

	private float maxRayDistance = 5f;

	private LayerMask interactableLayers;

	private LayerMask cursorBlockerLayers;

	private float raycastInterval;

	private bool debugDrawRay;

	private bool stopAtFirstValidHit;

	private bool ignoreTriggerColliders;

	private Camera raycastCamera;

	private bool hideSystemCursor;

	private bool lockCursorInFPSMode;

	private bool confineSystemCursorInFreeMode;

	private bool persistGrabDuringDrag;

	private bool forceDefaultAfterGrabEnd;

	private bool emitHoverChangeEvents;

	private bool emitVisualStateEvents;

	private bool broadcastStateOnModeSwitch;

	private bool restoreSystemCursorOnDisable;

	private bool emitPrimaryClickEvents;

	private List<InputActionReference> primaryClickActions;

	private bool enableActionsOnEnable;

	private bool sanitizeClickStateEachFrame;

	private bool logClickAggregation;

	private VirtualCursor virtualCursor;

	private bool routeMapPieceDragsThroughManager;

	private bool autoAssignVirtualCursorToMapPieces;

	private bool routeDraggableItemsThroughManager;

	private PlayerInput _playerInput;

	private PresentationMode _currentMode;

	private float _lastRayTime;

	private Interactable _currentHover;

	private Interactable _currentPassiveHover;

	private ICursorDraggable _activeDrag;

	private CursorVisualState _currentVisualState;

	private bool _clickPressed;

	private bool _clickDownThisFrame;

	private bool _clickWasPressedLastFrame;

	private Interactable _pressSourceForBroadcast;

	private bool _uiIsBlockingWorld;

	private bool _uiWantsHoverState;

	private bool _worldIsBlockedByOccluder;

	private readonly Dictionary<InputAction, Action<InputAction.CallbackContext>> _startedHandlers;

	private readonly Dictionary<InputAction, Action<InputAction.CallbackContext>> _canceledHandlers;

	private ICursorDraggable _capturedDraggableOnPress;

	private MapPiece3D _capturedMapPieceOnPress;

	private DraggableItem _capturedDraggableItemOnPress;

	private bool _suppressedByLockBroker;

	private bool _003CIsClampedToValve_003Ek__BackingField;

	private bool _003CIsClampingMouse_003Ek__BackingField;

	private Vector2 _003CValveScreenPosition_003Ek__BackingField;

	private Vector2 _003CValveDefaultScreenPosition_003Ek__BackingField;

	private float _003CCursorDistanceMultiplierFromCenter_003Ek__BackingField;

	private bool _003CIsAngleConstrained_003Ek__BackingField;

	private bool _003CResetToDefault_003Ek__BackingField;

	private float _003CMinAngle_003Ek__BackingField;

	private float _003CMaxAngle_003Ek__BackingField;

	public bool ClampMouseToValveSetting;

	public bool IsSuppressedByLockBroker => _suppressedByLockBroker;

	public VirtualCursor VirtualCursorRef => virtualCursor;

	public PresentationMode CurrentMode => _currentMode;

	public CursorVisualState CurrentVisualState => _currentVisualState;

	public Interactable CurrentHover => _currentHover;

	public Interactable CurrentPassiveHover => _currentPassiveHover;

	public bool IsDragging
	{
		get
		{
			if (_activeDrag == null)
			{
				return false;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			bool result = default(bool);
			return result;
		}
	}

	public bool IsWorldBlockedByCursorBlocker => _worldIsBlockedByOccluder;

	public bool IsClampedToValve
	{
		get
		{
			return _003CIsClampedToValve_003Ek__BackingField;
		}
		private set
		{
			_003CIsClampedToValve_003Ek__BackingField = value;
		}
	}

	public bool IsClampingMouse
	{
		get
		{
			return _003CIsClampingMouse_003Ek__BackingField;
		}
		private set
		{
			_003CIsClampingMouse_003Ek__BackingField = value;
		}
	}

	public Vector2 ValveScreenPosition
	{
		get
		{
			Vector2 result = default(Vector2);
			return result;
		}
		private set
		{
			_003CValveScreenPosition_003Ek__BackingField = value;
		}
	}

	public Vector2 ValveDefaultScreenPosition
	{
		get
		{
			Vector2 result = default(Vector2);
			return result;
		}
		private set
		{
			_003CValveDefaultScreenPosition_003Ek__BackingField = value;
		}
	}

	public float CursorDistanceMultiplierFromCenter
	{
		get
		{
			return _003CCursorDistanceMultiplierFromCenter_003Ek__BackingField;
		}
		private set
		{
			_003CCursorDistanceMultiplierFromCenter_003Ek__BackingField = value;
		}
	}

	public bool IsAngleConstrained
	{
		get
		{
			return _003CIsAngleConstrained_003Ek__BackingField;
		}
		private set
		{
			_003CIsAngleConstrained_003Ek__BackingField = value;
		}
	}

	public bool ResetToDefault
	{
		get
		{
			return _003CResetToDefault_003Ek__BackingField;
		}
		private set
		{
			_003CResetToDefault_003Ek__BackingField = value;
		}
	}

	public float MinAngle
	{
		get
		{
			return _003CMinAngle_003Ek__BackingField;
		}
		private set
		{
			_003CMinAngle_003Ek__BackingField = value;
		}
	}

	public float MaxAngle
	{
		get
		{
			return _003CMaxAngle_003Ek__BackingField;
		}
		private set
		{
			_003CMaxAngle_003Ek__BackingField = value;
		}
	}

	public Interactable CurrentGrabInteractable
	{
		get
		{
			//IL_0013: Expected I, but got O
			//IL_001b: Expected I, but got O
			//IL_002b: Expected O, but got I
			//IL_0067: Expected O, but got I
			ICursorDraggable activeDrag = _activeDrag;
			Interactable result;
			if (_activeDrag != null)
			{
				nint num = (nint)typeof(Component);
				nint num2 = (nint)activeDrag;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rdx_v3 (Il2CppClass<UnityEngine.Component>)+130]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v3 (Il2CppClass<ICursorDraggable>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rdx_v3 (Il2CppClass<UnityEngine.Component>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v3 (Il2CppClass<ICursorDraggable>)+C8]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ rax_v6+FFFFFFF8+v44 @ rax_v5*8]");
					if (0 == (nint)typeof(Component))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
						Interactable interactable = default(Interactable);
						bool flag = (object)interactable != null;
						result = interactable;
						if (!flag)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
							return interactable;
						}
						goto IL_0108;
					}
				}
			}
			result = null;
			goto IL_0108;
			IL_0108:
			return result;
		}
	}

	public event Action<CursorVisualState> OnCursorVisualStateChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 32;
			Delegate obj2 = this.m_OnCursorVisualStateChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 32;
			Delegate obj2 = this.m_OnCursorVisualStateChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action<Interactable> OnCursorTargetChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 40;
			Delegate obj2 = this.m_OnCursorTargetChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 40;
			Delegate obj2 = this.m_OnCursorTargetChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action<Interactable> OnPassiveTargetChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 48;
			Delegate obj2 = this.m_OnPassiveTargetChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 48;
			Delegate obj2 = this.m_OnPassiveTargetChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action<Interactable> OnPrimaryClickDown
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 56;
			Delegate obj2 = this.m_OnPrimaryClickDown;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 56;
			Delegate obj2 = this.m_OnPrimaryClickDown;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action<Interactable> OnPrimaryClickUp
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 64;
			Delegate obj2 = this.m_OnPrimaryClickUp;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 64;
			Delegate obj2 = this.m_OnPrimaryClickUp;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action<bool> OnSuppressedByLockBrokerChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 72;
			Delegate obj2 = this.m_OnSuppressedByLockBrokerChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 72;
			Delegate obj2 = this.m_OnSuppressedByLockBrokerChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	private void Awake()
	{
		if (raycastCamera == null)
		{
			Camera main = Camera.main;
			raycastCamera = main;
		}
		bool flag = !hideSystemCursor;
		_currentMode = initialMode;
		Cursor.visible = flag;
		CursorLockMode lockState;
		if (_currentMode != PresentationMode.FPSLocked)
		{
			if (hideSystemCursor)
			{
				lockState = CursorLockMode.None;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb ecx,ecx\"");
				lockState = (CursorLockMode)((flag ? 1 : 0) & 2);
			}
		}
		else
		{
			bool flag2 = !lockCursorInFPSMode;
			bool flag3 = !flag2;
			lockState = (flag3 ? CursorLockMode.Locked : CursorLockMode.None);
		}
		Cursor.lockState = lockState;
		if (emitVisualStateEvents)
		{
			Action<CursorVisualState> onCursorVisualStateChanged = this.m_OnCursorVisualStateChanged;
			if (this.m_OnCursorVisualStateChanged != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v139 @ rcx_v8 (System.Action`1<DynamicCursorManager+CursorVisualState>)+18] (should have been resolved before IL gen)");
			}
		}
	}

	private void OnEnable()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<InputActionReference>.Enumerator enumerator = default(List<InputActionReference>.Enumerator);
		UnityEngine.Object obj = default(UnityEngine.Object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (!(obj != null))
				{
					continue;
				}
				if ((object)obj != null)
				{
					InputAction action = ((InputActionReference)obj).action;
					if (action == null)
					{
						continue;
					}
					if (enableActionsOnEnable)
					{
						InputAction action2 = ((InputActionReference)obj).action;
						if (action2 == null)
						{
							throw new NullReferenceException();
						}
						if (!action2.enabled)
						{
							InputAction action3 = ((InputActionReference)obj).action;
							if (action3 == null)
							{
								break;
							}
							action3.Enable();
						}
					}
					InputAction action4 = ((InputActionReference)obj).action;
					if (_startedHandlers != null)
					{
						if (_startedHandlers.ContainsKey(action4))
						{
							continue;
						}
						Action<InputAction.CallbackContext> value = delegate
						{
							if (logClickAggregation)
							{
								Debug.Log("[DCM] ClickStarted callback (edge resolution deferred to Update).");
							}
						};
						Action<InputAction.CallbackContext> value2 = delegate
						{
							if (logClickAggregation)
							{
								Debug.Log("[DCM] ClickCanceled callback (edge resolution deferred to Update).");
							}
						};
						InputAction action5 = ((InputActionReference)obj).action;
						if (_startedHandlers != null)
						{
							_startedHandlers.set_Item(action5, value);
							InputAction action6 = ((InputActionReference)obj).action;
							if (_canceledHandlers != null)
							{
								_canceledHandlers.set_Item(action6, value2);
								InputAction action7 = ((InputActionReference)obj).action;
								if (action7 != null)
								{
									action7.started += value;
									InputAction action8 = ((InputActionReference)obj).action;
									if (action8 != null)
									{
										action8.canceled += value2;
										continue;
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			if (broadcastStateOnModeSwitch && emitVisualStateEvents)
			{
				Action<CursorVisualState> onCursorVisualStateChanged = this.m_OnCursorVisualStateChanged;
				if (this.m_OnCursorVisualStateChanged != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v502 @ rcx_v19 (System.Action`1<DynamicCursorManager+CursorVisualState>)+18] (should have been resolved before IL gen)");
				}
			}
			bool flag = (Cursor.visible = !hideSystemCursor);
			bool lockState;
			if (_currentMode != PresentationMode.FPSLocked)
			{
				bool flag3 = hideSystemCursor;
				lockState = false;
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb ecx,ecx\"");
					bool flag4 = (byte)((flag ? 1u : 0u) & 2u) != 0;
					lockState = flag4;
				}
			}
			else
			{
				bool flag5 = !lockCursorInFPSMode;
				lockState = !flag5;
			}
			Cursor.lockState = (lockState ? CursorLockMode.Locked : CursorLockMode.None);
			return;
		}
		throw new NullReferenceException();
	}

	private void Update()
	{
		//IL_0088: Invalid comparison between F4 and I4
		//IL_0099: Invalid comparison between I4 and F4
		//IL_00ef: Invalid comparison between F4 and I4
		//IL_00b6: Expected F4, but got I4
		//IL_067d: Expected O, but got I
		//IL_068d: Expected O, but got I
		//IL_069d: Expected O, but got I
		//IL_05cd: Expected O, but got I
		//IL_05dd: Expected O, but got I
		//IL_05ed: Expected O, but got I
		//IL_05f6: Expected O, but got I4
		if (!(raycastCamera != null))
		{
			return;
		}
		ResolvePressStateFromActions();
		if (!_suppressedByLockBroker)
		{
			if (!_uiIsBlockingWorld)
			{
				float num = 0f - raycastInterval;
				bool flag = num == 0f;
				if (!(0f < raycastInterval))
				{
					float num2 = 0f;
				}
				else
				{
					float unscaledTime = Time.unscaledTime;
					float num2 = unscaledTime - _lastRayTime;
					float num3 = num2 - raycastInterval;
					flag = num3 == 0f;
				}
				if (!flag)
				{
					float num2 = Time.unscaledTime;
					_lastRayTime = num2;
					PerformHoverDetection();
					if (_worldIsBlockedByOccluder)
					{
						goto IL_0341;
					}
					if (_activeDrag == null && _currentHover != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
						ICursorDraggable cursorDraggable = default(ICursorDraggable);
						bool flag2 = cursorDraggable != null;
						ICursorDraggable draggable = cursorDraggable;
						if (!flag2)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
							ICursorDraggable cursorDraggable2 = default(ICursorDraggable);
							bool flag3 = cursorDraggable2 == null;
							draggable = cursorDraggable2;
							if (flag3)
							{
								goto IL_01f9;
							}
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						object obj = default(object);
						if (obj != null)
						{
							BeginDrag(draggable);
						}
					}
				}
				goto IL_01f9;
			}
			_worldIsBlockedByOccluder = false;
			if (_currentHover != null)
			{
				_currentHover = null;
				if (emitHoverChangeEvents)
				{
					Action<Interactable> onCursorTargetChanged = this.m_OnCursorTargetChanged;
					if (this.m_OnCursorTargetChanged != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v502 @ rcx_v44 (System.Action`1<Interactable>)+18] (should have been resolved before IL gen)");
					}
				}
			}
			if (_currentPassiveHover != null)
			{
				_currentPassiveHover = null;
				Action<Interactable> onPassiveTargetChanged = this.m_OnPassiveTargetChanged;
				if (this.m_OnPassiveTargetChanged != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v758 @ rcx_v41 (System.Action`1<Interactable>)+18] (should have been resolved before IL gen)");
				}
			}
			if (_uiWantsHoverState && _activeDrag == null)
			{
				if (_currentVisualState != CursorVisualState.Hover)
				{
					_currentVisualState = CursorVisualState.Hover;
					if (emitVisualStateEvents)
					{
						Action<CursorVisualState> onCursorVisualStateChanged = this.m_OnCursorVisualStateChanged;
						if (this.m_OnCursorVisualStateChanged != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1044 @ r9_v4 (System.Action`1<DynamicCursorManager+CursorVisualState>)+18]");
							object obj2 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1044 @ r9_v4 (System.Action`1<DynamicCursorManager+CursorVisualState>)+28]");
							object obj3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1044 @ r9_v4 (System.Action`1<DynamicCursorManager+CursorVisualState>)+40]");
							object obj4 = 0;
							Interactable interactable = (Interactable)1;
							goto IL_08fa;
						}
					}
				}
			}
			else if (_currentVisualState != CursorVisualState.Default)
			{
				_currentVisualState = CursorVisualState.Default;
				if (emitVisualStateEvents)
				{
					Action<CursorVisualState> onCursorVisualStateChanged2 = this.m_OnCursorVisualStateChanged;
					if (this.m_OnCursorVisualStateChanged != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1057 @ rcx_v36 (System.Action`1<DynamicCursorManager+CursorVisualState>)+18]");
						object obj2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1057 @ rcx_v36 (System.Action`1<DynamicCursorManager+CursorVisualState>)+28]");
						object obj3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1057 @ rcx_v36 (System.Action`1<DynamicCursorManager+CursorVisualState>)+40]");
						object obj4 = 0;
						Interactable interactable = null;
						goto IL_08fa;
					}
				}
			}
			goto IL_08d0;
		}
		_worldIsBlockedByOccluder = false;
		if (_currentHover != null)
		{
			_currentHover = null;
			if (emitHoverChangeEvents)
			{
				Action<Interactable> onCursorTargetChanged2 = this.m_OnCursorTargetChanged;
				if (this.m_OnCursorTargetChanged != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v352 @ rcx_v24 (System.Action`1<Interactable>)+18] (should have been resolved before IL gen)");
				}
			}
		}
		if (_currentPassiveHover != null)
		{
			_currentPassiveHover = null;
			Action<Interactable> onPassiveTargetChanged2 = this.m_OnPassiveTargetChanged;
			if (this.m_OnPassiveTargetChanged != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v700 @ rcx_v21 (System.Action`1<Interactable>)+18] (should have been resolved before IL gen)");
			}
		}
		if (_currentVisualState != CursorVisualState.Default)
		{
			_currentVisualState = CursorVisualState.Default;
			if (emitVisualStateEvents)
			{
				Action<CursorVisualState> onCursorVisualStateChanged3 = this.m_OnCursorVisualStateChanged;
				if (this.m_OnCursorVisualStateChanged != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v809 @ rcx_v18 (System.Action`1<DynamicCursorManager+CursorVisualState>)+18] (should have been resolved before IL gen)");
				}
			}
		}
		_clickDownThisFrame = false;
		ClearActiveDrag();
		_capturedDraggableOnPress = null;
		_capturedMapPieceOnPress = null;
		_capturedDraggableItemOnPress = null;
		return;
		IL_08d0:
		_clickDownThisFrame = false;
		if (!sanitizeClickStateEachFrame)
		{
			return;
		}
		goto IL_06a7;
		IL_087e:
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1155 @ rcx_v47 (System.Action`1<DynamicCursorManager+CursorVisualState>)+18] (should have been resolved before IL gen)");
		goto IL_0851;
		IL_0851:
		_clickDownThisFrame = false;
		if (!sanitizeClickStateEachFrame)
		{
			return;
		}
		goto IL_06a7;
		IL_08fa:
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v1161 @ rax_v36 (should have been resolved before IL gen)");
		goto IL_08d0;
		IL_01f9:
		if (!_worldIsBlockedByOccluder)
		{
			if (_activeDrag != null && persistGrabDuringDrag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj5 = default(object);
				if (obj5 != null)
				{
					if (_currentVisualState != CursorVisualState.Grab)
					{
						_currentVisualState = CursorVisualState.Grab;
						if (emitVisualStateEvents)
						{
							Action<CursorVisualState> onCursorVisualStateChanged4 = this.m_OnCursorVisualStateChanged;
							if (this.m_OnCursorVisualStateChanged != null)
							{
								bool flag4 = true;
								goto IL_087e;
							}
						}
					}
					goto IL_0851;
				}
			}
			HandleGrabInput();
			if (sanitizeClickStateEachFrame)
			{
				SanitizeAggregatedClickState();
			}
			_clickDownThisFrame = false;
			return;
		}
		goto IL_0341;
		IL_06a7:
		SanitizeAggregatedClickState();
		return;
		IL_0341:
		if (_currentHover != null)
		{
			_currentHover = null;
			if (emitHoverChangeEvents)
			{
				Action<Interactable> onCursorTargetChanged3 = this.m_OnCursorTargetChanged;
				if (this.m_OnCursorTargetChanged != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v849 @ rcx_v56 (System.Action`1<Interactable>)+18] (should have been resolved before IL gen)");
				}
			}
		}
		if (_currentVisualState != CursorVisualState.Default)
		{
			_currentVisualState = CursorVisualState.Default;
			if (emitVisualStateEvents)
			{
				Action<CursorVisualState> onCursorVisualStateChanged4 = this.m_OnCursorVisualStateChanged;
				if (this.m_OnCursorVisualStateChanged != null)
				{
					bool flag4 = false;
					goto IL_087e;
				}
			}
		}
		goto IL_0851;
	}

	private void OnDisable()
	{
		if (primaryClickActions != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<InputActionReference>.Enumerator enumerator = default(List<InputActionReference>.Enumerator);
			UnityEngine.Object obj = default(UnityEngine.Object);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (!(obj != null))
				{
					continue;
				}
				if ((object)obj != null)
				{
					InputAction action = ((InputActionReference)obj).action;
					if (action == null)
					{
						continue;
					}
					InputAction action2 = ((InputActionReference)obj).action;
					if (_startedHandlers != null)
					{
						if (_startedHandlers.TryGetValue(action2, out var value))
						{
							InputAction action3 = ((InputActionReference)obj).action;
							if (action3 == null)
							{
								throw new NullReferenceException();
							}
							action3.started -= value;
						}
						InputAction action4 = ((InputActionReference)obj).action;
						if (_canceledHandlers != null)
						{
							if (_canceledHandlers.TryGetValue(action4, out var value2))
							{
								InputAction action5 = ((InputActionReference)obj).action;
								if (action5 == null)
								{
									throw new NullReferenceException();
								}
								action5.canceled -= value2;
							}
							continue;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			if (_startedHandlers != null)
			{
				_startedHandlers.Clear();
				if (_canceledHandlers != null)
				{
					_canceledHandlers.Clear();
					ForceReleaseIfPressed("OnDisable");
					ClearActiveDrag();
					_capturedDraggableOnPress = null;
					_capturedMapPieceOnPress = null;
					_capturedDraggableItemOnPress = null;
					if (restoreSystemCursorOnDisable)
					{
						Cursor.visible = true;
						Cursor.lockState = CursorLockMode.None;
					}
					return;
				}
			}
		}
		throw new NullReferenceException();
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A93C]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (!hasFocus)
		{
			ForceReleaseIfPressed("OnApplicationFocus(false)");
		}
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A93D]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (pauseStatus)
		{
			ForceReleaseIfPressed("OnApplicationPause(true)");
		}
	}

	public void SetSuppressedByLockBroker(bool suppressed, bool forceRefresh = true)
	{
		if (_suppressedByLockBroker == suppressed)
		{
			return;
		}
		Action<bool> onSuppressedByLockBrokerChanged = this.m_OnSuppressedByLockBrokerChanged;
		_suppressedByLockBroker = suppressed;
		if (this.m_OnSuppressedByLockBrokerChanged != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v45 @ rcx_v3 (System.Action`1<System.Boolean>)+18] (should have been resolved before IL gen)");
			bool flag = suppressed;
		}
		if (!_suppressedByLockBroker)
		{
			if (forceRefresh)
			{
				ForceRefresh(forceBroadcast: true);
			}
			return;
		}
		if (_currentHover != null)
		{
			_currentHover = null;
			if (emitHoverChangeEvents)
			{
				Action<Interactable> onCursorTargetChanged = this.m_OnCursorTargetChanged;
				if (this.m_OnCursorTargetChanged != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v194 @ rcx_v23 (System.Action`1<Interactable>)+18] (should have been resolved before IL gen)");
				}
			}
		}
		if (_currentPassiveHover != null)
		{
			_currentPassiveHover = null;
			Action<Interactable> onPassiveTargetChanged = this.m_OnPassiveTargetChanged;
			if (this.m_OnPassiveTargetChanged != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v264 @ rcx_v20 (System.Action`1<Interactable>)+18] (should have been resolved before IL gen)");
			}
		}
		ForceReleaseIfPressed("SetSuppressedByLockBroker(true)");
		ClearActiveDrag();
		_capturedDraggableOnPress = null;
		_capturedMapPieceOnPress = null;
		_capturedDraggableItemOnPress = null;
		_currentVisualState = CursorVisualState.Default;
		if (emitVisualStateEvents)
		{
			Action<CursorVisualState> onCursorVisualStateChanged = this.m_OnCursorVisualStateChanged;
			if (this.m_OnCursorVisualStateChanged != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v307 @ rcx_v17 (System.Action`1<DynamicCursorManager+CursorVisualState>)+18] (should have been resolved before IL gen)");
			}
		}
		_clickDownThisFrame = false;
	}

	private unsafe void PerformHoverDetection()
	{
		//IL_0008: Expected O, but got Ref
		//IL_006b: Expected O, but got I4
		//IL_0821: Expected O, but got Ref
		//IL_004e: Expected O, but got I4
		//IL_007a: Invalid comparison between F4 and O
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Expected I4, but got Unknown
		//IL_0116: Expected O, but got Ref
		//IL_00d5: Expected O, but got Ref
		//IL_00d5: Expected O, but got Ref
		//IL_00d5: Expected O, but got Ref
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Expected O, but got Unknown
		//IL_01d8: Expected O, but got I4
		//IL_01eb: Expected O, but got I4
		//IL_0201: Expected F4, but got I
		//IL_021e: Expected O, but got I
		//IL_07c8: Expected O, but got I
		//IL_07d8: Expected O, but got I
		//IL_07e1: Expected O, but got I4
		//IL_0946: Unknown result type (might be due to invalid IL or missing references)
		//IL_094b: Expected O, but got Unknown
		//IL_0979: Unknown result type (might be due to invalid IL or missing references)
		//IL_097e: Expected I4, but got Unknown
		//IL_09b6: Expected O, but got I4
		//IL_09be: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c3: Expected O, but got Unknown
		//IL_0691: Expected O, but got I
		//IL_069f: Expected O, but got Ref
		//IL_06b4: Expected O, but got I
		//IL_0920: Unknown result type (might be due to invalid IL or missing references)
		//IL_0925: Expected O, but got Unknown
		//IL_092e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0933: Expected O, but got Unknown
		//IL_039b: Expected O, but got Ref
		//IL_03b5: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		Camera camera;
		object obj3;
		if (_currentMode != PresentationMode.FPSLocked)
		{
			bool flag = virtualCursor != null;
			camera = raycastCamera;
			if (flag)
			{
				obj3 = 0;
				goto IL_0814;
			}
		}
		else
		{
			camera = raycastCamera;
		}
		int width = Screen.width;
		int height = Screen.height;
		obj3 = 0;
		goto IL_0814;
		IL_0814:
		object obj4 = default(object);
		Ray ray = camera.ScreenPointToRay((Vector3)(&obj4));
		bool flag2 = !debugDrawRay;
		object obj6 = default(object);
		object obj5 = obj6;
		Vector3 vector = default(Vector3);
		if (!flag2)
		{
			float num = raycastInterval;
			float duration;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
			{
				duration = raycastInterval;
			}
			else
			{
				float deltaTime = Time.deltaTime;
				duration = deltaTime;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm1,8\"");
			object obj7 = default(object);
			Debug.DrawRay((Vector3)(&obj7), (Vector3)(&obj4), (Color)(&vector), duration);
			obj5 = obj6;
		}
		_worldIsBlockedByOccluder = false;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb esi,esi\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
		object obj9 = default(object);
		object obj10 = default(object);
		object obj8 = obj9 | obj10;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
		object obj11 = default(object);
		QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)(obj11 + 2);
		int layerMask = default(int);
		RaycastHit[] array = Physics.RaycastAll((Ray)(&vector), maxRayDistance, layerMask, queryTriggerInteraction);
		bool flag3 = array == null;
		UnityEngine.Object obj12 = null;
		UnityEngine.Object obj13 = null;
		float num2 = maxRayDistance;
		if (!flag3)
		{
			if (array.Length > 1)
			{
				Comparison<RaycastHit> comparison = _003C_003Ec._003C_003E9__76_0;
				if (_003C_003Ec._003C_003E9__76_0 == null)
				{
					comparison = (_003C_003Ec._003C_003E9__76_0 = delegate(RaycastHit a, RaycastHit b)
					{
						float distance = ((RaycastHit*)a)->distance;
						float distance2 = ((RaycastHit*)b)->distance;
						float num7 = default(float);
						return num7.CompareTo(distance2);
					});
					queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
				}
				Array.Sort(array, comparison);
			}
			bool flag4 = array.Length == 0;
			obj12 = null;
			obj13 = null;
			num2 = maxRayDistance;
			if (!flag4)
			{
				object obj14 = array + 32;
				obj13 = null;
				object obj15 = 0;
				num2 = maxRayDistance;
				object obj16 = 0;
				RaycastHit raycastHit = default(RaycastHit);
				object obj18 = default(object);
				while (true)
				{
					bool flag5 = (nint)obj16 >= array.Length;
					obj12 = null;
					if (flag5)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v195 @ rsi_v6+10]");
					num2 = 0f;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v195 @ rsi_v6+10]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v195 @ rsi_v6+20]");
					obj5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v195 @ rsi_v6+20]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v195 @ rsi_v6+28]");
					_ = 0;
					if (ignoreTriggerColliders)
					{
						Collider collider = raycastHit.collider;
						if (collider != null)
						{
							Collider collider2 = raycastHit.collider;
							if (collider2.isTrigger)
							{
								goto IL_0917;
							}
						}
					}
					Collider collider3 = raycastHit.collider;
					int num3;
					if (collider3 != null)
					{
						Collider collider4 = raycastHit.collider;
						GameObject gameObject = collider4.gameObject;
						int layer = gameObject.layer;
						num3 = layer;
					}
					else
					{
						num3 = 0;
					}
					object obj17 = this + 92;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
					int num4 = num3 & 0x1F;
					int num5 = 1 << num4;
					int num6 = num5 & obj18;
					bool flag6 = num6 == 0;
					bool flag7 = num6 < 0;
					bool flag8 = !flag7;
					object obj19 = !flag6;
					object obj20 = flag8 & obj19;
					if (obj20 == null)
					{
						Collider collider5 = raycastHit.collider;
						UnityEngine.Object obj22;
						if (collider5 != null)
						{
							Collider collider6 = raycastHit.collider;
							object obj21 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 64));
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+40]");
							obj22 = (UnityEngine.Object)0;
						}
						else
						{
							obj22 = null;
						}
						if (obj22 != null && ((Behaviour)obj22).isActiveAndEnabled)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v276 @ rbx_v21 (UnityEngine.Object)+40]");
							if ((nint)0 != 0)
							{
								Collider collider7 = raycastHit.collider;
								if (((Interactable)obj22).MatchesHitCollider(collider7))
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v276 @ rbx_v21 (UnityEngine.Object)+41]");
									if ((nint)0 == 0)
									{
										obj12 = obj22;
										break;
									}
									if (obj13 == null)
									{
										obj13 = obj22;
									}
								}
							}
						}
						goto IL_0917;
					}
					_worldIsBlockedByOccluder = true;
					obj12 = null;
					obj13 = null;
					break;
					IL_0917:
					obj15++;
					obj14 += 44;
					obj16 = obj15;
				}
			}
		}
		if (!_worldIsBlockedByOccluder)
		{
			if (obj12 != _currentHover)
			{
				_currentHover = (Interactable)obj12;
				if (emitHoverChangeEvents)
				{
					Action<Interactable> onCursorTargetChanged = this.m_OnCursorTargetChanged;
					if (this.m_OnCursorTargetChanged != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v985 @ rcx_v41 (System.Action`1<Interactable>)+18] (should have been resolved before IL gen)");
					}
				}
			}
			if (obj13 != _currentPassiveHover)
			{
				_currentPassiveHover = (Interactable)obj13;
				Action<Interactable> onPassiveTargetChanged = this.m_OnPassiveTargetChanged;
				if (this.m_OnPassiveTargetChanged != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1217 @ rcx_v38 (System.Action`1<Interactable>)+18] (should have been resolved before IL gen)");
				}
			}
			if (_activeDrag != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj23 = default(object);
				if (obj23 != null)
				{
					return;
				}
			}
			bool flag9 = _currentHover != null;
			if (_currentVisualState == (flag9 ? CursorVisualState.Hover : CursorVisualState.Default))
			{
				return;
			}
			bool flag10 = !emitVisualStateEvents;
			_currentVisualState = (flag9 ? CursorVisualState.Hover : CursorVisualState.Default);
			if (flag10)
			{
				return;
			}
			Action<Interactable> onCursorVisualStateChanged = (Action<Interactable>)(object)this.m_OnCursorVisualStateChanged;
			if (this.m_OnCursorVisualStateChanged == null)
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1387 @ rcx_v15 (System.Action`1<Interactable>)+28]");
			object obj24 = 0;
			object obj25 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 64));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1387 @ rcx_v15 (System.Action`1<Interactable>)+18]");
			object obj26 = 0;
		}
		else
		{
			if (_currentHover != null)
			{
				_currentHover = null;
				if (emitHoverChangeEvents)
				{
					Action<Interactable> onCursorTargetChanged2 = this.m_OnCursorTargetChanged;
					if (this.m_OnCursorTargetChanged != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1040 @ rcx_v25 (System.Action`1<Interactable>)+18] (should have been resolved before IL gen)");
					}
				}
			}
			if (!(_currentPassiveHover != null))
			{
				return;
			}
			_currentPassiveHover = null;
			Action<Interactable> onCursorVisualStateChanged = this.m_OnPassiveTargetChanged;
			if (this.m_OnPassiveTargetChanged == null)
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1387 @ rcx_v15 (System.Action`1<Interactable>)+18]");
			object obj26 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1387 @ rcx_v15 (System.Action`1<Interactable>)+28]");
			object obj24 = 0;
			object obj25 = 0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v1385 @ rax_v19 (should have been resolved before IL gen)");
	}

	private unsafe Ray BuildRay()
	{
		//IL_00ff: Expected O, but got Ref
		//IL_0167: Expected native int or pointer, but got O
		//IL_0144: Expected O, but got Ref
		object obj = default(object);
		Camera camera;
		Vector3 pos;
		if (_currentMode != PresentationMode.FPSLocked)
		{
			object obj2 = default(object);
			if (virtualCursor == null)
			{
				int width = Screen.width;
				int height = Screen.height;
				if ((object)raycastCamera == null)
				{
					goto IL_0104;
				}
				obj = obj2;
			}
			else
			{
				if ((object)virtualCursor == null || (object)raycastCamera == null)
				{
					goto IL_0104;
				}
				obj = obj2;
			}
			camera = raycastCamera;
			pos = (Vector3)(&obj);
		}
		else
		{
			int width2 = Screen.width;
			int height2 = Screen.height;
			if ((object)raycastCamera == null)
			{
				goto IL_0104;
			}
			camera = raycastCamera;
			pos = (Vector3)(&obj);
		}
		Ray ray = default(Ray);
		((Ray*)(nint)ray)->m_Origin = camera.ScreenPointToRay(pos).m_Origin;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v231 @ rax_v3 (UnityEngine.Ray)+10]");
		_ = 0;
		return ray;
		IL_0104:
		return (Ray)new NullReferenceException();
	}

	private Vector2 GetActivePointerScreenPosition()
	{
		Vector2 result = default(Vector2);
		if (_currentMode != PresentationMode.FPSLocked && virtualCursor != null)
		{
			if ((object)virtualCursor != null)
			{
				return result;
			}
			return (Vector2)new NullReferenceException();
		}
		int width = Screen.width;
		int height = Screen.height;
		return result;
	}

	private void HandleGrabInput()
	{
		//IL_010d: Expected I, but got O
		//IL_0115: Expected I, but got O
		//IL_0125: Expected O, but got I
		//IL_0265: Expected I, but got O
		//IL_026d: Expected I, but got O
		//IL_027d: Expected O, but got I
		//IL_01a5: Expected O, but got I4
		//IL_039d: Expected I, but got O
		//IL_03a5: Expected I, but got O
		//IL_03b5: Expected O, but got I
		//IL_0161: Expected O, but got I
		//IL_02b9: Expected O, but got I
		//IL_03f1: Expected O, but got I
		//IL_0197: Expected O, but got I4
		//IL_0476: Expected O, but got I
		//IL_04d5: Expected O, but got I
		if (!_clickDownThisFrame || !(_currentHover != null) || _activeDrag != null || _capturedDraggableOnPress != null)
		{
			goto IL_050a;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		ICursorDraggable cursorDraggable = default(ICursorDraggable);
		bool flag = cursorDraggable != null;
		ICursorDraggable cursorDraggable2 = cursorDraggable;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
			ICursorDraggable cursorDraggable3 = default(ICursorDraggable);
			bool flag2 = cursorDraggable3 == null;
			cursorDraggable2 = cursorDraggable3;
			if (flag2)
			{
				goto IL_050a;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		object obj = default(object);
		if ((nint)obj > 0)
		{
			goto IL_022e;
		}
		nint num = (nint)typeof(Component);
		nint num2 = (nint)cursorDraggable2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ rdx_v34 (Il2CppClass<UnityEngine.Component>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v414 @ r8_v23 (Il2CppClass<ICursorDraggable>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ rdx_v34 (Il2CppClass<UnityEngine.Component>)+130]");
		object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v414 @ r8_v23 (Il2CppClass<ICursorDraggable>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v447 @ rax_v61+FFFFFFF8+v415 @ rax_v49*8]");
			if (0 == (nint)typeof(Component))
			{
				obj4 = 1;
				goto IL_05a0;
			}
		}
		obj4 = 0;
		goto IL_05a0;
		IL_050a:
		if (!_clickPressed && _activeDrag != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj5 = default(object);
			if (obj5 == null)
			{
				EndDragSequence();
			}
		}
		return;
		IL_05c2:
		BeginDrag(cursorDraggable2);
		goto IL_050a;
		IL_05a0:
		bool flag3 = obj4 == null;
		ICursorDraggable cursorDraggable4 = null;
		if (!flag3)
		{
			cursorDraggable4 = cursorDraggable2;
		}
		if ((UnityEngine.Object)cursorDraggable4 != null)
		{
			GameObject gameObject = ((Component)cursorDraggable4).gameObject;
			GameObject gameObject2 = _currentHover.gameObject;
			if (gameObject == gameObject2)
			{
				goto IL_022e;
			}
		}
		goto IL_050a;
		IL_022e:
		_capturedDraggableOnPress = cursorDraggable2;
		if (routeMapPieceDragsThroughManager)
		{
			nint num4 = (nint)typeof(MapPiece3D);
			nint num5 = (nint)cursorDraggable2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v509 @ rdx_v27 (Il2CppClass<MapPiece3D>)+130]");
			object obj6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v510 @ r8_v19 (Il2CppClass<ICursorDraggable>)+130]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v509 @ rdx_v27 (Il2CppClass<MapPiece3D>)+130]");
			if (num6 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v510 @ r8_v19 (Il2CppClass<ICursorDraggable>)+C8]");
				object obj7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v528 @ rax_v40+FFFFFFF8+v511 @ rax_v39*8]");
				if (0 == (nint)typeof(MapPiece3D))
				{
					_capturedMapPieceOnPress = (MapPiece3D)cursorDraggable2;
					if (autoAssignVirtualCursorToMapPieces && virtualCursor != null)
					{
						_ = virtualCursor;
					}
					Vector2 activePointerScreenPosition = GetActivePointerScreenPosition();
					((MapPiece3D)cursorDraggable2).BeginDragFromManager(raycastCamera, activePointerScreenPosition);
					goto IL_05c2;
				}
			}
		}
		if (routeDraggableItemsThroughManager)
		{
			nint num7 = (nint)typeof(DraggableItem);
			nint num8 = (nint)cursorDraggable2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v574 @ rdx_v15 (Il2CppClass<DraggableItem>)+130]");
			object obj8 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v575 @ r8_v13 (Il2CppClass<ICursorDraggable>)+130]");
			nint num9 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v574 @ rdx_v15 (Il2CppClass<DraggableItem>)+130]");
			if (num9 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v575 @ r8_v13 (Il2CppClass<ICursorDraggable>)+C8]");
				object obj9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v603 @ rax_v19+FFFFFFF8+v576 @ rax_v18*8]");
				if (0 == (nint)typeof(DraggableItem))
				{
					_capturedDraggableItemOnPress = (DraggableItem)cursorDraggable2;
					Vector2 activePointerScreenPosition2 = GetActivePointerScreenPosition();
					if (((Behaviour)cursorDraggable2).isActiveAndEnabled)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rsi_v5 (ICursorDraggable)+128]");
						if ((nint)0 != 0)
						{
							ICursorDraggable cursorDraggable5 = cursorDraggable2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rsi_v5 (ICursorDraggable)+128]");
							((MonoBehaviour)cursorDraggable5).StopCoroutine((Coroutine)0);
							_ = 0;
						}
						bool flag4 = raycastCamera == null;
						Camera camera = raycastCamera;
						if (flag4)
						{
							Camera main = Camera.main;
							camera = main;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rsi_v5 (ICursorDraggable)+E8]");
						if ((bool)(UnityEngine.Object)0)
						{
							_ = 1;
							((DraggableItem)cursorDraggable2).StartDragInternal(activePointerScreenPosition2);
						}
					}
				}
			}
		}
		goto IL_05c2;
	}

	private unsafe void BeginDrag(ICursorDraggable draggable)
	{
		ClearActiveDrag();
		_activeDrag = draggable;
		Action action = OnDragStarted;
		action._002Ector(this, (nint)__ldftn(DynamicCursorManager.OnDragStarted));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
		Action action2 = OnDragEnded;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		object obj = default(object);
		if (obj == null || _currentVisualState == CursorVisualState.Grab)
		{
			return;
		}
		bool flag = !emitVisualStateEvents;
		_currentVisualState = CursorVisualState.Grab;
		if (!flag)
		{
			Action<CursorVisualState> onCursorVisualStateChanged = this.m_OnCursorVisualStateChanged;
			if (this.m_OnCursorVisualStateChanged != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v185 @ rcx_v13 (System.Action`1<DynamicCursorManager+CursorVisualState>)+18] (should have been resolved before IL gen)");
			}
		}
	}

	private void OnDragStarted()
	{
		if (_currentVisualState == CursorVisualState.Grab)
		{
			return;
		}
		bool flag = !emitVisualStateEvents;
		_currentVisualState = CursorVisualState.Grab;
		if (!flag)
		{
			Action<CursorVisualState> onCursorVisualStateChanged = this.m_OnCursorVisualStateChanged;
			if (this.m_OnCursorVisualStateChanged != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v35 @ rcx_v2 (System.Action`1<DynamicCursorManager+CursorVisualState>)+18] (should have been resolved before IL gen)");
			}
		}
	}

	private void OnDragEnded()
	{
		if (persistGrabDuringDrag)
		{
			EndDragSequence();
		}
	}

	private void EndDragSequence()
	{
		ClearActiveDrag();
		bool flag;
		if (forceDefaultAfterGrabEnd)
		{
			flag = false;
		}
		else
		{
			bool flag2 = _currentHover != null;
			bool flag3 = !flag2;
			flag = !flag3;
		}
		if (_currentVisualState == (flag ? CursorVisualState.Hover : CursorVisualState.Default))
		{
			return;
		}
		bool flag4 = !emitVisualStateEvents;
		_currentVisualState = (flag ? CursorVisualState.Hover : CursorVisualState.Default);
		if (!flag4)
		{
			Action<CursorVisualState> onCursorVisualStateChanged = this.m_OnCursorVisualStateChanged;
			if (this.m_OnCursorVisualStateChanged != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v116 @ r9_v2 (System.Action`1<DynamicCursorManager+CursorVisualState>)+18] (should have been resolved before IL gen)");
			}
		}
	}

	private void ClearActiveDrag()
	{
		if (_activeDrag != null)
		{
			Action action = OnDragStarted;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
			Action action2 = OnDragEnded;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
			_activeDrag = null;
		}
	}

	private void ClearCapturedPressDrag()
	{
		_capturedDraggableOnPress = null;
		_capturedMapPieceOnPress = null;
		_capturedDraggableItemOnPress = null;
	}

	private void OnAnyClickStarted()
	{
		if (logClickAggregation)
		{
			Debug.Log("[DCM] ClickStarted callback (edge resolution deferred to Update).");
		}
	}

	private void OnAnyClickCanceled()
	{
		if (logClickAggregation)
		{
			Debug.Log("[DCM] ClickCanceled callback (edge resolution deferred to Update).");
		}
	}

	private void ResolvePressStateFromActions()
	{
		if (!_suppressedByLockBroker)
		{
			bool flag = (_clickPressed = IsAnyActionPressed());
			if (flag)
			{
				if (!_clickWasPressedLastFrame)
				{
					_pressSourceForBroadcast = _currentHover;
					_clickDownThisFrame = true;
					if (logClickAggregation)
					{
						Debug.Log("[DCM] Rising edge -> ClickDown.");
					}
					if (emitPrimaryClickEvents)
					{
						Action<Interactable> onPrimaryClickDown = this.m_OnPrimaryClickDown;
						if (this.m_OnPrimaryClickDown != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v190 @ rcx_v29 (System.Action`1<Interactable>)+18] (should have been resolved before IL gen)");
						}
					}
				}
			}
			else if (~(_clickWasPressedLastFrame ? 1u : 0u) == 0)
			{
				if (logClickAggregation)
				{
					Debug.Log("[DCM] Falling edge -> ClickUp.");
				}
				_pressSourceForBroadcast = null;
				if (emitPrimaryClickEvents)
				{
					Action<Interactable> onPrimaryClickUp = this.m_OnPrimaryClickUp;
					if (this.m_OnPrimaryClickUp != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v328 @ rcx_v23 (System.Action`1<Interactable>)+18] (should have been resolved before IL gen)");
					}
				}
				if (routeMapPieceDragsThroughManager && _capturedMapPieceOnPress != null)
				{
					_capturedMapPieceOnPress.EndDragFromManager();
				}
				if (routeDraggableItemsThroughManager && _capturedDraggableItemOnPress != null)
				{
					DraggableItem capturedDraggableItemOnPress = _capturedDraggableItemOnPress;
					capturedDraggableItemOnPress._externallyControlled = false;
					if (capturedDraggableItemOnPress.IsBeingDragged)
					{
						capturedDraggableItemOnPress.EndDragInternal();
					}
				}
				_capturedDraggableOnPress = null;
				_capturedMapPieceOnPress = null;
				_capturedDraggableItemOnPress = null;
				if (_activeDrag != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					object obj = default(object);
					if (obj != null)
					{
						EndDragSequence();
					}
				}
			}
			_clickWasPressedLastFrame = flag;
		}
		else
		{
			if (_clickPressed)
			{
				ForceReleaseIfPressed("ResolvePressState(Suppressed)");
			}
			_clickWasPressedLastFrame = false;
		}
	}

	private bool IsAnyActionPressed()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<InputActionReference>.Enumerator enumerator = default(List<InputActionReference>.Enumerator);
		InputActionReference inputActionReference = default(InputActionReference);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			if ((object)inputActionReference != null)
			{
				InputAction action = inputActionReference.action;
				if (action != null && action.enabled && action.IsPressed())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
					return true;
				}
			}
		}
		enumerator.Dispose();
		return false;
	}

	private void SanitizeAggregatedClickState()
	{
		if (!_suppressedByLockBroker)
		{
			if (!_clickPressed)
			{
				return;
			}
			bool flag = IsAnyActionPressed();
			if (!flag)
			{
				if (logClickAggregation != flag)
				{
					Debug.Log("[DCM] Sanitizer: synthesizing release (stale press detected).");
				}
				ForceReleaseIfPressed("SanitizeAggregatedClickState(StalePress)");
				_clickWasPressedLastFrame = false;
			}
		}
		else
		{
			ForceReleaseIfPressed("SanitizeAggregatedClickState(Suppressed)");
		}
	}

	private void ForceReleaseIfPressed(string reason)
	{
		if (!_clickPressed)
		{
			return;
		}
		if (logClickAggregation)
		{
			string message = "[DCM] ForceReleaseIfPressed: " + reason + ".";
			Debug.Log(message);
		}
		_clickPressed = false;
		_pressSourceForBroadcast = null;
		_clickWasPressedLastFrame = false;
		if (emitPrimaryClickEvents)
		{
			Action<Interactable> onPrimaryClickUp = this.m_OnPrimaryClickUp;
			if (this.m_OnPrimaryClickUp != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v189 @ rcx_v21 (System.Action`1<Interactable>)+18] (should have been resolved before IL gen)");
			}
		}
		if (routeMapPieceDragsThroughManager && _capturedMapPieceOnPress != null)
		{
			_capturedMapPieceOnPress.EndDragFromManager();
		}
		if (routeDraggableItemsThroughManager && _capturedDraggableItemOnPress != null)
		{
			DraggableItem capturedDraggableItemOnPress = _capturedDraggableItemOnPress;
			capturedDraggableItemOnPress._externallyControlled = false;
			if (capturedDraggableItemOnPress.IsBeingDragged)
			{
				capturedDraggableItemOnPress.EndDragInternal();
			}
		}
		_capturedDraggableOnPress = null;
		_capturedMapPieceOnPress = null;
		_capturedDraggableItemOnPress = null;
		if (_activeDrag != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj = default(object);
			if (obj != null)
			{
				EndDragSequence();
			}
		}
	}

	private void TryAdoptExternalActiveDrag()
	{
		if (_worldIsBlockedByOccluder || _activeDrag != null || !(_currentHover != null))
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		ICursorDraggable cursorDraggable = default(ICursorDraggable);
		bool flag = cursorDraggable != null;
		ICursorDraggable draggable = cursorDraggable;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
			ICursorDraggable cursorDraggable2 = default(ICursorDraggable);
			bool flag2 = cursorDraggable2 == null;
			draggable = cursorDraggable2;
			if (flag2)
			{
				return;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		object obj = default(object);
		if (obj != null)
		{
			BeginDrag(draggable);
		}
	}

	public void NotifyExternalDragStarted(ICursorDraggable draggable)
	{
		if (draggable == null || _suppressedByLockBroker)
		{
			return;
		}
		if (_activeDrag != draggable)
		{
			BeginDrag(draggable);
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		object obj = default(object);
		if (obj == null || _worldIsBlockedByOccluder || _currentVisualState == CursorVisualState.Grab)
		{
			return;
		}
		bool flag = !emitVisualStateEvents;
		_currentVisualState = CursorVisualState.Grab;
		if (!flag)
		{
			Action<CursorVisualState> onCursorVisualStateChanged = this.m_OnCursorVisualStateChanged;
			if (this.m_OnCursorVisualStateChanged != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v66 @ rcx_v4 (System.Action`1<DynamicCursorManager+CursorVisualState>)+18] (should have been resolved before IL gen)");
			}
		}
	}

	public void NotifyExternalDragEnded(ICursorDraggable draggable)
	{
		if (_activeDrag == draggable)
		{
			EndDragSequence();
		}
	}

	private void SetVisualState(CursorVisualState newState, bool forceBroadcast = false)
	{
		if (_currentVisualState == newState && !forceBroadcast)
		{
			return;
		}
		bool flag = !emitVisualStateEvents;
		_currentVisualState = newState;
		if (!flag)
		{
			Action<CursorVisualState> onCursorVisualStateChanged = this.m_OnCursorVisualStateChanged;
			if (this.m_OnCursorVisualStateChanged != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v56 @ rcx_v2 (System.Action`1<DynamicCursorManager+CursorVisualState>)+18] (should have been resolved before IL gen)");
			}
		}
	}

	public void ForceBroadcastVisualState()
	{
		if (emitVisualStateEvents)
		{
			Action<CursorVisualState> onCursorVisualStateChanged = this.m_OnCursorVisualStateChanged;
			if (this.m_OnCursorVisualStateChanged != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v11 @ r9_v2 (System.Action`1<DynamicCursorManager+CursorVisualState>)+18] (should have been resolved before IL gen)");
			}
		}
	}

	private void ApplyModeSettings(bool initializing)
	{
		bool flag = (Cursor.visible = !hideSystemCursor);
		CursorLockMode lockState;
		if (_currentMode != PresentationMode.FPSLocked)
		{
			if (hideSystemCursor)
			{
				lockState = CursorLockMode.None;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb ecx,ecx\"");
				lockState = (CursorLockMode)((flag ? 1 : 0) & 2);
			}
		}
		else
		{
			bool flag3 = !lockCursorInFPSMode;
			bool flag4 = !flag3;
			lockState = (flag4 ? CursorLockMode.Locked : CursorLockMode.None);
		}
		Cursor.lockState = lockState;
		if (!initializing && broadcastStateOnModeSwitch != initializing && emitVisualStateEvents != initializing)
		{
			Action<CursorVisualState> onCursorVisualStateChanged = this.m_OnCursorVisualStateChanged;
			if (this.m_OnCursorVisualStateChanged != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v104 @ rcx_v4 (System.Action`1<DynamicCursorManager+CursorVisualState>)+18] (should have been resolved before IL gen)");
			}
		}
	}

	private void ApplySystemCursorSettings()
	{
		bool flag = (Cursor.visible = !hideSystemCursor);
		if (_currentMode != PresentationMode.FPSLocked)
		{
			if (hideSystemCursor)
			{
				Cursor.lockState = CursorLockMode.None;
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb ecx,ecx\"");
			CursorLockMode lockState = (CursorLockMode)((flag ? 1 : 0) & 2);
			Cursor.lockState = lockState;
		}
		else
		{
			bool flag3 = !lockCursorInFPSMode;
			bool lockState2 = !flag3;
			Cursor.lockState = (lockState2 ? CursorLockMode.Locked : CursorLockMode.None);
		}
	}

	public void SwitchToPresentationMode(PresentationMode mode)
	{
		if (_currentMode == mode)
		{
			return;
		}
		bool flag = !hideSystemCursor;
		_currentMode = mode;
		Cursor.visible = flag;
		CursorLockMode lockState;
		if (_currentMode != PresentationMode.FPSLocked)
		{
			if (hideSystemCursor)
			{
				lockState = CursorLockMode.None;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb ecx,ecx\"");
				lockState = (CursorLockMode)((flag ? 1 : 0) & 2);
			}
		}
		else
		{
			bool flag2 = !lockCursorInFPSMode;
			bool flag3 = !flag2;
			lockState = (flag3 ? CursorLockMode.Locked : CursorLockMode.None);
		}
		Cursor.lockState = lockState;
		if (broadcastStateOnModeSwitch && emitVisualStateEvents)
		{
			Action<CursorVisualState> onCursorVisualStateChanged = this.m_OnCursorVisualStateChanged;
			if (this.m_OnCursorVisualStateChanged != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v43 @ rcx_v4 (System.Action`1<DynamicCursorManager+CursorVisualState>)+18] (should have been resolved before IL gen)");
			}
		}
	}

	public void SwitchToFPSLocked()
	{
		SwitchToPresentationMode(PresentationMode.FPSLocked);
	}

	public void SwitchToFreeMouse()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 2 Invalid \"Jump target not found in method: 0x18051FBA0\"");
	}

	public void SetUIBlocking(bool shouldBlock, bool wantsHoverState)
	{
		_uiIsBlockingWorld = shouldBlock;
		_uiWantsHoverState = wantsHoverState;
	}

	public void ForceRefresh(bool forceBroadcast = false)
	{
		//IL_0429: Expected O, but got I4
		bool flag2;
		bool flag3;
		if (!_suppressedByLockBroker)
		{
			if (_uiIsBlockingWorld)
			{
				if (_currentHover != null)
				{
					_currentHover = null;
					if (emitHoverChangeEvents)
					{
						Action<Interactable> onCursorTargetChanged = this.m_OnCursorTargetChanged;
						if (this.m_OnCursorTargetChanged != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v288 @ rcx_v17 (System.Action`1<Interactable>)+18] (should have been resolved before IL gen)");
						}
					}
				}
				bool flag = !_uiWantsHoverState;
				flag2 = !flag;
				if (_currentVisualState != (flag2 ? CursorVisualState.Hover : CursorVisualState.Default) || forceBroadcast)
				{
					flag3 = !emitVisualStateEvents;
					goto IL_047a;
				}
				return;
			}
			PerformHoverDetection();
			if (!_worldIsBlockedByOccluder)
			{
				if (_activeDrag != null && persistGrabDuringDrag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					object obj = default(object);
					if (obj != null)
					{
						if (_currentVisualState != CursorVisualState.Grab || forceBroadcast)
						{
							bool flag4 = !emitVisualStateEvents;
							_currentVisualState = CursorVisualState.Grab;
							if (!flag4)
							{
								Action<CursorVisualState> onCursorVisualStateChanged = this.m_OnCursorVisualStateChanged;
								if (this.m_OnCursorVisualStateChanged != null)
								{
									bool flag5 = true;
									goto IL_040b;
								}
								return;
							}
							return;
						}
						return;
					}
				}
				bool flag6 = _currentHover != null;
				if (_currentVisualState != (flag6 ? CursorVisualState.Hover : CursorVisualState.Default) || forceBroadcast)
				{
					bool flag7 = !emitVisualStateEvents;
					_currentVisualState = (flag6 ? CursorVisualState.Hover : CursorVisualState.Default);
					if (!flag7)
					{
						Action<CursorVisualState> onCursorVisualStateChanged = this.m_OnCursorVisualStateChanged;
						if (this.m_OnCursorVisualStateChanged != null)
						{
							bool flag5 = flag6;
							goto IL_040b;
						}
						return;
					}
					return;
				}
				return;
			}
			if (_currentVisualState == CursorVisualState.Default && !forceBroadcast)
			{
				return;
			}
			flag2 = false;
		}
		else
		{
			bool flag8 = _currentHover != null;
			bool flag9 = !flag8;
			flag2 = false;
			if (!flag9)
			{
				_currentHover = null;
				bool flag10 = !emitHoverChangeEvents;
				flag2 = false;
				if (!flag10)
				{
					Action<Interactable> onCursorTargetChanged2 = this.m_OnCursorTargetChanged;
					bool flag11 = this.m_OnCursorTargetChanged == null;
					flag2 = false;
					if (!flag11)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v164 @ rcx_v11 (System.Action`1<Interactable>)+18] (should have been resolved before IL gen)");
						flag2 = false;
					}
				}
			}
		}
		object obj2 = (emitVisualStateEvents ? 1 : 0) - (flag2 ? 1 : 0);
		flag3 = obj2 == null;
		goto IL_047a;
		IL_040b:
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v545 @ rcx_v5 (System.Action`1<DynamicCursorManager+CursorVisualState>)+18] (should have been resolved before IL gen)");
		return;
		IL_047a:
		_currentVisualState = (flag2 ? CursorVisualState.Hover : CursorVisualState.Default);
		if (!flag3)
		{
			Action<CursorVisualState> onCursorVisualStateChanged = this.m_OnCursorVisualStateChanged;
			if (this.m_OnCursorVisualStateChanged != null)
			{
				bool flag5 = flag2;
				goto IL_040b;
			}
		}
	}

	public bool IsCurrentDeviceGamepad()
	{
		//IL_006c: Expected I4, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A951]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if ((object)_playerInput != null)
		{
			string currentControlScheme = _playerInput.currentControlScheme;
			return currentControlScheme == "Gamepad";
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public unsafe void ClampCursorToValve(Vector3 position, float distance, bool isClampingMouse, bool angleConstraint = false, bool resetToDefault = false, float minAngle = 0f, float maxAngle = 0f)
	{
		//IL_001f: Expected O, but got Ref
		//IL_0037: Expected O, but got F4
		//IL_004b: Expected O, but got Ref
		//IL_0063: Expected O, but got F4
		_003CIsClampedToValve_003Ek__BackingField = true;
		float num = default(float);
		_003CValveScreenPosition_003Ek__BackingField = (Vector2)raycastCamera.WorldToScreenPoint((Vector3)(&num)).x;
		_003CValveDefaultScreenPosition_003Ek__BackingField = (Vector2)raycastCamera.WorldToScreenPoint((Vector3)(&num)).x;
		bool flag = default(bool);
		_003CIsAngleConstrained_003Ek__BackingField = flag;
		_003CCursorDistanceMultiplierFromCenter_003Ek__BackingField = distance;
		_003CIsClampingMouse_003Ek__BackingField = isClampingMouse;
		float num2 = default(float);
		_003CMinAngle_003Ek__BackingField = num2;
		float num3 = default(float);
		_003CMaxAngle_003Ek__BackingField = num3;
		bool flag2 = default(bool);
		_003CResetToDefault_003Ek__BackingField = flag2;
	}

	public void DisableValveClamping()
	{
		//IL_001e: Expected I, but got O
		//IL_0059: Expected I, but got O
		_003CIsClampedToValve_003Ek__BackingField = false;
		nint num = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rax_v3 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
		_ = 0;
		_003CValveScreenPosition_003Ek__BackingField = Vector2.zeroVector;
		nint num3 = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rax_v6 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num4 = 0;
		_003CValveDefaultScreenPosition_003Ek__BackingField = Vector2.zeroVector;
		_003CCursorDistanceMultiplierFromCenter_003Ek__BackingField = 0f;
		_003CIsAngleConstrained_003Ek__BackingField = false;
		_003CMinAngle_003Ek__BackingField = 0f;
		_003CIsClampingMouse_003Ek__BackingField = false;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rax_v7 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
		_ = 0;
	}

	public DynamicCursorManager()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
		LayerMask layerMask = default(LayerMask);
		interactableLayers = layerMask;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
		LayerMask layerMask2 = default(LayerMask);
		cursorBlockerLayers = layerMask2;
		hideSystemCursor = true;
		persistGrabDuringDrag = true;
		emitHoverChangeEvents = true;
		emitPrimaryClickEvents = true;
		List<InputActionReference> list = new List<InputActionReference>();
		list._002Ector();
		primaryClickActions = list;
		enableActionsOnEnable = true;
		routeMapPieceDragsThroughManager = true;
		routeDraggableItemsThroughManager = true;
		Dictionary<InputAction, Action<InputAction.CallbackContext>> startedHandlers = new Dictionary<InputAction, Action<InputAction.CallbackContext>>();
		_startedHandlers = startedHandlers;
		Dictionary<InputAction, Action<InputAction.CallbackContext>> canceledHandlers = new Dictionary<InputAction, Action<InputAction.CallbackContext>>();
		_canceledHandlers = canceledHandlers;
		base._002Ector();
	}

	private void _003COnEnable_003Eb__68_0(InputAction.CallbackContext ctx)
	{
		if (logClickAggregation)
		{
			Debug.Log("[DCM] ClickStarted callback (edge resolution deferred to Update).");
		}
	}

	private void _003COnEnable_003Eb__68_1(InputAction.CallbackContext ctx)
	{
		if (logClickAggregation)
		{
			Debug.Log("[DCM] ClickCanceled callback (edge resolution deferred to Update).");
		}
	}
}
