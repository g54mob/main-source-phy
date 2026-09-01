using System.Collections.Generic;
using System.IO;
using Landfall.TABS;
using UnityEngine;

namespace DM
{
	[CreateAssetMenu]
	public class BuildDatabase : ScriptableObject
	{
		[SerializeField]
		private string m_buildDateTimeString;

		[SerializeField]
		private List<Object> m_objects;

		[SerializeField]
		private SerializedHelpingData m_helpingData;

		[SerializeField]
		private CustomFactionColorDatabase m_customFactionColorDatabase;

		[SerializeField]
		private List<string> m_mainMenuScenes;

		[SerializeField]
		private UnitEditorColorPalette m_unitEditorColorPalette;

		[SerializeField]
		private string m_version;

		[SerializeField]
		private UpgradeDataAsset m_upgradeDataAsset;

		[SerializeField]
		private UnitBlueprint m_unitEditorBlueprint;

		private const string assetDatabaseFilepath = "Assets/Resources/DMAssetDatabase.json";

		private const string landfallContentDatabaseFilepath = "Assets/Resources/DMLandfallContentDatabase.json";

		public static string ImagesResourcePath = "images/";

		public static string ImagesPath = "Assets/Resources/images/";

		public static string UnitsPath = "Assets/Resources/units/";

		public static string LevelsPath = "Assets/Resources/levels/";

		public static string CampaignsPath = "Assets/Resources/campaigns/";

		public static NonStreamableDatabaseAssets GetNonStreamableDatabaseAssets()
		{
			BuildDatabase buildDatabase = Resources.Load<BuildDatabase>("Build Database");
			return new NonStreamableDatabaseAssets
			{
				BuildDateTimeString = buildDatabase.m_buildDateTimeString,
				Objects = buildDatabase.m_objects,
				HelpingData = buildDatabase.m_helpingData,
				CustomFactionColorDatabase = buildDatabase.m_customFactionColorDatabase,
				MainMenuScenes = buildDatabase.m_mainMenuScenes,
				UnitEditorColorPalette = buildDatabase.m_unitEditorColorPalette,
				Version = buildDatabase.m_version,
				UpgradeDataAsset = buildDatabase.m_upgradeDataAsset,
				UnitEditorBlueprint = buildDatabase.m_unitEditorBlueprint
			};
		}

		public static string GetImagePath(string filename)
		{
			return $"{ImagesPath}{filename}";
		}

		public static string GetUnitPath(DatabaseID id)
		{
			return $"{UnitsPath}{id}.json";
		}

		public static string GetLevelPath(DatabaseID id)
		{
			return $"{LevelsPath}{id}.json";
		}

		public static string GetCampaignPath(DatabaseID id)
		{
			return $"{CampaignsPath}{id}.json";
		}

		private static void CreateFoldersAndWriteTextFile(string filepath, string fileContent)
		{
			FileInfo fileInfo = new FileInfo(filepath);
			fileInfo.Directory.Create();
			File.WriteAllText(fileInfo.FullName, fileContent);
		}

		private static void CreateFoldersAndWriteByteFile(string filepath, byte[] fileContent)
		{
			FileInfo fileInfo = new FileInfo(filepath);
			fileInfo.Directory.Create();
			File.WriteAllBytes(fileInfo.FullName, fileContent);
		}
	}
}
