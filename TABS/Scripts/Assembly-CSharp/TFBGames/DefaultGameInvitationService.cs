using System;
using System.Threading.Tasks;
using BitCode;
using BitCode.Networking;
using BitCode.Users;

namespace TFBGames
{
	public class DefaultGameInvitationService : IGameInvitationService, IGameInvitationManager, IPlatformService, IService
	{
		public bool CanSendInvites => false;

		public event Action<IGameInvitation, ILocalAccount> InvitationReceived;

		public event Action<IPlatformService, Exception> InternalErrorOccurred;

		public void SendGameInviteAsync(ILocalAccount user, int maxInvitees, IGameInvitation invitation, Action<bool, Exception> sentCallback)
		{
			throw new NotImplementedException();
		}

		public Task<bool> SendGameInviteAsync(ILocalAccount user, int maxInvitees, IGameInvitation invitation)
		{
			throw new NotImplementedException();
		}

		public void SendGameInviteAsync(ILocalAccount user, IRemoteAccount[] invitees, IGameInvitation invitation, Action<bool, Exception> sentCallback)
		{
			throw new NotImplementedException();
		}

		public Task<bool> SendGameInviteAsync(ILocalAccount user, IRemoteAccount[] invitees, IGameInvitation invitation)
		{
			throw new NotImplementedException();
		}

		public IGameInvitation CreateInviteToMultiplayerSession(IMultiplayerSession session, byte[] applicationData = null)
		{
			throw new NotImplementedException();
		}

		public virtual void OnAwake()
		{
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

		public virtual void UnRegister()
		{
		}

		public void SetAppReadyToReceiveInvites()
		{
		}

		public string GetBoltSessionIdFromInvite(IGameInvitation invite)
		{
			throw new NotImplementedException();
		}
	}
}
