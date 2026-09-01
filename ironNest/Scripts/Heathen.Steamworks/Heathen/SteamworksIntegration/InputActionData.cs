using System;
using System.ComponentModel;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct InputActionData
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SerializeField]
		private InputActionType type;

		[EditorBrowsable(EditorBrowsableState.Never)]
		[SerializeField]
		private string name;

		public readonly InputActionType Type => default(InputActionType);

		public readonly string Name => null;

		public readonly InputAnalogActionHandle_t AnalogHandle => default(InputAnalogActionHandle_t);

		public readonly InputDigitalActionHandle_t DigitalHandle => default(InputDigitalActionHandle_t);

		public InputActionData(string actionName, InputActionType actionType)
		{
			type = default(InputActionType);
			name = null;
		}

		public readonly InputActionStateData GetActionData(InputHandle_t controller)
		{
			return default(InputActionStateData);
		}

		public readonly InputActionStateData GetActionData()
		{
			return default(InputActionStateData);
		}

		public readonly Texture2D[] GetInputGlyphs(InputHandle_t controller, InputActionSetData set)
		{
			return null;
		}

		public readonly Texture2D[] GetInputGlyphs(InputHandle_t controller, InputActionSetLayerData set)
		{
			return null;
		}

		public readonly Texture2D[] GetInputGlyphs(InputHandle_t controller, InputActionSetHandle_t set)
		{
			return null;
		}

		public readonly string[] GetInputNames(InputHandle_t controller, InputActionSetData set)
		{
			return null;
		}

		public readonly string[] GetInputNames(InputHandle_t controller, InputActionSetLayerData set)
		{
			return null;
		}

		public readonly string[] GetInputNames(InputHandle_t controller, InputActionSetHandle_t set)
		{
			return null;
		}
	}
}
