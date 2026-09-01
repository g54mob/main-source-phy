using System;
using ModIO;
using ModIO.UserDataIOCallbacks;
using TFBGames;
using UnityEngine;

namespace DM
{
	public class IUserDataIO_To_IFileIOPlatform : IUserDataIO
	{
		private FileIOWrapper fileIOWrapper;

		private FileHandlingFileType fileHandlingFileType;

		public string UserDirectory { get; }

		public IUserDataIO_To_IFileIOPlatform(FileIOWrapper fileIOWrapper, string rootUserDir)
		{
			this.fileIOWrapper = fileIOWrapper;
			UserDirectory = rootUserDir;
		}

		public void InitializeForDefaultUser(Action<bool> callback)
		{
			Debug.LogWarning("DM: IUserDataIO_To_IFileIOPlatform.InitializeForDefaultUser NOT IMPLEMENTED");
			callback?.Invoke(obj: true);
		}

		public void SetActiveUser(string platformUserId, SetActiveUserCallback<string> callback)
		{
			Debug.LogWarning("DM: IUserDataIO_To_IFileIOPlatform.SetActiveUser NOT IMPLEMENTED");
			callback?.Invoke("", success: true);
		}

		public void SetActiveUser(int platformUserId, SetActiveUserCallback<int> callback)
		{
			Debug.LogWarning("DM: IUserDataIO_To_IFileIOPlatform.SetActiveUser NOT IMPLEMENTED");
			callback?.Invoke(0, success: true);
		}

		public void ReadFile(string pathRelative, ReadFileCallback callback)
		{
			string combinedPath = IOUtilities.CombinePath(UserDirectory, pathRelative);
			Helpers.ReadFileIfExists(fileIOWrapper, fileHandlingFileType, combinedPath, delegate(byte[] maybeData, Exception maybeException)
			{
				Helpers.ReportExceptionIfAny("ReadFileIfExists", combinedPath, maybeException);
				callback?.Invoke(pathRelative, maybeException == null, maybeData);
			});
		}

		public void WriteFile(string relativePath, byte[] data, WriteFileCallback callback)
		{
			string combinedPath = IOUtilities.CombinePath(UserDirectory, relativePath);
			Helpers.WriteFile(fileIOWrapper, fileHandlingFileType, combinedPath, data, delegate(Exception maybeException)
			{
				Helpers.ReportExceptionIfAny("WriteFile", combinedPath, maybeException);
				callback?.Invoke(relativePath, maybeException == null);
			});
		}

		public void DeleteFile(string pathRelative, DeleteFileCallback callback)
		{
			string combinedPath = IOUtilities.CombinePath(UserDirectory, pathRelative);
			Helpers.DeleteFile(fileIOWrapper, fileHandlingFileType, combinedPath, delegate(Exception maybeException)
			{
				Helpers.ReportExceptionIfAny("DeleteFile", combinedPath, maybeException);
				callback?.Invoke(pathRelative, maybeException == null);
			});
		}

		public void ClearActiveUserData(ClearActiveUserDataCallback callback)
		{
			Debug.LogWarning("DM: IUserDataIO_To_IFileIOPlatform.ClearActiveUserData NOT IMPLEMENTED");
			callback?.Invoke(success: true);
		}
	}
}
