using System;
using System.Collections;
using System.Collections.Generic;
using InternalModding.Triggers;
using Localisation;
using Modding;
using UnityEngine;

[AddComponentMenu("LevelEditor/GenericEntity")]
public class GenericEntity : SaveableDataHolder
{
	[NonSerialized]
	[HideInInspector]
	public List<EntityLogic> logicData = new List<EntityLogic>();

	[NonSerialized]
	[HideInInspector]
	public List<EntityLogic> staticLogicData = new List<EntityLogic>();

	[NonSerialized]
	public bool hasLogic;

	[NonSerialized]
	public bool hasRunningLogic;

	public static string LOGIC_PREFIX = "lel-";

	public MText logicName;

	public MToggle activeOnStart;

	[NonSerialized]
	public bool isInitialized;

	public BreakOnForce[] breakForce;

	public StructuralPhysTile[] physTile;

	public Rigidbody[] childBodies;

	public LevelEntity entity;

	public LevelPrefab prefab;

	public EntityVisualController visualController;

	public LevelBoundingBox boundingBox;

	public bool hasBoundingBox;

	public List<EntityLogic> runningLogic = new List<EntityLogic>();

	public Dictionary<string, float> variables = new Dictionary<string, float>();

	public bool updateOnTransformEvent;

	public bool physicsDefaulted = true;

	public bool startingSim;

	public EventContainer.TransformEvent currentTransformEvent;

	public Vector3 accuratePosition;

	public Quaternion accurateRotation;

	public bool useAccurateTransform;

	protected MSlider breakForceAmount;

	protected MSlider destroyThreshold;

	protected MToggle physicsToggle;

	protected float[] childMass;

	protected MSlider massSlider;

	protected MSlider densitySlider;

	protected float minMassScale = 0.1f;

	protected float maxMassScale = 10f;

	protected float minDensity = 0.5f;

	protected float maxDensity = 20f;

	protected CustomLevel level;

	protected bool needsProjectileUpdate;

	protected bool hasPhysics;

	protected LevelEditor levelEditor;

	private bool hasIgnited;

	private bool hasExploded;

	private bool hasBroken;

	private bool hasDied;

	private bool materialLocked;

	protected static float MinDensity;

	protected static float MaxDensity = 100f;

	public virtual bool PhysicsEnabled
	{
		get
		{
			return hasPhysics && !entity.isStatic;
		}
	}

	public virtual bool IsMultiLook
	{
		get
		{
			return false;
		}
	}

	public bool IsGlobalVarTarget
	{
		get
		{
			List<EntityLogic> logic = GetLogic();
			for (int i = 0; i < logic.Count; i++)
			{
				EntityLogic entityLogic = logic[i];
				if (entityLogic.triggerType == TriggerType.Variable && entityLogic.varGlobal)
				{
					return true;
				}
			}
			return false;
		}
	}

	public event AddEntityHandler OnAdded;

	public event RemoveEntityHandler OnRemoved;

	public event ChangeEntityHandler OnChanged;

	public virtual bool DisplayNameWidget()
	{
		return true;
	}

	public virtual bool TriggerEvaluate()
	{
		return true;
	}

	public void LockMaterial(bool toggle)
	{
		materialLocked = toggle;
	}

	public void UpdateMaterial(Material mat)
	{
		if (!materialLocked)
		{
			visualController.ApplyMaterial(mat);
		}
	}

	public void RestoreMaterial()
	{
		if (!materialLocked)
		{
			visualController.Restore();
		}
	}

	public LevelBoundingBox.GroundResult Ground()
	{
		if (prefab.hasBoundingBox)
		{
			return boundingBox.Ground();
		}
		return LevelBoundingBox.Ground(Rigidbody);
	}

	public void RemoveBoundingBox()
	{
		if (hasBoundingBox)
		{
			UnityEngine.Object.Destroy(boundingBox.gameObject);
		}
		hasBoundingBox = false;
	}

	public bool GetLogic(int id, out EntityLogic logic, bool isStatic)
	{
		List<EntityLogic> list = ((!isStatic) ? logicData : staticLogicData);
		for (int i = 0; i < list.Count; i++)
		{
			EntityLogic entityLogic = list[i];
			if (entityLogic.ID == id)
			{
				logic = entityLogic;
				return true;
			}
		}
		logic = null;
		return false;
	}

	public bool GetVariableValue(string key, out float val)
	{
		return variables.TryGetValue(key, out val);
	}

	public void ReplaceEntityReference(long oldReference, long newReference)
	{
		foreach (EntityLogic logicDatum in logicData)
		{
			logicDatum.ReplaceEntityReference(oldReference, newReference);
		}
	}

	protected virtual bool IsCompatibleTrigger(TriggerType trigger)
	{
		for (int i = 0; i < prefab.events.Length; i++)
		{
			if (trigger == prefab.events[i])
			{
				return true;
			}
		}
		return false;
	}

	public byte[] GetIdentifierBytes()
	{
		return BitConverter.GetBytes(entity.identifier);
	}

	public void RemoveIncompatibleTriggers()
	{
		foreach (EntityLogic item in new List<EntityLogic>(logicData))
		{
			if (!IsCompatibleTrigger(item.triggerType))
			{
				logicData.Remove(item);
			}
		}
	}

	public virtual void OnAdd()
	{
		if (this.OnAdded != null)
		{
			this.OnAdded();
		}
	}

	public virtual string GetStartString()
	{
		return LocalisationManager.GetTranslation(3251);
	}

	public virtual string GetEndString()
	{
		return LocalisationManager.GetTranslation(3252);
	}

	public virtual void OnRemove()
	{
		if (this.OnRemoved != null)
		{
			this.OnRemoved();
		}
	}

	public virtual void OnChange()
	{
		if (this.OnChanged != null)
		{
			this.OnChanged();
		}
	}

	public override void OnMapperOpen()
	{
		base.OnMapperOpen();
		entity.Select(true);
		RestoreMaterial();
		LockMaterial(true);
		LevelEditor.Instance.UpdatePlayerSelection(entity);
	}

	public override void OnMapperClose()
	{
		base.OnMapperClose();
		LockMaterial(false);
		entity.Select(false);
		LevelEditor.Instance.UpdatePlayerSelection(null);
	}

	public TriggerType DefaultTriggerType()
	{
		TriggerType[] events = prefab.events;
		if (events.Length == 0)
		{
			Debug.LogError(base.name + " has no events setup in LevelPrefab! Fix!");
			return TriggerType.LevelStart;
		}
		return prefab.events[0];
	}

	public TriggerType NextTriggerType(TriggerType triggerType)
	{
		TriggerType[] events = prefab.events;
		int num = 0;
		for (int i = 0; i < events.Length; i++)
		{
			if (events[i] == triggerType)
			{
				num = i;
				break;
			}
		}
		return (num >= events.Length - 1) ? events[0] : events[num + 1];
	}

	public TriggerType PreviousTriggerType(TriggerType triggerType)
	{
		TriggerType[] events = prefab.events;
		int num = 0;
		for (int i = 0; i < events.Length; i++)
		{
			if (events[i] == triggerType)
			{
				num = i;
				break;
			}
		}
		return (num <= 0) ? events[events.Length - 1] : events[num - 1];
	}

	public int NextModdedTriggerType(int triggerType)
	{
		int[] moddedEvents = prefab.moddedEvents;
		int num = 0;
		for (int i = 0; i < moddedEvents.Length; i++)
		{
			if (moddedEvents[i] == triggerType)
			{
				num = i;
				break;
			}
		}
		return (num >= moddedEvents.Length - 1) ? moddedEvents[0] : moddedEvents[num + 1];
	}

	public int PreviousModdedTriggerType(int triggerType)
	{
		int[] moddedEvents = prefab.moddedEvents;
		int num = 0;
		for (int i = 0; i < moddedEvents.Length; i++)
		{
			if (moddedEvents[i] == triggerType)
			{
				num = i;
				break;
			}
		}
		return (num <= 0) ? moddedEvents[moddedEvents.Length - 1] : moddedEvents[num - 1];
	}

	public int TriggerTypeCount()
	{
		TriggerType[] events = prefab.events;
		if (events.Length == 0)
		{
			Debug.LogError(base.name + " has no events setup in LevelPrefab! Fix!");
			return 1;
		}
		return events.Length;
	}

	public virtual void TriggerEvent(TriggerType triggerType)
	{
		switch (triggerType)
		{
		case TriggerType.Destroy:
			if (!hasBroken)
			{
				ProcessEvent(triggerType);
				hasBroken = true;
			}
			break;
		case TriggerType.Explode:
			if (!hasExploded)
			{
				ProcessEvent(triggerType);
				hasExploded = true;
			}
			break;
		case TriggerType.Ignite:
			if (!hasIgnited)
			{
				ProcessEvent(triggerType);
				hasIgnited = true;
			}
			break;
		case TriggerType.Death:
			if (!hasDied)
			{
				ProcessEvent(triggerType);
				hasDied = true;
			}
			break;
		default:
			ProcessEvent(triggerType);
			break;
		}
	}

	public void ProcessEvent(TriggerType triggerType)
	{
		List<EntityLogic> logic = GetLogic();
		for (int i = 0; i < logic.Count; i++)
		{
			EntityLogic entityLogic = logic[i];
			if (entityLogic.triggerType == triggerType)
			{
				ExecuteLogic(entityLogic);
			}
		}
	}

	public void ProcessModdedEvent(ModdedTrigger triggerType)
	{
		List<EntityLogic> logic = GetLogic();
		for (int i = 0; i < logic.Count; i++)
		{
			EntityLogic entityLogic = logic[i];
			if (entityLogic.moddedTriggerType == triggerType)
			{
				ExecuteLogic(entityLogic);
			}
		}
	}

	public virtual void TriggerActivate(bool isSimStart)
	{
		List<EntityLogic> logic = GetLogic();
		needsProjectileUpdate = false;
		for (int i = 0; i < logic.Count; i++)
		{
			EntityLogic entityLogic = logic[i];
			if (entityLogic.triggerType == TriggerType.LevelStart)
			{
				if (isSimStart)
				{
					ExecuteLogic(entityLogic);
				}
			}
			else if (entityLogic.triggerType == TriggerType.Activate)
			{
				if (!isSimStart || entityLogic.simStartTrigger)
				{
					ExecuteLogic(entityLogic);
				}
			}
			else
			{
				if (!entityLogic.IsTrigger())
				{
					continue;
				}
				for (int j = 0; j < entityLogic.targets.Count; j++)
				{
					TriggerTarget triggerTarget = entityLogic.targets[j];
					if (triggerTarget.targetType == TriggerTargetType.Anything || triggerTarget.targetType == TriggerTargetType.AnyProjectile)
					{
						needsProjectileUpdate = true;
						break;
					}
				}
			}
		}
	}

	public virtual void UpdateLogic(float delta, bool useFixedUpdate)
	{
		List<EntityLogic> list = new List<EntityLogic>(runningLogic);
		for (int i = 0; i < list.Count; i++)
		{
			EntityLogic entityLogic = list[i];
			if (entityLogic.isRunning)
			{
				entityLogic.UpdateLogic(delta, useFixedUpdate);
				if (entity.isDestroyed)
				{
					break;
				}
				if (entityLogic.isDone)
				{
					StopLogic(entityLogic);
				}
			}
		}
	}

	protected virtual void ExecuteLogic(EntityLogic logic)
	{
		SingleInstance<Events>.Instance.ChainTriggered(logic);
		if (StatMaster.isClient && !StatMaster.isLocalSim)
		{
			return;
		}
		if (!logic.IsValid())
		{
			GenericUIPopup instance = SingleInstanceFindOnly<GenericUIPopup>.Instance;
			if (instance != null)
			{
				instance.Show(string.Format("{0}\n'{1}' {2} {3}", logic.LogicError, LogicName(), LocalisationManager.GetTranslation(2030), entity.identifier), 5f);
			}
			return;
		}
		if (logic.events.Exists((EntityEvent x) => x.eventType == EventContainer.EventType.Repeat))
		{
			logic.repeatEvent = true;
		}
		if (logic.isRunning)
		{
			StopLogic(logic);
		}
		logic.isRunning = true;
		logic.Execute();
		if (logic.isDone)
		{
			logic.isRunning = false;
		}
		else
		{
			AddRunningLogic(logic);
		}
	}

	public void AddRunningLogic(EntityLogic logic)
	{
		if (!runningLogic.Contains(logic))
		{
			runningLogic.Add(logic);
		}
		if (!hasRunningLogic)
		{
			level.RegisterUpdatingLogicRunner(this);
			hasRunningLogic = true;
		}
	}

	public virtual void ActivateEntity()
	{
		base.gameObject.SetActive(true);
	}

	public virtual void DeactivateEntity()
	{
		base.gameObject.SetActive(false);
	}

	public void OnEventFinish(EntityLogic logic, EntityEvent evt)
	{
		if (!StatMaster.isClient && StatMaster.levelSimulating && !StatMaster.isLocalSim && evt.eventData.IsProgressEvent())
		{
			int iD_LENGTH = LevelEntity.ID_LENGTH;
			int num = NetworkCompression.PackedUIntLength(logic.ID, false);
			int num2 = NetworkCompression.PackedUIntLength(evt.ID, false);
			int num3 = 0;
			byte[] array = new byte[1 + iD_LENGTH + num + num2 + 2];
			array[0] = 1;
			num3++;
			Buffer.BlockCopy(GetIdentifierBytes(), 0, array, num3, iD_LENGTH);
			num3 += iD_LENGTH;
			NetworkCompression.PackUInt(logic.ID, array, num3, false, num);
			num3 += num;
			NetworkCompression.PackUInt(evt.ID, array, num3, false, num2);
			num3 += num2;
			NetworkCompression.WriteUInt16((ushort)Mathf.RoundToInt(evt.eventData.GetProgress() * 65535f), array, num3);
			levelEditor.StopProgressEvent(array);
		}
	}

	public void StopLogic(EntityLogic logic)
	{
		if (runningLogic.Contains(logic))
		{
			int currentIndex = logic.currentIndex;
			if (currentIndex < logic.events.Count)
			{
				EntityEvent entityEvent = logic.events[currentIndex];
				OnEventFinish(logic, entityEvent);
				entityEvent.eventData.Stop();
			}
			runningLogic.Remove(logic);
			logic.isRunning = false;
			hasRunningLogic = runningLogic.Count > 0;
			if (!hasRunningLogic)
			{
				level.UnregisterUpdatingLogicRunner(this);
			}
		}
	}

	public void StopLogic()
	{
		while (runningLogic.Count > 0)
		{
			StopLogic(runningLogic[0]);
		}
	}

	public virtual void Reset()
	{
		hasIgnited = false;
		hasDied = false;
		hasExploded = false;
	}

	public virtual void Init()
	{
		if (!StatMaster.isMP || isInitialized)
		{
			return;
		}
		levelEditor = LevelEditor.Instance;
		level = CustomLevel.Instance;
		NetBlock = entity;
		isInitialized = true;
		if (entity.hasBase)
		{
			return;
		}
		infoType = BasicInfoType.Entity;
		logicName = AddText(2424, LOGIC_PREFIX + "name", prefab.name);
		activeOnStart = AddToggle(2423, LOGIC_PREFIX + "active-on-start", true);
		logicName.DisplayInMapper = false;
		activeOnStart.DisplayInMapper = false;
		if (prefab.ignorePhysics)
		{
			return;
		}
		hasPhysics = false;
		bool showPhysicsToggle = prefab.showPhysicsToggle;
		if (showPhysicsToggle)
		{
			if (breakForce.Length > 0)
			{
				BreakOnForce breakOnForce = breakForce[0];
				breakForceAmount = AddSlider(LocalisationManager.GetTranslation(2421), LOGIC_PREFIX + "break-force", breakOnForce.ForceToBreak, OptionsMaster.settingsMinBreakForce, OptionsMaster.settingsMaxBreakForce, string.Empty, string.Empty);
				breakForceAmount.logScaling = true;
				breakForceAmount.ValueChanged += OnBreakForceChanged;
				OnBreakForceChanged(breakOnForce.ForceToBreak);
			}
			if (physTile.Length > 0)
			{
				StructuralPhysTile structuralPhysTile = physTile[0];
				destroyThreshold = AddSlider(LocalisationManager.GetTranslation(2422), LOGIC_PREFIX + "destroy-threshold", structuralPhysTile.destroyThreshold, OptionsMaster.settingsMinDestroyThreshold, OptionsMaster.settingsMaxDestroyThreshold, string.Empty, string.Empty);
				destroyThreshold.logScaling = true;
				destroyThreshold.ValueChanged += OnDestroyThresholdChanged;
				OnDestroyThresholdChanged(structuralPhysTile.destroyThreshold);
			}
		}
		if (childBodies.Length <= 0)
		{
			return;
		}
		if (showPhysicsToggle)
		{
			childMass = new float[childBodies.Length];
			for (int i = 0; i < childBodies.Length; i++)
			{
				Rigidbody rigidbody = childBodies[i];
				childMass[i] = rigidbody.mass;
			}
			if (physTile.Length == 0)
			{
				if ((entity.children.Length < 1 && !IgnoredByWater) || (IsMultiLook && entity.children.Length > 1))
				{
					densitySlider = AddSliderUnclamped(4594, "density", density, minDensity, maxDensity, string.Empty, string.Empty, true);
					densitySlider.ValueChanged += OnDensityChanged;
					densitySlider.logScaling = true;
					OnDensityChanged(density);
					densitySlider.DisplayInMapper = true;
				}
				if (!prefab.stayKinematic || IsMultiLook)
				{
					float num = 1f;
					massSlider = AddSliderUnclamped(2420, LOGIC_PREFIX + "mass", num, minMassScale, maxMassScale, string.Empty);
					massSlider.ValueChanged += OnMassChanged;
					OnMassChanged(num);
					massSlider.DisplayInMapper = true;
				}
			}
			physicsToggle = AddToggle(2419, LOGIC_PREFIX + "enable-physics", physicsDefaulted);
			physicsToggle.Toggled += OnPhysicsToggled;
			OnPhysicsToggled(physicsDefaulted);
			physicsToggle.DisplayInMapper = true;
		}
		hasPhysics = true;
	}

	public static float SetVariable(Dictionary<string, float> varList, string key, EventContainer.VarModifyType modifyMode, float val)
	{
		float value;
		bool flag = varList.TryGetValue(key, out value);
		if (!flag)
		{
			value = 0f;
		}
		switch (modifyMode)
		{
		case EventContainer.VarModifyType.Add:
			value += val;
			break;
		case EventContainer.VarModifyType.Subtract:
			value -= val;
			break;
		case EventContainer.VarModifyType.Set:
			value = val;
			break;
		}
		if (flag)
		{
			varList[key] = value;
		}
		else
		{
			varList.Add(key, value);
		}
		return value;
	}

	public void SetVariable(string key, EventContainer.VarModifyType modifyMode, float val)
	{
		float newVal = SetVariable(variables, key, modifyMode, val);
		OnSetVariable(key, false, newVal);
	}

	public void OnSetVariable(string key, bool isGlobal, float newVal)
	{
		if (!entity.hasSpawned)
		{
			return;
		}
		List<EntityLogic> logic = GetLogic();
		for (int i = 0; i < logic.Count; i++)
		{
			EntityLogic entityLogic = logic[i];
			if (entityLogic.triggerType == TriggerType.Variable && entityLogic.varGlobal == isGlobal && entityLogic.varKey.Equals(key) && entityLogic.CompareVariable(newVal))
			{
				ExecuteLogic(entityLogic);
			}
		}
	}

	protected virtual void OnPhysicsToggled(bool toggle)
	{
		if (startingSim)
		{
			return;
		}
		if (massSlider != null)
		{
			massSlider.DisplayInMapper = toggle;
		}
		if (densitySlider != null)
		{
			densitySlider.DisplayInMapper = toggle;
		}
		if (breakForceAmount != null)
		{
			breakForceAmount.DisplayInMapper = toggle;
		}
		if (destroyThreshold != null)
		{
			destroyThreshold.DisplayInMapper = toggle;
		}
		bool flag = !toggle;
		if (entity.isStatic == flag)
		{
			return;
		}
		base.transform.SetParent((!flag) ? level.PhysGoal : level.StaticGoal, true);
		entity.isStatic = flag;
		for (int i = 0; i < breakForce.Length; i++)
		{
			breakForce[i].enabled = !entity.isStatic;
		}
		if (entity.isStatic)
		{
			entity.hasSpawned = true;
		}
		if (StatMaster.levelSimulating)
		{
			if (flag)
			{
				level.AddStaticEntity(entity);
				entity.StartDeactivated();
			}
			else
			{
				level.RemoveStaticEntity(entity);
				entity.ActivateEntity(0u);
			}
		}
	}

	protected void OnDestroyThresholdChanged(float newForce)
	{
		for (int i = 0; i < physTile.Length; i++)
		{
			StructuralPhysTile structuralPhysTile = physTile[i];
			structuralPhysTile.destroyThreshold = newForce;
			float num = 6000f + 6000f * newForce / 400f;
			for (int j = 0; j < structuralPhysTile.joints.Length; j++)
			{
				structuralPhysTile.joints[j].breakTorque = (int)num;
			}
		}
	}

	protected void OnBreakForceChanged(float newForce)
	{
		for (int i = 0; i < breakForce.Length; i++)
		{
			BreakOnForce breakOnForce = breakForce[i];
			breakOnForce.ForceToBreak = newForce;
		}
	}

	protected void OnMassChanged(float newMass)
	{
		if (!StatMaster.levelSimulating)
		{
			newMass = ((!(newMass >= 0f)) ? 0f : newMass);
			for (int i = 0; i < childBodies.Length; i++)
			{
				Rigidbody rigidbody = childBodies[i];
				rigidbody.mass = childMass[i] * newMass;
			}
			if (densitySlider != null && densitySlider.Value == 0f)
			{
				CalculateDensity(true);
			}
		}
	}

	protected virtual void OnDensityChanged(float newDensity)
	{
		if (newDensity > 0f)
		{
			newDensity = Mathf.Clamp(newDensity, MinDensity, MaxDensity);
			density = Mathf.Max(0.01f, newDensity);
			if (!IsMultiLook)
			{
				return;
			}
			for (int i = 0; i < entity.children.Length; i++)
			{
				LevelEntity levelEntity = entity.children[i] as LevelEntity;
				if (levelEntity.HasInfo)
				{
					levelEntity.bInfo.density = density;
				}
			}
		}
		else
		{
			CalculateDensity(true);
		}
	}

	public List<EntityLogic> GetLogic()
	{
		return (!entity.isSimulating || !entity.isStatic) ? logicData : staticLogicData;
	}

	public void ClearStaticLogic()
	{
		staticLogicData.Clear();
	}

	public void OnLoadLogic(XDataHolder data, bool isStatic)
	{
		List<EntityLogic> list = ((!isStatic) ? logicData : staticLogicData);
		list.Clear();
		int num = 0;
		string prefix = GetPrefix(num);
		while (data.HasKey(prefix + "type"))
		{
			string type = data.ReadString(prefix + "type");
			EntityLogic entityLogic = new EntityLogic(type, this);
			entityLogic.OnLoadLogic(data, prefix, true);
			if (!isStatic && !startingSim)
			{
				SingleInstanceFindOnly<TriggerLoader>.Instance.LogicChanged(entityLogic);
			}
			list.Add(entityLogic);
			prefix = GetPrefix(++num);
		}
		hasLogic = list.Count > 0;
	}

	public void ApplyLogicState(List<EntityLogicState.LogicProgress> runningLogic, float timeCorrection)
	{
		EntityLogic logic = null;
		EntityEvent evt = null;
		LevelEditor instance = LevelEditor.Instance;
		for (int i = 0; i < runningLogic.Count; i++)
		{
			EntityLogicState.LogicProgress logicProgress = runningLogic[i];
			if (!GetLogic(logicProgress.logicID, out logic, entity.isStatic) || !logic.GetEvent(logicProgress.eventID, out evt))
			{
				continue;
			}
			float progress = logicProgress.progress;
			List<LevelEntity> list = null;
			if (EventContainer.IsPickEvent(evt.eventType))
			{
				list = new List<LevelEntity>();
				if (logic.UseSelf(evt))
				{
					list.Add(logic.entityBehaviour.entity);
				}
				else
				{
					for (int j = 0; j < evt.entityList.Count; j++)
					{
						long num = evt.entityList[j];
						LevelEntity simEntity;
						if (num == LevelPrefab.INVALID_ID || !instance.Get(num, out simEntity))
						{
							continue;
						}
						if (!simEntity.isStatic)
						{
							if (simEntity.simEntity == null)
							{
								Debug.Log(string.Concat("Couldn't fire ", evt.eventType, " for ", prefab.name, " (", prefab.ID, "), doesn't have a sim block!"));
							}
							else
							{
								simEntity = simEntity.simEntity;
							}
						}
						list.Add(simEntity);
					}
				}
				if (evt.eventType == EventContainer.EventType.Transform)
				{
					EventContainer.TransformEvent transformEvent = evt.eventData as EventContainer.TransformEvent;
					transformEvent.Init(list.Count);
					if (logicProgress.hasEventData)
					{
						evt.eventData.DecodeEventData(logicProgress.eventData, 0);
					}
					for (i = 0; i < list.Count; i++)
					{
						transformEvent.entityList[i].Setup(list[i], transformEvent, false);
					}
				}
			}
			level.StartProgressEvent(logic, evt, progress, timeCorrection);
		}
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		LoadMapperValues(data);
		OnLoadLogic(data, false);
	}

	public override void OnLoad(XDataHolder data, CopyMode mode)
	{
		base.OnLoad(data, mode);
		switch (mode)
		{
		case CopyMode.Logic:
			OnLoadLogic(data, false);
			break;
		case CopyMode.Parameters:
		case CopyMode.Settings:
			LoadMapperValues(data);
			break;
		default:
			OnLoad(data);
			break;
		}
	}

	public void OnSaveLogicLoadValue(XDataHolder data)
	{
		for (int i = 0; i < logicData.Count; i++)
		{
			EntityLogic entityLogic = logicData[i];
			string prefix = GetPrefix(i);
			entityLogic.OnSaveLogicLoad(data, prefix);
		}
	}

	public void OnSaveLogic(XDataHolder data)
	{
		for (int i = 0; i < logicData.Count; i++)
		{
			EntityLogic entityLogic = logicData[i];
			string prefix = GetPrefix(i);
			entityLogic.OnSaveLogic(data, prefix, true);
		}
	}

	public void ResetLogic()
	{
		if (variables.Count > 0)
		{
			variables.Clear();
		}
		hasLogic = logicData.Count > 0;
		List<EntityLogic> logic = GetLogic();
		for (int i = 0; i < logic.Count; i++)
		{
			logic[i].Reset();
		}
	}

	public override void OnSave(XDataHolder data, CopyMode mode)
	{
		base.OnSave(data, mode);
		switch (mode)
		{
		case CopyMode.Logic:
			OnSaveLogic(data);
			break;
		case CopyMode.Settings:
			SaveMapperValues(data);
			break;
		case CopyMode.Parameters:
		{
			SaveMapperValues(data);
			string key = "bmt-" + logicName.Key;
			string key2 = "bmt-" + activeOnStart.Key;
			data.Remove(key);
			data.Remove(key2);
			break;
		}
		default:
			OnSave(data);
			break;
		}
	}

	public override void OnSave(XDataHolder data)
	{
		base.OnSave(data);
		SaveMapperValues(data);
		OnSaveLogic(data);
	}

	public virtual string LogicName()
	{
		if (logicName != null)
		{
			return logicName.Value;
		}
		return prefab.name;
	}

	public virtual bool ActiveOnStart()
	{
		return activeOnStart.IsActive;
	}

	public virtual void SetupDefault()
	{
		SetupDefaultName();
	}

	private void SetupDefaultName()
	{
		if (entity == null)
		{
			Debug.LogError("Couldn't set default name for " + base.name + "!");
			return;
		}
		if (logicName == null)
		{
			Debug.LogError("Logic name is null!");
			return;
		}
		string translation = LocalisationManager.GetTranslation(prefab.LocalisationID);
		logicName.Value = translation;
		logicName.SetDefaultText(translation);
		logicName.ApplyValue();
	}

	private string GetPrefix(int i)
	{
		return LOGIC_PREFIX + "logic" + i + "-";
	}

	public virtual void UpdateOnTransformEvent()
	{
	}

	public void WakeUpRigidbody(int frames)
	{
		StartCoroutine(IEWakeUpRigidbody(frames));
	}

	public IEnumerator IEWakeUpRigidbody(int frames)
	{
		for (int i = 0; i < frames; i++)
		{
			yield return new WaitForFixedUpdate();
		}
		if (!noRigidbody)
		{
			Rigidbody.WakeUp();
		}
	}

	public IEnumerator AfterTransformEvent()
	{
		yield return new WaitForEndOfFrame();
		useAccurateTransform = false;
	}

	public virtual void OnPositionChanged(Vector3 pos)
	{
	}

	public virtual void OnRotationChanged(Quaternion rot)
	{
	}

	public virtual void OnScaleChanged(Vector3 scale)
	{
		gotBounds = false;
		CalculateBounds();
	}
}
