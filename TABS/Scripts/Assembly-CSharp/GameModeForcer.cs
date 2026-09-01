using Landfall.TABS.GameMode;
using UnityEngine;

public class GameModeForcer : MonoBehaviour
{
	[Tooltip("Do not set to 'All' in the editor.")]
	[SerializeField]
	private GameModeState m_gameModeState;

	[SerializeField]
	private bool m_runOnAwake;

	[SerializeField]
	private bool m_runOnStart = true;

	private void Awake()
	{
		if (m_runOnAwake)
		{
			ForceMode();
		}
		if (m_runOnAwake && m_runOnStart)
		{
			Debug.LogError("Can't run GameModeForcer in RunOnAwake AND RunOnStart. Only running in awake, please uncheck either RunOnAwake or RunOnStart!", base.gameObject);
		}
	}

	private void Start()
	{
		if (!m_runOnAwake && m_runOnStart)
		{
			ForceMode();
		}
		if (!m_runOnAwake && !m_runOnStart)
		{
			Debug.LogError("Didn't run GameModeForcer, neither RunOnAwake or RunOnStart were checked.", base.gameObject);
		}
	}

	private void ForceMode()
	{
		switch (m_gameModeState)
		{
		case GameModeState.Menu:
			ServiceLocator.GetService<GameModeService>().SetGameMode<MainMenuGameMode>();
			break;
		case GameModeState.Campaign:
			ServiceLocator.GetService<GameModeService>().SetGameMode<CampaignGameMode>();
			break;
		case GameModeState.Sandbox:
			ServiceLocator.GetService<GameModeService>().SetGameMode<SandboxGameMode>();
			break;
		case GameModeState.Menu | GameModeState.Sandbox:
			break;
		}
	}
}
