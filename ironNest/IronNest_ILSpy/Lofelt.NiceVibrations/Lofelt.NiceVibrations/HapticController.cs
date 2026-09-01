using System;
using System.Threading;
using System.Timers;
using Cpp2ILInjected;

namespace Lofelt.NiceVibrations;

public static class HapticController
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static SendOrPostCallback _003C_003E9__29_1;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal void _003CInit_003Eb__29_1(object _)
		{
			HandleFinishedPlayback();
		}
	}

	private sealed class _003C_003Ec__DisplayClass29_0
	{
		public SynchronizationContext syncContext;

		internal void _003CInit_003Eb__0(object obj, ElapsedEventArgs args)
		{
			//IL_0012: Expected I, but got O
			while (true)
			{
				SynchronizationContext synchronizationContext = syncContext;
				if (_003C_003Ec._003C_003E9__29_1 == null)
				{
					SendOrPostCallback sendOrPostCallback = delegate
					{
						HandleFinishedPlayback();
					};
					_003C_003Ec._003C_003E9__29_1 = sendOrPostCallback;
				}
				nint num = (nint)synchronizationContext;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v121 @ r9_v2 (Il2CppClass<System.Threading.SynchronizationContext>)+188] (should have been resolved before IL gen)");
			}
		}
	}

	private static bool lofeltHapticsInitalized;

	private static System.Timers.Timer playbackFinishedTimer;

	private static float clipLoadedDurationSecs;

	private static bool clipLoaded;

	private static float lastSeekTime;

	private static bool deviceMeetsAdvancedRequirements;

	private static bool isLoopingEnabledByUser;

	private static bool isPlaybackLooping;

	private static HapticPatterns.PresetType _fallbackPreset;

	internal static bool _hapticsEnabled;

	internal static float _outputLevel;

	internal static float _clipLevel;

	public static Action LoadedClipChanged;

	public static Action PlaybackStarted;

	public static Action PlaybackStopped;

	public static HapticPatterns.PresetType fallbackPreset
	{
		get
		{
			return _fallbackPreset;
		}
		set
		{
			_fallbackPreset = value;
		}
	}

	public static bool hapticsEnabled
	{
		get
		{
			return _hapticsEnabled;
		}
		set
		{
			//IL_0081: Expected I, but got O
			//IL_001d: Expected I, but got O
			bool flag = !_hapticsEnabled;
			nint num = (nint)typeof(HapticController);
			if (!flag)
			{
				Stop();
				num = (nint)typeof(HapticController);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ rcx_v4 (Il2CppClass<Lofelt.NiceVibrations.HapticController>)+E4]");
			if ((nint)0 == 0)
			{
				_hapticsEnabled = value;
			}
			else
			{
				_hapticsEnabled = value;
			}
		}
	}

	public static float outputLevel
	{
		get
		{
			return _outputLevel;
		}
		set
		{
			_outputLevel = value;
			if (Init())
			{
			}
			ApplyLevelsToGamepadRumbler();
		}
	}

	public static float clipLevel
	{
		get
		{
			return _clipLevel;
		}
		set
		{
			_clipLevel = value;
			if (Init())
			{
			}
			ApplyLevelsToGamepadRumbler();
		}
	}

	public static float clipFrequencyShift
	{
		set
		{
			bool flag = Init();
		}
	}

	private static void ApplyLevelsToLofeltHaptics()
	{
		//IL_0035: Expected I, but got O
		if (Init())
		{
			nint num = (nint)typeof(HapticController);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rcx_v5 (Il2CppClass<Lofelt.NiceVibrations.HapticController>)+E4]");
			if ((nint)0 != 0)
			{
			}
		}
	}

	private static void ApplyLevelsToGamepadRumbler()
	{
		float lowFrequencyMotorSpeedMultiplication = _clipLevel * _outputLevel;
		GamepadRumbler.lowFrequencyMotorSpeedMultiplication = lowFrequencyMotorSpeedMultiplication;
		float highFrequencyMotorSpeedMultiplication = _clipLevel * _outputLevel;
		GamepadRumbler.highFrequencyMotorSpeedMultiplication = highFrequencyMotorSpeedMultiplication;
	}

	public static bool Init()
	{
		//IL_0101: Expected I4, but got O
		if (!lofeltHapticsInitalized)
		{
			_003C_003Ec__DisplayClass29_0 CS_0024_003C_003E8__locals6 = new _003C_003Ec__DisplayClass29_0();
			lofeltHapticsInitalized = true;
			SynchronizationContext current = SynchronizationContext.Current;
			if (CS_0024_003C_003E8__locals6 != null)
			{
				CS_0024_003C_003E8__locals6.syncContext = current;
				ElapsedEventHandler value = delegate
				{
					//IL_0012: Expected I, but got O
					while (true)
					{
						SynchronizationContext syncContext = CS_0024_003C_003E8__locals6.syncContext;
						if (_003C_003Ec._003C_003E9__29_1 == null)
						{
							SendOrPostCallback sendOrPostCallback = delegate
							{
								HandleFinishedPlayback();
							};
							_003C_003Ec._003C_003E9__29_1 = sendOrPostCallback;
						}
						nint num = (nint)syncContext;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v121 @ r9_v2 (Il2CppClass<System.Threading.SynchronizationContext>)+188] (should have been resolved before IL gen)");
					}
				};
				if (playbackFinishedTimer != null)
				{
					playbackFinishedTimer.Elapsed += value;
					if (DeviceCapabilities._003CisVersionSupported_003Ek__BackingField)
					{
						DeviceCapabilities._meetsAdvancedRequirements = true;
						deviceMeetsAdvancedRequirements = DeviceCapabilities._meetsAdvancedRequirements;
					}
					GamepadRumbler._003C_003Ec__DisplayClass9_0 CS_0024_003C_003E8__locals7 = new GamepadRumbler._003C_003Ec__DisplayClass9_0();
					SynchronizationContext current2 = SynchronizationContext.Current;
					if (CS_0024_003C_003E8__locals7 != null)
					{
						CS_0024_003C_003E8__locals7.syncContext = current2;
						ElapsedEventHandler value2 = delegate
						{
							//IL_0012: Expected I, but got O
							while (true)
							{
								SynchronizationContext syncContext = CS_0024_003C_003E8__locals7.syncContext;
								if (GamepadRumbler._003C_003Ec._003C_003E9__9_1 == null)
								{
									SendOrPostCallback sendOrPostCallback = delegate
									{
										GamepadRumbler.ProcessNextRumble();
									};
									GamepadRumbler._003C_003Ec._003C_003E9__9_1 = sendOrPostCallback;
								}
								nint num = (nint)syncContext;
								Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v121 @ r9_v2 (Il2CppClass<System.Threading.SynchronizationContext>)+188] (should have been resolved before IL gen)");
							}
						};
						if (GamepadRumbler.rumbleTimer != null)
						{
							GamepadRumbler.rumbleTimer.Elapsed += value2;
							goto IL_01c9;
						}
					}
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		goto IL_01c9;
		IL_01c9:
		return deviceMeetsAdvancedRequirements;
	}

	public static void Load(byte[] data)
	{
		GamepadRumbler.Unload();
		lastSeekTime = 0f;
		clipLoaded = true;
		clipLoadedDurationSecs = 0f;
		bool flag = Init();
		clipLevel = 1f;
		Action loadedClipChanged = LoadedClipChanged;
		if (LoadedClipChanged != null)
		{
			IntPtr invoke_impl = ((Delegate)loadedClipChanged).invoke_impl;
			IntPtr method = ((Delegate)loadedClipChanged).method;
			IntPtr method_code = ((Delegate)loadedClipChanged).method_code;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v95 @ rax_v13 (System.IntPtr) (should have been resolved before IL gen)");
		}
	}

	public unsafe static void Load(HapticClip clip)
	{
		//IL_001d: Expected O, but got Ref
		GamepadRumble gamepadRumble = default(GamepadRumble);
		Load(clip.json, (GamepadRumble)(&gamepadRumble));
	}

	public static void Load(byte[] json, GamepadRumble rumble)
	{
		//IL_028c: Expected I, but got O
		//IL_0137: Invalid comparison between F4 and I4
		GamepadRumbler.Unload();
		lastSeekTime = 0f;
		clipLoaded = true;
		clipLoadedDurationSecs = 0f;
		bool flag = Init();
		clipLevel = 1f;
		Action loadedClipChanged = LoadedClipChanged;
		if (LoadedClipChanged != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v122.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
		nint num = (nint)typeof(GamepadRumbler);
		int[] durationsMs = rumble.durationsMs;
		float[] lowFrequencyMotorSpeeds = rumble.lowFrequencyMotorSpeeds;
		if (rumble.durationsMs != null && rumble.lowFrequencyMotorSpeeds != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm6,8\"");
			if (rumble.lowFrequencyMotorSpeeds != null && durationsMs.Length == lowFrequencyMotorSpeeds.Length && durationsMs.Length == lowFrequencyMotorSpeeds.Length && durationsMs.Length > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v422 @ rcx_v24 (Il2CppClass<Lofelt.NiceVibrations.GamepadRumbler>)+B8]");
				nint num2 = 0;
				GamepadRumbler.loadedRumble = (GamepadRumble)rumble.durationsMs;
				_ = rumble.lowFrequencyMotorSpeeds;
				GamepadRumbler.rumbleLoaded = true;
				GamepadRumbler.lowFrequencyMotorSpeedMultiplication = 1f;
				GamepadRumbler.highFrequencyMotorSpeedMultiplication = 1f;
				goto IL_011e;
			}
		}
		GamepadRumbler.Unload();
		goto IL_011e;
		IL_011e:
		ApplyLevelsToGamepadRumbler();
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180A7D5FDh\"");
		if (clipLoadedDurationSecs != 0f || rumble.durationsMs == null || rumble.lowFrequencyMotorSpeeds == null || rumble.highFrequencyMotorSpeeds == null)
		{
			return;
		}
		float[] lowFrequencyMotorSpeeds2 = rumble.lowFrequencyMotorSpeeds;
		int[] durationsMs2 = rumble.durationsMs;
		if (durationsMs2.Length == lowFrequencyMotorSpeeds2.Length)
		{
			float[] highFrequencyMotorSpeeds = rumble.highFrequencyMotorSpeeds;
			if (durationsMs2.Length == highFrequencyMotorSpeeds.Length && durationsMs2.Length > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm6,8\"");
				float num3 = (float)rumble.durationsMs / 1000f;
				clipLoadedDurationSecs = num3;
			}
		}
	}

	private static void HandleFinishedPlayback()
	{
		lastSeekTime = 0f;
		isPlaybackLooping = false;
		playbackFinishedTimer.Enabled = false;
		Action playbackStopped = PlaybackStopped;
		if (PlaybackStopped != null)
		{
			IntPtr invoke_impl = ((Delegate)playbackStopped).invoke_impl;
			IntPtr method = ((Delegate)playbackStopped).method;
			IntPtr method_code = ((Delegate)playbackStopped).method_code;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v73 @ rax_v10 (System.IntPtr) (should have been resolved before IL gen)");
		}
	}

	public unsafe static void Play()
	{
		//IL_028e: Invalid comparison between F4 and I4
		//IL_0064: Expected F4, but got I4
		//IL_02ae: Expected F4, but got I4
		//IL_0461: Invalid comparison between F4 and I4
		//IL_0453: Expected I8, but got I4
		//IL_03fb: Expected F4, but got I4
		//IL_00c3: Expected O, but got I
		//IL_00d3: Expected O, but got I
		//IL_00fa: Expected O, but got I4
		//IL_0103: Expected F4, but got I4
		//IL_0121: Expected O, but got I4
		//IL_0148: Expected O, but got I4
		//IL_01c1: Expected O, but got I
		//IL_01d1: Expected O, but got I
		//IL_023d: Expected O, but got Ref
		if (!_hapticsEnabled)
		{
			return;
		}
		float num;
		bool flag2;
		if (!GamepadRumbler.CanPlay())
		{
			if (!Init())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180A7BA10");
				object obj = default(object);
				bool flag = obj == null;
				num = 0f;
				flag2 = false;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180A7BA60");
					HapticPatterns.PresetType presetType = default(HapticPatterns.PresetType);
					bool flag3 = presetType == HapticPatterns.PresetType.None;
					num = 0f;
					if (!flag3)
					{
						float[] maximumAmplitudePattern = HapticPatterns.GetPresetForType(presetType).maximumAmplitudePattern;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v664 @ rax_v104 (Lofelt.NiceVibrations.HapticPatterns+Preset)+20]");
						object obj2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v664 @ rax_v104 (Lofelt.NiceVibrations.HapticPatterns+Preset)+30]");
						object obj3 = 0;
						bool flag4 = maximumAmplitudePattern.Length == 0;
						HapticPatterns.PresetType presetType2 = presetType;
						object obj4 = 0;
						num = 0f;
						if (!flag4)
						{
							object obj5 = maximumAmplitudePattern.Length - 1;
							num = maximumAmplitudePattern[obj5];
							presetType2 = presetType;
							obj4 = 0;
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180A7BA60");
					bool flag5 = !_hapticsEnabled;
					flag2 = false;
					if (!flag5)
					{
						HapticPatterns.PresetType presetType3 = default(HapticPatterns.PresetType);
						bool flag6 = presetType3 == HapticPatterns.PresetType.None;
						flag2 = false;
						if (!flag6)
						{
							HapticPatterns.Preset presetForType = HapticPatterns.GetPresetForType(presetType3);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v932 @ rax_v79 (Lofelt.NiceVibrations.HapticPatterns+Preset)+20]");
							object obj3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v932 @ rax_v79 (Lofelt.NiceVibrations.HapticPatterns+Preset)+30]");
							object obj2 = 0;
							if (!Init() && !GamepadRumbler.IsConnected())
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180A7BA10");
								flag2 = false;
							}
							else
							{
								object obj6 = default(object);
								Load(presetForType.jsonClip, (GamepadRumble)(&obj6));
								Loop(enabled: false);
								Play();
								object obj7 = default(object);
								obj2 = obj7;
								object obj8 = default(object);
								obj3 = obj8;
								flag2 = false;
							}
						}
					}
				}
			}
			else
			{
				num = clipLoadedDurationSecs - lastSeekTime;
				if (num < 0f)
				{
					num = 0f;
				}
				flag2 = DeviceCapabilities._canLoop;
			}
		}
		else
		{
			num = clipLoadedDurationSecs;
			bool flag7 = GamepadRumbler.CanPlay();
			bool flag8 = !flag7;
			flag2 = false;
			if (!flag8)
			{
				GamepadRumbler.rumbleIndex = 0;
				GamepadRumbler.rumblePositionMs = 0L;
				GamepadRumbler.playbackWatch.Restart();
				GamepadRumbler.ProcessNextRumble();
				flag2 = false;
			}
		}
		bool flag9 = isLoopingEnabledByUser & flag2;
		isPlaybackLooping = flag9;
		Action playbackStarted = PlaybackStarted;
		if (PlaybackStarted != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v575.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
		if (!(num > 0f))
		{
			HandleFinishedPlayback();
			return;
		}
		float num2 = num * 1000f;
		playbackFinishedTimer.Interval = num2;
		playbackFinishedTimer.AutoReset = false;
		bool enabled = !isPlaybackLooping;
		playbackFinishedTimer.Enabled = enabled;
	}

	public unsafe static void Play(HapticClip clip)
	{
		//IL_0027: Expected O, but got Ref
		GamepadRumble gamepadRumble = default(GamepadRumble);
		Load(clip.json, (GamepadRumble)(&gamepadRumble));
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 92 Invalid \"Jump target not found in method: 0x180A7D8D0\"");
		throw new NullReferenceException();
	}

	public static void Stop()
	{
		bool flag = Init();
		GamepadRumbler.Stop();
		HandleFinishedPlayback();
	}

	public static void Seek(float time)
	{
		bool flag = Init();
		GamepadRumbler.Stop();
		lastSeekTime = time;
	}

	public static void Loop(bool enabled)
	{
		bool flag = Init();
		isLoopingEnabledByUser = enabled;
	}

	public static bool IsPlaying()
	{
		//IL_0043: Expected I4, but got O
		System.Timers.Timer timer = playbackFinishedTimer;
		if (playbackFinishedTimer != null)
		{
			if (timer.enabled)
			{
				return true;
			}
			return isPlaybackLooping;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public static void Reset()
	{
		//IL_00cb: Expected I, but got O
		//IL_0075: Expected I4, but got I8
		//IL_0066: Expected I4, but got I8
		//IL_0053: Expected I, but got O
		bool flag = !clipLoaded;
		nint num = (nint)typeof(HapticController);
		if (!flag)
		{
			Seek(0f);
			Stop();
			clipLevel = 1f;
			bool flag2 = Init();
			Loop(enabled: false);
			num = (nint)typeof(HapticController);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v126 @ rcx_v6 (Il2CppClass<Lofelt.NiceVibrations.HapticController>)+E4]");
		if ((nint)0 == 0)
		{
			_fallbackPreset = HapticPatterns.PresetType.None;
		}
		else
		{
			_fallbackPreset = HapticPatterns.PresetType.None;
		}
	}

	public static void ProcessApplicationFocus(bool hasFocus)
	{
		if (!hasFocus)
		{
			Stop();
		}
	}

	static HapticController()
	{
		//IL_004f: Expected I4, but got I8
		lofeltHapticsInitalized = false;
		System.Timers.Timer timer = new System.Timers.Timer();
		playbackFinishedTimer = timer;
		clipLoadedDurationSecs = 0f;
		clipLoaded = false;
		lastSeekTime = 0f;
		deviceMeetsAdvancedRequirements = false;
		isLoopingEnabledByUser = false;
		isPlaybackLooping = false;
		_fallbackPreset = HapticPatterns.PresetType.None;
		_hapticsEnabled = true;
		_outputLevel = 1f;
		_clipLevel = 1f;
	}
}
