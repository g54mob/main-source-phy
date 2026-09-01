using System.Collections.Generic;
using UnityEngine;

public class ProfileProxy
{
	public int m_Version;

	public bool m_GridEnabled;

	public bool m_StressViewEnabled;

	public bool m_AutoTriangulateEnabled;

	public bool m_AutoDrawEnabled;

	public bool m_SnapEnabled;

	public bool m_EdgeBisectEnabled;

	public bool m_FirstBreakEnabled;

	public bool m_SortSandboxLayoutByDate;

	public bool m_SortBridgeSavesByDate;

	public bool m_AutomatiallyLoadAutoSave;

	public bool m_PauseOnBreak;

	public bool m_LockBuildCamera;

	public bool m_DisableTooltips;

	public bool m_DisableBuildDataTooltips;

	public bool m_DisableBuildHelpTooltips;

	public string m_LastLoadedSandbox;

	public PointOfViewType m_PointOfViewType;

	public float m_EventEditorAnchorYNormalized;

	public List<float> m_CampaignLevelsDurationSeconds;

	public HashSet<PopUpWarningCategory> m_NeverShowAgain;

	public ArcShape m_ArcShape;

	public bool m_ArcSnapToGrid;

	public bool m_GalleryUnderBudget;

	public bool m_GalleryUnbreaking;

	public bool m_GalleryShowOnlyWins;

	public bool m_GalleryIncludeCheats;

	public GallerySortBy m_GallerySortBy;

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

	public HashSet<string> m_OpenedWeeklyChallengeItemIds;

	public HashSet<string> m_FiveStarUnlocks;

	public List<string> m_ActiveModDirectories;

	public List<string> m_ActiveLocalModDirectories;

	public Dictionary<string, string> m_LastPlayedLevelIDs;

	public Dictionary<string, string> m_MostRecentSimChecksums;

	public string m_AvatarAddressable;

	public string m_AvatarSkin;

	public bool m_DidCrashOnModLoad;

	public bool m_FollowCar;

	public bool m_LeaderboardsNoSubmit;

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

	public string m_TwitterToken;

	public string m_TwitterUsername;

	public string m_RedditRefreshToken;

	public string m_RedditUsername;

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

	public bool m_Bloom;

	public bool m_SSAO;

	public bool m_Vignette;

	public bool m_CustomResolution;

	public bool m_VehicleLights;

	public bool m_Vsync;

	public bool m_TerrainLights;

	public int m_VsyncInterval;

	public ShadowResolution m_ShadowResolution;

	public AntiAliasingQuality m_AntiAliasingQuality;

	public int m_ScreenWidth;

	public int m_ScreenHeight;

	public bool m_CuratedReplays;

	public bool m_Replays;

	public AsyncCaptureQuality m_ReplayQuality;

	public int m_ReplayLengthSeconds;

	public string m_ReplaysFolderOverride;

	public bool m_BlockGamepadInput;

	public bool m_GamepadAcceleration;

	public float m_GamepadCursorSpeedNormalized;

	public float m_GamepadRotateCameraSpeedNormalized;

	public float m_GamepadZoomSpeedNormalized;

	public GamepadButtonIconsChoice m_GamepadButtonIconsChoice;

	public UIScaleMode m_UIScaleMode;

	public float m_UIScaleFactor;

	public List<BindingProxy> m_Bindings = new List<BindingProxy>();

	public ProfileProxy(Profile profile, int version)
	{
		m_Version = version;
		m_GridEnabled = profile.m_GridEnabled;
		m_StressViewEnabled = profile.m_StressViewEnabled;
		m_AutoTriangulateEnabled = profile.m_AutoTriangulateEnabled;
		m_AutoDrawEnabled = profile.m_AutoDrawEnabled;
		m_SnapEnabled = profile.m_SnapEnabled;
		m_EdgeBisectEnabled = profile.m_EdgeBisectEnabled;
		m_FirstBreakEnabled = profile.m_FirstBreakEnabled;
		m_LastLoadedSandbox = profile.m_LastLoadedSandbox;
		m_SortSandboxLayoutByDate = profile.m_SortSandboxLayoutsByDate;
		m_SortBridgeSavesByDate = profile.m_SortBridgeSavesByDate;
		m_AutomatiallyLoadAutoSave = profile.m_AutomatiallyLoadAutoSave;
		m_PauseOnBreak = profile.m_PauseOnBreak;
		m_CuratedReplays = profile.m_CuratedReplays;
		m_Replays = profile.m_Replays;
		m_ReplayQuality = profile.m_ReplayQuality;
		m_ReplayLengthSeconds = profile.m_ReplayLengthSeconds;
		m_ReplaysFolderOverride = profile.m_ReplaysFolderOverride;
		m_LockBuildCamera = profile.m_LockBuildCamera;
		m_DisableTooltips = profile.m_DisableTooltips;
		m_DisableBuildDataTooltips = profile.m_DisableBuildDataTooltips;
		m_DisableBuildHelpTooltips = profile.m_DisableBuildHelpTooltips;
		m_PointOfViewType = profile.m_PointOfViewType;
		m_EventEditorAnchorYNormalized = profile.m_EventEditorAnchorYNormalized;
		m_NeverShowAgain = new HashSet<PopUpWarningCategory>(profile.m_NeverShowAgain);
		m_ArcShape = profile.m_ArcShape;
		m_ArcSnapToGrid = profile.m_ArcSnapToGrid;
		m_GalleryUnderBudget = profile.m_GalleryUnderBudget;
		m_GalleryUnbreaking = profile.m_GalleryUnbreaking;
		m_GalleryShowOnlyWins = profile.m_GalleryShowOnlyWins;
		m_GalleryIncludeCheats = profile.m_GalleryIncludeCheats;
		m_GallerySortBy = profile.m_GallerySortBy;
		m_LastSolvedCampaignLevelId = profile.m_LastSolvedCampaignLevelId;
		m_LastLoadedCampaignLevelId = profile.m_LastLoadedCampaignLevelId;
		m_LastMainMenuThemeId = profile.m_LastMainMenuThemeId;
		m_SeenMainMenu = profile.m_SeenMainMenu;
		m_DismissedAutoTriangulateHelpArrow = profile.m_DismissedAutoTriangulateHelpArrow;
		m_DismissedFoundationHelpArrow = profile.m_DismissedFoundationHelpArrow;
		m_WorkshopItemsSortBy = profile.m_WorkshopItemsSortBy;
		m_WorkshopModItemsSortBy = profile.m_WorkshopModItemsSortBy;
		m_HasClickedChallenges = profile.m_HasClickedChallenges;
		m_TwitchUsername = profile.m_TwitchUsername;
		m_TwitchSuscribersOnly = profile.m_TwitchSuscribersOnly;
		m_TwitchModerated = profile.m_TwitchModerated;
		m_TwitchAutoPlay = profile.m_TwitchAutoPlay;
		m_TwitchAutoAdvance = profile.m_TwitchAutoAdvance;
		m_TwitchViewerCooldownSeconds = profile.m_TwitchViewerCooldownSeconds;
		m_TwitchAllowSuggestions = profile.m_TwitchAllowSuggestions;
		m_TwitchBitsEnabled = profile.m_TwitchBitsEnabled;
		m_TwitchBitsMandatory = profile.m_TwitchBitsMandatory;
		m_TwitchStreamerWindowPos = profile.m_TwitchStreamerWindowPos;
		m_TwitchStreamerWindowCollapsed = profile.m_TwitchStreamerWindowCollapsed;
		m_TwitchStreamerWindowHeight = profile.m_TwitchStreamerWindowHeight;
		m_TwitchAuthorPanelPos = profile.m_TwitchAuthorPanelPos;
		m_TwitterToken = profile.m_TwitterToken;
		m_TwitterUsername = profile.m_TwitterUsername;
		m_RedditRefreshToken = profile.m_RedditRefreshToken;
		m_RedditUsername = profile.m_RedditUsername;
		m_LanguageCode = profile.m_LanguageCode;
		m_ColorBlindModeOn = profile.m_ColorBlindModeOn;
		m_MouseWheelSpeedNormalized = profile.m_MouseWheelSpeedNormalized;
		m_CameraRotateSpeedNormalized = profile.m_CameraRotateSpeedNormalized;
		m_CameraPanSpeedNormalized = profile.m_CameraPanSpeedNormalized;
		m_BlockGamepadInput = profile.m_BlockGamepadInput;
		m_GamepadAcceleration = profile.m_GamepadAcceleration;
		m_GamepadCursorSpeedNormalized = profile.m_GamepadCursorSpeedNormalized;
		m_GamepadRotateCameraSpeedNormalized = profile.m_GamepadRotateCameraSpeedNormalized;
		m_GamepadZoomSpeedNormalized = profile.m_GamepadZoomSpeedNormalized;
		m_GamepadButtonIconsChoice = profile.m_GamepadButtonIconsChoice;
		m_MasterVolume = profile.m_MasterVolume;
		m_AmbientVolume = profile.m_AmbientVolume;
		m_SFXVolume = profile.m_SFXVolume;
		m_MusicVolume = profile.m_MusicVolume;
		m_UIVolume = profile.m_UIVolume;
		m_Mute = profile.m_Mute;
		m_MuteInBackground = profile.m_MuteInBackground;
		m_OverBudgetAlert = profile.m_OverBudgetAlert;
		m_FullScreen = profile.m_FullScreen;
		m_VsyncInterval = profile.m_VsyncInterval;
		m_Bloom = profile.m_Bloom;
		m_SSAO = profile.m_SSAO;
		m_Vignette = profile.m_Vignette;
		m_CustomResolution = profile.m_CustomResolution;
		m_VehicleLights = profile.m_VehicleLights;
		m_TerrainLights = profile.m_TerrainLights;
		m_Vsync = profile.m_Vsync;
		m_ShadowResolution = profile.m_ShadowResolution;
		m_AntiAliasingQuality = profile.m_AntiAliasingQuality;
		m_ScreenWidth = profile.m_ScreenWidth;
		m_ScreenHeight = profile.m_ScreenHeight;
		StoreBindings();
		m_LeaderboardsViewExtended = profile.m_LeaderboardsViewExtended;
		m_LeaderboardsFilterExtended = profile.m_LeaderboardsFilterExtended;
		m_LeaderboardsView = profile.m_LeaderboardsView;
		m_LeaderboardsFilter = profile.m_LeaderboardsFilter;
		m_HideOtherPlayerSolutions = profile.m_HideOtherPlayerSolutions;
		m_HideOtherLeaderboards = profile.m_HideOtherLeaderboards;
		m_AutoPlayOnly = profile.m_AutoPlayOnly;
		m_ExcludeAutoPlay = profile.m_ExcludeAutoPlay;
		m_OpenedWeeklyChallengeItemIds = new HashSet<string>(profile.m_OpenedWeeklyChallengeItemIds);
		m_FiveStarUnlocks = new HashSet<string>(profile.m_FiveStarUnlocks);
		m_ActiveModDirectories = new List<string>(profile.m_ActiveModDirectories);
		m_ActiveLocalModDirectories = new List<string>(profile.m_ActiveLocalModDirectories);
		m_LastPlayedLevelIDs = new Dictionary<string, string>(profile.m_LastPlayedLevelIDs);
		m_MostRecentSimChecksums = new Dictionary<string, string>(profile.m_MostRecentSimChecksums);
		m_UIScaleMode = profile.m_UIScaleMode;
		m_UIScaleFactor = profile.m_UIScaleFactor;
		m_AvatarAddressable = profile.m_AvatarAddressable;
		m_AvatarSkin = profile.m_AvatarSkin;
		m_DidCrashOnModLoad = profile.m_DidCrashOnModLoad;
		m_FollowCar = profile.m_FollowCar;
		m_LeaderboardsNoSubmit = profile.m_LeaderboardsNoSubmit;
	}

	private void StoreBindings()
	{
		m_Bindings.Clear();
		foreach (KeyValuePair<BindingType, Binding> binding in Bindings.m_Bindings)
		{
			Binding value = binding.Value;
			m_Bindings.Add(new BindingProxy(value.m_BindingType, value.m_KeyCode, value.m_AltKeyCode));
		}
	}
}
