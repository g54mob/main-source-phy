using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelPicker
{
	public class TabData : Data
	{
		public OptionsPrefab_TabButton ui;

		public Transform container;

		public List<PackData> packs = new List<PackData>();

		public bool isPackless;

		public bool isLocked;

		public TabData(string id, bool isPackless, OptionsPrefab_TabButton ui, Transform container, bool isLocked)
		{
			base.id = id;
			this.isPackless = isPackless;
			this.ui = ui;
			this.container = container;
			this.isLocked = isLocked;
		}
	}

	public class PackData : Data
	{
		public PackUI ui;

		public GridLayoutGroup container;

		public bool canUseSearch;

		public bool canUseBrowse;

		public Comparison<LevelData> levelComparison;

		public List<LevelData> levels = new List<LevelData>();

		private string listId;

		public string listIdOverride;

		public string getListId
		{
			get
			{
				if (!string.IsNullOrEmpty(listIdOverride))
				{
					return listIdOverride;
				}
				return listId;
			}
		}

		public PackData(string id, string listId, PackUI ui, GridLayoutGroup container, bool canUseSearch, bool canUseBrowse, Comparison<LevelData> levelComparison)
		{
			base.id = id;
			this.listId = listId;
			this.ui = ui;
			this.container = container;
			this.canUseSearch = canUseSearch;
			this.canUseBrowse = canUseBrowse;
			this.levelComparison = levelComparison;
		}
	}

	public class LevelData : Data
	{
		public LevelUI ui;

		public LevelData(string id, LevelUI ui)
		{
			base.id = id;
			this.ui = ui;
		}
	}

	public class Data
	{
		public string id;

		public bool isSelected;

		public bool isVisible = true;
	}

	public Action onClick;

	public Action<Button> onDoubleClick;

	public Action<string> onBrowserClick;

	public LevelPickerUI ui;

	public string roomEditorOpenedRoomId;

	public bool isAnyLevelSelected;

	private bool controllerUsable = true;

	private List<TabData> tabs = new List<TabData>();

	private LevelProvider provider;

	private bool shouldSyncVisuals;

	private TabData currentProcessingTab;

	private bool isTabless;

	private string currentSelected;

	private List<LevelData> passedLevelDatasCache = new List<LevelData>();

	private GameObject previouslySelectedControllerObject;

	private bool isVR
	{
		get
		{
			if (VR.instance != null)
			{
				return VR.instance.isActive();
			}
			return false;
		}
	}

	public LevelPicker(LevelPickerUI ui, LevelProvider provider)
	{
		this.ui = ui;
		this.provider = provider;
		PineUI.addButtonDownDelegate(onButtonClick);
		PineUI.addButtonInteractionDelegate(onButtonEvent);
		addTriggerEntry(ui.LevelSelection_Search_SearchBg_SearchInputField.gameObject, EventTriggerType.Select).callback.AddListener(delegate
		{
			onSearchSelected();
		});
		ui.LevelSelection_Search_SearchBg_SearchInputField.onValueChanged.AddListener(delegate
		{
			if (getSelectedPack() != null)
			{
				setShouldSyncVisuals();
			}
		});
	}

	public void update()
	{
		foreach (TabData tab in tabs)
		{
			foreach (PackData pack in tab.packs)
			{
				List<string> list = provider.getList(pack.getListId);
				passedLevelDatasCache.Clear();
				foreach (string levelId in list)
				{
					LevelData levelData = pack.levels.Find((LevelData x) => x.id == levelId);
					LevelProvider.LevelItem levelItem = provider.getLevelItem(levelId);
					if (levelItem.scheduledDirtyTime > 0f)
					{
						levelItem.scheduledDirtyTime -= Time.deltaTime;
						levelItem.isDirty = levelItem.scheduledDirtyTime <= 0f;
					}
					if (levelData == null)
					{
						levelData = startLevel(levelId, pack, levelItem);
						string idFromButton = getIdFromButton(levelData.ui.root);
						modifyLevel(idFromButton, levelItem);
						setShouldSyncVisuals();
					}
					if (levelItem.isDirty)
					{
						string idFromButton2 = getIdFromButton(levelData.ui.root);
						modifyLevel(idFromButton2, levelItem);
						setShouldSyncVisuals();
					}
					passedLevelDatasCache.Add(levelData);
				}
				for (int num = pack.levels.Count - 1; num >= 0; num--)
				{
					LevelData levelData2 = pack.levels[num];
					if (!passedLevelDatasCache.Contains(levelData2))
					{
						UnityEngine.Object.Destroy(levelData2.ui.gameObject);
						pack.levels.Remove(levelData2);
						if (pack.isSelected)
						{
							select(getIdFromButton(pack.ui.root));
						}
					}
				}
			}
		}
		if (shouldSyncVisuals)
		{
			syncVisuals();
			shouldSyncVisuals = false;
		}
		updateController(forceChange: false);
		updateVR();
	}

	public void setControllerUsable(bool value)
	{
		controllerUsable = value;
		PackData selectedPack = getSelectedPack();
		Navigation navigation = default(Navigation);
		foreach (LevelData level in selectedPack.levels)
		{
			level.ui.root.navigation = (value ? Navigation.defaultNavigation : navigation);
		}
	}

	public void selectPack()
	{
		PackData selectedPack = getSelectedPack();
		if (selectedPack.levels.Count > 0)
		{
			LevelData levelData = selectedPack.levels[0];
			select(getIdFromButton(levelData.ui.root));
			Controller.selectSelectable(levelData.ui.root);
			setShouldSyncVisuals();
		}
	}

	public void updateController(bool forceChange)
	{
		if (!controllerUsable || !ui.gameObject.activeInHierarchy)
		{
			return;
		}
		if (Controller.controllerModeChanged || forceChange)
		{
			Controller.selectSelectable(getSelectedLevelOrPackButton());
			setShouldSyncVisuals();
		}
		if (!Controller.isActive())
		{
			return;
		}
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		if (previouslySelectedControllerObject != currentSelectedGameObject && currentSelectedGameObject != null)
		{
			string idFromButton = getIdFromButton(currentSelectedGameObject.GetComponent<Button>());
			if (!string.IsNullOrEmpty(idFromButton))
			{
				select(idFromButton);
				previouslySelectedControllerObject = currentSelectedGameObject;
				setShouldSyncVisuals();
			}
		}
		if (Controller.getButtonDown(ControllerButtonActionType.UIMoveTabLeft))
		{
			moveTab(-1);
		}
		if (Controller.getButtonDown(ControllerButtonActionType.UIMoveTabRight))
		{
			moveTab(1);
		}
		if (getSelectedLevel() == null && (Controller.getAxis(ControllerAxisActionType.UIMoveAndRotate).x > 0.5f || Controller.getAxis(ControllerAxisActionType.UIMoveAndCursor).x > 0.5f))
		{
			selectPack();
		}
		if (ui.LevelSelection_Search_BrowseButton_BrowseControllerHint.gameObject.activeInHierarchy && Controller.getButtonDown(ui.LevelSelection_Search_BrowseButton_BrowseControllerHint.controllerButton))
		{
			onButtonClick(ui.LevelSelection_Search_BrowseButton);
		}
		void moveTab(int direction)
		{
			if (!isTabless)
			{
				int num = tabs.IndexOf(getSelectedTab());
				num += direction;
				num = ((num >= 0) ? (num % tabs.Count) : (tabs.Count - 1));
				TabData tabData = tabs[num];
				if (!tabData.isVisible)
				{
					moveTab(direction + direction);
				}
				else
				{
					PackData packData = tabs[num].packs[0];
					if (!packData.isVisible)
					{
						for (int i = 1; i < tabs[num].packs.Count; i++)
						{
							if (tabs[num].packs[i].isVisible)
							{
								packData = tabs[num].packs[i];
								break;
							}
						}
					}
					if (tabData.isPackless)
					{
						LevelData levelData = packData.levels[0];
						select(tabData.id + "." + packData.id + "." + levelData.id);
						Controller.selectSelectable(levelData.ui.root);
					}
					else
					{
						select(tabData.id + "." + packData.id);
						Controller.selectSelectable(packData.ui.root);
					}
				}
			}
		}
	}

	public void updateVR()
	{
		if (isVR && VR.instance.input.RightHand.PrimaryButton.WasPressedThisFrame())
		{
			onKeyboardDeletePress();
		}
	}

	public void setTabless()
	{
		isTabless = true;
	}

	public void startTab(string id, string label, bool? isNew = null, bool? isBeta = null, bool isLocked = false, string packlessList = null)
	{
		OptionsPrefab_TabButton tabButton = OptionsPrefabs.get().getTabButton(label, ui.Frame.ui.Tabs_Parent);
		tabButton.transform.SetParent(ui.Frame.ui.Tabs_Parent);
		tabButton.transform.SetSiblingIndex(ui.Frame.ui.Tabs_Parent.transform.childCount - 3);
		tabButton.locked.gameObject.SetActive(isLocked);
		if (isLocked)
		{
			tabButton.button.interactable = false;
		}
		Transform container = UnityEngine.Object.Instantiate(ui.PackScroll_PackContent, ui.PackScroll_PackContent.transform.parent);
		bool flag = !string.IsNullOrEmpty(packlessList);
		TabData item = new TabData(id, flag, tabButton, container, isLocked);
		tabs.Add(item);
		currentProcessingTab = item;
		if (flag)
		{
			definePack(packlessList, packlessList, canUseSearch: false, canUseBrowse: false, null);
		}
	}

	public void definePack(string id, string listId, bool canUseSearch, bool canUseBrowse, Comparison<LevelData> comparison)
	{
		if (currentProcessingTab == null)
		{
			startTab("", "");
			isTabless = true;
		}
		if (canUseSearch && comparison == null)
		{
			comparison = getSearchComparison();
		}
		PackUI packUI = UnityEngine.Object.Instantiate(ui.PackScroll_PackContent_Pack, currentProcessingTab.container);
		packUI.gameObject.SetActive(value: true);
		GridLayoutGroup container = UnityEngine.Object.Instantiate(ui.LevelSelection_Scroll_LevelsContent_Panel, ui.LevelSelection_Scroll_LevelsContent_Panel.transform.parent);
		PackData item = new PackData(id, listId, packUI, container, canUseSearch, canUseBrowse, comparison);
		currentProcessingTab.packs.Add(item);
		if (!string.IsNullOrEmpty(currentSelected) && currentSelected.Contains(currentProcessingTab.id))
		{
			Debug.Log(currentProcessingTab.container, currentProcessingTab.container);
		}
	}

	public void defineSeparator()
	{
		UnityEngine.Object.Instantiate(ui.PackScroll_PackContent_Separator, currentProcessingTab.container).gameObject.SetActive(value: true);
	}

	private EventTrigger.Entry addTriggerEntry(GameObject uiObject, EventTriggerType type)
	{
		if (!uiObject.TryGetComponent<PineUITrigger>(out var component))
		{
			component = uiObject.gameObject.AddComponent<PineUITrigger>();
		}
		EventTrigger.Entry entry = new EventTrigger.Entry
		{
			eventID = type
		};
		component.triggers.Add(entry);
		return entry;
	}

	public void use(string tabId)
	{
		currentProcessingTab = tabs.Find((TabData x) => x.id == tabId);
	}

	public bool isSelected(string id)
	{
		if (currentSelected == id)
		{
			return true;
		}
		var (text, text2, text3) = getSelectedId();
		var (text4, text5, text6) = parseId(id);
		if (text == text4 && text2 == text5 && text3 == text6)
		{
			return true;
		}
		return false;
	}

	public void select(string id, bool selectClosestTo = false)
	{
		Debug.Log("select " + id + " " + Time.frameCount);
		if (currentSelected == id)
		{
			return;
		}
		(string tabId, string packId, string levelId) tuple = parseId(id);
		string item = tuple.tabId;
		string item2 = tuple.packId;
		string item3 = tuple.levelId;
		isAnyLevelSelected = false;
		string text = currentSelected;
		TabData tabData = null;
		PackData packData = null;
		LevelData levelData = null;
		foreach (TabData tab in tabs)
		{
			tab.isSelected = tab.id == item;
			if (tab.isSelected)
			{
				tabData = tab;
			}
			foreach (PackData pack in tab.packs)
			{
				pack.isSelected = tab.isSelected && pack.id == item2;
				if (pack.isSelected)
				{
					packData = pack;
				}
				foreach (LevelData level in pack.levels)
				{
					level.isSelected = pack.isSelected && level.id == item3;
					if (level.isSelected)
					{
						levelData = level;
					}
					if (pack.isSelected)
					{
						provider.requestData(level.id);
						isAnyLevelSelected = pack.levels.Exists((LevelData x) => x.isSelected);
					}
				}
			}
		}
		if (selectClosestTo)
		{
			if (tabData == null)
			{
				select(tabs[0].id);
			}
			else if (packData == null)
			{
				select(tabData.id + "." + tabData.packs[0].id);
			}
			else if (levelData == null)
			{
				if (packData.levels.Count > 0)
				{
					select(tabData.id + "." + packData.id + "." + packData.levels[0].id);
				}
				else
				{
					select(tabData.id + "." + tabData.packs[0].id);
				}
			}
		}
		currentSelected = id;
		setShouldSyncVisuals();
		if (text != currentSelected)
		{
			closeKeyboard();
			onClick();
		}
	}

	public bool onBackFromLevelSelectOnController(bool fromUIButton)
	{
		if ((isVR || Controller.isActive()) && isKeyboardActive())
		{
			closeKeyboard();
			return !fromUIButton;
		}
		if (!Controller.isActive())
		{
			return false;
		}
		if (!isAnyLevelSelected)
		{
			return false;
		}
		if (getSelectedTab().isPackless)
		{
			return false;
		}
		if (!controllerUsable)
		{
			return false;
		}
		Debug.Log("back!!");
		(string, string, string) selectedId = getSelectedId();
		select(selectedId.Item1 + "." + selectedId.Item2);
		return true;
	}

	public (string tabId, string packId, string levelId) getSelectedId()
	{
		if (string.IsNullOrEmpty(currentSelected))
		{
			return (tabId: null, packId: null, levelId: null);
		}
		return parseId(currentSelected);
	}

	public string getSelectedIdRaw()
	{
		return currentSelected;
	}

	public void changeVisibility(string id, bool visible)
	{
		Data dataFromId = getDataFromId(id);
		if (dataFromId == null || dataFromId.isVisible == visible)
		{
			return;
		}
		dataFromId.isVisible = visible;
		if (!visible && dataFromId.isSelected)
		{
			dataFromId.isSelected = false;
			PackData packData = dataFromId as PackData;
			if (packData != null)
			{
				TabData tabData = tabs.Find((TabData x) => x.packs.Contains(packData));
				PackData packData2 = tabData.packs.First((PackData x) => x.isVisible);
				if (packData2 != null)
				{
					select(tabData.id + "." + packData2.id);
				}
			}
		}
		setShouldSyncVisuals();
	}

	public void modify(string id, string label)
	{
		Data dataFromId = getDataFromId(id);
		Text text = null;
		if (dataFromId is TabData tabData)
		{
			text = tabData.ui.Text;
		}
		if (dataFromId is PackData packData)
		{
			text = packData.ui.Name;
		}
		if (dataFromId is LevelData levelData)
		{
			text = levelData.ui.LevelName;
		}
		text.text = label;
		Localization.translateObject(text.transform, forceNewText: true);
	}

	public void modifyPack(string id, string label = null, bool? isNew = null, bool? isFree = null, bool? isLine = null, bool? canUseSearch = null, bool? canUseBrowse = null, bool? visible = null, int? levelCount = null, string listIdOverride = null, Comparison<LevelData> levelComparison = null, LevelProvider.PackSize? packSize = null)
	{
		PackData packData = (PackData)getDataFromId(id);
		_ = levelCount.HasValue;
		if (label != null)
		{
			packData.ui.Name.text = label;
			Localization.translateObject(packData.ui.Name.transform, forceNewText: true);
		}
		_ = isNew.HasValue;
		_ = isFree.HasValue;
		if (isLine.HasValue)
		{
			packData.ui.Name.gameObject.SetActive(!isLine.Value);
			packData.ui.PreviewLbl.gameObject.SetActive(!isLine.Value);
			packData.ui.Line.gameObject.SetActive(isLine.Value);
			packData.ui.root.interactable = !isLine.Value;
		}
		if (canUseSearch.HasValue)
		{
			packData.canUseSearch = canUseSearch.Value;
		}
		if (canUseBrowse.HasValue)
		{
			packData.canUseBrowse = canUseBrowse.Value;
		}
		if (visible.HasValue)
		{
			packData.isVisible = visible.Value;
		}
		if (levelComparison != null)
		{
			packData.levelComparison = levelComparison;
		}
		if (listIdOverride != null)
		{
			packData.listIdOverride = listIdOverride;
		}
		if (packSize.HasValue)
		{
			if (packSize.Value == LevelProvider.PackSize.StandardRoom)
			{
				packData.container.cellSize = new Vector2(325f, 405f);
			}
			if (packSize.Value == LevelProvider.PackSize.CustomRoom)
			{
				packData.container.cellSize = new Vector2(380f, 225f);
			}
			if (packSize.Value == LevelProvider.PackSize.DarkestRoom)
			{
				packData.container.cellSize = new Vector2(325f, 325f);
			}
		}
	}

	public void setOverrideListIdOnWholeTab(string tabId, string listId)
	{
		foreach (TabData tab in tabs)
		{
			if (tab.id != tabId)
			{
				continue;
			}
			foreach (PackData pack in tab.packs)
			{
				pack.listIdOverride = listId;
			}
		}
	}

	public List<string> getCurrentLevels()
	{
		foreach (TabData tab in tabs)
		{
			if (!tab.isSelected)
			{
				continue;
			}
			foreach (PackData pack in tab.packs)
			{
				if (pack.isSelected)
				{
					return provider.getList(pack.getListId);
				}
			}
		}
		return new List<string>();
	}

	public void modifyLevel(string levelId, LevelProvider.LevelItem underlyingData)
	{
		LevelData levelData = (LevelData)getDataFromId(levelId);
		if (levelData == null)
		{
			Debug.LogError("[LP ERROR] modifyLevel - getDatafromID '" + levelId + "' returned null");
		}
		else
		{
			modifyLevelUI(levelData.ui, underlyingData);
		}
	}

	public void setLevelVisibility(string levelId, bool visible)
	{
		LevelData levelData = (LevelData)getDataFromId(levelId);
		if (levelData != null)
		{
			levelData.isVisible = visible;
			if (!visible && levelData.isSelected)
			{
				select(getSelectedTab().id + "." + getSelectedPack().id);
			}
			setShouldSyncVisuals();
		}
	}

	public (string tabId, string packId, string levelId) parseId(string id)
	{
		string tabId = "";
		string item = "";
		string item2 = "";
		string[] array = id.Split(".");
		tabId = array[0];
		TabData tabData = tabs.Find((TabData x) => x.id == tabId);
		if (array.Length > 1)
		{
			item = ((!tabData.isPackless) ? array[1] : tabData.packs[0].id);
			if (array.Length > 2)
			{
				item2 = array[2];
			}
		}
		return (tabId: tabId, packId: item, levelId: item2);
	}

	public Button getSelectedPackButton()
	{
		return getSelectedPack()?.ui.root;
	}

	public Transform getSelectedTabContainer()
	{
		return tabs.Find((TabData x) => x.isSelected).container;
	}

	public static void modifyLevelUI(LevelUI levelUI, LevelProvider.LevelItem data)
	{
		if (data.levelName != null)
		{
			string levelName = data.levelName;
			Text levelName2 = levelUI.LevelName;
			string text;
			if (levelName.Length < 2 || !levelName.StartsWith("%") || !levelName.EndsWith("%"))
			{
				text = levelName;
			}
			else
			{
				string text2 = levelName;
				text = Localization.lookupInDictionary(text2.Substring(1, text2.Length - 1 - 1), levelName);
			}
			levelName2.text = text;
		}
		if (data.displayMode == LevelProvider.DisplayMode.Hidden)
		{
			levelUI.LevelName.gameObject.SetActive(value: false);
		}
		if (data.texture != null)
		{
			levelUI.LevelImage.texture = data.texture;
			levelUI.LevelImageOverlay.texture = data.texture;
		}
		levelUI.Spinner.gameObject.SetActive(data.texture == null);
		if (data.installProgress != -1f)
		{
			levelUI.Installing.gameObject.SetActive(value: true);
			levelUI.Installing_InstallingSlider.minValue = 0f;
			levelUI.Installing_InstallingSlider.maxValue = 1f;
			levelUI.Installing_InstallingSlider.value = data.installProgress;
		}
		else
		{
			levelUI.Installing.gameObject.SetActive(value: false);
		}
		if (data.tokensMin != -1 && data.tokensMax != -1 && data.displayMode != LevelProvider.DisplayMode.Hidden)
		{
			levelUI.LevelTokensCollected.text = $"{data.tokensMin}/{data.tokensMax}";
			levelUI.TokenIcon.gameObject.SetActive(value: true);
		}
		else
		{
			levelUI.LevelTokensCollected.text = "";
			levelUI.TokenIcon.gameObject.SetActive(value: false);
		}
		levelUI.SaveDate.text = data.date;
		levelUI.SaveDate.gameObject.SetActive(!string.IsNullOrEmpty(data.date));
		levelUI.PlayTime.text = data.playTime;
		levelUI.PlayTime.gameObject.SetActive(!string.IsNullOrEmpty(data.playTime));
		levelUI.LevelFinishIcon.gameObject.SetActive(data.finished);
		levelUI.LevelTrophyIcon.gameObject.SetActive(data.gotTrophy);
		levelUI.CustomLevelNewIcon.gameObject.SetActive(data.isNew);
		levelUI.Locked.gameObject.SetActive(data.isLocked);
		if (data.unlockTime.Ticks != 0L)
		{
			levelUI.LevelFinishIcon.gameObject.SetActive(value: false);
			levelUI.LevelTrophyIcon.gameObject.SetActive(value: false);
			levelUI.TokenIcon.gameObject.SetActive(value: false);
			levelUI.LevelTokensCollected.gameObject.SetActive(value: false);
			data.scheduledDirtyTime = 1f;
		}
	}

	private LevelData startLevel(string id, PackData pack, LevelProvider.LevelItem underlyingData)
	{
		LevelUI original = null;
		if (underlyingData.levelType == LevelProvider.LevelItemType.Normal)
		{
			original = ui.LevelSelection_Scroll_LevelsContent_Panel_Level;
		}
		else if (underlyingData.levelType == LevelProvider.LevelItemType.DLCNotInstalled)
		{
			original = ui.LevelSelection_Scroll_LevelsContent_Panel_DLCNotInstalled;
		}
		LevelUI levelUI = UnityEngine.Object.Instantiate(original, pack.container.transform);
		levelUI.gameObject.SetActive(value: true);
		LevelData levelData = new LevelData(id, levelUI);
		pack.levels.Add(levelData);
		return levelData;
	}

	private void onButtonEvent(Button button, EventTriggerType type, PointerEventData data)
	{
		if (!ui.gameObject.activeInHierarchy)
		{
			return;
		}
		if (type == EventTriggerType.PointerClick && data.clickCount == 2 && getIdFromButton(button) != null)
		{
			onDoubleClick(button);
		}
		switch (type)
		{
		case EventTriggerType.PointerEnter:
		{
			string idFromButton2 = getIdFromButton(button);
			if (!string.IsNullOrEmpty(idFromButton2) && getDataFromId(idFromButton2) is PackData packData2)
			{
				Themes.setColorWithoutAlpha(packData2.ui.Name, Menu.getTheme().buttonHoverableColors.hoverColorText);
			}
			break;
		}
		case EventTriggerType.PointerExit:
		{
			string idFromButton = getIdFromButton(button);
			if (!string.IsNullOrEmpty(idFromButton) && getDataFromId(idFromButton) is PackData packData)
			{
				Themes.ColorsButtonHoverable buttonHoverableColors = Menu.getTheme().buttonHoverableColors;
				Color color = (packData.isSelected ? buttonHoverableColors.selectedColorText : buttonHoverableColors.unselectedColorText);
				Themes.setColorWithoutAlpha(packData.ui.Name, color);
			}
			break;
		}
		}
	}

	private void onButtonClick(Button button)
	{
		foreach (TabData tab in tabs)
		{
			if (button == tab.ui.button)
			{
				handleClickedTab(tab.id);
				break;
			}
			if (!tab.isSelected)
			{
				continue;
			}
			foreach (PackData pack in tab.packs)
			{
				if (button == pack.ui.root)
				{
					handleClickPack(tab.id, pack.id);
					break;
				}
				if (!pack.isSelected)
				{
					continue;
				}
				foreach (LevelData level in pack.levels)
				{
					if (button == level.ui.root)
					{
						handleClickLevel(tab.id, pack.id, level.id);
						break;
					}
				}
			}
		}
		if (button == ui.LevelSelection_Search_BrowseButton)
		{
			onBrowserClick(ui.LevelSelection_Search_SearchBg_SearchInputField.text);
		}
		if (button == ui.LevelSelection_Scroll_Keyboard.VRHints_VRDeleteButton)
		{
			onKeyboardDeletePress();
		}
		if (button == ui.LevelSelection_Scroll_Keyboard.VRHints_VRBackButton)
		{
			closeKeyboard();
		}
		void handleClickLevel(string tabId, string packId, string levelId)
		{
			string text = "";
			text = text + tabId + ".";
			if (packId != "")
			{
				text += packId;
			}
			text = text + "." + levelId;
			select(text);
		}
		void handleClickPack(string tabId, string packId)
		{
			PackData packData = ((TabData)getDataFromId(tabId)).packs.Find((PackData x) => x.id == packId);
			if (packData.levels.Count > 0)
			{
				select(tabId + "." + packId + "." + packData.levels[0].id);
			}
			else
			{
				select(tabId + "." + packId);
			}
		}
		void handleClickedTab(string tabId)
		{
			TabData tabData = (TabData)getDataFromId(tabId);
			if (tabData.packs.Count > 0)
			{
				for (int i = 0; i < tabData.packs.Count; i++)
				{
					if (tabData.packs[i].isVisible)
					{
						handleClickPack(tabId, tabData.packs[i].id);
						break;
					}
				}
			}
			else
			{
				select(tabId);
			}
		}
	}

	private Data getDataFromId(string id)
	{
		var (text, text2, text3) = parseId(id);
		foreach (TabData tab in tabs)
		{
			if (tab.id == text && text2 == "" && text3 == "")
			{
				return tab;
			}
			if (tab.id != text)
			{
				continue;
			}
			foreach (PackData pack in tab.packs)
			{
				if (pack.id == text2 && text3 == "")
				{
					return pack;
				}
				if (pack.id != text2)
				{
					continue;
				}
				foreach (LevelData level in pack.levels)
				{
					if (level.id == text3)
					{
						return level;
					}
				}
			}
		}
		return null;
	}

	private Button getButtonFromId(string id)
	{
		Data dataFromId = getDataFromId(id);
		if (dataFromId is TabData tabData)
		{
			return tabData.ui.button;
		}
		if (dataFromId is PackData packData)
		{
			return packData.ui.root;
		}
		if (dataFromId is LevelData levelData)
		{
			return levelData.ui.root;
		}
		return null;
	}

	private string getIdFromButton(Button button)
	{
		foreach (TabData tab in tabs)
		{
			if (tab.ui.button == button)
			{
				return tab.id;
			}
			foreach (PackData pack in tab.packs)
			{
				if (pack.ui.root == button)
				{
					return tab.id + "." + pack.id;
				}
				foreach (LevelData level in pack.levels)
				{
					if (level.ui.root == button)
					{
						return tab.id + "." + pack.id + "." + level.id;
					}
				}
			}
		}
		return null;
	}

	public void setShouldSyncVisuals()
	{
		shouldSyncVisuals = true;
	}

	private void syncVisuals()
	{
		Themes theme = Menu.getTheme();
		ui.Frame.ui.Tabs_Parent.gameObject.SetActive(!isTabless);
		foreach (TabData tab in tabs)
		{
			tab.ui.gameObject.SetActive(tab.isVisible);
			tab.ui.Text.color = (tab.isSelected ? theme.tabButtonColors.selectedColorText : theme.tabButtonColors.unselectedColorText);
			tab.ui.selectedImage.gameObject.SetActive(tab.isSelected);
			if (tab.isSelected)
			{
				ui.PackScroll.gameObject.SetActive(!tab.isPackless);
				ui.PackScroll.GetComponent<UndraggableRect>().content = (RectTransform)tab.container;
			}
			foreach (PackData pack in tab.packs)
			{
				pack.ui.gameObject.SetActive(pack.isVisible);
				if (pack.isSelected)
				{
					bool flag = !Controller.isActive() && pack.canUseSearch;
					ui.LevelSelection_Scroll.content = (RectTransform)pack.container.transform;
					ui.LevelSelection_Search.gameObject.SetActive(flag || pack.canUseBrowse);
					ui.LevelSelection_Search_BrowseButton.gameObject.SetActive(pack.canUseBrowse);
					ui.LevelSelection_Search_SearchBg.gameObject.SetActive(flag);
					if (Controller.isActive())
					{
						pack.ui.root.GetComponent<TweenStateButton>()?.OnPointerEnter(null);
					}
					pack.container.childAlignment = (tab.isPackless ? TextAnchor.MiddleCenter : TextAnchor.UpperLeft);
				}
				tab.container.gameObject.SetActive(tab.isSelected);
				pack.container.gameObject.SetActive(pack.isSelected);
				Color color = (pack.isSelected ? theme.buttonHoverableColors.selectedColorText : theme.buttonHoverableColors.unselectedColorText);
				Themes.setColorWithoutAlpha(pack.ui.Name, color);
				if (pack.isSelected && pack.levelComparison != null)
				{
					pack.levels.Sort(pack.levelComparison);
					for (int i = 0; i < pack.levels.Count; i++)
					{
						pack.levels[i].ui.transform.SetSiblingIndex(i);
					}
				}
				setupColumnNavigation(pack, tab.id.Contains("ugc") ? 3 : 4);
			}
		}
		ui.LevelSelection_Search_BrowseButton_BrowseControllerHint.gameObject.SetActive(Controller.isActive());
		ui.LevelSelection_Search_SearchBg_Image.gameObject.SetActive(Controller.isActive());
		ui.LevelSelection_Search_BrowseButton_BrowsePlus.gameObject.SetActive(!Controller.isActive());
		if (!ui.LevelSelection_Search_SearchBg_SearchInputField.gameObject.activeInHierarchy)
		{
			closeKeyboard();
		}
		void setupColumnNavigation(PackData pack, int columns)
		{
			for (int j = 0; j < pack.levels.Count; j++)
			{
				LevelData levelData = pack.levels[j];
				levelData.ui.gameObject.SetActive(levelData.isVisible);
				levelData.ui.SelectionFrame.gameObject.SetActive(levelData.isSelected);
				levelData.ui.CurrentlyOpenedInEditor.gameObject.SetActive(levelData.id == roomEditorOpenedRoomId);
				levelData.ui.LevelImageOverlay.gameObject.SetActive(levelData.isSelected);
				levelData.ui.LevelImageOverlay.enabled = levelData.isSelected;
				levelData.ui.LevelImage.color = (levelData.isSelected ? Color.white : new Color(0.5f, 0.5f, 0.5f, 1f));
				int num = j / columns;
				int num2 = j % columns;
				int num3 = (num - 1) * columns + num2;
				int num4 = (num + 1) * columns + num2;
				int num5 = ((num2 > 0) ? (j - 1) : (-1));
				int num6 = ((num2 < columns - 1) ? (j + 1) : (-1));
				LevelData levelData2 = ((num3 >= 0 && num3 < pack.levels.Count) ? pack.levels[num3] : null);
				LevelData levelData3 = ((num4 >= 0 && num4 < pack.levels.Count) ? pack.levels[num4] : null);
				LevelData levelData4 = ((num5 >= 0 && num5 < pack.levels.Count) ? pack.levels[num5] : null);
				LevelData levelData5 = ((num6 >= 0 && num6 < pack.levels.Count) ? pack.levels[num6] : null);
				Navigation navigation = new Navigation
				{
					mode = Navigation.Mode.Explicit,
					selectOnUp = levelData2?.ui.root,
					selectOnDown = levelData3?.ui.root,
					selectOnLeft = levelData4?.ui.root,
					selectOnRight = levelData5?.ui.root
				};
				levelData.ui.root.navigation = navigation;
			}
		}
	}

	public Comparison<LevelData> getOculusAvailabilityComparison()
	{
		return delegate(LevelData x, LevelData y)
		{
			bool isAvailableOnOculus = RoomDatabase.getRoom(x.id).isAvailableOnOculus;
			bool isAvailableOnOculus2 = RoomDatabase.getRoom(y.id).isAvailableOnOculus;
			int num = isAvailableOnOculus2.CompareTo(isAvailableOnOculus);
			return (num == 0) ? string.Compare(x.id, y.id, StringComparison.Ordinal) : num;
		};
	}

	public Comparison<LevelData> getSaveComparison()
	{
		return delegate(LevelData x, LevelData y)
		{
			long lastSaveTimeInTicks = provider.ensureLevelItem(x.id).lastSaveTimeInTicks;
			long lastSaveTimeInTicks2 = provider.ensureLevelItem(y.id).lastSaveTimeInTicks;
			return lastSaveTimeInTicks2.CompareTo(lastSaveTimeInTicks);
		};
	}

	public Comparison<LevelData> getInstalledLevelsComparer()
	{
		return delegate(LevelData x, LevelData y)
		{
			string text = ui.LevelSelection_Search_SearchBg_SearchInputField.text;
			int num2;
			if (string.IsNullOrEmpty(text))
			{
				int num = 0;
				if (x.ui.LevelFinishIcon.gameObject.activeSelf)
				{
					num = 1;
				}
				if (x.ui.LevelTrophyIcon.gameObject.activeSelf)
				{
					num = 2;
				}
				int value = 0;
				if (y.ui.LevelFinishIcon.gameObject.activeSelf)
				{
					value = 1;
				}
				if (y.ui.LevelTrophyIcon.gameObject.activeSelf)
				{
					value = 2;
				}
				num2 = num.CompareTo(value);
				if (num2 != 0)
				{
					return num2;
				}
				DateTime dateFolderCreated = provider.ensureLevelItem(x.id).dateFolderCreated;
				DateTime dateFolderCreated2 = provider.ensureLevelItem(y.id).dateFolderCreated;
				num2 = dateFolderCreated2.CompareTo(dateFolderCreated);
			}
			else
			{
				num2 = getRelevanceScore(y.ui.LevelName.text, text).CompareTo(getRelevanceScore(x.ui.LevelName.text, text));
			}
			return (num2 == 0) ? x.ui.LevelName.text.CompareTo(y.ui.LevelName.text) : num2;
		};
	}

	public Comparison<LevelData> getSearchComparison()
	{
		return delegate(LevelData x, LevelData y)
		{
			string text = ui.LevelSelection_Search_SearchBg_SearchInputField.text;
			int num;
			if (string.IsNullOrEmpty(text))
			{
				DateTime dateFolderCreated = provider.ensureLevelItem(x.id).dateFolderCreated;
				DateTime dateFolderCreated2 = provider.ensureLevelItem(y.id).dateFolderCreated;
				num = dateFolderCreated2.CompareTo(dateFolderCreated);
			}
			else
			{
				num = getRelevanceScore(y.ui.LevelName.text, text).CompareTo(getRelevanceScore(x.ui.LevelName.text, text));
			}
			return (num == 0) ? x.ui.LevelName.text.CompareTo(y.ui.LevelName.text) : num;
		};
	}

	public Comparison<LevelData> getYourRoomsSorter(string pinnedId)
	{
		return delegate(LevelData x, LevelData y)
		{
			if (x.id == pinnedId)
			{
				return -1;
			}
			if (y.id == pinnedId)
			{
				return 1;
			}
			string text = ui.LevelSelection_Search_SearchBg_SearchInputField.text;
			int num;
			if (string.IsNullOrEmpty(text))
			{
				DateTime dateRoomModified = provider.ensureLevelItem(x.id).dateRoomModified;
				DateTime dateRoomModified2 = provider.ensureLevelItem(y.id).dateRoomModified;
				num = dateRoomModified2.CompareTo(dateRoomModified);
				if (num != 0)
				{
					return num;
				}
				return string.Compare(y.id, x.id, StringComparison.Ordinal);
			}
			num = getRelevanceScore(y.ui.LevelName.text, text).CompareTo(getRelevanceScore(x.ui.LevelName.text, text));
			return (num == 0) ? string.Compare(x.ui.LevelName.text, y.ui.LevelName.text, StringComparison.Ordinal) : num;
		};
	}

	private int getRelevanceScore(string name, string searchPattern)
	{
		name = name.ToLower();
		searchPattern = searchPattern.ToLower();
		int num = getCommonPrefixLength(name, searchPattern);
		int num2 = getCommonSubstringLength(name, searchPattern) * 5;
		return num + num2;
		static int getCommonPrefixLength(string str1, string str2)
		{
			int num3 = Math.Min(str1.Length, str2.Length);
			int num4 = 0;
			for (int i = 0; i < num3; i++)
			{
				num4 = ((str1[i] != str2[i]) ? (num4 - 1) : (num4 + 1));
			}
			return num4;
		}
		static int getCommonSubstringLength(string str1, string str2)
		{
			int num3 = 0;
			for (int i = 0; i < str1.Length; i++)
			{
				for (int j = 0; j < str2.Length; j++)
				{
					int k;
					for (k = 0; i + k < str1.Length && j + k < str2.Length && str1[i + k] == str2[j + k]; k++)
					{
					}
					num3 = Math.Max(num3, k);
				}
			}
			return num3;
		}
	}

	private PackData getSelectedPack()
	{
		return tabs.Find((TabData x) => x.isSelected)?.packs.Find((PackData x) => x.isSelected);
	}

	private TabData getSelectedTab()
	{
		return tabs.Find((TabData x) => x.isSelected);
	}

	private LevelData getSelectedLevel()
	{
		return tabs.Find((TabData x) => x.isSelected)?.packs.Find((PackData x) => x.isSelected)?.levels.Find((LevelData x) => x.isSelected);
	}

	private Button getSelectedLevelOrPackButton()
	{
		(string tabId, string packId, string levelId) selectedId = getSelectedId();
		string item = selectedId.packId;
		if (!string.IsNullOrEmpty(selectedId.levelId))
		{
			return getSelectedLevel().ui.root;
		}
		if (!string.IsNullOrEmpty(item))
		{
			return getSelectedPack().ui.root;
		}
		return null;
	}

	private bool isKeyboardActive()
	{
		return ui.LevelSelection_Scroll_Keyboard.gameObject.activeInHierarchy;
	}

	private void closeKeyboard()
	{
		if (isKeyboardActive())
		{
			ui.LevelSelection_Search_SearchBg_SearchInputField.DeactivateInputField();
			ui.LevelSelection_Scroll_Keyboard.gameObject.SetActive(value: false);
		}
	}

	private void onKeyboardDeletePress()
	{
		if (isKeyboardActive())
		{
			string text = ui.LevelSelection_Search_SearchBg_SearchInputField.text;
			if (text.Length > 0)
			{
				ui.LevelSelection_Search_SearchBg_SearchInputField.text = text.Substring(0, text.Length - 1);
			}
		}
	}

	private void onSearchSelected()
	{
		if (isVR)
		{
			ui.LevelSelection_Scroll_Keyboard.gameObject.SetActive(value: true);
			ui.LevelSelection_Search_SearchBg_SearchInputField.MoveTextEnd(shift: false);
			ui.LevelSelection_Search_SearchBg_SearchInputField.caretPosition = ui.LevelSelection_Search_SearchBg_SearchInputField.text.Length - 1;
			ui.LevelSelection_Scroll_Keyboard.root.usePointer = false;
			ui.LevelSelection_Scroll_Keyboard.VRHints_VRDeleteButton.gameObject.SetActive(value: true);
			ui.LevelSelection_Scroll_Keyboard.VRHints_VRBackButton.gameObject.SetActive(value: true);
			ui.LevelSelection_Scroll_Keyboard.VRHints.gameObject.SetActive(value: true);
			Color caretColor = ui.LevelSelection_Search_SearchBg_SearchInputField.caretColor;
			caretColor.a = 0f;
			ui.LevelSelection_Search_SearchBg_SearchInputField.caretColor = caretColor;
			Color selectionColor = ui.LevelSelection_Search_SearchBg_SearchInputField.selectionColor;
			selectionColor.a = 0f;
			ui.LevelSelection_Search_SearchBg_SearchInputField.selectionColor = selectionColor;
		}
	}
}
