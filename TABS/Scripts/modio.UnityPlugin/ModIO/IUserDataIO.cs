using System;
using ModIO.UserDataIOCallbacks;

namespace ModIO
{
	public interface IUserDataIO
	{
		string UserDirectory { get; }

		void InitializeForDefaultUser(Action<bool> callback);

		void SetActiveUser(string platformUserId, SetActiveUserCallback<string> callback);

		void SetActiveUser(int platformUserId, SetActiveUserCallback<int> callback);

		void ReadFile(string pathRelative, ReadFileCallback callback);

		void WriteFile(string pathRelative, byte[] data, WriteFileCallback callback);

		void DeleteFile(string pathRelative, DeleteFileCallback callback);

		void ClearActiveUserData(ClearActiveUserDataCallback callback);
	}
	public interface IUserDataIO<T> : IUserDataIO
	{
		void SetActiveUser(T platformUserId, SetActiveUserCallback<T> callback);
	}
}
