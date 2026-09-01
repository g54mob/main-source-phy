using System.Collections.Generic;
using TMPro;

public class CampaignWorldDropdown
{
	private static Dictionary<string, CampaignWorld> m_WorldNameWorldMap = new Dictionary<string, CampaignWorld>();

	public static bool ContainsKey(string worldName)
	{
		return m_WorldNameWorldMap.ContainsKey(worldName);
	}

	public static CampaignWorld GetValue(string worldName)
	{
		if (!ContainsKey(worldName))
		{
			return null;
		}
		return m_WorldNameWorldMap[worldName];
	}

	public static void Populate(TMP_Dropdown worldNameDropdown, bool includeAll)
	{
		List<string> list = new List<string>();
		if (includeAll)
		{
			list.Add(Localize.Get("UI_ALL"));
		}
		m_WorldNameWorldMap.Clear();
		CampaignWorld[] worlds = CampaignWorlds.m_Instance.m_Worlds;
		foreach (CampaignWorld campaignWorld in worlds)
		{
			if (!campaignWorld.IsSecretWorld() || GameManager.IsSecretWorldUnlocked())
			{
				string text = FormatWorldName(campaignWorld.m_DisplayNameLocID);
				list.Add(text);
				m_WorldNameWorldMap.Add(text, campaignWorld);
			}
		}
		worldNameDropdown.ClearOptions();
		worldNameDropdown.AddOptions(list);
	}

	public static void Select(TMP_Dropdown worldNameDropdown, string worldID)
	{
		if (worldID == CampaignWorlds.WORLD_ID_ALL)
		{
			worldNameDropdown.value = 0;
			return;
		}
		foreach (KeyValuePair<string, CampaignWorld> item in m_WorldNameWorldMap)
		{
			CampaignWorld value = item.Value;
			if (value.m_Id == worldID)
			{
				string text = FormatWorldName(value.m_DisplayNameLocID);
				DropdownUtils.SelectItem(worldNameDropdown, text);
				break;
			}
		}
	}

	private static string FormatWorldName(string locId)
	{
		return Localize.Get(locId);
	}
}
