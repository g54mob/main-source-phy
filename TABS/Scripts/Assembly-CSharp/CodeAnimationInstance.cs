using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class CodeAnimationInstance
{
	public enum AnimationType
	{
		position = 0,
		scale = 1,
		rectPosition = 2,
		floatNumber = 3,
		rotation = 4
	}

	public enum AnimationUse
	{
		In = 0,
		Out = 1,
		None = 2,
		Boop = 3
	}

	[Space(15f)]
	public AnimationType animationType;

	public AnimationUse animationUse;

	public AnimationCurve curve;

	public float multiplier = 1f;

	public float randomMultiplier;

	public Vector3 direction;

	public UnityEvent startEvent;

	public UnityEvent timedEvent;

	public float eventTiming;

	public UnityEvent endEvent;
}
