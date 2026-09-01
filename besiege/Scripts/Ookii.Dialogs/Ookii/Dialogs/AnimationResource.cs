using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	public sealed class AnimationResource
	{
		public string ResourceFile { get; private set; }

		public int ResourceId { get; private set; }

		public AnimationResource(string resourceFile, int resourceId)
		{
			if (resourceFile == null)
			{
				throw new ArgumentNullException("resourceFile");
			}
			ResourceFile = resourceFile;
			ResourceId = resourceId;
		}

		public static AnimationResource GetShellAnimation(ShellAnimation animation)
		{
			if (!Enum.IsDefined(typeof(ShellAnimation), animation))
			{
				throw new ArgumentOutOfRangeException("animation");
			}
			return new AnimationResource("shell32.dll", (int)animation);
		}

		internal SafeModuleHandle LoadLibrary()
		{
			SafeModuleHandle safeModuleHandle = NativeMethods.LoadLibraryEx(ResourceFile, IntPtr.Zero, NativeMethods.LoadLibraryExFlags.LoadLibraryAsDatafile);
			if (safeModuleHandle.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 2)
				{
					throw new FileNotFoundException(string.Format(CultureInfo.CurrentCulture, Resources.FileNotFoundFormat, ResourceFile));
				}
				throw new Win32Exception(lastWin32Error);
			}
			return safeModuleHandle;
		}
	}
}
