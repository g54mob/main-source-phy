using UnityEngine;

public static class FrameRate
{
	public static int GetFPSLock(BesiegeConfig conf)
	{
		switch (conf.FPSLock)
		{
		case FPSLock.Lock30:
			return 30;
		case FPSLock.Lock60:
			return 60;
		case FPSLock.Lock75:
			return 75;
		case FPSLock.Lock100:
			return 100;
		case FPSLock.Lock120:
			return 120;
		case FPSLock.Lock144:
			return 144;
		case FPSLock.Lock165:
			return 165;
		case FPSLock.Lock180:
			return 180;
		case FPSLock.Lock240:
			return 240;
		case FPSLock.Unlimited:
			return -1;
		default:
			return (int)conf.FPSLock;
		}
	}

	public static int GetBestRefreshRate()
	{
		int fPSLock = GetFPSLock(OptionsMaster.BesiegeConfig);
		if (fPSLock <= 0)
		{
			return GetRefreshRate();
		}
		return Mathf.Min(fPSLock, GetRefreshRate());
	}

	public static int GetRefreshRate()
	{
		return Screen.currentResolution.refreshRate;
	}

	public static float GetLockDelta(BesiegeConfig conf)
	{
		float num = 1f / (1f * (float)GetFPSLock(conf));
		if (num < 0f)
		{
			num = 0.01f;
		}
		return num;
	}
}
