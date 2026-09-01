namespace ModIO
{
	public class NotEqualToFilter<T> : AFieldFilterBase<T>
	{
		public NotEqualToFilter(T filterValue = default(T))
			: base(FieldFilterMethod.NotEqual, "-not=")
		{
			base.filterValue = filterValue;
		}
	}
}
