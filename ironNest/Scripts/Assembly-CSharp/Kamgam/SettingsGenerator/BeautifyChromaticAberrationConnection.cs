using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class BeautifyChromaticAberrationConnection : Connection<float>
	{
		private readonly Vector2 _inputRange;

		private readonly Vector2 _outputRange;

		private readonly BeautifyConnectionResolver _resolver;

		public BeautifyChromaticAberrationConnection(Vector2 inputRange, Vector2 outputRange, bool resolveEveryAccess, bool logWarnings)
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

		private float MapInputToOutput(float v)
		{
			return 0f;
		}

		private float MapOutputToInput(float v)
		{
			return 0f;
		}
	}
}
