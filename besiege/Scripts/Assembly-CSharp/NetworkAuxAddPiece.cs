using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BesiegeDlc;
using InternalModding;
using InternalModding.Mods;
using Localisation;
using Steamworks;
using UnityEngine;

[AddComponentMenu("Core/Multiplayer/Network Aux Add Piece")]
public class NetworkAuxAddPiece : MonoBehaviour
{
	private class ServerRequestData
	{
		public PlayerData player;

		public OrderedRPC.RPCMessage message;

		public bool hasMachine;

		public ServerMachine machine;

		public bool acceptedRequest;
	}

	public const int ZONE_SIZE = 18;

	public const float ZONE_Y_OFFSET = 5.05f;

	public const float ZONE_PLACE_Y_OFFSET = 5.072f;

	private const float ZombieTimeout = 2f;

	private const float UpdatePingFrequency = 1f;

	public static bool hasInstance;

	public static NetworkAuxAddPiece Instance;

	public static int TIMESCALE_LENGTH = 1;

	public static int floatByteLength = 4;

	public GameObject buildZonePrefab;

	[HideInInspector]
	public ushort ownerId;

	[HideInInspector]
	public NetworkHUD hud;

	[HideInInspector]
	public bool receivedGameState;

	[HideInInspector]
	public bool requestedSimFrame;

	private NetworkAddPiece networkAddPiece;

	private List<BuildZoneObject> zoneObjects = new List<BuildZoneObject>();

	private List<PlayerBuildZone> buildZones = new List<PlayerBuildZone>();

	private List<ushort> clientList;

	private FragmentedRPC blockTransformCacheBuffer;

	private FragmentedRPC entityAddBuffer;

	private FragmentedRPC entityRemoveBuffer;

	private FragmentedRPC entityUpdateBuffer;

	private FragmentedRPC gameStateBuffer;

	private FragmentedRPC clusterBuffer;

	private FragmentedRPC editKeyBuffer;

	private FragmentedRPC rebindKeyBuffer;

	private FragmentedRPC machineDataBuffer;

	private FragmentedRPC paintBuffer;

	private FragmentedRPC blockDataBuffer;

	private FragmentedRPC resetBlockBuffer;

	private FragmentedRPC pasteBlockBuffer;

	private float gameStateCorrection = float.MaxValue;

	private string lastLevelData;

	private CustomLevel level;

	private LevelEditor levelEditor;

	private FragmentedRPC loadLevelBuffer;

	private FragmentedRPC logicDataBuffer;

	private FragmentedRPC loadMachineBuffer;

	private FragmentedRPC modJoinErrorBuffer;

	private int maxMessageCount;

	private OrderedRPC messageBuffer;

	private BesiegeNetworkManager networkManager;

	private NetworkScene networkScene;

	private OrderedRPCQueue orderedQueue;

	private List<Vector2> playerZoneDirections;

	private Vector3 posHolder = default(Vector3);

	private Quaternion rotHolder = default(Quaternion);

	private FragmentedRPC simFrameBuffer;

	private float simFrameCorrection = float.MaxValue;

	private byte[] skipData;

	private FragmentedRPC printRPCBuffer;

	private float lastPingUpdate;

	private IEnumerator loadMachineCoroutine;

	private float halfChokeTime = OptionsMaster.chokeWaitTime * 0.5f;

	public bool MessagesLocked
	{
		get
		{
			return orderedQueue.isLocked;
		}
	}

	public int FragmentMessageHeaderSize
	{
		get
		{
			return 2;
		}
	}

	public bool PlayersLimited
	{
		get
		{
			return OptionsMaster.limitPlayers && StatMaster.activePlayerCount >= levelEditor.Settings.MaxPlayers;
		}
	}

	public static int BuildZoneCount
	{
		get
		{
			return (!(Instance == null)) ? Instance.zoneObjects.Count : 9;
		}
	}

	public void ChangeBlockSkin(BlockBehaviour block, BlockSkinLoader.SkinPack.Skin skin)
	{
		block.VisualController.ReplaceSkin(skin);
		byte[] array = skin.pack.Encode();
		int num = NetworkCompression.PackedUIntLength(block.BuildIndex, false);
		int num2 = NetworkCompression.PackedUIntLength(array.Length, true);
		byte[] array2 = new byte[2 + num + num2 + array.Length];
		int num3 = 0;
		Machine parentMachine = block.ParentMachine;
		NetworkCompression.WriteUInt16(parentMachine.PlayerID, array2, num3);
		num3 += 2;
		NetworkCompression.PackUInt(block.BuildIndex, array2, num3, false, num);
		num3 += num;
		NetworkCompression.PackUInt(array.Length, array2, num3, true, num2);
		num3 += num2;
		Buffer.BlockCopy(array, 0, array2, num3, array.Length);
		if (StatMaster.cachingTransformActions)
		{
			(parentMachine as ServerMachine).CacheBlockTransformAction(RPCMessageType.EditBlockSkin, array2);
		}
		else
		{
			SendNetworkMessage(RPCMessageType.EditBlockSkin, array2);
		}
	}

	public void ClearAllBuffers()
	{
		messageBuffer.Clear();
		loadLevelBuffer.Clear();
		logicDataBuffer.Clear();
		loadMachineBuffer.Clear();
		modJoinErrorBuffer.Clear();
		gameStateBuffer.Clear();
		simFrameBuffer.Clear();
		clusterBuffer.Clear();
		machineDataBuffer.Clear();
		blockTransformCacheBuffer.Clear();
		networkAddPiece.clientInputBuffer.Clear();
		editKeyBuffer.Clear();
		paintBuffer.Clear();
		rebindKeyBuffer.Clear();
		entityAddBuffer.Clear();
		entityRemoveBuffer.Clear();
		entityUpdateBuffer.Clear();
		printRPCBuffer.Clear();
		blockDataBuffer.Clear();
		resetBlockBuffer.Clear();
		pasteBlockBuffer.Clear();
		orderedQueue.Clear();
	}

	public void ClearBuffers(ushort playerId)
	{
		messageBuffer.Clear(playerId);
		loadLevelBuffer.Clear(playerId);
		loadMachineBuffer.Clear(playerId);
		modJoinErrorBuffer.Clear(playerId);
		simFrameBuffer.Clear(playerId);
		clusterBuffer.Clear(playerId);
		machineDataBuffer.Clear(playerId);
		blockTransformCacheBuffer.Clear(playerId);
		blockDataBuffer.Clear(playerId);
		resetBlockBuffer.Clear(playerId);
		pasteBlockBuffer.Clear(playerId);
		editKeyBuffer.Clear(playerId);
		rebindKeyBuffer.Clear(playerId);
		entityAddBuffer.Clear(playerId);
		entityRemoveBuffer.Clear(playerId);
		entityUpdateBuffer.Clear(playerId);
		printRPCBuffer.Clear(playerId);
		paintBuffer.Clear();
	}

	public void ClearPlayers()
	{
		for (int i = 0; i < Playerlist.Players.Count; i++)
		{
			RemoveMachine(Playerlist.Players[i]);
		}
		PlayerData.localPlayer = null;
		PlayerData.hasLocalPlayer = false;
		Playerlist.ClearPlayers();
		hud.UpdatePlayers();
	}

	public void ClearSpawns()
	{
		while (zoneObjects.Count > 0)
		{
			zoneObjects[0].OnRemove();
		}
	}

	public void TurnOffZoneColliders()
	{
		for (int i = 0; i < zoneObjects.Count; i++)
		{
			BuildZoneObject buildZoneObject = zoneObjects[i];
			if (buildZoneObject == null)
			{
				zoneObjects.RemoveAt(i);
				i--;
				Debug.LogError("Removed null zone from zoneObjects, this shouldn't happen!");
			}
			else if (buildZoneObject.gameObject != null)
			{
				buildZoneObject.ToggleCollider(false);
			}
		}
	}

	public void DropClient(ushort networkId)
	{
		DropClient(networkId, null);
	}

	public void DropClient(ushort networkId, string reason)
	{
		string text = "Player kicked";
		if (networkId == 0)
		{
			return;
		}
		PlayerData player;
		if (!Playerlist.GetPlayer(networkId, out player))
		{
			Debug.LogWarning("Tried to drop a non-existing client '" + networkId + "'");
			return;
		}
		if (player.isZombie)
		{
			Debug.LogWarning("Client '" + networkId + "' is already being kicked");
			return;
		}
		string text2 = string.Format("'{0}' was kicked.", player.name);
		if (!string.IsNullOrEmpty(reason))
		{
			text = string.Format("{0}: {1}", text, reason);
			text2 = string.Format("{0} Reason: {1}", text2, reason);
		}
		SendConsolePrint(text2);
		byte[] bytes = Encoding.UTF8.GetBytes(text);
		SendPlayerMessage(networkId, RPCMessageType.Disconnect, bytes);
		player.lastPacketTime = Time.time;
		player.isZombie = true;
	}

	public void ClearSimFrameBuffer()
	{
		simFrameBuffer.Clear();
	}

	public ServerMachine CreateClient(PlayerData player, Vector3 position, Quaternion rotation, bool addStartBlock)
	{
		ushort networkId = player.networkId;
		ServerMachine serverMachine = MachineObjectTracker.CreateMachine<ServerMachine>("Player " + networkId);
		serverMachine.Position = position;
		serverMachine.Rotation = rotation;
		serverMachine.SetPlayer(player);
		player.machine = serverMachine;
		player.isSpectator = false;
		if (StatMaster.isHosting)
		{
			for (int i = 0; i < networkScene.clientList.Count; i++)
			{
				PlayerData playerData = networkScene.clientList[i];
				serverMachine.fullUpdate.Add(playerData.networkId);
			}
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(buildZonePrefab);
		gameObject.name = "BuildZone " + networkId;
		PlayerBuildZone component = gameObject.GetComponent<PlayerBuildZone>();
		component.transform.position = position;
		component.transform.rotation = rotation;
		(serverMachine.boundingBoxController = gameObject.GetComponentInChildren<NetworkBoundingBoxController>(true)).machine = serverMachine;
		component.Init(player);
		player.buildZone = component;
		buildZones.Add(component);
		if (addStartBlock)
		{
			BlockBehaviour block;
			serverMachine.RemoteAddBlock(Vector3.zero, Quaternion.identity, BlockType.StartingBlock, false, out block);
		}
		if (player.isLocalPlayer)
		{
			component.ResetBounds();
			networkAddPiece.SetupZone(networkId);
			SingleInstance<MachineObjectTracker>.Instance.SetActiveMachine(serverMachine);
			UpdateBuildZoneTransform(position, rotation);
			StatMaster.SetSimulationState(SimulationState.BuildMode);
		}
		player.PlayMode = BesiegePlayMode.BuildMode;
		StatMaster.activePlayerCount++;
		if (addStartBlock)
		{
			serverMachine.PostLoad();
		}
		return serverMachine;
	}

	public void OnClientStop()
	{
		StopLoadLocalMachine();
		StopAllCoroutines();
		networkAddPiece.OnClientStop();
		hud.OnClientStop();
		ClearAllBuffers();
	}

	public List<ushort> GetClientList(ushort excludeId)
	{
		if (clientList.Count > 0)
		{
			clientList.Clear();
		}
		for (int i = 0; i < networkScene.clientIDList.Count; i++)
		{
			ushort num = networkScene.clientIDList[i];
			if (num != excludeId)
			{
				clientList.Add(num);
			}
		}
		return clientList;
	}

	public byte[] GetFragmentedMessage(ushort current, byte[] data)
	{
		NetworkCompression.WriteUInt16(current, data, 0);
		return data;
	}

	public bool GetZone(ushort playerId, out PlayerBuildZone buildZone)
	{
		for (int i = 0; i < buildZones.Count; i++)
		{
			PlayerBuildZone playerBuildZone = buildZones[i];
			if (playerBuildZone.player.networkId == playerId)
			{
				buildZone = playerBuildZone;
				return true;
			}
		}
		buildZone = null;
		return false;
	}

	public bool HasNextZone()
	{
		for (int i = 0; i < zoneObjects.Count; i++)
		{
			BuildZoneObject buildZoneObject = zoneObjects[i];
			if (!buildZoneObject.hasZone)
			{
				return true;
			}
		}
		return false;
	}

	public bool GetNextZone(out BuildZoneObject zoneObj)
	{
		for (int i = 0; i < zoneObjects.Count; i++)
		{
			BuildZoneObject buildZoneObject = zoneObjects[i];
			if (!buildZoneObject.hasZone)
			{
				zoneObj = buildZoneObject;
				return true;
			}
		}
		zoneObj = null;
		return false;
	}

	public byte[] GetPlayerConfig()
	{
		if (BesiegeLogFilter.logDebug)
		{
			Debug.Log("Sending player config..");
		}
		byte[] bytes = Encoding.UTF8.GetBytes(VersionNumber.GetVersionString());
		byte[] bytes2 = Encoding.UTF8.GetBytes(OptionsMaster.BesiegeConfig.PlayerName);
		int num = NetworkCompression.PackedUIntLength(bytes2.Length, true);
		int num2 = 1 + num + bytes2.Length + 1 + bytes.Length + 4;
		byte[] modConfigHash = CompatibilityChecker.GetModConfigHash();
		num2 += modConfigHash.Length;
		PlayerPlatform playerPlatform = PlayerPlatform.Unknown;
		byte[] array = new byte[8];
		byte[] array2 = null;
		if (OptionsMaster.networkType == PlayerNetworkType.Playfab)
		{
			array2 = Encoding.UTF8.GetBytes(SingleInstance<WorkshopManager>.Instance.GetPlayerName());
			playerPlatform = PlayerPlatform.Steam;
			array = BitConverter.GetBytes(SteamUser.GetSteamID().m_SteamID);
			num2 += 2 + array2.Length;
			if (playerPlatform != PlayerPlatform.Unknown)
			{
				num2 += array.Length;
			}
		}
		byte[] array3 = new byte[num2];
		int num3 = 0;
		array3[num3] = (byte)(OptionsMaster.spectatorEnabled ? 1u : 0u);
		num3++;
		NetworkCompression.PackUInt(bytes2.Length, array3, num3, true, num);
		num3 += num;
		Buffer.BlockCopy(bytes2, 0, array3, num3, bytes2.Length);
		num3 += bytes2.Length;
		if (OptionsMaster.networkType == PlayerNetworkType.Playfab)
		{
			array3[num3++] = (byte)playerPlatform;
			array3[num3++] = (byte)array2.Length;
			Buffer.BlockCopy(array2, 0, array3, num3, array2.Length);
			num3 += array2.Length;
			Buffer.BlockCopy(array, 0, array3, num3, array.Length);
			num3 += array.Length;
		}
		array3[num3] = (byte)bytes.Length;
		num3++;
		Buffer.BlockCopy(bytes, 0, array3, num3, bytes.Length);
		num3 += bytes.Length;
		uint maskFromDlcTypes = DlcManager.Instance.GetMaskFromDlcTypes(DlcManager.Instance.GetLocalDlcTypes(false));
		NetworkCompression.WriteUInt(maskFromDlcTypes, false, array3, num3);
		num3 += 4;
		Buffer.BlockCopy(modConfigHash, 0, array3, num3, modConfigHash.Length);
		num3 += modConfigHash.Length;
		return array3;
	}

	public void HideLoadingText()
	{
		if (!receivedGameState || !StatMaster.waitingForServerResponse)
		{
			if (StatMaster.isHosting)
			{
				hud.OnStartHost();
			}
			else
			{
				hud.OnJoin();
			}
		}
	}

	private int CompareVersion(string v1, string v2)
	{
		if (v1.Equals(v2))
		{
			return 0;
		}
		string[] array = v1.Trim().Normalize().Split('-');
		string[] array2 = v2.Trim().Normalize().Split('-');
		if (array.Length != array2.Length)
		{
			Debug.LogWarning("Full Version length not matching! ClientVersion=" + v1 + " Serverversion=" + v2);
			return -1;
		}
		System.Version version = new System.Version(array[0]);
		System.Version version2 = new System.Version(array2[0]);
		if (version != version2)
		{
			Debug.LogWarning(string.Concat("Version not matching! ClientVersion=", version, " Serverversion=", version2));
			return -1;
		}
		int result;
		int result2;
		if (!int.TryParse(array[1], out result) || !int.TryParse(array2[1], out result2))
		{
			Debug.LogError("Trying to compare faulty changeset numbers! ClientChangeSet=" + array[1] + " ServerChangeSet=" + array2[1]);
			return -1;
		}
		if (result < result2 - 100)
		{
			Debug.LogWarning("Changeset not matching! ClientChangeSet=" + result + " ServerChangeSet=" + result2);
			return -1;
		}
		if (result > result2 + 100)
		{
			Debug.LogWarning("Changeset not matching! ClientChangeSet=" + result + " ServerChangeSet=" + result2);
			return 1;
		}
		return 0;
	}

	public void RemoveServerPlayer(PlayerData player)
	{
		StartCoroutine(IEPlayerLeave(player));
	}

	public void InitServerPlayer(PlayerData player, byte[] configData)
	{
		int num = 0;
		bool flag = configData[num] == 1;
		bool flag2 = flag;
		num++;
		int count;
		num += NetworkCompression.UnpackUInt(configData, num, true, out count);
		byte[] array = new byte[count];
		Buffer.BlockCopy(configData, num, array, 0, count);
		string text = Encoding.UTF8.GetString(configData, num, count);
		num += count;
		PlayerPlatform playerPlatform = PlayerPlatform.Unknown;
		byte[] array2 = null;
		ulong num2 = 0uL;
		byte[] array3 = null;
		string text2 = null;
		if (OptionsMaster.networkType == PlayerNetworkType.Playfab)
		{
			playerPlatform = (PlayerPlatform)configData[num++];
			array3 = new byte[configData[num++]];
			Buffer.BlockCopy(configData, num, array3, 0, array3.Length);
			num += array3.Length;
			text2 = Encoding.UTF8.GetString(array3);
			array2 = new byte[8];
			Buffer.BlockCopy(configData, num, array2, 0, array2.Length);
			num2 = BitConverter.ToUInt64(array2, 0);
			num += array2.Length;
		}
		int num3 = configData[num];
		num++;
		string text3 = Encoding.UTF8.GetString(configData, num, num3);
		string versionString = VersionNumber.GetVersionString();
		int num4 = CompareVersion(text3, versionString);
		if (num4 != 0)
		{
			if (BesiegeLogFilter.logDebug)
			{
				Debug.Log(text + " (" + player.networkId + ") join error: version mismatch! clientVersion=" + text3 + " serverVersion=" + versionString);
			}
			SendJoinError(player.networkId, (byte)((num4 != -1) ? 6u : 5u));
			return;
		}
		num += num3;
		uint num5 = NetworkCompression.ReadUInt(false, configData, num);
		num += 4;
		byte[] array4 = new byte[4];
		DlcManager instance = DlcManager.Instance;
		List<uint> dlcTypesFromMask = instance.GetDlcTypesFromMask(num5);
		List<uint> dlcTypesFromMask2 = instance.GetDlcTypesFromMask(NetworkScene.ServerSettings.dlcMask);
		for (int i = 0; i < dlcTypesFromMask2.Count; i++)
		{
			DlcManager.DlcType dlcType = (DlcManager.DlcType)dlcTypesFromMask2[i];
			if (!dlcTypesFromMask.Contains((uint)dlcType))
			{
				Debug.LogError(text + " (" + player.networkId + ") join error: Client (dlcMask=" + num5 + ") does not have DLC " + instance.GetDlcName(dlcType) + " (" + (uint)dlcType + "))!");
				NetworkCompression.WriteUInt(NetworkScene.ServerSettings.dlcMask, false, array4, 0);
				SendPlayerMessage(player.networkId, RPCMessageType.DlcJoinError, array4);
				return;
			}
		}
		switch (CompatibilityChecker.CompareModConfigHash(configData, ref num))
		{
		case 0:
			if (BesiegeLogFilter.logDebug)
			{
				Debug.LogError(text + " (" + player.networkId + ") join error: mod mismatch!");
			}
			SendModJoinError(player.networkId);
			return;
		case -1:
			if (BesiegeLogFilter.logDebug)
			{
				Debug.LogError(text + " (" + player.networkId + ") join error: no mod list hash!");
			}
			SendJoinError(player.networkId, 0);
			return;
		}
		int num6 = 2 + array.Length;
		if (OptionsMaster.networkType == PlayerNetworkType.Playfab)
		{
			num6++;
			if (playerPlatform != PlayerPlatform.Unknown)
			{
				num6 += 1 + array3.Length;
				num6 += array2.Length;
			}
		}
		byte[] array5 = new byte[num6];
		num = 0;
		NetworkCompression.WriteUInt16(player.networkId, array5, num);
		num += 2;
		player.wantSpectator = flag2;
		player.name = text;
		Buffer.BlockCopy(array, 0, array5, num, array.Length);
		num += array.Length;
		if (OptionsMaster.networkType == PlayerNetworkType.Playfab)
		{
			player.platform = playerPlatform;
			player.platformUserName = text2;
			player.platformUserId = num2;
			array5[num++] = (byte)playerPlatform;
			if (playerPlatform != PlayerPlatform.Unknown)
			{
				Buffer.BlockCopy(array3, 0, array5, num, array3.Length);
				num += array3.Length;
				Buffer.BlockCopy(array2, 0, array5, num, array2.Length);
			}
			if (BesiegeLogFilter.logDebug)
			{
				Debug.Log(string.Concat("Initializing player ", player.networkId, " (", player.name, "), spec: ", flag2, ", platform=", playerPlatform, " userName=", text2, " userId=", num2));
			}
		}
		player.initReady = true;
		if (PlayerData.onInitReady != null)
		{
			PlayerData.onInitReady(player, true);
		}
		StartCoroutine(IEPlayerJoin(player, array5, flag));
	}

	public void LoadLevel(string levelData, string levelName, bool localLoad = true)
	{
		lastLevelData = levelData;
		if (localLoad)
		{
			byte[] messageData = CLZF2.Compress(level.Encode(levelData, levelName));
			SendFragmentedServerMessage(RPCMessageType.LoadLevel, messageData);
			return;
		}
		if (StatMaster.isHosting)
		{
			StopAllSimulation();
			byte[] messageData = CLZF2.Compress(level.Encode(levelData, levelName));
			SendFragmentedNetworkMessage(RPCMessageType.LoadLevel, messageData);
		}
		OnLevelLoad(levelData, levelName);
	}

	public void LoadLocalMachine(MachineInfo info)
	{
		StopLoadLocalMachine();
		loadMachineCoroutine = IELoadLocalMachine(info);
		StartCoroutine(loadMachineCoroutine);
	}

	public void StopLoadLocalMachine()
	{
		if (loadMachineCoroutine != null)
		{
			StopCoroutine(loadMachineCoroutine);
		}
	}

	private IEnumerator IELoadLocalMachine(MachineInfo info)
	{
		if (MessagesLocked || StatMaster.waitingForServerResponse)
		{
			while (MessagesLocked || StatMaster.waitingForServerResponse)
			{
				yield return null;
			}
		}
		PlayerData localPlayer = PlayerData.localPlayer;
		if (localPlayer.isSpectator)
		{
			Debug.LogError("Player is spectator, returning from LoadLocalMachine!");
			yield return null;
		}
		ServerMachine machine = localPlayer.machine;
		if (machine.isSimulating)
		{
			Debug.LogError("Machine is simulating, returning from LoadLocalMachine!");
			yield return null;
		}
		ReplaceMachineUndoAction undoAction = new ReplaceMachineUndoAction(machine, info);
		machine.UndoSystem.AddAction(undoAction);
		machine.LoadMachineInfo(info);
		machine.ToggleModification(!StatMaster.LimitMachineModification);
	}

	public void LoadMachineInfo(MachineInfo machineInfo)
	{
		ReferenceMaster.ConsoleController.AppendLogLine("Loading machine: '" + machineInfo.Name + "'");
		byte[] inputBytes = machineInfo.Encode();
		byte[] messageData = CLZF2.Compress(inputBytes);
		SetLoadingText(LocalisationManager.GetTranslation(2842));
		StatMaster.WaitForServerResponse(StatMaster.ServerResponseType.MachineLoad, true);
		SendFragmentedServerMessage(RPCMessageType.LoadMachine, messageData);
	}

	public void PickAllowedMachine(int index)
	{
		ReferenceMaster.ConsoleController.AppendLogLine("Picked limited machine " + index + string.Empty);
		int num = NetworkCompression.PackedUIntLength(index, true);
		byte[] array = new byte[num];
		NetworkCompression.PackUInt(index, array, 0, true, num);
		SetLoadingText(LocalisationManager.GetTranslation(2842));
		StatMaster.WaitForServerResponse(StatMaster.ServerResponseType.MachineLoad, true);
		SendServerRequest(RPCMessageType.PickMachine, array);
	}

	public void LockMessageExecution(bool toggle)
	{
		orderedQueue.ToggleLock(toggle);
	}

	public void OnServerSettingsChanged()
	{
		ServerSettings serverSettings = NetworkScene.ServerSettings;
		NetworkInterpolation.AdjustThreshold(serverSettings.vecThreshold, serverSettings.rotThreshold);
		SendNetworkMessage(RPCMessageType.ServerSettings, serverSettings.Encode());
	}

	public PlayerData PlayerConnected(ushort playerId, bool isSpectator)
	{
		PlayerData player;
		if (Playerlist.GetPlayer(playerId, out player))
		{
			return player;
		}
		player = new PlayerData(playerId);
		if (StatMaster.isHosting)
		{
			for (int i = 0; i < Playerlist.Players.Count; i++)
			{
				PlayerData playerData = Playerlist.Players[i];
				if (!playerData.isSpectator)
				{
					ServerMachine machine = playerData.machine;
					machine.fullUpdate.Add(playerId);
				}
			}
		}
		if (playerId == ownerId)
		{
			PlayerData.localPlayer = player;
			player.isLocalPlayer = true;
			PlayerData.hasLocalPlayer = true;
		}
		Playerlist.AddPlayer(player);
		levelEditor.PlayerJoin(playerId);
		return player;
	}

	public void PlayerDisconnected(ushort playerId)
	{
		PlayerData player;
		if (!Playerlist.GetPlayer(playerId, out player))
		{
			return;
		}
		RemoveMachine(player);
		ClearBuffers(playerId);
		Playerlist.DeletePlayer(player);
		levelEditor.PlayerLeave(playerId);
		if (StatMaster.isHosting)
		{
			for (int i = 0; i < Playerlist.Players.Count; i++)
			{
				PlayerData playerData = Playerlist.Players[i];
				if (!playerData.isSpectator)
				{
					ServerMachine machine = playerData.machine;
					if (machine.fullUpdate.Contains(playerId))
					{
						machine.fullUpdate.Remove(playerId);
					}
					else if (machine.essentialUpdate.Contains(playerId))
					{
						machine.essentialUpdate.Remove(playerId);
					}
				}
			}
		}
		if (OptionsMaster.votingEnabled)
		{
			networkAddPiece.RefreshPlayerViewer();
			if (StatMaster.isHosting)
			{
				SendNetworkMessage(RPCMessageType.RefreshPlayerStates);
			}
		}
		hud.UpdatePlayers();
		ReferenceMaster.ConsoleController.AppendLogLine("Player '" + player.name + "' disconnected.");
	}

	public void PlayerJoin(byte[] playerData)
	{
		ushort num = NetworkCompression.ReadUInt16(playerData, 0);
		int num2 = 2;
		if (num == ownerId)
		{
			if (StatMaster.isClient)
			{
				SetLoadingText(LocalisationManager.GetTranslation(3371));
			}
		}
		else
		{
			if (!receivedGameState)
			{
				return;
			}
			PlayerData playerData2 = PlayerConnected(num, true);
			byte[] array = new byte[playerData.Length - num2];
			Buffer.BlockCopy(playerData, num2, array, 0, array.Length);
			string text = Encoding.UTF8.GetString(array);
			playerData2.name = text;
			num2 += array.Length;
			if (OptionsMaster.networkType == PlayerNetworkType.Playfab)
			{
				playerData2.platform = (PlayerPlatform)playerData[num2++];
				if (playerData2.platform != PlayerPlatform.Unknown)
				{
					byte[] array2 = new byte[playerData[num2++]];
					Buffer.BlockCopy(playerData, num2, array2, 0, array2.Length);
					num2 += array2.Length;
					playerData2.platformUserName = Encoding.UTF8.GetString(array2);
					byte[] array3 = new byte[8];
					Buffer.BlockCopy(playerData, num2, array3, 0, array3.Length);
					playerData2.platformUserId = BitConverter.ToUInt64(array3, 0);
				}
			}
			playerData2.initReady = true;
			if (PlayerData.onInitReady != null)
			{
				PlayerData.onInitReady(playerData2, true);
			}
			hud.UpdatePlayers();
		}
	}

	private IEnumerator ToggleMachinePlayerMode(PlayerData player, BesiegePlayMode playMode)
	{
		yield return StartCoroutine(ToggleMachinePlayerMode(new List<PlayerData> { player }, playMode));
	}

	private IEnumerator ToggleMachinePlayerMode(List<PlayerData> players, BesiegePlayMode playMode)
	{
		LockMessageExecution(true);
		List<Machine> machineList = new List<Machine>();
		List<Machine> stopMachineList = new List<Machine>();
		foreach (PlayerData player in players)
		{
			if (player.isSpectator || player.PlayMode == playMode)
			{
				continue;
			}
			player.PlayMode = playMode;
			if (player.machine.isSimulating)
			{
				if ((playMode == BesiegePlayMode.GlobalSimulation && player.machine.isLocalSim) || (playMode == BesiegePlayMode.LocalSimulation && !player.machine.isLocalSim))
				{
					stopMachineList.Add(player.machine);
				}
			}
			else if (playMode == BesiegePlayMode.LocalSimulation)
			{
				if (StatMaster.Mode.curtainMode && !player.isLocalPlayer)
				{
					continue;
				}
				player.machine.isLocalSim = true;
			}
			else
			{
				player.machine.isLocalSim = false;
			}
			if (player.isLocalPlayer)
			{
				switch (playMode)
				{
				case BesiegePlayMode.BuildMode:
					StatMaster.SetSimulationState(SimulationState.BuildMode);
					break;
				case BesiegePlayMode.GlobalSimulation:
					StatMaster.SetSimulationState(SimulationState.GlobalSimulation);
					break;
				case BesiegePlayMode.LocalSimulation:
					StatMaster.SetSimulationState(SimulationState.LocalSimulation);
					break;
				}
				networkAddPiece.UpdatePlayIcon();
			}
			machineList.Add(player.machine);
		}
		if (stopMachineList.Count > 0)
		{
			yield return StartCoroutine(networkAddPiece.StopMachines(stopMachineList));
			for (int i = 0; i < stopMachineList.Count; i++)
			{
				stopMachineList[i].isLocalSim = playMode == BesiegePlayMode.LocalSimulation;
			}
		}
		if (machineList.Count > 0)
		{
			if (playMode != BesiegePlayMode.BuildMode)
			{
				yield return StartCoroutine(networkAddPiece.StartMachines(machineList));
			}
			else
			{
				yield return StartCoroutine(networkAddPiece.StopMachines(machineList));
			}
		}
		StatMaster.UpdateSimulationState();
		LockMessageExecution(false);
	}

	public void RegisterSpawn(BuildZoneObject zoneObj)
	{
		if (!zoneObjects.Contains(zoneObj))
		{
			zoneObjects.Add(zoneObj);
		}
	}

	public bool ReloadLevel(bool saveCurrentLevel)
	{
		if (!StatMaster.isHosting)
		{
			return false;
		}
		string levelName = null;
		if (saveCurrentLevel)
		{
			levelName = levelEditor.Settings.Name;
			lastLevelData = level.SaveLevel();
		}
		if (!string.IsNullOrEmpty(lastLevelData))
		{
			LoadLevel(lastLevelData, levelName);
		}
		return true;
	}

	public void SendSay(ChatMode mode, string text)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(text);
		byte[] array = new byte[1 + bytes.Length];
		array[0] = (byte)mode;
		Buffer.BlockCopy(bytes, 0, array, 1, bytes.Length);
		SendServerMessage(RPCMessageType.CmdSay, array);
	}

	public void SendConsolePrint(string message)
	{
		ConsoleController.ShowServerMessage(message);
		byte[] bytes = Encoding.UTF8.GetBytes(message);
		SendNetworkMessage(RPCMessageType.Print, bytes);
	}

	public void SendConsolePrint(ushort playerId, string message)
	{
		if (playerId == networkManager.PlayerID)
		{
			ConsoleController.ShowServerMessage(message);
		}
		else
		{
			SendFragmentedPlayerMessage(playerId, RPCMessageType.FragmentedPrint, Encoding.UTF8.GetBytes(message));
		}
	}

	public void SendJoinError(ushort playerId, byte error)
	{
		SendPlayerMessage(playerId, RPCMessageType.JoinError, new byte[1] { error });
	}

	public void SendModJoinError(ushort playerId)
	{
		byte[] modConfig = CompatibilityChecker.GetModConfig();
		SendFragmentedPlayerMessage(playerId, RPCMessageType.JoinErrorMod, modConfig);
	}

	private bool WaitForNetworkBufferRelease()
	{
		return networkManager.sentMessages >= maxMessageCount;
	}

	public void SendNetworkMessage(RPCMessageType message)
	{
		SendNetworkMessage(message, null);
	}

	public void SendNetworkMessage(RPCMessageType messageType, byte[] messageData)
	{
		SendNetworkMessageData(messageType, messageData, OrderedRPC.RPCDestination.Network, 0);
	}

	public void SendServerRequest(RPCMessageType messageType, byte[] messageData)
	{
		SendNetworkMessageData(messageType, messageData, OrderedRPC.RPCDestination.ServerRequest, 0);
	}

	public void SendServerMessage(RPCMessageType message)
	{
		SendServerMessage(message, null);
	}

	public void SendServerMessage(RPCMessageType messageType, byte[] messageData)
	{
		SendNetworkMessageData(messageType, messageData, OrderedRPC.RPCDestination.Server, 0);
	}

	public void SendFragmentedNetworkMessage(RPCMessageType messageType, byte[] messageData)
	{
		SendFragmentedMessage(messageType, messageData, OrderedRPC.RPCDestination.Network, 0);
	}

	public void SendFragmentedServerMessage(RPCMessageType messageType, byte[] messageData)
	{
		SendFragmentedMessage(messageType, messageData, OrderedRPC.RPCDestination.Server, 0);
	}

	public void SendFragmentedPlayerMessage(ushort playerId, RPCMessageType messageType, byte[] messageData)
	{
		SendFragmentedMessage(messageType, messageData, OrderedRPC.RPCDestination.Player, playerId);
	}

	private void SendFragmentedMessage(RPCMessageType messageType, byte[] messageData, OrderedRPC.RPCDestination destination, ushort playerId)
	{
		List<byte[]> bytesList = new List<byte[]>();
		Action<ushort, byte[]> sendFunc = delegate(ushort current, byte[] data)
		{
			bytesList.Add(GetFragmentedMessage(current, data));
		};
		FragmentedRPC.Send(sendFunc, messageData, ((destination != OrderedRPC.RPCDestination.Server) ? networkManager.PlayerMessageHeaderSize : networkManager.ServerMessageHeaderSize) + 5, FragmentMessageHeaderSize);
		for (int num = 0; num < bytesList.Count; num++)
		{
			SendNetworkMessageData(messageType, bytesList[num], destination, playerId);
		}
	}

	public void SendPlayerMessage(ushort playerId, RPCMessageType message)
	{
		SendPlayerMessage(playerId, message, null);
	}

	public void SendPlayerMessage(ushort playerId, RPCMessageType message, byte[] messageData)
	{
		if (StatMaster.isHosting)
		{
			StartCoroutine(IESendPlayerMessage(playerId, ownerId, message, messageData));
		}
	}

	private void SendNetworkMessageData(RPCMessageType messageType, byte[] messageData, OrderedRPC.RPCDestination destination, ushort playerId)
	{
		if (destination == OrderedRPC.RPCDestination.Player)
		{
			if (StatMaster.isHosting)
			{
				StartCoroutine(IESendPlayerMessage(playerId, ownerId, messageType, messageData));
			}
			return;
		}
		byte destByte = (byte)destination;
		int num = ((messageData != null) ? messageData.Length : 0);
		bool increaseID = true;
		bool flag = true;
		if (StatMaster.isHosting)
		{
			increaseID = destination == OrderedRPC.RPCDestination.Network;
			flag = destination != OrderedRPC.RPCDestination.Server && destination != OrderedRPC.RPCDestination.ServerRequest;
		}
		int serverMessageHeaderSize = networkManager.ServerMessageHeaderSize;
		byte[] array = new byte[serverMessageHeaderSize + OrderedRPC.RPCMessage.Size(num)];
		messageBuffer.Send(messageType, messageData, 0, num, increaseID, array, serverMessageHeaderSize);
		if (flag && WaitForNetworkBufferRelease())
		{
			StartCoroutine(WaitAndSendMessageData(destByte, array));
		}
		else
		{
			networkManager.SendServerMessage(destByte, array);
		}
	}

	private IEnumerator WaitAndSendMessageData(byte destByte, byte[] data)
	{
		while (WaitForNetworkBufferRelease())
		{
			yield return new WaitForSeconds(halfChokeTime);
		}
		networkManager.SendServerMessage(destByte, data);
	}

	private IEnumerator IESendPlayerMessage(ushort playerId, ushort senderId, RPCMessageType message, byte[] messageData)
	{
		int headerSize = networkManager.PlayerMessageHeaderSize;
		int dataSize = ((messageData != null) ? messageData.Length : 0);
		byte[] data = new byte[headerSize + OrderedRPC.RPCMessage.Size(dataSize)];
		ushort messageId = messageBuffer.GetSendID();
		messageBuffer.Send(message, messageData, 0, dataSize, true, data, headerSize);
		OrderedRPC.SetMessageID(messageId, skipData, headerSize);
		for (int i = 0; i < networkScene.clientList.Count; i++)
		{
			PlayerData player = networkScene.clientList[i];
			ushort pId = player.networkId;
			while (WaitForNetworkBufferRelease())
			{
				yield return new WaitForSeconds(halfChokeTime);
			}
			if (pId == playerId)
			{
				networkManager.SendPlayerMessage(pId, senderId, data);
			}
			else
			{
				networkManager.SendPlayerMessage(pId, senderId, skipData);
			}
		}
	}

	public void SendLevelSettings(byte[] settingsBytes)
	{
		if (StatMaster.isHosting)
		{
			SendFragmentedNetworkMessage(RPCMessageType.LevelSettings, settingsBytes);
			levelEditor.DecodeSettings(settingsBytes);
		}
		else
		{
			SendFragmentedServerMessage(RPCMessageType.LevelSettings, settingsBytes);
		}
	}

	public void SendClusterResults(ushort playerId, byte[] clusterResults)
	{
		SetLoadingText(LocalisationManager.GetTranslation(3387), 0.1f);
		StatMaster.WaitForServerResponse(StatMaster.ServerResponseType.ClusterResults, true);
		byte[] messageData = CLZF2.Compress(clusterResults);
		SendFragmentedServerMessage(RPCMessageType.ClusterResults, messageData);
	}

	public void SetLoadingText(string str, float delay = 0f)
	{
		if (!receivedGameState || !StatMaster.waitingForServerResponse)
		{
			hud.EnableConnectionWidget(delay);
			hud.SetLoadingText(str);
		}
	}

	public void SetOwner(ushort owner)
	{
		messageBuffer.SetNetworkID(owner);
		ClearAllBuffers();
		ownerId = owner;
		networkAddPiece.SetOwner(owner);
		levelEditor.SetOwner(owner);
		hud.SetOwner(owner);
		maxMessageCount = ((!StatMaster.isClient) ? 110 : 80);
	}

	public void SetSpawnZone(ushort playerId, byte[] message)
	{
		if (StatMaster.isClient)
		{
			playerId = NetworkCompression.ReadUInt16(message, 0);
		}
		PlayerData player;
		if (!Playerlist.GetPlayer(playerId, out player))
		{
			return;
		}
		if (player.isSpectator)
		{
			if (StatMaster.isHosting)
			{
				CancelResponse(playerId, StatMaster.ServerResponseType.SetSpawnZone);
			}
			return;
		}
		if (player.isLocalPlayer)
		{
			StatMaster.WaitForServerResponse(StatMaster.ServerResponseType.SetSpawnZone, false);
			HideLoadingText();
		}
		long num = BitConverter.ToInt64(message, (!StatMaster.isHosting) ? 2 : 0);
		LevelEntity entity;
		if (levelEditor.Get(num, out entity) && entity.isBuildZone)
		{
			(entity.behaviour as BuildZoneObject).SetBuildZone(player.buildZone, true);
			return;
		}
		Debug.LogError("Couldn't find spawn zone " + num + "!");
		if (StatMaster.isHosting)
		{
			CancelResponse(playerId, StatMaster.ServerResponseType.SetSpawnZone);
		}
	}

	public void StopAllSimulation()
	{
		ForceAllPlayMode(BesiegePlayMode.BuildMode);
	}

	public void ForceAllPlayMode(BesiegePlayMode playMode)
	{
		SendServerRequest(RPCMessageType.ChangePlayModeAll, new byte[1] { (byte)playMode });
	}

	public void SetPlayerReadyStateAll(bool ready)
	{
		bool flag = !StatMaster.levelSimulating || StatMaster.isLocalSim;
		if (!ready)
		{
			flag = !flag;
		}
		byte isReadyByte = (byte)(flag ? 1u : 0u);
		for (int i = 0; i < Playerlist.Players.Count; i++)
		{
			PlayerData playerData = Playerlist.Players[i];
			if (!playerData.isSpectator && playerData.voteState != flag)
			{
				SendPlayerReadyChanged(playerData.networkId, isReadyByte);
				HandlePlayerReadyChanged(playerData, flag);
			}
		}
	}

	public void ToggleSpectator(ushort playerId, byte[] spectatorData)
	{
		if (StatMaster.isClient)
		{
			playerId = NetworkCompression.ReadUInt16(spectatorData, 0);
			byte[] array = new byte[spectatorData.Length - 2];
			Buffer.BlockCopy(spectatorData, 2, array, 0, array.Length);
			spectatorData = array;
		}
		PlayerData player;
		if (!Playerlist.GetPlayer(playerId, out player))
		{
			Debug.LogError("Couldn't find player " + playerId + "!");
			return;
		}
		bool flag = spectatorData[0] == 1;
		byte[] array2 = null;
		Transform transform = null;
		bool flag2 = false;
		BuildZoneObject zoneObj = null;
		int num = 0;
		if (StatMaster.isHosting)
		{
			if (!player.isLocalPlayer && player.wantSpectator != flag)
			{
				player.wantSpectator = flag;
			}
			if (!flag && !CanCreateClient())
			{
				flag = true;
				SendPlayerMessage(player.networkId, RPCMessageType.PlayerLimitSpectator);
			}
			bool flag3 = flag != player.isSpectator;
			array2 = new byte[3 + ((!flag && flag3) ? (28 + LevelEntity.ID_LENGTH) : 0)];
			NetworkCompression.WriteUInt16(playerId, array2, num);
			num += 2;
			if (!flag3)
			{
				array2[num] = (byte)(flag ? 1u : 0u);
				SendNetworkMessage(RPCMessageType.ToggleSpectator, array2);
				return;
			}
			array2[num] = spectatorData[0];
			num++;
		}
		if (player.isLocalPlayer)
		{
			StatMaster.WaitForServerResponse(StatMaster.ServerResponseType.SpectatorToggle, false);
			HideLoadingText();
		}
		if (flag == player.isSpectator)
		{
			if (player.isLocalPlayer)
			{
				StatMaster.SetSimulationState((!flag) ? SimulationState.BuildMode : SimulationState.SpectatorMode);
			}
			return;
		}
		if (flag)
		{
			PlayerBuildZone buildZone = player.buildZone;
			ServerMachine machine = player.machine;
			zoneObj = buildZone.spawnZone;
			if (machine.isLocalMachine)
			{
				networkAddPiece.StopLocalMachine();
				hud.prevBuild = machine.CreateMachineInfo();
				player.prevMachine = true;
			}
			else if (machine.isSimulating)
			{
				machine.EndSimulation();
			}
			RemoveMachine(player, false);
			player.voteState = false;
			player.buildZone = null;
			player.useCustomPos = false;
		}
		else
		{
			LevelEntity entity = null;
			ServerMachine machine;
			PlayerBuildZone buildZone;
			if (StatMaster.isHosting)
			{
				if (spectatorData[1] == 1)
				{
					long id = BitConverter.ToInt64(spectatorData, 2);
					if (levelEditor.Get(id, out entity))
					{
						zoneObj = entity.behaviour as BuildZoneObject;
						flag2 = true;
					}
				}
				else if (GetNextZone(out zoneObj))
				{
					entity = zoneObj.entity;
					flag2 = true;
				}
				Vector3 vector = default(Vector3);
				Quaternion quaternion = default(Quaternion);
				if (!flag2)
				{
					vector = GetZonePosition();
					quaternion = Quaternion.identity;
					levelEditor.AddEntity(LevelEditor.BUILD_ZONE_ID, vector, quaternion, Vector3.one, false);
					if (!GetNextZone(out zoneObj))
					{
						Debug.LogError("ERROR: Couldn't create spawn zone for player!");
						return;
					}
					entity = zoneObj.entity;
					flag2 = true;
				}
				machine = CreateClient(player, entity.Position, entity.Rotation, true);
				buildZone = player.buildZone;
				transform = buildZone.transform;
				NetworkCompression.PackVector(transform.position, array2, num);
				num += 12;
				NetworkCompression.PackQuaternion(transform.rotation, array2, num);
				num += 16;
				Buffer.BlockCopy(BitConverter.GetBytes((!flag2) ? LevelPrefab.INVALID_ID : entity.identifier), 0, array2, num, LevelEntity.ID_LENGTH);
			}
			else
			{
				num++;
				Vector3 vec = default(Vector3);
				NetworkCompression.UnpackVector(spectatorData, num, out vec);
				num += 12;
				Quaternion quat = default(Quaternion);
				NetworkCompression.UnpackQuaternion(spectatorData, num, out quat);
				num += 16;
				long id = BitConverter.ToInt64(spectatorData, num);
				if (id != LevelPrefab.INVALID_ID && levelEditor.Get(id, out entity))
				{
					zoneObj = entity.behaviour as BuildZoneObject;
					flag2 = true;
				}
				machine = CreateClient(player, vec, quat, true);
				buildZone = player.buildZone;
				transform = buildZone.transform;
			}
			player.machine = machine;
			player.buildZone = buildZone;
			if (flag2)
			{
				zoneObj.SetBuildZone(buildZone, false);
			}
		}
		if (StatMaster.isHosting)
		{
			SendNetworkMessage(RPCMessageType.ToggleSpectator, array2);
		}
		hud.UpdatePlayers();
		if (player.isLocalPlayer && !StatMaster.waitingForServerResponse)
		{
			networkAddPiece.UpdateBarController();
			hud.OnToggleSpectator();
		}
	}

	public void UnregisterSpawn(BuildZoneObject zoneObj)
	{
		if (zoneObjects.Contains(zoneObj))
		{
			zoneObjects.Remove(zoneObj);
		}
	}

	public void UpdateBuildZoneTransform(Vector3 pos, Quaternion rot)
	{
		hud.SetBuildzoneTransform(pos, rot);
	}

	private void ApplyMachineSkin(ushort playerId, byte[] data)
	{
		byte[] data2;
		PlayerData player;
		if (!HandleFragmentedMessage(paintBuffer, playerId, data, out data2) || !Playerlist.GetPlayer(playerId, out player) || player.isSpectator)
		{
			return;
		}
		byte[] array = CLZF2.Decompress(data2);
		ServerMachine machine = player.machine;
		int num = 0;
		int count;
		num += NetworkCompression.UnpackUInt(array, num, false, out count);
		List<BlockBehaviour> list = new List<BlockBehaviour>();
		for (int i = 0; i < count; i++)
		{
			int count2;
			num += NetworkCompression.UnpackUInt(array, num, false, out count2);
			BlockBehaviour block;
			if (machine.GetBlockFromIndex(count2, out block))
			{
				list.Add(block);
			}
		}
		BlockSkinLoader.SkinPack.Skin skin;
		BlockSkinLoader.SkinPack.Skin.Decode(array, num, out skin);
		BlockSkinLoader.SetBlocksToPack(skin.pack, machine, list);
	}

	protected void OnDestroy()
	{
		if (Instance == this)
		{
			hasInstance = false;
		}
	}

	protected void Awake()
	{
		Instance = this;
		hasInstance = true;
		logicDataBuffer = new FragmentedRPC();
		loadLevelBuffer = new FragmentedRPC();
		loadMachineBuffer = new FragmentedRPC();
		modJoinErrorBuffer = new FragmentedRPC();
		gameStateBuffer = new FragmentedRPC();
		simFrameBuffer = new FragmentedRPC();
		clusterBuffer = new FragmentedRPC();
		machineDataBuffer = new FragmentedRPC();
		blockTransformCacheBuffer = new FragmentedRPC();
		blockDataBuffer = new FragmentedRPC();
		resetBlockBuffer = new FragmentedRPC();
		pasteBlockBuffer = new FragmentedRPC();
		editKeyBuffer = new FragmentedRPC();
		rebindKeyBuffer = new FragmentedRPC();
		entityRemoveBuffer = new FragmentedRPC();
		entityAddBuffer = new FragmentedRPC();
		entityUpdateBuffer = new FragmentedRPC();
		printRPCBuffer = new FragmentedRPC();
		paintBuffer = new FragmentedRPC();
		messageBuffer = new OrderedRPC(MessageReceived);
		orderedQueue = base.gameObject.AddComponent<OrderedRPCQueue>();
		orderedQueue.SetExecuteMethod(ExecuteMessage);
		clientList = new List<ushort>(20);
		playerZoneDirections = new List<Vector2>();
		playerZoneDirections.Add(new Vector2(18f, 0f));
		playerZoneDirections.Add(new Vector2(-18f, 0f));
		playerZoneDirections.Add(new Vector2(0f, -18f));
		playerZoneDirections.Add(new Vector2(0f, 18f));
		playerZoneDirections.Add(new Vector2(18f, 18f));
		playerZoneDirections.Add(new Vector2(-18f, 18f));
		playerZoneDirections.Add(new Vector2(18f, -18f));
		playerZoneDirections.Add(new Vector2(-18f, -18f));
	}

	private bool CanCreateClient()
	{
		bool flag = OptionsMaster.allowExcessPlayers || HasNextZone();
		bool flag2 = OptionsMaster.votingEnabled && StatMaster.SimulationState == SimulationState.GlobalSimulation;
		return flag && !flag2 && !PlayersLimited;
	}

	private int ChangeBlockSkin(byte[] data, int offset)
	{
		int num = offset;
		ushort playerId = NetworkCompression.ReadUInt16(data, offset);
		offset += 2;
		int count;
		offset += NetworkCompression.UnpackUInt(data, offset, false, out count);
		int count2;
		offset += NetworkCompression.UnpackUInt(data, offset, true, out count2);
		byte[] array = new byte[count2];
		Buffer.BlockCopy(data, offset, array, 0, count2);
		offset += count2;
		ServerMachine machine;
		if (networkScene.GetMachine(playerId, out machine))
		{
			BlockSkinLoader.SkinPack.Skin skin;
			BlockSkinLoader.SkinPack.Skin.Decode(array, 0, out skin);
			BlockBehaviour block;
			if (machine.GetBlockFromIndex(count, out block))
			{
				block.VisualController.ReplaceSkin(skin);
			}
		}
		return offset - num;
	}

	private byte[] OnChangePlayMode(ServerRequestData requestData)
	{
		if (requestData.player.isLocalPlayer)
		{
			StatMaster.WaitForServerResponse(StatMaster.ServerResponseType.ChangePlayMode, false);
			HideLoadingText();
			if (!requestData.acceptedRequest)
			{
				if (requestData.hasMachine)
				{
					if (!requestData.machine.isSimulating)
					{
						StatMaster.SetSimulationState(SimulationState.BuildMode);
					}
					else
					{
						StatMaster.SetSimulationState(SimulationState.GlobalSimulation);
					}
				}
				return null;
			}
		}
		if (!requestData.hasMachine)
		{
			return null;
		}
		BesiegePlayMode besiegePlayMode = (BesiegePlayMode)requestData.message.data[0];
		if (!requestData.player.isLocalPlayer && (requestData.player.PlayMode == besiegePlayMode || (StatMaster.isHosting && !StatMaster.Mode.LevelEditor.clientSimControl && besiegePlayMode == BesiegePlayMode.GlobalSimulation)))
		{
			return null;
		}
		StartCoroutine(ToggleMachinePlayerMode(requestData.player, besiegePlayMode));
		return requestData.message.data;
	}

	private byte[] OnChangePlayModeAll(ServerRequestData requestData)
	{
		BesiegePlayMode playMode = (BesiegePlayMode)requestData.message.data[0];
		StartCoroutine(ToggleMachinePlayerMode(Playerlist.Players, playMode));
		return requestData.message.data;
	}

	private byte[] OnPickMachine(ServerRequestData requestData)
	{
		if (requestData.player.isLocalPlayer)
		{
			StatMaster.WaitForServerResponse(StatMaster.ServerResponseType.MachineLoad, false);
			HideLoadingText();
			if (!requestData.acceptedRequest)
			{
				return null;
			}
		}
		LevelSettings settings = levelEditor.Settings;
		int count;
		NetworkCompression.UnpackUInt(requestData.message.data, 0, true, out count);
		if (StatMaster.isHosting && (!StatMaster.limitMachines || requestData.machine.isSimulating || count < 0 || count >= settings.AllowedMachines.Count))
		{
			Debug.LogWarning("Can't pick machine " + count + " for player " + requestData.player.networkId + "! Info: limitMachines=" + StatMaster.limitMachines + " machine.isSimulating=" + requestData.machine.isSimulating + " pickedIndex=" + count + " allowedMachinesCount=" + settings.AllowedMachines.Count + ")");
			return null;
		}
		byte[] machineData = settings.AllowedMachines[count].GetMachineData();
		requestData.machine.player.allowedMachineIndex = count;
		requestData.machine.Decode(false, false, machineData, 0);
		requestData.machine.ToggleModification(!StatMaster.LimitMachineModification);
		return requestData.message.data;
	}

	private byte[] OnToggleSimControl(ServerRequestData requestData)
	{
		bool flag = (StatMaster.Mode.LevelEditor.clientSimControl = requestData.message.data[0] == 1);
		LevelEditorUI.Options options = SingleInstanceFindOnly<LevelEditorUI>.Instance.options;
		options.UpdateLocalSimButton();
		if (StatMaster.isClient && !flag && !requestData.player.inLocalSim)
		{
			options.ToggleLocalSim();
		}
		return requestData.message.data;
	}

	private byte[] OnToggleLevelEditor(ServerRequestData requestData)
	{
		byte b = requestData.message.data[0];
		bool toggle = (b & 1) != 0;
		ServerSettings serverSettings = NetworkScene.ServerSettings;
		serverSettings.levelEditor = toggle;
		if (StatMaster.isClient)
		{
			levelEditor.ToggleEditor(toggle, false);
		}
		return requestData.message.data;
	}

	private void OnToggleLevelEditorFinished(ServerRequestData requestData)
	{
		byte b = requestData.message.data[0];
		bool flag = (b & 1) != 0;
		ServerSettings serverSettings = NetworkScene.ServerSettings;
		if (serverSettings.playListIndex != -1 && serverSettings.playListIndex >= serverSettings.playList.Count)
		{
			serverSettings.playListIndex = ((serverSettings.playList.Count <= 0) ? (-1) : 0);
		}
		bool flag2 = flag || serverSettings.playList.Count == 0;
		levelEditor.ToggleEditor(flag, flag2);
		if (!flag2)
		{
			levelEditor.LoadPlaylistLevel(serverSettings.playListIndex);
		}
	}

	private byte[] OnToggleLocalSim(ServerRequestData requestData)
	{
		PlayerData player = requestData.player;
		if (StatMaster.isClient && player.isLocalPlayer)
		{
			StatMaster.WaitForServerResponse(StatMaster.ServerResponseType.ToggleLocalSim, false);
			HideLoadingText();
			if (!requestData.acceptedRequest)
			{
				return null;
			}
		}
		bool flag = requestData.message.data[0] == 1;
		if (StatMaster.isHosting && ((!StatMaster.Mode.LevelEditor.clientSimControl && !flag) || (!StatMaster.Mode.levelEdit && flag) || flag == player.inLocalSim))
		{
			return null;
		}
		player.inLocalSim = flag;
		if (player.isLocalPlayer)
		{
			NetworkAddPiece instance = NetworkAddPiece.Instance;
			StatMaster.Mode.LevelEditor.clientGlobalSim = !player.inLocalSim;
			if (!player.isSpectator && player.PlayMode != BesiegePlayMode.BuildMode)
			{
				instance.TogglePlayMode(BesiegePlayMode.BuildMode);
			}
			SingleInstanceFindOnly<LevelEditorUI>.Instance.options.UpdateLocalSimButton();
			ClearSimFrameBuffer();
			if (player.inLocalSim)
			{
				if (StatMaster.levelSimulating && !StatMaster.isLocalSim)
				{
					instance.ToggleLevelSimulation(false, true);
				}
				requestedSimFrame = false;
			}
			else
			{
				instance.SetTimeScale(instance.lastTimeScale, false);
				if (level.remoteSim)
				{
					requestedSimFrame = true;
					SendServerMessage(RPCMessageType.LevelSimFrame);
				}
			}
			for (int i = 0; i < Playerlist.Players.Count; i++)
			{
				PlayerData playerData = Playerlist.Players[i];
				if (!playerData.isSpectator && !playerData.machine.isLocalMachine && playerData.machine.isSimulating)
				{
					playerData.machine.ToggleGhost(player.inLocalSim || playerData.machine.isLocalSim);
				}
			}
		}
		return requestData.message.data;
	}

	public bool HandleServerRequest(ushort playerId, OrderedRPC.RPCMessage message)
	{
		bool flag = true;
		Func<ServerRequestData, byte[]> func = null;
		Action<ServerRequestData> action = null;
		switch (message.type)
		{
		case RPCMessageType.PickMachine:
			func = OnPickMachine;
			break;
		case RPCMessageType.ChangePlayMode:
			func = OnChangePlayMode;
			break;
		case RPCMessageType.ChangePlayModeAll:
			func = OnChangePlayModeAll;
			flag = false;
			break;
		case RPCMessageType.ToggleLocalSim:
			func = OnToggleLocalSim;
			flag = false;
			break;
		case RPCMessageType.ToggleSimControl:
			func = OnToggleSimControl;
			flag = false;
			break;
		case RPCMessageType.ToggleLevelEditor:
			func = OnToggleLevelEditor;
			action = OnToggleLevelEditorFinished;
			flag = false;
			break;
		default:
			return false;
		}
		byte[] data = message.data;
		ServerMachine machine = null;
		bool hasMachine = flag && networkScene.GetMachine(playerId, out machine);
		bool acceptedRequest = StatMaster.isHosting || message.data.Length > 0;
		ServerRequestData serverRequestData = null;
		PlayerData player;
		if (Playerlist.GetPlayer(playerId, out player))
		{
			ServerRequestData serverRequestData2 = new ServerRequestData();
			serverRequestData2.player = player;
			serverRequestData2.message = message;
			serverRequestData2.hasMachine = hasMachine;
			serverRequestData2.machine = machine;
			serverRequestData2.acceptedRequest = acceptedRequest;
			serverRequestData = serverRequestData2;
			data = func(serverRequestData);
		}
		else
		{
			data = null;
		}
		if (StatMaster.isHosting)
		{
			if (data != null)
			{
				message.data = data;
				StartCoroutine(IESendToClients(playerId, message, false));
			}
			else
			{
				StartCoroutine(IESendPlayerMessage(playerId, playerId, message.type, new byte[0]));
			}
		}
		if (StatMaster.isHosting && data != null && action != null)
		{
			action(serverRequestData);
		}
		return true;
	}

	public void CancelResponse(ushort playerId, StatMaster.ServerResponseType responseType)
	{
		if (playerId == ownerId)
		{
			StatMaster.WaitForServerResponse(responseType, false);
			HideLoadingText();
		}
		else
		{
			SendPlayerMessage(playerId, RPCMessageType.CancelResponse, new byte[1] { (byte)responseType });
		}
	}

	private void OnCancelResponse(ushort senderPlayerId, OrderedRPC.RPCMessage message)
	{
		if (message.data.Length != 0)
		{
			StatMaster.ServerResponseType responseType = (StatMaster.ServerResponseType)message.data[0];
			StatMaster.WaitForServerResponse(responseType, false);
			HideLoadingText();
		}
	}

	private void ExecuteMessage(ushort senderPlayerId, OrderedRPC.RPCMessage message)
	{
		RPCMessageType type = message.type;
		if ((StatMaster.isClient && !receivedGameState && !IsInitMessage(type)) || type == RPCMessageType.Skip || HandleServerRequest(senderPlayerId, message))
		{
			return;
		}
		switch (type)
		{
		case RPCMessageType.JoinError:
			if (message.data.Length > 0)
			{
				networkManager.OnJoinFailed(message.data[0]);
				return;
			}
			Debug.LogError("Couldn't display proper join error, no data supplied!");
			networkManager.OnJoinFailed(0);
			return;
		case RPCMessageType.DlcJoinError:
			networkManager.OnDlcJoinFailed(NetworkCompression.ReadUInt(false, message.data, 0));
			return;
		case RPCMessageType.JoinErrorMod:
			OnJoinFailedMod(senderPlayerId, message.data);
			return;
		case RPCMessageType.UpdatePlayerSelection:
			UpdatePlayerSelection(senderPlayerId, message.data, false);
			return;
		case RPCMessageType.ResetPlayerSelection:
			UpdatePlayerSelection(senderPlayerId, message.data, true);
			return;
		case RPCMessageType.ToggleGhost:
			levelEditor.ToggleGhost(senderPlayerId, message.data);
			return;
		case RPCMessageType.GameWin:
			levelEditor.OnWinEvent(message.data);
			return;
		case RPCMessageType.CancelResponse:
			OnCancelResponse(senderPlayerId, message);
			return;
		case RPCMessageType.LoadMachine:
			OnLoadMachineData(senderPlayerId, message);
			return;
		case RPCMessageType.AddEntities:
			OnAddEntities(senderPlayerId, message);
			return;
		case RPCMessageType.RemoveEntities:
			OnRemoveEntities(senderPlayerId, message);
			return;
		case RPCMessageType.UpdateEntities:
			OnUpdateEntities(senderPlayerId, message);
			return;
		case RPCMessageType.UpdateBlockState:
			UpdateBlockState(senderPlayerId, message.data);
			return;
		case RPCMessageType.UpdateEntityState:
			UpdateEntityState(senderPlayerId, message.data);
			return;
		case RPCMessageType.MapperResetBlock:
			OnResetBlock(senderPlayerId, message);
			return;
		case RPCMessageType.MapperResetEntity:
			BlockMapper.ResetEntity(senderPlayerId, message.data);
			return;
		case RPCMessageType.ApplyMachineSkin:
			ApplyMachineSkin(senderPlayerId, message.data);
			return;
		case RPCMessageType.EditBlockSkin:
			ChangeBlockSkin(message.data, 0);
			return;
		case RPCMessageType.MapperRebindKeys:
			OnMapperRebindKeys(senderPlayerId, message);
			return;
		case RPCMessageType.MapperRebindGroup:
			OnMapperEditKey(senderPlayerId, message);
			return;
		case RPCMessageType.EditMachineData:
			OnEditMachineData(senderPlayerId, message);
			return;
		case RPCMessageType.MapperPasteBlock:
			OnPasteBlock(senderPlayerId, message);
			return;
		case RPCMessageType.MapperPasteEntity:
			BlockMapper.PasteEntity(senderPlayerId, message.data);
			return;
		case RPCMessageType.EditBlock:
			OnUpdateBlockData(senderPlayerId, message);
			return;
		case RPCMessageType.EditEntity:
			BlockMapper.UpdateEntityData(senderPlayerId, message.data, 0);
			return;
		case RPCMessageType.EditLogic:
			levelEditor.editLogicHandler.OnLogicChange(senderPlayerId, message.data);
			return;
		case RPCMessageType.SetSpawnZone:
			SetSpawnZone(senderPlayerId, message.data);
			return;
		case RPCMessageType.PlayerLimitSpectator:
			SingleInstanceFindOnly<GenericUIPopup>.Instance.Show(LocalisationManager.GetTranslation(3342), 3f);
			return;
		case RPCMessageType.LevelSimFrame:
			OnLevelSimFrame(senderPlayerId, message);
			return;
		case RPCMessageType.LevelSettings:
			OnLevelSettings(senderPlayerId, message);
			return;
		case RPCMessageType.LoadLevel:
			OnLoadLevelData(senderPlayerId, message);
			return;
		case RPCMessageType.ClearLevel:
			levelEditor.OnClearLevel(true);
			return;
		case RPCMessageType.SimulateLevel:
			if (message.data.Length > 0)
			{
				if (StatMaster.isClient)
				{
					bool flag = message.data[0] == 1;
					bool flag2 = StatMaster.Mode.LevelEditor.clientGlobalSim && !StatMaster.isLocalSim;
					if (!flag && flag2)
					{
						networkAddPiece.SimStateChange(false, true);
					}
					if (flag2 && StatMaster.levelSimulating != flag)
					{
						networkAddPiece.ToggleLevelSimulation(flag);
					}
					if (!flag)
					{
						level.IncrementSession();
					}
					else if (flag2)
					{
						networkAddPiece.SimStateChange(true, true);
					}
					level.remoteSim = flag;
				}
			}
			else
			{
				Debug.LogError("Couldn't simulate level, no data supplied!");
			}
			return;
		case RPCMessageType.ServerSettings:
			if (StatMaster.isClient)
			{
				int offset = 0;
				networkScene.UpdateSettings(ServerSettings.Decode(message.data, ref offset));
			}
			return;
		case RPCMessageType.ServerPassword:
			if (StatMaster.isHosting)
			{
				byte[] array2 = new byte[1];
				string text2 = Encoding.UTF8.GetString(message.data);
				bool flag4 = NetworkScene.ServerSettings.password.Equals(text2);
				array2[0] = (byte)(flag4 ? 1u : 0u);
				PlayerData player;
				if (Playerlist.GetPlayer(senderPlayerId, out player) && flag4)
				{
					player.passCorrect = true;
				}
				if (BesiegeLogFilter.logDebug)
				{
					Debug.Log("Password: '" + text2 + "' > " + flag4);
				}
				SendPlayerMessage(senderPlayerId, RPCMessageType.ServerPassword, array2);
			}
			else if (message.data.Length > 0)
			{
				bool flag5 = message.data[0] == 1;
				hud.ReceivedPassword(flag5);
				if (flag5)
				{
					SendServerMessage(RPCMessageType.PlayerConfig, GetPlayerConfig());
				}
			}
			else
			{
				Debug.LogError("Couldn't verify password, no data supplied!");
			}
			return;
		case RPCMessageType.Init:
			if (message.data.Length > 0)
			{
				if (message.data[0] == 1)
				{
					networkManager.SetServerFull();
				}
				else if (message.data.Length == 12)
				{
					bool flag3 = message.data[1] == 1;
					ushort id = NetworkCompression.ReadUInt16(message.data, 2);
					ulong lobbyId = BitConverter.ToUInt64(message.data, 4);
					networkManager.SetPlayerID(id, lobbyId);
					messageBuffer.SetReceiveID(0, message.ID);
					if (flag3)
					{
						if (BesiegeLogFilter.logDebug)
						{
							Debug.Log("Showing password dialog..");
						}
						hud.ShowPasswordDialog();
					}
					else
					{
						SendServerMessage(RPCMessageType.PlayerConfig, GetPlayerConfig());
					}
				}
				else
				{
					Debug.LogError("Couldn't join server, received " + message.data.Length + " bytes (expected 12)!");
					networkManager.OnJoinFailed(0);
				}
			}
			else
			{
				Debug.LogError("Couldn't initialize player, no data supplied!");
			}
			return;
		case RPCMessageType.PlayerConfig:
		{
			if (!StatMaster.isHosting)
			{
				return;
			}
			PlayerData player2;
			if (Playerlist.GetPlayer(senderPlayerId, out player2) && networkScene.clientIDList.Contains(senderPlayerId) && player2.passCorrect)
			{
				InitServerPlayer(player2, message.data);
				return;
			}
			byte b3 = 0;
			b3 = (byte)((!Playerlist.Contains(senderPlayerId)) ? 1 : ((!networkScene.clientIDList.Contains(senderPlayerId)) ? 2 : (player2.passCorrect ? 4 : 3)));
			if (BesiegeLogFilter.logError)
			{
				Debug.LogError("Player " + senderPlayerId + " isn't setup correctly, sending connect error " + b3);
			}
			SendJoinError(senderPlayerId, b3);
			return;
		}
		case RPCMessageType.PlayerJoin:
			if (StatMaster.isClient)
			{
				PlayerJoin(message.data);
				SingleInstanceFindOnly<ModManager>.Instance.OnJoin();
			}
			return;
		case RPCMessageType.PlayerLeave:
			if (StatMaster.isClient)
			{
				senderPlayerId = NetworkCompression.ReadUInt16(message.data, 0);
				PlayerDisconnected(senderPlayerId);
			}
			return;
		case RPCMessageType.AutoTimeScale:
		case RPCMessageType.TimeScale:
			if (StatMaster.isClient)
			{
				networkAddPiece.SetTimeScale(message.data, type == RPCMessageType.AutoTimeScale);
			}
			return;
		case RPCMessageType.RefreshPlayerStates:
			if (StatMaster.isClient)
			{
				networkAddPiece.RefreshPlayerViewer();
			}
			return;
		case RPCMessageType.ReceiveLevelState:
			if (StatMaster.isClient)
			{
				OnReceiveGameState(senderPlayerId, message);
			}
			return;
		case RPCMessageType.ClusterResults:
			OnClusterResults(senderPlayerId, message);
			return;
		case RPCMessageType.Clone:
			if (StatMaster.Mode.allowClone)
			{
				ushort num;
				ushort num2;
				if (StatMaster.isHosting)
				{
					num = senderPlayerId;
					num2 = NetworkCompression.ReadUInt16(message.data, 0);
					byte[] array = new byte[4];
					NetworkCompression.WriteUInt16(num, array, 0);
					NetworkCompression.WriteUInt16(num2, array, 2);
					SendNetworkMessage(RPCMessageType.Clone, array);
				}
				else
				{
					num = NetworkCompression.ReadUInt16(message.data, 0);
					num2 = NetworkCompression.ReadUInt16(message.data, 2);
				}
				ServerMachine machine2;
				ServerMachine machine3;
				if (networkScene.GetMachine(num, out machine2) && networkScene.GetMachine(num2, out machine3))
				{
					machine2.Clone(machine3);
				}
			}
			return;
		case RPCMessageType.ToggleSpectator:
			ToggleSpectator(senderPlayerId, message.data);
			return;
		case RPCMessageType.IncrementSession:
			if (message.data.Length == 4)
			{
				ServerMachine machine;
				if (networkScene.GetMachine(NetworkCompression.ReadUInt16(message.data, 0), out machine))
				{
					machine.IncrementSession(false, message.data[2] == 1, message.data[3]);
				}
			}
			else
			{
				Debug.LogError("Couldn't increment session, received " + message.data.Length + " bytes (4 expected)!");
			}
			return;
		case RPCMessageType.RequestPlayerPings:
			networkManager.RequestPings(senderPlayerId);
			return;
		case RPCMessageType.RconCommand:
		{
			byte b = message.data[0];
			string password = Encoding.UTF8.GetString(message.data, 1, b);
			byte b2 = message.data[b + 1];
			string command = Encoding.UTF8.GetString(message.data, b + 2, b2);
			string text = string.Empty;
			if (message.data.Length > b + b2 + 2)
			{
				text = Encoding.UTF8.GetString(message.data, b + b2 + 2, message.data.Length - b - b2 - 2);
			}
			string[] args = ((!string.IsNullOrEmpty(text)) ? text.Split('\n') : new string[0]);
			ReferenceMaster.ConsoleController.HandleRconCommand(senderPlayerId, password, command, args);
			return;
		}
		case RPCMessageType.Print:
			HandlePrintMessage(message.data);
			return;
		case RPCMessageType.Disconnect:
			HandleDisconnectMessage(message);
			return;
		case RPCMessageType.CmdSay:
			if (StatMaster.isHosting)
			{
				HandleSayCommand(message);
			}
			return;
		case RPCMessageType.Say:
			if (StatMaster.isClient)
			{
				HandleSayMessage(message.data);
			}
			return;
		case RPCMessageType.FragmentedPrint:
			HandleFragmentedPrintMessage(message);
			return;
		case RPCMessageType.CmdPlayerReady:
			if (StatMaster.isHosting)
			{
				HandlePlayerReadyCommand(message);
			}
			return;
		case RPCMessageType.PlayerReadyChanged:
			if (StatMaster.isClient)
			{
				HandlePlayerReadyChangedMessage(message);
			}
			return;
		}
		PlayerData player3;
		if (!Playerlist.GetPlayer(senderPlayerId, out player3) || player3.isSpectator)
		{
			return;
		}
		ServerMachine machine4 = player3.machine;
		PlayerBuildZone buildZone = machine4.player.buildZone;
		switch (type)
		{
		case RPCMessageType.ToggleBounds:
			if (message.data.Length > 0)
			{
				buildZone.boundingBoxController.RemoteToggleBounds(message.data[0] == 1);
			}
			else
			{
				Debug.LogError("Can't toggle bounds, no data supplied!");
			}
			return;
		case RPCMessageType.UpdateGodMode:
			if (message.data.Length > 0)
			{
				machine4.UpdateGodMode(message.data[0]);
			}
			else
			{
				Debug.LogError("Couldn't update God mode, no data supplied!");
			}
			return;
		case RPCMessageType.Translate:
			machine4.SetRigidInterpolation(RigidbodyInterpolation.None);
			NetworkCompression.UnpackVector(message.data, 0, out posHolder);
			machine4.SetPosition(posHolder);
			machine4.RestoreRigidInterpolation();
			return;
		case RPCMessageType.Rotate:
			machine4.SetRigidInterpolation(RigidbodyInterpolation.None);
			NetworkCompression.UnpackQuaternion(message.data, 0, out rotHolder);
			machine4.SetRotation(rotHolder);
			machine4.RestoreRigidInterpolation();
			return;
		}
		if (!StatMaster.LimitMachineModification)
		{
			switch (type)
			{
			case RPCMessageType.TransformCache:
				OnBlockTransformCache(machine4, message.data);
				break;
			case RPCMessageType.AddBlock:
			{
				BlockBehaviour block;
				machine4.RemoteAddBlock(message.data, 0, out block);
				break;
			}
			case RPCMessageType.RemoveBlock:
				machine4.RemoteRemoveBlock(message.data, 0);
				break;
			case RPCMessageType.MoveBlock:
				machine4.RemoteMoveBlock(message.data, 0);
				break;
			case RPCMessageType.RotateBlock:
				machine4.RemoteRotateBlock(message.data, 0);
				break;
			case RPCMessageType.ScaleBlock:
				machine4.RemoteScaleBlock(message.data, 0);
				break;
			case RPCMessageType.ShortenBlock:
				machine4.RemoteShortenBlock(message.data, 0);
				break;
			case RPCMessageType.MirrorDragged:
				machine4.RemoteMirrorDragged(message.data, 0);
				break;
			case RPCMessageType.ReverseBlock:
				machine4.RemoteReverse(message.data, 0);
				break;
			case RPCMessageType.EditBlockData:
				machine4.RemoteEditBlockData(message.data, 0);
				break;
			case RPCMessageType.RefreshBlocks:
				machine4.RemoteRefreshBlocks(message.data, 0);
				break;
			case RPCMessageType.Reset:
				machine4.RemoteReset();
				break;
			}
		}
	}

	public void SendToggleLevelEditor(bool toggle)
	{
		SendServerRequest(RPCMessageType.ToggleLevelEditor, new byte[1] { (byte)(toggle ? 1u : 0u) });
	}

	private void HandlePlayerReadyChangedMessage(OrderedRPC.RPCMessage message)
	{
		ushort networkId = NetworkCompression.ReadUInt16(message.data, 0);
		byte b = message.data[2];
		bool voteState = b == 1;
		PlayerData player = Playerlist.GetPlayer(networkId);
		if (player == null)
		{
			Debug.LogError("Player is null, this should not happen");
		}
		else
		{
			HandlePlayerReadyChanged(player, voteState);
		}
	}

	private void HandlePlayerReadyChanged(PlayerData player, bool voteState)
	{
		player.voteState = voteState;
		ConsoleController.ShowMessage("Player " + player.networkId + " is now: " + ((!voteState) ? "not ready" : "ready"));
		if (player.isLocalPlayer)
		{
			if (StatMaster.SimulationState == SimulationState.PendingReadyVote)
			{
				StatMaster.SetSimulationState(StatMaster.PreviousSimulationState);
			}
			if (StatMaster.SimulationState == SimulationState.BuildMode && voteState)
			{
				hud.DisableMachineTools();
				if (StatMaster.advancedBuilding)
				{
					AdvancedBlockEditor instance = AdvancedBlockEditor.Instance;
					if (instance != null)
					{
						BlockSelectionTool selectionController = instance.selectionController;
						if (selectionController != null && selectionController.Count > 0)
						{
							selectionController.DeselectAll(true);
						}
					}
				}
			}
			StatMaster.WaitForServerResponse(StatMaster.ServerResponseType.RequestVote, false);
			HideLoadingText();
			if (!player.isSpectator && !StatMaster.LimitMachineModification)
			{
				player.machine.ToggleModification(!player.voteState);
			}
			networkAddPiece.UpdatePlayIcon();
		}
		hud.UpdatePlayers();
		if (OptionsMaster.votingEnabled)
		{
			networkAddPiece.UpdateVoting();
		}
	}

	private void HandlePlayerReadyCommand(OrderedRPC.RPCMessage message)
	{
		byte b = message.data[0];
		bool flag = b == 1;
		ushort senderPlayerID = message.senderPlayerID;
		PlayerData player;
		if (Playerlist.GetPlayer(senderPlayerID, out player) && !player.isSpectator && OptionsMaster.votingEnabled && player.voteState != flag)
		{
			SendPlayerReadyChanged(message.senderPlayerID, b);
			HandlePlayerReadyChanged(player, flag);
		}
	}

	private void SendPlayerReadyChanged(ushort playerId, byte isReadyByte)
	{
		byte[] array = new byte[3];
		NetworkCompression.WriteUInt16(playerId, array, 0);
		array[2] = isReadyByte;
		SendNetworkMessage(RPCMessageType.PlayerReadyChanged, array);
	}

	private void HandleDisconnectMessage(OrderedRPC.RPCMessage message)
	{
		string text = "Disconnected from server.";
		if (message.data != null && message.data.Length > 0)
		{
			text = text + "\n" + Encoding.UTF8.GetString(message.data, 0, message.data.Length);
		}
		networkScene.ManualStop(text);
	}

	private void HandleFragmentedPrintMessage(OrderedRPC.RPCMessage message)
	{
		byte[] data;
		if (HandleFragmentedMessage(printRPCBuffer, message.senderPlayerID, message.data, out data))
		{
			HandlePrintMessage(data);
		}
	}

	private void HandlePrintMessage(byte[] messageData)
	{
		string message = Encoding.UTF8.GetString(messageData, 0, messageData.Length);
		ConsoleController.ShowServerMessage(message);
	}

	private void HandleSayCommand(OrderedRPC.RPCMessage message)
	{
		PlayerData player = Playerlist.GetPlayer(message.senderPlayerID);
		ChatMode chatMode = (ChatMode)message.data[0];
		string chatMessage = Encoding.UTF8.GetString(message.data, 1, message.data.Length - 1);
		IChatController chatController = ReferenceMaster.ChatController;
		chatController.HandleSayCommand(player, chatMode, chatMessage);
	}

	public void HandleSayMessage(byte[] data)
	{
		PlayerData source = Playerlist.GetPlayer(NetworkCompression.ReadUInt16(data, 0));
		string input = Encoding.UTF8.GetString(data, 2, data.Length - 2);
		WorkshopManager.VerifyString(input, delegate(WorkshopManager.VerifyStringResult res, string str)
		{
			ConsoleController.ShowMessage(str);
			IChatController chatController = ReferenceMaster.ChatController;
			chatController.HandleSayMessage(source, str);
		});
	}

	private byte[] GetLevelCompletionData()
	{
		float[] teamProgress = levelEditor.winCondition.GetTeamProgress();
		byte[] array = new byte[5 + 4 * teamProgress.Length];
		int num = 0;
		array[num] = (byte)teamProgress.Length;
		num++;
		Buffer.BlockCopy(BitConverter.GetBytes(levelEditor.winCondition.completion), 0, array, num, 4);
		num += 4;
		for (int i = 0; i < teamProgress.Length; i++)
		{
			Buffer.BlockCopy(BitConverter.GetBytes(teamProgress[i]), 0, array, num, 4);
			num += 4;
		}
		return array;
	}

	private byte[] GetTeamWinData()
	{
		List<MPTeam> teamWins = levelEditor.winCondition.GetTeamWins();
		byte[] array = new byte[1 + 1 * teamWins.Count];
		int num = 0;
		array[num] = (byte)teamWins.Count;
		num++;
		for (int i = 0; i < teamWins.Count; i++)
		{
			array[num] = (byte)teamWins[i];
			num++;
		}
		return array;
	}

	public bool HandleFragmentedMessage(FragmentedRPC buffer, ushort id, byte[] messageData, out byte[] data)
	{
		int num = 0;
		ushort num2 = NetworkCompression.ReadUInt16(messageData, num);
		int num3 = num + 2;
		byte[] array = new byte[messageData.Length - num3];
		Buffer.BlockCopy(messageData, num3, array, 0, array.Length);
		if (num2 != 0)
		{
			if (buffer.GetCurrentCount(id) == 0)
			{
				Debug.LogWarning("Expected first entry for " + id + ", received " + num2 + ": " + Environment.StackTrace);
				data = null;
				return false;
			}
		}
		else if (buffer.GetCurrentCount(id) > 0)
		{
			buffer.Clear(id);
			Debug.LogWarning("Emptying buffer for " + id + ", buffer is not empty!");
		}
		return buffer.Add(id, num2, array, out data);
	}

	private bool IsInitMessage(RPCMessageType type)
	{
		return type == RPCMessageType.Init || type == RPCMessageType.PlayerJoin || type == RPCMessageType.ReceiveLevelState || type == RPCMessageType.JoinError || type == RPCMessageType.DlcJoinError || type == RPCMessageType.ServerPassword || type == RPCMessageType.JoinErrorMod;
	}

	private IEnumerator IESendToClients(ushort playerId, OrderedRPC.RPCMessage message, bool skipSender)
	{
		int messageLength = OrderedRPC.RPCMessage.Size(message.data.Length);
		ushort messageId = message.ID;
		int headerSize = networkManager.PlayerMessageHeaderSize;
		byte[] clientData = new byte[headerSize + messageLength];
		if (!skipSender || playerId != ownerId)
		{
			messageId = messageBuffer.GetSendID();
			messageBuffer.IncrementSendID();
		}
		OrderedRPC.RPCMessage.Encode(messageId, message.type, message.data, 0, message.data.Length, clientData, headerSize);
		OrderedRPC.SetMessageID(messageId, skipData, headerSize);
		for (int i = 0; i < networkScene.clientList.Count; i++)
		{
			PlayerData player = networkScene.clientList[i];
			while (WaitForNetworkBufferRelease())
			{
				yield return new WaitForSeconds(halfChokeTime);
			}
			ushort pId = player.networkId;
			if ((!skipSender || pId != playerId) && player.initReady)
			{
				networkManager.SendPlayerMessage(pId, playerId, clientData);
			}
			else
			{
				networkManager.SendPlayerMessage(pId, ownerId, skipData);
			}
		}
	}

	private void MessageReceived(ushort playerId, OrderedRPC.RPCMessage message)
	{
		if (message.destination == OrderedRPC.RPCDestination.Network)
		{
			StartCoroutine(IESendToClients(playerId, message, true));
		}
		if (!StatMaster.isHosting || message.execute)
		{
			if (Time.timeScale == 0f && (message.type == RPCMessageType.TimeScale || message.type == RPCMessageType.AutoTimeScale))
			{
				networkAddPiece.SetTimeScale(message.data, message.type == RPCMessageType.AutoTimeScale);
			}
			else
			{
				orderedQueue.Add(playerId, message);
			}
		}
	}

	private void OnAddEntities(ushort playerId, OrderedRPC.RPCMessage message)
	{
		byte[] data;
		if (levelEditor.isActive && HandleFragmentedMessage(entityAddBuffer, playerId, message.data, out data))
		{
			levelEditor.Add(playerId, data);
		}
	}

	private void OnBlockTransformCache(ServerMachine m, byte[] mData)
	{
		byte[] data;
		if (!HandleFragmentedMessage(blockTransformCacheBuffer, m.PlayerID, mData, out data))
		{
			return;
		}
		byte[] array = CLZF2.Decompress(data);
		int num = 0;
		while (num < array.Length)
		{
			switch ((RPCMessageType)array[num++])
			{
			case RPCMessageType.MoveBlock:
				num += m.RemoteMoveBlock(array, num);
				break;
			case RPCMessageType.RotateBlock:
				num += m.RemoteRotateBlock(array, num);
				break;
			case RPCMessageType.RemoveBlock:
				num += m.RemoteRemoveBlock(array, num);
				break;
			case RPCMessageType.AddBlock:
			{
				BlockBehaviour block;
				num += m.RemoteAddBlock(array, num, out block);
				break;
			}
			case RPCMessageType.MirrorDragged:
				num += m.RemoteMirrorDragged(array, num);
				break;
			case RPCMessageType.ReverseBlock:
				num += m.RemoteReverse(array, num);
				break;
			case RPCMessageType.EditBlockData:
				num += m.RemoteEditBlockData(array, num);
				break;
			case RPCMessageType.RefreshBlocks:
				num += m.RemoteRefreshBlocks(array, num);
				break;
			case RPCMessageType.EditBlockSkin:
				num += ChangeBlockSkin(array, num);
				break;
			}
		}
	}

	private void OnCamData(ushort playerId, byte[] camData)
	{
		int num = 0;
		uint num2 = camData[num];
		num++;
		int num3 = 0;
		while (num3++ < num2)
		{
			ushort playerId2 = NetworkCompression.ReadUInt16(camData, num);
			bool flag = camData[num + 2] == 1;
			ServerMachine machine;
			if (networkScene.GetMachine(playerId2, out machine))
			{
				if (flag)
				{
					if (machine.essentialUpdate.Contains(playerId))
					{
						machine.essentialUpdate.Remove(playerId);
						machine.fullUpdate.Add(playerId);
					}
				}
				else if (machine.fullUpdate.Contains(playerId))
				{
					machine.fullUpdate.Remove(playerId);
					machine.essentialUpdate.Add(playerId);
				}
			}
			num += 3;
		}
	}

	private void OnClientMessage(ushort playerId, byte[] messageData, int offset)
	{
		messageBuffer.Receive(playerId, messageData, offset);
		messageData = null;
	}

	private void OnGhostData(ushort playerId, byte[] data, int offset, int size)
	{
		if (StatMaster.isHosting)
		{
			byte[] array = new byte[size];
			Buffer.BlockCopy(data, 0, array, 0, size);
			networkManager.SendGhostData(GetClientList(playerId), array);
		}
		if (playerId != ownerId)
		{
			levelEditor.UpdateGhost(playerId, data, offset);
		}
	}

	private void OnInputData(ushort playerId, int session, byte[] data, int offset)
	{
		if (StatMaster.isHosting)
		{
			ServerMachine machine;
			if (networkScene.GetMachine(playerId, out machine) && machine.Session == session)
			{
				byte[] array = new byte[data.Length - offset];
				Buffer.BlockCopy(data, offset, array, 0, array.Length);
				networkAddPiece.AddInput(machine, array);
			}
		}
		else
		{
			networkAddPiece.ProcessInputData(data, offset);
		}
	}

	private void OnLogicData(BesiegeDataFrame frameData)
	{
		FragmentedRPC buffer;
		if (!level.logicFrameManager.Get(frameData.frame, out buffer))
		{
			return;
		}
		int logicMessageHeaderSize = networkManager.LogicMessageHeaderSize;
		byte[] array = new byte[frameData.dataSize - logicMessageHeaderSize];
		Buffer.BlockCopy(frameData.data, logicMessageHeaderSize, array, 0, array.Length);
		byte[] outData;
		if (!buffer.Add((ushort)frameData.session, frameData.current, array, out outData))
		{
			return;
		}
		if (StatMaster.Mode.LevelEditor.clientGlobalSim)
		{
			if (StatMaster.levelSimulating && (StatMaster.Mode.LevelEditor.clientGlobalSim || OptionsMaster.votingEnabled) && frameData.session == level.Session && level.logicFrame == frameData.frame)
			{
				float time = Time.time;
				levelEditor.ExecuteLogicData(outData, time - buffer.createTime);
				level.logicFrame++;
				FrameBufferManager.CacheEntry cacheData;
				while (level.logicFrameManager.PopCache(level.logicFrame, level.Session, out cacheData))
				{
					levelEditor.ExecuteLogicData(cacheData.data, time - cacheData.createTime);
					level.logicFrame++;
				}
			}
			else
			{
				level.logicFrameManager.AddCache(frameData.frame, frameData.session, outData, buffer.createTime);
			}
		}
		level.logicFrameManager.Remove(buffer, frameData.frame);
	}

	private void OnLevelData(BesiegeDataFrame frameData)
	{
		FragmentedRPC buffer;
		if (!level.frameManager.Get(frameData.frame, out buffer))
		{
			return;
		}
		int levelMessageHeaderSize = networkManager.LevelMessageHeaderSize;
		byte[] array = new byte[frameData.dataSize - levelMessageHeaderSize];
		Buffer.BlockCopy(frameData.data, levelMessageHeaderSize, array, 0, array.Length);
		byte[] outData;
		if (!buffer.Add((ushort)frameData.session, frameData.current, array, out outData))
		{
			return;
		}
		if (StatMaster.Mode.LevelEditor.clientGlobalSim)
		{
			if (StatMaster.levelSimulating && (StatMaster.Mode.LevelEditor.clientGlobalSim || OptionsMaster.votingEnabled) && frameData.session == level.Session)
			{
				networkAddPiece.frame = frameData.frame;
				networkAddPiece.dataManager.UnpackData(frameData.frame, frameData.session, outData);
			}
			else
			{
				level.frameManager.AddCache(frameData.frame, frameData.session, outData, 0f);
			}
		}
		level.frameManager.Remove(buffer, frameData.frame);
	}

	public void OnLevelLoad(string levelData, string levelName)
	{
		networkAddPiece.AutoSave();
		levelEditor.ClearLevel();
		LevelXMLLoader.ReadLevelFromString(levelData, false);
		levelEditor.Settings.Name = levelName;
		hud.CloseAllowedMachines();
		hud.DisableMachineTools();
		if (PlayerData.hasLocalPlayer && !PlayerData.localPlayer.isSpectator)
		{
			PlayerData.localPlayer.allowedMachineIndex = -1;
		}
		if (StatMaster.isHosting)
		{
			if (OptionsMaster.votingEnabled)
			{
				SetPlayerReadyStateAll(false);
			}
			levelEditor.AssignSpawnZones();
			levelEditor.UpdatePlayerStates();
		}
		levelEditor.OnLevelLoad();
		if (ReferenceMaster.onLevelLoad != null)
		{
			ReferenceMaster.onLevelLoad();
		}
		if (StatMaster.isHosting && OptionsMaster.votingEnabled)
		{
			SendNetworkMessage(RPCMessageType.RefreshPlayerStates);
		}
		hud.UpdatePlayers();
		ReferenceMaster.ConsoleController.AppendLogLine("Loaded level '" + levelEditor.Settings.Name + "'");
		networkAddPiece.ToggleLoadingLevel(false);
	}

	private void OnMapperRebindKeys(ushort playerId, OrderedRPC.RPCMessage message)
	{
		byte[] data;
		if (HandleFragmentedMessage(rebindKeyBuffer, playerId, message.data, out data))
		{
			OverviewBlockMapper.OnRebindKeyRemote(playerId, data);
		}
	}

	private void OnMapperEditKey(ushort playerId, OrderedRPC.RPCMessage message)
	{
		byte[] data;
		if (HandleFragmentedMessage(editKeyBuffer, playerId, message.data, out data))
		{
			OverviewBlockMapper.OnRebindGroupRemote(playerId, data);
		}
	}

	private void OnEditMachineData(ushort playerId, OrderedRPC.RPCMessage message)
	{
		byte[] data;
		if (HandleFragmentedMessage(machineDataBuffer, playerId, message.data, out data))
		{
			OverviewBlockMapper.OnEditMachineData(playerId, data);
		}
	}

	private void OnClusterResults(ushort playerId, OrderedRPC.RPCMessage message)
	{
		byte[] data;
		if (!OptionsMaster.networkClusters || !HandleFragmentedMessage(clusterBuffer, playerId, message.data, out data))
		{
			return;
		}
		byte[] array = CLZF2.Decompress(data);
		int num = 0;
		if (StatMaster.isHosting)
		{
			byte[] array2 = new byte[2 + array.Length];
			NetworkCompression.WriteUInt16(playerId, array2, 0);
			Buffer.BlockCopy(array, 0, array2, 2, array.Length);
			byte[] messageData = CLZF2.Compress(array2);
			SendFragmentedNetworkMessage(RPCMessageType.ClusterResults, messageData);
		}
		else
		{
			playerId = NetworkCompression.ReadUInt16(array, 0);
			num += 2;
		}
		PlayerData player;
		if (!Playerlist.GetPlayer(playerId, out player))
		{
			return;
		}
		if (player.isLocalPlayer)
		{
			StatMaster.WaitForServerResponse(StatMaster.ServerResponseType.ClusterResults, false);
			HideLoadingText();
			return;
		}
		if (player.isSpectator)
		{
			if (StatMaster.isHosting)
			{
				CancelResponse(playerId, StatMaster.ServerResponseType.ClusterResults);
			}
			return;
		}
		ServerMachine machine = player.machine;
		if (!machine.isLocalMachine)
		{
			ServerMachine.ClusterResultData resultData;
			machine.ProcessClusterResults(array, num, out resultData);
			machine.ApplyClusterResults(resultData);
		}
	}

	private void OnLevelSettings(ushort playerId, OrderedRPC.RPCMessage message)
	{
		byte[] data;
		if (levelEditor.isActive && HandleFragmentedMessage(entityAddBuffer, playerId, message.data, out data))
		{
			if (StatMaster.isHosting)
			{
				SendLevelSettings(data);
			}
			else
			{
				levelEditor.DecodeSettings(data);
			}
		}
	}

	private void OnLevelSimFrame(ushort playerId, OrderedRPC.RPCMessage message)
	{
		if (StatMaster.isHosting)
		{
			if (StatMaster.levelSimulating && !StatMaster.isLocalSim)
			{
				byte[] levelCompletionData = GetLevelCompletionData();
				int num = levelCompletionData.Length;
				byte[] teamWinData = GetTeamWinData();
				int num2 = teamWinData.Length;
				int simFrame = networkAddPiece.dataManager.GetSimFrame();
				byte[] array = new byte[num + num2 + simFrame];
				int num3 = 0;
				Buffer.BlockCopy(levelCompletionData, 0, array, num3, num);
				num3 += num;
				Buffer.BlockCopy(teamWinData, 0, array, num3, num2);
				num3 += num2;
				networkAddPiece.dataManager.WriteSimFrame(array, num + num2);
				SendFragmentedPlayerMessage(playerId, RPCMessageType.LevelSimFrame, CLZF2.Compress(array));
			}
		}
		else if (StatMaster.Mode.LevelEditor.clientGlobalSim)
		{
			simFrameCorrection = Mathf.Min(message.timeReceived, simFrameCorrection);
			byte[] data;
			if (HandleFragmentedMessage(simFrameBuffer, playerId, message.data, out data))
			{
				ReceiveSimFrame(data, Time.time - simFrameCorrection);
				simFrameCorrection = float.MaxValue;
			}
		}
	}

	private void OnLoadLevelData(ushort playerId, OrderedRPC.RPCMessage message)
	{
		if (!StatMaster.isHosting || playerId == ownerId || StatMaster.Mode.levelEdit)
		{
			byte[] data;
			if (HandleFragmentedMessage(loadLevelBuffer, playerId, message.data, out data))
			{
				byte[] data2 = CLZF2.Decompress(data);
				string levelName;
				string levelData = level.Decode(data2, out levelName);
				HideLoadingText();
				LoadLevel(levelData, levelName, false);
			}
			else
			{
				int completionPercentage = loadLevelBuffer.GetCompletionPercentage(playerId);
				SetLoadingText(LocalisationManager.GetTranslation(2956) + completionPercentage + "%..");
				networkAddPiece.ToggleLoadingLevel(true);
			}
		}
	}

	private void OnLoadMachineData(ushort playerId, OrderedRPC.RPCMessage message)
	{
		byte[] data;
		if (HandleFragmentedMessage(loadMachineBuffer, playerId, message.data, out data))
		{
			int num = 0;
			if (StatMaster.isClient)
			{
				playerId = NetworkCompression.ReadUInt16(data, 0);
				num += 2;
			}
			HideLoadingText();
			ServerMachine machine;
			if (!StatMaster.limitMachines && networkScene.GetMachine(playerId, out machine))
			{
				if (machine.isLocalMachine)
				{
					StatMaster.WaitForServerResponse(StatMaster.ServerResponseType.MachineLoad, false);
					HideLoadingText();
				}
				byte[] array;
				if (StatMaster.isHosting)
				{
					array = data;
					byte[] array2 = new byte[data.Length + 2];
					NetworkCompression.WriteUInt16(playerId, array2, 0);
					Buffer.BlockCopy(data, 0, array2, 2, data.Length);
					SendFragmentedNetworkMessage(RPCMessageType.LoadMachine, array2);
				}
				else
				{
					array = new byte[data.Length - 2];
					Buffer.BlockCopy(data, num, array, 0, array.Length);
				}
				machine.Decode(false, true, CLZF2.Decompress(array), 0);
			}
			else if (StatMaster.isHosting)
			{
				CancelResponse(playerId, StatMaster.ServerResponseType.MachineLoad);
			}
		}
		else
		{
			int completionPercentage = loadMachineBuffer.GetCompletionPercentage(playerId);
			SetLoadingText(LocalisationManager.GetTranslation(2955) + completionPercentage + "%..");
		}
	}

	private void OnMachineData(ushort playerId, BesiegeDataFrame frameData)
	{
		byte[] array;
		if (StatMaster.isHosting)
		{
			array = new byte[frameData.dataSize];
			Buffer.BlockCopy(frameData.data, 0, array, 0, frameData.dataSize);
			networkManager.SendMachineData(GetClientList(playerId), playerId, frameData.frame, frameData.session, frameData.current, array);
		}
		PlayerData player;
		if (!Playerlist.GetPlayer(playerId, out player))
		{
			return;
		}
		uint frame = frameData.frame;
		int session = frameData.session;
		FragmentedRPC buffer;
		if (!player.frameManager.Get(frame, out buffer))
		{
			return;
		}
		int machineMessageHeaderSize = networkManager.MachineMessageHeaderSize;
		array = new byte[frameData.dataSize - machineMessageHeaderSize];
		Buffer.BlockCopy(frameData.data, machineMessageHeaderSize, array, 0, array.Length);
		byte[] outData;
		if (!buffer.Add((ushort)session, frameData.current, array, out outData))
		{
			return;
		}
		ServerMachine machine = player.machine;
		if (!player.isSpectator && machine.isSimulating && machine.isReady && machine.Session == session)
		{
			int num = machine.ReadTransformHeader(frame, outData);
			if (outData.Length > num)
			{
				machine.ReadBufferData(frame, outData, num);
			}
			machine.NewFrame(frame);
		}
		else
		{
			player.frameManager.AddCache(frame, session, outData, 0f);
		}
		player.frameManager.Remove(buffer, frame);
	}

	private void OnReceiveGameState(ushort playerId, OrderedRPC.RPCMessage message)
	{
		gameStateCorrection = Mathf.Min(message.timeReceived, gameStateCorrection);
		byte[] data;
		if (HandleFragmentedMessage(gameStateBuffer, playerId, message.data, out data))
		{
			HideLoadingText();
			StartCoroutine(IEReceiveGameState(data, Time.time - gameStateCorrection));
			gameStateCorrection = float.MaxValue;
		}
		else
		{
			int completionPercentage = gameStateBuffer.GetCompletionPercentage(playerId);
			hud.SetLoadingText(LocalisationManager.GetTranslation(2957) + completionPercentage + "%..");
		}
	}

	private void OnRemoveEntities(ushort playerId, OrderedRPC.RPCMessage message)
	{
		byte[] data;
		if (levelEditor.isActive && HandleFragmentedMessage(entityRemoveBuffer, playerId, message.data, out data))
		{
			levelEditor.Remove(playerId, data);
		}
	}

	private void OnServerMessage(ushort playerId, byte destByte, byte[] data, int offset)
	{
		OrderedRPC.RPCMessage message;
		if (!OrderedRPC.RPCMessage.Decode(data, offset, out message))
		{
			Debug.LogError("Message was incomplete!");
			return;
		}
		message.destination = (OrderedRPC.RPCDestination)destByte;
		message.execute = message.destination != OrderedRPC.RPCDestination.Network || playerId != ownerId;
		messageBuffer.Receive(playerId, message);
	}

	private void OnUpdateEntities(ushort playerId, OrderedRPC.RPCMessage message)
	{
		byte[] data;
		if (levelEditor.isActive && HandleFragmentedMessage(entityUpdateBuffer, playerId, message.data, out data))
		{
			levelEditor.UpdateEntities(playerId, data);
		}
	}

	private int ReadLevelCompletionData(byte[] data, int offset, out float[] teamCompletion)
	{
		int num = offset;
		int num2 = data[offset];
		offset++;
		teamCompletion = new float[1 + num2];
		for (int i = 0; i < num2 + 1; i++)
		{
			teamCompletion[i] = BitConverter.ToSingle(data, offset);
			offset += 4;
		}
		return offset - num;
	}

	private int ReadTeamWinsData(byte[] data, int offset, out List<MPTeam> teamWins)
	{
		int num = offset;
		int num2 = data[offset];
		offset++;
		teamWins = new List<MPTeam>();
		for (int i = 0; i < num2; i++)
		{
			teamWins.Add((MPTeam)data[offset]);
			offset++;
		}
		return offset - num;
	}

	private IEnumerator IEReceiveGameState(byte[] compressedBytes, float timeCorrection)
	{
		LockMessageExecution(true);
		byte[] stateBytes = CLZF2.Decompress(compressedBytes);
		ServerMachine currentMachine = null;
		int currentPlayer = 0;
		List<PlayerData> globalMachines = new List<PlayerData>();
		List<PlayerData> localMachines = new List<PlayerData>();
		PlayerPlatform platform = PlayerPlatform.Unknown;
		ulong platformUserId = 0uL;
		string platformUserName = null;
		int offset = 0;
		byte[] timeScaleBytes = new byte[TIMESCALE_LENGTH];
		Buffer.BlockCopy(stateBytes, offset, timeScaleBytes, 0, timeScaleBytes.Length);
		offset += TIMESCALE_LENGTH;
		networkAddPiece.SetTimeScale(timeScaleBytes, false);
		byte[] hostTimeSinceLevelLoad = new byte[floatByteLength];
		Buffer.BlockCopy(stateBytes, offset, hostTimeSinceLevelLoad, 0, hostTimeSinceLevelLoad.Length);
		offset += floatByteLength;
		networkAddPiece.SetTimeSinceLevelStartOffset(hostTimeSinceLevelLoad, timeCorrection);
		ServerSettings serverSettings = ServerSettings.Decode(stateBytes, ref offset);
		networkScene.UpdateSettings(serverSettings);
		int levelSession = stateBytes[offset];
		offset++;
		int stateByte = stateBytes[offset];
		bool levelIsSim = (stateByte & 1) != 0;
		bool clientSimControl = (stateByte & 2) != 0;
		int playerCount = stateByte >> 2;
		offset++;
		if (StatMaster.Mode.LevelEditor.clientSimControl != clientSimControl)
		{
			levelEditor.ToggleClientSimControl(clientSimControl);
		}
		List<MPTeam> teamWins = new List<MPTeam>();
		float[] teamCompletion;
		if (levelIsSim)
		{
			byte[] autoTimeScaleBytes = new byte[TIMESCALE_LENGTH];
			Buffer.BlockCopy(stateBytes, offset, autoTimeScaleBytes, 0, TIMESCALE_LENGTH);
			offset += TIMESCALE_LENGTH;
			networkAddPiece.SetTimeScale(autoTimeScaleBytes, true, false);
			timeCorrection *= networkAddPiece.lastAutoTimeScale * 2f;
			offset += ReadLevelCompletionData(stateBytes, offset, out teamCompletion);
			offset += ReadTeamWinsData(stateBytes, offset, out teamWins);
		}
		else
		{
			teamCompletion = new float[0];
		}
		uint levelDataLength = BitConverter.ToUInt32(stateBytes, offset);
		offset += 4;
		byte[] levelData = new byte[levelDataLength];
		Buffer.BlockCopy(stateBytes, offset, levelData, 0, levelData.Length);
		string levelName;
		string levelDataString = level.Decode(levelData, out levelName);
		LoadLevel(levelDataString, levelName, false);
		level.Session = levelSession;
		level.ResetFrame();
		offset += levelData.Length;
		level.remoteSim = levelIsSim;
		if (levelIsSim)
		{
			networkAddPiece.dataManager.SetLevel(level);
			int levelSimLength = networkAddPiece.dataManager.ReadSimFrame(stateBytes, offset, timeCorrection);
			offset += levelSimLength;
			networkAddPiece.ToggleLevelSimulation(true);
			for (int t = 0; t < teamCompletion.Length; t++)
			{
				MPTeam currentTeam = (MPTeam)t;
				levelEditor.winCondition.AddProgress(currentTeam, teamCompletion[t]);
			}
			if (teamWins.Count > 0)
			{
				levelEditor.winCondition.SetWinningTeams(teamWins);
			}
		}
		for (; currentPlayer < playerCount; currentPlayer++)
		{
			ushort currentPlayerId = NetworkCompression.ReadUInt16(stateBytes, offset);
			offset += 2;
			int nameLength = NetworkCompression.ReadUInt16(stateBytes, offset);
			offset += 2;
			string playerName = Encoding.UTF8.GetString(stateBytes, offset, nameLength);
			offset += nameLength;
			if (OptionsMaster.networkType == PlayerNetworkType.Playfab)
			{
				platform = (PlayerPlatform)stateBytes[offset++];
				if (platform != PlayerPlatform.Unknown)
				{
					byte[] platformNameBytes = new byte[stateBytes[offset++]];
					Buffer.BlockCopy(stateBytes, offset, platformNameBytes, 0, platformNameBytes.Length);
					platformUserName = Encoding.UTF8.GetString(platformNameBytes);
					offset += platformNameBytes.Length;
					platformUserId = BitConverter.ToUInt64(stateBytes, offset);
					offset += 8;
				}
			}
			byte machineState = stateBytes[offset];
			offset++;
			bool isSpectator = (machineState & 1) != 0;
			bool localSim = (machineState & 2) != 0;
			PlayerData playerData = PlayerConnected(currentPlayerId, isSpectator);
			if (!isSpectator)
			{
				bool isSim = (machineState & 4) != 0;
				bool hasClusters = (machineState & 8) != 0;
				bool boundsEnabled = (machineState & 0x10) != 0;
				bool hasSpawnZone = (machineState & 0x20) != 0;
				bool voteState = (machineState & 0x40) != 0;
				int machineSession = stateBytes[offset];
				offset++;
				byte godModes = stateBytes[offset];
				offset++;
				long spawnZoneId = 0L;
				if (hasSpawnZone)
				{
					spawnZoneId = BitConverter.ToInt64(stateBytes, offset);
					offset += LevelEntity.ID_LENGTH;
				}
				NetworkCompression.UnpackVector(stateBytes, offset, out posHolder);
				offset += 12;
				NetworkCompression.UnpackQuaternion(stateBytes, offset, out rotHolder);
				offset += 16;
				currentMachine = CreateClient(playerData, posHolder, rotHolder, false);
				playerData.voteState = voteState;
				if (isSim)
				{
					if (localSim)
					{
						localMachines.Add(playerData);
					}
					else
					{
						globalMachines.Add(playerData);
					}
				}
				offset += currentMachine.Decode(hasClusters, true, stateBytes, offset);
				currentMachine.Session = machineSession;
				currentMachine.UpdateGodMode(godModes);
				if (!boundsEnabled)
				{
					playerData.buildZone.boundingBoxController.RemoteToggleBounds(false);
				}
				LevelEntity spawnEntity;
				if (hasSpawnZone && levelEditor.Get(spawnZoneId, out spawnEntity) && spawnEntity.behaviour is BuildZoneObject)
				{
					BuildZoneObject zoneObj = spawnEntity.behaviour as BuildZoneObject;
					zoneObj.SetBuildZone(playerData.buildZone, false);
				}
			}
			playerData.name = playerName;
			if (OptionsMaster.networkType == PlayerNetworkType.Playfab)
			{
				playerData.platform = platform;
				playerData.platformUserName = platformUserName;
				playerData.platformUserId = platformUserId;
			}
			playerData.initReady = true;
			if (PlayerData.onInitReady != null)
			{
				PlayerData.onInitReady(playerData, true);
			}
			if (playerData.isLocalPlayer)
			{
				PlayerData.localPlayer = playerData;
				playerData.isLocalPlayer = true;
				playerData.inLocalSim = localSim;
				PlayerData.hasLocalPlayer = true;
				playerData.wantSpectator = OptionsMaster.spectatorEnabled;
				StatMaster.Mode.LevelEditor.clientGlobalSim = !localSim;
				OptionsMaster.spectatorEnabled = isSpectator;
				if (!isSpectator)
				{
					SingleInstance<MachineObjectTracker>.Instance.SetActiveMachine(currentMachine);
				}
			}
		}
		if (PlayerData.hasLocalPlayer)
		{
			hud.OnToggleSpectator();
			if (localMachines.Count > 0)
			{
				yield return ToggleMachinePlayerMode(localMachines, BesiegePlayMode.LocalSimulation);
			}
			if (globalMachines.Count > 0)
			{
				yield return ToggleMachinePlayerMode(globalMachines, BesiegePlayMode.GlobalSimulation);
			}
			LockMessageExecution(false);
			hud.OnGameStateReceived();
		}
		else
		{
			Debug.LogError("Didn't receive local player!");
		}
	}

	private void ReceiveSimFrame(byte[] compressedSimData, float timeCorrection)
	{
		if (!StatMaster.levelSimulating && level.remoteSim && requestedSimFrame)
		{
			byte[] data = CLZF2.Decompress(compressedSimData);
			int num = 0;
			float[] teamCompletion;
			num += ReadLevelCompletionData(data, num, out teamCompletion);
			List<MPTeam> teamWins = new List<MPTeam>();
			num += ReadTeamWinsData(data, num, out teamWins);
			networkAddPiece.dataManager.ReadSimFrame(data, num, timeCorrection);
			networkAddPiece.ToggleLevelSimulation(true, false, true);
			networkAddPiece.SetTimeScale(networkAddPiece.lastAutoTimeScale, false);
			timeCorrection *= networkAddPiece.lastAutoTimeScale * 2f;
			for (int i = 0; i < teamCompletion.Length; i++)
			{
				MPTeam team = (MPTeam)i;
				levelEditor.winCondition.AddProgress(team, teamCompletion[i]);
			}
			if (teamWins.Count > 0)
			{
				levelEditor.winCondition.SetWinningTeams(teamWins);
			}
			requestedSimFrame = false;
		}
	}

	public void SyncLogicData(byte[] logicData)
	{
		FragmentedRPC.Send(SendLogicData, logicData, 0, networkManager.LogicMessageHeaderSize);
	}

	private void SendLogicData(ushort current, byte[] data)
	{
		networkManager.SendLogicData(level.logicFrame, level.Session, current, data);
	}

	private void RemoveMachine(PlayerData player, bool createInfoForLocal = true)
	{
		PlayerBuildZone buildZone = player.buildZone;
		if (player.isSpectator)
		{
			return;
		}
		ServerMachine machine = player.machine;
		if (!machine.isReady)
		{
			LockMessageExecution(false);
		}
		if (machine.isSimulating)
		{
			bool simPhysics = machine.SimPhysics;
			machine.EndSimulation();
			if (simPhysics && !networkAddPiece.hasActiveMachines)
			{
				networkAddPiece.ToggleLevelSimulation(false, true);
			}
		}
		machine.StopAllCoroutines();
		if (machine.isLocalMachine && createInfoForLocal)
		{
			machine.nodeController.Dispose();
			MachineObjectTracker.lastBuild = machine.CreateMachineInfo();
		}
		player.isSpectator = true;
		player.allowedMachineIndex = -1;
		if (buildZone.hasSpawnZone)
		{
			buildZone.spawnZone.RemoveBuildZone();
		}
		if (machine.isLocalMachine)
		{
			machine.FinishDraggedBlocks();
			networkAddPiece.ClearGhost();
			hud.TurnOffMachineRules();
			SingleInstance<MachineObjectTracker>.Instance.SetActiveMachine(null);
			StatMaster.SetSimulationState(SimulationState.SpectatorMode);
		}
		player.PlayMode = BesiegePlayMode.Spectator;
		StatMaster.activePlayerCount--;
		UnityEngine.Object.Destroy(machine.gameObject);
		player.machine = null;
		buildZones.Remove(buildZone);
		UnityEngine.Object.Destroy(buildZone.gameObject);
		player.buildZone = null;
		ReferenceMaster.ClearBuildingBlocks(player.networkId);
		if (StatMaster.totalBlocksChanged != null)
		{
			StatMaster.totalBlocksChanged();
		}
	}

	private IEnumerator IEPlayerLeave(PlayerData player)
	{
		while (MessagesLocked)
		{
			yield return null;
		}
		ushort playerId = player.networkId;
		if (BesiegeLogFilter.logDebug)
		{
			Debug.Log("Player " + playerId + " (" + player.name + ") left");
		}
		byte[] playerBytes = new byte[2];
		NetworkCompression.WriteUInt16(playerId, playerBytes, 0);
		SendNetworkMessage(RPCMessageType.PlayerLeave, playerBytes);
		PlayerDisconnected(playerId);
	}

	private IEnumerator IEPlayerJoin(PlayerData targetPlayer, byte[] playerInfo, bool spectator)
	{
		while ((!targetPlayer.isLocalPlayer && !networkScene.IsHostReady()) || MessagesLocked)
		{
			yield return null;
		}
		SendNetworkMessage(RPCMessageType.PlayerJoin, playerInfo);
		if (!spectator && CanCreateClient())
		{
			ToggleSpectator(spectatorData: new byte[2], playerId: targetPlayer.networkId);
		}
		else
		{
			hud.UpdatePlayers();
		}
		ReferenceMaster.ConsoleController.AppendLogLine("Player '" + targetPlayer.name + "' connected.");
		if (targetPlayer.isLocalPlayer)
		{
			yield break;
		}
		targetPlayer.inLocalSim = !StatMaster.Mode.LevelEditor.clientSimControl;
		LockMessageExecution(true);
		List<byte[]> dataList = new List<byte[]>();
		int totalLength = 0;
		byte playerCount = 0;
		int offset = 0;
		for (int i = 0; i < Playerlist.Players.Count; i++)
		{
			PlayerData player = Playerlist.Players[i];
			playerCount++;
			byte[] playerNameBytes = Encoding.UTF8.GetBytes(player.name);
			int headerSize = 5 + playerNameBytes.Length;
			byte[] idBytes = null;
			byte[] platformUserNameBytes = null;
			PlayerPlatform platform = PlayerPlatform.Unknown;
			if (OptionsMaster.networkType == PlayerNetworkType.Playfab)
			{
				headerSize++;
				platform = player.platform;
				if (platform != PlayerPlatform.Unknown)
				{
					platformUserNameBytes = Encoding.UTF8.GetBytes(player.platformUserName);
					headerSize += 1 + platformUserNameBytes.Length;
					idBytes = BitConverter.GetBytes(player.platformUserId);
					headerSize += idBytes.Length;
				}
			}
			offset = 0;
			byte[] data;
			if (player.isSpectator)
			{
				data = new byte[headerSize];
				NetworkCompression.WriteUInt16(player.networkId, data, offset);
				offset += 2;
				NetworkCompression.WriteUInt16((ushort)playerNameBytes.Length, data, offset);
				offset += 2;
				Buffer.BlockCopy(playerNameBytes, 0, data, offset, playerNameBytes.Length);
				offset += playerNameBytes.Length;
				if (OptionsMaster.networkType == PlayerNetworkType.Playfab)
				{
					data[offset++] = (byte)platform;
					if (platform != PlayerPlatform.Unknown)
					{
						data[offset++] = (byte)platformUserNameBytes.Length;
						Buffer.BlockCopy(platformUserNameBytes, 0, data, offset, platformUserNameBytes.Length);
						offset += platformUserNameBytes.Length;
						Buffer.BlockCopy(idBytes, 0, data, offset, idBytes.Length);
						offset += idBytes.Length;
					}
				}
				data[offset] = (byte)(1 | (player.inLocalSim ? 2 : 0));
				offset++;
			}
			else
			{
				ServerMachine machine = player.machine;
				PlayerBuildZone buildZone = player.buildZone;
				bool isSim = machine.isSimulating;
				bool hasSimData = isSim && machine.isReady;
				bool includeClusters = targetPlayer.networkId != machine.PlayerID && !machine.analyzing;
				byte[] machineData = machine.Encode(hasSimData, ref includeClusters);
				bool hasSpawnZone = buildZone.hasSpawnZone;
				data = new byte[headerSize + 1 + 1 + (hasSpawnZone ? LevelEntity.ID_LENGTH : 0) + 12 + 16 + machineData.Length];
				NetworkCompression.WriteUInt16(player.networkId, data, offset);
				offset += 2;
				NetworkCompression.WriteUInt16((ushort)playerNameBytes.Length, data, offset);
				offset += 2;
				Buffer.BlockCopy(playerNameBytes, 0, data, offset, playerNameBytes.Length);
				offset += playerNameBytes.Length;
				if (OptionsMaster.networkType == PlayerNetworkType.Playfab)
				{
					data[offset++] = (byte)platform;
					if (platform != PlayerPlatform.Unknown)
					{
						data[offset++] = (byte)platformUserNameBytes.Length;
						Buffer.BlockCopy(platformUserNameBytes, 0, data, offset, platformUserNameBytes.Length);
						offset += platformUserNameBytes.Length;
						Buffer.BlockCopy(idBytes, 0, data, offset, idBytes.Length);
						offset += idBytes.Length;
					}
				}
				bool boundsEnabled = ((!machine.isLocalMachine) ? buildZone.boundingBoxController.boundingEnabled : StatMaster.Bounding.Enabled);
				data[offset] = (byte)((player.inLocalSim ? 2 : 0) | (isSim ? 4 : 0) | (includeClusters ? 8 : 0) | (boundsEnabled ? 16 : 0) | (hasSpawnZone ? 32 : 0) | (player.voteState ? 64 : 0));
				offset++;
				data[offset] = (byte)machine.Session;
				offset++;
				data[offset] = machine.GetGodModes();
				offset++;
				if (hasSpawnZone)
				{
					byte[] spawnIdBytes = buildZone.spawnZone.GetIdentifierBytes();
					Buffer.BlockCopy(spawnIdBytes, 0, data, offset, spawnIdBytes.Length);
					offset += LevelEntity.ID_LENGTH;
				}
				NetworkCompression.PackVector(buildZone.transform.position, data, offset);
				offset += 12;
				NetworkCompression.PackQuaternion(buildZone.transform.rotation, data, offset);
				offset += 16;
				Buffer.BlockCopy(machineData, 0, data, offset, machineData.Length);
				offset += machineData.Length;
			}
			dataList.Add(data);
			totalLength += offset;
		}
		byte[] levelData = level.Encode(levelEditor.Settings.Name);
		bool levelSim = StatMaster.levelSimulating && !StatMaster.isLocalSim;
		int levelSimLength = (levelSim ? networkAddPiece.dataManager.GetSimFrame() : 0);
		byte[] timeScaleBytes = networkAddPiece.GetTimeScale(networkAddPiece.lastTimeScale);
		byte[] serverSettings = NetworkScene.ServerSettings.Encode();
		byte[] autoTimeScaleBytes = ((!levelSim) ? new byte[0] : networkAddPiece.GetTimeScale(networkAddPiece.lastAutoTimeScale));
		byte[] completionData = ((!levelSim) ? new byte[0] : GetLevelCompletionData());
		int completionLength = completionData.Length;
		byte[] teamWinData = ((!levelSim) ? new byte[0] : GetTeamWinData());
		int teamWinDataLength = teamWinData.Length;
		byte[] hostTimeSinceLevelLoad = BitConverter.GetBytes(Time.timeSinceLevelLoad);
		Debug.Log("TimeSinceLevelStart=" + Time.timeSinceLevelLoad);
		byte[] stateBytes = new byte[timeScaleBytes.Length + hostTimeSinceLevelLoad.Length + serverSettings.Length + 1 + 1 + autoTimeScaleBytes.Length + completionLength + teamWinDataLength + 4 + levelData.Length + levelSimLength + totalLength];
		offset = 0;
		Buffer.BlockCopy(timeScaleBytes, 0, stateBytes, offset, timeScaleBytes.Length);
		offset += timeScaleBytes.Length;
		Buffer.BlockCopy(hostTimeSinceLevelLoad, 0, stateBytes, offset, hostTimeSinceLevelLoad.Length);
		offset += hostTimeSinceLevelLoad.Length;
		Buffer.BlockCopy(serverSettings, 0, stateBytes, offset, serverSettings.Length);
		offset += serverSettings.Length;
		stateBytes[offset] = (byte)level.Session;
		offset++;
		stateBytes[offset] = (byte)((levelSim ? 1 : 0) | (StatMaster.Mode.LevelEditor.clientSimControl ? 2 : 0) | (playerCount << 2));
		offset++;
		if (levelSim)
		{
			Buffer.BlockCopy(autoTimeScaleBytes, 0, stateBytes, offset, autoTimeScaleBytes.Length);
			offset += autoTimeScaleBytes.Length;
			Buffer.BlockCopy(completionData, 0, stateBytes, offset, completionLength);
			offset += completionLength;
			Buffer.BlockCopy(teamWinData, 0, stateBytes, offset, teamWinDataLength);
			offset += teamWinDataLength;
		}
		NetworkCompression.WriteUInt((uint)levelData.Length, false, stateBytes, offset);
		offset += 4;
		Buffer.BlockCopy(levelData, 0, stateBytes, offset, levelData.Length);
		offset += levelData.Length;
		if (levelSim)
		{
			networkAddPiece.dataManager.WriteSimFrame(stateBytes, offset);
			offset += levelSimLength;
		}
		NetworkCompression.WriteArray(dataList, stateBytes, offset);
		SendFragmentedPlayerMessage(targetPlayer.networkId, RPCMessageType.ReceiveLevelState, CLZF2.Compress(stateBytes));
		LockMessageExecution(false);
	}

	public Vector3 GetZonePosition(int index)
	{
		if (index == 0)
		{
			return new Vector3(0f, 5.072f, 0f);
		}
		index--;
		int num = 1 + Mathf.FloorToInt(index / playerZoneDirections.Count);
		while (index >= playerZoneDirections.Count)
		{
			index -= playerZoneDirections.Count;
		}
		Vector2 vector = playerZoneDirections[index] * num;
		return new Vector3(vector.x, 5.072f, vector.y);
	}

	public Vector3 GetZonePosition()
	{
		int i = 1;
		int num = 5;
		Vector3[] array = new Vector3[buildZones.Count];
		for (int j = 0; j < buildZones.Count; j++)
		{
			array[j] = buildZones[j].zoneTransform.position;
		}
		Vector3 vector = new Vector3(0f, 5.072f, 0f);
		if (IsAvailablePos(vector, array))
		{
			return vector;
		}
		for (; i <= num; i++)
		{
			for (int k = 0; k < playerZoneDirections.Count; k++)
			{
				Vector2 vector2 = playerZoneDirections[k] * i;
				vector.x = vector2.x;
				vector.z = vector2.y;
				if (IsAvailablePos(vector, array))
				{
					return vector;
				}
			}
		}
		Debug.LogError("Couldn't find a suitable start position!");
		return Vector3.zero;
	}

	private bool IsAvailablePos(Vector3 zonePos, Vector3[] zonePositions)
	{
		foreach (Vector3 vector in zonePositions)
		{
			if ((vector - zonePos).sqrMagnitude < 10f)
			{
				return false;
			}
		}
		return true;
	}

	private void Start()
	{
		networkAddPiece = NetworkAddPiece.Instance;
		levelEditor = LevelEditor.Instance;
		level = CustomLevel.Instance;
		networkScene = NetworkScene.Instance;
		networkManager = BesiegeNetworkManager.Instance;
		networkManager.onServerMessage = OnServerMessage;
		networkManager.onClientMessage = OnClientMessage;
		networkManager.onLevelData = OnLevelData;
		networkManager.onLogicData = OnLogicData;
		networkManager.onMachineData = OnMachineData;
		networkManager.onInputData = OnInputData;
		networkManager.onGhostData = OnGhostData;
		networkManager.onCamData = OnCamData;
		int playerMessageHeaderSize = networkManager.PlayerMessageHeaderSize;
		skipData = new byte[playerMessageHeaderSize + OrderedRPC.RPCMessage.Size(0)];
		OrderedRPC.RPCMessage.Encode(0, RPCMessageType.Skip, null, 0, 0, skipData, playerMessageHeaderSize);
	}

	private void Update()
	{
		if (StatMaster.isHosting)
		{
			TimeoutZombies();
			UpdatePlayerPings();
		}
	}

	private void TimeoutZombies()
	{
		for (int i = 0; i < Playerlist.Players.Count; i++)
		{
			PlayerData playerData = Playerlist.Players[i];
			if (playerData.isZombie && !playerData.isDropped && playerData.lastPacketTime + 2f < Time.time)
			{
				ConsoleController.ShowServerMessage("Dropping player '" + playerData.name + "', timed out(zombie).");
				playerData.isDropped = true;
				networkManager.DisconnectPlayer(playerData.networkId);
			}
		}
	}

	private void UpdatePlayerPings()
	{
		if (lastPingUpdate + 1f > Time.time)
		{
			return;
		}
		lastPingUpdate = Time.time;
		foreach (PlayerData player in Playerlist.Players)
		{
			player.ping = networkManager.GetPlayerPing(player);
		}
	}

	private void UpdateBlockState(ushort playerId, byte[] data)
	{
		if (StatMaster.isClient)
		{
			playerId = NetworkCompression.ReadUInt16(data, 0);
			byte[] array = new byte[data.Length - 2];
			Buffer.BlockCopy(data, 2, array, 0, array.Length);
			data = array;
		}
		ServerMachine machine;
		if (!networkScene.GetMachine(playerId, out machine))
		{
			return;
		}
		int num = 0;
		int count;
		num += NetworkCompression.UnpackUInt(data, num, true, out count);
		BlockBehaviour block;
		if (!machine.GetBlockFromIndex(count, out block))
		{
			return;
		}
		int num2 = data[num];
		bool flag = (num2 & 2) != 0;
		bool flag2 = (num2 & 4) != 0;
		num++;
		block.isBMAction = true;
		if (flag)
		{
			XDataHolder xDataHolder = new XDataHolder();
			num += xDataHolder.Decode(data, num);
			block.OnLoad(xDataHolder, CopyMode.All);
		}
		BlockSkinLoader.SkinPack.Skin skin;
		if (flag2)
		{
			BlockSkinLoader.SkinPack.Skin.Decode(data, num, out skin);
		}
		else
		{
			skin = block.Prefab.DefaultSkin;
		}
		if (block.VisualController.selectedSkin != skin)
		{
			block.VisualController.ReplaceSkin(skin);
			if (machine.isLocalMachine)
			{
				block.OnUpdateSkin();
			}
		}
		block.isBMAction = false;
		if (StatMaster.isHosting)
		{
			byte[] array = new byte[2 + data.Length];
			NetworkCompression.WriteUInt16(playerId, array, 0);
			Buffer.BlockCopy(data, 0, array, 2, data.Length);
			SendNetworkMessage(RPCMessageType.UpdateBlockState, array);
		}
	}

	private void UpdateEntityState(ushort playerId, byte[] data)
	{
		if (!levelEditor.isActive)
		{
			return;
		}
		if (StatMaster.isClient)
		{
			playerId = NetworkCompression.ReadUInt16(data, 0);
			byte[] array = new byte[data.Length - 2];
			Buffer.BlockCopy(data, 2, array, 0, array.Length);
			data = array;
		}
		int num = 0;
		long id = BitConverter.ToInt64(data, num);
		num += LevelEntity.ID_LENGTH;
		LevelEntity entity;
		if (levelEditor.Get(id, out entity))
		{
			SaveableDataHolder behaviour = entity.behaviour;
			int num2 = data[num];
			bool flag = (num2 & 2) != 0;
			num++;
			CopyMode mode = (CopyMode)data[num];
			num++;
			behaviour.isBMAction = true;
			XDataHolder xDataHolder = new XDataHolder();
			if (flag)
			{
				xDataHolder.Decode(data, num);
			}
			behaviour.OnLoad(xDataHolder, mode);
			BlockMapper currentInstance = BlockMapper.CurrentInstance;
			if ((bool)currentInstance && currentInstance.IsLogic && currentInstance.Entity == behaviour)
			{
				currentInstance.Refresh();
			}
			behaviour.isBMAction = false;
			if (StatMaster.isHosting)
			{
				byte[] array = new byte[2 + data.Length];
				NetworkCompression.WriteUInt16(playerId, array, 0);
				Buffer.BlockCopy(data, 0, array, 2, data.Length);
				SendNetworkMessage(RPCMessageType.UpdateEntityState, array);
			}
		}
	}

	private void UpdatePlayerSelection(ushort playerId, byte[] data, bool isReset)
	{
		PlayerData player;
		if (Playerlist.GetPlayer(playerId, out player))
		{
			LevelEntity entity;
			if (isReset)
			{
				player.hasSelection = false;
				player.selectedEntity = null;
			}
			else if (levelEditor.Get(BitConverter.ToInt64(data, 0), out entity))
			{
				player.hasSelection = true;
				player.selectedEntity = entity;
				player.activePrefab = entity.behaviour.prefab;
			}
		}
	}

	private void OnUpdateBlockData(ushort senderPlayerId, OrderedRPC.RPCMessage message)
	{
		byte[] data;
		if (HandleFragmentedMessage(blockDataBuffer, senderPlayerId, message.data, out data))
		{
			BlockMapper.UpdateBlockData(senderPlayerId, data);
		}
	}

	private void OnResetBlock(ushort senderPlayerId, OrderedRPC.RPCMessage message)
	{
		byte[] data;
		if (HandleFragmentedMessage(resetBlockBuffer, senderPlayerId, message.data, out data))
		{
			BlockMapper.ResetBlock(senderPlayerId, data);
		}
	}

	private void OnPasteBlock(ushort senderPlayerId, OrderedRPC.RPCMessage message)
	{
		byte[] data;
		if (HandleFragmentedMessage(pasteBlockBuffer, senderPlayerId, message.data, out data))
		{
			BlockMapper.PasteBlock(senderPlayerId, data);
		}
	}

	public void OnJoinFailedMod(ushort playerId, byte[] modListBytes)
	{
		byte[] data;
		if (HandleFragmentedMessage(modJoinErrorBuffer, playerId, modListBytes, out data))
		{
			int offset = 0;
			ModList local = ModList.GetLocal();
			ModList modList = ModList.FromBytes(data, ref offset);
			List<ModList.Mod> mismatchedMods;
			local.Compare(modList, out mismatchedMods);
			if (BesiegeLogFilter.logDebug)
			{
				Debug.Log("[ModList] Got JoinError!\nLocal:\n\t- " + string.Join("\n\t- ", local.GetStringArray()) + "\n\nRemote:\n\t- " + string.Join("\n\t- ", modList.GetStringArray()) + "\n\nComputed mismatches: " + CompatibilityChecker.MismatchesToString(mismatchedMods));
			}
			networkManager.OnJoinFailedMod(mismatchedMods);
		}
	}
}
