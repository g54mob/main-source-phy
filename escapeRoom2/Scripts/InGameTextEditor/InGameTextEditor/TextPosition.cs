namespace InGameTextEditor
{
	public class TextPosition
	{
		public int lineIndex;

		public int colIndex;

		public bool preferNextLine;

		public TextPosition(int lineIndex, int colIndex, bool preferNextLine = false)
		{
			this.lineIndex = lineIndex;
			this.colIndex = colIndex;
			this.preferNextLine = preferNextLine;
		}

		public TextPosition Clone()
		{
			return new TextPosition(lineIndex, colIndex, preferNextLine);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (!(obj is TextPosition))
			{
				return false;
			}
			if (((TextPosition)obj).lineIndex == lineIndex)
			{
				return ((TextPosition)obj).colIndex == colIndex;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (-16351716 * -1521134295 + lineIndex.GetHashCode()) * -1521134295 + colIndex.GetHashCode();
		}
	}
}
