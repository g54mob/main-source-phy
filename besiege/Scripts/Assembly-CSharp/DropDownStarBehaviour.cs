public class DropDownStarBehaviour : ClickBehaviour
{
	public string myValue;

	public ExtraOption extraOption;

	public JustAnotherScalingScript justAnotherScalingScript;

	private void OnMouseEnter()
	{
		justAnotherScalingScript.SetGoal(1f);
	}

	private void OnMouseExit()
	{
		justAnotherScalingScript.SetGoal(0f);
	}

	public override void OnClicked()
	{
		justAnotherScalingScript.SetCurrent(0.5f);
		extraOption.SetValue(myValue);
	}
}
