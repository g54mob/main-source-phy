using System.Collections.Generic;
using UnityEngine;

public class SetGlobalJointSettings : MonoBehaviour
{
	public List<ConfigurableJoint> allJoints = new List<ConfigurableJoint>();

	public float linearSpring;

	public float linearDamper;

	public float limit;

	public float linearDriveSpring;

	public float linearDriveDamper;

	public float angularSpring;

	public float angularDamper;

	public float maxAngular = 500f;

	private void Start()
	{
		Rigidbody[] allRigs = GetComponent<RigidbodyHolder>().AllRigs;
		for (int i = 0; i < allRigs.Length; i++)
		{
			allRigs[i].maxAngularVelocity = maxAngular;
			ConfigurableJoint component = allRigs[i].GetComponent<ConfigurableJoint>();
			if ((bool)component)
			{
				allJoints.Add(component);
			}
			_ = (bool)component;
		}
	}

	public static void SetJointMultiplier(float multiplier, Rigidbody jointRig)
	{
		ConfigurableJoint component = jointRig.GetComponent<ConfigurableJoint>();
		SoftJointLimit highAngularXLimit = component.highAngularXLimit;
		highAngularXLimit.limit *= multiplier;
		component.highAngularXLimit = highAngularXLimit;
		highAngularXLimit = component.lowAngularXLimit;
		highAngularXLimit.limit *= multiplier;
		component.lowAngularXLimit = highAngularXLimit;
		highAngularXLimit = component.angularYLimit;
		highAngularXLimit.limit *= multiplier;
		component.angularYLimit = highAngularXLimit;
		highAngularXLimit = component.angularZLimit;
		highAngularXLimit.limit *= multiplier;
		component.angularZLimit = highAngularXLimit;
	}

	public void SetJointConstraints(bool free = false)
	{
		for (int i = 0; i < allJoints.Count; i++)
		{
			allJoints[i].angularXMotion = ((!free) ? ConfigurableJointMotion.Limited : ConfigurableJointMotion.Free);
			allJoints[i].angularYMotion = ((!free) ? ConfigurableJointMotion.Limited : ConfigurableJointMotion.Free);
			allJoints[i].angularZMotion = ((!free) ? ConfigurableJointMotion.Limited : ConfigurableJointMotion.Free);
		}
	}

	public static void SetRotationalSpring(ConfigurableJoint joint, Rigidbody hostRig, float angularSpring, float angularDamper)
	{
		float num = 10f;
		if ((bool)hostRig)
		{
			num = hostRig.mass;
		}
		SoftJointLimitSpring angularXLimitSpring = joint.angularXLimitSpring;
		if (joint.angularXLimitSpring.spring == 0f)
		{
			angularXLimitSpring.spring = angularSpring * num;
		}
		else
		{
			angularXLimitSpring.spring *= num;
		}
		if (joint.angularXLimitSpring.damper == 0f)
		{
			angularXLimitSpring.damper = angularDamper * num;
		}
		else
		{
			angularXLimitSpring.damper *= num;
		}
		joint.angularXLimitSpring = angularXLimitSpring;
		SoftJointLimitSpring angularYZLimitSpring = joint.angularYZLimitSpring;
		if (joint.angularYZLimitSpring.spring == 0f)
		{
			angularYZLimitSpring.spring = angularSpring * num;
		}
		else
		{
			angularYZLimitSpring.spring *= num;
		}
		if (joint.angularYZLimitSpring.damper == 0f)
		{
			angularYZLimitSpring.damper = angularDamper * num;
		}
		else
		{
			angularYZLimitSpring.damper *= num;
		}
		joint.angularYZLimitSpring = angularYZLimitSpring;
	}

	public static void SetPositionalSpring(ConfigurableJoint joint, Rigidbody hostRig, float setDriveSpring, float setDriveDamper, float setLinearDamper, float setLinearSpring)
	{
		if ((bool)hostRig)
		{
			JointDrive xDrive = joint.xDrive;
			if (xDrive.positionSpring == 0f)
			{
				xDrive.positionSpring = setDriveSpring * hostRig.mass;
			}
			else
			{
				xDrive.positionSpring *= hostRig.mass;
			}
			if (xDrive.positionDamper == 0f)
			{
				xDrive.positionDamper = setDriveDamper * hostRig.mass;
			}
			else
			{
				xDrive.positionDamper *= hostRig.mass;
			}
			joint.xDrive = xDrive;
			joint.yDrive = xDrive;
			joint.zDrive = xDrive;
			SoftJointLimitSpring linearLimitSpring = joint.linearLimitSpring;
			if (linearLimitSpring.spring == 0f)
			{
				linearLimitSpring.spring = setLinearSpring * hostRig.mass;
			}
			else
			{
				linearLimitSpring.spring *= hostRig.mass;
			}
			if (linearLimitSpring.damper == 0f)
			{
				linearLimitSpring.damper = setLinearDamper * hostRig.mass;
			}
			else
			{
				linearLimitSpring.damper *= hostRig.mass;
			}
			joint.linearLimitSpring = linearLimitSpring;
			SoftJointLimit linearLimit = joint.linearLimit;
			if (linearLimit.limit == 0f)
			{
				linearLimit.limit = 0.01f;
			}
			joint.linearLimit = linearLimit;
		}
	}
}
