using System;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Provides a managed wrapper for a process handle.</summary>
	public sealed class SafeProcessHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		internal static SafeProcessHandle InvalidHandle;

		internal SafeProcessHandle(IntPtr handle)
			: base(ownsHandle: false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.SafeProcessHandle" /> class from the specified handle, indicating whether to release the handle during the finalization phase. </summary>
		/// <param name="existingHandle">The handle to be wrapped.</param>
		/// <param name="ownsHandle">
		///       <see langword="true" /> to reliably let <see cref="T:Microsoft.Win32.SafeHandles.SafeProcessHandle" /> release the handle during the finalization phase; otherwise, <see langword="false" />.</param>
		public SafeProcessHandle(IntPtr existingHandle, bool ownsHandle)
			: base(ownsHandle: false)
		{
		}

		protected override bool ReleaseHandle()
		{
			return false;
		}
	}
}
