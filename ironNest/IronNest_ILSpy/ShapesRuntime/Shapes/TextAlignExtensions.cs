using Cpp2ILInjected;
using TMPro;

namespace Shapes;

public static class TextAlignExtensions
{
	public static TextAlignmentOptions GetTMPAlignment(TextAlign align)
	{
		//IL_002a: Expected O, but got I8
		//IL_0044: Expected O, but got I8
		if (align <= TextAlign.Converted)
		{
			object obj = 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ rdx_v1+10690F0+align @ rcx (Shapes.TextAlign)*4]");
			object obj2 = 0 + 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v17 @ rcx_v2 (should have been resolved before IL gen)");
		}
		return (TextAlignmentOptions)0;
	}
}
