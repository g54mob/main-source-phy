using UnityEngine;

public class ReplaceOnTencent : MonoBehaviour
{
	public MeshRenderer currentRenderer;

	public MeshFilter filter;

	public Mesh replacementMesh;

	public Material replacementMaterial;

	public void Awake()
	{
		if (!SingleInstance<StatMaster>.Instance.LowViolence)
		{
			Object.Destroy(this);
			return;
		}
		if (currentRenderer != null && replacementMaterial != null)
		{
			currentRenderer.material = replacementMaterial;
		}
		if (filter != null)
		{
			filter.mesh = replacementMesh;
		}
	}
}
