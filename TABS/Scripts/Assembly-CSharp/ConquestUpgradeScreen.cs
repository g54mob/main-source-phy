using Landfall.TABS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConquestUpgradeScreen : MonoBehaviour
{
	public UnitUpgradeTree upgrades;

	public ConquestPlayer player;

	public GameObject armyBar;

	public GameObject upgradeBar;

	public GameObject upgradePrefab;

	public GameObject unitButtonPrefab;

	public TextMeshProUGUI goldText;

	private UnitBlueprint unitBeingUpgraded;

	private void Start()
	{
		PopulateArmyBar();
	}

	public void ClickUpgradeButton(UnitBlueprint unit)
	{
		if (player.TryToUpgradeUnit(unitBeingUpgraded, unit))
		{
			PopulateArmyBar();
			if (!player.TryToUpgradeUnit(unitBeingUpgraded, unit, justChecking: true))
			{
				ConquestUserUI.ClearUnitBar(upgradeBar);
			}
		}
	}

	public void ClickArmyUnitToUpgrade(UnitBlueprint unit)
	{
		unitBeingUpgraded = unit;
		PopulateUpgradeBar(upgrades.GetUpgrades(unit));
	}

	public void PopulateArmyBar()
	{
		goldText.text = "YOUR GOLD: " + player.gold;
		ConquestUserUI.ClearUnitBar(armyBar);
		ConquestUserUI.Populate(unitButtonPrefab, player.units, armyBar);
		for (int i = 0; i < armyBar.transform.childCount; i++)
		{
			if (armyBar.transform.GetChild(i).gameObject.activeSelf)
			{
				Button button = armyBar.transform.GetChild(i).GetComponent<Button>();
				button.onClick.AddListener(delegate
				{
					ClickArmyUnitToUpgrade(button.GetComponent<ConquestUnitButton>().unit);
				});
				button.GetComponent<UIScaleJiggle>().enabled = true;
			}
		}
	}

	public void PopulateUpgradeBar(UnitBlueprint[] units)
	{
		if (units == null)
		{
			return;
		}
		ConquestUserUI.ClearUnitBar(upgradeBar);
		ConquestUserUI.Populate(upgradePrefab, units, upgradeBar, (int)unitBeingUpgraded.GetUnitCost());
		for (int i = 0; i < upgradeBar.transform.childCount; i++)
		{
			if (upgradeBar.transform.GetChild(i).gameObject.activeSelf)
			{
				Button button = upgradeBar.transform.GetChild(i).GetComponent<Button>();
				button.onClick.AddListener(delegate
				{
					ClickUpgradeButton(button.GetComponent<ConquestUnitButton>().unit);
				});
			}
		}
	}
}
