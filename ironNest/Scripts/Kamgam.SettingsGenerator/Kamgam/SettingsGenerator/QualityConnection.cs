using System;
using System.Collections.Generic;

namespace Kamgam.SettingsGenerator
{
	public class QualityConnection : ConnectionWithOptions<string>, IConnectionWithSettingsAccess
	{
		public Settings Settings;

		protected List<string> _labels;

		protected List<int> _values;

		[Obsolete("QualityConnection(Settings settings) constuctor is deprecated. Use the default constructor and SetSettings(Settings settings) instead.")]
		public QualityConnection(Settings settings)
		{
		}

		public QualityConnection()
		{
		}

		public override int GetOrder()
		{
			return 0;
		}

		public override int Get()
		{
			return 0;
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

		public override void Set(int value)
		{
		}

		public void SetSettings(Settings settings)
		{
		}

		public Settings GetSettings()
		{
			return null;
		}
	}
}
