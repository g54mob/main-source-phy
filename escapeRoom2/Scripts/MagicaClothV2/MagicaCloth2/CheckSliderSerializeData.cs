using System;
using UnityEngine;

namespace MagicaCloth2
{
	[Serializable]
	public class CheckSliderSerializeData
	{
		public float value;

		public bool use;

		public CheckSliderSerializeData()
		{
		}

		public CheckSliderSerializeData(bool use, float value)
		{
			this.use = use;
			this.value = value;
		}

		public float GetValue(float unusedValue)
		{
			if (!use)
			{
				return unusedValue;
			}
			return value;
		}

		public void SetValue(bool use, float value)
		{
			this.use = use;
			this.value = value;
		}

		public void DataValidate(float min, float max)
		{
			value = Mathf.Clamp(value, min, max);
		}

		public CheckSliderSerializeData Clone()
		{
			return new CheckSliderSerializeData
			{
				value = value,
				use = use
			};
		}
	}
}
