using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Physics/Trigger Set Joint (Brace)")]
public class TriggerSetJointBrace : TriggerSetJointBase
{
	public BraceCode braceCode;

	public ConfigurableJoint myJoint;

	public bool canDestroyJoints = true;

	[SerializeField]
	protected Rigidbody myBody;

	[SerializeField]
	protected Collider myCollider;

	private bool isDestroyed;

	private bool isInitialized;

	private List<Collider> colliders = new List<Collider>();

	private IEnumerator Start()
	{
		isInitialized = true;
		if (!braceCode.SimPhysics)
		{
			if (braceCode.isSimulating)
			{
				DestroyComponents();
			}
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
		if (braceCode.isSimulating && canDestroyJoints)
		{
			yield return new WaitForFixedUpdate();
			yield return new WaitForFixedUpdate();
			yield return new WaitForFixedUpdate();
			braceCode.CheckJoints();
			yield return new WaitForSeconds(1f);
			if (!isDestroyed)
			{
				DestroyComponents();
			}
		}
	}

	private void DestroyComponents()
	{
		Object.Destroy(this);
		isDestroyed = true;
		if (myCollider.isTrigger)
		{
			Object.Destroy(myCollider);
			Object.Destroy(myBody);
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
			if (!braceCode.SimPhysics || !braceCode.isSimulating)
			{
				return;
			}
			Rigidbody attachedRigidbody = other.attachedRigidbody;
			if (!braceCode.CanCreateJoint(attachedRigidbody, base.transform.localPosition) || (StatMaster.isMP && other.attachedRigidbody.transform.parent.name == "Building Machine"))
			{
				return;
			}
			int layer = other.gameObject.layer;
			if (myJoint != null)
			{
				switch (layer)
				{
				case 14:
					if (!(myJoint.connectedBody == null))
					{
						break;
					}
					goto case 12;
				case 12:
					myJoint.connectedBody = attachedRigidbody;
					braceCode.CreateJointReferences(myJoint);
					DestroyComponents();
					break;
				}
			}
			else if (layer == 12 || layer == 14)
			{
				Rigidbody rigidbody = attachedRigidbody;
				if (rigidbody != null)
				{
					braceCode.AddJointy(rigidbody);
					DestroyComponents();
				}
			}
		}
	}
}
