using UnityEngine;
using cakeslice;

[AddComponentMenu("LevelEditor/EntityVisualController")]
public class EntityVisualController : MonoBehaviour
{
	public Outline[] outlines;

	public Renderer[] renderers;

	private Material[] rendererMaterials;

	private bool changedMaterial;

	protected void Awake()
	{
		Init(base.gameObject);
	}

	public void Init(GameObject entityGO)
	{
		if (entityGO != base.gameObject || renderers == null)
		{
			renderers = entityGO.GetComponentsInChildren<Renderer>();
		}
		rendererMaterials = new Material[renderers.Length];
		for (int i = 0; i < renderers.Length; i++)
		{
			rendererMaterials[i] = renderers[i].sharedMaterial;
			if (WaterController.Exist && rendererMaterials[i].renderQueue == 3000)
			{
				rendererMaterials[i].renderQueue = 3001;
			}
		}
		changedMaterial = false;
	}

	public void RemoveOutline()
	{
		for (int i = 0; i < outlines.Length; i++)
		{
			Outline outline = outlines[i];
			if (outline != null)
			{
				Object.Destroy(outline);
				outlines[i] = null;
			}
		}
	}

	public void ApplyMaterial(Material mat)
	{
		if (WaterController.Exist && mat.renderQueue == 3000)
		{
			mat.renderQueue = 3001;
		}
		for (int i = 0; i < renderers.Length; i++)
		{
			if (renderers[i] != null)
			{
				renderers[i].material = mat;
			}
		}
		changedMaterial = true;
	}

	public void Restore()
	{
		if (changedMaterial)
		{
			for (int i = 0; i < renderers.Length; i++)
			{
				renderers[i].material = rendererMaterials[i];
			}
			changedMaterial = false;
		}
	}
}
