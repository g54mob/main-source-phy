using System.IO;
using UnityEngine;

namespace Backtrace.Unity.Common
{
	internal static class DatabasePathHelper
	{
		internal static string GetFullDatabasePath(string databasePath)
		{
			if (string.IsNullOrEmpty(databasePath))
			{
				return string.Empty;
			}
			return databasePath.ParseInterpolatedString().GetFullPath();
		}

		private static string ParseInterpolatedString(this string databasePath)
		{
			int num = databasePath.IndexOf("${");
			if (num == -1)
			{
				return databasePath;
			}
			int num2 = databasePath.IndexOf('}', num);
			if (num2 == -1)
			{
				return databasePath;
			}
			string text = databasePath.Substring(num, num2 - num + 1);
			if (string.IsNullOrEmpty(text))
			{
				return databasePath;
			}
			string text2 = text.ToLower();
			if (!(text2 == "${application.persistentdatapath}"))
			{
				if (text2 == "${application.datapath}")
				{
					return databasePath.Replace(text, Application.dataPath);
				}
				return databasePath;
			}
			return databasePath.Replace(text, Application.persistentDataPath);
		}

		private static string GetFullPath(this string databasePath)
		{
			if (!Path.IsPathRooted(databasePath))
			{
				databasePath = Path.Combine(Application.persistentDataPath, databasePath);
			}
			return Path.GetFullPath(databasePath);
		}
	}
}
