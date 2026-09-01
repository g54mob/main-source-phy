using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PreviewCache
{
	public static Dictionary<string, PreviewCacheItem> m_Cache = new Dictionary<string, PreviewCacheItem>();

	private static readonly int MAX_PREVIEWS_IN_CACHE = 300;

	private static string m_PreviewCacheDirectory;

	public static void Init()
	{
		m_PreviewCacheDirectory = Path.Combine(Application.persistentDataPath, "PreviewCache");
		Utils.DeleteAllFilesInDirectory(m_PreviewCacheDirectory);
	}

	public static Texture2D Get(string id)
	{
		if (m_Cache.ContainsKey(id))
		{
			return m_Cache[id].m_Texture2D;
		}
		return null;
	}

	public static void Cache(string id, Texture2D texture)
	{
		if (!m_Cache.ContainsKey(id) && texture != null)
		{
			AddInternal(id, texture);
		}
	}

	public static void FlushPreviewsOverCacheLimit()
	{
		while (m_Cache.Count - MAX_PREVIEWS_IN_CACHE > 0)
		{
			RemoveOldestItem();
		}
	}

	private static string GetFullPath(string id)
	{
		return Path.Combine(m_PreviewCacheDirectory, id);
	}

	private static void AddInternal(string id, Texture2D texture)
	{
		m_Cache.Add(id, new PreviewCacheItem(texture, Time.realtimeSinceStartup));
	}

	private static void RemoveOldestItem()
	{
		string text = string.Empty;
		float num = float.MaxValue;
		foreach (KeyValuePair<string, PreviewCacheItem> item in m_Cache)
		{
			if (item.Value.m_CreateTime < num)
			{
				text = item.Key;
				num = item.Value.m_CreateTime;
			}
		}
		if (!string.IsNullOrEmpty(text))
		{
			Object.Destroy(m_Cache[text].m_Texture2D);
			m_Cache.Remove(text);
		}
	}
}
