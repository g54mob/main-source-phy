using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InControl;
using Landfall.TABS.GameState;
using TFBGames;
using TMPro;
using UnityEngine;

namespace Landfall.TABS_Input
{
	public class TABSTutorial : GameStateListener
	{
		public const string TUTORIAL_KEY = "CompletedTutorial";

		public const string POSSESSION_TUTORIAL_KEY = "CompletedPossessionTutorial";

		private const int NOT_COMPLETE = 0;

		private const int COMPLETE = 1;

		private PlayerActions m_PlayerActions;

		private Dictionary<TutorialSteps, TutorialStep> m_TutorialSteps;

		private TextMeshProUGUI m_TutorialStepsText;

		private TutorialSteps m_CurrentTip;

		private bool m_HasStarted;

		public CodeAnimation anim;

		private GlyphService m_glyphService;

		private IPlayerPrefsPlatform m_PlayerPrefs;

		private InputService m_inputService;

		private float counter;

		protected override void Awake()
		{
			base.Awake();
			m_PlayerPrefs = ServiceLocator.GetService<IPlayerPrefsPlatform>();
			bool flag = m_PlayerPrefs.GetInt("CompletedTutorial", 0) == 1;
			bool flag2 = m_PlayerPrefs.GetInt("CompletedPossessionTutorial", 0) == 1;
			if (flag && flag2)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else if (flag)
			{
				m_CurrentTip = TutorialSteps.Look;
			}
		}

		private void Start()
		{
			m_TutorialStepsText = GetComponentInChildren<TextMeshProUGUI>();
			m_glyphService = ServiceLocator.GetService<GlyphService>();
			Init();
		}

		private void Init()
		{
			CameraAbilityPossess cameraAbilityPossess = UnityEngine.Object.FindObjectOfType<CameraAbilityPossess>();
			m_PlayerActions = PlayerActions.Instance;
			m_TutorialSteps = new Dictionary<TutorialSteps, TutorialStep>();
			TutorialSteps[] array = (TutorialSteps[])Enum.GetValues(typeof(TutorialSteps));
			foreach (TutorialSteps tutorialSteps in array)
			{
				switch (tutorialSteps)
				{
				case TutorialSteps.Movement:
				{
					PlayerAction[] bindings = new PlayerAction[4] { m_PlayerActions.m_moveForward, m_PlayerActions.m_moveBackward, m_PlayerActions.m_moveLeft, m_PlayerActions.m_moveRight };
					m_TutorialSteps.Add(TutorialSteps.Movement, new TutorialStep(bindings, tutorialSteps));
					break;
				}
				case TutorialSteps.UpDown:
				{
					PlayerAction[] bindings = new PlayerAction[2] { m_PlayerActions.m_moveUp, m_PlayerActions.m_moveDown };
					m_TutorialSteps.Add(TutorialSteps.UpDown, new TutorialStep(bindings, tutorialSteps));
					break;
				}
				case TutorialSteps.Look:
				{
					PlayerAction[] bindings = new PlayerAction[4] { m_PlayerActions.m_aimDown, m_PlayerActions.m_aimUp, m_PlayerActions.m_aimLeft, m_PlayerActions.m_aimRight };
					m_TutorialSteps.Add(TutorialSteps.Look, new TutorialStep(bindings, tutorialSteps));
					break;
				}
				case TutorialSteps.Possession_Enter:
					m_TutorialSteps.Add(tutorialSteps, new TutorialStep(cameraAbilityPossess.AddOnPossessEnterAction, tutorialSteps));
					break;
				case TutorialSteps.Possession_Attack:
				{
					PlayerAction[] bindings = new PlayerAction[1] { m_PlayerActions.m_possessAttack1 };
					m_TutorialSteps.Add(tutorialSteps, new TutorialStep(bindings, tutorialSteps, TutorialSteps.Possession_Enter));
					break;
				}
				case TutorialSteps.Possession_SwitchCamera:
				{
					PlayerAction[] bindings = new PlayerAction[1] { m_PlayerActions.m_possessToggleCamera };
					m_TutorialSteps.Add(tutorialSteps, new TutorialStep(bindings, tutorialSteps, TutorialSteps.Possession_Enter));
					break;
				}
				case TutorialSteps.Possession_Exit:
					m_TutorialSteps.Add(tutorialSteps, new TutorialStep(cameraAbilityPossess.AddOnPossessExitAction, tutorialSteps, TutorialSteps.Possession_Enter));
					break;
				}
			}
		}

		private void Update()
		{
			CheckForInput();
		}

		private void CheckForInput()
		{
			if (!m_HasStarted)
			{
				return;
			}
			foreach (KeyValuePair<TutorialSteps, TutorialStep> tutorialStep in m_TutorialSteps)
			{
				tutorialStep.Value.Check(m_TutorialSteps);
			}
			if (m_TutorialSteps.ContainsKey(m_CurrentTip) && m_TutorialSteps[m_CurrentTip].Completed)
			{
				IncrementTip();
				NextTip();
			}
		}

		public override void OnEnterPlacementState()
		{
		}

		public override void OnEnterBattleState()
		{
			StartTutorial();
		}

		private void StartTutorial()
		{
			Debug.Log("Start Tutorial!");
			m_HasStarted = true;
			StartCoroutine(DelayStart());
		}

		private IEnumerator DelayStart()
		{
			IncrementTip();
			yield return new WaitForSeconds(5f);
			if ((int)m_CurrentTip < Enum.GetValues(typeof(TutorialSteps)).Length)
			{
				anim.PlayIn();
				NextTip();
			}
		}

		private void IncrementTip()
		{
			m_CurrentTip++;
			if ((int)m_CurrentTip >= Enum.GetValues(typeof(TutorialSteps)).Length)
			{
				CompleteTutorial();
			}
			else if (m_TutorialSteps[m_CurrentTip].Completed)
			{
				IncrementTip();
			}
		}

		private void NextTip()
		{
			if ((int)m_CurrentTip >= Enum.GetValues(typeof(TutorialSteps)).Length)
			{
				return;
			}
			Debug.Log("Next Tip! Current: " + m_CurrentTip);
			string text = "Use: [";
			switch (m_CurrentTip)
			{
			case TutorialSteps.Movement:
				if (m_PlayerActions.InputType == InputType.Keyboard)
				{
					text += GetBindings(m_PlayerActions.m_moveForward, onlyPrimary: true);
					text += ",";
					text += GetBindings(m_PlayerActions.m_moveLeft, onlyPrimary: true);
					text += ",";
					text += GetBindings(m_PlayerActions.m_moveBackward, onlyPrimary: true);
					text += ",";
					text += GetBindings(m_PlayerActions.m_moveRight, onlyPrimary: true);
				}
				if (m_PlayerActions.InputType == InputType.Controller)
				{
					text += GetBindings(m_PlayerActions.m_moveForward, onlyPrimary: true, m_PlayerActions.InputType);
				}
				text += "] to move";
				break;
			case TutorialSteps.UpDown:
				text += GetBindings(m_PlayerActions.m_moveUp, onlyPrimary: true, m_PlayerActions.InputType);
				text += ",";
				text += GetBindings(m_PlayerActions.m_moveDown, onlyPrimary: true, m_PlayerActions.InputType);
				text += "] to move up and down";
				break;
			case TutorialSteps.Look:
				if (m_PlayerActions.InputType == InputType.Keyboard)
				{
					text += "MOUSE] to look around";
				}
				else if (m_PlayerActions.InputType == InputType.Controller)
				{
					text += GetBindings(m_PlayerActions.m_aimUp, onlyPrimary: true, m_PlayerActions.InputType);
					text += "] to look around";
				}
				break;
			case TutorialSteps.Possession_Enter:
				text = text + GetBindings(m_PlayerActions.m_possessToggle, onlyPrimary: true, m_PlayerActions.InputType) + "] to possess a unit";
				break;
			case TutorialSteps.Possession_Attack:
				text = text + GetBindings(m_PlayerActions.m_possessAttack1, onlyPrimary: true, m_PlayerActions.InputType) + "] to attack with a unit";
				break;
			case TutorialSteps.Possession_SwitchCamera:
				text = text + GetBindings(m_PlayerActions.m_possessToggleCamera, onlyPrimary: true, m_PlayerActions.InputType) + "] to switch camera mode";
				break;
			case TutorialSteps.Possession_Exit:
				text = text + GetBindings(m_PlayerActions.m_possessToggle, onlyPrimary: true, m_PlayerActions.InputType) + "] again to exit a unit";
				break;
			default:
				Debug.LogError(string.Concat("Tutorial Setp: ", m_CurrentTip, " Is Not setup!"));
				return;
			}
			if (m_CurrentTip != TutorialSteps.Movement)
			{
				anim.PlayBoop();
			}
			m_TutorialStepsText.text = text;
		}

		private void CompleteTutorial()
		{
			m_PlayerPrefs.SetInt("CompletedTutorial", 1);
			m_PlayerPrefs.SetInt("CompletedPossessionTutorial", 1);
			if (anim.currentState != CodeAnimationInstance.AnimationUse.Out)
			{
				anim.PlayOut();
			}
			StartCoroutine(DelayDestroy());
		}

		private IEnumerator DelayDestroy()
		{
			yield return new WaitForSeconds(1f);
			UnityEngine.Object.Destroy(base.gameObject);
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

		private void OnEnable()
		{
			m_inputService = ServiceLocator.GetService<InputService>();
			m_inputService.InputChanged += OnInputChange;
			m_inputService.InputDeviceStyleChanged += OnInputStyleChanged;
		}

		private void OnDisable()
		{
			if (m_inputService != null)
			{
				m_inputService.InputChanged -= OnInputChange;
				m_inputService.InputDeviceStyleChanged -= OnInputStyleChanged;
			}
		}

		private void OnInputChange(InputType bindingSourceType)
		{
			NextTip();
		}

		private void OnInputStyleChanged(InputDeviceStyle deviceStyle)
		{
			NextTip();
		}
	}
}
