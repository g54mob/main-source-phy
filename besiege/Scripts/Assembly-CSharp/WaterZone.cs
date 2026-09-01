using System;
using System.Collections.Generic;
using UnityEngine;

public class WaterZone : CalmZone
{
	public static List<WaterZone> primaryZones = new List<WaterZone>();

	[Range(0f, 1f)]
	[HideInInspector]
	public float pct;

	[NonSerialized]
	[HideInInspector]
	public float depthRange = 10f;

	[HideInInspector]
	public WaterZone superZone;

	private List<WaterZone> subZones = new List<WaterZone>();

	private bool primary;

	[HideInInspector]
	public bool secondary;

	[HideInInspector]
	public Vector3 lastPos;

	public bool needsUpdate = true;

	public float DepthEffect
	{
		get
		{
			float num = base.transform.position.y - WaterController.waterTransformHeight;
			return Mathf.Max(0f, (num + depthRange) / depthRange);
		}
	}

	public float Pct
	{
		get
		{
			return DepthEffect * pct;
		}
		set
		{
			if (pct != value)
			{
				needsUpdate = true;
				if (secondary)
				{
					superZone.needsUpdate = true;
				}
			}
			pct = value;
		}
	}

	public float Value
	{
		get
		{
			return (baseValue - 1f) * Intensity + 1f;
		}
		set
		{
			if (baseValue != value)
			{
				needsUpdate = true;
				if (secondary)
				{
					superZone.needsUpdate = true;
				}
			}
			baseValue = value;
		}
	}

	public float Range
	{
		get
		{
			float num = 0f;
			float num2 = 0f;
			foreach (WaterZone subZone in subZones)
			{
				float num3 = (Vector3.Distance(lastPos, subZone.transform.position) + subZone.gradientSize) * subZone.Pct;
				if (num3 > num)
				{
					num = num3;
				}
				if (subZone.Pct > num2)
				{
					num2 = subZone.Pct;
				}
			}
			if (num2 == 0f)
			{
				return 0f;
			}
			return num / num2;
		}
		set
		{
			if (gradientSize != value)
			{
				needsUpdate = true;
				if (secondary)
				{
					superZone.needsUpdate = true;
				}
			}
			gradientSize = value;
		}
	}

	public float Intensity
	{
		get
		{
			float num = -1f;
			foreach (WaterZone subZone in subZones)
			{
				float num2 = subZone.Pct;
				if (num2 > num)
				{
					num = num2;
				}
			}
			return num;
		}
	}

	public float Exponent
	{
		get
		{
			float num = 0f;
			float num2 = 0.5f;
			foreach (WaterZone subZone in subZones)
			{
				float num3 = Mathf.Pow(subZone.pct, 0.2f);
				float num4 = (subZone.exponentialIncrease - num2) * num3 + num2;
				if (num4 > num)
				{
					num = num4;
				}
			}
			return num;
		}
		set
		{
			if (exponentialIncrease != value)
			{
				needsUpdate = true;
				if (secondary)
				{
					superZone.needsUpdate = true;
				}
			}
			exponentialIncrease = value;
		}
	}

	public Vector3 Position
	{
		get
		{
			Vector3 zero = Vector3.zero;
			float num = 0f;
			foreach (WaterZone subZone in subZones)
			{
				zero += subZone.transform.position * subZone.Pct;
				num += subZone.Pct;
			}
			if (num == 0f)
			{
				return lastPos;
			}
			zero /= num;
			zero.y = 0f;
			lastPos = zero;
			return zero;
		}
	}

	public void Awake()
	{
		lastPos = base.transform.position;
		subZones = new List<WaterZone> { this };
	}

	public void Start()
	{
		if (DepthEffect <= 0f)
		{
			return;
		}
		float num = gradientSize * gradientSize * 0.5f;
		float num2 = float.MaxValue;
		WaterZone waterZone = null;
		foreach (WaterZone primaryZone in primaryZones)
		{
			float sqrMagnitude = (Position - primaryZone.Position).sqrMagnitude;
			if (primaryZone.Intensity > float.Epsilon && sqrMagnitude < num2)
			{
				num2 = sqrMagnitude;
				waterZone = primaryZone;
			}
		}
		if (num2 < num)
		{
			secondary = true;
			waterZone.Contribute(this);
			return;
		}
		if (primaryZones.Count < 96 - CalmZoneController.calmCount)
		{
			primary = true;
			primaryZones.Add(this);
			return;
		}
		num2 = float.MaxValue;
		foreach (WaterZone primaryZone2 in primaryZones)
		{
			float sqrMagnitude2 = (Position - primaryZone2.Position).sqrMagnitude;
			if (sqrMagnitude2 < primaryZone2.gradientSize * primaryZone2.gradientSize && sqrMagnitude2 < num2)
			{
				num2 = sqrMagnitude2;
				waterZone = primaryZone2;
			}
		}
		if (num2 < num)
		{
			secondary = true;
			waterZone.Contribute(this);
		}
	}

	public void OnDestroy()
	{
		RemoveFromGrid();
		if (primary)
		{
			primaryZones.Remove(this);
			primary = false;
			if (subZones.Count > 1)
			{
				WaterZone waterZone = subZones[1];
				waterZone.primary = true;
				waterZone.secondary = false;
				primaryZones.Add(waterZone);
				waterZone.subZones.Clear();
				for (int i = 1; i < subZones.Count; i++)
				{
					waterZone.Contribute(subZones[i]);
				}
			}
		}
		else if (secondary)
		{
			superZone.Subtract(this);
		}
	}

	public void Contribute(WaterZone zone)
	{
		zone.superZone = this;
		subZones.Add(zone);
	}

	public void Subtract(WaterZone zone)
	{
		subZones.Remove(zone);
	}

	public override void UpdateGrid(uint index, CalmZoneController controller)
	{
		if (needsUpdate && !secondary)
		{
			RemoveFromGrid();
			PopulateGrid(index, controller);
			needsUpdate = false;
		}
	}

	public override void PopulateGrid(uint index, CalmZoneController controller)
	{
		myIndex = index;
		uint num = (uint)Math.Pow(2.0, index % 24);
		int num2 = Mathf.FloorToInt(index / 24);
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 40f;
		float range = Range;
		float num8 = range * 2f + num7;
		if (range / 40f > 32f)
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
					int index2 = (index3 = num2);
					float num12 = obj[num11][index3];
					cellsContains[num10][index2] = num12 + (float)num;
					cellsAffected.Add(num9);
				}
			}
			return;
		}
		for (float num13 = 0f; num13 <= num8; num13 += num7)
		{
			num3 = range - num13;
			num4 = num3 * num3;
			for (float num14 = 0f; num14 <= num8; num14 += num7)
			{
				num5 = range - num14;
				num6 = Mathf.Sqrt(num5 * num5 + num4);
				if (!(num6 - range > num7) && !((range + num7) / num6 < 0.9f))
				{
					float x = num14 + Position.x - range;
					float y = num13 + Position.z - range;
					uint cellKey = controller.GetCellKey(new Vector2(x, y));
					if (!cellsAffected.Contains(cellKey))
					{
						Vector4[] cellsContains2 = controller.CellsContains;
						uint num15 = cellKey;
						Vector4[] obj2 = cellsContains2;
						uint num16 = num15;
						int index3;
						int index4 = (index3 = num2);
						float num12 = obj2[num16][index3];
						cellsContains2[num15][index4] = num12 + (float)num;
						cellsAffected.Add(cellKey);
					}
				}
			}
		}
	}

	protected override void RemoveFromGrid()
	{
		if (!secondary)
		{
			base.RemoveFromGrid();
		}
	}
}
