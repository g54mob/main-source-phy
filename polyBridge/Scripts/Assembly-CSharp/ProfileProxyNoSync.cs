public class ProfileProxyNoSync
{
	public int m_Version;

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

	public bool m_VehicleLights;

	public bool m_Vsync;

	public bool m_TerrainLights;

	public bool m_CustomResolution;

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

	public ProfileProxyNoSync(Profile profile, int version)
	{
		m_Version = version;
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
		m_CuratedReplays = profile.m_CuratedReplays;
		m_Replays = profile.m_Replays;
		m_ReplayQuality = profile.m_ReplayQuality;
		m_ReplayLengthSeconds = profile.m_ReplayLengthSeconds;
		m_ReplaysFolderOverride = profile.m_ReplaysFolderOverride;
		m_BlockGamepadInput = profile.m_BlockGamepadInput;
		m_GamepadAcceleration = profile.m_GamepadAcceleration;
		m_GamepadCursorSpeedNormalized = profile.m_GamepadCursorSpeedNormalized;
		m_GamepadRotateCameraSpeedNormalized = profile.m_GamepadRotateCameraSpeedNormalized;
		m_GamepadZoomSpeedNormalized = profile.m_GamepadZoomSpeedNormalized;
		m_GamepadButtonIconsChoice = profile.m_GamepadButtonIconsChoice;
		m_UIScaleMode = profile.m_UIScaleMode;
		m_UIScaleFactor = profile.m_UIScaleFactor;
	}
}
