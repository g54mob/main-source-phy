using UnityEngine;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[ModularEvents(typeof(SteamAchievementData))]
	[RequireComponent(typeof(SteamAchievementData))]
	public class SteamAchievementChanged : MonoBehaviour
	{
		[EventField]
		public UnityEvent<bool> onChanged;

		private SteamAchievementData _mData;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandleChange(string arg0, bool arg1)
		{
		}
	}
}
