using ModIO;

namespace Landfall.TABS.Workshop
{
	public class LoadedCustomFactionWrapper : ILoadedCustomContent
	{
		private bool m_WasChanged;

		public int ModID { get; private set; }

		public ModProfile ModProfile { get; private set; }

		public DatabaseID ID { get; private set; }

		public Faction faction { get; private set; }

		public string DirectoryPath { get; private set; }

		public string FullPath { get; private set; }

		public long TimeStamp { get; private set; }

		public LoadedCustomFactionWrapper(Faction faction, int modID, string parentDirectory, string fullPath, long timeStamp)
		{
			ID = faction.Entity.GUID;
			ModID = modID;
			this.faction = faction;
			DirectoryPath = parentDirectory;
			FullPath = fullPath;
			TimeStamp = timeStamp;
			m_WasChanged = false;
		}

		public void SetDetails(ModProfile profile)
		{
			ModProfile = profile;
		}

		public void Changed()
		{
			m_WasChanged = true;
		}

		public bool WasChanged()
		{
			bool wasChanged = m_WasChanged;
			m_WasChanged = false;
			return wasChanged;
		}

		public void SetTempSteamID(ulong steamID)
		{
		}

		public void SetTempModID(int ModID)
		{
		}
	}
}
