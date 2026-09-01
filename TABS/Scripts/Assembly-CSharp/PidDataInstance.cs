using System;
using UnityEngine;

[Serializable]
public class PidDataInstance
{
	[HideInInspector]
	public Rigidbody rig;

	public bool disableForceDuringSwing = true;

	public float proportionalForce;

	public bool disableTorqueDuringSwing = true;

	public float proportionalTorque;

	public float holdingForceMultiplier = 1f;

	public float holdingTorqueMultiplier = 1f;

	public float capAngle;

	public float extraUpPerMeter;
}
