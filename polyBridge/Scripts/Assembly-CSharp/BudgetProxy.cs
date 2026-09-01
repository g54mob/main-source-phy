using System.Collections.Generic;

public class BudgetProxy
{
	public int m_CashBudget;

	public int m_RoadBudget;

	public int m_WoodBudget;

	public int m_SteelBudget;

	public int m_HydraulicBudget;

	public int m_RopeBudget;

	public int m_CableBudget;

	public int m_SpringBudget;

	public int m_PillarBudget;

	public int m_BungieRopeBudget;

	public bool m_AllowWood;

	public bool m_AllowSteel;

	public bool m_AllowHydraulic;

	public bool m_AllowRope;

	public bool m_AllowCable;

	public bool m_AllowSpring;

	public bool m_AllowPillar;

	public BudgetProxy()
	{
		m_CashBudget = Budget.m_CashBudget;
		m_RoadBudget = Budget.m_RoadBudget;
		m_WoodBudget = Budget.m_WoodBudget;
		m_SteelBudget = Budget.m_SteelBudget;
		m_HydraulicBudget = Budget.m_HydraulicBudget;
		m_RopeBudget = Budget.m_RopeBudget;
		m_CableBudget = Budget.m_CableBudget;
		m_BungieRopeBudget = Budget.m_BungieRopeBudget;
		m_SpringBudget = Budget.m_SpringBudget;
		m_PillarBudget = Budget.m_PillarBudget;
		m_AllowWood = Budget.m_AllowWood;
		m_AllowSteel = Budget.m_AllowSteel;
		m_AllowHydraulic = Budget.m_AllowHydraulic;
		m_AllowRope = Budget.m_AllowRope;
		m_AllowCable = Budget.m_AllowCable;
		m_AllowSpring = Budget.m_AllowSpring;
		m_AllowPillar = Budget.m_AllowPillar;
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeInt(m_CashBudget));
		list.AddRange(ByteSerializer.SerializeInt(m_RoadBudget));
		list.AddRange(ByteSerializer.SerializeInt(m_WoodBudget));
		list.AddRange(ByteSerializer.SerializeInt(m_SteelBudget));
		list.AddRange(ByteSerializer.SerializeInt(m_HydraulicBudget));
		list.AddRange(ByteSerializer.SerializeInt(m_RopeBudget));
		list.AddRange(ByteSerializer.SerializeInt(m_CableBudget));
		list.AddRange(ByteSerializer.SerializeInt(m_SpringBudget));
		list.AddRange(ByteSerializer.SerializeInt(m_BungieRopeBudget));
		list.AddRange(ByteSerializer.SerializeInt(m_PillarBudget));
		list.AddRange(ByteSerializer.SerializeBool(m_AllowWood));
		list.AddRange(ByteSerializer.SerializeBool(m_AllowSteel));
		list.AddRange(ByteSerializer.SerializeBool(m_AllowHydraulic));
		list.AddRange(ByteSerializer.SerializeBool(m_AllowRope));
		list.AddRange(ByteSerializer.SerializeBool(m_AllowCable));
		list.AddRange(ByteSerializer.SerializeBool(m_AllowSpring));
		list.AddRange(ByteSerializer.SerializeBool(m_AllowPillar));
		return list.ToArray();
	}

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		m_CashBudget = ByteSerializer.DeserializeInt(bytes, ref offset);
		m_RoadBudget = ByteSerializer.DeserializeInt(bytes, ref offset);
		m_WoodBudget = ByteSerializer.DeserializeInt(bytes, ref offset);
		m_SteelBudget = ByteSerializer.DeserializeInt(bytes, ref offset);
		m_HydraulicBudget = ByteSerializer.DeserializeInt(bytes, ref offset);
		m_RopeBudget = ByteSerializer.DeserializeInt(bytes, ref offset);
		m_CableBudget = ByteSerializer.DeserializeInt(bytes, ref offset);
		m_SpringBudget = ByteSerializer.DeserializeInt(bytes, ref offset);
		m_BungieRopeBudget = ByteSerializer.DeserializeInt(bytes, ref offset);
		m_PillarBudget = ((version >= 30) ? ByteSerializer.DeserializeInt(bytes, ref offset) : 0);
		m_AllowWood = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_AllowSteel = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_AllowHydraulic = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_AllowRope = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_AllowCable = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_AllowSpring = ByteSerializer.DeserializeBool(bytes, ref offset);
		if (version < 27)
		{
			ByteSerializer.DeserializeBool(bytes, ref offset);
		}
		m_AllowPillar = version >= 30 && ByteSerializer.DeserializeBool(bytes, ref offset);
		ForceAllowMaterials();
	}

	private void ForceAllowMaterials()
	{
		if (!m_AllowWood)
		{
			m_AllowWood = true;
			m_WoodBudget = 0;
		}
		if (!m_AllowSteel)
		{
			m_AllowSteel = true;
			m_SteelBudget = 0;
		}
		if (!m_AllowHydraulic)
		{
			m_AllowHydraulic = true;
			m_HydraulicBudget = 0;
		}
		if (!m_AllowRope)
		{
			m_AllowRope = true;
			m_RopeBudget = 0;
		}
		if (!m_AllowCable)
		{
			m_AllowCable = true;
			m_CableBudget = 0;
		}
		if (!m_AllowSpring)
		{
			m_AllowSpring = true;
			m_SpringBudget = 0;
		}
		if (!m_AllowPillar)
		{
			m_AllowPillar = true;
			m_PillarBudget = 0;
		}
	}
}
