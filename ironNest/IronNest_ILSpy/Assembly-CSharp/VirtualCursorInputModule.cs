using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class VirtualCursorInputModule : BaseInputModule
{
	private sealed class _003CDelayedSelectedChange_003Ed__34 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public VirtualCursorInputModule _003C_003E4__this;

		public GameObject selectable;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CDelayedSelectedChange_003Ed__34(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_00fa: Expected I4, but got I8
			//IL_0187: Expected I4, but got O
			//IL_0015: Expected O, but got I4
			//IL_00c1: Expected I4, but got I8
			//IL_0052: Expected I4, but got I8
			VirtualCursorInputModule virtualCursorInputModule = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					if ((nint)obj == 1)
					{
						_003C_003E1__state = -1;
						if ((object)_003C_003E4__this == null || (object)((BaseInputModule)virtualCursorInputModule).m_EventSystem == null)
						{
							goto IL_0179;
						}
						((BaseInputModule)virtualCursorInputModule).m_EventSystem.SetSelectedGameObject(selectable);
					}
					return false;
				}
				_003C_003E1__state = -1;
				WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
				_003C_003E2__current = waitForEndOfFrame;
				_003C_003E1__state = 2;
				return true;
			}
			_003C_003E1__state = -1;
			if ((object)_003C_003E4__this != null && (object)((BaseInputModule)virtualCursorInputModule).m_EventSystem != null)
			{
				((BaseInputModule)virtualCursorInputModule).m_EventSystem.SetSelectedGameObject(null);
				WaitForEndOfFrame waitForEndOfFrame2 = new WaitForEndOfFrame();
				_003C_003E2__current = waitForEndOfFrame2;
				_003C_003E1__state = 1;
				return true;
			}
			goto IL_0179;
			IL_0179:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
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

	private VirtualCursor virtualCursor;

	private DynamicCursorManager cursorManager;

	private List<InputActionReference> primaryClickActions;

	private InputActionReference scrollAction;

	private bool enableActionsOnEnable;

	private bool enableUIInFPSLockedMode;

	private bool enableUIInFreeMouseMode;

	private bool blockWorldRaycastsWhenOverUI;

	private bool setCursorHoverStateForUI;

	private int maxHierarchySearchDepth;

	private bool enableDragEvents;

	private bool useDragThreshold;

	private bool logUIEvents;

	private bool logClickAggregation;

	private bool logRaycastDetails;

	private PointerEventData _pointerEventData;

	private GameObject _currentPointerTarget;

	private GameObject _currentRaycastHit;

	private GameObject _currentPointerPress;

	private bool _isOverInteractableUI;

	private Vector2 _lastPointerPosition;

	private bool _draggingThisPress;

	private bool _clickPressed;

	private bool _wasClickPressed;

	private readonly Dictionary<InputAction, Action<InputAction.CallbackContext>> _startedHandlers;

	private readonly Dictionary<InputAction, Action<InputAction.CallbackContext>> _canceledHandlers;

	protected override void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		if (!ValidateSetup())
		{
			base.enabled = false;
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if (ValidateSetup())
		{
			SubscribeToClickActions();
		}
		else
		{
			base.enabled = false;
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		UnsubscribeFromClickActions();
		ForceReleaseIfPressed("OnDisable");
		ClearPointerState(sendExit: true);
		if (cursorManager != null)
		{
			DynamicCursorManager dynamicCursorManager = cursorManager;
			dynamicCursorManager._uiIsBlockingWorld = false;
		}
	}

	public override void Process()
	{
		//IL_0194: Expected O, but got I
		//IL_0317: Expected O, but got I
		//IL_0553: Expected O, but got I4
		if (cursorManager != null)
		{
			DynamicCursorManager dynamicCursorManager = cursorManager;
			bool flag = dynamicCursorManager._currentMode == DynamicCursorManager.PresentationMode.FPSLocked;
			bool flag2 = !flag;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v78 (System.Boolean)+79+this @ rcx (VirtualCursorInputModule)]");
			if ((nint)0 == 0)
			{
				ClearPointerState(sendExit: true);
				if (cursorManager != null)
				{
					DynamicCursorManager dynamicCursorManager2 = cursorManager;
					dynamicCursorManager2._uiIsBlockingWorld = false;
				}
				goto IL_05e1;
			}
		}
		bool clickPressed = IsAnyActionPressed();
		_clickPressed = clickPressed;
		if (_pointerEventData == null)
		{
			PointerEventData pointerEventData = new PointerEventData(base.m_EventSystem);
			_pointerEventData = pointerEventData;
			VirtualCursor virtualCursor = this.virtualCursor;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v209 @ rax_v75 (VirtualCursor)+70]");
			_ = 0;
			_lastPointerPosition = virtualCursor._position;
		}
		VirtualCursor virtualCursor2 = this.virtualCursor;
		PointerEventData pointerEventData2 = _pointerEventData;
		Vector2 vector = virtualCursor2._position - _lastPointerPosition;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ rax_v12 (VirtualCursor)+70]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VirtualCursorInputModule)+B8]");
		object obj = num - 0;
		pointerEventData2._003Cdelta_003Ek__BackingField = vector;
		PointerEventData pointerEventData3 = _pointerEventData;
		pointerEventData3._003Cposition_003Ek__BackingField = virtualCursor2._position;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ rax_v12 (VirtualCursor)+70]");
		_ = 0;
		_lastPointerPosition = virtualCursor2._position;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ rax_v12 (VirtualCursor)+70]");
		_ = 0;
		base.m_EventSystem.RaycastAll(_pointerEventData, m_RaycastResultCache);
		PointerEventData pointerEventData4 = _pointerEventData;
		RaycastResult raycastResult = BaseInputModule.FindFirstRaycast(m_RaycastResultCache);
		pointerEventData4._003CpointerCurrentRaycast_003Ek__BackingField = (RaycastResult)raycastResult.m_GameObject;
		_ = raycastResult.distance;
		_ = raycastResult.sortingGroupOrder;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v212 @ rax_v16 (UnityEngine.EventSystems.RaycastResult)+30]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v212 @ rax_v16 (UnityEngine.EventSystems.RaycastResult)+40]");
		_ = 0;
		_ = raycastResult.screenPosition;
		_ = raycastResult.document;
		List<RaycastResult> raycastResultCache = m_RaycastResultCache;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v176 @ rbx_v7 (System.Collections.Generic.List`1<UnityEngine.EventSystems.RaycastResult>)+1C]");
		_ = (nint)0 + (nint)1;
		if (!RuntimeHelpers.IsReferenceOrContainsReferences<RaycastResult>())
		{
			_ = 0;
		}
		else
		{
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v176 @ rbx_v7 (System.Collections.Generic.List`1<UnityEngine.EventSystems.RaycastResult>)+18]");
			if ((nint)0 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v176 @ rbx_v7 (System.Collections.Generic.List`1<UnityEngine.EventSystems.RaycastResult>)+10]");
				nint num2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v176 @ rbx_v7 (System.Collections.Generic.List`1<UnityEngine.EventSystems.RaycastResult>)+18]");
				Array.Clear((Array)num2, 0, 0);
			}
		}
		ProcessHover();
		bool flag4;
		if (!_wasClickPressed)
		{
			bool flag3 = (byte)(~(_clickPressed ? 1u : 0u)) != 0;
			flag4 = false;
			if (!flag3)
			{
				HandlePress();
				flag4 = false;
			}
		}
		else
		{
			bool flag5 = !_clickPressed;
			flag4 = flag5;
		}
		if (enableDragEvents)
		{
			HandleDragWhileHeld();
		}
		if (flag4)
		{
			HandleRelease();
		}
		InputAction action = scrollAction.action;
		if (!action.enabled)
		{
			InputAction action2 = scrollAction.action;
			action2.Enable();
		}
		PointerEventData pointerEventData5 = _pointerEventData;
		InputAction action3 = scrollAction.action;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807015E0");
		Vector2 vector2 = default(Vector2);
		pointerEventData5._003CscrollDelta_003Ek__BackingField = vector2;
		if (_pointerEventData.IsScrolling())
		{
			GameObject eventHandler = ExecuteEvents.GetEventHandler<IScrollHandler>(_currentPointerTarget);
			GameObject gameObject = ExecuteEvents.ExecuteHierarchy(eventHandler, _pointerEventData, ExecuteEvents.s_ScrollHandler);
		}
		if (cursorManager != null)
		{
			bool flag6 = !blockWorldRaycastsWhenOverUI;
			bool flag7 = false;
			if (!flag6)
			{
				flag7 = _isOverInteractableUI;
			}
			bool flag8 = !flag7;
			bool flag9 = !flag8;
			bool flag10 = !setCursorHoverStateForUI;
			bool flag11 = false;
			if (!flag10)
			{
				flag11 = _isOverInteractableUI;
			}
			DynamicCursorManager dynamicCursorManager3 = cursorManager;
			bool flag12 = !flag11;
			dynamicCursorManager3._uiIsBlockingWorld = flag9;
			bool uiWantsHoverState = !flag12;
			dynamicCursorManager3._uiWantsHoverState = uiWantsHoverState;
			object obj2 = logUIEvents & flag9;
			if (obj2 != null && _currentPointerTarget != null)
			{
				string text = _currentPointerTarget.name;
				string message = "[VirtualCursorInputModule] Blocking 3D raycasts (UI hovered: " + text + ")";
				Debug.Log(message);
			}
		}
		goto IL_05e1;
		IL_05e1:
		_wasClickPressed = _clickPressed;
	}

	private bool ValidateSetup()
	{
		//IL_00f3: Expected I4, but got O
		if (virtualCursor != null)
		{
			if (cursorManager == null)
			{
				Debug.LogWarning("[VirtualCursorInputModule] DynamicCursorManager is not assigned. UI will work, but world blocking/cursor-state updates will be skipped.", this);
			}
			List<InputActionReference> list = primaryClickActions;
			if (primaryClickActions != null)
			{
				if (list._size == 0)
				{
					Debug.LogWarning("[VirtualCursorInputModule] No primaryClickActions assigned. UI clicks/drags will not work.", this);
				}
				return true;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		Debug.LogError("[VirtualCursorInputModule] VirtualCursor reference is required. Assign it in the inspector.", this);
		return false;
	}

	private bool IsUIInteractionEnabled()
	{
		//IL_0090: Expected I4, but got O
		if (cursorManager != null)
		{
			DynamicCursorManager dynamicCursorManager = cursorManager;
			if ((object)cursorManager != null)
			{
				if (dynamicCursorManager._currentMode != DynamicCursorManager.PresentationMode.FPSLocked)
				{
					return enableUIInFreeMouseMode;
				}
				return enableUIInFPSLockedMode;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return true;
	}

	public void SetSelectedEventSystemObject(GameObject selectable)
	{
		DynamicCursorManager dynamicCursorManager = cursorManager;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A951]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		string currentControlScheme = dynamicCursorManager._playerInput.currentControlScheme;
		if (currentControlScheme == "Gamepad" && selectable != null)
		{
			_003CDelayedSelectedChange_003Ed__34 obj = new _003CDelayedSelectedChange_003Ed__34(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			obj.selectable = selectable;
			Coroutine coroutine = StartCoroutine(obj);
		}
	}

	public void SetSelectedObjectToNull()
	{
		base.m_EventSystem.SetSelectedGameObject(null);
	}

	private IEnumerator DelayedSelectedChange(GameObject selectable)
	{
		_003CDelayedSelectedChange_003Ed__34 obj = new _003CDelayedSelectedChange_003Ed__34(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.selectable = selectable;
		return obj;
	}

	private void ProcessHover()
	{
		PointerEventData pointerEventData = _pointerEventData;
		if (logRaycastDetails && (UnityEngine.Object)pointerEventData._003CpointerCurrentRaycast_003Ek__BackingField != null)
		{
			string text = ((UnityEngine.Object)pointerEventData._003CpointerCurrentRaycast_003Ek__BackingField).name;
			string message = "[VirtualCursorInputModule] Raw Raycast Hit: " + text;
			Debug.Log(message);
		}
		GameObject gameObject = FindInteractableInHierarchy((GameObject)pointerEventData._003CpointerCurrentRaycast_003Ek__BackingField);
		if (logRaycastDetails && (UnityEngine.Object)pointerEventData._003CpointerCurrentRaycast_003Ek__BackingField != null)
		{
			string message2;
			if (gameObject != null && gameObject != (UnityEngine.Object)pointerEventData._003CpointerCurrentRaycast_003Ek__BackingField)
			{
				string text2 = gameObject.name;
				string text3 = ((UnityEngine.Object)pointerEventData._003CpointerCurrentRaycast_003Ek__BackingField).name;
				message2 = "[VirtualCursorInputModule] Interactable parent: " + text2 + " (hit child: " + text3 + ")";
			}
			else
			{
				if (!(gameObject == null))
				{
					goto IL_0267;
				}
				string text4 = ((UnityEngine.Object)pointerEventData._003CpointerCurrentRaycast_003Ek__BackingField).name;
				message2 = "[VirtualCursorInputModule] No interactable found in hierarchy for: " + text4;
			}
			Debug.Log(message2);
		}
		goto IL_0267;
		IL_0267:
		_currentRaycastHit = (GameObject)pointerEventData._003CpointerCurrentRaycast_003Ek__BackingField;
		if (_currentPointerTarget != gameObject)
		{
			if (_currentPointerTarget != null)
			{
				bool flag = ExecuteEvents.Execute(_currentPointerTarget, _pointerEventData, ExecuteEvents.s_PointerExitHandler);
				if (logUIEvents)
				{
					string text5 = _currentPointerTarget.name;
					string message3 = "[VirtualCursorInputModule] UI Exit: " + text5;
					Debug.Log(message3);
				}
			}
			_currentPointerTarget = gameObject;
			if (_currentPointerTarget != null)
			{
				bool flag2 = ExecuteEvents.Execute(_currentPointerTarget, _pointerEventData, ExecuteEvents.s_PointerEnterHandler);
				if (logUIEvents)
				{
					string text6 = _currentPointerTarget.name;
					string message4 = "[VirtualCursorInputModule] UI Enter: " + text6;
					Debug.Log(message4);
				}
			}
		}
		bool isOverInteractableUI = _currentPointerTarget != null;
		_isOverInteractableUI = isOverInteractableUI;
	}

	private GameObject FindInteractableInHierarchy(GameObject start)
	{
		//IL_0070: Expected O, but got I4
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Expected O, but got Unknown
		if (start != null)
		{
			if ((object)start == null)
			{
				goto IL_0207;
			}
			Transform transform = start.transform;
			bool flag = maxHierarchySearchDepth <= 0;
			Transform transform2 = transform;
			object obj = 0;
			if (!flag)
			{
				UnityEngine.Object obj2 = default(UnityEngine.Object);
				object obj3 = default(object);
				object obj4 = default(object);
				object obj5 = default(object);
				object obj6 = default(object);
				while (transform2 != null)
				{
					if ((object)transform2 != null)
					{
						GameObject gameObject = transform2.gameObject;
						if ((object)gameObject != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
							if (obj2 == null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
								if (obj3 == null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
									if (obj4 == null)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
										if (obj5 == null)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
											if (obj6 == null)
											{
												Transform parent = transform2.parent;
												obj++;
												bool flag2 = (nint)obj < maxHierarchySearchDepth;
												transform2 = parent;
												if (!flag2)
												{
													break;
												}
												continue;
											}
										}
									}
								}
							}
							return gameObject;
						}
					}
					goto IL_0207;
				}
			}
		}
		return null;
		IL_0207:
		return (GameObject)(object)new NullReferenceException();
	}

	private void ClearPointerState(bool sendExit)
	{
		//IL_01a5: Expected O, but got I4
		if (sendExit && _currentPointerTarget != null && _pointerEventData != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805112F0");
			ExecuteEvents.EventFunction<IPointerExitHandler> functor = default(ExecuteEvents.EventFunction<IPointerExitHandler>);
			bool flag = ExecuteEvents.Execute(_currentPointerTarget, _pointerEventData, functor);
		}
		_currentPointerTarget = null;
		_currentRaycastHit = null;
		_isOverInteractableUI = false;
		if (_currentPointerPress != null && _pointerEventData != null)
		{
			bool flag2 = ExecuteEvents.Execute(_currentPointerPress, _pointerEventData, ExecuteEvents.s_PointerUpHandler);
			if (_draggingThisPress)
			{
				PointerEventData pointerEventData = _pointerEventData;
				if (pointerEventData._003CpointerDrag_003Ek__BackingField != null)
				{
					PointerEventData pointerEventData2 = _pointerEventData;
					bool flag3 = ExecuteEvents.Execute<IPointerUpHandler>(null, null, null);
					bool flag4 = ExecuteEvents.Execute(pointerEventData2._003CpointerDrag_003Ek__BackingField, _pointerEventData, (ExecuteEvents.EventFunction<IEndDragHandler>)flag3);
				}
			}
		}
		_currentPointerPress = null;
		_draggingThisPress = false;
		if (_pointerEventData != null)
		{
			_pointerEventData.pointerPress = null;
			PointerEventData pointerEventData3 = _pointerEventData;
			pointerEventData3._003CrawPointerPress_003Ek__BackingField = null;
			PointerEventData pointerEventData4 = _pointerEventData;
			pointerEventData4._003CpointerDrag_003Ek__BackingField = null;
			PointerEventData pointerEventData5 = _pointerEventData;
			pointerEventData5._003Cdragging_003Ek__BackingField = false;
		}
	}

	private void ProcessPressReleaseAndDrag()
	{
		bool flag2;
		if (!_wasClickPressed)
		{
			bool flag = (byte)(~(_clickPressed ? 1u : 0u)) != 0;
			flag2 = false;
			if (!flag)
			{
				HandlePress();
				flag2 = false;
			}
		}
		else
		{
			bool flag3 = !_clickPressed;
			flag2 = flag3;
		}
		if (enableDragEvents)
		{
			HandleDragWhileHeld();
		}
		if (flag2)
		{
			HandleRelease();
		}
	}

	private void HandlePress()
	{
		if (!(_currentPointerTarget != null))
		{
			return;
		}
		_currentPointerPress = _currentPointerTarget;
		_pointerEventData.pointerPress = _currentPointerPress;
		PointerEventData pointerEventData = _pointerEventData;
		pointerEventData._003CrawPointerPress_003Ek__BackingField = _currentPointerPress;
		PointerEventData pointerEventData2 = _pointerEventData;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v243 @ rax_v12 (UnityEngine.EventSystems.PointerEventData)+148]");
		_ = 0;
		pointerEventData2._003CpressPosition_003Ek__BackingField = pointerEventData2._003Cposition_003Ek__BackingField;
		PointerEventData pointerEventData3 = _pointerEventData;
		pointerEventData3._003CpointerPressRaycast_003Ek__BackingField = pointerEventData3._003CpointerCurrentRaycast_003Ek__BackingField;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v244 @ rax_v14 (UnityEngine.EventSystems.PointerEventData)+60]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v244 @ rax_v14 (UnityEngine.EventSystems.PointerEventData)+70]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v244 @ rax_v14 (UnityEngine.EventSystems.PointerEventData)+80]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v244 @ rax_v14 (UnityEngine.EventSystems.PointerEventData)+90]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v244 @ rax_v14 (UnityEngine.EventSystems.PointerEventData)+A0]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v244 @ rax_v14 (UnityEngine.EventSystems.PointerEventData)+B0]");
		_ = 0;
		PointerEventData pointerEventData4 = _pointerEventData;
		float unscaledTime = Time.unscaledTime;
		pointerEventData4._003CclickTime_003Ek__BackingField = unscaledTime;
		PointerEventData pointerEventData5 = _pointerEventData;
		pointerEventData5._003CclickCount_003Ek__BackingField = 1;
		PointerEventData pointerEventData6 = _pointerEventData;
		GameObject eventHandler = ExecuteEvents.GetEventHandler<IDragHandler>(_currentPointerPress);
		pointerEventData6._003CpointerDrag_003Ek__BackingField = eventHandler;
		PointerEventData pointerEventData7 = _pointerEventData;
		pointerEventData7._003Cdragging_003Ek__BackingField = false;
		bool flag = !enableDragEvents;
		_draggingThisPress = false;
		if (!flag)
		{
			PointerEventData pointerEventData8 = _pointerEventData;
			if (pointerEventData8._003CpointerDrag_003Ek__BackingField != null)
			{
				PointerEventData pointerEventData9 = _pointerEventData;
				bool flag2 = ExecuteEvents.Execute(pointerEventData9._003CpointerDrag_003Ek__BackingField, _pointerEventData, ExecuteEvents.s_InitializePotentialDragHandler);
			}
		}
		bool flag3 = ExecuteEvents.Execute(_currentPointerPress, _pointerEventData, ExecuteEvents.s_PointerDownHandler);
		base.m_EventSystem.SetSelectedGameObject(_currentPointerPress);
		if (logUIEvents)
		{
			string[] array = new string[5] { "[VirtualCursorInputModule] UI Press Down: ", null, null, null, null };
			string text = _currentPointerPress.name;
			array[1] = text;
			array[2] = " (pointerDrag=";
			PointerEventData pointerEventData10 = _pointerEventData;
			string text2;
			if (pointerEventData10._003CpointerDrag_003Ek__BackingField != null)
			{
				PointerEventData pointerEventData11 = _pointerEventData;
				text2 = pointerEventData11._003CpointerDrag_003Ek__BackingField.name;
			}
			else
			{
				text2 = "null";
			}
			array[3] = text2;
			array[4] = ")";
			string message = string.Concat(array);
			Debug.Log(message);
		}
	}

	private void HandleDragWhileHeld()
	{
		//IL_00bf: Expected O, but got I
		//IL_02b3: Invalid comparison between O and F4
		//IL_0147: Expected F4, but got I4
		if (!_clickPressed || _pointerEventData == null)
		{
			return;
		}
		PointerEventData pointerEventData = _pointerEventData;
		bool flag = pointerEventData._003CpointerDrag_003Ek__BackingField == null;
		if (flag)
		{
			return;
		}
		if (_draggingThisPress == flag)
		{
			if (useDragThreshold != flag)
			{
				PointerEventData pointerEventData2 = _pointerEventData;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v286 @ rcx_v25 (UnityEngine.EventSystems.PointerEventData)+148]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v286 @ rcx_v25 (UnityEngine.EventSystems.PointerEventData)+158]");
				object obj = num - 0;
				object obj2 = pointerEventData2._003Cposition_003Ek__BackingField - pointerEventData2._003CpressPosition_003Ek__BackingField;
				object obj3 = obj * obj;
				object obj4 = obj2 * obj2;
				object obj5 = obj3 + obj4;
				float num2;
				if (base.m_EventSystem != null)
				{
					EventSystem eventSystem = base.m_EventSystem;
					num2 = eventSystem.m_DragThreshold;
				}
				else
				{
					num2 = 5f;
				}
				float num3 = num2 * num2;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3))
				{
					goto IL_0222;
				}
			}
			PointerEventData pointerEventData3 = _pointerEventData;
			pointerEventData3._003Cdragging_003Ek__BackingField = true;
			PointerEventData pointerEventData4 = _pointerEventData;
			_draggingThisPress = true;
			bool flag2 = ExecuteEvents.Execute(pointerEventData4._003CpointerDrag_003Ek__BackingField, _pointerEventData, ExecuteEvents.s_BeginDragHandler);
			if (logUIEvents)
			{
				PointerEventData pointerEventData5 = _pointerEventData;
				string text = pointerEventData5._003CpointerDrag_003Ek__BackingField.name;
				string message = "[VirtualCursorInputModule] UI BeginDrag: " + text;
				Debug.Log(message);
			}
			goto IL_0222;
		}
		goto IL_0241;
		IL_0241:
		PointerEventData pointerEventData6 = _pointerEventData;
		bool flag3 = ExecuteEvents.Execute(pointerEventData6._003CpointerDrag_003Ek__BackingField, _pointerEventData, ExecuteEvents.s_DragHandler);
		return;
		IL_0222:
		if (_draggingThisPress)
		{
			goto IL_0241;
		}
	}

	private void HandleRelease()
	{
		bool flag = _currentPointerPress == null;
		if (flag)
		{
			return;
		}
		if (enableDragEvents != flag && _draggingThisPress != flag)
		{
			PointerEventData pointerEventData = _pointerEventData;
			if (pointerEventData._003CpointerDrag_003Ek__BackingField != null)
			{
				PointerEventData pointerEventData2 = _pointerEventData;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511160");
				ExecuteEvents.EventFunction<IEndDragHandler> functor = default(ExecuteEvents.EventFunction<IEndDragHandler>);
				bool flag2 = ExecuteEvents.Execute(pointerEventData2._003CpointerDrag_003Ek__BackingField, _pointerEventData, functor);
				if (logUIEvents)
				{
					PointerEventData pointerEventData3 = _pointerEventData;
					string text = pointerEventData3._003CpointerDrag_003Ek__BackingField.name;
					string message = "[VirtualCursorInputModule] UI EndDrag: " + text;
					Debug.Log(message);
				}
			}
		}
		bool flag3 = ExecuteEvents.Execute(_currentPointerPress, _pointerEventData, ExecuteEvents.s_PointerUpHandler);
		if (logUIEvents)
		{
			string text2 = _currentPointerPress.name;
			string message2 = "[VirtualCursorInputModule] UI Press Up: " + text2;
			Debug.Log(message2);
		}
		if (!(_currentPointerPress != _currentPointerTarget))
		{
			goto IL_02ec;
		}
		if (_currentRaycastHit != null && _currentPointerPress != null)
		{
			if (!(_currentRaycastHit != _currentPointerPress))
			{
				goto IL_02ec;
			}
			Transform transform = _currentRaycastHit.transform;
			Transform transform2 = transform;
			while (true)
			{
				Transform parent = transform2.parent;
				if (!(parent != null))
				{
					break;
				}
				GameObject gameObject = parent.gameObject;
				if (!(gameObject == _currentPointerPress))
				{
					transform2 = parent;
					continue;
				}
				goto IL_02ec;
			}
		}
		goto IL_035e;
		IL_035e:
		_currentPointerPress = null;
		_draggingThisPress = false;
		_pointerEventData.pointerPress = null;
		PointerEventData pointerEventData4 = _pointerEventData;
		pointerEventData4._003CrawPointerPress_003Ek__BackingField = null;
		PointerEventData pointerEventData5 = _pointerEventData;
		pointerEventData5._003CpointerDrag_003Ek__BackingField = null;
		PointerEventData pointerEventData6 = _pointerEventData;
		pointerEventData6._003Cdragging_003Ek__BackingField = false;
		return;
		IL_02ec:
		bool flag4 = ExecuteEvents.Execute(_currentPointerPress, _pointerEventData, ExecuteEvents.s_PointerClickHandler);
		if (logUIEvents)
		{
			string text3 = _currentPointerPress.name;
			string message3 = "[VirtualCursorInputModule] UI Click: " + text3;
			Debug.Log(message3);
		}
		goto IL_035e;
	}

	private static bool IsChildOf(GameObject child, GameObject parent)
	{
		//IL_0156: Expected I4, but got O
		if (!(child != null) || !(parent != null))
		{
			goto IL_0142;
		}
		if (!(child != parent))
		{
			goto IL_013c;
		}
		if ((object)child != null)
		{
			Transform transform = child.transform;
			if ((object)transform != null)
			{
				Transform transform2 = transform;
				while (true)
				{
					Transform parent2 = transform2.parent;
					if (!(parent2 != null))
					{
						break;
					}
					if ((object)parent2 != null)
					{
						GameObject gameObject = parent2.gameObject;
						if (!(gameObject != parent))
						{
							goto IL_013c;
						}
						transform2 = parent2;
						continue;
					}
					goto IL_0148;
				}
				goto IL_0142;
			}
		}
		goto IL_0148;
		IL_0142:
		return false;
		IL_0148:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_013c:
		return true;
	}

	private void UpdateManagerState()
	{
		//IL_00ac: Expected O, but got I4
		if (cursorManager != null)
		{
			bool flag = !blockWorldRaycastsWhenOverUI;
			bool flag2 = false;
			if (!flag)
			{
				flag2 = _isOverInteractableUI;
			}
			bool flag3 = !flag2;
			bool flag4 = !flag3;
			bool flag5 = !setCursorHoverStateForUI;
			bool flag6 = false;
			if (!flag5)
			{
				flag6 = _isOverInteractableUI;
			}
			DynamicCursorManager dynamicCursorManager = cursorManager;
			bool flag7 = !flag6;
			dynamicCursorManager._uiIsBlockingWorld = flag4;
			bool uiWantsHoverState = !flag7;
			dynamicCursorManager._uiWantsHoverState = uiWantsHoverState;
			object obj = logUIEvents & flag4;
			if (obj != null && _currentPointerTarget != null)
			{
				string text = _currentPointerTarget.name;
				string message = "[VirtualCursorInputModule] Blocking 3D raycasts (UI hovered: " + text + ")";
				Debug.Log(message);
			}
		}
	}

	private void SubscribeToClickActions()
	{
		InputAction action = scrollAction.action;
		action.Enable();
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
					InputAction action2 = ((InputActionReference)obj).action;
					if (action2 == null)
					{
						continue;
					}
					if (enableActionsOnEnable)
					{
						InputAction action3 = ((InputActionReference)obj).action;
						if (action3 == null)
						{
							break;
						}
						if (!action3.enabled)
						{
							InputAction action4 = ((InputActionReference)obj).action;
							action4.Enable();
						}
					}
					InputAction action5 = ((InputActionReference)obj).action;
					if (_startedHandlers != null)
					{
						if (_startedHandlers.ContainsKey(action5))
						{
							continue;
						}
						Action<InputAction.CallbackContext> value = delegate
						{
							if (logClickAggregation)
							{
								Debug.Log("[VirtualCursorInputModule] ClickStarted callback (edge resolution deferred to Process).");
							}
						};
						Action<InputAction.CallbackContext> value2 = delegate
						{
							if (logClickAggregation)
							{
								Debug.Log("[VirtualCursorInputModule] ClickCanceled callback (edge resolution deferred to Process).");
							}
						};
						InputAction action6 = ((InputActionReference)obj).action;
						if (_startedHandlers != null)
						{
							_startedHandlers.set_Item(action6, value);
							InputAction action7 = ((InputActionReference)obj).action;
							if (_canceledHandlers != null)
							{
								_canceledHandlers.set_Item(action7, value2);
								InputAction action8 = ((InputActionReference)obj).action;
								if (action8 != null)
								{
									action8.started += value;
									InputAction action9 = ((InputActionReference)obj).action;
									if (action9 != null)
									{
										action9.canceled += value2;
										if (logClickAggregation)
										{
											InputAction action10 = ((InputActionReference)obj).action;
											if (action10 == null)
											{
												throw new NullReferenceException();
											}
											string message = "[VirtualCursorInputModule] Subscribed to action: " + action10.m_Name;
											Debug.Log(message);
										}
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
			return;
		}
		throw new NullReferenceException();
	}

	private void UnsubscribeFromClickActions()
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
					return;
				}
			}
		}
		throw new NullReferenceException();
	}

	private void OnAnyClickStarted()
	{
		if (logClickAggregation)
		{
			Debug.Log("[VirtualCursorInputModule] ClickStarted callback (edge resolution deferred to Process).");
		}
	}

	private void OnAnyClickCanceled()
	{
		if (logClickAggregation)
		{
			Debug.Log("[VirtualCursorInputModule] ClickCanceled callback (edge resolution deferred to Process).");
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

	private void ResolvePressState()
	{
		bool clickPressed = IsAnyActionPressed();
		_clickPressed = clickPressed;
	}

	private void ForceReleaseIfPressed(string reason)
	{
		//IL_01a2: Expected O, but got I4
		//IL_0158: Expected O, but got I4
		if (!_clickPressed)
		{
			return;
		}
		if (logClickAggregation)
		{
			string message = "[VirtualCursorInputModule] ForceRelease: " + reason;
			Debug.Log(message);
		}
		_clickPressed = false;
		if (_currentPointerPress != null && _pointerEventData != null)
		{
			bool flag = !enableDragEvents;
			BaseEventData eventData = null;
			ExecuteEvents.EventFunction<IEndDragHandler> functor = null;
			if (!flag)
			{
				bool flag2 = !_draggingThisPress;
				eventData = null;
				functor = null;
				if (!flag2)
				{
					PointerEventData pointerEventData = _pointerEventData;
					bool flag3 = pointerEventData._003CpointerDrag_003Ek__BackingField != null;
					bool flag4 = !flag3;
					eventData = null;
					functor = null;
					if (!flag4)
					{
						PointerEventData pointerEventData2 = _pointerEventData;
						ExecuteEvents.EventFunction<IEndDragHandler> eventFunction = (ExecuteEvents.EventFunction<IEndDragHandler>)ExecuteEvents.Execute<IEndDragHandler>(null, null, null);
						bool flag5 = ExecuteEvents.Execute(pointerEventData2._003CpointerDrag_003Ek__BackingField, _pointerEventData, eventFunction);
						eventData = _pointerEventData;
						functor = eventFunction;
					}
				}
			}
			ExecuteEvents.EventFunction<IPointerUpHandler> functor2 = (ExecuteEvents.EventFunction<IPointerUpHandler>)ExecuteEvents.Execute(null, eventData, functor);
			bool flag6 = ExecuteEvents.Execute(_currentPointerPress, _pointerEventData, functor2);
		}
		_currentPointerPress = null;
		_draggingThisPress = false;
		if (_pointerEventData != null)
		{
			_pointerEventData.pointerPress = null;
			PointerEventData pointerEventData3 = _pointerEventData;
			pointerEventData3._003CrawPointerPress_003Ek__BackingField = null;
			PointerEventData pointerEventData4 = _pointerEventData;
			pointerEventData4._003CpointerDrag_003Ek__BackingField = null;
			PointerEventData pointerEventData5 = _pointerEventData;
			pointerEventData5._003Cdragging_003Ek__BackingField = false;
		}
	}

	private void ProcessScroll()
	{
		InputAction action = scrollAction.action;
		if (!action.enabled)
		{
			InputAction action2 = scrollAction.action;
			action2.Enable();
		}
		PointerEventData pointerEventData = _pointerEventData;
		InputAction action3 = scrollAction.action;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807015E0");
		Vector2 vector = default(Vector2);
		pointerEventData._003CscrollDelta_003Ek__BackingField = vector;
		if (_pointerEventData.IsScrolling())
		{
			GameObject eventHandler = ExecuteEvents.GetEventHandler<IScrollHandler>(_currentPointerTarget);
			GameObject gameObject = ExecuteEvents.ExecuteHierarchy(eventHandler, _pointerEventData, ExecuteEvents.s_ScrollHandler);
		}
	}

	public VirtualCursorInputModule()
	{
		List<InputActionReference> list = new List<InputActionReference>();
		primaryClickActions = list;
		enableActionsOnEnable = true;
		enableUIInFreeMouseMode = true;
		setCursorHoverStateForUI = true;
		maxHierarchySearchDepth = 10;
		enableDragEvents = true;
		_startedHandlers = new Dictionary<InputAction, Action<InputAction.CallbackContext>>();
		_canceledHandlers = new Dictionary<InputAction, Action<InputAction.CallbackContext>>();
		base._002Ector();
	}

	private void _003CSubscribeToClickActions_003Eb__44_0(InputAction.CallbackContext ctx)
	{
		if (logClickAggregation)
		{
			Debug.Log("[VirtualCursorInputModule] ClickStarted callback (edge resolution deferred to Process).");
		}
	}

	private void _003CSubscribeToClickActions_003Eb__44_1(InputAction.CallbackContext ctx)
	{
		if (logClickAggregation)
		{
			Debug.Log("[VirtualCursorInputModule] ClickCanceled callback (edge resolution deferred to Process).");
		}
	}
}
