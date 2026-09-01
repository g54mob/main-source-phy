using System.Collections.Generic;

namespace Kamgam.SettingsGenerator
{
	public interface IConnectionWithOptions<TOption> : IConnection<int>, IConnection, IQualityChangeReceiver
	{
		bool HasOptions();

		List<TOption> GetOptionLabels();

		void SetOptionLabels(List<TOption> optionLabels);

		void RefreshOptionLabels();
	}
}
