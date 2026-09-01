using Landfall.TABS;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitUpgradeTree", menuName = "TABS/UnitUpgradeTree", order = 10)]
public class UnitUpgradeTree : ScriptableObject
{
	public UnitUpgrades[] upgrades;

	public UnitBlueprint[] GetUpgrades(UnitBlueprint unit)
	{
		for (int i = 0; i < upgrades.Length; i++)
		{
			if (unit == upgrades[i].unit)
			{
				return upgrades[i].upgrades;
			}
		}
		return null;
	}
}
