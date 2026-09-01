using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Physics/Trigger Set Joint (Spring)")]
public class TriggerSetJointSpring : TriggerSetJointBase
{
	public bool hingeJointy;

	[SerializeField]
	protected BlockBehaviour block;

	[SerializeField]
	protected Rigidbody myBody;

	[SerializeField]
	protected Collider myCollider;

	[SerializeField]
	protected Joint myJoint;

	private bool isDestroyed;

	public bool makePhysicalOnFail;

	[NonSerialized]
	private bool isInitialized;

	[NonSerialized]
	private List<Collider> colliders = new List<Collider>();

	private IEnumerator Start()
	{
		isInitialized = true;
		if (!block.isSimulating)
		{
			yield break;
		}
		if (!block.SimPhysics)
		{
			DestroyComponents();
			yield break;
		}
		if (colliders.Count > 0)
		{
			for (int i = 0; i < colliders.Count; i++)
			{
				Collider c = colliders[i];
				if (c != null)
				{
					OnTriggerEnter(c);
				}
			}
		}
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		block.CheckJoints();
		if (myBody != null)
		{
			myBody.isKinematic = false;
		}
		if (!isDestroyed)
		{
			DestroyComponents();
		}
	}

	private void DestroyComponents()
	{
		UnityEngine.Object.Destroy(this);
		isDestroyed = true;
		if (makePhysicalOnFail && myJoint.connectedBody == null)
		{
			myCollider.isTrigger = false;
			myBody.inertiaTensor = Vector3.one;
			myCollider.gameObject.layer = 25;
		}
		else if (myCollider.isTrigger)
		{
			UnityEngine.Object.Destroy(myCollider);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!isInitialized)
		{
			colliders.Add(other);
		}
		else
		{
			if (!block.SimPhysics || !block.isSimulating)
			{
				return;
			}
			int layer = other.gameObject.layer;
			if (layer != 12 && layer != 14)
			{
				return;
			}
			Rigidbody attachedRigidbody = other.attachedRigidbody;
			if (!(attachedRigidbody == null) && !(attachedRigidbody == block.Rigidbody) && (!StatMaster.isMP || !(attachedRigidbody.transform.parent.name == "Building Machine")) && myJoint != null && myJoint.connectedBody == null)
			{
				myJoint.connectedBody = attachedRigidbody;
				CreateJointReferences();
				if (myBody != null)
				{
					myBody.isKinematic = false;
				}
				DestroyComponents();
			}
		}
	}

	private void CreateJointReferences()
	{
		block.CreateSimLists();
		block.iJointTo.Add(myJoint);
		BlockBehaviour blockBehaviour = ((!(myJoint.connectedBody != null)) ? null : myJoint.connectedBody.GetComponent<BlockBehaviour>());
		if ((bool)blockBehaviour)
		{
			blockBehaviour.CreateSimLists();
			blockBehaviour.jointsToMe.Add(myJoint);
		}
	}
}
