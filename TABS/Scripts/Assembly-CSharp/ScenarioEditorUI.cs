using System;
using System.Collections;
using System.Collections.Generic;
using GamepadUI.StateManager.Core;
using Landfall.TABS;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using ModIO;
using ModIO.UI;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioEditorUI : UIComponent
{
	[SerializeField]
	private Toggle battlesToggle;

	[SerializeField]
	private Toggle uploadToggle;

	[SerializeField]
	private Toggle[] pageToggles;

	[SerializeField]
	private CodeAnimation m_BackgroundCodeAnimator;

	[SerializeField]
	private Button m_BackButton;

	[SerializeField]
	public GameObject m_ModIOLogInDialog;

	[SerializeField]
	public GameObject m_campaignCreatorDesignPanel;

	private ModalPanel modalPanelService;

	private ClickOffCatcher m_PopUpClickOffCatcher;

	private BattleCreatorTabsUIHandler battleCreatorTabsUiHandler;

	private CodeAnimation animationHandler;

	private PlayerActions playerActions;

	private int pageToggleIndex = 1;

	private BattleCreatorState currentBattleState;

	private BattleCreatorScreenState currentScreenState;

	private Stack<BattleCreatorState> battleStateStack = new Stack<BattleCreatorState>();

	private Stack<BattleCreatorScreenState> battleScreenStateStack = new Stack<BattleCreatorScreenState>();

	private bool canSwitchTabs;

	private static ScenarioEditorUI internalInstance;

	private bool modIoDialogOpen
	{
		get
		{
			if (m_ModIOLogInDialog != null)
			{
				return m_ModIOLogInDialog.activeSelf;
			}
			return false;
		}
	}

	public static ScenarioEditorUI instance => internalInstance;

	protected override void Awake()
	{
		internalInstance = this;
		playerActions = PlayerActions.Instance;
		battleCreatorTabsUiHandler = GetComponent<BattleCreatorTabsUIHandler>();
		battleCreatorTabsUiHandler.PageOpened += OnPageOpened;
		battleCreatorTabsUiHandler.UIClosed += OnUIClose;
		animationHandler = GetComponent<CodeAnimation>();
		m_PopUpClickOffCatcher = m_ModIOLogInDialog.GetComponentInChildren<ClickOffCatcher>();
		uploadToggle.onValueChanged.AddListener(delegate(bool isOn)
		{
			GoToUpload(isOn);
		});
		modalPanelService = ServiceLocator.GetService<ModalPanel>();
		base.Awake();
	}

	protected override void Update()
	{
		IBattleCreatorMenu battleCreatorMenu = battleCreatorTabsUiHandler.m_CampaignMenus[currentScreenState];
		battleCreatorMenu.NavigateUIWithController(playerActions);
		canSwitchTabs = battleCreatorMenu.AllowPageChange;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		battleCreatorTabsUiHandler.PageOpened -= OnPageOpened;
		battleCreatorTabsUiHandler.UIClosed -= OnUIClose;
	}

	public void CloseScenarioEditor()
	{
		battleScreenStateStack.Clear();
		battleStateStack.Clear();
		battleCreatorTabsUiHandler.Close();
	}

	private void CloseScenarioEditorFromGamepad()
	{
		if (modIoDialogOpen)
		{
			if (m_PopUpClickOffCatcher != null)
			{
				m_PopUpClickOffCatcher.clickedOff.Invoke();
			}
			return;
		}
		battleCreatorTabsUiHandler.Close();
		if (m_BackButton != null)
		{
			m_BackButton.onClick.Invoke();
		}
	}

	public void GoToBattles(bool toggleOn)
	{
		if (toggleOn)
		{
			battleCreatorTabsUiHandler?.OpenNewScreen(BattleCreatorScreenState.AssetMenu, BattleCreatorState.Load, null, closeIfAlreadyOpen: false);
		}
	}

	public void GoToCampaigns(bool toggleOn)
	{
		if (toggleOn)
		{
			battleCreatorTabsUiHandler?.OpenNewScreen(BattleCreatorScreenState.AssetMenu, BattleCreatorState.CampaignCreator, null, closeIfAlreadyOpen: false);
		}
	}

	public void GoToUpload(bool toggleOn)
	{
		if (toggleOn)
		{
			battleCreatorTabsUiHandler?.OpenUploadScreen();
		}
	}

	public void GoToStartScreen(bool toggleOn)
	{
		if (toggleOn)
		{
			battleCreatorTabsUiHandler?.OpenNewScreen(BattleCreatorScreenState.StartScreen, BattleCreatorState.Permissions, null, closeIfAlreadyOpen: false);
		}
	}

	public void GoToPreviousScreen()
	{
		if (!modalPanelService.IsPopupOpen && !m_campaignCreatorDesignPanel.activeSelf)
		{
			if (battleScreenStateStack.Count <= 1)
			{
				CloseScenarioEditor();
				return;
			}
			battleCreatorTabsUiHandler?.OpenNewScreen(battleScreenStateStack.Pop(), battleStateStack.Pop(), null, closeIfAlreadyOpen: false);
			battleScreenStateStack.Pop();
			battleStateStack.Pop();
		}
	}

	public void GoToSaveBattleScreen()
	{
		battleCreatorTabsUiHandler?.OpenNewScreen(BattleCreatorScreenState.Save, BattleCreatorState.Save, null, closeIfAlreadyOpen: false);
		battleScreenStateStack.Clear();
		battleStateStack.Clear();
	}

	private void OnPageOpened(BattleCreatorScreenState screenState)
	{
		battleScreenStateStack.Push(currentScreenState);
		currentScreenState = screenState;
	}

	protected override void OnOpen()
	{
		animationHandler.PlayIn();
		if (m_BackgroundCodeAnimator != null)
		{
			m_BackgroundCodeAnimator.PlayIn();
		}
		Toggle[] array = pageToggles;
		foreach (Toggle obj in array)
		{
			obj.isOn = obj == battlesToggle;
		}
		battleScreenStateStack.Clear();
		battleStateStack.Clear();
		GoToSaveBattleScreen();
	}

	protected override void OnClose()
	{
		animationHandler.PlayOut();
		if (m_BackgroundCodeAnimator != null)
		{
			m_BackgroundCodeAnimator.PlayOut();
		}
		battlesToggle.isOn = true;
		if (modIoDialogOpen && m_PopUpClickOffCatcher != null)
		{
			m_PopUpClickOffCatcher.clickedOff.Invoke();
		}
	}

	private void OnUIClose(BattleCreatorState uiState)
	{
		battleStateStack.Push(currentBattleState);
		currentBattleState = uiState;
		if (uiState == BattleCreatorState.None)
		{
			base.Close();
		}
	}

	public void WaitForToken(Action onLogin)
	{
		StartCoroutine(WaitForTokenCoroutine(onLogin));
	}

	private IEnumerator WaitForTokenCoroutine(Action onLogin)
	{
		if (!HasValidToken())
		{
			m_ModIOLogInDialog.SetActive(value: true);
		}
		yield return new WaitUntil(() => HasValidToken() || !instance.m_ModIOLogInDialog.activeSelf);
		if (HasValidToken())
		{
			onLogin?.Invoke();
		}
	}

	private bool HasValidToken()
	{
		return LocalUser.AuthenticationState == AuthenticationState.ValidToken;
	}
}
