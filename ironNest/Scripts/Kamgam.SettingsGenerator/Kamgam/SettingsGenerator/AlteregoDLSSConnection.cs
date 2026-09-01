using System.Collections.Generic;

namespace Kamgam.SettingsGenerator
{
	public class AlteregoDLSSConnection : ConnectionWithOptions<string>
	{
		protected List<string> _labels;

		public bool CheckForCameraMarker;

		protected List<int> _enumOptionsAsIntegers;

		public bool IsSupported()
		{
			return false;
		}

		public override List<string> GetOptionLabels()
		{
			return null;
		}

		protected List<int> getOptionsEnumList()
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
