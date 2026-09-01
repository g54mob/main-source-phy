using UnityEngine;

public class DestroyOnTriggerEnter : SimBehaviour
{
	public Transform objToSpawn;

	private void OnTriggerEnter(Collider other)
	{
		if (base.SimPhysics && base.isSimulating && other.gameObject.layer != 29 && (bool)other.attachedRigidbody)
		{
			DestroyObject();
		}
	}

	private void DestroyObject()
	{
		Transform transform = Object.Instantiate(objToSpawn, base.transform.position, Quaternion.identity) as Transform;
		transform.SetParent(base.transform.parent, true);
		base.gameObject.SetActive(false);
	}
}
