using Landfall.TABS;
using TFBGames;
using UnityEngine;

public class PlayerBattleStatusUI : MonoBehaviour
{
	[SerializeField]
	private Team m_team;

	private CodeAnimation m_codeAnimation;

	private INetworkService m_networkService;

	private NetworkBattleController m_networkBattleController;

	private void Start()
	{
		m_codeAnimation = GetComponent<CodeAnimation>();
		m_networkService = ServiceLocator.GetService<INetworkService>();
		m_networkBattleController = ServiceLocator.GetService<NetworkBattleController>();
		if (!(m_networkBattleController == null))
		{
			m_networkBattleController.PhaseChanged += OnPhaseChanged;
			m_networkBattleController.RemotePhaseChanged += OnRemotePhaseChanged;
		}
	}

	private void OnDestroy()
	{
		if (!(m_networkBattleController == null))
		{
			m_networkBattleController.PhaseChanged -= OnPhaseChanged;
			m_networkBattleController.RemotePhaseChanged -= OnRemotePhaseChanged;
		}
	}

	private void OnPhaseChanged(NetworkGamePhase oldPhase, NetworkGamePhase newPhase)
	{
		if (newPhase == NetworkGamePhase.RequestBattleEnd && m_networkService.PlayerTeam == m_team)
		{
			Show();
		}
		else
		{
			Hide();
		}
	}

	private void OnRemotePhaseChanged(NetworkGamePhase oldRemotePhase, NetworkGamePhase newRemotePhase)
	{
		if (newRemotePhase == NetworkGamePhase.RequestBattleEnd && m_networkService.RemotePlayerTeam == m_team)
		{
			Show();
		}
		else
		{
			Hide();
		}
	}

	private void Show()
	{
		m_codeAnimation.PlayIn();
	}

	private void Hide()
	{
		if (m_codeAnimation.currentState != CodeAnimationInstance.AnimationUse.Out)
		{
			m_codeAnimation.PlayOut();
		}
	}
}
