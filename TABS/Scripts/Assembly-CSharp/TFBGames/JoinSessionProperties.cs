namespace TFBGames
{
	public struct JoinSessionProperties
	{
		public readonly string SessionId;

		public readonly string RegionCode;

		public JoinSessionProperties(string sessionId, string regionCode)
		{
			SessionId = sessionId;
			RegionCode = regionCode;
		}
	}
}
