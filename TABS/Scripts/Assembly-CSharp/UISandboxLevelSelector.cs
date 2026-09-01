using System;
using System.Collections.Generic;
using System.Linq;
using DM;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;
using UnityEngine.UI;

public class UISandboxLevelSelector : Paginator
{
	[SerializeField]
	private PageCounter m_PageCounter;

	[SerializeField]
	private GameObject m_templateLevelButton;

	[SerializeField]
	private MainMenuUIHandler m_mainMenuUIHandler;

	[SerializeField]
	private Transform m_Grid;

	[SerializeField]
	private Toggle[] m_SandboxTabs;

	[SerializeField]
	private UIMapSelector uiMapSelector;

	private int m_SandboxTabIndex;

	private List<Button> m_buttons;

	private bool inGameScene;

	private List<GameObject> SpawnedButtons = new List<GameObject>();

	private MapAsset.MapType mapType;

	private INetworkService m_networkService;

	protected override void Awake()
	{
		base.Awake();
		if (m_mainMenuUIHandler == null)
		{
			inGameScene = true;
		}
		if (uiMapSelector != null)
		{
			uiMapSelector.MapSelectorOpened += EnableNavigation;
			uiMapSelector.MapSelectorClosed += DisableNavigation;
		}
	}

	protected override void Start()
	{
		base.Start();
		InputService service = ServiceLocator.GetService<InputService>();
		if (service != null)
		{
			service.InputChanged += OnInputSourceChanged;
		}
		m_networkService = ServiceLocator.GetService<INetworkService>();
	}

	protected override void Populate(int page = 0)
	{
		base.Populate(page);
		Clear();
		m_buttons = new List<Button>();
		MapAsset[] array = ContentDatabase.Instance().GetMapAssetsByType(mapType, onlyUnlocked: true).ToArray();
		totalPages = Mathf.CeilToInt((float)array.Length / 12f);
		int num = array.Length - 12 * page;
		if (m_PageCounter != null)
		{
			m_PageCounter.Set(page + 1, totalPages);
		}
		for (int i = 0; i < Mathf.Min(12, num); i++)
		{
			int currentButtonIndex = array.Length - (num - i);
			MapAsset map = array[currentButtonIndex];
			GameObject buttonInstance = UnityEngine.Object.Instantiate(m_templateLevelButton, m_Grid);
			buttonInstance.SetActive(value: true);
			MapAsset currentMap = array[currentButtonIndex];
			currentMap.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
			{
				if (buttonInstance != null)
				{
					buttonInstance.GetComponent<MapGrid>().Setup(sprite, currentMap.Entity.Name, localized: true);
				}
			});
			Button component = buttonInstance.GetComponent<Button>();
			component.onClick.AddListener(delegate
			{
				bool flag = true;
				GameModeService service = ServiceLocator.GetService<GameModeService>();
				if (CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.Sandbox)
				{
					service.SetGameMode<SandboxGameMode>();
				}
				else if (CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.LocalMultiplayer)
				{
					service.SetGameMode<LocalMultiplayerGameMode>();
				}
				else if (CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.OnlineMultiplayer)
				{
					flag = false;
					StartOnlineGame(mapType, currentButtonIndex);
				}
				if (uiMapSelector != null)
				{
					uiMapSelector.CloseMapSelector();
				}
				if (flag)
				{
					TABSSceneManager.LoadMap(map);
				}
			});
			if (inGameScene && TABSSceneManager.CurrentLoadedMap == map)
			{
				buttonInstance.GetComponent<MapGrid>().Selected();
			}
			m_buttons.Add(component);
			SpawnedButtons.Add(buttonInstance);
		}
		SelectFirstButtonInList();
	}

	private void StartOnlineGame(MapAsset.MapType useMapType, int useMapIndex)
	{
		throw new NotImplementedException("Set canPlayCrossNetwork.");
	}

	private void OnCreateSession(NetworkSession session, NetworkException exception)
	{
		if (session == null || exception != null)
		{
			Debug.LogError("Failed to create a session.");
		}
		else
		{
			TABSSceneManager.LoadMap(ContentDatabase.Instance().GetMapAssetByTypeAndMapIndex(session.Metadata.RoomMapType, session.Metadata.RoomMapIndex));
		}
	}

	protected override void Clear()
	{
		base.Clear();
		for (int i = 0; i < SpawnedButtons.Count; i++)
		{
			UnityEngine.Object.Destroy(SpawnedButtons[i]);
		}
		SpawnedButtons.Clear();
	}

	protected override void Update()
	{
		base.Update();
		if (base.IsOpen)
		{
			if (PlayerActions.Instance.m_cycleUnitsUp.WasPressed)
			{
				NextPage();
			}
			if (PlayerActions.Instance.m_cycleUnitsDown.WasPressed)
			{
				PreviousPage();
			}
			if (PlayerActions.Instance.m_cycleTabsUp.WasPressed)
			{
				ChangeMapType((int)(mapType + 1));
			}
			if (PlayerActions.Instance.m_cycleTabsDown.WasPressed)
			{
				ChangeMapType((int)(mapType - 1));
			}
			if (PlayerActions.Instance.m_cycleTabs.WasPressed && m_SandboxTabs != null && m_SandboxTabs.Length != 0)
			{
				int num = m_SandboxTabs.Length;
				int num2 = Mathf.RoundToInt(PlayerActions.Instance.m_cycleTabs.Value);
				m_SandboxTabIndex = (m_SandboxTabIndex + num2) % num;
				m_SandboxTabIndex = ((m_SandboxTabIndex < 0) ? (num - 1) : m_SandboxTabIndex);
				m_SandboxTabs[m_SandboxTabIndex].isOn = true;
			}
		}
	}

	public void ChangeMapType(int map)
	{
		int length = Enum.GetValues(typeof(MapAsset.MapType)).Length;
		if (map < 0)
		{
			map = length - 1;
		}
		else if (map >= length)
		{
			map = 0;
		}
		MapAsset.MapType mapType = (MapAsset.MapType)map;
		if (this.mapType != mapType)
		{
			this.mapType = mapType;
			Populate();
		}
	}

	private void OnInputSourceChanged(InputType type)
	{
		if (!(m_mainMenuUIHandler != null) || m_mainMenuUIHandler.currentMenuState == MenuState.LevelSelection)
		{
			switch (type)
			{
			case InputType.Controller:
				SelectFirstButtonInList();
				break;
			default:
				throw new ArgumentOutOfRangeException("type", type, null);
			case InputType.Keyboard:
			case InputType.Any:
				break;
			}
		}
	}

	public override void OpenPage()
	{
		base.OpenPage();
		EnableNavigation();
	}

	public override void ClosePage()
	{
		base.ClosePage();
		DisableNavigation();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		InputService service = ServiceLocator.GetService<InputService>();
		if (service != null)
		{
			service.InputChanged -= OnInputSourceChanged;
		}
		if (uiMapSelector != null)
		{
			uiMapSelector.MapSelectorOpened -= EnableNavigation;
			uiMapSelector.MapSelectorClosed -= DisableNavigation;
		}
	}

	private void EnableNaviagtionOnUnpause()
	{
		if (base.IsOpen)
		{
			EnableNavigation();
		}
	}

	public void SelectFirstButtonInList()
	{
		m_buttons[0].Select();
	}
}
