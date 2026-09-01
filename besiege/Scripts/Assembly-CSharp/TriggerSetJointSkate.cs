using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Physics/Trigger Set Joint (SkateboardWheel)")]
public class TriggerSetJointSkate : TriggerSetJointBase
{
	public SkateboardWheel skateBlock;

	public ConfigurableJoint myJoint;

	public bool canDestroyJoints = true;

	public Rigidbody myBody;

	private bool isDestroyed;

	private bool isInitialized;

	private List<Collider> colliders = new List<Collider>();

	private IEnumerator Start()
	{
		isInitialized = true;
		if (!skateBlock.SimPhysics)
		{
			if (skateBlock.isSimulating)
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
		if (skateBlock.isSimulating && canDestroyJoints)
		{
			yield return new WaitForFixedUpdate();
			yield return new WaitForFixedUpdate();
			yield return new WaitForFixedUpdate();
			skateBlock.CheckJoints();
			yield return new WaitForSeconds(1f);
			if (!isDestroyed)
			{
				DestroyComponents();
			}
		}
	}

	private void DestroyComponents()
	{
		Object.Destroy(base.gameObject);
		isDestroyed = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!isInitialized)
		{
			colliders.Add(other);
		}
		else
		{
			if (!skateBlock.SimPhysics || !skateBlock.isSimulating || other.transform.IsChildOf(skateBlock.transform))
			{
				return;
			}
			Rigidbody attachedRigidbody = other.attachedRigidbody;
			if (attachedRigidbody == null || (StatMaster.isMP && attachedRigidbody.transform.parent.name == "Building Machine"))
			{
				return;
			}
			int layer = other.gameObject.layer;
			BlockBehaviour blockBehaviour = other.GetComponentInParent<BlockBehaviour>();
			if (blockBehaviour == null)
			{
				SimBehaviour componentInChildren = other.GetComponentInChildren<SimBehaviour>();
				if (componentInChildren != null)
				{
					blockBehaviour = componentInChildren.basicInfo as BlockBehaviour;
				}
			}
			if (blockBehaviour != null)
			{
				BlockType type = blockBehaviour.Prefab.Type;
				bool flag = false;
				switch (type)
				{
				case BlockType.Brace:
				case BlockType.Spring:
				case BlockType.RopeWinch:
					flag = true;
					break;
				case BlockType.Balloon:
				case BlockType.BuildSurface:
				case BlockType.BigBarrel:
				case BlockType.SkateWheel:
					flag = true;
					break;
				default:
				{
					TriggerSetJointBase component = other.GetComponent<TriggerSetJointBase>();
					if (component != null && component.isDynamicLink)
					{
						flag = true;
					}
					break;
				}
				}
				if (flag)
				{
					skateBlock.SplitWheelBody();
					DestroyComponents();
					return;
				}
			}
			switch (layer)
			{
			case 14:
				if (!(myJoint.connectedBody == null))
				{
					break;
				}
				goto case 12;
			case 12:
			{
				if (skateBlock.dualBody)
				{
					break;
				}
				myJoint.connectedBody = attachedRigidbody;
				CreateJointReferences(attachedRigidbody);
				DestroyComponents();
				List<Joint> list = new List<Joint>(skateBlock.jointsToMe);
				for (int i = 0; i < list.Count; i++)
				{
					Joint joint = list[i];
					if (joint.gameObject == attachedRigidbody.gameObject)
					{
						Object.Destroy(joint);
						skateBlock.jointsToMe.Remove(joint);
					}
				}
				break;
			}
			}
		}
	}

	private void CreateJointReferences(Rigidbody otherBody)
	{
		skateBlock.CreateSimLists();
		skateBlock.iJointTo.Add(myJoint);
		BlockBehaviour blockBehaviour = ((!(otherBody != null)) ? null : otherBody.GetComponent<BlockBehaviour>());
		if ((bool)blockBehaviour)
		{
			skateBlock.SimReparent(blockBehaviour, blockBehaviour.BuildIndex);
			blockBehaviour.CreateSimLists();
			blockBehaviour.jointsToMe.Add(myJoint);
		}
	}
}
