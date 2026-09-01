using System;
using UnityEngine;

[Serializable]
public class StandingInstance
{
	public string name = "";

	public int id;

	public AnimationCurve curve;

	public AnimationCurve bobCurve;

	public float staticAngleLowering;

	public float downHillHelp;

	public float UpHillHelp;
}
