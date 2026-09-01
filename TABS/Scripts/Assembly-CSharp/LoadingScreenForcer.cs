using UnityEngine;

public class LoadingScreenForcer : MonoBehaviour
{
	private enum RunEvent
	{
		None = 0,
		Awake = 1,
		Start = 2
	}

	[SerializeField]
	private RunEvent runEvent = RunEvent.Start;

	private void Awake()
	{
		if (runEvent == RunEvent.Awake)
		{
			ForceLoadingScreen();
		}
	}

	private void Start()
	{
		if (runEvent == RunEvent.Start)
		{
			ForceLoadingScreen();
		}
	}

	private void ForceLoadingScreen()
	{
		ServiceLocator.GetService<LoadingScreenHandler>().HideLoadingScreen(null);
	}
}
