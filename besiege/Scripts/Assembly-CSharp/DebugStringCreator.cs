using System.Collections.Generic;

public class DebugStringCreator
{
	private List<string> stringList = new List<string>();

	private int indentCount;

	public void al(string line)
	{
		for (int i = 0; i < indentCount; i++)
		{
			line = "\t" + line;
			line.Replace("\n", "\n\t");
		}
		stringList.Add(line);
	}

	public void ip()
	{
		indentCount++;
	}

	public void im()
	{
		indentCount--;
	}

	public override string ToString()
	{
		string text = string.Empty;
		for (int i = 0; i < stringList.Count; i++)
		{
			text = text + stringList[i] + "\n";
		}
		stringList.Clear();
		return text;
	}
}
