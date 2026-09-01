using UnityEngine;

public class RandomForceOnStart : SimBehaviour
{
	public float forceToAdd;

	public float minimumForce;

	public float torqueToAdd = 500f;

	protected override void Start()
	{
		base.Start();
		if (base.SimPhysics)
		{
			Rigidbody component = GetComponent<Rigidbody>();
			if ((bool)component && base.isSimulating)
			{
				Vector3 force = ((minimumForce != 0f) ? (Random.onUnitSphere * Random.Range(minimumForce, forceToAdd)) : (Random.insideUnitSphere * forceToAdd));
				component.AddForce(force);
				component.AddTorque(Random.insideUnitSphere * torqueToAdd);
			}
		}
	}
}
