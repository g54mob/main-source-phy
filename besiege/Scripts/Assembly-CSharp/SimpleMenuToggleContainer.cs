public class SimpleMenuToggleContainer
{
	public ToggleExtraOption togglePositionInExtraOptionsArray;

	public ExtraOption[] dropDownPositionsInExtraOptionsArray;

	public object[] sliderPositionsInExtraOptionsArray;

	public int containingTab;

	public SimpleMenuToggleContainer(int containingTab, ToggleExtraOption togglePositionInExtraOptionsArray, ExtraOption[] dropDownPositionsInExtraOptionsArray, object[] sliderPositionsInExtraOptionsArray)
	{
		this.containingTab = containingTab;
		this.togglePositionInExtraOptionsArray = togglePositionInExtraOptionsArray;
		this.dropDownPositionsInExtraOptionsArray = dropDownPositionsInExtraOptionsArray;
		this.sliderPositionsInExtraOptionsArray = sliderPositionsInExtraOptionsArray;
	}
}
