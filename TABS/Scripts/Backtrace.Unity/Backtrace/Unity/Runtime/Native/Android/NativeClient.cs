using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Backtrace.Unity.Model;
using Backtrace.Unity.Model.JsonData;
using UnityEngine;

namespace Backtrace.Unity.Runtime.Native.Android
{
	internal class NativeClient : INativeClient
	{
		internal float _lastUpdateTime;

		private Thread _anrThread;

		private readonly BacktraceConfiguration _configuration;

		private const string _namespace = "backtrace.io.backtrace_unity_android_plugin";

		private readonly string _nativeAttributesPath = string.Format("{0}.{1}", "backtrace.io.backtrace_unity_android_plugin", "BacktraceAttributes");

		private readonly string _anrPath = string.Format("{0}.{1}", "backtrace.io.backtrace_unity_android_plugin", "BacktraceANRWatchdog");

		private bool _enabled;

		private AndroidJavaObject _anrWatcher;

		private bool _captureNativeCrashes;

		private readonly bool _handlerANR;

		[DllImport("backtrace-native")]
		private static extern bool Initialize(IntPtr submissionUrl, IntPtr databasePath, IntPtr handlerPath, IntPtr keys, IntPtr values);

		[DllImport("backtrace-native")]
		private static extern bool AddAttribute(IntPtr key, IntPtr value);

		[DllImport("backtrace-native", EntryPoint = "DumpWithoutCrash")]
		private static extern bool NativeReport(IntPtr message);

		public NativeClient(string gameObjectName, BacktraceConfiguration configuration)
		{
			_configuration = configuration;
			_ = _enabled;
		}

		private void HandleNativeCrashes()
		{
			if (true)
			{
				Debug.LogWarning("Backtrace native integration status: Disabled NDK integration");
				return;
			}
			string crashpadDatabasePath = _configuration.CrashpadDatabasePath;
			if (string.IsNullOrEmpty(crashpadDatabasePath) || !Directory.Exists(_configuration.GetFullDatabasePath()))
			{
				Debug.LogWarning("Backtrace native integration status: database path doesn't exist");
				return;
			}
			if (!Directory.Exists(crashpadDatabasePath))
			{
				Directory.CreateDirectory(crashpadDatabasePath);
			}
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.os.Build$VERSION"))
			{
				if (androidJavaClass.GetStatic<int>("SDK_INT") < 21)
				{
					Debug.LogWarning("Backtrace native integration status: Unsupported Android API level");
					return;
				}
			}
			string path = Path.Combine(Path.GetDirectoryName(Application.dataPath), "lib");
			if (!Directory.Exists(path))
			{
				return;
			}
			string text = Directory.GetFiles(path, "libcrashpad_handler.so", SearchOption.AllDirectories).FirstOrDefault();
			if (string.IsNullOrEmpty(text))
			{
				Debug.LogWarning("Backtrace native integration status: Cannot find crashpad library");
				return;
			}
			BacktraceAttributes backtraceAttributes = new BacktraceAttributes(null, null, onlyBuiltInAttributes: true);
			string bytes = new BacktraceCredentials(_configuration.GetValidServerUrl()).GetMinidumpSubmissionUrl().ToString();
			_captureNativeCrashes = Initialize(AndroidJNI.NewStringUTF(bytes), AndroidJNI.NewStringUTF(crashpadDatabasePath), AndroidJNI.NewStringUTF(text), AndroidJNIHelper.ConvertToJNIArray(backtraceAttributes.Attributes.Keys.ToArray()), AndroidJNIHelper.ConvertToJNIArray(backtraceAttributes.Attributes.Values.ToArray()));
			if (!_captureNativeCrashes)
			{
				Debug.LogWarning("Backtrace native integration status: Cannot initialize Crashpad client");
			}
			AddAttribute(AndroidJNI.NewStringUTF("error.type"), AndroidJNI.NewStringUTF("Crash"));
		}

		public void GetAttributes(Dictionary<string, string> result)
		{
			if (!_enabled)
			{
				return;
			}
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity"))
				{
					using (AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("getApplicationContext", Array.Empty<object>()))
					{
						using (AndroidJavaObject androidJavaObject3 = new AndroidJavaObject(_nativeAttributesPath))
						{
							AndroidJavaObject androidJavaObject4 = androidJavaObject3.Call<AndroidJavaObject>("GetAttributes", new object[1] { androidJavaObject2 }).Call<AndroidJavaObject>("entrySet", Array.Empty<object>()).Call<AndroidJavaObject>("iterator", Array.Empty<object>());
							while (androidJavaObject4.Call<bool>("hasNext", Array.Empty<object>()))
							{
								AndroidJavaObject androidJavaObject5 = androidJavaObject4.Call<AndroidJavaObject>("next", Array.Empty<object>());
								string key = androidJavaObject5.Call<string>("getKey", Array.Empty<object>());
								string value = androidJavaObject5.Call<string>("getValue", Array.Empty<object>());
								result[key] = value;
							}
						}
					}
				}
			}
		}

		public void HandleAnr(string gameObjectName, string callbackName)
		{
			if (!_handlerANR)
			{
				return;
			}
			try
			{
				_anrWatcher = new AndroidJavaObject(_anrPath, gameObjectName, callbackName);
			}
			catch (Exception ex)
			{
				Debug.LogWarning($"Cannot initialize ANR watchdog - reason: {ex.Message}");
				_enabled = false;
			}
			if (!_captureNativeCrashes)
			{
				return;
			}
			bool reported = false;
			_ = Thread.CurrentThread.ManagedThreadId;
			_anrThread = new Thread((ThreadStart)delegate
			{
				float num = 0f;
				while (true)
				{
					if (num == 0f)
					{
						num = _lastUpdateTime;
					}
					else if (num == _lastUpdateTime)
					{
						if (!reported)
						{
							reported = true;
							if (AndroidJNI.AttachCurrentThread() == 0)
							{
								AddAttribute(AndroidJNI.NewStringUTF("error.type"), AndroidJNI.NewStringUTF("Hang"));
								NativeReport(AndroidJNI.NewStringUTF("ANRException: Blocked thread detected."));
								SetAttribute("error.type", "Crash");
							}
						}
					}
					else
					{
						reported = false;
					}
					num = _lastUpdateTime;
					Thread.Sleep(5000);
				}
			});
			_anrThread.Start();
		}

		public void SetAttribute(string key, string value)
		{
			if (_captureNativeCrashes && !string.IsNullOrEmpty(key))
			{
				if (value == null)
				{
					value = string.Empty;
				}
				AddAttribute(AndroidJNI.NewStringUTF(key), AndroidJNI.NewStringUTF(value));
			}
		}

		public bool OnOOM()
		{
			if (!_enabled || _captureNativeCrashes)
			{
				return false;
			}
			SetAttribute("error.type", "Low Memory");
			NativeReport(AndroidJNI.NewStringUTF("OOMException: Out of memory detected."));
			SetAttribute("error.type", "Crash");
			return true;
		}

		public void UpdateClientTime(float time)
		{
			_lastUpdateTime = time;
		}

		public void Disable()
		{
			if (_anrThread != null)
			{
				_anrThread.Abort();
			}
		}
	}
}
