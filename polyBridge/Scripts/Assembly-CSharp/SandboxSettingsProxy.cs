using System.Collections.Generic;
using UnityEngine;

public class SandboxSettingsProxy
{
	public string m_Title;

	public string m_Description;

	public bool m_HydraulicControllerEnabled;

	public bool m_Unbreakable;

	public bool m_UnlimitedHeightFoundations;

	public bool m_NoWater;

	public bool m_NoReinforcedRoad;

	public bool m_SpringAdjustmentsAllowed;

	public bool m_HideDecor;

	public float m_FogHeightNormalized;

	public float m_FogHeightMinWorldY;

	public float m_FogHeightMaxWorldY;

	public float m_FogHeightEndRelativeY;

	public float m_MultiSelectMovementIncrement;

	public bool m_ThumbnailCameraSaved;

	public Vector3 m_ThumbnailCameraPos;

	public Quaternion m_ThumbnailCameraRot;

	public float m_ThumbnailCameraOrthographicSize;

	public SandboxSettingsProxy()
	{
		m_Title = SandboxSettings.m_Title;
		m_Description = SandboxSettings.m_Description;
		m_HydraulicControllerEnabled = SandboxSettings.m_HydraulicControllerEnabled;
		m_Unbreakable = SandboxSettings.m_Unbreakable;
		m_UnlimitedHeightFoundations = SandboxSettings.m_UnlimitedHeightFoundations;
		m_NoWater = SandboxSettings.m_NoWater;
		m_NoReinforcedRoad = SandboxSettings.m_NoReinforcedRoad;
		m_SpringAdjustmentsAllowed = SandboxSettings.m_SpringAdjustmentsAllowed;
		m_HideDecor = SandboxSettings.m_HideDecor;
		m_FogHeightNormalized = SandboxSettings.m_FogHeightNormalized;
		m_FogHeightMinWorldY = SandboxSettings.m_FogHeightMinWorldY;
		m_FogHeightMaxWorldY = SandboxSettings.m_FogHeightMaxWorldY;
		m_FogHeightEndRelativeY = SandboxSettings.m_FogHeightEndRelativeY;
		m_MultiSelectMovementIncrement = SandboxSettings.m_MultiSelectMovementIncrement;
		m_ThumbnailCameraSaved = SandboxSettings.m_ThumbnailCameraSaved;
		m_ThumbnailCameraPos = SandboxSettings.m_ThumbnailCameraPos;
		m_ThumbnailCameraRot = SandboxSettings.m_ThumbnailCameraRot;
		m_ThumbnailCameraOrthographicSize = SandboxSettings.m_ThumbnailCameraOrthographicSize;
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeString(m_Title));
		list.AddRange(ByteSerializer.SerializeString(m_Description));
		list.AddRange(ByteSerializer.SerializeBool(m_HydraulicControllerEnabled));
		list.AddRange(ByteSerializer.SerializeBool(m_Unbreakable));
		list.AddRange(ByteSerializer.SerializeBool(m_UnlimitedHeightFoundations));
		list.AddRange(ByteSerializer.SerializeBool(m_NoWater));
		list.AddRange(ByteSerializer.SerializeBool(m_NoReinforcedRoad));
		list.AddRange(ByteSerializer.SerializeBool(m_SpringAdjustmentsAllowed));
		list.AddRange(ByteSerializer.SerializeBool(m_HideDecor));
		list.AddRange(ByteSerializer.SerializeFloat(m_FogHeightNormalized));
		list.AddRange(ByteSerializer.SerializeFloat(m_FogHeightMinWorldY));
		list.AddRange(ByteSerializer.SerializeFloat(m_FogHeightMaxWorldY));
		list.AddRange(ByteSerializer.SerializeFloat(m_FogHeightEndRelativeY));
		list.AddRange(ByteSerializer.SerializeFloat(m_MultiSelectMovementIncrement));
		if (PolyTwitch.m_IsSerializing)
		{
			list.AddRange(ByteSerializer.SerializeBool(value: false));
			list.AddRange(ByteSerializer.SerializeVector3(Vector3.zero));
			list.AddRange(ByteSerializer.SerializeQuaternion(Quaternion.identity));
			list.AddRange(ByteSerializer.SerializeFloat(0f));
		}
		else
		{
			list.AddRange(ByteSerializer.SerializeBool(m_ThumbnailCameraSaved));
			list.AddRange(ByteSerializer.SerializeVector3(m_ThumbnailCameraPos));
			list.AddRange(ByteSerializer.SerializeQuaternion(m_ThumbnailCameraRot));
			list.AddRange(ByteSerializer.SerializeFloat(m_ThumbnailCameraOrthographicSize));
		}
		return list.ToArray();
	}

	public void DeserializeBinary(int version, float waterHeight, byte[] bytes, ref int offset)
	{
		m_Title = ((version >= 61) ? ByteSerializer.DeserializeString(bytes, ref offset) : string.Empty);
		m_Description = ((version >= 61) ? ByteSerializer.DeserializeString(bytes, ref offset) : string.Empty);
		m_HydraulicControllerEnabled = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_Unbreakable = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_UnlimitedHeightFoundations = version >= 55 && ByteSerializer.DeserializeBool(bytes, ref offset);
		m_NoWater = version >= 26 && ByteSerializer.DeserializeBool(bytes, ref offset);
		m_NoReinforcedRoad = version >= 27 && ByteSerializer.DeserializeBool(bytes, ref offset);
		m_NoReinforcedRoad = true;
		m_SpringAdjustmentsAllowed = version >= 27 && ByteSerializer.DeserializeBool(bytes, ref offset);
		m_HideDecor = version >= 36 && ByteSerializer.DeserializeBool(bytes, ref offset);
		m_FogHeightNormalized = ((version >= 46) ? ByteSerializer.DeserializeFloat(bytes, ref offset) : SandboxSettings.DEFAULT_FOG_HEIGHT_NORMALIZED);
		if (version >= 71)
		{
			m_FogHeightMinWorldY = ByteSerializer.DeserializeFloat(bytes, ref offset);
			m_FogHeightMaxWorldY = ByteSerializer.DeserializeFloat(bytes, ref offset);
			m_FogHeightEndRelativeY = ByteSerializer.DeserializeFloat(bytes, ref offset);
		}
		else
		{
			if (version >= 68)
			{
				ByteSerializer.DeserializeFloat(bytes, ref offset);
				ByteSerializer.DeserializeFloat(bytes, ref offset);
			}
			m_FogHeightMinWorldY = HeightFog.DEFAULT_FOG_HEIGHT_START_MIN_WORLD_Y;
			m_FogHeightMaxWorldY = waterHeight + Theme.m_Instance.GetFogHeightStart();
			m_FogHeightEndRelativeY = Theme.m_Instance.GetFogHeightEnd();
		}
		m_MultiSelectMovementIncrement = ((version >= 59) ? ByteSerializer.DeserializeFloat(bytes, ref offset) : GameGrid.m_Spacing);
		m_ThumbnailCameraSaved = version >= 69 && ByteSerializer.DeserializeBool(bytes, ref offset);
		m_ThumbnailCameraPos = ((version >= 69) ? ByteSerializer.DeserializeVector3(bytes, ref offset) : Vector3.zero);
		m_ThumbnailCameraRot = ((version >= 69) ? ByteSerializer.DeserializeQuaternion(bytes, ref offset) : Quaternion.identity);
		m_ThumbnailCameraOrthographicSize = ((version >= 69) ? ByteSerializer.DeserializeFloat(bytes, ref offset) : 10f);
	}
}
