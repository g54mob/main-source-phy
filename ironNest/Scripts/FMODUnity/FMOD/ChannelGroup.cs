using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	public struct ChannelGroup : IChannelControl
	{
		public IntPtr handle;

		public RESULT release()
		{
			return default(RESULT);
		}

		public RESULT addGroup(ChannelGroup group, bool propagatedspclock = true)
		{
			return default(RESULT);
		}

		public RESULT addGroup(ChannelGroup group, bool propagatedspclock, out DSPConnection connection)
		{
			connection = default(DSPConnection);
			return default(RESULT);
		}

		public RESULT getNumGroups(out int numgroups)
		{
			numgroups = default(int);
			return default(RESULT);
		}

		public RESULT getGroup(int index, out ChannelGroup group)
		{
			group = default(ChannelGroup);
			return default(RESULT);
		}

		public RESULT getParentGroup(out ChannelGroup group)
		{
			group = default(ChannelGroup);
			return default(RESULT);
		}

		public RESULT getName(out string name, int namelen)
		{
			name = null;
			return default(RESULT);
		}

		public RESULT getNumChannels(out int numchannels)
		{
			numchannels = default(int);
			return default(RESULT);
		}

		public RESULT getChannel(int index, out Channel channel)
		{
			channel = default(Channel);
			return default(RESULT);
		}

		public RESULT getSystemObject(out System system)
		{
			system = default(System);
			return default(RESULT);
		}

		public RESULT stop()
		{
			return default(RESULT);
		}

		public RESULT setPaused(bool paused)
		{
			return default(RESULT);
		}

		public RESULT getPaused(out bool paused)
		{
			paused = default(bool);
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

		public RESULT setVolumeRamp(bool ramp)
		{
			return default(RESULT);
		}

		public RESULT getVolumeRamp(out bool ramp)
		{
			ramp = default(bool);
			return default(RESULT);
		}

		public RESULT getAudibility(out float audibility)
		{
			audibility = default(float);
			return default(RESULT);
		}

		public RESULT setPitch(float pitch)
		{
			return default(RESULT);
		}

		public RESULT getPitch(out float pitch)
		{
			pitch = default(float);
			return default(RESULT);
		}

		public RESULT setMute(bool mute)
		{
			return default(RESULT);
		}

		public RESULT getMute(out bool mute)
		{
			mute = default(bool);
			return default(RESULT);
		}

		public RESULT setReverbProperties(int instance, float wet)
		{
			return default(RESULT);
		}

		public RESULT getReverbProperties(int instance, out float wet)
		{
			wet = default(float);
			return default(RESULT);
		}

		public RESULT setLowPassGain(float gain)
		{
			return default(RESULT);
		}

		public RESULT getLowPassGain(out float gain)
		{
			gain = default(float);
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

		public RESULT setCallback(CHANNELCONTROL_CALLBACK callback)
		{
			return default(RESULT);
		}

		public RESULT isPlaying(out bool isplaying)
		{
			isplaying = default(bool);
			return default(RESULT);
		}

		public RESULT setPan(float pan)
		{
			return default(RESULT);
		}

		public RESULT setMixLevelsOutput(float frontleft, float frontright, float center, float lfe, float surroundleft, float surroundright, float backleft, float backright)
		{
			return default(RESULT);
		}

		public RESULT setMixLevelsInput(float[] levels, int numlevels)
		{
			return default(RESULT);
		}

		public RESULT setMixMatrix(float[] matrix, int outchannels, int inchannels, int inchannel_hop)
		{
			return default(RESULT);
		}

		public RESULT getMixMatrix(float[] matrix, out int outchannels, out int inchannels, int inchannel_hop)
		{
			outchannels = default(int);
			inchannels = default(int);
			return default(RESULT);
		}

		public RESULT getDSPClock(out ulong dspclock, out ulong parentclock)
		{
			dspclock = default(ulong);
			parentclock = default(ulong);
			return default(RESULT);
		}

		public RESULT setDelay(ulong dspclock_start, ulong dspclock_end, bool stopchannels)
		{
			return default(RESULT);
		}

		public RESULT getDelay(out ulong dspclock_start, out ulong dspclock_end)
		{
			dspclock_start = default(ulong);
			dspclock_end = default(ulong);
			return default(RESULT);
		}

		public RESULT getDelay(out ulong dspclock_start, out ulong dspclock_end, out bool stopchannels)
		{
			dspclock_start = default(ulong);
			dspclock_end = default(ulong);
			stopchannels = default(bool);
			return default(RESULT);
		}

		public RESULT addFadePoint(ulong dspclock, float volume)
		{
			return default(RESULT);
		}

		public RESULT setFadePointRamp(ulong dspclock, float volume)
		{
			return default(RESULT);
		}

		public RESULT removeFadePoints(ulong dspclock_start, ulong dspclock_end)
		{
			return default(RESULT);
		}

		public RESULT getFadePoints(ref uint numpoints, ulong[] point_dspclock, float[] point_volume)
		{
			return default(RESULT);
		}

		public RESULT getDSP(int index, out DSP dsp)
		{
			dsp = default(DSP);
			return default(RESULT);
		}

		public RESULT addDSP(int index, DSP dsp)
		{
			return default(RESULT);
		}

		public RESULT removeDSP(DSP dsp)
		{
			return default(RESULT);
		}

		public RESULT getNumDSPs(out int numdsps)
		{
			numdsps = default(int);
			return default(RESULT);
		}

		public RESULT setDSPIndex(DSP dsp, int index)
		{
			return default(RESULT);
		}

		public RESULT getDSPIndex(DSP dsp, out int index)
		{
			index = default(int);
			return default(RESULT);
		}

		public RESULT set3DAttributes(ref VECTOR pos, ref VECTOR vel)
		{
			return default(RESULT);
		}

		public RESULT get3DAttributes(out VECTOR pos, out VECTOR vel)
		{
			pos = default(VECTOR);
			vel = default(VECTOR);
			return default(RESULT);
		}

		public RESULT set3DMinMaxDistance(float mindistance, float maxdistance)
		{
			return default(RESULT);
		}

		public RESULT get3DMinMaxDistance(out float mindistance, out float maxdistance)
		{
			mindistance = default(float);
			maxdistance = default(float);
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

		public RESULT set3DConeOrientation(ref VECTOR orientation)
		{
			return default(RESULT);
		}

		public RESULT get3DConeOrientation(out VECTOR orientation)
		{
			orientation = default(VECTOR);
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

		public RESULT set3DOcclusion(float directocclusion, float reverbocclusion)
		{
			return default(RESULT);
		}

		public RESULT get3DOcclusion(out float directocclusion, out float reverbocclusion)
		{
			directocclusion = default(float);
			reverbocclusion = default(float);
			return default(RESULT);
		}

		public RESULT set3DSpread(float angle)
		{
			return default(RESULT);
		}

		public RESULT get3DSpread(out float angle)
		{
			angle = default(float);
			return default(RESULT);
		}

		public RESULT set3DLevel(float level)
		{
			return default(RESULT);
		}

		public RESULT get3DLevel(out float level)
		{
			level = default(float);
			return default(RESULT);
		}

		public RESULT set3DDopplerLevel(float level)
		{
			return default(RESULT);
		}

		public RESULT get3DDopplerLevel(out float level)
		{
			level = default(float);
			return default(RESULT);
		}

		public RESULT set3DDistanceFilter(bool custom, float customLevel, float centerFreq)
		{
			return default(RESULT);
		}

		public RESULT get3DDistanceFilter(out bool custom, out float customLevel, out float centerFreq)
		{
			custom = default(bool);
			customLevel = default(float);
			centerFreq = default(float);
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
		private static extern RESULT FMOD5_ChannelGroup_Release(IntPtr channelgroup);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_AddGroup(IntPtr channelgroup, IntPtr group, bool propagatedspclock, IntPtr zero);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_AddGroup(IntPtr channelgroup, IntPtr group, bool propagatedspclock, out IntPtr connection);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetNumGroups(IntPtr channelgroup, out int numgroups);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetGroup(IntPtr channelgroup, int index, out IntPtr group);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetParentGroup(IntPtr channelgroup, out IntPtr group);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetName(IntPtr channelgroup, IntPtr name, int namelen);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetNumChannels(IntPtr channelgroup, out int numchannels);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetChannel(IntPtr channelgroup, int index, out IntPtr channel);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetSystemObject(IntPtr channelgroup, out IntPtr system);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_Stop(IntPtr channelgroup);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_SetPaused(IntPtr channelgroup, bool paused);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetPaused(IntPtr channelgroup, out bool paused);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_SetVolume(IntPtr channelgroup, float volume);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetVolume(IntPtr channelgroup, out float volume);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_SetVolumeRamp(IntPtr channelgroup, bool ramp);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetVolumeRamp(IntPtr channelgroup, out bool ramp);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetAudibility(IntPtr channelgroup, out float audibility);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_SetPitch(IntPtr channelgroup, float pitch);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetPitch(IntPtr channelgroup, out float pitch);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_SetMute(IntPtr channelgroup, bool mute);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetMute(IntPtr channelgroup, out bool mute);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_SetReverbProperties(IntPtr channelgroup, int instance, float wet);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetReverbProperties(IntPtr channelgroup, int instance, out float wet);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_SetLowPassGain(IntPtr channelgroup, float gain);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetLowPassGain(IntPtr channelgroup, out float gain);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_SetMode(IntPtr channelgroup, MODE mode);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetMode(IntPtr channelgroup, out MODE mode);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_SetCallback(IntPtr channelgroup, CHANNELCONTROL_CALLBACK callback);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_IsPlaying(IntPtr channelgroup, out bool isplaying);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_SetPan(IntPtr channelgroup, float pan);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_SetMixLevelsOutput(IntPtr channelgroup, float frontleft, float frontright, float center, float lfe, float surroundleft, float surroundright, float backleft, float backright);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_SetMixLevelsInput(IntPtr channelgroup, float[] levels, int numlevels);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_SetMixMatrix(IntPtr channelgroup, float[] matrix, int outchannels, int inchannels, int inchannel_hop);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetMixMatrix(IntPtr channelgroup, float[] matrix, out int outchannels, out int inchannels, int inchannel_hop);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetDSPClock(IntPtr channelgroup, out ulong dspclock, out ulong parentclock);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_SetDelay(IntPtr channelgroup, ulong dspclock_start, ulong dspclock_end, bool stopchannels);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetDelay(IntPtr channelgroup, out ulong dspclock_start, out ulong dspclock_end, IntPtr zero);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetDelay(IntPtr channelgroup, out ulong dspclock_start, out ulong dspclock_end, out bool stopchannels);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_AddFadePoint(IntPtr channelgroup, ulong dspclock, float volume);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_SetFadePointRamp(IntPtr channelgroup, ulong dspclock, float volume);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_RemoveFadePoints(IntPtr channelgroup, ulong dspclock_start, ulong dspclock_end);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetFadePoints(IntPtr channelgroup, ref uint numpoints, ulong[] point_dspclock, float[] point_volume);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetDSP(IntPtr channelgroup, int index, out IntPtr dsp);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_AddDSP(IntPtr channelgroup, int index, IntPtr dsp);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_RemoveDSP(IntPtr channelgroup, IntPtr dsp);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetNumDSPs(IntPtr channelgroup, out int numdsps);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_SetDSPIndex(IntPtr channelgroup, IntPtr dsp, int index);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetDSPIndex(IntPtr channelgroup, IntPtr dsp, out int index);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_Set3DAttributes(IntPtr channelgroup, ref VECTOR pos, ref VECTOR vel);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_Get3DAttributes(IntPtr channelgroup, out VECTOR pos, out VECTOR vel);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_Set3DMinMaxDistance(IntPtr channelgroup, float mindistance, float maxdistance);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_Get3DMinMaxDistance(IntPtr channelgroup, out float mindistance, out float maxdistance);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_Set3DConeSettings(IntPtr channelgroup, float insideconeangle, float outsideconeangle, float outsidevolume);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_Get3DConeSettings(IntPtr channelgroup, out float insideconeangle, out float outsideconeangle, out float outsidevolume);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_Set3DConeOrientation(IntPtr channelgroup, ref VECTOR orientation);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_Get3DConeOrientation(IntPtr channelgroup, out VECTOR orientation);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_Set3DCustomRolloff(IntPtr channelgroup, ref VECTOR points, int numpoints);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_Get3DCustomRolloff(IntPtr channelgroup, out IntPtr points, out int numpoints);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_Set3DOcclusion(IntPtr channelgroup, float directocclusion, float reverbocclusion);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_Get3DOcclusion(IntPtr channelgroup, out float directocclusion, out float reverbocclusion);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_Set3DSpread(IntPtr channelgroup, float angle);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_Get3DSpread(IntPtr channelgroup, out float angle);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_Set3DLevel(IntPtr channelgroup, float level);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_Get3DLevel(IntPtr channelgroup, out float level);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_Set3DDopplerLevel(IntPtr channelgroup, float level);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_Get3DDopplerLevel(IntPtr channelgroup, out float level);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_Set3DDistanceFilter(IntPtr channelgroup, bool custom, float customLevel, float centerFreq);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_Get3DDistanceFilter(IntPtr channelgroup, out bool custom, out float customLevel, out float centerFreq);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_SetUserData(IntPtr channelgroup, IntPtr userdata);

		[PreserveSig]
		private static extern RESULT FMOD5_ChannelGroup_GetUserData(IntPtr channelgroup, out IntPtr userdata);

		public ChannelGroup(IntPtr ptr)
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
