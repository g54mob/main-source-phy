using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs
{
	internal sealed class ComCtlv6ActivationContext : IDisposable
	{
		private IntPtr _cookie;

		private static NativeMethods.ACTCTX _enableThemingActivationContext;

		private static ActivationContextSafeHandle _activationContext;

		private static bool _contextCreationSucceeded;

		private static readonly object _contextCreationLock = new object();

		public ComCtlv6ActivationContext(bool enable)
		{
			if (enable && NativeMethods.IsWindowsXPOrLater && EnsureActivateContextCreated() && !NativeMethods.ActivateActCtx(_activationContext, out _cookie))
			{
				_cookie = IntPtr.Zero;
			}
		}

		~ComCtlv6ActivationContext()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (_cookie != IntPtr.Zero && NativeMethods.DeactivateActCtx(0u, _cookie))
			{
				_cookie = IntPtr.Zero;
			}
		}

		private static bool EnsureActivateContextCreated()
		{
			lock (_contextCreationLock)
			{
				if (!_contextCreationSucceeded)
				{
					string text = null;
					text = typeof(object).Assembly.Location;
					string text2 = null;
					string text3 = null;
					if (text != null)
					{
						text3 = Path.GetDirectoryName(text);
						text2 = Path.Combine(text3, "XPThemes.manifest");
					}
					if (text2 != null && text3 != null)
					{
						_enableThemingActivationContext = default(NativeMethods.ACTCTX);
						_enableThemingActivationContext.cbSize = Marshal.SizeOf(typeof(NativeMethods.ACTCTX));
						_enableThemingActivationContext.lpSource = text2;
						_enableThemingActivationContext.lpAssemblyDirectory = text3;
						_enableThemingActivationContext.dwFlags = 4u;
						_activationContext = NativeMethods.CreateActCtx(ref _enableThemingActivationContext);
						_contextCreationSucceeded = !_activationContext.IsInvalid;
					}
				}
				return _contextCreationSucceeded;
			}
		}
	}
}
