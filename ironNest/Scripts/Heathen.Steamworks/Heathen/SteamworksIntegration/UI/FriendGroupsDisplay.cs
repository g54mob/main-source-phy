using UnityEngine;

namespace Heathen.SteamworksIntegration.UI
{
	[HelpURL("https://kb.heathen.group/assets/steamworks/unity-engine/ui-components/friend-groups-display")]
	public class FriendGroupsDisplay : MonoBehaviour
	{
		[SerializeField]
		private Transform inGameCollection;

		[SerializeField]
		private Transform inOtherGameCollection;

		[SerializeField]
		private Transform groupedCollection;

		[SerializeField]
		private Transform onlineCollection;

		[SerializeField]
		private Transform offlineCollection;

		[SerializeField]
		private GameObject groupPrefab;

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		public void Clear()
		{
		}

		public void UpdateDisplay()
		{
		}
	}
}
