using UnityEngine;

public class CheckJointOnDelete : SimBehaviour
{
	public int layerToCheck = 12;

	public bool hingeJointy;

	public Transform joinedObjects;

	private Transform myParent;

	protected override void Start()
	{
		base.Start();
		myParent = base.transform.parent;
	}

	private void Check()
	{
		if (joinedObjects == null)
		{
			Object.Destroy(myParent.gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!base.isSimulating && other.gameObject.layer == layerToCheck && other.transform.parent != myParent)
		{
			joinedObjects = other.transform.parent;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (!base.isSimulating && other.transform.parent == joinedObjects)
		{
			joinedObjects = null;
		}
	}
}
