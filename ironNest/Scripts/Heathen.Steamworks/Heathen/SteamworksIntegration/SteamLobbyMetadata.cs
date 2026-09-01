using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamLobbyData), "Metadata", null)]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLobbyData))]
	public class SteamLobbyMetadata : MonoBehaviour
	{
		[Serializable]
		public struct KeyEventMap
		{
			public string key;

			public UnityEvent<string> onUpdate;

			[NonSerialized]
			[HideInInspector]
			public string PreviousValue;
		}

		[SettingsField(0, false, "Metadata")]
		[Tooltip("A collection of key and values to be set when the Set() function is called.")]
		public List<StringKeyValuePair> dataToSet;

		[SettingsField(0, false, "Metadata")]
		[Tooltip("A collection of key and event, the events will be invoked when the key's data changes on the lobby.")]
		public List<KeyEventMap> onChanged;

		private SteamLobbyData _inspector;

		private void Awake()
		{
		}

		private void HandleMetadataChange(LobbyData lobby, LobbyMemberData? member)
		{
		}

		private void HandleOnChanged(LobbyData arg0)
		{
		}

		public void RefreshKeyValues()
		{
		}

		public void Set()
		{
		}

		public void Set(string key, string value)
		{
		}

		public void Set(StringKeyValuePair data)
		{
		}

		public string Get(string key)
		{
			return null;
		}

		public bool HasKey(string key)
		{
			return false;
		}
	}
}
