using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Backtrace.Unity.Common;
using Backtrace.Unity.Extensions;
using Backtrace.Unity.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Backtrace.Unity.Model.JsonData
{
	public class BacktraceAttributes
	{
		public readonly Dictionary<string, string> Attributes;

		private static string _machineId;

		private static string MachineId
		{
			get
			{
				if (string.IsNullOrEmpty(_machineId))
				{
					_machineId = GenerateMachineId();
				}
				return _machineId;
			}
		}

		public BacktraceAttributes(BacktraceReport report, Dictionary<string, string> clientAttributes, bool onlyBuiltInAttributes = false)
		{
			if (clientAttributes == null)
			{
				clientAttributes = new Dictionary<string, string>();
			}
			Attributes = clientAttributes;
			if (report != null)
			{
				if (report.Attributes != null)
				{
					foreach (KeyValuePair<string, string> attribute in report.Attributes)
					{
						Attributes[attribute.Key] = attribute.Value;
					}
				}
				SetExceptionAttributes(report);
			}
			SetLibraryAttributes(report);
			SetMachineAttributes(onlyBuiltInAttributes);
			SetProcessAttributes(onlyBuiltInAttributes);
			SetSceneInformation(onlyBuiltInAttributes);
		}

		private BacktraceAttributes()
		{
		}

		public BacktraceJObject ToJson()
		{
			return new BacktraceJObject(Attributes);
		}

		private void SetScriptingBackend()
		{
			Attributes["api.compatibility"] = ".NET Framework 4.5";
			Attributes["scripting.backend"] = "Mono";
		}

		private void SetLibraryAttributes(BacktraceReport report)
		{
			if (report != null)
			{
				if (!string.IsNullOrEmpty(report.Factor))
				{
					Attributes["_mod_factor"] = report.Factor;
				}
				if (!string.IsNullOrEmpty(report.Fingerprint))
				{
					Attributes["_mod_fingerprint"] = report.Fingerprint;
				}
			}
			Attributes["guid"] = MachineId;
			Attributes["backtrace.version"] = "3.3.0";
			SetScriptingBackend();
			Attributes["application"] = Application.productName;
			Attributes["application.version"] = Application.version;
			Attributes["application.url"] = Application.absoluteURL;
			Attributes["application.company.name"] = Application.companyName;
			Attributes["application.data_path"] = Application.dataPath;
			Attributes["application.id"] = Application.identifier;
			Attributes["application.installer.name"] = Application.installerName;
			Attributes["application.internet_reachability"] = Application.internetReachability.ToString();
			Attributes["application.editor"] = Application.isEditor.ToString();
			Attributes["application.focused"] = Application.isFocused.ToString();
			Attributes["application.mobile"] = Application.isMobilePlatform.ToString();
			Attributes["application.playing"] = Application.isPlaying.ToString();
			Attributes["application.background"] = Application.runInBackground.ToString();
			Attributes["application.sandboxType"] = Application.sandboxType.ToString();
			Attributes["application.system.language"] = Application.systemLanguage.ToString();
			Attributes["application.unity.version"] = Application.unityVersion;
			Attributes["application.debug"] = Debug.isDebugBuild.ToString();
			Attributes["application.temporary_cache"] = Application.temporaryCachePath;
		}

		private static string GenerateMachineId()
		{
			if (SystemInfo.deviceUniqueIdentifier != "n/a")
			{
				return SystemInfo.deviceUniqueIdentifier;
			}
			NetworkInterface networkInterface = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault((NetworkInterface n) => n.OperationalStatus == OperationalStatus.Up);
			PhysicalAddress physicalAddress = null;
			string text = null;
			if (networkInterface == null || (physicalAddress = networkInterface.GetPhysicalAddress()) == null || string.IsNullOrEmpty(text = physicalAddress.ToString()))
			{
				return Guid.NewGuid().ToString();
			}
			return GuidExtensions.FromLong(Convert.ToInt64(text.Replace(":", string.Empty), 16)).ToString();
		}

		internal void SetExceptionAttributes(BacktraceReport report)
		{
			if (report == null)
			{
				return;
			}
			Attributes["error.message"] = (report.ExceptionTypeReport ? report.Exception.Message : report.Message);
			string key = "error.type";
			if (!report.ExceptionTypeReport)
			{
				Attributes[key] = "Message";
			}
			else if (report.Exception is BacktraceUnhandledException)
			{
				string classifier = (report.Exception as BacktraceUnhandledException).Classifier;
				if (classifier == "ANRException")
				{
					Attributes[key] = "Hang";
				}
				else if (classifier == "OOMException")
				{
					Attributes[key] = "Low Memory";
				}
				else
				{
					Attributes[key] = "Unhandled exception";
				}
			}
			else
			{
				Attributes[key] = "Exception";
			}
		}

		internal void SetSceneInformation(bool onlyBuiltInAttributes = false)
		{
			if (SceneManager.sceneCountInBuildSettings > 0)
			{
				Attributes["scene.count.build"] = SceneManager.sceneCountInBuildSettings.ToString();
			}
			Attributes["scene.count"] = SceneManager.sceneCount.ToString();
			if (!onlyBuiltInAttributes)
			{
				Scene activeScene = SceneManager.GetActiveScene();
				Attributes["scene.active"] = activeScene.name;
				Attributes["scene.buildIndex"] = activeScene.buildIndex.ToString();
				Attributes["scene.handle"] = activeScene.handle.ToString();
				Attributes["scene.isDirty"] = activeScene.isDirty.ToString();
				Attributes["scene.isLoaded"] = activeScene.isLoaded.ToString();
				Attributes["scene.name"] = activeScene.name;
				Attributes["scene.path"] = activeScene.path;
			}
		}

		private void SetProcessAttributes(bool onlyBuiltInAttributes = false)
		{
			if (!onlyBuiltInAttributes)
			{
				Attributes["gc.heap.used"] = GC.GetTotalMemory(forceFullCollection: false).ToString();
				Attributes["process.age"] = Math.Round(Time.realtimeSinceStartup).ToString();
			}
		}

		private void SetGraphicCardInformation()
		{
			Attributes["graphic.id"] = SystemInfo.graphicsDeviceID.ToString();
			Attributes["graphic.name"] = SystemInfo.graphicsDeviceName;
			Attributes["graphic.type"] = SystemInfo.graphicsDeviceType.ToString();
			Attributes["graphic.vendor"] = SystemInfo.graphicsDeviceVendor;
			Attributes["graphic.vendor.id"] = SystemInfo.graphicsDeviceVendorID.ToString();
			Attributes["graphic.driver.version"] = SystemInfo.graphicsDeviceVersion;
			Attributes["graphic.memory"] = SystemInfo.graphicsMemorySize.ToString();
			Attributes["graphic.multithreaded"] = SystemInfo.graphicsMultiThreaded.ToString();
			Attributes["graphic.shader"] = SystemInfo.graphicsShaderLevel.ToString();
			Attributes["graphic.topUv"] = SystemInfo.graphicsUVStartsAtTop.ToString();
		}

		private void SetMachineAttributes(bool onlyBuiltInAttributes = false)
		{
			if (onlyBuiltInAttributes)
			{
				float num = ((SystemInfo.batteryLevel == -1f) ? (-1f) : (SystemInfo.batteryLevel * 100f));
				Attributes["battery.level"] = num.ToString();
				Attributes["battery.status"] = SystemInfo.batteryStatus.ToString();
			}
			if (SystemInfo.deviceModel != "n/a")
			{
				Attributes["device.model"] = SystemInfo.deviceModel;
				Attributes["device.name"] = SystemInfo.deviceName;
				Attributes["device.type"] = SystemInfo.deviceType.ToString();
			}
			SetGraphicCardInformation();
			string value = SystemHelper.CpuArchitecture();
			if (!string.IsNullOrEmpty(value))
			{
				Attributes["uname.machine"] = value;
			}
			Attributes["uname.sysname"] = SystemHelper.Name();
			Attributes["uname.version"] = Environment.OSVersion.Version.ToString();
			Attributes["uname.fullname"] = SystemInfo.operatingSystem;
			Attributes["uname.family"] = SystemInfo.operatingSystemFamily.ToString();
			Attributes["cpu.count"] = SystemInfo.processorCount.ToString();
			Attributes["cpu.frequency"] = SystemInfo.processorFrequency.ToString();
			Attributes["cpu.brand"] = SystemInfo.processorType;
			Attributes["audio.supported"] = SystemInfo.supportsAudio.ToString();
			int num2 = Environment.TickCount;
			if (num2 <= 0)
			{
				num2 = int.MaxValue;
			}
			Attributes["cpu.boottime"] = num2.ToString();
			Attributes["hostname"] = Environment.MachineName;
			if (SystemInfo.systemMemorySize != 0)
			{
				Attributes["vm.rss.size"] = ((long)SystemInfo.systemMemorySize * 1048576L).ToString();
			}
		}
	}
}
