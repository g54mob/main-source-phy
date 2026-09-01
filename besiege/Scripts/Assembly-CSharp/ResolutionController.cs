using UnityEngine;

public class ResolutionController : ClickBehaviour
{
	public int myResX = 640;

	public int myResY = 480;

	public void Set(int x, int y)
	{
		myResX = x;
		myResY = y;
	}

	public override void OnClicked()
	{
		OptionsMaster.BesiegeConfig.ScreenWidth = myResX;
		OptionsMaster.BesiegeConfig.ScreenHeight = myResY;
		Screen.SetResolution(OptionsMaster.BesiegeConfig.ScreenWidth, OptionsMaster.BesiegeConfig.ScreenHeight, !OptionsMaster.BesiegeConfig.WindowedMode);
	}
}
