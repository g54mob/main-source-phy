namespace ModIO
{
	public class StringLikeFilter : AFieldFilterBase<string>
	{
		public string likeValue
		{
			get
			{
				return base.filterValue;
			}
			set
			{
				base.filterValue = value;
			}
		}

		public StringLikeFilter(string filterValue = null)
			: base(FieldFilterMethod.LikeString, "-lk=")
		{
			base.filterValue = filterValue;
		}
	}
}
