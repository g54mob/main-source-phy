using System.Collections.Concurrent;
using System.Text;
using UnityEngine;

namespace Backtrace.Unity.Model
{
	internal class BacktraceLogManager
	{
		internal readonly ConcurrentQueue<string> LogQueue;

		private readonly object lockObject = new object();

		private readonly uint _limit;

		public int Size => LogQueue.Count;

		public bool Disabled => _limit == 0;

		public BacktraceLogManager(uint numberOfLogs)
		{
			_limit = numberOfLogs;
			LogQueue = new ConcurrentQueue<string>();
		}

		public bool Enqueue(BacktraceReport report)
		{
			return Enqueue(new BacktraceUnityMessage(report));
		}

		public bool Enqueue(string message, string stackTrace, LogType type)
		{
			return Enqueue(new BacktraceUnityMessage(message, stackTrace, type));
		}

		public bool Enqueue(BacktraceUnityMessage unityMessage)
		{
			if (Disabled)
			{
				return false;
			}
			LogQueue.Enqueue(unityMessage.ToString());
			lock (lockObject)
			{
				string result;
				while (LogQueue.Count > _limit && LogQueue.TryDequeue(out result))
				{
				}
			}
			return true;
		}

		public string ToSourceCode()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string item in LogQueue)
			{
				stringBuilder.AppendLine(item);
			}
			return stringBuilder.ToString();
		}
	}
}
