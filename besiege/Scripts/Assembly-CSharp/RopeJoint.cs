using UnityEngine;

public class RopeJoint : SetJointBreakForce
{
	[SerializeField]
	protected ConfigurableJoint configurableJoint;

	protected override void Start()
	{
		base.Start();
		if (basicInfo.isSimulating && !basicInfo.SimPhysics)
		{
			DestroySelf();
		}
	}

	private void OnJointBreak(float breakForce)
	{
		if (basicInfo.isSimulating && basicInfo.SimPhysics && !basicInfo.ParentMachine.UnbreakableMode)
		{
			FragmentVisualController.EmitJointBreakMarker(base.transform.position);
			DestroySelf();
		}
	}

	private void Update()
	{
		if (basicInfo.isSimulating && configurableJoint == null)
		{
			DestroySelf();
		}
	}

	private void DestroySelf()
	{
		if (basicInfo.SimPhysics)
		{
			ISnapable snapable = basicInfo as ISnapable;
			snapable.BreakJoint(configurableJoint);
			snapable.Snap();
		}
		base.enabled = false;
		Object.Destroy(this);
	}
}
