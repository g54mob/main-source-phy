using System.Collections.Generic;
using HTraceAO.Scripts.Infrastructure.URP;

namespace Kamgam.SettingsGenerator
{
	public class AmbientOcclusionHConnection : ConnectionWithOptions<string>
	{
		protected List<string> _labels;

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

		public override void Set(int index)
		{
		}

		public override void OnQualityChanged(int qualityLevel)
		{
		}

		public HTraceAOVolume GetVolume()
		{
			return null;
		}
	}
}
