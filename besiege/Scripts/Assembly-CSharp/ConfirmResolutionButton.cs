using UnityEngine;

public class ConfirmResolutionButton : ClickBehaviour
{
	public Resolution resolution;

	public bool customResolution;

	public ResolutionNumberField xBox;

	public ResolutionNumberField yBox;

	public GameObject confirmationMenu;

	public ConfirmResolutionChange confirmResolutionChange;

	public CurrentResolution resText;

	public FullScreenController windowBox;

	public override void OnClicked()
	{
		if (customResolution)
		{
			resolution = Screen.currentResolution;
			OptionsMaster.BesiegeConfig.ScreenHeight = yBox.myValue;
			Screen.SetResolution(OptionsMaster.BesiegeConfig.ScreenWidth, OptionsMaster.BesiegeConfig.ScreenHeight, !OptionsMaster.BesiegeConfig.WindowedMode);
			confirmationMenu.SetActive(true);
			confirmResolutionChange.BeginCountdown(resolution, Screen.currentResolution);
			return;
		}
		if (ConfirmResolutionChange.FullscreenConfirm)
		{
			Screen.fullScreen = !Screen.fullScreen;
			OptionsMaster.BesiegeConfig.WindowedMode = !Screen.fullScreen;
		}
		else
		{
			Screen.SetResolution(resolution.width, resolution.height, !OptionsMaster.BesiegeConfig.WindowedMode);
		}
		windowBox.Default();
		ConfirmResolutionChange.AwaitingConfirmation = false;
		confirmationMenu.SetActive(false);
	}
}
