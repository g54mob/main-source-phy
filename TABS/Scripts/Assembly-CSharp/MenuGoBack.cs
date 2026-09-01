using System;
using Landfall.TABS_Input;
using UnityEngine;

public class MenuGoBack : MonoBehaviour
{
	[SerializeField]
	private SettingsHandler m_settingsHandler;

	private MainMenuStateHandler m_menuStateHandler;

	private PlayerActions m_playerActions;

	private void Start()
	{
		m_menuStateHandler = GetComponent<MainMenuStateHandler>();
		m_playerActions = PlayerActions.Instance;
	}

	private void Update()
	{
		if (!m_playerActions.m_back.WasPressed)
		{
			return;
		}
		switch (m_menuStateHandler.m_CurrentMenuState)
		{
		case MenuState.Main:
		{
			ModBrowserLandfall modBrowserLandfall = UnityEngine.Object.FindObjectOfType<ModBrowserLandfall>();
			if ((bool)modBrowserLandfall && modBrowserLandfall.gameObject.activeInHierarchy)
			{
				modBrowserLandfall.transform.parent.gameObject.SetActive(value: false);
			}
			break;
		}
		case MenuState.CampaignSelect:
			m_menuStateHandler.GoToMenu(MenuState.Main);
			m_menuStateHandler.CloseCampaignSelect();
			break;
		case MenuState.CampaignLevels:
			m_menuStateHandler.GoToMenu(MenuState.CampaignSelect);
			m_menuStateHandler.CloseCampaignLevelSelect();
			break;
		case MenuState.Settings:
			if (!m_playerActions.IsListeningForBinding)
			{
				if (m_settingsHandler.IsPopupOpen())
				{
					m_settingsHandler.CloseSettingsPopUp();
					break;
				}
				m_menuStateHandler.CloseSettingsUI();
				m_menuStateHandler.GoToMenu(MenuState.Main);
			}
			break;
		case MenuState.LevelSelection:
			m_menuStateHandler.GoToMenu(MenuState.Main);
			m_menuStateHandler.CloseSandboxSelect();
			break;
		case MenuState.ThanksScreen:
			if (!m_playerActions.IsListeningForBinding)
			{
				m_menuStateHandler.GoToMenu(MenuState.Main);
			}
			m_menuStateHandler.CloseCampaignRatingUI();
			break;
		case MenuState.Roadmap:
			m_menuStateHandler.GoToMenu(MenuState.Main);
			m_menuStateHandler.CloseRoadmapUI();
			break;
		case MenuState.Workshop:
			m_menuStateHandler.GoToMenu(MenuState.Main);
			m_menuStateHandler.CloseModBrowserUI();
			break;
		default:
			throw new ArgumentOutOfRangeException();
		case MenuState.UnitCreator:
		case MenuState.ScreenSize:
		case MenuState.Multiplayer:
			break;
		}
	}
}
