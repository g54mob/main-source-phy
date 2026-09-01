using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "NakedDevXeSSConnection", menuName = "SettingsGenerator/Connection/NakedDevXeSSConnection", order = 4)]
	public class NakedDevXeSSConnectionSO : OptionConnectionSO
	{
		protected NakedDevXeSSConnection _connection;

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
