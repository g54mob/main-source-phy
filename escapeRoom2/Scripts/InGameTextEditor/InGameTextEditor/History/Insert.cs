namespace InGameTextEditor.History
{
	public class Insert : Action
	{
		public TextPosition startTextPosition;

		public TextPosition endTextPosition;

		public string text;

		public Insert(TextPosition startTextPosition, TextPosition endTextPosition, string text, State stateBefore, State stateAfter)
		{
			this.startTextPosition = startTextPosition.Clone();
			this.endTextPosition = endTextPosition.Clone();
			this.text = text ?? "";
			base.stateBefore = stateBefore;
			base.stateAfter = stateAfter;
		}
	}
}
