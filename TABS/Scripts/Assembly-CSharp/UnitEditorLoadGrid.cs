using System;
using System.Collections.Generic;
using System.Linq;
using DM;
using Landfall.TABS;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.UI;

public class UnitEditorLoadGrid : UIComponentMainMenu
{
	private int currentPage;

	private List<GameObject> spawnedButtons = new List<GameObject>();

	public GameObject buttonCell;

	public GameObject factionCell;

	[SerializeField]
	private Transform contentContainer;

	[SerializeField]
	private LoadUnitTypeTab[] TABS;

	private PlayerActions playerActions;

	protected override void Start()
	{
		base.Start();
		playerActions = PlayerActions.Instance;
		SwitchTab(0, force: true);
	}

	protected override void Update()
	{
		base.Update();
		if (playerActions.m_cycleTabs.WasPressed)
		{
			int num = Mathf.RoundToInt(playerActions.m_cycleTabs.Value);
			int num2 = (currentPage + num) % TABS.Length;
			int index = ((num2 < 0) ? (TABS.Length - 1) : num2);
			SwitchTab(index);
		}
	}

	public void SwitchTab(int index)
	{
		SwitchTab(index, force: false);
	}

	public void SwitchTab(int index, bool force, bool alsoTriggerToggleValueChange = true)
	{
		if (index != currentPage || force)
		{
			currentPage = index;
			Toggle tab = TABS[index].tab;
			if (alsoTriggerToggleValueChange)
			{
				tab.isOn = true;
			}
			RePopulate(TABS[index].unitType);
		}
	}

	public void TabClicked(int index)
	{
		SwitchTab(index, force: false, alsoTriggerToggleValueChange: false);
	}

	public void RePopulate(UnitEditorLoadType unitType)
	{
		UnitBlueprint[] units = new UnitBlueprint[0];
		switch (unitType)
		{
		case UnitEditorLoadType.LocalUser:
			units = ContentDatabase.Instance().GetUserUnitBlueprints().ToArray();
			break;
		case UnitEditorLoadType.Factions:
		{
			Faction[] factions = ContentDatabase.Instance().GetAllFactions().ToArray();
			PopulateFactions(factions);
			return;
		}
		default:
			throw new ArgumentOutOfRangeException("unitType", unitType, null);
		case UnitEditorLoadType.Workshop:
			break;
		}
		Populate(units);
	}

	public void SelectFaction(Faction faction)
	{
		Populate(faction.Units);
	}

	public void PopulateFactions(Faction[] factions)
	{
		for (int i = 0; i < spawnedButtons.Count; i++)
		{
			UnityEngine.Object.Destroy(spawnedButtons[i]);
		}
		spawnedButtons.Clear();
		for (int j = 0; j < factions.Length; j++)
		{
			SpawnFactionButton(factions[j]);
		}
	}

	public void Populate(UnitBlueprint[] units)
	{
		for (int i = 0; i < spawnedButtons.Count; i++)
		{
			UnityEngine.Object.Destroy(spawnedButtons[i]);
		}
		spawnedButtons.Clear();
		int num = units.Length;
		for (int j = 0; j < num; j++)
		{
			SpawnUnitButton(units[j]);
		}
	}

	private void SpawnUnitButton(UnitBlueprint unit)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(buttonCell, contentContainer);
		gameObject.GetComponent<UnitEditorLoadUnitCell>().Init(unit);
		gameObject.SetActive(value: true);
		spawnedButtons.Add(gameObject);
	}

	private void SpawnFactionButton(Faction faction)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(factionCell, contentContainer);
		gameObject.GetComponent<UnitEditorLoadFactionCell>().Init(faction, this);
		gameObject.SetActive(value: true);
		spawnedButtons.Add(gameObject);
	}
}
