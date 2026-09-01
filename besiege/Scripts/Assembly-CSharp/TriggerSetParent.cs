using UnityEngine;

[AddComponentMenu("Physics/Trigger Set Joint (Parent)")]
public class TriggerSetParent : MonoBehaviour
{
	public int layerToCheck = 12;

	public int layerToCheck2 = 14;

	public Transform myParent;

	public bool joinToAllBlocks;

	public bool destroyRigidbodyOnDone;

	private Machine machine;

	private void Start()
	{
		myParent = base.transform.parent;
		machine = GetComponentInParent<Machine>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!machine.isSimulating)
		{
			return;
		}
		if (!joinToAllBlocks)
		{
			if (other.gameObject.layer == layerToCheck || other.gameObject.layer == layerToCheck2)
			{
				myParent.parent = other.transform.parent;
				if (destroyRigidbodyOnDone)
				{
					Object.DestroyImmediate(base.transform.GetComponent<Rigidbody>());
				}
				Object.Destroy(this);
			}
		}
		else if ((bool)other.attachedRigidbody && (bool)other.attachedRigidbody.GetComponent<BlockBehaviour>())
		{
			myParent.parent = other.transform.parent;
			Object.Destroy(this);
		}
	}
}
