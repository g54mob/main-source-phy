using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class ClipboardOffsetConnection : Connection<float>
	{
		private readonly Vector2 _inputRange;

		private readonly Vector2 _outputUnitsRange;

		private readonly string _targetTag;

		private readonly bool _resolveEverySet;

		private readonly bool _logWarnings;

		private ClipboardAspectRatioOffsetFader _cachedFader;

		public ClipboardOffsetConnection(Vector2 inputRange, Vector2 outputUnitsRange, string targetTag, bool resolveEverySet, bool logWarnings)
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

		private ClipboardAspectRatioOffsetFader ResolveFader(bool allowCache)
		{
			return null;
		}

		private float MapUiToUnits(float uiValue)
		{
			return 0f;
		}

		private float MapUnitsToUi(float units)
		{
			return 0f;
		}

		private float DefaultUiValue()
		{
			return 0f;
		}
	}
}
