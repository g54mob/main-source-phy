using System.Collections.Generic;

namespace Kamgam.SettingsGenerator
{
	public class FrameRateConnection : ConnectionWithOptions<string>
	{
		public bool RemoveUnlimited;

		public List<int> CustomFrameRates;

		public List<int> _values;

		public List<string> _labels;

		protected List<int> getFrameRates()
		{
			return null;
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

		public override void Set(int index)
		{
		}
	}
}
