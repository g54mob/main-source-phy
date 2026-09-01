using System;
using System.Runtime.InteropServices;

namespace FMOD.Studio
{
	public struct EventInstance
	{
		public IntPtr handle;

		public RESULT getDescription(out EventDescription description)
		{
			description = default(EventDescription);
			return default(RESULT);
		}

		public RESULT getSystem(out System system)
		{
			system = default(System);
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

		public RESULT getPitch(out float pitch)
		{
			pitch = default(float);
			return default(RESULT);
		}

		public RESULT getPitch(out float pitch, out float finalpitch)
		{
			pitch = default(float);
			finalpitch = default(float);
			return default(RESULT);
		}

		public RESULT setPitch(float pitch)
		{
			return default(RESULT);
		}

		public RESULT get3DAttributes(out ATTRIBUTES_3D attributes)
		{
			attributes = default(ATTRIBUTES_3D);
			return default(RESULT);
		}

		public RESULT set3DAttributes(ATTRIBUTES_3D attributes)
		{
			return default(RESULT);
		}

		public RESULT getListenerMask(out uint mask)
		{
			mask = default(uint);
			return default(RESULT);
		}

		public RESULT setListenerMask(uint mask)
		{
			return default(RESULT);
		}

		public RESULT getProperty(EVENT_PROPERTY index, out float value)
		{
			value = default(float);
			return default(RESULT);
		}

		public RESULT setProperty(EVENT_PROPERTY index, float value)
		{
			return default(RESULT);
		}

		public RESULT getReverbLevel(int index, out float level)
		{
			level = default(float);
			return default(RESULT);
		}

		public RESULT setReverbLevel(int index, float level)
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

		public RESULT start()
		{
			return default(RESULT);
		}

		public RESULT stop(STOP_MODE mode)
		{
			return default(RESULT);
		}

		public RESULT getTimelinePosition(out int position)
		{
			position = default(int);
			return default(RESULT);
		}

		public RESULT setTimelinePosition(int position)
		{
			return default(RESULT);
		}

		public RESULT getPlaybackState(out PLAYBACK_STATE state)
		{
			state = default(PLAYBACK_STATE);
			return default(RESULT);
		}

		public RESULT getChannelGroup(out ChannelGroup group)
		{
			group = default(ChannelGroup);
			return default(RESULT);
		}

		public RESULT getMinMaxDistance(out float min, out float max)
		{
			min = default(float);
			max = default(float);
			return default(RESULT);
		}

		public RESULT release()
		{
			return default(RESULT);
		}

		public RESULT isVirtual(out bool virtualstate)
		{
			virtualstate = default(bool);
			return default(RESULT);
		}

		public RESULT getParameterByID(PARAMETER_ID id, out float value)
		{
			value = default(float);
			return default(RESULT);
		}

		public RESULT getParameterByID(PARAMETER_ID id, out float value, out float finalvalue)
		{
			value = default(float);
			finalvalue = default(float);
			return default(RESULT);
		}

		public RESULT setParameterByID(PARAMETER_ID id, float value, bool ignoreseekspeed = false)
		{
			return default(RESULT);
		}

		public RESULT setParameterByIDWithLabel(PARAMETER_ID id, string label, bool ignoreseekspeed = false)
		{
			return default(RESULT);
		}

		public RESULT setParametersByIDs(PARAMETER_ID[] ids, float[] values, int count, bool ignoreseekspeed = false)
		{
			return default(RESULT);
		}

		public RESULT getParameterByName(string name, out float value)
		{
			value = default(float);
			return default(RESULT);
		}

		public RESULT getParameterByName(string name, out float value, out float finalvalue)
		{
			value = default(float);
			finalvalue = default(float);
			return default(RESULT);
		}

		public RESULT setParameterByName(string name, float value, bool ignoreseekspeed = false)
		{
			return default(RESULT);
		}

		public RESULT setParameterByNameWithLabel(string name, string label, bool ignoreseekspeed = false)
		{
			return default(RESULT);
		}

		public RESULT keyOff()
		{
			return default(RESULT);
		}

		public RESULT setCallback(EVENT_CALLBACK callback, EVENT_CALLBACK_TYPE callbackmask = EVENT_CALLBACK_TYPE.ALL)
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

		[PreserveSig]
		private static extern bool FMOD_Studio_EventInstance_IsValid(IntPtr _event);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_GetDescription(IntPtr _event, out IntPtr description);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_GetSystem(IntPtr _event, out IntPtr system);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_GetVolume(IntPtr _event, out float volume, IntPtr zero);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_GetVolume(IntPtr _event, out float volume, out float finalvolume);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_SetVolume(IntPtr _event, float volume);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_GetPitch(IntPtr _event, out float pitch, IntPtr zero);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_GetPitch(IntPtr _event, out float pitch, out float finalpitch);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_SetPitch(IntPtr _event, float pitch);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_Get3DAttributes(IntPtr _event, out ATTRIBUTES_3D attributes);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_Set3DAttributes(IntPtr _event, ref ATTRIBUTES_3D attributes);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_GetListenerMask(IntPtr _event, out uint mask);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_SetListenerMask(IntPtr _event, uint mask);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_GetProperty(IntPtr _event, EVENT_PROPERTY index, out float value);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_SetProperty(IntPtr _event, EVENT_PROPERTY index, float value);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_GetReverbLevel(IntPtr _event, int index, out float level);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_SetReverbLevel(IntPtr _event, int index, float level);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_GetPaused(IntPtr _event, out bool paused);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_SetPaused(IntPtr _event, bool paused);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_Start(IntPtr _event);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_Stop(IntPtr _event, STOP_MODE mode);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_GetTimelinePosition(IntPtr _event, out int position);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_SetTimelinePosition(IntPtr _event, int position);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_GetPlaybackState(IntPtr _event, out PLAYBACK_STATE state);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_GetChannelGroup(IntPtr _event, out IntPtr group);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_GetMinMaxDistance(IntPtr _event, out float min, out float max);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_Release(IntPtr _event);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_IsVirtual(IntPtr _event, out bool virtualstate);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_GetParameterByName(IntPtr _event, byte[] name, out float value, out float finalvalue);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_SetParameterByName(IntPtr _event, byte[] name, float value, bool ignoreseekspeed);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_SetParameterByNameWithLabel(IntPtr _event, byte[] name, byte[] label, bool ignoreseekspeed);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_GetParameterByID(IntPtr _event, PARAMETER_ID id, out float value, out float finalvalue);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_SetParameterByID(IntPtr _event, PARAMETER_ID id, float value, bool ignoreseekspeed);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_SetParameterByIDWithLabel(IntPtr _event, PARAMETER_ID id, byte[] label, bool ignoreseekspeed);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_SetParametersByIDs(IntPtr _event, PARAMETER_ID[] ids, float[] values, int count, bool ignoreseekspeed);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_KeyOff(IntPtr _event);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_SetCallback(IntPtr _event, EVENT_CALLBACK callback, EVENT_CALLBACK_TYPE callbackmask);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_GetUserData(IntPtr _event, out IntPtr userdata);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_SetUserData(IntPtr _event, IntPtr userdata);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_GetCPUUsage(IntPtr _event, out uint exclusive, out uint inclusive);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventInstance_GetMemoryUsage(IntPtr _event, out MEMORY_USAGE memoryusage);

		public EventInstance(IntPtr ptr)
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
