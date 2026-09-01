using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class AudioVolumeConnection : Connection<float>
	{
		public Vector2 InputRange;

		public AudioVolumeConnection(Vector2 inputRange)
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
