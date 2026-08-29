namespace InGameTextEditor.Operations
{
	public class MoveCaretOperation : IOperation
	{
		public enum Direction
		{
			UP = 0,
			DOWN = 1,
			LEFT = 2,
			RIGHT = 3
		}

		public readonly Direction direction;

		public readonly bool select;

		public readonly bool entireWord;

		public MoveCaretOperation(Direction direction, bool select, bool entireWord)
		{
			this.direction = direction;
			this.select = select;
			this.entireWord = entireWord;
		}
	}
}
