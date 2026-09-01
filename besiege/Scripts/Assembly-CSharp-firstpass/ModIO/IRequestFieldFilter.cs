namespace ModIO
{
	public interface IRequestFieldFilter
	{
		object filterValue { get; }

		FieldFilterMethod filterMethod { get; }

		string GenerateFilterString(string fieldName);
	}
	public interface IRequestFieldFilter<T>
	{
		T filterValue { get; }

		FieldFilterMethod filterMethod { get; }

		string GenerateFilterString(string fieldName);
	}
}
