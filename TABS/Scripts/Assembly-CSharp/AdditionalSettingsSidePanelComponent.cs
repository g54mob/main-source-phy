using System;
using System.Collections;
using System.Collections.Generic;
using GamepadUI.StateManager.Core;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS_Input;
using LevelCreator;
using TFBGames;
using UnityEngine;
using UnityEngine.UI;

public class AdditionalSettingsSidePanelComponent : UIComponent
{
	[SerializeField]
	private Transform m_ButtonsContainer;

	[SerializeField]
	private CodeAnimation m_CodeAnimationRoot;

	[SerializeField]
	private Button m_BackButton;

	[SerializeField]
	private Button m_BattleCreatorButton;

	[SerializeField]
	private Button m_MapSettingsButton;

	[SerializeField]
	private Button[] m_ButtonsThatAnimatePlacementUIOut;

	[SerializeField]
	private SimpleStateAnimation[] m_PlacementUIAnimators;

	[SerializeField]
	private MovingUIElement[] m_PlacementUIMovingUIElements;

	[SerializeField]
	private GameObject m_OpenButtonGameObject;

	[SerializeField]
	private Button m_OpenCloseButton;

	[SerializeField]
	private Button m_UIStateButton;

	[SerializeField]
	private Button m_CampaignCreatorButton;

	[SerializeField]
	private GameObject m_CampaignCreatorUILineDivider;

	[SerializeField]
	private CodeAnimation m_HideStateCodeAnimator;

	[SerializeField]
	private EscapeMenuComponent m_EscapeMenuComponent;

	[SerializeField]
	private LevelSelection m_LevelSelection;

	private bool m_PreventPlacementUIAnimators;

	private PlayerActions m_PlayerActions;

	private InputService m_InputService;

	private BaseGameMode m_currentGameMode;

	private bool m_IsSandbox;

	private IAccountPermissions accountPermissions;

	protected override void Awake()
	{
		base.Awake();
		m_PlayerActions = PlayerActions.Instance;
		m_InputService = ServiceLocator.GetService<InputService>();
		m_IsSandbox = CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.Sandbox;
		if (m_BattleCreatorButton != null)
		{
			m_BattleCreatorButton.gameObject.SetActive(m_IsSandbox);
		}
		if (CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.Campaign && m_MapSettingsButton != null)
		{
			m_MapSettingsButton.gameObject.SetActive(value: false);
		}
		EnableSettingsButton();
		InputChanged(m_PlayerActions.InputType);
		m_currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
		if (m_OpenCloseButton != null)
		{
			m_OpenCloseButton.onClick.RemoveAllListeners();
			m_OpenCloseButton.onClick.AddListener(OpenFromCog);
		}
		SubscribeToGameModeEvents();
		accountPermissions = ServiceLocator.GetService<IAccountPermissions>();
		if (m_EscapeMenuComponent != null)
		{
			EscapeMenuComponent escapeMenuComponent = m_EscapeMenuComponent;
			escapeMenuComponent.OpenMenu = (System.Action)Delegate.Combine(escapeMenuComponent.OpenMenu, new System.Action(DisableSettingsButton));
			EscapeMenuComponent escapeMenuComponent2 = m_EscapeMenuComponent;
			escapeMenuComponent2.CloseMenu = (System.Action)Delegate.Combine(escapeMenuComponent2.CloseMenu, new System.Action(EnableSettingsButton));
		}
		if (m_LevelSelection != null)
		{
			LevelSelection levelSelection = m_LevelSelection;
			levelSelection.OpenMenu = (System.Action)Delegate.Combine(levelSelection.OpenMenu, new System.Action(DisableSettingsButton));
			LevelSelection levelSelection2 = m_LevelSelection;
			levelSelection2.CloseMenu = (System.Action)Delegate.Combine(levelSelection2.CloseMenu, new System.Action(EnableSettingsButton));
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if (m_InputService != null)
		{
			m_InputService.InputChanged += InputChanged;
		}
		if (m_ButtonsThatAnimatePlacementUIOut == null)
		{
			return;
		}
		Button[] buttonsThatAnimatePlacementUIOut = m_ButtonsThatAnimatePlacementUIOut;
		foreach (Button button in buttonsThatAnimatePlacementUIOut)
		{
			if (button != null)
			{
				button.onClick.AddListener(AnimatePlacementUIOut);
			}
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (m_InputService != null)
		{
			m_InputService.InputChanged -= InputChanged;
		}
		if (m_ButtonsThatAnimatePlacementUIOut == null)
		{
			return;
		}
		Button[] buttonsThatAnimatePlacementUIOut = m_ButtonsThatAnimatePlacementUIOut;
		foreach (Button button in buttonsThatAnimatePlacementUIOut)
		{
			if (button != null)
			{
				button.onClick.RemoveListener(AnimatePlacementUIOut);
			}
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		UnsubscribeFromGameModeEvents();
		if (m_EscapeMenuComponent != null)
		{
			EscapeMenuComponent escapeMenuComponent = m_EscapeMenuComponent;
			escapeMenuComponent.OpenMenu = (System.Action)Delegate.Remove(escapeMenuComponent.OpenMenu, new System.Action(DisableSettingsButton));
			EscapeMenuComponent escapeMenuComponent2 = m_EscapeMenuComponent;
			escapeMenuComponent2.CloseMenu = (System.Action)Delegate.Remove(escapeMenuComponent2.CloseMenu, new System.Action(EnableSettingsButton));
		}
		if (m_LevelSelection != null)
		{
			LevelSelection levelSelection = m_LevelSelection;
			levelSelection.OpenMenu = (System.Action)Delegate.Remove(levelSelection.OpenMenu, new System.Action(DisableSettingsButton));
			LevelSelection levelSelection2 = m_LevelSelection;
			levelSelection2.CloseMenu = (System.Action)Delegate.Remove(levelSelection2.CloseMenu, new System.Action(EnableSettingsButton));
		}
	}

	public void SetSettingsButtonState(bool enabled)
	{
		if (m_OpenButtonGameObject == null)
		{
			return;
		}
		bool flag = enabled;
		if (!flag)
		{
			m_OpenButtonGameObject.SetActive(flag);
			return;
		}
		GameModeState currentGameModeState = CampaignPlayerDataHolder.CurrentGameModeState;
		flag = m_IsSandbox || currentGameModeState == GameModeState.LocalMultiplayer || currentGameModeState == GameModeState.OnlineMultiplayer;
		if (SpawnLevel.IsCustomLevelTestRun)
		{
			flag = false;
		}
		m_OpenButtonGameObject.SetActive(flag);
	}

	private void EnableSettingsButton()
	{
		SetSettingsButtonState(enabled: true);
	}

	private void DisableSettingsButton()
	{
		SetSettingsButtonState(enabled: false);
	}

	private void InputChanged(InputType inputType)
	{
		switch (inputType)
		{
		case InputType.Controller:
			autoHighlightSelectableWhenActive = true;
			break;
		case InputType.Keyboard:
		case InputType.Any:
			autoHighlightSelectableWhenActive = false;
			break;
		}
		RebuildNavigation();
	}

	private void LazyLoadGameMode()
	{
		if (m_currentGameMode == null)
		{
			Debug.LogWarning("Lazy Loading GameModeServiceCurrentGameMode because it was null.");
			m_currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
		}
	}

	protected override void OnOpen()
	{
		base.OnOpen();
		if (m_CodeAnimationRoot != null)
		{
			m_CodeAnimationRoot.PlayIn();
		}
		bool flag = ServiceLocator.GetService<GameStateManager>().GameState == GameState.PlacementState;
		if (CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.Sandbox && m_MapSettingsButton != null)
		{
			m_MapSettingsButton.gameObject.SetActive(flag);
		}
		if (m_BattleCreatorButton != null && m_BattleCreatorButton.gameObject.activeInHierarchy)
		{
			m_BattleCreatorButton.interactable = flag;
		}
		InputChanged(m_PlayerActions.InputType);
		if (m_OpenCloseButton != null)
		{
			m_OpenCloseButton.onClick.RemoveAllListeners();
			m_OpenCloseButton.onClick.AddListener(CloseFromCog);
		}
		AnimatePlacementUIOut();
	}

	protected override void OnClose()
	{
		base.OnClose();
		if (m_CodeAnimationRoot != null)
		{
			m_CodeAnimationRoot.PlayOut();
		}
		if (m_OpenCloseButton != null)
		{
			m_OpenCloseButton.onClick.RemoveAllListeners();
			m_OpenCloseButton.onClick.AddListener(OpenFromCog);
		}
	}

	protected override void Update()
	{
		base.Update();
		if (base.IsActive && m_PlayerActions != null && (m_PlayerActions.m_back.WasPressed || m_PlayerActions.m_mapSettings.WasPressed) && m_BackButton != null)
		{
			m_BackButton.onClick.Invoke();
		}
	}

	private void CloseFromCog()
	{
		if (m_BackButton != null && base.IsActive)
		{
			m_BackButton.onClick.Invoke();
		}
	}

	private void OpenFromCog()
	{
		NetworkBattleController service = ServiceLocator.GetService<NetworkBattleController>();
		if ((!(service != null) || service.AreBothPlayersInBattleScene) && m_UIStateButton != null && !base.IsActive)
		{
			m_UIStateButton.onClick.Invoke();
		}
	}

	private void SubscribeToGameModeEvents()
	{
		m_currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
		if (m_currentGameMode != null)
		{
			m_currentGameMode.EnteredFreeLook += OnEnterFreeLook;
			m_currentGameMode.ExitedFreeLook += OnExitFreeLook;
		}
	}

	private void UnsubscribeFromGameModeEvents()
	{
		if (m_currentGameMode != null)
		{
			m_currentGameMode.EnteredFreeLook -= OnEnterFreeLook;
			m_currentGameMode.ExitedFreeLook -= OnExitFreeLook;
		}
	}

	private void OnEnterFreeLook()
	{
		if (m_HideStateCodeAnimator != null)
		{
			m_HideStateCodeAnimator.PlayOut();
		}
	}

	private void OnExitFreeLook()
	{
		if (m_HideStateCodeAnimator != null)
		{
			m_HideStateCodeAnimator.PlayIn();
		}
	}

	private void AnimatePlacementUIOut()
	{
		if (m_PlacementUIAnimators != null)
		{
			SimpleStateAnimation[] placementUIAnimators = m_PlacementUIAnimators;
			foreach (SimpleStateAnimation simpleStateAnimation in placementUIAnimators)
			{
				if (simpleStateAnimation != null)
				{
					simpleStateAnimation.SetState(SimpleStateAnimation.State.State02);
				}
			}
		}
		if (m_PlacementUIMovingUIElements == null)
		{
			return;
		}
		MovingUIElement[] placementUIMovingUIElements = m_PlacementUIMovingUIElements;
		foreach (MovingUIElement movingUIElement in placementUIMovingUIElements)
		{
			if (movingUIElement != null)
			{
				movingUIElement.Hide();
			}
		}
	}

	private void RebuildNavigation()
	{
		IList<Selectable> selectablesInParent;
		if (base.IsActive)
		{
			selectablesInParent = m_ButtonsContainer.GetSelectableChildren(excludeInactive: true);
			if (selectablesInParent != null)
			{
				StartCoroutine(BuildNavigation());
			}
		}
		IEnumerator BuildNavigation()
		{
			yield return null;
			UIHelpers.CreateExplicitLinearNavigation(selectablesInParent, horizontal: false);
			Selectable selectable = selectablesInParent[0];
			if (selectable != null && autoHighlightSelectableWhenActive)
			{
				selectable.Select();
			}
		}
	}
}
