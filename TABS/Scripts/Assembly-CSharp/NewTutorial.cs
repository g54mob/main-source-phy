using System.Collections;
using System.Collections.ObjectModel;
using InControl;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS.Services;
using Landfall.TABS_Input;
using TFBGames;
using TMPro;
using UnityEngine;

public class NewTutorial : GameStateListener
{
	public int battlesNeeded;

	public string TUTORIAL_KEY = "CompletedTutorial";

	public string PREREQUISITE_KEY = "CompletedPossessionTutorial";

	private const int NOT_COMPLETE = 0;

	private const int COMPLETE = 1;

	public NewTutorialStep[] tutorialSteps;

	private PlayerActions m_PlayerActions;

	private TextMeshProUGUI m_TutorialStepsText;

	private bool m_HasStarted;

	public CodeAnimation anim;

	private int currentTipID;

	private float sinceSwitch;

	private bool done;

	private GlyphService m_glyphService;

	private IPlayerPrefsPlatform m_playerPrefs;

	private ITimeService m_timeService;

	private GameModeService gamemodeService;

	private const string toMoveKey = "TUTORIAL_TO_MOVE";

	private const string controllerToMoveKey = "TUTORIAL_CONTROLLER_TO_MOVE";

	private const string moveUpAndDownKey = "TUTORIAL_MOVE_UP_DOWN";

	private const string lookAroundKey = "TUTORIAL_LOOK_AROUND";

	private const string controllerLookAround = "TUTORIAL_CONTROLLER_LOOK_AROUND";

	private const string mouseKey = "TUTORIAL_MOUSE";

	private const string possessKey = "TUTORIAL_TO_POSSESS";

	private const int GlyphOverrideSize = 150;

	private const int GlyphOverrideVerticalAlign = -5;

	private bool possessComplete;

	private CameraAbilityPossess possession;

	private bool showTip = true;

	private float clearAmmount;

	private bool isBattleState;

	protected override void Awake()
	{
		base.Awake();
		m_playerPrefs = ServiceLocator.GetService<IPlayerPrefsPlatform>();
		if (m_playerPrefs != null && m_playerPrefs.GetInt(TUTORIAL_KEY, 0) == 1)
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void Start()
	{
		possession = Object.FindObjectOfType<CameraAbilityPossess>();
		m_TutorialStepsText = GetComponentInChildren<TextMeshProUGUI>();
		m_glyphService = ServiceLocator.GetService<GlyphService>();
		m_timeService = ServiceLocator.GetService<ITimeService>();
		gamemodeService = ServiceLocator.GetService<GameModeService>();
		Init();
	}

	private void Init()
	{
		m_PlayerActions = PlayerActions.Instance;
	}

	private void Update()
	{
		CheckForInput();
		DoAnim();
		DoData();
	}

	private void DoData()
	{
		sinceSwitch += Time.unscaledDeltaTime;
	}

	private void DoAnim()
	{
		showTip = true;
		if (done)
		{
			showTip = false;
		}
		if (sinceSwitch < 0.5f)
		{
			showTip = false;
		}
		if (!m_HasStarted)
		{
			showTip = false;
		}
		if (!isBattleState)
		{
			showTip = false;
		}
		if (m_timeService.IsPaused())
		{
			showTip = false;
		}
		if (gamemodeService.CurrentGameMode.MatchOver)
		{
			showTip = false;
		}
		if (showTip && anim.currentState != CodeAnimationInstance.AnimationUse.In)
		{
			anim.PlayIn();
		}
		if (!showTip && anim.currentState != CodeAnimationInstance.AnimationUse.Out)
		{
			anim.PlayOut();
		}
	}

	private void CheckForInput()
	{
		if (!showTip)
		{
			return;
		}
		if (tutorialSteps[currentTipID].tip == NewTutorialStep.Tip.Movement && ((bool)m_PlayerActions.m_moveForward || (bool)m_PlayerActions.m_moveBackward || (bool)m_PlayerActions.m_moveLeft || (bool)m_PlayerActions.m_moveRight))
		{
			clearAmmount += Time.unscaledDeltaTime;
		}
		if (tutorialSteps[currentTipID].tip == NewTutorialStep.Tip.UpDowm && ((bool)m_PlayerActions.m_moveUp || (bool)m_PlayerActions.m_moveDown))
		{
			clearAmmount += Time.unscaledDeltaTime;
		}
		if (tutorialSteps[currentTipID].tip == NewTutorialStep.Tip.Look)
		{
			clearAmmount += m_PlayerActions.m_aim.Value.magnitude * 0.015f;
		}
		if (tutorialSteps[currentTipID].tip == NewTutorialStep.Tip.Possession_Enter)
		{
			clearAmmount += Time.unscaledDeltaTime * 0.1f;
			if (possession.IsPossessing)
			{
				clearAmmount += 1f;
			}
		}
		if (clearAmmount > 1f)
		{
			IncrementTip();
		}
	}

	public override void OnEnterPlacementState()
	{
		isBattleState = false;
	}

	public override void OnEnterBattleState()
	{
		if (battlesNeeded != 0)
		{
			int num = m_playerPrefs?.GetInt("TOTAL_BATTLES", 0) ?? 0;
			if (num < battlesNeeded)
			{
				num++;
				m_playerPrefs?.SetInt("TOTAL_BATTLES", num);
				return;
			}
			StartTutorial();
			isBattleState = true;
		}
		else if (battlesNeeded == 0)
		{
			StartTutorial();
			isBattleState = true;
		}
		if (PREREQUISITE_KEY != "")
		{
			_ = m_playerPrefs?.GetInt(PREREQUISITE_KEY) ?? 0;
			_ = 1;
		}
	}

	private void StartTutorial()
	{
		StartCoroutine(DelayStart());
	}

	private IEnumerator DelayStart()
	{
		yield return new WaitForSeconds(1f);
		m_HasStarted = true;
		UpdateTip();
	}

	private void IncrementTip()
	{
		currentTipID++;
		clearAmmount = 0f;
		sinceSwitch = 0f;
		if (currentTipID >= tutorialSteps.Length)
		{
			done = true;
			CompleteTutorial();
		}
		else
		{
			StartCoroutine(DelayUpdate());
		}
	}

	private IEnumerator DelayUpdate()
	{
		yield return new WaitForSeconds(0.5f);
		UpdateTip();
	}

	private void UpdateTip()
	{
		string text = "";
		if (currentTipID < tutorialSteps.Length)
		{
			switch (tutorialSteps[currentTipID].tip)
			{
			default:
				return;
			case NewTutorialStep.Tip.Movement:
				if (m_PlayerActions.InputType == InputType.Keyboard)
				{
					string bindings = GetBindings(m_PlayerActions.m_moveForward, onlyPrimary: true);
					string bindings2 = GetBindings(m_PlayerActions.m_moveLeft, onlyPrimary: true);
					string bindings3 = GetBindings(m_PlayerActions.m_moveBackward, onlyPrimary: true);
					string bindings4 = GetBindings(m_PlayerActions.m_moveRight, onlyPrimary: true);
					text = string.Format(Localizer.GetSinglePhrase("TUTORIAL_TO_MOVE"), bindings, bindings2, bindings3, bindings4);
				}
				else if (m_PlayerActions.InputType == InputType.Controller)
				{
					string arg3 = OverrideTextSizeAndAlignment(GetBindings(m_PlayerActions.m_moveLeft, onlyPrimary: true, m_PlayerActions.InputType), 150, -5);
					text = string.Format(Localizer.GetSinglePhrase("TUTORIAL_CONTROLLER_TO_MOVE"), arg3);
				}
				break;
			case NewTutorialStep.Tip.UpDowm:
			{
				string arg4 = OverrideTextSizeAndAlignment(GetBindings(m_PlayerActions.m_moveUp, onlyPrimary: true, m_PlayerActions.InputType), 150, -5);
				string arg5 = OverrideTextSizeAndAlignment(GetBindings(m_PlayerActions.m_moveDown, onlyPrimary: true, m_PlayerActions.InputType), 150, -5);
				text = string.Format(Localizer.GetSinglePhrase("TUTORIAL_MOVE_UP_DOWN"), arg4, arg5);
				break;
			}
			case NewTutorialStep.Tip.Look:
				if (m_PlayerActions.InputType == InputType.Keyboard)
				{
					text = Localizer.GetSinglePhrase("TUTORIAL_MOUSE");
				}
				else if (m_PlayerActions.InputType == InputType.Controller)
				{
					string arg2 = OverrideTextSizeAndAlignment(GetBindings(m_PlayerActions.m_aimUp, onlyPrimary: true, m_PlayerActions.InputType), 150, -5);
					text = string.Format(Localizer.GetSinglePhrase("TUTORIAL_CONTROLLER_LOOK_AROUND"), arg2);
				}
				break;
			case NewTutorialStep.Tip.Possession_Enter:
			{
				string arg = OverrideTextSizeAndAlignment(GetBindings(m_PlayerActions.m_possessToggle, onlyPrimary: true, m_PlayerActions.InputType), 150, -5);
				text = string.Format(Localizer.GetSinglePhrase("TUTORIAL_TO_POSSESS"), arg);
				break;
			}
			case (NewTutorialStep.Tip)3:
				return;
			}
		}
		m_TutorialStepsText.text = text;
	}

	private void CompleteTutorial()
	{
		if (m_playerPrefs != null)
		{
			m_playerPrefs.SetInt(TUTORIAL_KEY, 1);
			m_playerPrefs.Save();
		}
		StartCoroutine(DelayDestroy());
	}

	private IEnumerator DelayDestroy()
	{
		yield return new WaitForSeconds(2f);
		Object.Destroy(base.gameObject);
	}

	protected override void OnDestroy()
	{
		StopCoroutine(DelayUpdate());
		base.OnDestroy();
	}

	private string GetBindings(PlayerAction action, bool onlyPrimary = false, InputType inputPromptType = InputType.Keyboard)
	{
		string text = "";
		ReadOnlyCollection<BindingSource> bindings = action.Bindings;
		int num = 0;
		for (int i = 0; i < bindings.Count; i++)
		{
			BindingSource bindingSource = bindings[i];
			if (num > 0)
			{
				text += " , ";
			}
			InputType inputType = m_PlayerActions.GetInputType(bindingSource.BindingSourceType);
			if (inputType == InputType.Keyboard && inputPromptType == InputType.Keyboard)
			{
				text += m_glyphService.GetBindingsGlyph(bindingSource, inputType, m_PlayerActions.LastDeviceStyle);
				num++;
			}
			else if (inputType == InputType.Controller && inputPromptType == InputType.Controller)
			{
				text += m_glyphService.GetBindingsGlyph(bindingSource, inputType, m_PlayerActions.LastDeviceStyle);
				num++;
			}
			if (num > 0 && onlyPrimary)
			{
				break;
			}
		}
		return text;
	}

	private bool PlatformShouldOverrideTextSizeAndAlignment()
	{
		return false;
	}

	private string OverrideTextSizeAndAlignment(string text, int overrideGlyphSize, int overrideVerticalAlign)
	{
		if (PlatformShouldOverrideTextSizeAndAlignment())
		{
			text = $"<voffset={overrideVerticalAlign}>{text}</voffset>";
			return $"<size={overrideGlyphSize}%>{text}</size>";
		}
		return text;
	}
}
