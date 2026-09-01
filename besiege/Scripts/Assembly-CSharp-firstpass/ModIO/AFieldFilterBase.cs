namespace ModIO
{
	public abstract class AFieldFilterBase<T> : IRequestFieldFilter<T>, IRequestFieldFilter
	{
		protected FieldFilter<T> data = default(FieldFilter<T>);

		protected string apiStringOperator = string.Empty;

		object IRequestFieldFilter.filterValue
		{
			get
			{
				return data.filterValue;
			}
		}

		T IRequestFieldFilter<T>.filterValue
		{
			get
			{
				return data.filterValue;
			}
		}

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

		public virtual FieldFilterMethod filterMethod
		{
			get
			{
				return data.filterMethod;
			}
		}

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
