using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	public class UgcQuery : IDisposable
	{
		public UGCQueryHandle_t Handle;

		public uint MatchedRecordCount;

		public uint PageCount;

		private bool _isAllQuery;

		private bool _isUserQuery;

		private EUserUGCList _listType;

		private EUGCQuery _queryType;

		private EUGCMatchingUGCType _matchingType;

		private EUserUGCListSortOrder _sortOrder;

		private AppId_t _creatorApp;

		private AppId_t _consumerApp;

		private AccountID_t _account;

		private uint _page;

		private UnityAction<UgcQuery> _callback;

		public CallResult<SteamUGCQueryCompleted_t> MSteamUgcQueryCompleted;

		public List<WorkshopItemDetails> ResultsList;

		public uint Page
		{
			get
			{
				return 0u;
			}
			set
			{
			}
		}

		private UgcQuery()
		{
		}

		public static UgcQuery Get(EUGCQuery queryType, EUGCMatchingUGCType matchingType, AppId_t creatorApp, AppId_t consumerApp)
		{
			return null;
		}

		public static UgcQuery Get(params PublishedFileId_t[] fileIds)
		{
			return null;
		}

		public static UgcQuery Get(IEnumerable<PublishedFileId_t> fileIds)
		{
			return null;
		}

		public static UgcQuery Get(AccountID_t account, EUserUGCList listType, EUGCMatchingUGCType matchingType, EUserUGCListSortOrder sortOrder, AppId_t creatorApp, AppId_t consumerApp)
		{
			return null;
		}

		public static UgcQuery Get(UserData user, EUserUGCList listType, EUGCMatchingUGCType matchingType, EUserUGCListSortOrder sortOrder, AppId_t creatorApp, AppId_t consumerApp)
		{
			return null;
		}

		public static UgcQuery GetMyPublished()
		{
			return null;
		}

		public static UgcQuery GetMyPublished(AppData creatorApp, AppData consumerApp)
		{
			return null;
		}

		public static UgcQuery GetSubscribed(bool withLongDescription, bool withMetadata, bool withKeyValueTags, bool withAdditionalPreviews, uint withPlayTimeStatsInDays)
		{
			return null;
		}

		public static UgcQuery GetPlayed()
		{
			return null;
		}

		public static UgcQuery GetPlayed(AppData creatorApp, AppData consumerApp)
		{
			return null;
		}

		public bool AddExcludedTag(string tagName)
		{
			return false;
		}

		public bool AddRequiredKeyValueTag(string key, string value)
		{
			return false;
		}

		public bool AddRequiredTag(string tagName)
		{
			return false;
		}

		public bool SetAllowCachedResponse(uint maxAgeSeconds)
		{
			return false;
		}

		public bool SetCloudFileNameFilter(string fileName)
		{
			return false;
		}

		public bool SetLanguage(string language)
		{
			return false;
		}

		public bool SetMatchAnyTag(bool anyTag)
		{
			return false;
		}

		public bool SetRankedByTrendDays(uint days)
		{
			return false;
		}

		public bool SetReturnAdditionalPreviews(bool additionalPreviews)
		{
			return false;
		}

		public bool SetReturnChildren(bool returnChildren)
		{
			return false;
		}

		public bool SetReturnKeyValueTags(bool tags)
		{
			return false;
		}

		public bool SetReturnLongDescription(bool longDescription)
		{
			return false;
		}

		public bool SetReturnMetadata(bool metadata)
		{
			return false;
		}

		public bool SetReturnOnlyIDs(bool onlyIds)
		{
			return false;
		}

		public bool SetReturnPlaytimeStats(uint days)
		{
			return false;
		}

		public bool SetReturnTotalOnly(bool totalOnly)
		{
			return false;
		}

		public bool SetSearchText(string text)
		{
			return false;
		}

		public bool SetNextPage()
		{
			return false;
		}

		public bool SetPreviousPage()
		{
			return false;
		}

		public bool SetPage(uint page)
		{
			return false;
		}

		public bool Execute(UnityAction<UgcQuery> callback)
		{
			return false;
		}

		private void HandleQueryCompleted(SteamUGCQueryCompleted_t param, bool bIOFailure)
		{
		}

		public void ReleaseHandle()
		{
		}

		public void Dispose()
		{
		}

		public static UgcQuery GetSubscribed(bool IncludeLocallyDisabled = false)
		{
			return null;
		}
	}
}
