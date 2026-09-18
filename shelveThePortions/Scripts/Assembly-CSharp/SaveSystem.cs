using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Steamworks;
using UnityEngine;
using UnityEngine.InputSystem;
using VInspector;

public class SaveSystem : Singleton<SaveSystem>
{
	[ReadOnly]
	public static bool disableSavingSystem = false;

	public static bool isDebugOn = false;

	public static string filePrefex = ".txt";

	public static string startingSaveFileName = "StartingSave_Data";

	public static string saveFileName = "Save_Data";

	public static string gameDataSaveFileName = "Game_Data";

	public static LevelSaveData currentlevelSaveData = null;

	private void OnEnable()
	{
		FBPP.Start(new FBPPConfig
		{
			SaveFileName = gameDataSaveFileName + filePrefex,
			AutoSaveData = false,
			SaveFilePath = GetSaveDir()
		});
		if (!IsSaveFileAvilable(gameDataSaveFileName))
		{
			ResetGame();
		}
		if (GetResulationSetting() == -1)
		{
			SetScreenSettingFirstTime();
		}
		LoadLevelSaveData();
	}

	private static string GetFilePath(string fileName)
	{
		return GetSaveDir() + "/" + fileName + filePrefex;
	}

	public static string GetSaveDir(string folderName = "")
	{
		try
		{
			string text = Application.persistentDataPath;
			if (SteamManager.Initialized)
			{
				text = text + "/" + SteamUser.GetSteamID().ToString();
			}
			if (folderName != "")
			{
				text = text + "/" + folderName;
			}
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}
		catch
		{
			return Application.persistentDataPath;
		}
	}

	public static bool IsSaveFileAvilable(string fileName)
	{
		return File.Exists(GetFilePath(fileName));
	}

	public static void ResetGame()
	{
		FBPP.DeleteAll();
		PlayerPrefs.DeleteAll();
		ResetPlayerInput();
		SetScreenSettingFirstTime();
		SavePrefs();
	}

	private static void SetScreenSettingFirstTime()
	{
		ScreenSettingDataStore screenSettingDataStore = Object.FindObjectOfType<ScreenSettingDataStore>(includeInactive: true);
		if (screenSettingDataStore != null)
		{
			screenSettingDataStore.SetScreenSettingFirstTime();
		}
		FPSLimiter.UpdateFPSLimit();
		SavePrefs();
	}

	public static void SaveLevel()
	{
		if ((SceneSingleton<TutorialManager>.Instance != null && !SceneSingleton<TutorialManager>.Instance.isTutorialCompleted) || (Singleton<GameBuildManager>.Instance != null && Singleton<GameBuildManager>.Instance.IsFreeVersion()))
		{
			return;
		}
		if (isDebugOn)
		{
			UnityEngine.Debug.Log("SaveLevel");
		}
		LevelSaveData levelSaveData = new LevelSaveData
		{
			saveFileName = saveFileName,
			gameTimerString = SceneSingleton<GameTimeManager>.Instance.GetTimerString(),
			isGameEnded = SceneSingleton<GameManager>.Instance.isGameEnded,
			currentGameHours = SceneSingleton<GameTimeManager>.Instance.currentHours,
			currentGameMinutes = SceneSingleton<GameTimeManager>.Instance.currentMinutes,
			currentGameSeconds = SceneSingleton<GameTimeManager>.Instance.currentSeconds,
			inventoryUpgradeIndex = SceneSingleton<UpgradesManager>.Instance.currentInventoryUpgradeIndex,
			speedUpgradeIndex = SceneSingleton<UpgradesManager>.Instance.currentSpeedUpgradeIndex,
			currentHighlightIndex = SceneSingleton<UpgradesManager>.Instance.currentHighlightIndex,
			currentShelvesHighlightIndex = SceneSingleton<UpgradesManager>.Instance.currentShelvesHighlightIndex,
			currentAssembleIndex = SceneSingleton<UpgradesManager>.Instance.currentAssembleIndex,
			isSprintUpgraded = SceneSingleton<UpgradesManager>.Instance.isSprintUpgraded,
			currentHighlightTimer = SceneSingleton<AbilitiesManager>.Instance.currentHighlightTimer,
			currentAssembleTimer = SceneSingleton<AbilitiesManager>.Instance.currentAssembleTimer,
			currentShelveHighlightMaxTimer = SceneSingleton<AbilitiesManager>.Instance.currentShelveHighlightMaxTimer,
			highlightPuzzleTimer = SceneSingleton<CatsManager>.Instance.currentHighlightPuzzleTimer,
			revealSolutionTimer = SceneSingleton<CatsManager>.Instance.currentRevealSolutionTimer,
			isCatsPettedList = SceneSingleton<CatsManager>.Instance.GetCatsPettedCount(),
			catsPettedCount = SceneSingleton<CatsManager>.Instance.catsPettedCount,
			abilitiesUsed = SceneSingleton<AbilitiesManager>.Instance.abilitiesUsed,
			solvedSortingPuzzleTypesList = Singleton<PotionsDataHolder>.Instance.solvedPuzzlesList,
			puzzleSaveData = new PuzzleSaveData()
		};
		levelSaveData.puzzleSaveData.SaveData();
		levelSaveData.potionSaveDatasList = new List<PotionSaveData>();
		foreach (PotionController potionControllers in SceneSingleton<PotionsManager>.Instance.potionControllersList)
		{
			levelSaveData.potionSaveDatasList.Add(new PotionSaveData(potionControllers));
		}
		JsonManager.SaveJsonFile(saveFileName, JsonUtility.ToJson(levelSaveData));
	}

	public static void DeleteSavedLevel()
	{
		if (isDebugOn)
		{
			UnityEngine.Debug.Log("DeleteSavedLevel");
		}
		JsonManager.DeleteJsonFile(saveFileName);
		LoadLevelSaveData();
	}

	public static void LoadLevelSaveData()
	{
		if (disableSavingSystem)
		{
			return;
		}
		string text = "";
		if (IsSaveFileAvilable(saveFileName))
		{
			if (isDebugOn)
			{
				UnityEngine.Debug.Log("LoadLevelSaveData " + saveFileName);
			}
			text = JsonManager.LoadJsonFile(saveFileName);
		}
		else
		{
			if (isDebugOn)
			{
				UnityEngine.Debug.Log("LoadLevelSaveData " + startingSaveFileName);
			}
			TextAsset textAsset = Resources.Load<TextAsset>(startingSaveFileName);
			if (textAsset == null)
			{
				UnityEngine.Debug.Log("StartingSaveFile is not avilabale in the Resources folder");
			}
			text = textAsset.text;
		}
		currentlevelSaveData = JsonUtility.FromJson<LevelSaveData>(text);
	}

	public static LevelSaveData GetStartingLevelSaveData()
	{
		return JsonUtility.FromJson<LevelSaveData>(Resources.Load<TextAsset>(startingSaveFileName).text);
	}

	public static bool IsThereASavedLevel()
	{
		return IsSaveFileAvilable(saveFileName);
	}

	public static bool GetAnalyticsUserConsentSetting()
	{
		return GetSavedBool("AnalyticsUserConsentSetting", defaultBool: true);
	}

	public static void SetAnalyticsUserConsentSetting(bool value)
	{
		SaveNewBool("AnalyticsUserConsentSetting", value);
	}

	public static bool GetTimeToggleSetting()
	{
		return GetSavedBool("InGameToggleSetting");
	}

	public static void SetTimeToggleSetting(bool value)
	{
		SaveNewBool("InGameToggleSetting", value);
	}

	public static bool GetAutoSaveToggleSetting()
	{
		return GetSavedBool("AutoSaveToggleSetting", defaultBool: true);
	}

	public static void SetAutoSaveToggleSetting(bool value)
	{
		SaveNewBool("AutoSaveToggleSetting", value);
	}

	public static bool GetPlayInBackgroundSetting()
	{
		return GetSavedBool("PlayInBackgroundSetting");
	}

	public static void SetPlayInBackgroundSetting(bool value)
	{
		SaveNewBool("PlayInBackgroundSetting", value);
	}

	public static bool GetInGameCloudSetting()
	{
		return GetSavedBool("InGameCloudSetting", defaultBool: true);
	}

	public static void SetInGameCloudSetting(bool value)
	{
		SaveNewBool("InGameCloudSetting", value);
	}

	public static bool GetInGameWindSetting()
	{
		return GetSavedBool("InGameWindSetting", defaultBool: true);
	}

	public static void SetInGameWindSetting(bool value)
	{
		SaveNewBool("InGameWindSetting", value);
	}

	public static bool GetInGameSeaSetting()
	{
		return GetSavedBool("InGameSeaSetting", defaultBool: true);
	}

	public static void SetInGameSeaSetting(bool value)
	{
		SaveNewBool("InGameSeaSetting", value);
	}

	public static int GetUISizeSetting()
	{
		return PlayerPrefs.GetInt("UISize_Setting", 0);
	}

	public static void SaveUISizeSetting(int index)
	{
		PlayerPrefs.SetInt("UISize_Setting", index);
		SavePrefs();
	}

	public static int GetResulationSetting()
	{
		return PlayerPrefs.GetInt("ResulationSetting", Singleton<ScreenSettingDataStore>.Instance.defaultResolutionIndex);
	}

	public static void SaveResulationSetting(int index)
	{
		PlayerPrefs.SetInt("ResulationSetting", index);
		SavePrefs();
	}

	public static int GetScreenModeSetting()
	{
		return PlayerPrefs.GetInt("ScreenModeSetting", Singleton<ScreenSettingDataStore>.Instance.defaultScreenIndex);
	}

	public static void SaveScreenModeSetting(int index)
	{
		PlayerPrefs.SetInt("ScreenModeSetting", index);
		SavePrefs();
	}

	public static int GetLanguageSetting(int defaultLanguage = 0)
	{
		return FBPP.GetInt("LanguagesSetting", defaultLanguage);
	}

	public static void SaveLanguageSetting(int index)
	{
		SaveNewInt("LanguagesSetting", index);
		SavePrefs();
	}

	public static bool GetVsyncSetting()
	{
		return PlayerPrefs.GetInt("VsyncSetting", 0) == 1;
	}

	public static void SetVsyncSetting(bool value)
	{
		PlayerPrefs.SetInt("VsyncSetting", value ? 1 : 0);
	}

	public static float GetBrightnessSlider()
	{
		return FBPP.GetFloat("BrightnessSliderSetting", 0.5f);
	}

	public static void SetBrightnessSlider(float vol)
	{
		SaveNewFloat("BrightnessSliderSetting", vol);
		SavePrefs();
	}

	public static float GetMasterSlider()
	{
		return FBPP.GetFloat("MasterSliderSetting", 0.5f);
	}

	public static void SetMasterSlider(float vol)
	{
		SaveNewFloat("MasterSliderSetting", vol);
		SavePrefs();
	}

	public static float GetMusicVolume()
	{
		return GetMusicSlider() * GetMasterSlider();
	}

	public static float GetMusicSlider()
	{
		return FBPP.GetFloat("MusicSliderSetting", 0.5f);
	}

	public static void SetMusicSlider(float vol)
	{
		SaveNewFloat("MusicSliderSetting", vol);
		SavePrefs();
	}

	public static float GetSFXVolume()
	{
		return GetSFXSlider() * GetMasterSlider();
	}

	public static float GetSFXSlider()
	{
		return FBPP.GetFloat("SFXSliderSetting", 0.5f);
	}

	public static void SetSFXSlider(float vol)
	{
		SaveNewFloat("SFXSliderSetting", vol);
		SavePrefs();
	}

	public static float GetEnvironmentVolume()
	{
		return GetEnvironmentSlider() * GetMasterSlider();
	}

	public static float GetEnvironmentSlider()
	{
		return FBPP.GetFloat("AmbientSliderSetting", 0.5f);
	}

	public static void SetEnvironmentSlider(float vol)
	{
		SaveNewFloat("AmbientSliderSetting", vol);
		SavePrefs();
	}

	public static float GetFOVSlider()
	{
		return FBPP.GetFloat("FOVSliderSettings", 0.2f);
	}

	public static void SetFOVSlider(float vol)
	{
		SaveNewFloat("FOVSliderSettings", vol);
		SavePrefs();
	}

	public static float GetMouseSensitivitySlider()
	{
		return FBPP.GetFloat("MouseSensitivitySliderSetting", 0.5f);
	}

	public static void SetMouseSensitivitySlider(float vol)
	{
		SaveNewFloat("MouseSensitivitySliderSetting", vol);
		SavePrefs();
	}

	public static float GetReticleSlider()
	{
		return FBPP.GetFloat("ReticleSliderSetting", 0.25f);
	}

	public static void SetReticleSlider(float vol)
	{
		SaveNewFloat("ReticleSliderSetting", vol);
		SavePrefs();
	}

	public static float GetControllerSensitivitySlider()
	{
		return FBPP.GetFloat("ControllerSensitivitySliderSetting", 0.5f);
	}

	public static void SetControllerSensitivitySlider(float vol)
	{
		SaveNewFloat("ControllerSensitivitySliderSetting", vol);
		SavePrefs();
	}

	public static bool GetHeadBobSetting()
	{
		return GetSavedBool("HeadBobSetting");
	}

	public static void SetHeadBobSetting(bool value)
	{
		SaveNewBool("HeadBobSetting", value);
	}

	public static bool GetDisableBatsSetting()
	{
		return GetSavedBool("DisableBatsSetting");
	}

	public static void SetDisableBatsSetting(bool value)
	{
		SaveNewBool("DisableBatsSetting", value);
	}

	public static bool GetHoverSolutionToggleSetting()
	{
		return GetSavedBool("HoverSolutionToggleSetting");
	}

	public static void SetHoverSolutionToggleSetting(bool value)
	{
		SaveNewBool("HoverSolutionToggleSetting", value);
	}

	public static bool GetDisableHUDSetting()
	{
		return GetSavedBool("DisableHUDSetting");
	}

	public static void SetDisableHUDSetting(bool value)
	{
		SaveNewBool("DisableHUDSetting", value);
	}

	public static bool GetDisableAbilitiesSetting()
	{
		return GetSavedBool("DisableAbilitiesSetting");
	}

	public static void SetDisableAbilitiesSetting(bool value)
	{
		SaveNewBool("DisableAbilitiesSetting", value);
	}

	public static bool GetDisableInGameTimer()
	{
		return GetSavedBool("DisableInGameTimer");
	}

	public static void SetDisableInGameTimer(bool value)
	{
		SaveNewBool("DisableInGameTimer", value);
	}

	public static bool GetInvertMouseXSetting()
	{
		return GetSavedBool("InvertMouseXSetting");
	}

	public static void SetInvertMouseXSetting(bool value)
	{
		SaveNewBool("InvertMouseXSetting", value);
	}

	public static bool GetInvertMouseYSetting()
	{
		return GetSavedBool("InvertMouseYSetting");
	}

	public static void SetInvertMouseYSetting(bool value)
	{
		SaveNewBool("InvertMouseYSetting", value);
	}

	public static bool GetInvertMouseWheelSetting()
	{
		return GetSavedBool("InvertMouseWheelSetting");
	}

	public static void SetInvertMouseWheelSetting(bool value)
	{
		SaveNewBool("InvertMouseWheelSetting", value);
	}

	public static bool GetInvertControllerXSetting()
	{
		return GetSavedBool("InvertControllerXSetting");
	}

	public static void SetInvertControllerXSetting(bool value)
	{
		SaveNewBool("InvertControllerXSetting", value);
	}

	public static bool GetInvertControllerYSetting()
	{
		return GetSavedBool("InvertControllerYSetting");
	}

	public static void SetInvertControllerYSetting(bool value)
	{
		SaveNewBool("InvertControllerYSetting", value);
	}

	public static bool GetIsColorBlindSetting()
	{
		return GetSavedBool("GetIsColorBlindSetting");
	}

	public static void SetIsColorBlindSetting(bool value)
	{
		SaveNewBool("GetIsColorBlindSetting", value);
	}

	public static void SavePlayerInput()
	{
		string value = InputDataStore.baseInputActions.SaveBindingOverridesAsJson();
		PlayerPrefs.SetString("Input", value);
		SavePrefs();
	}

	public static void LoadPlayerInput()
	{
		string text = PlayerPrefs.GetString("Input", "");
		if (text == "")
		{
			text = new PlayerInputActions().SaveBindingOverridesAsJson();
		}
		InputDataStore.baseInputActions.LoadBindingOverridesFromJson(text);
	}

	public static void ResetPlayerInput()
	{
		PlayerInputActions actions = new PlayerInputActions();
		PlayerPrefs.SetString("Input", actions.SaveBindingOverridesAsJson());
		SavePrefs();
	}

	public static void SaveNewBool(string playerPrefKey, bool booleanValue)
	{
		SaveNewInt(playerPrefKey, booleanValue ? 1 : 0);
		SavePrefs();
	}

	public static void SaveNewFloat(string playerPrefKey, float floatValue)
	{
		FBPP.SetFloat(playerPrefKey, floatValue);
		SavePrefs();
	}

	public static void SaveNewInt(string playerPrefKey, int IntValue)
	{
		FBPP.SetInt(playerPrefKey, IntValue);
		SavePrefs();
	}

	public static bool GetSavedBool(string playerPrefKey, bool defaultBool = false)
	{
		if (FBPP.GetInt(playerPrefKey, defaultBool ? 1 : 0) != 1)
		{
			return false;
		}
		return true;
	}

	private static void SavePrefs()
	{
		FBPP.Save();
		PlayerPrefs.Save();
	}

	public static void OpenSaveFolder()
	{
		string persistentDataPath = Application.persistentDataPath;
		Process.Start(new ProcessStartInfo
		{
			FileName = persistentDataPath,
			UseShellExecute = true
		});
	}
}
