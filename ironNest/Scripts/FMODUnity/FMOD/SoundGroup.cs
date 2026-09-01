using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	public struct SoundGroup
	{
		public IntPtr handle;

		public RESULT release()
		{
			return default(RESULT);
		}

		public RESULT getSystemObject(out System system)
		{
			system = default(System);
			return default(RESULT);
		}

		public RESULT setMaxAudible(int maxaudible)
		{
			return default(RESULT);
		}

		public RESULT getMaxAudible(out int maxaudible)
		{
			maxaudible = default(int);
			return default(RESULT);
		}

		public RESULT setMaxAudibleBehavior(SOUNDGROUP_BEHAVIOR behavior)
		{
			return default(RESULT);
		}

		public RESULT getMaxAudibleBehavior(out SOUNDGROUP_BEHAVIOR behavior)
		{
			behavior = default(SOUNDGROUP_BEHAVIOR);
			return default(RESULT);
		}

		public RESULT setMuteFadeSpeed(float speed)
		{
			return default(RESULT);
		}

		public RESULT getMuteFadeSpeed(out float speed)
		{
			speed = default(float);
			return default(RESULT);
		}

		public RESULT setVolume(float volume)
		{
			return default(RESULT);
		}

		public RESULT getVolume(out float volume)
		{
			volume = default(float);
			return default(RESULT);
		}

		public RESULT stop()
		{
			return default(RESULT);
		}

		public RESULT getName(out string name, int namelen)
		{
			name = null;
			return default(RESULT);
		}

		public RESULT getNumSounds(out int numsounds)
		{
			numsounds = default(int);
			return default(RESULT);
		}

		public RESULT getSound(int index, out Sound sound)
		{
			sound = default(Sound);
			return default(RESULT);
		}

		public RESULT getNumPlaying(out int numplaying)
		{
			numplaying = default(int);
			return default(RESULT);
		}

		public RESULT setUserData(IntPtr userdata)
		{
			return default(RESULT);
		}

		public RESULT getUserData(out IntPtr userdata)
		{
			userdata = default(IntPtr);
			return default(RESULT);
		}

		[PreserveSig]
		private static extern RESULT FMOD5_SoundGroup_Release(IntPtr soundgroup);

		[PreserveSig]
		private static extern RESULT FMOD5_SoundGroup_GetSystemObject(IntPtr soundgroup, out IntPtr system);

		[PreserveSig]
		private static extern RESULT FMOD5_SoundGroup_SetMaxAudible(IntPtr soundgroup, int maxaudible);

		[PreserveSig]
		private static extern RESULT FMOD5_SoundGroup_GetMaxAudible(IntPtr soundgroup, out int maxaudible);

		[PreserveSig]
		private static extern RESULT FMOD5_SoundGroup_SetMaxAudibleBehavior(IntPtr soundgroup, SOUNDGROUP_BEHAVIOR behavior);

		[PreserveSig]
		private static extern RESULT FMOD5_SoundGroup_GetMaxAudibleBehavior(IntPtr soundgroup, out SOUNDGROUP_BEHAVIOR behavior);

		[PreserveSig]
		private static extern RESULT FMOD5_SoundGroup_SetMuteFadeSpeed(IntPtr soundgroup, float speed);

		[PreserveSig]
		private static extern RESULT FMOD5_SoundGroup_GetMuteFadeSpeed(IntPtr soundgroup, out float speed);

		[PreserveSig]
		private static extern RESULT FMOD5_SoundGroup_SetVolume(IntPtr soundgroup, float volume);

		[PreserveSig]
		private static extern RESULT FMOD5_SoundGroup_GetVolume(IntPtr soundgroup, out float volume);

		[PreserveSig]
		private static extern RESULT FMOD5_SoundGroup_Stop(IntPtr soundgroup);

		[PreserveSig]
		private static extern RESULT FMOD5_SoundGroup_GetName(IntPtr soundgroup, IntPtr name, int namelen);

		[PreserveSig]
		private static extern RESULT FMOD5_SoundGroup_GetNumSounds(IntPtr soundgroup, out int numsounds);

		[PreserveSig]
		private static extern RESULT FMOD5_SoundGroup_GetSound(IntPtr soundgroup, int index, out IntPtr sound);

		[PreserveSig]
		private static extern RESULT FMOD5_SoundGroup_GetNumPlaying(IntPtr soundgroup, out int numplaying);

		[PreserveSig]
		private static extern RESULT FMOD5_SoundGroup_SetUserData(IntPtr soundgroup, IntPtr userdata);

		[PreserveSig]
		private static extern RESULT FMOD5_SoundGroup_GetUserData(IntPtr soundgroup, out IntPtr userdata);

		public SoundGroup(IntPtr ptr)
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
	}
}
