using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace ModIO.API
{
	public static class RequestCache
	{
		private struct Entry
		{
			public int timeStamp;

			public string responseBody;

			public uint size;
		}

		private const int ENTRY_LIFETIME = 120;

		private static readonly uint MAX_CACHE_SIZE = PluginSettings.CACHE_SIZE_BYTES;

		private static Dictionary<string, int> urlResponseIndexMap = new Dictionary<string, int>();

		private static List<Entry> responses = new List<Entry>();

		private static string lastOAuthToken = null;

		private static uint currentCacheSize = 0u;

		public static bool TryGetResponse(string url, out string response)
		{
			response = null;
			string endpointURL = null;
			if (!TryTrimAPIURLAndKey(url, out endpointURL))
			{
				return false;
			}
			bool result = false;
			int index;
			Entry entry;
			if (LocalUser.OAuthToken == lastOAuthToken && TryGetEntry(endpointURL, out index, out entry))
			{
				if (ServerTimeStamp.Now - entry.timeStamp >= 120)
				{
					RemoveOldestEntries(index + 1);
				}
				else
				{
					response = entry.responseBody;
					result = true;
				}
			}
			return result;
		}

		public static void StoreResponse(string url, string responseBody)
		{
			if (LocalUser.OAuthToken != lastOAuthToken)
			{
				Clear();
				lastOAuthToken = LocalUser.OAuthToken;
			}
			string endpointURL = null;
			if (!TryTrimAPIURLAndKey(url, out endpointURL))
			{
				Debug.LogWarning("[mod.io] Attempted to cache response for url that does not contain the api URL.\nRequest URL: " + ((url != null) ? url : "NULL"));
				return;
			}
			int index;
			Entry entry;
			if (TryGetEntry(endpointURL, out index, out entry))
			{
				Debug.LogWarning("[mod.io] Stale cached request found. Removing all older entries.");
				RemoveOldestEntries(index + 1);
			}
			uint num = 0u;
			if (responseBody != null)
			{
				num = (uint)(responseBody.Length * 2);
			}
			if (num > MAX_CACHE_SIZE)
			{
				Debug.Log("[mod.io] Could not cache entry as the response body is larger than MAX_CACHE_SIZE.\nMAX_CACHE_SIZE=" + ValueFormatting.ByteCount(MAX_CACHE_SIZE, "0.0") + "\nendpointURL=" + endpointURL + "\nResponseBody Size=" + ValueFormatting.ByteCount(num, "0.0"));
				return;
			}
			if (currentCacheSize + num > MAX_CACHE_SIZE)
			{
				TrimCacheToMaxSize(MAX_CACHE_SIZE - num);
			}
			Entry item = new Entry
			{
				timeStamp = ServerTimeStamp.Now,
				responseBody = responseBody,
				size = num
			};
			urlResponseIndexMap.Add(endpointURL, responses.Count);
			responses.Add(item);
			currentCacheSize += num;
		}

		public static void StoreMods(int gameId, IEnumerable<ModProfile> mods)
		{
			if (mods == null)
			{
				return;
			}
			List<string> list = new List<string>();
			List<Entry> list2 = new List<Entry>();
			int now = ServerTimeStamp.Now;
			uint num = 0u;
			foreach (ModProfile mod in mods)
			{
				if (mod == null)
				{
					continue;
				}
				string text = APIClient.BuildGetModEndpointURL(gameId, mod.id);
				if (!urlResponseIndexMap.ContainsKey(text))
				{
					string text2 = JsonConvert.SerializeObject(mod);
					uint num2 = (uint)(text2.Length * 2);
					if (num + num2 >= MAX_CACHE_SIZE)
					{
						break;
					}
					list.Add(text);
					list2.Add(new Entry
					{
						timeStamp = now,
						size = num2,
						responseBody = text2
					});
					num += num2;
				}
			}
			if (num != 0)
			{
				if (num + currentCacheSize > MAX_CACHE_SIZE)
				{
					TrimCacheToMaxSize(MAX_CACHE_SIZE - num);
				}
				int count = responses.Count;
				for (int i = 0; i < list.Count; i++)
				{
					urlResponseIndexMap.Add(list[i], i + count);
				}
				responses.AddRange(list2);
				currentCacheSize += num;
			}
		}

		public static bool TryGetMod(int gameId, int modId, out ModProfile profile)
		{
			profile = null;
			bool result = false;
			string endpointURL = APIClient.BuildGetModEndpointURL(gameId, modId);
			int index;
			Entry entry;
			if (LocalUser.OAuthToken == lastOAuthToken && TryGetEntry(endpointURL, out index, out entry))
			{
				if (ServerTimeStamp.Now - entry.timeStamp >= 120)
				{
					RemoveOldestEntries(index + 1);
				}
				else
				{
					string responseBody = entry.responseBody;
					profile = JsonConvert.DeserializeObject<ModProfile>(responseBody);
					result = true;
				}
			}
			return result;
		}

		public static void Clear()
		{
			urlResponseIndexMap.Clear();
			responses.Clear();
			currentCacheSize = 0u;
		}

		private static void TrimCacheToMaxSize(uint maxSize)
		{
			uint num = currentCacheSize;
			int i;
			for (i = 0; i < responses.Count; i++)
			{
				if (num <= maxSize)
				{
					break;
				}
				num -= responses[i].size;
			}
			if (num != 0)
			{
				RemoveOldestEntries(i + 1);
			}
			else
			{
				Clear();
			}
		}

		private static void RemoveOldestEntries(int count)
		{
			if (count >= responses.Count)
			{
				Clear();
				return;
			}
			List<string> list = new List<string>(urlResponseIndexMap.Keys);
			foreach (string item in list)
			{
				int num = urlResponseIndexMap[item] - count;
				urlResponseIndexMap[item] = num;
				if (num < 0)
				{
					urlResponseIndexMap.Remove(item);
				}
			}
			uint num2 = 0u;
			for (int i = 0; i < count; i++)
			{
				num2 += responses[i].size;
			}
			currentCacheSize -= num2;
			responses.RemoveRange(0, count);
		}

		private static bool TryGetEntry(string endpointURL, out int index, out Entry entry)
		{
			if (!urlResponseIndexMap.TryGetValue(endpointURL, out index) || index < 0 || index >= responses.Count)
			{
				index = -1;
				entry = default(Entry);
				return false;
			}
			entry = responses[index];
			return true;
		}

		private static bool TryTrimAPIURLAndKey(string requestURL, out string endpointURL)
		{
			if (string.IsNullOrEmpty(requestURL) || !requestURL.StartsWith(PluginSettings.API_URL) || requestURL.Length == PluginSettings.API_URL.Length)
			{
				endpointURL = null;
				return false;
			}
			endpointURL = requestURL.Substring(PluginSettings.API_URL.Length + 1).Replace("&api_key=" + PluginSettings.GAME_API_KEY, string.Empty);
			return true;
		}
	}
}
