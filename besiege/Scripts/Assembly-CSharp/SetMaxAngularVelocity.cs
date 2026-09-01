using UnityEngine;

public class SetMaxAngularVelocity : MonoBehaviour
{
	public float MaxAngularVelocity = 1000f;

	public Machine machine;

	private void Start()
	{
		if (object.ReferenceEquals(machine, null))
		{
			machine = GetComponentInParent<Machine>();
		}
		if (!machine || machine.SimPhysics)
		{
			Rigidbody component = GetComponent<Rigidbody>();
			component.maxAngularVelocity = MaxAngularVelocity;
		}
	}
}
