using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using InternalModding.Blocks;
using InternalModding.LevelEntities;
using InternalModding.Misc;
using InternalModding.Mods;
using InternalModding.Workshop;
using Localisation;
using Modding;
using Selectors;
using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InternalModding.UI
{
	public class ModListUI : MonoBehaviour
	{
		private enum Tab
		{
			Mods = 0,
			Blocks = 1,
			Entities = 2
		}

		public GameObject ModEntryTemplate;

		public GameObject BlockEntryTemplate;

		public GameObject EntityEntryTemplate;

		public GameObject InterfaceParent;

		public GameObject ModsContent;

		public GameObject BlocksContent;

		public GameObject EntitiesContent;

		public float EntryYOffset;

		public UIButtonExtended ModsButton;

		public UIButtonExtended BlocksButton;

		public UIButtonExtended EntitiesButton;

		public ToggleWorkshopButton WorkshopButton;

		public UIButton CloseButton;

		public UIScrollbar Scrollbar;

		public TextHolder SearchField;

		public UploadDialog UploadDialog;

		public FileBrowserView FileBrowser;

		public MeshRenderer BG;

		public Material titleScreenBGMat;

		public Material defaultBGMat;

		public UIButton openModList;

		public DynamicText modCount;

		private List<ModContainer> displayedMods = new List<ModContainer>();

		private List<ModContainer> allMods = new List<ModContainer>();

		private List<ModListEntry> modEntries;

		private List<BlockListEntry> blockEntries;

		private List<EntityListEntry> entityEntries;

		private Vector3 nextEntryPosMods;

		private Vector3 nextEntryPosBlocks;

		private Vector3 nextEntryPosEntries;

		private bool isInitialized;

		private Tab currentTab;

		public void Awake()
		{
			if (SceneManager.GetActiveScene().name == "TITLE SCREEN")
			{
				BG.material = titleScreenBGMat;
			}
			else
			{
				BG.material = defaultBGMat;
			}
			modEntries = new List<ModListEntry>();
			blockEntries = new List<BlockListEntry>();
			entityEntries = new List<EntityListEntry>();
			nextEntryPosMods = ModEntryTemplate.transform.localPosition;
			nextEntryPosBlocks = BlockEntryTemplate.transform.localPosition;
			nextEntryPosEntries = EntityEntryTemplate.transform.localPosition;
			if (openModList != null)
			{
				openModList.Click += Show;
			}
			ModsButton.Click += delegate
			{
				OpenTab(Tab.Mods);
			};
			BlocksButton.Click += delegate
			{
				OpenTab(Tab.Blocks);
			};
			EntitiesButton.Click += delegate
			{
				OpenTab(Tab.Entities);
			};
			ToggleWorkshopButton workshopButton = WorkshopButton;
			workshopButton.OpenWorkshopButtonClicked = (Action<WorkshopType>)Delegate.Combine(workshopButton.OpenWorkshopButtonClicked, new Action<WorkshopType>(OnOpenWorkshopButtonClicked));
			CloseButton.Click += Toggle;
			SearchField.TextChanged += delegate
			{
				RecreateList();
			};
			isInitialized = true;
			if (SingleInstanceFindOnly<ModManager>.hasInstance())
			{
				InterfaceParent.SetActive(false);
			}
			else
			{
				GenerateTestEntries();
			}
		}

		protected void OnEnable()
		{
			ModManager.OnModLoad += OnModLoad;
			if (isInitialized)
			{
				RefreshMods();
			}
		}

		protected void OnDisable()
		{
			ModManager.OnModLoad -= OnModLoad;
		}

		private void OnOpenWorkshopButtonClicked(WorkshopType workshopType)
		{
			HandleClickSteam();
		}

		private void HandleClickSteam()
		{
			if (SteamManager.Initialized)
			{
				string pchURL = "http://steamcommunity.com/workshop/browse/?appid=346010&requiredtags[]=Mods";
				SteamFriends.ActivateGameOverlayToWebPage(pchURL);
			}
		}

		public void OnDestroy()
		{
			ToggleWorkshopButton workshopButton = WorkshopButton;
			workshopButton.OpenWorkshopButtonClicked = (Action<WorkshopType>)Delegate.Remove(workshopButton.OpenWorkshopButtonClicked, new Action<WorkshopType>(OnOpenWorkshopButtonClicked));
			ModManager.OnModLoad -= OnModLoad;
		}

		private void OpenTab(Tab tab)
		{
			if (currentTab != tab)
			{
				ModsButton.ToggleBG(tab == Tab.Mods);
				ModsContent.SetActive(tab == Tab.Mods);
				BlocksButton.ToggleBG(tab == Tab.Blocks);
				BlocksContent.SetActive(tab == Tab.Blocks);
				EntitiesButton.ToggleBG(tab == Tab.Entities);
				EntitiesContent.SetActive(tab == Tab.Entities);
				GameObject gameObject;
				switch (tab)
				{
				case Tab.Mods:
					gameObject = ModsContent;
					break;
				case Tab.Blocks:
					gameObject = BlocksContent;
					break;
				case Tab.Entities:
					gameObject = EntitiesContent;
					break;
				default:
					throw new Exception("Not a valid tab: " + tab);
				}
				Scrollbar.contentParent = gameObject.transform;
				Scrollbar.UpdateBounds();
				currentTab = tab;
			}
		}

		private void UpdateDisplayedMods()
		{
			displayedMods.Clear();
			string text = SearchField.ValueText;
			if (text == string.Empty)
			{
				displayedMods.AddRange(allMods);
				return;
			}
			Predicate<string> filterText = (string input) => CultureInfo.InvariantCulture.CompareInfo.IndexOf(input, text, CompareOptions.IgnoreCase) >= 0;
			displayedMods.AddRange(allMods.Where((ModContainer mod) => filterText(mod.Info.Name) || filterText(mod.Info.Author) || filterText(mod.Info.Description) || mod.Blocks.Any((ModdedBlock block) => filterText(block.Name)) || mod.Entities.Any((ModdedEntity entity) => filterText(entity.Name))));
		}

		public void AddMod(ModContainer mod)
		{
			allMods.Add(mod);
		}

		private void OnModLoad(ModContainer mod)
		{
			RefreshMods();
		}

		public void RefreshMods()
		{
			allMods.Clear();
			if (SingleInstanceFindOnly<ModManager>.hasInstance())
			{
				allMods.AddRange(ModManager.Mods);
			}
			RecreateList();
			int num = 0;
			foreach (ModContainer mod in ModManager.Mods)
			{
				if (mod.IsEnabled || mod.IsActive)
				{
					num++;
				}
			}
			string text = num.ToString();
			text = ((!(text == "0")) ? text.Replace("0", "⁰").Replace("1", "¹").Replace("2", "²")
				.Replace("3", "³")
				.Replace("4", "⁴")
				.Replace("5", "⁵")
				.Replace("6", "⁶")
				.Replace("7", "⁷")
				.Replace("8", "⁸")
				.Replace("9", "⁹") : string.Empty);
			ReferenceMaster.SetDynamicText(modCount, LocalisationManager.GetTranslation(3560) + text);
			modCount.GetComponent<AlignUI>().Align();
		}

		private void RecreateList()
		{
			foreach (ModListEntry modEntry in modEntries)
			{
				UnityEngine.Object.DestroyImmediate(modEntry.gameObject);
			}
			modEntries.Clear();
			foreach (BlockListEntry blockEntry in blockEntries)
			{
				UnityEngine.Object.DestroyImmediate(blockEntry.gameObject);
			}
			blockEntries.Clear();
			foreach (EntityListEntry entityEntry in entityEntries)
			{
				UnityEngine.Object.DestroyImmediate(entityEntry.gameObject);
			}
			entityEntries.Clear();
			nextEntryPosMods = ModEntryTemplate.transform.localPosition;
			nextEntryPosBlocks = BlockEntryTemplate.transform.localPosition;
			nextEntryPosEntries = EntityEntryTemplate.transform.localPosition;
			UpdateDisplayedMods();
			foreach (ModContainer displayedMod in displayedMods)
			{
				CreateEntries(displayedMod);
			}
			Scrollbar.UpdateBounds();
		}

		private void GenerateTestEntries()
		{
			for (int i = 0; i < 15; i++)
			{
				CreateEntries(null);
			}
			Scrollbar.UpdateBounds();
		}

		private void CreateEntries(ModContainer mod)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(ModEntryTemplate, ModsContent.transform);
			ModListEntry component = gameObject.GetComponent<ModListEntry>();
			component.List = this;
			if (mod != null)
			{
				component.SetMod(mod);
			}
			gameObject.transform.localPosition = nextEntryPosMods;
			nextEntryPosMods.y -= EntryYOffset;
			gameObject.SetActive(true);
			modEntries.Add(component);
			if (mod == null)
			{
				return;
			}
			foreach (ModdedBlock block in mod.Blocks)
			{
				GameObject gameObject2 = (GameObject)UnityEngine.Object.Instantiate(BlockEntryTemplate, BlocksContent.transform);
				BlockListEntry component2 = gameObject2.GetComponent<BlockListEntry>();
				component2.SetBlock(this, block);
				gameObject2.transform.localPosition = nextEntryPosBlocks;
				nextEntryPosBlocks.y -= EntryYOffset;
				gameObject2.SetActive(true);
				blockEntries.Add(component2);
			}
			foreach (ModdedEntity entity in mod.Entities)
			{
				GameObject gameObject3 = (GameObject)UnityEngine.Object.Instantiate(EntityEntryTemplate, EntitiesContent.transform);
				EntityListEntry component3 = gameObject3.GetComponent<EntityListEntry>();
				component3.SetEntity(this, entity);
				gameObject3.transform.localPosition = nextEntryPosEntries;
				nextEntryPosEntries.y -= EntryYOffset;
				gameObject3.SetActive(true);
				entityEntries.Add(component3);
			}
		}

		public void MoveTo(ModContainer mod)
		{
			OpenTab(Tab.Mods);
			ModListEntry modListEntry = modEntries.FirstOrDefault((ModListEntry e) => e.Mod == mod);
			if (modListEntry != null)
			{
				Scrollbar.ScrollToElement(modListEntry.transform);
			}
		}

		private UploadData CreateUploadData(ModContainer mod)
		{
			string thumbnailPath = GetThumbnailPath(mod);
			UploadData uploadData = new UploadData();
			uploadData.Name = mod.Info.Name;
			uploadData.Path = mod.Info.Directory;
			uploadData.IsFolder = true;
			uploadData.ThumbnailPath = thumbnailPath;
			uploadData.ItemType = WorkshopManager.ItemTypes.Mods;
			return uploadData;
		}

		public void OpenWorkshopInterface(ModContainer mod)
		{
			UploadDialog uploadDialog = (UploadDialog)UnityEngine.Object.Instantiate(UploadDialog, InterfaceParent.transform);
			uploadDialog.transform.position = UploadDialog.transform.position;
			uploadDialog.transform.rotation = UploadDialog.transform.rotation;
			ModTexture modTexture = mod.Info.WorkshopThumbnail;
			if (modTexture == null)
			{
				modTexture = mod.Info.Icon;
			}
			uploadDialog.gameObject.SetActive(true);
			UploadData uploadData = CreateUploadData(mod);
			uploadDialog.Initialize(UploadDialog.UploadDialogMode.NewUploadOrModify, uploadData, (Texture2D)modTexture);
			SetupTags(mod, uploadDialog);
			uploadDialog.ModifyClicked = delegate(UploadDialog.UploadDialogMode uploadType, UploadData data)
			{
				ReferenceMaster.UIActive = ReferenceMaster.WorkshopItemType.Mods;
				data.UploadThumbnail = true;
				data.UploadContent = true;
				FileBrowser.SetUploadData(data);
				FileBrowser.Open(FileBrowserType.PublishedSteamMods, false);
			};
			uploadDialog.UploadClicked = delegate(UploadDialog.UploadDialogMode uploadType, UploadData data)
			{
				if (mod.Info.DebugEnabled)
				{
					MLog.Error("Can't upload mod with Debug enabled.");
				}
				else
				{
					ModWorkshopManager.Upload(data);
				}
			};
		}

		private string GetThumbnailPath(ModContainer mod)
		{
			if (mod.Info.WorkshopThumbnail != null)
			{
				return mod.Info.WorkshopThumbnail.Info.Path;
			}
			if (mod.Info.Icon.Info != null)
			{
				return mod.Info.Icon.Info.Path;
			}
			return null;
		}

		private void SetupTags(ModContainer mod, UploadDialog dialog)
		{
			List<int> list = new List<int>();
			ModInfo info = mod.Info;
			if (info.Blocks.Any())
			{
				list.Add(0);
			}
			if (info.Entities.Any())
			{
				list.Add(1);
			}
			if (info.Triggers.Any() || info.Events.Any())
			{
				list.Add(2);
			}
			if (info.MultiplayerCompatible)
			{
				list.Add(8);
			}
			dialog.SetTags(list);
		}

		public void Show()
		{
			InterfaceParent.SetActive(true);
			if (allMods.Count == 0)
			{
				RefreshMods();
			}
		}

		public void Hide()
		{
			InterfaceParent.SetActive(false);
		}

		public void Toggle()
		{
			if (InterfaceParent.activeSelf)
			{
				Hide();
			}
			else
			{
				Show();
			}
		}
	}
}
