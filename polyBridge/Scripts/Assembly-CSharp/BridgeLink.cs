using UnityEngine;

public class BridgeLink
{
	public GameObject m_Link;

	private MeshRenderer m_MeshRenderer;

	public BridgeLink(GameObject prefab, Transform parent)
	{
		m_Link = Object.Instantiate(prefab, parent);
		m_MeshRenderer = m_Link.GetComponent<MeshRenderer>();
	}

	public void UploadPropertyBlock(MaterialPropertyBlock materialPropertyBlock)
	{
		m_MeshRenderer.SetPropertyBlock(materialPropertyBlock);
	}

	public void Destroy()
	{
		Object.Destroy(m_Link);
	}
}
