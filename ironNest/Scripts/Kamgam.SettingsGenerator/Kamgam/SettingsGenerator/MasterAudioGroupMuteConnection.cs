namespace Kamgam.SettingsGenerator
{
	public class MasterAudioGroupMuteConnection : Connection<bool>
	{
		public bool Invert;

		public string GroupName;

		public MasterAudioGroupMuteConnection(string groupName, bool invert)
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
