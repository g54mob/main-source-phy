namespace Kamgam.SettingsGenerator
{
	public class OutlineConnection : Connection<bool>
	{
		private readonly string _targetTag;

		private readonly bool _resolveEverySet;

		private readonly bool _logWarnings;

		private OutlineController _cachedController;

		public OutlineConnection(string targetTag, bool resolveEverySet, bool logWarnings)
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

		private OutlineController ResolveController(bool allowCache)
		{
			return null;
		}

		private bool DefaultValue()
		{
			return false;
		}
	}
}
