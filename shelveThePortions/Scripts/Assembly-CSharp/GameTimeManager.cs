using System.Collections;
using UnityEngine;

public class GameTimeManager : SceneSingleton<GameTimeManager>
{
	private GameTimeManagerUI gameTimeManagerUI;

	[HideInInspector]
	public int currentSeconds;

	[HideInInspector]
	public int currentMinutes;

	[HideInInspector]
	public int currentHours;

	private WaitForSecondsRealtime secondDelay = new WaitForSecondsRealtime(1f);

	private void Start()
	{
		gameTimeManagerUI = SceneSingleton<GameTimeManagerUI>.Instance;
		EventManager.GamePause += PauseTimer;
		EventManager.GameResume += ResumeTimer;
		EventManager.GameStart += StartTimer;
	}

	private void OnDisable()
	{
		EventManager.GamePause -= PauseTimer;
		EventManager.GameResume -= ResumeTimer;
		EventManager.GameStart -= StartTimer;
	}

	public void LoadTimer()
	{
		currentHours = SaveSystem.currentlevelSaveData.currentGameHours;
		currentMinutes = SaveSystem.currentlevelSaveData.currentGameMinutes;
		currentSeconds = SaveSystem.currentlevelSaveData.currentGameSeconds;
	}

	public void StartTimer()
	{
		StopAllCoroutines();
		StartCoroutine(TimerCoroutine());
	}

	public void PauseTimer()
	{
		StopAllCoroutines();
	}

	public void ResumeTimer()
	{
		StopAllCoroutines();
		StartCoroutine(TimerCoroutine());
	}

	private IEnumerator TimerCoroutine()
	{
		while (true)
		{
			if (currentSeconds == 60)
			{
				currentSeconds = 0;
				currentMinutes++;
			}
			if (currentMinutes == 60)
			{
				currentMinutes = 0;
				currentHours++;
			}
			if (gameTimeManagerUI != null)
			{
				gameTimeManagerUI.SetTimer(GetTimerString());
			}
			yield return secondDelay;
			currentSeconds++;
		}
	}

	public string GetTimerString()
	{
		return string.Format("{0}:{1}:{2}", currentHours.ToString("D2"), currentMinutes.ToString("D2"), currentSeconds.ToString("D2"));
	}

	public bool CanUpdateBestTime(int bestHours, int bestMinutes, int bestSeconds)
	{
		if (currentHours < bestHours)
		{
			return true;
		}
		if (currentMinutes < bestMinutes)
		{
			return true;
		}
		if (currentSeconds < bestSeconds)
		{
			return true;
		}
		return false;
	}

	public int GetCurrentGameTimeInSeconds()
	{
		return currentHours * 3600 + currentMinutes * 60 + currentSeconds;
	}
}
