using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamLobbyData), "Input UI", null)]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLobbyData))]
	public class SteamLobbyInputUI : MonoBehaviour
	{
		[SettingsField(0, false, "Input UI")]
		public bool inputAlwaysReadOnly;

		[SettingsField(0, false, "Input UI")]
		public bool onlyOwnerCanInvite;

		[SettingsField(0, false, "Input UI")]
		public int minimalIdLength;

		[ElementField("Input UI", 0)]
		public TMP_InputField idInput;

		[ElementField("Input UI", 0)]
		public GameObject createElement;

		[ElementField("Input UI", 0)]
		public GameObject joinElement;

		[ElementField("Input UI", 0)]
		public GameObject leaveElement;

		[ElementField("Input UI", 0)]
		public GameObject inviteElement;

		[ElementField("Input UI", 0)]
		public GameObject membersElement;

		[ElementField("Input UI", 0)]
		public GameObject chatElement;

		private SteamLobbyData _mInspector;

		private void Start()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandleOnValueChanged(string arg0)
		{
		}

		private void HandleOnChanged(LobbyData arg0)
		{
		}
	}
}
