using System.Collections.Generic;
using UnityEngine;

public class VehicleStubs : MonoBehaviour
{
	public VehicleStub[] m_Stubs;

	public static VehicleStubs m_Instance;

	public static Dictionary<string, VehicleStub> m_StubsDict = new Dictionary<string, VehicleStub>();

	private void Awake()
	{
		m_Instance = this;
		BuildBaseDictionary();
	}

	public static void Register(VehicleStub stub)
	{
		if (!(stub == null) && !m_StubsDict.ContainsKey(stub.m_PrefabAddress))
		{
			m_StubsDict.Add(stub.m_PrefabAddress, stub);
		}
	}

	public static VehicleStub GetStubByAddressable(string addressable)
	{
		if (string.IsNullOrEmpty(addressable))
		{
			return null;
		}
		if (!m_StubsDict.ContainsKey(addressable))
		{
			Debug.LogWarningFormat("VehicleStub id " + addressable + " not found in Stubs dictionary");
			return null;
		}
		return m_StubsDict[addressable];
	}

	public static string GetRandomVehiclePrefabAddress()
	{
		List<VehicleStub> list = new List<VehicleStub>();
		foreach (KeyValuePair<string, VehicleStub> item in m_StubsDict)
		{
			if (!item.Value.m_UGC && !item.Value.m_ExcludeFromRandomSpawning)
			{
				list.Add(item.Value);
			}
		}
		if (list.Count == 0)
		{
			return string.Empty;
		}
		return list[Random.Range(0, list.Count)].m_PrefabAddress;
	}

	public static void RemoveAllUgcStubs()
	{
		if (m_Instance != null)
		{
			m_Instance.BuildBaseDictionary();
		}
	}

	private void BuildBaseDictionary()
	{
		m_StubsDict.Clear();
		VehicleStub[] stubs = m_Stubs;
		foreach (VehicleStub vehicleStub in stubs)
		{
			if (!m_StubsDict.ContainsKey(vehicleStub.m_PrefabAddress))
			{
				m_StubsDict.Add(vehicleStub.m_PrefabAddress, vehicleStub);
			}
		}
	}
}
