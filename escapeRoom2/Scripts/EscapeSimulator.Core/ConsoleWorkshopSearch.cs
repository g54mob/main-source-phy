using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ConsoleWorkshopSearch : MonoBehaviour
{
	[Serializable]
	public class RoomSearchInfo
	{
		public string ResultCount;

		public string ResultsPerPage;

		public string PageCount;

		public string Page;

		public List<Menu.PineRoomData> Rooms = new List<Menu.PineRoomData>();

		public string Debug;
	}

	public enum SortDropdownValues
	{
		recent = 0,
		popular = 1
	}

	public enum TimeDropdownValues
	{
		month = 0,
		year = 1,
		alltime = 2
	}

	private class RoomImageRequest
	{
		public string roomID;

		public UnityWebRequest request;

		public DownloadHandlerTexture texHandler;
	}

	[HideInInspector]
	public ScrollRect scrollView;

	[NonSerialized]
	public CustomRoomsLocalRepository db;

	private ConsoleWorkshopSearchCanvasUI ui;

	private PineTweenSystemEnableNoHandles tweener = new PineTweenSystemEnableNoHandles();

	private Action onClose;

	private Action<Selectable> onChangeSelection;

	private Action<Selectable, Menu.WorkshopSearchRoomInfo> onLevelSelected;

	private Action<Menu.WorkshopRoomInfo> onLevelInstalled;

	private Action onDownloadError;

	private Dictionary<string, Menu.WorkshopSearchRoomInfo> workshopSearchRooms = new Dictionary<string, Menu.WorkshopSearchRoomInfo>();

	private UnityWebRequest searchRoomsRequest;

	private RoomSearchInfo roomSearchInfos;

	private List<RoomImageRequest> roomImagesRequests = new List<RoomImageRequest>();

	private bool nsaIdError;

	private MenuLevelUI[] searchLevelsUI;

	private List<Toggle> allLevelToggles = new List<Toggle>();

	private bool useFullFileSize;

	private int currentPage;

	private string previousSearchText;

	private SortDropdownValues previousSortValue;

	private TimeDropdownValues previousTimeValue;

	private Menu menu;

	private List<Selectable> paginationDots;

	private UnityWebRequest clientWebRequestRoomInfo;

	private int maxPaginationDots = 6;

	private int timeLimit = 2600000;

	private int maxTextureCacheCount = 3;

	public bool isFromCoop { get; private set; }

	public Transform levelHolder => ui.LevelsScrollView_Content.transform;

	private void defineDropdown(Dropdown dropdown, string[] choices, int initial, Action<int> onChange, int direction = 1, Func<bool> validation = null)
	{
		dropdown.gameObject.SetActive(value: true);
		dropdown.gameObject.GetComponentInChildren<Text>().text = choices[initial];
		dropdown.options = new List<Dropdown.OptionData>();
		foreach (string text in choices)
		{
			dropdown.options.Add(new Dropdown.OptionData
			{
				text = "%" + text + "%"
			});
		}
		dropdown.value = initial;
		dropdown.onValueChanged.RemoveAllListeners();
		dropdown.onValueChanged.AddListener(delegate
		{
			onChange(dropdown.value);
			PineFmod.playOneShotSound("event:/SFX/GENERAL/uiToggle");
		});
		Localization.translateObject(dropdown.transform);
	}

	private void onButtonClick(Button button)
	{
	}

	private void onToggleChange(Toggle toggle, bool value)
	{
		Debug.Log("onToggleChange " + toggle.name + " " + value);
	}

	private void refreshSearchlevels()
	{
		if (ui.LevelsScrollView_Content.transform.childCount == 1)
		{
			searchRoomsRequest = Menu.sendWorkshopWebRequest($"/query/verified/basic/popular/desc/alltime/{currentPage}");
			return;
		}
		MenuLevelUI componentInChildren = ui.LevelsScrollView_Content.GetComponentInChildren<MenuLevelUI>();
		foreach (KeyValuePair<string, Menu.WorkshopSearchRoomInfo> workshopSearchRoom in workshopSearchRooms)
		{
			if (workshopSearchRoom.Value.workshopScreenMenuLevelUI == componentInChildren)
			{
				setWorkshopSearchSelectedLevel(workshopSearchRoom.Value);
			}
			if (workshopSearchRoom.Value.workshopScreenMenuLevelUI != null)
			{
				workshopSearchRoom.Value.workshopScreenMenuLevelUI.customLevelDownloadedIcon.SetActive(workshopSearchRoom.Value.levelState != Menu.WorkshopSearchLevelState.Installed && workshopSearchRoom.Value.levelState != Menu.WorkshopSearchLevelState.Installing);
			}
		}
	}

	private Selectable setWorkshopSearchSelectedLevel(Menu.WorkshopSearchRoomInfo roomInfo)
	{
		Selectable result = null;
		if (roomInfo != null && roomInfo.workshopScreenMenuLevelUI != null)
		{
			Toggle componentInChildren = roomInfo.workshopScreenMenuLevelUI.GetComponentInChildren<Toggle>(includeInactive: true);
			componentInChildren.isOn = false;
			componentInChildren.isOn = true;
			result = roomInfo.workshopScreenMenuLevelUI.GetComponent<Selectable>();
			onChangeSelection?.Invoke(roomInfo.workshopScreenMenuLevelUI.GetComponent<Selectable>());
		}
		return result;
	}

	private void onDropdownSelection(Dropdown dropdown, int newValue)
	{
		if (dropdown == ui.LevelSelectorHeader_SortDropdown || dropdown == ui.LevelSelectorHeader_TimeDropdown)
		{
			sendQuery();
		}
	}

	private MenuLevelUI constructWorkshopSearchLevel(Menu.PineRoomData info, bool clickable, Action onClick, Transform parent = null, Action onDoubleClick = null)
	{
		if (workshopSearchRooms.ContainsKey(info.SteamID) && workshopSearchRooms[info.SteamID].workshopScreenMenuLevelUI != null)
		{
			return workshopSearchRooms[info.SteamID].workshopScreenMenuLevelUI;
		}
		if (parent == null)
		{
			parent = ui.LevelsScrollView_Content.transform;
		}
		MenuLevelUI menuLevelUI = null;
		menuLevelUI = createMenuUI(info);
		Menu.WorkshopSearchRoomInfo searchInfo;
		if (workshopSearchRooms.ContainsKey(info.SteamID))
		{
			searchInfo = workshopSearchRooms[info.SteamID];
		}
		else
		{
			searchInfo = new Menu.WorkshopSearchRoomInfo();
			searchInfo.levelState = Menu.WorkshopSearchLevelState.NotInstalled;
		}
		searchInfo.workshopScreenMenuLevelUI = menuLevelUI;
		searchInfo.downloadedInfo = info;
		if (db.isRoomInstalled(info.id))
		{
			menuLevelUI.customLevelDownloadedIcon.SetActive(value: false);
			searchInfo.levelState = Menu.WorkshopSearchLevelState.Installed;
		}
		workshopSearchRooms[info.SteamID] = searchInfo;
		if (ulong.TryParse(info.SteamID, out var result))
		{
			switch (PlayerSave.getFinishState(result.ToString()))
			{
			case PlayerSave.FinishState.Finished:
				menuLevelUI.customLevelFinishIcon.SetActive(value: true);
				break;
			case PlayerSave.FinishState.None:
			{
				bool flag = false;
				if (DateTime.TryParse(info.Modified, out var result2))
				{
					DateTime dateTime = result2.ToUniversalTime();
					flag = (DateTime.Now - dateTime).TotalSeconds < (double)timeLimit;
				}
				else
				{
					Debug.Log("Created at date parse fail - " + info.Name);
				}
				menuLevelUI.customLevelDotIcon.SetActive(!flag);
				menuLevelUI.customLevelNewIcon?.SetActive(flag);
				break;
			}
			}
		}
		Toggle toggle = menuLevelUI.GetComponentInChildren<Toggle>(includeInactive: true);
		toggle.group = parent.GetComponent<ToggleGroup>();
		allLevelToggles.Add(toggle);
		toggle.interactable = !Controller.isActive();
		Navigation navigation = toggle.navigation;
		navigation.mode = Navigation.Mode.Explicit;
		toggle.navigation = navigation;
		if (clickable)
		{
			toggle.onValueChanged.AddListener(delegate
			{
				if (toggle.isOn)
				{
					onLevelSelected?.Invoke(toggle, searchInfo);
				}
			});
			EventButton component = menuLevelUI.GetComponent<EventButton>();
			component.onDoubleClick.AddListener(delegate
			{
			});
			if (onDoubleClick != null)
			{
				component.onDoubleClick.AddListener(delegate
				{
					onDoubleClick();
				});
			}
		}
		else
		{
			toggle.graphic.gameObject.SetActive(value: false);
			UnityEngine.Object.Destroy(toggle);
		}
		menuLevelUI.gameObject.SetActive(value: true);
		return menuLevelUI;
		MenuLevelUI createMenuUI(Menu.PineRoomData pineRoomData)
		{
			Toggle toggle2 = UnityEngine.Object.Instantiate(ui.LevelsScrollView_Content_Level, parent);
			toggle2.gameObject.SetActive(value: true);
			MenuLevelUI component2 = toggle2.GetComponent<MenuLevelUI>();
			component2.title.text = pineRoomData.Name;
			component2.title.gameObject.AddComponent<DontTranslate>();
			component2.tokens.gameObject.SetActive(value: false);
			component2.tokensIcon.gameObject.SetActive(value: false);
			component2.customLevelDownloadedIcon.SetActive(value: true);
			component2.isCustom = true;
			return component2;
		}
	}

	private void sendControllerWorkshopQuery(string nameQuery, SortDropdownValues sort, TimeDropdownValues time, int page)
	{
		string text = "";
		text = ((!string.IsNullOrEmpty(nameQuery)) ? $"name/{nameQuery}/{sort.ToString()}/desc/{time.ToString()}/{page}" : $"basic/{sort.ToString()}/desc/{time.ToString()}/{page}");
		searchRoomsRequest = Menu.sendWorkshopWebRequest("/query/verified/" + text);
		cleanUpLevelUIs();
		destroyDownloadedInformation();
	}

	private void updateLevelTextureCache()
	{
		foreach (KeyValuePair<string, Menu.WorkshopSearchRoomInfo> workshopSearchRoom in workshopSearchRooms)
		{
			if (workshopSearchRoom.Value.levelState != Menu.WorkshopSearchLevelState.Installed && !(workshopSearchRoom.Value.workshopScreenMenuLevelUI != null))
			{
				workshopSearchRoom.Value.cachedForCount--;
				if (workshopSearchRoom.Value.cachedForCount <= 0 && workshopSearchRoom.Value.cachedLevelTexture != null)
				{
					UnityEngine.Object.Destroy(workshopSearchRoom.Value.cachedLevelTexture);
				}
			}
		}
	}

	private void connectLevelTiles(List<MenuLevelUI> tiles, int columnCount = 2, bool shouldResetNavigation = false)
	{
		if (shouldResetNavigation)
		{
			foreach (MenuLevelUI tile in tiles)
			{
				Selectable component = tile.GetComponent<Selectable>();
				Navigation navigation = component.navigation;
				navigation.selectOnLeft = null;
				navigation.selectOnRight = null;
				navigation.selectOnUp = null;
				navigation.selectOnDown = null;
				component.navigation = navigation;
			}
		}
		for (int i = 0; i < tiles.Count; i++)
		{
			MenuLevelUI menuLevelUI = null;
			MenuLevelUI menuLevelUI2 = null;
			if (i > 0)
			{
				menuLevelUI = tiles[i - 1];
			}
			if (i >= columnCount)
			{
				menuLevelUI2 = tiles[i - columnCount];
			}
			if (menuLevelUI != null)
			{
				connectSelectablesHorizontal(menuLevelUI.gameObject, tiles[i].gameObject);
			}
			if (menuLevelUI2 != null)
			{
				connectSelectablesVertical(menuLevelUI2.gameObject, tiles[i].gameObject);
			}
		}
	}

	private void connectSelectablesVertical(Selectable top, Selectable bot)
	{
		Navigation navigation = top.navigation;
		navigation.selectOnDown = bot;
		top.navigation = navigation;
		Navigation navigation2 = bot.navigation;
		navigation2.selectOnUp = top;
		bot.navigation = navigation2;
	}

	private void connectSelectablesVertical(GameObject topGO, GameObject botGO)
	{
		Selectable component = topGO.GetComponent<Selectable>();
		Selectable component2 = botGO.GetComponent<Selectable>();
		connectSelectablesVertical(component, component2);
	}

	private void connectSelectablesHorizontal(Selectable left, Selectable right)
	{
		Navigation navigation = left.navigation;
		navigation.selectOnRight = right;
		left.navigation = navigation;
		Navigation navigation2 = right.navigation;
		navigation2.selectOnLeft = left;
		right.navigation = navigation2;
	}

	private void connectSelectablesHorizontal(GameObject leftGO, GameObject rightGO)
	{
		Selectable component = leftGO.GetComponent<Selectable>();
		Selectable component2 = rightGO.GetComponent<Selectable>();
		connectSelectablesHorizontal(component, component2);
	}

	private void activateErrorPopup(MenuErrorPopupUI popup, string title, string description, string buttonLabel, Action onButton)
	{
		popup.root.onClick.RemoveAllListeners();
		popup.root.onClick.AddListener(delegate
		{
			onButton();
		});
		popup.errorTitle.text = Localization.translate(title);
		popup.errorLabel.text = Localization.translate(description);
		popup.Button_Title.text = Localization.translate(buttonLabel);
		popup.root.gameObject.SetActive(value: true);
	}

	private void deactivateErrorPopup(MenuErrorPopupUI popup)
	{
		popup.root.gameObject.SetActive(value: false);
	}

	private void changePage(int amount)
	{
		int num = currentPage + amount;
		int num2 = ((roomSearchInfos != null) ? (int.Parse(roomSearchInfos.PageCount) - 1) : 0);
		if (num >= 0 && num <= num2)
		{
			currentPage = Mathf.Clamp(num, 0, num2);
			sendQuery();
			updateFooter();
		}
	}

	private void updateFooter()
	{
		if (roomSearchInfos == null)
		{
			ui.PrevHint.transform.parent.gameObject.SetActive(value: false);
			return;
		}
		ui.PrevHint.transform.parent.gameObject.SetActive(value: true);
		int num = int.Parse(roomSearchInfos.PageCount);
		ui.PrevHint.interactable = currentPage > 0;
		ui.NextHint.interactable = currentPage < num - 1;
		ui.PrevHint.transform.parent.gameObject.SetActive(num > 1);
		if (num <= 1)
		{
			return;
		}
		int num2 = Mathf.Min(num, maxPaginationDots);
		for (int i = 0; i < paginationDots.Count; i++)
		{
			paginationDots[i].gameObject.SetActive(i < num2);
			if (num <= maxPaginationDots)
			{
				paginationDots[i].interactable = i == currentPage;
				ui.smallDotLeft.gameObject.SetActive(value: false);
				ui.smallDotRight.gameObject.SetActive(value: false);
				continue;
			}
			ui.smallDotLeft.gameObject.SetActive(value: true);
			ui.smallDotRight.gameObject.SetActive(value: true);
			ui.smallDotLeft.enabled = currentPage != 0;
			ui.smallDotRight.enabled = currentPage != num - 1;
			int value = Mathf.FloorToInt((currentPage - 1) * num2 / (num - 2));
			value = Mathf.Clamp(value, 0, num2 - 1);
			paginationDots[i].interactable = i == value;
		}
	}

	private void cleanUpLevelUIs()
	{
		foreach (KeyValuePair<string, Menu.WorkshopSearchRoomInfo> workshopSearchRoom in workshopSearchRooms)
		{
			if (workshopSearchRoom.Value.workshopScreenMenuLevelUI != null)
			{
				UnityEngine.Object.Destroy(workshopSearchRoom.Value.workshopScreenMenuLevelUI.gameObject);
				workshopSearchRoom.Value.workshopScreenMenuLevelUI = null;
			}
			if (workshopSearchRoom.Value.levelState == Menu.WorkshopSearchLevelState.NotInstalled)
			{
				if (workshopSearchRoom.Value.communityRoomsTabMenuLevelUI != null)
				{
					UnityEngine.Object.Destroy(workshopSearchRoom.Value.communityRoomsTabMenuLevelUI.levelImage.texture);
					UnityEngine.Object.Destroy(workshopSearchRoom.Value.communityRoomsTabMenuLevelUI.levelImageOverlay.texture);
					UnityEngine.Object.Destroy(workshopSearchRoom.Value.communityRoomsTabMenuLevelUI.gameObject);
					workshopSearchRoom.Value.communityRoomsTabMenuLevelUI = null;
				}
				if (workshopSearchRoom.Value.communityCoopTabMenuLevelUI != null)
				{
					UnityEngine.Object.Destroy(workshopSearchRoom.Value.communityRoomsTabMenuLevelUI.levelImage.texture);
					UnityEngine.Object.Destroy(workshopSearchRoom.Value.communityRoomsTabMenuLevelUI.levelImageOverlay.texture);
					UnityEngine.Object.Destroy(workshopSearchRoom.Value.communityCoopTabMenuLevelUI.gameObject);
					workshopSearchRoom.Value.communityCoopTabMenuLevelUI = null;
				}
			}
		}
	}

	private void destroyDownloadedInformation(bool destroyAll = false)
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, Menu.WorkshopSearchRoomInfo> workshopSearchRoom in workshopSearchRooms)
		{
			Menu.WorkshopSearchRoomInfo value = workshopSearchRoom.Value;
			if (value.webRequest != null)
			{
				cancelDownload(value);
			}
			if (value.workshopScreenMenuLevelUI != null)
			{
				UnityEngine.Object.Destroy(value.workshopScreenMenuLevelUI.levelImage.texture);
			}
			if (destroyAll || value.levelState == Menu.WorkshopSearchLevelState.NotInstalled)
			{
				list.Add(workshopSearchRoom.Key);
				if (value.cachedLevelTexture != null)
				{
					UnityEngine.Object.Destroy(value.cachedLevelTexture);
				}
				if (value.workshopScreenMenuLevelUI != null)
				{
					UnityEngine.Object.Destroy(value.workshopScreenMenuLevelUI.levelImage.texture);
				}
				if (value.workshopScreenMenuLevelUI != null)
				{
					UnityEngine.Object.Destroy(value.workshopScreenMenuLevelUI.levelImage.texture);
				}
			}
		}
		foreach (string item in list)
		{
			RoomImageRequest roomImageRequest = null;
			foreach (RoomImageRequest roomImagesRequest in roomImagesRequests)
			{
				if (roomImagesRequest.roomID == item)
				{
					roomImageRequest = roomImagesRequest;
				}
			}
			if (roomImageRequest != null)
			{
				if (roomImageRequest.texHandler != null)
				{
					roomImageRequest.texHandler.Dispose();
				}
				if (roomImageRequest.request != null)
				{
					roomImageRequest.request.Dispose();
				}
				roomImagesRequests.Remove(roomImageRequest);
			}
			workshopSearchRooms.Remove(item);
		}
	}

	private void cancelDownload(Menu.WorkshopSearchRoomInfo room)
	{
		if (room.webRequest != null)
		{
			room.webRequest.Abort();
			room.webRequest.downloadHandler.Dispose();
			room.webRequest.Dispose();
			room.webRequest = null;
			room.levelState = Menu.WorkshopSearchLevelState.NotInstalled;
			if (room.workshopScreenMenuLevelUI != null)
			{
				room.workshopScreenMenuLevelUI.installing.SetActive(value: false);
			}
		}
	}

	private void OnDestroy()
	{
		destroyDownloadedInformation(destroyAll: true);
	}

	public void init(Menu menu, bool useFullFileSize, Action<Selectable> onChangeSelection, Action<Selectable, Menu.WorkshopSearchRoomInfo> onLevelSelected, Action<Menu.WorkshopRoomInfo> onLevelInstalled, Action onDownloadError)
	{
		this.menu = menu;
		ui = GetComponent<ConsoleWorkshopSearchCanvasUI>();
		scrollView = ui.LevelsScrollView.GetComponent<ScrollRect>();
		workshopSearchRooms = new Dictionary<string, Menu.WorkshopSearchRoomInfo>();
		paginationDots = new List<Selectable>();
		this.onChangeSelection = onChangeSelection;
		this.onLevelSelected = onLevelSelected;
		this.onLevelInstalled = onLevelInstalled;
		this.onDownloadError = onDownloadError;
		this.useFullFileSize = useFullFileSize;
		previousSearchText = "";
		previousSortValue = SortDropdownValues.popular;
		previousTimeValue = TimeDropdownValues.alltime;
		db = CustomRoomsLocalRepository.get();
		Button prevHint = ui.PrevHint;
		bool interactable = (ui.NextHint.interactable = false);
		prevHint.interactable = interactable;
		for (int i = 0; i < maxPaginationDots; i++)
		{
			Image image = UnityEngine.Object.Instantiate(ui.dot, ui.dot.transform.parent);
			paginationDots.Add(image.GetComponent<Selectable>());
			ui.smallDotRight.transform.SetAsLastSibling();
			ui.NextHint.transform.SetAsLastSibling();
		}
		defineDropdown(ui.LevelSelectorHeader_SortDropdown, Enum.GetNames(typeof(SortDropdownValues)), (int)previousSortValue, delegate(int newValue)
		{
			onDropdownSelection(ui.LevelSelectorHeader_SortDropdown, newValue);
		});
		defineDropdown(ui.LevelSelectorHeader_TimeDropdown, Enum.GetNames(typeof(TimeDropdownValues)), (int)previousTimeValue, delegate(int newValue)
		{
			onDropdownSelection(ui.LevelSelectorHeader_TimeDropdown, newValue);
		});
		PineUI.addButtonClickDelegate(onButtonClick);
		PineUI.addToggleDelegate(onToggleChange);
		PineUI.addDropdownDelegate(onDropdownSelection);
		string[] installedRoomsFilenames = db.getInstalledRoomsFilenames();
		foreach (string roomFilename in installedRoomsFilenames)
		{
			Menu.WorkshopRoomInfo workshopRoomInfo = db.readRoomInfo(roomFilename);
			if (workshopRoomInfo != null)
			{
				onLevelInstalled?.Invoke(workshopRoomInfo);
			}
		}
		updateFooter();
	}

	public void activate(bool isFromCoop, bool nsaIdFailure, Action onClose = null)
	{
		PineFmod.playOneShotSound("event:/SFX/GENERAL/uiToggle");
		PineTweenSystemEnableNoHandles pineTweenSystemEnableNoHandles = tweener;
		GameObject obj = base.gameObject;
		float? alphaTo = 1f;
		pineTweenSystemEnableNoHandles.tween(obj, 0.1f, 0f, deactivate: false, activate: true, destroy: false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, alphaTo);
		this.onClose = onClose;
		this.isFromCoop = isFromCoop;
		if (nsaIdFailure)
		{
			nsaIdError = true;
		}
		else
		{
			refreshSearchlevels();
		}
	}

	public void deactivate()
	{
		if (isActive())
		{
			onChangeSelection?.Invoke(null);
			setWorkshopSearchSelectedLevel(null);
			onClose?.Invoke();
			PineTweenSystemEnableNoHandles pineTweenSystemEnableNoHandles = tweener;
			GameObject obj = base.gameObject;
			float? alphaTo = 0f;
			pineTweenSystemEnableNoHandles.tween(obj, 0.1f, 0f, deactivate: true, activate: false, destroy: false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, alphaTo);
		}
		onClose = null;
		cleanUpLevelUIs();
		destroyDownloadedInformation();
	}

	public bool isActive()
	{
		if (ui != null)
		{
			return ui.root.gameObject.activeInHierarchy;
		}
		return false;
	}

	public void sendQuery()
	{
		bool flag = false;
		string text = ui.LevelSelectorHeader_Search_SearchInputField.text;
		if (!text.Equals(previousSearchText))
		{
			flag = true;
		}
		previousSearchText = text;
		SortDropdownValues value = (SortDropdownValues)ui.LevelSelectorHeader_SortDropdown.value;
		if (previousSortValue != value)
		{
			flag = true;
		}
		previousSortValue = value;
		TimeDropdownValues value2 = (TimeDropdownValues)ui.LevelSelectorHeader_TimeDropdown.value;
		if (previousTimeValue != value2)
		{
			flag = true;
		}
		previousTimeValue = value2;
		if (flag)
		{
			currentPage = 0;
		}
		sendControllerWorkshopQuery(text, value, value2, currentPage);
	}

	private MenuLevelUI constructWorkshopSearchLevelAndSetLevelImage(Menu.PineRoomData info)
	{
		MenuLevelUI result = constructWorkshopSearchLevel(info, clickable: true, null, ui.LevelsScrollView_Content.transform);
		Menu.WorkshopSearchRoomInfo workshopSearchRoomInfo = workshopSearchRooms[info.SteamID];
		if (workshopSearchRoomInfo.cachedLevelTexture == null)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(info.ImageURL);
			DownloadHandlerTexture texHandler = (DownloadHandlerTexture)(unityWebRequest.downloadHandler = new DownloadHandlerTexture());
			unityWebRequest.SendWebRequest();
			RoomImageRequest item = new RoomImageRequest
			{
				roomID = info.SteamID,
				request = unityWebRequest,
				texHandler = texHandler
			};
			roomImagesRequests.Add(item);
			return result;
		}
		setupLevelImage(info.SteamID, workshopSearchRoomInfo.cachedLevelTexture);
		return result;
	}

	public void updateSearchLevelsScreen(MenuErrorPopupUI workshopErrorPopupUI)
	{
		tweener.processTweens(Time.deltaTime);
		if (searchRoomsRequest != null && searchRoomsRequest.isDone)
		{
			if (searchRoomsRequest.responseCode == 200)
			{
				byte[] data = searchRoomsRequest.downloadHandler.data;
				string json = Encoding.Default.GetString(data);
				try
				{
					roomSearchInfos = JsonUtility.FromJson<RoomSearchInfo>(json);
					roomImagesRequests = new List<RoomImageRequest>();
					searchLevelsUI = new MenuLevelUI[roomSearchInfos.Rooms.Count];
					for (int i = 0; i < roomSearchInfos.Rooms.Count; i++)
					{
						Menu.PineRoomData pineRoomData = roomSearchInfos.Rooms[i];
						if (int.Parse(roomSearchInfos.ResultCount) > 0)
						{
							searchLevelsUI[i] = constructWorkshopSearchLevelAndSetLevelImage(pineRoomData);
							if (i == 0)
							{
								setWorkshopSearchSelectedLevel(workshopSearchRooms[pineRoomData.SteamID]);
							}
						}
						else
						{
							setWorkshopSearchSelectedLevel(null);
						}
					}
					updateLevelTextureCache();
					connectLevelTiles(new List<MenuLevelUI>(searchLevelsUI), 3);
				}
				catch (Exception message)
				{
					Debug.LogError(message);
				}
				finally
				{
					searchRoomsRequest.Dispose();
					searchRoomsRequest = null;
					updateFooter();
				}
			}
			else
			{
				Debug.Log($"Search room request returned error ({searchRoomsRequest.responseCode})");
				searchRoomsRequest = null;
				workshopErrorPopupUI.errorTitle.text = Localization.lookupInDictionary("roomDownloadingErrorTitle");
				workshopErrorPopupUI.errorLabel.text = Localization.lookupInDictionary("roomDownloadingErrorText") + " " + searchRoomsRequest.responseCode;
				workshopErrorPopupUI.root.gameObject.SetActive(value: true);
			}
		}
		if (roomImagesRequests == null)
		{
			return;
		}
		for (int num = roomImagesRequests.Count - 1; num >= 0; num--)
		{
			RoomImageRequest roomImageRequest = roomImagesRequests[num];
			if (roomImageRequest.request != null && roomImageRequest.request.isDone)
			{
				if (roomImageRequest.request.responseCode == 200)
				{
					setupLevelImage(roomImageRequest.roomID, DownloadHandlerTexture.GetContent(roomImageRequest.request));
				}
				roomImageRequest.texHandler.Dispose();
				roomImageRequest.request.Dispose();
				roomImagesRequests.RemoveAt(num);
			}
		}
	}

	private void setupLevelImage(string roomID, Texture2D texture)
	{
		if (workshopSearchRooms.ContainsKey(roomID))
		{
			Menu.WorkshopSearchRoomInfo workshopSearchRoomInfo = workshopSearchRooms[roomID];
			workshopSearchRoomInfo.cachedLevelTexture = texture;
			workshopSearchRoomInfo.cachedForCount = maxTextureCacheCount + 1;
			if (workshopSearchRoomInfo.workshopScreenMenuLevelUI != null)
			{
				workshopSearchRoomInfo.workshopScreenMenuLevelUI.levelImage.texture = texture;
				workshopSearchRoomInfo.workshopScreenMenuLevelUI.levelImageOverlay.texture = texture;
			}
			if (workshopSearchRoomInfo.communityRoomsTabMenuLevelUI != null)
			{
				workshopSearchRoomInfo.communityRoomsTabMenuLevelUI.levelImage.texture = texture;
				workshopSearchRoomInfo.communityRoomsTabMenuLevelUI.levelImageOverlay.texture = texture;
			}
			if (workshopSearchRoomInfo.communityCoopTabMenuLevelUI != null)
			{
				workshopSearchRoomInfo.communityCoopTabMenuLevelUI.levelImage.texture = texture;
				workshopSearchRoomInfo.communityCoopTabMenuLevelUI.levelImageOverlay.texture = texture;
			}
		}
	}

	public void updateRoomDownload(MenuErrorPopupUI workshopErrorPopupUI)
	{
		foreach (KeyValuePair<string, Menu.WorkshopSearchRoomInfo> workshopSearchRoom in workshopSearchRooms)
		{
			Menu.WorkshopSearchRoomInfo value = workshopSearchRoom.Value;
			UnityWebRequest unityWebRequest = value.webRequest;
			if (unityWebRequest != null && value.workshopScreenMenuLevelUI != null)
			{
				bool flag = value.levelState == Menu.WorkshopSearchLevelState.Installing;
				value.workshopScreenMenuLevelUI.installing.SetActive(flag);
				if (flag)
				{
					value.workshopScreenMenuLevelUI.installingSlider.value = unityWebRequest.downloadProgress;
				}
			}
			if (unityWebRequest == null || !unityWebRequest.isDone)
			{
				continue;
			}
			bool flag2 = false;
			bool flag3 = false;
			if (unityWebRequest.result == UnityWebRequest.Result.Success)
			{
				byte[] data = unityWebRequest.downloadHandler.data;
				if (value.levelState == Menu.WorkshopSearchLevelState.FetchingDownloadLink)
				{
					value.levelState = Menu.WorkshopSearchLevelState.Installing;
					unityWebRequest = new UnityWebRequest(unityWebRequest.downloadHandler.text);
					unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
					unityWebRequest.SendWebRequest();
					value.webRequest = unityWebRequest;
					flag2 = true;
				}
				else
				{
					value.levelState = Menu.WorkshopSearchLevelState.Installed;
					value.webRequest = null;
					if (value.workshopScreenMenuLevelUI != null)
					{
						value.workshopScreenMenuLevelUI.installing.SetActive(value: false);
						value.workshopScreenMenuLevelUI.customLevelDownloadedIcon.SetActive(value: false);
					}
					byte[] array = db.roomDataToInfoBytes(value.downloadedInfo.id, data, workshopSearchRoom.Value.downloadedInfo.Creator);
					string error;
					CustomRoomsLocalRepository.FlushRoomResult flushRoomResult = db.flushRoomToDisk(value.downloadedInfo.id, data, out error, array);
					Debug.Log(flushRoomResult.ToString() + " " + error);
					switch (flushRoomResult)
					{
					case CustomRoomsLocalRepository.FlushRoomResult.Success:
						value.levelState = Menu.WorkshopSearchLevelState.Installed;
						break;
					case CustomRoomsLocalRepository.FlushRoomResult.FailNoMemory:
						value.levelState = Menu.WorkshopSearchLevelState.Installed;
						menu.installedWorkshopSearchRooms[value.downloadedInfo.SteamID] = value;
						Debug.Log("downloaded room je jednako resultWithMetadata");
						break;
					default:
						value.levelState = Menu.WorkshopSearchLevelState.NotInstalled;
						break;
					}
					if (flushRoomResult == CustomRoomsLocalRepository.FlushRoomResult.FailNoMemory)
					{
						onDownloadError?.Invoke();
						activateErrorPopup(workshopErrorPopupUI, "%notEnoughSpaceError%", "%switchNotEnoughSpaceErrorLabel%", "%back%", delegate
						{
							deactivateErrorPopup(workshopErrorPopupUI);
						});
					}
					if (flushRoomResult == CustomRoomsLocalRepository.FlushRoomResult.Success)
					{
						Debug.Log("downloaded: " + value.downloadedInfo.id);
						Menu.WorkshopRoomInfo workshopRoomInfo = db.infoBytesToRoomInfo(array);
						if (workshopRoomInfo != null)
						{
							onLevelInstalled?.Invoke(workshopRoomInfo);
						}
					}
				}
			}
			else
			{
				flag3 = true;
				Debug.Log(unityWebRequest.error);
			}
			if (!flag2 || flag3)
			{
				value.webRequest = null;
				unityWebRequest.Dispose();
			}
		}
		if (clientWebRequestRoomInfo == null || !clientWebRequestRoomInfo.isDone)
		{
			return;
		}
		Debug.Log("Finished getting roomInfo, get room data");
		byte[] data2 = clientWebRequestRoomInfo.downloadHandler.data;
		Menu.PineRoomData pineRoomData = JsonUtility.FromJson<RoomSearchInfo>(Encoding.Default.GetString(data2)).Rooms[0];
		foreach (KeyValuePair<string, Menu.WorkshopSearchRoomInfo> workshopSearchRoom2 in workshopSearchRooms)
		{
			if (workshopSearchRoom2.Value.levelState == Menu.WorkshopSearchLevelState.FetchingDownloadLink || workshopSearchRoom2.Value.levelState == Menu.WorkshopSearchLevelState.Installing)
			{
				cancelDownload(workshopSearchRoom2.Value);
			}
		}
		constructWorkshopSearchLevelAndSetLevelImage(pineRoomData);
		startRoomDownloadCustom(pineRoomData.SteamID);
		clientWebRequestRoomInfo = null;
	}

	public void startRoomDownloadCustom(string roomSteamId)
	{
		if (workshopSearchRooms.ContainsKey(roomSteamId))
		{
			Menu.WorkshopSearchRoomInfo workshopSearchRoomInfo = workshopSearchRooms[roomSteamId];
			if (workshopSearchRoomInfo.levelState != Menu.WorkshopSearchLevelState.NotInstalled)
			{
				return;
			}
			foreach (KeyValuePair<string, Menu.WorkshopSearchRoomInfo> workshopSearchRoom in workshopSearchRooms)
			{
				if (!(roomSteamId == workshopSearchRoom.Key) && (workshopSearchRoom.Value.levelState == Menu.WorkshopSearchLevelState.FetchingDownloadLink || workshopSearchRoom.Value.levelState == Menu.WorkshopSearchLevelState.Installing))
				{
					cancelDownload(workshopSearchRoom.Value);
				}
			}
			string modified = workshopSearchRoomInfo.downloadedInfo.Modified;
			string text = (useFullFileSize ? "full" : "half");
			string text2 = "/download/" + roomSteamId + "/" + modified + "/" + text;
			workshopSearchRoomInfo.webRequest = Menu.sendWorkshopWebRequest(text2);
			Debug.Log("DOWNLOADING: '" + workshopSearchRoomInfo.downloadedInfo.Name + "' with '" + text2 + "'");
			workshopSearchRoomInfo.levelState = Menu.WorkshopSearchLevelState.FetchingDownloadLink;
		}
		else
		{
			Debug.Log("No room data saved!");
			if (clientWebRequestRoomInfo != null)
			{
				clientWebRequestRoomInfo.Abort();
			}
			string text3 = "/query/exact/" + roomSteamId;
			clientWebRequestRoomInfo = Menu.sendWorkshopWebRequest(text3);
			Debug.Log("Sending Client Room Info Web Request: '" + roomSteamId + "' with '" + text3 + "'");
		}
	}

	public void onPreviousPage()
	{
		changePage(-1);
	}

	public void onNextPage()
	{
		changePage(1);
	}

	public void onChangeSortDropDown()
	{
		currentPage = 0;
		ui.LevelSelectorHeader_SortDropdown.value = (ui.LevelSelectorHeader_SortDropdown.value + 1) % ui.LevelSelectorHeader_SortDropdown.options.Count;
	}

	public void onChangeTimeDropDown()
	{
		currentPage = 0;
		ui.LevelSelectorHeader_TimeDropdown.value = (ui.LevelSelectorHeader_TimeDropdown.value + 1) % ui.LevelSelectorHeader_TimeDropdown.options.Count;
	}

	public void onDeleteRoom(string steamId, string pineModified)
	{
		if (workshopSearchRooms.ContainsKey(steamId))
		{
			workshopSearchRooms[steamId].levelState = Menu.WorkshopSearchLevelState.NotInstalled;
			if (workshopSearchRooms[steamId].workshopScreenMenuLevelUI != null)
			{
				workshopSearchRooms[steamId].workshopScreenMenuLevelUI.customLevelDownloadedIcon.gameObject.SetActive(value: true);
			}
		}
		db.deleteRoom(db.getFileIdName(steamId, pineModified));
	}

	public bool tryGetClientDownloadProgress(string roomID, out float progress)
	{
		if (workshopSearchRooms.TryGetValue(roomID, out var value) && value.webRequest != null)
		{
			progress = value.webRequest.downloadProgress;
			return true;
		}
		progress = 0f;
		return false;
	}
}
