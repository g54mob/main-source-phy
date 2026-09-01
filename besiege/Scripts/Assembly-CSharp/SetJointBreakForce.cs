using System.Collections;
using UnityEngine;

public class SetJointBreakForce : SimBehaviour
{
	private ConfigurableJoint[] configJoints;

	private HingeJoint[] hingeJoints;

	private float[] startBreakForce;

	private float[] startBreakTorque;

	private int breakCount = 5;

	private Rigidbody myRigidbody;

	private Machine machine;

	protected override void Start()
	{
		base.Start();
		machine = base.ParentMachine;
		if (base.HasParentMachine && base.isSimulating && base.SimPhysics)
		{
			StartCoroutine(StartBreakForce());
		}
	}

	private IEnumerator StartBreakForce()
	{
		for (int i = 0; i < 8; i++)
		{
			yield return new WaitForFixedUpdate();
		}
		myRigidbody = GetComponent<Rigidbody>();
		GetJoints();
		if (base.HasParentMachine && machine.UnbreakableMode)
		{
			SetInvincible();
		}
	}

	private void GetJoints()
	{
		configJoints = base.gameObject.GetComponents<ConfigurableJoint>();
		hingeJoints = base.gameObject.GetComponents<HingeJoint>();
		breakCount = ((configJoints.Length <= hingeJoints.Length) ? hingeJoints.Length : configJoints.Length);
		startBreakForce = new float[breakCount];
		startBreakTorque = new float[breakCount];
		bool flag = true;
		bool flag2 = !HasBasicInfo || basicInfo.infoType != BasicInfo.BasicInfoType.Block || BlockBehaviour.IgnoredSwapBlock((basicInfo as BlockBehaviour).Prefab.Type);
		if (myRigidbody == null || myRigidbody.isKinematic || flag2)
		{
			flag = false;
		}
		for (int i = 0; i < configJoints.Length; i++)
		{
			startBreakForce[i] = configJoints[i].breakForce;
			startBreakTorque[i] = configJoints[i].breakTorque;
			Rigidbody connectedBody = configJoints[i].connectedBody;
			if (flag && (bool)connectedBody)
			{
				configJoints[i].swapBodies = myRigidbody.mass < connectedBody.mass || (connectedBody.isKinematic && !connectedBody.gameObject.CompareTag("StayKinematic"));
			}
		}
		for (int j = 0; j < hingeJoints.Length; j++)
		{
			startBreakForce[j] = hingeJoints[j].breakForce;
			startBreakTorque[j] = hingeJoints[j].breakTorque;
		}
	}

	private void SetInvincible()
	{
		SetConfigJoints();
		SetHingeJoints();
	}

	private IEnumerator SetInvincibleTemp()
	{
		SetConfigJoints();
		SetHingeJoints();
		for (int i = 0; i < 13; i++)
		{
			yield return new WaitForFixedUpdate();
		}
		SetNormal();
	}

	private void SetNormal()
	{
		for (int i = 0; i < configJoints.Length; i++)
		{
			configJoints[i].breakForce = startBreakForce[i];
			configJoints[i].breakTorque = startBreakTorque[i];
		}
		for (int j = 0; j < hingeJoints.Length; j++)
		{
			hingeJoints[j].breakForce = startBreakForce[j];
			hingeJoints[j].breakTorque = startBreakTorque[j];
		}
	}

	private void SetConfigJoints()
	{
		for (int i = 0; i < configJoints.Length; i++)
		{
			configJoints[i].breakForce = float.PositiveInfinity;
			configJoints[i].breakTorque = float.PositiveInfinity;
			if (configJoints[i].projectionMode != JointProjectionMode.PositionAndRotation)
			{
				configJoints[i].projectionMode = JointProjectionMode.PositionAndRotation;
				configJoints[i].projectionDistance = 0.5f;
				configJoints[i].projectionAngle = 180f;
			}
		}
	}

	private void SetHingeJoints()
	{
		for (int i = 0; i < hingeJoints.Length; i++)
		{
			hingeJoints[i].breakForce = float.PositiveInfinity;
			hingeJoints[i].breakTorque = float.PositiveInfinity;
		}
	}
}
