using System;

namespace MoreMountains.Tools
{
	public static class MMSaveLoadManager
	{
		public static IMMSaveLoadManagerMethod SaveLoadMethod;

		private const string _baseFolderName = "/MMData/";

		private const string _defaultFolderName = "MMSaveLoadManager";

		private static string DetermineSavePath(string folderName = "MMSaveLoadManager")
		{
			return null;
		}

		private static string DetermineSaveFileName(string fileName)
		{
			return null;
		}

		public static void Save(object saveObject, string fileName, string foldername = "MMSaveLoadManager")
		{
		}

		public static object Load(Type objectType, string fileName, string foldername = "MMSaveLoadManager")
		{
			return null;
		}

		public static void DeleteSave(string fileName, string folderName = "MMSaveLoadManager")
		{
		}

		public static void DeleteSaveFolder(string folderName = "MMSaveLoadManager")
		{
		}

		public static void DeleteAllSaveFiles()
		{
		}

		public static void DeleteDirectory(string target_dir)
		{
		}
	}
}
