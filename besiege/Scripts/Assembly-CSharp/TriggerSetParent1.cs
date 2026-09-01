using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Physics/Trigger Set Joint (Parent) 1")]
public class TriggerSetParent1 : TriggerSetJointBase
{
	[HideInInspector]
	public Machine machine;

	[HideInInspector]
	public bool hasMachine;

	public Transform myParent;

	public List<int> layersToCheck = new List<int> { 12 };

	public bool joinToAllBlocks;

	public bool destroyRigidbodyOnDone;

	public bool destroyObjectOnDone;

	private void OnTriggerEnter(Collider other)
	{
		if (!hasMachine || !machine.isSimulating)
		{
			return;
		}
		if (!joinToAllBlocks)
		{
			if (AcceptedLayer(other.gameObject.layer))
			{
				myParent.parent = other.attachedRigidbody.transform;
				if (destroyObjectOnDone)
				{
					Object.Destroy(base.gameObject);
				}
				else if (destroyRigidbodyOnDone)
				{
					Machine.RemoveBody(base.transform);
					Object.Destroy(this);
				}
				else
				{
					Object.Destroy(this);
				}
			}
		}
		else if ((bool)other.attachedRigidbody && (bool)other.attachedRigidbody.GetComponent<BlockBehaviour>())
		{
			myParent.parent = other.attachedRigidbody.transform;
			Object.Destroy(this);
		}
	}

	private bool AcceptedLayer(int layer)
	{
		foreach (int item in layersToCheck)
		{
			if (item == layer)
			{
				return true;
			}
		}
		return false;
	}
}
