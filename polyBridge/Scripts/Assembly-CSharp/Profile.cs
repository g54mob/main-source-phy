using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.Serialization;
using UnityEngine;

public class Profile
{
	public int m_Version;

	public string m_Name;

	public bool m_GridEnabled;

	public bool m_StressViewEnabled;

	public bool m_AutoTriangulateEnabled;

	public bool m_AutoDrawEnabled;

	public bool m_SnapEnabled;

	public bool m_EdgeBisectEnabled;

	public bool m_FirstBreakEnabled;

	public bool m_SortSandboxLayoutsByDate;

	public bool m_SortBridgeSavesByDate;

	public bool m_AutomatiallyLoadAutoSave;

	public bool m_PauseOnBreak;

	public bool m_CuratedReplays;

	public bool m_Replays;

	public bool m_LockBuildCamera;

	public AsyncCaptureQuality m_ReplayQuality;

	public int m_ReplayLengthSeconds;

	public string m_ReplaysFolderOverride;

	public string m_ReplaysFolderOverrideMacOS;

	public bool m_DisableTooltips;

	public bool m_DisableBuildDataTooltips;

	public bool m_DisableBuildHelpTooltips;

	public string m_LastLoadedSandbox;

	public PointOfViewType m_PointOfViewType;

	public float m_EventEditorAnchorYNormalized;

	public HashSet<PopUpWarningCategory> m_NeverShowAgain = new HashSet<PopUpWarningCategory>();

	public ArcShape m_ArcShape;

	public bool m_ArcSnapToGrid;

	public bool m_GalleryUnderBudget;

	public bool m_GalleryUnbreaking;

	public bool m_GalleryShowOnlyWins;

	public bool m_GalleryIncludeCheats;

	public GallerySortBy m_GallerySortBy;

	public string m_LanguageCode;

	public bool m_ColorBlindModeOn;

	public float m_MouseWheelSpeedNormalized;

	public float m_CameraRotateSpeedNormalized;

	public float m_CameraPanSpeedNormalized;

	public int m_MasterVolume;

	public int m_AmbientVolume;

	public int m_SFXVolume;

	public int m_MusicVolume;

	public int m_UIVolume;

	public bool m_Mute;

	public bool m_MuteInBackground;

	public bool m_OverBudgetAlert;

	public bool m_FullScreen;

	public bool m_VehicleLights;

	public bool m_SSAO;

	public bool m_Vignette;

	public bool m_CustomResolution;

	public bool m_Bloom;

	public bool m_Vsync;

	public bool m_TerrainLights;

	public ShadowResolution m_ShadowResolution;

	public AntiAliasingQuality m_AntiAliasingQuality;

	public int m_ScreenWidth;

	public int m_ScreenHeight;

	public int m_VsyncInterval;

	public string m_TwitterToken;

	public string m_TwitterUsername;

	public string m_RedditRefreshToken;

	public string m_RedditUsername;

	public string m_LastSolvedCampaignLevelId;

	public string m_LastLoadedCampaignLevelId;

	public string m_LastMainMenuThemeId;

	public bool m_SeenMainMenu;

	public bool m_DismissedAutoTriangulateHelpArrow;

	public bool m_DismissedFoundationHelpArrow;

	public WorkshopSortOrder m_WorkshopItemsSortBy;

	public WorkshopSortOrder m_WorkshopModItemsSortBy;

	public bool m_HasClickedChallenges;

	public LeaderboardsView m_LeaderboardsViewExtended;

	public LeaderboardsFilter m_LeaderboardsFilterExtended;

	public LeaderboardsView m_LeaderboardsView;

	public LeaderboardsFilter m_LeaderboardsFilter;

	public bool m_HideOtherPlayerSolutions;

	public bool m_HideOtherLeaderboards;

	public bool m_AutoPlayOnly;

	public bool m_ExcludeAutoPlay;

	public HashSet<string> m_OpenedWeeklyChallengeItemIds = new HashSet<string>();

	public HashSet<string> m_FiveStarUnlocks = new HashSet<string>();

	public List<string> m_ActiveModDirectories = new List<string>();

	public List<string> m_ActiveLocalModDirectories = new List<string>();

	public Dictionary<string, string> m_LastPlayedLevelIDs = new Dictionary<string, string>();

	public Dictionary<string, string> m_MostRecentSimChecksums = new Dictionary<string, string>();

	public string m_AvatarAddressable;

	public string m_AvatarSkin;

	public bool m_GodMode;

	public bool m_ShowDecor;

	public bool m_DidCrashOnModLoad;

	public bool m_FollowCar;

	public string m_TwitchUsername;

	public bool m_TwitchSuscribersOnly;

	public bool m_TwitchModerated;

	public bool m_TwitchAutoPlay;

	public bool m_TwitchAutoAdvance;

	public bool m_TwitchAllowSuggestions;

	public bool m_TwitchBitsEnabled;

	public bool m_TwitchBitsMandatory;

	public int m_TwitchViewerCooldownSeconds;

	public Vector2 m_TwitchStreamerWindowPos;

	public bool m_TwitchStreamerWindowCollapsed;

	public float m_TwitchStreamerWindowHeight;

	public Vector2 m_TwitchAuthorPanelPos;

	public bool m_LeaderboardsNoSubmit;

	public bool m_BlockGamepadInput;

	public bool m_GamepadAcceleration;

	public float m_GamepadCursorSpeedNormalized;

	public float m_GamepadRotateCameraSpeedNormalized;

	public float m_GamepadZoomSpeedNormalized;

	public GamepadButtonIconsChoice m_GamepadButtonIconsChoice;

	public UIScaleMode m_UIScaleMode;

	public float m_UIScaleFactor;

	public void Init(string profileName)
	{
		m_Version = Profiles.CURRENT_VERSION;
		m_Name = profileName;
		m_GridEnabled = true;
		m_StressViewEnabled = false;
		m_AutoTriangulateEnabled = false;
		m_AutoDrawEnabled = false;
		m_SnapEnabled = true;
		m_SortSandboxLayoutsByDate = false;
		m_SortBridgeSavesByDate = false;
		m_AutomatiallyLoadAutoSave = true;
		m_PauseOnBreak = false;
		m_CuratedReplays = false;
		m_Replays = SystemInfo.systemMemorySize / 1000 >= 4;
		m_ReplayQuality = AsyncCaptureQuality.HIGH;
		m_ReplayLengthSeconds = Replays.DEFAULT_SECONDS_PER_REPLAY;
		m_ReplaysFolderOverride = Replays.GetDefaultReplaysPath();
		m_DisableTooltips = false;
		m_DisableBuildDataTooltips = false;
		m_DisableBuildHelpTooltips = false;
		m_LastLoadedSandbox = string.Empty;
		m_PointOfViewType = PointOfViewType.SIM_LEFT;
		m_EventEditorAnchorYNormalized = EventEditor.DEFAULT_ANCHOR_Y / EventEditor.MAX_ANCHOR_Y;
		m_NeverShowAgain = new HashSet<PopUpWarningCategory>();
		m_ArcShape = ArcShape.CURVED;
		m_ArcSnapToGrid = false;
		m_GalleryUnderBudget = false;
		m_GalleryUnbreaking = false;
		m_GalleryShowOnlyWins = false;
		m_GalleryIncludeCheats = false;
		m_GallerySortBy = GallerySortBy.MOST_RECENT;
		m_TwitchAllowSuggestions = true;
		m_TwitchSuscribersOnly = false;
		m_TwitchModerated = false;
		m_TwitchAutoPlay = false;
		m_TwitchAutoAdvance = true;
		m_TwitchBitsEnabled = false;
		m_TwitchBitsMandatory = false;
		m_TwitchViewerCooldownSeconds = 5;
		m_TwitchStreamerWindowHeight = PolyTwitch.DEFAULT_STREAMER_WINDOW_HEIGHT;
		m_TwitchStreamerWindowPos = PolyTwitch.DEFAULT_STREAMER_WINDOW_POS;
		m_TwitchStreamerWindowCollapsed = false;
		m_TwitchAuthorPanelPos = PolyTwitch.DEFAULT_AUTHOR_WINDOW_POS;
		m_TwitterToken = string.Empty;
		m_TwitterUsername = string.Empty;
		m_RedditRefreshToken = string.Empty;
		m_RedditUsername = string.Empty;
		m_MasterVolume = AudioMixerManager.DEFAULT_MASTER_VOLUME;
		m_AmbientVolume = AudioMixerManager.DEFAULT_AMBIENT_VOLUME;
		m_SFXVolume = AudioMixerManager.DEFAULT_SFX_VOLUME;
		m_MusicVolume = AudioMixerManager.DEFAULT_MUSIC_VOLUME;
		m_UIVolume = AudioMixerManager.DEFAULT_UI_VOLUME;
		m_Mute = false;
		m_MuteInBackground = false;
		m_OverBudgetAlert = true;
		m_VehicleLights = true;
		m_Bloom = true;
		m_SSAO = true;
		m_Vignette = true;
		m_CustomResolution = false;
		m_Vsync = true;
		m_TerrainLights = true;
		m_ShadowResolution = ShadowResolution.HIGH;
		m_AntiAliasingQuality = AntiAliasingQuality.QUALITY;
		m_FullScreen = true;
		m_VsyncInterval = 1;
		m_ScreenWidth = Screen.currentResolution.width;
		m_ScreenHeight = Screen.currentResolution.height;
		m_LanguageCode = Localize.GetSystemLanguageCode();
		m_ColorBlindModeOn = false;
		m_LastSolvedCampaignLevelId = string.Empty;
		m_LastLoadedCampaignLevelId = string.Empty;
		m_LastMainMenuThemeId = string.Empty;
		m_SeenMainMenu = false;
		m_DismissedAutoTriangulateHelpArrow = false;
		m_DismissedFoundationHelpArrow = false;
		m_WorkshopItemsSortBy = WorkshopSortOrder.MOST_LIKED;
		m_WorkshopModItemsSortBy = WorkshopSortOrder.MOST_LIKED;
		m_HasClickedChallenges = false;
		m_CameraRotateSpeedNormalized = GameSettings.DefaultCameraRotateSpeedNormalized();
		m_CameraPanSpeedNormalized = GameSettings.DefaultCameraPanSpeedNormalized();
		m_MouseWheelSpeedNormalized = GameSettings.DefaultMouseWheelSpeedNormalized();
		m_BlockGamepadInput = false;
		m_GamepadAcceleration = true;
		m_GamepadCursorSpeedNormalized = GamepadManager.GetDefaultCursorSpeedNormalized();
		m_GamepadRotateCameraSpeedNormalized = GamepadManager.GetDefaultRotateCameraSpeedNormalized();
		m_GamepadZoomSpeedNormalized = GamepadManager.GetDefaultZoomSpeedNormalized();
		m_GamepadButtonIconsChoice = GamepadButtonIconsChoice.DETECTED;
		m_UIScaleMode = UIScaleMode.SCALE_WITH_SCREEN_SIZE;
		m_UIScaleFactor = 1f;
		m_LeaderboardsViewExtended = LeaderboardsView.AROUND_YOU;
		m_LeaderboardsFilterExtended = LeaderboardsFilter.ALL;
		m_LeaderboardsView = LeaderboardsView.AROUND_YOU;
		m_LeaderboardsFilter = LeaderboardsFilter.ALL;
		m_HideOtherPlayerSolutions = false;
		m_HideOtherLeaderboards = false;
		m_AutoPlayOnly = false;
		m_ExcludeAutoPlay = false;
		m_OpenedWeeklyChallengeItemIds = new HashSet<string>();
		m_FiveStarUnlocks = new HashSet<string>();
		m_ActiveModDirectories = new List<string>();
		m_EdgeBisectEnabled = true;
		m_FirstBreakEnabled = true;
		m_DidCrashOnModLoad = false;
		m_FollowCar = false;
		m_LeaderboardsNoSubmit = false;
		m_AvatarAddressable = Profiles.DEFAULT_AVATAR_ADDRESSABLE;
		m_AvatarSkin = Profiles.DEFAULT_AVATAR_SKIN;
		Bindings.ResetToDefaults();
	}

	public void Apply()
	{
		ApplyVolumes();
		if (!Game.IsRunningOnSteamDeck())
		{
			ApplyResolution();
		}
		ApplyGraphicsSettings();
		ApplyReplaySettings();
		ApplyGamepadSettings();
		Mods.SetActiveModsFromProfile();
		Localize.SwitchToLanguage(m_LanguageCode);
	}

	public bool Write()
	{
		string profileDirectory = Profiles.GetProfileDirectory(m_Name);
		Utils.CreateDirectory(profileDirectory);
		if (!Directory.Exists(profileDirectory))
		{
			return false;
		}
		string profileDirectoryNoSync = Profiles.GetProfileDirectoryNoSync(m_Name);
		Utils.CreateDirectory(profileDirectoryNoSync);
		if (!Directory.Exists(profileDirectoryNoSync))
		{
			return false;
		}
		try
		{
			byte[] array = SerializationUtility.SerializeValue(new ProfileProxy(this, Profiles.CURRENT_VERSION), DataFormat.JSON);
			if (array.Length != 0 && array[0] != 0)
			{
				Utils.WriteBytesWithBackup(profileDirectory, Profiles.PROFILE_SETTINGS_FILENAME, array);
			}
			byte[] array2 = SerializationUtility.SerializeValue(new ProfileProxyNoSync(this, Profiles.CURRENT_VERSION), DataFormat.JSON);
			if (array2.Length != 0 && array2[0] != 0)
			{
				Utils.WriteBytesWithBackup(profileDirectoryNoSync, Profiles.PROFILE_SETTINGS_FILENAME, array2);
			}
			return true;
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("Exception {0} when trying to write profile", ex.Message);
			return false;
		}
	}

	public bool Load()
	{
		string profileDirectoryNoSync = Profiles.GetProfileDirectoryNoSync(m_Name);
		string profileDirectory = Profiles.GetProfileDirectory(m_Name);
		if (!Directory.Exists(profileDirectory))
		{
			return false;
		}
		string text = Path.Combine(profileDirectory, Profiles.PROFILE_SETTINGS_FILENAME);
		string text2 = Path.Combine(profileDirectoryNoSync, Profiles.PROFILE_SETTINGS_FILENAME);
		bool flag = TryLoadProfile(text, text2);
		if (!flag)
		{
			text = Path.ChangeExtension(text, ".restore");
			text2 = Path.ChangeExtension(text2, ".restore");
			flag = TryLoadProfile(text, text2);
		}
		return flag;
	}

	private bool TryLoadProfile(string filepath, string filepathNoSync)
	{
		try
		{
			ProfileProxy profileProxy = null;
			ProfileProxyNoSync proxyNoSync = null;
			if (File.Exists(filepath))
			{
				byte[] array = File.ReadAllBytes(filepath);
				if (array != null && array.Length != 0 && array[0] != 0)
				{
					profileProxy = SerializationUtility.DeserializeValue<ProfileProxy>(array, DataFormat.JSON);
				}
			}
			if (File.Exists(filepathNoSync))
			{
				byte[] array2 = File.ReadAllBytes(filepathNoSync);
				if (array2 != null && array2.Length != 0 && array2[0] != 0)
				{
					proxyNoSync = SerializationUtility.DeserializeValue<ProfileProxyNoSync>(array2, DataFormat.JSON);
				}
			}
			if (profileProxy == null)
			{
				return false;
			}
			DeserializeProxy(profileProxy, proxyNoSync);
			return true;
		}
		catch (Exception ex)
		{
			Debug.LogFormat("Caught exception reading profile: {0}", ex.Message.ToString());
		}
		return false;
	}

	public void DeserializeProxy(ProfileProxy proxy, ProfileProxyNoSync proxyNoSync)
	{
		m_LastLoadedSandbox = proxy.m_LastLoadedSandbox;
		m_GridEnabled = proxy.m_Version < 2 || proxy.m_GridEnabled;
		m_PointOfViewType = ((proxy.m_Version < 3) ? PointOfViewType.SIM_LEFT : proxy.m_PointOfViewType);
		m_EventEditorAnchorYNormalized = Mathf.Clamp(proxy.m_EventEditorAnchorYNormalized, EventEditor.DEFAULT_ANCHOR_Y / EventEditor.MAX_ANCHOR_Y, 1f);
		m_StressViewEnabled = proxy.m_Version >= 5 && proxy.m_StressViewEnabled;
		m_AutoTriangulateEnabled = proxy.m_Version < 62 || proxy.m_AutoTriangulateEnabled;
		m_AutoDrawEnabled = false;
		m_SnapEnabled = proxy.m_Version < 106 || proxy.m_SnapEnabled;
		m_SortSandboxLayoutsByDate = proxy.m_Version >= 7 && proxy.m_SortSandboxLayoutByDate;
		m_SortBridgeSavesByDate = proxy.m_Version >= 7 && proxy.m_SortBridgeSavesByDate;
		m_AutomatiallyLoadAutoSave = proxy.m_Version < 7 || proxy.m_AutomatiallyLoadAutoSave;
		m_PauseOnBreak = proxy.m_PauseOnBreak;
		m_EdgeBisectEnabled = proxy.m_Version < 77 || proxy.m_EdgeBisectEnabled;
		m_FirstBreakEnabled = proxy.m_Version < 116 || proxy.m_FirstBreakEnabled;
		m_LockBuildCamera = proxy.m_LockBuildCamera;
		m_DisableTooltips = proxy.m_Version >= 66 && proxy.m_DisableTooltips;
		m_DisableBuildDataTooltips = proxy.m_Version >= 91 && proxy.m_DisableBuildDataTooltips;
		m_DisableBuildHelpTooltips = proxy.m_Version >= 91 && proxy.m_DisableBuildHelpTooltips;
		m_ArcShape = ((proxy.m_Version >= 17) ? proxy.m_ArcShape : ArcShape.CURVED);
		m_ArcSnapToGrid = proxy.m_Version >= 114 && proxy.m_ArcSnapToGrid;
		m_GalleryUnderBudget = proxy.m_GalleryUnderBudget;
		m_GalleryUnbreaking = proxy.m_GalleryUnbreaking;
		m_GalleryShowOnlyWins = proxy.m_GalleryShowOnlyWins;
		m_GalleryIncludeCheats = proxy.m_GalleryIncludeCheats;
		m_GallerySortBy = ((proxy.m_Version >= 100) ? proxy.m_GallerySortBy : GallerySortBy.MOST_RECENT);
		m_LastSolvedCampaignLevelId = ((proxy.m_Version < 30) ? string.Empty : proxy.m_LastSolvedCampaignLevelId);
		m_LastLoadedCampaignLevelId = ((proxy.m_Version < 46) ? string.Empty : proxy.m_LastLoadedCampaignLevelId);
		m_SeenMainMenu = proxy.m_Version >= 32 && proxy.m_SeenMainMenu;
		m_DismissedAutoTriangulateHelpArrow = proxy.m_Version >= 93 && proxy.m_DismissedAutoTriangulateHelpArrow;
		m_DismissedFoundationHelpArrow = proxy.m_Version >= 94 && proxy.m_DismissedFoundationHelpArrow;
		m_WorkshopItemsSortBy = ((proxy.m_Version < 34) ? WorkshopSortOrder.MOST_LIKED : proxy.m_WorkshopItemsSortBy);
		m_WorkshopModItemsSortBy = ((proxy.m_Version < 115) ? WorkshopSortOrder.MOST_LIKED : proxy.m_WorkshopModItemsSortBy);
		m_HasClickedChallenges = proxy.m_Version >= 35 && proxy.m_HasClickedChallenges;
		m_LastMainMenuThemeId = ((proxy.m_Version < 66) ? string.Empty : proxy.m_LastMainMenuThemeId);
		m_TwitterToken = ((proxy.m_Version < 25) ? string.Empty : proxy.m_TwitterToken);
		m_TwitterUsername = ((proxy.m_Version < 25) ? string.Empty : proxy.m_TwitterUsername);
		m_RedditRefreshToken = ((proxy.m_Version < 25) ? string.Empty : proxy.m_RedditRefreshToken);
		m_RedditUsername = ((proxy.m_Version < 25) ? string.Empty : proxy.m_RedditUsername);
		m_LanguageCode = ((proxy.m_Version < 98) ? Localize.GetSystemLanguageCode() : proxy.m_LanguageCode);
		m_ColorBlindModeOn = proxy.m_Version >= 16 && proxy.m_ColorBlindModeOn;
		m_MouseWheelSpeedNormalized = Mathf.Clamp01((proxy.m_Version < 16) ? GameSettings.DefaultMouseWheelSpeedNormalized() : proxy.m_MouseWheelSpeedNormalized);
		if (proxy.m_Version >= 16 && proxy.m_Version < 56)
		{
			m_MouseWheelSpeedNormalized *= 0.1f;
		}
		m_CameraRotateSpeedNormalized = ((proxy.m_Version < 16) ? GameSettings.DefaultCameraRotateSpeedNormalized() : proxy.m_CameraRotateSpeedNormalized);
		m_CameraPanSpeedNormalized = ((proxy.m_Version < 16) ? GameSettings.DefaultCameraPanSpeedNormalized() : proxy.m_CameraPanSpeedNormalized);
		if (proxyNoSync == null)
		{
			m_MasterVolume = ((proxy.m_Version < 11 || proxy.m_Version > 102) ? AudioMixerManager.DEFAULT_MASTER_VOLUME : proxy.m_MasterVolume);
			m_AmbientVolume = ((proxy.m_Version < 79 || proxy.m_Version > 102) ? AudioMixerManager.DEFAULT_AMBIENT_VOLUME : proxy.m_AmbientVolume);
			m_SFXVolume = ((proxy.m_Version < 11 || proxy.m_Version > 102) ? AudioMixerManager.DEFAULT_SFX_VOLUME : proxy.m_SFXVolume);
			m_MusicVolume = ((proxy.m_Version < 11 || proxy.m_Version > 102) ? AudioMixerManager.DEFAULT_MUSIC_VOLUME : proxy.m_MusicVolume);
			m_UIVolume = ((proxy.m_Version < 22 || proxy.m_Version > 102) ? AudioMixerManager.DEFAULT_UI_VOLUME : proxy.m_UIVolume);
			m_Mute = proxy.m_Version >= 78 && proxy.m_Version <= 102 && proxy.m_Mute;
			m_MuteInBackground = proxy.m_Version >= 78 && proxy.m_Version <= 102 && proxy.m_MuteInBackground;
			m_OverBudgetAlert = true;
		}
		else
		{
			m_MasterVolume = proxyNoSync.m_MasterVolume;
			m_AmbientVolume = proxyNoSync.m_AmbientVolume;
			m_SFXVolume = proxyNoSync.m_SFXVolume;
			m_MusicVolume = proxyNoSync.m_MusicVolume;
			m_UIVolume = proxyNoSync.m_UIVolume;
			m_Mute = proxyNoSync.m_Mute;
			m_MuteInBackground = proxyNoSync.m_Version >= 116 && proxyNoSync.m_MuteInBackground;
			m_OverBudgetAlert = proxyNoSync.m_OverBudgetAlert;
		}
		if (proxyNoSync == null)
		{
			m_FullScreen = proxy.m_Version < 12 || proxy.m_Version > 102 || proxy.m_FullScreen;
			m_VsyncInterval = ((proxy.m_Version < 95 || proxy.m_Version > 102) ? Game.GetDefaultVsyncInterval() : proxy.m_VsyncInterval);
			m_ScreenWidth = ((proxy.m_Version < 12 || proxy.m_Version > 102) ? Screen.width : proxy.m_ScreenWidth);
			m_ScreenHeight = ((proxy.m_Version < 12 || proxy.m_Version > 102) ? Screen.height : proxy.m_ScreenHeight);
			m_Bloom = proxy.m_Version < 50 || proxy.m_Version > 102 || proxy.m_Bloom;
			m_SSAO = true;
			m_Vignette = proxy.m_Version < 71 || proxy.m_Version > 102 || proxy.m_Vignette;
			m_CustomResolution = proxy.m_Version >= 104 && proxy.m_CustomResolution;
			m_VehicleLights = proxy.m_Version < 65 || proxy.m_Version > 102 || proxy.m_VehicleLights;
			m_Vsync = proxy.m_Version < 57 || proxy.m_Version > 102 || proxy.m_Vsync;
			m_ShadowResolution = ((proxy.m_Version < 50 || proxy.m_Version > 102) ? ShadowResolution.HIGH : proxy.m_ShadowResolution);
			m_AntiAliasingQuality = ((proxy.m_Version < 50 || proxy.m_Version > 102) ? AntiAliasingQuality.QUALITY : proxy.m_AntiAliasingQuality);
			m_TerrainLights = proxy.m_Version < 76 || proxy.m_Version > 102 || proxy.m_TerrainLights;
			m_UIScaleMode = ((proxy.m_Version >= 113) ? proxy.m_UIScaleMode : UIScaleMode.SCALE_WITH_SCREEN_SIZE);
			m_UIScaleFactor = ((proxy.m_Version < 113) ? 1f : proxy.m_UIScaleFactor);
		}
		else
		{
			m_FullScreen = proxyNoSync.m_FullScreen;
			m_VsyncInterval = proxyNoSync.m_VsyncInterval;
			m_ScreenWidth = proxyNoSync.m_ScreenWidth;
			m_ScreenHeight = proxyNoSync.m_ScreenHeight;
			m_Bloom = proxyNoSync.m_Bloom;
			m_SSAO = true;
			m_Vignette = proxyNoSync.m_Vignette;
			m_CustomResolution = proxyNoSync.m_CustomResolution;
			m_VehicleLights = proxyNoSync.m_VehicleLights;
			m_Vsync = proxyNoSync.m_Vsync;
			m_ShadowResolution = proxyNoSync.m_ShadowResolution;
			m_AntiAliasingQuality = proxyNoSync.m_AntiAliasingQuality;
			m_TerrainLights = proxyNoSync.m_TerrainLights;
			m_UIScaleMode = ((proxy.m_Version >= 113) ? proxyNoSync.m_UIScaleMode : UIScaleMode.SCALE_WITH_SCREEN_SIZE);
			m_UIScaleFactor = ((proxy.m_Version < 113) ? 1f : proxyNoSync.m_UIScaleFactor);
		}
		if (Mathf.Approximately(m_UIScaleFactor, 0f))
		{
			m_UIScaleFactor = 1f;
		}
		if (proxyNoSync == null || proxy.m_Version < 110)
		{
			m_Replays = proxy.m_Version < 52 || proxy.m_Replays;
			m_ReplayQuality = ((proxy.m_Version < 69) ? AsyncCaptureQuality.HIGH : proxy.m_ReplayQuality);
			m_ReplayLengthSeconds = ((proxy.m_Version < 88) ? Replays.DEFAULT_SECONDS_PER_REPLAY : proxy.m_ReplayLengthSeconds);
			m_ReplaysFolderOverride = ((proxy.m_Version < 92) ? Replays.GetDefaultReplaysPath() : proxy.m_ReplaysFolderOverride);
		}
		else
		{
			m_Replays = proxyNoSync.m_Replays;
			m_ReplayQuality = proxyNoSync.m_ReplayQuality;
			m_ReplayLengthSeconds = proxyNoSync.m_ReplayLengthSeconds;
			m_ReplaysFolderOverride = proxyNoSync.m_ReplaysFolderOverride;
		}
		if (proxyNoSync == null || proxy.m_Version < 112)
		{
			m_CuratedReplays = proxy.m_Version >= 112 && proxy.m_CuratedReplays;
		}
		else
		{
			m_CuratedReplays = proxyNoSync.m_CuratedReplays;
		}
		if (!string.IsNullOrEmpty(m_ReplaysFolderOverride))
		{
			m_ReplaysFolderOverride = Path.GetFullPath(m_ReplaysFolderOverride);
		}
		if (proxyNoSync == null || proxy.m_Version < 111)
		{
			m_BlockGamepadInput = proxy.m_Version >= 105 && proxy.m_BlockGamepadInput;
			m_GamepadAcceleration = proxy.m_Version < 111 || proxy.m_GamepadAcceleration;
			m_GamepadCursorSpeedNormalized = ((proxy.m_Version < 105) ? GamepadManager.GetDefaultCursorSpeedNormalized() : proxy.m_GamepadCursorSpeedNormalized);
			m_GamepadZoomSpeedNormalized = ((proxy.m_Version < 105) ? GamepadManager.GetDefaultZoomSpeedNormalized() : proxy.m_GamepadZoomSpeedNormalized);
			m_GamepadButtonIconsChoice = ((proxy.m_Version >= 107) ? proxy.m_GamepadButtonIconsChoice : GamepadButtonIconsChoice.DETECTED);
			m_GamepadRotateCameraSpeedNormalized = ((proxy.m_Version < 110) ? GamepadManager.GetDefaultRotateCameraSpeedNormalized() : proxy.m_GamepadRotateCameraSpeedNormalized);
		}
		else
		{
			m_BlockGamepadInput = proxyNoSync.m_BlockGamepadInput;
			m_GamepadAcceleration = proxyNoSync.m_GamepadAcceleration;
			m_GamepadCursorSpeedNormalized = ((proxyNoSync.m_GamepadCursorSpeedNormalized == 0f) ? GamepadManager.GetDefaultCursorSpeedNormalized() : proxyNoSync.m_GamepadCursorSpeedNormalized);
			m_GamepadRotateCameraSpeedNormalized = ((proxyNoSync.m_GamepadRotateCameraSpeedNormalized == 0f) ? GamepadManager.GetDefaultRotateCameraSpeedNormalized() : proxyNoSync.m_GamepadRotateCameraSpeedNormalized);
			m_GamepadZoomSpeedNormalized = ((proxyNoSync.m_GamepadZoomSpeedNormalized == 0f) ? GamepadManager.GetDefaultZoomSpeedNormalized() : proxyNoSync.m_GamepadZoomSpeedNormalized);
			m_GamepadButtonIconsChoice = proxyNoSync.m_GamepadButtonIconsChoice;
		}
		if (proxy.m_Version >= 13)
		{
			ApplyBindings(proxy.m_Bindings);
		}
		if (proxy.m_Version >= 9)
		{
			m_NeverShowAgain = new HashSet<PopUpWarningCategory>(proxy.m_NeverShowAgain);
		}
		m_HideOtherPlayerSolutions = proxy.m_Version >= 51 && proxy.m_HideOtherPlayerSolutions;
		m_HideOtherLeaderboards = proxy.m_Version >= 87 && proxy.m_HideOtherLeaderboards;
		if (proxy.m_Version >= 101)
		{
			m_OpenedWeeklyChallengeItemIds = proxy.m_OpenedWeeklyChallengeItemIds;
		}
		if (proxy.m_Version >= 64)
		{
			m_AutoPlayOnly = proxy.m_AutoPlayOnly;
			m_ExcludeAutoPlay = proxy.m_ExcludeAutoPlay;
		}
		if (proxy.m_Version >= 70)
		{
			m_LeaderboardsViewExtended = proxy.m_LeaderboardsViewExtended;
			m_LeaderboardsFilterExtended = proxy.m_LeaderboardsFilterExtended;
			m_LeaderboardsView = proxy.m_LeaderboardsView;
			m_LeaderboardsFilter = proxy.m_LeaderboardsFilter;
		}
		if (proxy.m_Version >= 72)
		{
			m_ActiveModDirectories = proxy.m_ActiveModDirectories;
		}
		else
		{
			m_ActiveModDirectories = new List<string>();
		}
		if (proxy.m_Version >= 89)
		{
			m_ActiveLocalModDirectories = proxy.m_ActiveLocalModDirectories;
		}
		else
		{
			m_ActiveLocalModDirectories = new List<string>();
		}
		if (proxy.m_Version >= 73)
		{
			m_LastPlayedLevelIDs = proxy.m_LastPlayedLevelIDs;
		}
		if (proxy.m_Version > 86)
		{
			m_MostRecentSimChecksums = proxy.m_MostRecentSimChecksums;
		}
		if (proxy.m_Version >= 96)
		{
			m_FiveStarUnlocks = proxy.m_FiveStarUnlocks;
		}
		if (m_FiveStarUnlocks == null)
		{
			m_FiveStarUnlocks = new HashSet<string>();
		}
		m_TwitchUsername = ((proxy.m_Version < 90) ? string.Empty : proxy.m_TwitchUsername);
		m_TwitchSuscribersOnly = proxy.m_Version >= 90 && proxy.m_TwitchSuscribersOnly;
		m_TwitchModerated = proxy.m_Version >= 90 && proxy.m_TwitchModerated;
		m_TwitchAutoPlay = proxy.m_Version >= 90 && proxy.m_TwitchAutoPlay;
		m_TwitchAutoAdvance = proxy.m_Version < 90 || proxy.m_TwitchAutoAdvance;
		m_TwitchAllowSuggestions = proxy.m_Version < 90 || proxy.m_TwitchAllowSuggestions;
		m_TwitchBitsEnabled = proxy.m_Version >= 90 && proxy.m_TwitchBitsEnabled;
		m_TwitchBitsMandatory = proxy.m_Version >= 90 && proxy.m_TwitchBitsMandatory;
		m_TwitchViewerCooldownSeconds = ((proxy.m_Version < 90) ? 5 : proxy.m_TwitchViewerCooldownSeconds);
		m_TwitchStreamerWindowPos = ((proxy.m_Version < 90) ? PolyTwitch.DEFAULT_STREAMER_WINDOW_POS : proxy.m_TwitchStreamerWindowPos);
		m_TwitchStreamerWindowCollapsed = proxy.m_Version >= 90 && proxy.m_TwitchStreamerWindowCollapsed;
		m_TwitchStreamerWindowHeight = ((proxy.m_Version < 90) ? PolyTwitch.DEFAULT_STREAMER_WINDOW_HEIGHT : proxy.m_TwitchStreamerWindowHeight);
		m_TwitchAuthorPanelPos = ((proxy.m_Version < 90) ? PolyTwitch.DEFAULT_AUTHOR_WINDOW_POS : proxy.m_TwitchAuthorPanelPos);
		m_AvatarAddressable = ((proxy.m_Version < 82) ? Profiles.DEFAULT_AVATAR_ADDRESSABLE : proxy.m_AvatarAddressable);
		m_AvatarSkin = ((proxy.m_Version < 82) ? Profiles.DEFAULT_AVATAR_SKIN : proxy.m_AvatarSkin);
		if (string.IsNullOrEmpty(m_AvatarAddressable))
		{
			m_AvatarAddressable = Profiles.DEFAULT_AVATAR_ADDRESSABLE;
		}
		if (string.IsNullOrEmpty(m_AvatarSkin))
		{
			m_AvatarSkin = Profiles.DEFAULT_AVATAR_SKIN;
		}
		m_DidCrashOnModLoad = proxy.m_Version >= 83 && proxy.m_DidCrashOnModLoad;
		m_FollowCar = proxy.m_Version >= 85 && proxy.m_FollowCar;
		m_LeaderboardsNoSubmit = proxy.m_Version >= 99 && proxy.m_LeaderboardsNoSubmit;
	}

	public void CopyAudioSettings(Profile source)
	{
		m_MasterVolume = source.m_MasterVolume;
		m_AmbientVolume = source.m_AmbientVolume;
		m_MusicVolume = source.m_MusicVolume;
		m_SFXVolume = source.m_SFXVolume;
		m_UIVolume = source.m_UIVolume;
	}

	public void CopyGraphicsSettings(Profile source)
	{
		m_ScreenWidth = source.m_ScreenWidth;
		m_ScreenHeight = source.m_ScreenHeight;
		m_FullScreen = source.m_FullScreen;
		m_VsyncInterval = source.m_VsyncInterval;
		m_Vsync = source.m_Vsync;
		m_ShadowResolution = source.m_ShadowResolution;
		m_AntiAliasingQuality = source.m_AntiAliasingQuality;
		m_SSAO = source.m_SSAO;
		m_Bloom = source.m_Bloom;
	}

	private void ApplyResolution()
	{
		try
		{
			bool flag = false;
			if (m_ScreenWidth == 0 || m_ScreenHeight == 0)
			{
				m_ScreenWidth = Screen.currentResolution.width;
				m_ScreenHeight = Screen.currentResolution.height;
				m_FullScreen = Screen.fullScreen;
				flag = true;
			}
			if ((Profiles.m_ActiveProfile == null || !Profiles.m_ActiveProfile.m_CustomResolution || Profiles.m_ActiveProfile.m_FullScreen) && (flag || Screen.currentResolution.width != m_ScreenWidth || Screen.currentResolution.height != m_ScreenHeight || Screen.fullScreen != m_FullScreen))
			{
				Screen.SetResolution(m_ScreenWidth, m_ScreenHeight, m_FullScreen);
			}
		}
		catch (Exception ex)
		{
			Debug.Log("HANDLED: " + ex.Message);
		}
	}

	private void ApplyGraphicsSettings()
	{
		try
		{
			GameRenderSettings.SetQualitySettings(m_Vsync, m_VsyncInterval, m_ShadowResolution);
			GameRenderSettings.SetPostFXSettings(m_SSAO, m_Bloom, m_Vignette, m_AntiAliasingQuality);
			if (!Game.IsRunningOnSteamDeck())
			{
				GameUI.ApplyUIScaleMode(Profiles.m_ActiveProfile.m_UIScaleMode);
				GameUI.ApplyUIScaleFactor(Profiles.m_ActiveProfile.m_UIScaleFactor);
			}
		}
		catch (Exception ex)
		{
			Debug.Log("HANDLED: " + ex.Message);
		}
	}

	private void ApplyBindings(List<BindingProxy> bindings)
	{
		foreach (BindingProxy binding2 in bindings)
		{
			Binding binding = Bindings.GetBinding(binding2.m_BindingType);
			if (binding != null)
			{
				binding.m_KeyCode = binding2.m_KeyCode;
				binding.m_AltKeyCode = binding2.m_AltKeyCode;
			}
		}
	}

	private void ApplyVolumes()
	{
		try
		{
			AudioVolume.Mute(Profiles.m_ActiveProfile.m_Mute);
			AudioVolume.MuteInBackground(Profiles.m_ActiveProfile.m_MuteInBackground);
		}
		catch (Exception ex)
		{
			Debug.Log("HANDLED: " + ex.Message);
		}
	}

	public void ApplyReplaySettings()
	{
		try
		{
			if (Profiles.m_ActiveProfile.m_Replays && !Cameras.m_AsyncCapture.m_Initialized)
			{
				Cameras.m_AsyncCapture.Init(Profiles.m_ActiveProfile.m_ReplayQuality, Profiles.m_ActiveProfile.m_ReplayLengthSeconds);
			}
		}
		catch (Exception ex)
		{
			Debug.Log("HANDLED: " + ex.Message);
		}
	}

	public void ApplyGamepadSettings()
	{
		try
		{
			if (Profiles.m_ActiveProfile.m_BlockGamepadInput)
			{
				GameInput.ChangeActiveGameDevice(GameDevice.KeyboardAndMouse);
			}
		}
		catch (Exception ex)
		{
			Debug.Log("HANDLED: " + ex.Message);
		}
	}

	public string GetLastPlayedLevelIDForWorld(string worldID)
	{
		if (m_LastPlayedLevelIDs.ContainsKey(worldID))
		{
			return m_LastPlayedLevelIDs[worldID];
		}
		return string.Empty;
	}

	public void SetLastPlayedLevelIDForWorld(string levelID)
	{
		CampaignWorld worldWithLevelId = CampaignWorlds.m_Instance.GetWorldWithLevelId(levelID);
		if (!(worldWithLevelId == null))
		{
			if (m_LastPlayedLevelIDs.ContainsKey(worldWithLevelId.m_Id))
			{
				m_LastPlayedLevelIDs[worldWithLevelId.m_Id] = levelID;
			}
			else
			{
				m_LastPlayedLevelIDs.Add(worldWithLevelId.m_Id, levelID);
			}
		}
	}

	public string GetMostRecentSimChecksum(string levelID)
	{
		if (m_MostRecentSimChecksums.ContainsKey(levelID))
		{
			return m_MostRecentSimChecksums[levelID];
		}
		return string.Empty;
	}

	public void SetMostRecentSimChecksum(string levelID, string checksum)
	{
		if (m_MostRecentSimChecksums.ContainsKey(levelID))
		{
			m_MostRecentSimChecksums[levelID] = checksum;
		}
		else
		{
			m_MostRecentSimChecksums.Add(levelID, checksum);
		}
	}

	public bool LastPlayedLevelIDAlreadyStored(string levelID)
	{
		foreach (KeyValuePair<string, string> lastPlayedLevelID in m_LastPlayedLevelIDs)
		{
			if (lastPlayedLevelID.Value == levelID)
			{
				return true;
			}
		}
		return false;
	}
}
