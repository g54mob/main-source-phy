using System.Collections.Generic;
using Photon.Bolt.Internal;
using UdpKit;

namespace Photon.Bolt.Matchmaking
{
	public static class BoltMatchmaking
	{
		public static UdpSession CurrentSession => BoltCore.CurrentSession;

		public static Dictionary<string, object> CurrentMetadata => BoltCore.CurrentMetadata;

		public static void CreateSession(string sessionID, IProtocolToken token = null, string sceneToLoad = null, IProtocolToken sceneToken = null)
		{
			ConfigureServer(sessionID, token, sceneToLoad, sceneToken);
		}

		public static void UpdateSession(IProtocolToken token)
		{
			UdpSession currentSession = CurrentSession;
			if (currentSession != null)
			{
				ConfigureServer(currentSession.HostName, token, null, null);
			}
		}

		public static void JoinSession(string sessionID, IProtocolToken token = null)
		{
			BoltCore.Connect(sessionID, token);
		}

		public static void JoinSession(UdpSession session, IProtocolToken token = null)
		{
			BoltCore.Connect(session, token);
		}

		public static void JoinRandomSession(IProtocolToken token = null)
		{
			JoinRandomSession(null, token);
		}

		public static void JoinRandomSession(UdpSessionFilter sessionFilter, IProtocolToken token = null)
		{
			BoltCore.ConnectRandom(sessionFilter, token);
		}

		internal static void ConfigureServer(string sessionID, IProtocolToken sessionToken, string sceneToLoad, IProtocolToken sceneToken)
		{
			BoltCore.SetServerInfo(sessionID, dedicated: false, sessionToken);
			if (!string.IsNullOrEmpty(sceneToLoad))
			{
				BoltCore._autoLoadSceneInfo = new AutoLoadSceneInfo
				{
					Origin = AutoLoadSceneOrigin.SESSION_CREATION,
					Scene = sceneToLoad,
					Token = sceneToken
				};
			}
		}
	}
}
