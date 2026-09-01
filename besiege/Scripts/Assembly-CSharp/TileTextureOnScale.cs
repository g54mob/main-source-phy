using UnityEngine;

public class TileTextureOnScale : MonoBehaviour
{
	public MeshRenderer[] renderers;

	public float multiplier = 1f;

	public bool splitComponents;

	private Vector2 ratio;

	private Material scaleMaterial;

	private Vector3 lastScale = Vector3.zero;

	private Material xzMaterial;

	private Material yzMaterial;

	private Material xyMaterial;

	private void Start()
	{
		MeshRenderer meshRenderer = renderers[0];
		scaleMaterial = meshRenderer.material;
		Vector3 vector = scaleMaterial.mainTextureScale;
		if (splitComponents)
		{
			if (renderers.Length == 6)
			{
				MeshRenderer obj = renderers[0];
				Material sharedMaterial = (xzMaterial = scaleMaterial);
				renderers[1].sharedMaterial = sharedMaterial;
				obj.sharedMaterial = sharedMaterial;
				xzMaterial.name = "XZ";
				MeshRenderer obj2 = renderers[2];
				sharedMaterial = (yzMaterial = Object.Instantiate(scaleMaterial));
				renderers[3].sharedMaterial = sharedMaterial;
				obj2.sharedMaterial = sharedMaterial;
				yzMaterial.name = "YZ";
				MeshRenderer obj3 = renderers[4];
				sharedMaterial = (xyMaterial = Object.Instantiate(scaleMaterial));
				renderers[5].sharedMaterial = sharedMaterial;
				obj3.sharedMaterial = sharedMaterial;
				xyMaterial.name = "XY";
			}
			else
			{
				Debug.LogWarning("SplitComponents with wrong amount of renderers active!");
			}
		}
		else
		{
			float num = ((!(vector.y > vector.x)) ? vector.x : vector.y);
			ratio = vector / num * multiplier;
			if (renderers.Length > 1)
			{
				for (int i = 1; i < renderers.Length; i++)
				{
					meshRenderer = renderers[i];
					meshRenderer.sharedMaterial = scaleMaterial;
				}
			}
		}
		UpdateTiling();
	}

	private void OnBecameVisible()
	{
		base.enabled = true;
	}

	private void OnBecameInvisible()
	{
		base.enabled = false;
	}

	private void Update()
	{
		UpdateTiling();
	}

	private void UpdateTiling()
	{
		Vector3 lossyScale = base.transform.lossyScale;
		if (!(lossyScale == lastScale))
		{
			if (splitComponents)
			{
				float x = lossyScale.x * multiplier;
				float y = lossyScale.y * multiplier;
				float num = lossyScale.z * multiplier;
				xzMaterial.mainTextureScale = new Vector2(x, num);
				xyMaterial.mainTextureScale = new Vector2(x, y);
				yzMaterial.mainTextureScale = new Vector2(num, y);
			}
			else
			{
				scaleMaterial.mainTextureScale = new Vector2(lossyScale.x * ratio.x, lossyScale.y * ratio.y);
			}
			lastScale = lossyScale;
		}
	}
}
