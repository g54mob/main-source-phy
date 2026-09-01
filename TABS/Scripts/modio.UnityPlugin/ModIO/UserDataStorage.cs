using DM;
using ModIO.UserDataIOCallbacks;
using UnityEngine;

namespace ModIO
{
	public static class UserDataStorage
	{
		public static readonly IUserDataIO PLATFORM_IO;

		public static string USER_DIRECTORY => PLATFORM_IO.UserDirectory;

		static UserDataStorage()
		{
			if (InjectedIO.userDataIO != null)
			{
				Debug.LogFormat("DM: Using injected userDataIO");
				PLATFORM_IO = InjectedIO.userDataIO;
			}
			else
			{
				Debug.LogFormat("DM: Not using injected userDataIO");
				PLATFORM_IO = new SystemIOWrapper();
			}
			PLATFORM_IO.InitializeForDefaultUser(delegate(bool success)
			{
				if (success)
				{
					LocalUser.Load();
				}
			});
		}

		public static void SetActiveUser(string platformUserId, SetActiveUserCallback<string> callback)
		{
			PLATFORM_IO.SetActiveUser(platformUserId, delegate(string id, bool success)
			{
				if (success)
				{
					LocalUser.Load(delegate
					{
						if (callback != null)
						{
							callback(id, success);
						}
					});
				}
				else
				{
					LocalUser.instance = default(LocalUser);
					Debug.Log("[mod.io] Failed to set active user. LocalUser cleared.");
					if (callback != null)
					{
						callback(id, success);
					}
				}
			});
		}

		public static void SetActiveUser(int platformUserId, SetActiveUserCallback<int> callback)
		{
			PLATFORM_IO.SetActiveUser(platformUserId, delegate(int id, bool success)
			{
				if (success)
				{
					LocalUser.Load(delegate
					{
						if (callback != null)
						{
							callback(id, success);
						}
					});
				}
				else
				{
					LocalUser.instance = default(LocalUser);
					Debug.Log("[mod.io] Failed to set active user. LocalUser cleared.");
					if (callback != null)
					{
						callback(id, success);
					}
				}
			});
		}

		public static void SetActiveUser<T>(T platformUserHandle, SetActiveUserCallback<T> callback)
		{
			if (PLATFORM_IO is IUserDataIO<T>)
			{
				((IUserDataIO<T>)PLATFORM_IO).SetActiveUser(platformUserHandle, delegate(T id, bool success)
				{
					if (success)
					{
						LocalUser.Load(delegate
						{
							if (callback != null)
							{
								callback(id, success);
							}
						});
					}
					else
					{
						LocalUser.instance = default(LocalUser);
						Debug.Log("[mod.io] Failed to set active user. LocalUser cleared.");
						if (callback != null)
						{
							callback(id, success);
						}
					}
				});
			}
			else
			{
				Debug.LogWarning("[mod.io] Attempt to call SetActiveUser with a type of: " + typeof(T).ToString() + "\nThis type of user handle is unsupported by the assigned IUserDataIO implementation: " + ((PLATFORM_IO == null) ? "NULL" : PLATFORM_IO.GetType().ToString()));
				if (callback != null)
				{
					callback(platformUserHandle, success: false);
				}
			}
		}

		public static void ReadFile(string relativePath, ReadFileCallback callback)
		{
			PLATFORM_IO.ReadFile(relativePath, callback);
		}

		public static void ReadJSONFile<T>(string relativePath, ReadJSONFileCallback<T> callback)
		{
			ReadFile(relativePath, delegate(string p, bool success, byte[] fileData)
			{
				T jsonObject;
				if (success)
				{
					success = IOUtilities.TryParseUTF8JSONData<T>(fileData, out jsonObject);
				}
				else
				{
					jsonObject = default(T);
				}
				callback(relativePath, success, jsonObject);
			});
		}

		public static void WriteFile(string relativePath, byte[] data, WriteFileCallback callback)
		{
			PLATFORM_IO.WriteFile(relativePath, data, callback);
		}

		public static void WriteJSONFile<T>(string relativePath, T jsonObject, WriteFileCallback callback)
		{
			byte[] array = IOUtilities.GenerateUTF8JSONData(jsonObject);
			if (array != null)
			{
				WriteFile(relativePath, array, callback);
				return;
			}
			Debug.LogWarning("[mod.io] Failed create JSON representation of object before writing file.\nFile: " + relativePath + "\n\n");
			callback?.Invoke(relativePath, success: false);
		}

		public static void DeleteFile(string relativePath, DeleteFileCallback callback)
		{
			PLATFORM_IO.DeleteFile(relativePath, callback);
		}

		public static void ClearActiveUserData(ClearActiveUserDataCallback callback)
		{
			PLATFORM_IO.ClearActiveUserData(callback);
		}
	}
}
