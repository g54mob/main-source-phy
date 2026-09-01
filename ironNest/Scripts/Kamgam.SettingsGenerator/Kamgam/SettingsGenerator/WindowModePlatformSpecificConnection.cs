using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class WindowModePlatformSpecificConnection : WindowModeConnection
	{
		public override List<string> GetOptionLabels()
		{
			return null;
		}

		protected override List<FullScreenMode> getWindowOptions()
		{
			return null;
		}
	}
}
