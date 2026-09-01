using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegionSelection : MonoBehaviour
{
	[SerializeField]
	private Dropdown regionDropDown;

	private void Awake()
	{
		if (regionDropDown == null)
		{
			Debug.LogError("Region Dropdown is not assigned, please assign it.");
			base.enabled = false;
		}
		else
		{
			Initialize();
		}
	}

	private void Initialize()
	{
		FillDropdownOptions();
		SelectDefaultRegion();
		regionDropDown.onValueChanged.AddListener(OnDropdownValueChanged);
	}

	private void SelectDefaultRegion()
	{
		Region defaultRegion = OptionsMaster.BesiegeConfig.Region;
		int num = regionDropDown.options.FindIndex((Dropdown.OptionData x) => ((RegionOptionData)x).Region == defaultRegion);
		if (num != -1)
		{
			regionDropDown.value = num;
		}
	}

	private void FillDropdownOptions()
	{
		regionDropDown.ClearOptions();
		List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
		foreach (Region key in ReferenceMaster.RegionServers.Keys)
		{
			string text = ReferenceMaster.TranslateRegion(key);
			list.Add(new RegionOptionData(key, text));
		}
		regionDropDown.AddOptions(list);
	}

	private void OnDropdownValueChanged(int selectedIndex)
	{
		RegionOptionData regionOptionData = (RegionOptionData)regionDropDown.options[selectedIndex];
		Region region = regionOptionData.Region;
		OptionsMaster.BesiegeConfig.Region = region;
		if (ReferenceMaster.RegionChanged != null)
		{
			ReferenceMaster.RegionChanged(region);
		}
	}

	private void OnDestroy()
	{
		regionDropDown.onValueChanged.RemoveListener(OnDropdownValueChanged);
	}
}
