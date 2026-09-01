using UnityEngine;

public class SandboxUI
{
	public static void Init()
	{
	}

	public static void EnableUI()
	{
		GameUI.m_Instance.m_TopBar.gameObject.SetActive(!GameUI.m_DisableHud);
		GameUI.m_Instance.m_TopBar.m_CostAndBudget.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_TopBar.m_ButtonContainerSpeed.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_ButtonContainerPauseResume.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_LevelInfo.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_TopBar.m_LevelNavButtons.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_ModeToggle.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_TopBar.m_ModeToggle.SetStateImmediate(ToggleSliderState.OFF);
		GameUI.m_Instance.m_TopBar.m_SimButton.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_TopBar.m_ExitSimButton.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_PauseSimButton.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_UnPauseSimButton.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_HelpButton.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_ReplayButton.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_GodModeParent.SetActive(GameManager.GetGameMode() == GameMode.SANDBOX);
		GameUI.m_Instance.m_TopBar.m_GodModeButton.gameObject.SetActive(!Profiles.m_ActiveProfile.m_GodMode);
		GameUI.m_Instance.m_TopBar.m_GodModeSelectedButton.gameObject.SetActive(Profiles.m_ActiveProfile.m_GodMode);
		GameUI.m_Instance.m_TopBar.m_ShowDecorParent.SetActive(GameManager.GetGameMode() == GameMode.SANDBOX);
		GameUI.m_Instance.m_TopBar.m_ShowDecorButton.gameObject.SetActive(!Profiles.m_ActiveProfile.m_ShowDecor);
		GameUI.m_Instance.m_TopBar.m_ShowDecorSelectedButton.gameObject.SetActive(Profiles.m_ActiveProfile.m_ShowDecor);
		GameUI.m_Instance.m_TopBar.m_SandboxUndoRedoPanel.SetActive(value: true);
		GameUI.m_Instance.m_BottomBar.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_Selection.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_Clipboard.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_LiveStress.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_SimToolBar.gameObject.SetActive(value: false);
	}

	public static void UpdateSandboxMenu(bool cameraInTransition)
	{
		GameUI.m_Instance.m_SandboxMenu.gameObject.SetActive(!cameraInTransition);
		float x = 0f;
		if (!GameUI.m_Instance.m_SandboxMenu.IsCollapsed())
		{
			x = (SandboxSelectionSet.IsEmpty() ? (-207f) : (-417f));
		}
		Vector2 normalizedPosition = GameUI.m_Instance.m_EventEditor.m_ScrollRect.normalizedPosition;
		GameUI.m_Instance.m_EventEditor.m_RootRectTransform.sizeDelta = new Vector2(x, GameUI.m_Instance.m_EventEditor.m_RootRectTransform.sizeDelta.y);
		GameUI.m_Instance.m_EventEditor.m_ScrollRect.normalizedPosition = normalizedPosition;
	}

	public static void DeActivateAllPanels()
	{
		GameUI.m_Instance.m_SandboxMenu.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_EventEditor.gameObject.SetActive(value: false);
	}
}
