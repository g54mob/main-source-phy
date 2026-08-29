using System.Collections.Generic;

public class PineUIState
{
	public List<ButtonDelegate> buttonClickDelegates;

	public List<ButtonDelegate> buttonClickPostDelegates;

	public List<ButtonDelegate> buttonDownDelegates;

	public List<ButtonDelegate> buttonUpDelegates;

	public List<ButtonInteractionDelegate> buttonInteractionDelegates;

	public List<ToggleDelegate> toggleDelegates;

	public List<SliderDelegate> sliderDelegates;

	public List<DropdownDelegate> dropdownDelegates;
}
