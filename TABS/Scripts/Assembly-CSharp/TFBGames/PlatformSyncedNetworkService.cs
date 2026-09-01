using System;
using System.Text;
using BitCode.Extensions;
using BitCode.Networking;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using Photon.Bolt.Utils;
using UdpKit.Platform.Photon;
using UnityEngine;
using UnityEngine.Networking;

namespace TFBGames
{
	public class PlatformSyncedNetworkService : NetworkService
	{
		private IPlatformNetworkManagerService platformNetworkManagerService;

		private const string BoltSessionDataKey = "BoltSessionData";

		private const string RegionCodeKey = "RegionCode";

		private const string BoltSessionIdKey = "BoltId";

		public bool IsSessionActive => platformNetworkManagerService.IsSessionActive;

		protected virtual void Awake()
		{
			platformNetworkManagerService = ServiceLocator.GetService<IPlatformNetworkManagerService>();
		}

		public override void CreateSessionAsync(CreateSessionProperties properties, CreateSessionCallback callback)
		{
			platformNetworkManagerService = ServiceLocator.GetService<IPlatformNetworkManagerService>();
			if (platformNetworkManagerService == null)
			{
				NetworkException exception = new NetworkException(NetworkErrorCode.FailedToCreatePlatformSession, "No IPlatformNetworkManagerService registered with the ServiceLocator.");
				callback(null, exception);
				return;
			}
			base.CreateSessionAsync(properties, delegate(NetworkSession session, NetworkException ex)
			{
				if (ex == null)
				{
					platformNetworkManagerService.CreateSession(properties.IsPublicSession, delegate(IMultiplayerSession platformSession, Exception platformException)
					{
						if (platformException == null)
						{
							SetPlatformSessionJoinInfo(session, platformSession);
						}
						else
						{
							string message = "Failed to create platform session.";
							Debug.LogError(message);
							ex = new NetworkException(NetworkErrorCode.FailedToCreatePlatformSession, message, platformException);
						}
						callback?.Invoke(session, ex);
					});
				}
				else
				{
					callback?.Invoke(session, ex);
				}
			});
		}

		public override void JoinSessionAsync(bool isQuickGame, JoinSessionProperties properties, JoinSessionCallback callback)
		{
			base.JoinSessionAsync(isQuickGame, properties, delegate(NetworkSession session, NetworkException exception)
			{
				OnJoinSessionAsync(session, exception, callback);
			});
		}

		public virtual void JoinSessionFromInviteAsync(IGameInvitation invite, JoinSessionCallback callback)
		{
			if (platformNetworkManagerService.IsSessionActive)
			{
				platformNetworkManagerService.LeaveActiveSession(OnSessionLeft);
			}
			else
			{
				JoinPlatformSession();
			}
			void JoinPlatformSession()
			{
				platformNetworkManagerService.JoinSessionFromInvite(invite, delegate(IMultiplayerSession platformSession, Exception platformException)
				{
					OnPlatformSessionJoined(invite, callback, platformSession, platformException);
				});
			}
			void OnSessionLeft(Exception exception)
			{
				if (exception == null)
				{
					JoinPlatformSession();
				}
				else
				{
					string message = "Could not leave the current session before joining";
					Debug.LogError(message);
					callback(null, new NetworkException(NetworkErrorCode.FailedToLeaveToPlatformSession, message, exception));
				}
			}
		}

		private void OnPlatformSessionJoined(IGameInvitation invite, JoinSessionCallback callback, IMultiplayerSession platformSession, Exception platformException)
		{
			if (platformException != null)
			{
				string message = "Could not join the platform session.";
				NetworkException exception = new NetworkException(NetworkErrorCode.FailedToConnectToPlatformSession, message, platformException);
				callback(null, exception);
				return;
			}
			if (invite.HasApplicationData())
			{
				JoinSessionHelper(invite, callback);
				return;
			}
			throw new NotImplementedException("Retrieving invite information is not implemented for this platform");
		}

		private void JoinSessionHelper(IGameInvitation invite, JoinSessionCallback callback)
		{
			if (!GetJoinDataFromInvite(invite, callback, out var sessionId, out var regionCode))
			{
				NetworkException exception = new NetworkException(NetworkErrorCode.FailedToRetrieveJoinData, "Could not get join data from invite.");
				callback(null, exception);
			}
			else
			{
				JoinBoltSession(sessionId, regionCode, callback);
			}
		}

		private void JoinBoltSession(string sessionId, string regionCode, JoinSessionCallback callback)
		{
			JoinSessionProperties properties = new JoinSessionProperties(sessionId, regionCode);
			base.JoinSessionAsync(isQuickGame: false, properties, delegate(NetworkSession boltSession, NetworkException boltException)
			{
				platformNetworkManagerService.SendPlatformPlayerInfo();
				callback?.Invoke(boltSession, boltException);
			});
		}

		private bool GetJoinDataFromInvite(IGameInvitation invite, JoinSessionCallback callback, out string sessionId, out string regionCode)
		{
			sessionId = null;
			regionCode = null;
			if (invite.ApplicationData == null)
			{
				Debug.LogError("Failed to get bolt session join info from invite, application data is null.");
				callback?.Invoke(null, new NetworkException(NetworkErrorCode.FailedToConnectToPlatformSession));
				return false;
			}
			string stringFromTwiceUrlEncodedBytes = GetStringFromTwiceUrlEncodedBytes(invite.ApplicationData);
			string[] array = stringFromTwiceUrlEncodedBytes.Split(SessionInfoArgumentSeparator);
			if (array.Length != SessionInfoArgumentsCount)
			{
				Debug.LogError("Invite doesn't contain the correct number of Bolt join session properties! Original string: " + stringFromTwiceUrlEncodedBytes);
				Debug.Log("Check that the encoding of invite.ApplicationData is correct for this platform.");
				callback?.Invoke(null, new NetworkException(NetworkErrorCode.FailedToConnectToPlatformSession));
				return false;
			}
			sessionId = array[0];
			regionCode = array[1];
			return true;
		}

		private string GetStringFromTwiceUrlEncodedBytes(byte[] bytes)
		{
			return UnityWebRequest.UnEscapeURL(UnityWebRequest.UnEscapeURL(Encoding.UTF8.GetString(bytes)));
		}

		public override void JoinRandomSessionAsync(NetworkSessionFilter filter, JoinSessionCallback callback)
		{
			base.JoinRandomSessionAsync(filter, delegate(NetworkSession session, NetworkException exception)
			{
				OnJoinSessionAsync(session, exception, callback);
			});
		}

		public override void ShutdownAsync(ShutDownCallback callback)
		{
			base.ShutdownAsync(OnShutdown);
			void OnLeavePlatformSession(Exception platformException)
			{
				NetworkException exception = null;
				if (platformException != null)
				{
					string message = "Failed to leave the active platform session.";
					Debug.LogError(message);
					exception = new NetworkException(NetworkErrorCode.FailedToLeaveToPlatformSession, message, platformException);
				}
				callback?.Invoke(exception);
			}
			void OnShutdown(NetworkException exception)
			{
				if (exception != null)
				{
					callback?.Invoke(exception);
				}
				else if (platformNetworkManagerService == null)
				{
					exception = new NetworkException(NetworkErrorCode.FailedToCreatePlatformSession, "No platformNetworkManagerService is null (Probably no IPlatformNetworkManagerService is registered with the ServiceLocator.");
					callback?.Invoke(exception);
				}
				else
				{
					platformNetworkManagerService.LeaveActiveSession(OnLeavePlatformSession);
				}
			}
		}

		public override void OnEvent(PlayerPlatformInfoEvent playerInfoEvent)
		{
			if (!playerInfoEvent.FromSelf)
			{
				SetPlayerPlatformInfo(playerInfoEvent.PlatformInfo);
				if (BoltNetwork.IsServer)
				{
					SendPlatformPlayerInfo();
				}
			}
		}

		private void SetPlayerPlatformInfo(string playerPlatformInfo)
		{
		}

		private void SendPlatformPlayerInfo()
		{
		}

		private string GetPlayerId()
		{
			return string.Empty;
		}

		private void OnJoinSessionAsync(NetworkSession session, NetworkException exception, JoinSessionCallback callback)
		{
			if (exception != null)
			{
				Debug.LogException(exception);
			}
			if (exception == null)
			{
				if (!session.Metadata.PlatformSessionJoinInfo.TryParseFromBase64(out var decoded))
				{
					Debug.LogError("Failed to parse platform info string from bolt session.");
					exception = new NetworkException(NetworkErrorCode.FailedToConnectToPlatformSession);
					callback?.Invoke(session, exception);
					return;
				}
				SendPlatformPlayerInfo();
				platformNetworkManagerService.JoinSession(decoded, delegate(IMultiplayerSession platformSession, Exception platformException)
				{
					if (platformException != null)
					{
						string message = "Failed to join platform session.";
						Debug.LogError(message);
						exception = new NetworkException(NetworkErrorCode.FailedToConnectToPlatformSession, message, platformException);
					}
					callback?.Invoke(session, exception);
				});
			}
			else
			{
				callback?.Invoke(session, exception);
			}
		}

		private void SetPlatformSessionJoinInfo(NetworkSession session, IMultiplayerSession platformSession)
		{
			if (BoltNetwork.IsServer && session != null && BoltMatchmaking.CurrentSession is PhotonSession photonSession && photonSession.HostName == session.Id && photonSession.GetProtocolToken() is PhotonRoomProperties photonRoomProperties)
			{
				string platformSessionInfo = platformNetworkManagerService.GetSessionJoinString(platformSession).ToBase64();
				photonRoomProperties.UpdatePlatformSessionJoinInfo(platformSessionInfo);
				BoltMatchmaking.UpdateSession(photonRoomProperties);
			}
		}
	}
}
