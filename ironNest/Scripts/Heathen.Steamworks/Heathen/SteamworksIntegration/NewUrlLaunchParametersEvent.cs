using System;
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class NewUrlLaunchParametersEvent : UnityEvent<NewUrlLaunchParameters_t>
	{
	}
}
