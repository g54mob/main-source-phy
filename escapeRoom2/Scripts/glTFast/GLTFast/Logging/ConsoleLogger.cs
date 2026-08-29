using UnityEngine;

namespace GLTFast.Logging
{
	public class ConsoleLogger : ICodeLogger
	{
		public void Error(LogCode code, params string[] messages)
		{
			Debug.LogError(LogMessages.GetFullMessage(code, messages));
		}

		public void Warning(LogCode code, params string[] messages)
		{
			Debug.LogWarning(LogMessages.GetFullMessage(code, messages));
		}

		public void Info(LogCode code, params string[] messages)
		{
			Debug.Log(LogMessages.GetFullMessage(code, messages));
		}

		public void Log(LogType logType, LogCode code, params string[] messages)
		{
			Debug.unityLogger.Log(logType, LogMessages.GetFullMessage(code, messages));
		}

		public void Error(string message)
		{
			Debug.LogError(message);
		}

		public void Warning(string message)
		{
			Debug.LogWarning(message);
		}

		public void Info(string message)
		{
			Debug.Log(message);
		}
	}
}
