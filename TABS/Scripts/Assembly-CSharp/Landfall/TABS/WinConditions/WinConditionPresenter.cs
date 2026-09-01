using System;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using UnityEngine;

namespace Landfall.TABS.WinConditions
{
	public class WinConditionPresenter : GameStateListener
	{
		[SerializeField]
		private EscapeMenuComponent m_EscapeMenuComponent;

		private Transform m_redWinConditions;

		private Transform m_blueWinConditions;

		private CodeAnimation m_redAnimation;

		private CodeAnimation m_blueAnimation;

		private WinConObjectivesUI m_redWinCons;

		private WinConObjectivesUI m_blueWinCons;

		private WinConditionPropagator m_winConditionPropagator;

		private BaseGameMode m_currentGameMode;

		private bool m_hideBlueConditions;

		private bool m_isEscapeMenuOpen;

		private new void Awake()
		{
			base.Awake();
			m_redWinConditions = base.transform.Find("RedConditions");
			m_redAnimation = m_redWinConditions.GetComponent<CodeAnimation>();
			m_redWinCons = m_redWinConditions.GetComponent<WinConObjectivesUI>();
			m_blueWinConditions = base.transform.Find("BlueConditions");
			m_blueAnimation = m_blueWinConditions.GetComponent<CodeAnimation>();
			m_blueWinCons = m_blueWinConditions.GetComponent<WinConObjectivesUI>();
			m_currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
			m_winConditionPropagator = m_currentGameMode.WinConditionPropagator;
			BaseGameMode currentGameMode = m_currentGameMode;
			currentGameMode.OnDonePlacingUnitsCallback = (BaseGameMode.OnDonePlacingAllUnitsDelegate)Delegate.Combine(currentGameMode.OnDonePlacingUnitsCallback, new BaseGameMode.OnDonePlacingAllUnitsDelegate(OnDonePlacingAllUnits));
			BaseGameMode currentGameMode2 = m_currentGameMode;
			currentGameMode2.OnUnitRemovedCallback = (BaseGameMode.OnUnitRemovedDelegate)Delegate.Combine(currentGameMode2.OnUnitRemovedCallback, new BaseGameMode.OnUnitRemovedDelegate(OnUnitRemoved));
			m_hideBlueConditions = m_currentGameMode.GetType() == typeof(CampaignGameMode);
			if (m_hideBlueConditions)
			{
				m_blueWinConditions.gameObject.SetActive(value: false);
			}
			ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_FLIP_COLORS").OnValueChanged += OnTeamColorSettingChanged;
			Localizer.RegisterCallback(this, OnLanguageChanged);
		}

		private void OnEnable()
		{
			EscapeMenuComponent escapeMenuComponent = m_EscapeMenuComponent;
			escapeMenuComponent.OpenMenu = (Action)Delegate.Combine(escapeMenuComponent.OpenMenu, new Action(OnEscapeMenuOpen));
			EscapeMenuComponent escapeMenuComponent2 = m_EscapeMenuComponent;
			escapeMenuComponent2.CloseMenu = (Action)Delegate.Combine(escapeMenuComponent2.CloseMenu, new Action(OnEscapeMenuClose));
		}

		private void OnDisable()
		{
			EscapeMenuComponent escapeMenuComponent = m_EscapeMenuComponent;
			escapeMenuComponent.OpenMenu = (Action)Delegate.Remove(escapeMenuComponent.OpenMenu, new Action(OnEscapeMenuOpen));
			EscapeMenuComponent escapeMenuComponent2 = m_EscapeMenuComponent;
			escapeMenuComponent2.CloseMenu = (Action)Delegate.Remove(escapeMenuComponent2.CloseMenu, new Action(OnEscapeMenuClose));
		}

		private void OnEscapeMenuOpen()
		{
			m_isEscapeMenuOpen = true;
		}

		private void OnEscapeMenuClose()
		{
			m_isEscapeMenuOpen = false;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			GlobalSettingsHandler service = ServiceLocator.GetService<GlobalSettingsHandler>();
			if (service == null)
			{
				return;
			}
			SettingsInstance settingsInstance = service.GetSettingsInstance("GAMEPLAY_FLIP_COLORS");
			if (settingsInstance != null)
			{
				settingsInstance.OnValueChanged -= OnTeamColorSettingChanged;
				if (m_currentGameMode != null)
				{
					BaseGameMode currentGameMode = m_currentGameMode;
					currentGameMode.OnDonePlacingUnitsCallback = (BaseGameMode.OnDonePlacingAllUnitsDelegate)Delegate.Remove(currentGameMode.OnDonePlacingUnitsCallback, new BaseGameMode.OnDonePlacingAllUnitsDelegate(OnDonePlacingAllUnits));
					BaseGameMode currentGameMode2 = m_currentGameMode;
					currentGameMode2.OnUnitRemovedCallback = (BaseGameMode.OnUnitRemovedDelegate)Delegate.Remove(currentGameMode2.OnUnitRemovedCallback, new BaseGameMode.OnUnitRemovedDelegate(OnUnitRemoved));
				}
				Localizer.UnregisterCallback(this);
			}
		}

		private void OnTeamColorSettingChanged(int newValue)
		{
			m_redWinCons.UpdateGUI();
			if (!m_hideBlueConditions)
			{
				m_blueWinCons.UpdateGUI();
			}
		}

		private void OnDonePlacingAllUnits()
		{
			if (!m_isEscapeMenuOpen && CanAnimate())
			{
				ShowConditions();
			}
		}

		private void OnUnitRemoved(Unit unit)
		{
			if (m_winConditionPropagator.IsUnitMustKill(unit, out var mustKillUnitWinCondition))
			{
				Team team = ((unit.Team != Team.Blue) ? Team.Blue : Team.Red);
				m_winConditionPropagator.RemoveWinCondition(team, mustKillUnitWinCondition);
				if (CanAnimate())
				{
					ShowConditions();
				}
			}
		}

		public override void OnEnterPlacementState()
		{
		}

		public override void OnEnterBattleState()
		{
			if (CanAnimate())
			{
				HideConditions();
			}
		}

		public void ShowConditions()
		{
			if (base.GameStateManager.GameState != Landfall.TABS.GameState.GameState.BattleState && !m_currentGameMode.IsInFreeLook)
			{
				m_redWinCons.UpdateGUI();
				m_redAnimation.PlayIn();
				if (!m_hideBlueConditions)
				{
					m_blueWinCons.UpdateGUI();
					m_blueAnimation.PlayIn();
				}
			}
		}

		public void HideConditions()
		{
			m_redAnimation.PlayOut();
			if (!m_hideBlueConditions)
			{
				m_blueAnimation.PlayOut();
			}
		}

		private bool CanAnimate()
		{
			if (m_redAnimation != null)
			{
				return m_blueAnimation != null;
			}
			return false;
		}

		private void OnLanguageChanged()
		{
			m_redWinCons.UpdateGUI();
			m_blueWinCons.UpdateGUI();
		}
	}
}
