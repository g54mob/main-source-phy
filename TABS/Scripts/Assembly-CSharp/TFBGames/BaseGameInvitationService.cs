using System;
using System.Threading.Tasks;
using BitCode;
using BitCode.Networking;
using BitCode.Users;

namespace TFBGames
{
	public abstract class BaseGameInvitationService : IGameInvitationService, IGameInvitationManager, IPlatformService, IService
	{
		protected IGameInvitationManager gameInvitationManager;

		protected ReceivedGameInvitationBuffer receivedInviteBuffer;

		protected bool bufferInvites;

		public event Action<IGameInvitation, ILocalAccount> InvitationReceived;

		public event Action<IPlatformService, Exception> InternalErrorOccurred;

		protected BaseGameInvitationService(bool bufferInvites = true)
		{
			this.bufferInvites = bufferInvites;
		}

		public virtual void SendGameInviteAsync(ILocalAccount user, int maxInvitees, IGameInvitation invitation, Action<bool, Exception> sentCallback)
		{
			gameInvitationManager.SendGameInviteAsync(user, maxInvitees, invitation, sentCallback);
		}

		public virtual Task<bool> SendGameInviteAsync(ILocalAccount user, int maxInvitees, IGameInvitation invitation)
		{
			return gameInvitationManager.SendGameInviteAsync(user, maxInvitees, invitation);
		}

		public virtual void SendGameInviteAsync(ILocalAccount user, IRemoteAccount[] invitees, IGameInvitation invitation, Action<bool, Exception> sentCallback)
		{
			gameInvitationManager.SendGameInviteAsync(user, invitees, invitation, sentCallback);
		}

		public virtual Task<bool> SendGameInviteAsync(ILocalAccount user, IRemoteAccount[] invitees, IGameInvitation invitation)
		{
			return gameInvitationManager.SendGameInviteAsync(user, invitees, invitation);
		}

		public virtual IGameInvitation CreateInviteToMultiplayerSession(IMultiplayerSession session, byte[] applicationData = null)
		{
			return gameInvitationManager.CreateInviteToMultiplayerSession(session, applicationData);
		}

		public virtual void OnAwake()
		{
			gameInvitationManager = FindGameInvitationManager();
			receivedInviteBuffer = new ReceivedGameInvitationBuffer(gameInvitationManager, bufferInvites);
			receivedInviteBuffer.InvitationReceived += InvokeInvitationReceived;
		}

		public virtual void UnRegister()
		{
			receivedInviteBuffer.InvitationReceived -= InvokeInvitationReceived;
			receivedInviteBuffer.Dispose();
			receivedInviteBuffer = null;
			if (gameInvitationManager is IDisposable disposable)
			{
				disposable.Dispose();
			}
			gameInvitationManager = null;
		}

		public virtual void OnRegister()
		{
		}

		public virtual void OnStart()
		{
		}

		public virtual void OnUpdate()
		{
		}

		public virtual void OnFixedUpdate()
		{
		}

		public virtual void OnLateUpdate()
		{
		}

		public void SetAppReadyToReceiveInvites()
		{
			receivedInviteBuffer.SetBufferInvites(buffer: false);
		}

		protected virtual IGameInvitationManager FindGameInvitationManager()
		{
			return ServiceLocator.GetService<IPlatformManager>().Services.GameInvitationManager;
		}

		protected async void InvokeInvitationReceived(IGameInvitation invitation, ILocalAccount recipient)
		{
			if (await ServiceLocator.GetService<PermissionsHelper>().IsOnline(showPopup: true))
			{
				ServiceLocator.GetService<IAccountPermissions>().CanPlayInAMultiplayerSessionAsync(showPopup: true, "MP_POPUP_NOT_ALLOWED_TO_PLAY_IN_A_MULTIPLAYER_SESSION", delegate(bool permitted)
				{
					OnCanPlayInAMultiplayerSession(permitted, invitation, recipient);
				});
			}
		}

		private void OnCanPlayInAMultiplayerSession(bool permitted, IGameInvitation invitation, ILocalAccount recipient)
		{
			if (permitted)
			{
				this.InvitationReceived?.Invoke(invitation, recipient);
			}
		}
	}
}
