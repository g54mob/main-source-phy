using System;
using System.Collections.Generic;

namespace DM
{
	[Serializable]
	public class LandfallContentDatabaseFile
	{
		[Serializable]
		public struct LandfallGuid
		{
			public int modId;

			public int id;
		}

		public string buildDateTime;

		public List<LandfallGuid> mapAssetGuids = new List<LandfallGuid>();

		public List<LandfallGuid> unitBlueprintGuids = new List<LandfallGuid>();

		public List<LandfallGuid> factionGuids = new List<LandfallGuid>();

		public List<LandfallGuid> campaignGuids = new List<LandfallGuid>();

		public List<LandfallGuid> campaignLevelGuids = new List<LandfallGuid>();

		public List<LandfallGuid> turningDataGuids = new List<LandfallGuid>();

		public List<LandfallGuid> unitBaseGuids = new List<LandfallGuid>();

		public List<LandfallGuid> weaponGuids = new List<LandfallGuid>();

		public List<LandfallGuid> combatMoveGuids = new List<LandfallGuid>();

		public List<LandfallGuid> characterPropGuids = new List<LandfallGuid>();

		public List<LandfallGuid> projectileGuids = new List<LandfallGuid>();

		public List<LandfallGuid> voiceBundleGuids = new List<LandfallGuid>();

		public List<LandfallGuid> defaultHotbarFactionGuids = new List<LandfallGuid>();

		public List<LandfallGuid> factionIconGuids = new List<LandfallGuid>();

		public LandfallGuid defaultVoiceBundleGuid;
	}
}
