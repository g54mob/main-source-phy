using System;
using Landfall.TABS.Workshop;
using UnityEngine;

namespace Landfall.TABS
{
	public class SandboxUIManager : InstancedHandler<SandboxUIManager>
	{
		public enum PlacementUIMode
		{
			PlacementMode = 0,
			SelectionMode = 1
		}

		[SerializeField]
		private PlacementUIMode m_placementUIMode;

		private BattleCreatorSaveUI m_currentSaveLevelUI;

		public static PlacementUIMode UIMode
		{
			get
			{
				if (InstancedHandler<SandboxUIManager>.Instance == null)
				{
					return PlacementUIMode.PlacementMode;
				}
				return InstancedHandler<SandboxUIManager>.Instance.m_placementUIMode;
			}
			set
			{
				InstancedHandler<SandboxUIManager>.Instance.m_placementUIMode = value;
			}
		}

		public event Action<DatabaseID> UnitWhitelistClickAction;

		public event Action<DatabaseID> FactionWhitelistClickAction;

		public static void SetSaveLevelUI(BattleCreatorSaveUI saveLevelUI)
		{
			InstancedHandler<SandboxUIManager>.Instance.m_currentSaveLevelUI = saveLevelUI;
		}

		public static DatabaseID[] GetCurrentAllowedUnits()
		{
			return new DatabaseID[0];
		}

		public void ClearActions()
		{
			this.UnitWhitelistClickAction = null;
			this.FactionWhitelistClickAction = null;
		}

		public void DoOnWhitelistClickAction(DatabaseID id)
		{
			this.UnitWhitelistClickAction?.Invoke(id);
		}

		public void DoOnWhitelistFactionClickAction(DatabaseID id)
		{
			this.FactionWhitelistClickAction?.Invoke(id);
		}

		public void OnDestroy()
		{
			InstancedHandler<SandboxUIManager>.Instance = null;
		}
	}
}
