namespace InGameTextEditor.Operations
{
	public class FindOperation : IOperation
	{
		public readonly string searchString;

		public readonly bool forward;

		public FindOperation(string searchString, bool forward)
		{
			this.searchString = searchString;
			this.forward = forward;
		}
	}
}
