using System;
using System.Collections.Generic;
using System.Linq;
using InternalModding.Assemblies;
using InternalModding.Blocks;
using InternalModding.Events;
using InternalModding.LevelEntities;
using InternalModding.Misc;
using InternalModding.Triggers;
using Modding;
using Modding.Blocks;
using Modding.Levels;
using UnityEngine;

namespace InternalModding.Mods
{
	public class ModContainer
	{
		public enum State
		{
			Loaded = 0,
			Active = 1,
			ActiveUnregistered = 2
		}

		public List<ModAssembly> Assemblies;

		public List<ModdedBlock> Blocks;

		public List<ModdedEntity> Entities;

		public List<ModResource> Resources;

		public List<ModdedTrigger> Triggers;

		public List<ModdedEvent> Events;

		public ModInfo Info { get; private set; }

		public Texture2D SmallIcon { get; set; }

		public State CurrentState { get; set; }

		public bool loadedMP { get; set; }

		public bool IsActive
		{
			get
			{
				return CurrentState != State.Loaded;
			}
		}

		public bool IsEnabled { get; set; }

		public bool HadLoadOrActivateErrors { get; set; }

		public ModContainer(ModInfo data)
		{
			Info = data;
			Assemblies = new List<ModAssembly>();
			Blocks = new List<ModdedBlock>();
			Entities = new List<ModdedEntity>();
			Resources = new List<ModResource>();
			Triggers = new List<ModdedTrigger>();
			Events = new List<ModdedEvent>();
			HadLoadOrActivateErrors = false;
		}

		public List<Type> GetTypesByName(string name)
		{
			try
			{
				return Assemblies.SelectMany((ModAssembly a) => from t in a.Assembly.GetTypes()
					where t.FullName == name
					select t).ToList();
			}
			catch (Exception ex)
			{
				MLog.Error("Error searching for type " + name + ": " + ex);
				return new List<Type>();
			}
		}

		public bool IsRequiredForMachine(PlayerMachineInfo machine)
		{
			return Assemblies.Any(delegate(ModAssembly a)
			{
				if (!a.HasModEntryPoint)
				{
					return false;
				}
				bool result;
				return ModdingUtil.PerformCallback(() => a.ModEntryPoint.IsRequiredForMachine(machine), out result) && result;
			});
		}

		public bool IsRequiredForLevel(Level level)
		{
			return Assemblies.Any(delegate(ModAssembly a)
			{
				if (!a.HasModEntryPoint)
				{
					return false;
				}
				bool result;
				return ModdingUtil.PerformCallback(() => a.ModEntryPoint.IsRequiredForLevel(level), out result) && result;
			});
		}

		public void OnBlockPrefabCreation(int blockId, GameObject prefab, GameObject ghost)
		{
			foreach (ModAssembly assembly in Assemblies.Where((ModAssembly a) => a.HasModEntryPoint))
			{
				ModdingUtil.PerformCallback(delegate
				{
					assembly.ModEntryPoint.OnBlockPrefabCreation(blockId, prefab, ghost);
				});
			}
		}

		public void OnEntityPrefabCreation(int entityId, GameObject prefab)
		{
			foreach (ModAssembly assembly in Assemblies.Where((ModAssembly a) => a.HasModEntryPoint))
			{
				ModdingUtil.PerformCallback(delegate
				{
					assembly.ModEntryPoint.OnEntityPrefabCreation(entityId, prefab);
				});
			}
		}

		public override bool Equals(object obj)
		{
			ModContainer modContainer = obj as ModContainer;
			if (modContainer == null)
			{
				return false;
			}
			return modContainer.Info.Id == Info.Id;
		}

		public override int GetHashCode()
		{
			return (Info != null) ? Info.GetHashCode() : 0;
		}
	}
}
