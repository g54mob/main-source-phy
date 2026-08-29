using System.Collections.Generic;

namespace InGameTextEditor
{
	public class Selection
	{
		public TextPosition start;

		public TextPosition end;

		public bool IsValid
		{
			get
			{
				if (start.lineIndex == end.lineIndex)
				{
					return start.colIndex != end.colIndex;
				}
				return true;
			}
		}

		public bool IsReversed
		{
			get
			{
				if (start.lineIndex <= end.lineIndex)
				{
					if (start.lineIndex == end.lineIndex)
					{
						return start.colIndex > end.colIndex;
					}
					return false;
				}
				return true;
			}
		}

		public Selection(TextPosition start, TextPosition end)
		{
			this.start = new TextPosition(start.lineIndex, start.colIndex, start.preferNextLine);
			this.end = new TextPosition(end.lineIndex, end.colIndex, end.preferNextLine);
		}

		public Selection Clone()
		{
			return new Selection(start.Clone(), end.Clone());
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (!(obj is Selection))
			{
				return false;
			}
			Selection selection = (Selection)obj;
			if (!selection.start.Equals(start) || !selection.end.Equals(end))
			{
				if (selection.start.Equals(end))
				{
					return selection.end.Equals(start);
				}
				return false;
			}
			return true;
		}

		public override int GetHashCode()
		{
			return (1075529825 * -1521134295 + EqualityComparer<TextPosition>.Default.GetHashCode(start)) * -1521134295 + EqualityComparer<TextPosition>.Default.GetHashCode(end);
		}
	}
}
