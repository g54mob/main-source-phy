using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ModIO
{
	[Serializable]
	public class ModEvent
	{
		private const string APIOBJECT_VALUESTRING_MODAVAILABLE = "MOD_AVAILABLE";

		private const string APIOBJECT_VALUESTRING_MODUNAVAILABLE = "MOD_UNAVAILABLE";

		private const string APIOBJECT_VALUESTRING_MODEDITED = "MOD_EDITED";

		private const string APIOBJECT_VALUESTRING_MODFILECHANGED = "MODFILE_CHANGED";

		[JsonProperty("id")]
		public int id;

		[JsonProperty("mod_id")]
		public int modId;

		[JsonProperty("user_id")]
		public int userId;

		[JsonProperty("date_added")]
		public int dateAdded;

		[JsonProperty("mod_event_type")]
		public ModEventType eventType;

		[JsonProperty("event_type")]
		private string _eventTypeString;

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (!string.IsNullOrEmpty(_eventTypeString))
			{
				switch (_eventTypeString.ToUpper())
				{
				case "MOD_AVAILABLE":
					eventType = ModEventType.ModAvailable;
					break;
				case "MOD_UNAVAILABLE":
					eventType = ModEventType.ModUnavailable;
					break;
				case "MOD_EDITED":
					eventType = ModEventType.ModEdited;
					break;
				case "MODFILE_CHANGED":
					eventType = ModEventType.ModfileChanged;
					break;
				default:
					eventType = ModEventType._UNKNOWN;
					break;
				}
				_eventTypeString = null;
			}
		}
	}
}
