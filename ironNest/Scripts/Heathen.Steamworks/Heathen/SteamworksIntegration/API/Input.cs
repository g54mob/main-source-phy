using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration.API
{
	public static class Input
	{
		public static class Client
		{
			private static InputHandle_t[] _currentArrayBuffer;

			private static InputHandle_t[] _addedBuffer;

			private static InputHandle_t[] _removedBuffer;

			private static InputHandle_t[] _controllerHandleBuffer;

			private static HashSet<InputHandle_t> _currentControllers;

			public static bool IsAutoRefreshControllerState;

			private static bool _mInitialized;

			private static Dictionary<string, InputActionSetHandle_t> _mInputActionSetHandles;

			private static Dictionary<string, InputAnalogActionHandle_t> _mInputAnalogActionHandles;

			private static Dictionary<string, InputDigitalActionHandle_t> _mInputDigitalActionHandles;

			private static Dictionary<EInputActionOrigin, Texture2D> _glyphs;

			private static List<(string name, InputActionType type)> _actions;

			private static Dictionary<InputHandle_t, InputControllerStateData> _controllers;

			private static Dictionary<InputHandle_t, int> _controllerUpdates;

			public static List<InputHandle_t> ConnectedControllers;

			public static bool Initialized => false;

			[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
			private static void RuntimeInit()
			{
			}

			public static void AddInput(string name, InputActionType type)
			{
			}

			public static void RemoveInput(string name)
			{
			}

			public static InputActionStateData GetActionData(string name)
			{
				return default(InputActionStateData);
			}

			public static InputActionStateData GetActionData(InputHandle_t controller, string name)
			{
				return default(InputActionStateData);
			}

			public static InputControllerStateData Update(InputHandle_t controller)
			{
				return default(InputControllerStateData);
			}

			public static void ActivateActionSet(InputHandle_t controllerHandle, InputActionSetHandle_t actionSetHandle)
			{
			}

			public static void ActivateActionSet(InputActionSetHandle_t actionSetHandle)
			{
			}

			public static void ActivateActionSet(InputHandle_t controllerHandle, string actionSet)
			{
			}

			public static void ActivateActionSetLayer(InputHandle_t controllerHandle, InputActionSetHandle_t actionSetHandle)
			{
			}

			public static void ActivateActionSetLayer(InputActionSetHandle_t actionSetHandle)
			{
			}

			public static void ActivateActionSetLayer(InputHandle_t controllerHandle, string actionSet)
			{
			}

			public static void DeactivateActionSetLayer(InputHandle_t controllerHandle, InputActionSetHandle_t actionSetHandle)
			{
			}

			public static void DeactivateActionSetLayer(InputHandle_t controllerHandle, string actionSet)
			{
			}

			public static void DeactivateAllActionSetLayers(InputHandle_t controllerHandle)
			{
			}

			public static InputActionSetHandle_t[] GetActiveActionSetLayers(InputHandle_t controllerHandle)
			{
				return null;
			}

			public static InputActionSetHandle_t GetActionSetHandle(string setName)
			{
				return default(InputActionSetHandle_t);
			}

			public static InputAnalogActionData_t GetAnalogActionData(InputHandle_t controllerHandle, InputAnalogActionHandle_t analogActionHandle)
			{
				return default(InputAnalogActionData_t);
			}

			public static InputAnalogActionData_t GetAnalogActionData(InputHandle_t controllerHandle, string actionName)
			{
				return default(InputAnalogActionData_t);
			}

			public static InputAnalogActionHandle_t GetAnalogActionHandle(string actionName)
			{
				return default(InputAnalogActionHandle_t);
			}

			public static EInputActionOrigin[] GetAnalogActionOrigins(InputHandle_t controllerHandle, InputActionSetHandle_t actionSetHandle, InputAnalogActionHandle_t analogActionHandle)
			{
				return null;
			}

			public static EInputActionOrigin[] GetAnalogActionOrigins(InputHandle_t controllerHandle, string actionSet, string analogName)
			{
				return null;
			}

			public static InputHandle_t GetControllerForGamepadIndex(int index)
			{
				return default(InputHandle_t);
			}

			public static InputActionSetHandle_t GetCurrentActionSet(InputHandle_t controllerHandle)
			{
				return default(InputActionSetHandle_t);
			}

			public static InputDigitalActionData_t GetDigitalActionData(InputHandle_t controllerHandle, InputDigitalActionHandle_t actionHandle)
			{
				return default(InputDigitalActionData_t);
			}

			public static InputDigitalActionData_t GetDigitalActionData(InputHandle_t controllerHandle, string actionName)
			{
				return default(InputDigitalActionData_t);
			}

			public static InputDigitalActionHandle_t GetDigitalActionHandle(string actionName)
			{
				return default(InputDigitalActionHandle_t);
			}

			public static EInputActionOrigin[] GetDigitalActionOrigins(InputHandle_t controllerHandle, InputActionSetHandle_t actionSetHandle, InputDigitalActionHandle_t digitalActionHandle)
			{
				return null;
			}

			public static EInputActionOrigin[] GetDigitalActionOrigins(InputHandle_t controllerHandle, string actionSet, string actionName)
			{
				return null;
			}

			public static int GetGamepadIndexForController(InputHandle_t controllerHandle)
			{
				return 0;
			}

			public static Texture2D GetGlyphActionOrigin(EInputActionOrigin origin)
			{
				return null;
			}

			public static void UnloadGlyphImages()
			{
			}

			public static string GetGlyphPNGForActionOrigin(EInputActionOrigin origin, ESteamInputGlyphSize size, uint flags)
			{
				return null;
			}

			public static string GetGlyphSvgForActionOrigin(EInputActionOrigin origin, uint flags)
			{
				return null;
			}

			public static ESteamInputType GetInputTypeForHandle(InputHandle_t controllerHandle)
			{
				return default(ESteamInputType);
			}

			public static InputMotionData_t GetMotionData(InputHandle_t controllerHandle)
			{
				return default(InputMotionData_t);
			}

			public static string GetStringForActionOrigin(EInputActionOrigin origin)
			{
				return null;
			}

			public static bool Init(IEnumerable<(string name, InputActionType type)> actions = null)
			{
				return false;
			}

			public static void RunFrame()
			{
			}

			public static void SetLedColor(InputHandle_t controllerHandle, Color32 color)
			{
			}

			public static void ResetLedColor(InputHandle_t controllerHandle)
			{
			}

			public static bool Shutdown()
			{
				return false;
			}

			public static void ShowBindingPanel(InputHandle_t controllerHandle)
			{
			}

			public static void StopAnalogActionMomentum(InputHandle_t controllerHandle, InputAnalogActionHandle_t analogAction)
			{
			}

			public static void StopAnalogActionMomentum(InputHandle_t controllerHandle, string actionName)
			{
			}

			public static void TriggerVibration(InputHandle_t controllerHandle, ushort leftSpeed, ushort rightSpeed)
			{
			}

			public static void GetActionOriginFromXboxOrigin(InputHandle_t controllerHandle, EXboxOrigin origin)
			{
			}

			public static void TranslateActionOrigin(ESteamInputType destination, EInputActionOrigin source)
			{
			}

			public static bool GetDeviceBindingRevision(InputHandle_t controllerHandle, out int major, out int minor)
			{
				major = default(int);
				minor = default(int);
				return false;
			}

			public static uint GetRemotePlaySessionID(InputHandle_t controllerHandle)
			{
				return 0u;
			}
		}
	}
}
