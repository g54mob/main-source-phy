namespace InGameTextEditor.Operations
{
	public class InsertCharacterOperation : IOperation
	{
		public enum State
		{
			START = 0,
			DELETE = 1,
			INSERT = 2,
			CLEANUP = 3
		}

		public readonly char character;

		public State state;

		public DeleteTextOperation deleteTextOp;

		public InsertCharacterOperation(char character)
		{
			this.character = character;
		}
	}
}
