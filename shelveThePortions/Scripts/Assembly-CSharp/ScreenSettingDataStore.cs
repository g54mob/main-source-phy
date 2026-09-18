using System.Collections.Generic;
using UnityEngine;

public class ScreenSettingDataStore : Singleton<ScreenSettingDataStore>
{
	[SerializeField]
	public int defaultResolutionIndex;

	[SerializeField]
	private List<Resolution> resolutionsList;

	[Space]
	[SerializeField]
	public int defaultScreenIndex;

	[SerializeField]
	private List<ScreenMode> screenModesList;

	public int GetResolutionCount()
	{
		return resolutionsList.Count;
	}

	public Resolution GetResolution(int index)
	{
		return resolutionsList[index];
	}

	public int GetScreenModeCount()
	{
		return screenModesList.Count;
	}

	public ScreenMode GetScreenMode(int index)
	{
		return screenModesList[index];
	}

	public void SetScreenSettingFirstTime()
	{
		Resolution resolution = new Resolution();
		resolution.width = Screen.currentResolution.width;
		resolution.height = Screen.currentResolution.height;
		int num = -1;
		foreach (Resolution resolutions in resolutionsList)
		{
			num++;
			if (resolutions.width == resolution.width && resolutions.height == resolution.height)
			{
				break;
			}
		}
		if (num >= resolutionsList.Count)
		{
			SaveSystem.SaveResulationSetting(defaultResolutionIndex);
		}
		else
		{
			SaveSystem.SaveResulationSetting(num);
		}
		SaveSystem.SaveScreenModeSetting(0);
	}
}
