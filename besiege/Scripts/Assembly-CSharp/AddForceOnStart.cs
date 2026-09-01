using UnityEngine;

public class AddForceOnStart : SimBehaviour
{
	public float forceToAdd;

	public Rigidbody myRigidbody;

	private Transform target;

	protected override void Start()
	{
		base.Start();
		if (base.isSimulating)
		{
			myRigidbody.isKinematic = false;
			myRigidbody.WakeUp();
			target = Machine.Active().GetRandomBlock().transform;
			myRigidbody.AddForce((myRigidbody.position - target.position).normalized * forceToAdd);
		}
	}
}
