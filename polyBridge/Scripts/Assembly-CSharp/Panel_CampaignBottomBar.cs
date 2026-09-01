using UnityEngine;
using UnityEngine.UI;

public class Panel_CampaignBottomBar : MonoBehaviour
{
	public Button m_Leaderboards;

	public Button m_Gallery;

	public Button m_Achievements;

	public Button m_Ranks;

	private void Start()
	{
		m_Leaderboards.onClick.AddListener(OnLeaderboards);
		m_Gallery.onClick.AddListener(OnGallery);
		m_Achievements.onClick.AddListener(OnAchievements);
		m_Ranks.onClick.AddListener(OnMyRankings);
	}

	private void OnLeaderboards()
	{
		InterfaceAudio.Play("ui_menu_select");
		string selectedCampaignLevelID = GetSelectedCampaignLevelID();
		GameUI.m_Instance.m_Campaign.m_Root.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_LeaderboardsPanel.Open((!string.IsNullOrEmpty(selectedCampaignLevelID)) ? selectedCampaignLevelID : GetDefaultWorldForFilter());
	}

	private void OnGallery()
	{
		if (GameUI.m_Instance.m_Campaign.gameObject.activeInHierarchy)
		{
			OnGalleryForCampaign();
		}
		else
		{
			OnGalleryForWorkshop();
		}
	}

	private void OnGalleryForCampaign()
	{
		string selectedCampaignLevelID = GetSelectedCampaignLevelID();
		if (string.IsNullOrEmpty(selectedCampaignLevelID))
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		InterfaceAudio.Play("ui_menu_select");
		CampaignLevel levelFromId = CampaignWorlds.m_Instance.GetLevelFromId(selectedCampaignLevelID);
		GameUI.m_Instance.m_Campaign.m_Root.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_Gallery.OpenCampaignLevel(levelFromId);
	}

	private void OnGalleryForWorkshop()
	{
		if (GameUI.m_Instance.m_Workshop.m_WorkshopCampaignPanel == null)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		WorkshopItem selectedItem = GameUI.m_Instance.m_Workshop.m_WorkshopCampaignPanel.GetSelectedItem();
		if (selectedItem == null)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		GameUI.m_Instance.m_Workshop.m_WorkshopCampaignPanel.m_Root.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_Workshop.m_WorkshopCampaignPanel.m_Ducking.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_Gallery.OpenWorkshopItem(selectedItem.GetTitle(), selectedItem.GetId());
	}

	private void OnAchievements()
	{
		InterfaceAudio.Play("ui_menu_select");
		GameUI.m_Instance.m_Campaign.m_Root.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_Achievements.Open();
	}

	private void OnMyRankings()
	{
		InterfaceAudio.Play("ui_menu_select");
		if (GameManager.IsSteamOffline())
		{
			PopUpMessage.DisplayErrorOkOnly(Localize.Get("UI_STEAM_OFFLINE"));
			return;
		}
		GameUI.m_Instance.m_Campaign.m_Root.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_MyRankings.Open(GetDefaultWorldForFilter());
	}

	private string GetDefaultWorldForFilter()
	{
		string selectedWorldID = GameUI.m_Instance.m_Campaign.GetSelectedWorldID();
		if (!string.IsNullOrEmpty(selectedWorldID))
		{
			return selectedWorldID;
		}
		CampaignLevel campaignLevel = CampaignWorlds.m_Instance.GetLevelFromId(Profiles.m_ActiveProfile.m_LastLoadedCampaignLevelId);
		if (campaignLevel == null)
		{
			campaignLevel = CampaignWorlds.m_Instance.m_Worlds[0].m_Levels[0];
		}
		CampaignWorld worldWithLevelId = CampaignWorlds.m_Instance.GetWorldWithLevelId(campaignLevel.m_Id);
		if (!(worldWithLevelId != null))
		{
			return "001";
		}
		return worldWithLevelId.m_Id;
	}

	private string GetSelectedCampaignLevelID()
	{
		string text = GameUI.m_Instance.m_Campaign.GetSelectedLevelID();
		if (!string.IsNullOrEmpty(text))
		{
			CampaignLevel levelFromId = CampaignWorlds.m_Instance.GetLevelFromId(text);
			if ((bool)levelFromId && levelFromId.IsTutorial())
			{
				text = CampaignWorlds.m_Instance.GetNextLevel(levelFromId).m_Id;
			}
		}
		return text;
	}
}
