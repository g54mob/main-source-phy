using System;
using System.Collections.Generic;
using System.IO;
using DM;
using Landfall.TABS;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TFBGames
{
	public class DebugMenu : MonoBehaviour
	{
		[SerializeField]
		private Toggle unlimitedUnitPointsToggle;

		[SerializeField]
		private Toggle unlockProgressToggle;

		[SerializeField]
		private Toggle unlockAllUnitsToggle;

		[SerializeField]
		private Button deleteAllUserDataButton;

		[SerializeField]
		private Button projectMarsOptions1;

		[SerializeField]
		private Button projectMarsNetworkInfoButton;

		[SerializeField]
		private Button resetAllSettingsButton;

		[SerializeField]
		private Button resetStatsAndAchievementsButton;

		[SerializeField]
		private GameObject projectMarsDebugObject;

		private Canvas menuCanvas;

		private GraphicRaycaster graphicRaycaster;

		private InputService inputService;

		private AchievementService achievementService;

		private GameObject lastSelectedGameObject;

		private Selectable[] selectables;

		private DebugClearUserData clearUserData;

		private GlobalSettingsHandler globalSettingsHandler;

		private GameObject lastUpdateSelectedGameObject;

		private SettingsInstance unlimitedUnitsSettings;

		private SettingsInstance unlockProgressSettings;

		private SettingsInstance unlockAllUnitsSettings;

		public bool HasUnlimitedUnitPoints => unlimitedUnitsSettings.currentValue == 1;

		public bool HasUnlockedProgress => unlockProgressSettings.currentValue == 1;

		public bool HasUnlockedAllUnits => unlockAllUnitsSettings.currentValue == 1;

		private void Start()
		{
			menuCanvas = GetComponent<Canvas>();
			graphicRaycaster = GetComponent<GraphicRaycaster>();
			globalSettingsHandler = ServiceLocator.GetService<GlobalSettingsHandler>();
			InitSettings(globalSettingsHandler.GameplaySettings);
			inputService = ServiceLocator.GetService<InputService>();
			inputService.InputChanged += OnInputChange;
			selectables = GetComponentsInChildren<Selectable>(includeInactive: true);
			clearUserData = GetComponentInChildren<DebugClearUserData>();
			achievementService = ServiceLocator.GetService<AchievementService>();
			EnableCanvas(enable: false);
			UpdateControlsVisibility();
		}

		private void OnEnable()
		{
			UpdateControlsVisibility();
		}

		private void Update()
		{
			bool num = Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D);
			bool flag = PlayerActions.Instance.m_pageLeft.IsPressed && PlayerActions.Instance.m_pageRight.IsPressed && PlayerActions.Instance.m_enterExitBattle.WasPressed;
			if (num || flag)
			{
				EnableCanvas(!menuCanvas.enabled);
				if (menuCanvas.enabled)
				{
					OnInputChange(PlayerActions.Instance.InputType);
				}
				else if (lastSelectedGameObject != null)
				{
					lastSelectedGameObject.GetComponent<Selectable>().Select();
				}
			}
			SetLastUpdateSelectedGameObject();
		}

		private void LateUpdate()
		{
			UpdateFocus();
			SetLastUpdateSelectedGameObject();
		}

		private void InitSettings(SettingsInstance[] settings)
		{
			foreach (SettingsInstance settingsInstance in settings)
			{
				switch (settingsInstance.m_settingsKey)
				{
				case "STACKS_ON_STACKS":
					unlimitedUnitsSettings = settingsInstance;
					break;
				case "UNLOCK_CAMPAIGN_LEVELS":
					unlockProgressSettings = settingsInstance;
					break;
				case "GAMEPLAY_RESTRICT_UNITS":
					unlockAllUnitsSettings = settingsInstance;
					break;
				}
			}
		}

		public void UnlimitedMoneySettingsToggleChanged()
		{
			ToggleSetting(unlimitedUnitsSettings, unlimitedUnitPointsToggle);
		}

		public void UnlockProgressToggleChanged()
		{
			ToggleSetting(unlockProgressSettings, unlockProgressToggle);
		}

		public void UnlockAllUnitsToggleChanged()
		{
			ToggleSetting(unlockAllUnitsSettings, unlockAllUnitsToggle);
		}

		public void DeleteCampaignProgress()
		{
			ServiceLocator.GetService<ISaveLoaderService>().ClearLevels(null);
			IPlayerPrefsPlatform service = ServiceLocator.GetService<IPlayerPrefsPlatform>();
			service.SetInt("CompletedTutorial", 0);
			service.SetInt("CompletedPossessionTutorial", 0);
			service.Save();
		}

		public void DeleteCustomBattleAndCampaigns()
		{
			ContentDatabase contentDatabase = ContentDatabase.Instance();
			IEnumerable<TABSCampaignAsset> userCampaigns = contentDatabase.GetUserCampaigns();
			IEnumerable<TABSCampaignLevelAsset> userCampaignLevels = contentDatabase.GetUserCampaignLevels();
			string value = "24Bit_";
			foreach (TABSCampaignAsset item in userCampaigns)
			{
				if (item.Entity.Name.IndexOf(value, StringComparison.InvariantCultureIgnoreCase) < 0)
				{
					DeleteContentFolder(item.Entity, item.FolderPath, ContentTypeFilter.Campaigns);
				}
			}
			foreach (TABSCampaignLevelAsset item2 in userCampaignLevels)
			{
				if (item2.Entity.Name.IndexOf(value, StringComparison.InvariantCultureIgnoreCase) < 0)
				{
					string fullName = new FileInfo(item2.FilePath).Directory.FullName;
					DeleteContentFolder(item2.Entity, fullName, ContentTypeFilter.Battles);
				}
			}
		}

		public void OnDeleteAllUserData()
		{
			if (clearUserData != null && clearUserData.IsPlatformSupported())
			{
				clearUserData.ClearActiveUserData();
				OnResetAllSettings();
			}
		}

		public void OnProjectMarsOptions1()
		{
			UpdateProjectMarsOptions();
		}

		public void ToggleProjectMarsInfo()
		{
		}

		public void EnableProjectMarsDebug(bool enable)
		{
		}

		public void OnResetAllSettings()
		{
			globalSettingsHandler.ResetToDefault();
		}

		public void OnResetStatsAndAchievements()
		{
			if (achievementService != null)
			{
				achievementService.ResetStats();
			}
		}

		private void DeleteContentFolder(DatabaseEntity entity, string folderPath, ContentTypeFilter filter)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
			FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
			string path = directoryInfo.FullName;
			Debug.LogFormat("Deleting Level: {0}", path);
			fileIO.DirectoryExists(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (!exists)
				{
					Debug.LogErrorFormat("Error deleting content: {0}     Does not exist", path);
				}
				else
				{
					fileIO.DeleteDirectory(path, recursive: true, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(Exception exception)
					{
						if (exception != null)
						{
							Debug.LogErrorFormat("Error deleting content: {0}\n{1}", path, exception);
						}
						else
						{
							switch (filter)
							{
							case ContentTypeFilter.Battles:
								LandfallUnitDatabase.GetDatabase().RemoveCampaignLevelWithGUID(entity.GUID, null);
								break;
							case ContentTypeFilter.Campaigns:
								ContentDatabase.Instance().RemoveUserCampaign(entity.GUID);
								break;
							case ContentTypeFilter.None:
							case ContentTypeFilter.Battles | ContentTypeFilter.Campaigns:
							case ContentTypeFilter.Units:
							case ContentTypeFilter.Battles | ContentTypeFilter.Units:
							case ContentTypeFilter.Campaigns | ContentTypeFilter.Units:
							case ContentTypeFilter.Battles | ContentTypeFilter.Campaigns | ContentTypeFilter.Units:
							case ContentTypeFilter.Factions:
								break;
							}
						}
					});
				}
			});
		}

		private bool isDebugMenuInFocus()
		{
			GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
			Selectable[] array = selectables;
			foreach (Selectable selectable in array)
			{
				if (currentSelectedGameObject == selectable.gameObject)
				{
					return true;
				}
			}
			return false;
		}

		private void ToggleSetting(SettingsInstance setting, Toggle toggle)
		{
			if (setting != null)
			{
				setting.currentValue = (toggle.isOn ? 1 : 0);
			}
		}

		private void OnInputChange(InputType inputType)
		{
			if (menuCanvas.enabled)
			{
				switch (inputType)
				{
				case InputType.Controller:
					lastSelectedGameObject = EventSystem.current.currentSelectedGameObject;
					SetFocusToFirstItem();
					break;
				case InputType.Keyboard:
				case InputType.Any:
					EventSystem.current.SetSelectedGameObject(null);
					break;
				default:
					throw new ArgumentOutOfRangeException("inputType", inputType, null);
				}
			}
		}

		private void OnDestroy()
		{
			if (inputService != null)
			{
				inputService.InputChanged -= OnInputChange;
			}
		}

		private void UpdateControlsVisibility()
		{
			bool active = clearUserData != null && clearUserData.IsPlatformSupported();
			if (deleteAllUserDataButton != null)
			{
				deleteAllUserDataButton.gameObject.SetActive(active);
			}
			UpdateProjectMarsOptions();
		}

		private void UpdateProjectMarsOptions()
		{
			if (!(projectMarsOptions1 == null))
			{
				projectMarsOptions1.gameObject.SetActive(value: false);
			}
		}

		private void UpdateFocus()
		{
			if (menuCanvas.enabled && !isDebugMenuInFocus() && PlayerActions.Instance.InputType == InputType.Controller)
			{
				EventSystem current = EventSystem.current;
				if (lastUpdateSelectedGameObject != null && current != null)
				{
					current.SetSelectedGameObject(lastUpdateSelectedGameObject);
				}
				else
				{
					SetFocusToFirstItem();
				}
			}
		}

		private void SetLastUpdateSelectedGameObject()
		{
			EventSystem current = EventSystem.current;
			if (!menuCanvas.enabled || current == null)
			{
				lastUpdateSelectedGameObject = null;
				return;
			}
			GameObject currentSelectedGameObject = current.currentSelectedGameObject;
			if (currentSelectedGameObject != null && currentSelectedGameObject.transform.IsChildOf(base.transform))
			{
				lastUpdateSelectedGameObject = current.currentSelectedGameObject;
			}
		}

		private void SetFocusToFirstItem()
		{
			unlimitedUnitPointsToggle.Select();
		}

		private void EnableCanvas(bool enable)
		{
			menuCanvas.enabled = enable;
			graphicRaycaster.enabled = enable;
			if (selectables == null || selectables.Length == 0)
			{
				return;
			}
			int i = 0;
			for (int num = selectables.Length; i < num; i++)
			{
				Selectable selectable = selectables[i];
				if (!(selectable == null))
				{
					selectable.navigation = (enable ? new Navigation
					{
						mode = Navigation.Mode.Vertical
					} : new Navigation
					{
						mode = Navigation.Mode.None
					});
				}
			}
		}
	}
}
