using Poly.Game;
using UnityEngine;

public class BridgeSpringLink
{
	public GameObject m_Link;

	public SpringCoilMeshGenerator m_meshGenerator;

	public SkinnedMeshRenderer m_MeshRenderer;

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	private int m_ShaderIDForStress;

	private int m_ShaderIDForColorBlind;

	public BridgeSpringLink(GameObject prefab, Transform parent)
	{
		m_Link = Object.Instantiate(prefab, parent);
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
		m_MeshRenderer = m_Link.GetComponentInChildren<SkinnedMeshRenderer>();
		m_meshGenerator = m_Link.GetComponent<SpringCoilMeshGenerator>();
		m_meshGenerator.Init();
	}

	public void SetStressColor(Color stressColor)
	{
		m_MaterialPropertyBlock.SetColor(BridgeEdges.STRESS_COLOR_SHADER_ID, stressColor);
		m_MeshRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
	}

	public void Desaturate(bool desaturate)
	{
		m_MaterialPropertyBlock.SetFloat(BridgeEdges.DESATURATE_SHADER_ID, desaturate ? 1f : 0f);
		m_MeshRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
	}

	public void Destroy()
	{
		if ((bool)m_MeshRenderer)
		{
			m_MeshRenderer.sharedMesh = null;
			Object.Destroy(m_MeshRenderer);
			m_MeshRenderer = null;
		}
		Object.Destroy(m_Link);
		m_Link = null;
	}
}
