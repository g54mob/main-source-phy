using System.Collections;
using UnityEngine;

public class AutoSetResolution : ClickBehaviour
{
	public Vector2 screenSize;

	public override void OnClicked()
	{
		StartCoroutine(IEClick());
	}

	public IEnumerator IEClick()
	{
		Resolution[] resolutions = Screen.resolutions;
		screenSize.x = resolutions[resolutions.Length - 1].width;
		screenSize.y = resolutions[resolutions.Length - 1].height;
		OptionsMaster.BesiegeConfig.ScreenWidth = Mathf.RoundToInt(screenSize.x);
		OptionsMaster.BesiegeConfig.ScreenHeight = Mathf.RoundToInt(screenSize.y);
		yield return null;
		Screen.SetResolution(OptionsMaster.BesiegeConfig.ScreenWidth, OptionsMaster.BesiegeConfig.ScreenHeight, !OptionsMaster.BesiegeConfig.WindowedMode);
	}
}
