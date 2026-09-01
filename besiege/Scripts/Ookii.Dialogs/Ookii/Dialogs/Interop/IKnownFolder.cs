using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	[ComImport]
	[Guid("38521333-6A87-46A7-AE10-0F16706816C3")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IKnownFolder
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetId(out Guid pkfid);

		void spacer1();

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetShellItem([In] uint dwFlags, ref Guid riid, out IShellItem ppv);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetPath([In] uint dwFlags, [MarshalAs(UnmanagedType.LPWStr)] out string ppszPath);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetPath([In] uint dwFlags, [In][MarshalAs(UnmanagedType.LPWStr)] string pszPath);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetLocation([In] uint dwFlags, [Out][ComAliasName("Interop.wirePIDL")] IntPtr ppidl);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetFolderType(out Guid pftid);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetRedirectionCapabilities(out uint pCapabilities);

		void spacer2();
	}
}
