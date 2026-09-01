using UnityEngine;

internal static class AnimatorBridgeExtensions
{
	public static float GetFloatSafe(this Animator animator, int paramHash, bool paramExists)
	{
		return 0f;
	}

	public static int GetIntSafe(this Animator animator, int paramHash, bool paramExists)
	{
		return 0;
	}
}
