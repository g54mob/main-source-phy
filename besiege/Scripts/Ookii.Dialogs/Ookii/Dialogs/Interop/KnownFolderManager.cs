using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	[ComImport]
	[Guid("44BEAAEC-24F4-4E90-B3F0-23D258FBB146")]
	[CoClass(typeof(KnownFolderManagerRCW))]
	internal interface KnownFolderManager : IKnownFolderManager
	{
	}
}
