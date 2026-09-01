using System;
using System.Collections.Generic;
using System.Linq;
using DM;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMapSelector : MonoBehaviour
{
	private interface IMapSelector
	{
		void InitDropdown(UIMapSelector selector);

		int GetCurrentLevelIndex();

		void SetMapButtonsClickable(bool enabled);
	}

	private class UIMapSelectorCampaign : IMapSelector
	{
		private int m_currentLevelIndex;

		private bool m_enabled = true;

		public void InitDropdown(UIMapSelector selector)
		{
			TABSCampaignLevelAsset currentLevel = CampaignPlayerDataHolder.GetCurrentLevel();
			selector.m_currentMapName.text = currentLevel.Entity.Name;
			selector.m_currentMapName.transform.parent.GetComponent<SimpleButton>().enabled = false;
			TABSCampaignLevelAsset[] levelsInCurrentCampaign = CampaignPlayerDataHolder.GetLevelsInCurrentCampaign();
			bool flag = false;
			int num = levelsInCurrentCampaign.Length;
			selector.ChangeName("Levels");
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				bool selected = false;
				TABSCampaignLevelAsset tABSCampaignLevelAsset = levelsInCurrentCampaign[i];
				if (!(tABSCampaignLevelAsset == null))
				{
					if (currentLevel.Entity.GUID == tABSCampaignLevelAsset.Entity.GUID)
					{
						selected = true;
					}
					bool flag2 = ServiceLocator.GetService<DebugService>() != null && ServiceLocator.GetService<DebugService>().HasUnlockedProgress;
					bool flag3 = ServiceLocator.GetService<ISaveLoaderService>().HasBeatenLevel(tABSCampaignLevelAsset.Entity.GUID, CampaignPlayerDataHolder.GetCurrentCampaignID);
					bool flag4 = num2 == 0;
					int index = i;
					bool flag5 = flag3 || flag4 || flag || flag2;
					bool newLevel = !flag2 && !flag3 && flag5;
					selector.AddMapButton(tABSCampaignLevelAsset.Entity.Name, delegate
					{
						OnCampaignLevelClicked(index);
					}, selected, flag5, newLevel);
					num2++;
					flag = flag3;
				}
			}
			selector.SetUpExplicitVerticalNavigation();
		}

		private void OnCampaignLevelClicked(int index)
		{
			if (m_enabled)
			{
				CampaignPlayerDataHolder.LoadCampaignLevelWithIndex(index);
			}
		}

		public int GetCurrentLevelIndex()
		{
			return m_currentLevelIndex;
		}

		public void SetMapButtonsClickable(bool enabled)
		{
			m_enabled = enabled;
		}
	}

	private class UIMapSelectorSandbox : IMapSelector
	{
		private int m_currentlyLoadedLevelIndex;

		private bool m_enabled = true;

		public void InitDropdown(UIMapSelector selector)
		{
			string name = SceneManager.GetActiveScene().name;
			MapAsset[] array = ContentDatabase.Instance().GetAllMapAssetsOrdered().ToArray();
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				bool selected = false;
				MapAsset mapAsset = array[i];
				if (mapAsset.MapName == name)
				{
					m_currentlyLoadedLevelIndex = i;
					selected = true;
				}
				int index = i;
				selector.AddMapButton(mapAsset.Entity.Name, delegate
				{
					OnMapChanged(index);
				}, selected, interactable: true);
			}
			selector.SetUpExplicitVerticalNavigation();
		}

		private void OnMapChanged(int newMap)
		{
			if (m_enabled)
			{
				TABSSceneManager.LoadMap(ContentDatabase.Instance().GetMapAssetByIndex(newMap));
			}
		}

		public int GetCurrentLevelIndex()
		{
			return m_currentlyLoadedLevelIndex;
		}

		public void SetMapButtonsClickable(bool enabled)
		{
			m_enabled = enabled;
		}
	}

	public MapsUIButton m_mapsUIButton;

	[SerializeField]
	private GameObject m_templateMap;

	[SerializeField]
	private TextMeshProUGUI m_NameText;

	[SerializeField]
	private TextMeshProUGUI m_currentMapName;

	[SerializeField]
	private PlacementUI m_placementUI;

	private List<GameObject> m_SpawnedMaps = new List<GameObject>();

	private List<Button> m_mapButtons;

	private UIMovementAnimation m_movementAnimation;

	private InputService m_inputService;

	private CanvasToggle[] canvasToggles;

	private bool m_canToggle = true;

	private IMapSelector m_mapSelector;

	public static bool IsOpen { get; private set; }

	public TextMeshProUGUI CurrentMapName => m_currentMapName;

	public event Action MapSelectorOpened;

	public event Action MapSelectorClosed;

	public static void SetOpen(bool open)
	{
		IsOpen = open;
	}

	private void Awake()
	{
		m_mapButtons = new List<Button>();
		m_templateMap.SetActive(value: false);
		m_inputService = ServiceLocator.GetService<InputService>();
		m_movementAnimation = GetComponent<UIMovementAnimation>();
		canvasToggles = GetComponentsInChildren<CanvasToggle>();
	}

	private void OnEnable()
	{
		m_movementAnimation = GetComponent<UIMovementAnimation>();
		if (m_movementAnimation != null)
		{
			m_movementAnimation.OnCompleteState01 += OnOpenMapSelector;
			m_movementAnimation.OnCompleteState02 += OnCloseMapSelector;
		}
	}

	private void Start()
	{
		m_templateMap.SetActive(value: false);
		Type type = ServiceLocator.GetService<GameModeService>().CurrentGameMode.GetType();
		bool flag = false;
		if (type == typeof(LocalMultiplayerGameMode))
		{
			flag = true;
		}
		if (type == typeof(OnlineMultiplayerGameMode))
		{
			flag = true;
		}
		if (type == typeof(CampaignGameMode))
		{
			m_mapSelector = new UIMapSelectorCampaign();
		}
		else if (type == typeof(SandboxGameMode) || flag)
		{
			m_mapSelector = new UIMapSelectorSandbox();
		}
		m_mapSelector.InitDropdown(this);
		InputService service = ServiceLocator.GetService<InputService>();
		if (service != null)
		{
			service.InputChanged += OnInputSourceChanged;
		}
		InstancedHandler<EscapeMenuHandler>.Instance.EscapeMenuClosed += OnEscapeMenuClosed;
	}

	private void OnEscapeMenuClosed()
	{
		if (IsOpen)
		{
			SelectFirstMapButton();
		}
	}

	public void ClearMaps()
	{
		for (int num = m_SpawnedMaps.Count - 1; num >= 0; num--)
		{
			UnityEngine.Object.Destroy(m_SpawnedMaps[num]);
		}
		m_mapButtons.Clear();
	}

	public void AddMapButton(string mapName, UnityAction buttonCallback, bool selected, bool interactable, bool newLevel = false)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(m_templateMap, m_templateMap.transform.parent);
		gameObject.FetchComponent<SelectedMapCellUI>().Init(buttonCallback, selected, interactable, newLevel);
		gameObject.SetActive(value: true);
		m_SpawnedMaps.Add(gameObject);
		gameObject.GetComponentInChildren<TextMeshProUGUI>().text = mapName;
		if (m_mapButtons == null)
		{
			m_mapButtons = new List<Button>();
		}
		Button component = gameObject.GetComponent<Button>();
		if (component != null)
		{
			component.onClick.AddListener(OpenMapSelector);
			m_mapButtons.Add(component);
		}
	}

	public void SetMapButtonsClickable(bool enabled)
	{
		if (m_mapSelector != null)
		{
			m_mapSelector.SetMapButtonsClickable(enabled);
		}
	}

	public void ChangeName(string newName)
	{
		m_NameText.text = newName;
	}

	public void SetUpExplicitVerticalNavigation()
	{
		UIHelpers.CreateAutomaticNavigation(m_mapButtons.ToArray());
	}

	public void OpenMapSelector()
	{
		if (m_movementAnimation.m_CompleteDone && !IsOpen)
		{
			UIScreenInputBlocker.AnimatedMenuTransitionStart();
			m_mapsUIButton.OnClick();
			SelectFirstMapButton();
			IsOpen = true;
			this.MapSelectorOpened?.Invoke();
			UIScreenInputBlocker.AnimatedMenuTransitionEnd();
		}
	}

	public void CloseMapSelector()
	{
		if (m_movementAnimation.m_CompleteDone && IsOpen)
		{
			UIScreenInputBlocker.AnimatedMenuTransitionStart();
			m_mapsUIButton.Close();
			if (!EscapeMenuHandler.InMenu)
			{
				EventSystem.current.SetSelectedGameObject(null);
			}
			IsOpen = false;
			this.MapSelectorClosed?.Invoke();
			UIScreenInputBlocker.AnimatedMenuTransitionEnd();
		}
	}

	private void OnCloseMapSelector()
	{
		m_inputService.OnUIClose();
	}

	private void OnOpenMapSelector()
	{
		m_inputService.OnUIOpen();
	}

	public void SetToggleCanvas(bool state)
	{
		if (canvasToggles != null)
		{
			CanvasToggle[] array = canvasToggles;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetCanvasToggle(state);
			}
		}
	}

	private void OnInputSourceChanged(InputType type)
	{
		if (IsOpen && !EscapeMenuHandler.InMenu)
		{
			switch (type)
			{
			case InputType.Controller:
				SelectFirstMapButton();
				break;
			case InputType.Keyboard:
			case InputType.Any:
				EventSystem.current.SetSelectedGameObject(null);
				break;
			default:
				throw new ArgumentOutOfRangeException("type", type, null);
			}
		}
	}

	private void SelectFirstMapButton()
	{
		if (m_mapButtons.Count > 0 && PlayerActions.Instance.InputType == InputType.Controller && !EscapeMenuHandler.InMenu)
		{
			m_mapButtons[0].Select();
		}
	}

	private void OnDisable()
	{
		if (m_movementAnimation != null)
		{
			m_movementAnimation.OnCompleteState01 -= OnOpenMapSelector;
			m_movementAnimation.OnCompleteState02 -= OnCloseMapSelector;
		}
		InputService service = ServiceLocator.GetService<InputService>();
		if (service != null)
		{
			service.InputChanged -= OnInputSourceChanged;
		}
	}

	public int GetCurrentLoadedLevelIndex()
	{
		return m_mapSelector.GetCurrentLevelIndex();
	}

	private void OnDestroy()
	{
		if (InstancedHandler<EscapeMenuHandler>.Instance != null)
		{
			InstancedHandler<EscapeMenuHandler>.Instance.EscapeMenuClosed -= OnEscapeMenuClosed;
		}
	}
}
