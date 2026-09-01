using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Physics/Trigger Set Joint 2")]
public class TriggerSetJoint2 : TriggerSetJointBase
{
	public ConfigurableJoint jointToCopy;

	public Rigidbody parentBody;

	private ConfigurableJoint jointy;

	private bool hasJoint;

	private bool dontJoin;

	private Collider objToJoinTo;

	private Transform parentToJoinTo;

	private Machine machine;

	private bool foundMachine;

	private bool handleTrigger;

	[NonSerialized]
	private bool isInitialized;

	[NonSerialized]
	private List<Collider> colliders = new List<Collider>();

	private IEnumerator Start()
	{
		isInitialized = true;
		handleTrigger = true;
		machine = GetComponentInParent<Machine>();
		foundMachine = machine != null;
		if (!foundMachine || !machine.SimPhysics)
		{
			if (!foundMachine || machine.isSimulating)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			handleTrigger = false;
			yield break;
		}
		if (!machine.isSimulating)
		{
			handleTrigger = false;
			yield break;
		}
		yield return new WaitForFixedUpdate();
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
		if (!dontJoin)
		{
			if (objToJoinTo != null && objToJoinTo.attachedRigidbody != null)
			{
				Rigidbody r = objToJoinTo.attachedRigidbody;
				AddJointy(r);
			}
			else if (parentToJoinTo != null)
			{
				Rigidbody r = parentToJoinTo.GetComponent<Rigidbody>();
				if (r != null)
				{
					AddJointy(r);
				}
			}
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!isInitialized)
		{
			colliders.Add(other);
		}
		else
		{
			if (!handleTrigger || hasJoint || (StatMaster.isMP && other.attachedRigidbody.transform.parent.name == "Building Machine"))
			{
				return;
			}
			int layer = other.gameObject.layer;
			Rigidbody attachedRigidbody = other.attachedRigidbody;
			if (layer == 12 || layer == 14)
			{
				if (!(attachedRigidbody != parentBody))
				{
					return;
				}
				if (isDynamicLink || !objToJoinTo || !other.CompareTag("OnlyMechanicalJoints"))
				{
					objToJoinTo = other;
					parentToJoinTo = attachedRigidbody.transform;
				}
			}
			if (object.ReferenceEquals(attachedRigidbody, null) || layer != 22)
			{
				return;
			}
			TriggerSetJointBase component = attachedRigidbody.GetComponent<TriggerSetJointBase>();
			if (component == null)
			{
				if (attachedRigidbody.tag == "MechanicalTag")
				{
					dontJoin = true;
				}
			}
			else if (component is TriggerSetJoint || component is TriggerSetJointSkate || (component is TriggerSetJointAlign && component.isDynamicLink))
			{
				dontJoin = true;
			}
		}
	}

	private void AddJointy(Rigidbody rigid)
	{
		if (parentBody == null)
		{
			Debug.LogWarning("TriggerSetJoint2::AddJointy: MyParent is null on " + Machine.GetObjectPath(base.gameObject));
		}
		else
		{
			if (!(rigid != null))
			{
				return;
			}
			BlockBehaviour component = rigid.GetComponent<BlockBehaviour>();
			bool flag = component != null;
			if (flag && component is BuildSurface)
			{
				SphereCollider component2 = GetComponent<SphereCollider>();
				float num = component2.radius * Mathf.Max(base.transform.lossyScale.x, base.transform.lossyScale.y, base.transform.lossyScale.z);
				num *= num;
				BuildSurface buildSurface = component as BuildSurface;
				for (int i = 0; i < buildSurface.Joints.Length; i++)
				{
					if (buildSurface.Joints[i] != null && buildSurface.Joints[i].connectedBody == parentBody && (base.transform.localPosition - buildSurface.Joints[i].connectedAnchor).sqrMagnitude < num)
					{
						return;
					}
				}
			}
			jointy = parentBody.gameObject.AddComponent<ConfigurableJoint>();
			jointy.CopyJoint(jointToCopy);
			jointy.connectedBody = rigid;
			if (flag)
			{
				component.CreateSimLists();
				component.jointsToMe.Add(jointy);
			}
			component = parentBody.GetComponent<BlockBehaviour>();
			if (component != null)
			{
				component.CreateSimLists();
				component.iJointTo.Add(jointy);
			}
		}
	}
}
