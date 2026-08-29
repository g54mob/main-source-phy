using InGameTextEditor.History;

namespace InGameTextEditor.Operations
{
	public class UndoOperation : IOperation
	{
		public enum State
		{
			START = 0,
			TRAVERSE_HISTORY = 1,
			REVERT_ACTION = 2,
			CLEANUP = 3
		}

		public State state;

		public Event e;

		public IOperation revertedOperation;
	}
}
