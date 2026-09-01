using Steamworks;
using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamUserData), "Hex Labels", "label")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamUserData))]
	public class SteamUserHexLabel : MonoBehaviour
	{
		public TextMeshProUGUI label;

		private SteamUserData _mSteamUserData;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandlePersonaStateChanged(UserData user, EPersonaChange flag)
		{
		}
	}
}
