using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Landfall.TABS
{
	public class CastleFightHouse : MonoBehaviour
	{
		public Material redMaterial;

		public Material blueMaterial;

		public UnitSpawner spawner;

		public int maxUnits = 3;

		public int level = 1;

		public float totalSpentOnBuilding;

		public List<Unit> units = new List<Unit>();

		public GameObject hpDebug;

		public void Setup(Team team, UnitBlueprint unit)
		{
			spawner = base.transform.GetComponent<UnitSpawner>();
			spawner.unitBlueprint = unit;
			spawner.team = team;
			if (team == Team.Red)
			{
				GetComponentInChildren<Renderer>().sharedMaterial = redMaterial;
			}
			else
			{
				GetComponentInChildren<Renderer>().sharedMaterial = blueMaterial;
			}
			units.Add(spawner.Spawn());
		}

		public void TryToSpawn()
		{
			for (int num = units.Count - 1; num > -1; num--)
			{
				if (units[num] == null || ((bool)units[num] && units[num].dead))
				{
					units.RemoveAt(num);
				}
			}
			if (units.Count != maxUnits)
			{
				Unit unit = spawner.Spawn();
				units.Add(unit);
				DoUpgrades(unit);
			}
		}

		private void DoUpgrades(Unit unit)
		{
			float num = 0f;
			float num2 = 1f;
			int num3 = 1;
			float num4 = 1f;
			float num5 = 1f;
			if (level == 2)
			{
				num = 500f + spawner.unitBlueprint.health * 4f;
				num2 = 0.05f;
				num5 += 5f;
				num3 = 2;
				num4 = 2f;
			}
			unit.GetComponentInChildren<DataHandler>().maxHealth += num;
			unit.GetComponentInChildren<DataHandler>().health += num;
			unit.SetMassMultiplier(1f + num * 0.01f);
			WeaponHandler componentInChildren = unit.GetComponentInChildren<WeaponHandler>();
			if ((bool)componentInChildren)
			{
				if ((bool)componentInChildren.leftWeapon)
				{
					componentInChildren.leftWeapon.internalCooldown *= num2;
					componentInChildren.leftWeapon.levelMultiplier *= num4;
					componentInChildren.leftWeapon.gameObject.AddComponent<Level>().levelMultiplier = level;
					RangeWeapon component = componentInChildren.leftWeapon.GetComponent<RangeWeapon>();
					if ((bool)component)
					{
						component.numberOfObjects *= num3;
						component.spread += num5;
						component.shootRecoil /= num3;
						component.shootRecoil *= num2;
					}
				}
				if ((bool)componentInChildren.rightWeapon)
				{
					componentInChildren.rightWeapon.internalCooldown *= num2;
					componentInChildren.rightWeapon.levelMultiplier *= num4;
					componentInChildren.rightWeapon.gameObject.AddComponent<Level>().levelMultiplier = level;
					RangeWeapon component2 = componentInChildren.rightWeapon.GetComponent<RangeWeapon>();
					if ((bool)component2)
					{
						component2.numberOfObjects *= num3;
						component2.spread += num5;
						component2.shootRecoil /= num3;
						component2.shootRecoil *= num2;
					}
				}
			}
			if (level > 1)
			{
				GameObject obj = Object.Instantiate(hpDebug);
				obj.GetComponent<FollowTransform>().target = unit.GetComponentInChildren<Head>().transform;
				obj.GetComponentInChildren<TextMeshPro>().text = "Level " + level;
			}
		}
	}
}
