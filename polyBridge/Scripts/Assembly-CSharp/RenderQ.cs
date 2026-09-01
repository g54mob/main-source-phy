using UnityEngine;

public class RenderQ : MonoBehaviour
{
	public int m_RenderQueueValue;

	public void ManualStart(MeshRenderer renderer, Material[] instantiatedMaterials)
	{
		if ((bool)renderer && instantiatedMaterials != null)
		{
			for (int i = 0; i < instantiatedMaterials.Length; i++)
			{
				instantiatedMaterials[i].renderQueue = m_RenderQueueValue;
			}
		}
	}
}
