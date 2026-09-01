namespace ModIO
{
	public abstract class AFieldFilterBase<T> : IRequestFieldFilter, IRequestFieldFilter<T>
	{
		protected FieldFilter<T> data;

		protected string apiStringOperator = string.Empty;

		public T filterValue
		{
			get
			{
				return data.filterValue;
			}
			set
			{
				data.filterValue = value;
			}
		}

		object IRequestFieldFilter.filterValue => data.filterValue;

		T IRequestFieldFilter<T>.filterValue => data.filterValue;

		public virtual FieldFilterMethod filterMethod => data.filterMethod;

		public AFieldFilterBase(FieldFilterMethod filterMethod, string apiStringOperator)
		{
			data.filterMethod = filterMethod;
			this.apiStringOperator = apiStringOperator;
		}

		public virtual string GenerateFilterString(string fieldName)
		{
			return fieldName + apiStringOperator + filterValue.ToString();
		}
	}
}
