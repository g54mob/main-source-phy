using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public static class PlayerSave
{
	[Serializable]
	public class Progress
	{
		public List<LevelState> levelStates = new List<LevelState>();

		public CharacterCustomization characterCustomization = new CharacterCustomization();

		public List<TokenState> demoLevelFoundTokens = new List<TokenState>();

		public List<TokenState> lobbyJigsawTokens = new List<TokenState>();

		public bool welcomeMessageShown;

		public bool sawPopupForHint;

		public bool reBetaMessageShown;

		public List<int> viewedNews = new List<int>();

		public Menu.AwardType awardToShowInLobby;

		public List<Menu.AwardType> awards = new List<Menu.AwardType>();

		public List<int> starKeyFound = new List<int>();
	}

	[Serializable]
	public class Settings
	{
		public const float MAX_CONTROLLER_SENSITIVITY_MOVE = 10f;

		public DebugSettings debugSettings = new DebugSettings();

		public RoomEditorSettings re = new RoomEditorSettings();

		public bool musicEnabled = true;

		public float musicVolume = 100f;

		public float menuMusicVolume = 100f;

		public bool isMenuMusicVolumeSetup;

		public bool soundEnabled = true;

		public float soundVolume = 100f;

		public bool invertLookX;

		public bool invertLookY;

		public float mouseSensitivity = 16f;

		public bool mouseSmoothing;

		public bool vSyncEnabled = true;

		public FullScreenMode fullScreenMode = FullScreenMode.FullScreenWindow;

		public int screenResolutionWidth;

		public int screenResolutionHeight;

		public int screenRefreshRate;

		public bool limitFrameRate = true;

		public int targetFrameRate = 30;

		public bool reduceSettingsOnLowFrameRate = true;

		public TextureLevel playerTextureLevel;

		public List<StoredKeyBinding> keyBindings = new List<StoredKeyBinding>();

		public bool isStreamerMode;

		public bool isHidingItemHints;

		public bool invertMouseScroll;

		public int language;

		public bool isOculusDeviceLanguageSetup;

		public float gameFov = 60f;

		public float controllerSensitivityMove = 6f;

		public float controllerSensitivityZoom = 15f;

		public bool controllerInvertRightX;

		public bool controllerInvertRightY;

		public bool controllerInvertLeftX;

		public bool controllerInvertLeftY;

		public float mouseSmoothnessAmount = 5f;

		public bool disableAnyLevelUIOverlay;

		public bool voiceChatEnabled;

		public float voiceChatVolume = 100f;

		public float movementSpeed = 1f;

		public bool disableExamineModeEffects;

		public bool voiceChatAlwaysOn = true;

		public bool showVoiceChatIndicators = true;

		public TextureCompressionLevel customTextureCompression = TextureCompressionLevel.Fast;

		public float voiceActivityThreshold = 0.01f;

		public int activeMonitor;

		public string regionSummary = string.Empty;

		public bool chatProfanityFilterEnabled = true;

		public Game.HoverEffectType hoverEffectType;

		public bool showCrosshair = true;

		public float teleportBlinkDuration = 0.2f;

		public float inventoryRotationSpeed;

		public float comfortVignetteSize = 50f;

		public bool autoCloseInventory = true;

		public Game.InventoryOpenPosition inventoryOpenPosition;

		public Game.HandVisibilityWhenHoldingItem handVisibilityWhenHoldingItem = Game.HandVisibilityWhenHoldingItem.Transparent;

		public Game.CursorType cursorType = Game.CursorType.Circle;

		public Game.RayType rayType;

		public float rightHandVibrationIntensity = 1f;

		public float leftHandVibrationIntensity = 1f;

		public bool useSnapTurn = true;

		public float snapTurnDegreesPerTurn = 45f;

		public float smoothTurnDegreesPerSecond = 90f;

		public Game.MovementScheme leftHandMovementScheme;

		public Game.MovementScheme rightHandMovementScheme;

		public bool holdDownToTurnAround;

		public float turnAroundHoldTime = 0.3f;

		public bool disableZoomScaling;

		public bool thumbstickRotatesItemInHand;

		public bool showItemInfoOnHover = true;

		public bool useHeadBob;

		public float itemInfoRequiredHoverDuration = 1f;

		public Game.HandSmoothingType handSmoothingType = Game.HandSmoothingType.Hand;

		public Game.VRHeightType vrHeightType;

		public VRPictureCamera.ImageFormat pictureImageFormat;

		public float gripPressThreshold = 0.35f;

		public bool hideSimpleTutorials;

		public float thumbstickDeadzone = 0.2f;

		public bool useNewThrowAlgorithm = true;

		public bool didInitialSetup;

		public YearlyEventTheme yearlyEventTheme = new YearlyEventTheme();

		public Themes.ColorScheme theme;

		[NonSerialized]
		public Menu.NetworkingProtocol networkingProtocol;

		public HDAdditionalCameraData.AntialiasingMode hdrpAntialiasingMode = HDAdditionalCameraData.AntialiasingMode.TemporalAntialiasing;

		public float hdrpPostExposure;

		public bool hdrpMotionBlur = true;

		public bool hdrpScreenSpaceAmbientOcclusion = true;

		public bool hdrpScreenSpaceReflections = true;

		public bool hdrpVolumetricFog = true;

		public bool hdrpBloom = true;

		public bool hdrpShadowsEnabled = true;

		public bool hdrpDecals = true;

		public bool hdrpVolumetrics = true;

		public int hdrpGeometry;

		public Game.ResolutionScaling hdrpResolutionScaling;

		public float hdrpResolutionScalingFixed = 0.8f;

		public float uiScaling = 1f;

		public float hoverUIScale = 1f;

		public bool showTimer = true;
	}

	[Serializable]
	public class RoomEditorSettings
	{
		public enum TextEditorTheme
		{
			Dark = 0,
			Light = 1
		}

		public enum TextEditorType
		{
			Builtin = 0,
			External = 1,
			Custom = 2
		}

		public enum TextureEditorType
		{
			Paint = 0,
			External = 1,
			Custom = 2
		}

		public enum NavMeshVisibility
		{
			ShowNone = 0,
			ShowOnlyStanding = 1,
			ShowOnlyCrouching = 2,
			ShowBoth = 3
		}

		public float cameraSpeed = 1f;

		public NavMeshVisibility navMeshVisibility;

		public bool gridEnabled = true;

		public float gridSize = 0.5f;

		public PlaneSystem gridPlane = PlaneSystem.XZ;

		public Color gridColor = Color.gray5.With(null, null, null, 0.25f);

		public bool polygonSnapWhenPlacing = true;

		public bool polygonSnapWhenEditing = true;

		public Vector3 floorSize = new Vector3(3f, 3f, 0f);

		public Vector3 wallSize = new Vector3(2f, 3f, 0f);

		public TextEditorTheme textEditorTheme;

		public TextEditorType textEditorType;

		public string customTextEditorPath = string.Empty;

		public TextureEditorType textureEditorType;

		public string customTextureEditorPath = string.Empty;

		public List<PropID> favoriteProps = new List<PropID>();
	}

	[Serializable]
	public class YearlyEventTheme
	{
		public Themes.ColorScheme themeToSetDuringEvent;

		public Themes.ColorScheme themeToSetAfterEventEnds;

		public bool isActive;
	}

	[Serializable]
	public class LevelState
	{
		public string levelId;

		public FinishState finishState;

		public FinishState lastFinishStateCheckedInMenu;

		public int lastNumberOfTokensCheckedInMenu;

		public List<TokenState> foundTokens = new List<TokenState>();
	}

	[Serializable]
	public class TokenState
	{
		public int tokenId;

		public bool isTokenInitialized;

		public Vector3 tokenPosition;

		public Quaternion tokenRotation;
	}

	[Serializable]
	public class StoredKeyBinding
	{
		public KeyBindingAction action;

		public KeyCode keyCode;

		public KeyCode keyCodeSecondary;

		public List<KeyCode> modifiers = new List<KeyCode>();

		public List<KeyCode> modifiersSecondary = new List<KeyCode>();
	}

	[Serializable]
	public class DebugSettings
	{
		public bool useNavMesh = true;

		public bool showCrosshair = true;

		public bool showHint = true;

		public bool showNameLabel = true;

		public bool showItemOutline = true;

		public float freeCamMovementSpeed = 1f;

		public float freeCamLookSpeed = 50f;

		public bool freeCamUseEasing = true;

		public float freeCamEasingSpeed = 0.5f;

		public List<CharacterCustomization> characterPresets = new List<CharacterCustomization>();

		public float freeCamFOV = 70f;
	}

	public enum FinishState
	{
		None = 0,
		Started = 1,
		Finished = 2,
		Trophy = 3
	}

	public enum TextureLevel
	{
		Full = 0,
		Half = 1,
		Quarter = 2
	}

	public enum TextureCompressionLevel
	{
		Uncompressed = 0,
		Fast = 1,
		Pretty = 2
	}

	private static readonly string SAVE_FILE_NAME = "ES2-Save-v10.sav";

	private static readonly string SETTINGS_FILE_NAME = "ES2-Settings-v10.json";

	private static string SAVE_PATH = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);

	private static string SETTINGS_PATH = Path.Combine(Application.persistentDataPath, SETTINGS_FILE_NAME);

	private static Progress progressInstance;

	private static Settings settingsInstance;

	private static Settings lowSettings;

	private static Settings lowSettingsQuest;

	private static Settings steamdeckDefaultSettings;

	public static void init(Language preferredLanguage)
	{
		lowSettings = new Settings
		{
			playerTextureLevel = TextureLevel.Quarter,
			customTextureCompression = TextureCompressionLevel.Fast
		};
		lowSettingsQuest = new Settings
		{
			playerTextureLevel = TextureLevel.Full,
			customTextureCompression = TextureCompressionLevel.Fast
		};
		steamdeckDefaultSettings = new Settings
		{
			hdrpResolutionScaling = Game.ResolutionScaling.Automatic,
			limitFrameRate = true,
			targetFrameRate = 60,
			vSyncEnabled = false,
			hdrpScreenSpaceAmbientOcclusion = false,
			hdrpMotionBlur = false,
			hdrpAntialiasingMode = HDAdditionalCameraData.AntialiasingMode.TemporalAntialiasing,
			hdrpGeometry = 1,
			screenResolutionHeight = 800,
			screenResolutionWidth = 1280,
			screenRefreshRate = 60,
			hoverUIScale = 1.5f
		};
		if (progressInstance == null)
		{
			try
			{
				progressInstance = JsonUtility.FromJson<Progress>(File.ReadAllText(SAVE_PATH));
			}
			catch (Exception ex)
			{
				Debug.LogError(">>> Progress <<< Corrupted progressInstance data!: " + ex.Message);
				Debug.LogError(ex.StackTrace);
				progressInstance = null;
			}
		}
		if (settingsInstance == null)
		{
			try
			{
				if (File.Exists(SETTINGS_PATH))
				{
					settingsInstance = JsonUtility.FromJson<Settings>(File.ReadAllText(SETTINGS_PATH));
				}
			}
			catch (Exception ex2)
			{
				Debug.LogError(">>> Settings <<< Corrupted settingsInstance data!: " + ex2.Message);
				settingsInstance = null;
			}
		}
		bool flag = false;
		if (progressInstance == null)
		{
			progressInstance = new Progress();
			progressInstance.characterCustomization.randomize();
			flag = true;
		}
		if (settingsInstance == null)
		{
			settingsInstance = new Settings();
			if (Is.Steamdeck)
			{
				settingsInstance = steamdeckDefaultSettings;
			}
			Localization.LocalizedLanguage item = Localization.allLanguages.Find((Localization.LocalizedLanguage x) => x.systemLanguage == preferredLanguage);
			settingsInstance.language = Localization.allLanguages.IndexOf(item);
			flag = true;
		}
		if (flag)
		{
			flush();
		}
	}

	public static void flush()
	{
		File.WriteAllText(SAVE_PATH, JsonUtility.ToJson(progressInstance, prettyPrint: true));
		File.WriteAllText(SETTINGS_PATH, JsonUtility.ToJson(settingsInstance, prettyPrint: true));
		Debug.Log("Writing to save file");
	}

	public static LevelState ensureAndGetLevelState(string levelId)
	{
		LevelState levelState = getProgress().levelStates.Find((LevelState levelState2) => levelState2.levelId == levelId);
		if (levelState != null)
		{
			return levelState;
		}
		levelState = new LevelState
		{
			levelId = levelId
		};
		getProgress().levelStates.Add(levelState);
		return levelState;
	}

	public static void setTokenCollected(string levelId, int tokenId)
	{
		if (!hasTokenBeenCollected(levelId, tokenId))
		{
			ensureAndGetLevelState(levelId).foundTokens.Add(new TokenState
			{
				tokenId = tokenId,
				isTokenInitialized = false,
				tokenPosition = Vector3.zero,
				tokenRotation = Quaternion.identity
			});
		}
	}

	public static bool hasTokenBeenCollected(string levelId, int tokenId)
	{
		return ensureAndGetLevelState(levelId).foundTokens.Find((TokenState tokenState) => tokenState.tokenId == tokenId) != null;
	}

	public static int countCollectedTokens(string levelId)
	{
		return ensureAndGetLevelState(levelId).foundTokens.Count;
	}

	public static List<int> getCollectedTokens(string levelId)
	{
		return ensureAndGetLevelState(levelId).foundTokens.ConvertAll((TokenState x) => x.tokenId);
	}

	public static void setLevelFinishState(string levelId, FinishState finishState)
	{
		LevelState levelState = ensureAndGetLevelState(levelId);
		if (levelState.finishState < finishState)
		{
			levelState.finishState = finishState;
		}
	}

	public static FinishState getFinishState(string levelId)
	{
		return ensureAndGetLevelState(levelId).finishState;
	}

	public static bool isLevelFinished(string levelId)
	{
		FinishState finishState = ensureAndGetLevelState(levelId).finishState;
		return finishState == FinishState.Finished || finishState == FinishState.Trophy;
	}

	public static int getTokenCountForPack(string packId)
	{
		int num = 0;
		foreach (LevelState levelState in getProgress().levelStates)
		{
			if (levelState.levelId.StartsWith(packId))
			{
				num += levelState.foundTokens.Count;
			}
		}
		return num;
	}

	public static void setFramerate()
	{
		bool vSyncEnabled = getSettings().vSyncEnabled;
		bool limitFrameRate = getSettings().limitFrameRate;
		int targetFrameRate = getSettings().targetFrameRate;
		Application.targetFrameRate = (vSyncEnabled ? (-1) : (limitFrameRate ? targetFrameRate : (-1)));
		QualitySettings.vSyncCount = (vSyncEnabled ? 1 : 0);
	}

	public static void setAllVolumes(bool forceMute = false)
	{
		float volume = ((!getSettings().musicEnabled || forceMute) ? 0f : (getSettings().musicVolume / 100f));
		PineFmod.setVolume(PineFmod.getBus("bus:/Music"), volume);
		float volume2 = ((!getSettings().soundEnabled || forceMute) ? 0f : (getSettings().soundVolume / 100f));
		PineFmod.setVolume(PineFmod.getBus("bus:/Sound Effects"), volume2);
		float volume3 = ((!getSettings().voiceChatEnabled || forceMute) ? 0f : (getSettings().voiceChatVolume / 100f));
		PineFmod.setVolume(PineFmod.getBus("bus:/Voice"), volume3);
	}

	public static Progress getProgress()
	{
		return progressInstance;
	}

	public static Settings getSettings()
	{
		return settingsInstance;
	}

	public static Settings getLowSettings()
	{
		return lowSettings;
	}

	public static Settings getLowSettingsQuest()
	{
		return lowSettingsQuest;
	}

	public static CharacterCustomization getCC()
	{
		return progressInstance.characterCustomization;
	}

	public static DebugSettings getDebug()
	{
		return settingsInstance.debugSettings;
	}
}
