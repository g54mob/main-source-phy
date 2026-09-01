using System;
using Cpp2ILInjected;
using UnityEngine;

namespace TMPro;

[Serializable]
public class TMP_PhoneNumberValidator : TMP_InputValidator
{
	public override char Validate(ref string text, ref int pos, char ch)
	{
		Debug.Log("Trying to validate...");
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 63 Invalid \"Jump target not found in method: 0x18039665F\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 82 Invalid \"Jump target not found in method: 0x180396647\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 100 Invalid \"Jump target not found in method: 0x180396632\"");
		return '\0';
	}
}
