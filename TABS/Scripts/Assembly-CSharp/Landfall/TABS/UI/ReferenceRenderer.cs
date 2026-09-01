using System;
using System.Collections.Generic;
using Landfall.TABS.AI.Systems;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS.WinConditions;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Landfall.TABS.UI
{
	public class ReferenceRenderer : GameStateListener
	{
		[SerializeField]
		private WinConTargetUIPreset m_attackPreset;

		[SerializeField]
		private WinConTargetUIPreset m_defendPreset;

		[SerializeField]
		private GameObject m_unitIconPrefab;

		[SerializeField]
		[FormerlySerializedAs("m_redUnitColor")]
		private Color m_redOutlineColor;

		[SerializeField]
		private Color m_redUIColor;

		[SerializeField]
		[FormerlySerializedAs("m_blueUnitColor")]
		private Color m_blueOutlineColor;

		[SerializeField]
		private Color m_blueUIColor;

		private RuntimeReferenceService m_referenceService;

		private TeamSystem m_teamSystem;

		private List<RuntimeReference> m_iconedUnits = new List<RuntimeReference>();

		private List<GameObject> m_iconInstances = new List<GameObject>();

		private BaseGameMode m_gameMode;

		private SettingsInstance m_flipColorSettings;

		private WinConditionPropagator m_WindConditionPropagator;

		private void Start()
		{
			m_referenceService = ServiceLocator.GetService<RuntimeReferenceService>();
			m_teamSystem = World.Active.GetOrCreateManager<TeamSystem>();
			BaseGameMode baseGameMode = (m_gameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode);
			baseGameMode.OnUnitSpawnedCallback = (BaseGameMode.OnUnitSpawnedDelegate)Delegate.Combine(baseGameMode.OnUnitSpawnedCallback, new BaseGameMode.OnUnitSpawnedDelegate(OnUnitSpawned));
			baseGameMode.OnDonePlacingUnitsCallback = (BaseGameMode.OnDonePlacingAllUnitsDelegate)Delegate.Combine(baseGameMode.OnDonePlacingUnitsCallback, new BaseGameMode.OnDonePlacingAllUnitsDelegate(OnDonePlacingAllUnits));
			m_flipColorSettings = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_FLIP_COLORS");
			m_flipColorSettings.OnValueChanged += OnFlipColorSettingChanged;
			RuntimeReferenceService service = ServiceLocator.GetService<RuntimeReferenceService>();
			service.OnReleasedReferenceCallback = (RuntimeReferenceService.OnReleasedReferenceDelegate)Delegate.Combine(service.OnReleasedReferenceCallback, new RuntimeReferenceService.OnReleasedReferenceDelegate(OnReleasedReference));
			m_WindConditionPropagator = ServiceLocator.GetService<GameModeService>().CurrentGameMode.WinConditionPropagator;
		}

		private void OnReleasedReference(RuntimeReference reference)
		{
			bool flag = false;
			for (int i = 0; i < m_iconedUnits.Count; i++)
			{
				if (m_iconedUnits[i].Guid == reference.Guid)
				{
					m_iconedUnits.RemoveAt(i);
					flag = true;
					ServiceLocator.GetService<RuntimeReferenceService>().GetReferenceTarget<Unit>(reference).RemoveHighlight();
					break;
				}
			}
			if (flag)
			{
				OnDonePlacingAllUnits();
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			RuntimeReferenceService service = ServiceLocator.GetService<RuntimeReferenceService>();
			if (service != null)
			{
				service.OnReleasedReferenceCallback = (RuntimeReferenceService.OnReleasedReferenceDelegate)Delegate.Remove(service.OnReleasedReferenceCallback, new RuntimeReferenceService.OnReleasedReferenceDelegate(OnReleasedReference));
			}
			GlobalSettingsHandler service2 = ServiceLocator.GetService<GlobalSettingsHandler>();
			if (!(service2 == null))
			{
				m_flipColorSettings = service2.GetSettingsInstance("GAMEPLAY_FLIP_COLORS");
				if (m_flipColorSettings != null)
				{
					m_flipColorSettings.OnValueChanged -= OnFlipColorSettingChanged;
				}
			}
		}

		private void OnFlipColorSettingChanged(int newValue)
		{
			ClearIcons();
			SpawnIcons();
		}

		private void OnDonePlacingAllUnits()
		{
			ClearIcons();
			m_iconInstances.Clear();
			SpawnIcons();
		}

		public override void OnEnterPlacementState()
		{
		}

		public override void OnEnterBattleState()
		{
		}

		private void ClearIcons()
		{
			for (int i = 0; i < m_iconInstances.Count; i++)
			{
				UnityEngine.Object.Destroy(m_iconInstances[i]);
			}
			m_iconInstances.Clear();
		}

		private void SpawnIcons()
		{
			RuntimeReferenceService service = ServiceLocator.GetService<RuntimeReferenceService>();
			List<RuntimeReference> list = new List<RuntimeReference>();
			foreach (RuntimeReference allReferencesRequest in service.GetAllReferencesRequests())
			{
				Unit referenceTarget = service.GetReferenceTarget<Unit>(allReferencesRequest);
				if (!(referenceTarget == null) && !list.Contains(allReferencesRequest))
				{
					list.Add(allReferencesRequest);
					if (CheckUnitReferenceIsValid(referenceTarget))
					{
						SetIcon(referenceTarget);
					}
				}
			}
		}

		private void OnUnitSpawned(Unit unit)
		{
			for (int i = 0; i < m_iconedUnits.Count; i++)
			{
				if (m_iconedUnits[i].Guid == unit.RuntimeReference.Guid && CheckUnitReferenceIsValid(unit))
				{
					SetIcon(unit);
				}
			}
		}

		public void AddIconedUnit(ReferenceRequest<Unit> unitReference)
		{
			m_iconedUnits.Add(unitReference);
			foreach (Unit allUnit in m_teamSystem.GetAllUnits())
			{
				if (allUnit.RuntimeReference != null)
				{
					_ = allUnit.RuntimeReference.Guid;
					if (allUnit.RuntimeReference.Guid == unitReference.Guid)
					{
						SetIcon(allUnit);
					}
				}
			}
		}

		public void RemoveIconedUnit(ReferenceRequest<Unit> unitReference)
		{
			Unit referenceTarget = ServiceLocator.GetService<RuntimeReferenceService>().GetReferenceTarget<Unit>(unitReference);
			if (!(referenceTarget == null))
			{
				referenceTarget.RemoveHighlight();
				WinConTargetUI componentInChildren = referenceTarget.gameObject.GetComponentInChildren<WinConTargetUI>();
				m_iconInstances.Remove(componentInChildren.gameObject);
				m_iconedUnits.Remove(unitReference);
				UnityEngine.Object.Destroy(componentInChildren.gameObject);
			}
		}

		private void SetIcon(Unit unit)
		{
			if (unit.GetComponent<WinConTargetUI>() != null)
			{
				return;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate(m_unitIconPrefab);
			WinConTargetUI component = gameObject.GetComponent<WinConTargetUI>();
			WinConTargetUIPreset preset = m_attackPreset;
			Color white = Color.white;
			Color white2 = Color.white;
			if (unit.Team == Team.Red)
			{
				white = m_redOutlineColor;
				white2 = m_redUIColor;
				if (m_flipColorSettings.currentValue == 1)
				{
					white = m_blueOutlineColor;
					white2 = m_blueUIColor;
				}
				if (m_gameMode.GetType() == typeof(CampaignGameMode))
				{
					preset = m_defendPreset;
				}
			}
			else
			{
				white = m_blueOutlineColor;
				white2 = m_blueUIColor;
				if (m_flipColorSettings.currentValue == 1)
				{
					white = m_redOutlineColor;
					white2 = m_redUIColor;
				}
			}
			component.Setup(preset, unit, white, white2);
			gameObject.transform.SetParent(unit.transform, worldPositionStays: false);
			m_iconInstances.Add(gameObject);
		}

		private bool CheckUnitReferenceIsValid(Unit unit)
		{
			Team team = ((unit.Team == Team.Red) ? Team.Blue : Team.Red);
			WinCondition[] winConditionsForTeam = m_WindConditionPropagator.GetWinConditionsForTeam(team);
			foreach (WinCondition winCondition in winConditionsForTeam)
			{
				Unit referenceTarget = m_referenceService.GetReferenceTarget<Unit>(winCondition.GetUnitToKill());
				if (!(referenceTarget == null) && referenceTarget == unit)
				{
					return true;
				}
			}
			return false;
		}
	}
}
