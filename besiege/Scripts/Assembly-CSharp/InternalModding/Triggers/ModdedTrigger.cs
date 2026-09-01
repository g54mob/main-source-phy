using System;
using System.Collections.Generic;
using InternalModding.Mods;
using Modding;
using Modding.Levels;

namespace InternalModding.Triggers
{
	public class ModdedTrigger
	{
		public ModInfo.TriggerInfo Info { get; set; }

		public string Name { get; set; }

		public int LocalId { get; set; }

		public int Id { get; set; }

		public string GlobalIdentifier
		{
			get
			{
				return string.Concat(Info.Mod.Info.Id, "-", LocalId.ToString(StaticSettings.Culture));
			}
		}

		public List<int> Targets { get; set; }

		public event ModTriggers.OnTriggerChanged OnTriggerChanged;

		public void TriggerAdded(Entity entity, Action callback)
		{
			if (this.OnTriggerChanged != null)
			{
				ModdingUtil.PerformCallback(this.OnTriggerChanged, entity, callback, false);
			}
		}

		public void TriggerRemoved(Entity entity)
		{
			if (this.OnTriggerChanged != null)
			{
				ModdingUtil.PerformCallback(this.OnTriggerChanged, entity, null, true);
			}
		}
	}
}
