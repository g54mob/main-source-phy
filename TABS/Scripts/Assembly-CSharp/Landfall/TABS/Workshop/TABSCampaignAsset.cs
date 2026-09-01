using System;
using System.IO;
using ModIO;
using UnityEngine;
using UnityEngine.Serialization;

namespace Landfall.TABS.Workshop
{
	[Serializable]
	[CreateAssetMenu(fileName = "Standard Campaign", menuName = "Campaign/StandardCampaign", order = 1)]
	public class TABSCampaignAsset : ScriptableObject, IDatabaseEntity
	{
		[SerializeField]
		private DatabaseEntity m_entity;

		[SerializeField]
		private CampaignInfo m_campaignInfo;

		[FormerlySerializedAs("LevelsInCampaign")]
		[SerializeField]
		private TABSCampaignLevelAsset[] m_levelsInCampaign;

		public DatabaseEntity Entity => m_entity;

		public CampaignInfo CampaignInfo
		{
			get
			{
				return m_campaignInfo;
			}
			set
			{
				m_campaignInfo = value;
			}
		}

		public TABSCampaignLevelAsset[] LevelsInCampaign
		{
			get
			{
				return m_levelsInCampaign;
			}
			private set
			{
				m_levelsInCampaign = value;
			}
		}

		public bool IsLandfallLevel { get; set; }

		public bool LocalClientOwnedCustomContent { get; private set; }

		public bool IsCustomCampaign { get; private set; }

		public string FilePath { get; private set; }

		public string FolderPath { get; private set; }

		public ModProfile ModProfile { get; private set; }

		public int ModID { get; private set; }

		public bool IsModCampaign => ModID != 0;

		public void SetCustomUnit(int id, ModProfile modProfile, FileInfo contentFile)
		{
			IsCustomCampaign = true;
			ModID = id;
			FilePath = contentFile.FullName;
			FolderPath = contentFile.Directory.FullName;
			if (modProfile != null && CustomContentLoaderModIO.LocalModIOUser != null)
			{
				LocalClientOwnedCustomContent = modProfile.submittedBy.id == CustomContentLoaderModIO.LocalModIOUser.id;
			}
			ModProfile = modProfile;
		}

		public void SetIconPath(string iconPath)
		{
			Entity.SetSpriteIconPath(iconPath);
		}

		public CampaignSequence SerializeCampaign(string iconPath = null, bool isLandfallCampaign = false)
		{
			CampaignSequence campaignSequence = new CampaignSequence();
			campaignSequence.CampaignName = Entity.Name;
			campaignSequence.CampaignDescription = m_campaignInfo.Description;
			campaignSequence.CampaignThankYouTitle = m_campaignInfo.ThankYouTitle;
			campaignSequence.CampaignThankYouText = m_campaignInfo.ThankYouText;
			campaignSequence.ID = m_entity.GUID;
			campaignSequence.IconPath = iconPath;
			campaignSequence.Levels = new CampaignLevelReference[LevelsInCampaign.Length];
			for (int i = 0; i < campaignSequence.Levels.Length; i++)
			{
				TABSCampaignLevelAsset tABSCampaignLevelAsset = LevelsInCampaign[i];
				if (!(tABSCampaignLevelAsset == null))
				{
					CampaignLevel campaignLevel = new CampaignLevel();
					campaignLevel.ID = tABSCampaignLevelAsset.Entity.GUID;
					campaignLevel.LevelName = tABSCampaignLevelAsset.Entity.Name;
					campaignLevel.Budget = tABSCampaignLevelAsset.m_budget;
					campaignLevel.MapID = tABSCampaignLevelAsset.MapAsset.Entity.GUID;
					campaignSequence.Levels[i] = new CampaignLevelReference(campaignLevel, i);
				}
			}
			if (isLandfallCampaign)
			{
				campaignSequence.UnlockKey = Entity.UnlockKey;
			}
			return campaignSequence;
		}

		public void SetData(string campaignName, TABSCampaignLevelAsset[] levels)
		{
			m_entity = new DatabaseEntity(WorkshopContentType.Campaign);
			m_entity.GUID = new DatabaseID(-1337, 1337);
			m_entity.Name = campaignName;
			LevelsInCampaign = levels;
			m_campaignInfo = new CampaignInfo
			{
				Description = "All Custom Battles"
			};
		}

		public void SetLevels(TABSCampaignLevelAsset[] levels)
		{
			m_levelsInCampaign = levels;
		}

		public static TABSCampaignAsset DeserializeCampaign(CampaignSequence serializedCampaign, Func<DatabaseID, TABSCampaignLevelAsset> getCampaignLevel, bool isLandfallCampaign = false)
		{
			TABSCampaignAsset tABSCampaignAsset = ScriptableObject.CreateInstance<TABSCampaignAsset>();
			tABSCampaignAsset.m_entity = new DatabaseEntity(WorkshopContentType.Campaign);
			tABSCampaignAsset.m_entity.Name = serializedCampaign.CampaignName;
			tABSCampaignAsset.m_entity.GUID = serializedCampaign.ID;
			if (!string.IsNullOrEmpty(serializedCampaign.IconPath))
			{
				tABSCampaignAsset.m_entity.SetSpriteIconPath(serializedCampaign.IconPath);
			}
			tABSCampaignAsset.m_campaignInfo = default(CampaignInfo);
			tABSCampaignAsset.m_campaignInfo.Description = serializedCampaign.CampaignDescription;
			tABSCampaignAsset.m_campaignInfo.ThankYouTitle = serializedCampaign.CampaignThankYouTitle;
			tABSCampaignAsset.m_campaignInfo.ThankYouText = serializedCampaign.CampaignThankYouText;
			tABSCampaignAsset.m_levelsInCampaign = new TABSCampaignLevelAsset[serializedCampaign.Levels.Length];
			for (int i = 0; i < tABSCampaignAsset.m_levelsInCampaign.Length; i++)
			{
				tABSCampaignAsset.m_levelsInCampaign[i] = getCampaignLevel(serializedCampaign.Levels[i].ID);
			}
			if (isLandfallCampaign)
			{
				tABSCampaignAsset.m_entity.SetUnlockKey(serializedCampaign.UnlockKey);
			}
			return tABSCampaignAsset;
		}
	}
}
