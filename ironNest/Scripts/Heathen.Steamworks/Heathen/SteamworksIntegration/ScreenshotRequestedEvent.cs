using System;
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class ScreenshotRequestedEvent : UnityEvent<ScreenshotRequested_t>
	{
	}
}
