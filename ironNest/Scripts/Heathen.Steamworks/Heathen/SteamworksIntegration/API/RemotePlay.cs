using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration.API
{
	public static class RemotePlay
	{
		public static class Client
		{
			public static uint GetSessionCount()
			{
				return 0u;
			}

			public static RemotePlaySessionID_t GetSessionID(int index)
			{
				return default(RemotePlaySessionID_t);
			}

			public static RemotePlaySessionID_t[] GetSessions()
			{
				return null;
			}

			public static UserData GetSessionUser(RemotePlaySessionID_t session)
			{
				return default(UserData);
			}

			public static string GetSessionClientName(RemotePlaySessionID_t session)
			{
				return null;
			}

			public static ESteamDeviceFormFactor GetSessionClientFormFactor(RemotePlaySessionID_t session)
			{
				return default(ESteamDeviceFormFactor);
			}

			public static Vector2Int GetSessionClientResolution(RemotePlaySessionID_t session)
			{
				return default(Vector2Int);
			}

			public static bool SendInvite(UserData user)
			{
				return false;
			}

			public static bool EnableRemotePlayTogetherDirectInput()
			{
				return false;
			}

			public static void DisableRemotePlayTogetherDirectInput()
			{
			}

			public static uint GetInput(RemotePlayInput_t[] Input, uint MaxEvents)
			{
				return 0u;
			}

			public static void SetMouseVisibility(RemotePlaySessionID_t unSessionID, bool bVisible)
			{
			}

			public static void SetMousePosition(RemotePlaySessionID_t SessionID, float Normalized_X, float Normalized_Y)
			{
			}

			public static void SetMouseCursor(RemotePlaySessionID_t SessionID, RemotePlayCursorID_t CursorID)
			{
			}
		}
	}
}
