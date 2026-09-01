using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	[ComImport]
	[Guid("d57c7288-d4ad-4768-be02-9d969532d960")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IFileOpenDialog : IFileDialog, IModalWindow
	{
		[MethodImpl(MethodImplOptions.InternalCall | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Runtime)]
		new int Show([In] IntPtr parent);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetFileTypes([In] uint cFileTypes, [In] ref NativeMethods.COMDLG_FILTERSPEC rgFilterSpec);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		new void SetFileTypeIndex([In] uint iFileType);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		new void GetFileTypeIndex(out uint piFileType);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		new void Advise([In][MarshalAs(UnmanagedType.Interface)] IFileDialogEvents pfde, out uint pdwCookie);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		new void Unadvise([In] uint dwCookie);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		new void SetOptions([In] NativeMethods.FOS fos);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		new void GetOptions(out NativeMethods.FOS pfos);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		new void SetDefaultFolder([In][MarshalAs(UnmanagedType.Interface)] IShellItem psi);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		new void SetFolder([In][MarshalAs(UnmanagedType.Interface)] IShellItem psi);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		new void GetFolder([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		new void GetCurrentSelection([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		new void SetFileName([In][MarshalAs(UnmanagedType.LPWStr)] string pszName);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		new void GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		new void SetTitle([In][MarshalAs(UnmanagedType.LPWStr)] string pszTitle);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		new void SetOkButtonLabel([In][MarshalAs(UnmanagedType.LPWStr)] string pszText);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		new void SetFileNameLabel([In][MarshalAs(UnmanagedType.LPWStr)] string pszLabel);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		new void GetResult([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		new void AddPlace([In][MarshalAs(UnmanagedType.Interface)] IShellItem psi, NativeMethods.FDAP fdap);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		new void SetDefaultExtension([In][MarshalAs(UnmanagedType.LPWStr)] string pszDefaultExtension);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		new void Close([MarshalAs(UnmanagedType.Error)] int hr);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		new void SetClientGuid([In] ref Guid guid);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		new void ClearClientData();

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		new void SetFilter([MarshalAs(UnmanagedType.Interface)] IntPtr pFilter);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetResults([MarshalAs(UnmanagedType.Interface)] out IShellItemArray ppenum);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetSelectedItems([MarshalAs(UnmanagedType.Interface)] out IShellItemArray ppsai);
	}
}
