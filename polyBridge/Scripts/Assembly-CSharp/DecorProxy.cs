using System.Collections.Generic;
using UnityEngine;

public class DecorProxy
{
	public Vector3 m_Pos;

	public Vector3 m_Scale;

	public float m_HeadingAngle;

	public float m_PitchAngle;

	public float m_RollAngle;

	public string m_ID;

	public bool m_ShowInBuildMode;

	public bool m_UniformScale;

	public string m_UndoGuid;

	public string m_ModId;

	private static Dictionary<string, string> LegacyGuidMap = new Dictionary<string, string>
	{
		{ "8746bbb2-adc1-44f9-a5f0-45ea9c4174b9", "Crane" },
		{ "6b9e82f7-f485-4d1f-a20a-3f92564c28d9", "FlyingAnchors1" },
		{ "50280314-b2cf-45d1-b932-631ed4e8ee5e", "FlyingAnchors2" },
		{ "971f2d40-acec-4800-a99d-260c4b22fd96", "Alpine_Ice1" },
		{ "1dbe68ca-4e17-4237-9be9-8d15d9881181", "Alpine_Ice2" },
		{ "d1f13854-e286-4731-9b83-511b4ee2bc42", "Alpine_Ice3" },
		{ "6c2650a3-cd6d-483d-91c9-97b8a9e3dc9e", "Alpine_Ice4" },
		{ "19af6a0a-a009-4de9-9cac-b464785b2e56", "PlatformLeg" },
		{ "c898183d-9654-43d7-8064-2bf38aba1d90", "Rocks_Rock1" },
		{ "87ccb362-2fbb-415f-9604-43ed91c49f27", "Rocks_Rock2" },
		{ "dece2caf-75fe-4b1e-a7dd-8cdff557c8f0", "Rocks_Rock3" },
		{ "dc5c38fd-8cb5-4f6c-b00c-c9051123215a", "Rocks_Rock4" },
		{ "1c0d23d4-e5dd-4644-a35f-a114a8c65c48", "Rocks_Rock5" },
		{ "afc9f629-e30e-42a3-902e-c1f9c31c5e8d", "Rocks_Rock6" },
		{ "87824a52-ae3c-45b5-8622-05913c981b7b", "TelephonePole" }
	};

	public DecorProxy(Decor decor)
	{
		m_Pos = decor.transform.position;
		m_Scale = decor.transform.localScale;
		m_HeadingAngle = decor.m_HeadingRotationDegrees;
		m_PitchAngle = decor.m_PitchRotationDegrees;
		m_RollAngle = decor.m_RollRotationDegrees;
		m_ID = decor.GetId();
		m_ShowInBuildMode = decor.m_ShowInBuildMode;
		m_UniformScale = decor.m_UniformScale;
		m_UndoGuid = decor.m_SandboxItem.m_UndoGuid;
		m_ModId = decor.m_ModId;
	}

	public DecorProxy(int version, byte[] bytes, ref int offset)
	{
		DeserializeBinary(version, bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeVector3(m_Pos));
		list.AddRange(ByteSerializer.SerializeVector3(m_Scale));
		list.AddRange(ByteSerializer.SerializeFloat(m_HeadingAngle));
		list.AddRange(ByteSerializer.SerializeFloat(m_PitchAngle));
		list.AddRange(ByteSerializer.SerializeFloat(m_RollAngle));
		list.AddRange(ByteSerializer.SerializeString(m_ID));
		list.AddRange(ByteSerializer.SerializeBool(m_ShowInBuildMode));
		list.AddRange(ByteSerializer.SerializeBool(m_UniformScale));
		list.AddRange(ByteSerializer.SerializeString(m_ModId));
		return list.ToArray();
	}

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		m_Pos = ByteSerializer.DeserializeVector3(bytes, ref offset);
		m_Scale = ((version >= 65) ? ByteSerializer.DeserializeVector3(bytes, ref offset) : Vector3.one);
		m_HeadingAngle = ByteSerializer.DeserializeFloat(bytes, ref offset);
		m_PitchAngle = ((version >= 58) ? ByteSerializer.DeserializeFloat(bytes, ref offset) : 0f);
		m_RollAngle = ((version >= 58) ? ByteSerializer.DeserializeFloat(bytes, ref offset) : 0f);
		m_ID = MaybeConvertLegacyId(ByteSerializer.DeserializeString(bytes, ref offset));
		m_ShowInBuildMode = version >= 40 && ByteSerializer.DeserializeBool(bytes, ref offset);
		m_UniformScale = version >= 66 && ByteSerializer.DeserializeBool(bytes, ref offset);
		m_ModId = ((version >= 54) ? ByteSerializer.DeserializeString(bytes, ref offset) : string.Empty);
	}

	private string MaybeConvertLegacyId(string id)
	{
		if (LegacyGuidMap.ContainsKey(id))
		{
			return LegacyGuidMap[id];
		}
		return id;
	}
}
