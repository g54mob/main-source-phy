using System;
using System.IO;
using TFBGames;

namespace Landfall.TABS.Workshop
{
	public class CustomContentFilePaths
	{
		public static string OldFilePathLayout => Path.Combine(GamePaths.DataPath, "../../TABS/Battles/");

		public static string OldFilePathCampaign => Path.Combine(GamePaths.DataPath, "../../TABS/Campaigns/");

		public static string UnitDirectoryPath => Path.Combine(GamePaths.DataPath, "CustomContent/CustomUnits/");

		public static string FilePathFaction => Path.Combine(GamePaths.DataPath, "CustomContent/CustomFactions/");

		public static string FilePathLayout => Path.Combine(GamePaths.DataPath, "CustomContent/Battles/");

		public static string FilePathCampaign => Path.Combine(GamePaths.DataPath, "CustomContent/Campaigns/");

		public static string FilePathCustomMap => Path.Combine(GamePaths.DataPath, "CustomContent/CustomMaps/");

		private static string TempFilePath => GamePaths.PersistentDataPath + "/temp/";

		public static string FileEndingLayout => ".totallyaccuratelevel";

		public static string FileEndingBattle => ".totallyaccuratebattle";

		public static string FileEndingCampaign => ".TABSCampaign";

		public static string FileEndingFaction => ".TABSFaction";

		public static string FileEndingUnit => ".unit";

		public static string FileEndingCustomMap => ".map";

		public static string FileEndingCustomLevel => ".tld";

		public static string CustomUnitFactionGUID => "3a940c3c-74a8-4456-8d32-ae811794d277";

		public static string GetTempFolderPath()
		{
			ServiceLocator.GetService<FileIOWrapper>().CreateDirectory(TempFilePath, FileHandlingFileType.CustomContentOrLocalStorageFile, null);
			return TempFilePath;
		}

		public static void DeleteTempFolder()
		{
			ServiceLocator.GetService<FileIOWrapper>().DeleteDirectory(TempFilePath, recursive: true, FileHandlingFileType.CustomContentOrLocalStorageFile, null);
		}

		public static string GetFileExtension(LevelType type)
		{
			switch (type)
			{
			case LevelType.Layout:
				return FileEndingLayout;
			case LevelType.Battle:
				return FileEndingBattle;
			case LevelType.Campaign:
				return FileEndingCampaign;
			default:
				throw new Exception("FileEnding: " + type.ToString() + " Is Not Configured!");
			}
		}

		public static string GetCustomContentFilePath(WorkshopContentType contentType)
		{
			switch (contentType)
			{
			case WorkshopContentType.Unit:
				return UnitDirectoryPath;
			case WorkshopContentType.Layout:
			case WorkshopContentType.Battle:
				return FilePathLayout;
			case WorkshopContentType.Campaign:
				return FilePathCampaign;
			case WorkshopContentType.Faction:
				return FilePathFaction;
			case WorkshopContentType.Map:
				return FilePathCustomMap;
			default:
				return null;
			}
		}
	}
}
