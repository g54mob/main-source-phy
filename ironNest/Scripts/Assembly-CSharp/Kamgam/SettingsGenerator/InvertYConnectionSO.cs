using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "InvertY", menuName = "SettingsGenerator/Connection/InvertY", order = 5)]
	public class InvertYConnectionSO : BoolConnectionSO
	{
		public string TargetTag;

		protected InvertYConnection _connection;

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
