using System;
using UnityEngine;

[Serializable]
public class SpellAnimation
{
	public enum AnimationRig
	{
		This = 0,
		Torso = 1,
		Hip = 2,
		All = 3,
		ThisRig = 4
	}

	public RangeWeapon.SpawnRotation animationDirection;

	public AnimationRig animationRig;

	public float animationDelay;

	public AnimationCurve rangeMultiplierCurve;

	public AnimationCurve rigAnimationCurve;

	public float rigAnimationForce;

	public bool setDirectionContinious;

	public bool invertForceIfLeft;
}
