using System;
using Cpp2ILInjected;

namespace Localisation;

[Serializable]
public class TextIdentifier
{
	public string Key;

	public string Raw;

	public TextIdentifier()
	{
	}

	public TextIdentifier(string rawText)
	{
		Raw = rawText;
	}

	public string Get()
	{
		//IL_0091: Expected O, but got I
		//IL_00a1: Expected O, but got I
		if (!string.IsNullOrEmpty(Key))
		{
			if ((object)LocalisationManager.Instance == null)
			{
				return (string)(object)new NullReferenceException();
			}
			if (LocalisationManager.Instance.TryGet(Key, out var text))
			{
				return text;
			}
		}
		if (string.IsNullOrEmpty(Raw))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v100 @ rax_v6+B8]");
			return (string)0;
		}
		return Raw;
	}

	public unsafe bool TryGet(out string text)
	{
		//IL_00aa: Expected O, but got I
		//IL_00ba: Expected O, but got I
		//IL_009a: Expected I4, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rax_v2+B8]");
		object obj2 = 0;
		ref string reference = ref *(string*)obj2;
		if (!string.IsNullOrEmpty(Key))
		{
			if ((object)LocalisationManager.Instance == null)
			{
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			if (LocalisationManager.Instance.TryGet(Key, out text))
			{
				goto IL_0086;
			}
		}
		if (string.IsNullOrEmpty(Raw))
		{
			return false;
		}
		reference = ref *(string*)Raw;
		goto IL_0086;
		IL_0086:
		return true;
	}
}
