using UnityEngine;

public class GameToolMode
{
	private static GameToolModeType m_Mode;

	private static bool m_EnteredMoveWithVerticalContraint;

	private static bool m_EnteredMoveWithHorizontalContraint;

	public static void Init()
	{
		m_Mode = GameToolModeType.BUILD;
	}

	public static void UpdateManual()
	{
		if (GameInput.JustPressed(BindingType.SELECT))
		{
			if (GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
			{
				InterfaceAudio.PlayErrorBeep();
			}
			else
			{
				SelectModeActivate(on: true);
			}
		}
		if (GameInput.JustReleased(BindingType.SELECT))
		{
			SelectModeActivate(on: false);
		}
		if (GetMode() != GameToolModeType.MOVE && ClipboardManager.IsEmpty() && (GameInput.JustPressed(BindingType.MOVE) || GameInput.JustPressed(BindingType.VERTICAL_CONSTRAINT_UNIVERSAL) || GameInput.JustPressed(BindingType.HORIZONTAL_CONSTRAINT_UNIVERSAL)))
		{
			if (GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
			{
				InterfaceAudio.PlayErrorBeep();
			}
			else
			{
				if (GameInput.JustPressed(BindingType.VERTICAL_CONSTRAINT_UNIVERSAL))
				{
					m_EnteredMoveWithVerticalContraint = true;
				}
				if (GameInput.JustPressed(BindingType.HORIZONTAL_CONSTRAINT_UNIVERSAL))
				{
					m_EnteredMoveWithHorizontalContraint = true;
				}
				MoveModeActivate(on: true);
			}
		}
		if (GameInput.JustReleased(BindingType.MOVE) || GamepadManager.ButtonJustReleased(GamepadButtonType.NORTH))
		{
			MoveModeActivate(on: false);
		}
		if (GameInput.JustReleased(BindingType.VERTICAL_CONSTRAINT_UNIVERSAL) && m_EnteredMoveWithVerticalContraint)
		{
			MoveModeActivate(on: false);
		}
		if (GameInput.JustReleased(BindingType.HORIZONTAL_CONSTRAINT_UNIVERSAL) && m_EnteredMoveWithHorizontalContraint)
		{
			MoveModeActivate(on: false);
		}
		if (GameInput.JustPressed(BindingType.ERASE))
		{
			if (GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
			{
				InterfaceAudio.PlayErrorBeep();
			}
			else
			{
				EraseModeActivate(on: true);
			}
		}
		if (GameInput.JustReleased(BindingType.ERASE))
		{
			EraseModeActivate(on: false);
		}
	}

	public static void SetMode(GameToolModeType mode)
	{
		if (mode != GameToolModeType.BUILD)
		{
			EnterNonBuildMode(mode);
		}
		m_Mode = mode;
		GameUI.m_Instance.m_BuildToolBar.UpdateGameToolModeIcons(mode);
	}

	public static GameToolModeType GetMode()
	{
		return m_Mode;
	}

	public static void EnterNonBuildMode(GameToolModeType mode)
	{
		GameToolModeType mode2 = GetMode();
		if (mode != mode2)
		{
			switch (mode2)
			{
			case GameToolModeType.SELECT:
				SelectModeActivate(on: false);
				break;
			case GameToolModeType.MOVE:
				MoveModeActivate(on: false);
				break;
			case GameToolModeType.ERASE:
				EraseModeActivate(on: false);
				break;
			default:
				Debug.LogWarning("Unexpeced game tool mode " + mode2);
				break;
			case GameToolModeType.BUILD:
				break;
			}
		}
		BridgeJointPlacement.CancelSelection();
		if (ClipboardManager.ReadyToPaste())
		{
			ClipboardManager.ClearClipboard();
		}
		if (BridgePillarPlacement.InPlacementMode())
		{
			BridgePillarPlacement.CancelPlacementAndSelectPreviousMaterial();
		}
	}

	public static void SelectModeActivate(bool on)
	{
		if ((on && GetMode() == GameToolModeType.SELECT) || (!on && GetMode() != GameToolModeType.SELECT))
		{
			return;
		}
		if (on)
		{
			SetMode(GameToolModeType.SELECT);
			return;
		}
		if (GroupSelect.IsActive())
		{
			BridgeSelectionSet.SelectAllInRect(GroupSelect.GetRect(), GameInput.MultiSelectIsDown());
			if (!BridgeSelectionSet.IsEmpty())
			{
				InterfaceAudio.Play("ui_build_select");
			}
		}
		GroupSelect.Cancel();
		SetMode(GameToolModeType.BUILD);
	}

	public static void MoveModeActivate(bool on)
	{
		if ((on && GetMode() == GameToolModeType.MOVE) || (!on && GetMode() != GameToolModeType.MOVE))
		{
			return;
		}
		if (on)
		{
			SetMode(GameToolModeType.MOVE);
			BridgeSelectionSet.CancelSelection();
			if (BridgeTrace.m_JustFilled)
			{
				BridgeTrace.ClearTraceLine();
				BridgeTrace.TurnOffTracing();
				BridgeTrace.m_JustFilled = false;
			}
			Camera.main.transform.rotation = Quaternion.identity;
		}
		else
		{
			if ((bool)BridgeJointMovement.m_SelectedJoint)
			{
				BridgeJointMovement.FinalizeMovement();
			}
			BridgeJointMovement.CancelSelection();
			SetMode(GameToolModeType.BUILD);
			m_EnteredMoveWithVerticalContraint = false;
			m_EnteredMoveWithHorizontalContraint = false;
		}
	}

	public static void EraseModeActivate(bool on)
	{
		if ((on && GetMode() == GameToolModeType.ERASE) || (!on && GetMode() != GameToolModeType.ERASE))
		{
			return;
		}
		if (on)
		{
			BridgeSelectionSet.CancelSelection();
			Bridge.InitPreviousErasePos(GameInput.GetMousePosition());
			SetMode(GameToolModeType.ERASE);
			if (BridgeTrace.m_JustFilled)
			{
				BridgeTrace.ClearTraceLine();
				BridgeTrace.TurnOffTracing();
				BridgeTrace.m_JustFilled = false;
			}
		}
		else
		{
			BridgeActions.FlushRecording();
			SetMode(GameToolModeType.BUILD);
		}
	}
}
