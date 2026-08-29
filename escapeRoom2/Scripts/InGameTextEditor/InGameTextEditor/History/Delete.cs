namespace InGameTextEditor.History
{
	public class Delete : Action
	{
		public Selection selection;

		public string deletedText;

		public Delete(Selection selection, string deletedText, State stateBefore, State stateAfter)
		{
			this.selection = (selection.IsReversed ? new Selection(selection.end, selection.start) : selection.Clone());
			this.deletedText = deletedText ?? "";
			base.stateBefore = stateBefore;
			base.stateAfter = stateAfter;
		}
	}
}
