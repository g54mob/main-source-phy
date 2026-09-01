using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu("Steamworks/User")]
	public class SteamUserData : MonoBehaviour, ISteamUserData
	{
		public enum ManagedEvents
		{
			Changed = 0,
			Clicked = 1
		}

		public bool localUser;

		[HideInInspector]
		public UnityEvent<UserData, EPersonaChange> onChanged;

		private UserData _mData;

		[FormerlySerializedAs("m_Delegates")]
		[SerializeField]
		private List<string> mDelegates;

		public UserData Data
		{
			get
			{
				return default(UserData);
			}
			set
			{
			}
		}

		private void Awake()
		{
		}

		private void Start()
		{
		}

		private void HandleInitialization()
		{
		}

		private void OnDestroy()
		{
		}

		private void GlobalPersonaUpdate(UserData user, EPersonaChange changeFlag)
		{
		}
	}
}
