using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuQuitGame : MonoBehaviour
{
	private void Awake()
	{
		Button component = GetComponent<Button>();
		if (component != null)
		{
			component.onClick.AddListener(QuitGame);
		}
	}

	private void QuitGame()
	{
		Process.GetCurrentProcess().Kill();
	}
}
