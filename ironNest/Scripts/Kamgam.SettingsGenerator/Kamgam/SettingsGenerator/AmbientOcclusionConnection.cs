using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator
{
	public class AmbientOcclusionConnection : Connection<bool>
	{
		public const float OFF_INTENSITY = 0.001f;

		public static bool UseActiveStateToDisable;

		protected Dictionary<UniversalRenderPipelineAsset, float> _lastKnownIntensities;

		protected ScriptableRenderer getRenderer()
		{
			return null;
		}

		private static float getIntensity(UniversalRenderPipelineAsset rpAsset)
		{
			return 0f;
		}

		private static void setIntensity(UniversalRenderPipelineAsset rpAsset, float intensity)
		{
		}

		public override bool Get()
		{
			return false;
		}

		public override void Set(bool enable)
		{
		}

		protected void updateLastKnownIntensity(UniversalRenderPipelineAsset rpAsset)
		{
		}
	}
}
