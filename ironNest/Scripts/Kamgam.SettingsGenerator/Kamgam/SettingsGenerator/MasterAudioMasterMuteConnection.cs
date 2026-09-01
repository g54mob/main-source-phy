namespace Kamgam.SettingsGenerator
{
	public class MasterAudioMasterMuteConnection : Connection<bool>
	{
		public bool Invert;

		public MasterAudioMasterMuteConnection(bool invert)
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
