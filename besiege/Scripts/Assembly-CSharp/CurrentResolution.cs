using UnityEngine;

public class CurrentResolution : MonoBehaviour
{
	public TextMesh resText;

	private void OnStart()
	{
		Set();
	}

	public void Set()
	{
		resText.text = Screen.currentResolution.width + " x " + Screen.currentResolution.height;
	}

	private void Set(Resolution currentResolution)
	{
		resText.text = currentResolution.width + " x " + currentResolution.height;
	}
}
