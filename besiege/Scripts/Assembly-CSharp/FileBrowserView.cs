using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BesiegeDlc;
using Localisation;
using Selectors;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FileBrowserView : MonoBehaviour
{
	public class LastEntry
	{
		public string saved;

		public string loaded;
	}

	private enum SortType
	{
		Name = 0,
		Date = 1
	}

	private enum SortMode
	{
		Ascending = 0,
		Descending = 1
	}

	private const int MarginBetweenPages = 20;

	public Action<bool> ViewToggled;

	public List<FileBrowserTypeTemplate> PageTemplateObjects = new List<FileBrowserTypeTemplate>();

	public static Dictionary<string, IVirtualObject> FieldValues = new Dictionary<string, IVirtualObject>();

	public static Dictionary<FileBrowserType, LastEntry> LastEntries = new Dictionary<FileBrowserType, LastEntry>();

	protected VirtualFolder currentFolder;

	[SerializeField]
	[Header("Tabs")]
	protected UITabToggler toggler;

	[SerializeField]
	protected Text localTab;

	[SerializeField]
	protected Text workshopTab;

	[SerializeField]
	protected BrowseWorkshopButton browseSteamWorkshopButton;

	[FormerlySerializedAs("loadSaveTextMesh")]
	[Header("Header")]
	[SerializeField]
	protected TextMesh interactionHeader;

	[SerializeField]
	[FormerlySerializedAs("objectTypeTextMesh")]
	protected TextMesh objectTypeHeader;

	[SerializeField]
	[Header("Item Name Field")]
	protected TextHolder objectPathTextField;

	[SerializeField]
	protected TextMesh objectPathFileExtension;

	[SerializeField]
	protected LoadSaveButton loadSaveButton;

	[SerializeField]
	protected LoadSaveButton loadSaveAdditiveButton;

	[SerializeField]
	protected OverwriteButton overwriteButton;

	[SerializeField]
	protected OverwriteButton additiveOverwriteButton;

	[Header("Folder Path")]
	[SerializeField]
	protected TextMesh folderPathText;

	[SerializeField]
	protected SimpleUIButton folderPathObjectButton;

	[Header("Header Buttons")]
	[SerializeField]
	protected SimpleUIButton createFolderButton;

	[SerializeField]
	protected SimpleUIButton folderBackButton;

	[SerializeField]
	protected SimpleUIButton closeButton;

	[SerializeField]
	[Header("Pages")]
	protected FileBrowserPagination pagination;

	[SerializeField]
	protected Transform pageParentTransform;

	[SerializeField]
	protected FileBrowserPageView defaultPageTemplateObject;

	[Header("Sorting")]
	[SerializeField]
	private SimpleUIButton sortByNameButton;

	[SerializeField]
	private SimpleUIButton sortByDateButton;

	[SerializeField]
	private SimpleUIButton sortAscendingButton;

	[SerializeField]
	private SimpleUIButton sortDescendingButton;

	[SerializeField]
	[Header("Pop-ups")]
	protected CreateFolderMenu createFolderMenu;

	[SerializeField]
	protected DlcsNotInstalledPopup dlcsNotInstalledPopup;

	[Header("Misc")]
	[SerializeField]
	protected SimpleUIButton backgroundColliderButton;

	[SerializeField]
	protected SimpleUIButton refreshButton;

	[SerializeField]
	protected ThumbnailCreator thumbnailCreator;

	[SerializeField]
	private int maxFolderPathCharacters = 95;

	[Space(10f)]
	private SortType sortType;

	private SortMode sortMode;

	private bool isOpen;

	private bool isSaveMenu;

	private int currentPageIndex;

	private WorkshopType workshopType;

	private FileBrowserType currentBrowserType;

	private FileBrowserType lastBrowserType;

	private IVirtualObject selectedObject;

	private AbstractObjectCollection objectCollection;

	private FileBrowserPageView currentPageView;

	private FileBrowserPageView pageTemplateObject;

	private UploadData cachedUploadData;

	private IWorkshopItem cachedWorkshopItem;

	private IEnumerator generatePagesCoroutine;

	private List<FileBrowserPageView> pages = new List<FileBrowserPageView>();

	private VirtualFolder previousFolder;

	private VirtualFolder restoreFolder;

	private IVirtualObject restoreObject;

	public static bool saveMenuUpload = false;

	public FileBrowserController Controller { get; private set; }

	public IFileBrowserViewExtension Extension { get; private set; }

	public bool IsSaveMenu
	{
		get
		{
			return isSaveMenu;
		}
	}

	public bool IsOpen
	{
		get
		{
			return isOpen;
		}
	}

	public UploadData CachedUploadData
	{
		get
		{
			return cachedUploadData;
		}
	}

	public void Generate(VirtualFolder folder)
	{
		previousFolder = currentFolder;
		Clear();
		ToggleOverwriteButton(false);
		SetFolderName(folder);
		ToggleFolderText();
		ToggleRefreshButton();
		ToggleCreateFolderButton();
		GeneratePages(folder);
		SetBackButton(folder);
		if (folder != null)
		{
			string key = folder.ObjectPath.ToString();
			if (FieldValues.ContainsKey(key))
			{
				SelectObject(FieldValues[key]);
			}
		}
	}

	private void ToggleCreateFolderButton()
	{
		FileBrowserType fileBrowserType = currentBrowserType;
		bool active = ((fileBrowserType == FileBrowserType.LocalMachines || fileBrowserType == FileBrowserType.LocalLevels) ? true : false);
		createFolderButton.gameObject.SetActive(active);
	}

	private void ToggleRefreshButton()
	{
		bool active;
		switch (currentBrowserType)
		{
		case FileBrowserType.SteamMachines:
		case FileBrowserType.SteamLevels:
		case FileBrowserType.WeGameMachines:
		case FileBrowserType.WeGameLevels:
		case FileBrowserType.ModIOMachines:
		case FileBrowserType.ModIOLevels:
			active = true;
			break;
		default:
			active = false;
			break;
		}
		refreshButton.gameObject.SetActive(active);
	}

	public void Open(FileBrowserType browserType, bool isSaveMenu, bool restore = false)
	{
		pageTemplateObject = (from x in PageTemplateObjects
			where x.Type == browserType
			select x.Template).FirstOrDefault();
		if (pageTemplateObject == null)
		{
			pageTemplateObject = defaultPageTemplateObject;
		}
		if (isOpen && !StatMaster.Mode.LevelEditor.isSelectingLevel)
		{
			ReferenceMaster.ResetLevelEditor();
			PushSubs();
		}
		else
		{
			BlockMapper.Close();
			OverviewBlockMapper.Close();
		}
		base.gameObject.SetActive(true);
		this.isSaveMenu = isSaveMenu;
		currentPageIndex = 0;
		isOpen = true;
		currentBrowserType = browserType;
		switch (browserType)
		{
		case FileBrowserType.LocalMachines:
		case FileBrowserType.LocalLevels:
		case FileBrowserType.Skins:
			toggler.SetTab(0);
			break;
		default:
			toggler.SetTab(1);
			break;
		}
		workshopType = WorkshopManager.DetermineWorkshopType();
		CleanView();
		SetupController(restore);
		UpdatePageViewMode();
		UpdateAlignmentComponents();
		if (ViewToggled != null)
		{
			ViewToggled(true);
		}
	}

	public static void SetLastLoadEntry(FileBrowserType browserType, string fileName)
	{
		LastEntry value;
		if (!LastEntries.TryGetValue(browserType, out value))
		{
			value.loaded = fileName;
		}
	}

	public static void SetLastSaveEntry(FileBrowserType browserType, string fileName)
	{
		LastEntry value;
		if (LastEntries.TryGetValue(browserType, out value))
		{
			value.saved = fileName;
		}
	}

	public static void AddLastEntry(FileBrowserType browserType, string fileName)
	{
		LastEntry value;
		if (!LastEntries.TryGetValue(browserType, out value))
		{
			LastEntries.Add(browserType, new LastEntry
			{
				loaded = fileName,
				saved = fileName
			});
		}
		else
		{
			value.loaded = (value.saved = fileName);
		}
	}

	public void SetUploadData(UploadData data)
	{
		cachedUploadData = data;
	}

	private void UpdateAlignmentComponents()
	{
		AlignGUIObject[] componentsInChildren = GetComponentsInChildren<AlignGUIObject>();
		foreach (AlignGUIObject alignGUIObject in componentsInChildren)
		{
			alignGUIObject.RealignObject();
		}
		MatchColliderUIParent[] componentsInChildren2 = GetComponentsInChildren<MatchColliderUIParent>();
		foreach (MatchColliderUIParent matchColliderUIParent in componentsInChildren2)
		{
			matchColliderUIParent.OnEnable();
		}
	}

	private void ToggleFileField(bool enable)
	{
		objectPathTextField.transform.parent.gameObject.SetActive(enable);
	}

	private void OnEnable()
	{
		StatMaster.SetInMenu(true);
	}

	private void OnDisable()
	{
		if (isOpen)
		{
			StatMaster.SetInMenu(false);
		}
	}

	public void Close()
	{
		Close(true);
	}

	public void SetRestoreFolder(bool isSave = false)
	{
		restoreFolder = currentFolder;
		if (isSave)
		{
			IVirtualObject virtualObject = currentFolder.GetObjects().FirstOrDefault((IVirtualObject x) => !x.IsFolder && x.Name == objectPathTextField.ValueText);
			if (virtualObject != null)
			{
				restoreObject = virtualObject;
			}
		}
		else
		{
			restoreObject = selectedObject;
		}
		lastBrowserType = currentBrowserType;
	}

	public void Close(bool clearCachedUploadData)
	{
		base.gameObject.SetActive(false);
		isOpen = false;
		if (clearCachedUploadData)
		{
			cachedWorkshopItem = null;
			cachedUploadData = null;
		}
		CleanView();
		if (!(Controller is LevelFileBrowserController))
		{
			ReferenceMaster.ResetLevelEditor();
		}
		if (Controller != null)
		{
			Controller.Dispose();
			Controller = null;
		}
		if (ViewToggled != null)
		{
			ViewToggled(false);
		}
		PushSubs();
		saveMenuUpload = false;
	}

	private void PushSubs()
	{
	}

	public void CreateThumbnail(string path, bool useMainCamera, bool isMachineSelection = false)
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		if (StatMaster.clusterCoded)
		{
			BlockSkinLoader.ColourCodeClusters(false);
			flag = true;
		}
		else if (StatMaster.aeroCoded)
		{
			BlockSkinLoader.SetAero(false);
			flag2 = true;
		}
		else if (StatMaster.stressCoded)
		{
			BlockSkinLoader.SetStress(false);
			flag3 = true;
		}
		if (isMachineSelection)
		{
			thumbnailCreator.CaptureMachineSelectionImage(path);
		}
		else
		{
			thumbnailCreator.CaptureImage(path, useMainCamera);
		}
		if (flag)
		{
			BlockSkinLoader.ColourCodeClusters(true);
		}
		else if (flag2)
		{
			BlockSkinLoader.SetAero(true);
		}
		else if (flag3)
		{
			BlockSkinLoader.SetStress(true);
		}
	}

	private void CleanView()
	{
		StopPageGeneration();
		pagination.DisablePageButtons();
		objectPathTextField.ValueText = string.Empty;
		DeselectObject();
		ClearPages();
	}

	private void SetBackButton(VirtualFolder folder)
	{
		bool active = folder.Parent != null;
		folderBackButton.gameObject.SetActive(active);
	}

	private void ClearPages()
	{
		foreach (FileBrowserPageView page in pages)
		{
			UnityEngine.Object.Destroy(page.gameObject);
		}
		pages.Clear();
	}

	private void StopPageGeneration()
	{
		if (generatePagesCoroutine != null)
		{
			StopCoroutine(generatePagesCoroutine);
		}
		generatePagesCoroutine = null;
	}

	private void GeneratePages(VirtualFolder folder)
	{
		currentFolder = folder;
		StopPageGeneration();
		generatePagesCoroutine = GeneratePagesIE(folder);
		StartCoroutine(generatePagesCoroutine);
	}

	private IEnumerator GeneratePagesIE(VirtualFolder folder)
	{
		yield return new WaitForSecondsRealtime(0.1f);
		List<IVirtualObject> folderObjects = objectCollection.GetFilteredObjectsFrom(folder.GetObjects()).ToList();
		if (currentBrowserType == FileBrowserType.Skins)
		{
			switch (sortType)
			{
			case SortType.Name:
				folderObjects.Sort((IVirtualObject x, IVirtualObject y) => (x.IsUploadable != y.IsUploadable) ? ((!x.IsUploadable) ? 1 : (-1)) : ((sortMode != SortMode.Ascending) ? y.Name.CompareTo(x.Name) : x.Name.CompareTo(y.Name)));
				break;
			case SortType.Date:
				folderObjects.Sort((IVirtualObject x, IVirtualObject y) => (x.IsUploadable != y.IsUploadable) ? ((!x.IsUploadable) ? 1 : (-1)) : ((sortMode != SortMode.Descending) ? y.Date.CompareTo(x.Date) : x.Date.CompareTo(y.Date)));
				break;
			}
			folderObjects = (from x in folderObjects
				orderby (x as LocalSkinFile).SkinPack.type == PackType.Official descending
				orderby x.Name == "DEFAULT" descending
				select x).ToList();
		}
		else
		{
			switch (sortType)
			{
			case SortType.Name:
				folderObjects.Sort((IVirtualObject x, IVirtualObject y) => (x.IsFolder != y.IsFolder) ? ((!x.IsFolder) ? 1 : (-1)) : ((sortMode != SortMode.Ascending) ? y.Name.CompareTo(x.Name) : x.Name.CompareTo(y.Name)));
				break;
			case SortType.Date:
				folderObjects.Sort((IVirtualObject x, IVirtualObject y) => (x.IsFolder != y.IsFolder) ? ((!x.IsFolder) ? 1 : (-1)) : ((sortMode != SortMode.Descending) ? y.Date.CompareTo(x.Date) : x.Date.CompareTo(y.Date)));
				break;
			}
		}
		List<List<IVirtualObject>> pagesCollections = folderObjects.Split(pageTemplateObject.FilesPerPage);
		if (folder != previousFolder)
		{
			currentPageIndex = 0;
			pagination.Generate(0, 0);
		}
		int previousIndex = currentPageIndex;
		int startPageIndex = GetLastSelectedPageIndex(previousIndex, pagesCollections.Count);
		if (pagesCollections.Count != 0)
		{
			GeneratePartialPage(pagesCollections[startPageIndex], false);
			SelectPage(0, 0);
			currentPageIndex = startPageIndex;
			for (int i = startPageIndex - 1; i > -1; i--)
			{
				GeneratePartialPage(pagesCollections[i], true);
				yield return null;
			}
			for (int i2 = startPageIndex + 1; i2 < pagesCollections.Count; i2++)
			{
				GeneratePartialPage(pagesCollections[i2], false);
				yield return null;
			}
		}
	}

	private void GeneratePartialPage(List<IVirtualObject> pageCollection, bool insertLeft)
	{
		FileBrowserPageView fileBrowserPageView = GeneratePage(pageCollection);
		if (insertLeft)
		{
			pages.Insert(0, fileBrowserPageView);
		}
		else
		{
			pages.Add(fileBrowserPageView);
		}
		Extension.OnPageViewCreated(fileBrowserPageView);
		GeneratePagination();
	}

	private int GetLastSelectedPageIndex(int lastSelectedIndex, int pageCount)
	{
		int num = lastSelectedIndex;
		while (num > pageCount - 1 && num > 0)
		{
			num--;
		}
		return num;
	}

	private void GeneratePagination()
	{
		int lastSelectedPageIndex = GetLastSelectedPageIndex(currentPageIndex, pages.Count);
		pagination.Generate(pages.Count, lastSelectedPageIndex);
	}

	private void Clear()
	{
		foreach (FileBrowserPageView page in pages)
		{
			UnityEngine.Object.Destroy(page.gameObject);
		}
		pages.Clear();
	}

	private void ToggleFolderText()
	{
		bool active = true;
		GameObject gameObject = localTab.transform.parent.parent.gameObject;
		GameObject gameObject2 = workshopTab.transform.parent.parent.gameObject;
		bool active2 = WorkshopManager.IsInitialized() && !saveMenuUpload;
		switch (currentBrowserType)
		{
		case FileBrowserType.LocalMachines:
		case FileBrowserType.LocalLevels:
			gameObject.SetActive(true);
			gameObject2.SetActive(active2);
			break;
		case FileBrowserType.Skins:
			gameObject.SetActive(true);
			gameObject2.SetActive(false);
			break;
		case FileBrowserType.PublishedSteamMods:
		case FileBrowserType.PublishedWeGameMods:
		case FileBrowserType.PublishedModIOMods:
			gameObject.SetActive(false);
			gameObject2.SetActive(true);
			active = false;
			break;
		default:
			gameObject.SetActive(true);
			gameObject2.SetActive(active2);
			active = false;
			break;
		}
		folderPathText.gameObject.SetActive(active);
	}

	private void SetFolderName(VirtualFolder folder)
	{
		switch (currentBrowserType)
		{
		case FileBrowserType.LocalMachines:
		case FileBrowserType.LocalLevels:
		case FileBrowserType.Skins:
		{
			currentFolder = folder;
			string text = folder.ObjectPath.ToString();
			string value = "Besiege";
			if (text.Contains("AppData"))
			{
				text = text.Substring(text.IndexOf("AppData")).Replace("AppData", "%AppData%");
			}
			else if (text.Contains(value))
			{
				string text2 = Path.GetPathRoot(text).Replace('\\', '/') + "•••/";
				text = text.Substring(text.IndexOf(value));
				text = text2 + text.Replace('\\', '/');
			}
			folderPathText.text = text.TruncateFolderPath(maxFolderPathCharacters);
			SetTabNames(text);
			break;
		}
		}
	}

	private void SetTabNames(string folderPath)
	{
		if (isSaveMenu)
		{
			workshopTab.text = LocalisationManager.GetTranslation(4238);
		}
		else
		{
			workshopTab.text = LocalisationManager.GetTranslation(4239);
		}
		if (saveMenuUpload)
		{
			FileBrowserType fileBrowserType = currentBrowserType;
			if (fileBrowserType == FileBrowserType.LocalMachines || fileBrowserType == FileBrowserType.LocalLevels)
			{
				localTab.text = LocalisationManager.GetTranslation(949) + "...";
				return;
			}
		}
		if (folderPath.EndsWith("CustomLevels/"))
		{
			localTab.text = LocalisationManager.GetTranslation(4116);
		}
		else if (folderPath.EndsWith("SavedMachines/"))
		{
			localTab.text = LocalisationManager.GetTranslation(4116);
		}
		else if (currentBrowserType == FileBrowserType.Skins)
		{
			localTab.text = LocalisationManager.GetTranslation(519);
		}
		else
		{
			localTab.text = ReferenceMaster.CamelCaseToSpaces(new DirectoryInfo(folderPath).Name).ToUpper();
		}
	}

	private FileBrowserPageView GeneratePage(IEnumerable<IVirtualObject> virtualObjects)
	{
		FileBrowserPageView fileBrowserPageView = UnityEngine.Object.Instantiate(pageTemplateObject);
		fileBrowserPageView.transform.parent = pageParentTransform;
		fileBrowserPageView.transform.localScale = Vector3.one;
		fileBrowserPageView.transform.localPosition = Vector3.zero;
		fileBrowserPageView.gameObject.SetActive(false);
		fileBrowserPageView.Initialize(this, virtualObjects, workshopType);
		fileBrowserPageView.SlotClicked = OnPageViewSlotClicked;
		fileBrowserPageView.SlotDoubleClicked = OnPageViewSlotDoubleClicked;
		fileBrowserPageView.SlotDeleteConfirmed = OnPageViewSlotDeleteConfirmed;
		fileBrowserPageView.SlotToggleRemoteClicked = OnPageViewSlotToggleRemoteClicked;
		fileBrowserPageView.SlotUploadClicked = OnPageViewSlotUpload;
		fileBrowserPageView.SlotDownloadClicked = OnPageViewSlotDownload;
		fileBrowserPageView.SlotVersionsClicked = OnPageViewSlotVersions;
		fileBrowserPageView.SlotLoadAsSelectionClicked = OnPageViewSlotLoadAsSelectionClicked;
		return fileBrowserPageView;
	}

	private void OnPageViewSlotDownload(IVirtualObject virtualObject)
	{
		Controller.DownloadObject(virtualObject);
	}

	public void OnPageViewSlotUpload(IVirtualObject virtualObject)
	{
		Controller.UploadObject(virtualObject);
	}

	private void OnPageViewSlotDeleteConfirmed(IVirtualObject virtualObject)
	{
		Controller.DeleteObject(virtualObject);
	}

	private void OnPageViewSlotToggleRemoteClicked(IVirtualObject virtualObject)
	{
		Controller.ToggleRemote(virtualObject);
	}

	private void OnPageViewSlotDoubleClicked(IVirtualObject virtualObject)
	{
		Controller.OpenObject(virtualObject, false, FileBrowserController.OpenMode.Normal);
	}

	private void OnPageViewSlotClicked(IVirtualObject virtualObject)
	{
		SelectObject(virtualObject);
	}

	private void OnPageViewSlotVersions(IVirtualObject virtualObject)
	{
		VirtualFolder parent = virtualObject.Parent;
		while (parent.Parent != null)
		{
			parent = parent.Parent;
		}
		IVirtualObject virtualObject2 = parent.GetObjects().FirstOrDefault((IVirtualObject x) => x.IsFolder && x.Name == "AutoSave");
		if (virtualObject2 != null)
		{
			virtualObject2.Open();
			IVirtualObject virtualObject3 = (virtualObject2 as VirtualFolder).GetObjects().FirstOrDefault((IVirtualObject x) => x.IsFolder && x.Name == virtualObject.Name);
			if (virtualObject3 != null)
			{
				Controller.OpenObject(virtualObject3, false, FileBrowserController.OpenMode.Normal);
			}
		}
	}

	private void OnPageViewSlotLoadAsSelectionClicked(IVirtualObject virtualObject)
	{
		Controller.OpenObject(virtualObject, false, FileBrowserController.OpenMode.AdditiveOrSelectionOnly);
	}

	public void SelectObject(IVirtualObject virtualObject)
	{
		selectedObject = virtualObject;
		ToggleOverwriteButton(false);
		if (!objectCollection.HideFileField)
		{
			string entityName = selectedObject.ObjectPath.EntityName;
			if (virtualObject.IsFolder)
			{
				objectPathTextField.ValueText = string.Empty;
				return;
			}
			objectPathTextField.ValueText = entityName.Substring(0, entityName.Length - objectCollection.FilterExtension.Length);
			UpdateFileExtensionPosition();
			AddFieldValue();
			objectPathTextField.IsFocused = false;
		}
	}

	private void DeselectObject()
	{
		selectedObject = null;
		ToggleOverwriteButton(false);
	}

	private void Awake()
	{
		Initialize();
	}

	private void Update()
	{
		if (InputManager.CloseKey())
		{
			Close();
		}
	}

	private void UpdatePageViewMode()
	{
		switch (currentBrowserType)
		{
		case FileBrowserType.Skins:
			interactionHeader.text = LocalisationManager.GetTranslation(956);
			break;
		case FileBrowserType.PublishedSteamMachines:
		case FileBrowserType.PublishedSteamLevels:
		case FileBrowserType.PublishedWeGameMachines:
		case FileBrowserType.PublishedWeGameLevels:
		case FileBrowserType.PublishedSteamMods:
		case FileBrowserType.PublishedWeGameMods:
		case FileBrowserType.PublishedModIOMods:
		case FileBrowserType.PublishedModIOMachines:
		case FileBrowserType.PublishedModIOLevels:
			interactionHeader.text = LocalisationManager.GetTranslation(949);
			break;
		default:
			if (saveMenuUpload)
			{
				interactionHeader.text = LocalisationManager.GetTranslation(2961);
			}
			else if (isSaveMenu)
			{
				interactionHeader.text = LocalisationManager.GetTranslation(934);
			}
			else
			{
				interactionHeader.text = LocalisationManager.GetTranslation(928);
			}
			break;
		}
		loadSaveButton.SetIsSaveMode(isSaveMenu);
		loadSaveAdditiveButton.loadTooltipLocalisationId = 5019;
		loadSaveAdditiveButton.saveTooltipLocalisationId = 5020;
		loadSaveAdditiveButton.SetIsSaveMode(isSaveMenu);
		loadSaveAdditiveButton.transform.parent.gameObject.SetActive(Controller.ShowAdditiveOrSelectionOnlyButton(isSaveMenu));
		objectPathFileExtension.text = objectCollection.FilterExtension;
	}

	private void OpenWorkshopItems()
	{
		FileBrowserType fileBrowserType = currentBrowserType;
		if (fileBrowserType == FileBrowserType.LocalMachines || fileBrowserType == FileBrowserType.LocalLevels)
		{
			SetRestoreFolder();
			OnOpenWorkshopButtonClicked(WorkshopType.Steam);
		}
	}

	private void Initialize()
	{
		toggler.tabs[0].button.Click += delegate
		{
			switch (currentBrowserType)
			{
			case FileBrowserType.LocalMachines:
			case FileBrowserType.LocalLevels:
			case FileBrowserType.Skins:
				break;
			default:
				Open(lastBrowserType, isSaveMenu, true);
				break;
			}
		};
		toggler.tabs[1].button.Click += delegate
		{
			OpenWorkshopItems();
		};
		pagination.Initialize();
		pagination.PageChanged = OnPaginationPageChanged;
		pagination.DisablePageButtons();
		loadSaveButton.Click += delegate
		{
			LoadSaveButtonClick(FileBrowserController.OpenMode.Normal);
		};
		loadSaveAdditiveButton.Click += delegate
		{
			LoadSaveButtonClick(FileBrowserController.OpenMode.AdditiveOrSelectionOnly);
		};
		overwriteButton.Click += delegate
		{
			LoadSaveButtonClick(FileBrowserController.OpenMode.Normal);
		};
		additiveOverwriteButton.Click += delegate
		{
			LoadSaveButtonClick(FileBrowserController.OpenMode.AdditiveOrSelectionOnly);
		};
		objectPathTextField.TextChanged += delegate
		{
			OnObjectPathChanged();
		};
		objectPathTextField.TextChangedExternal += delegate
		{
			UpdateFileExtensionPosition();
		};
		UpdateFileExtensionPosition();
		folderPathObjectButton.Click += FolderPathObjectButtonClick;
		folderBackButton.Click += FolderBackButtonClick;
		createFolderButton.Click += CreateFolderButtonClick;
		CreateFolderMenu obj = createFolderMenu;
		obj.CreateFolderConfirmed = (Action<string>)Delegate.Combine(obj.CreateFolderConfirmed, new Action<string>(OnCreateFolderConfirmed));
		createFolderMenu.gameObject.SetActive(false);
		if ((bool)backgroundColliderButton)
		{
			backgroundColliderButton.Click += BackgroundColliderButtonClick;
		}
		refreshButton.Click += RefreshButtonClick;
		closeButton.Click += CloseButtonClick;
		sortByNameButton.Click += delegate
		{
			if (sortType != SortType.Name)
			{
				SetSortType(SortType.Name);
				Clear();
				GeneratePages(currentFolder);
			}
		};
		sortByDateButton.Click += delegate
		{
			if (sortType != SortType.Date)
			{
				SetSortType(SortType.Date);
				Clear();
				GeneratePages(currentFolder);
			}
		};
		sortAscendingButton.Click += delegate
		{
			if (sortMode != SortMode.Ascending)
			{
				SetSortMode(SortMode.Ascending);
				Clear();
				GeneratePages(currentFolder);
			}
		};
		sortDescendingButton.Click += delegate
		{
			if (sortMode != SortMode.Descending)
			{
				SetSortMode(SortMode.Descending);
				Clear();
				GeneratePages(currentFolder);
			}
		};
		SetSortType(sortType);
		SetSortMode(sortMode);
	}

	private void SetSortType(SortType newType)
	{
		sortType = newType;
		float num = 0.15f;
		float num2 = 0.9f;
		sortByNameButton.GetComponentInChildren<Renderer>().material.color = new Color(1f, 1f, 1f, (sortType != SortType.Name) ? num : num2);
		sortByDateButton.GetComponentInChildren<Renderer>().material.SetColor("_TintColor", new Color(1f, 1f, 1f, (sortType != SortType.Date) ? num : num2));
	}

	private void SetSortMode(SortMode newMode)
	{
		sortMode = newMode;
		float num = 0.15f;
		float num2 = 0.9f;
		sortAscendingButton.GetComponentInChildren<Renderer>().material.SetColor("_TintColor", new Color(1f, 1f, 1f, (sortMode != SortMode.Ascending) ? num : num2));
		sortDescendingButton.GetComponentInChildren<Renderer>().material.SetColor("_TintColor", new Color(1f, 1f, 1f, (sortMode != SortMode.Descending) ? num : num2));
	}

	private void CloseButtonClick()
	{
		Close();
	}

	private void RefreshButtonClick()
	{
		Controller.RefreshView();
	}

	private void ToggleBrowseWorkshopButton()
	{
		browseSteamWorkshopButton.gameObject.SetActive(false);
		if (workshopType == WorkshopType.Steam || workshopType == WorkshopType.WeGame || workshopType == WorkshopType.ModIO)
		{
			browseSteamWorkshopButton.Initialize(currentBrowserType);
			browseSteamWorkshopButton.gameObject.SetActive(true);
		}
	}

	private void OnOpenWorkshopButtonClicked(WorkshopType workshopType)
	{
		Controller.OpenWorkshop(workshopType);
	}

	private void BackgroundColliderButtonClick()
	{
		Close();
	}

	private void FolderPathObjectButtonClick()
	{
		Controller.OpenFolderInExplorer();
	}

	private void OnObjectPathChanged()
	{
		ToggleOverwriteButton(false);
		UpdateFileExtensionPosition();
	}

	private void UpdateFileExtensionPosition()
	{
		objectPathFileExtension.transform.localPosition = new Vector3(objectPathTextField.text.transform.localPosition.x + objectPathTextField.text.bounds.max.x, -0.026f);
	}

	public void ToggleOverwriteButton(bool enable, bool additive = false)
	{
		if (enable && !additive)
		{
			overwriteButton.Show();
			additiveOverwriteButton.Hide();
		}
		else if (enable && additive)
		{
			overwriteButton.Hide();
			additiveOverwriteButton.Show();
		}
		else
		{
			overwriteButton.Hide();
			additiveOverwriteButton.Hide();
		}
	}

	private void SetupController(bool restore = false)
	{
		objectCollection = GetCollection(currentBrowserType);
		VirtualFolder virtualFolder = restoreFolder;
		if (Controller != null)
		{
			Controller.Dispose();
		}
		Controller = GetController(currentBrowserType);
		Extension = GetViewExtension(Controller);
		restore = restore && virtualFolder != null && lastBrowserType == currentBrowserType;
		Controller.Initialize(objectCollection, currentBrowserType, !restore);
		if (restore)
		{
			StopAllCoroutines();
			objectCollection.ChangeFolder(virtualFolder);
			if (restoreObject != null)
			{
				SelectObject(restoreObject);
			}
		}
	}

	private FileBrowserController GetController(FileBrowserType browserType)
	{
		ToggleBrowseWorkshopButton();
		switch (browserType)
		{
		case FileBrowserType.Skins:
			return new SkinFileBrowserController(this);
		case FileBrowserType.LocalMachines:
		case FileBrowserType.SteamMachines:
			return new MachineFileBrowserController(this);
		case FileBrowserType.LocalLevels:
		case FileBrowserType.SteamLevels:
			return new LevelFileBrowserController(this);
		case FileBrowserType.PublishedSteamMachines:
		case FileBrowserType.PublishedSteamLevels:
		case FileBrowserType.PublishedSteamSkins:
		case FileBrowserType.PublishedSteamMods:
		{
			PublishedSteamFileController publishedSteamFileController = new PublishedSteamFileController(this);
			publishedSteamFileController.SetCachedUploadData(cachedUploadData);
			return publishedSteamFileController;
		}
		default:
			return new MachineFileBrowserController(this);
		}
	}

	private IFileBrowserViewExtension GetViewExtension(FileBrowserController controller)
	{
		IFileBrowserViewExtension fileBrowserViewExtension;
		if (currentBrowserType == FileBrowserType.Skins)
		{
			fileBrowserViewExtension = new SkinViewExtension();
			fileBrowserViewExtension.Initialize(this, controller);
		}
		else
		{
			fileBrowserViewExtension = new NullViewExtension();
		}
		if (currentBrowserType == FileBrowserType.SteamMachines || currentBrowserType == FileBrowserType.SteamLevels || currentBrowserType == FileBrowserType.WeGameMachines || currentBrowserType == FileBrowserType.WeGameLevels || currentBrowserType == FileBrowserType.ModIOMachines || currentBrowserType == FileBrowserType.ModIOLevels)
		{
			ToggleFileField(false);
		}
		else
		{
			ToggleFileField(!objectCollection.HideFileField);
		}
		return fileBrowserViewExtension;
	}

	private AbstractObjectCollection GetCollection(FileBrowserType browserType)
	{
		AbstractObjectCollection abstractObjectCollection;
		switch (browserType)
		{
		case FileBrowserType.LocalMachines:
			abstractObjectCollection = new LocalMachineCollection();
			break;
		case FileBrowserType.LocalLevels:
			abstractObjectCollection = new LocalLevelCollection();
			break;
		case FileBrowserType.Skins:
			abstractObjectCollection = new SkinCollection();
			break;
		case FileBrowserType.SteamMachines:
			abstractObjectCollection = new SteamMachineCollection();
			break;
		case FileBrowserType.SteamLevels:
			abstractObjectCollection = new SteamLevelCollection();
			break;
		case FileBrowserType.PublishedSteamMachines:
			abstractObjectCollection = new PublishedSteamMachines();
			break;
		case FileBrowserType.PublishedSteamLevels:
			abstractObjectCollection = new PublishedSteamLevels();
			break;
		case FileBrowserType.PublishedSteamSkins:
			abstractObjectCollection = new PublishedSteamSkins();
			break;
		case FileBrowserType.PublishedSteamMods:
			abstractObjectCollection = new PublishedSteamMods();
			break;
		default:
			abstractObjectCollection = new LocalMachineCollection();
			break;
		}
		objectTypeHeader.text = abstractObjectCollection.ObjectName;
		return abstractObjectCollection;
	}

	public void HandlerFolderCreationResult(CreateFolderResult result)
	{
		createFolderMenu.HandleCreateFolderResult(result);
	}

	private void OnCreateFolderConfirmed(string folderName)
	{
		Controller.CreateFolder(folderName);
	}

	private void CreateFolderButtonClick()
	{
		createFolderMenu.gameObject.SetActive(true);
	}

	private void LoadSaveButtonClick(FileBrowserController.OpenMode mode)
	{
		bool flag = ((mode != FileBrowserController.OpenMode.Normal) ? additiveOverwriteButton.IsShown : overwriteButton.IsShown);
		if (selectedObject == null || selectedObject.Name != objectPathTextField.ValueText)
		{
			Controller.FindAndOpenObject(objectPathTextField.ValueText, flag, mode);
		}
		else
		{
			Controller.OpenObject(selectedObject, flag, mode);
		}
		if (flag)
		{
			overwriteButton.Hide();
			additiveOverwriteButton.Hide();
		}
	}

	protected void AddFieldValue()
	{
		string text = currentFolder.ObjectPath.ToString();
		if (text != string.Empty && selectedObject != null)
		{
			if (FieldValues.ContainsKey(text))
			{
				FieldValues[text] = selectedObject;
			}
			else
			{
				FieldValues.Add(text, selectedObject);
			}
		}
	}

	private void FolderBackButtonClick()
	{
		DeselectObject();
		Controller.OpenParentFolder();
	}

	private void OnPaginationPageChanged(int previousPageIndex, int pageIndex)
	{
		SelectPage(previousPageIndex, pageIndex);
	}

	private void SelectPage(int previousPageIndex, int pageIndex)
	{
		if (pageIndex <= pages.Count - 1)
		{
			PageMoveDirection direction = PageMoveDirection.None;
			if (pageIndex > previousPageIndex)
			{
				direction = PageMoveDirection.Left;
			}
			else if (pageIndex < previousPageIndex)
			{
				direction = PageMoveDirection.Right;
			}
			if (currentPageView != null)
			{
				currentPageView.Close(direction);
			}
			currentPageIndex = pageIndex;
			currentPageView = pages[pageIndex];
			currentPageView.Open(direction);
		}
	}

	public void OpenUploadDialog(WorkshopManager.ItemTypes uploadType, bool isFolder, string filePath, string fileName, string thumbnailPath)
	{
		if (!isFolder)
		{
			UploadFile(uploadType, fileName, filePath, thumbnailPath);
		}
		else
		{
			UploadFolder(fileName, filePath, thumbnailPath);
		}
	}

	private UploadData CreateUploadData(string fileName, string filePath, string thumbnailPath, bool isFolder, bool skipDLCCheck = false)
	{
		WorkshopManager.ItemTypes itemType = WorkshopManager.ItemTypes.Machines;
		uint dlcDependencyMask = 0u;
		Debug.Log("[FileBrowserView] Creating upload data for " + filePath + " skipDLC=" + skipDLCCheck + "..");
		if (!skipDLCCheck)
		{
			switch (ReferenceMaster.UIActive)
			{
			case ReferenceMaster.WorkshopItemType.Machine:
				itemType = WorkshopManager.ItemTypes.Machines;
				dlcDependencyMask = BesiegeContentHelper.GetDlcDependencyMaskFromPath(filePath, itemType, isFolder);
				break;
			case ReferenceMaster.WorkshopItemType.Levels:
				itemType = WorkshopManager.ItemTypes.Levels;
				dlcDependencyMask = BesiegeContentHelper.GetDlcDependencyMaskFromPath(filePath, itemType, isFolder);
				break;
			case ReferenceMaster.WorkshopItemType.Mods:
				itemType = WorkshopManager.ItemTypes.Mods;
				dlcDependencyMask = BesiegeContentHelper.GetDlcDependencyMaskFromPath(filePath, itemType, isFolder);
				break;
			}
		}
		UploadData uploadData = new UploadData();
		uploadData.Name = fileName;
		uploadData.Path = filePath;
		uploadData.ThumbnailPath = thumbnailPath;
		uploadData.ItemType = itemType;
		uploadData.IsFolder = isFolder;
		uploadData.DlcDependencyMask = dlcDependencyMask;
		return uploadData;
	}

	public void OpenUploadUpdateDialog(WorkshopManager.ItemTypes uploadType, string fileName, string filePath, Texture2D tex, List<string> existingTags)
	{
		bool flag = uploadType == WorkshopManager.ItemTypes.Levels;
		ReferenceMaster.UIActive = (flag ? ReferenceMaster.WorkshopItemType.Levels : ReferenceMaster.WorkshopItemType.Machine);
		string uploadPrefabPath = GetUploadPrefabPath(flag);
		saveMenuUpload = true;
		UploadDialog uploadDialog = SpawnUploadDialog(uploadPrefabPath);
		UploadData uploadData = CreateUploadData(fileName, filePath, null, false, true);
		uploadData.Tags = existingTags;
		uploadDialog.Initialize(UploadDialog.UploadDialogMode.UpdateExisting, uploadData, tex);
		Close(false);
		saveMenuUpload = true;
	}

	private string GetUploadPrefabPath(bool isLevel)
	{
		if (!isLevel)
		{
			return "Workshop/UploadDialog";
		}
		return "Workshop/Level Upload/UploadLevelDialog";
	}

	private void UploadFile(WorkshopManager.ItemTypes uploadType, string fileName, string filePath, string thumbnailPath)
	{
		bool flag = uploadType == WorkshopManager.ItemTypes.Levels;
		ReferenceMaster.UIActive = (flag ? ReferenceMaster.WorkshopItemType.Levels : ReferenceMaster.WorkshopItemType.Machine);
		string uploadPrefabPath = GetUploadPrefabPath(flag);
		UploadDialog uploadDialog = SpawnUploadDialog(uploadPrefabPath);
		UploadData uploadData = CreateUploadData(fileName, filePath, thumbnailPath, false);
		uploadDialog.Initialize(UploadDialog.UploadDialogMode.NewUpload, uploadData);
		Close();
	}

	private void UploadFolder(string folderName, string folderPath, string thumbnailPath)
	{
		ReferenceMaster.UIActive = ReferenceMaster.WorkshopItemType.Levels;
		string prefabPath = "Workshop/Level Upload/UploadLevelDialog";
		UploadDialog uploadDialog = SpawnUploadDialog(prefabPath);
		UploadData uploadData = CreateUploadData(folderName, folderPath, thumbnailPath, true);
		uploadDialog.Initialize(UploadDialog.UploadDialogMode.NewUpload, uploadData);
		Close();
	}

	private UploadDialog SpawnUploadDialog(string prefabPath)
	{
		UploadDialog original = Resources.Load<UploadDialog>(prefabPath);
		UploadDialog uploadDialog = (UploadDialog)UnityEngine.Object.Instantiate(original, new Vector3(-1.268114f, -19.89792f, 2.014296f), Quaternion.identity);
		uploadDialog.ModifyClicked = OnUploadDialogUpdateClicked;
		uploadDialog.UploadClicked = OnUploadDialogUploadClicked;
		return uploadDialog;
	}

	public void CacheWorkshopItem(IWorkshopItem workshopItem)
	{
		cachedWorkshopItem = workshopItem;
	}

	private void OnUploadDialogUploadClicked(UploadDialog.UploadDialogMode uploadType, UploadData uploadData)
	{
		switch (uploadType)
		{
		case UploadDialog.UploadDialogMode.NewUpload:
		{
			WorkshopManager instance = SingleInstance<WorkshopManager>.Instance;
			if (ReferenceMaster.UIActive == ReferenceMaster.WorkshopItemType.Machine)
			{
				Debug.Log("Uploading: '" + uploadData.Path + "' tags: " + string.Join(",", uploadData.Tags.ToArray()));
				instance.CreateWorkshopMachine(uploadData);
			}
			else if (ReferenceMaster.UIActive == ReferenceMaster.WorkshopItemType.Skins)
			{
				instance.CreateWorkshopSkin(uploadData);
			}
			else if (ReferenceMaster.UIActive == ReferenceMaster.WorkshopItemType.Levels)
			{
				instance.CreateWorkshopLevel(uploadData);
			}
			break;
		}
		case UploadDialog.UploadDialogMode.UpdateExisting:
			UpdateWorkshopFile(uploadData);
			break;
		}
	}

	public void UpdateWorkshopFileFromLocal(UploadData localFileUploadData)
	{
		UploadData uploadData = new UploadData(cachedUploadData);
		uploadData.Path = localFileUploadData.Path;
		uploadData.UploadContent = true;
		uploadData.ItemType = localFileUploadData.ItemType;
		if (!cachedUploadData.UploadThumbnail || string.IsNullOrEmpty(cachedUploadData.ThumbnailPath))
		{
			uploadData.ThumbnailPath = localFileUploadData.ThumbnailPath;
			uploadData.UploadThumbnail = true;
		}
		switch (ReferenceMaster.UIActive)
		{
		case ReferenceMaster.WorkshopItemType.Machine:
			uploadData.DlcDependencyMask = BesiegeContentHelper.GetDlcDependencyMaskFromPath(localFileUploadData.Path, localFileUploadData.ItemType, localFileUploadData.IsFolder);
			break;
		case ReferenceMaster.WorkshopItemType.Levels:
			uploadData.DlcDependencyMask = BesiegeContentHelper.GetDlcDependencyMaskFromPath(localFileUploadData.Path, localFileUploadData.ItemType, localFileUploadData.IsFolder);
			break;
		case ReferenceMaster.WorkshopItemType.Mods:
			uploadData.DlcDependencyMask = BesiegeContentHelper.GetDlcDependencyMaskFromPath(localFileUploadData.Path, localFileUploadData.ItemType, localFileUploadData.IsFolder);
			break;
		}
		UpdateWorkshopFile(uploadData);
	}

	public void UpdateWorkshopFile(UploadData uploadData)
	{
		if (cachedWorkshopItem == null)
		{
			Debug.LogError("CachedWorkshopItem is null, this should not happen");
			Close();
			return;
		}
		WorkshopManager instance = SingleInstance<WorkshopManager>.Instance;
		if (ReferenceMaster.UIActive == ReferenceMaster.WorkshopItemType.Machine)
		{
			uploadData.ItemType = WorkshopManager.ItemTypes.Machines;
			instance.UpdateWorkshopMachine(cachedWorkshopItem.WorkshopItemId, uploadData);
		}
		else if (ReferenceMaster.UIActive == ReferenceMaster.WorkshopItemType.Skins)
		{
			uploadData.ItemType = WorkshopManager.ItemTypes.Skins;
			instance.UpdateWorkshopSkin(cachedWorkshopItem.WorkshopItemId, uploadData);
		}
		else if (ReferenceMaster.UIActive == ReferenceMaster.WorkshopItemType.Levels)
		{
			uploadData.ItemType = WorkshopManager.ItemTypes.Levels;
			instance.UpdateWorkshopLevel(cachedWorkshopItem.WorkshopItemId, uploadData);
		}
		cachedWorkshopItem = null;
		cachedUploadData = null;
		Close();
	}

	private void OnUploadDialogUpdateClicked(UploadDialog.UploadDialogMode uploadType, UploadData uploadData)
	{
		cachedUploadData = uploadData;
		FileBrowserType browserType;
		if (uploadType == UploadDialog.UploadDialogMode.UpdateExisting)
		{
			ReferenceMaster.WorkshopItemType uIActive = ReferenceMaster.UIActive;
			browserType = ((uIActive == ReferenceMaster.WorkshopItemType.Levels) ? FileBrowserType.LocalLevels : FileBrowserType.LocalMachines);
			cachedUploadData.UploadContent = true;
		}
		else
		{
			switch (ReferenceMaster.UIActive)
			{
			case ReferenceMaster.WorkshopItemType.Levels:
				browserType = FileBrowserType.PublishedSteamLevels;
				break;
			case ReferenceMaster.WorkshopItemType.Skins:
				browserType = FileBrowserType.PublishedSteamSkins;
				break;
			default:
				browserType = FileBrowserType.PublishedSteamMachines;
				break;
			}
		}
		Open(browserType, false);
	}

	public void UploadSkin(BlockSkinLoader.SkinPack skinPack)
	{
		WorkshopManager instance = SingleInstance<WorkshopManager>.Instance;
		if (!(instance == null))
		{
			StartCoroutine(UploadSkinIE(skinPack));
		}
	}

	private IEnumerator UploadSkinIE(BlockSkinLoader.SkinPack skinPack)
	{
		BlockSkinLoader.SkinPack.Skin skinDisplayed = skinPack.FindAvailableSkin();
		string thumbnailPath = skinPack.path + "/thumbnail.png";
		if (!File.Exists(thumbnailPath))
		{
			SkinThumbnail capture = UnityEngine.Object.FindObjectOfType<SkinThumbnail>();
			if (skinDisplayed != null)
			{
				capture.SetSkin(skinDisplayed.Register(capture));
				while (!skinDisplayed.doneLoading)
				{
					yield return null;
				}
				capture.CaptureImage(thumbnailPath);
				float startTime = Time.time;
				while (!SkinThumbnail.Finished && Time.time < startTime + 10f)
				{
					yield return null;
				}
			}
			if (!File.Exists(thumbnailPath))
			{
				Texture2D image = Resources.Load<Texture2D>("Workshop/NoThumbnailFound");
				byte[] output = image.EncodeToPNG();
				try
				{
					File.WriteAllBytes(thumbnailPath, output);
				}
				catch (Exception ex)
				{
					Exception err = ex;
					Debug.Log("Could not write thumbnail: " + err);
				}
			}
			if (skinDisplayed != null)
			{
				skinDisplayed.Unregister(capture);
			}
		}
		OpenSkinUploadDialog(skinPack.path, skinPack.name, thumbnailPath);
		Close();
	}

	private void OpenSkinUploadDialog(string folderPath, string folderName, string thumbnailPath)
	{
		ReferenceMaster.UIActive = ReferenceMaster.WorkshopItemType.Skins;
		string prefabPath = "Workshop/Skin Upload/SteamUploadSkinDialog";
		SkinUploadDialog skinUploadDialog = (SkinUploadDialog)SpawnUploadDialog(prefabPath);
		UploadData uploadData = CreateUploadData(folderName, folderPath, thumbnailPath, true);
		skinUploadDialog.Initialize(UploadDialog.UploadDialogMode.NewUploadOrModify, uploadData);
		SingleInstanceFindOnly<PrefabVisualUI>.Instance.Close();
	}

	internal void OpenDlcsMissingPopup(List<DlcManager.DlcStatus> dlcIssues, int headerLocId)
	{
		if (!dlcsNotInstalledPopup.gameObject.activeSelf)
		{
			dlcsNotInstalledPopup.Setup(dlcIssues, headerLocId);
			dlcsNotInstalledPopup.gameObject.SetActive(true);
		}
	}

	internal void OpenDlcsMissingPopup(uint dlcDependencyMask, int headerLocId)
	{
		if (!dlcsNotInstalledPopup.gameObject.activeSelf)
		{
			dlcsNotInstalledPopup.Setup(dlcDependencyMask, headerLocId);
			dlcsNotInstalledPopup.gameObject.SetActive(true);
		}
	}
}
