using System.Collections.Generic;
using TND.Upscaling.Framework;

namespace Kamgam.SettingsGenerator
{
	public class TheNakedDevUpscalerConnection : ConnectionWithOptions<string>
	{
		protected List<string> _labels;

		protected List<UpscalerQuality> _labelQualities;

		public bool CheckForCameraMarker;

		protected bool _explicitOffValueExists;

		public TNDUpscaler GetUtils()
		{
			return null;
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
