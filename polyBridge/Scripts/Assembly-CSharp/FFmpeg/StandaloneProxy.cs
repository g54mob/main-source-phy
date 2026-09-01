using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;

namespace FFmpeg
{
	public class StandaloneProxy
	{
		public static string BINARY_RELATIVE_PATH_WIN = "_AssetStore/FFmpeg/Standalone/Win/ffmpeg";

		public static string BINARY_RELATIVE_PATH_MAC = "_AssetStore/FFmpeg/Standalone/Mac/ffmpeg";

		public static string BINARY_RELATIVE_PATH_LINUX = "_AssetStore/FFmpeg/Standalone/Linux/ffmpeg";

		private static string binaryPath;

		private static StringBuilder buffer;

		private static Action<string> callback;

		private static string m_Command;

		private static Process m_Process;

		private static Thread m_Thread;

		private static bool isRunning { get; set; }

		public static void Begin(Action<string> _callback)
		{
			binaryPath = Path.Combine(Application.dataPath, "ffmpeg");
			if (!File.Exists(binaryPath))
			{
				string text = "Binary is not found at " + binaryPath;
				_callback(text);
				throw new FileNotFoundException(text);
			}
			callback = _callback;
		}

		public static void Abort()
		{
			if (isRunning)
			{
				if (m_Process != null)
				{
					m_Process.Kill();
				}
				if (m_Thread != null)
				{
					m_Thread.Abort();
				}
				isRunning = false;
			}
		}

		private static void DoWork()
		{
			isRunning = true;
			Thread.CurrentThread.IsBackground = true;
			m_Process = new Process();
			m_Process.StartInfo.RedirectStandardOutput = true;
			m_Process.StartInfo.RedirectStandardError = true;
			m_Process.StartInfo.UseShellExecute = false;
			m_Process.StartInfo.CreateNoWindow = true;
			m_Process.StartInfo.FileName = binaryPath;
			m_Process.StartInfo.Arguments = m_Command;
			m_Process.OutputDataReceived += delegate(object s, DataReceivedEventArgs e)
			{
				callback("FFmpeg COMMAND: onProgress: " + AppendLog(e.Data));
			};
			m_Process.ErrorDataReceived += delegate(object s, DataReceivedEventArgs e)
			{
				if (!string.IsNullOrEmpty(e.Data))
				{
					if (e.Data.ToLower().Contains("error"))
					{
						callback("FFmpeg EXCEPTION: " + AppendLog(e.Data));
					}
					else
					{
						callback("FFmpeg COMMAND: onProgress: " + AppendLog(e.Data));
					}
				}
			};
			m_Process.Start();
			callback("FFmpeg COMMAND: onStart\nStarted\n");
			m_Process.BeginOutputReadLine();
			m_Process.BeginErrorReadLine();
			m_Process.WaitForExit();
			callback("FFmpeg COMMAND: " + ((m_Process.ExitCode == 0) ? ("onSuccess: " + AppendLog("Success!")) : ("onFailure: " + AppendLog("Failure. Search details above"))));
			m_Process.Close();
			callback("FFmpeg COMMAND: onFinish\nFinished\n");
			isRunning = false;
		}

		public static void Execute(string command)
		{
			if (!isRunning)
			{
				buffer = new StringBuilder(32767);
				m_Command = command;
				m_Thread = new Thread(DoWork);
				m_Thread.Start();
			}
		}

		private static string AppendLog(string msg)
		{
			return buffer.Append(msg).Append("\n").ToString();
		}
	}
}
