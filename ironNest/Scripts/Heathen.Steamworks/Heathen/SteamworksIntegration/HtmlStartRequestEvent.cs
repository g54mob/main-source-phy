using System;
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class HtmlStartRequestEvent : UnityEvent<HTML_StartRequest_t>
	{
	}
}
