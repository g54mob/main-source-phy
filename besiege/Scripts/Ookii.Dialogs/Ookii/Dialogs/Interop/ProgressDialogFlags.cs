using System;

namespace Ookii.Dialogs.Interop
{
	[Flags]
	internal enum ProgressDialogFlags : uint
	{
		Normal = 0u,
		Modal = 1u,
		AutoTime = 2u,
		NoTime = 4u,
		NoMinimize = 8u,
		NoProgressBar = 0x10u,
		MarqueeProgress = 0x20u,
		NoCancel = 0x40u
	}
}
