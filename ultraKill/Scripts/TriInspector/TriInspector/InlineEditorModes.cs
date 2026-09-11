using System;

namespace TriInspector
{
	[Flags]
	public enum InlineEditorModes
	{
		GUIOnly = 1,
		Header = 2,
		Preview = 4,
		GUIAndPreview = 5,
		GUIAndHeader = 3,
		FullEditor = 7
	}
}
