namespace Modding.Serialization
{
	public interface IValidatable
	{
		int LineNumber { get; }

		int LinePosition { get; }

		string AttributesUsed { get; }

		string ElementsUsed { get; }

		string FileName { get; }
	}
}
