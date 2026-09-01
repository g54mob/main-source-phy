using UnityEngine;

public class SetButtonCustomResolution : ClickBehaviour
{
	public CustomResolutionController myResX;

	public CustomResolutionController myResY;

	public override void OnClicked()
	{
		OptionsMaster.BesiegeConfig.ScreenWidth = myResX.myResX;
		OptionsMaster.BesiegeConfig.ScreenHeight = myResY.myResY;
		Screen.SetResolution(OptionsMaster.BesiegeConfig.ScreenWidth, OptionsMaster.BesiegeConfig.ScreenHeight, false);
	}
}
