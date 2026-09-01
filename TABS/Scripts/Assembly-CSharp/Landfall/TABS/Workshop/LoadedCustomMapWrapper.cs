using LevelCreator;
using ModIO;

namespace Landfall.TABS.Workshop
{
	public class LoadedCustomMapWrapper : ILoadedCustomContent
	{
		public CustomMap CustomMap;

		private bool m_WasChanged;

		public ModProfile ModProfile { get; private set; }

		public bool IsCustomUnit => ModID != 0;

		public int ModID { get; private set; }

		public DatabaseID ID { get; private set; }

		public string FilePath { get; private set; }

		public long TimeStamp { get; private set; }

		public LoadedCustomMapWrapper(CustomMap customMap, string filePath, DatabaseID ID, int modIOID, long timeStamp)
		{
			CustomMap = customMap;
			FilePath = filePath;
			this.ID = ID;
			ModID = modIOID;
			TimeStamp = timeStamp;
		}

		public void SetTempModID(int Mod)
		{
			ModID = Mod;
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
	}
}
