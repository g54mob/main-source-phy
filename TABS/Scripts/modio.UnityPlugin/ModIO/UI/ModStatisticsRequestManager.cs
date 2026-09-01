using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModIO.UI
{
	public class ModStatisticsRequestManager : MonoBehaviour
	{
		private static ModStatisticsRequestManager _instance;

		public bool clearCacheOnDisable = true;

		public Dictionary<int, ModStatistics> cache = new Dictionary<int, ModStatistics>();

		public bool refetchIfExpired = true;

		public static ModStatisticsRequestManager instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = UIUtilities.FindComponentInAllScenes<ModStatisticsRequestManager>(includeInactive: true);
					if (_instance == null)
					{
						_instance = new GameObject("Mod Statistics Request Manager").AddComponent<ModStatisticsRequestManager>();
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

		protected virtual void OnDisable()
		{
			if (clearCacheOnDisable)
			{
				cache.Clear();
			}
		}

		public virtual void RequestModStatistics(int modId, Action<ModStatistics> onSuccess, Action<WebRequestError> onError)
		{
			ModStatistics value = null;
			if (cache.TryGetValue(modId, out value) && IsValid(value))
			{
				onSuccess(value);
				return;
			}
			CacheClient.LoadModStatistics(modId, delegate(ModStatistics stats)
			{
				if (!(this == null))
				{
					if (IsValid(stats))
					{
						cache.Add(modId, stats);
						if (onSuccess != null)
						{
							onSuccess(stats);
						}
					}
					else
					{
						APIClient.GetModStats(modId, delegate(ModStatistics s)
						{
							if (this != null)
							{
								cache[modId] = s;
							}
							if (onSuccess != null)
							{
								onSuccess(s);
							}
						}, onError);
					}
				}
			});
		}

		public virtual void RequestModStatistics(IList<int> orderedIdList, Action<ModStatistics[]> onSuccess, Action<WebRequestError> onError)
		{
			ModStatistics[] results = new ModStatistics[orderedIdList.Count];
			List<int> missingIds = new List<int>(orderedIdList.Count);
			for (int i = 0; i < orderedIdList.Count; i++)
			{
				int num = orderedIdList[i];
				ModStatistics value = null;
				cache.TryGetValue(num, out value);
				results[i] = value;
				if (!IsValid(value))
				{
					missingIds.Add(num);
				}
			}
			CacheClient.RequestFilteredModStatistics(missingIds, delegate(IList<ModStatistics> cachedStatistics)
			{
				foreach (ModStatistics cachedStatistic in cachedStatistics)
				{
					if (IsValid(cachedStatistic))
					{
						int num2 = orderedIdList.IndexOf(cachedStatistic.modId);
						if (num2 >= 0)
						{
							results[num2] = cachedStatistic;
						}
						missingIds.Remove(cachedStatistic.modId);
					}
				}
				if (missingIds.Count == 0)
				{
					onSuccess(results);
				}
				else
				{
					Action<List<ModStatistics>> onSuccess2 = delegate(List<ModStatistics> modStatistics)
					{
						if (this != null)
						{
							foreach (ModStatistics modStatistic in modStatistics)
							{
								cache[modStatistic.modId] = modStatistic;
							}
						}
						if (onSuccess != null)
						{
							foreach (ModStatistics modStatistic2 in modStatistics)
							{
								int num3 = orderedIdList.IndexOf(modStatistic2.modId);
								if (num3 >= 0)
								{
									results[num3] = modStatistic2;
								}
							}
							onSuccess(results);
						}
					};
					StartCoroutine(FetchAllModStatistics(missingIds.ToArray(), onSuccess2, onError));
				}
			});
		}

		public virtual ModStatistics TryGetValid(int modId)
		{
			ModStatistics value = null;
			cache.TryGetValue(modId, out value);
			if (IsValid(value))
			{
				return value;
			}
			return null;
		}

		protected virtual bool IsValid(ModStatistics statistics)
		{
			if (statistics != null)
			{
				if (refetchIfExpired)
				{
					return ServerTimeStamp.Now < statistics.dateExpires;
				}
				return true;
			}
			return false;
		}

		protected IEnumerator FetchAllModStatistics(int[] modIds, Action<List<ModStatistics>> onSuccess, Action<WebRequestError> onError)
		{
			List<ModStatistics> modProfiles = new List<ModStatistics>();
			APIPaginationParameters pagination = new APIPaginationParameters
			{
				limit = 100,
				offset = 0
			};
			RequestFilter filter = new RequestFilter();
			filter.AddFieldFilter("mod_id", new InArrayFilter<int>
			{
				filterArray = modIds
			});
			bool isDone = false;
			while (!isDone)
			{
				RequestPage<ModStatistics> page = null;
				WebRequestError error = null;
				APIClient.GetAllModStats(filter, pagination, delegate(RequestPage<ModStatistics> r)
				{
					page = r;
				}, delegate(WebRequestError e)
				{
					error = e;
				});
				while (page == null && error == null)
				{
					yield return null;
				}
				if (error != null)
				{
					onError?.Invoke(error);
					modProfiles = null;
					isDone = true;
					continue;
				}
				modProfiles.AddRange(page.items);
				if (page.resultTotal <= page.resultOffset + page.size)
				{
					isDone = true;
				}
				else
				{
					pagination.offset = page.resultOffset + page.size;
				}
			}
			if (isDone && modProfiles != null)
			{
				onSuccess(modProfiles);
			}
		}
	}
}
