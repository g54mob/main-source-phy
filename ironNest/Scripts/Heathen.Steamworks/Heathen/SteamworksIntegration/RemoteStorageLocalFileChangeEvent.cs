using System;
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class RemoteStorageLocalFileChangeEvent : UnityEvent<RemoteStorageLocalFileChange_t>
	{
	}
}
