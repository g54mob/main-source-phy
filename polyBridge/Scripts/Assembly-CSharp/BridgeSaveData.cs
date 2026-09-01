using System;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSaveData
{
	public int m_Version;

	public List<BridgeJointProxy> m_BridgeJoints;

	public List<BridgeEdgeProxy> m_BridgeEdges;

	public List<BridgeSpringProxy> m_BridgeSprings;

	public List<PistonProxy> m_Pistons;

	public HydraulicsControllerProxy m_HydraulicsController;

	public List<BridgeJointProxy> m_Anchors;

	public List<BridgePillarProxy> m_BridgePillars;

	public List<BridgeJointProxy> m_BridgePillarAnchors;

	public Dictionary<string, string> m_BridgeEdgeColorsPermanent;

	public BridgeSaveData()
	{
		m_Version = BridgeSave.CURRENT_VERSION;
		m_BridgeJoints = new List<BridgeJointProxy>();
		m_BridgeEdges = new List<BridgeEdgeProxy>();
		m_BridgeSprings = new List<BridgeSpringProxy>();
		m_Pistons = new List<PistonProxy>();
		m_HydraulicsController = new HydraulicsControllerProxy();
		m_Anchors = new List<BridgeJointProxy>();
		m_BridgePillars = new List<BridgePillarProxy>();
		m_BridgePillarAnchors = new List<BridgeJointProxy>();
		m_BridgeEdgeColorsPermanent = new Dictionary<string, string>();
	}

	public BridgeSaveData(byte[] bytes, ref int offset)
	{
		DeserializeBinary(bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeInt(m_Version));
		list.AddRange(ByteSerializer.SerializeInt(m_BridgeJoints.Count));
		foreach (BridgeJointProxy bridgeJoint in m_BridgeJoints)
		{
			list.AddRange(bridgeJoint.SerializeBinary());
		}
		list.AddRange(ByteSerializer.SerializeInt(m_BridgeEdges.Count));
		foreach (BridgeEdgeProxy bridgeEdge in m_BridgeEdges)
		{
			list.AddRange(bridgeEdge.SerializeBinary());
		}
		list.AddRange(ByteSerializer.SerializeInt(m_BridgeSprings.Count));
		foreach (BridgeSpringProxy bridgeSpring in m_BridgeSprings)
		{
			list.AddRange(bridgeSpring.SerializeBinary());
		}
		list.AddRange(ByteSerializer.SerializeInt(m_Pistons.Count));
		foreach (PistonProxy piston in m_Pistons)
		{
			list.AddRange(piston.SerializeBinary());
		}
		list.AddRange(m_HydraulicsController.SerializeBinary());
		list.AddRange(ByteSerializer.SerializeInt(m_Anchors.Count));
		foreach (BridgeJointProxy anchor in m_Anchors)
		{
			list.AddRange(anchor.SerializeBinary());
		}
		list.AddRange(ByteSerializer.SerializeInt(m_BridgePillars.Count));
		foreach (BridgePillarProxy bridgePillar in m_BridgePillars)
		{
			list.AddRange(bridgePillar.SerializeBinary());
		}
		list.AddRange(ByteSerializer.SerializeInt(m_BridgePillarAnchors.Count));
		foreach (BridgeJointProxy bridgePillarAnchor in m_BridgePillarAnchors)
		{
			list.AddRange(bridgePillarAnchor.SerializeBinary());
		}
		if (!PolyTwitch.m_IsSerializing)
		{
			list.AddRange(ByteSerializer.SerializeInt(m_BridgeEdgeColorsPermanent.Count));
			foreach (KeyValuePair<string, string> item in m_BridgeEdgeColorsPermanent)
			{
				list.AddRange(ByteSerializer.SerializeString(item.Key));
				list.AddRange(ByteSerializer.SerializeString(item.Value));
			}
		}
		return list.ToArray();
	}

	public void DeserializeBinary(byte[] bytes, ref int offset)
	{
		m_Version = ByteSerializer.DeserializeInt(bytes, ref offset);
		if (m_Version < 2)
		{
			return;
		}
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_BridgeJoints.Add(new BridgeJointProxy(m_Version, bytes, ref offset));
		}
		int num2 = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int j = 0; j < num2; j++)
		{
			m_BridgeEdges.Add(new BridgeEdgeProxy(m_Version, bytes, ref offset));
		}
		if (m_Version >= 7)
		{
			int num3 = ByteSerializer.DeserializeInt(bytes, ref offset);
			for (int k = 0; k < num3; k++)
			{
				m_BridgeSprings.Add(new BridgeSpringProxy(bytes, ref offset));
			}
		}
		int num4 = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int l = 0; l < num4; l++)
		{
			m_Pistons.Add(new PistonProxy(m_Version, bytes, ref offset));
		}
		m_HydraulicsController.DeserializeBinary(m_Version, bytes, ref offset);
		if (m_Version >= 6)
		{
			int num5 = ByteSerializer.DeserializeInt(bytes, ref offset);
			for (int m = 0; m < num5; m++)
			{
				m_Anchors.Add(new BridgeJointProxy(m_Version, bytes, ref offset));
			}
		}
		if (m_Version >= 4 && m_Version < 9)
		{
			ByteSerializer.DeserializeBool(bytes, ref offset);
		}
		if (m_Version >= 11)
		{
			int num6 = ByteSerializer.DeserializeInt(bytes, ref offset);
			for (int n = 0; n < num6; n++)
			{
				m_BridgePillars.Add(new BridgePillarProxy(m_Version, bytes, ref offset));
			}
			int num7 = ByteSerializer.DeserializeInt(bytes, ref offset);
			for (int num8 = 0; num8 < num7; num8++)
			{
				m_BridgePillarAnchors.Add(new BridgeJointProxy(m_Version, bytes, ref offset));
			}
		}
		if (m_Version >= 16)
		{
			try
			{
				int num9 = ByteSerializer.DeserializeInt(bytes, ref offset);
				m_BridgeEdgeColorsPermanent = new Dictionary<string, string>();
				for (int num10 = 0; num10 < num9; num10++)
				{
					string key = ByteSerializer.DeserializeString(bytes, ref offset);
					string value = ByteSerializer.DeserializeString(bytes, ref offset);
					m_BridgeEdgeColorsPermanent.Add(key, value);
				}
			}
			catch (Exception)
			{
				Debug.Log("Caught exception trying to deserialize bridge color entries");
			}
		}
		if (m_Version == 17)
		{
			ByteSerializer.DeserializeBool(bytes, ref offset);
		}
	}

	public BridgeEdgeProxy GetBridgeEdge(string nodeA_Guid, string nodeB_Guid)
	{
		foreach (BridgeEdgeProxy bridgeEdge in m_BridgeEdges)
		{
			if (bridgeEdge.m_NodeA_Guid == nodeA_Guid && bridgeEdge.m_NodeB_Guid == nodeB_Guid)
			{
				return bridgeEdge;
			}
		}
		return null;
	}

	public BridgePillarProxy GetBridgePillar(string guid)
	{
		foreach (BridgePillarProxy bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.m_Guid == guid)
			{
				return bridgePillar;
			}
		}
		return null;
	}

	public Vector3 GetNodePosition(string guid)
	{
		foreach (BridgeJointProxy bridgeJoint in m_BridgeJoints)
		{
			if (bridgeJoint.m_Guid == guid)
			{
				return bridgeJoint.m_Pos;
			}
		}
		foreach (BridgeJointProxy anchor in m_Anchors)
		{
			if (anchor.m_Guid == guid)
			{
				return anchor.m_Pos;
			}
		}
		return new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
	}

	public bool HasPrebuilts()
	{
		foreach (BridgeEdgeProxy bridgeEdge in m_BridgeEdges)
		{
			if (bridgeEdge.m_BridgePrebuiltState != PrebuiltState.NONE)
			{
				return true;
			}
		}
		foreach (BridgePillarProxy bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.m_BridgePrebuiltState != PrebuiltState.NONE)
			{
				return true;
			}
		}
		return false;
	}

	public bool HasHardPrebuilts()
	{
		foreach (BridgeEdgeProxy bridgeEdge in m_BridgeEdges)
		{
			if (bridgeEdge.m_BridgePrebuiltState == PrebuiltState.HARD_LOCKED)
			{
				return true;
			}
		}
		foreach (BridgePillarProxy bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.m_BridgePrebuiltState == PrebuiltState.HARD_LOCKED)
			{
				return true;
			}
		}
		return false;
	}

	public int GetNumSoftPrebuiltMaterials(BridgeMaterialType bridgeMaterialType)
	{
		int num = 0;
		foreach (BridgeEdgeProxy bridgeEdge in m_BridgeEdges)
		{
			if (bridgeEdge.m_BridgePrebuiltState == PrebuiltState.SOFT_LOCKED && bridgeEdge.m_Material == bridgeMaterialType)
			{
				num++;
			}
		}
		foreach (BridgePillarProxy bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.m_BridgePrebuiltState == PrebuiltState.SOFT_LOCKED && bridgeMaterialType == BridgeMaterialType.PILLAR)
			{
				num++;
			}
		}
		return num;
	}

	public int GetNumHardPrebuiltMaterials(BridgeMaterialType bridgeMaterialType)
	{
		int num = 0;
		foreach (BridgeEdgeProxy bridgeEdge in m_BridgeEdges)
		{
			if (bridgeEdge.m_BridgePrebuiltState == PrebuiltState.HARD_LOCKED && bridgeEdge.m_Material == bridgeMaterialType)
			{
				num++;
			}
		}
		foreach (BridgePillarProxy bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.m_BridgePrebuiltState == PrebuiltState.HARD_LOCKED && bridgeMaterialType == BridgeMaterialType.PILLAR)
			{
				num++;
			}
		}
		return num;
	}
}
