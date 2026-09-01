using System;
using System.Collections.Generic;
using DM;
using GamepadUI.StateManager.Core;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.Services;
using Landfall.TABS.Workshop;
using LevelCreator;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomContetnManager : UIComponentMainMenu
{
	[Serializable]
	public class Page
	{
		public string Name;

		public GameObject PageObject;
	}

	public FactionCreatorManager factionCreator;

	[SerializeField]
	protected CustomContentPageLoadingRefreshIcon loadingIcon;

	[SerializeField]
	protected GameObject noContentElement;

	public Page[] Pages;

	private Page currentPage;

	public static TABSCampaignLevelAsset previousBattle;

	public static CustomMap previousCustomMap;

	public static MapAsset previousMap;

	public static bool returnToMapCreator;

	private new void Start()
	{
		NavigateToPage(Pages[0]);
	}

	private void SceneChanged(Scene arg0)
	{
		previousBattle = null;
		previousCustomMap = null;
		previousMap = null;
		returnToMapCreator = false;
		SceneManager.sceneUnloaded -= SceneChanged;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		ITimeService service = ServiceLocator.GetService<ITimeService>();
		service?.SetState(1f, 0f);
		service?.Lock();
		SceneManager.sceneUnloaded += SceneChanged;
		ServiceLocator.GetService<MusicHandler>().PlayMenuMusic();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		ServiceLocator.GetService<ITimeService>()?.Unlock();
	}

	public void UpdateLoadingScreenState(CustomContentPageLoadingRefreshIcon.LoadingIconState newState)
	{
		if (loadingIcon != null)
		{
			loadingIcon.UpdateLoadingScreenState(newState);
		}
	}

	public void OpenGameObjectSubMenu(GameObject menu)
	{
		if (!(menu == null))
		{
			UISubMenu component = menu.GetComponent<UISubMenu>();
			if (component != null)
			{
				OpenSubMenu(component);
			}
		}
	}

	public void NavigateToNewFaction()
	{
		factionCreator.Init();
		NavigateToPage("FACTIONCREATOR");
	}

	public void NavigateToNewFaction(bool init = true, Faction loadedFaction = null)
	{
		NavigateToPage("FACTIONCREATOR");
		if (init)
		{
			factionCreator.Init();
		}
		if ((bool)loadedFaction)
		{
			factionCreator.LoadFaction(loadedFaction);
		}
	}

	public void NavigateToPage(string pageName)
	{
	}

	public void NavigateToPage(Page page)
	{
	}

	private Page GetPageFromName(string pageName)
	{
		for (int i = 0; i < Pages.Length; i++)
		{
			if (Pages[i].Name == pageName)
			{
				return Pages[i];
			}
		}
		return null;
	}

	public void GoToUnitCreator()
	{
		TABSSceneManager.LoadUnitCreator();
	}

	public void GoToMainMenu()
	{
		if (returnToMapCreator)
		{
			CreateNewLevel();
		}
		else if (previousBattle != null || previousMap != null || previousCustomMap != null)
		{
			GoToPreviousBattle();
		}
		else
		{
			TABSSceneManager.LoadMainMenu();
		}
	}

	private void GoToPreviousBattle()
	{
		SpawnLevel.SetCustomMapToLoad(previousCustomMap);
		if (previousBattle != null)
		{
			IEnumerable<TABSCampaignLevelAsset> userCampaignLevelsByFilter = ContentDatabase.Instance().GetUserCampaignLevelsByFilter(new Filter
			{
				ExactNameMatch = true,
				NamePart = previousBattle.Entity.Name,
				WorkshopTypeFilter = WorkshopTypeFilter.All
			});
			bool flag = false;
			foreach (TABSCampaignLevelAsset item in userCampaignLevelsByFilter)
			{
				if (item.Entity.GUID == previousBattle.Entity.GUID)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				previousBattle = null;
			}
			CampaignPlayerDataHolder.StartedPlayingBattle(previousBattle);
			TABSSceneManager.LoadCampaign();
		}
		else if (previousMap != null)
		{
			ServiceLocator.GetService<GameModeService>().SetGameMode<SandboxGameMode>();
			CampaignPlayerDataHolder.StartedPlayingSandbox();
			TABSSceneManager.LoadMap(previousMap);
		}
		else if (previousBattle == null && previousMap == null)
		{
			TABSSceneManager.LoadMainMenu();
		}
	}

	public void GoToNewBattle()
	{
		ServiceLocator.GetService<GameModeService>().SetGameMode<SandboxGameMode>();
		CampaignPlayerDataHolder.StartedPlayingSandbox();
		ContentDatabase contentDatabase = ContentDatabase.Instance();
		int mapAssetCount = contentDatabase.GetMapAssetCount();
		MapAsset mapAssetByIndex = contentDatabase.GetMapAssetByIndex(UnityEngine.Random.Range(0, mapAssetCount));
		if (mapAssetByIndex != null)
		{
			TABSSceneManager.LoadMap(mapAssetByIndex);
		}
	}

	public void CreateNewLevel()
	{
		StartMenu.SetBackButtonState(StartMenu.StartMenuBackState.ToMainMenu);
		TABSSceneManager.LoadLevelCreator(DMEditor.StartState.New);
	}
}
