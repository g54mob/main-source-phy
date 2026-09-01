namespace Kamgam.SettingsGenerator
{
	public class CatEyeColorConnection : Connection<int>
	{
		private CatCustomizationController catCustomization;

		public override int Get()
		{
			return 0;
		}

		public override void Set(int value)
		{
		}

		private void ResolveReferenceIfNeeded()
		{
		}
	}
}
