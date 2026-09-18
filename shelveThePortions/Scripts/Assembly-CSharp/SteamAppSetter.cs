using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SteamAppSetter : SceneSingleton<SteamAppSetter>
{
	private string steamFileName = "steam_appid.txt";

	public List<GameObject> steamObjects;

	[Header("Full Game Data")]
	[SerializeField]
	private string SteamAppID;

	[Header("Beta Data")]
	[SerializeField]
	private string SteamPlaytestAppID;

	[Space]
	[Header("Demo Data")]
	[SerializeField]
	private string SteamAppDemoID;

	[Space]
	[Header("Proluge Data")]
	[SerializeField]
	private string SteamAppProlugeID;

	public void ReplaceSteamAppID()
	{
		ChangeID(SteamAppID);
		SetSteamObjects(flag: true);
	}

	public void ReplacePlaytestAppID()
	{
		ChangeID(SteamPlaytestAppID);
		SetSteamObjects(flag: true);
	}

	public void ReplaceUnityTestingID()
	{
		ChangeID("");
		SetSteamObjects(flag: false);
	}

	public void ReplaceSteamAppPrologueID()
	{
		ChangeID(SteamAppProlugeID);
		SetSteamObjects(flag: true);
	}

	public void ReplaceSteamAppDemoID()
	{
		ChangeID(SteamAppDemoID);
		SetSteamObjects(flag: true);
	}

	private void SetSteamObjects(bool flag)
	{
		foreach (GameObject steamObject in steamObjects)
		{
			steamObject.SetActive(flag);
		}
	}

	private void ChangeID(string appID)
	{
		File.WriteAllText(Path.Combine(Directory.GetParent(Application.dataPath).ToString(), steamFileName), appID);
	}
}
