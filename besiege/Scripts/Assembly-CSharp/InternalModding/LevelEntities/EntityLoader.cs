using System;
using System.Collections.Generic;
using System.Linq;
using InternalModding.Blocks;
using InternalModding.Common;
using InternalModding.Loading;
using InternalModding.Misc;
using InternalModding.Mods;
using InternalModding.Triggers;
using UnityEngine;

namespace InternalModding.LevelEntities
{
	public class EntityLoader : SingleInstanceFindOnly<EntityLoader>, IComponentProvider
	{
		public GameObject EntityTemplate;

		public GameObject ParticlesTemplate;

		public GameObject ParticleTemplate;

		public Material LoadingMaterial;

		public Mesh LoadingMesh;

		public Transform ParticlesParent;

		public override string Name
		{
			get
			{
				return "EntityLoader";
			}
		}

		public bool ActiveInSingleplayer
		{
			get
			{
				return false;
			}
		}

		public List<ModdedEntity> LoadedEntities { get; private set; }

		public bool IsModEntity(int id)
		{
			return LoadedEntities.Any((ModdedEntity e) => e.Id == id);
		}

		public string GetEntityName(int id)
		{
			return ModIds.GetEntityByEffectiveId(id).Name;
		}

		public int CountHiddenEntities(StatMaster.Category cat)
		{
			return LoadedEntities.Count((ModdedEntity e) => e.HideInUI && e.Category == cat);
		}

		public bool IsHiddenEntity(int id)
		{
			ModdedEntity entityByEffectiveId = ModIds.GetEntityByEffectiveId(id);
			return entityByEffectiveId != null && entityByEffectiveId.HideInUI;
		}

		public override void SetUp()
		{
			setUp = true;
			LoadedEntities = new List<ModdedEntity>();
			ModReloading.OnModReload += delegate(ModContainer mod, ModInfo newInfo)
			{
				foreach (ModInfo.EntityInfo entity in newInfo.Entities)
				{
					entity.Mod = mod;
					ApplyNewInfo(entity);
				}
			};
		}

		public bool LoadMod(ModContainer mod)
		{
			bool result = true;
			foreach (ModInfo.EntityInfo entity2 in mod.Info.Entities)
			{
				entity2.Mod = mod;
				ModdedEntity entity = LoadFile(entity2);
				if (entity == null)
				{
					result = false;
				}
				else if (entity.Info.Mod.Entities.Any((ModdedEntity e) => e != entity && e.LocalId == entity.LocalId))
				{
					MLog.Error("Multiple entities with the same ID: " + entity.LocalId);
					result = false;
				}
				else
				{
					mod.Entities.Add(entity);
				}
			}
			return result;
		}

		public bool ActivateMod(ModContainer mod)
		{
			bool result = true;
			foreach (ModdedEntity entity in mod.Entities)
			{
				try
				{
					LoadedEntities.Add(entity);
					CreatePrefab(entity);
				}
				catch (Exception ex)
				{
					MLog.Error("Error activating entity " + entity.Name + " from " + mod.Info.Name + ":");
					MLog.Error(ex.ToString());
					result = false;
				}
			}
			return result;
		}

		public void RegisterPrefabs(ModContainer mod)
		{
			foreach (ModdedEntity entity in mod.Entities)
			{
				RegisterPrefab(entity);
			}
		}

		public void UnregisterPrefabs(ModContainer mod)
		{
			foreach (ModdedEntity entity in mod.Entities)
			{
				UnregisterPrefab(entity);
			}
		}

		public void PostRegisterPrefabs()
		{
		}

		public ModdedEntity LoadFile(ModInfo.EntityInfo info)
		{
			ModdedEntity moddedEntity = ModXmlLoader.Deserialize<ModdedEntity>(info.Path, true);
			if (moddedEntity == null)
			{
				MLog.Error("Error loading " + info.Path);
				return null;
			}
			moddedEntity.Info = info;
			moddedEntity.LoadAssets();
			moddedEntity.HideInUI = ModStatus.IsEntityHidden(moddedEntity);
			return moddedEntity;
		}

		public void ApplyNewInfo(ModInfo.EntityInfo info)
		{
			if (!StatMaster.isMP)
			{
				return;
			}
			ModdedEntity moddedEntity = LoadedEntities.FirstOrDefault((ModdedEntity e) => e.Info.Mod == info.Mod && e.Info.Path == info.Path);
			if (moddedEntity == null)
			{
				MLog.Error("Can't find corresponding loaded entity to " + info.Path);
				return;
			}
			ModdedEntity moddedEntity2 = LoadFile(info);
			if (moddedEntity2 == null)
			{
				MLog.Error("Error re-loading entity: " + moddedEntity.Name);
			}
			else
			{
				Serialization.Reload(moddedEntity, moddedEntity2);
			}
		}

		private void CreatePrefab(ModdedEntity entity)
		{
			if (!entity.PrefabCreated)
			{
				EntityPrefabCreator.CreatePrefab(entity, SingleInstanceFindOnly<BlockLoader>.Instance.TempPrefabParent);
				entity.PrefabCreated = true;
				entity.Info.Mod.OnEntityPrefabCreation(entity.LocalId, entity.Prefab);
			}
		}

		private void RegisterPrefab(ModdedEntity entity)
		{
			if (entity.PrefabRegistered)
			{
				return;
			}
			if (entity.Prefab == null)
			{
				Debug.LogError("[EntityLoader]: " + entity.Name + " is missing Prefab, id: " + entity.Id + ", localid: " + entity.LocalId);
				entity.PrefabRegistered = false;
				return;
			}
			Transform transform = GameObject.Find("_PERSISTENT/OBJECTS/Prefabs").transform;
			Transform transform2 = transform.FindChild("Modded");
			Transform transform3;
			if (transform2 == null)
			{
				transform3 = new GameObject("Modded").transform;
				transform3.parent = transform;
			}
			else
			{
				transform3 = transform2.transform;
			}
			entity.Prefab.transform.parent = transform3;
			entity.Prefab.SetActive(false);
			PrefabMaster.CreateLevelPrefab(entity.Prefab.transform);
			entity.PrefabRegistered = true;
			LevelPrefab component = entity.Prefab.GetComponent<LevelPrefab>();
			component.ID = entity.Id;
			component.LocalisationID = -entity.Id;
			SingleInstanceFindOnly<TriggerLoader>.Instance.RegisterTriggersOnEntities();
		}

		public void UnregisterPrefab(ModdedEntity entity)
		{
			if (entity.PrefabRegistered)
			{
				entity.Prefab.transform.parent = SingleInstanceFindOnly<BlockLoader>.Instance.TempPrefabParent;
				PrefabMaster.RemoveLevelPrefab(entity.Category, entity.Id);
				entity.PrefabRegistered = false;
			}
		}
	}
}
