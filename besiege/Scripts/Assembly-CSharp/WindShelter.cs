using UnityEngine;

[AddComponentMenu("Physics/Wind/Shelter")]
public class WindShelter : MonoBehaviour
{
	public Collider col;

	public float shelterAmount;

	public bool doNotReset;

	private void Update()
	{
		if (StatMaster.levelSimulating && !col.enabled)
		{
			col.enabled = true;
		}
		else if (!StatMaster.levelSimulating && col.enabled)
		{
			col.enabled = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if ((bool)other.attachedRigidbody)
		{
			BlockBehaviour componentInParent = other.attachedRigidbody.GetComponentInParent<BlockBehaviour>();
			if (componentInParent != null)
			{
				componentInParent.ShelterAmount = shelterAmount;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (!doNotReset && (bool)other.attachedRigidbody)
		{
			BlockBehaviour componentInParent = other.attachedRigidbody.GetComponentInParent<BlockBehaviour>();
			if (componentInParent != null)
			{
				componentInParent.ShelterAmount = 0f;
			}
		}
	}
}
