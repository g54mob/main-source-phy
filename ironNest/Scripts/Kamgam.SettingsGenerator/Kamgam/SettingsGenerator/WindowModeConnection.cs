using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class WindowModeConnection : ConnectionWithOptions<string>
	{
		protected List<FullScreenMode> _values;

		protected List<string> _labels;

		protected FullScreenMode? lastKnownMode;

		protected int lastSetFrame;

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

		protected virtual List<FullScreenMode> getWindowOptions()
		{
			return null;
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
