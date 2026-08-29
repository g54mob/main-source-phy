using System;
using System.Collections.Generic;
using UnityEngine;

namespace GLTFast.Logging
{
	[Serializable]
	public class LogItem
	{
		[SerializeField]
		private LogType type;

		[SerializeField]
		private LogCode code;

		[SerializeField]
		private string[] messages;

		public LogType Type => type;

		public LogCode Code => code;

		public string[] Messages => messages;

		public LogItem(LogType type, LogCode code, params string[] messages)
		{
			this.type = type;
			this.code = code;
			this.messages = messages;
		}

		public void Log()
		{
			Debug.LogFormat(Type, LogOption.NoStacktrace, null, LogMessages.GetFullMessage(Code, Messages));
		}

		public override string ToString()
		{
			return LogMessages.GetFullMessage(Code, Messages);
		}

		public override int GetHashCode()
		{
			int num = 17;
			num = num * 31 + Type.GetHashCode();
			num = num * 31 + Code.GetHashCode();
			if (Messages != null)
			{
				num = num * 31 + Messages.Length;
				string[] array = Messages;
				foreach (string text in array)
				{
					num = num * 31 + text.GetHashCode();
				}
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			return Equals((LogItem)obj);
		}

		private bool Equals(LogItem other)
		{
			if (Type != other.Type || Code != other.Code)
			{
				return false;
			}
			if ((Messages == null) ^ (other.Messages == null))
			{
				return false;
			}
			if (Messages != null)
			{
				return AreEqual(Messages, other.Messages);
			}
			return true;
		}

		private static bool AreEqual(IReadOnlyList<string> a, IReadOnlyList<string> b)
		{
			if (a.Count == b.Count)
			{
				for (int i = 0; i < a.Count; i++)
				{
					if (!a[i].Equals(b[i]))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}
	}
}
