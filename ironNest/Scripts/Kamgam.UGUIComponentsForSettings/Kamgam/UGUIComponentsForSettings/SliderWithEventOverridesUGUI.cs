using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings
{
	public class SliderWithEventOverridesUGUI : Slider
	{
		public Func<AxisEventData, bool> OnMoveOverride;

		public override void OnMove(AxisEventData eventData)
		{
		}
	}
}
