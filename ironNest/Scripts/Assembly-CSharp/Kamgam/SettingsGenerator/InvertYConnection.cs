namespace Kamgam.SettingsGenerator
{
	public class InvertYConnection : Connection<bool>
	{
		private readonly string _targetTag;

		private FirstPersonController _cachedController;

		public InvertYConnection(string targetTag)
		{
		}

		public new void Destroy()
		{
		}

		public override bool Get()
		{
			return false;
		}

		public override void Set(bool uiValue)
		{
		}

		private FirstPersonController ResolveController()
		{
			return null;
		}

		private bool DefaultValue()
		{
			return false;
		}
	}
}
