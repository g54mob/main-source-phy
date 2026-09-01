using UnityEngine;

public class SandboxInput
{
	public delegate void AddDeltaDelegate(float delta);

	public static bool m_ForceIgnoreGrid;

	private static bool m_ignoreNextSelection;

	private static float m_ContinuousHoldTime;

	private static float m_NextTickTime;

	private static Vector2 m_StartPanScreenPos;

	private static float m_StartPanTime;

	public static void Reset()
	{
		m_ignoreNextSelection = false;
	}

	public static void ForceIgnoreNextSelection()
	{
		m_ignoreNextSelection = true;
	}

	public static void UpdateManual()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInput())
		{
			ProcessInput();
			GameStateCommonInput.Process();
			UpdateContinuousHold();
		}
	}

	public static void AddDeltaContinuous(float delta, AddDeltaDelegate addDeltaDelegate)
	{
		m_ContinuousHoldTime += Time.unscaledDeltaTime;
		m_NextTickTime += Time.unscaledDeltaTime;
		if (m_ContinuousHoldTime > 0.3f && m_NextTickTime > 0.05f)
		{
			addDeltaDelegate(delta);
			m_NextTickTime = Mathf.Min(m_NextTickTime - 0.05f, 0.05f);
		}
	}

	public static void UpdateContinuousHold()
	{
		if (GameInput.GetMouseButtonJustReleased(0))
		{
			m_ContinuousHoldTime = 0f;
			m_NextTickTime = 0f;
		}
	}

	private static void ProcessInput()
	{
		if (GameStateCommonInput.IgnoreKeyboardInput())
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			GameStateCommonInput.ProcessEscapeKeypress();
		}
		if (GameUI.m_Instance.m_PauseMenu.gameObject.activeInHierarchy)
		{
			return;
		}
		m_ForceIgnoreGrid = (GameInput.IsDown(BindingType.MOVE_OFF_GRID) ? true : false);
		if (GameInput.IsDown(BindingType.MULTI_SELECT) && ActivePanels.m_Panels.Count == 0 && ClipboardManager.IsEmpty())
		{
			GameUI.SetPointerMode(PointerMode.SELECT_TOGGLE);
		}
		else if (GameUI.m_Instance.m_SandboxEditCustomShapeTools.DeleteSubModeActive())
		{
			GameUI.SetPointerMode(PointerMode.ERASE);
		}
		else
		{
			GameUI.SetPointerMode(PointerMode.SELECT);
		}
		if (GameInput.JustPressed(BindingType.DELETE_SELECTION) && !GameUI.m_Instance.m_SandboxEditCustomShapeTools.gameObject.activeInHierarchy)
		{
			ProcessDelete();
		}
		if (GameInput.GetMouseButtonJustReleased(0) && !EditingRampSpline() && !EditingCustomShape())
		{
			ProcessLeftClickUp(GameInput.GetMousePosition());
		}
		if (GameInput.GetMouseButtonJustPressed(0))
		{
			ProcessLeftClickDown(GameInput.GetMousePosition());
		}
		if (GameInput.GetMouseButtonJustPressed(1))
		{
			ProcessRightClickDown(GameInput.GetMousePosition());
		}
		if (GameInput.GetMouseButtonJustReleased(1))
		{
			ProcessRightClickUp(GameInput.GetMousePosition());
		}
		if (GameInput.GetMouseButtonIsDown(0))
		{
			SandboxSelectionSet.MaybeMoveSelectionWithPointer();
		}
		if (GameInput.JustPressed(BindingType.START_SIM))
		{
			GameUI.m_Instance.m_TopBar.OnSim();
		}
		if (GameInput.JustPressed(BindingType.SANDBOX_BUILD_SIM_CYCLE))
		{
			GameUI.m_Instance.m_TopBar.m_ModeToggle.OnButton();
		}
		if (!GameInput.IsDown(BindingType.HORIZONTAL_CONSTRAINT_UNIVERSAL) && !GameInput.IsDown(BindingType.VERTICAL_CONSTRAINT_UNIVERSAL))
		{
			SandboxSelectionSet.SetMovementConstraint(MovementConstraint.NONE);
		}
		if (GameInput.IsDown(BindingType.HORIZONTAL_CONSTRAINT_UNIVERSAL))
		{
			SandboxSelectionSet.SetMovementConstraint(MovementConstraint.X_AXIS);
		}
		else if (GameInput.IsDown(BindingType.VERTICAL_CONSTRAINT_UNIVERSAL))
		{
			SandboxSelectionSet.SetMovementConstraint(MovementConstraint.Y_AXIS);
		}
		if (!BridgeSelectionSet.IsEmpty() && SandboxSelectionSet.IsEmpty())
		{
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.WEST))
			{
				GameUI.m_Instance.m_Selection.OnLockSoft();
			}
			else if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH))
			{
				GameUI.m_Instance.m_Selection.OnLock();
			}
			else if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_UP))
			{
				GameUI.m_Instance.m_Selection.OnUnLock();
			}
		}
	}

	private static void ProcessLeftClickUp(Vector2 screenPos)
	{
		if (SandboxSelectionSet.SelectionFollowsMouse())
		{
			SandboxSelectionSet.FinalizeMovement();
			BridgeEffects.PlayErrorEffectAtFirstIllegalNodePosition();
		}
		if ((bool)SandboxItems.m_NewUnPlacedItem)
		{
			SandboxItems.PlaceNewItem(SandboxItems.m_NewUnPlacedItem);
			SandboxItems.m_NewUnPlacedItem = null;
		}
		else if (!GameUI.IsPointerOverGameObject())
		{
			if (!m_ignoreNextSelection && !SandboxSelectionSet.SelectionFollowsMouse())
			{
				ProcessSelectAction(GameInput.GetMousePosition());
				Bridge.ProcessSelectAction();
			}
			if (!m_ignoreNextSelection && !SandboxItems.GetItemUnderPos(GameInput.GetMousePosition()) && GameUI.GetPointerMode() != PointerMode.SELECT_TOGGLE)
			{
				SandboxSelectionSet.CancelSelection();
				BridgeSelectionSet.CancelSelection();
			}
		}
		SandboxSelectionSet.CancelSelectionFollowingMouse();
		Sandbox.m_AllowedToPanCamera = false;
		m_ignoreNextSelection = false;
	}

	private static void ProcessLeftClickDown(Vector2 screenPos)
	{
		SandboxItem itemUnderPos = SandboxItems.GetItemUnderPos(GameInput.GetMousePosition());
		if ((bool)itemUnderPos && itemUnderPos.IsLocked())
		{
			Sandbox.m_AllowedToPanCamera = true;
			return;
		}
		if ((bool)itemUnderPos && !SandboxSelectionSet.SelectionFollowsMouse() && !GameUI.IsPointerOverGameObject() && !EditingRampSpline() && !EditingCustomShape())
		{
			if (!SandboxSelectionSet.m_Items.Contains(itemUnderPos))
			{
				ProcessSelectAction(GameInput.GetMousePosition());
				m_ignoreNextSelection = true;
			}
			else
			{
				GameUI.m_Instance.m_SandboxMenu.MaybeActivateEditSubmenu();
			}
		}
		if ((bool)itemUnderPos && SandboxSelectionSet.m_Items.Contains(itemUnderPos) && GameUI.GetPointerMode() != PointerMode.SELECT_TOGGLE && !GameUI.IsPointerOverGameObject() && !EditingRampSpline() && !EditingCustomShape())
		{
			SandboxSelectionSet.StartMoving(itemUnderPos);
		}
		else
		{
			Sandbox.m_AllowedToPanCamera = itemUnderPos == null || !GameUI.m_Instance.m_SandboxMenu.m_PointerEvents.m_IsHovering;
		}
		if (GameUI.m_Instance.m_SandboxMenu.m_PointerEvents.m_IsHovering)
		{
			m_ignoreNextSelection = true;
		}
	}

	private static bool AllowedToGroupSelect()
	{
		if (GameUI.IsPointerOverGameObject())
		{
			return false;
		}
		if (GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (SandboxSelectionSet.SelectionFollowsMouse())
		{
			return false;
		}
		if (GameUI.m_Instance.m_SandboxEditBuildZone.gameObject.activeInHierarchy && GameUI.m_Instance.m_SandboxEditBuildZone.IsEditing())
		{
			return false;
		}
		return true;
	}

	private static void ProcessRightClickDown(Vector2 screenPos)
	{
		m_StartPanScreenPos = GameInput.GetMousePosition();
		m_StartPanTime = Time.realtimeSinceStartup;
		if (SandboxSelectionSet.SelectionFollowsMouse())
		{
			SandboxSelectionSet.FinalizeMovement();
		}
		if (!GameUI.IsPointerOverGameObject() && !GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy && !SandboxSelectionSet.SelectionFollowsMouse() && GameToolMode.GetMode() != GameToolModeType.ERASE)
		{
			GroupSelect.Start(screenPos);
		}
		if (GameUI.GetPointerMode() != PointerMode.SELECT_TOGGLE)
		{
			SandboxItem itemUnderPos = SandboxItems.GetItemUnderPos(screenPos);
			if (!itemUnderPos || !SandboxSelectionSet.m_Items.Contains(itemUnderPos))
			{
				SandboxSelectionSet.CancelSelection();
				BridgeSelectionSet.CancelSelection();
			}
		}
	}

	private static void ProcessRightClickUp(Vector2 screenPos)
	{
		if (GroupSelect.IsActive())
		{
			if (GameUI.GetPointerMode() != PointerMode.SELECT_TOGGLE)
			{
				SandboxSelectionSet.CancelSelection();
				BridgeSelectionSet.CancelSelection();
			}
			SandboxSelectionSet.SelectAllInRect(GroupSelect.GetRect(), GameInput.MultiSelectIsDown());
			if (!SandboxSelectionSet.SelectionSetMatchesStoredGuids())
			{
				SandboxUndo.SnapShot();
			}
			BridgeSelectionSet.SelectAllInRect(GroupSelect.GetRect(), GameInput.MultiSelectIsDown());
			if (!BridgeSelectionSet.IsEmpty() || !SandboxSelectionSet.IsEmpty())
			{
				InterfaceAudio.Play("ui_build_select");
			}
			BridgeSelectionSet.RemoveAnchorsFromSelectionSet();
		}
		else if (!m_ignoreNextSelection && !SandboxSelectionSet.SelectionFollowsMouse() && !GameUI.IsPointerOverGameObject())
		{
			ProcessSelectAction(GameInput.GetMousePosition());
			Bridge.ProcessSelectAction();
		}
		m_ignoreNextSelection = false;
		GroupSelect.Cancel();
	}

	private static void ProcessSelectAction(Vector3 screenPos)
	{
		SandboxItems.TrySelectItem(screenPos);
	}

	private static bool EditingRampSpline()
	{
		return GameUI.m_Instance.m_SandboxEditRamp.IsEditingSplinePoints();
	}

	private static bool EditingCustomShape()
	{
		return GameUI.m_Instance.m_SandboxEditCustomShapeTools.gameObject.activeInHierarchy;
	}

	private static void ProcessDelete()
	{
		if (GameUI.m_Instance.m_SandboxEditRamp.IsEditingSplinePoints())
		{
			GameUI.m_Instance.m_SandboxEditRamp.ProcessDelete();
		}
		else if (!SandboxSelectionSet.IsEmpty())
		{
			SandboxSelectionSet.Delete();
			InterfaceAudio.Play("ui_build_delete");
		}
		else
		{
			InterfaceAudio.PlayErrorBeep();
		}
	}
}
