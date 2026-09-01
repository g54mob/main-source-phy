using System;
using UnityEngine;

[Serializable]
public class AnimationTorqueInstance
{
	public enum ForceType
	{
		Self = 0,
		World = 1,
		Hip = 2,
		Torso = 3,
		IputDirection = 4
	}

	public string name = "";

	public int ID;

	public Vector3 forwardTorque;

	public Vector3 backwardTorque;

	public ForceType forceType;

	[Header("Step Info")]
	public float switchDelay;

	public float smoothing = 1f;
}
