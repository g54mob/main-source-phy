using UnityEngine;

public class CopyMaterial : MonoBehaviour
{
	public bool onStart;

	public Renderer source;

	public Renderer[] visObjects;

	public Renderer[] propOnlyObjects;

	private bool replaceMaterial = true;

	private void Start()
	{
		if (onStart)
		{
			CopyMat(source);
		}
	}

	public void CopyMat(Renderer ren)
	{
		MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
		ren.GetPropertyBlock(materialPropertyBlock);
		if (source == null)
		{
			if (ren.materials.Length <= 1 || visObjects.Length > 0)
			{
			}
			if (visObjects.Length > 1)
			{
				Material sharedMaterial = visObjects[0].sharedMaterial;
				for (int i = 1; i < visObjects.Length; i++)
				{
					if (visObjects[i].sharedMaterial != sharedMaterial)
					{
						replaceMaterial = false;
						break;
					}
				}
			}
		}
		for (int j = 0; j < visObjects.Length; j++)
		{
			if (replaceMaterial)
			{
				visObjects[j].sharedMaterial = ren.material;
			}
			visObjects[j].SetPropertyBlock(materialPropertyBlock);
		}
		for (int k = 0; k < propOnlyObjects.Length; k++)
		{
			propOnlyObjects[k].SetPropertyBlock(materialPropertyBlock);
		}
	}

	public void Init(int length)
	{
		visObjects = new Renderer[length];
	}
}
