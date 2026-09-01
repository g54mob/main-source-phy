using System;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class ControllerDataEvent : UnityEvent<InputControllerStateData>
	{
	}
}
