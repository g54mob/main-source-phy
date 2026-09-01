using UnityEngine;

public class SetJointForceOnSim : SimBehaviour
{
	[SerializeField]
	[HideInInspector]
	private Joint[] joints = new Joint[0];

	[HideInInspector]
	[SerializeField]
	private float[] startBreakForce;

	[SerializeField]
	[HideInInspector]
	private float[] startBreakTorque;

	protected void OnEnable()
	{
		if (!base.isSimulating)
		{
			GetJoints();
			SetInvincible();
		}
		else
		{
			SetBreakable();
		}
	}

	protected void ResetPos()
	{
		for (int i = 0; i < joints.Length; i++)
		{
			Joint joint = joints[i];
			Transform transform = joint.transform;
			Rigidbody component = transform.GetComponent<Rigidbody>();
			Vector3 position = component.position;
			Vector3 vector = position - transform.TransformPoint(joint.anchor);
			Vector3 vector2 = joint.connectedBody.transform.TransformPoint(joint.connectedAnchor);
			if ((vector2 + vector - position).sqrMagnitude > 1f)
			{
				component.MovePosition(vector2 + vector);
				DebugExtension.DebugWireSphere(position + vector, Color.red, 1f, 2f);
				DebugExtension.DebugWireSphere(vector2, Color.red, 0.5f, 2f);
			}
			component.velocity = Vector3.zero;
		}
	}

	protected void FixedUpdate()
	{
		if (!base.isSimulating)
		{
			ResetPos();
		}
	}

	protected void GetJoints()
	{
		if (joints.Length == 0)
		{
			joints = base.gameObject.GetComponentsInChildren<Joint>();
			startBreakForce = new float[joints.Length];
			startBreakTorque = new float[joints.Length];
			for (int i = 0; i < joints.Length; i++)
			{
				startBreakForce[i] = joints[i].breakForce;
				startBreakTorque[i] = joints[i].breakTorque;
				joints[i].autoConfigureConnectedAnchor = false;
			}
		}
	}

	protected void SetInvincible()
	{
		for (int i = 0; i < joints.Length; i++)
		{
			joints[i].breakForce = float.PositiveInfinity;
			joints[i].breakTorque = float.PositiveInfinity;
		}
	}

	protected void SetBreakable()
	{
		for (int i = 0; i < joints.Length; i++)
		{
			Joint joint = joints[i];
			if ((bool)joint)
			{
				joint.breakForce = startBreakForce[i];
				joint.breakTorque = startBreakTorque[i];
			}
		}
	}
}
