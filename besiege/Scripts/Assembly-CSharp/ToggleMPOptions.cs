using UnityEngine;

public class ToggleMPOptions : ClickBehaviour
{
	public GameObject settingsWindow;

	public override void OnClicked()
	{
		settingsWindow.SetActive(true);
	}
}
