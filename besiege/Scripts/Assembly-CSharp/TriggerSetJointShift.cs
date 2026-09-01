using System.Collections;
using UnityEngine;

[AddComponentMenu("Physics/Trigger Set Joint Shift")]
public class TriggerSetJointShift : TriggerSetJointAlign
{
	public bool shift = true;

	public bool useExtraBody;

	protected override bool ValidTarget(Collider c)
	{
		Rigidbody attachedRigidbody = c.attachedRigidbody;
		return attachedRigidbody != body && attachedRigidbody != block.Rigidbody;
	}

	protected override void Connect(Collider c)
	{
		Rigidbody attachedRigidbody = c.attachedRigidbody;
		if (!shift)
		{
			blockJoint.connectedBody = attachedRigidbody;
			SetCrossReferences(blockJoint);
			StartCoroutine(DisableTrigger(true));
		}
		else if (!useExtraBody)
		{
			blockJoint.connectedBody = attachedRigidbody;
			blockJoint.autoConfigureConnectedAnchor = false;
			blockJoint.anchor += Vector3.forward;
			block.Rigidbody.angularVelocity = Vector3.zero;
			block.Rigidbody.velocity = Vector3.zero;
			SetCrossReferences(blockJoint);
			StartCoroutine(DisableTrigger(true));
		}
		else
		{
			blockJoint.connectedBody = body;
			base.transform.parent = block.transform.parent;
			base.transform.position -= base.transform.forward;
			actualJoint.connectedBody = attachedRigidbody;
			body.isKinematic = false;
			body.inertiaTensor = Vector3.one * 0.1f;
			body.maxAngularVelocity = 50f;
			body.angularVelocity = Vector3.zero;
			body.velocity = Vector3.zero;
			SetCrossReferences(actualJoint);
			StartCoroutine(DisableTrigger(false));
		}
	}

	public IEnumerator DisableTrigger(bool all)
	{
		while (!block.ParentMachine.isReady)
		{
			yield return new WaitForFixedUpdate();
		}
		block.Rigidbody.angularVelocity = Vector3.zero;
		block.Rigidbody.velocity = Vector3.zero;
		if (all)
		{
			Object.Destroy(base.gameObject);
			yield break;
		}
		body.centerOfMass = base.transform.InverseTransformPoint(actualJoint.connectedBody.worldCenterOfMass);
		Object.Destroy(trigger);
		Object.Destroy(this);
	}
}
