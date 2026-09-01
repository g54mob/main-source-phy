using Steamworks;
using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamUserData), "Hex Inputs", "input")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamUserData))]
	public class SteamUserHexInput : MonoBehaviour
	{
		public TMP_InputField input;

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
