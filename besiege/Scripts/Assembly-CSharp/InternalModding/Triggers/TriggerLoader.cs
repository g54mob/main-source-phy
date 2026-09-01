using System;
using System.Collections.Generic;
using System.Linq;
using InternalModding.Common;
using InternalModding.LevelEntities;
using InternalModding.Loading;
using InternalModding.Misc;
using InternalModding.Mods;
using Modding;
using Modding.Levels;
using Modding.Serialization;
using Ordered;
using UnityEngine;

namespace InternalModding.Triggers
{
	public class TriggerLoader : SingleInstanceFindOnly<TriggerLoader>, IComponentProvider
	{
		public List<ModdedTrigger> LoadedTriggers;

		private System.Collections.Generic.Dictionary<ModdedTrigger, List<EntityLogic>> TriggersToLogic;

		public override string Name
		{
			get
			{
				return "Trigger Loader";
			}
		}

		public bool ActiveInSingleplayer
		{
			get
			{
				return false;
			}
		}

		public override void SetUp()
		{
			LoadedTriggers = new List<ModdedTrigger>();
			TriggersToLogic = new System.Collections.Generic.Dictionary<ModdedTrigger, List<EntityLogic>>();
			EntityLogic.TriggerLoaderCallback = LogicChanged;
		}

		public bool LoadMod(ModContainer mod)
		{
			bool result = true;
			foreach (ModInfo.TriggerInfo trigger in mod.Info.Triggers)
			{
				trigger.Mod = mod;
				ModdedTrigger moddedTrigger = LoadTrigger(trigger);
				if (moddedTrigger == null)
				{
					result = false;
				}
				else
				{
					mod.Triggers.Add(moddedTrigger);
				}
			}
			return result;
		}

		public bool ActivateMod(ModContainer mod)
		{
			foreach (ModdedTrigger trigger in mod.Triggers)
			{
				LoadedTriggers.Add(trigger);
			}
			return true;
		}

		public void RegisterPrefabs(ModContainer mod)
		{
		}

		public void PostRegisterPrefabs()
		{
		}

		public void UnregisterPrefabs(ModContainer mod)
		{
		}

		private ModdedTrigger LoadTrigger(ModInfo.TriggerInfo info)
		{
			ModdedTrigger moddedTrigger = new ModdedTrigger();
			moddedTrigger.Name = info.Name;
			moddedTrigger.LocalId = info.LocalId;
			moddedTrigger.Info = info;
			ModdedTrigger moddedTrigger2 = moddedTrigger;
			if (info.Mod.Triggers.Any((ModdedTrigger t) => t.Info.LocalId == info.LocalId))
			{
				MLog.Error("Multiple triggers with the same ID: " + info.LocalId);
				return null;
			}
			TriggersToLogic.Add(moddedTrigger2, new List<EntityLogic>());
			return moddedTrigger2;
		}

		public ModdedTrigger GetTriggerById(string modId, int id)
		{
			ModContainer modById = ModIds.GetModById(modId);
			if (modById == null)
			{
				return null;
			}
			return modById.Triggers.FirstOrDefault((ModdedTrigger t) => t.LocalId == id);
		}

		public void RegisterTriggersOnEntities()
		{
			foreach (ModdedTrigger loadedTrigger in LoadedTriggers)
			{
				loadedTrigger.Targets = GetTargets(loadedTrigger.Info);
			}
			IEnumerable<LevelPrefab> enumerable = PrefabMaster.LevelPrefabs.Values.SelectMany((Ordered.Dictionary<int, LevelPrefab> v) => v.Values).Distinct();
			LevelPrefab entity;
			foreach (LevelPrefab item in enumerable)
			{
				entity = item;
				ModdedTrigger[] array = LoadedTriggers.Where((ModdedTrigger trigger) => trigger.Targets.Contains(entity.ID)).ToArray();
				if (array.Length > 0)
				{
					entity.moddedEvents = array.Select((ModdedTrigger t) => t.Id).ToArray();
					entity.events = new List<TriggerType>(entity.events) { TriggerType.Modded }.ToArray();
				}
				else
				{
					entity.moddedEvents = new int[0];
				}
			}
		}

		public Action GetGlobalCallback(ModContainer mod, int id)
		{
			ModdedTrigger trigger = mod.Triggers.FirstOrDefault((ModdedTrigger t) => t.LocalId == id);
			if (trigger == null)
			{
				throw new ArgumentException("No such trigger: " + id);
			}
			return delegate
			{
				TriggerGlobal(trigger);
			};
		}

		public void RegisterLocalCallback(ModContainer mod, int id, ModTriggers.OnTriggerChanged callback)
		{
			ModdedTrigger moddedTrigger = mod.Triggers.FirstOrDefault((ModdedTrigger t) => t.LocalId == id);
			if (moddedTrigger == null)
			{
				throw new ArgumentException("No such trigger: " + id);
			}
			moddedTrigger.OnTriggerChanged += callback;
		}

		public void LogicChanged(EntityLogic logic)
		{
			if (logic.triggerType != TriggerType.Modded)
			{
				if (logic.moddedTriggerType != null)
				{
					logic.moddedTriggerType.TriggerRemoved(Entity.From(logic.entityBehaviour.entity));
					TriggersToLogic[logic.moddedTriggerType].Remove(logic);
					logic.moddedTriggerType = null;
				}
				return;
			}
			KeyValuePair<ModdedTrigger, List<EntityLogic>> keyValuePair = TriggersToLogic.FirstOrDefault((KeyValuePair<ModdedTrigger, List<EntityLogic>> pair) => pair.Value.Contains(logic));
			if (keyValuePair.Key != null)
			{
				keyValuePair.Key.TriggerRemoved(Entity.From(logic.entityBehaviour.entity));
				TriggersToLogic[keyValuePair.Key].Remove(logic);
			}
			Action callback = delegate
			{
				TriggerLocal(logic.entityBehaviour.entity, logic.moddedTriggerType);
			};
			logic.moddedTriggerType.TriggerAdded(Entity.From(logic.entityBehaviour.entity), callback);
			TriggersToLogic[logic.moddedTriggerType].Add(logic);
		}

		private void TriggerGlobal(ModdedTrigger trigger)
		{
			if (!TriggersToLogic.ContainsKey(trigger))
			{
				return;
			}
			List<EntityLogic> list = new List<EntityLogic>(TriggersToLogic[trigger]);
			foreach (EntityLogic item in list)
			{
				if (item.entityBehaviour == null)
				{
					TriggersToLogic[trigger].Remove(item);
				}
			}
			IEnumerable<LevelEntity> enumerable = new List<LevelEntity>(TriggersToLogic[trigger].Select((EntityLogic l) => l.entityBehaviour.entity)).Distinct();
			foreach (LevelEntity item2 in enumerable)
			{
				TriggerLocal(item2, trigger);
			}
		}

		private void TriggerLocal(LevelEntity entity, ModdedTrigger trigger)
		{
			if (StatMaster.isClient && !StatMaster.isLocalSim)
			{
				throw new InvalidOperationException("Cannot activate a trigger from a client!");
			}
			entity.EntityBehaviour.ProcessModdedEvent(trigger);
		}

		private List<int> GetTargets(ModInfo.TriggerInfo info)
		{
			List<int> list = new List<int>();
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < info.TargetChoices.Targets.Length; i++)
			{
				ModInfo.TargetChoices.TargetType targetType = info.TargetChoices.TargetTypes[i];
				object obj = info.TargetChoices.Targets[i];
				switch (targetType)
				{
				case ModInfo.TargetChoices.TargetType.Entity:
				{
					int item = ((VanillaEntityType)obj).Get();
					list.Add(item);
					break;
				}
				case ModInfo.TargetChoices.TargetType.ModdedEntity:
				{
					ModIdPair modIdPair = (ModIdPair)obj;
					Guid modId = ((!modIdPair.ModIdSpecified) ? info.Mod.Info.Id : modIdPair.ModId);
					int effectiveEntityId = ModIds.GetEffectiveEntityId(modId, modIdPair.LocalId);
					if (effectiveEntityId != 0)
					{
						list.Add(effectiveEntityId);
					}
					break;
				}
				case ModInfo.TargetChoices.TargetType.AllOfficialEntities:
					if (!flag)
					{
						IEnumerable<LevelPrefab> source = PrefabMaster.LevelPrefabs.Values.SelectMany((Ordered.Dictionary<int, LevelPrefab> c) => c.Values).Distinct();
						IEnumerable<LevelPrefab> source2 = source.Where((LevelPrefab prefab) => !SingleInstanceFindOnly<EntityLoader>.Instance.IsModEntity(prefab.ID));
						list.AddRange(source2.Select((LevelPrefab entityPrefab) => entityPrefab.ID));
						flag = true;
					}
					break;
				case ModInfo.TargetChoices.TargetType.AllModdedEntities:
					if (!flag2)
					{
						List<ModdedEntity> loadedEntities = SingleInstanceFindOnly<EntityLoader>.Instance.LoadedEntities;
						list.AddRange(loadedEntities.Select((ModdedEntity entityPrefab) => entityPrefab.Id));
						flag2 = true;
					}
					break;
				default:
					Debug.LogError("Unknown trigger target type: " + targetType);
					break;
				}
			}
			list.AddRange(from entity in SingleInstanceFindOnly<EntityLoader>.Instance.LoadedEntities
				let targetIds = entity.Triggers.OfType<ModIdPair>()
				where targetIds.Any(delegate(ModIdPair t)
				{
					Guid guid = ((!t.ModIdSpecified) ? entity.Info.Mod.Info.Id : t.ModId);
					return info.Mod.Info.Id == guid && info.LocalId == t.LocalId;
				})
				select entity.Id);
			return list;
		}
	}
}
