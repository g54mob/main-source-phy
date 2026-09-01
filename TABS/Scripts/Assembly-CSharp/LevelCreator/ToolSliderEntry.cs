using System;
using UnityEngine;

namespace LevelCreator
{
	[Serializable]
	public class ToolSliderEntry : ToolControlUIEntry
	{
		[Space]
		public ToolControlSlider.SliderInfo m_sliderInfo;

		public OnValueChanged m_onValueChanged;
	}
}
