using UnityEngine;

public class ExplodeMultiplier : SimBehaviour, IExplosionEffect
{
	public Rigidbody myRigidbody;

	public float upAmountScaler = 0.1f;

	public float powerScler = 2f;

	protected override void Start()
	{
		base.Start();
		if (myRigidbody == null)
		{
			myRigidbody = GetComponent<Rigidbody>();
		}
	}

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		if (!base.enabled || !base.isSimulating || !base.SimPhysics)
		{
			return false;
		}
		Explodey(power, explosionPos, radius, upPower);
		return true;
	}

	public void Explodey(float powery, Vector3 position, float radiusy, float upAmount)
	{
		if (!(myRigidbody == null))
		{
			myRigidbody.AddExplosionForce(powery * powerScler, position, radiusy, upAmount * upAmountScaler);
		}
	}
}
