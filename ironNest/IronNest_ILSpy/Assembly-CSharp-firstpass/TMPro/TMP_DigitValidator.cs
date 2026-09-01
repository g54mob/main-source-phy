using System;
using Cpp2ILInjected;

namespace TMPro;

[Serializable]
public class TMP_DigitValidator : TMP_InputValidator
{
	public unsafe override char Validate(ref string text, ref int pos, char ch)
	{
		//IL_000e: Expected O, but got I4
		object obj = ch - 48;
		if ((nint)obj > 9)
		{
			return '\0';
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D4CC80");
		string text3 = default(string);
		string text2 = text + text3;
		ref string reference = ref *(string*)text2;
		ref int reference2 = ref *(int*)(pos + 1);
		char result = default(char);
		return result;
	}
}
