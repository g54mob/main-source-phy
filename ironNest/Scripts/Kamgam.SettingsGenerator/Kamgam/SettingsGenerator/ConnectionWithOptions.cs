using System.Collections.Generic;

namespace Kamgam.SettingsGenerator
{
	public abstract class ConnectionWithOptions<TOption> : Connection<int>, IConnectionWithOptions<TOption>, IConnection<int>, IConnection, IQualityChangeReceiver
	{
		public bool HasOptions()
		{
			return false;
		}

		public abstract List<TOption> GetOptionLabels();

		public abstract void SetOptionLabels(List<TOption> optionLabels);

		public abstract void RefreshOptionLabels();
	}
}
