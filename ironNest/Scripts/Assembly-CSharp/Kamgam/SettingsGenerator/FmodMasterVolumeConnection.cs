using FMOD.Studio;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class FmodMasterVolumeConnection : Connection<float>
	{
		private readonly Vector2 _inputRange;

		private readonly Vector2 _outputLinearRange;

		private readonly string _busPath;

		private Bus _bus;

		private bool _busResolved;

		public FmodMasterVolumeConnection(Vector2 inputRange, Vector2 outputLinearRange, string busPath)
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

		private void ResolveBusIfNeeded()
		{
		}

		private float MapUiToLinear(float uiValue)
		{
			return 0f;
		}

		private float MapLinearToUi(float linear)
		{
			return 0f;
		}

		private float DefaultUiValue()
		{
			return 0f;
		}
	}
}
