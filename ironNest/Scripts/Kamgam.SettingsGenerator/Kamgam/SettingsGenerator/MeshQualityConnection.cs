using System.Collections.Generic;

namespace Kamgam.SettingsGenerator
{
	public class MeshQualityConnection : ConnectionWithOptions<string>
	{
		protected List<string> _labels;

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

		public override void OnQualityChanged(int qualityLevel)
		{
		}
	}
}
