namespace Kamgam.SettingsGenerator
{
	public class CatEnabledConnection : Connection<bool>
	{
		private CatCustomizationController catCustomization;

		public override bool Get()
		{
			return false;
		}

		public override void Set(bool value)
		{
		}

		private void ResolveReferenceIfNeeded()
		{
		}
	}
}
