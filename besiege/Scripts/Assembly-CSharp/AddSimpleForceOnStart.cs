using UnityEngine;

public class AddSimpleForceOnStart : SimBehaviour
{
	public float Speed = 500f;

	protected override void Start()
	{
		base.Start();
		GetComponent<Rigidbody>().AddForce(base.transform.forward * Speed);
	}
}
