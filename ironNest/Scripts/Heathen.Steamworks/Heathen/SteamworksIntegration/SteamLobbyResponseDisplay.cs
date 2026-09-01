using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[HelpURL("https://kb.heathen.group/steam/features/lobby/unity-lobby/steam-lobby-response-display")]
	public class SteamLobbyResponseDisplay : MonoBehaviour
	{
		[Header("Configuration")]
		public float hideAfterSeconds;

		[FormerlySerializedAs("Success")]
		public SteamText success;

		[FormerlySerializedAs("DoesntExist")]
		public SteamText doesntExist;

		[FormerlySerializedAs("NotAllowed")]
		public SteamText notAllowed;

		[FormerlySerializedAs("Full")]
		public SteamText full;

		[FormerlySerializedAs("Error")]
		public SteamText error;

		[FormerlySerializedAs("Banned")]
		public SteamText banned;

		[FormerlySerializedAs("Limited")]
		public SteamText limited;

		[FormerlySerializedAs("ClanDisabled")]
		public SteamText clanDisabled;

		[FormerlySerializedAs("CommunityBan")]
		public SteamText communityBan;

		[FormerlySerializedAs("MemberBlockedYou")]
		public SteamText memberBlockedYou;

		[FormerlySerializedAs("YouBlockedMember")]
		public SteamText youBlockedMember;

		[FormerlySerializedAs("RateLimitExceeded")]
		public SteamText rateLimitExceeded;

		[Header("Elements")]
		public TMP_InputField outputElement;

		public GameObject displayElement;

		[Header("Events")]
		public UnityEvent onDisplay;

		public UnityEvent onHide;

		private void Start()
		{
		}

		public string GetString(EChatRoomEnterResponse response)
		{
			return null;
		}

		public void DisplayResponse(EChatRoomEnterResponse response)
		{
		}

		public void HideDisplay()
		{
		}
	}
}
