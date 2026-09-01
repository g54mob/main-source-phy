using UnityEngine;

public static class AnimationCurveFunctions
{
	public static float GetAnimLength(AnimationCurve curve)
	{
		return curve[curve.length - 1].time;
	}
}
