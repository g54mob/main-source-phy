using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkshopItemSlot : MonoBehaviour
{
	public delegate void OnHoverChangeDelegate(WorkshopItemSlot slot, bool hover);

	public OnHoverChangeDelegate m_OnHoverChangeCallback;

	public RawImage m_RawImage;

	public RectTransform m_RawImageRectTransform;

	public PointerEvents m_PointerEvents;

	public Button m_BodyButton;

	public Image m_Border;

	[Header("Header")]
	public TextMeshProUGUI m_NameText;

	public Button m_ForgetButton;

	public Image m_InfoIcon;

	public ToolTipText m_InfoToolTipText;

	[Header("Footer")]
	public Image[] m_Stars;

	public RectTransform m_FooterRectTransform;

	public TextMeshProUGUI m_ProgressText;

	public TextMeshProUGUI m_NumUpVotes;

	public TextMeshProUGUI m_NumDownVotes;

	public Button m_PlayButton;

	public ToolTipText m_PlayButtonToolTipText;

	public Image m_CompletedIcon;

	public Image m_UnderBudgetIcon;

	public Image m_UnderBudgetUnbreakingIcon;

	public Image m_ModActivatedIcon;

	public Image m_CheatIcon;

	public Image m_SubscribedIcon;

	[NonSerialized]
	public WorkshopItem m_Item;

	[NonSerialized]
	public byte[] m_PreviewData;

	[NonSerialized]
	public bool m_ForceShowPanel;

	private void Start()
	{
		m_BodyButton.onClick.AddListener(OnBody);
		m_PlayButton.onClick.AddListener(OnPlay);
		m_ForgetButton.onClick.AddListener(OnForget);
		m_Border.enabled = false;
	}

	public void OnEnable()
	{
		if (m_Item != null)
		{
			UpdateFields();
			UpdateInfoToolTipText();
		}
	}

	public void Update()
	{
		UpdateInfoToolTipText();
	}

	public void SetItem(WorkshopItem item)
	{
		m_Item = item;
		UpdateFields();
		UpdateInfoToolTipText();
	}

	public void UpdateFields()
	{
		GameUI.SetAndEnableText(m_NameText, m_Item.GetTitle());
		UpdateNumUpVotes();
		m_SubscribedIcon.gameObject.SetActive(m_Item.IsSubscribed() && m_Item.IsMod());
		if (m_Item.IsMod())
		{
			m_CompletedIcon.gameObject.SetActive(value: false);
			m_UnderBudgetIcon.gameObject.SetActive(value: false);
			m_UnderBudgetUnbreakingIcon.gameObject.SetActive(value: false);
			m_ForgetButton.gameObject.SetActive(value: false);
			bool active = Mods.ModIsActive(m_Item.GetId());
			m_ModActivatedIcon.gameObject.SetActive(active);
			FileInfo[] luaFilesInMod = Mods.GetLuaFilesInMod(m_Item.GetDirectory());
			if (luaFilesInMod != null && luaFilesInMod.Length != 0)
			{
				bool active2 = ModApi.CheckForCheatFunctions(luaFilesInMod);
				m_CheatIcon.gameObject.SetActive(active2);
			}
			else
			{
				m_CheatIcon.gameObject.SetActive(value: false);
			}
			m_PlayButton.gameObject.SetActive(value: false);
			m_ProgressText.transform.parent.gameObject.SetActive(value: false);
			return;
		}
		int budget = WorkshopMetaData.GetBudget(m_Item.GetMetadata());
		bool flag = BridgeSaveSlots.HasCompletedLevelUnderBudgetNoBreaks(m_Item.GetId(), budget);
		bool flag2 = flag || BridgeSaveSlots.HasCompletedLevelUnderBudget(m_Item.GetId(), budget);
		bool active3 = flag2 || BridgeSaveSlots.HasCompletedLevel(m_Item.GetId());
		m_CompletedIcon.gameObject.SetActive(active3);
		m_UnderBudgetIcon.gameObject.SetActive(flag2);
		m_UnderBudgetUnbreakingIcon.gameObject.SetActive(flag);
		m_ForgetButton.gameObject.SetActive(GameUI.m_Instance.m_Workshop.m_FilterBar.m_WorkshopSortOrder == WorkshopSortOrder.MOST_RECENTLY_PLAYED);
		m_ModActivatedIcon.gameObject.SetActive(value: false);
		m_CheatIcon.gameObject.SetActive(value: false);
		m_PlayButton.gameObject.SetActive(m_Item.IsSubscribed());
		m_PlayButtonToolTipText.m_RawLocalizationKey = (m_Item.IsCampaign() ? "UI_PLAY_CAMPAIGN" : "UI_PLAY_LEVEL");
		m_ProgressText.transform.parent.gameObject.SetActive(value: false);
		if (m_Item.IsCampaign() && m_Item.IsSubscribed())
		{
			int numCompletedLevels = WorkshopCampaigns.GetNumCompletedLevels(m_Item.GetId());
			int numLevels = WorkshopCampaigns.GetNumLevels(m_Item.GetId());
			if (numLevels > 0)
			{
				m_ProgressText.transform.parent.gameObject.SetActive(value: true);
				m_ProgressText.text = $"{numCompletedLevels}/{numLevels}";
			}
		}
	}

	public void SetHidden()
	{
		base.gameObject.SetActive(value: false);
	}

	public void SetImageTexture(Texture2D texture)
	{
		m_RawImage.color = Color.white;
		m_RawImage.texture = texture;
		if (m_RawImage.texture != null)
		{
			Utils.SizeRawImageToParent(m_RawImage);
		}
	}

	public void OnForget()
	{
		WorkshopRecentlyPlayed.ForgetItem(m_Item.GetId());
		WorkshopCaches.m_Caches[WorkshopTab.LEVELS].Clear();
		base.gameObject.SetActive(value: false);
	}

	public void OnPlay()
	{
		OnPlay(useUnlimitedBudget: false, useUnlimitedMaterials: false);
	}

	public void OnPlay(bool useUnlimitedBudget, bool useUnlimitedMaterials)
	{
		if (!m_Item.IsInstalled())
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else if (m_Item.IsCampaign())
		{
			WorkshopCampaigns.ActivateWorkshopCampaignMod(m_Item.GetId());
			WorkshopCampaign workshopCampaign = WorkshopCampaigns.Get(m_Item.GetId());
			if (workshopCampaign == null)
			{
				InterfaceAudio.PlayErrorBeep();
				return;
			}
			InterfaceAudio.Play("ui_menu_select");
			if (GameUI.m_Instance.m_Workshop.m_WorkshopItemPanel.gameObject.activeInHierarchy)
			{
				GameUI.m_Instance.m_Workshop.m_WorkshopItemPanel.Close();
			}
			GameUI.m_Instance.m_Workshop.m_WorkshopCampaignPanel.Open(workshopCampaign, string.Empty, string.Empty);
		}
		else
		{
			InterfaceAudio.Play("ui_menu_select");
			m_Item.Play(useUnlimitedBudget, useUnlimitedMaterials);
		}
	}

	private void OnBody()
	{
		GameUI.m_Instance.m_Workshop.m_WorkshopItemPanel.Open(this);
	}

	private void UpdateNumUpVotes()
	{
		m_NumUpVotes.text = m_Item.m_SteamItem.VotesUp.ToString();
		m_NumDownVotes.text = m_Item.m_SteamItem.VotesDown.ToString();
	}

	private void UpdateInfoToolTipText()
	{
		if (m_Item == null)
		{
			m_InfoToolTipText.m_Text = string.Empty;
			return;
		}
		string text = GameUI.MarkupForGold(Localize.Get("UI_CREATED_BY") + ":") + "\n" + m_Item.GetCreatorNameNoRichText();
		string text2 = GameUI.MarkupForGold(Localize.Get("UI_WORKSHOP_LAST_UPDATED") + ":") + "\n" + Utils.FormatShortDate(m_Item.GetLastUpdatedDate());
		m_InfoToolTipText.m_Text = text + "\n" + text2;
	}
}
