using UnityEngine;

[AddComponentMenu("Water/Objects/Water Buoyancy Zone")]
public class WaterBuoyancyZone : MonoBehaviour
{
	public Vector3 windAmount;

	public float waterHeight;

	public float dragToAdd = 4f;

	private float depth;

	private void OnTriggerStay(Collider other)
	{
		if ((bool)other.attachedRigidbody)
		{
			depth = other.attachedRigidbody.transform.position.y - waterHeight;
			other.attachedRigidbody.AddForce(windAmount * depth);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if ((bool)other.attachedRigidbody)
		{
			other.attachedRigidbody.drag = dragToAdd;
			AxialDrag component = other.GetComponent<AxialDrag>();
			if (component != null)
			{
				Vector3 axisDrag = component.AxisDrag;
				component.AxisDrag = new Vector3(axisDrag.x, 0.08f, axisDrag.z);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if ((bool)other.attachedRigidbody)
		{
			other.attachedRigidbody.drag = 0f;
			AxialDrag component = other.GetComponent<AxialDrag>();
			if (component != null)
			{
				Vector3 axisDrag = component.AxisDrag;
				component.AxisDrag = new Vector3(axisDrag.x, 0.02f, axisDrag.z);
			}
		}
	}
}
