using System;
using UnityEngine;

[AddComponentMenu("Blocks/BlockEventVisualController")]
public class BlockEventVisualController : BlockVisualController
{
	public Renderer[] receiveProperties;

	public Action<BlockSkinLoader.SkinPack.Skin> onMeshChanged;

	public Action<BlockSkinLoader.SkinPack.Skin> onMatChanged;

	protected override void SetMesh(Mesh m)
	{
		base.SetMesh(m);
		if (onMeshChanged != null)
		{
			onMeshChanged(selectedSkin);
		}
	}

	protected override void SetMaterial(Material mat)
	{
		base.SetMaterial(mat);
		if (onMatChanged != null)
		{
			onMatChanged(selectedSkin);
		}
	}

	protected override void SetMaterial(Material[] mats, bool splitMats)
	{
		base.SetMaterial(mats, splitMats);
		if (onMatChanged != null)
		{
			onMatChanged(selectedSkin);
		}
	}

	public override bool UpdateMats(Material[] mats, Material[] shortMats)
	{
		bool result = base.UpdateMats(mats, shortMats);
		if (onMatChanged != null)
		{
			onMatChanged(selectedSkin);
		}
		return result;
	}

	protected override void SetMaterialProperties(MaterialPropertyBlock prop)
	{
		base.SetMaterialProperties(prop);
		for (int i = 0; i < receiveProperties.Length; i++)
		{
			receiveProperties[i].SetPropertyBlock(prop);
		}
	}
}
