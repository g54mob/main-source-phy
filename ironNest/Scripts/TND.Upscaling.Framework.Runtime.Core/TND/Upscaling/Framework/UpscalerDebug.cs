using System;
using System.Diagnostics;

namespace TND.Upscaling.Framework
{
	public static class UpscalerDebug
	{
		[Conditional("TND_DEBUG")]
		public static void Log(object message)
		{
		}

		[Conditional("TND_DEBUG")]
		public static void LogWarning(object message)
		{
		}

		[Conditional("TND_DEBUG")]
		public static void LogError(object message)
		{
		}

		[Conditional("TND_DEBUG")]
		public static void LogException(Exception exception)
		{
		}
	}
}
