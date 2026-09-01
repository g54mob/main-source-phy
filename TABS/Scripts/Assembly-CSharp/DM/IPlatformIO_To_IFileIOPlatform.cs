using System;
using ModIO;
using ModIO.PlatformIOCallbacks;
using TFBGames;
using UnityEngine;

namespace DM
{
	public class IPlatformIO_To_IFileIOPlatform : IPlatformIO
	{
		private FileIOWrapper fileIOWrapper;

		private FileHandlingFileType fileHandlingFileType = FileHandlingFileType.CustomContentOrLocalStorageFile;

		public string InstallationDirectory { get; }

		public string CacheDirectory { get; }

		public IPlatformIO_To_IFileIOPlatform(FileIOWrapper fileIOWrapper, string installDir, string cacheDir)
		{
			this.fileIOWrapper = fileIOWrapper;
			InstallationDirectory = installDir;
			CacheDirectory = cacheDir;
		}

		public void ReadFile(string path, ReadFileCallback callback)
		{
			Helpers.ReadFileIfExists(fileIOWrapper, fileHandlingFileType, path, delegate(byte[] maybeData, Exception maybeException)
			{
				Helpers.ReportExceptionIfAny("ReadFileIfExists", path, maybeException);
				callback?.Invoke(path, maybeException == null && maybeData != null, maybeData);
			});
		}

		public void WriteFile(string path, byte[] data, WriteFileCallback callback)
		{
			Helpers.WriteFile(fileIOWrapper, fileHandlingFileType, path, data, delegate(Exception maybeException)
			{
				Helpers.ReportExceptionIfAny("WriteFile", path, maybeException);
				callback?.Invoke(path, maybeException == null);
			});
		}

		public void DeleteFile(string path, DeleteFileCallback callback)
		{
			Helpers.DeleteFile(fileIOWrapper, fileHandlingFileType, path, delegate(Exception maybeException)
			{
				Helpers.ReportExceptionIfAny("DeleteFile", path, maybeException);
				callback?.Invoke(path, maybeException == null);
			});
		}

		public void MoveFile(string sourcePath, string destinationPath, MoveFileCallback callback)
		{
			Helpers.MoveFile(fileIOWrapper, fileHandlingFileType, sourcePath, destinationPath, delegate(Exception maybeException)
			{
				Helpers.ReportExceptionIfAny("MoveFile", sourcePath, maybeException);
				callback?.Invoke(sourcePath, destinationPath, maybeException == null);
			});
		}

		public void GetFileExists(string path, GetFileExistsCallback callback)
		{
			fileIOWrapper.FileExists(path, fileHandlingFileType, delegate(bool exists)
			{
				callback?.Invoke(path, exists);
			});
		}

		public void GetFileSizeAndHash(string path, GetFileSizeAndHashCallback callback)
		{
			Helpers.ReadFileIfExists(fileIOWrapper, fileHandlingFileType, path, delegate(byte[] maybeData, Exception maybeException)
			{
				Helpers.ReportExceptionIfAny("ReadFileIfExists", path, maybeException);
				if (maybeException == null)
				{
					string text = Helpers.TryGetHash(maybeData);
					if (!string.IsNullOrEmpty(text))
					{
						callback?.Invoke(path, success: true, maybeData.Length, text);
					}
					else
					{
						callback?.Invoke(path, success: false, -1L, "");
					}
				}
				else
				{
					Debug.LogErrorFormat("Failed to get file size and hash from \"{0}\", reason: {1}", path, maybeException);
					callback?.Invoke(path, success: false, -1L, "");
				}
			});
		}

		public void GetFiles(string path, string nameFilter, bool recurseSubdirectories, GetFilesCallback callback)
		{
			if (nameFilter != null)
			{
				Debug.LogError("DM: nameFilter not supported in GetFiles");
				callback?.Invoke(path, success: false, null);
			}
			fileIOWrapper.GetFilesRecursive(path, fileHandlingFileType, delegate(string[] fileList, Exception maybeException)
			{
				Helpers.ReportExceptionIfAny("ReadFileIfExists", path, maybeException);
				callback?.Invoke(path, maybeException == null, fileList);
			});
		}

		public void CreateDirectory(string path, CreateDirectoryCallback callback)
		{
			fileIOWrapper.CreateDirectory(path, fileHandlingFileType, delegate(Exception maybeException)
			{
				Helpers.ReportExceptionIfAny("CreateDirectory", path, maybeException);
				callback?.Invoke(path, maybeException == null);
			});
		}

		public void DeleteDirectory(string path, DeleteDirectoryCallback callback)
		{
			Helpers.DeleteDirectory(fileIOWrapper, fileHandlingFileType, path, delegate(Exception maybeException)
			{
				Helpers.ReportExceptionIfAny("DeleteDirectory", path, maybeException);
				callback?.Invoke(path, maybeException == null);
			});
		}

		public void MoveDirectory(string sourcePath, string destinationPath, MoveDirectoryCallback callback)
		{
			fileIOWrapper.MoveDirectory(sourcePath, destinationPath, overwrite: true, fileHandlingFileType, delegate(Exception maybeException)
			{
				Helpers.ReportExceptionIfAny("MoveDirectory", sourcePath, maybeException);
				callback?.Invoke(sourcePath, destinationPath, maybeException == null);
			});
		}

		public void GetDirectoryExists(string path, GetDirectoryExistsCallback callback)
		{
			fileIOWrapper.DirectoryExists(path, fileHandlingFileType, delegate(bool exists)
			{
				callback?.Invoke(path, exists);
			});
		}

		public void GetDirectories(string path, GetDirectoriesCallback callback)
		{
			fileIOWrapper.GetDirectories(path, fileHandlingFileType, delegate(string[] directories, Exception maybeException)
			{
				Helpers.ReportExceptionIfAny("GetDirectories", path, maybeException);
				callback?.Invoke(path, maybeException == null, directories);
			});
		}
	}
}
