using System;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using Cpp2ILInjected;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Lofelt.NiceVibrations;

public static class GamepadRumbler
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static SendOrPostCallback _003C_003E9__9_1;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal void _003CInit_003Eb__9_1(object _)
		{
			ProcessNextRumble();
		}
	}

	private sealed class _003C_003Ec__DisplayClass9_0
	{
		public SynchronizationContext syncContext;

		internal void _003CInit_003Eb__0(object obj, ElapsedEventArgs args)
		{
			//IL_0012: Expected I, but got O
			while (true)
			{
				SynchronizationContext synchronizationContext = syncContext;
				if (_003C_003Ec._003C_003E9__9_1 == null)
				{
					SendOrPostCallback sendOrPostCallback = delegate
					{
						ProcessNextRumble();
					};
					_003C_003Ec._003C_003E9__9_1 = sendOrPostCallback;
				}
				nint num = (nint)synchronizationContext;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v121 @ r9_v2 (Il2CppClass<System.Threading.SynchronizationContext>)+188] (should have been resolved before IL gen)");
			}
		}
	}

	private static GamepadRumble loadedRumble;

	private static bool rumbleLoaded;

	private static System.Timers.Timer rumbleTimer;

	private static int rumbleIndex;

	private static long rumblePositionMs;

	private static Stopwatch playbackWatch;

	public static float lowFrequencyMotorSpeedMultiplication;

	public static float highFrequencyMotorSpeedMultiplication;

	private static int currentGamepadID;

	public static void Init()
	{
		_003C_003Ec__DisplayClass9_0 CS_0024_003C_003E8__locals2 = new _003C_003Ec__DisplayClass9_0();
		SynchronizationContext current = SynchronizationContext.Current;
		CS_0024_003C_003E8__locals2.syncContext = current;
		ElapsedEventHandler value = delegate
		{
			//IL_0012: Expected I, but got O
			while (true)
			{
				SynchronizationContext syncContext = CS_0024_003C_003E8__locals2.syncContext;
				if (_003C_003Ec._003C_003E9__9_1 == null)
				{
					SendOrPostCallback sendOrPostCallback = delegate
					{
						ProcessNextRumble();
					};
					_003C_003Ec._003C_003E9__9_1 = sendOrPostCallback;
				}
				nint num = (nint)syncContext;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v121 @ r9_v2 (Il2CppClass<System.Threading.SynchronizationContext>)+188] (should have been resolved before IL gen)");
			}
		};
		rumbleTimer.Elapsed += value;
	}

	public static bool CanPlay()
	{
		//IL_0043: Expected I, but got O
		//IL_00a7: Expected O, but got I
		//IL_00ec: Expected O, but got I
		Gamepad gamepad = GetGamepad(currentGamepadID);
		if (gamepad != null)
		{
			nint num = (nint)typeof(GamepadRumbler);
			if (rumbleLoaded)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ rcx_v10 (Il2CppClass<Lofelt.NiceVibrations.GamepadRumbler>)+B8]");
				nint num2 = 0;
				if ((object)loadedRumble != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rdx_v3 (Il2CppStaticFields<Lofelt.NiceVibrations.GamepadRumbler>)+10]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rdx_v3 (Il2CppStaticFields<Lofelt.NiceVibrations.GamepadRumbler>)+18]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rdx_v3 (Il2CppStaticFields<Lofelt.NiceVibrations.GamepadRumbler>)+10]");
							object obj = 0;
							GamepadRumble gamepadRumble = loadedRumble;
							float[] highFrequencyMotorSpeeds = gamepadRumble.highFrequencyMotorSpeeds;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ rax_v12+18]");
							if (highFrequencyMotorSpeeds == null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rdx_v3 (Il2CppStaticFields<Lofelt.NiceVibrations.GamepadRumbler>)+18]");
								object obj2 = 0;
								float[] highFrequencyMotorSpeeds2 = gamepadRumble.highFrequencyMotorSpeeds;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ rax_v14+18]");
								if (highFrequencyMotorSpeeds2 == null)
								{
									bool flag = (nint)gamepadRumble.highFrequencyMotorSpeeds < 0;
									bool flag2 = gamepadRumble.highFrequencyMotorSpeeds == null;
									bool flag3 = !flag;
									bool flag4 = !flag2;
									return flag4 & flag3;
								}
							}
						}
					}
				}
			}
		}
		return false;
	}

	private static Gamepad GetGamepad(int gamepadID)
	{
		if (gamepadID >= 0)
		{
			ReadOnlyArray<Gamepad> all = Gamepad.all;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
			object obj = (object)all >> 32;
			if (gamepadID < (nint)obj)
			{
				ReadOnlyArray<Gamepad> all2 = Gamepad.all;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA770");
				Gamepad result = default(Gamepad);
				return result;
			}
		}
		return Gamepad._003Ccurrent_003Ek__BackingField;
	}

	public static void SetCurrentGamepad(int gamepadID)
	{
		//IL_0029: Expected O, but got I
		ReadOnlyArray<Gamepad> all = Gamepad.all;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rax_v2 (UnityEngine.InputSystem.Utilities.ReadOnlyArray`1<UnityEngine.InputSystem.Gamepad>)+8]");
		object obj = (nint)0 >> 32;
		if (gamepadID < (nint)obj)
		{
			currentGamepadID = gamepadID;
		}
	}

	public static bool IsConnected()
	{
		Gamepad gamepad = GetGamepad(currentGamepadID);
		bool flag = gamepad == null;
		return !flag;
	}

	public static void Load(GamepadRumble rumble)
	{
		//IL_00e6: Expected I, but got O
		if (rumble.durationsMs != null && rumble.lowFrequencyMotorSpeeds != null && rumble.highFrequencyMotorSpeeds != null)
		{
			float[] lowFrequencyMotorSpeeds = rumble.lowFrequencyMotorSpeeds;
			int[] durationsMs = rumble.durationsMs;
			if (durationsMs.Length == lowFrequencyMotorSpeeds.Length)
			{
				float[] highFrequencyMotorSpeeds = rumble.highFrequencyMotorSpeeds;
				if (durationsMs.Length == highFrequencyMotorSpeeds.Length && durationsMs.Length > 0)
				{
					nint num = (nint)typeof(GamepadRumbler);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v164 @ rax_v11 (Il2CppClass<Lofelt.NiceVibrations.GamepadRumbler>)+B8]");
					nint num2 = 0;
					loadedRumble = (GamepadRumble)rumble.durationsMs;
					_ = rumble.lowFrequencyMotorSpeeds;
					rumbleLoaded = true;
					lowFrequencyMotorSpeedMultiplication = 1f;
					highFrequencyMotorSpeedMultiplication = 1f;
					return;
				}
			}
		}
		Unload();
	}

	public static void Play()
	{
		//IL_005f: Expected I8, but got I4
		if (CanPlay())
		{
			rumbleIndex = 0;
			rumblePositionMs = 0L;
			playbackWatch.Restart();
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 82 Invalid \"Jump target not found in method: 0x180A7C530\"");
		}
	}

	public static void Stop()
	{
		//IL_0035: Expected I4, but got I8
		//IL_003f: Expected I8, but got I4
		Gamepad gamepad = GetGamepad(currentGamepadID);
		if (gamepad != null)
		{
			Gamepad gamepad2 = GetGamepad(currentGamepadID);
			gamepad2.ResetHaptics();
		}
		rumbleTimer.Enabled = false;
		rumbleIndex = -1;
		rumblePositionMs = 0L;
		playbackWatch.Stop();
	}

	public static void Unload()
	{
		//IL_0023: Expected I, but got O
		//IL_004c: Expected I, but got O
		//IL_006c: Expected O, but got I4
		nint num = (nint)typeof(GamepadRumbler);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rax_v3 (Il2CppClass<Lofelt.NiceVibrations.GamepadRumbler>)+B8]");
		nint num2 = 0;
		_ = 0;
		nint num3 = (nint)typeof(GamepadRumbler);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rax_v7 (Il2CppClass<Lofelt.NiceVibrations.GamepadRumbler>)+B8]");
		nint num4 = 0;
		_ = 0;
		loadedRumble = (GamepadRumble)0;
		rumbleLoaded = false;
		Stop();
	}

	private static bool IncreaseRumbleIndex()
	{
		//IL_00c3: Expected I4, but got O
		GamepadRumble gamepadRumble = loadedRumble;
		int num = rumbleIndex;
		if (rumbleIndex < (nint)gamepadRumble.highFrequencyMotorSpeeds)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ rcx_v3 (Lofelt.NiceVibrations.GamepadRumble)+20+v55 @ r8_v3 (System.Int32)*4]");
			long num2 = 0 + rumblePositionMs;
			rumblePositionMs = num2;
			int num3 = rumbleIndex + 1;
			rumbleIndex = num3;
			GamepadRumble gamepadRumble2 = loadedRumble;
			if (rumbleIndex != (nint)gamepadRumble2.highFrequencyMotorSpeeds)
			{
				return true;
			}
			Stop();
			return false;
		}
		IndexOutOfRangeException ex = new IndexOutOfRangeException();
		return (byte)(int)ex != 0;
	}

	private static void ProcessNextRumble()
	{
		//IL_004d: Expected I, but got O
		//IL_0076: Expected O, but got I8
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_02f0: Expected O, but got I
		//IL_0170: Invalid comparison between F4 and I4
		//IL_031c: Expected O, but got I
		//IL_018d: Expected F4, but got I4
		//IL_01b3: Invalid comparison between F4 and I4
		//IL_011d: Expected I, but got O
		//IL_01d0: Expected F4, but got I4
		if (rumbleIndex == -1)
		{
			return;
		}
		GamepadRumble gamepadRumble = loadedRumble;
		if (rumbleIndex != (nint)gamepadRumble.highFrequencyMotorSpeeds)
		{
			long elapsedMilliseconds = playbackWatch.ElapsedMilliseconds;
			nint num = (nint)typeof(GamepadRumbler);
			bool flag;
			do
			{
				GamepadRumble gamepadRumble2 = loadedRumble;
				int num2 = rumbleIndex;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v235 @ r8_v7 (Lofelt.NiceVibrations.GamepadRumble)+20+v457 @ rcx_v18 (System.Int32)*4]");
				object obj = 0 + rumblePositionMs;
				object obj2 = obj - elapsedMilliseconds;
				if ((nint)obj2 <= 0)
				{
					GamepadRumble gamepadRumble3 = loadedRumble;
					int num3 = rumbleIndex;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v270 @ rcx_v42 (Lofelt.NiceVibrations.GamepadRumble)+20+v231 @ r8_v14 (System.Int32)*4]");
					long num4 = 0 + rumblePositionMs;
					rumblePositionMs = num4;
					int num5 = rumbleIndex + 1;
					rumbleIndex = num5;
					GamepadRumble gamepadRumble4 = loadedRumble;
					flag = rumbleIndex != (nint)gamepadRumble4.highFrequencyMotorSpeeds;
					num = (nint)typeof(GamepadRumbler);
					continue;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v250 @ rdx_v14 (Il2CppClass<Lofelt.NiceVibrations.GamepadRumbler>)+B8]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v260 @ rax_v25 (Il2CppStaticFields<Lofelt.NiceVibrations.GamepadRumbler>)+10]");
				object obj3 = 0;
				int num7 = rumbleIndex;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v250 @ rdx_v14 (Il2CppClass<Lofelt.NiceVibrations.GamepadRumbler>)+B8]");
				nint num8 = 0;
				float num9 = lowFrequencyMotorSpeedMultiplication;
				if (!(lowFrequencyMotorSpeedMultiplication > 0f))
				{
					num9 = 0f;
				}
				float num10 = num9;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v236 @ r8_v8+20+v458 @ rcx_v20 (System.Int32)*4]");
				float lowFrequency = num10 * 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v647 @ rcx_v21 (Il2CppStaticFields<Lofelt.NiceVibrations.GamepadRumbler>)+18]");
				object obj4 = 0;
				int num11 = rumbleIndex;
				float num12 = highFrequencyMotorSpeedMultiplication;
				if (!(highFrequencyMotorSpeedMultiplication > 0f))
				{
					num12 = 0f;
				}
				Gamepad gamepad = GetGamepad(currentGamepadID);
				if (gamepad != null)
				{
					float num13 = num12;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v272 @ rcx_v22+20+v237 @ r8_v9 (System.Int32)*4]");
					float highFrequency = num13 * 0f;
					gamepad.SetMotorSpeeds(lowFrequency, highFrequency);
					GamepadRumble gamepadRumble5 = loadedRumble;
					int num14 = rumbleIndex;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v273 @ rcx_v26 (Lofelt.NiceVibrations.GamepadRumble)+20+v232 @ r8_v10 (System.Int32)*4]");
					long num15 = 0 + rumblePositionMs;
					rumblePositionMs = num15;
					int num16 = rumbleIndex + 1;
					rumbleIndex = num16;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm1,rbx\"");
					rumbleTimer.Interval = 0.0;
					rumbleTimer.AutoReset = false;
					rumbleTimer.Enabled = true;
				}
				return;
			}
			while (flag);
		}
		Stop();
	}

	static GamepadRumbler()
	{
		//IL_0013: Expected I4, but got I8
		//IL_001d: Expected I8, but got I4
		//IL_0057: Expected I4, but got I8
		rumbleLoaded = false;
		System.Timers.Timer timer = new System.Timers.Timer();
		rumbleTimer = timer;
		rumbleIndex = -1;
		rumblePositionMs = 0L;
		Stopwatch stopwatch = new Stopwatch();
		playbackWatch = stopwatch;
		lowFrequencyMotorSpeedMultiplication = 1f;
		highFrequencyMotorSpeedMultiplication = 1f;
		currentGamepadID = -1;
	}
}
