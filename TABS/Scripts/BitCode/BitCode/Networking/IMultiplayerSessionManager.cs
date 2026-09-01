using System;
using System.Threading.Tasks;
using BitCode.Users;

namespace BitCode.Networking
{
	public interface IMultiplayerSessionManager : IPlatformService
	{
		IMultiplayerSession ActiveSession { get; }

		void CreateSessionAsync(IMultiplayerSessionCreateParameters createParams, Action<IMultiplayerSession, Exception> callback);

		Task<IMultiplayerSession> CreateSessionAsync(IMultiplayerSessionCreateParameters createParams);

		void JoinSessionAsync(IMultiplayerSessionJoinParameters joinParams, Action<IMultiplayerSession, Exception> callback);

		Task<IMultiplayerSession> JoinSessionAsync(IMultiplayerSessionJoinParameters joinParams);

		void LeaveActiveSessionAsync(IMultiplayerSessionLeaveParameters leaveParams, Action<Exception> callback);

		Task LeaveActiveSessionAsync(IMultiplayerSessionLeaveParameters leaveParams);

		IMultiplayerSessionCreateParameters GetSessionCreateParameters(ILocalAccount user);

		IMultiplayerSessionJoinParameters GetSessionJoinParameters(ILocalAccount user, IMultiplayerSessionInfo sessionInfo);

		IMultiplayerSessionLeaveParameters GetActiveSessionLeaveParameters();
	}
}
