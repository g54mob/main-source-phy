namespace InGameTextEditor.Operations
{
	public class SetSelectionOperation : IOperation
	{
		public readonly Selection selection;

		public SetSelectionOperation(Selection selection)
		{
			this.selection = selection;
		}
	}
}
