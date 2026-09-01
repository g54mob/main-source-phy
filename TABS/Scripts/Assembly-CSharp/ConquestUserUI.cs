using System.Collections.Generic;
using Landfall.TABS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConquestUserUI : MonoBehaviour
{
	public CodeAnimation uiBarAnim;

	public GameObject unitGrid;

	public GameObject playerArmyGrid;

	public GameObject unitButton;

	public TextMeshProUGUI placeName;

	public GameObject upgradeScreenObject;

	public void ToggleUpgradeScreen(bool on)
	{
		upgradeScreenObject.SetActive(on);
	}

	public void PopulatePlayerArmyBar(Dictionary<UnitBlueprint, int> dictionary)
	{
		ClearUnitBar(playerArmyGrid);
		Populate(unitButton, dictionary, playerArmyGrid);
	}

	private void PopulateUIBar(Dictionary<UnitBlueprint, int> dictionary)
	{
		ClearUnitBar(unitGrid);
		Populate(unitButton, dictionary, unitGrid);
	}

	public static void ClearUnitBar(GameObject bar)
	{
		for (int i = 0; i < bar.transform.childCount; i++)
		{
			if (bar.transform.GetChild(i).gameObject.activeSelf)
			{
				Object.Destroy(bar.transform.GetChild(i).gameObject);
			}
		}
	}

	public static void Populate(GameObject sourceButton, Dictionary<UnitBlueprint, int> dictionary, GameObject grid)
	{
		sourceButton.SetActive(value: true);
		foreach (KeyValuePair<UnitBlueprint, int> item in dictionary)
		{
			if (item.Value > 0)
			{
				GameObject obj = Object.Instantiate(sourceButton);
				obj.transform.SetParent(grid.transform);
				obj.transform.localScale = Vector3.one;
				obj.transform.localPosition = Vector3.zero;
				obj.transform.localRotation = Quaternion.identity;
				obj.transform.GetChild(0).GetComponent<Image>().sprite = item.Key.Entity.SpriteIcon;
				obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.Value.ToString();
				obj.GetComponent<ConquestUnitButton>().unit = item.Key;
			}
		}
		sourceButton.SetActive(value: false);
	}

	public static void Populate(GameObject sourceButton, UnitBlueprint[] units, GameObject grid, int costOfUnitBeingUpgraded)
	{
		sourceButton.SetActive(value: true);
		foreach (UnitBlueprint unitBlueprint in units)
		{
			GameObject obj = Object.Instantiate(sourceButton);
			obj.transform.SetParent(grid.transform);
			obj.transform.localScale = Vector3.one;
			obj.transform.localPosition = Vector3.zero;
			obj.transform.localRotation = Quaternion.identity;
			obj.transform.GetChild(0).GetComponent<Image>().sprite = unitBlueprint.Entity.SpriteIcon;
			obj.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = unitBlueprint.Name.ToString().ToUpper();
			obj.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = (unitBlueprint.GetUnitCost() - costOfUnitBeingUpgraded).ToString().ToUpper();
			obj.GetComponent<ConquestUnitButton>().unit = unitBlueprint;
		}
		sourceButton.SetActive(value: false);
	}

	public void Click(GameObject obj)
	{
		ConquestNode component = obj.GetComponent<ConquestNode>();
		if ((bool)component)
		{
			placeName.text = component.placeName;
			PopulateUIBar(component.units);
			if (uiBarAnim.currentState == CodeAnimationInstance.AnimationUse.Out)
			{
				uiBarAnim.PlayIn();
			}
			else
			{
				uiBarAnim.PlayBoop();
			}
		}
		else if (uiBarAnim.currentState != CodeAnimationInstance.AnimationUse.Out)
		{
			uiBarAnim.PlayOut();
		}
	}
}
