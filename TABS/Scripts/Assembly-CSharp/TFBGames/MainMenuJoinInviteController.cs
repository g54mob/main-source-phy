using BitCode.Networking;
using UnityEngine;

namespace TFBGames
{
	public class MainMenuJoinInviteController : MonoBehaviour
	{
		private enum State
		{
			Idle = 0,
			WaitingForAnimation = 1,
			WaitingForMainMenuToBeReady = 2,
			ReadyToJoinSession = 3
		}

		public delegate void ReceivedInvitationEventHandler(MainMenuJoinInviteController controller, IGameInvitation invite);

		private const int MaxReadyToJoinSessionDelay = 2;

		private SocialProfileService m_socialService;

		private State m_state;

		private IGameInvitation invite;

		private int m_readyToJoinSessionDelay;

		private static bool hasSetReadyForInvite;

		public event ReceivedInvitationEventHandler ReceivedInvitation;

		private void Awake()
		{
			m_socialService = ServiceLocator.GetService<SocialProfileService>();
			if (m_socialService != null)
			{
				m_socialService.ReceivedInvitation += OnReceivedInvitation;
			}
		}

		private void OnDestroy()
		{
			if (m_socialService != null)
			{
				m_socialService.ReceivedInvitation -= OnReceivedInvitation;
			}
		}

		private void Update()
		{
			UpdateJoinInvite();
			if (!hasSetReadyForInvite && IsMainMenuReady() && !IsIntroAnimationBusy())
			{
				hasSetReadyForInvite = true;
				ServiceLocator.GetService<IGameInvitationService>()?.SetAppReadyToReceiveInvites();
			}
		}

		private void SetState(State newState)
		{
			m_state = newState;
			if (newState == State.ReadyToJoinSession)
			{
				m_readyToJoinSessionDelay = 2;
			}
		}

		private bool IsIntroAnimationBusy()
		{
			if (MainMenuButtons.Instance != null)
			{
				return !MainMenuButtons.Instance.AllowAnimation;
			}
			return false;
		}

		private bool IsMainMenuReady()
		{
			if (!(MainMenuButtons.Instance != null) || !MainMenuButtons.Instance.IsActive)
			{
				if (ProjectMarsHandler.Instance != null)
				{
					return ProjectMarsHandler.Instance.IsActive;
				}
				return false;
			}
			return true;
		}

		private void OnReceivedInvitation(IGameInvitation invite)
		{
			if (m_state == State.Idle)
			{
				this.invite = invite;
				if (IsIntroAnimationBusy())
				{
					SetState(State.WaitingForAnimation);
				}
				else if (!IsMainMenuReady())
				{
					SetState(State.WaitingForMainMenuToBeReady);
				}
				else
				{
					SetState(State.ReadyToJoinSession);
				}
			}
		}

		private void UpdateJoinInvite()
		{
			switch (m_state)
			{
			case State.WaitingForAnimation:
				if (!IsIntroAnimationBusy())
				{
					if (!IsMainMenuReady())
					{
						SetState(State.WaitingForMainMenuToBeReady);
					}
					else
					{
						SetState(State.ReadyToJoinSession);
					}
				}
				break;
			case State.WaitingForMainMenuToBeReady:
				if (IsMainMenuReady())
				{
					SetState(State.ReadyToJoinSession);
				}
				break;
			case State.ReadyToJoinSession:
				m_readyToJoinSessionDelay--;
				if (m_readyToJoinSessionDelay <= 0)
				{
					HandleReadyToJoinSession();
				}
				break;
			}
		}

		private void HandleReadyToJoinSession()
		{
			SetState(State.Idle);
			this.ReceivedInvitation?.Invoke(this, invite);
		}
	}
}
