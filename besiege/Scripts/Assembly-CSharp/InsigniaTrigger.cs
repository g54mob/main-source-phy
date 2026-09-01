using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsigniaTrigger : GenericEntity
{
	public enum Type
	{
		Regular = 0,
		Checkpoint = 1,
		Pickup = 2
	}

	public enum TriggerResult
	{
		None = 0,
		Entity = 1,
		Block = 2,
		Projectile = 3,
		Misc = 4
	}

	private class TriggerContent
	{
		public Collider coll;

		public BasicInfo info;

		public bool hasInfo;

		public TriggerContent(BasicInfo i, Collider c, bool inf)
		{
			info = i;
			coll = c;
			hasInfo = inf;
		}
	}

	public FlashAlpha[] flashAlpha;

	public PulseAlpha[] pulseAlpha;

	public Renderer[] otherRenderers;

	public InsigniaTriggerObject triggerObject;

	public Type type;

	public Transform[] keepYScale;

	public MeshRenderer[] enableInWater = new MeshRenderer[0];

	public MeshRenderer[] disableInWater = new MeshRenderer[0];

	private bool projectileUpdate;

	private Bounds triggerBounds;

	private List<ProjectileScript> containedProjectiles;

	private List<ProjectileScript> updatedProjectiles;

	private List<TriggerContent> triggerContents;

	private MColourSlider visColorSlider;

	protected MToggle fadeOutFlash;

	private float colorMultiplier = 0.3f;

	private float glowMultiplier = 0.1f;

	private int floorLayer = 29;

	private IEnumerator fadeOutCoroutine;

	public override void OnScaleChanged(Vector3 scale)
	{
		for (int i = 0; i < keepYScale.Length; i++)
		{
			keepYScale[i].localScale = new Vector3(1f, 1f / scale.y, 1f);
		}
	}

	public override void OnPositionChanged(Vector3 pos)
	{
		EnvironmentChanged(LevelEditor.Instance.Settings);
	}

	private void EnvironmentChanged(LevelSettings s)
	{
		bool flag = s.Environment == LevelSettings.LevelEnvironment.Water && Mathf.Abs(base.transform.position.y - WaterController.waterTransformHeight) < 2f;
		for (int i = 0; i < enableInWater.Length; i++)
		{
			enableInWater[i].enabled = flag;
		}
		for (int j = 0; j < disableInWater.Length; j++)
		{
			disableInWater[j].enabled = !flag;
		}
	}

	public override bool TriggerEvaluate()
	{
		return false;
	}

	public override void Init()
	{
		if (!isInitialized)
		{
			visColorSlider = AddColourSlider(2504, "colour", new Color(0.5f, 0.5f, 1f), false);
			fadeOutFlash = AddToggle(3280, "flashfade", true);
			if (flashAlpha.Length == 0)
			{
				flashAlpha = base.gameObject.GetComponentsInChildren<FlashAlpha>();
			}
			if (pulseAlpha.Length == 0)
			{
				pulseAlpha = base.gameObject.GetComponentsInChildren<PulseAlpha>();
			}
			triggerContents = new List<TriggerContent>();
			containedProjectiles = new List<ProjectileScript>();
			updatedProjectiles = new List<ProjectileScript>();
			base.Init();
			SetVisColor(visColorSlider.Value);
			visColorSlider.ValueChanged += SetVisColor;
			if (StatMaster.isMP)
			{
				LevelEditor instance = LevelEditor.Instance;
				instance.LevelSettingsChanged = (LevelEditor.LevelSettingsChangedHandler)Delegate.Combine(instance.LevelSettingsChanged, new LevelEditor.LevelSettingsChangedHandler(EnvironmentChanged));
				EnvironmentChanged(LevelEditor.Instance.Settings);
			}
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		LevelEditor instance = LevelEditor.Instance;
		instance.LevelSettingsChanged = (LevelEditor.LevelSettingsChangedHandler)Delegate.Remove(instance.LevelSettingsChanged, new LevelEditor.LevelSettingsChangedHandler(EnvironmentChanged));
	}

	private bool GetInfo(BasicInfo info, out TriggerContent content)
	{
		for (int i = 0; i < triggerContents.Count; i++)
		{
			TriggerContent triggerContent = triggerContents[i];
			if (triggerContent.hasInfo && triggerContent.info == info)
			{
				content = triggerContent;
				return true;
			}
		}
		content = null;
		return false;
	}

	private bool GetCollider(Collider coll, out TriggerContent content)
	{
		for (int i = 0; i < triggerContents.Count; i++)
		{
			TriggerContent triggerContent = triggerContents[i];
			if (triggerContent.coll == coll)
			{
				content = triggerContent;
				return true;
			}
		}
		content = null;
		return false;
	}

	private void SetVisColor(Color color)
	{
		bool flag = color == new Color(0.5f, 0.5f, 1f);
		Color color2 = ((!flag) ? (Color.white * colorMultiplier * 0.3f + color * colorMultiplier * 0.7f) : (color * colorMultiplier));
		for (int i = 0; i < pulseAlpha.Length; i++)
		{
			for (int j = 0; j < pulseAlpha[i].rendys.Length; j++)
			{
				pulseAlpha[i].rendys[j].material.SetColor("_TintColor", new Color(color2.r, color2.g, color2.b, pulseAlpha[i].rendys[j].material.GetColor("_TintColor").a));
			}
		}
		for (int k = 0; k < otherRenderers.Length; k++)
		{
			otherRenderers[k].material.SetColor("_TintColor", new Color(color2.r, color2.g, color2.b, otherRenderers[k].material.GetColor("_TintColor").a));
		}
		color2 = ((!flag) ? (Color.white * glowMultiplier * 0.5f + color * glowMultiplier * 0.5f) : (color * glowMultiplier));
		for (int l = 0; l < flashAlpha.Length; l++)
		{
			float a = flashAlpha[l].startCol.a;
			flashAlpha[l].SetColor(new Color(color2.r, color2.g, color2.b, a));
		}
	}

	private void ClearTriggerContents()
	{
		while (triggerContents.Count > 0)
		{
			TriggerContent triggerContent = triggerContents[0];
			if (triggerContent.hasInfo && triggerContent.info.infoType == BasicInfoType.Block)
			{
				BlockBehaviour blockBehaviour = triggerContent.info as BlockBehaviour;
				blockBehaviour.onRespawn = (Action<BlockBehaviour>)Delegate.Remove(blockBehaviour.onRespawn, new Action<BlockBehaviour>(OnContentRespawn));
				blockBehaviour.respawnCallbackCount--;
			}
			triggerContents.RemoveAt(0);
		}
	}

	public override void Reset()
	{
		base.Reset();
		ClearTriggerContents();
		containedProjectiles.Clear();
	}

	public override void SetupDefault()
	{
		base.SetupDefault();
		EntityLogic entityLogic = new EntityLogic(TriggerType.Enter, this);
		TriggerTarget triggerTarget = new TriggerTarget(TriggerTargetType.Picker);
		triggerTarget.type = TriggerTargetObjectType.Block;
		triggerTarget.TargetBlockType = BlockType.StartingBlock;
		entityLogic.targets.Add(triggerTarget);
		triggerTarget.ApplyValue();
		switch (type)
		{
		case Type.Regular:
		{
			EntityEvent entityEvent = new EntityEvent(EventContainer.EventType.Progress);
			entityLogic.events.Add(entityEvent);
			entityEvent.ApplyValue();
			break;
		}
		case Type.Checkpoint:
		{
			EntityEvent entityEvent = new EntityEvent(EventContainer.EventType.Progress);
			entityLogic.events.Add(entityEvent);
			entityEvent.ApplyValue();
			entityEvent = new EntityEvent(EventContainer.EventType.SetRespawn);
			entityLogic.events.Add(entityEvent);
			entityEvent.ApplyValue();
			entityEvent = new EntityEvent(EventContainer.EventType.Activate);
			entityLogic.events.Add(entityEvent);
			entityEvent.ApplyValue();
			entityEvent = new EntityEvent(EventContainer.EventType.Deactivate);
			entityLogic.events.Add(entityEvent);
			entityEvent.ApplyValue();
			break;
		}
		case Type.Pickup:
		{
			EntityEvent entityEvent = new EntityEvent(EventContainer.EventType.ReloadMachine);
			entityLogic.events.Add(entityEvent);
			entityEvent.ApplyValue();
			break;
		}
		}
		logicData.Add(entityLogic);
		entityLogic.ApplyValue();
	}

	public void Flash()
	{
		if (StatMaster.isHosting && triggerObject.SimPhysics)
		{
			entity.Event(NetworkEntity.EntityEvent.InsigniaFlash);
		}
		if (entity.hasSpawned)
		{
			for (int i = 0; i < flashAlpha.Length; i++)
			{
				flashAlpha[i].Flash(!fadeOutFlash.IsActive);
			}
		}
	}

	public override void ActivateEntity()
	{
		if (fadeOutCoroutine != null)
		{
			StopCoroutine(fadeOutCoroutine);
			for (int i = 0; i < pulseAlpha.Length; i++)
			{
				pulseAlpha[i].StopAllCoroutines();
			}
			for (int j = 0; j < flashAlpha.Length; j++)
			{
				flashAlpha[j].StopAllCoroutines();
			}
		}
		base.gameObject.SetActive(false);
		base.gameObject.SetActive(true);
		triggerObject.Toggle(true);
	}

	public override void DeactivateEntity()
	{
		triggerObject.Toggle(false);
		ClearTriggerContents();
		containedProjectiles.Clear();
		fadeOutCoroutine = FadeOut();
		if (base.gameObject != null)
		{
			StartCoroutine(fadeOutCoroutine);
		}
	}

	public IEnumerator FadeOut()
	{
		for (int i = 0; i < pulseAlpha.Length; i++)
		{
			pulseAlpha[i].FadeOut();
		}
		for (int j = 0; j < flashAlpha.Length; j++)
		{
			if (flashAlpha[j].gameObject.activeInHierarchy)
			{
				flashAlpha[j].StartCoroutine(flashAlpha[j].FadeOut());
			}
		}
		for (int k = 0; k < flashAlpha.Length; k++)
		{
			while (flashAlpha[k].Transitioning)
			{
				yield return null;
			}
		}
		base.gameObject.SetActive(false);
	}

	public void TriggerEnter(Collider coll)
	{
		if (hasLogic)
		{
			EvaluateTrigger(coll, true);
		}
	}

	public void TriggerExit(Collider coll)
	{
		if (hasLogic)
		{
			EvaluateTrigger(coll, false);
		}
	}

	private void OnContentRespawn(BlockBehaviour b)
	{
		for (int i = 0; i < triggerContents.Count; i++)
		{
			TriggerContent triggerContent = triggerContents[i];
			if (triggerContent.hasInfo && triggerContent.info == b)
			{
				EvaluateTrigger(triggerContent.coll, false);
				break;
			}
		}
	}

	private bool EvaluateTrigger(Collider coll, bool isEnter)
	{
		if (coll.gameObject.layer == floorLayer)
		{
			return false;
		}
		BasicInfo basicInfo = coll.GetComponentInParent<BasicInfo>();
		bool flag = basicInfo != null;
		TriggerContent content = null;
		if (flag && basicInfo.infoType == BasicInfoType.Entity && !(basicInfo as GenericEntity).TriggerEvaluate())
		{
			return false;
		}
		if (flag && basicInfo.infoType == BasicInfoType.None && !(basicInfo is GenericEntity))
		{
			LevelEntity component = basicInfo.GetComponent<LevelEntity>();
			if (component != null && component.hasBase)
			{
				component = component.baseEntity as LevelEntity;
				if (component.hasBehaviour)
				{
					basicInfo = component.behaviour;
				}
			}
		}
		if (isEnter)
		{
			if ((flag && !GetInfo(basicInfo, out content)) || (!flag && !GetCollider(coll, out content)))
			{
				if (flag && basicInfo.infoType == BasicInfoType.Block)
				{
					BlockBehaviour blockBehaviour = basicInfo as BlockBehaviour;
					BlockBehaviour blockBehaviour2 = blockBehaviour;
					blockBehaviour2.onRespawn = (Action<BlockBehaviour>)Delegate.Combine(blockBehaviour2.onRespawn, new Action<BlockBehaviour>(OnContentRespawn));
					blockBehaviour.respawnCallbackCount++;
				}
				triggerContents.Add(new TriggerContent(basicInfo, coll, flag));
			}
			else if (content.coll != coll)
			{
				return false;
			}
		}
		else if (!isEnter)
		{
			if ((!flag || !GetInfo(basicInfo, out content)) && (flag || !GetCollider(coll, out content)))
			{
				return false;
			}
			if (flag && basicInfo.infoType == BasicInfoType.Block)
			{
				BlockBehaviour blockBehaviour = basicInfo as BlockBehaviour;
				BlockBehaviour blockBehaviour3 = blockBehaviour;
				blockBehaviour3.onRespawn = (Action<BlockBehaviour>)Delegate.Remove(blockBehaviour3.onRespawn, new Action<BlockBehaviour>(OnContentRespawn));
				blockBehaviour.respawnCallbackCount--;
			}
			triggerContents.Remove(content);
		}
		bool flag2 = false;
		bool flag3 = false;
		List<EntityLogic> logic = GetLogic();
		for (int i = 0; i < logic.Count; i++)
		{
			EntityLogic entityLogic = logic[i];
			if (entityLogic.isRunning)
			{
				continue;
			}
			flag3 = entityLogic.SingleTarget();
			if (!flag3)
			{
				flag2 = true;
			}
			if (((!isEnter || entityLogic.triggerType != TriggerType.Enter) && (isEnter || entityLogic.triggerType != TriggerType.Exit)) || !flag3)
			{
				continue;
			}
			LevelEntity levelEntity;
			BlockBehaviour block;
			ProjectileInfo proj;
			entityLogic.lastResult = ContainsAny(flag, basicInfo, entityLogic.targets, out levelEntity, out block, out proj);
			if (entityLogic.lastResult != TriggerResult.None)
			{
				if (entityLogic.lastResult == TriggerResult.Block)
				{
					entityLogic.lastResultObject = block;
				}
				else if (entityLogic.lastResult == TriggerResult.Projectile)
				{
					entityLogic.lastResultObject = proj;
				}
				ExecuteLogic(entityLogic);
			}
			else
			{
				entityLogic.lastResultObject = null;
			}
		}
		if (!flag2)
		{
			return false;
		}
		CleanContentLists();
		for (int i = 0; i < logic.Count; i++)
		{
			EntityLogic entityLogic = logic[i];
			if (!entityLogic.SingleTarget() && ((isEnter && entityLogic.triggerType == TriggerType.Enter) || (!isEnter && entityLogic.triggerType == TriggerType.Exit)) && Contains(isEnter, entityLogic.targets))
			{
				ExecuteLogic(entityLogic);
			}
		}
		return false;
	}

	public override void TriggerActivate(bool isSimStart)
	{
		base.TriggerActivate(isSimStart);
		if (needsProjectileUpdate)
		{
			if (!projectileUpdate)
			{
				ProjectileScript.onUpdateProjectile = (Action<ProjectileScript>)Delegate.Combine(ProjectileScript.onUpdateProjectile, new Action<ProjectileScript>(UpdateTriggerProjectile));
				projectileUpdate = true;
				updateOnTransformEvent = true;
			}
			triggerBounds = triggerObject.triggerObj.bounds;
		}
		else if (projectileUpdate)
		{
			ProjectileScript.onUpdateProjectile = (Action<ProjectileScript>)Delegate.Remove(ProjectileScript.onUpdateProjectile, new Action<ProjectileScript>(UpdateTriggerProjectile));
			projectileUpdate = false;
			updateOnTransformEvent = false;
		}
	}

	public override void UpdateOnTransformEvent()
	{
		base.UpdateOnTransformEvent();
		triggerBounds = triggerObject.triggerObj.bounds;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (projectileUpdate)
		{
			ProjectileScript.onUpdateProjectile = (Action<ProjectileScript>)Delegate.Remove(ProjectileScript.onUpdateProjectile, new Action<ProjectileScript>(UpdateTriggerProjectile));
			projectileUpdate = false;
			updateOnTransformEvent = false;
		}
	}

	protected override void LateUpdate()
	{
		base.LateUpdate();
		if (!projectileUpdate || (updatedProjectiles.Count == 0 && containedProjectiles.Count == 0))
		{
			return;
		}
		if (updatedProjectiles.Count > 0)
		{
			for (int i = 0; i < updatedProjectiles.Count; i++)
			{
				ProjectileScript projectileScript = updatedProjectiles[i];
				if (triggerBounds.Contains(projectileScript.projectilePosition))
				{
					if (!containedProjectiles.Contains(projectileScript))
					{
						containedProjectiles.Add(projectileScript);
						EvaluateTrigger(projectileScript.col, true);
					}
				}
				else if (containedProjectiles.Contains(projectileScript))
				{
					containedProjectiles.Remove(projectileScript);
					EvaluateTrigger(projectileScript.col, false);
				}
			}
		}
		for (int i = 0; i < containedProjectiles.Count; i++)
		{
			ProjectileScript projectileScript = containedProjectiles[i];
			if (!updatedProjectiles.Contains(projectileScript))
			{
				containedProjectiles.Remove(projectileScript);
				EvaluateTrigger(projectileScript.col, false);
			}
		}
		updatedProjectiles.Clear();
	}

	private void UpdateTriggerProjectile(ProjectileScript proj)
	{
		updatedProjectiles.Add(proj);
	}

	protected override void ExecuteLogic(EntityLogic logic)
	{
		if (!StatMaster.isClient || StatMaster.isLocalSim)
		{
			Flash();
			base.ExecuteLogic(logic);
		}
	}

	private bool HasEntity(List<TriggerContent> entityList, int prefabID, bool checkId, long identifier)
	{
		if (checkId)
		{
			foreach (TriggerContent entity in entityList)
			{
				if (entity.hasInfo && entity.info is GenericEntity)
				{
					GenericEntity genericEntity = entity.info as GenericEntity;
					if (genericEntity.entity.identifier == identifier)
					{
						return true;
					}
				}
			}
		}
		else
		{
			foreach (TriggerContent entity2 in entityList)
			{
				if (entity2.hasInfo && entity2.info is GenericEntity)
				{
					GenericEntity genericEntity2 = entity2.info as GenericEntity;
					if (genericEntity2.prefab.ID == prefabID)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	private bool HasBlock(List<TriggerContent> entityList, BlockType blockType, MPTeam team)
	{
		foreach (TriggerContent entity in entityList)
		{
			if (entity.hasInfo && entity.info.infoType == BasicInfoType.Block)
			{
				BlockBehaviour blockBehaviour = entity.info as BlockBehaviour;
				if (blockBehaviour.Prefab.Type == blockType && blockBehaviour.Team == team)
				{
					return true;
				}
			}
		}
		return false;
	}

	private void CleanContentLists()
	{
		for (int i = 0; i < triggerContents.Count; i++)
		{
			TriggerContent triggerContent = triggerContents[i];
			if (triggerContent.coll == null)
			{
				if (triggerContent.hasInfo && triggerContent.info.infoType == BasicInfoType.Block)
				{
					BlockBehaviour blockBehaviour = triggerContent.info as BlockBehaviour;
					blockBehaviour.onRespawn = (Action<BlockBehaviour>)Delegate.Remove(blockBehaviour.onRespawn, new Action<BlockBehaviour>(OnContentRespawn));
					blockBehaviour.respawnCallbackCount--;
				}
				triggerContents.Remove(triggerContent);
				i--;
			}
		}
	}

	private TriggerResult ContainsAny(bool hasInfo, BasicInfo info, List<TriggerTarget> entityList, out LevelEntity entity, out BlockBehaviour block, out ProjectileInfo proj)
	{
		entity = null;
		block = null;
		proj = null;
		for (int i = 0; i < entityList.Count; i++)
		{
			TriggerTarget triggerTarget = entityList[i];
			if (triggerTarget.targetType == TriggerTargetType.Anything)
			{
				return TriggerResult.Misc;
			}
			if (triggerTarget.targetType == TriggerTargetType.Picker)
			{
				if (triggerTarget.type == TriggerTargetObjectType.All)
				{
					if (hasInfo)
					{
						if (info.infoType == BasicInfoType.Entity)
						{
							entity = (info as GenericEntity).entity;
							return TriggerResult.Entity;
						}
						if (info.infoType == BasicInfoType.Block)
						{
							block = info as BlockBehaviour;
							return TriggerResult.Block;
						}
						if (info.infoType == BasicInfoType.Projectile)
						{
							proj = info as ProjectileInfo;
							return TriggerResult.Projectile;
						}
					}
				}
				else if (triggerTarget.type == TriggerTargetObjectType.Entity)
				{
					if (!hasInfo || info.infoType != BasicInfoType.Entity)
					{
						continue;
					}
					entity = (info as GenericEntity).entity;
					if (triggerTarget.IsEntityType)
					{
						if ((info as GenericEntity).prefab.ID == triggerTarget.PrefabID)
						{
							return TriggerResult.Entity;
						}
					}
					else if (entity.identifier == triggerTarget.EntityID)
					{
						return TriggerResult.Entity;
					}
				}
				else if (triggerTarget.type == TriggerTargetObjectType.Block && hasInfo && info.infoType == BasicInfoType.Block)
				{
					block = info as BlockBehaviour;
					if (block.Prefab.Type == triggerTarget.TargetBlockType && block.Team == triggerTarget.Team)
					{
						return TriggerResult.Block;
					}
				}
			}
			else if (triggerTarget.targetType == TriggerTargetType.AnyLevelObject)
			{
				if (hasInfo && info.infoType == BasicInfoType.Entity)
				{
					entity = (info as GenericEntity).entity;
					return TriggerResult.Entity;
				}
			}
			else if (triggerTarget.targetType == TriggerTargetType.AnyBlock)
			{
				if (hasInfo && info.infoType == BasicInfoType.Block)
				{
					block = info as BlockBehaviour;
					if (block.Team == triggerTarget.Team)
					{
						return TriggerResult.Block;
					}
				}
			}
			else if (triggerTarget.targetType == TriggerTargetType.AnyProjectile && hasInfo && info.infoType == BasicInfoType.Projectile)
			{
				proj = info as ProjectileInfo;
				if ((!proj.HasParentMachine && triggerTarget.Team == MPTeam.None) || (proj.HasParentMachine && (proj.ParentMachine as ServerMachine).player.team == triggerTarget.Team))
				{
					return TriggerResult.Projectile;
				}
			}
		}
		return TriggerResult.None;
	}

	private bool Contains(bool isAll, List<TriggerTarget> entityList)
	{
		bool flag = false;
		foreach (TriggerTarget entity in entityList)
		{
			if (entity.targetType == TriggerTargetType.Anything)
			{
				flag = triggerContents.Count > 0;
			}
			else if (entity.targetType == TriggerTargetType.Picker)
			{
				flag = ((entity.type == TriggerTargetObjectType.All) ? (triggerContents.Count > 0) : ((entity.type != TriggerTargetObjectType.Entity) ? HasBlock(triggerContents, entity.TargetBlockType, entity.Team) : HasEntity(triggerContents, entity.PrefabID, !entity.IsEntityType, entity.EntityID)));
			}
			else
			{
				flag = false;
				for (int i = 0; i < triggerContents.Count; i++)
				{
					TriggerContent triggerContent = triggerContents[i];
					if (entity.targetType == TriggerTargetType.AnyLevelObject)
					{
						if (triggerContent.hasInfo && triggerContent.info.infoType == BasicInfoType.Entity)
						{
							flag = true;
							break;
						}
					}
					else if (entity.targetType == TriggerTargetType.AnyBlock)
					{
						if (triggerContent.hasInfo && triggerContent.info.infoType == BasicInfoType.Block && (triggerContent.info as BlockBehaviour).Team == entity.Team)
						{
							flag = true;
							break;
						}
					}
					else if (entity.targetType == TriggerTargetType.AnyProjectile && triggerContent.hasInfo && triggerContent.info.infoType == BasicInfoType.Projectile && ((!triggerContent.info.HasParentMachine && entity.Team == MPTeam.None) || (triggerContent.info.HasParentMachine && (triggerContent.info.ParentMachine as ServerMachine).player.team == entity.Team)))
					{
						flag = true;
						break;
					}
				}
			}
			if (flag != isAll)
			{
				return false;
			}
		}
		return true;
	}
}
