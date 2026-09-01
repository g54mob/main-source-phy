using System.Collections;
using UnityEngine;

public class SetColliderIterationCount : SimBehaviour
{
	public int iterationCount = 30;

	public int velocityIterationCount = 1;

	protected override void Start()
	{
		base.Start();
		if (base.isSimulating && base.SimPhysics)
		{
			StartCoroutine(SetIterations());
		}
	}

	private IEnumerator SetIterations()
	{
		yield return new WaitForFixedUpdate();
		Rigidbody rb = GetComponent<Rigidbody>();
		if (!(rb == null))
		{
			rb.solverIterations = iterationCount;
			if (base.isSimulating && OptionsMaster.BesiegeConfig.MorePrecisePhysics)
			{
				rb.solverVelocityIterations = velocityIterationCount;
			}
		}
	}
}
