using UnityEngine;
using UnityEngine.UI;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamInputActionData), "Glyphs", "image")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamInputActionData))]
	public class SteamInputActionGlyph : MonoBehaviour
	{
		public RawImage image;

		private SteamInputActionData _mInspector;

		private void Awake()
		{
		}

		private void HandleInitialization()
		{
		}

		private void OnEnable()
		{
		}

		public void RefreshImage()
		{
		}
	}
}
