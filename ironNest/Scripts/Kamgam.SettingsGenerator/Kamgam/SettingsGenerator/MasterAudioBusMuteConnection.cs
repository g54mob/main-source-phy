namespace Kamgam.SettingsGenerator
{
	public class MasterAudioBusMuteConnection : Connection<bool>
	{
		public bool Invert;

		public string BusName;

		public MasterAudioBusMuteConnection(string busName, bool invert)
		{
		}

		public override bool Get()
		{
			return false;
		}

		public override void Set(bool mute)
		{
		}
	}
}
