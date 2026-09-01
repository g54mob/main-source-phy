using System;
using System.Collections.Generic;
using System.Linq;
using InternalModding.Blocks;
using InternalModding.LevelEntities;
using InternalModding.Mods;
using InternalModding.Triggers;

namespace InternalModding.Loading
{
	public static class ModIds
	{
		private class IdPair
		{
			public Guid ModId;

			public int LocalId;

			public int EffectiveId;

			public ModdedBlock Block;

			public ModdedEntity Entity;

			public ModdedTrigger Trigger;

			public ModContainer Mod;
		}

		private static List<IdPair> Mods = new List<IdPair>();

		private static List<IdPair> Blocks = new List<IdPair>();

		private static List<IdPair> Entities = new List<IdPair>();

		private static List<IdPair> Triggers = new List<IdPair>();

		public static void AssignIds(List<ModContainer> currentMods)
		{
			Mods.Clear();
			Blocks.Clear();
			Entities.Clear();
			Triggers.Clear();
			Comparison<IdPair> comparison = delegate(IdPair p1, IdPair p2)
			{
				int num5 = p1.ModId.CompareTo(p2.ModId);
				if (num5 != 0)
				{
					return num5;
				}
				if (p1.LocalId < p2.LocalId)
				{
					return -1;
				}
				return (p1.LocalId > p2.LocalId) ? 1 : 0;
			};
			List<IdPair> list = new List<IdPair>();
			foreach (ModContainer currentMod in currentMods)
			{
				list.Add(new IdPair
				{
					ModId = currentMod.Info.Id,
					LocalId = 0,
					Mod = currentMod
				});
			}
			list.Sort(comparison);
			for (int num = 0; num < list.Count; num++)
			{
				list[num].EffectiveId = Mods.Count + num;
			}
			Mods.AddRange(list);
			list.Clear();
			foreach (ModContainer currentMod2 in currentMods)
			{
				foreach (ModdedBlock block in currentMod2.Blocks)
				{
					list.Add(new IdPair
					{
						ModId = currentMod2.Info.Id,
						LocalId = block.LocalId,
						Block = block
					});
				}
			}
			list.Sort(comparison);
			for (int num2 = 0; num2 < list.Count; num2++)
			{
				list[num2].Block.Id = SingleInstanceFindOnly<ModManager>.Instance.BlockIdStart + Blocks.Count + num2;
				list[num2].EffectiveId = SingleInstanceFindOnly<ModManager>.Instance.BlockIdStart + Blocks.Count + num2;
			}
			Blocks.AddRange(list);
			list.Clear();
			foreach (ModContainer currentMod3 in currentMods)
			{
				foreach (ModdedEntity entity in currentMod3.Entities)
				{
					list.Add(new IdPair
					{
						ModId = currentMod3.Info.Id,
						LocalId = entity.LocalId,
						Entity = entity
					});
				}
			}
			list.Sort(comparison);
			for (int num3 = 0; num3 < list.Count; num3++)
			{
				list[num3].Entity.Id = SingleInstanceFindOnly<ModManager>.Instance.EntityIdStart + Entities.Count + num3;
				list[num3].EffectiveId = SingleInstanceFindOnly<ModManager>.Instance.EntityIdStart + Entities.Count + num3;
				if (list[num3].Entity.Prefab != null)
				{
					list[num3].Entity.Prefab.GetComponent<LevelPrefab>().ID = list[num3].EffectiveId;
				}
			}
			Entities.AddRange(list);
			list.Clear();
			foreach (ModContainer currentMod4 in currentMods)
			{
				foreach (ModdedTrigger trigger in currentMod4.Triggers)
				{
					list.Add(new IdPair
					{
						ModId = currentMod4.Info.Id,
						LocalId = trigger.LocalId,
						Trigger = trigger
					});
				}
			}
			list.Sort(comparison);
			for (int num4 = 0; num4 < list.Count; num4++)
			{
				list[num4].Trigger.Id = SingleInstanceFindOnly<ModManager>.Instance.TriggerIdStart + Triggers.Count + num4;
				list[num4].EffectiveId = SingleInstanceFindOnly<ModManager>.Instance.TriggerIdStart + Triggers.Count + num4;
			}
			Triggers.AddRange(list);
		}

		public static ModContainer GetModById(string strId, bool includeUnloadedMods = false)
		{
			try
			{
				return GetModById(new Guid(strId), includeUnloadedMods);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public static ModContainer GetModById(Guid id, bool includeUnloadedMods = false)
		{
			ModContainer modContainer = ModManager.Mods.FirstOrDefault((ModContainer m) => m.Info.Id == id);
			if (modContainer == null)
			{
				return null;
			}
			if (!modContainer.IsActive && !includeUnloadedMods)
			{
				return null;
			}
			return modContainer;
		}

		public static int GetModByIdOrName(string input, out ModContainer mod)
		{
			Guid g;
			if (ModdingUtil.TryParseGuid(input, out g))
			{
				mod = GetModById(g, true);
				if (mod == null)
				{
					return -1;
				}
				return 0;
			}
			List<ModContainer> list = ModManager.Mods.Where((ModContainer m) => m.Info.Name == input).ToList();
			if (list.Count == 0)
			{
				mod = null;
				return -1;
			}
			if (list.Count > 1)
			{
				mod = null;
				return -2;
			}
			mod = list[0];
			return 0;
		}

		public static ushort GetEffectiveModId(Guid modId)
		{
			return (ushort)Mods.First((IdPair m) => m.ModId == modId).EffectiveId;
		}

		public static ushort GetEffectiveModId(ModContainer mod)
		{
			return GetEffectiveModId(mod.Info.Id);
		}

		public static int GetEffectiveBlockId(Guid modId, int localId)
		{
			IdPair idPair = Blocks.FirstOrDefault((IdPair p) => p.ModId == modId && p.LocalId == localId);
			return (idPair != null) ? idPair.EffectiveId : 0;
		}

		public static int GetEffectiveEntityId(Guid modId, int localId)
		{
			IdPair idPair = Entities.FirstOrDefault((IdPair p) => p.ModId == modId && p.LocalId == localId);
			return (idPair != null) ? idPair.EffectiveId : 0;
		}

		public static int GetEffectiveTriggerId(Guid modId, int localId)
		{
			IdPair idPair = Triggers.FirstOrDefault((IdPair p) => p.ModId == modId && p.LocalId == localId);
			return (idPair != null) ? idPair.EffectiveId : 0;
		}

		public static int GetReplacementBlockId(int id)
		{
			ModdedBlock moddedBlock = SingleInstanceFindOnly<BlockLoader>.Instance.LoadedBlocks.FirstOrDefault((ModdedBlock b) => b.ReplacesBlock == id);
			if (moddedBlock == null)
			{
				return 0;
			}
			return moddedBlock.Id;
		}

		public static ModContainer GetModByEffectiveId(ushort effectiveId)
		{
			IdPair idPair = Mods.FirstOrDefault((IdPair m) => m.EffectiveId == effectiveId);
			return (idPair != null) ? idPair.Mod : null;
		}

		public static ModdedBlock GetBlockByEffectiveId(int effectiveId)
		{
			IdPair idPair = Blocks.FirstOrDefault((IdPair b) => b.EffectiveId == effectiveId);
			return (idPair != null) ? idPair.Block : null;
		}

		public static ModdedEntity GetEntityByEffectiveId(int effectiveId)
		{
			IdPair idPair = Entities.FirstOrDefault((IdPair e) => e.EffectiveId == effectiveId);
			return (idPair != null) ? idPair.Entity : null;
		}

		public static ModdedTrigger GetTriggerByEffectiveId(int effectiveId)
		{
			IdPair idPair = Triggers.FirstOrDefault((IdPair p) => p.EffectiveId == effectiveId);
			return (idPair != null) ? idPair.Trigger : null;
		}
	}
}
