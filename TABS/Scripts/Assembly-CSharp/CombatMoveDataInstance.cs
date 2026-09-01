using System;
using UnityEngine;

[Serializable]
public class CombatMoveDataInstance
{
	public enum RigidBodyToMove
	{
		Head = 0,
		Torso = 1,
		Hip = 2,
		FootLeft = 3,
		FootRight = 4,
		AllRigs = 5,
		HandLeft = 6,
		HandRight = 7,
		MainWeapon = 8,
		This = 9,
		Specific = 10
	}

	public enum ForceDirection
	{
		Up = 0,
		TorwardTarget = 1,
		AwayFromTargetWeapon = 2,
		CharacterForward = 3,
		CharacterRight = 4,
		CrossUpAndAwayFromAttacker = 5,
		CrossUpAndTowardsUnitTarget = 6,
		RotateTowardsTarget = 7,
		AwayFromTargetObject = 8,
		CrossUpAndAwayFromTargetObject = 9,
		TowardsTargetHead = 10,
		RotateTowardsTargetHead = 11,
		InWalkDirection = 12,
		RotateTowardsWalkDirection = 13,
		RigUp = 14,
		RotateTowardsPossCamElseTarget = 15,
		TowardTargetWithoutY = 16
	}

	public float force;

	public float torque;

	public AnimationCurve forceCurve;

	public RigidBodyToMove rigidbodyToMove;

	public Rigidbody specificRig;

	public ForceDirection forceDirection;

	public bool setDirectionContiniouiouss = true;

	public bool includeWeapons;

	public bool ignoreY = true;

	public float predictionAmount = 0.2f;

	public bool randomizeDirection;

	public bool normalize = true;

	public bool useAlternateForceProjectMarsClient;

	public float alternateClientForce;

	[HideInInspector]
	public float randomMultiplier = 1f;

	public AnimationCurve randomCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);
}
