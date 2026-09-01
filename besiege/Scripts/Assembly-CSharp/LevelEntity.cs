using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

[AddComponentMenu("LevelEditor/LevelEntity")]
public class LevelEntity : NetworkBlock, ISelectable
{
	public static int ID_LENGTH = 8;

	private static System.Random randomGenerator = new System.Random();

	private bool isSelected;

	public bool isStatic = true;

	public bool transformTarget;

	public bool activePoll = true;

	public GenericEntity behaviour;

	public bool hasBehaviour;

	public bool usePositionAsCenter;

	public SetCenterOfMass[] scaleCallbacks = new SetCenterOfMass[0];

	[NonSerialized]
	public bool HasInfo;

	[NonSerialized]
	public BasicInfo bInfo;

	[NonSerialized]
	public bool isCached;

	[NonSerialized]
	public bool isBuildZone;

	[NonSerialized]
	public bool isWaterZone;

	[NonSerialized]
	public Transform lerpTransformTarget;

	[NonSerialized]
	public bool isLerpTarget;

	[NonSerialized]
	public bool hasLerpTarget;

	[NonSerialized]
	public bool needsTracking;

	[NonSerialized]
	public bool hasSpawned;

	[NonSerialized]
	public LevelEntity simEntity;

	[NonSerialized]
	public LevelEntity buildEntity;

	[NonSerialized]
	public bool isSimulating;

	[NonSerialized]
	public bool addedToStatic;

	[NonSerialized]
	public bool addedToHiddenStatic;

	[NonSerialized]
	public bool addedToGlobalVarTargets;

	[NonSerialized]
	public bool changedScale;

	[NonSerialized]
	public bool isLerping;

	[NonSerialized]
	private bool updatingRigidBody;

	[NonSerialized]
	private bool needsRigidUpdate;

	protected long _identifier = LevelPrefab.INVALID_ID;

	private LevelEditor levelEditor;

	private EntityController entityController;

	private NetworkInterpolation scaleTracker;

	private Vector3 baseScale;

	private Vector3 invBaseScale;

	private LevelEntity original;

	private CustomLevel level;

	private bool isInsignia;

	private bool hasBroken;

	private bool isModified;

	private List<LevelEntity> deactivatedChildren = new List<LevelEntity>();

	private bool hasLogicState;

	private EntityLogicState logicState;

	private Vector3 _lastReceivedPos = default(Vector3);

	private Quaternion _lastReceivedRot = default(Quaternion);

	private Vector3 _lastReceivedScale = default(Vector3);

	private Vector3 _firstPosition = default(Vector3);

	private Quaternion _firstRotation = default(Quaternion);

	private Vector3 _firstScale = default(Vector3);

	private int[] wasInterpolating = new int[0];

	public bool IsSelected
	{
		get
		{
			return isSelected;
		}
		set
		{
			isSelected = value;
			if (!isSelected)
			{
				IsSelectedExtra = false;
			}
		}
	}

	public bool IsSelectedExtra { get; set; }

	public int SymmetryIndex { get; set; }

	public float TransformMultiplier { get; set; }

	public bool IsDestroyed
	{
		get
		{
			return isDestroyed;
		}
		set
		{
			isDestroyed = value;
		}
	}

	public long identifier
	{
		get
		{
			return _identifier;
		}
		set
		{
			_identifier = value;
		}
	}

	public bool CanRemove
	{
		get
		{
			return !(behaviour is BuildZoneObject) || !(behaviour as BuildZoneObject).hasZone;
		}
	}

	public GenericEntity EntityBehaviour
	{
		get
		{
			return behaviour;
		}
	}

	public Vector3 Offset
	{
		get
		{
			return base.transform.TransformVector(behaviour.prefab.offset);
		}
	}

	public Vector3 FirstPosition
	{
		get
		{
			return _firstPosition;
		}
	}

	public Vector3 LastPosition
	{
		get
		{
			return _lastReceivedPos;
		}
	}

	public Quaternion FirstRotation
	{
		get
		{
			return _firstRotation;
		}
	}

	public Quaternion LastRotation
	{
		get
		{
			return _lastReceivedRot;
		}
	}

	public Vector3 FirstScale
	{
		get
		{
			return _firstScale;
		}
	}

	public Vector3 Scale
	{
		get
		{
			return scaleTracker.lastVec;
		}
	}

	public Vector3 LastScale
	{
		get
		{
			return _lastReceivedScale;
		}
	}

	public void LoadEntityData(XDataHolder data)
	{
		behaviour.OnLoad(data);
	}

	public void ToggleStatic(bool s)
	{
		ToggleStatic(base.transform, s);
	}

	private void ToggleStatic(Transform t, bool s)
	{
		t.gameObject.isStatic = s;
		for (int i = 0; i < t.childCount; i++)
		{
			ToggleStatic(t.GetChild(i), s);
		}
	}

	public void ReplaceEntityReference(long oldReference, long newIdentifier)
	{
		if (hasBehaviour)
		{
			behaviour.ReplaceEntityReference(oldReference, newIdentifier);
		}
	}

	public void RemoveIncompatibleTriggers()
	{
		if (hasBehaviour)
		{
			behaviour.RemoveIncompatibleTriggers();
		}
	}

	public void OnXMLLoad()
	{
		if (!StatMaster.Mode.levelEdit && !behaviour.ActiveOnStart())
		{
			base.gameObject.SetActive(false);
		}
	}

	public XDataHolder GetEntityData()
	{
		XDataHolder xDataHolder = new XDataHolder();
		behaviour.OnSave(xDataHolder);
		return xDataHolder;
	}

	protected virtual void AwakeChild()
	{
		if (hasBase)
		{
			bInfo = GetComponent<BasicInfo>();
			HasInfo = bInfo != null;
			if (HasInfo && bInfo.NetBlock == null)
			{
				bInfo.NetBlock = this;
			}
		}
	}

	protected override void AwakeBase()
	{
		if (!StatMaster.isMP)
		{
			UnityEngine.Object.Destroy(this);
		}
		else if (!isAwake)
		{
			levelEditor = LevelEditor.Instance;
			scaleTracker = new NetworkInterpolation();
			base.AwakeBase();
			sendEntity = new SendEntity(false);
			for (int i = 0; i < children.Length; i++)
			{
				LevelEntity levelEntity = children[i] as LevelEntity;
				levelEntity.AwakeChild();
			}
		}
	}

	protected void Start()
	{
		if (!StatMaster.isMP)
		{
			UnityEngine.Object.Destroy(this);
		}
		else if (!isAwake && hasBehaviour && behaviour.prefab.canScale && behaviour.MeshRenderer != null)
		{
			MeshFilter component = behaviour.MeshRenderer.GetComponent<MeshFilter>();
			behaviour.prefab.canScale = component.sharedMesh.vertexCount > 3;
			if (!behaviour.prefab.canScale)
			{
				Debug.LogError(base.transform.name + " cant be scaled due to lack of vertecies");
			}
		}
	}

	public void StartSimTrack(bool trackMovement)
	{
		if (!needsTracking && isSimulating)
		{
			level.AddSimTrack(this);
			needsTracking = true;
			if (trackMovement)
			{
				pollTransform = true;
				activePoll = true;
			}
		}
	}

	public void StopSimTrack()
	{
		if (needsTracking)
		{
			level.RemoveSimTrack(this);
			needsTracking = false;
			activePoll = false;
			pollTransform = false;
		}
	}

	public void OnRemove()
	{
		MouseOrbit instance = SingleInstanceFindOnly<MouseOrbit>.Instance;
		if (hasBehaviour && instance.targetType == MouseOrbit.TargetType.Entity && instance.targetInfo == behaviour)
		{
			instance.SoftResetCamTarget();
		}
		if (isSimulating && isStatic)
		{
			if (hasBehaviour && behaviour.hasRunningLogic)
			{
				behaviour.StopLogic();
			}
			level.RemoveStaticEntity(this);
		}
		if (hasBehaviour && level != null)
		{
			level.RemoveGlobalVarTarget(this);
			level.RemoveTrack(this);
		}
		StopSimTrack();
		for (int i = 0; i < children.Length; i++)
		{
			LevelEntity levelEntity = children[i] as LevelEntity;
			levelEntity.OnRemove();
		}
		if (isModified)
		{
			ResetTransform();
		}
		if (hasBehaviour)
		{
			behaviour.OnRemove();
		}
		if (hasChildManager)
		{
			childManager.ClearChildren();
		}
	}

	public void SetData(bool setPos, Vector3 pos, bool setRot, Quaternion rot)
	{
		if (!needsTracking && (setPos || setRot))
		{
			level.AddSimTrack(this);
			needsTracking = true;
		}
		if (setPos)
		{
			posTracker.Set(pos);
			hasChangedPos = true;
		}
		if (setRot)
		{
			rotTracker.Set(rot);
			hasChangedRot = true;
		}
	}

	public override void SetData(uint frame, byte[] data, int offset, bool hasPos, bool hasRot, int eventCount)
	{
		if (!isDestroyed)
		{
			StartSimTrack(false);
			isLerpTarget = false;
			base.SetData(frame, data, offset, hasPos, hasRot, eventCount);
		}
	}

	public void Init()
	{
		AwakeBase();
		_firstPosition = (_lastReceivedPos = base.transform.position);
		_firstRotation = (_lastReceivedRot = base.transform.rotation);
		_firstScale = (_lastReceivedScale = base.transform.localScale);
		posTracker.SetData(baseInterval, _lastReceivedPos);
		rotTracker.SetData(baseInterval, _lastReceivedRot);
		scaleTracker.SetData(baseInterval, _lastReceivedScale);
		posTracker.SetPrediction(false);
		rotTracker.SetPrediction(false);
		scaleTracker.SetPrediction(false);
		level = CustomLevel.Instance;
		behaviour.Init();
		behaviour.OnAdd();
		behaviour.SaveInitialData();
		behaviour.Reset();
		if (behaviour.prefab != null && behaviour.prefab.ignorePhysics)
		{
			isBuildZone = behaviour is BuildZoneObject;
			hasSpawned = true;
		}
		isWaterZone = behaviour is WaterZoneEntity;
	}

	public Vector3 GetCenter()
	{
		if (isDestroyed || usePositionAsCenter)
		{
			return base.Position;
		}
		if (hasBehaviour)
		{
			if (behaviour.hasBoundingBox)
			{
				return behaviour.boundingBox.GetCenter();
			}
			return behaviour.GetCenter();
		}
		if (HasInfo)
		{
			return bInfo.GetCenter();
		}
		return base.Position;
	}

	public Transform GetTransform()
	{
		return base.transform;
	}

	public string LogicName()
	{
		return behaviour.LogicName();
	}

	public void SetupDefault()
	{
		behaviour.SetupDefault();
	}

	public void StartDeactivated()
	{
		DeactivateEntity(false);
		if (isStatic)
		{
			isTracking = StatMaster.isHosting || StatMaster.isLocalSim;
		}
	}

	public void DeactivateEntity()
	{
		DeactivateEntity(true);
	}

	private void DeactivateObj(LevelEntity ent)
	{
		if (ent.hasChildManager)
		{
			EntityChildManager entityChildManager = ent.childManager;
			for (int num = entityChildManager.children.Count - 1; num >= 0; num--)
			{
				LevelEntity levelEntity = entityChildManager.children[num] as LevelEntity;
				levelEntity.manualDeactivate = true;
				if (levelEntity.needsTracking)
				{
					level.RemoveSimTrack(levelEntity);
				}
				if (levelEntity.gameObject.activeSelf)
				{
					levelEntity.gameObject.SetActive(false);
					deactivatedChildren.Add(levelEntity);
				}
			}
		}
		if (ent.needsTracking)
		{
			level.RemoveSimTrack(ent);
		}
		if (ent.gameObject.activeSelf)
		{
			ent.manualDeactivate = true;
			ent.gameObject.SetActive(false);
			deactivatedChildren.Add(ent);
		}
	}

	public void DeactivateEntity(bool fullDeactivation)
	{
		if (!hasSpawned)
		{
			return;
		}
		manualDeactivate = true;
		deactivatedChildren.Clear();
		if (!isInsignia && !isStatic)
		{
			for (int i = 0; i < children.Length; i++)
			{
				LevelEntity ent = ((i >= children.Length) ? this : (children[children.Length - (i + 1)] as LevelEntity));
				DeactivateObj(ent);
			}
			DeactivateObj(this);
		}
		if (isTracking)
		{
			level.RemoveGlobalVarTarget(this);
		}
		if (!isStatic)
		{
			for (int j = 0; j < deactivatedChildren.Count; j++)
			{
				deactivatedChildren[j].manualDeactivate = false;
			}
		}
		manualDeactivate = false;
		if (fullDeactivation)
		{
			if (hasBehaviour)
			{
				behaviour.DeactivateEntity();
				behaviour.StopLogic();
			}
			TriggerEvent(TriggerType.Deactivate);
		}
		else
		{
			base.gameObject.SetActive(false);
		}
		hasSpawned = false;
	}

	public void ActivateKinematic()
	{
		if (isStatic)
		{
			base.gameObject.SetActive(true);
			hasSpawned = true;
			isTracking = StatMaster.isHosting || StatMaster.isLocalSim;
		}
	}

	public void ActivateEntity(uint frame)
	{
		if (hasSpawned)
		{
			return;
		}
		hasSpawned = true;
		if (!isInsignia && !isStatic)
		{
			for (int i = 0; i < deactivatedChildren.Count; i++)
			{
				LevelEntity levelEntity = deactivatedChildren[i];
				levelEntity.gameObject.SetActive(true);
				if (levelEntity.needsTracking)
				{
					level.AddSimTrack(levelEntity);
				}
			}
		}
		behaviour.ActivateEntity();
		if (isTracking)
		{
			level.AddGlobalVarTarget(this);
			if (hasBehaviour)
			{
				behaviour.TriggerActivate(false);
			}
		}
	}

	public override void BreakIntoChildren(Transform breakInstance)
	{
		hasBroken = true;
		if (breakInstance == null)
		{
			activePoll = true;
			pollTransform = true;
			if (!needsTracking)
			{
				level.AddSimTrack(this);
				needsTracking = true;
			}
		}
		else
		{
			if (hasChildManager)
			{
				LevelEntity component = breakInstance.GetComponent<LevelEntity>();
				if (component == null)
				{
					Debug.LogError(breakInstance.name + ": break Instance is missing a LevelEntity");
					return;
				}
				childManager.InitLevelChildren(component);
			}
			if (NetworkBlock.applyingState)
			{
				base.gameObject.SetActive(false);
			}
		}
		TriggerEvent(TriggerType.Destroy);
	}

	public void CorrectID(int correction)
	{
		if (addedToController)
		{
			networkController.Remove(this);
		}
		id = (uint)(id + correction);
		networkController.Replace(this, id);
		for (int i = 0; i < children.Length; i++)
		{
			LevelEntity levelEntity = children[i] as LevelEntity;
			if (levelEntity.addedToController)
			{
				networkController.Remove(levelEntity);
			}
			levelEntity.id = (uint)(levelEntity.id + correction);
			networkController.Replace(levelEntity, levelEntity.id);
		}
	}

	public void ResetEntity(uint frame)
	{
		if (!hasSpawned)
		{
			return;
		}
		if (hasBehaviour)
		{
			EventContainer.TransformEvent currentTransformEvent = behaviour.currentTransformEvent;
			if (currentTransformEvent != null)
			{
				for (int i = 0; i < currentTransformEvent.entityList.Length; i++)
				{
					if (currentTransformEvent.entityList[i].entity.identifier == identifier)
					{
						currentTransformEvent.entityList[i].Complete(currentTransformEvent.transformType);
						break;
					}
				}
				behaviour.currentTransformEvent = null;
			}
			ResetLogic();
		}
		bool flag = behaviour.ActiveOnStart();
		if (isStatic)
		{
			if (hasBehaviour)
			{
				if (!flag)
				{
					StartDeactivated();
				}
				if (behaviour.hasRunningLogic)
				{
					for (int j = 0; j < behaviour.runningLogic.Count; j++)
					{
						EntityLogic entityLogic = behaviour.runningLogic[j];
						global::EntityEvent entityEvent = entityLogic.events[entityLogic.currentIndex];
						if (!flag || entityEvent.eventType != EventContainer.EventType.Reset || entityLogic.currentIndex == entityLogic.events.Count - 1)
						{
							behaviour.StopLogic(entityLogic);
						}
					}
				}
			}
			ResetTransform();
			if (StatMaster.isHosting)
			{
				ProjectileManager.Instance.DespawnParentedProjectiles(base.transform);
			}
			return;
		}
		if (myTransform == null)
		{
			Debug.LogError("Trying to reset null object '" + base.name + "'!");
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(original.gameObject, myTransform.parent, true) as GameObject;
		if (!gameObject.activeSelf)
		{
			gameObject.SetActive(true);
		}
		gameObject.transform.SetSiblingIndex(original.transform.GetSiblingIndex());
		LevelEntity component = gameObject.GetComponent<LevelEntity>();
		component.name = base.name;
		uint num = id;
		component.behaviour.startingSim = true;
		component.behaviour.RemoveBoundingBox();
		component.Init(num++, networkController, original, isTracking);
		component.behaviour.startingSim = false;
		networkController.Replace(this, component);
		num += (uint)component.BreakCount;
		if (needsTracking)
		{
			level.RemoveSimTrack(this);
			needsTracking = false;
		}
		if (isTracking)
		{
			if (addedToGlobalVarTargets)
			{
				level.RemoveGlobalVarTarget(this);
				level.AddGlobalVarTarget(component);
			}
			if (StatMaster.isHosting)
			{
				ProjectileManager.Instance.DespawnParentedProjectiles(base.transform);
			}
		}
		for (uint num2 = 0u; num2 < children.Length; num2++)
		{
			LevelEntity levelEntity = children[num2] as LevelEntity;
			LevelEntity levelEntity2 = component.children[num2] as LevelEntity;
			levelEntity2.Init(num++, networkController, levelEntity, isTracking);
			networkController.Replace(children[num2], levelEntity2);
			num += (uint)levelEntity2.BreakCount;
			if (levelEntity.needsTracking)
			{
				level.RemoveSimTrack(levelEntity);
				levelEntity.needsTracking = false;
			}
			if (levelEntity.hasChildManager)
			{
				levelEntity.childManager.ClearChildren();
			}
			levelEntity.isDestroyed = true;
			Transform transform = levelEntity.transform;
			if (!transform.IsChildOf(base.transform))
			{
				ProjectileManager.Instance.DespawnParentedProjectiles(transform);
				UnityEngine.Object.Destroy(levelEntity.gameObject);
			}
		}
		if (hasChildManager)
		{
			childManager.ClearChildren();
		}
		original.simEntity = component;
		component.buildEntity = original;
		component.behaviour.variables = behaviour.variables;
		component.lastPosFrame = frame;
		component.lastRotFrame = frame;
		if (!component.behaviour.noRigidbody)
		{
			component.behaviour.WakeUpRigidbody(1);
		}
		if (hasBehaviour)
		{
			if (!flag)
			{
				component.StartDeactivated();
			}
			else if (component.behaviour is AIGenericEntity)
			{
				component.behaviour.TriggerEvent(TriggerType.Behaviour);
			}
			if (behaviour.hasRunningLogic)
			{
				for (int k = 0; k < behaviour.runningLogic.Count; k++)
				{
					EntityLogic entityLogic2 = behaviour.runningLogic[k];
					global::EntityEvent entityEvent2 = entityLogic2.events[entityLogic2.currentIndex];
					behaviour.StopLogic(entityLogic2);
					if (flag && entityLogic2.currentIndex + 1 < entityLogic2.events.Count && entityEvent2.eventType == EventContainer.EventType.Reset)
					{
						entityLogic2.entityBehaviour = component.behaviour;
						entityLogic2.isRunning = true;
						entityLogic2.currentIndex++;
						component.behaviour.AddRunningLogic(entityLogic2);
					}
				}
			}
		}
		isDestroyed = true;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void SetLogicStateFrame(EntityLogicState logicStateFrame)
	{
		logicState = logicStateFrame;
		hasLogicState = true;
	}

	public void OnSimFrameApplied(float timeCorrection)
	{
		if (!hasLogicState)
		{
			return;
		}
		if (logicState.stateChanged)
		{
			if (hasSpawned)
			{
				DeactivateEntity(false);
			}
			else
			{
				ActivateEntity(0u);
			}
		}
		if (logicState.hasPosition)
		{
			base.transform.position = logicState.position;
		}
		if (logicState.hasRotation)
		{
			base.transform.rotation = logicState.rotation;
		}
		if (logicState.hasScale)
		{
			base.transform.localScale = logicState.scale;
		}
	}

	public void OnSimLogicStart(float timeCorrection)
	{
		if (hasLogicState)
		{
			if (logicState.hasRunningLogic)
			{
				behaviour.ApplyLogicState(logicState.runningLogic, timeCorrection);
			}
			hasLogicState = false;
		}
	}

	public void SetLerping(bool toggle)
	{
		isLerping = toggle;
		if (StatMaster.isClient && !StatMaster.isLocalSim && isLerping)
		{
			if (!hasLerpTarget)
			{
				lerpTransformTarget = new GameObject().transform;
				level = CustomLevel.Instance;
				if (level.serverTransformParent == null)
				{
					Debug.LogWarning("Transform parent is null on LevelEntity (" + Machine.GetObjectPath(base.gameObject) + ") when calling SetLerping");
					level.CreateTransformParent();
				}
				lerpTransformTarget.parent = level.serverTransformParent;
				hasLerpTarget = true;
			}
			level.AddLerpTarget(this);
		}
		for (int i = 0; i < children.Length; i++)
		{
			LevelEntity levelEntity = children[i] as LevelEntity;
			levelEntity.SetLerping(toggle);
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		RemoveLerpTarget();
	}

	public void RemoveLerpTarget()
	{
		if (hasLerpTarget && !(lerpTransformTarget == null))
		{
			UnityEngine.Object.Destroy(lerpTransformTarget.gameObject);
			hasLerpTarget = false;
		}
	}

	public void Init(uint blockIdentifier, NetworkController controller, LevelEntity orig, bool track)
	{
		AwakeBase();
		if (!track)
		{
			posTracker.SetPrediction(true);
			rotTracker.SetPrediction(true);
		}
		original = orig;
		base.Init(blockIdentifier, controller, base.transform, track);
		hasBroken = false;
		level = CustomLevel.Instance;
		isSimulating = true;
		isStatic = false;
		if (!hasBase)
		{
			isStatic = original.isStatic;
			hasSpawned = true;
			_identifier = original.identifier;
			_lastReceivedPos = original._lastReceivedPos;
			_lastReceivedRot = original._lastReceivedRot;
			_lastReceivedScale = original._lastReceivedScale;
			isInsignia = behaviour is InsigniaTrigger;
			behaviour.Init();
			behaviour.OnLoad(original.GetEntityData());
		}
		bool flag = true;
		if (hasBase)
		{
			LevelEntity levelEntity = baseEntity as LevelEntity;
			if (levelEntity.hasBase)
			{
				levelEntity = levelEntity.baseEntity as LevelEntity;
			}
			if (levelEntity.hasBehaviour)
			{
				flag = levelEntity.behaviour.PhysicsEnabled;
			}
		}
		else if (hasBehaviour)
		{
			flag = behaviour.PhysicsEnabled;
		}
		if (flag)
		{
			StructuralPhysTile component = GetComponent<StructuralPhysTile>();
			bool flag2 = component != null;
			BreakOnForce component2 = GetComponent<BreakOnForce>();
			bool flag3 = component2 != null;
			EntityAI component3 = GetComponent<EntityAI>();
			bool flag4 = component3 != null && component3.my.killingHandler != null && component3.my.killingHandler.UseGibPrefab && component3.my.killingHandler.my.GibPrefab.GetComponent<LevelEntity>() != null;
			if (flag2 || flag3 || flag4)
			{
				childManager = new EntityChildManager(this, controller, isTracking);
				hasChildManager = true;
				NetworkBlock networkBlock = null;
				bool flag5 = false;
				if (flag3)
				{
					if ((bool)component2.BreakInto)
					{
						flag5 = true;
						networkBlock = component2.BreakInto.GetComponent<NetworkBlock>();
					}
				}
				else if (flag2 && (bool)component.brokenBlock)
				{
					flag5 = true;
					networkBlock = component.brokenBlock.GetComponent<NetworkBlock>();
				}
				else if (flag4)
				{
					flag5 = true;
					networkBlock = component3.my.killingHandler.my.GibPrefab.GetComponent<NetworkBlock>();
				}
				if (flag5)
				{
					if (networkBlock != null)
					{
						BreakCount = 1 + networkBlock.children.Length;
					}
					else
					{
						Debug.LogWarning("BreakInto doesn't have LevelEntity: " + base.name);
					}
				}
			}
		}
		if (track)
		{
			Rigidbody component4 = GetComponent<Rigidbody>();
			if (component4 != null)
			{
				LevelEntity levelEntity2 = ((!hasBase) ? this : (baseEntity as LevelEntity));
				if (levelEntity2.hasBase)
				{
					levelEntity2 = levelEntity2.baseEntity as LevelEntity;
				}
				bool flag6 = !levelEntity2.behaviour || levelEntity2.behaviour.PhysicsEnabled;
				if (!flag6)
				{
					activePoll = false;
					pollTransform = false;
				}
				if (flag6 != flag)
				{
					Debug.LogWarning(string.Concat(base.transform.parent, "/", base.name, " has unmatching physicsEnabled and physicsActive"));
				}
				if (flag6 && !component4.gameObject.CompareTag("StayKinematic") && levelEntity2.hasBehaviour && !levelEntity2.behaviour.prefab.stayKinematic)
				{
					component4.isKinematic = false;
					if (!component4.CompareTag("DontInterpolate"))
					{
						component4.interpolation = RigidbodyInterpolation.Interpolate;
					}
					else
					{
						component4.interpolation = RigidbodyInterpolation.None;
					}
				}
			}
			if (flag && activePoll)
			{
				level.AddSimTrack(this);
				needsTracking = true;
			}
		}
		else
		{
			NetworkBlock.StripTransform(base.transform);
			if (hasBehaviour)
			{
				behaviour.noRigidbody = true;
			}
		}
		if (hasBehaviour && behaviour.hasBoundingBox)
		{
			behaviour.boundingBox.Toggle(false);
		}
	}

	public override void FetchComponents()
	{
		fireTag = GetComponentInChildren<FireTag>();
		iceTag = GetComponentInChildren<IceTag>();
		FireController[] componentsInChildren = GetComponentsInChildren<FireController>(true);
		fireController = ((componentsInChildren.Length <= 0) ? null : componentsInChildren[0]);
		hasFireController = fireController != null;
	}

	public void ResetLogic()
	{
		behaviour.ResetLogic();
	}

	public void OnSimulationStart()
	{
		if (behaviour.ActiveOnStart())
		{
			behaviour.TriggerActivate(true);
		}
	}

	public void SetController(EntityController controller)
	{
		entityController = controller;
	}

	public override void ResetEntity()
	{
		base.ResetEntity();
		entityController = null;
		if (scaleTracker != null)
		{
			scaleTracker.SetData(NetworkScene.ServerSettings.sendRate, base.transform.localScale);
		}
	}

	public void ResetTransform()
	{
		RigidbodyInterpolation interpolation = RigidbodyInterpolation.None;
		bool flag = base.transform.position != _lastReceivedPos;
		bool flag2 = base.transform.rotation != _lastReceivedRot;
		bool flag3 = base.transform.localScale != _lastReceivedScale;
		bool flag4 = flag || flag2 || flag3;
		if (isStatic && hasBehaviour && flag4 && !behaviour.noRigidbody)
		{
			interpolation = behaviour.Rigidbody.interpolation;
			behaviour.Rigidbody.interpolation = RigidbodyInterpolation.None;
		}
		if (flag)
		{
			base.transform.position = _lastReceivedPos;
		}
		if (flag2)
		{
			base.transform.rotation = _lastReceivedRot;
		}
		if (flag3)
		{
			base.transform.localScale = _lastReceivedScale;
		}
		if (needsTracking)
		{
			level.RemoveSimTrack(this);
			needsTracking = false;
		}
		posTracker.Override(_lastReceivedPos, _lastReceivedPos);
		rotTracker.Override(_lastReceivedRot, _lastReceivedRot);
		scaleTracker.Override(_lastReceivedScale, _lastReceivedScale);
		if (isStatic && hasBehaviour)
		{
			if (behaviour.useAccurateTransform)
			{
				behaviour.accuratePosition = _lastReceivedPos;
				behaviour.accurateRotation = _lastReceivedRot;
			}
			if (flag4 && !behaviour.noRigidbody)
			{
				behaviour.Rigidbody.interpolation = interpolation;
			}
		}
		SetLerping(false);
		RemoveLerpTarget();
		isModified = false;
		isLerpTarget = (changedPos = (changedRot = (changedScale = false)));
		if (flag4)
		{
			if (isBuildZone)
			{
				(behaviour as BuildZoneObject).UpdateZone();
			}
			if (isWaterZone)
			{
				(behaviour as WaterZoneEntity).UpdateOnTransformEvent();
			}
		}
	}

	public void SetEntityData(ushort playerId, byte[] data, int offset)
	{
		byte entityState = data[offset];
		bool flag = false;
		offset++;
		bool flag2 = playerId == BesiegeNetworkManager.Instance.PlayerID;
		bool flag3 = !IsSelected || !flag2;
		if (SendEntity.HasPosition(entityState))
		{
			NetworkCompression.UnpackVector(data, offset, out _lastReceivedPos);
			offset += 12;
			if (flag3)
			{
				posTracker.Override(_lastReceivedPos, _lastReceivedPos);
				flag = true;
			}
			if ((isStatic && isSimulating) || (!isStatic && simEntity != null && simEntity.isSimulating))
			{
				changedPos = true;
			}
		}
		if (SendEntity.HasRotation(entityState))
		{
			NetworkCompression.UnpackQuaternion(data, offset, out _lastReceivedRot);
			offset += 16;
			if (flag3)
			{
				rotTracker.Override(_lastReceivedRot, _lastReceivedRot);
				flag = true;
			}
			if ((isStatic && isSimulating) || (!isStatic && simEntity != null && simEntity.isSimulating))
			{
				changedRot = true;
			}
		}
		if (SendEntity.HasVector(entityState))
		{
			NetworkCompression.UnpackVector(data, offset, out _lastReceivedScale);
			offset += 12;
			if (flag3)
			{
				scaleTracker.Override(_lastReceivedScale, _lastReceivedScale);
				flag = true;
			}
			if ((isStatic && isSimulating) || (!isStatic && simEntity != null && simEntity.isSimulating))
			{
				changedScale = true;
			}
		}
		if (behaviour.IsModifying)
		{
			BlockMapper currentInstance = BlockMapper.CurrentInstance;
			if (!currentInstance.IsLogic)
			{
				currentInstance.IsDirty = true;
			}
		}
		if (flag)
		{
			if (!needsTracking)
			{
				level.AddTrack(this);
				needsTracking = true;
			}
			if (isBuildZone)
			{
				(behaviour as BuildZoneObject).UpdateZone();
			}
			if (isWaterZone)
			{
				(behaviour as WaterZoneEntity).UpdateOnTransformEvent();
			}
		}
	}

	protected override void ApplyState(byte state)
	{
		NetworkBlock.applyingState = true;
		base.ApplyState(state);
		if (ContainsState(state, BlockState.Killed))
		{
			SetEvent(0u, EntityEvent.Kill);
		}
		NetworkBlock.applyingState = false;
	}

	public override bool UpdateEntity(float delta)
	{
		if (isDestroyed)
		{
			return false;
		}
		if (StatMaster.isHosting && isLerping)
		{
			return true;
		}
		bool flag = false;
		if (isLerpTarget)
		{
			if (posTracker.isActive)
			{
				posTracker.Update(delta);
				Vector3 vector = lerpTransformTarget.position;
				Vector3 vector2 = new Vector3(posTracker.Vector.x + (vector.x - posTracker.Vector.x) * 0.5f, posTracker.Vector.y + (vector.y - posTracker.Vector.y) * 0.5f, posTracker.Vector.z + (vector.z - posTracker.Vector.z) * 0.5f);
				if (behaviour.noRigidbody)
				{
					base.transform.position = vector2;
				}
				else
				{
					behaviour.Rigidbody.MovePosition(vector2);
				}
				flag = true;
			}
			if (rotTracker.isActive)
			{
				rotTracker.Update(delta);
				Quaternion quaternion = lerpTransformTarget.rotation;
				if (behaviour.noRigidbody)
				{
					base.transform.rotation = Mathfx.SlerpQuaternion(posTracker.Rotation, quaternion, 0.5f);
				}
				else
				{
					behaviour.Rigidbody.MoveRotation(quaternion);
				}
				flag = true;
			}
			if (!flag && !isLerping)
			{
				isLerpTarget = false;
			}
		}
		else
		{
			if (posTracker.isActive)
			{
				posTracker.Update(delta);
				base.transform.position = posTracker.Vector;
				flag = true;
			}
			if (rotTracker.isActive)
			{
				rotTracker.Update(delta);
				base.transform.rotation = rotTracker.Rotation;
				flag = true;
			}
		}
		if (scaleTracker.isActive)
		{
			scaleTracker.Update(delta);
			base.transform.localScale = scaleTracker.Vector;
			flag = true;
		}
		return flag;
	}

	public void OnEnable()
	{
		if (StatMaster.isMP)
		{
			if (needsRigidUpdate)
			{
				StartCoroutine(UpdateRigidbodies(base.transform));
				needsRigidUpdate = false;
			}
			if (posTracker.isActive)
			{
				posTracker.SkipToEnd();
			}
			if (rotTracker.isActive)
			{
				rotTracker.SkipToEnd();
			}
			if (scaleTracker.isActive)
			{
				scaleTracker.SkipToEnd();
			}
		}
	}

	private static long GenerateID()
	{
		byte[] array = new byte[ID_LENGTH];
		randomGenerator.NextBytes(array);
		return BitConverter.ToInt64(array, 0);
	}

	private bool IsValidID(long newId)
	{
		LevelEntity entity;
		return newId != LevelPrefab.INVALID_ID && !levelEditor.Get(newId, out entity);
	}

	public void Place()
	{
		levelEditor = LevelEditor.Instance;
		long newId = GenerateID();
		while (!IsValidID(newId))
		{
			newId = GenerateID();
		}
		_identifier = newId;
	}

	public void SetPosition(Vector3 pos)
	{
		if (pos == posTracker.lastVec)
		{
			return;
		}
		if (!updatingRigidBody)
		{
			if (base.gameObject.activeInHierarchy)
			{
				StartCoroutine(UpdateRigidbodies(base.transform));
			}
			else
			{
				needsRigidUpdate = true;
			}
		}
		posTracker.Override(pos, pos);
		if (hasBehaviour)
		{
			behaviour.OnPositionChanged(pos);
		}
		if (entityController != null)
		{
			NetworkCompression.PackVector(pos, sendEntity.Position, 0);
			sendEntity.hasPosition = true;
			entityController.BufferEntity(identifier, sendEntity);
			if (StatMaster.isClient)
			{
				isModified = true;
			}
		}
	}

	public void SetRotation(Quaternion rot)
	{
		if (rot == rotTracker.lastRot)
		{
			return;
		}
		if (!updatingRigidBody)
		{
			if (base.gameObject.activeInHierarchy)
			{
				StartCoroutine(UpdateRigidbodies(base.transform));
			}
			else
			{
				needsRigidUpdate = true;
			}
		}
		rotTracker.Override(rot, rot);
		if (hasBehaviour)
		{
			behaviour.OnRotationChanged(rot);
		}
		if (entityController != null)
		{
			NetworkCompression.PackQuaternion(rot, sendEntity.Rotation, 0);
			sendEntity.hasRotation = true;
			entityController.BufferEntity(identifier, sendEntity);
			if (StatMaster.isClient)
			{
				isModified = true;
			}
			if (isBuildZone)
			{
				(behaviour as BuildZoneObject).UpdateZone();
			}
		}
	}

	public void SetScale(Vector3 scale)
	{
		if (scale == scaleTracker.lastVec)
		{
			return;
		}
		if (!updatingRigidBody)
		{
			if (base.gameObject.activeInHierarchy)
			{
				StartCoroutine(UpdateRigidbodies(base.transform));
			}
			else
			{
				needsRigidUpdate = true;
			}
		}
		scaleTracker.Override(scale, scale);
		if (hasBehaviour)
		{
			behaviour.OnScaleChanged(scale);
		}
		for (int i = 0; i < scaleCallbacks.Length; i++)
		{
			scaleCallbacks[i].ScaleChanged();
		}
		if (entityController != null)
		{
			NetworkCompression.PackVector(scale, sendEntity.Vector, 0);
			sendEntity.hasVector = true;
			entityController.BufferEntity(identifier, sendEntity);
			if (StatMaster.isClient)
			{
				isModified = true;
			}
		}
		if (isWaterZone)
		{
			(behaviour as WaterZoneEntity).UpdateOnTransformEvent();
		}
	}

	public IEnumerator UpdateRigidbodies(Transform source)
	{
		updatingRigidBody = true;
		yield return new WaitForEndOfFrame();
		Rigidbody[] rs = source.GetComponentsInChildren<Rigidbody>();
		foreach (Rigidbody current in rs)
		{
			Transform currentTransform = current.transform;
			Vector3 pos = currentTransform.position;
			Quaternion rot = currentTransform.rotation;
			RigidbodyInterpolation ri = current.interpolation;
			current.interpolation = RigidbodyInterpolation.None;
			currentTransform.position = pos;
			currentTransform.rotation = rot;
			current.interpolation = ri;
			current.WakeUp();
		}
		updatingRigidBody = false;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		if (!manualDeactivate && needsTracking && sendEntity.eventCount == 0)
		{
			if (isSimulating)
			{
				level.RemoveSimTrack(this);
			}
			else
			{
				level.RemoveTrack(this);
			}
			needsTracking = false;
		}
	}

	public void Select(bool selected)
	{
		if (isDestroyed || base.gameObject == null)
		{
			Debug.LogWarning("Trying to select object while gameObject is null! selected=" + selected + " isDestroyed=" + isDestroyed);
			return;
		}
		levelEditor.outlineEffect.ChangeTargetType(0);
		IsSelected = selected;
		if (levelEditor.IsTransformTool(levelEditor.CurrentState) && hasBehaviour && behaviour.childBodies.Length != 0)
		{
			if (wasInterpolating.Length != behaviour.childBodies.Length)
			{
				wasInterpolating = new int[behaviour.childBodies.Length];
			}
			if (selected)
			{
				for (int i = 0; i < behaviour.childBodies.Length; i++)
				{
					wasInterpolating[i] = (int)behaviour.childBodies[i].interpolation;
					behaviour.childBodies[i].interpolation = RigidbodyInterpolation.None;
				}
			}
			else
			{
				for (int j = 0; j < behaviour.childBodies.Length; j++)
				{
					behaviour.childBodies[j].interpolation = (RigidbodyInterpolation)wasInterpolating[j];
				}
			}
		}
		if (!selected)
		{
			IsSelectedExtra = false;
		}
		if (hasBehaviour)
		{
			if (levelEditor.useOutline && !behaviour.prefab.ignoreOutline && behaviour.visualController.outlines.Length != 0)
			{
				if (behaviour.hasBoundingBox)
				{
					behaviour.boundingBox.Toggle(false);
				}
				bool flag = false;
				for (int k = 0; k < behaviour.visualController.outlines.Length; k++)
				{
					if (behaviour.visualController.outlines[k] != null)
					{
						behaviour.visualController.outlines[k].enabled = selected;
						flag = true;
					}
				}
				if (flag && selected)
				{
					OutlineEffect.ToggleOutline(selected);
				}
			}
			else if (behaviour.hasBoundingBox)
			{
				behaviour.boundingBox.Toggle(selected);
			}
		}
		if (isModified && !selected)
		{
			ResetTransform();
		}
	}

	public override void SetEvent(uint frame, EntityEvent evt, byte data)
	{
		switch (evt)
		{
		case EntityEvent.AIKilled:
		{
			EntityAI component = GetComponent<EntityAI>();
			if (component == null)
			{
				Debug.LogWarning(string.Concat(base.gameObject, "is not an AI"));
				break;
			}
			component.isDead = true;
			component.BloodQuad();
			if (data != 2 && data != 4)
			{
				component.my.killingHandler.BloodTextureSwap();
			}
			if (data == 3)
			{
				component.my.killingHandler.activeType = InjuryType.Blunt;
				component.my.killingHandler.UseGibPrefab = true;
			}
			component.my.killingHandler.Kill();
			if (component.my.killingHandler.UseGibPrefab)
			{
				component.my.killingHandler.Gib();
			}
			component.my.killingHandler.my.SoundController.Play();
			break;
		}
		case EntityEvent.ChangeMesh:
		{
			SetPoseForAI component2 = GetComponent<SetPoseForAI>();
			if ((bool)component2)
			{
				component2.ChangeMesh((EntityAI.EntityState)data);
			}
			break;
		}
		case EntityEvent.AttackHitParticles:
		{
			EntityAI component = GetComponent<EntityAI>();
			if (component == null)
			{
				Debug.LogWarning(string.Concat(base.gameObject, "is not an AI"));
			}
			else
			{
				component.my.attackScript.PlayHitParticles((AITargetType)data);
			}
			break;
		}
		case EntityEvent.Fade:
		{
			EntityAI component = GetComponent<EntityAI>();
			if (component == null)
			{
				Debug.LogWarning(string.Concat(base.gameObject, "is not an AI"));
				break;
			}
			float num = (int)data;
			component.retreating.fading.Fade(num);
			if (num >= 1f && component.gameObject.activeInHierarchy)
			{
				component.StartCoroutine(component.DieFromCowardice());
			}
			break;
		}
		default:
			base.SetEvent(frame, evt, data);
			break;
		}
	}

	public override int PollObject(bool fullUpdate, byte[] data, int offset)
	{
		int num = offset;
		offset++;
		if (!pollTransform || isLerping || isDestroyed)
		{
			int eventCount = sendEntity.eventCount;
			if (eventCount > 0)
			{
				data[num] = (byte)(eventCount << 3);
				Buffer.BlockCopy(sendEntity.EventList, 0, data, offset, eventCount);
				sendEntity.eventCount = 0;
			}
			return offset - num + eventCount;
		}
		position = trackTransform.position;
		Vector3 vector = NetworkEntity.ClampPosition(position);
		rotation = trackTransform.rotation;
		Quaternion rot = rotation;
		int num2 = 0;
		bool flag = false;
		if (!posTracker.WithinThreshold(vector))
		{
			int eventCount = sendEntity.eventCount;
			if (eventCount > 0)
			{
				Buffer.BlockCopy(sendEntity.EventList, 0, data, offset, eventCount);
				sendEntity.eventCount = 0;
				offset += eventCount;
				num2 |= eventCount << 3;
			}
			flag = true;
			NetworkCompression.CompressPosition(vector, data, offset);
			offset += 6;
			num2 |= 1;
			posTracker.Store(vector);
			hasChangedPos = true;
		}
		if (!flag)
		{
			int eventCount = sendEntity.eventCount;
			if (eventCount > 0)
			{
				Buffer.BlockCopy(sendEntity.EventList, 0, data, offset, eventCount);
				sendEntity.eventCount = 0;
				offset += eventCount;
				num2 |= eventCount << 3;
			}
		}
		if (!rotTracker.WithinThreshold(rot))
		{
			NetworkCompression.CompressRotation(rot, data, offset);
			offset += 7;
			num2 |= 4;
			rotTracker.Store(rot);
			hasChangedRot = true;
		}
		data[num] = (byte)num2;
		if (turningOff)
		{
			pollTransform = false;
			turningOff = false;
		}
		if (!activePoll && needsTracking)
		{
			StopSimTrack();
		}
		return offset - num;
	}

	public void TriggerEvent(TriggerType type)
	{
		if (isTracking)
		{
			LevelEntity levelEntity = ((!hasBase) ? this : (baseEntity as LevelEntity));
			if (levelEntity.hasBehaviour)
			{
				levelEntity.behaviour.TriggerEvent(type);
			}
		}
	}

	public override void Event(EntityEvent evt, byte eventData)
	{
		if (isSimulating)
		{
			if (!needsTracking && isTracking)
			{
				StartSimTrack(false);
			}
			switch (evt)
			{
			case EntityEvent.AIKilled:
				TriggerEvent(TriggerType.Death);
				break;
			case EntityEvent.Ignite:
				TriggerEvent(TriggerType.Ignite);
				break;
			}
			base.Event(evt, eventData);
			if (StatMaster.isClient)
			{
				sendEntity.eventCount = 0;
			}
		}
	}

	public override void Event(EntityEvent evt)
	{
		if (!isSimulating)
		{
			return;
		}
		if (!needsTracking && isTracking)
		{
			StartSimTrack(false);
		}
		int num = -1;
		switch (evt)
		{
		case EntityEvent.Kill:
			num = 64;
			TriggerEvent(TriggerType.Death);
			break;
		case EntityEvent.Break:
		{
			if (hasBroken)
			{
				break;
			}
			num = 2;
			BreakOnForce component = GetComponent<BreakOnForce>();
			if (component != null)
			{
				BreakIntoChildren(component.BrokenInstance);
				break;
			}
			StructuralPhysTile component2 = GetComponent<StructuralPhysTile>();
			if (component2 != null)
			{
				BreakIntoChildren(component2.BrokenInstance);
				break;
			}
			ShipPartHitManager component3 = GetComponent<ShipPartHitManager>();
			if (component3 != null)
			{
				BreakIntoChildren(component3.BrokenInstance);
			}
			break;
		}
		case EntityEvent.Explode:
			TriggerEvent(TriggerType.Explode);
			break;
		}
		if (num != -1)
		{
			blockState |= (byte)num;
			hasChangedState = true;
		}
		base.Event(evt);
		if (StatMaster.isClient)
		{
			sendEntity.eventCount = 0;
		}
	}

	public override void SetEvent(uint frame, EntityEvent evt)
	{
		base.SetEvent(frame, evt);
		EntityAI component = GetComponent<EntityAI>();
		if (component != null)
		{
			switch (evt)
			{
			case EntityEvent.BloodBurstHit:
				component.my.killingHandler.BloodOnHit();
				break;
			case EntityEvent.BloodParticle:
				component.my.killingHandler.BloodParticle();
				break;
			case EntityEvent.AttackSwingParticles:
				component.my.attackScript.PlaySwingParticles();
				break;
			case EntityEvent.StopDizzyParticles:
				component.StopDizzyParticles();
				break;
			case EntityEvent.PlayDizzyParticles:
				component.PlayDizzyParticles();
				break;
			case EntityEvent.BobPlayPause:
				component.BobPlayPause();
				break;
			case EntityEvent.Kill:
				if (component.my.killingHandler != null)
				{
					if (NetworkBlock.applyingState)
					{
						component.my.killingHandler.activeType = InjuryType.Crushed;
					}
					component.my.killingHandler.KillMe(false);
				}
				else
				{
					component.DieNoJump();
				}
				break;
			case EntityEvent.DropPot:
			{
				DropPotOnRun component4 = GetComponent<DropPotOnRun>();
				if (component4 != null)
				{
					component4.Drop();
				}
				break;
			}
			case EntityEvent.Break:
			{
				if (hasBroken)
				{
					break;
				}
				BreakOnForce component2 = GetComponent<BreakOnForce>();
				if (component2 != null)
				{
					component2.Break();
					BreakIntoChildren(component2.BrokenInstance);
					break;
				}
				StructuralPhysTile component3 = GetComponent<StructuralPhysTile>();
				if (component3 != null)
				{
					component3.DestroyTile(Vector3.zero);
					BreakIntoChildren(component3.BrokenInstance);
					break;
				}
				Debug.LogWarning(id + ": Trying to break object " + Machine.GetObjectPath(base.gameObject) + ", but doesn't have BreakOnForce or StructuralPhysTile!");
				break;
			}
			}
			return;
		}
		switch (evt)
		{
		case EntityEvent.Break:
		{
			if (hasBroken)
			{
				break;
			}
			BreakOnForce component5 = GetComponent<BreakOnForce>();
			if (component5 != null)
			{
				if (NetworkBlock.applyingState && !component5.enabled)
				{
					component5.enabled = true;
				}
				component5.Break();
				BreakIntoChildren(component5.BrokenInstance);
				break;
			}
			StructuralPhysTile component6 = GetComponent<StructuralPhysTile>();
			if (component6 != null)
			{
				component6.DestroyTile(Vector3.zero);
				BreakIntoChildren(component6.BrokenInstance);
				break;
			}
			ShipPartHitManager component7 = GetComponent<ShipPartHitManager>();
			if (component7 != null)
			{
				component7.BreakFully();
				BreakIntoChildren(component7.BrokenInstance);
				break;
			}
			Debug.LogWarning(id + ": Trying to break object " + Machine.GetObjectPath(base.gameObject) + ", but doesn't have BreakOnForce or StructuralPhysTile!");
			break;
		}
		case EntityEvent.InsigniaFlash:
			if (behaviour is InsigniaTrigger)
			{
				(behaviour as InsigniaTrigger).Flash();
				break;
			}
			Debug.LogWarning("Trying to flash insignia, but " + Machine.GetObjectPath(base.gameObject) + " " + ((!hasBehaviour) ? string.Empty : ("(" + behaviour.prefab.ID + ")")) + " doesn't have an insignia!");
			break;
		case EntityEvent.Kill:
		{
			SquashOnTriggerEnter componentInChildren2 = GetComponentInChildren<SquashOnTriggerEnter>();
			if (componentInChildren2 != null)
			{
				componentInChildren2.Gib();
			}
			break;
		}
		case EntityEvent.ParticleOnCollide:
		{
			ParticleOnCollide componentInChildren4 = GetComponentInChildren<ParticleOnCollide>();
			if (componentInChildren4 != null)
			{
				componentInChildren4.PlayParticles();
			}
			break;
		}
		case EntityEvent.ParticleOnTrigger:
		{
			ParticleOnTrigger componentInChildren3 = GetComponentInChildren<ParticleOnTrigger>();
			if (componentInChildren3 != null)
			{
				componentInChildren3.PlayParticles();
			}
			break;
		}
		case EntityEvent.CannonParticles:
		{
			IParticlePlay componentInChildren = GetComponentInChildren<IParticlePlay>();
			if (componentInChildren != null)
			{
				componentInChildren.PlayParticles();
			}
			break;
		}
		}
	}

	public void Init(int length)
	{
		children = new NetworkBlock[length];
	}
}
