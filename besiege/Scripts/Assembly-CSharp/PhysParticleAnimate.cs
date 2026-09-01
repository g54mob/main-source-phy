using UnityEngine;

public class PhysParticleAnimate : SimBehaviour
{
	public Transform pieces;

	public float upForceMin;

	public float upForceMax;

	public float minExplodeForce;

	public float maxExplodeForce;

	public float minTorque;

	public float maxTorque;

	protected override void Start()
	{
		base.Start();
		Explode();
	}

	private void Explode()
	{
		if (base.SimPhysics)
		{
			for (int i = 0; i < pieces.childCount; i++)
			{
				Rigidbody component = pieces.GetChild(i).GetComponent<Rigidbody>();
				component.AddForce(Random.insideUnitSphere * Random.Range(minExplodeForce, maxExplodeForce) + new Vector3(0f, Random.Range(upForceMin, upForceMax), 0f));
				component.AddTorque(Random.insideUnitSphere * Random.Range(minTorque, maxTorque));
			}
		}
	}
}
