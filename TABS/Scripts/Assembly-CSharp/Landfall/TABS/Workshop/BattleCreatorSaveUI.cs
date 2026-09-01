using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DM;
using Landfall.TABC;
using Landfall.TABS.GameMode;
using Landfall.TABS_Input;
using LevelCreator;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Landfall.TABS.Workshop
{
	public class BattleCreatorSaveUI : UIComponentMainMenu
	{
		[SerializeField]
		private TMP_Text m_Header;

		[SerializeField]
		private Transform m_SettingsContainer;

		[SerializeField]
		private Button m_LevelNameButton;

		[SerializeField]
		private TMP_Text m_LevelName;

		[SerializeField]
		private LocalizeText m_LevelNameLocalized;

		[SerializeField]
		private Button m_BudgetButton;

		[SerializeField]
		private TMP_Text m_BudgetValue;

		[SerializeField]
		private LocalizeText m_BudgetValueLocalized;

		[SerializeField]
		private Button m_SaveFriendlyUnits;

		[SerializeField]
		private Button m_DescriptionButton;

		[SerializeField]
		private TMP_Text m_DescriptionText;

		[SerializeField]
		private LocalizeText m_DescriptionTextLocalized;

		[SerializeField]
		private Button m_LimitUnitsButton;

		[SerializeField]
		private Transform m_limitsContainer;

		[SerializeField]
		private GameObject m_limitsItem;

		[SerializeField]
		private GameObject m_onScreenButtons;

		[SerializeField]
		private GameObject m_controlPromptLabels;

		[SerializeField]
		private GameObject m_UnitFilterPromptLabels;

		[SerializeField]
		private UnitWhitelistUI m_UnitWhiteList;

		[SerializeField]
		private UILayoutGroup m_LayoutGroup;

		[SerializeField]
		private UILayoutGroup m_FactionLayoutGroup;

		[SerializeField]
		private UILayoutGroup m_PanelFactionLayoutGroup;

		[SerializeField]
		private ExpandedFactionUI expandedFactionUI;

		[SerializeField]
		private ExpandedFactionUI placementExpandedFactionUI;

		[SerializeField]
		private GameObject m_BackButton;

		[SerializeField]
		private GameObject m_WhitelistBackButton;

		[SerializeField]
		private PlacementUI m_placementUI;

		private int m_DecidedBudget = -1;

		private string m_levelName;

		private string m_description;

		private bool m_saveFriendlyUnits;

		private TABSCampaignLevelAsset m_CurrentLoadedLayout;

		private ContentDatabase m_unitDatabase;

		private Faction[] AllowedFactions;

		private List<DatabaseID> BannedUnits = new List<DatabaseID>();

		private CanvasGroup m_canvasGroup;

		private ModalPanel modalService;

		private GameModeService gameModeService;

		private bool busySaving;

		public bool AllowPageChange { get; private set; }

		public bool FilteringUnits { get; private set; }

		protected override void Awake()
		{
			base.Awake();
			InitReference();
			InitListeners();
			CreateExplicitNavigation();
			gameModeService = ServiceLocator.GetService<GameModeService>();
		}

		private void InitReference()
		{
			m_canvasGroup = GetComponent<CanvasGroup>();
			BannedUnits = new List<DatabaseID>();
			AllowedFactions = new Faction[0];
			m_unitDatabase = ContentDatabase.Instance();
			if (m_UnitWhiteList != null)
			{
				m_UnitWhiteList.SetBattleCreatorSaveUI(this);
				m_UnitWhiteList.Close();
			}
		}

		private void InitListeners()
		{
			modalService = ServiceLocator.GetService<ModalPanel>();
			m_LevelNameButton.onClick.AddListener(delegate
			{
				modalService.Inputfield(new ModalPanel.InputFieldParameters
				{
					header = "POPUP_BATTLE_SETNAME_TITLE",
					onFinish = delegate(string name)
					{
						OnLevelNameChange(name);
						modalService.CloseWaitPopup();
					},
					yesButton = "POPUP_BATTLE_SETNAME_YES",
					startInput = m_levelName
				});
			});
			m_BudgetButton.onClick.AddListener(delegate
			{
				modalService.Inputfield(new ModalPanel.InputFieldParameters
				{
					header = "POPUP_BATTLE_BUDGET_TITLE",
					onFinish = delegate(string budget)
					{
						OnBudgetChange(budget);
						modalService.CloseWaitPopup();
					},
					yesButton = "POPUP_BATTLE_BUDGET_YES",
					startInput = m_DecidedBudget.ToString(),
					contentType = TMP_InputField.ContentType.IntegerNumber
				});
			});
			m_SaveFriendlyUnits.onClick.AddListener(delegate
			{
				ToggleSaveFriendlyUnits(!m_saveFriendlyUnits);
			});
			m_DescriptionButton.onClick.AddListener(delegate
			{
				modalService.Inputfield(new ModalPanel.InputFieldParameters
				{
					header = "POPUP_BATTLE_DESCRIPTION_TITLE",
					onFinish = delegate(string desc)
					{
						OnDescriptionChange(desc);
						modalService.CloseWaitPopup();
					},
					yesButton = "POPUP_BATTLE_DESCRIPTION_YES",
					startInput = m_description,
					isMultiline = true
				});
			});
			m_LimitUnitsButton.onClick.AddListener(delegate
			{
				ToggleUnitLimitSelector();
			});
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			m_LayoutGroup.RedrawAction -= UpdateUnitFilter;
			m_LevelNameLocalized.enabled = false;
			m_BudgetValueLocalized.enabled = false;
			m_DescriptionTextLocalized.enabled = false;
		}

		private SaveCampaignSettings GetSettings()
		{
			int currentLoadedLevelIndex = GetCurrentLoadedLevelIndex();
			MapAsset mapAssetByIndex = m_unitDatabase.GetMapAssetByIndex(currentLoadedLevelIndex);
			if (mapAssetByIndex != null)
			{
				DatabaseID gUID = mapAssetByIndex.Entity.GUID;
				Faction[] allowedFactions;
				AllowedUnitWrapper[] allowedUnits = GetAllowedUnits(out allowedFactions);
				if (allowedFactions == null || allowedFactions.Length == 0)
				{
					allowedFactions = m_unitDatabase.GetDefaultHotbarFactions().ToArray();
				}
				DatabaseID[] array = new DatabaseID[allowedFactions.Length];
				for (int i = 0; i < allowedFactions.Length; i++)
				{
					if (!(allowedFactions[i] == null))
					{
						array[i] = allowedFactions[i].Entity.GUID;
					}
				}
				DatabaseID customMap = default(DatabaseID);
				if (mapAssetByIndex.MapName == "LevelScene")
				{
					customMap = SpawnLevel.CustomMap.Entity.GUID;
				}
				TABSSceneSettings serializedSettings = SceneSettings.SerializedSettings;
				return new SaveCampaignSettings(m_DecidedBudget, allowedUnits, array, gUID, m_description, serializedSettings, m_saveFriendlyUnits, customMap);
			}
			return null;
		}

		private int GetCurrentLoadedLevelIndex()
		{
			string text = SceneManager.GetActiveScene().name;
			MapAsset[] array = m_unitDatabase.GetAllMapAssetsOrdered().ToArray();
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				if (array[i].MapName == text)
				{
					return i;
				}
			}
			return -1;
		}

		private AllowedUnitWrapper[] GetAllowedUnits(out Faction[] allowedFactions)
		{
			allowedFactions = AllowedFactions;
			DatabaseID[] allowedUnitIDs = GetAllowedUnitIDs();
			if (allowedUnitIDs == null || allowedUnitIDs.Length == 0)
			{
				return new AllowedUnitWrapper[0];
			}
			AllowedUnitWrapper[] array = new AllowedUnitWrapper[allowedUnitIDs.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new AllowedUnitWrapper();
				array[i].ID = allowedUnitIDs[i];
			}
			return array;
		}

		private void SaveLocal()
		{
			if (busySaving)
			{
				return;
			}
			SaveCampaignSettings settings = GetSettings();
			string fileExtension = CustomContentFilePaths.GetFileExtension(settings.LevelType);
			new FileInfo(string.Concat(CustomContentFilePaths.FilePathLayout + "/" + m_levelName, "/", m_levelName, fileExtension));
			TABSCampaignLevelAsset existingLevelAsset = null;
			UnityAction unityAction = async delegate
			{
				int customID = ((!(CampaignHandler.LastLoadedLevel == null)) ? CampaignHandler.LastLoadedLevel.ModID : 0);
				if (SpawnLevel.IsCustomLevelScene)
				{
					customID = SpawnLevel.CustomMap.Entity.GUID.m_modID;
				}
				busySaving = true;
				int openId = modalService.WaitPopUpWithFocus("LABEL_SAVING", false, -1f, null, null, true);
				await Task.Delay(500);
				CampaignHandler.SaveLayout(m_levelName, settings, temp: false, default(DatabaseID), existingLevelAsset, async delegate(bool success)
				{
					await WaitForFrames(5);
					busySaving = false;
					if (openId == modalService.OpenId)
					{
						modalService.CloseWaitPopup();
					}
					if (success)
					{
						m_CurrentLoadedLayout = CampaignHandler.LastLoadedLevel;
						if (m_CurrentLoadedLayout != null)
						{
							m_CurrentLoadedLayout.SetCustomIDButOnlySometimes(customID);
						}
						modalService.PopUp("POPUP_SAVED", Back);
					}
				});
			};
			UnityAction saveAction = unityAction;
			TABSCampaignLevelAsset asset = m_unitDatabase.GetUserCampaignLevelByExactNameAndType(m_levelName, WorkshopTypeFilter.Local);
			if (asset != null)
			{
				ServiceLocator.GetService<ModalPanel>().Choice("POPUP_SAVEANDREPLACE_TITLE", "POPUP_SAVEANDREPLACE_TEXT", delegate
				{
					existingLevelAsset = asset;
					saveAction();
				}, delegate
				{
				}, new string[1] { asset.Entity.Name });
			}
			else
			{
				saveAction();
			}
		}

		private async Task WaitForFrames(int frames)
		{
			for (int i = 0; i < frames; i++)
			{
				await Task.Yield();
			}
		}

		private DatabaseID[] GetAllowedUnitIDs()
		{
			ContentDatabase.Instance();
			List<DatabaseID> list = new List<DatabaseID>();
			if (AllowedFactions == null || AllowedFactions.Length == 0)
			{
				return new DatabaseID[0];
			}
			for (int i = 0; i < AllowedFactions.Length; i++)
			{
				Faction faction = AllowedFactions[i];
				if (faction == null || faction.Units == null)
				{
					continue;
				}
				for (int j = 0; j < faction.Units.Length; j++)
				{
					if (!(faction.Units[j] == null) && !list.Contains(faction.Units[j].Entity.GUID))
					{
						list.Add(faction.Units[j].Entity.GUID);
					}
				}
			}
			for (int k = 0; k < BannedUnits.Count; k++)
			{
				if (list.Contains(BannedUnits[k]))
				{
					list.Remove(BannedUnits[k]);
				}
			}
			return list.ToArray();
		}

		private UnitBlueprint[] GetAllowedUnitBlueprints()
		{
			ContentDatabase contentDatabase = ContentDatabase.Instance();
			List<UnitBlueprint> list = new List<UnitBlueprint>();
			DatabaseID[] allowedUnitIDs = GetAllowedUnitIDs();
			for (int i = 0; i < allowedUnitIDs.Length; i++)
			{
				list.Add(contentDatabase.GetUnitBlueprint(allowedUnitIDs[i]));
			}
			return list.ToArray();
		}

		private void OnBudgetChange(string value)
		{
			if (value == string.Empty)
			{
				m_DecidedBudget = 0;
			}
			if (int.TryParse(value, out var result))
			{
				m_DecidedBudget = result;
				if (m_DecidedBudget <= 0)
				{
					m_DecidedBudget = 0;
					m_BudgetValueLocalized.enabled = true;
					m_BudgetValueLocalized.LocaleID = m_BudgetValueLocalized.LocaleID;
				}
				else
				{
					m_BudgetValueLocalized.enabled = false;
					m_BudgetValue.text = m_DecidedBudget.ToString();
				}
			}
		}

		private void OnLevelNameChange(string lvlName)
		{
			m_levelName = lvlName;
			m_LevelName.text = lvlName;
			m_LevelNameLocalized.enabled = string.IsNullOrEmpty(lvlName);
		}

		private void OnDescriptionChange(string desc)
		{
			m_description = desc;
			m_DescriptionText.text = desc;
			m_DescriptionTextLocalized.enabled = string.IsNullOrEmpty(desc);
		}

		private void ToggleSaveFriendlyUnits(bool value)
		{
			m_saveFriendlyUnits = value;
			m_SaveFriendlyUnits.GetComponent<ToggleEvent>().SetState(value);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			m_CurrentLoadedLayout = CampaignHandler.LastLoadedLevel;
			m_levelName = ((m_CurrentLoadedLayout == null) ? string.Empty : m_CurrentLoadedLayout.Entity.Name);
			if (m_CurrentLoadedLayout != null)
			{
				OnLevelNameChange(m_CurrentLoadedLayout.Entity.Name);
				OnBudgetChange(m_CurrentLoadedLayout.m_budget.ToString());
				AllowedFactions = m_CurrentLoadedLayout.AllowedFactions;
				BannedUnits = GetBannedUnits((from p in m_CurrentLoadedLayout.AllowedUnits.Where(delegate(UnitBlueprint p)
					{
						if (p != null && p.Entity != null)
						{
							_ = p.Entity.GUID;
							return true;
						}
						return false;
					})
					select p.Entity.GUID).ToList(), AllowedFactions);
				ToggleSaveFriendlyUnits((m_CurrentLoadedLayout.RedUnits.Length != 0) ? true : false);
				OnDescriptionChange(m_CurrentLoadedLayout.CampaignInfo.Description);
			}
			else
			{
				OnLevelNameChange("");
				OnBudgetChange(0.ToString());
				ToggleSaveFriendlyUnits(value: false);
				OnDescriptionChange("");
				AllowedFactions = new Faction[0];
				BannedUnits = new List<DatabaseID>();
			}
			m_LayoutGroup.RedrawAction += UpdateUnitFilter;
		}

		private List<DatabaseID> GetBannedUnits(List<DatabaseID> allowedUnits, Faction[] allowedFactions)
		{
			List<DatabaseID> list = new List<DatabaseID>();
			List<DatabaseID> list2 = new List<DatabaseID>();
			if (allowedFactions == null)
			{
				return list2;
			}
			for (int i = 0; i < allowedFactions.Length; i++)
			{
				if (allowedFactions[i] == null || allowedFactions[i].Units == null)
				{
					continue;
				}
				for (int j = 0; j < allowedFactions[i].Units.Length; j++)
				{
					if (!(allowedFactions[i].Units[j] == null))
					{
						DatabaseID gUID = allowedFactions[i].Units[j].Entity.GUID;
						if (!list.Contains(gUID))
						{
							list.Add(gUID);
						}
					}
				}
			}
			if (allowedUnits != null)
			{
				for (int k = 0; k < list.Count; k++)
				{
					if (!allowedUnits.Contains(list[k]))
					{
						list2.Add(list[k]);
					}
				}
			}
			else
			{
				list2.AddRange(list);
			}
			return list2;
		}

		public void UpdateUnitFilter()
		{
			m_LayoutGroup.RefreshUnitWhitelist(GetAllowedUnitIDs());
		}

		public void ToggleUnitLimitSelector()
		{
			EnableUnitLimitSelector(!FilteringUnits);
		}

		private void OnFactionBarUpdated()
		{
			AllowedFactions = expandedFactionUI.factionBar.GetFactionsOnBar();
		}

		private void EnableUnitLimitSelector(bool enable)
		{
			m_BackButton.SetActive(!enable);
			m_WhitelistBackButton.SetActive(enable);
			m_UnitWhiteList.gameObject.SetActive(value: true);
			expandedFactionUI.ForceOpenState(enable);
			StartCoroutine(Delay());
			if (enable)
			{
				m_canvasGroup.interactable = false;
				placementExpandedFactionUI.gameObject.SetActive(value: false);
				PlacementFactionBar factionBar = expandedFactionUI.factionBar;
				factionBar.OnFactionBarChanged = (System.Action)Delegate.Combine(factionBar.OnFactionBarChanged, new System.Action(OnFactionBarUpdated));
				m_UnitWhiteList.Setup(AllowedFactions);
				m_UnitWhiteList.Open();
				if (PlayerActions.Instance.InputType == InputType.Controller)
				{
					m_controlPromptLabels.SetActive(value: false);
					m_UnitFilterPromptLabels.SetActive(value: true);
				}
				AllowPageChange = false;
				if (EventSystem.current != null)
				{
					Selectable[] componentsInChildren = expandedFactionUI.GetComponentsInChildren<Selectable>();
					foreach (Selectable selectable in componentsInChildren)
					{
						if (selectable != null && selectable.gameObject.activeSelf)
						{
							EventSystem.current.SetSelectedGameObject(selectable.gameObject);
							break;
						}
					}
				}
			}
			else
			{
				m_canvasGroup.interactable = true;
				placementExpandedFactionUI.gameObject.SetActive(value: true);
				PlacementFactionBar factionBar2 = expandedFactionUI.factionBar;
				factionBar2.OnFactionBarChanged = (System.Action)Delegate.Remove(factionBar2.OnFactionBarChanged, new System.Action(OnFactionBarUpdated));
				m_UnitWhiteList.Close();
				if (PlayerActions.Instance.InputType == InputType.Controller)
				{
					m_controlPromptLabels.SetActive(value: true);
					m_UnitFilterPromptLabels.SetActive(value: false);
				}
				AllowPageChange = true;
				if (EventSystem.current != null)
				{
					EventSystem.current.SetSelectedGameObject(m_LevelNameButton.gameObject);
				}
			}
			OnLevelNameChange(m_levelName);
			OnDescriptionChange(m_description);
			FilteringUnits = enable;
			IEnumerator Delay()
			{
				yield return null;
				expandedFactionUI.GetComponent<CanvasGroup>().interactable = enable;
			}
		}

		public void OnUnitClicked(DatabaseID unit)
		{
			if (BannedUnits.Contains(unit))
			{
				BannedUnits.Remove(unit);
			}
			else
			{
				BannedUnits.Add(unit);
			}
		}

		private bool ValidateLayout()
		{
			SaveCampaignSettings settings = GetSettings();
			if (ValidateSettings(settings, m_levelName, out var errorString))
			{
				return true;
			}
			ShowErrorPopup(errorString);
			return false;
		}

		private void ShowErrorPopup(string msg)
		{
			ServiceLocator.GetService<ModalPanel>().PopUp(msg, SelectBattleName);
		}

		private bool ValidateSettings(SaveCampaignSettings settings, string levelName, out string errorString)
		{
			bool flag = true;
			string text = string.Empty;
			BaseGameMode currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
			if (string.IsNullOrWhiteSpace(levelName))
			{
				flag = false;
				text = "POPUP_EMPTYLEVELNAME";
			}
			if (currentGameMode.TeamLayouts.GetTeamLayout(Team.Blue).Count <= 0)
			{
				flag = false;
				text = "POPUP_NOUNITSPLACED";
			}
			if (settings == null)
			{
				flag = false;
				text = "POPUP_FAILED_SAVING_BATTLE_ON_TEST_MAP";
			}
			else if (settings.AllowedFactions == null || settings.AllowedFactions.Length == 0)
			{
				flag = false;
				text = "POPUP_NO_UNIT_LIMITS_ASSIGNED";
			}
			if (flag)
			{
				text = string.Empty;
			}
			errorString = text.ToString();
			return flag;
		}

		public void Back()
		{
			if (FilteringUnits)
			{
				EnableUnitLimitSelector(enable: false);
			}
			else if (!modalService.IsPopupOpen)
			{
				stateManager.OpenUIComponent(m_placementUI);
			}
		}

		protected override void Update()
		{
			base.Update();
			NavigateUIWithController(PlayerActions.Instance);
		}

		public bool NavigateUIWithController(PlayerActions playerActions)
		{
			if (FilteringUnits && m_UnitWhiteList != null && m_UnitWhiteList.gameObject.activeSelf && playerActions.m_back.WasPressed)
			{
				Back();
				return false;
			}
			if (FilteringUnits)
			{
				DesktopModeUnitSelect(playerActions);
				return false;
			}
			if (playerActions.m_back.WasPressed)
			{
				Back();
			}
			if (playerActions.m_saveCustomContent.WasPressed && !busySaving && !modalService.IsPopupOpen)
			{
				SaveBattle();
			}
			return false;
		}

		private void CreateExplicitNavigation()
		{
			UIHelpers.CreateExplicitLinearNavigation(m_SettingsContainer.GetComponentsInChildren<Selectable>().ToList(), horizontal: false);
		}

		public void DesktopModeUnitSelect(PlayerActions playerActions)
		{
			if (m_UnitWhiteList != null)
			{
				m_UnitWhiteList.ProcessInput(playerActions);
			}
		}

		private void SelectBattleName()
		{
			if (PlayerActions.Instance.InputType == InputType.Controller)
			{
				EventSystem.current.SetSelectedGameObject(null);
				m_LevelNameButton.Select();
			}
		}

		protected override void OnOpen()
		{
			base.OnOpen();
			InputService service = ServiceLocator.GetService<InputService>();
			if (service != null)
			{
				service.InputChanged += OnInputSourceChanged;
			}
			OnInputSourceChanged(PlayerActions.Instance.InputType);
			SelectBattleName();
			gameModeService.CurrentGameMode.PlacementCamera.AllowMovement(allow: false);
		}

		protected override void OnClose()
		{
			base.OnClose();
			m_UnitWhiteList.gameObject.SetActive(value: false);
			InputService service = ServiceLocator.GetService<InputService>();
			if (service != null)
			{
				service.InputChanged -= OnInputSourceChanged;
			}
			gameModeService.CurrentGameMode.PlacementCamera.AllowMovement(allow: true);
		}

		public void SaveBattle()
		{
			if (ValidateLayout())
			{
				SaveLocal();
			}
		}

		private void OnInputSourceChanged(InputType type)
		{
			switch (type)
			{
			case InputType.Controller:
			{
				m_onScreenButtons.SetActive(value: false);
				if (FilteringUnits)
				{
					m_UnitFilterPromptLabels.SetActive(value: true);
					break;
				}
				m_controlPromptLabels.SetActive(value: true);
				Selectable[] componentsInChildren = GetComponentsInChildren<Selectable>();
				if (componentsInChildren != null && componentsInChildren.Length != 0)
				{
					componentsInChildren[0].Select();
				}
				break;
			}
			case InputType.Keyboard:
			case InputType.Any:
				m_onScreenButtons.SetActive(value: true);
				if (FilteringUnits)
				{
					m_UnitFilterPromptLabels.SetActive(value: false);
				}
				else
				{
					m_controlPromptLabels.SetActive(value: false);
				}
				break;
			default:
				throw new ArgumentOutOfRangeException("type", type, null);
			}
		}
	}
}
