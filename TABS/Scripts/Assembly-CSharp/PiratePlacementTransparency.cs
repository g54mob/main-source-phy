using System;
using UnityEngine;

public class PiratePlacementTransparency : MonoBehaviour
{
	[Serializable]
	public struct TransparentMaterial
	{
		public Material m_Material;

		public int m_MaterialIndex;

		[NonSerialized]
		public Material m_oldMaterial;
	}

	public TransparentMaterial[] Materials;

	public Collider[] colliders;

	private void Awake()
	{
		MeshRenderer component = GetComponent<MeshRenderer>();
		for (int i = 0; i < Materials.Length; i++)
		{
			if (Materials[i].m_MaterialIndex < component.sharedMaterials.Length)
			{
				Materials[i].m_oldMaterial = component.sharedMaterials[Materials[i].m_MaterialIndex];
			}
		}
	}

	public void MakeTransparent()
	{
		if (Materials != null)
		{
			MeshRenderer component = GetComponent<MeshRenderer>();
			Material[] array = new Material[Materials.Length];
			for (int i = 0; i < Materials.Length; i++)
			{
				array[Materials[i].m_MaterialIndex] = Materials[i].m_Material;
			}
			component.sharedMaterials = array;
		}
		if (colliders != null)
		{
			for (int j = 0; j < colliders.Length; j++)
			{
				colliders[j].enabled = false;
			}
		}
	}

	public void MakeVisable()
	{
		if (Materials != null)
		{
			MeshRenderer component = GetComponent<MeshRenderer>();
			Material[] array = new Material[Materials.Length];
			for (int i = 0; i < Materials.Length; i++)
			{
				array[Materials[i].m_MaterialIndex] = Materials[i].m_oldMaterial;
			}
			component.sharedMaterials = array;
		}
		if (colliders != null)
		{
			for (int j = 0; j < colliders.Length; j++)
			{
				colliders[j].enabled = true;
			}
		}
	}
}
