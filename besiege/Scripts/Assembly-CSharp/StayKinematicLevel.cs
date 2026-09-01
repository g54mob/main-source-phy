using UnityEngine;

public class StayKinematicLevel : MonoBehaviour
{
	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (StatMaster.levelSimulating && (bool)rb && !rb.isKinematic)
		{
			rb.isKinematic = true;
		}
	}
}
