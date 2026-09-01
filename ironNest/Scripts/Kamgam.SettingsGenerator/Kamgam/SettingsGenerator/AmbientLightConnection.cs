namespace Kamgam.SettingsGenerator
{
	public class AmbientLightConnection : Connection<float>
	{
		public float MinColorIntensity;

		public float MaxColorIntensity;

		public override float Get()
		{
			return 0f;
		}

		public override void Set(float intensity)
		{
		}
	}
}
