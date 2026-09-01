using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class ControllerSensitivityConnection : Connection<float>
	{
		private readonly Vector2 _inputRange;

		private readonly string _targetTag;

		private readonly bool _resolveEverySet;

		private readonly bool _logWarnings;

		private ControllerSensitivitySetter _cachedSetter;

		public ControllerSensitivityConnection(Vector2 inputRange, string targetTag, bool resolveEverySet, bool logWarnings)
		{
		}

		public new void Destroy()
		{
		}

		public override float Get()
		{
			return 0f;
		}

		public override void Set(float uiValue)
		{
		}

		private ControllerSensitivitySetter ResolveSetter(bool allowCache)
		{
			return null;
		}

		private float DefaultValue()
		{
			return 0f;
		}
	}
}
