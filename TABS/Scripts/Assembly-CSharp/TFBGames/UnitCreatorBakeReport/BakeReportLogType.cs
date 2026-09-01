namespace TFBGames.UnitCreatorBakeReport
{
	public enum BakeReportLogType
	{
		ManuallyFixed = 0,
		MissingLodGroup = 1,
		IncorrectLodGroupSetup = 2,
		MissingRenderers = 3,
		MissingMaterials = 4,
		UnequalMaterialCounts = 5,
		OneOrFewerLodLevels = 6,
		MultipleRenderersPerLod = 7,
		DuplicateMaterials = 8
	}
}
