using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/BlockBehaviour (Ignore Colliders)")]
public class BlockBehaviourIgnoreCols : BlockBehaviour
{
	protected HashSet<Collider> ignored = new HashSet<Collider>();

	protected bool broken;

	public Vector3 inertiaScale = Vector3.one;

	public override void StartPhysics(bool isKinematic)
	{
		base.StartPhysics(isKinematic);
		if (!noRigidbody)
		{
			Rigidbody.inertiaTensor = Vector3.Scale(Rigidbody.inertiaTensor, inertiaScale);
		}
	}

	protected virtual void OnJointBreak()
	{
		if (!broken)
		{
			FragmentVisualController.EmitJointBreakMarker(base.transform.position);
			IgnoreCollision(ignored, false);
			broken = true;
		}
	}

	public void IgnoreCollision(IEnumerable<Collider> others, bool toggle)
	{
		List<Collider> childColliders = myBounds.childColliders;
		foreach (Collider item in childColliders)
		{
			if (!item)
			{
				continue;
			}
			foreach (Collider other in others)
			{
				if (!other)
				{
					continue;
				}
				switch (other.gameObject.layer)
				{
				case 0:
				case 12:
				case 14:
				case 15:
				case 17:
				case 18:
				case 25:
				case 26:
					if (other.tag != "PendingDestructionSim")
					{
						if (toggle)
						{
							ignored.Add(other);
						}
						Physics.IgnoreCollision(item, other, toggle);
					}
					break;
				}
			}
		}
		if (!toggle)
		{
			ignored.Clear();
		}
	}
}
