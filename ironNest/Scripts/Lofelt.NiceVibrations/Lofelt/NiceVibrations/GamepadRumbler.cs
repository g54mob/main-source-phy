using System.Diagnostics;
using System.Timers;
using UnityEngine.InputSystem;

namespace Lofelt.NiceVibrations
{
	public static class GamepadRumbler
	{
		private static GamepadRumble loadedRumble;

		private static bool rumbleLoaded;

		private static Timer rumbleTimer;

		private static int rumbleIndex;

		private static long rumblePositionMs;

		private static Stopwatch playbackWatch;

		public static float lowFrequencyMotorSpeedMultiplication;

		public static float highFrequencyMotorSpeedMultiplication;

		private static int currentGamepadID;

		public static void Init()
		{
		}

		public static bool CanPlay()
		{
			return false;
		}

		private static Gamepad GetGamepad(int gamepadID)
		{
			return null;
		}

		public static void SetCurrentGamepad(int gamepadID)
		{
		}

		public static bool IsConnected()
		{
			return false;
		}

		public static void Load(GamepadRumble rumble)
		{
		}

		public static void Play()
		{
		}

		public static void Stop()
		{
		}

		public static void Unload()
		{
		}

		private static bool IncreaseRumbleIndex()
		{
			return false;
		}

		private static void ProcessNextRumble()
		{
		}
	}
}
