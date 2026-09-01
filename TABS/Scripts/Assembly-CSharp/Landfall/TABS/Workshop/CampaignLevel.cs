using System;

namespace Landfall.TABS.Workshop
{
	[Serializable]
	public class CampaignLevel
	{
		public double VersionNumber;

		public string LevelName;

		public string IconPath;

		public int Budget;

		public int campaignIndex;

		public UnitLayout BlueUnits;

		public UnitLayout RedUnits;

		public DatabaseID ID;

		public DatabaseID MapID;

		public DatabaseID CustomMapID;

		public string BattleDescription;

		public AllowedUnitWrapper[] AllowedUnits;

		public DatabaseID[] AllowedFactions;

		public TABSSceneSettings SceneSettings;
	}
}
