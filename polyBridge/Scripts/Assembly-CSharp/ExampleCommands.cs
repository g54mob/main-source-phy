using UnityEngine;
using UnityEngine.SceneManagement;

public class ExampleCommands : MonoBehaviour
{
	private void Start()
	{
		uConsole.RegisterCommand("fov", "change field of view of main camera", fov);
		uConsole.RegisterCommand("load_scene", "sychronously load new scene", LoadScene);
	}

	private static void fov()
	{
		if (!Camera.main)
		{
			Debug.LogWarningFormat("Trying to set FOV but no main camera is defined");
			return;
		}
		if (uConsole.GetNumParameters() == 0)
		{
			Debug.LogFormat("Current Camera FOV is {0} degrees", Camera.main.fieldOfView);
			return;
		}
		float fieldOfView = uConsole.GetFloat();
		Camera.main.fieldOfView = fieldOfView;
		Debug.LogFormat("Camera FOV set to {0} degrees", Camera.main.fieldOfView);
	}

	public static void LoadScene()
	{
		SceneManager.LoadScene(uConsole.GetString());
		uConsole.TurnOff();
	}
}
