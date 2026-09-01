using System.Collections.Generic;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Heathen.SteamworksIntegration.UI
{
	[HelpURL("https://kb.heathen.group/assets/steamworks/unity-engine/ui-components/friend-group")]
	public class FriendGroup : MonoBehaviour
	{
		private enum GroupType
		{
			None = 0,
			Online = 1,
			Offline = 2,
			InGame = 3,
			OtherGame = 4
		}

		[SerializeField]
		private TextMeshProUGUI label;

		[SerializeField]
		private TextMeshProUGUI counter;

		[SerializeField]
		private Toggle toggle;

		[SerializeField]
		private GameObject recordTemplate;

		[SerializeField]
		private Transform content;

		private readonly Dictionary<UserData, GameObject> _records;

		private GroupType _type;

		private void OnEnable()
		{
		}

		private void OnDisable()
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

		public void InitializeCustom(string name, List<UserData> users, bool expanded)
		{
		}

		public void InitializeOnline(string name, List<UserData> users, bool expanded)
		{
		}

		public void InitializeOffline(string name, List<UserData> users, bool expanded)
		{
		}

		public void InitializeInGame(string name, List<UserData> users, bool expanded)
		{
		}

		public void InitializeInOther(string groupName, List<UserData> users, bool expanded)
		{
		}
	}
}
