using System.Collections;
using UnityEngine;

public class SetWindowedModeOnLoad : MonoBehaviour
{
	private IEnumerator Start()
	{
		yield return null;
		if (!OptionsMaster.BesiegeConfig.FirstTimePlaying)
		{
			CheckRes();
		}
		CheckFullscreen();
	}

	private void CheckRes()
	{
		Screen.SetResolution(OptionsMaster.BesiegeConfig.ScreenWidth, OptionsMaster.BesiegeConfig.ScreenHeight, !OptionsMaster.BesiegeConfig.WindowedMode);
	}

	private void CheckFullscreen()
	{
		Screen.fullScreen = !OptionsMaster.BesiegeConfig.WindowedMode;
	}
}
