using System.Collections.Generic;
using UnityEngine;

public class DecorStubs : MonoBehaviour
{
	public static DecorStubs m_Instance;

	public static Dictionary<string, DecorStub> m_DecorStubsDict = new Dictionary<string, DecorStub>();

	public DecorStub[] m_DecorStubs;

	private void Awake()
	{
		m_Instance = this;
		BuildBaseDictionary();
	}

	public static DecorStub GetStubFromId(string id)
	{
		if (!m_DecorStubsDict.ContainsKey(id))
		{
			Debug.LogWarningFormat("DecorStub id " + id + " not found in Decor Stubs dictionary");
			return null;
		}
		return m_DecorStubsDict[id];
	}

	public static DecorStub GetRandomStub()
	{
		List<DecorStub> list = new List<DecorStub>();
		foreach (KeyValuePair<string, DecorStub> item in m_DecorStubsDict)
		{
			list.Add(item.Value);
		}
		if (list.Count == 0)
		{
			return null;
		}
		return list[Random.Range(0, list.Count)];
	}

	public static void AddUgcDecorStub(DecorStub decorStub)
	{
		if (!(decorStub == null) && !m_DecorStubsDict.ContainsKey(decorStub.m_PrefabAddress))
		{
			m_DecorStubsDict.Add(decorStub.m_PrefabAddress, decorStub);
		}
	}

	public static void RemoveAllUgcStubs()
	{
		if (m_Instance != null)
		{
			m_Instance.BuildBaseDictionary();
		}
	}

	public void BuildBaseDictionary()
	{
		m_DecorStubsDict.Clear();
		DecorStub[] decorStubs = m_DecorStubs;
		foreach (DecorStub decorStub in decorStubs)
		{
			if (m_DecorStubsDict.ContainsKey(decorStub.m_PrefabAddress))
			{
				Debug.LogWarning("Duplicate Decor Stub Prefab Address: " + decorStub.m_PrefabAddress);
			}
			else
			{
				m_DecorStubsDict.Add(decorStub.m_PrefabAddress, decorStub);
			}
		}
	}
}
