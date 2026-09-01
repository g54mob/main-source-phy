using Landfall.TABS.Workshop;
using TFBGames;
using UnityEngine;

namespace LevelCreator
{
	public static class Paths
	{
		public static string TemplateDirectory => Application.streamingAssetsPath + "/" + TemplateDirectoryName;

		public static string TemplateDirectoryName => "LevelTemplates";

		public static string PlayerLevelDirectory => CustomContentFilePaths.FilePathCustomMap;

		public static string PlayerLevelDirectoryName => "CustomMaps";

		public static string RecentLevelsFile => GamePaths.PersistentDataPath + "/" + RecentLevelsFileName;

		public static string RecentLevelsFileName => "RecentLevels.txt";

		public static string TestMapPath => GamePaths.PersistentDataPath + "/" + TestMapName;

		public static string TestMapName => "TestMap.tld";

		public static string ShownTutorialPopupsPath => GamePaths.PersistentDataPath + "/" + ShownTutorialPopupsName;

		public static string ShownTutorialPopupsName => "ShownTutorialPopups.txt";
	}
}
