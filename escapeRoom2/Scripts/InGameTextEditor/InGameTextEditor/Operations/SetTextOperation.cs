namespace InGameTextEditor.Operations
{
	public class SetTextOperation : IOperation
	{
		public enum State
		{
			DELETING = 0,
			INSERTING = 1,
			CLEANUP = 2
		}

		public State state;

		public string remainingText;

		public float tmpOffset;

		public SetTextOperation(string text)
		{
			remainingText = string.Copy(text);
		}
	}
}
