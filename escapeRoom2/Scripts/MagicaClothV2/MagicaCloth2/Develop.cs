using System.Diagnostics;
using UnityEngine;

namespace MagicaCloth2
{
	public static class Develop
	{
		public static void Log(in object mes)
		{
			UnityEngine.Debug.Log($"[MC2] {mes}");
		}

		public static void LogWarning(in object mes)
		{
			UnityEngine.Debug.LogWarning($"[MC2] {mes}");
		}

		public static void LogError(in object mes)
		{
			UnityEngine.Debug.LogError($"[MC2] {mes}");
		}

		[Conditional("MC2_LOG")]
		public static void DebugLog(in object mes)
		{
			UnityEngine.Debug.Log($"[MC2 DEBUG] {mes}");
		}

		[Conditional("MC2_DEBUG")]
		public static void DebugLogWarning(in object mes)
		{
			UnityEngine.Debug.LogWarning($"[MC2 DEBUG] {mes}");
		}

		[Conditional("MC2_DEBUG")]
		public static void DebugLogError(in object mes)
		{
			UnityEngine.Debug.LogError($"[MC2 DEBUG] {mes}");
		}

		[Conditional("MC2_DEBUG")]
		public static void Assert(bool condition)
		{
		}
	}
}
