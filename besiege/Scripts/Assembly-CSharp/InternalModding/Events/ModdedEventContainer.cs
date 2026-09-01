using System.Collections.Generic;
using System.Linq;
using Modding;

namespace InternalModding.Events
{
	public class ModdedEventContainer : EventContainer.PickContainer
	{
		private ModdedEvent _event;

		public Dictionary<string, EventProperty> Properties;

		public ModdedEvent Event
		{
			get
			{
				return _event;
			}
			set
			{
				if (_event != null)
				{
					_event.UnregisterContainer(this);
				}
				_event = value;
				SetDefaultPropertyValues();
				if (_event != null)
				{
					loadOffset = 2 + Properties.Count;
					_event.RegisterContainer(this);
				}
			}
		}

		public ModdedEventContainer()
		{
			if (SingleInstanceFindOnly<EventLoader>.Instance.LoadedEvents.Count > 0)
			{
				Event = SingleInstanceFindOnly<EventLoader>.Instance.LoadedEvents[0];
			}
			else
			{
				Event = null;
			}
		}

		private void SetDefaultPropertyValues()
		{
			if (Event == null)
			{
				Properties = null;
				return;
			}
			Properties = Event.Properties.ToDictionary((EventProperty p) => p.Name, (EventProperty p) => p.CreateInstance());
		}

		public override EventContainer Clone()
		{
			ModdedEventContainer moddedEventContainer = new ModdedEventContainer();
			moddedEventContainer.Event = Event;
			moddedEventContainer.Properties = Properties.Values.ToDictionary((EventProperty p) => p.Name, (EventProperty p) => p.CreateInstance());
			return moddedEventContainer;
		}

		public override bool IsProgressEvent()
		{
			return false;
		}

		public override void Load(string[] stringData)
		{
			string modId = stringData[EntityEvent.LoadOffset];
			string id = stringData[EntityEvent.LoadOffset + 1];
			Event = SingleInstanceFindOnly<EventLoader>.Instance.GetEventById(modId, id);
			for (int i = EntityEvent.LoadOffset + 2; i < stringData.Length; i++)
			{
				string[] array = stringData[i].Split('=');
				if (array.Length >= 2)
				{
					string key = array[0];
					string data = array[1];
					if (Properties.ContainsKey(key))
					{
						Properties[key].Load(data);
					}
				}
			}
			base.Load(stringData);
		}

		public override string Save()
		{
			string text = Event.Info.Mod.Info.Id.ToString();
			string identifier = Event.Identifier;
			string text2 = string.Join("|", Properties.Select((KeyValuePair<string, EventProperty> pair) => pair.Key + "=" + pair.Value.Save()).ToArray());
			return text + "|" + identifier + "|" + text2 + "|" + base.Save();
		}

		public override string SaveLoadValue()
		{
			string text = Event.Info.Mod.Info.Id.ToString();
			string identifier = Event.Identifier;
			string text2 = string.Join("|", Properties.Select((KeyValuePair<string, EventProperty> pair) => pair.Key + "=" + pair.Value.Save()).ToArray());
			return text + "|" + identifier + "|" + text2 + "|" + base.SaveLoadValue();
		}
	}
}
