using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "FieldOfViewConnection", menuName = "SettingsGenerator/Connection/FieldOfViewConnection", order = 4)]
	public class FieldOfViewConnectionSO : FloatConnectionSO
	{
		public bool UseMain;

		public bool UseMarkers;

		protected FieldOfViewConnection _connection;

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
