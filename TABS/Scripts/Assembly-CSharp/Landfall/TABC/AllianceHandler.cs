using System;
using System.Collections.Generic;
using UnityEngine;

namespace Landfall.TABC
{
	public class AllianceHandler : MonoBehaviour
	{
		public AllianceDatabase dataBase;

		public static AllianceHandler instance;

		private List<AllianceProgress> allianceProgress = new List<AllianceProgress>();

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			UnitHandler unitHandler = UnitHandler.instance;
			unitHandler.UpdateUnitsAction = (Action)Delegate.Combine(unitHandler.UpdateUnitsAction, new Action(UnitWereUpdated));
		}

		public void UnitWereUpdated()
		{
			this.allianceProgress.Clear();
			List<UnitData> myUnitsOnBoard = UnitHandler.instance.myUnitsOnBoard;
			List<SimulatedUnitBlueprint> list = new List<SimulatedUnitBlueprint>();
			for (int i = 0; i < myUnitsOnBoard.Count; i++)
			{
				if (list.Contains(myUnitsOnBoard[i].dataInstance.unit))
				{
					continue;
				}
				list.Add(myUnitsOnBoard[i].dataInstance.unit);
				Alliance[] alliances = myUnitsOnBoard[i].dataInstance.unit.alliances;
				for (int j = 0; j < alliances.Length; j++)
				{
					bool flag = false;
					for (int k = 0; k < this.allianceProgress.Count; k++)
					{
						if (this.allianceProgress[k].alliance == alliances[j])
						{
							this.allianceProgress[k].unlockedUnits++;
							flag = true;
						}
					}
					if (!flag)
					{
						AllianceProgress allianceProgress = new AllianceProgress();
						allianceProgress.alliance = alliances[j];
						allianceProgress.unlockedUnits = 1;
						this.allianceProgress.Add(allianceProgress);
					}
				}
			}
			for (int l = 0; l < this.allianceProgress.Count; l++)
			{
				this.allianceProgress[l].unlockedLevels = this.allianceProgress[l].alliance.GetUnlockedLevels(this.allianceProgress[l].unlockedUnits);
			}
			this.allianceProgress.Sort((AllianceProgress allianceProgress3, AllianceProgress allianceProgress2) => allianceProgress2.unlockedLevels.CompareTo(allianceProgress3.unlockedLevels));
			AllianceHandlerUI.instance.Populate(this.allianceProgress.ToArray());
		}

		public void ShowAllianceVisual(Alliance alliance)
		{
			List<UnitData> unitsWithAlliance = GetUnitsWithAlliance(alliance);
			for (int i = 0; i < unitsWithAlliance.Count; i++)
			{
				unitsWithAlliance[i].visuals.ShowAlliance(alliance);
			}
		}

		public void HideAllianceVisual()
		{
			for (int i = 0; i < UnitHandler.instance.myUnitsOnBoard.Count; i++)
			{
				UnitHandler.instance.myUnitsOnBoard[i].visuals.HideAlliance();
			}
		}

		private List<UnitData> GetUnitsWithAlliance(Alliance alliance)
		{
			List<UnitData> list = new List<UnitData>();
			UnitData[] array = UnitHandler.instance.myUnitsOnBoard.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				for (int j = 0; j < array[i].dataInstance.unit.alliances.Length; j++)
				{
					if (alliance == array[i].dataInstance.unit.alliances[j])
					{
						list.Add(array[i]);
					}
				}
			}
			return list;
		}
	}
}
