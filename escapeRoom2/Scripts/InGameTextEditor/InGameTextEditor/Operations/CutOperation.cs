namespace InGameTextEditor.Operations
{
	public class CutOperation : IOperation
	{
		public enum State
		{
			START = 0,
			DELETE = 1,
			CLEANUP = 2
		}

		public State state;

		public DeleteTextOperation deleteTextOp;
	}
}
