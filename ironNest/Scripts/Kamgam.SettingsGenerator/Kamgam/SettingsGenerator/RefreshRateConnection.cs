using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class RefreshRateConnection : ConnectionWithOptions<string>
	{
		public bool CacheRefreshRates;

		public bool LimitToCurrentResolution;

		public int MinRate;

		public int MaxRate;

		protected List<RefreshRate> _values;

		protected List<string> _labels;

		protected string _rateNameInOptionLabel;

		protected RefreshRate? lastKnownRefreshRate;

		protected int lastSetFrame;

		protected List<RefreshRate> getRefreshRates()
		{
			return null;
		}

		protected bool contains(List<RefreshRate> rates, RefreshRate rate)
		{
			return false;
		}

		public override List<string> GetOptionLabels()
		{
			return null;
		}

		public override void RefreshOptionLabels()
		{
		}

		public override void SetOptionLabels(List<string> optionLabels)
		{
		}

		public void SetOptionLabel(string rateNameInOptionLabel)
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
