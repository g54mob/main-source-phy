using System;
using System.Collections.Generic;
using UnityEngine;

public class CalmZone : MonoBehaviour
{
	public float baseValue;

	public float gradientSize = 2f;

	[Range(0f, 10f)]
	public float exponentialIncrease = 1f;

	protected HashSet<uint> cellsAffected = new HashSet<uint>();

	protected uint myIndex = 99999u;

	protected virtual void RemoveFromGrid()
	{
		if (myIndex == 99999)
		{
			return;
		}
		uint num = (uint)Math.Pow(2.0, myIndex % 24);
		int num2 = Mathf.FloorToInt(myIndex / 24);
		foreach (uint item in cellsAffected)
		{
			Vector4[] cellsContains = CalmZoneController.lastInstance.CellsContains;
			uint num3 = item;
			Vector4[] obj = cellsContains;
			uint num4 = num3;
			int index2;
			int index = (index2 = num2);
			float num5 = obj[num4][index2];
			cellsContains[num3][index] = num5 - (float)num;
		}
		cellsAffected.Clear();
	}

	public virtual void UpdateGrid(uint index, CalmZoneController controller)
	{
		RemoveFromGrid();
		PopulateGrid(index, controller);
	}

	public virtual void PopulateGrid(uint index, CalmZoneController controller)
	{
		myIndex = index;
		int num = Mathf.FloorToInt(index / 24);
		uint num2 = (uint)Math.Pow(2.0, index % 24);
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 40f;
		float num8 = gradientSize * 2f + num7;
		if (gradientSize / 40f > 32f)
		{
			for (uint num9 = 0u; num9 < 64; num9++)
			{
				if (!cellsAffected.Contains(num9))
				{
					Vector4[] cellsContains = controller.CellsContains;
					uint num10 = num9;
					Vector4[] obj = cellsContains;
					uint num11 = num10;
					int index3;
					int index2 = (index3 = num);
					float num12 = obj[num11][index3];
					cellsContains[num10][index2] = num12 + (float)num2;
					cellsAffected.Add(num9);
				}
			}
			return;
		}
		for (float num13 = 0f; num13 <= num8; num13 += num7)
		{
			num3 = gradientSize - num13;
			num4 = num3 * num3;
			for (float num14 = 0f; num14 <= num8; num14 += num7)
			{
				num5 = gradientSize - num14;
				num6 = Mathf.Sqrt(num5 * num5 + num4);
				if (!(num6 - gradientSize > num7) && !((gradientSize + num7) / num6 < 0.9f))
				{
					float x = num14 + base.transform.position.x - gradientSize;
					float y = num13 + base.transform.position.z - gradientSize;
					uint cellKey = controller.GetCellKey(new Vector2(x, y));
					if (!cellsAffected.Contains(cellKey))
					{
						Vector4[] cellsContains2 = controller.CellsContains;
						uint num15 = cellKey;
						Vector4[] obj2 = cellsContains2;
						uint num16 = num15;
						int index3;
						int index4 = (index3 = num);
						float num12 = obj2[num16][index3];
						cellsContains2[num15][index4] = num12 + (float)num2;
						cellsAffected.Add(cellKey);
					}
				}
			}
		}
	}
}
