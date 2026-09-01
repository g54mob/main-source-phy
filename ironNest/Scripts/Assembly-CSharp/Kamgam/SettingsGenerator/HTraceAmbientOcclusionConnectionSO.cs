using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "HTraceAmbientOcclusionConnection", menuName = "SettingsGenerator/Connection/Graphics/HTraceAmbientOcclusion", order = 51)]
	public class HTraceAmbientOcclusionConnectionSO : BoolConnectionSO
	{
		private HTraceAmbientOcclusionConnection _connection;

		public override IConnection<bool> GetConnection()
		{
			return null;
		}

		private void Create()
		{
		}

		public override void DestroyConnection()
		{
		}
	}
}
