using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	[ComImport]
	[Guid("973510DB-7D7F-452B-8975-74A85828D354")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IFileDialogEvents
	{
		[MethodImpl(MethodImplOptions.InternalCall | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Runtime)]
		HRESULT OnFileOk([In][MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);

		[MethodImpl(MethodImplOptions.InternalCall | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Runtime)]
		HRESULT OnFolderChanging([In][MarshalAs(UnmanagedType.Interface)] IFileDialog pfd, [In][MarshalAs(UnmanagedType.Interface)] IShellItem psiFolder);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void OnFolderChange([In][MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void OnSelectionChange([In][MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void OnShareViolation([In][MarshalAs(UnmanagedType.Interface)] IFileDialog pfd, [In][MarshalAs(UnmanagedType.Interface)] IShellItem psi);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void OnTypeChange([In][MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void OnOverwrite([In][MarshalAs(UnmanagedType.Interface)] IFileDialog pfd, [In][MarshalAs(UnmanagedType.Interface)] IShellItem psi);
	}
}
