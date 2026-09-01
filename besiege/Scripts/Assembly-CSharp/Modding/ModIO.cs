using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using InternalModding.Assemblies;
using InternalModding.Common;
using InternalModding.Misc;
using InternalModding.Mods;
using Modding.Serialization;

namespace Modding
{
	public static class ModIO
	{
		public static bool ExistsDirectory(string path, bool data = false)
		{
			return Directory.Exists(GetFullPath(Assembly.GetCallingAssembly(), path, data));
		}

		public static void DeleteDirectory(string path, bool recursive, bool data = false)
		{
			Directory.Delete(GetFullPath(Assembly.GetCallingAssembly(), path, data), recursive);
		}

		public static void CreateDirectory(string path, bool data = false)
		{
			Directory.CreateDirectory(GetFullPath(Assembly.GetCallingAssembly(), path, data));
		}

		public static string[] GetDirectories(string path, bool data = false)
		{
			Assembly assembly = Assembly.GetCallingAssembly();
			return (from d in Directory.GetDirectories(GetFullPath(assembly, path, data))
				select MakeRelativePath(assembly, d, data)).ToArray();
		}

		public static void OpenFolderInFileBrowser(string path, bool data = false)
		{
			string fullPath = GetFullPath(Assembly.GetCallingAssembly(), path, data);
			fullPath = fullPath.TrimEnd('\\', '/');
			Process.Start(fullPath);
		}

		public static bool ExistsFile(string filePath, bool data = false)
		{
			return File.Exists(GetFullPath(Assembly.GetCallingAssembly(), filePath, data));
		}

		public static void DeleteFile(string filePath, bool data = false)
		{
			File.Delete(GetFullPath(Assembly.GetCallingAssembly(), filePath, data));
		}

		public static string[] GetFiles(string dirPath, bool data = false)
		{
			Assembly assembly = Assembly.GetCallingAssembly();
			return (from f in Directory.GetFiles(GetFullPath(assembly, dirPath, data))
				select MakeRelativePath(assembly, f, data)).ToArray();
		}

		public static string[] GetFiles(string dirPath, string searchPattern, bool data = false)
		{
			Assembly assembly = Assembly.GetCallingAssembly();
			return (from f in Directory.GetFiles(GetFullPath(assembly, dirPath, data), searchPattern)
				select MakeRelativePath(assembly, f, data)).ToArray();
		}

		public static Stream Open(string filePath, FileMode mode, bool data = false, FileAccess access = FileAccess.ReadWrite, FileShare share = FileShare.None)
		{
			return File.Open(GetFullPath(Assembly.GetCallingAssembly(), filePath, data), mode, access, share);
		}

		public static Stream OpenRead(string filePath, bool data = false)
		{
			return File.OpenRead(GetFullPath(Assembly.GetCallingAssembly(), filePath, data));
		}

		public static TextReader OpenText(string filePath, bool data = false)
		{
			return File.OpenText(GetFullPath(Assembly.GetCallingAssembly(), filePath, data));
		}

		public static TextWriter CreateText(string filePath, bool data = false)
		{
			return File.CreateText(GetFullPath(Assembly.GetCallingAssembly(), filePath, data));
		}

		public static TextWriter AppendText(string filePath, bool data = false)
		{
			return File.AppendText(GetFullPath(Assembly.GetCallingAssembly(), filePath, data));
		}

		public static T DeserializeXml<T>(string filePath, bool data = false, bool validate = true) where T : Element
		{
			return ModXmlLoader.Deserialize<T>(GetFullPath(Assembly.GetCallingAssembly(), filePath, data), validate);
		}

		public static void SerializeXml<T>(T obj, string filePath, bool data = false) where T : Element
		{
			using (StreamWriter textWriter = File.CreateText(GetFullPath(Assembly.GetCallingAssembly(), filePath, data)))
			{
				new XmlSerializer(typeof(T)).Serialize(textWriter, obj);
			}
		}

		public static string ReadAllText(string filePath, bool data = false)
		{
			return File.ReadAllText(GetFullPath(Assembly.GetCallingAssembly(), filePath, data), Encoding.UTF8);
		}

		public static string ReadAllText(string filePath, Encoding encoding, bool data = false)
		{
			return File.ReadAllText(GetFullPath(Assembly.GetCallingAssembly(), filePath, data), encoding);
		}

		public static string[] ReadAllLines(string filePath, bool data = false)
		{
			return File.ReadAllLines(GetFullPath(Assembly.GetCallingAssembly(), filePath, data), Encoding.UTF8);
		}

		public static string[] ReadAllLines(string filePath, Encoding encoding, bool data = false)
		{
			return File.ReadAllLines(GetFullPath(Assembly.GetCallingAssembly(), filePath, data), encoding);
		}

		public static byte[] ReadAllBytes(string filePath, bool data = false)
		{
			return File.ReadAllBytes(GetFullPath(Assembly.GetCallingAssembly(), filePath, data));
		}

		public static void WriteAllText(string filePath, string text, bool data = false)
		{
			File.WriteAllText(GetFullPath(Assembly.GetCallingAssembly(), filePath, data), text);
		}

		public static void WriteAllLines(string filePath, string[] lines, bool data = false)
		{
			File.WriteAllLines(GetFullPath(Assembly.GetCallingAssembly(), filePath, data), lines);
		}

		public static void WriteAllBytes(string filePath, byte[] bytes, bool data = false)
		{
			File.WriteAllBytes(GetFullPath(Assembly.GetCallingAssembly(), filePath, data), bytes);
		}

		public static void DownloadFile(string url, string filePath, AsyncCompletedEventHandler downloadCompleted = null, DownloadProgressChangedEventHandler progressChanged = null, bool data = false)
		{
			if (url.StartsWith("file://"))
			{
				throw new InvalidOperationException("Tried to use Download* method to access a local file.");
			}
			string fullPath = GetFullPath(Assembly.GetCallingAssembly(), filePath, data);
			using (WebClient webClient = new WebClient())
			{
				if (downloadCompleted != null)
				{
					webClient.DownloadFileCompleted += downloadCompleted;
				}
				if (progressChanged != null)
				{
					webClient.DownloadProgressChanged += progressChanged;
				}
				webClient.DownloadFile(url, fullPath);
			}
		}

		public static void DownloadFileAsync(Uri uri, string filePath, AsyncCompletedEventHandler downloadComplete = null, DownloadProgressChangedEventHandler progressChanged = null, bool data = false)
		{
			if (uri.Scheme == "file")
			{
				throw new InvalidOperationException("Tried to use Download* method to access a local file.");
			}
			string fullPath = GetFullPath(Assembly.GetCallingAssembly(), filePath, data);
			using (WebClient webClient = new WebClient())
			{
				if (downloadComplete != null)
				{
					webClient.DownloadFileCompleted += downloadComplete;
				}
				if (progressChanged != null)
				{
					webClient.DownloadProgressChanged += progressChanged;
				}
				webClient.DownloadFileAsync(uri, fullPath);
			}
		}

		public static string DownloadString(string url)
		{
			if (url.StartsWith("file://"))
			{
				throw new InvalidOperationException("Tried to use Download* method to access a local file.");
			}
			using (WebClient webClient = new WebClient())
			{
				return webClient.DownloadString(url);
			}
		}

		public static void DownloadStringAsync(Uri uri, DownloadStringCompletedEventHandler downloadComplete, DownloadProgressChangedEventHandler progressChanged = null)
		{
			if (uri.Scheme == "file://")
			{
				throw new InvalidOperationException("Tried to use Download* method to access a local file.");
			}
			using (WebClient webClient = new WebClient())
			{
				webClient.DownloadStringCompleted += downloadComplete;
				if (progressChanged != null)
				{
					webClient.DownloadProgressChanged += progressChanged;
				}
				webClient.DownloadStringAsync(uri);
			}
		}

		public static byte[] DownloadData(string url)
		{
			if (url.StartsWith("file://"))
			{
				throw new InvalidOperationException("Tried to use Download* method to access a local file.");
			}
			using (WebClient webClient = new WebClient())
			{
				return webClient.DownloadData(url);
			}
		}

		public static void DownloadDataAsync(Uri uri, DownloadDataCompletedEventHandler downloadComplete, DownloadProgressChangedEventHandler progressChanged = null)
		{
			if (uri.Scheme == "file")
			{
				throw new InvalidOperationException("Tried to use Download* method to access a local file.");
			}
			using (WebClient webClient = new WebClient())
			{
				webClient.DownloadDataCompleted += downloadComplete;
				if (progressChanged != null)
				{
					webClient.DownloadProgressChanged += progressChanged;
				}
				webClient.DownloadDataAsync(uri);
			}
		}

		private static string GetFullPath(Assembly assembly, string file, bool data)
		{
			ModContainer modByAssembly = AssemblyLoader.GetModByAssembly(assembly);
			if (modByAssembly == null)
			{
				throw new InvalidOperationException("ModIO called from an assembly not listed in the mod manifest.");
			}
			return (!data) ? ModPaths.GetFilePath(modByAssembly.Info, file) : ModPaths.GetFilePathData(modByAssembly.Info, file);
		}

		private static string MakeRelativePath(Assembly assembly, string absolutePath, bool data)
		{
			ModContainer modByAssembly = AssemblyLoader.GetModByAssembly(assembly);
			if (modByAssembly == null)
			{
				throw new InvalidOperationException("ModIO called from an assembly not listed in the mod manifest.");
			}
			string text = ((!data) ? modByAssembly.Info.Directory : ModPaths.GetDataDirectoryMod(modByAssembly.Info));
			text = text.Replace("\\", "/");
			if (!text.StartsWith("file://"))
			{
				text = "file://" + text;
			}
			if (!text.EndsWith("/"))
			{
				text += "/";
			}
			absolutePath = absolutePath.Replace("\\", "/");
			if (!absolutePath.StartsWith("file://"))
			{
				absolutePath = "file://" + absolutePath;
			}
			Uri uri = new Uri(text, UriKind.Absolute);
			Uri uri2 = new Uri(absolutePath, UriKind.Absolute);
			Uri uri3 = uri.MakeRelativeUri(uri2);
			return Uri.UnescapeDataString(uri3.ToString());
		}
	}
}
