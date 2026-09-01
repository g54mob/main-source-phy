using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator
{
	public class ShadowResolutionConnection : ConnectionWithOptions<string>
	{
		public static bool SetAdditionalLightResolution;

		public static int AdditionalToMainResolutionFactor;

		protected List<int> _values;

		protected List<string> _labels;

		private static void setResolution(UniversalRenderPipelineAsset asset, int resolution)
		{
		}

		public override List<string> GetOptionLabels()
		{
			return null;
		}

		public override void SetOptionLabels(List<string> optionLabels)
		{
		}

		public override void RefreshOptionLabels()
		{
		}

		public override int Get()
		{
			return 0;
		}

		private List<int> getResolutions()
		{
			return null;
		}

		public override void Set(int index)
		{
		}
	}
}
