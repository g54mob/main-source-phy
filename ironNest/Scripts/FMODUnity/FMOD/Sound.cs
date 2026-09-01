using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	public struct Sound
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

		public RESULT @lock(uint offset, uint length, out IntPtr ptr1, out IntPtr ptr2, out uint len1, out uint len2)
		{
			ptr1 = default(IntPtr);
			ptr2 = default(IntPtr);
			len1 = default(uint);
			len2 = default(uint);
			return default(RESULT);
		}

		public RESULT unlock(IntPtr ptr1, IntPtr ptr2, uint len1, uint len2)
		{
			return default(RESULT);
		}

		public RESULT setDefaults(float frequency, int priority)
		{
			return default(RESULT);
		}

		public RESULT getDefaults(out float frequency, out int priority)
		{
			frequency = default(float);
			priority = default(int);
			return default(RESULT);
		}

		public RESULT set3DMinMaxDistance(float min, float max)
		{
			return default(RESULT);
		}

		public RESULT get3DMinMaxDistance(out float min, out float max)
		{
			min = default(float);
			max = default(float);
			return default(RESULT);
		}

		public RESULT set3DConeSettings(float insideconeangle, float outsideconeangle, float outsidevolume)
		{
			return default(RESULT);
		}

		public RESULT get3DConeSettings(out float insideconeangle, out float outsideconeangle, out float outsidevolume)
		{
			insideconeangle = default(float);
			outsideconeangle = default(float);
			outsidevolume = default(float);
			return default(RESULT);
		}

		public RESULT set3DCustomRolloff(ref VECTOR points, int numpoints)
		{
			return default(RESULT);
		}

		public RESULT get3DCustomRolloff(out IntPtr points, out int numpoints)
		{
			points = default(IntPtr);
			numpoints = default(int);
			return default(RESULT);
		}

		public RESULT getSubSound(int index, out Sound subsound)
		{
			subsound = default(Sound);
			return default(RESULT);
		}

		public RESULT getSubSoundParent(out Sound parentsound)
		{
			parentsound = default(Sound);
			return default(RESULT);
		}

		public RESULT getName(out string name, int namelen)
		{
			name = null;
			return default(RESULT);
		}

		public RESULT getLength(out uint length, TIMEUNIT lengthtype)
		{
			length = default(uint);
			return default(RESULT);
		}

		public RESULT getFormat(out SOUND_TYPE type, out SOUND_FORMAT format, out int channels, out int bits)
		{
			type = default(SOUND_TYPE);
			format = default(SOUND_FORMAT);
			channels = default(int);
			bits = default(int);
			return default(RESULT);
		}

		public RESULT getNumSubSounds(out int numsubsounds)
		{
			numsubsounds = default(int);
			return default(RESULT);
		}

		public RESULT getNumTags(out int numtags, out int numtagsupdated)
		{
			numtags = default(int);
			numtagsupdated = default(int);
			return default(RESULT);
		}

		public RESULT getTag(string name, int index, out TAG tag)
		{
			tag = default(TAG);
			return default(RESULT);
		}

		public RESULT getOpenState(out OPENSTATE openstate, out uint percentbuffered, out bool starving, out bool diskbusy)
		{
			openstate = default(OPENSTATE);
			percentbuffered = default(uint);
			starving = default(bool);
			diskbusy = default(bool);
			return default(RESULT);
		}

		public RESULT readData(byte[] buffer)
		{
			return default(RESULT);
		}

		public RESULT readData(byte[] buffer, out uint read)
		{
			read = default(uint);
			return default(RESULT);
		}

		public RESULT seekData(uint pcm)
		{
			return default(RESULT);
		}

		public RESULT setSoundGroup(SoundGroup soundgroup)
		{
			return default(RESULT);
		}

		public RESULT getSoundGroup(out SoundGroup soundgroup)
		{
			soundgroup = default(SoundGroup);
			return default(RESULT);
		}

		public RESULT getNumSyncPoints(out int numsyncpoints)
		{
			numsyncpoints = default(int);
			return default(RESULT);
		}

		public RESULT getSyncPoint(int index, out IntPtr point)
		{
			point = default(IntPtr);
			return default(RESULT);
		}

		public RESULT getSyncPointInfo(IntPtr point, out string name, int namelen, out uint offset, TIMEUNIT offsettype)
		{
			name = null;
			offset = default(uint);
			return default(RESULT);
		}

		public RESULT getSyncPointInfo(IntPtr point, out uint offset, TIMEUNIT offsettype)
		{
			offset = default(uint);
			return default(RESULT);
		}

		public RESULT addSyncPoint(uint offset, TIMEUNIT offsettype, string name, out IntPtr point)
		{
			point = default(IntPtr);
			return default(RESULT);
		}

		public RESULT deleteSyncPoint(IntPtr point)
		{
			return default(RESULT);
		}

		public RESULT setMode(MODE mode)
		{
			return default(RESULT);
		}

		public RESULT getMode(out MODE mode)
		{
			mode = default(MODE);
			return default(RESULT);
		}

		public RESULT setLoopCount(int loopcount)
		{
			return default(RESULT);
		}

		public RESULT getLoopCount(out int loopcount)
		{
			loopcount = default(int);
			return default(RESULT);
		}

		public RESULT setLoopPoints(uint loopstart, TIMEUNIT loopstarttype, uint loopend, TIMEUNIT loopendtype)
		{
			return default(RESULT);
		}

		public RESULT getLoopPoints(out uint loopstart, TIMEUNIT loopstarttype, out uint loopend, TIMEUNIT loopendtype)
		{
			loopstart = default(uint);
			loopend = default(uint);
			return default(RESULT);
		}

		public RESULT getMusicNumChannels(out int numchannels)
		{
			numchannels = default(int);
			return default(RESULT);
		}

		public RESULT setMusicChannelVolume(int channel, float volume)
		{
			return default(RESULT);
		}

		public RESULT getMusicChannelVolume(int channel, out float volume)
		{
			volume = default(float);
			return default(RESULT);
		}

		public RESULT setMusicSpeed(float speed)
		{
			return default(RESULT);
		}

		public RESULT getMusicSpeed(out float speed)
		{
			speed = default(float);
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
		private static extern RESULT FMOD5_Sound_Release(IntPtr sound);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_GetSystemObject(IntPtr sound, out IntPtr system);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_Lock(IntPtr sound, uint offset, uint length, out IntPtr ptr1, out IntPtr ptr2, out uint len1, out uint len2);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_Unlock(IntPtr sound, IntPtr ptr1, IntPtr ptr2, uint len1, uint len2);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_SetDefaults(IntPtr sound, float frequency, int priority);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_GetDefaults(IntPtr sound, out float frequency, out int priority);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_Set3DMinMaxDistance(IntPtr sound, float min, float max);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_Get3DMinMaxDistance(IntPtr sound, out float min, out float max);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_Set3DConeSettings(IntPtr sound, float insideconeangle, float outsideconeangle, float outsidevolume);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_Get3DConeSettings(IntPtr sound, out float insideconeangle, out float outsideconeangle, out float outsidevolume);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_Set3DCustomRolloff(IntPtr sound, ref VECTOR points, int numpoints);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_Get3DCustomRolloff(IntPtr sound, out IntPtr points, out int numpoints);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_GetSubSound(IntPtr sound, int index, out IntPtr subsound);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_GetSubSoundParent(IntPtr sound, out IntPtr parentsound);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_GetName(IntPtr sound, IntPtr name, int namelen);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_GetLength(IntPtr sound, out uint length, TIMEUNIT lengthtype);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_GetFormat(IntPtr sound, out SOUND_TYPE type, out SOUND_FORMAT format, out int channels, out int bits);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_GetNumSubSounds(IntPtr sound, out int numsubsounds);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_GetNumTags(IntPtr sound, out int numtags, out int numtagsupdated);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_GetTag(IntPtr sound, byte[] name, int index, out TAG tag);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_GetOpenState(IntPtr sound, out OPENSTATE openstate, out uint percentbuffered, out bool starving, out bool diskbusy);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_ReadData(IntPtr sound, byte[] buffer, uint length, IntPtr zero);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_ReadData(IntPtr sound, byte[] buffer, uint length, out uint read);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_SeekData(IntPtr sound, uint pcm);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_SetSoundGroup(IntPtr sound, IntPtr soundgroup);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_GetSoundGroup(IntPtr sound, out IntPtr soundgroup);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_GetNumSyncPoints(IntPtr sound, out int numsyncpoints);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_GetSyncPoint(IntPtr sound, int index, out IntPtr point);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_GetSyncPointInfo(IntPtr sound, IntPtr point, IntPtr name, int namelen, out uint offset, TIMEUNIT offsettype);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_AddSyncPoint(IntPtr sound, uint offset, TIMEUNIT offsettype, byte[] name, out IntPtr point);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_DeleteSyncPoint(IntPtr sound, IntPtr point);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_SetMode(IntPtr sound, MODE mode);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_GetMode(IntPtr sound, out MODE mode);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_SetLoopCount(IntPtr sound, int loopcount);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_GetLoopCount(IntPtr sound, out int loopcount);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_SetLoopPoints(IntPtr sound, uint loopstart, TIMEUNIT loopstarttype, uint loopend, TIMEUNIT loopendtype);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_GetLoopPoints(IntPtr sound, out uint loopstart, TIMEUNIT loopstarttype, out uint loopend, TIMEUNIT loopendtype);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_GetMusicNumChannels(IntPtr sound, out int numchannels);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_SetMusicChannelVolume(IntPtr sound, int channel, float volume);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_GetMusicChannelVolume(IntPtr sound, int channel, out float volume);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_SetMusicSpeed(IntPtr sound, float speed);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_GetMusicSpeed(IntPtr sound, out float speed);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_SetUserData(IntPtr sound, IntPtr userdata);

		[PreserveSig]
		private static extern RESULT FMOD5_Sound_GetUserData(IntPtr sound, out IntPtr userdata);

		public Sound(IntPtr ptr)
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
