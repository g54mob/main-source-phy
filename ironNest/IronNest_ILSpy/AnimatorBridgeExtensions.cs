using System;
using UnityEngine;

internal static class AnimatorBridgeExtensions
{
	public static float GetFloatSafe(Animator animator, int paramHash, bool paramExists)
	{
		//IL_0056: Expected F4, but got I4
		if (!(animator != null) || !paramExists)
		{
			return 0f;
		}
		return animator.GetFloat(paramHash);
	}

	public static int GetIntSafe(Animator animator, int paramHash, bool paramExists)
	{
		//IL_0081: Expected I4, but got O
		if (animator != null && paramExists)
		{
			if ((object)animator != null)
			{
				return animator.GetInteger(paramHash);
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		return 0;
	}
}
