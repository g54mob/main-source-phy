namespace InGameTextEditor.Operations
{
	public class ModifyIndentOperation : IOperation
	{
		public enum State
		{
			START = 0,
			MODIFY_INDENT = 1,
			CLEANUP = 2
		}

		public readonly bool increase;

		public State state;

		public int startLineIndex;

		public int endLineIndex;

		public int lineIndex;

		public ModifyIndentOperation(bool increase)
		{
			this.increase = increase;
		}
	}
}
