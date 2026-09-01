using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class MultiColorConnection : ConnectionWithOptions<Color>
	{
		private List<Color> _colors;

		private int _selectedIndex;

		public MultiColorConnection(int selectedIndex)
		{
		}

		public override void SetOptionLabels(List<Color> colors)
		{
		}

		public override void RefreshOptionLabels()
		{
		}

		public override int Get()
		{
			return 0;
		}

		public override List<Color> GetOptionLabels()
		{
			return null;
		}

		public override void Set(int selectedIndex)
		{
		}
	}
}
