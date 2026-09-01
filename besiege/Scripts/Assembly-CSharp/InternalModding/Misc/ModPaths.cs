using System;
using System.IO;
using InternalModding.Mods;

namespace InternalModding.Misc
{
	public static class ModPaths
	{
		public static string GetFilePath(ModInfo mod, string path, bool relativeToResources = false)
		{
			return GetFilePath(Path.Combine(mod.Directory, (!relativeToResources) ? string.Empty : "Resources"), path);
		}

		public static string GetFilePathData(ModInfo mod, string path)
		{
			return GetFilePath(Path.Combine(GetDataDirectory(), string.Concat(mod.Name.Replace(" ", string.Empty), "_", mod.Id, "/")), path);
		}

		public static string GetDataDirectoryMod(ModInfo mod)
		{
			return Path.Combine(GetDataDirectory(), string.Concat(mod.Name.Replace(" ", string.Empty), "_", mod.Id, "/"));
		}

		public static string GetFilePath(string baseDir, string path)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(baseDir);
			directoryInfo = directoryInfo.Parent.CreateSubdirectory(directoryInfo.Name);
			baseDir = baseDir.Replace("\\", "/");
			path = path.Replace("\\", "/");
			string text = ((!(path == "/")) ? Path.Combine(baseDir, path) : (baseDir + "/"));
			DirectoryInfo directoryInfo2;
			if (!text.EndsWith("/") && !text.EndsWith("\\"))
			{
				FileInfo fileInfo = new FileInfo(text);
				directoryInfo2 = fileInfo.Directory;
			}
			else
			{
				DirectoryInfo directoryInfo3 = new DirectoryInfo(text);
				directoryInfo2 = directoryInfo3;
			}
			DirectoryInfo directoryInfo4 = directoryInfo2;
			while (directoryInfo4 != null && !directoryInfo4.FullName.Equals(directoryInfo.FullName, StringComparison.OrdinalIgnoreCase) && !directoryInfo4.FullName.Equals(directoryInfo.FullName + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
			{
				directoryInfo4 = directoryInfo4.Parent;
			}
			if (directoryInfo4 == null)
			{
				throw new Exception("Path is not in mod directory! (" + path + ")");
			}
			return text;
		}

		public static string GetDataDirectory()
		{
			string text = Path.Combine(ModManager.DefaultModPath, "Data/");
			Directory.CreateDirectory(text);
			return text;
		}

		public static string GetAssemblyDirectory()
		{
			string text = Path.Combine(ModManager.DefaultModPath, ".CompiledAssemblies/");
			Directory.CreateDirectory(text);
			return text;
		}

		public static string GetAssemblyPath(ModContainer mod, string assemblyName)
		{
			return Path.Combine(GetAssemblyDirectory(), string.Concat(mod.Info.Id, "_", assemblyName, ".dll"));
		}
	}
}
