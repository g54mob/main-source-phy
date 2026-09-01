using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using InternalModding.Blocks;
using InternalModding.Events;
using InternalModding.LevelEntities;
using InternalModding.Loading;
using InternalModding.Misc;
using InternalModding.UI;
using Modding;
using Modding.Blocks;
using Modding.Levels;
using UnityEngine;

namespace InternalModding.Mods
{
	public static class CompatibilityChecker
	{
		public static void Initialize()
		{
			Modding.Events.OnMachineSave += OnMachineSave;
			Modding.Events.OnLevelSave += OnLevelSave;
			Modding.Events.OnMachineLoaded += OnMachineLoaded;
			Modding.Events.OnLevelLoaded += OnLevelLoaded;
		}

		public static byte[] GetModConfig()
		{
			return ModList.GetLocal().GetBytes();
		}

		public static byte[] GetModConfigHash()
		{
			byte[] bytes = ModList.GetLocal().GetBytes(true);
			SHA256Managed sHA256Managed = new SHA256Managed();
			byte[] array = sHA256Managed.ComputeHash(bytes);
			if (BesiegeLogFilter.logDebug)
			{
				Debug.Log("[ModList] Calculated hash: [" + string.Join(",", array.Select((byte b) => b.ToString()).ToArray()) + "]\nLocal mod list:\n- " + string.Join("\n-", ModList.GetLocal().GetStringArray()));
			}
			return array;
		}

		public static int CompareModConfigHash(byte[] remoteBytes, ref int offset)
		{
			if (remoteBytes.Length < offset + 32)
			{
				Debug.LogError("[ModList] Received connection request without mod list bytes! Did a client without ENABLE_MODDING try to connect?");
				return -1;
			}
			byte[] modConfigHash = GetModConfigHash();
			byte[] array = remoteBytes.Slice(offset, offset + 32);
			if (BesiegeLogFilter.logDebug)
			{
				Debug.Log("[ModList] Comparing hashes.\nLocal: [" + string.Join(",", modConfigHash.Select((byte b) => b.ToString()).ToArray()) + "]\nRemote: [" + string.Join(",", array.Select((byte b) => b.ToString()).ToArray()) + "]");
			}
			offset += 32;
			return modConfigHash.SequenceEqual(array) ? 1 : 0;
		}

		public static string MismatchesToString(List<ModList.Mod> mismatchedMods)
		{
			return string.Join(", ", mismatchedMods.Select((ModList.Mod m) => m.Name + ": " + m.Mismatch).ToArray());
		}

		private static void OnMachineSave(PlayerMachineInfo machineInfo)
		{
			XDataHolder machineData = machineInfo.MachineData;
			ReadOnlyCollection<Modding.Blocks.BlockInfo> blocks = machineInfo.Blocks;
			HashSet<ModContainer> hashSet = new HashSet<ModContainer>();
			Modding.Blocks.BlockInfo blockInfo;
			foreach (Modding.Blocks.BlockInfo item in blocks)
			{
				blockInfo = item;
				ModdedBlock moddedBlock = SingleInstanceFindOnly<BlockLoader>.Instance.LoadedBlocks.FirstOrDefault((ModdedBlock b) => b.Id == blockInfo.Type);
				if (moddedBlock != null)
				{
					hashSet.Add(moddedBlock.Info.Mod);
				}
			}
			IEnumerable<ModContainer> second = from mod in ModManager.Mods.Except(hashSet)
				where mod.IsRequiredForMachine(machineInfo)
				select mod;
			ModList modList = ModList.FromMods(hashSet.Union(second));
			machineData.Write("requiredMods", modList.GetStringArray());
		}

		private static void OnLevelSave(Level level)
		{
			List<LevelEntity> entities = LevelEditor.Instance.Entities;
			HashSet<ModContainer> hashSet = new HashSet<ModContainer>();
			LevelEntity entityInfo;
			foreach (LevelEntity item in entities)
			{
				entityInfo = item;
				ModdedEntity moddedEntity = SingleInstanceFindOnly<EntityLoader>.Instance.LoadedEntities.FirstOrDefault((ModdedEntity e) => e.Id == entityInfo.behaviour.prefab.ID);
				if (moddedEntity != null)
				{
					hashSet.Add(moddedEntity.Info.Mod);
				}
				foreach (EntityLogic logicDatum in entityInfo.behaviour.logicData)
				{
					if (logicDatum.triggerType == TriggerType.Modded)
					{
						hashSet.Add(logicDatum.moddedTriggerType.Info.Mod);
					}
					foreach (EntityEvent @event in logicDatum.events)
					{
						if (@event.eventType == EventContainer.EventType.Modded)
						{
							hashSet.Add(((ModdedEventContainer)@event.eventData).Event.Info.Mod);
						}
					}
				}
			}
			foreach (LevelSettings.LevelMachine allowedMachine in level.Setup.InternalObject.AllowedMachines)
			{
				foreach (BlockInfo block in allowedMachine.GetInfo().Blocks)
				{
					ModdedBlock blockByEffectiveId = ModIds.GetBlockByEffectiveId((int)block.ID);
					if (blockByEffectiveId != null)
					{
						hashSet.Add(blockByEffectiveId.Info.Mod);
					}
				}
			}
			IEnumerable<ModContainer> second = from mod in ModManager.Mods.Except(hashSet)
				where mod.IsRequiredForLevel(level)
				select mod;
			ModList modList = ModList.FromMods(hashSet.Union(second));
			level.CustomData.Write("requiredMods", modList.GetStringArray());
		}

		public static void OnMachineLoaded(PlayerMachineInfo machineInfo)
		{
			XDataHolder machineData = machineInfo.MachineData;
			if (!machineData.HasKey("requiredMods"))
			{
				return;
			}
			string[] array = machineData.ReadStringArray("requiredMods");
			if (array.Length != 1 || !string.IsNullOrEmpty(array[0]))
			{
				ModList remote = ModList.FromStringArray(array);
				List<ModList.Mod> mismatchedMods;
				if (!ModList.GetLocal().Compare(remote, out mismatchedMods, true, false))
				{
					MLog.Warn("Loading machine, missing some mods: " + MismatchesToString(mismatchedMods));
					ModsMissingMessage.ShowMachine(mismatchedMods);
				}
			}
		}

		private static void OnLevelLoaded(Level level)
		{
			if (level.CustomData.HasKey("requiredMods"))
			{
				ModList remote = ModList.FromStringArray(level.CustomData.ReadStringArray("requiredMods"));
				List<ModList.Mod> mismatchedMods;
				if (!ModList.GetLocal().Compare(remote, out mismatchedMods, true, false))
				{
					MLog.Warn("Loading level, missing some mods: " + MismatchesToString(mismatchedMods));
					ModsMissingMessage.ShowLevel(mismatchedMods);
				}
			}
		}
	}
}
