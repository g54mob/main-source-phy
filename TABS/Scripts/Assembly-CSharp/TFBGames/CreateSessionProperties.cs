using Landfall.TABS;

namespace TFBGames
{
	public struct CreateSessionProperties
	{
		public readonly MapAsset.MapType MapType;

		public readonly int MapIndex;

		public readonly bool CanPlayCrossNetwork;

		public readonly bool IsPublicSession;

		public CreateSessionProperties(MapAsset.MapType mapType, int mapIndex, bool canPlayCrossNetwork, bool isPublicSession)
		{
			MapType = mapType;
			MapIndex = mapIndex;
			CanPlayCrossNetwork = canPlayCrossNetwork;
			IsPublicSession = isPublicSession;
		}
	}
}
