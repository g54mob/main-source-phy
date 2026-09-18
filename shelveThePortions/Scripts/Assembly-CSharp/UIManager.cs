using System;
using UnityEngine;

public class UIManager : SceneSingleton<UIManager>
{
	[SerializeField]
	private CanvasGroup mainMenuCG;

	[SerializeField]
	private CanvasGroup inGameCG;

	[SerializeField]
	private CanvasGroup inGameTimerCG;

	[SerializeField]
	private CanvasGroup optionsCG;

	[SerializeField]
	private CanvasGroup pauseMenuCG;

	[SerializeField]
	private CanvasGroup gameOverMenuCG;

	[SerializeField]
	private CanvasGroup puzzleZoomCameraMenuCG;

	[SerializeField]
	private CanvasGroup catMenuCG;

	[SerializeField]
	private CanvasGroup puzzleTextPanelUI;

	[SerializeField]
	private CanvasGroup confirmationPanelUI;

	[SerializeField]
	private CanvasGroup playtestPanelUI;

	[SerializeField]
	private CanvasGroup lastCGPanel;

	[SerializeField]
	private CanvasGroup holdLastCGPanel;

	[Space]
	[SerializeField]
	private GameObject reticleObject;

	private IUISelectable currentIUISelectable;

	private bool disableInGameHUD;

	protected override void Awake()
	{
		base.Awake();
		EventManager.GamePause += ShowPauseMenu;
		EventManager.GameResume += ShowInGameMenu;
		EventManager.MainMenuLoaded += ShowMainMenu;
		EventManager.GameStart += ShowInGameMenu;
		EventManager.OnCutsceneStart += DisableUI;
		EventManager.OnCutsceneEnd += ShowInGameMenu;
		EventManager.PlaytestEnd += ShowPlaytestMenu;
		EventManager.GameEnd += DisableUI;
		EventManager.OnControlsChange += OnControlsChange;
	}

	private void OnDisable()
	{
		EventManager.GamePause -= ShowPauseMenu;
		EventManager.GameResume -= ShowInGameMenu;
		EventManager.MainMenuLoaded -= ShowMainMenu;
		EventManager.GameStart -= ShowInGameMenu;
		EventManager.OnCutsceneStart -= DisableUI;
		EventManager.OnCutsceneEnd -= ShowInGameMenu;
		EventManager.PlaytestEnd -= ShowPlaytestMenu;
		EventManager.GameEnd -= DisableUI;
		if (Singleton<EventManager>.Instance != null)
		{
			EventManager.OnControlsChange -= OnControlsChange;
		}
	}

	public void UpdateReticaleSize()
	{
		reticleObject.transform.localScale = Vector3.Lerp(new Vector3(0.1f, 0.1f, 0.1f), new Vector3(1f, 1f, 1f), SaveSystem.GetReticleSlider());
	}

	public void OnControlsChange()
	{
		if (Singleton<InputManager>.Instance.IsUsingGamepad && currentIUISelectable != null)
		{
			currentIUISelectable.SetSelectedButton();
		}
	}

	public void UpdateDisableInGameHudOption()
	{
		disableInGameHUD = SaveSystem.GetDisableHUDSetting();
	}

	public void UpdateDisableInGameTimer()
	{
		SetCG(inGameTimerCG, SaveSystem.GetDisableInGameTimer());
	}

	private void ShowMainMenu()
	{
		DisableUI();
		SetCG(mainMenuCG, flag: true);
		lastCGPanel = mainMenuCG;
	}

	private void ShowInGameMenu()
	{
		DisableUI();
		UpdateDisableInGameHudOption();
		UpdateDisableInGameTimer();
		if (!disableInGameHUD)
		{
			SetCG(inGameCG, flag: true);
		}
		UIBackKeyManager.ClearQueue();
		UIBackKeyManager.AddOnBackKeyPressed(SceneSingleton<GameManager>.Instance.PauseGame);
		reticleObject.SetActive(value: true);
		UpdateReticaleSize();
		lastCGPanel = inGameCG;
	}

	private void ShowPauseMenu()
	{
		DisableUI();
		SetCG(pauseMenuCG, flag: true);
		SetCG(inGameCG, flag: true);
		lastCGPanel = pauseMenuCG;
	}

	public void ShowOptionMenu(bool isPauseMenu)
	{
		DisableUI();
		if (isPauseMenu)
		{
			UIBackKeyManager.AddOnBackKeyPressed(ShowPauseMenu);
		}
		else
		{
			UIBackKeyManager.AddOnBackKeyPressed(ShowMainMenu);
		}
		SetCG(optionsCG, flag: true);
		lastCGPanel = optionsCG;
	}

	public void ShowCatCameraZoom(Action backAction)
	{
		UIBackKeyManager.AddOnBackKeyPressed(delegate
		{
			if (backAction != null)
			{
				backAction();
			}
			SceneSingleton<CatPanelUI>.Instance.DisableCatUI();
			EventManager.ActivateEvent(EventTypes.GameResume);
		});
		reticleObject.SetActive(value: false);
		SetCG(catMenuCG, flag: true);
		SceneSingleton<FirstPersonController>.Instance.enableHeadBobPuzzle = false;
	}

	public void ShowPuzzleCameraZoom(Action backAction)
	{
		LoadPuzzleUI(backAction);
		SetCG(puzzleZoomCameraMenuCG, flag: true);
	}

	public void ShowPuzzleTextPanel(PuzzleTextTypes puzzleTextType)
	{
		SceneSingleton<PuzzleTextPanelUI>.Instance.SetPanel(puzzleTextType);
		SetCG(puzzleTextPanelUI, flag: true);
		LoadPuzzleUI(null);
	}

	private void LoadPuzzleUI(Action backAction)
	{
		SceneSingleton<FirstPersonController>.Instance.enableHeadBobPuzzle = false;
		EventManager.ActivateEvent(EventTypes.PuzzleUILoaded);
		UIBackKeyManager.AddOnBackKeyPressed(delegate
		{
			if (backAction != null)
			{
				backAction();
			}
			EventManager.ActivateEvent(EventTypes.GameResume);
		});
		reticleObject.SetActive(value: false);
	}

	public void ShowConfirmationPanelUI(string textID, Action OnYesAction)
	{
		lastCGPanel.interactable = false;
		SetCG(confirmationPanelUI, flag: true);
		holdLastCGPanel = lastCGPanel;
		lastCGPanel = confirmationPanelUI;
		SceneSingleton<ConfirmationPanelUI>.Instance.SetPanel(textID, OnYesAction);
		UIBackKeyManager.AddOnBackKeyPressed(delegate
		{
			SceneSingleton<ConfirmationPanelUI>.Instance.DisableUI();
			SetCG(confirmationPanelUI, flag: false);
			lastCGPanel = holdLastCGPanel;
			SetCG(lastCGPanel, flag: true);
		});
	}

	public void ShowGameOverMenu()
	{
		DisableUI();
		SetCG(gameOverMenuCG, flag: true);
		UIBackKeyManager.AddOnBackKeyPressed(delegate
		{
			EventManager.ActivateEvent(EventTypes.GameResume);
		});
	}

	public void ShowPlaytestMenu()
	{
		DisableUI();
		SetCG(playtestPanelUI, flag: true);
		UIBackKeyManager.AddOnBackKeyPressed(delegate
		{
			SceneSingleton<PlaytestMenuUI>.Instance.GoToMainMenu();
		});
	}

	private void DisableUI()
	{
		SetCG(inGameCG, flag: false);
		SetCG(mainMenuCG, flag: false);
		SetCG(optionsCG, flag: false);
		SetCG(pauseMenuCG, flag: false);
		SetCG(gameOverMenuCG, flag: false);
		SetCG(puzzleTextPanelUI, flag: false);
		SetCG(puzzleZoomCameraMenuCG, flag: false);
		SetCG(catMenuCG, flag: false);
		SetCG(playtestPanelUI, flag: false);
		SetCG(confirmationPanelUI, flag: false);
		reticleObject.SetActive(value: false);
		SceneSingleton<FirstPersonController>.Instance.enableHeadBobPuzzle = true;
	}

	private void SetCG(CanvasGroup CG, bool flag)
	{
		CG.alpha = (flag ? 1 : 0);
		CG.interactable = flag;
		CG.blocksRaycasts = flag;
		if (flag)
		{
			SetCurrentIUISelectable(CG);
		}
	}

	private void SetCurrentIUISelectable(CanvasGroup cg)
	{
		currentIUISelectable = cg.GetComponent<IUISelectable>();
		if (currentIUISelectable != null)
		{
			currentIUISelectable.SetSelectedButton();
		}
	}
}
