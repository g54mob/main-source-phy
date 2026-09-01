using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backtrace.Unity.Interfaces;
using Backtrace.Unity.Types;
using UnityEngine;

namespace Backtrace.Unity.Model
{
	internal class NativeCrashUploader
	{
		private IBacktraceApi _backtraceApi;

		internal string nativeCrashesDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp", Application.companyName, Application.productName, "crashes");

		public void SetBacktraceApi(IBacktraceApi backtraceApi)
		{
			_backtraceApi = backtraceApi;
		}

		public IEnumerator SendUnhandledGameCrashesOnGameStartup()
		{
			if (string.IsNullOrEmpty(nativeCrashesDir) || !Directory.Exists(nativeCrashesDir))
			{
				yield break;
			}
			string[] directories = Directory.GetDirectories(nativeCrashesDir);
			string[] array = directories;
			foreach (string path in array)
			{
				string crashDirFullPath = Path.Combine(nativeCrashesDir, path);
				string[] files = Directory.GetFiles(crashDirFullPath);
				if (files.Any((string n) => n.EndsWith("backtrace.json")))
				{
					continue;
				}
				string minidumpPath = files.FirstOrDefault((string n) => n.EndsWith("crash.dmp"));
				if (string.IsNullOrEmpty(minidumpPath))
				{
					continue;
				}
				IEnumerable<string> attachments = files.Where((string n) => n != minidumpPath);
				yield return _backtraceApi.SendMinidump(minidumpPath, attachments, delegate(BacktraceResult result)
				{
					if (result != null && result.Status == BacktraceResultStatus.Ok)
					{
						File.Create(Path.Combine(crashDirFullPath, "backtrace.json"));
					}
				});
			}
		}
	}
}
