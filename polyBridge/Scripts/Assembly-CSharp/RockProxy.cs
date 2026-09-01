using System.Collections.Generic;
using UnityEngine;

public class RockProxy
{
	public Vector3 m_Pos;

	public Vector3 m_Scale;

	public string m_PrefabName;

	public bool m_Flipped;

	public bool m_LockToBottom;

	public bool m_UniformScale;

	public string m_UndoGuid;

	public RockProxy(Rock rock)
	{
		m_Pos = rock.transform.position;
		m_Scale = rock.transform.localScale;
		m_Flipped = rock.m_MeshRenderer.transform.localScale.x < 0f;
		m_LockToBottom = rock.m_LockToBottom;
		m_UniformScale = rock.m_UniformScale;
		m_PrefabName = rock.name;
		m_UndoGuid = rock.m_SandboxItem.m_UndoGuid;
	}

	public RockProxy(int version, byte[] bytes, ref int offset)
	{
		DeserializeBinary(version, bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeVector3(m_Pos));
		list.AddRange(ByteSerializer.SerializeVector3(m_Scale));
		list.AddRange(ByteSerializer.SerializeString(m_PrefabName));
		list.AddRange(ByteSerializer.SerializeBool(m_Flipped));
		list.AddRange(ByteSerializer.SerializeBool(m_LockToBottom));
		list.AddRange(ByteSerializer.SerializeBool(m_UniformScale));
		return list.ToArray();
	}

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		m_Pos = ByteSerializer.DeserializeVector3(bytes, ref offset);
		m_Scale = ByteSerializer.DeserializeVector3(bytes, ref offset);
		m_PrefabName = ByteSerializer.DeserializeString(bytes, ref offset);
		m_Flipped = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_LockToBottom = version >= 60 && ByteSerializer.DeserializeBool(bytes, ref offset);
		m_UniformScale = version >= 66 && ByteSerializer.DeserializeBool(bytes, ref offset);
	}
}
