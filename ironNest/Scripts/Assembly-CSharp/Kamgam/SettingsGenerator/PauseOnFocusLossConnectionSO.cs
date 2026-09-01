using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "PauseOnFocusLossConnection", menuName = "SettingsGenerator/Connection/Gameplay/PauseOnFocusLossConnection", order = 51)]
	public class PauseOnFocusLossConnectionSO : BoolConnectionSO
	{
		private PauseOnFocusLossConnection _connection;

		public override IConnection<bool> GetConnection()
		{
			return null;
		}

		public override void DestroyConnection()
		{
		}

		private void Create()
		{
		}
	}
}
