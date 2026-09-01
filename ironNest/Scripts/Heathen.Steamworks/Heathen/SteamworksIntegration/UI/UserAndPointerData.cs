using System;
using UnityEngine.EventSystems;

namespace Heathen.SteamworksIntegration.UI
{
	[Serializable]
	public class UserAndPointerData
	{
		public UserData user;

		public PointerEventData PointerEventData;

		public UserAndPointerData(UserData userData, PointerEventData data)
		{
		}
	}
}
