using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FMOD;
using FMODUnity;
using Oculus.Platform;
using Oculus.Platform.Models;
using Steamworks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.XR;
using WebSocketSharp;

public class Menu : MonoBehaviour
{
	public enum AwardType
	{
		None = 0,
		DraculaDarkest = 1,
		PirateDarkest = 2,
		SpaceDarkest = 3
	}

	[Serializable]
	public class AwardData
	{
		public AwardType type;

		public string titlekey;

		public string descriptionKey;

		public Sprite sprite;
	}

	private class MenuOptionsControllerData
	{
		public SelectableOption lastFrameSelected;

		public float controllerAxisMoveTo;

		public float axisMoveCooldown;

		public float moveSliderAcc = 1f;
	}

	[Serializable]
	public class AocItem
	{
		public ulong ns_uid;

		public string name;

		public string description;

		public string content_type;

		public string platform;
	}

	[Serializable]
	public class AocItemsResponse
	{
		public string lang;

		public int length;

		public int offset;

		public int total;

		public List<AocItem> aoc_items;
	}

	public enum LoadingReason
	{
		PreparingLobby = 0,
		JoiningRoom = 1,
		FindingRandomMatch = 2,
		ConnectingNSA = 3,
		ClientLoadingSave = 4
	}

	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct UserAttributes
	{
	}

	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct AppAttributes
	{
	}

	public enum NetworkingProtocol
	{
		SteamDefault = 0,
		SteamLegacy = 1,
		Crossplatform = 2
	}

	public class WorkshopRoomInfo
	{
		public ulong roomId;

		public string pineServerModified;

		public string roomTitle;

		public string roomDescription;

		public List<string> roomTags;

		public List<string> roomWalkthrough;

		public string roomLocalPath;

		public Texture2D roomPreviewImage;

		public string roomPreviewImageUrl;

		public float roomScore;

		public VoteState voteState;

		public ulong authorId;

		public string authorName;

		public bool helperRoomInfoPopulated;

		public string tutorialId;

		public PlayerSave.FinishState finishState;

		public bool isInYourRooms;

		public ulong collectionId;

		public int orderingNumber;

		public int fileSize;

		public bool isCurrentlyUnsubscribing;

		public List<MenuLevelUI> levelTiles = new List<MenuLevelUI>();
	}

	public enum VoteState
	{
		DidntVote = 0,
		UpVote = 1,
		DownVote = 2
	}

	private class JoinLobbyData
	{
		public string lobby;

		public ulong steamLobby;

		public float cooldownTimeToJoinLobby = -1f;
	}

	private class MenuCallbacks : IMenuNetCallbacks
	{
		private Menu menu;

		public MenuCallbacks(Menu menu)
		{
			this.menu = menu;
		}

		public void lobbyReady()
		{
			menu.lobbyReady();
		}

		public void onEnterLobby()
		{
			menu.onEnterLobby();
		}

		public void onCantEnterDetailsLobby(string code, string message)
		{
			menu.onCantEnterDetailsLobby(code, message);
		}

		public void onCantEnterLobby()
		{
			menu.onCantEnterLobby();
		}

		public void onFailedToFindLobby(string message)
		{
			menu.onFailedToFindLobby(message);
		}

		public void onLobbyCodeChanged(string lobbyCode)
		{
			menu.onLobbyCodeChanged(lobbyCode);
		}

		public void onGotLobbyInvitation(string lobbyId)
		{
			menu.onGotLobbyInvitation(lobbyId);
		}

		public void onLobbyMemberDataChanged()
		{
			menu.onLobbyMemberDataChanged();
		}

		public void onLobbyPacket(Packet packet)
		{
			menu.onLobbyPacket(packet);
		}

		public void onMatchCreateFail(string code, string message)
		{
			menu.onMatchCreateFail(code, message);
		}

		public void onCantEnterSteamLobbyFromCrossplatform()
		{
			menu.onCantEnterSteamLobbyFromCrossplatform();
		}

		public void onPlayerSynced(NetPlayerData player)
		{
			menu.onPlayerSynced(player);
		}
	}

	public class WorkshopCollection
	{
		public string collectionId;

		public string collectionName;

		public DateTime dateAdded = DateTime.Now;

		public List<WorkshopRoomInfo> rooms = new List<WorkshopRoomInfo>();

		public List<PublishedFileId_t> roomIds = new List<PublishedFileId_t>();

		public List<Toggle> headers = new List<Toggle>();
	}

	[Serializable]
	public class MainMenuCollection
	{
		public List<MenuCollection> collections = new List<MenuCollection>();
	}

	[Serializable]
	public class MenuCollection
	{
		public string id;

		public string name;

		public List<ulong> rooms = new List<ulong>();
	}

	public class CCCategory
	{
		public CCOptionsScriptable.CategoryData data;

		public CCCategoryTemplateUI ui;

		public List<CCVariant> variants = new List<CCVariant>();

		public CCCategory(CCOptionsScriptable.CategoryData data, CCCategoryTemplateUI ui)
		{
			this.data = data;
			this.ui = ui;
		}
	}

	public class CCVariant
	{
		public CCCategory category;

		public CCOptionsScriptable.VariantData data;

		public CCVariantButton button;

		public CCVariant(CCCategory category, CCOptionsScriptable.VariantData data, CCVariantButton button)
		{
			this.category = category;
			this.data = data;
			this.button = button;
		}
	}

	public class WorkshopSearchRoomInfo
	{
		public PineRoomData downloadedInfo;

		public MenuLevelUI workshopScreenMenuLevelUI;

		public MenuLevelUI communityRoomsTabMenuLevelUI;

		public MenuLevelUI communityCoopTabMenuLevelUI;

		public WorkshopSearchLevelState levelState;

		public UnityWebRequest webRequest;

		public Texture2D cachedLevelTexture;

		public int cachedForCount;

		public Texture2D getLevelImageTexture()
		{
			return cachedLevelTexture;
		}

		public string getFileSize(bool useFullSize)
		{
			if (!useFullSize)
			{
				return downloadedInfo.FileSizeBytesHalf;
			}
			return downloadedInfo.FileSizeBytesFull;
		}
	}

	public enum WorkshopSearchLevelState
	{
		NotInstalled = 0,
		FetchingDownloadLink = 1,
		Installing = 2,
		Installed = 3
	}

	public class PineWorkshopRequest
	{
		public UnityWebRequest request;

		public Action<string> onDone;
	}

	[Serializable]
	private class PineWorkshopList
	{
		public long Place;

		public string Name;

		public string GUID;
	}

	[Serializable]
	public class PineRoomsData
	{
		public string ResultCount;

		public string ResultsPerPage;

		public string PageCount;

		public string Page;

		public List<PineRoomData> Rooms = new List<PineRoomData>();

		public string Debug;
	}

	[Serializable]
	public class PineRoomData
	{
		public string SteamID;

		public string Modified;

		public string Name;

		public string Creator;

		public string ImageURL;

		public string FileSizeBytesFull;

		public string FileSizeBytesHalf;

		public string Tags;

		public string Description;

		public string CreatedAt;

		public string id => SteamID + "_" + Modified;
	}

	public GameObject InviteHintController;

	public GameObject InviteHintKeyboard;

	public List<AwardData> awards = new List<AwardData>();

	public VisualControlUI visualControlUI;

	private List<string> tweenEvents = new List<string>();

	private CustomMode escCustomMode;

	private bool canStartMenu;

	private float timePressedHost = -1f;

	private float timeCodeBlink = -1f;

	private const float TimeCodeBlinkEnd = 1f;

	private const float TimeCodeBlinkDelay = 0.25f;

	private float timeLobbyCopied;

	private const float TimeLobbyCopiedEnd = 5f;

	private MenuOptionsControllerData initialSetupControllerData = new MenuOptionsControllerData();

	private List<SelectableOption> initialSetupOptions = new List<SelectableOption>();

	private VisualController initialSetupVisualController = new VisualController();

	private InputDispatcher menuInputDispatcher;

	private PineUILayoutRebuilder layoutRebuilder = new PineUILayoutRebuilder();

	[NonSerialized]
	public bool inCoverScreen = true;

	public static bool enteredMenuForFirstTime;

	public static LoadRoomResult showRoomEditorOpenError;

	public static bool shouldEnterPoseInInit = true;

	[NonSerialized]
	public Game game;

	[NonSerialized]
	public CharacterPose initialPose;

	[NonSerialized]
	public Zoomable roomsZoomable;

	[NonSerialized]
	public Zoomable darkestPuzzlesZoomable;

	private TweenStateContext tweenStateContextMenu;

	public TextAsset richPresenceText;

	public VideoPlayer bgVideo;

	public Image bgImage;

	public MenuUI menuUI;

	public StudioEventEmitter menuMusic;

	private bool isGameLoadedProperly = true;

	public LevelPicker lpRooms;

	public LevelPicker lpDarkest;

	private string loadLevelId = string.Empty;

	private LevelProvider levelProvider;

	private const string INSTALLED_LEVELS_LIST_ID_PINE = "installedPine";

	private const string INSTALLED_LEVELS_LIST_ID_STEAM = "installedSteam";

	private const string FINISHED_LEVELS_LIST_ID = "finished";

	private const string ADVANCED_SEARCH_ID = "advancedSearch";

	private const string PINE_WORKSHOP_ID_SWITCH = "switch-browse";

	private const string PINE_WORKSHOP_ID_OCULUS = "dc96de3e-56d5-4c2b-934f-636edcfc3ae7";

	private const string PINE_WORKSHOP_ID_STEAM = "dc96de3e-56d5-4c2b-934f-636edcfc3ae7";

	private const string PINE_WORKSHOP_ID_PS = "switch-browse";

	private const string PINE_WORKSHOP_ID_XBOX = "switch-browse";

	private const string PINE_WORKSHOP_ID_MOBILE = "switch-browse";

	private List<string> pineWorkshopAllIds = new List<string> { "switch-browse", "dc96de3e-56d5-4c2b-934f-636edcfc3ae7", "dc96de3e-56d5-4c2b-934f-636edcfc3ae7", "switch-browse", "switch-browse", "switch-browse" };

	private Dictionary<string, HashSet<string>> pineWorkshopSteamIdsOfOtherPlatforms = new Dictionary<string, HashSet<string>>();

	private HashSet<string> pineWorkshopSteamIdsOfMyPlatforms = new HashSet<string>();

	private readonly List<DLC> dlcToHideIfNotInstalled = new List<DLC>();

	private PineTweenSystemEnableNoHandles tweener = new PineTweenSystemEnableNoHandles();

	private CustomInput customInput;

	private List<LoadingReason> loadingReasons = new List<LoadingReason>();

	private string waitForInstallToLoadLevelId;

	private Credits currentOpenCredits;

	private Callback<GameOverlayActivated_t> steamOverlayToggle;

	[Header("Player Platform Icons")]
	public Sprite oculusPlayerIcon;

	public Sprite vrPlayerIcon;

	public Sprite switchPlayerIcon;

	public Sprite steamPlayerIcon;

	[Header("News")]
	private long dateTillCountdown = 1733508000L;

	private const int SECRET_POPUP_CURRENT_ID = 3;

	private GameObject lastSelectedObjectBeforeSecretPopup;

	private Transform lastSelectedGroupObjectBeforeSecretPopup;

	public static int framesWithGame;

	private static AocItemsResponse aocShopItems;

	private bool isLoadingLevel;

	public const int DLC_COUNT = 0;

	private bool[] dlcInstalled = new bool[0];

	private static List<DLC> allDlcsCache;

	private string newsLink;

	private string newsAltLink;

	private Dictionary<string, string> newsTexts = new Dictionary<string, string>();

	public const string HIDDEN_CODE = "*****";

	private int lastFrameNumOfPlayers;

	private List<IDisposable> gcDontKill = new List<IDisposable>();

	private JoinLobbyData joinLobbyData;

	private bool inCrossplatformLobbyLastTime;

	private List<string> hostsRoomsWithSave;

	private List<CSteamID> kickedFromLobbyIds = new List<CSteamID>();

	private List<WorkshopCollection> workshopCollections = new List<WorkshopCollection>();

	private Text numOfPlayersText;

	private MenuOptionsControllerData searchControllerData = new MenuOptionsControllerData();

	private List<SelectableOption> advancedSearchOptions = new List<SelectableOption>();

	private Tags advancedSearchFilters = new Tags();

	private int advancedSearchSortSelected;

	private List<EUGCQuery> advancedSearchSortings = new List<EUGCQuery>
	{
		EUGCQuery.k_EUGCQuery_RankedByTotalPlaytime,
		EUGCQuery.k_EUGCQuery_RankedByVotesUp,
		EUGCQuery.k_EUGCQuery_RankedByTrend
	};

	private int advancedSearchDaysSelected = 2;

	private List<uint> advancedSearchDays = new List<uint> { 7u, 30u, 180u, 365u, 0u };

	public Transform ccSpawnPoint;

	public CustomMode ccCustomMode;

	public CharacterModels ccPlayer;

	public RuntimeAnimatorController[] ccAnimationControllers;

	public CCOptionsScriptable ccOptions;

	public List<CCCategory> allCategories = new List<CCCategory>();

	public List<CCVariant> allVariants = new List<CCVariant>();

	private GameObject controllerLastSelectedCCUI;

	private CCGroupTemplateUI controllerSelectedCCGroup;

	private float rotationTarget;

	private float rotationVelocity;

	private float rotationCurrent;

	private bool joystickPointerDoTeleport;

	private Vector3[] worldCornersCache = new Vector3[4];

	public static readonly List<DLC> switchAvaliableDLCs = new List<DLC>
	{
		DLC.Dieselpunk,
		DLC.Western,
		DLC.Magic,
		DLC.Mayan
	};

	private const string EscapeSimulatorEndpoint = "https://ugc.escapesimulator.com";

	private const string AuthMeTestToken = "FAIL_ME!!!";

	public static UnpackedCustomRoom downloadedRoom;

	public Dictionary<string, WorkshopSearchRoomInfo> installedWorkshopSearchRooms = new Dictionary<string, WorkshopSearchRoomInfo>();

	private List<PineWorkshopRequest> pineWorkshopWebRequests = new List<PineWorkshopRequest>();

	private string levelIdInDetailsView;

	private Callback<GetTicketForWebApiResponse_t> ticketForWebApiResponse;

	private static string webApiTicket;

	private static bool waitForWorkshopTokenThenCallQueryPineWorkshop;

	[Header("VR - Setup")]
	public VR.Type vrType;

	public GameObject vrPlane;

	public Canvas vrCanvasTemplate;

	public GameObject vrMenuLobby;

	public Transform vrMenuSpawnPoint;

	public Button vrKeyboardDeleteButton;

	public Button vrKeyboardBackButton;

	public Renderer vrPlatno;

	public Transform vrElevator;

	public Transform characterPivot;

	public Transform vrElevatorBot;

	public Transform vrElevatorTop;

	private float vrElevatorCurrent;

	public float vrElevatorCurrentGoal;

	public GameObject vrChristmasDecorations;

	public Animation vrCurtains;

	private float vrCurtainsCurrent;

	private float vrCurtainsDelay;

	public float vrCurtainsCurrentGoal;

	private float vrMenuButtonPressDuration;

	private int vrOculusBrowsePackConsecutiveClickCount;

	public float vrSimulatorSpawnOffset = 2f;

	public VR vr;

	[NonSerialized]
	public VRInputModule vrInputModule;

	private bool vrInitialLookDirectionInited;

	private VRHeadsetInterruptHandler vrHeadsetInterruptHandler;

	private Net session => Net.getSession();

	private bool inCrossplatformLobby
	{
		get
		{
			if (session != null && session.connectionMode == ConnectionMode.Photon)
			{
				return Is.Steam;
			}
			return false;
		}
	}

	private void initES2()
	{
		if (shouldEnterPoseInInit)
		{
			game.handlePoseEnterLocal(initialPose, transitionIn: false);
			shouldEnterPoseInInit = false;
		}
		else
		{
			transitionToLobby();
		}
		escCustomMode = new GameObject().AddComponent<CustomMode>();
		escCustomMode.allowExit = (escCustomMode.allowLook = (escCustomMode.allowMove = false));
		escCustomMode.allowCursor = true;
		doUnfade(delegate
		{
			canStartMenu = true;
		});
		if (!PlayerSave.getSettings().didInitialSetup)
		{
			menuUI.InitialSetup.gameObject.SetActive(value: true);
			menuUI.InitialSetup.Options.gameObject.SetActive(value: true);
			menuUI.InitialSetup.Tutorial.gameObject.SetActive(value: false);
		}
		initialSetupVisualController.setup(null, null, onFrameClick);
		menuUI.InitialSetup.Options_Layout_Content_BrightnessImage.material.SetFloat("_BrightnessMultiplier", Maths.Exp2(PlayerSave.getSettings().hdrpPostExposure));
		OptionsPrefabs optionsPrefabs = OptionsPrefabs.get();
		SelectableOption slider = optionsPrefabs.getSlider("%RoomEditor_Brightness%", menuUI.InitialSetup.Options_Layout_Content_B_L, PlayerSave.getSettings().hdrpPostExposure, -2f, 2f, delegate(float value)
		{
			PlayerSave.getSettings().hdrpPostExposure = value;
			menuUI.InitialSetup.Options_Layout_Content_BrightnessImage.material.SetFloat("_BrightnessMultiplier", Maths.Exp2(PlayerSave.getSettings().hdrpPostExposure));
			game.syncPostExposure();
			syncOptions(game.menuOptions);
		}, 0.01f);
		optionsPrefabs.addHoverable(slider.baseObject, delegate
		{
			menuUI.InitialSetup.Options_Layout_Content_B_R_Message.text = Localization.translate("%BrightnessMessage%");
		}, delegate
		{
			menuUI.InitialSetup.Options_Layout_Content_B_R_Message.text = "";
		});
		initialSetupOptions.Add(slider);
		SelectableOption toggle = optionsPrefabs.getToggle("%MotionBlur%", menuUI.InitialSetup.Options_Layout_Content_B_L, PlayerSave.getSettings().hdrpMotionBlur, delegate(bool value)
		{
			PlayerSave.getSettings().hdrpMotionBlur = value;
		});
		optionsPrefabs.addHoverable(toggle.baseObject, delegate
		{
			menuUI.InitialSetup.Options_Layout_Content_B_R_Message.text = Localization.translate("%MotionBlurMessage%");
		}, delegate
		{
			menuUI.InitialSetup.Options_Layout_Content_B_R_Message.text = "";
		});
		initialSetupOptions.Add(toggle);
		SelectableOption toggle2 = optionsPrefabs.getToggle("%ShowTimer%", menuUI.InitialSetup.Options_Layout_Content_B_L, PlayerSave.getSettings().showTimer, delegate(bool value)
		{
			PlayerSave.getSettings().showTimer = value;
		});
		optionsPrefabs.addHoverable(toggle2.baseObject, delegate
		{
			menuUI.InitialSetup.Options_Layout_Content_B_R_Message.text = Localization.translate("%TimerMessage%");
		}, delegate
		{
			menuUI.InitialSetup.Options_Layout_Content_B_R_Message.text = "";
		});
		initialSetupOptions.Add(toggle2);
		Controller.setupVerticalNavigation(initialSetupOptions);
		Transform parent = menuUI.InitialSetup.Options_Layout_Content_ButtonContainer.transform;
		VisualControl visualControl = new VisualControl("closeInitialOptions", ControllerButtonActionType.UIExtra1, "%continue%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
		visualControl.addKeybindAction(KeyBindingAction.Chat, game.menuOptions);
		initialSetupVisualController.addOrUpdateControl(visualControl, visualControlUI, parent);
		VisualControl visualControl2 = new VisualControl("welcomePlay", ControllerButtonActionType.UIConfirmPrimary, "%play%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
		visualControl2.addKeybindAction(KeyBindingAction.Chat, game.menuOptions);
		menuUI.InitialWelcomeMessage.ES2Frame.addOrUpdateControl(visualControl2);
		menuUI.InitialWelcomeMessage.ES2Frame.setup(menuInputDispatcher, null, null, onFrameClick);
		if (Is.Gamescom)
		{
			menuUI.ES2Cover.MainButtons_ButtonDiscord.gameObject.SetActive(value: false);
		}
		static void syncOptions(MenuOptions options)
		{
			Slider componentInChildren = options.menuOptionBrightness.GetComponentInChildren<Slider>(includeInactive: true);
			if (componentInChildren != null)
			{
				componentInChildren.value = PlayerSave.getSettings().hdrpPostExposure;
			}
			InputField componentInChildren2 = options.menuOptionBrightness.GetComponentInChildren<InputField>(includeInactive: true);
			if (componentInChildren2 != null)
			{
				componentInChildren2.text = componentInChildren.value.ToString("0.00");
			}
		}
	}

	private void transitionToLobby()
	{
		inCoverScreen = false;
		menuUI.ES2Cover.gameObject.SetActive(value: false);
		menuUI.ClientCode.gameObject.SetActive(value: false);
		menuUI.ES2Cover.gameObject.SetActive(value: false);
		game.handlePoseLeaveLocal(initialPose);
		menuUI.ES2Cover.PlayButtons.gameObject.SetActive(value: false);
		menuUI.ES2Cover.MainButtons.gameObject.SetActive(value: true);
		menuUI.ES2Cover.MainButtons_ButtonPlay.gameObject.SetActive(value: false);
	}

	private void updateES2Menu()
	{
		TweenState[] tweenStates = tweenStateContextMenu.tweenStates;
		for (int i = 0; i < tweenStates.Length; i++)
		{
			tweenStates[i].update(Time.deltaTime, tweenEvents);
			tweenEvents.Clear();
		}
		updateLobbyCode();
		handleES2UI();
		handleInitialSetup();
		handleTagPicker();
		handleMiniHostPanel();
		handleLobbyMessages();
		void handleES2UI()
		{
			bool activeInHierarchy = menuUI.Rooms.gameObject.activeInHierarchy;
			menuUI.Rooms.gameObject.SetActive(game.isInTopZoom(roomsZoomable.gameObject));
			bool activeInHierarchy2 = menuUI.Rooms.gameObject.activeInHierarchy;
			if (activeInHierarchy != activeInHierarchy2 && activeInHierarchy2 && Controller.isActive())
			{
				lpRooms.updateController(forceChange: true);
			}
			bool activeInHierarchy3 = menuUI.Darkest.gameObject.activeInHierarchy;
			menuUI.Darkest.gameObject.SetActive(game.isInTopZoom(darkestPuzzlesZoomable.gameObject));
			if (activeInHierarchy3 != menuUI.Darkest.gameObject.activeInHierarchy && Controller.isActive())
			{
				lpDarkest.updateController(forceChange: true);
			}
			bool activeInHierarchy4 = menuUI.Customization.gameObject.activeInHierarchy;
			menuUI.Customization.gameObject.SetActive(game.localPlayerData.customMode == ccCustomMode);
			if (activeInHierarchy4 != menuUI.Customization.gameObject.activeInHierarchy && Controller.isActive())
			{
				updateCCController(forceUpdate: true);
			}
			if (Controller.getButtonDown(ControllerButtonActionType.GameESC) || Input.GetKeyDown(KeyCode.Escape))
			{
				if (game.gameState.current == Game.GameState.Room)
				{
					game.openMainMenu();
				}
				else if (game.gameState.current == Game.GameState.Menu && !game.isRebindingEnterKeyOpen)
				{
					game.closeMainMenu();
				}
			}
		}
		void handleInitialSetup()
		{
			if (menuUI.InitialSetup.gameObject.activeInHierarchy)
			{
				initialSetupVisualController.update(menuInputDispatcher);
				MenuOptions.updateController(initialSetupOptions, null, null, delegate
				{
				}, ref initialSetupControllerData.lastFrameSelected, ref initialSetupControllerData.controllerAxisMoveTo, ref initialSetupControllerData.axisMoveCooldown, ref initialSetupControllerData.moveSliderAcc);
			}
		}
		void handleLobbyMessages()
		{
			if (game.gameState.current == Game.GameState.Room && game.gameState.time > 0.5f)
			{
				if (!PlayerSave.getProgress().welcomeMessageShown)
				{
					PlayerSave.getProgress().welcomeMessageShown = true;
					PlayerSave.flush();
					menuUI.InitialWelcomeMessage.Fader.alpha = 0f;
					menuUI.InitialWelcomeMessage.gameObject.SetActive(value: true);
					PineTweenSystemEnableNoHandles pineTweenSystemEnableNoHandles = tweener;
					GameObject obj = menuUI.InitialWelcomeMessage.Fader.gameObject;
					float? alphaTo = 1f;
					pineTweenSystemEnableNoHandles.tween(obj, 0.25f, 0f, deactivate: false, activate: false, destroy: false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, alphaTo);
					game.gameState.changeState(Game.GameState.Menu);
				}
				if (PlayerSave.getProgress().awardToShowInLobby != AwardType.None)
				{
					AwardData awardData = awards.Find((AwardData x) => x.type == PlayerSave.getProgress().awardToShowInLobby);
					game.optionDialog.showAsAward(awardData.titlekey, awardData.descriptionKey, awardData.sprite);
					PlayerSave.getProgress().awardToShowInLobby = AwardType.None;
					PlayerSave.flush();
				}
			}
		}
		void handleMiniHostPanel()
		{
			InviteHintController.SetActive(Controller.isActive());
			InviteHintKeyboard.SetActive(!Controller.isActive());
			if (menuUI.HostMiniPanel.Panel_BG_Spinner_Spinner.transform.parent.gameObject.activeInHierarchy && Time.time - timePressedHost >= 2f && !session.lobbyCode.IsNullOrEmpty())
			{
				menuUI.HostMiniPanel.Panel_BG_Spinner_Spinner.transform.parent.gameObject.SetActive(value: false);
				menuUI.HostMiniPanel.Panel_BG_Code.gameObject.SetActive(value: true);
				menuUI.HostMiniPanel.Panel_BG_Code.GetComponent<TweenState>()?.transitionTo("Down");
				menuUI.HostMiniPanel.LobbyCodeCopied.gameObject.SetActive(value: true);
				timeLobbyCopied = Time.time;
				game.copyCode();
				timeCodeBlink = Time.time + 0.25f;
			}
			if (timeCodeBlink > 0f && Time.time - timeCodeBlink >= 0.25f)
			{
				float num = Time.time - timeCodeBlink - 0.25f;
				float num2 = 0.5f * Mathf.Cos(2f * num * MathF.PI) + 0.5f;
				menuUI.HostMiniPanel.Panel.color = Color.Lerp(new Color(0f, 0f, 0f, num2), new Color(1f, 1f, 1f, 1f), num2);
				if (num >= 1f)
				{
					timeCodeBlink = -1f;
					menuUI.HostMiniPanel.InviteFriends.gameObject.SetActive(value: true);
				}
			}
			if (menuUI.HostMiniPanel.LobbyCodeCopied.gameObject.activeSelf && Time.time - timeLobbyCopied >= 5f)
			{
				menuUI.HostMiniPanel.LobbyCodeCopied.gameObject.SetActive(value: false);
			}
		}
		void handleTagPicker()
		{
			if (menuUI.TagPicker.gameObject.activeInHierarchy)
			{
				MenuOptions.updateController(advancedSearchOptions, menuUI.TagPicker.SafeAreas_Modal_ScrollView, null, delegate
				{
				}, ref searchControllerData.lastFrameSelected, ref searchControllerData.controllerAxisMoveTo, ref searchControllerData.axisMoveCooldown, ref searchControllerData.moveSliderAcc);
			}
		}
		void updateLobbyCode()
		{
			if (session != null)
			{
				Text panel_BG_Code = menuUI.HostMiniPanel.Panel_BG_Code;
				string text = ((!PlayerSave.getSettings().isStreamerMode || Controller.isActive()) ? session.lobbyCode : "*****");
				if (panel_BG_Code.text != text)
				{
					UnityEngine.Debug.Log("Updating lobby code: " + text);
					panel_BG_Code.text = text;
					Text component = panel_BG_Code.GetComponent<Text>();
					if (component != null)
					{
						Game.rebuildTexts(component.transform);
					}
				}
			}
		}
	}

	public static Language getPreferredLanguage()
	{
		return SteamApps.GetCurrentGameLanguage() switch
		{
			"japanese" => Language.Japanese, 
			"french" => Language.French, 
			"german" => Language.German, 
			"italian" => Language.Italian, 
			"spanish" => Language.Spanish, 
			"koreana" => Language.Korean, 
			"portuguese" => Language.Portuguese, 
			"latam" => Language.SpanishLatinAmerica, 
			"schinese" => Language.ChineseSimplified, 
			"tchinese" => Language.ChineseTraditional, 
			"brazilian" => Language.PortugueseBrasil, 
			_ => Language.English, 
		};
	}

	private void onSteamOverlayToggle(GameOverlayActivated_t param)
	{
		PlayerSave.setAllVolumes(param.m_bActive == 1);
	}

	public void init(Game game, CharacterPose initialPose, Zoomable roomsZoomable, Zoomable darkestPuzzlesZoomable)
	{
		UnityEngine.Debug.Log("SteamManager.Initialized in Menu: " + SteamManager.Initialized);
		UnityEngine.Debug.Log("Starting Escape Simulator2 v" + UnityEngine.Application.version);
		this.game = game;
		this.initialPose = initialPose;
		this.roomsZoomable = roomsZoomable;
		this.darkestPuzzlesZoomable = darkestPuzzlesZoomable;
		game.lobbyZoomablesWithFrame.Add(roomsZoomable);
		game.lobbyZoomablesWithFrame.Add(darkestPuzzlesZoomable);
		menuInputDispatcher = new InputDispatcher(game.menuOptions, () => InputDispatcher.AlwaysTrueContext.True);
		framesWithGame = 0;
		AssetBundleLoader.loadAssetBundle(AssetBundleType.Dependencies);
		PlayerSave.init(getPreferredLanguage());
		Localization.init(PlayerSave.getSettings().language);
		initVR();
		initAudio();
		initSettings();
		initUI();
		initPlatformSpecifics();
		initCustomization();
		menuUI.ClientCode.ControllerKeyboard.initKeyboard(tweener);
		ESUGC.init();
		ESUGC.standard.init(onUGCChanged);
		ESUGC.pineOverride.init(onUGCChanged);
		menuUI.RoomInfoPopup.init(ESUGC.pineOverride);
		if (!enteredMenuForFirstTime)
		{
			UnityUtils.resetCrashHandlers();
		}
		fillHasDlcInstalled();
		initES2();
		setupLevelPickers();
		initWorkshop();
		refreshAllInstalledRooms();
		setupLobby();
		initVROculusForwardOrientationReset();
		setupNews();
		setupRemoteConfig();
		setupVersion();
		setupErrorsIfNeeded();
		updateController(forceChange: true);
		onChangeLanguage();
		applyCurrentTheme(fromInit: true);
		tweenStateContextMenu = TweenState.createContext(menuUI.gameObject);
		UnityUtils.setCrashHandler(SceneManager.GetActiveScene().name, session);
		if (Is.Oculus)
		{
			PlayerSave.setAllVolumes();
			PineFmod.flushCommands();
		}
		enteredMenuForFirstTime = true;
		void initPlatformSpecifics()
		{
			if (!SteamManager.Initialized && Is.Steam)
			{
				Controller.init();
				VisualControl visualControl = new VisualControl("quit", ControllerButtonActionType.UIConfirmPrimary, "%quit%", VisualControl.ControlType.Button, VisualControl.Position.BotCenter);
				visualControl.addKeybindAction(KeyBindingAction.Chat, game.menuOptions);
				visualControl.addEscape(hidden: true);
				menuUI.OptionDialog.show("errorStartingSteam", "errorStartingSteamDescription", onOptionDialogClick, visualControl);
				isGameLoadedProperly = false;
				UnityEngine.Debug.Log("ERROR: Connection to Steam failed");
			}
			else
			{
				RichPresence.init(richPresenceText.text);
				SteamNetworkingUtils.InitRelayNetworkAccess();
				steamOverlayToggle = new Callback<GameOverlayActivated_t>(onSteamOverlayToggle);
				ticketForWebApiResponse = new Callback<GetTicketForWebApiResponse_t>(onSteamAuthed);
				SteamUser.GetAuthTicketForWebApi(null);
				Controller.init();
			}
		}
		void initSettings()
		{
			if (Is.Oculus)
			{
				OVRPlugin.foveatedRenderingLevel = OVRPlugin.FoveatedRenderingLevel.HighTop;
				OVRPlugin.useDynamicFoveatedRendering = true;
				OVRPlugin.systemDisplayFrequency = 72f;
				XRSettings.eyeTextureResolutionScale = 1.25f;
				UnityEngine.Application.targetFrameRate = 72;
			}
			if (vr.isActive())
			{
				vr.vrPostWeightGoal = 1f;
				UnityEngine.Application.targetFrameRate = 144;
			}
			else
			{
				PlayerSave.setFramerate();
			}
			QualitySettings.globalTextureMipmapLimit = (int)PlayerSave.getSettings().playerTextureLevel;
			if (PlayerSave.getSettings().screenResolutionWidth <= 0 || PlayerSave.getSettings().screenResolutionHeight <= 0)
			{
				Resolution newResolution = Screen.resolutions[Screen.resolutions.Length - 1];
				for (int num = Screen.resolutions.Length - 1; num >= 0; num--)
				{
					Resolution resolution = Screen.resolutions[num];
					if (resolution.width > resolution.height)
					{
						newResolution = resolution;
						break;
					}
				}
				MenuOptions.setNewResolution(newResolution);
				List<DisplayInfo> list = new List<DisplayInfo>();
				Screen.GetDisplayLayout(list);
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].Equals(Screen.mainWindowDisplayInfo))
					{
						PlayerSave.getSettings().activeMonitor = i;
					}
				}
			}
			UnityEngine.Debug.Log("Setting resolution: " + PlayerSave.getSettings().screenResolutionWidth + " x " + PlayerSave.getSettings().screenResolutionHeight + " , Fullscreen Mode: " + PlayerSave.getSettings().fullScreenMode.ToString() + " Refresh rate: " + PlayerSave.getSettings().screenRefreshRate);
			Screen.SetResolution(PlayerSave.getSettings().screenResolutionWidth, PlayerSave.getSettings().screenResolutionHeight, PlayerSave.getSettings().fullScreenMode, PlayerSave.getSettings().screenRefreshRate);
		}
		void initUI()
		{
			PineUI.addButtonClickDelegate(onButtonClick);
			menuUI.ES2Cover.gameObject.SetActive(value: true);
			InputField inputField = menuUI.ClientCode.InputField;
			inputField.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(inputField.onValidateInput, (InputField.OnValidateInput)((string s, int i, char c) => char.ToUpper(c)));
			menuUI.Rooms_LevelPicker.Frame.setup(menuInputDispatcher, null, null, onFrameClick);
			menuUI.Darkest_LevelPicker.Frame.setup(menuInputDispatcher, null, null, onFrameClick);
			menuUI.Customization.Frame.setup(menuInputDispatcher, null, null, onFrameClick);
			menuUI.ClientCode.Frame.setup(menuInputDispatcher, null, null, onFrameClick);
		}
		async Task initUnityRemoteConfig()
		{
			await UnityServices.InitializeAsync();
			if (!AuthenticationService.Instance.IsSignedIn)
			{
				await AuthenticationService.Instance.SignInAnonymouslyAsync();
			}
			RemoteConfigService.Instance.FetchCompleted += applyRemoteSettings;
			RemoteConfigService.Instance.FetchConfigs(default(UserAttributes), default(AppAttributes));
		}
		void setupDarkestLevelPicker(LevelPicker lp)
		{
			lp.onClick = delegate
			{
				onPickerClick(lp);
			};
			lp.onDoubleClick = onPickerDoubleClick;
			lp.onBrowserClick = onBrowseClick;
			lp.setTabless();
			lp.startTab("originalDarkest", "%Original%");
			foreach (DarkestPuzzles item in P_1.darkestPuzzlesToDisplay)
			{
				string text = item.ToString();
				string text2 = item.ToString().ToLower();
				lp.definePack(text, text, canUseSearch: false, canUseBrowse: false, null);
				LevelPicker levelPicker = lp;
				string id = "originalDarkest." + text;
				string label = "%" + text2 + "title%";
				LevelProvider.PackSize? packSize = LevelProvider.PackSize.DarkestRoom;
				levelPicker.modifyPack(id, label, null, null, null, null, null, null, null, null, null, packSize);
			}
			lp.update();
			lp.select("originalDarkest.DraculaDarkest.DraculaDarkest1");
		}
		void setupErrorsIfNeeded()
		{
			string text = "";
			if (showRoomEditorOpenError == LoadRoomResult.MissingCustomModels)
			{
				text = "RoomLoadingFailedMissingCustomModelsMessage";
			}
			else if (showRoomEditorOpenError == LoadRoomResult.Failed)
			{
				text = "RoomEditorFailedToLoadMessage";
			}
			else if (showRoomEditorOpenError == LoadRoomResult.NeedsGameUpdate)
			{
				text = "RoomEditorNewerVersionMessage";
			}
			if (!string.IsNullOrEmpty(text))
			{
				VisualControl visualControl = new VisualControl("", ControllerButtonActionType.UIConfirmPrimary, "%RoomEditor_OK%", VisualControl.ControlType.Button, VisualControl.Position.BotCenter);
				visualControl.addKeybindAction(KeyBindingAction.Chat, game.menuOptions);
				visualControl.addEscape(hidden: true);
				menuUI.OptionDialog.show("", text, onOptionDialogClick, visualControl);
			}
			showRoomEditorOpenError = LoadRoomResult.Success;
		}
		void setupFinishedCommunityLevelsPack(LevelPicker lp, string ugcTabId)
		{
			List<string> list = new List<string>();
			string[] array = Array.Empty<string>();
			bool flag = ESUGC.current is ESPineUGC;
			if (flag)
			{
				array = CustomRoomsLocalRepository.get().getInstalledRoomsFilenames();
			}
			foreach (PlayerSave.LevelState levelState4 in PlayerSave.getProgress().levelStates)
			{
				if (ESUGC.current.isCustomLevel(levelState4.levelId))
				{
					PlayerSave.FinishState finishState = levelState4.finishState;
					if (finishState == PlayerSave.FinishState.Finished || finishState == PlayerSave.FinishState.Trophy)
					{
						string communityLevelId = levelState4.levelId;
						bool flag2 = true;
						if (flag)
						{
							string text = Array.Find(array, (string x) => x.Contains(communityLevelId));
							if (text != null)
							{
								communityLevelId = text;
								flag2 = true;
							}
							else
							{
								flag2 = false;
							}
						}
						if (flag2)
						{
							levelProvider.addLevelToList("finished", communityLevelId);
							list.Add(communityLevelId);
						}
					}
				}
			}
			if (list.Count > 0)
			{
				lp.definePack("finished", "finished", canUseSearch: true, canUseBrowse: false, null);
				string id = ugcTabId + ".finished";
				int? levelCount = list.Count;
				LevelProvider.PackSize? packSize = LevelProvider.PackSize.CustomRoom;
				lp.modifyPack(id, "%Menu_Pack_Finished%", null, null, null, null, null, null, levelCount, null, null, packSize);
				ESUGC.current.refreshSpecificRooms(list.ToArray(), delegate(RefreshRoomData data)
				{
					LevelProvider obj = levelProvider;
					string id2 = data.id;
					string roomTitle = data.roomTitle;
					bool? finished = true;
					string imageUrl = data.imageUrl;
					DateTime? dateFolderCreated = data.modified;
					obj.modifyLevel(id2, roomTitle, null, imageUrl, -1, -1, finished, null, null, null, null, null, null, null, dateFolderCreated);
					levelProvider.requestData(data.id);
				});
			}
		}
		void setupLevelPickers()
		{
			List<DLC> dlcsToDisplay = new List<DLC>();
			List<OriginalRoom> originalRoomsToDisplay = new List<OriginalRoom>
			{
				OriginalRoom.Tutorial,
				OriginalRoom.Dracula,
				OriginalRoom.Space,
				OriginalRoom.Pirate
			};
			List<DarkestPuzzles> darkestPuzzlesToDisplay = new List<DarkestPuzzles>
			{
				DarkestPuzzles.DraculaDarkest,
				DarkestPuzzles.SpaceDarkest,
				DarkestPuzzles.PirateDarkest
			};
			Dictionary<DarkestPuzzles, OriginalRoom> darkestToOriginalRoom = new Dictionary<DarkestPuzzles, OriginalRoom>
			{
				{
					DarkestPuzzles.DraculaDarkest,
					OriginalRoom.Dracula
				},
				{
					DarkestPuzzles.SpaceDarkest,
					OriginalRoom.Space
				},
				{
					DarkestPuzzles.PirateDarkest,
					OriginalRoom.Pirate
				}
			};
			if (Is.Editor || Is.DebugBuild)
			{
				originalRoomsToDisplay.Add(OriginalRoom.Test);
			}
			levelProvider = new LevelProvider(ESUGC.current, onLevelRefresh);
			lpRooms = new LevelPicker(menuUI.Rooms_LevelPicker, levelProvider);
			lpDarkest = new LevelPicker(menuUI.Darkest_LevelPicker, levelProvider);
			setupLevelProvider();
			setupMainLevelPicker(lpRooms);
			setupDarkestLevelPicker(lpDarkest);
			setupOtherScenes();
		}
		void setupLevelProvider()
		{
			foreach (OriginalRoom value7 in Enum.GetValues(typeof(OriginalRoom)))
			{
				string listId = value7.ToString();
				foreach (RoomDatabase.Room allRoom in RoomDatabase.getAllRooms(value7, includeUnbuiltRooms: true))
				{
					if (allRoom.shouldBeDisplayed && (!Is.Oculus || allRoom.isAvailableOnOculus || allRoom.unlockTime.Ticks != 0L))
					{
						levelProvider.addLevelToList(listId, allRoom.name);
						PlayerSave.LevelState levelState = PlayerSave.ensureAndGetLevelState(allRoom.name);
						int num = ((value7 == OriginalRoom.Tutorial) ? (-1) : levelState.foundTokens.Count);
						int num2 = ((value7 == OriginalRoom.Tutorial) ? (-1) : 8);
						bool value = levelState.finishState == PlayerSave.FinishState.Finished;
						bool value2 = levelState.finishState == PlayerSave.FinishState.Trophy;
						DateTime? dateTime = ((allRoom.unlockTime.Ticks == 0L) ? ((DateTime?)null) : new DateTime?(allRoom.unlockTime));
						bool value3 = (Is.Demo && !allRoom.isInDemo) || dateTime.HasValue;
						if (!allRoom.shouldBeBuilt)
						{
							value3 = true;
						}
						levelProvider.modifyLevel(allRoom.name, "%" + RoomDatabase.roomNameToLocalizationKey(allRoom.name) + "%", allRoom.name + ".jpg", null, num, num2, value, value2, isLocked: value3, unlockTime: dateTime, isNew: false);
					}
				}
			}
			foreach (DarkestPuzzles value8 in Enum.GetValues(typeof(DarkestPuzzles)))
			{
				string listId2 = value8.ToString();
				foreach (RoomDatabase.Room allRoom2 in RoomDatabase.getAllRooms(value8, includeUnbuiltRooms: true))
				{
					if (allRoom2.shouldBeDisplayed && (!Is.Oculus || allRoom2.isAvailableOnOculus || allRoom2.unlockTime.Ticks != 0L))
					{
						levelProvider.addLevelToList(listId2, allRoom2.name);
						PlayerSave.LevelState levelState2 = PlayerSave.ensureAndGetLevelState(allRoom2.name);
						string text = allRoom2.name;
						char c = text[text.Length - 1];
						RoomDatabase.Room room = RoomDatabase.getRoom(P_0.darkestToOriginalRoom[value8].ToString() + c);
						PlayerSave.LevelState levelState3 = ((room == null) ? new PlayerSave.LevelState() : PlayerSave.ensureAndGetLevelState(room.name));
						int count = levelState2.foundTokens.Count;
						bool value4 = levelState2.finishState == PlayerSave.FinishState.Finished;
						bool value5 = levelState2.finishState == PlayerSave.FinishState.Trophy;
						bool num3 = !Is.Demo || allRoom2.isInDemo;
						PlayerSave.FinishState finishState = levelState3.finishState;
						bool flag = finishState == PlayerSave.FinishState.Finished || finishState == PlayerSave.FinishState.Trophy;
						if (Is.Editor)
						{
							flag = true;
						}
						bool value6 = !num3 || !flag;
						if (!allRoom2.shouldBeBuilt)
						{
							value6 = true;
						}
						levelProvider.modifyLevel(allRoom2.name, "%" + RoomDatabase.roomNameToLocalizationKey(allRoom2.name) + "%", allRoom2.name + ".jpg", null, count, 8, value4, value5, displayMode: LevelProvider.DisplayMode.Hidden, isLocked: value6, isNew: false);
					}
				}
			}
		}
		void setupLobby()
		{
			syncMenuWithNetSession();
			if (session.netMode != NetMode.Host)
			{
				transitionToLobby();
			}
			foreach (NetPlayerData player in session.players)
			{
				if (!session.isLocalPlayer(player.id))
				{
					Channel channel = default(Channel);
					bool flag = false;
					if (session.lobbyType == LobbyType.PhotonPine)
					{
						if (player.photonSpeaker != null)
						{
							channel = ((PineFmodAudioOut<float>)player.photonSpeaker.audioOutput).Channel;
							flag = true;
						}
					}
					else if (player.steamSpeaker != null)
					{
						channel = player.steamSpeaker.Channel;
						flag = true;
					}
					if (flag)
					{
						PineFmod.setMode(channel, PineFmodAudioOut<float>.DEFAULT_MODE | MODE._2D);
						PineFmod.setVolume(channel, 1f);
						VECTOR pos = new VECTOR
						{
							x = 0f,
							y = 0f,
							z = 0f
						};
						VECTOR vel = default(VECTOR);
						PineFmod.set3DAttributes(channel, ref pos, ref vel);
					}
				}
			}
			if (!enteredMenuForFirstTime)
			{
				string text = null;
				string[] commandLineArgs = Environment.GetCommandLineArgs();
				foreach (string text2 in commandLineArgs)
				{
					if (!string.IsNullOrEmpty(text2))
					{
						if (!Is.Editor)
						{
							UnityEngine.Debug.Log("Additional args: " + text2);
						}
						if (text2.StartsWith("steam:") || text2.StartsWith("photon:"))
						{
							text = text2;
						}
					}
				}
				if (text != null)
				{
					joinRoomByInvite(text);
				}
			}
		}
		void setupMainLevelPicker(LevelPicker lp)
		{
			bool pineBackend = Is.PineBackend;
			lp.onClick = delegate
			{
				onPickerClick(lp);
			};
			lp.onDoubleClick = onPickerDoubleClick;
			lp.onBrowserClick = onBrowseClick;
			lp.startTab("original", "%Original%", Is.Oculus);
			foreach (OriginalRoom item2 in P_1.originalRoomsToDisplay)
			{
				string text = item2.ToString();
				string text2 = item2.ToString().ToLower();
				lp.definePack(text, text, canUseSearch: false, canUseBrowse: false, null);
				lp.modifyPack("original." + text, "%" + text2 + "title%");
			}
			if (P_1.dlcsToDisplay.Count > 0)
			{
				lp.startTab("dlc", "%DLC%", true);
				foreach (DLC item3 in P_1.dlcsToDisplay)
				{
					if (!dlcToHideIfNotInstalled.Contains(item3) || hasDLC(item3))
					{
						string text3 = item3.ToString();
						string text4 = item3.ToString().ToLower();
						lp.definePack(text3, text3, canUseSearch: false, canUseBrowse: false, null);
						bool flag = !Is.Oculus && item3 == DLC.Mayan;
						int num2;
						if (Is.Oculus)
						{
							DLC num = item3;
							List<DLC> oculusAvailableDLCs = VR.oculusAvailableDLCs;
							num2 = ((num == oculusAvailableDLCs[oculusAvailableDLCs.Count - 1]) ? 1 : 0);
						}
						else
						{
							num2 = 0;
						}
						bool flag2 = (byte)num2 != 0;
						bool value = item3 == DLC.Portal || item3 == DLC.AmongUs || item3 == DLC.PowerWash || item3 == DLC.Talos;
						lp.modifyPack("dlc." + text3, "%" + text4 + "title%", flag || flag2, value);
					}
				}
			}
			LevelPicker levelPicker = lp;
			bool? isBeta = isVR();
			bool demo = Is.Demo;
			levelPicker.startTab("ugcSteam", "%Community%", null, isBeta, demo);
			int? levelCount;
			LevelProvider.PackSize? packSize;
			if (!pineBackend && !Is.Demo)
			{
				lp.definePack("installedSteam", "installedSteam", canUseSearch: true, canUseBrowse: false, lp.getInstalledLevelsComparer());
				LevelPicker levelPicker2 = lp;
				levelCount = 0;
				packSize = LevelProvider.PackSize.CustomRoom;
				levelPicker2.modifyPack("ugcSteam.installedSteam", "%menuInstalled%", null, null, null, null, null, null, levelCount, null, null, packSize);
				setupFinishedCommunityLevelsPack(lp, "ugcSteam");
				lp.definePack("trending", "trending", canUseSearch: false, canUseBrowse: false, null);
				LevelPicker levelPicker3 = lp;
				packSize = LevelProvider.PackSize.CustomRoom;
				levelPicker3.modifyPack("ugcSteam.trending", "%trending%", null, null, null, null, null, null, null, null, null, packSize);
				lp.definePack("advancedSearch", "advancedSearch", canUseSearch: false, canUseBrowse: false, lp.getInstalledLevelsComparer());
				LevelPicker levelPicker4 = lp;
				packSize = LevelProvider.PackSize.CustomRoom;
				levelPicker4.modifyPack("ugcSteam.advancedSearch", "%AdvancedSearch%", null, null, null, null, null, null, null, null, null, packSize);
				lp.defineSeparator();
			}
			lp.startTab("ugcPine", "%Community%");
			lp.definePack("installedPine", "installedPine", canUseSearch: false, canUseBrowse: false, lp.getInstalledLevelsComparer());
			LevelPicker levelPicker5 = lp;
			levelCount = 0;
			packSize = LevelProvider.PackSize.CustomRoom;
			levelPicker5.modifyPack("ugcPine.installedPine", "%menuInstalled%", null, null, null, null, null, null, levelCount, null, null, packSize);
			if (pineBackend)
			{
				setupFinishedCommunityLevelsPack(lp, "ugcPine");
			}
			if (Is.Steam)
			{
				lp.changeVisibility("ugcPine", visible: false);
			}
			lp.update();
			lp.select("original.Dracula");
		}
		void setupNews()
		{
			MenuNewsUI menuNews = menuUI.ES2Cover.MenuNews;
			menuNews.MenuButton.gameObject.SetActive(value: false);
			menuNews.root.alpha = 0f;
		}
		void setupOtherScenes()
		{
			levelProvider.addLevelToList("other", "RoomEditor");
			levelProvider.modifyLevel("RoomEditor", "roomEditor", "RoomEditor.jpg");
			levelProvider.requestData("RoomEditor");
			levelProvider.addLevelToList("other", "CustomLevel");
			levelProvider.modifyLevel("CustomLevel", "CustomLevel", "RoomEditor.jpg");
			levelProvider.requestData("CustomLevel");
		}
		void setupRemoteConfig()
		{
			Executor.executeTask(initUnityRemoteConfig(), delegate
			{
			});
		}
	}

	private void applyRemoteSettings(ConfigResponse configResponse)
	{
		string obj = (Is.Demo ? "Demo" : "");
		string key = obj + "NewsImage";
		string key2 = obj + "NewsText";
		string key3 = obj + "NewsLink";
		_ = obj + "NewsAltImage";
		_ = obj + "NewsAltText";
		_ = obj + "NewsAltSubtitle";
		_ = obj + "NewsAltLink";
		bool num = RemoteConfigService.Instance.appConfig.HasKey(key);
		bool flag = RemoteConfigService.Instance.appConfig.HasKey(key2);
		bool flag2 = RemoteConfigService.Instance.appConfig.HasKey(key3);
		if (num && flag && flag2)
		{
			string imageUrl = RemoteConfigService.Instance.appConfig.GetString(key);
			string text = RemoteConfigService.Instance.appConfig.GetString(key2);
			string text2 = RemoteConfigService.Instance.appConfig.GetString(key3);
			if (this != null)
			{
				StartCoroutine(populateNews(imageUrl, text, text2));
			}
			else
			{
				UnityEngine.Debug.LogError("Attempted to start coroutine on destroyed 'Menu' object.");
			}
		}
		string key4 = "workshopCollection";
		if (RemoteConfigService.Instance.appConfig.HasKey(key4))
		{
			string json = RemoteConfigService.Instance.appConfig.GetString(key4);
			try
			{
				MainMenuCollection mainCollection = JsonUtility.FromJson<MainMenuCollection>(json);
				initMainMenuCollection(mainCollection);
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.Log("Failed to parse collection json: " + ex.Message);
			}
		}
	}

	private void Update()
	{
		if (game == null)
		{
			return;
		}
		framesWithGame++;
		if (SplashScreen.finished && menuUI.ES2Cover.gameObject.activeInHierarchy && canStartMenu)
		{
			game.forceEnableCursor();
		}
		if (framesWithGame == 20)
		{
			PerformanceTester.startTestingIfNeeded();
		}
		menuInputDispatcher.update();
		tweener.processTweens(Time.deltaTime);
		menuUI.OptionDialog.update(menuInputDispatcher);
		if (isGameLoadedProperly)
		{
			Controller.validateSelectingGroup();
			if (session.netMode == NetMode.Client)
			{
				lpRooms.setOverrideListIdOnWholeTab("ugcSteam", "hostLevelList");
			}
			else
			{
				lpRooms.setOverrideListIdOnWholeTab("ugcSteam", "");
			}
			levelProvider.update();
			lpRooms.update();
			lpDarkest.update();
			ESUGC.current.update();
			updateES2Menu();
			updateCCController();
			updatePineWorkshop();
			updateVR();
			updateController();
			updateCharacterCustomization();
			updateMenuNet();
			updateClientCode(rebuildUI: false);
			updateLoadingReasons();
			if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
			{
				joinRoomWithCode();
			}
		}
		void updateLoadingReasons()
		{
			if (loadingReasons.Count > 0)
			{
				string line = loadingReasons[0] switch
				{
					LoadingReason.PreparingLobby => "coopPreparingLobby", 
					LoadingReason.JoiningRoom => "waitingHost", 
					LoadingReason.FindingRandomMatch => "connecting", 
					LoadingReason.ConnectingNSA => "connectingNSA", 
					LoadingReason.ClientLoadingSave => "ClientLoadingSave", 
					_ => "", 
				};
				menuUI.Loading.gameObject.SetActive(value: true);
				menuUI.Loading.Title.text = Localization.lookupInDictionary(line);
			}
			else
			{
				menuUI.Loading.gameObject.SetActive(value: false);
			}
		}
	}

	private void OnDestroy()
	{
		session.menuCallbacks = null;
		if (ticketForWebApiResponse != null)
		{
			ticketForWebApiResponse.Dispose();
			ticketForWebApiResponse = null;
		}
		RemoteConfigService.Instance.FetchCompleted -= applyRemoteSettings;
	}

	private void OnApplicationQuit()
	{
		PineFmod.destroy();
		if (SteamManager.Initialized)
		{
			session?.destroy();
			session?.log("destroyed session in OnApplicationQuit");
			Controller.destroy();
			session?.log("destroyed controller in OnApplicationQuit");
		}
		SharedWriter.dispose();
	}

	private void onLevelRefresh(string id)
	{
	}

	private void onFrameClick(string id)
	{
		UnityEngine.Debug.Log("Frame click (Menu) " + id);
		switch (id)
		{
		case "closeInitialOptions":
			closeInitialOptions();
			break;
		case "initialSkipTutorial":
			initialSkipTutorial();
			break;
		case "initialPlayTutorial":
			initialPlayTutorial();
			break;
		case "welcomePlay":
			menuUI.InitialWelcomeMessage.gameObject.SetActive(value: false);
			UnityEngine.Debug.Log("[JURA] TODO");
			break;
		case "play":
			playSelectedLevel();
			break;
		case "select":
			getActiveLevelPicker().selectPack();
			break;
		case "randomize":
		{
			for (int i = 0; i < allCategories.Count; i++)
			{
				List<CCVariant> list = allCategories[i].variants.FindAll((CCVariant x) => !x.button.ui.Locked.gameObject.activeInHierarchy);
				if (list.Count > 0)
				{
					CCVariant cCVariant = list[UnityEngine.Random.Range(0, list.Count)];
					onCCVariant(cCVariant.button.ui, i == allCategories.Count - 1);
				}
			}
			break;
		}
		case "openDetails":
			onOpenDetails();
			break;
		case "filter":
			menuUI.TagPicker.gameObject.SetActive(value: true);
			lpRooms.setControllerUsable(value: false);
			updateController(forceChange: true);
			break;
		case "filterSearch":
			fireAdvancedSearch();
			menuUI.TagPicker.gameObject.SetActive(value: false);
			lpRooms.setControllerUsable(value: true);
			break;
		case "back":
			onBack();
			break;
		case "copy":
			openCopyDialog();
			break;
		case "copyWorkshopYes":
			copyWorkshopRoom();
			break;
		case "install":
			installSelectedLevel();
			break;
		case "uninstall":
			uninstallSelectedLevel();
			break;
		case "join":
			joinRoomWithCode();
			break;
		case "deleteCode":
			deleteRoomCodeLetter();
			break;
		case "getDlc":
			openDlcPageForSelectedLevel();
			break;
		case "oculusReinstallDlcLevel":
		{
			(string tabId, string packId, string levelId) selectedId = getActiveLevelPicker().getSelectedId();
			string item = selectedId.packId;
			string item2 = selectedId.levelId;
			DLC dlc = findDlcFromName(item);
			int levelIndex = int.Parse(item2[item2.Length - 1].ToString());
			showOculusReinstallDlcLevelMessage(dlc, levelIndex);
			break;
		}
		}
		void closeInitialOptions()
		{
			menuUI.InitialSetup.Options.gameObject.SetActive(value: false);
			menuUI.InitialSetup.Tutorial.gameObject.SetActive(value: true);
			initialSetupVisualController.removeAll();
			Transform tutorial_Layout_Content_ButtonContainer = menuUI.InitialSetup.Tutorial_Layout_Content_ButtonContainer;
			VisualControl visualControl = new VisualControl("initialSkipTutorial", ControllerButtonActionType.UIBack, "%Skip%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
			visualControl.addEscape();
			initialSetupVisualController.addOrUpdateControl(visualControl, visualControlUI, tutorial_Layout_Content_ButtonContainer);
			VisualControl visualControl2 = new VisualControl("initialPlayTutorial", ControllerButtonActionType.UIConfirmPrimary, "%play%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
			visualControl2.addKeybindAction(KeyBindingAction.Chat, game.menuOptions);
			initialSetupVisualController.addOrUpdateControl(visualControl2, visualControlUI, tutorial_Layout_Content_ButtonContainer);
			layoutRebuilder.requestRebuild(menuUI.InitialSetup.Tutorial_Layout_Content_ButtonContainer);
			layoutRebuilder.requestRebuild(menuUI.InitialSetup.Tutorial);
		}
		void initialPlayTutorial()
		{
			openShouldLoadLevelDialog("Tutorial1");
			shouldEnterPoseInInit = true;
			menuUI.InitialSetup.gameObject.SetActive(value: false);
			PlayerSave.getSettings().didInitialSetup = true;
			PlayerSave.flush();
		}
		void initialSkipTutorial()
		{
			menuUI.InitialSetup.gameObject.SetActive(value: false);
			PlayerSave.getSettings().didInitialSetup = true;
			PlayerSave.flush();
			if (Controller.isActive())
			{
				updateController(forceChange: true);
			}
		}
		void installSelectedLevel()
		{
			LevelPicker activeLevelPicker = getActiveLevelPicker();
			string item3 = activeLevelPicker.getSelectedId().levelId;
			ESUGC.current.installRoom(item3);
			levelProvider.requestData(item3);
			updatePickerButtons(activeLevelPicker, "");
		}
		void onOpenDetails()
		{
			LevelPicker activeLevelPicker = getActiveLevelPicker();
			var (text, _, id2) = activeLevelPicker.getSelectedId();
			UnityEngine.Debug.Log(text);
			if (isUgcTab(text))
			{
				activeLevelPicker.setControllerUsable(value: false);
				menuUI.RoomInfoPopup.gameObject.SetActive(value: true);
				LevelProvider.LevelItem levelItem = levelProvider.getLevelItem(id2);
				menuUI.RoomInfoPopup.activateRoomInfo(levelItem, session.netMode == NetMode.Client);
				updateDetailsControls();
			}
		}
		void playSelectedLevel()
		{
			var (text, text2, text3) = getActiveLevelPicker().getSelectedId();
			if (game.isHost())
			{
				if (!string.IsNullOrEmpty(text3))
				{
					UnityEngine.Debug.Log(text + "." + text2 + "." + text3);
					openShouldLoadLevelDialog(text3);
				}
			}
			else
			{
				UnityEngine.Debug.Log("requesting scene load " + text3);
				if (hostsRoomsWithSave.Contains(text3))
				{
					VisualControl visualControl = new VisualControl("loadLevel", ControllerButtonActionType.UIConfirmPrimary, "%YesLoad%", VisualControl.ControlType.Button, VisualControl.Position.BotCenter);
					visualControl.addKeybindAction(KeyBindingAction.Chat, game.menuOptions);
					VisualControl visualControl2 = new VisualControl("startNewLevel", ControllerButtonActionType.UIBack, "%NoStartNew%", VisualControl.ControlType.Button, VisualControl.Position.BotCenter);
					visualControl2.addKeybindAction(KeyBindingAction.ExamineInventory, game.menuOptions);
					VisualControl visualControl3 = new VisualControl("", ControllerButtonActionType.UIBack, "%cancel%", VisualControl.ControlType.Button, VisualControl.Position.BotCenter);
					visualControl3.addEscape();
					loadLevelId = text3;
					menuUI.OptionDialog.show(RoomDatabase.roomNameToLocalizationKey(text3), "ContinueDetails", onOptionDialogClick, visualControl, visualControl2, visualControl3);
				}
				else
				{
					startSceneCheck(text3, shouldLoadLatestSave: false);
				}
			}
		}
		void uninstallSelectedLevel()
		{
			var (text, text2, text3) = getActiveLevelPicker().getSelectedId();
			UnityEngine.Debug.Log("Uninstalling room: " + text + ", " + text2 + ", " + text3);
			ESUGC.current.uninstallRoom(text3);
			levelProvider.requestData(text3);
		}
	}

	private void LateUpdate()
	{
		layoutRebuilder.lateUpdate();
	}

	private void updateDetailsControls()
	{
		LevelPicker activeLevelPicker = getActiveLevelPicker();
		string item = activeLevelPicker.getSelectedId().levelId;
		List<VisualControl> list = new List<VisualControl>
		{
			new VisualControl("back", ControllerButtonActionType.UIBack, "%back%", VisualControl.ControlType.Button, VisualControl.Position.BotLeft)
		};
		switch (ESUGC.current.customRoomState(item))
		{
		case CustomRoomState.NotInstalled:
		case CustomRoomState.NeedsUpdate:
		{
			VisualControl visualControl4 = new VisualControl("install", ControllerButtonActionType.UIConfirmPrimary, "%notInstalledSimple%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
			visualControl4.addKeybindAction(KeyBindingAction.Chat, game.menuOptions);
			list.Add(visualControl4);
			break;
		}
		case CustomRoomState.Installing:
		{
			VisualControl visualControl3 = new VisualControl("installing", ControllerButtonActionType.UIConfirmPrimary, "%installing%", VisualControl.ControlType.Text, VisualControl.Position.BotRight);
			visualControl3.addKeybindAction(KeyBindingAction.Chat, game.menuOptions);
			list.Add(visualControl3);
			break;
		}
		case CustomRoomState.Installed:
		{
			VisualControl visualControl = new VisualControl("play", ControllerButtonActionType.UIConfirmPrimary, "%play%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
			visualControl.addKeybindAction(KeyBindingAction.Chat, game.menuOptions);
			list.Add(visualControl);
			VisualControl item2 = new VisualControl("copy", ControllerButtonActionType.UIZoomin, "%copy%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
			list.Add(item2);
			VisualControl visualControl2 = new VisualControl("uninstall", ControllerButtonActionType.UIExtra1, "%Delete%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
			visualControl2.addKeybindAction(KeyBindingAction.ExamineInventory, game.menuOptions);
			list.Add(visualControl2);
			break;
		}
		}
		activeLevelPicker.ui.Frame.addOrUpdateControl(list);
	}

	private void onButtonClick(Button button)
	{
		PineFmod.playOneShotSoundAttached("event:/Sound Effects/00 General/Menu Sound Effects/Small/UI_Small_03", button.gameObject);
		PackUI component;
		CCVariantUI component2;
		if (button == menuUI.ES2Cover.MainButtons_ButtonPlay)
		{
			if (Is.Gamescom)
			{
				transitionToLobby();
				return;
			}
			menuUI.ES2Cover.MainButtons.gameObject.SetActive(value: false);
			menuUI.ES2Cover.PlayButtons.gameObject.SetActive(value: true);
			if (Controller.isActive())
			{
				Controller.setupVerticalNavigationAndSelectFirst(menuUI.ES2Cover.PlayButtons.transform);
			}
		}
		else if (button == menuUI.ES2Cover.MainButtons_ButtonBuild)
		{
			if (Is.Demo || Is.Gamescom)
			{
				VisualControl visualControl = new VisualControl("", ControllerButtonActionType.UIBack, "%RoomEditor_OK%", VisualControl.ControlType.Button, VisualControl.Position.BotCenter);
				visualControl.addEscape();
				menuUI.OptionDialog.show("roomEditor", "RoomEditorAvaliableInFullGame", onOptionDialogClick, visualControl);
			}
			else
			{
				game.saveAchievement("ACHIEVEMENT_BUILDER");
				game.loadScene("RoomEditor", shouldLoadLatestSave: false);
				shouldEnterPoseInInit = true;
			}
		}
		else if (button == menuUI.ES2Cover.MainButtons_ButtonQuit)
		{
			UnityEngine.Debug.Log("Application.Quit();");
			if (!UnityEngine.Application.isEditor)
			{
				if (SteamManager.Initialized && SteamUtils.IsSteamRunningOnSteamDeck() && Is.Linux)
				{
					LinuxQuit.linuxQuit();
				}
				else
				{
					UnityEngine.Application.Quit();
				}
			}
		}
		else if (button == menuUI.ES2Cover.PlayButtons_ButtonSolo)
		{
			transitionToLobby();
		}
		else if (button == menuUI.ES2Cover.PlayButtons_ButtonHost)
		{
			transitionToLobby();
			session.pressedHost = true;
			timePressedHost = Time.time;
		}
		else if (button == menuUI.ES2Cover.PlayButtons_ButtonJoin)
		{
			openClientCode();
		}
		else if (button == menuUI.ES2Cover.MainButtons_ButtonOptions)
		{
			game.menuOptions.gameObject.SetActive(value: true);
			menuUI.ES2Cover.gameObject.SetActive(value: false);
		}
		else if (button == menuUI.ES2Cover.PlayButtons_ButtonBack)
		{
			menuUI.ES2Cover.MainButtons.gameObject.SetActive(value: true);
			menuUI.ES2Cover.PlayButtons.gameObject.SetActive(value: false);
			if (Controller.isActive())
			{
				Controller.setupVerticalNavigationAndSelectFirst(menuUI.ES2Cover.MainButtons.transform);
			}
		}
		else if (button == menuUI.ES2Cover.MainButtons_ButtonCredits)
		{
			openCredits();
		}
		else if (button == menuUI.ES2Cover.MenuNews.MenuButton)
		{
			openWebPage("https://store.steampowered.com/app/2879840/Escape_Simulator_2/?utm_source=es2ingame&utm_campaign=es2News");
		}
		else if (button == menuUI.ES2Cover.Logo_Version)
		{
			onVersionClick();
		}
		else if (button == menuUI.ES2Cover.MainButtons_ButtonDiscord)
		{
			UnityEngine.Application.OpenURL("https://discord.gg/mFEeSk5AhT");
		}
		else if (button.TryGetComponent<PackUI>(out component))
		{
			LevelPicker activeLevelPicker = getActiveLevelPicker();
			if (activeLevelPicker != null)
			{
				handlePackClick(activeLevelPicker.getSelectedId().packId);
			}
		}
		else if (button.TryGetComponent<CCVariantUI>(out component2))
		{
			onCCVariant(component2);
		}
		else if (button == menuUI.Rooms_AdvancedSearch_FiltersBtn)
		{
			menuUI.TagPicker.gameObject.SetActive(value: true);
		}
		void handlePackClick(string packId)
		{
			if (Is.Oculus && !VR.oculusBetaRoomsAvailable)
			{
				if (!(packId == "dc96de3e-56d5-4c2b-934f-636edcfc3ae7"))
				{
					vrOculusBrowsePackConsecutiveClickCount = 0;
				}
				else
				{
					vrOculusBrowsePackConsecutiveClickCount++;
					if (vrOculusBrowsePackConsecutiveClickCount >= 7)
					{
						VR.oculusBetaRoomsAvailable = true;
						fillPack("beta", "Beta");
					}
				}
			}
		}
		void openCredits()
		{
			if (!(currentOpenCredits != null))
			{
				Credits credits = UnityEngine.Object.Instantiate(menuUI.Credits, menuUI.Credits.transform.parent);
				credits.transform.localPosition = Vector3.zero;
				credits.transform.localRotation = Quaternion.identity;
				credits.transform.localScale = Vector3.one;
				if (isVR())
				{
					credits.gameObject.AddComponent<Mask>();
					vr.mapCanvas(credits.GetComponent<Canvas>(), vrCanvasTemplate);
				}
				credits.showCredits(delegate
				{
					credits.gameObject.SetActive(value: false);
					Controller.selectSelectable(menuUI.ES2Cover.MainButtons_ButtonCredits);
				});
				currentOpenCredits = credits;
			}
		}
	}

	private void onOptionDialogClick(string id)
	{
		UnityEngine.Debug.Log("Option Dialog click (Menu) " + id);
		switch (id)
		{
		case "failedLobby":
			if (Controller.isActive())
			{
				Controller.selectSelectable(menuUI.ES2Cover.PlayButtons_ButtonJoin);
			}
			break;
		case "quit":
			UnityEngine.Application.Quit();
			break;
		case "openEditor":
			openShouldLoadLevelDialog("roomeditor");
			break;
		case "deleteRoom":
		{
			string item = getActiveLevelPicker().getSelectedId().levelId;
			UnityEngine.Debug.Log("Deleted room: " + item);
			Directory.Delete(Path.Combine(UnityEngine.Application.persistentDataPath, "UGC", item), recursive: true);
			levelProvider.removeLevelFromList("yourrooms", item);
			break;
		}
		case "coopNotFound":
			if (Controller.isActive())
			{
				Controller.selectSelectable(menuUI.ES2Cover.PlayButtons_ButtonJoin);
			}
			break;
		case "hostProton":
			host(ConnectionMode.Photon);
			break;
		case "loadLevel":
			startSceneCheck(loadLevelId, shouldLoadLatestSave: true);
			break;
		case "startNewLevel":
			startSceneCheck(loadLevelId, shouldLoadLatestSave: false);
			break;
		}
	}

	private void onBrowseClick(string searchInput)
	{
		if (!Is.Console)
		{
			string url = "http://steamcommunity.com/app/1435790/workshop/";
			string text = searchInput.Trim();
			if (!string.IsNullOrEmpty(text))
			{
				url = "https://steamcommunity.com/workshop/browse/?appid=1435790&searchtext=" + text.Replace(' ', '+');
			}
			openWebPage(url);
		}
	}

	public void onBack(bool fromUIButton = true)
	{
		UnityEngine.Debug.Log("onBack(); " + Time.frameCount);
		if (loadingReasons.Count > 0)
		{
			return;
		}
		bool flag = true;
		if (menuUI.TagPicker.gameObject.activeInHierarchy)
		{
			menuUI.TagPicker.gameObject.SetActive(value: false);
			lpRooms.setControllerUsable(value: true);
		}
		else if (menuUI.ClientCode.gameObject.activeInHierarchy)
		{
			menuUI.ClientCode.gameObject.SetActive(value: false);
			menuUI.ES2Cover.gameObject.SetActive(value: true);
			Controller.setupVerticalNavigationAndSelectFirst(menuUI.ES2Cover.PlayButtons.transform);
		}
		else if (menuUI.OptionDialog.gameObject.activeInHierarchy)
		{
			menuUI.OptionDialog.hide();
		}
		else if (menuUI.RoomInfoPopup.gameObject.activeInHierarchy)
		{
			menuUI.RoomInfoPopup.gameObject.SetActive(value: false);
			LevelPicker activeLevelPicker = getActiveLevelPicker();
			activeLevelPicker.setControllerUsable(value: true);
			var (text, text2, levelId) = activeLevelPicker.getSelectedId();
			if (text2 == "installedPine" && ESUGC.current.customRoomState(levelId) == CustomRoomState.NotInstalled)
			{
				activeLevelPicker.select(text + "." + text2);
			}
			flag = false;
			updatePickerButtons(getActiveLevelPicker(), "");
		}
		else if (menuUI.Rooms.gameObject.activeInHierarchy)
		{
			if (!lpRooms.onBackFromLevelSelectOnController(fromUIButton))
			{
				game.handleZoomLeave();
			}
		}
		else if (menuUI.Darkest.gameObject.activeInHierarchy)
		{
			if (!lpDarkest.onBackFromLevelSelectOnController(fromUIButton))
			{
				game.handleZoomLeave();
			}
		}
		else if (menuUI.Customization.gameObject.activeInHierarchy)
		{
			if (controllerSelectedCCGroup != null)
			{
				Controller.selectSelectable(controllerSelectedCCGroup.root);
				controllerSelectedCCGroup = null;
			}
			else
			{
				menuUI.Customization.gameObject.SetActive(value: false);
				doFade(delegate
				{
					game.handleCustomModeLeave(ccCustomMode);
				});
			}
		}
		else if (game.menuOptions.gameObject.activeInHierarchy)
		{
			UnityEngine.Debug.Log("asd");
			if (game.isRebindingEnterKeyOpen)
			{
				game.closeRebindingEnterKey();
			}
			else
			{
				game.menuOptions.closeActions();
				game.syncQualitySettings();
				game.menuOptions.gameObject.SetActive(value: false);
				menuUI.ES2Cover.gameObject.SetActive(value: true);
			}
			Controller.selectSelectable(menuUI.ES2Cover.MainButtons_ButtonOptions);
			flag = false;
		}
		else if (game.gameState.current == Game.GameState.Room)
		{
			UnityEngine.Debug.Log("[JURA] TODO");
			game.openMainMenu();
		}
		if (flag)
		{
			updateController(forceChange: true);
		}
	}

	private void onUGCChanged(string levelId, ESUGCChange changeType, ESUGC sender)
	{
		switch (changeType)
		{
		case ESUGCChange.Installed:
			refreshAllInstalledRooms();
			if (!string.IsNullOrEmpty(waitForInstallToLoadLevelId) && levelId.ToString() == waitForInstallToLoadLevelId)
			{
				waitForInstallToLoadLevelId = null;
			}
			break;
		case ESUGCChange.Modify:
			refreshSpecificInstalledRooms(levelId);
			break;
		case ESUGCChange.Uninstalled:
		{
			string listId = ((sender is ESPineUGC) ? "installedPine" : "installedSteam");
			levelProvider.removeLevelFromList(listId, levelId);
			break;
		}
		}
		updatePickerButtons(lpRooms, "");
		if (menuUI.RoomInfoPopup.gameObject.activeInHierarchy)
		{
			updateDetailsControls();
		}
	}

	private void onPickerDoubleClick(Button button)
	{
		var (text, text2, _) = getActiveLevelPicker().getSelectedId();
		if (!button.TryGetComponent<OptionsPrefab_TabButton>(out var _) && !button.TryGetComponent<PackUI>(out var _))
		{
			if (text2 == "yourrooms")
			{
				onFrameClick("edit");
			}
			else if (text == "dlc")
			{
				onFrameClick(hasDLC(findDlcFromName(text2)) ? "play" : "getDlc");
			}
			else
			{
				onFrameClick("play");
			}
		}
	}

	private void onChangeLanguage()
	{
		Localization.init(PlayerSave.getSettings().language);
		Localization.translateCurrentScene();
		if (newsTexts.Count > 0)
		{
			string text = Localization.allLanguages[PlayerSave.getSettings().language].systemLanguage.ToString();
			if (newsTexts.TryGetValue(text, out var value))
			{
				menuUI.ES2Cover.MenuNews.MenuButton_MessageText.text = value;
			}
			else
			{
				UnityEngine.Debug.LogError("News text does not exist for language '" + text + "'.");
			}
		}
		levelProvider.requestAllItems();
		lpRooms.setShouldSyncVisuals();
		lpDarkest.setShouldSyncVisuals();
	}

	private bool isUgcTab(string tabId)
	{
		if (!(tabId == "ugcPine"))
		{
			return tabId == "ugcSteam";
		}
		return true;
	}

	private void installDLC(DLC dlc)
	{
		SteamApps.InstallDLC(getDlcSteamId(dlc));
	}

	private void openDlcPageForSelectedLevel()
	{
		string item = getActiveLevelPicker().getSelectedId().packId;
		openDlcPage(findDlcFromName(item));
	}

	private void openDlcPage(DLC dlc)
	{
		if (Is.Oculus)
		{
			launchOculusDlcPurchaseFlow(dlc);
			return;
		}
		openWebPage(dlc switch
		{
			DLC.Dieselpunk => "https://store.steampowered.com/app/1942100/Escape_Simulator_Steampunk_DLC/?utm_source=landing&utm_campaign=DLCingame&cart_add=true", 
			DLC.Portal => "https://store.steampowered.com/app/2000170/Escape_Simulator_Portal_Escape_Chamber/?utm_source=landing&utm_campaign=DLCingame", 
			DLC.Magic => "https://store.steampowered.com/app/2419810/Escape_Simulator_Magic_DLC/?utm_source=landing&utm_campaign=DLCingame&cart_add=true", 
			DLC.AmongUs => "https://store.steampowered.com/app/2644870/Escape_Simulator_Among_Us_DLC/?utm_source=landing&utm_campaign=DLCingame", 
			DLC.Western => "https://store.steampowered.com/app/2175260/Escape_Simulator_Wild_West_DLC/?utm_source=landing&utm_campaign=DLCingame&cart_add=true", 
			DLC.PowerWash => "https://store.steampowered.com/app/2879620/Escape_Simulator_PowerWash_DLC/?utm_source=landing&utm_campaign=DLCingame", 
			DLC.Talos => "https://store.steampowered.com/app/3193660/Escape_Simulator_The_Talos_Principle_DLC/?utm_source=landing&utm_campaign=DLCingame", 
			DLC.Mayan => "https://store.steampowered.com/app/2644890/Escape_Simulator_Mayan_DLC/?utm_source=landing&utm_campaign=DLCingame", 
			_ => "", 
		});
	}

	private void showOculusReinstallDlcLevelMessage(DLC dlc, int levelIndex)
	{
		string arg = ((dlc == DLC.Dieselpunk) ? "Steampunk" : dlc.ToString());
		string.Format("{0} \"{1} {2}\"", Localization.lookupInDictionary("Menu_Oculus_ReinstallLevel"), arg, levelIndex);
		Localization.lookupInDictionary("Menu_Oculus_ReinstallLevel_Description");
	}

	private void reinstallOculusDlcLevel(DLC dlc, int levelIndex)
	{
		if (UnityEngine.Application.internetReachability == NetworkReachability.NotReachable)
		{
			showInternetNotAvailableMessage("Menu_Oculus_ReinstallLevel_NoInternet");
			return;
		}
		VR.oculusBoughtDLCsWhoseDownloadWasDismissed.Add(dlc);
		deleteOculusDlcLevelFile(dlc, levelIndex);
		downloadOculusDlc(dlc);
	}

	private void deleteOculusDlcLevelFile(DLC dlc, int levelIndex)
	{
		string text = Path.Combine("/sdcard/Android/obb/com.PineStudio.EscapeSimulator", $"{dlc.ToString().ToLower()}{levelIndex}");
		if (File.Exists(text))
		{
			UnityEngine.Debug.Log(string.Format("[{0}] Deleting {1} DLC level at path '{2}'...", "deleteOculusDlcLevelFile", dlc, text));
			File.Delete(text);
		}
	}

	private void launchOculusDlcPurchaseFlow(DLC dlc)
	{
		UnityEngine.Debug.Log(string.Format("[{0}] Launching Oculus checkout flow for DLC '{1}'", "launchOculusDlcPurchaseFlow", dlc));
		if (!VR.oculusAvailableDLCs.Contains(dlc))
		{
			UnityEngine.Debug.LogWarning(string.Format("[{0}] Oculus IAP not supported for DLC '{1}'", "launchOculusDlcPurchaseFlow", dlc));
		}
		else if (VR.oculusBoughtDLCs.Contains(dlc))
		{
			showDownloadOculusDlcMessage(dlc);
		}
		else
		{
			IAP.LaunchCheckoutFlow(dlc.ToString()).OnComplete(handlePurchaseResult);
		}
		static void handlePurchaseResult(Message<Purchase> message)
		{
			if (message.IsError)
			{
				UnityEngine.Debug.LogError("[launchOculusDlcPurchaseFlow] Oculus purchase failed: " + message.GetError().Message);
			}
			else
			{
				Purchase purchase = message.GetPurchase();
				UnityEngine.Debug.Log("[launchOculusDlcPurchaseFlow] Oculus purchase was successful for SKU '" + purchase.Sku + "'");
			}
		}
	}

	private void showDownloadOculusDlcMessage(DLC dlc)
	{
		Localization.lookupInDictionary("Menu_Oculus_DlcDownloadRequired_Title").Replace("[DLC]", dlc.ToString());
		Localization.lookupInDictionary("Menu_Oculus_DlcDownloadRequired_Description");
	}

	private void downloadOculusDlc(DLC dlc)
	{
		UnityEngine.Debug.Log(string.Format("[{0}] Started downloading of Oculus DLC '{1}'", "downloadOculusDlc", dlc));
		if (UnityEngine.Application.internetReachability == NetworkReachability.NotReachable)
		{
			VR.oculusBoughtDLCsWhoseDownloadWasDismissed.Add(dlc);
			showInternetNotAvailableMessage("Menu_Oculus_DlcDownloading_NoInternet");
			return;
		}
		List<AssetDetails> assetsToDownload = new List<AssetDetails>();
		int assetsToDownloadInitialCount = 0;
		HashSet<ulong> assetsDownloadedAndHandled = new HashSet<ulong>();
		ulong lastAssetId = 0uL;
		AssetDetails assetBeingDownloaded = null;
		AssetFile.GetList().OnComplete(getAssetDetailsList);
		void getAssetDetailsList(Message<AssetDetailsList> message)
		{
			if (message.IsError)
			{
				UnityEngine.Debug.LogError("[downloadOculusDlc] Failed to get asset details list: " + message.GetError().Message);
			}
			else
			{
				AssetDetailsList assetDetailsList = message.GetAssetDetailsList();
				string version = UnityEngine.Application.version;
				UnityEngine.Debug.Log(string.Format("[{0}] Fetched all available Oculus assets (count: {1}).", "downloadOculusDlc", assetDetailsList.Count));
				foreach (AssetDetails item in assetDetailsList)
				{
					bool flag = item.DownloadStatus == "available";
					string fileName = Path.GetFileName(item.Filepath);
					bool flag2 = fileName.Contains(dlc.ToString().ToLower());
					UnityEngine.Debug.Log(string.Format("[{0}] Asset {1} | File: {2} (name: {3}) | ", "downloadOculusDlc", item.AssetId, item.Filepath, fileName) + "Type: " + item.AssetType + " | Download: " + item.DownloadStatus + " | IAP: " + item.IapStatus + " | " + $"Is available: {flag} | Is DLC file: {flag2} | Metadata: {item.Metadata}");
					if (item.Metadata != version)
					{
						UnityEngine.Debug.LogError(string.Format("[{0}] Asset {1} metadata '{2}' does not match current build version '{3}'.", "downloadOculusDlc", item.AssetId, item.Metadata, version));
					}
					else if (flag && flag2)
					{
						assetsToDownload.Add(item);
						UnityEngine.Debug.Log(string.Format("[{0}] Added asset {1} ({2}) to download queue.", "downloadOculusDlc", item.AssetId, item.Filepath));
					}
				}
				if (assetsToDownload.Count != 0)
				{
					assetsToDownload.Sort((AssetDetails a1, AssetDetails a2) => string.Compare(Path.GetFileName(a1.Filepath), Path.GetFileName(a2.Filepath), StringComparison.Ordinal));
					List<AssetDetails> list = assetsToDownload;
					lastAssetId = list[list.Count - 1].AssetId;
					assetsToDownloadInitialCount = assetsToDownload.Count;
					UnityEngine.Debug.Log(string.Format("[{0}] Started downloading {1} '{2}' DLC assets...", "downloadOculusDlc", assetsToDownload.Count, dlc));
					AssetFile.SetDownloadUpdateNotificationCallback(handleAssetDownloadUpdate);
					startDownloadingNextFile();
				}
			}
		}
		string getPrettyFileName(AssetDetails levelAsset)
		{
			if (levelAsset == null)
			{
				return string.Empty;
			}
			string fileName = Path.GetFileName(levelAsset.Filepath);
			if (!int.TryParse(fileName[fileName.Length - 1].ToString(), out var result))
			{
				return fileName;
			}
			if (dlc != DLC.Dieselpunk)
			{
				return $"{dlc} #{result}";
			}
			return $"Steampunk #{result}";
		}
		void handleAssetDownloadComplete(ulong assetId)
		{
			if (assetsDownloadedAndHandled.Add(assetId))
			{
				UnityEngine.Debug.Log(string.Format("[{0}] Finished downloading asset {1}.", "handleAssetDownloadComplete", assetId));
				if (assetId != lastAssetId)
				{
					startDownloadingNextFile();
				}
			}
		}
		void handleAssetDownloadResult(Message<AssetFileDownloadResult> message)
		{
			AssetFileDownloadResult assetFileDownloadResult = message.GetAssetFileDownloadResult();
			if (message.IsError)
			{
				if (assetFileDownloadResult != null)
				{
					handleAssetDownloadComplete(assetFileDownloadResult.AssetId);
				}
				UnityEngine.Debug.LogError("[handleAssetDownloadResult] Failed to download asset: " + message.GetError().Message);
			}
			else
			{
				UnityEngine.Debug.Log(string.Format("[{0}] Successfully started downloading asset '{1}' (ID: {2}).", "handleAssetDownloadResult", assetFileDownloadResult.Filepath, assetFileDownloadResult.AssetId));
			}
		}
		void handleAssetDownloadUpdate(Message<AssetFileDownloadUpdate> message)
		{
			if (message.IsError)
			{
				UnityEngine.Debug.LogError("[handleAssetDownloadUpdate] Failed to download asset: " + message.GetError().Message);
			}
			else
			{
				AssetFileDownloadUpdate assetFileDownloadUpdate = message.GetAssetFileDownloadUpdate();
				int num = (int)((float)assetFileDownloadUpdate.BytesTransferred / (float)assetFileDownloadUpdate.BytesTotal * 100f);
				UnityEngine.Debug.Log(string.Format("[{0}] Downloading asset {1}: {2} / {3} bytes", "handleAssetDownloadUpdate", assetFileDownloadUpdate.AssetId, assetFileDownloadUpdate.BytesTransferred, assetFileDownloadUpdate.BytesTotal));
				VisualControl visualControl = new VisualControl("", ControllerButtonActionType.None, "", VisualControl.ControlType.Gear, VisualControl.Position.BotCenter);
				menuUI.OptionDialog.showNoTranslate(Localization.lookupInDictionary("Menu_Oculus_DlcDownloading_Title"), $"[{assetsToDownloadInitialCount - assetsToDownload.Count}/{assetsToDownloadInitialCount}] {getPrettyFileName(assetBeingDownloaded)}: {num}%", onOptionDialogClick, visualControl);
				if (assetFileDownloadUpdate.Completed)
				{
					handleAssetDownloadComplete(assetFileDownloadUpdate.AssetId);
				}
			}
		}
		void startDownloadingNextFile()
		{
			if (assetsToDownload.Count != 0)
			{
				assetBeingDownloaded = assetsToDownload[0];
				assetsToDownload.RemoveAt(0);
				new VisualControl("", ControllerButtonActionType.None, "", VisualControl.ControlType.Gear, VisualControl.Position.BotLeft);
				UnityEngine.Debug.Log("Potential issue with dialog option");
				AssetFile.DownloadById(assetBeingDownloaded.AssetId).OnComplete(handleAssetDownloadResult);
			}
		}
	}

	private void openShouldLoadLevelDialog(string levelId)
	{
		if (!isLoadingLevel && !levelProvider.getLevelItem(levelId).isLocked)
		{
			bool flag = Game.isSaveFileCompatibleWithLevel(Game.getLatestSaveForLevel(levelId), levelId);
			if (!Game.isSaveSystemEnabledFor(levelId) || !flag)
			{
				startSceneCheck(levelId, shouldLoadLatestSave: false);
				return;
			}
			loadLevelId = levelId;
			VisualControl visualControl = new VisualControl("loadLevel", ControllerButtonActionType.UIConfirmPrimary, "%YesLoad%", VisualControl.ControlType.Button, VisualControl.Position.BotCenter);
			visualControl.addKeybindAction(KeyBindingAction.Chat, game.menuOptions);
			VisualControl visualControl2 = new VisualControl("startNewLevel", ControllerButtonActionType.UIExtra1, "%NoStartNew%", VisualControl.ControlType.Button, VisualControl.Position.BotCenter);
			visualControl2.addKeybindAction(KeyBindingAction.ExamineInventory, game.menuOptions);
			VisualControl visualControl3 = new VisualControl("", ControllerButtonActionType.UIBack, "%cancel%", VisualControl.ControlType.Button, VisualControl.Position.BotCenter);
			visualControl3.addEscape();
			menuUI.OptionDialog.show(RoomDatabase.roomNameToLocalizationKey(levelId), "ContinueDetails", onOptionDialogClick, visualControl, visualControl2, visualControl3);
		}
	}

	private void startSceneCheck(string levelToCheck, bool shouldLoadLatestSave)
	{
		if (session.isHost(session.localPlayerId))
		{
			Game.SceneCheckPostEvent postEvent = ((!shouldLoadLatestSave) ? Game.SceneCheckPostEvent.LoadScene : Game.SceneCheckPostEvent.LoadSceneAndSave);
			game.startSceneCheck(levelToCheck, postEvent, session.localPlayerId);
		}
		else
		{
			game.requestLoadSceneFromHost(levelToCheck, shouldLoadLatestSave);
		}
	}

	private LevelPicker getActiveLevelPicker()
	{
		LevelPicker result = null;
		if (menuUI.Rooms.gameObject.activeInHierarchy)
		{
			result = lpRooms;
		}
		if (menuUI.Darkest.gameObject.activeInHierarchy)
		{
			result = lpDarkest;
		}
		return result;
	}

	private LevelPickerType getLevelPickerType(LevelPicker lp)
	{
		if (lp == null)
		{
			return LevelPickerType.None;
		}
		if (lp != lpRooms)
		{
			return LevelPickerType.Darkest;
		}
		return LevelPickerType.Rooms;
	}

	private LevelPicker getLevelPicker(LevelPickerType type)
	{
		return type switch
		{
			LevelPickerType.Rooms => lpRooms, 
			LevelPickerType.Darkest => lpDarkest, 
			_ => null, 
		};
	}

	public void applyCurrentTheme(bool fromInit = false)
	{
		if (fromInit)
		{
			handleYearlyEventTheme();
		}
		Themes theme = getTheme();
		ThemeingObject[] componentsInChildren = menuUI.transform.GetComponentsInChildren<ThemeingObject>(includeInactive: true);
		foreach (ThemeingObject themeingObject in componentsInChildren)
		{
			theme.theme(themeingObject.gameObject);
		}
		lpRooms.setShouldSyncVisuals();
		lpDarkest.setShouldSyncVisuals();
		game.menuOptions.syncTabVisuals(fromInit);
	}

	private void handleYearlyEventTheme()
	{
		DateTime now = DateTime.Now;
		Themes.ColorScheme? colorScheme = null;
		bool flag;
		if (now.Month == 10)
		{
			int day = now.Day;
			if ((uint)(day - 30) <= 1u)
			{
				flag = true;
				goto IL_0032;
			}
		}
		flag = false;
		goto IL_0032;
		IL_0032:
		bool flag2 = flag;
		if (now.Month == 11)
		{
			int day = now.Day;
			if ((uint)(day - 1) <= 1u)
			{
				flag = true;
				goto IL_0058;
			}
		}
		flag = false;
		goto IL_0058;
		IL_0058:
		bool flag3 = flag;
		if (flag2 || flag3)
		{
			colorScheme = Themes.ColorScheme.Dark;
		}
		bool num = now.Month == 12 && now.Day >= 18;
		bool flag4 = now.Month == 1 && now.Day < 8;
		if (num || flag4)
		{
			colorScheme = Themes.ColorScheme.Dark;
		}
		PlayerSave.Settings settings = PlayerSave.getSettings();
		PlayerSave.YearlyEventTheme yearlyEventTheme = settings.yearlyEventTheme;
		if (colorScheme.HasValue)
		{
			if (yearlyEventTheme.isActive)
			{
				if (yearlyEventTheme.themeToSetDuringEvent != colorScheme.Value)
				{
					UnityEngine.Debug.Log(string.Format("[{0}] Event changed from '{1}' to '{2}'.", "handleYearlyEventTheme", yearlyEventTheme.themeToSetDuringEvent, colorScheme));
					yearlyEventTheme.themeToSetDuringEvent = colorScheme.Value;
					settings.theme = colorScheme.Value;
					PlayerSave.flush();
				}
			}
			else
			{
				UnityEngine.Debug.Log(string.Format("[{0}] Event '{1}' started; overriding currently set '{2}' theme.", "handleYearlyEventTheme", colorScheme, settings.theme));
				yearlyEventTheme.themeToSetDuringEvent = colorScheme.Value;
				yearlyEventTheme.themeToSetAfterEventEnds = settings.theme;
				settings.theme = colorScheme.Value;
				yearlyEventTheme.isActive = true;
				PlayerSave.flush();
			}
		}
		else if (yearlyEventTheme.isActive)
		{
			UnityEngine.Debug.Log(string.Format("[{0}] Event '{1}' ended; back to '{2}' theme.", "handleYearlyEventTheme", yearlyEventTheme.themeToSetDuringEvent, yearlyEventTheme.themeToSetAfterEventEnds));
			settings.theme = yearlyEventTheme.themeToSetAfterEventEnds;
			yearlyEventTheme.isActive = false;
			PlayerSave.flush();
		}
	}

	public static Themes getTheme()
	{
		return Themes.get(PlayerSave.getSettings().theme);
	}

	private static bool extractSteamIdFromURL(string url, out uint steamId)
	{
		steamId = 0u;
		Match match = Regex.Match(url, "store\\.steampowered\\.com/app/(\\d+)");
		if (match.Success && uint.TryParse(match.Groups[1].Value, out steamId))
		{
			return true;
		}
		return false;
	}

	private static void addToCart(uint id)
	{
		SteamFriends.ActivateGameOverlayToStore(new AppId_t(id), EOverlayToStoreFlag.k_EOverlayToStoreFlag_AddToCartAndShow);
	}

	public static void openWebPage(string url)
	{
		UnityEngine.Application.OpenURL(url);
		UnityEngine.Debug.Log(url);
	}

	public static Texture2D loadTexture(string imagePath)
	{
		if (Is.Android && imagePath.Contains(UnityEngine.Application.streamingAssetsPath))
		{
			UnityEngine.Debug.LogError("[loadTexture] Cannot load from StreamingAssets on Android. Use 'loadTextureAsync' instead.");
			return null;
		}
		if (!File.Exists(imagePath))
		{
			return null;
		}
		Texture2D texture2D = new Texture2D(2, 2, TextureFormat.RGBA32, mipChain: true);
		texture2D.LoadImage(File.ReadAllBytes(imagePath), markNonReadable: true);
		return texture2D;
	}

	public static void loadTextureAsync(string imagePath, Action<Texture2D> onTextureLoaded)
	{
		Executor.executeCoroutine(loadTextureCoroutine(imagePath, onTextureLoaded));
		static IEnumerator loadTextureCoroutine(string uri, Action<Texture2D> action)
		{
			UnityWebRequest request = UnityWebRequestTexture.GetTexture(uri);
			UnityWebRequestAsyncOperation asyncOperation = request.SendWebRequest();
			yield return asyncOperation;
			if (asyncOperation.webRequest.result != UnityWebRequest.Result.Success)
			{
				action(null);
			}
			else
			{
				action(DownloadHandlerTexture.GetContent(request));
			}
		}
	}

	public static string constructLoadingHint(string levelId, bool isVR)
	{
		if (levelId == "roomeditor")
		{
			return Localization.lookupInDictionary($"loadingHintEditor{UnityEngine.Random.Range(0, 5)}");
		}
		int maxExclusive = 5;
		return Localization.lookupInDictionary($"es2_loadingHint{UnityEngine.Random.Range(0, maxExclusive)}");
	}

	public static RoomData getRoomData(string path)
	{
		RoomData result = null;
		try
		{
			result = JsonUtility.FromJson<RoomData>(File.ReadAllText(path));
		}
		catch (Exception message)
		{
			UnityEngine.Debug.Log(message);
		}
		return result;
	}

	public static void dirCopy(string sourceDirName, string destDirName, string baseDirName = "")
	{
		List<string> list = new List<string> { "id.bin", "tutorial.bin" };
		List<string> list2 = new List<string> { ".meta" };
		List<string> list3 = new List<string> { "backups" };
		List<string> list4 = new List<string> { "_CustomModels" };
		DirectoryInfo directoryInfo = new DirectoryInfo(sourceDirName);
		FileInfo[] files = directoryInfo.GetFiles();
		foreach (FileInfo file in files)
		{
			if (!list.Exists((string x) => file.Name.ToLower().Equals(x)) && !list2.Exists((string x) => file.Name.ToLower().Contains(x)))
			{
				file.CopyTo(Path.Combine(destDirName, file.Name), overwrite: false);
			}
		}
		DirectoryInfo[] directories = directoryInfo.GetDirectories();
		foreach (DirectoryInfo subdir in directories)
		{
			if ((string.IsNullOrEmpty(baseDirName) || !(sourceDirName == baseDirName) || list4.Exists((string x) => x.ToLower().Contains(subdir.Name.ToLower()))) && !list3.Exists((string x) => x.ToLower().Contains(subdir.Name.ToLower())))
			{
				string text = Path.Combine(destDirName, subdir.Name);
				Directory.CreateDirectory(text);
				dirCopy(subdir.FullName, text, baseDirName);
			}
		}
	}

	public static void writeIdToDisk(string levelDirPath, ulong id)
	{
		FileStream fileStream = File.OpenWrite(Path.Combine(levelDirPath, "Id.bin"));
		new BinaryWriter(fileStream).Write(id);
		fileStream.Close();
	}

	public static ulong readIdFromDisk(string levelDirPath)
	{
		string path = Path.Combine(levelDirPath, "Id.bin");
		if (!File.Exists(path))
		{
			return 0uL;
		}
		FileStream fileStream = File.Open(path, FileMode.Open);
		ulong result = new BinaryReader(fileStream).ReadUInt64();
		fileStream.Close();
		return result;
	}

	private static void connectSelectablesVertical(Selectable top, Selectable bot)
	{
		if (top == null)
		{
			Navigation navigation = bot.navigation;
			navigation.selectOnUp = null;
			bot.navigation = navigation;
			return;
		}
		if (bot == null)
		{
			Navigation navigation2 = top.navigation;
			navigation2.selectOnDown = null;
			top.navigation = navigation2;
			return;
		}
		Navigation navigation3 = top.navigation;
		navigation3.selectOnDown = bot;
		top.navigation = navigation3;
		Navigation navigation4 = bot.navigation;
		navigation4.selectOnUp = top;
		bot.navigation = navigation4;
	}

	public static void connectSelectablesVertical(GameObject topGO, GameObject botGO)
	{
		Selectable top = ((topGO == null) ? null : topGO.GetComponent<Selectable>());
		Selectable bot = ((botGO == null) ? null : botGO.GetComponent<Selectable>());
		connectSelectablesVertical(top, bot);
	}

	private static void connectSelectablesHorizontal(Selectable left, Selectable right)
	{
		Navigation navigation = left.navigation;
		navigation.selectOnRight = right;
		left.navigation = navigation;
		Navigation navigation2 = right.navigation;
		navigation2.selectOnLeft = left;
		right.navigation = navigation2;
	}

	public static void connectSelectablesHorizontal(GameObject leftGO, GameObject rightGO)
	{
		Selectable component = leftGO.GetComponent<Selectable>();
		Selectable component2 = rightGO.GetComponent<Selectable>();
		connectSelectablesHorizontal(component, component2);
	}

	private bool hasDLC(string dlcId)
	{
		return hasDLC(getDLCFromId(dlcId));
	}

	private bool hasDLC(DLC dlc)
	{
		if (dlc == DLC.None)
		{
			return true;
		}
		if (Is.Demo)
		{
			return false;
		}
		if (Is.iOS)
		{
			return true;
		}
		if (AssetBundleLoader.useAssetBundles)
		{
			return dlcInstalled[(int)dlc];
		}
		return true;
	}

	private DLC getDLCFromId(string dlcId)
	{
		if (!Enum.TryParse(typeof(DLC), dlcId, out var result))
		{
			return DLC.None;
		}
		return (DLC)result;
	}

	public AppId_t getDlcSteamId(DLC dlc)
	{
		return dlc switch
		{
			DLC.Dieselpunk => new AppId_t(1942100u), 
			DLC.Western => new AppId_t(2175260u), 
			DLC.Magic => new AppId_t(2419810u), 
			DLC.Portal => new AppId_t(2000170u), 
			DLC.AmongUs => new AppId_t(2644870u), 
			DLC.PowerWash => new AppId_t(2879620u), 
			DLC.Supporter => new AppId_t(2843390u), 
			DLC.Talos => new AppId_t(3193660u), 
			DLC.Mayan => new AppId_t(2644890u), 
			_ => AppId_t.Invalid, 
		};
	}

	public static DLC findDlcFromName(string sceneName)
	{
		if (string.IsNullOrEmpty(sceneName))
		{
			return DLC.None;
		}
		string text = sceneName.ToLower();
		foreach (DLC value in Enum.GetValues(typeof(DLC)))
		{
			if (value != DLC.Supporter && value != DLC.None && text.Contains(value.ToString().ToLower()))
			{
				return value;
			}
		}
		return DLC.None;
	}

	private void getLevelAndIndexFromSaveId(string saveId, out string name, out char index)
	{
		name = saveId.Substring(0, saveId.Length - 1);
		index = saveId[saveId.Length - 1];
	}

	public static List<DLC> getAllDlcs()
	{
		if (allDlcsCache == null)
		{
			allDlcsCache = new List<DLC>();
			Array values = Enum.GetValues(typeof(DLC));
			for (int i = 0; i < values.Length; i++)
			{
				allDlcsCache.Add((DLC)values.GetValue(i));
			}
		}
		return allDlcsCache;
	}

	private void initAudio()
	{
		PlayerSave.Settings settings = PlayerSave.getSettings();
		if (!settings.isMenuMusicVolumeSetup)
		{
			UnityEngine.Debug.Log(string.Format("[{0}] Menu music volume set to music volume value of {1}.", "initAudio", settings.musicVolume));
			settings.menuMusicVolume = settings.musicVolume;
			settings.isMenuMusicVolumeSetup = true;
			PlayerSave.flush();
		}
		PlayerSave.setAllVolumes();
		PineFmod.lockChannelGroup(PineFmod.getBus("bus:/Voice"));
		PineFmod.flushCommands();
	}

	public void fillHasDlcInstalled()
	{
		for (int i = 0; i < 0; i++)
		{
			DLC dLC = (DLC)i;
			bool flag = hasAllDlcLevelsInstalled(dLC);
			dlcInstalled[i] = flag && SteamApps.BIsDlcInstalled(getDlcSteamId(dLC));
			if (!dlcToHideIfNotInstalled.Contains(dLC))
			{
				UnityEngine.Debug.Log(dLC.ToString() + " Available: " + dlcInstalled[i]);
			}
		}
	}

	private bool hasAllDlcLevelsInstalled(DLC dlc, bool shouldLog = true)
	{
		string path = (Is.Android ? "/sdcard/Android/obb/com.PineStudio.EscapeSimulator" : dlcBasePath());
		int count = RoomDatabase.getAllRooms(dlc, Is.DebugBuild).Count;
		bool result = true;
		for (int i = 1; i <= count; i++)
		{
			string text = Path.Combine(path, $"{dlc.ToString().ToLower()}{i}");
			if (File.Exists(text))
			{
				if (shouldLog)
				{
					UnityEngine.Debug.Log(string.Format("[{0}] Found {1} DLC level: {2}", "hasAllDlcLevelsInstalled", dlc, text));
				}
				continue;
			}
			if (shouldLog && !dlcToHideIfNotInstalled.Contains(dlc))
			{
				UnityEngine.Debug.Log(string.Format("[{0}] Missing {1} DLC level: {2}", "hasAllDlcLevelsInstalled", dlc, text));
			}
			result = false;
		}
		return result;
	}

	public static string dlcBasePath()
	{
		string path = "DLC";
		string directoryName = Path.GetDirectoryName(UnityEngine.Application.dataPath);
		UnityEngine.Debug.Log("[DLC LOADING BUG]: dlcBasePath() return value = " + Path.Combine(directoryName, path));
		return Path.Combine(directoryName, path);
	}

	private void onPickerClick(LevelPicker lp)
	{
		if (session.isHost())
		{
			var (text, packId, _) = lp.getSelectedId();
			if (text == "ugcSteam")
			{
				List<string> currentLevels = lp.getCurrentLevels();
				session.send(new HostCustomLevelPickerPacket
				{
					packId = packId,
					levelIds = currentLevels
				}, allowSendInMessageResponse: true);
			}
		}
		session.send(new ChangeSelectedLevelInPickerPacket
		{
			selectedPicker = getLevelPickerType(lp),
			selectedLevel = lp.getSelectedIdRaw()
		});
		updatePickerButtons(lp, "");
	}

	private void updatePickerButtons(LevelPicker lp, string hintToShow)
	{
		var (tabId, packId, levelId) = lp.getSelectedId();
		lp.ui.Frame.visualController.removeAll();
		if (ESUGC.current.isCustomLevel(levelId))
		{
			updateCustomRoomButtons();
		}
		else
		{
			updateRegularRoomButtons();
		}
		int num = ((!((float)Screen.width / (float)Screen.height < 1.8f)) ? 1 : 0);
		menuUI.Rooms.matchWidthOrHeight = num;
		menuUI.Darkest.matchWidthOrHeight = num;
		void updateCustomRoomButtons()
		{
			CustomRoomState customRoomState = ESUGC.current.customRoomState(levelId);
			List<VisualControl> list = new List<VisualControl>();
			switch (customRoomState)
			{
			case CustomRoomState.Installed:
			{
				VisualControl visualControl = new VisualControl("play", ControllerButtonActionType.UIConfirmPrimary, "%play%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
				visualControl.addKeybindAction(KeyBindingAction.Chat, game.menuOptions);
				list.Add(visualControl);
				VisualControl item = new VisualControl("copy", ControllerButtonActionType.UIZoomin, "%copy%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
				list.Add(item);
				list.Add(new VisualControl("uninstall", ControllerButtonActionType.UIExtra1, "%Delete%", VisualControl.ControlType.Button, VisualControl.Position.BotRight));
				break;
			}
			case CustomRoomState.NotInstalled:
				list.Add(new VisualControl("install", ControllerButtonActionType.UIConfirmPrimary, "%notInstalledSimple%", VisualControl.ControlType.Button, VisualControl.Position.BotRight));
				break;
			case CustomRoomState.NeedsUpdate:
				list.Add(new VisualControl("install", ControllerButtonActionType.UIConfirmPrimary, "%publishUpdate%", VisualControl.ControlType.Button, VisualControl.Position.BotRight));
				break;
			case CustomRoomState.Installing:
				list.Add(new VisualControl("installing", ControllerButtonActionType.UIConfirmPrimary, "%installing%", VisualControl.ControlType.Text, VisualControl.Position.BotRight));
				break;
			}
			if (packId == "advancedSearch")
			{
				list.Add(new VisualControl("filter", ControllerButtonActionType.UIExtra2, "%Filters%", VisualControl.ControlType.Button, VisualControl.Position.BotRight));
			}
			VisualControl visualControl2 = new VisualControl("back", ControllerButtonActionType.UIBack, "%back%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
			visualControl2.addEscape();
			list.Add(visualControl2);
			lp.ui.Frame.addOrUpdateControl(list);
		}
		void updateRegularRoomButtons()
		{
			List<VisualControl> list = new List<VisualControl>();
			VisualControl visualControl = null;
			LevelProvider.LevelItem levelItem = levelProvider.getLevelItem(levelId);
			if (levelItem != null && !levelItem.isLocked)
			{
				visualControl = new VisualControl("play", ControllerButtonActionType.UIConfirmPrimary, "%play%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
				visualControl.addKeybindAction(KeyBindingAction.Chat, game.menuOptions);
			}
			VisualControl visualControl2 = new VisualControl("select", ControllerButtonActionType.UIConfirmPrimary, "%HoverAction_Select%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
			visualControl2.addKeybindAction(KeyBindingAction.Chat, game.menuOptions);
			if (levelId != "")
			{
				if (tabId == "dlc")
				{
					DLC dLCFromId = getDLCFromId(packId);
					if (!hasDLC(dLCFromId))
					{
						visualControl = null;
						list.Add(new VisualControl("getDlc", ControllerButtonActionType.UIConfirmPrimary, "%getDLC%", VisualControl.ControlType.Button, VisualControl.Position.BotRight));
					}
				}
				else if (Is.Demo && !RoomDatabase.getRoom(levelId).isInDemo)
				{
					visualControl = null;
				}
				visualControl2 = null;
			}
			else if (packId != "")
			{
				visualControl = null;
			}
			lp.ui.Frame.setHint(hintToShow);
			if (visualControl != null)
			{
				lp.ui.Frame.addOrUpdateControl(visualControl);
			}
			else
			{
				lp.ui.Frame.visualController.removeControl("play");
			}
			if (visualControl2 != null)
			{
				lp.ui.Frame.addOrUpdateControl(visualControl2);
			}
			else
			{
				lp.ui.Frame.visualController.removeControl("select");
			}
			if (packId == "advancedSearch")
			{
				list.Add(new VisualControl("filter", ControllerButtonActionType.UIExtra2, "%Filters%", VisualControl.ControlType.Button, VisualControl.Position.BotRight));
			}
			VisualControl visualControl3 = new VisualControl("back", ControllerButtonActionType.UIBack, "%back%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
			visualControl3.addEscape();
			list.Add(visualControl3);
			lp.ui.Frame.addOrUpdateControl(list);
		}
	}

	private void doUnfade(Action onUnfade)
	{
		menuUI.Fade.alpha = 1f;
		PineTweenSystemEnableNoHandles pineTweenSystemEnableNoHandles = tweener;
		GameObject obj = menuUI.Fade.gameObject;
		float? alphaTo = 0f;
		pineTweenSystemEnableNoHandles.tween(obj, 0.25f, 0f, deactivate: true, activate: false, destroy: false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, alphaTo).onComplete = delegate
		{
			onUnfade();
		};
	}

	public void doFade(Action onFade)
	{
		PineTweenSystemEnableNoHandles pineTweenSystemEnableNoHandles = tweener;
		GameObject obj = menuUI.Fade.gameObject;
		float? alphaTo = 1f;
		pineTweenSystemEnableNoHandles.tween(obj, 0.25f, 0f, deactivate: false, activate: true, destroy: false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, alphaTo).onComplete = delegate
		{
			onFade();
			PineTweenSystemEnableNoHandles pineTweenSystemEnableNoHandles2 = tweener;
			GameObject obj2 = menuUI.Fade.gameObject;
			float? alphaTo2 = 0f;
			pineTweenSystemEnableNoHandles2.tween(obj2, 0.25f, 0f, deactivate: true, activate: false, destroy: false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, alphaTo2);
		};
	}

	private void addLoadingReason(LoadingReason loadingReason)
	{
		if (loadingReasons.Contains(loadingReason))
		{
			UnityEngine.Debug.Log("[REASON] Cannot add same loading reason twice. " + loadingReason);
			return;
		}
		UnityEngine.Debug.Log("[REASON] add" + loadingReason);
		Controller.selectSelectable(null, EventSystem.current);
		loadingReasons.Add(loadingReason);
	}

	private void removeLoadingReason(LoadingReason loadingReason)
	{
		UnityEngine.Debug.Log("[REASON] remove" + loadingReason);
		loadingReasons.Remove(loadingReason);
	}

	private IEnumerator populateNews(string imageUrl, string text, string newsLink)
	{
		MenuNewsUI news = menuUI.ES2Cover.MenuNews;
		string text2 = Path.Combine(UnityEngine.Application.persistentDataPath, "NewsCache");
		if (!Directory.Exists(text2))
		{
			Directory.CreateDirectory(text2);
		}
		int newsId = UnityUtils.getPersistentHashCode(imageUrl);
		string cachedFilePath = Path.Combine(text2, newsId + ".png");
		bool cacheFile = true;
		if (File.Exists(cachedFilePath))
		{
			imageUrl = "file://" + cachedFilePath;
			cacheFile = false;
		}
		using UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);
		yield return www.SendWebRequest();
		int num = 0;
		if (www.result != UnityWebRequest.Result.Success)
		{
			UnityEngine.Debug.Log("Couldn't download news image from " + imageUrl);
			news.MenuButton.gameObject.SetActive(value: false);
		}
		else
		{
			Texture2D content = DownloadHandlerTexture.GetContent(www);
			news.MenuButton_MainImage.sprite = Sprite.Create(content, new Rect(0f, 0f, content.width, content.height), new Vector2(0.5f, 0.5f));
			news.MenuButton.gameObject.SetActive(value: true);
			newsTexts = Localization.parseYaml(text);
			news.MenuButton_MessageText.text = newsTexts[Localization.allLanguages[PlayerSave.getSettings().language].systemLanguage.ToString()];
			this.newsLink = newsLink;
			if (PlayerSave.getProgress().viewedNews.Contains(newsId))
			{
				news.MenuButton_ExclamationPoint.gameObject.SetActive(value: false);
			}
			else
			{
				news.MenuButton_ExclamationPoint.gameObject.SetActive(value: true);
				PlayerSave.getProgress().viewedNews.Add(newsId);
				PlayerSave.flush();
			}
			if (cacheFile)
			{
				File.WriteAllBytes(cachedFilePath, news.MenuButton_MainImage.sprite.texture.EncodeToPNG());
			}
			num++;
		}
		if (num > 0)
		{
			float num2 = (isVR() ? 1.3f : 1f);
			PineTweenSystemEnableNoHandles pineTweenSystemEnableNoHandles = tweener;
			GameObject obj = news.gameObject;
			float? alphaTo = 1f;
			Vector3? scaleFrom = Vector3.one * 0.95f * num2;
			Vector3? scaleTo = Vector3.one * num2;
			pineTweenSystemEnableNoHandles.tween(obj, 1f, 0f, deactivate: false, activate: false, destroy: false, null, null, null, null, null, null, null, null, null, null, null, null, scaleFrom, scaleTo, alphaTo, null, null, null, null, null, Interpolation.SmootherStep);
		}
	}

	private IEnumerator populateNewsHardcoded()
	{
		yield return new WaitForSeconds(3f);
		MenuNewsUI menuNews = menuUI.ES2Cover.MenuNews;
		PineTweenSystemEnableNoHandles pineTweenSystemEnableNoHandles = tweener;
		GameObject obj = menuNews.gameObject;
		float? alphaTo = 1f;
		Vector3? scaleFrom = Vector3.one * 0.95f;
		Vector3? scaleTo = Vector3.one;
		pineTweenSystemEnableNoHandles.tween(obj, 1f, 0f, deactivate: false, activate: false, destroy: false, null, null, null, null, null, null, null, null, null, null, null, null, scaleFrom, scaleTo, alphaTo, null, null, null, null, null, Interpolation.SmootherStep);
	}

	private void setupVersion()
	{
		string fullGameVersion = Version.fullGameVersion;
		fullGameVersion = ((!Is.dev()) ? (fullGameVersion + " (Rel)") : (fullGameVersion + " (Dev)"));
		menuUI.ES2Cover.Logo_Version_VersionLbl.text = fullGameVersion;
	}

	private void onVersionClick()
	{
		Text version = menuUI.ES2Cover.Logo_Version_VersionLbl;
		tweener.destroyTweens(version.gameObject, performEvents: false, performFinalUpdate: false);
		if (version.text == Version.fullGameVersion)
		{
			PineTweenSystemEnableNoHandles pineTweenSystemEnableNoHandles = tweener;
			GameObject obj = version.gameObject;
			Color? colorTo = Color.clear;
			pineTweenSystemEnableNoHandles.tween(obj, 0.2f, 0f, deactivate: false, activate: false, destroy: false, null, null, colorTo).onComplete = delegate
			{
				getCurrentVersionBuildTime(delegate(string buildDateTime)
				{
					version.text = buildDateTime;
				});
				PineTweenSystemEnableNoHandles pineTweenSystemEnableNoHandles3 = tweener;
				GameObject obj3 = version.gameObject;
				Color? colorTo2 = getTheme().textColors.secondary;
				pineTweenSystemEnableNoHandles3.tween(obj3, 0.2f, 0f, deactivate: false, activate: false, destroy: false, null, null, colorTo2);
			};
		}
		else
		{
			PineTweenSystemEnableNoHandles pineTweenSystemEnableNoHandles2 = tweener;
			GameObject obj2 = version.gameObject;
			Color? colorTo = Color.clear;
			pineTweenSystemEnableNoHandles2.tween(obj2, 0.2f, 0f, deactivate: false, activate: false, destroy: false, null, null, colorTo).onComplete = delegate
			{
				version.text = Version.fullGameVersion;
				PineTweenSystemEnableNoHandles pineTweenSystemEnableNoHandles3 = tweener;
				GameObject obj3 = version.gameObject;
				Color? colorTo2 = getTheme().textColors.secondary;
				pineTweenSystemEnableNoHandles3.tween(obj3, 0.2f, 0f, deactivate: false, activate: false, destroy: false, null, null, colorTo2);
			};
		}
		static void getCurrentVersionBuildTime(Action<string> onBuildDateTimeFetched)
		{
			UnityUtils.loadStreamingAssetsTextFileAsync("dateversion.txt", delegate(string versionAsString)
			{
				if (versionAsString == null || !long.TryParse(versionAsString, out var result))
				{
					onBuildDateTimeFetched?.Invoke(string.Empty);
				}
				else
				{
					onBuildDateTimeFetched?.Invoke(DateTimeOffset.FromUnixTimeSeconds(result).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"));
				}
			});
		}
	}

	private void openCopyDialog()
	{
		menuUI.OptionDialog.show("copyModalTitle", "copyModelMessage", onFrameClick, new VisualControl("copyWorkshopYes", ControllerButtonActionType.UIConfirmPrimary, "yes", VisualControl.ControlType.Button, VisualControl.Position.BotRight), new VisualControl("ignore", ControllerButtonActionType.UIBack, "no", VisualControl.ControlType.Button, VisualControl.Position.BotRight));
	}

	private void copyWorkshopRoom()
	{
		(string, string, string) selectedId = lpRooms.getSelectedId();
		string text = RoomEditor.createRoomDir(null);
		dirCopy(ESUGC.current.getPath(selectedId.Item3), text);
		string path = Path.Combine(text, "Room.room");
		RoomData roomData = JsonUtility.FromJson<RoomData>(File.ReadAllText(path));
		string newValue = roomData.name;
		roomData.name = "Copy of " + roomData.name;
		File.WriteAllText(path, JsonUtility.ToJson(roomData, prettyPrint: true));
		string title = Localization.lookupInDictionary("copySuccessTitle").Replace("<RoomName>", newValue);
		string message = Localization.lookupInDictionary("copySuccessMessage");
		menuUI.OptionDialog.showNoTranslate(title, message, null, new VisualControl("ignore", ControllerButtonActionType.UIBack, "ok", VisualControl.ControlType.Button, VisualControl.Position.BotRight));
	}

	private void openClientCode()
	{
		menuUI.ES2Cover.gameObject.SetActive(value: false);
		menuUI.ClientCode.gameObject.SetActive(value: true);
		menuUI.ClientCode.InputField.ActivateInputField();
		updateClientCode(rebuildUI: true);
	}

	private void joinRoomWithCode()
	{
		if (Is.Oculus && UnityEngine.Application.internetReachability == NetworkReachability.NotReachable)
		{
			closeAllCoop();
			showInternetNotAvailableMessage();
		}
		else if (menuUI.ClientCode.gameObject.activeInHierarchy && menuUI.ClientCode.InputField_Text.text.Length == 5)
		{
			menuUI.ClientCode.gameObject.SetActive(value: false);
			EventSystem.current.SetSelectedGameObject(null);
			addLoadingReason(LoadingReason.JoiningRoom);
			session.leaveLobby(finalUpdate: false);
			joinLobbyData = new JoinLobbyData
			{
				lobby = menuUI.ClientCode.InputField.text,
				cooldownTimeToJoinLobby = 1f
			};
		}
	}

	private void deleteRoomCodeLetter()
	{
		if (menuUI.ClientCode.gameObject.activeInHierarchy && menuUI.ClientCode.InputField_Text.text.Length > 0)
		{
			InputField inputField = menuUI.ClientCode.InputField;
			inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
		}
	}

	private void closeAllCoop()
	{
		UnityEngine.Debug.Log("close all coop");
		menuUI.ClientCode.gameObject.SetActive(value: false);
		menuUI.ES2Cover.gameObject.SetActive(value: true);
		game.createNewSession();
		syncMenuWithNetSession();
		session.startHost(LobbyType.SteamPine, ConnectionMode.SteamMessages);
		removeLoadingReason(LoadingReason.JoiningRoom);
		removeLoadingReason(LoadingReason.PreparingLobby);
		removeLoadingReason(LoadingReason.FindingRandomMatch);
		refreshAllInstalledRooms();
	}

	private void updateMenuNet()
	{
		if (joinLobbyData != null)
		{
			joinLobbyData.cooldownTimeToJoinLobby -= Time.deltaTime;
			if (joinLobbyData.cooldownTimeToJoinLobby <= 0f)
			{
				game.createNewSession();
				syncMenuWithNetSession();
				if (!string.IsNullOrEmpty(joinLobbyData.lobby))
				{
					session.startClient(joinLobbyData.lobby);
				}
				else
				{
					session.startClientSteamInvite(joinLobbyData.steamLobby.ToString());
				}
				joinLobbyData = null;
			}
		}
		if (session.netMode == NetMode.None)
		{
			lastFrameNumOfPlayers = -1;
			return;
		}
		string text = string.Empty;
		if (session.connectionMode == ConnectionMode.Photon)
		{
			if (session != null && session.photon != null && session.photon.voice != null && session.photon.voice.voiceRecorder != null)
			{
				session.photon.voice.voiceRecorder.VoiceDetection = true;
				session.photon.voice.voiceRecorder.VoiceDetectionThreshold = PlayerSave.getSettings().voiceActivityThreshold;
				session.photon.voice.voiceRecorder.VoiceDetectionThreshold = 0.01f;
			}
			string text2 = "-";
			if (session.photon != null && !string.IsNullOrWhiteSpace(session.photon.regionToken))
			{
				text2 = Localization.translate("%Menu_Region_" + PhotonService.convertTokenToSelector(session.photon.regionToken).ToString() + "%");
			}
			text = " (" + Localization.translate("%lobbyRegion%") + ": " + text2 + ")";
		}
		foreach (NetPlayerData player in session.players)
		{
			if (Is.Oculus)
			{
				Channel channel = ((session.lobbyType == LobbyType.PhotonPine) ? ((PineFmodAudioOut<float>)player.photonSpeaker.audioOutput).Channel : player.steamSpeaker.Channel);
				PineFmod.setMode(channel, PineFmodAudioOut<float>.DEFAULT_MODE | MODE._2D);
				float volume = (PlayerSave.getSettings().voiceChatEnabled ? (PlayerSave.getSettings().voiceChatVolume / 100f) : 0f);
				PineFmod.setVolume(channel, volume);
				VECTOR pos = new VECTOR
				{
					x = 0f,
					y = 0f,
					z = 0f
				};
				VECTOR vel = default(VECTOR);
				PineFmod.set3DAttributes(channel, ref pos, ref vel);
			}
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(Localization.translate($"%host_{session.connectionMode switch
		{
			ConnectionMode.SteamMessages => NetworkingProtocol.SteamDefault, 
			ConnectionMode.SteamLegacy => NetworkingProtocol.SteamLegacy, 
			ConnectionMode.Photon => NetworkingProtocol.Crossplatform, 
			_ => NetworkingProtocol.SteamDefault, 
		}}%") + text + " / ");
		stringBuilder.Append(Localization.translate($"%host_{session.gameDifficulty}%") + " / ");
		stringBuilder.Append((session.isRandomLobby ? Localization.translate("%lobbyRandom%") : Localization.translate("%lobbyPrivate%")) + " / ");
		stringBuilder.Append(Version.fullGameVersion ?? "");
		stringBuilder.ToString();
		if (session.netMode == NetMode.Host)
		{
			menuUI.HostMiniPanel.gameObject.SetActive(!string.IsNullOrEmpty(session.lobbyCode) && session.pressedHost && !game.menuHostOptions.gameObject.activeSelf);
			var (text3, dlcId, _) = lpRooms.getSelectedId();
			if (text3 == "dlc")
			{
				getDLCFromId(dlcId);
			}
			updateSceneLoad();
		}
		if (lastFrameNumOfPlayers != session.players.Count)
		{
			if (session.netMode == NetMode.Client && !session.isHostInLobby() && loadingReasons.Count == 0)
			{
				closeAllCoop();
			}
			lastFrameNumOfPlayers = session.players.Count;
		}
	}

	private void updateSceneLoad()
	{
		Game.SceneCheck currentSceneCheck = game.currentSceneCheck;
		if (currentSceneCheck == null || currentSceneCheck.invalidPlayers.Count <= 0)
		{
			return;
		}
		List<NetPlayerId> list = new List<NetPlayerId>(currentSceneCheck.invalidPlayers.Count);
		List<Game.CanLoadSceneResult> list2 = new List<Game.CanLoadSceneResult>(currentSceneCheck.invalidPlayers.Count);
		foreach (var invalidPlayer in currentSceneCheck.invalidPlayers)
		{
			list.Add(invalidPlayer.playerId);
			list2.Add(invalidPlayer.result);
		}
		if (currentSceneCheck.requestingPlayerId == session.localPlayerId)
		{
			showCantLoadSceneDialog(currentSceneCheck.sceneName, list, list2);
		}
		else
		{
			session.sendToSpecificPlayerFromHost(new CantLoadScenePacket
			{
				sceneName = currentSceneCheck.sceneName,
				invalidPlayerIds = list,
				invalidPlayerResults = list2
			}, currentSceneCheck.requestingPlayerId);
		}
		game.stopCurrentSceneCheck();
	}

	private void showCantLoadSceneDialog(string sceneName, List<NetPlayerId> invalidPlayerIds, List<Game.CanLoadSceneResult> invalidPlayerResults)
	{
		string text = "";
		text = ((!ESUGC.current.isCustomLevel(sceneName)) ? Localization.lookupInDictionary(RoomDatabase.roomNameToLocalizationKey(RoomDatabase.getRoom(sceneName).name)) : levelProvider.getLevelItem(sceneName).levelName);
		string title = Localization.lookupInDictionary("CantLoadScene_Title").Replace("[LevelName]", text);
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < invalidPlayerIds.Count; i++)
		{
			NetPlayerId playerId = invalidPlayerIds[i];
			string text2 = getCantLoadSceneLocalizationString(invalidPlayerResults[i]);
			text2 = text2.Replace("[PlayerName]", session.getUsername(playerId));
			text2 = text2.Replace("[LevelName]", text);
			stringBuilder.AppendLine(text2);
		}
		VisualControl visualControl = new VisualControl("back", ControllerButtonActionType.UIBack, "%back%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
		visualControl.addEscape();
		menuUI.OptionDialog.showNoTranslate(title, stringBuilder.ToString(), onOptionDialogClick, visualControl);
		static string getCantLoadSceneLocalizationString(Game.CanLoadSceneResult result)
		{
			return result switch
			{
				Game.CanLoadSceneResult.CannotLoadWrongVersion => Localization.lookupInDictionary("CantLoadScene_WrongVersion"), 
				Game.CanLoadSceneResult.CannotLoadDidntUnlockDarkest => Localization.lookupInDictionary("CantLoadScene_DidntUnlockDarkest"), 
				Game.CanLoadSceneResult.CannotLoadMissingDLC => Localization.lookupInDictionary("CantLoadScene_MissingDLC"), 
				Game.CanLoadSceneResult.CannotLoadNotInstalled => Localization.lookupInDictionary("CantLoadScene_NotInstalled"), 
				Game.CanLoadSceneResult.CannotLoadUGCFail => Localization.lookupInDictionary("CantLoadScene_UGCFail"), 
				Game.CanLoadSceneResult.CannotLoadNotInDemo => Localization.lookupInDictionary("CantLoadScene_NotInDemo"), 
				_ => "ERROR", 
			};
		}
	}

	private void updateClientCode(bool rebuildUI)
	{
		if (menuUI.ClientCode.gameObject.activeInHierarchy && (Controller.controllerModeChanged || rebuildUI))
		{
			menuUI.ClientCode.ControllerKeyboard.gameObject.SetActive(Controller.isActive() || isVR());
			if (isVR())
			{
				menuUI.ClientCode.ControllerKeyboard.usePointer = false;
				vrKeyboardDeleteButton.gameObject.SetActive(value: true);
				vrKeyboardBackButton.gameObject.SetActive(value: true);
				menuUI.ClientCode.ControllerKeyboard_VRHints.gameObject.SetActive(value: true);
			}
			updateController(forceChange: true);
			List<VisualControl> list = new List<VisualControl>();
			VisualControl visualControl = new VisualControl("join", ControllerButtonActionType.UIExtra1, "%client%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
			visualControl.addKeybindAction(KeyBindingAction.Chat, game.menuOptions);
			list.Add(visualControl);
			VisualControl visualControl2 = new VisualControl("back", ControllerButtonActionType.UIBack, "%back%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
			visualControl2.addEscape();
			list.Add(visualControl2);
			if (Controller.isActive())
			{
				list.Add(new VisualControl("deleteCode", ControllerButtonActionType.UIExtra2, "%Delete%", VisualControl.ControlType.Button, VisualControl.Position.BotRight));
			}
			menuUI.ClientCode.Frame.addOrUpdateControl(list);
		}
	}

	private void host(ConnectionMode connectionMode, GameDifficulty gameDifficulty = GameDifficulty.Normal, bool isRandomLobby = false)
	{
		LobbyType lobbyType = ((connectionMode == ConnectionMode.Photon) ? LobbyType.PhotonPine : LobbyType.SteamPine);
		session.startHost(lobbyType, connectionMode, gameDifficulty, isRandomLobby);
	}

	private void playRandomMatch()
	{
		addLoadingReason(LoadingReason.FindingRandomMatch);
		attemptToFindRandomLobby(ELobbyDistanceFilter.k_ELobbyDistanceFilterDefault, delegate(NetMode netMode)
		{
			removeLoadingReason(LoadingReason.FindingRandomMatch);
			if (netMode == NetMode.Host)
			{
				addLoadingReason(LoadingReason.PreparingLobby);
			}
			else
			{
				addLoadingReason(LoadingReason.JoiningRoom);
			}
		});
	}

	public void joinRoomByInvite(string connectionString)
	{
		UnityEngine.Debug.Log("joinRoomByInvite " + connectionString);
		string[] array = connectionString.Split(':');
		if (array.Length > 1)
		{
			bool flag = false;
			string text = array[1];
			if (array[0] == "photon")
			{
				session.startClient(text);
				flag = true;
			}
			else if (array[0] == "steam")
			{
				if (ulong.TryParse(text, out var result))
				{
					session.leaveLobby(finalUpdate: false);
					joinLobbyData = new JoinLobbyData
					{
						steamLobby = result,
						cooldownTimeToJoinLobby = 1f
					};
					flag = true;
				}
				else
				{
					UnityEngine.Debug.LogError("Failed to parse ulong from code: " + text);
				}
			}
			if (flag)
			{
				UnityEngine.Debug.Log("Joining with connection string: " + connectionString);
				lastFrameNumOfPlayers = 0;
				menuUI.ES2Cover.gameObject.SetActive(value: false);
				menuUI.ClientCode.gameObject.SetActive(value: false);
				menuUI.Customization.gameObject.gameObject.SetActive(value: false);
				menuUI.Rooms.gameObject.gameObject.SetActive(value: false);
				game.menuOptions.gameObject.SetActive(value: false);
				menuUI.Credits.exitCredits();
				menuUI.OptionDialog.gameObject.SetActive(value: false);
				menuUI.RoomInfoPopup.gameObject.SetActive(value: false);
				addLoadingReason(LoadingReason.JoiningRoom);
			}
			else
			{
				UnityEngine.Debug.Log("Can't start joining room with connection string: " + connectionString);
			}
		}
		else
		{
			UnityEngine.Debug.Log("Can't enter room with connection string: " + connectionString);
		}
	}

	private void attemptToFindRandomLobby(ELobbyDistanceFilter distanceFilter, Action<NetMode> onSearchComplete)
	{
		UnityEngine.Debug.Log($"Searching for a random lobby (distance filter: {distanceFilter})...");
		SteamMatchmaking.AddRequestLobbyListStringFilter("IsRandomLobby", "true", ELobbyComparison.k_ELobbyComparisonEqual);
		SteamMatchmaking.AddRequestLobbyListStringFilter("GameVersion", Version.baseGameVersion, ELobbyComparison.k_ELobbyComparisonEqual);
		SteamMatchmaking.AddRequestLobbyListDistanceFilter(distanceFilter);
		CallResult<LobbyMatchList_t> callResult = CallResult<LobbyMatchList_t>.Create(onLobbyListReceived);
		callResult.Set(SteamMatchmaking.RequestLobbyList());
		gcDontKill.Add(callResult);
		void onLobbyListReceived(LobbyMatchList_t lobbyListData, bool hasFailed)
		{
			if (hasFailed)
			{
				UnityEngine.Debug.LogError("Failed to get random lobbies...");
				onSearchComplete?.Invoke(NetMode.None);
			}
			else
			{
				int num = -1;
				int num2 = -1;
				for (int i = 0; i < lobbyListData.m_nLobbiesMatching; i++)
				{
					CSteamID lobbyByIndex = SteamMatchmaking.GetLobbyByIndex(i);
					if (!kickedFromLobbyIds.Contains((CSteamID)lobbyByIndex.m_SteamID))
					{
						int numLobbyMembers = SteamMatchmaking.GetNumLobbyMembers(lobbyByIndex);
						if (numLobbyMembers < 4 && numLobbyMembers > num2)
						{
							num2 = numLobbyMembers;
							num = i;
						}
					}
				}
				if (num == -1)
				{
					UnityEngine.Debug.Log($"Zero random lobbies found (distance filter: {distanceFilter}).");
					if (distanceFilter == ELobbyDistanceFilter.k_ELobbyDistanceFilterDefault)
					{
						attemptToFindRandomLobby(ELobbyDistanceFilter.k_ELobbyDistanceFilterFar, onSearchComplete);
					}
					else if (distanceFilter == ELobbyDistanceFilter.k_ELobbyDistanceFilterFar)
					{
						attemptToFindRandomLobby(ELobbyDistanceFilter.k_ELobbyDistanceFilterWorldwide, onSearchComplete);
					}
					else if (distanceFilter == ELobbyDistanceFilter.k_ELobbyDistanceFilterWorldwide)
					{
						UnityEngine.Debug.Log("Hosting a new random lobby...");
						host(ConnectionMode.SteamMessages, GameDifficulty.Normal, isRandomLobby: true);
						onSearchComplete?.Invoke(NetMode.Host);
					}
				}
				else
				{
					UnityEngine.Debug.Log($"Found {lobbyListData.m_nLobbiesMatching} lobbies. Joining lobby #{num} ({num2} players)...");
					session.startClientSteamInvite(SteamMatchmaking.GetLobbyByIndex(num).m_SteamID.ToString());
					onSearchComplete?.Invoke(NetMode.Client);
				}
			}
		}
	}

	private void syncMenuWithNetSession()
	{
		if (session == null)
		{
			UnityEngine.Debug.LogError("Session is somehow not initialized!");
			return;
		}
		session.menuCallbacks = new MenuCallbacks(this);
		lastFrameNumOfPlayers = 0;
		game.syncGameWithNetSession();
	}

	private void onChangeVoiceChat()
	{
		if (session != null)
		{
			session.toggleUseVoiceSystem(PlayerSave.getSettings().voiceChatEnabled);
		}
	}

	public void lobbyReady()
	{
		UnityEngine.Debug.Log("lobbyReady() " + session.lobbyCode);
		removeLoadingReason(LoadingReason.PreparingLobby);
	}

	public void onEnterLobby()
	{
		UnityEngine.Debug.Log($"onEnterLobby {session.lobbyType}");
		removeLoadingReason(LoadingReason.JoiningRoom);
		if (session.isHost())
		{
			transitionToLobby();
		}
		else
		{
			addLoadingReason(LoadingReason.ClientLoadingSave);
		}
	}

	public void onClientLoadedLevel()
	{
		removeLoadingReason(LoadingReason.ClientLoadingSave);
		transitionToLobby();
	}

	public void onCantEnterDetailsLobby(string code, string message)
	{
		UnityEngine.Debug.Log("Cant enter a lobby! " + code + " " + message);
		closeAllCoop();
		VisualControl visualControl = new VisualControl("close", ControllerButtonActionType.UIConfirmPrimary, "%close%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
		visualControl.addEscape();
		visualControl.addKeybindAction(KeyBindingAction.Chat, game.menuOptions);
		menuUI.OptionDialog.showNoTranslate(Localization.lookupInDictionary("coopCodeNotFoundTitle"), code + ": " + message, onOptionDialogClick, visualControl);
	}

	public void onCantEnterLobby()
	{
		UnityEngine.Debug.Log("Cant enter a lobby!");
		closeAllCoop();
		VisualControl visualControl = new VisualControl("coopNotFound", ControllerButtonActionType.UIConfirmPrimary, "%RoomEditor_OK%", VisualControl.ControlType.Button, VisualControl.Position.BotCenter);
		visualControl.addEscape(hidden: true);
		visualControl.addKeybindAction(KeyBindingAction.Chat, game.menuOptions);
		menuUI.OptionDialog.show("coopCodeNotFoundTitle", "coopCodeCantEnterDescription", onOptionDialogClick, visualControl);
	}

	public void onCantEnterSteamLobbyFromCrossplatform()
	{
		closeAllCoop();
		VisualControl visualControl = new VisualControl("hostProton", ControllerButtonActionType.UIConfirmPrimary, "%host%", VisualControl.ControlType.Button, VisualControl.Position.BotCenter);
		visualControl.addKeybindAction(KeyBindingAction.Chat, game.menuOptions);
		VisualControl visualControl2 = new VisualControl("", ControllerButtonActionType.UIBack, "%cancel%", VisualControl.ControlType.Button, VisualControl.Position.BotCenter);
		visualControl2.addEscape();
		menuUI.OptionDialog.show("Menu_CantEnterSteamLobbyFromCrossplatform_Title", "Menu_CantEnterSteamLobbyFromCrossplatform_Description", onOptionDialogClick, visualControl, visualControl2);
	}

	public void onFailedToFindLobby(string message)
	{
		string lobbyCode = session.lobbyCode;
		closeAllCoop();
		string title = Localization.lookupInDictionary("coopCodeNotFoundTitle");
		string message2 = Localization.lookupInDictionary("coopCodeNotFoundDescription").Replace("[]", lobbyCode).Replace("{}", message);
		VisualControl visualControl = new VisualControl("failedLobby", ControllerButtonActionType.UIConfirmPrimary, "%RoomEditor_OK%", VisualControl.ControlType.Button, VisualControl.Position.BotLeft);
		visualControl.addKeybindAction(KeyBindingAction.Chat, game.menuOptions);
		visualControl.addEscape(hidden: true);
		menuUI.OptionDialog.showNoTranslate(title, message2, onOptionDialogClick, visualControl);
	}

	public void onLobbyCodeChanged(string lobbyCode)
	{
		GUIUtility.systemCopyBuffer = lobbyCode;
	}

	public void onGotLobbyInvitation(string lobbyId)
	{
		joinRoomByInvite(lobbyId);
	}

	public void onLobbyMemberDataChanged()
	{
		lastFrameNumOfPlayers = 0;
	}

	public void onPlayerSynced(NetPlayerData player)
	{
		UnityEngine.Debug.Log("player synced: " + player.username);
		if (!session.isHost(session.localPlayerId))
		{
			return;
		}
		List<RoomDatabase.Room> allRooms = RoomDatabase.getAllRooms();
		List<string> list = new List<string>();
		foreach (RoomDatabase.Room item in allRooms)
		{
			if (!string.IsNullOrEmpty(Game.getLatestSaveForLevel(item.name)))
			{
				list.Add(item.name);
			}
		}
		session.sendToSpecificPlayer(new HostsAvaliableSavesPacket
		{
			levelsWithSaves = list
		}, player.id, allowSendInMessageResponse: true);
		var (text, packId, _) = lpRooms.getSelectedId();
		if (text == "ugcSteam")
		{
			List<string> currentLevels = lpRooms.getCurrentLevels();
			session.send(new HostCustomLevelPickerPacket
			{
				packId = packId,
				levelIds = currentLevels
			}, allowSendInMessageResponse: true);
		}
		session.sendToSpecificPlayer(new ChangeSelectedLevelInPickerPacket
		{
			selectedLevel = lpRooms.getSelectedIdRaw(),
			selectedPicker = LevelPickerType.Rooms
		}, player.id, allowSendInMessageResponse: true);
		session.sendToSpecificPlayer(new ChangeSelectedLevelInPickerPacket
		{
			selectedLevel = lpDarkest.getSelectedIdRaw(),
			selectedPicker = LevelPickerType.Darkest
		}, player.id, allowSendInMessageResponse: true);
	}

	private void onLobbyPacket(Packet packet)
	{
		Game.GamePlayerData gamePlayerData = game.netPlayers.Find((Game.GamePlayerData player) => player.id == packet.senderId);
		if (game.localPlayerData.id == packet.senderId)
		{
			gamePlayerData = game.localPlayerData;
		}
		if (gamePlayerData == null)
		{
			UnityEngine.Debug.LogError($"Lobby: Cannot process {packet.GetType()} as player with ID {packet.senderId} is not found.");
			return;
		}
		gamePlayerData.lastKeepAliveTimestamp = Time.time;
		game.isProcessingPacket = true;
		try
		{
			processLobbyPacket(packet);
		}
		catch (Exception exception)
		{
			UnityEngine.Debug.LogError($"Exception occurred while processing the following '{packet.GetType()}' lobby-packet:\n\n{packet}");
			UnityEngine.Debug.LogException(exception);
		}
		game.isProcessingPacket = false;
	}

	private void processLobbyPacket(Packet packet)
	{
		_ = packet.senderId;
		if (packet is KickedFromRandomLobbyPacket)
		{
			kickedFromLobbyIds.Add(session.currentLobbySteam);
			onKickedFromLobby();
		}
		else if (packet is HostsAvaliableSavesPacket hostsAvaliableSavesPacket)
		{
			hostsRoomsWithSave = hostsAvaliableSavesPacket.levelsWithSaves;
		}
		else if (packet is ChangeSelectedLevelInPickerPacket changeSelectedLevelInPickerPacket)
		{
			getLevelPicker(changeSelectedLevelInPickerPacket.selectedPicker).select(changeSelectedLevelInPickerPacket.selectedLevel);
		}
		else if (packet is CantLoadScenePacket cantLoadScenePacket)
		{
			showCantLoadSceneDialog(cantLoadScenePacket.sceneName, cantLoadScenePacket.invalidPlayerIds, cantLoadScenePacket.invalidPlayerResults);
		}
		else if (packet is HostCustomLevelPickerPacket hostCustomLevelPickerPacket)
		{
			HashSet<PublishedFileId_t> hashSet = new HashSet<PublishedFileId_t>();
			levelProvider.removeAllFromList("hostLevelList");
			foreach (string levelId in hostCustomLevelPickerPacket.levelIds)
			{
				if (ulong.TryParse(levelId, out var result))
				{
					if (!levelProvider.hasLevel(levelId))
					{
						hashSet.Add(new PublishedFileId_t(result));
					}
					levelProvider.addLevelToList("hostLevelList", levelId);
					levelProvider.requestData(levelId);
				}
			}
			if (hashSet.Count > 0)
			{
				PublishedFileId_t[] array = new PublishedFileId_t[hashSet.Count];
				hashSet.CopyTo(array);
				SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(SteamUGC.CreateQueryUGCDetailsRequest(array, (uint)array.Length));
				CallResult<SteamUGCQueryCompleted_t> callResult = CallResult<SteamUGCQueryCompleted_t>.Create(onDoneLevel);
				callResult.Set(hAPICall);
				gcDontKill.Add(callResult);
			}
		}
		else if (packet is RequestAdvancedSearchPacket requestAdvancedSearchPacket)
		{
			fireAdvancedSearchHost(requestAdvancedSearchPacket.tagsToInclude, requestAdvancedSearchPacket.tagsToExclude, requestAdvancedSearchPacket.sortIndex, requestAdvancedSearchPacket.daysIndex);
		}
		void onDoneLevel(SteamUGCQueryCompleted_t data, bool failure)
		{
			if (!(data.m_eResult != EResult.k_EResultOK || failure))
			{
				uint unNumResultsReturned = data.m_unNumResultsReturned;
				for (uint num = 0u; num < unNumResultsReturned; num++)
				{
					SteamUGC.GetQueryUGCResult(data.m_handle, num, out var pDetails);
					PublishedFileId_t nPublishedFileId = pDetails.m_nPublishedFileId;
					SteamUGC.GetQueryUGCPreviewURL(data.m_handle, num, out var pchURL, 5000u);
					bool value = PlayerSave.getFinishState(nPublishedFileId.m_PublishedFileId.ToString()) == PlayerSave.FinishState.Finished;
					levelProvider.modifyLevel(nPublishedFileId.ToString(), pDetails.m_rgchTitle, null, pchURL, -1, -1, value);
				}
				SteamUGC.ReleaseQueryUGCRequest(data.m_handle);
			}
		}
	}

	private void setupCrossplatformLevelPicker()
	{
		lpRooms.changeVisibility("ugcPine", visible: true);
		lpRooms.changeVisibility("ugcSteam", visible: false);
	}

	public void onMatchCreateFail(string code, string message)
	{
		closeAllCoop();
		if (!string.IsNullOrEmpty(code) || !string.IsNullOrEmpty(message))
		{
			VisualControl visualControl = new VisualControl("back", ControllerButtonActionType.UIBack, "%back%", VisualControl.ControlType.Button, VisualControl.Position.BotCenter);
			menuUI.OptionDialog.showNoTranslate(Localization.lookupInDictionary("coopCodeNotFoundTitle"), code + ": " + message, onOptionDialogClick, visualControl);
		}
	}

	public void onKickedFromLobby()
	{
		closeAllCoop();
		VisualControl visualControl = new VisualControl("", ControllerButtonActionType.UIConfirmPrimary, "%RoomEditor_OK%", VisualControl.ControlType.Button, VisualControl.Position.BotCenter);
		visualControl.addEscape(hidden: true);
		visualControl.addKeybindAction(KeyBindingAction.Chat, game.menuOptions);
		menuUI.OptionDialog.show("", "kickedFromLobby", onOptionDialogClick, visualControl);
	}

	private void refreshAllInstalledRooms()
	{
		ESUGC.current.refreshAllInstalledRooms(onLevelRefresh);
	}

	private void refreshSpecificInstalledRooms(params string[] roomIds)
	{
		ESUGC.current.refreshSpecificRooms(roomIds, onLevelRefresh);
	}

	private void onLevelRefresh(RefreshRoomData data)
	{
		PlayerSave.LevelState levelState = PlayerSave.ensureAndGetLevelState(data.id);
		bool value = levelState.finishState == PlayerSave.FinishState.Finished;
		bool value2 = levelState.finishState == PlayerSave.FinishState.Trophy;
		string listId = ((data.eSUGC is ESPineUGC) ? "installedPine" : "installedSteam");
		if (data.shouldBeDeleted)
		{
			levelProvider.removeLevelFromList(listId, data.id);
			data.eSUGC.uninstallRoom(data.id);
		}
		else
		{
			levelProvider.addLevelToList(listId, data.id);
			levelProvider.modifyLevel(data.id, data.roomTitle, null, data.imageUrl, -1, -1, dateFolderCreated: data.modified, finished: value, gotTrophy: value2);
			levelProvider.requestData(data.id);
		}
	}

	private void initMainMenuCollection(MainMenuCollection mainCollection)
	{
		HashSet<PublishedFileId_t> hashSet = new HashSet<PublishedFileId_t>();
		foreach (MenuCollection collection in mainCollection.collections)
		{
			WorkshopCollection workshopCollection = new WorkshopCollection();
			workshopCollection.collectionId = collection.id;
			workshopCollection.collectionName = collection.name;
			workshopCollection.rooms = new List<WorkshopRoomInfo>();
			workshopCollections.Add(workshopCollection);
			for (int i = 0; i < collection.rooms.Count; i++)
			{
				PublishedFileId_t item = new PublishedFileId_t(collection.rooms[i]);
				workshopCollection.roomIds.Add(item);
				hashSet.Add(item);
			}
		}
		PublishedFileId_t[] array = new PublishedFileId_t[hashSet.Count];
		hashSet.CopyTo(array);
		SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(SteamUGC.CreateQueryUGCDetailsRequest(array, (uint)array.Length));
		CallResult<SteamUGCQueryCompleted_t> callResult = CallResult<SteamUGCQueryCompleted_t>.Create(onDoneLevel);
		callResult.Set(hAPICall);
		gcDontKill.Add(callResult);
		void onDoneLevel(SteamUGCQueryCompleted_t data, bool failure)
		{
			if (!(data.m_eResult != EResult.k_EResultOK || failure))
			{
				foreach (MenuCollection collection2 in mainCollection.collections)
				{
					startPack(lpRooms, "ugcSteam", collection2.id, collection2.name);
				}
				uint unNumResultsReturned = data.m_unNumResultsReturned;
				for (uint num = 0u; num < unNumResultsReturned; num++)
				{
					SteamUGC.GetQueryUGCResult(data.m_handle, num, out var pDetails);
					PublishedFileId_t id = pDetails.m_nPublishedFileId;
					SteamUGC.GetQueryUGCPreviewURL(data.m_handle, num, out var pchURL, 5000u);
					workshopCollections.FindAll((WorkshopCollection x) => x.roomIds.Contains(id)).ForEach(delegate(WorkshopCollection x)
					{
						levelProvider.addLevelToList(x.collectionId, id.ToString());
					});
					bool value = PlayerSave.getFinishState(id.m_PublishedFileId.ToString()) == PlayerSave.FinishState.Finished;
					levelProvider.modifyLevel(id.ToString(), pDetails.m_rgchTitle, null, pchURL, -1, -1, value);
				}
				SteamUGC.ReleaseQueryUGCRequest(data.m_handle);
			}
		}
		static void startPack(LevelPicker lp, string tabId, string listId, string name)
		{
			lp.use(tabId);
			string id = tabId + "." + listId;
			lp.definePack(listId, listId, canUseSearch: true, canUseBrowse: false, null);
			LevelProvider.PackSize? packSize = LevelProvider.PackSize.CustomRoom;
			lp.modifyPack(id, name, null, null, null, null, null, null, null, null, null, packSize);
		}
	}

	private void initWorkshop()
	{
		if (Is.PineBackend || Is.Demo)
		{
			return;
		}
		advancedSearchFilters.minNumPlayersTagsList[0].isActive = true;
		List<Tags.Tag> maxNumPlayersTagsList = advancedSearchFilters.maxNumPlayersTagsList;
		maxNumPlayersTagsList[maxNumPlayersTagsList.Count - 1].isActive = true;
		advancedSearchFilters.playTimeTagsList[0].isActive = true;
		advancedSearchFilters.difficultyTagsList[0].isActive = true;
		advancedSearchFilters.languagesTagsList[0].isActive = true;
		advancedSearchFilters.themesTagsList[0].isActive = true;
		advancedSearchFilters.miscellaneousTagsList.ForEach(delegate(Tags.Tag x)
		{
			x.isAny = true;
		});
		advancedSearchFilters.pineTagsList.ForEach(delegate(Tags.Tag x)
		{
			x.isAny = true;
		});
		advancedSearchFilters.codeAddedTagsList.ForEach(delegate(Tags.Tag x)
		{
			x.isAny = true;
		});
		Transform parent = menuUI.TagPicker.SafeAreas_Modal_ScrollView_Viewport_Content_Panel.transform;
		OptionsPrefabs optionsPrefabs = OptionsPrefabs.get();
		optionsPrefabs.getHeader("%General%", parent);
		string[] choices = new string[3]
		{
			Localization.lookupInDictionary("sortPlaytime"),
			Localization.lookupInDictionary("sortVotes"),
			Localization.lookupInDictionary("sortTrend")
		};
		SelectableOption dropdown = optionsPrefabs.getDropdown("%Sorting%", parent, choices, advancedSearchSortSelected, delegate(int x)
		{
			advancedSearchSortSelected = x;
		});
		advancedSearchOptions.Add(dropdown);
		string[] choices2 = new string[5]
		{
			Localization.lookupInDictionary("oneWeek"),
			Localization.lookupInDictionary("oneMonth"),
			Localization.lookupInDictionary("sixMonths"),
			Localization.lookupInDictionary("oneYear"),
			Localization.lookupInDictionary("allTime")
		};
		SelectableOption dropdown2 = optionsPrefabs.getDropdown("%Timeframe%", parent, choices2, advancedSearchDaysSelected, delegate(int x)
		{
			advancedSearchDaysSelected = x;
		});
		advancedSearchOptions.Add(dropdown2);
		string[] choices3 = getTagOptions(advancedSearchFilters.themesTagsList);
		SelectableOption dropdown3 = optionsPrefabs.getDropdown("%Themes%", parent, choices3, advancedSearchFilters.themesTagsList.FindIndex((Tags.Tag x) => x.isActive), delegate(int x)
		{
			for (int i = 0; i < advancedSearchFilters.themesTagsList.Count; i++)
			{
				advancedSearchFilters.themesTagsList[i].isActive = i == x;
			}
		});
		advancedSearchOptions.Add(dropdown3);
		string[] choices4 = getTagOptions(advancedSearchFilters.difficultyTagsList);
		SelectableOption dropdown4 = optionsPrefabs.getDropdown("%Difficulty%", parent, choices4, advancedSearchFilters.difficultyTagsList.FindIndex((Tags.Tag x) => x.isActive), delegate(int x)
		{
			for (int i = 0; i < advancedSearchFilters.difficultyTagsList.Count; i++)
			{
				advancedSearchFilters.difficultyTagsList[i].isActive = i == x;
			}
		});
		advancedSearchOptions.Add(dropdown4);
		string[] choices5 = getTagOptions(advancedSearchFilters.playTimeTagsList);
		SelectableOption dropdown5 = optionsPrefabs.getDropdown("%PlayTime%", parent, choices5, advancedSearchFilters.playTimeTagsList.FindIndex((Tags.Tag x) => x.isActive), delegate(int x)
		{
			for (int i = 0; i < advancedSearchFilters.playTimeTagsList.Count; i++)
			{
				advancedSearchFilters.playTimeTagsList[i].isActive = i == x;
			}
		});
		advancedSearchOptions.Add(dropdown5);
		string[] choices6 = getTagOptions(advancedSearchFilters.languagesTagsList);
		SelectableOption dropdown6 = optionsPrefabs.getDropdown("%Language%", parent, choices6, advancedSearchFilters.languagesTagsList.FindIndex((Tags.Tag x) => x.isActive), delegate(int x)
		{
			for (int i = 0; i < advancedSearchFilters.languagesTagsList.Count; i++)
			{
				advancedSearchFilters.languagesTagsList[i].isActive = i == x;
			}
		});
		advancedSearchOptions.Add(dropdown6);
		numOfPlayersText = optionsPrefabs.getHeader(null, parent).mainObject.GetComponentInChildren<Text>(includeInactive: true);
		numOfPlayersText.text = Localization.lookupInDictionary("NumberOfPlayers") + " (" + PublishInfo.getNumOfPlayersText(advancedSearchFilters) + ")";
		string[] choices7 = getTagOptions(advancedSearchFilters.minNumPlayersTagsList);
		SelectableOption dropdown7 = optionsPrefabs.getDropdown("%NumberOfPlayersMin%", parent, choices7, advancedSearchFilters.minNumPlayersTagsList.FindIndex((Tags.Tag x) => x.isActive), delegate(int x)
		{
			for (int i = 0; i < advancedSearchFilters.minNumPlayersTagsList.Count; i++)
			{
				advancedSearchFilters.minNumPlayersTagsList[i].isActive = i == x;
			}
			numOfPlayersText.text = Localization.lookupInDictionary("NumberOfPlayers") + " (" + PublishInfo.getNumOfPlayersText(advancedSearchFilters) + ")";
		});
		advancedSearchOptions.Add(dropdown7);
		string[] choices8 = getTagOptions(advancedSearchFilters.maxNumPlayersTagsList);
		SelectableOption dropdown8 = optionsPrefabs.getDropdown("%NumberOfPlayersMax%", parent, choices8, advancedSearchFilters.maxNumPlayersTagsList.FindIndex((Tags.Tag x) => x.isActive), delegate(int x)
		{
			for (int i = 0; i < advancedSearchFilters.maxNumPlayersTagsList.Count; i++)
			{
				advancedSearchFilters.maxNumPlayersTagsList[i].isActive = i == x;
			}
			numOfPlayersText.text = Localization.lookupInDictionary("NumberOfPlayers") + " (" + PublishInfo.getNumOfPlayersText(advancedSearchFilters) + ")";
		});
		advancedSearchOptions.Add(dropdown8);
		optionsPrefabs.getHeader("%PineTags%", parent);
		foreach (Tags.Tag tag in advancedSearchFilters.pineTagsList)
		{
			SelectableOption dropdown9 = optionsPrefabs.getDropdown("%" + tag.name + "%", parent, new string[3] { "%Any%", "Include", "Exclude" }, 0, delegate(int newValue)
			{
				tag.isActive = newValue == 1;
				tag.isAny = newValue == 0;
			});
			advancedSearchOptions.Add(dropdown9);
		}
		optionsPrefabs.getHeader("%tagTitleMiscellaneous%", parent);
		foreach (Tags.Tag tag2 in advancedSearchFilters.miscellaneousTagsList)
		{
			SelectableOption dropdown10 = optionsPrefabs.getDropdown("%" + tag2.name + "%", parent, new string[3] { "%Any%", "Include", "Exclude" }, 0, delegate(int newValue)
			{
				tag2.isActive = newValue == 1;
				tag2.isAny = newValue == 0;
			});
			advancedSearchOptions.Add(dropdown10);
		}
		List<VisualControl> list = new List<VisualControl>();
		list.Add(new VisualControl("filterSearch", ControllerButtonActionType.UIExtra1, "%AdvancedSearch%", VisualControl.ControlType.Button, VisualControl.Position.BotRight));
		list.Add(new VisualControl("back", ControllerButtonActionType.UIBack, "%back%", VisualControl.ControlType.Button, VisualControl.Position.BotRight));
		menuUI.TagPicker.Frame.root.setup(menuInputDispatcher, null, null, onFrameClick);
		menuUI.TagPicker.Frame.root.addOrUpdateControl(list);
		UGCQueryHandle_t handle = SteamUGC.CreateQueryAllUGCRequest(EUGCQuery.k_EUGCQuery_RankedByTrend, EUGCMatchingUGCType.k_EUGCMatchingUGCType_All, new AppId_t(Game.STEAM_APP_ID), new AppId_t(Game.STEAM_APP_ID), 1u);
		SteamUGC.SetReturnMetadata(handle, bReturnMetadata: true);
		SteamUGC.SetRankedByTrendDays(handle, 7u);
		SteamUGC.AddRequiredTag(handle, "Room");
		SteamUGC.AddRequiredTag(handle, "English");
		SteamUGC.AddExcludedTag(handle, "Has profanity");
		SteamUGC.SetReturnChildren(handle, bReturnChildren: true);
		SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(handle);
		CallResult<SteamUGCQueryCompleted_t> callResult = CallResult<SteamUGCQueryCompleted_t>.Create(onDoneTrending);
		callResult.Set(hAPICall);
		gcDontKill.Add(callResult);
		fireAdvancedSearch();
		static string[] getTagOptions(List<Tags.Tag> tags)
		{
			List<string> list2 = new List<string>();
			foreach (Tags.Tag tag3 in tags)
			{
				list2.Add(Localization.lookupInDictionary(tag3.name, tag3.name));
			}
			return list2.ToArray();
		}
		void onDoneTrending(SteamUGCQueryCompleted_t data, bool failure)
		{
			WorkshopCollection workshopCollection = null;
			if (data.m_eResult == EResult.k_EResultOK && !failure)
			{
				workshopCollection = new WorkshopCollection
				{
					collectionId = "trending",
					collectionName = "Trending",
					rooms = new List<WorkshopRoomInfo>()
				};
				workshopCollections.Add(workshopCollection);
				uint unNumResultsReturned = data.m_unNumResultsReturned;
				for (uint num = 0u; num < unNumResultsReturned && num < 6; num++)
				{
					SteamUGC.GetQueryUGCResult(data.m_handle, num, out var pDetails);
					string text = pDetails.m_nPublishedFileId.ToString();
					SteamUGC.GetQueryUGCPreviewURL(data.m_handle, num, out var pchURL, 5000u);
					levelProvider.addLevelToList("trending", text);
					levelProvider.modifyLevel(text, pDetails.m_rgchTitle, pchURL);
				}
			}
			SteamUGC.ReleaseQueryUGCRequest(data.m_handle);
		}
	}

	private void fireAdvancedSearch()
	{
		if (Is.PineBackend || Is.Demo || Is.Gamescom || Is.perfTest())
		{
			return;
		}
		List<Tags.Tag> list = new List<Tags.Tag>();
		List<Tags.Tag> list2 = new List<Tags.Tag>();
		list.AddRange(advancedSearchFilters.miscellaneousTagsList.FindAll((Tags.Tag x) => x.isActive));
		list.AddRange(advancedSearchFilters.codeAddedTagsList.FindAll((Tags.Tag x) => x.isActive));
		list.AddRange(advancedSearchFilters.pineTagsList.FindAll((Tags.Tag x) => x.isActive));
		list2.AddRange(advancedSearchFilters.miscellaneousTagsList.FindAll((Tags.Tag x) => !x.isActive && !x.isAny));
		list2.AddRange(advancedSearchFilters.codeAddedTagsList.FindAll((Tags.Tag x) => !x.isActive && !x.isAny));
		list2.AddRange(advancedSearchFilters.pineTagsList.FindAll((Tags.Tag x) => !x.isActive && !x.isAny));
		for (int num = 0; num < advancedSearchFilters.minNumPlayersTagsList.Count && !advancedSearchFilters.minNumPlayersTagsList[num].isActive; num++)
		{
			list2.Add(advancedSearchFilters.minNumPlayersTagsList[num]);
		}
		bool flag = false;
		for (int num2 = 0; num2 < advancedSearchFilters.maxNumPlayersTagsList.Count; num2++)
		{
			if (flag)
			{
				list2.Add(advancedSearchFilters.maxNumPlayersTagsList[num2]);
			}
			if (advancedSearchFilters.maxNumPlayersTagsList[num2].isActive)
			{
				flag = true;
			}
		}
		if (!advancedSearchFilters.themesTagsList[0].isActive)
		{
			if (advancedSearchFilters.themesTagsList[1].isActive)
			{
				list2.Add(advancedSearchFilters.themesTagsList[5]);
			}
			else
			{
				list.AddRange(advancedSearchFilters.themesTagsList.FindAll((Tags.Tag x) => x.isActive));
			}
		}
		if (!advancedSearchFilters.languagesTagsList[0].isActive)
		{
			list.AddRange(advancedSearchFilters.languagesTagsList.FindAll((Tags.Tag x) => x.isActive));
		}
		if (!advancedSearchFilters.playTimeTagsList[0].isActive)
		{
			list.AddRange(advancedSearchFilters.playTimeTagsList.FindAll((Tags.Tag x) => x.isActive));
		}
		if (!advancedSearchFilters.difficultyTagsList[0].isActive)
		{
			list.AddRange(advancedSearchFilters.difficultyTagsList.FindAll((Tags.Tag x) => x.isActive));
		}
		List<string> list3 = list.ConvertAll((Tags.Tag x) => x.name);
		List<string> list4 = list2.ConvertAll((Tags.Tag x) => x.name);
		if (!session.isHost())
		{
			session.send(new RequestAdvancedSearchPacket
			{
				tagsToInclude = list3,
				tagsToExclude = list4,
				sortIndex = advancedSearchSortSelected,
				daysIndex = advancedSearchDaysSelected
			});
		}
		else
		{
			fireAdvancedSearchHost(list3, list4, advancedSearchSortSelected, advancedSearchDaysSelected);
		}
	}

	private void fireAdvancedSearchHost(List<string> enabledTags, List<string> disabledTags, int sortIndex, int daysIndex)
	{
		UnityEngine.Debug.Log("-----Firing advanced search-----");
		UGCQueryHandle_t handle = SteamUGC.CreateQueryAllUGCRequest(advancedSearchSortings[sortIndex], EUGCMatchingUGCType.k_EUGCMatchingUGCType_All, new AppId_t(Game.STEAM_APP_ID), new AppId_t(Game.STEAM_APP_ID), 1u);
		if (advancedSearchDaysSelected < advancedSearchDays.Count - 1)
		{
			UnityEngine.Debug.Log("Setting advanced search days: " + advancedSearchDays[daysIndex]);
			SteamUGC.SetRankedByTrendDays(handle, advancedSearchDays[daysIndex]);
		}
		else
		{
			UnityEngine.Debug.Log("Setting advanced search days: All Time");
		}
		foreach (string enabledTag in enabledTags)
		{
			string text = Localization.lookupInEnglishDictionary(enabledTag, enabledTag);
			UnityEngine.Debug.Log("INCLUDE TAG: " + text);
			SteamUGC.AddRequiredTag(handle, text);
		}
		foreach (string disabledTag in disabledTags)
		{
			string text2 = Localization.lookupInEnglishDictionary(disabledTag, disabledTag);
			UnityEngine.Debug.Log("EXCLUDE TAG: " + text2);
			SteamUGC.AddExcludedTag(handle, text2);
		}
		UnityEngine.Debug.Log("------");
		SteamAPICall_t hAPICall = SteamUGC.SendQueryUGCRequest(handle);
		CallResult<SteamUGCQueryCompleted_t> callResult = CallResult<SteamUGCQueryCompleted_t>.Create(onDoneSearch);
		callResult.Set(hAPICall);
		gcDontKill.Add(callResult);
		void onDoneDetails(SteamUGCQueryCompleted_t data, bool failure)
		{
			if (data.m_eResult == EResult.k_EResultOK && !failure)
			{
				levelProvider.removeAllFromList("advancedSearch");
				workshopCollections.RemoveAll((WorkshopCollection x) => x.collectionId == "advancedSearch");
				WorkshopCollection item = new WorkshopCollection
				{
					collectionId = "advancedSearch",
					collectionName = "advancedSearch",
					rooms = new List<WorkshopRoomInfo>()
				};
				workshopCollections.Add(item);
				uint unNumResultsReturned = data.m_unNumResultsReturned;
				for (uint num = 0u; num < unNumResultsReturned && num < 6; num++)
				{
					if (!SteamUGC.GetQueryUGCResult(data.m_handle, num, out var pDetails))
					{
						UnityEngine.Debug.LogError($"Failed to get query result for index {num} in advanced search.");
					}
					string text3 = pDetails.m_nPublishedFileId.ToString();
					SteamUGC.GetQueryUGCPreviewURL(data.m_handle, num, out var pchURL, 5000u);
					levelProvider.addLevelToList("advancedSearch", text3);
					levelProvider.modifyLevel(text3, pDetails.m_rgchTitle, null, pchURL);
				}
			}
			if (lpRooms.getSelectedId().packId == "advancedSearch")
			{
				List<string> currentLevels = lpRooms.getCurrentLevels();
				session.send(new HostCustomLevelPickerPacket
				{
					packId = "advancedSearch",
					levelIds = currentLevels
				}, allowSendInMessageResponse: true);
			}
			SteamUGC.ReleaseQueryUGCRequest(data.m_handle);
		}
		void onDoneSearch(SteamUGCQueryCompleted_t data, bool failure)
		{
			uint num = ((data.m_unNumResultsReturned < 6) ? data.m_unNumResultsReturned : 6u);
			PublishedFileId_t[] array = new PublishedFileId_t[data.m_unNumResultsReturned];
			for (uint num2 = 0u; num2 < num; num2++)
			{
				SteamUGC.GetQueryUGCResult(data.m_handle, num2, out var pDetails);
				array[num2] = pDetails.m_nPublishedFileId;
			}
			SteamAPICall_t hAPICall2 = SteamUGC.SendQueryUGCRequest(SteamUGC.CreateQueryUGCDetailsRequest(array, (uint)array.Length));
			CallResult<SteamUGCQueryCompleted_t> callResult2 = CallResult<SteamUGCQueryCompleted_t>.Create(onDoneDetails);
			callResult2.Set(hAPICall2);
			gcDontKill.Add(callResult2);
			SteamUGC.ReleaseQueryUGCRequest(data.m_handle);
		}
	}

	private void initCustomization()
	{
		VisualControl newControl = new VisualControl("randomize", ControllerButtonActionType.UIExtra1, "%randomize%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
		menuUI.Customization.Frame.addOrUpdateControl(newControl);
		VisualControl visualControl = new VisualControl("back", ControllerButtonActionType.UIBack, "%back%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
		visualControl.addEscape();
		menuUI.Customization.Frame.addOrUpdateControl(visualControl);
		menuUI.Customization.CustomizationContent_VariantsPanel.gameObject.SetActive(value: true);
		menuUI.Customization.CustomizationContent_VariantsPanel_VariantContent_CCCategoryTemplate.gameObject.SetActive(value: false);
		menuUI.Customization.CustomizationContent_VariantsPanel_VariantContent_CCCategoryTemplate.VariantsGrid_CCVariant.gameObject.SetActive(value: false);
		GameObject gameObject = UnityEngine.Object.Instantiate(game.netPlayerMesh, ccSpawnPoint);
		gameObject.transform.localPosition = Vector3.zero;
		ccPlayer = gameObject.GetComponentInChildren<CharacterModels>();
		CharacterBuild[] builds = ccPlayer.builds;
		foreach (CharacterBuild obj in builds)
		{
			obj.transform.GetComponentInChildren<Canvas>(includeInactive: true).gameObject.SetActive(value: false);
			Game.applyCCFromSave(obj);
		}
		ccPlayer.builds[0].GetComponent<Animator>().runtimeAnimatorController = ccAnimationControllers[0];
		ccPlayer.builds[1].GetComponent<Animator>().runtimeAnimatorController = ccAnimationControllers[1];
		setBodyType(PlayerSave.getCC().gender);
		Dictionary<string, int> categoryToSavedVariant = PlayerSave.getCC().getCategoryToSavedVariant();
		foreach (CCOptionsScriptable.CategoryData option in ccOptions.options)
		{
			CCCategory category = buildCCCategory(option);
			int num = categoryToSavedVariant[option.id];
			foreach (CCOptionsScriptable.VariantData variant in option.variants)
			{
				bool selected = num == variant.id;
				buildCCVariant(category, variant, selected);
			}
		}
	}

	private void onCCVariant(CCVariantUI ui, bool apply = true)
	{
		CCVariant cCVariant = (CCVariant)ui.data;
		if (cCVariant == null)
		{
			return;
		}
		if (cCVariant.category.data.id == "Outfit")
		{
			PlayerSave.getCC().outfit = (CharacterBuild.Outfit)cCVariant.data.id;
		}
		else if (cCVariant.category.data.id == "Hair")
		{
			PlayerSave.getCC().hair = (CharacterBuild.Hair)cCVariant.data.id;
		}
		else if (cCVariant.category.data.id == "Gender")
		{
			setBodyType((CharacterBuild.Gender)cCVariant.data.id);
		}
		else if (cCVariant.category.data.id == "SkinColor")
		{
			PlayerSave.getCC().skinType = cCVariant.data.id;
		}
		else if (cCVariant.category.data.id == "HairColor")
		{
			PlayerSave.getCC().hairColor = cCVariant.data.id;
		}
		else if (cCVariant.category.data.id == "EyeColor")
		{
			PlayerSave.getCC().eyeColor = (CharacterBuild.EyeColor)cCVariant.data.id;
		}
		if (apply)
		{
			Game.applyCCFromSave(game.localPlayerData.maleCharacter);
			Game.applyCCFromSave(game.localPlayerData.femaleCharacter);
			CharacterBuild[] builds = ccPlayer.builds;
			for (int i = 0; i < builds.Length; i++)
			{
				Game.applyCCFromSave(builds[i]);
			}
			game.localPlayerData.selectedCharacter.characterLayer = CharacterBuild.CharacterLayer.Local;
			PlayerSave.flush();
			session.send(new CharacterCustomizationPacket
			{
				customization = PlayerSave.getCC()
			}, allowSendInMessageResponse: true);
		}
		foreach (CCVariant variant in cCVariant.category.variants)
		{
			variant.button.ui.SelectionFrame.gameObject.SetActive(variant.button.ui == ui);
		}
		if (cCVariant.category.data.id == "Gender")
		{
			foreach (CCVariant allVariant in allVariants)
			{
				Sprite sprite = allVariant.data.sprite;
				if (PlayerSave.getCC().gender == CharacterBuild.Gender.Female && allVariant.data.spriteFemale != null)
				{
					sprite = allVariant.data.spriteFemale;
				}
				if (sprite != null)
				{
					allVariant.button.ui.Image.sprite = sprite;
				}
			}
		}
		if (PlayerSave.getCC().outfit == CharacterBuild.Outfit.DraculaPremium || PlayerSave.getCC().outfit == CharacterBuild.Outfit.PiratePremium || PlayerSave.getCC().outfit == CharacterBuild.Outfit.SpacePremium)
		{
			game.saveAchievement("ACHIEVEMENT_PREMIUM_LOOK");
		}
	}

	private void setBodyType(CharacterBuild.Gender gender)
	{
		if (game.localPlayerData.isMale)
		{
			game.localPlayerData.femaleCharacter.transform.position = game.localPlayerData.maleCharacter.transform.position;
			game.localPlayerData.femaleCharacter.transform.rotation = game.localPlayerData.maleCharacter.transform.rotation;
		}
		else
		{
			game.localPlayerData.maleCharacter.transform.position = game.localPlayerData.femaleCharacter.transform.position;
			game.localPlayerData.maleCharacter.transform.rotation = game.localPlayerData.femaleCharacter.transform.rotation;
		}
		game.localPlayerData.maleCharacter.gameObject.SetActive(value: false);
		game.localPlayerData.femaleCharacter.gameObject.SetActive(value: false);
		game.localPlayerData.selectedCharacter = ((gender == CharacterBuild.Gender.Male) ? game.localPlayerData.maleCharacter : game.localPlayerData.femaleCharacter);
		game.localPlayerData.selectedCharacter.gameObject.SetActive(value: true);
		game.localPlayerData.animator = game.localPlayerData.selectedCharacter.GetComponent<Animator>();
		game.localPlayerData.playerCanvas = game.localPlayerData.selectedCharacter.GetComponentInChildren<Canvas>(includeInactive: true);
		game.localPlayerData.playerCanvas.gameObject.SetActive(value: false);
		game.localPlayerData.isMale = gender == CharacterBuild.Gender.Male;
		CharacterBuild[] builds = ccPlayer.builds;
		foreach (CharacterBuild characterBuild in builds)
		{
			characterBuild.gameObject.SetActive(characterBuild.gender == gender);
		}
		PlayerSave.getCC().gender = gender;
	}

	private CCCategory buildCCCategory(CCOptionsScriptable.CategoryData data)
	{
		CCCategoryTemplateUI ui = UnityEngine.Object.Instantiate(menuUI.Customization.CustomizationContent_VariantsPanel_VariantContent_CCCategoryTemplate, menuUI.Customization.CustomizationContent_VariantsPanel_VariantContent_CCCategoryTemplate.transform.parent);
		CCCategory cCCategory = new CCCategory(data, ui);
		cCCategory.ui.data = cCCategory;
		cCCategory.ui.gameObject.SetActive(value: true);
		string text = "%CC_" + data.id + "%";
		cCCategory.ui.Title.GetComponent<TranslatedText>().originalText = text;
		cCCategory.ui.Title.text = text;
		cCCategory.ui.Title.gameObject.SetActive(data.shouldDisplayName);
		cCCategory.ui.Spacer.gameObject.SetActive(data.shouldDisplayName && data.id != "Gender");
		Localization.translateObject(cCCategory.ui.Title.transform, forceNewText: true);
		cCCategory.ui.VariantsGrid.cellSize = ((data.cellSize.x == 0f || data.cellSize.y == 0f) ? new Vector2(60f, 60f) : data.cellSize);
		allCategories.Add(cCCategory);
		return cCCategory;
	}

	private void buildCCVariant(CCCategory category, CCOptionsScriptable.VariantData data, bool selected = false)
	{
		CCVariantButton button = UnityEngine.Object.Instantiate(menuUI.Customization.CustomizationContent_VariantsPanel_VariantContent_CCCategoryTemplate.VariantsGrid_CCVariant, category.ui.VariantsGrid.transform);
		CCVariant cCVariant = new CCVariant(category, data, button);
		cCVariant.button.ui.data = cCVariant;
		cCVariant.button.gameObject.SetActive(value: true);
		cCVariant.button.ui.SelectionFrame.gameObject.SetActive(selected);
		cCVariant.button.ui.HoverFrame.gameObject.SetActive(value: false);
		Sprite sprite = data.sprite;
		if (PlayerSave.getCC().gender == CharacterBuild.Gender.Female && data.spriteFemale != null)
		{
			sprite = data.spriteFemale;
		}
		if (sprite != null)
		{
			cCVariant.button.ui.Image.sprite = sprite;
			cCVariant.button.ui.Image.color = Color.white;
		}
		else
		{
			Themes.setColorWithoutAlpha(cCVariant.button.ui.Image, data.color);
		}
		if (data.awardNeeded != -1 && !PlayerSave.getProgress().awards.Contains((AwardType)data.awardNeeded))
		{
			cCVariant.button.enabled = false;
			cCVariant.button.interactable = false;
			cCVariant.button.ui.Locked.gameObject.SetActive(value: true);
		}
		category.variants.Add(cCVariant);
		allVariants.Add(cCVariant);
	}

	private void updateCCController(bool forceUpdate = false)
	{
		if (!Controller.isActive() || !menuUI.Customization.gameObject.activeInHierarchy)
		{
			return;
		}
		if (forceUpdate)
		{
			Controller.selectSelectable(allVariants[0].button.ui.root);
		}
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		if (controllerLastSelectedCCUI != currentSelectedGameObject)
		{
			if (currentSelectedGameObject != null && currentSelectedGameObject.TryGetComponent<CCVariantButton>(out var component))
			{
				component.hover();
			}
			if (controllerLastSelectedCCUI != null && controllerLastSelectedCCUI.TryGetComponent<CCVariantButton>(out var component2))
			{
				component2.unhover();
			}
			controllerLastSelectedCCUI = currentSelectedGameObject;
		}
		if (Controller.getUIConfirmDown() && currentSelectedGameObject != null)
		{
			CCVariantButton component4;
			if (currentSelectedGameObject.TryGetComponent<CCGroupTemplateUI>(out var component3))
			{
				controllerSelectedCCGroup = component3;
			}
			else if (currentSelectedGameObject.TryGetComponent<CCVariantButton>(out component4))
			{
				onCCVariant(component4.ui);
			}
		}
	}

	private void updateCharacterCustomization()
	{
		if (!(game.localPlayerData.customMode != ccCustomMode))
		{
			if (Controller.isActive())
			{
				rotationTarget -= Controller.getAxis(ControllerAxisActionType.UIMoveAndCursor).x * 5f;
			}
			else if (Input.GetMouseButton(0))
			{
				float num = rotationTarget;
				rotationTarget += 0f - Input.GetAxis("Mouse X") * 500f * Time.deltaTime;
				rotationVelocity = rotationTarget - num;
			}
			else
			{
				rotationTarget += rotationVelocity * 10f * Time.deltaTime;
				rotationVelocity = Mathf.MoveTowards(rotationVelocity, 0f, Time.deltaTime * 70f);
			}
			rotationVelocity = Mathf.Clamp(rotationVelocity, -100f, 100f);
			rotationCurrent = rotationTarget;
			if (Mathf.Abs(rotationTarget) <= 10f)
			{
				rotationCurrent = rotationTarget;
			}
			ccSpawnPoint.transform.transform.localRotation = Quaternion.Euler(0f, rotationCurrent, 0f);
		}
	}

	private void updateController(bool forceChange = false)
	{
		if (Controller.controllerModeChanged || forceChange)
		{
			Controller.setupVerticalNavigation(menuUI.ES2Cover.MainButtons.transform, menuUI.ES2Cover.MainButtons_ButtonBuild);
			Controller.setupVerticalNavigation(menuUI.ES2Cover.PlayButtons.transform);
			if (Controller.isActive())
			{
				if (menuUI.TagPicker.gameObject.activeInHierarchy)
				{
					Controller.setupVerticalNavigationAndSelectFirst(advancedSearchOptions);
				}
				else if (menuUI.InitialSetup.gameObject.activeInHierarchy)
				{
					Controller.setupVerticalNavigationAndSelectFirst(initialSetupOptions);
				}
				else if (menuUI.ES2Cover.MainButtons.gameObject.activeInHierarchy)
				{
					Controller.setupVerticalNavigationAndSelectFirst(menuUI.ES2Cover.MainButtons.transform);
				}
				else if (menuUI.ES2Cover.PlayButtons.gameObject.activeInHierarchy)
				{
					Controller.setupVerticalNavigationAndSelectFirst(menuUI.ES2Cover.PlayButtons.transform);
				}
				else if (menuUI.Rooms.gameObject.activeInHierarchy)
				{
					lpRooms.updateController(forceChange: true);
				}
				else if (menuUI.Darkest.gameObject.activeInHierarchy)
				{
					lpDarkest.updateController(forceChange: true);
				}
				else if (game.menuOptions.gameObject.activeInHierarchy)
				{
					updateCCController(forceUpdate: true);
				}
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;
			}
			menuUI.Customization.JoystickPointer.gameObject.SetActive(Controller.isActive());
			menuUI.ES2Cover.MainButtons_ButtonBuild.gameObject.SetActive(!Controller.isActive());
			menuUI.ES2Cover.MainButtons_Spacer.gameObject.SetActive(Controller.isActive());
			if (Controller.isActive())
			{
				Controller.setupVerticalNavigation(menuUI.ES2Cover.MainButtons.transform, menuUI.ES2Cover.MainButtons_ButtonBuild);
			}
		}
		if (!Controller.isActive())
		{
			return;
		}
		Transform parent = null;
		Selectable firstSelectable = null;
		bool flag = true;
		if (!menuUI.OptionDialog.gameObject.activeInHierarchy)
		{
			if (game.menuHostOptions.gameObject.activeInHierarchy)
			{
				flag = false;
			}
			else if (controllerSelectedCCGroup != null)
			{
				parent = menuUI.Customization.CustomizationContent_VariantsPanel;
				firstSelectable = menuUI.Customization.CustomizationContent_VariantsPanel.GetComponentInChildren<CCVariantUI>().root;
			}
			else if (menuUI.ClientCode.gameObject.activeInHierarchy)
			{
				parent = menuUI.ClientCode.ControllerKeyboard.transform;
				firstSelectable = menuUI.ClientCode.ControllerKeyboard.firstButton;
			}
			else if (menuUI.Customization.gameObject.activeInHierarchy)
			{
				parent = menuUI.Customization.CustomizationContent.transform;
				firstSelectable = allVariants[0].button;
			}
		}
		if (flag)
		{
			Controller.setSelectingGroup(parent, firstSelectable);
		}
		if (menuUI.ES2Cover.PlayButtons.gameObject.activeInHierarchy && Controller.getButtonDown(ControllerButtonActionType.UIBack))
		{
			onButtonClick(menuUI.ES2Cover.PlayButtons_ButtonBack);
		}
		if (!menuUI.Customization.gameObject.activeInHierarchy)
		{
			return;
		}
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		menuUI.Customization.JoystickPointer.gameObject.SetActive(currentSelectedGameObject != null && !joystickPointerDoTeleport);
		if (currentSelectedGameObject != null)
		{
			currentSelectedGameObject.GetComponent<RectTransform>().GetWorldCorners(worldCornersCache);
			Vector3 vector = worldCornersCache[0];
			if (joystickPointerDoTeleport)
			{
				menuUI.Customization.JoystickPointer.position = vector;
				joystickPointerDoTeleport = false;
			}
			else
			{
				menuUI.Customization.JoystickPointer.position = Vector3.MoveTowards(menuUI.Customization.JoystickPointer.position, vector, Time.deltaTime * 10f);
			}
		}
	}

	public void initPineWorkshop()
	{
		if (Is.PineBackend)
		{
			queryPineWorkshop();
		}
	}

	private string getMyPineWorkshopId()
	{
		string result = "";
		if (Is.Switch)
		{
			result = "switch-browse";
		}
		if (Is.Oculus)
		{
			result = "dc96de3e-56d5-4c2b-934f-636edcfc3ae7";
		}
		if (Is.PS4)
		{
			result = "switch-browse";
		}
		if (Is.Xbox)
		{
			result = "switch-browse";
		}
		if (Is.Steam)
		{
			result = "dc96de3e-56d5-4c2b-934f-636edcfc3ae7";
		}
		if (Is.iOS || Is.AndroidPhone)
		{
			result = "switch-browse";
		}
		return result;
	}

	private void queryPineWorkshop()
	{
		if (string.IsNullOrEmpty(getWorkshopToken()))
		{
			waitForWorkshopTokenThenCallQueryPineWorkshop = true;
			return;
		}
		UnityEngine.Debug.Log("queryPineWorkshop");
		string myPineWorkshopId = getMyPineWorkshopId();
		fillPack(myPineWorkshopId, Localization.lookupInDictionary("browseRooms"));
		if (Is.Oculus && VR.oculusBetaRoomsAvailable)
		{
			fillPack("beta", "Beta");
		}
		fillLevelsForOtherPlatforms();
		void fillLevelsForOtherPlatforms()
		{
			List<string> list = new List<string>(pineWorkshopAllIds);
			list.Remove(getMyPineWorkshopId());
			foreach (string id in list)
			{
				pineWorkshopWebRequests.Add(new PineWorkshopRequest
				{
					request = sendWorkshopWebRequest("/query/listing/" + id),
					onDone = delegate(string roomsData)
					{
						onDoneList(id, roomsData);
					}
				});
			}
		}
		void onDoneList(string listId, string roomsData)
		{
			PineRoomsData pineRoomsData = JsonUtility.FromJson<PineRoomsData>(roomsData);
			HashSet<string> hashSet = new HashSet<string>();
			foreach (PineRoomData room in pineRoomsData.Rooms)
			{
				hashSet.Add(room.SteamID);
			}
			if (!pineWorkshopSteamIdsOfOtherPlatforms.ContainsKey(listId))
			{
				pineWorkshopSteamIdsOfOtherPlatforms.Add(listId, hashSet);
			}
		}
	}

	private void fillPack(string packId, string packName)
	{
		pineWorkshopWebRequests.Add(new PineWorkshopRequest
		{
			request = sendWorkshopWebRequest("/query/listing/" + packId),
			onDone = delegate(string roomsData)
			{
				onDoneList(packId, roomsData);
			}
		});
		startPack(lpRooms, "ugcPine." + packId, packId, packName);
		void onDoneList(string listId, string roomsData)
		{
			foreach (PineRoomData room in JsonUtility.FromJson<PineRoomsData>(roomsData).Rooms)
			{
				levelProvider.addLevelToList(listId, room.id);
				DateTime.TryParse(room.Modified, out var result);
				LevelProvider obj = levelProvider;
				string id = room.id;
				string text = room.Name;
				string imageURL = room.ImageURL;
				DateTime? dateFolderCreated = result;
				string fileSizeBytesHalf = room.FileSizeBytesHalf;
				string description = room.Description;
				string creator = room.Creator;
				string tags = room.Tags;
				bool? isLocked = false;
				obj.modifyLevel(id, text, null, imageURL, -1, -1, null, null, null, isLocked, null, null, fileSizeBytesHalf, null, dateFolderCreated, null, null, null, null, creator, tags, description);
				pineWorkshopSteamIdsOfMyPlatforms.Add(room.SteamID);
			}
		}
		static void startPack(LevelPicker lp, string id, string listId, string name)
		{
			lp.use("ugcPine");
			lp.definePack(listId, listId, canUseSearch: true, canUseBrowse: false, null);
			LevelProvider.PackSize? packSize = LevelProvider.PackSize.CustomRoom;
			lp.modifyPack(id, name, null, null, null, null, null, null, null, null, null, packSize);
		}
	}

	public static string getWorkshopToken()
	{
		return webApiTicket;
	}

	public static UnityWebRequest sendWorkshopWebRequest(string query, bool autoSend = true)
	{
		UnityEngine.Debug.Log("[sendWorkshopWebRequest] " + query);
		UnityWebRequest unityWebRequest = new UnityWebRequest("https://ugc.escapesimulator.com/workshop" + query);
		unityWebRequest.SetRequestHeader("Authorization", getWorkshopToken());
		string value = "steam";
		if (Is.Switch)
		{
			value = "switch";
		}
		if (Is.Oculus)
		{
			value = "oculus";
		}
		unityWebRequest.SetRequestHeader("Platform", value);
		unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
		if (autoSend)
		{
			unityWebRequest.SendWebRequest();
		}
		return unityWebRequest;
	}

	private void updatePineWorkshop()
	{
		if (inCrossplatformLobby && !inCrossplatformLobbyLastTime)
		{
			UnityEngine.Debug.Log("Setting up Crossplatform lobby");
			queryPineWorkshop();
			ESUGC.current.refreshAllInstalledRooms(onLevelRefresh);
			inCrossplatformLobbyLastTime = inCrossplatformLobby;
		}
		if (waitForWorkshopTokenThenCallQueryPineWorkshop && !string.IsNullOrEmpty(getWorkshopToken()))
		{
			waitForWorkshopTokenThenCallQueryPineWorkshop = false;
			queryPineWorkshop();
		}
		if (!Is.PineBackend && !inCrossplatformLobby)
		{
			return;
		}
		setupCrossplatformLevelPicker();
		for (int num = pineWorkshopWebRequests.Count - 1; num >= 0; num--)
		{
			PineWorkshopRequest pineWorkshopRequest = pineWorkshopWebRequests[num];
			if (pineWorkshopRequest.request.isDone)
			{
				if (pineWorkshopRequest.request.responseCode == 200)
				{
					string obj = Encoding.Default.GetString(pineWorkshopRequest.request.downloadHandler.data);
					pineWorkshopRequest.onDone(obj);
				}
				else
				{
					UnityEngine.Debug.LogError($"Pine workshop request failed (code: {pineWorkshopRequest.request.responseCode}): {pineWorkshopRequest.request.error}");
				}
				pineWorkshopWebRequests.RemoveAt(num);
			}
		}
	}

	private void onSteamAuthed(GetTicketForWebApiResponse_t data)
	{
		UnityEngine.Debug.Log($"onSteamAuthed{data.m_hAuthTicket} {data.m_eResult}");
		if (data.m_eResult == EResult.k_EResultOK)
		{
			webApiTicket = BitConverter.ToString(data.m_rgubTicket).Replace("-", "");
		}
	}

	public bool isVR()
	{
		if (vr != null)
		{
			return vr.isActive();
		}
		return false;
	}

	private void initVR()
	{
		vr = VR.initInstance(isPrimaryInstance: true);
		bool flag = UnityUtils.contains(Environment.GetCommandLineArgs(), "es_vr");
		vrType = ((!flag) ? VR.Type.ForceNone : VR.Type.EnableIfDetected);
	}

	private void initVRCamera()
	{
		Camera component = GetComponent<Camera>();
		int cullingMask = component.cullingMask;
		component.enabled = false;
		vr.rig.headCamera.cullingMask = cullingMask;
		vrCanvasTemplate.worldCamera = vr.rig.headCamera;
		if (vr.getActiveState() == VR.State.Simulator)
		{
			vr.rig.headCameraOffset.localPosition = new Vector3(0f, 1f, 0f);
		}
		if (enteredMenuForFirstTime)
		{
			return;
		}
		for (int i = 0; i < SceneManager.sceneCount; i++)
		{
			Scene sceneAt = SceneManager.GetSceneAt(i);
			if (!sceneAt.name.Contains("Splash"))
			{
				continue;
			}
			GameObject[] rootGameObjects = sceneAt.GetRootGameObjects();
			for (int j = 0; j < rootGameObjects.Length; j++)
			{
				if (rootGameObjects[j].TryGetComponent<Camera>(out var component2))
				{
					component2.enabled = false;
				}
			}
			break;
		}
		vr.fadeInAndOut(0f, 3f, null, () => vrInitialLookDirectionInited);
	}

	private void initVRCanvases()
	{
		Canvas[] componentsInChildren = menuUI.GetComponentsInChildren<Canvas>(includeInactive: true);
		foreach (Canvas canvas in componentsInChildren)
		{
			if (!(canvas == vr.rig.mainMenuCanvas))
			{
				if (canvas.transform.parent == menuUI.transform)
				{
					vr.mapCanvas(canvas, vrCanvasTemplate);
				}
				vr.setupCanvas(canvas);
			}
		}
		Canvas component = LoadingCanvas.get().gameObject.GetComponent<Canvas>();
		vr.mapCanvas(component, vrCanvasTemplate);
		vr.setupCanvas(component);
	}

	private void initVRRig()
	{
		vr.rig.rightController.raycastLineRenderer.enabled = true;
		vr.rig.rightController.teleportArcRenderer.gameObject.SetActive(value: false);
		vr.rig.rightController.teleportMarker.gameObject.SetActive(value: false);
		vr.rig.rightController.watch.gameObject.SetActive(value: false);
		vr.setControllerRayColor(2, isHovering: false);
		vr.setControllerIndicatorState(2, isShown: false);
		vr.rig.leftController.raycastLineRenderer.enabled = true;
		vr.rig.leftController.teleportArcRenderer.gameObject.SetActive(value: false);
		vr.rig.leftController.teleportMarker.gameObject.SetActive(value: false);
		vr.rig.leftController.watch.gameObject.SetActive(value: false);
		vr.setControllerRayColor(1, isHovering: false);
		vr.setControllerIndicatorState(1, isShown: false);
		vr.rig.wallSphere.SetActive(value: false);
		vr.rig.pauseCanvas.gameObject.SetActive(value: false);
		vr.rig.skyConfettiEffect.gameObject.SetActive(value: false);
		Vector3 vector = ((vr.getActiveState() == VR.State.Simulator) ? ((vrPlane.transform.position - vrMenuSpawnPoint.position).normalized * vrSimulatorSpawnOffset) : Vector3.zero);
		vr.setRigPosition(vrMenuSpawnPoint.position + vector);
		vr.setLayerToRenderers("CharacterLocal");
	}

	private void initVRMenu()
	{
		vrKeyboardDeleteButton.onClick.AddListener(onKeyboardDeletePressed);
		vrKeyboardBackButton.onClick.AddListener(delegate
		{
			onBack();
		});
	}

	private void initVRCharacter()
	{
		characterPivot.transform.SetParent(vrElevator, worldPositionStays: true);
		vrElevatorCurrent = 1f;
		vrCurtainsCurrentGoal = 1f;
	}

	private void initVROculus()
	{
		try
		{
			Core.AsyncInitialize("6960826027306377");
			Entitlements.IsUserEntitledToApplication().OnComplete(performEntitlementCheck);
		}
		catch (Exception exception)
		{
			UnityEngine.Debug.LogError("Oculus failed to initialize due to exception.");
			UnityEngine.Debug.LogException(exception);
			showEntitlementCheckFailedMessage();
		}
		static void getAvailableProducts(Message<ProductList> message)
		{
			if (message.IsError)
			{
				UnityEngine.Debug.LogError("[initVROculus] Failed to get available products: " + message.GetError().Message);
			}
			else
			{
				ProductList productList = message.GetProductList();
				UnityEngine.Debug.Log(string.Format("[{0}] There are {1} available products.", "initVROculus", productList.Count));
				for (int i = 0; i < productList.Count; i++)
				{
					Product product = productList[i];
					UnityEngine.Debug.Log($"Product #{i} | SKU: {product.Sku} | Name: {product.Name} | Price: {product.FormattedPrice}");
				}
			}
		}
		void getBoughtProducts(Message<PurchaseList> message, bool isFromCache)
		{
			if (message.IsError)
			{
				UnityEngine.Debug.LogError(string.Format("[{0}] Failed to get bought products (from cache: {1}): {2}", "initVROculus", isFromCache, message.GetError().Message));
				if (!isFromCache)
				{
					IAP.GetViewerPurchasesDurableCache().OnComplete(delegate(Message<PurchaseList> msg)
					{
						getBoughtProducts(msg, isFromCache: true);
					});
				}
				return;
			}
			PurchaseList purchaseList = message.GetPurchaseList();
			UnityEngine.Debug.Log(string.Format("[{0}] There are {1} bought products.", "initVROculus", purchaseList.Count));
			for (int num = 0; num < purchaseList.Count; num++)
			{
				Purchase purchase = purchaseList[num];
				UnityEngine.Debug.Log($"Product #{num} | SKU: {purchase.Sku} | Grant Time: {purchase.GrantTime} | ID: {purchase.ID}");
				foreach (DLC value in Enum.GetValues(typeof(DLC)))
				{
					if (!(value.ToString() != purchase.Sku))
					{
						if (!VR.oculusBoughtDLCs.Contains(value))
						{
							VR.oculusBoughtDLCs.Add(value);
						}
						break;
					}
				}
			}
			foreach (DLC oculusPlannedDLC in VR.oculusPlannedDLCs)
			{
				bool flag = VR.oculusBoughtDLCs.Contains(oculusPlannedDLC);
				bool flag2 = hasAllDlcLevelsInstalled(oculusPlannedDLC);
				UnityEngine.Debug.Log(string.Format("[{0}] {1} DLC | Bought: {2} | Installed: {3}", "getBoughtProducts", oculusPlannedDLC, flag, flag2));
				if (!flag && flag2)
				{
					int count = RoomDatabase.getAllRooms(oculusPlannedDLC, Is.DebugBuild).Count;
					for (int num2 = 1; num2 <= count; num2++)
					{
						string text = Path.Combine("/sdcard/Android/obb/com.PineStudio.EscapeSimulator", $"{oculusPlannedDLC.ToString().ToLower()}{num2}");
						if (File.Exists(text))
						{
							UnityEngine.Debug.Log(string.Format("[{0}] Deleting {1} DLC level '{2}' as DLC is not bought.", "getBoughtProducts", oculusPlannedDLC, text));
							File.Delete(text);
						}
					}
					dlcInstalled[(int)oculusPlannedDLC] = false;
				}
			}
		}
		void getLoggedInUser(Message<User> message)
		{
			if (message.IsError)
			{
				UnityEngine.Debug.LogError("[initVROculus] Failed to get logged in user info: " + message.GetError().Message);
			}
			else
			{
				User data = message.Data;
				VR.oculusId = data.ID;
				VR.oculusImageUrl = data.ImageURL;
				UnityEngine.Debug.Log(string.Format("[{0}] ID: {1}, Image URL: {2}", "initVROculus", VR.oculusId, VR.oculusImageUrl));
				loadTextureAsync(VR.oculusImageUrl, getLoggedInUserImage);
				Users.Get(VR.oculusId).OnComplete(getLoggedInUserDisplayName);
				Users.GetUserProof().OnComplete(getUserProof);
				requestMicrophonePermissionIfNeeded();
				string[] array = new string[VR.oculusAvailableDLCs.Count];
				for (int i = 0; i < VR.oculusAvailableDLCs.Count; i++)
				{
					array[i] = VR.oculusAvailableDLCs[i].ToString();
				}
				IAP.GetProductsBySKU(array).OnComplete(getAvailableProducts);
				IAP.GetViewerPurchases().OnComplete(delegate(Message<PurchaseList> msg)
				{
					getBoughtProducts(msg, isFromCache: false);
				});
			}
		}
		static void getLoggedInUserDisplayName(Message<User> message)
		{
			if (message.IsError)
			{
				UnityEngine.Debug.LogError("[initVROculus] Failed to get logged in user display-name: " + message.GetError().Message);
				VR.oculusUsername = Net.botNames[UnityEngine.Random.Range(0, Net.botNames.Length)];
			}
			else
			{
				VR.oculusUsername = message.Data.DisplayName;
				UnityEngine.Debug.Log("[initVROculus] Username: " + VR.oculusUsername);
			}
		}
		static void getLoggedInUserImage(Texture2D image)
		{
			if (image == null)
			{
				UnityEngine.Debug.LogError("[initVROculus] Failed to load profile picture texture from URL '" + VR.oculusImageUrl + "'.");
			}
			else
			{
				VR.oculusImage = image;
				UnityEngine.Debug.Log(string.Format("[{0}] Loaded {1}x{2} profile picture texture.", "initVROculus", VR.oculusImage.width, VR.oculusImage.height));
			}
		}
		void getUserProof(Message<UserProof> message)
		{
			string nonce;
			if (message.IsError)
			{
				UnityEngine.Debug.LogError("[getUserProof] Failed to get logged in user proof: " + message.GetError().Message);
			}
			else
			{
				nonce = message.Data.Value;
				StartCoroutine(getOculusToken());
				UnityEngine.Debug.Log("[getUserProof] Nonce: " + nonce);
			}
			IEnumerator getOculusToken()
			{
				using UnityWebRequest webRequest = UnityWebRequest.Get("https://ugc.escapesimulator.com/oculus_token");
				webRequest.SetRequestHeader("UserId", VR.oculusId.ToString());
				webRequest.SetRequestHeader("Nonce", nonce);
				yield return webRequest.SendWebRequest();
				if (webRequest.result != UnityWebRequest.Result.Success)
				{
					UnityEngine.Debug.LogError("[getOculusToken] Request error: " + webRequest.error);
				}
				else
				{
					string text = webRequest.downloadHandler.text;
					if (string.IsNullOrEmpty(text) || text.StartsWith("error:"))
					{
						UnityEngine.Debug.LogError("[getOculusToken] Token error: " + text);
						VR.oculusToken = string.Empty;
					}
					else
					{
						UnityEngine.Debug.Log("[getOculusToken] Token: " + text);
						VR.oculusToken = text;
						initPineWorkshop();
					}
				}
			}
		}
		void performEntitlementCheck(Message message)
		{
			if (message.IsError)
			{
				UnityEngine.Debug.LogError("[initVROculus] Entitlement check failed: " + message.GetError().Message);
				showEntitlementCheckFailedMessage();
			}
			else
			{
				UnityEngine.Debug.Log("[initVROculus] Entitlement check successful.");
				Users.GetLoggedInUser().OnComplete(getLoggedInUser);
				if (UnityEngine.Application.internetReachability == NetworkReachability.NotReachable)
				{
					showInternetNotAvailableMessage();
				}
				string loggedInUserLocale = Users.GetLoggedInUserLocale();
				Language language;
				switch (loggedInUserLocale)
				{
				case "en_US":
				case "en_GB":
					language = Language.English;
					break;
				case "zh_CN":
					language = Language.ChineseSimplified;
					break;
				case "zh_HK":
				case "zh_TW":
					language = Language.ChineseTraditional;
					break;
				case "ja_JP":
					language = Language.Japanese;
					break;
				case "ko_KR":
					language = Language.Korean;
					break;
				case "de_DE":
					language = Language.German;
					break;
				case "fr_FR":
					language = Language.French;
					break;
				case "tr_TR":
					language = Language.Turkish;
					break;
				case "es_ES":
					language = Language.Spanish;
					break;
				case "es_LA":
					language = Language.SpanishLatinAmerica;
					break;
				case "pt_PT":
					language = Language.Portuguese;
					break;
				case "pt_BR":
					language = Language.PortugueseBrasil;
					break;
				case "it_IT":
					language = Language.Italian;
					break;
				default:
					language = Language.English;
					break;
				}
				Language language2 = language;
				UnityEngine.Debug.Log(string.Format("[{0}] User's locale: {1}, game language: {2}", "initVROculus", loggedInUserLocale, language2));
				PlayerSave.Settings settings = PlayerSave.getSettings();
				if (!settings.isOculusDeviceLanguageSetup)
				{
					settings.language = (int)language2;
					settings.isOculusDeviceLanguageSetup = true;
					onChangeLanguage();
					PlayerSave.flush();
					if (game.menuOptions.languageDropdown != null)
					{
						game.menuOptions.languageDropdown.value = (int)language2;
					}
				}
			}
		}
		static void requestMicrophonePermissionIfNeeded()
		{
			VR.oculusHasMicrophonePermission = false;
			if (Is.Editor || Permission.HasUserAuthorizedPermission("android.permission.RECORD_AUDIO"))
			{
				VR.oculusHasMicrophonePermission = true;
			}
			else
			{
				PermissionCallbacks permissionCallbacks = new PermissionCallbacks();
				permissionCallbacks.PermissionDenied += delegate(string microphonePermission)
				{
					UnityEngine.Debug.Log("[requestMicrophonePermissionIfNeeded] Microphone permission denied: " + microphonePermission);
					VR.oculusHasMicrophonePermission = false;
				};
				permissionCallbacks.PermissionGranted += delegate(string microphonePermission)
				{
					UnityEngine.Debug.Log("[requestMicrophonePermissionIfNeeded] Microphone permission granted: " + microphonePermission);
					VR.oculusHasMicrophonePermission = true;
				};
				permissionCallbacks.PermissionDeniedAndDontAskAgain += delegate(string microphonePermission)
				{
					UnityEngine.Debug.Log("[requestMicrophonePermissionIfNeeded] Microphone permission denied and don't ask again: " + microphonePermission);
					VR.oculusHasMicrophonePermission = false;
				};
				Permission.RequestUserPermission("android.permission.RECORD_AUDIO", permissionCallbacks);
			}
		}
		static void showEntitlementCheckFailedMessage()
		{
		}
	}

	private void initVROculusForwardOrientationReset()
	{
		if (isVR() && Is.Android && OVRManager.display != null)
		{
			OVRManager.display.RecenteredPose -= resetForwardOrientation;
			OVRManager.display.RecenteredPose += resetForwardOrientation;
			UnityEngine.Debug.Log("Subscribed to event 'RecenteredPose'.");
		}
		void resetForwardOrientation()
		{
			vr.setRigPosition(vrMenuSpawnPoint.position);
			vr.setRigDirection(vrPlane.transform.position - vr.rig.transform.position);
			UnityEngine.Debug.Log("'resetForwardOrientation' invoked.");
		}
	}

	private void showInternetNotAvailableMessage(string messageKeyOverride = null)
	{
	}

	private void updateVR()
	{
		if (vr.isActive())
		{
			vr.updateVRSimulatorInput();
			vr.update();
			vr.updateControllerRayBasedOnUI(2, vrInputModule);
			vr.updateControllerRayBasedOnUI(1, vrInputModule);
			updateVRInitialLookDirection();
			updateVRForwardOrientationReset();
			if (vr.input.RightHand.SecondaryButton.WasPressedThisFrame())
			{
				onBack(fromUIButton: false);
			}
			updateJoinByCodeInVR();
			if (vr.getActiveState() == VR.State.Simulator)
			{
				Cursor.lockState = CursorLockMode.Locked;
			}
			if (vrCurtainsDelay > 0f)
			{
				vrCurtainsDelay -= Time.deltaTime;
			}
			else
			{
				vrCurtainsCurrent = Mathf.MoveTowards(vrCurtainsCurrent, vrCurtainsCurrentGoal, Time.deltaTime);
				float time = UnityUtils.map(0f, 1f, 0f, vrCurtains.clip.length, vrCurtainsCurrent);
				vrCurtains.clip.SampleAnimation(vrCurtains.gameObject, time);
			}
			float target = ((vrCurtainsCurrent < 0.5f) ? 1f : vrElevatorCurrentGoal);
			vrElevatorCurrent = Mathf.MoveTowards(vrElevatorCurrent, target, Time.deltaTime * 0.5f);
			Vector3 position = Vector3.Lerp(vrElevatorTop.transform.position, vrElevatorBot.transform.position, Mathf.SmoothStep(0f, 1f, vrElevatorCurrent));
			vrElevator.transform.position = position;
			if (vrInitialLookDirectionInited)
			{
				vrHeadsetInterruptHandler.update(vrMenuSpawnPoint.position);
			}
			if (Is.Oculus)
			{
				updateVROculus();
			}
		}
	}

	private void updateVRInitialLookDirection()
	{
		if (!vrInitialLookDirectionInited && (vr.getActiveState() != VR.State.VR || !(vr.input.HMD.hmdPosition.ReadValue<Vector3>() == Vector3.zero)))
		{
			vr.setRigDirection(vrPlane.transform.position - vr.rig.transform.position);
			vrInitialLookDirectionInited = true;
		}
	}

	private void updateVRForwardOrientationReset()
	{
		if (!Is.Android)
		{
			return;
		}
		if (vr.input.LeftHand.Menu.IsPressed())
		{
			float num = vrMenuButtonPressDuration;
			vrMenuButtonPressDuration += Time.deltaTime;
			if (num < 0.5f && vrMenuButtonPressDuration >= 0.5f)
			{
				VR.debugLog("Readjusting forward orientation...");
				vr.setRigPosition(vrMenuSpawnPoint.position);
				vr.setRigDirection(vrPlane.transform.position - vr.rig.transform.position);
			}
		}
		if (vr.input.LeftHand.Menu.WasReleasedThisFrame())
		{
			vrMenuButtonPressDuration = 0f;
			VR.debugLog("Released left hand menu button.");
		}
		if (vr.input.RightHand.Menu.WasReleasedThisFrame())
		{
			VR.debugLog("Released right hand menu button.");
		}
	}

	private void updateJoinByCodeInVR()
	{
		if (menuUI.ClientCode.gameObject.activeInHierarchy && vr.input.RightHand.PrimaryButton.WasPressedThisFrame())
		{
			onKeyboardDeletePressed();
		}
	}

	private void onKeyboardDeletePressed()
	{
		string text = menuUI.ClientCode.InputField.text;
		if (text.Length > 0)
		{
			menuUI.ClientCode.InputField.text = text.Substring(0, text.Length - 1);
		}
	}

	private void updateVROculus()
	{
		if (!Is.Oculus)
		{
			return;
		}
		foreach (DLC oculusAvailableDLC in VR.oculusAvailableDLCs)
		{
			if (!menuUI.OptionDialog.gameObject.activeInHierarchy && !VR.oculusBoughtDLCsWhoseDownloadWasDismissed.Contains(oculusAvailableDLC) && VR.oculusBoughtDLCs.Contains(oculusAvailableDLC) && !hasAllDlcLevelsInstalled(oculusAvailableDLC, shouldLog: false))
			{
				showDownloadOculusDlcMessage(oculusAvailableDLC);
				break;
			}
		}
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		vr?.handleApplicationFocusChange(hasFocus);
	}
}
