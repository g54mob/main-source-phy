using System.Collections;
using Landfall.MonoBatch;
using TFBGames;
using UnityEngine;

public class CurveAnimation : BatchedMonobehaviour
{
	public CurveAnimationData animationData;

	public bool isRight = true;

	public bool invertLocalAxis;

	public float uppHillHelp;

	public float multiplier = 1f;

	private CurveAnimationData internalData;

	private AnimationHandler anim;

	private DataHandler data;

	private Rigidbody rig;

	private Transform torso;

	private Transform hip;

	private Transform forcePoint;

	protected override void Start()
	{
		base.Start();
		torso = base.transform.root.GetComponentInChildren<Torso>().transform;
		hip = base.transform.root.GetComponentInChildren<Hip>().transform;
		rig = GetComponent<Rigidbody>();
		anim = GetComponentInParent<AnimationHandler>();
		data = GetComponentInParent<DataHandler>();
		ForcePoint componentInChildren = GetComponentInChildren<ForcePoint>();
		if ((bool)componentInChildren)
		{
			forcePoint = componentInChildren.transform;
		}
		internalData = animationData;
		StartCoroutine(CheckMultiplierAfterFrame());
	}

	private IEnumerator CheckMultiplierAfterFrame()
	{
		yield return new WaitForEndOfFrame();
		if (!data.unit.isUnitCopy)
		{
			multiplier *= anim.multiplier;
		}
	}

	public override void BatchedUpdate()
	{
		if (!rig)
		{
			return;
		}
		for (int i = 0; i < internalData.animations.Length; i++)
		{
			if (internalData.animations[i].ID == anim.currentState)
			{
				Animate(internalData.animations[i]);
			}
		}
		for (int j = 0; j < internalData.forceAnimations.Length; j++)
		{
			if (internalData.forceAnimations[j].ID == anim.currentState)
			{
				Animate(internalData.forceAnimations[j], isTorque: false);
			}
		}
	}

	private void Animate(CurveAnimationInstance currentAnimation, bool isTorque = true)
	{
		Vector3 vector = ((data.isRight != isRight) ? currentAnimation.backward : currentAnimation.forward);
		vector *= currentAnimation.curve.Evaluate(Mathf.Clamp(data.sinceSwitchedStep / Mathf.Clamp(data.currentLowSwitchCap, 0.001f, 1f), 0f, 1f));
		if (vector.sqrMagnitude < Mathf.Epsilon || data.muscleControl == 0f || multiplier == 0f)
		{
			return;
		}
		if (currentAnimation.forceType == CurveAnimationInstance.Space.Self)
		{
			vector = base.transform.TransformDirection(vector);
			if (invertLocalAxis)
			{
				vector *= -1f;
			}
		}
		else if (currentAnimation.forceType == CurveAnimationInstance.Space.Hip)
		{
			if (!isRight)
			{
				vector.y *= -1f;
			}
			vector = hip.TransformDirection(vector);
		}
		else if (currentAnimation.forceType == CurveAnimationInstance.Space.Torso)
		{
			if (!isRight)
			{
				vector.y *= -1f;
			}
			vector = torso.TransformDirection(vector);
		}
		else if (currentAnimation.forceType == CurveAnimationInstance.Space.IputDirection)
		{
			vector = data.groundedMovementDirectionObject.TransformDirection(vector);
		}
		if (uppHillHelp != 0f && data.isRight == isRight)
		{
			vector *= 1f + Mathf.Clamp(uppHillHelp * data.forwardGroundNormal.y, 0f, 1f);
		}
		vector *= data.muscleControl;
		if (vector.x != float.NaN && vector.y != float.NaN && vector.z != float.NaN)
		{
			Vector3 vector2 = 60f * multiplier * Time.deltaTime * vector;
			if (isTorque)
			{
				rig.AddTorque((0f - FixedTimeStepService.TorqueCoefficient) * vector2, ForceMode.Acceleration);
			}
			else if ((object)forcePoint != null)
			{
				rig.AddForceAtPosition(FixedTimeStepService.LargeForceCoefficient * vector2, forcePoint.position, ForceMode.Acceleration);
			}
			else
			{
				rig.AddForce(FixedTimeStepService.LargeForceCoefficient * vector2, ForceMode.Acceleration);
			}
		}
	}
}
