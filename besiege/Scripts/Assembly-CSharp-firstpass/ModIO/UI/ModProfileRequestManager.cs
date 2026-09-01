using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModIO.UI
{
	[Obsolete("Functionality now available through ModManager.GetRangeOfModProfiles()")]
	public class ModProfileRequestManager : MonoBehaviour
	{
		private static ModProfileRequestManager _instance;

		public int minimumFetchSize = 100;

		public static ModProfileRequestManager instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = UIUtilities.FindComponentInAllScenes<ModProfileRequestManager>(true);
					if (_instance == null)
					{
						GameObject gameObject = new GameObject("Mod Profile Request Manager");
						_instance = gameObject.AddComponent<ModProfileRequestManager>();
					}
				}
				return _instance;
			}
		}

		protected virtual void Awake()
		{
			if (_instance == null)
			{
				_instance = this;
			}
		}

		public virtual void FetchModProfilePage(RequestFilter filter, int resultOffset, int profileCount, Action<RequestPage<ModProfile>> onSuccess, Action<WebRequestError> onError)
		{
			if (onSuccess == null && onError == null)
			{
				return;
			}
			if (profileCount > 100)
			{
				Debug.LogWarning("[mod.io] FetchModProfilePage has been called with a profileCount larger than the APIPaginationParameters.LIMIT_MAX.\nAs such, results may not be as expected.");
				profileCount = 100;
			}
			if (resultOffset < 0)
			{
				resultOffset = 0;
			}
			if (profileCount < 0)
			{
				profileCount = 0;
			}
			List<ModProfile> results = new List<ModProfile>(profileCount);
			APIPaginationParameters pagination = new APIPaginationParameters();
			int num = resultOffset / minimumFetchSize;
			pagination.offset = num * minimumFetchSize;
			pagination.limit = minimumFetchSize;
			APIClient.GetAllMods(filter, pagination, delegate(RequestPage<ModProfile> r01)
			{
				int pageOffset = resultOffset % minimumFetchSize;
				for (int i = pageOffset; i < r01.items.Length && i < pageOffset + profileCount; i++)
				{
					results.Add(r01.items[i]);
				}
				if (pageOffset + profileCount > r01.size && r01.items.Length == r01.size)
				{
					pagination.offset += pagination.limit;
					APIClient.GetAllMods(filter, pagination, delegate(RequestPage<ModProfile> requestPage)
					{
						for (int j = 0; j < requestPage.items.Length && j < pageOffset + profileCount - requestPage.size; j++)
						{
							results.Add(requestPage.items[j]);
							OnModsReceived(resultOffset, profileCount, requestPage.resultTotal, results, onSuccess);
						}
					}, onError);
				}
				else
				{
					OnModsReceived(resultOffset, profileCount, r01.resultTotal, results, onSuccess);
				}
			}, onError);
		}

		private void OnModsReceived(int resultOffset, int pageSize, int resultTotal, List<ModProfile> results, Action<RequestPage<ModProfile>> onSuccess)
		{
			if (onSuccess != null)
			{
				RequestPage<ModProfile> requestPage = new RequestPage<ModProfile>();
				requestPage.size = pageSize;
				requestPage.resultOffset = resultOffset;
				requestPage.resultTotal = resultTotal;
				requestPage.items = results.ToArray();
				RequestPage<ModProfile> obj = requestPage;
				onSuccess(obj);
			}
		}

		public virtual void RequestModProfile(int id, Action<ModProfile> onSuccess, Action<WebRequestError> onError)
		{
			ModManager.GetModProfile(id, onSuccess, onError);
		}

		public virtual void RequestModProfiles(IList<int> orderedIdList, Action<ModProfile[]> onSuccess, Action<WebRequestError> onError)
		{
			ModManager.GetModProfiles(orderedIdList, onSuccess, onError);
		}
	}
}
