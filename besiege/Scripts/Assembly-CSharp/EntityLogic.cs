using System;
using System.Collections.Generic;
using InternalModding.Events;
using InternalModding.Loading;
using InternalModding.Triggers;
using Localisation;
using UnityEngine;

public class EntityLogic
{
	public enum VarCompareType
	{
		Higher = 0,
		EqualsOrHigher = 1,
		Equals = 2,
		EqualsOrLess = 3,
		Less = 4
	}

	private static Dictionary<string, TriggerType> triggerTypeLookup = new Dictionary<string, TriggerType>
	{
		{
			"Active",
			TriggerType.Activate
		},
		{
			"SimulationStart",
			TriggerType.Activate
		},
		{
			"StartSimulation",
			TriggerType.Activate
		},
		{
			"Activate",
			TriggerType.Activate
		},
		{
			"OnActivate",
			TriggerType.Activate
		},
		{
			"Start",
			TriggerType.Start
		},
		{
			"End",
			TriggerType.End
		},
		{
			"EndSimulation",
			TriggerType.End
		},
		{
			"Enter",
			TriggerType.Enter
		},
		{
			"LevelStart",
			TriggerType.LevelStart
		},
		{
			"EnterInsignia",
			TriggerType.Enter
		},
		{
			"Exit",
			TriggerType.Exit
		},
		{
			"ExitInsignia",
			TriggerType.Exit
		},
		{
			"Destroy",
			TriggerType.Destroy
		},
		{
			"Death",
			TriggerType.Death
		},
		{
			"Ignite",
			TriggerType.Ignite
		},
		{
			"Explode",
			TriggerType.Explode
		},
		{
			"Behaviour",
			TriggerType.Behaviour
		},
		{
			"Deactivate",
			TriggerType.Deactivate
		},
		{
			"OnDeactivate",
			TriggerType.Deactivate
		},
		{
			"Variable",
			TriggerType.Variable
		},
		{
			"MachineDamage",
			TriggerType.MachineDamage
		},
		{
			"KeyPressed",
			TriggerType.KeyPressed
		},
		{
			"KeyReleased",
			TriggerType.KeyReleased
		},
		{
			"Modded",
			TriggerType.Modded
		}
	};

	public GenericEntity entityBehaviour;

	public ushort ID;

	public static Action<EntityLogic> TriggerLoaderCallback;

	[NonSerialized]
	public int currentIndex;

	public TriggerType triggerType;

	public ModdedTrigger moddedTriggerType;

	public bool simStartTrigger;

	public bool allTargets;

	public float damageIncrement;

	public bool useHPRangeToggle;

	public string varKey;

	public VarCompareType varCompare;

	public float varThreshold;

	public KeyCode keyPressCode;

	public bool varGlobal;

	public TriggerType loadTriggerType;

	public ModdedTrigger loadModdedTriggerType;

	public bool loadSimStart;

	public bool loadAllTargets;

	public float loadDamageIncrement;

	public bool loadUseHPRangeToggle;

	public string loadVarKey;

	public VarCompareType loadVarCompare;

	public float loadVarThreshold;

	public KeyCode loadKeyPressCode;

	public bool loadVarGlobal;

	public List<TriggerTarget> targets;

	public List<EntityEvent> events;

	public MLogic mLogic;

	public bool hasMLogic;

	public MKey mKey;

	public bool hasMKey;

	public bool isRunning;

	public bool isDone;

	public InsigniaTrigger.TriggerResult lastResult;

	public BasicInfo lastResultObject;

	private int triggerCount;

	public bool repeatEvent;

	private bool loopEnded;

	private float excessDelta;

	private static System.Random randomGenerator;

	private static bool hasRandomGenerator = false;

	public bool canTrigger
	{
		get
		{
			return triggerCount <= OptionsMaster.maxLogicTriggerCount;
		}
	}

	public int triggerTargetTypeCount
	{
		get
		{
			return 3;
		}
	}

	public string LogicError { get; private set; }

	public event LogicChangeHandler LogicChanged;

	public EntityLogic(TriggerType type, GenericEntity entity)
	{
		loadTriggerType = (triggerType = type);
		entityBehaviour = entity;
		if (entity.prefab.moddedEvents.Length > 0)
		{
			if (type == TriggerType.Modded)
			{
				loadModdedTriggerType = (moddedTriggerType = ModIds.GetTriggerByEffectiveId(entity.prefab.moddedEvents[0]));
			}
			else
			{
				loadModdedTriggerType = (moddedTriggerType = null);
			}
		}
		Init();
	}

	public EntityLogic(string type, GenericEntity entity)
	{
		if (!triggerTypeLookup.TryGetValue(type, out triggerType))
		{
			Debug.LogError("Couldn't find trigger type " + type + "!");
			triggerType = TriggerType.Activate;
		}
		if (entity.prefab.moddedEvents.Length > 0)
		{
			loadModdedTriggerType = (moddedTriggerType = null);
		}
		entityBehaviour = entity;
		Init();
	}

	public bool IsTrigger()
	{
		return triggerType == TriggerType.Enter || triggerType == TriggerType.Exit;
	}

	public bool IsKeyChange()
	{
		return triggerType == TriggerType.KeyPressed || triggerType == TriggerType.KeyReleased;
	}

	public bool SingleTarget()
	{
		return !allTargets || targets.Count == 1;
	}

	public bool UseSelf(EntityEvent evt)
	{
		return evt.entityList.Count == 0 && (IsVarEvent(evt.eventType) || EventContainer.IsMachineEvent(evt.eventType) == entityBehaviour.entity.isBuildZone || evt.eventData is ModdedEventContainer) && (!(evt.eventData is ModdedEventContainer) || ((ModdedEventContainer)evt.eventData).Event.PickMode == StatMaster.Mode.PickMode.Zone == entityBehaviour.entity.isBuildZone);
	}

	public bool IsVarEvent(EventContainer.EventType evt)
	{
		return evt == EventContainer.EventType.Variable || evt == EventContainer.EventType.Random;
	}

	public bool UseTriggerResult(EntityEvent evt, bool isBlock)
	{
		if (IsTrigger() && evt.entityList.Count == 0 && SingleTarget() && targets.Count > 0)
		{
			TriggerTarget triggerTarget = targets[0];
			if (isBlock)
			{
				if (triggerTarget.targetType == TriggerTargetType.AnyBlock || (triggerTarget.targetType == TriggerTargetType.Picker && triggerTarget.type == TriggerTargetObjectType.Block))
				{
					return true;
				}
			}
			else if (triggerTarget.targetType == TriggerTargetType.AnyLevelObject || (triggerTarget.targetType == TriggerTargetType.Picker && triggerTarget.type == TriggerTargetObjectType.Entity))
			{
				return true;
			}
		}
		return false;
	}

	public static ushort GenerateID()
	{
		if (!hasRandomGenerator)
		{
			randomGenerator = new System.Random();
			hasRandomGenerator = true;
		}
		return (ushort)randomGenerator.Next(0, 65535);
	}

	public static TriggerType GetTriggerType(string name)
	{
		TriggerType value;
		if (!triggerTypeLookup.TryGetValue(name, out value))
		{
			Debug.LogError("Could not find trigger type: " + name);
			return TriggerType.Activate;
		}
		return value;
	}

	public void Reset()
	{
		isDone = false;
		lastResult = InsigniaTrigger.TriggerResult.None;
		lastResultObject = null;
		for (int i = 0; i < events.Count; i++)
		{
			EntityEvent entityEvent = events[i];
			entityEvent.Reset();
			if (entityEvent.eventData is EventContainer.RepeatEvent)
			{
				(entityEvent.eventData as EventContainer.RepeatEvent).ResetCurrentCount();
			}
		}
	}

	public void ReplaceEntityReference(long oldReference, long newReference)
	{
		for (int i = 0; i < targets.Count; i++)
		{
			TriggerTarget triggerTarget = targets[i];
			triggerTarget.ReplaceEntityReference(oldReference, newReference);
		}
		for (int i = 0; i < events.Count; i++)
		{
			EntityEvent entityEvent = events[i];
			entityEvent.ReplaceEntityReference(oldReference, newReference);
		}
	}

	public bool GetEvent(ushort id, out EntityEvent evt)
	{
		for (int i = 0; i < events.Count; i++)
		{
			EntityEvent entityEvent = events[i];
			if (entityEvent.ID == id)
			{
				evt = entityEvent;
				return true;
			}
		}
		evt = null;
		return false;
	}

	public bool GetTarget(ushort id, out TriggerTarget target)
	{
		for (int i = 0; i < targets.Count; i++)
		{
			TriggerTarget triggerTarget = targets[i];
			if (triggerTarget.ID == id)
			{
				target = triggerTarget;
				return true;
			}
		}
		target = null;
		return false;
	}

	public void ResetValue()
	{
		triggerType = loadTriggerType;
		moddedTriggerType = loadModdedTriggerType;
		damageIncrement = loadDamageIncrement;
		useHPRangeToggle = loadUseHPRangeToggle;
		allTargets = loadAllTargets;
		varThreshold = loadVarThreshold;
		keyPressCode = loadKeyPressCode;
		varGlobal = loadVarGlobal;
		varKey = loadVarKey;
		varCompare = loadVarCompare;
		if (hasMKey)
		{
			mKey.AddOrReplaceKey(0, keyPressCode);
			mKey.ApplyValue();
		}
		InvokeLogicChanged();
		foreach (TriggerTarget target in targets)
		{
			target.ResetValue();
		}
		foreach (EntityEvent @event in events)
		{
			@event.ResetValue();
		}
	}

	public void AddTarget(TriggerTarget target)
	{
		targets.Add(target);
		InvokeLogicChanged();
	}

	public void RemoveTarget(TriggerTarget target)
	{
		targets.Remove(target);
		InvokeLogicChanged();
	}

	public bool IsValid()
	{
		LogicError = string.Empty;
		if (!canTrigger)
		{
			LogicError = LocalisationManager.GetTranslation(2029);
			return false;
		}
		if (HasInvalidEventTargets())
		{
			return false;
		}
		return true;
	}

	private bool HasInvalidEventTargets()
	{
		bool result = false;
		foreach (EntityEvent @event in events)
		{
			if (HasInvalidEventTarget(@event))
			{
				result = true;
				break;
			}
		}
		return result;
	}

	private bool HasInvalidEventTarget(EntityEvent logicEvent)
	{
		bool flag = true;
		foreach (long entity2 in logicEvent.entityList)
		{
			LevelEntity entity;
			if (entity2 == LevelPrefab.INVALID_ID || !LevelEditor.Instance.Get(entity2, out entity) || HasInvalidMachineEventEntity(logicEvent, entity))
			{
				continue;
			}
			LogicError = string.Format(LocalisationManager.GetTranslation(3273), logicEvent.eventType, entity.LogicName());
			flag = false;
			break;
		}
		return !flag;
	}

	private bool HasInvalidMachineEventEntity(EntityEvent logicEvent, LevelEntity eventEntity)
	{
		if (!EventContainer.IsMachineEvent(logicEvent.eventType))
		{
			return true;
		}
		return eventEntity.behaviour.prefab.ID == LevelEditor.BUILD_ZONE_ID;
	}

	public void AddEvent(EntityEvent evt)
	{
		events.Add(evt);
		InvokeLogicChanged();
	}

	public void RemoveEvent(EntityEvent evt)
	{
		events.Remove(evt);
		InvokeLogicChanged();
	}

	private void ProcessEvents()
	{
		if (!repeatEvent)
		{
			isDone = true;
		}
		for (int i = currentIndex; i < events.Count; i++)
		{
			EntityEvent entityEvent = events[i];
			ReferenceMaster.onExecuteEvent(entityBehaviour, this, entityEvent);
			entityEvent.Reset();
			entityEvent.eventData.Execute();
			if (!entityEvent.eventData.isDone)
			{
				currentIndex = i;
				loopEnded = (isDone = false);
				break;
			}
			loopEnded = i == events.Count - 1;
		}
	}

	public void Execute()
	{
		currentIndex = 0;
		excessDelta = 0f;
		triggerCount++;
		ProcessEvents();
		triggerCount = 0;
	}

	public void UpdateLogic(float delta, bool fixedUpdate)
	{
		if (isDone || currentIndex >= events.Count)
		{
			return;
		}
		EntityEvent entityEvent = events[currentIndex];
		if (fixedUpdate != entityEvent.eventData.useFixedUpdate)
		{
			return;
		}
		if (excessDelta > 0f)
		{
			delta += excessDelta;
		}
		excessDelta = entityEvent.eventData.UpdateEvent(delta);
		if (!isRunning || !entityEvent.eventData.isDone)
		{
			return;
		}
		currentIndex++;
		entityBehaviour.OnEventFinish(this, entityEvent);
		if (entityBehaviour.isDestroyed)
		{
			isRunning = false;
			return;
		}
		if (this.repeatEvent && loopEnded)
		{
			for (int i = 0; i < events.Count; i++)
			{
				events[i].eventData.isDone = false;
				if (events[i].eventData is EventContainer.RepeatEvent)
				{
					EventContainer.RepeatEvent repeatEvent = events[i].eventData as EventContainer.RepeatEvent;
					this.repeatEvent = repeatEvent.keepRepeating;
				}
			}
			currentIndex = 0;
		}
		ProcessEvents();
		if (excessDelta > 0f && currentIndex < events.Count)
		{
			excessDelta = events[currentIndex].eventData.UpdateEvent(excessDelta);
		}
	}

	private void CreateMKey()
	{
		if (!hasMKey)
		{
			mKey = new MKey("KeyPress", "key-press", KeyCode.R);
			hasMKey = true;
		}
	}

	private void Init()
	{
		ID = GenerateID();
		loadAllTargets = (allTargets = false);
		loadSimStart = (simStartTrigger = false);
		loadDamageIncrement = (damageIncrement = 5f);
		loadUseHPRangeToggle = (useHPRangeToggle = false);
		loadVarCompare = (varCompare = VarCompareType.Equals);
		loadVarKey = (varKey = LocalisationManager.GetTranslation(2938));
		loadVarThreshold = (varThreshold = 1f);
		loadVarGlobal = (varGlobal = true);
		loadKeyPressCode = (keyPressCode = KeyCode.R);
		if (triggerType == TriggerType.KeyPressed || triggerType == TriggerType.KeyReleased)
		{
			CreateMKey();
		}
		targets = new List<TriggerTarget>();
		events = new List<EntityEvent>();
		Reset();
	}

	public bool Encode(bool fullEncode, out byte[] data)
	{
		XDataHolder xDataHolder = new XDataHolder();
		OnSaveLogic(xDataHolder, string.Empty, fullEncode);
		return xDataHolder.Encode(out data);
	}

	public void ApplyValue()
	{
		loadTriggerType = triggerType;
		loadModdedTriggerType = moddedTriggerType;
		loadAllTargets = allTargets;
		loadSimStart = simStartTrigger;
		loadDamageIncrement = damageIncrement;
		loadUseHPRangeToggle = useHPRangeToggle;
		loadKeyPressCode = keyPressCode;
		loadVarGlobal = varGlobal;
		loadVarKey = varKey;
		loadVarCompare = varCompare;
		loadVarThreshold = varThreshold;
	}

	public int Decode(byte[] data, int offset)
	{
		XDataHolder xDataHolder = new XDataHolder();
		int result = xDataHolder.Decode(data, offset);
		OnLoadLogic(xDataHolder, string.Empty, false);
		InvokeLogicChanged();
		return result;
	}

	public bool CompareVariable(float newVal)
	{
		if (varCompare == VarCompareType.Equals)
		{
			return Mathf.Approximately(newVal, varThreshold);
		}
		if (varCompare == VarCompareType.EqualsOrHigher)
		{
			return newVal > varThreshold || Mathf.Approximately(newVal, varThreshold);
		}
		if (varCompare == VarCompareType.Higher)
		{
			return newVal > varThreshold;
		}
		if (varCompare == VarCompareType.EqualsOrLess)
		{
			return newVal < varThreshold || Mathf.Approximately(newVal, varThreshold);
		}
		if (varCompare == VarCompareType.Less)
		{
			return newVal < varThreshold;
		}
		Debug.LogError("Invalid compare type: " + varCompare);
		return false;
	}

	public void OnSaveLogicLoad(XDataHolder data, string prefix)
	{
		data.Write(prefix + "type", loadTriggerType.ToString());
		int iD = ID;
		data.Write(prefix + "id", iD);
		if (loadTriggerType == TriggerType.MachineDamage)
		{
			data.Write(prefix + "damage", loadDamageIncrement);
			data.Write(prefix + "useHealth", loadUseHPRangeToggle);
		}
		else if (loadTriggerType == TriggerType.Activate)
		{
			data.Write(prefix + "sim-trigger", loadSimStart);
		}
		else if (loadTriggerType == TriggerType.Variable)
		{
			int num = (int)loadVarCompare;
			data.Write(prefix + "var-data", loadVarKey + "|" + loadVarGlobal + "|" + num + "|" + loadVarThreshold);
		}
		else if (loadTriggerType == TriggerType.KeyPressed || loadTriggerType == TriggerType.KeyReleased)
		{
			int num2 = (int)loadKeyPressCode;
			data.Write(prefix + "key", num2);
		}
		else if (loadTriggerType == TriggerType.Modded && loadModdedTriggerType != null)
		{
			data.Write(prefix + "trigger-mod", loadModdedTriggerType.Info.Mod.Info.Id.ToString());
			data.Write(prefix + "trigger-id", loadModdedTriggerType.LocalId.ToString(StaticSettings.Culture));
		}
		if (IsTrigger())
		{
			List<string> list = new List<string>();
			for (int i = 0; i < targets.Count; i++)
			{
				list.Add(targets[i].SaveLoadValue());
			}
			data.Write(prefix + "targets", list.ToArray());
			data.Write(prefix + "all-targets", loadAllTargets);
		}
		List<string> list2 = new List<string>();
		for (int i = 0; i < events.Count; i++)
		{
			list2.Add(events[i].SaveLoadValue());
		}
		data.Write(prefix + "events", list2.ToArray());
	}

	public void OnSaveLogic(XDataHolder data, string prefix, bool fullSave)
	{
		data.Write(prefix + "type", triggerType.ToString());
		int iD = ID;
		data.Write(prefix + "id", iD);
		if (triggerType == TriggerType.MachineDamage)
		{
			data.Write(prefix + "damage", damageIncrement);
			data.Write(prefix + "useHealth", useHPRangeToggle);
		}
		else if (triggerType == TriggerType.Activate)
		{
			data.Write(prefix + "sim-trigger", simStartTrigger);
		}
		else if (triggerType == TriggerType.Variable)
		{
			int num = (int)varCompare;
			data.Write(prefix + "var-data", varKey + "|" + varGlobal + "|" + num + "|" + varThreshold);
		}
		else if (triggerType == TriggerType.KeyPressed || triggerType == TriggerType.KeyReleased)
		{
			int num2 = (int)keyPressCode;
			data.Write(prefix + "key", num2);
		}
		else if (triggerType == TriggerType.Modded && moddedTriggerType != null)
		{
			data.Write(prefix + "trigger-mod", moddedTriggerType.Info.Mod.Info.Id.ToString());
			data.Write(prefix + "trigger-id", moddedTriggerType.LocalId.ToString(StaticSettings.Culture));
		}
		if (!fullSave)
		{
			return;
		}
		if (IsTrigger())
		{
			List<string> list = new List<string>();
			for (int i = 0; i < targets.Count; i++)
			{
				list.Add(targets[i].Save());
			}
			data.Write(prefix + "targets", list.ToArray());
			data.Write(prefix + "all-targets", allTargets);
		}
		List<string> list2 = new List<string>();
		for (int i = 0; i < events.Count; i++)
		{
			list2.Add(events[i].Save());
		}
		data.Write(prefix + "events", list2.ToArray());
	}

	public void OnLoadLogic(XDataHolder data, string prefix, bool fullLoad)
	{
		string text = data.ReadString(prefix + "type");
		if (!triggerTypeLookup.TryGetValue(text, out triggerType))
		{
			Debug.Log("Couldn't find trigger type '" + text + "'!");
			return;
		}
		ID = (ushort)data.ReadInt(prefix + "id");
		if (triggerType == TriggerType.MachineDamage)
		{
			damageIncrement = data.ReadFloat(prefix + "damage");
			if (data.HasKey(prefix + "useHealth"))
			{
				useHPRangeToggle = data.ReadBool(prefix + "useHealth");
			}
			else
			{
				useHPRangeToggle = false;
			}
		}
		else if (triggerType == TriggerType.Activate)
		{
			simStartTrigger = data.ReadBool(prefix + "sim-trigger");
		}
		else if (triggerType == TriggerType.Variable)
		{
			string text2 = data.ReadString(prefix + "var-data");
			string[] array = text2.Split('|');
			varKey = array[0];
			varGlobal = array[1].Equals("True");
			varCompare = (VarCompareType)int.Parse(array[2]);
			varThreshold = float.Parse(array[3]);
		}
		else if (triggerType == TriggerType.KeyPressed || triggerType == TriggerType.KeyReleased)
		{
			CreateMKey();
			keyPressCode = (KeyCode)data.ReadInt(prefix + "key");
			mKey.AddOrReplaceKey(0, keyPressCode);
			mKey.ApplyValue();
		}
		else if (triggerType == TriggerType.Modded)
		{
			if (!data.HasKey(prefix + "trigger-id"))
			{
				Debug.LogWarning("Trigger has type Modded but no trigger info stored!");
				return;
			}
			string text3 = data.ReadString(prefix + "trigger-mod");
			string text4 = data.ReadString(prefix + "trigger-id");
			int result;
			if (!int.TryParse(text4, out result))
			{
				Debug.LogWarning("Can't parse trigger ID: " + text4);
				return;
			}
			moddedTriggerType = SingleInstanceFindOnly<TriggerLoader>.Instance.GetTriggerById(text3, result);
			if (moddedTriggerType == null)
			{
				Debug.LogWarning("Can't find trigger " + text4 + " of mod " + text3 + "!");
				return;
			}
		}
		if (fullLoad)
		{
			events.Clear();
			targets.Clear();
			if (IsTrigger())
			{
				string[] array2 = data.ReadStringArray(prefix + "targets");
				string[] array3 = array2;
				foreach (string data2 in array3)
				{
					targets.Add(new TriggerTarget(data2));
				}
				allTargets = data.ReadBool(prefix + "all-targets");
			}
			string[] array4 = data.ReadStringArray(prefix + "events");
			string[] array5 = array4;
			foreach (string data3 in array5)
			{
				EntityEvent entityEvent = new EntityEvent(data3);
				if (entityEvent != null)
				{
					events.Add(entityEvent);
				}
			}
		}
		ApplyValue();
	}

	public void ClearHandler()
	{
		this.LogicChanged = null;
	}

	public void InvokeLogicChanged()
	{
		LogicChangeHandler logicChanged = this.LogicChanged;
		if (logicChanged != null)
		{
			logicChanged();
		}
		TriggerLoaderCallback(this);
	}
}
