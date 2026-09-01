using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[RequireComponent(typeof(SteamInputControllerData))]
	public class SteamInputControllerVibrate : MonoBehaviour
	{
		[SettingsField(0, false, "Vibrate")]
		[Range(0f, 1f)]
		public float left;

		[SettingsField(0, false, "Vibrate")]
		[Range(0f, 1f)]
		public float right;

		private SteamInputControllerData _mInspector;

		private void Awake()
		{
		}

		private void Update()
		{
		}
	}
}
