using System.Collections;
using UnityEngine;

[AddComponentMenu("Physics/Trigger Set Joint 1")]
public class TriggerSetJoint1 : TriggerSetJointBase
{
	public bool otherMechJoint;

	private Transform myParent;

	private Joint myJoint;

	private Rigidbody myBody;

	private BlockBehaviour block;

	private bool foundMachine;

	private IEnumerator Start()
	{
		block = GetComponentInParent<BlockBehaviour>();
		foundMachine = block.HasParentMachine;
		if (foundMachine && block.isSimulating)
		{
			if (!block.SimPhysics)
			{
				Object.Destroy(this);
			}
			myParent = base.transform.parent;
			myJoint = myParent.GetComponent<Joint>();
			myBody = myParent.GetComponent<Rigidbody>();
			yield return new WaitForFixedUpdate();
			yield return new WaitForFixedUpdate();
			block.CheckJoints();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!foundMachine || !block.SimPhysics)
		{
			return;
		}
		if (!base.gameObject.CompareTag("MechanicalTag") && other.gameObject.CompareTag("MechanicalTag"))
		{
			otherMechJoint = true;
		}
		if (!block.isSimulating)
		{
			return;
		}
		int layer = other.gameObject.layer;
		if ((layer == 12 || layer == 14) && myJoint != null && myJoint.connectedBody == null)
		{
			Transform parent = other.transform.parent;
			if (parent != myParent)
			{
				CheckForDoubleJoints(other);
				block.CheckJoints();
				Object.Destroy(base.gameObject);
			}
		}
	}

	private void CheckForDoubleJoints(Collider obj)
	{
		Rigidbody component = obj.transform.parent.GetComponent<Rigidbody>();
		Joint component2 = component.GetComponent<Joint>();
		bool flag = component2 != null && (component2 is ConfigurableJoint || component2 is HingeJoint);
		if (component2 == null || (flag && component2.connectedBody != myBody))
		{
			myJoint.connectedBody = component;
		}
	}
}
