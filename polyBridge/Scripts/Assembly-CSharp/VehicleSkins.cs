using System.Collections.Generic;

public class VehicleSkins
{
	public static Dictionary<string, List<VehicleSkin>> m_Skins = new Dictionary<string, List<VehicleSkin>>();

	public static string DEFAULT_SKIN_ID = string.Empty;

	public static string DEFAULT_SKIN_NAME = "Default";

	public static List<VehicleSkin> GetSkinsForVehicle(Vehicle vehicle)
	{
		string prefabAddress = vehicle.m_Stub.m_PrefabAddress;
		if (string.IsNullOrEmpty(prefabAddress))
		{
			return null;
		}
		if (m_Skins.ContainsKey(prefabAddress))
		{
			return m_Skins[prefabAddress];
		}
		return null;
	}

	public static void Add(VehicleSkin skin)
	{
		if (FindByID(skin.m_ID) != null)
		{
			skin.m_RefCount++;
			return;
		}
		if (!m_Skins.ContainsKey(skin.m_VehicleAddressableName))
		{
			m_Skins.Add(skin.m_VehicleAddressableName, new List<VehicleSkin> { skin });
		}
		else
		{
			m_Skins[skin.m_VehicleAddressableName].Add(skin);
		}
		skin.m_RefCount = 1;
	}

	public static void Remove(VehicleSkin skin)
	{
		if (!m_Skins.ContainsKey(skin.m_VehicleAddressableName))
		{
			return;
		}
		if (skin.m_RefCount == 1)
		{
			if (m_Skins[skin.m_VehicleAddressableName].Contains(skin))
			{
				m_Skins[skin.m_VehicleAddressableName].Remove(skin);
				skin.m_RefCount = 0;
			}
		}
		else if (skin.m_RefCount > 1)
		{
			skin.m_RefCount--;
		}
		if (m_Skins[skin.m_VehicleAddressableName].Count == 0)
		{
			m_Skins.Remove(skin.m_VehicleAddressableName);
		}
	}

	public static VehicleSkin FindByID(string id)
	{
		foreach (KeyValuePair<string, List<VehicleSkin>> skin in m_Skins)
		{
			foreach (VehicleSkin item in skin.Value)
			{
				if (item.m_ID == id)
				{
					return item;
				}
			}
		}
		return null;
	}

	public static void ClearUGCSkins()
	{
		List<VehicleSkin> list = new List<VehicleSkin>();
		foreach (KeyValuePair<string, List<VehicleSkin>> skin in m_Skins)
		{
			foreach (VehicleSkin item in skin.Value)
			{
				if (item.m_IsMod)
				{
					list.Add(item);
				}
			}
		}
		foreach (VehicleSkin item2 in list)
		{
			Remove(item2);
		}
	}
}
