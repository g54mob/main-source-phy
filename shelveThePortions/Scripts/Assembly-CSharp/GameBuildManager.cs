using UnityEngine;
using VInspector;

public class GameBuildManager : Singleton<GameBuildManager>
{
	[SerializeField]
	private string appName = "Mini Settlers";

	[SerializeField]
	private string appVersion = "v0.1";

	[Space]
	[SerializeField]
	private GameVersion gameVersion;

	[SerializeField]
	private Platform platform;

	[SerializeField]
	private SteamAppSetter steamAppSetter;

	public GameVersion GetGameVersion()
	{
		return gameVersion;
	}

	public bool IsUnityGameVersion()
	{
		return gameVersion == GameVersion.UnityEngine;
	}

	public bool IsFreeVersion()
	{
		if (gameVersion != GameVersion.Playtest)
		{
			return gameVersion == GameVersion.Demo;
		}
		return true;
	}

	public Platform GetPlatform()
	{
		return platform;
	}

	[Button]
	public void SetSteamDemoReady()
	{
		gameVersion = GameVersion.Demo;
		platform = Platform.Steam;
		SetProductNameAndVersion(" Demo");
		steamAppSetter.ReplaceSteamAppDemoID();
		Debug.Log("Game is Set for Steam Demo Build");
	}

	[Button]
	public void SetPlaytestAppID()
	{
		gameVersion = GameVersion.Playtest;
		platform = Platform.Steam;
		SetProductNameAndVersion(" Playtest");
		steamAppSetter.ReplacePlaytestAppID();
		Debug.Log("Game is Set for Steam Beta Build");
	}

	[Button]
	public void SetGOGReady()
	{
		gameVersion = GameVersion.GOG;
		platform = Platform.GOG;
		SetProductNameAndVersion(" GOG");
		steamAppSetter.ReplaceUnityTestingID();
		Debug.Log("Game is Set for GOG Build");
	}

	[Button]
	public void SetSteamFullGameReady()
	{
		gameVersion = GameVersion.FullGame;
		platform = Platform.Steam;
		SetProductNameAndVersion("");
		steamAppSetter.ReplaceSteamAppID();
		Debug.Log("Game is Set for Steam Full Build");
	}

	[Button]
	public void SetUnityTestProject()
	{
		gameVersion = GameVersion.UnityEngine;
		platform = Platform.Steam;
		SetProductNameAndVersion(" Unity Test");
		steamAppSetter.ReplaceUnityTestingID();
		Debug.Log("Game is Set for Unity Testing");
	}

	public void SetProductNameAndVersion(string suffix)
	{
	}
}
