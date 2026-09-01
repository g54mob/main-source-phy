namespace ModIO
{
	public class EqualToFilter<T> : AFieldFilterBase<T>
	{
		public EqualToFilter(T filterValue = default(T))
			: base(FieldFilterMethod.Equal, "=")
		{
			base.filterValue = filterValue;
		}
	}
}
