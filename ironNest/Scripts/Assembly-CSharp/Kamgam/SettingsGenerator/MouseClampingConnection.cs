namespace Kamgam.SettingsGenerator
{
	public class MouseClampingConnection : Connection<bool>
	{
		private readonly string _targetTag;

		private readonly bool _resolveEverySet;

		private readonly bool _logWarnings;

		private DynamicCursorManager _cachedController;

		public MouseClampingConnection(string targetTag, bool resolveEverySet, bool logWarnings)
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

		private DynamicCursorManager ResolveController(bool allowCache)
		{
			return null;
		}

		private bool DefaultValue()
		{
			return false;
		}
	}
}
