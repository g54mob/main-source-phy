using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "RenderScaleConnection", menuName = "SettingsGenerator/Connection/RenderScaleConnection", order = 4)]
	public class RenderScaleConnectionSO : FloatConnectionSO
	{
		public bool ReapplyOnQualityChange;

		public float DefaultRenderScale;

		protected RenderScaleConnection _connection;

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
