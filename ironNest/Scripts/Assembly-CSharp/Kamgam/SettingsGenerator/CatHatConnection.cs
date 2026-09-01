namespace Kamgam.SettingsGenerator
{
	public class CatHatConnection : Connection<int>
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
