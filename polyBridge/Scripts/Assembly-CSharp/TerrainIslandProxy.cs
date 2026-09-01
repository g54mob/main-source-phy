using System.Collections.Generic;
using UnityEngine;

public class TerrainIslandProxy
{
	public Vector3 m_Pos;

	public string m_PrefabName;

	public float m_HeightAdded;

	public float m_Height;

	public float m_RightEdgeWaterHeight;

	public TerrainIslandType m_TerrainIslandType;

	public int m_VariantIndex;

	public bool m_Flipped;

	public bool m_LockPosition;

	public bool m_Hidden;

	public string m_UndoGuid;

	public TerrainIslandProxy(TerrainIsland terrain)
	{
		m_Pos = terrain.transform.position;
		m_PrefabName = terrain.name;
		m_HeightAdded = terrain.m_HeightAdded;
		m_Height = terrain.GetHeight();
		m_RightEdgeWaterHeight = terrain.m_RightEdgeWaterHeight;
		m_TerrainIslandType = terrain.m_TerrainIslandType;
		m_VariantIndex = Theme.m_Instance.GetTerrainPrefabIndex(terrain.m_TerrainIslandType, terrain.name);
		m_Flipped = terrain.m_Flipped;
		m_LockPosition = terrain.m_LockPosition;
		m_Hidden = terrain.m_Hidden;
		m_UndoGuid = terrain.m_SandboxItem.m_UndoGuid;
	}

	public TerrainIslandProxy(int version, byte[] bytes, ref int offset)
	{
		DeserializeBinary(version, bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeVector3(m_Pos));
		list.AddRange(ByteSerializer.SerializeString(m_PrefabName));
		list.AddRange(ByteSerializer.SerializeFloat(m_HeightAdded));
		list.AddRange(ByteSerializer.SerializeFloat(m_RightEdgeWaterHeight));
		list.AddRange(ByteSerializer.SerializeInt((int)m_TerrainIslandType));
		list.AddRange(ByteSerializer.SerializeInt(m_VariantIndex));
		list.AddRange(ByteSerializer.SerializeBool(m_Flipped));
		list.AddRange(ByteSerializer.SerializeBool(m_LockPosition));
		list.AddRange(ByteSerializer.SerializeBool(m_Hidden));
		list.AddRange(ByteSerializer.SerializeFloat(m_Height));
		return list.ToArray();
	}

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		m_Pos = ByteSerializer.DeserializeVector3(bytes, ref offset);
		m_PrefabName = ByteSerializer.DeserializeString(bytes, ref offset);
		m_HeightAdded = ByteSerializer.DeserializeFloat(bytes, ref offset);
		m_RightEdgeWaterHeight = ByteSerializer.DeserializeFloat(bytes, ref offset);
		m_TerrainIslandType = (TerrainIslandType)ByteSerializer.DeserializeInt(bytes, ref offset);
		m_VariantIndex = ByteSerializer.DeserializeInt(bytes, ref offset);
		m_Flipped = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_LockPosition = version >= 6 && ByteSerializer.DeserializeBool(bytes, ref offset);
		m_Hidden = version >= 52 && ByteSerializer.DeserializeBool(bytes, ref offset);
		m_Height = ((version >= 34) ? ByteSerializer.DeserializeFloat(bytes, ref offset) : (TerrainIslands.DEFAULT_HEIGHT + m_HeightAdded));
	}
}
