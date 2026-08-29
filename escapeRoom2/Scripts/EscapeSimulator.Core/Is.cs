using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public static class Is
{
	private static List<string> args;

	private static bool? isDev;

	private static bool? isPerfTest;

	public static bool Oculus
	{
		get
		{
			if (VR)
			{
				return Android;
			}
			return false;
		}
	}

	public static bool VR
	{
		get
		{
			if (global::VR.instance != null)
			{
				return global::VR.instance.isActive();
			}
			return false;
		}
	}

	public static bool LowDevice
	{
		get
		{
			if (!Android)
			{
				return Switch;
			}
			return true;
		}
	}

	public static bool Forge => Application.isBatchMode;

	public static bool Editor => false;

	public static bool Release => true;

	public static bool Android => false;

	public static bool AndroidPhone => false;

	public static bool iOS => false;

	public static bool PS4 => false;

	public static bool Xbox => false;

	public static bool Switch => false;

	public static bool Demo => false;

	public static bool Steam => true;

	public static bool Steamdeck => SteamUtils.IsSteamRunningOnSteamDeck();

	public static bool DebugBuild => false;

	public static bool PineBackend => false;

	public static bool Gamescom => false;

	public static bool Console => false;

	public static bool Linux => false;

	public static bool Mac => false;

	static Is()
	{
		args = new List<string>(Environment.GetCommandLineArgs());
	}

	public static bool dev()
	{
		if (!Release)
		{
			return true;
		}
		if (!isDev.HasValue)
		{
			isDev = args.Contains("dev");
		}
		return isDev.Value;
	}

	public static bool perfTest()
	{
		if (!isPerfTest.HasValue)
		{
			isPerfTest = args.Contains("perfTest");
		}
		return isPerfTest.Value;
	}
}
