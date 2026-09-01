using System.Collections;
using UnityEngine;

public class SetJointIdleOnSim : SimBehaviour
{
	public bool weakenJointsOnCollision = true;

	[SerializeField]
	[HideInInspector]
	private ConfigurableJoint[] configJoints;

	[HideInInspector]
	[SerializeField]
	private float[] startBreakForce;

	[HideInInspector]
	[SerializeField]
	private float[] startBreakTorque;

	private float pct = 1f;

	protected override void Start()
	{
		base.Start();
		if (!base.isSimulating)
		{
			GetJoints();
			SetInvincible();
		}
		else
		{
			StartCoroutine(ResetJoints());
		}
	}

	private void GetJoints()
	{
		configJoints = base.gameObject.GetComponents<ConfigurableJoint>();
		startBreakForce = new float[configJoints.Length];
		startBreakTorque = new float[configJoints.Length];
		for (int i = 0; i < configJoints.Length; i++)
		{
			startBreakForce[i] = configJoints[i].breakForce;
			startBreakTorque[i] = configJoints[i].breakTorque;
		}
	}

	private void SetInvincible()
	{
		for (int i = 0; i < configJoints.Length; i++)
		{
			configJoints[i].breakForce = float.PositiveInfinity;
			configJoints[i].breakTorque = float.PositiveInfinity;
		}
	}

	private void SetStrength(float pct = 1f)
	{
		for (int i = 0; i < configJoints.Length; i++)
		{
			ConfigurableJoint configurableJoint = configJoints[i];
			if ((bool)configurableJoint)
			{
				configurableJoint.breakForce = startBreakForce[i] * pct;
				configurableJoint.breakTorque = startBreakTorque[i] * pct;
			}
		}
	}

	private IEnumerator ResetJoints()
	{
		for (int i = 0; i < 8; i++)
		{
			yield return new WaitForFixedUpdate();
		}
		SetStrength();
	}

	protected virtual void OnCollisionEnter(Collision collision)
	{
		if (base.enabled && base.isSimulating && weakenJointsOnCollision && collision.relativeVelocity.sqrMagnitude > 100f)
		{
			pct -= 0.1f;
			SetStrength(pct);
		}
	}

	protected void OnDisable()
	{
		if (!base.isSimulating)
		{
			return;
		}
		for (int i = 0; i < configJoints.Length; i++)
		{
			ConfigurableJoint configurableJoint = configJoints[i];
			if ((bool)configurableJoint)
			{
				Object.Destroy(configurableJoint);
			}
		}
	}
}
