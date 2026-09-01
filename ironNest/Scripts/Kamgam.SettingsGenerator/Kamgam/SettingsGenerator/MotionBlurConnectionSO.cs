using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "MotionBlurConnection", menuName = "SettingsGenerator/Connection/MotionBlurConnection", order = 4)]
	public class MotionBlurConnectionSO : BoolConnectionSO
	{
		protected MotionBlurConnection _connection;

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
