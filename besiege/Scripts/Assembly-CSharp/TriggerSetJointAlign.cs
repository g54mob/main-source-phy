using UnityEngine;

[AddComponentMenu("Physics/Trigger Set Joint Align")]
public class TriggerSetJointAlign : TriggerSetJointBase
{
	public BlockBehaviour block;

	public Rigidbody body;

	public Joint actualJoint;

	public SphereCollider trigger;

	public Transform rotator;

	protected Joint blockJoint;

	protected Collider[] colliders;

	protected virtual Vector3 OverlapPos()
	{
		return base.transform.position;
	}

	protected virtual void Start()
	{
		if (!block.SimPhysics)
		{
			if (block.isSimulating)
			{
				Object.Destroy(base.gameObject);
				if ((bool)block.blockJoint)
				{
					Object.Destroy(block.blockJoint);
				}
			}
			else if (!block.HasParentMachine)
			{
				Object.Destroy(base.gameObject);
			}
		}
		else
		{
			if (!block.isSimulating)
			{
				return;
			}
			float radius = trigger.radius * Mathf.Max(base.transform.lossyScale.x, base.transform.lossyScale.y, base.transform.lossyScale.z);
			colliders = Physics.OverlapSphere(OverlapPos(), radius, AddPiece.CreateLayerMask(new int[2] { 12, 14 }));
			blockJoint = block.blockJoint;
			if (colliders.Length > 0)
			{
				float num = float.MaxValue;
				int num2 = -1;
				for (int i = 0; i < colliders.Length; i++)
				{
					Collider collider = colliders[i];
					float sqrMagnitude = (collider.transform.position - base.transform.position).sqrMagnitude;
					if (ValidTarget(collider) && sqrMagnitude < num)
					{
						num = sqrMagnitude;
						num2 = i;
					}
				}
				if (num2 > -1)
				{
					Connect(colliders[num2]);
					return;
				}
			}
			Object.DestroyImmediate(blockJoint);
			Object.Destroy(base.gameObject);
		}
	}

	protected virtual bool ValidTarget(Collider c)
	{
		return c.gameObject.tag != "ClusterIgnore";
	}

	protected virtual void Connect(Collider c)
	{
		Quaternion rotation = base.transform.rotation;
		rotator.parent = block.transform.parent;
		Vector3 normalized = (base.transform.position - c.transform.position).normalized;
		normalized = AddPiece.GetLocalDirClosestTo(c.transform, normalized);
		Vector3 normalized2 = Vector3.Cross(normalized, base.transform.forward).normalized;
		Vector3 upwards = Vector3.Cross(normalized2, base.transform.forward);
		Vector3 upwards2 = Vector3.Cross(normalized2, normalized);
		rotator.rotation = Quaternion.LookRotation(base.transform.forward, upwards);
		base.transform.parent = rotator;
		rotator.rotation = Quaternion.LookRotation(normalized, upwards2);
		base.transform.parent = block.transform.parent;
		Rigidbody attachedRigidbody = c.attachedRigidbody;
		actualJoint.connectedBody = attachedRigidbody;
		(block as BlockBehaviourIgnoreCols).IgnoreCollision(attachedRigidbody.GetComponentsInChildren<Collider>(false), true);
		body.isKinematic = false;
		body.inertiaTensor = Vector3.one;
		body.maxAngularVelocity = 50f;
		body.angularVelocity = Vector3.zero;
		body.velocity = Vector3.zero;
		base.transform.rotation = rotation;
		blockJoint.connectedBody = body;
		SetCrossReferences(actualJoint);
		Object.Destroy(rotator.gameObject);
		Object.Destroy(trigger);
		Object.Destroy(this);
	}

	protected void SetCrossReferences(Joint j)
	{
		block.CreateSimLists();
		block.iJointTo.Add(blockJoint);
		BlockBehaviour component = j.connectedBody.GetComponent<BlockBehaviour>();
		component.CreateSimLists();
		component.jointsToMe.Add(blockJoint);
	}
}
