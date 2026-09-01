using System;
using UnityEngine;

[Serializable]
public class SwingData
{
	public enum SwingType
	{
		Swing = 0,
		Stab = 1
	}

	public SwingType swingType;

	public float swingDirectionAngle;

	public AnimationCurve swingCurve;

	public AnimationCurve shoulderHeightMultiplierCurve;

	public float animationSpeed = 1f;

	[Space(10f)]
	public float startAngle = 90f;

	public float endAngle = -90f;

	[Space(10f)]
	public float tiltFactor;

	public float stabDistanceMultiplier = 1f;

	[Space(10f)]
	public Vector3 swingHoldForward;

	public Vector3 swingHoldUp;

	[Space(10f)]
	public Vector3 curveAnimation;

	public AnimationCurve curveAnimationCurve;
}
