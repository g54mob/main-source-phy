using System.Collections.Generic;
using UnityEngine;

public class ZedAxisVehicleStubs : MonoBehaviour
{
	public ZedAxisVehicleStub[] m_Stubs;

	public static ZedAxisVehicleStubs m_Instance;

	public static Dictionary<string, ZedAxisVehicleStub> m_StubsDict = new Dictionary<string, ZedAxisVehicleStub>();

	private void Awake()
	{
		m_Instance = this;
		BuildBaseDictionary();
	}

	public static ZedAxisVehicleStub GetStubByAddressable(string addressable)
	{
		if (!m_StubsDict.ContainsKey(addressable))
		{
			Debug.LogWarningFormat("ZedAxisVehicleStub id " + addressable + " not found in Stubs dictionary");
			return null;
		}
		return m_StubsDict[addressable];
	}

	public static void Register(ZedAxisVehicleStub stub)
	{
		if (!(stub == null) && !m_StubsDict.ContainsKey(stub.m_PrefabAddress))
		{
			m_StubsDict.Add(stub.m_PrefabAddress, stub);
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
		m_StubsDict.Clear();
		ZedAxisVehicleStub[] stubs = m_Stubs;
		foreach (ZedAxisVehicleStub zedAxisVehicleStub in stubs)
		{
			m_StubsDict.Add(zedAxisVehicleStub.m_PrefabAddress, zedAxisVehicleStub);
		}
	}
}
