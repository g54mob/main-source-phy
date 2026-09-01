using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using InternalModding.Events;
using Modding;
using Modding.Levels;
using UnityEngine;
using cakeslice;

public class LevelEditor : MonoBehaviour
{
	public enum EntityUpdateState
	{
		Remove = 0,
		Position = 1,
		Rotation = 2,
		Scale = 4,
		Transform = 7,
		Place = 15
	}

	private class HoverEntry
	{
		public LevelEntity entity;

		public GameObject obj;

		public float sqrDist;

		public bool isEntity;

		public RaycastHit hit;

		public HoverEntry(LevelEntity e, GameObject o, RaycastHit h, float dist, bool hasEntity)
		{
			entity = e;
			obj = o;
			sqrDist = dist;
			hit = h;
			isEntity = hasEntity;
		}
	}

	public delegate void LevelSettingsChangedHandler(LevelSettings settings);

	private const float compareThreshold = 0.0001f;

	public static float Version = 0.9f;

	public static int BUILD_ZONE_ID = 9001;

	public bool isDirty;

	public static LevelEditor Instance;

	[HideInInspector]
	public NetworkEditFieldHandler mapperEditHandler;

	[HideInInspector]
	public NetworkEditLogicHandler editLogicHandler;

	[HideInInspector]
	public OutlineEffect outlineEffect;

	public LevelSettings Settings;

	public XDataHolder CustomData;

	public LevelPrefab ActiveObjectBrush;

	public Transform[] Tools;

	public Transform ToolTransform;

	public EntitySelectionTool selectionController;

	public EntityController entityController;

	public LevelGhostManager ghostManager;

	public Material ghostMaterial;

	public FileBrowserView fileBrowserView;

	public NetworkHUD hud;

	public bool useOutline;

	public LevelEnvironmentManager environmentManager;

	public WinCondition winCondition;

	private CustomLevel level;

	private float ghostAngleIncrement = 45f;

	private RaycastHit lastHit;

	private Ray lastRay;

	private float waterSurfaceY;

	private Plane waterSurfacePlane;

	private GameObject ToolGO;

	private NetworkAddPiece addPiece;

	private NetworkAuxAddPiece auxAddPiece;

	private NetworkScene networkScene;

	private ushort ownerId;

	private float lastSent;

	private float ghostRotY;

	private Vector3 ghostScale;

	private bool isInitialized;

	private LevelPrefab lastPrefab;

	private StatMaster.Tool lastTool;

	private List<LevelEntity> lastSelection;

	private int gizmoLayer = 23;

	private float deleteMouseDownTime = 0.5f;

	private float currentMouseDownTime;

	private LevelEntity lastSelectedEntity;

	private bool isHoldingRotation;

	private bool isHoldingScale;

	private Vector2 pressedMousePos = Vector3.zero;

	private Vector3 pressedMousePoint;

	private float mouseRotateY;

	private Vector3 mouseScale = Vector3.one;

	private float rotateMouseIncrement = 25f;

	private float scaleMouseIncrement = 10f;

	private float paintDownTime;

	private float paintInterval = 0.3f;

	private LevelEditorUI editorUI;

	private List<LevelEntity> removedEntities;

	private float lastSelectUpdate;

	private float updateSelectionInterval = 0.5f;

	private bool updatedSelection;

	private LevelEntity lastSentSelection;

	private List<byte[]> logicEventData;

	private int logicEventDataSize;

	public static Action<float> WaterHeightUpdated;

	public LevelSettingsChangedHandler LevelSettingsChanged;

	public Action<bool> FileBrowserToggled;

	private WaterFogController waterFog;

	protected bool quitting;

	private Vector2 lastClickPoint = Vector3.zero;

	public bool isActive
	{
		get
		{
			return StatMaster.Mode.levelEdit;
		}
	}

	public List<LevelEntity> Entities
	{
		get
		{
			return entityController.Entities;
		}
	}

	public List<LevelEntity> SortedEntities
	{
		get
		{
			return entityController.SortedEntities;
		}
	}

	public CustomLevel Level
	{
		get
		{
			return level;
		}
	}

	public StatMaster.Tool CurrentState
	{
		get
		{
			return StatMaster.Mode.LevelEditor.selectedTool;
		}
	}

	public bool inPlaceMode
	{
		get
		{
			if (CurrentState != StatMaster.Tool.None)
			{
				return false;
			}
			return ActiveObjectBrush != null && ActiveObjectBrush.gameObject != null;
		}
	}

	public List<LevelEntity> Selection
	{
		get
		{
			return selectionController.LevelSelection;
		}
	}

	public int SelectionCount
	{
		get
		{
			return selectionController.Count;
		}
	}

	public static string SaveVector3(Vector3 v)
	{
		int digits = 3;
		return "(" + Math.Round(v.x, digits) + ", " + Math.Round(v.y, digits) + ", " + Math.Round(v.z, digits) + ")";
	}

	public bool SelectionContains(long id)
	{
		return selectionController.Contains(id);
	}

	public static bool IsEqualVec(Vector3 vec1, Vector3 vec2)
	{
		return Mathf.Abs(vec1.x - vec2.x) < 0.0001f && Mathf.Abs(vec1.y - vec2.y) < 0.0001f && Mathf.Abs(vec1.z - vec2.z) < 0.0001f;
	}

	public static bool IsEqualQuat(Quaternion q1, Quaternion q2)
	{
		return Mathf.Abs(q1.x - q2.x) < 0.0001f && Mathf.Abs(q1.y - q2.y) < 0.0001f && Mathf.Abs(q1.z - q2.z) < 0.0001f && Mathf.Abs(q1.w - q2.w) < 0.0001f;
	}

	protected void Awake()
	{
		Instance = this;
		ReferenceMaster.ResetEditor += ResetWindow;
		logicEventData = new List<byte[]>();
		logicEventDataSize = 0;
		entityController = base.gameObject.AddComponent<EntityController>();
		mapperEditHandler = base.gameObject.AddComponent<NetworkEditFieldHandler>();
		editLogicHandler = base.gameObject.AddComponent<NetworkEditLogicHandler>();
		removedEntities = new List<LevelEntity>();
		ToolGO = ToolTransform.gameObject;
		outlineEffect = UnityEngine.Object.FindObjectOfType(typeof(OutlineEffect)) as OutlineEffect;
		UpdateWaterHeight(waterSurfaceY);
		ReferenceMaster.onExecuteEvent = OnExecuteEvent;
		if (StatMaster.Mode.LevelEditor.paintPlacement)
		{
			ResetGhostTransform();
		}
	}

	public void UpdateWaterHeight(float h)
	{
		waterSurfaceY = h;
		waterSurfacePlane = new Plane(Vector3.up, Vector3.up * waterSurfaceY);
		environmentManager.UpdateWaterHeight(h);
		if (WaterHeightUpdated != null)
		{
			WaterHeightUpdated(h);
		}
	}

	public void UpdateEnvironmentType(int i)
	{
		environmentManager.UpdateEnvironmentType(i);
	}

	protected void Start()
	{
		addPiece = NetworkAddPiece.Instance;
		auxAddPiece = NetworkAuxAddPiece.Instance;
		networkScene = NetworkScene.Instance;
		editorUI = SingleInstanceFindOnly<LevelEditorUI>.Instance;
		winCondition = WinCondition.Instance;
		Settings = new LevelSettings();
		CustomData = new XDataHolder();
		Init();
	}

	public void StopProgressEvent(byte[] stopBytes)
	{
		logicEventData.Add(stopBytes);
		logicEventDataSize += stopBytes.Length;
	}

	public void ExecuteLogicData(byte[] logicData, float timeCorrection)
	{
		int num = 0;
		List<Machine> list = new List<Machine>();
		logicData = CLZF2.Decompress(logicData);
		uint frame = BitConverter.ToUInt32(logicData, num);
		num += 4;
		int count;
		num += NetworkCompression.UnpackUInt(logicData, num, false, out count);
		for (int i = 0; i < count; i++)
		{
			bool flag = logicData[num] == 0;
			num++;
			long id = BitConverter.ToInt64(logicData, num);
			num += LevelEntity.ID_LENGTH;
			int count2;
			num += NetworkCompression.UnpackUInt(logicData, num, false, out count2);
			int count3;
			num += NetworkCompression.UnpackUInt(logicData, num, false, out count3);
			LevelEntity entity;
			EntityLogic logic;
			EntityEvent evt;
			GenericEntity behaviour;
			if (flag)
			{
				int count4;
				num += NetworkCompression.UnpackUInt(logicData, num, false, out count4);
				byte[] array = new byte[count4];
				Buffer.BlockCopy(logicData, num, array, 0, count4);
				num += count4;
				bool flag2 = logicData[num] == 1;
				num++;
				list.Clear();
				bool hasMachineList = false;
				if (flag2)
				{
					int num2 = logicData[num];
					num++;
					if (num2 > 0)
					{
						hasMachineList = true;
						for (int j = 0; j < num2; j++)
						{
							ServerMachine machine;
							if (networkScene.GetMachine(NetworkCompression.ReadUInt16(logicData, num), out machine))
							{
								list.Add(machine);
							}
							num += 2;
						}
					}
				}
				if (!Get(id, out entity))
				{
					continue;
				}
				if (!entity.isStatic)
				{
					if (entity.simEntity == null)
					{
						continue;
					}
					entity = entity.simEntity;
				}
				behaviour = entity.behaviour;
				if (behaviour.GetLogic(count2, out logic, entity.isStatic) && logic.GetEvent((ushort)count3, out evt))
				{
					OnExecuteEvent(behaviour, logic, evt, frame, count4 > 0, array, hasMachineList, list, timeCorrection);
				}
				continue;
			}
			float progress = (float)(int)NetworkCompression.ReadUInt16(logicData, num) / 65535f;
			num += 2;
			if (!Get(id, out entity))
			{
				continue;
			}
			if (!entity.isStatic)
			{
				if (entity.simEntity == null)
				{
					Debug.Log("Couldn't fire logic event, " + entity.behaviour.prefab.name + " (" + entity.behaviour.prefab.ID + ") doesn't have a sim block!");
					continue;
				}
				entity = entity.simEntity;
			}
			behaviour = entity.behaviour;
			if (behaviour.GetLogic(count2, out logic, entity.isStatic) && logic.GetEvent((ushort)count3, out evt) && evt.eventData.IsProgressEvent())
			{
				level.StopProgressEvent(logic, evt, progress);
			}
		}
	}

	private void OnExecuteEvent(GenericEntity entityBehaviour, EntityLogic logic, EntityEvent evt)
	{
		OnExecuteEvent(entityBehaviour, logic, evt, addPiece.frame, false, null, false, null, 0f);
	}

	private List<MPTeam> GetWinTeamsFromEvent(EventContainer.GameWinEvent winEvent)
	{
		float[] array = new float[ReferenceMaster.Instance.teamColors.Length];
		int[] array2 = new int[array.Length];
		float num = 0f;
		bool flag = false;
		for (int i = 0; i < array.Length; i++)
		{
			float num2 = 0f;
			MPTeam mPTeam = (MPTeam)i;
			int num3 = 0;
			for (int j = 0; j < Playerlist.Players.Count; j++)
			{
				PlayerData playerData = Playerlist.Players[j];
				if (playerData.isSpectator || playerData.team != mPTeam || !playerData.machine.isSimulating || !playerData.machine.SimPhysics)
				{
					continue;
				}
				if (winEvent.winType == EventContainer.GameWinEvent.WinType.Variable)
				{
					if (!playerData.buildZone.hasSpawnZone)
					{
						continue;
					}
					float val;
					if (!playerData.buildZone.spawnZone.GetVariableValue(winEvent.varName, out val))
					{
						val = 0f;
					}
					num2 += val;
				}
				else if (winEvent.winType == EventContainer.GameWinEvent.WinType.Health && playerData.machine.registerDamage)
				{
					num2 += playerData.machine.Health;
				}
				num3++;
			}
			if (num3 > 0)
			{
				if (winEvent.winType == EventContainer.GameWinEvent.WinType.Progress)
				{
					num2 = winCondition.GetTeamProgress(mPTeam);
				}
				else if (winEvent.winType == EventContainer.GameWinEvent.WinType.Health)
				{
					num2 /= (float)num3;
				}
			}
			array2[i] = num3;
			array[i] = num2;
			if (!flag || num2 > num)
			{
				num = num2;
				flag = true;
			}
		}
		List<MPTeam> list = new List<MPTeam>();
		for (int i = 0; i < array.Length; i++)
		{
			if (array2[i] > 0 && Mathf.Approximately(array[i], num))
			{
				MPTeam mPTeam = (MPTeam)i;
				list.Add(mPTeam);
			}
		}
		if (list.Count == 0)
		{
			list.Add(MPTeam.None);
		}
		return list;
	}

	private void OnExecuteWinEvent(EventContainer.GameWinEvent winEvent)
	{
		if (winCondition.HasWinEvent)
		{
			Debug.Log("Ignoring win event, we already had one");
			return;
		}
		CustomLevel.Instance.DeactivateEntityWithEventType(EventContainer.EventType.GameWin);
		List<MPTeam> winTeamsFromEvent = GetWinTeamsFromEvent(winEvent);
		OnWinEvent(winTeamsFromEvent);
	}

	public void OnWinEvent(byte[] data)
	{
		if (StatMaster.levelSimulating && !StatMaster.isLocalSim)
		{
			List<MPTeam> list = new List<MPTeam>();
			for (int i = 0; i < data.Length; i++)
			{
				list.Add((MPTeam)data[i]);
			}
			OnWinEvent(list);
		}
	}

	private void OnWinEvent(List<MPTeam> winningTeams)
	{
		winCondition.OnWinEvent(winningTeams);
		if (StatMaster.isHosting && !StatMaster.isLocalSim)
		{
			byte[] array = new byte[winningTeams.Count];
			for (int i = 0; i < winningTeams.Count; i++)
			{
				array[i] = (byte)winningTeams[i];
			}
			auxAddPiece.SendNetworkMessage(RPCMessageType.GameWin, array);
		}
	}

	private bool GetSimEntity(long id, out LevelEntity entity)
	{
		entity = null;
		if (id == LevelPrefab.INVALID_ID || !Get(id, out entity))
		{
			return false;
		}
		if (!entity.isStatic && entity.simEntity != null)
		{
			entity = entity.simEntity;
		}
		return true;
	}

	private void OnExecuteEvent(GenericEntity entityBehaviour, EntityLogic logic, EntityEvent evt, uint frame, bool hasEventData, byte[] eventData, bool hasMachineList, List<Machine> machineList, float timeCorrection)
	{
		EventContainer.EventType eventType = evt.eventType;
		bool flag = false;
		bool flag2 = EventContainer.IsMachineEvent(eventType);
		if (!hasMachineList && flag2)
		{
			machineList = new List<Machine>();
		}
		SingleInstance<Events>.Instance.EventExecuted(logic, evt);
		switch (eventType)
		{
		case EventContainer.EventType.GameWin:
			OnExecuteWinEvent(evt.eventData as EventContainer.GameWinEvent);
			break;
		case EventContainer.EventType.Wait:
			flag = (evt.eventData as EventContainer.WaitEvent).displayCountDown;
			break;
		case EventContainer.EventType.Progress:
		{
			EventContainer.LevelProgressEvent levelProgressEvent = evt.eventData as EventContainer.LevelProgressEvent;
			winCondition.AddProgress(evt.team, levelProgressEvent.progress);
			flag = true;
			break;
		}
		case EventContainer.EventType.Modded:
		{
			ModdedEventContainer eventContainer = evt.eventData as ModdedEventContainer;
			SingleInstanceFindOnly<EventLoader>.Instance.ExecuteEvent(eventContainer, logic);
			break;
		}
		default:
		{
			if (eventType == EventContainer.EventType.Random)
			{
				EventContainer.RandomEvent randomEvent = evt.eventData as EventContainer.RandomEvent;
				randomEvent.randomVal = UnityEngine.Random.Range(randomEvent.min, randomEvent.max + ((randomEvent.max > 0) ? 1 : (-1)));
			}
			List<LevelEntity> list = new List<LevelEntity>();
			LevelEntity entity;
			for (int i = 0; i < evt.entityList.Count; i++)
			{
				long id = evt.entityList[i];
				if (GetSimEntity(id, out entity))
				{
					list.Add(entity);
				}
			}
			bool flag3 = false;
			if (!hasMachineList)
			{
				flag3 = true;
				if (flag2 && logic.UseTriggerResult(evt, true))
				{
					if ((logic.lastResult == InsigniaTrigger.TriggerResult.Block || logic.lastResult == InsigniaTrigger.TriggerResult.Projectile) && logic.lastResultObject != null && logic.lastResultObject.HasParentMachine)
					{
						Machine parentMachine = logic.lastResultObject.ParentMachine;
						machineList.Add(parentMachine);
						flag = true;
					}
				}
				else if (logic.UseSelf(evt))
				{
					if (logic.IsVarEvent(evt.eventType))
					{
						if (evt.eventType == EventContainer.EventType.Variable)
						{
							EventContainer.VariableEvent variableEvent = evt.eventData as EventContainer.VariableEvent;
							level.SetVariable(variableEvent.key, variableEvent.modifyType, variableEvent.val);
						}
						else if (evt.eventType == EventContainer.EventType.Random)
						{
							EventContainer.RandomEvent randomEvent2 = evt.eventData as EventContainer.RandomEvent;
							level.SetVariable(randomEvent2.key, randomEvent2.modifyType, randomEvent2.randomVal);
						}
						flag3 = false;
					}
					else if (entityBehaviour.entity != null && GetSimEntity(entityBehaviour.entity.identifier, out entity))
					{
						list.Add(entity);
					}
				}
				if (evt.eventType == EventContainer.EventType.Transform)
				{
					EventContainer.TransformEvent transformEvent = evt.eventData as EventContainer.TransformEvent;
					transformEvent.Init(list.Count);
				}
				if (flag3)
				{
					if (hasEventData)
					{
						evt.eventData.DecodeEventData(eventData, 0);
					}
					for (int i = 0; i < list.Count; i++)
					{
						entity = list[i];
						if (flag2)
						{
							BuildZoneObject buildZoneObject = entity.behaviour as BuildZoneObject;
							if (buildZoneObject.hasZone)
							{
								ServerMachine machine = buildZoneObject.buildZone.player.machine;
								if (!machineList.Contains(machine) && machine.isSimulating && machine.SimPhysics && (!machine.isRespawning || eventType != EventContainer.EventType.RespawnMachine))
								{
									machineList.Add(machine);
									flag = true;
								}
							}
							continue;
						}
						flag = true;
						switch (eventType)
						{
						case EventContainer.EventType.Activate:
							entity.ActivateEntity(frame);
							break;
						case EventContainer.EventType.Deactivate:
							entity.DeactivateEntity();
							break;
						case EventContainer.EventType.Reset:
							if (GetSimEntity(entity.identifier, out entity))
							{
								entity.ResetEntity(frame);
							}
							break;
						case EventContainer.EventType.Variable:
						{
							EventContainer.VariableEvent variableEvent2 = evt.eventData as EventContainer.VariableEvent;
							entity.behaviour.SetVariable(variableEvent2.key, variableEvent2.modifyType, variableEvent2.val);
							flag = false;
							break;
						}
						case EventContainer.EventType.Random:
						{
							EventContainer.RandomEvent randomEvent3 = evt.eventData as EventContainer.RandomEvent;
							entity.behaviour.SetVariable(randomEvent3.key, randomEvent3.modifyType, randomEvent3.randomVal);
							flag = false;
							break;
						}
						case EventContainer.EventType.Transform:
						{
							EventContainer.TransformEvent transformEvent2 = evt.eventData as EventContainer.TransformEvent;
							transformEvent2.entityList[i].Setup(entity, transformEvent2, StatMaster.isHosting || StatMaster.isLocalSim);
							break;
						}
						}
					}
				}
			}
			else if (hasEventData)
			{
				evt.eventData.DecodeEventData(eventData, 0);
			}
			if (!flag2)
			{
				break;
			}
			switch (eventType)
			{
			case EventContainer.EventType.ReloadMachine:
			{
				EventContainer.ReloadMachineEvent reloadMachineEvent = evt.eventData as EventContainer.ReloadMachineEvent;
				if (reloadMachineEvent.ammoType == ReloadAmmoType.Random)
				{
					if (StatMaster.isHosting)
					{
						reloadMachineEvent.lastAmmoType = (ReloadAmmoType)UnityEngine.Random.Range(1, 4);
					}
				}
				else
				{
					reloadMachineEvent.lastAmmoType = reloadMachineEvent.ammoType;
				}
				for (int k = 0; k < machineList.Count; k++)
				{
					ServerMachine serverMachine = machineList[k] as ServerMachine;
					serverMachine.ReloadAmmo((int)reloadMachineEvent.reloadValue, reloadMachineEvent.lastAmmoType, reloadMachineEvent.setAmmo, reloadMachineEvent.eachBlock);
					flag = true;
				}
				break;
			}
			case EventContainer.EventType.RespawnMachine:
				if (machineList.Count > 0)
				{
					addPiece.StartCoroutine(addPiece.RespawnMachines(machineList));
				}
				break;
			case EventContainer.EventType.SetRespawn:
			{
				if (machineList.Count <= 0)
				{
					break;
				}
				EventContainer.SetRespawnEvent setRespawnEvent = evt.eventData as EventContainer.SetRespawnEvent;
				for (int j = 0; j < machineList.Count; j++)
				{
					LevelEntity entity2;
					if (setRespawnEvent.zoneTarget != LevelPrefab.UNASSIGNED_ID && GetSimEntity(setRespawnEvent.zoneTarget, out entity2))
					{
						Transform transform = entity2.transform;
						ServerMachine serverMachine = machineList[j] as ServerMachine;
						serverMachine.SpawnTransform.position = transform.position;
						serverMachine.SpawnTransform.rotation = transform.rotation;
					}
				}
				break;
			}
			}
			break;
		}
		}
		if (StatMaster.isClient && !StatMaster.isLocalSim && evt.eventData.IsProgressEvent())
		{
			level.StartProgressEvent(logic, evt, 0f, timeCorrection);
		}
		if (!flag || !StatMaster.isHosting || StatMaster.isLocalSim)
		{
			return;
		}
		int iD_LENGTH = LevelEntity.ID_LENGTH;
		int num = evt.eventData.EventDataSize();
		int num2 = NetworkCompression.PackedUIntLength(num, false);
		int num3 = NetworkCompression.PackedUIntLength(logic.ID, false);
		int num4 = NetworkCompression.PackedUIntLength(evt.ID, false);
		byte[] array = new byte[1 + iD_LENGTH + num3 + num4 + num2 + num + 1 + (flag2 ? (1 + machineList.Count * 2) : 0)];
		int num5 = 0;
		array[num5] = 0;
		num5++;
		Buffer.BlockCopy(entityBehaviour.GetIdentifierBytes(), 0, array, num5, iD_LENGTH);
		num5 += iD_LENGTH;
		NetworkCompression.PackUInt(logic.ID, array, num5, false, num3);
		num5 += num3;
		NetworkCompression.PackUInt(evt.ID, array, num5, false, num4);
		num5 += num4;
		NetworkCompression.PackUInt(num, array, num5, false, num2);
		num5 += num2;
		if (num > 0)
		{
			evt.eventData.EncodeEventData(array, num5);
			num5 += num;
		}
		array[num5] = (byte)(flag2 ? 1u : 0u);
		num5++;
		if (flag2)
		{
			array[num5] = (byte)machineList.Count;
			num5++;
			for (int l = 0; l < machineList.Count; l++)
			{
				NetworkCompression.WriteUInt16(machineList[l].PlayerID, array, num5);
				num5 += 2;
			}
		}
		logicEventData.Add(array);
		logicEventDataSize += array.Length;
	}

	public void UpdatePlayerStates()
	{
		if (StatMaster.Mode.levelEdit)
		{
			return;
		}
		byte[] array = new byte[2];
		if (!OptionsMaster.allowExcessPlayers || auxAddPiece.PlayersLimited)
		{
			array[0] = 1;
			for (int num = Playerlist.Players.Count - 1; num >= 0; num--)
			{
				PlayerData playerData = Playerlist.Players[num];
				if (!playerData.isSpectator && !playerData.buildZone.hasSpawnZone && auxAddPiece.PlayersLimited)
				{
					auxAddPiece.ToggleSpectator(playerData.networkId, array);
					auxAddPiece.SendPlayerMessage(playerData.networkId, RPCMessageType.PlayerLimitSpectator);
					playerData.wantSpectator = false;
				}
			}
		}
		array[0] = 0;
		for (int num = 0; num < Playerlist.Players.Count; num++)
		{
			if (auxAddPiece.PlayersLimited)
			{
				break;
			}
			PlayerData playerData = Playerlist.Players[num];
			if (playerData.isSpectator && !playerData.wantSpectator && (OptionsMaster.allowExcessPlayers || auxAddPiece.HasNextZone()))
			{
				array[0] = 0;
				auxAddPiece.ToggleSpectator(playerData.networkId, array);
			}
		}
	}

	public void SetUIState(LevelEditorUI.UIState mode)
	{
		editorUI.SetUIState(mode);
	}

	public void OnDestroy()
	{
		FileBrowserView obj = fileBrowserView;
		obj.ViewToggled = (Action<bool>)Delegate.Remove(obj.ViewToggled, new Action<bool>(OnFileBrowserViewToggled));
		if (!quitting)
		{
			ReferenceMaster.ResetEditor -= ResetWindow;
		}
	}

	protected void OnApplicationQuit()
	{
		quitting = true;
	}

	public void Init()
	{
		if (!isInitialized)
		{
			FileBrowserView obj = fileBrowserView;
			obj.ViewToggled = (Action<bool>)Delegate.Combine(obj.ViewToggled, new Action<bool>(OnFileBrowserViewToggled));
			GameObject gameObject = GameObject.Find("FloorBig");
			Transform floorTransform = gameObject.transform;
			LevelXMLSaver.Init(floorTransform, winCondition);
			level = CustomLevel.Instance;
			selectionController.Init(this);
			isInitialized = true;
		}
	}

	private void OnFileBrowserViewToggled(bool isOpened)
	{
		if (FileBrowserToggled != null)
		{
			FileBrowserToggled(isOpened);
		}
	}

	public static void ResetLevelSettings()
	{
		OptionsMaster.allowExcessPlayers = (StatMaster.Mode.allowClone = true);
		bool limitPlayers = (StatMaster.Rules.DisableExplosions = false);
		OptionsMaster.votingEnabled = (OptionsMaster.limitPlayers = limitPlayers);
		StatMaster.Rules.DisableProjectiles = (StatMaster.limitMachines = (StatMaster.Mode.curtainMode = (StatMaster.Mode.hideLabels = false)));
	}

	public void UpdateLevelSettings(LevelSettings settings)
	{
		Settings = settings;
		bool levelEdit = StatMaster.Mode.levelEdit;
		OptionsMaster.allowExcessPlayers = levelEdit || settings.AllowExcessPlayers;
		StatMaster.Mode.allowClone = levelEdit || settings.AllowCopyMachine;
		OptionsMaster.votingEnabled = !levelEdit && settings.UseVoting;
		OptionsMaster.limitPlayers = !levelEdit && settings.MaxPlayers != -1;
		StatMaster.Rules.DisableExplosions = !levelEdit && settings.IsRuleEnabled(ReferenceMaster.Instance.godPowers[1]);
		StatMaster.Rules.DisableProjectiles = !levelEdit && settings.IsRuleEnabled(ReferenceMaster.Instance.godPowers[2]);
		StatMaster.limitMachines = !levelEdit && settings.AllowedMachines.Count > 0;
		StatMaster.Mode.curtainMode = !levelEdit && settings.CurtainMode;
		StatMaster.Mode.hideLabels = !levelEdit && settings.HidePlayerLabels;
		level.PlayTrack(settings.MusicID, settings.MusicVolume);
		addPiece.OnUpdateLevelSettings(settings);
		hud.OnUpdateLevelSettings(settings);
		environmentManager.SetEnvironment(settings.Environment);
		if (WaterController.Exist)
		{
			UpdateWaterHeight((float)settings.WaterHeight * 0.1f);
			if (settings.Environment == LevelSettings.LevelEnvironment.Water)
			{
				UpdateEnvironmentType(settings.EnvType);
			}
		}
		if (LevelSettingsChanged != null)
		{
			LevelSettingsChanged(settings);
		}
	}

	public void OnUpdateSettings(ServerSettings settings)
	{
		StatMaster.Mode.levelEdit = settings.levelEditor;
		addPiece.OnUpdateSettings(settings);
		SingleInstanceFindOnly<LevelEditorUI>.Instance.OnUpdateSettings(settings);
	}

	public void SetOwner(ushort owner)
	{
		ownerId = owner;
		entityController.SetOwner(owner);
		ghostManager.Init(ghostMaterial);
		PlayerJoin(owner);
		if (inPlaceMode)
		{
			ghostManager.SetPrefab(owner, ActiveObjectBrush);
		}
	}

	public LevelPrefab GetGhostPrefab(ushort playerId)
	{
		return ghostManager.GetPrefab(playerId);
	}

	public void PlayerJoin(ushort playerId)
	{
		ghostManager.CreateGhost(playerId, playerId == ownerId);
	}

	public void PlayerLeave(ushort playerId)
	{
		if (!(ghostManager == null))
		{
			if (playerId == ownerId)
			{
				ghostManager.Clear();
				PlayerJoin(ownerId);
			}
			else
			{
				ghostManager.RemoveGhost(playerId);
			}
		}
	}

	public void ToggleGhost(ushort playerId, byte[] data)
	{
		ghostManager.Toggle(playerId, data);
	}

	public void UpdateGhost(ushort playerId, byte[] data, int offset)
	{
		ghostManager.UpdateGhost(playerId, data, offset);
	}

	public void Reset()
	{
		ghostManager.Clear();
		ClearLevel();
		ResetWindow();
	}

	public void ResetWindow()
	{
		if (CurrentState != StatMaster.Tool.None)
		{
			SetActiveTool(StatMaster.Tool.None);
		}
		if (inPlaceMode)
		{
			SetPrefab(null);
		}
		if (CurrentState != StatMaster.Tool.None)
		{
			SetActiveTool(StatMaster.Tool.None);
		}
	}

	public void ClearLevel()
	{
		BlockMapper currentInstance = BlockMapper.CurrentInstance;
		if (currentInstance != null && !currentInstance.IsBlock)
		{
			BlockMapper.Close();
		}
		isDirty = false;
		if (CalmZoneController.lastInstance != null)
		{
			CalmZoneController.lastInstance.Reset();
		}
		level.ResetLevel();
		if (StatMaster.levelSimulating)
		{
			addPiece.ResetMapperTargets();
			addPiece.ToggleLevelSimulation(false, true);
		}
		UpdateLevelSettings(new LevelSettings());
		CustomData = new XDataHolder();
		MouseOrbit instance = SingleInstanceFindOnly<MouseOrbit>.Instance;
		if (instance != null && instance.targetType == MouseOrbit.TargetType.Entity)
		{
			instance.ResetCamTarget();
		}
		LevelUndoSystem.Reset();
		selectionController.Clear();
		entityController.Clear();
		if (StatMaster.isHosting)
		{
			auxAddPiece.ClearSpawns();
		}
		SingleInstance<Events>.Instance.LevelDeleted();
	}

	public void ToggleSimulation(bool toggle)
	{
		if (StatMaster.levelSimulating == toggle)
		{
			return;
		}
		if (toggle)
		{
			lastTool = CurrentState;
			if (IsTransformTool(lastTool))
			{
				lastSelection = new List<LevelEntity>(selectionController.LevelSelection);
			}
			addPiece.CloseEntityMapper();
			SetActiveTool(StatMaster.Tool.None, false);
			base.enabled = false;
			selectionController.enabled = false;
			ghostManager.Toggle(ownerId, false, Vector3.zero);
		}
		else
		{
			logicEventData.Clear();
			logicEventDataSize = 0;
		}
		winCondition.ResetProgress();
		level.ToggleSim(toggle);
		if (hud.clearLevelWarning.activeInHierarchy)
		{
			hud.clearLevelWarning.SetActive(false);
		}
		addPiece.UpdatePlayIcon();
		if (ReferenceMaster.onLevelSimulation != null)
		{
			ReferenceMaster.onLevelSimulation(toggle);
		}
		if (toggle)
		{
			if (StatMaster.Mode.levelEdit)
			{
				selectionController.Hide();
				if (!PlayerData.hasLocalPlayer || PlayerData.localPlayer.isSpectator || !PlayerData.localPlayer.machine.isSimulating)
				{
					editorUI.SetUIState(LevelEditorUI.UIState.Simulating);
				}
			}
			else if (StatMaster.isClient && OptionsMaster.votingEnabled)
			{
				auxAddPiece.HideLoadingText();
				if (fileBrowserView.IsOpen)
				{
					fileBrowserView.Close();
				}
			}
			return;
		}
		WinCondition.timeTaken = 0f;
		base.enabled = true;
		selectionController.enabled = true;
		if (!StatMaster.Mode.levelEdit)
		{
			return;
		}
		editorUI.SetUIState(LevelEditorUI.UIState.BuildMode);
		if (lastTool == StatMaster.Tool.None)
		{
			return;
		}
		SetActiveTool(lastTool);
		if (IsTransformTool(lastTool))
		{
			List<LevelEntity> list = new List<LevelEntity>();
			for (int i = 0; i < lastSelection.Count; i++)
			{
				LevelEntity levelEntity = lastSelection[i];
				if ((bool)levelEntity && levelEntity.transform.parent != null)
				{
					list.Add(levelEntity);
				}
			}
			selectionController.Select(list, false, false);
		}
		else if (lastTool == StatMaster.Tool.Modify)
		{
			addPiece.ReopenEntityMapper();
		}
	}

	public void CloseLoadSaveScreen()
	{
		if (fileBrowserView.IsOpen)
		{
			fileBrowserView.Close();
		}
	}

	public void OpenLoadLevelWindow()
	{
		fileBrowserView.Open(FileBrowserType.LocalLevels, false);
	}

	public void Undo()
	{
		if (!StatMaster.levelSimulating && !StatMaster.waitingForServerResponse)
		{
			LevelUndoSystem.Undo();
			UpdateEditingLevel();
		}
	}

	public void Redo()
	{
		if (!StatMaster.levelSimulating && !StatMaster.waitingForServerResponse)
		{
			LevelUndoSystem.Redo();
			UpdateEditingLevel();
		}
	}

	public void OnClearLevelClicked()
	{
		hud.OnClearLevel();
	}

	public void AssignSpawnZones()
	{
		BuildZoneObject zoneObj = null;
		int num = 0;
		for (int i = 0; i < Playerlist.Players.Count; i++)
		{
			PlayerData playerData = Playerlist.Players[i];
			if (!playerData.isSpectator && !playerData.buildZone.hasSpawnZone)
			{
				if (StatMaster.Mode.levelEdit && !auxAddPiece.HasNextZone())
				{
					Transform zoneTransform = playerData.buildZone.zoneTransform;
					AddEntity(BUILD_ZONE_ID, zoneTransform.position, zoneTransform.rotation, Vector3.one, false);
				}
				if (!auxAddPiece.GetNextZone(out zoneObj))
				{
					break;
				}
				zoneObj.SetBuildZone(playerData.buildZone, true);
				num++;
				if (OptionsMaster.limitPlayers && num == Settings.MaxPlayers)
				{
					break;
				}
			}
		}
	}

	public void ToggleClientSimControl(bool toggle)
	{
		editorUI.options.UpdateClientSimControl(toggle);
	}

	public void OnClearLevel(bool isRemote)
	{
		if (StatMaster.isClient && !isRemote)
		{
			if (!StatMaster.waitingForServerResponse)
			{
				auxAddPiece.SendServerMessage(RPCMessageType.ClearLevel);
			}
		}
		else
		{
			if (StatMaster.isHosting && !isActive && isRemote)
			{
				return;
			}
			ClearLevel();
			UpdateEditingLevel();
			if (!StatMaster.isHosting)
			{
				return;
			}
			auxAddPiece.SendNetworkMessage(RPCMessageType.ClearLevel);
			int num = 0;
			for (int i = 0; i < Playerlist.Players.Count; i++)
			{
				PlayerData playerData = Playerlist.Players[i];
				if (!playerData.isSpectator)
				{
					AddEntity(BUILD_ZONE_ID, auxAddPiece.GetZonePosition(num++), Quaternion.identity, Vector3.one, false);
				}
			}
			AssignSpawnZones();
		}
	}

	public void ToggleGlobal()
	{
		if (IsTransformTool(CurrentState))
		{
			UpdateTool();
		}
	}

	public void ResetGhostTransform()
	{
		bool flag = ActiveObjectBrush != null;
		Vector3 vector = ((!flag) ? Vector3.one : ActiveObjectBrush.placementScale);
		if (StatMaster.Mode.LevelEditor.paintPlacement)
		{
			ghostRotY = UnityEngine.Random.Range(StatMaster.Mode.LevelEditor.minRandomRot, StatMaster.Mode.LevelEditor.maxRandomRot);
			ghostScale = new Vector3(UnityEngine.Random.Range(StatMaster.Mode.LevelEditor.minRandomScaleX * vector.x, StatMaster.Mode.LevelEditor.maxRandomScaleX * vector.x), UnityEngine.Random.Range(StatMaster.Mode.LevelEditor.minRandomScaleY * vector.y, StatMaster.Mode.LevelEditor.maxRandomScaleY * vector.y), UnityEngine.Random.Range(StatMaster.Mode.LevelEditor.minRandomScaleZ * vector.z, StatMaster.Mode.LevelEditor.maxRandomScaleZ * vector.z));
		}
		else
		{
			ghostRotY = ((!flag) ? 0f : ActiveObjectBrush.rotation.y);
			ghostScale = vector;
		}
	}

	public void TogglePivot()
	{
		if (IsTransformTool(CurrentState))
		{
			UpdateTool();
		}
	}

	public void RemoteUpdate(ushort playerId, long id, byte[] data, int offset)
	{
		LevelEntity entity;
		if (!entityController.Get(id, out entity))
		{
			Debug.LogWarning("LevelEditor::RemoteUpdate(): Couldn't find entity: " + id + "!");
			return;
		}
		entity.SetEntityData(playerId, data, offset);
		if (playerId != ownerId)
		{
			OnSelectionUpdate();
		}
	}

	public void UpdateEntities(ushort playerId, byte[] data)
	{
		entityController.SetTransformData(playerId, data);
	}

	public void ToggleTool(StatMaster.Tool option)
	{
		if (!StatMaster.levelSimulating)
		{
			StatMaster.Tool activeTool = ((SelectionCount != 0 || StatMaster.Mode.LevelEditor.selectedTool != option) ? option : StatMaster.Tool.None);
			SetActiveTool(activeTool);
		}
	}

	public void SyncLogicData(uint frame)
	{
		if (logicEventDataSize != 0)
		{
			int num = NetworkCompression.PackedUIntLength(logicEventData.Count, false);
			byte[] array = new byte[4 + num + logicEventDataSize];
			int num2 = 0;
			NetworkCompression.WriteUInt(addPiece.frame, false, array, num2);
			num2 += 4;
			NetworkCompression.PackUInt(logicEventData.Count, array, num2, false, num);
			num2 += num;
			NetworkCompression.WriteArray(logicEventData, array, num2);
			auxAddPiece.SyncLogicData(CLZF2.Compress(array));
			logicEventData.Clear();
			logicEventDataSize = 0;
			level.logicFrame++;
		}
	}

	protected void LateUpdate()
	{
		float unscaledDeltaTime = Time.unscaledDeltaTime;
		lastSent += unscaledDeltaTime;
		float sendRate = NetworkScene.ServerSettings.sendRate;
		if (lastSent > sendRate)
		{
			while (lastSent >= sendRate)
			{
				lastSent -= sendRate;
			}
			entityController.UpdateEntities();
		}
		entityController.UpdateLevelEntities(unscaledDeltaTime);
		if (StatMaster.isHeadless)
		{
			return;
		}
		if (StatMaster.Mode.levelEdit && !StatMaster.inMenu && editorUI.IsOpen)
		{
			if (InputManager.LevelEditor.MoveToolKey())
			{
				ToggleTool(StatMaster.Tool.Translate);
			}
			else if (InputManager.LevelEditor.RotateToolKey())
			{
				ToggleTool(StatMaster.Tool.Rotate);
			}
			else if (InputManager.LevelEditor.ScaleToolKey())
			{
				ToggleTool(StatMaster.Tool.Scale);
			}
			else if (InputManager.LevelEditor.MirrorToolKey())
			{
				ToggleTool(StatMaster.Tool.Mirror);
			}
			else if (InputManager.LevelEditor.ModifyToolKey())
			{
				ToggleTool(StatMaster.Tool.Modify);
			}
			else if (InputManager.LevelEditor.BrushModeKey())
			{
				StatMaster.Mode.LevelEditor.paintPlacement = !StatMaster.Mode.LevelEditor.paintPlacement;
				SingleInstanceFindOnly<LevelEditorUI>.Instance.options.Paint.BG.SetActive(StatMaster.Mode.LevelEditor.paintPlacement);
				ResetGhostTransform();
			}
		}
		if (!StatMaster.Mode.levelEdit || !AddPiece.isEditingLevel || StatMaster.ToolActive)
		{
			ghostManager.Toggle(ownerId, false, Vector3.zero);
			return;
		}
		if (updatedSelection)
		{
			lastSelectUpdate += Time.deltaTime;
			if (lastSelectUpdate > updateSelectionInterval)
			{
				SendPlayerSelection();
				updatedSelection = false;
			}
		}
		bool flag = InputManager.LevelEditor.LeftShiftKey();
		bool flag2 = InputManager.LevelEditor.LeftCtrlKey();
		bool flag3 = flag;
		bool flag4 = false;
		bool flag5 = InputManager.LeftMouseButton();
		bool flag6 = InputManager.LeftMouseButtonReleased();
		bool flag7 = InputManager.LeftMouseButtonHeld();
		if (flag5 || flag6)
		{
			currentMouseDownTime = 0f;
		}
		else if (flag7)
		{
			currentMouseDownTime += Time.deltaTime;
		}
		bool flag8 = CurrentState == StatMaster.Tool.Erase;
		bool flag9 = InputManager.DeleteKeyHeld() || (flag8 && flag7 && currentMouseDownTime > deleteMouseDownTime);
		bool flag10 = InputManager.DeleteKey();
		bool flag11 = flag8 || flag10 || flag9;
		bool flag12 = CurrentState == StatMaster.Tool.Modify;
		bool flag13 = CurrentState == StatMaster.Tool.Translate || CurrentState == StatMaster.Tool.Rotate || CurrentState == StatMaster.Tool.Scale || CurrentState == StatMaster.Tool.Mirror;
		bool flag14 = flag12 && flag5;
		bool flag15 = selectionController.Count > 0;
		bool flag16 = inPlaceMode;
		Vector2 vector = Input.mousePosition;
		bool flag17 = flag11 || flag14 || flag5 || flag16;
		if (!StatMaster.hudOccluding && flag17)
		{
			GameObject gameObject = null;
			LevelEntity levelEntity = null;
			bool flag18 = false;
			lastRay = Camera.main.ScreenPointToRay(vector);
			bool flag19 = false;
			Vector3 vector2 = Vector3.zero;
			bool flag20 = StatMaster.Mode.pickMode != StatMaster.Mode.PickMode.None;
			RaycastHit[] array = Physics.RaycastAll(lastRay, 1000f, (!flag20) ? ReferenceMaster.Instance.levelEditorMask : ReferenceMaster.Instance.editorPickMask, QueryTriggerInteraction.Collide);
			List<HoverEntry> list = new List<HoverEntry>();
			bool flag21 = false;
			if (array.Length > 0)
			{
				Vector3 position = Camera.main.transform.position;
				for (int i = 0; i < array.Length; i++)
				{
					RaycastHit h = array[i];
					if (h.collider.gameObject.layer == gizmoLayer)
					{
						flag19 = true;
						break;
					}
					LevelEntity levelEntity2 = h.collider.GetComponentInParent<LevelEntity>();
					bool flag22 = levelEntity2 != null;
					if (flag22)
					{
						if (levelEntity2.isSimulating)
						{
							continue;
						}
						if (levelEntity2.hasBase)
						{
							levelEntity2 = levelEntity2.baseEntity as LevelEntity;
							if (levelEntity2.hasBase)
							{
								levelEntity2 = levelEntity2.baseEntity as LevelEntity;
							}
						}
						if (levelEntity2.behaviour == null)
						{
							continue;
						}
						LevelPrefab prefab = levelEntity2.behaviour.prefab;
						if ((flag16 && prefab.ignoreInPlaceMode) || (StatMaster.Mode.pickMode == StatMaster.Mode.PickMode.Entity && !prefab.canPick) || (StatMaster.Mode.pickMode == StatMaster.Mode.PickMode.Zone && prefab.ID != BUILD_ZONE_ID) || (flag11 && !levelEntity2.CanRemove))
						{
							continue;
						}
					}
					if (h.collider.name.Equals("ICE FREEZE"))
					{
						continue;
					}
					float sqrMagnitude = (h.point - position).sqrMagnitude;
					bool flag23 = false;
					if (flag22)
					{
						for (int j = 0; j < list.Count; j++)
						{
							HoverEntry hoverEntry = list[j];
							if (hoverEntry.isEntity && hoverEntry.entity.identifier == levelEntity2.identifier)
							{
								if (sqrMagnitude < hoverEntry.sqrDist)
								{
									list.Remove(hoverEntry);
								}
								else
								{
									flag23 = true;
								}
								break;
							}
						}
					}
					if (!flag23)
					{
						int k;
						for (k = 0; k < list.Count && sqrMagnitude >= list[k].sqrDist; k++)
						{
						}
						list.Insert(k, new HoverEntry(levelEntity2, h.collider.gameObject, h, sqrMagnitude, flag22));
					}
				}
				bool flag24 = list.Count > 0;
				if (flag20 && !flag24)
				{
					return;
				}
				if (flag24 && !flag19)
				{
					int index = 0;
					float sqrMagnitude2 = ((lastClickPoint - vector) / Screen.height).sqrMagnitude;
					if (!flag3 && !flag20 && (flag14 || selectionController.Count == 1))
					{
						for (int j = 0; j < list.Count; j++)
						{
							HoverEntry hoverEntry2 = list[j];
							if (!hoverEntry2.isEntity)
							{
								continue;
							}
							if (sqrMagnitude2 > 0.001f)
							{
								index = j;
								break;
							}
							if (!hoverEntry2.entity.IsSelected)
							{
								continue;
							}
							for (int l = j + 1; l < list.Count; l++)
							{
								HoverEntry hoverEntry3 = list[l];
								if (hoverEntry3.isEntity)
								{
									index = l;
									break;
								}
							}
							break;
						}
					}
					lastClickPoint = vector;
					HoverEntry hoverEntry4 = list[index];
					lastHit = hoverEntry4.hit;
					gameObject = hoverEntry4.obj;
					vector2 = lastHit.point;
					levelEntity = hoverEntry4.entity;
					if (!levelEntity || levelEntity.hasBase)
					{
					}
					flag21 = true;
				}
				if (environmentManager.currentEnv == LevelSettings.LevelEnvironment.Water && !flag8 && !flag12 && !flag13 && (bool)ActiveObjectBrush && ActiveObjectBrush.placeOnWater)
				{
					Vector3 origin = lastRay.origin;
					float enter;
					if (origin.y > waterSurfaceY && waterSurfacePlane.Raycast(lastRay, out enter))
					{
						Vector3 point = lastRay.GetPoint(enter);
						if (!flag21 || (point - origin).sqrMagnitude < (vector2 - origin).sqrMagnitude)
						{
							vector2 = point;
							flag21 = true;
							gameObject = null;
							levelEntity = null;
						}
					}
				}
			}
			if (flag21)
			{
				if (flag20 && flag5)
				{
					GameObject pickedObject = null;
					bool flag25 = false;
					if (levelEntity != null)
					{
						pickedObject = levelEntity.gameObject;
						flag25 = true;
					}
					else if (BlockMapper.CurrentInstance.PickSupportsBlocks)
					{
						BlockBehaviour componentInParent = gameObject.GetComponentInParent<BlockBehaviour>();
						if (componentInParent != null)
						{
							pickedObject = componentInParent.gameObject;
							flag25 = true;
						}
					}
					if (flag25)
					{
						BlockMapper.Pick(pickedObject);
						return;
					}
				}
				if (flag16 && !flag11)
				{
					bool grid = StatMaster.Mode.LevelEditor.grid;
					grid = ((!flag2) ? grid : (!grid));
					Vector3 vector3 = Vector3.zero;
					Vector3 vector4 = Vector3.zero;
					if (grid)
					{
						float sNAP_VALUE = EntityTranslateTool.SNAP_VALUE;
						vector3 = TransformTool.SnapCeil(vector2, sNAP_VALUE);
						vector4 = TransformTool.SnapFloor(vector2, sNAP_VALUE);
						vector2 = TransformTool.Snap(vector2, sNAP_VALUE);
					}
					bool flag26 = InputManager.LevelEditor.RotateKeyHeld();
					bool flag27 = false;
					if (flag26 && isHoldingRotation != flag26)
					{
						mouseRotateY = ghostRotY;
						flag27 = true;
					}
					bool flag28 = InputManager.LevelEditor.ScaleKeyHeld() && ActiveObjectBrush.canScale;
					if (flag28 && isHoldingScale != flag28)
					{
						mouseScale = ghostScale;
						flag27 = true;
					}
					if (flag27 && !isHoldingScale && !isHoldingRotation)
					{
						pressedMousePos = Input.mousePosition;
						pressedMousePoint = vector2;
					}
					bool flag29 = false;
					if (isHoldingRotation || isHoldingScale)
					{
						vector2 = pressedMousePoint;
						Vector2 vector5 = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - pressedMousePos;
						float num = Vector2.Dot(vector5.normalized, new Vector2(-0.5f, -0.5f));
						float num2 = vector5.magnitude * num;
						if (isHoldingRotation)
						{
							float num3 = num2 / rotateMouseIncrement;
							float num4 = ((!grid) ? mouseRotateY : TransformTool.Snap(mouseRotateY, EntityRotateTool.SNAP_VALUE));
							ghostRotY = num4 - ((!grid) ? num3 : Mathf.Round(num3)) * EntityRotateTool.SNAP_VALUE;
							flag29 = true;
						}
						if (isHoldingScale)
						{
							float num5 = num2 / scaleMouseIncrement;
							float num6 = ((!grid) ? mouseScale.x : TransformTool.Snap(mouseScale.x, EntityScaleTool.SNAP_VALUE));
							float s = num6 - ((!grid) ? num5 : Mathf.Round(num5)) * EntityScaleTool.SNAP_VALUE * ActiveObjectBrush.placementScale.x;
							ghostScale = Vector3.one * EntityScaleTool.GetScaleValue(s);
						}
					}
					if (InputManager.RotateKeyUp() && !isHoldingRotation)
					{
						ghostRotY += ghostAngleIncrement;
						flag29 = true;
					}
					if (InputManager.LevelEditor.ScaleKeyUp() && !isHoldingScale && ActiveObjectBrush.canScale)
					{
						ghostScale += Vector3.one * EntityScaleTool.SNAP_VALUE * ActiveObjectBrush.placementScale.x;
					}
					isHoldingRotation = flag26;
					isHoldingScale = flag28;
					if (flag29)
					{
						while (ghostRotY >= 360f)
						{
							ghostRotY -= 360f;
						}
						while (ghostRotY < 0f)
						{
							ghostRotY += 360f;
						}
					}
					Quaternion q = Quaternion.Euler(0f, ghostRotY, 0f);
					Vector3 scale = ghostScale;
					if (ActiveObjectBrush.offset != Vector3.zero)
					{
						Vector3 vector6 = vector2;
						vector2 = Matrix4x4.TRS(vector2, q, Vector3.one).MultiplyPoint3x4(Vector3.Scale(ActiveObjectBrush.offset, ghostScale));
						if (grid)
						{
							Vector3 vector7 = vector2 - vector6;
							vector4 += vector7;
							vector3 += vector7;
						}
					}
					DebugExtension.DebugWireSphere(vector2, Color.red, 0.5f, 0f);
					if (grid && !isHoldingRotation)
					{
						Vector3 normal = lastHit.normal;
						float num7 = ((!(normal.x < 0f)) ? normal.x : (0f - normal.x));
						float num8 = ((!(normal.y < 0f)) ? normal.y : (0f - normal.y));
						float num9 = ((!(normal.z < 0f)) ? normal.z : (0f - normal.z));
						vector2 = ((num7 > num8 && num7 > num9) ? new Vector3((!(normal.x < 0f)) ? vector3.x : vector4.x, vector2.y, vector2.z) : ((!(num8 > num7) || !(num8 > num9)) ? new Vector3(vector2.x, vector2.y, (!(normal.z < 0f)) ? vector3.z : vector4.z) : new Vector3(vector2.x, (!(normal.y < 0f)) ? vector3.y : vector4.y, vector2.z)));
					}
					DebugExtension.DebugWireSphere(vector2, Color.yellow, 1f, 0f);
					bool flag30 = false;
					if (StatMaster.Mode.LevelEditor.paintPlacement && currentMouseDownTime > paintInterval)
					{
						paintDownTime += Time.deltaTime;
						if (paintDownTime > paintInterval)
						{
							flag30 = true;
							paintDownTime = 0f;
						}
					}
					if (!flag5 && !flag30)
					{
						ghostManager.Toggle(ownerId, true, vector2);
						ghostManager.MoveGhost(ownerId, vector2, new Vector3(ActiveObjectBrush.rotation.x, ghostRotY, ActiveObjectBrush.rotation.z), ghostScale);
						flag4 = true;
					}
					else
					{
						AddEntity(ActiveObjectBrush.ID, vector2, Quaternion.Euler(ActiveObjectBrush.rotation.x, ghostRotY, ActiveObjectBrush.rotation.z), scale, true);
						if (StatMaster.Mode.LevelEditor.paintPlacement)
						{
							ResetGhostTransform();
						}
						flag18 = true;
					}
				}
				else if (levelEntity != null)
				{
					if (flag11)
					{
						if (flag9 || flag5 || (flag10 && !flag15))
						{
							if (levelEntity.IsSelected)
							{
								selectionController.Remove(levelEntity);
								levelEntity.Select(false);
							}
							if (entityController.Remove(levelEntity.identifier, false))
							{
								RandomSoundController deleteSound = addPiece.deleteSound;
								if ((bool)deleteSound)
								{
									deleteSound.Stop();
									deleteSound.Play();
								}
							}
						}
					}
					else if (flag5)
					{
						if (flag14)
						{
							OpenBlockMapper(levelEntity.behaviour);
							flag18 = true;
						}
						else
						{
							if (!levelEntity.IsSelected)
							{
								selectionController.Select(levelEntity, flag3, true);
							}
							else if (flag3)
							{
								selectionController.Deselect(levelEntity, true);
							}
							flag18 = true;
						}
					}
				}
			}
			if (flag5 && !flag11 && !flag3 && !flag19 && !flag18)
			{
				if (!flag14)
				{
					selectionController.DeselectAll(true);
				}
				if (flag20)
				{
					BlockMapper.Pick(null);
				}
			}
		}
		if (!StatMaster.stopHotkeys && !StatMaster.inMenu && flag15)
		{
			if (flag10)
			{
				RemoveSelection();
			}
			else if (InputManager.LevelEditor.DuplicateKeys())
			{
				DuplicateSelection();
			}
			else if (InputManager.AdvancedBuilding.SelectInverseKeys())
			{
				InverseSelection(true);
			}
		}
		if (!flag4)
		{
			ghostManager.Toggle(ownerId, false, Vector3.zero);
		}
		if (!StatMaster.inMenu)
		{
			if (InputManager.LevelEditor.SelectAllKeys())
			{
				selectionController.SelectAll(true);
			}
			else if (InputManager.RedoKeys())
			{
				Redo();
			}
			else if (InputManager.UndoKeys())
			{
				Undo();
			}
		}
	}

	public virtual void InverseSelection(bool addToUndo)
	{
		List<LevelEntity> levelSelection = selectionController.LevelSelection;
		if (levelSelection.Count == 0)
		{
			return;
		}
		List<LevelEntity> list = new List<LevelEntity>();
		for (int i = 0; i < entityController.Entities.Count; i++)
		{
			LevelEntity levelEntity = entityController.Entities[i];
			if (!levelEntity.IsSelected)
			{
				list.Add(levelEntity);
			}
		}
		DeselectAll(true);
		Select(list, false, true);
	}

	public void LoadPlaylistLevel(int index)
	{
		ServerSettings serverSettings = NetworkScene.ServerSettings;
		serverSettings.playListIndex = index;
		string text = serverSettings.playList[index];
		if (!Path.IsPathRooted(text))
		{
			text = Path.Combine(StaticSettings.DataPath + "/CustomLevels/", text);
		}
		if (File.Exists(text))
		{
			string levelData = File.ReadAllText(text);
			auxAddPiece.LoadLevel(levelData, Path.GetFileNameWithoutExtension(text));
		}
		else
		{
			Debug.LogError("Couldn't find level at " + text + "!");
		}
	}

	public void LoadCustomLevel(string levelPath)
	{
		if (!Path.IsPathRooted(levelPath))
		{
			levelPath = Path.Combine(StaticSettings.DataPath + "/CustomLevels/", levelPath);
		}
		if (File.Exists(levelPath))
		{
			string levelData = File.ReadAllText(levelPath);
			auxAddPiece.LoadLevel(levelData, Path.GetFileNameWithoutExtension(levelPath));
		}
		else
		{
			Debug.LogError("Couldn't find level at " + levelPath + "!");
		}
	}

	public void AddEntity(int prefabID, Vector3 location, Quaternion rotation, Vector3 scale, bool showPlacementEffect)
	{
		entityController.Add(prefabID, location, rotation, scale, LevelPrefab.INVALID_ID, false, false, showPlacementEffect);
	}

	public byte[] EncodeSettings(LevelSettings settings)
	{
		StringWriter stringWriter = new StringWriter();
		XmlWriter xmlWriter = XmlWriter.Create(stringWriter);
		LevelXMLSaver.WriteLevelSettings(xmlWriter, settings);
		xmlWriter.Close();
		string s = stringWriter.ToString();
		stringWriter.Close();
		return CLZF2.Compress(Encoding.UTF8.GetBytes(s));
	}

	public void DecodeSettings(byte[] data)
	{
		string fileData = Encoding.UTF8.GetString(CLZF2.Decompress(data));
		LevelXMLLoader.ReadLevelFromString(fileData, true);
		SingleInstance<Events>.Instance.LevelSetupChanged(LevelSetup.From(Settings));
	}

	private void OpenBlockMapper(GenericEntity entityBehaviour)
	{
		BlockMapper blockMapper = BlockMapper.Open(entityBehaviour);
		if (blockMapper != null)
		{
			BlockMapper.AudioSource.Play();
			editLogicHandler.Init(blockMapper);
		}
	}

	public void DuplicateSelection()
	{
		List<EntityController.PlaceEntry> list = new List<EntityController.PlaceEntry>();
		List<LevelEntity> levelSelection = selectionController.LevelSelection;
		for (int i = 0; i < levelSelection.Count; i++)
		{
			LevelEntity levelEntity = levelSelection[i];
			if (!(levelEntity == null))
			{
				list.Add(new EntityController.PlaceEntry(levelEntity.behaviour.prefab.ID, levelEntity.Position, levelEntity.Rotation, levelEntity.Scale, levelEntity.GetEntityData(), levelEntity.identifier));
			}
		}
		if (list.Count > 0)
		{
			entityController.Add(list, false, true, true);
		}
	}

	public void ResetRotation()
	{
		List<LevelUndoAction> list = new List<LevelUndoAction>();
		foreach (LevelEntity item in selectionController.Selection)
		{
			if (item.Rotation != Quaternion.identity)
			{
				list.Add(new LUARotateEntity(item, item.Rotation, item.Position));
			}
			Vector3 center = item.GetCenter();
			Transform transform = item.transform;
			Quaternion identity = Quaternion.identity;
			item.SetRotation(identity);
			transform.rotation = identity;
			if (!StatMaster.Mode.LevelEditor.objectPivot)
			{
				Vector3 center2 = item.GetCenter();
				Vector3 vector = center - center2;
				Vector3 position = item.Position + vector;
				item.SetPosition(position);
				transform.position = position;
			}
		}
		if (list.Count > 0)
		{
			LevelUndoSystem.Add(list);
		}
	}

	public void ToggleEditor(bool toggle, bool reloadLevel)
	{
		ServerSettings serverSettings = NetworkScene.ServerSettings;
		Settings.ResetBlockTypeLimiter();
		serverSettings.levelEditor = toggle;
		networkScene.UpdateSettings(serverSettings);
		editorUI.options.ResetLocalSim();
		if (StatMaster.isHosting && reloadLevel)
		{
			auxAddPiece.ReloadLevel(true);
		}
		ResetWindow();
	}

	public void RemoveSelection()
	{
		List<LevelEntity> levelSelection = selectionController.LevelSelection;
		List<long> list = new List<long>();
		for (int i = 0; i < levelSelection.Count; i++)
		{
			LevelEntity levelEntity = levelSelection[i];
			list.Add(levelEntity.identifier);
		}
		entityController.Remove(list, false);
	}

	public void Add(List<EntityController.PlaceEntry> entries, bool isUndo, bool isDuplicate, bool showPlacementEffect)
	{
		entityController.Add(entries, isUndo, isDuplicate, showPlacementEffect);
	}

	public void Add(ushort playerId, byte[] messageData)
	{
		entityController.Add(playerId, messageData);
	}

	public void Remove(ushort playerId, byte[] messageData)
	{
		entityController.Remove(playerId, messageData);
	}

	public void Remove(List<long> ids, bool isUndo)
	{
		entityController.Remove(ids, isUndo);
	}

	public void Select(List<LevelEntity> entities, bool multiSelect, bool addToUndo)
	{
		if (!IsTransformTool(CurrentState))
		{
			SetActiveTool(StatMaster.Tool.Translate);
		}
		selectionController.Select(entities, multiSelect, addToUndo);
	}

	public void Select(LevelEntity entity, bool multiSelect, bool addToUndo)
	{
		if (!IsTransformTool(CurrentState))
		{
			SetActiveTool(StatMaster.Tool.Translate);
		}
		selectionController.Select(entity, multiSelect, addToUndo);
	}

	public void Deselect(LevelEntity entity, bool addToUndo)
	{
		selectionController.Deselect(entity, addToUndo);
	}

	public void Deselect(List<LevelEntity> entities, bool addToUndo)
	{
		selectionController.Deselect(entities, addToUndo);
	}

	public void DeselectAll(bool addToUndo)
	{
		selectionController.DeselectAll(addToUndo);
	}

	public void RemoveSelect(LevelEntity entity)
	{
		selectionController.Remove(entity);
	}

	public bool Get(long id, out LevelEntity entity)
	{
		return entityController.Get(id, out entity);
	}

	public bool GetPrefab(int prefabId, out LevelPrefab prefab)
	{
		return PrefabMaster.LevelPrefabs[10].TryGetValue(prefabId, out prefab);
	}

	public void OnSelectionUpdate()
	{
		UpdateTool();
		SetActiveTool(CurrentState);
	}

	public void OnEntityUpdate(LevelEntity entity, EntityUpdateState updateState)
	{
		if (!entity)
		{
			return;
		}
		switch (updateState)
		{
		case EntityUpdateState.Place:
			entityController.Add(entity);
			entity.SetController(entityController);
			break;
		case EntityUpdateState.Remove:
		{
			for (int i = 0; i < Playerlist.Players.Count; i++)
			{
				PlayerData playerData = Playerlist.Players[i];
				if (playerData.hasSelection && playerData.selectedEntity == entity)
				{
					playerData.hasSelection = false;
					playerData.selectedEntity = null;
				}
			}
			entity.OnRemove();
			entityController.Remove(entity);
			break;
		}
		}
		if (entity.IsSelected)
		{
			OnSelectionUpdate();
		}
	}

	protected void UpdateEditingLevel()
	{
		bool isEditingLevel = AddPiece.isEditingLevel;
		bool flag = (AddPiece.isEditingLevel = CurrentState != StatMaster.Tool.None || ActiveObjectBrush != null || (PlayerData.hasLocalPlayer && PlayerData.localPlayer.isSpectator));
		if (flag != isEditingLevel)
		{
			if (flag)
			{
				hud.DisableMachineTools();
			}
			addPiece.UpdateBlockButtons();
		}
	}

	public void SetActiveTool(StatMaster.Tool tool, bool addToUndo)
	{
		if (StatMaster.levelSimulating)
		{
			return;
		}
		bool flag = tool != CurrentState;
		if (flag)
		{
			if (CurrentState == StatMaster.Tool.Modify && BlockMapper.CurrentInstance != null && !BlockMapper.CurrentInstance.IsBlock)
			{
				BlockMapper.Close();
			}
			if (!IsTransformTool(tool))
			{
				selectionController.DeselectAll(addToUndo);
				if (StatMaster.ToolActive)
				{
					StatMaster.ToolActive = false;
					StatMaster.StopHotKeys(false);
				}
				StatMaster.Mode.isTranslating = false;
			}
			StatMaster.Mode.LevelEditor.selectedTool = tool;
			SingleInstanceFindOnly<LevelEditorUI>.Instance.transformTools.UpdateSelectedTool();
			if (tool != StatMaster.Tool.None || ActiveObjectBrush != lastPrefab)
			{
				SetPrefab((tool != StatMaster.Tool.None) ? null : lastPrefab);
			}
		}
		UpdateEditingLevel();
		bool flag2 = selectionController.Count > 0;
		if (tool != StatMaster.Tool.None)
		{
			if (!ToolGO.activeSelf)
			{
				ToolGO.SetActive(true);
			}
			for (int i = 0; i < Tools.Length; i++)
			{
				bool flag3 = i == (int)tool;
				Tools[i].gameObject.SetActive(flag2 && flag3);
			}
		}
		else if (ToolGO.activeSelf)
		{
			ToolGO.SetActive(false);
		}
		if (flag)
		{
			UpdateTool();
		}
	}

	public void SetActiveTool(StatMaster.Tool tool)
	{
		SetActiveTool(tool, true);
	}

	public bool IsTransformTool(StatMaster.Tool tool)
	{
		return tool == StatMaster.Tool.Translate || tool == StatMaster.Tool.Rotate || tool == StatMaster.Tool.Scale || tool == StatMaster.Tool.Mirror;
	}

	public void ClearRemovedEntity(LevelEntity entity)
	{
		if (entity.isStatic && !entity.isDestroyed)
		{
			if (entity.needsTracking)
			{
				level.RemoveSimTrack(entity);
			}
			for (int i = 0; i < entity.children.Length; i++)
			{
				LevelEntity levelEntity = entity.children[i] as LevelEntity;
				if (levelEntity.needsTracking)
				{
					level.RemoveSimTrack(entity);
				}
				levelEntity.isDestroyed = true;
			}
			entity.isDestroyed = true;
			UnityEngine.Object.DestroyImmediate(entity.gameObject);
		}
		removedEntities.Remove(entity);
	}

	public void AddRemovedEntity(LevelEntity entity)
	{
		removedEntities.Add(entity);
	}

	public void MoveStaticToPhysGoal(LevelEntity entity)
	{
		entity.transform.SetParent(ReferenceMaster.physicsGoalInstance);
		entityController.Remove(entity);
		removedEntities.Add(entity);
	}

	public bool DestroyEntity(LevelEntity entity)
	{
		if (entity.isDestroyed || entity.gameObject == null)
		{
			return false;
		}
		OnEntityUpdate(entity, EntityUpdateState.Remove);
		if (entity.IsSelected)
		{
			selectionController.Deselect(entity, false);
		}
		if (entity.needsTracking)
		{
			level.RemoveSimTrack(entity);
		}
		for (int i = 0; i < entity.children.Length; i++)
		{
			LevelEntity levelEntity = entity.children[i] as LevelEntity;
			if (levelEntity.needsTracking)
			{
				level.RemoveSimTrack(entity);
			}
			levelEntity.isDestroyed = true;
		}
		entity.isDestroyed = true;
		UnityEngine.Object.DestroyImmediate(entity.gameObject);
		return true;
	}

	public LevelEntity InstantiatePrefab(LevelPrefab prefab, Vector3 pos, Quaternion rot, Vector3 scale)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(prefab.gameObject, pos, rot, (!prefab.ignorePhysics) ? level.PhysGoal : level.StaticGoal) as GameObject;
		Transform transform = gameObject.transform;
		transform.localScale = scale;
		gameObject.tag = "LevelObject";
		LevelEntity component = gameObject.GetComponent<LevelEntity>();
		component.isStatic = prefab.ignorePhysics;
		component.behaviour.isBuildBlock = true;
		gameObject.SetActive(true);
		gameObject.name = prefab.name;
		return component;
	}

	public static void ProcessPrefab(LevelPrefab prefab, out Vector3 boundCenter, out Vector3 boundSize)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(prefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
		Transform transform = gameObject.transform;
		transform.localScale = Vector3.one;
		LevelEntity component = gameObject.GetComponent<LevelEntity>();
		LevelBoundingBox.Calculate(component, out boundCenter, out boundSize);
		UnityEngine.Object.DestroyImmediate(component.gameObject);
	}

	public void ResetPrefab()
	{
		LevelPrefab activeObjectBrush = ActiveObjectBrush;
		ActiveObjectBrush = null;
		StatMaster.SelectedLevelPrefab = ActiveObjectBrush;
		if (ActiveObjectBrush != activeObjectBrush)
		{
			SingleInstanceFindOnly<LevelEditorUI>.Instance.UpdatePage();
		}
		UpdateEditingLevel();
		ghostRotY = activeObjectBrush.rotation.y;
	}

	public void SetPrefab(LevelPrefab prefabObject)
	{
		LevelPrefab activeObjectBrush = ActiveObjectBrush;
		ActiveObjectBrush = ((!(ActiveObjectBrush != prefabObject)) ? null : prefabObject);
		StatMaster.SelectedLevelPrefab = ActiveObjectBrush;
		if (prefabObject != null)
		{
			lastPrefab = ActiveObjectBrush;
		}
		if (ActiveObjectBrush != null)
		{
			ghostManager.SetPrefab(ownerId, ActiveObjectBrush);
			if (CurrentState != StatMaster.Tool.None)
			{
				SetActiveTool(StatMaster.Tool.None);
			}
		}
		if (ActiveObjectBrush != activeObjectBrush)
		{
			SingleInstanceFindOnly<LevelEditorUI>.Instance.UpdatePage();
		}
		UpdateEditingLevel();
		ResetGhostTransform();
	}

	private void SendPlayerSelection()
	{
		if (lastSentSelection == lastSelectedEntity)
		{
			return;
		}
		if (lastSelectedEntity != null)
		{
			if (lastSelectedEntity.hasBehaviour)
			{
				auxAddPiece.SendNetworkMessage(RPCMessageType.UpdatePlayerSelection, lastSelectedEntity.behaviour.GetIdentifierBytes());
			}
			else
			{
				Debug.LogError("LevelEditor::UpdatePlayerSelection(): Entity behaviour is null!");
			}
		}
		else
		{
			auxAddPiece.SendNetworkMessage(RPCMessageType.ResetPlayerSelection);
		}
		lastSentSelection = lastSelectedEntity;
	}

	public void UpdatePlayerSelection(LevelEntity entity)
	{
		if (!(entity == lastSelectedEntity))
		{
			lastSelectedEntity = entity;
			if (!updatedSelection)
			{
				SendPlayerSelection();
				updatedSelection = true;
				lastSelectUpdate = 0f;
			}
		}
	}

	public void UpdateTool()
	{
		if (selectionController.Count == 0)
		{
			UpdatePlayerSelection(null);
			return;
		}
		LevelEntity lastEntity = selectionController.LastEntity;
		ToolTransform.position = (StatMaster.Mode.LevelEditor.objectPivot ? lastEntity.Position : ((selectionController.Count != 1 && StatMaster.Mode.LevelEditor.global && (StatMaster.Mode.LevelEditor.selectedTool != StatMaster.Tool.Rotate || StatMaster.Mode.LevelEditor.linked)) ? selectionController.GetSelectionCenter() : lastEntity.GetCenter()));
		ToolTransform.rotation = ((!StatMaster.Mode.LevelEditor.global) ? lastEntity.Rotation : Quaternion.identity);
		UpdatePlayerSelection(lastEntity);
	}

	public void OnLevelLoad()
	{
		if (StatMaster.entityCountChanged != null)
		{
			StatMaster.entityCountChanged(entityController.Entities.Count);
		}
		if (!StatMaster.Mode.levelEdit && OptionsMaster.gatherTransformTargets)
		{
			level.ClearTransformTargets();
			List<LevelEntity> entities = Entities;
			for (int i = 0; i < entities.Count; i++)
			{
				level.AddTransformTargets(entities[i].behaviour);
			}
			level.CombineStatic();
		}
	}
}
