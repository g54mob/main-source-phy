using System;
using UnityEngine;

[Serializable]
public class AnimationForceInstance
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

	public Vector3 forwardForce;

	public Vector3 backwardForce;

	public ForceType forceType;

	public float switchDelay;

	public float smoothing = 1f;
}
