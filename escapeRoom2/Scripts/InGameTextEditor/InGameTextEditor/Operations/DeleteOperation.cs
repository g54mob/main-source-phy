namespace InGameTextEditor.Operations
{
	public class DeleteOperation : IOperation
	{
		public enum State
		{
			START = 0,
			DELETE = 1,
			CLEANUP = 2
		}

		public State state;

		public bool forward;

		public DeleteTextOperation deleteTextOp;

		public DeleteOperation(bool forward)
		{
			this.forward = forward;
		}
	}
}
