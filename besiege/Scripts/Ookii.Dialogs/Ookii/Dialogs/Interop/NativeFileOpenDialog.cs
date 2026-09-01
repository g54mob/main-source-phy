using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	[ComImport]
	[Guid("d57c7288-d4ad-4768-be02-9d969532d960")]
	[CoClass(typeof(FileOpenDialogRCW))]
	internal interface NativeFileOpenDialog : IFileOpenDialog, IFileDialog, IModalWindow
	{
	}
}
