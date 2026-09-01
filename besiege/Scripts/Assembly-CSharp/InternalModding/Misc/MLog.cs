using UnityEngine;

namespace InternalModding.Misc
{
	public static class MLog
	{
		public const string LogTag = "[Mods] ";

		public static void Info(string message)
		{
			Debug.Log("[Mods] " + message);
		}

		public static void InfoFormat(string format, params object[] args)
		{
			Debug.LogFormat("[Mods] " + format, args);
		}

		public static void Warn(string message)
		{
			Debug.LogWarning("[Mods] " + message);
		}

		public static void WarnFormat(string format, params object[] args)
		{
			Debug.LogWarningFormat("[Mods] " + format, args);
		}

		public static void Error(string message)
		{
			Debug.LogError("[Mods] " + message);
		}

		public static void ErrorFormat(string format, params object[] args)
		{
			Debug.LogErrorFormat("[Mods] " + format, args);
		}
	}
}
