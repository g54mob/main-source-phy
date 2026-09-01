using Landfall.TABS.GameMode;
using UnityEngine;

namespace TFBGames
{
	public class ProjectMarsBattleWaitingUI : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Waiting message group to show/hide.")]
		protected GameObject m_waitingGroup;

		private BaseGameMode m_currentGameMode;

		private INetworkService m_networkService;

		private NetworkBattleController m_networkBattle;

		private void Start()
		{
			OnStart();
		}

		private void OnDisable()
		{
			if (m_networkBattle != null)
			{
				m_networkBattle.BothPlayersEnteredBattleScene -= OnBothPlayersEnteredBattleScene;
			}
		}

		private void Update()
		{
			UpdateVisibility();
		}

		private void Show()
		{
			if (m_waitingGroup != null)
			{
				m_waitingGroup.SetActive(value: true);
			}
		}

		private void Hide(bool hidePermanently = true)
		{
			if (m_waitingGroup != null)
			{
				m_waitingGroup.SetActive(value: false);
			}
			if (hidePermanently)
			{
				base.gameObject.SetActive(value: false);
			}
		}

		private void OnStart()
		{
			m_currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
			if (!(m_currentGameMode is OnlineMultiplayerGameMode))
			{
				Hide();
				return;
			}
			m_networkService = ServiceLocator.GetService<INetworkService>();
			m_networkBattle = ServiceLocator.GetService<NetworkBattleController>();
			if (m_networkBattle == null || m_networkService.IsClient || m_networkBattle.AreBothPlayersInBattleScene)
			{
				Hide();
				return;
			}
			Show();
			if (AreAnyMenusOpen())
			{
				Hide(hidePermanently: false);
			}
			m_networkBattle.BothPlayersEnteredBattleScene += OnBothPlayersEnteredBattleScene;
		}

		private void OnBothPlayersEnteredBattleScene()
		{
			Hide();
		}

		private void UpdateVisibility()
		{
			if (!(m_waitingGroup == null))
			{
				if (AreAnyMenusOpen() && m_waitingGroup.activeSelf)
				{
					Hide(hidePermanently: false);
				}
				else if (!AreAnyMenusOpen() && !m_waitingGroup.activeSelf)
				{
					Show();
				}
			}
		}

		private bool AreAnyMenusOpen()
		{
			if (m_currentGameMode != null)
			{
				return m_currentGameMode.IsMenuOpen();
			}
			return false;
		}
	}
}
