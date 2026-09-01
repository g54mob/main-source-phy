using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ModIO
{
	[Serializable]
	public class UserEvent
	{
		[JsonProperty("id")]
		public int id;

		[JsonProperty("mod_id")]
		public int modId;

		[JsonProperty("user_id")]
		public int userId;

		[JsonProperty("date_added")]
		public int dateAdded;

		[JsonProperty("user_event_type")]
		public UserEventType eventType;

		private const string APIOBJECT_VALUESTRING_TEAMJOINED = "USER_TEAM_JOIN";

		private const string APIOBJECT_VALUESTRING_TEAMLEFT = "USER_TEAM_LEAVE";

		private const string APIOBJECT_VALUESTRING_MODSUBSCRIBED = "USER_SUBSCRIBE";

		private const string APIOBJECT_VALUESTRING_MODUNSUBSCRIBED = "USER_UNSUBSCRIBE";

		[JsonProperty("event_type")]
		public string _eventTypeString;

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (!string.IsNullOrEmpty(_eventTypeString))
			{
				switch (_eventTypeString.ToUpper())
				{
				case "USER_TEAM_JOIN":
					eventType = UserEventType.TeamJoined;
					break;
				case "USER_TEAM_LEAVE":
					eventType = UserEventType.TeamLeft;
					break;
				case "USER_SUBSCRIBE":
					eventType = UserEventType.ModSubscribed;
					break;
				case "USER_UNSUBSCRIBE":
					eventType = UserEventType.ModUnsubscribed;
					break;
				default:
					eventType = UserEventType._UNKNOWN;
					break;
				}
				_eventTypeString = null;
			}
		}
	}
}
