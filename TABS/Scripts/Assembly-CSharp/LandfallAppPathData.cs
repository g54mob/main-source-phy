public static class LandfallAppPathData
{
	public const string PrefabPattern = "*.prefab";

	public const string AssetPattern = "*.asset";

	public const string ScenePattern = "*.unity";

	public const string PNGFileExtension = ".png";

	public const string JPGFileExtension = ".jpg";

	private const string PrefabPathRoot = "Assets/1 Prefabs/";

	public const string LowEndAssetSuffix = "_LE";

	public const string VertexBakeRoot = "Assets/VertexBake/";

	public const string VertexBakeLowEndRoot = "Assets/VertexBake/LowEnd/";

	public const string UnitPath = "Assets/2 Units/";

	public const string CustomEditorUnitPath = "Assets/2 Units/UnitEditor/";

	public const string UnitPathLowEnd = "Assets/LowEnd/2 Units/";

	public const string TurningDataPath = "Assets/2 Units/Data/";

	public const string IconsPath = "Assets/2 Units/Icons/";

	public const string MapsPath = "Assets/12 Maps/";

	public const string StandardBakedAssetCachePath = "Assets/VertexBake/Cache/BakeCache.asset";

	public const string LowEndBakedAssetCachePath = "Assets/VertexBake/Cache/BakeCache LowEnd.asset";

	public const string VoiceBundlesPath = "Assets/10 Audio/VoiceBundles/";

	public const string FactionIconsPath = "Assets/8 Data/FactionIcons";

	public const string MainMenuScenePath = "Assets/11 Scenes/MainMenuScenes/";

	public const string UnitBasePath = "Assets/1 Prefabs/0 UnitBases/";

	public const string CombatMovesPath = "Assets/1 Prefabs/4 Moves/";

	public const string PropPath = "Assets/1 Prefabs/8 CharacterProps/";

	public const string WeaponPath = "Assets/1 Prefabs/1 Weapons/";

	public const string ProjectilePath = "Assets/1 Prefabs/2 Projectiles/";

	public static string CampaignsPath = "Assets/13 Campaigns/";

	public static string CampaignsSubfolderPath = CampaignsPath + "Campaigns/";

	public static string CampaignsLevelsFolderPath = CampaignsPath + "Levels/";

	public static string FactionsPath = "Assets/2 Units/Factions/";

	public const string LowEndCacheAsset = "LowEndCache.asset";

	public const string LowEndScenesCacheAsset = "LowEndScenesCache.asset";

	public const string StandardBakeCacheAsset = "BakeCache.asset";

	public const string LowEndBakeCacheAsset = "BakeCache LowEnd.asset";

	public const string CachePath = "Assets/VertexBake/Cache/";

	public const string UnitBakeFolder = "Unit_Items/";

	public const string MapBakeFolder = "Maps";

	public const string StandardUnbakedScenePath = "Assets/11 Scenes/";

	public const string LowEndUnbakedScenePath = "Assets/LowEnd/11 Scenes/";

	public static string UnbakedScenePath = "Assets/11 Scenes/";

	public static string SceneBakePath = "Assets/VertexBake/Scenes/";

	public static string MeshBakePath = "Assets/VertexBake/Meshes/";

	public static string MaterialBakePath = "Assets/VertexBake/Materials/";

	public static string PrefabBakePath = "Assets/VertexBake/Prefabs/";

	public static string BakeCacheAsset = "BakeCache.asset";

	public static string[] UseFactions = new string[2] { "Secret", "Subunits" };

	public const string LegacyCutoutShader = "Legacy Shaders/Transparent/Cutout/Diffuse";

	public const string SimpleVertShader = "TFBG/SimpleVertexColor";

	public const string SimpleVertAlphaShader = "TFBG/SimpleVertexColorAlpha";

	public const string SimpleUnitVertShader = "TFBG/SimpleVertexColorUnit";

	public const string NormalVertColorShader = "TFBG/VertexColorNormal";

	public const string NormalVertColorUnitShader = "TFBG/VertexColorNormalUnit";

	public const string SimpleTintShader = "TFBG/SimpleTintDiffuse";

	public const string SimpleUnitTintShader = "TFBG/SimpleTintDiffuseUnit";

	public const string VertexEmissionShader = "TFBG/EmitVertexColor";

	public const string TintEmissionShader = "TFBG/SimpleTintEmit";

	public const string VertColorNormalOcclusionShader = "TFBG/VertexColorOcclusionNormal";

	public const string VertexColorTexture = "TFBG/VertColorTexture";

	public const string VertTextureEmitShader = "TFBG/VertColorTextureEmit";

	private static bool usingStandardAssets = true;

	public static bool UsingStandardAssets => usingStandardAssets;

	public static void SetPathsToStandardAssets()
	{
		UnbakedScenePath = "Assets/11 Scenes/";
		SceneBakePath = "Assets/VertexBake/Scenes/";
		MeshBakePath = "Assets/VertexBake/Meshes/";
		MaterialBakePath = "Assets/VertexBake/Materials/";
		PrefabBakePath = "Assets/VertexBake/Prefabs/";
		BakeCacheAsset = "BakeCache.asset";
		CampaignsPath = "Assets/13 Campaigns/";
		CampaignsSubfolderPath = CampaignsPath + "Campaigns/";
		CampaignsLevelsFolderPath = CampaignsPath + "Levels/";
		FactionsPath = "Assets/2 Units/Factions/";
		UseFactions = new string[2] { "Secret", "Subunits" };
		usingStandardAssets = true;
	}

	public static void SetPathsToLowEndAssets()
	{
		UnbakedScenePath = "Assets/LowEnd/11 Scenes/";
		SceneBakePath = "Assets/VertexBake/LowEnd/Scenes/";
		MeshBakePath = "Assets/VertexBake/LowEnd/Meshes/";
		MaterialBakePath = "Assets/VertexBake/LowEnd/Materials/";
		PrefabBakePath = "Assets/VertexBake/LowEnd/Prefabs/";
		BakeCacheAsset = "BakeCache LowEnd.asset";
		CampaignsPath = "Assets/LowEnd/13 Campaigns/";
		CampaignsSubfolderPath = CampaignsPath + "Campaigns/";
		CampaignsLevelsFolderPath = CampaignsPath + "Levels/";
		FactionsPath = "Assets/LowEnd/2 Units/Factions/";
		UseFactions = new string[2] { "Secret_LE", "Subunits_LE" };
		usingStandardAssets = false;
	}
}
