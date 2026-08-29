using System;
using System.Text;

public class CodeBuilder
{
	private StringBuilder builder = new StringBuilder();

	private int indentLevel;

	public void writeLine(string line = null)
	{
		if (!string.IsNullOrWhiteSpace(line))
		{
			for (int i = 0; i < indentLevel; i++)
			{
				builder.Append("    ");
			}
			builder.Append(line);
		}
		builder.Append('\n');
	}

	public void beginBlock()
	{
		indentLevel++;
	}

	public void endBlock()
	{
		if (indentLevel == 0)
		{
			throw new InvalidOperationException("No block to end.");
		}
		indentLevel--;
	}

	public override string ToString()
	{
		if (indentLevel == 0)
		{
			return builder.ToString();
		}
		throw new InvalidOperationException($"Unbalanced block start and end (indent level at {indentLevel}).");
	}
}
