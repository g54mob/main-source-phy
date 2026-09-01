using UnityEngine;

public class JointActions
{
	public static ConfigurableJoint AttachJoint(Rigidbody handRig, Holdable holdable, Vector3 handPos, Quaternion handRot, bool simpleAttach = false, float breakForce = 0f)
	{
		ConfigurableJoint configurableJoint = holdable.gameObject.AddComponent<ConfigurableJoint>();
		if (simpleAttach)
		{
			holdable.transform.SetPositionAndRotation(handPos, handRot);
		}
		else
		{
			handRig.transform.SetPositionAndRotation(handPos, handRot);
		}
		configurableJoint.xMotion = ConfigurableJointMotion.Locked;
		configurableJoint.yMotion = ConfigurableJointMotion.Locked;
		configurableJoint.zMotion = ConfigurableJointMotion.Locked;
		configurableJoint.angularXMotion = ConfigurableJointMotion.Limited;
		configurableJoint.angularYMotion = ConfigurableJointMotion.Limited;
		configurableJoint.angularZMotion = ConfigurableJointMotion.Limited;
		SoftJointLimit lowAngularXLimit = configurableJoint.lowAngularXLimit;
		lowAngularXLimit.limit = 0f - holdable.jointData.limit;
		configurableJoint.lowAngularXLimit = lowAngularXLimit;
		lowAngularXLimit.limit = holdable.jointData.limit;
		configurableJoint.highAngularXLimit = lowAngularXLimit;
		configurableJoint.angularYLimit = lowAngularXLimit;
		configurableJoint.angularZLimit = lowAngularXLimit;
		if (breakForce != 0f)
		{
			configurableJoint.breakForce = breakForce;
			configurableJoint.breakTorque = breakForce;
		}
		configurableJoint.connectedBody = handRig;
		if (!simpleAttach)
		{
			configurableJoint.anchor = holdable.transform.InverseTransformPoint(handRig.transform.position);
		}
		configurableJoint.projectionMode = JointProjectionMode.PositionAndRotation;
		JointDrive angularXDrive = configurableJoint.angularXDrive;
		angularXDrive.positionSpring = 500f * holdable.jointData.angularDrive;
		angularXDrive.positionDamper = 50f * holdable.jointData.angularDrive;
		configurableJoint.angularXDrive = angularXDrive;
		configurableJoint.angularYZDrive = angularXDrive;
		handRig.GetComponentInChildren<Collider>().enabled = true;
		holdable.rig.isKinematic = false;
		configurableJoint.enablePreprocessing = false;
		return configurableJoint;
	}

	public static ConfigurableJoint AttachJoint(Rigidbody rig1, Rigidbody rig2, float angles, float spring, bool lockRot, bool lockPos, float positionSpring)
	{
		ConfigurableJoint configurableJoint = rig1.gameObject.AddComponent<ConfigurableJoint>();
		configurableJoint.xMotion = ((!lockPos) ? ConfigurableJointMotion.Limited : ConfigurableJointMotion.Locked);
		configurableJoint.yMotion = ((!lockPos) ? ConfigurableJointMotion.Limited : ConfigurableJointMotion.Locked);
		configurableJoint.zMotion = ((!lockPos) ? ConfigurableJointMotion.Limited : ConfigurableJointMotion.Locked);
		configurableJoint.angularXMotion = ((!lockRot) ? ConfigurableJointMotion.Limited : ConfigurableJointMotion.Locked);
		configurableJoint.angularYMotion = ((!lockRot) ? ConfigurableJointMotion.Limited : ConfigurableJointMotion.Locked);
		configurableJoint.angularZMotion = ((!lockRot) ? ConfigurableJointMotion.Limited : ConfigurableJointMotion.Locked);
		if (!lockPos)
		{
			SoftJointLimit linearLimit = configurableJoint.linearLimit;
			linearLimit.limit = 0.01f;
			configurableJoint.linearLimit = linearLimit;
			SoftJointLimitSpring linearLimitSpring = configurableJoint.linearLimitSpring;
			linearLimitSpring.spring = 500f * positionSpring;
			linearLimitSpring.damper = 50f * positionSpring;
			configurableJoint.linearLimitSpring = linearLimitSpring;
			JointDrive angularXDrive = configurableJoint.angularXDrive;
			angularXDrive.positionSpring = 500f * positionSpring;
			angularXDrive.positionDamper = 50f * positionSpring;
			configurableJoint.xDrive = angularXDrive;
			configurableJoint.yDrive = angularXDrive;
			configurableJoint.zDrive = angularXDrive;
		}
		SoftJointLimit lowAngularXLimit = configurableJoint.lowAngularXLimit;
		lowAngularXLimit.limit = 0f - angles;
		configurableJoint.lowAngularXLimit = lowAngularXLimit;
		lowAngularXLimit.limit = angles;
		configurableJoint.highAngularXLimit = lowAngularXLimit;
		configurableJoint.angularYLimit = lowAngularXLimit;
		configurableJoint.angularZLimit = lowAngularXLimit;
		configurableJoint.connectedBody = rig2;
		configurableJoint.anchor = rig1.transform.InverseTransformPoint(rig1.transform.position);
		configurableJoint.projectionMode = JointProjectionMode.PositionAndRotation;
		JointDrive angularXDrive2 = configurableJoint.angularXDrive;
		angularXDrive2.positionSpring = 500f * spring;
		angularXDrive2.positionDamper = 50f * spring;
		configurableJoint.angularXDrive = angularXDrive2;
		configurableJoint.angularYZDrive = angularXDrive2;
		configurableJoint.enablePreprocessing = false;
		return configurableJoint;
	}
}
