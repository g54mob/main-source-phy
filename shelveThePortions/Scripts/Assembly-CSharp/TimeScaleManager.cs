using UnityEngine;

public class TimeScaleManager : Singleton<TimeScaleManager>
{
	[SerializeField]
	private float debug_StartingTimeScale = 1f;

	private void Start()
	{
		EventManager.MainMenuLoaded += SetTimeScaleToNormal;
		EventManager.GameStart += SetTimeScaleToNormal;
		EventManager.GamePause += PauseGame;
		EventManager.GameResume += ResumeGame;
		EventManager.PlaytestEnd += PauseGame;
	}

	private void OnDisable()
	{
		EventManager.MainMenuLoaded -= SetTimeScaleToNormal;
		EventManager.GameStart -= SetTimeScaleToNormal;
		EventManager.GamePause -= PauseGame;
		EventManager.GameResume -= ResumeGame;
		EventManager.PlaytestEnd -= PauseGame;
	}

	private void SetTimeScaleToNormal()
	{
		ResumeGame();
	}

	private void PauseGame()
	{
		SetTimeSpeed(0f);
	}

	private void ResumeGame()
	{
		SetTimeSpeed(1f);
	}

	private void SetTimeSpeed(float speed)
	{
		Time.timeScale = speed;
	}
}
