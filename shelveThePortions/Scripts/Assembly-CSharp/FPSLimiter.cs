using UnityEngine;

public static class FPSLimiter
{
	public static readonly int[] fpsOptions = new int[5] { 30, 60, 90, 120, -1 };

	public static void UpdateFPSLimit()
	{
		int num = fpsOptions[GetFPSIndex()];
		if (SaveSystem.GetVsyncSetting())
		{
			QualitySettings.vSyncCount = 1;
		}
		else if (num == 0)
		{
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = -1;
		}
		else
		{
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = num;
		}
	}

	public static int GetFPS()
	{
		return fpsOptions[PlayerPrefs.GetInt("FPSLimit", 1)];
	}

	public static int GetFPSIndex()
	{
		return PlayerPrefs.GetInt("FPSLimit", 1);
	}

	public static void SetFPSIndex(int vol)
	{
		PlayerPrefs.SetInt("FPSLimit", vol);
		UpdateFPSLimit();
	}
}
