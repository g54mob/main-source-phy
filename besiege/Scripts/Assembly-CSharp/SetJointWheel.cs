using UnityEngine;

[AddComponentMenu("Physics/Set Joint (New Wheel)")]
public class SetJointWheel : SetJointNoCollider
{
	public Collider addPoint;

	protected override void Start()
	{
		if (!block || !block.ParentMachine)
		{
			Object.Destroy(base.gameObject);
		}
		else if (block.ParentMachine.isSimulating)
		{
			addPoint.enabled = false;
			base.Start();
		}
	}

	protected override bool CheckCollider(Collider other, out Rigidbody b)
	{
		b = other.attachedRigidbody;
		if (b == null || b.transform.parent.name == "Building Machine")
		{
			return false;
		}
		if (other.gameObject.layer == 22 && other.transform.parent.name == "Brace")
		{
			b = other.transform.parent.GetComponent<Rigidbody>();
			if (b.transform.parent.name != "Building Machine")
			{
				return true;
			}
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
}
