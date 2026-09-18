using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class CutscenesManager : SceneSingleton<CutscenesManager>
{
	private ConversationPanelUI conversationPanelUI;

	[SerializeField]
	private GameObject tutorialEndAnim;

	[SerializeField]
	private GameObject gameEndAnim;

	[SerializeField]
	private CatAnimator counterCatAnimator;

	[SerializeField]
	private List<string> gameEndConversationLines;

	[Space]
	[SerializeField]
	private bool isDebugOn;

	private void Start()
	{
		conversationPanelUI = SceneSingleton<ConversationPanelUI>.Instance;
	}

	private void OnEnable()
	{
		EventManager.GameEnd += StartGameEnd;
	}

	private void OnDisable()
	{
		EventManager.GameEnd -= StartGameEnd;
	}

	[Button]
	public void StartTutorialEnd()
	{
		if (isDebugOn)
		{
			Debug.Log("StartTutorialEnd");
		}
		UIBackKeyManager.ClearQueue();
		Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.Door_Open);
		EventManager.ActivateEvent(EventTypes.CutsceneStart);
		tutorialEndAnim.SetActive(value: true);
	}

	public void TutorialEndCutsceneFinished()
	{
		if (isDebugOn)
		{
			Debug.Log("TutorialEndCutsceneFinished");
		}
		EventManager.ActivateEvent(EventTypes.CutsceneEnd);
		tutorialEndAnim.SetActive(value: false);
		if (Singleton<GameBuildManager>.Instance != null && Singleton<GameBuildManager>.Instance.IsFreeVersion())
		{
			EventManager.ActivateEvent(EventTypes.PlaytestEnd);
		}
		SaveSystem.SaveLevel();
	}

	private void StartGameEnd()
	{
		UIBackKeyManager.ClearQueue();
		Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.Ending_Bell);
		if (isDebugOn)
		{
			Debug.Log("StartGameEnd");
		}
		gameEndAnim.SetActive(value: true);
	}

	public void SetConversationLineIndex(int index)
	{
		Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_TutorialStepCompleted);
		conversationPanelUI.SetPanel(gameEndConversationLines[index]);
	}

	public void CounterCatEndGameAnimation()
	{
		counterCatAnimator.PlayEndGameClips();
	}

	public void GameEndCutsceneFinished()
	{
		if (isDebugOn)
		{
			Debug.Log("GameEndCutsceneFinished");
		}
		gameEndAnim.SetActive(value: false);
		conversationPanelUI.HidePanel();
		SceneSingleton<UIManager>.Instance.ShowGameOverMenu();
	}

	[Button]
	public void GameEndButton()
	{
		EventManager.ActivateEvent(EventTypes.GameEnd);
	}
}
