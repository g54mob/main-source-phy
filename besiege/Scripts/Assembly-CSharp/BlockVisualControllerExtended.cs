using System.Collections.Generic;
using UnityEngine;

public class BlockVisualControllerExtended : BlockVisualController
{
	public int[] otherBlocksSkinsAllowed;

	private bool optionsChecked;

	private bool downloadedChecked;

	protected List<BlockSkinLoader.SkinPack.Skin> _options = new List<BlockSkinLoader.SkinPack.Skin>();

	protected List<BlockSkinLoader.SkinPack.Skin> _downloaded = new List<BlockSkinLoader.SkinPack.Skin>();

	protected override List<BlockSkinLoader.SkinPack.Skin> _Options()
	{
		if (!optionsChecked)
		{
			_options = base.Prefab.AvailableSkins;
			if (!Block.stripped)
			{
				for (int i = 0; i < otherBlocksSkinsAllowed.Length; i++)
				{
					_options.AddRange(PrefabMaster.BlockPrefabs[otherBlocksSkinsAllowed[i]].AvailableSkins);
				}
			}
			optionsChecked = true;
		}
		return _options;
	}

	public override List<BlockSkinLoader.SkinPack.Skin> CustomOptions()
	{
		if (!downloadedChecked)
		{
			_downloaded = base.Prefab.downloadedSkins;
			if (!Block.stripped)
			{
				for (int i = 0; i < otherBlocksSkinsAllowed.Length; i++)
				{
					_downloaded.AddRange(PrefabMaster.BlockPrefabs[otherBlocksSkinsAllowed[i]].downloadedSkins);
				}
			}
			downloadedChecked = true;
		}
		return _downloaded;
	}

	protected void LateUpdate()
	{
		optionsChecked = false;
	}

	public bool AcceptedPrefab(BlockPrefab p)
	{
		if (p == null)
		{
			return false;
		}
		if (p.ID == base.Prefab.ID)
		{
			return true;
		}
		for (int i = 0; i < otherBlocksSkinsAllowed.Length; i++)
		{
			if (p.ID == otherBlocksSkinsAllowed[i])
			{
				return true;
			}
		}
		return false;
	}

	public override void PlaceFromBlockInfo(BlockInfo info)
	{
		if (info != null && info.Skin != null)
		{
			if (info.Skin.pack != null)
			{
				BlockSkinLoader.SkinPack.Skin skin = FindVisualOptionFor(info.Skin.pack);
				if (skin == null)
				{
					skin = ((!AcceptedPrefab(info.Skin.prefab)) ? base.Prefab.DefaultSkin : info.Skin);
				}
				Initialize(skin);
			}
			else
			{
				Initialize(base.Prefab.DefaultSkin);
			}
		}
		else
		{
			Initialize(base.Prefab.DefaultSkin);
		}
	}

	public override BlockSkinLoader.SkinPack.Skin FindVisualOptionFor(BlockSkinLoader.SkinPack pack)
	{
		BlockSkinLoader.SkinPack.Skin result = null;
		int num = 0;
		if (pack != null)
		{
			List<BlockSkinLoader.SkinPack.Skin> options = base.Options;
			for (int i = 0; i < options.Count; i++)
			{
				if (options[i].pack == pack)
				{
					if (options[i].prefab == base.Prefab)
					{
						return options[i];
					}
					if (num < 3)
					{
						result = options[i];
						num = 3;
					}
				}
				else if (!string.IsNullOrEmpty(pack.id) && !char.IsLetter(pack.id[0]))
				{
					if (options[i].pack.id == pack.id && options[i].prefab == base.Prefab)
					{
						return options[i];
					}
					if (options[i].pack.name == pack.name && num < 2)
					{
						result = options[i];
						num = 2;
					}
				}
				else if (options[i].pack.name == pack.name)
				{
					if (options[i].pack.id == pack.id && options[i].prefab == base.Prefab)
					{
						return options[i];
					}
					if (num < 1)
					{
						result = options[i];
						num = 1;
					}
				}
			}
		}
		return result;
	}

	public override void UpdateVisFromBlockInfo(BlockInfo info)
	{
		if (info != null && info.Skin != null && info.Skin.pack != null)
		{
			BlockSkinLoader.SkinPack.Skin skin = FindVisualOptionFor(info.Skin.pack);
			if (skin == null)
			{
				skin = ((!AcceptedPrefab(info.Skin.prefab)) ? base.Prefab.DefaultSkin : info.Skin);
			}
			UpdateVis(skin);
		}
	}

	public override bool UpdateVis(BlockSkinLoader.SkinPack.Skin skin = null)
	{
		if (isDestroyed)
		{
			return false;
		}
		if (Block.isBuildBlock && Block.hasSimBlock)
		{
			Block.SimBlock.VisualController.UpdateVis(skin);
		}
		if (base.Prefab.CanGetNewVisuals)
		{
			if (OptionsMaster.skinsEnabled != skinsWereEnabled || !hasBeenAssigned || skin != null || selectedSkin == null || selectedSkin != prevSelectedSkin || selectedSkin.enabled != skinWasEnabled || selectedSkin.pack.deleted)
			{
				skinsWereEnabled = OptionsMaster.skinsEnabled;
				if (skin == null)
				{
					skin = selectedSkin;
				}
				if (skin == null || skin.pack.deleted)
				{
					skin = base.Prefab.DefaultSkin;
				}
				if (skin == null)
				{
					return false;
				}
				if (AcceptedPrefab(skin.prefab))
				{
					if (skin != selectedSkin)
					{
						if (selectedSkin != null && selectedSkin.shortSkin != null)
						{
							selectedSkin.shortSkin.Unregister(this);
						}
						if (selectedSkin != null)
						{
							selectedSkin.Unregister(this);
						}
						selectedSkin = skin.Register(this);
						if (selectedSkin != null && selectedSkin.shortSkin != null)
						{
							selectedSkin.shortSkin.Register(this);
						}
					}
					prevSelectedSkin = selectedSkin;
					skinWasEnabled = selectedSkin.enabled;
					hasBeenAssigned = selectedSkin.doneLoading && (selectedSkin.shortSkin == null || selectedSkin.shortSkin.doneLoading);
					AssignSkin(selectedSkin);
					return true;
				}
			}
		}
		else if (CanChangeTexture && selectedSkin == null)
		{
			selectedSkin = base.Prefab.DefaultSkin;
		}
		return false;
	}

	protected override void SetMesh(BlockSkinLoader.SkinPack.Skin selectedSkin)
	{
		if (OptionsMaster.skinsEnabled && selectedSkin.enabled)
		{
			base.SetMesh(selectedSkin);
		}
		else
		{
			MeshFilter.sharedMesh = base.Prefab.DefaultSkin.mesh;
		}
	}

	protected override void SetMaterial(BlockSkinLoader.SkinPack.Skin selectedSkin)
	{
		if (OptionsMaster.skinsEnabled && selectedSkin.enabled)
		{
			SetNormal();
		}
		else if (!NormalBypassCases() && !StatMaster.isHeadless)
		{
			UpdateMats(base.Prefab.DefaultSkin.materials, null);
		}
	}

	public override bool UpdateMat(Material mat)
	{
		if (mat == null)
		{
			return false;
		}
		return UpdateMats(new Material[1] { mat }, null);
	}

	public override bool UpdateMats(Material[] mats, Material[] shortMats)
	{
		if (mats == null || !CanChangeTexture)
		{
			return false;
		}
		lastClusterIndex = -3;
		for (int i = 0; i < renderers.Length; i++)
		{
			MeshRenderer meshRenderer = renderers[i];
			if (!(meshRenderer == null))
			{
				meshRenderer.sharedMaterials = mats;
			}
		}
		return true;
	}
}
