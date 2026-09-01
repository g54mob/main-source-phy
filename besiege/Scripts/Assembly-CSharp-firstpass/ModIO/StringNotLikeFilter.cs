namespace ModIO
{
	public class StringNotLikeFilter : AFieldFilterBase<string>
	{
		public string notLikeValue
		{
			get
			{
				return filterValue;
			}
			set
			{
				base.filterValue = value;
			}
		}

		public StringNotLikeFilter(string filterValue = null)
			: base(FieldFilterMethod.NotLikeString, "-not-lk=")
		{
			base.filterValue = filterValue;
		}
	}
}
