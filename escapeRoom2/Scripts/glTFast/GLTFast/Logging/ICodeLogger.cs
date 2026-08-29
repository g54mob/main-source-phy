using UnityEngine;

namespace GLTFast.Logging
{
	public interface ICodeLogger
	{
		void Error(LogCode code, params string[] messages);

		void Warning(LogCode code, params string[] messages);

		void Info(LogCode code, params string[] messages);

		void Log(LogType logType, LogCode code, params string[] messages)
		{
			switch (logType)
			{
			case LogType.Log:
				Info(code, messages);
				break;
			case LogType.Warning:
				Warning(code, messages);
				break;
			default:
				Error(code, messages);
				break;
			}
		}

		void Error(string message);

		void Warning(string message);

		void Info(string message);
	}
}
