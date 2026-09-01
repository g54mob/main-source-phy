using UnityEngine;

public class WebStick : MonoBehaviour
{
	public ProjectileStick stick1;

	public ProjectileStick stick2;

	private ConfigurableJoint joint;

	private void Update()
	{
		if (!stick1.target || !stick2.target || (bool)joint)
		{
			return;
		}
		Rigidbody rigidbody = null;
		Rigidbody rigidbody2 = null;
		if ((bool)stick1.targetRig)
		{
			rigidbody = stick1.targetRig;
		}
		if ((bool)stick2.targetRig)
		{
			rigidbody2 = stick2.targetRig;
		}
		if (((bool)rigidbody || (bool)rigidbody2) && rigidbody != rigidbody2)
		{
			if ((bool)rigidbody)
			{
				joint = rigidbody.gameObject.AddComponent<ConfigurableJoint>();
				if ((bool)rigidbody2)
				{
					joint.connectedBody = rigidbody2;
				}
				joint.anchor = rigidbody.transform.InverseTransformPoint(stick2.transform.position);
			}
			else
			{
				joint = rigidbody2.gameObject.AddComponent<ConfigurableJoint>();
				joint.anchor = rigidbody2.transform.InverseTransformPoint(stick1.transform.position);
			}
			joint.xMotion = ConfigurableJointMotion.Limited;
			joint.yMotion = ConfigurableJointMotion.Limited;
			joint.zMotion = ConfigurableJointMotion.Limited;
			SoftJointLimit linearLimit = joint.linearLimit;
			linearLimit.limit = 0.01f;
			joint.linearLimit = linearLimit;
			SoftJointLimitSpring linearLimitSpring = joint.linearLimitSpring;
			linearLimitSpring.spring = 10f;
			joint.linearLimitSpring = linearLimitSpring;
			JointDrive xDrive = joint.xDrive;
			xDrive.positionSpring = 2000f;
			xDrive.positionDamper = 100f;
			joint.xDrive = xDrive;
			joint.yDrive = xDrive;
			joint.zDrive = xDrive;
		}
		else
		{
			base.enabled = false;
		}
	}

	private void OnDestroy()
	{
		if ((bool)joint)
		{
			Object.Destroy(joint);
		}
	}
}
