using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Kamgam.UGUIComponentsForSettings
{
	public static class InputUtils
	{
		private static int _lastMouseWheelChangeFrame;

		private static Vector2 _lastMouseWheelValue;

		public static Key[] Keys;

		private static List<UniversalKeyCode> _tmpUniversalKeyResults;

		private static Dictionary<UniversalKeyCode, string> keyNameDictionary;

		private static List<(Key, UniversalKeyCode)> _inputSystemKeyMap;

		public static string BindingPathToDisplayName(string path, Func<string, string> localizeFunc = null, string localizedSeparator = " + ")
		{
			return null;
		}

		public static string SingleBindingPathToDisplayName(string path, Func<string, string> localizeFunc = null)
		{
			return null;
		}

		public static bool IsGamePadKey(UniversalKeyCode keyCode)
		{
			return false;
		}

		public static bool IsMouseWheel(UniversalKeyCode keyCode)
		{
			return false;
		}

		public static UniversalKeyCode KeyCodeToUniversalKeyCode(KeyCode keyCode)
		{
			return default(UniversalKeyCode);
		}

		public static UniversalKeyCode KeyCodeToUniversalKeyCode(KeyCode keyCode, bool convertJoyStickToGamePad)
		{
			return default(UniversalKeyCode);
		}

		public static KeyCode UniversalKeyCodeToKeyCode(UniversalKeyCode universalKeyCode)
		{
			return default(KeyCode);
		}

		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
		}

		private static void OnInputEvent(InputEventPtr eventPtr, InputDevice device)
		{
		}

		private static void buildKeyCache()
		{
		}

		public static void ResetStuckKeyStates()
		{
		}

		public static bool AnyKey()
		{
			return false;
		}

		public static bool AnyKeyDown()
		{
			return false;
		}

		public static bool MouseUp()
		{
			return false;
		}

		public static bool MouseWheelUsed()
		{
			return false;
		}

		public static bool SubmitDown()
		{
			return false;
		}

		public static bool SubmitUp()
		{
			return false;
		}

		public static bool CancelDown()
		{
			return false;
		}

		public static bool CancelUp()
		{
			return false;
		}

		public static bool AnyDirection()
		{
			return false;
		}

		public static bool UpPressed()
		{
			return false;
		}

		public static bool DownPressed()
		{
			return false;
		}

		public static bool LeftPressed()
		{
			return false;
		}

		public static bool RightPressed()
		{
			return false;
		}

		public static bool LeftMouse()
		{
			return false;
		}

		public static bool GetModifierKeyDown(UniversalKeyCode universalKeyCode)
		{
			return false;
		}

		public static UniversalKeyCode GetModifierKeyDown()
		{
			return default(UniversalKeyCode);
		}

		public static List<UniversalKeyCode> GetModifierKeysDown(List<UniversalKeyCode> results = null)
		{
			return null;
		}

		public static bool GetUniversalKeyDown(UniversalKeyCode universalKeyCode)
		{
			return false;
		}

		public static UniversalKeyCode GetUniversalKeyDown(bool excludeModifierKeys, bool excludeMouseButtons)
		{
			return default(UniversalKeyCode);
		}

		public static List<UniversalKeyCode> GetUniversalKeysDown(bool excludeModifierKeys, bool excludeMouseButtons, List<UniversalKeyCode> results)
		{
			return null;
		}

		public static bool GetUniversalKeyUp(UniversalKeyCode universalKeyCode)
		{
			return false;
		}

		public static UniversalKeyCode GetUniversalKeyUp(bool excludeModifierKeys, bool excludeMouseButtons)
		{
			return default(UniversalKeyCode);
		}

		public static List<UniversalKeyCode> GetUniversalKeysUp(bool excludeModifierKeys, bool excludeMouseButtons, List<UniversalKeyCode> results)
		{
			return null;
		}

		public static bool GetUniversalKey(UniversalKeyCode universalKeyCode)
		{
			return false;
		}

		public static UniversalKeyCode GetPressedUniversalKey(bool excludeModifierKeys, bool excludeMouseButtons)
		{
			return default(UniversalKeyCode);
		}

		public static List<UniversalKeyCode> GetPressedUniversalKeys(bool excludeModifierKeys, bool excludeMouseButtons, List<UniversalKeyCode> results = null)
		{
			return null;
		}

		public static string UniversalKeyName(UniversalKeyCode keyCode)
		{
			return null;
		}

		public static UniversalKeyCode KeyToUniversalKeyCode(Key key)
		{
			return default(UniversalKeyCode);
		}

		public static Key? UniversalKeyCodeToKey(UniversalKeyCode universalKeyCode)
		{
			return null;
		}
	}
}
