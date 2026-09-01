using System;

namespace Landfall.TABS.Workshop
{
	[Serializable]
	public class CampaignLevelReference
	{
		public string LevelName;

		public int CampaignIndex;

		public DatabaseID ID;

		public DatabaseID MapID;

		public int Budget;

		public CampaignLevelReference(CampaignLevel lvl, int index)
		{
			LevelName = lvl.LevelName;
			ID = lvl.ID;
			MapID = lvl.MapID;
			CampaignIndex = index;
			Budget = lvl.Budget;
		}
	}
}
