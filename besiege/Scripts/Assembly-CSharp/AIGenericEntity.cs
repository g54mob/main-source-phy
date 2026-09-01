using System.Collections.Generic;
using Localisation;
using UnityEngine;

public class AIGenericEntity : GenericEntity
{
	public new EntityAI aiEntity;

	private NetworkBlock netBlock;

	private MTeam aiTeam;

	private MValue healthValue;

	private MValue attackDamage;

	private MValue blockDamage;

	private MValue attackForce;

	private MSlider attackSpeed;

	private MSlider rangedPrediction;

	private MToggle UseSmartTargeting;

	private MToggle AvoidFire;

	private MText DamageMultiplier;

	private MValue DamageMultiplierFire;

	private MValue DamageMultiplierBlunt;

	private MValue DamageMultiplierSharp;

	private MToggle CanJump;

	private MValue JumpHight;

	private MToggle isStationary;

	private MValue minVelocityFalling;

	private MToggle UseMoral;

	private MToggle FleeOnfire;

	private bool hasAIEntity;

	public MPTeam Team
	{
		get
		{
			return aiTeam.Team;
		}
	}

	public override bool PhysicsEnabled
	{
		get
		{
			if (StatMaster.isMP && entity.hasBase)
			{
				LevelEntity levelEntity = entity.baseEntity as LevelEntity;
				return levelEntity.behaviour.PhysicsEnabled;
			}
			return hasPhysics && !entity.isStatic;
		}
	}

	protected override bool IsCompatibleTrigger(TriggerType trigger)
	{
		return base.IsCompatibleTrigger(trigger) || trigger == TriggerType.Behaviour;
	}

	public override void Init()
	{
		if (isInitialized)
		{
			return;
		}
		base.Init();
		aiTeam = AddTeam(2479, GenericEntity.LOGIC_PREFIX + "team", MPTeam.None);
		aiTeam.TeamChanged += SetTeam;
		hasAIEntity = aiEntity != null;
		if (!hasAIEntity)
		{
			aiEntity = GetComponent<EntityAI>();
			hasAIEntity = aiEntity != null;
			if (!hasAIEntity)
			{
				Debug.LogError("AIGenericEntity::Init(): AIEntity is null!");
				return;
			}
		}
		healthValue = AddValue(2480, GenericEntity.LOGIC_PREFIX + "health", aiEntity.health, 1f, float.MaxValue);
		healthValue.ValueChanged += OnHealthChanged;
		if ((bool)aiEntity.my.attackScript)
		{
			attackDamage = AddValue(2481, GenericEntity.LOGIC_PREFIX + "AD", aiEntity.my.attackScript.attackDamage);
			attackDamage.ValueChanged += OnADChanged;
			blockDamage = AddValue(2482, GenericEntity.LOGIC_PREFIX + "BD", aiEntity.my.attackScript.blockDamageAmount);
			blockDamage.ValueChanged += OnBDChanged;
			attackForce = AddValue(2483, GenericEntity.LOGIC_PREFIX + "AttackForce", aiEntity.my.attackScript.impactForceAddition, 0f, float.MaxValue);
			attackForce.ValueChanged += OnAttackForceChanged;
			if (aiEntity.my.attackScript.ranged)
			{
				rangedPrediction = AddSlider(LocalisationManager.GetTranslation(2484), GenericEntity.LOGIC_PREFIX + "RP", aiEntity.my.attackScript.range.predictionScalar, 0f, 1f, string.Empty);
				rangedPrediction.ValueChanged += OnRPChanged;
			}
			attackSpeed = AddSlider(LocalisationManager.GetTranslation(2485), GenericEntity.LOGIC_PREFIX + "AS", aiEntity.my.attackScript.attackDelay, 0.1f, 10f, string.Empty);
			attackSpeed.ValueChanged += OnASChanged;
			if (PhysicsEnabled && StatMaster.levelSimulating)
			{
				aiEntity.my.attackScript.meleeAttackRange *= (base.transform.localScale.x + base.transform.localScale.y + base.transform.localScale.z) / 3f;
			}
		}
		UseSmartTargeting = AddToggle(2486, GenericEntity.LOGIC_PREFIX + "UST", LocalisationManager.GetTranslation(3343), aiEntity.disposition.SmartTargeting);
		UseSmartTargeting.Toggled += OnUSTChange;
		AvoidFire = AddToggle(2487, GenericEntity.LOGIC_PREFIX + "Avoid Fire", aiEntity.disposition.AvoidFire);
		AvoidFire.Toggled += OnAFChange;
		isStationary = AddToggle(2488, GenericEntity.LOGIC_PREFIX + "Can Move", aiEntity.movement.Able);
		isStationary.Toggled += OnCMChange;
		UseMoral = AddToggle(2489, GenericEntity.LOGIC_PREFIX + "Use Moral", LocalisationManager.GetTranslation(3344), aiEntity.retreating.useMoral);
		UseMoral.Toggled += OnUMChange;
		aiEntity.my.aiGenEntity = this;
		CreateJointToVehicles();
	}

	private void CreateJointToVehicles()
	{
		if (!PhysicsEnabled || !StatMaster.levelSimulating || (!StatMaster.isHosting && !StatMaster.isLocalSim) || !(Vector3.Dot(Vector3.up, base.transform.up) > 0.707f))
		{
			return;
		}
		LayerMask layerMask = AddPiece.CreateLayerMask(21, 24, 26, 28, 29);
		RaycastHit[] array = Physics.RaycastAll(base.transform.position + Vector3.down, Vector3.down, 0.35f, layerMask);
		for (int i = 0; i < array.Length; i++)
		{
			if ((bool)array[i].collider.attachedRigidbody && array[i].collider.gameObject.CompareTag("AIJoinable"))
			{
				HingeJoint hingeJoint = base.gameObject.AddComponent<HingeJoint>();
				hingeJoint.enableCollision = false;
				hingeJoint.axis = Vector3.up;
				hingeJoint.anchor = Vector3.down;
				hingeJoint.autoConfigureConnectedAnchor = true;
				hingeJoint.connectedBody = array[i].collider.attachedRigidbody;
				hingeJoint.breakForce = 25000f;
				hingeJoint.breakTorque = 50000f;
				aiEntity.groundJoint = hingeJoint;
				aiEntity.grounded = true;
				aiEntity.useJointAsGround = true;
				break;
			}
		}
	}

	protected override void OnPhysicsToggled(bool toggle)
	{
		base.OnPhysicsToggled(toggle);
		if (hasAIEntity)
		{
			aiEntity.enabled = toggle;
			if (aiEntity.my.attackScript != null)
			{
				aiEntity.my.attackScript.enabled = toggle;
			}
			if (aiEntity.my.killingHandler != null)
			{
				aiEntity.my.killingHandler.enabled = toggle;
			}
		}
	}

	protected override void Start()
	{
		base.Start();
		hasAIEntity = aiEntity != null;
		if (!StatMaster.isMP || (StatMaster.isMP && StatMaster.levelSimulating))
		{
			Init();
		}
		if (!hasAIEntity)
		{
			Debug.LogError("AIGenericEntity::Init(): AIEntity is null!");
		}
		else if (StatMaster.isMP && StatMaster.levelSimulating && !PhysicsEnabled)
		{
			aiEntity.enabled = false;
			if ((bool)aiEntity.my.attackScript)
			{
				aiEntity.my.attackScript.enabled = false;
			}
			aiEntity.my.killingHandler.enabled = false;
			if ((bool)aiEntity.my.killingHandler.my.bomb)
			{
				aiEntity.my.killingHandler.my.bomb.enabled = false;
				aiEntity.my.killingHandler.my.bomb.hasExploded = true;
			}
			Rigidbody.isKinematic = true;
		}
		else if (StatMaster.levelSimulating)
		{
			if (MeshRenderer == null)
			{
				MeshRenderer = aiEntity.my.VisObject.GetComponentInChildren<MeshRenderer>();
			}
			float num = (base.transform.localScale.x + base.transform.localScale.y + base.transform.localScale.z) / 3f;
			aiEntity.bob.Rate *= num;
			aiEntity.bob.Amount *= 1f / num;
		}
	}

	public override void TriggerActivate(bool isSimStart)
	{
		TriggerEvent(TriggerType.Behaviour);
		base.TriggerActivate(isSimStart);
	}

	public void SetTeam(MPTeam team)
	{
		if (hasAIEntity)
		{
			aiEntity.factionSystem.team = team;
			aiEntity.factionSystem.factionName = ((team != MPTeam.None) ? team.ToString() : null);
		}
	}

	protected void OnHealthChanged(float newhealth)
	{
		aiEntity.health = newhealth;
	}

	protected void OnADChanged(float newValue)
	{
		aiEntity.my.attackScript.attackDamage = newValue;
	}

	protected void OnBDChanged(float newValue)
	{
		aiEntity.my.attackScript.blockDamageAmount = newValue;
	}

	protected void OnRPChanged(float newValue)
	{
		if (newValue == 0f)
		{
			aiEntity.my.attackScript.range.prediction = false;
		}
		else
		{
			aiEntity.my.attackScript.range.prediction = true;
		}
		aiEntity.my.attackScript.range.predictionScalar = newValue;
	}

	protected void OnUSTChange(bool newValue)
	{
		aiEntity.disposition.SmartTargeting = newValue;
	}

	protected void OnAFChange(bool newValue)
	{
		aiEntity.disposition.AvoidFire = newValue;
	}

	protected void OnCMChange(bool newValue)
	{
		aiEntity.movement.Able = newValue;
	}

	protected void OnUMChange(bool newValue)
	{
		aiEntity.retreating.useMoral = newValue;
	}

	protected void OnAttackForceChanged(float newValue)
	{
		aiEntity.my.attackScript.impactForceAddition = newValue;
	}

	protected void OnASChanged(float newValue)
	{
		aiEntity.my.attackScript.attackDelay = 10f / newValue;
	}

	public override void TriggerEvent(TriggerType triggerType)
	{
		base.TriggerEvent(triggerType);
		if (!hasAIEntity || triggerType != TriggerType.Behaviour)
		{
			return;
		}
		aiEntity.disposition.behaviours.Clear();
		List<EntityLogic> logic = GetLogic();
		for (int i = 0; i < logic.Count; i++)
		{
			EntityLogic entityLogic = logic[i];
			if (entityLogic.triggerType != TriggerType.Behaviour)
			{
				continue;
			}
			for (int j = 0; j < entityLogic.events.Count; j++)
			{
				EntityEvent entityEvent = entityLogic.events[j];
				ReferenceMaster.onExecuteEvent(entityLogic.entityBehaviour, entityLogic, entityEvent);
				EventContainer.EntityBehaviourEvent entityBehaviourEvent = entityEvent.eventData as EventContainer.EntityBehaviourEvent;
				EntityAI.Action a = EntityAI.Action.None;
				switch (entityEvent.eventType)
				{
				case EventContainer.EventType.Approach:
					a = EntityAI.Action.PursueTarget;
					break;
				case EventContainer.EventType.Pursue:
					a = EntityAI.Action.PursueTarget;
					break;
				case EventContainer.EventType.FactionCharge:
					a = EntityAI.Action.FactionCharge;
					break;
				case EventContainer.EventType.Flee:
					a = EntityAI.Action.Flee;
					break;
				case EventContainer.EventType.Strafe:
					a = EntityAI.Action.Strafe;
					break;
				case EventContainer.EventType.WalkAround:
					a = EntityAI.Action.WalkAround;
					break;
				case EventContainer.EventType.Stationary:
					a = EntityAI.Action.Stationary;
					break;
				}
				if (aiEntity.disposition != null)
				{
					aiEntity.disposition.behaviours.Add(new EntityAI.Behaviour(entityBehaviourEvent.activationDistance, a, entityBehaviourEvent.speed, entityBehaviourEvent.attack));
				}
			}
			entityLogic.isDone = true;
		}
	}

	public override void SetupDefault()
	{
		base.SetupDefault();
		EntityLogic entityLogic = new EntityLogic(TriggerType.Behaviour, this);
		if (!hasAIEntity)
		{
			return;
		}
		EntityEvent entityEvent = null;
		for (int i = 0; i < aiEntity.disposition.behaviours.Count; i++)
		{
			EntityAI.Behaviour behaviour = aiEntity.disposition.behaviours[i];
			switch (behaviour.Action)
			{
			case EntityAI.Action.ApproachTarget:
				entityEvent = new EntityEvent(EventContainer.EventType.Pursue);
				break;
			case EntityAI.Action.PursueTarget:
				entityEvent = new EntityEvent(EventContainer.EventType.Pursue);
				break;
			case EntityAI.Action.Flee:
				entityEvent = new EntityEvent(EventContainer.EventType.Flee);
				break;
			case EntityAI.Action.Strafe:
				entityEvent = new EntityEvent(EventContainer.EventType.Strafe);
				break;
			case EntityAI.Action.Stationary:
				entityEvent = new EntityEvent(EventContainer.EventType.Stationary);
				break;
			case EntityAI.Action.FactionCharge:
				entityEvent = new EntityEvent(EventContainer.EventType.FactionCharge);
				break;
			case EntityAI.Action.WalkAround:
				entityEvent = new EntityEvent(EventContainer.EventType.WalkAround);
				break;
			case EntityAI.Action.None:
				continue;
			}
			EventContainer.EntityBehaviourEvent entityBehaviourEvent = entityEvent.eventData as EventContainer.EntityBehaviourEvent;
			entityBehaviourEvent.activationDistance = behaviour.Radius;
			entityBehaviourEvent.speed = behaviour.parameters.Speed;
			entityBehaviourEvent.attack = behaviour.attackState;
			entityLogic.events.Add(entityEvent);
			entityEvent.ApplyValue();
		}
		logicData.Add(entityLogic);
		entityLogic.ApplyValue();
	}
}
