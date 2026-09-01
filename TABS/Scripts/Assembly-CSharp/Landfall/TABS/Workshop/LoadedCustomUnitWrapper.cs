using ModIO;

namespace Landfall.TABS.Workshop
{
	public class LoadedCustomUnitWrapper : ILoadedCustomContent
	{
		private bool m_WasChanged;

		public ModProfile ModProfile { get; private set; }

		public bool IsCustomUnit => BluePrint.ModID != 0;

		public DatabaseID UnitID { get; private set; }

		public UnitBlueprint BluePrint { get; private set; }

		public string DirectoryPath { get; private set; }

		public string FullPath { get; private set; }

		public long TimeStamp { get; private set; }

		public LoadedCustomUnitWrapper(UnitBlueprint bluePrint, string parentDirectory, string fullPath, long timeStamp)
		{
			UnitID = bluePrint.Entity.GUID;
			BluePrint = bluePrint;
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
