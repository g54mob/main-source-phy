using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapIsland : MonoBehaviour
{
	[Header("Rigging")]
	public CampaignWorld m_World;

	[Header("UI")]
	public Button m_Button;

	public Image m_SelectedImage;

	public Image m_Image;

	public Image m_SelectedBrackets;

	[Header("Progress")]
	public GameObject m_ProgressBar;

	public RectTransform m_FillingRectTransform;

	public Image m_Filled;

	[NonSerialized]
	public bool m_ShowUnlockPopup;

	[NonSerialized]
	public Vector2 m_OriginalAnchoredPosition;

	private float MAX_FILLING_WIDTH = 58f;

	private PointerEvents m_PointerEvents;

	private WorkshopCampaignWorld m_WorkshopCampaignWorld;

	private ButtonHoverScale m_ButtonHoverScale;

	private int m_NumUpdatesSinceEnable;

	private void Awake()
	{
		m_PointerEvents = GetComponent<PointerEvents>();
		m_Image.alphaHitTestMinimumThreshold = 0.1f;
		m_SelectedBrackets.gameObject.SetActive(value: false);
		m_OriginalAnchoredPosition = GetComponent<RectTransform>().anchoredPosition;
		m_ButtonHoverScale = GetComponent<ButtonHoverScale>();
	}

	private void Start()
	{
		m_Button.onClick.AddListener(OnClicked);
	}

	public void OnEnable()
	{
		m_ButtonHoverScale.enabled = false;
		m_NumUpdatesSinceEnable = 0;
		UpdateIcon();
		UpdateProgressBar();
	}

	public void Update()
	{
		UpdateIcon();
		m_NumUpdatesSinceEnable++;
		if (m_NumUpdatesSinceEnable > 2)
		{
			m_ButtonHoverScale.enabled = true;
		}
	}

	public bool HasBeenPlayed()
	{
		foreach (KeyValuePair<string, string> lastPlayedLevelID in Profiles.m_ActiveProfile.m_LastPlayedLevelIDs)
		{
			if (lastPlayedLevelID.Key == m_World.m_Id)
			{
				return true;
			}
		}
		return false;
	}

	public bool IsUnderPointer()
	{
		if (!(m_PointerEvents != null))
		{
			return false;
		}
		return m_PointerEvents.m_IsHovering;
	}

	public string GetDisplayName()
	{
		if (m_WorkshopCampaignWorld == null)
		{
			return m_World.GetDisplayName();
		}
		return m_WorkshopCampaignWorld.m_DisplayName;
	}

	public string GetSubTitle()
	{
		if (m_WorkshopCampaignWorld == null)
		{
			return m_World.GetDescription();
		}
		return m_WorkshopCampaignWorld.m_Subtitle;
	}

	public bool IsLocked()
	{
		return !IsUnLocked();
	}

	public bool IsUnLocked()
	{
		if (m_WorkshopCampaignWorld == null)
		{
			return m_World.IsUnLocked();
		}
		return true;
	}

	public bool IsSecretWorld()
	{
		if (m_WorkshopCampaignWorld == null)
		{
			return m_World.IsSecretWorld();
		}
		return false;
	}

	public int GetNumStars()
	{
		if (m_WorkshopCampaignWorld == null)
		{
			return m_World.m_NumStars;
		}
		return m_WorkshopCampaignWorld.m_NumStars;
	}

	public Sprite GetIcon()
	{
		if (m_WorkshopCampaignWorld == null || !(m_WorkshopCampaignWorld.m_IconSprite != null))
		{
			return m_World.m_ThemePreloadStub.m_Icon;
		}
		return m_WorkshopCampaignWorld.m_IconSprite;
	}

	public Sprite GetSelectedIcon()
	{
		if (m_WorkshopCampaignWorld == null || !(m_WorkshopCampaignWorld.m_IconSpriteSelected != null))
		{
			return m_World.m_ThemePreloadStub.m_IconSelected;
		}
		return m_WorkshopCampaignWorld.m_IconSpriteSelected;
	}

	public Sprite GetSilouetteIcon()
	{
		return m_World.m_ThemePreloadStub.m_IconSilouette;
	}

	public void SetWorkshopCampaignWorld(WorkshopCampaignWorld world)
	{
		m_WorkshopCampaignWorld = world;
	}

	private void UpdateIcon()
	{
		if (IsLocked())
		{
			m_Image.sprite = GetSilouetteIcon();
		}
		else
		{
			m_Image.sprite = (IsUnderPointer() ? GetSelectedIcon() : GetIcon());
		}
		if (IsSecretWorld() && IsLocked())
		{
			m_Image.gameObject.SetActive(value: false);
		}
		else
		{
			m_Image.gameObject.SetActive(value: true);
		}
		m_SelectedImage.sprite = GetSelectedIcon();
		m_SelectedImage.gameObject.SetActive(IsUnderPointer() && IsUnLocked());
		if (m_WorkshopCampaignWorld != null)
		{
			m_SelectedBrackets.gameObject.SetActive(GameUI.m_Instance.m_Workshop.m_WorkshopCampaignPanel.GetSelectedWorldID() == m_WorkshopCampaignWorld.m_Id);
		}
		else
		{
			m_SelectedBrackets.gameObject.SetActive(GameUI.m_Instance.m_Campaign.GetSelectedWorldID() == m_World.m_Id);
		}
	}

	private void UpdateProgressBar()
	{
		m_ProgressBar.gameObject.SetActive(!IsLocked());
		float nomrmalizedProgress = GetNomrmalizedProgress();
		m_FillingRectTransform.sizeDelta = new Vector2(nomrmalizedProgress * MAX_FILLING_WIDTH, m_FillingRectTransform.sizeDelta.y);
		bool flag = Mathf.Approximately(nomrmalizedProgress, 1f);
		m_FillingRectTransform.gameObject.SetActive(!flag);
		m_Filled.gameObject.SetActive(flag);
		m_Filled.color = (Is100PercentComplete() ? GameUI.m_Instance.m_GoldColor : Color.white);
	}

	private float GetNomrmalizedProgress()
	{
		if (m_WorkshopCampaignWorld != null)
		{
			if (m_WorkshopCampaignWorld.m_LevelIds == null || m_WorkshopCampaignWorld.m_LevelIds.Count == 0)
			{
				return 0f;
			}
			return Mathf.Clamp01((float)m_WorkshopCampaignWorld.GetNumPassedLevels() / (float)m_WorkshopCampaignWorld.GetNumLevels());
		}
		if (m_World.m_Levels == null || m_World.m_Levels.Length == 0)
		{
			return 0f;
		}
		return Mathf.Clamp01((float)m_World.GetNumPassedLevels() / (float)m_World.m_Levels.Length);
	}

	private bool Is100PercentComplete()
	{
		if (m_WorkshopCampaignWorld != null)
		{
			if (m_WorkshopCampaignWorld.m_LevelIds == null || m_WorkshopCampaignWorld.m_LevelIds.Count == 0)
			{
				return false;
			}
			return m_WorkshopCampaignWorld.Is100PercentComplete();
		}
		if (m_World.m_Levels == null || m_World.m_Levels.Length == 0)
		{
			return false;
		}
		return m_World.Is100PercentComplete();
	}

	private void OnClicked()
	{
		if (IsLocked())
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		InterfaceAudio.Play("ui_menu_select");
		if (m_WorkshopCampaignWorld != null)
		{
			WorkshopCampaign withWorld = WorkshopCampaigns.GetWithWorld(m_WorkshopCampaignWorld);
			GameUI.m_Instance.m_Workshop.m_WorkshopCampaignPanel.Open(withWorld, string.Empty, m_WorkshopCampaignWorld.m_Id);
		}
		else
		{
			GameUI.m_Instance.m_Campaign.Open(Profiles.m_ActiveProfile.GetLastPlayedLevelIDForWorld(m_World.m_Id), m_World.m_Id);
		}
	}
}
