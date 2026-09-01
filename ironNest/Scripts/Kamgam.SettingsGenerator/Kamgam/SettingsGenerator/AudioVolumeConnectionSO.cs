using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "AudioVolumeConnection", menuName = "SettingsGenerator/Connection/AudioVolumeConnection", order = 4)]
	public class AudioVolumeConnectionSO : FloatConnectionSO
	{
		[Tooltip("How the input should be mapped to 0f..1f.\nUseful if you have a range in percent (from 0 to 100) but need output ranging from 0f to 1f.")]
		public Vector2 InputRange;

		protected AudioVolumeConnection _connection;

		public override IConnection<float> GetConnection()
		{
			return null;
		}

		public void Create()
		{
		}

		public override void DestroyConnection()
		{
		}
	}
}
