namespace InGameTextEditor.History
{
	public class State
	{
		public TextPosition caretTextPosition;

		public Selection selection;

		public State(TextPosition caretTextPosition, Selection selection)
		{
			this.caretTextPosition = caretTextPosition.Clone();
			if (selection != null)
			{
				this.selection = selection.Clone();
			}
		}
	}
}
