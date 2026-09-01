using System;

public class LabelCopyButton : ClickBehaviour
{
	public Action ButtonClicked;

	public override void OnClicked()
	{
		if (ButtonClicked != null)
		{
			ButtonClicked();
		}
	}
}
