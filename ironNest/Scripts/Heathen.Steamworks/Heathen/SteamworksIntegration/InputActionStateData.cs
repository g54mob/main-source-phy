using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct InputActionStateData
	{
		public string name;

		public InputActionType type;

		public InputHandle_t controller;

		public bool active;

		public EInputSourceMode mode;

		public bool state;

		public float x;

		public float y;

		public override string ToString()
		{
			return null;
		}
	}
}
