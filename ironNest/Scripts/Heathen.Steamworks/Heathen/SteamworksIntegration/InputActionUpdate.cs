using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct InputActionUpdate
	{
		public InputHandle_t controller;

		public string name;

		public InputActionType type;

		public EInputSourceMode mode;

		public bool isActive;

		public bool isState;

		public float isX;

		public float isY;

		public bool wasActive;

		public bool wasState;

		public float wasX;

		public float wasY;

		public bool IsNil => false;

		public float DeltaX => 0f;

		public float DeltaY => 0f;

		public bool Active => false;

		public bool State => false;

		public float X => 0f;

		public float Y => 0f;

		public InputActionStateData Data => default(InputActionStateData);
	}
}
