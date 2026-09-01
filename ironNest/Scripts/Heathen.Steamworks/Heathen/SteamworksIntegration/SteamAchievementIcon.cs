using UnityEngine;
using UnityEngine.UI;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamAchievementData), "Icons", "image")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamAchievementData))]
	public class SteamAchievementIcon : MonoBehaviour
	{
		public RawImage image;

		private SteamAchievementData _data;

		private void Awake()
		{
		}

		private void Start()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandleChange(string arg0, bool arg1)
		{
		}

		private void Refresh()
		{
		}
	}
}
