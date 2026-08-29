using InGameTextEditor.History;

namespace InGameTextEditor.Operations
{
	public class DeleteTextOperation : IOperation
	{
		public enum State
		{
			START = 0,
			DELETE_SINGLE_LINE = 1,
			DELETE_FIRST_LINE = 2,
			DELETE_INTERMEDIATE_OR_LAST_LINE = 3,
			CLEANUP = 4
		}

		public readonly Selection deleteSelection;

		public readonly bool addToHistory;

		public State state;

		public InGameTextEditor.History.State editorStateBefore;

		public string selectedText;

		public bool recalculateLongestLineWidth;

		public string before;

		public string after;

		public int lineIndex;

		public DeleteTextOperation(Selection deleteSelection, bool addToHistory)
		{
			this.deleteSelection = (deleteSelection.IsReversed ? new Selection(deleteSelection.end, deleteSelection.start) : deleteSelection);
			this.addToHistory = addToHistory;
		}
	}
}
