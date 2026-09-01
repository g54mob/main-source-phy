using System.Collections;
using System.Collections.Generic;
using DM;
using GamepadUI.StateManager.Core;
using Landfall.TABS.GameMode;
using Landfall.TABS.Workshop;
using LevelCreator;
using TFBGames;
using TMPro;
using UIStateManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class CustomContentSideBar : MonoBehaviour
	{
		public FactionCreatorFadeBG fade;

		public CodeAnimation factionSideBarAnimation;

		public FactionCreatorManager factionCreator;

		public CustomContentPopup customPopup;

		public InterfaceStateManager interfaceManager;

		[Header("Buttons")]
		public GameObject playButton;

		public GameObject editButton;

		public GameObject renameButton;

		public GameObject uploadButton;

		public GameObject deleteButton;

		public GameObject playButtonGlyph;

		public GameObject editButtonGlyph;

		public GameObject renameButtonGlyph;

		public GameObject uploadButtonGlyph;

		public GameObject deleteButtonGlyph;

		[Header("Parents")]
		public GameObject factionParent;

		public GameObject unitParent;

		public GameObject battleParent;

		public GameObject campaignParent;

		public GameObject levelParent;

		public TextMeshProUGUI FactionName;

		public TextMeshProUGUI FactionDescription;

		public TextMeshProUGUI FactionUnitCount;

		public Image FactionIcon;

		public Image FactionColor;

		public GameObject unitButton;

		public Transform unitsGrid;

		public SpriteAtlas factionAtlas;

		public TextMeshProUGUI UnitName;

		public Image UnitIcon;

		public TextMeshProUGUI UnitCost;

		public TextMeshProUGUI UserName;

		public TextMeshProUGUI UnitDescription;

		public TextMeshProUGUI BattleName;

		public Image BattleIcon;

		public TextMeshProUGUI BattleDescription;

		public TextMeshProUGUI CampaignName;

		public Image CampaignIcon;

		public TextMeshProUGUI CampaignDescription;

		public GameObject battleButton;

		public Transform battlesGrid;

		public TMP_Text battleCount;

		public TextMeshProUGUI LevelName;

		public Image LevelIcon;

		public MapAsset LevelMapAsset;

		[Header("Loading Icon")]
		[SerializeField]
		private GameObject loadingIconParent;

		private Faction loadedFaction;

		private UnitBlueprint loadedUnit;

		private TABSCampaignLevelAsset loadedBattle;

		private TABSCampaignAsset loadedCampaign;

		private CustomMap loadedCustomMap;

		private List<GameObject> objectsToClear = new List<GameObject>();

		private GameObject browserSelection;

		private CanvasGroup canvasGroup;

		private SettingsProfileManager m_SettingsProfileManager;

		private ModalPanel m_modalPanel;

		private FileIOWrapper m_FileIO;

		private string warningHeader = "POPUP_UNIT_LIMIT_TITLE";

		private string unitLimitWarning = "POPUP_UNIT_LIMIT_DESC";

		private string couldNotLoadMapError = Localizer.GetSinglePhrase("POPUP_LOADERROR");

		private PermissionsHelper permissionsHelper;

		private int cancelPermissionsCheckCount;

		private int showLoadingIconCount;

		public bool isOpen
		{
			get
			{
				if (canvasGroup != null)
				{
					return canvasGroup.interactable;
				}
				return false;
			}
		}

		public bool isLoadingIconVisible
		{
			get
			{
				if (loadingIconParent != null)
				{
					return loadingIconParent.activeSelf;
				}
				return false;
			}
		}

		private void Awake()
		{
			canvasGroup = GetComponent<CanvasGroup>();
			m_SettingsProfileManager = ServiceLocator.GetService<SettingsProfileManager>();
			m_modalPanel = ServiceLocator.GetService<ModalPanel>();
			m_FileIO = ServiceLocator.GetService<FileIOWrapper>();
			permissionsHelper = ServiceLocator.GetService<PermissionsHelper>();
		}

		private void OnDestroy()
		{
			cancelPermissionsCheckCount++;
		}

		public void ShowUnit(UnitBlueprint unit, bool playAnimation = true)
		{
			if (playAnimation)
			{
				OpenFactionPreview();
			}
			campaignParent.SetActive(value: false);
			battleParent.SetActive(value: false);
			factionParent.SetActive(value: false);
			unitParent.SetActive(value: true);
			levelParent.SetActive(value: false);
			HideLoadingIcon();
			UnitName.text = unit.Name;
			UnitCost.text = unit.GetUnitCost().ToString();
			UnitDescription.text = unit.UnitDescription;
			unit.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
			{
				if (sprite != null && UnitIcon != null)
				{
					UnitIcon.sprite = sprite;
				}
			});
			loadedUnit = unit;
			loadedFaction = null;
			loadedBattle = null;
			loadedCampaign = null;
			loadedCustomMap = null;
			UpdatePlayButton();
			UpdateLocalButtons();
		}

		public void OpenFactionPreview()
		{
			if (!isOpen)
			{
				browserSelection = EventSystem.current.currentSelectedGameObject;
				base.gameObject.SetActive(value: true);
				fade.SetOn();
				GetComponentInParent<UIComponentMainMenu>().OpenSubMenu(GetComponent<UISubMenu>());
			}
		}

		public void CloseFactionPreview()
		{
			cancelPermissionsCheckCount++;
			HideLoadingIcon();
			fade.SetOff();
			GetComponentInParent<UIComponentMainMenu>().OnBackPressed(0);
			if (browserSelection == null)
			{
				GetComponentInParent<UnitCreatorFactionBrowser>().FocusSelection();
			}
			else
			{
				EventSystem.current.SetSelectedGameObject(browserSelection);
			}
		}

		public void ShowFaction(Faction faction, bool playAnimation = true)
		{
			if (playAnimation)
			{
				OpenFactionPreview();
			}
			for (int i = 0; i < objectsToClear.Count; i++)
			{
				Object.Destroy(objectsToClear[i]);
			}
			objectsToClear.Clear();
			campaignParent.SetActive(value: false);
			battleParent.SetActive(value: false);
			factionParent.SetActive(value: true);
			unitParent.SetActive(value: false);
			levelParent.SetActive(value: false);
			HideLoadingIcon();
			FactionName.text = faction.Entity.Name;
			FactionDescription.text = "";
			FactionUnitCount.text = faction.Units.Length.ToString();
			FactionColor.color = faction.m_FactionColor;
			faction.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
			{
				if (sprite != null && FactionIcon != null)
				{
					FactionIcon.sprite = sprite;
				}
			});
			for (int num = 0; num < faction.Units.Length; num++)
			{
				UnitButtonBase unitButtonBase = Object.Instantiate(unitButton, unitsGrid).GetComponent<UnitButtonBase>().Setup(faction.Units[num]);
				unitButtonBase.gameObject.AddComponent<Selectable>();
				objectsToClear.Add(unitButtonBase.gameObject);
			}
			if (objectsToClear.Count > 0)
			{
				objectsToClear[0].GetComponent<Selectable>().Select();
			}
			loadedFaction = faction;
			loadedUnit = null;
			loadedBattle = null;
			loadedCampaign = null;
			loadedCustomMap = null;
			UpdatePlayButton();
			UpdateLocalButtons();
		}

		public void ShowBattle(TABSCampaignLevelAsset battle, bool playAnimation = true)
		{
			if (playAnimation)
			{
				OpenFactionPreview();
			}
			campaignParent.SetActive(value: false);
			battleParent.SetActive(value: true);
			factionParent.SetActive(value: false);
			unitParent.SetActive(value: false);
			levelParent.SetActive(value: false);
			HideLoadingIcon();
			BattleName.text = battle.Entity.Name;
			BattleDescription.text = battle.CampaignInfo.Description;
			CampaignHandler.GetBattleSprite(battle, delegate(Sprite sprite)
			{
				if (BattleIcon != null)
				{
					BattleIcon.sprite = sprite;
				}
			});
			loadedBattle = battle;
			loadedFaction = null;
			loadedUnit = null;
			loadedCampaign = null;
			loadedCustomMap = null;
			UpdatePlayButton();
			UpdateLocalButtons();
		}

		public void ShowCampaign(TABSCampaignAsset campaign, bool playAnimation = true)
		{
			if (playAnimation)
			{
				OpenFactionPreview();
			}
			for (int i = 0; i < objectsToClear.Count; i++)
			{
				Object.Destroy(objectsToClear[i]);
			}
			objectsToClear.Clear();
			campaignParent.SetActive(value: true);
			battleParent.SetActive(value: false);
			factionParent.SetActive(value: false);
			unitParent.SetActive(value: false);
			levelParent.SetActive(value: false);
			HideLoadingIcon();
			CampaignName.text = campaign.Entity.Name;
			CampaignDescription.text = campaign.CampaignInfo.Description;
			CampaignHandler.GetCampaignSprite(campaign, delegate(Sprite sprite)
			{
				CampaignIcon.sprite = sprite;
			});
			battleCount.text = campaign.LevelsInCampaign.Length.ToString();
			for (int num = 0; num < campaign.LevelsInCampaign.Length; num++)
			{
				GameObject gameObject = Object.Instantiate(battleButton, battlesGrid);
				objectsToClear.Add(gameObject);
				CustomContentBattleButton component = gameObject.GetComponent<CustomContentBattleButton>();
				component.Setup(campaign.LevelsInCampaign[num]);
				component.EnableShadow(enable: false);
				Object.Destroy(component);
			}
			if (objectsToClear.Count > 0)
			{
				objectsToClear[0].GetComponent<Selectable>().Select();
			}
			loadedCampaign = campaign;
			loadedFaction = null;
			loadedUnit = null;
			loadedBattle = null;
			loadedCustomMap = null;
			UpdatePlayButton();
			UpdateLocalButtons();
		}

		public void ShowLevel(CustomMap customMap, bool playAnimation = true)
		{
			if (playAnimation)
			{
				OpenFactionPreview();
			}
			campaignParent.SetActive(value: false);
			battleParent.SetActive(value: false);
			factionParent.SetActive(value: false);
			unitParent.SetActive(value: false);
			levelParent.SetActive(value: true);
			HideLoadingIcon();
			LevelName.text = customMap.Entity.Name;
			customMap.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
			{
				if (sprite != null && LevelIcon != null)
				{
					LevelIcon.sprite = sprite;
				}
			});
			loadedBattle = null;
			loadedFaction = null;
			loadedUnit = null;
			loadedCampaign = null;
			loadedCustomMap = customMap;
			UpdatePlayButton();
			UpdateLocalButtons();
		}

		private void UpdatePlayButton()
		{
			if ((bool)loadedCampaign)
			{
				playButton.SetActive(value: true);
				playButtonGlyph.SetActive(value: true);
			}
			else if ((bool)loadedFaction)
			{
				playButton.SetActive(value: false);
				playButtonGlyph.SetActive(value: false);
			}
			else if ((bool)loadedUnit)
			{
				playButton.SetActive(value: false);
				playButtonGlyph.SetActive(value: false);
			}
			else if ((bool)loadedBattle)
			{
				playButton.SetActive(value: true);
				playButtonGlyph.SetActive(value: true);
			}
			else if ((bool)loadedCustomMap)
			{
				playButton.SetActive(value: true);
				playButtonGlyph.SetActive(value: true);
			}
		}

		private void UpdateLocalButtons()
		{
			bool active = IsContentLocal();
			editButton.SetActive(active);
			editButtonGlyph.SetActive(active);
			renameButton.SetActive(active);
			renameButtonGlyph.SetActive(active);
			deleteButton.SetActive(active);
			deleteButtonGlyph.SetActive(active);
			uploadButton.SetActive(active);
			uploadButtonGlyph.SetActive(active);
		}

		private bool IsContentLocal()
		{
			if ((bool)loadedUnit)
			{
				return !loadedUnit.IsModUnit;
			}
			if ((bool)loadedFaction)
			{
				return !loadedFaction.IsModFaction;
			}
			if ((bool)loadedBattle)
			{
				return !loadedBattle.IsModIOLevel;
			}
			if ((bool)loadedCampaign)
			{
				return !loadedCampaign.IsModCampaign;
			}
			if ((bool)loadedCustomMap)
			{
				return !loadedCustomMap.IsModLevel();
			}
			return true;
		}

		public void Edit()
		{
			if (!IsContentLocal())
			{
				return;
			}
			if ((bool)loadedUnit)
			{
				try
				{
					TABSSceneManager.LoadUnitCreator(loadedUnit);
					return;
				}
				catch
				{
					m_modalPanel.PopUp(string.Format(couldNotLoadMapError, loadedUnit.Entity.Name));
					return;
				}
			}
			if ((bool)loadedFaction)
			{
				try
				{
					StartCoroutine(Delay());
					return;
				}
				catch
				{
					m_modalPanel.PopUp(string.Format(couldNotLoadMapError, loadedFaction.Entity.Name));
					return;
				}
			}
			if ((bool)loadedBattle)
			{
				try
				{
					if (m_SettingsProfileManager.CurrentSettingsProfile == null)
					{
						EditBattle();
					}
					else if (m_SettingsProfileManager.CurrentSettingsProfile.UserPlacementMaxUnits.HasValue)
					{
						int value = m_SettingsProfileManager.CurrentSettingsProfile.UserPlacementMaxUnits.Value;
						if (loadedBattle.GetTotalUnits() > value)
						{
							m_modalPanel.Choice(warningHeader, unitLimitWarning, EditBattle, null);
						}
						else
						{
							EditBattle();
						}
					}
					else
					{
						EditBattle();
					}
					return;
				}
				catch
				{
					m_modalPanel.PopUp(string.Format(couldNotLoadMapError, loadedBattle.Entity.Name));
					return;
				}
			}
			if ((bool)loadedCampaign)
			{
				try
				{
					StartCoroutine(Delay2());
					return;
				}
				catch
				{
					m_modalPanel.PopUp(string.Format(couldNotLoadMapError, loadedCampaign.Entity.Name));
					return;
				}
			}
			if (!loadedCustomMap)
			{
				return;
			}
			try
			{
				TABSSceneManager.LoadLevelCreator(DMEditor.StartState.Edit, loadedCustomMap.LevelPath);
			}
			catch
			{
				m_modalPanel.PopUp(string.Format(couldNotLoadMapError, loadedCustomMap.Entity.Name));
			}
			IEnumerator Delay()
			{
				CloseFactionPreview();
				factionSideBarAnimation.SetTime(1f);
				yield return null;
				interfaceManager.OpenUIComponent(factionCreator.GetComponentInParent<UIComponentMainMenu>());
				Object.FindObjectOfType<CustomContetnManager>().NavigateToNewFaction(init: true, loadedFaction);
			}
			IEnumerator Delay2()
			{
				DMCampaignCreator campaignCreator = Object.FindObjectOfType<DMCampaignCreator>();
				if (campaignCreator != null)
				{
					CloseFactionPreview();
					yield return null;
					interfaceManager.OpenUIComponent(campaignCreator.GetComponentInParent<UIComponentMainMenu>());
					campaignCreator.OpenWithData(loadedCampaign);
				}
			}
		}

		public void Rename()
		{
			if (!IsContentLocal())
			{
				return;
			}
			if ((bool)loadedUnit)
			{
				m_modalPanel.Inputfield(new ModalPanel.InputFieldParameters
				{
					header = "POPUP_RENAME",
					startInput = loadedUnit.Entity.Name,
					singlelineHeader = "POPUP_NEWNAME",
					yesButton = "POPUP_RENAME",
					onFinish = delegate(string input)
					{
						_ = loadedUnit.Entity.GUID;
						loadedUnit.Entity.Name = input;
						CustomUnitHandler.OverrideUnit(loadedUnit, delegate
						{
							GetComponentInParent<UnitCreatorFactionBrowser>().QuickRefresh(WorkshopContentType.Unit);
							ShowUnit(loadedUnit, playAnimation: false);
						});
					}
				}, new ModalPanel.ContentDisplayParameters
				{
					unit = loadedUnit
				});
			}
			else if ((bool)loadedFaction)
			{
				m_modalPanel.Inputfield(new ModalPanel.InputFieldParameters
				{
					header = "POPUP_RENAME",
					startInput = loadedFaction.Entity.Name,
					singlelineHeader = "POPUP_NEWNAME",
					yesButton = "POPUP_RENAME",
					onFinish = delegate(string input)
					{
						DatabaseID id = loadedFaction.Entity.GUID;
						loadedFaction.Entity.Name = input;
						CustomFactionHandler.SaveFaction(loadedFaction, id, delegate
						{
							GetComponentInParent<UnitCreatorFactionBrowser>().QuickRefresh(WorkshopContentType.Faction);
							ShowFaction(ContentDatabase.Instance().GetFaction(id), playAnimation: false);
						});
					}
				}, new ModalPanel.ContentDisplayParameters
				{
					faction = loadedFaction
				});
			}
			else if ((bool)loadedBattle)
			{
				m_modalPanel.Inputfield(new ModalPanel.InputFieldParameters
				{
					header = "POPUP_RENAME",
					startInput = loadedBattle.Entity.Name,
					singlelineHeader = "POPUP_NEWNAME",
					yesButton = "POPUP_RENAME",
					onFinish = delegate(string input)
					{
						DatabaseID id = loadedBattle.Entity.GUID;
						loadedBattle.Entity.Name = input;
						CampaignHandler.OverwriteLayout(loadedBattle, delegate
						{
							GetComponentInParent<UnitCreatorFactionBrowser>().QuickRefresh(WorkshopContentType.Battle);
							ShowBattle(ContentDatabase.Instance().GetCampaignLevel(id), playAnimation: false);
						});
					}
				}, new ModalPanel.ContentDisplayParameters
				{
					battle = loadedBattle
				});
			}
			else if ((bool)loadedCampaign)
			{
				m_modalPanel.Inputfield(new ModalPanel.InputFieldParameters
				{
					header = "POPUP_RENAME",
					startInput = loadedCampaign.Entity.Name,
					singlelineHeader = "POPUP_NEWNAME",
					yesButton = "POPUP_RENAME",
					onFinish = delegate(string input)
					{
						DatabaseID id = loadedCampaign.Entity.GUID;
						loadedCampaign.Entity.Name = input;
						CampaignHandler.OverwriteCampaign(loadedCampaign, delegate
						{
							GetComponentInParent<UnitCreatorFactionBrowser>().QuickRefresh(WorkshopContentType.Campaign);
							ShowCampaign(ContentDatabase.Instance().GetCampaign(id), playAnimation: false);
						});
					}
				}, new ModalPanel.ContentDisplayParameters
				{
					campaign = loadedCampaign
				});
			}
			else
			{
				if (!loadedCustomMap)
				{
					return;
				}
				m_modalPanel.Inputfield(new ModalPanel.InputFieldParameters
				{
					header = "POPUP_RENAME",
					startInput = loadedCustomMap.Entity.Name,
					singlelineHeader = "POPUP_NEWNAME",
					yesButton = "POPUP_RENAME",
					onFinish = delegate(string input)
					{
						m_FileIO.FileExists(loadedCustomMap.FilePath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool fileExists)
						{
							if (fileExists)
							{
								loadedCustomMap.Entity.Name = input;
								DMEditor.SaveMetadata(loadedCustomMap.FilePath.Substring(0, loadedCustomMap.FilePath.Length - 4), input, loadedCustomMap.Entity.GUID, delegate
								{
									GetComponentInParent<UnitCreatorFactionBrowser>().QuickRefresh(WorkshopContentType.Map);
									ShowLevel(loadedCustomMap, playAnimation: false);
								});
							}
						});
					}
				}, new ModalPanel.ContentDisplayParameters
				{
					level = loadedCustomMap
				});
			}
		}

		public void DeleteUnit()
		{
			if (IsContentLocal())
			{
				if ((bool)loadedUnit)
				{
					ModalPanel.ContentDisplayParameters c = new ModalPanel.ContentDisplayParameters
					{
						unit = loadedUnit
					};
					DeleteContent(ContentTypeFilter.Units, loadedUnit, CustomContentFilePaths.UnitDirectoryPath, c);
				}
				else if ((bool)loadedFaction)
				{
					ModalPanel.ContentDisplayParameters c2 = new ModalPanel.ContentDisplayParameters
					{
						faction = loadedFaction
					};
					DeleteContent(ContentTypeFilter.Factions, loadedFaction, CustomContentFilePaths.FilePathFaction, c2);
				}
				else if ((bool)loadedBattle)
				{
					ModalPanel.ContentDisplayParameters c3 = new ModalPanel.ContentDisplayParameters
					{
						battle = loadedBattle
					};
					DeleteContent(ContentTypeFilter.Battles, loadedBattle, CustomContentFilePaths.FilePathLayout, c3);
				}
				else if ((bool)loadedCampaign)
				{
					ModalPanel.ContentDisplayParameters c4 = new ModalPanel.ContentDisplayParameters
					{
						campaign = loadedCampaign
					};
					DeleteContent(ContentTypeFilter.Campaigns, loadedCampaign, CustomContentFilePaths.FilePathCampaign, c4);
				}
				else if ((bool)loadedCustomMap)
				{
					ModalPanel.ContentDisplayParameters c5 = new ModalPanel.ContentDisplayParameters
					{
						level = loadedCustomMap
					};
					LevelUtility.RemoveRecentLevel(loadedCustomMap.LevelPath);
					DeleteContent(ContentTypeFilter.Maps, loadedCustomMap, CustomContentFilePaths.FilePathCustomMap, c5);
				}
			}
		}

		private void DeleteContent(ContentTypeFilter contentType, IDatabaseEntity contentData, string contentFilePath, ModalPanel.ContentDisplayParameters c)
		{
			ServiceLocator.GetService<ModalPanel>().Choice("POPUP_DELETE_TITLE", "POPUP_DELETE_TEXT", c, delegate
			{
				string guid = contentData.Entity.GUID.m_ID.ToString();
				string folderPath = contentFilePath + contentData.Entity.GUID;
				m_FileIO.DirectoryExists(folderPath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool folderExists)
				{
					if (folderExists)
					{
						DeleteBattleCreatorSharedCommandsContent(contentType, contentData, folderPath);
					}
					else
					{
						folderPath = contentFilePath + DatabaseID._LOCAL_CONTENT + guid;
						m_FileIO.DirectoryExists(folderPath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool nextFolderExists)
						{
							if (nextFolderExists)
							{
								DeleteBattleCreatorSharedCommandsContent(contentType, contentData, folderPath);
							}
							else
							{
								Debug.LogError("File does not exist: " + folderPath);
							}
						});
					}
				});
			}, null, "BUTTON_DELETE", "BUTTON_CANCEL", true, true, "\n");
		}

		private void DeleteBattleCreatorSharedCommandsContent(ContentTypeFilter contentType, IDatabaseEntity contentData, string folderPath)
		{
			Debug.Log("Deleting " + contentType.ToString() + " in folder: " + folderPath);
			BattleCreatorSharedCommands.DeleteContentFolder(new CustomContentDataPackage(contentData.Entity.GUID, folderPath, contentType), delegate
			{
				DMNewContentManager.RemoveNewContentID(0, contentData.Entity.Name, isSavedToLocal: true, contentType.ToWorkshopTypeFilter());
				if (CustomContetnManager.previousBattle != null)
				{
					if (CustomContetnManager.previousBattle.Entity.GUID == contentData.Entity.GUID)
					{
						CampaignHandler.ResetLoadedLevel();
						CustomContetnManager.previousBattle = null;
					}
				}
				else if (CampaignHandler.LastLoadedLevel != null && CampaignHandler.LastLoadedLevel.Entity.GUID == contentData.Entity.GUID)
				{
					CampaignHandler.ResetLoadedLevel();
					CustomContetnManager.previousMap = null;
				}
				Object.FindObjectOfType<UnitCreatorFactionBrowser>().QuickRefresh(contentType.ToWorkshopTypeFilter());
				browserSelection = null;
				CloseFactionPreview();
			});
		}

		public void Upload()
		{
			if (!IsContentLocal())
			{
				return;
			}
			int tempShowCount = ShowLoadingIcon();
			CheckWorkshopPermissions(delegate(PermissionsHelperResult result)
			{
				if (result != PermissionsHelperResult.Succeeded)
				{
					if (tempShowCount == showLoadingIconCount)
					{
						HideLoadingIcon();
					}
				}
				else if ((bool)loadedUnit)
				{
					TABSSceneManager.LoadMainMenuForUpload(loadedUnit);
				}
				else if ((bool)loadedFaction)
				{
					TABSSceneManager.LoadMainMenuForUpload(loadedFaction);
				}
				else if ((bool)loadedBattle)
				{
					TABSSceneManager.LoadMainMenuForUpload(loadedBattle);
				}
				else if ((bool)loadedCampaign)
				{
					TABSSceneManager.LoadMainMenuForUpload(loadedCampaign);
				}
				else if ((bool)loadedCustomMap)
				{
					TABSSceneManager.LoadMainMenuForUpload(loadedCustomMap);
				}
			});
		}

		public void Play()
		{
			if ((bool)loadedBattle)
			{
				try
				{
					if (m_SettingsProfileManager.CurrentSettingsProfile == null)
					{
						PlayBattle();
					}
					else if (m_SettingsProfileManager.CurrentSettingsProfile.UserPlacementMaxUnits.HasValue)
					{
						int value = m_SettingsProfileManager.CurrentSettingsProfile.UserPlacementMaxUnits.Value;
						if (loadedBattle.GetTotalUnits() > value)
						{
							m_modalPanel.Choice(warningHeader, unitLimitWarning, PlayBattle, null);
						}
						else
						{
							PlayBattle();
						}
					}
					else
					{
						PlayBattle();
					}
					return;
				}
				catch
				{
					m_modalPanel.PopUp(string.Format(couldNotLoadMapError, loadedBattle.Entity.Name));
					return;
				}
			}
			if ((bool)loadedCampaign)
			{
				try
				{
					CampaignPlayerDataHolder.StartedPlayingNewCampaign(loadedCampaign, 0);
					TABSSceneManager.LoadCampaign();
					return;
				}
				catch
				{
					m_modalPanel.PopUp(string.Format(couldNotLoadMapError, loadedCampaign.Entity.Name));
					return;
				}
			}
			if ((bool)loadedCustomMap)
			{
				CampaignHandler.ResetLoadedLevel();
				SpawnLevel.SetCustomMapToLoad(loadedCustomMap);
				ServiceLocator.GetService<GameModeService>().SetGameMode<SandboxGameMode>();
				CampaignPlayerDataHolder.StartedPlayingSandbox();
				TABSSceneManager.LoadMap(LevelMapAsset);
			}
		}

		public void EditBattle()
		{
			if (loadedBattle == null || loadedBattle.MapAsset == null)
			{
				m_modalPanel.PopUp(string.Format(couldNotLoadMapError, loadedBattle.Entity.Name));
				return;
			}
			if (loadedBattle.CustomMap != default(DatabaseID))
			{
				CustomMap userMap = ContentDatabase.Instance().GetUserMap(loadedBattle.CustomMap);
				if (userMap != null)
				{
					SpawnLevel.SetCustomMapToLoad(userMap);
				}
			}
			ServiceLocator.GetService<GameModeService>().SetGameMode<SandboxGameMode>();
			CampaignPlayerDataHolder.StartedPlayingSandbox();
			CampaignHandler.LoadLayoutFromDisk(loadedBattle.FilePath, null);
		}

		private void PlayBattle()
		{
			if (loadedBattle == null || loadedBattle.MapAsset == null)
			{
				m_modalPanel.PopUp(string.Format(couldNotLoadMapError, loadedBattle.Entity.Name));
				return;
			}
			if (loadedBattle.CustomMap != default(DatabaseID))
			{
				CustomMap userMap = ContentDatabase.Instance().GetUserMap(loadedBattle.CustomMap);
				if (!(userMap != null))
				{
					m_modalPanel.PopUp(string.Format(couldNotLoadMapError, loadedBattle.Entity.Name));
					return;
				}
				SpawnLevel.SetCustomMapToLoad(userMap);
			}
			CampaignPlayerDataHolder.StartedPlayingBattle(loadedBattle);
			TABSSceneManager.LoadCampaign();
		}

		private void CheckWorkshopPermissions(CheckWorkshopPermissionsCallback callback)
		{
			cancelPermissionsCheckCount++;
			int tempCheckCount = cancelPermissionsCheckCount;
			permissionsHelper.CheckWorkshopPermissions(() => tempCheckCount != cancelPermissionsCheckCount, callback);
		}

		private int ShowLoadingIcon()
		{
			showLoadingIconCount++;
			loadingIconParent.SetActive(value: true);
			return showLoadingIconCount;
		}

		private void HideLoadingIcon()
		{
			if (loadingIconParent != null)
			{
				loadingIconParent.SetActive(value: false);
			}
		}
	}
}
