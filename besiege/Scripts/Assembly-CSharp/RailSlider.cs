using UnityEngine;

public class RailSlider : MonoBehaviour
{
	private Machine machine;

	private void Start()
	{
		machine = GetComponentInParent<Machine>();
	}

	private void FixedUpdate()
	{
		if (machine.SimPhysics)
		{
			Vector3 direction = new Vector3(0f, 0f, base.transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity).z);
			GetComponent<Rigidbody>().velocity = base.transform.TransformDirection(direction);
		}
	}
}
