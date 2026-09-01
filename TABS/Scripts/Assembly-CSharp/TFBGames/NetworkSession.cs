namespace TFBGames
{
	public class NetworkSession
	{
		public readonly string Id;

		public bool IsOpen;

		public bool IsVisible;

		public readonly MultiplayerSessionMetadata Metadata;

		public NetworkSession(string id, bool isOpen, bool isVisible, MultiplayerSessionMetadata metadata)
		{
			Id = id;
			IsOpen = isOpen;
			IsVisible = isVisible;
			Metadata = metadata;
		}
	}
}
