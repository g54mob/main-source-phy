using System;
using System.Collections.Generic;

namespace Kamgam.SettingsGenerator
{
	[Serializable]
	public class SettingFieldsData
	{
		public List<SettingData> Fields;

		public SettingFieldsData(List<SettingData> fields)
		{
		}
	}
}
