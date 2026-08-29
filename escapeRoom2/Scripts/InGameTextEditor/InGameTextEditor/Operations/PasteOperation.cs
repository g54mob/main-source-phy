namespace InGameTextEditor.Operations
{
	public class PasteOperation : IOperation
	{
		public enum State
		{
			START = 0,
			DELETE = 1,
			INSERT = 2,
			CLEANUP = 3
		}

		public State state;

		public string clipboardText;

		public DeleteTextOperation deleteTextOp;

		public InsertTextOperation insertTextOp;
	}
}
