using System.IO;
using ModIO;

namespace Landfall.TABS.Workshop
{
	public class LoadedCustomCampaignWrapper : ILoadedCustomContent
	{
		private bool m_WasChanged;

		public FileInfo ContentFile { get; private set; }

		public ModProfile ModProfile { get; private set; }

		public int ModID { get; private set; }

		public bool IsCustomUnit => ModID != 0;

		public CampaignSequence CampaignSequence { get; private set; }

		public long TimeStamp { get; private set; }

		public LoadedCustomCampaignWrapper(int modID, CampaignSequence sequence, long timeStamp, FileInfo contentFile)
		{
			ModID = modID;
			CampaignSequence = sequence;
			TimeStamp = timeStamp;
			ContentFile = contentFile;
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
