using UnityEngine;

public static class BuildInfoLogger
{
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void LogBuildInfo()
	{
	}
}
