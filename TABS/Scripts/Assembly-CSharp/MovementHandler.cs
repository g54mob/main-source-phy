using System.Collections.Generic;
using Landfall.MonoBatch;
using UnityEngine;

public class MovementHandler : BatchedMonobehaviour
{
	public MovementHandlerData movementData;

	private RigidbodyHolder rigidbodyHolder;

	[HideInInspector]
	public List<RigidbodyHolder> otherHolders;

	private AnimationHandler anim;

	[HideInInspector]
	public DataHandler data;

	private GeneralInput input;

	public float multiplier = 1f;

	public float cowardMultiplier = 1f;

	public bool stopForTurns = true;

	private Vector3 force;

	public float lerpSpeed = 1f;

	public RigidbodyWithMultiplier[] rigsWithMultiplier;

	public bool removeUpwardsHelp = true;

	public float forwardTohelperForwardLerp = 1f;

	protected override void Start()
	{
		base.Start();
		input = GetComponent<GeneralInput>();
		anim = GetComponent<AnimationHandler>();
		data = GetComponent<DataHandler>();
		rigidbodyHolder = GetComponent<RigidbodyHolder>();
	}

	public override void BatchedFixedUpdate()
	{
		if (data.Dead || !data.isGrounded)
		{
			return;
		}
		Vector3 vector = Vector3.Lerp(data.mainRig.transform.forward, data.groundedMovementDirectionObject.forward, forwardTohelperForwardLerp);
		if (vector.y < 0f && removeUpwardsHelp)
		{
			vector.y *= 0f;
		}
		MovementInstance movementInstance = null;
		for (int i = 0; i < movementData.moves.Length; i++)
		{
			if (movementData.moves[i].ID == anim.currentState)
			{
				movementInstance = movementData.moves[i];
			}
		}
		if (movementInstance == null)
		{
			return;
		}
		float num = 1f;
		if (stopForTurns)
		{
			num = 1f - data.angleToTarget * 0.001f;
		}
		force = Vector3.Lerp(force, movementInstance.force * data.muscleControl * data.torsoAngleMultiplier * input.inputDirection.magnitude * num * vector, lerpSpeed * Time.fixedDeltaTime * 10f);
		if ((bool)data.targetData && data.targetData.input.hasControl)
		{
			if (data.targetData.input.inputDirection.sqrMagnitude > 0.25f && Vector3.Angle(data.targetData.mainRig.velocity, data.characterForwardObject.forward) < 100f)
			{
				cowardMultiplier = 2f;
			}
			else
			{
				cowardMultiplier = 1f;
			}
		}
		else
		{
			cowardMultiplier = 1f;
		}
		Move(rigidbodyHolder, movementInstance);
		if (otherHolders == null || otherHolders.Count <= 0)
		{
			return;
		}
		for (int j = 0; j < otherHolders.Count; j++)
		{
			if ((bool)otherHolders[j] && !otherHolders[j].data.Dead)
			{
				Move(otherHolders[j], movementInstance);
			}
		}
	}

	private void Move(RigidbodyHolder rigHolder, MovementInstance currentMove)
	{
		if (input.inputDirection.z != 1f || !stopForTurns || !(Vector3.Angle(data.lookDirectionIntent, data.input.Direction.forward) > 30f) || input.hasControl)
		{
			Rigidbody[] allRigs = rigHolder.AllRigs;
			Vector3 vector = multiplier * cowardMultiplier * force;
			for (int i = 0; i < allRigs.Length; i++)
			{
				allRigs[i].AddForce(vector, ForceMode.Acceleration);
			}
			Vector3 vector2 = cowardMultiplier * force;
			for (int j = 0; j < rigsWithMultiplier.Length; j++)
			{
				RigidbodyWithMultiplier rigidbodyWithMultiplier = rigsWithMultiplier[j];
				rigidbodyWithMultiplier.rig.AddForce(rigidbodyWithMultiplier.multiplier * vector2, ForceMode.Acceleration);
			}
		}
	}
}
