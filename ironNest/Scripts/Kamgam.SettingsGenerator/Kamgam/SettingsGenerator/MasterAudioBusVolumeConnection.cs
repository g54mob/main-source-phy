using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class MasterAudioBusVolumeConnection : Connection<float>
	{
		public string BusName;

		public Vector2 InputRange;

		public MasterAudioBusVolumeConnection(Vector2 inputRange, string busName)
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
