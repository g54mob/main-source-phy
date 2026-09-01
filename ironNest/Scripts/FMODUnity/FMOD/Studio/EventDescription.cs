using System;
using System.Runtime.InteropServices;

namespace FMOD.Studio
{
	public struct EventDescription
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

		public RESULT getParameterDescriptionCount(out int count)
		{
			count = default(int);
			return default(RESULT);
		}

		public RESULT getParameterDescriptionByIndex(int index, out PARAMETER_DESCRIPTION parameter)
		{
			parameter = default(PARAMETER_DESCRIPTION);
			return default(RESULT);
		}

		public RESULT getParameterDescriptionByName(string name, out PARAMETER_DESCRIPTION parameter)
		{
			parameter = default(PARAMETER_DESCRIPTION);
			return default(RESULT);
		}

		public RESULT getParameterDescriptionByID(PARAMETER_ID id, out PARAMETER_DESCRIPTION parameter)
		{
			parameter = default(PARAMETER_DESCRIPTION);
			return default(RESULT);
		}

		public RESULT getParameterLabelByIndex(int index, int labelindex, out string label)
		{
			label = null;
			return default(RESULT);
		}

		public RESULT getParameterLabelByName(string name, int labelindex, out string label)
		{
			label = null;
			return default(RESULT);
		}

		public RESULT getParameterLabelByID(PARAMETER_ID id, int labelindex, out string label)
		{
			label = null;
			return default(RESULT);
		}

		public RESULT getUserPropertyCount(out int count)
		{
			count = default(int);
			return default(RESULT);
		}

		public RESULT getUserPropertyByIndex(int index, out USER_PROPERTY property)
		{
			property = default(USER_PROPERTY);
			return default(RESULT);
		}

		public RESULT getUserProperty(string name, out USER_PROPERTY property)
		{
			property = default(USER_PROPERTY);
			return default(RESULT);
		}

		public RESULT getLength(out int length)
		{
			length = default(int);
			return default(RESULT);
		}

		public RESULT getMinMaxDistance(out float min, out float max)
		{
			min = default(float);
			max = default(float);
			return default(RESULT);
		}

		public RESULT getSoundSize(out float size)
		{
			size = default(float);
			return default(RESULT);
		}

		public RESULT isSnapshot(out bool snapshot)
		{
			snapshot = default(bool);
			return default(RESULT);
		}

		public RESULT isOneshot(out bool oneshot)
		{
			oneshot = default(bool);
			return default(RESULT);
		}

		public RESULT isStream(out bool isStream)
		{
			isStream = default(bool);
			return default(RESULT);
		}

		public RESULT is3D(out bool is3D)
		{
			is3D = default(bool);
			return default(RESULT);
		}

		public RESULT isDopplerEnabled(out bool doppler)
		{
			doppler = default(bool);
			return default(RESULT);
		}

		public RESULT hasSustainPoint(out bool sustainPoint)
		{
			sustainPoint = default(bool);
			return default(RESULT);
		}

		public RESULT createInstance(out EventInstance instance)
		{
			instance = default(EventInstance);
			return default(RESULT);
		}

		public RESULT getInstanceCount(out int count)
		{
			count = default(int);
			return default(RESULT);
		}

		public RESULT getInstanceList(out EventInstance[] array)
		{
			array = null;
			return default(RESULT);
		}

		public RESULT loadSampleData()
		{
			return default(RESULT);
		}

		public RESULT unloadSampleData()
		{
			return default(RESULT);
		}

		public RESULT getSampleLoadingState(out LOADING_STATE state)
		{
			state = default(LOADING_STATE);
			return default(RESULT);
		}

		public RESULT releaseAllInstances()
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

		[PreserveSig]
		private static extern bool FMOD_Studio_EventDescription_IsValid(IntPtr eventdescription);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_GetID(IntPtr eventdescription, out GUID id);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_GetPath(IntPtr eventdescription, IntPtr path, int size, out int retrieved);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_GetParameterDescriptionCount(IntPtr eventdescription, out int count);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_GetParameterDescriptionByIndex(IntPtr eventdescription, int index, out PARAMETER_DESCRIPTION parameter);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_GetParameterDescriptionByName(IntPtr eventdescription, byte[] name, out PARAMETER_DESCRIPTION parameter);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_GetParameterDescriptionByID(IntPtr eventdescription, PARAMETER_ID id, out PARAMETER_DESCRIPTION parameter);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_GetParameterLabelByIndex(IntPtr eventdescription, int index, int labelindex, IntPtr label, int size, out int retrieved);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_GetParameterLabelByName(IntPtr eventdescription, byte[] name, int labelindex, IntPtr label, int size, out int retrieved);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_GetParameterLabelByID(IntPtr eventdescription, PARAMETER_ID id, int labelindex, IntPtr label, int size, out int retrieved);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_GetUserPropertyCount(IntPtr eventdescription, out int count);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_GetUserPropertyByIndex(IntPtr eventdescription, int index, out USER_PROPERTY property);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_GetUserProperty(IntPtr eventdescription, byte[] name, out USER_PROPERTY property);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_GetLength(IntPtr eventdescription, out int length);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_GetMinMaxDistance(IntPtr eventdescription, out float min, out float max);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_GetSoundSize(IntPtr eventdescription, out float size);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_IsSnapshot(IntPtr eventdescription, out bool snapshot);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_IsOneshot(IntPtr eventdescription, out bool oneshot);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_IsStream(IntPtr eventdescription, out bool isStream);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_Is3D(IntPtr eventdescription, out bool is3D);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_IsDopplerEnabled(IntPtr eventdescription, out bool doppler);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_HasSustainPoint(IntPtr eventdescription, out bool sustainPoint);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_CreateInstance(IntPtr eventdescription, out IntPtr instance);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_GetInstanceCount(IntPtr eventdescription, out int count);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_GetInstanceList(IntPtr eventdescription, IntPtr[] array, int capacity, out int count);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_LoadSampleData(IntPtr eventdescription);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_UnloadSampleData(IntPtr eventdescription);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_GetSampleLoadingState(IntPtr eventdescription, out LOADING_STATE state);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_ReleaseAllInstances(IntPtr eventdescription);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_SetCallback(IntPtr eventdescription, EVENT_CALLBACK callback, EVENT_CALLBACK_TYPE callbackmask);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_GetUserData(IntPtr eventdescription, out IntPtr userdata);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_EventDescription_SetUserData(IntPtr eventdescription, IntPtr userdata);

		public EventDescription(IntPtr ptr)
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
