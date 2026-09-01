using InternalModding.Blocks;
using UnityEngine;

[AddComponentMenu("Blocks/BlockSkinnedVisualController")]
public class BlockSkinnedVisualController : BlockEventVisualController
{
	public MeshFilter meshFilter;

	public SkinnedMeshRenderer meshRenderer;

	public static Mesh empty;

	public bool defaultIsSkinned = true;

	public bool keepSkinned = true;

	public override MeshFilter MeshFilter
	{
		get
		{
			return meshFilter;
		}
	}

	protected override void SetMesh(Mesh m)
	{
		if ((defaultIsSkinned && (!OptionsMaster.skinsEnabled || selectedSkin.isDefault || selectedSkin.mesh == selectedSkin.prefab.DefaultSkin.mesh)) || (!selectedSkin.pack.isDefault && selectedSkin.pack.id != "3dprint" && selectedSkin.pack.type == PackType.Official))
		{
			if (!keepSkinned)
			{
				meshRenderer.enabled = true;
				if (base.Selected)
				{
					UpdateOutline(0);
					UpdateOutline((!Block.IsSelectedExtra) ? 1 : 2);
				}
			}
			meshRenderer.sharedMesh = m;
			if (selectedSkin.shortSkin == null)
			{
				meshFilter.sharedMesh = empty;
				meshFilter.gameObject.SetActive(false);
			}
			else
			{
				meshFilter.sharedMesh = selectedSkin.shortSkin.mesh;
				meshFilter.gameObject.SetActive(true);
			}
		}
		else
		{
			if (!keepSkinned)
			{
				meshRenderer.enabled = false;
			}
			meshRenderer.sharedMesh = empty;
			meshFilter.sharedMesh = m;
			meshFilter.gameObject.SetActive(true);
		}
		if (onMeshChanged != null)
		{
			onMeshChanged(selectedSkin);
		}
	}

	protected override Renderer DefaultRenderer()
	{
		return meshRenderer;
	}

	protected override bool HasRenderer()
	{
		return meshRenderer != null;
	}

	protected override void EnableRenderer(bool e)
	{
		if (!e)
		{
			renVisible.Add(meshRenderer.enabled);
			meshRenderer.enabled = false;
		}
		else
		{
			meshRenderer.enabled = renVisible.Count <= 0 || renVisible[0];
		}
		base.EnableRenderer(e);
	}

	protected override void SetMaterial(Material mat)
	{
		SkinnedMeshRenderer skinnedMeshRenderer = meshRenderer;
		if (skinnedMeshRenderer == null)
		{
			return;
		}
		Material[] sharedMaterials = skinnedMeshRenderer.sharedMaterials;
		if (sharedMaterials.Length > 1)
		{
			for (int i = 0; i < sharedMaterials.Length; i++)
			{
				sharedMaterials[i] = mat;
			}
			skinnedMeshRenderer.sharedMaterials = sharedMaterials;
		}
		else
		{
			skinnedMeshRenderer.sharedMaterial = mat;
		}
		base.SetMaterial(mat);
	}

	protected override void SetMaterial(Material[] mats, bool splitMats)
	{
		SkinnedMeshRenderer skinnedMeshRenderer = meshRenderer;
		if (!(skinnedMeshRenderer == null))
		{
			skinnedMeshRenderer.sharedMaterials = mats;
			base.SetMaterial(mats, false);
		}
	}

	public override bool UpdateMats(Material[] mats, Material[] shortMats)
	{
		if (mats == null || shortMats == null || !CanChangeTexture)
		{
			return false;
		}
		SkinnedMeshRenderer skinnedMeshRenderer = meshRenderer;
		if (skinnedMeshRenderer == null)
		{
			return false;
		}
		skinnedMeshRenderer.sharedMaterials = mats;
		return base.UpdateMats(mats, shortMats);
	}

	protected override void SetMaterialProperties(MaterialPropertyBlock prop)
	{
		if (HasRenderer())
		{
			meshRenderer.SetPropertyBlock(prop);
		}
		for (int i = 0; i < renderers.Length; i++)
		{
			if (!(renderers[i] == null))
			{
				renderers[i].SetPropertyBlock(prop);
			}
		}
	}

	protected override void SetShadow(bool isDefault)
	{
	}

	public override void UpdateShadowCastingMode()
	{
	}

	public override void SetPrefabIcons()
	{
		BlockButtonControl[] buttonIcons = base.Prefab.buttonIcons;
		if (buttonIcons == null || buttonIcons.Length == 0 || buttonIcons[0] == null || !HasRenderer())
		{
			return;
		}
		for (int i = 0; i < buttonIcons.Length; i++)
		{
			if (buttonIcons[i] == null)
			{
				Debug.LogError("ButtonIcon " + i + " on '" + base.name + "' is null!", base.gameObject);
			}
			else
			{
				if (base.CanChangeMesh)
				{
					buttonIcons[i].SetMesh(selectedSkin.mesh);
				}
				if (CanChangeTexture)
				{
					buttonIcons[i].SetMaterial(selectedSkin, false);
				}
			}
		}
	}

	public override void SetBurnedLevel(float pct)
	{
		if (pct != _prevBurnPct)
		{
			Color b = base.Prefab.burnColor;
			if (Block is ModBlockBehaviourHandler)
			{
				b = burning.Color;
			}
			Color color = selectedSkin.material.color;
			SetMaterialProperty("_Color", Color.Lerp(color, b, pct));
			SetMaterialProperty("_EmissCol", pct * Color.white);
			SetMaterialProperty("_Cutoff", 0.5f + pct * pct * 0.5f);
			SetMaterialProperties();
			_prevBurnPct = pct;
		}
	}
}
