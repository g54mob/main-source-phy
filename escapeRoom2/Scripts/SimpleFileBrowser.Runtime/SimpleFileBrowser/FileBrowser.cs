using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SimpleFileBrowser
{
	public class FileBrowser : MonoBehaviour, IListViewAdapter
	{
		public enum Permission
		{
			Denied = 0,
			Granted = 1,
			ShouldAsk = 2
		}

		public enum PickMode
		{
			Files = 0,
			Folders = 1,
			FilesAndFolders = 2
		}

		[Serializable]
		private struct FiletypeIcon
		{
			public string extension;

			public Sprite icon;
		}

		[Serializable]
		private struct QuickLink
		{
			public Environment.SpecialFolder target;

			public string name;

			public Sprite icon;
		}

		public class Filter
		{
			public readonly string name;

			public readonly string[] extensions;

			public readonly HashSet<string> extensionsSet;

			public readonly string defaultExtension;

			public readonly bool allExtensionsHaveSingleSuffix;

			internal Filter(string name)
			{
				this.name = name;
				extensions = null;
				extensionsSet = null;
				defaultExtension = null;
				allExtensionsHaveSingleSuffix = true;
			}

			public Filter(string name, string extension)
			{
				this.name = name;
				extension = extension.ToLowerInvariant();
				if (extension[0] != '.')
				{
					extension = "." + extension;
				}
				extensions = new string[1] { extension };
				extensionsSet = new HashSet<string> { extension };
				defaultExtension = extension;
				allExtensionsHaveSingleSuffix = extension.LastIndexOf('.') == 0;
			}

			public Filter(string name, params string[] extensions)
			{
				this.name = name;
				allExtensionsHaveSingleSuffix = true;
				for (int i = 0; i < extensions.Length; i++)
				{
					extensions[i] = extensions[i].ToLowerInvariant();
					if (extensions[i][0] != '.')
					{
						extensions[i] = "." + extensions[i];
					}
					allExtensionsHaveSingleSuffix &= extensions[i].LastIndexOf('.') == 0;
				}
				this.extensions = extensions;
				extensionsSet = new HashSet<string>(extensions);
				defaultExtension = extensions[0];
			}

			public bool MatchesExtension(string extension, bool extensionMayHaveMultipleSuffixes)
			{
				if (extensionsSet == null || extensionsSet.Contains(extension))
				{
					return true;
				}
				if (extensionMayHaveMultipleSuffixes)
				{
					for (int i = 0; i < extensions.Length; i++)
					{
						if (extension.EndsWith(extensions[i], StringComparison.Ordinal))
						{
							extensionsSet.Add(extension);
							return true;
						}
					}
				}
				return false;
			}

			public override string ToString()
			{
				string text = string.Empty;
				if (name != null)
				{
					text = name;
				}
				if (extensions != null)
				{
					if (name != null)
					{
						text += " (";
					}
					for (int i = 0; i < extensions.Length; i++)
					{
						text = ((i <= 0) ? (text + extensions[i]) : (text + ", " + extensions[i]));
					}
					if (name != null)
					{
						text += ")";
					}
				}
				return text;
			}
		}

		public delegate void OnSuccess(string[] paths);

		public delegate void OnCancel();

		public delegate bool FileSystemEntryFilter(FileSystemEntry entry);

		public delegate void PermissionCallback(Permission permission);

		private const int FILENAME_INPUT_FIELD_MAX_FILE_COUNT = 7;

		private const string SAF_PICK_FOLDER_QUICK_LINK_PATH = "SAF_PICK_FOLDER";

		[SerializeField]
		private UISkin m_skin;

		private int m_skinVersion;

		private int m_skinPrevFontSize;

		private Sprite m_skinPrevDriveIcon;

		private Sprite m_skinPrevFolderIcon;

		private static bool m_askPermissions = true;

		private static bool m_singleClickMode = false;

		private FileSystemEntryFilter m_displayedEntriesFilter;

		private static bool m_showFileOverwriteDialog = true;

		private static bool m_checkWriteAccessToDestinationDirectory = false;

		public static bool CanDeleteFiles = true;

		public static bool CanRenameFiles = true;

		private static float m_drivesRefreshInterval = 5f;

		private bool m_displayHiddenFilesToggle = true;

		private string m_allFilesFilterText = "All Files (.*)";

		private string m_foldersFilterText = "Folders";

		private string m_pickFolderQuickLinkText = "Browse...";

		private static FileBrowser m_instance = null;

		[Header("Settings")]
		[SerializeField]
		internal int minWidth = 380;

		[SerializeField]
		internal int minHeight = 300;

		[SerializeField]
		private float narrowScreenWidth = 380f;

		[SerializeField]
		private float quickLinksMaxWidthPercentage = 0.4f;

		[SerializeField]
		private bool sortFilesByName = true;

		[SerializeField]
		[FormerlySerializedAs("excludeExtensions")]
		private string[] excludedExtensions;

		[SerializeField]
		private QuickLink[] quickLinks;

		private bool quickLinksInitialized;

		private readonly HashSet<string> excludedExtensionsSet = new HashSet<string>();

		[SerializeField]
		private bool generateQuickLinksForDrives = true;

		[SerializeField]
		private bool showResizeCursor = true;

		[Header("Internal References")]
		[SerializeField]
		private FileBrowserMovement window;

		private RectTransform windowTR;

		private VerticalLayoutGroup windowLayoutGroup;

		[SerializeField]
		private LayoutElement[] windowResponsiveRows;

		[SerializeField]
		private RectTransform topViewNarrowScreen;

		[SerializeField]
		private Image middleView;

		[SerializeField]
		private RectTransform middleViewQuickLinks;

		private Vector2 middleViewQuickLinksOriginalSize;

		[SerializeField]
		private RectTransform middleViewFiles;

		[SerializeField]
		private Image middleViewSeparator;

		[SerializeField]
		private FileBrowserItem itemPrefab;

		private readonly List<FileBrowserItem> allItems = new List<FileBrowserItem>(16);

		[SerializeField]
		private FileBrowserQuickLink quickLinkPrefab;

		private readonly List<FileBrowserQuickLink> allQuickLinks = new List<FileBrowserQuickLink>(8);

		[SerializeField]
		private TextMeshProUGUI titleText;

		[SerializeField]
		private Image titleBackground;

		[SerializeField]
		private Button backButton;

		[SerializeField]
		private Button forwardButton;

		[SerializeField]
		private Button upButton;

		[SerializeField]
		private Button moreOptionsButton;

		[SerializeField]
		private TMP_InputField pathInputField;

		[SerializeField]
		private RectTransform pathInputFieldSlotTop;

		[SerializeField]
		private RectTransform pathInputFieldSlotBottom;

		[SerializeField]
		private TMP_InputField searchInputField;

		[SerializeField]
		private RectTransform quickLinksContainer;

		[SerializeField]
		private ScrollRect quickLinksScrollRect;

		[SerializeField]
		private RectTransform filesContainer;

		[SerializeField]
		private ScrollRect filesScrollRect;

		[SerializeField]
		private RecycledListView listView;

		[SerializeField]
		private TMP_InputField filenameInputField;

		[SerializeField]
		private Image filenameImage;

		[SerializeField]
		private TMP_Dropdown filtersDropdown;

		private RectTransform filtersDropdownContainer;

		private TMP_Text filterItemTemplate;

		[SerializeField]
		private Toggle showHiddenFilesToggle;

		[SerializeField]
		private Button submitButton;

		[SerializeField]
		private TextMeshProUGUI submitButtonText;

		[SerializeField]
		private Button cancelButton;

		[SerializeField]
		private Button[] allButtons;

		[SerializeField]
		private RectTransform moreOptionsContextMenuPosition;

		[SerializeField]
		private FileBrowserRenamedItem renameItem;

		[SerializeField]
		private FileBrowserContextMenu contextMenu;

		[SerializeField]
		private FileBrowserFileOperationConfirmationPanel fileOperationConfirmationPanel;

		[SerializeField]
		private FileBrowserAccessRestrictedPanel accessRestrictedPanel;

		[SerializeField]
		private FileBrowserCursorHandler resizeCursorHandler;

		internal RectTransform rectTransform;

		private Canvas canvas;

		private FileAttributes ignoredFileAttributes = FileAttributes.System;

		private FileSystemEntry[] allFileEntries;

		private readonly List<FileSystemEntry> validFileEntries = new List<FileSystemEntry>();

		private readonly List<int> selectedFileEntries = new List<int>(4);

		private readonly List<string> pendingFileEntrySelection = new List<string>();

		private readonly List<string> submittedFileEntryPaths = new List<string>(4);

		private readonly List<string> submittedFolderPaths = new List<string>(4);

		private readonly List<FileSystemEntry> submittedFileEntriesToOverwrite = new List<FileSystemEntry>(4);

		private int multiSelectionPivotFileEntry;

		private StringBuilder multiSelectionFilenameBuilder;

		private readonly List<Filter> filters = new List<Filter>();

		private Filter allFilesFilter;

		private readonly List<string> filterLabels = new List<string>(4);

		private bool showAllFilesFilter = true;

		private bool allFiltersHaveSingleSuffix = true;

		private bool allExcludedExtensionsHaveSingleSuffix = true;

		private string defaultInitialPath;

		private int currentPathIndex = -1;

		private readonly List<string> pathsFollowed = new List<string>();

		private HashSet<char> invalidFilenameChars;

		private float drivesNextRefreshTime;

		private string[] driveQuickLinks;

		private int numberOfDriveQuickLinks;

		private readonly List<string> timedOutDirectoryExistsRequests = new List<string>(2);

		private bool canvasDimensionsChanged;

		private readonly CompareInfo textComparer = new CultureInfo("en-US").CompareInfo;

		private readonly CompareOptions textCompareOptions = CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace;

		private PointerEventData nullPointerEventData;

		private string m_currentPath = string.Empty;

		private string m_searchString = string.Empty;

		private bool m_acceptNonExistingFilename;

		private PickMode m_pickerMode;

		private bool m_allowMultiSelection;

		private bool m_multiSelectionToggleSelectionMode;

		private OnSuccess onSuccess;

		private OnCancel onCancel;

		public static bool IsOpen { get; private set; }

		public static bool Success { get; private set; }

		public static string[] Result { get; private set; }

		public static UISkin Skin
		{
			get
			{
				return Instance.m_skin;
			}
			set
			{
				if ((bool)value && Instance.m_skin != value)
				{
					Instance.m_skin = value;
					Instance.m_skinVersion = Instance.m_skin.Version;
					Instance.RefreshSkin();
				}
			}
		}

		public static bool AskPermissions
		{
			get
			{
				return m_askPermissions;
			}
			set
			{
				m_askPermissions = value;
			}
		}

		public static bool SingleClickMode
		{
			get
			{
				return m_singleClickMode;
			}
			set
			{
				m_singleClickMode = value;
			}
		}

		public static bool ShowFileOverwriteDialog
		{
			get
			{
				return m_showFileOverwriteDialog;
			}
			set
			{
				m_showFileOverwriteDialog = value;
			}
		}

		public static bool CheckWriteAccessToDestinationDirectory
		{
			get
			{
				return m_checkWriteAccessToDestinationDirectory;
			}
			set
			{
				m_checkWriteAccessToDestinationDirectory = value;
			}
		}

		public static float DrivesRefreshInterval
		{
			get
			{
				return m_drivesRefreshInterval;
			}
			set
			{
				m_drivesRefreshInterval = value;
			}
		}

		public static bool ShowHiddenFiles
		{
			get
			{
				return Instance.showHiddenFilesToggle.isOn;
			}
			set
			{
				Instance.showHiddenFilesToggle.isOn = value;
			}
		}

		public static bool DisplayHiddenFilesToggle
		{
			get
			{
				return Instance.m_displayHiddenFilesToggle;
			}
			set
			{
				if (Instance.m_displayHiddenFilesToggle != value)
				{
					m_instance.m_displayHiddenFilesToggle = value;
					if (!value)
					{
						m_instance.showHiddenFilesToggle.gameObject.SetActive(value: false);
					}
					else if (m_instance.windowTR.sizeDelta.x >= m_instance.narrowScreenWidth)
					{
						m_instance.showHiddenFilesToggle.gameObject.SetActive(value: true);
					}
				}
			}
		}

		public static string AllFilesFilterText
		{
			get
			{
				return Instance.m_allFilesFilterText;
			}
			set
			{
				if (Instance.m_allFilesFilterText != value)
				{
					string allFilesFilterText = m_instance.m_allFilesFilterText;
					m_instance.m_allFilesFilterText = value;
					Filter filter = m_instance.allFilesFilter;
					m_instance.allFilesFilter = new Filter(value);
					if (m_instance.filters.Count > 0 && m_instance.filters[0] == filter)
					{
						m_instance.filters[0] = m_instance.allFilesFilter;
					}
					if (m_instance.filtersDropdown.options[0].text == allFilesFilterText)
					{
						m_instance.filtersDropdown.options[0].text = value;
						m_instance.filtersDropdown.RefreshShownValue();
					}
				}
			}
		}

		public static string FoldersFilterText
		{
			get
			{
				return Instance.m_foldersFilterText;
			}
			set
			{
				if (Instance.m_foldersFilterText != value)
				{
					string foldersFilterText = m_instance.m_foldersFilterText;
					m_instance.m_foldersFilterText = value;
					if (m_instance.filtersDropdown.options[0].text == foldersFilterText)
					{
						m_instance.filtersDropdown.options[0].text = value;
						m_instance.filtersDropdown.RefreshShownValue();
					}
				}
			}
		}

		public static string PickFolderQuickLinkText
		{
			get
			{
				return Instance.m_pickFolderQuickLinkText;
			}
			set
			{
				if (!(Instance.m_pickFolderQuickLinkText != value))
				{
					return;
				}
				m_instance.m_pickFolderQuickLinkText = value;
				for (int i = 0; i < m_instance.allQuickLinks.Count; i++)
				{
					FileBrowserQuickLink fileBrowserQuickLink = m_instance.allQuickLinks[i];
					if ((bool)fileBrowserQuickLink && fileBrowserQuickLink.TargetPath == "SAF_PICK_FOLDER")
					{
						fileBrowserQuickLink.SetQuickLink(Skin.DriveIcon, value, "SAF_PICK_FOLDER");
						break;
					}
				}
			}
		}

		public static FileBrowser Instance
		{
			get
			{
				if (!m_instance)
				{
					m_instance = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("SimpleFileBrowserCanvas")).GetComponent<FileBrowser>();
					m_instance.gameObject.SetActive(value: false);
				}
				return m_instance;
			}
		}

		private bool AllExtensionsHaveSingleSuffix
		{
			get
			{
				if (allFiltersHaveSingleSuffix && allExcludedExtensionsHaveSingleSuffix)
				{
					return m_skin.AllIconExtensionsHaveSingleSuffix;
				}
				return false;
			}
		}

		private string CurrentPath
		{
			get
			{
				return m_currentPath;
			}
			set
			{
				if (value != null)
				{
					value = value.Trim();
					value = GetPathWithoutTrailingDirectorySeparator(value);
				}
				if (string.IsNullOrEmpty(value))
				{
					pathInputField.text = m_currentPath;
					return;
				}
				if (m_currentPath != value)
				{
					if (!FileBrowserHelpers.DirectoryExists(value))
					{
						pathInputField.text = m_currentPath;
						return;
					}
					m_currentPath = value;
					pathInputField.text = m_currentPath;
					if (currentPathIndex == -1 || pathsFollowed[currentPathIndex] != m_currentPath)
					{
						currentPathIndex++;
						if (currentPathIndex < pathsFollowed.Count)
						{
							pathsFollowed[currentPathIndex] = value;
							for (int num = pathsFollowed.Count - 1; num >= currentPathIndex + 1; num--)
							{
								pathsFollowed.RemoveAt(num);
							}
						}
						else
						{
							pathsFollowed.Add(m_currentPath);
						}
					}
					backButton.interactable = currentPathIndex > 0;
					forwardButton.interactable = currentPathIndex < pathsFollowed.Count - 1;
					try
					{
						upButton.interactable = Directory.GetParent(m_currentPath) != null;
					}
					catch
					{
						upButton.interactable = false;
					}
					m_searchString = string.Empty;
					searchInputField.text = m_searchString;
					multiSelectionPivotFileEntry = 0;
					filesScrollRect.verticalNormalizedPosition = 1f;
					filenameImage.color = m_skin.InputFieldNormalBackgroundColor;
					if (m_pickerMode != PickMode.Files)
					{
						filenameInputField.text = string.Empty;
						filenameInputField.interactable = true;
					}
					for (int i = 0; i < allQuickLinks.Count; i++)
					{
						allQuickLinks[i].SetSelected(allQuickLinks[i].TargetPath == m_currentPath);
					}
				}
				m_multiSelectionToggleSelectionMode = false;
				RefreshFiles(pathChanged: true);
			}
		}

		private string SearchString
		{
			get
			{
				return m_searchString;
			}
			set
			{
				if (m_searchString != value)
				{
					m_searchString = value;
					searchInputField.text = m_searchString;
					RefreshFiles(pathChanged: false);
				}
			}
		}

		private bool AcceptNonExistingFilename
		{
			get
			{
				return m_acceptNonExistingFilename;
			}
			set
			{
				m_acceptNonExistingFilename = value;
			}
		}

		internal PickMode PickerMode
		{
			get
			{
				return m_pickerMode;
			}
			private set
			{
				m_pickerMode = value;
				if (m_pickerMode == PickMode.Folders)
				{
					filtersDropdown.options[0].text = FoldersFilterText;
					filtersDropdown.value = 0;
					filtersDropdown.interactable = false;
				}
				else
				{
					filtersDropdown.options[0].text = filters[0].ToString();
					filtersDropdown.interactable = true;
				}
				filtersDropdown.RefreshShownValue();
				TextMeshProUGUI textMeshProUGUI = filenameInputField.placeholder as TextMeshProUGUI;
				if ((bool)textMeshProUGUI)
				{
					textMeshProUGUI.gameObject.SetActive(m_pickerMode != PickMode.Folders);
				}
			}
		}

		internal bool AllowMultiSelection
		{
			get
			{
				return m_allowMultiSelection;
			}
			private set
			{
				m_allowMultiSelection = value;
			}
		}

		internal bool MultiSelectionToggleSelectionMode
		{
			get
			{
				return m_multiSelectionToggleSelectionMode;
			}
			private set
			{
				if (m_multiSelectionToggleSelectionMode == value)
				{
					return;
				}
				m_multiSelectionToggleSelectionMode = value;
				for (int i = 0; i < allItems.Count; i++)
				{
					if (allItems[i].gameObject.activeSelf)
					{
						allItems[i].SetSelected(selectedFileEntries.Contains(allItems[i].Position));
					}
				}
			}
		}

		private string Title
		{
			get
			{
				return titleText.text;
			}
			set
			{
				titleText.text = value;
			}
		}

		private string SubmitButtonText
		{
			get
			{
				return submitButtonText.text;
			}
			set
			{
				submitButtonText.text = value;
			}
		}

		private string LastBrowsedFolder
		{
			get
			{
				return PlayerPrefs.GetString("FBLastPath", null);
			}
			set
			{
				PlayerPrefs.SetString("FBLastPath", value);
			}
		}

		OnItemClickedHandler IListViewAdapter.OnItemClicked
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		int IListViewAdapter.Count => validFileEntries.Count;

		float IListViewAdapter.ItemHeight => m_skin.FileHeight;

		public static event FileSystemEntryFilter DisplayedEntriesFilter
		{
			add
			{
				FileBrowser instance = Instance;
				instance.m_displayedEntriesFilter = (FileSystemEntryFilter)Delegate.Combine(instance.m_displayedEntriesFilter, value);
				m_instance.PersistFileEntrySelection();
				m_instance.RefreshFiles(pathChanged: false);
			}
			remove
			{
				FileBrowser instance = Instance;
				instance.m_displayedEntriesFilter = (FileSystemEntryFilter)Delegate.Remove(instance.m_displayedEntriesFilter, value);
				m_instance.PersistFileEntrySelection();
				m_instance.RefreshFiles(pathChanged: false);
			}
		}

		private void Awake()
		{
			m_instance = this;
			canvas = GetComponent<Canvas>();
			rectTransform = (RectTransform)base.transform;
			windowTR = (RectTransform)window.transform;
			windowLayoutGroup = window.GetComponent<VerticalLayoutGroup>();
			filtersDropdownContainer = filtersDropdown.template;
			filterItemTemplate = filtersDropdown.itemText;
			middleViewQuickLinksOriginalSize = middleViewQuickLinks.sizeDelta;
			nullPointerEventData = new PointerEventData(null);
			defaultInitialPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			SetExcludedExtensions(excludedExtensions);
			backButton.interactable = false;
			forwardButton.interactable = false;
			upButton.interactable = false;
			backButton.onClick.AddListener(OnBackButtonClicked);
			forwardButton.onClick.AddListener(OnForwardButtonClicked);
			upButton.onClick.AddListener(OnUpButtonClicked);
			moreOptionsButton.onClick.AddListener(OnMoreOptionsButtonClicked);
			submitButton.onClick.AddListener(OnSubmitButtonClicked);
			cancelButton.onClick.AddListener(OnCancelButtonClicked);
			pathInputField.onEndEdit.AddListener(OnPathChanged);
			searchInputField.onValueChanged.AddListener(OnSearchStringChanged);
			filenameInputField.onSubmit.AddListener(delegate
			{
				OnSubmitButtonClicked();
			});
			filenameInputField.onEndEdit.AddListener(delegate
			{
				ResetInputFieldTextPosition(filenameInputField);
			});
			filenameInputField.onValueChanged.AddListener(OnFilenameInputChanged);
			filtersDropdown.onValueChanged.AddListener(OnFilterChanged);
			showHiddenFilesToggle.onValueChanged.AddListener(OnShowHiddenFilesToggleChanged);
			allFilesFilter = new Filter(AllFilesFilterText);
			filters.Add(allFilesFilter);
			filterLabels.Add(allFilesFilter.ToString());
			invalidFilenameChars = new HashSet<char>(Path.GetInvalidFileNameChars())
			{
				Path.DirectorySeparatorChar,
				Path.AltDirectorySeparatorChar
			};
			window.Initialize(this);
			listView.SetAdapter(this);
			m_skinVersion = m_skin.Version;
			RefreshSkin();
			if (!showResizeCursor)
			{
				UnityEngine.Object.Destroy(resizeCursorHandler);
			}
		}

		private void OnRectTransformDimensionsChange()
		{
			canvasDimensionsChanged = true;
		}

		private void Update()
		{
			if ((bool)m_skin && m_skinVersion != m_skin.Version)
			{
				m_skinVersion = m_skin.Version;
				RefreshSkin();
			}
		}

		private void LateUpdate()
		{
			if (canvasDimensionsChanged)
			{
				canvasDimensionsChanged = false;
				Vector2 sizeDelta = windowTR.sizeDelta;
				EnsureWindowIsWithinBounds();
				if (windowTR.sizeDelta != sizeDelta)
				{
					OnWindowDimensionsChanged(windowTR.sizeDelta);
				}
				fileOperationConfirmationPanel.OnCanvasDimensionsChanged(rectTransform.sizeDelta);
				if (contextMenu.gameObject.activeSelf)
				{
					contextMenu.Hide();
				}
			}
			if (!EventSystem.current.currentSelectedGameObject)
			{
				if (Input.GetKeyDown(KeyCode.Delete))
				{
					DeleteSelectedFiles();
				}
				if (Input.GetKeyDown(KeyCode.F2))
				{
					RenameSelectedFile();
				}
				if (Input.GetKeyDown(KeyCode.A) && IsCtrlKeyHeld())
				{
					SelectAllFiles();
				}
			}
			if (quickLinksInitialized && generateQuickLinksForDrives && m_drivesRefreshInterval >= 0f && Time.realtimeSinceStartup >= drivesNextRefreshTime)
			{
				drivesNextRefreshTime = Time.realtimeSinceStartup + m_drivesRefreshInterval;
				RefreshDriveQuickLinks();
			}
		}

		private void OnApplicationFocus(bool focus)
		{
			if (!focus)
			{
				PersistFileEntrySelection();
			}
			else
			{
				RefreshFiles(pathChanged: true);
			}
		}

		ListItem IListViewAdapter.CreateItem()
		{
			FileBrowserItem fileBrowserItem = UnityEngine.Object.Instantiate(itemPrefab, filesContainer, worldPositionStays: false);
			fileBrowserItem.SetFileBrowser(this, m_skin);
			allItems.Add(fileBrowserItem);
			return fileBrowserItem;
		}

		void IListViewAdapter.SetItemContent(ListItem item)
		{
			FileBrowserItem fileBrowserItem = (FileBrowserItem)item;
			FileSystemEntry fileInfo = validFileEntries[item.Position];
			fileBrowserItem.SetFile(GetIconForFileEntry(in fileInfo), fileInfo.Name, fileInfo.IsDirectory);
			fileBrowserItem.SetSelected(selectedFileEntries.Contains(fileBrowserItem.Position));
			fileBrowserItem.SetHidden((fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden);
		}

		private void InitializeQuickLinks()
		{
			quickLinksInitialized = true;
			drivesNextRefreshTime = Time.realtimeSinceStartup + m_drivesRefreshInterval;
			FileBrowserQuickLink[] array = ((allQuickLinks.Count > 0) ? allQuickLinks.ToArray() : null);
			if (generateQuickLinksForDrives)
			{
				RefreshDriveQuickLinks();
			}
			for (int i = 0; i < quickLinks.Length; i++)
			{
				QuickLink quickLink = quickLinks[i];
				string folderPath = Environment.GetFolderPath(quickLink.target);
				AddQuickLink(quickLink.icon, quickLink.name, folderPath);
			}
			quickLinks = null;
			if (array != null && allQuickLinks.Count > array.Length)
			{
				for (int j = 0; j < array.Length; j++)
				{
					allQuickLinks.Remove(array[j]);
					allQuickLinks.Add(array[j]);
				}
				for (int k = 0; k < allQuickLinks.Count; k++)
				{
					allQuickLinks[k].TransformComponent.anchoredPosition = new Vector2(0f, (float)(-k) * m_skin.FileHeight);
				}
			}
		}

		private void RefreshDriveQuickLinks()
		{
			string[] logicalDrives = Directory.GetLogicalDrives();
			if (driveQuickLinks != null && logicalDrives.Length == driveQuickLinks.Length)
			{
				bool flag = true;
				for (int i = 0; i < logicalDrives.Length; i++)
				{
					if (!timedOutDirectoryExistsRequests.Contains(logicalDrives[i]) && logicalDrives[i] != driveQuickLinks[i])
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					return;
				}
			}
			for (driveQuickLinks = logicalDrives; numberOfDriveQuickLinks > 0; numberOfDriveQuickLinks--)
			{
				UnityEngine.Object.Destroy(allQuickLinks[numberOfDriveQuickLinks - 1].gameObject);
				allQuickLinks.RemoveAt(numberOfDriveQuickLinks - 1);
			}
			FileBrowserQuickLink[] array = ((allQuickLinks.Count > 0) ? allQuickLinks.ToArray() : null);
			allQuickLinks.Clear();
			quickLinksContainer.sizeDelta = Vector2.zero;
			for (int j = 0; j < logicalDrives.Length; j++)
			{
				if (!string.IsNullOrEmpty(logicalDrives[j]) && AddQuickLink(m_skin.DriveIcon, logicalDrives[j], logicalDrives[j]))
				{
					numberOfDriveQuickLinks++;
				}
			}
			if (array != null)
			{
				Vector2 anchoredPosition = new Vector2(0f, 0f - quickLinksContainer.sizeDelta.y);
				for (int k = 0; k < array.Length; k++)
				{
					array[k].TransformComponent.anchoredPosition = anchoredPosition;
					anchoredPosition.y -= m_skin.FileHeight;
					allQuickLinks.Add(array[k]);
				}
				quickLinksContainer.sizeDelta = new Vector2(0f, 0f - anchoredPosition.y);
			}
			try
			{
				if (!string.IsNullOrEmpty(m_currentPath) && !FileBrowserHelpers.DirectoryExists(m_currentPath))
				{
					string pathRoot = Path.GetPathRoot(m_currentPath);
					if (!string.IsNullOrEmpty(pathRoot) && FileBrowserHelpers.DirectoryExists(pathRoot))
					{
						CurrentPath = pathRoot;
					}
					else if (allQuickLinks.Count > 0)
					{
						CurrentPath = allQuickLinks[0].TargetPath;
					}
				}
			}
			catch
			{
			}
		}

		private void RefreshSkin()
		{
			windowLayoutGroup.spacing = m_skin.RowSpacing;
			for (int i = 0; i < windowResponsiveRows.Length; i++)
			{
				windowResponsiveRows[i].preferredHeight = m_skin.RowHeight;
			}
			moreOptionsContextMenuPosition.anchoredPosition = new Vector2(moreOptionsContextMenuPosition.anchoredPosition.x, 0f - m_skin.RowSpacing);
			window.GetComponent<Image>().color = m_skin.WindowColor;
			middleView.color = m_skin.FilesListColor;
			middleViewSeparator.color = m_skin.FilesVerticalSeparatorColor;
			titleBackground.color = m_skin.TitleBackgroundColor;
			m_skin.ApplyTo(titleText, m_skin.TitleTextColor);
			backButton.image.color = m_skin.HeaderButtonsColor;
			forwardButton.image.color = m_skin.HeaderButtonsColor;
			upButton.image.color = m_skin.HeaderButtonsColor;
			moreOptionsButton.image.color = m_skin.HeaderButtonsColor;
			backButton.image.sprite = m_skin.HeaderBackButton;
			forwardButton.image.sprite = m_skin.HeaderForwardButton;
			upButton.image.sprite = m_skin.HeaderUpButton;
			moreOptionsButton.image.sprite = m_skin.HeaderContextMenuButton;
			if ((bool)resizeCursorHandler)
			{
				Image component = resizeCursorHandler.GetComponent<Image>();
				component.color = m_skin.WindowResizeGizmoColor;
				component.sprite = m_skin.WindowResizeGizmo;
			}
			m_skin.ApplyTo(filenameInputField);
			m_skin.ApplyTo(pathInputField);
			m_skin.ApplyTo(searchInputField);
			m_skin.ApplyTo(renameItem.InputField);
			for (int j = 0; j < allButtons.Length; j++)
			{
				m_skin.ApplyTo(allButtons[j]);
			}
			m_skin.ApplyTo(filtersDropdown);
			m_skin.ApplyTo(showHiddenFilesToggle);
			m_skin.ApplyTo(quickLinksScrollRect.verticalScrollbar);
			m_skin.ApplyTo(filesScrollRect.verticalScrollbar);
			m_skin.ApplyTo(filtersDropdown.template.GetComponent<ScrollRect>().verticalScrollbar);
			for (int k = 0; k < allQuickLinks.Count; k++)
			{
				allQuickLinks[k].OnSkinRefreshed(m_skin);
				allQuickLinks[k].TransformComponent.anchoredPosition = new Vector2(0f, (float)allQuickLinks[k].TransformComponent.GetSiblingIndex() * (0f - m_skin.FileHeight));
				if (allQuickLinks[k].Icon.sprite == m_skinPrevDriveIcon)
				{
					allQuickLinks[k].Icon.sprite = m_skin.DriveIcon;
				}
				else if (allQuickLinks[k].Icon.sprite == m_skinPrevFolderIcon)
				{
					allQuickLinks[k].Icon.sprite = m_skin.FolderIcon;
				}
			}
			quickLinksContainer.sizeDelta = new Vector2(0f, (float)allQuickLinks.Count * m_skin.FileHeight);
			for (int l = 0; l < allItems.Count; l++)
			{
				allItems[l].OnSkinRefreshed(m_skin);
			}
			renameItem.TransformComponent.sizeDelta = new Vector2(renameItem.TransformComponent.sizeDelta.x, m_skin.FileHeight);
			contextMenu.RefreshSkin(m_skin);
			fileOperationConfirmationPanel.RefreshSkin(m_skin);
			accessRestrictedPanel.RefreshSkin(m_skin);
			if (m_skin.FontSize != m_skinPrevFontSize)
			{
				RefreshFiltersDropdownWidth();
			}
			listView.OnSkinRefreshed();
			m_skinPrevFontSize = m_skin.FontSize;
			m_skinPrevDriveIcon = m_skin.DriveIcon;
			m_skinPrevFolderIcon = m_skin.FolderIcon;
		}

		private void OnBackButtonClicked()
		{
			if (currentPathIndex > 0)
			{
				CurrentPath = pathsFollowed[--currentPathIndex];
			}
		}

		private void OnForwardButtonClicked()
		{
			if (currentPathIndex < pathsFollowed.Count - 1)
			{
				CurrentPath = pathsFollowed[++currentPathIndex];
			}
		}

		private void OnUpButtonClicked()
		{
			try
			{
				DirectoryInfo parent = Directory.GetParent(m_currentPath);
				if (parent != null)
				{
					CurrentPath = parent.FullName;
				}
			}
			catch
			{
			}
		}

		private void OnMoreOptionsButtonClicked()
		{
			ShowContextMenuAt(rectTransform.InverseTransformPoint(moreOptionsContextMenuPosition.position), isMoreOptionsMenu: true);
		}

		internal void OnContextMenuTriggered(Vector2 pointerPos)
		{
			filesScrollRect.velocity = Vector2.zero;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, pointerPos, canvas.worldCamera, out var localPoint);
			ShowContextMenuAt(localPoint, isMoreOptionsMenu: false);
		}

		private void ShowContextMenuAt(Vector2 position, bool isMoreOptionsMenu)
		{
			if (string.IsNullOrEmpty(m_currentPath))
			{
				return;
			}
			bool flag = isMoreOptionsMenu && m_allowMultiSelection && validFileEntries.Count > 0;
			bool deselectAllButtonVisible = isMoreOptionsMenu && selectedFileEntries.Count > 1;
			bool deleteButtonVisible = CanDeleteFiles && selectedFileEntries.Count > 0;
			bool renameButtonVisible = CanRenameFiles && selectedFileEntries.Count == 1;
			if (flag && m_pickerMode == PickMode.Files)
			{
				flag = false;
				for (int i = 0; i < validFileEntries.Count; i++)
				{
					if (!validFileEntries[i].IsDirectory)
					{
						flag = true;
						break;
					}
				}
			}
			contextMenu.Show(flag, deselectAllButtonVisible, deleteButtonVisible, renameButtonVisible, position, isMoreOptionsMenu);
		}

		private void OnSubmitButtonClicked()
		{
			string[] result = null;
			string text = filenameInputField.text.Trim();
			submittedFileEntryPaths.Clear();
			submittedFolderPaths.Clear();
			submittedFileEntriesToOverwrite.Clear();
			if (text.Length == 0)
			{
				if (m_pickerMode == PickMode.Files)
				{
					filenameImage.color = m_skin.InputFieldInvalidBackgroundColor;
					return;
				}
				result = new string[1] { m_currentPath };
				submittedFolderPaths.Add(m_currentPath);
			}
			if (result == null)
			{
				if (m_allowMultiSelection && selectedFileEntries.Count > 1)
				{
					if (m_pickerMode == PickMode.Files)
					{
						for (int i = 0; i < selectedFileEntries.Count; i++)
						{
							if (validFileEntries[selectedFileEntries[i]].IsDirectory)
							{
								CurrentPath = validFileEntries[selectedFileEntries[i]].Path;
								return;
							}
						}
					}
					result = new string[selectedFileEntries.Count];
					for (int j = 0; j < selectedFileEntries.Count; j++)
					{
						result[j] = validFileEntries[selectedFileEntries[j]].Path;
						if (validFileEntries[selectedFileEntries[j]].IsDirectory)
						{
							submittedFolderPaths.Add(result[j]);
						}
						else if (m_acceptNonExistingFilename)
						{
							submittedFileEntriesToOverwrite.Add(validFileEntries[selectedFileEntries[j]]);
							if (!submittedFolderPaths.Contains(m_currentPath))
							{
								submittedFolderPaths.Add(m_currentPath);
							}
						}
					}
				}
				else
				{
					int startIndex = 0;
					for (int nextStartIndex = 0; startIndex < text.Length; startIndex = nextStartIndex)
					{
						int num = ExtractFilenameFromInput(text, ref startIndex, out nextStartIndex);
						if (num == 0)
						{
							continue;
						}
						string text2 = text.Substring(startIndex, num).Trim();
						if (!VerifyFilename(text2))
						{
							try
							{
								if (FileBrowserHelpers.DirectoryExists(text2))
								{
									if (FileSystemEntryMatchesFilters(new FileSystemEntry(text2, FileBrowserHelpers.GetFilename(text2), "", isDirectory: true), AllExtensionsHaveSingleSuffix))
									{
										if (m_pickerMode == PickMode.Files)
										{
											CurrentPath = text2;
											return;
										}
										submittedFileEntryPaths.Add(text2);
										submittedFolderPaths.Add(text2);
										continue;
									}
								}
								else if (m_pickerMode != PickMode.Folders && FileBrowserHelpers.FileExists(text2))
								{
									string filename = FileBrowserHelpers.GetFilename(text2);
									FileSystemEntry item = new FileSystemEntry(text2, filename, GetExtensionFromFilename(filename, AllExtensionsHaveSingleSuffix), isDirectory: false);
									if (FileSystemEntryMatchesFilters(in item, AllExtensionsHaveSingleSuffix))
									{
										submittedFileEntryPaths.Add(text2);
										submittedFileEntriesToOverwrite.Add(item);
										if (m_acceptNonExistingFilename)
										{
											submittedFolderPaths.Add(FileBrowserHelpers.GetDirectoryName(text2));
										}
										continue;
									}
								}
							}
							catch
							{
							}
							filenameImage.color = m_skin.InputFieldInvalidBackgroundColor;
							return;
						}
						try
						{
							int num2 = FilenameToFileEntryIndex(text2);
							if (num2 < 0 && m_pickerMode != PickMode.Folders)
							{
								bool flag = filters[filtersDropdown.value].extensions == null;
								if (!m_acceptNonExistingFilename || !flag)
								{
									for (int k = 0; k < validFileEntries.Count; k++)
									{
										if (!validFileEntries[k].IsDirectory && validFileEntries[k].Name.Length >= text2.Length + 2 && validFileEntries[k].Name[text2.Length] == '.')
										{
											if (validFileEntries[k].Name.StartsWith(text2))
											{
												num2 = k;
												break;
											}
											if (textComparer.IsPrefix(validFileEntries[k].Name, text2, textCompareOptions))
											{
												num2 = k;
											}
										}
									}
								}
								if (m_acceptNonExistingFilename && num2 < 0 && !flag)
								{
									string extensionFromFilename = GetExtensionFromFilename(text2, AllExtensionsHaveSingleSuffix);
									if (string.IsNullOrEmpty(extensionFromFilename) || !filters[filtersDropdown.value].MatchesExtension(extensionFromFilename, !AllExtensionsHaveSingleSuffix))
									{
										text2 = Path.ChangeExtension(text2, filters[filtersDropdown.value].defaultExtension);
										num2 = FilenameToFileEntryIndex(text2);
									}
								}
							}
							if (num2 >= 0)
							{
								if (validFileEntries[num2].IsDirectory && m_pickerMode == PickMode.Files)
								{
									CurrentPath = validFileEntries[num2].Path;
									return;
								}
								submittedFileEntryPaths.Add(validFileEntries[num2].Path);
								if (validFileEntries[num2].IsDirectory)
								{
									submittedFolderPaths.Add(validFileEntries[num2].Path);
								}
								else if (m_acceptNonExistingFilename)
								{
									submittedFileEntriesToOverwrite.Add(validFileEntries[num2]);
									if (!submittedFolderPaths.Contains(m_currentPath))
									{
										submittedFolderPaths.Add(m_currentPath);
									}
								}
							}
							else
							{
								if (!m_acceptNonExistingFilename)
								{
									filenameImage.color = m_skin.InputFieldInvalidBackgroundColor;
									return;
								}
								submittedFileEntryPaths.Add(Path.Combine(m_currentPath, text2));
								if (!submittedFolderPaths.Contains(m_currentPath))
								{
									submittedFolderPaths.Add(m_currentPath);
								}
							}
						}
						catch (ArgumentException exception)
						{
							filenameImage.color = m_skin.InputFieldInvalidBackgroundColor;
							Debug.LogException(exception);
							return;
						}
					}
					if (submittedFileEntryPaths.Count == 0)
					{
						filenameImage.color = m_skin.InputFieldInvalidBackgroundColor;
						return;
					}
					result = submittedFileEntryPaths.ToArray();
				}
			}
			if (result == null)
			{
				return;
			}
			if (m_checkWriteAccessToDestinationDirectory)
			{
				for (int l = 0; l < submittedFolderPaths.Count; l++)
				{
					if (!string.IsNullOrEmpty(submittedFolderPaths[l]) && !CheckDirectoryWriteAccess(submittedFolderPaths[l]))
					{
						accessRestrictedPanel.Show();
						return;
					}
				}
			}
			if (m_showFileOverwriteDialog && submittedFileEntriesToOverwrite.Count > 0)
			{
				fileOperationConfirmationPanel.Show(this, submittedFileEntriesToOverwrite, FileBrowserFileOperationConfirmationPanel.OperationType.Overwrite, delegate
				{
					OnOperationSuccessful(result);
				});
			}
			else
			{
				OnOperationSuccessful(result);
			}
		}

		private void OnCancelButtonClicked()
		{
			OnOperationCanceled(invokeCancelCallback: true);
		}

		private void OnOperationSuccessful(string[] paths)
		{
			Success = true;
			Result = paths;
			Hide();
			if (!string.IsNullOrEmpty(m_currentPath))
			{
				LastBrowsedFolder = m_currentPath;
			}
			OnSuccess onSuccess = this.onSuccess;
			this.onSuccess = null;
			onCancel = null;
			onSuccess?.Invoke(paths);
		}

		private void OnOperationCanceled(bool invokeCancelCallback)
		{
			Success = false;
			Result = null;
			Hide();
			if (!string.IsNullOrEmpty(m_currentPath))
			{
				LastBrowsedFolder = m_currentPath;
			}
			OnCancel onCancel = this.onCancel;
			onSuccess = null;
			this.onCancel = null;
			if (invokeCancelCallback)
			{
				onCancel?.Invoke();
			}
		}

		private void OnPathChanged(string newPath)
		{
			if ((bool)canvas)
			{
				CurrentPath = newPath;
				ResetInputFieldTextPosition(pathInputField);
			}
		}

		private void OnSearchStringChanged(string newSearchString)
		{
			if ((bool)canvas)
			{
				PersistFileEntrySelection();
				SearchString = newSearchString;
			}
		}

		private void OnFilterChanged(int value)
		{
			if ((bool)canvas)
			{
				bool pathChanged = false;
				if (filters != null && value < filters.Count)
				{
					bool allExtensionsHaveSingleSuffix = AllExtensionsHaveSingleSuffix;
					allFiltersHaveSingleSuffix = filters[value].allExtensionsHaveSingleSuffix;
					pathChanged = AllExtensionsHaveSingleSuffix != allExtensionsHaveSingleSuffix;
				}
				PersistFileEntrySelection();
				RefreshFiles(pathChanged);
			}
		}

		private void OnShowHiddenFilesToggleChanged(bool value)
		{
			if ((bool)canvas)
			{
				PersistFileEntrySelection();
				RefreshFiles(pathChanged: false);
			}
		}

		public void OnItemSelected(FileBrowserItem item, bool isDoubleClick)
		{
			if (item == null)
			{
				return;
			}
			if (item is FileBrowserQuickLink)
			{
				CurrentPath = ((FileBrowserQuickLink)item).TargetPath;
				return;
			}
			if (m_multiSelectionToggleSelectionMode)
			{
				if (item.IsDirectory && m_pickerMode == PickMode.Files && !selectedFileEntries.Contains(item.Position))
				{
					return;
				}
				isDoubleClick = false;
			}
			if (!isDoubleClick)
			{
				if (!m_allowMultiSelection)
				{
					selectedFileEntries.Clear();
					selectedFileEntries.Add(item.Position);
				}
				else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
				{
					multiSelectionPivotFileEntry = Mathf.Clamp(multiSelectionPivotFileEntry, 0, validFileEntries.Count - 1);
					selectedFileEntries.Clear();
					for (int i = multiSelectionPivotFileEntry; i < item.Position; i++)
					{
						selectedFileEntries.Add(i);
					}
					for (int num = multiSelectionPivotFileEntry; num > item.Position; num--)
					{
						selectedFileEntries.Add(num);
					}
					selectedFileEntries.Add(item.Position);
				}
				else
				{
					multiSelectionPivotFileEntry = item.Position;
					if (m_multiSelectionToggleSelectionMode || IsCtrlKeyHeld())
					{
						if (!selectedFileEntries.Contains(item.Position))
						{
							selectedFileEntries.Add(item.Position);
						}
						else
						{
							selectedFileEntries.Remove(item.Position);
							if (selectedFileEntries.Count == 0)
							{
								MultiSelectionToggleSelectionMode = false;
							}
						}
					}
					else
					{
						selectedFileEntries.Clear();
						selectedFileEntries.Add(item.Position);
					}
				}
				UpdateFilenameInputFieldWithSelection();
			}
			for (int j = 0; j < allItems.Count; j++)
			{
				if (allItems[j].gameObject.activeSelf)
				{
					allItems[j].SetSelected(selectedFileEntries.Contains(allItems[j].Position));
				}
			}
			if (selectedFileEntries.Count > 0 && (isDoubleClick || (SingleClickMode && !m_multiSelectionToggleSelectionMode)))
			{
				if (!item.IsDirectory)
				{
					OnSubmitButtonClicked();
				}
				else
				{
					CurrentPath = Path.Combine(m_currentPath, item.Name);
				}
			}
		}

		public void OnItemHeld(FileBrowserItem item)
		{
			if (item is FileBrowserQuickLink)
			{
				OnItemSelected(item, isDoubleClick: false);
			}
			else
			{
				if (!m_allowMultiSelection || (item.IsDirectory && m_pickerMode == PickMode.Files))
				{
					return;
				}
				if (!MultiSelectionToggleSelectionMode)
				{
					if (m_pickerMode == PickMode.Files)
					{
						for (int num = selectedFileEntries.Count - 1; num >= 0; num--)
						{
							if (validFileEntries[selectedFileEntries[num]].IsDirectory)
							{
								selectedFileEntries.RemoveAt(num);
							}
						}
					}
					MultiSelectionToggleSelectionMode = true;
				}
				if (!selectedFileEntries.Contains(item.Position))
				{
					OnItemSelected(item, isDoubleClick: false);
				}
			}
		}

		private void OnFilenameInputChanged(string text)
		{
			filenameImage.color = m_skin.InputFieldNormalBackgroundColor;
		}

		private void ResetInputFieldTextPosition(TMP_InputField inputField)
		{
			inputField.textComponent.rectTransform.anchoredPosition = Vector2.zero;
			((RectTransform)inputField.textViewport.GetChild(0)).anchoredPosition = Vector2.zero;
		}

		public void Show(string initialPath, string initialFilename)
		{
			if (AskPermissions)
			{
				RequestPermissionAsync(delegate
				{
					ShowInternal(initialPath, initialFilename);
				});
			}
			else
			{
				ShowInternal(initialPath, initialFilename);
			}
		}

		private void ShowInternal(string initialPath, string initialFilename)
		{
			if (!quickLinksInitialized)
			{
				InitializeQuickLinks();
			}
			selectedFileEntries.Clear();
			m_multiSelectionToggleSelectionMode = false;
			m_searchString = string.Empty;
			searchInputField.text = m_searchString;
			filesScrollRect.verticalNormalizedPosition = 1f;
			IsOpen = true;
			Success = false;
			Result = null;
			base.gameObject.SetActive(value: true);
			CurrentPath = GetInitialPath(initialPath);
			filenameInputField.text = initialFilename ?? string.Empty;
			filenameInputField.interactable = true;
			filenameImage.color = m_skin.InputFieldNormalBackgroundColor;
		}

		public void Hide()
		{
			IsOpen = false;
			currentPathIndex = -1;
			pathsFollowed.Clear();
			backButton.interactable = false;
			forwardButton.interactable = false;
			upButton.interactable = false;
			base.gameObject.SetActive(value: false);
		}

		public void RefreshFiles(bool pathChanged)
		{
			bool allExtensionsHaveSingleSuffix = AllExtensionsHaveSingleSuffix;
			if (pathChanged)
			{
				if (!string.IsNullOrEmpty(m_currentPath))
				{
					allFileEntries = FileBrowserHelpers.GetEntriesInDirectory(m_currentPath, allExtensionsHaveSingleSuffix);
				}
				else
				{
					allFileEntries = null;
				}
			}
			selectedFileEntries.Clear();
			if (!showHiddenFilesToggle.isOn)
			{
				ignoredFileAttributes |= FileAttributes.Hidden;
			}
			else
			{
				ignoredFileAttributes &= ~FileAttributes.Hidden;
			}
			validFileEntries.Clear();
			if (allFileEntries != null)
			{
				if (sortFilesByName)
				{
					Array.Sort(allFileEntries, (FileSystemEntry entry1, FileSystemEntry entry2) => (entry1.IsDirectory != entry2.IsDirectory) ? ((!entry1.IsDirectory) ? 1 : (-1)) : entry1.Name.CompareTo(entry2.Name));
				}
				for (int num = 0; num < allFileEntries.Length; num++)
				{
					try
					{
						FileSystemEntry item = allFileEntries[num];
						if (FileSystemEntryMatchesFilters(in item, allExtensionsHaveSingleSuffix))
						{
							validFileEntries.Add(item);
						}
					}
					catch (Exception exception)
					{
						Debug.LogException(exception);
					}
				}
			}
			if (pendingFileEntrySelection.Count > 0)
			{
				for (int num2 = 0; num2 < pendingFileEntrySelection.Count; num2++)
				{
					string text = pendingFileEntrySelection[num2];
					for (int num3 = 0; num3 < validFileEntries.Count; num3++)
					{
						if (validFileEntries[num3].Name == text)
						{
							selectedFileEntries.Add(num3);
							break;
						}
					}
				}
				pendingFileEntrySelection.Clear();
			}
			if (!filenameInputField.interactable && selectedFileEntries.Count <= 1)
			{
				filenameInputField.interactable = true;
				if (selectedFileEntries.Count == 0)
				{
					filenameInputField.text = string.Empty;
				}
			}
			listView.UpdateList();
			EnsureScrollViewIsWithinBounds();
		}

		private bool FileSystemEntryMatchesFilters(in FileSystemEntry item, bool allExtensionsHaveSingleSuffix)
		{
			if ((item.Attributes & ignoredFileAttributes) != 0)
			{
				return false;
			}
			if (!item.IsDirectory)
			{
				if (m_pickerMode == PickMode.Folders)
				{
					return false;
				}
				string extension = item.Extension;
				if (excludedExtensionsSet.Contains(extension))
				{
					return false;
				}
				if (!allExtensionsHaveSingleSuffix)
				{
					for (int i = 0; i < excludedExtensions.Length; i++)
					{
						if (extension.EndsWith(excludedExtensions[i], StringComparison.Ordinal))
						{
							excludedExtensionsSet.Add(extension);
						}
					}
				}
				if (!filters[filtersDropdown.value].MatchesExtension(extension, !allExtensionsHaveSingleSuffix))
				{
					return false;
				}
			}
			if (m_searchString.Length > 0 && textComparer.IndexOf(item.Name, m_searchString, textCompareOptions) < 0)
			{
				return false;
			}
			if (m_displayedEntriesFilter != null && !m_displayedEntriesFilter(item))
			{
				return false;
			}
			return true;
		}

		public void SelectAllFiles()
		{
			if (!m_allowMultiSelection || validFileEntries.Count == 0)
			{
				return;
			}
			multiSelectionPivotFileEntry = 0;
			selectedFileEntries.Clear();
			if (m_pickerMode != PickMode.Files)
			{
				for (int i = 0; i < validFileEntries.Count; i++)
				{
					selectedFileEntries.Add(i);
				}
			}
			else
			{
				for (int j = 0; j < validFileEntries.Count; j++)
				{
					if (!m_multiSelectionToggleSelectionMode || !validFileEntries[j].IsDirectory)
					{
						selectedFileEntries.Add(j);
					}
				}
			}
			UpdateFilenameInputFieldWithSelection();
			listView.UpdateList();
		}

		public void DeselectAllFiles()
		{
			if (selectedFileEntries.Count != 0)
			{
				selectedFileEntries.Clear();
				MultiSelectionToggleSelectionMode = false;
				filenameInputField.text = string.Empty;
				filenameInputField.interactable = true;
				listView.UpdateList();
			}
		}

		public void CreateNewFolder()
		{
			StartCoroutine(CreateNewFolderCoroutine());
		}

		private IEnumerator CreateNewFolderCoroutine()
		{
			filesScrollRect.verticalNormalizedPosition = 1f;
			filesScrollRect.velocity = Vector2.zero;
			if (selectedFileEntries.Count > 0)
			{
				selectedFileEntries.Clear();
				MultiSelectionToggleSelectionMode = false;
				filenameInputField.text = string.Empty;
				filenameInputField.interactable = true;
				listView.UpdateList();
			}
			filesScrollRect.movementType = ScrollRect.MovementType.Unrestricted;
			yield return null;
			filesContainer.anchoredPosition = new Vector2(0f, 0f - m_skin.FileHeight);
			yield return null;
			filesContainer.anchoredPosition = new Vector2(0f, 0f - m_skin.FileHeight);
			((RectTransform)renameItem.transform).anchoredPosition = new Vector2(1f, m_skin.FileHeight);
			renameItem.Show(string.Empty, m_skin.FileSelectedBackgroundColor, m_skin.FolderIcon, delegate(string folderName)
			{
				filesScrollRect.movementType = ScrollRect.MovementType.Clamped;
				filesContainer.anchoredPosition = Vector2.zero;
				if (!string.IsNullOrEmpty(folderName))
				{
					FileBrowserHelpers.CreateFolderInDirectory(CurrentPath, folderName);
					pendingFileEntrySelection.Clear();
					pendingFileEntrySelection.Add(folderName);
					RefreshFiles(pathChanged: true);
					if (m_pickerMode != PickMode.Files)
					{
						filenameInputField.text = folderName;
					}
					int num = Mathf.Max(0, FilenameToFileEntryIndex(folderName));
					filesScrollRect.verticalNormalizedPosition = ((validFileEntries.Count > 1) ? (1f - (float)num / (float)(validFileEntries.Count - 1)) : 1f);
				}
			});
		}

		public void RenameSelectedFile()
		{
			if (!CanRenameFiles || selectedFileEntries.Count != 1)
			{
				return;
			}
			MultiSelectionToggleSelectionMode = false;
			int num = selectedFileEntries[0];
			FileSystemEntry fileInfo = validFileEntries[num];
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < allItems.Count; i++)
			{
				if (!allItems[i].gameObject.activeSelf)
				{
					continue;
				}
				if (allItems[i].Position == num - 1)
				{
					flag = true;
					if (flag && flag2)
					{
						break;
					}
				}
				else if (allItems[i].Position == num + 1)
				{
					flag2 = true;
					if (flag && flag2)
					{
						break;
					}
				}
			}
			if (!flag || !flag2)
			{
				filesScrollRect.verticalNormalizedPosition = ((validFileEntries.Count > 1) ? (1f - (float)num / (float)(validFileEntries.Count - 1)) : 1f);
			}
			filesScrollRect.velocity = Vector2.zero;
			((RectTransform)renameItem.transform).anchoredPosition = new Vector2(1f, (float)(-num) * m_skin.FileHeight);
			renameItem.Show(fileInfo.Name, m_skin.FileSelectedBackgroundColor, GetIconForFileEntry(in fileInfo), delegate(string newName)
			{
				if (!string.IsNullOrEmpty(newName) && !(newName == fileInfo.Name))
				{
					if (fileInfo.IsDirectory)
					{
						FileBrowserHelpers.RenameDirectory(fileInfo.Path, newName);
					}
					else
					{
						FileBrowserHelpers.RenameFile(fileInfo.Path, newName);
					}
					pendingFileEntrySelection.Clear();
					pendingFileEntrySelection.Add(newName);
					RefreshFiles(pathChanged: true);
					if ((fileInfo.IsDirectory && m_pickerMode != PickMode.Files) || (!fileInfo.IsDirectory && m_pickerMode != PickMode.Folders))
					{
						filenameInputField.text = newName;
					}
				}
			});
		}

		public void DeleteSelectedFiles()
		{
			if (!CanDeleteFiles || selectedFileEntries.Count == 0)
			{
				return;
			}
			selectedFileEntries.Sort();
			fileOperationConfirmationPanel.Show(this, validFileEntries, selectedFileEntries, FileBrowserFileOperationConfirmationPanel.OperationType.Delete, delegate
			{
				for (int num = selectedFileEntries.Count - 1; num >= 0; num--)
				{
					FileSystemEntry fileSystemEntry = validFileEntries[selectedFileEntries[num]];
					if (fileSystemEntry.IsDirectory)
					{
						FileBrowserHelpers.DeleteDirectory(fileSystemEntry.Path);
					}
					else
					{
						FileBrowserHelpers.DeleteFile(fileSystemEntry.Path);
					}
				}
				selectedFileEntries.Clear();
				MultiSelectionToggleSelectionMode = false;
				RefreshFiles(pathChanged: true);
			});
		}

		private void PersistFileEntrySelection()
		{
			pendingFileEntrySelection.Clear();
			for (int i = 0; i < selectedFileEntries.Count; i++)
			{
				pendingFileEntrySelection.Add(validFileEntries[selectedFileEntries[i]].Name);
			}
		}

		private bool AddQuickLink(Sprite icon, string name, string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return false;
			}
			if (!CheckDirectoryExistsWithTimeout(path))
			{
				return false;
			}
			path = GetPathWithoutTrailingDirectorySeparator(path.Trim());
			for (int i = 0; i < allQuickLinks.Count; i++)
			{
				if (allQuickLinks[i].TargetPath == path)
				{
					return false;
				}
			}
			FileBrowserQuickLink fileBrowserQuickLink = UnityEngine.Object.Instantiate(quickLinkPrefab, quickLinksContainer, worldPositionStays: false);
			fileBrowserQuickLink.SetFileBrowser(this, m_skin);
			if (icon != null)
			{
				fileBrowserQuickLink.SetQuickLink(icon, name, path);
			}
			else
			{
				fileBrowserQuickLink.SetQuickLink(m_skin.FolderIcon, name, path);
			}
			Vector2 anchoredPosition = new Vector2(0f, 0f - quickLinksContainer.sizeDelta.y);
			fileBrowserQuickLink.TransformComponent.anchoredPosition = anchoredPosition;
			anchoredPosition.y -= m_skin.FileHeight;
			quickLinksContainer.sizeDelta = new Vector2(0f, 0f - anchoredPosition.y);
			allQuickLinks.Add(fileBrowserQuickLink);
			return true;
		}

		private void ClearQuickLinksInternal()
		{
			Vector2 zero = Vector2.zero;
			for (int i = 0; i < allQuickLinks.Count; i++)
			{
				if (allQuickLinks[i].TargetPath == "SAF_PICK_FOLDER")
				{
					allQuickLinks[i].TransformComponent.anchoredPosition = zero;
					zero.y -= m_skin.FileHeight;
				}
				else
				{
					UnityEngine.Object.Destroy(allQuickLinks[i].gameObject);
					allQuickLinks.RemoveAt(i--);
				}
			}
			quickLinksContainer.sizeDelta = new Vector2(0f, 0f - zero.y);
			quickLinksInitialized = true;
			generateQuickLinksForDrives = false;
		}

		private void EnsureScrollViewIsWithinBounds()
		{
			if (filesScrollRect.verticalNormalizedPosition <= Mathf.Epsilon)
			{
				filesScrollRect.verticalNormalizedPosition = 0.0001f;
			}
			filesScrollRect.OnScroll(nullPointerEventData);
		}

		internal void EnsureWindowIsWithinBounds()
		{
			Vector2 sizeDelta = rectTransform.sizeDelta;
			Vector2 sizeDelta2 = windowTR.sizeDelta;
			if (sizeDelta2.x < (float)minWidth)
			{
				sizeDelta2.x = minWidth;
			}
			if (sizeDelta2.y < (float)minHeight)
			{
				sizeDelta2.y = minHeight;
			}
			if (sizeDelta2.x > sizeDelta.x)
			{
				sizeDelta2.x = sizeDelta.x;
			}
			if (sizeDelta2.y > sizeDelta.y)
			{
				sizeDelta2.y = sizeDelta.y;
			}
			Vector2 anchoredPosition = windowTR.anchoredPosition;
			Vector2 vector = sizeDelta * 0.5f;
			Vector2 vector2 = sizeDelta2 * 0.5f;
			Vector2 vector3 = anchoredPosition - vector2 + vector;
			Vector2 vector4 = anchoredPosition + vector2 + vector;
			if (vector3.x < 0f)
			{
				anchoredPosition.x -= vector3.x;
			}
			else if (vector4.x > sizeDelta.x)
			{
				anchoredPosition.x -= vector4.x - sizeDelta.x;
			}
			if (vector3.y < 0f)
			{
				anchoredPosition.y -= vector3.y;
			}
			else if (vector4.y > sizeDelta.y)
			{
				anchoredPosition.y -= vector4.y - sizeDelta.y;
			}
			windowTR.anchoredPosition = anchoredPosition;
			windowTR.sizeDelta = sizeDelta2;
		}

		internal void OnWindowDimensionsChanged(Vector2 size)
		{
			float x = size.x;
			float num = Mathf.Min(middleViewQuickLinksOriginalSize.x, x * quickLinksMaxWidthPercentage);
			if (middleViewQuickLinks.sizeDelta.x != num)
			{
				middleViewQuickLinks.sizeDelta = new Vector2(num, middleViewQuickLinksOriginalSize.y);
				middleViewFiles.anchoredPosition = new Vector2(num, 0f);
				middleViewFiles.sizeDelta = new Vector2(0f - num, middleViewQuickLinksOriginalSize.y);
				middleViewSeparator.rectTransform.anchoredPosition = new Vector2(num, 0f);
			}
			if (x >= narrowScreenWidth)
			{
				if (topViewNarrowScreen.gameObject.activeSelf)
				{
					topViewNarrowScreen.gameObject.SetActive(value: false);
					pathInputField.transform.SetParent(pathInputFieldSlotTop, worldPositionStays: false);
					showHiddenFilesToggle.gameObject.SetActive(m_displayHiddenFilesToggle);
					listView.OnViewportDimensionsChanged();
					EnsureScrollViewIsWithinBounds();
				}
			}
			else if (!topViewNarrowScreen.gameObject.activeSelf)
			{
				topViewNarrowScreen.gameObject.SetActive(value: true);
				pathInputField.transform.SetParent(pathInputFieldSlotBottom, worldPositionStays: false);
				showHiddenFilesToggle.gameObject.SetActive(value: false);
				listView.OnViewportDimensionsChanged();
				EnsureScrollViewIsWithinBounds();
			}
		}

		internal Sprite GetIconForFileEntry(in FileSystemEntry fileInfo)
		{
			return m_skin.GetIconForFileEntry(in fileInfo, !AllExtensionsHaveSingleSuffix);
		}

		internal static string GetExtensionFromFilename(string filename, bool extractOnlyLastSuffix)
		{
			int length = filename.Length;
			if (extractOnlyLastSuffix)
			{
				for (int num = length - 2; num >= 0; num--)
				{
					if (filename[num] == '.')
					{
						return filename.Substring(num, length - num).ToLowerInvariant();
					}
				}
			}
			else
			{
				int i = 0;
				for (int num2 = length - 2; i <= num2; i++)
				{
					if (filename[i] == '.')
					{
						return filename.Substring(i, length - i).ToLowerInvariant();
					}
				}
			}
			return string.Empty;
		}

		private string GetPathWithoutTrailingDirectorySeparator(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return null;
			}
			try
			{
				if (Path.GetDirectoryName(path) != null)
				{
					char c = path[path.Length - 1];
					if (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar)
					{
						path = path.Substring(0, path.Length - 1);
					}
				}
			}
			catch
			{
			}
			return path;
		}

		private void UpdateFilenameInputFieldWithSelection()
		{
			int num = 0;
			if (m_pickerMode != PickMode.Files)
			{
				num = selectedFileEntries.Count;
			}
			else
			{
				for (int i = 0; i < selectedFileEntries.Count; i++)
				{
					if (!validFileEntries[selectedFileEntries[i]].IsDirectory)
					{
						num++;
						if (num >= 7)
						{
							break;
						}
					}
				}
			}
			filenameInputField.interactable = selectedFileEntries.Count <= 1;
			if (num == 0)
			{
				if (filenameInputField.text.StartsWith("\""))
				{
					filenameInputField.text = string.Empty;
				}
				return;
			}
			if (num > 1)
			{
				if (multiSelectionFilenameBuilder == null)
				{
					multiSelectionFilenameBuilder = new StringBuilder(75);
				}
				else
				{
					multiSelectionFilenameBuilder.Length = 0;
				}
			}
			int j = 0;
			int num2 = 0;
			for (; j < selectedFileEntries.Count; j++)
			{
				FileSystemEntry fileSystemEntry = validFileEntries[selectedFileEntries[j]];
				if (m_pickerMode != PickMode.Files || !fileSystemEntry.IsDirectory)
				{
					if (num == 1)
					{
						filenameInputField.text = fileSystemEntry.Name;
						break;
					}
					multiSelectionFilenameBuilder.Append("\"").Append(fileSystemEntry.Name).Append("\" ");
					if (++num2 >= 7)
					{
						multiSelectionFilenameBuilder.Append("...");
						break;
					}
				}
			}
			if (num > 1)
			{
				filenameInputField.text = multiSelectionFilenameBuilder.ToString();
			}
		}

		private int ExtractFilenameFromInput(string input, ref int startIndex, out int nextStartIndex)
		{
			if (!m_allowMultiSelection || input[startIndex] != '"')
			{
				nextStartIndex = input.Length;
				return input.Length - startIndex;
			}
			if (startIndex + 1 >= input.Length)
			{
				nextStartIndex = input.Length;
				return 1;
			}
			int num = input.IndexOf('"', startIndex + 1);
			while (true)
			{
				if (num == -1)
				{
					nextStartIndex = input.Length;
					return input.Length - startIndex;
				}
				if (num == input.Length - 1 || input[num + 1] == ' ')
				{
					break;
				}
				num = input.IndexOf('"', num + 1);
			}
			startIndex++;
			nextStartIndex = num + 1;
			while (nextStartIndex < input.Length && input[nextStartIndex] == ' ')
			{
				nextStartIndex++;
			}
			return num - startIndex;
		}

		private int FilenameToFileEntryIndex(string filename)
		{
			int result = -1;
			for (int i = 0; i < validFileEntries.Count; i++)
			{
				if (validFileEntries[i].Name.Length == filename.Length)
				{
					if (filename == validFileEntries[i].Name)
					{
						return i;
					}
					if (textComparer.Compare(filename, validFileEntries[i].Name, textCompareOptions) == 0)
					{
						result = i;
					}
				}
			}
			return result;
		}

		private bool VerifyFilename(string filename)
		{
			bool flag = true;
			foreach (char c in filename)
			{
				if (invalidFilenameChars.Contains(c))
				{
					return false;
				}
				if (flag && !char.IsWhiteSpace(c))
				{
					flag = false;
				}
			}
			return !flag;
		}

		private void RefreshFiltersDropdownWidth()
		{
			filtersDropdownContainer.gameObject.SetActive(value: true);
			float num = 0f;
			for (int i = 0; i < filterLabels.Count; i++)
			{
				filterItemTemplate.text = filterLabels[i];
				num = Mathf.Max(num, filterItemTemplate.preferredWidth);
			}
			Vector2 sizeDelta = filtersDropdownContainer.sizeDelta;
			sizeDelta.x = Mathf.Max(((RectTransform)filtersDropdown.transform).sizeDelta.x, num + 35f);
			filtersDropdownContainer.sizeDelta = sizeDelta;
			filtersDropdownContainer.gameObject.SetActive(value: false);
		}

		private string GetInitialPath(string initialPath)
		{
			if (!string.IsNullOrEmpty(initialPath) && !FileBrowserHelpers.DirectoryExists(initialPath) && FileBrowserHelpers.FileExists(initialPath))
			{
				initialPath = FileBrowserHelpers.GetDirectoryName(initialPath);
			}
			if (string.IsNullOrEmpty(initialPath) || !FileBrowserHelpers.DirectoryExists(initialPath))
			{
				if (CurrentPath.Length > 0)
				{
					initialPath = CurrentPath;
				}
				else
				{
					string lastBrowsedFolder = LastBrowsedFolder;
					initialPath = ((string.IsNullOrEmpty(lastBrowsedFolder) || !FileBrowserHelpers.DirectoryExists(lastBrowsedFolder)) ? defaultInitialPath : lastBrowsedFolder);
				}
			}
			m_currentPath = string.Empty;
			return initialPath;
		}

		private bool CheckDirectoryExistsWithTimeout(string path, int timeout = 750)
		{
			if (timedOutDirectoryExistsRequests.Contains(path))
			{
				return false;
			}
			bool directoryExists = false;
			try
			{
				Task task = new Task(delegate
				{
					directoryExists = Directory.Exists(path);
				});
				task.Start();
				if (!task.Wait(timeout))
				{
					timedOutDirectoryExistsRequests.Add(path);
				}
			}
			catch
			{
				directoryExists = Directory.Exists(path);
			}
			return directoryExists;
		}

		private bool CheckDirectoryWriteAccess(string path)
		{
			string path2 = Path.Combine(path, "__fsWrite.tmp");
			try
			{
				File.Create(path2).Close();
				File.Delete(path2);
				return true;
			}
			catch
			{
				return false;
			}
			finally
			{
				try
				{
					File.Delete(path2);
				}
				catch
				{
				}
			}
		}

		private bool IsCtrlKeyHeld()
		{
			if (!Input.GetKey(KeyCode.LeftControl))
			{
				return Input.GetKey(KeyCode.RightControl);
			}
			return true;
		}

		public static bool ShowSaveDialog(OnSuccess onSuccess, OnCancel onCancel, PickMode pickMode, bool allowMultiSelection = false, string initialPath = null, string initialFilename = null, string title = "Save", string saveButtonText = "Save")
		{
			return ShowDialogInternal(onSuccess, onCancel, pickMode, allowMultiSelection, pickMode != PickMode.Folders, initialPath, initialFilename, title, saveButtonText);
		}

		public static bool ShowLoadDialog(OnSuccess onSuccess, OnCancel onCancel, PickMode pickMode, bool allowMultiSelection = false, string initialPath = null, string initialFilename = null, string title = "Load", string loadButtonText = "Select")
		{
			return ShowDialogInternal(onSuccess, onCancel, pickMode, allowMultiSelection, acceptNonExistingFilename: false, initialPath, initialFilename, title, loadButtonText);
		}

		private static bool ShowDialogInternal(OnSuccess onSuccess, OnCancel onCancel, PickMode pickMode, bool allowMultiSelection, bool acceptNonExistingFilename, string initialPath, string initialFilename, string title, string submitButtonText)
		{
			Instance.onSuccess = onSuccess;
			Instance.onCancel = onCancel;
			Instance.PickerMode = pickMode;
			Instance.AllowMultiSelection = allowMultiSelection;
			Instance.Title = title;
			Instance.SubmitButtonText = submitButtonText;
			Instance.AcceptNonExistingFilename = acceptNonExistingFilename;
			Instance.Show(initialPath, initialFilename);
			return true;
		}

		public static void HideDialog(bool invokeCancelCallback = false)
		{
			Instance.OnOperationCanceled(invokeCancelCallback);
		}

		public static IEnumerator WaitForSaveDialog(PickMode pickMode, bool allowMultiSelection = false, string initialPath = null, string initialFilename = null, string title = "Save", string saveButtonText = "Save")
		{
			bool? result = null;
			if (ShowSaveDialog(delegate
			{
				result = true;
			}, delegate
			{
				result = false;
			}, pickMode, allowMultiSelection, initialPath, initialFilename, title, saveButtonText))
			{
				while (!result.HasValue)
				{
					yield return null;
				}
			}
		}

		public static IEnumerator WaitForLoadDialog(PickMode pickMode, bool allowMultiSelection = false, string initialPath = null, string initialFilename = null, string title = "Load", string loadButtonText = "Select")
		{
			bool? result = null;
			if (ShowLoadDialog(delegate
			{
				result = true;
			}, delegate
			{
				result = false;
			}, pickMode, allowMultiSelection, initialPath, initialFilename, title, loadButtonText))
			{
				while (!result.HasValue)
				{
					yield return null;
				}
			}
		}

		public static bool AddQuickLink(string name, string path, Sprite icon = null)
		{
			if (string.IsNullOrEmpty(path) || !FileBrowserHelpers.DirectoryExists(path))
			{
				return false;
			}
			return Instance.AddQuickLink(icon, name, path);
		}

		public static void ClearQuickLinks()
		{
			Instance.ClearQuickLinksInternal();
		}

		public static void SetExcludedExtensions(params string[] excludedExtensions)
		{
			Instance.excludedExtensions = excludedExtensions ?? new string[0];
			Instance.excludedExtensionsSet.Clear();
			Instance.allExcludedExtensionsHaveSingleSuffix = true;
			if (excludedExtensions == null)
			{
				return;
			}
			for (int i = 0; i < excludedExtensions.Length; i++)
			{
				excludedExtensions[i] = excludedExtensions[i].ToLowerInvariant();
				if (excludedExtensions[i][0] != '.')
				{
					excludedExtensions[i] = "." + excludedExtensions[i];
				}
				Instance.excludedExtensionsSet.Add(excludedExtensions[i]);
				Instance.allExcludedExtensionsHaveSingleSuffix &= excludedExtensions[i].LastIndexOf('.') == 0;
			}
		}

		public static void SetFilters(bool showAllFilesFilter)
		{
			SetFilters(showAllFilesFilter, (string[])null);
		}

		public static void SetFilters(bool showAllFilesFilter, IEnumerable<string> filters)
		{
			SetFiltersPreProcessing(showAllFilesFilter);
			if (filters != null)
			{
				foreach (string filter in filters)
				{
					if (!string.IsNullOrEmpty(filter))
					{
						Instance.filters.Add(new Filter(null, filter));
					}
				}
			}
			SetFiltersPostProcessing();
		}

		public static void SetFilters(bool showAllFilesFilter, params string[] filters)
		{
			SetFiltersPreProcessing(showAllFilesFilter);
			if (filters != null)
			{
				for (int i = 0; i < filters.Length; i++)
				{
					if (!string.IsNullOrEmpty(filters[i]))
					{
						Instance.filters.Add(new Filter(null, filters[i]));
					}
				}
			}
			SetFiltersPostProcessing();
		}

		public static void SetFilters(bool showAllFilesFilter, IEnumerable<Filter> filters)
		{
			SetFiltersPreProcessing(showAllFilesFilter);
			if (filters != null)
			{
				foreach (Filter filter in filters)
				{
					if (filter != null && filter.defaultExtension.Length > 0)
					{
						Instance.filters.Add(filter);
					}
				}
			}
			SetFiltersPostProcessing();
		}

		public static void SetFilters(bool showAllFilesFilter, params Filter[] filters)
		{
			SetFiltersPreProcessing(showAllFilesFilter);
			if (filters != null)
			{
				for (int i = 0; i < filters.Length; i++)
				{
					if (filters[i] != null && filters[i].defaultExtension.Length > 0)
					{
						Instance.filters.Add(filters[i]);
					}
				}
			}
			SetFiltersPostProcessing();
		}

		private static void SetFiltersPreProcessing(bool showAllFilesFilter)
		{
			Instance.showAllFilesFilter = showAllFilesFilter;
			Instance.filters.Clear();
			if (showAllFilesFilter)
			{
				Instance.filters.Add(Instance.allFilesFilter);
			}
		}

		private static void SetFiltersPostProcessing()
		{
			List<Filter> list = Instance.filters;
			if (list.Count == 0)
			{
				list.Add(Instance.allFilesFilter);
			}
			Instance.filterLabels.Clear();
			for (int i = 0; i < list.Count; i++)
			{
				Instance.filterLabels.Add(list[i].ToString());
			}
			Instance.RefreshFiltersDropdownWidth();
			Instance.filtersDropdown.ClearOptions();
			Instance.filtersDropdown.AddOptions(Instance.filterLabels);
			Instance.filtersDropdown.value = 0;
			Instance.allFiltersHaveSingleSuffix = list[0].allExtensionsHaveSingleSuffix;
		}

		public static bool SetDefaultFilter(string defaultFilter)
		{
			if (string.IsNullOrEmpty(defaultFilter))
			{
				if (Instance.showAllFilesFilter)
				{
					Instance.filtersDropdown.value = 0;
					Instance.filtersDropdown.RefreshShownValue();
					return true;
				}
				return false;
			}
			defaultFilter = defaultFilter.ToLowerInvariant();
			if (defaultFilter[0] != '.')
			{
				defaultFilter = "." + defaultFilter;
			}
			for (int i = 0; i < Instance.filters.Count; i++)
			{
				HashSet<string> extensionsSet = Instance.filters[i].extensionsSet;
				if (extensionsSet != null && extensionsSet.Contains(defaultFilter))
				{
					Instance.filtersDropdown.value = i;
					Instance.filtersDropdown.RefreshShownValue();
					return true;
				}
			}
			return false;
		}

		public static bool CheckPermission()
		{
			return true;
		}

		public static void RequestPermissionAsync(PermissionCallback callback)
		{
			callback(Permission.Granted);
		}
	}
}
