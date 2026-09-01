using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InternalModding.Common;
using InternalModding.Loading;
using InternalModding.Misc;
using InternalModding.Mods;
using Modding.Modules;
using Modding.Modules.Official;
using UnityEngine;

namespace InternalModding.Blocks
{
	public class BlockLoader : SingleInstanceFindOnly<BlockLoader>, IComponentProvider
	{
		public float DefaultDensityMultiplier = 1f;

		public GameObject BlockTemplate;

		public GameObject Joint2Template;

		public GameObject GhostTemplate;

		public GameObject TabButtonTemplate;

		public GameObject SubTabButtonTemplate;

		public GameObject TabTemplate;

		public GameObject BlockButtonTemplate;

		public Transform TempPrefabParent;

		public Material LoadingMaterial;

		public Material ColliderVisualMaterial;

		public Material TriggerVisualMaterial;

		public Material AddingPointVisualMaterial;

		public Material FireTriggerVisualMaterial;

		public ModulePrefabs ModulePrefabs;

		public ModBlockThumbnailCreator ThumbnailCreator;

		public List<ModdedBlock> LoadedBlocks;

		private Dictionary<int, ModdedBlock> ShootingModuleProjectiles;

		public override string Name
		{
			get
			{
				return "Block Loader";
			}
		}

		public bool ActiveInSingleplayer
		{
			get
			{
				return true;
			}
		}

		public List<ModdedBlock> VisibleBlocks
		{
			get
			{
				return LoadedBlocks.Where((ModdedBlock b) => !b.HideInUI).ToList();
			}
		}

		public int VisibleBlocksCount
		{
			get
			{
				return LoadedBlocks.Count((ModdedBlock b) => !b.HideInUI);
			}
		}

		public int LoadedBlocksCount
		{
			get
			{
				return LoadedBlocks.Count;
			}
		}

		public bool IsModBlock(int id)
		{
			return LoadedBlocks.Any((ModdedBlock b) => b.Id == id);
		}

		public string GetBlockName(int id)
		{
			return ModIds.GetBlockByEffectiveId(id).Name;
		}

		public int GetProjectileBlockType(int projectileId)
		{
			if (!ShootingModuleProjectiles.ContainsKey(projectileId))
			{
				return 0;
			}
			return ShootingModuleProjectiles[projectileId].Id;
		}

		public override void SetUp()
		{
			LoadedBlocks = new List<ModdedBlock>();
			ShootingModuleProjectiles = new Dictionary<int, ModdedBlock>();
			ModReloading.OnModReload += delegate(ModContainer mod, ModInfo newInfo)
			{
				foreach (ModInfo.BlockInfo block in newInfo.Blocks)
				{
					block.Mod = mod;
					ApplyNewInfo(block);
				}
			};
		}

		public void CreateUI()
		{
			TabCreator.DestroyTabs();
			BlockButtonCreator.DestroyBlockButtons();
			TabCreator.CreateTabs();
			CreateBlockButtons();
			StartCoroutine(CreateBlockTypeThumbnails());
		}

		public bool LoadMod(ModContainer mod)
		{
			bool result = true;
			foreach (ModInfo.BlockInfo block2 in mod.Info.Blocks)
			{
				block2.Mod = mod;
				ModdedBlock block = LoadFile(block2);
				if (block == null)
				{
					result = false;
				}
				else if (block.Info.Mod.Blocks.Any((ModdedBlock b) => b != block && b.LocalId == block.LocalId))
				{
					MLog.Error("Multiple blocks with the same ID: " + block.LocalId);
					result = false;
				}
				else
				{
					mod.Blocks.Add(block);
				}
			}
			return result;
		}

		public bool ActivateMod(ModContainer mod)
		{
			bool result = true;
			foreach (ModdedBlock block in mod.Blocks)
			{
				try
				{
					LoadedBlocks.Add(block);
					CreatePrefab(block);
					block.LoadModules();
				}
				catch (Exception ex)
				{
					MLog.Error("Error activating block " + block.Name + " from " + mod.Info.Name + ":");
					MLog.Error(ex.ToString());
					result = false;
				}
			}
			return result;
		}

		public void RegisterPrefabs(ModContainer mod)
		{
			foreach (ModdedBlock block in mod.Blocks)
			{
				RegisterPrefab(block);
			}
		}

		public void UnregisterPrefabs(ModContainer mod)
		{
			foreach (ModdedBlock block in mod.Blocks)
			{
				UnregisterPrefab(block);
			}
		}

		public ModdedBlock LoadFile(ModInfo.BlockInfo info)
		{
			ModdedBlock moddedBlock = ModXmlLoader.Deserialize<ModdedBlock>(info.Path, true);
			if (moddedBlock == null)
			{
				MLog.Error("Error loading " + info.Path);
				return null;
			}
			moddedBlock.Info = info;
			moddedBlock.LoadAssets();
			moddedBlock.HideInUI = ModStatus.IsBlockHidden(moddedBlock);
			return moddedBlock;
		}

		public void ApplyNewInfo(ModInfo.BlockInfo info)
		{
			ModdedBlock moddedBlock = LoadedBlocks.FirstOrDefault((ModdedBlock b) => b.Info.Mod == info.Mod && b.Info.Path == info.Path);
			if (moddedBlock == null)
			{
				MLog.Error("Can't find corresponding loaded block to " + info.Path);
				return;
			}
			ModdedBlock moddedBlock2 = LoadFile(info);
			if (moddedBlock2 == null)
			{
				MLog.Error("Error re-loading block: " + moddedBlock.Name);
			}
			else
			{
				Serialization.Reload(moddedBlock, moddedBlock2);
			}
		}

		private void CreatePrefab(ModdedBlock block)
		{
			if (!block.PrefabCreated)
			{
				BlockPrefabCreator.CreateGhost(block);
				BlockPrefabCreator.CreatePrefab(block);
				block.PrefabCreated = true;
				block.Info.Mod.OnBlockPrefabCreation(block.LocalId, block.Prefab, block.Ghost);
				BlockPrefabCreator.CreateStrippedPrefab(block);
			}
		}

		private void RegisterPrefab(ModdedBlock block)
		{
			if (!block.PrefabRegistered)
			{
				Transform parent = GameObject.Find("_PERSISTENT/BLOCKS/Ghosts").transform;
				Transform parent2 = GameObject.Find("_PERSISTENT/BLOCKS/Prefabs").transform;
				Transform parent3 = GameObject.Find("_PERSISTENT/BLOCKS/StrippedPrefabs").transform;
				block.Ghost.transform.parent = parent;
				block.Prefab.transform.parent = parent2;
				block.StrippedPrefab.transform.parent = parent3;
				if (block.BlockPrefab == null)
				{
					Debug.LogError("Couldn't register modded prefab: Prefabs are null! CreatePrefab needs to run before registering!");
					return;
				}
				block.BlockPrefab.SetGameObject(block.Prefab);
				block.BlockPrefab.SetNameFromGameObject();
				block.BlockPrefab.strippedBlock = block.StrippedPrefab.GetComponent<BlockBehaviour>();
				PrefabMaster.AddBlockPrefab((BlockType)block.Id, block.BlockPrefab);
				block.ReadyForOnPrefabCreation();
				block.PrefabRegistered = true;
			}
		}

		public void PostRegisterPrefabs()
		{
			foreach (BlockSkinLoader.SkinPack skinPack in BlockSkinLoader.SkinPacks)
			{
				BlockSkinLoader.LoadNewSkinsIn(skinPack);
				foreach (BlockSkinLoader.SkinPack.Skin skin in skinPack.skins)
				{
					if (skin.ModId != Guid.Empty)
					{
						skin.SetID(ModIds.GetEffectiveBlockId(skin.ModId, skin.LocalId));
						PrefabMaster.BlockPrefabs.TryGetValue(skin.ID, out skin.prefab);
					}
				}
			}
			ShootingModuleProjectiles.Clear();
			IOrderedEnumerable<ModdedBlock> orderedEnumerable = from b in LoadedBlocks
				where b.Modules.Any((BlockModule m) => m is ShootingModule)
				orderby b.Id
				select b;
			int num = Enum.GetValues(typeof(NetworkProjectileType)).Length;
			foreach (ModdedBlock item in orderedEnumerable)
			{
				BlockModule[] modules = item.Modules;
				foreach (BlockModule blockModule in modules)
				{
					ShootingModule shootingModule = blockModule as ShootingModule;
					if (shootingModule != null)
					{
						shootingModule.ProjectileId = num;
						num++;
						ShootingModuleProjectiles.Add(shootingModule.ProjectileId, item);
					}
				}
			}
			if (!StatMaster.isMP || !ProjectileManager.Instance)
			{
				return;
			}
			ProjectileManager projectileManager = ProjectileManager.Instance;
			projectileManager.ClearAdditionalProjectiles();
			foreach (ModdedBlock item2 in orderedEnumerable)
			{
				ModBlockBehaviourHandler component = item2.Prefab.GetComponent<ModBlockBehaviourHandler>();
				BlockModule[] modules2 = item2.Modules;
				foreach (BlockModule blockModule2 in modules2)
				{
					ShootingModule shootingModule2 = blockModule2 as ShootingModule;
					if (shootingModule2 != null)
					{
						projectileManager.AddAdditionalProjectile(shootingModule2.ProjectileId, shootingModule2.GetProjectilePrefab(component));
					}
				}
			}
		}

		public void UnregisterPrefab(ModdedBlock block)
		{
			if (block.PrefabRegistered)
			{
				block.Ghost.transform.parent = TempPrefabParent;
				block.Prefab.transform.parent = TempPrefabParent;
				block.StrippedPrefab.transform.parent = TempPrefabParent;
				block.BlockPrefab = PrefabMaster.RemoveBlockPrefab(block.Id);
				block.PrefabRegistered = false;
			}
		}

		private void CreateBlockButtons()
		{
			List<ModdedBlock> visibleBlocks = VisibleBlocks;
			for (int i = 0; i < visibleBlocks.Count; i++)
			{
				int index = Mathf.FloorToInt((float)i / (float)TabCreator.MaxBlocksPerTab);
				BlockButtonCreator.CreateBlockButton(visibleBlocks[i], TabCreator.ModTabs[index]);
			}
			TabCreator.RegisterBlocksToTabs();
			BlockButtonCreator.PositionButtons();
		}

		private IEnumerator CreateBlockTypeThumbnails()
		{
			if (VisibleBlocks.All((ModdedBlock b) => b.BlockTypeIcon != null))
			{
				yield break;
			}
			while (VisibleBlocks.Any((ModdedBlock b) => !b.Mesh.Loaded || !b.Texture.Loaded))
			{
				yield return new WaitForSeconds(0.5f);
			}
			while (StatMaster.PlayMode != BesiegePlayMode.BuildMode)
			{
				yield return new WaitForSeconds(0.5f);
			}
			List<ModdedBlock> existingThumbnails = BlockTypeIconCreator.GetExistingThumbnails();
			yield return StartCoroutine(BlockTypeIconCreator.LoadThumbnails(existingThumbnails));
			if (existingThumbnails.Count == VisibleBlocks.Count)
			{
				yield break;
			}
			foreach (ModdedBlock block in VisibleBlocks.Where((ModdedBlock b) => !existingThumbnails.Contains(b)))
			{
				yield return StartCoroutine(BlockTypeIconCreator.CreateBlockThumbnail(block));
			}
			yield return StartCoroutine(BlockTypeIconCreator.LoadThumbnails(LoadedBlocks.Where((ModdedBlock b) => !b.HideInUI && !existingThumbnails.Contains(b))));
		}
	}
}
