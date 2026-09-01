using System;
using Landfall.TABS;
using UnityEngine;

[Serializable]
public class AttackArm
{
	public enum ArmState
	{
		Free = 0,
		Holding = 1
	}

	public Unit heldUnit;

	public GameObject targetObj;

	public GameObject restPosObj;

	public float counter = 10f;

	public ArmState armState;

	public float lerpSpeed = 1f;

	public Vector3 smoothTargetPos;

	public Vector3 targetPos;
}
