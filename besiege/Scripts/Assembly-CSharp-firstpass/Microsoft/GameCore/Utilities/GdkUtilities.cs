using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace Microsoft.GameCore.Utilities
{
	public class GdkUtilities
	{
		private static class RegUtil
		{
			[Flags]
			private enum RegSAM : uint
			{
				QUERY_VALUE = 1u,
				WOW64_64KEY = 0x100u,
				QUERY64 = 0x101u
			}

			public const uint HKEY_LOCAL_MACHINE = 2147483650u;

			private const uint FORMAT_MESSAGE_FROM_SYSTEM = 4096u;

			public static string GetRegKey(uint inHive, string inKeyName, string inPropertyName)
			{
				uint phkResult = 0u;
				try
				{
					uint lpdwDisposition;
					uint num = RegCreateKeyEx(inHive, inKeyName, 0u, null, 0u, 257u, 0u, out phkResult, out lpdwDisposition);
					if (num != 0)
					{
						Debug.LogError("Create/OpenKey (Query) failed " + num + ": " + FormatMessage(num));
						return string.Empty;
					}
					uint lpType = 0u;
					uint lpcbData = 1024u;
					StringBuilder stringBuilder = new StringBuilder(1024);
					num = RegQueryValueEx(phkResult, inPropertyName, 0u, ref lpType, stringBuilder, ref lpcbData);
					if (num != 0)
					{
						if (num != 2)
						{
							Debug.LogError("QueryKey failed " + num + ": " + FormatMessage(num));
						}
						return string.Empty;
					}
					return stringBuilder.ToString();
				}
				catch (Exception ex)
				{
					Debug.LogError("Failed to get key: " + ex.Message);
					return string.Empty;
				}
				finally
				{
					if (phkResult != 0)
					{
						uint num = RegCloseKey(phkResult);
						if (num != 0)
						{
							Debug.LogError("CloseKey (Query) failed " + num + ": " + FormatMessage(num));
						}
					}
				}
			}

			[DllImport("Advapi32.dll")]
			private static extern uint RegCreateKeyEx(uint hKey, string lpSubKey, uint reserved, string lpClass, uint dwOptions, uint samDesired, uint lpSecurityAttributes, out uint phkResult, out uint lpdwDisposition);

			[DllImport("Advapi32.dll")]
			private static extern uint RegCloseKey(uint hKey);

			[DllImport("Advapi32.dll")]
			private static extern uint RegQueryValueEx(uint hKey, string lpValueName, uint lpReserved, ref uint lpType, StringBuilder lpData, ref uint lpcbData);

			[DllImport("Kernel32.dll")]
			private static extern uint FormatMessage(uint dwFlags, uint lpSource, uint dwMessageId, uint dwLanguageId, StringBuilder lpBuffer, uint nSize, uint arguments);

			private static string FormatMessage(uint dwMessageId)
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				FormatMessage(4096u, 0u, dwMessageId, 0u, stringBuilder, 1024u, 0u);
				return stringBuilder.ToString();
			}
		}

		private static string _gdkVersion;

		private static string _xsapiLibPath;

		private static string _xCurlLibPath;

		private static string _gdkToolsPath;

		private static string _pluginDllPath;

		private static string _rootPluginPath;

		private static string _gameConfigPath;

		public static string XsapiLibName
		{
			get
			{
				return "Microsoft.Xbox.Services.141.GDK.C.Thunks.dll";
			}
		}

		public static string XCurlLibName
		{
			get
			{
				return "XCurl.dll";
			}
		}

		public static string GdkToolsPath
		{
			get
			{
				if (!File.Exists(_gdkToolsPath))
				{
					_gdkToolsPath = Path.Combine(RegUtil.GetRegKey(2147483650u, "SOFTWARE\\WOW6432Node\\Microsoft\\GDK", "InstallPath"), "bin");
				}
				return _gdkToolsPath;
			}
		}

		public static string GdkVersion
		{
			get
			{
				if (string.IsNullOrEmpty(_gdkVersion))
				{
					_xsapiLibPath = string.Empty;
					_xCurlLibPath = string.Empty;
					_gdkVersion = RegUtil.GetRegKey(2147483650u, "SOFTWARE\\WOW6432Node\\Microsoft\\GDK", "GRDKLatest");
				}
				return _gdkVersion;
			}
		}

		public static string XsapiLibPath
		{
			get
			{
				if (!File.Exists(_xsapiLibPath))
				{
					_xsapiLibPath = Path.Combine(Path.Combine(RegUtil.GetRegKey(2147483650u, "SOFTWARE\\WOW6432Node\\Microsoft\\GDK", "InstallPath"), GdkVersion), Path.Combine("GRDK\\ExtensionLibraries\\Xbox.Services.API.C\\DesignTime\\CommonConfiguration\\Neutral\\Lib\\Release", XsapiLibName));
				}
				return _xsapiLibPath;
			}
		}

		public static string XCurlLibPath
		{
			get
			{
				if (!File.Exists(_xCurlLibPath))
				{
					_xCurlLibPath = Path.Combine(Path.Combine(RegUtil.GetRegKey(2147483650u, "SOFTWARE\\WOW6432Node\\Microsoft\\GDK", "InstallPath"), GdkVersion), Path.Combine("GRDK\\ExtensionLibraries\\Xbox.XCurl.API\\Redist\\CommonConfiguration\\neutral", XCurlLibName));
				}
				return _xCurlLibPath;
			}
		}

		public static string RootPluginPath
		{
			get
			{
				if (!File.Exists(_rootPluginPath))
				{
					_rootPluginPath = Path.Combine(Application.dataPath, "Plugins/GDKPC").Replace("/", "\\");
				}
				return _rootPluginPath;
			}
		}

		public static string PluginDllPath
		{
			get
			{
				if (!File.Exists(_pluginDllPath))
				{
					_pluginDllPath = Path.Combine(RootPluginPath, "GDK-APIs\\Runtime\\DLLs");
				}
				return _pluginDllPath;
			}
		}

		public static string GameConfigPath
		{
			get
			{
				if (!File.Exists(_gameConfigPath))
				{
					string text = string.Empty;
					try
					{
						string path = Path.Combine(RootPluginPath, "GDK-Loca");
						string[] files = Directory.GetFiles(path, "MicrosoftGame.Config", SearchOption.TopDirectoryOnly);
						if (files.Length == 0)
						{
							Debug.Log("Searching for MicrosoftGame.Config in all asset folders.");
							files = Directory.GetFiles(Application.dataPath, "MicrosoftGame.Config", SearchOption.AllDirectories);
						}
						if (files.Length > 0)
						{
							text = files[0];
						}
						_gameConfigPath = text.Replace("/", "\\");
					}
					catch
					{
						Debug.LogError("No MicrosoftGame.Config found. Please re-import this plugin.");
					}
				}
				return _gameConfigPath;
			}
		}

		public static void PullGdkDlls()
		{
			string gdkVersion = GdkVersion;
			_gdkVersion = string.Empty;
			string path = Path.Combine(PluginDllPath, XsapiLibName);
			string path2 = Path.Combine(PluginDllPath, XCurlLibName);
			bool flag = GdkVersion.Length >= 4 && int.Parse(GdkVersion.Substring(0, 4)) >= 2110;
			if (gdkVersion.Equals(GdkVersion) && File.Exists(path) && (!flag || File.Exists(path2)))
			{
				return;
			}
			if (!File.Exists(XsapiLibPath))
			{
				Debug.LogError("Could not find the GDK DLLs. Make sure you have the Microsoft GDK installed.");
				return;
			}
			File.Copy(XsapiLibPath, Path.Combine(PluginDllPath, XsapiLibName), true);
			if (File.Exists(XCurlLibPath))
			{
				File.Copy(XCurlLibPath, Path.Combine(PluginDllPath, XCurlLibName), true);
			}
		}
	}
}
