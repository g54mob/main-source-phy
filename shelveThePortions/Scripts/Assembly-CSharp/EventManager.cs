public class EventManager : Singleton<EventManager>
{
	public delegate void Event();

	public static event Event GameStart;

	public static event Event GamePause;

	public static event Event GameRestart;

	public static event Event GameResume;

	public static event Event GameEnd;

	public static event Event GameLoading;

	public static event Event MainMenuLoaded;

	public static event Event OnResulationChanged;

	public static event Event OnControlsChange;

	public static event Event CatUILoaded;

	public static event Event PuzzleUILoaded;

	public static event Event OnCutsceneStart;

	public static event Event OnCutsceneEnd;

	public static event Event PlaytestEnd;

	public static void ActivateEvent(EventTypes x)
	{
		switch (x)
		{
		case EventTypes.GameStart:
			EventManager.GameStart?.Invoke();
			break;
		case EventTypes.GameEnd:
			EventManager.GameEnd?.Invoke();
			break;
		case EventTypes.GameRestart:
			EventManager.GameRestart?.Invoke();
			break;
		case EventTypes.GamePause:
			EventManager.GamePause?.Invoke();
			break;
		case EventTypes.GameResume:
			EventManager.GameResume?.Invoke();
			break;
		case EventTypes.GameLoading:
			EventManager.GameLoading?.Invoke();
			break;
		case EventTypes.MainMenuLoaded:
			EventManager.MainMenuLoaded?.Invoke();
			break;
		case EventTypes.OnResulationChanged:
			EventManager.OnResulationChanged?.Invoke();
			break;
		case EventTypes.OnControlsChange:
			EventManager.OnControlsChange?.Invoke();
			break;
		case EventTypes.CatUILoaded:
			EventManager.CatUILoaded?.Invoke();
			break;
		case EventTypes.PuzzleUILoaded:
			EventManager.PuzzleUILoaded?.Invoke();
			break;
		case EventTypes.CutsceneStart:
			EventManager.OnCutsceneStart?.Invoke();
			break;
		case EventTypes.CutsceneEnd:
			EventManager.OnCutsceneEnd?.Invoke();
			break;
		case EventTypes.PlaytestEnd:
			EventManager.PlaytestEnd?.Invoke();
			break;
		}
	}
}
