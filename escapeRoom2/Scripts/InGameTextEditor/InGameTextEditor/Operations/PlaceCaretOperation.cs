namespace InGameTextEditor.Operations
{
	public class PlaceCaretOperation : IOperation
	{
		public readonly TextPosition textPosition;

		public PlaceCaretOperation(TextPosition textPosition)
		{
			this.textPosition = textPosition;
		}
	}
}
