using System;
using System.IO;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CampaignLevel", menuName = "Game/CampaignLevel", order = 3)]
public class CampaignLevel : ScriptableObject
{
	[NonSerialized]
	public string m_Id;

	[NonSerialized]
	public string m_Filename;

	[NonSerialized]
	public string m_DisplayNameLocID;

	[NonSerialized]
	public string m_DescriptionLocID;

	[NonSerialized]
	public string m_WorldId;

	[NonSerialized]
	public string m_NumberPrefix;

	[NonSerialized]
	public bool m_UnlimitedBudget;

	[NonSerialized]
	public bool m_UnlimitedMaterial;

	[NonSerialized]
	private string m_CachedLocalizedDisplayNameWithPrefix;

	[NonSerialized]
	private string m_CachedFullNameFormatted;

	[NonSerialized]
	private string m_CachedFullNameWithouColorizationTags;

	public void OnEnable()
	{
		m_Id = base.name;
		m_Filename = m_Id + ".layout";
		m_DisplayNameLocID = "LEVEL_" + m_Id;
		m_DescriptionLocID = "LEVEL_" + m_Id + "_DESC";
	}

	public void RefreshCachedStrings()
	{
		m_CachedLocalizedDisplayNameWithPrefix = $"{m_NumberPrefix} {Localize.Get(m_DisplayNameLocID)}".Trim();
		m_CachedFullNameFormatted = GameUI.GOLD_COLOR_HEX_TAG + GetPrefix() + " <#FFFFFF>" + GetLocalizedDisplayNameWithoutPrefix();
		m_CachedFullNameWithouColorizationTags = GetPrefix() + " " + GetLocalizedDisplayNameWithoutPrefix();
	}

	public string GetLocalizedDisplayNameWithoutPrefix()
	{
		return Localize.Get(m_DisplayNameLocID);
	}

	public string GetLevelNumberFormatted()
	{
		CampaignWorld worldWithLevelId = CampaignWorlds.m_Instance.GetWorldWithLevelId(m_Id);
		if (worldWithLevelId == null)
		{
			return string.Empty;
		}
		int num = 0;
		CampaignLevel[] levels = worldWithLevelId.m_Levels;
		for (int i = 0; i < levels.Length; i++)
		{
			if (levels[i].m_Id == m_Id)
			{
				return (num + 1).ToString("D2");
			}
			num++;
		}
		return string.Empty;
	}

	public string GetPrefix()
	{
		return m_NumberPrefix;
	}

	public string GetLocalizedDisplayNameWithPrefix()
	{
		return m_CachedLocalizedDisplayNameWithPrefix;
	}

	public string GetLocalizedDescription()
	{
		return Localize.Get(m_DescriptionLocID);
	}

	public bool IsTutorial()
	{
		return GetCampaignTutorialType() != CampaignTutorialType.None;
	}

	public bool IsSecretWorldLevel()
	{
		CampaignWorld worldWithLevelId = CampaignWorlds.m_Instance.GetWorldWithLevelId(m_Id);
		if (worldWithLevelId != null)
		{
			return worldWithLevelId.IsSecretWorld();
		}
		return false;
	}

	public bool HasHelpPreviews()
	{
		CampaignWorld worldWithLevelId = CampaignWorlds.m_Instance.GetWorldWithLevelId(m_Id);
		if ((bool)worldWithLevelId)
		{
			return worldWithLevelId.m_NumStars < 3;
		}
		return false;
	}

	public string GetLayoutPath()
	{
		return Path.Combine(Campaign.GetLevelsPath(m_Id), m_Filename);
	}

	public CampaignTutorialType GetCampaignTutorialType()
	{
		if (!(CampaignTutorials.m_Instance != null))
		{
			return CampaignTutorialType.None;
		}
		return CampaignTutorials.m_Instance.GetTutorialTypeForLevelId(m_Id);
	}

	public string GetFullNameFormatted()
	{
		return m_CachedFullNameFormatted;
	}

	public string GetFullNameWithoutColorizationTags()
	{
		return m_CachedFullNameWithouColorizationTags;
	}
}
