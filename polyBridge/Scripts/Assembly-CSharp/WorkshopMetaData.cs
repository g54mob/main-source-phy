using System.Collections.Generic;
using UnityEngine;

public class WorkshopMetaData
{
	public static readonly int NUM_MATERIALS = 8;

	private static readonly string V2_PREFIX = "v2";

	public static bool IsLegacy(string metadata)
	{
		return !metadata.StartsWith(V2_PREFIX);
	}

	public static int GetBudget(string metadata)
	{
		if (string.IsNullOrEmpty(metadata))
		{
			return 0;
		}
		string[] array = metadata.Split(',');
		if (array.Length > 1 && int.TryParse((array[0] == V2_PREFIX) ? array[1] : array[0], out var result))
		{
			return result;
		}
		return 0;
	}

	public static List<int> GetMaterialCounts(string metadata)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < NUM_MATERIALS; i++)
		{
			list.Add(0);
		}
		if (!string.IsNullOrEmpty(metadata))
		{
			string[] array = metadata.Split(',');
			int num = ((!(array[0] == V2_PREFIX)) ? 1 : 2);
			for (int j = 0; j < NUM_MATERIALS && j + num < array.Length; j++)
			{
				if (int.TryParse(array[j + num], out var result))
				{
					list[j] = result;
				}
			}
		}
		return list;
	}

	public static void SetMaterialCountForIcon(GameObject icon, int count)
	{
		MaterialLimit componentInChildren = icon.GetComponentInChildren<MaterialLimit>(includeInactive: true);
		if (!(componentInChildren == null))
		{
			componentInChildren.Set(count);
			componentInChildren.gameObject.SetActive(count != Budget.UNLIMITED_MATERIAL_BUDGET && count != 0);
		}
	}

	public static string Create()
	{
		return string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat($"v2,{Mathf.RoundToInt(Budget.m_CashBudget)}" + $",{Budget.m_RoadBudget}", Budget.m_AllowWood ? $",{Budget.m_WoodBudget}" : ",0"), Budget.m_AllowSteel ? $",{Budget.m_SteelBudget}" : ",0"), Budget.m_AllowHydraulic ? $",{Budget.m_HydraulicBudget}" : ",0"), Budget.m_AllowRope ? $",{Budget.m_RopeBudget}" : ",0"), Budget.m_AllowCable ? $",{Budget.m_CableBudget}" : ",0"), Budget.m_AllowSpring ? $",{Budget.m_SpringBudget}" : ",0"), Budget.m_AllowPillar ? $",{Budget.m_PillarBudget}" : ",0");
	}
}
