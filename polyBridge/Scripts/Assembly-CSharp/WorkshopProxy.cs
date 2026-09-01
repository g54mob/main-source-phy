using System.Collections.Generic;

public class WorkshopProxy
{
	public string m_Id;

	public bool m_ShowPrebuilds;

	public bool m_AutoPlay;

	public bool m_AllowFeatured;

	public WorkshopProxy()
	{
		m_Id = string.Empty;
		m_AutoPlay = false;
		m_AllowFeatured = false;
		m_ShowPrebuilds = true;
	}

	public WorkshopProxy(string id, bool autoPlay, bool allowFeatured, bool showPrebuilds)
	{
		m_Id = id;
		m_AutoPlay = autoPlay;
		m_AllowFeatured = allowFeatured;
		m_ShowPrebuilds = showPrebuilds;
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeString(m_Id));
		list.AddRange(ByteSerializer.SerializeBool(m_AutoPlay));
		list.AddRange(ByteSerializer.SerializeBool(m_AllowFeatured));
		list.AddRange(ByteSerializer.SerializeBool(m_ShowPrebuilds));
		return list.ToArray();
	}

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		m_Id = ByteSerializer.DeserializeString(bytes, ref offset);
		if (version >= 16 && version <= 38)
		{
			ByteSerializer.DeserializeString(bytes, ref offset);
		}
		if (version < 61)
		{
			ByteSerializer.DeserializeString(bytes, ref offset);
			ByteSerializer.DeserializeString(bytes, ref offset);
		}
		m_AutoPlay = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_AllowFeatured = version >= 67 && ByteSerializer.DeserializeBool(bytes, ref offset);
		m_ShowPrebuilds = version < 75 || ByteSerializer.DeserializeBool(bytes, ref offset);
		if (version < 70)
		{
			int num = ByteSerializer.DeserializeInt(bytes, ref offset);
			for (int i = 0; i < num; i++)
			{
				ByteSerializer.DeserializeString(bytes, ref offset);
			}
		}
	}
}
