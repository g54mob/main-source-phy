using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Landfall.TABC
{
	public class ScoreBoardUI : MonoBehaviour
	{
		public enum TitleType
		{
			Damage = 0,
			DamageTaken = 1
		}

		public TextMeshProUGUI titleText;

		private ScoreBoardObject[] scoreObjects;

		public Populate populate;

		private List<UnitStatData> statData = new List<UnitStatData>();

		private TitleType titleType;

		private List<UnitData> myUnits;

		public void ToggleTitle()
		{
			if (titleType == TitleType.Damage)
			{
				titleText.text = "DAMAGE TAKEN";
				titleType = TitleType.DamageTaken;
			}
			else if (titleType == TitleType.DamageTaken)
			{
				titleText.text = "DAMAGE";
				titleType = TitleType.Damage;
			}
		}

		private void Start()
		{
			RoundHandler instance = RoundHandler.instance;
			instance.EnterBattleAction = (Action)Delegate.Combine(instance.EnterBattleAction, new Action(UnitsUpdated));
		}

		private void Update()
		{
			if (RoundHandler.instance.roundState == RoundHandler.RoundState.Battle)
			{
				UpdateData();
			}
			UpdateScoreboard();
		}

		private void UnitsUpdated()
		{
			myUnits = UnitHandler.instance.myUnitsOnBoard;
			if (scoreObjects != null)
			{
				for (int i = 0; i < scoreObjects.Length; i++)
				{
					UnityEngine.Object.Destroy(scoreObjects[i].gameObject);
				}
			}
			populate.times = myUnits.Count;
			scoreObjects = populate.DoPopulate<ScoreBoardObject>().ToArray();
			for (int j = 0; j < scoreObjects.Length; j++)
			{
				scoreObjects[j].Init(myUnits[j]);
			}
			UpdateData();
			UpdateScoreboard();
		}

		private void UpdateData()
		{
			statData.Clear();
			for (int i = 0; i < myUnits.Count; i++)
			{
				UnitStatData unitStatData = new UnitStatData();
				unitStatData.damageDealt = myUnits[i].damageDealt;
				unitStatData.damageTaken = myUnits[i].damageTaken;
				statData.Add(unitStatData);
			}
		}

		private void UpdateScoreboard()
		{
			if (statData == null)
			{
				return;
			}
			float num = 0f;
			for (int i = 0; i < statData.Count; i++)
			{
				float num2 = statData[i].damageDealt;
				if (titleType == TitleType.DamageTaken)
				{
					num2 = statData[i].damageTaken;
				}
				if (num2 > num)
				{
					num = num2;
				}
			}
			List<int> list = SortByUnitDamage(statData);
			for (int j = 0; j < statData.Count; j++)
			{
				if (j < scoreObjects.Length)
				{
					if (num == 0f)
					{
						scoreObjects[j].UpdateInfo(0f, 1f, -1);
					}
					else if (titleType == TitleType.Damage)
					{
						scoreObjects[j].UpdateInfo(statData[j].damageDealt, num, list[j]);
					}
					else
					{
						scoreObjects[j].UpdateInfo(statData[j].damageTaken, num, list[j]);
					}
				}
			}
		}

		private List<int> SortByUnitDamage(List<UnitStatData> units)
		{
			List<int> list = new List<int>();
			List<UnitStatData> list2 = new List<UnitStatData>();
			for (int i = 0; i < units.Count; i++)
			{
				list2.Add(units[i]);
			}
			if (titleType == TitleType.Damage)
			{
				list2.Sort((UnitStatData unitID1, UnitStatData unitID2) => unitID2.damageDealt.CompareTo(unitID1.damageDealt));
			}
			else
			{
				list2.Sort((UnitStatData unitID1, UnitStatData unitID2) => unitID2.damageTaken.CompareTo(unitID1.damageTaken));
			}
			for (int num = 0; num < units.Count; num++)
			{
				for (int num2 = 0; num2 < list2.Count; num2++)
				{
					if (units[num] == list2[num2])
					{
						list.Add(num2);
					}
				}
			}
			return list;
		}
	}
}
