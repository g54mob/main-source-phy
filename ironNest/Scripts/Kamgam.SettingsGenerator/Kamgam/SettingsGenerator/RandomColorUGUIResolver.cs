using Kamgam.UGUIComponentsForSettings;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[AddComponentMenu("UI/Settings/RandomColorUGUIResolver")]
	[RequireComponent(typeof(RandomColorUGUI))]
	public class RandomColorUGUIResolver : SettingResolver, ISettingResolver
	{
		protected SettingData.DataType[] supportedDataTypes;

		protected RandomColorUGUI randomColorUGUI;

		protected bool stopPropagation;

		public RandomColorUGUI RandomColorUGUI => null;

		public override SettingData.DataType[] GetSupportedDataTypes()
		{
			return null;
		}

		public override void Start()
		{
		}

		public override void OnDestroy()
		{
		}

		private void onColorChanged(Color color)
		{
		}

		public override void Refresh()
		{
		}
	}
}
