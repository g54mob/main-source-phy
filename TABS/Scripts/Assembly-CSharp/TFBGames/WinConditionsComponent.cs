using System.Collections;
using GamepadUI.StateManager.Core;
using Landfall.TABS.GameMode;
using Landfall.TABS.Services;
using Landfall.TABS.UnitPlacement;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TFBGames
{
	public class WinConditionsComponent : UIComponent
	{
		[SerializeField]
		private CodeAnimation[] m_SideVictoryPannelAnimators;

		[SerializeField]
		private CodeAnimation m_BackButtonAnimator;

		[SerializeField]
		private WinConditionContentInspector m_WinConditionPanelComponent;

		[SerializeField]
		private DMWinConditionsBrowser m_WinConditionBrowserPanelComponent;

		[SerializeField]
		private WinConditionsTeamPanel m_RedTeamPanelConditions;

		[SerializeField]
		private WinConditionsTeamPanel m_BlueTeamPanelConditions;

		[SerializeField]
		private Button m_BackButtonBrowseConditions;

		[SerializeField]
		private Button m_BackButtonEditUnitCondition;

		[SerializeField]
		private Button m_RootBackButton;

		[SerializeField]
		private CodeAnimation m_AdditionalSettingsCogButtonAnimator;

		[SerializeField]
		private GameObject m_UnitSelectPrompt;

		private WinConditionsTeamPanel m_CurrentTeamPanelConditions;

		private PlayerActions m_PlayerActions;

		public bool m_IsSelectingUnit;

		private UnitPlacementBrush m_UnitPlacementBrush;

		private InputService m_InputService;

		private ITimeService m_timeService;

		public bool m_animateMainPanels = true;

		public bool AutoHighlightListItem => autoHighlightSelectableWhenActive;

		public bool VictoryConditionsPanelIsOpen
		{
			get
			{
				if (m_WinConditionPanelComponent != null)
				{
					return m_WinConditionPanelComponent.IsOpen;
				}
				return false;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			m_InputService = ServiceLocator.GetService<InputService>();
			m_PlayerActions = PlayerActions.Instance;
			m_CurrentTeamPanelConditions = m_RedTeamPanelConditions;
			GameModeService service = ServiceLocator.GetService<GameModeService>();
			if (service != null)
			{
				m_UnitPlacementBrush = service.CurrentGameMode?.Brush;
			}
			m_timeService = ServiceLocator.GetService<ITimeService>();
		}

		protected override void Update()
		{
			base.Update();
			HandleGamepadInput();
		}

		public void PointerEnterRed()
		{
			if (!((m_WinConditionPanelComponent.IsOpen || m_IsSelectingUnit) & (m_CurrentTeamPanelConditions != null)))
			{
				SetActiveTeam(m_RedTeamPanelConditions);
			}
		}

		public void PointerEnterBlue()
		{
			if (!((m_WinConditionPanelComponent.IsOpen || m_IsSelectingUnit) & (m_CurrentTeamPanelConditions != null)))
			{
				SetActiveTeam(m_BlueTeamPanelConditions);
			}
		}

		public void OpenConditionBrowserPanel(bool setOpen)
		{
			if (!(m_WinConditionBrowserPanelComponent == null) && !(m_CurrentTeamPanelConditions == null) && !(m_BackButtonAnimator == null) && m_WinConditionBrowserPanelComponent.IsOpen != setOpen)
			{
				m_WinConditionBrowserPanelComponent.GetComponent<DMWinConditionsBrowser>().SetTeam(m_RedTeamPanelConditions.IsFocused);
				if (m_WinConditionBrowserPanelComponent.IsOpen)
				{
					m_WinConditionBrowserPanelComponent.Close();
					ToggleSidePannelAnimatorsOpen(shouldOpen: true);
					m_CurrentTeamPanelConditions.Focused(paused: true);
					m_BackButtonAnimator.PlayIn();
				}
				else
				{
					m_WinConditionBrowserPanelComponent.Open();
					ToggleSidePannelAnimatorsOpen(shouldOpen: false);
					m_CurrentTeamPanelConditions.Focused(paused: false);
					m_BackButtonAnimator.PlayOut();
				}
			}
		}

		public void OpenVictoryConditionPanel(bool setOpen)
		{
			if (!(m_WinConditionPanelComponent == null) && !(m_CurrentTeamPanelConditions == null) && !(m_BackButtonAnimator == null) && m_WinConditionPanelComponent.IsOpen != setOpen)
			{
				if (m_WinConditionPanelComponent.IsOpen)
				{
					m_WinConditionPanelComponent.Close();
					m_CurrentTeamPanelConditions.Focused(paused: true);
					ToggleSidePannelAnimatorsOpen(shouldOpen: true);
					m_BackButtonAnimator.PlayIn();
				}
				else
				{
					m_WinConditionPanelComponent.Open();
					m_CurrentTeamPanelConditions.Focused(paused: false);
					ToggleSidePannelAnimatorsOpen(shouldOpen: false);
					m_BackButtonAnimator.PlayOut();
				}
			}
		}

		public void HideForUnitSelect()
		{
			StartCoroutine(Delay());
			IEnumerator Delay()
			{
				yield return null;
				m_IsSelectingUnit = true;
				m_animateMainPanels = false;
				OpenVictoryConditionPanel(setOpen: false);
				m_animateMainPanels = true;
				m_CurrentTeamPanelConditions.Focused(paused: false);
				UpdateBackButtons();
				if (m_UnitSelectPrompt != null)
				{
					m_UnitSelectPrompt.SetActive(value: true);
				}
				m_timeService.UnPause();
				UIScreenInputBlocker.SetBlockCameraMovement(blockCameraMovement: false);
			}
		}

		public void ShowAfterUnitSelected()
		{
			StartCoroutine(Delay());
			IEnumerator Delay()
			{
				yield return null;
				m_IsSelectingUnit = false;
				m_animateMainPanels = false;
				OpenVictoryConditionPanel(setOpen: true);
				m_animateMainPanels = true;
				m_CurrentTeamPanelConditions.ReOpenBrowser();
				UpdateBackButtons();
				if (m_UnitSelectPrompt != null)
				{
					m_UnitSelectPrompt.SetActive(value: false);
				}
				m_timeService.Pause();
				UIScreenInputBlocker.SetBlockCameraMovement(blockCameraMovement: true);
			}
		}

		protected override void OnOpen()
		{
			base.OnOpen();
			if (m_InputService != null)
			{
				m_InputService.InputChanged += OnInputTypeChanged;
				if (m_PlayerActions != null)
				{
					OnInputTypeChanged(m_PlayerActions.InputType);
				}
			}
			if (m_timeService.IsPaused())
			{
				m_timeService.UnPause();
			}
			if (m_BackButtonAnimator != null)
			{
				m_BackButtonAnimator.PlayIn();
				UpdateBackButtons();
			}
			m_RedTeamPanelConditions.UpdateWinConditionsList();
			m_BlueTeamPanelConditions.UpdateWinConditionsList();
			ToggleSidePannelAnimatorsOpen(shouldOpen: true);
			m_BlueTeamPanelConditions.SetAsActiveTeam(isActiveTeam: false);
			m_RedTeamPanelConditions.SetAsActiveTeam(isActiveTeam: true);
			m_CurrentTeamPanelConditions = m_RedTeamPanelConditions;
			m_UnitPlacementBrush.ShouldUpdate(update: false);
			if (m_AdditionalSettingsCogButtonAnimator != null)
			{
				m_AdditionalSettingsCogButtonAnimator.PlayOut();
			}
		}

		protected override void OnClose()
		{
			base.OnClose();
			if (m_InputService != null)
			{
				m_InputService.InputChanged -= OnInputTypeChanged;
			}
			if (m_BackButtonAnimator != null)
			{
				m_BackButtonAnimator.PlayOut();
			}
			ToggleSidePannelAnimatorsOpen(shouldOpen: false);
			OpenVictoryConditionPanel(setOpen: false);
			m_CurrentTeamPanelConditions.SetAsActiveTeam(isActiveTeam: false);
			m_UnitPlacementBrush.ShouldUpdate(update: true);
			UIScreenInputBlocker.SetBlockCameraMovement(blockCameraMovement: false);
			m_timeService.UnPause();
			if (m_AdditionalSettingsCogButtonAnimator != null)
			{
				m_AdditionalSettingsCogButtonAnimator.PlayIn();
			}
		}

		private void ToggleSidePannelAnimatorsOpen(bool shouldOpen)
		{
			if (m_SideVictoryPannelAnimators == null || !m_animateMainPanels)
			{
				return;
			}
			CodeAnimation[] sideVictoryPannelAnimators = m_SideVictoryPannelAnimators;
			foreach (CodeAnimation codeAnimation in sideVictoryPannelAnimators)
			{
				if (!(codeAnimation == null))
				{
					if (shouldOpen && codeAnimation.currentState == CodeAnimationInstance.AnimationUse.Out)
					{
						codeAnimation.PlayIn();
						UpdateBackButtons();
					}
					else if (!shouldOpen && codeAnimation.currentState == CodeAnimationInstance.AnimationUse.In)
					{
						codeAnimation.PlayOut();
					}
				}
			}
		}

		private void HandleGamepadInput()
		{
			if (m_PlayerActions == null || !base.IsActive || m_WinConditionPanelComponent.IsOpen || m_IsSelectingUnit || m_WinConditionBrowserPanelComponent.IsOpen)
			{
				return;
			}
			if (m_PlayerActions.m_back.WasPressed)
			{
				m_RootBackButton.onClick.Invoke();
			}
			if (m_CurrentTeamPanelConditions != null)
			{
				if (m_PlayerActions.m_uiLeft.WasPressed)
				{
					SetActiveTeam(m_RedTeamPanelConditions);
				}
				if (m_PlayerActions.m_uiRight.WasPressed)
				{
					SetActiveTeam(m_BlueTeamPanelConditions);
				}
			}
		}

		public void UpdateBackButtons()
		{
			if (m_BackButtonBrowseConditions != null)
			{
				m_BackButtonBrowseConditions.gameObject.SetActive(m_WinConditionBrowserPanelComponent.IsOpen);
			}
			if (m_BackButtonEditUnitCondition != null)
			{
				m_BackButtonEditUnitCondition.gameObject.SetActive(m_WinConditionPanelComponent.IsOpen || m_IsSelectingUnit);
			}
			if (m_RootBackButton != null)
			{
				m_RootBackButton.gameObject.SetActive(!m_WinConditionBrowserPanelComponent.IsOpen && !m_WinConditionPanelComponent.IsOpen);
			}
		}

		private void SetActiveTeam(WinConditionsTeamPanel setAsActiveTeamPanel)
		{
			m_CurrentTeamPanelConditions.SetAsActiveTeam(isActiveTeam: false);
			m_CurrentTeamPanelConditions = setAsActiveTeamPanel;
			m_CurrentTeamPanelConditions.SetAsActiveTeam(isActiveTeam: true);
		}

		private void OnInputTypeChanged(InputType inputType)
		{
			switch (inputType)
			{
			case InputType.Controller:
				autoHighlightSelectableWhenActive = true;
				break;
			case InputType.Keyboard:
			case InputType.Any:
				autoHighlightSelectableWhenActive = false;
				EventSystem.current.SetSelectedGameObject(null);
				break;
			}
		}
	}
}
