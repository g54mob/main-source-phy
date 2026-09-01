using ModIO.PlatformIOCallbacks;
using UnityEngine;

namespace ModIO
{
	public static class DataStorage
	{
		public static readonly IPlatformIO PLATFORM_IO;

		public static string INSTALLATION_DIRECTORY
		{
			get
			{
				return PLATFORM_IO.InstallationDirectory;
			}
		}

		public static string CACHE_DIRECTORY
		{
			get
			{
				return PLATFORM_IO.CacheDirectory;
			}
		}

		static DataStorage()
		{
			PLATFORM_IO = new SystemIOWrapper();
		}

		public static void ReadFile(string path, ReadFileCallback onComplete)
		{
			PLATFORM_IO.ReadFile(path, onComplete);
		}

		public static void ReadJSONFile<T>(string path, ReadJSONFileCallback<T> onComplete)
		{
			PLATFORM_IO.ReadFile(path, delegate(string p, bool success, byte[] data)
			{
				T jsonObject;
				if (success)
				{
					success = IOUtilities.TryParseUTF8JSONData<T>(data, out jsonObject);
					if (!success)
					{
						Debug.LogWarning("[mod.io] Failed parse file content as JSON Object.\nFile: " + path + "\n\n");
					}
				}
				else
				{
					jsonObject = default(T);
				}
				if (onComplete != null)
				{
					onComplete(path, success, jsonObject);
				}
			});
		}

		public static void WriteFile(string path, byte[] data, WriteFileCallback onComplete)
		{
			PLATFORM_IO.WriteFile(path, data, onComplete);
		}

		public static void WriteJSONFile<T>(string path, T jsonObject, WriteFileCallback onComplete)
		{
			byte[] array = IOUtilities.GenerateUTF8JSONData(jsonObject);
			if (array != null && array.Length > 0)
			{
				PLATFORM_IO.WriteFile(path, array, onComplete);
				return;
			}
			Debug.LogWarning("[mod.io] Failed create JSON representation of object before writing file.\nFile: " + path + "\n\n");
			if (onComplete != null)
			{
				onComplete(path, false);
			}
		}

		public static void DeleteFile(string path, DeleteFileCallback onComplete)
		{
			PLATFORM_IO.DeleteFile(path, onComplete);
		}

		public static void MoveFile(string source, string destination, MoveFileCallback onComplete)
		{
			PLATFORM_IO.MoveFile(source, destination, onComplete);
		}

		public static void GetFileExists(string path, GetFileExistsCallback onComplete)
		{
			PLATFORM_IO.GetFileExists(path, onComplete);
		}

		public static void GetFileSizeAndHash(string path, GetFileSizeAndHashCallback onComplete)
		{
			PLATFORM_IO.GetFileSizeAndHash(path, onComplete);
		}

		public static void GetFiles(string path, string nameFilter, bool recurseSubdirectories, GetFilesCallback onComplete)
		{
			PLATFORM_IO.GetFiles(path, nameFilter, recurseSubdirectories, onComplete);
		}

		public static void CreateDirectory(string path, CreateDirectoryCallback onComplete)
		{
			PLATFORM_IO.CreateDirectory(path, onComplete);
		}

		public static void DeleteDirectory(string path, DeleteDirectoryCallback onComplete)
		{
			PLATFORM_IO.DeleteDirectory(path, onComplete);
		}

		public static void MoveDirectory(string source, string destination, MoveDirectoryCallback onComplete)
		{
			PLATFORM_IO.MoveDirectory(source, destination, onComplete);
		}

		public static void GetDirectoryExists(string path, GetDirectoryExistsCallback onComplete)
		{
			PLATFORM_IO.GetDirectoryExists(path, onComplete);
		}

		public static void GetDirectories(string path, GetDirectoriesCallback onComplete)
		{
			PLATFORM_IO.GetDirectories(path, onComplete);
		}
	}
}
