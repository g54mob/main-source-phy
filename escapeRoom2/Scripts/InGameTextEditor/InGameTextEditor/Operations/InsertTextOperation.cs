using InGameTextEditor.History;

namespace InGameTextEditor.Operations
{
	public class InsertTextOperation : IOperation
	{
		public enum State
		{
			START = 0,
			INSERT_SINGLE_LINE = 1,
			INSERT_FIRST_LINE = 2,
			INSERT_INTERMEDIATE_LINE = 3,
			INSERT_LAST_LINE = 4,
			CLEANUP = 5
		}

		public readonly TextPosition textPosition;

		public readonly string text;

		public readonly bool addToHistory;

		public State state;

		public InGameTextEditor.History.State editorStateBefore;

		public TextPosition startTextPosition;

		public bool recalculateLongestLineWidth = true;

		public float oldLineHeight;

		public float oldLineWidth;

		public string before;

		public string after;

		public string[] textLines;

		public float tmpOffset;

		public int lineIndex = 1;

		public InsertTextOperation(TextPosition textPosition, string text, bool addToHistory)
		{
			this.textPosition = textPosition;
			this.text = text;
			this.addToHistory = addToHistory;
		}
	}
}
