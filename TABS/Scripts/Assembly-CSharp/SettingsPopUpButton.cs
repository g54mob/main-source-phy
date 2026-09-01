using UnityEngine;

public class SettingsPopUpButton : MonoBehaviour
{
	public int value;

	public SettingsHandler settingsHandler;

	public MenuSettingsButton targetSettingsButton;

	public void Click()
	{
		settingsHandler.ClickSettingsPopUp(this);
	}
}
