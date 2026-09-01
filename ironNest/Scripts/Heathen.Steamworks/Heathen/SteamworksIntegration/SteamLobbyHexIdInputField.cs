using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamLobbyData), "Hex Inputs", "input")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLobbyData))]
	public class SteamLobbyHexIdInputField : MonoBehaviour
	{
		public TMP_InputField input;

		private SteamLobbyData _inspector;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandleOnChanged(LobbyData arg0)
		{
		}
	}
}
