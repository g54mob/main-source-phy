using System.Collections.Generic;
using System.Linq;
using InternalModding.Mods;
using Modding;
using Modding.Levels;

namespace InternalModding.Events
{
	public class ModdedEvent
	{
		public List<EventProperty> Properties;

		internal List<ModdedEventContainer> Containers = new List<ModdedEventContainer>();

		public ModInfo.EventInfo Info { get; set; }

		public string Name { get; set; }

		public string Identifier { get; set; }

		public string GlobalIdentifier
		{
			get
			{
				return string.Concat(Info.Mod.Info.Id, "-", Identifier);
			}
		}

		public ModTexture Icon { get; set; }

		private IEnumerable<EventProperty.Picker> Pickers
		{
			get
			{
				return Properties.OfType<EventProperty.Picker>();
			}
		}

		public bool HasPicker
		{
			get
			{
				return Pickers.Any();
			}
		}

		public StatMaster.Mode.PickMode PickMode
		{
			get
			{
				return HasPicker ? Pickers.ElementAt(0).Mode : StatMaster.Mode.PickMode.None;
			}
		}

		public event ModEvents.OnEventExecute OnEventExecute;

		public void OnExecute(LogicChain logic, IDictionary<string, EventProperty> properties)
		{
			if (this.OnEventExecute != null)
			{
				ModdingUtil.PerformCallback(this.OnEventExecute, logic, properties);
			}
		}

		internal void RegisterContainer(ModdedEventContainer container)
		{
			Containers.Add(container);
		}

		internal void UnregisterContainer(ModdedEventContainer container)
		{
			Containers.Remove(container);
		}
	}
}
