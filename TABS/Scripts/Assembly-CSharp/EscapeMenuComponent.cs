using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GamepadUI.StateManager.Core;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS.Services;
using Landfall.TABS.UnitPlacement;
using Landfall.TABS_Input;
using LevelCreator;
using TFBGames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EscapeMenuComponent : UIComponent
{
	[SerializeField]
	private CanvasGroup buttonsCanvasGroup;

	[SerializeField]
	private CanvasGroup escapeMenuButtonGroup;

	[SerializeField]
	private Button m_FirstButton;

	[SerializeField]
	private CodeAnimation backgroundAnimationHandler;

	[SerializeField]
	private GameObject bottomGamePadPrompts;

	[SerializeField]
	private CodeAnimation inputViewerAnimator;

	[SerializeField]
	private float fadeAnimationTime = 0.1f;

	[SerializeField]
	private AnimationCurve fadeAnimationCurve;

	[SerializeField]
	private Transform m_ButtonsContainer;

	[SerializeField]
	private Button m_ResumeButton;

	[SerializeField]
	private Button m_endBattleButton;

	[SerializeField]
	private Button m_customContentButton;

	[SerializeField]
	private Button m_showInputViewerButton;

	[SerializeField]
	private Button m_returnToEditorButton;

	private PlayerActions m_playerActions;

	private float fadeAnimTimer;

	private float invFadeAnimationTime;

	private CodeAnimation buttonsAnimationHandler;

	private UnitPlacementBrush m_PlacementBrush;

	private BaseGameMode m_currentGameMode;

	private GameStateManager m_gameStateManager;

	private bool enableCustomContentButton;

	private InputService m_InputService;

	private ModalPanel m_modalPanel;

	private INetworkService m_networkService;

	private INetworkQuitController m_networkQuit;

	private int? m_waitForMultiplayerToEndOpenPanelId;

	public System.Action OpenMenu;

	public System.Action CloseMenu;

	private float m_cachedTargetTimeScale = 1f;

	private bool IsWaitingForMultiplayerToEnd => m_waitForMultiplayerToEndOpenPanelId.HasValue;

	protected override void Awake()
	{
		m_playerActions = PlayerActions.Instance;
		invFadeAnimationTime = 1f / fadeAnimationTime;
		buttonsAnimationHandler = escapeMenuButtonGroup.GetComponent<CodeAnimation>();
		m_InputService = ServiceLocator.GetService<InputService>();
		m_gameStateManager = ServiceLocator.GetService<GameStateManager>();
		m_modalPanel = ServiceLocator.GetService<ModalPanel>();
		GameModeService service = ServiceLocator.GetService<GameModeService>();
		m_networkService = ServiceLocator.GetService<INetworkService>();
		m_networkQuit = ServiceLocator.GetService<INetworkQuitController>();
		if (NetworkBattleUICloser.Instance != null)
		{
			NetworkBattleUICloser.Instance.RegisterComponent(this, OnMultiplayerMatchEnded);
		}
		if (m_ResumeButton != null)
		{
			m_ResumeButton.gameObject.SetActive(value: false);
		}
		if ((bool)m_endBattleButton)
		{
			m_endBattleButton.gameObject.SetActive(value: false);
		}
		m_currentGameMode = service.CurrentGameMode;
		m_PlacementBrush = m_currentGameMode.Brush;
		enableCustomContentButton = (service.IsCurrentBaseGameModeType<SandboxGameMode>() || (service.IsCurrentBaseGameModeType<CampaignGameMode>() && CampaignPlayerDataHolder.PlayingSingleBattles)) && ServiceLocator.GetService<AccountManager>().ActiveAccount != null && !SpawnLevel.IsCustomLevelTestRun;
		bool active = false;
		m_returnToEditorButton.gameObject.SetActive(active);
		m_returnToEditorButton.onClick.AddListener(delegate
		{
			SpawnLevel.ReturnToEditor();
		});
		RebuildNavigation();
		m_ResumeButton.onClick.AddListener(delegate
		{
			EnableCurtains(enable: true);
		});
		m_ResumeButton.onClick.AddListener(delegate
		{
			Resume();
		});
		base.Awake();
	}

	private new void OnDestroy()
	{
		if (NetworkBattleUICloser.Instance != null)
		{
			NetworkBattleUICloser.Instance.UnregisterComponent(this);
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if (m_InputService != null)
		{
			m_InputService.InputChanged += OnInputChange;
			OnInputChange(PlayerActions.Instance.InputType);
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (inputViewerAnimator != null && inputViewerAnimator.IsVisible)
		{
			inputViewerAnimator.PlayOut();
		}
		if (m_InputService != null)
		{
			m_InputService.InputChanged -= OnInputChange;
		}
	}

	private void RebuildNavigation()
	{
		IList<Selectable> selectableChildren = m_ButtonsContainer.GetSelectableChildren();
		if (selectableChildren == null)
		{
			return;
		}
		UIHelpers.CreateExplicitLinearNavigation(selectableChildren, horizontal: false);
		foreach (Selectable item in selectableChildren)
		{
			if (item.isActiveAndEnabled)
			{
				m_FirstButton = item.GetComponent<Button>();
				break;
			}
		}
	}

	protected override void Update()
	{
		HandleControlsViewer();
		if (m_playerActions.m_back.WasPressed || m_playerActions.m_menu.WasPressed)
		{
			m_ResumeButton.onClick.Invoke();
		}
	}

	private void Resume()
	{
		StartCoroutine(Delay());
		IEnumerator Delay()
		{
			yield return 5;
			ITimeService service = ServiceLocator.GetService<ITimeService>();
			if (!MainCam.instance.GetComponentInParent<CameraAbilityPossess>().IsPossessing)
			{
				service.Unlock();
			}
			else
			{
				service.Lock();
			}
		}
	}

	protected override void OnOpen()
	{
		base.OnOpen();
		bool flag = m_playerActions.InputType == InputType.Controller;
		OnInputChange(m_playerActions.InputType);
		SetContextualButtons(!flag);
		SetPause(paused: true);
		if (autoHighlightSelectableWhenActive)
		{
			SetFirstSelectedButton();
		}
		escapeMenuButtonGroup.interactable = true;
		m_customContentButton.gameObject.SetActive(enableCustomContentButton);
		buttonsCanvasGroup.alpha = 1f;
		SetPause(paused: true);
		if (!backgroundAnimationHandler.IsVisible)
		{
			backgroundAnimationHandler.PlayIn();
		}
		buttonsAnimationHandler.PlayIn();
		RebuildNavigation();
		EnableCurtains(enable: false);
		OpenMenu?.Invoke();
	}

	protected override void OnBackground()
	{
		base.OnBackground();
		buttonsAnimationHandler.PlayOut();
	}

	protected override void OnClose()
	{
		base.OnClose();
		if (m_currentGameMode != null && !m_currentGameMode.MatchOver)
		{
			SetPause(paused: false);
		}
		buttonsAnimationHandler.PlayOut();
		escapeMenuButtonGroup.interactable = false;
		m_PlacementBrush.PreventPlacementBuffer();
		CloseMenu?.Invoke();
	}

	private void EnableCurtains(bool enable)
	{
		if (m_currentGameMode.MatchOver)
		{
			if (!enable)
			{
				m_currentGameMode.CurtainController.OpenCurtains();
			}
			else
			{
				m_currentGameMode.CurtainController.PeekCurtains();
			}
		}
	}

	public async void GoToMainMenu()
	{
		if (!IsWaitingForMultiplayerToEnd)
		{
			if (m_networkService.IsRunning)
			{
				await QuitMultiplayerAndWaitForItToEnd();
			}
			if (m_InputService != null && m_currentGameMode is LocalMultiplayerGameMode)
			{
				m_InputService.ClearInputsForMainMenu();
			}
			UIUtil.AskForConfirmationFirstIfEditorHasDirtyUserLevel(delegate
			{
				UIUtil.ClearEditorLevel();
				TABSSceneManager.LoadMainMenu();
			});
		}
	}

	public void ShowBackgroundBlur(bool show)
	{
		if (show)
		{
			backgroundAnimationHandler.PlayIn();
		}
		else
		{
			backgroundAnimationHandler.PlayOut();
		}
	}

	private void SetPause(bool paused)
	{
		LazyLoadGameMode();
		m_currentGameMode.PlacementCamera.AllowMovement(!paused);
		if (m_networkService.IsRunning)
		{
			return;
		}
		ITimeService timeService = m_currentGameMode.TimeService;
		if (paused)
		{
			if (!Mathf.Approximately(timeService.CurrentTargetTimeScale, 0f))
			{
				m_cachedTargetTimeScale = timeService.CurrentTargetTimeScale;
			}
			timeService.Unlock();
			timeService.SetState(0f, 0f);
			timeService.Pause();
			timeService.Lock();
		}
		else
		{
			timeService.Unlock();
			timeService.UnPause();
			timeService.SetState(m_cachedTargetTimeScale, 0f);
			timeService.Lock();
		}
	}

	private void LazyLoadGameMode()
	{
		if (m_currentGameMode == null)
		{
			Debug.LogWarning("Lazy Loading GameModeServiceCurrentGameMode because it was null.");
			m_currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
		}
	}

	public void GoToCustomContent()
	{
		if (m_networkService.IsRunning)
		{
			m_networkService.ShutdownAsync(null);
		}
		UIUtil.AskForConfirmationFirstIfEditorHasDirtyUserLevel(delegate
		{
			UIUtil.ClearEditorLevel();
			Proceed();
		});
		void Proceed()
		{
			GameModeService service = ServiceLocator.GetService<GameModeService>();
			bool flag = service.IsCurrentBaseGameModeType<SandboxGameMode>();
			if (service.IsCurrentBaseGameModeType<CampaignGameMode>())
			{
				CustomContetnManager.previousBattle = CampaignPlayerDataHolder.GetCurrentLevel();
			}
			else if (flag)
			{
				CustomContetnManager.previousMap = TABSSceneManager.CurrentLoadedMap;
			}
			CustomContetnManager.previousCustomMap = SpawnLevel.CustomMap;
			ITimeService service2 = ServiceLocator.GetService<ITimeService>();
			service2.Unlock();
			service2.SetState(1f, 0f);
			service2.UnPause();
			TABSSceneManager.LoadCustomContentPage();
		}
	}

	private void OnMultiplayerMatchEnded()
	{
		if (base.IsActive && m_ResumeButton != null)
		{
			m_ResumeButton.onClick.Invoke();
		}
	}

	private async Task QuitMultiplayerAndWaitForItToEnd()
	{
		m_networkQuit.QuitMultiplayerGame(loadMainMenu: false);
		m_waitForMultiplayerToEndOpenPanelId = m_modalPanel.WaitPopUpWithFocus("MP_LABEL_LOADING", -1f, null, null, true);
		m_modalPanel.SetForWaitingToShutdownPopup(showForWaitingToShutdown: true);
		await Task.Delay(500);
		while (m_networkService.IsRunning)
		{
			await Task.Delay(200);
		}
		await Task.Delay(1000);
		if (m_waitForMultiplayerToEndOpenPanelId.HasValue && m_waitForMultiplayerToEndOpenPanelId.Value == m_modalPanel.OpenId)
		{
			m_modalPanel.CloseWaitPopup();
		}
		m_waitForMultiplayerToEndOpenPanelId = null;
		m_modalPanel.SetForWaitingToShutdownPopup(showForWaitingToShutdown: false);
	}

	private bool HandleControlsViewer()
	{
		if (m_playerActions.m_showInput.WasPressed)
		{
			fadeAnimTimer = 0f;
			if (EventSystem.current != null)
			{
				EventSystem.current.SetSelectedGameObject(null);
			}
			m_showInputViewerButton.onClick.Invoke();
		}
		bool isPressed = m_playerActions.m_showInput.IsPressed;
		if (isPressed)
		{
			if (fadeAnimTimer <= fadeAnimationTime || buttonsCanvasGroup.alpha > 0f)
			{
				buttonsCanvasGroup.alpha = 1f - fadeAnimationCurve.Evaluate(fadeAnimTimer * invFadeAnimationTime);
				fadeAnimTimer += Time.unscaledDeltaTime;
				return isPressed;
			}
		}
		else if (buttonsCanvasGroup.alpha < 1f && fadeAnimTimer <= fadeAnimationTime)
		{
			buttonsCanvasGroup.alpha = fadeAnimationCurve.Evaluate(fadeAnimTimer * invFadeAnimationTime);
			fadeAnimTimer += Time.unscaledDeltaTime;
		}
		return isPressed;
	}

	private void SetFirstSelectedButton()
	{
		if (EventSystem.current != null && m_FirstButton != null && autoHighlightSelectableWhenActive)
		{
			EventSystem.current.SetSelectedGameObject(m_FirstButton.gameObject);
		}
	}

	private void SetContextualButtons(bool isDesktop)
	{
		if (m_ResumeButton != null)
		{
			m_ResumeButton.gameObject.SetActive(isDesktop);
		}
		bool active = m_gameStateManager.GameState == GameState.BattleState;
		if ((bool)m_endBattleButton)
		{
			m_endBattleButton.gameObject.SetActive(active);
		}
	}

	private void OnInputChange(InputType inputType)
	{
		switch (inputType)
		{
		case InputType.Controller:
			autoHighlightSelectableWhenActive = true;
			SetContextualButtons(isDesktop: false);
			RebuildNavigation();
			SetFirstSelectedButton();
			break;
		case InputType.Keyboard:
		case InputType.Any:
			autoHighlightSelectableWhenActive = false;
			SetContextualButtons(isDesktop: true);
			EventSystem.current.SetSelectedGameObject(null);
			break;
		}
		if (bottomGamePadPrompts != null)
		{
			bottomGamePadPrompts.SetActive(autoHighlightSelectableWhenActive);
		}
	}
}
