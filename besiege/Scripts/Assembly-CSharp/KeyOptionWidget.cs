using Selectors;
using UnityEngine;

public class KeyOptionWidget : BaseOptionWidget
{
	[SerializeField]
	private ControlSelector selector;

	private MainOptionsMenu.OptionsCategory.ControlsOption controlOption;

	public override void Set(MainOptionsMenu.OptionsCategory.MenuOption option)
	{
		controlOption = option as MainOptionsMenu.OptionsCategory.ControlsOption;
		UpdateVisual();
	}

	public override void UpdateVisual()
	{
		selector.DisplayOptions(controlOption.SplitLocID, controlOption.getFunc());
	}
}
