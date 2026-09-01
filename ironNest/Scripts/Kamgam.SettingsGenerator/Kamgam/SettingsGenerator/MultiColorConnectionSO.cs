using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "MultiColorConnection", menuName = "SettingsGenerator/Connection/MultiColorConnection", order = 4)]
	public class MultiColorConnectionSO : ColorOptionConnectionSO
	{
		protected MultiColorConnection _connection;

		public override IConnectionWithOptions<Color> GetConnection()
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
