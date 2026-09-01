using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "InvertX", menuName = "SettingsGenerator/Connection/InvertX", order = 5)]
	public class InvertXConnectionSO : BoolConnectionSO
	{
		public string TargetTag;

		protected InvertXConnection _connection;

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
