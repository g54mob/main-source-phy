using System;
using UnityEngine;

[AddComponentMenu("Blocks/Ghost/GhostMaterialController")]
public class GhostMaterialController : MonoBehaviour
{
	[NonSerialized]
	public Material[] startingMaterials;

	public Renderer[] renderers;

	public Material[] originalMaterials;

	public Material redMaterial;

	public bool outOfBounds;

	[NonSerialized]
	public MeshFilter visFilter;

	private bool red;

	public bool isRed
	{
		get
		{
			return red;
		}
	}

	public void Awake()
	{
		if (renderers.Length <= 0)
		{
			return;
		}
		if (originalMaterials.Length <= 0)
		{
			visFilter = renderers[0].GetComponent<MeshFilter>();
			originalMaterials = new Material[renderers.Length];
			if (startingMaterials == null)
			{
				startingMaterials = new Material[renderers.Length];
			}
			for (int i = 0; i < renderers.Length; i++)
			{
				originalMaterials[i] = renderers[i].material;
				startingMaterials[i] = new Material(renderers[i].material);
			}
		}
		else
		{
			visFilter = renderers[0].GetComponent<MeshFilter>();
			if (startingMaterials == null)
			{
				startingMaterials = new Material[originalMaterials.Length];
			}
			for (int j = 0; j < originalMaterials.Length; j++)
			{
				startingMaterials[j] = new Material(originalMaterials[j]);
			}
		}
	}

	public virtual void LateUpdate()
	{
		if (ReferenceMaster.activeMachineSimulating && base.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(false);
		}
	}

	public void SetHalfOpacity()
	{
		if (renderers.Length > 0)
		{
			for (int i = 0; i < renderers.Length; i++)
			{
				Material material = renderers[i].material;
				material.color = new Color(material.color.r, material.color.g, material.color.b, material.color.a * 0.5f);
				renderers[i].material = material;
				originalMaterials[i] = renderers[i].material;
			}
		}
	}

	public void SetRed()
	{
		outOfBounds = true;
		if (red)
		{
			return;
		}
		red = true;
		if (renderers.Length <= 0)
		{
			return;
		}
		for (int i = 0; i < renderers.Length; i++)
		{
			Material[] array = new Material[renderers[i].materials.Length];
			for (int j = 0; j < renderers[i].materials.Length; j++)
			{
				array[j] = redMaterial;
			}
			renderers[i].materials = array;
		}
	}

	public void SetNormal()
	{
		outOfBounds = false;
		if (!red)
		{
			return;
		}
		red = false;
		if (renderers.Length <= 0)
		{
			return;
		}
		for (int i = 0; i < renderers.Length; i++)
		{
			Material[] array = new Material[renderers[i].materials.Length];
			for (int j = 0; j < renderers[i].materials.Length; j++)
			{
				array[j] = originalMaterials[j];
			}
			renderers[i].materials = array;
		}
	}

	public void SetGhostVis(Mesh mesh, Material[] materials)
	{
		SetGhostVis(materials);
		SetGhostVis(mesh);
	}

	public virtual void SetGhostVis(Material[] materials)
	{
		if (originalMaterials.Length > 0)
		{
			if (!outOfBounds)
			{
				renderers[0].materials = materials;
			}
			originalMaterials = materials;
		}
	}

	public virtual void SetGhostVis(Mesh mesh)
	{
		if ((bool)visFilter)
		{
			visFilter.sharedMesh = mesh;
		}
	}
}
