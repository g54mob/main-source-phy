using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "MasterAudioBusVolumeConnection", menuName = "SettingsGenerator/Connection/MasterAudio/BusVolumeConnection", order = 4)]
	public class MasterAudioBusVolumeConnectionSO : FloatConnectionSO
	{
		public string BusName;

		[Tooltip("How the input should be mapped to 0f..1f.\nUseful if you have a range in percent (from 0 to 100) but need output ranging from 0f to 1f.")]
		public Vector2 InputRange;

		protected MasterAudioBusVolumeConnection _connection;

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
