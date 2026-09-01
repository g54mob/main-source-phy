using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GalleryMetaData
{
	public static string LEVEL_ID_KEY => GalleryMetaDataKeys.LEVEL_ID.ToString();

	public static string WORLD_ID_KEY => GalleryMetaDataKeys.WORLD_ID.ToString();

	public static string MAX_STRESS_KEY => GalleryMetaDataKeys.MAX_STRESS.ToString();

	public static string MAX_STRESS_ENCODED_KEY => GalleryMetaDataKeys.MAX_STRESS_ENCODED.ToString();

	public static string BUDGET_KEY => GalleryMetaDataKeys.BUDGET.ToString();

	public static string STEAM_ID_KEY => GalleryMetaDataKeys.STEAM_ID.ToString();

	public static string WORKSHOP_LEVEL_NAME_KEY => GalleryMetaDataKeys.WORKSHOP_LEVEL_NAME.ToString();

	public static string Create(string steamId, string levelId, string worldId, string maxStress, string budget, string workshopLevelName)
	{
		string text = BUDGET_KEY + "=" + budget + "|" + MAX_STRESS_ENCODED_KEY + "=" + maxStress;
		if (!string.IsNullOrEmpty(steamId))
		{
			text = text + "|" + STEAM_ID_KEY + "=" + steamId;
		}
		if (!string.IsNullOrEmpty(levelId))
		{
			text = text + "|" + LEVEL_ID_KEY + "=" + levelId;
		}
		if (!string.IsNullOrEmpty(worldId))
		{
			text = text + "|" + WORLD_ID_KEY + "=" + worldId;
		}
		if (!string.IsNullOrEmpty(workshopLevelName))
		{
			text = text + "|" + WORKSHOP_LEVEL_NAME_KEY + "=" + workshopLevelName;
		}
		return text;
	}

	public static string GetLevelID(Dictionary<string, string> metaData)
	{
		if (metaData != null && metaData.ContainsKey(LEVEL_ID_KEY))
		{
			return metaData[LEVEL_ID_KEY];
		}
		return string.Empty;
	}

	public static string GetLevelNameFormatted(Dictionary<string, string> metaData)
	{
		if (metaData != null && metaData.ContainsKey(LEVEL_ID_KEY))
		{
			string levelId = metaData[LEVEL_ID_KEY];
			CampaignLevel levelFromId = CampaignWorlds.m_Instance.GetLevelFromId(levelId);
			if (levelFromId != null)
			{
				return levelFromId.GetFullNameFormatted();
			}
		}
		if (metaData != null && metaData.ContainsKey(WORKSHOP_LEVEL_NAME_KEY))
		{
			return metaData[WORKSHOP_LEVEL_NAME_KEY];
		}
		return Localize.Get("MAINMENU_SANDBOX");
	}

	public static string GetLevelNameWithoutColorizationTags(Dictionary<string, string> metaData)
	{
		if (metaData != null && metaData.ContainsKey(LEVEL_ID_KEY))
		{
			string levelId = metaData[LEVEL_ID_KEY];
			CampaignLevel levelFromId = CampaignWorlds.m_Instance.GetLevelFromId(levelId);
			if (levelFromId != null)
			{
				return levelFromId.GetFullNameWithoutColorizationTags();
			}
		}
		if (metaData != null && metaData.ContainsKey(WORKSHOP_LEVEL_NAME_KEY))
		{
			return metaData[WORKSHOP_LEVEL_NAME_KEY];
		}
		return Localize.Get("MAINMENU_SANDBOX");
	}

	public static string GetLevelNameNoPrefix(Dictionary<string, string> metaData)
	{
		if (metaData != null && metaData.ContainsKey(LEVEL_ID_KEY))
		{
			string levelId = metaData[LEVEL_ID_KEY];
			CampaignLevel levelFromId = CampaignWorlds.m_Instance.GetLevelFromId(levelId);
			if (levelFromId != null)
			{
				return levelFromId.GetLocalizedDisplayNameWithoutPrefix();
			}
		}
		if (metaData != null && metaData.ContainsKey(WORKSHOP_LEVEL_NAME_KEY))
		{
			return metaData[WORKSHOP_LEVEL_NAME_KEY];
		}
		return Localize.Get("MAINMENU_SANDBOX");
	}

	public static string GetWorldName(Dictionary<string, string> metaData)
	{
		if (metaData == null || metaData.ContainsKey(WORLD_ID_KEY))
		{
			string id = metaData[WORLD_ID_KEY];
			CampaignWorld worldById = CampaignWorlds.m_Instance.GetWorldById(id);
			if (!(worldById == null))
			{
				return worldById.GetDisplayName();
			}
			return string.Empty;
		}
		if (metaData == null || metaData.ContainsKey(WORKSHOP_LEVEL_NAME_KEY))
		{
			return Localize.Get("UI_WORKSHOP");
		}
		return string.Empty;
	}

	public static string GetBudget(Dictionary<string, string> metaData)
	{
		if (metaData == null || !metaData.ContainsKey(BUDGET_KEY))
		{
			return string.Empty;
		}
		if (!int.TryParse(metaData[BUDGET_KEY], out var result))
		{
			return string.Empty;
		}
		return string.Format(CultureInfo.InvariantCulture, "{0:n0}", result);
	}

	public static string GetMaxStress(Dictionary<string, string> metaData)
	{
		if (metaData == null)
		{
			return string.Empty;
		}
		if (metaData.ContainsKey(MAX_STRESS_KEY))
		{
			if (!int.TryParse(metaData[MAX_STRESS_KEY], out var result))
			{
				return Utils.FormatInteger(0f) + "%";
			}
			return Utils.FormatInteger(result) + "%";
		}
		if (metaData.ContainsKey(MAX_STRESS_ENCODED_KEY))
		{
			if (!int.TryParse(metaData[MAX_STRESS_ENCODED_KEY], out var result2))
			{
				return Utils.FormatStress(0f);
			}
			return Utils.FormatStress(Mathf.Clamp((float)result2 / 100f, 0f, 100f));
		}
		return Utils.FormatStress(0f);
	}

	public static float GetMaxStressNormalized(Dictionary<string, string> metaData)
	{
		if (metaData == null)
		{
			return 0f;
		}
		if (metaData.ContainsKey(MAX_STRESS_KEY))
		{
			if (!float.TryParse(metaData[MAX_STRESS_KEY], out var result))
			{
				return 0f;
			}
			return Mathf.Clamp(result / 100f, 0f, 1f);
		}
		if (metaData.ContainsKey(MAX_STRESS_ENCODED_KEY))
		{
			if (!int.TryParse(metaData[MAX_STRESS_ENCODED_KEY], out var result2))
			{
				return 0f;
			}
			return Mathf.Clamp((float)result2 / 10000f, 0f, 1f);
		}
		return 0f;
	}

	public static string GetSteamId(Dictionary<string, string> metaData)
	{
		if (metaData == null || !metaData.ContainsKey(STEAM_ID_KEY))
		{
			return string.Empty;
		}
		return metaData[STEAM_ID_KEY];
	}
}
