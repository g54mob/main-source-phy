using System.Collections.Generic;
using Poly.Physics;
using UnityEngine;

public class BridgeRope
{
	public Rope m_PhysicsRope;

	public BridgeEdge m_ParentEdge;

	public GameObject m_LinkPrefab;

	public List<BridgeLink> m_Links = new List<BridgeLink>();

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	private int m_ShaderIDForStress;

	private int m_ShaderIDForColorBlind;

	private GameObject m_LinkContainer;

	private bool m_isEnabled = true;

	public BridgeRope(Rope rope, BridgeEdge edge, GameObject linkPrefab)
	{
		m_PhysicsRope = rope;
		m_ParentEdge = edge;
		m_LinkPrefab = linkPrefab;
		m_LinkContainer = new GameObject();
		m_LinkContainer.name = "RopeLinks";
		m_LinkContainer.transform.SetParent(BridgeRopes.GetRopesContainerTransform());
		m_isEnabled = true;
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
		UpdateLinks();
	}

	public void UpdateManual()
	{
		if (m_isEnabled)
		{
			UpdateLinks();
		}
	}

	public void FixedUpdateManual()
	{
	}

	public void SetStressColor(Color stressColor)
	{
		m_MaterialPropertyBlock.SetColor(BridgeEdges.STRESS_COLOR_SHADER_ID, stressColor);
		foreach (BridgeLink link in m_Links)
		{
			link.UploadPropertyBlock(m_MaterialPropertyBlock);
		}
	}

	public void Desaturate(bool desaturate)
	{
		m_MaterialPropertyBlock.SetFloat(BridgeEdges.DESATURATE_SHADER_ID, desaturate ? 1f : 0f);
		foreach (BridgeLink link in m_Links)
		{
			link.UploadPropertyBlock(m_MaterialPropertyBlock);
		}
	}

	public void Destroy()
	{
		Object.Destroy(m_LinkContainer);
		foreach (BridgeLink link in m_Links)
		{
			link.Destroy();
		}
		m_Links.Clear();
	}

	public void ClearLinksAndDisable()
	{
		foreach (BridgeLink link in m_Links)
		{
			link.Destroy();
		}
		m_Links.Clear();
		m_isEnabled = false;
	}

	private void UpdateLinks()
	{
		Vector3[] array = m_PhysicsRope.ComputeNodePositions();
		if (array.Length < 2)
		{
			return;
		}
		int num = array.Length - 1;
		for (int i = m_Links.Count; i < num; i++)
		{
			BridgeLink bridgeLink = new BridgeLink(m_LinkPrefab, m_LinkContainer.transform);
			if (bridgeLink != null)
			{
				m_Links.Add(bridgeLink);
			}
			m_ParentEdge?.ForceStressVisualizationRefresh();
		}
		int num2 = Mathf.Min(num, m_Links.Count);
		for (int j = 0; j < num2; j++)
		{
			m_Links[j].m_Link.SetActive(value: true);
			m_Links[j].m_Link.transform.position = (array[j] + array[j + 1]) / 2f;
			Vector3 normalized = (array[j + 1] - array[j]).normalized;
			float num3 = 57.29578f * Mathf.Acos(Vector3.Dot(normalized, Vector3.right));
			m_Links[j].m_Link.transform.rotation = Quaternion.identity;
			m_Links[j].m_Link.transform.Rotate(0f, 0f, (Vector3.Dot(Vector3.up, normalized) < 0f) ? (0f - num3) : num3, Space.Self);
			m_Links[j].m_Link.transform.localScale = new Vector3(Vector3.Distance(array[j], array[j + 1]) * 4f, 1f, 1f);
		}
		for (int k = num2; k < m_Links.Count; k++)
		{
			m_Links[k].m_Link.gameObject.SetActive(value: false);
		}
	}
}
