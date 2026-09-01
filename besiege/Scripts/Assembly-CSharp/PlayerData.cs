using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
	public class CamInfo
	{
		public bool fullUpdate;

		public CamInfo()
		{
			fullUpdate = true;
		}
	}

	public static Action<PlayerData, bool> onInitReady;

	public static PlayerData localPlayer;

	public static bool hasLocalPlayer;

	public string name;

	public PlayerNetworkType networkType;

	public PlayerPlatform platform;

	public ushort networkId;

	public string platformUserName;

	public ulong platformUserId;

	public bool voteState;

	public bool isVisible;

	public bool isSpectator;

	public bool wantSpectator;

	public bool passCorrect;

	public bool initReady;

	public int allowedMachineIndex;

	public bool inLocalSim;

	public Vector3 customPos;

	public bool useCustomPos;

	public bool hasSelection;

	public LevelPrefab activePrefab;

	public LevelEntity selectedEntity;

	public bool isLocalPlayer;

	public MPTeam team;

	public ServerMachine machine;

	public PlayerBuildZone buildZone;

	public bool prevMachine;

	public FrameBufferManager frameManager;

	public Dictionary<ushort, CamInfo> camInfo;

	public int ping;

	public bool isZombie;

	public float lastPacketTime;

	public bool isDropped;

	private BesiegePlayMode previousPlayMode;

	private BesiegePlayMode playMode;

	public BesiegePlayMode PlayMode
	{
		get
		{
			return playMode;
		}
		set
		{
			previousPlayMode = playMode;
			playMode = value;
		}
	}

	public BesiegePlayMode PreviousPlayMode
	{
		get
		{
			return previousPlayMode;
		}
	}

	public PlayerData(ushort id)
	{
		networkId = id;
		initReady = false;
		allowedMachineIndex = -1;
		name = string.Empty;
		voteState = false;
		isLocalPlayer = false;
		isSpectator = true;
		passCorrect = true;
		wantSpectator = false;
		isVisible = true;
		useCustomPos = false;
		hasSelection = false;
		prevMachine = false;
		customPos = Vector3.zero;
		playMode = (previousPlayMode = BesiegePlayMode.Spectator);
		team = MPTeam.None;
		frameManager = new FrameBufferManager();
		ping = 0;
		if (StatMaster.isClient)
		{
			camInfo = new Dictionary<ushort, CamInfo>();
		}
	}
}
