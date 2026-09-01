using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "MicrophoneConnection", menuName = "SettingsGenerator/Connection/MicrophoneConnection", order = 4)]
	public class MicrophoneConnectionSO : OptionConnectionSO
	{
		[Tooltip("If > 0 then every # seconds the connection will check for new microphones and update the options list.")]
		public float PollIntervalInSec;

		protected MicrophoneConnection _connection;

		public override IConnectionWithOptions<string> GetConnection()
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
