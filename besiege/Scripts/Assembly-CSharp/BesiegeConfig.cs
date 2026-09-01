using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using UnityEngine;

public class BesiegeConfig
{
	public static bool IsAppleMChip;

	public bool ShadowsEnabled { get; set; }

	public bool HardShadows { get; set; }

	public ShadowResolution ShadowRes { get; set; }

	public bool ShadowsDoubled { get; set; }

	public int ShadowCascades { get; set; }

	public float ShadowRenderDistance { get; set; }

	public bool ScreenSpaceAmbientOcclusion { get; set; }

	public OptionsMaster.Tier SSAOQuality { get; set; }

	public float SSAOIntensity { get; set; }

	public AAMode AntiAliasingMode { get; set; }

	public bool Tooltips { get; set; }

	public bool Tutorials { get; set; }

	public bool HotkeyHUD { get; set; }

	public bool DepthOfField { get; set; }

	public bool Vignette { get; set; }

	public bool Bloom { get; set; }

	public float Saturation { get; set; }

	public float BloomIntensity { get; set; }

	public float FieldOfView { get; set; }

	public float UIScale { get; set; }

	public float UIIntensity { get; set; }

	public bool ShowConquered { get; set; }

	public bool WindowedMode { get; set; }

	public int Monitor { get; set; }

	public int ScreenWidth { get; set; }

	public int ScreenHeight { get; set; }

	public FPSLock FPSLock { get; set; }

	public int VSync { get; set; }

	public bool AutoTimeScale { get; set; }

	public float MinTimeScale { get; set; }

	public float MaxTimeScale { get; set; }

	public bool SmoothCamera { get; set; }

	public bool LimitCamera { get; set; }

	public bool UseBoundsCenter { get; set; }

	public float CameraSensitivity { get; set; }

	public bool Vibration { get; set; }

	public MouseOrbit.SimOrientation SimCamFollow { get; set; }

	public bool FirstTimePlaying { get; set; }

	public bool AutoSetLocalisation { get; set; }

	public string[] ShownLanguageWarningFor { get; set; }

	public bool BloodEnabled { get; set; }

	public bool SkinsEnabled { get; set; }

	public bool DeformMeshes { get; set; }

	public bool MorePrecisePhysics { get; set; }

	public bool AdvancedBuilding { get; set; }

	public bool MiddleClickVFX { get; set; }

	public bool UIBlur { get; set; }

	public bool ShowSurfaceNodeGrid { get; set; }

	public bool GuideBookShown { get; set; }

	public string PlayerName { get; set; }

	public string Language { get; set; }

	public string LastConnectedAddress { get; set; }

	public bool LevelEditorEnabled { get; set; }

	public bool ExcludeDefaultSaveData { get; set; }

	public string MasterserverIP { get; set; }

	public int MasterserverPort { get; set; }

	public string FacilitatorIP { get; set; }

	public int FacilitatorPort { get; set; }

	public string ConnectiontesterIP { get; set; }

	public int ConnectiontesterPort { get; set; }

	public float PortForwardingTimeout { get; set; }

	public float PunchThroughTimeout { get; set; }

	public float HostResolveTimeout { get; set; }

	public float ReconnectTimeout { get; set; }

	public int MaxReconnectAttempts { get; set; }

	public int MaximumTransmissionUnit { get; set; }

	public bool ShowTutorialWindows { get; set; }

	public Region Region { get; set; }

	public bool CloudSaving { get; set; }

	public bool UseLeaderboards { get; set; }

	public bool MusicEnabled { get; set; }

	public float MasterVolume { get; set; }

	public float MusicVolume { get; set; }

	public float UIVolume { get; set; }

	public float SfxVolume { get; set; }

	public float AmbientVolume { get; set; }

	public float PhysicsVolume { get; set; }

	public float BlockVolume { get; set; }

	public bool DuckVolumeUnfocused { get; set; }

	public bool SfxDistanceFX { get; set; }

	public int TextureQuality { get; set; }

	public int ReflectionQuality { get; set; }

	public bool Rippling { get; set; }

	public bool WaterCannonRippling { get; set; }

	public AnisotropicFiltering AnisoFilter { get; set; }

	public string RconPassword { get; set; }

	public bool ShowDebugLogs { get; set; }

	public bool ShowLogFrameNumber { get; set; }

	public string LastVersion { get; set; }

	public int MVBlocksPerFrame { get; set; }

	public int MVSurfacesPerFrame { get; set; }

	public bool AutosaveEnabled { get; set; }

	public int AutosaveDeleteAfterDays { get; set; }

	public int AutosaveMaxFiles { get; set; }

	public bool SavePreviousVersionsEnabled { get; set; }

	public int VersionMaxFiles { get; set; }

	public int VersionDeleteAfterDays { get; set; }

	[XmlIgnore]
	public Dictionary<string, DateTime> SkinsLastUsedTimes { get; set; }

	public string SkinsLastUsedTimesString { get; set; }

	public bool Crossplay { get; set; }

	public string[] AdditionalModsDirectories { get; set; }

	public BesiegeConfig()
	{
		SetDefaultValues();
	}

	public void Save(string configFile, BesiegeFileManager.FileLocation configLocation)
	{
		SkinsLastUsedTimesString = JsonUtility.ToJson(SkinsLastUsedTimes);
		using (MemoryStream memoryStream = new MemoryStream())
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(BesiegeConfig));
			xmlSerializer.Serialize(memoryStream, this);
			BesiegeFileManager.Save(configFile, configLocation, memoryStream.ToArray());
		}
	}

	public void Load(string configFile, BesiegeFileManager.FileLocation configLocation)
	{
		byte[] data;
		if (BesiegeFileManager.Load(configFile, configLocation, out data))
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(BesiegeConfig));
			using (MemoryStream stream = new MemoryStream(data))
			{
				BesiegeConfig besiegeConfig = xmlSerializer.Deserialize(stream) as BesiegeConfig;
				string versionString = VersionNumber.GetVersionString();
				float result;
				if (besiegeConfig.LastVersion != versionString && float.TryParse(besiegeConfig.LastVersion.Split('-')[0], out result))
				{
					Debug.Log("Loaded Config from version: " + result + ", game version is " + versionString.Split('-')[0]);
					if ((double)result < 1.77 && besiegeConfig.FPSLock > FPSLock.Lock60)
					{
						besiegeConfig.FPSLock = FPSLock.Lock144;
					}
				}
				besiegeConfig.CopyProperties(this);
			}
		}
		SkinsLastUsedTimes = ((!string.IsNullOrEmpty(SkinsLastUsedTimesString)) ? JsonUtility.FromJson<Dictionary<string, DateTime>>(SkinsLastUsedTimesString) : new Dictionary<string, DateTime>());
	}

	public void SetFirstTimerValues()
	{
		SimCamFollow = MouseOrbit.SimOrientation.Machine;
	}

	private void SetDefaultValues()
	{
		ShadowsEnabled = true;
		HardShadows = false;
		ShadowRes = ShadowResolution.VeryHigh;
		ShadowsDoubled = false;
		ShadowCascades = 0;
		ShadowRenderDistance = 350f;
		ScreenSpaceAmbientOcclusion = true;
		SSAOQuality = OptionsMaster.Tier.Medium;
		SSAOIntensity = 100f;
		AntiAliasingMode = AAMode.FXAA2;
		Tooltips = true;
		Tutorials = true;
		HotkeyHUD = true;
		FieldOfView = 72.8f;
		UIScale = 100f;
		UIIntensity = 100f;
		ShowConquered = true;
		Vignette = true;
		Bloom = true;
		BloomIntensity = 100f;
		Saturation = 100f;
		WindowedMode = false;
		FPSLock = FPSLock.Lock60;
		VSync = 0;
		AutoTimeScale = true;
		MinTimeScale = 10f;
		MaxTimeScale = 100f;
		SmoothCamera = false;
		LimitCamera = true;
		UseBoundsCenter = false;
		CameraSensitivity = 100f;
		SimCamFollow = MouseOrbit.SimOrientation.Manual;
		AutoSetLocalisation = false;
		FirstTimePlaying = true;
		ShownLanguageWarningFor = new string[0];
		BloodEnabled = true;
		SkinsEnabled = false;
		DeformMeshes = true;
		AdvancedBuilding = false;
		MiddleClickVFX = true;
		UIBlur = true;
		ShowSurfaceNodeGrid = true;
		GuideBookShown = true;
		PlayerName = "UNKNOWN PLAYER";
		Language = "English";
		LevelEditorEnabled = true;
		ExcludeDefaultSaveData = true;
		string text = (ConnectiontesterIP = "ms.spiderlinggames.co.uk");
		text = (FacilitatorIP = text);
		MasterserverIP = text;
		MasterserverPort = 23466;
		FacilitatorPort = 61111;
		ConnectiontesterPort = 10737;
		PortForwardingTimeout = 1.5f;
		PunchThroughTimeout = 10f;
		HostResolveTimeout = 2f;
		ReconnectTimeout = 1f;
		MaxReconnectAttempts = 5;
		Monitor = 0;
		ScreenWidth = 1920;
		ScreenHeight = 1080;
		MaximumTransmissionUnit = 1480;
		ShowTutorialWindows = true;
		Region = Region.EUCentral;
		MusicEnabled = true;
		SfxDistanceFX = true;
		MasterVolume = 75f;
		MusicVolume = 75f;
		UIVolume = 100f;
		SfxVolume = 100f;
		AmbientVolume = 100f;
		PhysicsVolume = 100f;
		BlockVolume = 100f;
		DuckVolumeUnfocused = false;
		TextureQuality = 3;
		ReflectionQuality = 0;
		Rippling = false;
		WaterCannonRippling = false;
		AnisoFilter = AnisotropicFiltering.Enable;
		ShowDebugLogs = false;
		ShowLogFrameNumber = false;
		MVBlocksPerFrame = 20;
		MVSurfacesPerFrame = 4;
		AutosaveEnabled = true;
		AutosaveDeleteAfterDays = 28;
		AutosaveMaxFiles = 100;
		SavePreviousVersionsEnabled = true;
		VersionDeleteAfterDays = 28;
		VersionMaxFiles = 100;
		CloudSaving = true;
		SkinsLastUsedTimes = new Dictionary<string, DateTime>();
		UseLeaderboards = true;
		Crossplay = false;
		AdditionalModsDirectories = new string[0];
	}

	public static bool ContainsAppleMChip(string input)
	{
		return IsAppleMChip = input.Contains("Apple") && Regex.IsMatch(input, "(?<!\\w)M\\d+(?!\\w)");
	}
}
