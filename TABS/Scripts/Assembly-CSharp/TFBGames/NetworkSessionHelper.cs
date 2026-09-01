using System;
using BitCode.Users;
using DM;
using ExitGames.Client.Photon;
using Landfall.TABS;
using Photon.Bolt;
using Photon.Bolt.Utils;
using UdpKit;
using UdpKit.Platform.Photon;
using UnityEngine;

namespace TFBGames
{
	public static class NetworkSessionHelper
	{
		public static string GetGameVersion()
		{
			ContentDatabase contentDatabase = ContentDatabase.Instance();
			if (contentDatabase == null)
			{
				return string.Empty;
			}
			return contentDatabase.GetVersion();
		}

		public static PhotonRoomProperties ConvertRoomProperties(CreateSessionProperties source, bool isOpen = true, bool isVisible = true)
		{
			PhotonRoomProperties photonRoomProperties = new PhotonRoomProperties
			{
				IsOpen = isOpen,
				IsVisible = isVisible
			};
			AccountManager service = ServiceLocator.GetService<AccountManager>();
			string hostPlayerDisplayName = ((service != null && service.ActiveAccount != null && service.ActiveAccount.Name.Status == UserAccountPropertyStatus.Loaded) ? service.ActiveAccount.Name.Value : "UNKNOWN PLAYER");
			new MultiplayerSessionMetadata(GetGameVersion(), hostPlayerDisplayName, GetMultiplayerPlatform(), "", source.MapType, source.MapIndex, source.CanPlayCrossNetwork, source.IsPublicSession).WriteMetadataToRoomProperties(photonRoomProperties);
			return photonRoomProperties;
		}

		public static NetworkSession ConvertSession(UdpSession source)
		{
			PhotonSession obj = (PhotonSession)source;
			PhotonRoomProperties photonRoomProperties = (PhotonRoomProperties)obj.GetProtocolToken();
			return new NetworkSession(isOpen: photonRoomProperties?.IsOpen ?? false, isVisible: photonRoomProperties?.IsVisible ?? false, metadata: new MultiplayerSessionMetadata(obj), id: obj.HostName);
		}

		public static bool CanJoinSession(NetworkSession session, NetworkSessionFilter filter, bool canJoinIfHidden)
		{
			if (session == null || !session.IsOpen || (!canJoinIfHidden && !session.IsVisible))
			{
				return false;
			}
			if (filter == null)
			{
				return true;
			}
			MultiplayerSessionMetadata metadata = session.Metadata;
			if (!metadata.HostRoomIsPublic)
			{
				return false;
			}
			if (string.IsNullOrEmpty(filter.GameVersion) || string.IsNullOrEmpty(metadata.GameVersionNumber) || !filter.GameVersion.Equals(metadata.GameVersionNumber, StringComparison.InvariantCulture))
			{
				return false;
			}
			bool flag = filter.AllowedPlatforms != null && filter.AllowedPlatforms.Contains(metadata.HostPlatform);
			bool num = metadata.HostPlatform == GetMultiplayerPlatform();
			bool flag2 = filter.CanPlayCrossNetworkSession && metadata.HostCanPlayCrossNetwork;
			return num || flag || flag2;
		}

		public static void UpdateMap(this PhotonSession photonSession, MultiplayerSessionMetadata metadata)
		{
			photonSession.Properties[MultiplayerSessionMetadata.RoomPropertyMapTypeKey] = (int)metadata.RoomMapType;
			photonSession.Properties[MultiplayerSessionMetadata.RoomPropertyMapIndexKey] = metadata.RoomMapIndex;
		}

		public static void UpdateMap(this PhotonRoomProperties roomProperties, MultiplayerSessionMetadata metadata)
		{
			roomProperties[MultiplayerSessionMetadata.RoomPropertyMapTypeKey] = (int)metadata.RoomMapType;
			roomProperties[MultiplayerSessionMetadata.RoomPropertyMapIndexKey] = metadata.RoomMapIndex;
		}

		public static void UpdateMap(this PhotonRoomProperties roomProperties, MapAsset.MapType mapType, int mapIndex)
		{
			roomProperties[MultiplayerSessionMetadata.RoomPropertyMapTypeKey] = (int)mapType;
			roomProperties[MultiplayerSessionMetadata.RoomPropertyMapIndexKey] = mapIndex;
		}

		public static void UpdatePlatformSessionJoinInfo(this PhotonRoomProperties roomProperties, string platformSessionInfo)
		{
			roomProperties["psj"] = platformSessionInfo;
		}

		public static T GetValue<T>(this Hashtable table, string key)
		{
			if (table.TryGetValue(key, out var value))
			{
				object obj;
				if ((obj = value) is T)
				{
					return (T)obj;
				}
				Debug.LogWarning("The type for key " + key + " has changed. Returning default value. (You are probably trying to connect to an older session.)");
				return default(T);
			}
			return default(T);
		}

		public static T GetValue<T>(this PhotonRoomProperties roomProperties, string key)
		{
			object obj;
			if ((obj = roomProperties[key]) is T)
			{
				return (T)obj;
			}
			Debug.LogWarning("The type for key " + key + " has changed. Returning default value. (You are probably trying to connect to an older session.)");
			return default(T);
		}

		public static MultiplayerPlatform GetMultiplayerPlatform()
		{
			return MultiplayerPlatform.Steam;
		}
	}
}
