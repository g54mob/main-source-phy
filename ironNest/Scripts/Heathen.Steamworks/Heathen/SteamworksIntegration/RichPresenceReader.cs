using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	public class RichPresenceReader : MonoBehaviour
	{
		[Serializable]
		public class RichPresenceReaderUpdatedEvent : UnityEvent<RichPresenceReader>
		{
		}

		public RichPresenceReaderUpdatedEvent evtUpdate;

		private UserData _currentUser;

		public AppData App { get; private set; }

		public UserData User
		{
			get
			{
				return default(UserData);
			}
			set
			{
			}
		}

		public Dictionary<string, string> Values { get; private set; }

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		public void Apply(UserData user)
		{
		}

		private void HandleChange(UserData friend, AppData app)
		{
		}
	}
}
