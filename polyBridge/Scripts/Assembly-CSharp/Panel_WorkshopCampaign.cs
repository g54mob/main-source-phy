using System;
using System.Collections.Generic;
using Steamworks;
using Steamworks.Data;
using Steamworks.Ugc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_WorkshopCampaign : MonoBehaviour
{
	public RectTransform m_Root;

	public RectTransform m_Ducking;

	[Header("Header")]
	public Banner m_Banner;

	public TextMeshProUGUI m_ProgressText;

	public Button m_CancelButton;

	public TextMeshProUGUI m_CampaignName;

	public TextMeshProUGUI m_WorldName;

	public TextMeshProUGUI m_WorldDifficulty;

	public TextMeshProUGUI m_WorldSubtitle;

	[Header("World Selection")]
	public Panel_WorkshopCampaignWorldSelection m_WorldSelection;

	[Header("Level List")]
	public Panel_FileLoader m_FileLoader;

	public GameObject m_LevelsWaitAnimation;

	[Header("Thumbnail")]
	public RawImage m_RawImage;

	public Button m_ThumbnailButton;

	[Header("Level Status")]
	public GameObject m_PassStatus;

	public GameObject m_UnderBudgetStatus;

	public GameObject m_UnbreakingStatus;

	[Header("Level Details")]
	public TextMeshProUGUI m_LevelName;

	public TextMeshProUGUI m_LevelAuthor;

	public TextMeshProUGUI m_LevelDescription;

	public TextMeshProUGUI m_LevelBudget;

	public GameObject[] m_MaterialIconObjects;

	public UnityEngine.UI.Image m_RoadIcon;

	public UnityEngine.UI.Image m_WoodIcon;

	public UnityEngine.UI.Image m_SteelIcon;

	public UnityEngine.UI.Image m_HydraulicIcon;

	public UnityEngine.UI.Image m_RopeIcon;

	public UnityEngine.UI.Image m_CableIcon;

	public UnityEngine.UI.Image m_SpringIcon;

	public UnityEngine.UI.Image m_PillarIcon;

	[Header("Footer")]
	public Button m_PlayButton;

	public GameObject m_PlayButtonTextAndIcon;

	public GameObject m_PlayButtonWaiting;

	[Header("Cheats")]
	public GameObject m_UnlimitedBudgetLocked;

	public GameObject m_UnlimitedMaterialsLocked;

	public TwoStateButton m_UnlimitedBudgetButton;

	public TwoStateButton m_UnlimitedMaterialsButton;

	[HideInInspector]
	public bool m_ReturnToGameOnClose;

	private Dictionary<FileSlot, WorkshopCampaignLevel> m_SlotLevels = new Dictionary<FileSlot, WorkshopCampaignLevel>();

	private float m_LastClickTime;

	private FileSlot m_SelectedSlot;

	private int m_SelectedSlotIndex;

	private int m_SelectedSlotSetOnFrameCount;

	private string m_LevelIdToSelectAfterInitialLoad;

	private WorkshopCampaign m_WorkshopCampaign;

	private WorkshopCampaignWorld m_WorkshopCampaignWorld;

	private void Awake()
	{
		m_UnlimitedBudgetButton.m_Button.onClick.AddListener(OnUnlimitedBudgetToggle);
		m_UnlimitedMaterialsButton.m_Button.onClick.AddListener(OnUnlimitedMaterialToggle);
		m_LevelsWaitAnimation.gameObject.SetActive(value: false);
		m_RawImage.uvRect = new Rect(m_RawImage.uvRect.x, m_RawImage.uvRect.y, m_RawImage.uvRect.width, 0.999f);
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
		GameUI.m_Instance.m_Workshop.m_RootPanel.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_Workshop.m_Ducking.gameObject.SetActive(value: false);
		UpdateHeader();
		ShowGamepadLegend();
	}

	private void OnDisable()
	{
		m_ReturnToGameOnClose = false;
		GameUI.m_Instance.m_Workshop.m_RootPanel.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_Workshop.m_Ducking.gameObject.SetActive(value: true);
		ActivePanels.Remove(base.gameObject);
	}

	private void Start()
	{
		m_CancelButton.onClick.AddListener(OnCancel);
		m_PlayButton.onClick.AddListener(OnPlay);
		m_ThumbnailButton.onClick.AddListener(OnThumbnail);
	}

	public void Open(WorkshopCampaign campaign, string levelID, string worldID)
	{
		if (campaign == null)
		{
			return;
		}
		if (m_WorkshopCampaign != null && m_WorkshopCampaign.m_Id != campaign.m_Id)
		{
			m_WorkshopCampaignWorld = null;
		}
		m_WorkshopCampaign = campaign;
		m_WorkshopCampaignWorld = null;
		WorkshopCampaignProgress.Load(m_WorkshopCampaign);
		if (!string.IsNullOrEmpty(levelID))
		{
			m_WorkshopCampaignWorld = m_WorkshopCampaign.GetWorldWithLevelId(levelID);
		}
		else if (!string.IsNullOrEmpty(worldID))
		{
			m_WorkshopCampaignWorld = m_WorkshopCampaign.GetWorld(worldID);
		}
		if (m_WorkshopCampaignWorld == null)
		{
			if (m_WorkshopCampaign.m_Worlds.Count == 0)
			{
				return;
			}
			m_WorkshopCampaignWorld = m_WorkshopCampaign.GetWorldWithIndex(0);
		}
		if (m_WorkshopCampaignWorld.m_LevelIds.Count != 0)
		{
			if (!base.gameObject.activeInHierarchy)
			{
				base.gameObject.SetActive(value: true);
				WorldSelectionInitForCampaign(m_WorkshopCampaign);
			}
			m_WorkshopCampaign.ClearUnlimitedBudgetAndMaterialFlags();
			m_RawImage.gameObject.SetActive(value: false);
			List<string> listOfLevelIdsToQuery = GetListOfLevelIdsToQuery(m_WorkshopCampaign);
			if (listOfLevelIdsToQuery.Count > 0)
			{
				m_LevelsWaitAnimation.gameObject.SetActive(value: true);
				m_LevelIdToSelectAfterInitialLoad = levelID;
				QueryLevelList(listOfLevelIdsToQuery, LevelListQueryComplete);
			}
			else
			{
				PopulateSlots(m_WorkshopCampaignWorld);
				SelectLevel(levelID);
			}
			UpdateHeader();
			ShowDownloading(on: false);
			GameAchievements.InvalidateSpeedRunnerTimer();
			m_LastClickTime = 0f;
		}
	}

	private List<string> GetListOfLevelIdsToQuery(WorkshopCampaign campaign)
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, WorkshopCampaignWorld> world in campaign.m_Worlds)
		{
			foreach (string levelId in world.Value.m_LevelIds)
			{
				if (WorkshopCampaignsLevelCache.Get(levelId) == null)
				{
					list.Add(levelId);
				}
			}
		}
		return list;
	}

	private void WorldSelectionInitForCampaign(WorkshopCampaign campaign)
	{
		m_WorldSelection.DisableAllWorlds();
		foreach (KeyValuePair<string, WorkshopCampaignWorld> world in campaign.m_Worlds)
		{
			m_WorldSelection.SetWorkshopCampaignWorld(world.Value.m_Index, world.Value);
			m_WorldSelection.EnableWorld(world.Value.m_Index, active: true);
			if (world.Value.m_UseCustomPosition)
			{
				m_WorldSelection.SetIconPosition(world.Value.m_Index, world.Value.m_IconPosition);
			}
			else
			{
				m_WorldSelection.SetIconDefaultPosition(world.Value.m_Index);
			}
		}
	}

	public string GetSelectedWorldID()
	{
		if (m_WorkshopCampaignWorld == null)
		{
			return string.Empty;
		}
		return m_WorkshopCampaignWorld.m_Id;
	}

	public string GetSelectedLevelID()
	{
		if (m_SelectedSlot == null)
		{
			return string.Empty;
		}
		WorkshopCampaignLevel levelForSlot = GetLevelForSlot(m_SelectedSlot);
		if (levelForSlot == null)
		{
			return string.Empty;
		}
		return levelForSlot.GetId();
	}

	public WorkshopItem GetSelectedItem()
	{
		if (m_SelectedSlot == null)
		{
			return null;
		}
		return GetLevelForSlot(m_SelectedSlot)?.m_WorkshopItem;
	}

	private void Update()
	{
		ProcessInput();
		RefreshPlayButton();
		WorkshopCampaignLevel levelForSlot = GetLevelForSlot(m_SelectedSlot);
		if (levelForSlot != null && levelForSlot.m_WorkshopItem != null)
		{
			UpdateLevelThumbnail(levelForSlot);
		}
		if (ActivePanels.IsTopPanel(base.gameObject))
		{
			ShowGamepadLegend();
		}
	}

	public void Close()
	{
		if (m_ReturnToGameOnClose)
		{
			GameUI.m_Instance.m_Workshop.Close();
			m_ReturnToGameOnClose = false;
		}
		base.gameObject.SetActive(value: false);
	}

	private void RefreshPlayButton()
	{
		WorkshopCampaignLevel levelForSlot = GetLevelForSlot(m_SelectedSlot);
		m_PlayButton.gameObject.SetActive(!IsLocked(levelForSlot));
	}

	private void SelectLevel(string levelId)
	{
		WorkshopCampaignLevel level = m_WorkshopCampaignWorld.GetLevel(levelId);
		if (level == null)
		{
			return;
		}
		foreach (FileSlot slot in m_FileLoader.m_Slots)
		{
			if (slot.m_FileName == level.GetId())
			{
				SetSelectedSlot(slot);
				break;
			}
		}
	}

	private void UpdateHeader()
	{
		m_CampaignName.text = m_WorkshopCampaign.GetTitle();
		m_WorldName.text = m_WorkshopCampaignWorld.m_DisplayName;
		m_WorldDifficulty.text = Campaign.FormatDifficultyLabel(m_WorkshopCampaignWorld.m_NumStars);
		m_WorldSubtitle.text = m_WorkshopCampaignWorld.m_Subtitle;
		m_WorldSubtitle.gameObject.SetActive(!Game.IsRunningOnSteamDeck());
		m_Banner.Refresh();
		UpdateProgress();
	}

	private void UpdateProgress()
	{
		int numCompletedLevels = WorkshopCampaigns.GetNumCompletedLevels(m_WorkshopCampaign.m_Id);
		int numLevels = WorkshopCampaigns.GetNumLevels(m_WorkshopCampaign.m_Id);
		if (numLevels > 0)
		{
			m_ProgressText.transform.parent.gameObject.SetActive(value: true);
			m_ProgressText.text = $"{numCompletedLevels}/{numLevels}";
		}
		else
		{
			m_ProgressText.text = string.Empty;
		}
	}

	private void PopulateSlots(WorkshopCampaignWorld world)
	{
		m_FileLoader.DestroySlots();
		m_SlotLevels.Clear();
		AddCampaignLevels(world);
		SetSelectedSlot(m_FileLoader.GetFirstSlot());
	}

	private void AddCampaignLevels(WorkshopCampaignWorld world)
	{
		List<WorkshopCampaignLevel> list = new List<WorkshopCampaignLevel>();
		foreach (string levelId in world.m_LevelIds)
		{
			WorkshopCampaignLevel workshopCampaignLevel = WorkshopCampaignsLevelCache.Get(levelId);
			if (workshopCampaignLevel != null)
			{
				list.Add(workshopCampaignLevel);
			}
		}
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < list.Count; i++)
		{
			WorkshopCampaignLevel workshopCampaignLevel2 = list[i];
			string displayName = Localize.Get(workshopCampaignLevel2.GetTitle());
			FileSlot fileSlot = m_FileLoader.AddSlot(workshopCampaignLevel2.GetId(), 0L, displayName, SlotClickedCallback, null);
			if (!(fileSlot != null))
			{
				continue;
			}
			if (WorkshopCampaigns.IsLevelATutorial(workshopCampaignLevel2.GetId()))
			{
				if (world.m_Tutorials.Count > 1)
				{
					fileSlot.m_Prefix.text = $"T{num2 + 1}";
					num2++;
				}
				else
				{
					fileSlot.m_Prefix.text = "T";
				}
				num--;
			}
			else
			{
				fileSlot.m_Prefix.text = $"{i + 1 + num}";
			}
			fileSlot.m_Prefix.color = GameUI.m_Instance.m_GoldColor;
			CampaignLevelStatus levelStatus = m_WorkshopCampaign.m_CampaignProgress.GetLevelStatus(workshopCampaignLevel2.GetId());
			fileSlot.SetStatusIcon(levelStatus);
			m_SlotLevels.Add(fileSlot, workshopCampaignLevel2);
		}
	}

	private void SlotClickedCallback(FileSlot slot)
	{
		if ((bool)slot && !IsDownloading() && (m_SlotLevels.ContainsKey(slot) ? m_SlotLevels[slot] : null) != null)
		{
			float num = Time.realtimeSinceStartup - m_LastClickTime;
			m_LastClickTime = Time.realtimeSinceStartup;
			if (slot == m_SelectedSlot && num < GameUI.DOUBLE_CLICK_THRESHOLD_SECONDS)
			{
				OnPlay();
			}
			if (slot != m_SelectedSlot)
			{
				SetSelectedSlot(slot);
				InterfaceAudio.Play("ui_menu_select");
			}
		}
	}

	private void SetSelectedSlot(FileSlot slot)
	{
		if (slot == null)
		{
			m_ThumbnailButton.gameObject.SetActive(value: false);
			return;
		}
		m_ThumbnailButton.gameObject.SetActive(value: true);
		m_SelectedSlot = slot;
		m_SelectedSlotIndex = m_FileLoader.GetSlotIndex(slot);
		m_SelectedSlotSetOnFrameCount = Time.frameCount;
		m_FileLoader.SelectSlot(slot);
		WorkshopCampaignLevel workshopCampaignLevel = (m_SlotLevels.ContainsKey(slot) ? m_SlotLevels[slot] : null);
		if (workshopCampaignLevel != null)
		{
			UpdateLevelStatusPanel(workshopCampaignLevel);
			UpdateLevelInfoPanel(workshopCampaignLevel);
			UpdateLevelThumbnail(workshopCampaignLevel);
		}
	}

	private void UpdateLevelStatusPanel(WorkshopCampaignLevel level)
	{
		CampaignLevelStatus levelStatus = m_WorkshopCampaign.m_CampaignProgress.GetLevelStatus(level.GetId());
		m_PassStatus.SetActive(levelStatus == CampaignLevelStatus.PASS || levelStatus == CampaignLevelStatus.UNDER_BUDGET || levelStatus == CampaignLevelStatus.UNDER_BUDGET_NO_BREAKS);
		m_UnderBudgetStatus.SetActive(levelStatus == CampaignLevelStatus.UNDER_BUDGET || levelStatus == CampaignLevelStatus.UNDER_BUDGET_NO_BREAKS);
		m_UnbreakingStatus.SetActive(levelStatus == CampaignLevelStatus.UNDER_BUDGET_NO_BREAKS);
	}

	private void UpdateLevelInfoPanel(WorkshopCampaignLevel level)
	{
		m_LevelAuthor.text = Localize.Get("UI_WORKSHOP_BY", level.m_WorkshopItem.GetCreatorName());
		m_LevelName.text = m_WorkshopCampaignWorld.GetFormattedLevelNameWithPrefix(level.m_WorkshopItem);
		m_LevelDescription.text = level.GetDescription();
		PopulateBudgetAndMaterials(level.GetMetaData());
		UpdateCheatToggles(level);
	}

	private void PopulateBudgetAndMaterials(string metadata)
	{
		if (!string.IsNullOrEmpty(metadata))
		{
			int budget = WorkshopMetaData.GetBudget(metadata);
			m_LevelBudget.text = Utils.FormatCash(budget);
			List<int> materialCounts = WorkshopMetaData.GetMaterialCounts(metadata);
			for (int i = 0; i < m_MaterialIconObjects.Length; i++)
			{
				m_MaterialIconObjects[i].SetActive(materialCounts != null && i < materialCounts.Count && materialCounts[i] > 0);
				WorkshopMetaData.SetMaterialCountForIcon(m_MaterialIconObjects[i], (!WorkshopMetaData.IsLegacy(metadata)) ? materialCounts[i] : 0);
			}
		}
	}

	private void UpdateCheatToggles(WorkshopCampaignLevel level)
	{
		m_UnlimitedBudgetLocked.SetActive(!m_WorkshopCampaign.m_CampaignProgress.HasCompletedLevelUnderBudget(level.GetId()));
		m_UnlimitedMaterialsLocked.SetActive(!m_WorkshopCampaign.m_CampaignProgress.HasCompletedLevelUnderBudgetNoBreaks(level.GetId()));
		m_UnlimitedBudgetButton.gameObject.SetActive(!m_UnlimitedBudgetLocked.activeSelf);
		m_UnlimitedMaterialsButton.gameObject.SetActive(!m_UnlimitedMaterialsLocked.activeSelf);
		m_UnlimitedBudgetButton.TurnOn(level.m_UnlimitedBudget);
		m_UnlimitedMaterialsButton.TurnOn(level.m_UnlimitedMaterial);
	}

	private void DisableAllMaterialIcons()
	{
		m_RoadIcon.gameObject.SetActive(value: false);
		m_WoodIcon.gameObject.SetActive(value: false);
		m_SteelIcon.gameObject.SetActive(value: false);
		m_HydraulicIcon.gameObject.SetActive(value: false);
		m_RopeIcon.gameObject.SetActive(value: false);
		m_CableIcon.gameObject.SetActive(value: false);
		m_SpringIcon.gameObject.SetActive(value: false);
	}

	private void EnableMaterialIcons(BudgetProxy budgetProxy)
	{
		m_RoadIcon.gameObject.SetActive(budgetProxy.m_RoadBudget > 0);
		m_WoodIcon.gameObject.SetActive(budgetProxy.m_AllowWood && budgetProxy.m_WoodBudget > 0);
		m_SteelIcon.gameObject.SetActive(budgetProxy.m_AllowSteel && budgetProxy.m_SteelBudget > 0);
		m_HydraulicIcon.gameObject.SetActive(budgetProxy.m_AllowHydraulic && budgetProxy.m_HydraulicBudget > 0);
		m_RopeIcon.gameObject.SetActive(budgetProxy.m_AllowRope && budgetProxy.m_RopeBudget > 0);
		m_CableIcon.gameObject.SetActive(budgetProxy.m_AllowCable && budgetProxy.m_CableBudget > 0);
		m_SpringIcon.gameObject.SetActive(budgetProxy.m_AllowSpring && budgetProxy.m_SpringBudget > 0);
		m_PillarIcon.gameObject.SetActive(budgetProxy.m_AllowPillar && budgetProxy.m_PillarBudget > 0);
	}

	private void SetMaterialLimits(BudgetProxy budgetProxy, BridgeSaveData bridgeSaveData)
	{
		MaterialLimit componentInChildren = m_RoadIcon.GetComponentInChildren<MaterialLimit>(includeInactive: true);
		componentInChildren.Set(budgetProxy.m_RoadBudget - bridgeSaveData.GetNumSoftPrebuiltMaterials(BridgeMaterialType.ROAD));
		componentInChildren.gameObject.SetActive(budgetProxy.m_RoadBudget != Budget.UNLIMITED_MATERIAL_BUDGET && budgetProxy.m_RoadBudget != 0);
		MaterialLimit componentInChildren2 = m_WoodIcon.GetComponentInChildren<MaterialLimit>(includeInactive: true);
		componentInChildren2.Set(budgetProxy.m_WoodBudget - bridgeSaveData.GetNumSoftPrebuiltMaterials(BridgeMaterialType.WOOD));
		componentInChildren2.gameObject.SetActive(budgetProxy.m_WoodBudget != Budget.UNLIMITED_MATERIAL_BUDGET && budgetProxy.m_WoodBudget != 0);
		MaterialLimit componentInChildren3 = m_SteelIcon.GetComponentInChildren<MaterialLimit>(includeInactive: true);
		componentInChildren3.Set(budgetProxy.m_SteelBudget - bridgeSaveData.GetNumSoftPrebuiltMaterials(BridgeMaterialType.STEEL));
		componentInChildren3.gameObject.SetActive(budgetProxy.m_SteelBudget != Budget.UNLIMITED_MATERIAL_BUDGET && budgetProxy.m_SteelBudget != 0);
		MaterialLimit componentInChildren4 = m_HydraulicIcon.GetComponentInChildren<MaterialLimit>(includeInactive: true);
		componentInChildren4.Set(budgetProxy.m_HydraulicBudget - bridgeSaveData.GetNumSoftPrebuiltMaterials(BridgeMaterialType.HYDRAULICS));
		componentInChildren4.gameObject.SetActive(budgetProxy.m_HydraulicBudget != Budget.UNLIMITED_MATERIAL_BUDGET && budgetProxy.m_HydraulicBudget != 0);
		MaterialLimit componentInChildren5 = m_RopeIcon.GetComponentInChildren<MaterialLimit>(includeInactive: true);
		componentInChildren5.Set(budgetProxy.m_RoadBudget - bridgeSaveData.GetNumSoftPrebuiltMaterials(BridgeMaterialType.ROPE));
		componentInChildren5.gameObject.SetActive(budgetProxy.m_RopeBudget != Budget.UNLIMITED_MATERIAL_BUDGET && budgetProxy.m_RopeBudget != 0);
		MaterialLimit componentInChildren6 = m_CableIcon.GetComponentInChildren<MaterialLimit>(includeInactive: true);
		componentInChildren6.Set(budgetProxy.m_CableBudget - bridgeSaveData.GetNumSoftPrebuiltMaterials(BridgeMaterialType.CABLE));
		componentInChildren6.gameObject.SetActive(budgetProxy.m_CableBudget != Budget.UNLIMITED_MATERIAL_BUDGET && budgetProxy.m_CableBudget != 0);
		MaterialLimit componentInChildren7 = m_SpringIcon.GetComponentInChildren<MaterialLimit>(includeInactive: true);
		componentInChildren7.Set(budgetProxy.m_SpringBudget - bridgeSaveData.GetNumSoftPrebuiltMaterials(BridgeMaterialType.SPRING));
		componentInChildren7.gameObject.SetActive(budgetProxy.m_SpringBudget != Budget.UNLIMITED_MATERIAL_BUDGET && budgetProxy.m_SpringBudget != 0);
		MaterialLimit componentInChildren8 = m_PillarIcon.GetComponentInChildren<MaterialLimit>(includeInactive: true);
		componentInChildren8.Set(budgetProxy.m_PillarBudget - bridgeSaveData.GetNumSoftPrebuiltMaterials(BridgeMaterialType.PILLAR));
		componentInChildren8.gameObject.SetActive(budgetProxy.m_PillarBudget != Budget.UNLIMITED_MATERIAL_BUDGET && budgetProxy.m_PillarBudget != 0);
	}

	private void UpdateLevelThumbnail(WorkshopCampaignLevel level)
	{
		if (level.m_WorkshopItem.m_PreviewTexture != null)
		{
			m_RawImage.texture = level.m_WorkshopItem.m_PreviewTexture;
			if (!m_RawImage.gameObject.activeInHierarchy)
			{
				m_RawImage.gameObject.SetActive(value: true);
			}
		}
		else if (m_RawImage.gameObject.activeInHierarchy)
		{
			m_RawImage.gameObject.SetActive(value: false);
		}
	}

	private void TryLoadPreviewThumb(WorkshopCampaignLevel level)
	{
		level.m_WorkshopItem.DownloadPreviewFromSteam(null);
	}

	private void OnCancel()
	{
		if (!m_ReturnToGameOnClose)
		{
			WorkshopCampaigns.DeactivateCurrentWorkshopCampaignMod();
		}
		InterfaceAudio.Play("ui_window_close");
		Close();
	}

	private void OnPlay()
	{
		if (IsDownloading() || m_SelectedSlot == null)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		WorkshopCampaignLevel levelForSlot = GetLevelForSlot(m_SelectedSlot);
		if (levelForSlot == null || levelForSlot.m_WorkshopItem == null)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		InterfaceAudio.Play("ui_menu_select");
		if (Utils.FileExists(levelForSlot.m_WorkshopItem.GetLevelLayoutPathAndFilename()))
		{
			Play(success: true);
			return;
		}
		ShowDownloading(on: true);
		levelForSlot.m_WorkshopItem.DownloadFromSteam(Play);
	}

	private void Play(bool success)
	{
		ShowDownloading(on: false);
		if (!success)
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("WARN_WORKSHOP_DOWNLOAD_FAIL"));
			return;
		}
		WorkshopCampaignLevel levelForSlot = GetLevelForSlot(m_SelectedSlot);
		if (levelForSlot != null && levelForSlot.m_WorkshopItem != null)
		{
			WorkshopRecentlyPlayed.SaveCampaign(m_WorkshopCampaign.GetId());
			List<string> inactiveModsInLayout = Mods.GetInactiveModsInLayout(levelForSlot.m_WorkshopItem.GetLevelLayoutPathAndFilename());
			if (inactiveModsInLayout.Count > 0)
			{
				GameUI.m_Instance.m_ModsRequiredPopup.Open(inactiveModsInLayout, null, DoPlayAfterModCheck);
				return;
			}
			Mods.DeactivateAutoLoadedMods();
			DoPlayAfterModCheck(null);
		}
	}

	private void DoPlayAfterModCheck(FileSlot slot)
	{
		BridgeCheat.Clear();
		WorkshopCampaignLevel levelForSlot = GetLevelForSlot(m_SelectedSlot);
		if (levelForSlot != null && levelForSlot.m_WorkshopItem != null)
		{
			BridgeCheat.m_ForceUnlimitedBudget = levelForSlot.m_UnlimitedBudget;
			BridgeCheat.m_ForceUnlimitedMaterial = levelForSlot.m_UnlimitedMaterial;
			GameStatePreloadingAssets.PreloadLevel(levelForSlot.m_WorkshopItem.GetLevelLayoutPathAndFilename(), slot, PreloadOpenLevelCallback);
		}
	}

	private void PreloadOpenLevelCallback(string layoutPath, FileSlot slot)
	{
		WorkshopCampaignLevel levelForSlot = GetLevelForSlot(m_SelectedSlot);
		if (levelForSlot != null && levelForSlot.m_WorkshopItem != null)
		{
			BridgeCheat.Clear();
			Workshop.PlayLevel(levelForSlot.m_WorkshopItem, layoutPath, GameSubMode.NONE);
			Close();
			GameUI.m_Instance.m_Workshop.Close();
			GameUI.m_Instance.m_Gallery.Close();
		}
	}

	private void ShowDownloading(bool on)
	{
		m_PlayButtonTextAndIcon.SetActive(!on);
		m_PlayButtonWaiting.SetActive(on);
	}

	private bool IsDownloading()
	{
		return m_PlayButtonWaiting.activeInHierarchy;
	}

	private void OnThumbnail()
	{
		float num = Time.realtimeSinceStartup - m_LastClickTime;
		m_LastClickTime = Time.realtimeSinceStartup;
		if (num < GameUI.DOUBLE_CLICK_THRESHOLD_SECONDS && !IsLocked(m_SelectedSlot))
		{
			OnPlay();
		}
	}

	private bool IgnoreKeyboardInput()
	{
		if (uConsole.IsOn())
		{
			return true;
		}
		return false;
	}

	private void ProcessInput()
	{
		if (GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
		{
			OnCancel();
		}
		if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && !IsLocked(m_SelectedSlot))
		{
			OnPlay();
		}
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
		{
			ScrollUp();
			GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_START_DELAY;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
		{
			ScrollDown();
			GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_START_DELAY;
		}
		if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && Time.unscaledTime > GameUI.m_NextAutoScrollTime)
		{
			ScrollUp();
			GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_DELAY;
		}
		if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && Time.unscaledTime > GameUI.m_NextAutoScrollTime)
		{
			ScrollDown();
			GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_DELAY;
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH))
		{
			OnPlay();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.SOUTH) && m_SelectedSlot != null)
		{
			FileSlot fileSlotUnderPointer = GameUI.GetFileSlotUnderPointer();
			if (m_SelectedSlot == fileSlotUnderPointer && !IsLocked(m_SelectedSlot) && Time.frameCount > m_SelectedSlotSetOnFrameCount)
			{
				OnPlay();
			}
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_DOWN) || GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_DOWN))
		{
			ProcessDpadDown();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_UP) || GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_UP))
		{
			ProcessDpadUp();
		}
	}

	private void ProcessDpadUp()
	{
		ScrollUp();
		ForceGamepadCursorToSelecctedSlot();
		GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_START_DELAY;
	}

	private void ProcessDpadDown()
	{
		ScrollDown();
		ForceGamepadCursorToSelecctedSlot();
		GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_START_DELAY;
	}

	private void ForceGamepadCursorToSelecctedSlot()
	{
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad && m_SelectedSlotIndex != -1)
		{
			FileSlot fileSlot = m_FileLoader.FindSlotByIndex(m_SelectedSlotIndex);
			if (fileSlot != null && fileSlot.m_Prefix != null)
			{
				GameInput.SetVirtualMousePosition(fileSlot.m_Prefix.transform.position);
			}
		}
	}

	private void ScrollDown()
	{
		m_SelectedSlotIndex++;
		if (m_SelectedSlotIndex >= m_FileLoader.NumSlots())
		{
			m_SelectedSlotIndex = 0;
		}
		SetSelectedSlot(m_FileLoader.FindSlotByIndex(m_SelectedSlotIndex));
		InterfaceAudio.Play("ui_menu_select");
	}

	private void ScrollUp()
	{
		m_SelectedSlotIndex--;
		if (m_SelectedSlotIndex < 0)
		{
			m_SelectedSlotIndex = m_FileLoader.NumSlots() - 1;
		}
		SetSelectedSlot(m_FileLoader.FindSlotByIndex(m_SelectedSlotIndex));
		InterfaceAudio.Play("ui_menu_select");
	}

	private WorkshopCampaignLevel GetLevelForSlot(FileSlot slot)
	{
		if (slot == null)
		{
			return null;
		}
		if (!m_SlotLevels.ContainsKey(slot))
		{
			return null;
		}
		return m_SlotLevels[slot];
	}

	private void OnUnlimitedBudgetToggle()
	{
		InterfaceAudio.Play("ui_menu_select");
		m_UnlimitedBudgetButton.Toggle();
		m_UnlimitedBudgetButton.m_ToolTipText.m_RawLocalizationKey = (m_UnlimitedBudgetButton.IsOn() ? "UI_UNLIMITED_BUDGET_ON" : "UI_UNLIMITED_BUDGET_OFF");
		WorkshopCampaignLevel levelForSlot = GetLevelForSlot(m_SelectedSlot);
		if (levelForSlot != null)
		{
			levelForSlot.m_UnlimitedBudget = m_UnlimitedBudgetButton.IsOn();
		}
	}

	private void OnUnlimitedMaterialToggle()
	{
		InterfaceAudio.Play("ui_menu_select");
		m_UnlimitedMaterialsButton.Toggle();
		m_UnlimitedMaterialsButton.m_ToolTipText.m_RawLocalizationKey = (m_UnlimitedMaterialsButton.IsOn() ? "UI_UNLIMITED_MATERIAL_ON" : "UI_UNLIMITED_MATERIAL_OFF");
		WorkshopCampaignLevel levelForSlot = GetLevelForSlot(m_SelectedSlot);
		if (levelForSlot != null)
		{
			levelForSlot.m_UnlimitedMaterial = m_UnlimitedMaterialsButton.IsOn();
		}
	}

	private bool IsLocked(string levelId)
	{
		return false;
	}

	private bool IsLocked(WorkshopCampaignLevel level)
	{
		if (level == null)
		{
			return true;
		}
		return IsLocked(level.GetId());
	}

	private bool IsLocked(FileSlot slot)
	{
		WorkshopCampaignLevel levelForSlot = GetLevelForSlot(slot);
		if (levelForSlot == null)
		{
			return true;
		}
		return IsLocked(levelForSlot.GetId());
	}

	private async void QueryLevelList(List<string> ids, Action<List<WorkshopItem>> callback)
	{
		List<WorkshopItem> items = new List<WorkshopItem>();
		PublishedFileId[] array = new PublishedFileId[ids.Count];
		for (int i = 0; i < ids.Count; i++)
		{
			array[i] = default(PublishedFileId);
			ulong.TryParse(ids[i], out var result);
			array[i].Value = result;
		}
		try
		{
			ResultPage? resultPage = await Query.All.WithFileId(array).WithMetadata(b: true).WithLongDescription(b: true)
				.GetPageAsync(1);
			if (resultPage.HasValue && resultPage.Value.ResultCount > 0)
			{
				foreach (Item entry in resultPage.Value.Entries)
				{
					if (entry.Result == Result.OK)
					{
						WorkshopItem item = new WorkshopItem(entry);
						items.Add(item);
					}
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Handled Exception: " + ex.Message);
			callback?.Invoke(null);
		}
		callback?.Invoke(items);
	}

	private void LevelListQueryComplete(List<WorkshopItem> items)
	{
		m_LevelsWaitAnimation.gameObject.SetActive(value: false);
		if (items == null)
		{
			return;
		}
		foreach (WorkshopItem item in items)
		{
			WorkshopCampaignLevel level = new WorkshopCampaignLevel(item);
			WorkshopCampaignsLevelCache.Add(item.GetId(), level);
			TryLoadPreviewThumb(level);
		}
		PopulateSlots(m_WorkshopCampaignWorld);
		WorkshopCampaignLevel firstLevel = m_WorkshopCampaignWorld.GetFirstLevel();
		if (firstLevel != null)
		{
			SelectLevel((!string.IsNullOrEmpty(m_LevelIdToSelectAfterInitialLoad)) ? m_LevelIdToSelectAfterInitialLoad : firstLevel.GetId());
		}
	}

	private void ShowGamepadLegend()
	{
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.NORTH, Localize.Get("UI_PLAY_LEVEL"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
	}
}
