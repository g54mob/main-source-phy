using UnityEngine;

public class AnisotropicDropDownElement : ClickBehaviour
{
	public AnisotropicFiltering type;

	public override void OnClicked()
	{
		QualitySettings.anisotropicFiltering = type;
	}
}
