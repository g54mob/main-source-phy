using UnityEngine;

public class QuitButton : ClickBehaviour
{
	private void Awake()
	{
		releaseOnlyOver = true;
	}

	public override void OnClickReleased()
	{
		Application.Quit();
	}
}
