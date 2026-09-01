using System;
using System.Threading.Tasks;
using BitCode.Networking;
using BitCode.Users;
using JetBrains.Annotations;

namespace BitCode.Platform.Steamworks.Networking
{
	public class SteamMultiplayerSessionManager : IPlatformService, IMultiplayerSessionManager
	{
		private readonly SteamService JvEZsMEbjtsmodyZFfToTIvzzHoC;

		private SteamMultiplayerSession XyhFcRbcNwpkhSHSDOSovTPkTdoo;

		public IMultiplayerSession ActiveSession
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		event Action<IPlatformService, Exception> IPlatformService.InternalErrorOccurred
		{
			add
			{
			}
			remove
			{
			}
		}

		internal SteamMultiplayerSessionManager([NotNull] SteamService P_0)
		{
			throw new NotImplementedException();
		}

		public void CreateSessionAsync(IMultiplayerSessionCreateParameters createParams, Action<IMultiplayerSession, Exception> callback)
		{
			throw new NotImplementedException();
		}

		public Task<IMultiplayerSession> CreateSessionAsync(IMultiplayerSessionCreateParameters createParams)
		{
			throw new NotImplementedException();
		}

		public void JoinSessionAsync(IMultiplayerSessionJoinParameters joinParams, Action<IMultiplayerSession, Exception> callback)
		{
			throw new NotImplementedException();
		}

		public Task<IMultiplayerSession> JoinSessionAsync(IMultiplayerSessionJoinParameters joinParams)
		{
			throw new NotImplementedException();
		}

		public void LeaveActiveSessionAsync(IMultiplayerSessionLeaveParameters leaveParams, Action<Exception> callback)
		{
			throw new NotImplementedException();
		}

		public Task LeaveActiveSessionAsync(IMultiplayerSessionLeaveParameters leaveParams = null)
		{
			throw new NotImplementedException();
		}

		public IMultiplayerSessionCreateParameters GetSessionCreateParameters(ILocalAccount user)
		{
			throw new NotImplementedException();
		}

		public IMultiplayerSessionJoinParameters GetSessionJoinParameters(ILocalAccount user, IMultiplayerSessionInfo sessionInfo)
		{
			throw new NotImplementedException();
		}

		public IMultiplayerSessionLeaveParameters GetActiveSessionLeaveParameters()
		{
			throw new NotImplementedException();
		}
	}
}
