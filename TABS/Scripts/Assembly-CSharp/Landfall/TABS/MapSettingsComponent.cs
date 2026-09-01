using System;
using GamepadUI.StateManager.Core;
using Landfall.TABS.GameMode;
using Landfall.TABS.TeamEdge;
using Landfall.TABS.UnitPlacement;
using Landfall.TABS_Input;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class MapSettingsComponent : UIComponent
	{
		private const float SwitchGlyphSizeOverride = 150f;

		public TextMeshProUGUI helpText;

		[SerializeField]
		private Button m_editLineButton;

		[SerializeField]
		private Button m_swapSidesButton;

		[SerializeField]
		private Button m_cycleLineTypeButton;

		[SerializeField]
		private Button m_backButton;

		[SerializeField]
		private Button m_resetTeamLine;

		[SerializeField]
		private Image m_resetCircleFill;

		[SerializeField]
		private CodeAnimation m_AdditionalSettingsCogButtonAnimator;

		private CodeAnimation m_animationHandler;

		private InputService m_inputService;

		private GlyphService m_iconService;

		private SceneSettings m_sceneSettings;

		private PlayerActions m_playerActions;

		private BaseGameMode m_currentGameMode;

		private UnitPlacementBrush m_PlacementBrush;

		private bool m_hasEditedLine;

		private bool m_isLevelName;

		private bool m_isEditing;

		private bool holdingToResetLine;

		private float holdTimeToResetLine = 1f;

		private float timeSinceHoldResetLine;

		protected override void Awake()
		{
			base.Awake();
			m_animationHandler = GetComponent<CodeAnimation>();
			m_sceneSettings = UnityEngine.Object.FindObjectOfType<SceneSettings>();
			AddButtonOnClickListeners();
			m_playerActions = PlayerActions.Instance;
			m_inputService = ServiceLocator.GetService<InputService>();
			m_iconService = ServiceLocator.GetService<GlyphService>();
			m_currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
			m_PlacementBrush = m_currentGameMode.Brush;
		}

		protected override void Start()
		{
			base.Start();
			if (CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.Campaign)
			{
				m_isLevelName = true;
			}
		}

		private void AddButtonOnClickListeners()
		{
			if (m_editLineButton != null)
			{
				m_editLineButton.onClick.AddListener(EditTeamLine);
			}
			if (m_swapSidesButton != null)
			{
				m_swapSidesButton.onClick.AddListener(SwapTeamLineEdge);
			}
			if (m_cycleLineTypeButton != null)
			{
				m_cycleLineTypeButton.onClick.AddListener(CycleLineType);
			}
			if (m_resetTeamLine != null)
			{
				m_resetTeamLine.onClick.AddListener(ResetTeamLine);
			}
		}

		protected override void OnOpen()
		{
			base.OnOpen();
			m_inputService.OnUIOpen();
			m_animationHandler.PlayIn();
			if (m_currentGameMode.PlacementCamera == null)
			{
				m_currentGameMode.FindPlacementCamera();
			}
			if (m_currentGameMode.PlacementCamera != null)
			{
				m_currentGameMode.PlacementCamera.AllowMovement(allow: true);
			}
			m_hasEditedLine = false;
			if (m_AdditionalSettingsCogButtonAnimator != null)
			{
				m_AdditionalSettingsCogButtonAnimator.PlayOut();
			}
			if (m_inputService != null)
			{
				m_inputService.InputChanged += OnInputSourceChanged;
			}
		}

		protected override void OnClose()
		{
			base.OnClose();
			m_animationHandler.PlayOut();
			m_inputService.OnUIClose();
			m_currentGameMode.PlacementCamera.AllowMovement(m_hasEditedLine);
			if (m_AdditionalSettingsCogButtonAnimator != null)
			{
				m_AdditionalSettingsCogButtonAnimator.PlayIn();
			}
			if (m_inputService != null)
			{
				m_inputService.InputChanged -= OnInputSourceChanged;
			}
		}

		protected override void Update()
		{
			base.Update();
			HandleGamepadInput();
		}

		private void HandleGamepadInput()
		{
			if (m_playerActions == null)
			{
				return;
			}
			if (m_playerActions.m_editLine.WasPressed)
			{
				EditTeamLine();
			}
			if (m_playerActions.m_swapTeamLine.WasPressed)
			{
				SwapTeamLineEdge();
			}
			if (m_playerActions.m_cycleLineType.WasPressed)
			{
				CycleLineType();
			}
			if (m_playerActions.m_resetTeamLine.WasPressed)
			{
				StartTeamLineResetHold();
			}
			if (m_playerActions.m_resetTeamLine.IsPressed && holdingToResetLine)
			{
				timeSinceHoldResetLine += Time.deltaTime;
				if (timeSinceHoldResetLine > holdTimeToResetLine)
				{
					ResetTeamLine();
				}
				float circleFill = timeSinceHoldResetLine / holdTimeToResetLine;
				SetCircleFill(circleFill);
			}
			if (m_playerActions.m_resetTeamLine.WasReleased)
			{
				StopTeamLineResetHold();
			}
			if (m_playerActions != null && m_playerActions.m_back.WasPressed)
			{
				if (m_isEditing)
				{
					EditTeamLine();
				}
				else if (!(m_backButton == null))
				{
					m_backButton.onClick.Invoke();
					m_PlacementBrush.PreventPlacementBuffer();
				}
			}
		}

		private void StartTeamLineResetHold()
		{
			holdingToResetLine = true;
			timeSinceHoldResetLine = 0f;
			SetCircleFill(0f);
		}

		private void StopTeamLineResetHold()
		{
			holdingToResetLine = false;
			SetCircleFill(0f);
		}

		private void ResetTeamLine()
		{
			holdingToResetLine = false;
			m_sceneSettings.ResetToDefaultTeamLine();
			SetCircleFill(0f);
		}

		private void SetCircleFill(float amount)
		{
			if (m_resetCircleFill != null)
			{
				m_resetCircleFill.fillAmount = amount;
			}
		}

		private void EditTeamLine()
		{
			if (m_isEditing)
			{
				m_sceneSettings.StopLineEditing();
			}
			else
			{
				StartEditingTeamLine();
			}
			m_hasEditedLine = true;
		}

		private void CycleLineType()
		{
			switch (m_sceneSettings.m_EdgeType)
			{
			case EdgeType.Line:
				SetEdgeType(EdgeType.Circle.GetHashCode());
				break;
			case EdgeType.Circle:
				SetEdgeType(EdgeType.Line.GetHashCode());
				break;
			}
			m_hasEditedLine = true;
		}

		private void StartEditingTeamLine()
		{
			m_sceneSettings.StartLineEditing();
			if (helpText != null)
			{
				helpText.gameObject.SetActive(value: true);
				helpText.text = GetHelpText();
			}
			else
			{
				Debug.LogError("Map Settings help text is null");
			}
			m_isEditing = true;
			m_hasEditedLine = true;
		}

		public void StopEditing()
		{
			if (helpText != null)
			{
				helpText.gameObject.SetActive(value: false);
			}
			else
			{
				Debug.LogError("Map Settings help text is null");
			}
			m_isEditing = false;
		}

		private void SetEdgeType(int type)
		{
			if (m_sceneSettings.SetType((EdgeType)type))
			{
				EditTeamLine();
			}
		}

		private void SwapTeamLineEdge()
		{
			m_sceneSettings.SwapTeamEdge();
			m_hasEditedLine = true;
		}

		private string GetHelpText()
		{
			string text = "";
			string text2 = "";
			string actionGlyph = m_iconService.GetActionGlyph(PlayerActions.Instance.m_placementZoomOut, InputType.Controller, PlayerActions.Instance.LastDeviceStyle);
			string actionGlyph2 = m_iconService.GetActionGlyph(PlayerActions.Instance.m_placementZoomIn, InputType.Controller, PlayerActions.Instance.LastDeviceStyle);
			string actionGlyph3 = m_iconService.GetActionGlyph(PlayerActions.Instance.m_accept, InputType.Controller, PlayerActions.Instance.LastDeviceStyle);
			switch (m_sceneSettings.m_EdgeType)
			{
			case EdgeType.Line:
				text = Localizer.GetSinglePhrase("LABEL_LINE_KEYBOARD", Environment.NewLine);
				text2 = Localizer.GetSinglePhrase("LABEL_LINE_CONTROLLER", actionGlyph, actionGlyph2, Environment.NewLine, actionGlyph3);
				break;
			case EdgeType.Circle:
				text = Localizer.GetSinglePhrase("LABEL_CIRCLE_KEYBOARD", Environment.NewLine);
				text2 = Localizer.GetSinglePhrase("LABEL_CIRCLE_CONTROLLER", actionGlyph, actionGlyph2, Environment.NewLine, actionGlyph3);
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			if (PlayerActions.Instance.InputType != InputType.Controller)
			{
				return text;
			}
			return text2;
		}

		private void OnInputSourceChanged(InputType inputType)
		{
			if (helpText != null)
			{
				helpText.text = GetHelpText();
			}
		}
	}
}
