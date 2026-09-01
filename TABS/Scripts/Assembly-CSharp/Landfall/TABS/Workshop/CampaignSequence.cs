using System;

namespace Landfall.TABS.Workshop
{
	[Serializable]
	public class CampaignSequence
	{
		public DatabaseID ID;

		public string UnlockKey;

		public string CampaignName;

		public string IconPath;

		public string CampaignDescription;

		public string CampaignThankYouTitle;

		public string CampaignThankYouText;

		public CampaignLevelReference[] Levels;
	}
}
