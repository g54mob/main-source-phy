using System;
using Steamworks;
using Unity.Mathematics;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct InputControllerStateData
	{
		public InputHandle_t handle;

		public InputActionStateData[] inputs;

		public InputActionUpdate[] changes;

		public InputActionStateData GetActionData(InputActionData action)
		{
			return default(InputActionStateData);
		}

		public InputActionStateData GetActionData(string name)
		{
			return default(InputActionStateData);
		}

		public bool GetActive(string name)
		{
			return false;
		}

		public bool GetState(string name)
		{
			return false;
		}

		public float GetFloat(string name)
		{
			return 0f;
		}

		public float2 GetFloat2(string name)
		{
			return default(float2);
		}

		public EInputSourceMode GetMode(string name)
		{
			return default(EInputSourceMode);
		}
	}
}
