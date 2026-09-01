using UnityEngine;

public class HotAirBalloon : BreakBase
{
	public TriggerEnterHook baloonTrigger;

	public CollisionHook collisionHook;

	public CollisionHook collisionHook2;

	public Rigidbody[] bodies;

	public Transform PopEffect;

	public float popImpactThreshold;

	public GameObject balloonPopObj;

	public GameObject[] objectsToDisable;

	public BleedOnJointBreak bleedOnJointB;

	public KillingHandler killHandle;

	public Joint aiJoint;

	private bool popped;

	public override Vector3 Center()
	{
		if (base.SimPhysics)
		{
			return bodies[0].worldCenterOfMass;
		}
		return base.Center();
	}

	protected override void Awake()
	{
		base.Awake();
		if (base.isSimulating)
		{
			baloonTrigger.TriggerEntered += TriggerEnter;
			collisionHook.CollisionHappend += CollisionEnter;
			if ((bool)collisionHook2)
			{
				collisionHook2.CollisionHappend += CollisionEnter;
			}
		}
	}

	protected override void Start()
	{
		base.Start();
		if (base.isSimulating)
		{
			for (int i = 0; i < bodies.Length; i++)
			{
				bodies[i].isKinematic = false;
			}
		}
	}

	protected override void OnDestroy()
	{
		baloonTrigger.TriggerEntered -= TriggerEnter;
		collisionHook.CollisionHappend -= CollisionEnter;
		if ((bool)collisionHook2)
		{
			collisionHook2.CollisionHappend -= CollisionEnter;
		}
		base.OnDestroy();
	}

	private void Pop()
	{
		popped = true;
		bodies[1].AddTorque(Random.insideUnitSphere * 150f);
		Object.Destroy(aiJoint);
		bodies[2].AddTorque(Random.insideUnitSphere * 250f);
		bodies[2].AddForce(Vector3.up * 25f);
		if ((bool)bleedOnJointB)
		{
			bleedOnJointB.KillMe(false);
		}
		else if ((bool)killHandle)
		{
			killHandle.KillUnit(false, InjuryType.Blunt);
		}
		for (int i = 0; i < objectsToDisable.Length; i++)
		{
			objectsToDisable[i].SetActive(false);
		}
		Object.Instantiate(balloonPopObj, PopEffect.position, PopEffect.rotation);
		AddToPercentageBar();
		if (OnBreakTrigger != null)
		{
			OnBreakTrigger(this);
		}
	}

	private void TriggerEnter(Collider other)
	{
		if (!StatMaster.levelSimulating || popped || other == null)
		{
			return;
		}
		GameObject gameObject = other.gameObject;
		if (gameObject != null)
		{
			int layer = gameObject.layer;
			if (layer != 2 && layer != 27 && other.attachedRigidbody != null)
			{
				Pop();
			}
		}
	}

	private void FireKill()
	{
		Pop();
	}

	private void CollisionEnter(Collision other)
	{
		if (popped || other.rigidbody == null)
		{
			return;
		}
		if (other.relativeVelocity.sqrMagnitude > popImpactThreshold)
		{
			Pop();
			return;
		}
		BlockBehaviour component = other.rigidbody.GetComponent<BlockBehaviour>();
		if (!object.ReferenceEquals(component, null) && component.Prefab.hasDamageType && component.Prefab.myDamageType == DamageType.Sharp)
		{
			Pop();
		}
	}

	private void AddToPercentageBar()
	{
		OnBreak();
		if (base.gameObject.CompareTag("ObjectiveObj"))
		{
			WinCondition.currentObjsCompleted++;
		}
	}
}
