using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu("Steamworks/Lobby")]
	[HelpURL("https://heathen.group/kb/lobby/")]
	public class SteamLobbyData : MonoBehaviour, ISteamLobbyData
	{
		public enum LoadOnStart
		{
			None = 0,
			Any = 1,
			Party = 2,
			Session = 3,
			General = 4
		}

		public LoadOnStart load;

		[HideInInspector]
		public LobbyDataEvent onChanged;

		private LobbyData _mData;

		[FormerlySerializedAs("m_Delegates")]
		[SerializeField]
		private List<string> mDelegates;

		public LobbyData Data
		{
			get
			{
				return default(LobbyData);
			}
			set
			{
			}
		}

		private void Start()
		{
		}
	}
}
