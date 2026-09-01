using Landfall.TABS;

namespace TFBGames
{
	public interface INetworkService : IService
	{
		bool IsRunning { get; }

		bool IsServer { get; }

		bool IsClient { get; }

		bool IsConnected { get; }

		Team PlayerTeam { get; }

		Team RemotePlayerTeam { get; }

		string CurrentSessionId { get; }

		string RegionCode { get; }

		void SetUserAuthenticationData(string data);

		NetworkSession GetCurrentSession();

		void CreateSessionAsync(CreateSessionProperties properties, CreateSessionCallback callback);

		void JoinSessionAsync(bool isQuickGame, JoinSessionProperties properties, JoinSessionCallback callback);

		void JoinRandomSessionAsync(NetworkSessionFilter filter, JoinSessionCallback callback);

		void GetSessionsAsync(GetSessionsCallback callback);

		void ShutdownAsync(ShutDownCallback callback);

		JoinSessionProperties GetJoinSessionPropertiesFromDataBuffer(byte[] data);

		byte[] CreateJoinSessionPropertiesAsDataBuffer();

		void AuthenticateUserTokenAsync(string token, string regionCode, AuthenticateUserTokenCallback callback);

		void GetSessionInfoFromDataBuffer(byte[] data, out string sessionId, out string regionCode);

		byte[] GetDataBufferFromSessionInfo(string sessionId, string regionCode);

		bool SendPleaseStayConnectedEvent();

		int GetConnectionsCount();
	}
}
