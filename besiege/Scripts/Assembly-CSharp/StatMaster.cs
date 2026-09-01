using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class StatMaster : SingleInstance<StatMaster>
{
	public enum Tool
	{
		Translate = 0,
		Rotate = 1,
		Scale = 2,
		Mirror = 3,
		Erase = 4,
		Modify = 5,
		Paint = 6,
		None = 7
	}

	public enum Category
	{
		Buildings = 0,
		Props = 1,
		Brick = 2,
		Animals = 3,
		Humans = 4,
		Weaponry = 5,
		EnvironmentFoliage = 6,
		Primitives = 7,
		Virtual = 8,
		[Obsolete]
		Weather = 9,
		All = 10
	}

	public enum ServerResponseType
	{
		MachineLoad = 1,
		SpectatorToggle = 2,
		ChangePlayMode = 4,
		RequestVote = 8,
		ClusterResults = 0x10,
		SetSpawnZone = 0x20,
		ToggleLocalSim = 0x40
	}

	public class Mode
	{
		public enum PickMode
		{
			None = 0,
			Entity = 1,
			Zone = 2,
			All = 3,
			SpawnZone = 4,
			Trigger = 5
		}

		public class Symmetry
		{
			public static bool placement = true;

			public static bool selection = true;

			public static bool eraser = true;

			public static bool modifying;
		}

		public class Transform
		{
			public class Snap
			{
				public static float position = 0.5f;

				public static float rotation = 45f;

				public static event Action OnChanged;

				public static void InvokeOnChanged()
				{
					if (Snap.OnChanged != null)
					{
						Snap.OnChanged();
					}
				}
			}

			public static bool global = true;

			public static bool pivot = true;

			public static bool linked = true;
		}

		public class LevelEditor
		{
			protected static Tool _selectedTool = Tool.None;

			public static bool moveMachineWithZone = true;

			public static bool isSelectingLevel;

			public static bool clientGlobalSim = true;

			public static bool clientSimControl = true;

			public static bool global = true;

			public static bool linked = true;

			public static bool grid = true;

			public static bool objectPivot = true;

			public static bool paintPlacement;

			public static float minRandomScaleX = 0.8f;

			public static float minRandomScaleY = 0.8f;

			public static float minRandomScaleZ = 0.8f;

			public static float maxRandomScaleX = 1.2f;

			public static float maxRandomScaleY = 1.2f;

			public static float maxRandomScaleZ = 1.2f;

			public static float minRandomRot;

			public static float maxRandomRot = 360f;

			public static Tool selectedTool
			{
				get
				{
					return _selectedTool;
				}
				set
				{
					_selectedTool = value;
					ToggleLevelEditing(_selectedTool == Tool.None && SelectedLevelPrefab == null);
				}
			}
		}

		public static bool isTranslating;

		public static bool isRotating;

		public static bool isScaling;

		public static bool placingBlock;

		public static BlockTransformTool currentBlockTool;

		private static PickMode _pickMode;

		public static bool keyMapView;

		public static bool levelEdit = true;

		public static bool curtainMode;

		public static bool hideLabels;

		public static bool allowClone;

		public static Tool previousTool = Tool.None;

		protected static Tool _selectedTool = Tool.None;

		public static bool selectSymmetryPivot;

		public static bool allowIntersection;

		private static bool _displayDrag;

		public static Action AeroDisplayChanged;

		public static Action BeforeSelectionChanged;

		public static Action SelectionChanged;

		public static PickMode pickMode
		{
			get
			{
				return _pickMode;
			}
			set
			{
				_pickMode = value;
				UpdatePickCursor();
			}
		}

		public static Tool selectedTool
		{
			get
			{
				return _selectedTool;
			}
			set
			{
				if (_selectedTool != value)
				{
					previousTool = _selectedTool;
					_selectedTool = value;
					ChangeTool(value);
				}
			}
		}

		public static bool displayDrag
		{
			get
			{
				return _displayDrag;
			}
			set
			{
				_displayDrag = value;
				InvokeAeroDisplayChanged();
			}
		}

		public static event ToolChanged ToolChanged;

		private static void UpdatePickCursor()
		{
			bool flag = _pickMode != PickMode.None;
			Cursor.SetCursor((!flag) ? null : ReferenceMaster.Instance.pickerCursor, (!flag) ? Vector2.zero : new Vector2((float)ReferenceMaster.Instance.pickerCursor.width * 0.2f, (float)ReferenceMaster.Instance.pickerCursor.height * 0.8f), CursorMode.Auto);
		}

		public static void ChangeTool(Tool t)
		{
			if (Mode.ToolChanged != null)
			{
				Mode.ToolChanged(t);
			}
			if (AeroDynamicDisplay.IsSelected)
			{
				AeroDynamicDisplay.Select(false);
			}
		}

		public static void InvokeAeroDisplayChanged()
		{
			if (AeroDisplayChanged != null)
			{
				AeroDisplayChanged();
			}
		}
	}

	public class Bounding
	{
		public static bool Enabled = true;

		public static ZoneRotationMode zoneRotationMode;

		public static Vector3 worldCenter;

		public static Vector3 worldExtents;

		public static Plane[] worldBounds;

		public static float floorPos = 10000f;

		public static float roofHeight = 10000f;

		public static float frontPos = 10000f;

		public static float backPos = -10000f;

		public static float leftPos = -10000f;

		public static float rightPos = 10000f;

		public static bool inGround;

		public static bool inRoof;

		public static bool inRightWall;

		public static bool inLeftWall;

		public static bool inFrontWall;

		public static bool inBackWall;
	}

	public class KeyMapper
	{
		public static bool disableSliderLimits;

		public static int VariableCharLimit = 32;

		public static int MaxDisplayedTags = 100;

		public static bool multipleStartingBlocks;

		public static bool allowSelectingNodes;
	}

	public class GodTools
	{
		public static bool PyroMode;

		public static bool DragMode;

		public static bool UnbreakableMode;

		public static bool InfiniteAmmoMode;

		public static bool GravityDisabled;

		public static bool ExplodingCannonballs;

		public static bool HasBeenUsed;

		public static void ResetGodTools()
		{
			PyroMode = false;
			DragMode = false;
			UnbreakableMode = false;
			InfiniteAmmoMode = false;
			GravityDisabled = false;
			ExplodingCannonballs = false;
			HasBeenUsed = false;
		}

		public static bool GodToolsUsed()
		{
			if (ReferenceMaster.activeMachineSimulating)
			{
				if (UnbreakableMode || GravityDisabled || HasBeenUsed)
				{
					HasBeenUsed = true;
					return true;
				}
				return false;
			}
			HasBeenUsed = false;
			return false;
		}
	}

	public class Rules
	{
		private static bool _disableExplosions;

		private static bool _disableProjectiles;

		private static bool _disableFire;

		public static bool DisableExplosions
		{
			get
			{
				return isMP && !Mode.levelEdit && _disableExplosions;
			}
			set
			{
				_disableExplosions = value;
			}
		}

		public static bool DisableProjectiles
		{
			get
			{
				return isMP && !Mode.levelEdit && _disableProjectiles;
			}
			set
			{
				_disableProjectiles = value;
			}
		}

		public static bool DisableFire
		{
			get
			{
				return isMP && !Mode.levelEdit && _disableFire;
			}
			set
			{
				_disableFire = value;
			}
		}
	}

	public static bool SimulationStartInProgress = false;

	private static LevelSettings.LevelEnvironment levelEnvironment;

	private static SimulationState simulationState;

	private static SimulationState previousSimulationState;

	public static bool isLoadingLevels;

	public bool isDeveloper;

	public BuildSettingsObject BuildSettings;

	public bool OverrideIsDebug;

	public bool OverrideLowViolence;

	public bool DisableWorkShopUploads;

	public static bool outlineBlocks = true;

	public static bool showOutline = true;

	public static bool isPaste = false;

	public static readonly int DefaultPort = 7777;

	public static Action totalBlocksChanged;

	public static Action<bool> onKeyMapView;

	public static Action<int> entityCountChanged;

	public static bool PopupExceptions = true;

	public static bool SavingXML = false;

	public static bool UnlockSpeedSliders = false;

	public static float SurfaceEdgeMovement = 0.5f;

	public static bool UseJointParenting = true;

	public static bool useSmartInterpolation = false;

	public static bool deleteVisualControllersInSimulation = false;

	public static bool handleCrossPatternJoints = false;

	public static bool mergeSurfaceTypesOnDeselect = true;

	public static int currentIslandID = 0;

	public static bool wasSimulating = false;

	public static bool cachingTransformActions = false;

	public static bool clusterCoded = false;

	public static bool aeroCoded = false;

	public static bool stressCoded = false;

	public static bool highQualityExplosions = true;

	private static LevelPrefab _selectedLevelPrefab;

	public static bool IsGlobalWindPresent = false;

	private static int waitServerResponse = 0;

	public static bool limitMachines;

	public static bool waitingForSim = false;

	public static string lastLoadedLevel = string.Empty;

	public static bool allowFullRebinding = false;

	public static bool textFieldSelected = false;

	public static bool allowScrollRebind = true;

	public static bool stopCamZoom = false;

	public static bool isSearching = false;

	public static bool isHeadless = false;

	public static bool networkActive = false;

	public static bool isHosting = false;

	public static bool isClient = false;

	public static bool isMP = false;

	public static bool initializingHostEnvironment = false;

	public static bool hostDisabledDLC = false;

	private static bool isLevelEditorOnly = false;

	public static Action levelEditorOnlyChanged;

	public static Action inMenuChanged;

	public static bool isMainMenu = false;

	public static int activePlayerCount = 0;

	public static bool collapseSkinMapper = false;

	public static float TotalMass = 0f;

	public static int BlockCount = 0;

	public static bool hudOccluding = false;

	public static bool gizmoOccluding = false;

	public static bool hudHidden = false;

	protected static bool _levelSimulating = false;

	public static bool startingMachines = false;

	public static bool isLocalSim = false;

	public static bool _customLevelSimulating = false;

	public static bool IgnoreLevelTriggerResults = false;

	private static string externalIP = string.Empty;

	public static bool ShowNetworkStats;

	public static string LEVEL_FILE_EXTENSION = "blv";

	public static bool ToolActive = false;

	public static bool isServer = false;

	public static Color BloodColor = new Color(0.933f, 0.1098f, 0.14117f);

	public static LevelSettings.LevelEnvironment DefaultLevelEnvironment = LevelSettings.LevelEnvironment.Barren;

	public static string DefaultMPLevel;

	public override string Name
	{
		get
		{
			return "StatMaster";
		}
	}

	public static BesiegePlayMode PlayMode
	{
		get
		{
			return PlayerData.hasLocalPlayer ? PlayerData.localPlayer.PlayMode : ((!levelSimulating) ? BesiegePlayMode.BuildMode : BesiegePlayMode.GlobalSimulation);
		}
	}

	public static BesiegePlayMode PreviousPlayMode
	{
		get
		{
			return (!PlayerData.hasLocalPlayer) ? BesiegePlayMode.BuildMode : PlayerData.localPlayer.PreviousPlayMode;
		}
	}

	public static bool InGlobalPlayMode
	{
		get
		{
			return PlayMode == BesiegePlayMode.GlobalSimulation;
		}
	}

	public static bool InLocalPlayMode
	{
		get
		{
			return PlayMode == BesiegePlayMode.LocalSimulation;
		}
	}

	public static bool InBuildPlayMode
	{
		get
		{
			return PlayMode == BesiegePlayMode.LocalSimulation;
		}
	}

	public static bool WasInGlobalPlayMode
	{
		get
		{
			return PreviousPlayMode == BesiegePlayMode.GlobalSimulation;
		}
	}

	public static bool SwitchingStates
	{
		get
		{
			return simulationState == SimulationState.SwitchingToLocalSimulation || simulationState == SimulationState.SwitchingToGlobalSimulation || simulationState == SimulationState.SwitchingToBuildMode || simulationState == SimulationState.WaitingOnMachineReady || simulationState == SimulationState.PendingReadyVote;
		}
	}

	public static LevelSettings.LevelEnvironment LevelEnvironment
	{
		get
		{
			return levelEnvironment;
		}
	}

	public static bool ShowExplosionDecals
	{
		get
		{
			return !isMP || levelEnvironment != LevelSettings.LevelEnvironment.MountainTop;
		}
	}

	public static SimulationState SimulationState
	{
		get
		{
			return simulationState;
		}
	}

	public static SimulationState PreviousSimulationState
	{
		get
		{
			return previousSimulationState;
		}
	}

	public bool isDebug
	{
		get
		{
			return BuildSettings.IsDebug;
		}
		set
		{
			BuildSettings.IsDebug = value;
		}
	}

	public bool LowViolence
	{
		get
		{
			return BuildSettings.LowViolence;
		}
	}

	public static int MaxFPS
	{
		get
		{
			int num = OptionsMaster.GetFPSLock();
			if (num == -1)
			{
				num = int.MaxValue;
			}
			return num;
		}
	}

	public static LevelPrefab SelectedLevelPrefab
	{
		get
		{
			return _selectedLevelPrefab;
		}
		set
		{
			_selectedLevelPrefab = value;
			ToggleLevelEditing(Mode.LevelEditor.selectedTool == Tool.None && _selectedLevelPrefab == null);
		}
	}

	public static bool waitingForServerResponse
	{
		get
		{
			return waitServerResponse > 0;
		}
	}

	public static bool stopWASDcamMovement
	{
		get
		{
			return stopWasdCounter > 0;
		}
	}

	public static int stopHotCounter { get; private set; }

	public static int stopWasdCounter { get; private set; }

	public static int stopZoomCounter { get; private set; }

	public static bool stopHotkeys
	{
		get
		{
			return stopHotCounter > 0;
		}
	}

	public static bool disableCameraZoom
	{
		get
		{
			return stopZoomCounter > 0;
		}
	}

	public static bool HasGameState
	{
		get
		{
			return isMP && NetworkAuxAddPiece.hasInstance && NetworkAuxAddPiece.Instance.receivedGameState;
		}
	}

	public static bool IsLevelEditorOnly
	{
		get
		{
			return isLevelEditorOnly;
		}
		set
		{
			isLevelEditorOnly = value;
			if (levelEditorOnlyChanged != null)
			{
				levelEditorOnlyChanged();
			}
		}
	}

	public static bool advancedBuilding
	{
		get
		{
			return OptionsMaster.BesiegeConfig.AdvancedBuilding;
		}
	}

	public static int inMenuCounter { get; private set; }

	public static bool inMenu
	{
		get
		{
			return inMenuCounter > 0;
		}
	}

	public static bool levelSimulating
	{
		get
		{
			return (!isMP) ? _levelSimulating : _customLevelSimulating;
		}
		set
		{
			_levelSimulating = value;
		}
	}

	public static string ExternalIP
	{
		get
		{
			return (!string.IsNullOrEmpty(externalIP)) ? externalIP : SingleInstanceFindOnly<NetworkAnalyser>.Instance.ExternalIP;
		}
		set
		{
			externalIP = value;
		}
	}

	public static ulong FacilitatorGUID
	{
		get
		{
			return SingleInstanceFindOnly<NetworkAnalyser>.Instance.FacilitatorController.FacilitatorGuid;
		}
	}

	public static bool LimitMachineModification
	{
		get
		{
			return !Mode.levelEdit && limitMachines && !LevelEditor.Instance.Settings.AllowModMachines;
		}
	}

	public static BlockType SelectedBlockId { get; set; }

	public static event Action hudHiddenChanged;

	public static event ChangedSelectedBlock SelectedBlockChanged;

	public static event LevelEditingToggled LevelEditingToggled;

	public static void SetLevelEnvironment(LevelSettings.LevelEnvironment env)
	{
		if (env != levelEnvironment)
		{
			levelEnvironment = env;
		}
	}

	public static void SetSimulationState(SimulationState state)
	{
		if (state != simulationState)
		{
			previousSimulationState = simulationState;
			simulationState = DetermineCorrectState(state);
			if (BesiegeLogFilter.logDev)
			{
				Debug.Log(string.Concat("ChangeSimulationState: ", previousSimulationState, " => ", simulationState));
			}
		}
	}

	public static void UpdateSimulationState()
	{
		SimulationState simulationState = DetermineCorrectState(StatMaster.simulationState);
		if (simulationState != StatMaster.simulationState)
		{
			SetSimulationState(simulationState);
		}
	}

	public static void ResetStateSettings()
	{
		isHosting = (isClient = (waitingForSim = (networkActive = (cachingTransformActions = false))));
		inMenuCounter = 0;
		ResetWaitForServerResponse();
		activePlayerCount = 0;
		isLocalSim = false;
		levelSimulating = (_customLevelSimulating = false);
		previousSimulationState = (simulationState = SimulationState.SpectatorMode);
		PlayerData.hasLocalPlayer = false;
		PlayerData.localPlayer = null;
		Mode.levelEdit = (Mode.LevelEditor.clientGlobalSim = (Mode.LevelEditor.clientSimControl = true));
	}

	private static SimulationState DetermineCorrectState(SimulationState requestedState)
	{
		bool flag = Playerlist.HasRemoteLocalSimulations();
		switch (requestedState)
		{
		case SimulationState.BuildMode:
		case SimulationState.BuildModeGlobalSim:
		case SimulationState.BuildModeRemoteLocalSim:
		case SimulationState.BuildModeGlobalSimRemoteLocalSim:
			requestedState = ((!levelSimulating) ? ((!flag) ? SimulationState.BuildMode : SimulationState.BuildModeRemoteLocalSim) : ((!flag) ? SimulationState.BuildModeGlobalSim : SimulationState.BuildModeGlobalSimRemoteLocalSim));
			break;
		case SimulationState.GlobalSimulation:
		case SimulationState.GlobalSimulationRemoteLocalSim:
			requestedState = ((!flag) ? SimulationState.GlobalSimulation : SimulationState.GlobalSimulationRemoteLocalSim);
			break;
		case SimulationState.LocalSimulation:
		case SimulationState.LocalSimulationRemoteLocalSim:
			requestedState = ((!flag) ? SimulationState.LocalSimulation : SimulationState.LocalSimulationRemoteLocalSim);
			break;
		}
		return requestedState;
	}

	public static Island GetCurrentIsland()
	{
		if (isMP)
		{
			return Island.None;
		}
		switch (currentIslandID)
		{
		case 0:
			return Island.Ipsilon;
		case 1:
			return Island.Tolbrynd;
		case 2:
			return Island.Valfross;
		case 3:
			return Island.Krolmar;
		case 4:
			return Island.Water;
		case -2:
			return Island.WaterSandbox;
		default:
			return Island.None;
		}
	}

	public static void ResetWaitForServerResponse()
	{
		waitServerResponse = 0;
	}

	public static void WaitForServerResponse(ServerResponseType responseType, bool toggle)
	{
		if (toggle != (((uint)waitServerResponse & (uint)responseType) != 0))
		{
			if (toggle)
			{
				waitServerResponse |= (int)responseType;
			}
			else
			{
				waitServerResponse &= (int)(~responseType);
			}
		}
	}

	public static void StopHotKeys(bool value)
	{
		if (value)
		{
			stopHotCounter++;
			return;
		}
		stopHotCounter--;
		if (stopHotCounter < 0)
		{
			Debug.LogError("stopHotCounter < 0!");
			stopHotCounter = 0;
		}
	}

	public static void DelayStopHotKeys(bool value)
	{
		SingleInstance<StatMaster>.Instance.StartCoroutine(SingleInstance<StatMaster>.Instance.IEDelayStopHotKeys(value, Time.deltaTime * 2f));
	}

	public IEnumerator IEDelayStopHotKeys(bool value, float delay)
	{
		yield return new WaitForSeconds(delay);
		StopHotKeys(value);
	}

	public static void StopCameraKeys(bool value)
	{
		if (value)
		{
			stopWasdCounter++;
			return;
		}
		stopWasdCounter--;
		if (stopWasdCounter < 0)
		{
			stopWasdCounter = 0;
		}
	}

	public static void DisableCameraZoom(bool value)
	{
		if (value)
		{
			stopZoomCounter++;
			return;
		}
		stopZoomCounter--;
		if (stopZoomCounter < 0)
		{
			stopZoomCounter = 0;
		}
	}

	public static void InvokeHudHiddenChanged()
	{
		if (StatMaster.hudHiddenChanged != null)
		{
			StatMaster.hudHiddenChanged();
		}
	}

	public static void ChangeSelectedBlock(BlockType id)
	{
		SelectedBlockId = id;
		if (StatMaster.SelectedBlockChanged != null)
		{
			StatMaster.SelectedBlockChanged(id);
		}
	}

	protected static void ToggleLevelEditing(bool enabled)
	{
		if (StatMaster.LevelEditingToggled != null)
		{
			StatMaster.LevelEditingToggled(enabled);
		}
	}

	public static void SetLogFilter(int filterLevel)
	{
		BesiegeLogFilter.currentLogLevel = filterLevel;
		LogFilter.currentLogLevel = BesiegeLogFilter.currentLogLevel;
	}

	public static void SetInMenu(bool isInMenu)
	{
		if (isInMenu)
		{
			inMenuCounter++;
		}
		else if (inMenuCounter > 0)
		{
			inMenuCounter--;
		}
		if (inMenuChanged != null)
		{
			inMenuChanged();
		}
		if (BesiegeLogFilter.logDev)
		{
			Debug.Log("SetInMenu(" + inMenuCounter + "): " + isInMenu);
		}
	}

	private void Awake()
	{
		TotalMass = 0f;
		BlockCount = 0;
		SceneManager.sceneLoaded += OnSceneLoad;
	}

	public override void SetUp()
	{
		BuildSettings = Resources.Load<BuildSettingsObject>("BuildSettingsObject");
		if (BuildSettings == null)
		{
			Debug.LogWarning("BuildSettingsObject not found in build, creating default one");
			BuildSettings = ScriptableObject.CreateInstance<BuildSettingsObject>();
		}
	}

	private void OnSceneLoad(Scene scene, LoadSceneMode m)
	{
		Mode.isTranslating = false;
		Mode.isRotating = false;
		Mode.isScaling = false;
		Mode.keyMapView = false;
		Mode.selectedTool = Tool.None;
		Mode.selectSymmetryPivot = false;
		Bounding.Enabled = true;
	}
}
