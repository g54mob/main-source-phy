namespace Kamgam.SettingsGenerator
{
	public class CatBodyColorConnection : Connection<int>
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
