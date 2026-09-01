using System.Collections.Generic;

namespace Kamgam.SettingsGenerator
{
	public class AlteregoFSR2Connection : ConnectionWithOptions<string>
	{
		protected List<string> _labels;

		public bool IsSupported()
		{
			return false;
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

		public override int Get()
		{
			return 0;
		}

		public static T GetFieldValue<T>(object obj, string fieldName)
		{
			return default(T);
		}

		public override void Set(int index)
		{
		}
	}
}
