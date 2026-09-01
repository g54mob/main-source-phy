using System;
using System.Collections.Generic;
using System.IO;
using Backtrace.Unity.Common;
using Backtrace.Unity.Types;
using UnityEngine;

namespace Backtrace.Unity.Model.Database
{
	internal class BacktraceDatabaseAttachmentManager
	{
		private readonly BacktraceDatabaseSettings _settings;

		private float _lastScreenTime;

		private string _lastScreenPath;

		private readonly object _lock = new object();

		public BacktraceDatabaseAttachmentManager(BacktraceDatabaseSettings settings)
		{
			_settings = settings;
		}

		public IEnumerable<string> GetReportAttachments(BacktraceData data)
		{
			string uuidString = data.UuidString;
			List<string> list = new List<string>();
			AddIfPathIsNotEmpty(list, GetScreenshotPath(uuidString));
			AddIfPathIsNotEmpty(list, GetUnityPlayerLogFile(data, uuidString));
			AddIfPathIsNotEmpty(list, GetMinidumpPath(data, uuidString));
			return list;
		}

		private void AddIfPathIsNotEmpty(List<string> source, string attachmentPath)
		{
			if (!string.IsNullOrEmpty(attachmentPath))
			{
				source.Add(attachmentPath);
			}
		}

		private string GetMinidumpPath(BacktraceData backtraceData, string dataPrefix)
		{
			if (_settings.MinidumpType == MiniDumpType.None)
			{
				return string.Empty;
			}
			string text = Path.Combine(_settings.DatabasePath, $"{dataPrefix}-dump.dmp");
			BacktraceReport report = backtraceData.Report;
			if (report == null)
			{
				return string.Empty;
			}
			MinidumpException exceptionType = (report.ExceptionTypeReport ? MinidumpException.Present : MinidumpException.None);
			if (!MinidumpHelper.Write(text, _settings.MinidumpType, exceptionType))
			{
				return string.Empty;
			}
			return text;
		}

		private string GetScreenshotPath(string dataPrefix)
		{
			if (!_settings.GenerateScreenshotOnException)
			{
				return string.Empty;
			}
			string text = Path.Combine(_settings.DatabasePath, $"{dataPrefix}-screen.jpg");
			lock (_lock)
			{
				if (BacktraceDatabase.LastFrameTime == _lastScreenTime)
				{
					if (File.Exists(_lastScreenPath))
					{
						File.Copy(_lastScreenPath, text);
						return text;
					}
					return _lastScreenPath;
				}
				int width = Screen.width;
				int height = Screen.height;
				Texture2D texture2D = new Texture2D(width, height, TextureFormat.RGB24, mipChain: false);
				texture2D.ReadPixels(new Rect(0f, 0f, width, height), 0, 0);
				texture2D.Apply();
				byte[] array = texture2D.EncodeToJPG();
				using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write))
				{
					fileStream.Write(array, 0, array.Length);
				}
				_lastScreenTime = BacktraceDatabase.LastFrameTime;
				_lastScreenPath = text;
				return text;
			}
		}

		private string GetUnityPlayerLogFile(BacktraceData backtraceData, string dataPrefix)
		{
			if (!_settings.AddUnityLogToReport)
			{
				return string.Empty;
			}
			string text = Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)).FullName, "LocalLow", Application.companyName, Application.productName, "Player.log");
			if (string.IsNullOrEmpty(text) || !File.Exists(text))
			{
				return string.Empty;
			}
			string text2 = Path.Combine(_settings.DatabasePath, $"{dataPrefix}-lg.log");
			File.Copy(text, text2);
			return text2;
		}
	}
}
