using UnityEngine;

public class KeepAwake : SimBehaviour
{
	private Rigidbody rb;

	protected override void Start()
	{
		base.Start();
		if (base.isSimulating && base.SimPhysics)
		{
			rb = GetComponent<Rigidbody>();
			if (rb == null)
			{
				base.enabled = false;
			}
		}
	}

	private void Update()
	{
		if (base.SimPhysics && base.isSimulating && rb.IsSleeping())
		{
			rb.WakeUp();
		}
	}
}
