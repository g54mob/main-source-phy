using System;
using System.Collections.Generic;
using System.Linq;
using InternalModding.Loading;
using InternalModding.Misc;
using InternalModding.Mods;
using Localisation;
using Modding;
using Modding.Levels;
using UnityEngine;

namespace InternalModding.Events
{
	public class EventLoader : SingleInstanceFindOnly<EventLoader>, IComponentProvider
	{
		public GameObject ModdedEventDisplay;

		public GameObject TextInputPrefab;

		public GameObject NumberInputPrefab;

		public GameObject ChoicePrefab;

		public GameObject TogglePrefab;

		public GameObject TeamButtonPrefab;

		public GameObject IconPrefab;

		public GameObject TextPrefab;

		public Texture2D DefaultIcon;

		public List<ModdedEvent> LoadedEvents;

		public override string Name
		{
			get
			{
				return "Event Loader";
			}
		}

		public bool ActiveInSingleplayer
		{
			get
			{
				return false;
			}
		}

		public ModdedEvent GetEventById(string modId, string id)
		{
			ModContainer modById = ModIds.GetModById(modId);
			if (modById == null)
			{
				return null;
			}
			return modById.Events.FirstOrDefault((ModdedEvent e) => e.Identifier == id);
		}

		public ModdedEvent NextEvent(ModdedEvent current)
		{
			int num = LoadedEvents.IndexOf(current);
			return (num >= LoadedEvents.Count - 1) ? LoadedEvents[0] : LoadedEvents[num + 1];
		}

		public ModdedEvent PreviousEvent(ModdedEvent current)
		{
			int num = LoadedEvents.IndexOf(current);
			return (num <= 0) ? LoadedEvents[LoadedEvents.Count - 1] : LoadedEvents[num - 1];
		}

		public override void SetUp()
		{
			LoadedEvents = new List<ModdedEvent>();
			LogicEvent.TypeNames = new Dictionary<EventContainer.EventType, string>();
			GameObject gameObject = UnityEngine.Resources.Load<GameObject>("Prefabs/BlockMapper/LevelEditor/EventContainer");
			Transform transform = gameObject.transform.FindChild("Container");
			foreach (Transform item in transform)
			{
				EventContainer.EventType key = (EventContainer.EventType)(int)Enum.Parse(typeof(EventContainer.EventType), item.name);
				string translation = LocalisationManager.GetTranslation(item.FindChild("Text").GetComponent<LocalisationChild>().translationID);
				LogicEvent.TypeNames[key] = translation;
			}
			foreach (object value in Enum.GetValues(typeof(EventContainer.EventType)))
			{
				EventContainer.EventType eventType = (EventContainer.EventType)(int)value;
				if (!LogicEvent.TypeNames.ContainsKey(eventType))
				{
					LogicEvent.TypeNames[eventType] = eventType.ToString();
				}
			}
			ModReloading.OnModReload += delegate(ModContainer mod, ModInfo newInfo)
			{
				foreach (ModInfo.EventInfo @event in newInfo.Events)
				{
					@event.Mod = mod;
					ApplyNewInfo(@event);
				}
			};
		}

		public bool LoadMod(ModContainer mod)
		{
			bool result = true;
			foreach (ModInfo.EventInfo @event in mod.Info.Events)
			{
				@event.Mod = mod;
				ModdedEvent moddedEvent = LoadEvent(@event);
				if (moddedEvent == null)
				{
					result = false;
				}
				else
				{
					mod.Events.Add(moddedEvent);
				}
			}
			return result;
		}

		public bool ActivateMod(ModContainer mod)
		{
			foreach (ModdedEvent @event in mod.Events)
			{
				LoadedEvents.Add(@event);
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

		private ModdedEvent LoadEvent(ModInfo.EventInfo info)
		{
			EventProperty[] properties = info.Properties;
			if (info.Mod.Events.Any((ModdedEvent e) => e.Info.Id == info.Id))
			{
				MLog.Error("Multiple events with the same ID: " + info.Id);
				return null;
			}
			if (properties.OfType<EventProperty.Picker>().Count() > 1)
			{
				MLog.Error("Invalid properties in " + info.Name + ": More than one picker is not supported!");
				MLog.Error("Not loading event " + info.Id);
				return null;
			}
			List<EventProperty.Choice> list = properties.OfType<EventProperty.Choice>().ToList();
			foreach (EventProperty.Choice item in list)
			{
				int num = item.Options.AsEnumerable().Count((EventProperty.Choice.Option o) => o.IndexSpecified);
				if (num == 0)
				{
					for (int num2 = 0; num2 < item.Options.Length; num2++)
					{
						item.Options[num2].Index = num2;
					}
				}
				else if (num != item.Options.Length)
				{
					MLog.Error("Invalid properties in " + info.Name + ": If one option has an index explicitly specified, all of them are required to have one.");
					MLog.Error("Not loading event " + info.Id);
					return null;
				}
			}
			if (properties.Any((EventProperty eventProperty) => eventProperty.Name.Contains("|") || eventProperty.Name.Contains("=")))
			{
				MLog.Error("Invalid properties in " + info.Name + ": Property name may not contain '|' or '='!");
				return null;
			}
			EventProperty[] array = properties;
			EventProperty prop;
			for (int num3 = 0; num3 < array.Length; num3++)
			{
				prop = array[num3];
				if (properties.Any((EventProperty p) => p.Name == prop.Name && p != prop))
				{
					MLog.Error("Multiple properties with the same name: " + prop.Name);
					MLog.Error("Not loading event " + info.Id);
					return null;
				}
			}
			ModdedEvent moddedEvent = new ModdedEvent();
			moddedEvent.Name = info.Name;
			moddedEvent.Identifier = info.Id.ToString(StaticSettings.Culture);
			moddedEvent.Info = info;
			moddedEvent.Properties = properties.ToList();
			ModdedEvent moddedEvent2 = moddedEvent;
			if (info.IconReference != null)
			{
				moddedEvent2.Icon = (ModTexture)ModResource.Get(info.IconReference, info.Mod);
			}
			foreach (EventProperty property in moddedEvent2.Properties)
			{
				property.LoadAssets(info.Mod);
			}
			return moddedEvent2;
		}

		public void ApplyNewInfo(ModInfo.EventInfo info)
		{
			ModdedEvent moddedEvent = LoadedEvents.FirstOrDefault((ModdedEvent e) => e.Info.Mod == info.Mod && e.Info.Name == info.Name);
			if (moddedEvent == null)
			{
				MLog.Error("Can't find corresponding loaded event to " + info.Name);
				return;
			}
			info.CreateProperties();
			EventProperty[] properties = moddedEvent.Info.Properties;
			EventProperty prop;
			for (int num = 0; num < properties.Length; num++)
			{
				prop = properties[num];
				EventProperty eventProperty = info.Properties.FirstOrDefault((EventProperty p) => p.Name == prop.Name);
				if (eventProperty == null)
				{
					MLog.Warn("Can't find event property " + prop.Name + " in reloaded file!");
					continue;
				}
				prop.Row = eventProperty.Row;
				prop.X = eventProperty.X;
				prop.XSpecified = eventProperty.XSpecified;
				foreach (ModdedEventContainer container in moddedEvent.Containers)
				{
					container.Properties[prop.Name].X = eventProperty.X;
					container.Properties[prop.Name].XSpecified = eventProperty.XSpecified;
				}
			}
			moddedEvent.Properties = moddedEvent.Info.Properties.ToList();
		}

		public void RegisterCallback(ModContainer mod, int id, ModEvents.OnEventExecute callback)
		{
			if (mod == null)
			{
				throw new InvalidOperationException("[Events] RegisterCallback called from an assembly not listed in the mod manifest.");
			}
			ModdedEvent moddedEvent = mod.Events.FirstOrDefault((ModdedEvent e) => e.Identifier == id.ToString(StaticSettings.Culture));
			if (moddedEvent == null)
			{
				throw new InvalidOperationException("[Events] No such event in your mod: " + id);
			}
			moddedEvent.OnEventExecute += callback;
		}

		public void ExecuteEvent(ModdedEventContainer eventContainer, EntityLogic logic)
		{
			if (eventContainer.Event != null)
			{
				IEnumerable<Entity> source = eventContainer.entityEvent.entityList.Select(delegate(long id)
				{
					LevelEntity entity;
					LevelEditor.Instance.Get(id, out entity);
					return Entity.From(entity);
				});
				Dictionary<string, EventProperty> dictionary = eventContainer.Properties.Where((KeyValuePair<string, EventProperty> p) => !(p.Value is EventProperty.Icon) && !(p.Value is EventProperty.Text)).ToDictionary((KeyValuePair<string, EventProperty> p) => p.Key, (KeyValuePair<string, EventProperty> p) => p.Value);
				EventProperty.Picker picker = dictionary.Values.OfType<EventProperty.Picker>().FirstOrDefault();
				if (picker != null)
				{
					picker.Entities = source.ToList();
				}
				eventContainer.Event.OnExecute(LogicChain.From(logic), dictionary);
			}
		}
	}
}
