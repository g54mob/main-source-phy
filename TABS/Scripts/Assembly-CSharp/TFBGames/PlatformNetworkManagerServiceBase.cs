using System;
using BitCode.Networking;
using UnityEngine;

namespace TFBGames
{
	public abstract class PlatformNetworkManagerServiceBase : IPlatformNetworkManagerService, IService
	{
		protected INetworkService boltNetworkService;

		protected IMultiplayerSessionManager platformNetworkManager;

		public virtual IMultiplayerSession ActiveSession => platformNetworkManager?.ActiveSession;

		public virtual bool IsSessionActive
		{
			get
			{
				if (platformNetworkManager != null)
				{
					return platformNetworkManager.IsSessionActive();
				}
				return false;
			}
		}

		public virtual bool CanSendInvites
		{
			get
			{
				if (IsSessionActive)
				{
					return platformNetworkManager.ActiveSession.CanSendInvites;
				}
				return false;
			}
		}

		protected abstract string PlatformString { get; }

		public abstract void CreateSession(bool isQuickGame, Action<IMultiplayerSession, Exception> callback);

		public abstract void JoinSession(string sessionJoinInfo, Action<IMultiplayerSession, Exception> callback);

		public abstract void JoinSessionFromInvite(IGameInvitation invite, Action<IMultiplayerSession, Exception> callback);

		public abstract void LeaveActiveSession(Action<Exception> callback);

		public abstract string GetSessionJoinString(IMultiplayerSession session);

		public virtual void SendPlatformPlayerInfo()
		{
		}

		public virtual void OnAwake()
		{
			boltNetworkService = ServiceLocator.GetService<INetworkService>();
			platformNetworkManager = (ServiceLocator.GetService<IPlatformManager>()?.Services)?.MultiplayerSessionManager;
		}

		public virtual void UnRegister()
		{
			boltNetworkService = null;
			platformNetworkManager = null;
		}

		public void OnRegister()
		{
		}

		public void OnStart()
		{
		}

		public virtual void OnUpdate()
		{
		}

		public void OnFixedUpdate()
		{
		}

		public void OnLateUpdate()
		{
		}

		protected void CreateSessionAsync(IMultiplayerSessionCreateParameters createParams, Action<IMultiplayerSession, Exception> callback)
		{
			platformNetworkManager.CreateSessionAsync(createParams, delegate(IMultiplayerSession session, Exception exception)
			{
				if (exception == null)
				{
					Debug.Log(">>>>>>>>>> Created " + PlatformString + " Network Session.");
				}
				else
				{
					Debug.LogError(">>>>>>>>>> FAILED to create " + PlatformString + " Network Session.");
				}
				callback?.Invoke(session, exception);
			});
		}

		protected void JoinSessionAsync(IMultiplayerSessionJoinParameters joinParams, Action<IMultiplayerSession, Exception> callback)
		{
			platformNetworkManager.JoinSessionAsync(joinParams, delegate(IMultiplayerSession session, Exception exception)
			{
				if (exception == null)
				{
					Debug.Log(">>>>>>>>>> Joined " + PlatformString + " Network Session.");
				}
				else
				{
					Debug.LogError(">>>>>>>>>> FAILED to join " + PlatformString + " Network Session.");
				}
				callback?.Invoke(session, exception);
			});
		}

		protected void LeaveSessionAsync(NetworkSession boltSession, Action<Exception> callback)
		{
			if (!platformNetworkManager.IsSessionActive())
			{
				Debug.Log(">>>>>>>>>> No " + PlatformString + " network session to leave.");
				callback?.Invoke(null);
			}
			else
			{
				platformNetworkManager.LeaveActiveSessionAsync(null, OnActiveSessionLeft);
			}
			void OnActiveSessionLeft(Exception exception)
			{
				if (exception == null)
				{
					Debug.Log(">>>>>>>>>> Left " + PlatformString + " Network Session.");
				}
				else
				{
					Debug.LogError(">>>>>>>>>> FAILED to leave " + PlatformString + " Network Session.");
				}
				callback?.Invoke(exception);
			}
		}
	}
}
