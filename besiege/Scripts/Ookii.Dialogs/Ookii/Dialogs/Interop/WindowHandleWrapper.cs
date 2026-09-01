using System;
using System.Windows.Forms;

namespace Ookii.Dialogs.Interop
{
	internal class WindowHandleWrapper : IWin32Window
	{
		private IntPtr _handle;

		public IntPtr Handle
		{
			get
			{
				return _handle;
			}
		}

		public WindowHandleWrapper(IntPtr handle)
		{
			_handle = handle;
		}
	}
}
