using UnityEngine;

public class DisableAllSkinnedClothes : MonoBehaviour
{
	public void DoIt()
	{
		SkinnedMeshRenderer[] componentsInChildren = GetComponentsInChildren<SkinnedMeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = false;
		}
	}
}
