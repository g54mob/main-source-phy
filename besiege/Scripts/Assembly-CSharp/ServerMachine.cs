using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[AddComponentMenu("Core/Multiplayer/Server Machine")]
public class ServerMachine : Machine
{
	public class NetworkCluster
	{
		public NetworkBlock[] Blocks;
	}

	private class BlockTransformCache
	{
		public RPCMessageType type;

		public byte[] data;
	}

	public class ClusterResultData
	{
		public class ClusterData
		{
			public class NeighbourNode
			{
				public class TriggerData
				{
					public int index;

					public bool isDynamic;
				}

				public int otherIndex;

				public TriggerData[] Triggers;
			}

			public class ChildNode
			{
				public int index;

				public NeighbourNode[] Neighbours;
			}

			public int baseIndex;

			public ChildNode[] Nodes;
		}

		public Vector3 Size;

		public Vector3 Center;

		public Vector3 CenterOffset;

		public int centerIndex;

		public int buildingBlockCount;

		public ClusterData[] clusterData;
	}

	public class BlockData
	{
		public BlockInfo info;

		public bool hasData;

		public byte[] data;

		public BlockData(BlockInfo i)
		{
			info = i;
			hasData = false;
		}

		public BlockData(BlockInfo i, byte[] d)
		{
			info = i;
			hasData = true;
			data = d;
		}
	}

	public BannedBlocksUpdated BannedBlocksUpdated;

	public PlayerData player;

	public float blockRadiusSqr;

	public float blockRadius;

	public List<ushort> fullUpdate;

	public List<ushort> essentialUpdate;

	public bool registerDamage;

	public bool hasLogicKeys;

	public NetworkBlock[] networkBlocks;

	public Dictionary<int, int> BlockTypeCount = new Dictionary<int, int>();

	protected NetworkAuxAddPiece networkAuxAddPiece;

	protected NetworkAddPiece networkAddPiece;

	protected NetworkController networkController;

	protected NetKeyInputController netInputController;

	protected MachineDamageController damageController;

	private bool updatedLabelHealth;

	private Dictionary<uint, byte[]> simFrameData;

	private Dictionary<uint, byte[]> simNetworkData;

	private bool invokeRPC = true;

	private bool sendRPC = true;

	private Dictionary<Transform, float> simBaseBlocks;

	private List<Collider> simColliders;

	private PlayerLabel machineLabel;

	private bool hasLabel;

	private float currentHealth = 1f;

	private bool registeredMachine;

	private bool startedMachine;

	private SetBuildZoneDialog setZoneDialog;

	private BesiegeNetworkManager networkManager;

	private LevelEditor levelEditor;

	public bool canModify = true;

	private byte[] lastClusterResults;

	private bool isLoadingLevel;

	protected Vector3 hammerFwd;

	protected Vector3 hammerHit;

	protected Vector3 posHolder = default(Vector3);

	protected Quaternion rotHolder = default(Quaternion);

	private List<KeyCode> logicKeys;

	private BlockBehaviour lastCamBlock;

	private bool hasLastCamBlock;

	private KeyInputController cachedKeyInputController;

	private bool hasCameraBlocks;

	private Dictionary<BuildSurface, Dictionary<int, List<BlockBehaviour>>> pendingSurfaceConnections = new Dictionary<BuildSurface, Dictionary<int, List<BlockBehaviour>>>();

	private List<BlockTransformCache> cachedBlockTransforms = new List<BlockTransformCache>();

	private int cachedBlockTransformSize;

	public MachineDamageController DamageController
	{
		get
		{
			return damageController;
		}
	}

	public int FullBufferLength
	{
		get
		{
			return networkController.FullBufferLengthRelative;
		}
	}

	public int EssentialBufferLength
	{
		get
		{
			return networkController.EssentialBufferLengthRelative;
		}
	}

	public bool InputDirty
	{
		get
		{
			return netInputController.isDirty;
		}
	}

	public int InputSize
	{
		get
		{
			return netInputController.InputSize;
		}
	}

	public bool SendShort
	{
		get
		{
			return networkController.SendShort;
		}
	}

	public Vector3 LabelOffset
	{
		get
		{
			return Vector3.up * (_centerPosOffsetToCenter.y + base.Size.y / 2f);
		}
	}

	public override ushort PlayerID
	{
		get
		{
			return player.networkId;
		}
	}

	public override bool ReadyForSim
	{
		get
		{
			return !isLoadingLevel && isReady && !analyzing;
		}
	}

	public override bool CanModify
	{
		get
		{
			return !isLocalMachine || (!StatMaster.waitingForSim && !StatMaster.waitingForServerResponse && canModify);
		}
	}

	public override bool BuildingLocked
	{
		get
		{
			return !canModify;
		}
	}

	public float Health
	{
		get
		{
			return currentHealth;
		}
	}

	public Vector3 MachineMovementDirection
	{
		get
		{
			return (!isSimulating || !centerBlock.hasSimBlock || centerBlock.noRigidbody) ? Vector3.zero : centerBlock.SimBlock.Rigidbody.velocity;
		}
	}

	public int Session { get; set; }

	public bool HasBannedBlocks { get; internal set; }

	public void AddPendingSurfaceConnection(BuildSurface surface, Dictionary<int, List<BlockBehaviour>> connections)
	{
		pendingSurfaceConnections.Add(surface, connections);
	}

	public void CreateSurfaceConnections()
	{
		foreach (KeyValuePair<BuildSurface, Dictionary<int, List<BlockBehaviour>>> pendingSurfaceConnection in pendingSurfaceConnections)
		{
			foreach (KeyValuePair<int, List<BlockBehaviour>> item in pendingSurfaceConnection.Value)
			{
				if (!(pendingSurfaceConnection.Key.FragmentController == null))
				{
					pendingSurfaceConnection.Key.FragmentController.OnConnectionEstablished(item.Key, item.Value);
				}
			}
		}
	}

	public void CacheBlockTransformAction(RPCMessageType t, byte[] d)
	{
		cachedBlockTransforms.Add(new BlockTransformCache
		{
			type = t,
			data = d
		});
		cachedBlockTransformSize += 1 + d.Length;
	}

	public void FlushBlockTransformActions()
	{
		if (!StatMaster.cachingTransformActions)
		{
			return;
		}
		if (cachedBlockTransforms.Count > 0)
		{
			bool flag = false;
			byte[] array = new byte[cachedBlockTransformSize];
			int num = 0;
			for (int i = 0; i < cachedBlockTransforms.Count; i++)
			{
				BlockTransformCache blockTransformCache = cachedBlockTransforms[i];
				if (blockTransformCache.type == RPCMessageType.RemoveBlock)
				{
					flag = true;
				}
				array[num] = (byte)blockTransformCache.type;
				num++;
				Buffer.BlockCopy(blockTransformCache.data, 0, array, num, blockTransformCache.data.Length);
				num += blockTransformCache.data.Length;
			}
			NetworkAuxAddPiece.Instance.SendFragmentedNetworkMessage(RPCMessageType.TransformCache, CLZF2.Compress(array));
			if (flag)
			{
				OnFlushTransformCache();
			}
		}
		ResetTransformCache();
	}

	public void ToggleLoadingLevel(bool toggle)
	{
		isLoadingLevel = toggle;
	}

	public void ToggleModification(bool toggle)
	{
		canModify = toggle;
		SingleInstanceFindOnly<BarPositionController>.Instance.Set();
	}

	public List<BlockLink> GetBlockNeighbours(int nodeIndex)
	{
		return linkManager.GetNeighbours(nodeIndex);
	}

	public byte[] GetTransformHeader()
	{
		int num = (registerDamage ? ((!updatedLabelHealth) ? 1 : 2) : 0);
		byte[] array = new byte[num];
		if (registerDamage)
		{
			array[0] = (byte)(updatedLabelHealth ? 1u : 0u);
			if (updatedLabelHealth)
			{
				array[1] = (byte)Mathf.RoundToInt(currentHealth * 255f);
				updatedLabelHealth = false;
			}
		}
		return array;
	}

	public int ReadTransformHeader(uint frame, byte[] data)
	{
		int num = 0;
		if (!registerDamage)
		{
			return num;
		}
		num++;
		if (data[0] == 1)
		{
			currentHealth = (float)(int)data[1] / 255f;
			UpdateLabelHealth(currentHealth);
			num++;
		}
		return num;
	}

	public void ReloadAmmo(int units, ReloadAmmoType type, bool setAmmo, bool eachBlock)
	{
		int units2 = units;
		for (int i = 0; i < simBlocks.Count; i++)
		{
			simBlocks[i].OnReloadAmmo(ref units2, type, setAmmo, eachBlock);
			if (eachBlock)
			{
				units2 = units;
			}
			else if (!setAmmo && units2 <= 0)
			{
				break;
			}
		}
	}

	public void SetLabel(PlayerLabel label)
	{
		machineLabel = label;
		machineLabel.UpdateHealth(currentHealth, true);
		hasLabel = true;
	}

	public override Bounds GetBounds(bool renew = true)
	{
		if (!renew || isSimulating)
		{
			return machineBounds;
		}
		Transform transform = base.BuildingMachine;
		player.buildZone.UndoRotation(transform, false);
		base.GetBounds();
		Vector3 position = player.buildZone.transform.position;
		player.buildZone.ApplyRotation(transform, false);
		position.y -= 5.05f;
		machineBounds.center -= position;
		return machineBounds;
	}

	protected override void Awake()
	{
		simFrameData = new Dictionary<uint, byte[]>();
		simNetworkData = new Dictionary<uint, byte[]>();
		fullUpdate = new List<ushort>();
		essentialUpdate = new List<ushort>();
		networkManager = BesiegeNetworkManager.Instance;
		levelEditor = LevelEditor.Instance;
		if (levelEditor != null)
		{
			LevelEditor obj = levelEditor;
			obj.LevelSettingsChanged = (LevelEditor.LevelSettingsChangedHandler)Delegate.Combine(obj.LevelSettingsChanged, new LevelEditor.LevelSettingsChangedHandler(OnLevelSettingsChanged));
		}
		else
		{
			Debug.LogWarning("Could not hook into levelSettingsChanged, levelEditor is null");
		}
		Session = 0;
		AwakeBase();
	}

	private void ResetBannedBlocks()
	{
		HasBannedBlocks = false;
		for (int i = 0; i < buildingBlocks.Count; i++)
		{
			buildingBlocks[i].wasNotAllowed = true;
		}
	}

	public void FlushAndBan()
	{
		DetermineBannedBlocks();
		FlushBlockTransformActions();
	}

	public void DetermineBannedBlocks()
	{
		if (!isLocalMachine)
		{
			return;
		}
		LevelSettings settings = levelEditor.Settings;
		if (BannedBlocksUpdated != null)
		{
			BannedBlocksUpdated();
		}
		if (!HasBannedBlocks && settings.BlockCountLimiter == -1 && settings.BlockTypeLimiter.Count == 0)
		{
			return;
		}
		ResetBannedBlocks();
		for (int i = 0; i < buildingBlocks.Count; i++)
		{
			if ((levelEditor.isActive && settings.AllowModMachines) || (!levelEditor.isActive && StatMaster.limitMachines && !settings.AllowModMachines))
			{
				buildingBlocks[i].wasNotAllowed = false;
			}
			else
			{
				buildingBlocks[i].wasNotAllowed = IsBlockBanned(buildingBlocks[i].Prefab.Type);
			}
			if (buildingBlocks[i].wasNotAllowed)
			{
				HasBannedBlocks = true;
			}
			buildingBlocks[i].VisualController.SetNormal();
		}
	}

	private void OnLevelSettingsChanged(LevelSettings settings)
	{
		DetermineBannedBlocks();
	}

	public void SetPlayer(PlayerData playerData)
	{
		player = playerData;
		isLocalMachine = player.isLocalPlayer;
		if (isLocalMachine)
		{
			setZoneDialog = SetBuildZoneDialog.Instance;
		}
		inputController = (netInputController = base.gameObject.AddComponent<NetKeyInputController>());
		netInputController.Init(this);
		GetBlocks(PlayerID);
		if (isLocalMachine)
		{
			lastMachinePosition = base.Position;
			nodeController.Initialize();
		}
	}

	public byte GetGodModes()
	{
		return (byte)((UnbreakableMode ? 1 : 0) | (InfiniteAmmoMode ? 2 : 0) | (ExplodingCannonballs ? 4 : 0));
	}

	public void UpdateGodMode()
	{
		byte[] messageData = new byte[1] { GetGodModes() };
		networkAuxAddPiece.SendServerMessage(RPCMessageType.UpdateGodMode, messageData);
	}

	public void UpdateGodMode(byte modeByte)
	{
		UnbreakableMode = (modeByte & 1) != 0;
		InfiniteAmmoMode = (modeByte & 2) != 0;
		ExplodingCannonballs = (modeByte & 4) != 0;
	}

	public void SendMachineDataEssential(ushort current, byte[] data)
	{
		networkManager.SendMachineData(essentialUpdate, PlayerID, networkAddPiece.frame, Session, current, data);
	}

	public void SendMachineDataFull(ushort current, byte[] data)
	{
		networkManager.SendMachineData(fullUpdate, PlayerID, networkAddPiece.frame, Session, current, data);
	}

	public void SendMachineDataLocal(ushort current, byte[] data)
	{
		networkManager.SendMachineData(PlayerID, networkAddPiece.frame, Session, current, data);
	}

	public void ClearInputBuffer()
	{
		netInputController.ClearInputBuffer();
	}

	public void WriteInputData(byte[] data, int offset)
	{
		netInputController.WriteInput(data, offset);
	}

	public int ReadInputData(byte[] inputData, int offset)
	{
		return netInputController.ReadInput(inputData, offset);
	}

	public void WriteBufferData(bool isFullUpdate, byte[] buffer, int offset)
	{
		networkController.WriteBufferRelative(isFullUpdate, buffer, offset);
	}

	public void ToggleEssentialBuffer(bool toggle)
	{
		networkController.ToggleEssentialBuffer(toggle);
	}

	public bool PollObjects(bool fullUpdate)
	{
		return networkController.PollObjectsRelative(fullUpdate, networkBlocks);
	}

	public int ReadBufferData(uint frame, byte[] transformData, int offset)
	{
		return networkController.ReadBufferRelative(frame, transformData, offset, networkBlocks);
	}

	public void NewFrame(uint frame)
	{
		networkController.NewFrame(frame, networkBlocks, (uint)networkBlocks.Length);
	}

	public void OnUpdateSettings(ServerSettings settings)
	{
		for (int i = 0; i < networkBlocks.Length; i++)
		{
			networkBlocks[i].UpdateBaseInterval();
		}
	}

	public void UpdateBlocks(float delta)
	{
		for (int i = 0; i < networkBlocks.Length; i++)
		{
			networkBlocks[i].UpdateEntity(delta);
		}
	}

	protected override void AwakeBase()
	{
		isLocalMachine = false;
		base.AwakeBase();
		networkAuxAddPiece = NetworkAuxAddPiece.Instance;
		networkAddPiece = addPiece as NetworkAddPiece;
		networkController = base.gameObject.AddComponent<NetworkController>();
		damageController = base.gameObject.AddComponent<MachineDamageController>();
		damageController.Init(OnMachineDamage, linkManager);
		simBaseBlocks = new Dictionary<Transform, float>();
		simColliders = new List<Collider>();
		isLocalSim = false;
	}

	protected override void InitSim()
	{
		base.InitSim();
		networkController.Clear();
		simBaseBlocks.Clear();
	}

	protected override void PostSimStart()
	{
		networkController.InitSim(base.SimPhysics);
		if (simFrameData.Count > 0)
		{
			simNetworkData.Clear();
			foreach (uint key in simFrameData.Keys)
			{
				ushort num = (ushort)key;
				BlockBehaviour simBlock = buildingBlocks[num].SimBlock;
				simNetworkData.Add((ushort)simBlock.NetBlock.id, simFrameData[num]);
			}
			networkController.ApplySimFrame(simNetworkData);
			simFrameData.Clear();
		}
		if (!isLocalMachine && (!StatMaster.Mode.LevelEditor.clientGlobalSim || isLocalSim))
		{
			ToggleGhost(true);
		}
		base.PostSimStart();
	}

	public override void SetPosition(Vector3 pos, bool boundCheck = true)
	{
		if (isLocalMachine)
		{
			networkAuxAddPiece.SendNetworkMessage(RPCMessageType.Translate, NetworkCompression.PackVector(pos));
		}
		base.SetPosition(pos, boundCheck);
	}

	public override void SetRotation(Quaternion rot, bool boundCheck = true)
	{
		if (isLocalMachine)
		{
			networkAuxAddPiece.SendNetworkMessage(RPCMessageType.Rotate, NetworkCompression.PackQuaternion(rot));
		}
		base.SetRotation(rot, boundCheck);
	}

	public override void SetTransform(Vector3 pos, Quaternion rot, bool boundCheck = true)
	{
		if (isLocalMachine)
		{
			networkAuxAddPiece.SendNetworkMessage(RPCMessageType.Translate, NetworkCompression.PackVector(pos));
			networkAuxAddPiece.SendNetworkMessage(RPCMessageType.Rotate, NetworkCompression.PackQuaternion(rot));
		}
		base.SetTransform(pos, rot, boundCheck);
	}

	public void RegisterIntact(BlockBehaviour block)
	{
		damageController.RegisterBlock(block);
	}

	public void ApplyDamage(BlockBehaviour block, MachineDamageType damageType)
	{
		damageController.ApplyDamage(block, damageType);
	}

	public void ApplyBlockDamage(BlockBehaviour block, float amount)
	{
		damageController.ApplyBlockDamage(block, amount);
	}

	private void OnMachineDamage(float totalDamage)
	{
		if (!base.SimPhysics || !player.buildZone)
		{
			return;
		}
		PlayerBuildZone buildZone = player.buildZone;
		if (buildZone.hasSpawnZone)
		{
			BuildZoneObject spawnZone = buildZone.spawnZone;
			if (hasLabel)
			{
				currentHealth = 1f - Mathf.Clamp01(totalDamage * spawnZone.healthBarScale);
				UpdateLabelHealth(currentHealth);
			}
			spawnZone.OnMachineDamage(totalDamage);
		}
	}

	public void UpdateLabelHealth(float newHealth)
	{
		if (hasLabel)
		{
			machineLabel.UpdateHealth(newHealth, false);
			updatedLabelHealth = true;
		}
	}

	public override void StartSimulation()
	{
		networkController.ResetFrame();
		bool flag = !isLocalMachine && curtainMode;
		if (flag)
		{
			ToggleCurtain(false, true);
		}
		List<BlockBehaviour> value;
		if (ReferenceMaster.IntactBlocks.TryGetValue(player.networkId, out value))
		{
			value.Clear();
		}
		base.StartSimulation();
		if (flag)
		{
			player.buildZone.ToggleCurtain(false);
			curtainMode = true;
		}
		ServerHealth.countDirty = true;
		player.useCustomPos = false;
		if (player.buildZone.hasSpawnZone)
		{
			BuildZoneObject spawnZone = player.buildZone.spawnZone;
			if (!isRespawning)
			{
				spawnZone.OnStartSim();
			}
			spawnZone.ResetDamage();
		}
		if (isLocalMachine)
		{
			StatMaster.waitingForSim = false;
			StatMaster.WaitForServerResponse(StatMaster.ServerResponseType.ChangePlayMode, false);
			networkAuxAddPiece.HideLoadingText();
			if (levelEditor.isActive)
			{
				levelEditor.SetUIState(LevelEditorUI.UIState.Inactive);
			}
			setZoneDialog.Toggle(false);
		}
		startedMachine = true;
		registeredMachine = false;
	}

	protected override void WakePhysics(BlockBehaviour block)
	{
		if (base.SimPhysics)
		{
			base.WakePhysics(block);
		}
	}

	public void IncrementSession(bool simPhys, bool isRespawn, int session)
	{
		Session = session;
		if (simPhys)
		{
			if (Session < 200)
			{
				Session++;
			}
			else
			{
				Session = 0;
			}
			byte[] array = new byte[4];
			NetworkCompression.WriteUInt16(PlayerID, array, 0);
			array[2] = (byte)(isRespawning ? 1u : 0u);
			array[3] = (byte)Session;
			networkAuxAddPiece.SendNetworkMessage(RPCMessageType.IncrementSession, array);
		}
		else if (isRespawn && (!StatMaster.Mode.LevelEditor.clientGlobalSim || base.RemoteLocal))
		{
			List<Machine> list = new List<Machine>();
			list.Add(this);
			List<Machine> machines = list;
			networkAddPiece.StartCoroutine(networkAddPiece.RespawnMachines(machines));
		}
	}

	public override void EndSimulation()
	{
		if (!isSimulating)
		{
			return;
		}
		if (base.SimPhysics)
		{
			IncrementSession(true, isRespawning, Session);
		}
		ghostMode = false;
		isReady = false;
		if (!isRespawning)
		{
			if (player.buildZone.hasSpawnZone)
			{
				player.buildZone.spawnZone.OnStopSim();
			}
			player.frameManager.Clear();
		}
		if (base.SimPhysics)
		{
			ReferenceMaster.OnMachineEndSimulation();
		}
		if (simColliders.Count > 0)
		{
			simColliders.Clear();
		}
		base.EndSimulation();
		if (isLocalMachine)
		{
			StatMaster.waitingForSim = false;
			StatMaster.WaitForServerResponse(StatMaster.ServerResponseType.ChangePlayMode, false);
			networkAuxAddPiece.HideLoadingText();
			if (levelEditor.isActive)
			{
				levelEditor.SetUIState(StatMaster.levelSimulating ? LevelEditorUI.UIState.Simulating : LevelEditorUI.UIState.BuildMode);
			}
			setZoneDialog.Toggle(true);
		}
		else if (curtainMode)
		{
			player.buildZone.ToggleCurtain(true);
		}
		networkController.Clear();
		registerDamage = false;
		hasLogicKeys = false;
		ServerHealth.countDirty = true;
		startedMachine = false;
		isLocalSim = false;
		isReady = true;
	}

	public override void SoftEndSimulation()
	{
		hasLastCamBlock = false;
		if (isRespawning)
		{
			if (isLocalMachine && hasCameraBlocks)
			{
				FixedCameraController instance = SingleInstance<FixedCameraController>.Instance;
				foreach (FixedCameraBlock camera in instance.cameras)
				{
					if (camera.isActive)
					{
						lastCamBlock = camera.BuildingBlock;
						hasLastCamBlock = true;
					}
				}
			}
			if (base.SimPhysics)
			{
				for (int i = 0; i < buildingBlocks.Count; i++)
				{
					BlockBehaviour blockBehaviour = buildingBlocks[i];
					if (blockBehaviour.hasSimBlock)
					{
						BlockBehaviour simBlock = blockBehaviour.SimBlock;
						if (simBlock.respawnCallbackCount > 0)
						{
							simBlock.onRespawn(blockBehaviour);
						}
					}
				}
			}
		}
		if (StatMaster.isHosting && base.SimPhysics)
		{
			ProjectileManager.Instance.DespawnParentedProjectiles(base.transform);
		}
		base.SoftEndSimulation();
		if (registeredMachine)
		{
			networkAddPiece.RemoveRunningMachine(this);
			registeredMachine = false;
		}
	}

	public override void StartPhysics()
	{
		base.StartPhysics();
		FrameBufferManager frameManager = player.frameManager;
		uint cacheFrame;
		FrameBufferManager.CacheEntry cacheEntry;
		while (frameManager.GetOldestCache(Session, out cacheFrame, out cacheEntry))
		{
			int num = ReadTransformHeader(cacheFrame, cacheEntry.data);
			if (cacheEntry.data.Length > num)
			{
				ReadBufferData(cacheFrame, cacheEntry.data, num);
			}
		}
		if (isRespawning && isLocalMachine && hasLastCamBlock && lastCamBlock != null && lastCamBlock.hasSimBlock && lastCamBlock.SimBlock is FixedCameraBlock)
		{
			SingleInstance<FixedCameraController>.Instance.Activate(lastCamBlock.SimBlock as FixedCameraBlock);
		}
		hasLastCamBlock = false;
		if (startedMachine)
		{
			networkAddPiece.AddRunningMachine(this);
			registeredMachine = true;
		}
	}

	protected override void InitBlocks()
	{
		networkController.SetCapacity(buildingBlocks.Count);
		int count = linkManager.Clusters.Count;
		networkBlocks = new NetworkBlock[linkManager.Nodes.Count - linkManager.IgnoredNodes.Count];
		simClusters = new SimCluster[count];
		clusterSurplus = base.BlockCount < simClusters.Length + 3;
		uint num = 0u;
		hasCameraBlocks = false;
		damageController.Reset();
		pendingSurfaceConnections.Clear();
		PlayerBuildZone buildZone = player.buildZone;
		registerDamage = false;
		hasLogicKeys = false;
		if (buildZone.hasSpawnZone)
		{
			BuildZoneObject spawnZone = buildZone.spawnZone;
			registerDamage = spawnZone.RegisterDamage;
			if (StatMaster.isHosting || isLocalMachine)
			{
				logicKeys = spawnZone.GetLogicKeys();
				hasLogicKeys = logicKeys.Count > 0;
				for (int i = 0; i < logicKeys.Count; i++)
				{
					inputController.Add(logicKeys[i]);
				}
			}
			if (registerDamage && hasLabel)
			{
				spawnZone.healthBarScale = spawnZone.GetHealthScale();
				currentHealth = 1f;
				updatedLabelHealth = false;
				machineLabel.UpdateHealth(currentHealth, true);
			}
		}
		damageController.Toggle(registerDamage);
		HasEmulationBlocks = SingleInstanceFindOnly<CinematicCam>.hasInstance() && SingleInstanceFindOnly<CinematicCam>.Instance.emulateKey;
		for (int j = 0; j < simBlocks.Count; j++)
		{
			if (simBlocks[j].BuildingBlock.Prefab.EmulatesAnyKeys)
			{
				HasEmulationBlocks = true;
				break;
			}
		}
		inputController.SetHasAnyEmulation(HasEmulationBlocks);
		InitSimClusters();
		refBlock = GetRefBlock();
		refBlockTransform = refBlock.transform;
		for (int j = 0; j < linkManager.IgnoredNodes.Count; j++)
		{
			BlockBehaviour block = linkManager.IgnoredNodes[j].Block;
			BlockBehaviour simBlock = GetSimBlock(block);
			if (simBlock != null)
			{
				InitSimBlock(simBlock, block, simBlock.transform, num++);
				continue;
			}
			Debug.LogError("Couldn't find ignored node on " + PlayerID + " (" + player.name + "), please report!");
		}
		if (!base.SimPhysics)
		{
			CreateSurfaceConnections();
			for (int j = 0; j < simClusters.Length; j++)
			{
				SimCluster simCluster = simClusters[j];
				Transform baseTransform = simCluster.BaseTransform;
				Transform parent = baseTransform;
				if (baseTransform.localScale != Vector3.one)
				{
					GameObject gameObject = new GameObject();
					gameObject.name = baseTransform.name + "Base";
					Transform transform = gameObject.transform;
					transform.SetParent(tempSim, false);
					NetworkBlock netBlock = simCluster.Base.NetBlock;
					transform.rotation = netBlock.Rotation;
					transform.position = netBlock.Position;
					baseTransform.SetParent(transform, true);
					netBlock.SetTrackTransform(transform);
					parent = transform;
				}
				for (int k = 0; k < simCluster.Blocks.Length; k++)
				{
					simCluster.Blocks[k].transform.SetParent(parent, true);
				}
			}
		}
		if (hasCenterBlock && centerBlock.SimBlock != null)
		{
			Transform transform2 = centerBlock.SimBlock.transform;
			if (transform2 != null)
			{
				centerBlockTransform = transform2;
			}
		}
		damageController.SaveIntactBlocks();
	}

	protected override void InitSimClusters()
	{
		int count = linkManager.Clusters.Count;
		uint index = 0u;
		Vector3 vector = tempSim.TransformPoint(_machineMiddle);
		Vector3 zero = Vector3.zero;
		float num = 0f;
		for (int i = 0; i < count; i++)
		{
			BlockCluster blockCluster = linkManager.Clusters[i];
			BlockBehaviour block = blockCluster.Base.Block;
			BlockBehaviour simBlock = GetSimBlock(block);
			SimCluster simCluster = new SimCluster(simBlock, blockCluster.CenterOffset, blockCluster.BlockWeight, blockCluster.Blocks.Count);
			simCluster.alwaysIncludeInCenter = AlwaysIncludeInCenter(block.BlockID);
			InitSimBlock(simBlock, block, simCluster.BaseTransform, index);
			networkBlocks[index++] = simBlock.NetBlock;
			int count2 = blockCluster.Blocks.Count;
			simCluster.Blocks = new BlockBehaviour[count2 - 1];
			int num2 = 0;
			for (int j = 0; j < count2; j++)
			{
				BlockBehaviour block2 = blockCluster.Blocks[j].Block;
				if (block2.NodeIndex != block.NodeIndex)
				{
					simBlock = GetSimBlock(block2);
					simCluster.Blocks[num2++] = simBlock;
					InitSimBlock(simBlock, block2, simCluster.BaseTransform, index);
					networkBlocks[index++] = simBlock.NetBlock;
				}
			}
			simClusters[i] = simCluster;
			if (simCluster.count >= 2 || simCluster.alwaysIncludeInCenter || clusterSurplus)
			{
				zero += simCluster.Base.transform.TransformPoint(simCluster.CenterOffset) * simCluster.Weight;
				num += simCluster.Weight;
			}
		}
		Vector3 direction = vector - zero / num;
		for (int i = 0; i < count; i++)
		{
			SimCluster simCluster = simClusters[i];
			simCluster.SimOffset = simCluster.BaseTransform.InverseTransformDirection(direction);
		}
	}

	protected void InitSimBlock(BlockBehaviour block, BlockBehaviour oldBlock, Transform baseEnt, uint index)
	{
		InitSimBlock(block, oldBlock, index);
		block.Team = player.team;
		if (block.Prefab.Type == BlockType.CameraBlock)
		{
			hasCameraBlocks = true;
		}
		NetworkBlock netBlock = block.NetBlock;
		netBlock.Init(index, networkController, baseEnt, base.SimPhysics);
		networkController.Add(netBlock);
	}

	public void EnableInputRecorder()
	{
		Debug.Log("Replacing inputcontroller with the recording inputcontroller...");
		inputController.enabled = false;
		cachedKeyInputController = inputController;
		inputController = base.gameObject.AddComponent<RecordingKeyInputController>();
		inputController.ResetKeys();
	}

	public void DisableInputRecorder()
	{
		Debug.Log("Resetting input controller to default one...");
		UnityEngine.Object.Destroy(inputController);
		inputController = cachedKeyInputController;
		inputController.enabled = true;
		if (isSimulating)
		{
			inputController.Toggle(true);
		}
		else
		{
			inputController.Toggle(false);
		}
	}

	private void ToggleInputRecordingMode()
	{
		RecordingKeyInputController recordingKeyInputController = (RecordingKeyInputController)inputController;
		recordingKeyInputController.ToggleRecordingMode();
	}

	public void SetInputRecordingMode(RecordingKeyInputController.RecordingMode mode)
	{
		RecordingKeyInputController recordingKeyInputController = (RecordingKeyInputController)inputController;
		recordingKeyInputController.SetRecordingMode(mode);
	}

	private void ProcessLogicKeys()
	{
		for (int i = 0; i < logicKeys.Count; i++)
		{
			KeyCode keyCode = logicKeys[i];
			if (inputController.IsPressed(keyCode))
			{
				player.buildZone.spawnZone.TriggerKey(keyCode, true);
			}
			else if (inputController.IsReleased(keyCode))
			{
				player.buildZone.spawnZone.TriggerKey(keyCode, false);
			}
		}
	}

	protected override void Update()
	{
		if (!isLocalMachine)
		{
			base.Update();
			if (hasLogicKeys && base.SimPhysics)
			{
				ProcessLogicKeys();
			}
		}
		else
		{
			base.Update();
			if (isSimulating && !StatMaster.inMenu && !StatMaster.stopHotkeys && hasLogicKeys && base.SimPhysics)
			{
				ProcessLogicKeys();
			}
		}
	}

	public override bool AddBlock(BlockInfo blockInfo, out BlockBehaviour block)
	{
		bool result = base.AddBlock(blockInfo, out block);
		if (invokeRPC && block.PlacementComplete)
		{
			SendBlockInfo(block, blockInfo);
		}
		invokeRPC = true;
		return result;
	}

	public void SendBlockInfo(BlockBehaviour block, BlockInfo blockInfo)
	{
		ushort num = (ushort)block.Prefab.ID;
		byte[] array = blockInfo.Encode();
		int num2 = NetworkCompression.PackedUIntLength(array.Length, false);
		byte[] array2 = new byte[19 + num2 + array.Length];
		bool flag = !isLoadingInfo && num != 0;
		int num3 = 0;
		array2[num3] = (byte)(flag ? 1u : 0u);
		num3++;
		NetworkCompression.CompressPosition(AddPiece.hammerPos, array2, num3);
		num3 += 6;
		NetworkCompression.CompressVector(AddPiece.hammerFwd, -1f, 1f, array2, num3);
		num3 += 6;
		NetworkCompression.CompressPosition(AddPiece.mouseHitPos, array2, num3);
		num3 += 6;
		NetworkCompression.PackUInt(array.Length, array2, num3, false, num2);
		num3 += num2;
		Buffer.BlockCopy(array, 0, array2, num3, array.Length);
		if (StatMaster.cachingTransformActions)
		{
			CacheBlockTransformAction(RPCMessageType.AddBlock, array2);
		}
		else
		{
			networkAuxAddPiece.SendNetworkMessage(RPCMessageType.AddBlock, array2);
		}
	}

	public override void AddDraggedBlock(GenericDraggedBlock doubleBlock)
	{
		if (invokeRPC && isLocalMachine)
		{
			base.AddDraggedBlock(doubleBlock);
			BlockInfo blockInfo = BlockInfo.FromBlockBehaviour(doubleBlock);
			SendBlockInfo(doubleBlock, blockInfo);
		}
		invokeRPC = true;
	}

	public int RemoteAddBlock(byte[] blockData, int offset, out BlockBehaviour block)
	{
		int num = offset;
		bool flag = blockData[offset++] == 1;
		NetworkCompression.DecompressPosition(blockData, offset, out posHolder);
		offset += 6;
		NetworkCompression.DecompressVector(blockData, offset, -1f, 1f, out hammerFwd);
		offset += 6;
		NetworkCompression.DecompressPosition(blockData, offset, out hammerHit);
		offset += 6;
		int count;
		offset += NetworkCompression.UnpackUInt(blockData, offset, false, out count);
		byte[] array = new byte[count];
		Buffer.BlockCopy(blockData, offset, array, 0, count);
		offset += count;
		if (flag && !curtainMode)
		{
			player.buildZone.AnimateHammer(hammerHit, posHolder, hammerFwd, isLocalMachine);
		}
		BlockInfo blockInfo = BlockInfo.Decode((ushort)buildingBlocks.Count, array, 0);
		invokeRPC = false;
		ToggleUndo(true);
		bool flag2 = base.AddBlock(blockInfo, out block);
		ToggleUndo(false);
		if (flag2)
		{
			if (block.Prefab.hasBVC)
			{
				block.VisualController.PlaceFromBlockInfo(blockInfo);
			}
			if (!isLocalMachine)
			{
				block.OnAddRemote();
			}
			UpdateMachineDLCStatus();
		}
		return offset - num;
	}

	public bool RemoteAddBlockGlobal(Vector3 position, Quaternion rotation, BlockType id, bool isFlipped, out BlockBehaviour block)
	{
		invokeRPC = false;
		return base.AddBlockGlobal(position, rotation, id, isFlipped, out block);
	}

	public void ToggleCurtain(bool toggle, bool updateVis)
	{
		if (curtainMode == toggle)
		{
			return;
		}
		curtainMode = toggle;
		if (isLocalMachine)
		{
			return;
		}
		player.buildZone.ToggleCurtain(toggle);
		if (!updateVis)
		{
			return;
		}
		for (int i = 0; i < buildingBlocks.Count; i++)
		{
			BlockBehaviour blockBehaviour = buildingBlocks[i];
			if (blockBehaviour.Prefab.hasBVC)
			{
				blockBehaviour.VisualController.SetNormal();
			}
		}
	}

	public void ToggleGhost(bool toggle)
	{
		if (toggle == ghostMode || isLocalMachine || !isSimulating)
		{
			return;
		}
		ghostMode = toggle;
		for (int i = 0; i < networkBlocks.Length; i++)
		{
			NetworkBlock networkBlock = networkBlocks[i];
			if (!(networkBlock == null))
			{
				BlockBehaviour blockBehaviour = networkBlock.blockBehaviour;
				if (blockBehaviour.Prefab.hasBVC)
				{
					blockBehaviour.VisualController.SetNormal();
				}
			}
		}
	}

	public bool RemoteAddBlock(Vector3 position, Quaternion rotation, BlockType id, bool isFlipped, out BlockBehaviour block)
	{
		invokeRPC = false;
		bool flag = AddBlock(position, rotation, id, isFlipped, out block);
		if (flag)
		{
			block.VisualController.PlaceFromPrefab();
		}
		return flag;
	}

	public void SendBlockReverseMessage(int bIndex, bool flipped)
	{
		byte[] array = new byte[5];
		NetworkCompression.WriteUInt((uint)bIndex, false, array, 0);
		array[4] = (byte)(flipped ? 1u : 0u);
		if (StatMaster.cachingTransformActions)
		{
			CacheBlockTransformAction(RPCMessageType.ReverseBlock, array);
		}
		else
		{
			networkAuxAddPiece.SendNetworkMessage(RPCMessageType.ReverseBlock, array);
		}
	}

	public override bool ReverseBlock(BlockBehaviour block, bool playSound, bool isUndo)
	{
		bool flag = base.ReverseBlock(block, playSound, isUndo);
		if (flag)
		{
			int buildIndex = block.BuildIndex;
			if (buildIndex != -1)
			{
				SendBlockReverseMessage(buildIndex, block.Flipped);
			}
		}
		return flag;
	}

	public override void EditBlockData(BlockBehaviour block, XDataHolder data)
	{
		base.EditBlockData(block, data);
		byte[] outData;
		data.Encode(out outData);
		byte[] array = new byte[6 + outData.Length];
		int buildIndex = block.BuildIndex;
		int num = 0;
		NetworkCompression.WriteUInt((uint)buildIndex, false, array, num);
		num += 4;
		NetworkCompression.WriteUInt16((ushort)outData.Length, array, num);
		num += 2;
		Buffer.BlockCopy(outData, 0, array, num, outData.Length);
		if (StatMaster.cachingTransformActions)
		{
			CacheBlockTransformAction(RPCMessageType.EditBlockData, array);
		}
		else
		{
			networkAuxAddPiece.SendNetworkMessage(RPCMessageType.EditBlockData, array);
		}
	}

	public override void MoveBlock(Guid guid, Vector3 pos)
	{
		base.MoveBlock(guid, pos);
		BlockBehaviour block;
		if (isLocalMachine && GetBlock(guid, out block))
		{
			byte[] array = new byte[28];
			int num = 0;
			NetworkCompression.WriteUInt((uint)block.BuildIndex, false, array, num);
			num += 4;
			NetworkCompression.PackVector(pos, array, num);
			num += 12;
			NetworkCompression.PackVector(block.Position, array, num);
			num += 12;
			if (StatMaster.cachingTransformActions)
			{
				CacheBlockTransformAction(RPCMessageType.MoveBlock, array);
			}
			else
			{
				networkAuxAddPiece.SendNetworkMessage(RPCMessageType.MoveBlock, array);
			}
		}
	}

	public override void RotateBlock(Guid guid, Quaternion rot)
	{
		base.RotateBlock(guid, rot);
		BlockBehaviour block;
		if (isLocalMachine && GetBlock(guid, out block))
		{
			byte[] array = new byte[36];
			int num = 0;
			NetworkCompression.WriteUInt((uint)block.BuildIndex, false, array, num);
			num += 4;
			NetworkCompression.PackQuaternion(rot, array, num);
			num += 16;
			NetworkCompression.PackQuaternion(block.Rotation, array, num);
			num += 16;
			if (StatMaster.cachingTransformActions)
			{
				CacheBlockTransformAction(RPCMessageType.RotateBlock, array);
			}
			else
			{
				networkAuxAddPiece.SendNetworkMessage(RPCMessageType.RotateBlock, array);
			}
		}
	}

	public override void ScaleBlock(Guid guid, Vector3 scale)
	{
		base.ScaleBlock(guid, scale);
		BlockBehaviour block;
		if (isLocalMachine && GetBlock(guid, out block))
		{
			byte[] array = new byte[16];
			int num = 0;
			NetworkCompression.WriteUInt((uint)block.BuildIndex, false, array, num);
			num += 4;
			NetworkCompression.PackVector(scale, array, num);
			if (StatMaster.cachingTransformActions)
			{
				CacheBlockTransformAction(RPCMessageType.ScaleBlock, array);
			}
			else
			{
				networkAuxAddPiece.SendNetworkMessage(RPCMessageType.ScaleBlock, array);
			}
		}
	}

	public int RemoteMoveBlock(byte[] moveData, int offset)
	{
		int num = offset;
		uint blockIndex = BitConverter.ToUInt32(moveData, offset);
		offset += 4;
		Vector3 vec;
		NetworkCompression.UnpackVector(moveData, offset, out vec);
		offset += 12;
		Vector3 vec2;
		NetworkCompression.UnpackVector(moveData, offset, out vec2);
		offset += 12;
		BlockBehaviour block;
		if (GetBlockFromIndex((int)blockIndex, out block))
		{
			if (!block.noRigidbody)
			{
				SetRigidInterpolation(RigidbodyInterpolation.None, new List<BlockBehaviour> { block });
			}
			block.SetPosition(vec);
			block.Position = vec2;
			if (!block.noRigidbody)
			{
				RestoreRigidInterpolation();
			}
			analyzing = true;
		}
		return offset - num;
	}

	public int RemoteRotateBlock(byte[] rotateData, int offset)
	{
		int num = offset;
		uint blockIndex = BitConverter.ToUInt32(rotateData, offset);
		offset += 4;
		Quaternion quat;
		NetworkCompression.UnpackQuaternion(rotateData, offset, out quat);
		offset += 16;
		Quaternion quat2;
		NetworkCompression.UnpackQuaternion(rotateData, offset, out quat2);
		offset += 16;
		BlockBehaviour block;
		if (GetBlockFromIndex((int)blockIndex, out block))
		{
			if (!block.noRigidbody)
			{
				SetRigidInterpolation(RigidbodyInterpolation.None, new List<BlockBehaviour> { block });
			}
			block.SetRotation(quat);
			block.Rotation = quat2;
			if (!block.noRigidbody)
			{
				RestoreRigidInterpolation();
			}
			analyzing = true;
		}
		return offset - num;
	}

	public int RemoteScaleBlock(byte[] scaleData, int offset)
	{
		uint blockIndex = BitConverter.ToUInt32(scaleData, offset);
		int num = offset;
		offset += 4;
		BlockBehaviour block;
		if (GetBlockFromIndex((int)blockIndex, out block))
		{
			Vector3 vec;
			NetworkCompression.UnpackVector(scaleData, offset, out vec);
		}
		offset += 12;
		return offset - num;
	}

	public int RemoteShortenBlock(byte[] shortenData, int offset)
	{
		int num = offset;
		int blockIndex = (int)BitConverter.ToUInt32(shortenData, offset);
		offset += 4;
		BlockBehaviour block;
		if (GetBlockFromIndex(blockIndex, out block))
		{
			ShorteningBlock shorteningBlock = block as ShorteningBlock;
			if (shorteningBlock != null)
			{
				shorteningBlock.UpdateLength(shortenData[offset], true);
				offset++;
			}
		}
		return offset - num;
	}

	public int RemoteMirrorDragged(byte[] draggedInfo, int offset)
	{
		int num = offset;
		int blockIndex = (int)BitConverter.ToUInt32(draggedInfo, offset);
		offset += 4;
		Vector3 vec;
		NetworkCompression.UnpackVector(draggedInfo, offset, out vec);
		offset += 12;
		Vector3 vec2;
		NetworkCompression.UnpackVector(draggedInfo, offset, out vec2);
		offset += 12;
		Quaternion quat;
		NetworkCompression.UnpackQuaternion(draggedInfo, offset, out quat);
		offset += 16;
		Quaternion quat2;
		NetworkCompression.UnpackQuaternion(draggedInfo, offset, out quat2);
		offset += 16;
		int num2 = (int)NetworkCompression.ReadUInt(false, draggedInfo, offset);
		offset += 4;
		XDataHolder xDataHolder = new XDataHolder();
		byte[] array = new byte[num2];
		Buffer.BlockCopy(draggedInfo, offset, array, 0, num2);
		offset += num2;
		BlockBehaviour block;
		if (GetBlockFromIndex(blockIndex, out block))
		{
			GenericDraggedBlock genericDraggedBlock = block as GenericDraggedBlock;
			xDataHolder.Decode(array, 0);
			if (genericDraggedBlock != null)
			{
				if (!block.noRigidbody)
				{
					SetRigidInterpolation(RigidbodyInterpolation.None, new List<BlockBehaviour> { genericDraggedBlock });
				}
				genericDraggedBlock.rotOffset = (genericDraggedBlock.rotInvOffset = Quaternion.identity);
				genericDraggedBlock.posOffset = Vector3.zero;
				genericDraggedBlock.SetPosition(buildingMachine.TransformPoint(vec));
				genericDraggedBlock.SetRotation(buildingMachine.rotation * quat);
				genericDraggedBlock.OnLoad(xDataHolder);
				if (genericDraggedBlock is BraceCode)
				{
					genericDraggedBlock.NetBlock.SetupBrace(genericDraggedBlock as BraceCode);
				}
				if (!block.noRigidbody)
				{
					RestoreRigidInterpolation();
				}
			}
		}
		return offset - num;
	}

	public int RemoteReverse(byte[] flipData, int offset)
	{
		int num = offset;
		int blockIndex = (int)BitConverter.ToUInt32(flipData, offset);
		offset += 4;
		byte b = flipData[offset++];
		BlockBehaviour block;
		if (GetBlockFromIndex(blockIndex, out block))
		{
			bool flipped = b == 1;
			block.Flipped = flipped;
			block.PostFlip(false, false);
		}
		return offset - num;
	}

	public int RemoteEditBlockData(byte[] editData, int offset)
	{
		int num = offset;
		int blockIndex = (int)NetworkCompression.ReadUInt(false, editData, offset);
		offset += 4;
		ushort num2 = NetworkCompression.ReadUInt16(editData, offset);
		offset += 2;
		byte[] array = new byte[num2];
		Buffer.BlockCopy(editData, offset, array, 0, num2);
		offset += num2;
		BlockBehaviour block;
		if (GetBlockFromIndex(blockIndex, out block))
		{
			XDataHolder xDataHolder = new XDataHolder();
			xDataHolder.Decode(array, 0);
			block.OnLoad(xDataHolder);
			block.OnPostEdit();
		}
		return offset - num;
	}

	public int RemoteRefreshBlocks(byte[] messageData, int offset)
	{
		nodeController.RefreshBlocks(true, true);
		return 2;
	}

	private bool IsBlockBanned(BlockType blockType)
	{
		int num = (BlockTypeCount.ContainsKey((int)blockType) ? BlockTypeCount[(int)blockType] : 0);
		int num2 = 0;
		int num3 = 0;
		int blockLimit = levelEditor.Settings.GetBlockLimit(blockType);
		int blockCountLimiter = levelEditor.Settings.BlockCountLimiter;
		for (int i = 0; i < buildingBlocks.Count; i++)
		{
			BlockBehaviour blockBehaviour = buildingBlocks[i];
			if (blockBehaviour.Prefab.Type == blockType && !blockBehaviour.wasNotAllowed)
			{
				num2++;
			}
			if (!blockBehaviour.wasNotAllowed && blockBehaviour.BlockID != 71 && blockBehaviour.BlockID != 72)
			{
				num3++;
			}
		}
		if (blockType == BlockType.BuildNode || blockType == BlockType.BuildEdge || blockType == BlockType.ScalingBlock)
		{
			return false;
		}
		if (blockCountLimiter != -1 && base.DisplayBlockCount > blockCountLimiter + 1 && num3 >= blockCountLimiter + 1)
		{
			return true;
		}
		switch (blockLimit)
		{
		case -1:
			return false;
		case 0:
			return true;
		default:
			if (num > blockLimit && num2 >= blockLimit)
			{
				return true;
			}
			return false;
		}
	}

	protected override void PostAddBlock(BlockBehaviour blockClone, BlockInfo blockInfo, XDataHolder xdataholder)
	{
		base.PostAddBlock(blockClone, blockInfo, xdataholder);
		int iD = (int)blockInfo.ID;
		if (!BlockTypeCount.ContainsKey(iD))
		{
			BlockTypeCount.Add(iD, 1);
		}
		else
		{
			Dictionary<int, int> blockTypeCount;
			Dictionary<int, int> dictionary = (blockTypeCount = BlockTypeCount);
			int key2;
			int key = (key2 = iD);
			key2 = blockTypeCount[key2];
			dictionary[key] = key2 + 1;
		}
		if (!isLoadingInfo && !StatMaster.cachingTransformActions)
		{
			DetermineBannedBlocks();
		}
		if (!isLoadingInfo && blockClone.PlacementComplete)
		{
			BlockHealthBar component = blockClone.GetComponent<BlockHealthBar>();
			if (component != null)
			{
				damageController.AddTotalDamage(component.health);
			}
		}
	}

	public override void Reset(bool resetUndoActions = true)
	{
		if (sendRPC && !isLoadingInfo)
		{
			networkAuxAddPiece.SendNetworkMessage(RPCMessageType.Reset);
		}
		Transform transform = player.buildZone.transform;
		_basePosition = transform.position;
		_baseRotation = transform.rotation;
		base.Reset(resetUndoActions);
		damageController.ResetTotalDamage();
		ResetTransformCache();
		if (!isLoadingInfo)
		{
			DetermineBannedBlocks();
		}
		BlockTypeCount.Clear();
	}

	public void OnFlushTransformCache()
	{
		analyzer.Analyze();
	}

	public void RemoteReset()
	{
		bool flag = sendRPC;
		sendRPC = false;
		Reset(false);
		sendRPC = flag;
	}

	public override void RemoveBlock(BlockBehaviour block)
	{
		if (sendRPC && block.PlacementComplete)
		{
			int num = buildingBlocks.IndexOf(block);
			if (num != -1)
			{
				byte[] array = new byte[4];
				NetworkCompression.WriteUInt((uint)num, false, array, 0);
				if (StatMaster.cachingTransformActions)
				{
					CacheBlockTransformAction(RPCMessageType.RemoveBlock, array);
				}
				else
				{
					networkAuxAddPiece.SendNetworkMessage(RPCMessageType.RemoveBlock, array);
				}
			}
		}
		base.RemoveBlock(block);
	}

	public int RemoteRemoveBlock(byte[] removeData, int offset)
	{
		int blockIndex = (int)BitConverter.ToUInt32(removeData, offset);
		BlockBehaviour block;
		if (GetBlockFromIndex(blockIndex, out block))
		{
			base.RemoveBlock(block);
		}
		return 4;
	}

	public override void UnregisterBlock(BlockBehaviour block, bool updateIndices)
	{
		base.UnregisterBlock(block, updateIndices);
		if (BlockTypeCount.ContainsKey(block.BlockID))
		{
			Dictionary<int, int> blockTypeCount;
			Dictionary<int, int> dictionary = (blockTypeCount = BlockTypeCount);
			int blockID;
			int key = (blockID = block.BlockID);
			blockID = blockTypeCount[blockID];
			dictionary[key] = blockID - 1;
		}
		if (updateIndices && !StatMaster.cachingTransformActions)
		{
			DetermineBannedBlocks();
		}
		BlockHealthBar component = block.GetComponent<BlockHealthBar>();
		if (component != null)
		{
			damageController.RemoveTotalDamage(component.health);
		}
	}

	public override void LoadMachineInfo(MachineInfo info, bool resetUndoActions = false)
	{
		if (isSimulating)
		{
			Debug.LogError("Trying to load machine info for player " + PlayerID + " (" + player.name + ") while simulating!");
		}
		else if (isLocalMachine)
		{
			Vector3 newPos;
			Quaternion newRot;
			player.buildZone.ApplyTransform(info.Position, info.Rotation, out newPos, out newRot);
			info.Position = newPos;
			info.Rotation = newRot;
			networkAuxAddPiece.LoadMachineInfo(info);
		}
	}

	public bool ApplyClusterResults(ClusterResultData resultData)
	{
		if (resultData.buildingBlockCount != buildingBlocks.Count)
		{
			return false;
		}
		linkManager.Size = resultData.Size;
		linkManager.Center = resultData.Center;
		if (!GetBlockFromIndex(resultData.centerIndex, out centerBlock))
		{
			Debug.LogError("Center block at index " + resultData.centerIndex + " doesn't exist!");
			hasCenterBlock = false;
			return false;
		}
		hasCenterBlock = true;
		centerBlockTransform = centerBlock.transform;
		lastCenterPos = centerBlockTransform.position;
		_centerPosOffsetToCenter = resultData.CenterOffset;
		linkManager.Clusters.Clear();
		for (int i = 0; i < resultData.clusterData.Length; i++)
		{
			BlockCluster blockCluster = new BlockCluster(linkManager);
			linkManager.Clusters.Add(blockCluster);
			ClusterResultData.ClusterData clusterData = resultData.clusterData[i];
			for (int j = 0; j < clusterData.Nodes.Length; j++)
			{
				BlockBehaviour block = null;
				BlockNode node = null;
				ClusterResultData.ClusterData.ChildNode childNode = clusterData.Nodes[j];
				if (!GetBlockFromIndex(childNode.index, out block) || !linkManager.AddBlock(block, out node))
				{
					Debug.LogError("Couldn't create child block " + clusterData.baseIndex + "!");
					return false;
				}
				node.Neighbours.Clear();
				for (int k = 0; k < childNode.Neighbours.Length; k++)
				{
					ClusterResultData.ClusterData.NeighbourNode neighbourNode = childNode.Neighbours[k];
					BlockBehaviour block2;
					BlockNode node2;
					if (!GetBlockFromIndex(neighbourNode.otherIndex, out block2) || !linkManager.AddBlock(block2, out node2))
					{
						Debug.LogError("Couldn't find neighbour " + neighbourNode.otherIndex + "!");
						return false;
					}
					BlockLink blockLink = new BlockLink(node2);
					for (int l = 0; l < neighbourNode.Triggers.Length; l++)
					{
						ClusterResultData.ClusterData.NeighbourNode.TriggerData triggerData = neighbourNode.Triggers[l];
						blockLink.AddTrigger(triggerData.index, triggerData.isDynamic, true);
					}
					node.Neighbours.Add(blockLink);
				}
				block.ClusterIndex = i;
				if (j == clusterData.baseIndex)
				{
					blockCluster.Base = node;
				}
				blockCluster.Blocks.Add(node);
			}
		}
		linkManager.IgnoredNodes.Clear();
		for (int i = 0; i < linkManager.Nodes.Count; i++)
		{
			BlockNode blockNode = linkManager.Nodes[i];
			if (BlockLinkManager.IgnoreType(blockNode.Type))
			{
				blockNode.Block.ClusterIndex = -2;
				linkManager.IgnoredNodes.Add(blockNode);
			}
			for (int j = 0; j < blockNode.Neighbours.Count; j++)
			{
				BlockLink blockLink2 = blockNode.Neighbours[j];
				bool flag = false;
				BlockLink blockLink3 = null;
				for (int m = 0; m < blockLink2.Other.Neighbours.Count; m++)
				{
					List<BlockLink> neighbours = blockLink2.Other.Neighbours;
					BlockLink blockLink4 = neighbours[m];
					if (blockLink4.Other == blockNode)
					{
						flag = true;
						blockLink3 = blockLink4;
					}
				}
				if (!flag)
				{
					blockLink3 = new BlockLink(blockNode);
					blockLink2.Other.Neighbours.Add(blockLink3);
				}
				for (int m = 0; m < blockLink2.Triggers.Count; m++)
				{
					BlockTrigger blockTrigger = blockLink2.Triggers[m];
					if (blockTrigger.isOwnLink)
					{
						blockLink3.AddTrigger(blockTrigger.Index, blockTrigger.isDynamic, false);
					}
				}
			}
		}
		OnAnalyzeComplete();
		analyzing = false;
		return true;
	}

	public int ProcessClusterResults(byte[] clusterData, int offset, out ClusterResultData resultData)
	{
		int num = offset;
		resultData = new ClusterResultData();
		offset += NetworkCompression.UnpackUInt(clusterData, offset, true, out resultData.buildingBlockCount);
		resultData.Size = default(Vector3);
		NetworkCompression.UnpackVector(clusterData, offset, out resultData.Size);
		offset += 12;
		resultData.Center = default(Vector3);
		NetworkCompression.UnpackVector(clusterData, offset, out resultData.Center);
		offset += 12;
		resultData.CenterOffset = default(Vector3);
		NetworkCompression.UnpackVector(clusterData, offset, out resultData.CenterOffset);
		offset += 12;
		offset += NetworkCompression.UnpackUInt(clusterData, offset, true, out resultData.centerIndex);
		int count;
		offset += NetworkCompression.UnpackUInt(clusterData, offset, true, out count);
		resultData.clusterData = new ClusterResultData.ClusterData[count];
		for (int i = 0; i < count; i++)
		{
			ClusterResultData.ClusterData clusterData2 = new ClusterResultData.ClusterData();
			offset += NetworkCompression.UnpackUInt(clusterData, offset, true, out clusterData2.baseIndex);
			int count2;
			offset += NetworkCompression.UnpackUInt(clusterData, offset, true, out count2);
			clusterData2.Nodes = new ClusterResultData.ClusterData.ChildNode[count2];
			for (int j = 0; j < count2; j++)
			{
				ClusterResultData.ClusterData.ChildNode childNode = new ClusterResultData.ClusterData.ChildNode();
				offset += NetworkCompression.UnpackUInt(clusterData, offset, true, out childNode.index);
				int count3;
				offset += NetworkCompression.UnpackUInt(clusterData, offset, true, out count3);
				childNode.Neighbours = new ClusterResultData.ClusterData.NeighbourNode[count3];
				for (int k = 0; k < count3; k++)
				{
					ClusterResultData.ClusterData.NeighbourNode neighbourNode = new ClusterResultData.ClusterData.NeighbourNode();
					offset += NetworkCompression.UnpackUInt(clusterData, offset, true, out neighbourNode.otherIndex);
					int count4;
					offset += NetworkCompression.UnpackUInt(clusterData, offset, true, out count4);
					neighbourNode.Triggers = new ClusterResultData.ClusterData.NeighbourNode.TriggerData[count4];
					for (int l = 0; l < count4; l++)
					{
						byte b = clusterData[offset++];
						neighbourNode.Triggers[l] = new ClusterResultData.ClusterData.NeighbourNode.TriggerData
						{
							index = b >> 1,
							isDynamic = ((b & 1) != 0)
						};
					}
					childNode.Neighbours[k] = neighbourNode;
				}
				clusterData2.Nodes[j] = childNode;
			}
			resultData.clusterData[i] = clusterData2;
		}
		int num2 = offset - num;
		if (BesiegeLogFilter.logDev)
		{
			Debug.Log(string.Concat("Process results [", PlayerID, ", ", num2, "] ", linkManager.Clusters.Count, " ", linkManager.IgnoredNodes.Count, " ", linkManager.Size, " ", linkManager.Center));
		}
		if (StatMaster.isHosting)
		{
			lastClusterResults = new byte[num2];
			Buffer.BlockCopy(clusterData, num, lastClusterResults, 0, num2);
		}
		return num2;
	}

	private void SendClusterResults()
	{
		int count = linkManager.Clusters.Count;
		int count2 = buildingBlocks.Count;
		byte[][] array = new byte[count][];
		int num = 0;
		int num6;
		for (int i = 0; i < count; i++)
		{
			BlockCluster blockCluster = linkManager.Clusters[i];
			int count3 = blockCluster.Blocks.Count;
			int count4 = blockCluster.Blocks.IndexOf(blockCluster.Base);
			int num2 = NetworkCompression.PackedUIntLength(count4, true);
			int num3 = NetworkCompression.PackedUIntLength(count3, true);
			byte[][] array2 = new byte[count3][];
			int num4 = 0;
			for (int j = 0; j < count3; j++)
			{
				BlockNode blockNode = blockCluster.Blocks[j];
				int buildIndex = blockNode.Block.BuildIndex;
				int num5 = NetworkCompression.PackedUIntLength(buildIndex, true);
				byte[] array3 = blockCluster.Blocks[j].Encode(num5);
				NetworkCompression.PackUInt(buildIndex, array3, 0, true, num5);
				array2[j] = array3;
				num4 += array3.Length;
			}
			num6 = 0;
			byte[] array4 = new byte[num2 + num3 + num4];
			NetworkCompression.PackUInt(count4, array4, num6, true, num2);
			num6 += num2;
			NetworkCompression.PackUInt(count3, array4, num6, true, num3);
			num6 += num3;
			NetworkCompression.WriteArray(array2, array4, num6);
			num6 += num4;
			array[i] = array4;
			num += array4.Length;
		}
		int count5 = ((!hasCenterBlock) ? (-1) : centerBlock.BuildIndex);
		int num7 = NetworkCompression.PackedUIntLength(count, true);
		int num8 = NetworkCompression.PackedUIntLength(count2, true);
		int num9 = NetworkCompression.PackedUIntLength(count5, true);
		byte[] array5 = new byte[num8 + 12 + 12 + 12 + num9 + num7 + num];
		num6 = 0;
		NetworkCompression.PackUInt(count2, array5, num6, true, num8);
		num6 += num8;
		NetworkCompression.PackVector(linkManager.Size, array5, num6);
		num6 += 12;
		NetworkCompression.PackVector(linkManager.Center, array5, num6);
		num6 += 12;
		NetworkCompression.PackVector(_centerPosOffsetToCenter, array5, num6);
		num6 += 12;
		NetworkCompression.PackUInt(count5, array5, num6, true, num9);
		num6 += num9;
		NetworkCompression.PackUInt(count, array5, num6, true, num7);
		num6 += num7;
		NetworkCompression.WriteArray(array, array5, num6);
		num6 += num;
		lastClusterResults = array5;
		networkAuxAddPiece.SendClusterResults(PlayerID, array5);
	}

	public override void OnAnalyzeComplete()
	{
		if (isLocalMachine)
		{
			base.OnAnalyzeComplete();
		}
		UpdateCenterBlock();
		if (StatMaster.isClient)
		{
			blockRadius = Mathf.Max(linkManager.Size.x, linkManager.Size.y, linkManager.Size.z) * 0.5f;
			blockRadiusSqr = blockRadius * blockRadius;
		}
		if (isLocalMachine || !OptionsMaster.networkClusters)
		{
			SendClusterResults();
		}
		if (StatMaster.totalBlocksChanged != null)
		{
			StatMaster.totalBlocksChanged();
		}
	}

	public byte[] Encode(bool includeSimState, ref bool includeClusters)
	{
		List<byte[]> list = new List<byte[]>();
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < buildingBlocks.Count; i++)
		{
			BlockBehaviour blockBehaviour = base.BuildingBlocks[i];
			if (blockBehaviour == null)
			{
				buildingBlocks.RemoveAt(i--);
				num2++;
				Debug.LogError("Null block detected, removing from buildingBlocks!");
			}
			else
			{
				blockBehaviour.BuildIndex -= num2;
				byte[] array = BlockInfo.Encode(blockBehaviour, includeSimState);
				list.Add(array);
				num += array.Length;
			}
		}
		if (num2 > 0)
		{
			foreach (KeyValuePair<Guid, BlockBehaviour> item in new Dictionary<Guid, BlockBehaviour>(guidToBlock))
			{
				if (item.Value == null)
				{
					guidToBlock.Remove(item.Key);
				}
			}
			UpdateIndices();
		}
		int count = buildingBlocks.Count;
		byte[] bytes = Encoding.UTF8.GetBytes(base.Name);
		byte[] outData;
		bool hasMachineData = machineData.Encode(out outData);
		int num3 = MachineInfo.HeaderLength(bytes, hasMachineData, outData);
		int num4 = NetworkCompression.PackedUIntLength(count, true);
		if (includeClusters && lastClusterResults == null)
		{
			includeClusters = false;
			Debug.LogError("Machine doesn't have cluster results while encoding!");
		}
		byte[] array2 = new byte[num3 + num4 + num + (includeClusters ? lastClusterResults.Length : 0)];
		int num5 = 0;
		num5 += MachineInfo.WriteHeader(bytes, hasMachineData, outData, base.BuildingMachine.position, base.BuildingMachine.rotation, machineData, array2, num5);
		NetworkCompression.PackUInt(count, array2, num5, true, num4);
		num5 += num4;
		NetworkCompression.WriteArray(list, array2, num5);
		num5 += num;
		if (includeClusters)
		{
			Buffer.BlockCopy(lastClusterResults, 0, array2, num5, lastClusterResults.Length);
		}
		return array2;
	}

	public void Clone(ServerMachine source)
	{
		isLoadingInfo = true;
		ReplaceMachineUndoAction action = new ReplaceMachineUndoAction(this, source.CreateMachineInfo());
		base.UndoSystem.AddAction(action);
		Reset(false);
		base.Name = source.Name;
		base.Author = source.Author;
		base.MachineType = MachineInfo.MachineType.Multiplayer;
		if (StatMaster.Mode.LevelEditor.moveMachineWithZone)
		{
			source.player.buildZone.UndoTransform(source.BuildingMachine, false);
			base.BuildingMachine.position = source.Position;
			base.BuildingMachine.rotation = source.Rotation;
			source.player.buildZone.ApplyTransform(source.BuildingMachine, false);
			player.buildZone.ApplyTransform(base.BuildingMachine, false);
		}
		machineData = source.machineData.Clone();
		List<BlockData> list = new List<BlockData>();
		List<BlockBehaviour> list2 = source.buildingBlocks;
		for (int i = 0; i < list2.Count; i++)
		{
			list.Add(new BlockData(BlockInfo.FromBlockBehaviour(list2[i])));
		}
		networkAuxAddPiece.LockMessageExecution(true);
		StartCoroutine(IESpawnMachine(list, null));
	}

	public override void PostLoad(bool resetUndoActions)
	{
		base.PostLoad(resetUndoActions);
		if (StatMaster.limitMachines && LevelEditor.Instance.Settings.AllowModMachines && !LevelEditor.Instance.isActive && !isSimulating)
		{
			AdjustBlockLimits();
		}
		DetermineBannedBlocks();
	}

	private void AdjustBlockLimits()
	{
		LevelSettings settings = LevelEditor.Instance.Settings;
		if (settings == null || !isLocalMachine)
		{
			return;
		}
		settings.ResetBlockTypeLimiter();
		for (int i = 0; i < buildingBlocks.Count; i++)
		{
			int blockID = buildingBlocks[i].BlockID;
			int num = (BlockTypeCount.ContainsKey(blockID) ? BlockTypeCount[blockID] : 0);
			if (settings.BlockTypeLimiter.ContainsKey(blockID))
			{
				settings.BaseBlockTypeLimiter[blockID] = num + settings.BlockTypeLimiter[blockID];
			}
		}
	}

	public override MachineInfo CreateMachineInfo(bool withBlocks = true)
	{
		MachineInfo machineInfo = base.CreateMachineInfo(withBlocks);
		Vector3 newPos;
		Quaternion newRot;
		player.buildZone.UndoTransform(machineInfo.Position, machineInfo.Rotation, out newPos, out newRot);
		machineInfo.Position = newPos;
		machineInfo.Rotation = newRot;
		return machineInfo;
	}

	public int Decode(bool hasClusterData, bool localSpace, byte[] data, int offset)
	{
		int num = offset;
		if (isSimulating)
		{
			Debug.LogError("Trying to load machine info while simulating!");
			return offset;
		}
		isLoadingInfo = true;
		Reset(false);
		int num2 = data[offset];
		offset++;
		string text = Encoding.UTF8.GetString(data, offset, num2);
		base.Name = text;
		offset += num2;
		NetworkCompression.UnpackVector(data, offset, out posHolder);
		offset += 12;
		NetworkCompression.UnpackQuaternion(data, offset, out rotHolder);
		offset += 16;
		if (!localSpace)
		{
			Vector3 newPos;
			Quaternion newRot;
			player.buildZone.ApplyTransform(posHolder, rotHolder, out newPos, out newRot);
			posHolder = newPos;
			rotHolder = newRot;
		}
		bool flag = data[offset] == 1;
		offset++;
		machineData = new XDataHolder();
		if (flag)
		{
			offset += machineData.Decode(data, offset);
		}
		simFrameData.Clear();
		byte[] array = null;
		int count;
		offset += NetworkCompression.UnpackUInt(data, offset, true, out count);
		List<BlockData> list = new List<BlockData>(count);
		bool flag2 = false;
		for (ushort num3 = 0; num3 < count; num3++)
		{
			BlockInfo blockInfo = BlockInfo.Decode(num3, data, offset);
			blockInfo.BlockData.WasLoadedFromFile = true;
			offset += blockInfo.EncodedSize;
			flag2 = false;
			BlockData item = null;
			if (blockInfo.HasSimData)
			{
				int num4 = data[offset];
				if (num4 > 0)
				{
					int dataSize = NetworkEntity.GetDataSize(num4);
					array = new byte[dataSize];
					Buffer.BlockCopy(data, offset, array, 0, dataSize);
					item = new BlockData(blockInfo, array);
					flag2 = true;
					offset += dataSize;
				}
				else
				{
					offset++;
				}
			}
			if (!flag2)
			{
				item = new BlockData(blockInfo);
			}
			list.Add(item);
		}
		networkAuxAddPiece.LockMessageExecution(true);
		ClusterResultData resultData = null;
		if (hasClusterData)
		{
			offset += ProcessClusterResults(data, offset, out resultData);
		}
		base.BuildingMachine.position = posHolder;
		base.BuildingMachine.rotation = rotHolder;
		StartCoroutine(IESpawnMachine(list, resultData));
		return offset - num;
	}

	private IEnumerator IESpawnMachine(List<BlockData> blockData, ClusterResultData clusterResults)
	{
		spawningMachine = true;
		int spawnedBlocks = 0;
		analyzer.SetLocked(true);
		bool even = false;
		float sixtyth = 1f / 60f;
		for (ushort i = 0; i < blockData.Count; i++)
		{
			BlockData block = blockData[i];
			BlockBehaviour newBlock;
			if (base.AddBlock(block.info, out newBlock))
			{
				if (newBlock.Prefab.hasBVC)
				{
					newBlock.VisualController.PlaceFromBlockInfo(block.info);
				}
				if (!isLocalMachine)
				{
					newBlock.OnAddRemote();
				}
			}
			if (block.hasData)
			{
				simFrameData.Add(i, block.data);
			}
			if (networkAuxAddPiece.receivedGameState && (!isLocalMachine || StatMaster.isHosting || StatMaster.IsLevelEditorOnly))
			{
				spawnedBlocks++;
				float c = OptionsMaster.BesiegeConfig.MVBlocksPerFrame;
				c = Mathf.Max(5f, c * sixtyth / Time.unscaledDeltaTime);
				if (even)
				{
					c *= 0.25f;
				}
				if ((float)spawnedBlocks >= c)
				{
					spawnedBlocks = 0;
					yield return null;
					even = !even;
				}
			}
		}
		yield return StartCoroutine(nodeController.IERefreshBlocks(true, true));
		analyzer.SetLocked(false);
		yield return null;
		isLoadingInfo = false;
		spawningMachine = false;
		PostLoad(false);
		if (clusterResults != null)
		{
			ApplyClusterResults(clusterResults);
		}
		networkAuxAddPiece.LockMessageExecution(false);
		UpdateMachineDLCStatus();
	}

	protected override void OnDestroy()
	{
		if (levelEditor != null)
		{
			LevelEditor obj = levelEditor;
			obj.LevelSettingsChanged = (LevelEditor.LevelSettingsChangedHandler)Delegate.Remove(obj.LevelSettingsChanged, new LevelEditor.LevelSettingsChangedHandler(OnLevelSettingsChanged));
		}
		ResetCameraTarget();
	}

	private void ResetTransformCache()
	{
		if (StatMaster.cachingTransformActions)
		{
		}
		cachedBlockTransformSize = 0;
		cachedBlockTransforms.Clear();
		StatMaster.cachingTransformActions = false;
	}
}
