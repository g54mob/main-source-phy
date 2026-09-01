using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS.Workshop;
using UnityEngine;
using UnityEngine.Analytics;

public class TABSAnalytics
{
	private const string SANDBOX_ENDED_KEY = "SandboxBattleEnded";

	private const string CAMPAIGN_ENDED_KEY = "CampaignBattleEnded";

	private const string MISSING_DATABASE_ID = "MissingDatabaseID";

	private const string C_DID_WIN_KEY = "Win";

	private const string C_LEVEL = "Level";

	private const string S_RED_WIN_KEY = "RedWin";

	private const string S_LEVEL = "Level";

	public static void CampaignBattleEnded(bool didWin, TABSCampaignLevelAsset map)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("Win", didWin);
		dictionary.Add("Level", map.Entity.Name);
		Debug.Log("--- ANALYTICS --- Sent CampaignBattleEnded: " + AnalyticsEvent.Custom("CampaignBattleEnded", dictionary));
	}

	public static void SandboxBattleEnded(bool redWin, MapAsset map)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("RedWin", redWin);
		if (map != null)
		{
			dictionary.Add("Level", map.Entity.Name);
		}
		Debug.Log("--- ANALYTICS --- Sent SandboxBattleeEnded: " + AnalyticsEvent.Custom("SandboxBattleEnded", dictionary).ToString() + " Map: " + map?.Entity.Name);
	}

	public static void MissingDatabaseID(DatabaseID id)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("ID", id);
		AnalyticsEvent.Custom("MissingDatabaseID", dictionary);
	}
}
