using System;
using System.Runtime.InteropServices;

namespace FMOD.Studio
{
	public struct Bank
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

		public RESULT unload()
		{
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

		public RESULT getLoadingState(out LOADING_STATE state)
		{
			state = default(LOADING_STATE);
			return default(RESULT);
		}

		public RESULT getSampleLoadingState(out LOADING_STATE state)
		{
			state = default(LOADING_STATE);
			return default(RESULT);
		}

		public RESULT getStringCount(out int count)
		{
			count = default(int);
			return default(RESULT);
		}

		public RESULT getStringInfo(int index, out GUID id, out string path)
		{
			id = default(GUID);
			path = null;
			return default(RESULT);
		}

		public RESULT getEventCount(out int count)
		{
			count = default(int);
			return default(RESULT);
		}

		public RESULT getEventList(out EventDescription[] array)
		{
			array = null;
			return default(RESULT);
		}

		public RESULT getBusCount(out int count)
		{
			count = default(int);
			return default(RESULT);
		}

		public RESULT getBusList(out Bus[] array)
		{
			array = null;
			return default(RESULT);
		}

		public RESULT getVCACount(out int count)
		{
			count = default(int);
			return default(RESULT);
		}

		public RESULT getVCAList(out VCA[] array)
		{
			array = null;
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
		private static extern bool FMOD_Studio_Bank_IsValid(IntPtr bank);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bank_GetID(IntPtr bank, out GUID id);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bank_GetPath(IntPtr bank, IntPtr path, int size, out int retrieved);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bank_Unload(IntPtr bank);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bank_LoadSampleData(IntPtr bank);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bank_UnloadSampleData(IntPtr bank);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bank_GetLoadingState(IntPtr bank, out LOADING_STATE state);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bank_GetSampleLoadingState(IntPtr bank, out LOADING_STATE state);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bank_GetStringCount(IntPtr bank, out int count);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bank_GetStringInfo(IntPtr bank, int index, out GUID id, IntPtr path, int size, out int retrieved);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bank_GetEventCount(IntPtr bank, out int count);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bank_GetEventList(IntPtr bank, IntPtr[] array, int capacity, out int count);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bank_GetBusCount(IntPtr bank, out int count);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bank_GetBusList(IntPtr bank, IntPtr[] array, int capacity, out int count);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bank_GetVCACount(IntPtr bank, out int count);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bank_GetVCAList(IntPtr bank, IntPtr[] array, int capacity, out int count);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bank_GetUserData(IntPtr bank, out IntPtr userdata);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_Bank_SetUserData(IntPtr bank, IntPtr userdata);

		public Bank(IntPtr ptr)
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
