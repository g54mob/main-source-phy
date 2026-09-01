using Landfall.TABS;
using Photon.Bolt;
using UdpKit.Platform.Photon;

namespace TFBGames
{
	public class MultiplayerSessionMetadata
	{
		public static string HostPlayerDisplayNameKey;

		public static string HostPlatformKey;

		public const string PlatformSessionJoinInfoKey = "psj";

		public static string HostCanPlayCrossNetworkKey;

		public static string GameVersionNumberKey;

		public static string RoomPropertyMapTypeKey;

		public static string RoomPropertyMapIndexKey;

		public static string HostRoomIsPublicKey;

		public string GameVersionNumber { get; private set; }

		public string HostPlayerDisplayName { get; private set; }

		public MultiplayerPlatform HostPlatform { get; private set; }

		public string PlatformSessionJoinInfo { get; private set; }

		public bool HostCanPlayCrossNetwork { get; private set; }

		public MapAsset.MapType RoomMapType { get; private set; }

		public int RoomMapIndex { get; private set; }

		public bool HostRoomIsPublic { get; private set; }

		public MultiplayerSessionMetadata(string gameVersionNumber, string hostPlayerDisplayName, MultiplayerPlatform hostPlatform, string platformSessionJoinInfo, MapAsset.MapType roomMapType, int roomMapIndex, bool hostCanPlayCrossNetwork, bool isPublicSession)
		{
			GameVersionNumber = gameVersionNumber;
			HostPlayerDisplayName = hostPlayerDisplayName;
			HostPlatform = hostPlatform;
			PlatformSessionJoinInfo = platformSessionJoinInfo;
			RoomMapType = roomMapType;
			RoomMapIndex = roomMapIndex;
			HostCanPlayCrossNetwork = hostCanPlayCrossNetwork;
			HostRoomIsPublic = isPublicSession;
		}

		public MultiplayerSessionMetadata(PhotonSession photonSession)
		{
			GameVersionNumber = photonSession.Properties.GetValue<string>(GameVersionNumberKey);
			HostPlayerDisplayName = photonSession.Properties.GetValue<string>(HostPlayerDisplayNameKey);
			HostPlatform = (MultiplayerPlatform)photonSession.Properties.GetValue<int>(HostPlatformKey);
			PlatformSessionJoinInfo = photonSession.Properties.GetValue<string>("psj");
			RoomMapType = (MapAsset.MapType)photonSession.Properties.GetValue<int>(RoomPropertyMapTypeKey);
			RoomMapIndex = photonSession.Properties.GetValue<int>(RoomPropertyMapIndexKey);
			HostCanPlayCrossNetwork = photonSession.Properties.GetValue<bool>(HostCanPlayCrossNetworkKey);
			HostRoomIsPublic = photonSession.Properties.GetValue<bool>(HostRoomIsPublicKey);
		}

		public MultiplayerSessionMetadata(PhotonRoomProperties roomProperties)
		{
			GameVersionNumber = roomProperties.GetValue<string>(GameVersionNumberKey);
			HostPlayerDisplayName = roomProperties.GetValue<string>(HostPlayerDisplayNameKey);
			HostPlatform = (MultiplayerPlatform)roomProperties.GetValue<int>(HostPlatformKey);
			PlatformSessionJoinInfo = roomProperties.GetValue<string>("psj");
			RoomMapType = (MapAsset.MapType)roomProperties.GetValue<int>(RoomPropertyMapTypeKey);
			RoomMapIndex = roomProperties.GetValue<int>(RoomPropertyMapIndexKey);
			HostCanPlayCrossNetwork = roomProperties.GetValue<bool>(HostCanPlayCrossNetworkKey);
			HostRoomIsPublic = roomProperties.GetValue<bool>(HostRoomIsPublicKey);
		}

		public void UpdateMap(MapAsset.MapType roomMapType, int roomMapIndex)
		{
			RoomMapType = roomMapType;
			RoomMapIndex = roomMapIndex;
		}

		public void UpdatePlatformSessionJoinInfo(string platformSessionInfo)
		{
			PlatformSessionJoinInfo = platformSessionInfo;
		}

		public void WriteMetadataToSession(PhotonSession photonSession)
		{
			photonSession.Properties[GameVersionNumberKey] = GameVersionNumber;
			photonSession.Properties[HostPlayerDisplayNameKey] = HostPlayerDisplayName;
			photonSession.Properties[HostPlatformKey] = (int)HostPlatform;
			photonSession.Properties["psj"] = PlatformSessionJoinInfo;
			photonSession.Properties[RoomPropertyMapTypeKey] = (int)RoomMapType;
			photonSession.Properties[RoomPropertyMapIndexKey] = RoomMapIndex;
			photonSession.Properties[HostCanPlayCrossNetworkKey] = HostCanPlayCrossNetwork;
			photonSession.Properties[HostRoomIsPublicKey] = HostRoomIsPublic;
		}

		public void WriteMetadataToRoomProperties(PhotonRoomProperties roomProperties)
		{
			roomProperties[GameVersionNumberKey] = GameVersionNumber;
			roomProperties[HostPlayerDisplayNameKey] = HostPlayerDisplayName;
			roomProperties[HostPlatformKey] = (int)HostPlatform;
			roomProperties["psj"] = PlatformSessionJoinInfo;
			roomProperties[RoomPropertyMapTypeKey] = (int)RoomMapType;
			roomProperties[RoomPropertyMapIndexKey] = RoomMapIndex;
			roomProperties[HostCanPlayCrossNetworkKey] = HostCanPlayCrossNetwork;
			roomProperties[HostRoomIsPublicKey] = HostRoomIsPublic;
		}

		public static void SetMetaDataKeys(ProjectMarsGameServiceAsset projectMarsSettings)
		{
			HostPlayerDisplayNameKey = projectMarsSettings.HostPlayerDisplayNameKey;
			HostPlatformKey = projectMarsSettings.HostPlatformKey;
			HostCanPlayCrossNetworkKey = projectMarsSettings.HostCanPlayCrossNetworkKey;
			GameVersionNumberKey = projectMarsSettings.GameVersionNumberKey;
			RoomPropertyMapTypeKey = projectMarsSettings.RoomPropertyMapTypeKey;
			RoomPropertyMapIndexKey = projectMarsSettings.RoomPropertyMapIndexKey;
			HostRoomIsPublicKey = projectMarsSettings.HostRoomIsPublicKey;
		}
	}
}
