using System;
using BitCode.Networking;

namespace TFBGames
{
	public class DefaultNetworkManagerService : IPlatformNetworkManagerService, IService
	{
		public IMultiplayerSession ActiveSession
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool IsSessionActive
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool CanSendInvites
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public void CreateSession(bool isQuickGame, Action<IMultiplayerSession, Exception> callback)
		{
			throw new NotImplementedException();
		}

		public void JoinSession(string sessionJoinInfo, Action<IMultiplayerSession, Exception> callback)
		{
			throw new NotImplementedException();
		}

		public void JoinSessionFromInvite(IGameInvitation invite, Action<IMultiplayerSession, Exception> callback)
		{
			throw new NotImplementedException();
		}

		public void LeaveActiveSession(Action<Exception> callback)
		{
		}

		public string GetSessionJoinString(IMultiplayerSession session)
		{
			throw new NotImplementedException();
		}

		public void SendPlatformPlayerInfo()
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
	}
}
