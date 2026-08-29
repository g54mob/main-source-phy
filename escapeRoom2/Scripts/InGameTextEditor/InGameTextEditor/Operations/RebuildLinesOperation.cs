namespace InGameTextEditor.Operations
{
	public class RebuildLinesOperation : IOperation
	{
		public enum State
		{
			START = 0,
			REBUILD = 1,
			CLEANUP = 2
		}

		public State state;

		public float tmpOffset;

		public int lineIndex;
	}
}
