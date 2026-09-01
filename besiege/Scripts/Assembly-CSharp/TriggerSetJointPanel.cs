using UnityEngine;

[AddComponentMenu("Physics/Trigger Set Joint (Panel)")]
public class TriggerSetJointPanel : MonoBehaviour
{
	public int layerToCheck = 12;

	public NailRayStart panelCode;

	public ConfigurableJoint myJoint;

	private Transform myParent;

	private BlockBehaviour block;

	private bool foundBlock;

	private void Start()
	{
		block = GetComponentInParent<BlockBehaviour>();
		foundBlock = block != null;
		if (!foundBlock || !block.SimPhysics)
		{
			if (!foundBlock || block.isSimulating)
			{
				Object.Destroy(this);
			}
		}
		else
		{
			myParent = base.transform.parent;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!foundBlock || !block.SimPhysics || !block.isSimulating)
		{
			return;
		}
		Transform parent = other.transform.parent;
		if (parent == myParent)
		{
			return;
		}
		int layer = other.gameObject.layer;
		if (myJoint != null)
		{
			if (layer == layerToCheck && myJoint.connectedBody == null)
			{
				myJoint.connectedBody = parent.GetComponent<Rigidbody>();
				block.CheckJoints();
				Object.Destroy(this);
			}
		}
		else if (layer == layerToCheck)
		{
			panelCode.AddJointy(parent.GetComponent<Rigidbody>());
			block.CheckJoints();
			Object.Destroy(this);
		}
	}
}
