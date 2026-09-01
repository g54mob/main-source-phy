using System;
using System.Runtime.InteropServices;

namespace FMOD.Studio
{
	public struct Bus
	{
		public IntPtr handle;

		public RESULT getID(out GUID id)
		{
			id = default(GUID);
			return default(RESULT);
		}

		public RESULT getPath(out string path)
		{
			path = null;
			return default(RESULT);
		}

		public RESULT getVolume(out float volume)
		{
			volume = default(float);
			return default(RESULT);
		}

		public RESULT getVolume(out float volume, out float finalvolume)
		{
			volume = default(float);
			finalvolume = default(float);
			return default(RESULT);
		}

		public RESULT setVolume(float volume)
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

		public RESULT getMute(out bool mute)
		{
			mute = default(bool);
			return default(RESULT);
		}

		public RESULT setMute(bool mute)
		{
			return default(RESULT);
		}

		public RESULT stopAllEvents(STOP_MODE mode)
		{
			return default(RESULT);
		}

		public RESULT lockChannelGroup()
		{
			return default(RESULT);
		}

		public RESULT unlockChannelGroup()
		{
			return default(RESULT);
		}

		public RESULT getChannelGroup(out ChannelGroup group)
		{
			group = default(ChannelGroup);
			return default(RESULT);
		}

		public RESULT getCPUUsage(out uint exclusive, out uint inclusive)
		{
			exclusive = default(uint);
			inclusive = default(uint);
			return default(RESULT);
		}

		public RESULT getMemoryUsage(out MEMORY_USAGE memoryusage)
		{
			memoryusage = default(MEMORY_USAGE);
			return default(RESULT);
		}

		public RESULT getPortIndex(out ulong index)
		{
			index = default(ulong);
			return default(RESULT);
		}

		public RESULT setPortIndex(ulong index)
		{
			return default(RESULT);
		}

		[PreserveSig]
		private static extern bool FMOD_Studio_Bus_IsValid(IntPtr bus);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bus_GetID(IntPtr bus, out GUID id);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bus_GetPath(IntPtr bus, IntPtr path, int size, out int retrieved);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bus_GetVolume(IntPtr bus, out float volume, out float finalvolume);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bus_SetVolume(IntPtr bus, float volume);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bus_GetPaused(IntPtr bus, out bool paused);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bus_SetPaused(IntPtr bus, bool paused);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bus_GetMute(IntPtr bus, out bool mute);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bus_SetMute(IntPtr bus, bool mute);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bus_StopAllEvents(IntPtr bus, STOP_MODE mode);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bus_LockChannelGroup(IntPtr bus);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bus_UnlockChannelGroup(IntPtr bus);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bus_GetChannelGroup(IntPtr bus, out IntPtr group);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bus_GetCPUUsage(IntPtr bus, out uint exclusive, out uint inclusive);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bus_GetMemoryUsage(IntPtr bus, out MEMORY_USAGE memoryusage);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bus_GetPortIndex(IntPtr bus, out ulong index);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bus_SetPortIndex(IntPtr bus, ulong index);

		public Bus(IntPtr ptr)
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
