using System;
using Landfall.TABS;
using TFBGames;
using UnityEngine;

namespace LevelCreator
{
	[Serializable]
	public class SerializedCustomMap
	{
		public DatabaseID id;

		public string name;

		public string levelPath;

		public string iconPath;

		public string filePath;

		public static void GetLoadedCustomMapFromDisk(string path, Action<SerializedCustomMap> doneCallback)
		{
			FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
			fileIO.FileExists(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (!exists)
				{
					doneCallback?.Invoke(null);
				}
				else
				{
					fileIO.ReadAllText(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(string json, Exception readException)
					{
						if (string.IsNullOrEmpty(json))
						{
							Debug.LogFormat("Failed to load: {0}", path);
							doneCallback?.Invoke(null);
						}
						else
						{
							SerializedCustomMap obj = JsonUtility.FromJson<SerializedCustomMap>(json);
							doneCallback?.Invoke(obj);
						}
					});
				}
			});
		}
	}
}
