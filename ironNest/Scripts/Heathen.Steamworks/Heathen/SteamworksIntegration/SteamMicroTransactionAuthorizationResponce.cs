using System;
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class SteamMicroTransactionAuthorizationResponce : UnityEvent<AppId_t, ulong, bool>
	{
	}
}
