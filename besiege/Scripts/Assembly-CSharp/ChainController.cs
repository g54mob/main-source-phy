using System.Collections;
using UnityEngine;

public class ChainController : SimBehaviour
{
	public Rigidbody[] segments;

	public float jointForce = 800f;

	public float dampenAmount = 100f;

	public float distanceMgntd;

	public float restingSpringPower = 0.02f;

	public float velocityDamper = 1.2f;

	public float velocityClamp = 10000f;

	private float forceMultiplier = 20f;

	private float startMgntd;

	private float maxMgntd;

	protected override void Start()
	{
		base.Start();
		startMgntd = (segments[0].position - segments[1].position).magnitude;
		maxMgntd = startMgntd + 0.05f;
	}

	private void FixedUpdate()
	{
		if (base.isSimulating)
		{
			for (int i = 1; i < segments.Length; i++)
			{
				Rigidbody rigidbody = segments[i];
				Rigidbody rigidbody2 = segments[i - 1];
				distanceMgntd = (rigidbody.position - rigidbody2.position).magnitude;
				SetSegmentForces(rigidbody, rigidbody2);
			}
		}
	}

	private void SetSegmentForces(Rigidbody r1, Rigidbody r2)
	{
		if (!(distanceMgntd <= maxMgntd))
		{
			ContractNormalised(r1, r2, (distanceMgntd - maxMgntd) * restingSpringPower * forceMultiplier);
			StartCoroutine(Dampen(r1, r2, (distanceMgntd - maxMgntd) * dampenAmount * forceMultiplier));
		}
	}

	private void ContractNormalised(Rigidbody r1, Rigidbody r2, float scaler)
	{
		scaler = Mathf.Clamp(scaler, 0f - velocityClamp, velocityClamp);
		float num = jointForce * scaler;
		r1.AddForce((r1.position - r2.position).normalized * num, ForceMode.Acceleration);
		r2.AddForce((r2.position - r1.position).normalized * num, ForceMode.Acceleration);
	}

	private IEnumerator Dampen(Rigidbody r1, Rigidbody r2, float amount)
	{
		yield return new WaitForFixedUpdate();
		ContractNormalised(r1, r2, amount);
	}
}
