using System;
using UnityEngine;

[Serializable]
public class CurveAnimationInstance
{
	public enum Space
	{
		Self = 0,
		World = 1,
		Hip = 2,
		Torso = 3,
		IputDirection = 4
	}

	public string name = "";

	public int ID;

	public Vector3 forward;

	public Vector3 backward;

	public Space forceType;

	public AnimationCurve curve;
}
