using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using GLTFast;
using GLTFast.Schema;
using InGameTextEditor;
using InGameTextEditor.Format;
using RuntimeSceneGizmo;
using SimpleFileBrowser;
using Steamworks;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class RoomEditor : MonoBehaviour
{
	private enum RmbHeldState
	{
		None = 0,
		ClickTesting = 1,
		HeldFrame = 2,
		Held = 3
	}

	private class LineConnection
	{
		public LineRenderer lineRenderer;

		public GameObject first;

		public GameObject second;

		public LineConnection(LineRenderer lineRenderer, GameObject first, GameObject second)
		{
			this.lineRenderer = lineRenderer;
			this.first = first;
			this.second = second;
		}
	}

	public enum UserAssetDropdownType
	{
		All = 0,
		Textures = 1,
		Materials = 2,
		Audio = 3,
		Scripts = 4,
		Models = 5
	}

	public enum TestPlaceMode
	{
		PlaceNone = 0,
		PlaceLogics = 1,
		PlaceAll = 2
	}

	private abstract class DragAndDropItem
	{
	}

	private class PropDragAndDropItem : DragAndDropItem
	{
		public PropID propID;

		public PropDragAndDropItem(PropID propID)
		{
			this.propID = propID;
		}
	}

	private class AudioAssetDragAndDropItem : DragAndDropItem
	{
		public AudioAsset audioAsset;

		public AudioAssetDragAndDropItem(AudioAsset audioAsset)
		{
			this.audioAsset = audioAsset;
		}
	}

	private class ScriptAssetDragAndDropItem : DragAndDropItem
	{
		public ScriptAsset scriptAsset;

		public ScriptAssetDragAndDropItem(ScriptAsset scriptAsset)
		{
			this.scriptAsset = scriptAsset;
		}
	}

	private class MaterialAssetDragAndDropItem : DragAndDropItem
	{
		public class MaterialSwap
		{
			public Renderer renderer;

			public int materialIndex;

			public UnityEngine.Material originalMaterial;
		}

		public MaterialAsset materialAsset;

		public MaterialSwap materialSwap;

		public MaterialAssetDragAndDropItem(MaterialAsset materialAsset)
		{
			this.materialAsset = materialAsset;
		}

		public void revertMaterialChange()
		{
			if (materialSwap != null)
			{
				UnityEngine.Material[] sharedMaterials = materialSwap.renderer.sharedMaterials;
				sharedMaterials[materialSwap.materialIndex] = materialSwap.originalMaterial;
				materialSwap.renderer.sharedMaterials = sharedMaterials;
				materialSwap = null;
			}
		}
	}

	private struct LogicProp
	{
		public enum Type
		{
			Gameplay = 0,
			Movement = 1,
			VisualAndAudio = 2
		}

		public string tooltip;

		public Type type;

		public LogicProp(string tooltip, Type type)
		{
			this.tooltip = tooltip;
			this.type = type;
		}
	}

	private class MaterialLocation
	{
		public UnityEngine.Material material;

		public Transform root;

		public string path;

		public int index;
	}

	private enum LuaParseResult
	{
		ValidName = 0,
		EmptyName = 1,
		InvalidFirstSymbol = 2,
		InvalidIsKeyword = 3,
		InvalidIsKeywordArray = 4,
		InvalidSymbolAfterArray = 5,
		InvalidSymbolInArray = 6,
		InvalidArrayNotClosed = 7,
		InvalidArrayHasNoDigits = 8,
		InvalidMultipleArrays = 9,
		InvalidSymbolInName = 10
	}

	private struct TargetArrayEntry
	{
		public UnityEngine.Texture texture;

		public int index;

		public string name;
	}

	private enum ChangePropResult
	{
		Success = 0,
		FailedToInstantiate = 1
	}

	private delegate bool PasswordPredicate(Lock _lock, int index);

	private enum PrepChangeDataStrategy
	{
		UseSelected = 0,
		UseTarget = 1
	}

	private class Tab
	{
		public readonly EditorTab type;

		public Button button;

		public UnityEngine.UI.Image icon;

		public Text text;

		public Color defaultIconColor;

		public Color defaultTextColor;

		public PropTag selectedTag;

		public string searchFilter;

		public PropButtonSize propButtonSize;

		public bool isSpecialTagsSectionShown = true;

		public bool isThemeTagsSectionShown = true;

		public bool isCategoryTagsSectionShown = true;

		public Tab(EditorTab type)
		{
			this.type = type;
		}
	}

	private enum SearchMatchResult
	{
		ExactMatch = 0,
		StartsWithMatch = 1,
		ContainsMatch = 2,
		NoMatch = 3
	}

	private class UserAsset
	{
		private static int idCounter;

		public readonly int id;

		public DateTime lastUpdated;

		public string path;

		public string name;

		public string nameWithExtension;

		protected UserAsset()
		{
			id = idCounter++;
		}
	}

	private class AudioAsset : UserAsset
	{
	}

	private class TextureAsset : UserAsset
	{
		public Texture2D texture;

		public new string name;
	}

	private class ScriptAsset : UserAsset
	{
	}

	private class ModelAsset : UserAsset
	{
	}

	private class MaterialAsset : UserAsset
	{
	}

	public static bool isUsingRoomEditor;

	private const string CUSTOM_MODEL_PREFIX = "custom_";

	private const string DEFAULT_MISSING_MODEL = "cube.prefab";

	public static List<FMOD.Sound> soundsCache = new List<FMOD.Sound>();

	public LevelContainerEditor levelContainer;

	public GameStarter gameStarter;

	public UnityEngine.Material outlineMaterial;

	public UnityEngine.Material outlineSelectedMaterial;

	public UnityEngine.Material outlineCombineMaterial;

	public UnityEngine.Material overlayMaterial;

	public TransformGizmoNeo transformGizmo;

	public LineRenderer redLine;

	public LineRenderer blueLine;

	public EditorAssetsMeta assets;

	public UnityEngine.Material ghostMaterial;

	public Shader uvShader;

	public MenuOptions menuOptions;

	public GameObject poof;

	public Font normalFont;

	public Font boldFont;

	public Texture2D defaultBrush;

	public Texture2D differentParentIcon;

	public EventSystem eventSystem;

	public CustomPassVolume customPassAfterPostprocess;

	public InitialWelcomeMessageUI betaMessageUI;

	[Header("UI")]
	public EditorTransformUI transformUI;

	public OptionDialog optionDialog;

	public EditorMenuUI menuUI;

	public EditorMetaUI metaUI;

	public EditorConfigUI configUI;

	public EditorTargetModeUI targetModeUI;

	public EditorPropertiesUI propertiesUI;

	public EditorTransformUI editorTransformUI;

	public EditorAssetBrowserUI assetBrowserUI;

	public EditorSoundPickerUI soundPickerUI;

	public EditorImportUI importUI;

	public RoomToolUI roomToolUI;

	public RightClickUI rightClickUI;

	public Canvas tooltipUI;

	public Canvas rebindingUI;

	public Canvas menuOptionsUI;

	public EditorFadeUI fadeUI;

	public RectSelectionUI rectSelectionUI;

	public EditorDebugUI debugUI;

	public Canvas sceneGizmoUI;

	public SearchUI searchUI;

	public ColorPickerUI colorPickerUI;

	[Header("Cursors")]
	public Texture2D defaultCursor;

	public Texture2D handCursor;

	private GameObject propPreview;

	private Bounds propPreviewBounds;

	private OriginalMaterials[] propPreviewOriginalMaterials;

	private int instanceIDCounter = 1;

	private readonly Dictionary<InstanceID, PropInstance> propInstances = new Dictionary<InstanceID, PropInstance>();

	private readonly List<CustomModelObject> customModelPrefabs = new List<CustomModelObject>();

	private GameObject propToPlacePrefab;

	private GameObject propToPlace;

	private Vector3 propToPlacePosition;

	private Vector3 propToPlaceNormal;

	private Quaternion propToPlaceRotation;

	private Quaternion propToPlaceOriginalRotation;

	private List<GameObject> instantiatedRuntime = new List<GameObject>();

	private readonly Dictionary<PropID, Texture2D> propIcons = new Dictionary<PropID, Texture2D>();

	private readonly Dictionary<PropID, Sprite> propSprites = new Dictionary<PropID, Sprite>();

	private Texture2D missingModelImage;

	private float lastCheckedModelImports = -1f;

	private readonly EditorOutlineContext outlineContext = new EditorOutlineContext();

	private readonly InputFieldNavigationData inputFieldsNavigationDataColorPicker = new InputFieldNavigationData();

	private readonly InstanceRaycast raycast = new InstanceRaycast
	{
		hitCounts = new int[2],
		hits = new RaycastHit[2][]
		{
			new RaycastHit[100],
			new RaycastHit[100]
		},
		instances = new PropInstance[128],
		lastInstanceHit = -1,
		mouse = Vector2.zero
	};

	private readonly RaycastComparer raycastComparer = new RaycastComparer();

	private readonly RaycastHit[] sharedHits = new RaycastHit[100];

	private readonly Dictionary<InstanceID, TransformData> cachedTransforms = new Dictionary<InstanceID, TransformData>();

	private List<RoomMeta> loadableRooms;

	private Vector3 cameraScreenshotPosition;

	private Quaternion cameraScreenshotRotation;

	private bool useNonLegacyLights;

	private bool useNonLegacyFloorColliders;

	private bool hidePlayerNameplates;

	private bool hideItemNameplates;

	private bool useProximityChat;

	private bool useSkyboxPreview = true;

	private bool usePostProcessingPreview = true;

	private bool useWaterPreview = true;

	private bool camToLinkedProp;

	private readonly PineTweenSystemEnableNoHandles tweener = new PineTweenSystemEnableNoHandles();

	private readonly List<PineTween> targetTransformTweens = new List<PineTween>();

	private RmbHeldState rmbHeldState;

	private float rmbDownTime;

	private float rmbDownDistance;

	private Vector2 rmbLastPos;

	private bool shouldEatNextLMB;

	private bool shouldEatNextRMB;

	private bool wasOnApplicationFocus;

	private readonly List<LoadTextureOp> loadTexturesOps = new List<LoadTextureOp>();

	private readonly List<StaggeredTextureLoadInfo> staggeredTextureLoad = new List<StaggeredTextureLoadInfo>();

	private Dictionary<string, long> soundPointersCache = new Dictionary<string, long>();

	private SpecialMode special;

	private readonly Dictionary<InstanceID, InstanceUICache> uiCache = new Dictionary<InstanceID, InstanceUICache>();

	private bool isRebinding;

	private readonly List<AssetRequest> assetsRequests = new List<AssetRequest>();

	private List<AssetBundle> assetsBundles = new List<AssetBundle>();

	private AssetBundle assetsIconsBundle;

	private InputDispatcher reInputDispatcher;

	public VirtualCursorUI virtualCursorUI;

	public Vector2 virtualCursorPosition;

	private PropInstance prevHoverInstance;

	private PropInstance hoverInstance;

	private bool skipNextAxisInput;

	private readonly SoundPicker soundPicker = new SoundPicker();

	private string loadingCanvasText = "%RoomEditor_LoadingCustomModels%";

	private string _roomDirPath;

	private EditorVolumeRefs editorVolumeRefs;

	private EditorPostProcessingData editorPostProcessingDefault;

	private EditorPostProcessingData editorCurrentActivePostProcessing;

	private EditorPostProcessingData editorPostProcessingOnStartActive;

	private SkyboxData skyboxDefaultCache;

	private SkyboxData skyboxCurrentActiveCache;

	private SkyboxData skyboxOnStartActiveCache;

	private CloudsData cloudsDefaultCache;

	private CloudsData cloudsCurrentActiveCache;

	private CloudsData cloudsOnStartActiveCache;

	private FogData fogDefaultCache;

	private FogData fogCurrentActiveCache;

	private FogData fogOnStartActiveCache;

	private OceanData oceanDefaultCache;

	private OceanData oceanCurrentActiveCache;

	private OceanData oceanOnStartActiveCache;

	private List<EditorDisplay> allDisplays;

	private RaycastHit[] playModeCacheHits = new RaycastHit[100];

	private List<LineConnection> lineConnections = new List<LineConnection>();

	private List<List<PropInstance>> propInstanceChildrenCache = new List<List<PropInstance>>
	{
		new List<PropInstance>(32),
		new List<PropInstance>(32),
		new List<PropInstance>(32),
		new List<PropInstance>(32)
	};

	private int instanceCacheAt;

	public UISkin assetBrowserSkin;

	public List<TextEditorTheme> textEditorThemes = new List<TextEditorTheme>();

	private float popupAssetBrowserOpenTime;

	private string popupAssetBrowserSelectedAssetPath = string.Empty;

	public Sprite[] userAssetDropdownTypeIcons;

	private string currentlyImportingGltfPath;

	private const float CAMERA_MIN_SPEED = 0.01f;

	private const float CAMERA_MAX_SPEED = 4f;

	private const float CAMERA_SPEED_INCREMENT_NORMAL = 0.05f;

	private const float CAMERA_SPEED_INCREMENT_SLOW = 0.01f;

	private const float CAMERA_MIN_ORTHO_SIZE = 0.1f;

	private const float CAMERA_MAX_ORTHO_SIZE = 100f;

	private static int CAMERA_HIDE_LAYER;

	private static int CAMERA_RAYCAST_MASK;

	public UnityEngine.Camera mainCam;

	private Vector3 cameraPosition;

	private Vector3 cameraMoveVelocity;

	private float cameraHoldSpeedMultiplier = 1f;

	private Quaternion cameraRotation;

	private CamMouseControl cameraMouseControl;

	private Vector3 cameraMovementAxis;

	private PineTween cameraTransition;

	private PineTween cameraProjectionTransition;

	public TextEditorUI textEditorUI;

	private string textEditorPath;

	private bool wasTextEditorLayoutRebuilt;

	[Header("== TESTING ==")]
	public TestPlaceMode testPlaceMode;

	public float testPlaceAllSpacing = 5f;

	public bool testPlaceOneByOne;

	public bool testLoadRoomOneByOne;

	public PropTag testPlaceTag;

	public int testStartFloorIndex = -1;

	public int testStartWallIndex = -1;

	public string roomToLoadOnStart = "";

	private PlaceAllTest placeAllTest;

	private LoadAllTest loadAllTest;

	private DragAndDropItem dragAndDropItem;

	public SceneGizmoRenderer gizmoAxisRenderer;

	public CustomPassVolume gizmoAxisCustomPassVolume;

	private GizmoAxisCustomPass gizmoAxisCustomPass;

	public CustomPassVolume gridCustomPassVolume;

	private GridCustomPass gridCustomPass;

	private readonly HashSet<InstanceID> hiddenPropInstanceIDs = new HashSet<InstanceID>();

	private readonly Dictionary<GameObject, int> hiddenPropLayerMemory = new Dictionary<GameObject, int>();

	private string hierarchySearchFilterLastFrame = "";

	private readonly List<PropID> propHistory = new List<PropID>();

	private const string ACTIVATOR_TOOLTIP = "Used to activate, deactivate or toggle properties of an object.";

	private const string CLOUDS_TOOLTIP = "Defines the clouds for this room.\nThere can be multiple clouds in a room, but only one can be active at a time.\nClouds are a visual effect that can be used to create atmosphere in the room.";

	private const string COLLIDER_TOOLTIP = "Defines an invisible collision volume.\nItems throw into it will collide with it as if it were a wall.";

	private const string DELAY_TOOLTIP = "Delays any action by fixed amount of time.";

	private const string DISPLAY_TOOLTIP = "Displays current values of a Lock (each value maps to an image in a sprite sheet).";

	private const string EMPTY_TOOLTIP = "A prop with appearance.";

	private const string FINISH_TOOLTIP = "Prop that, when targeted, will make it so that players have beaten the room.";

	private const string FOG_TOOLTIP = "Defines the fog settings for this room.\nFog is a visual effect that can be used to create atmosphere in the room.";

	private const string ITEM_RESPAWNER_TOOLTIP = "Component used to respawn important items so they don't get stuck in unreachable places.";

	private const string LADDER_TOOLTIP = "Logic prop used to climb up and down.";

	private const string LIGHT_TOOLTIP = "Logic prop used to emit light in the scene.";

	private const string LOCK_TOOLTIP = "Logic prop used to calculate a true or false value based on the current input and settings.";

	private const string OBSTACLE_TOOLTIP = "Prop that blocks movement of a player.\nPlayer cannot walk through the obstacle.";

	private const string OCEAN_TOOLTIP = "Logic prop used to create Ocean in the scene.\nIts used for one infinite water instance.";

	private const string OPEN_LINK_TOOLTIP = "Logic prop that allows you to open some link when targeted.";

	private const string POST_PROCESSING_TOOLTIP = "Defines the post processing effect for this room.\nThere can be multiple post processing effects in a room, but only one can be active at a time.";

	private const string PUZZLE_TOOLTIP = "A prop that defines a puzzle in the room.\nUsed to generate Walkthroughes for your players.";

	private const string ROULETTE_TOOLTIP = "A roulette is a prop that randomly selects a target from the list of targets.\nThe roulette can be used to create random events in the room.\nRoulette is synced in co-op.";

	private const string SCRIPT_TOOLTIP = "Logic prop used to channel Lua code to interact with other props.\nFind the documentation and get help from the community on our official Pine Studio Discord!";

	private const string SKYBOX_TOOLTIP = "Defines the skybox for this room.\nThere can be multiple skyboxes in a room, but only one can be active at a time.\nSkybox is a visual effect that can be used to create atmosphere in the room.";

	private const string SLOT_TOOLTIP = "Logic prop used to create areas that accept certain keys as inputs for unlocking.";

	private const string SOUND_TOOLTIP = "Defines a sound prop. Can be music or one shot sound.";

	private const string SETUP_TOOLTIP = "Allows you to trigger any action at the start of the room.";

	private const string STAIRS_TOOLTIP = "Defines a stairs prop. Player can walk up and down the stairs.";

	private const string SPAWN_POINT_TOOLTIP = "Defines a point where a player will spawn when starting this room.";

	private const string TELEPORT_TOOLTIP = "Defines where and who teleports to this position and/or rotation when activated.";

	private const string TEST_TOOLTIP = "Used only for testing properties (will not be in released build).";

	private const string TEXT_TOOLTIP = "Simple display of text in the world.";

	private const string TRIGGER_TOOLTIP = "Logic prop used to determine when another prop or player enters/exits an area.";

	private const string WATER_TOOLTIP = "Logic prop used to create water in the scene.\nIts used for smaller water instances.";

	private const string LOCAL_ONLY_TOOLTIP = "If true, only local player can trigger this object.\nUseful for effect that should not be seen by other players.\nFor example, when you enter a room, you might want to see different <NAME> from other players.";

	private const string MATERIAL_ASSET_FILE_EXTENSION = ".es2mat";

	private const string MATERIAL_ASSET_LEGACY_EXTENSION = ".mat";

	private const string DEFAULT_NEW_MATERIAL_NAME = "Material";

	private const string DEFAULT_MATERIAL_PROP_ID = "Primitives/Cube";

	private const string BASE_MAP_KEY = "_BaseColorMap";

	private const string BASE_COLOR_KEY = "_BaseColor";

	private const string NORMAL_MAP_KEY = "_NormalMap";

	private const string NORMAL_SCALE_KEY = "_NormalScale";

	private const string MASK_MAP_KEY = "_MaskMap";

	private const string METALLIC_REMAP_MIN_KEY = "_MetallicRemapMin";

	private const string METALLIC_REMAP_MAX_KEY = "_MetallicRemapMax";

	private const string METALLIC_KEY = "_Metallic";

	private const string SMOOTHNESS_REMAP_MIN_KEY = "_SmoothnessRemapMin";

	private const string SMOOTHNESS_REMAP_MAX_KEY = "_SmoothnessRemapMax";

	private const string SMOOTHNESS_KEY = "_Smoothness";

	private const string EMISSIVE_MAP_KEY = "_EmissiveColorMap";

	private const string EMISSIVE_COLOR_KEY = "_EmissiveColor";

	public MaterialEditorUI materialEditorUI;

	public UnityEngine.Texture materialEditorEmptyTextureIcon;

	public UnityEngine.Texture materialMissingTexture;

	private string materialEditorAssetPath;

	private MaterialData materialEditorData;

	private bool materialEditorBaseMapShown = true;

	private bool materialEditorNormalMapShown = true;

	private bool materialEditorMaskMapShown = true;

	private bool materialEditorEmissiveMapShown = true;

	private bool materialEditorTransparencyShown = true;

	private PropInstance materialEditorNewMaterialInstance;

	private MaterialLocation materialEditorNewMaterialLocation;

	private static readonly Dictionary<string, UnityEngine.Material> materialAssetPathToMaterial = new Dictionary<string, UnityEngine.Material>();

	private static readonly Dictionary<UnityEngine.Material, string> materialToMaterialAssetPath = new Dictionary<UnityEngine.Material, string>();

	private string customMaterialsRoomDirPath;

	private InputFieldNavigationData inputFieldsNavigationDataMaterials = new InputFieldNavigationData();

	public MeshFilter navMeshPreview;

	public MeshFilter navMeshCrouchPreview;

	[Header("Polygon Tool")]
	public PolygonTool polygonTool;

	public PolygonToolUI polygonToolUI;

	private Polygon selectedPolygonLastFrame;

	private readonly List<PropertyData> properties = new List<PropertyData>();

	private PropertyData draggedProperty;

	private PropertyData colorPickerProperty;

	private InputFieldNavigationData inputFieldsNavigationDataProperties = new InputFieldNavigationData();

	private HashSet<Interactive> scrubbingInteractives = new HashSet<Interactive>();

	private EditorContext contextCache;

	private static int nextTokenId = 0;

	public RectTransform hoverTooltipTemplate;

	private Transform propertiesParent;

	private UITooltip.Position propertiesTooltipPosition;

	private static TimeBudgetPerFrameDeferAgent _timeBudgetDeferAgent;

	public EditorUI editorUI;

	private readonly List<Tab> tabs = new List<Tab>();

	private Tab currentTab;

	private List<PropButtonGroupUI> cachedPropGroups = new List<PropButtonGroupUI>();

	public Sprite plusIcon;

	private PropInstance currentlySavingPremade;

	public Color tabsHeaderColor = Color.gray.With(null, null, null, 0.05f);

	private bool isFloorsAndWallsSectionShown = true;

	private bool isVisibilitySectionShown = true;

	private bool isGridSectionShown = true;

	private bool isCameraSectionShown = true;

	private InputFieldNavigationData inputFieldNavigationDataBuilding = new InputFieldNavigationData();

	public const string PREVIEW_IMAGE_NAME = "Preview.jpg";

	public PublishInfo publishInfo;

	private Menu.WorkshopRoomInfo publishingRoomInfo;

	private readonly RefreshFlags refresh = new RefreshFlags();

	private static string loadRoomOnStart;

	private LoadingCanvas.EndCondition endConditionCustomModels;

	private readonly List<CustomModelRequest> loadingCustomModels = new List<CustomModelRequest>();

	public RoomPickerUI roomPickerUI;

	private bool canCloseRoomPicker;

	private LevelPicker levelPicker;

	private LevelProvider levelProvider;

	private const string TYPE_QUERY_PREFIX = "t:";

	private const float SEARCH_DEBOUNCE_TIME = 0.5f;

	private bool isSearchMultipleSelecting;

	private float lastSearchQueryTime;

	private bool isSearching;

	private string lastSearchQuery;

	private string currentSearchQuery;

	private PropInstance firstSearchResult;

	private bool isHoveringSearchUI;

	private EditorSelection selection;

	private RectSelection rectSelection;

	private bool rectSelectionCanceled;

	private Dictionary<InstanceID, (Vector2, Vector3)> selectionPosCache;

	private static List<PropInstance> childSearchCache = new List<PropInstance>(32);

	private InputFieldNavigationData inputFieldNavigationDataSpecial = new InputFieldNavigationData();

	private static readonly string CLEAN_SCENE_SAVE_TEXT = "Save";

	private static readonly string DIRTY_SCENE_SAVE_TEXT = "Save" + " ✱".Colored(Color.orangeRed);

	private readonly UndoSystem undo = new UndoSystem();

	private readonly List<UserAsset> userAssets = new List<UserAsset>();

	private readonly List<string> supportedAssetFormats = new List<string>();

	private readonly List<string> supportedTextureAssetFormats = new List<string> { ".png", ".jpg", ".jpeg" };

	private readonly List<string> supportedMaterialAssetFormats = new List<string> { ".es2mat" };

	private readonly List<string> supportedAudioAssetFormats = new List<string> { ".mp3", ".wav", ".aiff", ".ogg" };

	private readonly List<string> supportedScriptAssetFormats = new List<string> { ".lua" };

	private readonly List<string> supportedModelAssetFormats = new List<string> { ".gltf", ".glb" };

	public WalkthroughUI walkthroughUI;

	private List<CustomWalkthroughHandler.Header> walkthroughSteps = new List<CustomWalkthroughHandler.Header>();

	public Action<int> onDeleteHeader;

	private string roomDirPath
	{
		get
		{
			return _roomDirPath;
		}
		set
		{
			UnityEngine.Debug.Log("Setting room dir path to: " + value);
			_roomDirPath = value;
			editorUI.RoomFolder.text = "Room Folder: " + Path.GetFileName(roomDirPath);
		}
	}

	private bool isDragAndDrop => dragAndDropItem != null;

	public static TimeBudgetPerFrameDeferAgent timeBudgetDeferAgent
	{
		get
		{
			if (!_timeBudgetDeferAgent)
			{
				_timeBudgetDeferAgent = new GameObject("Time budget defer agent").AddComponent<TimeBudgetPerFrameDeferAgent>();
			}
			return _timeBudgetDeferAgent;
		}
	}

	private void Start()
	{
		UnityEngine.Random.InitState(DateTime.Now.GetHashCode());
		Cursor.lockState = CursorLockMode.None;
		isUsingRoomEditor = true;
		reInputDispatcher = new InputDispatcher(menuOptions, () => InputDispatcher.AlwaysTrueContext.True);
		assetsBundles = loadAssetsBundles();
		assetsIconsBundle = loadAssetsIconsBundle();
		loadPropIcons();
		PlayerSave.init(Language.English);
		Controller.init();
		Localization.init(PlayerSave.getSettings().language);
		initAudio();
		initUI();
		initMaterials();
		initAssetBrowser();
		initSelection();
		initGrid();
		initPolygonTool();
		initUserAssets();
		initRoomLoading();
		initCamera();
		initVolumes();
		initRoomPicker();
		initGizmoAxis();
		initDebug();
		initBetaMessage();
		RichPresence.init("");
		RichPresence.instance.setRichPressence("RoomEditor");
		Localization.translateCurrentScene();
	}

	private void initBetaMessage()
	{
		VisualControl visualControl = new VisualControl("betaOk", ControllerButtonActionType.UIConfirmPrimary, "%ok%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
		visualControl.addKeybindAction(KeyBindingAction.Chat, menuOptions);
		betaMessageUI.ES2Frame.addOrUpdateControl(visualControl);
		betaMessageUI.ES2Frame.setup(reInputDispatcher, null, null, onFrameClick);
	}

	private void initAudio()
	{
		Bus bus = PineFmod.getBus("bus:/Music");
		PineFmod.setVolume(bus, PlayerSave.getSettings().musicEnabled ? (PlayerSave.getSettings().musicVolume / 100f) : 0f);
		PineFmod.lockChannelGroup(bus);
		Bus bus2 = PineFmod.getBus("bus:/Sound Effects");
		PineFmod.setVolume(bus2, PlayerSave.getSettings().soundEnabled ? (PlayerSave.getSettings().soundVolume / 100f) : 0f);
		PineFmod.lockChannelGroup(bus2);
		PineFmod.flushCommands();
	}

	private void initUI()
	{
		PineUI.init();
		PineUI.addButtonInteractionDelegate(onButtonEvent);
		PineUI.addButtonClickDelegate(onButtonClick);
		PineUI.addButtonClickDelegate(onButtonClickWalkthrough);
		PineUI.addButtonClickDelegate(onButtonClickRoomPicker);
		PineUI.addButtonClickPostDelegate(onButtonPostClick);
		PineUI.addToggleDelegate(onToggleChange);
		PineUI.addSliderDelegate(onSliderChange);
		foreach (Transform item in propertiesUI.Content)
		{
			if (item.TryGetComponent<TargetArrayPropertyUI>(out var component))
			{
				component.TargetArrayAdd.gameObject.SetActive(value: false);
				component.TargetArrayElement.gameObject.SetActive(value: false);
			}
			item.gameObject.SetActive(value: false);
		}
		removeTriggerEntry(editorUI.PrefsUI_Background.gameObject);
		addTriggerEntry(editorUI.PrefsUI_Background.gameObject, EventTriggerType.PointerClick).callback.AddListener(delegate
		{
			editorUI.PrefsUI.gameObject.SetActive(value: false);
		});
		editorUI.GameVersion.text = "Version: " + Version.fullGameVersion;
		editorUI.SaveNotification.alpha = 0f;
		editorUI.PrefsUI.gameObject.SetActive(value: false);
		editorUI.SettingsContent_OpenRoomFile.gameObject.SetActive(Is.Editor);
		colorPickerUI.gameObject.SetActive(value: false);
		virtualCursorUI.MovementSpeed.gameObject.SetActive(value: false);
		initPublishingUI();
		initMenuOptions();
		initTabs();
		initSearchUI();
		initColorPicker();
		Themes theme = Menu.getTheme();
		ThemeingObject[] componentsInChildren = base.transform.GetComponentsInChildren<ThemeingObject>(includeInactive: true);
		foreach (ThemeingObject themeingObject in componentsInChildren)
		{
			theme.theme(themeingObject.gameObject);
		}
		refreshEditorUI();
		refreshPropHistoryUI();
		refresh.hiddenPropsMessageUI = true;
	}

	private void initColorPicker()
	{
		initInputField(colorPickerUI.ColorPicker.Hex);
		initInputField(colorPickerUI.ColorPicker.R.GetComponentInChildren<InputField>());
		initInputField(colorPickerUI.ColorPicker.G.GetComponentInChildren<InputField>());
		initInputField(colorPickerUI.ColorPicker.B.GetComponentInChildren<InputField>());
		initInputField(colorPickerUI.ColorPicker.A.GetComponentInChildren<InputField>());
		inputFieldsNavigationDataColorPicker.initNavigation(colorPickerUI.transform, "ColorPicker");
		static void initInputField(InputField field)
		{
			addTriggerEntry(field.gameObject, EventTriggerType.Select).callback.AddListener(delegate
			{
				InputFieldNavigationData.currentFocusedInputFieldName = field.name;
			});
			field.onEndEdit.AddListener(delegate
			{
				InputFieldNavigationData.currentFocusedInputFieldName = "";
			});
		}
	}

	private void initMenuOptions()
	{
		menuOptions.constructMenuOptions(isVR: false, isEditor: true, isInGame: false, eventSystem, stopRebinding, startRebinding, delegate
		{
			Localization.init(PlayerSave.getSettings().language);
			Localization.translateCurrentScene();
			refresh.editorUI = true;
			refresh.propertiesUI = true;
		});
		menuOptions.keyBindingsEditorReset();
		menuOptions.keyBindingsLoadSave();
		menuOptions.frame.setup(new InputDispatcher(menuOptions, () => InputDispatcher.AlwaysTrueContext.True), null, null, onFrameClick);
		string keyBindingAsString = menuOptions.getKeyBindingAsString(KeyBindingAction.EditorEnterPlayMode, "P");
		setHoverTooltip("Enter Play Mode (" + keyBindingAsString + ")\nHold ALT to spawn at current camera location.", editorUI.Play, UITooltip.Position.Right);
		string keyBindingAsString2 = menuOptions.getKeyBindingAsString(KeyBindingAction.EditorUndo, "CTRL + Z");
		setHoverTooltip("Undo (" + keyBindingAsString2 + ")", editorUI.Undo, UITooltip.Position.Right);
		string keyBindingAsString3 = menuOptions.getKeyBindingAsString(KeyBindingAction.EditorRedo, "CTRL + Shift + Z");
		setHoverTooltip("Redo (" + keyBindingAsString3 + ")", editorUI.Redo, UITooltip.Position.Right);
		string keyBindingAsString4 = menuOptions.getKeyBindingAsString(KeyBindingAction.EditorGizmoObjectSpace, "Q");
		setHoverTooltip("Toggle object space to local (" + keyBindingAsString4 + ")", editorTransformUI.Space_Global, UITooltip.Position.Bot, 20f);
		setHoverTooltip("Toggle object space to world (" + keyBindingAsString4 + ")", editorTransformUI.Space_Local, UITooltip.Position.Bot, 20f);
		string keyBindingAsString5 = menuOptions.getKeyBindingAsString(KeyBindingAction.EditorGizmoPosition, "W");
		setHoverTooltip("Adjust object position (" + keyBindingAsString5 + ")", editorTransformUI.Move, UITooltip.Position.Bot, 20f);
		string keyBindingAsString6 = menuOptions.getKeyBindingAsString(KeyBindingAction.EditorGizmoRotation, "E");
		setHoverTooltip("Adjust object rotation (" + keyBindingAsString6 + ")", editorTransformUI.Rotate, UITooltip.Position.Bot, 20f);
		string keyBindingAsString7 = menuOptions.getKeyBindingAsString(KeyBindingAction.EditorGizmoScale, "R");
		setHoverTooltip("Adjust object scale (" + keyBindingAsString7 + ")", editorTransformUI.Scale, UITooltip.Position.Bot, 20f);
	}

	private void initVolumes()
	{
		editorVolumeRefs = levelContainer.GetComponentInChildren<EditorVolumeRefs>();
		editorPostProcessingDefault = new EditorPostProcessingData
		{
			exposure = editorVolumeRefs.exposure.fixedExposure.value,
			ssaoIntensity = editorVolumeRefs.ssao.intensity.value,
			ssaoRadius = editorVolumeRefs.ssao.radius.value,
			bloomThreshold = editorVolumeRefs.bloom.threshold.value,
			bloomIntensity = editorVolumeRefs.bloom.intensity.value,
			colorAdjustmentContrast = editorVolumeRefs.colorAdjustments.contrast.value,
			colorAdjustmentColorFilter = editorVolumeRefs.colorAdjustments.colorFilter.value,
			colorAdjustmentHue = editorVolumeRefs.colorAdjustments.hueShift.value,
			colorAdjustmentSaturation = editorVolumeRefs.colorAdjustments.saturation.value,
			chromaticAberrationIntensity = editorVolumeRefs.chromaticAberration.intensity.value,
			vignetteIntensity = editorVolumeRefs.vignette.intensity.value,
			grainIntensity = editorVolumeRefs.filmGrain.intensity.value,
			motionBlurIntensity = editorVolumeRefs.motionBlur.intensity.value,
			activeOnStart = false,
			transitionTime = 0f
		};
		skyboxDefaultCache = new SkyboxData
		{
			type = SkyboxType.Procedural,
			topColor = editorVolumeRefs.gradientSky.top.value,
			midColor = editorVolumeRefs.gradientSky.middle.value,
			botColor = editorVolumeRefs.gradientSky.bottom.value,
			exposure = editorVolumeRefs.gradientSky.exposure.value,
			horizonSize = editorVolumeRefs.gradientSky.gradientDiffusion.value
		};
		cloudsDefaultCache = new CloudsData
		{
			opacity = 0f,
			upperHemisphereOnly = true,
			opacityColorsR = 1f,
			opacityColorsG = 0f,
			opacityColorsB = 0f,
			opacityColorsA = 0f,
			rotation = 0f,
			tint = Color.white,
			exposure = 0f,
			useWind = false,
			windSpeed = 100f,
			windDirection = 0f,
			useWindShadows = false,
			windShadowsColor = Color.black
		};
		fogDefaultCache = new FogData
		{
			attenuation = 50f,
			distance = 100f,
			maxHeight = 50f,
			color = Color.white,
			isVolumetric = false,
			volumetricColor = Color.white
		};
		oceanDefaultCache = null;
	}

	private void Update()
	{
		updateDebug();
		updateRoomPicker();
		updateGrid();
		updateUndoSystem();
		updatePolygonTool();
		updateMaterials();
		updateLineConnections();
		optionDialog.update(reInputDispatcher);
		if (updateRoomLoading())
		{
			updateInput();
			updateDragAndDrop();
			updateSpecialMode();
			updateHover();
			updateRefresh();
			updatePropertiesUI();
			updateTabNavigation();
			updateRectSelection();
			updateCamera();
			updateGizmoAxis();
			updateCursor();
			updateSelectionOutline();
			updateAssets();
			updateSearchUI();
			updateHierarchy();
			publishInfo.update();
			tweener.processTweens(Time.deltaTime);
			UnityUtils.setCrashHandlerGame(null);
		}
	}

	private void FixedUpdate()
	{
		tweener.processFixedUpdateTweens(Time.fixedDeltaTime);
		Physics.Simulate(Time.fixedDeltaTime);
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		if (hasFocus)
		{
			refresh.userAssets = true;
			wasOnApplicationFocus = true;
		}
	}

	private void OnDestroy()
	{
		Net.setSession(null);
		publishInfo.destroy();
	}

	private void OnApplicationQuit()
	{
		PineFmod.destroy();
		Controller.destroy();
		SteamAPI.Shutdown();
		SharedWriter.dispose();
	}

	public static bool isCtrlPressed()
	{
		if (!Input.GetKey(KeyCode.LeftControl))
		{
			return Input.GetKey(KeyCode.RightControl);
		}
		return true;
	}

	private static bool isShiftPressed()
	{
		if (!Input.GetKey(KeyCode.LeftShift))
		{
			return Input.GetKey(KeyCode.RightShift);
		}
		return true;
	}

	private static bool isAltPressed()
	{
		if (!Input.GetKey(KeyCode.LeftAlt))
		{
			return Input.GetKey(KeyCode.RightAlt);
		}
		return true;
	}

	private static Vector2 getMouseRelPos()
	{
		Vector2 result = Input.mousePosition;
		result.x /= Screen.width;
		result.y /= Screen.height;
		return result;
	}

	private void updateInput()
	{
		transformGizmo.doUpdate();
		SpecialMode specialMode = special;
		if (specialMode != null && specialMode.mode == EditorMode.CodeEditor)
		{
			updateCodeEditorInput();
			return;
		}
		bool lmbDown = Input.GetMouseButtonDown(0);
		Input.GetMouseButton(0);
		bool lmbUp = Input.GetMouseButtonUp(0);
		bool mouseButtonDown = Input.GetMouseButtonDown(1);
		if (mouseButtonDown)
		{
			rmbDownTime = Time.time;
			rmbDownDistance = 0f;
			rmbLastPos = getMouseRelPos();
			rmbHeldState = RmbHeldState.ClickTesting;
		}
		if (Input.GetMouseButton(1) && !mouseButtonDown)
		{
			Vector2 mouseRelPos = getMouseRelPos();
			rmbDownDistance += Vector2.Distance(mouseRelPos, rmbLastPos);
			rmbLastPos = mouseRelPos;
		}
		bool rmbUp = Input.GetMouseButtonUp(1);
		float num = 0.15f;
		bool rmbClick = false;
		if (rmbHeldState != RmbHeldState.None && rmbUp)
		{
			if (rmbHeldState == RmbHeldState.ClickTesting)
			{
				rmbClick = true;
			}
			rmbHeldState = RmbHeldState.None;
		}
		else if (rmbHeldState == RmbHeldState.ClickTesting && (Time.time - rmbDownTime > num || rmbDownDistance * PlayerSave.getSettings().mouseSensitivity > 0.04f))
		{
			rmbHeldState = RmbHeldState.HeldFrame;
		}
		else if (rmbHeldState == RmbHeldState.HeldFrame)
		{
			rmbHeldState = RmbHeldState.Held;
		}
		bool mmbDown = Input.GetMouseButtonDown(2);
		bool mmbPressed = Input.GetMouseButton(2);
		bool mmbUp = Input.GetMouseButtonUp(2);
		bool mouseBackUp = Input.GetMouseButtonUp(3);
		bool ctrlPressed = isCtrlPressed();
		bool shiftPressed = isShiftPressed();
		bool altPressed = isAltPressed();
		bool commandPressed = Input.GetKey(KeyCode.LeftMeta) || Input.GetKey(KeyCode.RightMeta);
		bool enterDown = Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter);
		bool escaped = Input.GetKeyDown(KeyCode.Escape);
		bool isOverUI = EventSystem.current.IsPointerOverGameObject() || refresh.propertiesUI || refresh.editorUI;
		bool flag = special != null;
		bool inMainMode = !flag;
		bool flag2 = flag && special.mode == EditorMode.CameraRotation && special.updateZoomableCamera;
		bool disableCameraMovementInput = flag2 || rightClickUI.root.enabled;
		if (isDragAndDrop && (escaped || rmbClick))
		{
			shouldEatNextLMB = true;
			cancelDragAndDrop();
			return;
		}
		if (interactionInProgress())
		{
			handleInteractionInProgress();
			return;
		}
		if (isRebinding)
		{
			handleRebinding();
			return;
		}
		if (modalUIActive())
		{
			handleModalUI();
			return;
		}
		if (rectSelection.phase == RSP.Inactive)
		{
			handleSpecialModeInput();
			if (isPropSelected())
			{
				handlePropSelectedInput();
			}
			if (isPropInstanceSelected() && handlePropInstanceSelectedInput())
			{
				return;
			}
		}
		handleOtherInput();
		void adjustTargetCoordinate(float moveCoordinate, float startCoordinate, ref float targetCoordinate)
		{
			if (moveCoordinate != 0f)
			{
				float num2 = (float)Mathf.FloorToInt(startCoordinate / gridCustomPass.scale) * gridCustomPass.scale;
				float num3 = (float)Mathf.CeilToInt(startCoordinate / gridCustomPass.scale) * gridCustomPass.scale;
				float num4 = ((moveCoordinate > 0f) ? num3 : num2);
				if (num4 != startCoordinate)
				{
					targetCoordinate = num4;
				}
			}
		}
		void handleInteractionInProgress()
		{
			float axis = Input.GetAxis("Mouse X");
			float axis2 = Input.GetAxis("Mouse Y");
			Vector2 input = new Vector2(axis, axis2);
			if (cameraMouseControl == CamMouseControl.Pan && mmbPressed)
			{
				handleCameraPan(input);
			}
			if (rmbHeldState == RmbHeldState.Held && cameraMouseControl == CamMouseControl.Zoom)
			{
				float delta = (input.x + input.y) * PlayerSave.getSettings().re.cameraSpeed * 0.1f;
				handleCameraAxisMovement(delta, cameraMovementAxis);
			}
			if (rmbHeldState == RmbHeldState.Held && cameraMouseControl == CamMouseControl.WASD)
			{
				handleCameraRotate(input);
				handleCameraWasdMovement();
			}
			if (rmbUp && cameraMouseControl == CamMouseControl.WASD)
			{
				cameraMouseControl = CamMouseControl.None;
			}
			if (rmbUp && cameraMouseControl == CamMouseControl.Zoom)
			{
				cameraMouseControl = CamMouseControl.None;
			}
			if (mmbUp && cameraMouseControl == CamMouseControl.Pan)
			{
				cameraMouseControl = CamMouseControl.None;
			}
			if (transformGizmo.isTransforming)
			{
				transformGizmo.isSnapping = ctrlPressed;
				if (lmbUp)
				{
					transformGizmo.EndDragging();
					if (isSpecialMode(EditorMode.Pivot))
					{
						Slidable component2;
						if (special.target.TryGetComponent<Slot>(out var component))
						{
							if (pivotChanged(component.pivot))
							{
								var (propData, next) = prepChangeData(special.target);
								getSlotData(propData).pivot = special.cachedPivot;
								undo.push(actionChangeProp(propData, next, "special change slot pivot"));
							}
						}
						else if (special.target.TryGetComponent<Slidable>(out component2) && pivotChanged(component2.endNode))
						{
							var (propData2, next2) = prepChangeData(special.target);
							getSlidableData(propData2).endNode = special.cachedPivot;
							undo.push(actionChangeProp(propData2, next2, "special change slidable pivot"));
							destroySlidablePreview(special.target);
							createSlidablePreview(special.target, specialMode: true);
						}
					}
					else
					{
						bool flag3 = false;
						foreach (InstanceID propInstanceID in selection.propInstanceIDs)
						{
							PropInstance instanceByID = getInstanceByID(propInstanceID);
							flag3 = true;
							if (cachedTransforms.TryGetValue(propInstanceID, out var value))
							{
								flag3 = instanceByID.transform.position != value.position || instanceByID.transform.rotation.eulerAngles != value.rotation;
								if (!flag3)
								{
									flag3 = (value.isLocal ? instanceByID.transform.localScale : instanceByID.transform.lossyScale) != value.scale;
								}
							}
							if (flag3)
							{
								break;
							}
						}
						if (flag3)
						{
							List<EditorAction> list = new List<EditorAction>();
							foreach (InstanceID propInstanceID2 in selection.propInstanceIDs)
							{
								PropInstance instanceByID2 = getInstanceByID(propInstanceID2);
								if (!containsParentPropInstance(instanceByID2, selection.propInstanceIDs))
								{
									list.Add(actionChangeTransform(instanceByID2, "transform by gizmo"));
								}
							}
							undo.push(list.ToArray());
						}
					}
				}
			}
			if (rectSelection.phase == RSP.Active && lmbUp)
			{
				endRectSelection(apply: true, altPressed);
			}
			if (isSearchUiOpen() && !isOverUI && lmbUp)
			{
				closeSearchUI();
			}
			if (escaped)
			{
				cancelInteraction();
			}
			bool flag4 = !interactionInProgress();
			if (rmbClick && inMainMode && flag4)
			{
				popSelection();
			}
		}
		void handleModalUI()
		{
			if (escaped)
			{
				if (optionDialog.gameObject.activeInHierarchy)
				{
					optionDialog.hide();
				}
				else if (rebindingUI.enabled)
				{
					closeRebinding();
				}
				else if (rightClickUI.root.enabled)
				{
					closeRightClickUI();
				}
				else if (walkthroughUI.root.enabled)
				{
					closeWalkthroughUI();
				}
				else if (roomPickerUI.gameObject.activeInHierarchy)
				{
					if (canCloseRoomPicker)
					{
						roomPickerUI.gameObject.SetActive(value: false);
					}
				}
				else if (menuOptionsUI.enabled)
				{
					closeMenuOptions();
				}
				else if (menuUI.root.enabled)
				{
					menuUI.root.enabled = false;
				}
				else if (FileBrowser.IsOpen)
				{
					FileBrowser.HideDialog(invokeCancelCallback: true);
				}
				else if (metaUIActive())
				{
					if (publishInfo.isActive())
					{
						publishInfo.close(canActivateSaveOnExitModal: false);
					}
					toggleCanvasGroupUI(metaUI.gameObject, visible: false);
				}
				else if (configUIActive())
				{
					toggleCanvasGroupUI(configUI.gameObject, visible: false);
				}
				else if (assetBrowserUI.root.enabled)
				{
					closeAssetBrowser(assetBrowserUI);
				}
				else if (materialEditorUI.root.enabled)
				{
					closeMaterialEditor();
				}
				else if (soundPickerUI.root.enabled)
				{
					closeSoundPickerUI();
				}
			}
		}
		void handleOtherInput()
		{
			if (menuOptions.getActionKeyDown(KeyBindingAction.EditorToggleCodeEditor))
			{
				openTextEditor();
			}
			if (menuOptions.getActionKeyDown(KeyBindingAction.EditorEnterPlayMode, checkModifiers: false))
			{
				enterPlayMode(isAltPressed());
			}
			if (inMainMode)
			{
				bool flag3 = false;
				if (escaped || mouseBackUp)
				{
					flag3 = popSelection();
				}
				if (currentTab.type == EditorTab.Props && currentTab.selectedTag != PropTag.NONE && escaped && !flag3)
				{
					flag3 = true;
					onTabBack();
				}
				if (currentTab.type != EditorTab.Settings && escaped && !flag3)
				{
					flag3 = true;
					changeTab(EditorTab.Settings);
				}
				if (currentTab.type == EditorTab.Settings && escaped && !flag3)
				{
					flag3 = true;
					openRoomPicker(canBeClosed: true);
				}
				if (isSearchUiOpen() && escaped && !flag3)
				{
					flag3 = true;
					closeSearchUI();
				}
				if (menuOptions.getActionKeyDown(KeyBindingAction.EditorSave))
				{
					saveRoom();
				}
				if (menuOptions.getActionKeyDown(KeyBindingAction.EditorUnhideAll))
				{
					unhideProps();
				}
				if (menuOptions.getActionKeyDown(KeyBindingAction.EditorOpenSearch))
				{
					refreshSearchUI("", selectInput: true);
				}
				if (!inputFieldsNavigationDataProperties.isInputFieldFocused && !inputFieldNavigationDataSpecial.isInputFieldFocused && !inputFieldNavigationDataBuilding.isInputFieldFocused && !inputFieldsNavigationDataMaterials.isInputFieldFocused && !inputFieldsNavigationDataColorPicker.isInputFieldFocused && Input.GetKeyDown(KeyCode.Tab))
				{
					int type = (int)currentTab.type;
					type = (type + ((!isCtrlPressed()) ? 1 : (-1))) % tabs.Count;
					if (type < 0)
					{
						type = tabs.Count - 1;
					}
					changeTab((EditorTab)type);
					PineFmod.playOneShotSound("event:/Sound Effects/00 General/Menu Sound Effects/Small/UI_Small_03");
				}
			}
			if (inMainMode && !isPropSelected() && !isOverUI)
			{
				if (lmbDown && !transformGizmo.isTransforming)
				{
					rectSelection.startPos = Input.mousePosition;
					rectSelection.phase = RSP.Attempt;
					rectSelectionCanceled = false;
				}
				if (lmbUp && !transformGizmo.isTransforming && !rectSelectionCanceled)
				{
					if (shouldEatNextLMB)
					{
						shouldEatNextLMB = false;
					}
					else
					{
						rectSelection.phase = RSP.Inactive;
						Predicate<PropInstance> predicate = null;
						if ((ctrlPressed || shiftPressed) && altPressed)
						{
							predicate = (PropInstance p) => !isChildOfSelected(p, selection.propInstanceIDs);
						}
						PropInstance nextHitPropInstance = getNextHitPropInstance(predicate);
						if (nextHitPropInstance != null)
						{
							addSelection(nextHitPropInstance, ctrlPressed || shiftPressed, altPressed);
						}
						else if (!ctrlPressed && !shiftPressed && isPropInstanceSelected())
						{
							popSelection();
						}
					}
				}
				if (rmbClick && !rectSelectionCanceled)
				{
					rmbClick = false;
					if (shouldEatNextRMB)
					{
						shouldEatNextRMB = false;
					}
					else
					{
						bool flag4 = isPropInstanceSelected();
						bool num2 = ctrlPressed || shiftPressed;
						if (flag4)
						{
							setGizmoTargets(new List<PropInstance>());
						}
						if (!num2 && flag4)
						{
							popSelection();
						}
						if (getNextHitPropInstance() != null)
						{
							refreshRightClickUI();
						}
					}
				}
			}
			if (transformUI.root.enabled && transformUI.ToolsBackground.gameObject.activeSelf)
			{
				if (menuOptions.getActionKeyDown(KeyBindingAction.EditorGizmoObjectSpace))
				{
					toggleGizmoObjectSpace();
				}
				if (menuOptions.getActionKeyDown(KeyBindingAction.EditorGizmoPosition))
				{
					transformUI.Move.isOn = true;
				}
				if (menuOptions.getActionKeyDown(KeyBindingAction.EditorGizmoRotation) && transformUI.Rotate.interactable)
				{
					transformUI.Rotate.isOn = true;
				}
				if (menuOptions.getActionKeyDown(KeyBindingAction.EditorGizmoScale) && transformUI.Scale.interactable)
				{
					transformUI.Scale.isOn = true;
				}
			}
			if (lmbDown && !isOverUI)
			{
				closeTopUI();
			}
			if (!isOverUI)
			{
				if (lmbDown && transformGizmo.mainTargetRoot != null && isSpecialMode(EditorMode.Pivot))
				{
					Slidable component2;
					if (special.target.TryGetComponent<Slot>(out var component))
					{
						special.cachedPivot = saveTransform(component.pivot.transform, isLocal: false);
					}
					else if (special.target.TryGetComponent<Slidable>(out component2))
					{
						special.cachedPivot = saveTransform(component2.endNode.transform, isLocal: false);
					}
				}
				if (!disableCameraMovementInput)
				{
					if (rmbHeldState == RmbHeldState.HeldFrame)
					{
						if (ctrlPressed)
						{
							cameraMouseControl = CamMouseControl.Zoom;
							cameraMovementAxis = mainCam.transform.forward;
						}
						else if (shiftPressed)
						{
							cameraMouseControl = CamMouseControl.Zoom;
							cameraMovementAxis = mainCam.transform.up;
						}
						else if (altPressed)
						{
							cameraMouseControl = CamMouseControl.Zoom;
							cameraMovementAxis = mainCam.transform.right;
						}
						else
						{
							cameraMouseControl = CamMouseControl.WASD;
						}
					}
					if (mmbDown)
					{
						cameraMouseControl = CamMouseControl.Pan;
					}
				}
				if (!disableCameraMovementInput)
				{
					if (!isPropInstanceSelected())
					{
						handleCameraWasdMovement();
					}
					handleCameraScrollZoom(Input.mouseScrollDelta.y);
				}
			}
		}
		bool handlePropInstanceSelectedInput()
		{
			bool flag3 = false;
			Vector3 vector = handleSelectedObjectGridMovement(ctrlPressed, commandPressed);
			if (vector != Vector3.zero)
			{
				flag3 = true;
				List<EditorAction> actions = new List<EditorAction>();
				Dictionary<InstanceID, (bool isKinematic, bool detectCollisions)> exRBs = storeAndDestroyRBs();
				foreach (InstanceID id in selection.propInstanceIDs)
				{
					PropInstance instance = getInstanceByID(id);
					if (!containsParentPropInstance(instance, selection.propInstanceIDs))
					{
						Vector3 startPos = instance.transform.position;
						Vector3 targetPos = instance.transform.position + vector;
						if (shiftPressed)
						{
							adjustTargetCoordinate(vector.x, startPos.x, ref targetPos.x);
							adjustTargetCoordinate(vector.y, startPos.y, ref targetPos.y);
							adjustTargetCoordinate(vector.z, startPos.z, ref targetPos.z);
						}
						targetTransformTweens.Add(tweener.tween(instance.gameObject, 0.125f, 0f, deactivate: false, activate: false, destroy: false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, delegate(float t)
						{
							instance.gameObject.transform.position = Vector3.Lerp(startPos, targetPos, t);
						}, delegate
						{
							if (exRBs.TryGetValue(id, out (bool, bool) value))
							{
								Rigidbody component = instance.gameObject.GetComponent<Rigidbody>();
								(component.isKinematic, component.detectCollisions) = value;
							}
							actions.Add(actionChangeTransform(instance, "position step"));
							if (actions.Count == targetTransformTweens.Count)
							{
								targetTransformTweens.Clear();
								undo.push(actions.ToArray());
							}
						}, Interpolation.SmoothStep));
					}
				}
			}
			if (menuOptions.getActionKeyDown(KeyBindingAction.EditorRotate, checkModifiers: false) && !flag3)
			{
				flag3 = true;
				List<EditorAction> actions2 = new List<EditorAction>();
				Vector3 pivotPoint = transformGizmo.pivotPoint;
				Vector3 zero = Vector3.zero;
				if (transformGizmo.space == TransformSpace.Global)
				{
					zero = Vector3.up;
					if (shiftPressed)
					{
						zero = Vector3.forward;
					}
					else if (altPressed)
					{
						zero = Vector3.right;
					}
				}
				else
				{
					zero = transformGizmo.mainTargetRoot.up;
					if (shiftPressed)
					{
						zero = transformGizmo.mainTargetRoot.forward;
					}
					else if (altPressed)
					{
						zero = transformGizmo.mainTargetRoot.right;
					}
				}
				Dictionary<InstanceID, (bool isKinematic, bool detectCollisions)> exRBs2 = storeAndDestroyRBs();
				foreach (InstanceID instanceID in selection.propInstanceIDs)
				{
					PropInstance instance2 = getInstanceByID(instanceID);
					if (!containsParentPropInstance(instance2, selection.propInstanceIDs))
					{
						(Vector3, Quaternion) tuple = rotateAround(instance2.transform, pivotPoint, zero, 90f);
						Vector3 item = tuple.Item1;
						Quaternion item2 = tuple.Item2;
						List<PineTween> list = targetTransformTweens;
						PineTweenSystemEnableNoHandles pineTweenSystemEnableNoHandles = tweener;
						GameObject obj = instance2.gameObject;
						Vector3? positionTo = item;
						Quaternion? rotationTo = item2;
						list.Add(pineTweenSystemEnableNoHandles.tween(obj, 0.125f, 0f, deactivate: false, activate: false, destroy: false, null, null, null, null, null, positionTo, null, null, null, rotationTo, null, null, null, null, null, null, null, null, null, delegate
						{
							if (exRBs2.TryGetValue(instanceID, out (bool, bool) value))
							{
								Rigidbody component = instance2.gameObject.GetComponent<Rigidbody>();
								(component.isKinematic, component.detectCollisions) = value;
							}
							actions2.Add(actionChangeTransform(instance2, "rotation step"));
							if (actions2.Count == targetTransformTweens.Count)
							{
								targetTransformTweens.Clear();
								undo.push(actions2.ToArray());
							}
						}));
					}
				}
			}
			if (menuOptions.getActionKeyDown(KeyBindingAction.EditorFocus))
			{
				moveCameraTo(transformGizmo.pivotPoint);
			}
			if (menuOptions.getActionKeyDown(KeyBindingAction.EditorHideFocus) && !flag3)
			{
				flag3 = true;
				hideNonSelectedProps(includeChildProps: true);
			}
			if (menuOptions.getActionKeyDown(KeyBindingAction.EditorHide) && !flag3)
			{
				flag3 = true;
				hideSelectedProps(includeChildProps: true);
			}
			if (menuOptions.getActionKeyDown(KeyBindingAction.EditorHideOnlySelected) && !flag3)
			{
				flag3 = true;
				hideSelectedProps(includeChildProps: false);
			}
			if (menuOptions.getActionKeyDown(KeyBindingAction.EditorDuplicate) && !flag3)
			{
				flag3 = true;
				List<EditorAction> list2 = new List<EditorAction>();
				HashSet<InstanceID> hashSet = new HashSet<InstanceID>();
				Dictionary<InstanceID, InstanceID> dictionary = new Dictionary<InstanceID, InstanceID>();
				List<PropData> list3 = new List<PropData>();
				foreach (InstanceID propInstanceID3 in selection.propInstanceIDs)
				{
					PropInstance instanceByID = getInstanceByID(propInstanceID3);
					List<PropInstance> childPropInstances = getChildPropInstances(instanceByID, includeSelf: true, recursive: true, selection.propInstanceIDs);
					List<PropData> list4 = compressProps(childPropInstances);
					foreach (PropData item3 in list4)
					{
						dictionary[item3.ID] = getNextInstanceID();
					}
					list3.AddRange(list4);
					hashSet.Add(dictionary[list4[0].ID]);
				}
				patchIDs(list3, dictionary, removeMissingIDLinks: false);
				actionsLoadProps(list3, list2);
				list2.Add(actionChangeSelection(newInstanceSelection(hashSet)));
				undo.push(list2.ToArray());
			}
			if (menuOptions.getActionKeyDown(KeyBindingAction.EditorDelete) && !flag3)
			{
				flag3 = true;
				if (isSelectedSingle<Polygon>())
				{
					exitPolygonTool();
				}
				deleteProps(selection.propInstanceIDs, invokedByDeleteKey: true);
			}
			return flag3;
		}
		void handlePropSelectedInput()
		{
			bool flag3 = false;
			RaycastHit[] array = sharedHits;
			if (!isOverUI)
			{
				Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
				int num2 = Physics.RaycastNonAlloc(ray, array, float.PositiveInfinity, CAMERA_RAYCAST_MASK);
				int num3 = 0;
				for (int i = 0; i < num2; i++)
				{
					RaycastHit raycastHit = array[i];
					PropInstance componentInParent = raycastHit.transform.GetComponentInParent<PropInstance>();
					if (!(componentInParent != null) || !componentInParent.TryGetComponent<PropTags>(out var component) || !component.tags.Contains(PropTag.Logic) || !raycastHit.collider.isTrigger)
					{
						array[num3] = raycastHit;
						num3++;
					}
				}
				num2 = num3;
				Array.Sort(array, 0, num2, raycastComparer);
				if (num2 == 0 && new Plane(Vector3.up, new Vector3(0f, gridCustomPassVolume.transform.position.y, 0f)).Raycast(ray, out var enter) && enter > 0f)
				{
					array[0] = new RaycastHit
					{
						point = ray.GetPoint(enter),
						normal = Vector3.up,
						distance = enter
					};
					num2 = 1;
				}
				flag3 = num2 > 0;
			}
			propToPlacePosition = Vector3.zero;
			propToPlaceNormal = Vector3.up;
			propToPlaceRotation = Quaternion.identity;
			int num5;
			if (flag3)
			{
				RaycastHit raycastHit2 = array[0];
				Quaternion quaternion = Quaternion.identity;
				Vector3 rhs = raycastHit2.point - mainCam.transform.position;
				rhs.y = 0f;
				if (rhs.sqrMagnitude != 0f)
				{
					rhs.Normalize();
					float[] array2 = new float[4]
					{
						Vector3.Dot(new Vector3(1f, 0f, 0f), rhs),
						Vector3.Dot(new Vector3(-1f, 0f, 0f), rhs),
						Vector3.Dot(new Vector3(0f, 0f, 1f), rhs),
						Vector3.Dot(new Vector3(0f, 0f, -1f), rhs)
					};
					Vector3[] array3 = new Vector3[4]
					{
						new Vector3(0f, 90f, 0f),
						new Vector3(0f, 270f, 0f),
						new Vector3(0f, 360f, 0f),
						new Vector3(0f, 180f, 0f)
					};
					float num4 = 0f;
					Vector3 euler = Vector3.zero;
					for (int j = 0; j < 4; j++)
					{
						if (array2[j] > num4)
						{
							num4 = array2[j];
							euler = array3[j];
						}
					}
					quaternion = Quaternion.Euler(euler);
				}
				Quaternion rotation = propToPlaceOriginalRotation;
				Quaternion rotation2 = quaternion * propToPlace.transform.rotation;
				Item component2 = propToPlace.GetComponent<Item>();
				if (component2 != null && component2.groundRotation != Vector3.zero)
				{
					rotation2 = quaternion * Quaternion.Euler(component2.groundRotation);
				}
				EditorNonItemPoses component3 = propToPlace.GetComponent<EditorNonItemPoses>();
				if (component3 != null && component3.groundRotation != Vector3.zero)
				{
					rotation2 = quaternion * Quaternion.Euler(component3.groundRotation);
				}
				if (propToPlace.GetComponent<Floor>() != null)
				{
					rotation2 = Quaternion.identity;
				}
				propToPlace.transform.rotation = rotation2;
				Vector3 vector = Vector3.Scale(propToPlace.transform.lossyScale, propToPlace.transform.rotation * propPreviewBounds.center);
				Vector3 vector2 = Vector3.Scale(propToPlace.transform.localScale, propToPlace.transform.localRotation * propPreviewBounds.extents);
				vector2.x = Mathf.Abs(vector2.x);
				vector2.y = Mathf.Abs(vector2.y);
				vector2.z = Mathf.Abs(vector2.z);
				propToPlacePosition = raycastHit2.point - vector + Vector3.up * vector2.y + new Vector3(vector2.x * raycastHit2.normal.x, 0f, vector2.z * raycastHit2.normal.z);
				propToPlaceNormal = raycastHit2.normal;
				propToPlaceRotation = propToPlace.transform.rotation;
				propToPlace.transform.rotation = rotation;
				Wall component4 = null;
				Carving component5 = null;
				if (raycastHit2.transform != null && raycastHit2.transform.parent != null && raycastHit2.transform.parent.TryGetComponent<Wall>(out component4) && component4.meshFilter == raycastHit2.transform.GetComponent<MeshFilter>())
				{
					num5 = (propToPlace.TryGetComponent<Carving>(out component5) ? 1 : 0);
					if (num5 != 0)
					{
						propToPlace.transform.rotation = propToPlaceOriginalRotation;
						propToPlaceRotation = Quaternion.LookRotation(component4.planeWorldNormal, component4.planeWorldUp);
						propToPlace.transform.rotation = propToPlaceRotation;
						Transform verticesParent = component5.verticesParent;
						Vector3 position = verticesParent.GetChild(0).position;
						Vector3 position2 = verticesParent.GetChild(1).position;
						Vector3 normalized = Vector3.Cross(rhs: verticesParent.GetChild(2).position - position, lhs: position2 - position).normalized;
						propToPlaceRotation = Quaternion.FromToRotation(normalized, component4.planeWorldNormal) * propToPlaceRotation;
						propToPlace.transform.rotation = propToPlaceOriginalRotation;
						Plane plane = new Plane(component4.planeWorldNormal, component4.transform.position);
						Ray ray2 = mainCam.ScreenPointToRay(Input.mousePosition);
						plane.Raycast(ray2, out var enter2);
						propToPlacePosition = ((enter2 > 0f) ? ray2.GetPoint(enter2) : Vector3.zero);
						component4.forceConvexCollider = true;
						propToPlace.transform.SetParent(component4.transform);
						if (propPreviewOriginalMaterials == null)
						{
							propPreviewOriginalMaterials = setGhostMaterial(propPreview);
						}
						goto IL_07e4;
					}
				}
				else
				{
					num5 = 0;
				}
				propToPlace.transform.SetParent(levelContainer.transform);
				if (propPreviewOriginalMaterials != null)
				{
					restoreOriginalMaterials(propPreviewOriginalMaterials);
					propPreviewOriginalMaterials = null;
				}
				goto IL_07e4;
			}
			propPreview.SetActive(value: false);
			goto IL_0a0c;
			IL_0a0c:
			if (flag3 && !isDragAndDrop && lmbDown)
			{
				placeSelectedProp();
			}
			if (rmbClick)
			{
				rmbClick = false;
				popSelection();
			}
			return;
			IL_07e4:
			bool flag4 = propToPlace.GetComponent<Polygon>() != null || propToPlace.GetComponent<Stairs>() != null || propToPlace.GetComponent<Carving>() != null;
			bool flag5 = !flag4 && shiftPressed;
			bool polygonSnapWhenPlacing = PlayerSave.getSettings().re.polygonSnapWhenPlacing;
			bool flag6 = flag4 && ((polygonSnapWhenPlacing && !shiftPressed) || (!polygonSnapWhenPlacing && shiftPressed));
			if (flag5 || flag6)
			{
				propToPlacePosition.x = Mathf.Round(propToPlacePosition.x / gridCustomPass.scale) * gridCustomPass.scale;
				if (flag4)
				{
					propToPlacePosition.y = Mathf.Round(propToPlacePosition.y / gridCustomPass.scale) * gridCustomPass.scale;
				}
				propToPlacePosition.z = Mathf.Round(propToPlacePosition.z / gridCustomPass.scale) * gridCustomPass.scale;
			}
			if (num5 != 0)
			{
				propToPlace.transform.position = propToPlacePosition;
				propToPlace.transform.rotation = propToPlaceRotation;
			}
			propPreview.transform.position = propToPlacePosition;
			propPreview.transform.rotation = propToPlaceRotation;
			propPreview.SetActive(value: true);
			refresh.transforms = true;
			goto IL_0a0c;
		}
		void handleRebinding()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				stopRebinding();
			}
			else
			{
				var (keyCode, list) = menuOptions.getAnyKeyWithMods();
				if (keyCode != KeyCode.None)
				{
					KeyCode[] modifiers = list.ToArray();
					menuOptions.resetIfKeybindingExists(menuOptions.currentKeyBindingActionToSet, keyCode, checkModifiers: true, modifiers);
					menuOptions.keyBindingsSetKey(keyCode, modifiers);
					stopRebinding();
				}
			}
		}
		void handleSpecialModeInput()
		{
			if (isSpecialMode(EditorMode.Screenshot))
			{
				if (enterDown)
				{
					toggleScreenshotMode(show: false, apply: true, Directory.Exists(roomDirPath));
				}
				if (escaped)
				{
					toggleScreenshotMode(show: false);
				}
			}
			else if (isSpecialMode(EditorMode.Pivot))
			{
				bool flag3 = special.target.GetComponent<TweenState>() != null;
				bool flag4 = special.target.GetComponent<Slot>() != null;
				bool flag5 = special.target.GetComponent<Slidable>() != null;
				if ((Application.isEditor ? Input.GetKeyDown(KeyCode.F6) : menuOptions.getActionKeyDown(KeyBindingAction.EditorUndo)) && flag5)
				{
					destroySlidablePreview(special.target);
					createSlidablePreview(special.target, specialMode: true);
				}
				if ((Application.isEditor ? Input.GetKeyDown(KeyCode.F7) : menuOptions.getActionKeyDown(KeyBindingAction.EditorRedo)) && flag5)
				{
					destroySlidablePreview(special.target);
					createSlidablePreview(special.target, specialMode: true);
				}
				if (enterDown)
				{
					if (flag3)
					{
						toggleTweenStateMode(show: false, apply: true);
					}
					else if (flag4)
					{
						toggleSlotPivotMode(show: false, apply: true);
					}
					else
					{
						toggleSlidablePivotMode(show: false, apply: true);
					}
				}
				if (escaped)
				{
					if (flag3)
					{
						toggleTweenStateMode(show: false);
					}
					else if (flag4)
					{
						toggleSlotPivotMode(show: false);
					}
					else
					{
						toggleSlidablePivotMode(show: false, apply: true);
					}
				}
			}
			else if (isSpecialMode(EditorMode.Target))
			{
				if (lmbDown)
				{
					PropInstance nextHitPropInstance = getNextHitPropInstance();
					if (nextHitPropInstance != null)
					{
						if (special.multiTarget)
						{
							if (special.targets.Contains(nextHitPropInstance))
							{
								special.targets.Remove(nextHitPropInstance);
								if (!isSpecialEffect(nextHitPropInstance))
								{
									outlineContext.outline.Remove(nextHitPropInstance.gameObject);
									outlineContext.dirty = true;
								}
							}
							else
							{
								special.targets.Add(nextHitPropInstance);
								if (!isSpecialEffect(nextHitPropInstance))
								{
									outlineObject(nextHitPropInstance.gameObject, clearOld: false);
								}
							}
						}
						else
						{
							special.target = nextHitPropInstance;
							if (!isSpecialEffect(nextHitPropInstance))
							{
								outlineObject(nextHitPropInstance.gameObject);
							}
							hidePasswordSelection();
						}
					}
				}
				bool flag6 = special.target != null || (special.multiTarget && (special.targets.Count > 0 || special.lockPasswordIndexes.Count > 0));
				targetModeUI.Buttons_Apply.gameObject.SetActive(flag6);
				if (enterDown && flag6)
				{
					exitTargetMode(apply: true);
				}
				if (escaped)
				{
					exitTargetMode();
				}
			}
			else if (isSpecialMode(EditorMode.CameraRotation))
			{
				if (enterDown)
				{
					toggleZoomableRotationMode(show: false, apply: true);
				}
				if (escaped)
				{
					toggleZoomableRotationMode(show: false);
				}
			}
		}
		bool pivotChanged(Transform pivot)
		{
			bool flag3 = pivot.position != special.cachedPivot.position || pivot.rotation.eulerAngles != special.cachedPivot.rotation;
			if (!flag3)
			{
				flag3 = (special.cachedPivot.isLocal ? pivot.localScale : pivot.lossyScale) != special.cachedPivot.scale;
			}
			return flag3;
		}
		static (Vector3, Quaternion) rotateAround(Transform transform, Vector3 point, Vector3 axis, float angle)
		{
			Quaternion quaternion = Quaternion.AngleAxis(angle, axis);
			Vector3 vector = quaternion * (transform.position - point);
			Vector3 item = point + vector;
			Quaternion rotation = transform.rotation;
			rotation *= Quaternion.Inverse(rotation) * quaternion * rotation;
			return (item, rotation);
		}
		Dictionary<InstanceID, (bool isKinematic, bool detectCollisions)> storeAndDestroyRBs()
		{
			Dictionary<InstanceID, (bool, bool)> dictionary = new Dictionary<InstanceID, (bool, bool)>();
			foreach (InstanceID propInstanceID4 in selection.propInstanceIDs)
			{
				if (getInstanceByID(propInstanceID4).TryGetComponent<Rigidbody>(out var component))
				{
					dictionary[propInstanceID4] = (component.isKinematic, component.detectCollisions);
					component.isKinematic = true;
					component.detectCollisions = false;
				}
			}
			return dictionary;
		}
	}

	private PropInstance placeSelectedProp()
	{
		if (propToPlace == null)
		{
			return null;
		}
		PropInstance component = propToPlace.GetComponent<PropInstance>();
		TransformData transformData = saveTransform(component.transform, isLocal: false);
		applyTransform(component.transform, new TransformData
		{
			position = propToPlacePosition,
			rotation = propToPlaceRotation.eulerAngles,
			scale = component.transform.lossyScale
		});
		List<PropInstance> childPropInstances = getChildPropInstances(component);
		foreach (PropInstance item in childPropInstances)
		{
			item.ID = getNextInstanceID();
		}
		List<PropData> propsData = compressProps(childPropInstances, checkUnparentComp: true);
		applyTransform(component.transform, transformData);
		List<EditorAction> list = new List<EditorAction>();
		actionsLoadProps(propsData, list);
		undo.push(list.ToArray());
		InstanceID iD = childPropInstances[0].ID;
		PropInstance createdInstance = getInstanceByID(iD);
		GameObject clone = UnityEngine.Object.Instantiate(createdInstance.gameObject, createdInstance.transform.parent);
		ImpostorClone.stripNonVisualComponents(clone, ImpostorClone.StrippingFlags.AllowAll);
		List<Renderer> visibleRenderers = new List<Renderer>();
		Renderer[] componentsInChildren = createdInstance.GetComponentsInChildren<Renderer>();
		foreach (Renderer renderer in componentsInChildren)
		{
			if (renderer.enabled)
			{
				visibleRenderers.Add(renderer);
				renderer.enabled = false;
			}
		}
		if (createdInstance.gameObject.GetComponentInChildren<ParticleSystem>() == null)
		{
			Vector3 jumpStart = propToPlacePosition + propToPlaceNormal * 0.2f;
			Vector3 jumpEnd = propToPlacePosition;
			tweener.tween(clone, 0.5f, 0f, deactivate: false, activate: false, destroy: false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, delegate(float t)
			{
				clone.transform.position = Vector3.Lerp(jumpStart, jumpEnd, t);
			}, delegate
			{
				UnityEngine.Object.Destroy(clone);
				if (createdInstance == null)
				{
					return;
				}
				foreach (Renderer item2 in visibleRenderers)
				{
					item2.enabled = true;
				}
			}, Interpolation.OutBounce);
		}
		else
		{
			UnityEngine.Object.Destroy(clone);
			if (createdInstance != null)
			{
				foreach (Renderer item3 in visibleRenderers)
				{
					item3.enabled = true;
				}
			}
		}
		addPropToHistory(selection.propID);
		return createdInstance;
	}

	private void deleteProps(HashSet<InstanceID> propInstanceIDs, bool invokedByDeleteKey)
	{
		int num = 0;
		if (invokedByDeleteKey)
		{
			EditorSelection newSelection = selection;
			newSelection.propInstanceIDs = new HashSet<InstanceID>();
			undo.push(actionChangeSelection(newSelection));
			num++;
		}
		List<PropInstance> list = new List<PropInstance>(propInstanceIDs.Count);
		HashSet<InstanceID> hashSet = new HashSet<InstanceID>(hiddenPropInstanceIDs);
		foreach (InstanceID propInstanceID in selection.propInstanceIDs)
		{
			hashSet.Add(propInstanceID);
		}
		foreach (InstanceID propInstanceID2 in propInstanceIDs)
		{
			PropInstance instanceByID = getInstanceByID(propInstanceID2);
			if (invokedByDeleteKey && propInstanceIDs.Count <= 10)
			{
				GameObject clone = UnityEngine.Object.Instantiate(instanceByID.gameObject, levelContainer.transform, worldPositionStays: true);
				ImpostorClone.stripNonVisualComponents(clone);
				Vector3 originalScale = clone.transform.localScale;
				float delay = UnityEngine.Random.Range(0f, 0.15f);
				tweener.tween(clone, 0.35f, delay, deactivate: false, activate: false, destroy: false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, delegate(float t)
				{
					clone.transform.localScale = Vector3.LerpUnclamped(originalScale, Vector3.zero, t);
				}, delegate
				{
					UnityEngine.Object.Destroy(UnityEngine.Object.Instantiate(poof, clone.transform.position, Quaternion.identity, levelContainer.transform), 3f);
					UnityEngine.Object.Destroy(clone);
				}, Interpolation.InBack);
			}
			List<PropInstance> childPropInstances = getChildPropInstances(instanceByID, includeSelf: true, recursive: true, propInstanceIDs);
			foreach (PropInstance item3 in childPropInstances)
			{
				(PropData prev, PropData next) tuple = prepChangeData(item3);
				PropData item = tuple.prev;
				PropData item2 = tuple.next;
				item2.parentID = InstanceID.None;
				undo.push(actionChangeProp(item, item2, "remove parent"));
				num++;
				if (invokedByDeleteKey && hashSet.Contains(item2.ID))
				{
					hashSet.Remove(item2.ID);
				}
			}
			foreach (PropInstance item4 in childPropInstances)
			{
				List<EditorAction> list2 = new List<EditorAction>();
				actionsRemoveFromTargeters(list2, item4, -1, targetIndexReduce: false, deletingTarget: true);
				if (list2.Count > 0)
				{
					undo.push(list2.ToArray());
					num += list2.Count;
				}
			}
			list.AddRange(childPropInstances);
		}
		if (invokedByDeleteKey)
		{
			undo.push(actionChangeHidden(hashSet));
			num++;
		}
		foreach (PropInstance item5 in list)
		{
			PropData propData = compressProp(item5, getEditorContext());
			if (editorPostProcessingOnStartActive != null && tryGetEditorPostProcessingData(propData, out var data) && data.activeOnStart)
			{
				editorPostProcessingOnStartActive = null;
				refresh.postProcessing = true;
			}
			if (skyboxOnStartActiveCache != null && tryGetSkyboxData(propData, out var data2) && data2.activeOnStart)
			{
				skyboxOnStartActiveCache = null;
				refresh.skybox = true;
			}
			if (cloudsOnStartActiveCache != null && tryGetCloudsData(propData, out var data3) && data3.activeOnStart)
			{
				cloudsOnStartActiveCache = null;
				refresh.clouds = true;
			}
			if (oceanOnStartActiveCache != null && tryGetOceanData(propData, out var data4) && data4.activeOnStart)
			{
				oceanOnStartActiveCache = null;
				refresh.ocean = true;
			}
			if (fogOnStartActiveCache != null && tryGetFogData(propData, out var data5) && data5.activeOnStart)
			{
				fogOnStartActiveCache = null;
				refresh.fog = true;
			}
			undo.push(actionChangeProp(propData, null, "delete"));
			num++;
		}
		undo.linkLast(num);
	}

	private void updateHover()
	{
		bool flag = (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) || refresh.propertiesUI || refresh.editorUI;
		bool flag2 = transformGizmo.mainTargetRoot == null || !transformGizmo.isTransforming;
		if (special == null && !flag && !interactionInProgress() && !isPropSelected() && flag2)
		{
			PropInstance nextHitPropInstance = getNextHitPropInstance();
			getPrevHitPropInstance();
			if (nextHitPropInstance != null)
			{
				bool flag3 = false;
				PropInstance propInstance = nextHitPropInstance;
				while (!flag3 && propInstance != null)
				{
					flag3 = selection.propInstanceIDs.Contains(propInstance.ID);
					propInstance = getParentInstance(propInstance);
				}
				if (!flag3)
				{
					hoverInstance = nextHitPropInstance;
				}
			}
		}
		if (hoverInstance != prevHoverInstance)
		{
			if (prevHoverInstance != null && !isSpecialEffect(prevHoverInstance))
			{
				outlineContext.specialOutline.Remove(prevHoverInstance.gameObject);
			}
			if (hoverInstance != null && !isSpecialEffect(hoverInstance))
			{
				outlineContext.specialOutline.Add(hoverInstance.gameObject);
			}
			outlineContext.dirty = true;
		}
		prevHoverInstance = hoverInstance;
		hoverInstance = null;
	}

	private void updateTabNavigation(bool isClear = false)
	{
		if (isClear)
		{
			inputFieldsNavigationDataProperties.clear();
			return;
		}
		inputFieldsNavigationDataProperties.update(isPropInstanceSelected());
		inputFieldNavigationDataBuilding.update(editorUI.PrefsUI.gameObject.activeInHierarchy);
		inputFieldsNavigationDataMaterials.update(materialEditorUI.root.enabled);
		inputFieldNavigationDataSpecial.update(targetModeUI.root.enabled);
		inputFieldsNavigationDataColorPicker.update(colorPickerUI.gameObject.activeSelf);
	}

	private void updateAssets()
	{
		updateAssetsRequests(assetsRequests);
		updateLoadingTextureOps(loadTexturesOps, staggeredTextureLoad, null);
		updateLoadingCustomModels(loadingCustomModels);
		if (currentTab.selectedTag == PropTag.Custom && Time.time - lastCheckedModelImports > 15f)
		{
			lastCheckedModelImports = Time.time;
			if (Directory.Exists(roomDirPath))
			{
				loadCustomProps(getEditorContext(), roomDirPath, levelContainer.customModelsParent.transform, delegate
				{
					refresh.editorUI = true;
					refresh.propertiesUI = true;
				}, fromEditor: true, propSprites, propIcons, missingModelImage);
			}
		}
		assetBrowserUI.Header_Folder.enabled = !string.IsNullOrEmpty(roomDirPath);
	}

	public void OnEnable()
	{
		foreach (GameObject item in instantiatedRuntime)
		{
			UnityEngine.Object.Destroy(item);
		}
		instantiatedRuntime.Clear();
		levelContainer.gameObject.SetActive(value: true);
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		Physics.simulationMode = SimulationMode.Script;
		if (special != null)
		{
			restoreSelection();
			exitSpecialMode();
		}
		restoreHiddenProps();
		navMeshPreview.gameObject.SetActive(value: true);
		navMeshCrouchPreview.gameObject.SetActive(value: true);
		customPassAfterPostprocess.enabled = true;
		if (gizmoAxisCustomPass != null)
		{
			gizmoAxisCustomPass.enabled = true;
		}
		PineFmod.killSoundsInBus(RuntimeManager.GetBus("bus:/Sound Effects"));
		PineFmod.killSoundsInBus(RuntimeManager.GetBus("bus:/Music"));
	}

	public void OnDisable()
	{
		if (levelContainer != null)
		{
			levelContainer.gameObject.SetActive(value: false);
		}
	}

	private void onFrameClick(string id)
	{
		if (id.StartsWith("rp_"))
		{
			onRoomPickerFrameClick(id);
			return;
		}
		switch (id)
		{
		case "back":
			if (menuOptionsUI.enabled)
			{
				closeMenuOptions();
			}
			break;
		case "betaOk":
			betaMessageUI.gameObject.SetActive(value: false);
			break;
		case "discard":
			startLoadingRoomInEditor(getRoomMeta(new DirectoryInfo(roomDirPath)));
			break;
		case "ok":
			saveRoom();
			break;
		case "importCustomModel":
			importUserAsset(currentlyImportingGltfPath, isCustomModel: true);
			break;
		case "newPremade":
		{
			string path = createPremadesFolder();
			UnityEngine.Debug.Log(optionDialog.inputField.text);
			string text2 = optionDialog.inputField.text;
			text2 = text2.Replace("/", "-");
			string path2 = Path.Combine(path, text2 + ".json");
			if (File.Exists(path2))
			{
				optionDialog.showNoTranslate("Cannot create premade", "A premade with this name already exists.", onFrameClick, new VisualControl("ignore", ControllerButtonActionType.UIConfirmPrimary, "%RoomEditor_OK%", VisualControl.ControlType.Button, VisualControl.Position.BotCenter));
			}
			else
			{
				string contents = JsonUtility.ToJson(compressProp(currentlySavingPremade, getEditorContext()));
				File.WriteAllText(path2, contents);
			}
			currentlySavingPremade = null;
			break;
		}
		case "newScriptFromInspector":
		case "newScriptFromTextEditor":
		case "newScriptFromAssetBrowser":
		{
			string text3 = optionDialog.inputField.text;
			char invalidSymbol2;
			if (string.IsNullOrWhiteSpace(text3))
			{
				openNewScriptDialog(id, "Name cannot be empty. Please enter non-empty name.".Colored(Color.darkRed));
			}
			else if (tryGetInvalidSymbolInFileName(text3, out invalidSymbol2))
			{
				openNewScriptDialog(id, $"Name contains invalid symbol: {invalidSymbol2}\nPlease enter a different name.".Colored(Color.darkRed), text3);
			}
			else if (File.Exists(Path.Combine(roomDirPath, text3 + ".lua")))
			{
				openNewScriptDialog(id, "Script with the same name already exists.".Colored(Color.darkRed), text3);
			}
			else
			{
				createNewScript(text3, id == "newScriptFromInspector");
			}
			break;
		}
		case "newScriptCancel":
			textEditorUI.Editor.disableInput = textEditorPath == null;
			break;
		case "newMaterialFromAssetBrowser":
		case "newMaterialFromAssetPicker":
		case "newMaterialFromInspector":
		case "newMaterialDuplicate":
		{
			string text = optionDialog.inputField.text;
			if (string.IsNullOrWhiteSpace(text))
			{
				openNewMaterialDialog(id, "Name cannot be empty. Please enter non-empty name.".Colored(Color.darkRed));
				break;
			}
			if (tryGetInvalidSymbolInFileName(text, out var invalidSymbol))
			{
				openNewMaterialDialog(id, $"Name contains invalid symbol: {invalidSymbol}\nPlease enter a different name.".Colored(Color.darkRed), text);
				break;
			}
			if (File.Exists(Path.Combine(roomDirPath, text + ".es2mat")))
			{
				openNewMaterialDialog(id, "Material with the same name already exists.".Colored(Color.darkRed), text);
				break;
			}
			switch (id)
			{
			case "newMaterialFromAssetBrowser":
			case "newMaterialFromAssetPicker":
				createDefaultMaterialAsset(text, id == "newMaterialFromAssetBrowser");
				break;
			case "newMaterialFromInspector":
				createCopyMaterialAsset(text);
				break;
			case "newMaterialDuplicate":
				createDuplicateMaterialAsset(text);
				break;
			}
			break;
		}
		case "polygonToolDiscard":
			exitPolygonTool();
			break;
		}
	}

	private void onButtonEvent(Button button, EventTriggerType type, PointerEventData data)
	{
		switch (type)
		{
		case EventTriggerType.PointerEnter:
			Game.onButtonPointerEnter(button, handCursor, isRoomEditor: true);
			break;
		case EventTriggerType.PointerExit:
			Game.onButtonPointerExit(button, defaultCursor);
			break;
		}
		onTabButtonEvent(button, type);
		onTopLeftButtonEvent(button, type);
		onAssetBrowserItemButtonEvent(button, type);
		onPropButtonEvent(button, type);
	}

	private void onTopLeftButtonEvent(Button button, EventTriggerType type)
	{
		if ((!(button != editorUI.Play) || !(button != editorUI.Undo) || !(button != editorUI.Redo) || !(button != editorUI.PrefsToggle)) && button.TryGetComponent<UnityEngine.UI.Image>(out var component))
		{
			UnityEngine.UI.Image image = component;
			Color color;
			switch (type)
			{
			case EventTriggerType.PointerEnter:
			{
				Color black3 = Color.black;
				float? a = 0.9f;
				color = black3.With(null, null, null, a);
				break;
			}
			case EventTriggerType.PointerExit:
				color = Color.black;
				break;
			case EventTriggerType.PointerDown:
			{
				Color black2 = Color.black;
				float? a = 0.8f;
				color = black2.With(null, null, null, a);
				break;
			}
			case EventTriggerType.PointerClick:
			{
				Color black = Color.black;
				float? a = 0.9f;
				color = black.With(null, null, null, a);
				break;
			}
			default:
				color = component.color;
				break;
			}
			image.color = color;
		}
	}

	private void onAssetBrowserItemButtonEvent(Button button, EventTriggerType type)
	{
		if ((!(button.GetComponent<AddNewItemUI>() == null) || !(button.GetComponent<TexItemUI>() == null) || !(button.GetComponent<MaterialItemUI>() == null) || !(button.GetComponent<AudioItemUI>() == null) || !(button.GetComponent<ScriptItemUI>() == null) || !(button.GetComponent<CustomModelItemUI>() == null)) && button.TryGetComponent<UnityEngine.UI.Image>(out var component))
		{
			if (type == EventTriggerType.PointerEnter && assetBrowserUI.Header_Search_Input.isFocused)
			{
				assetBrowserUI.Header_Search_Input.DeactivateInputField();
			}
			UnityEngine.UI.Image image = component;
			Color color;
			switch (type)
			{
			case EventTriggerType.PointerEnter:
			{
				Color white4 = Color.white;
				float? a = 0.02f;
				color = white4.With(null, null, null, a);
				break;
			}
			case EventTriggerType.PointerExit:
			{
				Color white3 = Color.white;
				float? a = 0f;
				color = white3.With(null, null, null, a);
				break;
			}
			case EventTriggerType.PointerDown:
			{
				Color white2 = Color.white;
				float? a = 0.04f;
				color = white2.With(null, null, null, a);
				break;
			}
			case EventTriggerType.PointerClick:
			{
				Color white = Color.white;
				float? a = 0.02f;
				color = white.With(null, null, null, a);
				break;
			}
			default:
				color = component.color;
				break;
			}
			image.color = color;
		}
	}

	private void onPropButtonEvent(Button button, EventTriggerType type)
	{
		if (button.TryGetComponent<PropButtonUI>(out var component))
		{
			UnityEngine.UI.Image background = component.Background;
			Color color;
			switch (type)
			{
			case EventTriggerType.PointerEnter:
			{
				Color white4 = Color.white;
				float? a = 0.05f;
				color = white4.With(null, null, null, a);
				break;
			}
			case EventTriggerType.PointerExit:
			{
				Color white3 = Color.white;
				float? a = 0f;
				color = white3.With(null, null, null, a);
				break;
			}
			case EventTriggerType.PointerDown:
			{
				Color white2 = Color.white;
				float? a = 0.08f;
				color = white2.With(null, null, null, a);
				break;
			}
			case EventTriggerType.PointerClick:
			{
				Color white = Color.white;
				float? a = 0.05f;
				color = white.With(null, null, null, a);
				break;
			}
			default:
				color = component.Background.color;
				break;
			}
			background.color = color;
		}
	}

	private void onButtonClick(Button button)
	{
		if (button == editorUI.Play)
		{
			enterPlayMode(isAltPressed());
		}
		if (button == editorUI.Undo)
		{
			undo.undoLast(log: true);
		}
		if (button == editorUI.Redo)
		{
			undo.redoLast(dry: false, log: true);
		}
		if (button == editorUI.PrefsToggle)
		{
			editorUI.PrefsUI.gameObject.SetActive(!editorUI.PrefsUI.gameObject.activeSelf);
		}
		if (button == editorUI.PropsHeader_Back)
		{
			onTabBack();
		}
		if (button == editorUI.PropsHeader_Filter_SearchClearButton)
		{
			editorUI.PropsHeader_Filter_Input.text = string.Empty;
			editorUI.PropsHeader_Filter_Input.Select();
		}
		if (button == editorUI.Settings)
		{
			changeTab(EditorTab.Settings);
		}
		if (button == editorUI.Props)
		{
			changeTab(EditorTab.Props);
		}
		if (button == editorUI.Logic)
		{
			changeTab(EditorTab.Logic);
		}
		if (button == editorUI.Building)
		{
			changeTab(EditorTab.Building);
		}
		if (button == editorUI.Assets)
		{
			changeTab(EditorTab.Assets);
		}
		if (button == editorUI.SettingsContent_Save)
		{
			saveRoom();
			menuUI.root.enabled = false;
		}
		if (button == editorUI.SettingsContent_Publish)
		{
			publishInfo.open(publishingRoomInfo);
			toggleCanvasGroupUI(metaUI.gameObject, visible: true);
		}
		if (button == editorUI.SettingsContent_Options)
		{
			menuOptionsUI.enabled = true;
			menuOptions.scrollView.verticalNormalizedPosition = 1f;
		}
		if (button == editorUI.SettingsContent_Discard)
		{
			discard();
		}
		if (button == editorUI.SettingsContent_Walkthrough)
		{
			walkthroughUI.root.enabled = true;
		}
		if (button == editorUI.SettingsContent_ShowInitialMenu)
		{
			openRoomPicker(canBeClosed: true);
		}
		if (button == editorUI.SettingsContent_OpenUGC)
		{
			if (!string.IsNullOrEmpty(roomDirPath) && Directory.Exists(roomDirPath))
			{
				openFile(roomDirPath);
			}
			else
			{
				string persistentDataPath = Application.persistentDataPath;
				string text = Path.Combine(persistentDataPath, "UGC");
				if (Directory.Exists(text))
				{
					Application.OpenURL("file://" + text);
				}
				else if (Directory.Exists(persistentDataPath))
				{
					Application.OpenURL("file://" + persistentDataPath);
				}
			}
			menuUI.root.enabled = false;
		}
		if (button == editorUI.SettingsContent_OpenRoomFile)
		{
			openFile(Path.Combine(roomDirPath, "Room.room"));
		}
		if (button == editorUI.SettingsContent_ShowLevelCode)
		{
			openTextEditor();
		}
		if (button == editorUI.SettingsContent_ExitSave)
		{
			saveRoom();
			unloadAndGotoMenu();
		}
		if (button == menuUI.Close || button == menuUI.Background)
		{
			if (menuOptionsUI.enabled)
			{
				closeMenuOptions();
			}
			else if (rebindingUI.enabled)
			{
				closeRebinding();
			}
			else
			{
				menuUI.root.enabled = false;
			}
		}
		if (button == metaUI.Background)
		{
			toggleCanvasGroupUI(metaUI.gameObject, visible: false);
		}
		if (button == configUI.Background || button == configUI.ConfigInfo_CloseButton)
		{
			toggleCanvasGroupUI(configUI.gameObject, visible: false);
		}
		if (button == targetModeUI.Buttons_Apply || button == targetModeUI.Buttons_CancelExit)
		{
			bool apply = button == targetModeUI.Buttons_Apply;
			if (special.mode == EditorMode.Screenshot)
			{
				toggleScreenshotMode(show: false, apply, Directory.Exists(roomDirPath));
			}
			else if (special.mode == EditorMode.Pivot)
			{
				bool flag = special.target.GetComponent<TweenState>() != null;
				bool flag2 = special.target.GetComponent<Slot>() != null;
				_ = special.target.GetComponent<Slidable>() != null;
				if (flag)
				{
					toggleTweenStateMode(show: false, apply);
				}
				else if (flag2)
				{
					toggleSlotPivotMode(show: false, apply);
				}
				else if (flag)
				{
					toggleTweenStateMode(show: false, apply);
				}
				else
				{
					toggleSlidablePivotMode(show: false, apply);
				}
			}
			else if (special.mode == EditorMode.CameraRotation)
			{
				if (special.target.transform.TryGetComponent<Finish>(out var _))
				{
					toggleFinishRotationMode(show: false, apply);
				}
				else
				{
					toggleZoomableRotationMode(show: false, apply);
				}
			}
			else
			{
				exitTargetMode(apply);
			}
		}
		if (button == targetModeUI.ResetBackground_Reset)
		{
			special.target.originalPivot = new TransformData
			{
				isLocal = true,
				scale = Vector3.one
			};
			if (special.target.TryGetComponent<Slot>(out var component2))
			{
				applyTransform(component2.pivot.transform, special.target.originalPivot, ignoreScale: true);
			}
			else
			{
				special.target.TryGetComponent<Slidable>(out var component3);
				applyTransform(component3.endNode.transform, special.target.originalPivot, ignoreScale: true);
				destroySlidablePreview(special.target);
			}
		}
		if (button == propertiesUI.Header_PropLink)
		{
			PropInstance selectedInstance = getSelectedInstance();
			PropID propIDLinkForInstance = getPropIDLinkForInstance(selectedInstance);
			undo.push(actionChangeSelection(newPropSelection(propIDLinkForInstance)));
		}
		onAssetBrowserButtonClick(button);
		onTextEditorButtonClick(button);
		onPolygonToolButtonClick(button);
		if (button == soundPickerUI.Background)
		{
			closeSoundPickerUI();
		}
		if (button == transformUI.Space)
		{
			toggleGizmoObjectSpace();
		}
	}

	private void onButtonPostClick(Button button)
	{
		Game.onButtonPostClick(button, defaultCursor);
	}

	private void toggleGizmoObjectSpace()
	{
		transformGizmo.space = ((transformGizmo.space != TransformSpace.Local) ? TransformSpace.Local : TransformSpace.Global);
		transformUI.Space_Global.enabled = transformGizmo.space == TransformSpace.Global;
		transformUI.Space_Local.enabled = transformGizmo.space == TransformSpace.Local;
	}

	private void discard()
	{
		optionDialog.showNoTranslate("Discard changes?", "Are you sure you want to discard all changes?", onFrameClick, new VisualControl("discard", ControllerButtonActionType.UIConfirmPrimary, "%RoomEditor_Discard%", VisualControl.ControlType.Button, VisualControl.Position.BotRight), new VisualControl("ignore", ControllerButtonActionType.UIConfirmPrimary, "%no%", VisualControl.ControlType.Button, VisualControl.Position.BotRight));
	}

	private void unloadAndGotoMenu()
	{
		LoadingCanvas.EndCondition condition = LoadingCanvas.get().enqueueEndCondition(Localization.lookupInDictionary("Toy1_Loading"), Localization.lookupInDictionary("lobby1title"));
		Texture2D asset = AssetBundleLoader.getAsset<Texture2D>(AssetBundleType.RoomMetaAssets, "Assets/_RoomMetaAssets/Lobby1.jpg");
		LoadingCanvas.get().changeImage(asset);
		tweener.animate(delegate
		{
			bool flag = LoadingCanvas.get().isOpaque();
			if (!flag)
			{
				return flag;
			}
			if (assetsBundles != null)
			{
				foreach (AssetBundle assetsBundle in assetsBundles)
				{
					UnityEngine.Debug.Log("Unloading " + assetsBundle.name);
					assetsBundle.Unload(unloadAllLoadedObjects: true);
				}
			}
			if (assetsIconsBundle != null)
			{
				UnityEngine.Debug.Log("Unloading assetsIconsBundle");
				assetsIconsBundle.Unload(unloadAllLoadedObjects: true);
			}
			condition.done = true;
			PineSceneManager.loadScene("Lobby1", Localization.lookupInDictionary("lobby1title"), "", null);
			return flag;
		});
	}

	private void openFile(string url)
	{
		if (!string.IsNullOrEmpty(url) && (File.Exists(url) || Directory.Exists(url)))
		{
			Application.OpenURL("file://" + url);
			UnityEngine.Debug.Log("opening file: " + url);
		}
	}

	private void startLoadingRoomInEditor(RoomMeta targetRoomItem)
	{
		if (targetRoomItem == null)
		{
			UnityEngine.Debug.LogError("Cannot start loading room as 'targetRoomItem' is null.");
			return;
		}
		loadRoomOnStart = targetRoomItem.loadPath;
		EditorContext editorContext = getEditorContext();
		foreach (CustomModelObject customModelPrefab in customModelPrefabs)
		{
			UnityEngine.Object.Destroy(customModelPrefab.gameObject);
		}
		customModelPrefabs.Clear();
		updateLoadingScreenCustomModelNumbers(-1, -1);
		loadCustomProps(editorContext, loadRoomOnStart, levelContainer.customModelsParent.transform, delegate
		{
			int count = customModelPrefabs.Count;
			int modelsLoaded = count - loadingCustomModels.Count + 1;
			updateLoadingScreenCustomModelNumbers(count, modelsLoaded);
		}, fromEditor: true, propSprites, propIcons);
	}

	private void onToggleChange(Toggle toggle, bool value)
	{
		if (value)
		{
			if (toggle == transformUI.Move)
			{
				transformGizmo.transformType = TransformType.Move;
			}
			else if (toggle == transformUI.Rotate)
			{
				transformGizmo.transformType = TransformType.Rotate;
			}
			else if (toggle == transformUI.Scale)
			{
				transformGizmo.transformType = TransformType.Scale;
			}
			onPropButtonSizeToggle(toggle);
		}
		if (toggle == configUI.ConfigInfo_LegacyLightsToggle)
		{
			activateLegacyLights(value);
		}
		if (toggle == configUI.ConfigInfo_LegacyFloorCollidersToggle)
		{
			useNonLegacyFloorColliders = !value;
		}
		if (toggle == configUI.ConfigInfo_PlayerNameplateVisible)
		{
			hidePlayerNameplates = !value;
		}
		if (toggle == configUI.ConfigInfo_ItemNameplatesVisible)
		{
			hideItemNameplates = !value;
		}
		if (toggle == configUI.ConfigInfo_UseProximityChat)
		{
			useProximityChat = value;
		}
		if (toggle == configUI.ConfigInfo_UseOnStartSkybox)
		{
			useSkyboxPreview = value;
			refresh.skybox = true;
		}
		if (toggle == configUI.ConfigInfo_UseOnStartPostProcessing)
		{
			usePostProcessingPreview = value;
			refresh.postProcessing = true;
		}
		if (toggle == configUI.ConfigInfo_CamToLinkedProp)
		{
			camToLinkedProp = value;
		}
	}

	private void onSliderChange(Slider slider, float value)
	{
	}

	private void enterPlayMode(bool fromCurrentPos)
	{
		enterSpecialMode(EditorMode.Playmode);
		cancelSelectionWithRestoreFlag();
		updatePolygonTool();
		if (levelContainer.baseSpotLight.TryGetComponent<Light>(out var component))
		{
			RenderSettings.sun = component;
		}
		if (!string.IsNullOrEmpty(roomDirPath))
		{
			saveRoom(autosave: true);
		}
		refreshLineConnections(new List<PropInstance>());
		showAllHiddenProps();
		LevelContainerEditor levelContainerEditor = UnityEngine.Object.Instantiate(levelContainer);
		levelContainerEditor.name = "LevelContainer";
		if (fromCurrentPos)
		{
			GameObject gameObject = new GameObject("Spawn Point At Position");
			gameObject.transform.parent = levelContainerEditor.spawnPoints;
			gameObject.transform.rotation = mainCam.transform.rotation;
			Vector3 vector = mainCam.transform.position;
			int num = Physics.RaycastNonAlloc(vector, Vector3.down, playModeCacheHits);
			Array.Sort(playModeCacheHits, 0, num, raycastComparer);
			for (int i = 0; i < num; i++)
			{
				if (playModeCacheHits[i].transform.GetComponentInParent<Floor>() != null)
				{
					vector = playModeCacheHits[i].point;
					break;
				}
			}
			gameObject.transform.position = vector;
		}
		placeSpawnPoints(propInstances, levelContainerEditor);
		CustomLevelLoaderData customLevelData = new CustomLevelLoaderData
		{
			path = roomDirPath,
			hideItemNameplates = hideItemNameplates,
			hidePlayerNameplates = hidePlayerNameplates,
			useNonLegacyFloorColliders = useNonLegacyFloorColliders,
			useProximityChat = useProximityChat,
			title = "Room Editor",
			walkthrough = publishingRoomInfo.roomWalkthrough
		};
		generateCodeFile();
		LoadingCanvas.get().changeImage(null);
		LoadingCanvas.get().setOpaque();
		gameStarter.startTestingFromRoomEditor(customLevelData, levelContainerEditor.gameObject, base.gameObject);
		base.gameObject.SetActive(value: false);
		instantiatedRuntime.Add(levelContainerEditor.gameObject);
		gridCustomPass.enabled = false;
		gizmoAxisCustomPass.enabled = false;
		customPassAfterPostprocess.enabled = false;
		navMeshPreview.gameObject.SetActive(value: false);
		navMeshCrouchPreview.gameObject.SetActive(value: false);
	}

	private void activateLegacyLights(bool activate = true)
	{
		useNonLegacyLights = !activate;
	}

	private static void placeSpawnPoints(Dictionary<InstanceID, PropInstance> instances, LevelContainerEditor container)
	{
		int num = 0;
		foreach (var (_, propInstance2) in instances)
		{
			if (propInstance2.TryGetComponent<SpawnPoint>(out var component))
			{
				SpawnPoint spawnPoint = createSpawnPoint(propInstance2.transform.position, propInstance2.transform.rotation);
				spawnPoint.walkingSpeed = component.walkingSpeed;
				spawnPoint.runningSpeed = component.runningSpeed;
				num++;
			}
		}
		if (num == 0)
		{
			for (int i = 0; i < 8; i++)
			{
				createSpawnPoint(Vector3.zero, Quaternion.identity);
			}
			return;
		}
		int num2 = 8 - num;
		if (num2 != 0)
		{
			for (int j = 0; j < num2; j++)
			{
				Transform child = container.spawnPoints.GetChild(j % num);
				createSpawnPoint(child.position, child.rotation);
			}
		}
		SpawnPoint createSpawnPoint(Vector3 position, Quaternion rotation)
		{
			GameObject obj = new GameObject($"Spawn Point {container.spawnPoints.childCount}");
			obj.transform.parent = container.spawnPoints;
			obj.transform.position = position;
			obj.transform.rotation = rotation;
			return obj.AddComponent<SpawnPoint>();
		}
	}

	private void startRebinding()
	{
		UnityEngine.Debug.Log("startRebinding");
		isRebinding = true;
		menuOptions.pressKeyCanvas.gameObject.SetActive(value: true);
		menuOptions.pressKeyCanvas.GetComponent<CanvasGroup>().alpha = 1f;
	}

	private void stopRebinding()
	{
		isRebinding = false;
		menuOptions.pressKeyCanvas.gameObject.SetActive(value: false);
		menuOptions.pressKeyCanvas.GetComponent<CanvasGroup>().alpha = 0f;
	}

	private void closeRebinding()
	{
		rebindingUI.enabled = false;
	}

	private void closeMenuOptions()
	{
		menuOptionsUI.enabled = false;
	}

	private PropInstance getPrevHitPropInstance(Predicate<PropInstance> predicate = null)
	{
		return getHitPropInstance(-1, predicate);
	}

	private PropInstance getNextHitPropInstance(Predicate<PropInstance> predicate = null)
	{
		return getHitPropInstance(1, predicate);
	}

	private PropInstance getHitPropInstance(int direction, Predicate<PropInstance> predicate = null)
	{
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return null;
		}
		Physics.SyncTransforms();
		int hitsPtr = raycast.hitsPtr;
		int num = raycast.hitCounts[hitsPtr];
		RaycastHit[] array = raycast.hits[hitsPtr];
		raycast.hitsPtr = (raycast.hitsPtr + 1) % 2;
		RaycastHit[] array2 = raycast.hits[raycast.hitsPtr];
		Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
		raycast.hitCounts[raycast.hitsPtr] = Physics.RaycastNonAlloc(ray, array2, float.PositiveInfinity, CAMERA_RAYCAST_MASK);
		int num2 = raycast.hitCounts[raycast.hitsPtr];
		Array.Sort(array2, 0, num2, raycastComparer);
		bool flag = num == num2;
		HashSet<Transform> hashSet = new HashSet<Transform>();
		for (int i = 0; i < num; i++)
		{
			hashSet.Add(array[i].transform);
		}
		for (int j = 0; j < num && flag; j++)
		{
			flag = hashSet.Contains(array2[j].transform);
		}
		Vector2 a = raycast.mouse * new Vector2(1f / (float)Screen.width, 1f / (float)Screen.height);
		Vector2 b = Input.mousePosition * new Vector2(1f / (float)Screen.width, 1f / (float)Screen.height);
		bool flag2 = Vector2.Distance(a, b) > 0.005f;
		if (!flag || flag2)
		{
			raycast.mouse = Input.mousePosition;
			raycast.lastInstanceHit = -1;
			raycast.instanceCount = 0;
			HashSet<PropInstance> hashSet2 = new HashSet<PropInstance>();
			for (int k = 0; k < num2; k++)
			{
				Transform parent = array2[k].collider.transform;
				PropInstance component = null;
				InstanceID none = InstanceID.None;
				while (parent != null && (!parent.gameObject.TryGetComponent<PropInstance>(out component) || component.ID == none))
				{
					parent = parent.parent;
				}
				if (component != null && component.ID != none && !hashSet2.Contains(component))
				{
					hashSet2.Add(component);
					raycast.instances[raycast.instanceCount++] = component;
				}
			}
		}
		PropInstance propInstance = null;
		if (raycast.instanceCount > 0)
		{
			int num3 = 0;
			while (propInstance == null && num3++ < raycast.instanceCount)
			{
				raycast.lastInstanceHit += direction;
				if (raycast.lastInstanceHit < 0)
				{
					raycast.lastInstanceHit = raycast.instanceCount - 1;
				}
				raycast.lastInstanceHit %= raycast.instanceCount;
				PropInstance propInstance2 = raycast.instances[raycast.lastInstanceHit];
				bool flag3 = true;
				if (special != null)
				{
					flag3 = special.raycastPredicate(propInstance2);
				}
				if (predicate != null)
				{
					flag3 = flag3 && predicate(propInstance2);
				}
				if (flag3)
				{
					propInstance = propInstance2;
				}
			}
		}
		return propInstance;
	}

	private bool modalUIActive()
	{
		if (!metaUIActive() && !configUIActive() && !rebindingUI.enabled && !menuUI.root.enabled && !assetBrowserUI.root.enabled && !soundPickerUI.root.enabled && !importUI.root.enabled && !menuOptionsUI.enabled && !materialEditorUI.root.enabled && !walkthroughUI.root.enabled && !roomPickerUI.gameObject.activeInHierarchy && !optionDialog.gameObject.activeInHierarchy)
		{
			return FileBrowser.IsOpen;
		}
		return true;
	}

	private bool metaUIActive()
	{
		return metaUI.GetComponent<CanvasGroup>().interactable;
	}

	private bool configUIActive()
	{
		return configUI.GetComponent<CanvasGroup>().interactable;
	}

	private void toggleCanvasGroupUI(GameObject ui, bool visible)
	{
		CanvasGroup component = ui.GetComponent<CanvasGroup>();
		component.alpha = (visible ? 1f : 0f);
		component.interactable = visible;
		component.blocksRaycasts = visible;
	}

	private void toggleScreenshotMode(bool show, bool apply = false, bool roomSaved = true)
	{
		toggleCanvasGroupUI(metaUI.gameObject, !show);
		editorUI.root.enabled = !show;
		targetModeUI.root.enabled = show;
		targetModeUI.Buttons_Apply.gameObject.SetActive(show);
		targetModeUI.Properties.gameObject.SetActive(value: false);
		if (show)
		{
			enterSpecialMode(EditorMode.Screenshot);
			special.restoreCamPos = cameraPosition;
			special.restoreCamRot = cameraRotation;
			special.restoreCamCullingMask = mainCam.cullingMask;
			tweenCameraTo(cameraScreenshotPosition, cameraScreenshotRotation);
			mainCam.cullingMask = getScreenshotCameraCullingLayers(mainCam.cullingMask);
			cancelSelectionWithRestoreFlag();
			updatePolygonTool();
			return;
		}
		if (apply)
		{
			cameraScreenshotPosition = cameraPosition;
			cameraScreenshotRotation = cameraRotation;
			if (roomSaved)
			{
				publishInfo.changeLevelImage(takePreviewScreenshot());
			}
		}
		tweenCameraTo(special.restoreCamPos, special.restoreCamRot);
		mainCam.cullingMask = special.restoreCamCullingMask;
		restoreSelection();
		exitSpecialMode();
	}

	private OriginalMaterials[] setGhostMaterial(GameObject go, GameObject skip = null)
	{
		List<Renderer> allRenderers = new List<Renderer>();
		iterate(go.transform);
		int layer = LayerMask.NameToLayer("PlacingGhost");
		OriginalMaterials[] array = new OriginalMaterials[allRenderers.Count];
		for (int i = 0; i < allRenderers.Count; i++)
		{
			Renderer renderer = allRenderers[i];
			array[i] = new OriginalMaterials
			{
				renderer = renderer,
				materials = renderer.materials,
				layer = renderer.gameObject.layer
			};
			int num = renderer.sharedMaterials.Length;
			UnityEngine.Material[] array2 = new UnityEngine.Material[num];
			for (int j = 0; j < num; j++)
			{
				array2[j] = ghostMaterial;
			}
			renderer.materials = array2;
			renderer.gameObject.layer = layer;
		}
		return array;
		void iterate(Transform t)
		{
			if (!(t.gameObject == skip))
			{
				if (t.TryGetComponent<Renderer>(out var component))
				{
					allRenderers.Add(component);
				}
				for (int k = 0; k < t.childCount; k++)
				{
					iterate(t.GetChild(k));
				}
			}
		}
	}

	private static void restoreOriginalMaterials(OriginalMaterials[] originalMaterials)
	{
		foreach (OriginalMaterials originalMaterials2 in originalMaterials)
		{
			originalMaterials2.renderer.materials = originalMaterials2.materials;
			originalMaterials2.renderer.gameObject.layer = originalMaterials2.layer;
		}
	}

	private void addClickableIcon(PasswordPredicate passwordPredicate, PropInstance instance, bool multiSelect = false)
	{
		if (isPropHidden(instance))
		{
			return;
		}
		if (passwordPredicate != null && instance.TryGetComponent<Lock>(out var _lock))
		{
			PasswordPickerUI passwordPickerUI = UnityEngine.Object.Instantiate(targetModeUI.PasswordPicker, targetModeUI.root.transform);
			passwordPickerUI.gameObject.SetActive(value: true);
			PasswordPopup passwordPopup = new PasswordPopup
			{
				_lock = _lock,
				pickerUI = passwordPickerUI,
				entries = new PassEntryUI[_lock.password.Length],
				resetEntry = new PassEntryUI(),
				unlockEntry = new PassEntryUI()
			};
			special.passwordPopups.Add(passwordPopup);
			for (int i = -2; i < _lock.password.Length; i++)
			{
				int index = i;
				switch (i)
				{
				case -2:
					index = -500;
					break;
				case -1:
					index = -400;
					break;
				}
				bool flag = passwordPredicate(_lock, index);
				PassEntryUI passEntryUI = UnityEngine.Object.Instantiate(passwordPickerUI.PassEntry, passwordPickerUI.root.transform);
				passEntryUI.Disabled.enabled = !flag;
				passEntryUI.gameObject.SetActive(value: true);
				passEntryUI.root.interactable = flag;
				passEntryUI.root.onClick.AddListener(delegate
				{
					togglePasswordSelection(_lock, index, multiSelect);
				});
				if (index == -500)
				{
					passEntryUI.Text.text = "U";
					passwordPopup.unlockEntry = passEntryUI;
					passEntryUI.Background.color = new Color(0f, 0.66f, 0f);
					continue;
				}
				if (index == -400)
				{
					passwordPopup.resetEntry = passEntryUI;
					passEntryUI.Text.text = "R";
					passEntryUI.Background.color = Color.red;
					UnityEngine.Object.Instantiate(passwordPickerUI.SpacerEntry, passwordPickerUI.root.transform).gameObject.SetActive(value: true);
					continue;
				}
				passEntryUI.Text.text = _lock.password[i].ToString();
				passwordPopup.entries[i] = passEntryUI;
				LockType lockType = _lock.lockType;
				if (lockType == LockType.Continuous || lockType == LockType.Fixed)
				{
					passEntryUI.Text.text = "*";
					break;
				}
			}
		}
		else
		{
			UnityEngine.UI.Image image = UnityEngine.Object.Instantiate(targetModeUI.TargetPin, targetModeUI.transform);
			image.gameObject.SetActive(value: true);
			special.targetPins.Add(new TargetPin
			{
				transform = instance.transform,
				img = image
			});
		}
	}

	private void togglePasswordSelection(Lock _lock, int index, bool multiSelect = false)
	{
		PropInstance component = _lock.GetComponent<PropInstance>();
		bool flag = false;
		if (!multiSelect)
		{
			special.lockPasswordIndex = index;
			special.target = component;
		}
		else if (!special.lockPasswordIndexes.Add((component, index)))
		{
			flag = true;
			special.lockPasswordIndexes.Remove((component, index));
		}
		if (component != null && !isSpecialEffect(component))
		{
			outlineObject(_lock.gameObject, !multiSelect);
		}
		for (int i = 0; i < special.passwordPopups.Count; i++)
		{
			PasswordPopup passwordPopup = special.passwordPopups[i];
			for (int j = 0; j < passwordPopup.entries.Length; j++)
			{
				if (!multiSelect || (passwordPopup._lock == _lock && j == index))
				{
					passwordPopup.entries[j].Selected.enabled = (multiSelect ? (!flag) : (passwordPopup._lock == _lock && j == index));
				}
			}
			if (!multiSelect || (passwordPopup._lock == _lock && index == -400))
			{
				passwordPopup.resetEntry.Selected.enabled = (multiSelect ? (!flag) : (passwordPopup._lock == _lock && index == -400));
			}
			if (!multiSelect || (passwordPopup._lock == _lock && index == -500))
			{
				passwordPopup.unlockEntry.Selected.enabled = (multiSelect ? (!flag) : (passwordPopup._lock == _lock && index == -500));
			}
		}
	}

	private void hidePasswordSelection()
	{
		special.lockPasswordIndex = -1;
		special.lockPasswordIndexes = null;
		for (int i = 0; i < special.passwordPopups.Count; i++)
		{
			PasswordPopup passwordPopup = special.passwordPopups[i];
			for (int j = 0; j < passwordPopup.entries.Length; j++)
			{
				passwordPopup.entries[j].Selected.enabled = false;
			}
			passwordPopup.resetEntry.Selected.enabled = false;
			passwordPopup.unlockEntry.Selected.enabled = false;
		}
	}

	private bool isSpecialEffect(PropInstance instance)
	{
		if (!instance.TryGetComponent<PropTags>(out var component))
		{
			UnityEngine.Debug.LogError(string.Format("Prop '{0}' has no {1} component attached!", instance, "PropTags"), instance);
			return false;
		}
		return component.tags.Contains(PropTag.Effects);
	}

	private string randomString(int len)
	{
		int num = 97;
		int num2 = 25;
		string text = "";
		for (int i = 0; i < len; i++)
		{
			text += (char)UnityEngine.Random.Range(num, num + num2);
		}
		return text;
	}

	private static Texture2D copyUnreadableTexture(Texture2D texture, int width = 0, int height = 0)
	{
		int num = ((width == 0) ? texture.width : width);
		int num2 = ((height == 0) ? texture.height : height);
		RenderTexture temporary = RenderTexture.GetTemporary(num, num2, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Default);
		Graphics.Blit(texture, temporary);
		RenderTexture active = RenderTexture.active;
		RenderTexture.active = temporary;
		Texture2D texture2D = new Texture2D(num, num2);
		texture2D.ReadPixels(new Rect(0f, 0f, num, num2), 0, 0);
		texture2D.Apply();
		RenderTexture.active = active;
		RenderTexture.ReleaseTemporary(temporary);
		return texture2D;
	}

	private void refreshRightClickUI()
	{
		closeRightClickUI();
		if (raycast.instanceCount == 0)
		{
			return;
		}
		rightClickUI.root.enabled = true;
		rightClickUI.Rect.transform.position = Input.mousePosition;
		RectTransform component = rightClickUI.Rect.GetComponent<RectTransform>();
		RectTransform component2 = rightClickUI.Rect_Content_Item.GetComponent<RectTransform>();
		int num = Mathf.FloorToInt(Input.mousePosition.y / component2.sizeDelta.y);
		if (num == 0)
		{
			rightClickUI.root.enabled = false;
			return;
		}
		component.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, component2.sizeDelta.y * (float)Mathf.Min(raycast.instanceCount, num));
		for (int i = 0; i < raycast.instanceCount; i++)
		{
			PropInstance propInstance = raycast.instances[i];
			Button selectionOption = UnityEngine.Object.Instantiate(rightClickUI.Rect_Content_Item, rightClickUI.Rect_Content);
			selectionOption.gameObject.SetActive(value: true);
			addTriggerEntry(selectionOption.gameObject, EventTriggerType.PointerEnter).callback.AddListener(delegate
			{
				Color white = Color.white;
				float? a = 0.05f;
				Color color = white.With(null, null, null, a);
				selectionOption.GetComponentInChildren<UnityEngine.UI.Image>().color = color;
			});
			addTriggerEntry(selectionOption.gameObject, EventTriggerType.PointerExit).callback.AddListener(delegate
			{
				Color white = Color.white;
				float? a = 0f;
				Color color = white.With(null, null, null, a);
				selectionOption.GetComponentInChildren<UnityEngine.UI.Image>().color = color;
			});
			Text componentInChildren = selectionOption.GetComponentInChildren<Text>();
			string text = propInstance.displayName;
			if (!string.IsNullOrEmpty(text))
			{
				text += " (Display Name)".Colored(Color.dodgerBlue);
			}
			if (string.IsNullOrEmpty(text))
			{
				text = propInstance.scriptName;
				if (!string.IsNullOrEmpty(text))
				{
					text += " (Script Name)".Colored(Color.forestGreen);
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				text = propInstance.exportName;
				if (!string.IsNullOrEmpty(text))
				{
					text += " (Export Name)".Colored(Color.darkOrange);
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				text = "(No Name)".Colored(Color.darkRed);
			}
			string text2 = $"{i + 1}.".Colored(Color.gray6);
			componentInChildren.text = text2 + " " + text;
			selectionOption.onClick.AddListener(delegate
			{
				addSelection(propInstance, isCtrlPressed() || isShiftPressed(), isAltPressed());
				closeRightClickUI();
			});
			EditorButtonHover component3 = selectionOption.GetComponent<EditorButtonHover>();
			if (component3 != null)
			{
				component3.onHovering = delegate
				{
					hoverInstance = propInstance;
				};
			}
		}
	}

	private void closeRightClickUI()
	{
		rightClickUI.root.enabled = false;
		for (int num = rightClickUI.Rect_Content.childCount - 1; num >= 1; num--)
		{
			UnityEngine.Object.Destroy(rightClickUI.Rect_Content.GetChild(num).gameObject);
		}
	}

	private void closeTopUI()
	{
		if (rightClickUI.root.enabled)
		{
			closeRightClickUI();
		}
		if (isSearchUiOpen())
		{
			closeSearchUI();
		}
		if (editorUI.PrefsUI.gameObject.activeSelf)
		{
			editorUI.PrefsUI.gameObject.SetActive(value: false);
		}
	}

	private void importPropData(RoomMeta room, List<PropData> propsData, PropData root)
	{
		Dictionary<InstanceID, PropData> dictionary = new Dictionary<InstanceID, PropData>(propsData.Count);
		foreach (PropData propsDatum in propsData)
		{
			dictionary[propsDatum.ID] = propsDatum;
		}
		HashSet<InstanceID> hashSet = new HashSet<InstanceID> { root.ID };
		root.parentID = InstanceID.None;
		foreach (PropData propsDatum2 in propsData)
		{
			if (hashSet.Contains(propsDatum2.ID))
			{
				continue;
			}
			PropData propData = propsDatum2;
			while (propData.parentID != InstanceID.None)
			{
				propData = dictionary[propData.parentID];
			}
			if (propData == root)
			{
				propData = propsDatum2;
				while (propData.parentID != InstanceID.None)
				{
					hashSet.Add(propData.ID);
					propData = dictionary[propData.parentID];
				}
			}
		}
		Dictionary<string, string> fileCollisions = new Dictionary<string, string>();
		HashSet<string> copiedFilesThisRun = new HashSet<string>();
		Dictionary<InstanceID, InstanceID> dictionary2 = new Dictionary<InstanceID, InstanceID>();
		List<PropData> list = new List<PropData>(hashSet.Count);
		foreach (InstanceID item in hashSet)
		{
			PropData propData2 = dictionary[item];
			list.Add(propData2);
			dictionary2[item] = getNextInstanceID();
			if (tryGetSoundData(propData2, out var data) && !string.IsNullOrEmpty(data.path))
			{
				data.path = copyFile(data.path);
			}
			if (tryGetEditorDisplayData(propData2, out var data2) && data2.spriteSheetFileName != "default")
			{
				data2.spriteSheetFileName = copyFile(data2.spriteSheetFileName);
			}
			if (tryGetScriptComponentData(propData2, out var data3) && !string.IsNullOrEmpty(data3.scriptLocation))
			{
				string text = copyFile(data3.scriptLocation + ".lua");
				data3.scriptLocation = text.Substring(0, text.Length - 4);
			}
			if (!tryGetCustomModelData(propData2, out var data4))
			{
				continue;
			}
			foreach (string usedFile in data4.usedFiles)
			{
				string text2 = usedFile;
				if (!text2.StartsWith("_CustomModels"))
				{
					text2 = "_CustomModels/" + usedFile;
				}
				copyFile(text2, overrideName: false);
			}
		}
		InstanceID id = dictionary2[root.ID];
		patchIDs(list, dictionary2, removeMissingIDLinks: true);
		List<EditorAction> list2 = new List<EditorAction>();
		actionsLoadProps(list, list2);
		list2.Add(actionChangeSelection(newInstanceSelection(id)));
		undo.push(list2.ToArray());
		string copyFile(string fileName, bool overrideName = true)
		{
			string text3 = Path.Combine(room.loadPath, fileName);
			string text4 = Path.Combine(roomDirPath, fileName);
			if (string.IsNullOrEmpty(roomDirPath) || !Directory.Exists(roomDirPath))
			{
				UnityEngine.Debug.Log("Couldn't copy '" + orange(fileName) + "' to an unsaved room");
			}
			else if (!File.Exists(text3))
			{
				UnityEngine.Debug.Log("Source file '" + orange(fileName) + "' is missing.");
			}
			else if (File.Exists(text4))
			{
				if (!overrideName || copiedFilesThisRun.Contains(fileName))
				{
					UnityEngine.Debug.Log("File '" + orange(fileName) + "' already exists in your directory");
				}
				else if (!fileCollisions.ContainsKey(fileName))
				{
					string text5 = "png";
					string[] array = fileName.Split('.');
					if (array.Length > 1)
					{
						text5 = array[^1];
					}
					string text6;
					string path;
					do
					{
						text6 = randomString(10) + "." + text5;
						path = Path.Combine(roomDirPath, text6);
					}
					while (File.Exists(path));
					fileCollisions[fileName] = text6;
					string str = fileName;
					fileName = fileCollisions[fileName];
					text4 = Path.Combine(roomDirPath, fileName);
					File.Copy(text3, text4);
					UnityEngine.Debug.Log("Collision! Copied new replacement file '" + orange(str) + "' -> '" + orange(fileName) + "' to your directory");
				}
				else
				{
					string str2 = fileName;
					fileName = fileCollisions[fileName];
					UnityEngine.Debug.Log("Replacement file '" + orange(str2) + "' -> '" + orange(fileName) + "' already exists in your directory");
				}
			}
			else
			{
				string directoryName = Path.GetDirectoryName(text4);
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				File.Copy(text3, text4);
				UnityEngine.Debug.Log("Copied new file '" + orange(fileName) + "' to your directory");
				copiedFilesThisRun.Add(fileName);
			}
			return fileName;
		}
	}

	private static void patchIDs(IEnumerable<PropData> propsData, Dictionary<InstanceID, InstanceID> idsMap, bool removeMissingIDLinks)
	{
		foreach (PropData propsDatum in propsData)
		{
			propsDatum.ID = idsMap[propsDatum.ID];
			if (propsDatum.parentID != InstanceID.None && idsMap.TryGetValue(propsDatum.parentID, out var value))
			{
				propsDatum.parentID = value;
			}
			if (tryGetSwitch3DData(propsDatum, out var data))
			{
				patch(data.onLinks.targets, removeMissingIDLinks);
				patchLock(data.onLinks.locks, removeMissingIDLinks);
				patch(data.offLinks.targets, removeMissingIDLinks);
				patchLock(data.offLinks.locks, removeMissingIDLinks);
			}
			if (tryGetTurnableData(propsDatum, out var data2))
			{
				patchLock(data2.locks, removeMissingIDLinks);
			}
			if (tryGetDialData(propsDatum, out var data3))
			{
				patchLock(data3.locks, removeMissingIDLinks);
			}
			if (tryGetLockData(propsDatum, out var data4))
			{
				patch(data4.onLock.targets, removeMissingIDLinks);
				patchLock(data4.onLock.locks, removeMissingIDLinks);
				patch(data4.onUnlock.targets, removeMissingIDLinks);
				patchLock(data4.onUnlock.locks, removeMissingIDLinks);
			}
			if (tryGetSlotData(propsDatum, out var data5))
			{
				patch(data5.keys, removeMissingIDLinks);
				patch(data5.rejectKeys, removeMissingIDLinks);
				patchLock(data5.onPlace.locks, removeMissingIDLinks);
				patch(data5.onPlace.targets, removeMissingIDLinks);
				patch(data5.onRemove.targets, removeMissingIDLinks);
				patchLock(data5.onRemove.locks, removeMissingIDLinks);
			}
			if (tryGetTriggerData(propsDatum, out var data6))
			{
				patch(data6.keys, removeMissingIDLinks);
				patch(data6.onEnter.targets, removeMissingIDLinks);
				patchLock(data6.onEnter.locks, removeMissingIDLinks);
				patch(data6.onExit.targets, removeMissingIDLinks);
				patchLock(data6.onExit.locks, removeMissingIDLinks);
				patch(data6.onStart.targets, removeMissingIDLinks);
				patchLock(data6.onStart.locks, removeMissingIDLinks);
				patch(data6.onEnd.targets, removeMissingIDLinks);
				patchLock(data6.onEnd.locks, removeMissingIDLinks);
			}
			if (tryGetActivatorComponentData(propsDatum, out var data7))
			{
				patch(data7.keys, removeMissingIDLinks);
			}
			if (tryGetEditorDisplayData(propsDatum, out var data8))
			{
				patchSingle(ref data8.targetLock, removeMissingIDLinks);
			}
			if (tryGetSlidableData(propsDatum, out var data9))
			{
				patchLock(data9.locks, removeMissingIDLinks);
			}
			if (tryGetFogData(propsDatum, out var data10))
			{
				data10.activeOnStart = false;
			}
			if (tryGetSoundData(propsDatum, out var data11))
			{
				data11.activeOnStart = false;
			}
			if (tryGetSkyboxData(propsDatum, out var data12))
			{
				data12.activeOnStart = false;
			}
			if (tryGetCloudsData(propsDatum, out var data13))
			{
				data13.activeOnStart = false;
			}
			if (tryGetOceanData(propsDatum, out var data14))
			{
				data14.activeOnStart = false;
			}
			if (tryGetEditorPostProcessingData(propsDatum, out var data15))
			{
				data15.activeOnStart = false;
			}
			if (tryGetPuzzleData(propsDatum, out var data16))
			{
				patch(data16.conditions, removeMissingIDLinks);
			}
			if (tryGetRouletteData(propsDatum, out var data17))
			{
				patch(data17.targets, removeMissingIDLinks);
			}
			if (tryGetEditorSetupData(propsDatum, out var data18))
			{
				patch(data18.targets, removeMissingIDLinks);
			}
			if (tryGetDelayData(propsDatum, out var data19))
			{
				patch(data19.targets, removeMissingIDLinks);
			}
		}
		void patch(List<InstanceID> IDs, bool flag)
		{
			for (int num = IDs.Count - 1; num >= 0; num--)
			{
				if (idsMap.ContainsKey(IDs[num]))
				{
					IDs[num] = idsMap[IDs[num]];
				}
				else if (flag)
				{
					IDs.RemoveAt(num);
				}
			}
		}
		void patchLock(List<LockArgData> locks, bool flag)
		{
			for (int num = locks.Count - 1; num >= 0; num--)
			{
				if (idsMap.ContainsKey(locks[num].instanceID))
				{
					locks[num].instanceID = idsMap[locks[num].instanceID];
				}
				else if (flag)
				{
					locks.RemoveAt(num);
				}
			}
		}
		void patchSingle(ref InstanceID ID, bool flag)
		{
			if (idsMap.ContainsKey(ID))
			{
				ID = idsMap[ID];
			}
			else if (flag)
			{
				ID = InstanceID.None;
			}
		}
	}

	private void updateSelectionOutline()
	{
		bool flag = true;
		if (flag)
		{
			flag &= !isSelectedSingle<Polygon>() || !polygonTool.isInteractingWithVertexOrEdge(includeHover: true);
		}
		if (flag)
		{
			flag &= !assetBrowserUI.root.enabled || assetBrowserUI.data != typeof(MaterialAsset);
		}
		if (flag)
		{
			flag &= !materialEditorUI.root.enabled;
		}
		customPassAfterPostprocess.enabled = flag;
		if (!outlineContext.dirty)
		{
			return;
		}
		foreach (CustomPass customPass in customPassAfterPostprocess.customPasses)
		{
			if (!(customPass is PineCustomDrawEditorSelection pineCustomDrawEditorSelection))
			{
				continue;
			}
			pineCustomDrawEditorSelection.selectedRenderers.Clear();
			if (!isSelectedSingle<Polygon>())
			{
				foreach (GameObject item in outlineContext.outline)
				{
					pineCustomDrawEditorSelection.selectedRenderers.AddRange(getRenderersForGameObject(item));
				}
			}
			pineCustomDrawEditorSelection.specialSelectedRenderers.Clear();
			foreach (GameObject item2 in outlineContext.specialOutline)
			{
				pineCustomDrawEditorSelection.specialSelectedRenderers.AddRange(getRenderersForGameObject(item2));
			}
		}
		outlineContext.dirty = false;
		static List<Renderer> getRenderersForGameObject(GameObject go)
		{
			List<Renderer> list = new List<Renderer>();
			if (go == null)
			{
				return list;
			}
			list.AddRange(go.GetComponentsInChildren<Renderer>());
			if (go.TryGetComponent<Floor>(out var component))
			{
				HashSet<Renderer> hashSet = new HashSet<Renderer>();
				foreach (Wall wall in component.getWalls())
				{
					hashSet.UnionWith(wall.GetComponentsInChildren<Renderer>());
				}
				foreach (Floor ceiling in component.getCeilings())
				{
					hashSet.UnionWith(ceiling.GetComponentsInChildren<Renderer>());
				}
				for (int num = list.Count - 1; num >= 0; num--)
				{
					if (hashSet.Contains(list[num]))
					{
						list.RemoveAt(num);
					}
				}
			}
			list.RemoveAll((Renderer r) => r is ParticleSystemRenderer);
			return list;
		}
	}

	private void outlineObject(GameObject gameObject = null, bool clearOld = true)
	{
		if (clearOld)
		{
			outlineContext.outline.Clear();
		}
		if (gameObject != null)
		{
			outlineContext.outline.Add(gameObject);
		}
		outlineContext.dirty = true;
	}

	private void setGizmoTargets(List<PropInstance> targetInstances)
	{
		transformGizmo.ClearTargets();
		transformGizmo.pivot = ((targetInstances.Count <= 1) ? TransformPivot.Pivot : TransformPivot.Center);
		outlineContext.outline.Clear();
		cachedTransforms.Clear();
		foreach (PropInstance targetInstance in targetInstances)
		{
			transformGizmo.AddTarget(targetInstance.transform, calcPivot: false);
			if (!isSpecialEffect(targetInstance))
			{
				outlineContext.outline.Add(targetInstance.gameObject);
			}
			cachedTransforms[targetInstance.ID] = saveTransform(targetInstance.transform, isLocal: false);
		}
		outlineContext.dirty = true;
		refresh.propertiesUI = true;
	}

	private void refreshLineConnections(List<PropInstance> targetInstances)
	{
		foreach (LineConnection lineConnection in lineConnections)
		{
			UnityEngine.Object.Destroy(lineConnection.lineRenderer.gameObject);
		}
		lineConnections.Clear();
		if (targetInstances.Count != 1)
		{
			return;
		}
		PropInstance target = targetInstances[0];
		if (isPropHidden(target))
		{
			return;
		}
		if (target.TryGetComponent<Switch3D>(out var component))
		{
			drawLocksConnection(component.onLinks.locks);
			drawLocksConnection(component.offLinks.locks);
			drawGameObjectsConnections(component.onLinks.targets);
			drawGameObjectsConnections(component.offLinks.targets);
		}
		if (target.TryGetComponent<Turnable>(out var component2))
		{
			drawLocksConnection(component2.locks);
		}
		if (target.TryGetComponent<Dial>(out var component3))
		{
			drawLocksConnection(component3.locks);
		}
		if (target.TryGetComponent<Lock>(out var component4) && target.GetComponent<Teleport>() == null && target.GetComponent<Fog>() == null && target.GetComponent<Sound>() == null && target.GetComponent<EditorSkybox>() == null && target.GetComponent<EditorPostProcessing>() == null)
		{
			drawGameObjectsConnections(component4.onUnlock.targets);
			drawLocksConnection(component4.onUnlock.locks);
			drawGameObjectsConnections(component4.onLock.targets);
			drawLocksConnection(component4.onUnlock.locks);
		}
		if (target.TryGetComponent<Slot>(out var component5))
		{
			drawItemConnections(component5.acceptItems);
			drawItemConnections(component5.rejectItems);
			if (component5.insertedItem != null)
			{
				spawnLine(component5.insertedItem.gameObject, redLine);
			}
			drawLocksConnection(component5.onPlace.locks);
			drawGameObjectsConnections(component5.onPlace.targets);
			drawGameObjectsConnections(component5.onRemove.targets);
			drawLocksConnection(component5.onRemove.locks);
		}
		if (target.TryGetComponent<Lookable>(out var component6))
		{
			drawGameObjectsConnections(component6.onActivated);
			drawGameObjectsConnections(component6.onDeactivated);
		}
		if (target.TryGetComponent<Trigger>(out var component7))
		{
			drawLocksConnection(component7.enterData.locks);
			drawLocksConnection(component7.exitData.locks);
			drawGameObjectsConnections(component7.enterData.targets);
			drawGameObjectsConnections(component7.exitData.targets);
			drawLocksConnection(component7.startData.locks);
			drawLocksConnection(component7.endData.locks);
			drawGameObjectsConnections(component7.startData.targets);
			drawGameObjectsConnections(component7.endData.targets);
			drawGameObjectsConnections(component7.keys);
		}
		if (target.TryGetComponent<ActivatorComponent>(out var component8))
		{
			drawKeyTargetConnectionsGo(component8.keys);
		}
		if (target.TryGetComponent<EditorDisplay>(out var component9) && component9.targetLock != null)
		{
			drawKeyTargetConnectionsGo(new List<GameObject> { component9.targetLock.gameObject });
		}
		if (target.TryGetComponent<Slidable>(out var component10))
		{
			drawLocksConnection(component10.locks);
		}
		if (target.TryGetComponent<EditorPuzzle>(out var component11))
		{
			drawGameObjectsConnections(component11.conditions);
		}
		if (target.TryGetComponent<Roulette>(out var component12))
		{
			drawGameObjectsConnections(component12.targets);
		}
		if (target.TryGetComponent<EditorDelay>(out var component13))
		{
			drawGameObjectsConnections(component13.targets);
		}
		if (target.TryGetComponent<EditorSetup>(out var component14))
		{
			drawGameObjectsConnections(component14.targets);
		}
		if (target.TryGetComponent<Lock>(out var targetLock))
		{
			foreach (KeyValuePair<InstanceID, PropInstance> propInstance in propInstances)
			{
				PropInstance value = propInstance.Value;
				if (!(value.ID == target.ID) && ((value.TryGetComponent<Switch3D>(out var component15) && component15.onLinks.locks.Exists(findPredicate)) || (value.TryGetComponent<Turnable>(out var component16) && component16.locks.Exists(findPredicate)) || (value.TryGetComponent<Dial>(out var component17) && component17.locks.Exists(findPredicate)) || (value.TryGetComponent<Lock>(out var component18) && (component18.onUnlock.locks.Exists(findPredicate) || component18.onLock.locks.Exists(findPredicate))) || (value.TryGetComponent<Slot>(out var component19) && (component19.onPlace.locks.Exists(findPredicate) || component19.onRemove.locks.Exists(findPredicate))) || (value.TryGetComponent<Trigger>(out var component20) && (component20.enterData.locks.Exists(findPredicate) || component20.exitData.locks.Exists(findPredicate) || component20.startData.locks.Exists(findPredicate) || component20.endData.locks.Exists(findPredicate))) || (value.TryGetComponent<Slidable>(out var component21) && component21.locks.Exists(findPredicate))))
				{
					spawnLine(value.gameObject, blueLine);
				}
			}
		}
		foreach (KeyValuePair<InstanceID, PropInstance> propInstance2 in propInstances)
		{
			PropInstance value2 = propInstance2.Value;
			if (!(value2.ID == target.ID) && ((value2.TryGetComponent<Switch3D>(out var component22) && component22.onLinks.targets.Exists(findPredicate2)) || (value2.TryGetComponent<Lock>(out var component23) && (component23.onUnlock.targets.Exists(findPredicate2) || component23.onLock.targets.Exists(findPredicate2))) || (value2.TryGetComponent<Slot>(out var component24) && referencedBySlot(component24)) || (value2.TryGetComponent<Trigger>(out var component25) && referencedByTrigger(component25)) || (value2.TryGetComponent<EditorPuzzle>(out var component26) && component26.conditions.Exists(findPredicate2)) || (value2.TryGetComponent<Roulette>(out var component27) && component27.targets.Exists(findPredicate2)) || (value2.TryGetComponent<EditorSetup>(out var component28) && component28.targets.Exists(findPredicate2)) || (value2.TryGetComponent<ActivatorComponent>(out var component29) && component29.keys.Exists(findPredicate2)) || (value2.TryGetComponent<Lookable>(out var component30) && (component30.onActivated.Exists(findPredicate2) || component30.onDeactivated.Exists(findPredicate2)))))
			{
				spawnLine(value2.gameObject, blueLine);
			}
		}
		void drawGameObjectsConnections(IEnumerable<GameObject> targets)
		{
			foreach (GameObject target2 in targets)
			{
				spawnLine(target2.gameObject, redLine);
			}
		}
		void drawItemConnections(Item[] targets)
		{
			foreach (Item item in targets)
			{
				if (!(item == null))
				{
					spawnLine(item.gameObject, redLine);
				}
			}
		}
		void drawKeyTargetConnectionsGo(List<GameObject> targets)
		{
			foreach (GameObject target3 in targets)
			{
				spawnLine(target3.gameObject, redLine);
			}
		}
		void drawLocksConnection(List<LockArgument> locks)
		{
			foreach (LockArgument item2 in safeLocks(locks))
			{
				spawnLine(item2.targetLock.gameObject, redLine);
			}
		}
		bool findPredicate(LockArgument x)
		{
			return x.targetLock == targetLock;
		}
		bool findPredicate2(GameObject go)
		{
			return go == target.gameObject;
		}
		bool findPredicateItem(Item item)
		{
			if (item != null)
			{
				return item.gameObject == target.gameObject;
			}
			return false;
		}
		bool referencedBySlot(Slot s)
		{
			if (!s.onPlace.targets.Exists(findPredicate2) && !s.onRemove.targets.Exists(findPredicate2) && !Array.Exists(s.acceptItems, findPredicateItem))
			{
				return Array.Exists(s.rejectItems, findPredicateItem);
			}
			return true;
		}
		bool referencedByTrigger(Trigger t)
		{
			if (!t.enterData.targets.Exists(findPredicate2) && !t.exitData.targets.Exists(findPredicate2) && !t.startData.targets.Exists(findPredicate2) && !t.endData.targets.Exists(findPredicate2))
			{
				return t.keys.Exists(findPredicate2);
			}
			return true;
		}
		void spawnLine(GameObject second, LineRenderer prefab)
		{
			LineRenderer lineRenderer = UnityEngine.Object.Instantiate(prefab);
			lineRenderer.gameObject.SetActive(value: true);
			lineRenderer.SetPositions(new Vector3[2]
			{
				target.transform.position,
				second.transform.position
			});
			lineConnections.Add(new LineConnection(lineRenderer, target.gameObject, second));
		}
	}

	private void updateLineConnections()
	{
		for (int i = 0; i < lineConnections.Count; i++)
		{
			LineConnection lineConnection = lineConnections[i];
			if (lineConnection.first == null || lineConnection.second == null)
			{
				UnityEngine.Object.Destroy(lineConnection.lineRenderer.gameObject);
				lineConnections.RemoveAt(i);
				i--;
			}
			else
			{
				lineConnection.lineRenderer.SetPositions(new Vector3[2]
				{
					lineConnection.first.transform.position,
					lineConnection.second.transform.position
				});
			}
		}
	}

	private void createSlotPreview(PropInstance instance)
	{
		if (!(instance == null) && instance.TryGetComponent<Slot>(out var slot))
		{
			if (slot.acceptItems.Length != 0 && slot.acceptItems[0] != null)
			{
				createKeyPreviews(slot.acceptItems);
			}
			else if (slot.rejectItems.Length != 0 && slot.rejectItems[0] != null)
			{
				createKeyPreviews(slot.rejectItems);
			}
		}
		void createKeyPreviews(Item[] keys)
		{
			Transform transform = UnityEngine.Object.Instantiate(keys[0].transform, slot.transform, worldPositionStays: true);
			ImpostorClone.stripNonVisualComponents(transform.gameObject, ImpostorClone.StrippingFlags.AllowLights);
			applyTransform(transform.transform, saveTransform(slot.pivot, isLocal: false), ignoreScale: true);
			setGhostMaterial(transform.gameObject);
			transform.gameObject.AddComponent<EditorPreviewCloneTag>();
		}
	}

	private void destroySlotPreview(PropInstance instance)
	{
		if (instance == null || !instance.TryGetComponent<Slot>(out var component))
		{
			return;
		}
		for (int num = component.transform.childCount - 1; num >= 0; num--)
		{
			Transform child = component.transform.GetChild(num);
			if (!(child.GetComponent<EditorPreviewCloneTag>() == null))
			{
				UnityEngine.Object.DestroyImmediate(child.gameObject);
			}
		}
	}

	private void createSlidablePreview(PropInstance instance, bool specialMode = false)
	{
		if (!instance.TryGetComponent<Slidable>(out var slidable))
		{
			return;
		}
		if (slidable.snapPointIndex != 0 && !specialMode)
		{
			createPreview(UnityEngine.Object.Instantiate(slidable.gameObject), slidable.startPoint + slidable.segment * slidable.snapPointIndex);
		}
		for (int i = 1; i < slidable.additionalSnapPointCount + 2; i++)
		{
			if (slidable.snapPointIndex != i || specialMode)
			{
				GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				gameObject.transform.localScale = Vector3.one * 0.04f;
				createPreview(gameObject, slidable.startPoint + slidable.segment * i);
			}
		}
		spawnPreviewLine(slidable.startNode.transform.position, slidable.endNode.transform.position, blueLine, slidable.transform);
		void createPreview(GameObject keyPreview, Vector3 targetPosition)
		{
			if (keyPreview != null)
			{
				ImpostorClone.stripNonVisualComponents(keyPreview, ImpostorClone.StrippingFlags.AllowLights);
				keyPreview.transform.parent = slidable.transform;
				keyPreview.transform.position = targetPosition;
				setGhostMaterial(keyPreview);
				keyPreview.AddComponent<EditorPreviewCloneTag>();
			}
		}
		static void spawnPreviewLine(Vector3 start, Vector3 end, LineRenderer prefab, Transform parent)
		{
			LineRenderer lineRenderer = UnityEngine.Object.Instantiate(prefab);
			lineRenderer.gameObject.SetActive(value: true);
			lineRenderer.SetPositions(new Vector3[2] { start, end });
			lineRenderer.transform.parent = parent;
			lineRenderer.gameObject.AddComponent<EditorPreviewCloneTag>();
			lineRenderer.tag = "NoOutline";
		}
	}

	private void destroySlidablePreview(PropInstance instance)
	{
		if (!instance.TryGetComponent<Slidable>(out var component))
		{
			return;
		}
		for (int num = component.transform.childCount - 1; num >= 0; num--)
		{
			Transform child = component.transform.GetChild(num);
			if (child.TryGetComponent<EditorPreviewCloneTag>(out var _))
			{
				UnityEngine.Object.DestroyImmediate(child.gameObject);
			}
		}
	}

	private bool interactionInProgress()
	{
		if (!polygonTool.isInteractingWithVertexOrEdge(includeHover: false) && cameraTransition == null && cameraMouseControl == CamMouseControl.None && !transformGizmo.isTransforming && targetTransformTweens.Count <= 0 && draggedProperty == null && !editorUI.PropsHeader_Filter_Input.isFocused && !inputFieldsNavigationDataProperties.isInputFieldFocused && !inputFieldNavigationDataBuilding.isInputFieldFocused && !inputFieldNavigationDataSpecial.isInputFieldFocused && !inputFieldsNavigationDataMaterials.isInputFieldFocused && !inputFieldsNavigationDataColorPicker.isInputFieldFocused && !colorPickerUI.gameObject.activeSelf && !isPropertyDropdownInUse())
		{
			return rectSelection.phase == RSP.Active;
		}
		return true;
	}

	private void cancelInteraction()
	{
		if (transformGizmo.isTransforming && (special == null || special.mode != EditorMode.Pivot))
		{
			foreach (InstanceID propInstanceID in selection.propInstanceIDs)
			{
				PropInstance instanceByID = getInstanceByID(propInstanceID);
				TransformData transformData = cachedTransforms[instanceByID.ID];
				applyTransform(instanceByID.transform, transformData);
			}
			transformGizmo.EndDragging();
			refresh.transforms = true;
			UnityEngine.Debug.Log("cancelInteraction " + orange("transformGizmo.isTransforming"));
		}
		else
		{
			if (targetTransformTweens.Count > 0)
			{
				return;
			}
			if (draggedProperty != null)
			{
				draggedProperty.dragData.pointerDrag = null;
				draggedProperty.onCancel();
				draggedProperty = null;
				UnityEngine.Debug.Log("cancelInteraction " + orange("draggedProperty"));
			}
			else if (inputFieldsNavigationDataProperties.isInputFieldFocused)
			{
				UnityEngine.Debug.Log("cancelInteraction " + orange("isInputFieldFocused"));
				InputFieldNavigationData.currentFocusedInputFieldName = "";
				if (isSearchUiOpen())
				{
					closeSearchUI();
				}
			}
			else if (inputFieldNavigationDataBuilding.isInputFieldFocused)
			{
				UnityEngine.Debug.Log("cancelInteraction " + orange("isInputFieldFocused"));
				InputFieldNavigationData.currentFocusedInputFieldName = "";
				if (isSearchUiOpen())
				{
					closeSearchUI();
				}
			}
			else if (inputFieldNavigationDataSpecial.isInputFieldFocused)
			{
				UnityEngine.Debug.Log("cancelInteraction " + orange("isInputFieldFocused"));
				InputFieldNavigationData.currentFocusedInputFieldName = "";
				if (isSearchUiOpen())
				{
					closeSearchUI();
				}
			}
			else if (inputFieldsNavigationDataMaterials.isInputFieldFocused)
			{
				UnityEngine.Debug.Log("cancelInteraction " + orange("isInputFieldFocused"));
				InputFieldNavigationData.currentFocusedInputFieldName = "";
				if (isSearchUiOpen())
				{
					closeSearchUI();
				}
			}
			else if (inputFieldsNavigationDataColorPicker.isInputFieldFocused)
			{
				UnityEngine.Debug.Log("cancelInteraction " + orange("isInputFieldFocused"));
				InputFieldNavigationData.currentFocusedInputFieldName = "";
				if (isSearchUiOpen())
				{
					closeSearchUI();
				}
			}
			else if (isPropertyDropdownInUse())
			{
				UnityEngine.Debug.Log("cancelInteraction " + orange("Property Dropdown"));
			}
			else if (colorPickerUI.gameObject.activeSelf)
			{
				colorPickerProperty.onCancel();
				UnityEngine.Debug.Log("cancelInteraction " + orange("Property ColorPicker"));
			}
			else if (rectSelection.phase == RSP.Active)
			{
				endRectSelection(apply: false);
				rectSelectionCanceled = true;
				UnityEngine.Debug.Log("cancelInteraction " + orange("RectSelection"));
			}
		}
	}

	private bool isPropertyDropdownInUse()
	{
		foreach (PropertyData property in properties)
		{
			if (property.ui is EnumPropertyUI enumPropertyUI && enumPropertyUI.Dropdown.transform.Find("Dropdown List") != null)
			{
				return true;
			}
		}
		return false;
	}

	private static TransformData saveTransform(Transform t, bool isLocal)
	{
		TransformData transformData = new TransformData
		{
			isLocal = isLocal
		};
		if (isLocal)
		{
			transformData.position = t.localPosition;
			transformData.rotation = t.localRotation.eulerAngles;
			transformData.scale = t.localScale;
		}
		else
		{
			transformData.position = t.position;
			transformData.rotation = t.rotation.eulerAngles;
			transformData.scale = t.lossyScale;
		}
		return transformData;
	}

	private static void applyTransform(Transform t, TransformData transformData, bool ignoreScale = false)
	{
		if (transformData.isLocal)
		{
			t.localPosition = transformData.position;
			t.localRotation = Quaternion.Euler(transformData.rotation);
			if (!ignoreScale)
			{
				t.localScale = transformData.scale;
			}
		}
		else
		{
			t.position = transformData.position;
			t.rotation = Quaternion.Euler(transformData.rotation);
			if (!ignoreScale)
			{
				applyGlobalScale(t, transformData.scale);
			}
		}
		static void applyGlobalScale(Transform transform, Vector3 scale)
		{
			Transform parent = transform.parent;
			transform.parent = null;
			transform.localScale = scale;
			transform.parent = parent;
		}
	}

	private InstanceID getNextInstanceID()
	{
		return new InstanceID
		{
			value = instanceIDCounter++
		};
	}

	private static PropInstance getInstanceByID(Dictionary<InstanceID, PropInstance> propInstances, InstanceID ID)
	{
		propInstances.TryGetValue(ID, out var value);
		return value;
	}

	private PropInstance getInstanceByID(InstanceID ID)
	{
		propInstances.TryGetValue(ID, out var value);
		return value;
	}

	private static InstanceID getParentID(PropInstance instance)
	{
		PropInstance parentInstance = getParentInstance(instance);
		if (!(parentInstance != null))
		{
			return InstanceID.None;
		}
		return parentInstance.ID;
	}

	private static PropInstance getParentInstance(PropInstance instance)
	{
		Transform parent = instance.transform.parent;
		if (!(parent == null))
		{
			return parent.GetComponent<PropInstance>();
		}
		return null;
	}

	private void actionsRemoveFromTargeters(List<EditorAction> actions, PropInstance target, int targetIndex = -1, bool targetIndexReduce = false, bool deletingTarget = false)
	{
		foreach (KeyValuePair<InstanceID, PropInstance> propInstance in propInstances)
		{
			PropInstance value = propInstance.Value;
			if (value.ID == target.ID)
			{
				continue;
			}
			if (value.TryGetComponent<Dial>(out var component))
			{
				var (prev, next) = prepChangeData(value);
				if (component.locks.Exists(findPredicateLock))
				{
					actionRemoveLocksIDs(prev, next, (PropData propData) => getDialData(propData).locks, "Dial remove lock");
				}
			}
			if (value.TryGetComponent<Lock>(out var component2))
			{
				var (prev2, next2) = prepChangeData(value);
				if (component2.onLock.locks.Exists(findPredicateLock))
				{
					actionRemoveLocksIDs(prev2, next2, (PropData propData) => getLockData(propData).onLock.locks, "Lock remove lock onLock");
				}
				if (component2.onLock.targets.Exists(findPredicate))
				{
					actionRemoveIDs(prev2, next2, (PropData propData) => getLockData(propData).onLock.targets, "Lock remove target onLock");
				}
				if (component2.onUnlock.locks.Exists(findPredicateLock))
				{
					actionRemoveLocksIDs(prev2, next2, (PropData propData) => getLockData(propData).onUnlock.locks, "Lock remove lock onUnlock");
				}
				if (component2.onUnlock.targets.Exists(findPredicate))
				{
					actionRemoveIDs(prev2, next2, (PropData propData) => getLockData(propData).onUnlock.targets, "Lock remove target onUnlock");
				}
			}
			if (value.TryGetComponent<Slidable>(out var component3))
			{
				var (prev3, next3) = prepChangeData(value);
				if (component3.locks.Exists(findPredicateLock))
				{
					actionRemoveLocksIDs(prev3, next3, (PropData propData) => getSlidableData(propData).locks, "Slidable remove lock");
				}
			}
			if (value.TryGetComponent<Slot>(out var component4))
			{
				var (prev4, next4) = prepChangeData(value);
				if (Array.Exists(component4.acceptItems, findPredicateItem))
				{
					actionRemoveIDs(prev4, next4, (PropData propData) => getSlotData(propData).keys, "Slot remove key");
				}
				if (Array.Exists(component4.rejectItems, findPredicateItem))
				{
					actionRemoveIDs(prev4, next4, (PropData propData) => getSlotData(propData).rejectKeys, "Slot remove reject key");
				}
				if (component4.onPlace.locks.Exists(findPredicateLock))
				{
					actionRemoveLocksIDs(prev4, next4, (PropData propData) => getSlotData(propData).onPlace.locks, "Slot remove lock onPlace");
				}
				if (component4.onPlace.targets.Exists(findPredicate))
				{
					actionRemoveIDs(prev4, next4, (PropData propData) => getSlotData(propData).onPlace.targets, "Slot remove target onPlace");
				}
				if (component4.onRemove.locks.Exists(findPredicateLock))
				{
					actionRemoveLocksIDs(prev4, next4, (PropData propData) => getSlotData(propData).onRemove.locks, "Slot remove lock onRemove");
				}
				if (component4.onRemove.targets.Exists(findPredicate))
				{
					actionRemoveIDs(prev4, next4, (PropData propData) => getSlotData(propData).onRemove.targets, "Slot remove target onRemove");
				}
			}
			if (value.TryGetComponent<Switch3D>(out var component5))
			{
				var (prev5, next5) = prepChangeData(value);
				if (component5.onLinks.locks.Exists(findPredicateLock))
				{
					actionRemoveLocksIDs(prev5, next5, (PropData propData) => getSwitch3DData(propData).onLinks.locks, "Switch remove lock onLinks");
				}
				if (component5.onLinks.targets.Exists(findPredicate))
				{
					actionRemoveIDs(prev5, next5, (PropData propData) => getSwitch3DData(propData).onLinks.targets, "Switch remove target onLinks");
				}
				if (component5.offLinks.locks.Exists(findPredicateLock))
				{
					actionRemoveLocksIDs(prev5, next5, (PropData propData) => getSwitch3DData(propData).offLinks.locks, "Switch remove lock offLinks");
				}
				if (component5.offLinks.targets.Exists(findPredicate))
				{
					actionRemoveIDs(prev5, next5, (PropData propData) => getSwitch3DData(propData).offLinks.targets, "Switch remove target offLinks");
				}
			}
			if (value.TryGetComponent<Trigger>(out var component6))
			{
				var (prev6, next6) = prepChangeData(value);
				if (deletingTarget && component6.keys.Exists(findPredicate))
				{
					actionRemoveIDs(prev6, next6, (PropData propData) => getTriggerData(propData).keys, "Trigger enter remove key");
				}
				if (component6.enterData.locks.Exists(findPredicateLock))
				{
					actionRemoveLocksIDs(prev6, next6, (PropData propData) => getTriggerData(propData).onEnter.locks, "Trigger remove lock onEnter");
				}
				if (component6.enterData.targets.Exists(findPredicate))
				{
					actionRemoveIDs(prev6, next6, (PropData propData) => getTriggerData(propData).onEnter.targets, "Trigger remove target onEnter");
				}
				if (component6.exitData.locks.Exists(findPredicateLock))
				{
					actionRemoveLocksIDs(prev6, next6, (PropData propData) => getTriggerData(propData).onExit.locks, "Trigger remove lock onExit");
				}
				if (component6.exitData.targets.Exists(findPredicate))
				{
					actionRemoveIDs(prev6, next6, (PropData propData) => getTriggerData(propData).onExit.targets, "Trigger remove target onExit");
				}
				if (component6.startData.locks.Exists(findPredicateLock))
				{
					actionRemoveLocksIDs(prev6, next6, (PropData propData) => getTriggerData(propData).onStart.locks, "Trigger remove lock onStart");
				}
				if (component6.startData.targets.Exists(findPredicate))
				{
					actionRemoveIDs(prev6, next6, (PropData propData) => getTriggerData(propData).onStart.targets, "Trigger remove target onStart");
				}
				if (component6.endData.locks.Exists(findPredicateLock))
				{
					actionRemoveLocksIDs(prev6, next6, (PropData propData) => getTriggerData(propData).onEnd.locks, "Trigger remove lock onEnd");
				}
				if (component6.endData.targets.Exists(findPredicate))
				{
					actionRemoveIDs(prev6, next6, (PropData propData) => getTriggerData(propData).onEnd.targets, "Trigger remove target onEnd");
				}
			}
			if (value.TryGetComponent<Turnable>(out var component7))
			{
				var (prev7, next7) = prepChangeData(value);
				if (component7.locks.Exists(findPredicateLock))
				{
					actionRemoveLocksIDs(prev7, next7, (PropData propData) => getTurnableData(propData).locks, "Turnable remove lock");
				}
			}
			if (deletingTarget && value.TryGetComponent<ActivatorComponent>(out var component8))
			{
				var (prev8, next8) = prepChangeData(value);
				if (component8.keys.Exists(findPredicate))
				{
					actionRemoveIDs(prev8, next8, (PropData propData) => getActivatorComponentData(propData).keys, "Activator Component remove key");
				}
			}
			if (deletingTarget && value.TryGetComponent<EditorPuzzle>(out var component9))
			{
				var (prev9, next9) = prepChangeData(value);
				if (component9.conditions.Exists(findPredicate))
				{
					actionRemoveIDs(prev9, next9, (PropData propData) => getPuzzleData(propData).conditions, "Puzzle remove key");
				}
			}
			if (deletingTarget && value.TryGetComponent<Roulette>(out var component10))
			{
				var (prev10, next10) = prepChangeData(value);
				if (component10.targets.Exists(findPredicate))
				{
					actionRemoveIDs(prev10, next10, (PropData propData) => getRouletteData(propData).targets, "Roulette remove key");
				}
			}
			if (!deletingTarget || !value.TryGetComponent<EditorDelay>(out var component11))
			{
				continue;
			}
			var (prev11, next11) = prepChangeData(value);
			if (component11.targets.Exists(findPredicate))
			{
				actionRemoveIDs(prev11, next11, (PropData propData) => getDelayData(propData).targets, "Delay remove key");
			}
		}
		void actionRemoveIDs(PropData prev12, PropData propData, Func<PropData, List<InstanceID>> getIDs, string actionChangeLabel)
		{
			getIDs(propData).Remove(target.ID);
			actions.Add(actionChangeProp(prev12, propData, actionChangeLabel));
		}
		void actionRemoveLocksIDs(PropData prev12, PropData propData, Func<PropData, List<LockArgData>> getIDs, string actionChangeLabel)
		{
			getIDs(propData).RemoveAll((LockArgData x) => x.instanceID == target.ID);
			actions.Add(actionChangeProp(prev12, propData, actionChangeLabel));
		}
		bool findPredicate(GameObject x)
		{
			return x == target.gameObject;
		}
		bool findPredicateItem(Item x)
		{
			return x.gameObject == target.gameObject;
		}
		bool findPredicateLock(LockArgument lockArg)
		{
			return lockArg.targetLock.gameObject == target.gameObject;
		}
	}

	private static PropInstance newPropInstance(PropID propID, InstanceID id, EditorContext context)
	{
		GameObject gameObject = loadPrefab(context, propID);
		if (gameObject == null)
		{
			UnityEngine.Debug.LogError("Error loading prefab " + propID.value);
			return null;
		}
		GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, context.container.transform);
		gameObject2.name = propID.value;
		if (isPropCustom(propID))
		{
			gameObject2.SetActive(value: true);
		}
		PropInstance component = gameObject2.GetComponent<PropInstance>();
		component.ID = id;
		component.parent = null;
		component.exportName = "";
		component.isExpandedInHierarchy = true;
		context.propInstances[component.ID] = component;
		return component;
	}

	private void discardRoom()
	{
		cancelSelection();
		exitPolygonTool();
		undo.reset();
		foreach (KeyValuePair<InstanceID, PropInstance> propInstance in propInstances)
		{
			propInstance.Deconstruct(out var _, out var value);
			UnityEngine.Object.Destroy(value.gameObject);
		}
		propInstances.Clear();
		outlineContext.outline.Clear();
		outlineContext.specialOutline.Clear();
		Resources.UnloadUnusedAssets();
	}

	private static int getAppVersion()
	{
		int.TryParse(Application.version, out var result);
		return result;
	}

	private List<PropInstance> getChildPropInstances(PropInstance instance, bool includeSelf = true, bool recursive = true, HashSet<InstanceID> ignoreInstances = null)
	{
		int index = instanceCacheAt++ % propInstanceChildrenCache.Count;
		List<PropInstance> list = propInstanceChildrenCache[index];
		list.Clear();
		getChildPropInstances(instance, list, includeSelf, recursive, ignoreInstances);
		return list;
	}

	private void getChildPropInstances(PropInstance instance, ICollection<PropInstance> result, bool includeSelf = true, bool recursive = true, HashSet<InstanceID> ignoreInstances = null)
	{
		iterate(instance.transform, root: true, ignoreInstances);
		void iterate(Transform transform, bool root, HashSet<InstanceID> hashSet = null)
		{
			bool flag = false;
			if ((!root || includeSelf) && transform.TryGetComponent<PropInstance>(out var component))
			{
				if (!root && hashSet != null && hashSet.TryGetValue(component.ID, out var _))
				{
					flag = true;
				}
				if (root || !flag)
				{
					result.Add(component);
				}
			}
			if (!flag && (root || recursive))
			{
				for (int i = 0; i < transform.childCount; i++)
				{
					iterate(transform.GetChild(i), root: false, hashSet);
				}
			}
		}
	}

	private static bool containsParentPropInstance(PropInstance targetChild, HashSet<InstanceID> possibleParents)
	{
		if (iterate(targetChild.transform.parent, possibleParents))
		{
			return true;
		}
		return false;
		static bool iterate(Transform transform, HashSet<InstanceID> hashSet)
		{
			if (transform == null)
			{
				return false;
			}
			if (transform.TryGetComponent<PropInstance>(out var component) && hashSet.TryGetValue(component.ID, out var _))
			{
				return true;
			}
			return iterate(transform.parent, hashSet);
		}
	}

	private static PlayerSave.Settings getProperSettings()
	{
		return PlayerSave.getSettings();
	}

	public static void addNavMeshObstacles(PropInstance instance)
	{
		foreach (Collider item in instance.GetComponentsInInstance<Collider>())
		{
			NavMeshObstacle orAddComponent = item.gameObject.GetOrAddComponent<NavMeshObstacle>();
			orAddComponent.carving = true;
			orAddComponent.carveOnlyStationary = true;
		}
	}

	public static void removeNavMeshObstacles(PropInstance instance)
	{
		foreach (NavMeshObstacle item in instance.GetComponentsInInstance<NavMeshObstacle>())
		{
			UnityEngine.Object.DestroyImmediate(item);
		}
	}

	private void initAssetBrowser()
	{
		assetBrowserUI.Background.onClick.RemoveAllListeners();
		assetBrowserUI.Background.onClick.AddListener(delegate
		{
			if (FileBrowser.IsOpen)
			{
				FileBrowser.HideDialog();
			}
			else
			{
				closeAssetBrowser(assetBrowserUI);
			}
		});
	}

	private void onAssetBrowserButtonClick(Button button)
	{
		if (button == assetBrowserUI.Header_Quit)
		{
			if (FileBrowser.IsOpen)
			{
				FileBrowser.HideDialog();
			}
			closeAssetBrowser(assetBrowserUI);
		}
		if (button == assetBrowserUI.Header_Folder || button == editorUI.EditorAssetBrowserUITABS.Header_Folder)
		{
			openFileBrowserForType((Type)assetBrowserUI.data);
		}
	}

	private void openFileBrowserForType(Type currentUserAssetType)
	{
		FileBrowser.Filter filter = new FileBrowser.Filter("All", supportedAssetFormats.ToArray());
		FileBrowser.Filter filter2 = new FileBrowser.Filter("Texture", supportedTextureAssetFormats.ToArray());
		FileBrowser.Filter filter3 = new FileBrowser.Filter("Material", supportedMaterialAssetFormats.ToArray());
		FileBrowser.Filter filter4 = new FileBrowser.Filter("Audio", supportedAudioAssetFormats.ToArray());
		FileBrowser.Filter filter5 = new FileBrowser.Filter("Script", supportedScriptAssetFormats.ToArray());
		FileBrowser.Filter filter6 = new FileBrowser.Filter("Model", supportedModelAssetFormats.ToArray());
		if (currentUserAssetType == typeof(TextureAsset))
		{
			FileBrowser.SetFilters(true, filter2);
			FileBrowser.SetDefaultFilter(supportedTextureAssetFormats[0]);
		}
		else if (currentUserAssetType == typeof(MaterialAsset))
		{
			FileBrowser.SetFilters(true, filter3);
			FileBrowser.SetDefaultFilter(supportedMaterialAssetFormats[0]);
		}
		else if (currentUserAssetType == typeof(AudioAsset))
		{
			FileBrowser.SetFilters(true, filter4);
			FileBrowser.SetDefaultFilter(supportedAudioAssetFormats[0]);
		}
		else if (currentUserAssetType == typeof(ScriptAsset))
		{
			FileBrowser.SetFilters(true, filter5);
			FileBrowser.SetDefaultFilter(supportedScriptAssetFormats[0]);
		}
		else if (currentUserAssetType == typeof(ModelAsset))
		{
			FileBrowser.SetFilters(true, filter6);
			FileBrowser.SetDefaultFilter(supportedModelAssetFormats[0]);
		}
		else if (currentUserAssetType == typeof(UserAsset))
		{
			FileBrowser.SetFilters(true, filter, filter2, filter3, filter4, filter5, filter6);
			FileBrowser.SetDefaultFilter(supportedAssetFormats[0]);
		}
		FileBrowser.Skin = assetBrowserSkin;
		FileBrowser.ShowLoadDialog(onFileBrowserSuccess, onFileBrowserCancel, FileBrowser.PickMode.Files, allowMultiSelection: false, null, null, "Import", "Import");
	}

	private void openAssetBrowserPopup<T>(Action<UserAsset> onItemClick) where T : UserAsset
	{
		closeAssetBrowser(assetBrowserUI);
		assetBrowserUI.root.enabled = true;
		Action<Type> onNewAssetPressed = delegate(Type type)
		{
			onAddNewAssetPressed(type, isAssetPicker: true);
		};
		openAssetBrowserBase<T>(onItemClick, onNewAssetPressed, assetBrowserUI, assetBrowserUI.Header_Search_Input.text);
		assetBrowserUI.Header_AssetTypeTabs.gameObject.SetActive(typeof(T) == typeof(UserAsset));
		assetBrowserUI.Header_Title.text = getAssetTypeName<T>() + " Picker";
		assetBrowserUI.Header_Search_Input.onValueChanged.RemoveAllListeners();
		assetBrowserUI.Header_Search_Input.onEndEdit.RemoveAllListeners();
		string text = assetBrowserUI.Header_Search_Input.text;
		assetBrowserUI.Header_Search_Input.text = text + "r";
		assetBrowserUI.Header_Search_Input.ActivateInputField();
		assetBrowserUI.Header_Search_Input.text = text;
		assetBrowserUI.Header_Search_Input.onValueChanged.AddListener(delegate
		{
			refreshAssetBrowser<T>(onItemClick, onNewAssetPressed, assetBrowserUI, assetBrowserUI.Header_Search_Input.text);
		});
		assetBrowserUI.Header_Search_Input.onEndEdit.AddListener(delegate
		{
			refreshAssetBrowser<T>(onItemClick, onNewAssetPressed, assetBrowserUI, assetBrowserUI.Header_Search_Input.text);
		});
	}

	private void openAssetBrowserBase<T>(Action<UserAsset> onItemClick, Action<Type> onAddNewAsset, EditorAssetBrowserUI assetBrowser, string searchFilter) where T : UserAsset
	{
		assetBrowser.root.enabled = true;
		assetBrowser.data = typeof(T);
		if (typeof(T) == typeof(UserAsset))
		{
			assetBrowser.Header_AssetTypeTabs.gameObject.SetActive(value: true);
			foreach (Transform item2 in assetBrowser.Header_AssetTypeTabs.transform)
			{
				if (!(item2.gameObject == assetBrowser.Header_AssetTypeTabs_AssetBrowserTabItem.gameObject) && !(item2.gameObject == assetBrowser.Header_AssetTypeTabs_Line.gameObject))
				{
					UnityEngine.Object.Destroy(item2.gameObject);
				}
			}
			string[] enumNames = typeof(UserAssetDropdownType).GetEnumNames();
			List<AssetBrowserTabItemUI> tabItems = new List<AssetBrowserTabItemUI>();
			for (int i = 0; i < enumNames.Length; i++)
			{
				string text = enumNames[i];
				if (text == "Models")
				{
					continue;
				}
				AssetBrowserTabItemUI item = UnityEngine.Object.Instantiate(assetBrowser.Header_AssetTypeTabs_AssetBrowserTabItem, assetBrowser.Header_AssetTypeTabs.transform);
				tabItems.Add(item);
				item.Icon.sprite = userAssetDropdownTypeIcons[i];
				item.Selected.gameObject.SetActive(text == "All");
				item.gameObject.SetActive(value: true);
				item.root.onClick.RemoveAllListeners();
				int local_i = i;
				item.root.onClick.AddListener(delegate
				{
					foreach (AssetBrowserTabItemUI item3 in tabItems)
					{
						item3.Selected.gameObject.SetActive(value: false);
					}
					item.Selected.gameObject.SetActive(value: true);
					switch ((UserAssetDropdownType)local_i)
					{
					case UserAssetDropdownType.All:
						refreshAssetBrowser<UserAsset>(onItemClick, onAddNewAsset, assetBrowser, searchFilter, typeof(ModelAsset));
						break;
					case UserAssetDropdownType.Textures:
						refreshAssetBrowser<TextureAsset>(onItemClick, onAddNewAsset, assetBrowser, searchFilter);
						break;
					case UserAssetDropdownType.Materials:
						refreshAssetBrowser<MaterialAsset>(onItemClick, onAddNewAsset, assetBrowser, searchFilter);
						break;
					case UserAssetDropdownType.Audio:
						refreshAssetBrowser<AudioAsset>(onItemClick, onAddNewAsset, assetBrowser, searchFilter);
						break;
					case UserAssetDropdownType.Scripts:
						refreshAssetBrowser<ScriptAsset>(onItemClick, onAddNewAsset, assetBrowser, searchFilter);
						break;
					case UserAssetDropdownType.Models:
						refreshAssetBrowser<ModelAsset>(onItemClick, onAddNewAsset, assetBrowser, searchFilter);
						break;
					}
				});
			}
		}
		if (typeof(T) == typeof(UserAsset))
		{
			populateAssetBrowser<T>(onItemClick, onAddNewAsset, assetBrowser, searchFilter, typeof(ModelAsset));
		}
		else
		{
			populateAssetBrowser<T>(onItemClick, onAddNewAsset, assetBrowser, searchFilter);
		}
	}

	private void populateAssetBrowser<T>(Action<UserAsset> onItemClick, Action<Type> onAddNewAsset, EditorAssetBrowserUI assetBrowser, string filter, Type assetTypeToIgnore = null) where T : UserAsset
	{
		List<UserAsset> list = findUserAssets<T>(filter, assetTypeToIgnore);
		assetBrowser.data = typeof(T);
		bool isPicker = assetBrowser == assetBrowserUI;
		int num = list.Count;
		if (isPicker)
		{
			popupAssetBrowserOpenTime = Time.time;
		}
		if (onAddNewAsset != null)
		{
			num++;
			AddNewItemUI addNewItemUI = UnityEngine.Object.Instantiate(assetBrowser.AddNewItem, assetBrowser.Content.transform);
			addNewItemUI.gameObject.SetActive(value: true);
			addNewItemUI.Text.text = (isAssetTypeImportable<T>() ? "Import" : "New") + " " + getAssetTypeName<T>();
			addNewItemUI.root.onClick.AddListener(delegate
			{
				onAddNewAsset(typeof(T));
			});
			setHoverTooltip(string.Empty, addNewItemUI, UITooltip.Position.Right);
			PineUI.addButtonListeners(addNewItemUI.root);
		}
		foreach (UserAsset item in list)
		{
			UserAsset asset = item;
			bool isAssetSelected = isPicker && asset.path == popupAssetBrowserSelectedAssetPath;
			TextureAsset textureAsset = asset as TextureAsset;
			if (textureAsset != null)
			{
				if (!(textureAsset.texture == null))
				{
					TexItemUI texItemUI = UnityEngine.Object.Instantiate(assetBrowser.TexItem, assetBrowser.Content.transform);
					setupTextureInUI(texItemUI, textureAsset.texture);
					if (!isAssetSelected)
					{
						PineUI.addButtonListeners(texItemUI.root);
					}
				}
				continue;
			}
			MaterialAsset materialAsset = asset as MaterialAsset;
			if (materialAsset != null)
			{
				MaterialItemUI materialItemUI = UnityEngine.Object.Instantiate(assetBrowser.MaterialItem, assetBrowser.Content.transform);
				materialItemUI.gameObject.SetActive(value: true);
				materialItemUI.Text.text = asset.name;
				string text = "<b>" + asset.name + "</b> (Material)";
				string text2 = (isPicker ? "" : "\n\nClick: Open material in material editor.\nDrag and drop: Apply material to hovered prop.");
				setHoverTooltip(text + text2, materialItemUI, UITooltip.Position.Right);
				if (isAssetSelected && materialItemUI.root.TryGetComponent<UnityEngine.UI.Image>(out var component))
				{
					UnityEngine.UI.Image image = component;
					Color white = Color.white;
					float? a = 0.05f;
					image.color = white.With(null, null, null, a);
				}
				if (!isAssetSelected)
				{
					PineUI.addButtonListeners(materialItemUI.root);
				}
				if (!isPicker)
				{
					defineMaterialDragAndDropButton(materialItemUI, materialAsset);
				}
				materialItemUI.root.onClick.AddListener(delegate
				{
					handleAssetClick<MaterialAsset>(materialAsset);
				});
				applyMaterialToRawImage(getCustomMaterial(materialAsset.path), materialItemUI.Texture, useMask: true);
				continue;
			}
			AudioAsset audioAsset = asset as AudioAsset;
			if (audioAsset != null)
			{
				AudioItemUI audioItemUI = UnityEngine.Object.Instantiate(assetBrowser.AudioItem, assetBrowser.Content.transform);
				audioItemUI.gameObject.SetActive(value: true);
				audioItemUI.Text.text = asset.name;
				string text3 = "<b>" + asset.name + "</b> (Sound)";
				string text4 = (isPicker ? "" : "\n\nClick: Play this sound.\nDrag and drop: Add new sound prop into room.");
				setHoverTooltip(text3 + text4, audioItemUI, UITooltip.Position.Right);
				if (!isPicker)
				{
					defineAudioDragAndDropButton(audioItemUI, audioAsset);
				}
				audioItemUI.root.onClick.AddListener(delegate
				{
					onItemClick(audioAsset);
				});
				PineUI.addButtonListeners(audioItemUI.root);
				continue;
			}
			ScriptAsset scriptAsset = asset as ScriptAsset;
			if (scriptAsset != null)
			{
				ScriptItemUI scriptItemUI = UnityEngine.Object.Instantiate(assetBrowser.ScriptItem, assetBrowser.Content.transform);
				scriptItemUI.gameObject.SetActive(value: true);
				scriptItemUI.Text.text = asset.name;
				string text5 = "<b>" + asset.name + "</b> (Script)";
				string text6 = (isPicker ? "" : "\n\nClick: Open script in code editor.\nDrag and drop: Add new script prop into room.");
				setHoverTooltip(text5 + text6, scriptItemUI, UITooltip.Position.Right);
				if (!isPicker)
				{
					defineScriptDragAndDropButton(scriptItemUI, scriptAsset);
				}
				scriptItemUI.root.onClick.AddListener(delegate
				{
					onItemClick(scriptAsset);
				});
				PineUI.addButtonListeners(scriptItemUI.root);
				continue;
			}
			ModelAsset customModelAsset = asset as ModelAsset;
			if (customModelAsset != null)
			{
				CustomModelItemUI customModelItemUI = UnityEngine.Object.Instantiate(assetBrowser.CustomModelItem, assetBrowser.Content.transform);
				customModelItemUI.gameObject.SetActive(value: true);
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Directory.GetParent(customModelAsset.path).FullName);
				customModelItemUI.Text.text = fileNameWithoutExtension;
				setHoverTooltip(fileNameWithoutExtension, customModelItemUI, UITooltip.Position.Right);
				customModelItemUI.root.onClick.AddListener(delegate
				{
					onItemClick(customModelAsset);
				});
				PineUI.addButtonListeners(customModelItemUI.root);
			}
			void handleAssetClick<TAsset>(UserAsset clickedAsset) where TAsset : UserAsset
			{
				if (isPicker && isAssetSelected)
				{
					if (Time.time - popupAssetBrowserOpenTime < 0.5f)
					{
						closeAssetBrowser(assetBrowserUI);
					}
				}
				else
				{
					onItemClick(clickedAsset);
					if (isPicker)
					{
						popupAssetBrowserSelectedAssetPath = clickedAsset.path;
						refreshAssetBrowser<TAsset>(onItemClick, onAddNewAsset, assetBrowser, filter);
					}
				}
			}
			void setupTextureInUI(TexItemUI item, Texture2D texture)
			{
				bool flag = texture.width % 4 != 0 || texture.height % 4 != 0;
				item.gameObject.SetActive(value: true);
				item.Texture.texture = texture;
				string text7 = asset.name;
				if (text7.Length > 14)
				{
					text7 = text7.Substring(0, 14) + "..";
				}
				item.Text.text = text7;
				if (!flag)
				{
					string text8 = "<b>" + asset.name + "</b> (Texture)";
					string text9 = (isPicker ? "" : "\n\nClick: Open texture in image editor.");
					setHoverTooltip(text8 + text9, item, UITooltip.Position.Right);
					if (isAssetSelected && item.root.TryGetComponent<UnityEngine.UI.Image>(out var component2))
					{
						UnityEngine.UI.Image image2 = component2;
						Color white2 = Color.white;
						float? a2 = 0.05f;
						image2.color = white2.With(null, null, null, a2);
					}
					item.root.onClick.AddListener(delegate
					{
						handleAssetClick<TextureAsset>(textureAsset);
					});
				}
				else
				{
					setHoverTooltip(Localization.lookupInDictionary("RE_fixImageTooltip"), item, UITooltip.Position.Right);
					item.FixButton_Text.text = Localization.lookupInDictionary("RE_fixImage");
					item.FixButton.onClick.AddListener(performImageFix);
					item.root.onClick.AddListener(performImageFix);
				}
				item.FixButton.gameObject.SetActive(flag);
				void performImageFix()
				{
					texture = fixTexture(texture, asset.path);
					setupTextureInUI(item, texture);
					refresh.userAssets = true;
				}
			}
		}
		assetBrowser.Content.childAlignment = ((num >= assetBrowser.Content.constraintCount) ? TextAnchor.MiddleCenter : TextAnchor.UpperLeft);
	}

	private static string getAssetTypeName<T>() where T : UserAsset
	{
		if (typeof(T) == typeof(TextureAsset))
		{
			return "Texture";
		}
		if (typeof(T) == typeof(MaterialAsset))
		{
			return "Material";
		}
		if (typeof(T) == typeof(AudioAsset))
		{
			return "Audio";
		}
		if (typeof(T) == typeof(ScriptAsset))
		{
			return "Script";
		}
		if (typeof(T) == typeof(ModelAsset))
		{
			return "Model";
		}
		return "Asset";
	}

	private static bool isAssetTypeImportable<T>() where T : UserAsset
	{
		if (typeof(T) == typeof(TextureAsset))
		{
			return true;
		}
		if (typeof(T) == typeof(MaterialAsset))
		{
			return false;
		}
		if (typeof(T) == typeof(AudioAsset))
		{
			return true;
		}
		if (typeof(T) == typeof(ScriptAsset))
		{
			return false;
		}
		_ = typeof(T) == typeof(ModelAsset);
		return true;
	}

	private void refreshAssetBrowser<T>(Action<UserAsset> onItemClick, Action<Type> onAddNewAsset, EditorAssetBrowserUI assetBrowser, string filter, Type assetTypeToIgnore = null) where T : UserAsset
	{
		clearAssetBrowser(assetBrowser);
		populateAssetBrowser<T>(onItemClick, onAddNewAsset, assetBrowser, filter, assetTypeToIgnore);
	}

	private void closeAssetBrowser(EditorAssetBrowserUI assetBrowser)
	{
		assetBrowser.root.enabled = false;
		assetBrowser.Header_Search_Input.text = "";
		killLoadingTextureOps(loadTexturesOps, staggeredTextureLoad, "AssetBrowserUI");
		clearAssetBrowser(assetBrowser);
	}

	private void clearAssetBrowser(EditorAssetBrowserUI assetBrowser)
	{
		for (int num = assetBrowser.Content.transform.childCount - 1; num >= 0; num--)
		{
			GameObject gameObject = assetBrowser.Content.transform.GetChild(num).gameObject;
			if (!(gameObject == assetBrowser.TexItem.gameObject) && !(gameObject == assetBrowser.MaterialItem.gameObject) && !(gameObject == assetBrowser.AudioItem.gameObject) && !(gameObject == assetBrowser.CustomModelItem.gameObject) && !(gameObject == assetBrowser.FolderItem.gameObject) && !(gameObject == assetBrowser.ScriptItem.gameObject) && !(gameObject == assetBrowser.AddNewItem.gameObject))
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
	}

	private void onFileBrowserSuccess(string[] paths)
	{
		if (paths.Length == 0)
		{
			return;
		}
		FileBrowser.SetDefaultFilter(null);
		FileBrowser.SetFilters(showAllFilesFilter: true, Array.Empty<string>());
		List<string> list = new List<string>(paths);
		bool flag = false;
		foreach (string extension in supportedModelAssetFormats)
		{
			if (list.Find((string x) => x.Contains(extension)) != null)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			string text = list.Find((string x) => x.EndsWith(".gltf"));
			string text2 = list.Find((string x) => x.EndsWith(".glb"));
			if (!string.IsNullOrEmpty(text))
			{
				DirectoryInfo parent = Directory.GetParent(text);
				FileInfo[] files = parent.GetFiles("*.*", SearchOption.AllDirectories);
				currentlyImportingGltfPath = parent.FullName;
				string text3 = "You are trying to import this custom model:\n";
				FileInfo[] array = files;
				for (int num = 0; num < array.Length; num++)
				{
					text3 = text3 + array[num]?.ToString() + "\n";
				}
				optionDialog.showNoTranslate("Importing gltf", text3, onFrameClick, new VisualControl("importCustomModel", ControllerButtonActionType.UIConfirmPrimary, "%RoomEditor_OK%", VisualControl.ControlType.Button, VisualControl.Position.BotCenter), new VisualControl("ignore", ControllerButtonActionType.UIConfirmPrimary, "%cancel%", VisualControl.ControlType.Button, VisualControl.Position.BotCenter));
			}
			else if (!string.IsNullOrEmpty(text2))
			{
				importUserAsset(text2, isCustomModel: true);
			}
		}
		else
		{
			UnityEngine.Debug.Log("Importing: " + paths[0]);
			importUserAsset(paths[0], isCustomModel: false);
		}
	}

	private void onFileBrowserCancel()
	{
		FileBrowser.SetDefaultFilter(null);
		FileBrowser.SetFilters(showAllFilesFilter: true, Array.Empty<string>());
	}

	private void openUserAssetEditor(UserAsset asset)
	{
		if (asset is TextureAsset textureAsset)
		{
			openTextureEditor(textureAsset.path);
		}
		else if (asset is MaterialAsset materialAsset)
		{
			openMaterialEditor(materialAsset.path);
		}
		else if (asset is AudioAsset audioAsset)
		{
			playSoundPickerSound(audioAsset.nameWithExtension);
		}
		else if (asset is ScriptAsset scriptAsset)
		{
			openTextEditor(scriptAsset.path);
		}
		else if (asset is ModelAsset modelAsset)
		{
			openCustomModelEditor(modelAsset.path);
		}
		else
		{
			UnityEngine.Debug.LogError($"Unsupported asset type for editor: {asset.GetType()}");
		}
	}

	private void onAddNewAssetPressed(Type type, bool isAssetPicker)
	{
		if (type == typeof(ScriptAsset))
		{
			openNewScriptDialog("newScriptFromAssetBrowser", null, getDefaultScriptName());
		}
		else if (type == typeof(MaterialAsset))
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(getNextAvailableFilePath(Path.Combine(roomDirPath, "Material.es2mat")));
			string okButtonId = (isAssetPicker ? "newMaterialFromAssetPicker" : "newMaterialFromAssetBrowser");
			openNewMaterialDialog(okButtonId, "How should the new material be named?", fileNameWithoutExtension);
		}
		else
		{
			openFileBrowserForType(type);
		}
	}

	private void openAudioEditor(string path)
	{
		Application.OpenURL("file://" + Directory.GetParent(path).FullName);
	}

	private void openCustomModelEditor(string path)
	{
		Application.OpenURL("file://" + Directory.GetParent(path).FullName);
	}

	private void openTextureEditor(string path)
	{
		switch (PlayerSave.getSettings().re.textureEditorType)
		{
		case PlayerSave.RoomEditorSettings.TextureEditorType.Paint:
			Process.Start(new ProcessStartInfo
			{
				FileName = "mspaint.exe",
				Arguments = "\"" + path + "\"",
				WindowStyle = ProcessWindowStyle.Maximized,
				UseShellExecute = true
			});
			break;
		case PlayerSave.RoomEditorSettings.TextureEditorType.Custom:
			Process.Start(new ProcessStartInfo
			{
				FileName = PlayerSave.getSettings().re.customTextureEditorPath,
				Arguments = "\"" + path + "\"",
				WindowStyle = ProcessWindowStyle.Maximized,
				UseShellExecute = true
			});
			break;
		default:
			openFile(path);
			break;
		}
	}

	private void initCamera()
	{
		CAMERA_HIDE_LAYER = LayerMask.NameToLayer("Zoom");
		CAMERA_RAYCAST_MASK = ~LayerMask.GetMask("Zoom");
		cameraPosition = new Vector3(0f, 4f, -6f);
		cameraRotation = Quaternion.Euler(30f, 0f, 0f);
		mainCam.transform.position = cameraPosition;
		mainCam.transform.rotation = cameraRotation;
		cameraScreenshotPosition = cameraPosition;
		cameraScreenshotRotation = cameraRotation;
	}

	private void updateCamera()
	{
		if (cameraTransition == null)
		{
			mainCam.transform.position = Vector3.SmoothDamp(mainCam.transform.position, cameraPosition, ref cameraMoveVelocity, 0.1f);
			mainCam.transform.rotation = cameraRotation;
		}
		int num = (isSpecialMode(EditorMode.CameraRotation) ? 60 : 80);
		if (mainCam.fieldOfView != (float)num)
		{
			mainCam.fieldOfView = Mathf.SmoothStep(mainCam.fieldOfView, num, Time.deltaTime * 20f);
		}
	}

	private void setCameraRotation(Vector3 targetForward)
	{
		Quaternion identity = Quaternion.identity;
		if (Mathf.Abs(targetForward.y) < 0.99f)
		{
			identity = Quaternion.LookRotation(targetForward);
		}
		else
		{
			Vector3 vector = mainCam.transform.forward;
			if (vector.x == 0f && vector.z == 0f)
			{
				vector.y = 1f;
			}
			else if (Mathf.Abs(vector.x) > Mathf.Abs(vector.z))
			{
				vector.x = Mathf.Sign(vector.x);
				vector.y = 0f;
				vector.z = 0f;
			}
			else
			{
				vector.x = 0f;
				vector.y = 0f;
				vector.z = Mathf.Sign(vector.z);
			}
			if (targetForward.y > 0f)
			{
				vector = -vector;
			}
			identity = Quaternion.LookRotation(targetForward, vector);
		}
		tweenCameraTo(cameraPosition, identity);
	}

	private void moveCameraTo(GameObject target)
	{
		if (camToLinkedProp)
		{
			moveCameraTo(target.transform.position);
		}
	}

	private void moveCameraTo(Vector3 position, float offset = 0.5f)
	{
		Vector3 vector = Vector3.Normalize(mainCam.transform.position - position);
		Vector3 vector2 = position + vector * offset;
		Quaternion rotation = Quaternion.LookRotation(Vector3.Normalize(position - vector2));
		tweenCameraTo(vector2, rotation);
	}

	private void tweenCameraTo(Vector3 position, Quaternion rotation)
	{
		float num = 0.4f;
		if (cameraTransition != null)
		{
			num -= cameraTransition.elapsedTime;
			tweener.destroyTweens(mainCam.gameObject, performEvents: false, performFinalUpdate: false);
		}
		PineTweenSystemEnableNoHandles pineTweenSystemEnableNoHandles = tweener;
		GameObject obj = mainCam.gameObject;
		float duration = num;
		Vector3? positionTo = position;
		Quaternion? rotationTo = rotation;
		cameraTransition = pineTweenSystemEnableNoHandles.tween(obj, duration, 0f, deactivate: false, activate: false, destroy: false, null, null, null, null, null, positionTo, null, null, null, rotationTo, null, null, null, null, null, null, null, null, null, delegate
		{
			cameraPosition = mainCam.transform.position;
			cameraRotation = mainCam.transform.rotation;
			cameraTransition = null;
		}, Interpolation.SmootherStep);
	}

	private void switchCameraProjection()
	{
		if (cameraProjectionTransition == null)
		{
			float nearClipPlane = mainCam.nearClipPlane;
			float farClipPlane = mainCam.farClipPlane;
			Matrix4x4 startMatrix = mainCam.projectionMatrix;
			bool isOrtho = mainCam.orthographic;
			Matrix4x4 targetMatrix;
			if (isOrtho)
			{
				targetMatrix = Matrix4x4.Perspective(mainCam.fieldOfView, mainCam.aspect, nearClipPlane, farClipPlane);
			}
			else
			{
				float orthographicSize = mainCam.orthographicSize;
				float aspect = mainCam.aspect;
				targetMatrix = Matrix4x4.Ortho((0f - orthographicSize) * aspect, orthographicSize * aspect, 0f - orthographicSize, orthographicSize, nearClipPlane, farClipPlane);
				mainCam.orthographic = true;
			}
			cameraProjectionTransition = tweener.tween(mainCam.gameObject, 0.2f, 0f, deactivate: false, activate: false, destroy: false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, delegate(float t)
			{
				mainCam.projectionMatrix = Maths.lerpMatrix(startMatrix, targetMatrix, t);
			}, delegate
			{
				cameraProjectionTransition = null;
				mainCam.ResetProjectionMatrix();
				mainCam.orthographic = !isOrtho;
			}, Interpolation.SmootherStep);
		}
	}

	private void handleCameraWasdMovement()
	{
		Vector3 zero = Vector3.zero;
		if (menuOptions.getActionKey(KeyBindingAction.EditorForward))
		{
			zero.z += 1f;
		}
		if (menuOptions.getActionKey(KeyBindingAction.EditorBackward))
		{
			zero.z += -1f;
		}
		if (menuOptions.getActionKey(KeyBindingAction.EditorLeft))
		{
			zero.x += -1f;
		}
		if (menuOptions.getActionKey(KeyBindingAction.EditorRight))
		{
			zero.x += 1f;
		}
		if (menuOptions.getActionKey(KeyBindingAction.EditorDown))
		{
			zero.y += -1f;
		}
		if (menuOptions.getActionKey(KeyBindingAction.EditorUp))
		{
			zero.y += 1f;
		}
		cameraHoldSpeedMultiplier = ((zero == Vector3.zero) ? 1f : (cameraHoldSpeedMultiplier * Mathf.Pow(1.25f, Time.deltaTime)));
		cameraHoldSpeedMultiplier = Mathf.Clamp(cameraHoldSpeedMultiplier, 1f, 10f);
		float num = PlayerSave.getSettings().re.cameraSpeed * cameraHoldSpeedMultiplier * 10f;
		if (isCtrlPressed())
		{
			num /= 2f;
		}
		if (isShiftPressed())
		{
			num *= 2f;
		}
		Vector3 vector = zero * (num * Time.deltaTime);
		if (mainCam.orthographic)
		{
			cameraPosition += mainCam.transform.right * vector.x + mainCam.transform.up * vector.y;
			cameraPosition += mainCam.transform.forward * vector.z;
		}
		else
		{
			cameraPosition += mainCam.transform.rotation * vector;
		}
	}

	private void handleCameraScrollZoom(float scroll)
	{
		float num = (isCtrlPressed() ? 0.1f : 1f);
		float delta = scroll * num * PlayerSave.getSettings().re.cameraSpeed;
		handleCameraAxisMovement(delta, mainCam.transform.forward);
	}

	private void handleCameraAxisMovement(float delta, Vector3 axis)
	{
		if (mainCam.orthographic && axis == mainCam.transform.forward)
		{
			float num = delta * Mathf.Tan(mainCam.fieldOfView * 0.5f * (MathF.PI / 180f));
			bool num2 = mainCam.orthographicSize - num < 0.1f;
			bool flag = mainCam.orthographicSize - num > 100f;
			if (num2 || flag)
			{
				return;
			}
			mainCam.orthographicSize -= num;
		}
		cameraPosition += axis * delta;
	}

	private void handleCameraRotate(Vector2 input)
	{
		Vector2 vector = mainCam.transform.eulerAngles;
		vector.y += input.x * 3f;
		vector.x += (0f - input.y) * 3f;
		vector.x = ((vector.x > 180f) ? (vector.x - 360f) : vector.x);
		vector.x = Mathf.Clamp(vector.x, -89.99f, 89.99f);
		cameraRotation = Quaternion.Euler(vector.x, vector.y, 0f);
	}

	private void handleCameraPan(Vector2 input)
	{
		float num = 0.25f;
		if (isPropInstanceSelected())
		{
			float value = Mathf.Clamp(Vector3.Distance(mainCam.transform.position, transformGizmo.pivotPoint), 0.5f, 5f);
			float num2 = Mathf.InverseLerp(0.5f, 5f, value);
			num *= num2;
		}
		Vector2 vector = input * (-1f * num);
		cameraPosition += mainCam.transform.right * vector.x + mainCam.transform.up * vector.y;
	}

	private void updateCodeEditorInput()
	{
		if (Input.GetKeyDown(KeyCode.Home))
		{
			TextPosition caretPosition = textEditorUI.Editor.CaretPosition;
			int lineIndex = caretPosition.lineIndex;
			string text = textEditorUI.Editor.Lines[lineIndex].Text;
			int colIndex = text.Length - text.TrimStart().Length;
			TextPosition textPosition = new TextPosition(lineIndex, colIndex);
			textEditorUI.Editor.CaretPosition = textPosition;
			if (isShiftPressed())
			{
				textEditorUI.Editor.Selection = new Selection(caretPosition, textPosition);
			}
		}
		else if (Input.GetKeyDown(KeyCode.End))
		{
			TextPosition caretPosition2 = textEditorUI.Editor.CaretPosition;
			int lineIndex2 = caretPosition2.lineIndex;
			int length = textEditorUI.Editor.Lines[lineIndex2].Text.Length;
			TextPosition textPosition2 = new TextPosition(lineIndex2, length);
			textEditorUI.Editor.CaretPosition = textPosition2;
			if (isShiftPressed())
			{
				textEditorUI.Editor.Selection = new Selection(caretPosition2, textPosition2);
			}
		}
		else if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (optionDialog.gameObject.activeInHierarchy)
			{
				optionDialog.hide();
				textEditorUI.Editor.disableInput = textEditorPath == null;
			}
			else
			{
				closeTextEditor();
			}
		}
		else if (menuOptions.getActionKeyDown(KeyBindingAction.EditorToggleCodeEditor))
		{
			optionDialog.hide();
			closeTextEditor();
		}
	}

	private void onTextEditorButtonClick(Button button)
	{
		if (button == textEditorUI.Buttons_Close)
		{
			closeTextEditor();
		}
		else if (button == textEditorUI.Buttons_Copy)
		{
			textEditorUI.Editor.Copy();
		}
		else if (button == textEditorUI.Buttons_Cut)
		{
			textEditorUI.Editor.Cut();
		}
		else if (button == textEditorUI.Buttons_Paste)
		{
			textEditorUI.Editor.Paste();
		}
		else if (button == textEditorUI.Buttons_SelectAll)
		{
			textEditorUI.Editor.SelectAll();
		}
		else if (button == textEditorUI.Buttons_Undo)
		{
			textEditorUI.Editor.Undo();
		}
		else if (button == textEditorUI.Buttons_Redo)
		{
			textEditorUI.Editor.Redo();
		}
	}

	private void openTextEditor()
	{
		if (!string.IsNullOrEmpty(textEditorPath) && File.Exists(textEditorPath))
		{
			openTextEditor(textEditorPath);
			return;
		}
		HashSet<string> invokableFunctions;
		string content = setupAndGenerateLevelLogicLua(getEditorContext().container.gameObject, dry: true, out invokableFunctions);
		openTextEditor(content, isReadOnly: true);
	}

	private void openTextEditor(string path)
	{
		switch (PlayerSave.getSettings().re.textEditorType)
		{
		case PlayerSave.RoomEditorSettings.TextEditorType.Builtin:
		{
			if (isSpecialMode(EditorMode.CodeEditor))
			{
				closeTextEditor();
			}
			textEditorPath = path;
			string content = File.ReadAllText(path);
			openTextEditor(content, isReadOnly: false);
			break;
		}
		case PlayerSave.RoomEditorSettings.TextEditorType.Custom:
			Process.Start(new ProcessStartInfo
			{
				FileName = PlayerSave.getSettings().re.customTextEditorPath,
				Arguments = "\"" + path + "\"",
				WindowStyle = ProcessWindowStyle.Maximized,
				UseShellExecute = true
			});
			break;
		default:
			openFile(path);
			break;
		}
	}

	private void openTextEditor(string content, bool isReadOnly)
	{
		enterSpecialMode(EditorMode.CodeEditor);
		editorUI.root.enabled = false;
		int num = (int)PlayerSave.getSettings().re.textEditorTheme;
		if (num > textEditorThemes.Count - 1)
		{
			num = 0;
		}
		TextEditorTheme textEditorTheme = textEditorThemes[num];
		InGameTextEditor.TextEditor editor = textEditorUI.Editor;
		editor.mainBackgroundColor = textEditorTheme.mainBackgroundColor;
		editor.mainFontColor = textEditorTheme.mainFontColor;
		editor.lineNumberFontColor = textEditorTheme.mainFontColor;
		editor.lineNumberBackgroundColor = textEditorTheme.lineNumberBackgroundColor;
		editor.caretColor = textEditorTheme.caretColor;
		editor.selectionActiveColor = textEditorTheme.activeSelectionColor;
		editor.selectionInactiveColor = textEditorTheme.inactiveSelectionColor;
		editor.disableInput = isReadOnly;
		LuaSyntaxHighlighter component = editor.GetComponent<LuaSyntaxHighlighter>();
		component.textStyleComment.fontColor = textEditorTheme.commentColor;
		component.textStyleString.fontColor = textEditorTheme.stringColor;
		component.textStyleNumber.fontColor = textEditorTheme.numberColor;
		component.textStyleKeyword.fontColor = textEditorTheme.keywordColor;
		component.textStyleBuiltIn.fontColor = textEditorTheme.builtInColor;
		textEditorUI.Buttons_Close_Text.text = "Close";
		textEditorUI.Editor.CaretPosition = new TextPosition(0, 0);
		textEditorUI.root.enabled = true;
		textEditorUI.Editor.SetText(content);
		textEditorUI.LuaFiles.Container_LuaScriptButton.GetComponent<Button>().onClick.RemoveAllListeners();
		textEditorUI.LuaFiles.Container_AddFileButton.onClick.RemoveAllListeners();
		Dictionary<string, List<PropInstance>> dictionary = new Dictionary<string, List<PropInstance>>();
		LuaExecutor[] componentsInChildren = levelContainer.GetComponentsInChildren<LuaExecutor>(includeInactive: true);
		foreach (LuaExecutor luaExecutor in componentsInChildren)
		{
			string luaCodeLocation = luaExecutor.luaCodeLocation;
			if (!string.IsNullOrEmpty(luaCodeLocation))
			{
				if (dictionary.TryGetValue(luaCodeLocation, out var value))
				{
					value.Add(luaExecutor.GetComponent<PropInstance>());
					continue;
				}
				dictionary[luaCodeLocation] = new List<PropInstance> { luaExecutor.GetComponent<PropInstance>() };
			}
		}
		foreach (Transform item in textEditorUI.LuaFiles.Container_LuaScriptButton.transform.parent)
		{
			if (!(item == textEditorUI.LuaFiles.Container_LuaScriptButton.transform) && !(item == textEditorUI.LuaFiles.Container_AddFileButton.transform))
			{
				UnityEngine.Object.Destroy(item.gameObject);
			}
		}
		Color forestGreen = Color.forestGreen;
		Color white = Color.white;
		textEditorUI.LuaFiles.Container_LuaScriptButton.Usages.gameObject.SetActive(value: false);
		textEditorUI.LuaFiles.Container_LuaScriptButton.ScriptName.color = ((textEditorPath == null) ? forestGreen : white);
		foreach (string scriptName in getAllScriptNames(roomDirPath))
		{
			LuaScriptButtonUI container_LuaScriptButton = textEditorUI.LuaFiles.Container_LuaScriptButton;
			LuaScriptButtonUI luaScriptButtonUI = UnityEngine.Object.Instantiate(container_LuaScriptButton, container_LuaScriptButton.transform.parent);
			Button component2 = luaScriptButtonUI.GetComponent<Button>();
			PineUI.addButtonListeners(component2);
			List<PropInstance> value2;
			bool flag = dictionary.TryGetValue(scriptName, out value2);
			luaScriptButtonUI.Usages.gameObject.SetActive(value: true);
			luaScriptButtonUI.ScriptName.text = scriptName;
			bool flag2 = textEditorPath != null && Path.GetFileNameWithoutExtension(textEditorPath) == scriptName;
			luaScriptButtonUI.ScriptName.color = (flag2 ? forestGreen : white);
			luaScriptButtonUI.Usages.color = (flag2 ? forestGreen : white);
			string text = "No usages found.";
			if (flag)
			{
				text = string.Format(arg2: string.Join(", ", value2.ConvertAll((PropInstance propInstance) => (!string.IsNullOrEmpty(propInstance.scriptName)) ? propInstance.scriptName : $"id_{propInstance.ID}")), arg1: (value2.Count == 1) ? "" : "s", format: "{0} usage{1}:  {2}", arg0: value2.Count);
			}
			if (!flag2)
			{
				text = text.Colored(Color.gray3);
			}
			luaScriptButtonUI.Usages.text = text;
			component2.onClick.AddListener(delegate
			{
				string text2 = roomDirPath + "/" + scriptName + ".lua";
				if (!(textEditorPath == text2))
				{
					openTextEditor(roomDirPath + "/" + scriptName + ".lua");
				}
			});
		}
		textEditorUI.LuaFiles.Container_LuaScriptButton.GetComponent<Button>().onClick.AddListener(delegate
		{
			closeTextEditor();
			textEditorPath = null;
			openTextEditor();
		});
		textEditorUI.LuaFiles.Container_AddFileButton.onClick.AddListener(delegate
		{
			textEditorUI.Editor.disableInput = true;
			openNewScriptDialog("newScriptFromTextEditor", null, getDefaultScriptName());
		});
		textEditorUI.Buttons.color = textEditorTheme.buttonsPanelBackgroundColor;
		textEditorUI.LuaFiles.root.color = textEditorTheme.filePickerBackgroundColor;
		textEditorUI.LuaFiles.Container_AddFileButton.transform.SetAsLastSibling();
		if (!wasTextEditorLayoutRebuilt)
		{
			textEditorUI.Editor.UpdateFont();
			textEditorUI.Editor.UpdateLayout();
			wasTextEditorLayoutRebuilt = true;
		}
	}

	private void closeTextEditor()
	{
		if (!string.IsNullOrEmpty(textEditorPath))
		{
			File.WriteAllText(textEditorPath, textEditorUI.Editor.Text);
		}
		editorUI.root.enabled = true;
		textEditorUI.root.enabled = false;
		exitSpecialMode();
	}

	private void openNewScriptDialog(string okButtonId, string message = null, string inputFieldText = "")
	{
		optionDialog.showNoTranslate("New Script", message ?? "Give a name to the new script.", onFrameClick, new VisualControl(okButtonId, ControllerButtonActionType.UIConfirmPrimary, "%RoomEditor_OK%", VisualControl.ControlType.Button, VisualControl.Position.BotLeft), new VisualControl("newScriptCancel", ControllerButtonActionType.UIConfirmPrimary, "%cancel%", VisualControl.ControlType.Button, VisualControl.Position.BotRight));
		optionDialog.useInputField(okButtonId, inputFieldText);
	}

	private void createNewScript(string scriptName, bool linkToSelectedProp)
	{
		scriptName = scriptName.Trim();
		if (string.IsNullOrEmpty(scriptName))
		{
			return;
		}
		string path = roomDirPath + "/" + scriptName + ".lua";
		if (!File.Exists(path))
		{
			File.WriteAllText(path, "function onInit()\r\n    api.levelNote(\"Hello World!\")\r\nend");
			refresh.userAssets = true;
			if (linkToSelectedProp && isSelectedSingle<LuaExecutor>())
			{
				PropInstance selectedInstance = getSelectedInstance();
				var (prev, propData) = prepChangeData(selectedInstance);
				getScriptComponentData(propData).scriptLocation = scriptName;
				undo.push(actionChangeProp(prev, propData, "codeLocation"));
				refresh.propertiesUI = true;
			}
			openTextEditor(path);
		}
	}

	private static List<string> getAllScriptNames(string roomDirPath)
	{
		List<string> list = new List<string>();
		if (string.IsNullOrEmpty(roomDirPath) || !Directory.Exists(roomDirPath))
		{
			return list;
		}
		string[] files = Directory.GetFiles(roomDirPath);
		foreach (string path in files)
		{
			if (!(Path.GetExtension(path) != ".lua"))
			{
				list.Add(Path.GetFileNameWithoutExtension(path));
			}
		}
		return list;
	}

	private string getDefaultScriptName()
	{
		return Path.GetFileNameWithoutExtension(getNextAvailableFilePath(Path.Combine(roomDirPath, "Script.lua")));
	}

	public static string setupAndGenerateLevelLogicLua(GameObject levelContainer, bool dry, out HashSet<string> invokableFunctions)
	{
		List<PropInstance> list = new List<PropInstance>();
		PropInstance[] componentsInChildren = levelContainer.GetComponentsInChildren<PropInstance>(includeInactive: true);
		foreach (PropInstance propInstance in componentsInChildren)
		{
			if (string.IsNullOrWhiteSpace(propInstance.scriptName))
			{
				propInstance.scriptName = $"id_{propInstance.ID.value}";
				if (dry)
				{
					list.Add(propInstance);
				}
			}
		}
		CodeBuilder codeBuilder = new CodeBuilder();
		appendHeaderMessage();
		invokableFunctions = new HashSet<string>();
		if (appendOnInit())
		{
			invokableFunctions.Add("onInit");
		}
		if (appendOnUpdate())
		{
			invokableFunctions.Add("onUpdate");
		}
		if (appendOnSwitch3D())
		{
			invokableFunctions.Add("onSwitch3D");
		}
		if (appendOnSlot())
		{
			invokableFunctions.Add("onSlot");
		}
		if (appendOnRemoveFromSlot())
		{
			invokableFunctions.Add("onRemoveFromSlot");
		}
		if (appendOnUnlock())
		{
			invokableFunctions.Add("onUnlock");
		}
		if (appendOnTrigger())
		{
			invokableFunctions.Add("onTrigger");
		}
		if (appendOnRoulette())
		{
			invokableFunctions.Add("onRoulette");
		}
		if (appendOnDelay())
		{
			invokableFunctions.Add("onDelay");
		}
		if (appendOnLookable())
		{
			invokableFunctions.Add("onLookable");
		}
		if (dry)
		{
			foreach (PropInstance item in list)
			{
				item.scriptName = "";
			}
		}
		return codeBuilder.ToString();
		void appendHeaderMessage()
		{
			codeBuilder.writeLine("-- This file is auto-generated and cannot be modified.");
			codeBuilder.writeLine("-- If you want to add custom Lua logic, add \"Script\" logic component.");
			codeBuilder.writeLine("-- This file also acts as documentation and all the functions defined here can be implemented in your own scripts.");
			codeBuilder.writeLine();
		}
		bool appendOnDelay()
		{
			codeBuilder.writeLine("-- Called when a EditorDelay has finished the delay.");
			codeBuilder.writeLine("-- delay: EditorDelay which has finished.");
			bool result = false;
			codeBuilder.writeLine("function onDelay(delay)");
			codeBuilder.beginBlock();
			EditorDelay[] componentsInChildren2 = levelContainer.GetComponentsInChildren<EditorDelay>(includeInactive: true);
			foreach (EditorDelay obj in componentsInChildren2)
			{
				PropInstance component = obj.GetComponent<PropInstance>();
				codeBuilder.writeLine("if delay == " + component.scriptName + " then");
				codeBuilder.beginBlock();
				foreach (GameObject target in obj.targets)
				{
					appendTargets(target);
					result = true;
				}
				codeBuilder.endBlock();
				codeBuilder.writeLine("end");
			}
			codeBuilder.endBlock();
			codeBuilder.writeLine("end");
			codeBuilder.writeLine();
			return result;
		}
		bool appendOnInit()
		{
			codeBuilder.writeLine("-- Called once at the start of the level.");
			codeBuilder.writeLine("-- Use to initialize your script logic.");
			codeBuilder.writeLine("function onInit()");
			bool result = false;
			codeBuilder.beginBlock();
			EditorSetup[] componentsInChildren2 = levelContainer.GetComponentsInChildren<EditorSetup>(includeInactive: true);
			foreach (EditorSetup obj in componentsInChildren2)
			{
				PropInstance component = obj.GetComponent<PropInstance>();
				codeBuilder.writeLine("-- Setup \"" + component.scriptName + "\"");
				foreach (GameObject target2 in obj.targets)
				{
					appendTargets(target2);
					result = true;
				}
				codeBuilder.writeLine();
			}
			codeBuilder.endBlock();
			codeBuilder.writeLine("end");
			codeBuilder.writeLine();
			return result;
		}
		bool appendOnLookable()
		{
			codeBuilder.writeLine("-- Called when a Lookable gets targeted.");
			codeBuilder.writeLine("-- lookable: Lookable which has been targeted.");
			codeBuilder.writeLine("-- isActivated: is the target lookable activated or not. If true, it means that player is looking at the target lookable.");
			bool result = false;
			codeBuilder.writeLine("function onLookable(lookable, isActivated)");
			codeBuilder.beginBlock();
			Lookable[] componentsInChildren2 = levelContainer.GetComponentsInChildren<Lookable>(includeInactive: true);
			foreach (Lookable lookable in componentsInChildren2)
			{
				PropInstance component = lookable.GetComponent<PropInstance>();
				codeBuilder.writeLine("if lookable == " + component.scriptName + " then");
				codeBuilder.beginBlock();
				codeBuilder.writeLine("if isActivated then");
				for (int k = 0; k < lookable.onActivated.Count; k++)
				{
					appendTargets(lookable.onActivated[k].gameObject, indent: true);
					result = true;
				}
				codeBuilder.writeLine("else");
				for (int l = 0; l < lookable.onDeactivated.Count; l++)
				{
					appendTargets(lookable.onDeactivated[l].gameObject, indent: true);
					result = true;
				}
				codeBuilder.writeLine("end");
				codeBuilder.endBlock();
				codeBuilder.writeLine("end");
			}
			codeBuilder.endBlock();
			codeBuilder.writeLine("end");
			codeBuilder.writeLine();
			return result;
		}
		bool appendOnRemoveFromSlot()
		{
			codeBuilder.writeLine("-- Called every time item is removed from Slot.");
			codeBuilder.writeLine("-- slot: Slot for which item removal has occurred.");
			codeBuilder.writeLine("-- item: Item which was removed.");
			bool result = false;
			codeBuilder.writeLine("function onRemoveFromSlot(slot, item)");
			codeBuilder.beginBlock();
			Slot[] componentsInChildren2 = levelContainer.GetComponentsInChildren<Slot>(includeInactive: true);
			foreach (Slot slot in componentsInChildren2)
			{
				PropInstance component = slot.GetComponent<PropInstance>();
				codeBuilder.writeLine("if slot == " + component.scriptName + " then");
				codeBuilder.beginBlock();
				for (int k = 0; k < slot.onRemove.targets.Count; k++)
				{
					appendTargets(slot.onRemove.targets[k].gameObject);
					result = true;
				}
				codeBuilder.endBlock();
				codeBuilder.writeLine("end");
			}
			codeBuilder.endBlock();
			codeBuilder.writeLine("end");
			codeBuilder.writeLine();
			return result;
		}
		bool appendOnRoulette()
		{
			codeBuilder.writeLine("-- Called when a Roulette gets targeted.");
			codeBuilder.writeLine("-- roulette: Roulette which has been targeted.");
			codeBuilder.writeLine("-- index: Index of target that Roulette has randomly chosen to target.");
			bool result = false;
			codeBuilder.writeLine("function onRoulette(roulette, index)");
			codeBuilder.beginBlock();
			Roulette[] componentsInChildren2 = levelContainer.GetComponentsInChildren<Roulette>(includeInactive: true);
			foreach (Roulette roulette in componentsInChildren2)
			{
				PropInstance component = roulette.GetComponent<PropInstance>();
				codeBuilder.writeLine("if roulette == " + component.scriptName + " then");
				codeBuilder.beginBlock();
				for (int k = 0; k < roulette.targets.Count; k++)
				{
					codeBuilder.writeLine(string.Format("if {0} == {1} then", "index", k));
					appendTargets(roulette.targets[k].gameObject, indent: true);
					codeBuilder.writeLine("end");
					result = true;
				}
				codeBuilder.endBlock();
				codeBuilder.writeLine("end");
			}
			codeBuilder.endBlock();
			codeBuilder.writeLine("end");
			codeBuilder.writeLine();
			return result;
		}
		bool appendOnSlot()
		{
			codeBuilder.writeLine("-- Called every time item is inserted into Slot.");
			codeBuilder.writeLine("-- slot: Slot for which item insertion has occurred.");
			bool result = false;
			codeBuilder.writeLine("function onSlot(slot)");
			codeBuilder.beginBlock();
			Slot[] componentsInChildren2 = levelContainer.GetComponentsInChildren<Slot>(includeInactive: true);
			foreach (Slot obj in componentsInChildren2)
			{
				PropInstance component = obj.GetComponent<PropInstance>();
				codeBuilder.writeLine("if slot == " + component.scriptName + " and slot.isUnlocked then");
				codeBuilder.beginBlock();
				foreach (GameObject target3 in obj.onPlace.targets)
				{
					appendTargets(target3);
					result = true;
				}
				codeBuilder.endBlock();
				codeBuilder.writeLine("end");
			}
			codeBuilder.endBlock();
			codeBuilder.writeLine("end");
			codeBuilder.writeLine();
			return result;
		}
		bool appendOnSwitch3D()
		{
			codeBuilder.writeLine("-- Called every time Switch3D event happens.");
			codeBuilder.writeLine("-- switch3D: Switch for which event has occurred.");
			codeBuilder.writeLine("-- switchEvent: Event that has occurred. Can be one of the following:");
			codeBuilder.writeLine("--     Start: Invoked when switch3D is pressed");
			codeBuilder.writeLine("--     On: Invoked when switch3D arrives to destination.");
			codeBuilder.writeLine("--     Off: Invoked when switch3D returns back to initial starting point.");
			codeBuilder.writeLine("--     Release: Invoked when switch3D of type \"Hold\" is released.");
			bool isInvokable = false;
			codeBuilder.writeLine("function onSwitch3D(switch3D, switchEvent)");
			codeBuilder.beginBlock();
			Switch3D[] switch3Ds = levelContainer.GetComponentsInChildren<Switch3D>(includeInactive: true);
			codeBuilder.writeLine("if switchEvent == Switch3DEvent.Start then");
			codeBuilder.writeLine("elseif switchEvent == Switch3DEvent.On then");
			appendSwitchLinks((Switch3D switch3D) => switch3D.onLinks, "onLinks");
			codeBuilder.writeLine("elseif switchEvent == Switch3DEvent.Off then");
			appendSwitchLinks((Switch3D switch3D) => switch3D.offLinks, "offLinks");
			codeBuilder.writeLine("elseif switchEvent == Switch3DEvent.Release then");
			codeBuilder.writeLine("end");
			codeBuilder.endBlock();
			codeBuilder.writeLine("end");
			codeBuilder.writeLine();
			return isInvokable;
			void appendSwitchLinks(Func<Switch3D, RoomEditorLinks> getLinks, string linksName)
			{
				codeBuilder.beginBlock();
				Switch3D[] array = switch3Ds;
				foreach (Switch3D switch3D in array)
				{
					PropInstance component = switch3D.GetComponent<PropInstance>();
					codeBuilder.writeLine("if switch3D == " + component.scriptName + " then");
					codeBuilder.beginBlock();
					RoomEditorLinks roomEditorLinks = getLinks(switch3D);
					for (int k = 0; k < roomEditorLinks.targets.Count; k++)
					{
						appendTargets(roomEditorLinks.targets[k].gameObject);
						isInvokable = true;
					}
					codeBuilder.endBlock();
					codeBuilder.writeLine("end");
				}
				codeBuilder.endBlock();
			}
		}
		bool appendOnTrigger()
		{
			codeBuilder.writeLine("-- Called every time player and/or item (depending on settings) enters or exists Trigger.");
			codeBuilder.writeLine("-- trigger: Trigger which has been entered or exited.");
			codeBuilder.writeLine("-- triggerEvent: Event which has occurred. Can be one of the following:");
			codeBuilder.writeLine("--     TriggerEventType.Started: Invoked when the trigger was empty and something enters it.");
			codeBuilder.writeLine("--     TriggerEventType.Enter: Invoked everytime when something enters trigger.");
			codeBuilder.writeLine("--     TriggerEventType.Exit: Invoked everytime when something exits trigger.");
			codeBuilder.writeLine("--     TriggerEventType.Ended: Invoked when the trigger had something in it, it left and the trigger is now empty.");
			bool isInvokable = false;
			codeBuilder.writeLine("function onTrigger(trigger, triggerEvent)");
			codeBuilder.beginBlock();
			Trigger[] triggers = levelContainer.GetComponentsInChildren<Trigger>(includeInactive: true);
			appendTriggerEventTargets("if", "Started", (Trigger t) => t.startData);
			appendTriggerEventTargets("elseif", "Enter", (Trigger t) => t.enterData);
			appendTriggerEventTargets("elseif", "Exit", (Trigger t) => t.exitData);
			appendTriggerEventTargets("elseif", "Ended", (Trigger t) => t.endData);
			codeBuilder.writeLine("end");
			codeBuilder.endBlock();
			codeBuilder.writeLine("end");
			codeBuilder.writeLine();
			return isInvokable;
			void appendTriggerEventTargets(string ifStatement, string eventType, Func<Trigger, RoomEditorLinks> getData)
			{
				codeBuilder.writeLine(ifStatement + " triggerEvent.type == TriggerEventType." + eventType + " then");
				codeBuilder.beginBlock();
				Trigger[] array = triggers;
				foreach (Trigger trigger in array)
				{
					PropInstance component = trigger.GetComponent<PropInstance>();
					codeBuilder.writeLine("if trigger == " + component.scriptName + " then");
					codeBuilder.beginBlock();
					RoomEditorLinks roomEditorLinks = getData(trigger);
					for (int k = 0; k < roomEditorLinks.targets.Count; k++)
					{
						appendTargets(roomEditorLinks.targets[k].gameObject);
						isInvokable = true;
					}
					codeBuilder.endBlock();
					codeBuilder.writeLine("end");
				}
				codeBuilder.endBlock();
			}
		}
		bool appendOnUnlock()
		{
			codeBuilder.writeLine("-- Called when a Lock gets unlocked.");
			codeBuilder.writeLine("-- lock: Lock which has unlocked.");
			bool result = false;
			codeBuilder.writeLine("function onUnlock(lock)");
			codeBuilder.beginBlock();
			Lock[] componentsInChildren2 = levelContainer.GetComponentsInChildren<Lock>(includeInactive: true);
			foreach (Lock obj in componentsInChildren2)
			{
				PropInstance component = obj.GetComponent<PropInstance>();
				codeBuilder.writeLine("if lock == " + component.scriptName + " then");
				codeBuilder.beginBlock();
				for (int k = 0; k < obj.onUnlock.targets.Count; k++)
				{
					appendTargets(obj.onUnlock.targets[k].gameObject);
					result = true;
				}
				codeBuilder.endBlock();
				codeBuilder.writeLine("end");
			}
			codeBuilder.endBlock();
			codeBuilder.writeLine("end");
			codeBuilder.writeLine();
			return result;
		}
		bool appendOnUpdate()
		{
			codeBuilder.writeLine("-- Called every frame.");
			codeBuilder.writeLine("-- Use to execute your script logic.");
			codeBuilder.writeLine("function onUpdate()");
			codeBuilder.writeLine("end");
			codeBuilder.writeLine();
			return false;
		}
		void appendTargets(GameObject activatedObject, bool indent = false)
		{
			if (indent)
			{
				codeBuilder.beginBlock();
			}
			string text = activatedObject.GetComponent<PropInstance>().scriptName + ".gameObject";
			Teleport component2;
			Roulette component3;
			Finish component4;
			OpenLink component5;
			Switch3D component6;
			ActivatorComponent component7;
			EditorPuzzle component8;
			EditorPostProcessing component9;
			EditorSkybox component10;
			EditorClouds component11;
			Fog component12;
			EditorOcean component13;
			LuaExecutor component14;
			EditorDelay component15;
			if (activatedObject.gameObject.TryGetComponent<Sound>(out var _))
			{
				codeBuilder.writeLine("api.playSound(" + text + ")");
			}
			else if (activatedObject.gameObject.TryGetComponent<Teleport>(out component2))
			{
				codeBuilder.writeLine("api.teleport(" + text + ")");
			}
			else if (activatedObject.gameObject.TryGetComponent<Roulette>(out component3))
			{
				codeBuilder.writeLine("api.handleRoulette(" + text + ")");
			}
			else if (activatedObject.gameObject.TryGetComponent<Finish>(out component4))
			{
				codeBuilder.writeLine("api.handleFinish(" + text + ")");
			}
			else if (activatedObject.gameObject.TryGetComponent<OpenLink>(out component5))
			{
				codeBuilder.writeLine("api.openLink(" + text + ")");
			}
			else if (activatedObject.gameObject.TryGetComponent<Switch3D>(out component6))
			{
				codeBuilder.writeLine("api.handleSwitch3D(" + text + ")");
			}
			else if (activatedObject.gameObject.TryGetComponent<ActivatorComponent>(out component7))
			{
				codeBuilder.writeLine("api.handleActivator(" + text + ")");
			}
			else if (activatedObject.gameObject.TryGetComponent<EditorPuzzle>(out component8))
			{
				codeBuilder.writeLine("api.handlePuzzle(" + text + ")");
			}
			else if (activatedObject.gameObject.TryGetComponent<EditorPostProcessing>(out component9))
			{
				codeBuilder.writeLine("api.activatePostProcessing(" + text + ")");
			}
			else if (activatedObject.gameObject.TryGetComponent<EditorSkybox>(out component10))
			{
				codeBuilder.writeLine("api.activateSkybox(" + text + ")");
			}
			else if (activatedObject.gameObject.TryGetComponent<EditorClouds>(out component11))
			{
				codeBuilder.writeLine("api.activateClouds(" + text + ")");
			}
			else if (activatedObject.gameObject.TryGetComponent<Fog>(out component12))
			{
				codeBuilder.writeLine("api.activateFog(" + text + ")");
			}
			else if (activatedObject.gameObject.TryGetComponent<EditorOcean>(out component13))
			{
				codeBuilder.writeLine("api.activateOcean(" + text + ")");
			}
			else if (activatedObject.gameObject.TryGetComponent<LuaExecutor>(out component14))
			{
				codeBuilder.writeLine("api.callLuaFunction(" + text + ")");
			}
			else if (activatedObject.gameObject.TryGetComponent<EditorDelay>(out component15))
			{
				codeBuilder.writeLine("api.handleDelay(" + text + ")");
			}
			if (indent)
			{
				codeBuilder.endBlock();
			}
		}
	}

	private static T copy<T>(T value) where T : IReadWrite, new()
	{
		FastBinaryWriter writer;
		using (SharedWriter.borrow(out writer))
		{
			writer.WriteIReadWrite(value);
			using FastBinaryReader reader = new FastBinaryReader(writer.ToArray());
			return reader.ReadIReadWrite<T>();
		}
	}

	private static bool tryGetPolygonData(PropData propData, bool isFloor, out PolygonData data)
	{
		data = getPolygonData(propData, isFloor);
		return data != null;
	}

	private static PolygonData getPolygonData(PropData propData, bool isFloor)
	{
		if (!isFloor)
		{
			return getWallData(propData);
		}
		return getFloorData(propData);
	}

	private static string grey(string str)
	{
		return colorized(str, "grey");
	}

	private static string red(string str)
	{
		return colorized(str, "red");
	}

	private static string orange(string str)
	{
		return colorized(str, "orange");
	}

	private static string colorized(string str, string color)
	{
		return "<color=" + color + ">" + str + "</color>";
	}

	private void initDebug()
	{
		if (!Is.Editor)
		{
			return;
		}
		if (testPlaceMode == TestPlaceMode.PlaceLogics)
		{
			testPlaceProps((Prop prop) => prop.ID.value.StartsWith("Logics"));
		}
		if (testPlaceMode == TestPlaceMode.PlaceAll)
		{
			testPlaceProps((Prop _) => true);
		}
	}

	private void updateDebug()
	{
		if (!Is.Editor)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			UnityUtils.printUIUnderMouse();
		}
		if (testPlaceOneByOne)
		{
			testPlaceOneByOneProc();
		}
		else if (testLoadRoomOneByOne)
		{
			testLoadOneByOneProc();
		}
		if (!debugUI.root.enabled)
		{
			return;
		}
		for (int i = 1; i < debugUI.root.transform.childCount; i++)
		{
			Transform child = debugUI.root.transform.GetChild(i);
			PropInstance instanceByID = getInstanceByID(new InstanceID
			{
				value = int.Parse(child.name)
			});
			bool flag = Vector3.Distance(mainCam.transform.position, instanceByID.transform.position) < 2f;
			child.GetComponent<UnityEngine.UI.Image>().enabled = flag;
			if (flag)
			{
				Vector3 position = mainCam.WorldToScreenPoint(instanceByID.transform.position);
				child.position = position;
			}
		}
	}

	private void testPlaceOneByOneProc()
	{
		if (placeAllTest.propIndex == assets.props.Count || Time.frameCount - placeAllTest.frame <= 0)
		{
			return;
		}
		bool flag = false;
		if (placeAllTest.stage == PlaceTestStage.Instantiate)
		{
			PropID iD = assets.props[placeAllTest.propIndex].ID;
			while (placeAllTest.propIndex < assets.props.Count)
			{
				iD = assets.props[placeAllTest.propIndex].ID;
				if (isWhitelistedProp(assets.props[placeAllTest.propIndex]) && !isBlacklistedProp(iD))
				{
					break;
				}
				placeAllTest.propIndex++;
			}
			flag = true;
			placeAllTest.instance = testPlaceProp(iD);
			UnityEngine.Debug.Log($"Instantiate {orange(iD.value)} at {placeAllTest.propIndex + 1}/{assets.props.Count}");
		}
		else if (placeAllTest.stage == PlaceTestStage.Select)
		{
			flag = true;
			undo.push(actionChangeSelection(newInstanceSelection(placeAllTest.instance.ID)));
		}
		else
		{
			flag = true;
			cancelSelection();
			placeAllTest.propIndex++;
		}
		if (flag)
		{
			placeAllTest.stage = (PlaceTestStage)((int)(placeAllTest.stage + 1) % 3);
			placeAllTest.frame = Time.frameCount;
		}
	}

	private PropInstance testPlaceProp(PropID propID, Vector3? position = null)
	{
		UnityEngine.Debug.Log("Test placing prop with ID: " + propID.value);
		GameObject gameObject = UnityEngine.Object.Instantiate(loadAssetPrefab(propID));
		gameObject.transform.position = position ?? Vector3.forward;
		gameObject.name = propID.value;
		PropInstance component = gameObject.GetComponent<PropInstance>();
		List<PropInstance> childPropInstances = getChildPropInstances(component);
		foreach (PropInstance item in childPropInstances)
		{
			item.ID = getNextInstanceID();
		}
		List<PropData> propsData = compressProps(childPropInstances);
		List<EditorAction> list = new List<EditorAction>();
		actionsLoadProps(propsData, list);
		undo.push(list.ToArray());
		UnityEngine.Object.Destroy(gameObject);
		return propInstances[childPropInstances[0].ID];
	}

	private void testPlaceProps(Predicate<Prop> filter)
	{
		int count = assets.props.Count;
		int num = 0;
		UnityEngine.Debug.Log($"Placing all {count} props in the scene...");
		int num2 = 0;
		int num3 = 0;
		int num4 = Mathf.CeilToInt(Mathf.Sqrt(count));
		while (num < count)
		{
			Prop prop = assets.props[num];
			num++;
			if (filter(prop))
			{
				if (isWhitelistedProp(prop) && !isBlacklistedProp(prop.ID))
				{
					float x = (float)num3 * testPlaceAllSpacing;
					float z = (float)num2 * testPlaceAllSpacing;
					testPlaceProp(prop.ID, new Vector3(x, 0f, z));
				}
				num3++;
				if (num3 == num4)
				{
					num3 = 0;
					num2++;
				}
			}
		}
	}

	private void testLoadOneByOneProc()
	{
		if (loadableRooms == null)
		{
			loadableRooms = loadRoomsMeta();
		}
		if (loadAllTest.roomIndex != loadableRooms.Count && Time.frameCount - loadAllTest.frame > 60)
		{
			RoomMeta roomMeta = loadableRooms[loadAllTest.roomIndex++];
			UnityEngine.Debug.Log("=== Loading room " + orange(roomMeta.name) + " at path " + orange(roomMeta.loadPath) + " ===");
			discardRoom();
			loadRoomInEditor(roomMeta.loadPath);
			loadAllTest.frame = Time.frameCount;
			if (loadAllTest.roomIndex == loadableRooms.Count)
			{
				UnityEngine.Debug.Log("=== DONE ===");
			}
		}
	}

	private bool isBlacklistedProp(PropID propId)
	{
		return false;
	}

	private bool isWhitelistedProp(Prop prop)
	{
		if (testPlaceTag != PropTag.NONE)
		{
			return prop.tags.Exists((PropTag tag) => tag == testPlaceTag);
		}
		return true;
	}

	private void startDragAndDrop(DragAndDropItem item)
	{
		if (item is PropDragAndDropItem propDragAndDropItem)
		{
			changeSelection(newPropSelection(propDragAndDropItem.propID));
		}
		else if (item is AudioAssetDragAndDropItem)
		{
			changeSelection(newPropSelection(new PropID
			{
				value = "Logics/Sound"
			}));
		}
		else if (item is ScriptAssetDragAndDropItem)
		{
			changeSelection(newPropSelection(new PropID
			{
				value = "Logics/Script"
			}));
		}
		else if (item is MaterialAssetDragAndDropItem materialAssetDragAndDropItem)
		{
			applyMaterialToRawImage(getCustomMaterial(materialAssetDragAndDropItem.materialAsset.path), virtualCursorUI.MaterialDragIcon, useMask: false);
		}
		dragAndDropItem = item;
		closeTopUI();
	}

	private void updateDragAndDrop()
	{
		if (isDragAndDrop && dragAndDropItem is MaterialAssetDragAndDropItem item)
		{
			updateMaterialAssetDragAndDrop(item);
		}
		void updateMaterialAssetDragAndDrop(MaterialAssetDragAndDropItem materialAssetDragAndDropItem)
		{
			MaterialAsset materialAsset = materialAssetDragAndDropItem.materialAsset;
			int num = -1;
			Renderer renderer = null;
			if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out var hitInfo, float.PositiveInfinity, CAMERA_RAYCAST_MASK))
			{
				(renderer, num) = getMaterialFromRaycast(hitInfo);
			}
			materialAssetDragAndDropItem.revertMaterialChange();
			if (!(renderer == null) && num >= 0)
			{
				UnityEngine.Material material = renderer.sharedMaterials[num];
				UnityEngine.Material customMaterial = getCustomMaterial(materialAsset.path);
				if (!(material == customMaterial))
				{
					materialAssetDragAndDropItem.materialSwap = new MaterialAssetDragAndDropItem.MaterialSwap
					{
						renderer = renderer,
						materialIndex = num,
						originalMaterial = material
					};
					UnityEngine.Material[] sharedMaterials = renderer.sharedMaterials;
					sharedMaterials[num] = customMaterial;
					renderer.sharedMaterials = sharedMaterials;
				}
			}
		}
	}

	private void endDragAndDrop()
	{
		if (dragAndDropItem is PropDragAndDropItem)
		{
			placeSelectedProp();
		}
		else if (dragAndDropItem is AudioAssetDragAndDropItem audioAssetDragAndDropItem)
		{
			int stackIndex = undo.stackIndex;
			PropInstance instance = placeSelectedProp();
			var (prev, propData) = prepChangeData(instance);
			getSoundData(propData).path = audioAssetDragAndDropItem.audioAsset.nameWithExtension;
			undo.push(actionChangeProp(prev, propData, "Change Sound Path"));
			undo.linkLast(undo.stackIndex - stackIndex);
		}
		else if (dragAndDropItem is ScriptAssetDragAndDropItem scriptAssetDragAndDropItem)
		{
			int stackIndex2 = undo.stackIndex;
			PropInstance instance2 = placeSelectedProp();
			var (prev2, propData2) = prepChangeData(instance2);
			getScriptComponentData(propData2).scriptLocation = scriptAssetDragAndDropItem.scriptAsset.name;
			undo.push(actionChangeProp(prev2, propData2, "Change Script Path"));
			undo.linkLast(undo.stackIndex - stackIndex2);
		}
		else if (dragAndDropItem is MaterialAssetDragAndDropItem materialItem)
		{
			endMaterialAssetDragAndDrop(materialItem);
		}
		cancelDragAndDrop();
		void endMaterialAssetDragAndDrop(MaterialAssetDragAndDropItem materialAssetDragAndDropItem)
		{
			MaterialAssetDragAndDropItem.MaterialSwap materialSwap = materialAssetDragAndDropItem.materialSwap;
			materialAssetDragAndDropItem.revertMaterialChange();
			if (materialSwap != null)
			{
				string rendererPath = getRendererPath(materialSwap.renderer);
				PropInstance componentInParent = materialSwap.renderer.GetComponentInParent<PropInstance>();
				List<MaterialLocation> materialLocations = getMaterialLocations(loadPrefab(getEditorContext(), getPropPrefabID(componentInParent)).GetComponent<PropInstance>());
				MaterialLocation materialLocation = null;
				foreach (MaterialLocation item in materialLocations)
				{
					if (!(item.path != rendererPath) && item.index == materialSwap.materialIndex)
					{
						materialLocation = item;
						break;
					}
				}
				if (materialLocation == null)
				{
					UnityEngine.Debug.LogError("Could not change material as material location was not found!");
				}
				else
				{
					var (prev3, propData3) = prepChangeData(componentInParent);
					if (materialToMaterialAssetPath.ContainsKey(materialSwap.originalMaterial))
					{
						foreach (MaterialSwapData materialSwap2 in propData3.materialSwaps)
						{
							if (!(materialSwap2.path != materialLocation.path) && materialSwap2.index == materialLocation.index)
							{
								materialSwap2.asset = materialAssetDragAndDropItem.materialAsset.nameWithExtension;
								break;
							}
						}
					}
					else
					{
						propData3.materialSwaps.Add(new MaterialSwapData
						{
							asset = materialAssetDragAndDropItem.materialAsset.nameWithExtension,
							path = materialLocation.path,
							index = materialLocation.index
						});
					}
					undo.push(actionChangeProp(prev3, propData3, "Swap Material via Drag & Drop"));
				}
			}
		}
	}

	private void cancelDragAndDrop()
	{
		if (dragAndDropItem is PropDragAndDropItem)
		{
			changeSelection(newEmptySelection());
		}
		else if (dragAndDropItem is AudioAssetDragAndDropItem)
		{
			changeSelection(newEmptySelection());
		}
		else if (dragAndDropItem is ScriptAssetDragAndDropItem)
		{
			changeSelection(newEmptySelection());
		}
		else if (dragAndDropItem is MaterialAssetDragAndDropItem materialAssetDragAndDropItem)
		{
			materialAssetDragAndDropItem.revertMaterialChange();
		}
		dragAndDropItem = null;
	}

	private void definePropDragAndDropButton(PropButtonUI propButton, PropID propID)
	{
		defineDragAndDropUI(propButton.root.gameObject, delegate
		{
			startDragAndDrop(new PropDragAndDropItem(propID));
		}, endDragAndDrop, cancelDragAndDrop);
	}

	private void defineAudioDragAndDropButton(AudioItemUI audioItem, AudioAsset audioAsset)
	{
		defineDragAndDropUI(audioItem.root.gameObject, delegate
		{
			startDragAndDrop(new AudioAssetDragAndDropItem(audioAsset));
		}, endDragAndDrop, cancelDragAndDrop);
	}

	private void defineScriptDragAndDropButton(ScriptItemUI scriptItem, ScriptAsset scriptAsset)
	{
		defineDragAndDropUI(scriptItem.root.gameObject, delegate
		{
			startDragAndDrop(new ScriptAssetDragAndDropItem(scriptAsset));
		}, endDragAndDrop, cancelDragAndDrop);
	}

	private void defineMaterialDragAndDropButton(MaterialItemUI materialItem, MaterialAsset materialAsset)
	{
		defineDragAndDropUI(materialItem.root.gameObject, delegate
		{
			startDragAndDrop(new MaterialAssetDragAndDropItem(materialAsset));
		}, endDragAndDrop, cancelDragAndDrop);
	}

	private static void defineDragAndDropUI(GameObject ui, Action onDragStart, Action onDragEnd, Action onDragCancel)
	{
		bool isDown = false;
		bool isDrag = false;
		addTriggerEntry(ui, EventTriggerType.PointerDown).callback.AddListener(delegate
		{
			isDown = true;
			isDrag = false;
		});
		addTriggerEntry(ui, EventTriggerType.PointerExit).callback.AddListener(delegate
		{
			isDrag = isDown;
			if (isDrag)
			{
				onDragStart();
			}
		});
		addTriggerEntry(ui, EventTriggerType.PointerEnter).callback.AddListener(delegate
		{
			if (isDrag)
			{
				onDragCancel();
				isDrag = false;
			}
		});
		addTriggerEntry(ui, EventTriggerType.PointerUp).callback.AddListener(delegate
		{
			if (isDrag)
			{
				onDragEnd();
			}
			isDown = false;
			isDrag = false;
		});
	}

	private void initGizmoAxis()
	{
		foreach (CustomPass customPass in gizmoAxisCustomPassVolume.customPasses)
		{
			if (customPass is GizmoAxisCustomPass gizmoAxisCustomPass)
			{
				this.gizmoAxisCustomPass = gizmoAxisCustomPass;
				break;
			}
		}
		this.gizmoAxisCustomPass.gizmoCamera = gizmoAxisRenderer.controller.gizmoCamera;
		this.gizmoAxisCustomPass.gizmoRenderers = new List<Renderer>(gizmoAxisRenderer.controller.gizmoRenderers);
		TextMesh[] gizmoLabels = gizmoAxisRenderer.controller.gizmoLabels;
		foreach (TextMesh textMesh in gizmoLabels)
		{
			this.gizmoAxisCustomPass.gizmoRenderers.Add(textMesh.GetComponent<Renderer>());
		}
		RawImage gizmoImage = this.gizmoAxisCustomPass.gizmoImage;
		Color white = Color.white;
		float? a = 0f;
		gizmoImage.color = white.With(null, null, null, a);
	}

	private void updateGizmoAxis()
	{
		bool flag = special == null || special.mode == EditorMode.Pivot;
		sceneGizmoUI.enabled = flag;
		gizmoAxisCustomPass.enabled = flag;
		Vector2 anchoredPosition = (propertiesUI.root.enabled ? new Vector2(-400f, 0f) : Vector2.zero);
		((RectTransform)gizmoAxisRenderer.transform).anchoredPosition = anchoredPosition;
		transformUI.root.enabled = isPropInstanceSelected() || isSpecialMode(EditorMode.Pivot);
		transformUI.ToolsBackground.gameObject.SetActive(!isPropSelected());
	}

	public void OnGizmoComponentClicked(GizmoComponent component)
	{
		switch (component)
		{
		case GizmoComponent.Center:
			switchCameraProjection();
			break;
		case GizmoComponent.XNegative:
			setCameraRotation(Vector3.right);
			break;
		case GizmoComponent.XPositive:
			setCameraRotation(-Vector3.right);
			break;
		case GizmoComponent.YNegative:
			setCameraRotation(Vector3.up);
			break;
		case GizmoComponent.YPositive:
			setCameraRotation(-Vector3.up);
			break;
		case GizmoComponent.ZNegative:
			setCameraRotation(Vector3.forward);
			break;
		default:
			setCameraRotation(-Vector3.forward);
			break;
		}
	}

	private void initGrid()
	{
		foreach (CustomPass customPass in gridCustomPassVolume.customPasses)
		{
			if (customPass is GridCustomPass gridCustomPass)
			{
				this.gridCustomPass = gridCustomPass;
				break;
			}
		}
	}

	private void updateGrid()
	{
		PlayerSave.Settings settings = PlayerSave.getSettings();
		if (polygonToolUI.root.enabled)
		{
			polygonTool.snapStep = new Vector2(gridCustomPass.scale, gridCustomPass.scale);
			gridCustomPass.enabled = true;
		}
		else if (isSpecialMode(EditorMode.Playmode) || isSpecialMode(EditorMode.Screenshot))
		{
			gridCustomPass.enabled = false;
		}
		else
		{
			gridCustomPass.enabled = settings.re.gridEnabled;
			gridCustomPass.planeSystem = settings.re.gridPlane;
		}
		Color gridColor = settings.re.gridColor;
		GridCustomPass.Grid grid = gridCustomPass.grids[0];
		Color color = gridColor;
		float? a = UnityUtils.map(0f, 1f, 0f, 0.25f, gridColor.a);
		grid.color = color.With(null, null, null, a);
		GridCustomPass.Grid grid2 = gridCustomPass.grids[1];
		Color color2 = gridColor;
		a = UnityUtils.map(0f, 1f, 0f, 0.5f, gridColor.a);
		grid2.color = color2.With(null, null, null, a);
		gridCustomPass.grids[2].color = gridColor;
		float gridSize = settings.re.gridSize;
		gridCustomPass.scale = gridSize;
		transformGizmo.positionHandle.GridSize = gridSize;
	}

	private Vector3 handleSelectedObjectGridMovement(bool ctrlPressed, bool commandPressed)
	{
		Vector3 zero = Vector3.zero;
		float scale = gridCustomPass.scale;
		Vector3 forward = Vector3.forward;
		Vector3 forward2 = mainCam.transform.forward;
		forward2.y = 0f;
		float num = Vector3.SignedAngle(forward, forward2, Vector3.up);
		int num2;
		if (num > -45f)
		{
			if (!(num <= 45f))
			{
				if (num >= 135f)
				{
					goto IL_0076;
				}
				goto IL_007b;
			}
			num2 = 1;
		}
		else
		{
			if (!(num > -135f))
			{
				if (num < -135f)
				{
					goto IL_0076;
				}
				goto IL_007b;
			}
			num2 = 2;
		}
		goto IL_007e;
		IL_0076:
		num2 = 3;
		goto IL_007e;
		IL_007e:
		int num3 = num2;
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			switch (num3)
			{
			case 1:
				zero.x = 0f - scale;
				break;
			case 2:
				zero.z = 0f - scale;
				break;
			case 3:
				zero.x = scale;
				break;
			default:
				zero.z = scale;
				break;
			}
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			switch (num3)
			{
			case 1:
				zero.x = scale;
				break;
			case 2:
				zero.z = scale;
				break;
			case 3:
				zero.x = 0f - scale;
				break;
			default:
				zero.z = 0f - scale;
				break;
			}
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			if (ctrlPressed || commandPressed)
			{
				zero.y = scale;
			}
			else
			{
				switch (num3)
				{
				case 1:
					zero.z = scale;
					break;
				case 2:
					zero.x = 0f - scale;
					break;
				case 3:
					zero.z = 0f - scale;
					break;
				default:
					zero.x = scale;
					break;
				}
			}
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			if (ctrlPressed || commandPressed)
			{
				zero.y = 0f - scale;
			}
			else
			{
				switch (num3)
				{
				case 1:
					zero.z = 0f - scale;
					break;
				case 2:
					zero.x = scale;
					break;
				case 3:
					zero.z = scale;
					break;
				default:
					zero.x = 0f - scale;
					break;
				}
			}
		}
		return zero;
		IL_007b:
		num2 = 4;
		goto IL_007e;
	}

	private void hideSelectedProps(bool includeChildProps)
	{
		HashSet<InstanceID> hashSet = new HashSet<InstanceID>(hiddenPropInstanceIDs);
		foreach (PropInstance selectedInstance in getSelectedInstances(includeChildProps))
		{
			hashSet.Add(selectedInstance.ID);
		}
		EditorSelection newSelection = selection;
		newSelection.propInstanceIDs = new HashSet<InstanceID>();
		List<EditorAction> list = new List<EditorAction>();
		list.Add(actionChangeSelection(newSelection));
		list.Add(actionChangeHidden(hashSet));
		undo.push(list.ToArray());
	}

	private void hideNonSelectedProps(bool includeChildProps)
	{
		HashSet<InstanceID> hashSet = new HashSet<InstanceID>();
		foreach (PropInstance selectedInstance in getSelectedInstances(includeChildProps))
		{
			hashSet.Add(selectedInstance.ID);
		}
		HashSet<InstanceID> hashSet2 = new HashSet<InstanceID>();
		foreach (InstanceID key in propInstances.Keys)
		{
			if (!hashSet.Contains(key))
			{
				hashSet2.Add(key);
			}
		}
		undo.push(actionChangeHidden(hashSet2));
	}

	private void unhideProps()
	{
		undo.push(actionChangeHidden(new HashSet<InstanceID>()));
	}

	private void changeHidden(HashSet<InstanceID> toHide)
	{
		foreach (InstanceID hiddenPropInstanceID in hiddenPropInstanceIDs)
		{
			PropInstance instanceByID = getInstanceByID(hiddenPropInstanceID);
			if (!(instanceByID == null))
			{
				GameObject gameObject = instanceByID.gameObject;
				gameObject.layer = hiddenPropLayerMemory[gameObject];
				traverseAllNonPropChildren(gameObject.transform, delegate(GameObject child)
				{
					child.layer = hiddenPropLayerMemory[child];
				});
			}
		}
		hiddenPropLayerMemory.Clear();
		foreach (InstanceID item in toHide)
		{
			GameObject gameObject2 = getInstanceByID(item).gameObject;
			hiddenPropLayerMemory[gameObject2] = gameObject2.layer;
			gameObject2.layer = CAMERA_HIDE_LAYER;
			traverseAllNonPropChildren(gameObject2.transform, delegate(GameObject child)
			{
				hiddenPropLayerMemory[child] = child.layer;
				child.layer = CAMERA_HIDE_LAYER;
			});
		}
		hiddenPropInstanceIDs.Clear();
		hiddenPropInstanceIDs.UnionWith(toHide);
		refresh.editorUI = true;
		refresh.buildingUI = true;
		refresh.hiddenPropsMessageUI = true;
		static void traverseAllNonPropChildren(Transform root, Action<GameObject> action)
		{
			foreach (Transform item2 in root.transform)
			{
				if (!item2.TryGetComponent<PropInstance>(out var _))
				{
					action(item2.gameObject);
					traverseAllNonPropChildren(item2, action);
				}
			}
		}
	}

	private void showAllHiddenProps()
	{
		foreach (var (gameObject2, layer) in hiddenPropLayerMemory)
		{
			if (!(gameObject2 == null))
			{
				gameObject2.layer = layer;
			}
		}
	}

	private void restoreHiddenProps()
	{
		foreach (GameObject key in hiddenPropLayerMemory.Keys)
		{
			if (!(key == null))
			{
				key.layer = CAMERA_HIDE_LAYER;
			}
		}
	}

	private bool isPropHidden(PropInstance instance)
	{
		return isPropHidden(instance.ID);
	}

	private bool isPropHidden(InstanceID id)
	{
		return hiddenPropInstanceIDs.Contains(id);
	}

	private bool isPropHiddenAny<T>() where T : Component
	{
		foreach (PropInstance value in propInstances.Values)
		{
			if (!(value == null) && !(value.GetComponent<T>() == null) && hiddenPropInstanceIDs.Contains(value.ID))
			{
				return true;
			}
		}
		return false;
	}

	private void toggleHiddenOf<T>(bool shouldShow, bool includeChildProps) where T : Component
	{
		HashSet<InstanceID> hashSet = new HashSet<InstanceID>(hiddenPropInstanceIDs);
		if (shouldShow)
		{
			HashSet<InstanceID> hashSet2 = new HashSet<InstanceID>();
			foreach (InstanceID item in hashSet)
			{
				PropInstance instanceByID = getInstanceByID(item);
				if (instanceByID == null || instanceByID.GetComponent<T>() == null)
				{
					continue;
				}
				if (includeChildProps)
				{
					List<PropInstance> list = new List<PropInstance>();
					getChildPropInstances(instanceByID, list);
					foreach (PropInstance item2 in list)
					{
						hashSet2.Add(item2.ID);
					}
				}
				else
				{
					hashSet2.Add(instanceByID.ID);
				}
			}
			hashSet.ExceptWith(hashSet2);
		}
		else
		{
			foreach (PropInstance value in propInstances.Values)
			{
				if (!(value == null) && !(value.GetComponent<T>() == null))
				{
					hashSet.Add(value.ID);
				}
			}
			List<PropInstance> list2 = new List<PropInstance>();
			foreach (InstanceID item3 in hashSet)
			{
				PropInstance instanceByID2 = getInstanceByID(item3);
				if (includeChildProps)
				{
					getChildPropInstances(instanceByID2, list2);
				}
				else
				{
					list2.Add(instanceByID2);
				}
			}
			foreach (PropInstance item4 in list2)
			{
				hashSet.Add(item4.ID);
			}
		}
		undo.push(actionChangeHidden(hashSet));
	}

	private void refreshHiddenPropsMessage()
	{
		int num = (isSelectedSingle<Polygon>() ? 70 : 10);
		editorUI.HiddenPropsPopup.rectT().anchoredPosition3D = new Vector3(0f, num, 0f);
		if (hiddenPropInstanceIDs.Count <= 0)
		{
			editorUI.HiddenPropsPopup.gameObject.SetActive(value: false);
			return;
		}
		string keyBindingAsString = menuOptions.getKeyBindingAsString(KeyBindingAction.EditorUnhideAll, "Ctrl + H");
		string arg = ((hiddenPropInstanceIDs.Count == 1) ? string.Empty : "s");
		editorUI.HiddenPropsPopup_Text.text = $"{hiddenPropInstanceIDs.Count} prop{arg} hidden.\nUse <b>{keyBindingAsString}</b> to un-hide all.";
		editorUI.HiddenPropsPopup.gameObject.SetActive(value: true);
		editorUI.HiddenPropsPopup_Unhide.onClick.RemoveAllListeners();
		editorUI.HiddenPropsPopup_Unhide.onClick.AddListener(unhideProps);
	}

	private bool isHierarchyEnabled()
	{
		return false;
	}

	private bool isHierarchyOpened()
	{
		if (isHierarchyEnabled())
		{
			return currentTab.type == EditorTab.Building;
		}
		return false;
	}

	private void initHierarchy()
	{
	}

	private void updateHierarchy()
	{
		if (isHierarchyOpened())
		{
			if (currentTab.searchFilter != hierarchySearchFilterLastFrame)
			{
				refresh.editorUI = true;
			}
			hierarchySearchFilterLastFrame = currentTab.searchFilter;
		}
	}

	private void refreshHierarchy()
	{
		if (!isHierarchyOpened())
		{
			return;
		}
		int num = -1;
		PropInstance[] componentsInChildren = levelContainer.GetComponentsInChildren<PropInstance>();
		foreach (PropInstance instance in componentsInChildren)
		{
			int num2 = 0;
			PropInstance parent = instance.parent;
			while (parent != null)
			{
				num2++;
				parent = parent.parent;
			}
			if (num != -1 && num2 > num)
			{
				continue;
			}
			num = (instance.isExpandedInHierarchy ? (-1) : num2);
			if (!string.IsNullOrEmpty(currentTab.searchFilter))
			{
				bool num3 = instance.displayName.ToLower().Contains(currentTab.searchFilter.ToLower());
				bool flag = instance.scriptName.ToLower().Contains(currentTab.searchFilter.ToLower());
				if (!num3 && !flag)
				{
					continue;
				}
			}
			HierarchyItemUI hierarchyItemUI = UnityEngine.Object.Instantiate(editorUI.PropsRect_HierarchyItem, editorUI.PropsRect_Content);
			hierarchyItemUI.Indent.preferredWidth = num2 * 30;
			hierarchyItemUI.gameObject.SetActive(value: true);
			PropID propPrefabID = getPropPrefabID(instance);
			if (propSprites.TryGetValue(propPrefabID, out var value))
			{
				hierarchyItemUI.Button_PropIcon_Image.sprite = value;
			}
			hierarchyItemUI.Button.targetGraphic.color = (selection.propInstanceIDs.Contains(instance.ID) ? Color.white : Color.gray3);
			hierarchyItemUI.Button_PropName.text = (string.IsNullOrEmpty(instance.scriptName) ? instance.displayName : (instance.displayName + " " + instance.scriptName.Colored(Color.dodgerBlue)));
			hierarchyItemUI.Button.onClick.RemoveAllListeners();
			hierarchyItemUI.Button.onClick.AddListener(delegate
			{
				if (isCtrlPressed())
				{
					EditorSelection newSelection = selection;
					newSelection.propInstanceIDs = new HashSet<InstanceID>(selection.propInstanceIDs);
					if (!newSelection.propInstanceIDs.Add(instance.ID))
					{
						newSelection.propInstanceIDs.Remove(instance.ID);
					}
					undo.push(actionChangeSelection(newSelection));
				}
				else
				{
					EditorSelection newSelection2 = newInstanceSelection(instance.ID);
					if (isDifferentSelection(newSelection2))
					{
						undo.push(actionChangeSelection(newSelection2));
					}
				}
				closeTopUI();
			});
			bool flag2 = instance.children.Count > 0;
			hierarchyItemUI.Button_Collapse_Image.enabled = flag2;
			hierarchyItemUI.Button_Collapse.GetComponent<LayoutElement>().preferredWidth = (flag2 ? 30 : 10);
			if (!flag2)
			{
				continue;
			}
			hierarchyItemUI.Button_Collapse_Image.transform.rotation = (instance.isExpandedInHierarchy ? Quaternion.Euler(0f, 0f, -90f) : Quaternion.Euler(0f, 0f, 0f));
			hierarchyItemUI.Button_Collapse.onClick.RemoveAllListeners();
			hierarchyItemUI.Button_Collapse.onClick.AddListener(delegate
			{
				bool newState;
				if (isAltPressed())
				{
					newState = !instance.isExpandedInHierarchy;
					setStateRecursively(instance);
				}
				else
				{
					instance.isExpandedInHierarchy = !instance.isExpandedInHierarchy;
				}
				refresh.editorUI = true;
				void setStateRecursively(PropInstance propInstance)
				{
					propInstance.isExpandedInHierarchy = newState;
					foreach (PropInstance child in propInstance.children)
					{
						setStateRecursively(child);
					}
				}
			});
		}
	}

	private void addPropToHistory(PropID id)
	{
		int num = propHistory.IndexOf(id);
		if (num != 0)
		{
			if (num > 0)
			{
				propHistory.RemoveAt(num);
			}
			propHistory.Insert(0, id);
			if (propHistory.Count > 5)
			{
				propHistory.RemoveAt(5);
			}
			refreshPropHistoryUI();
		}
	}

	private void refreshPropHistoryUI()
	{
		for (int num = editorUI.HistoryProps.transform.childCount - 1; num >= 0; num--)
		{
			GameObject gameObject = editorUI.HistoryProps.transform.GetChild(num).gameObject;
			if (gameObject.activeSelf)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		foreach (PropID propID in propHistory)
		{
			PropButtonUI propButtonUI = UnityEngine.Object.Instantiate(editorUI.HistoryProps_PropButton, editorUI.HistoryProps.transform);
			propButtonUI.gameObject.SetActive(value: true);
			propButtonUI.root.onClick.AddListener(delegate
			{
				if (!(selection.propID == propID))
				{
					undo.push(actionChangeSelection(newPropSelection(propID)));
				}
			});
			PineUI.addButtonListeners(propButtonUI.root);
			definePropDragAndDropButton(propButtonUI, propID);
			propButtonUI.name = propID.value;
			if (propSprites.TryGetValue(propID, out var value))
			{
				propButtonUI.Icon.sprite = value;
			}
		}
	}

	private static LogicProp getLogicPropData(PropID logicPropID)
	{
		return logicPropID.value switch
		{
			"Logics/Activator" => new LogicProp("Used to activate, deactivate or toggle properties of an object.", LogicProp.Type.Gameplay), 
			"Logics/Clouds" => new LogicProp("Defines the clouds for this room.\nThere can be multiple clouds in a room, but only one can be active at a time.\nClouds are a visual effect that can be used to create atmosphere in the room.", LogicProp.Type.VisualAndAudio), 
			"Logics/Collider" => new LogicProp("Defines an invisible collision volume.\nItems throw into it will collide with it as if it were a wall.", LogicProp.Type.Gameplay), 
			"Logics/Delay" => new LogicProp("Delays any action by fixed amount of time.", LogicProp.Type.Gameplay), 
			"Logics/Display" => new LogicProp("Displays current values of a Lock (each value maps to an image in a sprite sheet).", LogicProp.Type.VisualAndAudio), 
			"Logics/Empty" => new LogicProp("A prop with appearance.", LogicProp.Type.Gameplay), 
			"Logics/Finish" => new LogicProp("Prop that, when targeted, will make it so that players have beaten the room.", LogicProp.Type.Gameplay), 
			"Logics/Fog" => new LogicProp("Defines the fog settings for this room.\nFog is a visual effect that can be used to create atmosphere in the room.", LogicProp.Type.VisualAndAudio), 
			"Logics/ItemRespawner" => new LogicProp("Component used to respawn important items so they don't get stuck in unreachable places.", LogicProp.Type.Gameplay), 
			"Logics/Ladder" => new LogicProp("Logic prop used to climb up and down.", LogicProp.Type.Movement), 
			"Logics/Light" => new LogicProp("Logic prop used to emit light in the scene.", LogicProp.Type.VisualAndAudio), 
			"Logics/Lock" => new LogicProp("Logic prop used to calculate a true or false value based on the current input and settings.", LogicProp.Type.Gameplay), 
			"Logics/Obstacle" => new LogicProp("Prop that blocks movement of a player.\nPlayer cannot walk through the obstacle.", LogicProp.Type.Movement), 
			"Logics/Ocean" => new LogicProp("Logic prop used to create Ocean in the scene.\nIts used for one infinite water instance.", LogicProp.Type.VisualAndAudio), 
			"Logics/OpenLink" => new LogicProp("Logic prop that allows you to open some link when targeted.", LogicProp.Type.Gameplay), 
			"Logics/PostProcessing" => new LogicProp("Defines the post processing effect for this room.\nThere can be multiple post processing effects in a room, but only one can be active at a time.", LogicProp.Type.VisualAndAudio), 
			"Logics/Puzzle" => new LogicProp("A prop that defines a puzzle in the room.\nUsed to generate Walkthroughes for your players.", LogicProp.Type.Gameplay), 
			"Logics/Roulette" => new LogicProp("A roulette is a prop that randomly selects a target from the list of targets.\nThe roulette can be used to create random events in the room.\nRoulette is synced in co-op.", LogicProp.Type.Gameplay), 
			"Logics/Script" => new LogicProp("Logic prop used to channel Lua code to interact with other props.\nFind the documentation and get help from the community on our official Pine Studio Discord!", LogicProp.Type.Gameplay), 
			"Logics/Setup" => new LogicProp("Allows you to trigger any action at the start of the room.", LogicProp.Type.Gameplay), 
			"Logics/Skybox" => new LogicProp("Defines the skybox for this room.\nThere can be multiple skyboxes in a room, but only one can be active at a time.\nSkybox is a visual effect that can be used to create atmosphere in the room.", LogicProp.Type.VisualAndAudio), 
			"Logics/Slot" => new LogicProp("Logic prop used to create areas that accept certain keys as inputs for unlocking.", LogicProp.Type.Gameplay), 
			"Logics/Sound" => new LogicProp("Defines a sound prop. Can be music or one shot sound.", LogicProp.Type.VisualAndAudio), 
			"Logics/SpawnPoint" => new LogicProp("Defines a point where a player will spawn when starting this room.", LogicProp.Type.Movement), 
			"Logics/Teleport" => new LogicProp("Defines where and who teleports to this position and/or rotation when activated.", LogicProp.Type.Movement), 
			"Logics/Test" => new LogicProp("Used only for testing properties (will not be in released build).", LogicProp.Type.Gameplay), 
			"Logics/Trigger" => new LogicProp("Logic prop used to determine when another prop or player enters/exits an area.", LogicProp.Type.Gameplay), 
			"Logics/Water" => new LogicProp("Logic prop used to create water in the scene.\nIts used for smaller water instances.", LogicProp.Type.VisualAndAudio), 
			"Logics/Text" => new LogicProp("Simple display of text in the world.", LogicProp.Type.VisualAndAudio), 
			_ => new LogicProp(string.Empty, LogicProp.Type.Gameplay), 
		};
	}

	private void initMaterials()
	{
		materialEditorUI.root.enabled = false;
	}

	private void updateMaterials()
	{
		if (!string.IsNullOrEmpty(roomDirPath) && !(customMaterialsRoomDirPath == roomDirPath))
		{
			customMaterialsRoomDirPath = roomDirPath;
			registerCustomMaterials(getEditorContext());
		}
	}

	private static void registerCustomMaterials(EditorContext editorContext)
	{
		UnityEngine.Debug.Log("registerCustomMaterials, Room Path: " + editorContext.roomDirPath);
		materialAssetPathToMaterial.Clear();
		materialToMaterialAssetPath.Clear();
		string[] files = Directory.GetFiles(editorContext.roomDirPath, "*.mat", SearchOption.TopDirectoryOnly);
		foreach (string text in files)
		{
			string text2 = File.ReadAllText(text);
			if (JsonUtility.FromJson<MaterialData>(text2) == null)
			{
				UnityEngine.Debug.LogError("Failed to parse material data from file: " + text + ", JSON:\n" + text2);
				continue;
			}
			string text3 = Path.ChangeExtension(text, ".es2mat");
			File.Move(text, text3);
			UnityEngine.Debug.Log("Renamed material asset file to new extension: " + text3);
		}
		files = Directory.GetFiles(editorContext.roomDirPath, "*.es2mat", SearchOption.TopDirectoryOnly);
		foreach (string text4 in files)
		{
			string text5 = File.ReadAllText(text4);
			MaterialData materialData = JsonUtility.FromJson<MaterialData>(text5);
			if (materialData == null)
			{
				UnityEngine.Debug.LogError("Failed to parse material data from file: " + text4 + ", JSON:\n" + text5);
				continue;
			}
			UnityEngine.Material prefabMaterial = getPrefabMaterial(materialData.sourcePrefabId, materialData.sourceMaterialPath, materialData.sourceMaterialIndex, editorContext);
			if (prefabMaterial == null)
			{
				string sourcePrefabId = materialData.sourcePrefabId;
				string sourceMaterialPath = materialData.sourceMaterialPath;
				int sourceMaterialIndex = materialData.sourceMaterialIndex;
				UnityEngine.Debug.LogError($"Could not find source material. Prefab: {sourcePrefabId} | Path: {sourceMaterialPath} | Index: {sourceMaterialIndex}");
			}
			else
			{
				UnityEngine.Material material = new UnityEngine.Material(prefabMaterial);
				applyMaterialData(materialData, material, editorContext);
				registerCustomMaterial(material, text4);
			}
		}
	}

	private static void registerCustomMaterial(UnityEngine.Material material, string materialAssetPath)
	{
		if (material == null)
		{
			UnityEngine.Debug.LogError("Material cannot be null.");
			return;
		}
		if (string.IsNullOrEmpty(materialAssetPath))
		{
			UnityEngine.Debug.LogError("Material asset ID cannot be null or empty.");
			return;
		}
		materialAssetPathToMaterial[materialAssetPath] = material;
		materialToMaterialAssetPath[material] = materialAssetPath;
	}

	private static UnityEngine.Material getPrefabMaterial(string prefabId, string materialPath, int materialIndex, EditorContext editorContext)
	{
		GameObject gameObject = loadPrefab(editorContext, new PropID
		{
			value = prefabId
		});
		if (gameObject == null)
		{
			UnityEngine.Debug.LogError("Could not find prefab for ID: " + prefabId);
			return null;
		}
		return getMaterialForTransform(gameObject.transform, materialPath, materialIndex);
	}

	private static void applyMaterialData(MaterialData materialData, UnityEngine.Material material, EditorContext editorContext)
	{
		if (materialData == null)
		{
			UnityEngine.Debug.LogError("Material data cannot be null");
			return;
		}
		if (material == null)
		{
			UnityEngine.Debug.LogError("Material cannot be null");
			return;
		}
		setMaterialSurfaceType(material, materialData.baseMapColor.a, materialData.transparencyBlendingMode);
		if (materialData.baseMapColor.a != 1f)
		{
			material.renderQueue = 3000 + materialData.transparencyRenderOffset;
		}
		GameObject gameObject = loadPrefab(editorContext, new PropID
		{
			value = materialData.sourcePrefabId
		});
		UnityEngine.Material prefabMaterial = getMaterialForTransform(gameObject.transform, materialData.sourceMaterialPath, materialData.sourceMaterialIndex);
		editorContext.refresh.unloadUnusedAssets = true;
		UnityEngine.Texture texture = getTextureForId(materialData.baseMapTexture, "_BaseColorMap");
		applyTextureProperty("_BaseColorMap", texture);
		material.SetColor("_BaseColor", materialData.baseMapColor);
		material.SetTextureScale("_BaseColorMap", materialData.textureScale);
		material.SetTextureOffset("_BaseColorMap", materialData.textureOffset);
		UnityEngine.Texture texture2 = getTextureForId(materialData.normalMapTexture, "_NormalMap");
		applyTextureProperty("_NormalMap", texture2);
		material.SetFloat("_NormalScale", materialData.normalMapScale);
		UnityEngine.Texture texture3 = getTextureForId(materialData.maskMapTexture, "_MaskMap");
		applyTextureProperty("_MaskMap", texture3);
		material.SetFloat("_MetallicRemapMin", materialData.metallicRemapMin);
		material.SetFloat("_MetallicRemapMax", materialData.metallicRemapMax);
		material.SetFloat("_Metallic", materialData.metallic);
		material.SetFloat("_SmoothnessRemapMin", materialData.smoothnessRemapMin);
		material.SetFloat("_SmoothnessRemapMax", materialData.smoothnessRemapMax);
		material.SetFloat("_Smoothness", materialData.smoothness);
		UnityEngine.Texture texture4 = getTextureForId(materialData.emissiveMapTexture, "_EmissiveColorMap");
		applyTextureProperty("_EmissiveColorMap", texture4);
		material.SetColor("_EmissiveColor", materialData.emissiveMapColor * materialData.emissiveMapScale);
		void applyTextureProperty(string propertyName, UnityEngine.Texture value)
		{
			if (!material.HasProperty(propertyName))
			{
				UnityEngine.Debug.LogError("Material '" + material.name + "' has no property '" + propertyName + "'.");
			}
			else
			{
				material.SetTexture(propertyName, value);
			}
		}
		UnityEngine.Texture getTextureForId(string textureId, string textureKey)
		{
			if (string.IsNullOrEmpty(textureId))
			{
				return prefabMaterial.GetTexture(textureKey);
			}
			string text = Path.Combine(editorContext.roomDirPath, textureId);
			if (!File.Exists(text))
			{
				UnityEngine.Debug.LogError("Material '" + material.name + "' error: Could not find texture '" + textureKey + "' at path '" + text + "'.");
				return editorContext.materialMissingTexture;
			}
			Texture2D texture5 = new Texture2D(2, 2, TextureFormat.RGBA32, mipChain: true);
			texture5.LoadImage(File.ReadAllBytes(text), markNonReadable: false);
			processTexture(ref texture5);
			return texture5;
		}
	}

	private static void setMaterialSurfaceType(UnityEngine.Material material, float alpha, MaterialData.BlendingMode blendingMode)
	{
		if (material == null)
		{
			UnityEngine.Debug.LogError("Material cannot be null.");
			return;
		}
		if (alpha == 1f)
		{
			material.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
			material.DisableKeyword("_ENABLE_FOG_ON_TRANSPARENT");
			material.renderQueue = 2000;
			material.SetFloat("_SurfaceType", 0f);
			material.SetFloat("_ZWrite", 1f);
			material.SetFloat("_ZTestDepthEqualForOpaque", 3f);
			material.SetFloat("_EnableBlendModePreserveSpecularLighting", 1f);
			setBlending(0f, BlendMode.One, BlendMode.Zero, BlendMode.Zero, BlendMode.One, BlendMode.Zero);
			return;
		}
		material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
		material.EnableKeyword("_ENABLE_FOG_ON_TRANSPARENT");
		material.renderQueue = 3000;
		material.SetFloat("_SurfaceType", 1f);
		material.SetFloat("_ZWrite", 0f);
		material.SetFloat("_ZTestDepthEqualForOpaque", 4f);
		material.SetFloat("_EnableBlendModePreserveSpecularLighting", (alpha != 0f) ? 1 : 0);
		switch (blendingMode)
		{
		case MaterialData.BlendingMode.Alpha:
			setBlending(0f, BlendMode.One, BlendMode.OneMinusSrcAlpha, BlendMode.OneMinusSrcAlpha, BlendMode.One, BlendMode.OneMinusSrcAlpha);
			break;
		case MaterialData.BlendingMode.Additive:
			setBlending(1f, BlendMode.One, BlendMode.One, BlendMode.OneMinusSrcAlpha, BlendMode.One, BlendMode.One);
			break;
		case MaterialData.BlendingMode.Multiply:
			setBlending(2f, BlendMode.DstColor, BlendMode.Zero, BlendMode.Zero, BlendMode.DstAlpha, BlendMode.Zero);
			break;
		}
		void setBlending(float blendMode, BlendMode srcBlend, BlendMode dstBlend, BlendMode dstBlend2, BlendMode alphaSrcBlend, BlendMode alphaDstBlend)
		{
			material.SetFloat("_BlendMode", blendMode);
			material.SetFloat("_SrcBlend", (float)srcBlend);
			material.SetFloat("_DstBlend", (float)dstBlend);
			material.SetFloat("_DstBlend2", (float)dstBlend2);
			material.SetFloat("_AlphaSrcBlend", (float)alphaSrcBlend);
			material.SetFloat("_AlphaDstBlend", (float)alphaDstBlend);
		}
	}

	private static void extractMaterialData(MaterialData materialData, UnityEngine.Material material)
	{
		materialData.baseMapColor = material.GetColor("_BaseColor");
		materialData.textureScale = material.GetTextureScale("_BaseColorMap");
		materialData.textureOffset = material.GetTextureOffset("_BaseColorMap");
		materialData.normalMapScale = material.GetFloat("_NormalScale");
		materialData.metallicRemapMin = material.GetFloat("_MetallicRemapMin");
		materialData.metallicRemapMax = material.GetFloat("_MetallicRemapMax");
		materialData.metallic = material.GetFloat("_Metallic");
		materialData.smoothnessRemapMin = material.GetFloat("_SmoothnessRemapMin");
		materialData.smoothnessRemapMax = material.GetFloat("_SmoothnessRemapMax");
		materialData.smoothness = material.GetFloat("_Smoothness");
		materialData.emissiveMapColor = material.GetColor("_EmissiveColor");
	}

	private static UnityEngine.Material getCustomMaterial(string materialAssetPath)
	{
		if (materialAssetPath == null)
		{
			UnityEngine.Debug.LogError("Material asset ID is null.");
			return null;
		}
		if (!materialAssetPathToMaterial.TryGetValue(materialAssetPath, out var value))
		{
			UnityEngine.Debug.LogError("Could not find custom material for ID: " + materialAssetPath);
			return null;
		}
		return value;
	}

	private void refreshMaterialsUI(PropInstance instance)
	{
		List<MaterialLocation> materialLocations = getMaterialLocations(instance);
		materialLocations.RemoveAll((MaterialLocation location) => location.material == null || !(location.material.shader.name == "HDRP/Lit"));
		if (materialLocations.Count == 0)
		{
			return;
		}
		InstanceUICache cache = getInstanceUICache(instance);
		bool isMaterialsShown = cache.isMaterialsShown;
		Action<bool> onFoldout = delegate(bool isShown)
		{
			cache.isMaterialsShown = isShown;
		};
		Color? color = null;
		defineHeaderProperty("Materials", "Defines the appearance of the prop.", isMaterialsShown, onFoldout, color);
		if (!cache.isMaterialsShown)
		{
			return;
		}
		Polygon component = instance.GetComponent<Polygon>();
		if (component != null && component.thickness <= 0f)
		{
			MaterialLocation item = materialLocations[0];
			materialLocations.Clear();
			materialLocations.Add(item);
		}
		for (int num = 0; num < materialLocations.Count; num++)
		{
			MaterialLocation materialLocation = materialLocations[num];
			UnityEngine.Material instanceMaterial = getMaterialForTransform(instance.transform, materialLocation.path, materialLocation.index);
			bool isCustomMaterial = materialToMaterialAssetPath.ContainsKey(instanceMaterial);
			string text = (isCustomMaterial ? Path.GetFileNameWithoutExtension(materialToMaterialAssetPath[instanceMaterial]) : $"Material {num}");
			if (!isCustomMaterial && component != null)
			{
				text = num switch
				{
					0 => "Front Face", 
					1 => "Side Face", 
					2 => "Back Face", 
					_ => $"Material {num}", 
				};
			}
			string label = text;
			string tooltip = $"Material at index {num}.";
			UnityEngine.Texture mainTexture = instanceMaterial.mainTexture;
			Action onTexturePressed = delegate
			{
				popupAssetBrowserSelectedAssetPath = (isCustomMaterial ? materialToMaterialAssetPath[instanceMaterial] : string.Empty);
				openAssetBrowserPopup<MaterialAsset>(delegate(UserAsset material)
				{
					var (prev, propData) = prepChangeData(instance);
					if (isCustomMaterial)
					{
						foreach (MaterialSwapData materialSwap in propData.materialSwaps)
						{
							if (!(materialSwap.path != materialLocation.path) && materialSwap.index == materialLocation.index)
							{
								materialSwap.asset = material.nameWithExtension;
								break;
							}
						}
					}
					else
					{
						propData.materialSwaps.Add(new MaterialSwapData
						{
							asset = material.nameWithExtension,
							path = materialLocation.path,
							index = materialLocation.index
						});
					}
					undo.push(actionChangeProp(prev, propData, "Create material swap"));
				});
			};
			Action onEditPressed = delegate
			{
				if (isCustomMaterial)
				{
					openMaterialEditor(materialToMaterialAssetPath[instanceMaterial]);
				}
				else
				{
					materialEditorNewMaterialInstance = instance;
					materialEditorNewMaterialLocation = materialLocation;
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(getNextAvailableFilePath(Path.Combine(roomDirPath, "Material.es2mat")));
					openNewMaterialDialog("newMaterialFromInspector", "To modify Material properties, new Material asset will be created. What should it be named?", fileNameWithoutExtension);
				}
			};
			Color color2 = instanceMaterial.color;
			float? a = 1f;
			color = color2.With(null, null, null, a);
			defineTextureProperty(label, tooltip, mainTexture, onTexturePressed, onEditPressed, (!isCustomMaterial) ? null : ((Action)delegate
			{
				(PropData prev, PropData next) tuple = prepChangeData(instance);
				PropData item2 = tuple.prev;
				PropData item3 = tuple.next;
				for (int num2 = item3.materialSwaps.Count - 1; num2 >= 0; num2--)
				{
					MaterialSwapData materialSwapData = item3.materialSwaps[num2];
					if (!(materialSwapData.path != materialLocation.path) && materialSwapData.index == materialLocation.index)
					{
						item3.materialSwaps.RemoveAt(num2);
					}
				}
				undo.push(actionChangeProp(item2, item3, "Revert material swap"));
			}), "", color);
		}
	}

	private void openNewMaterialDialog(string okButtonId, string message, string inputFieldText = "")
	{
		optionDialog.showNoTranslate("New Material", message, onFrameClick, new VisualControl(okButtonId, ControllerButtonActionType.UIConfirmPrimary, "%RoomEditor_OK%", VisualControl.ControlType.Button, VisualControl.Position.BotLeft), new VisualControl("newMaterialCancel", ControllerButtonActionType.UIConfirmPrimary, "%cancel%", VisualControl.ControlType.Button, VisualControl.Position.BotRight));
		optionDialog.useInputField(okButtonId, inputFieldText);
	}

	private void createDefaultMaterialAsset(string materialName, bool openMaterialEditorAfterCreation)
	{
		UnityEngine.Material materialForTransform = getMaterialForTransform(loadPrefab(getEditorContext(), new PropID
		{
			value = "Primitives/Cube"
		}).transform, "", 0);
		MaterialData materialData = new MaterialData
		{
			sourcePrefabId = "Primitives/Cube",
			sourceMaterialPath = "",
			sourceMaterialIndex = 0
		};
		extractMaterialData(materialData, materialForTransform);
		string text = Path.Combine(roomDirPath, materialName + ".es2mat");
		File.WriteAllText(text, JsonUtility.ToJson(materialData, prettyPrint: true));
		refresh.userAssets = true;
		registerCustomMaterial(new UnityEngine.Material(materialForTransform)
		{
			name = materialName
		}, text);
		if (openMaterialEditorAfterCreation)
		{
			openMaterialEditor(text);
		}
	}

	private void createCopyMaterialAsset(string materialName)
	{
		MaterialData materialData = new MaterialData
		{
			sourcePrefabId = getPropPrefabID(materialEditorNewMaterialInstance).value,
			sourceMaterialPath = materialEditorNewMaterialLocation.path,
			sourceMaterialIndex = materialEditorNewMaterialLocation.index
		};
		extractMaterialData(materialData, materialEditorNewMaterialLocation.material);
		string text = Path.Combine(roomDirPath, materialName + ".es2mat");
		File.WriteAllText(text, JsonUtility.ToJson(materialData, prettyPrint: true));
		refresh.userAssets = true;
		registerCustomMaterial(new UnityEngine.Material(materialEditorNewMaterialLocation.material)
		{
			name = materialName
		}, text);
		openMaterialEditor(text);
		var (prev, propData) = prepChangeData(materialEditorNewMaterialInstance);
		propData.materialSwaps.Add(new MaterialSwapData
		{
			asset = Path.GetFileName(text),
			path = materialEditorNewMaterialLocation.path,
			index = materialEditorNewMaterialLocation.index
		});
		undo.push(actionChangeProp(prev, propData, "Create material swap"));
	}

	private void createDuplicateMaterialAsset(string materialName)
	{
		closeMaterialEditor();
		string text = Path.Combine(roomDirPath, materialName + ".es2mat");
		File.WriteAllText(text, JsonUtility.ToJson(materialEditorData, prettyPrint: true));
		refresh.userAssets = true;
		registerCustomMaterial(new UnityEngine.Material(getCustomMaterial(materialEditorAssetPath))
		{
			name = materialName
		}, text);
		openMaterialEditor(text);
	}

	private void openMaterialEditor(string materialAssetPath)
	{
		if (!File.Exists(materialAssetPath))
		{
			UnityEngine.Debug.LogError("Material asset file does not exist: " + materialAssetPath);
			return;
		}
		UnityEngine.Debug.Log("Opening material editor for asset: " + materialAssetPath);
		materialEditorData = JsonUtility.FromJson<MaterialData>(File.ReadAllText(materialAssetPath));
		materialEditorAssetPath = materialAssetPath;
		materialEditorUI.root.enabled = true;
		refreshMaterialEditor();
	}

	private void refreshMaterialEditor()
	{
		if (materialEditorAssetPath == null || materialEditorData == null)
		{
			return;
		}
		Transform parent = materialEditorUI.Properties.transform;
		inputFieldsNavigationDataMaterials.removeOnEndEditListeners();
		removePropertiesForParent(parent);
		startPropertySection(parent, UITooltip.Position.Right);
		UnityEngine.Material material = getCustomMaterial(materialEditorAssetPath);
		applyMaterialData(materialEditorData, material, getEditorContext());
		defineHeaderProperty("Base Map Settings", "", materialEditorBaseMapShown, delegate(bool isShown)
		{
			materialEditorBaseMapShown = isShown;
			refresh.materialEditorUI = true;
		}, tabsHeaderColor);
		if (materialEditorBaseMapShown)
		{
			defineSeparatorProperty(6f);
			defineMaterialTextureProperty("Base Map", "Specifies the base color of the Material.\nIt is also known as albedo, diffuse, or color texture.", "_BaseColorMap", () => materialEditorData.baseMapTexture, delegate(string textureAssetName)
			{
				materialEditorData.baseMapTexture = textureAssetName;
			});
			defineColorProperty("Base Map Color", "The main color tint applied to the surface.\nTints the Base Map texture. Set to white for no tint.", () => materialEditorData.baseMapColor, delegate(Color color)
			{
				materialEditorData.baseMapColor = color;
				material.SetColor("_BaseColor", color);
				setMaterialSurfaceType(material, color.a, materialEditorData.transparencyBlendingMode);
			}, delegate
			{
				refresh.materialEditorUI = true;
				refresh.propertiesUI = true;
				refresh.userAssets = true;
			});
			defineVector2Property("Texture Scale", "Defines how stretched the texture is on each axis.", () => materialEditorData.textureScale, delegate(Vector2 value)
			{
				materialEditorData.textureScale = value;
				material.SetTextureScale("_BaseColorMap", value);
			}, delegate
			{
			}, null, 0.01f);
			defineVector2Property("Texture Offset", "Defines how offset the texture is on each axis.", () => materialEditorData.textureOffset, delegate(Vector2 value)
			{
				materialEditorData.textureOffset = value;
				material.SetTextureOffset("_BaseColorMap", value);
			}, delegate
			{
			}, null, 0.01f);
			defineSeparatorProperty(6f);
		}
		defineHeaderProperty("Normal Map Settings", "", materialEditorNormalMapShown, delegate(bool isShown)
		{
			materialEditorNormalMapShown = isShown;
			refresh.materialEditorUI = true;
		}, tabsHeaderColor);
		if (materialEditorNormalMapShown)
		{
			defineSeparatorProperty(6f);
			defineMaterialTextureProperty("Normal Map", "Defines surface detail by simulating bumps and dents without changing geometry.", "_NormalMap", () => materialEditorData.normalMapTexture, delegate(string textureAssetName)
			{
				materialEditorData.normalMapTexture = textureAssetName;
			});
			defineFloatProperty("Normal Scale", "Controls how strongly the normal map affects the surface's appearance.", () => materialEditorData.normalMapScale, delegate(float value)
			{
				materialEditorData.normalMapScale = value;
				material.SetFloat("_NormalScale", value);
			}, delegate
			{
			}, null, 0f, float.PositiveInfinity, 0.01f);
			defineSeparatorProperty(6f);
		}
		defineHeaderProperty("Mask Map Settings", "", materialEditorMaskMapShown, delegate(bool isShown)
		{
			materialEditorMaskMapShown = isShown;
			refresh.materialEditorUI = true;
		}, tabsHeaderColor);
		if (materialEditorMaskMapShown)
		{
			defineSeparatorProperty(6f);
			if (defineMaterialTextureProperty("Mask Map", "Each color channel represents a different Material property.\n\nRed (R) channel represents Metallic.\nGreen (G) channel represents Ambient Occlusion (AO).\nBlue (B) channel represents Detail Mask.\nAlpha (A) channel represents Smoothness.", "_MaskMap", () => materialEditorData.maskMapTexture, delegate(string textureAssetName)
			{
				materialEditorData.maskMapTexture = textureAssetName;
			}) != null)
			{
				defineVector2Property("Metallic Remap", "Controls the remapping for the Metallic channel in the Mask Map.", () => new Vector2(materialEditorData.metallicRemapMin, materialEditorData.metallicRemapMax), delegate(Vector2 value)
				{
					if (value.x != materialEditorData.metallicRemapMin)
					{
						value.x = Mathf.Clamp(value.x, 0f, materialEditorData.metallicRemapMax);
					}
					else if (value.y != materialEditorData.metallicRemapMax)
					{
						value.y = Mathf.Clamp(value.y, materialEditorData.metallicRemapMin, 1f);
					}
					materialEditorData.metallicRemapMin = value.x;
					materialEditorData.metallicRemapMax = value.y;
					material.SetFloat("_MetallicRemapMin", value.x);
					material.SetFloat("_MetallicRemapMax", value.y);
				}, delegate
				{
				}, null, 0.01f);
				defineVector2Property("Smoothness Remap", "Controls the remapping for the Smoothness channel in the Mask Map.", () => new Vector2(materialEditorData.smoothnessRemapMin, materialEditorData.smoothnessRemapMax), delegate(Vector2 value)
				{
					if (value.x != materialEditorData.smoothnessRemapMin)
					{
						value.x = Mathf.Clamp(value.x, 0f, materialEditorData.smoothnessRemapMax);
					}
					else if (value.y != materialEditorData.smoothnessRemapMax)
					{
						value.y = Mathf.Clamp(value.y, materialEditorData.smoothnessRemapMin, 1f);
					}
					materialEditorData.smoothnessRemapMin = value.x;
					materialEditorData.smoothnessRemapMax = value.y;
					material.SetFloat("_SmoothnessRemapMin", value.x);
					material.SetFloat("_SmoothnessRemapMax", value.y);
				}, delegate
				{
				}, null, 0.01f);
			}
			else
			{
				defineFloatProperty("Metallic", "Controls the scale factor for the Material's metallic effect.", () => materialEditorData.metallic, delegate(float value)
				{
					materialEditorData.metallic = value;
					material.SetFloat("_Metallic", value);
				}, delegate
				{
				}, null, 0f, 1f, 0.01f);
				defineFloatProperty("Smoothness", "Controls the scale factor for the Material's smoothness.", () => materialEditorData.smoothness, delegate(float value)
				{
					materialEditorData.smoothness = value;
					material.SetFloat("_Smoothness", value);
				}, delegate
				{
				}, null, 0f, 1f, 0.01f);
			}
			defineSeparatorProperty(6f);
		}
		defineHeaderProperty("Emissive Map Settings", "", materialEditorEmissiveMapShown, delegate(bool isShown)
		{
			materialEditorEmissiveMapShown = isShown;
			refresh.materialEditorUI = true;
		}, tabsHeaderColor);
		if (materialEditorEmissiveMapShown)
		{
			defineSeparatorProperty(6f);
			defineMaterialTextureProperty("Emissive Map", "Specifies the emissive color of the Material.", "_EmissiveColorMap", () => materialEditorData.emissiveMapTexture, delegate(string textureAssetName)
			{
				materialEditorData.emissiveMapTexture = textureAssetName;
			});
			defineColorProperty("Emissive Map Color", "The color the surface emits light with, combined with the Emissive Map to define glow.", () => materialEditorData.emissiveMapColor, delegate(Color color)
			{
				materialEditorData.emissiveMapColor = color;
				material.SetColor("_EmissiveColor", color * materialEditorData.emissiveMapScale);
			}, delegate
			{
				refresh.propertiesUI = true;
				refresh.userAssets = true;
			});
			defineFloatProperty("Emissive Scale", "Controls the intensity of the emissive effect.", () => materialEditorData.emissiveMapScale, delegate(float value)
			{
				materialEditorData.emissiveMapScale = value;
				material.SetColor("_EmissiveColor", materialEditorData.emissiveMapColor * value);
			}, delegate
			{
			}, null, 0f, 5f, 0.1f);
			defineSeparatorProperty(6f);
		}
		if (materialEditorData.baseMapColor.a != 1f)
		{
			defineHeaderProperty("Transparency Settings", "", materialEditorTransparencyShown, delegate(bool isShown)
			{
				materialEditorTransparencyShown = isShown;
				refresh.materialEditorUI = true;
			}, tabsHeaderColor);
			if (materialEditorTransparencyShown)
			{
				defineIntProperty("Render Offset", "Allows you to sort rendering of transparent objects.", () => materialEditorData.transparencyRenderOffset, delegate(int value)
				{
					materialEditorData.transparencyRenderOffset = value;
					material.renderQueue = 3000 + value;
				}, delegate
				{
				}, null, 0, 100);
				defineEnumProperty("Blending Mode", "Defines how transparent areas of the material should blend with the background.\n\nAlpha - Material color and background color are mixed based on alpha channel.\nAdditive - Material color and background color are added, resulting in brighter color.\nMultiply - Material color and background color are multiplied, resulting in darker color.", (int)materialEditorData.transparencyBlendingMode, new List<string> { "Alpha", "Additive", "Multiply" }, delegate(int value)
				{
					materialEditorData.transparencyBlendingMode = (MaterialData.BlendingMode)value;
					setMaterialSurfaceType(material, materialEditorData.baseMapColor.a, (MaterialData.BlendingMode)value);
				});
			}
		}
		materialEditorUI.Header_Title.text = "Material <b>" + Path.GetFileNameWithoutExtension(materialEditorAssetPath) + "</b>";
		materialEditorUI.Background.onClick.RemoveAllListeners();
		materialEditorUI.Background.onClick.AddListener(closeMaterialEditor);
		materialEditorUI.DuplicateButton.onClick.RemoveAllListeners();
		materialEditorUI.DuplicateButton.onClick.AddListener(delegate
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(getNextAvailableFilePath(Path.Combine(roomDirPath, "Material.es2mat")));
			openNewMaterialDialog("newMaterialDuplicate", "What should the duplicated material be named?", fileNameWithoutExtension);
		});
		materialEditorUI.Header_QuitButton.onClick.RemoveAllListeners();
		materialEditorUI.Header_QuitButton.onClick.AddListener(closeMaterialEditor);
		inputFieldsNavigationDataMaterials.initNavigation(parent, "MaterialEditor");
		UnityEngine.Texture defineMaterialTextureProperty(string label, string tooltip, string textureKey, Func<string> materialDataTextureGetter, Action<string> materialDataTextureSetter)
		{
			if (material == null)
			{
				UnityEngine.Debug.LogError("Material is null!", parent);
				return null;
			}
			UnityEngine.Texture texture = material.GetTexture(textureKey);
			defineTextureProperty(label, tooltip, (texture != null) ? texture : materialEditorEmptyTextureIcon, delegate
			{
				popupAssetBrowserSelectedAssetPath = (string.IsNullOrEmpty(materialDataTextureGetter()) ? string.Empty : Path.Combine(roomDirPath, materialDataTextureGetter()));
				openAssetBrowserPopup<TextureAsset>(delegate(UserAsset textureAsset)
				{
					materialDataTextureSetter(textureAsset.nameWithExtension);
					refresh.materialEditorUI = true;
					refresh.propertiesUI = true;
					refresh.userAssets = true;
				});
			}, (texture == null) ? null : ((Action)delegate
			{
				if (string.IsNullOrEmpty(materialDataTextureGetter()))
				{
					string nextAvailableFilePath = getNextAvailableFilePath(Path.Combine(roomDirPath, texture.name + ".png"));
					Texture2D texture2 = copyUnreadableTexture((Texture2D)texture);
					saveTexture2DToFile(nextAvailableFilePath, texture2);
					materialDataTextureSetter(Path.GetFileName(nextAvailableFilePath));
					refresh.materialEditorUI = true;
					refresh.propertiesUI = true;
					refresh.userAssets = true;
				}
				openTextureEditor(Path.Combine(roomDirPath, materialDataTextureGetter()));
			}), string.IsNullOrEmpty(materialDataTextureGetter()) ? null : ((Action)delegate
			{
				materialDataTextureSetter(string.Empty);
				refresh.materialEditorUI = true;
				refresh.propertiesUI = true;
				refresh.userAssets = true;
			}), materialDataTextureGetter());
			return texture;
		}
	}

	private void closeMaterialEditor()
	{
		UnityEngine.Debug.Log("Closing material editor for asset: " + materialEditorAssetPath);
		string contents = JsonUtility.ToJson(materialEditorData, prettyPrint: true);
		File.WriteAllText(materialEditorAssetPath, contents);
		materialEditorUI.root.enabled = false;
	}

	private void handleTextureAssetsChanged(List<TextureAsset> changedTextureAssets)
	{
		if (changedTextureAssets.Count == 0)
		{
			return;
		}
		HashSet<string> hashSet = new HashSet<string>();
		foreach (TextureAsset changedTextureAsset in changedTextureAssets)
		{
			hashSet.Add(changedTextureAsset.nameWithExtension);
		}
		UnityEngine.Debug.Log(string.Format("Total texture assets changed: {0}. File names:\n{1}", changedTextureAssets.Count, string.Join("\n", hashSet)));
		string[] files = Directory.GetFiles(roomDirPath, "*.es2mat", SearchOption.TopDirectoryOnly);
		foreach (string text in files)
		{
			string text2 = File.ReadAllText(text);
			MaterialData materialData = JsonUtility.FromJson<MaterialData>(text2);
			if (materialData == null)
			{
				UnityEngine.Debug.LogError("Failed to parse material data from file: " + text + ", JSON:\n" + text2);
			}
			else if (hashSet.Contains(materialData.baseMapTexture) || hashSet.Contains(materialData.normalMapTexture) || hashSet.Contains(materialData.maskMapTexture) || hashSet.Contains(materialData.emissiveMapTexture))
			{
				applyMaterialData(materialData, getCustomMaterial(text), getEditorContext());
			}
		}
		refresh.materialEditorUI = true;
		refresh.propertiesUI = true;
	}

	private void handleMaterialAssetChanged(MaterialAsset materialAsset)
	{
		if (wasOnApplicationFocus)
		{
			UnityEngine.Debug.Log("Material asset changed: " + materialAsset.nameWithExtension);
			MaterialData materialData = JsonUtility.FromJson<MaterialData>(File.ReadAllText(materialAsset.path));
			UnityEngine.Material customMaterial = getCustomMaterial(materialAsset.path);
			applyMaterialData(materialData, customMaterial, getEditorContext());
		}
	}

	private static UnityEngine.Material getMaterialForTransform(Transform transform, string path, int index)
	{
		Transform transform2 = transform.Find(path);
		if (transform2 == null || !transform2.TryGetComponent<Renderer>(out var component))
		{
			return null;
		}
		UnityEngine.Material[] sharedMaterials = component.sharedMaterials;
		if (index < 0 || index >= sharedMaterials.Length)
		{
			return null;
		}
		return sharedMaterials[index];
	}

	private static string getRendererPath(Renderer renderer)
	{
		if (renderer == null)
		{
			UnityEngine.Debug.LogError("Renderer should not be null!");
			return string.Empty;
		}
		PropInstance componentInParent = renderer.transform.GetComponentInParent<PropInstance>();
		if (componentInParent == null)
		{
			UnityEngine.Debug.LogError("Renderer does not have parent prop instance!");
			return string.Empty;
		}
		Transform parent = renderer.transform;
		Transform transform = componentInParent.transform;
		if (parent == transform)
		{
			return "";
		}
		string text = parent.name;
		while (parent != null)
		{
			parent = parent.parent;
			if (parent == transform)
			{
				break;
			}
			text = parent.name + "/" + text;
		}
		return text;
	}

	private static (Renderer renderer, int materialIndex) getMaterialFromRaycast(RaycastHit hit)
	{
		Renderer renderer = getFirstValidRenderer(hit.transform);
		if (renderer == null)
		{
			return (renderer: null, materialIndex: -1);
		}
		UnityEngine.Material[] sharedMaterials = renderer.sharedMaterials;
		if (sharedMaterials.Length == 0)
		{
			return (renderer: renderer, materialIndex: -1);
		}
		if (sharedMaterials.Length == 1)
		{
			return (renderer: renderer, materialIndex: 0);
		}
		int triangleIndex = hit.triangleIndex;
		if (triangleIndex < 0)
		{
			return (renderer: renderer, materialIndex: 0);
		}
		MeshCollider meshCollider = hit.collider as MeshCollider;
		if (meshCollider == null)
		{
			return (renderer: renderer, materialIndex: -1);
		}
		UnityEngine.Mesh sharedMesh = meshCollider.sharedMesh;
		if (sharedMesh == null || !sharedMesh.isReadable)
		{
			return (renderer: renderer, materialIndex: -1);
		}
		for (int i = 0; i < sharedMesh.subMeshCount; i++)
		{
			SubMeshDescriptor subMesh = sharedMesh.GetSubMesh(i);
			int num = (subMesh.indexStart + subMesh.indexCount) / 3;
			if (triangleIndex < num)
			{
				return (renderer: renderer, materialIndex: i);
			}
		}
		return (renderer: renderer, materialIndex: -1);
		static Renderer getFirstValidRenderer(Transform transform)
		{
			PropInstance componentInParent = transform.GetComponentInParent<PropInstance>();
			if (componentInParent == null)
			{
				return null;
			}
			Transform transform2 = transform;
			while (transform2 != componentInParent.transform)
			{
				Renderer component = transform2.GetComponent<Renderer>();
				if (component != null && component.enabled)
				{
					return component;
				}
				transform2 = transform2.parent;
			}
			return transform2.GetComponent<Renderer>();
		}
	}

	private static List<MaterialLocation> getMaterialLocations(PropInstance instance)
	{
		List<MaterialLocation> materialLocations = new List<MaterialLocation>();
		if (instance == null)
		{
			UnityEngine.Debug.LogError("Cannot get material locations as prop instance is null.");
			return materialLocations;
		}
		iterate(instance.transform, "");
		return materialLocations;
		void iterate(Transform root, string path)
		{
			if (root.gameObject.TryGetComponent<Renderer>(out var component) && component.enabled)
			{
				for (int i = 0; i < component.sharedMaterials.Length; i++)
				{
					UnityEngine.Material material = component.sharedMaterials[i];
					if (!(material == null))
					{
						materialLocations.Add(new MaterialLocation
						{
							root = instance.transform,
							material = material,
							path = path,
							index = i
						});
					}
				}
			}
			for (int j = 0; j < root.childCount; j++)
			{
				Transform child = root.GetChild(j);
				if (!(child.GetComponent<PropInstance>() != null))
				{
					iterate(child, (path == "") ? child.name : (path + "/" + child.name));
				}
			}
		}
	}

	private static void revertPropInstanceToDefaultMaterials(PropInstance instance, EditorContext context)
	{
		GameObject gameObject = loadPrefab(context, getPropPrefabID(instance));
		foreach (MaterialLocation materialLocation in getMaterialLocations(gameObject.GetComponent<PropInstance>()))
		{
			UnityEngine.Material materialForTransform = getMaterialForTransform(instance.transform, materialLocation.path, materialLocation.index);
			if (materialToMaterialAssetPath.ContainsKey(materialForTransform))
			{
				applyMaterialAtLocation(getMaterialForTransform(gameObject.transform, materialLocation.path, materialLocation.index), new MaterialLocation
				{
					root = instance.transform,
					path = materialLocation.path,
					index = materialLocation.index
				});
			}
		}
		context.refresh.unloadUnusedAssets = true;
	}

	private static List<MaterialSwapData> getPropInstanceMaterialSwaps(PropInstance instance, EditorContext context)
	{
		List<MaterialSwapData> list = new List<MaterialSwapData>();
		if (instance.TryGetComponent<PropTags>(out var component) && component.tags.Contains(PropTag.Effects))
		{
			return list;
		}
		List<MaterialLocation> materialLocations = getMaterialLocations(loadPrefab(context, getPropPrefabID(instance)).GetComponent<PropInstance>());
		context.refresh.unloadUnusedAssets = true;
		foreach (MaterialLocation item in materialLocations)
		{
			UnityEngine.Material materialForTransform = getMaterialForTransform(instance.transform, item.path, item.index);
			string value;
			if (materialForTransform == null)
			{
				UnityEngine.Debug.LogError($"Could not find material for path '{item.path}' and index {item.index} on instance '{instance.name}'.", instance);
			}
			else if (materialToMaterialAssetPath.TryGetValue(materialForTransform, out value))
			{
				list.Add(new MaterialSwapData
				{
					asset = Path.GetFileName(value),
					path = item.path,
					index = item.index
				});
			}
		}
		return list;
	}

	private static void applyPropInstanceMaterialSwaps(PropInstance instance, List<MaterialSwapData> materialSwaps)
	{
		foreach (MaterialSwapData materialSwap in materialSwaps)
		{
			UnityEngine.Material material = null;
			foreach (var (path, material3) in materialAssetPathToMaterial)
			{
				if (!(Path.GetFileName(path) != materialSwap.asset))
				{
					material = material3;
					break;
				}
			}
			if (material == null)
			{
				UnityEngine.Debug.LogError("Could not find custom material for swap: " + materialSwap.asset);
				continue;
			}
			applyMaterialAtLocation(material, new MaterialLocation
			{
				root = instance.transform,
				path = materialSwap.path,
				index = materialSwap.index
			});
		}
	}

	private static void applyMaterialAtLocation(UnityEngine.Material material, MaterialLocation materialLocation)
	{
		if (material == null)
		{
			UnityEngine.Debug.LogError("Cannot apply material as it is null.");
			return;
		}
		if (materialLocation == null)
		{
			UnityEngine.Debug.LogError("Cannot apply material as location is null.");
			return;
		}
		if (materialLocation.root == null)
		{
			UnityEngine.Debug.LogError("Cannot apply material as location root transform is null.");
			return;
		}
		if (materialLocation.path == null)
		{
			UnityEngine.Debug.LogError("Cannot apply material as location path is null.");
			return;
		}
		Transform transform = materialLocation.root.Find(materialLocation.path);
		if (transform == null)
		{
			return;
		}
		Renderer component = transform.GetComponent<Renderer>();
		if (!(component == null))
		{
			UnityEngine.Material[] sharedMaterials = component.sharedMaterials;
			if (materialLocation.index >= 0 && materialLocation.index < sharedMaterials.Length)
			{
				sharedMaterials[materialLocation.index] = material;
				component.sharedMaterials = sharedMaterials;
			}
		}
	}

	private static void applyMaterialToRawImage(UnityEngine.Material material, RawImage rawImage, bool useMask)
	{
		if (material == null)
		{
			return;
		}
		rawImage.texture = material.mainTexture;
		if (!rawImage.material.name.EndsWith("_Instance"))
		{
			UnityEngine.Material material2 = new UnityEngine.Material(rawImage.material);
			material2.name += "_Instance";
			if (useMask)
			{
				material2.SetFloat("_UseUIMask", 1f);
				material2.SetInt("_StencilComp", 3);
			}
			else
			{
				material2.SetFloat("_UseUIMask", 0f);
				material2.SetInt("_StencilComp", 8);
			}
			rawImage.material = material2;
		}
		rawImage.material.color = material.color;
	}

	private void refreshNavMeshPreview()
	{
		initMeshIfNeeded(navMeshPreview);
		initMeshIfNeeded(navMeshCrouchPreview);
		PlayerSave.RoomEditorSettings.NavMeshVisibility navMeshVisibility = PlayerSave.getSettings().re.navMeshVisibility;
		if (navMeshVisibility == PlayerSave.RoomEditorSettings.NavMeshVisibility.ShowNone)
		{
			navMeshPreview.sharedMesh.Clear();
			navMeshCrouchPreview.sharedMesh.Clear();
			return;
		}
		Dictionary<NavMeshObstacle, bool> dictionary = new Dictionary<NavMeshObstacle, bool>();
		NavMeshObstacle[] componentsInChildren = levelContainer.GetComponentsInChildren<NavMeshObstacle>();
		foreach (NavMeshObstacle navMeshObstacle in componentsInChildren)
		{
			dictionary[navMeshObstacle] = navMeshObstacle.carveOnlyStationary;
			navMeshObstacle.carveOnlyStationary = false;
		}
		levelContainer.navMeshSurface.BuildNavMesh();
		levelContainer.navMeshSurfaceCrouch.BuildNavMesh();
		createTriangulation(navMeshPreview);
		createTriangulation(navMeshCrouchPreview);
		if (navMeshVisibility == PlayerSave.RoomEditorSettings.NavMeshVisibility.ShowOnlyStanding)
		{
			navMeshCrouchPreview.sharedMesh.Clear();
		}
		if (navMeshVisibility == PlayerSave.RoomEditorSettings.NavMeshVisibility.ShowOnlyCrouching)
		{
			navMeshPreview.sharedMesh.Clear();
		}
		foreach (KeyValuePair<NavMeshObstacle, bool> item in dictionary)
		{
			item.Deconstruct(out var key, out var value);
			NavMeshObstacle navMeshObstacle2 = key;
			bool carveOnlyStationary = value;
			navMeshObstacle2.carveOnlyStationary = carveOnlyStationary;
		}
		void createTriangulation(MeshFilter navPreview)
		{
			NavMeshSurface obj = ((navPreview == navMeshPreview) ? levelContainer.navMeshSurfaceCrouch : levelContainer.navMeshSurface);
			bool flag = obj.enabled;
			obj.enabled = false;
			NavMeshTriangulation navMeshTriangulation = NavMesh.CalculateTriangulation();
			navPreview.sharedMesh.Clear();
			navPreview.sharedMesh.SetVertices(navMeshTriangulation.vertices);
			navPreview.sharedMesh.SetIndices(navMeshTriangulation.indices, MeshTopology.Triangles, 0);
			obj.enabled = flag;
		}
		static void initMeshIfNeeded(MeshFilter navPreview)
		{
			if (!(navPreview.sharedMesh != null))
			{
				UnityEngine.Mesh mesh = new UnityEngine.Mesh();
				mesh.MarkDynamic();
				navPreview.sharedMesh = mesh;
			}
		}
	}

	private void initPolygonTool()
	{
		polygonTool.init();
		polygonTool.camera = mainCam;
		polygonTool.alwaysSnapToGrid = PlayerSave.getSettings().re.polygonSnapWhenEditing;
		PolygonTool obj = polygonTool;
		obj.onPolygonChanged = (PolygonTool.PolygonChanged)Delegate.Remove(obj.onPolygonChanged, new PolygonTool.PolygonChanged(handlePolygonChanged));
		PolygonTool obj2 = polygonTool;
		obj2.onPolygonChanged = (PolygonTool.PolygonChanged)Delegate.Combine(obj2.onPolygonChanged, new PolygonTool.PolygonChanged(handlePolygonChanged));
		PolygonTool obj3 = polygonTool;
		obj3.onPolygonCancel = (Action)Delegate.Remove(obj3.onPolygonCancel, new Action(handlePolygonCancel));
		PolygonTool obj4 = polygonTool;
		obj4.onPolygonCancel = (Action)Delegate.Combine(obj4.onPolygonCancel, new Action(handlePolygonCancel));
		PolygonTool obj5 = polygonTool;
		obj5.onFloorEdgePressed = (PolygonTool.FloorEdgePressed)Delegate.Remove(obj5.onFloorEdgePressed, new PolygonTool.FloorEdgePressed(handleFloorEdgePressed));
		PolygonTool obj6 = polygonTool;
		obj6.onFloorEdgePressed = (PolygonTool.FloorEdgePressed)Delegate.Combine(obj6.onFloorEdgePressed, new PolygonTool.FloorEdgePressed(handleFloorEdgePressed));
		polygonToolUI.root.enabled = false;
		polygonToolUI.SaveChanges.gameObject.SetActive(value: false);
		polygonToolUI.DiscardChanges.gameObject.SetActive(value: false);
	}

	private void handlePolygonChanged(Polygon polygon, List<Vector2> previousVertices, List<Hole> previousHoles)
	{
		(PropData prev, PropData next) tuple = prepChangeData(polygon.GetComponent<PropInstance>());
		PropData item = tuple.prev;
		PropData item2 = tuple.next;
		bool flag = polygon is Floor;
		getPolygonData(item, flag).vertices = previousVertices;
		getPolygonData(item, flag).holes = previousHoles;
		string text = string.Empty;
		int count = previousVertices.Count;
		int count2 = polygon.vertices.Count;
		if (count == count2)
		{
			text = "changed";
		}
		if (count < count2)
		{
			text = "added";
		}
		if (count > count2)
		{
			text = "removed";
		}
		List<EditorAction> list = new List<EditorAction>();
		list.Add(actionChangeProp(item, item2, (flag ? "Floor" : "Wall") + ".vertices " + text));
		Wall wall = null;
		Floor floor = polygon as Floor;
		PropData prevData;
		PropID wallPropID;
		GameObject wallPrefab;
		List<PropData> generatedWallProps;
		if ((object)floor != null)
		{
			Quaternion rotation = floor.transform.rotation;
			floor.transform.rotation = Quaternion.identity;
			List<Wall> walls = floor.getWalls();
			bool flag2 = true;
			if (count == count2)
			{
				bool flag3 = false;
				for (int i = 0; i < previousVertices.Count; i++)
				{
					Vector2 vector = previousVertices[i];
					Vector2 vector2 = previousVertices[(i + 1) % previousVertices.Count];
					Vector2 vector3 = floor.vertices[i];
					Vector2 vector4 = floor.vertices[(i + 1) % previousVertices.Count];
					if (!(vector == vector3) || !(vector2 == vector4))
					{
						flag3 = true;
						List<Vector2> vertices = floor.vertices;
						floor.vertices = previousVertices;
						Wall wall2 = floor.getWall(i, walls);
						floor.vertices = vertices;
						if (!(wall2 == null))
						{
							PropData prev = compressProp(wall2.GetComponent<PropInstance>(), getEditorContext());
							floor.setWall(i, wall2);
							PropData next = compressProp(wall2.GetComponent<PropInstance>(), getEditorContext());
							list.Add(actionChangeProp(prev, next, "Wall.vertices changed"));
						}
					}
				}
				if (!flag3)
				{
					flag2 = false;
				}
			}
			else if (count < count2)
			{
				int num = 0;
				for (int j = 0; j < count; j++)
				{
					if (!(floor.vertices[j] == previousVertices[j]))
					{
						num = j;
						break;
					}
				}
				List<Vector2> vertices2 = floor.vertices;
				floor.vertices = previousVertices;
				bool flag4 = num == 0;
				int num2 = num - 1;
				if (num2 < 0)
				{
					num2 = previousVertices.Count - 1;
				}
				Wall wall3 = floor.getWall(num2, walls);
				floor.vertices = vertices2;
				if (wall3 != null)
				{
					PropInstance component = wall3.GetComponent<PropInstance>();
					prevData = compressProp(component, getEditorContext());
					floor.setWall((!flag4) ? ((num2 + 1) % floor.vertices.Count) : 0, wall3);
					PropData next2 = compressProp(component, getEditorContext());
					list.Add(actionChangeProp(prevData, next2));
					wallPropID = getPropPrefabID(component);
					wallPrefab = loadPrefab(getEditorContext(), wallPropID);
					generatedWallProps = new List<PropData>();
					generateWall(flag4 ? (floor.vertices.Count - 1) : num2);
					if (count2 - count == 2)
					{
						generateWall(flag4 ? 1 : ((num2 + 2) % floor.vertices.Count));
					}
					actionsLoadProps(generatedWallProps, list);
				}
			}
			else if (count > count2)
			{
				int num3 = previousVertices.Count - 1;
				for (int k = 0; k < count2; k++)
				{
					if (!(floor.vertices[k] == previousVertices[k]))
					{
						num3 = k;
						break;
					}
				}
				List<Vector2> vertices3 = floor.vertices;
				floor.vertices = previousVertices;
				Wall wall4 = floor.getWall((num3 - 1 + floor.vertices.Count) % floor.vertices.Count);
				Wall wall5 = floor.getWall(num3);
				floor.vertices = vertices3;
				Wall wall6 = null;
				if (wall4 != null && wall5 != null)
				{
					wall = wall4;
					wall6 = wall5;
				}
				else if (wall4 != null)
				{
					wall6 = wall4;
				}
				else if (wall5 != null)
				{
					wall6 = wall5;
				}
				if (wall6 != null)
				{
					PropInstance component2 = wall6.GetComponent<PropInstance>();
					PropData prev2 = compressProp(component2, getEditorContext());
					floor.setWall((num3 == 0) ? (floor.vertices.Count - 1) : (num3 - 1), wall6);
					PropData next3 = compressProp(component2, getEditorContext());
					list.Add(actionChangeProp(prev2, next3));
				}
			}
			if (flag2)
			{
				foreach (Floor ceiling in floor.getCeilings())
				{
					(PropData prev, PropData next) tuple2 = prepChangeData(ceiling.GetComponent<PropInstance>());
					PropData item3 = tuple2.prev;
					PropData item4 = tuple2.next;
					FloorData floorData = getFloorData(item4);
					floorData.vertices.Clear();
					foreach (Vector2 vertex in floor.vertices)
					{
						Vector3 worldPosition = floor.polygonToWorldPosition(vertex) + Vector3.up * floor.wallHeight;
						floorData.vertices.Add(ceiling.worldToPolygonPosition(worldPosition));
					}
					list.Add(actionChangeProp(item3, item4, "Ceiling.vertices changed"));
				}
			}
			floor.transform.rotation = rotation;
		}
		int stackIndex = undo.stackIndex;
		undo.push(list.ToArray());
		if (wall != null)
		{
			deleteProps(new HashSet<InstanceID> { wall.GetComponent<PropInstance>().ID }, invokedByDeleteKey: false);
			undo.linkLast(undo.stackIndex - stackIndex);
		}
		void generateWall(int wallEdgeIndex)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(wallPrefab, floor.transform);
			gameObject.name = wallPropID.value;
			floor.setWall(wallEdgeIndex, gameObject.GetComponent<Wall>());
			PropInstance component3 = gameObject.GetComponent<PropInstance>();
			component3.ID = getNextInstanceID();
			PropData propData = compressProp(component3, getEditorContext());
			propData.materialSwaps = prevData.materialSwaps;
			if (tryGetWallData(propData, out var data) && tryGetWallData(prevData, out var data2))
			{
				data.baseThickness = data2.baseThickness;
				data.extrusionThickness = data2.extrusionThickness;
			}
			generatedWallProps.Add(propData);
			UnityEngine.Object.Destroy(component3.gameObject);
		}
	}

	private void handlePolygonCancel()
	{
		shouldEatNextLMB = true;
		shouldEatNextRMB = true;
	}

	private void handleFloorEdgePressed(Floor floor, int edgeIndex)
	{
		if (floor.tryGetWall(edgeIndex, out var wall))
		{
			deleteProps(new HashSet<InstanceID> { wall.GetComponent<PropInstance>().ID }, invokedByDeleteKey: false);
		}
		else
		{
			List<EditorAction> list = new List<EditorAction>();
			actionsGenerateWall(floor, edgeIndex, (floor.getWalls().Count == 0) ? getRandomWallPropID() : PropID.None, list);
			undo.push(list.ToArray());
		}
	}

	private void updatePolygonTool()
	{
		Polygon component;
		bool num = isSelectedSingle<Polygon>(out component);
		if (selectedPolygonLastFrame != component)
		{
			exitPolygonTool();
			if (component != null)
			{
				enterPolygonTool(component);
			}
			selectedPolygonLastFrame = component;
		}
		bool active = true;
		if (num)
		{
			if (polygonTool.isInteractingWithVertexOrEdge(includeHover: false))
			{
				polygonTool.enabled = true;
				active = false;
			}
			else
			{
				polygonTool.enabled = !transformGizmo.isTransforming && !modalUIActive() && !EventSystem.current.IsPointerOverGameObject();
				active = !polygonTool.isInteractingWithVertexOrEdge(includeHover: true);
			}
			if (component is Floor floor && !floor.isCeiling())
			{
				polygonToolUI.GenerateWalls.gameObject.SetActive(floor.getWalls().Count < floor.vertices.Count);
				polygonToolUI.GenerateCeiling.gameObject.SetActive(floor.getCeilings().Count == 0);
			}
			else
			{
				polygonToolUI.GenerateWalls.gameObject.SetActive(value: false);
				polygonToolUI.GenerateCeiling.gameObject.SetActive(value: false);
			}
			polygonToolUI.AddHole.gameObject.SetActive(!polygonTool.polygon.hasHole(polygonTool.newHole()));
		}
		if (isAnyLinkedPolygonSelected())
		{
			polygonTool.isEditingEnabled = false;
			active = false;
		}
		else
		{
			polygonTool.isEditingEnabled = true;
		}
		transformGizmo.gameObject.SetActive(active);
		if (polygonTool.enabled)
		{
			polygonTool.update();
		}
	}

	private void enterPolygonTool(Polygon polygon)
	{
		polygonTool.polygon = polygon;
		polygonToolUI.root.enabled = true;
		refresh.propertiesUI = true;
		refresh.hiddenPropsMessageUI = true;
		gridCustomPass.target = polygon.transform;
		gridCustomPass.planeSystem = polygon.planeSystem;
	}

	private void onPolygonToolButtonClick(Button button)
	{
		if (button == polygonToolUI.SaveChanges)
		{
			exitPolygonTool();
		}
		if (button == polygonToolUI.GenerateWalls && polygonTool.polygon is Floor floor)
		{
			generateWalls(floor);
		}
		if (button == polygonToolUI.GenerateCeiling && polygonTool.polygon is Floor floor2)
		{
			generateCeiling(floor2);
		}
		if (button == polygonToolUI.AddHole)
		{
			polygonTool.addHole();
			moveCameraTo(transformGizmo.pivotPoint, Mathf.Max(polygonTool.snapStep.x, polygonTool.snapStep.y) * 8f);
		}
		if (button == polygonToolUI.DiscardChanges)
		{
			int num = undo.calculateUndoableActionCount();
			string text = ((num == 1) ? "" : "s");
			optionDialog.showNoTranslate("Discard", $"Discard {num} change{text}? Change{text} will be lost.", onFrameClick, new VisualControl("polygonToolDiscard", ControllerButtonActionType.UIConfirmPrimary, "%RoomEditor_Discard%", VisualControl.ControlType.Button, VisualControl.Position.BotRight), new VisualControl("ignore", ControllerButtonActionType.UIConfirmPrimary, "%cancel%", VisualControl.ControlType.Button, VisualControl.Position.BotRight));
		}
	}

	private void exitPolygonTool()
	{
		polygonToolUI.root.enabled = false;
		refresh.propertiesUI = true;
		refresh.hiddenPropsMessageUI = true;
		gridCustomPass.target = gridCustomPassVolume.transform;
		gridCustomPass.planeSystem = PlayerSave.getSettings().re.gridPlane;
		transformGizmo.gameObject.SetActive(value: true);
		polygonTool.polygon = null;
		polygonTool.update();
	}

	private void generateWalls(Floor floor)
	{
		List<Vector2> list = floor.copyVertices();
		if (!Maths.isPolygonClockwise(list))
		{
			list.Reverse();
		}
		List<Wall> walls = floor.getWalls();
		PropID wallPropID = ((walls.Count == 0) ? getRandomWallPropID() : PropID.None);
		List<EditorAction> list2 = new List<EditorAction>();
		for (int i = 0; i < list.Count; i++)
		{
			if (floor.getWall(i, walls) == null)
			{
				actionsGenerateWall(floor, i, wallPropID, list2);
			}
		}
		undo.push(list2.ToArray());
	}

	private void actionsGenerateWall(Floor floor, int edgeIndex, PropID wallPropID, List<EditorAction> actions, List<Wall> walls = null)
	{
		GameObject gameObject;
		List<MaterialSwapData> materialSwaps;
		if (wallPropID != PropID.None)
		{
			gameObject = UnityEngine.Object.Instantiate(loadPrefab(getEditorContext(), wallPropID), floor.transform);
			gameObject.name = wallPropID.value;
			materialSwaps = new List<MaterialSwapData>();
		}
		else
		{
			PropInstance component = floor.getClosestWall(edgeIndex, walls).GetComponent<PropInstance>();
			PropID propPrefabID = getPropPrefabID(component);
			gameObject = UnityEngine.Object.Instantiate(loadPrefab(getEditorContext(), propPrefabID), floor.transform);
			gameObject.name = propPrefabID.value;
			materialSwaps = getPropInstanceMaterialSwaps(component, getEditorContext());
		}
		Wall component2 = gameObject.GetComponent<Wall>();
		component2.baseThickness = floor.wallThickness;
		floor.setWall(edgeIndex, component2);
		PropInstance component3 = gameObject.GetComponent<PropInstance>();
		component3.ID = getNextInstanceID();
		PropData propData = compressProp(component3, getEditorContext());
		propData.materialSwaps = materialSwaps;
		UnityEngine.Object.DestroyImmediate(component3.gameObject);
		actionsLoadProps(new List<PropData> { propData }, actions);
	}

	private PropID getRandomWallPropID()
	{
		List<Prop> propsWithTag = assets.getPropsWithTag(PropTag.BuildingWalls);
		int index = UnityEngine.Random.Range(0, propsWithTag.Count);
		return propsWithTag[index].ID;
	}

	private void generateCeiling(Floor floor)
	{
		List<Vector2> list = floor.copyVertices();
		if (!Maths.isPolygonClockwise(list))
		{
			list.Reverse();
		}
		List<Prop> propsWithTag = assets.getPropsWithTag(PropTag.BuildingFloors);
		int index = ((Is.Editor && testStartWallIndex >= 0) ? testStartWallIndex : UnityEngine.Random.Range(0, propsWithTag.Count));
		Prop prop = propsWithTag[index];
		GameObject original = loadPrefab(getEditorContext(), prop.ID);
		Quaternion rotation = floor.transform.rotation;
		floor.transform.rotation = Quaternion.identity;
		GameObject gameObject = UnityEngine.Object.Instantiate(original, floor.transform);
		gameObject.name = prop.ID.value;
		gameObject.transform.position = floor.transform.position + Vector3.up * floor.wallHeight;
		gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
		Floor component = gameObject.GetComponent<Floor>();
		component.extrusionType = Polygon.ExtrusionType.Front;
		component.extrusionThickness = 0f;
		component.vertices.Clear();
		foreach (Vector2 item2 in list)
		{
			Vector3 worldPosition = floor.polygonToWorldPosition(item2) + Vector3.up * floor.wallHeight;
			component.vertices.Add(component.worldToPolygonPosition(worldPosition));
		}
		PropInstance component2 = gameObject.GetComponent<PropInstance>();
		component2.ID = getNextInstanceID();
		floor.transform.rotation = rotation;
		PropData item = compressProp(component2, getEditorContext());
		UnityEngine.Object.Destroy(component2.gameObject);
		List<EditorAction> list2 = new List<EditorAction>();
		actionsLoadProps(new List<PropData> { item }, list2);
		undo.push(list2.ToArray());
	}

	private List<PropData> compressProps(List<PropInstance> instances, bool checkUnparentComp = false)
	{
		List<PropData> list = new List<PropData>(instances.Count);
		for (int i = 0; i < instances.Count; i++)
		{
			list.Add(compressProp(instances[i], getEditorContext()));
			if (checkUnparentComp && instances[i].GetComponent<Unparent>() != null)
			{
				list[i].parentID = InstanceID.None;
			}
		}
		return list;
	}

	private static PropData compressProp(PropInstance instance, EditorContext context)
	{
		PropData propData = new PropData
		{
			propID = getPropPrefabID(instance),
			ID = instance.ID,
			parentID = getParentID(instance),
			transform = saveTransform(instance.transform, isLocal: true),
			scriptName = (instance.scriptName ?? string.Empty),
			isExpandedInHierarchy = instance.isExpandedInHierarchy,
			isObstacle = (instance.CountComponentsInInstance<NavMeshObstacle>() > 0)
		};
		Interactive component = instance.GetComponent<Interactive>();
		propData.displayName = ((!string.IsNullOrEmpty(instance.displayName)) ? instance.displayName : ((component != null) ? component.displayName : string.Empty));
		if (component != null)
		{
			propData.targetPriority = component.roomEditorTargetPriotity;
		}
		propData.item = new List<ItemData>();
		if (instance.TryGetComponent<Item>(out var component2))
		{
			ItemData itemData = new ItemData();
			itemData.itemType = component2.itemType;
			itemData.hasRigidbody = component2.hasRigidbody;
			itemData.carriable = component2.carriable;
			propData.item.Add(itemData);
		}
		propData.switch3D = new List<Switch3DData>();
		if (instance.TryGetComponent<Switch3D>(out var component3))
		{
			Switch3DData switch3DData = new Switch3DData();
			switch3DData.duration = component3.duration;
			switch3DData.interpolation = component3.tweenState.interpolation;
			switch3DData.type = component3.switchType;
			switch3DData.isAnimation = component3.isAnimation;
			switch3DData.autoplay = component3.autoplay;
			switch3DData.pauseOnLoop = component3.pauseOnLoop;
			switch3DData.onLinks = compressLinks(component3.onLinks);
			switch3DData.offLinks = compressLinks(component3.offLinks);
			propData.switch3D.Add(switch3DData);
		}
		propData.tweenState = new List<TweenStateDataRoomEditor>();
		if (instance.TryGetComponent<TweenState>(out var component4))
		{
			TweenStateDataRoomEditor item = compressTweenStateData(component4);
			propData.tweenState.Add(item);
		}
		propData.turnable = new List<TurnableData>();
		if (instance.TryGetComponent<Turnable>(out var component5))
		{
			TurnableData turnableData = new TurnableData();
			turnableData.worldAxis = component5.worldAxis;
			turnableData.screenAxis = component5.screenAxis;
			turnableData.steps = component5.steps;
			turnableData.useHotspots = component5.useHotspots;
			turnableData.useAngleLimits = component5.useAngleLimits;
			turnableData.angleLimit1 = component5.angleLimit1;
			turnableData.angleLimit2 = component5.angleLimit2;
			turnableData.clickDirection = component5.clickDirection;
			turnableData.type = component5.type;
			turnableData.speed = component5.speed;
			turnableData.locks = compressLockArguments(component5.locks);
			propData.turnable.Add(turnableData);
		}
		propData.rotatable = new List<RotatableData>();
		if (instance.TryGetComponent<Rotatable>(out var component6))
		{
			RotatableData rotatableData = new RotatableData();
			rotatableData.speed = component6.speed;
			propData.rotatable.Add(rotatableData);
		}
		propData.draggable = new List<DraggableData>();
		if (instance.TryGetComponent<Draggable>(out var component7))
		{
			DraggableData draggableData = new DraggableData();
			draggableData.dragConstraintFlags = component7.dragConstraintFlags;
			draggableData.forcePoint = component7.forcePoint;
			draggableData.manipulationType = component7.manipulationType;
			propData.draggable.Add(draggableData);
		}
		propData.dial = new List<DialData>();
		if (instance.TryGetComponent<Dial>(out var component8))
		{
			DialData dialData = new DialData();
			dialData.rotationAxis = component8.rotationAxis;
			dialData.valueCount = component8.valueCount;
			dialData.snapToPosition = component8.snapToPosition;
			dialData.clickDirection = component8.clickDirection;
			dialData.useAngleLimits = component8.useAngleLimits;
			dialData.angleLimit1 = component8.angleLimit1;
			dialData.angleLimit2 = component8.angleLimit2;
			dialData.locks = compressLockArguments(component8.locks);
			propData.dial.Add(dialData);
		}
		propData.@lock = new List<LockData>();
		if (instance.TryGetComponent<Lock>(out var component9) && instance.GetComponent<Teleport>() == null && instance.GetComponent<Fog>() == null && instance.GetComponent<Sound>() == null && instance.GetComponent<EditorSkybox>() == null && instance.GetComponent<EditorPostProcessing>() == null)
		{
			LockData lockData = new LockData();
			lockData.type = component9.lockType;
			lockData.password = new List<int>(component9.password);
			lockData.disableLockOnUnlock = component9.deactivateLockOnUnlock;
			lockData.exitZoomOnUnlock = component9.exitZoomOnUnlock;
			lockData.onLock = compressLinks(component9.onLock);
			lockData.onUnlock = compressLinks(component9.onUnlock);
			lockData.logicType = component9.lockLogicType;
			lockData.negateValue = component9.lockNegateValue;
			propData.@lock.Add(lockData);
		}
		propData.slot = new List<SlotData>();
		if (instance.TryGetComponent<Slot>(out var component10))
		{
			SlotData slotData = new SlotData();
			slotData.turnDuration = component10.rotateKeyTurnDuration;
			slotData.turnCount = component10.rotateKeyTurnCount;
			slotData.turnAxis = component10.rotateKeyTurnAxis;
			slotData.ejectDirection = component10.ejectDirection;
			slotData.unlockSuccessKey = component10.onAcceptItemPlaced;
			slotData.unlockFailedKey = component10.onRejectItemPlaced;
			slotData.animationType = component10.animationType;
			slotData.onPlace = compressLinks(component10.onPlace);
			slotData.onRemove = compressLinks(component10.onRemove);
			slotData.keys = compressSlotKeys(component10.acceptItems);
			slotData.rejectKeys = compressSlotKeys(component10.rejectItems);
			slotData.pivot = saveTransform(component10.pivot, isLocal: true);
			slotData.initialItemID = ((component10.initialInsertedItem != null) ? getTargetID(component10.initialInsertedItem.gameObject) : InstanceID.None);
			propData.slot.Add(slotData);
		}
		propData.ladder = new List<LadderData>();
		if (instance.TryGetComponent<Ladder>(out var component11))
		{
			LadderData ladderData = new LadderData();
			ladderData.upLocalPosition = component11.upEmpty.transform.localPosition;
			ladderData.downLocalPosition = component11.downEmpty.transform.localPosition;
			ladderData.upExitLocalPosition = component11.upExit.transform.localPosition;
			ladderData.downExitLocalPosition = component11.downExit.transform.localPosition;
			propData.ladder.Add(ladderData);
		}
		propData.trigger = new List<TriggerData>();
		if (instance.TryGetComponent<Trigger>(out var component12))
		{
			TriggerData triggerData = new TriggerData();
			triggerData.keys = compressTargets(component12.keys);
			triggerData.anyObjectCanTrigger = component12.anyObjectCanTrigger;
			triggerData.canPlayerTrigger = component12.canPlayerTrigger;
			triggerData.triggerWhenAllPlayersEnter = component12.triggerWhenAllPlayersEnter;
			triggerData.isSticky = component12.isSticky;
			triggerData.onEnter = compressLinks(component12.enterData);
			triggerData.onExit = compressLinks(component12.exitData);
			triggerData.onStart = compressLinks(component12.startData);
			triggerData.onEnd = compressLinks(component12.endData);
			propData.trigger.Add(triggerData);
		}
		propData.spawnPoint = new List<SpawnPointData>();
		if (instance.TryGetComponent<SpawnPoint>(out var component13))
		{
			SpawnPointData spawnPointData = new SpawnPointData();
			spawnPointData.walkingSpeed = component13.walkingSpeed;
			spawnPointData.runningSpeed = component13.runningSpeed;
			propData.spawnPoint.Add(spawnPointData);
		}
		propData.teleport = new List<TeleportData>();
		if (instance.TryGetComponent<Teleport>(out var component14))
		{
			TeleportData teleportData = new TeleportData();
			teleportData.teleportAll = teleportData.teleportAll;
			teleportData.changeRotation = component14.changeRotation;
			teleportData.walkingSpeed = component14.walkingSpeed;
			teleportData.runningSpeed = component14.runningSpeed;
			propData.teleport.Add(teleportData);
		}
		propData.finish = new List<FinishData>();
		if (instance.TryGetComponent<Finish>(out var component15))
		{
			FinishData finishData = new FinishData();
			finishData.eyePosition = component15.levelShot.eyeLocalPosition;
			finishData.eyeRotation = component15.levelShot.eyeLocalRotation;
			propData.finish.Add(finishData);
		}
		propData.stairs = new List<StairsData>();
		if (instance.TryGetComponent<Stairs>(out var component16))
		{
			StairsData stairsData = new StairsData();
			stairsData.direction = component16.direction;
			propData.stairs.Add(stairsData);
		}
		propData.fog = new List<FogData>();
		if (instance.TryGetComponent<Fog>(out var component17))
		{
			propData.fog.Add(copy(component17.data));
		}
		propData.sound = new List<SoundData>();
		if (instance.TryGetComponent<Sound>(out var component18))
		{
			SoundData soundData = new SoundData();
			soundData.path = component18.path;
			soundData._event = component18._event;
			soundData.type = component18.type;
			soundData.isLoopable = component18.isLoopable;
			soundData.is2D = component18.is2D;
			soundData.minDistance3D = component18.minDistance3D;
			soundData.maxDistance3D = component18.maxDistance3D;
			soundData.activeOnStart = component18.activeOnStart;
			soundData.volume = component18.volume;
			soundData.localOnly = component18.localOnly;
			soundData.falloffMode = component18.falloffMode;
			propData.sound.Add(soundData);
		}
		propData.skybox = new List<SkyboxData>();
		if (instance.TryGetComponent<EditorSkybox>(out var component19))
		{
			SkyboxData skyboxData = copy(component19.data);
			skyboxData.textureData = new SkyTexData
			{
				fileName = component19.data.textureData?.fileName,
				custom = (component19.data.tempTexture != null && component19.data.textureData.custom)
			};
			propData.skybox.Add(skyboxData);
		}
		propData.clouds = new List<CloudsData>();
		if (instance.TryGetComponent<EditorClouds>(out var component20))
		{
			propData.clouds.Add(copy(component20.data));
		}
		propData.ocean = new List<OceanData>();
		if (instance.TryGetComponent<EditorOcean>(out var component21))
		{
			propData.ocean.Add(copy(component21.data));
		}
		propData.openLink = new List<OpenLinkData>();
		if (instance.TryGetComponent<OpenLink>(out var component22))
		{
			OpenLinkData openLinkData = new OpenLinkData();
			openLinkData.link = component22.link;
			propData.openLink.Add(openLinkData);
		}
		propData.water = new List<WaterData>();
		if (instance.TryGetComponent<EditorWater>(out var component23))
		{
			propData.water.Add(copy(component23.data));
		}
		propData.activator = new List<ActivatorComponentData>();
		if (instance.TryGetComponent<ActivatorComponent>(out var component24))
		{
			ActivatorComponentData activatorComponentData = new ActivatorComponentData();
			activatorComponentData.keys = compressTargets(component24.keys);
			activatorComponentData.type = component24.type;
			activatorComponentData.targetObject = component24.targetObject;
			activatorComponentData.targetRenderer = component24.targetRenderer;
			activatorComponentData.targetCollider = component24.targetCollider;
			activatorComponentData.targetObstacle = component24.targetObstacle;
			activatorComponentData.targetTargetable = component24.targetTargetable;
			activatorComponentData.activeOnStart = component24.activeOnStart;
			propData.activator.Add(activatorComponentData);
		}
		propData.lookable = new List<LookableData>();
		if (instance.TryGetComponent<Lookable>(out var component25))
		{
			LookableData lookableData = new LookableData();
			lookableData.onActivated = compressTargets(component25.onActivated);
			lookableData.onDeactivated = compressTargets(component25.onDeactivated);
			lookableData.activateOnlyOnce = component25.activateOnlyOnce;
			lookableData.targetVisibilityPercent = component25.targetVisibilityPercent;
			lookableData.targetScreenPercent = component25.targetScreenPercent;
			propData.lookable.Add(lookableData);
		}
		propData.zoomable = new List<ZoomableData>();
		if (instance.TryGetComponent<Zoomable>(out var component26))
		{
			ZoomableData zoomableData = new ZoomableData();
			zoomableData.eyePosition = component26.eyeLocalPosition;
			zoomableData.eyeRotation = component26.eyeLocalRotation;
			propData.zoomable.Add(zoomableData);
		}
		propData.token = new List<TokenData>();
		if (instance.TryGetComponent<Token>(out var component27))
		{
			TokenData tokenData = new TokenData();
			tokenData.id = component27.roomEditorId;
			propData.token.Add(tokenData);
		}
		propData.display = new List<EditorDisplayData>();
		if (instance.TryGetComponent<EditorDisplay>(out var component28))
		{
			EditorDisplayData editorDisplayData = new EditorDisplayData();
			Lock targetLock = component28.targetLock;
			editorDisplayData.targetLock = ((targetLock != null) ? targetLock.GetComponent<PropInstance>().ID : InstanceID.None);
			editorDisplayData.columns = component28.columns;
			editorDisplayData.rows = component28.rows;
			editorDisplayData.padding = component28.padding;
			editorDisplayData.spriteSheetColumns = component28.spriteSheetColumns;
			editorDisplayData.spriteSheetRows = component28.spriteSheetRows;
			editorDisplayData.spriteSheetFileName = component28.spriteSheetFileName;
			propData.display.Add(editorDisplayData);
		}
		propData.light = new List<EditorLightData>();
		if (instance.TryGetComponent<EditorLight>(out var component29))
		{
			EditorLightData editorLightData = new EditorLightData();
			if (component29.lightComponent != null)
			{
				editorLightData.type = component29.lightComponent.type;
				editorLightData.range = component29.lightComponent.range;
				editorLightData.spotAngle = component29.lightComponent.spotAngle;
				editorLightData.color = component29.lightComponent.color;
				editorLightData.intensity = component29.lightComponent.intensity;
				editorLightData.castShadows = component29.lightComponent.shadows != LightShadows.None;
				editorLightData.shadowDimmer = component29.hdLightData.shadowDimmer;
				editorLightData.cookieTextureId = component29.cookieTextureId;
			}
			propData.light.Add(editorLightData);
		}
		propData.postProcessing = new List<EditorPostProcessingData>();
		if (instance.TryGetComponent<EditorPostProcessing>(out var component30))
		{
			propData.postProcessing.Add(copy(component30.data));
		}
		propData.slidable = new List<SlidableData>();
		if (instance.TryGetComponent<Slidable>(out var component31))
		{
			SlidableData slidableData = new SlidableData();
			slidableData.endNode = saveTransform(component31.endNode, isLocal: true);
			slidableData.snapMode = component31.snapMode;
			slidableData.additionalSnappingPoints = component31.additionalSnapPointCount;
			slidableData.snapAnimationDuration = component31.snapAnimationDuration;
			slidableData.startingIndex = component31.snapPointIndex;
			slidableData.locks = compressLockArguments(component31.locks);
			propData.slidable.Add(slidableData);
		}
		propData.puzzle = new List<PuzzleData>();
		if (instance.TryGetComponent<EditorPuzzle>(out var component32))
		{
			PuzzleData puzzleData = new PuzzleData();
			puzzleData.puzzleName = component32.puzzleName;
			puzzleData.hints = new List<int>(component32.hints);
			puzzleData.conditions = compressTargets(component32.conditions);
			puzzleData.muteSound = component32.muteSound;
			propData.puzzle.Add(puzzleData);
		}
		propData.roulette = new List<RouletteData>();
		if (instance.TryGetComponent<Roulette>(out var component33))
		{
			RouletteData rouletteData = new RouletteData();
			rouletteData.targets = compressTargets(component33.targets);
			rouletteData.removeTargetOnTrigger = component33.targetCanBeActivatedOnlyOnce;
			rouletteData.testingTarget = component33.testingTarget;
			propData.roulette.Add(rouletteData);
		}
		propData.setup = new List<EditorSetupData>();
		if (instance.TryGetComponent<EditorSetup>(out var component34))
		{
			EditorSetupData editorSetupData = new EditorSetupData();
			editorSetupData.targets = compressTargets(component34.targets);
			editorSetupData.difficulty = component34.difficulty;
			propData.setup.Add(editorSetupData);
		}
		propData.customModel = new List<CustomModelData>();
		if (instance.TryGetComponent<CustomModel>(out var component35))
		{
			CustomModelData customModelData = new CustomModelData();
			customModelData.disableGeneratedCollider = component35.disableGeneratedCollider;
			customModelData.usedFiles = new List<string>();
			foreach (string usedFile in component35.usedFiles)
			{
				customModelData.usedFiles.Add(usedFile);
			}
			customModelData.currentAnimation = component35.currentAnimation;
			customModelData.colliderType = component35.colliderType;
			propData.customModel.Add(customModelData);
		}
		propData.itemRespawner = new List<ItemRespawnerData>();
		if (instance.TryGetComponent<ItemRespawnVolume>(out var component36))
		{
			ItemRespawnerData itemRespawnerData = new ItemRespawnerData();
			itemRespawnerData.respawnMode = component36.respawnMode;
			itemRespawnerData.respawnWithPhysics = component36.respawnWithPhysics;
			itemRespawnerData.objectToRespawnTo = ((component36.objectToRespawnTo != null) ? component36.objectToRespawnTo.GetComponent<PropInstance>().ID : InstanceID.None);
			propData.itemRespawner.Add(itemRespawnerData);
		}
		propData.script = new List<ScriptComponentData>();
		if (instance.TryGetComponent<LuaExecutor>(out var component37))
		{
			ScriptComponentData scriptComponentData = new ScriptComponentData();
			scriptComponentData.scriptLocation = component37.luaCodeLocation;
			scriptComponentData.functionToCall = component37.functionToCall;
			scriptComponentData.canBeTriggered = component37.canBeTriggered;
			propData.script.Add(scriptComponentData);
		}
		propData.floor = new List<FloorData>();
		propData.wall = new List<WallData>();
		if (instance.TryGetComponent<Polygon>(out var component38))
		{
			PolygonData polygonData = ((component38 is Floor) ? ((PolygonData)new FloorData()) : ((PolygonData)new WallData()));
			polygonData.vertices = component38.copyVertices();
			polygonData.holes = component38.copyHoles();
			polygonData.baseThickness = component38.baseThickness;
			polygonData.extrusionThickness = component38.extrusionThickness;
			polygonData.extrusionType = component38.extrusionType;
			polygonData.collisionType = component38.collisionType;
			polygonData.hasCollider = component38.hasCollider;
			if (component38 is Floor floor)
			{
				FloorData floorData = polygonData as FloorData;
				floorData.wallHeight = floor.wallHeight;
				floorData.wallThickness = floor.wallThickness;
				floorData.connectWalls = floor.connectWalls;
				propData.floor.Add(floorData);
			}
			else if (component38 is Wall)
			{
				WallData item2 = polygonData as WallData;
				propData.wall.Add(item2);
			}
			else
			{
				UnityEngine.Debug.LogError("Unsupported polygon type: " + component38.GetType().Name);
			}
		}
		propData.test = new List<TestData>();
		if (instance.TryGetComponent<Test>(out var component39))
		{
			TestData testData = new TestData();
			testData.integer = component39.intValue;
			testData.integerRange = component39.intRange;
			testData.single = component39.floatValue;
			testData.singleRange = component39.floatRange;
			testData.vector3 = component39.vector3Value;
			testData.vector2 = component39.vector2Value;
			testData.text = component39.stringValue;
			testData.color1 = component39.color1Value;
			testData.color2 = component39.color2Value;
			testData.boolean = component39.boolValue;
			testData.option = component39.enumValue;
			testData.textureId = component39.textureValue;
			propData.test.Add(testData);
		}
		propData.text = new List<TextData>();
		if (instance.TryGetComponent<EditorText>(out var component40))
		{
			TextData textData = new TextData();
			textData.text = component40.text;
			textData.fontSize = component40.fontSize;
			textData.color = component40.color;
			textData.isBold = component40.isBold;
			textData.isItalic = component40.isItalic;
			textData.canvasSize = component40.canvasSize;
			propData.text.Add(textData);
		}
		propData.delay = new List<DelayData>();
		if (instance.TryGetComponent<EditorDelay>(out var component41))
		{
			DelayData delayData = new DelayData();
			delayData.targets = compressTargets(component41.targets);
			delayData.delay = component41.delay;
			propData.delay.Add(delayData);
		}
		propData.materialSwaps = getPropInstanceMaterialSwaps(instance, context);
		return propData;
		static RoomEditorLinksData compressLinks(RoomEditorLinks links)
		{
			return new RoomEditorLinksData
			{
				locks = compressLockArguments(links.locks),
				targets = compressTargets(links.targets),
				output = links.output
			};
		}
		static List<LockArgData> compressLockArguments(List<LockArgument> lockArguments)
		{
			if (lockArguments == null)
			{
				return new List<LockArgData>();
			}
			List<LockArgData> list = new List<LockArgData>(lockArguments.Count);
			foreach (LockArgument lockArgument in lockArguments)
			{
				PropInstance component42 = lockArgument.targetLock.gameObject.GetComponent<PropInstance>();
				list.Add(new LockArgData
				{
					instanceID = component42.ID,
					passwordIndex = lockArgument.targetIndex
				});
			}
			return list;
		}
		static List<InstanceID> compressSlotKeys(Item[] keys)
		{
			List<InstanceID> list = new List<InstanceID>(keys.Length);
			foreach (Item item3 in keys)
			{
				list.Add(getTargetID(item3.gameObject));
			}
			return list;
		}
		static List<InstanceID> compressTargets(List<GameObject> targets)
		{
			List<InstanceID> list = new List<InstanceID>(targets.Count);
			foreach (GameObject target in targets)
			{
				list.Add(getTargetID(target));
			}
			return list;
		}
		static TweenStateDataRoomEditor compressTweenStateData(TweenState tweenState)
		{
			TweenStateDataRoomEditor tweenStateDataRoomEditor = new TweenStateDataRoomEditor
			{
				tweenStates = new List<TweenStateRecordRoomEditor>()
			};
			foreach (TweenState.TweenStateRecord item4 in tweenState.tweenStateDataNeo)
			{
				TweenStateRecordRoomEditor tweenStateRecordRoomEditor = new TweenStateRecordRoomEditor
				{
					name = item4.name,
					weight = item4.weight,
					targetWeight = item4.targetWeight,
					speed = item4.speed,
					delay = item4.delay
				};
				foreach (ObjectState state in item4.states)
				{
					ObjectStateRoomEditor objectStateRoomEditor = new ObjectStateRoomEditor
					{
						path = state.path,
						flags = state.flags,
						Image_color = state.Image_color,
						Text_color = state.Text_color,
						Text_fontSize = state.Text_fontSize,
						Transform_localRotation = state.Transform_localRotation,
						Transform_localRotationEuler = state.Transform_localRotationEuler,
						Transform_localScale = state.Transform_localScale,
						Transform_localPosition = state.Transform_localPosition,
						AnimationSampler_unitTime = state.AnimationSampler_unitTime,
						Item_examinePivotOffset = state.Item_examinePivotOffset,
						Item_examineScaleModifier = state.Item_examineScaleModifier,
						Item_examineBaseRotation = state.Item_examineBaseRotation,
						Item_groundRotation = state.Item_groundRotation,
						Interactive_targetPriority = state.Interactive_targetPriority,
						MaterialState = state.MaterialState,
						tweenState = state.tweenState,
						Light_Filter = state.Light_Filter,
						Light_Temperature = state.Light_Temperature,
						Light_Intensity = state.Light_Intensity
					};
					if (string.IsNullOrEmpty(objectStateRoomEditor.path))
					{
						Transform transform = tweenState.transform.Find(objectStateRoomEditor.path);
						GameObject go = ((transform == null) ? null : transform.gameObject);
						objectStateRoomEditor.gameObjectId = getTargetID(go);
					}
					tweenStateRecordRoomEditor.states.Add(objectStateRoomEditor);
				}
				tweenStateDataRoomEditor.tweenStates.Add(tweenStateRecordRoomEditor);
			}
			return tweenStateDataRoomEditor;
		}
		static InstanceID getTargetID(GameObject go)
		{
			return go.GetComponent<PropInstance>().ID;
		}
	}

	private void updatePropertiesUI()
	{
		bool flag = isPropInstanceSelected();
		propertiesUI.root.enabled = flag;
		foreach (PropertyData property in properties)
		{
			if (flag || !(property.ui.transform.parent == propertiesUI.Content))
			{
				property.onUpdate?.Invoke();
			}
		}
	}

	private void refreshTargetPropertiesUI()
	{
		foreach (Interactive scrubbingInteractive in scrubbingInteractives)
		{
			if (scrubbingInteractive is Switch3D switch3D)
			{
				switch3D.tweenState?.setWeight("Down", 0f);
			}
		}
		scrubbingInteractives.Clear();
		List<PropInstance> targetInstances = new List<PropInstance>();
		if (isPropInstanceSelected())
		{
			targetInstances = getSelectedInstances(includeChildInstances: false);
			refreshPropertiesUI(targetInstances);
		}
		refreshLineConnections(targetInstances);
	}

	private void removePropertiesForParent(Transform parent)
	{
		for (int num = properties.Count - 1; num >= 0; num--)
		{
			PropertyData propertyData = properties[num];
			if (propertyData.ui == null)
			{
				properties.RemoveAt(num);
			}
			else if (!(propertyData.ui.transform.parent != parent))
			{
				UnityEngine.Object.DestroyImmediate(propertyData.ui.gameObject);
				properties.RemoveAt(num);
			}
		}
	}

	private void refreshPropertiesUI(List<PropInstance> targetInstances)
	{
		inputFieldsNavigationDataProperties.removeOnEndEditListeners();
		removePropertiesForParent(propertiesUI.Content);
		startPropertySection(propertiesUI.Content, UITooltip.Position.Left);
		bool flag = targetInstances.Count == 1;
		propertiesUI.Header_InstanceID.text = $"ID: {targetInstances[0].ID.value}";
		propertiesUI.Header_InstanceID.gameObject.SetActive(flag);
		string tooltip = "Number that uniquely identifies this prop instance (" + getPropPrefabID(targetInstances[0]).value + ").";
		setHoverTooltip(tooltip, propertiesUI.Header_InstanceID, UITooltip.Position.Left);
		propertiesUI.Header_PropLink.gameObject.SetActive(flag && getPropIDLinkForInstance(targetInstances[0]) != PropID.None);
		propertiesUI.Header_Text.text = $"Properties ({targetInstances.Count})";
		PropInstance targetInstance;
		Item item;
		Switch3D switch3d;
		TweenState tweenState;
		Turnable turnable;
		Rotatable rotatable;
		Dial dial;
		Zoomable zoomable;
		Lookable lookable;
		Token token;
		Slidable slidable;
		Draggable draggable;
		CustomModel customModel;
		bool isWall;
		bool isLight;
		InstanceUICache cache;
		if (flag)
		{
			targetInstance = targetInstances[0];
			item = targetInstance.GetComponent<Item>();
			switch3d = targetInstance.GetComponent<Switch3D>();
			tweenState = targetInstance.GetComponent<TweenState>();
			turnable = targetInstance.GetComponent<Turnable>();
			rotatable = targetInstance.GetComponent<Rotatable>();
			Ladder component = targetInstance.GetComponent<Ladder>();
			dial = targetInstance.GetComponent<Dial>();
			zoomable = targetInstance.GetComponent<Zoomable>();
			lookable = targetInstance.GetComponent<Lookable>();
			token = targetInstance.GetComponent<Token>();
			slidable = targetInstance.GetComponent<Slidable>();
			draggable = targetInstance.GetComponent<Draggable>();
			Lock component2 = targetInstance.GetComponent<Lock>();
			Slot component3 = targetInstance.GetComponent<Slot>();
			Trigger component4 = targetInstance.GetComponent<Trigger>();
			Obstacle component5 = targetInstance.GetComponent<Obstacle>();
			Teleport component6 = targetInstance.GetComponent<Teleport>();
			Stairs component7 = targetInstance.GetComponent<Stairs>();
			Finish component8 = targetInstance.GetComponent<Finish>();
			Fog component9 = targetInstance.GetComponent<Fog>();
			OpenLink component10 = targetInstance.GetComponent<OpenLink>();
			LuaExecutor component11 = targetInstance.GetComponent<LuaExecutor>();
			ActivatorComponent component12 = targetInstance.GetComponent<ActivatorComponent>();
			customModel = targetInstance.GetComponent<CustomModel>();
			Sound component13 = targetInstance.GetComponent<Sound>();
			EditorCollider component14 = targetInstance.GetComponent<EditorCollider>();
			EditorLight component15 = targetInstance.GetComponent<EditorLight>();
			EditorPostProcessing component16 = targetInstance.GetComponent<EditorPostProcessing>();
			EditorDisplay component17 = targetInstance.GetComponent<EditorDisplay>();
			EditorSkybox component18 = targetInstance.GetComponent<EditorSkybox>();
			EditorWater component19 = targetInstance.GetComponent<EditorWater>();
			EditorOcean component20 = targetInstance.GetComponent<EditorOcean>();
			EditorClouds component21 = targetInstance.GetComponent<EditorClouds>();
			EditorPuzzle component22 = targetInstance.GetComponent<EditorPuzzle>();
			Roulette component23 = targetInstance.GetComponent<Roulette>();
			EditorSetup component24 = targetInstance.GetComponent<EditorSetup>();
			Floor component25 = targetInstance.GetComponent<Floor>();
			EditorText component26 = targetInstance.GetComponent<EditorText>();
			EditorDelay component27 = targetInstance.GetComponent<EditorDelay>();
			bool flag2 = targetInstance.GetComponent<EditorEmpty>() != null;
			bool flag3 = targetInstance.GetComponent<ItemRespawnVolume>() != null;
			isWall = targetInstance.GetComponent<Wall>() != null;
			bool flag4 = targetInstance.GetComponent<Test>() != null;
			bool flag5 = targetInstance.GetComponent<SpawnPoint>() != null;
			bool flag6 = component25 != null;
			bool flag7 = component9 != null;
			bool flag8 = component13 != null;
			bool flag9 = component18 != null;
			bool flag10 = component19 != null;
			bool flag11 = component20 != null;
			bool flag12 = component21 != null;
			bool flag13 = component3 != null;
			bool flag14 = component2 != null;
			bool flag15 = component6 != null;
			bool flag16 = component7 != null;
			bool flag17 = component8 != null;
			bool flag18 = rotatable != null;
			bool flag19 = component != null;
			bool flag20 = component4 != null;
			bool flag21 = component5 != null;
			bool flag22 = component10 != null;
			bool flag23 = component11 != null;
			bool flag24 = component12 != null;
			bool flag25 = customModel != null;
			bool flag26 = component14 != null;
			isLight = component15 != null;
			bool flag27 = component16 != null;
			bool flag28 = component17 != null;
			bool flag29 = component22 != null;
			bool flag30 = component23 != null;
			bool flag31 = component24 != null;
			bool flag32 = component26 != null;
			bool flag33 = component27 != null;
			bool flag34 = isSpecialEffect(targetInstance);
			bool num = flag13 || flag14 || flag20 || flag21 || flag5 || flag17 || flag23 || flag15 || flag22 || flag24 || flag26 || isLight || flag27 || flag28 || flag29 || flag30 || flag31 || flag10 || flag11 || flag12 || flag9 || flag7 || flag3 || flag8 || flag19 || flag32 || flag33;
			if (flag2)
			{
				propertiesUI.Header_Text.text = "Empty";
			}
			else if (flag34)
			{
				propertiesUI.Header_Text.text = "Effect";
			}
			else if (flag3)
			{
				propertiesUI.Header_Text.text = "Item Respawner";
			}
			else if (flag6)
			{
				propertiesUI.Header_Text.text = (component25.isCeiling() ? "Ceiling" : "Floor");
			}
			else if (isWall)
			{
				propertiesUI.Header_Text.text = "Wall";
			}
			else if (flag13)
			{
				propertiesUI.Header_Text.text = "Slot";
			}
			else if (flag15)
			{
				propertiesUI.Header_Text.text = "Teleport";
			}
			else if (flag7)
			{
				propertiesUI.Header_Text.text = "Fog";
			}
			else if (flag8)
			{
				propertiesUI.Header_Text.text = "Sound";
			}
			else if (flag9)
			{
				propertiesUI.Header_Text.text = "Skybox";
			}
			else if (flag27)
			{
				propertiesUI.Header_Text.text = "Post Processing";
			}
			else if (flag20)
			{
				propertiesUI.Header_Text.text = "Trigger";
			}
			else if (flag21)
			{
				propertiesUI.Header_Text.text = "Obstacle";
			}
			else if (flag5)
			{
				propertiesUI.Header_Text.text = "Spawn Point";
			}
			else if (flag17)
			{
				propertiesUI.Header_Text.text = "Finish";
			}
			else if (flag22)
			{
				propertiesUI.Header_Text.text = "Open Link";
			}
			else if (flag18)
			{
				propertiesUI.Header_Text.text = "Rotatable";
			}
			else if (flag19)
			{
				propertiesUI.Header_Text.text = "Ladder";
			}
			else if (flag23)
			{
				propertiesUI.Header_Text.text = "Script";
			}
			else if (flag24)
			{
				propertiesUI.Header_Text.text = "Activator";
			}
			else if (flag25)
			{
				propertiesUI.Header_Text.text = "Custom Model";
			}
			else if (flag26)
			{
				propertiesUI.Header_Text.text = "Collider";
			}
			else if (isLight)
			{
				propertiesUI.Header_Text.text = "Light";
			}
			else if (flag28)
			{
				propertiesUI.Header_Text.text = "Display";
			}
			else if (flag29)
			{
				propertiesUI.Header_Text.text = "Puzzle";
			}
			else if (flag30)
			{
				propertiesUI.Header_Text.text = "Roulette";
			}
			else if (flag31)
			{
				propertiesUI.Header_Text.text = "Setup";
			}
			else if (flag14)
			{
				propertiesUI.Header_Text.text = "Lock";
			}
			else if (flag4)
			{
				propertiesUI.Header_Text.text = "Test";
			}
			else if (flag10)
			{
				propertiesUI.Header_Text.text = "Water";
			}
			else if (flag11)
			{
				propertiesUI.Header_Text.text = "Ocean";
			}
			else if (flag12)
			{
				propertiesUI.Header_Text.text = "Clouds";
			}
			else if (flag32)
			{
				propertiesUI.Header_Text.text = "Text";
			}
			else if (flag33)
			{
				propertiesUI.Header_Text.text = "Delay";
			}
			else
			{
				propertiesUI.Header_Text.text = "Properties";
			}
			cache = getInstanceUICache(targetInstance);
			targetInstance.GetComponent<Rigidbody>();
			refreshTransformUI(targetInstance);
			refreshHierarchyUI(targetInstance);
			refreshNamingUI(targetInstance);
			bool flag35 = (!num || isLight) && !flag6;
			bool flag36 = (!num || flag26) && !isWall && !flag6 && !flag2;
			if (flag35 || flag36)
			{
				defineHeaderProperty("Behaviour", "Defines what kind of interactions this prop has with player or other props in the room.", cache.isBehaviourShown, delegate(bool isShown)
				{
					cache.isBehaviourShown = isShown;
				});
			}
			if (cache.isBehaviourShown)
			{
				if (flag35)
				{
					buildBehavioursDropDown();
				}
				if (flag36)
				{
					defineBoolProperty("Is Obstacle", "When checked, players won't be able to walk inside this prop.\nNot recommended for moving props.", targetInstance.CountComponentsInInstance<NavMeshObstacle>() > 0, delegate(bool value)
					{
						(PropData prev, PropData next) tuple = prepChangeData(targetInstance);
						PropData item2 = tuple.prev;
						PropData item3 = tuple.next;
						item3.isObstacle = value;
						undo.push(actionChangeProp(item2, item3, "isObstacle"));
					});
				}
				if (flag35)
				{
					buildBehaviours();
				}
			}
			if (flag24)
			{
				refreshActivatorUI(targetInstance);
			}
			if (flag12)
			{
				refreshCloudsUI(targetInstance);
			}
			if (flag25)
			{
				refreshCustomModelUI(targetInstance);
			}
			if (flag33)
			{
				refreshDelayUI(targetInstance);
			}
			if (flag28)
			{
				refreshDisplayUI(targetInstance);
			}
			if (flag17)
			{
				refreshFinishUI(targetInstance);
			}
			if (flag6 || isWall)
			{
				refreshPolygonUI(targetInstance);
			}
			if (flag7)
			{
				refreshFogUI(targetInstance);
			}
			if (flag3)
			{
				refreshItemRespawnerUI(targetInstance);
			}
			if (flag19)
			{
				refreshLadderUI(targetInstance);
			}
			if (isLight)
			{
				refreshLightUI(targetInstance);
			}
			if (flag14)
			{
				refreshLockUI(targetInstance);
			}
			if (flag11)
			{
				refreshOceanUI(targetInstance);
			}
			if (flag22)
			{
				refreshOpenLinkUI(targetInstance);
			}
			if (flag27)
			{
				refreshPostProcessingUI(targetInstance);
			}
			if (flag29)
			{
				refreshPuzzleUI(targetInstance);
			}
			if (flag30)
			{
				refreshRouletteUI(targetInstance);
			}
			if (flag23)
			{
				refreshScriptUI(targetInstance);
			}
			if (flag9)
			{
				refreshSkyboxUI(targetInstance);
			}
			if (flag31)
			{
				refreshSetupUI(targetInstance);
			}
			if (flag13)
			{
				refreshSlotUI(targetInstance);
			}
			if (flag5)
			{
				refreshSpawnPointUI(targetInstance);
			}
			if (flag16)
			{
				refreshStairsUI(targetInstance);
			}
			if (flag8)
			{
				refreshSoundUI(targetInstance);
			}
			if (flag15)
			{
				refreshTeleportUI(targetInstance);
			}
			if (flag4)
			{
				refreshTestUI(targetInstance);
			}
			if (flag32)
			{
				refreshTextUI(targetInstance);
			}
			if (flag20)
			{
				refreshTriggerUI(targetInstance);
			}
			if (flag10)
			{
				refreshWaterUI(targetInstance);
			}
			if (!num && !flag2 && !flag34)
			{
				refreshMaterialsUI(targetInstance);
			}
			Resources.UnloadUnusedAssets();
		}
		else
		{
			refreshMultiTargetTransformUI(targetInstances);
			int[] obstaclesFlags = new int[targetInstances.Count];
			int num2 = 0;
			int num3 = 0;
			for (int num4 = 0; num4 < targetInstances.Count; num4++)
			{
				int num5 = 2;
				if (!instanceIsSpecial(targetInstances[num4]))
				{
					num5 = ((targetInstances[num4].CountComponentsInInstance<NavMeshObstacle>() > 0) ? 1 : 0);
				}
				obstaclesFlags[num4] = num5;
				if (num5 == 0)
				{
					num3++;
				}
				if (num5 == 1)
				{
					num2++;
				}
			}
			if (num2 > 0 || num3 > 0)
			{
				defineBoolProperty("Is Obstacle", "When checked, players won't be able to walk inside this prop.\nNot recommended for moving props.", (num2 > 0 && num3 > 0) ? ((bool?)null) : new bool?(num2 > 0), delegate(bool value)
				{
					List<EditorAction> list = new List<EditorAction>();
					for (int i = 0; i < targetInstances.Count; i++)
					{
						int num8 = (value ? 1 : 0);
						if (obstaclesFlags[i] != 2 && obstaclesFlags[i] != num8)
						{
							(PropData prev, PropData next) tuple = prepChangeData(targetInstances[i]);
							PropData item2 = tuple.prev;
							PropData item3 = tuple.next;
							item3.isObstacle = value;
							list.Add(actionChangeProp(item2, item3, "isObstacle"));
						}
					}
					undo.push(list.ToArray());
				});
			}
			bool flag37 = true;
			PropInstance parentInstance = getParentInstance(targetInstances[0]);
			for (int num6 = 1; num6 < targetInstances.Count && flag37; num6++)
			{
				flag37 = getParentInstance(targetInstances[num6]) == parentInstance;
			}
			if (flag37 && parentInstance == null)
			{
				List<TargetArrayEntry> elements = new List<TargetArrayEntry>();
				defineTargetArrayProperty("%RE_parent%", "%RE_parentTooltip%", elements, 1, delegate
				{
					enterTargetMode(targetParentPredicate, null, delegate(PropInstance pi, int _)
					{
						EditorAction[] array = new EditorAction[targetInstances.Count];
						for (int i = 0; i < targetInstances.Count; i++)
						{
							(PropData prev, PropData next) tuple = prepChangeData(targetInstances[i]);
							PropData item2 = tuple.prev;
							PropData item3 = tuple.next;
							item3.parentID = pi.ID;
							array[i] = actionChangeProp(item2, item3, "change parent");
						}
						undo.push(array);
					});
				}, delegate
				{
				}, delegate
				{
				});
			}
			else if (flag37 && parentInstance != null)
			{
				PropID propID = new PropID
				{
					value = parentInstance.name
				};
				List<TargetArrayEntry> elements2 = new List<TargetArrayEntry> { getTargetArrayEntry(parentInstance.gameObject) };
				defineTargetArrayProperty("%RE_parent%", "%RE_parentTooltip%", elements2, 1, delegate
				{
				}, delegate
				{
					undo.push(actionChangeSelection(newInstanceSelection(parentInstance.ID)));
					moveCameraTo(parentInstance.gameObject);
				}, delegate
				{
					EditorAction[] array = new EditorAction[targetInstances.Count];
					for (int i = 0; i < targetInstances.Count; i++)
					{
						(PropData prev, PropData next) tuple = prepChangeData(targetInstances[i]);
						PropData item2 = tuple.prev;
						PropData item3 = tuple.next;
						item3.parentID = InstanceID.None;
						array[i] = actionChangeProp(item2, item3, "Remove Parent");
					}
					undo.push(array);
				});
			}
			else if (!flag37)
			{
				List<TargetArrayEntry> elements3 = new List<TargetArrayEntry>
				{
					new TargetArrayEntry
					{
						index = -1,
						texture = differentParentIcon
					}
				};
				defineTargetArrayProperty("%RE_parent%", "%RE_parentTooltip%", elements3, 1, delegate
				{
				}, delegate
				{
				}, delegate
				{
					List<EditorAction> list = new List<EditorAction>();
					foreach (PropInstance targetInstance2 in targetInstances)
					{
						if (!(getParentInstance(targetInstance2) == null))
						{
							(PropData prev, PropData next) tuple = prepChangeData(targetInstance2);
							PropData item2 = tuple.prev;
							PropData item3 = tuple.next;
							item3.parentID = InstanceID.None;
							list.Add(actionChangeProp(item2, item3, "Remove Parent"));
						}
					}
					if (list.Count > 0)
					{
						undo.push(list.ToArray());
					}
				});
			}
			defineSeparatorProperty();
			List<PropInstance> childPropInstances = getChildPropInstances(targetInstances[0]);
			for (int num7 = 1; num7 < targetInstances.Count; num7++)
			{
				getChildPropInstances(targetInstances[num7], childPropInstances);
			}
		}
		inputFieldsNavigationDataProperties.initNavigation(propertiesUI.Content, "Inspector");
		recalculateHeightOfPropertiesUI();
		void addInteraction(Action<PropData> addToNext, string actionChangeLabel)
		{
			var (prev, propData) = prepChangeData(targetInstance);
			addToNext(propData);
			undo.push(actionChangeProp(prev, propData, actionChangeLabel));
		}
		void buildBehaviours()
		{
			buildShared();
			if (item != null)
			{
				buildItem();
			}
			else if (switch3d != null)
			{
				buildSwitch3d();
			}
			else if (turnable != null)
			{
				buildTurnable();
			}
			else if (rotatable != null)
			{
				buildRotatable();
			}
			else if (draggable != null)
			{
				buildDraggable();
			}
			else if (dial != null)
			{
				buildDial();
			}
			else if (zoomable != null)
			{
				buildZoomable();
			}
			else if (slidable != null)
			{
				buildSlidable();
			}
			else if (lookable != null)
			{
				buildLookable();
			}
			else if (token != null)
			{
				buildToken();
			}
		}
		void buildBehavioursDropDown()
		{
			int initValue = (int)getSelectedBehaviourType();
			List<string> options = new List<string>
			{
				"None", "Item", "Animation", "Button", "Turnable", "Dial", "Zoomable", "Slidable", "Rotatable", "Draggable",
				"Lookable", "Token"
			};
			List<string> optionsTooltips = new List<string>
			{
				"No special behaviour.", "Provides an item that can be picked up by the player and stored in inventory.", "Used to move the prop from the current position to a waypoint.", "An animation the player can hover over and start with a click.", "Makes the prop rotatable, move the mouse vertically and horizontally across the screen.", "Makes the prop rotatable, move the mouse around the prop center.", "Enables zooming in on an object without picking it up.", "Makes the prop movable between two or more nodes.", "Makes the prop rotatable on all axes.", "Makes the prop Draggable, move the mouse to drag the object.",
				"Lookable allows you to trigger actions based if player is looking at it.", "This prop acts as a collectable token"
			};
			Func<int, BehaviourType> getBehaviourType = (int value) => (BehaviourType)value;
			if (isLight || isWall)
			{
				initValue = ((switch3d != null && switch3d.isAnimation) ? 2 : 0);
				options = new List<string> { "None", "Animation" };
				getBehaviourType = (int value) => (value != 0) ? BehaviourType.Animation : BehaviourType.None;
				optionsTooltips = new List<string> { "No special behaviour.", "Used to move the prop from the current position to a waypoint." };
			}
			defineEnumProperty("Behaviour", "Defines what kind of interactions this prop has with player or other props in the room.", initValue, options, delegate(int value)
			{
				BehaviourType num8 = getBehaviourType(initValue);
				BehaviourType behaviourType = getBehaviourType(value);
				int num9 = 0;
				if (num8 == BehaviourType.Item && behaviourType != BehaviourType.Item)
				{
					List<EditorAction> list = new List<EditorAction>();
					actionsRemoveFromTargeters(list, targetInstance);
					undo.push(list.ToArray());
					num9 += list.Count;
				}
				switch (behaviourType)
				{
				case BehaviourType.None:
					num9 += removeInteraction();
					break;
				case BehaviourType.Item:
					if (item != null)
					{
						return;
					}
					num9 += removeInteraction();
					var (prev, propData) = prepChangeData(targetInstance);
					propData.item.Add(getDefaultItemData(targetInstance));
					clearInteractionFoldCache(targetInstance);
					undo.push(actionChangeProp(prev, propData, "add item"));
					num9++;
					break;
				case BehaviourType.Button:
					if (switch3d != null && !switch3d.isAnimation)
					{
						UnityEngine.Debug.LogError("Cannot add button behaviour to a prop that already has a button behaviour.");
						return;
					}
					num9 += removeInteraction();
					var (prev3, propData3) = prepChangeData(targetInstance);
					propData3.switch3D.Add(getDefaultSwitch3dData(targetInstance, isAnimation: false));
					propData3.tweenState.Add(getDefaultTweenStateData(targetInstance));
					clearInteractionFoldCache(targetInstance);
					undo.push(actionChangeProp(prev3, propData3, "add switch3d button"));
					num9++;
					break;
				case BehaviourType.Animation:
					if (switch3d != null && switch3d.isAnimation)
					{
						UnityEngine.Debug.LogError("Cannot add animation behaviour to a prop that already has an animation behaviour.");
						return;
					}
					num9 += removeInteraction();
					var (prev2, propData2) = prepChangeData(targetInstance);
					propData2.tweenState.Add(getDefaultTweenStateData(targetInstance));
					propData2.switch3D.Add(getDefaultSwitch3dData(targetInstance, isAnimation: true));
					clearInteractionFoldCache(targetInstance);
					undo.push(actionChangeProp(prev2, propData2, "add switch3d animation"));
					num9++;
					break;
				case BehaviourType.Turnable:
					if (targetInstance.GetComponent<Turnable>() != null)
					{
						return;
					}
					num9 += removeInteraction();
					addInteraction(delegate(PropData next)
					{
						next.turnable.Add(getDefaultTurnableData());
						clearInteractionFoldCache(targetInstance);
					}, "add turnable");
					num9++;
					break;
				case BehaviourType.Rotatable:
					if (targetInstance.GetComponent<Rotatable>() != null)
					{
						return;
					}
					num9 += removeInteraction();
					addInteraction(delegate(PropData next)
					{
						next.rotatable.Add(getDefaultRotatableData());
						clearInteractionFoldCache(targetInstance);
					}, "add rotatable");
					num9++;
					break;
				case BehaviourType.Draggable:
					if (targetInstance.GetComponent<Draggable>() != null)
					{
						return;
					}
					num9 += removeInteraction();
					addInteraction(delegate(PropData next)
					{
						next.draggable.Add(getDefaultDraggableData());
						clearInteractionFoldCache(targetInstance);
					}, "add draggable");
					num9++;
					break;
				case BehaviourType.Dial:
					if (targetInstance.GetComponent<Dial>() != null)
					{
						return;
					}
					num9 += removeInteraction();
					addInteraction(delegate(PropData next)
					{
						next.dial.Add(getDefaultDialData());
						clearInteractionFoldCache(targetInstance);
					}, "add dial");
					num9++;
					break;
				case BehaviourType.Zoomable:
					if (targetInstance.GetComponent<Zoomable>() != null)
					{
						return;
					}
					num9 += removeInteraction();
					addInteraction(delegate(PropData next)
					{
						next.zoomable.Add(getDefaultZoomableData());
						clearInteractionFoldCache(targetInstance);
					}, "add zoomable");
					num9++;
					break;
				case BehaviourType.Slidable:
					if (targetInstance.GetComponent<Slidable>() != null)
					{
						return;
					}
					num9 += removeInteraction();
					addInteraction(delegate(PropData next)
					{
						next.slidable.Add(getDefaultSlidableData(targetInstance));
						clearInteractionFoldCache(targetInstance);
					}, "add slidable");
					num9++;
					break;
				case BehaviourType.Lookable:
					if (targetInstance.GetComponent<Lookable>() != null)
					{
						return;
					}
					num9 += removeInteraction();
					addInteraction(delegate(PropData next)
					{
						next.lookable.Add(getDefaultLookableData(targetInstance));
						clearInteractionFoldCache(targetInstance);
					}, "add lookable");
					num9++;
					break;
				case BehaviourType.Token:
					if (targetInstance.GetComponent<Token>() != null)
					{
						return;
					}
					num9 += removeInteraction();
					addInteraction(delegate(PropData next)
					{
						next.token.Add(getDefaultTokenData());
						clearInteractionFoldCache(targetInstance);
					}, "add token");
					num9++;
					break;
				}
				if (num9 > 1)
				{
					undo.linkLast(num9);
				}
			}, useInterpolationPopup: false, optionsTooltips);
		}
		void buildDial()
		{
			if (!(dial == null) && cache.isInteractionShown)
			{
				Vector3[] axis3D = new Vector3[6]
				{
					Vector3.right,
					Vector3.left,
					Vector3.up,
					Vector3.down,
					Vector3.forward,
					Vector3.back
				};
				List<string> options = new List<string> { "Right", "Left", "Up", "Down", "Forward", "Back" };
				int initValue = Mathf.Max(0, Array.IndexOf(axis3D, dial.rotationAxis));
				defineEnumProperty("World Axis", "Determines along which axis the prop will rotate.", initValue, options, delegate(int index)
				{
					var (prev, propData) = prepChangeData(targetInstance);
					getDialData(propData).rotationAxis = axis3D[index];
					undo.push(actionChangeProp(prev, propData, "Dial Rotation Axis"));
				});
				defineIntProperty("Value Count", "Determines the amount of dials steps.\nShouldn't be larger than Value Output Range (gives zero).\nUsually you need to set it to the same number as the Output Value Range.\nMake it double the Output Value Range to only get even numbers.", () => dial.valueCount, delegate(int value)
				{
					dial.valueCount = value;
				}, delegate(int prevValue, int nextValue)
				{
					var (propData, next) = prepChangeData(targetInstance);
					getDialData(propData).valueCount = prevValue;
					undo.push(actionChangeProp(propData, next, "Dial valueCount"));
				}, null, 1, int.MaxValue, 0.075f);
				defineBoolProperty("Snap To Position", "When checked, the dial will snap to the closest rotation when released.", dial.snapToPosition, delegate(bool value)
				{
					var (prev, propData) = prepChangeData(targetInstance);
					getDialData(propData).snapToPosition = value;
					undo.push(actionChangeProp(prev, propData, "Dial snapToPosition"));
				});
				defineIntProperty("Click Direction", "Determines the direction of the mouse click needed to rotate the dial.", () => dial.clickDirection, delegate(int value)
				{
					dial.clickDirection = value;
				}, delegate(int prevValue, int nextValue)
				{
					var (propData, next) = prepChangeData(targetInstance);
					getDialData(propData).clickDirection = prevValue;
					undo.push(actionChangeProp(propData, next, "Dial click direction"));
				}, null, -1, 1, 0.075f);
				defineBoolProperty("Use Angle Limits", "Determines whether the prop will be limited to a certain angle range.\nIf disabled, the prop will rotate freely.", dial.useAngleLimits, delegate(bool value)
				{
					var (prev, propData) = prepChangeData(targetInstance);
					getDialData(propData).useAngleLimits = value;
					undo.push(actionChangeProp(prev, propData, "Dial use angle limits"));
				});
				if (dial.useAngleLimits)
				{
					defineFloatProperty("Angle Limit Top", "Upper angle limit.", () => dial.angleLimit1, delegate(float value)
					{
						dial.angleLimit1 = value;
					}, delegate(float prevValue, float nextValue)
					{
						var (propData, next) = prepChangeData(targetInstance);
						getDialData(propData).angleLimit1 = prevValue;
						undo.push(actionChangeProp(propData, next, "Dial angle limit 1"));
					}, null, 0f, 360f, 0.01f);
					defineFloatProperty("Angle Limit Bot", "Lower angle limit.", () => dial.angleLimit2, delegate(float value)
					{
						dial.angleLimit2 = value;
					}, delegate(float prevValue, float nextValue)
					{
						var (propData, next) = prepChangeData(targetInstance);
						getDialData(propData).angleLimit2 = prevValue;
						undo.push(actionChangeProp(propData, next, "Dial angle limit 2"));
					}, null, 0f, 360f, 0.01f);
				}
				defineLockLinksProperty("%RE_locks%", "%RE_dialLocksTooltip%", dial.locks, targetInstance, (PropData x) => getDialData(x).locks);
			}
		}
		void buildDraggable()
		{
			List<string> list = new List<string>();
			foreach (object value in Enum.GetValues(typeof(RigidbodyConstraints)))
			{
				list.Add(UnityUtils.addSpacesBeforeCapitalLetters(value.ToString()));
			}
			defineEnumProperty("Constraints", "Determines the constraints of the draggable prop.\nNone - no constraints, the prop can be dragged anywhere.", (int)draggable.dragConstraintFlags, list, delegate(int index)
			{
				var (prev, propData) = prepChangeData(targetInstance);
				getDraggableData(propData).dragConstraintFlags = (RigidbodyConstraints)index;
				undo.push(actionChangeProp(prev, propData, "Draggable Constraints"));
			});
			List<string> list2 = new List<string>();
			foreach (object value2 in Enum.GetValues(typeof(Draggable.ManipulationType)))
			{
				list2.Add(UnityUtils.addSpacesBeforeCapitalLetters(value2.ToString()));
			}
			defineEnumProperty("Manipulation Type", "Determines how the draggable is manipulated.", (int)draggable.manipulationType, list2, delegate(int index)
			{
				var (prev, propData) = prepChangeData(targetInstance);
				getDraggableData(propData).manipulationType = (Draggable.ManipulationType)index;
				undo.push(actionChangeProp(prev, propData, "Draggable Manipulation"));
			});
			List<string> options = new List<string> { "Hit Point", "Center of Mass" };
			defineEnumProperty("Force Point", "Determinates from where the draggable is dragged.", (int)draggable.forcePoint, options, delegate(int index)
			{
				var (prev, propData) = prepChangeData(targetInstance);
				getDraggableData(propData).forcePoint = (Draggable.ForcePoint)index;
				undo.push(actionChangeProp(prev, propData, "Draggable Force"));
			});
		}
		void buildItem()
		{
			defineEnumProperty("Item Importance", "Defines how important this item is to this room's puzzles.", optionsTooltips: new List<string> { "Common items can be picked up and stored in inventory. Use this if it doesn't have any special meaning.", "Marked with a key symbol. It is usually required to finish the room.", "Marked with a book symbol. It means it can provide hints or lore." }, initValue: (int)item.itemType, options: new List<string> { "Common", "Key", "Hint" }, onSubmit: delegate(int value)
			{
				var (prev, propData) = prepChangeData(targetInstance);
				getItemData(propData).itemType = (ItemType)value;
				undo.push(actionChangeProp(prev, propData, "pickableType"));
			});
			defineBoolProperty("Simulate Physics", "When checked, the item will have a Rigidbody component and will be affected by physics.\nWhen not checked, the item will not have a Rigidbody component and will not be affected by physics.", item.hasRigidbody, delegate(bool value)
			{
				var (prev, propData) = prepChangeData(targetInstance);
				getItemData(propData).hasRigidbody = value;
				undo.push(actionChangeProp(prev, propData, "Item - has rigidbody"));
			});
			defineBoolProperty("Is Carriable", "Is this item carriable or just a regular item?", item.carriable, delegate(bool value)
			{
				var (prev, propData) = prepChangeData(targetInstance);
				getItemData(propData).carriable = value;
				undo.push(actionChangeProp(prev, propData, "Item - carriable"));
			});
		}
		void buildLookable()
		{
			defineFloatProperty("Target Visibility Percent", "Determines the percent of the prop that should be visible to trigger the lookable.", () => lookable.targetVisibilityPercent, delegate(float value)
			{
				lookable.targetVisibilityPercent = value;
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, next) = prepChangeData(targetInstance);
				getLookableData(propData).targetVisibilityPercent = prevValue;
				undo.push(actionChangeProp(propData, next, "lookable targetVisibilityPercent"));
			}, null, 0f, 1f, 0.01f);
			defineFloatProperty("Target Screen Percent", "Determines the percent of the prop that should be visible on the screen to trigger the lookable.", () => lookable.targetScreenPercent, delegate(float value)
			{
				lookable.targetScreenPercent = value;
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, next) = prepChangeData(targetInstance);
				getLookableData(propData).targetScreenPercent = prevValue;
				undo.push(actionChangeProp(propData, next, "lookable targetScreenPercent"));
			}, null, 0f, 1f, 0.01f);
			defineBoolProperty("Trigger Only Once", "When checked, the lookable will only trigger once when the player looks at it.", lookable.activateOnlyOnce, delegate(bool value)
			{
				var (prev, propData) = prepChangeData(targetInstance);
				getLookableData(propData).activateOnlyOnce = value;
				undo.push(actionChangeProp(prev, propData, "Lookable activateOnlyOnce"));
			});
			List<PropInstance> list = new List<PropInstance>();
			foreach (GameObject item4 in lookable.onActivated)
			{
				list.Add(item4.GetComponent<PropInstance>());
			}
			defineTargetsProperty("On Activated", "What to do when the player looks at this prop.", PrepChangeDataStrategy.UseTarget, delegate(PropData prev, PropData next, PropInstance value)
			{
				getLookableData(next).onActivated.Add(value.ID);
			}, delegate(PropData prev, PropData next, int index)
			{
				getLookableData(next).onActivated.RemoveAt(index);
			}, targetInstance, defaultTargetPredicate(targetInstances[0], lookable.onActivated, withLock: false), list, -1);
			List<PropInstance> list2 = new List<PropInstance>();
			foreach (GameObject item5 in lookable.onDeactivated)
			{
				list2.Add(item5.GetComponent<PropInstance>());
			}
			defineTargetsProperty("On Deactivated", "What to do when the player stops looking at this prop.", PrepChangeDataStrategy.UseTarget, delegate(PropData prev, PropData next, PropInstance value)
			{
				getLookableData(next).onDeactivated.Add(value.ID);
			}, delegate(PropData prev, PropData next, int index)
			{
				getLookableData(next).onDeactivated.RemoveAt(index);
			}, targetInstance, defaultTargetPredicate(targetInstances[0], lookable.onDeactivated, withLock: false), list2, -1);
		}
		void buildRotatable()
		{
			defineFloatProperty("Rotation Speed", "Determines how fast the prop will rotate.", () => rotatable.speed, delegate(float value)
			{
				rotatable.speed = value;
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, next) = prepChangeData(targetInstance);
				getRotatableData(propData).speed = prevValue;
				undo.push(actionChangeProp(propData, next, "Rotatable Speed"));
			}, null, 0.001f, float.PositiveInfinity, 0.01f);
		}
		void buildShared()
		{
			Interactive component28 = targetInstance.GetComponent<Interactive>();
			if (!(component28 == null) && (!(component28 is Switch3D) || !switch3d.isAnimation) && !(component28.transform.parent.GetComponentInParent<Interactive>() == null))
			{
				defineEnumProperty("Click Priority", "Defines which interactive gets priority when clicking on overlapping interactives.", optionsTooltips: new List<string> { "This interactive will have the same click priority as its parent interactive.", "This interactive will have higher click priority than its parent interactive." }, initValue: (int)component28.roomEditorTargetPriotity, options: new List<string> { "Same As Parent", "Higher Than Parent" }, onSubmit: delegate(int value)
				{
					(PropData prev, PropData next) tuple = prepChangeData(targetInstance);
					PropData item2 = tuple.prev;
					PropData item3 = tuple.next;
					item3.targetPriority = (RoomEditorTargetPriotity)value;
					undo.push(actionChangeProp(item2, item3, "Room Editor Target Priority"));
				});
			}
		}
		void buildSlidable()
		{
			destroySlidablePreview(targetInstance);
			createSlidablePreview(targetInstance);
			TransformData transformData = saveTransform(slidable.endNode, isLocal: true);
			TransformData originalPivot = targetInstance.originalPivot;
			Action<float> onScrub = ((!(transformData.position != originalPivot.position) && !(transformData.rotation != originalPivot.rotation) && !(transformData.scale != originalPivot.scale)) ? null : ((Action<float>)delegate(float x)
			{
				destroySlidablePreview(targetInstance);
				slidable.setParentOfNodes(shouldSlidableBeParent: false);
				slidable.setAtPercent(x);
				slidable.setParentOfNodes(shouldSlidableBeParent: true);
				createSlidablePreview(targetInstance);
				scrubbingInteractives.Add(slidable);
			}));
			defineTargetProperty("%editEndNode%", "%editEndNodeTooltip%", modified: false, delegate
			{
				toggleSlidablePivotMode(show: true);
			}, onScrub, slidable.value);
			defineEnumProperty("Snap Mode", "Determines the snap mode when the player releases the prop.", optionsTooltips: new List<string> { "No snapping will be performed on release.", "Snap to the closest snap point on release.", "Snap to the starting point on release." }, initValue: (int)slidable.snapMode, options: new List<string> { "No Snapping", "Closest", "Starting" }, onSubmit: delegate(int value)
			{
				var (prev, propData) = prepChangeData(targetInstance);
				getSlidableData(propData).snapMode = (Slidable.SnapMode)value;
				undo.push(actionChangeProp(prev, propData, "Slidable snap mode"));
			});
			if (slidable.snapMode != Slidable.SnapMode.DontSnap)
			{
				defineIntProperty("Additional Snap Points", "Number of additional snap points between nodes. If zero, snap is performed between start and end nodes.", () => slidable.additionalSnapPointCount, delegate(int value)
				{
					slidable.additionalSnapPointCount = value;
				}, delegate(int prevValue, int nextValue)
				{
					var (propData, propData2) = prepChangeData(targetInstance);
					getSlidableData(propData).additionalSnappingPoints = prevValue;
					int num9 = slidable.additionalSnapPointCount + 1;
					if (num9 < slidable.snapPointIndex)
					{
						getSlidableData(propData2).startingIndex = (slidable.snapPointIndex = num9);
					}
					undo.push(actionChangeProp(propData, propData2, "Slidable Additional Snap Points"));
				}, null, 0);
				defineFloatProperty("Snap Animation Duration", "Snap animation duration in seconds. If zero, snapping will not be animated.", () => slidable.snapAnimationDuration, delegate(float value)
				{
					slidable.snapAnimationDuration = value;
				}, delegate(float prevValue, float nextValue)
				{
					var (propData, next) = prepChangeData(targetInstance);
					getSlidableData(propData).snapAnimationDuration = prevValue;
					undo.push(actionChangeProp(propData, next, "Slidable Snap Animation Duration"));
				}, null, 0f, float.PositiveInfinity, 0.01f);
			}
			List<LockArgument> lockArgs = safeLocks(slidable.locks);
			List<TargetArrayEntry> list = new List<TargetArrayEntry>(lockArgs.Count);
			for (int num8 = 0; num8 < lockArgs.Count; num8++)
			{
				LockArgument lockArgument = lockArgs[num8];
				TargetArrayEntry targetArrayEntry = getTargetArrayEntry(lockArgument.targetLock.gameObject);
				targetArrayEntry.index = lockArgument.targetIndex;
				list.Add(targetArrayEntry);
			}
			defineTargetArrayProperty("%RE_locks%", "%slidable_locks%", list, -1, delegate
			{
				enterMultiTargetMode(targetLockPredicate, delegate(HashSet<PropInstance> pis, HashSet<(PropInstance, int)> passIndexes)
				{
					var (prev, propData) = prepChangeData(targetInstance);
					foreach (var passIndex in passIndexes)
					{
						PropInstance item2 = passIndex.Item1;
						int item3 = passIndex.Item2;
						Lock component28 = item2.GetComponent<Lock>();
						if (!lockArgs.Exists(lockPredicate(component28, item3)))
						{
							getSlidableData(propData).locks.Add(new LockArgData
							{
								instanceID = item2.ID,
								passwordIndex = item3
							});
						}
					}
					undo.push(actionChangeProp(prev, propData, "Slidable Add Locks"));
				}, passwordPredicateNeo(slidable.locks));
			}, delegate(int index)
			{
				GameObject gameObject = lockArgs[index].targetLock.gameObject;
				if (gameObject != null && gameObject.TryGetComponent<PropInstance>(out var component28))
				{
					undo.push(actionChangeSelection(newInstanceSelection(component28.ID)));
					moveCameraTo(gameObject);
				}
			}, delegate(int index)
			{
				var (prev, propData) = prepChangeData(targetInstance);
				getSlidableData(propData).locks.RemoveAt(index);
				undo.push(actionChangeProp(prev, propData, "Slidable Remove Lock"));
			});
		}
		void buildSwitch3d()
		{
			List<Switch3DType> listValues = new List<Switch3DType>
			{
				Switch3DType.Flip,
				Switch3DType.Click,
				Switch3DType.Loop,
				Switch3DType.ClickLoop,
				Switch3DType.Hold,
				Switch3DType.OnlyOnce
			};
			List<string> list = new List<string> { "Tick", "Tick-Tock", "Tick Loop", "Tick-Tock Loop", "Hold", "Only Once" };
			List<string> list2 = new List<string> { "(A→B) Acts as a lever - when activated, flips to the other side.", "(A→B→A) Acts as a keyboard button - when activated, goes in and returns back.", "(A→B A→B) Lerps to the goal, instantly teleported to 0 and loops to goal again indefinitely.", "(A→B→A→B→A) Lerps to the goal, lerps back to 0 and then to goal again indefinitely.", "Acts as a spring - while held, it is being extended, when released returns back to initial position.", "Lerps to the goal, stays there, and ignores any further input." };
			if (switch3d.isAnimation)
			{
				list.Remove("Hold");
				listValues.Remove(Switch3DType.Hold);
				list2.RemoveAt(4);
			}
			if (customModel != null)
			{
				defineHintProperty("On Custom Models, you define animation you want to use in the Custom Model component.");
			}
			RoomEditor roomEditor = this;
			string label = (switch3d.isAnimation ? "Animation Type" : "Button Type");
			List<string> optionsTooltips = list2;
			roomEditor.defineEnumProperty(label, "Defines the animation behaviour of this prop.", listValues.IndexOf(switch3d.switchType), list, delegate(int value)
			{
				var (prev, propData) = prepChangeData(targetInstance);
				getSwitch3DData(propData).type = listValues[value];
				undo.push(actionChangeProp(prev, propData, "switch type"));
			}, useInterpolationPopup: false, optionsTooltips);
			defineFloatProperty("Duration", "Defines how long the animation lasts.", () => switch3d.duration, delegate(float value)
			{
				switch3d.duration = value;
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, next) = prepChangeData(targetInstance);
				getSwitch3DData(propData).duration = prevValue;
				undo.push(actionChangeProp(propData, next, "Switch3d duration"));
			}, null, 0f, float.PositiveInfinity, 0.01f);
			Switch3DType switchType = switch3d.switchType;
			if (switchType == Switch3DType.Loop || switchType == Switch3DType.ClickLoop)
			{
				defineFloatProperty("Pause", "Defines pause between loops.", () => switch3d.pauseOnLoop, delegate(float value)
				{
					switch3d.pauseOnLoop = value;
				}, delegate(float prevValue, float nextValue)
				{
					var (propData, next) = prepChangeData(targetInstance);
					getSwitch3DData(propData).pauseOnLoop = prevValue;
					undo.push(actionChangeProp(propData, next, "Switch3d pause"));
				}, null, 0f, float.PositiveInfinity, 0.01f);
			}
			if (switch3d.isAnimation)
			{
				defineBoolProperty("Autoplay", "Should the animation start automatically when the room is loaded?", switch3d.autoplay, delegate(bool value)
				{
					var (prev, propData) = prepChangeData(targetInstance);
					getSwitch3DData(propData).autoplay = value;
					undo.push(actionChangeProp(prev, propData, "Switch3d autoplay"));
				});
			}
			Action<float> onScrub = null;
			List<TweenState.TweenStateRecord> tweenStateDataNeo = switch3d.tweenState.tweenStateDataNeo;
			if (tweenStateDataNeo != null && tweenStateDataNeo.Count > 0)
			{
				switch3d.tweenState.init();
				switch3d.tweenState.setWeight("Down", 0f);
				onScrub = delegate(float t)
				{
					switch3d.tweenState.setWeight("Down", t);
					scrubbingInteractives.Add(switch3d);
				};
			}
			if (customModel == null)
			{
				defineTargetProperty("Edit Waypoint", "Set the endpoint position and rotation of the movement.", modified: false, delegate
				{
					toggleTweenStateMode(show: true);
				}, onScrub);
			}
			List<string> list3 = new List<string>();
			foreach (object value3 in Enum.GetValues(typeof(Interpolation)))
			{
				list3.Add(UnityUtils.addSpacesBeforeCapitalLetters(value3.ToString()));
			}
			defineEnumProperty("Interpolation", "Defines the interpolation mode between start and end nodes", (int)switch3d.tweenState.interpolation, list3, delegate(int index)
			{
				var (prev, propData) = prepChangeData(targetInstance);
				getSwitch3DData(propData).interpolation = (Interpolation)index;
				undo.push(actionChangeProp(prev, propData, "Switch3d interpolation"));
			}, useInterpolationPopup: true);
			string pickerTitle = (switch3d.isAnimation ? "On Done" : "On Press");
			defineRoomEditorLinksProperty("OnDone Output value", "Number that will be sent to the targeted props on correct completion.", pickerTitle, "%RE_switchOnPressTooltip%", switch3d.onLinks, targetInstance, (PropData x) => getSwitch3DData(x).onLinks, defaultTargetPredicate(targetInstances[0], switch3d.onLinks.targets, withLock: true));
			defineRoomEditorLinksProperty("On Return Output value", "Number that will be sent to the targeted props on returning.", "On Return", "%RE_switchOnPressTooltip%", switch3d.offLinks, targetInstance, (PropData x) => getSwitch3DData(x).offLinks, defaultTargetPredicate(targetInstances[0], switch3d.offLinks.targets, withLock: true));
		}
		void buildToken()
		{
			defineIntProperty("ID", "Used to sort tokens by ID when the level starts. Collected token IDs are saved for each player.", () => token.roomEditorId, delegate(int value)
			{
				token.roomEditorId = value;
			}, delegate(int prevValue, int nextValue)
			{
				var (propData, next) = prepChangeData(targetInstance);
				getTokenData(propData).id = prevValue;
				undo.push(actionChangeProp(propData, next, "Token ID change"));
			});
		}
		void buildTurnable()
		{
			if (cache.isInteractionShown)
			{
				Vector2[] axis2D = new Vector2[8]
				{
					new Vector2(1f, 0f),
					new Vector2(-1f, 0f),
					new Vector2(0f, 1f),
					new Vector2(0f, -1f),
					new Vector2(1f, 1f),
					new Vector2(-1f, 1f),
					new Vector2(-1f, -1f),
					new Vector2(1f, -1f)
				};
				List<string> options = new List<string> { "Right", "Left", "Up", "Down", "Right Up", "Left Up", "Left Down", "Right Down" };
				Vector3[] axis3D = new Vector3[6]
				{
					Vector3.right,
					Vector3.left,
					Vector3.up,
					Vector3.down,
					Vector3.forward,
					Vector3.back
				};
				List<string> options2 = new List<string> { "Right", "Left", "Up", "Down", "Forward", "Back" };
				int initValue = Mathf.Max(0, Array.IndexOf(axis3D, turnable.worldAxis));
				defineEnumProperty("Turnable Type", "Determines how the prop will be turned.", (int)turnable.type, new List<string> { "Rotation", "UV" }, delegate(int index)
				{
					var (prev, propData) = prepChangeData(targetInstance);
					getTurnableData(propData).type = (TurnableType)index;
					undo.push(actionChangeProp(prev, propData, "Turnable Type."));
				}, useInterpolationPopup: false, new List<string> { "The prop will rotate.", "The prop will change it's UVs." });
				defineFloatProperty("Rotation Speed", "Determines how fast the prop will rotate.", () => turnable.speed, delegate(float value)
				{
					turnable.speed = value;
				}, delegate(float prevValue, float nextValue)
				{
					var (propData, next) = prepChangeData(targetInstance);
					getTurnableData(propData).speed = prevValue;
					undo.push(actionChangeProp(propData, next, "Turnable Speed"));
				}, null, 0.001f, float.PositiveInfinity, 0.01f);
				defineEnumProperty("World Axis", "Determines along which axis the prop will rotate.", initValue, options2, delegate(int index)
				{
					var (prev, propData) = prepChangeData(targetInstance);
					getTurnableData(propData).worldAxis = axis3D[index];
					undo.push(actionChangeProp(prev, propData, "Turnable World Axis"));
				});
				initValue = Mathf.Max(0, Array.IndexOf(axis2D, turnable.screenAxis));
				defineEnumProperty("Screen Axis", "Determines the direction of the mouse movement needed to rotate the prop and increase the value.\nRight - Moving the mouse to the Right increases it's value.\nRight Up - Moving the mouse to the Right or Up increases the value.\n...", initValue, options, delegate(int index)
				{
					var (prev, propData) = prepChangeData(targetInstance);
					getTurnableData(propData).screenAxis = axis2D[index];
					undo.push(actionChangeProp(prev, propData, "Turnable Screen Axis"));
				});
				defineIntProperty("Steps", "Determines the number of snap positions.\nOutput value goes from 0 to Steps - 1.", () => turnable.steps, delegate(int value)
				{
					turnable.steps = value;
				}, delegate(int prevValue, int nextValue)
				{
					var (propData, next) = prepChangeData(targetInstance);
					getTurnableData(propData).steps = prevValue;
					undo.push(actionChangeProp(propData, next, "Turnable steps"));
				}, null, 1, int.MaxValue, 0.075f);
				defineBoolProperty("Use Hotspots", "Determines whether the prop will use hotspots to snap to positions.\nIf disabled, the prop will rotate smoothly.", turnable.useHotspots, delegate(bool value)
				{
					var (prev, propData) = prepChangeData(targetInstance);
					getTurnableData(propData).useHotspots = value;
					undo.push(actionChangeProp(prev, propData, "Turnable use hotspots"));
				});
				defineBoolProperty("Use Angle Limits", "Determines whether the prop will be limited to a certain angle range.\nIf disabled, the prop will rotate freely.", turnable.useAngleLimits, delegate(bool value)
				{
					var (prev, propData) = prepChangeData(targetInstance);
					getTurnableData(propData).useAngleLimits = value;
					undo.push(actionChangeProp(prev, propData, "Turnable use angle limits"));
				});
				if (turnable.useAngleLimits)
				{
					defineFloatProperty("Angle Limit Top", "Upper angle limit.", () => turnable.angleLimit1, delegate(float value)
					{
						turnable.angleLimit1 = value;
					}, delegate(float prevValue, float nextValue)
					{
						var (propData, next) = prepChangeData(targetInstance);
						getTurnableData(propData).angleLimit1 = prevValue;
						undo.push(actionChangeProp(propData, next, "Turnable angle limit 1"));
					}, null, 0f, 360f, 0.01f);
					defineFloatProperty("Angle Limit Bot", "Lower angle limit.", () => turnable.angleLimit2, delegate(float value)
					{
						turnable.angleLimit2 = value;
					}, delegate(float prevValue, float nextValue)
					{
						var (propData, next) = prepChangeData(targetInstance);
						getTurnableData(propData).angleLimit2 = prevValue;
						undo.push(actionChangeProp(propData, next, "Turnable angle limit 2"));
					}, null, 0f, 360f, 0.01f);
				}
				defineIntProperty("Click Direction", "Determines the direction of the mouse click needed to rotate the turnable.", () => turnable.clickDirection, delegate(int value)
				{
					turnable.clickDirection = value;
				}, delegate(int prevValue, int nextValue)
				{
					var (propData, next) = prepChangeData(targetInstance);
					getTurnableData(propData).clickDirection = prevValue;
					undo.push(actionChangeProp(propData, next, "Turnable click direction"));
				}, null, -1, 1, 0.075f);
				defineLockLinksProperty("%RE_locks%", "%RE_turableLocksTooltip%", turnable.locks, targetInstance, (PropData x) => getTurnableData(x).locks);
			}
		}
		void buildZoomable()
		{
			if (cache.isInteractionShown)
			{
				defineButtonProperty("Edit Rotation", "Set the camera position of the 'Zoomable' the players will have while they are using it.", delegate
				{
					toggleZoomableRotationMode(show: true);
				});
			}
		}
		void clearInteractionFoldCache(PropInstance instance)
		{
			getInstanceUICache(instance).isInteractionShown = true;
		}
		BehaviourType getSelectedBehaviourType()
		{
			if (item != null)
			{
				return BehaviourType.Item;
			}
			if (switch3d != null && !switch3d.isAnimation)
			{
				return BehaviourType.Button;
			}
			if (switch3d != null && switch3d.isAnimation)
			{
				return BehaviourType.Animation;
			}
			if (turnable != null)
			{
				return BehaviourType.Turnable;
			}
			if (rotatable != null)
			{
				return BehaviourType.Rotatable;
			}
			if (dial != null)
			{
				return BehaviourType.Dial;
			}
			if (zoomable != null)
			{
				return BehaviourType.Zoomable;
			}
			if (slidable != null)
			{
				return BehaviourType.Slidable;
			}
			if (draggable != null)
			{
				return BehaviourType.Draggable;
			}
			if (lookable != null)
			{
				return BehaviourType.Lookable;
			}
			if (token != null)
			{
				return BehaviourType.Token;
			}
			return BehaviourType.None;
		}
		static bool instanceIsSpecial(PropInstance instance)
		{
			if (!(instance.GetComponent<Lock>() != null) && !(instance.GetComponent<Slot>() != null) && !(instance.GetComponent<Trigger>() != null) && !(instance.GetComponent<Obstacle>() != null) && !(instance.GetComponent<EditorEmpty>() != null) && !(instance.GetComponent<SpawnPoint>() != null) && !(instance.GetComponent<Finish>() != null) && !(instance.GetComponent<Teleport>() != null) && !(instance.GetComponent<Fog>() != null) && !(instance.GetComponent<Sound>() != null) && !(instance.GetComponent<EditorSkybox>() != null) && !(instance.GetComponent<OpenLink>() != null) && !(instance.GetComponent<ActivatorComponent>() != null) && !(instance.GetComponent<EditorCollider>() != null) && !(instance.GetComponent<EditorPostProcessing>() != null) && !(instance.GetComponent<EditorPuzzle>() != null))
			{
				return instance.GetComponent<Roulette>() != null;
			}
			return true;
		}
		int removeInteraction()
		{
			int num8 = 0;
			if (item != null)
			{
				var (prev, propData) = prepChangeData(targetInstance);
				propData.item.Clear();
				undo.push(actionChangeProp(prev, propData, "Remove Item"));
				num8++;
			}
			if (switch3d != null)
			{
				var (prev2, propData2) = prepChangeData(targetInstance);
				propData2.switch3D.Clear();
				undo.push(actionChangeProp(prev2, propData2, "Remove Switch3D"));
				num8++;
			}
			if (tweenState != null)
			{
				var (prev3, propData3) = prepChangeData(targetInstance);
				propData3.tweenState.Clear();
				undo.push(actionChangeProp(prev3, propData3, "Remove TweenState"));
				num8++;
			}
			if (rotatable != null)
			{
				var (prev4, propData4) = prepChangeData(targetInstance);
				propData4.rotatable.Clear();
				undo.push(actionChangeProp(prev4, propData4, "Remove Rotatable"));
				num8++;
			}
			if (draggable != null)
			{
				var (prev5, propData5) = prepChangeData(targetInstance);
				propData5.draggable.Clear();
				undo.push(actionChangeProp(prev5, propData5, "Remove Draggable"));
				num8++;
			}
			if (turnable != null)
			{
				var (prev6, propData6) = prepChangeData(targetInstance);
				propData6.turnable.Clear();
				undo.push(actionChangeProp(prev6, propData6, "Remove Turnable"));
				num8++;
			}
			if (dial != null)
			{
				var (prev7, propData7) = prepChangeData(targetInstance);
				propData7.dial.Clear();
				undo.push(actionChangeProp(prev7, propData7, "Remove Dial"));
				num8++;
			}
			if (zoomable != null)
			{
				var (prev8, propData8) = prepChangeData(targetInstance);
				propData8.zoomable.Clear();
				undo.push(actionChangeProp(prev8, propData8, "Remove Zoomable"));
				num8++;
			}
			if (slidable != null)
			{
				if (slidable.startNode != null)
				{
					UnityEngine.Object.Destroy(slidable.startNode);
				}
				destroySlidablePreview(targetInstance);
				var (prev9, propData9) = prepChangeData(targetInstance);
				propData9.slidable.Clear();
				undo.push(actionChangeProp(prev9, propData9, "Remove Slidable"));
				num8++;
			}
			if (lookable != null)
			{
				var (prev10, propData10) = prepChangeData(targetInstance);
				propData10.lookable.Clear();
				undo.push(actionChangeProp(prev10, propData10, "Remove Lookable"));
				num8++;
			}
			if (token != null)
			{
				var (prev11, propData11) = prepChangeData(targetInstance);
				propData11.token.Clear();
				undo.push(actionChangeProp(prev11, propData11, "Remove Token"));
				num8++;
			}
			return num8;
		}
		bool targetParentPredicate(PropInstance pi)
		{
			if (pi.GetComponent<Slot>() != null)
			{
				return false;
			}
			List<PropInstance> childPropInstances2 = getChildPropInstances(targetInstances[0]);
			for (int i = 1; i < targetInstances.Count; i++)
			{
				getChildPropInstances(targetInstances[i], childPropInstances2);
			}
			return childPropInstances2.IndexOf(pi) < 0;
		}
	}

	private void recalculateHeightOfPropertiesUI()
	{
		Game.rebuildTexts(propertiesUI.Content);
		float y = editorUI.GetComponent<CanvasScaler>().referenceResolution.y;
		float x = propertiesUI.Background.rectTransform.sizeDelta.x;
		float num = propertiesUI.Content.sizeDelta.y + 50f;
		float y2 = Mathf.Min(y, num);
		propertiesUI.Background.rectTransform.sizeDelta = new Vector2(x, y2);
		if (num > y)
		{
			return;
		}
		float x2 = propertiesUI.Scrollbar.rectT().sizeDelta.x;
		for (int i = 0; i < propertiesUI.Content.childCount; i++)
		{
			Transform child = propertiesUI.Content.GetChild(i);
			if (child.gameObject.activeSelf && child.TryGetComponent<LayoutElement>(out var component))
			{
				component.preferredWidth += x2;
			}
		}
	}

	private static Predicate<PropInstance> defaultTargetPredicate(PropInstance selectedInstance, IEnumerable<GameObject> ignoredInstances, bool withLock)
	{
		return delegate(PropInstance instance)
		{
			if (instance == selectedInstance)
			{
				return false;
			}
			foreach (GameObject ignoredInstance in ignoredInstances)
			{
				if (ignoredInstance == instance.gameObject)
				{
					return false;
				}
			}
			if (withLock && instance.TryGetComponent<Lock>(out var _))
			{
				return true;
			}
			if (instance.TryGetComponent<Sound>(out var _))
			{
				return true;
			}
			if (instance.TryGetComponent<Teleport>(out var _))
			{
				return true;
			}
			if (instance.TryGetComponent<Roulette>(out var _))
			{
				return true;
			}
			if (instance.TryGetComponent<Finish>(out var _))
			{
				return true;
			}
			if (instance.TryGetComponent<OpenLink>(out var _))
			{
				return true;
			}
			if (instance.TryGetComponent<Switch3D>(out var component7) && component7.isAnimation)
			{
				return true;
			}
			if (instance.TryGetComponent<ActivatorComponent>(out var _))
			{
				return true;
			}
			if (instance.TryGetComponent<EditorPuzzle>(out var _))
			{
				return true;
			}
			if (instance.TryGetComponent<EditorPostProcessing>(out var _))
			{
				return true;
			}
			if (instance.TryGetComponent<EditorSkybox>(out var _))
			{
				return true;
			}
			if (instance.TryGetComponent<EditorClouds>(out var _))
			{
				return true;
			}
			if (instance.TryGetComponent<LuaExecutor>(out var component13) && component13.canBeTriggered)
			{
				return true;
			}
			if (instance.TryGetComponent<Fog>(out var _))
			{
				return true;
			}
			if (instance.TryGetComponent<EditorOcean>(out var _))
			{
				return true;
			}
			EditorDelay component16;
			return instance.TryGetComponent<EditorDelay>(out component16) ? true : false;
		};
	}

	private static Predicate<PropInstance> targetInListPredicate(PropInstance selectedInstance, IEnumerable<GameObject> onlyTargets)
	{
		return delegate(PropInstance instance)
		{
			if (instance == selectedInstance)
			{
				return false;
			}
			foreach (GameObject onlyTarget in onlyTargets)
			{
				if (onlyTarget == instance.gameObject)
				{
					return true;
				}
			}
			return false;
		};
	}

	private bool targetLockPredicate(PropInstance pi)
	{
		Lock component;
		return pi.TryGetComponent<Lock>(out component);
	}

	private bool targetEmptyPredicate(PropInstance pi)
	{
		EditorEmpty component;
		return pi.TryGetComponent<EditorEmpty>(out component);
	}

	private PasswordPredicate passwordPredicateNeo(List<LockArgument> ignoreLocks, bool zeroOneOnly = false)
	{
		return passwordPredicate(ignoreLocks.ToArray(), zeroOneOnly);
	}

	private PasswordPredicate passwordPredicate(LockArgument[] ignoreLocks, bool zeroOneOnly = false)
	{
		return (Lock lo, int index) => !Array.Exists(ignoreLocks, (LockArgument x) => x.targetLock == lo && x.targetIndex == index) && (!zeroOneOnly || lo.password[index] <= 1);
	}

	private void refreshTransformUI(PropInstance instance)
	{
		if (isAnyLinkedPolygonSelected())
		{
			return;
		}
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Transform", "Defines where this prop is positioned, how much it is rotated and how scaled it is.", cache.isTransformShown, delegate(bool isShown)
		{
			cache.isTransformShown = isShown;
		});
		if (cache.isTransformShown)
		{
			Transform targetTransform = instance.transform;
			defineVector3Property("Position", "Local position of this prop (relative to parent).\nWhen changing the position of a parent prop all the children props move as well.", () => targetTransform.localPosition, delegate(Vector3 value)
			{
				targetTransform.localPosition = value;
			}, delegate
			{
				undo.push(actionChangeTransform(instance, "Position"));
			}, null, 0.01f);
			defineVector3Property("Rotation", "Local rotation of this prop (relative to parent).\nWhen changing the rotation of a parent prop all the children props rotate as well.", () => targetTransform.localRotation.eulerAngles, delegate(Vector3 value)
			{
				targetTransform.localRotation = Quaternion.Euler(value);
			}, delegate
			{
				undo.push(actionChangeTransform(instance, "Rotation"));
			}, null, 0.1f);
			defineVector3Property("Scale", "Local scale of this prop (relative to parent).\nWhen changing the scale of a parent prop all the children props scale as well.", () => targetTransform.localScale, delegate(Vector3 value)
			{
				targetTransform.localScale = value;
			}, delegate
			{
				undo.push(actionChangeTransform(instance, "Scale"));
			}, null, 0.01f);
		}
	}

	private void refreshHierarchyUI(PropInstance instance)
	{
		if (instance.GetComponent<Polygon>() != null)
		{
			return;
		}
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Hierarchy", "Defines parent and children props of this prop instance.", cache.isHierarchyShown, delegate(bool isShown)
		{
			cache.isHierarchyShown = isShown;
		});
		if (cache.isHierarchyShown)
		{
			List<PropInstance> list = new List<PropInstance>();
			if (instance.parent != null)
			{
				list.Add(instance.parent);
			}
			defineTargetsProperty("Parent", "Defines the parent of this prop.\nWhen moving, rotating or scaling the parent prop, this prop will also be affected.", PrepChangeDataStrategy.UseTarget, delegate(PropData _, PropData next, PropInstance targetProp)
			{
				next.parentID = targetProp.ID;
			}, delegate(PropData _, PropData next, int _)
			{
				next.parentID = InstanceID.None;
			}, instance, targetPickerWithIgnore(list, canTargetPolygon: true), list, 1);
			List<PropInstance> childPropInstances = getChildPropInstances(instance, includeSelf: false, recursive: false);
			defineTargetsProperty("Children", "Defines child props of this prop.\nAll child props will be affected when moving, rotating or scaling this prop.", PrepChangeDataStrategy.UseSelected, delegate(PropData _, PropData next, PropInstance _)
			{
				next.parentID = instance.ID;
			}, delegate(PropData _, PropData next, int _)
			{
				next.parentID = InstanceID.None;
			}, instance, targetPickerWithIgnore(childPropInstances, canTargetPolygon: false), childPropInstances, -1);
		}
		Predicate<PropInstance> targetPickerWithIgnore(List<PropInstance> ignoreList, bool canTargetPolygon)
		{
			return delegate(PropInstance propInstance)
			{
				if (ignoreList.Exists((PropInstance pi) => pi == propInstance))
				{
					return false;
				}
				if (!canTargetPolygon && propInstance.GetComponent<Polygon>() != null)
				{
					return false;
				}
				bool flag = instance != propInstance;
				PropInstance parent = instance.parent;
				while (parent != null && flag)
				{
					flag = parent != propInstance;
					parent = parent.parent;
				}
				return flag;
			};
		}
	}

	private void refreshNamingUI(PropInstance instance)
	{
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Naming", "Defines names for how this prop is displayed in game or referenced in scripts.", cache.isNamingShown, delegate(bool isShown)
		{
			cache.isNamingShown = isShown;
		});
		if (!cache.isNamingShown)
		{
			return;
		}
		defineStringProperty("Display Name", "Name that will be shown in-game when interacting with this prop.", () => instance.displayName, delegate(string value)
		{
			instance.displayName = value;
		}, delegate(string prevValue, string nextValue)
		{
			(PropData prev, PropData next) tuple = prepChangeData(instance);
			PropData item = tuple.prev;
			PropData item2 = tuple.next;
			item.displayName = prevValue;
			undo.push(actionChangeProp(item, item2, "Display Name"));
		});
		LuaParseResult luaParseResult = parseLuaVariableName(instance.scriptName);
		HintPropertyUI luaVariableErrorHint = defineHintProperty(string.Empty);
		luaVariableErrorHint.root.gameObject.SetActive(luaParseResult != LuaParseResult.ValidName && luaParseResult != LuaParseResult.EmptyName);
		defineStringProperty("Script Name", "Name that is used to refer to this prop in custom Lua scripts. Valid name must:\n\n1. Start with a letter (A-Z) or underscore (_) symbol.\n2. Followed by any number of letters (A-Z), digits (0-9) or underscore (_) symbols.\n3. Not contain any whitespace symbols (spaces or tabs).\n4. Must not be a reserved Lua keyword.\n\nExamples of valid names: apple, _apple, _apple123\n\nYou can also make this prop an element of an array by suffixing the name with {N},\nwhere N is index (in Lua index starts from 1).\n\nExamples of array variable: apple{1}, _apple{42}, _apple123{123}", () => instance.scriptName, delegate(string value)
		{
			instance.scriptName = value;
		}, delegate(string prevValue, string nextValue)
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (PropInstance value in propInstances.Values)
			{
				if (!(value == instance) && !string.IsNullOrEmpty(value.scriptName))
				{
					hashSet.Add(value.scriptName);
				}
			}
			string text = instance.scriptName.Trim();
			int num = 1;
			bool flag = true;
			while (flag)
			{
				flag = false;
				if (hashSet.Contains(text))
				{
					text = instance.scriptName + "_" + num;
					num++;
					flag = true;
				}
			}
			instance.scriptName = text;
			(PropData prev, PropData next) tuple = prepChangeData(instance);
			PropData item = tuple.prev;
			PropData item2 = tuple.next;
			item.scriptName = prevValue;
			undo.push(actionChangeProp(item, item2, "Script Name"));
		}, null, delegate(string variable)
		{
			LuaParseResult luaParseResult2 = parseLuaVariableName(variable);
			bool flag = luaParseResult2 == LuaParseResult.ValidName || luaParseResult2 == LuaParseResult.EmptyName;
			luaVariableErrorHint.root.gameObject.SetActive(!flag);
			recalculateHeightOfPropertiesUI();
			string text = luaParseResult2 switch
			{
				LuaParseResult.ValidName => null, 
				LuaParseResult.EmptyName => null, 
				LuaParseResult.InvalidFirstSymbol => "First symbol must be a letter (A-Z) or underscore (_).", 
				LuaParseResult.InvalidIsKeyword => "Script name must not be a Lua keyword.", 
				LuaParseResult.InvalidIsKeywordArray => "Script array name must not be a Lua keyword.", 
				LuaParseResult.InvalidSymbolAfterArray => "There must be no symbols after closing array.", 
				LuaParseResult.InvalidSymbolInArray => "Array index must contain only digits (0-9), example: {7}", 
				LuaParseResult.InvalidArrayNotClosed => "Array has been opened with {, but not closed with }", 
				LuaParseResult.InvalidArrayHasNoDigits => "Array is missing an index inside it.", 
				LuaParseResult.InvalidMultipleArrays => "Name must contain at most 1 array index.", 
				LuaParseResult.InvalidSymbolInName => "Name must have only letters (A-Z), digits (0-9) or underscore (_) symbols.", 
				_ => throw new ArgumentOutOfRangeException(), 
			};
			if (text == null)
			{
				luaVariableErrorHint.root.gameObject.SetActive(value: false);
			}
			else
			{
				luaVariableErrorHint.root.gameObject.SetActive(value: true);
				luaVariableErrorHint.Text.text = text;
				luaVariableErrorHint.Text.color = Color.darkRed;
			}
			return flag;
		});
		luaVariableErrorHint.root.transform.SetSiblingIndex(luaVariableErrorHint.root.transform.GetSiblingIndex() + 1);
	}

	private LuaParseResult parseLuaVariableName(string variable)
	{
		if (string.IsNullOrWhiteSpace(variable))
		{
			return LuaParseResult.EmptyName;
		}
		variable = variable.Trim();
		if (!variable.StartsWith('_') && !char.IsLetter(variable[0]))
		{
			return LuaParseResult.InvalidFirstSymbol;
		}
		if (LuaSyntaxHighlighter.keywords.Contains(variable))
		{
			return LuaParseResult.InvalidIsKeyword;
		}
		bool flag = false;
		int num = -1;
		for (int i = 1; i < variable.Length; i++)
		{
			char c = variable[i];
			if (flag)
			{
				if (char.IsDigit(c))
				{
					num++;
					continue;
				}
				if (c != '}')
				{
					return LuaParseResult.InvalidSymbolInArray;
				}
				flag = false;
				if (i < variable.Length - 1)
				{
					return LuaParseResult.InvalidSymbolAfterArray;
				}
			}
			else if (c == '{')
			{
				if (num != -1)
				{
					return LuaParseResult.InvalidMultipleArrays;
				}
				flag = true;
				num = 0;
				string item = variable.Substring(0, i);
				if (LuaSyntaxHighlighter.keywords.Contains(item))
				{
					return LuaParseResult.InvalidIsKeywordArray;
				}
			}
			else if (!char.IsLetterOrDigit(c) && c != '_')
			{
				return LuaParseResult.InvalidSymbolInName;
			}
		}
		if (flag)
		{
			return LuaParseResult.InvalidArrayNotClosed;
		}
		if (num == 0)
		{
			return LuaParseResult.InvalidArrayHasNoDigits;
		}
		return LuaParseResult.ValidName;
	}

	private void refreshMultiTargetTransformUI(List<PropInstance> instances)
	{
		if (isAnyLinkedPolygonSelected())
		{
			defineHintProperty("Linked wall or ceiling is selected, transform is not editable.");
			return;
		}
		refreshMultiTargetTransformPositionUI(instances);
		refreshMultiTargetTransformRotationUI(instances);
		refreshMultiTargetTransformScaleUI(instances);
	}

	private void refreshMultiTargetTransformPositionUI(List<PropInstance> instances)
	{
		Vector3[] prevPositions = new Vector3[instances.Count];
		defineVector3Property("Position", "Position of the prop in the room.\nWhen changing the position of a parent prop all the children props move as well.", delegate
		{
			for (int i = 0; i < instances.Count; i++)
			{
				prevPositions[i] = instances[i].transform.position;
			}
		}, delegate(float delta, Vector3 axis)
		{
			List<PropInstance> list = instances;
			Vector3 position = list[list.Count - 1].transform.position;
			Vector3 vector = Vector3.Scale(axis, position) + delta * axis;
			foreach (PropInstance instance in instances)
			{
				Transform obj = instance.transform;
				obj.position = Vector3.Scale(obj.position, revertMask(axis)) + vector;
			}
		}, delegate(Vector3 value, Vector3 axis)
		{
			foreach (PropInstance instance2 in instances)
			{
				Transform obj = instance2.transform;
				obj.position = vec3AddAlongAxis(obj.position, value, axis);
			}
		}, delegate
		{
			for (int i = 0; i < instances.Count; i++)
			{
				instances[i].transform.position = prevPositions[i];
			}
		}, delegate
		{
			bool flag = false;
			for (int i = 0; i < instances.Count; i++)
			{
				if (flag)
				{
					break;
				}
				flag = instances[i].transform.position != prevPositions[i];
			}
			if (flag)
			{
				List<EditorAction> list = new List<EditorAction>();
				foreach (PropInstance instance3 in instances)
				{
					list.Add(actionChangeTransform(instance3, "Position UI"));
				}
				undo.push(list.ToArray());
			}
			return flag;
		}, delegate(Vector3PropertyUI ui)
		{
			Vector3 position = instances[0].transform.position;
			bool flag = true;
			bool flag2 = true;
			bool flag3 = true;
			for (int i = 1; i < instances.Count; i++)
			{
				if (!(flag || flag2 || flag3))
				{
					break;
				}
				Vector3 position2 = instances[i].transform.position;
				flag &= position.x == position2.x;
				flag2 &= position.y == position2.y;
				flag3 &= position.z == position2.z;
			}
			ui.XValue_Input.text = (flag ? position.x.ToString("0.###") : "-");
			ui.YValue_Input.text = (flag2 ? position.y.ToString("0.###") : "-");
			ui.ZValue_Input.text = (flag3 ? position.z.ToString("0.###") : "-");
		}, 0.01f);
	}

	private void refreshMultiTargetTransformRotationUI(List<PropInstance> instances)
	{
		Quaternion[] prevRotations = new Quaternion[instances.Count];
		defineVector3Property("Rotation", "Rotation of the prop in the room.\nWhen changing the rotation of a parent prop all the children props rotate as well.", delegate
		{
			for (int i = 0; i < instances.Count; i++)
			{
				prevRotations[i] = instances[i].transform.rotation;
			}
		}, delegate(float delta, Vector3 axis)
		{
			List<PropInstance> list = instances;
			Quaternion rotation = list[list.Count - 1].transform.rotation;
			foreach (PropInstance instance in instances)
			{
				Transform obj = instance.transform;
				obj.rotation = rotation;
				obj.Rotate(axis, delta);
			}
		}, delegate(Vector3 value, Vector3 axis)
		{
			List<PropInstance> list = instances;
			Quaternion rotation = Quaternion.Euler(Vector3.Scale(list[list.Count - 1].transform.eulerAngles, revertMask(axis)) + Vector3.Scale(value, axis));
			foreach (PropInstance instance2 in instances)
			{
				instance2.transform.rotation = rotation;
			}
		}, delegate
		{
			for (int i = 0; i < instances.Count; i++)
			{
				instances[i].transform.rotation = prevRotations[i];
			}
		}, delegate
		{
			bool flag = false;
			for (int i = 0; i < instances.Count; i++)
			{
				if (flag)
				{
					break;
				}
				flag = instances[i].transform.rotation != prevRotations[i];
			}
			if (flag)
			{
				List<EditorAction> list = new List<EditorAction>();
				foreach (PropInstance instance3 in instances)
				{
					list.Add(actionChangeTransform(instance3, "Rotation UI"));
				}
				undo.push(list.ToArray());
			}
			return flag;
		}, delegate(Vector3PropertyUI ui)
		{
			Vector3 eulerAngles = instances[0].transform.eulerAngles;
			bool flag = true;
			bool flag2 = true;
			bool flag3 = true;
			for (int i = 1; i < instances.Count; i++)
			{
				if (!(flag || flag2 || flag3))
				{
					break;
				}
				Vector3 eulerAngles2 = instances[i].transform.eulerAngles;
				flag &= eulerAngles.x == eulerAngles2.x;
				flag2 &= eulerAngles.y == eulerAngles2.y;
				flag3 &= eulerAngles.z == eulerAngles2.z;
			}
			ui.XValue_Input.text = (flag ? eulerAngles.x.ToString("0.###") : "-");
			ui.YValue_Input.text = (flag2 ? eulerAngles.y.ToString("0.###") : "-");
			ui.ZValue_Input.text = (flag3 ? eulerAngles.z.ToString("0.###") : "-");
		}, 0.1f);
	}

	private void refreshMultiTargetTransformScaleUI(List<PropInstance> instances)
	{
		Vector3[] prevScales = new Vector3[instances.Count];
		defineVector3Property("Scale", "Scale of the prop in the room.\nWhen changing the scale of a parent prop all the children props scale as well.", delegate
		{
			for (int i = 0; i < instances.Count; i++)
			{
				prevScales[i] = instances[i].transform.localScale;
			}
		}, delegate(float delta, Vector3 axis)
		{
			List<PropInstance> list = instances;
			Vector3 localScale = list[list.Count - 1].transform.localScale;
			Vector3 vector = Vector3.Scale(axis, localScale) + delta * axis;
			foreach (PropInstance instance in instances)
			{
				Transform obj = instance.transform;
				obj.localScale = Vector3.Scale(obj.localScale, revertMask(axis)) + vector;
			}
		}, delegate(Vector3 value, Vector3 axis)
		{
			foreach (PropInstance instance2 in instances)
			{
				Transform obj = instance2.transform;
				obj.localScale = vec3AddAlongAxis(obj.localScale, value, axis);
			}
		}, delegate
		{
			for (int i = 0; i < instances.Count; i++)
			{
				instances[i].transform.localScale = prevScales[i];
			}
		}, delegate
		{
			bool flag = false;
			for (int i = 0; i < instances.Count; i++)
			{
				if (flag)
				{
					break;
				}
				flag = instances[i].transform.localScale != prevScales[i];
			}
			if (flag)
			{
				List<EditorAction> list = new List<EditorAction>();
				foreach (PropInstance instance3 in instances)
				{
					list.Add(actionChangeTransform(instance3, "Scale UI"));
				}
				undo.push(list.ToArray());
			}
			return flag;
		}, delegate(Vector3PropertyUI ui)
		{
			Vector3 localScale = instances[0].transform.localScale;
			bool flag = true;
			bool flag2 = true;
			bool flag3 = true;
			for (int i = 1; i < instances.Count; i++)
			{
				if (!(flag || flag2 || flag3))
				{
					break;
				}
				Vector3 localScale2 = instances[i].transform.localScale;
				flag &= localScale.x == localScale2.x;
				flag2 &= localScale.y == localScale2.y;
				flag3 &= localScale.z == localScale2.z;
			}
			ui.XValue_Input.text = (flag ? localScale.x.ToString("0.###") : "-");
			ui.YValue_Input.text = (flag2 ? localScale.y.ToString("0.###") : "-");
			ui.ZValue_Input.text = (flag3 ? localScale.z.ToString("0.###") : "-");
		}, 0.01f);
	}

	private void refreshItemRespawnerUI(PropInstance instance)
	{
		ItemRespawnVolume component = instance.GetComponent<ItemRespawnVolume>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Item Respawner", "Component used to respawn important items so they don't get stuck in unreachable places.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (!cache.isInteractionShown)
		{
			return;
		}
		defineEnumProperty("Respawn Mode", "Defines where the item will respawn.", (int)component.respawnMode, optionsTooltips: new List<string> { "The item will be added to the player's inventory on respawn.", "The item will be moved back to its original position and rotation on respawn.", "The item will be moved to a specified object on respawn." }, options: new List<string> { "To Player Inventory", "To Original Position", "To An Object" }, onSubmit: delegate(int value)
		{
			var (prev, propData) = prepChangeData(instance);
			getItemRespawnerData(propData).respawnMode = (ItemRespawnVolume.RespawnMode)value;
			undo.push(actionChangeProp(prev, propData, "ItemRespawnVolume respawn mode"));
		});
		ItemRespawnVolume.RespawnMode respawnMode = component.respawnMode;
		if (respawnMode == ItemRespawnVolume.RespawnMode.ToAnObject || respawnMode == ItemRespawnVolume.RespawnMode.ToOriginalPositionAndRotation)
		{
			defineBoolProperty("Use Physics on respawn", "Enabled - The item will respawn with physics applied.\nDisabled - The item will respawn without physics applied.", component.respawnWithPhysics, delegate(bool value)
			{
				var (prev, propData) = prepChangeData(instance);
				getItemRespawnerData(propData).respawnWithPhysics = value;
				undo.push(actionChangeProp(prev, propData, "ItemRespawnVolume Use Physics."));
			});
		}
		if (component.respawnMode == ItemRespawnVolume.RespawnMode.ToAnObject)
		{
			List<PropInstance> list = new List<PropInstance>();
			if (component.objectToRespawnTo != null)
			{
				PropInstance component2 = component.objectToRespawnTo.GetComponent<PropInstance>();
				list.Add(component2);
			}
			defineTargetsProperty("Respawn to an Empty", "The item will respawn to the position of the selected Empty prop.", PrepChangeDataStrategy.UseTarget, delegate(PropData prev, PropData next, PropInstance value)
			{
				getItemRespawnerData(next).objectToRespawnTo = value.ID;
			}, delegate(PropData prev, PropData next, int index)
			{
				getItemRespawnerData(next).objectToRespawnTo = InstanceID.None;
			}, instance, targetEmptyPredicate, list, 1);
		}
		defineHintProperty("Adjust the volume size by changing its Transform scale!");
	}

	private void refreshPolygonUI(PropInstance instance)
	{
		Polygon polygon = instance.GetComponent<Polygon>();
		bool isFloor = polygon is Floor;
		string polygonType = ((!isFloor) ? "Wall" : ((polygon as Floor).isCeiling() ? "Ceiling" : "Floor"));
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty(polygonType, "Defines the shape and appearance of this " + polygonType + ".", cache.isPolygonShown, delegate(bool isShown)
		{
			cache.isPolygonShown = isShown;
		});
		if (!cache.isPolygonShown)
		{
			return;
		}
		bool num = polygon is Wall wall && wall.getFloor() != null;
		bool flag = polygon is Floor floor && floor.isCeiling();
		if (!num)
		{
			defineFloatProperty("Thickness", "Defines how thick this " + polygonType + " is.", () => polygon.extrusionThickness, delegate(float value)
			{
				polygon.extrusionThickness = value;
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getPolygonData(propData, isFloor).extrusionThickness = prevValue;
				undo.push(actionChangeProp(propData, next, polygonType + ".extrusionThickness changed."));
			}, null, float.NegativeInfinity, float.PositiveInfinity, 0.01f);
			defineEnumProperty("Thickness Source", "Defines from where thickness is calculated. Used to more easily position the " + polygonType + ".", (int)polygon.extrusionType, optionsTooltips: new List<string> { "Half the thickness will come from the front face, half from the back face.", "All the thickness will come from the front face.", "All the thickness will come from the back face." }, options: new List<string> { "Center", "Front", "Back" }, onSubmit: delegate(int value)
			{
				var (prev, propData) = prepChangeData(instance);
				getPolygonData(propData, isFloor).extrusionType = (Polygon.ExtrusionType)value;
				undo.push(actionChangeProp(prev, propData, polygonType + ".extrusionType changed."));
			});
			if (!isFloor)
			{
				defineBoolProperty("Has Collider", "Enabled - You cannot throw items through it. You can also define navigation type.\nDisabled - You can throw items through it. Players also can walk through it.", polygon.hasCollider, delegate(bool value)
				{
					var (prev, propData) = prepChangeData(instance);
					getPolygonData(propData, isFloor).hasCollider = value;
					polygon.recalculate();
					undo.push(actionChangeProp(prev, propData, polygonType + ".hasCollider changed."));
				});
				if (polygon.hasCollider)
				{
					defineEnumProperty("Navigation Type", "Defines the navigation properties of this " + polygonType + ".", (int)polygon.collisionType, new List<string> { "Static", "Obstacle", "None" }, delegate(int value)
					{
						var (prev, propData) = prepChangeData(instance);
						getPolygonData(propData, isFloor).collisionType = (Polygon.CollisionType)value;
						undo.push(actionChangeProp(prev, propData, polygonType + ".collisionType changed."));
					}, useInterpolationPopup: false, new List<string>
					{
						"Collider cannot be moved, but it can be filled with holes. It's the most performant option.",
						polygonType + " acts the same way as Obstacle prop. It can be moved and has a box collider. Holes do not change its collider.",
						polygonType + " does not carve nav-mesh and players can walk through it."
					});
				}
			}
		}
		if (num || flag)
		{
			defineHintProperty("This " + polygonType + " is linked to floor, so its position/rotation/scale or shape cannot be modified (it is driven by the floor).\n\nYou can unlink it from the floor to gain full control over it.");
			defineButtonProperty("Unlink", "", delegate
			{
				(PropData prev, PropData next) tuple = prepChangeData(instance);
				PropData item = tuple.prev;
				PropData item2 = tuple.next;
				item2.parentID = InstanceID.None;
				undo.push(actionChangeProp(item, item2, polygonType + " unlinked from floor."));
			});
		}
		Floor floor2 = polygon as Floor;
		if ((object)floor2 != null && floor2.getWalls().Count > 0)
		{
			defineFloatProperty("Wall Height", "Defines the height of walls linked to this floor.", () => floor2.wallHeight, delegate(float value)
			{
				floor2.wallHeight = value;
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getFloorData(propData).wallHeight = prevValue;
				undo.push(actionChangeProp(propData, next, polygonType + ".wallHeight changed."));
			}, null, 0f, float.PositiveInfinity, 0.01f);
			defineFloatProperty("Wall Thickness", "Defines the base thickness of walls linked to this floor.", () => floor2.wallThickness, delegate(float value)
			{
				floor2.wallThickness = value;
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getFloorData(propData).wallThickness = prevValue;
				undo.push(actionChangeProp(propData, next, polygonType + ".wallThickness changed."));
			}, null, 0f, float.PositiveInfinity, 0.01f);
			defineBoolProperty("Connect Walls", "If enabled, walls will extrude nicely connect to each other.\nIf disabled, walls will extrude straight without connecting.", floor2.connectWalls, delegate(bool value)
			{
				var (prev, propData) = prepChangeData(instance);
				getFloorData(propData).connectWalls = value;
				undo.push(actionChangeProp(prev, propData, polygonType + ".connectWalls changed."));
			});
		}
	}

	private void refreshTestUI(PropInstance instance)
	{
		Test test = instance.GetComponent<Test>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Ints", "Ints header tooltip", cache.isTestIntsShown, delegate(bool isShown)
		{
			cache.isTestIntsShown = isShown;
		});
		if (cache.isTestIntsShown)
		{
			defineIntProperty("Int", "Int tooltip", () => test.intValue, delegate(int value)
			{
				test.intValue = value;
			}, delegate(int prevValue, int nextValue)
			{
				UnityEngine.Debug.Log($"Int submit: {prevValue} -> {nextValue}");
				var (propData, next) = prepChangeData(instance);
				getTestData(propData).integer = prevValue;
				undo.push(actionChangeProp(propData, next, "Test.intValue changed."));
			}, delegate(int prevValue, int nextValue)
			{
				UnityEngine.Debug.Log($"Int change: {prevValue} -> {nextValue}");
			});
			defineIntProperty("IntRange", "IntRange tooltip", () => test.intRange, delegate(int value)
			{
				test.intRange = value;
			}, delegate(int prevValue, int nextValue)
			{
				UnityEngine.Debug.Log($"IntRange submit: {prevValue} -> {nextValue}");
				var (propData, next) = prepChangeData(instance);
				getTestData(propData).integerRange = prevValue;
				undo.push(actionChangeProp(propData, next, "Test.intRange changed."));
			}, delegate(int prevValue, int nextValue)
			{
				UnityEngine.Debug.Log($"IntRange change: {prevValue} -> {nextValue}");
			}, -10, 10);
			defineSeparatorProperty();
		}
		defineHeaderProperty("Floats", "Floats header tooltip", cache.isTestFloatsShown, delegate(bool isShown)
		{
			cache.isTestFloatsShown = isShown;
		});
		if (cache.isTestFloatsShown)
		{
			defineFloatProperty("Float", "Float tooltip", () => test.floatValue, delegate(float value)
			{
				test.floatValue = value;
			}, delegate(float prevValue, float nextValue)
			{
				UnityEngine.Debug.Log($"Float submit: {prevValue} -> {nextValue}");
				var (propData, next) = prepChangeData(instance);
				getTestData(propData).single = prevValue;
				undo.push(actionChangeProp(propData, next, "Test.floatValue changed."));
			}, delegate(float prevValue, float nextValue)
			{
				UnityEngine.Debug.Log($"Float change: {prevValue} -> {nextValue}");
			});
			defineFloatProperty("FloatRange", "FloatRange tooltip", () => test.floatRange, delegate(float value)
			{
				test.floatRange = value;
			}, delegate(float prevValue, float nextValue)
			{
				UnityEngine.Debug.Log($"FloatRange submit: {prevValue} -> {nextValue}");
				var (propData, next) = prepChangeData(instance);
				getTestData(propData).singleRange = prevValue;
				undo.push(actionChangeProp(propData, next, "Test.floatRange changed."));
			}, delegate(float prevValue, float nextValue)
			{
				UnityEngine.Debug.Log($"FloatRange change: {prevValue} -> {nextValue}");
			}, -10f, 10f);
			defineSeparatorProperty();
		}
		defineVector2Property("Vector2", "Vector2 tooltip", () => test.vector2Value, delegate(Vector2 value)
		{
			test.vector2Value = value;
		}, delegate(Vector2 prevValue, Vector2 nextValue)
		{
			UnityEngine.Debug.Log($"Vector2 submit: {prevValue} -> {nextValue}");
			var (propData, next) = prepChangeData(instance);
			getTestData(propData).vector2 = prevValue;
			undo.push(actionChangeProp(propData, next, "Test.vector2Value changed."));
		}, delegate(Vector2 prevValue, Vector2 nextValue)
		{
			UnityEngine.Debug.Log($"Vector2 change: {prevValue} -> {nextValue}");
		});
		defineVector3Property("Vector3", "Vector3 tooltip", () => test.vector3Value, delegate(Vector3 value)
		{
			test.vector3Value = value;
		}, delegate(Vector3 prevValue, Vector3 nextValue)
		{
			UnityEngine.Debug.Log($"Vector3 submit: {prevValue} -> {nextValue}");
			var (propData, next) = prepChangeData(instance);
			getTestData(propData).vector3 = prevValue;
			undo.push(actionChangeProp(propData, next, "Test.vector3Value changed."));
		}, delegate(Vector3 prevValue, Vector3 nextValue)
		{
			UnityEngine.Debug.Log($"Vector3 change: {prevValue} -> {nextValue}");
		});
		defineStringProperty("String", "String tooltip", () => test.stringValue, delegate(string value)
		{
			test.stringValue = value;
		}, delegate(string prevValue, string nextValue)
		{
			UnityEngine.Debug.Log("String submit: " + prevValue + " -> " + nextValue);
			var (propData, next) = prepChangeData(instance);
			getTestData(propData).text = prevValue;
			undo.push(actionChangeProp(propData, next, "Test.stringValue changed."));
		}, delegate(string prevValue, string nextValue)
		{
			UnityEngine.Debug.Log("String change: " + prevValue + " -> " + nextValue);
		}, (string value) => value.Length <= 4);
		defineColorProperty("Color 1", "Color 1 tooltip", () => test.color1Value, delegate(Color color)
		{
			UnityEngine.Debug.Log($"Color 1 change: {color}");
			test.color1Value = color;
		}, delegate(Color prevColor, Color nextColor)
		{
			UnityEngine.Debug.Log($"Color 1 submit: {prevColor} -> {nextColor}");
			var (propData, next) = prepChangeData(instance);
			getTestData(propData).color1 = prevColor;
			undo.push(actionChangeProp(propData, next, "Test.color1Value changed."));
		});
		defineColorProperty("Color 2", "Color 2 tooltip", () => test.color2Value, delegate(Color color)
		{
			UnityEngine.Debug.Log($"Color 2 change: {color}");
			test.color2Value = color;
		}, delegate(Color prevColor, Color nextColor)
		{
			UnityEngine.Debug.Log($"Color 2 submit: {prevColor} -> {nextColor}");
			var (propData, next) = prepChangeData(instance);
			getTestData(propData).color2 = prevColor;
			undo.push(actionChangeProp(propData, next, "Test.color2Value changed."));
		});
		defineBoolProperty("Bool", "Bool tooltip", test.boolValue, delegate(bool value)
		{
			UnityEngine.Debug.Log($"Bool submit: {value}");
			var (prev, propData) = prepChangeData(instance);
			getTestData(propData).boolean = value;
			undo.push(actionChangeProp(prev, propData, "Test.boolValue changed."));
		});
		defineEnumProperty("Enum", "Option 1 - First option\nOption 2 - Second option\nOption 3 - Third option", (int)test.enumValue, new List<string> { "Option 1", "Option 2", "Option 3" }, delegate(int value)
		{
			UnityEngine.Debug.Log($"Enum submit: {(Test.SomeOption)value}");
			var (prev, propData) = prepChangeData(instance);
			getTestData(propData).option = (Test.SomeOption)value;
			undo.push(actionChangeProp(prev, propData, "Test.enumValue changed."));
		});
		defineFoldoutProperty("What is this?", "This is foldout property!", cache.isTestHelpShown, delegate(bool isShown)
		{
			cache.isTestHelpShown = isShown;
		});
		if (cache.isTestHelpShown)
		{
			defineHintProperty("This component is used to test properties.");
		}
		defineButtonProperty("Button", "Button tooltip", delegate
		{
			UnityEngine.Debug.Log("Button clicked!");
		});
		defineHintProperty($"Empty separator (height: {4f})");
		defineSeparatorProperty(4f);
		defineHintProperty($"Non-empty separator (height: {4f})");
		defineSeparatorProperty(4f, isEmpty: false);
	}

	private void refreshTextUI(PropInstance instance)
	{
		EditorText text = instance.GetComponent<EditorText>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Text", "Simple display of text in the world.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (cache.isInteractionShown)
		{
			defineStringProperty("Text", "Text to display.", () => text.text, delegate(string value)
			{
				text.text = value;
				text.syncVisuals();
			}, delegate(string prevValue, string nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getTextData(propData).text = prevValue;
				undo.push(actionChangeProp(propData, next, "EditorText.text changed."));
			}, null, null, InputField.LineType.MultiLineNewline);
			defineIntProperty("Font Size", "Font size of the text.", () => text.fontSize, delegate(int value)
			{
				text.fontSize = value;
				text.syncVisuals();
			}, delegate(int prevValue, int nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getTextData(propData).fontSize = prevValue;
				undo.push(actionChangeProp(propData, next, "EditorText.fontSize changed."));
			}, null, 1);
			defineColorProperty("Color", "Color of the text.", () => text.color, delegate(Color color)
			{
				text.color = color;
				text.syncVisuals();
			}, delegate(Color prevColor, Color nextColor)
			{
				var (propData, next) = prepChangeData(instance);
				getTextData(propData).color = prevColor;
				undo.push(actionChangeProp(propData, next, "EditorText.color changed."));
			});
			defineBoolProperty("Bold", "Whether the text is bold.", text.isBold, delegate(bool value)
			{
				var (prev, propData) = prepChangeData(instance);
				getTextData(propData).isBold = value;
				undo.push(actionChangeProp(prev, propData, "EditorText.isBold changed."));
			});
			defineBoolProperty("Italic", "Whether the text is italic.", text.isItalic, delegate(bool value)
			{
				var (prev, propData) = prepChangeData(instance);
				getTextData(propData).isItalic = value;
				undo.push(actionChangeProp(prev, propData, "EditorText.isItalic changed."));
			});
			defineVector2Property("Canvas Size", "Size of the text canvas.", () => text.canvasSize, delegate(Vector2 value)
			{
				float x = Mathf.Max(0f, value.x);
				float y = Mathf.Max(0f, value.y);
				text.canvasSize = new Vector2(x, y);
				text.syncVisuals();
			}, delegate(Vector2 prevValue, Vector2 nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getTextData(propData).canvasSize = prevValue;
				undo.push(actionChangeProp(propData, next, "EditorText.canvasSize changed."));
			}, null, 0.01f);
		}
	}

	private void refreshCustomModelUI(PropInstance instance)
	{
		CustomModel component = instance.GetComponent<CustomModel>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Model", "Defines settings for imported custom model.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (!cache.isInteractionShown)
		{
			return;
		}
		if (component.hasModelColliders)
		{
			int num = (int)((!component.disableGeneratedCollider) ? component.colliderType : CustomModelColliderType.None);
			List<string> list = new List<string> { "None", "Box", "ConvexMesh" };
			Rigidbody component2 = instance.GetComponent<Rigidbody>();
			Switch3D component3 = instance.GetComponent<Switch3D>();
			if (component2 == null && component3 == null)
			{
				list.Add("ConcaveMesh");
			}
			else if (num == 3)
			{
				num = 2;
			}
			defineEnumProperty("Collider Type", "Defines the type of collider generated for this model.", num, list, delegate(int value)
			{
				(PropData prev, PropData next) tuple = prepChangeData(instance);
				PropData item = tuple.prev;
				PropData item2 = tuple.next;
				CustomModelData customModelData = getCustomModelData(item2);
				customModelData.colliderType = (CustomModelColliderType)value;
				customModelData.disableGeneratedCollider = customModelData.colliderType == CustomModelColliderType.None;
				undo.push(actionChangeProp(item, item2, "Custom model change collider type"));
			}, useInterpolationPopup: false, new List<string> { "Disables the generated collider.", "Generates a cube that encapsulates the whole mesh.", "Generates an approximate shape, resource intensive and not recommended for many props.", "Generates a more detailed shape but can only be used for static props, resource intensive and not recommended for many props." });
		}
		if (component.animations != null && component.animations.Count > 0)
		{
			defineEnumProperty("Animations", "Select one animation to loop.", component.currentAnimation, component.animations, delegate(int value)
			{
				var (prev, propData) = prepChangeData(instance);
				getCustomModelData(propData).currentAnimation = value;
				undo.push(actionChangeProp(prev, propData, "Custom model change animation"));
			});
		}
	}

	private void refreshTeleportUI(PropInstance instance)
	{
		Teleport teleport = instance.GetComponent<Teleport>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Teleport", "Defines where and who teleports to this position and/or rotation when activated.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (cache.isInteractionShown)
		{
			defineBoolProperty("Teleport All Players", "When checked, all players will be teleported to this teleport position.\nWhen not checked, only one player is teleported.", teleport.teleportAll, delegate(bool value)
			{
				var (prev, propData) = prepChangeData(instance);
				getTeleportData(propData).teleportAll = value;
				undo.push(actionChangeProp(prev, propData, "Teleport teleportAll"));
			});
			defineBoolProperty("Change Rotation", "When checked, teleported players will face the direction this teleport is pointing at.\nWhen not checked, the players keep their previous rotations.", teleport.changeRotation, delegate(bool value)
			{
				var (prev, propData) = prepChangeData(instance);
				getTeleportData(propData).changeRotation = value;
				undo.push(actionChangeProp(prev, propData, "Teleport changeRotation"));
			});
			defineFloatProperty("Walking Speed", "Player walking speed to set when a player is teleported to this point.\nTo reset it, teleport the player to another point.", () => teleport.walkingSpeed, delegate(float value)
			{
				teleport.walkingSpeed = value;
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getTeleportData(propData).walkingSpeed = prevValue;
				undo.push(actionChangeProp(propData, next, "Teleport Walking Speed"));
			}, null, 0f, float.PositiveInfinity, 0.01f);
			defineFloatProperty("Running Speed", "Player running speed to set when a player is teleported to this point.\nTo reset it, teleport the player to another point.", () => teleport.runningSpeed, delegate(float value)
			{
				teleport.runningSpeed = value;
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getTeleportData(propData).runningSpeed = prevValue;
				undo.push(actionChangeProp(propData, next, "Teleport Running Speed"));
			}, null, 0f, float.PositiveInfinity, 0.01f);
		}
	}

	private void refreshStairsUI(PropInstance instance)
	{
		Stairs stairs = instance.GetComponent<Stairs>();
		InstanceUICache cache = getInstanceUICache(instance);
		if (stairs.direction == Stairs.Direction.Straight)
		{
			return;
		}
		defineHeaderProperty("Stairs", "Defines a stairs prop. Player can walk up and down the stairs.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (cache.isInteractionShown)
		{
			defineEnumProperty("Direction", "Changes the direction the stairs are curving to.", (int)stairs.direction, new List<string> { "Right", "Left" }, delegate(int value)
			{
				var (prev, propData) = prepChangeData(instance);
				getStairsData(propData).direction = (Stairs.Direction)value;
				undo.push(actionChangeProp(prev, propData, "Stairs direction"));
				stairs.syncVisuals();
			}, useInterpolationPopup: false, new List<string> { "Default orientation of the stairs", "Changes the direction of the stairs to the other side" });
		}
	}

	private void refreshSoundUI(PropInstance instance)
	{
		Sound sound = instance.GetComponent<Sound>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Sound", "Defines a sound prop. Can be music or one shot sound.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (!cache.isInteractionShown)
		{
			return;
		}
		string value = "None";
		if (sound.path.Length > 0)
		{
			value = sound.path;
		}
		if (sound._event.Length > 0)
		{
			value = sound._event.Substring("event:/ROOM EDITOR/".Length);
		}
		defineSoundProperty("%RE_soundAsset%", "%RE_soundAssetTooltip%", value, delegate
		{
			soundPicker.selectedFile = sound.path;
			soundPicker.presetSelection = null;
			refreshSoundPickerUI();
		});
		defineEnumProperty("Sound Type", "Determines the type of this sound.", (int)sound.type, new List<string> { "Music", "Sound Effect", "Footstep" }, delegate(int type)
		{
			var (prev, propData) = prepChangeData(instance);
			getSoundData(propData).type = (Sound.SoundType)type;
			undo.push(actionChangeProp(prev, propData, "Sound Type"));
		}, useInterpolationPopup: false, new List<string> { "The sound will be played on the music track. Only one 'Is Music' sound can play at a time.", "The sound is an effect that can be layerd with other effects. It can be 3D or 2D.", "The sound is played only when the player walks." });
		if (sound.path.Length > 0 && sound.type == Sound.SoundType.SoundEffect)
		{
			defineBoolProperty("Is Loopable", "When checked, the sound will replay itself.\nTurn the sound off by reactivating the sound prop.", sound.isLoopable, delegate(bool isLoopable)
			{
				var (prev, propData) = prepChangeData(instance);
				getSoundData(propData).isLoopable = isLoopable;
				undo.push(actionChangeProp(prev, propData, "Sound isLoopable"));
			});
			defineBoolProperty("Is 2D", "When checked, the sound is played directly in the audio-output device.\nWhen not checked, the sound is directional.", sound.is2D, delegate(bool is2D)
			{
				var (prev, propData) = prepChangeData(instance);
				getSoundData(propData).is2D = is2D;
				undo.push(actionChangeProp(prev, propData, "Sound is2D"));
			});
			if (!sound.is2D)
			{
				defineEnumProperty("Falloff Mode", "Determines how the sound fades with distance.", (int)sound.falloffMode, new List<string> { "Natural Fade", "Linear Fade", "Smooth Fade" }, delegate(int falloffMode2)
				{
					var (prev, propData) = prepChangeData(instance);
					getSoundData(propData).falloffMode = (Sound.FalloffMode)falloffMode2;
					undo.push(actionChangeProp(prev, propData, "Sound Falloff Mode"));
				}, useInterpolationPopup: false, new List<string> { "Volume drops off naturally with distance, like in real life.", "Volume decreases evenly until it’s silent at max range.", "Starts natural near you, then fades out more gently at long range." });
				Sound.FalloffMode falloffMode = sound.falloffMode;
				string text = default(string);
				switch (falloffMode)
				{
				case Sound.FalloffMode.NaturalFade:
					text = "Near Distance";
					break;
				case Sound.FalloffMode.LinearFade:
					text = "Start Fading Distance";
					break;
				case Sound.FalloffMode.SmoothFade:
					text = "Near Distance";
					break;
				default:
					global::_003CPrivateImplementationDetails_003E.ThrowSwitchExpressionException(falloffMode);
					break;
				}
				string label = text;
				falloffMode = sound.falloffMode;
				switch (falloffMode)
				{
				case Sound.FalloffMode.NaturalFade:
					text = "The distance at which the sound reaches its maximum volume. Getting closer than this will not make it louder.\nThink of standing inside a small circle around the sound — anywhere inside, the volume stays full.";
					break;
				case Sound.FalloffMode.LinearFade:
					text = "The point where the sound begins to fade with distance. Closer than this, it always plays at full volume.\nThink of it as stepping back from a speaker — fading begins as soon as you leave this line.";
					break;
				case Sound.FalloffMode.SmoothFade:
					text = "The closest range where the sound is at full volume. A larger value makes the sound feel as if it comes from a bigger source.\nPicture a wide circle around the sound — the bigger the circle, the larger the source seems.";
					break;
				default:
					global::_003CPrivateImplementationDetails_003E.ThrowSwitchExpressionException(falloffMode);
					break;
				}
				string tooltip = text;
				defineFloatProperty(label, tooltip, () => sound.minDistance3D, delegate(float minDistance3D)
				{
					sound.minDistance3D = minDistance3D;
				}, delegate(float prevValue, float nextValue)
				{
					var (propData, next) = prepChangeData(instance);
					getSoundData(propData).minDistance3D = prevValue;
					undo.push(actionChangeProp(propData, next, "Sound min distance"));
				}, null, 0f, float.PositiveInfinity, 0.01f);
				falloffMode = sound.falloffMode;
				switch (falloffMode)
				{
				case Sound.FalloffMode.NaturalFade:
					text = "Far Distance";
					break;
				case Sound.FalloffMode.LinearFade:
					text = "Silent Distance";
					break;
				case Sound.FalloffMode.SmoothFade:
					text = "Far Distance";
					break;
				default:
					global::_003CPrivateImplementationDetails_003E.ThrowSwitchExpressionException(falloffMode);
					break;
				}
				string label2 = text;
				falloffMode = sound.falloffMode;
				switch (falloffMode)
				{
				case Sound.FalloffMode.NaturalFade:
					text = "The distance where volume fading stops. Beyond this range, the sound remains faint but does not get any quieter.\nPicture a large bubble around the sound — outside the bubble, the volume stays at a constant low level.";
					break;
				case Sound.FalloffMode.LinearFade:
					text = "The point where the sound becomes completely inaudible. Beyond this distance, it can no longer be heard.\nThink of walking away from music until it fully disappears at this boundary.";
					break;
				case Sound.FalloffMode.SmoothFade:
					text = "The distance where fading stops. Beyond this, the sound does not get any quieter but will already be very faint.\nImagine a soft echo in the distance — it never disappears completely, but becomes barely noticeable.";
					break;
				default:
					global::_003CPrivateImplementationDetails_003E.ThrowSwitchExpressionException(falloffMode);
					break;
				}
				string tooltip2 = text;
				defineFloatProperty(label2, tooltip2, () => sound.maxDistance3D, delegate(float maxDistance3D)
				{
					sound.maxDistance3D = maxDistance3D;
				}, delegate(float prevValue, float nextValue)
				{
					var (propData, next) = prepChangeData(instance);
					getSoundData(propData).maxDistance3D = prevValue;
					undo.push(actionChangeProp(propData, next, "Sound max distance"));
				}, null, 0f, float.PositiveInfinity, 0.01f);
			}
		}
		defineFloatProperty("Volume", "Determines the volume of the sound.", () => sound.volume, delegate(float volume)
		{
			sound.volume = volume;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, next) = prepChangeData(instance);
			getSoundData(propData).volume = prevValue;
			undo.push(actionChangeProp(propData, next, "Sound volume"));
		}, null, 0f, float.PositiveInfinity, 0.01f);
		defineBoolProperty("Active On Start", "When checked, this sound is activated as soon as the player starts the room.\nOnly one 'Is Music' can play at the start.", sound.activeOnStart, delegate(bool flag)
		{
			List<EditorAction> list = new List<EditorAction>();
			if (flag && sound.type == Sound.SoundType.Music)
			{
				actionsDisableActiveOnStart(list);
			}
			var (prev, propData) = prepChangeData(instance);
			getSoundData(propData).activeOnStart = flag;
			list.Add(actionChangeProp(prev, propData, "Sound activeOnStart"));
			undo.push(list.ToArray());
		});
		defineBoolProperty("Local Player Only", "If true, only local player can trigger this object.\nUseful for effect that should not be seen by other players.\nFor example, when you enter a room, you might want to see different <NAME> from other players.".Replace("<NAME>", "Sound"), sound.localOnly, delegate(bool localOnly)
		{
			sound.localOnly = localOnly;
			var (propData, next) = prepChangeData(instance);
			getSoundData(propData).localOnly = localOnly;
			undo.push(actionChangeProp(propData, next, "Sound local only", refreshVolume: true));
		});
		void actionsDisableActiveOnStart(List<EditorAction> actions)
		{
			foreach (KeyValuePair<InstanceID, PropInstance> propInstance in propInstances)
			{
				if (propInstance.Value != instance && propInstance.Value.TryGetComponent<Sound>(out var component) && component.activeOnStart && component.type == Sound.SoundType.Music)
				{
					var (prev, propData) = prepChangeData(propInstance.Value);
					getSoundData(propData).activeOnStart = false;
					actions.Add(actionChangeProp(prev, propData, "Disable Other Sound activeOnStart"));
				}
			}
		}
	}

	private void refreshSkyboxUI(PropInstance instance)
	{
		EditorSkybox component = instance.GetComponent<EditorSkybox>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Skybox", "Defines the skybox for this room.\nThere can be multiple skyboxes in a room, but only one can be active at a time.\nSkybox is a visual effect that can be used to create atmosphere in the room.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (!cache.isInteractionShown)
		{
			return;
		}
		SkyboxData skyData = component.data;
		defineEnumProperty("Type", "Determines the type of skybox used.", (int)skyData.type, new List<string> { "Procedural", "6 Sided", "Physically Based (Advanced)" }, delegate(int value)
		{
			(PropData prev, PropData next) tuple = prepChangeData(instance);
			PropData item = tuple.prev;
			PropData item2 = tuple.next;
			SkyboxData skyboxData = getSkyboxData(item2);
			skyboxData.type = (SkyboxType)value;
			switch (value)
			{
			case 1:
			{
				SkyboxMeta skyboxMeta = assets.skyboxes[0];
				skyboxData.exposure2 = skyboxMeta.exposure;
				skyboxData.rotation = skyboxMeta.rotation;
				skyboxData.textureData = new SkyTexData
				{
					fileName = skyboxMeta.texture
				};
				break;
			}
			case 0:
				skyboxData.textureData = null;
				break;
			case 2:
				optionDialog.showNoTranslate("Using Physically Based Skybox", "Using Physically Based Skybox requires additional setup.\n\nIt requires ONE directional lightning that will represent the sun.\n\nIt will not work with 0 or more of directional lightnings!", onFrameClick, new VisualControl("ignore", ControllerButtonActionType.UIConfirmPrimary, "%RoomEditor_OK%", VisualControl.ControlType.Button, VisualControl.Position.BotCenter));
				skyboxData.textureData = null;
				break;
			}
			undo.push(actionChangeProp(item, item2, "Skybox type", refreshVolume: true));
			savePreviewSkyboxData(skyboxData);
		}, useInterpolationPopup: false, new List<string> { "A skybox that is generated based on colors you choose. It creates a gradient from top to bottom.", "A skybox that uses 6 textures to create a sky. You can choose from pre-made skyboxes.", "A skybox that simulates a real-world sky based on the position of the sun and atmospheric conditions.\nThis is an advanced feature that requires additional setup.\nThis uses Light, Clouds, Water and other elements to create a realistic sky." });
		defineBoolProperty("Active On Start", "When checked, this skybox is activated as soon as the player starts the room.\nOnly one skybox can be activated on start.", skyData.activeOnStart, delegate(bool value)
		{
			List<EditorAction> list2 = new List<EditorAction>();
			foreach (KeyValuePair<InstanceID, PropInstance> propInstance in propInstances)
			{
				if (propInstance.Value != instance && propInstance.Value.TryGetComponent<EditorSkybox>(out var component2) && component2.data.activeOnStart)
				{
					var (prev, propData) = prepChangeData(propInstance.Value);
					getSkyboxData(propData).activeOnStart = false;
					list2.Add(actionChangeProp(prev, propData, "Disable Other Skybox activeOnStart"));
				}
			}
			(PropData prev, PropData next) tuple2 = prepChangeData(instance);
			PropData item = tuple2.prev;
			PropData item2 = tuple2.next;
			SkyboxData skyboxData = getSkyboxData(item2);
			skyboxData.activeOnStart = value;
			list2.Add(actionChangeProp(item, item2, "Skybox activeOnStart"));
			undo.push(list2.ToArray());
			if (value)
			{
				skyboxOnStartActiveCache = skyboxData;
				skyboxCurrentActiveCache = skyboxData;
			}
			else
			{
				skyboxOnStartActiveCache = null;
			}
		});
		defineBoolProperty("Local Player Only", "If true, only local player can trigger this object.\nUseful for effect that should not be seen by other players.\nFor example, when you enter a room, you might want to see different <NAME> from other players.".Replace("<NAME>", "Skybox"), skyData.localOnly, delegate(bool value)
		{
			skyData.localOnly = value;
			var (propData, next) = prepChangeData(instance);
			getSkyboxData(propData).localOnly = value;
			undo.push(actionChangeProp(propData, next, "Skybox local only", refreshVolume: true));
		});
		defineFloatProperty("Transition Time", "Time in seconds it takes to transition from any active skybox setting to this one.", () => skyData.transitionTime, delegate(float value)
		{
			skyData.transitionTime = value;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, next) = prepChangeData(instance);
			getSkyboxData(propData).transitionTime = prevValue;
			undo.push(actionChangeProp(propData, next, "Skybox transition time", refreshVolume: true));
		}, null, 0f);
		if (skyData.type == SkyboxType.Procedural)
		{
			defineColorProperty("Sky Color", "The color of the sky.\nThis is the main color of the procedural skybox.", () => skyData.topColor, delegate(Color color)
			{
				skyData.topColor = color;
				refresh.skybox = true;
			}, delegate(Color prevColor, Color nextColor)
			{
				var (propData, propData2) = prepChangeData(instance);
				getSkyboxData(propData).topColor = prevColor;
				undo.push(actionChangeProp(propData, propData2, "Skybox top color", refreshVolume: true));
				savePreviewSkyboxData(getSkyboxData(propData2));
			});
			defineColorProperty("Horizon Color", "The color of the horizon.\nThis is the color that is blended with the sky color to create a gradient.", () => skyData.midColor, delegate(Color color)
			{
				skyData.midColor = color;
				refresh.skybox = true;
			}, delegate(Color prevColor, Color nextColor)
			{
				var (propData, propData2) = prepChangeData(instance);
				getSkyboxData(propData).midColor = prevColor;
				undo.push(actionChangeProp(propData, propData2, "Skybox mid color", refreshVolume: true));
				savePreviewSkyboxData(getSkyboxData(propData2));
			});
			defineColorProperty("Ground Color", "The color of the ground.\nThis is the color that is blended with the horizon color to create a gradient.", () => skyData.botColor, delegate(Color color)
			{
				skyData.botColor = color;
				refresh.skybox = true;
			}, delegate(Color prevColor, Color nextColor)
			{
				var (propData, propData2) = prepChangeData(instance);
				getSkyboxData(propData).botColor = prevColor;
				undo.push(actionChangeProp(propData, propData2, "Skybox mid color", refreshVolume: true));
				savePreviewSkyboxData(getSkyboxData(propData2));
			});
			defineFloatProperty("Horizon Size", "The size of the horizon. The bigger the value, the more horizon is visible.", () => skyData.horizonSize, delegate(float value)
			{
				refresh.skybox = true;
				skyData.horizonSize = value;
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, propData2) = prepChangeData(instance);
				getSkyboxData(propData).horizonSize = prevValue;
				undo.push(actionChangeProp(propData, propData2, "Skybox horizon size", refreshVolume: true));
				SkyboxData skyboxData = getSkyboxData(propData2);
				savePreviewSkyboxData(skyboxData);
			}, null, 0f, 15f);
			defineFloatProperty("Exposure", "Adjusts the sky’s exposure.\nThis allows you to change tonal values in the skybox this Material generates.\nLarger values produce a more exposed, seemingly brighter, skybox.\nSmaller values produce a less exposed, seemingly darker, skybox.", () => skyData.exposure, delegate(float value)
			{
				refresh.skybox = true;
				skyData.exposure = value;
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, propData2) = prepChangeData(instance);
				getSkyboxData(propData).exposure = prevValue;
				undo.push(actionChangeProp(propData, propData2, "Skybox exposure", refreshVolume: true));
				SkyboxData skyboxData = getSkyboxData(propData2);
				savePreviewSkyboxData(skyboxData);
			}, null, 0f, 8f);
		}
		else if (skyData.type == SkyboxType.SixSided)
		{
			List<string> list = new List<string>(assets.skyboxes.Count);
			for (int num = 0; num < assets.skyboxes.Count; num++)
			{
				list.Add(assets.skyboxes[num].presetName);
			}
			int num2 = -1;
			for (int num3 = 0; num3 < assets.skyboxes.Count; num3++)
			{
				if (assets.skyboxes[num3].texture == component.data.textureData?.fileName)
				{
					num2 = num3;
					break;
				}
			}
			if (num2 == -1)
			{
				num2 = assets.skyboxes.Count;
				list.Add("Custom");
			}
			defineEnumProperty("Presets", "A selection of textures to choose from.", num2, list, delegate(int value)
			{
				SkyboxMeta skyboxMeta = assets.skyboxes[value];
				(PropData prev, PropData next) tuple = prepChangeData(instance);
				PropData item = tuple.prev;
				PropData item2 = tuple.next;
				SkyboxData skyboxData = getSkyboxData(item2);
				skyboxData.exposure2 = skyboxMeta.exposure;
				skyboxData.rotation = skyboxMeta.rotation;
				skyboxData.textureData = new SkyTexData
				{
					fileName = skyboxMeta.texture
				};
				undo.push(actionChangeProp(item, item2, "Skybox preset", refreshVolume: true));
				savePreviewSkyboxData(skyboxData);
			});
			defineFloatProperty("Exposure", "Adjusts the sky’s exposure.\nThis allows you to change tonal values in the skybox this Material generates.\nLarger values produce a more exposed, seemingly brighter, skybox.\nSmaller values produce a less exposed, seemingly darker, skybox.", () => skyData.exposure2, delegate(float value)
			{
				refresh.skybox = true;
				skyData.exposure2 = value;
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, propData2) = prepChangeData(instance);
				getSkyboxData(propData).exposure2 = prevValue;
				undo.push(actionChangeProp(propData, propData2, "Skybox exposure2", refreshVolume: true));
				savePreviewSkyboxData(getSkyboxData(propData2));
			}, null, 0f, 8f);
			defineIntProperty("Rotation", "Used to rotate the images of the custom skybox.", () => skyData.rotation, delegate(int value)
			{
				refresh.skybox = true;
				skyData.rotation = value;
			}, delegate(int prevValue, int nextValue)
			{
				var (propData, propData2) = prepChangeData(instance);
				getSkyboxData(propData).rotation = prevValue;
				undo.push(actionChangeProp(propData, propData2, "Skybox rotation", refreshVolume: true));
				savePreviewSkyboxData(getSkyboxData(propData2));
			}, null, 0, 360);
		}
		else if (skyData.type == SkyboxType.PhysicallyBased)
		{
			defineFloatProperty("Aerosol Density", "The density of the aerosol in the skybox.\nHigher values make the skybox more hazy.", () => skyData.aersolDensity, delegate(float value)
			{
				refresh.skybox = true;
				skyData.aersolDensity = value;
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, propData2) = prepChangeData(instance);
				getSkyboxData(propData).aersolDensity = prevValue;
				undo.push(actionChangeProp(propData, propData2, "Skybox aerosol density", refreshVolume: true));
				savePreviewSkyboxData(getSkyboxData(propData2));
			}, null, 0f, 1f);
			defineFloatProperty("Alpha Multiplier", "Additional strenght of the skybox color", () => skyData.alphaMultiplier, delegate(float value)
			{
				refresh.skybox = true;
				skyData.alphaMultiplier = value;
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, propData2) = prepChangeData(instance);
				getSkyboxData(propData).alphaMultiplier = prevValue;
				undo.push(actionChangeProp(propData, propData2, "Skybox alpha multiplier", refreshVolume: true));
				savePreviewSkyboxData(getSkyboxData(propData2));
			}, null, 0f, 1f);
		}
		void savePreviewSkyboxData(SkyboxData skyboxData)
		{
			refresh.skybox = true;
			if (skyboxData.activeOnStart)
			{
				skyboxOnStartActiveCache = skyboxData;
			}
			skyboxCurrentActiveCache = skyboxData;
		}
	}

	private void refreshCloudsUI(PropInstance instance)
	{
		EditorClouds component = instance.GetComponent<EditorClouds>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Clouds", "Defines the clouds for this room.\nThere can be multiple clouds in a room, but only one can be active at a time.\nClouds are a visual effect that can be used to create atmosphere in the room.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (!cache.isInteractionShown)
		{
			return;
		}
		CloudsData cloudsData = component.data;
		defineBoolProperty("Active On Start", "When checked, this skybox is activated as soon as the player starts the room.\nOnly one Cloud object can be activated on start.", cloudsData.activeOnStart, delegate(bool value)
		{
			List<EditorAction> list = new List<EditorAction>();
			foreach (KeyValuePair<InstanceID, PropInstance> propInstance in propInstances)
			{
				if (propInstance.Value != instance && propInstance.Value.TryGetComponent<EditorClouds>(out var component2) && component2.data.activeOnStart)
				{
					var (prev, propData) = prepChangeData(propInstance.Value);
					getCloudsData(propData).activeOnStart = false;
					list.Add(actionChangeProp(prev, propData, "Disable Other Clouds activeOnStart"));
				}
			}
			(PropData prev, PropData next) tuple2 = prepChangeData(instance);
			PropData item = tuple2.prev;
			PropData item2 = tuple2.next;
			CloudsData cloudsData2 = getCloudsData(item2);
			cloudsData2.activeOnStart = value;
			list.Add(actionChangeProp(item, item2, "Clouds activeOnStart"));
			undo.push(list.ToArray());
			if (value)
			{
				cloudsOnStartActiveCache = cloudsData2;
				cloudsCurrentActiveCache = cloudsData2;
			}
			else
			{
				cloudsOnStartActiveCache = null;
			}
		});
		defineBoolProperty("Local Player Only", "If true, only local player can trigger this object.\nUseful for effect that should not be seen by other players.\nFor example, when you enter a room, you might want to see different <NAME> from other players.".Replace("<NAME>", "Clouds"), cloudsData.localOnly, delegate(bool value)
		{
			cloudsData.localOnly = value;
			var (propData, next) = prepChangeData(instance);
			getCloudsData(propData).localOnly = value;
			undo.push(actionChangeProp(propData, next, "Clouds local only", refreshVolume: true));
		});
		defineFloatProperty("Transition Time", "time in seconds it takes to transition from any active clouds setting to this one.", () => cloudsData.transitionTime, delegate(float value)
		{
			cloudsData.transitionTime = value;
		}, delegate(float prevvalue, float nextvalue)
		{
			var (propData, next) = prepChangeData(instance);
			getCloudsData(propData).transitionTime = prevvalue;
			undo.push(actionChangeProp(propData, next, "clouds transition time", refreshVolume: true));
		}, null, 0f);
		defineFloatProperty("Opacity", "The opacity of the clouds.\nHigher values make the clouds more opaque.", () => cloudsData.opacity, delegate(float value)
		{
			refresh.clouds = true;
			cloudsData.opacity = value;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getCloudsData(propData).opacity = prevValue;
			undo.push(actionChangeProp(propData, propData2, "Clouds Opacity", refreshVolume: true));
			savePreviewCloudsData(getCloudsData(propData2));
		}, null, 0f, 1f);
		defineBoolProperty("Upper Hemisphere Only", "Determines whether the clouds are only visible in the upper hemisphere of the skybox.\nWhen checked, clouds are only visible above the horizon.", cloudsData.upperHemisphereOnly, delegate(bool value)
		{
			cloudsData.upperHemisphereOnly = value;
			var (propData, propData2) = prepChangeData(instance);
			getCloudsData(propData).upperHemisphereOnly = value;
			undo.push(actionChangeProp(propData, propData2, "Clouds Upper Hemisphere Only", refreshVolume: true));
			savePreviewCloudsData(getCloudsData(propData2));
		});
		defineFloatProperty("Layer 1 Strength", "The opacity of the clouds in the red channel.\nHigher values make the clouds more opaque in the red channel.", () => cloudsData.opacityColorsR, delegate(float value)
		{
			refresh.clouds = true;
			cloudsData.opacityColorsR = value;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getCloudsData(propData).opacityColorsR = prevValue;
			undo.push(actionChangeProp(propData, propData2, "Clouds Opacity R", refreshVolume: true));
			savePreviewCloudsData(getCloudsData(propData2));
		}, null, 0f, 1f);
		defineFloatProperty("Layer 2 Strength", "The opacity of the clouds in the green channel.\nHigher values make the clouds more opaque in the green channel.", () => cloudsData.opacityColorsG, delegate(float value)
		{
			refresh.clouds = true;
			cloudsData.opacityColorsG = value;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getCloudsData(propData).opacityColorsG = prevValue;
			undo.push(actionChangeProp(propData, propData2, "Clouds Opacity G", refreshVolume: true));
			savePreviewCloudsData(getCloudsData(propData2));
		}, null, 0f, 1f);
		defineFloatProperty("Layer 3 Strength", "The opacity of the clouds in the blue channel.\nHigher values make the clouds more opaque in the blue channel.", () => cloudsData.opacityColorsB, delegate(float value)
		{
			refresh.clouds = true;
			cloudsData.opacityColorsB = value;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getCloudsData(propData).opacityColorsB = prevValue;
			undo.push(actionChangeProp(propData, propData2, "Clouds Opacity B", refreshVolume: true));
			savePreviewCloudsData(getCloudsData(propData2));
		}, null, 0f, 1f);
		defineFloatProperty("Layer 4 Strength", "The opacity of the clouds in the alpha channel.\nHigher values make the clouds more opaque in the alpha channel.", () => cloudsData.opacityColorsA, delegate(float value)
		{
			refresh.clouds = true;
			cloudsData.opacityColorsA = value;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getCloudsData(propData).opacityColorsA = prevValue;
			undo.push(actionChangeProp(propData, propData2, "Clouds Opacity A", refreshVolume: true));
			savePreviewCloudsData(getCloudsData(propData2));
		}, null, 0f, 1f);
		defineFloatProperty("Rotation", "The rotation of the clouds.\nThis allows you to rotate the clouds around the skybox.", () => cloudsData.rotation, delegate(float value)
		{
			refresh.clouds = true;
			cloudsData.rotation = value;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getCloudsData(propData).rotation = prevValue;
			undo.push(actionChangeProp(propData, propData2, "Clouds Rotation", refreshVolume: true));
			savePreviewCloudsData(getCloudsData(propData2));
		}, null, 0f, 360f);
		defineColorProperty("Tint", "The color of the clouds.\nThis is the main color of the procedural clouds.", () => cloudsData.tint, delegate(Color color)
		{
			cloudsData.tint = color;
			refresh.clouds = true;
		}, delegate(Color prevColor, Color nextColor)
		{
			var (propData, propData2) = prepChangeData(instance);
			getCloudsData(propData).tint = prevColor;
			undo.push(actionChangeProp(propData, propData2, "Skybox tint", refreshVolume: true));
			savePreviewCloudsData(getCloudsData(propData2));
		});
		defineFloatProperty("Exposure", "Adjusts the clouds’ exposure.\nThis allows you to change tonal values in the clouds this Material generates.\nLarger values produce a more exposed, seemingly brighter, clouds.\nSmaller values produce a less exposed, seemingly darker, clouds.", () => cloudsData.exposure, delegate(float value)
		{
			refresh.clouds = true;
			cloudsData.exposure = value;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getCloudsData(propData).exposure = prevValue;
			undo.push(actionChangeProp(propData, propData2, "Clouds Exposure", refreshVolume: true));
			savePreviewCloudsData(getCloudsData(propData2));
		}, null, -1f, 1f);
		defineBoolProperty("Use Wind", "When checked, the clouds will move based on the wind settings.", cloudsData.useWind, delegate(bool value)
		{
			cloudsData.useWind = value;
			var (propData, propData2) = prepChangeData(instance);
			getCloudsData(propData).useWind = value;
			undo.push(actionChangeProp(propData, propData2, "Clouds Use Wind", refreshVolume: true));
			savePreviewCloudsData(getCloudsData(propData2));
		});
		if (!cloudsData.useWind)
		{
			return;
		}
		defineFloatProperty("Wind Speed", "The speed of the wind that affects the clouds.\nHigher values make the clouds move faster.", () => cloudsData.windSpeed, delegate(float value)
		{
			refresh.clouds = true;
			cloudsData.windSpeed = value;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getCloudsData(propData).windSpeed = prevValue;
			undo.push(actionChangeProp(propData, propData2, "Wind Speed", refreshVolume: true));
			savePreviewCloudsData(getCloudsData(propData2));
		}, null, 0f, 1f);
		defineFloatProperty("Wind Direction", "The orientation of the wind relative to the X world vector (in degrees).", () => cloudsData.windDirection, delegate(float value)
		{
			refresh.clouds = true;
			cloudsData.windDirection = value;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getCloudsData(propData).windDirection = prevValue;
			undo.push(actionChangeProp(propData, propData2, "Wind Direction", refreshVolume: true));
			savePreviewCloudsData(getCloudsData(propData2));
		}, null, 0f, 360f);
		defineBoolProperty("Use Wind Shadows", "When checked, the clouds will cast shadows based on the wind settings.", cloudsData.useWindShadows, delegate(bool value)
		{
			cloudsData.useWindShadows = value;
			var (propData, propData2) = prepChangeData(instance);
			getCloudsData(propData).useWindShadows = value;
			undo.push(actionChangeProp(propData, propData2, "Clouds Use Wind Shadows", refreshVolume: true));
			savePreviewCloudsData(getCloudsData(propData2));
		});
		if (cloudsData.useWindShadows)
		{
			defineColorProperty("Wind Shadows Color", "The color of the wind shadows.", () => cloudsData.windShadowsColor, delegate(Color color)
			{
				cloudsData.windShadowsColor = color;
				refresh.clouds = true;
			}, delegate(Color prevColor, Color nextColor)
			{
				var (propData, propData2) = prepChangeData(instance);
				getCloudsData(propData).windShadowsColor = prevColor;
				undo.push(actionChangeProp(propData, propData2, "Clouds Wind Shadows Color", refreshVolume: true));
				savePreviewCloudsData(getCloudsData(propData2));
			});
		}
		void savePreviewCloudsData(CloudsData cloudsData2)
		{
			refresh.clouds = true;
			if (cloudsData2.activeOnStart)
			{
				cloudsOnStartActiveCache = cloudsData2;
			}
			cloudsCurrentActiveCache = cloudsData2;
		}
	}

	private void refreshOpenLinkUI(PropInstance instance)
	{
		OpenLink openLink = instance.GetComponent<OpenLink>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Open Link", "Logic prop that allows you to open some link when targeted.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (cache.isInteractionShown)
		{
			defineStringProperty("Link (Steam/Wiki)", "Add a url link to a Steam, Steam Community or Wikipedia page.\nThe link turns red if it is invalid.", () => openLink.link, delegate(string value)
			{
				openLink.link = value;
			}, delegate(string prevValue, string nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getOpenLinkData(propData).link = prevValue;
				undo.push(actionChangeProp(propData, next, "Open Link - change link"));
			}, null, Game.verifyOpenLink);
		}
	}

	private void refreshOceanUI(PropInstance instance)
	{
		EditorOcean ocean = instance.GetComponent<EditorOcean>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Ocean", "Logic prop used to create Ocean in the scene.\nIts used for one infinite water instance.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (!cache.isInteractionShown)
		{
			return;
		}
		defineBoolProperty("Active On Start", "When checked, this Ocean is activated as soon as the player starts the room.\nOnly one Ocean object can be activated on start.", ocean.data.activeOnStart, delegate(bool value)
		{
			List<EditorAction> list = new List<EditorAction>();
			foreach (KeyValuePair<InstanceID, PropInstance> propInstance in propInstances)
			{
				if (propInstance.Value != instance && propInstance.Value.TryGetComponent<EditorOcean>(out var component) && component.data.activeOnStart)
				{
					var (prev, propData) = prepChangeData(propInstance.Value);
					getOceanData(propData).activeOnStart = false;
					list.Add(actionChangeProp(prev, propData, "Disable Other Ocean activeOnStart"));
				}
			}
			(PropData prev, PropData next) tuple2 = prepChangeData(instance);
			PropData item = tuple2.prev;
			PropData item2 = tuple2.next;
			OceanData oceanData = getOceanData(item2);
			oceanData.activeOnStart = value;
			list.Add(actionChangeProp(item, item2, "Ocean activeOnStart"));
			undo.push(list.ToArray());
			if (value)
			{
				oceanOnStartActiveCache = oceanData;
				oceanCurrentActiveCache = oceanData;
			}
			else
			{
				oceanOnStartActiveCache = null;
			}
		});
		defineBoolProperty("Local Player Only", "If true, only local player can trigger this object.\nUseful for effect that should not be seen by other players.\nFor example, when you enter a room, you might want to see different <NAME> from other players.".Replace("<NAME>", "Ocean"), ocean.data.localOnly, delegate(bool value)
		{
			ocean.data.localOnly = value;
			var (propData, next) = prepChangeData(instance);
			getOceanData(propData).localOnly = value;
			undo.push(actionChangeProp(propData, next, "Ocean local only", refreshVolume: true));
		});
		defineFloatProperty("Transition Time", "time in seconds it takes to transition from any active ocean setting to this one.", () => ocean.data.transitionTime, delegate(float value)
		{
			ocean.data.transitionTime = value;
		}, delegate(float prevvalue, float nextvalue)
		{
			var (propData, next) = prepChangeData(instance);
			getOceanData(propData).transitionTime = prevvalue;
			undo.push(actionChangeProp(propData, next, "ocean transition time", refreshVolume: true));
		}, null, 0f);
		defineFloatProperty("Distant Wind Size", "Represents the speed of distant wind in kilometers per hour.", () => ocean.data.distantWindSize, delegate(float value)
		{
			refresh.ocean = true;
			ocean.data.distantWindSize = value;
		}, delegate(float prevvalue, float nextvalue)
		{
			var (propData, next) = prepChangeData(instance);
			getOceanData(propData).distantWindSize = prevvalue;
			undo.push(actionChangeProp(propData, next, "ocean distanat wind", refreshVolume: true));
		}, null, 0f, 250f);
		defineFloatProperty("Chaos", "Determines how chaotic the ocean is.", () => ocean.data.chaos, delegate(float value)
		{
			refresh.ocean = true;
			ocean.data.chaos = value;
		}, delegate(float prevvalue, float nextvalue)
		{
			var (propData, next) = prepChangeData(instance);
			getOceanData(propData).chaos = prevvalue;
			undo.push(actionChangeProp(propData, next, "ocean chaos", refreshVolume: true));
		}, null, 0f, 1f);
		defineFloatProperty("Speed", "Translates the swell at a constant speed.", () => ocean.data.currentSpeed, delegate(float value)
		{
			refresh.ocean = true;
			ocean.data.currentSpeed = value;
		}, delegate(float prevvalue, float nextvalue)
		{
			var (propData, next) = prepChangeData(instance);
			getOceanData(propData).currentSpeed = prevvalue;
			undo.push(actionChangeProp(propData, next, "ocean current speed", refreshVolume: true));
		}, null, 0f, 20f);
		if (ocean.data.currentSpeed > 0f)
		{
			defineFloatProperty("Direction", "Determines the direction of the Ocean current.", () => ocean.data.currentDirection, delegate(float value)
			{
				refresh.ocean = true;
				ocean.data.currentDirection = value;
			}, delegate(float prevvalue, float nextvalue)
			{
				var (propData, next) = prepChangeData(instance);
				getOceanData(propData).currentDirection = prevvalue;
				undo.push(actionChangeProp(propData, next, "ocean current speed direction", refreshVolume: true));
			}, null, 0f, 360f);
		}
		defineFloatProperty("Amplitude Modifier", "Modifies the amplitude of the ocean waves.", () => ocean.data.amplitudeModifier, delegate(float value)
		{
			refresh.ocean = true;
			ocean.data.amplitudeModifier = value;
		}, delegate(float prevvalue, float nextvalue)
		{
			var (propData, next) = prepChangeData(instance);
			getOceanData(propData).amplitudeModifier = prevvalue;
			undo.push(actionChangeProp(propData, next, "ocean amplitude modi", refreshVolume: true));
		}, null, 0f, 1f);
		defineFloatProperty("Ripple Speed", "Determines how fast the ripples move across the ocean surface.", () => ocean.data.rippleSpeed, delegate(float value)
		{
			refresh.ocean = true;
			ocean.data.rippleSpeed = value;
		}, delegate(float prevvalue, float nextvalue)
		{
			var (propData, next) = prepChangeData(instance);
			getOceanData(propData).rippleSpeed = prevvalue;
			undo.push(actionChangeProp(propData, next, "ocean ripple speed", refreshVolume: true));
		}, null, 0f, 15f);
		defineColorProperty("Water Color", "Determines the color of the Ocean.", () => ocean.data.waterColor, delegate(Color color)
		{
			ocean.data.waterColor = color;
			refresh.ocean = true;
		}, delegate(Color prevColor, Color nextColor)
		{
			var (propData, next) = prepChangeData(instance);
			getOceanData(propData).waterColor = prevColor;
			undo.push(actionChangeProp(propData, next, "Ocean Color"));
		});
		defineColorProperty("Refraction Color", "Determines the color of the ocean refraction.", () => ocean.data.refractionColor, delegate(Color color)
		{
			ocean.data.refractionColor = color;
			refresh.ocean = true;
		}, delegate(Color prevColor, Color nextColor)
		{
			var (propData, next) = prepChangeData(instance);
			getOceanData(propData).refractionColor = prevColor;
			undo.push(actionChangeProp(propData, next, "Ocean Refraction Color"));
		});
		defineFloatProperty("Ambient Intensity", "Determines how much ambient light is reflected from the ocean surface.", () => ocean.data.ambientIntensity, delegate(float value)
		{
			refresh.ocean = true;
			ocean.data.ambientIntensity = value;
		}, delegate(float prevvalue, float nextvalue)
		{
			var (propData, next) = prepChangeData(instance);
			getOceanData(propData).ambientIntensity = prevvalue;
			undo.push(actionChangeProp(propData, next, "ocean ripple speed", refreshVolume: true));
		}, null, 0f, 1f);
		defineFloatProperty("Ocean Height", "Determines the height of the ocean surface.", () => ocean.data.height, delegate(float value)
		{
			refresh.ocean = true;
			ocean.data.height = value;
		}, delegate(float prevvalue, float nextvalue)
		{
			var (propData, next) = prepChangeData(instance);
			getOceanData(propData).height = prevvalue;
			undo.push(actionChangeProp(propData, next, "ocean height", refreshVolume: true));
		}, null, float.NegativeInfinity, float.PositiveInfinity, 0.1f);
	}

	private void refreshWaterUI(PropInstance instance)
	{
		EditorWater water = instance.GetComponent<EditorWater>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Water", "Logic prop used to create water in the scene.\nIts used for smaller water instances.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (cache.isInteractionShown)
		{
			defineEnumProperty("Shape", "Determines the visual type of the water surface.", (int)water.data.geometryType, new List<string> { "Plane", "Circle" }, delegate(int value)
			{
				var (prev, propData) = prepChangeData(instance);
				getWaterData(propData).geometryType = (EditorWater.WaterGeometryType)value;
				water.syncVisuals();
				undo.push(actionChangeProp(prev, propData, "Water geometry type"));
			});
			defineFloatProperty("Time Scale", "Determines how fast the water moves.", () => water.data.timeScale, delegate(float value)
			{
				water.data.timeScale = value;
				water.syncVisuals();
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getWaterData(propData).timeScale = prevValue;
				undo.push(actionChangeProp(propData, next, "Water time scale"));
			}, null, 0f, 10f, 0.01f);
			defineFloatProperty("Wind Speed", "Determines how fast the water moves in the wind.", () => water.data.localWindSpeed, delegate(float value)
			{
				water.data.localWindSpeed = value;
				water.syncVisuals();
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getWaterData(propData).localWindSpeed = prevValue;
				undo.push(actionChangeProp(propData, next, "Water local wind speed"));
			}, null, 0f, 15f, 0.01f);
			defineFloatProperty("Chaos", "Determines how chaotic the water is.", () => water.data.chaos, delegate(float value)
			{
				water.data.chaos = value;
				water.syncVisuals();
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getWaterData(propData).chaos = prevValue;
				undo.push(actionChangeProp(propData, next, "Water chaos"));
			}, null, 0f, 1f, 0.01f);
			defineFloatProperty("Ripple Speed", "Determines how fast the ripples move across the water surface.", () => water.data.rippleSpeed, delegate(float value)
			{
				water.data.rippleSpeed = value;
				water.syncVisuals();
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getWaterData(propData).rippleSpeed = prevValue;
				undo.push(actionChangeProp(propData, next, "Water ripple speed"));
			}, null, 0f, 20f, 0.01f);
			defineColorProperty("Water Color", "Determines the color of the water.", () => water.data.waterColor, delegate(Color color)
			{
				water.data.waterColor = color;
				water.syncVisuals();
			}, delegate(Color prevColor, Color nextColor)
			{
				var (propData, next) = prepChangeData(instance);
				getWaterData(propData).waterColor = prevColor;
				undo.push(actionChangeProp(propData, next, "Water Color"));
			});
			defineColorProperty("Refraction Color", "Determines the color of the water refraction.", () => water.data.refractionColor, delegate(Color color)
			{
				water.data.refractionColor = color;
				water.syncVisuals();
			}, delegate(Color prevColor, Color nextColor)
			{
				var (propData, next) = prepChangeData(instance);
				getWaterData(propData).refractionColor = prevColor;
				undo.push(actionChangeProp(propData, next, "Water Refraction Color"));
			});
			defineFloatProperty("Ambient Intensity", "Determines how much ambient light is reflected from the water surface.", () => water.data.ambientIntensity, delegate(float value)
			{
				water.data.ambientIntensity = value;
				water.syncVisuals();
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getWaterData(propData).ambientIntensity = prevValue;
				undo.push(actionChangeProp(propData, next, "Water ambient intensity"));
			}, null, 0f, 1f, 0.01f);
		}
	}

	private void refreshFinishUI(PropInstance instance)
	{
		instance.GetComponent<Finish>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Finish", "Prop that, when targeted, will make it so that players have beaten the room.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (cache.isInteractionShown)
		{
			defineButtonProperty("Edit Camera Pose", "Set the camera position of the camera when the player finishes the room.", delegate
			{
				toggleFinishRotationMode(show: true);
			});
		}
	}

	private void refreshScriptUI(PropInstance instance)
	{
		LuaExecutor script = instance.GetComponent<LuaExecutor>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Script", "Logic prop used to channel Lua code to interact with other props.\nFind the documentation and get help from the community on our official Pine Studio Discord!", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (!cache.isInteractionShown)
		{
			return;
		}
		List<string> allScriptNames = getAllScriptNames(roomDirPath);
		bool flag = false;
		if (allScriptNames.Count == 0)
		{
			defineHintProperty("There are no scripts in this room.\nYou can create a new script by clicking the button below.");
		}
		else
		{
			List<string> allScripts = new List<string> { "None" };
			allScripts.AddRange(allScriptNames);
			int num = allScripts.IndexOf(script.luaCodeLocation);
			defineEnumProperty("Script Code", "Selects which script this prop will execute.", (num >= 0 && num < allScripts.Count) ? num : 0, allScripts, delegate(int value)
			{
				var (prev, propData) = prepChangeData(instance);
				getScriptComponentData(propData).scriptLocation = ((value == 0) ? string.Empty : allScripts[value]);
				undo.push(actionChangeProp(prev, propData, "Code location"));
			});
			if (num > 0)
			{
				flag = true;
			}
		}
		if (!string.IsNullOrEmpty(script.luaCodeLocation))
		{
			defineBoolProperty("Invoke Function", "When checked, this script can be triggered to execute one its function by other props.", script.canBeTriggered, delegate(bool value)
			{
				var (prev, propData) = prepChangeData(instance);
				getScriptComponentData(propData).canBeTriggered = value;
				undo.push(actionChangeProp(prev, propData, "Lua can be triggered"));
			});
			if (script.canBeTriggered)
			{
				IEnumerable<string> enumerable = File.ReadLines(Path.Combine(roomDirPath, script.luaCodeLocation + ".lua"));
				List<string> functionNames = new List<string>();
				foreach (string item in enumerable)
				{
					string text = item.Trim();
					if (text.StartsWith("function ") && text.EndsWith("()"))
					{
						string text2 = text.Split(new char[3] { ' ', '(', ')' }, StringSplitOptions.RemoveEmptyEntries)[1];
						if (text2 != "onInit" && text2 != "onUpdate")
						{
							functionNames.Add(text2);
						}
					}
				}
				functionNames.Insert(0, "None");
				int num2 = functionNames.IndexOf(script.functionToCall);
				if (num2 == -1)
				{
					num2 = 0;
				}
				defineEnumProperty("Function", "The name of the function to call when this script is triggered.\nThe function must be defined in the script file.\nIt also MUST have no parameters.", num2, functionNames, delegate(int value)
				{
					var (prev, propData) = prepChangeData(instance);
					getScriptComponentData(propData).functionToCall = functionNames[value];
					undo.push(actionChangeProp(prev, propData, "Lua function to call"));
				});
			}
		}
		if (flag)
		{
			defineButtonProperty("Edit Script", "Press to edit currently selected script.", delegate
			{
				string persistentDataPath = Application.persistentDataPath;
				string uGCFolder = getUGCFolder();
				string path = Path.Combine(roomDirPath, script.luaCodeLocation + ".lua");
				if (File.Exists(path))
				{
					openTextEditor(path);
				}
				else if (Directory.Exists(roomDirPath))
				{
					openFile(roomDirPath);
				}
				else if (Directory.Exists(uGCFolder))
				{
					openFile(uGCFolder);
				}
				else if (Directory.Exists(persistentDataPath))
				{
					openFile(persistentDataPath);
				}
			});
		}
		defineButtonProperty("Add Script", "Create a new script file in the room directory.", delegate
		{
			openNewScriptDialog("newScriptFromInspector", null, getDefaultScriptName());
		});
	}

	private void refreshLightUI(PropInstance instance)
	{
		EditorLight light = instance.GetComponent<EditorLight>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Light", "Logic prop used to emit light in the scene.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (!cache.isInteractionShown)
		{
			return;
		}
		defineEnumProperty("Type", "Determines the shape of the light's emission.", (int)light.lightComponent.type, new List<string> { "Spot", "Directional", "Point" }, delegate(int value)
		{
			var (prev, propData) = prepChangeData(instance);
			getEditorLightData(propData).type = (LightType)value;
			undo.push(actionChangeProp(prev, propData, "Light Type"));
		}, useInterpolationPopup: false, new List<string> { "Emits light in a cone shape.", "A light that is located infinitely far away and emits light in one direction.", "Located at a point and emits light in all directions equally." });
		defineColorProperty("Color", "The color that the selected light prop emits.", () => light.lightComponent.color, delegate(Color color)
		{
			light.lightComponent.color = color;
		}, delegate(Color prevColor, Color nextColor)
		{
			var (propData, next) = prepChangeData(instance);
			getEditorLightData(propData).color = prevColor;
			undo.push(actionChangeProp(propData, next, "Light Color"));
		});
		defineFloatProperty("Intensity", "Determines the light's brightness multiplied with the light color.", () => light.lightComponent.intensity, delegate(float value)
		{
			light.lightComponent.intensity = value;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, next) = prepChangeData(instance);
			getEditorLightData(propData).intensity = prevValue;
			undo.push(actionChangeProp(propData, next, "Light Intensity"));
		}, null, 0f, float.PositiveInfinity, 0.1f);
		LightType type = light.lightComponent.type;
		if (type == LightType.Point || type == LightType.Spot)
		{
			defineFloatProperty("Range", "Determines the distance the light travels before stopping.", () => light.lightComponent.range, delegate(float value)
			{
				light.lightComponent.range = value;
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getEditorLightData(propData).range = prevValue;
				undo.push(actionChangeProp(propData, next, "Light Range"));
			}, null, 0f, float.PositiveInfinity, 0.1f);
		}
		if (light.lightComponent == null)
		{
			light.lightComponent = light.GetComponent<Light>();
		}
		if (light.lightComponent.type == LightType.Spot)
		{
			defineFloatProperty("Spot Angle", "Determines the angle of the spotlight cone in degrees.", () => light.lightComponent.spotAngle, delegate(float value)
			{
				light.lightComponent.spotAngle = value;
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getEditorLightData(propData).spotAngle = prevValue;
				undo.push(actionChangeProp(propData, next, "Light Spot angle"));
			}, null, 1f, 179f, 0.1f);
		}
		defineBoolProperty("Cast Shadows", "Determines whether the light casts shadows.", light.lightComponent.shadows != LightShadows.None, delegate(bool value)
		{
			var (prev, propData) = prepChangeData(instance);
			getEditorLightData(propData).castShadows = value;
			undo.push(actionChangeProp(prev, propData, "Light shadows"));
		});
		if (light.lightComponent.shadows != LightShadows.None)
		{
			defineFloatProperty("Shadow Dimmer", "Controls the darkness of the shadows cast by this light.", () => light.hdLightData.shadowDimmer, delegate(float value)
			{
				light.hdLightData.shadowDimmer = value;
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getEditorLightData(propData).shadowDimmer = prevValue;
				undo.push(actionChangeProp(propData, next, "Light Shadow Dimmer"));
			}, null, 0f, 1f, 0.1f);
		}
	}

	private void refreshLadderUI(PropInstance instance)
	{
		Ladder ladder = instance.GetComponent<Ladder>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Ladder", "Logic prop used to climb up and down.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (cache.isInteractionShown)
		{
			defineVector3Property("Top Point", "Defines the upper-most point of the ladder.\nWhen player reaches this point while climbing up, player will exit the ladder.", () => ladder.upEmpty.transform.localPosition, delegate(Vector3 value)
			{
				ladder.upEmpty.transform.localPosition = value;
			}, delegate(Vector3 prevValue, Vector3 nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getLadderData(propData).upLocalPosition = prevValue;
				undo.push(actionChangeProp(propData, next, "Ladder Top Point"));
			}, null, 0.01f);
			defineVector3Property("Top Exit", "Position where the player will be moved when exiting at the top of the ladder.", () => ladder.upExit.transform.localPosition, delegate(Vector3 value)
			{
				ladder.upExit.transform.localPosition = value;
			}, delegate(Vector3 prevValue, Vector3 nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getLadderData(propData).upExitLocalPosition = prevValue;
				undo.push(actionChangeProp(propData, next, "Ladder Top Exit"));
			}, null, 0.01f);
			defineVector3Property("Bottom Point", "Defines the lower-most point of the ladder.\nWhen player reaches this point while climbing down, player will exit the ladder.", () => ladder.downEmpty.transform.localPosition, delegate(Vector3 value)
			{
				ladder.downEmpty.transform.localPosition = value;
			}, delegate(Vector3 prevValue, Vector3 nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getLadderData(propData).downLocalPosition = prevValue;
				undo.push(actionChangeProp(propData, next, "Ladder Bottom Point"));
			}, null, 0.01f);
			defineVector3Property("Bottom Exit", "Position where the player will be moved when exiting at the bottom of the ladder.", () => ladder.downExit.transform.localPosition, delegate(Vector3 value)
			{
				ladder.downExit.transform.localPosition = value;
			}, delegate(Vector3 prevValue, Vector3 nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getLadderData(propData).downExitLocalPosition = prevValue;
				undo.push(actionChangeProp(propData, next, "Ladder Bottom Exit"));
			}, null, 0.01f);
		}
	}

	private void refreshFogUI(PropInstance instance)
	{
		Fog component = instance.GetComponent<Fog>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Fog", "Defines the fog settings for this room.\nFog is a visual effect that can be used to create atmosphere in the room.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (!cache.isInteractionShown)
		{
			return;
		}
		FogData fogData = component.data;
		defineBoolProperty("Active On Start", "When checked, the fog is activated as soon as the player starts the room.\nOnly one fog can be activated on start.", fogData.activeOnStart, delegate(bool value)
		{
			List<EditorAction> list = new List<EditorAction>();
			foreach (KeyValuePair<InstanceID, PropInstance> propInstance in propInstances)
			{
				if (propInstance.Value != instance && propInstance.Value.TryGetComponent<Fog>(out var component2) && component2.data.activeOnStart)
				{
					var (prev, propData) = prepChangeData(propInstance.Value);
					getFogData(propData).activeOnStart = false;
					list.Add(actionChangeProp(prev, propData, "Disable Other Fog activeOnStart"));
				}
			}
			var (prev2, propData2) = prepChangeData(instance);
			getFogData(propData2).activeOnStart = value;
			list.Add(actionChangeProp(prev2, propData2, "Fog activeOnStart", refreshVolume: true));
			undo.push(list.ToArray());
			if (value)
			{
				fogOnStartActiveCache = fogData;
				fogCurrentActiveCache = fogData;
			}
			else
			{
				fogOnStartActiveCache = null;
			}
		});
		defineBoolProperty("Local Player Only", "If true, only local player can trigger this object.\nUseful for effect that should not be seen by other players.\nFor example, when you enter a room, you might want to see different <NAME> from other players.".Replace("<NAME>", "Fog"), fogData.localOnly, delegate(bool value)
		{
			var (prev, propData) = prepChangeData(instance);
			getFogData(propData).localOnly = value;
			undo.push(actionChangeProp(prev, propData, "Fog local only", refreshVolume: true));
		});
		defineFloatProperty("Transition", "Time in seconds it takes to transition from any active fog setting to this one.", () => fogData.transitionTime, delegate(float value)
		{
			fogData.transitionTime = value;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, next) = prepChangeData(instance);
			getFogData(propData).transitionTime = prevValue;
			undo.push(actionChangeProp(propData, next, "Fog transitionTime", refreshVolume: true));
		}, null, 0f, float.PositiveInfinity, 0.01f);
		defineFloatProperty("Attenuation", "Controls the density of the fog.\nHigher value make the fog less dense.", () => fogData.attenuation, delegate(float value)
		{
			refresh.fog = true;
			fogData.attenuation = value;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getFogData(propData).attenuation = prevValue;
			undo.push(actionChangeProp(propData, propData2, "Fog Attenuation", refreshVolume: true));
			savePreviewFogData(getFogData(propData2));
		}, null, 0.1f, 50f, 0.1f);
		defineFloatProperty("Distance", "The distance value of the fog.\nHigher values make the fog extend further.", () => fogData.distance, delegate(float value)
		{
			refresh.fog = true;
			fogData.distance = value;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getFogData(propData).distance = prevValue;
			undo.push(actionChangeProp(propData, propData2, "Fog Distance", refreshVolume: true));
			savePreviewFogData(getFogData(propData2));
		}, null, 0f, 100f, 0.1f);
		defineFloatProperty("Height", "The height value of the fog.\nHigher values make the fog extend higher.", () => fogData.maxHeight, delegate(float value)
		{
			refresh.fog = true;
			fogData.maxHeight = value;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getFogData(propData).maxHeight = prevValue;
			undo.push(actionChangeProp(propData, propData2, "Fog Height", refreshVolume: true));
			savePreviewFogData(getFogData(propData2));
		}, null, 0f, 100f, 0.1f);
		defineColorProperty("Color", "The color value of the selected fog.", () => fogData.color, delegate(Color color)
		{
			fogData.color = color;
			refresh.fog = true;
		}, delegate(Color prevColor, Color nextColor)
		{
			var (propData, propData2) = prepChangeData(instance);
			getFogData(propData).color = prevColor;
			undo.push(actionChangeProp(propData, propData2, "Fog Color", refreshVolume: true));
			savePreviewFogData(getFogData(propData2));
		});
		defineBoolProperty("Is Volumetric", "When checked, the fog is volumetric.\nWhen not checked, the fog is a simple overlay.", fogData.isVolumetric, delegate(bool value)
		{
			var (prev, propData) = prepChangeData(instance);
			getFogData(propData).isVolumetric = value;
			undo.push(actionChangeProp(prev, propData, "Fog isVolumetric", refreshVolume: true));
			savePreviewFogData(getFogData(propData));
		});
		if (fogData.isVolumetric)
		{
			defineColorProperty("Volumetric Color", "The color value of the volumetric fog.\nThis is only used when the fog is volumetric.", () => fogData.volumetricColor, delegate(Color color)
			{
				fogData.volumetricColor = color;
				refresh.fog = true;
			}, delegate(Color prevColor, Color nextColor)
			{
				var (propData, propData2) = prepChangeData(instance);
				getFogData(propData).volumetricColor = prevColor;
				undo.push(actionChangeProp(propData, propData2, "Fog Volumetric Color", refreshVolume: true));
				savePreviewFogData(getFogData(propData2));
			});
		}
		void savePreviewFogData(FogData fogData2)
		{
			refresh.fog = true;
			if (fogData2.activeOnStart)
			{
				fogOnStartActiveCache = fogData2;
			}
			fogCurrentActiveCache = fogData2;
		}
	}

	private void refreshActivatorUI(PropInstance instance)
	{
		ActivatorComponent activator = instance.GetComponent<ActivatorComponent>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Activator", "Used to activate, deactivate or toggle properties of an object.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (!cache.isInteractionShown)
		{
			return;
		}
		defineEnumProperty("Activation Type", "Determines how the activator will change the targeted properties on the prop.", (int)activator.type, new List<string> { "Disable", "Enable", "Toggle" }, delegate(int value)
		{
			var (prev, propData) = prepChangeData(instance);
			getActivatorComponentData(propData).type = (ActivatorComponent.ActivatorType)value;
			undo.push(actionChangeProp(prev, propData, "Activation type"));
		}, useInterpolationPopup: false, new List<string> { "Always disables the targeted properties.", "Always enables the targeted properties.", "Enables/disables the targeted properties based on their previous state on the prop." });
		defineBoolProperty("Targeting Object", "When checked, the activator will disable the targeted prop,\nincluding its renderer, collider, and children.", activator.targetObject, delegate(bool value)
		{
			var (prev, propData) = prepChangeData(instance);
			getActivatorComponentData(propData).targetObject = value;
			undo.push(actionChangeProp(prev, propData, "Activator Toggle Target Object"));
		});
		if (!activator.targetObject)
		{
			defineBoolProperty("Targeting Renderer", "When checked, the activator will target prop's renderer.", activator.targetRenderer, delegate(bool value)
			{
				var (prev, propData) = prepChangeData(instance);
				getActivatorComponentData(propData).targetRenderer = value;
				undo.push(actionChangeProp(prev, propData, "Activator Toggle Renderer"));
			});
			defineBoolProperty("Targeting Collider", "When checked, the activator will target prop's collider.\nIf the prop uses physics and activator disables the collider, it will fall through the floor.", activator.targetCollider, delegate(bool value)
			{
				var (prev, propData) = prepChangeData(instance);
				getActivatorComponentData(propData).targetCollider = value;
				undo.push(actionChangeProp(prev, propData, "Activator Toggle Collider"));
			});
			defineBoolProperty("Targeting Obstacle", "When checked, the activator will target prop's obstacle property.", activator.targetObstacle, delegate(bool value)
			{
				var (prev, propData) = prepChangeData(instance);
				getActivatorComponentData(propData).targetObstacle = value;
				undo.push(actionChangeProp(prev, propData, "Activator Toggle Obstacle"));
			});
			defineBoolProperty("Targeting Usable", "When checked, the activator will target prop's usable/targetable property. If the prop is not usable, the player cannot interact with it.", activator.targetTargetable, delegate(bool value)
			{
				var (prev, propData) = prepChangeData(instance);
				getActivatorComponentData(propData).targetTargetable = value;
				undo.push(actionChangeProp(prev, propData, "Activator Toggle Targetable"));
			});
			defineSeparatorProperty(4f);
		}
		defineBoolProperty("Active On Start", "When checked, the activator is triggered as soon as the player starts the room.", activator.activeOnStart, delegate(bool value)
		{
			var (prev, propData) = prepChangeData(instance);
			getActivatorComponentData(propData).activeOnStart = value;
			undo.push(actionChangeProp(prev, propData, "Activator Component activeOnStart"));
		});
		List<PropInstance> list = new List<PropInstance>();
		foreach (GameObject key in activator.keys)
		{
			list.Add(key.GetComponent<PropInstance>());
		}
		defineTargetsProperty("%RE_targetObjects%", "%RE_activatorTargetObjectsTooltip%", PrepChangeDataStrategy.UseTarget, delegate(PropData prev, PropData next, PropInstance value)
		{
			getActivatorComponentData(next).keys.Add(value.ID);
		}, delegate(PropData prev, PropData next, int index)
		{
			getActivatorComponentData(next).keys.RemoveAt(index);
		}, instance, targetActivatorPredicate, list, -1);
		bool targetActivatorPredicate(PropInstance pi)
		{
			if (pi == instance)
			{
				return false;
			}
			if (pi.GetComponent<SpawnPoint>() == null && pi.GetComponent<Finish>() == null && !pi.TryGetComponent<EditorPuzzle>(out var _))
			{
				return !activator.keys.Exists((GameObject x) => x == pi.gameObject);
			}
			return false;
		}
	}

	private void refreshRouletteUI(PropInstance instance)
	{
		Roulette roulette = instance.GetComponent<Roulette>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Roulette", "A roulette is a prop that randomly selects a target from the list of targets.\nThe roulette can be used to create random events in the room.\nRoulette is synced in co-op.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (!cache.isInteractionShown)
		{
			return;
		}
		defineTargetsProperty("%random_targets%", "%random_targetsTooltip%", instance, (PropData propData) => getRouletteData(propData).targets, defaultTargetPredicate(instance, roulette.targets, withLock: false), roulette.targets);
		defineBoolProperty("Target Only Once", "If true the selected and triggered random target will be removed\nfrom the list of possible targets when triggering this Roulette again.", roulette.targetCanBeActivatedOnlyOnce, delegate(bool value)
		{
			var (prev, propData) = prepChangeData(instance);
			getRouletteData(propData).removeTargetOnTrigger = value;
			undo.push(actionChangeProp(prev, propData, "Roulette - remove target on trigger"));
		});
		if (roulette.targets.Count <= 0)
		{
			return;
		}
		defineSeparatorProperty();
		List<PropInstance> list = new List<PropInstance>();
		if (roulette.testingTarget != -1)
		{
			list.Add(roulette.targets[roulette.testingTarget].GetComponent<PropInstance>());
		}
		defineTargetsProperty("Testing Target", "%roulette_testingTargetTooltip%", PrepChangeDataStrategy.UseTarget, delegate(PropData prev, PropData next, PropInstance value)
		{
			getRouletteData(next).testingTarget = roulette.targets.FindIndex((GameObject target) => target == value.gameObject);
		}, delegate(PropData prev, PropData next, int index)
		{
			getRouletteData(next).testingTarget = -1;
		}, instance, targetInListPredicate(instance, roulette.targets), list, 1);
		defineHintProperty("The value above is only used for testing.");
	}

	private void refreshPostProcessingUI(PropInstance instance)
	{
		EditorPostProcessingData postProcessing = instance.GetComponent<EditorPostProcessing>().data;
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Post Processing ", "Defines the post processing effect for this room.\nThere can be multiple post processing effects in a room, but only one can be active at a time.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (!cache.isInteractionShown)
		{
			return;
		}
		defineBoolProperty("Active On Start", "When checked, this effect is activated as soon as the player starts the room.\nOnly one effect can be activated on start.", postProcessing.activeOnStart, delegate(bool value)
		{
			List<EditorAction> list = new List<EditorAction>();
			foreach (KeyValuePair<InstanceID, PropInstance> propInstance in propInstances)
			{
				if (propInstance.Value != instance && propInstance.Value.TryGetComponent<EditorPostProcessing>(out var component) && component.data.activeOnStart)
				{
					var (prev, propData) = prepChangeData(propInstance.Value);
					getEditorPostProcessingData(propData).activeOnStart = false;
					list.Add(actionChangeProp(prev, propData, "Disable Other Post Processing activeOnStart"));
				}
			}
			(PropData prev, PropData next) tuple2 = prepChangeData(instance);
			PropData item = tuple2.prev;
			PropData item2 = tuple2.next;
			EditorPostProcessingData editorPostProcessingData = getEditorPostProcessingData(item2);
			editorPostProcessingData.activeOnStart = value;
			list.Add(actionChangeProp(item, item2, "Post Processing activeOnStart"));
			undo.push(list.ToArray());
			refresh.postProcessing = true;
			if (value)
			{
				editorCurrentActivePostProcessing = editorPostProcessingData;
				editorPostProcessingOnStartActive = editorPostProcessingData;
			}
			else
			{
				editorPostProcessingOnStartActive = null;
			}
		});
		defineBoolProperty("Local Player Only", "If true, only local player can trigger this object.\nUseful for effect that should not be seen by other players.\nFor example, when you enter a room, you might want to see different <NAME> from other players.".Replace("<NAME>", "Post Processing"), postProcessing.localOnly, delegate(bool value)
		{
			postProcessing.localOnly = value;
			var (propData, next) = prepChangeData(instance);
			getEditorPostProcessingData(propData).localOnly = value;
			undo.push(actionChangeProp(propData, next, "PP local only", refreshVolume: true));
		});
		defineFloatProperty("Transition", "Time in seconds it takes to transition from one Post Processing setting to this one.", () => postProcessing.transitionTime, delegate(float value)
		{
			postProcessing.transitionTime = value;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, next) = prepChangeData(instance);
			getEditorPostProcessingData(propData).transitionTime = prevValue;
			undo.push(actionChangeProp(propData, next, "Post Processing transitionTime"));
		}, null, 0f, float.PositiveInfinity, 0.01f);
		defineFloatProperty("Exposure", "Adjusts the overall exposure of the scene.", () => postProcessing.exposure, delegate(float value)
		{
			refresh.postProcessing = true;
			postProcessing.exposure = value;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getEditorPostProcessingData(propData).exposure = prevValue;
			undo.push(actionChangeProp(propData, propData2, "pp exposure"));
			refresh.postProcessing = true;
			EditorPostProcessingData editorPostProcessingData = getEditorPostProcessingData(propData2);
			if (editorPostProcessingData.activeOnStart)
			{
				editorPostProcessingOnStartActive = editorPostProcessingData;
			}
			editorCurrentActivePostProcessing = editorPostProcessingData;
		}, null, -1f, 1f);
		defineFloatProperty("SSAO Intensity", "Determines the intensity of the Screen Space Ambient Occlusion effect.", () => postProcessing.ssaoIntensity, delegate(float value)
		{
			postProcessing.ssaoIntensity = value;
			refresh.postProcessing = true;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getEditorPostProcessingData(propData).ssaoIntensity = prevValue;
			undo.push(actionChangeProp(propData, propData2, "pp ssao Intensity"));
			refresh.postProcessing = true;
			EditorPostProcessingData editorPostProcessingData = getEditorPostProcessingData(propData2);
			if (editorPostProcessingData.activeOnStart)
			{
				editorPostProcessingOnStartActive = editorPostProcessingData;
			}
			editorCurrentActivePostProcessing = editorPostProcessingData;
		}, null, 0f, 4f);
		defineFloatProperty("SSAO Radius", "Determines the radius of the Screen Space Ambient Occlusion effect.", () => postProcessing.ssaoRadius, delegate(float value)
		{
			postProcessing.ssaoRadius = value;
			refresh.postProcessing = true;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getEditorPostProcessingData(propData).ssaoRadius = prevValue;
			undo.push(actionChangeProp(propData, propData2, "pp ssao Radius"));
			refresh.postProcessing = true;
			EditorPostProcessingData editorPostProcessingData = getEditorPostProcessingData(propData2);
			if (editorPostProcessingData.activeOnStart)
			{
				editorPostProcessingOnStartActive = editorPostProcessingData;
			}
			editorCurrentActivePostProcessing = editorPostProcessingData;
		}, null, -0.25f, 5f);
		defineFloatProperty("Bloom Threshold", "Determines the threshold for the bloom effect.\nLarger values make the bloom effect less intense.", () => postProcessing.bloomThreshold, delegate(float value)
		{
			postProcessing.bloomThreshold = value;
			refresh.postProcessing = true;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getEditorPostProcessingData(propData).bloomThreshold = prevValue;
			undo.push(actionChangeProp(propData, propData2, "pp bloom Threshold"));
			refresh.postProcessing = true;
			EditorPostProcessingData editorPostProcessingData = getEditorPostProcessingData(propData2);
			if (editorPostProcessingData.activeOnStart)
			{
				editorPostProcessingOnStartActive = editorPostProcessingData;
			}
			editorCurrentActivePostProcessing = editorPostProcessingData;
		}, null, 0f, 5f);
		defineFloatProperty("Bloom Intensity", "Determines the intensity of the bloom effect.\nLarger values make the bloom effect more intense.", () => postProcessing.bloomIntensity, delegate(float value)
		{
			postProcessing.bloomIntensity = value;
			refresh.postProcessing = true;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getEditorPostProcessingData(propData).bloomIntensity = prevValue;
			undo.push(actionChangeProp(propData, propData2, "pp bloom Intensity"));
			refresh.postProcessing = true;
			EditorPostProcessingData editorPostProcessingData = getEditorPostProcessingData(propData2);
			if (editorPostProcessingData.activeOnStart)
			{
				editorPostProcessingOnStartActive = editorPostProcessingData;
			}
			editorCurrentActivePostProcessing = editorPostProcessingData;
		}, null, 0f, 1f);
		defineFloatProperty("Contrast", "Expands or shrinks the overall range of tonal values.", () => postProcessing.colorAdjustmentContrast, delegate(float value)
		{
			postProcessing.colorAdjustmentContrast = value;
			refresh.postProcessing = true;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getEditorPostProcessingData(propData).colorAdjustmentContrast = prevValue;
			undo.push(actionChangeProp(propData, propData2, "pp bloom Intensity"));
			refresh.postProcessing = true;
			EditorPostProcessingData editorPostProcessingData = getEditorPostProcessingData(propData2);
			if (editorPostProcessingData.activeOnStart)
			{
				editorPostProcessingOnStartActive = editorPostProcessingData;
			}
			editorCurrentActivePostProcessing = editorPostProcessingData;
		}, null, -10f, 100f);
		defineColorProperty("Color Filter", "The color filter that is applied to the image.\nThis allows you to change the overall color of the image.", () => postProcessing.colorAdjustmentColorFilter, delegate(Color color)
		{
			postProcessing.colorAdjustmentColorFilter = color;
			refresh.postProcessing = true;
		}, delegate(Color prevColor, Color nextColor)
		{
			var (propData, propData2) = prepChangeData(instance);
			getEditorPostProcessingData(propData).colorAdjustmentColorFilter = prevColor;
			undo.push(actionChangeProp(propData, propData2, "pp color adjustment Color Filter"));
			refresh.postProcessing = true;
			EditorPostProcessingData editorPostProcessingData = getEditorPostProcessingData(propData2);
			if (editorPostProcessingData.activeOnStart)
			{
				editorPostProcessingOnStartActive = editorPostProcessingData;
			}
			editorCurrentActivePostProcessing = editorPostProcessingData;
		});
		defineFloatProperty("Hue Shift", "Determines the amount of hue shift for all colors of the image.", () => postProcessing.colorAdjustmentHue, delegate(float value)
		{
			postProcessing.colorAdjustmentHue = value;
			refresh.postProcessing = true;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getEditorPostProcessingData(propData).colorAdjustmentHue = prevValue;
			undo.push(actionChangeProp(propData, propData2, "pp color adjustment Hue Shift"));
			refresh.postProcessing = true;
			EditorPostProcessingData editorPostProcessingData = getEditorPostProcessingData(propData2);
			if (editorPostProcessingData.activeOnStart)
			{
				editorPostProcessingOnStartActive = editorPostProcessingData;
			}
			editorCurrentActivePostProcessing = editorPostProcessingData;
		}, null, -180f, 180f);
		defineFloatProperty("Saturation", "Determines the intensity of all colors.", () => postProcessing.colorAdjustmentSaturation, delegate(float value)
		{
			postProcessing.colorAdjustmentSaturation = value;
			refresh.postProcessing = true;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getEditorPostProcessingData(propData).colorAdjustmentSaturation = prevValue;
			undo.push(actionChangeProp(propData, propData2, "pp color adjustment Saturation"));
			refresh.postProcessing = true;
			EditorPostProcessingData editorPostProcessingData = getEditorPostProcessingData(propData2);
			if (editorPostProcessingData.activeOnStart)
			{
				editorPostProcessingOnStartActive = editorPostProcessingData;
			}
			editorCurrentActivePostProcessing = editorPostProcessingData;
		}, null, -100f, 100f);
		defineFloatProperty("Chromatic Aberration", "Determines the intensity of the chromatic aberration effect.\nLarger values make the effect more intense.", () => postProcessing.chromaticAberrationIntensity, delegate(float value)
		{
			postProcessing.chromaticAberrationIntensity = value;
			refresh.postProcessing = true;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getEditorPostProcessingData(propData).chromaticAberrationIntensity = prevValue;
			undo.push(actionChangeProp(propData, propData2, "pp chromatic Aberration"));
			refresh.postProcessing = true;
			EditorPostProcessingData editorPostProcessingData = getEditorPostProcessingData(propData2);
			if (editorPostProcessingData.activeOnStart)
			{
				editorPostProcessingOnStartActive = editorPostProcessingData;
			}
			editorCurrentActivePostProcessing = editorPostProcessingData;
		}, null, 0f, 1f);
		defineFloatProperty("Vignette Intensity", "Determines the intensity of the vignette effect.\nLarger values make the vignette effect more intense.", () => postProcessing.vignetteIntensity, delegate(float value)
		{
			postProcessing.vignetteIntensity = value;
			refresh.postProcessing = true;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getEditorPostProcessingData(propData).vignetteIntensity = prevValue;
			undo.push(actionChangeProp(propData, propData2, "pp vignette Intensity"));
			refresh.postProcessing = true;
			EditorPostProcessingData editorPostProcessingData = getEditorPostProcessingData(propData2);
			if (editorPostProcessingData.activeOnStart)
			{
				editorPostProcessingOnStartActive = editorPostProcessingData;
			}
			editorCurrentActivePostProcessing = editorPostProcessingData;
		}, null, 0f, 1f);
		defineFloatProperty("Grain", "Determines the intensity of the film grain effect.\nLarger values make the grain effect more intense.", () => postProcessing.grainIntensity, delegate(float value)
		{
			postProcessing.grainIntensity = value;
			refresh.postProcessing = true;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getEditorPostProcessingData(propData).grainIntensity = prevValue;
			undo.push(actionChangeProp(propData, propData2, "pp grain Intensity"));
			refresh.postProcessing = true;
			EditorPostProcessingData editorPostProcessingData = getEditorPostProcessingData(propData2);
			if (editorPostProcessingData.activeOnStart)
			{
				editorPostProcessingOnStartActive = editorPostProcessingData;
			}
			editorCurrentActivePostProcessing = editorPostProcessingData;
		}, null, 0f, 1f);
		defineFloatProperty("Motion Blur", "Determines the intensity of the motion blur effect.\nLarger values make the motion blur effect more intense.", () => postProcessing.motionBlurIntensity, delegate(float value)
		{
			postProcessing.motionBlurIntensity = value;
			refresh.postProcessing = true;
		}, delegate(float prevValue, float nextValue)
		{
			var (propData, propData2) = prepChangeData(instance);
			getEditorPostProcessingData(propData).motionBlurIntensity = prevValue;
			undo.push(actionChangeProp(propData, propData2, "pp grain Intensity"));
			refresh.postProcessing = true;
			EditorPostProcessingData editorPostProcessingData = getEditorPostProcessingData(propData2);
			if (editorPostProcessingData.activeOnStart)
			{
				editorPostProcessingOnStartActive = editorPostProcessingData;
			}
			editorCurrentActivePostProcessing = editorPostProcessingData;
		}, null, 0f, 50f);
	}

	private void refreshDelayUI(PropInstance instance)
	{
		EditorDelay delay = instance.GetComponent<EditorDelay>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Delay", "Delays any action by fixed amount of time.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (cache.isInteractionShown)
		{
			defineTargetsProperty("Targets", "Targets to be triggered after the delay time has passed.", instance, (PropData propData) => getDelayData(propData).targets, defaultTargetPredicate(instance, delay.targets, withLock: false), delay.targets);
			defineFloatProperty("Delay Time", "Time in seconds to wait before triggering the targets.", () => delay.delay, delegate(float value)
			{
				delay.delay = value;
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getDelayData(propData).delay = prevValue;
				undo.push(actionChangeProp(propData, next, "Delay time changed"));
			}, null, 0f, float.PositiveInfinity, 0.1f);
		}
	}

	private void refreshDisplayUI(PropInstance instance)
	{
		EditorDisplay display = instance.GetComponent<EditorDisplay>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Display", "Displays current values of a Lock (each value maps to an image in a sprite sheet).", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (!cache.isInteractionShown)
		{
			return;
		}
		List<PropInstance> list = new List<PropInstance>();
		if (display.targetLock != null)
		{
			list.Add(display.targetLock.GetComponent<PropInstance>());
		}
		defineTargetsProperty("Target Lock", "During play the selected lock's current inputted password will be used for the display.\nDuring edit time the display shows the password value.", PrepChangeDataStrategy.UseTarget, delegate(PropData prev, PropData next, PropInstance value)
		{
			getEditorDisplayData(next).targetLock = value.ID;
		}, delegate(PropData prev, PropData next, int index)
		{
			getEditorDisplayData(next).targetLock = InstanceID.None;
		}, instance, targetLockPredicate, list, 1);
		defineIntProperty("Columns", "Amount of display columns to show.", () => display.columns, delegate(int value)
		{
			display.columns = value;
		}, delegate(int prevValue, int nextValue)
		{
			var (propData, next) = prepChangeData(instance);
			getEditorDisplayData(propData).columns = prevValue;
			undo.push(actionChangeProp(propData, next, "Editor columns"));
			refresh.editorDisplay = true;
		}, null, 1, int.MaxValue, 0.1f);
		defineIntProperty("Rows", "Amount of display rows to show.", () => display.rows, delegate(int value)
		{
			display.rows = value;
		}, delegate(int prevValue, int nextValue)
		{
			var (propData, next) = prepChangeData(instance);
			getEditorDisplayData(propData).rows = prevValue;
			undo.push(actionChangeProp(propData, next, "Editor rows"));
			refresh.editorDisplay = true;
		}, null, 1, int.MaxValue, 0.1f);
		defineVector2Property("Padding", "Amount of space to leave around a single display quad.", () => display.padding, delegate(Vector2 value)
		{
			display.padding = value;
		}, delegate(Vector2 prevValue, Vector2 nextValue)
		{
			var (propData, next) = prepChangeData(instance);
			getEditorDisplayData(propData).padding = prevValue;
			undo.push(actionChangeProp(propData, next, "Display padding changed"));
		}, delegate
		{
			refresh.editorDisplay = true;
		}, 0.1f);
		defineIntProperty("Sprite Sheet Columns", "Amount of sprites the selected sprite sheet has in one row.", () => display.spriteSheetColumns, delegate(int value)
		{
			display.spriteSheetColumns = value;
		}, delegate(int prevValue, int nextValue)
		{
			var (propData, next) = prepChangeData(instance);
			getEditorDisplayData(propData).spriteSheetColumns = prevValue;
			undo.push(actionChangeProp(propData, next, "Editor spriteSheetColumns"));
			refresh.editorDisplay = true;
		}, null, 1, int.MaxValue, 0.2f);
		defineIntProperty("Sprite Sheet Rows", "Amount of sprites the selected sprite sheet has in one column.", () => display.spriteSheetRows, delegate(int value)
		{
			display.spriteSheetRows = value;
		}, delegate(int prevValue, int nextValue)
		{
			var (propData, next) = prepChangeData(instance);
			getEditorDisplayData(propData).spriteSheetRows = prevValue;
			undo.push(actionChangeProp(propData, next, "Editor spriteSheetRows"));
			refresh.editorDisplay = true;
		}, null, 1, int.MaxValue, 0.2f);
		Texture2D spriteSheetTexture = display.spriteSheetTexture;
		if (spriteSheetTexture == null)
		{
			_ = assets.skyboxes[0];
			getEditorDisplayData(prepChangeData(instance).next).spriteSheetFileName = "";
		}
		defineSkyTextureProperty("%RE_spriteSheet%", "%RE_spriteSheetTooltip%", spriteSheetTexture, delegate
		{
			openAssetBrowserPopup<TextureAsset>(delegate(UserAsset texture)
			{
				var (prev, propData) = prepChangeData(instance);
				getEditorDisplayData(propData).spriteSheetFileName = texture.nameWithExtension;
				undo.push(actionChangeProp(prev, propData, "Display sprite sheet", refreshVolume: true));
			});
		}, (display.spriteSheetFileName == "default") ? null : ((Action)delegate
		{
			var (prev, propData) = prepChangeData(instance);
			getEditorDisplayData(propData).spriteSheetFileName = "default";
			undo.push(actionChangeProp(prev, propData, "Display sprite sheet revert", refreshVolume: true));
		}));
	}

	private void refreshPuzzleUI(PropInstance instance)
	{
		EditorPuzzle puzzle = instance.GetComponent<EditorPuzzle>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Puzzle", "A prop that defines a puzzle in the room.\nUsed to generate Walkthroughes for your players.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (!cache.isInteractionShown)
		{
			return;
		}
		defineStringProperty("Puzzle Name", "The name players will see displayed when they complete the puzzle.\nKeep this empty if you don't want to display a text message.", () => puzzle.puzzleName, delegate(string value)
		{
			puzzle.puzzleName = value;
		}, delegate(string prevValue, string nextValue)
		{
			var (propData, next) = prepChangeData(instance);
			getPuzzleData(propData).puzzleName = prevValue;
			undo.push(actionChangeProp(propData, next, "Puzzle - change puzzle name"));
		});
		defineBoolProperty("Mute Sound", "If not checked the puzzle will play the default puzzle completed sound when triggered.", puzzle.muteSound, delegate(bool value)
		{
			var (prev, propData) = prepChangeData(instance);
			getPuzzleData(propData).muteSound = value;
			undo.push(actionChangeProp(prev, propData, "Puzzle - mute sound"));
		});
		List<string> types = getWalkthroughHeaderTitles(publishingRoomInfo.roomWalkthrough);
		List<int> initValues = new List<int>(puzzle.hints);
		defineMultiEnumPropertyPropertiesUI("%puzzle_hints%", "%puzzle_hintsTooltip%", initValues, types, translateOptions: false, delegate(int index, int value)
		{
			(PropData prev, PropData next) tuple = prepChangeData(instance);
			PropData item = tuple.prev;
			PropData item2 = tuple.next;
			List<int> hints = getPuzzleData(item2).hints;
			if (index >= 0 && index < hints.Count)
			{
				if (value == -1)
				{
					hints.RemoveAt(index);
				}
				else
				{
					hints[index] = value;
				}
				undo.push(actionChangeProp(item, item2, "Puzzle - hints changed"));
				refresh.propertiesUI = true;
			}
		}, delegate
		{
			(PropData prev, PropData next) tuple = prepChangeData(instance);
			PropData item = tuple.prev;
			PropData item2 = tuple.next;
			for (int i = 0; i < types.Count; i++)
			{
				if (!initValues.Contains(i))
				{
					getPuzzleData(item2).hints.Add(i);
					break;
				}
			}
			undo.push(actionChangeProp(item, item2, "Puzzle - added show hint"));
			refresh.propertiesUI = true;
		});
		List<TargetArrayEntry> list = new List<TargetArrayEntry>(puzzle.conditions.Count);
		for (int num = 0; num < puzzle.conditions.Count; num++)
		{
			list.Add(getTargetArrayEntry(puzzle.conditions[num].gameObject));
		}
		defineTargetArrayProperty("%puzzle_conditions%", "%puzzle_conditionsTooltip%", list, -1, delegate
		{
			enterMultiTargetMode(targetPuzzlePredicate(puzzle.conditions), delegate(HashSet<PropInstance> pis, HashSet<(PropInstance, int)> _)
			{
				var (prev, propData) = prepChangeData(instance);
				foreach (PropInstance pi in pis)
				{
					getPuzzleData(propData).conditions.Add(pi.ID);
				}
				undo.push(actionChangeProp(prev, propData, "Add Puzzle conditions"));
			});
		}, delegate(int index)
		{
			GameObject gameObject = null;
			if (index < puzzle.conditions.Count)
			{
				gameObject = puzzle.conditions[index].gameObject;
			}
			if (gameObject != null && gameObject.TryGetComponent<PropInstance>(out var component))
			{
				undo.push(actionChangeSelection(newInstanceSelection(component.ID)));
				moveCameraTo(gameObject);
			}
		}, delegate(int index)
		{
			var (prev, propData) = prepChangeData(instance);
			getPuzzleData(propData).conditions.RemoveAt(index);
			undo.push(actionChangeProp(prev, propData, "Puzzle Remove Target"));
		});
		static List<string> getWalkthroughHeaderTitles(List<string> walkthroughSteps)
		{
			List<string> list2 = new List<string>();
			string text = CustomWalkthroughHandler.headerPrefix + CustomWalkthroughHandler.separator;
			foreach (string walkthroughStep in walkthroughSteps)
			{
				if (walkthroughStep.StartsWith(text))
				{
					list2.Add(walkthroughStep.Remove(0, text.Length));
				}
			}
			return list2;
		}
		Predicate<PropInstance> targetPuzzlePredicate(IEnumerable<GameObject> ignoreTargets)
		{
			return delegate(PropInstance propInstance)
			{
				if (propInstance == instance)
				{
					return false;
				}
				if (!propInstance.TryGetComponent<EditorPuzzle>(out var _))
				{
					return false;
				}
				foreach (GameObject ignoreTarget in ignoreTargets)
				{
					if (ignoreTarget.gameObject == propInstance.gameObject)
					{
						return false;
					}
				}
				return true;
			};
		}
	}

	private void refreshSetupUI(PropInstance instance)
	{
		EditorSetup component = instance.GetComponent<EditorSetup>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Setup", "Allows you to trigger any action at the start of the room.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (cache.isInteractionShown)
		{
			defineTargetsProperty("Targets", "Targets to trigger at the start of the room.", instance, (PropData propData) => getEditorSetupData(propData).targets, defaultTargetPredicate(instance, component.targets, withLock: false), component.targets);
			defineHintProperty("This prop is used to initialize your room.\nTargets will be triggered only once at start.");
		}
	}

	private void refreshLockUI(PropInstance instance)
	{
		Lock component = instance.GetComponent<Lock>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Lock", "Logic prop used to calculate a true or false value based on the current input and settings.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (!cache.isInteractionShown)
		{
			return;
		}
		defineEnumProperty("Input Mode", "The way the lock reads the input values.", (int)component.lockType, new List<string> { "Separate", "Sequence", "Resetting Sequence" }, delegate(int value)
		{
			var (prev, propData) = prepChangeData(instance);
			getLockData(propData).type = (LockType)value;
			undo.push(actionChangeProp(prev, propData, "Lock type"));
		}, useInterpolationPopup: false, new List<string> { "All values must be correct. You can change any value at any time.", "Reads inputs in order.", "Reads inputs in order; resets after the full sequence if incorrect." });
		int[] password = component.password;
		definePasswordProperty("%RE_lockPassword%", "%RE_lockPasswordTooltip%", password, delegate(int value)
		{
			var (prev, propData) = prepChangeData(instance);
			getLockData(propData).password.Add(value);
			undo.push(actionChangeProp(prev, propData, "Lock add password entry"));
			refresh.editorDisplay = true;
		}, delegate(int index)
		{
			List<EditorAction> list = new List<EditorAction>();
			actionsRemoveFromTargeters(list, instance, index);
			actionsRemoveFromTargeters(list, instance, index, targetIndexReduce: true);
			var (prev, propData) = prepChangeData(instance);
			getLockData(propData).password.RemoveAt(index);
			list.Add(actionChangeProp(prev, propData, "Lock remove password entry"));
			undo.push(list.ToArray());
			refresh.editorDisplay = true;
		}, delegate(int index, int value)
		{
			var (prev, propData) = prepChangeData(instance);
			getLockData(propData).password[index] = value;
			undo.push(actionChangeProp(prev, propData, "Lock change password entry"));
			refresh.editorDisplay = true;
		});
		defineRoomEditorLinksProperty("Unlock Output value", "Number that will be sent to the targeted props on correct completion.", "%RE_onUnlock%", "%RE_lockOnUnlock%", component.onUnlock, instance, (PropData x) => getLockData(x).onUnlock, defaultTargetPredicate(instance, component.onUnlock.targets, withLock: true));
		defineRoomEditorLinksProperty("Lock Output value", "Number that will be sent to the targeted props on correct completion.", "%RE_onLock%", "%RE_lockOnLock%", component.onLock, instance, (PropData x) => getLockData(x).onLock, defaultTargetPredicate(instance, component.onLock.targets, withLock: true));
		defineFoldoutProperty("Extras", null, cache.isInteractionExtraShown, delegate(bool isShown)
		{
			cache.isInteractionExtraShown = isShown;
		});
		if (cache.isInteractionExtraShown)
		{
			defineEnumProperty("Logic Type", "The way password values are evaluated.", (int)component.lockLogicType, new List<string> { "AND", "OR", "Parity XOR", "Exclusive XOR" }, delegate(int value)
			{
				var (prev, propData) = prepChangeData(instance);
				getLockData(propData).logicType = (LockLogicType)value;
				undo.push(actionChangeProp(prev, propData, "Lock logic type"));
			}, useInterpolationPopup: false, new List<string> { "All password values must be inputted correctly.", "One or more of the password values must be correct.", "There must be an odd number of correct values.", "There must be only one correct value." });
			defineBoolProperty("Negate", "When checked, the logic will be reversed.\nAny password that is not correct will unlock the lock, and every correct lock will lock the lock.", component.lockNegateValue, delegate(bool value)
			{
				var (prev, propData) = prepChangeData(instance);
				getLockData(propData).negateValue = value;
				undo.push(actionChangeProp(prev, propData, "Lock lockNegateValue"));
			});
			defineBoolProperty("Disable On Unlock", "When checked, the lock is disabled after it is unlocked once, ignoring any further inputs.", component.deactivateLockOnUnlock, delegate(bool value)
			{
				var (prev, propData) = prepChangeData(instance);
				getLockData(propData).disableLockOnUnlock = value;
				undo.push(actionChangeProp(prev, propData, "Lock disableLockOnUnlock"));
			});
			defineBoolProperty("Exit Zoom On Unlock", "When checked this lock triggers an exit from a parent 'Zoomable', when unlocked,\nif a player is at that moment zoomed in on the 'Zoomable' parent.", component.exitZoomOnUnlock, delegate(bool value)
			{
				var (prev, propData) = prepChangeData(instance);
				getLockData(propData).exitZoomOnUnlock = value;
				undo.push(actionChangeProp(prev, propData, "Lock exitZoomOnUnlock"));
			});
		}
	}

	private void refreshSpawnPointUI(PropInstance instance)
	{
		SpawnPoint spawnPoint = instance.GetComponent<SpawnPoint>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Spawn Point", "Defines a point where a player will spawn when starting this room.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (cache.isInteractionShown)
		{
			defineFloatProperty("Walking Speed", "Player walking speed to set when player is spawned on this spawn point.\nTo reset it, teleport the player to another point.", () => spawnPoint.walkingSpeed, delegate(float value)
			{
				spawnPoint.walkingSpeed = value;
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getSpawnPointData(propData).walkingSpeed = prevValue;
				undo.push(actionChangeProp(propData, next, "Spawn Point Walking Speed"));
			}, null, 0f, float.PositiveInfinity, 0.01f);
			defineFloatProperty("Running Speed", "Player running speed to set when player is spawned on this spawn point.\nTo reset it, teleport the player to another point.", () => spawnPoint.runningSpeed, delegate(float value)
			{
				spawnPoint.runningSpeed = value;
			}, delegate(float prevValue, float nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getSpawnPointData(propData).runningSpeed = prevValue;
				undo.push(actionChangeProp(propData, next, "Spawn Point Running Speed"));
			}, null, 0f, float.PositiveInfinity, 0.01f);
		}
	}

	private void refreshSlotUI(PropInstance instance)
	{
		Slot slot = instance.GetComponent<Slot>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Slot", "Logic prop used to create areas that accept certain keys as inputs for unlocking.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		destroySlotPreview(instance);
		createSlotPreview(instance);
		if (!cache.isInteractionShown)
		{
			return;
		}
		List<PropInstance> list = new List<PropInstance>();
		List<PropInstance> list2 = new List<PropInstance>();
		Item[] acceptItems = slot.acceptItems;
		foreach (Item item in acceptItems)
		{
			if (!(item == null))
			{
				list.Add(item.GetComponent<PropInstance>());
			}
		}
		acceptItems = slot.rejectItems;
		foreach (Item item2 in acceptItems)
		{
			if (!(item2 == null))
			{
				list2.Add(item2.GetComponent<PropInstance>());
			}
		}
		defineTargetsProperty("Keys", "%RE_slotKeysTooltip%", PrepChangeDataStrategy.UseTarget, delegate(PropData prev, PropData next, PropInstance value)
		{
			getSlotData(next).keys.Add(value.ID);
		}, delegate(PropData prev, PropData next, int index)
		{
			getSlotData(next).keys.RemoveAt(index);
		}, instance, targetKeyPredicate, list, -1);
		defineTargetsProperty("%RE_rejectKeys%", "%RE_rejectKeysTooltip%", PrepChangeDataStrategy.UseTarget, delegate(PropData prev, PropData next, PropInstance value)
		{
			getSlotData(next).rejectKeys.Add(value.ID);
		}, delegate(PropData prev, PropData next, int index)
		{
			getSlotData(next).rejectKeys.RemoveAt(index);
		}, instance, targetKeyPredicate, list2, -1);
		if (slot.acceptItems.Length != 0 || slot.rejectItems.Length != 0)
		{
			TransformData transformData = saveTransform(slot.pivot, isLocal: true);
			TransformData originalPivot = instance.originalPivot;
			bool modified = !vec3ApproxEqual(transformData.position, originalPivot.position) || !vec3ApproxEqual(transformData.rotation, originalPivot.rotation);
			defineTargetProperty("%RE_editKeyPlacement%", "%RE_editKeyPlacementTooltip%", modified, delegate
			{
				toggleSlotPivotMode(show: true);
			}, null);
		}
		List<PropInstance> list3 = new List<PropInstance>();
		if (slot.initialInsertedItem != null)
		{
			list3.Add(slot.initialInsertedItem.GetComponent<PropInstance>());
		}
		defineTargetsProperty("Initial Key", "If set, selected item will be placed in this slot when the room is loaded.", PrepChangeDataStrategy.UseTarget, delegate(PropData _, PropData next, PropInstance value)
		{
			getSlotData(next).initialItemID = value.ID;
		}, delegate(PropData _, PropData next, int _)
		{
			getSlotData(next).initialItemID = InstanceID.None;
		}, instance, (PropInstance pi) => Array.Find(slot.acceptItems, (Item x) => x.gameObject == pi.gameObject), list3, 1);
		List<string> optionsTooltips = new List<string> { "The key is parented to the slot, but can be removed.", "Ejects the key from the slot into the player inventory.", "The key becomes a static part of the slot.", "Ejects the key from the slot into the world." };
		defineEnumProperty("Unlock Success Key", "The way the key is handled when a correct key is placed in the slot.", (int)slot.onAcceptItemPlaced, new List<string> { "Stays In Slot", "Eject To Inventory", "Merge With Slot", "Eject To World" }, delegate(int value)
		{
			var (prev, propData) = prepChangeData(instance);
			getSlotData(propData).unlockSuccessKey = (SlotPlaceBehaviour)value;
			undo.push(actionChangeProp(prev, propData, "Unlock success key"));
		}, useInterpolationPopup: false, optionsTooltips);
		defineEnumProperty("Unlock Failed Key", "The way the key is handled when an incorrect key is placed in the slot.", (int)slot.onRejectItemPlaced, new List<string> { "Stays In Slot", "Eject To Inventory", "Merge With Slot", "Eject To World" }, delegate(int value)
		{
			var (prev, propData) = prepChangeData(instance);
			getSlotData(propData).unlockFailedKey = (SlotPlaceBehaviour)value;
			undo.push(actionChangeProp(prev, propData, "Unlock failed key"));
		}, useInterpolationPopup: false, optionsTooltips);
		SlotPlaceBehaviour onAcceptItemPlaced = slot.onAcceptItemPlaced;
		if (onAcceptItemPlaced != SlotPlaceBehaviour.ItemEjectsToInventory && onAcceptItemPlaced != SlotPlaceBehaviour.ItemEjectsToWorld)
		{
			onAcceptItemPlaced = slot.onRejectItemPlaced;
			if (onAcceptItemPlaced != SlotPlaceBehaviour.ItemEjectsToInventory && onAcceptItemPlaced != SlotPlaceBehaviour.ItemEjectsToWorld)
			{
				goto IL_044f;
			}
		}
		defineVector3Property("Eject Direction", "Direction in which the key will be ejected from the slot.", () => slot.ejectDirection, delegate(Vector3 value)
		{
			slot.ejectDirection = value;
		}, delegate(Vector3 prevValue, Vector3 nextValue)
		{
			var (propData, next) = prepChangeData(instance);
			getSlotData(propData).ejectDirection = prevValue;
			undo.push(actionChangeProp(propData, next, "Slot Eject Direction"));
		}, null, 0.01f);
		goto IL_044f;
		IL_044f:
		List<string> options = new List<string> { "Rotate Key", "None" };
		defineEnumProperty("Animation Type", "Defines the animation that will be played when a key is placed in the slot.", (int)slot.animationType, options, delegate(int value)
		{
			(PropData prev, PropData next) tuple = prepChangeData(instance);
			PropData item3 = tuple.prev;
			PropData item4 = tuple.next;
			SlotData slotData = getSlotData(item4);
			slotData.animationType = (SlotAnimationType)value;
			slotData.turnDuration = ((options[value] == "None") ? 0f : 0.5f);
			undo.push(actionChangeProp(item3, item4, "Slot animation type, Turn Duration"));
		}, useInterpolationPopup: false, new List<string> { "Rotate the key when placed.", "No animation." });
		if (slot.animationType != SlotAnimationType.None)
		{
			defineVector3Property("Turn Axis", "Set the axis around which the turn animation will rotate around.\nIf all the values are set to zero the key item will not rotate.", () => slot.rotateKeyTurnAxis, delegate(Vector3 value)
			{
				slot.rotateKeyTurnAxis = value;
			}, delegate(Vector3 prevValue, Vector3 nextValue)
			{
				var (propData, next) = prepChangeData(instance);
				getSlotData(propData).turnAxis = prevValue;
				undo.push(actionChangeProp(propData, next, "Slot Turn Axis"));
			}, null, 0.01f);
			if (slot.rotateKeyTurnAxis != Vector3.zero)
			{
				defineFloatProperty("Turn Duration", "Determines how long to turn the inserted key before executing the end actions.", () => slot.rotateKeyTurnDuration, delegate(float value)
				{
					slot.rotateKeyTurnDuration = value;
				}, delegate(float prevValue, float nextValue)
				{
					var (propData, next) = prepChangeData(instance);
					getSlotData(propData).turnDuration = prevValue;
					undo.push(actionChangeProp(propData, next, "Slot Turn Duration"));
				}, null, 0f, float.PositiveInfinity, 0.01f);
				defineFloatProperty("Turn Count", "Determines the amount of turns the inserted key will do in the Turn Duration time given.", () => slot.rotateKeyTurnCount, delegate(float value)
				{
					slot.rotateKeyTurnCount = value;
				}, delegate(float prevValue, float nextValue)
				{
					var (propData, next) = prepChangeData(instance);
					getSlotData(propData).turnCount = prevValue;
					undo.push(actionChangeProp(propData, next, "Slot Turn Count"));
				}, null, 0f, float.PositiveInfinity, 0.01f);
			}
		}
		defineRoomEditorLinksProperty("Output value", "Number that will be sent to the targeted props on correct completion.", "%RE_onPlace%", "%RE_slotOnPlaceTooltip%", slot.onPlace, instance, (PropData x) => getSlotData(x).onPlace, defaultTargetPredicate(instance, slot.onPlace.targets, withLock: true));
		defineRoomEditorLinksProperty("Output value", "Number that will be sent to the targeted props on correct completion.", "%RE_onRemove%", "%RE_onRemoveTooltip%", slot.onRemove, instance, (PropData x) => getSlotData(x).onRemove, defaultTargetPredicate(instance, slot.onRemove.targets, withLock: true));
		bool targetKeyPredicate(PropInstance pi)
		{
			if (pi != instance && pi.TryGetComponent<Item>(out var component) && !component.carriable && !Array.Exists(slot.acceptItems, (Item i) => i != null && i.gameObject == pi.gameObject))
			{
				return !Array.Exists(slot.rejectItems, (Item i) => i != null && i.gameObject == pi.gameObject);
			}
			return false;
		}
		static bool vec3ApproxEqual(Vector3 v1, Vector3 v2)
		{
			if (Mathf.Approximately(v1.x, v2.x) && Mathf.Approximately(v1.y, v2.y))
			{
				return Mathf.Approximately(v1.z, v2.z);
			}
			return false;
		}
	}

	private void refreshTriggerUI(PropInstance instance)
	{
		Trigger component = instance.GetComponent<Trigger>();
		InstanceUICache cache = getInstanceUICache(instance);
		defineHeaderProperty("Trigger", "Logic prop used to determine when another prop or player enters/exits an area.", cache.isInteractionShown, delegate(bool isShown)
		{
			cache.isInteractionShown = isShown;
		});
		if (!cache.isInteractionShown)
		{
			return;
		}
		defineBoolProperty("Triggers By All Items", "If true, any object in this trigger will activate it.", component.anyObjectCanTrigger, delegate(bool value)
		{
			var (prev, propData) = prepChangeData(instance);
			getTriggerData(propData).anyObjectCanTrigger = value;
			undo.push(actionChangeProp(prev, propData, "Trigger anyObjectCanTrigger"));
		});
		if (!component.anyObjectCanTrigger)
		{
			defineTargetsProperty("Triggers By Items", "%RE_triggerTriggersByTooltip%", instance, (PropData x) => getTriggerData(x).keys, targetPicker(component.keys), component.keys);
		}
		defineBoolProperty("Triggers By Player", "When checked, players that enter the trigger area can trigger it.\nWhen not checked, the trigger ignores players entering the trigger area.", component.canPlayerTrigger, delegate(bool value)
		{
			(PropData prev, PropData next) tuple = prepChangeData(instance);
			PropData item = tuple.prev;
			PropData item2 = tuple.next;
			TriggerData triggerData = getTriggerData(item2);
			triggerData.canPlayerTrigger = value;
			if (!value)
			{
				triggerData.triggerWhenAllPlayersEnter = false;
			}
			undo.push(actionChangeProp(item, item2, "Trigger enter canPlayerTrigger"));
		});
		if (component.canPlayerTrigger)
		{
			defineBoolProperty("Requires All Players", "When checked, the trigger will activate once all the players are in the trigger area.\nWhen not checked, the trigger will activate when only one player enters the trigger area.", component.triggerWhenAllPlayersEnter, delegate(bool value)
			{
				var (prev, propData) = prepChangeData(instance);
				getTriggerData(propData).triggerWhenAllPlayersEnter = value;
				undo.push(actionChangeProp(prev, propData, "Trigger toggle triggerWhenAllPlayersEnter"));
			});
		}
		defineBoolProperty("Is Sticky", "When checked, the trigger is disabled after it is triggered once and can no longer be triggered.", component.isSticky, delegate(bool value)
		{
			var (prev, propData) = prepChangeData(instance);
			getTriggerData(propData).isSticky = value;
			undo.push(actionChangeProp(prev, propData, "Trigger isSticky"));
		});
		defineSeparatorProperty(2f, isEmpty: false);
		defineRoomEditorLinksProperty("Output value", "Number that will be sent to the targeted props on correct completion.", "On Start", "Prop actions that are triggered when the trigger was empty but now has at least one triggering object or player INSIDE the 'Trigger' collision box.", component.startData, instance, (PropData x) => getTriggerData(x).onStart, defaultTargetPredicate(instance, component.startData.targets, withLock: true));
		defineRoomEditorLinksProperty("Output value", "Number that will be sent to the targeted props on correct completion.", "On Enter", "Prop actions that are triggered when the triggering objects or players ENTER the 'Trigger' collision box.", component.enterData, instance, (PropData x) => getTriggerData(x).onEnter, defaultTargetPredicate(instance, component.enterData.targets, withLock: true));
		defineRoomEditorLinksProperty("Output value", "Number that will be sent to the targeted props on correct completion.", "On Exit", "Prop actions that are triggered when the triggering objects or players EXIT the 'Trigger' collision box.", component.exitData, instance, (PropData x) => getTriggerData(x).onExit, defaultTargetPredicate(instance, component.exitData.targets, withLock: true));
		defineRoomEditorLinksProperty("Output value", "Number that will be sent to the targeted props on correct completion.", "On End", "Prop actions that are triggered when triggering objects or players leave the 'Trigger' collision box and it becomes empty.", component.endData, instance, (PropData x) => getTriggerData(x).onEnd, defaultTargetPredicate(instance, component.endData.targets, withLock: true));
		Predicate<PropInstance> targetPicker(List<GameObject> ignoreList)
		{
			return delegate(PropInstance pi)
			{
				if (pi == instance)
				{
					return false;
				}
				if (ignoreList.Exists((GameObject x) => x == pi))
				{
					return false;
				}
				Item component2;
				Draggable component3;
				return pi.TryGetComponent<Item>(out component2) || pi.TryGetComponent<Draggable>(out component3);
			};
		}
	}

	private void refreshSoundPickerUI()
	{
		closeSoundPickerUI();
		soundPickerUI.root.enabled = true;
		bool presetsTab = soundPickerUI.Header_Tabs_Presets.isOn;
		soundPickerUI.AddNewSound.gameObject.SetActive(!presetsTab);
		PropInstance instance = getSelectedInstance();
		Sound sound = instance.GetComponent<Sound>();
		soundPickerUI.AddNewSound.root.onClick.RemoveAllListeners();
		soundPickerUI.AddNewSound.root.onClick.AddListener(delegate
		{
			openFileBrowserForType(typeof(AudioAsset));
		});
		soundPickerUI.Header_Tabs_Presets.onValueChanged.RemoveAllListeners();
		soundPickerUI.Header_Tabs_Presets.onValueChanged.AddListener(delegate
		{
			refreshSoundPickerUI();
		});
		soundPickerUI.Header_Tabs_Custom.onValueChanged.RemoveAllListeners();
		soundPickerUI.Header_Tabs_Custom.onValueChanged.AddListener(delegate
		{
			refreshSoundPickerUI();
		});
		soundPickerUI.Header_Options_Apply.onClick.RemoveAllListeners();
		soundPickerUI.Header_Options_Apply.onClick.AddListener(delegate
		{
			if (presetsTab)
			{
				(PropData prev, PropData next) tuple = prepChangeData(instance);
				PropData item = tuple.prev;
				PropData item2 = tuple.next;
				EventDescription fmodEvent2 = soundPicker.presetSelection.fmodEvent;
				fmodEvent2.getPath(out var path3);
				fmodEvent2.is3D(out var is3D2);
				fmodEvent2.isOneshot(out var oneshot);
				SoundData soundData = getSoundData(item2);
				soundData.path = "";
				soundData._event = path3;
				soundData.type = ((!path3.StartsWith("event:/ROOM EDITOR/Music")) ? Sound.SoundType.SoundEffect : Sound.SoundType.Music);
				soundData.isLoopable = !oneshot;
				soundData.is2D = !is3D2;
				undo.push(actionChangeProp(item, item2, "Sound path"));
			}
			else
			{
				string selectedFile = soundPicker.selectedFile;
				if (selectedFile != sound.path)
				{
					var (prev, propData) = prepChangeData(instance);
					getSoundData(propData).path = selectedFile;
					getSoundData(propData)._event = "";
					undo.push(actionChangeProp(prev, propData, "Sound path"));
				}
			}
			closeSoundPickerUI();
		});
		if (presetsTab)
		{
			if (soundPicker.presetsRoot == null)
			{
				soundPicker.presetsRoot = new SoundLibNode
				{
					name = "Root",
					children = new List<SoundLibNode>()
				};
			}
			if (PineFmod.getAudioPresetsBank().getEventList(out var array) == RESULT.OK)
			{
				for (int num = 0; num < array.Length; num++)
				{
					if (array[num].getPath(out var path) != RESULT.OK || !path.StartsWith("event:/Room Editor"))
					{
						continue;
					}
					string[] array2 = path.Split('/');
					SoundLibNode soundLibNode = soundPicker.presetsRoot;
					for (int num2 = 1; num2 < array2.Length; num2++)
					{
						string token = array2[num2];
						token = typoFix(token);
						SoundLibNode soundLibNode2 = soundLibNode.children.Find((SoundLibNode x) => x.name == token);
						if (soundLibNode2 != null)
						{
							soundLibNode = soundLibNode2;
							continue;
						}
						SoundLibNode soundLibNode3 = new SoundLibNode
						{
							name = token,
							children = new List<SoundLibNode>()
						};
						soundLibNode.children.Add(soundLibNode3);
						soundLibNode = soundLibNode3;
					}
					soundLibNode.fmodEvent = array[num];
				}
			}
			iterateSort(soundPicker.presetsRoot);
			iterateDisplay(soundPicker.presetsRoot, "", -2);
		}
		else
		{
			string[] files = Directory.GetFiles(roomDirPath);
			Array.Sort(files);
			soundPicker.files = new List<string>();
			foreach (string path2 in files)
			{
				string fileName = Path.GetFileName(path2);
				if (supportedAudioAssetFormats.Exists((string audioFormat) => fileName.EndsWith(audioFormat)))
				{
					soundPicker.files.Add(fileName);
					_ = soundPicker.files.Count;
					PickSoundItemUI pickSoundItemUI = UnityEngine.Object.Instantiate(soundPickerUI.PickSoundItem, soundPickerUI.Content);
					pickSoundItemUI.gameObject.SetActive(value: true);
					pickSoundItemUI.Selection.enabled = fileName == soundPicker.selectedFile;
					pickSoundItemUI.Title.text = fileName;
					pickSoundItemUI.root.onClick.AddListener(delegate
					{
						soundPicker.selectedFile = fileName;
						refreshSoundPickerUI();
					});
				}
			}
		}
		bool flag = (presetsTab && soundPicker.presetSelection != null && soundPicker.presetSelection.children.Count == 0) || (!presetsTab && soundPicker.files.Count > 0 && !string.IsNullOrEmpty(soundPicker.selectedFile));
		soundPickerUI.Header_Options_Apply.interactable = flag;
		if (!flag)
		{
			return;
		}
		if (presetsTab)
		{
			EventDescription fmodEvent = soundPicker.presetSelection.fmodEvent;
			if (PineFmod.createInstance(fmodEvent, out var instance2) == RESULT.OK)
			{
				if (fmodEvent.is3D(out var is3D) == RESULT.OK && is3D)
				{
					PineFmod.attachInstanceToGameObject(instance2, mainCam.transform, null);
				}
				PineFmod.start(instance2);
				soundPicker.playingInstance = instance2;
			}
		}
		else
		{
			playSoundPickerSound(soundPicker.selectedFile);
		}
		void iterateDisplay(SoundLibNode node, string text, int depth)
		{
			bool flag2 = node.name == "Root";
			bool flag3 = node.name == "Room Editor";
			if (!flag2)
			{
				text = text + "/" + node.name;
			}
			if (!flag2 && !flag3)
			{
				PickSoundItemUI pickSoundItemUI2 = UnityEngine.Object.Instantiate(soundPickerUI.PickSoundItem, soundPickerUI.Content);
				pickSoundItemUI2.gameObject.SetActive(value: true);
				pickSoundItemUI2.Selection.enabled = soundPicker.presetSelection == node;
				bool flag4 = node.children.Count > 0;
				pickSoundItemUI2.SoundIcon.gameObject.SetActive(!flag4);
				pickSoundItemUI2.FolderIcon.gameObject.SetActive(flag4);
				string text2 = node.name;
				if (!flag4)
				{
					text2 = ((node.fmodEvent.isOneshot(out var oneshot) == RESULT.OK && oneshot) ? (text2 + " (Oneshot)") : ((!(node.fmodEvent.is3D(out var is3D2) == RESULT.OK && is3D2)) ? (text2 + " (2D)") : (text2 + " (3D)")));
				}
				pickSoundItemUI2.Title.text = text2;
				for (int i = 0; i < Mathf.Min(pickSoundItemUI2.Tab_List.Length, depth); i++)
				{
					pickSoundItemUI2.Tab_List[i].gameObject.SetActive(value: true);
				}
				pickSoundItemUI2.root.onClick.AddListener(delegate
				{
					soundPicker.presetSelection = node;
					node.unfolded = !node.unfolded;
					refreshSoundPickerUI();
				});
			}
			if (node.unfolded || flag2 || flag3)
			{
				for (int num4 = 0; num4 < node.children.Count; num4++)
				{
					iterateDisplay(node.children[num4], text, depth + 1);
				}
			}
		}
		static void iterateSort(SoundLibNode node)
		{
			node.children.Sort((SoundLibNode x, SoundLibNode y) => x.name.CompareTo(y.name));
			for (int num4 = 0; num4 < node.children.Count; num4++)
			{
				iterateSort(node.children[num4]);
			}
		}
		static string typoFix(string text)
		{
			List<string> obj = new List<string> { "Scarry Music 01", "Scarry Music 02", "Scarry Music 03", "Scarry Music 04", "Scarry Music 05" };
			List<string> list = new List<string> { "Scary Music 01", "Scary Music 02", "Scary Music 03", "Scary Music 04", "Scary Music 05" };
			int num4 = obj.IndexOf(text);
			if (num4 >= 0 && num4 < list.Count)
			{
				return list[num4];
			}
			return text;
		}
	}

	private InstanceUICache getInstanceUICache(PropInstance instance)
	{
		if (uiCache.TryGetValue(instance.ID, out var value))
		{
			return value;
		}
		value = new InstanceUICache();
		uiCache[instance.ID] = value;
		return value;
	}

	private void defineMultiEnumPropertyPropertiesUI(string label, string labelTooltip, List<int> initValues, List<string> options, bool translateOptions, Action<int, int> onChange, Action<MultiEnumPropertyUI> onButtonClick)
	{
		defineMultiEnumProperty(label, labelTooltip, initValues, options, translateOptions, onChange, onButtonClick, propertiesUI.MultiEnumProperty, propertiesUI.Content);
	}

	private void defineMultiEnumProperty(string label, string tooltip, List<int> initValues, List<string> options, bool translateOptions, Action<int, int> onChange, Action<MultiEnumPropertyUI> onButtonClick, MultiEnumPropertyUI template, Transform parent)
	{
		label = tryGetLocalized(label);
		tooltip = tryGetLocalized(tooltip);
		MultiEnumPropertyUI ui = UnityEngine.Object.Instantiate(template, parent);
		ui.gameObject.SetActive(value: true);
		ui.Label_Text.text = Localization.translate(label);
		setHoverTooltip(tooltip, ui.Label, UITooltip.Position.Left);
		registerProperty(ui, null, null);
		ui.Content_Dropdown.ClearOptions();
		ui.Content_Dropdown.onValueChanged.RemoveAllListeners();
		for (int i = 0; i < initValues.Count; i++)
		{
			int value = initValues[i] + 1;
			Dropdown dropdown = UnityEngine.Object.Instantiate(ui.Content_Dropdown, ui.Content_Dropdown.transform.parent);
			if (translateOptions)
			{
				UnityEngine.Object.DestroyImmediate(dropdown.GetComponent<DontTranslate>());
			}
			List<int> list = new List<int>();
			for (int j = -1; j < options.Count; j++)
			{
				string text = ((j > -1) ? options[j] : Localization.lookupInDictionary("multiEnumDelete"));
				dropdown.options.Add(new Dropdown.OptionData
				{
					text = (translateOptions ? Localization.translate(text) : text)
				});
				if (initValues.Contains(j))
				{
					list.Add(j + 1);
				}
			}
			if (translateOptions)
			{
				Localization.translateObject(dropdown.transform, forceNewText: true);
			}
			dropdown.GetComponent<DropdownController>().indexesToDisable = list;
			dropdown.value = value;
			int index = i;
			dropdown.onValueChanged.AddListener(delegate(int num)
			{
				onChange(index, num - 1);
				LayoutRebuilder.ForceRebuildLayoutImmediate(ui.Content_Dropdown.transform.parent.GetComponent<RectTransform>());
				LayoutRebuilder.ForceRebuildLayoutImmediate(parent.GetComponent<RectTransform>());
			});
		}
		ui.Content_Dropdown.gameObject.SetActive(value: false);
		bool flag = options.Count == 0;
		ui.Content_Button.onClick.RemoveAllListeners();
		ui.Content_Button.interactable = !flag;
		ui.Content_Button.transform.SetAsLastSibling();
		if (!flag)
		{
			bool active = initValues.Count < options.Count;
			ui.Content_Button.gameObject.SetActive(active);
			ui.Content_Button.onClick.AddListener(delegate
			{
				LayoutRebuilder.ForceRebuildLayoutImmediate(ui.Content_Dropdown.transform.parent.GetComponent<RectTransform>());
				LayoutRebuilder.ForceRebuildLayoutImmediate(parent.GetComponent<RectTransform>());
				onButtonClick(ui);
			});
		}
	}

	private void defineTargetProperty(string label, string tooltip, bool modified, Action onClick, Action<float> onScrub, float scrubInitialValue = 0f)
	{
		label = tryGetLocalized(label);
		tooltip = tryGetLocalized(tooltip);
		TargetPropertyUI targetPropertyUI = UnityEngine.Object.Instantiate(propertiesUI.TargetProperty, propertiesUI.Content);
		targetPropertyUI.gameObject.SetActive(value: true);
		targetPropertyUI.Label_Text.text = label;
		setHoverTooltip(tooltip, targetPropertyUI.Label, UITooltip.Position.Left);
		if (modified)
		{
			targetPropertyUI.Button_Modified.gameObject.SetActive(value: true);
		}
		registerProperty(targetPropertyUI, null, null);
		targetPropertyUI.Button.onClick.RemoveAllListeners();
		targetPropertyUI.Button.onClick.AddListener(delegate
		{
			if (onScrub != null && scrubInitialValue != 0f)
			{
				onScrub(0f);
			}
			onClick();
		});
		targetPropertyUI.Slider.gameObject.SetActive(onScrub != null);
		if (onScrub != null)
		{
			targetPropertyUI.Slider.value = scrubInitialValue;
			targetPropertyUI.Slider.onValueChanged.AddListener(delegate(float t)
			{
				onScrub(t);
			});
		}
	}

	private void defineSoundProperty(string label, string tooltip, string value, Action onClick)
	{
		label = tryGetLocalized(label);
		tooltip = tryGetLocalized(tooltip);
		SoundPropertyUI soundPropertyUI = UnityEngine.Object.Instantiate(propertiesUI.SoundProperty, propertiesUI.Content);
		soundPropertyUI.gameObject.SetActive(value: true);
		soundPropertyUI.Label_Text.text = Localization.translate(label);
		setHoverTooltip(tooltip, soundPropertyUI.Label, UITooltip.Position.Left);
		soundPropertyUI.Value_TextMask_Text.text = value;
		registerProperty(soundPropertyUI, null, null);
		soundPropertyUI.Value.onClick.RemoveAllListeners();
		soundPropertyUI.Value.onClick.AddListener(delegate
		{
			onClick();
		});
	}

	private TargetArrayEntry getTargetArrayEntry(GameObject instance)
	{
		TargetArrayEntry result = new TargetArrayEntry
		{
			index = -1
		};
		if (instance.TryGetComponent<Interactive>(out var component))
		{
			result.name = component.displayName;
		}
		if (propIcons.TryGetValue(new PropID
		{
			value = instance.name
		}, out var value))
		{
			result.texture = value;
		}
		return result;
	}

	private void definePasswordProperty(string label, string tooltip, int[] elements, Action<int> onAdd, Action<int> onRemove, Action<int, int> onChange)
	{
		PasswordPropertyUI passwordPropertyUI = UnityEngine.Object.Instantiate(propertiesUI.PasswordProperty, propertiesUI.Content);
		passwordPropertyUI.gameObject.SetActive(value: true);
		passwordPropertyUI.Label_Text.text = Localization.translate(label);
		setHoverTooltip(tooltip, passwordPropertyUI.Label, UITooltip.Position.Left);
		registerProperty(passwordPropertyUI, null, null);
		bool removalAlloved = elements.Length > 1;
		for (int i = 0; i < elements.Length; i++)
		{
			int index = i;
			PasswordElementUI elementUI = UnityEngine.Object.Instantiate(passwordPropertyUI.PasswordElement, passwordPropertyUI.root);
			elementUI.gameObject.SetActive(value: true);
			elementUI.root.text = elements[i].ToString();
			elementUI.root.name = label + "-Element" + i;
			elementUI.root.onEndEdit.RemoveAllListeners();
			elementUI.root.onEndEdit.AddListener(delegate(string value)
			{
				if (!elementUI.root.wasCanceled)
				{
					if (value.Length == 0 && removalAlloved)
					{
						onRemove(index);
					}
					else
					{
						if (value.Length == 0)
						{
							value = "0";
							elementUI.root.text = value;
						}
						int num = int.Parse(value);
						if (elements[index] != num)
						{
							onChange(index, num);
						}
					}
				}
			});
			elementUI.Remove.gameObject.SetActive(removalAlloved);
			elementUI.Remove.onClick.RemoveAllListeners();
			elementUI.Remove.onClick.AddListener(delegate
			{
				onRemove(index);
			});
		}
		PasswordElementUI addUI = UnityEngine.Object.Instantiate(passwordPropertyUI.PasswordElement, passwordPropertyUI.root);
		addUI.gameObject.SetActive(value: true);
		addUI.root.text = "";
		addUI.root.name = label + "-AddPasswordElement";
		addUI.root.onEndEdit.RemoveAllListeners();
		addUI.root.onEndEdit.AddListener(delegate(string value)
		{
			if (!addUI.root.wasCanceled && value.Length > 0)
			{
				InputFieldNavigationData.currentFocusedInputFieldName = addUI.root.name;
				onAdd(int.Parse(value));
			}
			else
			{
				InputFieldNavigationData.currentFocusedInputFieldName = "";
			}
		});
		addUI.Remove.gameObject.SetActive(value: false);
		PasswordHintPropertyUI passwordHintPropertyUI = UnityEngine.Object.Instantiate(propertiesUI.PasswordHintProperty, propertiesUI.Content);
		passwordHintPropertyUI.gameObject.SetActive(value: true);
		registerProperty(passwordHintPropertyUI, null, null);
	}

	private void defineSkyTextureProperty(string label, string tooltip, UnityEngine.Texture texture, Action onClick, Action onRevert = null)
	{
		SkyTexturePropertyUI skyTexturePropertyUI = UnityEngine.Object.Instantiate(propertiesUI.SkyTextureProperty, propertiesUI.Content);
		skyTexturePropertyUI.gameObject.SetActive(value: true);
		skyTexturePropertyUI.Label_Text.text = Localization.translate(label);
		setHoverTooltip(tooltip, skyTexturePropertyUI.Label, UITooltip.Position.Left);
		skyTexturePropertyUI.Texture.texture = texture;
		registerProperty(skyTexturePropertyUI, null, null);
		Vector2 size = skyTexturePropertyUI.TexButton.rectT().rect.size;
		if (texture != null)
		{
			Rect rect = rectAspectFit(size, new Vector2(texture.width, texture.height));
			skyTexturePropertyUI.Texture.rectT().sizeDelta = rect.size;
			skyTexturePropertyUI.Texture.rectT().anchoredPosition = rect.min;
		}
		skyTexturePropertyUI.TexButton.onClick.AddListener(delegate
		{
			onClick();
		});
		skyTexturePropertyUI.Revert.gameObject.SetActive(onRevert != null);
		if (onRevert != null)
		{
			skyTexturePropertyUI.Revert.onClick.AddListener(delegate
			{
				onRevert();
			});
		}
	}

	private (PropData prev, PropData next) prepChangeData(PropInstance instance)
	{
		PropData propData = compressProp(instance, getEditorContext());
		PropData item = copy(propData);
		return (prev: propData, next: item);
	}

	public EditorContext getEditorContext()
	{
		if (contextCache == null)
		{
			contextCache = new EditorContext();
		}
		contextCache.container = levelContainer;
		contextCache.propInstances = propInstances;
		contextCache.customModels = customModelPrefabs;
		contextCache.loadingCustomModels = loadingCustomModels;
		contextCache.roomDirPath = roomDirPath;
		contextCache.loadTexturesOps = loadTexturesOps;
		contextCache.staggeredTextureLoad = staggeredTextureLoad;
		contextCache.assetsBundles = assetsBundles;
		contextCache.assetsIconsBundle = assetsIconsBundle;
		contextCache.assetsRequests = assetsRequests;
		contextCache.refresh = refresh;
		contextCache.useNonLegacyLights = useNonLegacyLights;
		contextCache.soundPointersCache = soundPointersCache;
		contextCache.materialMissingTexture = materialMissingTexture;
		contextCache.editorVolumeRefs = editorVolumeRefs;
		return contextCache;
	}

	private static Interactive addInteractive(PropInstance instance)
	{
		if (instance.TryGetComponent<Interactive>(out var component))
		{
			if (component.GetType() == typeof(Interactive))
			{
				return component;
			}
			UnityEngine.Debug.Log("Destroying Interactives for " + instance.name);
			UnityEngine.Object.DestroyImmediate(component);
		}
		return instance.gameObject.AddComponent<Interactive>();
	}

	private static void removeInteractive(PropInstance instance, Type ignore = null)
	{
		if (instance.TryGetComponent<Interactive>(out var component) && component.GetType() != ignore)
		{
			UnityEngine.Debug.Log("Destroying Interactive for " + instance.name);
			UnityEngine.Object.DestroyImmediate(component);
		}
	}

	private static ItemData getDefaultItemData(PropInstance instance)
	{
		return new ItemData
		{
			hasRigidbody = false,
			carriable = false,
			itemType = ItemType.Undefined
		};
	}

	private static Item addItemComponent(PropInstance instance)
	{
		removeInteractive(instance, typeof(Item));
		if (!instance.TryGetComponent<Item>(out var component))
		{
			return instance.gameObject.AddComponent<Item>();
		}
		return component;
	}

	private static Switch3DData getDefaultSwitch3dData(PropInstance instance, bool isAnimation)
	{
		return new Switch3DData
		{
			duration = 1f,
			type = Switch3DType.Flip,
			isAnimation = isAnimation,
			autoplay = false,
			pauseOnLoop = 0f,
			onLinks = new RoomEditorLinksData(),
			offLinks = new RoomEditorLinksData()
		};
	}

	private static Switch3D addSwitch3dComponent(PropInstance instance)
	{
		removeInteractive(instance, typeof(Switch3D));
		if (!instance.TryGetComponent<Switch3D>(out var component))
		{
			return instance.gameObject.AddComponent<Switch3D>();
		}
		return component;
	}

	private static TweenStateDataRoomEditor getDefaultTweenStateData(PropInstance instance)
	{
		return new TweenStateDataRoomEditor
		{
			tweenStates = new List<TweenStateRecordRoomEditor>()
		};
	}

	private static TweenState addTweenStateComponent(PropInstance instance)
	{
		if (!instance.TryGetComponent<TweenState>(out var component))
		{
			return instance.gameObject.AddComponent<TweenState>();
		}
		return component;
	}

	private static TransformData getDefaultPivotTransform(GameObject switchGO, string pivotName)
	{
		TransformData transformData = new TransformData
		{
			isLocal = true,
			position = Vector3.zero,
			rotation = Vector3.zero,
			scale = Vector3.one
		};
		Transform transform = switchGO.transform.Find(pivotName);
		if (transform != null && transform.GetComponent<Renderer>() == null)
		{
			transformData.position = transform.localPosition;
			transformData.rotation = transform.localRotation.eulerAngles;
			transformData.scale = transform.localScale;
		}
		return transformData;
	}

	private static TurnableData getDefaultTurnableData()
	{
		return new TurnableData
		{
			worldAxis = Vector3.right,
			screenAxis = Vector2.right,
			steps = 4,
			useHotspots = true,
			useAngleLimits = false,
			angleLimit1 = 0f,
			angleLimit2 = 0f,
			clickDirection = 1,
			type = TurnableType.Rotation,
			speed = 1f,
			locks = new List<LockArgData>()
		};
	}

	private static Turnable addTurnableComponent(PropInstance instance)
	{
		removeInteractive(instance, typeof(Turnable));
		if (!instance.TryGetComponent<Turnable>(out var component))
		{
			return instance.gameObject.AddComponent<Turnable>();
		}
		return component;
	}

	private static RotatableData getDefaultRotatableData()
	{
		return new RotatableData
		{
			speed = 1f
		};
	}

	private static Rotatable addRotatableComponent(PropInstance instance)
	{
		removeInteractive(instance, typeof(Rotatable));
		if (!instance.TryGetComponent<Rotatable>(out var component))
		{
			return instance.gameObject.AddComponent<Rotatable>();
		}
		return component;
	}

	private static DraggableData getDefaultDraggableData()
	{
		return new DraggableData();
	}

	private static Draggable addDraggableComponent(PropInstance instance, bool loading = false)
	{
		removeInteractive(instance, typeof(Draggable));
		if (!instance.TryGetComponent<Draggable>(out var component))
		{
			return instance.gameObject.AddComponent<Draggable>();
		}
		return component;
	}

	private static DialData getDefaultDialData()
	{
		return new DialData
		{
			rotationAxis = Vector3.up,
			valueCount = 20,
			clickDirection = 1,
			locks = new List<LockArgData>()
		};
	}

	private static TokenData getDefaultTokenData()
	{
		return new TokenData
		{
			id = nextTokenId++
		};
	}

	private static Token addTokenComponent(PropInstance instance)
	{
		removeInteractive(instance, typeof(Token));
		if (!instance.TryGetComponent<Token>(out var component))
		{
			return instance.gameObject.AddComponent<Token>();
		}
		return component;
	}

	private static Dial addDialComponent(PropInstance instance)
	{
		removeInteractive(instance, typeof(Dial));
		if (!instance.TryGetComponent<Dial>(out var component))
		{
			return instance.gameObject.AddComponent<Dial>();
		}
		return component;
	}

	private static ZoomableData getDefaultZoomableData()
	{
		return new ZoomableData();
	}

	private static Zoomable addZoomableComponent(PropInstance instance)
	{
		removeInteractive(instance, typeof(Zoomable));
		if (!instance.TryGetComponent<Zoomable>(out var component))
		{
			return instance.gameObject.AddComponent<Zoomable>();
		}
		return component;
	}

	private static SlidableData getDefaultSlidableData(PropInstance instance)
	{
		return new SlidableData
		{
			snapAnimationDuration = 0.3f,
			endNode = getDefaultPivotTransform(instance.gameObject, "endNode"),
			locks = new List<LockArgData>()
		};
	}

	private static LookableData getDefaultLookableData(PropInstance instance)
	{
		return new LookableData
		{
			onActivated = new List<InstanceID>(),
			onDeactivated = new List<InstanceID>(),
			targetVisibilityPercent = 0.5f,
			targetScreenPercent = 0.5f
		};
	}

	private static Lookable addLookableComponent(PropInstance instance)
	{
		removeInteractive(instance, typeof(Lookable));
		if (!instance.TryGetComponent<Lookable>(out var component))
		{
			return instance.gameObject.AddComponent<Lookable>();
		}
		return component;
	}

	private static Slidable addSlidableComponent(PropInstance instance)
	{
		removeInteractive(instance, typeof(Slidable));
		if (!instance.TryGetComponent<Slidable>(out var component))
		{
			component = instance.gameObject.AddComponent<Slidable>();
		}
		component.startNode = getNode(instance, component, "startNode").transform;
		component.endNode = getNode(instance, component, "endNode").transform;
		return component;
		static Transform getNode(PropInstance propInstance, Slidable slidable, string name)
		{
			Transform transform = slidable.transform.Find(name);
			if (transform != null)
			{
				propInstance.originalPivot = saveTransform(transform, isLocal: true);
			}
			else
			{
				transform = new GameObject().transform;
				transform.name = name;
				transform.SetParent(slidable.transform);
				transform.gameObject.SetActive(value: false);
				propInstance.originalPivot = new TransformData
				{
					isLocal = true,
					scale = Vector3.one
				};
			}
			return transform;
		}
	}

	private static ChangePropResult changeProp(PropData prev, PropData next, EditorContext context)
	{
		ChangePropResult changePropResult = ChangePropResult.Success;
		bool flag = prev == null && next != null;
		bool flag2 = prev != null && next == null;
		if (flag)
		{
			if (newPropInstance(next.propID, next.ID, context) == null)
			{
				changePropResult = ChangePropResult.FailedToInstantiate;
			}
			context.refresh.editorDisplayCount = true;
			context.refresh.propCountUI = true;
			context.refresh.hierarchyUI = 0;
		}
		else if (flag2)
		{
			PropInstance instanceByID = getInstanceByID(context.propInstances, prev.ID);
			context.propInstances.Remove(instanceByID.ID);
			UnityEngine.Object.Destroy(instanceByID.gameObject);
			context.refresh.unloadUnusedAssets = true;
			context.refresh.editorDisplayCount = true;
			context.refresh.propCountUI = true;
			context.refresh.hierarchyUI = 1;
		}
		context.refresh.navMeshPreview = 1;
		if (next == null || changePropResult != ChangePropResult.Success)
		{
			return changePropResult;
		}
		PropInstance instance = getInstanceByID(context.propInstances, next.ID);
		InstanceID instanceID = prev?.parentID ?? InstanceID.None;
		TransformData transformData = next.transform;
		if (instanceID != next.parentID)
		{
			if (instanceID != InstanceID.None)
			{
				getInstanceByID(context.propInstances, instanceID).children.Remove(instance);
			}
			InstanceID parentID = next.parentID;
			PropInstance propInstance = null;
			Transform parent = context.container.transform;
			if (parentID != InstanceID.None)
			{
				propInstance = getInstanceByID(context.propInstances, parentID);
				propInstance.children.Add(instance);
				parent = propInstance.transform;
			}
			instance.transform.SetParent(parent, worldPositionStays: true);
			instance.parent = propInstance;
			if (!flag)
			{
				transformData = saveTransform(instance.transform, isLocal: true);
			}
			context.refresh.hierarchyUI = 0;
		}
		applyTransform(instance.transform, transformData);
		if (!flag && prev.displayName != next.displayName)
		{
			context.refresh.hierarchyUI = 0;
		}
		if (!flag && prev.scriptName != next.scriptName)
		{
			context.refresh.hierarchyUI = 0;
		}
		if (!flag && prev.targetPriority != next.targetPriority)
		{
			context.refresh.hierarchyUI = 0;
		}
		instance.isExpandedInHierarchy = next.isExpandedInHierarchy;
		instance.displayName = next.displayName;
		instance.scriptName = next.scriptName;
		if (instance.GetComponent<Obstacle>() == null)
		{
			if (next.isObstacle)
			{
				addNavMeshObstacles(instance);
			}
			else
			{
				removeNavMeshObstacles(instance);
			}
		}
		Joint componentInChildren = instance.GetComponentInChildren<Joint>();
		if (componentInChildren != null)
		{
			componentInChildren.autoConfigureConnectedAnchor = false;
			componentInChildren.connectedAnchor = componentInChildren.transform.position;
		}
		ItemData itemData = getItemData(prev);
		ItemData itemData2 = getItemData(next);
		if (itemData != null && itemData2 != null)
		{
			changeItem(itemData, itemData2);
		}
		else if (itemData2 != null)
		{
			addItem(itemData2);
		}
		else
		{
			removeItem();
		}
		changePropComponent(prev, next, instance, (PropData propData) => (propData == null || propData.floor.Count == 0) ? null : propData.floor[0], delegate(Floor floor, FloorData floorData, bool isCreate)
		{
			floor.vertices = new List<Vector2>(floorData.vertices);
			floor.holes = new List<Hole>();
			foreach (Hole hole in floorData.holes)
			{
				floor.holes.Add(hole.copy());
			}
			floor.baseThickness = floorData.baseThickness;
			floor.extrusionThickness = floorData.extrusionThickness;
			floor.extrusionType = floorData.extrusionType;
			floor.collisionType = floorData.collisionType;
			floor.hasCollider = floorData.hasCollider;
			floor.wallHeight = floorData.wallHeight;
			floor.wallThickness = floorData.wallThickness;
			floor.connectWalls = floorData.connectWalls;
			floor.recalculate();
		});
		changePropComponent(prev, next, instance, (PropData propData) => (propData == null || propData.wall.Count == 0) ? null : propData.wall[0], delegate(Wall wall, WallData wallData, bool isCreate)
		{
			wall.vertices = new List<Vector2>(wallData.vertices);
			wall.holes = new List<Hole>();
			foreach (Hole hole2 in wallData.holes)
			{
				wall.holes.Add(hole2.copy());
			}
			wall.baseThickness = wallData.baseThickness;
			wall.extrusionThickness = wallData.extrusionThickness;
			wall.collisionType = wallData.collisionType;
			wall.hasCollider = wallData.hasCollider;
			wall.extrusionType = wallData.extrusionType;
			wall.recalculate();
		});
		changePropComponent(prev, next, instance, (PropData propData) => (propData == null || propData.test.Count == 0) ? null : propData.test[0], delegate(Test test, TestData testData, bool isCreate)
		{
			test.intValue = testData.integer;
			test.intRange = testData.integerRange;
			test.floatValue = testData.single;
			test.floatRange = testData.singleRange;
			test.vector3Value = testData.vector3;
			test.vector2Value = testData.vector2;
			test.stringValue = testData.text;
			test.color1Value = testData.color1;
			test.color2Value = testData.color2;
			test.boolValue = testData.boolean;
			test.enumValue = testData.option;
			test.textureValue = testData.textureId;
		});
		changePropComponent(prev, next, instance, (PropData propData) => (propData == null || propData.text.Count == 0) ? null : propData.text[0], delegate(EditorText editorText, TextData textData, bool isCreate)
		{
			editorText.text = textData.text;
			editorText.fontSize = textData.fontSize;
			editorText.color = textData.color;
			editorText.isBold = textData.isBold;
			editorText.isItalic = textData.isItalic;
			editorText.canvasSize = textData.canvasSize;
			editorText.syncVisuals();
		});
		changePropComponent(prev, next, instance, (PropData propData) => (propData == null || propData.delay.Count == 0) ? null : propData.delay[0], delegate(EditorDelay delay, DelayData delayData, bool isCreate)
		{
			delay.targets = syncTargetDataList(delayData.targets, context);
			delay.delay = delayData.delay;
		});
		TweenStateDataRoomEditor tweenStateDataRoomEditor = getTweenStateDataRoomEditor(prev);
		TweenStateDataRoomEditor tweenStateDataRoomEditor2 = getTweenStateDataRoomEditor(next);
		if (tweenStateDataRoomEditor != null && tweenStateDataRoomEditor2 != null)
		{
			changeTweenState(tweenStateDataRoomEditor, tweenStateDataRoomEditor2);
		}
		else if (tweenStateDataRoomEditor2 != null)
		{
			addTweenState(tweenStateDataRoomEditor2);
		}
		else
		{
			removeTweenState();
		}
		Switch3DData switch3DData = getSwitch3DData(prev);
		Switch3DData switch3DData2 = getSwitch3DData(next);
		if (switch3DData != null && switch3DData2 != null)
		{
			changeSwitch3D(switch3DData, switch3DData2);
		}
		else if (switch3DData2 != null)
		{
			addSwitch3D(switch3DData2);
		}
		else
		{
			removeSwitch3D();
		}
		TurnableData turnableData = getTurnableData(prev);
		TurnableData turnableData2 = getTurnableData(next);
		if (turnableData != null && turnableData2 != null)
		{
			changeTurnable(turnableData, turnableData2);
		}
		else if (turnableData2 != null)
		{
			addTurnable(turnableData2);
		}
		else
		{
			removeTurnable();
		}
		RotatableData rotatableData = getRotatableData(prev);
		RotatableData rotatableData2 = getRotatableData(next);
		if (rotatableData != null && rotatableData2 != null)
		{
			changeRotatable(rotatableData, rotatableData2);
		}
		else if (rotatableData2 != null)
		{
			addRotatable(rotatableData2);
		}
		else
		{
			removeRotatable();
		}
		DraggableData draggableData = getDraggableData(prev);
		DraggableData draggableData2 = getDraggableData(next);
		if (draggableData != null && draggableData2 != null)
		{
			changeDraggable(draggableData, draggableData2);
		}
		else if (draggableData2 != null)
		{
			addDraggable(draggableData2);
		}
		else
		{
			removeDraggable();
		}
		DialData dialData = getDialData(prev);
		DialData dialData2 = getDialData(next);
		if (dialData != null && dialData2 != null)
		{
			changeDial(dialData, dialData2);
		}
		else if (dialData2 != null)
		{
			addDial(dialData2);
		}
		else
		{
			removeDial();
		}
		ZoomableData zoomableData = getZoomableData(prev);
		ZoomableData zoomableData2 = getZoomableData(next);
		if (zoomableData != null && zoomableData2 != null)
		{
			changeZoomable(zoomableData, zoomableData2);
		}
		else if (zoomableData2 != null)
		{
			addZoomable(zoomableData2);
		}
		else
		{
			removeZoomable();
		}
		SlidableData slidableData = getSlidableData(prev);
		SlidableData slidableData2 = getSlidableData(next);
		if (slidableData != null && slidableData2 != null)
		{
			changeSlidable(slidableData, slidableData2);
		}
		else if (slidableData2 != null)
		{
			addSlidable(slidableData2);
		}
		else
		{
			removeSlidable();
		}
		LookableData lookableData = getLookableData(prev);
		LookableData lookableData2 = getLookableData(next);
		if (lookableData != null && lookableData2 != null)
		{
			changeLookable(lookableData, lookableData2);
		}
		else if (lookableData2 != null)
		{
			addLookable(lookableData2);
		}
		else
		{
			removeLookable();
		}
		TokenData tokenData = getTokenData(prev);
		TokenData tokenData2 = getTokenData(next);
		if (tokenData != null && tokenData2 != null)
		{
			changeToken(tokenData, tokenData2);
		}
		else if (tokenData2 != null)
		{
			addToken(tokenData2);
		}
		else
		{
			removeToken();
		}
		if (instance.TryGetComponent<Interactive>(out var component))
		{
			component.displayName = next.displayName;
			component.roomEditorTargetPriotity = next.targetPriority;
		}
		if (tryGetFinishData(next, out var data))
		{
			Finish component2 = instance.GetComponent<Finish>();
			component2.levelShot.eyeLocalPosition = data.eyePosition;
			component2.levelShot.eyeLocalRotation = data.eyeRotation;
		}
		if (tryGetLockData(next, out var data2) && instance.TryGetComponent<Lock>(out var component3))
		{
			component3.lockType = data2.type;
			component3.password = data2.password.ToArray();
			component3.onUnlock.output = data2.onUnlock.output;
			component3.deactivateLockOnUnlock = data2.disableLockOnUnlock;
			component3.exitZoomOnUnlock = data2.exitZoomOnUnlock;
			component3.onUnlock = syncRoomEditorLinks(data2.onUnlock, context);
			component3.onLock = syncRoomEditorLinks(data2.onLock, context);
			component3.lockLogicType = data2.logicType;
			component3.lockNegateValue = data2.negateValue;
		}
		if (tryGetSlotData(next, out var data3))
		{
			Slot component4 = instance.GetComponent<Slot>();
			component4.rotateKeyTurnDuration = data3.turnDuration;
			component4.rotateKeyTurnCount = data3.turnCount;
			component4.rotateKeyTurnAxis = data3.turnAxis;
			component4.ejectDirection = data3.ejectDirection;
			component4.onAcceptItemPlaced = data3.unlockSuccessKey;
			component4.onRejectItemPlaced = data3.unlockFailedKey;
			component4.animationType = data3.animationType;
			component4.onPlace = syncRoomEditorLinks(data3.onPlace, context);
			component4.onRemove = syncRoomEditorLinks(data3.onRemove, context);
			component4.acceptItems = syncKeysDataItem(data3.keys, context);
			component4.rejectItems = syncKeysDataItem(data3.rejectKeys, context);
			component4.initialInsertedItem = ((data3.initialItemID != InstanceID.None) ? getInstanceByID(context.propInstances, data3.initialItemID).GetComponent<Item>() : null);
			if (data3.pivot.scale != Vector3.zero)
			{
				applyTransform(component4.pivot, data3.pivot);
			}
			if (flag)
			{
				instance.originalPivot = new TransformData
				{
					isLocal = true,
					scale = Vector3.one
				};
			}
		}
		if (tryGetLadderData(next, out var data4))
		{
			Ladder component5 = instance.GetComponent<Ladder>();
			component5.upEmpty.transform.localPosition = data4.upLocalPosition;
			component5.downEmpty.transform.localPosition = data4.downLocalPosition;
			component5.upExit.transform.localPosition = data4.upExitLocalPosition;
			component5.downExit.transform.localPosition = data4.downExitLocalPosition;
		}
		if (tryGetTriggerData(next, out var data5))
		{
			Trigger component6 = instance.GetComponent<Trigger>();
			component6.keys = syncKeysData(data5.keys, context);
			component6.anyObjectCanTrigger = data5.anyObjectCanTrigger;
			component6.canPlayerTrigger = data5.canPlayerTrigger;
			component6.triggerWhenAllPlayersEnter = data5.triggerWhenAllPlayersEnter;
			component6.isSticky = data5.isSticky;
			component6.enterData = syncRoomEditorLinks(data5.onEnter, context);
			component6.exitData = syncRoomEditorLinks(data5.onExit, context);
			component6.startData = syncRoomEditorLinks(data5.onStart, context);
			component6.endData = syncRoomEditorLinks(data5.onEnd, context);
		}
		if (tryGetSpawnPointData(next, out var data6))
		{
			SpawnPoint component7 = instance.GetComponent<SpawnPoint>();
			component7.walkingSpeed = data6.walkingSpeed;
			component7.runningSpeed = data6.runningSpeed;
		}
		if (tryGetTeleportData(next, out var data7))
		{
			Teleport component8 = instance.GetComponent<Teleport>();
			component8.changeRotation = data7.changeRotation;
			component8.teleportAll = data7.teleportAll;
			component8.walkingSpeed = data7.walkingSpeed;
			component8.runningSpeed = data7.runningSpeed;
		}
		if (tryGetStairsData(next, out var data8))
		{
			Stairs component9 = instance.GetComponent<Stairs>();
			component9.direction = data8.direction;
			component9.syncVisuals();
		}
		if (tryGetFogData(next, out var data9))
		{
			Fog component10 = instance.GetComponent<Fog>();
			component10.data.activeOnStart = data9.activeOnStart;
			component10.data.transitionTime = data9.transitionTime;
			component10.data.attenuation = data9.attenuation;
			component10.data.distance = data9.distance;
			component10.data.color = data9.color;
			component10.data.isVolumetric = data9.isVolumetric;
			component10.data.localOnly = data9.localOnly;
			component10.data.volumetricColor = data9.volumetricColor;
		}
		if (tryGetSoundData(next, out var newSoundData))
		{
			Sound component11 = instance.GetComponent<Sound>();
			if (component11.path != newSoundData.path)
			{
				if (component11.handle != 0L)
				{
					UnityEngine.Debug.Log($"Unloading sound with handle {component11.handle}");
					new FMOD.Sound(new IntPtr(component11.handle)).release();
					if (context.soundPointersCache.ContainsKey(newSoundData.path))
					{
						context.soundPointersCache.Remove(newSoundData.path);
					}
					component11.handle = 0L;
				}
				component11.path = newSoundData.path;
				if (newSoundData.path != null && newSoundData.path.Length > 0)
				{
					FMOD.System coreSystem = RuntimeManager.CoreSystem;
					if (context.customRoom != null)
					{
						CustomRoomDataFile customRoomDataFile = context.customRoom.customFiles.Find((CustomRoomDataFile x) => x.path == newSoundData.path && x.customType == CustomRoomDataFile.CustomType.Sound);
						if (customRoomDataFile != null)
						{
							if (context.soundPointersCache.ContainsKey(newSoundData.path))
							{
								component11.handle = context.soundPointersCache[newSoundData.path];
								UnityEngine.Debug.Log($"Found cached sound for '{newSoundData.path}' with handle {component11.handle}");
							}
							else
							{
								byte[] data10 = customRoomDataFile.data;
								CREATESOUNDEXINFO exinfo = default(CREATESOUNDEXINFO);
								if (coreSystem.createSound(data10, MODE.OPENMEMORY, ref exinfo, out var sound) == RESULT.OK)
								{
									component11.handle = sound.handle.ToInt64();
									context.soundPointersCache[newSoundData.path] = component11.handle;
									soundsCache.Add(sound);
									UnityEngine.Debug.Log($"Creating sound for '{newSoundData.path}' with handle {component11.handle}");
								}
								else
								{
									UnityEngine.Debug.Log("Failed to create sound at " + red(newSoundData.path));
								}
							}
						}
					}
					else if (context.soundPointersCache.ContainsKey(newSoundData.path))
					{
						component11.handle = context.soundPointersCache[newSoundData.path];
						UnityEngine.Debug.Log($"Found cached sound for '{newSoundData.path}' with handle {component11.handle}");
					}
					else
					{
						string text = context.roomDirPath + "/" + newSoundData.path;
						if (coreSystem.createSound(text, MODE.DEFAULT, out var sound2) == RESULT.OK)
						{
							component11.handle = sound2.handle.ToInt64();
							context.soundPointersCache[newSoundData.path] = component11.handle;
							soundsCache.Add(sound2);
							UnityEngine.Debug.Log($"Creating sound for '{text}' with handle {component11.handle}");
						}
						else
						{
							UnityEngine.Debug.Log("Failed to create sound at " + red(text));
						}
					}
				}
			}
			if (component11._event != newSoundData._event)
			{
				component11._event = newSoundData._event;
				component11.eventHandle = 0L;
				UnityEngine.Debug.Log("new _event: " + component11._event);
				if (newSoundData._event != null && newSoundData._event.Length > 0)
				{
					PineFmod.getAudioPresetsBank().getEventList(out var array);
					int num = Array.FindIndex(array, delegate(EventDescription x)
					{
						x.getPath(out var path);
						return path == newSoundData._event;
					});
					if (num >= 0)
					{
						UnityEngine.Debug.Log($"Creating sound with event {component11.eventHandle}");
						component11.eventHandle = array[num].handle.ToInt64();
					}
				}
			}
			component11.type = newSoundData.type;
			component11.isLoopable = newSoundData.isLoopable;
			component11.is2D = newSoundData.is2D;
			component11.minDistance3D = newSoundData.minDistance3D;
			component11.maxDistance3D = newSoundData.maxDistance3D;
			component11.activeOnStart = newSoundData.activeOnStart;
			component11.volume = newSoundData.volume;
			component11.localOnly = newSoundData.localOnly;
			component11.falloffMode = newSoundData.falloffMode;
		}
		SkyboxData skyboxData = getSkyboxData(prev);
		SkyboxData skyboxData2 = getSkyboxData(next);
		if (skyboxData2 != null)
		{
			EditorSkybox component12 = instance.GetComponent<EditorSkybox>();
			component12.data = skyboxData2;
			SkyTexData skyTexData = skyboxData?.textureData;
			SkyTexData textureData = skyboxData2.textureData;
			if (skyTexData != null && string.IsNullOrEmpty(skyTexData.fileName))
			{
				component12.data.tempTexture = null;
			}
			if (textureData != null)
			{
				loadSkyTexture(component12.data);
			}
		}
		CloudsData cloudsData = getCloudsData(next);
		if (cloudsData != null)
		{
			instance.GetComponent<EditorClouds>().data = cloudsData;
		}
		OceanData oceanData = getOceanData(next);
		if (oceanData != null)
		{
			instance.GetComponent<EditorOcean>().data = oceanData;
		}
		if (tryGetOpenLinkData(next, out var data11))
		{
			instance.GetComponent<OpenLink>().link = data11.link;
		}
		if (tryGetWaterData(next, out var data12))
		{
			EditorWater component13 = instance.GetComponent<EditorWater>();
			component13.data = data12;
			component13.syncVisuals();
		}
		if (tryGetActivatorComponentData(next, out var data13))
		{
			ActivatorComponent component14 = instance.GetComponent<ActivatorComponent>();
			component14.keys = syncKeysData(data13.keys, context);
			component14.type = data13.type;
			component14.targetObject = data13.targetObject;
			component14.targetRenderer = data13.targetRenderer;
			component14.targetCollider = data13.targetCollider;
			component14.targetObstacle = data13.targetObstacle;
			component14.targetTargetable = data13.targetTargetable;
			component14.activeOnStart = data13.activeOnStart;
		}
		if (tryGetEditorLightData(next, out var data14))
		{
			EditorLight component15 = instance.GetComponent<EditorLight>();
			component15.lightComponent.type = data14.type;
			component15.lightComponent.range = data14.range;
			component15.lightComponent.spotAngle = data14.spotAngle;
			component15.lightComponent.color = data14.color;
			component15.lightComponent.intensity = data14.intensity;
			component15.lightComponent.shadows = (data14.castShadows ? LightShadows.Hard : LightShadows.None);
			component15.hdLightData.shadowDimmer = data14.shadowDimmer;
			component15.cookieTextureId = data14.cookieTextureId;
		}
		if (tryGetEditorPostProcessingData(next, out var data15))
		{
			instance.GetComponent<EditorPostProcessing>().data = data15;
		}
		EditorDisplay _display;
		if (tryGetEditorDisplayData(next, out var data16))
		{
			EditorDisplayData editorDisplayData = getEditorDisplayData(prev);
			_display = instance.GetComponent<EditorDisplay>();
			PropInstance instanceByID2 = getInstanceByID(context.propInstances, data16.targetLock);
			_display.targetLock = ((instanceByID2 != null) ? instanceByID2.GetComponent<Lock>() : null);
			_display.columns = data16.columns;
			_display.rows = data16.rows;
			_display.padding = data16.padding;
			_display.spriteSheetColumns = data16.spriteSheetColumns;
			_display.spriteSheetRows = data16.spriteSheetRows;
			_display.spriteSheetFileName = data16.spriteSheetFileName;
			if (editorDisplayData == null || editorDisplayData.spriteSheetFileName != data16.spriteSheetFileName)
			{
				loadDisplayTexture(data16);
			}
		}
		if (tryGetPuzzleData(next, out var data17))
		{
			EditorPuzzle component16 = instance.GetComponent<EditorPuzzle>();
			component16.puzzleName = data17.puzzleName;
			component16.hints = data17.hints.ToArray();
			component16.conditions = syncTargetDataList(data17.conditions, context);
			component16.muteSound = data17.muteSound;
		}
		if (tryGetRouletteData(next, out var data18))
		{
			Roulette component17 = instance.GetComponent<Roulette>();
			component17.targets = syncTargetDataList(data18.targets, context);
			component17.targetCanBeActivatedOnlyOnce = data18.removeTargetOnTrigger;
			component17.testingTarget = data18.testingTarget;
		}
		if (tryGetEditorSetupData(next, out var data19))
		{
			EditorSetup component18 = instance.GetComponent<EditorSetup>();
			component18.targets = syncTargetDataList(data19.targets, context);
			component18.difficulty = data19.difficulty;
		}
		if (tryGetItemRespawnerData(next, out var data20))
		{
			ItemRespawnVolume component19 = instance.GetComponent<ItemRespawnVolume>();
			component19.respawnMode = data20.respawnMode;
			component19.respawnWithPhysics = data20.respawnWithPhysics;
			PropInstance instanceByID3 = getInstanceByID(context.propInstances, data20.objectToRespawnTo);
			component19.objectToRespawnTo = ((instanceByID3 != null) ? instanceByID3.transform : null);
		}
		if (tryGetCustomModelData(next, out var data21))
		{
			CustomModel component20 = instance.GetComponent<CustomModel>();
			if (component20 != null)
			{
				component20.disableGeneratedCollider = data21.disableGeneratedCollider;
				component20.currentAnimation = data21.currentAnimation;
				if (component20.modelAnimation != null && component20.currentAnimation >= 0 && component20.currentAnimation < component20.animations.Count)
				{
					string text2 = component20.animations[component20.currentAnimation];
					component20.modelAnimation.clip = component20.modelAnimation.GetClip(text2);
				}
				if (component20.colliderType != data21.colliderType)
				{
					MeshCollider meshCollider = component20.transform.GetComponentInChildren<MeshCollider>();
					if (component20.colliderType == CustomModelColliderType.None || component20.colliderType == CustomModelColliderType.Box)
					{
						Collider component21 = component20.GetComponent<Collider>();
						if (component21 != null)
						{
							UnityEngine.Object.Destroy(component21);
						}
					}
					else if (meshCollider != null)
					{
						meshCollider.gameObject.SetActive(value: false);
					}
					if (data21.colliderType == CustomModelColliderType.None || data21.colliderType == CustomModelColliderType.Box)
					{
						BoxCollider boxCollider = component20.gameObject.AddComponent<BoxCollider>();
						boxCollider.size = component20.calculatedBounds.size;
						boxCollider.center = component20.calculatedBounds.center;
					}
					else
					{
						if (meshCollider == null)
						{
							GameObject obj = new GameObject("convexCollider");
							obj.transform.SetParent(component20.transform);
							meshCollider = obj.gameObject.AddComponent<MeshCollider>();
							MeshFilter[] componentsInChildren = component20.gameObject.GetComponentsInChildren<MeshFilter>();
							CombineInstance[] array2 = new CombineInstance[componentsInChildren.Length];
							for (int num2 = 0; num2 < componentsInChildren.Length; num2++)
							{
								array2[num2].mesh = componentsInChildren[num2].sharedMesh;
								array2[num2].transform = componentsInChildren[num2].transform.localToWorldMatrix;
							}
							meshCollider.sharedMesh = new UnityEngine.Mesh();
							meshCollider.sharedMesh.CombineMeshes(array2, mergeSubMeshes: true, useMatrices: true, hasLightmapData: false);
						}
						else
						{
							meshCollider.gameObject.SetActive(value: true);
						}
						if (component20.GetComponent<Rigidbody>() != null || data21.colliderType == CustomModelColliderType.ConvexMesh)
						{
							meshCollider.convex = true;
						}
						else
						{
							meshCollider.convex = false;
						}
					}
					component20.colliderType = data21.colliderType;
				}
			}
		}
		if (tryGetScriptComponentData(next, out var data22))
		{
			LuaExecutor component22 = instance.GetComponent<LuaExecutor>();
			component22.luaCodeLocation = data22.scriptLocation;
			component22.functionToCall = data22.functionToCall;
			component22.canBeTriggered = data22.canBeTriggered;
		}
		revertPropInstanceToDefaultMaterials(instance, context);
		applyPropInstanceMaterialSwaps(instance, next.materialSwaps);
		return changePropResult;
		void addDial(DialData add)
		{
			Dial dial = addDialComponent(instance);
			syncDial(dial, add);
		}
		void addDraggable(DraggableData add)
		{
			syncDraggable(addDraggableComponent(instance), add);
		}
		void addItem(ItemData add)
		{
			syncItem(addItemComponent(instance), add);
		}
		void addLookable(LookableData add)
		{
			Lookable lookable = addLookableComponent(instance);
			syncLookable(lookable, add);
		}
		void addRotatable(RotatableData add)
		{
			syncRotatable(addRotatableComponent(instance), add);
		}
		void addSlidable(SlidableData add)
		{
			Slidable slidable = addSlidableComponent(instance);
			syncSlidable(slidable, add);
		}
		void addSwitch3D(Switch3DData add)
		{
			Switch3D switch3d = addSwitch3dComponent(instance);
			syncSwitch3D(switch3d, add);
		}
		void addToken(TokenData add)
		{
			syncToken(addTokenComponent(instance), add);
		}
		void addTurnable(TurnableData add)
		{
			Turnable turnable = addTurnableComponent(instance);
			syncTurnable(turnable, add);
		}
		void addTweenState(TweenStateDataRoomEditor add)
		{
			TweenState tweenState = addTweenStateComponent(instance);
			syncTweenStateData(tweenState, add);
		}
		void addZoomable(ZoomableData add)
		{
			syncZoomable(addZoomableComponent(instance), add);
		}
		void changeDial(DialData prevData, DialData newData)
		{
			Dial component23 = instance.GetComponent<Dial>();
			syncDial(component23, newData);
		}
		void changeDraggable(DraggableData prevData, DraggableData newData)
		{
			syncDraggable(instance.GetComponent<Draggable>(), newData);
		}
		void changeItem(ItemData prevData, ItemData newData)
		{
			syncItem(instance.GetComponent<Item>(), newData);
		}
		void changeLookable(LookableData prevData, LookableData newData)
		{
			Lookable component23 = instance.GetComponent<Lookable>();
			syncLookable(component23, newData);
		}
		void changeRotatable(RotatableData prevData, RotatableData newData)
		{
			syncRotatable(instance.GetComponent<Rotatable>(), newData);
		}
		void changeSlidable(SlidableData prevData, SlidableData newData)
		{
			Slidable component23 = instance.GetComponent<Slidable>();
			syncSlidable(component23, newData);
		}
		void changeSwitch3D(Switch3DData prevData, Switch3DData newData)
		{
			Switch3D component23 = instance.GetComponent<Switch3D>();
			syncSwitch3D(component23, newData);
		}
		void changeToken(TokenData prevData, TokenData newData)
		{
			syncToken(instance.GetComponent<Token>(), newData);
		}
		void changeTurnable(TurnableData prevData, TurnableData newData)
		{
			Turnable component23 = instance.GetComponent<Turnable>();
			syncTurnable(component23, newData);
		}
		void changeTweenState(TweenStateDataRoomEditor prevData, TweenStateDataRoomEditor newData)
		{
			TweenState component23 = instance.GetComponent<TweenState>();
			syncTweenStateData(component23, newData);
		}
		void changeZoomable(ZoomableData prevData, ZoomableData newData)
		{
			syncZoomable(instance.GetComponent<Zoomable>(), newData);
		}
		void finishLoadTexture(SkyboxData skyboxData3, UnityEngine.Texture texture)
		{
			Cubemap tempTexture = (Cubemap)texture;
			if (texture == null)
			{
				skyboxData3.textureData = null;
			}
			else
			{
				texture.wrapMode = TextureWrapMode.Clamp;
				skyboxData3.tempTexture = tempTexture;
			}
			context.refresh.propertiesUI = true;
			context.refresh.skybox = true;
		}
		void finishLoadTexture2(UnityEngine.Texture texture)
		{
			texture.filterMode = FilterMode.Point;
			_display.spriteSheetTexture = (Texture2D)texture;
			context.refresh.propertiesUI = true;
			context.refresh.editorDisplay = true;
		}
		static GameObject getTarget(EditorContext editorContext, InstanceID ID)
		{
			return getInstanceByID(editorContext.propInstances, ID).gameObject;
		}
		void loadDisplayTexture(EditorDisplayData displayData)
		{
			string killID = "DisplayTex" + next.ID.value;
			killLoadingTextureOps(context.loadTexturesOps, context.staggeredTextureLoad, killID);
			killAssetRequests(context.assetsRequests, killID);
			if (displayData.spriteSheetFileName == "default")
			{
				string defaultsTexPath = EditorPaths.getDefaultsTexPath("number_sprite");
				loadTexture2DAsync(context, defaultsTexPath, delegate(object asset)
				{
					finishLoadTexture2((Texture2D)asset);
					context.refresh.editorDisplay = true;
				});
			}
			else
			{
				string filePath = context.roomDirPath + "/" + displayData.spriteSheetFileName;
				loadTextureFromDisk(context.staggeredTextureLoad, filePath, killID, delegate(UnityEngine.Texture texture)
				{
					finishLoadTexture2(texture);
					context.refresh.editorDisplay = true;
				});
			}
		}
		void loadSkyTexture(SkyboxData skyboxData3)
		{
			string killID = "SkyTex" + next.ID.value;
			killLoadingTextureOps(context.loadTexturesOps, context.staggeredTextureLoad, killID);
			killAssetRequests(context.assetsRequests, killID);
			if (skyboxData3.textureData.custom)
			{
				string filePath = context.roomDirPath + "/" + skyboxData3.textureData.fileName;
				loadTextureFromDisk(context.staggeredTextureLoad, filePath, killID, delegate(UnityEngine.Texture texture)
				{
					finishLoadTexture(skyboxData3, texture);
				});
			}
			else
			{
				string skyboxTexPath = EditorPaths.getSkyboxTexPath(skyboxData3.textureData.fileName);
				loadTexture2DAsync(context, skyboxTexPath, delegate(object asset)
				{
					finishLoadTexture(skyboxData3, (UnityEngine.Texture)asset);
				}, "");
			}
		}
		void removeDial()
		{
			if (instance.TryGetComponent<Dial>(out var component23))
			{
				UnityEngine.Object.DestroyImmediate(component23);
			}
		}
		void removeDraggable()
		{
			if (instance.TryGetComponent<Draggable>(out var component23))
			{
				UnityEngine.Object.DestroyImmediate(component23);
			}
		}
		void removeItem()
		{
			if (instance.TryGetComponent<Item>(out var component23))
			{
				UnityEngine.Object.DestroyImmediate(component23);
			}
		}
		void removeLookable()
		{
			if (instance.TryGetComponent<Lookable>(out var component23))
			{
				UnityEngine.Object.DestroyImmediate(component23);
			}
		}
		void removeRotatable()
		{
			if (instance.TryGetComponent<Rotatable>(out var component23))
			{
				UnityEngine.Object.DestroyImmediate(component23);
			}
		}
		void removeSlidable()
		{
			if (instance.TryGetComponent<Slidable>(out var component23))
			{
				if (component23.startNode != null)
				{
					UnityEngine.Object.DestroyImmediate(component23.startNode.gameObject);
				}
				if (component23.endNode != null)
				{
					UnityEngine.Object.DestroyImmediate(component23.endNode.gameObject);
				}
				UnityEngine.Object.DestroyImmediate(component23);
			}
		}
		void removeSwitch3D()
		{
			if (instance.TryGetComponent<Switch3D>(out var component23))
			{
				UnityEngine.Object.DestroyImmediate(component23);
			}
		}
		void removeToken()
		{
			if (instance.TryGetComponent<Token>(out var component23))
			{
				UnityEngine.Object.DestroyImmediate(component23);
			}
		}
		void removeTurnable()
		{
			if (instance.TryGetComponent<Turnable>(out var component23))
			{
				UnityEngine.Object.DestroyImmediate(component23);
			}
		}
		void removeTweenState()
		{
			if (instance.TryGetComponent<TweenState>(out var component23))
			{
				UnityEngine.Object.DestroyImmediate(component23);
			}
		}
		void removeZoomable()
		{
			if (instance.TryGetComponent<Zoomable>(out var component23))
			{
				UnityEngine.Object.DestroyImmediate(component23);
			}
		}
		void syncDial(Dial dial, DialData sync)
		{
			dial.rotationAxis = sync.rotationAxis;
			dial.valueCount = sync.valueCount;
			dial.snapToPosition = sync.snapToPosition;
			dial.clickDirection = sync.clickDirection;
			dial.useAngleLimits = sync.useAngleLimits;
			dial.angleLimit1 = sync.angleLimit1;
			dial.angleLimit2 = sync.angleLimit2;
			dial.locks = syncLockDataNeo(sync.locks, context);
		}
		static void syncDraggable(Draggable draggable, DraggableData sync)
		{
			draggable.dragConstraintFlags = sync.dragConstraintFlags;
			draggable.forcePoint = sync.forcePoint;
			draggable.manipulationType = sync.manipulationType;
		}
		static void syncItem(Item item, ItemData sync)
		{
			item.itemType = sync.itemType;
			item.hasRigidbody = sync.hasRigidbody;
			item.carriable = sync.carriable;
		}
		static List<GameObject> syncKeysData(List<InstanceID> keysData, EditorContext editorContext)
		{
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < keysData.Count; i++)
			{
				InstanceID iD = keysData[i];
				PropInstance instanceByID4 = getInstanceByID(editorContext.propInstances, iD);
				list.Add(instanceByID4.gameObject);
			}
			return list;
		}
		static Item[] syncKeysDataItem(List<InstanceID> keysData, EditorContext editorContext)
		{
			Item[] array3 = new Item[keysData.Count];
			for (int i = 0; i < keysData.Count; i++)
			{
				InstanceID iD = keysData[i];
				PropInstance instanceByID4 = getInstanceByID(editorContext.propInstances, iD);
				array3[i] = instanceByID4.GetComponent<Item>();
			}
			return array3;
		}
		static LockArgument[] syncLockData(List<LockArgData> locksData, EditorContext editorContext)
		{
			LockArgument[] array3 = new LockArgument[locksData.Count];
			for (int i = 0; i < locksData.Count; i++)
			{
				LockArgData lockArgData = locksData[i];
				Lock component23 = getInstanceByID(editorContext.propInstances, lockArgData.instanceID).gameObject.GetComponent<Lock>();
				array3[i] = new LockArgument
				{
					targetLock = component23,
					targetIndex = lockArgData.passwordIndex
				};
			}
			return array3;
		}
		static List<LockArgument> syncLockDataNeo(List<LockArgData> locksData, EditorContext editorContext)
		{
			List<LockArgument> list = new List<LockArgument>(locksData.Count);
			for (int i = 0; i < locksData.Count; i++)
			{
				LockArgData lockArgData = locksData[i];
				Lock component23 = getInstanceByID(editorContext.propInstances, lockArgData.instanceID).gameObject.GetComponent<Lock>();
				list.Add(new LockArgument
				{
					targetLock = component23,
					targetIndex = lockArgData.passwordIndex
				});
			}
			return list;
		}
		void syncLookable(Lookable lookable, LookableData sync)
		{
			lookable.targetVisibilityPercent = sync.targetVisibilityPercent;
			lookable.targetScreenPercent = sync.targetScreenPercent;
			lookable.activateOnlyOnce = sync.activateOnlyOnce;
			lookable.onActivated = syncKeysData(sync.onActivated, context);
			lookable.onDeactivated = syncKeysData(sync.onDeactivated, context);
		}
		static RoomEditorLinks syncRoomEditorLinks(RoomEditorLinksData roomEditorLinksData, EditorContext context2)
		{
			return new RoomEditorLinks
			{
				locks = new List<LockArgument>(syncLockData(roomEditorLinksData.locks, context2)),
				targets = new List<GameObject>(syncTargetData(roomEditorLinksData.targets, context2)),
				output = roomEditorLinksData.output
			};
		}
		static void syncRotatable(Rotatable rotatable, RotatableData sync)
		{
			rotatable.speed = sync.speed;
		}
		void syncSlidable(Slidable slidable, SlidableData sync)
		{
			slidable.startNode.transform.localPosition = Vector3.zero;
			slidable.startNode.transform.localRotation = Quaternion.identity;
			applyTransform(slidable.endNode, sync.endNode);
			slidable.snapMode = sync.snapMode;
			slidable.additionalSnapPointCount = sync.additionalSnappingPoints;
			slidable.snapAnimationDuration = sync.snapAnimationDuration;
			slidable.snapPointIndex = sync.startingIndex;
			slidable.locks = syncLockDataNeo(sync.locks, context);
		}
		void syncSwitch3D(Switch3D switch3d, Switch3DData sync)
		{
			switch3d.duration = sync.duration;
			switch3d.switchType = sync.type;
			switch3d.tweenState = switch3d.GetComponent<TweenState>();
			switch3d.tweenState.interpolation = sync.interpolation;
			switch3d.isAnimation = sync.isAnimation;
			switch3d.autoplay = sync.autoplay;
			switch3d.pauseOnLoop = sync.pauseOnLoop;
			switch3d.onLinks = syncRoomEditorLinks(sync.onLinks, context);
			switch3d.offLinks = syncRoomEditorLinks(sync.offLinks, context);
		}
		static GameObject[] syncTargetData(List<InstanceID> targetsData, EditorContext context2)
		{
			GameObject[] array3 = new GameObject[targetsData.Count];
			for (int i = 0; i < targetsData.Count; i++)
			{
				array3[i] = getTarget(context2, targetsData[i]);
			}
			return array3;
		}
		static List<GameObject> syncTargetDataList(List<InstanceID> targetsData, EditorContext context2)
		{
			List<GameObject> list = new List<GameObject>(targetsData.Count);
			for (int i = 0; i < targetsData.Count; i++)
			{
				list.Add(getTarget(context2, targetsData[i]));
			}
			return list;
		}
		static void syncToken(Token token, TokenData sync)
		{
			token.roomEditorId = sync.id;
		}
		void syncTurnable(Turnable turnable, TurnableData sync)
		{
			turnable.worldAxis = sync.worldAxis;
			turnable.screenAxis = sync.screenAxis;
			turnable.steps = sync.steps;
			turnable.useHotspots = sync.useHotspots;
			turnable.useAngleLimits = sync.useAngleLimits;
			turnable.angleLimit1 = sync.angleLimit1;
			turnable.angleLimit2 = sync.angleLimit2;
			turnable.clickDirection = sync.clickDirection;
			turnable.type = sync.type;
			turnable.speed = sync.speed;
			turnable.locks = syncLockDataNeo(sync.locks, context);
		}
		void syncTweenStateData(TweenState tweenState, TweenStateDataRoomEditor tweenStateDataRoomEditor3)
		{
			tweenState.tweenStateDataNeo = new List<TweenState.TweenStateRecord>();
			if (tweenState.TryGetComponent<CustomModel>(out var component23) && component23.modelAnimation != null && component23.currentAnimation >= 0)
			{
				TweenState.TweenStateRecord item = new TweenState.TweenStateRecord
				{
					name = "Down",
					weight = 1f,
					targetWeight = 1f,
					speed = 1f,
					delay = 0f,
					states = new List<ObjectState>
					{
						new ObjectState
						{
							flags = 128,
							gameObject = component23.animationSampler.gameObject,
							path = component23.animationSampler.gameObject.name,
							AnimationSampler_unitTime = 1f
						}
					}
				};
				tweenState.tweenStateDataNeo.Add(item);
				return;
			}
			foreach (TweenStateRecordRoomEditor tweenState2 in tweenStateDataRoomEditor3.tweenStates)
			{
				TweenState.TweenStateRecord tweenStateRecord = new TweenState.TweenStateRecord
				{
					name = tweenState2.name,
					weight = tweenState2.weight,
					targetWeight = tweenState2.targetWeight,
					speed = tweenState2.speed,
					delay = tweenState2.delay,
					states = new List<ObjectState>()
				};
				foreach (ObjectStateRoomEditor state in tweenState2.states)
				{
					ObjectState objectState = new ObjectState
					{
						path = state.path,
						flags = state.flags,
						Image_color = state.Image_color,
						Text_color = state.Text_color,
						Text_fontSize = state.Text_fontSize,
						Transform_localRotation = state.Transform_localRotation,
						Transform_localRotationEuler = state.Transform_localRotationEuler,
						Transform_localScale = state.Transform_localScale,
						Transform_localPosition = state.Transform_localPosition,
						AnimationSampler_unitTime = state.AnimationSampler_unitTime,
						Item_examinePivotOffset = state.Item_examinePivotOffset,
						Item_examineScaleModifier = state.Item_examineScaleModifier,
						Item_examineBaseRotation = state.Item_examineBaseRotation,
						Item_groundRotation = state.Item_groundRotation,
						Interactive_targetPriority = state.Interactive_targetPriority,
						MaterialState = state.MaterialState,
						tweenState = state.tweenState,
						Light_Filter = state.Light_Filter,
						Light_Temperature = state.Light_Temperature,
						Light_Intensity = state.Light_Intensity
					};
					if (string.IsNullOrEmpty(objectState.path))
					{
						tryGetTarget(context, state.gameObjectId, out var gameObject);
						objectState.gameObject = gameObject;
					}
					else
					{
						UnityEngine.Debug.Log(objectState.path + " " + tweenState.transform.Find(objectState.path), tweenState);
						objectState.gameObject = tweenState.transform.Find(objectState.path).gameObject;
					}
					tweenStateRecord.states.Add(objectState);
				}
				tweenState.tweenStateDataNeo.Add(tweenStateRecord);
			}
		}
		static void syncZoomable(Zoomable zoomable, ZoomableData sync)
		{
			zoomable.eyeLocalPosition = sync.eyePosition;
			zoomable.eyeLocalRotation = sync.eyeRotation;
		}
		static bool tryGetTarget(EditorContext editorContext, InstanceID ID, out GameObject gameObject)
		{
			PropInstance instanceByID4 = getInstanceByID(editorContext.propInstances, ID);
			gameObject = ((instanceByID4 != null) ? instanceByID4.gameObject : null);
			return gameObject != null;
		}
	}

	private static void changePropComponent<TComponent, TData>(PropData prev, PropData next, PropInstance instance, Func<PropData, TData> getData, Action<TComponent, TData, bool> sync) where TComponent : Component
	{
		TData val = getData(prev);
		TData val2 = getData(next);
		TComponent component;
		if (val != null && val2 != null)
		{
			sync(instance.GetComponent<TComponent>(), val2, arg3: false);
		}
		else if (val2 != null)
		{
			sync(instance.GetOrAddComponent<TComponent>(), val2, arg3: true);
		}
		else if (instance.TryGetComponent<TComponent>(out component))
		{
			UnityEngine.Object.DestroyImmediate(component);
		}
	}

	private static PropID getPropPrefabID(PropInstance instance)
	{
		if (instance.TryGetComponent<PropIDLink>(out var component))
		{
			return component.ID;
		}
		return new PropID
		{
			value = instance.name
		};
	}

	private static List<LockArgument> safeLocks(List<LockArgument> locks)
	{
		return locks ?? new List<LockArgument>();
	}

	private static Predicate<LockArgument> lockPredicate(Lock _lock, int passwordIndex)
	{
		return (LockArgument lockArg) => lockArg.targetLock == _lock && lockArg.targetIndex == passwordIndex;
	}

	private Rect rectAspectFit(Vector2 parentSize, Vector2 size)
	{
		float a = parentSize.x / size.x;
		float b = parentSize.y / size.y;
		float num = Mathf.Min(a, b);
		Vector2 vector = new Vector2(size.x * num, size.y * num);
		Vector2 vector2 = (parentSize - vector) * 0.5f;
		return new Rect(vector2.x, vector2.y, vector.x, vector.y);
	}

	private void updateCursor()
	{
		bool visible = Cursor.visible;
		CursorLockMode lockState = Cursor.lockState;
		MaterialAssetDragAndDropItem materialAssetDragAndDropItem = dragAndDropItem as MaterialAssetDragAndDropItem;
		bool flag = materialAssetDragAndDropItem != null;
		virtualCursorUI.MaterialDragIcon.gameObject.SetActive(flag);
		bool flag2 = cameraMouseControl == CamMouseControl.None && !flag;
		if (!flag2)
		{
			CamMouseControl camMouseControl = cameraMouseControl;
			if (camMouseControl == CamMouseControl.WASD || camMouseControl == CamMouseControl.Pan)
			{
				flag2 = rmbHeldState == RmbHeldState.ClickTesting;
			}
		}
		Cursor.visible = flag2;
		Cursor.lockState = ((!flag2) ? CursorLockMode.Confined : CursorLockMode.None);
		if (lockState == CursorLockMode.None && Cursor.lockState == CursorLockMode.Confined)
		{
			skipNextAxisInput = true;
		}
		bool flag3 = !flag2;
		virtualCursorUI.root.enabled = flag3;
		if (flag3)
		{
			if (visible != Cursor.visible)
			{
				virtualCursorPosition = Input.mousePosition;
			}
			else if (flag)
			{
				virtualCursorPosition = Input.mousePosition;
			}
			else
			{
				float mouseSensitivity = PlayerSave.getSettings().mouseSensitivity;
				virtualCursorPosition.x += Input.GetAxis("Mouse X") * mouseSensitivity;
				virtualCursorPosition.x %= Screen.width;
				if (virtualCursorPosition.x < 0f)
				{
					virtualCursorPosition.x += Screen.width;
				}
				virtualCursorPosition.y += Input.GetAxis("Mouse Y") * mouseSensitivity;
				virtualCursorPosition.y %= Screen.height;
				if (virtualCursorPosition.y < 0f)
				{
					virtualCursorPosition.y += Screen.height;
				}
			}
			virtualCursorUI.Icon.transform.position = virtualCursorPosition;
			virtualCursorUI.Icon.enabled = !flag;
			virtualCursorUI.MaterialDragIcon.transform.position = virtualCursorPosition;
			Vector2 sizeDelta = Vector2.one * ((materialAssetDragAndDropItem?.materialSwap == null) ? 10 : 40);
			virtualCursorUI.MaterialDragIcon.rectT().sizeDelta = sizeDelta;
			if (Input.mouseScrollDelta.y != 0f)
			{
				float y = Input.mouseScrollDelta.y;
				float num = ((!(y < 0f)) ? ((PlayerSave.getSettings().re.cameraSpeed >= 0.0499f) ? 0.05f : 0.01f) : ((PlayerSave.getSettings().re.cameraSpeed > 0.06f) ? 0.05f : 0.01f));
				float value = PlayerSave.getSettings().re.cameraSpeed + y * num;
				value = Mathf.Clamp(value, 0.01f, 4f);
				PlayerSave.getSettings().re.cameraSpeed = value;
				tweener.destroyTweens(virtualCursorUI.MovementSpeed.gameObject, performEvents: false, performFinalUpdate: false);
				virtualCursorUI.MovementSpeed_Text.text = value.ToString("0.00", CultureInfo.InvariantCulture) + "x";
				virtualCursorUI.MovementSpeed.gameObject.SetActive(value: true);
				tweener.tween(virtualCursorUI.MovementSpeed.gameObject, 0.6f, 0f, deactivate: false, activate: false, destroy: false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, delegate(float t)
				{
					virtualCursorUI.MovementSpeed.alpha = Mathf.Lerp(1f, 0.9f, t);
				}, delegate
				{
					tweener.tween(virtualCursorUI.MovementSpeed.gameObject, 0.6f, 0f, deactivate: false, activate: false, destroy: false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, delegate(float t)
					{
						virtualCursorUI.MovementSpeed.alpha = Mathf.Lerp(1f, 0f, t);
					}, delegate
					{
						virtualCursorUI.MovementSpeed.gameObject.SetActive(value: false);
					});
				});
			}
		}
		if (!flag3 && visible != Cursor.visible)
		{
			Mouse.current.WarpCursorPosition(virtualCursorPosition);
			tweener.destroyTweens(virtualCursorUI.MovementSpeed.gameObject, performEvents: false, performFinalUpdate: false);
			virtualCursorUI.MovementSpeed.gameObject.SetActive(value: false);
		}
	}

	private void setCurrentPostProcessingVolume(EditorPostProcessingData data)
	{
		Game.applyVolumeData(editorVolumeRefs, data);
		editorVolumeRefs.volume.enabled = true;
	}

	private void revertToDefaultPostProcessingSettings()
	{
		setCurrentPostProcessingVolume(editorPostProcessingDefault);
	}

	private static Vector3 revertMask(Vector3 mask)
	{
		mask.x = ((mask.x > 0.5f) ? 0f : 1f);
		mask.y = ((mask.y > 0.5f) ? 0f : 1f);
		mask.z = ((mask.z > 0.5f) ? 0f : 1f);
		return mask;
	}

	private static Vector3 vec3AddAlongAxis(Vector3 current, Vector3 add, Vector3 axis)
	{
		return Vector3.Scale(current, revertMask(axis)) + Vector3.Scale(add, axis);
	}

	private PropID getPropIDLinkForInstance(PropInstance instance)
	{
		PropID propID = getPropPrefabID(instance);
		Prop propByID = assets.getPropByID(propID);
		if (propByID != null && propByID.roots.Count > 0)
		{
			propID = propByID.roots[0];
		}
		if (assets.hiddenPropIDs.Contains(propID))
		{
			propID = PropID.None;
		}
		return propID;
	}

	private void enterMultiTargetMode(Predicate<PropInstance> targetPredicate, Action<HashSet<PropInstance>, HashSet<(PropInstance, int)>> onApply, PasswordPredicate passwordPredicate = null)
	{
		editorUI.root.enabled = false;
		targetModeUI.root.enabled = true;
		enterSpecialMode(EditorMode.Target);
		special.targetModeApplyMultiple = onApply;
		special.multiTarget = true;
		special.targets = new HashSet<PropInstance>();
		special.lockPasswordIndexes = new HashSet<(PropInstance, int)>();
		special.passwordPopups = new List<PasswordPopup>();
		cancelSelectionWithRestoreFlag();
		foreach (KeyValuePair<InstanceID, PropInstance> propInstance in propInstances)
		{
			PropInstance value = propInstance.Value;
			if (targetPredicate(value))
			{
				addClickableIcon(passwordPredicate, value, multiSelect: true);
			}
		}
		special.raycastPredicate = (PropInstance pi) => special.targetPins.Exists((TargetPin pin) => pin.transform == pi.transform);
	}

	private void enterTargetMode(Predicate<PropInstance> targetPredicate, PasswordPredicate passwordPredicate, Action<PropInstance, int> onApply)
	{
		editorUI.root.enabled = false;
		targetModeUI.root.enabled = true;
		enterSpecialMode(EditorMode.Target);
		special.lockPasswordIndex = -1;
		special.targetModeApply = onApply;
		special.passwordPopups = new List<PasswordPopup>();
		cancelSelectionWithRestoreFlag();
		foreach (KeyValuePair<InstanceID, PropInstance> propInstance in propInstances)
		{
			PropInstance value = propInstance.Value;
			if (targetPredicate(value))
			{
				addClickableIcon(passwordPredicate, value);
			}
		}
		special.raycastPredicate = (PropInstance pi) => special.targetPins.Exists((TargetPin pin) => pin.transform == pi.transform);
	}

	private void exitTargetMode(bool apply = false)
	{
		editorUI.root.enabled = true;
		targetModeUI.root.enabled = false;
		restoreSelection();
		for (int i = 0; i < special.targetPins.Count; i++)
		{
			UnityEngine.Object.Destroy(special.targetPins[i].img);
		}
		int num = 0;
		while (special.passwordPopups != null && num < special.passwordPopups.Count)
		{
			UnityEngine.Object.Destroy(special.passwordPopups[num].pickerUI.root.gameObject);
			num++;
		}
		if (apply)
		{
			if (special.multiTarget)
			{
				(PropInstance, int) item = (null, -1);
				foreach (var lockPasswordIndex in special.lockPasswordIndexes)
				{
					var (propInstance, _) = lockPasswordIndex;
					if (lockPasswordIndex.Item2 != 0)
					{
						bool flag = propInstance.GetComponent<Teleport>() != null;
						bool flag2 = propInstance.GetComponent<Fog>() != null;
						bool flag3 = propInstance.GetComponent<Sound>() != null;
						bool flag4 = propInstance.GetComponent<EditorSkybox>() != null;
						bool flag5 = propInstance.GetComponent<EditorPostProcessing>() != null;
						if (flag || flag2 || flag3 || flag4 || flag5)
						{
							item = lockPasswordIndex;
						}
					}
				}
				if (item.Item1 != null)
				{
					special.lockPasswordIndexes.Remove(item);
					special.lockPasswordIndexes.Add((item.Item1, 0));
				}
				special.targetModeApplyMultiple(special.targets, special.lockPasswordIndexes);
			}
			else if (special.target != null)
			{
				bool num2 = special.target.GetComponent<Teleport>() != null;
				bool flag6 = special.target.GetComponent<Fog>() != null;
				bool flag7 = special.target.GetComponent<Sound>() != null;
				bool flag8 = special.target.GetComponent<EditorSkybox>() != null;
				bool flag9 = special.target.GetComponent<EditorPostProcessing>() != null;
				if (num2 || flag6 || flag7 || flag8 || flag9)
				{
					special.lockPasswordIndex = 0;
				}
				special.targetModeApply(special.target, special.lockPasswordIndex);
			}
		}
		exitSpecialMode();
	}

	private void playSoundPickerSound(string audioAssetFileName)
	{
		FMOD.System coreSystem = RuntimeManager.CoreSystem;
		RuntimeManager.GetBus("bus:/Sound Effects").getChannelGroup(out var group);
		if (coreSystem.createSound(Path.Combine(roomDirPath, audioAssetFileName), MODE.DEFAULT, out var sound) == RESULT.OK)
		{
			soundPicker.playingSound = sound;
			if (coreSystem.playSound(sound, group, paused: false, out var channel) == RESULT.OK)
			{
				soundPicker.playingChannel = channel;
			}
		}
	}

	private void closeSoundPickerUI()
	{
		soundPickerUI.root.enabled = false;
		for (int num = soundPickerUI.Content.childCount - 1; num >= 3; num--)
		{
			UnityEngine.Object.Destroy(soundPickerUI.Content.GetChild(num).gameObject);
		}
		soundPicker.files = new List<string>();
		stopFMODTestSound();
	}

	private void stopFMODTestSound()
	{
		if (PineFmod.isValid(soundPicker.playingInstance))
		{
			PineFmod.stop(soundPicker.playingInstance, FMOD.Studio.STOP_MODE.IMMEDIATE);
			PineFmod.release(soundPicker.playingInstance);
			PineFmod.clearHandle(soundPicker.playingInstance);
		}
		if (PineFmod.hasHandle(soundPicker.playingChannel))
		{
			PineFmod.stop(soundPicker.playingChannel);
		}
		if (PineFmod.hasHandle(soundPicker.playingSound))
		{
			PineFmod.release(soundPicker.playingSound);
		}
	}

	private void startPropertySection(Transform parent, UITooltip.Position tooltipPosition)
	{
		propertiesParent = parent;
		propertiesTooltipPosition = tooltipPosition;
	}

	private void defineIntProperty(string label, string tooltip, Func<int> getter, Action<int> setter, Action<int, int> onSubmit, Action<int, int> onChange = null, int min = int.MinValue, int max = int.MaxValue, float dragPixelToUnit = 1f)
	{
		label = tryGetLocalized(label);
		tooltip = tryGetLocalized(tooltip);
		IntPropertyUI ui = UnityEngine.Object.Instantiate(propertiesUI.IntProperty, propertiesParent);
		ui.gameObject.SetActive(value: true);
		ui.Label_Text.text = label;
		setHoverTooltip(tooltip, ui.Label, propertiesTooltipPosition);
		bool isSlider = min != int.MinValue && max != int.MaxValue;
		bool isSliderBeingDragged = false;
		ui.InputField_Input.name = label + "-Int" + (isSlider ? "Range" : "");
		ui.Slider.gameObject.SetActive(isSlider);
		ui.InputField_Drag.gameObject.SetActive(!isSlider);
		if (isSlider)
		{
			ui.Slider.wholeNumbers = true;
			ui.Slider.minValue = min;
			ui.Slider.maxValue = max;
			ui.InputField_Input.textComponent.alignment = TextAnchor.MiddleCenter;
		}
		int editStartValue = 0;
		PropertyData property = registerProperty(ui, onUpdate, delegate
		{
			setValue(editStartValue);
		});
		addTriggerEntry(ui.InputField_Input.gameObject, EventTriggerType.Select).callback.AddListener(delegate
		{
			InputFieldNavigationData.currentFocusedInputFieldName = ui.InputField_Input.name;
			editStartValue = getter();
		});
		ui.InputField_Input.onValueChanged.AddListener(delegate
		{
			if (int.TryParse(ui.InputField_Input.text, out var result))
			{
				setValue(result);
			}
		});
		ui.InputField_Input.onEndEdit.AddListener(delegate
		{
			InputFieldNavigationData.currentFocusedInputFieldName = "";
			if (!ui.InputField_Input.wasCanceled)
			{
				int num = getter();
				if (editStartValue != num)
				{
					onSubmit(editStartValue, num);
					editStartValue = num;
				}
			}
		});
		if (isSlider)
		{
			addTriggerEntry(ui.Slider.gameObject, EventTriggerType.BeginDrag).callback.AddListener(delegate(BaseEventData data)
			{
				editStartValue = getter();
				draggedProperty = property;
				draggedProperty.dragData = (PointerEventData)data;
				isSliderBeingDragged = true;
			});
			ui.Slider.onValueChanged.AddListener(delegate(float value)
			{
				setValue((int)value);
			});
			addTriggerEntry(ui.Slider.gameObject, EventTriggerType.EndDrag).callback.AddListener(delegate
			{
				if (getter() != editStartValue)
				{
					onSubmit(editStartValue, getter());
				}
				draggedProperty = null;
				isSliderBeingDragged = false;
			});
			return;
		}
		addTriggerEntry(ui.InputField_Drag.gameObject, EventTriggerType.BeginDrag).callback.AddListener(delegate(BaseEventData data)
		{
			editStartValue = getter();
			draggedProperty = property;
			draggedProperty.dragData = (PointerEventData)data;
		});
		float accumulatedDelta = 0f;
		addTriggerEntry(ui.InputField_Drag.gameObject, EventTriggerType.Drag).callback.AddListener(delegate(BaseEventData data)
		{
			accumulatedDelta += ((PointerEventData)data).delta.x * dragPixelToUnit;
			if (!(Mathf.Abs(accumulatedDelta) < 1f))
			{
				int num = (int)accumulatedDelta;
				setValue(getter() + num);
				accumulatedDelta -= num;
			}
		});
		addTriggerEntry(ui.InputField_Drag.gameObject, EventTriggerType.EndDrag).callback.AddListener(delegate
		{
			if (getter() != editStartValue)
			{
				onSubmit(editStartValue, getter());
			}
			draggedProperty = null;
		});
		void onUpdate()
		{
			if (isSlider && !isSliderBeingDragged)
			{
				ui.Slider.SetValueWithoutNotify(getter());
			}
			if (!ui.InputField_Input.isFocused)
			{
				setIntPropertyText(ui.InputField_Input, getter());
			}
		}
		void setValue(int value)
		{
			int arg = getter();
			int num = Mathf.Clamp(value, min, max);
			setter(num);
			onChange?.Invoke(arg, num);
		}
	}

	private void defineFloatProperty(string label, string tooltip, Func<float> getter, Action<float> setter, Action<float, float> onSubmit, Action<float, float> onChange = null, float min = float.NegativeInfinity, float max = float.PositiveInfinity, float dragPixelToUnit = 1f, int decimals = 3)
	{
		label = tryGetLocalized(label);
		tooltip = tryGetLocalized(tooltip);
		FloatPropertyUI ui = UnityEngine.Object.Instantiate(propertiesUI.FloatProperty, propertiesParent);
		ui.gameObject.SetActive(value: true);
		ui.Label_Text.text = label;
		setHoverTooltip(tooltip, ui.Label, propertiesTooltipPosition);
		bool isSlider = !float.IsNegativeInfinity(min) && !float.IsPositiveInfinity(max);
		bool isSliderBeingDragged = false;
		ui.InputField_Input.name = label + "-Float" + (isSlider ? "Range" : "");
		ui.Slider.gameObject.SetActive(isSlider);
		ui.InputField_Drag.gameObject.SetActive(!isSlider);
		if (isSlider)
		{
			ui.Slider.wholeNumbers = false;
			ui.Slider.minValue = min;
			ui.Slider.maxValue = max;
			ui.InputField_Input.textComponent.alignment = TextAnchor.MiddleCenter;
		}
		float editStartValue = 0f;
		PropertyData property = registerProperty(ui, onUpdate, delegate
		{
			setValue(editStartValue);
		});
		addTriggerEntry(ui.InputField_Input.gameObject, EventTriggerType.Select).callback.AddListener(delegate
		{
			InputFieldNavigationData.currentFocusedInputFieldName = ui.InputField_Input.name;
			editStartValue = getter();
		});
		ui.InputField_Input.onValueChanged.AddListener(delegate
		{
			if (tryParseFloat(ui.InputField_Input.text, out var value))
			{
				setValue(value);
			}
		});
		ui.InputField_Input.onEndEdit.AddListener(delegate
		{
			InputFieldNavigationData.currentFocusedInputFieldName = "";
			if (!ui.InputField_Input.wasCanceled)
			{
				float num = getter();
				if (editStartValue != num)
				{
					onSubmit(editStartValue, num);
					editStartValue = num;
				}
			}
		});
		if (isSlider)
		{
			addTriggerEntry(ui.Slider.gameObject, EventTriggerType.BeginDrag).callback.AddListener(delegate(BaseEventData data)
			{
				editStartValue = getter();
				draggedProperty = property;
				draggedProperty.dragData = (PointerEventData)data;
				isSliderBeingDragged = true;
			});
			ui.Slider.onValueChanged.AddListener(setValue);
			addTriggerEntry(ui.Slider.gameObject, EventTriggerType.EndDrag).callback.AddListener(delegate
			{
				if (getter() != editStartValue)
				{
					onSubmit(editStartValue, getter());
				}
				draggedProperty = null;
				isSliderBeingDragged = false;
			});
			return;
		}
		addTriggerEntry(ui.InputField_Drag.gameObject, EventTriggerType.BeginDrag).callback.AddListener(delegate(BaseEventData data)
		{
			editStartValue = getter();
			draggedProperty = property;
			draggedProperty.dragData = (PointerEventData)data;
		});
		addTriggerEntry(ui.InputField_Drag.gameObject, EventTriggerType.Drag).callback.AddListener(delegate(BaseEventData data)
		{
			float num = ((PointerEventData)data).delta.x * dragPixelToUnit;
			setValue(getter() + num);
		});
		addTriggerEntry(ui.InputField_Drag.gameObject, EventTriggerType.EndDrag).callback.AddListener(delegate
		{
			if (getter() != editStartValue)
			{
				onSubmit(editStartValue, getter());
			}
			draggedProperty = null;
		});
		void onUpdate()
		{
			if (isSlider && !isSliderBeingDragged)
			{
				ui.Slider.SetValueWithoutNotify(getter());
			}
			if (!ui.InputField_Input.isFocused)
			{
				setFloatPropertyText(ui.InputField_Input, getter(), decimals);
			}
		}
		void setValue(float value)
		{
			float arg = getter();
			float num = Mathf.Clamp(value, min, max);
			setter(num);
			onChange?.Invoke(arg, num);
		}
	}

	private void defineVector2Property(string label, string tooltip, Func<Vector2> getter, Action<Vector2> setter, Action<Vector2, Vector2> onSubmit, Action<Vector2, Vector2> onChange = null, float dragPixelToUnit = 1f)
	{
		label = tryGetLocalized(label);
		tooltip = tryGetLocalized(tooltip);
		Vector2PropertyUI ui = UnityEngine.Object.Instantiate(propertiesUI.Vector2Property, propertiesParent);
		ui.gameObject.SetActive(value: true);
		ui.Label_Text.text = label;
		setHoverTooltip(tooltip, ui.Label, propertiesTooltipPosition);
		ui.XValue_Input.name = label + "-X";
		ui.YValue_Input.name = label + "-Y";
		Vector2 editStartValue = Vector2.zero;
		PropertyData property = registerProperty(ui, onUpdate, delegate
		{
			setValue(editStartValue);
		});
		addInputFieldHandlers(ui.XValue_Input);
		addInputFieldHandlers(ui.YValue_Input);
		addDragGizmoHandlers(ui.XValue_Drag, new Vector2(1f, 0f));
		addDragGizmoHandlers(ui.YValue_Drag, new Vector2(0f, 1f));
		void addDragGizmoHandlers(Button button, Vector2 axis)
		{
			addTriggerEntry(button.gameObject, EventTriggerType.BeginDrag).callback.AddListener(delegate(BaseEventData data)
			{
				editStartValue = getter();
				draggedProperty = property;
				draggedProperty.dragData = (PointerEventData)data;
			});
			addTriggerEntry(button.gameObject, EventTriggerType.Drag).callback.AddListener(delegate(BaseEventData data)
			{
				Vector2 vector = ((PointerEventData)data).delta.x * dragPixelToUnit * axis;
				setValue(getter() + vector);
			});
			addTriggerEntry(button.gameObject, EventTriggerType.EndDrag).callback.AddListener(delegate
			{
				if (getter() != editStartValue)
				{
					onSubmit(editStartValue, getter());
				}
				draggedProperty = null;
			});
		}
		void addInputFieldHandlers(InputField field)
		{
			addTriggerEntry(field.gameObject, EventTriggerType.Select).callback.AddListener(delegate
			{
				InputFieldNavigationData.currentFocusedInputFieldName = field.name;
				editStartValue = getter();
			});
			field.onValueChanged.AddListener(delegate
			{
				Vector2 value = getter();
				if (tryParseFloat(ui.XValue_Input.text, out var value2))
				{
					value.x = value2;
				}
				if (tryParseFloat(ui.YValue_Input.text, out var value3))
				{
					value.y = value3;
				}
				setValue(value);
			});
			field.onEndEdit.AddListener(delegate
			{
				InputFieldNavigationData.currentFocusedInputFieldName = "";
				if (!field.wasCanceled)
				{
					Vector2 vector = getter();
					if (!(editStartValue == vector))
					{
						onSubmit(editStartValue, vector);
						editStartValue = vector;
					}
				}
			});
		}
		void onUpdate()
		{
			if (!ui.XValue_Input.isFocused && !ui.YValue_Input.isFocused)
			{
				setVector2PropertyText(ui.XValue_Input, ui.YValue_Input, getter());
			}
		}
		void setValue(Vector2 value)
		{
			Vector2 arg = getter();
			setter(value);
			onChange?.Invoke(arg, value);
		}
	}

	private void defineVector3Property(string label, string tooltip, Func<Vector3> getter, Action<Vector3> setter, Action<Vector3, Vector3> onSubmit, Action<Vector3, Vector3> onChange = null, float dragPixelToUnit = 1f)
	{
		label = tryGetLocalized(label);
		tooltip = tryGetLocalized(tooltip);
		Vector3PropertyUI ui = UnityEngine.Object.Instantiate(propertiesUI.Vector3Property, propertiesParent);
		ui.gameObject.SetActive(value: true);
		ui.Label_Text.text = label;
		setHoverTooltip(tooltip, ui.Label, propertiesTooltipPosition);
		ui.XValue_Input.name = label + "-X";
		ui.YValue_Input.name = label + "-Y";
		ui.ZValue_Input.name = label + "-Z";
		Vector3 editStartValue = Vector3.zero;
		PropertyData property = registerProperty(ui, onUpdate, delegate
		{
			setValue(editStartValue);
		});
		addInputFieldHandlers(ui.XValue_Input);
		addInputFieldHandlers(ui.YValue_Input);
		addInputFieldHandlers(ui.ZValue_Input);
		addDragGizmoHandlers(ui.XValue_Drag, new Vector3(1f, 0f, 0f));
		addDragGizmoHandlers(ui.YValue_Drag, new Vector3(0f, 1f, 0f));
		addDragGizmoHandlers(ui.ZValue_Drag, new Vector3(0f, 0f, 1f));
		void addDragGizmoHandlers(Button button, Vector3 axis)
		{
			addTriggerEntry(button.gameObject, EventTriggerType.BeginDrag).callback.AddListener(delegate(BaseEventData data)
			{
				editStartValue = getter();
				draggedProperty = property;
				draggedProperty.dragData = (PointerEventData)data;
			});
			addTriggerEntry(button.gameObject, EventTriggerType.Drag).callback.AddListener(delegate(BaseEventData data)
			{
				Vector3 vector = ((PointerEventData)data).delta.x * dragPixelToUnit * axis;
				setValue(getter() + vector);
			});
			addTriggerEntry(button.gameObject, EventTriggerType.EndDrag).callback.AddListener(delegate
			{
				if (getter() != editStartValue)
				{
					onSubmit(editStartValue, getter());
				}
				draggedProperty = null;
			});
		}
		void addInputFieldHandlers(InputField field)
		{
			addTriggerEntry(field.gameObject, EventTriggerType.Select).callback.AddListener(delegate
			{
				InputFieldNavigationData.currentFocusedInputFieldName = field.name;
				editStartValue = getter();
			});
			field.onValueChanged.AddListener(delegate
			{
				Vector3 value = getter();
				if (tryParseFloat(ui.XValue_Input.text, out var value2))
				{
					value.x = value2;
				}
				if (tryParseFloat(ui.YValue_Input.text, out var value3))
				{
					value.y = value3;
				}
				if (tryParseFloat(ui.ZValue_Input.text, out var value4))
				{
					value.z = value4;
				}
				setValue(value);
			});
			field.onEndEdit.AddListener(delegate
			{
				InputFieldNavigationData.currentFocusedInputFieldName = "";
				if (!field.wasCanceled)
				{
					Vector3 vector = getter();
					if (!(editStartValue == vector))
					{
						onSubmit(editStartValue, vector);
						editStartValue = vector;
					}
				}
			});
		}
		void onUpdate()
		{
			if (!ui.XValue_Input.isFocused && !ui.YValue_Input.isFocused && !ui.ZValue_Input.isFocused)
			{
				setVector3PropertyText(ui.XValue_Input, ui.YValue_Input, ui.ZValue_Input, getter());
			}
		}
		void setValue(Vector3 value)
		{
			Vector3 arg = getter();
			setter(value);
			onChange?.Invoke(arg, value);
		}
	}

	private void defineVector3Property(string label, string tooltip, Action onStoreValue, Action<float, Vector3> onDrag, Action<Vector3, Vector3> onModify, Action onCancel, Func<bool> onApply, Action<Vector3PropertyUI> onSyncUI, float dragPixelToUnit = 1f)
	{
		label = tryGetLocalized(label);
		tooltip = tryGetLocalized(tooltip);
		Vector3PropertyUI ui = UnityEngine.Object.Instantiate(propertiesUI.Vector3Property, propertiesParent);
		ui.gameObject.SetActive(value: true);
		ui.Label_Text.text = label;
		setHoverTooltip(tooltip, ui.Label, propertiesTooltipPosition);
		ui.XValue_Input.name = label + "-X";
		ui.YValue_Input.name = label + "-Y";
		ui.ZValue_Input.name = label + "-Z";
		PropertyData property = registerProperty(ui, onUpdate, onCancel);
		addSelectionTriggers(ui.XValue_Input);
		addSelectionTriggers(ui.YValue_Input);
		addSelectionTriggers(ui.ZValue_Input);
		addDragTriggers(ui.XValue_Drag, new Vector3(1f, 0f, 0f));
		addDragTriggers(ui.YValue_Drag, new Vector3(0f, 1f, 0f));
		addDragTriggers(ui.ZValue_Drag, new Vector3(0f, 0f, 1f));
		void addDragTriggers(Button button, Vector3 axis)
		{
			addTriggerEntry(button.gameObject, EventTriggerType.BeginDrag).callback.AddListener(delegate(BaseEventData data)
			{
				onStoreValue();
				draggedProperty = property;
				draggedProperty.dragData = (PointerEventData)data;
			});
			addTriggerEntry(button.gameObject, EventTriggerType.Drag).callback.AddListener(delegate(BaseEventData data)
			{
				PointerEventData pointerEventData = (PointerEventData)data;
				onDrag(pointerEventData.delta.x * dragPixelToUnit, axis);
			});
			addTriggerEntry(button.gameObject, EventTriggerType.EndDrag).callback.AddListener(delegate
			{
				onApply();
				draggedProperty = null;
			});
		}
		void addSelectionTriggers(InputField field)
		{
			addTriggerEntry(field.gameObject, EventTriggerType.Select).callback.AddListener(delegate
			{
				InputFieldNavigationData.currentFocusedInputFieldName = field.name;
				onStoreValue();
			});
			field.onEndEdit.AddListener(delegate
			{
				InputFieldNavigationData.currentFocusedInputFieldName = "";
				if (field.wasCanceled)
				{
					onCancel();
				}
				else
				{
					onApply();
				}
			});
		}
		void getValueAxis(out Vector3 value, out Vector3 axis)
		{
			value = Vector3.zero;
			axis = Vector3.zero;
			if (tryParseFloat(ui.XValue_Input.text, out var value2))
			{
				value.x = value2;
				axis.x = 1f;
			}
			if (tryParseFloat(ui.YValue_Input.text, out var value3))
			{
				value.y = value3;
				axis.y = 1f;
			}
			if (tryParseFloat(ui.ZValue_Input.text, out var value4))
			{
				value.z = value4;
				axis.z = 1f;
			}
		}
		void onUpdate()
		{
			if (ui.XValue_Input.isFocused || ui.YValue_Input.isFocused || ui.ZValue_Input.isFocused)
			{
				getValueAxis(out var value, out var axis);
				onModify(value, axis);
			}
			else
			{
				onSyncUI(ui);
			}
		}
	}

	private void defineStringProperty(string label, string tooltip, Func<string> getter, Action<string> setter, Action<string, string> onSubmit, Action<string, string> onChange = null, Predicate<string> onValidate = null, InputField.LineType lineType = InputField.LineType.SingleLine)
	{
		label = tryGetLocalized(label);
		tooltip = tryGetLocalized(tooltip);
		StringPropertyUI ui = UnityEngine.Object.Instantiate(propertiesUI.StringProperty, propertiesParent);
		ui.gameObject.SetActive(value: true);
		ui.Label_Text.text = label;
		setHoverTooltip(tooltip, ui.Label, propertiesTooltipPosition);
		ui.Input.name = label + "-String";
		ui.Input.lineType = lineType;
		string editStartValue = string.Empty;
		registerProperty(ui, onUpdate, delegate
		{
			setValue(editStartValue);
		});
		ui.Input.SetTextWithoutNotify(getter());
		calculateMultilineHeight();
		addTriggerEntry(ui.Input.gameObject, EventTriggerType.Select).callback.AddListener(delegate
		{
			InputFieldNavigationData.currentFocusedInputFieldName = ui.Input.name;
			editStartValue = getter();
		});
		ui.Input.onValueChanged.AddListener(delegate
		{
			setValue(ui.Input.text);
		});
		ui.Input.onEndEdit.AddListener(delegate
		{
			InputFieldNavigationData.currentFocusedInputFieldName = "";
			if (!ui.Input.wasCanceled)
			{
				string text = getter();
				if (!(editStartValue == text))
				{
					onSubmit(editStartValue, text);
					editStartValue = text;
				}
			}
		});
		void calculateMultilineHeight()
		{
			if (lineType != InputField.LineType.SingleLine)
			{
				RectTransform component = ui.Input.transform.parent.GetComponent<RectTransform>();
				component.SetHeight(1000000f);
				float num = Mathf.Abs(ui.Input.textComponent.rectT().offsetMin.y);
				float num2 = Mathf.Abs(ui.Input.textComponent.rectT().offsetMax.y);
				component.SetHeight(Mathf.Max(30f, ui.Input.textComponent.preferredHeight + num + num2));
			}
		}
		void onUpdate()
		{
			string text = getter();
			bool flag = onValidate == null || onValidate(text);
			ui.Input.textComponent.color = (flag ? Color.white : Color.red);
			calculateMultilineHeight();
			if (!ui.Input.isFocused)
			{
				ui.Input.SetTextWithoutNotify(text);
			}
		}
		void setValue(string value)
		{
			string arg = getter();
			setter(value);
			onChange?.Invoke(arg, value);
		}
	}

	private void defineStringProperty(string label, string tooltip, Action onStoreValue, Action<string> onModify, Action onCancel, Func<bool> onApply, Action<StringPropertyUI> onSyncUI)
	{
		label = tryGetLocalized(label);
		tooltip = tryGetLocalized(tooltip);
		StringPropertyUI ui = UnityEngine.Object.Instantiate(propertiesUI.StringProperty, propertiesParent);
		ui.gameObject.SetActive(value: true);
		ui.Label_Text.text = label;
		setHoverTooltip(tooltip, ui.Label, propertiesTooltipPosition);
		ui.Input.name = label + "-String";
		registerProperty(ui, onUpdate, onCancel);
		addTriggerEntry(ui.Input.gameObject, EventTriggerType.Select).callback.AddListener(delegate
		{
			InputFieldNavigationData.currentFocusedInputFieldName = ui.Input.name;
			onStoreValue();
		});
		ui.Input.onEndEdit.AddListener(delegate
		{
			InputFieldNavigationData.currentFocusedInputFieldName = "";
			if (ui.Input.wasCanceled)
			{
				onCancel();
			}
			else
			{
				onApply();
			}
		});
		void onUpdate()
		{
			if (ui.Input.isFocused)
			{
				onModify(ui.Input.text);
			}
			else
			{
				onSyncUI(ui);
			}
		}
	}

	private void defineTextureProperty(string label, string tooltip, UnityEngine.Texture texture, Action onTexturePressed, Action onEditPressed = null, Action onRevertPressed = null, string text = "", Color? textureColor = null)
	{
		label = tryGetLocalized(label);
		tooltip = tryGetLocalized(tooltip);
		TexturePropertyUI texturePropertyUI = UnityEngine.Object.Instantiate(propertiesUI.TextureProperty, propertiesParent);
		texturePropertyUI.gameObject.SetActive(value: true);
		texturePropertyUI.Label_Text.text = label;
		setHoverTooltip(tooltip, texturePropertyUI.Label, propertiesTooltipPosition);
		registerProperty(texturePropertyUI, null, null);
		RawImage component = texturePropertyUI.TexturePreview.GetComponent<RawImage>();
		component.texture = texture;
		if (textureColor.HasValue)
		{
			component.color = textureColor.Value;
		}
		texturePropertyUI.TexturePreview.onClick.RemoveAllListeners();
		texturePropertyUI.TexturePreview.onClick.AddListener(delegate
		{
			onTexturePressed();
		});
		if (onEditPressed != null)
		{
			texturePropertyUI.TextureEdit.gameObject.SetActive(value: true);
			texturePropertyUI.TextureEdit.onClick.RemoveAllListeners();
			texturePropertyUI.TextureEdit.onClick.AddListener(delegate
			{
				onEditPressed();
			});
		}
		else
		{
			texturePropertyUI.TextureEdit.gameObject.SetActive(value: false);
		}
		if (onRevertPressed != null)
		{
			texturePropertyUI.TextureRevert.gameObject.SetActive(value: true);
			texturePropertyUI.TextureRevert.onClick.RemoveAllListeners();
			texturePropertyUI.TextureRevert.onClick.AddListener(delegate
			{
				onRevertPressed();
			});
		}
		else
		{
			texturePropertyUI.TextureRevert.gameObject.SetActive(value: false);
		}
		texturePropertyUI.TextureName.text = text;
	}

	private void defineColorProperty(string label, string tooltip, Func<Color> getter, Action<Color> setter, Action<Color, Color> onSubmit)
	{
		label = tryGetLocalized(label);
		tooltip = tryGetLocalized(tooltip);
		ColorPropertyUI ui = UnityEngine.Object.Instantiate(propertiesUI.ColorProperty, propertiesParent);
		ui.gameObject.SetActive(value: true);
		ui.Label_Text.text = label;
		setHoverTooltip(tooltip, ui.Label, propertiesTooltipPosition);
		Color initialColor = getter();
		setColor(initialColor, invokeSetter: false);
		bool isColorPickerOpen = false;
		Color prevColorPickerColor = initialColor;
		PropertyData property = registerProperty(ui, onUpdate, onCancel);
		ui.Button.onClick.RemoveAllListeners();
		ui.Button.onClick.AddListener(delegate
		{
			initialColor = getter();
			isColorPickerOpen = true;
			colorPickerProperty = property;
			colorPickerUI.ColorPicker.A.Set(ui.Button_Color.color.a);
			colorPickerUI.ColorPicker.SetColor(ui.Button_Color.color);
			colorPickerUI.gameObject.SetActive(value: true);
			colorPickerUI.Background.onClick.RemoveAllListeners();
			colorPickerUI.Background.onClick.AddListener(closePickerAndSubmitColor);
			colorPickerUI.ColorPicker_QuitButton.onClick.RemoveAllListeners();
			colorPickerUI.ColorPicker_QuitButton.onClick.AddListener(closePickerAndSubmitColor);
		});
		void closePicker()
		{
			colorPickerUI.gameObject.SetActive(value: false);
			colorPickerProperty = null;
			isColorPickerOpen = false;
		}
		void closePickerAndSubmitColor()
		{
			closePicker();
			Color color = colorPickerUI.ColorPicker.Color;
			if (color != initialColor)
			{
				onSubmit(initialColor, color);
			}
		}
		void onCancel()
		{
			closePicker();
			setColor(initialColor, colorPickerUI.ColorPicker.Color != initialColor);
		}
		void onUpdate()
		{
			if (colorPickerUI.gameObject.activeSelf && isColorPickerOpen)
			{
				Color color = colorPickerUI.ColorPicker.Color;
				if (!(color == prevColorPickerColor))
				{
					setColor(color, invokeSetter: true);
					prevColorPickerColor = color;
				}
			}
		}
		void setColor(Color color, bool invokeSetter)
		{
			if (invokeSetter)
			{
				setter(color);
			}
			ui.Button_Color.color = color;
			ui.Button_Color_Transparency.fillAmount = color.a;
		}
	}

	private void defineBoolProperty(string label, string tooltip, bool? initValue, Action<bool> onSubmit)
	{
		label = tryGetLocalized(label);
		tooltip = tryGetLocalized(tooltip);
		TogglePropertyUI togglePropertyUI = UnityEngine.Object.Instantiate(propertiesUI.BoolProperty, propertiesParent);
		togglePropertyUI.gameObject.SetActive(value: true);
		togglePropertyUI.Label_Text.text = label;
		setHoverTooltip(tooltip, togglePropertyUI.Label, propertiesTooltipPosition);
		togglePropertyUI.Toggle.isOn = initValue == true;
		togglePropertyUI.Toggle.interactable = onSubmit != null;
		togglePropertyUI.Toggle.name = label + "-Bool";
		if (!initValue.HasValue)
		{
			togglePropertyUI.Toggle_Background_Checkmark.enabled = false;
			togglePropertyUI.Toggle_Background_Semicheck.enabled = true;
			togglePropertyUI.Toggle.graphic = togglePropertyUI.Toggle_Background_Semicheck;
		}
		registerProperty(togglePropertyUI, null, null);
		togglePropertyUI.Toggle.onValueChanged.RemoveAllListeners();
		togglePropertyUI.Toggle.onValueChanged.AddListener(delegate(bool value)
		{
			onSubmit?.Invoke(!initValue.HasValue || value);
		});
	}

	private void defineEnumProperty(string label, string tooltip, int initValue, List<string> options, Action<int> onSubmit, bool useInterpolationPopup = false, List<string> optionsTooltips = null)
	{
		label = tryGetLocalized(label);
		tooltip = tryGetLocalized(tooltip);
		EnumPropertyUI enumPropertyUI = UnityEngine.Object.Instantiate(propertiesUI.EnumProperty, propertiesParent);
		if (!useInterpolationPopup)
		{
			enumPropertyUI.Dropdown.template.GetComponentInChildren<InterpolationDropdownItem>().popup = null;
		}
		if (optionsTooltips == null)
		{
			UnityEngine.Object.Destroy(enumPropertyUI.Dropdown.template.GetComponentInChildren<UITooltip>());
		}
		else
		{
			enumPropertyUI.Dropdown.GetComponent<DropdownTooltips>().tooltips = optionsTooltips;
		}
		enumPropertyUI.gameObject.SetActive(value: true);
		enumPropertyUI.Label_Text.text = label;
		setHoverTooltip(tooltip, enumPropertyUI.Label, propertiesTooltipPosition);
		enumPropertyUI.Dropdown.ClearOptions();
		foreach (string option in options)
		{
			enumPropertyUI.Dropdown.options.Add(new Dropdown.OptionData
			{
				text = option
			});
		}
		enumPropertyUI.Dropdown.value = initValue;
		enumPropertyUI.Dropdown.interactable = options.Count > 0;
		registerProperty(enumPropertyUI, null, null);
		enumPropertyUI.Dropdown.onValueChanged.RemoveAllListeners();
		enumPropertyUI.Dropdown.onValueChanged.AddListener(delegate(int value)
		{
			onSubmit(value);
		});
	}

	private HintPropertyUI defineHintProperty(string label, string tooltip = "")
	{
		label = tryGetLocalized(label);
		tooltip = tryGetLocalized(tooltip);
		HintPropertyUI hintPropertyUI = UnityEngine.Object.Instantiate(propertiesUI.HintProperty, propertiesParent);
		hintPropertyUI.gameObject.SetActive(value: true);
		hintPropertyUI.Text.text = label;
		setHoverTooltip(tooltip, hintPropertyUI, propertiesTooltipPosition);
		registerProperty(hintPropertyUI, null, null);
		return hintPropertyUI;
	}

	private void defineSeparatorProperty(float height = 1f, bool isEmpty = true)
	{
		PropertiesSeparatorUI propertiesSeparatorUI = UnityEngine.Object.Instantiate(propertiesUI.SeparatorProperty, propertiesParent);
		propertiesSeparatorUI.gameObject.SetActive(value: true);
		propertiesSeparatorUI.GetComponent<LayoutElement>().preferredHeight = height;
		propertiesSeparatorUI.root.color = (isEmpty ? Color.clear : Color.white);
		registerProperty(propertiesSeparatorUI, null, null);
	}

	private void defineHeaderProperty(string label, string tooltip, bool isShown, Action<bool> onFoldout, Color? backgroundColor = null)
	{
		label = tryGetLocalized(label);
		tooltip = tryGetLocalized(tooltip);
		HeaderPropertyUI headerPropertyUI = UnityEngine.Object.Instantiate(propertiesUI.HeaderProperty, propertiesParent);
		headerPropertyUI.gameObject.SetActive(value: true);
		headerPropertyUI.Text.text = label;
		setHoverTooltip(tooltip, headerPropertyUI.Text, propertiesTooltipPosition);
		registerProperty(headerPropertyUI, null, null);
		if (backgroundColor.HasValue)
		{
			headerPropertyUI.root.color = backgroundColor.Value;
		}
		if (onFoldout == null)
		{
			headerPropertyUI.Fold.gameObject.SetActive(value: false);
			return;
		}
		headerPropertyUI.Fold_In.enabled = !isShown;
		headerPropertyUI.Fold_Out.enabled = isShown;
		headerPropertyUI.Fold.gameObject.SetActive(value: true);
		headerPropertyUI.Fold.onClick.RemoveAllListeners();
		Transform parent = propertiesParent;
		addTriggerEntry(headerPropertyUI.Fold.gameObject, EventTriggerType.PointerClick).callback.AddListener(delegate
		{
			toggleFold();
		});
		addTriggerEntry(headerPropertyUI.Text.gameObject, EventTriggerType.PointerClick).callback.AddListener(delegate
		{
			toggleFold();
		});
		void toggleFold()
		{
			onFoldout(!isShown);
			if (parent == null || parent == propertiesUI.Content)
			{
				refresh.propertiesUI = true;
			}
		}
	}

	private void defineFoldoutProperty(string label, string tooltip, bool isShown, Action<bool> onFoldout)
	{
		label = tryGetLocalized(label);
		tooltip = tryGetLocalized(tooltip);
		FoldoutPropertyUI foldoutPropertyUI = UnityEngine.Object.Instantiate(propertiesUI.FoldoutProperty, propertiesParent);
		foldoutPropertyUI.gameObject.SetActive(value: true);
		foldoutPropertyUI.Label.text = label;
		setHoverTooltip(tooltip, foldoutPropertyUI.Label, propertiesTooltipPosition);
		registerProperty(foldoutPropertyUI, null, null);
		foldoutPropertyUI.Folded.gameObject.SetActive(!isShown);
		foldoutPropertyUI.Unfolded.gameObject.SetActive(isShown);
		if (onFoldout == null)
		{
			return;
		}
		Transform parent = propertiesParent;
		foldoutPropertyUI.root.onClick.RemoveAllListeners();
		foldoutPropertyUI.root.onClick.AddListener(delegate
		{
			onFoldout(!isShown);
			if (parent == null || parent == propertiesUI.Content)
			{
				refresh.propertiesUI = true;
			}
		});
	}

	private void defineButtonProperty(string label, string tooltip, Action onClick)
	{
		label = tryGetLocalized(label);
		tooltip = tryGetLocalized(tooltip);
		ButtonPropertyUI buttonPropertyUI = UnityEngine.Object.Instantiate(propertiesUI.ButtonProperty, propertiesParent);
		buttonPropertyUI.gameObject.SetActive(value: true);
		buttonPropertyUI.Button_Text.text = label;
		setHoverTooltip(tooltip, buttonPropertyUI.Button, propertiesTooltipPosition);
		registerProperty(buttonPropertyUI, null, null);
		buttonPropertyUI.Button.name = label + "-Button";
		buttonPropertyUI.Button.onClick.RemoveAllListeners();
		buttonPropertyUI.Button.onClick.AddListener(delegate
		{
			onClick();
		});
	}

	private void defineTargetsProperty(string label, string tooltip, PrepChangeDataStrategy prepChangeDataStrategy, Action<PropData, PropData, PropInstance> onAdd, Action<PropData, PropData, int> onRemove, PropInstance target, Predicate<PropInstance> picker, List<PropInstance> currentSelected, int maxSelected)
	{
		label = tryGetLocalized(label);
		tooltip = tryGetLocalized(tooltip);
		List<TargetArrayEntry> list = new List<TargetArrayEntry>(currentSelected.Count);
		foreach (PropInstance item in currentSelected)
		{
			list.Add(getTargetArrayEntry(item.gameObject));
		}
		defineTargetArrayProperty(label, tooltip, list, maxSelected, delegate
		{
			enterMultiTargetMode(picker, delegate(HashSet<PropInstance> pis, HashSet<(PropInstance, int)> _)
			{
				foreach (PropInstance pi in pis)
				{
					PropData propData;
					PropData propData2;
					if (prepChangeDataStrategy == PrepChangeDataStrategy.UseSelected)
					{
						(propData, propData2) = prepChangeData(pi);
					}
					else
					{
						(propData, propData2) = prepChangeData(target);
					}
					onAdd(propData, propData2, pi);
					undo.push(actionChangeProp(propData, propData2, "Add Keys"));
				}
			});
		}, delegate(int index)
		{
			PropInstance propInstance = currentSelected[index];
			if (propInstance.TryGetComponent<PropInstance>(out var component))
			{
				undo.push(actionChangeSelection(newInstanceSelection(component.ID)));
				moveCameraTo(propInstance.gameObject);
			}
		}, delegate(int index)
		{
			PropData propData;
			PropData propData2;
			if (prepChangeDataStrategy != PrepChangeDataStrategy.UseSelected)
			{
				(propData, propData2) = prepChangeData(target);
			}
			else
			{
				(propData, propData2) = prepChangeData(currentSelected[index]);
			}
			onRemove(propData, propData2, index);
			undo.push(actionChangeProp(propData, propData2, "Remove Key"));
		});
	}

	private void defineTargetsProperty(string label, string tooltip, PropInstance target, Func<PropData, List<InstanceID>> getData, Predicate<PropInstance> picker, List<GameObject> targets)
	{
		label = tryGetLocalized(label);
		tooltip = tryGetLocalized(tooltip);
		List<TargetArrayEntry> list = new List<TargetArrayEntry>(targets.Count);
		foreach (GameObject target2 in targets)
		{
			list.Add(getTargetArrayEntry(target2));
		}
		defineTargetArrayProperty(label, tooltip, list, -1, delegate
		{
			enterMultiTargetMode(picker, delegate(HashSet<PropInstance> pis, HashSet<(PropInstance, int)> _)
			{
				var (prev, propData) = prepChangeData(target);
				foreach (PropInstance pi in pis)
				{
					getData(propData).Add(pi.ID);
				}
				undo.push(actionChangeProp(prev, propData, "Add Keys"));
			});
		}, delegate(int index)
		{
			GameObject gameObject = targets[index];
			if (gameObject.TryGetComponent<PropInstance>(out var component))
			{
				undo.push(actionChangeSelection(newInstanceSelection(component.ID)));
				moveCameraTo(gameObject);
			}
		}, delegate(int index)
		{
			var (prev, propData) = prepChangeData(target);
			getData(propData).RemoveAt(index);
			undo.push(actionChangeProp(prev, propData, "Remove Key"));
		});
	}

	private void defineTargetArrayProperty(string label, string tooltip, List<TargetArrayEntry> elements, int maxArrayCount, Action onAdd, Action<int> onElement, Action<int> onRemove)
	{
		label = tryGetLocalized(label);
		tooltip = tryGetLocalized(tooltip);
		TargetArrayPropertyUI targetArrayPropertyUI = UnityEngine.Object.Instantiate(propertiesUI.TargetArrayProperty, propertiesParent);
		targetArrayPropertyUI.gameObject.SetActive(value: true);
		targetArrayPropertyUI.Label_Text.text = label;
		setHoverTooltip(tooltip, targetArrayPropertyUI.Label, propertiesTooltipPosition);
		registerProperty(targetArrayPropertyUI, null, null);
		for (int i = 0; i < elements.Count; i++)
		{
			Transform parent = targetArrayPropertyUI.TargetArrayElement.transform.parent;
			TargetArrayElementUI targetArrayElementUI = UnityEngine.Object.Instantiate(targetArrayPropertyUI.TargetArrayElement, parent);
			targetArrayElementUI.gameObject.SetActive(value: true);
			int index = i;
			TargetArrayEntry targetArrayEntry = elements[i];
			targetArrayElementUI.Button.onClick.RemoveAllListeners();
			targetArrayElementUI.Button.onClick.AddListener(delegate
			{
				onElement(index);
			});
			targetArrayElementUI.Button_Icon.texture = targetArrayEntry.texture;
			targetArrayElementUI.Remove.onClick.RemoveAllListeners();
			targetArrayElementUI.Remove.onClick.AddListener(delegate
			{
				onRemove(index);
			});
			if (targetArrayEntry.index >= 0)
			{
				targetArrayElementUI.IndexLabel.gameObject.SetActive(value: true);
				targetArrayElementUI.IndexLabel_Text.text = $"@ {targetArrayEntry.index}";
			}
			else if (targetArrayEntry.index == -400)
			{
				targetArrayElementUI.IndexLabel.gameObject.SetActive(value: true);
				targetArrayElementUI.IndexLabel_Text.text = "RESET";
			}
			else if (targetArrayEntry.index == -500)
			{
				targetArrayElementUI.IndexLabel.gameObject.SetActive(value: true);
				targetArrayElementUI.IndexLabel_Text.text = "UNLOCK";
			}
			setHoverTooltip(targetArrayEntry.name, targetArrayElementUI.Button, propertiesTooltipPosition);
		}
		if (onAdd != null && (maxArrayCount < 0 || elements.Count < maxArrayCount))
		{
			Transform parent2 = targetArrayPropertyUI.TargetArrayAdd.transform.parent;
			RectTransform rectTransform = UnityEngine.Object.Instantiate(targetArrayPropertyUI.TargetArrayAdd, parent2);
			rectTransform.gameObject.SetActive(value: true);
			rectTransform.transform.SetAsFirstSibling();
			Button obj = rectTransform.GetComponentsInChildren<Button>()[0];
			obj.onClick.RemoveAllListeners();
			obj.onClick.AddListener(delegate
			{
				onAdd();
			});
		}
	}

	private void defineRoomEditorLinksProperty(string labelTitle, string labelTooltip, string pickerTitle, string pickerTooltip, RoomEditorLinks links, PropInstance target, Func<PropData, RoomEditorLinksData> getData, Predicate<PropInstance> picker)
	{
		labelTitle = tryGetLocalized(labelTitle);
		labelTooltip = tryGetLocalized(labelTooltip);
		pickerTitle = tryGetLocalized(pickerTitle);
		pickerTooltip = tryGetLocalized(pickerTooltip);
		List<TargetArrayEntry> list = new List<TargetArrayEntry>(links.targets.Count + links.locks.Count);
		for (int i = 0; i < links.targets.Count; i++)
		{
			list.Add(getTargetArrayEntry(links.targets[i].gameObject));
		}
		for (int j = 0; j < links.locks.Count; j++)
		{
			TargetArrayEntry targetArrayEntry = getTargetArrayEntry(links.locks[j].targetLock.gameObject);
			targetArrayEntry.index = links.locks[j].targetIndex;
			list.Add(targetArrayEntry);
		}
		defineTargetArrayProperty(pickerTitle, pickerTooltip, list, -1, delegate
		{
			enterMultiTargetMode(picker, delegate(HashSet<PropInstance> pis, HashSet<(PropInstance, int)> passIndexes)
			{
				var (prev, propData) = prepChangeData(target);
				foreach (PropInstance pi in pis)
				{
					if (pi.TryGetComponent<Lock>(out var _))
					{
						UnityEngine.Debug.Log("NO LONGER SUPPORTING LOCKS AS TARGETS");
					}
					else
					{
						getData(propData).targets.Add(pi.ID);
					}
				}
				foreach (var passIndex in passIndexes)
				{
					PropInstance item = passIndex.Item1;
					int item2 = passIndex.Item2;
					Lock component2 = item.GetComponent<Lock>();
					if (!links.locks.Exists(lockPredicate(component2, item2)))
					{
						getData(propData).locks.Add(new LockArgData
						{
							instanceID = item.ID,
							passwordIndex = item2
						});
					}
				}
				undo.push(actionChangeProp(prev, propData, "Links Add Targets and Locks"));
			}, passwordPredicateNeo(links.locks));
		}, delegate(int index)
		{
			GameObject gameObject = null;
			gameObject = ((index >= links.targets.Count) ? links.locks[getSafeLockIndex(index)].targetLock.gameObject : links.targets[index].gameObject);
			if (gameObject != null && gameObject.TryGetComponent<PropInstance>(out var component))
			{
				undo.push(actionChangeSelection(newInstanceSelection(component.ID)));
				moveCameraTo(gameObject);
			}
		}, delegate(int index)
		{
			if (index < links.targets.Count)
			{
				var (prev, propData) = prepChangeData(target);
				getData(propData).targets.RemoveAt(index);
				undo.push(actionChangeProp(prev, propData, "Links Remove Target"));
			}
			else
			{
				var (prev2, propData2) = prepChangeData(target);
				getData(propData2).locks.RemoveAt(getSafeLockIndex(index));
				undo.push(actionChangeProp(prev2, propData2, "Links Remove Lock"));
			}
		});
		if (links.locks.Count > 0)
		{
			defineIntProperty(labelTitle, labelTooltip, () => links.output, delegate(int value)
			{
				links.output = value;
			}, delegate(int prevValue, int _)
			{
				var (propData, next) = prepChangeData(target);
				getData(propData).output = prevValue;
				undo.push(actionChangeProp(propData, next, "Trigger Output Value"));
			});
		}
		int getSafeLockIndex(int index)
		{
			return Mathf.Clamp(index - links.targets.Count, 0, links.locks.Count - 1);
		}
	}

	private void defineLockLinksProperty(string label, string tooltip, List<LockArgument> locks, PropInstance targetInstance, Func<PropData, List<LockArgData>> getLockData)
	{
		label = tryGetLocalized(label);
		tooltip = tryGetLocalized(tooltip);
		List<LockArgument> lockArgs = safeLocks(locks);
		List<TargetArrayEntry> list = new List<TargetArrayEntry>(lockArgs.Count);
		for (int i = 0; i < lockArgs.Count; i++)
		{
			LockArgument lockArgument = lockArgs[i];
			TargetArrayEntry targetArrayEntry = getTargetArrayEntry(lockArgument.targetLock.gameObject);
			targetArrayEntry.index = lockArgument.targetIndex;
			list.Add(targetArrayEntry);
		}
		defineTargetArrayProperty(label, tooltip, list, -1, delegate
		{
			enterMultiTargetMode(targetLockPredicate, delegate(HashSet<PropInstance> pis, HashSet<(PropInstance, int)> passIndexes)
			{
				var (prev, propData) = prepChangeData(targetInstance);
				foreach (var passIndex in passIndexes)
				{
					PropInstance item = passIndex.Item1;
					int item2 = passIndex.Item2;
					Lock component = item.GetComponent<Lock>();
					if (!lockArgs.Exists(lockPredicate(component, item2)))
					{
						getLockData(propData).Add(new LockArgData
						{
							instanceID = item.ID,
							passwordIndex = item2
						});
					}
				}
				undo.push(actionChangeProp(prev, propData, "Add Locks"));
			}, passwordPredicateNeo(locks));
		}, delegate(int index)
		{
			GameObject gameObject = lockArgs[index].targetLock.gameObject;
			if (gameObject.TryGetComponent<PropInstance>(out var component))
			{
				undo.push(actionChangeSelection(newInstanceSelection(component.ID)));
				moveCameraTo(gameObject);
			}
		}, delegate(int index)
		{
			var (prev, propData) = prepChangeData(targetInstance);
			getLockData(propData).RemoveAt(index);
			undo.push(actionChangeProp(prev, propData, "Remove Lock"));
		});
		static bool targetLockPredicate(PropInstance pi)
		{
			Lock component;
			return pi.TryGetComponent<Lock>(out component);
		}
	}

	private PropertyData registerProperty(PineUIComponent ui, Action onUpdate, Action onCancel)
	{
		PropertyData propertyData = properties.Find((PropertyData p) => p.ui == ui);
		if (propertyData != null)
		{
			UnityEngine.Debug.LogError($"Property '{ui}' is already registered!", ui);
			return propertyData;
		}
		PropertyData propertyData2 = new PropertyData
		{
			ui = ui,
			onUpdate = onUpdate,
			onCancel = onCancel
		};
		properties.Add(propertyData2);
		return propertyData2;
	}

	private void setHoverTooltip(string tooltip, Component ui, UITooltip.Position position, float spacing = 10f)
	{
		UITooltip orAddComponent = ui.GetOrAddComponent<UITooltip>();
		if (orAddComponent.tooltipTemplate == null)
		{
			orAddComponent.tooltipTemplate = hoverTooltipTemplate;
			orAddComponent.calculateSize = true;
		}
		if (orAddComponent.enabled = !string.IsNullOrWhiteSpace(tooltip))
		{
			orAddComponent.text = tryGetLocalized(tooltip);
			orAddComponent.position = position;
			orAddComponent.spacing = spacing;
			orAddComponent.enabled = true;
		}
	}

	private static EventTrigger.Entry addTriggerEntry(GameObject uiObject, EventTriggerType type)
	{
		PineUITrigger orAddComponent = uiObject.GetOrAddComponent<PineUITrigger>();
		EventTrigger.Entry entry = new EventTrigger.Entry
		{
			eventID = type
		};
		orAddComponent.triggers.Add(entry);
		return entry;
	}

	private static void removeTriggerEntry(GameObject uiObject, EventTriggerType? type = null)
	{
		PineUITrigger component = uiObject.GetComponent<PineUITrigger>();
		if (component == null)
		{
			return;
		}
		for (int num = component.triggers.Count - 1; num >= 0; num--)
		{
			if (!type.HasValue || type.Value == component.triggers[num].eventID)
			{
				component.triggers.RemoveAt(num);
			}
		}
	}

	private static void setVector3PropertyText(InputField xField, InputField yField, InputField zField, Vector3 value)
	{
		setFloatPropertyText(xField, value.x);
		setFloatPropertyText(yField, value.y);
		setFloatPropertyText(zField, value.z);
	}

	private static void setVector2PropertyText(InputField xField, InputField yField, Vector2 value)
	{
		setFloatPropertyText(xField, value.x);
		setFloatPropertyText(yField, value.y);
	}

	private static void setFloatPropertyText(InputField inputField, float value, int decimals = 3)
	{
		inputField.SetTextWithoutNotify(value.ToString("0." + new string('#', decimals), CultureInfo.InvariantCulture));
	}

	private static void setIntPropertyText(InputField inputField, int value)
	{
		inputField.SetTextWithoutNotify(value.ToString());
	}

	private static bool tryParseFloat(string floatAsString, out float value)
	{
		return float.TryParse(floatAsString.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out value);
	}

	private static string tryGetLocalized(string line)
	{
		if (line == null)
		{
			return null;
		}
		if (line == "")
		{
			return "";
		}
		if (line.StartsWith("%") && line.EndsWith("%"))
		{
			return Localization.lookupInEnglishDictionary(line.Substring(1, line.Length - 2), line);
		}
		return line;
	}

	private static List<AssetBundle> loadAssetsBundles()
	{
		UnityEngine.Debug.Log("Loading assetsBundle");
		IEnumerable<AssetBundle> allLoadedAssetBundles = AssetBundle.GetAllLoadedAssetBundles();
		List<AssetBundle> list = new List<AssetBundle>();
		List<string> remainingBundles = new List<string>();
		EditorPaths.propInfos.ForEach(delegate(EditorPaths.PropAssetBundleInfo x)
		{
			remainingBundles.Add(x.assetBundleName);
		});
		foreach (AssetBundle item in allLoadedAssetBundles)
		{
			for (int num = remainingBundles.Count - 1; num >= 0; num--)
			{
				string text = remainingBundles[num];
				if (item.name == text)
				{
					list.Add(item);
					remainingBundles.Remove(item.name);
				}
			}
		}
		foreach (string item2 in remainingBundles)
		{
			AssetBundle assetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "AssetBundles/" + item2));
			UnityEngine.Debug.Log("bundle path: " + item2);
			UnityEngine.Debug.Log("is streamed " + assetBundle.isStreamedSceneAssetBundle);
			UnityEngine.Debug.Log("loaded asset");
			list.Add(assetBundle);
		}
		list.Sort(delegate(AssetBundle a, AssetBundle b)
		{
			int num2 = EditorPaths.propInfos.FindIndex((EditorPaths.PropAssetBundleInfo x) => x.assetBundleName == a.name);
			int value = EditorPaths.propInfos.FindIndex((EditorPaths.PropAssetBundleInfo x) => x.assetBundleName == b.name);
			return num2.CompareTo(value);
		});
		return list;
	}

	private static AssetBundle loadAssetsIconsBundle()
	{
		UnityEngine.Debug.Log("Loading assetsIconsBundle");
		foreach (AssetBundle allLoadedAssetBundle in AssetBundle.GetAllLoadedAssetBundles())
		{
			if (allLoadedAssetBundle.name == "editorassetsicons")
			{
				return allLoadedAssetBundle;
			}
		}
		return AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "AssetBundles/editorassetsicons"));
	}

	private static string getUGCFolder()
	{
		return Path.Combine(Application.persistentDataPath, "UGC");
	}

	private static string createUGCFolder()
	{
		string uGCFolder = getUGCFolder();
		if (!Directory.Exists(uGCFolder))
		{
			Directory.CreateDirectory(uGCFolder);
		}
		return uGCFolder;
	}

	private static string getPremadesFolder()
	{
		return Path.Combine(Application.persistentDataPath, "UGC", "Premades");
	}

	private static string createPremadesFolder()
	{
		string premadesFolder = getPremadesFolder();
		if (!Directory.Exists(premadesFolder))
		{
			Directory.CreateDirectory(premadesFolder);
		}
		return premadesFolder;
	}

	private static string getCustomModelsFolder(string roomDirPath)
	{
		return Path.Combine(roomDirPath, "_CustomModels");
	}

	private static string createCustomModelsFolder(string roomDirPath)
	{
		string customModelsFolder = getCustomModelsFolder(roomDirPath);
		if (!Directory.Exists(customModelsFolder))
		{
			Directory.CreateDirectory(customModelsFolder);
		}
		return customModelsFolder;
	}

	private static string createTempFolder()
	{
		string text = Path.Combine(createUGCFolder(), "Temp");
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		return text;
	}

	public static string createRoomDir(string dirPath)
	{
		if (string.IsNullOrEmpty(dirPath))
		{
			int num = 0;
			string text = "Room_";
			string text2 = createUGCFolder();
			string[] directories = Directory.GetDirectories(text2);
			for (int i = 0; i < directories.Length; i++)
			{
				string fileName = Path.GetFileName(directories[i]);
				if (fileName.StartsWith(text) && int.TryParse(fileName.Substring(text.Length), out var result))
				{
					num = Mathf.Max(num, result);
				}
			}
			string path = text + (num + 1);
			dirPath = Path.Combine(text2, path);
		}
		Directory.CreateDirectory(dirPath);
		return dirPath;
	}

	public static void clearUnusedFiles(string roomDirPath)
	{
		(RoomData, LoadRoomResult) tuple = loadRoomDataFromPath(Path.Combine(roomDirPath, "Room.room"));
		var (roomData, _) = tuple;
		if (tuple.Item2 != LoadRoomResult.Success)
		{
			return;
		}
		HashSet<string> usedFiles = new HashSet<string>();
		HashSet<string> hashSet = new HashSet<string>();
		usedFiles.Add("Room.room");
		usedFiles.Add("Preview.jpg");
		foreach (PropData prop in roomData.props)
		{
			foreach (MaterialSwapData materialSwap in prop.materialSwaps)
			{
				usedFiles.Add(materialSwap.asset);
				string text = Path.Combine(roomDirPath, materialSwap.asset);
				string text2 = File.ReadAllText(text);
				MaterialData materialData = JsonUtility.FromJson<MaterialData>(text2);
				if (materialData == null)
				{
					UnityEngine.Debug.LogError("Failed to parse material data from file: " + text + ", JSON:\n" + text2);
					continue;
				}
				addTextureIfNeeded(materialData.baseMapTexture);
				addTextureIfNeeded(materialData.normalMapTexture);
				addTextureIfNeeded(materialData.maskMapTexture);
				addTextureIfNeeded(materialData.emissiveMapTexture);
			}
			if (tryGetSoundData(prop, out var data) && !string.IsNullOrEmpty(data.path))
			{
				usedFiles.Add(data.path);
			}
			if (tryGetScriptComponentData(prop, out var data2) && !string.IsNullOrEmpty(data2.scriptLocation))
			{
				usedFiles.Add(data2.scriptLocation + ".lua");
			}
			if (tryGetSkyboxData(prop, out var data3) && data3.textureData != null && data3.textureData.custom)
			{
				usedFiles.Add(data3.textureData.fileName);
			}
			if (tryGetCustomModelData(prop, out var data4))
			{
				foreach (string usedFile in data4.usedFiles)
				{
					hashSet.Add(usedFile);
				}
			}
			if (tryGetEditorDisplayData(prop, out var data5))
			{
				usedFiles.Add(data5.spriteSheetFileName);
			}
		}
		string[] files = Directory.GetFiles(roomDirPath);
		foreach (string text3 in files)
		{
			string fileName = Path.GetFileName(text3);
			if (!usedFiles.Contains(fileName))
			{
				File.Delete(text3);
				UnityEngine.Debug.Log("Deleted '" + text3 + "'");
			}
		}
		string path = "_CustomModels";
		string text4 = Path.Combine(roomDirPath, path);
		string[] array = new string[0];
		if (Directory.Exists(text4))
		{
			array = Directory.GetFiles(text4, "*", SearchOption.AllDirectories);
			array = fixPathModelPaths(array);
		}
		foreach (string text5 in array)
		{
			string item = Path.GetFileName(text5).Replace('\\', '/');
			if (usedFiles.Contains(item))
			{
				continue;
			}
			bool flag = false;
			foreach (string item2 in hashSet)
			{
				if (text5.Contains(item2))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				File.Delete(text5);
				UnityEngine.Debug.Log("Deleted '" + text5 + "'");
			}
		}
		deleteEmptyDirectories(text4, text4);
		if (Directory.Exists(text4) && Directory.GetFiles(text4).Length == 0 && Directory.GetDirectories(text4).Length == 0)
		{
			UnityEngine.Debug.Log("Deleted '" + text4 + "'");
			Directory.Delete(text4);
		}
		void addTextureIfNeeded(string texture)
		{
			if (!string.IsNullOrEmpty(texture))
			{
				usedFiles.Add(texture);
			}
		}
		static bool deleteEmptyDirectories(string baseDirectory, string directoryPath)
		{
			if (!Directory.Exists(directoryPath))
			{
				return false;
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			if (directories.Length == 0 && directoryInfo.GetFiles().Length == 0)
			{
				return true;
			}
			for (int num = directories.Length - 1; num >= 0; num--)
			{
				DirectoryInfo directoryInfo2 = directories[num];
				if (deleteEmptyDirectories(baseDirectory, directoryInfo2.FullName))
				{
					UnityEngine.Debug.Log("Deleted '" + directoryInfo2.FullName + "'");
					Directory.Delete(directoryInfo2.FullName);
					if (directoryPath != baseDirectory)
					{
						deleteEmptyDirectories(baseDirectory, directoryInfo.Parent.FullName);
					}
				}
			}
			return false;
		}
	}

	private static string[] fixPathModelPaths(string[] oldPaths)
	{
		for (int i = 0; i < oldPaths.Length; i++)
		{
			oldPaths[i] = oldPaths[i].Replace('\\', '/');
		}
		return oldPaths;
	}

	private static bool loadCustomProps(EditorContext context, string roomDirPath, Transform customModelParent, Action onComplete, bool fromEditor = false, Dictionary<PropID, Sprite> sprites = null, Dictionary<PropID, Texture2D> icons = null, Texture2D missingCustomModelImage = null, bool useOverrideRoomData = false)
	{
		List<CustomModelObject> customModelPrefabs;
		if (!useOverrideRoomData)
		{
			customModelPrefabs = context.customModels;
			string[] array = checkIfAddedCustomModels(customModelPrefabs, roomDirPath, fromEditor);
			loadCustomModels(timeBudgetDeferAgent, array);
			return array.Length != 0;
		}
		List<CustomModelObject> customModelPrefabs2 = context.customModels;
		onLoadTimeBudgetPerFrameDeferAgentButton(context.customRoom?.customFiles);
		UnpackedCustomRoom customRoom = context.customRoom;
		if (customRoom == null)
		{
			return false;
		}
		return customRoom.customFiles.Count > 0;
		async Task CustomDeferAgent(IDeferAgent deferAgent, List<CustomRoomDataFile> customFile)
		{
			List<Task> list = new List<Task>();
			foreach (CustomRoomDataFile model in customFile)
			{
				if (model.path.EndsWith(".gltf") || model.path.EndsWith(".glb"))
				{
					PropID ID = new PropID
					{
						value = "custom_/" + model.path + "-" + model.path
					};
					bool flag = false;
					foreach (CustomModelRequest loadingCustomModel in context.loadingCustomModels)
					{
						if (loadingCustomModel.id == ID)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						UnityEngine.Debug.Log("Model name ID duplicate, skipping: '" + model?.ToString() + "' - file: " + model);
					}
					else
					{
						CustomModelRequest newRequest = new CustomModelRequest();
						newRequest.id = ID;
						newRequest.callback = delegate
						{
							newRequest.isDone = true;
						};
						context.loadingCustomModels.Add(newRequest);
						CustomModelObject customModelObject = new CustomModelObject();
						customModelObject.filepath = model.path;
						customModelObject.name = model.path;
						customModelObject.prop = new Prop
						{
							ID = ID,
							name = customModelObject.name
						};
						customModelObject.prop.tags = new List<PropTag> { PropTag.Custom };
						customModelPrefabs2.Add(customModelObject);
						GameObject parent = new GameObject(customModelObject.name);
						parent.transform.SetParent(customModelParent);
						customModelObject.gameObject = parent;
						if (missingCustomModelImage != null)
						{
							icons.Add(ID, missingCustomModelImage);
							Sprite value = Sprite.Create(rect: new Rect(0f, 0f, missingCustomModelImage.width, missingCustomModelImage.height), texture: missingCustomModelImage, pivot: new Vector2(0.5f, 0.5f), pixelsPerUnit: 100f);
							sprites.Add(ID, value);
						}
						UnityEngine.Debug.Log("Loading custom model: " + model);
						ImportSettings importSettings = new ImportSettings
						{
							NodeNameMethod = NameImportMethod.OriginalUnique,
							GenerateMipMaps = true
						};
						GltfImport gltf = new GltfImport(null, deferAgent);
						Task item = gltf.LoadGltfBinary(model.data, null, importSettings).ContinueWith(delegate(Task<bool> t)
						{
							newRequest.callback();
							if (t.Result)
							{
								PineGLTFastInstantiator instantiator = new PineGLTFastInstantiator(gltf, parent.transform);
								gltf.InstantiateMainScene(instantiator);
								parent.SetActive(value: false);
								Bounds bounds = Game.computeLocalBounds(parent);
								addBasicComponents(customModelObject, bounds, out var _);
								if (fromEditor)
								{
									if (context.container.screenshotCamera != null && sprites != null)
									{
										Texture2D customPropIcon = getCustomPropIcon(parent, context.container.screenshotCamera, bounds);
										icons[ID] = customPropIcon;
										Rect rect = new Rect(0f, 0f, customPropIcon.width, customPropIcon.height);
										Sprite value2 = Sprite.Create(customPropIcon, rect, new Vector2(0.5f, 0.5f), 100f);
										sprites[ID] = value2;
									}
									else
									{
										UnityEngine.Debug.Log("Screenshot camera is null");
									}
								}
								if (onComplete != null)
								{
									onComplete();
								}
								UnityEngine.Debug.Log("Done loading: " + model);
							}
							else
							{
								UnityEngine.Debug.LogError("Error with loading model: " + model);
								customModelPrefabs2.Remove(customModelObject);
							}
						}, TaskScheduler.FromCurrentSynchronizationContext());
						list.Add(item);
					}
				}
			}
			await Task.WhenAll(list);
		}
		static void addBasicComponents(CustomModelObject customModelObject, Bounds bounds, out CustomModel customModel)
		{
			customModel = customModelObject.gameObject.AddComponent<CustomModel>();
			customModel.usedFiles = new List<string>();
			customModel.colliderType = CustomModelColliderType.None;
			if (customModelObject.gameObject.GetComponentInChildren<Collider>() == null)
			{
				customModel.hasModelColliders = true;
				BoxCollider boxCollider = customModelObject.gameObject.AddComponent<BoxCollider>();
				boxCollider.size = bounds.size;
				boxCollider.center = bounds.center;
				customModel.calculatedBounds = bounds;
				customModel.colliderType = CustomModelColliderType.Box;
			}
			customModel.modelAnimation = customModelObject.gameObject.GetComponentInChildren<UnityEngine.Animation>();
			customModel.animations = new List<string>();
			customModel.currentAnimation = -1;
			if (customModel.modelAnimation != null)
			{
				customModel.modelAnimation.playAutomatically = false;
				foreach (AnimationState item2 in customModel.modelAnimation)
				{
					customModel.animations.Add(item2.clip.name);
				}
				if (customModel.animations.Count > 0)
				{
					customModel.currentAnimation = 0;
					customModel.animationSampler = customModel.modelAnimation.gameObject.AddComponent<AnimationSampler>();
				}
			}
			customModelObject.gameObject.AddComponent<PropInstance>();
			customModelObject.gameObject.AddComponent<PropTags>().tags = new List<PropTag> { PropTag.Custom };
		}
		static string[] checkIfAddedCustomModels(List<CustomModelObject> list2, string text, bool fromEditor2)
		{
			List<string> list = new List<string>();
			string[] array2 = getCustomModelsFilePaths(text, fromEditor2);
			if (array2 != null)
			{
				string[] array3 = array2;
				foreach (string text2 in array3)
				{
					if (File.Exists(text2))
					{
						bool flag = false;
						foreach (CustomModelObject item3 in list2)
						{
							if (item3.filepath == text2)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							string fileName = Path.GetFileName(text2);
							if (fileName.EndsWith(".gltf") || fileName.EndsWith(".glb"))
							{
								list.Add(text2);
							}
						}
					}
				}
			}
			return list.ToArray();
		}
		static string[] getCustomModelsFilePaths(string text, bool flag = false)
		{
			string path;
			if (flag)
			{
				path = createCustomModelsFolder(text);
			}
			else
			{
				path = getCustomModelsFolder(text);
				if (!Directory.Exists(path))
				{
					return null;
				}
			}
			return fixPathModelPaths(Directory.GetFiles(path, "*.gl*", SearchOption.AllDirectories));
		}
		async Task loadCustomModels(IDeferAgent deferAgent, string[] filepaths)
		{
			foreach (string filePath in filepaths)
			{
				string fileName = Path.GetFileName(filePath);
				if (!File.Exists(filePath) || (!fileName.EndsWith(".gltf") && !fileName.EndsWith(".glb")))
				{
					UnityEngine.Debug.Log("Custom model not loaded: " + filePath);
				}
				else
				{
					string[] array2 = filePath.Split(new string[1] { "_CustomModels" }, StringSplitOptions.None);
					PropID ID = new PropID
					{
						value = "custom_" + array2[^1] + "-" + fileName
					};
					bool flag = false;
					foreach (CustomModelRequest loadingCustomModel2 in context.loadingCustomModels)
					{
						if (loadingCustomModel2.id == ID)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						UnityEngine.Debug.Log("Model name ID duplicate, skipping: '" + fileName + "' - file: " + filePath);
					}
					else
					{
						CustomModelRequest newRequest = new CustomModelRequest();
						newRequest.id = ID;
						newRequest.callback = delegate
						{
							newRequest.isDone = true;
						};
						context.loadingCustomModels.Add(newRequest);
						CustomModelObject customModelObject = new CustomModelObject();
						customModelObject.filepath = filePath;
						customModelObject.name = fileName.Split('.')[0];
						customModelObject.prop = new Prop
						{
							ID = ID,
							name = customModelObject.name
						};
						customModelObject.prop.tags = new List<PropTag> { PropTag.Custom };
						customModelPrefabs.Add(customModelObject);
						GameObject parent = new GameObject(customModelObject.name);
						parent.transform.SetParent(customModelParent);
						customModelObject.gameObject = parent;
						if (missingCustomModelImage != null)
						{
							icons.Add(ID, missingCustomModelImage);
							Sprite value = Sprite.Create(rect: new Rect(0f, 0f, missingCustomModelImage.width, missingCustomModelImage.height), texture: missingCustomModelImage, pivot: new Vector2(0.5f, 0.5f), pixelsPerUnit: 100f);
							sprites.Add(ID, value);
						}
						UnityEngine.Debug.Log("Loading custom model: " + fileName + " - " + filePath);
						ImportSettings importSettings = new ImportSettings
						{
							NodeNameMethod = NameImportMethod.OriginalUnique,
							GenerateMipMaps = true
						};
						GltfImport gltf = new GltfImport(null, deferAgent, new GltfToHdrpMaterialGenerator());
						bool num = await gltf.Load(filePath, importSettings);
						newRequest.callback();
						if (num)
						{
							PineGLTFastInstantiator instantiator = new PineGLTFastInstantiator(gltf, parent.transform);
							await gltf.InstantiateMainSceneAsync(instantiator);
							parent.SetActive(value: false);
							Bounds bounds = Game.computeLocalBounds(parent);
							addBasicComponents(customModelObject, bounds, out var customModel);
							string text = new FileInfo(filePath).Directory.Name;
							customModel.usedFiles.Add(text + "/" + fileName);
							if (fromEditor)
							{
								if (context.container.screenshotCamera != null && sprites != null)
								{
									Texture2D customPropIcon = getCustomPropIcon(parent, context.container.screenshotCamera, bounds);
									icons[ID] = customPropIcon;
									Rect rect = new Rect(0f, 0f, customPropIcon.width, customPropIcon.height);
									Sprite value2 = Sprite.Create(customPropIcon, rect, new Vector2(0.5f, 0.5f), 100f);
									sprites[ID] = value2;
								}
								else
								{
									UnityEngine.Debug.Log("Screenshot camera is null");
								}
							}
							if (fileName.EndsWith(".gltf"))
							{
								if (gltf.GetSourceRoot().images != null)
								{
									GLTFast.Schema.Image[] images = gltf.GetSourceRoot().images;
									foreach (GLTFast.Schema.Image image in images)
									{
										if (!string.IsNullOrEmpty(image.uri) && !image.uri.StartsWith("data:"))
										{
											string text2 = separatorFix(image.uri);
											customModel.usedFiles.Add(text + "/" + text2);
										}
									}
								}
								if (gltf.GetSourceRoot().buffers != null)
								{
									GLTFast.Schema.Buffer[] buffers = gltf.GetSourceRoot().buffers;
									foreach (GLTFast.Schema.Buffer buffer in buffers)
									{
										if (!string.IsNullOrEmpty(buffer.uri) && !buffer.uri.StartsWith("data:"))
										{
											string text3 = separatorFix(buffer.uri);
											customModel.usedFiles.Add(text + "/" + text3);
										}
									}
								}
							}
							if (onComplete != null)
							{
								onComplete();
							}
							UnityEngine.Debug.Log("Done loading: " + fileName + " - " + filePath);
						}
						else
						{
							UnityEngine.Debug.LogError("Error with loading model: " + fileName + " - " + filePath);
							customModelPrefabs.Remove(customModelObject);
						}
					}
				}
			}
		}
		void onLoadTimeBudgetPerFrameDeferAgentButton(List<CustomRoomDataFile> customFile)
		{
			CustomDeferAgent(new TimeBudgetPerFrameDeferAgent(), customFile);
		}
		static string separatorFix(string uri)
		{
			return uri.Replace('\\', '/');
		}
	}

	private static void updateAssetsRequests(List<AssetRequest> assetsRequests)
	{
		for (int num = assetsRequests.Count - 1; num >= 0; num--)
		{
			AssetRequest assetRequest = assetsRequests[num];
			if (assetRequest.op.isDone)
			{
				assetsRequests.RemoveAt(num);
				assetRequest.callback(assetRequest.op.asset);
			}
		}
	}

	private static void updateLoadingCustomModels(List<CustomModelRequest> loadingCustomModels)
	{
		for (int num = loadingCustomModels.Count - 1; num >= 0; num--)
		{
			if (loadingCustomModels[num].isDone)
			{
				loadingCustomModels.RemoveAt(num);
			}
		}
	}

	private static void killAssetRequests(List<AssetRequest> requests, string killID)
	{
		requests.RemoveAll((AssetRequest x) => x.killID == killID);
	}

	private GameObject loadAssetPrefab(PropID id)
	{
		return loadAssetPrefab(id.value);
	}

	private GameObject loadAssetPrefab(string assetPath)
	{
		return loadAssetPrefab(assetsBundles, assetPath);
	}

	private static GameObject loadAssetPrefab(List<AssetBundle> assetBundles, PropID id)
	{
		return loadAssetPrefab(assetBundles, id.value);
	}

	private static GameObject loadAssetPrefab(List<AssetBundle> assetBundles, string localPropPath)
	{
		List<string> propPaths = EditorPaths.getPropPaths(localPropPath);
		for (int i = 0; i < propPaths.Count; i++)
		{
			string text = propPaths[i];
			if (assetBundles[i].Contains(text))
			{
				return assetBundles[i].LoadAsset<GameObject>(text);
			}
		}
		return null;
	}

	public static GameObject debugLoadAssetPrefab(string localPath)
	{
		UnityEngine.Debug.LogError("debugLoadAssetPrefab shouldn't be called from build");
		return null;
	}

	private Texture2D loadAssetIcon(string assetPath)
	{
		return loadAssetIcon(assetsIconsBundle, assetPath);
	}

	private static Texture2D loadAssetIcon(AssetBundle assetsBundle, string assetPath)
	{
		return assetsBundle.LoadAsset<Texture2D>(assetPath);
	}

	private static GameObject loadCustomPrefab(List<CustomModelObject> customModels, PropID propID)
	{
		return customModels.Find((CustomModelObject x) => x.prop.ID == propID)?.gameObject;
	}

	private static GameObject loadPrefab(EditorContext context, PropID propID)
	{
		if (!isPropCustom(propID))
		{
			return loadAssetPrefab(context.assetsBundles, propID);
		}
		GameObject gameObject = loadCustomPrefab(context.customModels, propID);
		if (gameObject == null)
		{
			gameObject = loadAssetPrefab(context.assetsBundles, "cube.prefab");
		}
		return gameObject;
	}

	private static bool isPropCustom(PropID propID)
	{
		return propID.value.StartsWith("custom_");
	}

	private static Texture2D getCustomPropIcon(GameObject propGO, UnityEngine.Camera ssCam, Bounds bounds)
	{
		ssCam.gameObject.SetActive(value: true);
		float num = Mathf.Max(Mathf.Max(bounds.size.x, bounds.size.y), bounds.size.z);
		if (bounds.extents.magnitude > 0f)
		{
			propGO.transform.localScale = Vector3.one * (1f / num);
		}
		Vector3 vector = Vector3.Scale(propGO.transform.localScale, propGO.transform.localRotation * bounds.center);
		propGO.transform.localPosition = -vector;
		ssCam.transform.position -= ssCam.transform.forward * 1f;
		propGO.SetActive(value: true);
		int layer = propGO.layer;
		int layer2 = LayerMask.NameToLayer("EditorPropScreenshot");
		Renderer[] componentsInChildren = propGO.GetComponentsInChildren<Renderer>(includeInactive: true);
		Renderer[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gameObject.layer = layer2;
		}
		RenderTexture renderTexture = (ssCam.targetTexture = new RenderTexture(512, 512, 24));
		ssCam.Render();
		ssCam.gameObject.SetActive(value: false);
		array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gameObject.layer = layer;
		}
		propGO.SetActive(value: false);
		RenderTexture.active = renderTexture;
		Texture2D texture2D = new Texture2D(512, 512);
		texture2D.ReadPixels(new Rect(0f, 0f, renderTexture.width, renderTexture.height), 0, 0);
		Texture2D texture2D2 = getTrimmedTexture(texture2D, 512, 512);
		texture2D2.Apply();
		return texture2D2;
		static Texture2D getTrimmedTexture(Texture2D tex, int w, int h)
		{
			Color[] pixels = tex.GetPixels();
			Vector2 vector2 = new Vector2(w - 1, h - 1);
			Vector2 zero = Vector2.zero;
			for (int j = 0; j < w; j++)
			{
				for (int k = 0; k < h; k++)
				{
					if (pixels[j * w + k].a > 0f)
					{
						if ((float)k < vector2.x)
						{
							vector2.x = k;
						}
						if ((float)k > zero.x)
						{
							zero.x = k;
						}
						if ((float)j < vector2.y)
						{
							vector2.y = j;
						}
						if ((float)j > zero.y)
						{
							zero.y = j;
						}
					}
				}
			}
			int num2 = (int)(zero.x - vector2.x);
			int num3 = (int)(zero.y - vector2.y);
			if (num2 > 0 || num3 > 0)
			{
				Color[] pixels2 = tex.GetPixels((int)vector2.x, (int)vector2.y, num2, num3);
				int num4 = Mathf.Max(num2, num3);
				Texture2D texture2D3 = new Texture2D(num4, num4);
				Color32 color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);
				Color32[] pixels3 = texture2D3.GetPixels32();
				for (int l = 0; l < pixels3.Length; l++)
				{
					pixels3[l] = color;
				}
				texture2D3.SetPixels32(pixels3);
				int x = (num4 - num2) / 2;
				int y = (num4 - num3) / 2;
				texture2D3.SetPixels(x, y, num2, num3, pixels2, 0);
				return texture2D3;
			}
			return tex;
		}
	}

	private void updateLoadingScreenCustomModelNumbers(int totalModelsLoading, int modelsLoaded)
	{
		string loadingScreenCustomModelText = getLoadingScreenCustomModelText(loadingCanvasText, totalModelsLoading, modelsLoaded);
		if (endConditionCustomModels != null)
		{
			LoadingCanvas.get().changeHint(endConditionCustomModels, loadingScreenCustomModelText);
		}
	}

	public static string getLoadingScreenCustomModelText(string loadingText, int totalModelsLoading, int modelsLoaded)
	{
		string text = Localization.translate(loadingText);
		if (modelsLoaded != -1)
		{
			text = text + " (" + modelsLoaded + "/" + totalModelsLoading + ")";
		}
		return text;
	}

	public static void updateLoadingScreenCustomModelNumbers(LoadingCanvas.EndCondition endCondition, EditorContext context, string loadingText)
	{
		int count = context.customModels.Count;
		int modelsLoaded = count - context.loadingCustomModels.Count + 1;
		string loadingScreenCustomModelText = getLoadingScreenCustomModelText(loadingText, count, modelsLoaded);
		LoadingCanvas.get().changeHint(endCondition, loadingScreenCustomModelText);
	}

	private static void loadTexture2DAsync(EditorContext context, string assetPath, Action<object> callback, string ext = ".png", string killID = "", bool loadIcon = false)
	{
		assetPath += ext;
		AssetRequest assetRequest = new AssetRequest();
		if (loadIcon)
		{
			assetRequest.op = context.assetsIconsBundle.LoadAssetAsync<UnityEngine.Texture>(assetPath);
		}
		else
		{
			foreach (AssetBundle assetsBundle in context.assetsBundles)
			{
				if (assetsBundle.Contains(assetPath))
				{
					assetRequest.op = assetsBundle.LoadAssetAsync<UnityEngine.Texture>(assetPath);
					break;
				}
			}
		}
		assetRequest.callback = callback;
		assetRequest.killID = killID;
		if (assetRequest.op != null)
		{
			context.assetsRequests.Add(assetRequest);
		}
		else
		{
			UnityEngine.Debug.Log("Missing texture async load: " + assetPath);
		}
	}

	private void loadIconTexture2DAsync(string assetPath, Action<object> callback, string ext = ".png", string killID = "")
	{
		loadTexture2DAsync(getEditorContext(), assetPath, callback, ext, killID, loadIcon: true);
	}

	private void loadPropIcons()
	{
		foreach (Prop prop2 in assets.props)
		{
			string imgName = prop2.ID.value.Replace("/", "-");
			Prop prop1 = prop2;
			loadIconTexture2DAsync(EditorPaths.getPropIconPath(imgName), delegate(object asset)
			{
				Texture2D texture2D = (Texture2D)asset;
				if (texture2D != null)
				{
					propIcons[prop1.ID] = texture2D;
					Rect rect = new Rect(0f, 0f, texture2D.width, texture2D.height);
					propSprites[prop1.ID] = Sprite.Create(texture2D, rect, new Vector2(0.5f, 0.5f), 100f);
				}
				else
				{
					UnityEngine.Debug.Log("Missing icon for prop '" + imgName + "'");
				}
			});
		}
		loadIconTexture2DAsync(EditorPaths.getPropIconPath("Base-EmptyModel"), delegate(object asset)
		{
			Texture2D texture2D = (Texture2D)asset;
			if (texture2D != null)
			{
				missingModelImage = texture2D;
			}
			else
			{
				UnityEngine.Debug.LogError("MISSING MISSING CUSTOM MODEL IMAGE");
			}
		});
	}

	private static void loadTextureFromDisk(List<StaggeredTextureLoadInfo> staggeredTextureLoad, string filePath, string killID, Action<UnityEngine.Texture> callback)
	{
		bool flag = false;
		for (int i = 0; i < staggeredTextureLoad.Count; i++)
		{
			StaggeredTextureLoadInfo staggeredTextureLoadInfo = staggeredTextureLoad[i];
			if (staggeredTextureLoadInfo.filePath == filePath)
			{
				flag = true;
				if (staggeredTextureLoadInfo.callbacks == null)
				{
					staggeredTextureLoadInfo.callbacks = new List<Action<UnityEngine.Texture>>();
					staggeredTextureLoadInfo.callbacks.Add(staggeredTextureLoadInfo.callback);
				}
				staggeredTextureLoadInfo.callbacks.Add(callback);
			}
		}
		if (!flag)
		{
			staggeredTextureLoad.Add(new StaggeredTextureLoadInfo
			{
				filePath = filePath,
				killID = killID,
				callback = callback
			});
		}
	}

	private void loadTextureFromDisk(string filePath, string killID, Action<UnityEngine.Texture> callback)
	{
		loadTextureFromDisk(staggeredTextureLoad, filePath, killID, callback);
	}

	private static void updateLoadingTextureOps(List<LoadTextureOp> loadTexturesOps, List<StaggeredTextureLoadInfo> staggeredTextureLoad, List<CustomRoomDataFile> overrideFiles)
	{
		while (staggeredTextureLoad.Count > 0 && loadTexturesOps.Count < 5)
		{
			if (overrideFiles == null)
			{
				UnityWebRequestAsyncOperation asyncOp = UnityWebRequestTexture.GetTexture(staggeredTextureLoad[0].filePath, nonReadable: false).SendWebRequest();
				loadTexturesOps.Add(new LoadTextureOp
				{
					asyncOp = asyncOp,
					callback = staggeredTextureLoad[0].callback,
					callbacks = staggeredTextureLoad[0].callbacks,
					killID = staggeredTextureLoad[0].killID
				});
			}
			else
			{
				string filename = staggeredTextureLoad[0].filePath;
				string[] array = staggeredTextureLoad[0].filePath.Split(".roomdata/");
				if (array.Length > 1)
				{
					filename = array[1];
				}
				CustomRoomDataFile customRoomDataFile = overrideFiles.Find((CustomRoomDataFile x) => x.path == filename && x.customType == CustomRoomDataFile.CustomType.Texture);
				if (customRoomDataFile != null)
				{
					byte[] data = customRoomDataFile.data;
					TextureFormat textureFormat = TextureFormat.RGBA32;
					if (SystemInfo.SupportsTextureFormat(TextureFormat.DXT5))
					{
						textureFormat = TextureFormat.DXT5;
					}
					Texture2D texture2D = new Texture2D(4, 4, textureFormat, mipChain: true);
					if (!texture2D.LoadImage(data, markNonReadable: true))
					{
						texture2D = new Texture2D(4, 4, TextureFormat.RGBA32, mipChain: true);
						texture2D.LoadImage(data, markNonReadable: true);
					}
					StaggeredTextureLoadInfo staggeredTextureLoadInfo = staggeredTextureLoad[0];
					if (staggeredTextureLoadInfo.callbacks != null)
					{
						for (int num = 0; num < staggeredTextureLoadInfo.callbacks.Count; num++)
						{
							staggeredTextureLoadInfo.callbacks[num](texture2D);
						}
					}
					else
					{
						staggeredTextureLoadInfo.callback(texture2D);
					}
					if (customRoomDataFile.path.Contains("tex_story"))
					{
						UnityEngine.Debug.Log(customRoomDataFile.path + " " + texture2D.width + " " + texture2D.height + " " + texture2D.anisoLevel + " " + texture2D.mipMapBias + " " + texture2D.mipmapCount + " " + texture2D.graphicsFormat.ToString() + " " + getProperSettings().customTextureCompression);
					}
				}
			}
			staggeredTextureLoad.RemoveAt(0);
		}
		int num2 = 0;
		while (num2 < loadTexturesOps.Count)
		{
			LoadTextureOp loadTextureOp = loadTexturesOps[num2];
			if (loadTextureOp.asyncOp.isDone)
			{
				loadTexturesOps.RemoveAt(num2);
				Texture2D texture = null;
				UnityWebRequest webRequest = loadTextureOp.asyncOp.webRequest;
				if (webRequest.result == UnityWebRequest.Result.Success)
				{
					texture = DownloadHandlerTexture.GetContent(webRequest);
					processTexture(ref texture);
					loadTextureOp.asyncOp = null;
				}
				else
				{
					UnityEngine.Debug.Log("Failed to load texture at path " + grey(webRequest.uri.ToString()) + " with error: " + webRequest.error);
				}
				webRequest.Dispose();
				if (loadTextureOp.callbacks != null)
				{
					for (int num3 = 0; num3 < loadTextureOp.callbacks.Count; num3++)
					{
						loadTextureOp.callbacks[num3](texture);
					}
				}
				else
				{
					loadTextureOp.callback(texture);
				}
			}
			else
			{
				num2++;
			}
		}
	}

	private static void processTexture(ref Texture2D texture)
	{
		PlayerSave.Settings properSettings = getProperSettings();
		if (!isUsingRoomEditor && properSettings.customTextureCompression != PlayerSave.TextureCompressionLevel.Uncompressed && texture.width >= 4 && texture.height >= 4)
		{
			if (!Mathf.IsPowerOfTwo(texture.width) || !Mathf.IsPowerOfTwo(texture.height))
			{
				texture = fixTexture(texture);
			}
			texture.Compress(properSettings.customTextureCompression == PlayerSave.TextureCompressionLevel.Pretty);
		}
		texture.filterMode = FilterMode.Trilinear;
		texture.Apply(updateMipmaps: true, makeNoLongerReadable: true);
	}

	public static bool updateLoadWorkshopRoomAssets(EditorContext context)
	{
		updateLoadingTextureOps(context.loadTexturesOps, context.staggeredTextureLoad, context.customRoom?.customFiles);
		updateAssetsRequests(context.assetsRequests);
		if (context.loadTexturesOps.Count == 0 && context.staggeredTextureLoad.Count == 0)
		{
			return context.assetsRequests.Count == 0;
		}
		return false;
	}

	public static bool updateLoadWorkshopRoomCustomModels(EditorContext context)
	{
		updateLoadingCustomModels(context.loadingCustomModels);
		return context.loadingCustomModels.Count == 0;
	}

	private static Texture2D fixTexture(Texture2D originalTexture, string filePath = "")
	{
		int width = Mathf.Max(4, Mathf.ClosestPowerOfTwo(originalTexture.width));
		int height = Mathf.Max(4, Mathf.ClosestPowerOfTwo(originalTexture.height));
		Texture2D texture2D = copyUnreadableTexture(originalTexture, width, height);
		if (!string.IsNullOrEmpty(filePath))
		{
			string text = filePath + "bk";
			if (File.Exists(text))
			{
				File.Delete(text);
			}
			File.Move(filePath, text);
			saveTexture2DToFile(filePath, texture2D);
			texture2D.Apply(updateMipmaps: true, makeNoLongerReadable: true);
		}
		return texture2D;
	}

	private static void saveTexture2DToFile(string filePath, Texture2D texture)
	{
		byte[] bytes = texture.EncodeToPNG();
		File.WriteAllBytes(filePath, bytes);
	}

	private static string getNextAvailableFilePath(string filePath)
	{
		string directoryName = Path.GetDirectoryName(filePath);
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
		string extension = Path.GetExtension(filePath);
		string text = filePath;
		int num = 1;
		while (File.Exists(text) || Directory.Exists(text))
		{
			text = Path.Combine(directoryName, $"{fileNameWithoutExtension}_{num}{extension}");
			num++;
		}
		return text;
	}

	private static bool tryGetInvalidSymbolInFileName(string fileName, out char invalidSymbol)
	{
		foreach (char c in fileName)
		{
			if (!char.IsLetterOrDigit(c) && c != '_' && c != '-' && c != ' ')
			{
				invalidSymbol = c;
				return true;
			}
		}
		invalidSymbol = '\0';
		return false;
	}

	private static void killLoadingTextureOps(List<LoadTextureOp> ops, List<StaggeredTextureLoadInfo> staggered, string killID)
	{
		ops.RemoveAll(delegate(LoadTextureOp x)
		{
			if (x.killID == killID)
			{
				x.asyncOp.webRequest.Abort();
				return true;
			}
			return false;
		});
		staggered.RemoveAll((StaggeredTextureLoadInfo x) => (x.killID == killID) ? true : false);
	}

	private void initTabs()
	{
		EditorUI editorUI = this.editorUI;
		addTab(EditorTab.Settings, editorUI.Settings, editorUI.Settings_Icon, editorUI.Settings_Name);
		addTab(EditorTab.Props, editorUI.Props, editorUI.Props_Icon, editorUI.Props_Name);
		addTab(EditorTab.Logic, editorUI.Logic, editorUI.Logic_Icon, editorUI.Logic_Name);
		addTab(EditorTab.Building, editorUI.Building, editorUI.Building_Icon, editorUI.Building_Name);
		addTab(EditorTab.Assets, editorUI.Assets, editorUI.Assets_Icon, editorUI.Assets_Name);
		currentTab = tabs[0];
		this.editorUI.PropsHeader_Filter_Input.onValueChanged.AddListener(delegate(string filter)
		{
			currentTab.searchFilter = filter;
			this.editorUI.PropsHeader_Filter_SearchClearButton.gameObject.SetActive(filter.Length > 0);
			refreshEditorUI();
		});
		refreshBuildingTab();
		changeTab(EditorTab.Props);
		void addTab(EditorTab type, Button button, UnityEngine.UI.Image icon, Text text)
		{
			Tab tab = new Tab(type)
			{
				button = button,
				icon = icon,
				text = text
			};
			if (type == EditorTab.Logic)
			{
				tab.selectedTag = PropTag.Logic;
			}
			tab.propButtonSize = PropButtonSize.Medium;
			if (type == EditorTab.Assets)
			{
				tab.propButtonSize = PropButtonSize.Big;
			}
			tabs.Add(tab);
		}
	}

	private void refreshBuildingTab()
	{
		PlayerSave.Settings settings = PlayerSave.getSettings();
		Transform parent = editorUI.PrefsUI_Panel.transform;
		inputFieldNavigationDataBuilding.removeOnEndEditListeners();
		removePropertiesForParent(parent);
		startPropertySection(parent, UITooltip.Position.Right);
		defineHeaderProperty("Floors / Walls", "Settings to make working with floors and walls easier.", isFloorsAndWallsSectionShown, delegate(bool isShown)
		{
			isFloorsAndWallsSectionShown = isShown;
			refresh.buildingUI = true;
		}, tabsHeaderColor);
		if (isFloorsAndWallsSectionShown)
		{
			defineVector3Property("Floor Size", "Size of the newly placed floor.\nX and Y values represent the dimensions of the floor, while Z represents its thickness.", () => settings.re.floorSize, delegate(Vector3 size)
			{
				settings.re.floorSize = clampPolygonSize(size);
			}, delegate(Vector3 prevValue, Vector3 nextValue)
			{
				settings.re.floorSize = nextValue;
				PlayerSave.flush();
			});
			defineVector3Property("Wall Size", "Size of the newly placed wall.\nX and Y values represent the dimensions of the wall, while Z represents its thickness.", () => settings.re.wallSize, delegate(Vector3 size)
			{
				settings.re.wallSize = clampPolygonSize(size);
			}, delegate(Vector3 prevValue, Vector3 nextValue)
			{
				settings.re.wallSize = nextValue;
				PlayerSave.flush();
			});
			defineBoolProperty("Snap When Placing", "If enabled, every time you place a new floor or wall, it will automatically be snapped to grid.", settings.re.polygonSnapWhenPlacing, delegate(bool value)
			{
				settings.re.polygonSnapWhenPlacing = value;
				PlayerSave.flush();
			});
			defineBoolProperty("Snap When Editing", "If enabled, when you edit shape of existing floor or wall, points will be snapped to grid.", settings.re.polygonSnapWhenEditing, delegate(bool value)
			{
				polygonTool.alwaysSnapToGrid = value;
				settings.re.polygonSnapWhenEditing = value;
				PlayerSave.flush();
			});
		}
		defineHeaderProperty("Visibility", "Controls visibility of floors, walls and navigation-mesh for easier building.", isVisibilitySectionShown, delegate(bool isShown)
		{
			isVisibilitySectionShown = isShown;
			refresh.buildingUI = true;
		}, tabsHeaderColor);
		if (isVisibilitySectionShown)
		{
			defineBoolProperty("Show Floors", "Whether floors should be shown or hidden.", !isPropHiddenAny<Floor>(), delegate(bool shouldShow)
			{
				toggleHiddenOf<Floor>(shouldShow, includeChildProps: false);
			});
			defineBoolProperty("Show Walls", "Whether walls should be shown or hidden.", !isPropHiddenAny<Wall>(), delegate(bool shouldShow)
			{
				toggleHiddenOf<Wall>(shouldShow, includeChildProps: false);
			});
			defineEnumProperty("Show Nav-Mesh", "Whether the preview of walkable player area is visible.\nEnable to see exactly where player can walk. Available options:\n\nNone - Nav-mesh preview is not shown.\nStanding - Shows area (in blue color) where player can walk while standing.\nCrouching - Shows area (in orange color) where player can walk while crouching.\nBoth - Shows both standing and crouching walkable areas.", (int)settings.re.navMeshVisibility, new List<string> { "None", "Standing", "Crouching", "Both" }, delegate(int value)
			{
				refresh.navMeshPreview = 0;
				settings.re.navMeshVisibility = (PlayerSave.RoomEditorSettings.NavMeshVisibility)value;
				PlayerSave.flush();
			});
		}
		defineHeaderProperty("Grid", "Defines settings of the global grid.", isGridSectionShown, delegate(bool isShown)
		{
			isGridSectionShown = isShown;
			refresh.buildingUI = true;
		}, tabsHeaderColor);
		if (isGridSectionShown)
		{
			defineBoolProperty("Enabled", "Whether the global grid is shown or not.", settings.re.gridEnabled, delegate(bool value)
			{
				settings.re.gridEnabled = value;
				PlayerSave.flush();
			});
			defineFloatProperty("Size", "Dimension of the grid square.", () => settings.re.gridSize, delegate(float value)
			{
				settings.re.gridSize = value;
			}, delegate(float prevValue, float nextValue)
			{
				settings.re.gridSize = nextValue;
				PlayerSave.flush();
			}, null, 0f, float.PositiveInfinity, 0.25f);
			defineFloatProperty("Height", "Controls the Y axis position of the grid.\nUseful for changing the ground elevation for easier building.", () => gridCustomPassVolume.transform.position.y, delegate(float value)
			{
				Vector3 position = gridCustomPassVolume.transform.position;
				position.y = value;
				gridCustomPassVolume.transform.position = position;
			}, delegate
			{
			}, null, float.NegativeInfinity, float.PositiveInfinity, 0.25f);
			defineColorProperty("Color", "Color of the grid.", () => settings.re.gridColor, delegate(Color color)
			{
				settings.re.gridColor = color;
			}, delegate
			{
				PlayerSave.flush();
			});
		}
		defineHeaderProperty("Camera", "Defines settings of the scene camera.", isCameraSectionShown, delegate(bool isShown)
		{
			isCameraSectionShown = isShown;
			refresh.buildingUI = true;
		}, tabsHeaderColor);
		if (isCameraSectionShown)
		{
			defineFloatProperty("Speed", "Speed at which camera moves.\nYou can quickly adjust this value by holding RMB and scrolling.\n\nAlso, hold Shift to move faster, or CTRL to move slower.", () => settings.re.cameraSpeed, delegate(float value)
			{
				settings.re.cameraSpeed = value;
			}, delegate(float prevValue, float nextValue)
			{
				settings.re.cameraSpeed = nextValue;
				PlayerSave.flush();
			}, null, 0.01f, 4f, 0.05f);
			defineVector3Property("Position", "Current camera position.\nUseful for precisely positioning the camera or jumping to a specific coordinate.", () => cameraPosition, delegate(Vector3 value)
			{
				cameraPosition = value;
			}, delegate
			{
			}, null, 0.1f);
			defineVector3Property("Rotation", "Current camera rotation.\nUseful for precisely defining camera's look direction.", () => cameraRotation.eulerAngles, delegate(Vector3 value)
			{
				cameraRotation.eulerAngles = value;
			}, delegate
			{
			}, null, 0.1f);
			defineButtonProperty("Jump To Origin", "Moves camera to origin.\nUsed to quickly reset camera in case of getting lost in the room.", delegate
			{
				editorUI.PrefsUI.gameObject.SetActive(value: false);
				tweenCameraTo(new Vector3(0f, 1f, 0f), mainCam.transform.rotation);
			});
		}
		inputFieldNavigationDataBuilding.initNavigation(parent, "Building");
		static Vector3 clampPolygonSize(Vector3 size)
		{
			float x = Mathf.Clamp(size.x, 1f, 100f);
			float y = Mathf.Clamp(size.y, 1f, 100f);
			float z = Mathf.Clamp(size.z, 0f, 100f);
			return new Vector3(x, y, z);
		}
	}

	private void onTabButtonEvent(Button button, EventTriggerType type)
	{
		Tab tab = tabs.Find((Tab tabButton) => tabButton.button == button);
		if (tab != null && tab.type != currentTab.type)
		{
			switch (type)
			{
			case EventTriggerType.PointerEnter:
			{
				tab.defaultIconColor = tab.icon.color;
				tab.defaultTextColor = tab.text.color;
				Color white = Color.white;
				float? a = 0.5f;
				Color color = white.With(null, null, null, a);
				tab.icon.color = color;
				tab.text.color = color;
				break;
			}
			case EventTriggerType.PointerExit:
				tab.icon.color = tab.defaultIconColor;
				tab.text.color = tab.defaultTextColor;
				break;
			}
		}
	}

	private void onPropButtonSizeToggle(Toggle toggle)
	{
		if (toggle == editorUI.PropsHeader_Sizing_Big)
		{
			currentTab.propButtonSize = PropButtonSize.Big;
			refreshPropButtonSizes();
		}
		if (toggle == editorUI.PropsHeader_Sizing_Med)
		{
			currentTab.propButtonSize = PropButtonSize.Medium;
			refreshPropButtonSizes();
		}
		if (toggle == editorUI.PropsHeader_Sizing_Small)
		{
			currentTab.propButtonSize = PropButtonSize.Small;
			refreshPropButtonSizes();
		}
	}

	private void onTabBack()
	{
		currentTab.selectedTag = PropTag.NONE;
		refresh.editorUI = true;
	}

	private void changeTab(EditorTab nextTab)
	{
		if (currentTab == null || currentTab.type != nextTab)
		{
			bool flag = nextTab == EditorTab.Settings;
			bool flag2 = nextTab == EditorTab.Props;
			bool flag3 = nextTab == EditorTab.Logic;
			bool flag4 = nextTab == EditorTab.Building;
			bool flag5 = nextTab == EditorTab.Assets;
			Themes.ColorsTabButton tabButtonColors = Menu.getTheme().tabButtonColors;
			editorUI.Settings_Icon.color = (flag ? tabButtonColors.selectedColorText : tabButtonColors.unselectedColorText);
			editorUI.Settings_Name.color = (flag ? tabButtonColors.selectedColorText : tabButtonColors.unselectedColorText);
			editorUI.Settings_Name_Selected.gameObject.SetActive(flag);
			editorUI.Props_Icon.color = (flag2 ? tabButtonColors.selectedColorText : tabButtonColors.unselectedColorText);
			editorUI.Props_Name.color = (flag2 ? tabButtonColors.selectedColorText : tabButtonColors.unselectedColorText);
			editorUI.Props_Name_Selected.gameObject.SetActive(flag2);
			editorUI.Logic_Icon.color = (flag3 ? tabButtonColors.selectedColorText : tabButtonColors.unselectedColorText);
			editorUI.Logic_Name.color = (flag3 ? tabButtonColors.selectedColorText : tabButtonColors.unselectedColorText);
			editorUI.Logic_Name_Selected.gameObject.SetActive(flag3);
			editorUI.Building_Icon.color = (flag4 ? tabButtonColors.selectedColorText : tabButtonColors.unselectedColorText);
			editorUI.Building_Name.color = (flag4 ? tabButtonColors.selectedColorText : tabButtonColors.unselectedColorText);
			editorUI.Building_Name_Selected.gameObject.SetActive(flag4);
			editorUI.Assets_Icon.color = (flag5 ? tabButtonColors.selectedColorText : tabButtonColors.unselectedColorText);
			editorUI.Assets_Name.color = (flag5 ? tabButtonColors.selectedColorText : tabButtonColors.unselectedColorText);
			editorUI.Assets_Name_Selected.gameObject.SetActive(flag5);
			editorUI.Info.gameObject.SetActive(flag);
			currentTab = tabs.Find((Tab tab) => tab.type == nextTab);
			refreshEditorUI();
		}
	}

	private void refreshEditorUI()
	{
		bool isTagSelected = currentTab.selectedTag != PropTag.NONE;
		editorUI.PropsHeader_Filter_Input.text = currentTab.searchFilter;
		string filter = editorUI.PropsHeader_Filter_Input.text.ToLower();
		bool isUsingFilter = filter.Length > 0;
		editorUI.PropsHeader.gameObject.SetActive(currentTab.type != EditorTab.Settings);
		editorUI.PropsHeader_Back.gameObject.SetActive(value: false);
		editorUI.PropsHeader_Sizing.gameObject.SetActive(value: false);
		editorUI.SettingsContent.gameObject.SetActive(currentTab.type == EditorTab.Settings);
		GameObject obj = editorUI.PropsRect.gameObject;
		EditorTab type = currentTab.type;
		obj.SetActive(type == EditorTab.Props || type == EditorTab.Logic || type == EditorTab.Building);
		editorUI.EditorAssetBrowserUITABS.gameObject.SetActive(currentTab.type == EditorTab.Assets);
		if (currentTab.type != EditorTab.Props || currentTab.selectedTag == PropTag.NONE)
		{
			editorUI.PropsTagHeader.root.gameObject.SetActive(value: false);
			editorUI.PropsRect.rectT().SetTop(55f);
		}
		else
		{
			editorUI.PropsTagHeader.Text.text = currentTab.selectedTag.ToString().ToUpper();
			editorUI.PropsTagHeader.Back.onClick.RemoveAllListeners();
			editorUI.PropsTagHeader.Back.onClick.AddListener(onTabBack);
			editorUI.PropsTagHeader.Back.gameObject.SetActive(value: true);
			editorUI.PropsTagHeader.root.gameObject.SetActive(value: true);
			editorUI.PropsRect.rectT().SetTop(95f);
		}
		if (isHierarchyOpened())
		{
			editorUI.PropsTagHeader.Text.text = $"Props: {propInstances.Count}";
			if (selection.propInstanceIDs.Count > 0)
			{
				editorUI.PropsTagHeader.Text.text += $" ({selection.propInstanceIDs.Count} selected)".Colored(Color.gray5);
			}
			editorUI.PropsTagHeader.Back.gameObject.SetActive(value: false);
			editorUI.PropsTagHeader.root.gameObject.SetActive(value: true);
			editorUI.PropsRect.rectT().SetTop(95f);
		}
		refreshTabShared();
		if (currentTab.type == EditorTab.Props)
		{
			refreshPropsTab();
		}
		else if (currentTab.type == EditorTab.Logic)
		{
			refreshLogicTab();
		}
		else if (currentTab.type == EditorTab.Building)
		{
			if (isHierarchyEnabled())
			{
				refreshHierarchy();
			}
			else
			{
				refreshBuildingTab();
			}
		}
		else if (currentTab.type == EditorTab.Assets)
		{
			refreshAssetsTab();
		}
		refreshPropButtonSizes();
		void createBuildingSection(PropTag buildingTag)
		{
			string text = buildingTag.ToString();
			int length = "Building".Length;
			createHeader(text.Substring(length, text.Length - length).ToUpper());
			createSpacer(5f);
			createPropButtons(buildingTag, assets.getPropsWithTag(buildingTag), null, hideHeader: true, 10f);
		}
		void createHeader(string title, Action onBackButtonPressed = null)
		{
			HeaderWithBackUI headerWithBackUI = UnityEngine.Object.Instantiate(editorUI.PropsRect_HeaderWithBack, editorUI.PropsRect_Content);
			headerWithBackUI.gameObject.SetActive(value: true);
			headerWithBackUI.Text.text = title;
			if (onBackButtonPressed != null)
			{
				headerWithBackUI.Back.onClick.AddListener(delegate
				{
					onBackButtonPressed();
				});
			}
			else
			{
				headerWithBackUI.Back.gameObject.SetActive(value: false);
			}
		}
		void createLogicSection(string title, LogicProp.Type logicPropType)
		{
			createHeader(title);
			List<Prop> list = new List<Prop>(P_2.logicProps);
			list.RemoveAll((Prop prop) => getLogicPropData(prop.ID).type != logicPropType);
			createPropButtons(currentTab.selectedTag, list, null, hideHeader: false, 10f);
		}
		void createPropButtons(PropTag propTag, List<Prop> props, PropButtonGroupUI propButtonGroup = null, bool hideHeader = false, float endSpacerHeight = -1f)
		{
			if (props.Count != 0)
			{
				bool flag = propTag == PropTag.Logic;
				if (propTag != PropTag.NONE && !flag && !hideHeader)
				{
					Text text = UnityEngine.Object.Instantiate(editorUI.PropsRect_Header, editorUI.PropsRect_Content);
					text.gameObject.SetActive(value: true);
					string text2 = (assets.buildingTags.Exists((TagMeta t) => t.tag == propTag) ? "Building" : string.Empty);
					string text3 = propTag.ToString();
					int length = text2.Length;
					text.text = text3.Substring(length, text3.Length - length).SplitPascalCase();
				}
				if (propButtonGroup == null)
				{
					propButtonGroup = getNewPropButtonGroup(flag);
					propButtonGroup.gameObject.SetActive(value: true);
				}
				foreach (Prop prop in props)
				{
					PropID propID = prop.ID;
					PropButtonUI propButtonUI = getNewPropButton(propButtonGroup, flag);
					propButtonUI.root.onClick.RemoveAllListeners();
					propButtonUI.root.onClick.AddListener(delegate
					{
						EditorSelection newSelection = newPropSelection(propID);
						if (isDifferentSelection(newSelection))
						{
							undo.push(actionChangeSelection(newSelection));
						}
						closeTopUI();
					});
					if (propButtonUI.root.TryGetComponent<PineUITrigger>(out var component))
					{
						component.triggers.Clear();
					}
					definePropDragAndDropButton(propButtonUI, propID);
					PineUI.addButtonListeners(propButtonUI.root);
					if (flag)
					{
						setHoverTooltip(getLogicPropData(propID).tooltip, propButtonUI, UITooltip.Position.Right);
					}
					else
					{
						setHoverTooltip(prop.name, propButtonUI, UITooltip.Position.Right);
					}
					bool flag2 = selection.propID == propID;
					propButtonUI.Selected.enabled = flag2;
					if (propSprites.TryGetValue(propID, out var value))
					{
						propButtonUI.Icon.sprite = value;
					}
					propButtonUI.Name.text = prop.name;
					propButtonUI.Name.gameObject.SetActive(flag);
					UnityEngine.UI.Image favoriteImage = propButtonUI.Favorite.GetComponent<UnityEngine.UI.Image>();
					if (propTag == PropTag.Custom || propTag == PropTag.Premades)
					{
						favoriteImage.enabled = false;
					}
					else
					{
						bool isFavorite = PlayerSave.getSettings().re.favoriteProps.Contains(propID);
						favoriteImage.color = (isFavorite ? Color.yellow : Color.gray);
						favoriteImage.enabled = isFavorite;
						addTriggerEntry(propButtonUI.root.gameObject, EventTriggerType.PointerEnter).callback.AddListener(delegate
						{
							favoriteImage.enabled = true;
						});
						addTriggerEntry(propButtonUI.root.gameObject, EventTriggerType.PointerExit).callback.AddListener(delegate
						{
							favoriteImage.enabled = isFavorite;
						});
						propButtonUI.Favorite.onClick.RemoveAllListeners();
						propButtonUI.Favorite.onClick.AddListener(delegate
						{
							List<PropID> favoriteProps = PlayerSave.getSettings().re.favoriteProps;
							if (favoriteProps.Contains(propID))
							{
								favoriteProps.Remove(propID);
							}
							else
							{
								favoriteProps.Add(propID);
							}
							isFavorite = favoriteProps.Contains(propID);
							favoriteImage.color = (isFavorite ? Color.yellow : Color.gray);
							PlayerSave.flush();
						});
					}
				}
				createSpacer(endSpacerHeight);
			}
		}
		void createSpacer(float height = -1f)
		{
			RectTransform rectTransform = UnityEngine.Object.Instantiate(editorUI.PropsRect_Spacer, editorUI.PropsRect_Content);
			rectTransform.gameObject.SetActive(value: true);
			if (height != -1f)
			{
				rectTransform.GetComponent<LayoutElement>().preferredHeight = height;
			}
		}
		void createTagButtons(List<TagMeta> tags, bool background = false)
		{
			TagButtonGroupUI tagButtonGroupUI = UnityEngine.Object.Instantiate(editorUI.PropsRect_TagButtonGroup, editorUI.PropsRect_Content);
			tagButtonGroupUI.gameObject.SetActive(value: true);
			foreach (TagMeta tag in tags)
			{
				PropTag capturedTag = tag.tag;
				if (!tag.isHidden)
				{
					TagButtonUI tagButtonUI = UnityEngine.Object.Instantiate(tagButtonGroupUI.TagButton, tagButtonGroupUI.transform);
					tagButtonUI.gameObject.SetActive(value: true);
					if (background)
					{
						tagButtonUI.Background.enabled = true;
					}
					string text = (assets.buildingTags.Exists((TagMeta t) => t.tag == capturedTag) ? "Building" : string.Empty);
					Text text2 = tagButtonUI.Text;
					string text3 = capturedTag.ToString();
					int length = text.Length;
					text2.text = text3.Substring(length, text3.Length - length).SplitPascalCase();
					if (tag.icon != null)
					{
						tagButtonUI.Icon.sprite = tag.icon;
						tagButtonUI.Icon.color = Color.white;
					}
					tagButtonUI.root.onClick.RemoveAllListeners();
					tagButtonUI.root.onClick.AddListener(delegate
					{
						if (capturedTag == PropTag.Custom)
						{
							lastCheckedModelImports = Time.time;
							loadCustomProps(getEditorContext(), roomDirPath, levelContainer.customModelsParent.transform, delegate
							{
								refresh.editorUI = true;
								refresh.propertiesUI = true;
							}, fromEditor: true, propSprites, propIcons, missingModelImage);
						}
						editorUI.PropsRect_ES2ScrollbarVertical.GetComponent<Scrollbar>().value = 1f;
						currentTab.selectedTag = capturedTag;
						refresh.editorUI = true;
						closeTopUI();
					});
				}
			}
		}
		static PropButtonUI getNewPropButton(PropButtonGroupUI groupUI, bool isLogic)
		{
			PropButtonUI propButtonUI = null;
			for (int i = 0; i < groupUI.transform.childCount; i++)
			{
				Transform child = groupUI.transform.GetChild(i);
				if (!(child == groupUI.PropButton.transform) && !(child == groupUI.PropButtonLogic.transform) && !child.gameObject.activeSelf)
				{
					PropButtonUI component = child.GetComponent<PropButtonUI>();
					if ((bool)component.data == isLogic)
					{
						propButtonUI = component;
						propButtonUI.transform.SetAsLastSibling();
						break;
					}
				}
			}
			if (propButtonUI == null)
			{
				propButtonUI = UnityEngine.Object.Instantiate(isLogic ? groupUI.PropButtonLogic : groupUI.PropButton, groupUI.transform);
				propButtonUI.data = isLogic;
			}
			else
			{
				removeTriggerEntry(propButtonUI.root.gameObject, EventTriggerType.PointerEnter);
				removeTriggerEntry(propButtonUI.root.gameObject, EventTriggerType.PointerExit);
			}
			propButtonUI.gameObject.SetActive(value: true);
			return propButtonUI;
		}
		PropButtonGroupUI getNewPropButtonGroup(bool isLogic)
		{
			PropButtonGroupUI propButtonGroupUI;
			if (cachedPropGroups.Count > 0)
			{
				int index = cachedPropGroups.Count - 1;
				propButtonGroupUI = cachedPropGroups[index];
				cachedPropGroups.RemoveAt(index);
				propButtonGroupUI.transform.SetAsLastSibling();
			}
			else
			{
				propButtonGroupUI = UnityEngine.Object.Instantiate(editorUI.PropsRect_PropButtonGroup, editorUI.PropsRect_Content);
			}
			GridLayoutGroup component = propButtonGroupUI.GetComponent<GridLayoutGroup>();
			if (isLogic)
			{
				component.padding = new RectOffset(0, 0, 0, 0);
				component.spacing = new Vector2(0f, 0f);
				component.cellSize = new Vector2(150f, 50f);
			}
			else
			{
				component.padding = new RectOffset(5, 5, 0, 0);
				component.spacing = new Vector2(5f, 5f);
				component.cellSize = new Vector2(128f, 50f);
			}
			propButtonGroupUI.gameObject.SetActive(value: true);
			return propButtonGroupUI;
		}
		void refreshBuildingTab()
		{
			if (isTagSelected)
			{
				createPropButtons(currentTab.selectedTag, getFilteredProps(assets.getPropsWithTag(currentTab.selectedTag), filter));
			}
			else if (isUsingFilter)
			{
				HashSet<PropTag> hashSet = new HashSet<PropTag>();
				foreach (TagMeta buildingTag in assets.buildingTags)
				{
					hashSet.Add(buildingTag.tag);
				}
				List<Prop> propsWithAnyOfTags = assets.getPropsWithAnyOfTags(hashSet);
				createPropButtons(PropTag.NONE, getFilteredProps(propsWithAnyOfTags, filter));
			}
			else
			{
				createBuildingSection(PropTag.BuildingFloors);
				createBuildingSection(PropTag.BuildingWalls);
				createBuildingSection(PropTag.BuildingStairs);
				createBuildingSection(PropTag.BuildingDoors);
				createBuildingSection(PropTag.BuildingWindows);
			}
		}
		void refreshLogicTab()
		{
			List<Prop> logicProps = assets.getPropsWithTag(PropTag.Logic);
			if (Is.Release)
			{
				logicProps.RemoveAll((Prop prop) => prop.ID.value == "Logics/Test");
			}
			if (isUsingFilter)
			{
				createPropButtons(currentTab.selectedTag, getFilteredProps(logicProps, filter));
			}
			else
			{
				createLogicSection("GAMEPLAY", LogicProp.Type.Gameplay);
				createLogicSection("MOVEMENT", LogicProp.Type.Movement);
				createLogicSection("VISUAL / AUDIO", LogicProp.Type.VisualAndAudio);
			}
		}
		void refreshPropsTab()
		{
			Dictionary<PropTag, List<Prop>> subgroups;
			if (!isTagSelected)
			{
				if (isUsingFilter)
				{
					createPropButtons(PropTag.NONE, getFilteredProps(assets.getPropsAll(), filter));
				}
				else
				{
					RectTransform propsRect_Content = editorUI.PropsRect_Content;
					startPropertySection(propsRect_Content, UITooltip.Position.Right);
					defineHeaderProperty("Special", "Special, commonly used and custom made props.", currentTab.isSpecialTagsSectionShown, delegate(bool isShown)
					{
						currentTab.isSpecialTagsSectionShown = isShown;
						refresh.editorUI = true;
					}, tabsHeaderColor);
					if (currentTab.isSpecialTagsSectionShown)
					{
						defineSeparatorProperty(10f);
						createTagButtons(assets.specialTags);
						defineSeparatorProperty(10f);
					}
					defineHeaderProperty("Themes", "Each group holds props belonging to specific room theme.", currentTab.isThemeTagsSectionShown, delegate(bool isShown)
					{
						currentTab.isThemeTagsSectionShown = isShown;
						refresh.editorUI = true;
					}, tabsHeaderColor);
					if (currentTab.isThemeTagsSectionShown)
					{
						defineSeparatorProperty(10f);
						createTagButtons(assets.themeTags);
						defineSeparatorProperty(10f);
					}
					defineHeaderProperty("Categories", "Each group holds props belonging to specific category.", currentTab.isCategoryTagsSectionShown, delegate(bool isShown)
					{
						currentTab.isCategoryTagsSectionShown = isShown;
						refresh.editorUI = true;
					}, tabsHeaderColor);
					if (currentTab.isCategoryTagsSectionShown)
					{
						defineSeparatorProperty(10f);
						List<TagMeta> list = new List<TagMeta>(assets.categoryTags);
						list.RemoveAll((TagMeta tagMeta) => !assets.usedPropTags.Contains(tagMeta.tag));
						createTagButtons(list);
						defineSeparatorProperty(10f);
					}
				}
			}
			else if (currentTab.selectedTag == PropTag.Shapes)
			{
				List<Prop> propsWithTag = assets.getPropsWithTag(PropTag.Primitives);
				createPropButtons(PropTag.Primitives, getFilteredProps(propsWithTag, filter));
				List<Prop> propsWithTag2 = assets.getPropsWithTag(PropTag.Shapes);
				createPropButtons(PropTag.Shapes, getFilteredProps(propsWithTag2, filter));
			}
			else if (currentTab.selectedTag == PropTag.Effects)
			{
				List<Prop> propsWithTag3 = assets.getPropsWithTag(PropTag.Effects);
				createPropButtons(PropTag.Effects, getFilteredProps(propsWithTag3, filter), null, hideHeader: true);
			}
			else if (currentTab.selectedTag == PropTag.Custom)
			{
				PropButtonGroupUI propButtonGroupUI = getNewPropButtonGroup(isLogic: false);
				propButtonGroupUI.gameObject.SetActive(value: true);
				PropButtonUI propButtonUI = getNewPropButton(propButtonGroupUI, isLogic: false);
				propButtonUI.Favorite.GetComponent<UnityEngine.UI.Image>().enabled = false;
				setHoverTooltip("Import Model", propButtonUI, UITooltip.Position.Right);
				propButtonUI.Icon.sprite = plusIcon;
				propButtonUI.Name.text = "Add new Custom Model";
				propButtonUI.Name.gameObject.SetActive(value: false);
				propButtonUI.root.onClick.RemoveAllListeners();
				propButtonUI.root.onClick.AddListener(delegate
				{
					openFileBrowserForType(typeof(ModelAsset));
				});
				List<Prop> list2 = new List<Prop>();
				foreach (CustomModelObject customModelPrefab in customModelPrefabs)
				{
					list2.Add(customModelPrefab.prop);
				}
				createPropButtons(currentTab.selectedTag, getFilteredProps(list2, filter), propButtonGroupUI);
				propButtonGroupUI.transform.SetSiblingIndex(propButtonGroupUI.transform.GetSiblingIndex() + 1);
			}
			else if (currentTab.selectedTag == PropTag.Favorites)
			{
				List<PropID> favoriteProps = PlayerSave.getSettings().re.favoriteProps;
				List<Prop> props = assets.getProps((Prop prop) => favoriteProps.Contains(prop.ID));
				createPropButtons(currentTab.selectedTag, getFilteredProps(props, filter), null, hideHeader: true);
			}
			else if (currentTab.selectedTag == PropTag.Premades)
			{
				PropButtonGroupUI propButtonGroupUI2 = getNewPropButtonGroup(isLogic: false);
				propButtonGroupUI2.gameObject.SetActive(value: true);
				PropButtonUI propButtonUI2 = getNewPropButton(propButtonGroupUI2, isLogic: false);
				propButtonUI2.Favorite.GetComponent<UnityEngine.UI.Image>().enabled = false;
				propButtonUI2.root.onClick.RemoveAllListeners();
				propButtonUI2.root.onClick.AddListener(delegate
				{
					enterTargetMode((PropInstance _) => true, null, delegate(PropInstance pi, int _)
					{
						currentlySavingPremade = pi;
						optionDialog.showNoTranslate("Premade name", "Give your premade a name", onFrameClick, new VisualControl("newPremade", ControllerButtonActionType.UIConfirmPrimary, "%RoomEditor_OK%", VisualControl.ControlType.Button, VisualControl.Position.BotLeft));
						optionDialog.useInputField("newPremade");
					});
				});
				setHoverTooltip("Create Premade", propButtonUI2, UITooltip.Position.Right);
				propButtonUI2.Icon.sprite = plusIcon;
				propButtonUI2.Name.text = "Add new premade";
				propButtonUI2.Name.gameObject.SetActive(value: false);
			}
			else if (assets.categoryTags.Exists((TagMeta tagMeta) => tagMeta.tag == currentTab.selectedTag))
			{
				List<Prop> propsWithTag4 = assets.getPropsWithTag(currentTab.selectedTag);
				createPropButtons(currentTab.selectedTag, getFilteredProps(propsWithTag4, filter), null, hideHeader: true);
			}
			else
			{
				if (assets.themeTags.Exists((TagMeta tagMeta) => tagMeta.tag == currentTab.selectedTag))
				{
					List<Prop> propsWithTag5 = assets.getPropsWithTag(currentTab.selectedTag);
					subgroups = new Dictionary<PropTag, List<Prop>>();
					foreach (Prop item in propsWithTag5)
					{
						if (item.tags.Count == 1)
						{
							addProp(currentTab.selectedTag, item);
						}
						else
						{
							foreach (PropTag tag2 in item.tags)
							{
								if (tag2 != currentTab.selectedTag)
								{
									addProp(tag2, item);
									break;
								}
							}
						}
					}
					if (subgroups.TryGetValue(currentTab.selectedTag, out var value))
					{
						createPropButtons(currentTab.selectedTag, getFilteredProps(value, filter));
						subgroups.Remove(currentTab.selectedTag);
					}
					{
						foreach (var (propTag2, props2) in subgroups)
						{
							createPropButtons(propTag2, getFilteredProps(props2, filter));
						}
						return;
					}
				}
				List<Prop> propsWithTag6 = assets.getPropsWithTag(currentTab.selectedTag);
				createPropButtons(currentTab.selectedTag, getFilteredProps(propsWithTag6, filter));
			}
			void addProp(PropTag key, Prop prop)
			{
				if (!subgroups.TryGetValue(key, out var value2))
				{
					value2 = new List<Prop>();
					subgroups.Add(key, value2);
				}
				value2.Add(prop);
			}
		}
		void refreshTabShared()
		{
			for (int num = editorUI.PropsRect_Content.childCount - 1; num >= 0; num--)
			{
				Transform child = editorUI.PropsRect_Content.GetChild(num);
				if (child.gameObject.activeSelf)
				{
					if (child.TryGetComponent<PropButtonGroupUI>(out var component))
					{
						for (int i = 0; i < component.transform.childCount; i++)
						{
							component.transform.GetChild(i).gameObject.SetActive(value: false);
						}
						component.gameObject.SetActive(value: false);
						cachedPropGroups.Add(component);
					}
					else
					{
						UnityEngine.Object.DestroyImmediate(child.gameObject);
					}
				}
			}
			removePropertiesForParent(editorUI.PropsRect_Content);
		}
	}

	private void refreshPropButtonSizes()
	{
		if (currentTab.type == EditorTab.Logic)
		{
			for (int num = editorUI.PropsRect_Content.childCount - 1; num >= 0; num--)
			{
				Transform child = editorUI.PropsRect_Content.GetChild(num);
				if (child.gameObject.activeSelf && child.TryGetComponent<PropButtonGroupUI>(out var _))
				{
					GridLayoutGroup component2 = child.GetComponent<GridLayoutGroup>();
					component2.cellSize = new Vector2(150f, 50f);
					component2.constraintCount = 2;
				}
			}
			return;
		}
		int num2 = (new int[3] { 5, 4, 3 })[(int)currentTab.propButtonSize];
		float width = editorUI.PropsRect_Content.rect.width;
		for (int num3 = editorUI.PropsRect_Content.childCount - 1; num3 >= 0; num3--)
		{
			Transform child2 = editorUI.PropsRect_Content.GetChild(num3);
			if (child2.gameObject.activeSelf && child2.TryGetComponent<PropButtonGroupUI>(out var _))
			{
				GridLayoutGroup component4 = child2.GetComponent<GridLayoutGroup>();
				float num4 = (width - (float)component4.padding.left - (float)component4.padding.right - component4.spacing.x * (float)(num2 - 1)) / (float)num2;
				component4.cellSize = new Vector2(num4, num4);
				component4.constraintCount = num2;
			}
		}
	}

	private void refreshAssetsTab()
	{
		Type type = (Type)editorUI.EditorAssetBrowserUITABS.data;
		if (type == null || type == typeof(UserAsset))
		{
			openAssetWith<UserAsset>();
		}
		else if (type == typeof(ScriptAsset))
		{
			openAssetWith<ScriptAsset>();
		}
		else if (type == typeof(TextureAsset))
		{
			openAssetWith<TextureAsset>();
		}
		else if (type == typeof(MaterialAsset))
		{
			openAssetWith<MaterialAsset>();
		}
		else if (type == typeof(AudioAsset))
		{
			openAssetWith<AudioAsset>();
		}
		else if (type == typeof(ModelAsset))
		{
			openAssetWith<ModelAsset>();
		}
		void openAssetWith<T>() where T : UserAsset
		{
			clearAssetBrowser(editorUI.EditorAssetBrowserUITABS);
			closeAssetBrowser(editorUI.EditorAssetBrowserUITABS);
			Action<Type> onAddNewAsset = delegate(Type type2)
			{
				onAddNewAssetPressed(type2, isAssetPicker: false);
			};
			openAssetBrowserBase<T>(openUserAssetEditor, onAddNewAsset, editorUI.EditorAssetBrowserUITABS, editorUI.PropsHeader_Filter_Input.text);
		}
	}

	private static List<Prop> getFilteredProps(List<Prop> props, string filter)
	{
		if (string.IsNullOrEmpty(filter))
		{
			return props;
		}
		List<Prop> list = new List<Prop>();
		List<Prop> list2 = new List<Prop>();
		List<Prop> list3 = new List<Prop>();
		foreach (Prop prop in props)
		{
			switch (matchString(prop.name, filter))
			{
			case SearchMatchResult.ExactMatch:
				list.Add(prop);
				break;
			case SearchMatchResult.StartsWithMatch:
				list2.Add(prop);
				break;
			case SearchMatchResult.ContainsMatch:
				list3.Add(prop);
				break;
			default:
				_ = 3;
				break;
			}
		}
		List<Prop> list4 = new List<Prop>();
		list4.AddRange(list);
		list4.AddRange(list2);
		list4.AddRange(list3);
		return list4;
	}

	private static SearchMatchResult matchString(string input, string filter)
	{
		if (string.IsNullOrEmpty(filter))
		{
			return SearchMatchResult.ContainsMatch;
		}
		if (string.IsNullOrEmpty(input))
		{
			return SearchMatchResult.NoMatch;
		}
		input = input.ToLowerInvariant();
		filter = filter.ToLowerInvariant();
		if (input == filter)
		{
			return SearchMatchResult.ExactMatch;
		}
		if (input.StartsWith(filter))
		{
			return SearchMatchResult.StartsWithMatch;
		}
		if (input.Contains(filter))
		{
			return SearchMatchResult.ContainsMatch;
		}
		return SearchMatchResult.NoMatch;
	}

	private void initPublishingUI()
	{
		publishingRoomInfo = new Menu.WorkshopRoomInfo();
		publishingRoomInfo.roomTitle = "MyRoom";
		publishingRoomInfo.roomTags = new List<string>();
		publishingRoomInfo.roomWalkthrough = new List<string>();
		publishInfo.init();
		publishInfo.canActivateSaveOnExitModal = false;
		publishInfo.onPublishSucess = delegate
		{
			toggleCanvasGroupUI(metaUI.gameObject, visible: false);
		};
		publishInfo.onScreenshotButton = delegate
		{
			toggleScreenshotMode(show: true);
		};
		publishInfo.onPublishStart = delegate
		{
			saveRoom();
		};
		publishInfo.onClosePublish = delegate(bool success)
		{
			publishingRoomInfo.roomTitle = publishInfo.ui.Publish_Description_BasicInformationContent_Layout_Name_InputField.text;
			publishingRoomInfo.roomDescription = publishInfo.ui.Publish_Description_BasicInformationContent_DescriptionInput.text;
			List<string> walkthrough;
			bool activeWalkthrough = getActiveWalkthrough(out walkthrough);
			if (publishInfo.getActiveTags(out var output, activeWalkthrough))
			{
				publishingRoomInfo.roomTags = output;
			}
			if (activeWalkthrough)
			{
				publishingRoomInfo.roomWalkthrough = walkthrough;
			}
			if (success)
			{
				toggleCanvasGroupUI(metaUI.gameObject, visible: false);
			}
			refresh.propertiesUI = true;
		};
		onDeleteHeader = delegate(int deletedIndex)
		{
			List<EditorAction> list = new List<EditorAction>();
			foreach (KeyValuePair<InstanceID, PropInstance> propInstance in propInstances)
			{
				if (propInstance.Value.TryGetComponent<EditorPuzzle>(out var _))
				{
					var (prev, propData) = prepChangeData(propInstance.Value);
					fixIndexes(ref getPuzzleData(propData).hints);
					list.Add(actionChangeProp(prev, propData, "Adjust puzzle index"));
				}
			}
			undo.push(list.ToArray());
			void fixIndexes(ref List<int> hints)
			{
				for (int num = hints.Count - 1; num >= 0; num--)
				{
					if (hints[num] > deletedIndex)
					{
						UnityEngine.Debug.Log("FIXING from: " + hints[num] + " to: " + (hints[num] - 1));
						hints[num] -= 1;
					}
					else if (hints[num] == deletedIndex)
					{
						UnityEngine.Debug.Log("REMOVING: " + hints[num]);
						hints.RemoveAt(num);
					}
				}
			}
		};
		publishInfo.ui.Publish_Footer_CancelButton_Text.text = "%close%";
		Localization.translateObject(publishInfo.transform, forceNewText: true);
	}

	private void initPublishingWorkshopRoomInfo(RoomData roomData)
	{
		publishingRoomInfo.authorId = SteamUser.GetSteamID().m_SteamID;
		publishingRoomInfo.authorName = SteamFriends.GetPersonaName();
		publishingRoomInfo.roomId = Menu.readIdFromDisk(roomDirPath);
		publishingRoomInfo.roomTitle = roomData.name;
		publishingRoomInfo.roomDescription = roomData.description;
		publishingRoomInfo.roomTags = new List<string>(roomData.tags);
		publishingRoomInfo.roomWalkthrough = new List<string>(roomData.walkthrough);
		publishingRoomInfo.roomLocalPath = roomDirPath;
		publishingRoomInfo.roomPreviewImageUrl = "file:///" + roomDirPath + "/Preview.jpg";
		string path = Path.Combine(roomDirPath, "Preview.jpg");
		if (!File.Exists(path))
		{
			publishingRoomInfo.roomPreviewImage = takePreviewScreenshot();
		}
		else
		{
			byte[] data = File.ReadAllBytes(path);
			publishingRoomInfo.roomPreviewImage = new Texture2D(2, 2, TextureFormat.RGBA32, mipChain: true);
			publishingRoomInfo.roomPreviewImage.LoadImage(data, markNonReadable: false);
		}
		string path2 = Path.Combine(roomDirPath, "tutorial.bin");
		if (File.Exists(path2))
		{
			publishingRoomInfo.tutorialId = File.ReadAllText(path2);
		}
		publishingRoomInfo.isInYourRooms = true;
	}

	private Texture2D takePreviewScreenshot()
	{
		Vector3 position = mainCam.transform.position;
		Quaternion rotation = mainCam.transform.rotation;
		int cullingMask = mainCam.cullingMask;
		bool flag = gridCustomPass?.enabled ?? false;
		bool flag2 = gizmoAxisCustomPass?.enabled ?? false;
		mainCam.cullingMask = getScreenshotCameraCullingLayers(mainCam.cullingMask);
		mainCam.transform.position = cameraScreenshotPosition;
		mainCam.transform.rotation = cameraScreenshotRotation;
		if (gridCustomPass != null)
		{
			gridCustomPass.enabled = false;
		}
		if (gizmoAxisCustomPass != null)
		{
			gizmoAxisCustomPass.enabled = false;
		}
		RenderTexture renderTexture = new RenderTexture(1280, 720, 24);
		mainCam.targetTexture = renderTexture;
		Texture2D texture2D = new Texture2D(1280, 720, TextureFormat.RGB24, mipChain: false);
		mainCam.Render();
		RenderTexture.active = renderTexture;
		texture2D.ReadPixels(new Rect(0f, 0f, 1280f, 720f), 0, 0);
		mainCam.targetTexture = null;
		RenderTexture.active = null;
		UnityEngine.Object.Destroy(renderTexture);
		mainCam.transform.position = position;
		mainCam.transform.rotation = rotation;
		mainCam.cullingMask = cullingMask;
		if (gridCustomPass != null)
		{
			gridCustomPass.enabled = flag;
		}
		if (gizmoAxisCustomPass != null)
		{
			gizmoAxisCustomPass.enabled = flag2;
		}
		texture2D.Apply();
		publishingRoomInfo.roomPreviewImage = texture2D;
		return texture2D;
	}

	private static int getScreenshotCameraCullingLayers(int cullingMask)
	{
		string[] array = new string[4] { "UI", "RoomEditorSpecialObject", "PinnedItems", "IgnoreTrigger" };
		foreach (string text in array)
		{
			int num = LayerMask.NameToLayer(text);
			if (num == -1)
			{
				UnityEngine.Debug.LogError("Layer named '" + text + "' does not exist.");
				continue;
			}
			int num2 = 1 << num;
			cullingMask &= ~num2;
		}
		return cullingMask;
	}

	private void updateRefresh()
	{
		if (refresh.materialEditorUI)
		{
			refresh.materialEditorUI = false;
			refreshMaterialEditor();
		}
		if (refresh.hiddenPropsMessageUI)
		{
			refresh.hiddenPropsMessageUI = false;
			refreshHiddenPropsMessage();
		}
		if (refresh.propCountUI)
		{
			refresh.propCountUI = false;
			editorUI.PropCount.text = $"Prop Count: {propInstances.Count}";
		}
		if (refresh.propertiesUI && !interactionInProgress())
		{
			refresh.propertiesUI = false;
			refreshTargetPropertiesUI();
		}
		if (refresh.buildingUI)
		{
			refresh.buildingUI = false;
			refreshBuildingTab();
		}
		if (refresh.userAssets)
		{
			refresh.userAssets = false;
			refreshUserAssets();
			wasOnApplicationFocus = false;
		}
		if (refresh.unloadUnusedAssets)
		{
			refresh.unloadUnusedAssets = false;
			Resources.UnloadUnusedAssets();
		}
		if (refresh.skybox)
		{
			refresh.skybox = false;
			bool flag = false;
			if (isPropInstanceSelected(out var count) && count == 1 && getSelectedInstance().TryGetComponent<EditorSkybox>(out var component))
			{
				flag = true;
				Game.applySkyData(editorVolumeRefs, component.data);
			}
			if (!flag && !restoreSkybox(skyboxCurrentActiveCache))
			{
				bool flag2 = false;
				if (useSkyboxPreview)
				{
					if (skyboxOnStartActiveCache != null && skyboxOnStartActiveCache.type == SkyboxType.SixSided)
					{
						foreach (KeyValuePair<InstanceID, PropInstance> propInstance4 in propInstances)
						{
							if (propInstance4.Value.TryGetComponent<EditorSkybox>(out var component2) && component2.data.activeOnStart && component2.data.type == SkyboxType.SixSided)
							{
								skyboxOnStartActiveCache = component2.data;
								break;
							}
						}
					}
					flag2 = restoreSkybox(skyboxOnStartActiveCache);
				}
				if (!flag2)
				{
					restoreSkybox(skyboxDefaultCache);
				}
			}
		}
		if (refresh.clouds)
		{
			refresh.clouds = false;
			bool flag3 = false;
			if (isPropInstanceSelected(out var count2) && count2 == 1 && getSelectedInstance().TryGetComponent<EditorClouds>(out var component3))
			{
				flag3 = true;
				Game.applyClouds(editorVolumeRefs, component3.data);
			}
			if (!flag3 && !restoreClouds(cloudsCurrentActiveCache) && !restoreClouds(cloudsOnStartActiveCache))
			{
				restoreClouds(cloudsDefaultCache);
			}
		}
		if (refresh.ocean)
		{
			refresh.ocean = false;
			bool flag4 = false;
			if (isPropInstanceSelected(out var count3) && count3 == 1)
			{
				PropInstance propInstance = null;
				foreach (InstanceID propInstanceID in selection.propInstanceIDs)
				{
					propInstance = getInstanceByID(propInstanceID);
				}
				if (propInstance.TryGetComponent<EditorOcean>(out var component4))
				{
					flag4 = true;
					Game.applyOcean(editorVolumeRefs, component4.data);
				}
			}
			if (!flag4 && !restoreOcean(oceanCurrentActiveCache, force: false) && !restoreOcean(oceanOnStartActiveCache, force: false))
			{
				restoreOcean(oceanDefaultCache, force: true);
			}
		}
		if (refresh.fog)
		{
			refresh.fog = false;
			bool flag5 = false;
			if (isPropInstanceSelected(out var count4) && count4 == 1)
			{
				PropInstance propInstance2 = null;
				foreach (InstanceID propInstanceID2 in selection.propInstanceIDs)
				{
					propInstance2 = getInstanceByID(propInstanceID2);
				}
				if (propInstance2.TryGetComponent<Fog>(out var component5))
				{
					flag5 = true;
					Game.applyFogData(editorVolumeRefs, component5.data);
				}
			}
			if (!flag5 && !restoreFog(fogCurrentActiveCache) && !restoreFog(fogOnStartActiveCache))
			{
				restoreFog(fogDefaultCache);
			}
		}
		if (refresh.postProcessing)
		{
			refresh.postProcessing = false;
			bool flag6 = false;
			if (isPropInstanceSelected(out var count5) && count5 == 1)
			{
				PropInstance propInstance3 = null;
				foreach (InstanceID propInstanceID3 in selection.propInstanceIDs)
				{
					propInstance3 = getInstanceByID(propInstanceID3);
				}
				if (propInstance3.TryGetComponent<EditorPostProcessing>(out var _))
				{
					flag6 = true;
					editorVolumeRefs.volume.enabled = true;
					EditorPostProcessingData editorPostProcessingData = getEditorPostProcessingData(compressProp(propInstance3, getEditorContext()));
					setCurrentPostProcessingVolume(editorPostProcessingData);
				}
			}
			if (!flag6)
			{
				EditorPostProcessingData editorPostProcessingData2 = editorCurrentActivePostProcessing;
				if (editorPostProcessingData2 == null)
				{
					editorPostProcessingData2 = (usePostProcessingPreview ? editorPostProcessingOnStartActive : null);
					if (editorPostProcessingData2 == null)
					{
						editorPostProcessingData2 = editorPostProcessingDefault;
						editorVolumeRefs.volume.enabled = false;
					}
					else
					{
						editorVolumeRefs.volume.enabled = true;
					}
				}
				else
				{
					editorVolumeRefs.volume.enabled = true;
				}
				setCurrentPostProcessingVolume(editorPostProcessingData2);
			}
		}
		if (refresh.editorDisplay)
		{
			if (refresh.editorDisplayCount)
			{
				refresh.editorDisplayCount = false;
				allDisplays = new List<EditorDisplay>(levelContainer.GetComponentsInChildren<EditorDisplay>(includeInactive: true));
			}
			refresh.editorDisplay = false;
			Game.processDisplays(allDisplays, runningInEditor: true);
		}
		if (refresh.transforms)
		{
			refresh.transforms = false;
			Physics.SyncTransforms();
		}
		if (refresh.hierarchyUI >= 0 && refresh.hierarchyUI-- == 0 && isHierarchyOpened())
		{
			refresh.editorUI = true;
		}
		if (refresh.editorUI)
		{
			refresh.editorUI = false;
			refreshEditorUI();
		}
		if (refresh.navMeshPreview >= 0 && refresh.navMeshPreview-- == 0)
		{
			refreshNavMeshPreview();
		}
		bool restoreClouds(CloudsData data)
		{
			if (data != null)
			{
				Game.applyClouds(editorVolumeRefs, data);
				return true;
			}
			return false;
		}
		bool restoreFog(FogData data)
		{
			if (data != null)
			{
				Game.applyFogData(editorVolumeRefs, data);
				return true;
			}
			return false;
		}
		bool restoreOcean(OceanData data, bool force)
		{
			if (data != null || force)
			{
				Game.applyOcean(editorVolumeRefs, data);
				return true;
			}
			return false;
		}
		bool restoreSkybox(SkyboxData data)
		{
			if (data != null)
			{
				Game.applySkyData(editorVolumeRefs, data);
				return true;
			}
			return false;
		}
	}

	public static (WorkshopRoom, LoadRoomResult) startLoadingWorkshopRoom(LevelContainerEditor container, string dirPath, UnpackedCustomRoom overrideRoomData = null)
	{
		RoomData roomData = new RoomData();
		LoadRoomResult loadRoomResult = LoadRoomResult.Success;
		if (overrideRoomData == null)
		{
			(roomData, loadRoomResult) = loadRoomDataFromPath(Path.Combine(dirPath, "Room.room"));
		}
		else
		{
			roomData = overrideRoomData.roomData;
			UnityEngine.Debug.Log("loaded from override Room data");
		}
		WorkshopRoom workshopRoom = null;
		if (loadRoomResult == LoadRoomResult.Success)
		{
			ensureBackwardCompatibility(roomData);
			workshopRoom = new WorkshopRoom();
			workshopRoom.context = new EditorContext
			{
				container = container,
				propInstances = new Dictionary<InstanceID, PropInstance>(),
				roomDirPath = dirPath,
				loadTexturesOps = new List<LoadTextureOp>(),
				staggeredTextureLoad = new List<StaggeredTextureLoadInfo>(),
				assetsBundles = loadAssetsBundles(),
				assetsRequests = new List<AssetRequest>(),
				refresh = new RefreshFlags(),
				customModels = new List<CustomModelObject>(),
				loadingCustomModels = new List<CustomModelRequest>(),
				soundPointersCache = new Dictionary<string, long>(),
				customRoom = overrideRoomData
			};
			if (isUsingRoomEditor)
			{
				workshopRoom.context.assetsIconsBundle = loadAssetsIconsBundle();
			}
			try
			{
				workshopRoom.context.useNonLegacyLights = roomData.useNonLegacyLights;
				workshopRoom.context.hidePlayerNameplates = roomData.hidePlayerNameplates;
				workshopRoom.context.hideItemNameplates = roomData.hideItemNameplates;
				workshopRoom.context.useProximityChat = roomData.useProximityChat;
				workshopRoom.context.useSkyboxPreview = roomData.useSkyboxPreview;
				workshopRoom.context.useWaterPreview = roomData.useWaterPreview;
				workshopRoom.context.usePostProcessingPreview = roomData.usePostProcessingPreview;
				loadCustomProps(workshopRoom.context, dirPath, container.customModelsParent.transform, null, fromEditor: false, null, null, null, overrideRoomData != null);
				workshopRoom.roomData = roomData;
				PineFmod.lockChannelGroup(PineFmod.getBus("bus:/Music"));
				PineFmod.lockChannelGroup(PineFmod.getBus("bus:/Sound Effects"));
				PineFmod.flushCommands();
			}
			catch (Exception ex)
			{
				workshopRoom = null;
				loadRoomResult = LoadRoomResult.Failed;
				UnityEngine.Debug.Log(ex.Message);
				UnityEngine.Debug.Log(ex.StackTrace);
			}
		}
		return (workshopRoom, loadRoomResult);
	}

	public static void finishLoadingLevel(LevelContainerEditor container, WorkshopRoom workshopRoom)
	{
		try
		{
			registerCustomMaterials(workshopRoom.context);
			instantiateLoadedProps(workshopRoom.roomData.props, workshopRoom.context);
			placeSpawnPoints(workshopRoom.context.propInstances, container);
			UnityEngine.Object.DestroyImmediate(container.customModelsParent);
		}
		catch (Exception exception)
		{
			UnityEngine.Debug.LogException(exception);
		}
	}

	private static (RoomData, LoadRoomResult) loadRoomDataFromPath(string roomFilePath)
	{
		string contents = File.ReadAllText(roomFilePath);
		string[] customModelFiles = Array.Empty<string>();
		string path = Path.Combine(new FileInfo(roomFilePath).Directory.FullName, "_CustomModels");
		if (Directory.Exists(path))
		{
			customModelFiles = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
			customModelFiles = fixPathModelPaths(customModelFiles);
		}
		return loadRoomData(contents, customModelFiles);
	}

	private static (RoomData, LoadRoomResult) loadRoomData(string contents, string[] customModelFiles)
	{
		LoadRoomResult loadRoomResult = LoadRoomResult.Success;
		List<string> list = new List<string>();
		RoomData roomData;
		try
		{
			roomData = JsonUtility.FromJson<RoomData>(contents);
			nextTokenId = 0;
			foreach (PropData prop in roomData.props)
			{
				if (prop.token.Count > 0 && prop.token[0].id > nextTokenId)
				{
					nextTokenId = prop.token[0].id;
				}
				if (customModelFiles.Length == 0 || !tryGetCustomModelData(prop, out var data))
				{
					continue;
				}
				foreach (string usedFile in data.usedFiles)
				{
					if (usedFile.Contains("data:"))
					{
						break;
					}
					string text = usedFile.Replace("\\", "/");
					bool flag = false;
					for (int i = 0; i < customModelFiles.Length; i++)
					{
						if (customModelFiles[i].Replace("\\", "/").Contains(text))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						list.Add(text);
					}
				}
				if (!data.disableGeneratedCollider && data.colliderType == CustomModelColliderType.None)
				{
					data.colliderType = CustomModelColliderType.Box;
				}
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			roomData = null;
			loadRoomResult = LoadRoomResult.Failed;
		}
		if (loadRoomResult == LoadRoomResult.Success)
		{
			int appVersion = getAppVersion();
			if (roomData.pointVersion > appVersion)
			{
				roomData = null;
				loadRoomResult = LoadRoomResult.NeedsGameUpdate;
			}
		}
		if (loadRoomResult == LoadRoomResult.Success && list.Count > 0)
		{
			string text2 = "Missing files: ";
			foreach (string item in list)
			{
				text2 = text2 + "'" + item + "'" + Environment.NewLine;
			}
			UnityEngine.Debug.Log(text2);
			loadRoomResult = LoadRoomResult.MissingCustomModels;
		}
		return (roomData, loadRoomResult);
	}

	private void initRoomLoading()
	{
		foreach (FileInfo item in new DirectoryInfo(createTempFolder()).EnumerateFiles())
		{
			item.Delete();
		}
		if (loadRoomOnStart == null)
		{
			useNonLegacyLights = true;
			useNonLegacyFloorColliders = true;
			camToLinkedProp = false;
			configUI.ConfigInfo_LegacyLightsToggle.SetIsOnWithoutNotify(value: false);
			configUI.ConfigInfo_LegacyFloorCollidersToggle.SetIsOnWithoutNotify(value: false);
			configUI.ConfigInfo_PlayerNameplateVisible.SetIsOnWithoutNotify(value: true);
			configUI.ConfigInfo_ItemNameplatesVisible.SetIsOnWithoutNotify(value: true);
			configUI.ConfigInfo_UseProximityChat.SetIsOnWithoutNotify(value: false);
			configUI.ConfigInfo_UseOnStartPostProcessing.SetIsOnWithoutNotify(value: true);
			configUI.ConfigInfo_UseOnStartSkybox.SetIsOnWithoutNotify(value: true);
			configUI.ConfigInfo_EnableScriptingProp.SetIsOnWithoutNotify(value: false);
			configUI.ConfigInfo_CamToLinkedProp.SetIsOnWithoutNotify(value: false);
		}
		else
		{
			endConditionCustomModels = LoadingCanvas.get().enqueueEndCondition(Localization.translate(loadingCanvasText), LoadingCanvas.get().getTopEndCondition().title);
			loadCustomProps(getEditorContext(), loadRoomOnStart, levelContainer.customModelsParent.transform, delegate
			{
				int count = customModelPrefabs.Count;
				int modelsLoaded = count - loadingCustomModels.Count + 1;
				updateLoadingScreenCustomModelNumbers(count, modelsLoaded);
			}, fromEditor: true, propSprites, propIcons);
		}
		configUI.ConfigInfo_RemoveAllEditorPuzzleComponents.gameObject.SetActive(value: false);
	}

	private bool updateRoomLoading()
	{
		if (loadRoomOnStart == null)
		{
			return true;
		}
		if (loadingCustomModels.Count > 0)
		{
			updateLoadingCustomModels(loadingCustomModels);
			return false;
		}
		switch (loadRoomInEditor(loadRoomOnStart))
		{
		case LoadRoomResult.Failed:
			optionDialog.showNoTranslate("Failed to load room", "The room could not be loaded.", onFrameClick);
			break;
		case LoadRoomResult.NeedsGameUpdate:
			optionDialog.showNoTranslate("Update", "Room Editor Level failed to load. Level was made with newer version of the game. Update the game and try again.", onFrameClick);
			break;
		case LoadRoomResult.MissingCustomModels:
			optionDialog.showNoTranslate("Missing Models", "Room Editor missing custom model files detected. Replace the missing files or remove the props from the room.", onFrameClick);
			break;
		}
		if (endConditionCustomModels != null)
		{
			endConditionCustomModels.done = true;
		}
		refresh.userAssets = true;
		loadRoomOnStart = null;
		foreach (KeyValuePair<InstanceID, PropInstance> propInstance in propInstances)
		{
			if (getInstanceByID(propInstances, propInstance.Key).TryGetComponent<HDAdditionalLightData>(out var component))
			{
				component.enabled = false;
				component.enabled = true;
			}
		}
		return true;
	}

	private LoadRoomResult loadRoomInEditor(string dirPath)
	{
		var (roomData, loadRoomResult) = loadRoomDataFromPath(Path.Combine(dirPath, "Room.room"));
		createWalkthrough(roomData.walkthrough);
		if (loadRoomResult == LoadRoomResult.Success || loadRoomResult == LoadRoomResult.MissingCustomModels)
		{
			roomDirPath = dirPath;
			loadRoomInEditor(roomData);
			UnityEngine.Debug.Log("Loaded room from '" + roomDirPath + "'");
		}
		return loadRoomResult;
	}

	private void loadRoomInEditor(RoomData roomData)
	{
		if (!PlayerSave.getProgress().reBetaMessageShown)
		{
			betaMessageUI.gameObject.SetActive(value: true);
			PlayerSave.getProgress().reBetaMessageShown = true;
			PlayerSave.flush();
		}
		discardRoom();
		publishInfo.ui.Publish_Description_BasicInformationContent_Layout_Name_InputField.text = roomData.name ?? "MyRoom";
		publishInfo.ui.Publish_Description_BasicInformationContent_DescriptionInput.text = roomData.description ?? "Description";
		cameraScreenshotPosition = roomData.cameraScreenshotPosition;
		cameraScreenshotRotation = Quaternion.Euler(roomData.cameraScreenshotRotation);
		if (roomData.cameraEditPosition != Vector3.zero && roomData.cameraEditRotation != Vector3.zero)
		{
			cameraPosition = roomData.cameraEditPosition;
			cameraRotation = Quaternion.Euler(roomData.cameraEditRotation);
			mainCam.transform.position = cameraPosition;
			mainCam.transform.rotation = cameraRotation;
		}
		ensureBackwardCompatibility(roomData);
		useNonLegacyLights = roomData.useNonLegacyLights;
		useNonLegacyFloorColliders = roomData.useNonLegacyFloorColliders;
		hidePlayerNameplates = roomData.hidePlayerNameplates;
		hideItemNameplates = roomData.hideItemNameplates;
		useProximityChat = roomData.useProximityChat;
		useSkyboxPreview = roomData.useSkyboxPreview;
		usePostProcessingPreview = roomData.usePostProcessingPreview;
		useWaterPreview = roomData.useWaterPreview;
		camToLinkedProp = roomData.camToLinkedProp;
		configUI.ConfigInfo_LegacyLightsToggle.SetIsOnWithoutNotify(!useNonLegacyLights);
		configUI.ConfigInfo_LegacyFloorCollidersToggle.SetIsOnWithoutNotify(!useNonLegacyFloorColliders);
		configUI.ConfigInfo_PlayerNameplateVisible.SetIsOnWithoutNotify(!hidePlayerNameplates);
		configUI.ConfigInfo_ItemNameplatesVisible.SetIsOnWithoutNotify(!hideItemNameplates);
		configUI.ConfigInfo_UseProximityChat.SetIsOnWithoutNotify(useProximityChat);
		configUI.ConfigInfo_UseOnStartPostProcessing.SetIsOnWithoutNotify(usePostProcessingPreview);
		configUI.ConfigInfo_UseOnStartSkybox.SetIsOnWithoutNotify(useSkyboxPreview);
		configUI.ConfigInfo_UseOnStartWater.SetIsOnWithoutNotify(useWaterPreview);
		configUI.ConfigInfo_EnableScriptingProp.SetIsOnWithoutNotify(value: false);
		configUI.ConfigInfo_CamToLinkedProp.SetIsOnWithoutNotify(camToLinkedProp);
		activateLegacyLights(!useNonLegacyLights);
		updateMaterials();
		instantiateLoadedProps(roomData.props, getEditorContext());
		editorPostProcessingOnStartActive = null;
		refresh.postProcessing = true;
		skyboxOnStartActiveCache = null;
		refresh.skybox = true;
		cloudsOnStartActiveCache = null;
		refresh.clouds = true;
		oceanOnStartActiveCache = null;
		refresh.ocean = true;
		foreach (PropData prop in roomData.props)
		{
			if (tryGetEditorPostProcessingData(prop, out var data) && data.activeOnStart)
			{
				editorPostProcessingOnStartActive = data;
			}
			if (tryGetSkyboxData(prop, out var data2) && data2.activeOnStart)
			{
				skyboxOnStartActiveCache = data2;
			}
			if (tryGetCloudsData(prop, out var data3) && data3.activeOnStart)
			{
				cloudsOnStartActiveCache = data3;
			}
			if (tryGetOceanData(prop, out var data4) && data4.activeOnStart)
			{
				oceanOnStartActiveCache = data4;
			}
		}
		int num = 0;
		foreach (KeyValuePair<InstanceID, PropInstance> propInstance in propInstances)
		{
			propInstance.Deconstruct(out var key, out var _);
			InstanceID instanceID = key;
			num = Mathf.Max(num, instanceID.value);
		}
		instanceIDCounter = num + 1;
		refresh.transforms = true;
		refresh.navMeshPreview = 1;
		gridCustomPassVolume.transform.position = Vector3.zero;
		propHistory.Clear();
		refreshPropHistoryUI();
		initPublishingWorkshopRoomInfo(roomData);
		removePropertiesForParent(propertiesUI.Content);
		updateTabNavigation(isClear: true);
		userAssets.Clear();
		refresh.userAssets = true;
	}

	private void actionsLoadProps(List<PropData> propsData, List<EditorAction> actions)
	{
		PropData[] array = new PropData[propsData.Count];
		for (int i = 0; i < propsData.Count; i++)
		{
			PropData propData = copy(propsData[i]);
			clearPropDataLinks(propData);
			array[i] = propData;
			actions.Add(actionChangeProp(null, propData, "Pass 1: Create props"));
		}
		for (int j = 0; j < propsData.Count; j++)
		{
			actions.Add(actionChangeProp(array[j], propsData[j], "Pass 2: Link props"));
		}
		foreach (PropData propsDatum in propsData)
		{
			actions.Add(actionChangeTransform(propsDatum.ID, propsDatum.transform, "Pass 3: Assign local transform"));
		}
	}

	private static void clearPropDataLinks(PropData data, HashSet<InstanceID> clearIDs = null)
	{
		Predicate<InstanceID> predicate = (InstanceID id) => clearIDs == null || clearIDs.Contains(id);
		Predicate<LockArgData> match = (LockArgData lockArg) => clearIDs == null || clearIDs.Contains(lockArg.instanceID);
		if (predicate(data.parentID))
		{
			data.parentID = InstanceID.None;
		}
		if (tryGetSwitch3DData(data, out var data2))
		{
			data2.onLinks.targets.RemoveAll(predicate);
			data2.onLinks.locks.RemoveAll(match);
			data2.offLinks.targets.RemoveAll(predicate);
			data2.offLinks.locks.RemoveAll(match);
		}
		if (tryGetTurnableData(data, out var data3))
		{
			data3.locks.RemoveAll(match);
		}
		if (tryGetDialData(data, out var data4))
		{
			data4.locks.RemoveAll(match);
		}
		if (tryGetLockData(data, out var data5))
		{
			data5.onLock.targets.RemoveAll(predicate);
			data5.onLock.locks.RemoveAll(match);
			data5.onUnlock.targets.RemoveAll(predicate);
			data5.onUnlock.locks.RemoveAll(match);
		}
		if (tryGetSlotData(data, out var data6))
		{
			data6.keys.RemoveAll(predicate);
			data6.rejectKeys.RemoveAll(predicate);
			data6.onPlace.locks.RemoveAll(match);
			data6.onPlace.targets.RemoveAll(predicate);
			data6.onRemove.locks.RemoveAll(match);
			data6.onRemove.targets.RemoveAll(predicate);
		}
		if (tryGetTriggerData(data, out var data7))
		{
			data7.keys.RemoveAll(predicate);
			data7.onEnter.targets.RemoveAll(predicate);
			data7.onEnter.locks.RemoveAll(match);
			data7.onExit.targets.RemoveAll(predicate);
			data7.onExit.locks.RemoveAll(match);
			data7.onStart.targets.RemoveAll(predicate);
			data7.onStart.locks.RemoveAll(match);
			data7.onEnd.targets.RemoveAll(predicate);
			data7.onEnd.locks.RemoveAll(match);
		}
		if (tryGetActivatorComponentData(data, out var data8))
		{
			data8.keys.RemoveAll(predicate);
		}
		if (tryGetCustomModelData(data, out var _))
		{
			data.propID.value = data.propID.value.Replace('\\', '/');
		}
		if (tryGetEditorDisplayData(data, out var data10) && predicate(data10.targetLock))
		{
			data10.targetLock = InstanceID.None;
		}
		if (tryGetSlidableData(data, out var data11))
		{
			data11.locks.RemoveAll(match);
		}
		if (tryGetPuzzleData(data, out var data12))
		{
			data12.conditions.RemoveAll(predicate);
		}
		if (tryGetRouletteData(data, out var data13))
		{
			data13.targets.RemoveAll(predicate);
		}
		if (tryGetEditorSetupData(data, out var data14))
		{
			data14.targets.RemoveAll(predicate);
		}
		if (tryGetDelayData(data, out var data15))
		{
			data15.targets.RemoveAll(predicate);
		}
	}

	private static void ensureBackwardCompatibility(RoomData roomDataToFix)
	{
		foreach (PropData prop in roomDataToFix.props)
		{
			PropData propData = prop;
			if (propData.displayName == null)
			{
				propData.displayName = string.Empty;
			}
			propData = prop;
			if (propData.scriptName == null)
			{
				propData.scriptName = string.Empty;
			}
			List<SwapMaterialData> materials = prop.materials;
			if (materials != null && materials.Count > 0)
			{
				propData = prop;
				if (propData.materialSwaps == null)
				{
					propData.materialSwaps = new List<MaterialSwapData>();
				}
				foreach (SwapMaterialData material in prop.materials)
				{
					foreach (MaterialPath materialPath in material.materialPaths)
					{
						MaterialSwapData materialSwapData = new MaterialSwapData();
						materialSwapData.asset = material.materialAssetFileName;
						materialSwapData.path = materialPath.path;
						materialSwapData.index = materialPath.index;
						prop.materialSwaps.Add(materialSwapData);
					}
				}
				prop.materials.Clear();
			}
			List<MaterialSwapData> materialSwaps = prop.materialSwaps;
			if (materialSwaps == null || materialSwaps.Count <= 0)
			{
				continue;
			}
			foreach (MaterialSwapData materialSwap in prop.materialSwaps)
			{
				if (Path.GetExtension(materialSwap.asset) == ".mat")
				{
					UnityEngine.Debug.Log("Fixing material swap asset '" + materialSwap.asset + "' from .mat to .es2mat");
					materialSwap.asset = Path.ChangeExtension(materialSwap.asset, ".es2mat");
				}
			}
		}
	}

	private static void instantiateLoadedProps(List<PropData> propsData, EditorContext context)
	{
		PropData[] array = new PropData[propsData.Count];
		HashSet<InstanceID> hashSet = new HashSet<InstanceID>();
		for (int i = 0; i < propsData.Count; i++)
		{
			PropData propData = copy(propsData[i]);
			clearPropDataLinks(propData);
			array[i] = propData;
			if (changeProp(null, propData, context) != ChangePropResult.Success)
			{
				UnityEngine.Debug.LogError("Failed to load prop with ID '" + propData.propID.value + "'");
				hashSet.Add(propData.ID);
			}
		}
		for (int j = 0; j < propsData.Count; j++)
		{
			if (!hashSet.Contains(array[j].ID))
			{
				if (hashSet.Count > 0)
				{
					clearPropDataLinks(propsData[j], hashSet);
				}
				changeProp(array[j], propsData[j], context);
			}
		}
		foreach (PropData propsDatum in propsData)
		{
			if (context.propInstances.TryGetValue(propsDatum.ID, out var value))
			{
				applyTransform(value.transform, propsDatum.transform);
			}
		}
	}

	private static List<RoomMeta> loadRoomsMeta()
	{
		List<RoomMeta> list = new List<RoomMeta>();
		string path = Path.Combine(Application.persistentDataPath, "UGC");
		if (!Directory.Exists(path))
		{
			return list;
		}
		DirectoryInfo[] directories = new DirectoryInfo(path).GetDirectories();
		for (int i = 0; i < directories.Length; i++)
		{
			RoomMeta roomMeta = getRoomMeta(directories[i]);
			if (roomMeta != null)
			{
				list.Add(roomMeta);
			}
		}
		return list;
	}

	private static RoomMeta getRoomMeta(DirectoryInfo roomDir)
	{
		FileInfo fileInfo = Array.Find(roomDir.GetFiles(), (FileInfo x) => x.Name == "Room.room");
		if (fileInfo == null)
		{
			return null;
		}
		(RoomData, LoadRoomResult) tuple = loadRoomDataFromPath(fileInfo.FullName);
		var (roomData, _) = tuple;
		if (tuple.Item2 != LoadRoomResult.Success)
		{
			return null;
		}
		Sprite preview = null;
		string path = Path.Combine(roomDir.FullName, "Preview.jpg");
		if (File.Exists(path))
		{
			byte[] data = File.ReadAllBytes(path);
			Texture2D texture2D = new Texture2D(2, 2, TextureFormat.RGBA32, mipChain: true);
			texture2D.LoadImage(data, markNonReadable: true);
			Rect rect = new Rect(0f, 0f, texture2D.width, texture2D.height);
			preview = Sprite.Create(texture2D, rect, new Vector2(0.5f, 0.5f), 100f);
		}
		return new RoomMeta
		{
			loadPath = roomDir.FullName,
			folder = roomDir.Name,
			name = roomData.name,
			description = roomData.description,
			preview = preview
		};
	}

	private void saveRoom(bool autosave = false, string roomName = null)
	{
		UnityEngine.Debug.Log(string.Format("[{0}] Room dir: {1} | Autosave: {2} | Room name: {3}", "saveRoom", roomDirPath, autosave, roomName));
		int appVersion = getAppVersion();
		List<string> walkthrough;
		bool activeWalkthrough = getActiveWalkthrough(out walkthrough);
		if (roomName != null)
		{
			publishingRoomInfo.roomTitle = roomName;
			publishInfo.ui.Publish_Description_BasicInformationContent_Layout_Name_InputField.text = roomName;
		}
		string text = publishInfo.ui.Publish_Description_BasicInformationContent_Layout_Name_InputField.text;
		string text2 = publishInfo.ui.Publish_Description_BasicInformationContent_DescriptionInput.text;
		List<string> output;
		RoomData obj = new RoomData
		{
			name = ((text.Length > 0 && text != "<Title to publish>") ? text : "MyRoom"),
			description = ((text2.Length > 0 && text2 != "<Description>") ? text2 : "Description"),
			tags = (publishInfo.getActiveTags(out output, activeWalkthrough) ? output : publishingRoomInfo.roomTags),
			walkthrough = (activeWalkthrough ? walkthrough : publishingRoomInfo.roomWalkthrough),
			pointVersion = appVersion,
			cameraScreenshotPosition = cameraScreenshotPosition,
			cameraScreenshotRotation = cameraScreenshotRotation.eulerAngles,
			cameraEditPosition = mainCam.transform.position,
			cameraEditRotation = mainCam.transform.eulerAngles,
			useNonLegacyLights = useNonLegacyLights,
			useNonLegacyFloorColliders = useNonLegacyFloorColliders,
			hidePlayerNameplates = hidePlayerNameplates,
			hideItemNameplates = hideItemNameplates,
			useProximityChat = useProximityChat,
			useSkyboxPreview = useSkyboxPreview,
			useWaterPreview = useWaterPreview,
			usePostProcessingPreview = usePostProcessingPreview,
			camToLinkedProp = camToLinkedProp
		};
		roomDirPath = createRoomDir(roomDirPath);
		getEditorContext().roomDirPath = roomDirPath;
		publishingRoomInfo.roomLocalPath = roomDirPath;
		if (!autosave)
		{
			createUGCFolder();
			if (publishingRoomInfo.roomPreviewImage != null)
			{
				writePreviewScreenshotToDisk(publishingRoomInfo.roomPreviewImage);
			}
			GameObject msg = editorUI.SaveNotification.gameObject;
			tweener.destroyTweens(msg, performEvents: false, performFinalUpdate: false);
			PineTweenSystemEnableNoHandles pineTweenSystemEnableNoHandles = tweener;
			GameObject obj2 = msg;
			float? alphaFrom = 0f;
			float? alphaTo = 1f;
			PineTween pineTween = pineTweenSystemEnableNoHandles.tween(obj2, 0.35f, 0f, deactivate: false, activate: false, destroy: false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, alphaTo, alphaFrom);
			pineTween.onComplete = (OnComplete)Delegate.Combine(pineTween.onComplete, (OnComplete)delegate
			{
				PineTweenSystemEnableNoHandles pineTweenSystemEnableNoHandles2 = tweener;
				GameObject obj3 = msg;
				float? alphaTo2 = 0f;
				pineTweenSystemEnableNoHandles2.tween(obj3, 0.35f, 1.5f, deactivate: false, activate: false, destroy: false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, alphaTo2);
			});
		}
		List<PropInstance> list = new List<PropInstance>(propInstances.Values);
		list.Sort((PropInstance x, PropInstance y) => x.ID.value.CompareTo(y.ID.value));
		obj.props = compressProps(list);
		string contents = JsonUtility.ToJson(obj, prettyPrint: true);
		string text3 = Path.Combine(roomDirPath, "Room.room");
		if (File.Exists(text3))
		{
			string text4 = Path.Combine(roomDirPath, "Backups");
			if (!Directory.Exists(text4))
			{
				Directory.CreateDirectory(text4);
			}
			int num = 0;
			string[] files = Directory.GetFiles(text4);
			for (int num2 = 0; num2 < files.Length; num2++)
			{
				string fileName = Path.GetFileName(files[num2]);
				if (!fileName.StartsWith("Room.roombk"))
				{
					continue;
				}
				int num3 = 0;
				for (int num4 = "Room.roombk".Length; num4 < fileName.Length; num4++)
				{
					if (char.IsDigit(fileName[num4]))
					{
						num3++;
					}
				}
				string s = fileName.Substring("Room.roombk".Length, num3);
				if (num3 > 0 && int.TryParse(s, out var result))
				{
					num = Mathf.Max(num, result);
				}
			}
			string text5 = Path.Combine(text4, string.Format("{0}{1}", "Room.roombk", num + 1));
			if (autosave)
			{
				text3 = text5 + "auto";
				UnityEngine.Debug.Log("Autosaved to '" + text5 + "'");
			}
			else
			{
				File.Copy(text3, text5);
				UnityEngine.Debug.Log("Copied old Room.room to '" + text5 + "'");
			}
		}
		File.WriteAllText(text3, contents);
		if (!autosave)
		{
			undo.isDirty = false;
			UnityEngine.Debug.Log("Saved room to '" + roomDirPath + "'");
			generateCodeFile();
			bool num5 = cancelSelection();
			if (refresh.editorUI)
			{
				refreshEditorUI();
			}
			if (refresh.propertiesUI)
			{
				refreshTargetPropertiesUI();
			}
			if (!File.Exists(Path.Combine(roomDirPath, "Preview.jpg")))
			{
				Texture2D screenshot = takePreviewScreenshot();
				writePreviewScreenshotToDisk(screenshot);
			}
			RoomMeta roomMeta = getRoomMeta(new DirectoryInfo(roomDirPath));
			addOrUpdateRoomPickerLevel("yourRooms", roomMeta);
			if (loadableRooms.Find((RoomMeta entry) => entry.folder == roomMeta.folder) == null)
			{
				loadableRooms.Add(roomMeta);
			}
			refresh.userAssets = true;
			if (num5)
			{
				undo.undoLast();
			}
		}
		void writePreviewScreenshotToDisk(Texture2D tex)
		{
			byte[] bytes = tex.EncodeToJPG(75);
			File.WriteAllBytes(Path.Combine(roomDirPath, "Preview.jpg"), bytes);
		}
	}

	private void generateCodeFile()
	{
		string text = Path.Combine(roomDirPath, "Code.room");
		HashSet<string> invokableFunctions;
		string script = setupAndGenerateLevelLogicLua(getEditorContext().container.gameObject, dry: true, out invokableFunctions);
		string contents = JsonUtility.ToJson(new CodeData
		{
			script = script,
			invokeableFunctions = new List<string>(invokableFunctions)
		});
		File.WriteAllText(text, contents);
		UnityEngine.Debug.Log("Generated code file at: " + text);
	}

	private void initRoomPicker()
	{
		ESUGC.init();
		roomPickerUI.LevelPicker.Frame.setup(reInputDispatcher, null, null, onFrameClick);
		levelProvider = new LevelProvider(ESUGC.current, onLevelRefresh);
		string path = Path.Combine(Application.streamingAssetsPath, "RoomEditor", "TutorialRooms");
		string path2 = Path.Combine(Application.streamingAssetsPath, "RoomEditor", "PremadeRooms");
		string[] directories = Directory.GetDirectories(path);
		for (int i = 0; i < directories.Length; i++)
		{
			RoomMeta roomMeta = getRoomMeta(new DirectoryInfo(directories[i]));
			if (roomMeta != null)
			{
				UnityEngine.Debug.Log(roomMeta.folder);
				addOrUpdateRoomPickerLevel("tutorial", roomMeta);
			}
		}
		directories = Directory.GetDirectories(path2);
		for (int i = 0; i < directories.Length; i++)
		{
			RoomMeta roomMeta2 = getRoomMeta(new DirectoryInfo(directories[i]));
			if (roomMeta2 != null)
			{
				addOrUpdateRoomPickerLevel("preset", roomMeta2);
			}
		}
		levelProvider.addLevelToList("yourRooms", "addNewRoom");
		levelProvider.modifyLevel("addNewRoom", "Create New Room", "AddNewRoom.jpg");
		if (loadableRooms == null)
		{
			loadableRooms = loadRoomsMeta();
		}
		foreach (RoomMeta loadableRoom in loadableRooms)
		{
			addOrUpdateRoomPickerLevel("yourRooms", loadableRoom);
		}
		levelPicker = new LevelPicker(roomPickerUI.LevelPicker, levelProvider);
		levelPicker.onClick = syncRoomPickerButtons;
		levelPicker.onDoubleClick = onPickerDoubleClick;
		levelPicker.onBrowserClick = onPickerBrowseClick;
		levelPicker.startTab("yourRooms", "%yourRooms%", null, null, isLocked: false, "yourRooms");
		LevelPicker obj = levelPicker;
		Comparison<LevelPicker.LevelData> yourRoomsSorter = levelPicker.getYourRoomsSorter("addNewRoom");
		obj.modifyPack("yourRooms.yourRooms", null, null, null, null, null, null, null, null, null, yourRoomsSorter);
		levelPicker.startTab("tutorial", "%tutorialRooms%", null, null, isLocked: false, "tutorial");
		levelPicker.startTab("preset", "%PresetRooms%", null, null, isLocked: false, "preset");
		levelPicker.select("yourRooms.addNewRoom");
		levelProvider.requestAllItems();
		levelProvider.requestAllTextures();
		levelPicker.setShouldSyncVisuals();
		string text = Path.Combine(getUGCFolder(), roomToLoadOnStart);
		if (Is.Editor && !string.IsNullOrEmpty(roomToLoadOnStart) && Directory.Exists(text))
		{
			loadRoomInEditor(text);
		}
		else
		{
			openRoomPicker(canBeClosed: false);
		}
	}

	private void updateRoomPicker()
	{
		levelProvider.update();
		levelPicker.update();
	}

	private void addOrUpdateRoomPickerLevel(string listId, RoomMeta room)
	{
		levelProvider.addLevelToList(listId, room.folder);
		string text = "file:///" + room.loadPath + "/";
		text += "Preview.jpg";
		DateTime lastWriteTime = Directory.GetLastWriteTime(Path.Combine(room.loadPath, "Room.room"));
		LevelProvider obj = levelProvider;
		string folder = room.folder;
		string text2 = room.name;
		string textureUrl = text;
		DateTime? dateRoomModified = lastWriteTime;
		obj.modifyLevel(folder, text2, null, textureUrl, -1, -1, null, null, null, null, null, null, null, null, null, dateRoomModified);
		levelPicker?.setShouldSyncVisuals();
	}

	private void onRoomPickerFrameClick(string id)
	{
		switch (id)
		{
		case "rp_createNew":
			showCreateNewRoomDialog("rp_createNewRoomCreate", "rp_createNewRoomCancel");
			break;
		case "rp_createNewRoomCreate":
		case "rp_createNewRoomCancel":
			if (id == "rp_createNewRoomCreate")
			{
				onCreateNewRoomPressed();
			}
			break;
		case "rp_openRoom":
		{
			string levelId = levelPicker.getSelectedId().levelId;
			if (roomDirPath != null && roomDirPath.EndsWith(levelId))
			{
				break;
			}
			if (undo.isDirty)
			{
				showUnsavedChangesDialog("rp_unsavedChangesSave", "rp_unsavedChangesDiscard");
				break;
			}
			RoomMeta targetRoomItem = loadableRooms.Find((RoomMeta x) => x.folder == levelId);
			startLoadingRoomInEditor(targetRoomItem);
			roomPickerUI.gameObject.SetActive(value: false);
			break;
		}
		case "rp_copyAndOpen":
		{
			if (undo.isDirty)
			{
				showUnsavedChangesDialog("rp_unsavedChangesSave", "rp_unsavedChangesDiscard");
				break;
			}
			(string tabId, string packId, string levelId) selectedId = levelPicker.getSelectedId();
			string item2 = selectedId.tabId;
			string item3 = selectedId.levelId;
			string sourceDirName = Path.Combine(Application.streamingAssetsPath, "RoomEditor", (item2 == "tutorial") ? "TutorialRooms" : "PremadeRooms", item3);
			string text = createRoomDir(null);
			Menu.dirCopy(sourceDirName, text);
			RoomMeta roomMeta2 = getRoomMeta(new DirectoryInfo(text));
			addOrUpdateRoomPickerLevel("yourRooms", roomMeta2);
			startLoadingRoomInEditor(roomMeta2);
			roomPickerUI.gameObject.SetActive(value: false);
			break;
		}
		case "rp_unsavedChangesSave":
		case "rp_unsavedChangesDiscard":
		{
			if (id == "rp_unsavedChangesSave")
			{
				saveRoom();
			}
			string levelId3 = levelPicker.getSelectedId().levelId;
			RoomMeta targetRoomItem2 = loadableRooms.Find((RoomMeta x) => x.folder == levelId3);
			startLoadingRoomInEditor(targetRoomItem2);
			roomPickerUI.gameObject.SetActive(value: false);
			break;
		}
		case "rp_newRoomUnsavedChangesSave":
		case "rp_newRoomUnsavedChangesDiscard":
			if (id == "rp_newRoomUnsavedChangesSave")
			{
				saveRoom();
			}
			createAndOpenNewRoom(optionDialog.inputField.text);
			break;
		case "rp_deleteRoom":
		{
			string item = levelPicker.getSelectedId().levelId;
			optionDialog.showNoTranslate("Delete " + levelProvider.getLevelItem(item).levelName, "Delete this room?", onFrameClick, new VisualControl("rp_deleteRoomConfirm", ControllerButtonActionType.GameBack, "%YesDelete%", VisualControl.ControlType.Button, VisualControl.Position.BotRight), new VisualControl("ignore", ControllerButtonActionType.GameBack, "%no%", VisualControl.ControlType.Button, VisualControl.Position.BotRight));
			break;
		}
		case "rp_deleteRoomConfirm":
		{
			string levelId2 = levelPicker.getSelectedId().levelId;
			RoomMeta roomMeta = loadableRooms.Find((RoomMeta x) => x.folder == levelId2);
			Directory.Delete(roomMeta.loadPath, recursive: true);
			levelProvider.removeLevelFromList("yourRooms", levelId2);
			loadableRooms.Remove(roomMeta);
			break;
		}
		case "rp_back":
			roomPickerUI.gameObject.SetActive(value: false);
			break;
		case "rp_quit":
			if (undo.isDirty)
			{
				optionDialog.showNoTranslate("Really quit?", "There are unsaved changes in the room editor.", onFrameClick, new VisualControl("rp_quitSave", ControllerButtonActionType.GameBack, "%uiSaveAndExit%", VisualControl.ControlType.Button, VisualControl.Position.BotRight), new VisualControl("rp_quitDiscard", ControllerButtonActionType.GameBack, "%RoomEditor_Discard%", VisualControl.ControlType.Button, VisualControl.Position.BotRight), new VisualControl("ignore", ControllerButtonActionType.GameBack, "%cancel%", VisualControl.ControlType.Button, VisualControl.Position.BotRight));
			}
			else
			{
				unloadAndGotoMenu();
			}
			break;
		case "rp_quitSave":
			saveRoom();
			unloadAndGotoMenu();
			break;
		case "rp_quitDiscard":
			unloadAndGotoMenu();
			break;
		}
	}

	private void onCreateNewRoomPressed()
	{
		if (!canCloseRoomPicker || !undo.isDirty)
		{
			createAndOpenNewRoom(optionDialog.inputField.text);
		}
		else if (undo.isDirty)
		{
			showUnsavedChangesDialog("rp_newRoomUnsavedChangesSave", "rp_newRoomUnsavedChangesDiscard");
		}
	}

	private void createAndOpenNewRoom(string newRoomName)
	{
		UnityEngine.Debug.Log("Creating new room: " + newRoomName);
		roomPickerUI.gameObject.SetActive(value: false);
		string sourceDirName = Path.Combine(Application.streamingAssetsPath, "RoomEditor", "PremadeRooms", "DefaultRoom");
		string text = createRoomDir(null);
		Menu.dirCopy(sourceDirName, text);
		RoomData roomData = JsonUtility.FromJson<RoomData>(File.ReadAllText(Path.Combine(text, "Room.room")));
		roomData.name = (string.IsNullOrEmpty(newRoomName) ? "MyRoom" : newRoomName);
		File.WriteAllText(Path.Combine(text, "Room.room"), JsonUtility.ToJson(roomData, prettyPrint: true));
		loadRoomInEditor(text);
		RoomMeta roomMeta = getRoomMeta(new DirectoryInfo(roomDirPath));
		loadableRooms.Add(roomMeta);
		addOrUpdateRoomPickerLevel("yourRooms", roomMeta);
	}

	private void showCreateNewRoomDialog(string createButtonId, string cancelButtonId)
	{
		optionDialog.showNoTranslate("Create New Room", "How should a new room be called?", onFrameClick, new VisualControl(createButtonId, ControllerButtonActionType.UIConfirmPrimary, "Create", VisualControl.ControlType.Button, VisualControl.Position.BotRight), new VisualControl(cancelButtonId, ControllerButtonActionType.UIConfirmPrimary, "Cancel", VisualControl.ControlType.Button, VisualControl.Position.BotRight));
		optionDialog.useInputField(createButtonId, "MyRoom");
	}

	private void showUnsavedChangesDialog(string saveButtonId, string discardButtonId)
	{
		optionDialog.showNoTranslate("Unsaved changes", "There are unsaved changes in your room. Do you want to discard them?", onFrameClick, new VisualControl(saveButtonId, ControllerButtonActionType.UIConfirmPrimary, "Save", VisualControl.ControlType.Button, VisualControl.Position.BotRight), new VisualControl(discardButtonId, ControllerButtonActionType.UIConfirmPrimary, "Discard", VisualControl.ControlType.Button, VisualControl.Position.BotRight), new VisualControl("unsavedChangesCancel", ControllerButtonActionType.UIConfirmPrimary, "Cancel", VisualControl.ControlType.Button, VisualControl.Position.BotRight));
	}

	private void onPickerDoubleClick(Button button)
	{
		var (text, _, text2) = levelPicker.getSelectedId();
		if (string.IsNullOrEmpty(text2))
		{
			return;
		}
		if (text == "yourRooms")
		{
			if (text2 == "addNewRoom")
			{
				onRoomPickerFrameClick("rp_createNew");
			}
			else
			{
				onRoomPickerFrameClick("rp_openRoom");
			}
		}
		else
		{
			onRoomPickerFrameClick("rp_copyAndOpen");
		}
	}

	private void onPickerBrowseClick(string searchText)
	{
		UnityEngine.Debug.Log("Browse clicked with search text: " + searchText);
	}

	private void openRoomPicker(bool canBeClosed)
	{
		roomPickerUI.gameObject.SetActive(value: true);
		canCloseRoomPicker = canBeClosed;
		levelPicker.roomEditorOpenedRoomId = ((!string.IsNullOrEmpty(roomDirPath)) ? getRoomMeta(new DirectoryInfo(roomDirPath)).folder : null);
		levelPicker.setShouldSyncVisuals();
		syncRoomPickerButtons();
	}

	private void syncRoomPickerButtons()
	{
		var (text, _, text2) = levelPicker.getSelectedId();
		levelPicker.ui.Frame.visualController.removeAll();
		List<VisualControl> list = new List<VisualControl>();
		if (!string.IsNullOrEmpty(text2))
		{
			if (text == "yourRooms")
			{
				if (text2 == "addNewRoom")
				{
					VisualControl item = new VisualControl("rp_createNew", ControllerButtonActionType.GameBack, "%create%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
					list.Add(item);
				}
				else if (roomDirPath == null || !roomDirPath.EndsWith(text2))
				{
					VisualControl item2 = new VisualControl("rp_openRoom", ControllerButtonActionType.GameBack, "%Open%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
					list.Add(item2);
					VisualControl item3 = new VisualControl("rp_deleteRoom", ControllerButtonActionType.GameBack, "%Delete%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
					list.Add(item3);
				}
			}
			else
			{
				VisualControl item4 = new VisualControl("rp_copyAndOpen", ControllerButtonActionType.GameBack, "%CopyAndOpen%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
				list.Add(item4);
			}
		}
		if (canCloseRoomPicker)
		{
			VisualControl visualControl = new VisualControl("rp_back", ControllerButtonActionType.GameBack, "%Close%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
			visualControl.addEscape();
			list.Add(visualControl);
		}
		VisualControl item5 = new VisualControl("rp_quit", ControllerButtonActionType.GameBack, "%quit%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
		list.Add(item5);
		levelPicker.ui.Frame.addOrUpdateControl(list);
	}

	private void onButtonClickRoomPicker(Button button)
	{
		if (button == roomPickerUI.Background && canCloseRoomPicker)
		{
			roomPickerUI.gameObject.SetActive(value: false);
		}
	}

	private void onLevelRefresh(string levelId)
	{
	}

	private void initSearchUI()
	{
		currentSearchQuery = (lastSearchQuery = "");
		addTriggerEntry(searchUI.Rect.gameObject, EventTriggerType.PointerEnter).callback.AddListener(delegate(BaseEventData data)
		{
			onSearchTrigger(data, enter: true);
		});
		addTriggerEntry(searchUI.Rect.gameObject, EventTriggerType.PointerExit).callback.AddListener(delegate(BaseEventData data)
		{
			onSearchTrigger(data, enter: false);
		});
		addTriggerEntry(searchUI.Outside.gameObject, EventTriggerType.PointerEnter).callback.AddListener(delegate(BaseEventData data)
		{
			onSearchTrigger(data, enter: true);
		});
		addTriggerEntry(searchUI.Outside.gameObject, EventTriggerType.PointerExit).callback.AddListener(delegate(BaseEventData data)
		{
			onSearchTrigger(data, enter: false);
		});
	}

	private void updateSearchUI()
	{
		if (!isSearchUiOpen())
		{
			return;
		}
		if (isSearchMultipleSelecting && !isCtrlPressed() && !isShiftPressed())
		{
			isSearchMultipleSelecting = false;
			closeSearchUI();
		}
		if (currentSearchQuery != lastSearchQuery)
		{
			lastSearchQueryTime = Time.timeSinceLevelLoad;
			lastSearchQuery = currentSearchQuery;
			isSearching = true;
		}
		if (Time.timeSinceLevelLoad - lastSearchQueryTime > 0.5f)
		{
			if (isSearching)
			{
				searchUI.Outside_Filter_Icon.gameObject.SetActive(value: true);
				searchUI.Outside_Filter_Gear.gameObject.SetActive(value: false);
				refreshSearchUI(currentSearchQuery);
				isSearching = false;
			}
		}
		else
		{
			searchUI.Outside_Filter_Icon.gameObject.SetActive(value: false);
			searchUI.Outside_Filter_Gear.gameObject.SetActive(value: true);
			searchUI.Outside_Filter_Gear.transform.Rotate(0f, 0f, -90f * Time.deltaTime);
		}
	}

	private bool isSearchUiOpen()
	{
		return searchUI.root.enabled;
	}

	private void closeSearchUI()
	{
		searchUI.root.enabled = false;
		for (int num = searchUI.Rect_Content.childCount - 1; num >= 1; num--)
		{
			UnityEngine.Object.Destroy(searchUI.Rect_Content.GetChild(num).gameObject);
		}
		currentSearchQuery = (lastSearchQuery = "");
		isSearching = false;
	}

	public void onSearchSubmit()
	{
		if (firstSearchResult != null)
		{
			undo.push(actionChangeSelection(newInstanceSelection(firstSearchResult.ID)));
			closeSearchUI();
		}
	}

	public void onSearchEndEdit()
	{
		if (currentSearchQuery == lastSearchQuery && !isHoveringSearchUI)
		{
			closeSearchUI();
		}
	}

	public void onSearchInput(string input)
	{
		currentSearchQuery = input;
	}

	private void onSearchTrigger(BaseEventData data, bool enter)
	{
		isHoveringSearchUI = enter;
	}

	private void refreshSearchUI(string input, bool selectInput = false)
	{
		closeSearchUI();
		searchUI.root.enabled = true;
		if (selectInput)
		{
			searchUI.Outside_Filter_Input.text = "";
			searchUI.Outside_Filter_Input.Select();
			searchUI.Outside_Filter_Icon.gameObject.SetActive(value: true);
			searchUI.Outside_Filter_Gear.gameObject.SetActive(value: false);
		}
		List<PropInstance> list = new List<PropInstance>();
		if (input.Trim().Length != 0)
		{
			list = performInstancesSearch(input);
		}
		searchUI.Tips.gameObject.SetActive(input.Trim().Length == 0);
		firstSearchResult = ((list.Count > 0) ? list[0] : null);
		RectTransform component = searchUI.Rect.GetComponent<RectTransform>();
		RectTransform component2 = searchUI.Rect_Content_Item.GetComponent<RectTransform>();
		float num = ((list.Count < 15) ? ((float)list.Count + 0.2f) : 15f);
		component.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, component2.sizeDelta.y * num);
		searchUI.Rect.gameObject.SetActive(list.Count != 0);
		searchUI.Outside_Filter_Counter.text = list.Count.ToString() ?? "";
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] == null)
			{
				continue;
			}
			PropInstance propInstance = list[i];
			Button button = UnityEngine.Object.Instantiate(searchUI.Rect_Content_Item, searchUI.Rect_Content);
			button.gameObject.SetActive(value: true);
			Text componentInChildren = button.GetComponentInChildren<Text>();
			string text = propInstance.displayName;
			if (string.IsNullOrEmpty(text))
			{
				text = propInstance.exportName;
				if (string.IsNullOrEmpty(text))
				{
					text = propInstance.scriptName;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				text = "*Empty Name*";
				componentInChildren.fontStyle = FontStyle.Italic;
			}
			componentInChildren.text = text;
			button.onClick.AddListener(delegate
			{
				bool flag = isCtrlPressed() || isShiftPressed();
				addSelection(propInstance, flag, useAlternativeSelection: false);
				if (!flag)
				{
					closeSearchUI();
				}
				else
				{
					isSearchMultipleSelecting = true;
				}
			});
			EditorButtonHover component3 = button.GetComponent<EditorButtonHover>();
			if (component3 != null)
			{
				component3.onHovering = delegate
				{
					hoverInstance = propInstance;
				};
			}
		}
	}

	private List<PropInstance> performInstancesSearch(string query)
	{
		List<PropInstance> list = new List<PropInstance>();
		string[] array = query.Split((char[])null);
		if (array.Length > 1)
		{
			return searchPropInstances(getCompoundFilter(array));
		}
		if (query.ToLower().StartsWith("t:"))
		{
			int length = "t:".Length;
			return searchPropInstances(getTypeFilter(query.Substring(length, query.Length - length)));
		}
		return searchPropInstances(getNameFilter(query));
	}

	private List<PropInstance> searchPropInstances(Predicate<PropInstance> filter)
	{
		List<PropInstance> list = new List<PropInstance>();
		foreach (KeyValuePair<InstanceID, PropInstance> propInstance in propInstances)
		{
			if (filter(propInstance.Value))
			{
				PropInstance value = propInstance.Value;
				list.Add(value);
			}
		}
		return list;
	}

	private static Predicate<PropInstance> getCompoundFilter(IEnumerable<string> queries)
	{
		List<Predicate<PropInstance>> filters = new List<Predicate<PropInstance>>();
		foreach (string query in queries)
		{
			if (query.ToLower().StartsWith("t:"))
			{
				List<Predicate<PropInstance>> list = filters;
				string text = query;
				int length = "t:".Length;
				list.Add(getTypeFilter(text.Substring(length, text.Length - length)));
			}
			else
			{
				filters.Add(getNameFilter(query));
			}
		}
		return (PropInstance property) => filters.All((Predicate<PropInstance> filter) => filter(property));
	}

	private static Predicate<PropInstance> getTypeFilter(string typeName)
	{
		return delegate(PropInstance instance)
		{
			Dictionary<string, Type> dictionary = new Dictionary<string, Type> { 
			{
				"VisibilityActivator",
				typeof(ActivatorComponent)
			} };
			List<Type> list = new List<Type>
			{
				typeof(Transform),
				typeof(Rigidbody),
				typeof(MeshRenderer),
				typeof(SkinnedMeshRenderer),
				typeof(MeshFilter),
				typeof(PropTags),
				typeof(PropInstance),
				typeof(DisabledRB),
				typeof(PropIDLink),
				typeof(BoxCollider),
				typeof(CapsuleCollider),
				typeof(SphereCollider),
				typeof(MeshCollider)
			};
			Component[] components = instance.GetComponents<Component>();
			foreach (Component component in components)
			{
				if (!list.Contains(component.GetType()))
				{
					if (component is Switch3D switch3D)
					{
						bool flag = !switch3D.isAnimation;
						if (flag && "button".Contains(typeName, StringComparison.InvariantCultureIgnoreCase))
						{
							return true;
						}
						if (!flag && "animation".Contains(typeName, StringComparison.InvariantCultureIgnoreCase))
						{
							return true;
						}
					}
					else if (component is Lock && "lock".Contains(typeName, StringComparison.InvariantCultureIgnoreCase))
					{
						if (instance.TryGetComponent<Lock>(out var _))
						{
							UnityEngine.Debug.Log("Type filter found: " + component.GetType().Name);
							return true;
						}
					}
					else
					{
						foreach (string key in dictionary.Keys)
						{
							if (key.Contains(typeName, StringComparison.InvariantCultureIgnoreCase) && component.GetType() == dictionary[key])
							{
								UnityEngine.Debug.Log("Type filter found: " + component.GetType().Name);
								return true;
							}
						}
						if (component.GetType().Name.Contains(typeName, StringComparison.InvariantCultureIgnoreCase))
						{
							UnityEngine.Debug.Log("Type filter found: " + component.GetType().Name);
							return true;
						}
					}
				}
			}
			return false;
		};
	}

	private static Predicate<PropInstance> getNameFilter(string fieldName)
	{
		return delegate(PropInstance instance)
		{
			if (instance.displayName.Contains(fieldName, StringComparison.InvariantCultureIgnoreCase))
			{
				return true;
			}
			if (instance.exportName.Contains(fieldName, StringComparison.InvariantCultureIgnoreCase))
			{
				return true;
			}
			return instance.scriptName.Contains(fieldName, StringComparison.InvariantCultureIgnoreCase) ? true : false;
		};
	}

	private void initSelection()
	{
		selection = newEmptySelection();
		rectSelection.selected = new HashSet<InstanceID>();
		selectionPosCache = new Dictionary<InstanceID, (Vector2, Vector3)>();
	}

	private void changeSelection(EditorSelection newSelection)
	{
		EditorSelection editorSelection = selection;
		bool flag = !compareInstancesSelection(editorSelection.propInstanceIDs, newSelection.propInstanceIDs);
		if (isHierarchyOpened() && flag)
		{
			refresh.editorUI = true;
		}
		if (editorSelection.propInstanceIDs.Count > 0 && flag)
		{
			setGizmoTargets(new List<PropInstance>());
			if (editorSelection.propInstanceIDs.Count == 1)
			{
				InstanceID iD = InstanceID.None;
				foreach (InstanceID propInstanceID in editorSelection.propInstanceIDs)
				{
					iD = propInstanceID;
				}
				PropInstance instanceByID = getInstanceByID(iD);
				destroySlotPreview(instanceByID);
				destroySlidablePreview(instanceByID);
			}
		}
		if (editorSelection.propID != PropID.None)
		{
			Wall[] componentsInChildren = levelContainer.GetComponentsInChildren<Wall>(includeInactive: true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].forceConvexCollider = false;
			}
			UnityEngine.Object.Destroy(propToPlace);
			UnityEngine.Object.Destroy(propPreview);
			propToPlace = null;
			propPreview = null;
			propToPlacePrefab = null;
			Resources.UnloadUnusedAssets();
		}
		selection = newSelection;
		if (newSelection.propID != PropID.None)
		{
			GameObject original = (propToPlacePrefab = loadPrefab(getEditorContext(), newSelection.propID));
			propToPlace = UnityEngine.Object.Instantiate(original);
			propToPlace.name = newSelection.propID.value;
			propToPlace.SetActive(value: false);
			propToPlaceOriginalRotation = propToPlace.transform.rotation;
			propPreview = UnityEngine.Object.Instantiate(original, levelContainer.transform);
			propPreview.name = newSelection.propID.value;
			ImpostorClone.stripNonVisualComponents(propPreview, ImpostorClone.StrippingFlags.AllowParticles | ImpostorClone.StrippingFlags.AllowLights);
			propPreviewOriginalMaterials = null;
			if (propToPlace.TryGetComponent<Polygon>(out var component))
			{
				Vector3 size = ((component is Floor) ? PlayerSave.getSettings().re.floorSize : PlayerSave.getSettings().re.wallSize);
				component.turnIntoCube(size);
				Polygon component2 = propPreview.GetComponent<Polygon>();
				component2.turnIntoCube(size);
				component2.recalculate();
			}
			propPreviewBounds = Game.computeLocalBounds(propPreview, overrideAcceptEditorSpecial: true);
			if (isSpecialEffect(propToPlace.GetComponent<PropInstance>()))
			{
				if (propToPlace.TryGetComponentInChildren<BoxCollider>(out var component3) && component3.enabled && component3.isTrigger)
				{
					propPreviewBounds.center = component3.center;
					propPreviewBounds.size = component3.size;
				}
				else
				{
					UnityEngine.Debug.LogError("Effect props should have an enabled trigger box collider.");
				}
			}
			Item component4 = propPreview.GetComponent<Item>();
			if (component4 != null && component4.groundRotation != Vector3.zero)
			{
				propPreview.transform.rotation = Quaternion.Euler(component4.groundRotation);
			}
			EditorNonItemPoses component5 = propPreview.GetComponent<EditorNonItemPoses>();
			if (component5 != null && component5.groundRotation != Vector3.zero)
			{
				propPreview.transform.rotation = Quaternion.Euler(component5.groundRotation);
			}
		}
		EditorSkybox component6;
		EditorClouds component7;
		EditorOcean component8;
		Fog component9;
		EditorPostProcessing component10;
		if (newSelection.propInstanceIDs.Count > 0 && flag)
		{
			List<PropInstance> list = new List<PropInstance>(newSelection.propInstanceIDs.Count);
			foreach (InstanceID propInstanceID2 in newSelection.propInstanceIDs)
			{
				PropInstance instanceByID2 = getInstanceByID(propInstanceID2);
				list.Add(instanceByID2);
			}
			setGizmoTargets(list);
			propertiesUI.Content.anchoredPosition = Vector2.zero;
			if (newSelection.propInstanceIDs.Count == 1)
			{
				PropInstance propInstance = null;
				foreach (InstanceID propInstanceID3 in newSelection.propInstanceIDs)
				{
					propInstance = getInstanceByID(propInstanceID3);
				}
				if (propInstance.TryGetComponent<EditorSkybox>(out component6))
				{
					refresh.skybox = true;
					SkyboxData skyboxData = (skyboxCurrentActiveCache = getSkyboxData(compressProp(propInstance, getEditorContext())));
					if (skyboxData.activeOnStart)
					{
						skyboxOnStartActiveCache = skyboxData;
					}
				}
				if (propInstance.TryGetComponent<EditorClouds>(out component7))
				{
					refresh.clouds = true;
					CloudsData cloudsData = (cloudsCurrentActiveCache = getCloudsData(compressProp(propInstance, getEditorContext())));
					if (cloudsData.activeOnStart)
					{
						cloudsOnStartActiveCache = cloudsData;
					}
				}
				if (propInstance.TryGetComponent<EditorOcean>(out component8))
				{
					refresh.ocean = true;
					OceanData oceanData = (oceanCurrentActiveCache = getOceanData(compressProp(propInstance, getEditorContext())));
					if (oceanData.activeOnStart)
					{
						oceanOnStartActiveCache = oceanData;
					}
				}
				if (propInstance.TryGetComponent<Fog>(out component9))
				{
					refresh.fog = true;
					FogData fogData = (fogCurrentActiveCache = getFogData(compressProp(propInstance, getEditorContext())));
					if (fogData.activeOnStart)
					{
						fogOnStartActiveCache = fogData;
					}
				}
				if (propInstance.TryGetComponent<EditorPostProcessing>(out component10))
				{
					refresh.postProcessing = true;
					editorCurrentActivePostProcessing = getEditorPostProcessingData(compressProp(propInstance, getEditorContext()));
				}
			}
		}
		if (!(editorSelection.propInstanceIDs.Count == 1 && flag))
		{
			return;
		}
		PropInstance propInstance2 = null;
		foreach (InstanceID propInstanceID4 in editorSelection.propInstanceIDs)
		{
			propInstance2 = getInstanceByID(propInstanceID4);
		}
		if (propInstance2.TryGetComponent<EditorSkybox>(out component6))
		{
			skyboxCurrentActiveCache = null;
			refresh.skybox = true;
		}
		if (propInstance2.TryGetComponent<EditorPostProcessing>(out component10))
		{
			editorCurrentActivePostProcessing = null;
			refresh.postProcessing = true;
		}
		if (propInstance2.TryGetComponent<EditorClouds>(out component7))
		{
			cloudsCurrentActiveCache = null;
			refresh.clouds = true;
		}
		if (propInstance2.TryGetComponent<EditorOcean>(out component8))
		{
			oceanCurrentActiveCache = null;
			refresh.ocean = true;
		}
		if (propInstance2.TryGetComponent<Fog>(out component9))
		{
			fogCurrentActiveCache = null;
			refresh.fog = true;
		}
	}

	private void addSelection(PropInstance instance, bool isAddModifierPressed, bool useAlternativeSelection)
	{
		if (isAddModifierPressed)
		{
			if (selection.propInstanceIDs.Contains(instance.ID))
			{
				HashSet<InstanceID> hashSet = new HashSet<InstanceID>(selection.propInstanceIDs);
				hashSet.Remove(instance.ID);
				EditorSelection newSelection = newInstanceSelection(hashSet);
				undo.push(actionChangeSelection(newSelection));
				return;
			}
			HashSet<InstanceID> hashSet2 = new HashSet<InstanceID>(selection.propInstanceIDs);
			if (tryAddInstanceToSelection(instance, hashSet2, useAlternativeSelection))
			{
				EditorSelection newSelection2 = newInstanceSelection(hashSet2);
				undo.push(actionChangeSelection(newSelection2));
			}
		}
		else
		{
			EditorSelection newSelection3 = newInstanceSelection(instance.ID);
			if (isDifferentSelection(newSelection3))
			{
				undo.push(actionChangeSelection(newSelection3));
			}
		}
	}

	private bool tryAddInstanceToSelection(PropInstance addInstance, HashSet<InstanceID> selected, bool filterOutChildren)
	{
		bool flag = isChildOfSelected(addInstance, selected);
		if (!filterOutChildren || !flag)
		{
			List<PropInstance> list = childSearchCache;
			list.Clear();
			list.Add(addInstance);
			int num = 0;
			while (num < list.Count)
			{
				PropInstance propInstance = list[num++];
				for (int i = 0; i < propInstance.children.Count; i++)
				{
					PropInstance propInstance2 = propInstance.children[i];
					if (selected.Contains(propInstance2.ID))
					{
						selected.Remove(propInstance2.ID);
					}
					else
					{
						list.Add(propInstance2);
					}
				}
			}
			selected.Add(addInstance.ID);
		}
		if (filterOutChildren)
		{
			return !flag;
		}
		return true;
	}

	private bool isChildOfSelected(PropInstance instance, HashSet<InstanceID> selected)
	{
		bool flag = false;
		PropInstance parent = instance.parent;
		while (parent != null && !flag)
		{
			flag = selected.Contains(parent.ID);
			parent = parent.parent;
		}
		return flag;
	}

	private bool isDifferentSelection(EditorSelection newSelection)
	{
		if (selection.propID != newSelection.propID)
		{
			return true;
		}
		return !compareInstancesSelection(selection.propInstanceIDs, newSelection.propInstanceIDs);
	}

	private PropInstance getSelectedInstance()
	{
		PropInstance result = null;
		foreach (InstanceID propInstanceID in selection.propInstanceIDs)
		{
			result = getInstanceByID(propInstanceID);
		}
		return result;
	}

	private List<PropInstance> getSelectedInstances(bool includeChildInstances)
	{
		List<PropInstance> list = new List<PropInstance>(selection.propInstanceIDs.Count);
		foreach (InstanceID propInstanceID in selection.propInstanceIDs)
		{
			PropInstance instanceByID = getInstanceByID(propInstanceID);
			if (includeChildInstances)
			{
				getChildPropInstances(instanceByID, list);
			}
			else
			{
				list.Add(instanceByID);
			}
		}
		return list;
	}

	private bool cancelSelection()
	{
		EditorSelection newSelection = newEmptySelection();
		bool flag = isDifferentSelection(newSelection);
		if (flag)
		{
			undo.push(actionChangeSelection(newSelection));
		}
		return flag;
	}

	private bool popSelection()
	{
		EditorSelection newSelection = selection;
		bool flag = true;
		if (newSelection.propInstanceIDs.Count > 0)
		{
			newSelection.propInstanceIDs = new HashSet<InstanceID>();
		}
		else if (newSelection.propID != PropID.None)
		{
			newSelection.propID = PropID.None;
		}
		else
		{
			flag = false;
		}
		if (flag)
		{
			undo.push(actionChangeSelection(newSelection));
		}
		return flag;
	}

	private void cancelSelectionWithRestoreFlag()
	{
		special.restoreSelection = cancelSelection();
	}

	private void restoreSelection()
	{
		if (special.restoreSelection)
		{
			undo.undoLast();
		}
	}

	private bool isPropSelected()
	{
		return selection.propID != PropID.None;
	}

	private bool isPropInstanceSelected()
	{
		int count;
		return isPropInstanceSelected(out count);
	}

	private bool isPropInstanceSelected(out int count)
	{
		count = selection.propInstanceIDs.Count;
		return count > 0;
	}

	private bool isSelectedSingle<T>() where T : Component
	{
		T component;
		return isSelectedSingle<T>(out component);
	}

	private bool isSelectedSingle<T>(out T component) where T : Component
	{
		component = null;
		if (isPropInstanceSelected(out var count) && count == 1)
		{
			return getSelectedInstance().TryGetComponent<T>(out component);
		}
		return false;
	}

	private bool isAnyLinkedPolygonSelected()
	{
		foreach (InstanceID propInstanceID in selection.propInstanceIDs)
		{
			PropInstance instanceByID = getInstanceByID(propInstanceID);
			if (instanceByID.TryGetComponent<Wall>(out var component) && component.getFloor() != null)
			{
				return true;
			}
			if (instanceByID.TryGetComponent<Floor>(out var component2) && component2.isCeiling())
			{
				return true;
			}
		}
		return false;
	}

	private void updateRectSelection()
	{
		if (rectSelection.phase != RSP.Inactive)
		{
			Vector2 vector = Input.mousePosition;
			vector.x = Mathf.Clamp(vector.x, 0f, Screen.width);
			vector.y = Mathf.Clamp(vector.y, 0f, Screen.height);
			Vector2 anchoredPosition = new Vector2(Mathf.Min(vector.x, rectSelection.startPos.x), Mathf.Min(vector.y, rectSelection.startPos.y));
			Vector2 vector2 = new Vector2(Mathf.Max(vector.x, rectSelection.startPos.x), Mathf.Max(vector.y, rectSelection.startPos.y));
			Rect rect = Rect.MinMaxRect(anchoredPosition.x, anchoredPosition.y, vector2.x, vector2.y);
			Vector2 size = rect.size;
			if ((size.x > 10f || size.y > 10f) && rectSelection.phase == RSP.Attempt)
			{
				transformGizmo.ClearTargets();
				rectSelection.phase = RSP.Active;
				selectionPosCache.Clear();
				rectSelection.selected.Clear();
				outlineContext.outline.Clear();
			}
			rectSelectionUI.Rect.rectT().anchoredPosition = anchoredPosition;
			rectSelectionUI.Rect.rectT().sizeDelta = size;
			if (rectSelection.phase == RSP.Active)
			{
				rectSelection.mode = RSM.Normal;
				if (isCtrlPressed())
				{
					rectSelection.mode = RSM.Remove;
				}
				else if (isShiftPressed())
				{
					rectSelection.mode = RSM.Add;
				}
				HashSet<InstanceID> propInstanceIDs = selection.propInstanceIDs;
				bool flag = false;
				if (rectSelection.mode == RSM.Remove)
				{
					foreach (InstanceID item in propInstanceIDs)
					{
						PropInstance instanceByID = getInstanceByID(item);
						if (!selectionPosCache.TryGetValue(instanceByID.ID, out var value))
						{
							value.Item1 = mainCam.WorldToScreenPoint(instanceByID.transform.position);
							value.Item2 = mainCam.WorldToViewportPoint(instanceByID.transform.position);
							selectionPosCache[instanceByID.ID] = value;
						}
						bool flag2 = rectSelection.selected.Contains(instanceByID.ID);
						int num;
						if (rect.Contains(value.Item1))
						{
							num = (inViewport(value.Item2) ? 1 : 0);
							if (num != 0)
							{
								goto IL_02fc;
							}
						}
						else
						{
							num = 0;
						}
						if (!flag2)
						{
							rectSelection.selected.Add(instanceByID.ID);
							if (!isSpecialEffect(instanceByID))
							{
								outlineContext.outline.Add(instanceByID.gameObject);
							}
							flag = true;
						}
						goto IL_02fc;
						IL_02fc:
						if (((uint)num & (flag2 ? 1u : 0u)) != 0)
						{
							rectSelection.selected.Remove(instanceByID.ID);
							if (!isSpecialEffect(instanceByID))
							{
								outlineContext.outline.Remove(instanceByID.gameObject);
							}
							flag = true;
						}
					}
				}
				if (rectSelection.mode != RSM.Remove)
				{
					foreach (KeyValuePair<InstanceID, PropInstance> propInstance in propInstances)
					{
						PropInstance value2 = propInstance.Value;
						bool flag3 = value2.gameObject.layer == CAMERA_HIDE_LAYER;
						bool flag4 = rectSelection.mode == RSM.Add && propInstanceIDs.Contains(value2.ID);
						if (!flag4 && !flag3)
						{
							if (!selectionPosCache.TryGetValue(value2.ID, out var value3))
							{
								value3.Item1 = mainCam.WorldToScreenPoint(value2.transform.position);
								value3.Item2 = mainCam.WorldToViewportPoint(value2.transform.position);
								selectionPosCache[value2.ID] = value3;
							}
							flag4 = rect.Contains(value3.Item1) && inViewport(value3.Item2);
						}
						bool flag5 = rectSelection.selected.Contains(value2.ID);
						if (flag4 && !flag5)
						{
							rectSelection.selected.Add(value2.ID);
							if (!isSpecialEffect(value2))
							{
								outlineContext.outline.Add(value2.gameObject);
							}
							flag = true;
						}
						if (!flag4 && flag5)
						{
							rectSelection.selected.Remove(value2.ID);
							if (!isSpecialEffect(value2))
							{
								outlineContext.outline.Remove(value2.gameObject);
							}
							flag = true;
						}
					}
				}
				if (flag)
				{
					outlineContext.dirty = true;
				}
				int count = rectSelection.selected.Count;
				string text = ((count > 0) ? $"{count} Objects" : "");
				rectSelectionUI.Rect_Text.text = text;
			}
		}
		rectSelectionUI.root.enabled = rectSelection.phase == RSP.Active;
	}

	private void endRectSelection(bool apply, bool filterOutChildren = false)
	{
		rectSelection.phase = RSP.Inactive;
		HashSet<InstanceID> original = selection.propInstanceIDs;
		if (!apply)
		{
			revertToOriginalSelection();
			return;
		}
		HashSet<InstanceID> hashSet = new HashSet<InstanceID>();
		foreach (InstanceID item in rectSelection.selected)
		{
			if (!filterOutChildren || !isChildOfSelected(getInstanceByID(item), rectSelection.selected))
			{
				hashSet.Add(item);
			}
		}
		if (compareInstancesSelection(original, hashSet))
		{
			revertToOriginalSelection();
			return;
		}
		undo.push(actionChangeSelection(newInstanceSelection(hashSet)));
		void revertToOriginalSelection()
		{
			List<PropInstance> list = new List<PropInstance>(original.Count);
			foreach (InstanceID item2 in original)
			{
				list.Add(getInstanceByID(item2));
			}
			setGizmoTargets(list);
		}
	}

	private static bool compareInstancesSelection(HashSet<InstanceID> set1, HashSet<InstanceID> set2)
	{
		if (set1.Count != set2.Count)
		{
			return false;
		}
		foreach (InstanceID item in set1)
		{
			if (!set2.Contains(item))
			{
				return false;
			}
		}
		return true;
	}

	private static bool inViewport(Vector3 viewportPos)
	{
		float x = viewportPos.x;
		if (x >= 0f && x <= 1f)
		{
			x = viewportPos.y;
			if (x >= 0f && x <= 1f)
			{
				return viewportPos.z > 0f;
			}
		}
		return false;
	}

	private EditorSelection newEmptySelection()
	{
		return new EditorSelection
		{
			propInstanceIDs = new HashSet<InstanceID>()
		};
	}

	private EditorSelection newPropSelection(PropID id)
	{
		return new EditorSelection
		{
			propID = id,
			propInstanceIDs = new HashSet<InstanceID>()
		};
	}

	private EditorSelection newInstanceSelection(InstanceID id)
	{
		return new EditorSelection
		{
			propInstanceIDs = new HashSet<InstanceID> { id }
		};
	}

	private EditorSelection newInstanceSelection(HashSet<InstanceID> ids)
	{
		return new EditorSelection
		{
			propInstanceIDs = new HashSet<InstanceID>(ids)
		};
	}

	private void enterSpecialMode(EditorMode mode)
	{
		if (special != null)
		{
			UnityEngine.Debug.LogError($"Cannot enter special mode as you are already in special mode {special.mode}!");
			return;
		}
		undo.beginGroup(mode.ToString());
		special = new SpecialMode
		{
			mode = mode
		};
	}

	private void updateSpecialMode()
	{
		if (special == null)
		{
			return;
		}
		foreach (TargetPin targetPin in special.targetPins)
		{
			targetPin.img.enabled = false;
			if ((!(special.target != null) || !(special.target.transform == targetPin.transform)) && (special.targets == null || special.targets.Count <= 0 || !outlineContext.outline.Contains(targetPin.transform.gameObject)) && inViewport(mainCam.WorldToViewportPoint(targetPin.transform.position)) && calcTargetAlpha(targetPin.transform.position, out var alpha))
			{
				targetPin.img.enabled = true;
				Vector3 position = mainCam.WorldToScreenPoint(targetPin.transform.position);
				targetPin.img.rectT().position = position;
				targetPin.img.rectT().localScale = Vector3.one * alpha;
			}
		}
		int num = 0;
		while (special.passwordPopups != null && num < special.passwordPopups.Count)
		{
			PasswordPopup passwordPopup = special.passwordPopups[num];
			Vector3 position2 = passwordPopup._lock.transform.position;
			float num2 = 0f;
			if (inViewport(mainCam.WorldToViewportPoint(position2)) && calcTargetAlpha(position2, out var alpha2))
			{
				Vector3 position3 = mainCam.WorldToScreenPoint(passwordPopup._lock.transform.position);
				passwordPopup.pickerUI.root.position = position3;
				num2 = alpha2;
			}
			passwordPopup.pickerUI.root.localScale = Vector3.one * num2;
			num++;
		}
		if (special.mode == EditorMode.CameraRotation)
		{
			if (special.updateZoomableCamera)
			{
				special.updateZoomableCamera = false;
				Transform transform = special.target.transform;
				Vector3 lossyScale = transform.lossyScale;
				Vector3 vector = transform.right * (special.zoomableEyePosition.x * lossyScale.x);
				Vector3 vector2 = transform.up * (special.zoomableEyePosition.y * lossyScale.y);
				Vector3 vector3 = transform.forward * (special.zoomableEyePosition.z * lossyScale.z);
				cameraPosition = transform.position + vector + vector2 + vector3;
				cameraRotation = transform.rotation * Quaternion.Euler(special.zoomableEyeRotation);
			}
			else
			{
				getZoomableRotationFromCamera(special.target.transform, mainCam.transform, out special.zoomableEyePosition, out special.zoomableEyeRotation);
			}
		}
		bool calcTargetAlpha(Vector3 targetPos, out float reference)
		{
			float num3 = (targetPos - mainCam.transform.position).sqrMagnitude / 50f;
			reference = Mathf.Lerp(1f, 0.5f, num3);
			return num3 <= 1f;
		}
	}

	private void exitSpecialMode(bool keepChanges = true)
	{
		if (special == null)
		{
			UnityEngine.Debug.LogError("Cannot exit special mode as you are not in special mode!");
			return;
		}
		undo.endGroup(keepChanges);
		special = null;
	}

	private bool isSpecialMode(EditorMode mode)
	{
		if (special != null)
		{
			return special.mode == mode;
		}
		return false;
	}

	private void toggleTweenStateMode(bool show, bool apply = false)
	{
		togglePivotMode(show, delegate(PropInstance selectedInstance)
		{
			TweenState component = selectedInstance.gameObject.GetComponent<TweenState>();
			special.restoreTweenState = component.tweenStateDataNeo;
			GameObject gameObject = UnityEngine.Object.Instantiate(component.gameObject);
			if (selectedInstance.TryGetComponent<PropTags>(out var component2) && component2.tags.Contains(PropTag.BuildingDoors))
			{
				foreach (Transform item5 in gameObject.transform)
				{
					if (!(item5.GetComponentInChildren<Renderer>() == null))
					{
						gameObject = item5.gameObject;
						break;
					}
				}
			}
			ImpostorClone.stripNonVisualComponents(gameObject, ImpostorClone.StrippingFlags.AllowLights);
			gameObject.name = "pivot";
			special.pivotTransform = gameObject.transform;
			special.originalMaterials = setGhostMaterial(gameObject.gameObject);
			transformGizmo.ClearAndAddTarget(gameObject.transform);
			if (component.tweenStateDataNeo != null && component.tweenStateDataNeo.Count > 0)
			{
				List<TweenState.TweenStateRecord> tweenStateDataNeo = component.tweenStateDataNeo;
				TweenState.TweenStateRecord tweenStateRecord = tweenStateDataNeo[tweenStateDataNeo.Count - 1];
				Transform obj = gameObject.transform;
				Transform obj2 = selectedInstance.transform;
				List<ObjectState> states = tweenStateRecord.states;
				obj.position = obj2.TransformPoint(states[states.Count - 1].Transform_localPosition);
				Transform obj3 = gameObject.transform;
				Quaternion rotation = selectedInstance.transform.rotation;
				List<ObjectState> states2 = tweenStateRecord.states;
				obj3.rotation = rotation * states2[states2.Count - 1].Transform_localRotation;
			}
			else
			{
				gameObject.transform.position = selectedInstance.transform.position;
				gameObject.transform.rotation = selectedInstance.transform.rotation;
			}
		}, delegate(PropInstance selectedInstance)
		{
			restoreOriginalMaterials(special.originalMaterials);
			if (apply)
			{
				(PropData prev, PropData next) tuple = prepChangeData(selectedInstance);
				PropData item = tuple.prev;
				PropData item2 = tuple.next;
				TweenStateDataRoomEditor tweenStateDataRoomEditor = getTweenStateDataRoomEditor(item2);
				string path = "";
				if (selectedInstance.TryGetComponent<PropTags>(out var component) && component.tags.Contains(PropTag.BuildingDoors))
				{
					foreach (Transform item6 in selectedInstance.transform)
					{
						if (!(item6.GetComponentInChildren<Renderer>() == null))
						{
							path = item6.name;
							break;
						}
					}
				}
				ObjectStateRoomEditor item3 = new ObjectStateRoomEditor
				{
					flags = 80,
					path = path,
					gameObjectId = selectedInstance.ID,
					Transform_localPosition = selectedInstance.transform.InverseTransformPoint(special.pivotTransform.position),
					Transform_localRotation = Quaternion.Inverse(selectedInstance.transform.rotation) * special.pivotTransform.rotation
				};
				TweenStateRecordRoomEditor item4 = new TweenStateRecordRoomEditor
				{
					weight = 0f,
					name = "Down",
					states = new List<ObjectStateRoomEditor> { item3 }
				};
				tweenStateDataRoomEditor.tweenStates.Clear();
				tweenStateDataRoomEditor.tweenStates.Add(item4);
				undo.push(actionChangeProp(item, item2, "change tween state"));
			}
			else
			{
				refresh.transforms = true;
			}
			UnityEngine.Object.DestroyImmediate(special.pivotTransform.gameObject);
			transformGizmo.ClearAndAddTarget(selectedInstance.transform);
		});
	}

	private void toggleSlotPivotMode(bool show, bool apply = false)
	{
		togglePivotMode(show, delegate(PropInstance selectedInstance)
		{
			Slot component = selectedInstance.gameObject.GetComponent<Slot>();
			special.restorePivot = saveTransform(component.pivot.transform, isLocal: true);
			UnityEngine.Object.Destroy(component.pivot.gameObject);
			GameObject gameObject = null;
			gameObject = ((component.acceptItems.Length == 0) ? component.rejectItems[0].gameObject : component.acceptItems[0].gameObject);
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject);
			ImpostorClone.stripNonVisualComponents(gameObject2, ImpostorClone.StrippingFlags.AllowLights);
			gameObject2.name = "pivot";
			gameObject2.transform.parent = component.transform;
			applyTransform(gameObject2.transform, special.restorePivot, ignoreScale: true);
			component.pivot = gameObject2.transform;
			transformGizmo.ClearAndAddTarget(component.pivot.transform);
		}, delegate(PropInstance selectedInstance)
		{
			Slot component = selectedInstance.gameObject.GetComponent<Slot>();
			Transform transform = new GameObject().transform;
			transform.name = "pivot";
			transform.SetParent(component.transform);
			applyTransform(transform, saveTransform(component.pivot, isLocal: true));
			transform.gameObject.SetActive(value: false);
			UnityEngine.Object.Destroy(component.pivot.gameObject);
			component.pivot = transform;
			if (apply)
			{
				var (propData, next) = prepChangeData(selectedInstance);
				getSlotData(propData).pivot = special.restorePivot;
				undo.register(actionChangeProp(propData, next, "change pivot"));
			}
			else
			{
				applyTransform(component.pivot.transform, special.restorePivot);
				refresh.transforms = true;
			}
			createSlotPreview(selectedInstance);
		});
	}

	private void toggleSlidablePivotMode(bool show, bool apply = false)
	{
		togglePivotMode(show, delegate(PropInstance selectedInstance)
		{
			Slidable component = selectedInstance.gameObject.GetComponent<Slidable>();
			special.restorePivot = saveTransform(component.endNode.transform, isLocal: true);
			UnityEngine.Object.Destroy(component.endNode.gameObject);
			GameObject gameObject = UnityEngine.Object.Instantiate(component.gameObject);
			ImpostorClone.stripNonVisualComponents(gameObject, ImpostorClone.StrippingFlags.AllowLights);
			gameObject.name = "endNode";
			component.endNode = gameObject.transform;
			component.endNode.SetParent(component.transform);
			applyTransform(component.endNode, special.restorePivot);
			gameObject.AddComponent<EditorPreviewCloneTag>();
			Transform transform = new GameObject().transform;
			transform.name = "fixNonUniformScale";
			transform.parent = component.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = new Vector3(1f / component.transform.localScale.x, 1f / component.transform.localScale.y, 1f / component.transform.localScale.z);
			component.endNode.SetParent(transform);
			special.originalMaterials = setGhostMaterial(selectedInstance.gameObject, component.endNode.gameObject);
			transformGizmo.ClearAndAddTarget(component.endNode.transform);
			destroySlidablePreview(selectedInstance);
			createSlidablePreview(selectedInstance, specialMode: true);
		}, delegate(PropInstance selectedInstance)
		{
			destroySlidablePreview(selectedInstance);
			Slidable component = selectedInstance.gameObject.GetComponent<Slidable>();
			Transform transform = new GameObject().transform;
			transform.name = "endNode";
			transform.SetParent(component.transform);
			component.endNode.SetParent(component.transform);
			Transform transform2 = component.transform.Find("fixNonUniformScale");
			if (transform2 != null)
			{
				UnityEngine.Object.Destroy(transform2.gameObject);
			}
			applyTransform(transform, saveTransform(component.endNode, isLocal: true));
			transform.gameObject.SetActive(value: false);
			UnityEngine.Object.Destroy(component.endNode.gameObject);
			component.endNode = transform;
			restoreOriginalMaterials(special.originalMaterials);
			if (apply)
			{
				var (propData, next) = prepChangeData(selectedInstance);
				getSlidableData(propData).endNode = special.restorePivot;
				undo.push(actionChangeProp(propData, next, "slidable change end node pivot"));
			}
			else
			{
				applyTransform(component.endNode.transform, special.restorePivot);
				refresh.transforms = true;
			}
		});
	}

	private void togglePivotMode(bool show, Action<PropInstance> onShow, Action<PropInstance> onHide)
	{
		editorUI.root.enabled = !show;
		targetModeUI.root.enabled = show;
		targetModeUI.ResetBackground.gameObject.SetActive(show);
		targetModeUI.Buttons_Apply.gameObject.SetActive(show);
		if (show)
		{
			enterSpecialMode(EditorMode.Pivot);
			using (IEnumerator<Toggle> enumerator = transformUI.ToolsBackground.GetComponent<ToggleGroup>().ActiveToggles().GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					Toggle current = enumerator.Current;
					special.restoreTransformTool = current;
				}
			}
			transformUI.Scale.interactable = false;
			PropInstance selectedInstance = getSelectedInstance();
			cancelSelectionWithRestoreFlag();
			special.target = selectedInstance;
			onShow(selectedInstance);
		}
		else
		{
			restoreSelection();
			onHide(getSelectedInstance());
			special.restoreTransformTool.isOn = true;
			transformUI.Scale.interactable = true;
			exitSpecialMode();
		}
	}

	private void toggleFinishRotationMode(bool show, bool apply = false)
	{
		if (!show)
		{
			restoreSelection();
		}
		if (apply)
		{
			(PropData prev, PropData next) tuple = prepChangeData(getSelectedInstance());
			PropData item = tuple.prev;
			PropData item2 = tuple.next;
			FinishData finishData = getFinishData(item2);
			finishData.eyeRotation = special.zoomableEyeRotation;
			finishData.eyePosition = special.zoomableEyePosition;
			undo.push(actionChangeProp(item, item2, "change finish pose"));
		}
		Finish component = getSelectedInstance().GetComponent<Finish>();
		toggleRotationMode(show, ref component.levelShot.eyeLocalPosition, ref component.levelShot.eyeLocalRotation);
	}

	private void toggleZoomableRotationMode(bool show, bool apply = false)
	{
		if (!show)
		{
			restoreSelection();
		}
		if (apply)
		{
			(PropData prev, PropData next) tuple = prepChangeData(getSelectedInstance());
			PropData item = tuple.prev;
			PropData item2 = tuple.next;
			ZoomableData zoomableData = getZoomableData(item2);
			zoomableData.eyeRotation = special.zoomableEyeRotation;
			zoomableData.eyePosition = special.zoomableEyePosition;
			undo.push(actionChangeProp(item, item2, "change zoomable rotation"));
		}
		Zoomable component = getSelectedInstance().GetComponent<Zoomable>();
		toggleRotationMode(show, ref component.eyeLocalPosition, ref component.eyeLocalRotation);
	}

	private void toggleRotationMode(bool show, ref Vector3 position, ref Vector3 rotation)
	{
		editorUI.root.enabled = !show;
		targetModeUI.root.enabled = show;
		targetModeUI.ResetBackground.gameObject.SetActive(value: false);
		targetModeUI.Buttons_Apply.gameObject.SetActive(show);
		targetModeUI.Properties.gameObject.SetActive(show);
		Transform parent = targetModeUI.Properties.transform;
		inputFieldNavigationDataSpecial.removeOnEndEditListeners();
		if (show)
		{
			enterSpecialMode(EditorMode.CameraRotation);
			special.restoreCamPos = cameraPosition;
			special.restoreCamRot = cameraRotation;
			PropInstance selectedInstance = getSelectedInstance();
			cancelSelectionWithRestoreFlag();
			special.target = selectedInstance;
			special.zoomableEyeRotation = rotation;
			special.zoomableEyePosition = position;
			startPropertySection(parent, UITooltip.Position.Right);
			defineHintProperty("Hold right click to position the camera\nusing the mouse and keyboard.\nSet specific values below if needed.", "Hold right click and use WASD+QE to position the camera.");
			defineVector3Property("Eye Position", "Position from where camera is looking at (position is relative to the prop).", () => special.zoomableEyePosition, delegate(Vector3 value)
			{
				special.zoomableEyePosition = value;
			}, delegate
			{
				special.updateZoomableCamera = true;
			}, delegate
			{
				special.updateZoomableCamera = true;
			}, 0.1f);
			defineVector3Property("Eye Rotation", "Rotation of the camera (rotation is relative to the prop). Rotate the Z axis manually to tilt the camera.", () => special.zoomableEyeRotation, delegate(Vector3 value)
			{
				special.zoomableEyeRotation = value;
			}, delegate
			{
				special.updateZoomableCamera = true;
			}, delegate
			{
				special.updateZoomableCamera = true;
			}, 0.1f);
			defineButtonProperty("Center On Prop", "Change the position of the camera in order to center the prop on the screen.\nDistance and rotation of the camera doesn't change.", delegate
			{
				special.updateZoomableCamera = true;
				Bounds bounds = Game.computeLocalBounds(special.target.gameObject);
				Vector3 vector = special.target.transform.TransformPoint(bounds.center);
				Vector3 normalized = (cameraRotation * Vector3.forward).normalized;
				Vector3 vector2 = vector - cameraPosition;
				Vector3 vector3 = Vector3.Project(vector2, normalized);
				if (Vector3.Dot(normalized, vector2.normalized) < 0f)
				{
					special.zoomableEyePosition = special.target.transform.InverseTransformPoint(vector + vector3);
				}
				else
				{
					special.zoomableEyePosition = special.target.transform.InverseTransformPoint(vector - vector3);
				}
			});
			tweenCameraTo(Zoomable.calculateEyePosition(selectedInstance.transform, position), Zoomable.calculateEyeRotation(selectedInstance.transform, rotation));
		}
		else
		{
			removePropertiesForParent(parent);
			tweenCameraTo(special.restoreCamPos, special.restoreCamRot);
			exitSpecialMode();
			refreshTargetPropertiesUI();
		}
		inputFieldNavigationDataSpecial.initNavigation(parent, "SpecialMode");
	}

	private void getZoomableRotationFromCamera(Transform target, Transform camera, out Vector3 eyePosition, out Vector3 eyeRotation)
	{
		eyePosition = target.InverseTransformPoint(camera.transform.position);
		eyeRotation = (Quaternion.Inverse(target.rotation) * camera.rotation).eulerAngles;
	}

	private void updateUndoSystem()
	{
		bool flag = (Application.isEditor ? Input.GetKeyDown(KeyCode.F6) : menuOptions.getActionKeyDown(KeyBindingAction.EditorUndo));
		bool flag2 = (Application.isEditor ? Input.GetKeyDown(KeyCode.F7) : menuOptions.getActionKeyDown(KeyBindingAction.EditorRedo));
		if (isSpecialMode(EditorMode.CodeEditor))
		{
			if (flag)
			{
				textEditorUI.Editor.Undo();
			}
			if (flag2)
			{
				textEditorUI.Editor.Redo();
			}
		}
		else
		{
			if (editorUI.Undo.interactable != undo.isUndoable)
			{
				editorUI.Undo.GetComponent<UnityEngine.UI.Image>().color = Color.black;
				editorUI.Undo.interactable = undo.isUndoable;
			}
			if (editorUI.Redo.interactable != undo.isRedoable)
			{
				editorUI.Redo.GetComponent<UnityEngine.UI.Image>().color = Color.black;
				editorUI.Redo.interactable = undo.isRedoable;
			}
			if (flag)
			{
				undo.undoLast(log: true);
			}
			if (flag2)
			{
				undo.redoLast(dry: false, log: true);
			}
			if (Input.GetKeyDown(KeyCode.F4))
			{
				UnityEngine.Debug.Log(undo.ToString());
			}
		}
		editorUI.SettingsContent_Save_Text.text = (undo.isDirty ? DIRTY_SCENE_SAVE_TEXT : CLEAN_SCENE_SAVE_TEXT);
		editorUI.SettingsContent_Discard.gameObject.SetActive(undo.isDirty);
	}

	private EditorAction actionChangeHidden(HashSet<InstanceID> newHidden)
	{
		HashSet<InstanceID> prevSelection = new HashSet<InstanceID>(hiddenPropInstanceIDs);
		return new EditorAction
		{
			name = "actionChangeHidden",
			redo = redo,
			undo = undo,
			affectsRoomState = false
		};
		void redo()
		{
			changeHidden(newHidden);
		}
		void undo()
		{
			changeHidden(prevSelection);
		}
	}

	private EditorAction actionChangeTransform(PropInstance instance, string change = "")
	{
		(PropData prev, PropData next) tuple = prepChangeData(instance);
		PropData item = tuple.prev;
		PropData item2 = tuple.next;
		item.transform = cachedTransforms[instance.ID];
		return actionChangeProp(item, item2, change);
	}

	private EditorAction actionChangeTransform(InstanceID instanceID, TransformData transformData, string change = "")
	{
		return new EditorAction
		{
			name = "actionChangeTransform: " + change,
			redo = redo,
			undo = undo
		};
		void redo()
		{
			applyTransform(getInstanceByID(instanceID).transform, transformData);
		}
		static void undo()
		{
		}
	}

	private EditorAction actionChangeProp(PropData prev, PropData next, string change = "", bool refreshVolume = false)
	{
		return new EditorAction
		{
			name = "actionChangeProp: " + change,
			redo = redo,
			undo = undo
		};
		void redo()
		{
			changeProp(prev, next, getEditorContext());
			if (special == null)
			{
				if (next != null && selection.propInstanceIDs.Contains(next.ID))
				{
					cachedTransforms[next.ID] = next.transform;
				}
				refresh.propertiesUI = true;
				refresh.transforms = true;
				refreshVolumeIfNeeded();
			}
		}
		void refreshVolumeIfNeeded()
		{
			if (refreshVolume)
			{
				refresh.skybox = true;
				refresh.postProcessing = true;
				refresh.clouds = true;
				refresh.ocean = true;
				refresh.fog = true;
			}
		}
		void undo()
		{
			changeProp(next, prev, getEditorContext());
			if (special == null)
			{
				if (prev != null && selection.propInstanceIDs.Contains(prev.ID))
				{
					cachedTransforms[prev.ID] = prev.transform;
				}
				refresh.propertiesUI = true;
				refresh.transforms = true;
				refreshVolumeIfNeeded();
			}
		}
	}

	private EditorAction actionChangeSelection(EditorSelection newSelection)
	{
		EditorSelection prevSelection = selection;
		string text = "";
		return new EditorAction
		{
			name = "actionChangeSelection: " + text,
			redo = redo,
			undo = undo,
			affectsRoomState = false
		};
		void redo()
		{
			changeSelection(newSelection);
		}
		void undo()
		{
			changeSelection(prevSelection);
		}
	}

	private void initUserAssets()
	{
		supportedAssetFormats.AddRange(supportedAudioAssetFormats);
		supportedAssetFormats.AddRange(supportedTextureAssetFormats);
		supportedAssetFormats.AddRange(supportedScriptAssetFormats);
		supportedAssetFormats.AddRange(supportedModelAssetFormats);
		supportedAssetFormats.AddRange(supportedMaterialAssetFormats);
		refresh.userAssets = true;
	}

	private void refreshUserAssets()
	{
		if (!Directory.Exists(roomDirPath))
		{
			UnityEngine.Debug.Log("No user assets found. Directory does not exist: " + roomDirPath);
			return;
		}
		List<UserAsset> list = new List<UserAsset>();
		string[] files = Directory.GetFiles(roomDirPath, "*.*", SearchOption.AllDirectories);
		foreach (string filePath in files)
		{
			string extension = Path.GetExtension(filePath);
			if (!supportedAssetFormats.Contains(extension) || (supportedModelAssetFormats.Contains(extension) && !filePath.Contains("_CustomModels")))
			{
				continue;
			}
			UserAsset userAsset = userAssets.Find((UserAsset x) => x.path == filePath);
			bool flag = true;
			if (userAsset != null)
			{
				if (File.GetLastWriteTime(filePath) != userAsset.lastUpdated)
				{
					userAssets.Remove(userAsset);
				}
				else
				{
					flag = false;
				}
			}
			if (!flag)
			{
				continue;
			}
			UserAsset asset = null;
			if (supportedAudioAssetFormats.Contains(extension))
			{
				asset = new AudioAsset
				{
					path = filePath,
					lastUpdated = File.GetLastWriteTime(filePath)
				};
			}
			if (supportedScriptAssetFormats.Contains(extension))
			{
				asset = new ScriptAsset
				{
					path = filePath,
					lastUpdated = File.GetLastWriteTime(filePath)
				};
			}
			if (supportedModelAssetFormats.Contains(extension))
			{
				asset = new ModelAsset
				{
					path = filePath,
					lastUpdated = File.GetLastWriteTime(filePath)
				};
			}
			if (supportedMaterialAssetFormats.Contains(extension))
			{
				asset = new MaterialAsset
				{
					path = filePath,
					lastUpdated = File.GetLastWriteTime(filePath)
				};
			}
			if (supportedTextureAssetFormats.Contains(extension))
			{
				asset = new TextureAsset
				{
					path = filePath,
					lastUpdated = File.GetLastWriteTime(filePath),
					texture = new Texture2D(2, 2)
				};
				loadTextureFromDisk(asset.path, $"asset_{asset.id}", delegate(UnityEngine.Texture texture)
				{
					TextureAsset textureAsset = (TextureAsset)userAssets.Find((UserAsset x) => x.path == asset.path);
					textureAsset.texture = (Texture2D)texture;
					applyChangedAssets(new List<UserAsset> { textureAsset });
				});
			}
			if (asset == null)
			{
				UnityEngine.Debug.LogWarning("Unsupported asset format: " + extension + " in file " + filePath);
				continue;
			}
			asset.name = Path.GetFileNameWithoutExtension(filePath);
			asset.nameWithExtension = Path.GetFileName(filePath);
			userAssets.Add(asset);
			list.Add(asset);
		}
		applyChangedAssets(list);
	}

	private void applyChangedAssets(List<UserAsset> changedAssets)
	{
		List<TextureAsset> list = new List<TextureAsset>();
		foreach (UserAsset changedAsset in changedAssets)
		{
			if (changedAsset is TextureAsset item)
			{
				list.Add(item);
			}
			else if (changedAsset is MaterialAsset materialAsset)
			{
				handleMaterialAssetChanged(materialAsset);
			}
			else if (changedAsset is AudioAsset && soundPickerUI.root.enabled)
			{
				refreshSoundPickerUI();
			}
		}
		handleTextureAssetsChanged(list);
		refreshTargetPropertiesUI();
		if (assetBrowserUI.gameObject.activeInHierarchy)
		{
			assetBrowserUI.Header_Search_Input.onEndEdit.Invoke(assetBrowserUI.Header_Search_Input.text);
		}
		if (currentTab.type == EditorTab.Assets)
		{
			refreshAssetsTab();
		}
	}

	private List<UserAsset> findUserAssets<T>(string filter, Type assetTypeToIgnore = null) where T : UserAsset
	{
		filter = filter.ToLower();
		List<UserAsset> list = new List<UserAsset>();
		foreach (UserAsset userAsset in userAssets)
		{
			if (userAsset is T && userAsset.nameWithExtension.ToLower().Contains(filter) && (!(assetTypeToIgnore != null) || !(userAsset.GetType() == assetTypeToIgnore)))
			{
				list.Add(userAsset);
			}
		}
		return list;
	}

	private void importUserAsset(string path, bool isCustomModel)
	{
		refresh.userAssets = true;
		string destinationPath = roomDirPath;
		if (isCustomModel)
		{
			destinationPath = Path.Combine(roomDirPath, "_CustomModels");
		}
		copyFileOrFolder(path, destinationPath);
	}

	private bool copyFileOrFolder(string sourcePath, string destinationPath)
	{
		try
		{
			if (!File.Exists(sourcePath) && !Directory.Exists(sourcePath))
			{
				UnityEngine.Debug.LogError("Source path does not exist: " + sourcePath);
				return false;
			}
			string directoryName = Path.GetDirectoryName(destinationPath);
			if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			string text = destinationPath;
			if (!Path.HasExtension(destinationPath))
			{
				text = Path.Combine(destinationPath, Path.GetFileName(sourcePath));
			}
			if (File.Exists(sourcePath))
			{
				File.Copy(sourcePath, text, overwrite: true);
				UnityEngine.Debug.Log("File copied from " + sourcePath + " to " + text);
				return true;
			}
			if (Directory.Exists(sourcePath))
			{
				copyDirectory(sourcePath, text);
				UnityEngine.Debug.Log("Directory copied from " + sourcePath + " to " + text);
				return true;
			}
			return false;
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError("Error copying: " + sourcePath + ", (" + ex.Message + ")");
			return false;
		}
		static void copyDirectory(string sourceDir, string destDir)
		{
			if (!Directory.Exists(destDir))
			{
				Directory.CreateDirectory(destDir);
			}
			string[] files = Directory.GetFiles(sourceDir);
			foreach (string text2 in files)
			{
				string destFileName = Path.Combine(destDir, Path.GetFileName(text2));
				File.Copy(text2, destFileName, overwrite: true);
			}
			files = Directory.GetDirectories(sourceDir);
			foreach (string text3 in files)
			{
				string destDir2 = Path.Combine(destDir, Path.GetFileName(text3));
				copyDirectory(text3, destDir2);
			}
		}
	}

	private void onButtonClickWalkthrough(Button button)
	{
		if (button == walkthroughUI.WalkthroughScroll_Content_AddStepButton)
		{
			addNewHeader();
		}
		if (button == walkthroughUI.SaveAndBack)
		{
			closeWalkthroughUI();
		}
		for (int num = walkthroughSteps.Count - 1; num >= 0; num--)
		{
			CustomWalkthroughHandler.Header step = walkthroughSteps[num];
			if (button == step.ui.DeleteButton)
			{
				deleteHeader(step);
			}
			else if (button == step.ui.AddStepButton)
			{
				addNewSingleStep(ref step);
			}
			for (int num2 = step.stepUIs.Count - 1; num2 >= 0; num2--)
			{
				CustomWalkthroughSingleStepUI customWalkthroughSingleStepUI = step.stepUIs[num2];
				if (button == customWalkthroughSingleStepUI.DeleteButton)
				{
					deleteSingleStep(step, customWalkthroughSingleStepUI);
				}
			}
		}
	}

	private void closeWalkthroughUI()
	{
		if (getActiveWalkthrough(out var walkthrough))
		{
			publishingRoomInfo.roomWalkthrough = walkthrough;
			saveRoom();
		}
		walkthroughUI.root.enabled = false;
	}

	public bool getActiveWalkthrough(out List<string> walkthrough)
	{
		walkthrough = new List<string>();
		foreach (CustomWalkthroughHandler.Header walkthroughStep in walkthroughSteps)
		{
			string text = walkthroughStep.ui.StepTitle_InputField.text;
			if (string.IsNullOrEmpty(text))
			{
				text = "...";
			}
			walkthrough.Add(CustomWalkthroughHandler.headerPrefix + CustomWalkthroughHandler.separator + text);
			foreach (CustomWalkthroughSingleStepUI stepUI in walkthroughStep.stepUIs)
			{
				walkthrough.Add(CustomWalkthroughHandler.stepPrefix + CustomWalkthroughHandler.separator + stepUI.InputField.text);
			}
		}
		return walkthroughSteps.Count > 0;
	}

	public void addNewHeader()
	{
		walkthroughSteps.Add(buildHeader(""));
	}

	public void addNewSingleStep(ref CustomWalkthroughHandler.Header step)
	{
		int num = step.stepUIs.Count + 1;
		CustomWalkthroughSingleStepUI customWalkthroughSingleStepUI = buildSingleStep(step, num, "");
		step.ui.AddStepButton_StepNumber_Text.text = (num + 1).ToString();
		customWalkthroughSingleStepUI.InputField.Select();
	}

	private void createWalkthrough(List<string> walkthrough)
	{
		foreach (CustomWalkthroughHandler.Header walkthroughStep in walkthroughSteps)
		{
			UnityEngine.Object.Destroy(walkthroughStep.ui.gameObject);
		}
		walkthroughSteps.Clear();
		CustomWalkthroughHandler.Header header = default(CustomWalkthroughHandler.Header);
		foreach (string item in walkthrough)
		{
			bool isHeader;
			string text = CustomWalkthroughHandler.processText(item, out isHeader);
			if (!string.IsNullOrEmpty(text))
			{
				if (isHeader)
				{
					header = buildHeader(text);
					walkthroughSteps.Add(header);
				}
				else if (header.stepUIs != null)
				{
					int stepIndex = header.stepUIs.Count + 1;
					buildSingleStep(header, stepIndex, text);
				}
			}
		}
		sendAddStepButtonToBottom();
	}

	private void updateSingleStepNumbers(CustomWalkthroughHandler.Header step)
	{
		int num = 1;
		foreach (CustomWalkthroughSingleStepUI stepUI in step.stepUIs)
		{
			stepUI.StepNumber_Text.text = num.ToString();
			num++;
		}
		step.ui.AddStepButton_StepNumber_Text.text = num.ToString();
	}

	private CustomWalkthroughHandler.Header buildHeader(string headerText)
	{
		CustomWalkthroughStepUI walkthroughScroll_Content_CustomWalkthroughStepUI = walkthroughUI.WalkthroughScroll_Content_CustomWalkthroughStepUI;
		CustomWalkthroughStepUI customWalkthroughStepUI = UnityEngine.Object.Instantiate(walkthroughScroll_Content_CustomWalkthroughStepUI, walkthroughScroll_Content_CustomWalkthroughStepUI.transform.parent);
		customWalkthroughStepUI.StepTitle_InputField.text = headerText;
		customWalkthroughStepUI.gameObject.SetActive(value: true);
		CustomWalkthroughHandler.Header result = new CustomWalkthroughHandler.Header
		{
			ui = customWalkthroughStepUI,
			headerTitle = headerText,
			stepUIs = new List<CustomWalkthroughSingleStepUI>()
		};
		sendAddStepButtonToBottom();
		Menu.getTheme().themeWithChildren(customWalkthroughStepUI.gameObject);
		return result;
	}

	private CustomWalkthroughSingleStepUI buildSingleStep(CustomWalkthroughHandler.Header header, int stepIndex, string rawStep)
	{
		CustomWalkthroughSingleStepUI customWalkthroughSingleStepUI = header.ui.CustomWalkthroughSingleStepUI;
		CustomWalkthroughSingleStepUI customWalkthroughSingleStepUI2 = UnityEngine.Object.Instantiate(customWalkthroughSingleStepUI, customWalkthroughSingleStepUI.transform.parent);
		string text = rawStep.Trim();
		customWalkthroughSingleStepUI2.StepNumber_Text.text = stepIndex.ToString();
		customWalkthroughSingleStepUI2.InputField.text = text;
		customWalkthroughSingleStepUI2.InputField.GetComponent<ContentSizeFitter>().SetLayoutVertical();
		customWalkthroughSingleStepUI2.gameObject.SetActive(value: true);
		sendAddSingleStepButtonToTheBottom(header.ui);
		header.stepUIs.Add(customWalkthroughSingleStepUI2);
		updateSingleStepNumbers(header);
		return customWalkthroughSingleStepUI2;
	}

	private void deleteHeader(CustomWalkthroughHandler.Header header)
	{
		if (header.ui.DeleteButton_RedTrashcan.gameObject.activeInHierarchy)
		{
			tweener.destroyTweens(header.ui.DeleteButton_RedTrashcan.gameObject, performEvents: false, performFinalUpdate: false);
			int obj = walkthroughSteps.IndexOf(header);
			onDeleteHeader?.Invoke(obj);
			UnityEngine.Object.Destroy(header.ui.gameObject);
			walkthroughSteps.Remove(header);
		}
		else
		{
			header.ui.DeleteButton_RedTrashcan.gameObject.SetActive(value: true);
			tweener.destroyTweens(header.ui.DeleteButton_RedTrashcan.gameObject, performEvents: false, performFinalUpdate: false);
			tweener.tween(header.ui.DeleteButton_RedTrashcan.gameObject, 2f, 0f, deactivate: false, activate: false, destroy: false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, delegate
			{
				header.ui.DeleteButton_RedTrashcan.gameObject.SetActive(value: false);
			});
		}
	}

	private void deleteSingleStep(CustomWalkthroughHandler.Header header, CustomWalkthroughSingleStepUI singleStep)
	{
		if (singleStep.DeleteButton_RedConfirm.gameObject.activeSelf)
		{
			tweener.destroyTweens(singleStep.DeleteButton_RedConfirm.gameObject, performEvents: false, performFinalUpdate: false);
			header.stepUIs.Remove(singleStep);
			UnityEngine.Object.Destroy(singleStep.gameObject);
			updateSingleStepNumbers(header);
		}
		else
		{
			singleStep.DeleteButton_RedConfirm.gameObject.SetActive(value: true);
			tweener.destroyTweens(singleStep.DeleteButton_RedConfirm.gameObject, performEvents: false, performFinalUpdate: false);
			tweener.tween(singleStep.DeleteButton_RedConfirm.gameObject, 2f, 0f, deactivate: false, activate: false, destroy: false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, delegate
			{
				singleStep.DeleteButton_RedConfirm.gameObject.SetActive(value: false);
			});
		}
	}

	private void sendAddStepButtonToBottom()
	{
		walkthroughUI.WalkthroughScroll_Content_AddStepButton.transform.parent.SetAsLastSibling();
		LayoutRebuilder.MarkLayoutForRebuild(walkthroughUI.WalkthroughScroll_Content_AddStepButton.transform.parent.parent.GetComponent<RectTransform>());
	}

	private void sendAddSingleStepButtonToTheBottom(CustomWalkthroughStepUI stepUI)
	{
		stepUI.AddStepButton.transform.SetAsLastSibling();
		LayoutRebuilder.MarkLayoutForRebuild(stepUI.transform.parent.GetComponent<RectTransform>());
	}

	[CompilerGenerated]
	private static bool tryGetActivatorComponentData(PropData propData, out ActivatorComponentData data)
	{
		data = getActivatorComponentData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static ActivatorComponentData getActivatorComponentData(PropData propData)
	{
		if (propData == null || propData.activator.Count == 0)
		{
			return null;
		}
		return propData.activator[0];
	}

	[CompilerGenerated]
	private static bool tryGetCloudsData(PropData propData, out CloudsData data)
	{
		data = getCloudsData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static CloudsData getCloudsData(PropData propData)
	{
		if (propData == null || propData.clouds.Count == 0)
		{
			return null;
		}
		return propData.clouds[0];
	}

	[CompilerGenerated]
	private static bool tryGetCustomModelData(PropData propData, out CustomModelData data)
	{
		data = getCustomModelData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static CustomModelData getCustomModelData(PropData propData)
	{
		if (propData == null || propData.customModel.Count == 0)
		{
			return null;
		}
		return propData.customModel[0];
	}

	[CompilerGenerated]
	private static bool tryGetDelayData(PropData propData, out DelayData data)
	{
		data = getDelayData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static DelayData getDelayData(PropData propData)
	{
		if (propData == null || propData.delay.Count == 0)
		{
			return null;
		}
		return propData.delay[0];
	}

	[CompilerGenerated]
	private static bool tryGetDialData(PropData propData, out DialData data)
	{
		data = getDialData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static DialData getDialData(PropData propData)
	{
		if (propData == null || propData.dial.Count == 0)
		{
			return null;
		}
		return propData.dial[0];
	}

	[CompilerGenerated]
	private static bool tryGetEditorDisplayData(PropData propData, out EditorDisplayData data)
	{
		data = getEditorDisplayData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static EditorDisplayData getEditorDisplayData(PropData propData)
	{
		if (propData == null || propData.display.Count == 0)
		{
			return null;
		}
		return propData.display[0];
	}

	[CompilerGenerated]
	private static bool tryGetDraggableData(PropData propData, out DraggableData data)
	{
		data = getDraggableData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static DraggableData getDraggableData(PropData propData)
	{
		if (propData == null || propData.draggable.Count == 0)
		{
			return null;
		}
		return propData.draggable[0];
	}

	[CompilerGenerated]
	private static bool tryGetFinishData(PropData propData, out FinishData data)
	{
		data = getFinishData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static FinishData getFinishData(PropData propData)
	{
		if (propData == null || propData.finish.Count == 0)
		{
			return null;
		}
		return propData.finish[0];
	}

	[CompilerGenerated]
	private static bool tryGetFloorData(PropData propData, out FloorData data)
	{
		data = getFloorData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static FloorData getFloorData(PropData propData)
	{
		if (propData == null || propData.floor.Count == 0)
		{
			return null;
		}
		return propData.floor[0];
	}

	[CompilerGenerated]
	private static bool tryGetFogData(PropData propData, out FogData data)
	{
		data = getFogData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static FogData getFogData(PropData propData)
	{
		if (propData == null || propData.fog.Count == 0)
		{
			return null;
		}
		return propData.fog[0];
	}

	[CompilerGenerated]
	private static bool tryGetItemData(PropData propData, out ItemData data)
	{
		data = getItemData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static ItemData getItemData(PropData propData)
	{
		if (propData == null || propData.item.Count == 0)
		{
			return null;
		}
		return propData.item[0];
	}

	[CompilerGenerated]
	private static bool tryGetItemRespawnerData(PropData propData, out ItemRespawnerData data)
	{
		data = getItemRespawnerData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static ItemRespawnerData getItemRespawnerData(PropData propData)
	{
		if (propData == null || propData.itemRespawner.Count == 0)
		{
			return null;
		}
		return propData.itemRespawner[0];
	}

	[CompilerGenerated]
	private static bool tryGetLadderData(PropData propData, out LadderData data)
	{
		data = getLadderData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static LadderData getLadderData(PropData propData)
	{
		if (propData == null || propData.ladder.Count == 0)
		{
			return null;
		}
		return propData.ladder[0];
	}

	[CompilerGenerated]
	private static bool tryGetEditorLightData(PropData propData, out EditorLightData data)
	{
		data = getEditorLightData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static EditorLightData getEditorLightData(PropData propData)
	{
		if (propData == null || propData.light.Count == 0)
		{
			return null;
		}
		return propData.light[0];
	}

	[CompilerGenerated]
	private static bool tryGetLockData(PropData propData, out LockData data)
	{
		data = getLockData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static LockData getLockData(PropData propData)
	{
		if (propData == null || propData.@lock.Count == 0)
		{
			return null;
		}
		return propData.@lock[0];
	}

	[CompilerGenerated]
	private static bool tryGetLookableData(PropData propData, out LookableData data)
	{
		data = getLookableData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static LookableData getLookableData(PropData propData)
	{
		if (propData == null || propData.lookable.Count == 0)
		{
			return null;
		}
		return propData.lookable[0];
	}

	[CompilerGenerated]
	private static bool tryGetSwapMaterialData(PropData propData, out SwapMaterialData data)
	{
		data = getSwapMaterialData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static SwapMaterialData getSwapMaterialData(PropData propData)
	{
		if (propData == null || propData.materials.Count == 0)
		{
			return null;
		}
		return propData.materials[0];
	}

	[CompilerGenerated]
	private static bool tryGetMaterialSwapData(PropData propData, out MaterialSwapData data)
	{
		data = getMaterialSwapData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static MaterialSwapData getMaterialSwapData(PropData propData)
	{
		if (propData == null || propData.materialSwaps.Count == 0)
		{
			return null;
		}
		return propData.materialSwaps[0];
	}

	[CompilerGenerated]
	private static bool tryGetOceanData(PropData propData, out OceanData data)
	{
		data = getOceanData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static OceanData getOceanData(PropData propData)
	{
		if (propData == null || propData.ocean.Count == 0)
		{
			return null;
		}
		return propData.ocean[0];
	}

	[CompilerGenerated]
	private static bool tryGetOpenLinkData(PropData propData, out OpenLinkData data)
	{
		data = getOpenLinkData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static OpenLinkData getOpenLinkData(PropData propData)
	{
		if (propData == null || propData.openLink.Count == 0)
		{
			return null;
		}
		return propData.openLink[0];
	}

	[CompilerGenerated]
	private static bool tryGetEditorPostProcessingData(PropData propData, out EditorPostProcessingData data)
	{
		data = getEditorPostProcessingData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static EditorPostProcessingData getEditorPostProcessingData(PropData propData)
	{
		if (propData == null || propData.postProcessing.Count == 0)
		{
			return null;
		}
		return propData.postProcessing[0];
	}

	[CompilerGenerated]
	private static bool tryGetPuzzleData(PropData propData, out PuzzleData data)
	{
		data = getPuzzleData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static PuzzleData getPuzzleData(PropData propData)
	{
		if (propData == null || propData.puzzle.Count == 0)
		{
			return null;
		}
		return propData.puzzle[0];
	}

	[CompilerGenerated]
	private static bool tryGetRotatableData(PropData propData, out RotatableData data)
	{
		data = getRotatableData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static RotatableData getRotatableData(PropData propData)
	{
		if (propData == null || propData.rotatable.Count == 0)
		{
			return null;
		}
		return propData.rotatable[0];
	}

	[CompilerGenerated]
	private static bool tryGetRouletteData(PropData propData, out RouletteData data)
	{
		data = getRouletteData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static RouletteData getRouletteData(PropData propData)
	{
		if (propData == null || propData.roulette.Count == 0)
		{
			return null;
		}
		return propData.roulette[0];
	}

	[CompilerGenerated]
	private static bool tryGetScriptComponentData(PropData propData, out ScriptComponentData data)
	{
		data = getScriptComponentData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static ScriptComponentData getScriptComponentData(PropData propData)
	{
		if (propData == null || propData.script.Count == 0)
		{
			return null;
		}
		return propData.script[0];
	}

	[CompilerGenerated]
	private static bool tryGetEditorSetupData(PropData propData, out EditorSetupData data)
	{
		data = getEditorSetupData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static EditorSetupData getEditorSetupData(PropData propData)
	{
		if (propData == null || propData.setup.Count == 0)
		{
			return null;
		}
		return propData.setup[0];
	}

	[CompilerGenerated]
	private static bool tryGetSkyboxData(PropData propData, out SkyboxData data)
	{
		data = getSkyboxData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static SkyboxData getSkyboxData(PropData propData)
	{
		if (propData == null || propData.skybox.Count == 0)
		{
			return null;
		}
		return propData.skybox[0];
	}

	[CompilerGenerated]
	private static bool tryGetSlidableData(PropData propData, out SlidableData data)
	{
		data = getSlidableData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static SlidableData getSlidableData(PropData propData)
	{
		if (propData == null || propData.slidable.Count == 0)
		{
			return null;
		}
		return propData.slidable[0];
	}

	[CompilerGenerated]
	private static bool tryGetSlotData(PropData propData, out SlotData data)
	{
		data = getSlotData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static SlotData getSlotData(PropData propData)
	{
		if (propData == null || propData.slot.Count == 0)
		{
			return null;
		}
		return propData.slot[0];
	}

	[CompilerGenerated]
	private static bool tryGetSoundData(PropData propData, out SoundData data)
	{
		data = getSoundData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static SoundData getSoundData(PropData propData)
	{
		if (propData == null || propData.sound.Count == 0)
		{
			return null;
		}
		return propData.sound[0];
	}

	[CompilerGenerated]
	private static bool tryGetSpawnPointData(PropData propData, out SpawnPointData data)
	{
		data = getSpawnPointData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static SpawnPointData getSpawnPointData(PropData propData)
	{
		if (propData == null || propData.spawnPoint.Count == 0)
		{
			return null;
		}
		return propData.spawnPoint[0];
	}

	[CompilerGenerated]
	private static bool tryGetStairsData(PropData propData, out StairsData data)
	{
		data = getStairsData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static StairsData getStairsData(PropData propData)
	{
		if (propData == null || propData.stairs.Count == 0)
		{
			return null;
		}
		return propData.stairs[0];
	}

	[CompilerGenerated]
	private static bool tryGetSwitch3DData(PropData propData, out Switch3DData data)
	{
		data = getSwitch3DData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static Switch3DData getSwitch3DData(PropData propData)
	{
		if (propData == null || propData.switch3D.Count == 0)
		{
			return null;
		}
		return propData.switch3D[0];
	}

	[CompilerGenerated]
	private static bool tryGetTeleportData(PropData propData, out TeleportData data)
	{
		data = getTeleportData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static TeleportData getTeleportData(PropData propData)
	{
		if (propData == null || propData.teleport.Count == 0)
		{
			return null;
		}
		return propData.teleport[0];
	}

	[CompilerGenerated]
	private static bool tryGetTestData(PropData propData, out TestData data)
	{
		data = getTestData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static TestData getTestData(PropData propData)
	{
		if (propData == null || propData.test.Count == 0)
		{
			return null;
		}
		return propData.test[0];
	}

	[CompilerGenerated]
	private static bool tryGetTextData(PropData propData, out TextData data)
	{
		data = getTextData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static TextData getTextData(PropData propData)
	{
		if (propData == null || propData.text.Count == 0)
		{
			return null;
		}
		return propData.text[0];
	}

	[CompilerGenerated]
	private static bool tryGetTokenData(PropData propData, out TokenData data)
	{
		data = getTokenData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static TokenData getTokenData(PropData propData)
	{
		if (propData == null || propData.token.Count == 0)
		{
			return null;
		}
		return propData.token[0];
	}

	[CompilerGenerated]
	private static bool tryGetTriggerData(PropData propData, out TriggerData data)
	{
		data = getTriggerData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static TriggerData getTriggerData(PropData propData)
	{
		if (propData == null || propData.trigger.Count == 0)
		{
			return null;
		}
		return propData.trigger[0];
	}

	[CompilerGenerated]
	private static bool tryGetTurnableData(PropData propData, out TurnableData data)
	{
		data = getTurnableData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static TurnableData getTurnableData(PropData propData)
	{
		if (propData == null || propData.turnable.Count == 0)
		{
			return null;
		}
		return propData.turnable[0];
	}

	[CompilerGenerated]
	private static bool tryGetTweenStateDataRoomEditor(PropData propData, out TweenStateDataRoomEditor data)
	{
		data = getTweenStateDataRoomEditor(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static TweenStateDataRoomEditor getTweenStateDataRoomEditor(PropData propData)
	{
		if (propData == null || propData.tweenState.Count == 0)
		{
			return null;
		}
		return propData.tweenState[0];
	}

	[CompilerGenerated]
	private static bool tryGetWallData(PropData propData, out WallData data)
	{
		data = getWallData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static WallData getWallData(PropData propData)
	{
		if (propData == null || propData.wall.Count == 0)
		{
			return null;
		}
		return propData.wall[0];
	}

	[CompilerGenerated]
	private static bool tryGetWaterData(PropData propData, out WaterData data)
	{
		data = getWaterData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static WaterData getWaterData(PropData propData)
	{
		if (propData == null || propData.water.Count == 0)
		{
			return null;
		}
		return propData.water[0];
	}

	[CompilerGenerated]
	private static bool tryGetZoomableData(PropData propData, out ZoomableData data)
	{
		data = getZoomableData(propData);
		return data != null;
	}

	[CompilerGenerated]
	private static ZoomableData getZoomableData(PropData propData)
	{
		if (propData == null || propData.zoomable.Count == 0)
		{
			return null;
		}
		return propData.zoomable[0];
	}
}
