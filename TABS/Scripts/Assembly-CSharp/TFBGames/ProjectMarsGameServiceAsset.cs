using UnityEngine;

namespace TFBGames
{
	[CreateAssetMenu(menuName = "Services/Project Mars Data Settings")]
	public class ProjectMarsGameServiceAsset : ServiceAsset
	{
		[Header("Session Timeout Settings")]
		[SerializeField]
		private float getSessionsTimeOut;

		[SerializeField]
		private float cancellingTimeOut;

		[SerializeField]
		private float stateTimeOut;

		[Header("Xbox One Bolt Authentication URL")]
		[SerializeField]
		private string xboxOnePhotonBoltAuthenticationURL;

		[Header("Multiplayer Session Metadata Keys")]
		[SerializeField]
		private string hostPlayerDisplayNameKey;

		[SerializeField]
		private string hostPlatformKey;

		[SerializeField]
		private string hostCanPlayCrossNetworkKey;

		[SerializeField]
		private string gameVersionNumberKey;

		[SerializeField]
		private string roomPropertyMapTypeKey;

		[SerializeField]
		private string roomPropertyMapIndexKey;

		[SerializeField]
		private string hostRoomIsPublicKey;

		public float GetSessionsTimeOut => getSessionsTimeOut;

		public float CancellingTimeOut => cancellingTimeOut;

		public float StateTimeOut => stateTimeOut;

		public string XboxOnePhotonBoltAuthenticationURL => xboxOnePhotonBoltAuthenticationURL;

		public string HostPlayerDisplayNameKey => hostPlayerDisplayNameKey;

		public string HostPlatformKey => hostPlatformKey;

		public string HostCanPlayCrossNetworkKey => hostCanPlayCrossNetworkKey;

		public string GameVersionNumberKey => gameVersionNumberKey;

		public string RoomPropertyMapTypeKey => roomPropertyMapTypeKey;

		public string RoomPropertyMapIndexKey => roomPropertyMapIndexKey;

		public string HostRoomIsPublicKey => hostRoomIsPublicKey;
	}
}
