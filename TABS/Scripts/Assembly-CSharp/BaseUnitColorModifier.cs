using UnityEngine;

public class BaseUnitColorModifier : MonoBehaviour
{
	public MaterialWithID[] swaps;

	private void Start()
	{
		Renderer[] componentsInChildren = base.transform.root.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			for (int j = 0; j < componentsInChildren[i].sharedMaterials.Length; j++)
			{
				for (int k = 0; k < swaps.Length; k++)
				{
					if (componentsInChildren[i].sharedMaterials[j] == swaps[k].fromMat && swaps[k].newMat != null)
					{
						Material[] sharedMaterials = componentsInChildren[i].sharedMaterials;
						sharedMaterials[j] = swaps[k].newMat;
						componentsInChildren[i].sharedMaterials = sharedMaterials;
					}
				}
			}
		}
	}
}
