using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblLeaderboardQuery
	{
		public ulong XboxUserId { get; private set; }

		public string ServiceConfigurationId { get; private set; }

		public string LeaderboardName { get; private set; }

		public string StatName { get; private set; }

		public XblSocialGroupType SocialGroup { get; private set; }

		public string[] AdditionalColumnleaderboardNames { get; private set; }

		public XblLeaderboardSortOrder Order { get; private set; }

		public uint MaxItems { get; private set; }

		public ulong SkipToXboxUserId { get; private set; }

		public uint SkipResultToRank { get; private set; }

		public string ContinuationToken { get; private set; }

		public XblLeaderboardQueryType QueryType { get; private set; }

		private XblLeaderboardQuery(ulong xboxUserId, string serviceConfigurationId, string leaderboardName, string statName, XblSocialGroupType socialGroup, string[] additionalColumnleaderboardNames, XblLeaderboardSortOrder order, uint maxItems, ulong skipToXboxUserId, uint skipResultToRank, string continuationToken, XblLeaderboardQueryType queryType)
		{
			XboxUserId = xboxUserId;
			ServiceConfigurationId = serviceConfigurationId;
			LeaderboardName = leaderboardName;
			StatName = statName;
			SocialGroup = socialGroup;
			AdditionalColumnleaderboardNames = additionalColumnleaderboardNames;
			Order = order;
			MaxItems = maxItems;
			SkipToXboxUserId = skipToXboxUserId;
			SkipResultToRank = skipResultToRank;
			ContinuationToken = continuationToken;
			QueryType = queryType;
		}

		internal XblLeaderboardQuery(XGamingRuntime.Interop.XblLeaderboardQuery interopLeaderboardQuery)
		{
			XboxUserId = interopLeaderboardQuery.xboxUserId;
			ServiceConfigurationId = interopLeaderboardQuery.GetScid();
			LeaderboardName = interopLeaderboardQuery.leaderboardName.GetString();
			StatName = interopLeaderboardQuery.statName.GetString();
			SocialGroup = interopLeaderboardQuery.socialGroup;
			AdditionalColumnleaderboardNames = interopLeaderboardQuery.GetAdditionalColumnleaderboardNames();
			Order = interopLeaderboardQuery.order;
			MaxItems = interopLeaderboardQuery.maxItems;
			SkipToXboxUserId = interopLeaderboardQuery.skipToXboxUserId;
			SkipResultToRank = interopLeaderboardQuery.skipResultToRank;
			ContinuationToken = interopLeaderboardQuery.continuationToken.GetString();
			QueryType = interopLeaderboardQuery.queryType;
		}

		public static int Create(ulong xboxUserId, string serviceConfigurationId, string leaderboardName, string statName, XblSocialGroupType socialGroup, string[] additionalColumnleaderboardNames, XblLeaderboardSortOrder order, uint maxItems, ulong skipToXboxUserId, uint skipResultToRank, string continuationToken, XblLeaderboardQueryType queryType, out XblLeaderboardQuery leaderboardQuery)
		{
			if (!XGamingRuntime.Interop.XblLeaderboardQuery.ValidateFields(serviceConfigurationId))
			{
				leaderboardQuery = null;
				return -2147024809;
			}
			leaderboardQuery = new XblLeaderboardQuery(xboxUserId, serviceConfigurationId, leaderboardName, statName, socialGroup, additionalColumnleaderboardNames, order, maxItems, skipToXboxUserId, skipResultToRank, continuationToken, queryType);
			return 0;
		}
	}
}
