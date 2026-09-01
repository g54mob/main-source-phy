using System;
using Cpp2ILInjected;

namespace Localisation;

[Serializable]
public class TextEntry
{
	public string Key;

	public string Text;

	public TextEntry()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A63D]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		Key = "";
		Text = "";
	}
}
