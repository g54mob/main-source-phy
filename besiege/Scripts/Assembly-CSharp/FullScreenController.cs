using UnityEngine;

public class FullScreenController : ClickBehaviour
{
	public Material redMaterial;

	public Material darkMaterial;

	public void Default()
	{
	}

	public override void OnClicked()
	{
		Set();
	}

	private void Set()
	{
		OptionsMaster.BesiegeConfig.WindowedMode = !OptionsMaster.BesiegeConfig.WindowedMode;
		if (!OptionsMaster.BesiegeConfig.WindowedMode)
		{
			Screen.fullScreen = true;
			GetComponent<Renderer>().material = darkMaterial;
		}
		else
		{
			Screen.fullScreen = false;
			GetComponent<Renderer>().material = redMaterial;
		}
	}
}
