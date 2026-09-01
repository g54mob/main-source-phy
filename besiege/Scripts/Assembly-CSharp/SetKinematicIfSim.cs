using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Physics/SetKinematicIfSim")]
public class SetKinematicIfSim : SimBehaviour
{
	public List<Rigidbody> rigidbodies = new List<Rigidbody>();

	public bool useChildren;

	public bool denest;

	public bool interpolate;

	protected override void Start()
	{
		base.Start();
		if (base.isSimulating && (!StatMaster.isMP || !StatMaster.isClient || StatMaster.isLocalSim))
		{
			if (useChildren)
			{
				SetChildren();
			}
			SetKinematic(false, denest);
		}
	}

	public void SetKinematic(bool kinematic, bool denest)
	{
		for (int i = 0; i < rigidbodies.Count; i++)
		{
			Rigidbody rigidbody = rigidbodies[i];
			if (!rigidbody)
			{
				continue;
			}
			if (denest)
			{
				if (base.transform.position == Vector3.zero)
				{
					rigidbody.transform.parent = base.transform;
				}
				else
				{
					rigidbody.transform.parent = ReferenceMaster.physicsGoalInstance;
				}
			}
			rigidbody.isKinematic = kinematic;
			if (!kinematic && interpolate)
			{
				rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
			}
		}
	}

	private void SetChildren()
	{
		foreach (Object item in base.transform)
		{
			Transform transform = item as Transform;
			if (!(transform != null))
			{
				continue;
			}
			Rigidbody component = transform.gameObject.GetComponent<Rigidbody>();
			if (!(component != null))
			{
				continue;
			}
			if (denest)
			{
				if (base.transform.position == Vector3.zero || StatMaster.isMP)
				{
					component.transform.parent = base.transform;
				}
				else
				{
					component.transform.parent = ReferenceMaster.physicsGoalInstance;
				}
			}
			component.isKinematic = false;
		}
	}
}
