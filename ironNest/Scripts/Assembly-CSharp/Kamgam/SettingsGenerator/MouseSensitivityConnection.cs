using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class MouseSensitivityConnection : Connection<float>
	{
		private readonly Vector2 _inputRange;

		private readonly string _targetTag;

		private readonly bool _resolveEverySet;

		private readonly bool _logWarnings;

		private MouseSensitivitySetter _cachedSetter;

		public MouseSensitivityConnection(Vector2 inputRange, string targetTag, bool resolveEverySet, bool logWarnings)
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

		private MouseSensitivitySetter ResolveSetter(bool allowCache)
		{
			return null;
		}

		private float DefaultValue()
		{
			return 0f;
		}
	}
}
