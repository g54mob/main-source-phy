using System;
using System.Runtime.InteropServices;

namespace FMOD.Studio
{
	public struct CommandReplay
	{
		public IntPtr handle;

		public RESULT getSystem(out System system)
		{
			system = default(System);
			return default(RESULT);
		}

		public RESULT getLength(out float length)
		{
			length = default(float);
			return default(RESULT);
		}

		public RESULT getCommandCount(out int count)
		{
			count = default(int);
			return default(RESULT);
		}

		public RESULT getCommandInfo(int commandIndex, out COMMAND_INFO info)
		{
			info = default(COMMAND_INFO);
			return default(RESULT);
		}

		public RESULT getCommandString(int commandIndex, out string buffer)
		{
			buffer = null;
			return default(RESULT);
		}

		public RESULT getCommandAtTime(float time, out int commandIndex)
		{
			commandIndex = default(int);
			return default(RESULT);
		}

		public RESULT setBankPath(string bankPath)
		{
			return default(RESULT);
		}

		public RESULT start()
		{
			return default(RESULT);
		}

		public RESULT stop()
		{
			return default(RESULT);
		}

		public RESULT seekToTime(float time)
		{
			return default(RESULT);
		}

		public RESULT seekToCommand(int commandIndex)
		{
			return default(RESULT);
		}

		public RESULT getPaused(out bool paused)
		{
			paused = default(bool);
			return default(RESULT);
		}

		public RESULT setPaused(bool paused)
		{
			return default(RESULT);
		}

		public RESULT getPlaybackState(out PLAYBACK_STATE state)
		{
			state = default(PLAYBACK_STATE);
			return default(RESULT);
		}

		public RESULT getCurrentCommand(out int commandIndex, out float currentTime)
		{
			commandIndex = default(int);
			currentTime = default(float);
			return default(RESULT);
		}

		public RESULT release()
		{
			return default(RESULT);
		}

		public RESULT setFrameCallback(COMMANDREPLAY_FRAME_CALLBACK callback)
		{
			return default(RESULT);
		}

		public RESULT setLoadBankCallback(COMMANDREPLAY_LOAD_BANK_CALLBACK callback)
		{
			return default(RESULT);
		}

		public RESULT setCreateInstanceCallback(COMMANDREPLAY_CREATE_INSTANCE_CALLBACK callback)
		{
			return default(RESULT);
		}

		public RESULT getUserData(out IntPtr userdata)
		{
			userdata = default(IntPtr);
			return default(RESULT);
		}

		public RESULT setUserData(IntPtr userdata)
		{
			return default(RESULT);
		}

		[PreserveSig]
		private static extern bool FMOD_Studio_CommandReplay_IsValid(IntPtr replay);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_CommandReplay_GetSystem(IntPtr replay, out IntPtr system);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_CommandReplay_GetLength(IntPtr replay, out float length);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_CommandReplay_GetCommandCount(IntPtr replay, out int count);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_CommandReplay_GetCommandInfo(IntPtr replay, int commandindex, out COMMAND_INFO info);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_CommandReplay_GetCommandString(IntPtr replay, int commandIndex, IntPtr buffer, int length);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_CommandReplay_GetCommandAtTime(IntPtr replay, float time, out int commandIndex);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_CommandReplay_SetBankPath(IntPtr replay, byte[] bankPath);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_CommandReplay_Start(IntPtr replay);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_CommandReplay_Stop(IntPtr replay);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_CommandReplay_SeekToTime(IntPtr replay, float time);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_CommandReplay_SeekToCommand(IntPtr replay, int commandIndex);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_CommandReplay_GetPaused(IntPtr replay, out bool paused);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_CommandReplay_SetPaused(IntPtr replay, bool paused);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_CommandReplay_GetPlaybackState(IntPtr replay, out PLAYBACK_STATE state);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_CommandReplay_GetCurrentCommand(IntPtr replay, out int commandIndex, out float currentTime);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_CommandReplay_Release(IntPtr replay);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_CommandReplay_SetFrameCallback(IntPtr replay, COMMANDREPLAY_FRAME_CALLBACK callback);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_CommandReplay_SetLoadBankCallback(IntPtr replay, COMMANDREPLAY_LOAD_BANK_CALLBACK callback);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_CommandReplay_SetCreateInstanceCallback(IntPtr replay, COMMANDREPLAY_CREATE_INSTANCE_CALLBACK callback);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_CommandReplay_GetUserData(IntPtr replay, out IntPtr userdata);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_CommandReplay_SetUserData(IntPtr replay, IntPtr userdata);

		public CommandReplay(IntPtr ptr)
		{
			handle = (IntPtr)0;
		}

		public bool hasHandle()
		{
			return false;
		}

		public void clearHandle()
		{
		}

		public bool isValid()
		{
			return false;
		}
	}
}
