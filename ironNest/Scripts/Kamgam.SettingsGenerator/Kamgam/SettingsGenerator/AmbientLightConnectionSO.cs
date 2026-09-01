using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "AmbientLightConnection", menuName = "SettingsGenerator/Connection/AmbientLightConnection", order = 4)]
	public class AmbientLightConnectionSO : FloatConnectionSO
	{
		[Tooltip("The allowed min ambient color intensity.\nOnly used if ambient light is set to color.\nUseful to avoid losing the color info if ambient is set to black.\nNOTICE: This is only used in Built-In and URP, not HDRP.")]
		public float MinColorIntensity;

		[Tooltip("Max color intensity. 2f by default. You may want to change it depending on your ambient color (HD or SD).\nOnly used if ambient light is set to color.\nNOTICE: This is only used in Built-In and URP, not HDRP.")]
		public float MaxColorIntensity;

		protected AmbientLightConnection _connection;

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
