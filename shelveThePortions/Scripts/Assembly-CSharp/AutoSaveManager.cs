using UnityEngine;

public class AutoSaveManager : Singleton<AutoSaveManager>
{
	private float startingTime;

	private float targetTimer;

	[SerializeField]
	private bool disableAutoSave;

	[SerializeField]
	private float autoSaveTimer = 600f;

	private bool isAutoSavingOn;

	private void OnEnable()
	{
		startingTime = Time.unscaledTime;
		targetTimer = autoSaveTimer;
		EventManager.MainMenuLoaded += PauseAutoSave;
		EventManager.GameStart += ResumeAutoSave;
		EventManager.GameResume += ResumeAutoSave;
		EventManager.GamePause += PauseAutoSave;
	}

	private void OnDisable()
	{
		EventManager.MainMenuLoaded -= PauseAutoSave;
		EventManager.GameStart -= ResumeAutoSave;
		EventManager.GameResume -= ResumeAutoSave;
		EventManager.GamePause -= PauseAutoSave;
	}

	private void PauseAutoSave()
	{
		isAutoSavingOn = false;
		targetTimer -= Time.unscaledTime - startingTime;
	}

	private void ResumeAutoSave()
	{
		isAutoSavingOn = true;
		startingTime = Time.unscaledTime;
	}

	private void Update()
	{
		if (!disableAutoSave && isAutoSavingOn && Time.unscaledTime - startingTime > targetTimer)
		{
			SaveSystem.SaveLevel();
			startingTime = Time.unscaledTime;
			targetTimer = autoSaveTimer;
			if (SceneSingleton<AutoSavePanelUI>.Instance != null)
			{
				SceneSingleton<AutoSavePanelUI>.Instance.ShowPanel();
			}
		}
	}
}
