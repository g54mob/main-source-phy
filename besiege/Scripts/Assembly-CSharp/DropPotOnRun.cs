using System.Collections;
using UnityEngine;

public class DropPotOnRun : SimBehaviour
{
	public enum WaitUnit
	{
		Frames = 0,
		Seconds = 1
	}

	public GameObject pot;

	public float waitFor = 42f;

	public WaitUnit unit;

	public float throwForce = 12f;

	private bool dropOnce = true;

	private EnemyAISimple simpleAI;

	private EntityAI entityAI;

	protected override void Start()
	{
		base.Start();
		simpleAI = basicInfo as EnemyAISimple;
		if (object.ReferenceEquals(simpleAI, null))
		{
			entityAI = GetComponent<EntityAI>();
		}
		if ((base.isSimulating && !base.SimPhysics) || (object.ReferenceEquals(simpleAI, null) && object.ReferenceEquals(entityAI, null)))
		{
			base.enabled = false;
		}
	}

	private void FixedUpdate()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		if ((bool)simpleAI)
		{
			if (simpleAI.isRunningAway && dropOnce)
			{
				dropOnce = false;
				StartCoroutine(IEDropPot());
			}
		}
		else if (!object.ReferenceEquals(entityAI, null) && (entityAI.disposition.myState == EntityAI.EntityState.Fleeing || entityAI.disposition.myState == EntityAI.EntityState.Fallen || entityAI.isDead) && dropOnce)
		{
			dropOnce = false;
			StartCoroutine(IEDropPot());
		}
	}

	public void Drop()
	{
		pot.transform.SetParent(ReferenceMaster.physicsGoalInstance);
		pot.GetComponent<BreakOnForceNoScaling>().enabled = true;
		if (base.SimPhysics)
		{
			Rigidbody rigidbody = pot.AddComponent<Rigidbody>();
			rigidbody.drag = 1f;
			rigidbody.AddForce((Vector3.up - Vector3.forward * 0.2f) * throwForce, ForceMode.VelocityChange);
		}
		base.enabled = false;
	}

	private IEnumerator IEDropPot()
	{
		yield return new WaitForSeconds(waitFor * ((unit != WaitUnit.Frames) ? 1f : Time.deltaTime));
		if (base.isSimulating && base.SimPhysics)
		{
			NetworkBlock netBlock = base.NetBlock;
			if (netBlock != null)
			{
				netBlock.Event(NetworkEntity.EntityEvent.DropPot);
			}
		}
		Drop();
	}
}
