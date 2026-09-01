using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEngine;

public class CustomLevel : MonoBehaviour
{
	[Serializable]
	public class MusicInfo
	{
		public AudioClip track;

		public float volume;

		public float pitch;
	}

	private class LogicProgressEvent
	{
		public EntityLogic logic;

		public EntityEvent evt;

		public LogicProgressEvent(EntityLogic l, EntityEvent e)
		{
			logic = l;
			evt = e;
		}
	}

	public MusicInfo[] trackSelection;

	public static CustomLevel Instance;

	public bool remoteSim;

	public FrameBufferManager frameManager;

	public FrameBufferManager logicFrameManager;

	private Transform physInstance;

	private Transform physGoal;

	private Transform staticGoal;

	private LevelEditor levelEditor;

	private NetworkController networkController;

	private bool isTracking;

	private NetworkAuxAddPiece networkAuxAddPiece;

	private int entityCount;

	private ProjectileManager projectileManager;

	private List<LevelEntity> baseEntities;

	private List<LevelEntity> hiddenStatic;

	private List<LevelEntity> staticEntities;

	private List<GenericEntity> updatingLogicEntities;

	private List<LevelEntity> clientLerpTargets;

	private List<LevelEntity> trackedEntities;

	private List<LevelEntity> trackedSimEntities;

	private List<long> transformTargets;

	private List<LogicProgressEvent> progressEvents;

	private LevelDataManager dataManager;

	private WaitHUDManager countdownManager;

	private Dictionary<int, EntityLogicState> simPhysData = new Dictionary<int, EntityLogicState>();

	private Dictionary<long, int> idCorrectionMap = new Dictionary<long, int>();

	private List<long> skipMap = new List<long>();

	private bool hasSimFrame;

	private List<LevelEntity> removedSimEntities = new List<LevelEntity>();

	public Dictionary<string, float> variables;

	private List<GenericEntity> globalVarTargets;

	private byte[] simFrameData;

	private int physSimFrameSize;

	private NetworkAddPiece addPiece;

	private uint startFrame;

	private float timeCorrection;

	public uint logicFrame;

	public Transform serverTransformParent;

	private int selectedTrack = -1;

	public bool SendShort
	{
		get
		{
			return networkController.SendShort;
		}
	}

	public int BufferLength
	{
		get
		{
			return networkController.FullBufferLength;
		}
	}

	public Transform PhysGoal
	{
		get
		{
			return physGoal;
		}
	}

	public Transform StaticGoal
	{
		get
		{
			return staticGoal;
		}
	}

	public int Session { get; set; }

	public int TotalEntityCount
	{
		get
		{
			return StatMaster.levelSimulating ? ((!StatMaster.isLocalSim) ? ((int)networkController.ObjectCount) : physGoal.childCount) : 0;
		}
	}

	public bool PollObjects()
	{
		return networkController.PollObjects(trackedSimEntities);
	}

	protected void Awake()
	{
		Instance = this;
		remoteSim = false;
		updatingLogicEntities = new List<GenericEntity>();
		trackedEntities = new List<LevelEntity>();
		trackedSimEntities = new List<LevelEntity>();
		baseEntities = new List<LevelEntity>();
		staticEntities = new List<LevelEntity>();
		hiddenStatic = new List<LevelEntity>();
		progressEvents = new List<LogicProgressEvent>();
		transformTargets = new List<long>();
		clientLerpTargets = new List<LevelEntity>();
		globalVarTargets = new List<GenericEntity>();
		frameManager = new FrameBufferManager();
		logicFrameManager = new FrameBufferManager(true);
		Session = 0;
		variables = new Dictionary<string, float>();
		CreateTransformParent();
	}

	public void CreateTransformParent()
	{
		serverTransformParent = new GameObject("ServerTransformParent").transform;
		serverTransformParent.parent = base.transform;
	}

	public void ResetLevel()
	{
		hasSimFrame = false;
		simPhysData.Clear();
		idCorrectionMap.Clear();
		skipMap.Clear();
	}

	public void AddLerpTarget(LevelEntity entity)
	{
		if (!entity.isLerpTarget)
		{
			clientLerpTargets.Add(entity);
			entity.isLerpTarget = true;
		}
	}

	public void RemoveLerpTarget(LevelEntity entity)
	{
		clientLerpTargets.Remove(entity);
	}

	public void PlayTrack(int track, int volume)
	{
		int num = Mathf.Clamp(track, 0, trackSelection.Length - 1);
		if (selectedTrack != num)
		{
			SingleInstance<MusicController>.Instance.customMusicPresent = true;
			MusicInfo musicInfo = trackSelection[num];
			float num2 = (float)volume / 100f;
			SingleInstance<MusicController>.Instance.PlayCustomTrack(musicInfo.track, musicInfo.volume * num2, musicInfo.pitch);
			selectedTrack = num;
		}
	}

	public void SetVariable(string key, EventContainer.VarModifyType modifyMode, float val)
	{
		float newVal = GenericEntity.SetVariable(variables, key, modifyMode, val);
		List<GenericEntity> list = new List<GenericEntity>(globalVarTargets);
		for (int i = 0; i < list.Count; i++)
		{
			list[i].OnSetVariable(key, true, newVal);
		}
	}

	public void AddGlobalVarTarget(LevelEntity entity)
	{
		if (!entity.addedToGlobalVarTargets)
		{
			globalVarTargets.Add(entity.behaviour);
			entity.addedToGlobalVarTargets = true;
		}
	}

	public void RemoveGlobalVarTarget(LevelEntity entity)
	{
		if (entity.addedToGlobalVarTargets)
		{
			globalVarTargets.Remove(entity.behaviour);
			entity.addedToGlobalVarTargets = false;
		}
	}

	public void AddTrack(LevelEntity entity)
	{
		trackedEntities.Add(entity);
	}

	public void RemoveTrack(LevelEntity entity)
	{
		trackedEntities.Remove(entity);
	}

	public void AddSimTrack(LevelEntity entity)
	{
		trackedSimEntities.Add(entity);
	}

	public void RemoveSimTrack(LevelEntity entity)
	{
		trackedSimEntities.Remove(entity);
	}

	public void OnUpdateSettings(ServerSettings settings)
	{
		for (int i = 0; i < trackedEntities.Count; i++)
		{
			trackedEntities[i].UpdateBaseInterval();
		}
	}

	public void Init(LevelEditor e, LevelDataManager levelDataManager)
	{
		networkController = base.gameObject.AddComponent<NetworkController>();
		levelEditor = e;
		dataManager = levelDataManager;
		projectileManager = ProjectileManager.Instance;
		addPiece = NetworkAddPiece.Instance;
		countdownManager = SingleInstanceFindOnly<WaitHUDManager>.Instance;
		StatMaster._customLevelSimulating = false;
		StatMaster.UpdateSimulationState();
		physGoal = base.transform.FindChild("PHYSICS GOAL");
		staticGoal = base.transform.FindChild("STATIC");
		ResetFrame();
	}

	public void ShowWaitEvent(EventContainer.WaitEvent evt)
	{
		countdownManager.AddElement(evt);
	}

	public void HideWaitEvent(EventContainer.WaitEvent evt)
	{
		countdownManager.RemoveElement(evt);
	}

	protected void OnDestroy()
	{
		if (StatMaster._customLevelSimulating)
		{
			StatMaster._customLevelSimulating = false;
			StatMaster.UpdateSimulationState();
			StatMaster.isLocalSim = false;
		}
	}

	public void ResetFrame()
	{
		networkController.ResetFrame();
	}

	public int GetSimFrame()
	{
		physSimFrameSize = networkController.GetSimFrame();
		int num = 0;
		List<byte[]> list = new List<byte[]>();
		List<byte[]> list2 = new List<byte[]>();
		List<byte[]> list3 = new List<byte[]>();
		List<byte[]> list4 = new List<byte[]>();
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		List<LevelEntity> sortedEntities = levelEditor.SortedEntities;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		for (int i = 0; i < sortedEntities.Count; i++)
		{
			LevelEntity levelEntity = sortedEntities[i];
			if (levelEntity.isStatic)
			{
				byte[] data;
				if (EntityLogicState.Encode(levelEntity, LevelEntity.ID_LENGTH, out data))
				{
					Buffer.BlockCopy(levelEntity.behaviour.GetIdentifierBytes(), 0, data, 0, LevelEntity.ID_LENGTH);
					list2.Add(data);
					num3 += data.Length;
				}
				continue;
			}
			LevelEntity simEntity = levelEntity.simEntity;
			if (simEntity == null)
			{
				list4.Add(levelEntity.behaviour.GetIdentifierBytes());
				num7 += LevelEntity.ID_LENGTH;
				continue;
			}
			if (simEntity.id != num5)
			{
				short num8 = (short)(simEntity.id - num5);
				if (num8 != num4)
				{
					byte[] array = new byte[LevelEntity.ID_LENGTH + 2];
					Buffer.BlockCopy(simEntity.behaviour.GetIdentifierBytes(), 0, array, 0, LevelEntity.ID_LENGTH);
					Buffer.BlockCopy(BitConverter.GetBytes(num8), 0, array, LevelEntity.ID_LENGTH, 2);
					list3.Add(array);
					num6 += array.Length;
					num4 = num8;
				}
			}
			num5++;
			num5 += simEntity.BreakCount;
			for (int j = 0; j < simEntity.children.Length; j++)
			{
				LevelEntity levelEntity2 = simEntity.children[j] as LevelEntity;
				num5++;
				num5 += levelEntity2.BreakCount;
			}
			int num9 = NetworkCompression.PackedUIntLength(i, false);
			byte[] data2;
			if (EntityLogicState.Encode(simEntity, num9, out data2))
			{
				NetworkCompression.PackUInt(i, data2, 0, false, num9);
				list.Add(data2);
				num2 += data2.Length;
			}
		}
		int num10 = NetworkCompression.PackedUIntLength(list4.Count, false);
		int num11 = NetworkCompression.PackedUIntLength(list3.Count, false);
		int num12 = NetworkCompression.PackedUIntLength(list2.Count, false);
		int num13 = NetworkCompression.PackedUIntLength(list.Count, false);
		simFrameData = new byte[8 + num10 + num7 + num11 + num6 + num12 + num3 + num13 + num2];
		num = 0;
		NetworkCompression.WriteUInt(addPiece.frame, false, simFrameData, num);
		num += 4;
		NetworkCompression.WriteUInt(logicFrame, false, simFrameData, num);
		num += 4;
		NetworkCompression.PackUInt(list4.Count, simFrameData, num, false, num10);
		num += num10;
		NetworkCompression.WriteArray(list4, simFrameData, num);
		num += num7;
		NetworkCompression.PackUInt(list3.Count, simFrameData, num, false, num11);
		num += num11;
		NetworkCompression.WriteArray(list3, simFrameData, num);
		num += num6;
		NetworkCompression.PackUInt(list2.Count, simFrameData, num, false, num12);
		num += num12;
		NetworkCompression.WriteArray(list2, simFrameData, num);
		num += num3;
		NetworkCompression.PackUInt(list.Count, simFrameData, num, false, num13);
		num += num13;
		NetworkCompression.WriteArray(list, simFrameData, num);
		num += num2;
		return physSimFrameSize + simFrameData.Length;
	}

	public void WriteSimFrame(byte[] data, int offset)
	{
		networkController.WriteSimFrame(data, offset);
		Buffer.BlockCopy(simFrameData, 0, data, offset + physSimFrameSize, simFrameData.Length);
	}

	public int ReadSimFrame(byte[] frameData, int offset, float correction)
	{
		timeCorrection = correction;
		int num = offset;
		offset += networkController.ReadSimFrame(frameData, offset);
		startFrame = NetworkCompression.ReadUInt(false, frameData, offset);
		offset += 4;
		logicFrame = NetworkCompression.ReadUInt(false, frameData, offset);
		offset += 4;
		skipMap.Clear();
		int count;
		offset += NetworkCompression.UnpackUInt(frameData, offset, false, out count);
		for (int i = 0; i < count; i++)
		{
			long item = BitConverter.ToInt64(frameData, offset);
			skipMap.Add(item);
			offset += LevelEntity.ID_LENGTH;
		}
		idCorrectionMap.Clear();
		int count2;
		offset += NetworkCompression.UnpackUInt(frameData, offset, false, out count2);
		for (int i = 0; i < count2; i++)
		{
			long item = BitConverter.ToInt64(frameData, offset);
			offset += LevelEntity.ID_LENGTH;
			short value = BitConverter.ToInt16(frameData, offset);
			idCorrectionMap.Add(item, value);
			offset += 2;
		}
		int count3;
		offset += NetworkCompression.UnpackUInt(frameData, offset, false, out count3);
		for (int i = 0; i < count3; i++)
		{
			long item = BitConverter.ToInt64(frameData, offset);
			offset += LevelEntity.ID_LENGTH;
			EntityLogicState entityLogicState = new EntityLogicState();
			offset += entityLogicState.Decode(frameData, offset);
			LevelEntity entity;
			if (levelEditor.Get(item, out entity))
			{
				entity.SetLogicStateFrame(entityLogicState);
			}
		}
		simPhysData.Clear();
		int count4;
		offset += NetworkCompression.UnpackUInt(frameData, offset, false, out count4);
		for (int i = 0; i < count4; i++)
		{
			int count5;
			offset += NetworkCompression.UnpackUInt(frameData, offset, false, out count5);
			EntityLogicState entityLogicState = new EntityLogicState();
			offset += entityLogicState.Decode(frameData, offset);
			simPhysData.Add(count5, entityLogicState);
		}
		hasSimFrame = true;
		return offset - num;
	}

	public void WriteBufferData(byte[] buffer, int offset)
	{
		networkController.WriteBufferData(true, buffer, offset);
	}

	public void NewFrame(uint frame)
	{
		for (int i = 0; i < clientLerpTargets.Count; i++)
		{
			LevelEntity levelEntity = clientLerpTargets[i];
			if (levelEntity != null && levelEntity.hasLerpTarget)
			{
				if (levelEntity.behaviour != null && levelEntity.isLerpTarget)
				{
					EventContainer.TransformEvent currentTransformEvent = levelEntity.behaviour.currentTransformEvent;
					if (currentTransformEvent != null)
					{
						levelEntity.SetData(!currentTransformEvent.ignorePosition, levelEntity.lerpTransformTarget.position, !currentTransformEvent.ignoreRotation, levelEntity.lerpTransformTarget.rotation);
					}
				}
				else
				{
					RemoveLerpTarget(levelEntity);
					i--;
				}
			}
			else
			{
				clientLerpTargets.RemoveAt(i--);
			}
		}
		for (int i = 0; i < trackedSimEntities.Count; i++)
		{
			LevelEntity levelEntity = trackedSimEntities[i];
			if (!levelEntity.isLerpTarget)
			{
				levelEntity.NewFrame(frame);
			}
		}
	}

	public void UpdateEntities(float delta)
	{
		for (int i = 0; i < trackedEntities.Count; i++)
		{
			LevelEntity levelEntity = trackedEntities[i];
			if (!levelEntity.UpdateEntity(delta))
			{
				removedSimEntities.Add(levelEntity);
				levelEntity.needsTracking = false;
			}
		}
		for (int i = 0; i < removedSimEntities.Count; i++)
		{
			RemoveTrack(removedSimEntities[i]);
		}
		removedSimEntities.Clear();
	}

	public void UpdateProgressEvents(float delta, bool useFixedUpdate)
	{
		for (int i = 0; i < progressEvents.Count; i++)
		{
			EntityEvent evt = progressEvents[i].evt;
			if (!evt.eventData.isDone && useFixedUpdate == evt.eventData.useFixedUpdate)
			{
				evt.UpdateEvent(delta);
			}
		}
	}

	public void UpdateSimEntities(float delta)
	{
		for (int i = 0; i < trackedSimEntities.Count; i++)
		{
			LevelEntity levelEntity = trackedSimEntities[i];
			if (!levelEntity.UpdateEntity(delta))
			{
				removedSimEntities.Add(levelEntity);
				levelEntity.needsTracking = false;
			}
		}
		for (int i = 0; i < removedSimEntities.Count; i++)
		{
			RemoveSimTrack(removedSimEntities[i]);
		}
		removedSimEntities.Clear();
	}

	public int ReadBufferData(uint frame, int session, byte[] data, int offset)
	{
		if (frame >= startFrame)
		{
			return networkController.ReadBufferData(frame, data, offset);
		}
		return NetworkController.BufferDataLength(data, offset);
	}

	public void IncrementSession()
	{
		if (Session < 200)
		{
			Session++;
		}
		else
		{
			Session = 0;
		}
	}

	public void ToggleSim(bool toggle)
	{
		if (toggle)
		{
			StartSim();
		}
		else
		{
			StopSim();
		}
	}

	public void RegisterUpdatingLogicRunner(GenericEntity entity)
	{
		updatingLogicEntities.Add(entity);
	}

	public void UnregisterUpdatingLogicRunner(GenericEntity entity)
	{
		updatingLogicEntities.Remove(entity);
	}

	public void DeactivateEntityWithEventType(EventContainer.EventType eventType)
	{
		if (physInstance == null)
		{
			return;
		}
		GenericEntity[] array = (from x in physInstance.GetComponentsInChildren<GenericEntity>()
			where x.hasLogic && x.logicData.Any((EntityLogic y) => y.events.Any((EntityEvent z) => z.eventType == eventType))
			select x).ToArray();
		for (int num = 0; num < array.Length; num++)
		{
			array[num].DeactivateEntity();
		}
	}

	public void UpdateLogic(float delta, bool useFixedUpdate)
	{
		for (int i = 0; i < updatingLogicEntities.Count; i++)
		{
			GenericEntity genericEntity = updatingLogicEntities[i];
			genericEntity.UpdateLogic(delta, useFixedUpdate);
			if (!genericEntity.hasRunningLogic)
			{
				i--;
			}
		}
	}

	public void ClearTransformTargets()
	{
		transformTargets.Clear();
	}

	public void AddTransformTargets(GenericEntity genericEntity)
	{
		if (!genericEntity.hasLogic)
		{
			return;
		}
		List<EntityLogic> logicData = genericEntity.logicData;
		for (int i = 0; i < logicData.Count; i++)
		{
			EntityLogic entityLogic = logicData[i];
			for (int j = 0; j < entityLogic.events.Count; j++)
			{
				EntityEvent entityEvent = entityLogic.events[j];
				if (entityEvent.eventType != EventContainer.EventType.Transform)
				{
					continue;
				}
				if (entityLogic.UseSelf(entityEvent))
				{
					long identifier = genericEntity.entity.identifier;
					if (!transformTargets.Contains(identifier))
					{
						transformTargets.Add(identifier);
						if (genericEntity.entity.isStatic)
						{
							genericEntity.entity.transformTarget = true;
						}
					}
					continue;
				}
				for (int k = 0; k < entityEvent.entityList.Count; k++)
				{
					long identifier = entityEvent.entityList[k];
					if (!transformTargets.Contains(identifier))
					{
						transformTargets.Add(identifier);
						LevelEntity entity;
						if (levelEditor.Get(identifier, out entity))
						{
							entity.transformTarget = true;
						}
					}
				}
			}
		}
	}

	public void CombineStatic()
	{
		List<GameObject> list = new List<GameObject>();
		List<LevelEntity> entities = levelEditor.Entities;
		for (int i = 0; i < entities.Count; i++)
		{
			LevelEntity levelEntity = entities[i];
			if (levelEntity.isStatic && !levelEntity.transformTarget && levelEntity.behaviour.prefab.batchWhenStatic)
			{
				levelEntity.ToggleStatic(true);
				MeshRenderer meshRenderer = levelEntity.behaviour.MeshRenderer;
				if (levelEntity.behaviour.prefab.hasStaticMaterial && levelEntity.hasBehaviour)
				{
					meshRenderer.sharedMaterial = levelEntity.behaviour.prefab.staticMaterial;
				}
				list.Add(meshRenderer.gameObject);
			}
		}
		StaticBatchingUtility.Combine(list.ToArray(), staticGoal.gameObject);
	}

	public void StartStatic(LevelEntity entity)
	{
		if (entity.behaviour.hasLogic)
		{
			XDataHolder data = new XDataHolder();
			entity.behaviour.OnSaveLogic(data);
			entity.behaviour.startingSim = true;
			entity.behaviour.OnLoadLogic(data, true);
			entity.behaviour.startingSim = false;
		}
		else
		{
			entity.behaviour.ClearStaticLogic();
		}
		AddStaticEntity(entity);
		if (entity.behaviour.ActiveOnStart())
		{
			entity.ActivateKinematic();
		}
		else
		{
			entity.StartDeactivated();
		}
		if (isTracking && entity.behaviour.IsGlobalVarTarget)
		{
			AddGlobalVarTarget(entity);
		}
		entity.isSimulating = true;
		if (entity.hasFireController && entity.behaviour.prefab.playFireWhenStatic)
		{
			entity.fireController.StartStatic();
		}
	}

	public void StartSim()
	{
		if (StatMaster._customLevelSimulating)
		{
			return;
		}
		StatMaster._customLevelSimulating = true;
		DecalHandler.ResetAffectedObjects();
		StatMaster.UpdateSimulationState();
		StatMaster.isLocalSim = StatMaster.InLocalPlayMode;
		isTracking = StatMaster.isHosting || StatMaster.isLocalSim;
		ReferenceMaster.physicsGoalInstance = (physInstance = (UnityEngine.Object.Instantiate(physGoal.gameObject, physGoal.position, physGoal.rotation, physGoal.parent) as GameObject).transform);
		ReferenceMaster.physicsGoalInstance.name = "PHYSICS GOAL";
		physGoal.gameObject.SetActive(false);
		if (networkAuxAddPiece == null)
		{
			networkAuxAddPiece = NetworkAuxAddPiece.Instance;
		}
		entityCount = 0;
		baseEntities.Clear();
		List<LevelEntity> sortedEntities = levelEditor.SortedEntities;
		for (int i = 0; i < sortedEntities.Count; i++)
		{
			LevelEntity levelEntity = sortedEntities[i];
			if (levelEntity.isStatic)
			{
				StartStatic(levelEntity);
				continue;
			}
			Transform child = physInstance.GetChild(levelEntity.transform.GetSiblingIndex());
			GameObject gameObject = child.gameObject;
			if (!gameObject.activeSelf)
			{
				gameObject.SetActive(true);
			}
			LevelEntity levelEntity2 = (levelEntity.simEntity = gameObject.GetComponent<LevelEntity>());
			levelEntity2.buildEntity = levelEntity;
			levelEntity2.behaviour.startingSim = true;
			levelEntity2.Init((uint)entityCount++, networkController, levelEntity, isTracking);
			levelEntity2.behaviour.RemoveBoundingBox();
			levelEntity2.behaviour.startingSim = false;
			baseEntities.Add(levelEntity2);
			networkController.Add(levelEntity2);
			entityCount += levelEntity2.BreakCount;
			if (!levelEntity.behaviour.ActiveOnStart())
			{
				levelEntity2.StartDeactivated();
			}
			if (isTracking && levelEntity.behaviour.IsGlobalVarTarget)
			{
				AddGlobalVarTarget(levelEntity2);
			}
			for (int j = 0; j < levelEntity2.children.Length; j++)
			{
				if (!levelEntity2.hasBehaviour || !levelEntity2.behaviour.IsMultiLook || levelEntity2.children[j].gameObject.activeSelf)
				{
					LevelEntity levelEntity3 = levelEntity2.children[j] as LevelEntity;
					LevelEntity levelEntity4 = levelEntity.children[j] as LevelEntity;
					levelEntity4.simEntity = levelEntity3;
					levelEntity3.buildEntity = levelEntity4;
					levelEntity3.Init((uint)entityCount++, networkController, levelEntity4, isTracking);
					networkController.Add(levelEntity3);
					entityCount += levelEntity3.BreakCount;
				}
			}
		}
		if (!hasSimFrame)
		{
			networkController.SetCapacity(entityCount);
			networkController.FillStaticArray();
		}
		else
		{
			int num = 0;
			int num2 = 0;
			for (int j = 0; j < baseEntities.Count; j++)
			{
				LevelEntity levelEntity2 = baseEntities[j];
				if (skipMap.Contains(levelEntity2.identifier))
				{
					levelEntity2.buildEntity.simEntity = null;
					baseEntities.Remove(levelEntity2);
					if (levelEntity2.needsTracking)
					{
						RemoveSimTrack(levelEntity2);
					}
					for (int k = 0; k < levelEntity2.children.Length; k++)
					{
						LevelEntity levelEntity5 = levelEntity2.children[k] as LevelEntity;
						if (levelEntity5.needsTracking)
						{
							RemoveSimTrack(levelEntity5);
						}
						levelEntity5.isDestroyed = true;
					}
					levelEntity2.isDestroyed = true;
					UnityEngine.Object.DestroyImmediate(levelEntity2.gameObject);
					j--;
					continue;
				}
				int value;
				if (idCorrectionMap.TryGetValue(levelEntity2.identifier, out value))
				{
					num2 = value;
					if (num < num2)
					{
						num = num2;
					}
				}
				if (num2 != 0)
				{
					levelEntity2.CorrectID(num2);
				}
			}
			networkController.SetCapacity(entityCount + num2);
			networkController.FillStaticArray();
			networkController.ApplySimFrame();
			List<int> list = new List<int>(simPhysData.Keys);
			for (int j = 0; j < list.Count; j++)
			{
				int num3 = list[j];
				if (num3 < sortedEntities.Count)
				{
					LevelEntity levelEntity2 = sortedEntities[num3];
					if (levelEntity2.simEntity != null)
					{
						levelEntity2 = levelEntity2.simEntity;
						levelEntity2.SetLogicStateFrame(simPhysData[num3]);
						levelEntity2.OnSimFrameApplied(timeCorrection);
					}
					else
					{
						Debug.LogError("PhysKey for " + num3 + " doesn't have a simEntity, couldn't apply logic state!");
					}
				}
				else
				{
					Debug.LogError("Phys key out of range when reading sim frame!");
				}
			}
			for (int j = 0; j < staticEntities.Count; j++)
			{
				LevelEntity levelEntity6 = staticEntities[j];
				levelEntity6.OnSimFrameApplied(timeCorrection);
			}
		}
		if (isTracking && StatMaster.useSmartInterpolation && TimeSlider.Instance.delegateTimeScale >= 0.6f)
		{
			Rigidbody[] componentsInChildren = physInstance.GetComponentsInChildren<Rigidbody>();
			Rigidbody[] array = componentsInChildren;
			foreach (Rigidbody rigidbody in array)
			{
				if (!rigidbody.isKinematic)
				{
					rigidbody.interpolation = RigidbodyInterpolation.None;
				}
			}
		}
		networkController.InitSim(isTracking);
		if (isTracking || hasSimFrame)
		{
			for (int j = 0; j < baseEntities.Count; j++)
			{
				if (isTracking)
				{
					baseEntities[j].OnSimulationStart();
				}
				else
				{
					baseEntities[j].OnSimLogicStart(timeCorrection);
				}
			}
			for (int j = 0; j < staticEntities.Count; j++)
			{
				LevelEntity levelEntity7 = staticEntities[j];
				if (isTracking)
				{
					if (!levelEntity7.isBuildZone)
					{
						levelEntity7.OnSimulationStart();
					}
				}
				else
				{
					levelEntity7.OnSimLogicStart(timeCorrection);
				}
			}
			hasSimFrame = false;
		}
		projectileManager.InitSim(isTracking);
		if (StatMaster.isClient && StatMaster.isLocalSim)
		{
			return;
		}
		uint cacheFrame;
		FrameBufferManager.CacheEntry cacheEntry;
		while (frameManager.GetOldestCache(Session, out cacheFrame, out cacheEntry))
		{
			dataManager.UnpackData(cacheFrame, Session, cacheEntry.data);
			NewFrame(cacheFrame);
		}
		float time = Time.time;
		while (logicFrameManager.GetOldestCache(Session, out cacheFrame, out cacheEntry))
		{
			if (cacheFrame == logicFrame)
			{
				levelEditor.ExecuteLogicData(cacheEntry.data, time - cacheEntry.createTime);
				logicFrame++;
			}
		}
	}

	private bool IsRunningEvent(ushort eventID, ushort logicID, long entityID, out LogicProgressEvent logicEvent)
	{
		for (int i = 0; i < progressEvents.Count; i++)
		{
			LogicProgressEvent logicProgressEvent = progressEvents[i];
			if (logicProgressEvent.evt.ID == eventID && logicProgressEvent.logic.ID == logicID && logicProgressEvent.logic.entityBehaviour.entity.identifier == entityID)
			{
				logicEvent = logicProgressEvent;
				return true;
			}
		}
		logicEvent = null;
		return false;
	}

	private bool IsRunningLogic(ushort logicID, long entityID, out LogicProgressEvent logicEvent)
	{
		for (int i = 0; i < progressEvents.Count; i++)
		{
			LogicProgressEvent logicProgressEvent = progressEvents[i];
			if (logicProgressEvent.logic.ID == logicID && logicProgressEvent.logic.entityBehaviour.entity.identifier == entityID)
			{
				logicEvent = logicProgressEvent;
				return true;
			}
		}
		logicEvent = null;
		return false;
	}

	public void StartProgressEvent(EntityLogic logic, EntityEvent e, float progress, float timeCorrection)
	{
		EventContainer eventData = e.eventData;
		eventData.Reset();
		eventData.Execute();
		if (!eventData.isDone)
		{
			eventData.SetProgress(progress, timeCorrection);
			LogicProgressEvent logicEvent;
			if (eventData.isDone)
			{
				eventData.isDone = false;
			}
			else if (IsRunningLogic(logic.ID, logic.entityBehaviour.entity.identifier, out logicEvent))
			{
				logicEvent.evt = e;
			}
			else
			{
				progressEvents.Add(new LogicProgressEvent(logic, e));
			}
		}
	}

	public void StopProgressEvent(EntityLogic logic, EntityEvent e, float progress)
	{
		LogicProgressEvent logicEvent;
		if (IsRunningEvent(e.ID, logic.ID, logic.entityBehaviour.entity.identifier, out logicEvent))
		{
			EventContainer eventData = e.eventData;
			eventData.SetProgress(progress, 0f);
			eventData.Stop();
			eventData.Reset();
			progressEvents.Remove(logicEvent);
		}
	}

	public void AddHiddenStatic(LevelEntity staticEntity)
	{
		if (!staticEntity.addedToHiddenStatic)
		{
			staticEntity.addedToHiddenStatic = true;
			hiddenStatic.Add(staticEntity);
		}
	}

	public void RemoveHiddenStatic(LevelEntity staticEntity)
	{
		if (staticEntity.addedToHiddenStatic)
		{
			staticEntity.addedToHiddenStatic = false;
			hiddenStatic.Remove(staticEntity);
		}
	}

	public void AddStaticEntity(LevelEntity staticEntity)
	{
		if (!staticEntity.addedToStatic)
		{
			staticEntity.addedToStatic = true;
			staticEntities.Add(staticEntity);
		}
	}

	public void RemoveStaticEntity(LevelEntity staticEntity)
	{
		if (staticEntity.addedToStatic)
		{
			staticEntity.addedToStatic = false;
			staticEntities.Remove(staticEntity);
		}
	}

	public void StopSim()
	{
		if (!StatMaster._customLevelSimulating)
		{
			return;
		}
		logicFrame = 0u;
		countdownManager.ClearAll();
		StatMaster._customLevelSimulating = false;
		StatMaster.UpdateSimulationState();
		StatMaster.isLocalSim = false;
		if (ReferenceMaster.physicsGoalInstance != null)
		{
			UnityEngine.Object.Destroy(ReferenceMaster.physicsGoalInstance.gameObject);
		}
		while (updatingLogicEntities.Count > 0)
		{
			updatingLogicEntities[0].StopLogic();
		}
		while (progressEvents.Count > 0)
		{
			EntityEvent evt = progressEvents[0].evt;
			evt.eventData.Stop();
			evt.Reset();
			progressEvents.RemoveAt(0);
		}
		globalVarTargets.Clear();
		for (int i = 0; i < staticEntities.Count; i++)
		{
			LevelEntity levelEntity = staticEntities[i];
			if (!levelEntity.hasSpawned)
			{
				levelEntity.ActivateKinematic();
			}
			levelEntity.addedToStatic = false;
			levelEntity.isSimulating = false;
			levelEntity.ResetLogic();
			levelEntity.ResetTransform();
			levelEntity.addedToGlobalVarTargets = false;
			levelEntity.transformTarget = false;
			if (levelEntity.hasFireController && levelEntity.behaviour.prefab.playFireWhenStatic && !levelEntity.behaviour.prefab.PlayFireWhenBuilding)
			{
				levelEntity.fireController.StopStatic();
			}
			if (!StatMaster.Mode.levelEdit && !levelEntity.behaviour.ActiveOnStart())
			{
				levelEntity.gameObject.SetActive(false);
			}
		}
		staticEntities.Clear();
		hiddenStatic.Clear();
		if (variables.Count > 0)
		{
			variables.Clear();
		}
		entityCount = 0;
		physGoal.gameObject.SetActive(true);
		networkController.Clear();
		trackedSimEntities.Clear();
		clientLerpTargets.Clear();
		progressEvents.Clear();
		startFrame = 0u;
	}

	public byte[] Encode(string levelData, string levelName)
	{
		byte[] bytes = Encoding.UTF8.GetBytes((!string.IsNullOrEmpty(levelName)) ? levelName : string.Empty);
		byte[] bytes2 = Encoding.UTF8.GetBytes(levelData);
		int num = NetworkCompression.PackedUIntLength(bytes.Length, true);
		byte[] array = new byte[num + bytes.Length + bytes2.Length];
		int num2 = 0;
		NetworkCompression.PackUInt(bytes.Length, array, num2, true, num);
		num2 += num;
		Buffer.BlockCopy(bytes, 0, array, num2, bytes.Length);
		num2 += bytes.Length;
		Buffer.BlockCopy(bytes2, 0, array, num2, bytes2.Length);
		return array;
	}

	public string SaveLevel()
	{
		StringWriter stringWriter = new StringWriter();
		XmlWriter xmlWriter = XmlWriter.Create(stringWriter);
		LevelXMLSaver.WriteLevel(xmlWriter, string.Empty);
		xmlWriter.Close();
		string result = stringWriter.ToString();
		stringWriter.Close();
		return result;
	}

	public byte[] Encode(string levelName)
	{
		string levelData = SaveLevel();
		return Encode(levelData, levelName);
	}

	public string Decode(byte[] data, out string levelName)
	{
		int count;
		int num = NetworkCompression.UnpackUInt(data, 0, true, out count);
		levelName = Encoding.UTF8.GetString(data, num, count);
		num += count;
		return Encoding.UTF8.GetString(data, num, data.Length - num);
	}
}
