using System;
using System.Collections.Generic;
using UnityEngine;

namespace GLTFast.Logging
{
	[Serializable]
	public class CollectingLogger : ICodeLogger
	{
		private List<LogItem> m_Items;

		public int Count => m_Items?.Count ?? 0;

		public IEnumerable<LogItem> Items => m_Items?.AsReadOnly();

		public void Error(LogCode code, params string[] messages)
		{
			if (m_Items == null)
			{
				m_Items = new List<LogItem>();
			}
			m_Items.Add(new LogItem(LogType.Error, code, messages));
		}

		public void Warning(LogCode code, params string[] messages)
		{
			if (m_Items == null)
			{
				m_Items = new List<LogItem>();
			}
			m_Items.Add(new LogItem(LogType.Warning, code, messages));
		}

		public void Info(LogCode code, params string[] messages)
		{
			if (m_Items == null)
			{
				m_Items = new List<LogItem>();
			}
			m_Items.Add(new LogItem(LogType.Log, code, messages));
		}

		public void Log(LogType logType, LogCode code, params string[] messages)
		{
			if (m_Items == null)
			{
				m_Items = new List<LogItem>();
			}
			m_Items.Add(new LogItem(logType, code, messages));
		}

		public void Error(string message)
		{
			if (m_Items == null)
			{
				m_Items = new List<LogItem>();
			}
			m_Items.Add(new LogItem(LogType.Error, LogCode.None, message));
		}

		public void Warning(string message)
		{
			if (m_Items == null)
			{
				m_Items = new List<LogItem>();
			}
			m_Items.Add(new LogItem(LogType.Warning, LogCode.None, message));
		}

		public void Info(string message)
		{
			if (m_Items == null)
			{
				m_Items = new List<LogItem>();
			}
			m_Items.Add(new LogItem(LogType.Log, LogCode.None, message));
		}

		public void LogAll()
		{
			if (m_Items == null)
			{
				return;
			}
			foreach (LogItem item in m_Items)
			{
				item.Log();
			}
		}
	}
}
