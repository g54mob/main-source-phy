using System.Collections.Generic;
using UnityEngine;

public class ThemeStubs : MonoBehaviour
{
	public static ThemeStubs m_Instance;

	public ThemePreloadStub[] m_ThemePreloadStubs;

	private void Awake()
	{
		m_Instance = this;
	}

	public string GetAddressableNameForId(string id)
	{
		ThemePreloadStub[] themePreloadStubs = m_ThemePreloadStubs;
		foreach (ThemePreloadStub themePreloadStub in themePreloadStubs)
		{
			if (themePreloadStub.m_ID == id)
			{
				return themePreloadStub.m_StubPrefabAddress;
			}
		}
		return string.Empty;
	}

	public ThemePreloadStub GetPreloadStubFromId(string id)
	{
		ThemePreloadStub[] themePreloadStubs = m_ThemePreloadStubs;
		foreach (ThemePreloadStub themePreloadStub in themePreloadStubs)
		{
			if (themePreloadStub.m_ID == id)
			{
				return themePreloadStub;
			}
		}
		return null;
	}

	public ThemePreloadStub GetPreloadStubFromName(string name)
	{
		ThemePreloadStub[] themePreloadStubs = m_ThemePreloadStubs;
		foreach (ThemePreloadStub themePreloadStub in themePreloadStubs)
		{
			if (themePreloadStub.m_DisplayNameLocID == name)
			{
				return themePreloadStub;
			}
		}
		return null;
	}

	public string GetLocalizedDisplayName(string id)
	{
		ThemePreloadStub preloadStubFromId = GetPreloadStubFromId(id);
		if (preloadStubFromId == null)
		{
			return string.Empty;
		}
		return Localize.Get(preloadStubFromId.m_DisplayNameLocID);
	}

	public ThemeStub GetStubFromId(string id)
	{
		string addressableNameForId = m_Instance.GetAddressableNameForId(id);
		if (!string.IsNullOrEmpty(addressableNameForId) && Prefabs.AsyncPrefabExists(addressableNameForId))
		{
			return Prefabs.GetAsyncTheme(addressableNameForId);
		}
		return null;
	}

	public string GetIdFromName(string name)
	{
		string text = name.ToLower();
		ThemePreloadStub[] themePreloadStubs = m_ThemePreloadStubs;
		foreach (ThemePreloadStub themePreloadStub in themePreloadStubs)
		{
			if (themePreloadStub.m_StubPrefabAddress.ToLower() == text)
			{
				return themePreloadStub.m_ID;
			}
		}
		return string.Empty;
	}

	public string GetRandomNonLegacyAddressableName()
	{
		List<string> list = new List<string>();
		ThemePreloadStub[] themePreloadStubs = m_Instance.m_ThemePreloadStubs;
		foreach (ThemePreloadStub themePreloadStub in themePreloadStubs)
		{
			if (!themePreloadStub.m_ExcludeInRelease)
			{
				list.Add(themePreloadStub.m_StubPrefabAddress);
			}
		}
		int index = Random.Range(0, list.Count);
		return list[index];
	}

	public void ClearSkyOverrides()
	{
		ThemePreloadStub[] themePreloadStubs = m_Instance.m_ThemePreloadStubs;
		for (int i = 0; i < themePreloadStubs.Length; i++)
		{
			themePreloadStubs[i].m_ThemeSkyOverride = null;
		}
	}

	public ThemePreloadStub GetDefaultPreloadStub()
	{
		return m_ThemePreloadStubs[0];
	}
}
