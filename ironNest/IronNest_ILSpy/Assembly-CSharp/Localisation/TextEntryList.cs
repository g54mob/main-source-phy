using System;
using System.Collections.Generic;
using System.Text;
using Cpp2ILInjected;

namespace Localisation;

[Serializable]
public class TextEntryList
{
	public List<TextEntry> Items;

	public static TextEntryList Deserialise(byte[] data)
	{
		Encoding uTF = Encoding.UTF8;
		if (uTF != null)
		{
			string text = uTF.GetString(data);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070B3E0");
			TextEntryList result = default(TextEntryList);
			return result;
		}
		return (TextEntryList)(object)new NullReferenceException();
	}

	public TextEntryList()
	{
		List<TextEntry> items = new List<TextEntry>();
		Items = items;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
