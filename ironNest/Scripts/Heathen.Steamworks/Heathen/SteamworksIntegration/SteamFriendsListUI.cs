using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[HelpURL(null)]
	[AddComponentMenu("Steamworks/Friend List")]
	public class SteamFriendsListUI : MonoBehaviour
	{
		public enum Filter
		{
			All = 0,
			InThisGame = 1,
			InOtherGame = 2,
			InAnyGame = 3,
			NotInThisGame = 4,
			NotInGame = 5,
			AnyOnline = 6,
			AnyOffline = 7,
			Away = 8,
			Busy = 9,
			Followed = 10
		}

		public bool includeFollowed;

		[SerializeField]
		private Filter filter;

		public Transform content;

		public GameObject recordTemplate;

		private readonly Dictionary<UserData, GameObject> _records;

		public Filter ActiveFilter
		{
			get
			{
				return default(Filter);
			}
			set
			{
			}
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void DelayUpdate()
		{
		}

		private void HandleStateChange(UserData user, EPersonaChange change)
		{
		}

		private void Remove(UserData user)
		{
		}

		private void Add(UserData user)
		{
		}

		private void AddNewRecord(UserData user)
		{
		}

		private void SortRecords()
		{
		}

		public void Clear()
		{
		}

		public void UpdateDisplay()
		{
		}

		public bool MatchFilter(UserData friend)
		{
			return false;
		}
	}
}
