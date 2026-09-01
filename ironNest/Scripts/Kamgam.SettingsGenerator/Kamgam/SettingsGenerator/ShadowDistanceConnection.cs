using System.Collections.Generic;

namespace Kamgam.SettingsGenerator
{
	public class ShadowDistanceConnection : ConnectionWithOptions<string>
	{
		public List<float> QualityDistances;

		public bool UseQualitySettingsAsFallback;

		protected List<float> _distancesFromSettings;

		protected List<string> _labels;

		public ShadowDistanceConnection(List<float> qualityDistances, bool useQualitySettingsAsFallback = true)
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

		private List<float> getDistances()
		{
			return null;
		}

		public override void Set(int index)
		{
		}
	}
}
