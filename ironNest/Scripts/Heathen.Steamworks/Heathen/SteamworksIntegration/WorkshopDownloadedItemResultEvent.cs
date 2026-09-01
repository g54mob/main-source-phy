using System;
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class WorkshopDownloadedItemResultEvent : UnityEvent<DownloadItemResult_t>
	{
	}
}
