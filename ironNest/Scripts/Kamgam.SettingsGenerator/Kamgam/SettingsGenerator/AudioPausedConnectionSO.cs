using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "AudioPausedConnection", menuName = "SettingsGenerator/Connection/AudioPausedConnection", order = 4)]
	public class AudioPausedConnectionSO : BoolConnectionSO
	{
		[Tooltip("Invert the value? If you are connecting this to an audio ENABLED bool then the meaning is inverted (paused vs enabled) and you should set this to true.")]
		public bool Invert;

		protected AudioPausedConnection _connection;

		public override IConnection<bool> GetConnection()
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
