using System;
using System.Runtime.InteropServices;

namespace FMOD.Studio
{
	public struct VCA
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

		[PreserveSig]
		private static extern bool FMOD_Studio_VCA_IsValid(IntPtr vca);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_VCA_GetID(IntPtr vca, out GUID id);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_VCA_GetPath(IntPtr vca, IntPtr path, int size, out int retrieved);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_VCA_GetVolume(IntPtr vca, out float volume, out float finalvolume);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_VCA_SetVolume(IntPtr vca, float volume);

		public VCA(IntPtr ptr)
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
