using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XblSocialManagerEvent
	{
		internal readonly XUserHandle user;

		internal readonly XblSocialManagerEventType eventType;

		internal readonly int hr;

		internal readonly XblSocialManagerUserGroupHandle loadedGroup;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
		internal readonly IntPtr[] usersAffected;

		internal XblSocialManagerUser[] GetUserArray()
		{
			List<XblSocialManagerUser> list = new List<XblSocialManagerUser>();
			IntPtr[] array = usersAffected;
			foreach (IntPtr intPtr in array)
			{
				if (intPtr != IntPtr.Zero)
				{
					list.Add((XblSocialManagerUser)Marshal.PtrToStructure(intPtr, typeof(XblSocialManagerUser)));
					continue;
				}
				break;
			}
			return list.ToArray();
		}
	}
}
