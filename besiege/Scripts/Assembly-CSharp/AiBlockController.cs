using System.Collections.Generic;
using UnityEngine;

public class AiBlockController : BlockBehaviour
{
	public enum ControlMethod
	{
		Both = 0,
		UseTargetingSystem = 1,
		UseBehaviours = 2,
		None = 3
	}

	public AttackScript attack;

	public bool isFrozen;

	public EntityAI ai;

	public KillingHandler killingHandler;

	public SetPoseForAI setPoseForAI;

	public ConfigurableJoint footJoint;

	public Rigidbody rb;

	public EntityAI AI
	{
		get
		{
			if (ai == null)
			{
				ai = GetComponent<EntityAI>();
			}
			return ai;
		}
	}

	public bool IsJointedFeet
	{
		get
		{
			if (FootJoint != null)
			{
				return FootJoint.connectedBody ? true : false;
			}
			return false;
		}
	}

	public AttackScript AIAttackHandler
	{
		get
		{
			return attack;
		}
	}

	public KillingHandler AIKillingHandler
	{
		get
		{
			return killingHandler;
		}
	}

	public SetPoseForAI AIPoseHandler
	{
		get
		{
			return setPoseForAI;
		}
	}

	public ConfigurableJoint FootJoint
	{
		get
		{
			return footJoint;
		}
	}

	public Rigidbody Body
	{
		get
		{
			if (rb == null)
			{
				rb = GetComponent<Rigidbody>();
			}
			return rb;
		}
	}

	public List<EntityAI.Behaviour> BehaviourList
	{
		get
		{
			return AI.disposition.behaviours;
		}
	}

	protected override void Start()
	{
		base.Start();
		if (attack == null)
		{
			attack = GetComponent<AttackScript>();
		}
		if (killingHandler == null)
		{
			killingHandler = GetComponent<KillingHandler>();
		}
		if (setPoseForAI == null)
		{
			setPoseForAI = GetComponent<SetPoseForAI>();
		}
		if (footJoint == null)
		{
			footJoint = GetComponent<ConfigurableJoint>();
		}
	}

	public void OnSimulateStart(BlockBehaviour block)
	{
		if (base.HasParentMachine)
		{
			base.ParentMachine.UnregisterSimulationBlock(block);
		}
	}

	public void SetAiFaction(string faction, string primaryTargetFaction, FactionsController.AttackOnlyEnum attackOnlyTypeOf, FactionsController.DiscriminantEnum discrimination)
	{
		if (isSimulating)
		{
			FactionsController.ChangeFaction(AI, FactionsController.Factions[faction]);
			return;
		}
		AI.factionSystem.factionName = faction;
		AI.factionSystem.primaryTargetName = primaryTargetFaction;
		AI.factionSystem.AttackOnlyTypeOf = attackOnlyTypeOf;
		AI.factionSystem.Discrimination = discrimination;
	}

	public void SetRotationMethode(EntityAI.FocusOn methode)
	{
		AI.looking.Focus = methode;
	}

	public void SetRotationMethode(EntityAI.FocusOn methode, bool rotateRigidbody)
	{
		AI.looking.Focus = methode;
		AI.looking.rotateRigidbody = rotateRigidbody;
	}

	public void SetAIControlLevel(ControlMethod newControlMethod)
	{
		switch (newControlMethod)
		{
		case ControlMethod.Both:
			AI.disposition.AutomaticTargetSystem = true;
			AI.disposition.useBehaviours = true;
			break;
		case ControlMethod.UseTargetingSystem:
			AI.disposition.AutomaticTargetSystem = true;
			AI.disposition.useBehaviours = false;
			break;
		case ControlMethod.UseBehaviours:
			AI.disposition.AutomaticTargetSystem = false;
			AI.disposition.useBehaviours = true;
			break;
		case ControlMethod.None:
			AI.disposition.AutomaticTargetSystem = false;
			AI.disposition.useBehaviours = false;
			break;
		}
	}

	public void SetAIState(EntityAI.EntityState state)
	{
		AI.aiControllerState = state;
	}

	public override void FreezeMe()
	{
		base.FreezeMe();
		isFrozen = true;
	}
}
