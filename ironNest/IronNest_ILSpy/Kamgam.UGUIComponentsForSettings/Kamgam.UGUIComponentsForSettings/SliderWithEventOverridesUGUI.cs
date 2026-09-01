using System;
using Cpp2ILInjected;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings;

public class SliderWithEventOverridesUGUI : Slider
{
	public Func<AxisEventData, bool> OnMoveOverride;

	public override void OnMove(AxisEventData eventData)
	{
		if (OnMoveOverride != null)
		{
			Func<AxisEventData, bool> onMoveOverride = OnMoveOverride;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v18 @ rcx_v4 (System.Func`2<UnityEngine.EventSystems.AxisEventData, System.Boolean>)+18] (should have been resolved before IL gen)");
			object obj = default(object);
			if (obj == null)
			{
				return;
			}
		}
		base.OnMove(eventData);
	}
}
