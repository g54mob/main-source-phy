using UnityEngine;

[AddComponentMenu("Physics/Set Joint (No Collider)")]
public class SetJointNoCollider : TriggerSetJointBase
{
	public ConfigurableJoint jointToCopy;

	public BlockBehaviour block;

	public float radius = 0.5f;

	protected bool stopJoining;

	protected virtual void Start()
	{
		if (!block || !block.ParentMachine)
		{
			Debug.LogWarning("SetJoint(NoCollider)::AddJoint: Block is null on " + Machine.GetObjectPath(base.gameObject));
			Object.Destroy(base.gameObject);
		}
		else
		{
			if (!block.ParentMachine.isSimulating)
			{
				return;
			}
			if (jointToCopy == null)
			{
				Debug.LogError("SetJoint(NoCollider)::AddJoint: Joint is null on " + Machine.GetObjectPath(base.gameObject));
				return;
			}
			jointToCopy.transform.parent = ReferenceMaster.physicsGoalInstance;
			bool flag = jointToCopy.gameObject != block.gameObject;
			Collider[] array = Physics.OverlapSphere(base.transform.position, radius, AddPiece.CreateLayerMask(new int[3] { 12, 14, 22 }));
			Rigidbody rigidbody = null;
			bool flag2 = false;
			if (array.Length > 0)
			{
				foreach (Collider other in array)
				{
					Rigidbody b;
					if (CheckCollider(other, out b))
					{
						if (stopJoining)
						{
							break;
						}
						if (rigidbody == null)
						{
							rigidbody = b;
						}
						flag2 = true;
					}
				}
			}
			if (!stopJoining && flag2)
			{
				AddJoint(rigidbody);
			}
			else if (!flag)
			{
				Object.Destroy(jointToCopy);
			}
			if (flag)
			{
				Object.Destroy(jointToCopy.gameObject);
			}
			Object.Destroy(base.gameObject);
		}
	}

	protected virtual bool CheckCollider(Collider other, out Rigidbody b)
	{
		b = other.attachedRigidbody;
		if (b == null || (StatMaster.isMP && b.transform.parent.name == "Building Machine"))
		{
			return false;
		}
		if (!isDynamicLink && other.CompareTag("OnlyMechanicalJoints"))
		{
			return false;
		}
		if (b != block.Rigidbody)
		{
			if (other.gameObject.layer == 22)
			{
				TriggerSetJointBase component = other.gameObject.GetComponent<TriggerSetJointBase>();
				if ((bool)component)
				{
					if (isDynamicLink && !component.isDynamicLink)
					{
						return false;
					}
					stopJoining = true;
					return true;
				}
			}
			else
			{
				Joint[] components = b.GetComponents<Joint>();
				for (int i = 0; i < components.Length; i++)
				{
					if (components[i].connectedBody == this)
					{
						return false;
					}
				}
			}
			return true;
		}
		return false;
	}

	private void AddJoint(Rigidbody rigid)
	{
		ConfigurableJoint configurableJoint;
		if (jointToCopy.gameObject != block.gameObject)
		{
			configurableJoint = block.Rigidbody.gameObject.AddComponent<ConfigurableJoint>();
			configurableJoint.CopyJoint(jointToCopy);
		}
		else
		{
			configurableJoint = jointToCopy;
		}
		configurableJoint.connectedBody = rigid;
		BlockBehaviour component = rigid.GetComponent<BlockBehaviour>();
		if (component != null)
		{
			component.CreateSimLists();
			component.jointsToMe.Add(configurableJoint);
		}
		block.CreateSimLists();
		block.iJointTo.Add(configurableJoint);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = (Color.yellow + Color.green) / 2f;
		Gizmos.DrawWireSphere(base.transform.position, radius);
	}
}
