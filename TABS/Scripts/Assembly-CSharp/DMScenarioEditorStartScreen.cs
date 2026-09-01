using System;
using System.Linq;
using DM;
using Landfall.TABS;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DMScenarioEditorStartScreen : MonoBehaviour, IBattleCreatorMenu
{
	[SerializeField]
	private Button m_localBattlesBtn;

	[SerializeField]
	private TMP_Text m_localBattlesCount;

	[SerializeField]
	private Button m_workshopBattlesBtn;

	[SerializeField]
	private TMP_Text m_workshopBattlesCount;

	[SerializeField]
	private Button m_localCampaignsBtn;

	[SerializeField]
	private TMP_Text m_localCampaignsCount;

	[SerializeField]
	private Button m_workshopCampaignsBtn;

	[SerializeField]
	private TMP_Text m_workshopCampaignsCount;

	[SerializeField]
	private Button m_saveBattleBtn;

	[SerializeField]
	private Button m_saveCampaignBtn;

	[SerializeField]
	private BattleCreatorAssetHandlingUI m_assetHandlerUI;

	private BattleCreatorTabsUIHandler m_tabsHandler;

	private InputService m_inputService;

	public bool AllowPageChange => true;

	public void Open(BattleCreatorState state, object data)
	{
		m_inputService.OnUIOpen();
		base.gameObject.SetActive(value: true);
		GetComponentInChildren<Selectable>().Select();
		ContentDatabase contentDatabase = ContentDatabase.Instance();
		int? num = contentDatabase.GetUserCampaignLevelsByFilter(Filter.CreateMatchNamePartAndTypeFilter(string.Empty, WorkshopTypeFilter.Local))?.Count();
		int? num2 = contentDatabase.GetUserCampaignsByFilter(Filter.CreateMatchNamePartAndTypeFilter(string.Empty, WorkshopTypeFilter.Local))?.Count();
		m_localBattlesCount.text = num?.ToString();
		m_localCampaignsCount.text = num2?.ToString();
		m_localBattlesBtn.interactable = num > 0;
		m_localCampaignsBtn.interactable = num2 > 0;
	}

	public void Close()
	{
		m_inputService.OnUIClose();
		base.gameObject.SetActive(value: false);
	}

	public bool IsOpen()
	{
		return base.gameObject.activeSelf;
	}

	public void Init(BattleCreatorTabsUIHandler tabsHandler)
	{
		m_tabsHandler = tabsHandler;
		m_inputService = ServiceLocator.GetService<InputService>();
		BindButtons();
	}

	public void Init(CustomContentOverlaysManager overlay)
	{
		throw new NotImplementedException();
	}

	public bool NavigateUIWithController(PlayerActions playerActions)
	{
		if (playerActions.m_back.WasPressed && !m_inputService.IsTextInputCurrentlySelected())
		{
			ScenarioEditorUI.instance.CloseScenarioEditor();
		}
		return false;
	}

	private void BindButtons()
	{
		m_localBattlesBtn.onClick.AddListener(delegate
		{
			OpenLocalBattles();
		});
		m_workshopBattlesBtn.onClick.AddListener(delegate
		{
			OpenWorkshopBattles();
		});
		m_localCampaignsBtn.onClick.AddListener(delegate
		{
			OpenLocalCampaigns();
		});
		m_workshopCampaignsBtn.onClick.AddListener(delegate
		{
			OpenWorkshopCampaigns();
		});
		m_saveBattleBtn.onClick.AddListener(delegate
		{
			OpenSaveBattle();
		});
		m_saveCampaignBtn.onClick.AddListener(delegate
		{
			OpenSaveCampaign();
		});
	}

	public void OpenLocalBattles()
	{
		m_tabsHandler.OpenNewScreen(BattleCreatorScreenState.AssetMenu, BattleCreatorState.Load);
		m_assetHandlerUI.OnSwitchedContentFilter(WorkshopTypeFilter.Local);
	}

	public void OpenWorkshopBattles()
	{
		ScenarioEditorUI.instance.WaitForToken(delegate
		{
			m_tabsHandler.OpenNewScreen(BattleCreatorScreenState.AssetMenu, BattleCreatorState.Load);
			m_assetHandlerUI.OnSwitchedContentFilter(WorkshopTypeFilter.WorkshopSelf);
		});
	}

	public void OpenLocalCampaigns()
	{
		m_tabsHandler.OpenNewScreen(BattleCreatorScreenState.AssetMenu, BattleCreatorState.CampaignCreator);
		m_assetHandlerUI.OnSwitchedContentFilter(WorkshopTypeFilter.Local);
	}

	public void OpenWorkshopCampaigns()
	{
		ScenarioEditorUI.instance.WaitForToken(delegate
		{
			m_tabsHandler.OpenNewScreen(BattleCreatorScreenState.AssetMenu, BattleCreatorState.CampaignCreator);
			m_assetHandlerUI.OnSwitchedContentFilter(WorkshopTypeFilter.WorkshopSelf);
		});
	}

	public void OpenSaveBattle()
	{
		m_tabsHandler.OpenNewScreen(BattleCreatorScreenState.Save, BattleCreatorState.Save);
	}

	public void OpenSaveCampaign()
	{
		m_tabsHandler.OpenNewScreen(BattleCreatorScreenState.TwoList, BattleCreatorState.CampaignCreator);
	}
}
