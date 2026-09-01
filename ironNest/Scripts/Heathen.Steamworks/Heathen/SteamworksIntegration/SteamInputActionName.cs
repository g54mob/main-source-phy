using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamInputActionData), "Names", "label")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamInputActionData))]
	public class SteamInputActionName : MonoBehaviour
	{
		public TextMeshProUGUI label;

		private SteamInputActionData _mInspector;

		private void Start()
		{
		}

		private void HandleInitialization()
		{
		}

		private void OnEnable()
		{
		}

		public void RefreshName()
		{
		}
	}
}
