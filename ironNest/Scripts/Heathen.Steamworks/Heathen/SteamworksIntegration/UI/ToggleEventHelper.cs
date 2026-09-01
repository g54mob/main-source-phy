using UnityEngine;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration.UI
{
	public class ToggleEventHelper : MonoBehaviour
	{
		public UnityEvent on;

		public UnityEvent off;

		public void ToggleChanged(bool value)
		{
		}
	}
}
