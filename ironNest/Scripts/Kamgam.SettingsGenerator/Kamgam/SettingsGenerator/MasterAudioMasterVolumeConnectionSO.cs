using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "MasterAudioMasterVolumeConnection", menuName = "SettingsGenerator/Connection/MasterAudio/MasterVolumeConnection", order = 4)]
	public class MasterAudioMasterVolumeConnectionSO : FloatConnectionSO
	{
		[Tooltip("How the input should be mapped to 0f..1f.\nUseful if you have a range in percent (from 0 to 100) but need output ranging from 0f to 1f.")]
		public Vector2 InputRange;

		protected MasterAudioMasterVolumeConnection _connection;

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
