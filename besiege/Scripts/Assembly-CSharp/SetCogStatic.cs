using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/SetCogStatic")]
public class SetCogStatic : BlockBehaviour
{
	public JointLimits hingeLimit;

	private MToggle freezeToggle;

	private ConfigurableJoint hinge;

	protected override void Awake()
	{
		base.Awake();
		if (!isSimulating || SimPhysics)
		{
			freezeToggle = AddToggle(2500, "freeze", false);
		}
	}

	protected override void Start()
	{
		base.Start();
		if (SimPhysics && isSimulating)
		{
			hinge = blockJoint as ConfigurableJoint;
			if (freezeToggle.IsActive)
			{
				MakeCogStatic();
			}
			else
			{
				MakeCogNormal();
			}
		}
	}

	private void MakeCogStatic()
	{
		SoftJointLimitSpring angularXLimitSpring = hinge.angularXLimitSpring;
		angularXLimitSpring.damper = 10000f;
		hinge.angularXLimitSpring = angularXLimitSpring;
		JointDrive angularXDrive = hinge.angularXDrive;
		angularXDrive.positionSpring = 10000f;
		hinge.angularXDrive = angularXDrive;
	}

	private void MakeCogNormal()
	{
		SoftJointLimitSpring angularXLimitSpring = hinge.angularXLimitSpring;
		angularXLimitSpring.damper = 0f;
		hinge.angularXLimitSpring = angularXLimitSpring;
		JointDrive angularXDrive = hinge.angularXDrive;
		angularXDrive.positionSpring = 0f;
		hinge.angularXDrive = angularXDrive;
	}
}
