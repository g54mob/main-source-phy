using System;
using System.Collections.Generic;
using Landfall.TABS;
using UnityEngine;

namespace Landfall.TABC
{
	public class UnitCombinations : MonoBehaviour
	{
		public static UnitCombinations instance;

		public GameObject hpDebug;

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			RoundHandler roundHandler = RoundHandler.instance;
			roundHandler.EnterPostRoundActionLate = (Action)Delegate.Combine(roundHandler.EnterPostRoundActionLate, new Action(CheckForCombine));
		}

		public void CheckForCombine()
		{
			List<UnitCount> list = new List<UnitCount>();
			List<UnitData> myUnits = UnitHandler.instance.myUnits;
			for (int i = 0; i < myUnits.Count; i++)
			{
				if (!myUnits[i].isUnitButton && RoundHandler.instance.roundState == RoundHandler.RoundState.Battle)
				{
					continue;
				}
				bool flag = true;
				for (int j = 0; j < list.Count; j++)
				{
					if (IsSameUnitAndLevel(myUnits[i], list[j].unit))
					{
						list[j].numberOfUnits++;
						flag = false;
						if (list[j].numberOfUnits > 2)
						{
							Combine(myUnits[i]);
							break;
						}
					}
				}
				if (flag)
				{
					UnitCount item = new UnitCount(myUnits[i]);
					list.Add(item);
				}
			}
		}

		private void Combine(UnitData unitToCombine)
		{
			List<UnitData> list = new List<UnitData>();
			List<UnitData> myUnits = UnitHandler.instance.myUnits;
			for (int i = 0; i < myUnits.Count; i++)
			{
				if (IsSameUnitAndLevel(unitToCombine, myUnits[i]))
				{
					list.Add(myUnits[i]);
				}
			}
			int num = 0;
			for (int j = 0; j < list.Count; j++)
			{
				if (j != list.Count - 1 && j > num && list[j].isUnitButton)
				{
					num = j;
				}
			}
			for (int k = 0; k < list.Count; k++)
			{
				if (k != num)
				{
					UnitHandler.instance.RemoveUnit(list[k]);
				}
				else
				{
					list[k].LevelUp();
				}
			}
			UnitHandler.instance.UpdateUnits();
		}

		private bool IsSameUnitAndLevel(UnitData unit1, UnitData unit2)
		{
			if (!unit1 || !unit2 || unit1.dataInstance == null || unit2.dataInstance == null || !unit1.dataInstance.unit || !unit2.dataInstance.unit || !unit1.dataInstance.unit.unitBlueprint || !unit2.dataInstance.unit.unitBlueprint)
			{
				return false;
			}
			bool flag = unit1.dataInstance.unit.unitBlueprint == unit2.dataInstance.unit.unitBlueprint;
			return unit1.dataInstance.level == unit2.dataInstance.level && flag;
		}

		public void UpdateUnit(Unit unit, SimulatedUnitBlueprint bluep, int level)
		{
			int num = level - 1;
			WeaponHandler componentInChildren = unit.GetComponentInChildren<WeaponHandler>();
			Level level2 = null;
			Level level3 = null;
			if ((bool)componentInChildren)
			{
				if ((bool)componentInChildren.leftWeapon)
				{
					level3 = componentInChildren.leftWeapon.gameObject.FetchComponent<Level>();
					level3.ignoreTeam = true;
					level3.level = level;
				}
				if ((bool)componentInChildren.rightWeapon)
				{
					level2 = componentInChildren.rightWeapon.gameObject.FetchComponent<Level>();
					level2.ignoreTeam = true;
					level2.level = level;
				}
			}
			for (int i = 0; i < num; i++)
			{
				if ((bool)componentInChildren)
				{
					if ((bool)componentInChildren.leftWeapon)
					{
						componentInChildren.leftWeapon.internalCooldown *= 1f - bluep.attackSpeedPerLevel;
						componentInChildren.leftWeapon.levelMultiplier *= 1f + bluep.damagePerLevel;
					}
					if ((bool)componentInChildren.rightWeapon)
					{
						componentInChildren.rightWeapon.internalCooldown *= 1f - bluep.attackSpeedPerLevel;
						componentInChildren.rightWeapon.levelMultiplier *= 1f + bluep.damagePerLevel;
					}
					if ((bool)componentInChildren.leftWeapon)
					{
						level3.levelMultiplier *= 1f + bluep.damagePerLevel;
					}
					if ((bool)componentInChildren.rightWeapon)
					{
						level2.levelMultiplier *= 1f + bluep.damagePerLevel;
					}
				}
			}
			float num2 = 0f;
			num2 = (float)num * bluep.hpPerLevel + unit.unitBlueprint.health * (float)num * bluep.hpMultiplierPerLevel;
			unit.GetComponentInChildren<DataHandler>().maxHealth += num2;
			unit.GetComponentInChildren<DataHandler>().health += num2;
			unit.SetMassMultiplier(1f + num2 * 0.005f);
		}
	}
}
