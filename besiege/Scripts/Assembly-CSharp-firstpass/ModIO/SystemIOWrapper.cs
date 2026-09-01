using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using ModIO.PlatformIOCallbacks;
using ModIO.UserDataIOCallbacks;
using UnityEngine;

namespace ModIO
{
	public class SystemIOWrapper : IUserDataIO<string>, IUserDataIO<int>, IPlatformIO, IUserDataIO
	{
		protected string rootUserDir;

		public string InstallationDirectory { get; private set; }

		public string CacheDirectory { get; private set; }

		public string UserDirectory { get; private set; }

		public SystemIOWrapper()
			: this(PluginSettings.data.installationDirectory, PluginSettings.data.cacheDirectory, PluginSettings.data.userDirectory)
		{
		}

		protected SystemIOWrapper(string installDir, string cacheDir, string rootUserDir)
		{
			InstallationDirectory = installDir;
			CacheDirectory = cacheDir;
			UserDirectory = rootUserDir;
			this.rootUserDir = rootUserDir;
		}

		void IPlatformIO.ReadFile(string path, ModIO.PlatformIOCallbacks.ReadFileCallback callback)
		{
			byte[] data = null;
			bool success = ReadFile(path, out data);
			if (callback != null)
			{
				callback(path, success, data);
			}
		}

		void IPlatformIO.WriteFile(string path, byte[] data, ModIO.PlatformIOCallbacks.WriteFileCallback callback)
		{
			bool success = WriteFile(path, data);
			if (callback != null)
			{
				callback(path, success);
			}
		}

		void IPlatformIO.DeleteFile(string path, ModIO.PlatformIOCallbacks.DeleteFileCallback callback)
		{
			bool success = DeleteFile(path);
			if (callback != null)
			{
				callback(path, success);
			}
		}

		void IPlatformIO.MoveFile(string source, string destination, MoveFileCallback callback)
		{
			bool success = MoveFile(source, destination);
			if (callback != null)
			{
				callback(source, destination, success);
			}
		}

		void IPlatformIO.GetFileExists(string path, ModIO.PlatformIOCallbacks.GetFileExistsCallback callback)
		{
			bool fileExists = GetFileExists(path);
			if (callback != null)
			{
				callback(path, fileExists);
			}
		}

		void IPlatformIO.GetFileSizeAndHash(string path, ModIO.PlatformIOCallbacks.GetFileSizeAndHashCallback callback)
		{
			long byteCount;
			string md5Hash;
			bool fileSizeAndHash = GetFileSizeAndHash(path, out byteCount, out md5Hash);
			if (callback != null)
			{
				callback(path, fileSizeAndHash, byteCount, md5Hash);
			}
		}

		void IPlatformIO.GetFiles(string path, string nameFilter, bool recurseSubdirectories, GetFilesCallback callback)
		{
			IList<string> files = GetFiles(path, nameFilter, recurseSubdirectories);
			if (callback != null)
			{
				callback(path, files != null, files);
			}
		}

		void IPlatformIO.CreateDirectory(string path, CreateDirectoryCallback callback)
		{
			bool success = CreateDirectory(path);
			if (callback != null)
			{
				callback(path, success);
			}
		}

		void IPlatformIO.DeleteDirectory(string path, DeleteDirectoryCallback callback)
		{
			bool success = DeleteDirectory(path);
			if (callback != null)
			{
				callback(path, success);
			}
		}

		void IPlatformIO.MoveDirectory(string source, string destination, MoveDirectoryCallback callback)
		{
			bool success = MoveDirectory(source, destination);
			if (callback != null)
			{
				callback(source, destination, success);
			}
		}

		void IPlatformIO.GetDirectoryExists(string path, GetDirectoryExistsCallback callback)
		{
			bool directoryExists = GetDirectoryExists(path);
			if (callback != null)
			{
				callback(path, directoryExists);
			}
		}

		void IPlatformIO.GetDirectories(string path, GetDirectoriesCallback callback)
		{
			IList<string> directories = GetDirectories(path);
			if (callback != null)
			{
				callback(path, directories != null, directories);
			}
		}

		void IUserDataIO.ClearActiveUserData(ClearActiveUserDataCallback callback)
		{
			bool success = DeleteDirectory(UserDirectory);
			if (callback != null)
			{
				callback(success);
			}
		}

		void IUserDataIO.ReadFile(string relativePath, ModIO.UserDataIOCallbacks.ReadFileCallback callback)
		{
			string path = IOUtilities.CombinePath(UserDirectory, relativePath);
			byte[] data;
			bool success = ReadFile(path, out data);
			if (callback != null)
			{
				callback(relativePath, success, data);
			}
		}

		void IUserDataIO.WriteFile(string relativePath, byte[] data, ModIO.UserDataIOCallbacks.WriteFileCallback callback)
		{
			string path = IOUtilities.CombinePath(UserDirectory, relativePath);
			bool success = WriteFile(path, data);
			if (callback != null)
			{
				callback(relativePath, success);
			}
		}

		void IUserDataIO.DeleteFile(string relativePath, ModIO.UserDataIOCallbacks.DeleteFileCallback callback)
		{
			string path = IOUtilities.CombinePath(UserDirectory, relativePath);
			bool success = DeleteFile(path);
			if (callback != null)
			{
				callback(relativePath, success);
			}
		}

		public virtual void InitializeForDefaultUser(Action<bool> callback)
		{
			SetActiveUser(null, delegate(string userId, bool success)
			{
				if (callback != null)
				{
					callback(success);
				}
			});
		}

		public virtual void SetActiveUser(string platformUserId, SetActiveUserCallback<string> callback)
		{
			UserDirectory = GenerateActiveUserDirectory(platformUserId);
			bool success = CreateDirectory(UserDirectory);
			if (callback != null)
			{
				callback(platformUserId, success);
			}
		}

		public virtual void SetActiveUser(int platformUserId, SetActiveUserCallback<int> callback)
		{
			UserDirectory = GenerateActiveUserDirectory(platformUserId.ToString("x8"));
			bool success = CreateDirectory(UserDirectory);
			if (callback != null)
			{
				callback(platformUserId, success);
			}
		}

		protected virtual string GenerateActiveUserDirectory(string platformUserId)
		{
			string result = rootUserDir;
			if (!string.IsNullOrEmpty(platformUserId))
			{
				string text = IOUtilities.MakeValidFileName(platformUserId);
				result = IOUtilities.CombinePath(rootUserDir, text);
			}
			return result;
		}

		public virtual bool ReadFile(string path, out byte[] data)
		{
			if (!File.Exists(path))
			{
				data = null;
				return false;
			}
			try
			{
				data = File.ReadAllBytes(path);
				return true;
			}
			catch (Exception e)
			{
				string text = "[mod.io] Failed to read file.\nFile: " + path + "\n\n";
				Debug.LogWarning(text + Utility.GenerateExceptionDebugString(e));
				data = null;
				return false;
			}
		}

		public virtual bool WriteFile(string path, byte[] data)
		{
			try
			{
				Directory.CreateDirectory(Path.GetDirectoryName(path));
				File.WriteAllBytes(path, data);
				return true;
			}
			catch (Exception e)
			{
				string text = "[mod.io] Failed to write file.\nFile: " + path + "\n\n";
				Debug.LogWarning(text + Utility.GenerateExceptionDebugString(e));
				return false;
			}
		}

		public virtual bool DeleteFile(string path)
		{
			try
			{
				if (File.Exists(path))
				{
					File.Delete(path);
				}
				return true;
			}
			catch (Exception e)
			{
				string text = "[mod.io] Failed to delete file.\nFile: " + path + "\n\n";
				Debug.LogWarning(text + Utility.GenerateExceptionDebugString(e));
				return false;
			}
		}

		public virtual bool MoveFile(string source, string destination)
		{
			if (string.IsNullOrEmpty(source))
			{
				Debug.Log("[mod.io] Failed to move file. source is NullOrEmpty.");
				return false;
			}
			if (string.IsNullOrEmpty(destination))
			{
				Debug.Log("[mod.io] Failed to move file. destination is NullOrEmpty.");
				return false;
			}
			if (!DeleteFile(destination))
			{
				return false;
			}
			try
			{
				File.Move(source, destination);
				return true;
			}
			catch (Exception e)
			{
				string text = "Failed to move file.\nSource File: " + source + "\nDestination: " + destination + "\n\n";
				Debug.LogWarning(text + Utility.GenerateExceptionDebugString(e));
				return false;
			}
		}

		public virtual bool GetFileExists(string path)
		{
			return File.Exists(path);
		}

		public virtual long GetFileSize(string path)
		{
			if (!File.Exists(path))
			{
				return -1L;
			}
			try
			{
				FileInfo fileInfo = new FileInfo(path);
				return fileInfo.Length;
			}
			catch (Exception e)
			{
				string text = "[mod.io] Failed to get file size.\nFile: " + path + "\n\n";
				Debug.LogWarning(text + Utility.GenerateExceptionDebugString(e));
				return -1L;
			}
		}

		public virtual bool GetFileSizeAndHash(string path, out long byteCount, out string md5Hash)
		{
			byteCount = -1L;
			md5Hash = null;
			if (!File.Exists(path))
			{
				return false;
			}
			try
			{
				byteCount = new FileInfo(path).Length;
			}
			catch (Exception e)
			{
				string text = "[mod.io] Failed to get file size.\nFile: " + path + "\n\n";
				Debug.LogWarning(text + Utility.GenerateExceptionDebugString(e));
				byteCount = -1L;
				return false;
			}
			try
			{
				using (MD5 mD = MD5.Create())
				{
					using (FileStream inputStream = File.OpenRead(path))
					{
						byte[] array = mD.ComputeHash(inputStream);
						md5Hash = BitConverter.ToString(array).Replace("-", string.Empty).ToLowerInvariant();
					}
				}
			}
			catch (Exception e2)
			{
				string text2 = "[mod.io] Failed to calculate file hash.\nFile: " + path + "\n\n";
				Debug.LogWarning(text2 + Utility.GenerateExceptionDebugString(e2));
				md5Hash = null;
				return false;
			}
			return true;
		}

		public virtual IList<string> GetFiles(string path, string nameFilter, bool recurseSubdirectories)
		{
			if (!Directory.Exists(path))
			{
				return null;
			}
			SearchOption searchOption = (recurseSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
			if (nameFilter == null)
			{
				nameFilter = "*";
			}
			return Directory.GetFiles(path, nameFilter, searchOption);
		}

		public virtual bool CreateDirectory(string path)
		{
			try
			{
				Directory.CreateDirectory(path);
				return true;
			}
			catch (Exception e)
			{
				string text = "[mod.io] Failed to create directory.\nDirectory: " + path + "\n\n";
				Debug.LogWarning(text + Utility.GenerateExceptionDebugString(e));
				return true;
			}
		}

		public virtual bool DeleteDirectory(string path)
		{
			try
			{
				if (Directory.Exists(path))
				{
					Directory.Delete(path, true);
				}
				return true;
			}
			catch (Exception e)
			{
				string text = "[mod.io] Failed to delete directory.\nDirectory: " + path + "\n\n";
				Debug.LogWarning(text + Utility.GenerateExceptionDebugString(e));
				return false;
			}
		}

		public virtual bool MoveDirectory(string source, string destination)
		{
			try
			{
				Directory.Move(source, destination);
				return true;
			}
			catch (Exception e)
			{
				string text = "[mod.io] Failed to move directory.\nSource Directory: " + source + "\nDestination: " + destination + "\n\n" + Utility.GenerateExceptionDebugString(e);
				Debug.LogWarning(text + Utility.GenerateExceptionDebugString(e));
				return false;
			}
		}

		public virtual bool GetDirectoryExists(string path)
		{
			return Directory.Exists(path);
		}

		public virtual IList<string> GetDirectories(string path)
		{
			if (!Directory.Exists(path))
			{
				return null;
			}
			try
			{
				return Directory.GetDirectories(path);
			}
			catch (Exception e)
			{
				string text = "[mod.io] Failed to get directories.\nDirectory: " + path + "\n\n";
				Debug.LogWarning(text + Utility.GenerateExceptionDebugString(e));
				return null;
			}
		}
	}
}
