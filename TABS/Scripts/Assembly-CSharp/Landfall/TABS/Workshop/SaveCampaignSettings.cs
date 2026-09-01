namespace Landfall.TABS.Workshop
{
	public class SaveCampaignSettings
	{
		public LevelType LevelType { get; private set; }

		public bool SaveBlue { get; private set; }

		public bool SaveRed { get; private set; }

		public int Budget { get; private set; }

		public string BattleDescription { get; private set; }

		public AllowedUnitWrapper[] AllowedUnits { get; private set; }

		public DatabaseID[] AllowedFactions { get; private set; }

		public DatabaseID SelectedMap { get; private set; }

		public TABSSceneSettings SceneSettings { get; private set; }

		public DatabaseID CustomMap { get; private set; }

		public SaveCampaignSettings(int budget, AllowedUnitWrapper[] AllowedUnits, DatabaseID[] AllowedFactions, DatabaseID map, string desc, TABSSceneSettings sceneSettings, bool red = false, DatabaseID customMap = default(DatabaseID))
		{
			SaveBlue = true;
			SaveRed = red;
			Budget = budget;
			LevelType = LevelType.Battle;
			this.AllowedUnits = AllowedUnits;
			this.AllowedFactions = AllowedFactions;
			SelectedMap = map;
			BattleDescription = desc;
			SceneSettings = sceneSettings;
			CustomMap = customMap;
		}
	}
}
