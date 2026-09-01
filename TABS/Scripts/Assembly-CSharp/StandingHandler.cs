using System;
using Landfall.MonoBatch;
using UnityEngine;

public class StandingHandler : BatchedMonobehaviour
{
	[Serializable]
	public class RigWithMultiplier
	{
		public Rigidbody rig;

		public float multiplier;
	}

	public float selfOffset;

	public StandingData standingData;

	public RigWithMultiplier[] rigsToLift;

	public AnimationCurve torsoVelocityMultiplierCurve;

	private AnimationHandler anim;

	private DataHandler data;

	protected override void Start()
	{
		base.Start();
		data = GetComponent<DataHandler>();
		anim = GetComponent<AnimationHandler>();
		data.baseHeight = (standingData.standingInstances[0].curve.keys[1].time + standingData.standingInstances[0].curve.keys[2].time) / 2f;
	}

	public override void BatchedFixedUpdate()
	{
		if (data.Dead || !data.isGrounded)
		{
			return;
		}
		StandingInstance standingInstance = null;
		for (int i = 0; i < standingData.standingInstances.Length; i++)
		{
			if (anim.currentState == standingData.standingInstances[i].id)
			{
				standingInstance = standingData.standingInstances[i];
			}
		}
		if (standingInstance == null)
		{
			standingInstance = standingData.standingInstances[standingData.defaultSate];
		}
		float num = data.upHillVector.magnitude * 0.05f * standingInstance.staticAngleLowering + standingData.baseOffset;
		float num2 = ((Mathf.Clamp(data.forwardGroundNormal.y * standingInstance.downHillHelp, float.NegativeInfinity, 0f) * 0.2f + Mathf.Clamp(data.forwardGroundNormal.y * standingInstance.UpHillHelp, 0f, float.PositiveInfinity)) * 0.1f + standingInstance.curve.Evaluate((data.distanceToGround + selfOffset + num + standingInstance.bobCurve.Evaluate(data.sinceSwitchedStep / data.currentLowSwitchCap)) / base.transform.root.localScale.x)) * data.muscleControl * standingData.standingForce;
		for (int j = 0; j < rigsToLift.Length; j++)
		{
			RigWithMultiplier rigWithMultiplier = rigsToLift[j];
			rigWithMultiplier.rig.AddForce(num2 * rigWithMultiplier.multiplier * Vector3.up, ForceMode.Acceleration);
		}
	}
}
