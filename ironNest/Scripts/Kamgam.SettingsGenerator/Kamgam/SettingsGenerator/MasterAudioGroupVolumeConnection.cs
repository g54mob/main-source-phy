using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class MasterAudioGroupVolumeConnection : Connection<float>
	{
		public string GroupName;

		public Vector2 InputRange;

		public MasterAudioGroupVolumeConnection(Vector2 inputRange, string groupName)
		{
		}

		public override float Get()
		{
			return 0f;
		}

		public override void Set(float volume)
		{
		}
	}
}
