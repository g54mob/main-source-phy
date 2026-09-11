using UnityEngine;
using plog.Models;

namespace plog.unity.Extensions
{
	public static class UnityPLogExtensions
	{
		public static Level ToPLogLevel(this LogType type)
		{
			switch (type)
			{
			case LogType.Log:
				return Level.Info;
			case LogType.Warning:
				return Level.Warning;
			case LogType.Error:
			case LogType.Assert:
			case LogType.Exception:
				return Level.Error;
			default:
				return Level.Info;
			}
		}

		public static LogType ToUnityLogType(this Level level)
		{
			return level switch
			{
				Level.Debug => LogType.Log, 
				Level.Info => LogType.Log, 
				Level.Warning => LogType.Warning, 
				Level.Error => LogType.Error, 
				Level.Exception => LogType.Exception, 
				Level.CommandLine => LogType.Log, 
				Level.Config => LogType.Log, 
				_ => LogType.Log, 
			};
		}
	}
}
