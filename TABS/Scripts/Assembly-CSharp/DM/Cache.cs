using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DM
{
	public class Cache
	{
		internal struct WeakAssetObject
		{
			public WeakReference<object> obj;

			public AssetInfo assetInfo;
		}

		internal struct CacheSlot
		{
			public string uri;

			public uint timestamp;

			public AssetObject assetObject;
		}

		private const int m_cacheSlotCount = 200;

		private const int m_cacheBucketCount = 10;

		private Dictionary<string, WeakAssetObject> m_weakAssetObjects = new Dictionary<string, WeakAssetObject>();

		private CacheSlot[] m_cacheSlots = new CacheSlot[200];

		private uint m_cacheTimeStamp;

		private static Cache m_instance;

		public DateTime m_createdDateTime = DateTime.Now;

		public static Cache Instance()
		{
			if (m_instance == null)
			{
				m_instance = new Cache();
			}
			return m_instance;
		}

		private static object TryLock(WeakReference<object> weakReference)
		{
			if (weakReference.TryGetTarget(out var target) && (!(target is UnityEngine.Object) || target as UnityEngine.Object != null))
			{
				return target;
			}
			return null;
		}

		private void TEMP_CleanUp()
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, WeakAssetObject> weakAssetObject in m_weakAssetObjects)
			{
				if (TryLock(weakAssetObject.Value.obj) == null)
				{
					list.Add(weakAssetObject.Key);
				}
			}
			foreach (string item in list)
			{
				Debug.LogFormat("Clean up {0}", item);
				m_weakAssetObjects.Remove(item);
			}
		}

		private void SetCacheEntry(string uri, AssetObject assetObject)
		{
			m_cacheTimeStamp++;
			int hashCode = uri.GetHashCode();
			uint? num = null;
			for (int i = 0; i < 10; i++)
			{
				uint num2 = (uint)(hashCode + i) % (uint)m_cacheSlots.Length;
				if (m_cacheSlots[num2].uri == uri || m_cacheSlots[num2].assetObject.obj == null)
				{
					num = num2;
					break;
				}
				if (!num.HasValue)
				{
					num = num2;
					continue;
				}
				uint num3 = m_cacheTimeStamp - m_cacheSlots[num.Value].timestamp;
				if (m_cacheTimeStamp - m_cacheSlots[num2].timestamp > num3)
				{
					num = num2;
				}
			}
			if (num.HasValue)
			{
				m_cacheSlots[num.Value] = new CacheSlot
				{
					uri = uri,
					timestamp = m_cacheTimeStamp,
					assetObject = assetObject
				};
			}
		}

		public object GetObject(string uri, Func<AssetObject> loadAssetObject)
		{
			if (string.IsNullOrEmpty(uri))
			{
				return null;
			}
			if (m_weakAssetObjects.TryGetValue(uri, out var value))
			{
				object obj = TryLock(value.obj);
				if (obj != null)
				{
					SetCacheEntry(uri, new AssetObject
					{
						obj = obj,
						assetInfo = value.assetInfo
					});
					return obj;
				}
				m_weakAssetObjects.Remove(uri);
			}
			AssetObject assetObject = loadAssetObject();
			if (assetObject.obj != null)
			{
				m_weakAssetObjects.Add(uri, new WeakAssetObject
				{
					obj = new WeakReference<object>(assetObject.obj),
					assetInfo = assetObject.assetInfo
				});
				SetCacheEntry(uri, assetObject);
			}
			return assetObject.obj;
		}

		public void InvalidateEntry(string path)
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, WeakAssetObject> weakAssetObject in m_weakAssetObjects)
			{
				if (weakAssetObject.Key.StartsWith(path))
				{
					list.Add(path);
				}
			}
			foreach (string item in list)
			{
				m_weakAssetObjects.Remove(item);
			}
		}

		public IEnumerable<AssetInfo> GetCachedAssetInfos()
		{
			return m_cacheSlots.Select((CacheSlot cacheSlot) => cacheSlot.assetObject.assetInfo);
		}

		public IEnumerable<AssetInfo> GetWeakAssetInfos()
		{
			return from weakAssetObject in m_weakAssetObjects
				where TryLock(weakAssetObject.Value.obj) != null
				select weakAssetObject.Value.assetInfo;
		}

		public IEnumerable<AssetInfo> GetExpiredWeakAssetInfos()
		{
			return from weakAssetObject in m_weakAssetObjects
				where TryLock(weakAssetObject.Value.obj) == null
				select weakAssetObject.Value.assetInfo;
		}
	}
}
