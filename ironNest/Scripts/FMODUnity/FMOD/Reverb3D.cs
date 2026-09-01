using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	public struct Reverb3D
	{
		public IntPtr handle;

		public RESULT release()
		{
			return default(RESULT);
		}

		public RESULT set3DAttributes(ref VECTOR position, float mindistance, float maxdistance)
		{
			return default(RESULT);
		}

		public RESULT get3DAttributes(ref VECTOR position, ref float mindistance, ref float maxdistance)
		{
			return default(RESULT);
		}

		public RESULT setProperties(ref REVERB_PROPERTIES properties)
		{
			return default(RESULT);
		}

		public RESULT getProperties(ref REVERB_PROPERTIES properties)
		{
			return default(RESULT);
		}

		public RESULT setActive(bool active)
		{
			return default(RESULT);
		}

		public RESULT getActive(out bool active)
		{
			active = default(bool);
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
		private static extern RESULT FMOD5_Reverb3D_Release(IntPtr reverb3d);

		[PreserveSig]
		private static extern RESULT FMOD5_Reverb3D_Set3DAttributes(IntPtr reverb3d, ref VECTOR position, float mindistance, float maxdistance);

		[PreserveSig]
		private static extern RESULT FMOD5_Reverb3D_Get3DAttributes(IntPtr reverb3d, ref VECTOR position, ref float mindistance, ref float maxdistance);

		[PreserveSig]
		private static extern RESULT FMOD5_Reverb3D_SetProperties(IntPtr reverb3d, ref REVERB_PROPERTIES properties);

		[PreserveSig]
		private static extern RESULT FMOD5_Reverb3D_GetProperties(IntPtr reverb3d, ref REVERB_PROPERTIES properties);

		[PreserveSig]
		private static extern RESULT FMOD5_Reverb3D_SetActive(IntPtr reverb3d, bool active);

		[PreserveSig]
		private static extern RESULT FMOD5_Reverb3D_GetActive(IntPtr reverb3d, out bool active);

		[PreserveSig]
		private static extern RESULT FMOD5_Reverb3D_SetUserData(IntPtr reverb3d, IntPtr userdata);

		[PreserveSig]
		private static extern RESULT FMOD5_Reverb3D_GetUserData(IntPtr reverb3d, out IntPtr userdata);

		public Reverb3D(IntPtr ptr)
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
