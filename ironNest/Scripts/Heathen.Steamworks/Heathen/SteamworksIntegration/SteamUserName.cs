using Steamworks;
using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamUserData), "Names", "label")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamUserData))]
	public class SteamUserName : MonoBehaviour
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
