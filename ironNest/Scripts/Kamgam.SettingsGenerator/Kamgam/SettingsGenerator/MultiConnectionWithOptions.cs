using System.Collections.Generic;

namespace Kamgam.SettingsGenerator
{
	public class MultiConnectionWithOptions<TOption> : MultiConnection<int>, IConnectionWithOptions<TOption>, IConnection<int>, IConnection, IQualityChangeReceiver
	{
		public bool HasOptions()
		{
			return false;
		}

		public List<TOption> GetOptionLabels()
		{
			return null;
		}

		public void SetOptionLabels(List<TOption> optionLabels)
		{
		}

		public void RefreshOptionLabels()
		{
		}
	}
}
