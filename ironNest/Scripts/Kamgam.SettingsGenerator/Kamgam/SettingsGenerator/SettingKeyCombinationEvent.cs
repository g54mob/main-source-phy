using System;
using UnityEngine.Events;

namespace Kamgam.SettingsGenerator
{
	public class SettingKeyCombinationEvent : SettingEvent<KeyCombination>
	{
		[NonSerialized]
		protected KeyCombination _combo;

		public UnityEvent<KeyCombination> OnDown;

		public UnityEvent<KeyCombination> OnUp;

		public UnityEvent<KeyCombination> OnHold;

		public override SettingData.DataType[] GetSupportedDataTypes()
		{
			return null;
		}

		public override void TriggerEvent()
		{
		}

		public void Update()
		{
		}
	}
}
