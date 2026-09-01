using System.Collections.Generic;
using UnityEngine;

public class CustomShapeProxy
{
	public int m_Version;

	public Vector3 m_Pos;

	public Quaternion m_Rot;

	public Vector3 m_Scale;

	public Vector3 m_MeshScale;

	public bool m_CollidesWithRoad;

	public bool m_CollidesWithNodes;

	public bool m_CollidesWithRamps;

	public bool m_CollidesWithVehicles;

	public bool m_CollidesWithSplitNodes;

	public bool m_Flipped;

	public bool m_LowFriction;

	public float m_RotationDegrees;

	public float m_Mass;

	public float m_Bounciness;

	public float m_PinMotorStrength;

	public float m_PinTargetVelocity;

	public float m_PinTargetAcceleration;

	public float m_Thickness;

	public Color m_Color;

	public List<Vector2> m_PointsLocalSpace = new List<Vector2>();

	public List<Vector3> m_StaticPins = new List<Vector3>();

	public List<Vector3> m_DynamicAnchors = new List<Vector3>();

	public List<string> m_DynamicAnchorGuids = new List<string>();

	public string m_TextureId;

	public float m_TextureTiling;

	public CustomShapeBehavior m_Behavior;

	public string m_MeshId;

	public Vector3 m_MeshLocalPos;

	public string m_UndoGuid;

	private static readonly int VERSION = 1;

	public CustomShapeProxy(CustomShape shape)
	{
		m_Version = VERSION;
		m_Pos = new Vector3(shape.transform.position.x, shape.transform.position.y, shape.transform.position.z);
		m_Rot = shape.transform.rotation;
		m_Scale = shape.transform.localScale;
		m_MeshScale = ((shape.m_CustomMesh != null) ? shape.m_CustomMesh.transform.localScale : Vector3.one);
		m_Flipped = shape.transform.localScale.x < 0f;
		m_LowFriction = shape.m_LowFriction;
		m_CollidesWithRoad = shape.m_CollidesWithRoad;
		m_CollidesWithNodes = shape.m_CollidesWithNodes;
		m_CollidesWithRamps = shape.m_CollidesWithRamps;
		m_CollidesWithVehicles = shape.m_CollidesWithVehicles;
		m_CollidesWithSplitNodes = shape.m_CollidesWithSplitNodes;
		m_RotationDegrees = shape.m_RotationDegrees;
		m_Color = shape.m_Color;
		m_Mass = shape.m_Mass;
		m_Bounciness = shape.m_Bounciness;
		m_PinMotorStrength = shape.m_PinMotorStrength;
		m_PinTargetVelocity = shape.m_PinTargetVelocity;
		m_PinTargetAcceleration = shape.m_PinTargetAccelerationSeconds;
		m_Thickness = shape.m_Thickness;
		m_UndoGuid = shape.m_SandboxItem.m_UndoGuid;
		m_TextureId = ((shape.m_Texture != null) ? shape.m_Texture.m_ID : string.Empty);
		m_MeshId = shape.m_MeshId;
		m_TextureTiling = shape.m_TextureTiling;
		m_Behavior = shape.m_Behavior;
		m_MeshLocalPos = shape.m_MeshRenderer.transform.localPosition;
		for (int i = 0; i < shape.m_PolygonCollider2D.points.Length; i++)
		{
			m_PointsLocalSpace.Add(shape.m_PolygonCollider2D.points[i]);
		}
		foreach (CustomShapePin pin in shape.m_Pins)
		{
			m_StaticPins.Add(shape.transform.InverseTransformPoint(pin.transform.position));
		}
		foreach (CustomShapeAnchor anchor in shape.m_Anchors)
		{
			m_DynamicAnchorGuids.Add(anchor.m_BridgeJointGuid);
			m_DynamicAnchors.Add(shape.transform.InverseTransformPoint(anchor.transform.position));
		}
	}

	public CustomShapeProxy(int version, byte[] bytes, ref int offset)
	{
		DeserializeBinary(version, bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeInt(m_Version));
		list.AddRange(ByteSerializer.SerializeVector3(m_Pos));
		list.AddRange(ByteSerializer.SerializeQuaternion(m_Rot));
		list.AddRange(ByteSerializer.SerializeVector3(m_Scale));
		list.AddRange(ByteSerializer.SerializeVector3(m_MeshScale));
		list.AddRange(ByteSerializer.SerializeBool(m_Flipped));
		list.AddRange(ByteSerializer.SerializeBool(m_LowFriction));
		list.AddRange(ByteSerializer.SerializeBool(m_CollidesWithRoad));
		list.AddRange(ByteSerializer.SerializeBool(m_CollidesWithNodes));
		list.AddRange(ByteSerializer.SerializeBool(m_CollidesWithRamps));
		list.AddRange(ByteSerializer.SerializeBool(m_CollidesWithVehicles));
		list.AddRange(ByteSerializer.SerializeBool(m_CollidesWithSplitNodes));
		list.AddRange(ByteSerializer.SerializeFloat(m_RotationDegrees));
		list.AddRange(ByteSerializer.SerializeColor(m_Color));
		list.AddRange(ByteSerializer.SerializeFloat(m_Mass));
		list.AddRange(ByteSerializer.SerializeFloat(m_Bounciness));
		list.AddRange(ByteSerializer.SerializeFloat(m_PinMotorStrength));
		list.AddRange(ByteSerializer.SerializeFloat(m_PinTargetVelocity));
		list.AddRange(ByteSerializer.SerializeFloat(m_PinTargetAcceleration));
		list.AddRange(ByteSerializer.SerializeFloat(m_Thickness));
		list.AddRange(ByteSerializer.SerializeString(m_TextureId));
		list.AddRange(ByteSerializer.SerializeString(m_MeshId));
		list.AddRange(ByteSerializer.SerializeVector3(m_MeshLocalPos));
		list.AddRange(ByteSerializer.SerializeFloat(m_TextureTiling));
		list.AddRange(ByteSerializer.SerializeInt((int)m_Behavior));
		SerializePointsBinary(list);
		SerializeStaticPinsBinary(list);
		SerializeDynamicAnchorsBinary(list);
		return list.ToArray();
	}

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		m_Version = ((version >= 64) ? ByteSerializer.DeserializeInt(bytes, ref offset) : 0);
		m_Pos = ByteSerializer.DeserializeVector3(bytes, ref offset);
		m_Rot = ByteSerializer.DeserializeQuaternion(bytes, ref offset);
		m_Scale = ByteSerializer.DeserializeVector3(bytes, ref offset);
		m_MeshScale = ((version >= 72) ? ByteSerializer.DeserializeVector3(bytes, ref offset) : Vector3.one);
		m_Flipped = version >= 21 && ByteSerializer.DeserializeBool(bytes, ref offset);
		m_LowFriction = version >= 74 && ByteSerializer.DeserializeBool(bytes, ref offset);
		if (version < 45)
		{
			ByteSerializer.DeserializeBool(bytes, ref offset);
		}
		m_CollidesWithRoad = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_CollidesWithNodes = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_CollidesWithRamps = version >= 53 && ByteSerializer.DeserializeBool(bytes, ref offset);
		m_CollidesWithVehicles = version < 64 || ByteSerializer.DeserializeBool(bytes, ref offset);
		m_CollidesWithSplitNodes = version >= 25 && ByteSerializer.DeserializeBool(bytes, ref offset);
		m_RotationDegrees = ByteSerializer.DeserializeFloat(bytes, ref offset);
		if (version >= 10)
		{
			m_Color = ByteSerializer.DeserializeColor(bytes, ref offset);
		}
		else
		{
			ByteSerializer.DeserializeInt(bytes, ref offset);
		}
		if (version >= 11)
		{
			m_Mass = ByteSerializer.DeserializeFloat(bytes, ref offset);
		}
		else
		{
			ByteSerializer.DeserializeFloat(bytes, ref offset);
			m_Mass = CustomShapes.DEFAULT_MASS;
		}
		m_Bounciness = ((version >= 14) ? ByteSerializer.DeserializeFloat(bytes, ref offset) : CustomShapes.DEFAULT_BOUNCINESS);
		m_PinMotorStrength = ((version >= 24) ? ByteSerializer.DeserializeFloat(bytes, ref offset) : CustomShapes.DEFAULT_PIN_MOTOR_STRENGTH);
		m_PinTargetVelocity = ((version >= 24) ? ByteSerializer.DeserializeFloat(bytes, ref offset) : CustomShapes.DEFAULT_PIN_TARGET_VELOCITY);
		m_PinTargetAcceleration = ((version >= 63) ? ByteSerializer.DeserializeFloat(bytes, ref offset) : CustomShapes.DEFAULT_PIN_TARGET_ACCELERATION_SECONDS);
		m_Thickness = ((version >= 44) ? ByteSerializer.DeserializeFloat(bytes, ref offset) : CustomShapes.DEFAULT_THICKNESS);
		m_TextureId = ((version >= 41) ? ByteSerializer.DeserializeString(bytes, ref offset) : string.Empty);
		m_MeshId = ((version >= 47) ? ByteSerializer.DeserializeString(bytes, ref offset) : CustomShapes.AUTO_GENERATED_MESH_ID);
		m_MeshLocalPos = ((version >= 47) ? ByteSerializer.DeserializeVector3(bytes, ref offset) : Vector3.zero);
		if (version >= 45)
		{
			m_TextureTiling = ByteSerializer.DeserializeFloat(bytes, ref offset);
		}
		else if (version >= 42)
		{
			m_TextureTiling = ByteSerializer.DeserializeVector2(bytes, ref offset).x;
		}
		else
		{
			m_TextureTiling = 1f;
		}
		m_Behavior = ((version >= 45) ? ((CustomShapeBehavior)ByteSerializer.DeserializeInt(bytes, ref offset)) : CustomShapeBehavior.DYNAMIC);
		DeserializePointsBinary(bytes, ref offset);
		DeserializeStaticPinsBinary(bytes, ref offset);
		DeserializeDynamicAnchorsBinary(version, bytes, ref offset);
		if (version < 45)
		{
			if (m_StaticPins.Count > 1)
			{
				m_Behavior = CustomShapeBehavior.STATIC;
			}
			else if (m_StaticPins.Count == 1)
			{
				m_Behavior = CustomShapeBehavior.MOTORIZED;
			}
			else
			{
				m_Behavior = CustomShapeBehavior.STATIC;
			}
		}
		if (version < 48)
		{
			for (int i = 0; i < m_StaticPins.Count; i++)
			{
				m_StaticPins[i] = Quaternion.Inverse(m_Rot) * (m_StaticPins[i] - m_Pos);
			}
		}
		if (m_MeshId == "81c7c979-48e5-459c-a1eb-da631ae880ed")
		{
			m_MeshId = "CustomShapeBarrier";
		}
	}

	private void SerializePointsBinary(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_PointsLocalSpace.Count));
		foreach (Vector2 item in m_PointsLocalSpace)
		{
			bytes.AddRange(ByteSerializer.SerializeVector2(item));
		}
	}

	private void SerializeStaticPinsBinary(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_StaticPins.Count));
		foreach (Vector3 staticPin in m_StaticPins)
		{
			bytes.AddRange(ByteSerializer.SerializeVector3(staticPin));
		}
	}

	private void SerializeDynamicAnchorsBinary(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_DynamicAnchorGuids.Count));
		foreach (string dynamicAnchorGuid in m_DynamicAnchorGuids)
		{
			bytes.AddRange(ByteSerializer.SerializeString(dynamicAnchorGuid));
		}
		foreach (Vector3 dynamicAnchor in m_DynamicAnchors)
		{
			bytes.AddRange(ByteSerializer.SerializeVector3(dynamicAnchor));
		}
	}

	private void DeserializePointsBinary(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_PointsLocalSpace.Add(ByteSerializer.DeserializeVector2(bytes, ref offset));
		}
	}

	private void DeserializeStaticPinsBinary(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_StaticPins.Add(ByteSerializer.DeserializeVector3(bytes, ref offset));
		}
	}

	private void DeserializeDynamicAnchorsBinary(int version, byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			string item = ByteSerializer.DeserializeString(bytes, ref offset);
			m_DynamicAnchorGuids.Add(item);
		}
		if (version >= 48)
		{
			for (int j = 0; j < num; j++)
			{
				m_DynamicAnchors.Add(ByteSerializer.DeserializeVector3(bytes, ref offset));
			}
		}
	}
}
