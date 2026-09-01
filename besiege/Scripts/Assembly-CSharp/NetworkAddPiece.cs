using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Localisation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[AddComponentMenu("Core/Multiplayer/Network Add Piece")]
public class NetworkAddPiece : AddPiece
{
	public const int WORLD_BOUND_WIDTH = 20;

	public Text networkInfo;

	public Renderer voteIcon;

	public Renderer stopVoteIcon;

	public Renderer playIcon;

	public Renderer stopIcon;

	public PlayerViewer playerViewer;

	public LocalSimWarning localSimWarning;

	public LevelDataManager dataManager;

	public uint frame;

	public Bounds worldBoundaries;

	public bool hasActiveMachines;

	private NetworkAuxAddPiece networkAuxAddPiece;

	private float lastUpdate;

	private float lastCamUpdate;

	private Transform camTransform;

	private float essentialThreshold = 10000f;

	private float halfEssentialThreshold;

	private ushort ownerId;

	private CustomLevel level;

	private LevelEditor levelEditor;

	private bool hasPolledLevel;

	private BesiegeNetworkManager networkManager;

	private Dictionary<uint, ServerMachine> activePhysicsMachines;

	private List<ServerMachine> simulatingMachines;

	private bool pollLevel;

	private bool lockedMessages;

	private ProjectileManager projectileManager;

	public float lastTimeScale;

	public float lastLocalTimeScale;

	public float lastAutoTimeScale;

	private VoteCountdownController voteCountdown;

	private NetworkScene networkScene;

	private PerformanceAnalyser perfAnalyser;

	private IEnumerator setTimeScaleCoroutine;

	public FragmentedRPC clientInputBuffer;

	private byte clientInputID;

	public static float SIZE = 2f;

	public static float DEF_MAX = 1500f;

	public static float DEF_MIN = -25f;

	public static float WATER_MIN = -100f;

	protected int numberRespawning;

	protected int currentRespawn;

	public new static NetworkAddPiece Instance
	{
		get
		{
			return SingleInstanceFindOnly<AddPiece>.Instance as NetworkAddPiece;
		}
	}

	public bool GetActiveMachine(uint index, out ServerMachine machine)
	{
		return activePhysicsMachines.TryGetValue(index, out machine);
	}

	protected override void Awake()
	{
		base.Awake();
		lastAutoTimeScale = (lastLocalTimeScale = (lastTimeScale = OptionsMaster.defaultTimeScale));
		halfEssentialThreshold = essentialThreshold * 0.5f;
		voteIcon.enabled = false;
		stopVoteIcon.enabled = false;
		dataManager = new LevelDataManager();
		CalculateWorldBoundaries();
		simulatingMachines = new List<ServerMachine>();
		activePhysicsMachines = new Dictionary<uint, ServerMachine>();
		autoStartLevel = false;
		camTransform = Camera.main.transform;
		clientInputBuffer = new FragmentedRPC();
	}

	protected void Start()
	{
		level = CustomLevel.Instance;
		levelEditor = LevelEditor.Instance;
		networkAuxAddPiece = NetworkAuxAddPiece.Instance;
		projectileManager = ProjectileManager.Instance;
		voteCountdown = VoteCountdownController.Instance;
		networkScene = NetworkScene.Instance;
		VoteCountdownController.onFinish = OnCountdownReady;
		level.Init(levelEditor, dataManager);
		perfAnalyser = SingleInstance<PerformanceAnalyser>.Instance;
		level.ToggleSim(false);
	}

	protected override void OnSceneLoad(Scene scene, LoadSceneMode m)
	{
		if (AddPiece.IsMenuScene(scene.name))
		{
			base.OnSceneLoad(scene, m);
			StatMaster.isMP = false;
			networkScene.OnSceneChanged();
		}
	}

	public override void SetUp()
	{
		networkManager = BesiegeNetworkManager.Instance;
		base.SetUp();
	}

	public void SetOwner(ushort owner)
	{
		ownerId = owner;
		dataManager.Init();
	}

	public static void ScaleBoundary(BoxCollider b, LevelSettings.LevelEnvironment env)
	{
		float num = 2000f;
		float num2 = DEF_MAX;
		float num3 = DEF_MIN;
		switch (env)
		{
		case LevelSettings.LevelEnvironment.Water:
			num3 = WATER_MIN;
			break;
		case LevelSettings.LevelEnvironment.MountainTop:
			num2 -= 100f;
			num3 = -209f;
			break;
		}
		float num4 = num2 - num3;
		Transform child = b.transform.GetChild(0);
		Vector3 size = b.size;
		Vector3 position = b.transform.position;
		Vector3 localScale = child.transform.localScale;
		if (size.x > 500f)
		{
			size.x = SIZE * num;
			if (position.z != 0f)
			{
				position.z = SIZE * num * Mathf.Sign(position.z) * 0.5f;
			}
		}
		if (size.z > 500f)
		{
			size.z = SIZE * num;
			if (position.x != 0f)
			{
				position.x = SIZE * num * Mathf.Sign(position.x) * 0.5f;
			}
		}
		if (size.y < 500f)
		{
			position.y = num2;
			localScale.x = SIZE * num - 20f;
			localScale.y = SIZE * num - 20f;
		}
		else
		{
			position.y = (num2 + num3) * 0.5f;
			size.y = num4;
			localScale.x = SIZE * num - 20f;
			localScale.y = num4 - 10f;
		}
		b.size = size;
		b.transform.position = position;
		child.transform.localScale = localScale;
		Material material = child.GetComponent<MeshRenderer>().material;
		material.SetVector("_MainTex_ST", new Vector4(localScale.x * 0.1f, localScale.y * 0.1f, 0.5f, 0.5f));
	}

	public void CalculateWorldBoundaries()
	{
		GameObject gameObject = GameObject.Find("WORLD BOUNDARIES");
		if (gameObject == null)
		{
			Debug.LogError("Can't find level bounds!");
		}
		BoxCollider[] componentsInChildren = gameObject.GetComponentsInChildren<BoxCollider>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			ScaleBoundary(componentsInChildren[i], StatMaster.LevelEnvironment);
			Bounds bounds = componentsInChildren[i].bounds;
			if (i == 0)
			{
				worldBoundaries = bounds;
			}
			else
			{
				worldBoundaries.Encapsulate(bounds);
			}
		}
		worldBoundaries.extents = new Vector3(worldBoundaries.extents.x - 20f, worldBoundaries.extents.y - 10f, worldBoundaries.extents.z - 20f);
		worldBoundaries.center = new Vector3(worldBoundaries.center.x, worldBoundaries.center.y - 10f, worldBoundaries.center.z);
		NetworkCompression.SetWorldBounds(worldBoundaries);
		float num = 0f;
		num = ((StatMaster.LevelEnvironment != LevelSettings.LevelEnvironment.Water) ? ((0f - worldBoundaries.min.y) / 2f) : ((0f - worldBoundaries.min.y) / 2f + SingleInstanceFindOnly<AddPiece>.Instance.floorHeight));
		Vector3 vector = (StatMaster.Bounding.worldCenter = new Vector3(worldBoundaries.center.x, worldBoundaries.center.y + num, worldBoundaries.center.z));
		Vector3 vector2 = (StatMaster.Bounding.worldExtents = new Vector3(worldBoundaries.extents.x, worldBoundaries.extents.y - num, worldBoundaries.extents.z));
		StatMaster.Bounding.worldBounds = new Plane[6];
		StatMaster.Bounding.worldBounds[0] = new Plane(Vector3.left, new Vector3(vector.x - vector2.x, 0f, 0f));
		StatMaster.Bounding.worldBounds[1] = new Plane(Vector3.right, new Vector3(vector.x + vector2.x, 0f, 0f));
		StatMaster.Bounding.worldBounds[2] = new Plane(Vector3.down, new Vector3(0f, vector.y - vector2.y, 0f));
		StatMaster.Bounding.worldBounds[3] = new Plane(Vector3.up, new Vector3(0f, vector.y + vector2.y, 0f));
		StatMaster.Bounding.worldBounds[4] = new Plane(Vector3.back, new Vector3(0f, 0f, vector.z - vector2.z));
		StatMaster.Bounding.worldBounds[5] = new Plane(Vector3.forward, new Vector3(0f, 0f, vector.z + vector2.z));
	}

	public bool AutoSave(string fileName = null, bool overwrite = false, bool requireDirty = true)
	{
		if (!StatMaster.Mode.levelEdit || (requireDirty && !levelEditor.isDirty))
		{
			return false;
		}
		string levelAutosavePath = StaticSettings.LevelAutosavePath;
		string text = ((!string.IsNullOrEmpty(fileName)) ? fileName : ("autosave" + DateTime.Now.ToString("yyyyMMddHHmm")));
		if (!text.ToLower().EndsWith("." + StatMaster.LEVEL_FILE_EXTENSION))
		{
			text = text + "." + StatMaster.LEVEL_FILE_EXTENSION;
		}
		string path = Path.Combine(levelAutosavePath, text);
		if (!overwrite && File.Exists(path))
		{
			return false;
		}
		LevelXMLSaver.Create(path, "AutoSave");
		return true;
	}

	public void OnClientStop()
	{
		voteCountdown.StopCountdown();
		StopLocalMachine();
		if (StatMaster.levelSimulating)
		{
			ResetMapperTargets();
			ToggleLevelSimulation(false, true);
		}
		level.frameManager.Clear();
		level.logicFrameManager.Clear();
		AutoSave();
		StopAllCoroutines();
		if (StatMaster.startingMachines)
		{
			Physics.gravity = tempGrav;
			StatMaster.startingMachines = false;
		}
		if (lockedMessages)
		{
			LockNetworkMessages(false);
		}
		pollLevel = false;
		activePhysicsMachines.Clear();
		simulatingMachines.Clear();
		networkAuxAddPiece.ClearPlayers();
		LevelEditorUI levelEditorUI = SingleInstanceFindOnly<LevelEditorUI>.Instance;
		levelEditorUI.settingsWindow.SetActive(false);
		levelEditorUI.SetUIState(LevelEditorUI.UIState.Inactive);
		levelEditor.Reset();
		levelEditorUI.Toggle(false);
		float defaultTimeScale = OptionsMaster.defaultTimeScale;
		lastAutoTimeScale = (lastLocalTimeScale = (lastTimeScale = defaultTimeScale));
		TimeSlider.Instance.ResetScale(defaultTimeScale);
	}

	public void StopLocalMachine()
	{
		if (PlayerData.hasLocalPlayer)
		{
			PlayerData localPlayer = PlayerData.localPlayer;
			if (!localPlayer.isSpectator && localPlayer.machine.isSimulating)
			{
				stopIcon.enabled = false;
				playIcon.enabled = true;
				ServerMachine machine = localPlayer.machine;
				machine.EndSimulation();
				SetMiddle(machine.MiddlePosition);
				AdvancedUIController.Instance.ToggleAdvanced(StatMaster.advancedBuilding);
				SingleInstanceFindOnly<BarPositionController>.Instance.Set();
			}
		}
	}

	public void CloseEntityMapper()
	{
		BlockMapper currentInstance = BlockMapper.CurrentInstance;
		if (currentInstance != null && currentInstance.Current.infoType == BasicInfo.BasicInfoType.Entity)
		{
			reopenMode = ReopenMode.BlockMapper;
			lastBMTarget = currentInstance.Current;
			BlockMapper.Close();
		}
	}

	public void CloseBlockMapper()
	{
		BlockMapper currentInstance = BlockMapper.CurrentInstance;
		if (currentInstance != null && currentInstance.Current.infoType == BasicInfo.BasicInfoType.Block)
		{
			reopenMode = ReopenMode.BlockMapper;
			lastBMTarget = currentInstance.Current;
			BlockMapper.Close();
		}
	}

	public void ReopenEntityMapper()
	{
		if (reopenMode == ReopenMode.BlockMapper && lastBMTarget != null && lastBMTarget.infoType == BasicInfo.BasicInfoType.Entity)
		{
			ReopenBlockMapper(false);
		}
	}

	public override void SetBlockType(BlockType type)
	{
		if (base.CurrentType != type)
		{
			if (levelEditor != null)
			{
				levelEditor.ResetWindow();
			}
			base.SetBlockType(type);
		}
	}

	public void SaveTimeScale(float newScale, bool isAuto)
	{
		if (isAuto)
		{
			lastAutoTimeScale = newScale;
		}
		else
		{
			lastTimeScale = (lastAutoTimeScale = newScale);
		}
	}

	public byte[] GetTimeScale(float perc)
	{
		return new byte[1] { (byte)Mathf.RoundToInt(perc * 200f) };
	}

	public void SetTimeScale(byte[] timeScale, bool isAuto, bool animated = true)
	{
		float perc = (float)(int)timeScale[0] / 200f;
		if (isAuto)
		{
			lastAutoTimeScale = perc;
		}
		else
		{
			lastTimeScale = (lastAutoTimeScale = perc);
		}
		if (StatMaster.Mode.LevelEditor.clientGlobalSim)
		{
			SetTimeScale(perc, animated);
		}
	}

	public void SetTimeSinceLevelStartOffset(byte[] TSLSO, float timeCorrection)
	{
		float num = BitConverter.ToSingle(TSLSO, 0) + timeCorrection;
		WaterController.timeOffset = num - Time.timeSinceLevelLoad;
		Debug.Log("SetTimeSinceLevelStartOffset " + Time.timeSinceLevelLoad + " " + num + " > " + WaterController.timeOffset);
		Shader.SetGlobalFloat("_TimeOffset", WaterController.timeOffset);
	}

	public void SetTimeScale(float perc, bool animated)
	{
		if (setTimeScaleCoroutine != null)
		{
			StopCoroutine(setTimeScaleCoroutine);
		}
		if (animated)
		{
			setTimeScaleCoroutine = IESetTimeScale(perc, animated);
			StartCoroutine(setTimeScaleCoroutine);
		}
		else
		{
			TimeSlider.Instance.SetPercentage(perc);
		}
	}

	private IEnumerator IESetTimeScale(float perc, bool animated)
	{
		TimeSlider timeSlider = TimeSlider.Instance;
		float oldPercentage = timeSlider.percentagey;
		if (animated && oldPercentage != perc)
		{
			float interval = timeSlider.percSendInterval;
			float delta = 0f;
			while (delta < interval)
			{
				float percentage = Mathf.Lerp(oldPercentage, perc, Mathfx.GetHermiteValue(delta / interval));
				timeSlider.SetPercentage(percentage);
				delta += TimeSlider.Instance.deltaTime;
				yield return null;
			}
		}
		timeSlider.SetPercentage(perc);
	}

	public void UpdateBarController()
	{
		if (!StatMaster.isHeadless)
		{
			SingleInstanceFindOnly<BarPositionController>.Instance.Set();
		}
	}

	public void ToggleLoadingLevel(bool toggle)
	{
		if (PlayerData.hasLocalPlayer && !PlayerData.localPlayer.isSpectator)
		{
			PlayerData.localPlayer.machine.ToggleLoadingLevel(toggle);
		}
	}

	public void OnUpdateSettings(ServerSettings settings)
	{
		NetworkInterpolation.AdjustThreshold(settings.vecThreshold, settings.rotThreshold);
		level = CustomLevel.Instance;
		if (level != null)
		{
			dataManager.SetLevel(level);
			if (StatMaster.levelSimulating)
			{
				level.OnUpdateSettings(settings);
			}
		}
		for (int i = 0; i < simulatingMachines.Count; i++)
		{
			ServerMachine serverMachine = simulatingMachines[i];
			serverMachine.OnUpdateSettings(settings);
		}
	}

	public void OnUpdateLevelSettings(LevelSettings settings)
	{
		if (OptionsMaster.votingEnabled)
		{
			playerViewer.Toggle(true);
		}
		else
		{
			playerViewer.Toggle(false);
		}
	}

	public override IEnumerator IEToggleSimulate()
	{
		PlayerData player = PlayerData.localPlayer;
		ServerMachine activeMachine = player.machine;
		if (!networkManager.isConnected || player.isSpectator)
		{
			yield break;
		}
		while (!AddPiece.canSimulate || !activeMachine.ReadyForSim)
		{
			if (StatMaster.SimulationState != SimulationState.WaitingOnMachineReady)
			{
				StatMaster.SetSimulationState(SimulationState.WaitingOnMachineReady);
			}
			yield return new WaitForFixedUpdate();
		}
		if (OptionsMaster.votingEnabled)
		{
			if (activeMachine.isLocalSim)
			{
				TogglePlayMode();
			}
			else if (!StatMaster.InGlobalPlayMode && !player.voteState && StatMaster.activePlayerCount > 1)
			{
				TogglePlayMode(BesiegePlayMode.LocalSimulation);
			}
			else if (StatMaster.activePlayerCount == 1)
			{
				RequestPlayerReadyVote(!player.voteState);
			}
			else if (player.voteState)
			{
				RequestPlayerReadyVote(false);
			}
		}
		else
		{
			TogglePlayMode();
		}
	}

	public void UpdatePlayIcon()
	{
		if (!PlayerData.hasLocalPlayer)
		{
			return;
		}
		PlayerData localPlayer = PlayerData.localPlayer;
		bool voteState = localPlayer.voteState;
		bool flag = !localPlayer.isSpectator && localPlayer.PlayMode != BesiegePlayMode.BuildMode;
		if (OptionsMaster.votingEnabled)
		{
			if (flag)
			{
				Renderer renderer = voteIcon;
				bool flag2 = false;
				playIcon.enabled = flag2;
				renderer.enabled = flag2;
				stopVoteIcon.enabled = voteState;
				stopIcon.enabled = !voteState;
			}
			else
			{
				Renderer renderer2 = stopIcon;
				bool flag2 = false;
				stopVoteIcon.enabled = flag2;
				renderer2.enabled = flag2;
				voteIcon.enabled = voteState;
				playIcon.enabled = !voteState;
			}
		}
		else
		{
			Renderer renderer3 = stopVoteIcon;
			bool flag2 = false;
			voteIcon.enabled = flag2;
			renderer3.enabled = flag2;
			stopIcon.enabled = flag;
			playIcon.enabled = !flag;
		}
	}

	public void TogglePlayMode()
	{
		if (PlayerData.hasLocalPlayer && !PlayerData.localPlayer.isSpectator)
		{
			ServerMachine machine = PlayerData.localPlayer.machine;
			BesiegePlayMode requestPlayMode = BesiegePlayMode.BuildMode;
			if (machine.player.PlayMode == BesiegePlayMode.BuildMode)
			{
				requestPlayMode = ((!StatMaster.Mode.LevelEditor.clientGlobalSim) ? BesiegePlayMode.LocalSimulation : BesiegePlayMode.GlobalSimulation);
			}
			TogglePlayMode(requestPlayMode);
		}
	}

	public void TogglePlayMode(BesiegePlayMode requestPlayMode)
	{
		switch (requestPlayMode)
		{
		case BesiegePlayMode.BuildMode:
			StatMaster.SetSimulationState(SimulationState.SwitchingToBuildMode);
			break;
		case BesiegePlayMode.GlobalSimulation:
			StatMaster.SetSimulationState(SimulationState.SwitchingToGlobalSimulation);
			break;
		case BesiegePlayMode.LocalSimulation:
			StatMaster.SetSimulationState(SimulationState.SwitchingToLocalSimulation);
			break;
		}
		byte[] messageData = new byte[1] { (byte)requestPlayMode };
		string translation = LocalisationManager.GetTranslation((requestPlayMode != BesiegePlayMode.BuildMode) ? 2953 : 2952);
		networkAuxAddPiece.SetLoadingText(translation);
		StatMaster.WaitForServerResponse(StatMaster.ServerResponseType.ChangePlayMode, true);
		networkAuxAddPiece.SendServerRequest(RPCMessageType.ChangePlayMode, messageData);
	}

	public void RequestPlayerReadyVote(bool ready)
	{
		byte[] messageData = new byte[1] { (byte)(ready ? 1u : 0u) };
		StatMaster.SetSimulationState(SimulationState.PendingReadyVote);
		string translation = LocalisationManager.GetTranslation(3370);
		networkAuxAddPiece.SetLoadingText(translation);
		StatMaster.WaitForServerResponse(StatMaster.ServerResponseType.RequestVote, true);
		networkAuxAddPiece.SendServerMessage(RPCMessageType.CmdPlayerReady, messageData);
	}

	public void RefreshPlayerViewer()
	{
		if (StatMaster.isHeadless)
		{
			return;
		}
		for (int i = 0; i < Playerlist.Players.Count; i++)
		{
			PlayerData playerData = Playerlist.Players[i];
			if (playerData.initReady)
			{
				playerViewer.UpdateView(i, playerData);
			}
		}
	}

	public void UpdateVoting()
	{
		bool flag = StatMaster.levelSimulating && !StatMaster.isLocalSim;
		bool flag2 = !StatMaster.levelSimulating || StatMaster.isLocalSim;
		bool flag3 = true;
		for (int i = 0; i < Playerlist.Players.Count; i++)
		{
			PlayerData playerData = Playerlist.Players[i];
			if (!playerData.isSpectator && (!flag2 || !playerData.voteState) && (!flag || playerData.voteState))
			{
				flag3 = false;
			}
		}
		if (flag3)
		{
			if (flag)
			{
				OnVoteSimStart();
				return;
			}
			if (StatMaster.isHosting)
			{
				networkAuxAddPiece.StopAllSimulation();
			}
			voteCountdown.StartCountdown();
		}
		else if (voteCountdown.isRunning)
		{
			voteCountdown.StopCountdown();
		}
	}

	public void OnCountdownReady()
	{
		if (StatMaster.isClient)
		{
			if (!StatMaster.levelSimulating)
			{
				networkAuxAddPiece.SetLoadingText(LocalisationManager.GetTranslation(2951));
			}
		}
		else
		{
			OnVoteSimStart();
		}
	}

	private void OnVoteSimStart()
	{
		if (!StatMaster.isClient)
		{
			BesiegePlayMode playMode = (StatMaster.levelSimulating ? BesiegePlayMode.BuildMode : BesiegePlayMode.GlobalSimulation);
			networkAuxAddPiece.ForceAllPlayMode(playMode);
		}
	}

	protected override bool IsSimulating()
	{
		if (PlayerData.hasLocalPlayer && !PlayerData.localPlayer.isSpectator)
		{
			return PlayerData.localPlayer.machine.isSimulating;
		}
		return base.IsSimulating();
	}

	private void LockNetworkMessages(bool toggle)
	{
		networkAuxAddPiece.LockMessageExecution(toggle);
		lockedMessages = toggle;
	}

	public IEnumerator RespawnMachines(List<Machine> machines)
	{
		List<Machine> staticMachineList = new List<Machine>(machines);
		int r = numberRespawning;
		numberRespawning++;
		while (currentRespawn != r)
		{
			yield return new WaitForFixedUpdate();
		}
		LockNetworkMessages(true);
		while (!AllMachinesReady(staticMachineList))
		{
			yield return new WaitForFixedUpdate();
		}
		isRespawning = true;
		for (int i = 0; i < staticMachineList.Count; i++)
		{
			ServerMachine serverMachine = staticMachineList[i] as ServerMachine;
			serverMachine.isRespawning = true;
			bool wasLocal = serverMachine.isLocalSim;
			if (serverMachine.isSimulating)
			{
				serverMachine.SoftEndSimulation();
			}
			serverMachine.isLocalSim = wasLocal;
		}
		yield return StartCoroutine(StartMachines(staticMachineList));
		for (int i = 0; i < staticMachineList.Count; i++)
		{
			staticMachineList[i].isRespawning = false;
		}
		LockNetworkMessages(false);
		isRespawning = false;
		currentRespawn++;
	}

	public override IEnumerator StartMachines(List<Machine> machines)
	{
		bool ownLocalSim = StatMaster.InLocalPlayMode;
		bool isHost = StatMaster.isHosting;
		int startedMachines = 0;
		yieldOnMachineStart = false;
		for (int i = 0; i < machines.Count; i++)
		{
			ServerMachine machine = machines[i] as ServerMachine;
			if (machine.SimPhysics)
			{
				startedMachines++;
				yieldOnMachineStart = true;
			}
			if (!isRespawning && machine.isLocalMachine && ownLocalSim)
			{
				localSimWarning.LocalSimEnabled();
				yieldOnMachineStart = true;
				machine.isLocalSim = true;
				if (!isHost)
				{
					ResetFrame();
				}
			}
		}
		if (!isRespawning && !hasActiveMachines && startedMachines > 0)
		{
			pollLevel = isHost && !ownLocalSim;
			ToggleLevelSimulation(true, false, true);
		}
		bool initiallyLocked = lockedMessages;
		if (!initiallyLocked)
		{
			LockNetworkMessages(true);
		}
		yield return StartCoroutine(base.StartMachines(machines));
		if (!initiallyLocked)
		{
			LockNetworkMessages(false);
		}
	}

	public override IEnumerator StopMachines(List<Machine> machines)
	{
		LockNetworkMessages(true);
		while (!AddPiece.canSimulate)
		{
			yield return new WaitForFixedUpdate();
		}
		AddPiece.canSimulate = false;
		int stoppedMachines = 0;
		foreach (Machine machine in machines)
		{
			if (!(machine == null) && machine.isSimulating)
			{
				if (machine.SimPhysics)
				{
					stoppedMachines++;
				}
				if (machine.isLocalMachine)
				{
					StopLocalMachine();
				}
				else
				{
					machine.EndSimulation();
				}
			}
		}
		AddPiece.canSimulate = true;
		if (!isRespawning && !hasActiveMachines && stoppedMachines > 0)
		{
			ToggleLevelSimulation(false, true);
		}
		LockNetworkMessages(false);
	}

	public void ToggleLevelSimulation(bool toggle, bool preChangeState = false, bool postChangeState = false)
	{
		if (preChangeState)
		{
			SimStateChange(toggle);
		}
		if (toggle)
		{
			lastLocalTimeScale = TimeSlider.Instance.percentagey;
			projectileManager.ResetFrame();
		}
		else
		{
			projectileManager.Clear();
			SetTimeScale((!StatMaster.Mode.LevelEditor.clientGlobalSim) ? lastLocalTimeScale : lastTimeScale, false);
		}
		levelEditor.ToggleSimulation(toggle);
		AdvancedBlockEditor.Instance.ToggleSimulation(toggle);
		if (postChangeState)
		{
			SimStateChange(toggle);
		}
	}

	public override void SimStateChange(bool toggle)
	{
		SimStateChange(toggle, false);
	}

	private void ResetFrame()
	{
		frame = 0u;
		lastUpdate = 0f;
	}

	public void SimStateChange(bool toggle, bool isRemote)
	{
		if (!toggle && ReferenceMaster.onDestroyPhysicsGoal != null)
		{
			ReferenceMaster.onDestroyPhysicsGoal();
		}
		if (StatMaster.isHosting && (!OptionsMaster.votingEnabled || !StatMaster.isLocalSim))
		{
			byte[] messageData = new byte[1] { (byte)(toggle ? 1u : 0u) };
			networkAuxAddPiece.SendNetworkMessage(RPCMessageType.SimulateLevel, messageData);
			if (toggle)
			{
				ResetFrame();
			}
			else
			{
				level.IncrementSession();
			}
		}
		if (!toggle)
		{
			Resources.UnloadUnusedAssets();
		}
		if (isRemote && toggle)
		{
			ServerHealth.Instance.Reset();
		}
	}

	public void AddInput(ServerMachine machine, byte[] data)
	{
		dataManager.AddInput(machine, data);
		machine.ReadInputData(data, 0);
	}

	public void ProcessInputData(byte[] data, int offset)
	{
		dataManager.UnpackInputData(data, offset);
	}

	public void SetupZone(ushort playerId)
	{
		PlayerBuildZone buildZone;
		if (!networkAuxAddPiece.GetZone(playerId, out buildZone))
		{
			Debug.LogError("Couldn't find player zone id " + playerId);
			return;
		}
		hammerAndNail = buildZone.transform.FindChild("HAMMER").GetComponent<HammerAndNailAnim>();
		boundVisCode = buildZone.GetComponentInChildren<BoundingBoxController>();
	}

	protected override void UpdateHover(BlockBehaviour block)
	{
		if (block != null)
		{
			Machine componentInParent = block.GetComponentInParent<Machine>();
			if (!componentInParent.isLocalMachine)
			{
				canAdd = false;
				BlockHoverOut();
				return;
			}
		}
		base.UpdateHover(block);
	}

	public override void ToggleSimulate()
	{
		if (StatMaster.waitingForServerResponse)
		{
			GenericUIPopup genericUIPopup = SingleInstanceFindOnly<GenericUIPopup>.Instance;
			Debug.Log("Can't toggle, waiting for server response!");
			if (genericUIPopup != null)
			{
				genericUIPopup.Show(LocalisationManager.GetTranslation(3010));
			}
			return;
		}
		if (networkAuxAddPiece.MessagesLocked)
		{
			GenericUIPopup genericUIPopup = SingleInstanceFindOnly<GenericUIPopup>.Instance;
			if (genericUIPopup != null)
			{
				genericUIPopup.Show(LocalisationManager.GetTranslation(2318));
			}
			return;
		}
		if (PlayerData.hasLocalPlayer && !PlayerData.localPlayer.isSpectator)
		{
			ServerMachine machine = PlayerData.localPlayer.machine;
			if (!machine.isSimulating && machine.HasBannedBlocks)
			{
				GenericUIPopup genericUIPopup = SingleInstanceFindOnly<GenericUIPopup>.Instance;
				if (genericUIPopup != null)
				{
					genericUIPopup.Show(LocalisationManager.GetTranslation(3011), 3f);
				}
				return;
			}
		}
		base.ToggleSimulate();
	}

	protected override void OpenBlockMapper(BlockBehaviour block)
	{
		if (!block.HasParentMachine)
		{
			Debug.Log("Block doesn't have a parent machine: " + block.name + "!", base.gameObject);
			return;
		}
		Machine parentMachine = block.ParentMachine;
		if (parentMachine.CanModify && parentMachine.ReadyForSim)
		{
			base.OpenBlockMapper(block);
		}
	}

	public void AddRunningMachine(ServerMachine machine)
	{
		simulatingMachines.Add(machine);
		if (machine.SimPhysics && !activePhysicsMachines.ContainsKey(machine.PlayerID))
		{
			activePhysicsMachines.Add(machine.PlayerID, machine);
			hasActiveMachines = true;
		}
	}

	public void RemoveRunningMachine(ServerMachine machine)
	{
		if (!simulatingMachines.Contains(machine))
		{
			return;
		}
		simulatingMachines.Remove(machine);
		dataManager.ClearInput(machine);
		if (machine.SimPhysics)
		{
			if (activePhysicsMachines.ContainsKey(machine.PlayerID))
			{
				activePhysicsMachines.Remove(machine.PlayerID);
			}
			if (!isRespawning)
			{
				hasActiveMachines = activePhysicsMachines.Count > 0;
			}
		}
	}

	public int GetSimulatingMachines()
	{
		int num = 0;
		for (int i = 0; i < Playerlist.Players.Count; i++)
		{
			PlayerData playerData = Playerlist.Players[i];
			if (!playerData.isSpectator && playerData.machine.isSimulating)
			{
				num++;
			}
		}
		return num;
	}

	public int GetRemoteSimulatingMachines()
	{
		int num = 0;
		for (int i = 0; i < Playerlist.Players.Count; i++)
		{
			PlayerData playerData = Playerlist.Players[i];
			if (!playerData.isSpectator && !playerData.machine.isLocalMachine && playerData.machine.isSimulating)
			{
				num++;
			}
		}
		return num;
	}

	protected override void OnDestroy()
	{
	}

	protected override void Update()
	{
		if (!StatMaster.isHeadless && PlayerData.hasLocalPlayer)
		{
			base.Update();
		}
		else
		{
			timeSlider.Update();
		}
	}

	protected void FixedUpdate()
	{
		float deltaTime = Time.deltaTime;
		if (StatMaster.levelSimulating)
		{
			if (StatMaster.isHosting)
			{
				level.UpdateLogic(deltaTime, true);
			}
			else if (StatMaster.isLocalSim)
			{
				level.UpdateLogic(deltaTime, true);
			}
			else
			{
				level.UpdateProgressEvents(deltaTime, true);
			}
		}
	}

	protected void LateUpdate()
	{
		if (!StatMaster.networkActive)
		{
			return;
		}
		bool flag = Playerlist.Players.Count > 1;
		ServerMachine machine;
		if (networkScene.GetMachine(ownerId, out machine) && machine.isSimulating && machine.InputDirty)
		{
			if (flag)
			{
				int inputSize = machine.InputSize;
				if (StatMaster.isHosting)
				{
					byte[] data = new byte[inputSize];
					machine.WriteInputData(data, 0);
					AddInput(machine, data);
				}
				else
				{
					int inputMessageHeaderSize = networkManager.InputMessageHeaderSize;
					byte[] data2 = new byte[inputMessageHeaderSize + inputSize];
					machine.WriteInputData(data2, inputMessageHeaderSize);
					networkManager.SendInputData(ownerId, machine.Session, data2, (ushort)inputSize);
				}
			}
			else
			{
				machine.ClearInputBuffer();
			}
		}
		float deltaTime = TimeSlider.Instance.deltaTime;
		float deltaTime2 = Time.deltaTime;
		if (StatMaster.isHosting)
		{
			if (StatMaster.levelSimulating)
			{
				level.UpdateLogic(deltaTime2, false);
				levelEditor.SyncLogicData(frame);
			}
			if (simulatingMachines.Count > 0)
			{
				for (int i = 0; i < simulatingMachines.Count; i++)
				{
					ServerMachine serverMachine = simulatingMachines[i];
					if (!serverMachine.isLocalMachine && serverMachine.SimPhysics && serverMachine.InputDirty)
					{
						byte[] data2 = new byte[serverMachine.InputSize];
						serverMachine.WriteInputData(data2, 0);
						dataManager.AddInput(serverMachine, data2);
					}
				}
			}
			if (dataManager.inputDataDirty)
			{
				if (flag)
				{
					byte[] data2 = new byte[dataManager.inputDataSize];
					dataManager.WriteInputData(data2, 0);
					FragmentedRPC.Send(delegate(ushort current, byte[] array4)
					{
						array4[0] = clientInputID;
						int num6 = 3;
						NetworkCompression.WriteUInt16((ushort)(array4.Length - num6), array4, 1);
						NetworkCompression.WriteUInt16(current, array4, num6);
						networkManager.SendInputData(NetworkScene.Instance.clientIDList, array4);
					}, data2, 0, networkManager.InputMessageHeaderSize);
					clientInputID++;
				}
				else
				{
					dataManager.ClearInputData();
				}
			}
		}
		else
		{
			if (StatMaster.levelSimulating)
			{
				if (StatMaster.isLocalSim)
				{
					level.UpdateLogic(deltaTime2, false);
				}
				else
				{
					level.UpdateProgressEvents(deltaTime2, false);
					level.UpdateSimEntities(deltaTime);
					projectileManager.UpdateProjectiles(deltaTime);
				}
			}
			lastCamUpdate += deltaTime;
			float camUpdateRate = NetworkScene.ServerSettings.camUpdateRate;
			if (lastCamUpdate >= camUpdateRate)
			{
				PlayerData localPlayer = PlayerData.localPlayer;
				List<byte[]> list = new List<byte[]>();
				Vector3 position = camTransform.position;
				for (int i = 0; i < simulatingMachines.Count; i++)
				{
					ServerMachine serverMachine = simulatingMachines[i];
					if (!serverMachine.SimPhysics && !serverMachine.RemoteLocal)
					{
						PlayerData player = serverMachine.player;
						ushort networkId = player.networkId;
						PlayerData.CamInfo value;
						if (!localPlayer.camInfo.TryGetValue(networkId, out value))
						{
							value = new PlayerData.CamInfo();
							localPlayer.camInfo.Add(player.networkId, value);
						}
						Vector3 machineCenterPos = serverMachine.MachineCenterPos;
						float num = machineCenterPos.x - position.x;
						float num2 = machineCenterPos.y - position.y;
						float num3 = machineCenterPos.z - position.z;
						float num4 = num * num + num2 * num2 + num3 * num3 - serverMachine.blockRadiusSqr;
						bool flag2 = num4 < ((!player.isVisible) ? halfEssentialThreshold : essentialThreshold);
						if (value.fullUpdate != flag2)
						{
							byte[] array = new byte[3];
							NetworkCompression.WriteUInt16(networkId, array, 0);
							array[2] = (byte)(flag2 ? 1u : 0u);
							list.Add(array);
							value.fullUpdate = flag2;
						}
					}
				}
				if (list.Count > 0)
				{
					byte[] array2 = new byte[1 + list.Count * 3];
					array2[0] = (byte)list.Count;
					NetworkCompression.WriteArray(list, array2, 1);
					networkManager.SendCamData(array2);
				}
			}
		}
		bool flag3 = false;
		lastUpdate += deltaTime;
		float sendRate = NetworkScene.ServerSettings.sendRate;
		if (lastUpdate >= sendRate)
		{
			while (lastUpdate >= sendRate)
			{
				lastUpdate -= sendRate;
			}
			if (StatMaster.isHosting && dataManager.IsFPSFrame(frame))
			{
				ServerHealth.Instance.SetServerFPS(perfAnalyser.FPS);
			}
			if (pollLevel)
			{
				if (!hasPolledLevel)
				{
					dataManager.PollLevel(frame, flag);
				}
				else
				{
					hasPolledLevel = false;
				}
			}
			flag3 = true;
		}
		else if (pollLevel && !hasPolledLevel && lastUpdate / sendRate >= 0.5f)
		{
			dataManager.PollLevel(frame, flag);
			hasPolledLevel = true;
		}
		if (StatMaster.levelSimulating)
		{
			level.UpdateEntities(deltaTime);
		}
		for (int i = 0; i < simulatingMachines.Count; i++)
		{
			ServerMachine serverMachine = simulatingMachines[i];
			if (serverMachine.SimPhysics)
			{
				if (!flag3)
				{
					continue;
				}
				bool flag4 = serverMachine.fullUpdate.Count > 0;
				bool flag5 = StatMaster.isClient || serverMachine.essentialUpdate.Count > 0;
				serverMachine.ToggleEssentialBuffer(flag5);
				bool flag6 = serverMachine.PollObjects(flag4);
				if (!flag)
				{
					continue;
				}
				byte[] transformHeader = serverMachine.GetTransformHeader();
				int num5 = transformHeader.Length;
				if (flag4)
				{
					byte[] array3;
					if (flag6)
					{
						array3 = new byte[num5 + serverMachine.FullBufferLength];
						serverMachine.WriteBufferData(true, array3, num5);
					}
					else
					{
						array3 = new byte[num5];
					}
					Buffer.BlockCopy(transformHeader, 0, array3, 0, num5);
					FragmentedRPC.Send(serverMachine.SendMachineDataFull, array3, 0, networkManager.MachineMessageHeaderSize);
				}
				if (flag5)
				{
					byte[] array3;
					if (flag6)
					{
						array3 = new byte[num5 + serverMachine.EssentialBufferLength];
						serverMachine.WriteBufferData(false, array3, num5);
					}
					else
					{
						array3 = new byte[num5];
					}
					Buffer.BlockCopy(transformHeader, 0, array3, 0, num5);
					if (StatMaster.isHosting)
					{
						FragmentedRPC.Send(serverMachine.SendMachineDataEssential, array3, 0, networkManager.MachineMessageHeaderSize);
					}
					else
					{
						FragmentedRPC.Send(serverMachine.SendMachineDataLocal, array3, 0, networkManager.MachineMessageHeaderSize);
					}
				}
			}
			else if (serverMachine.isSimulating)
			{
				serverMachine.UpdateBlocks(deltaTime);
			}
		}
		if (flag3)
		{
			frame++;
		}
	}

	public void OnInputData(ushort networkID, byte inputID, byte[] buffer)
	{
		byte[] data;
		if (networkAuxAddPiece.HandleFragmentedMessage(clientInputBuffer, inputID, buffer, out data))
		{
			networkManager.OnInputData(networkID, 0, data, 0);
		}
	}
}
