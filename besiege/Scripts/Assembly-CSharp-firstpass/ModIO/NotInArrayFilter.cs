namespace ModIO
{
	public class NotInArrayFilter<T> : ArrayFieldFilterBase<T>
	{
		public NotInArrayFilter(T[] filterArray = null)
			: base(FieldFilterMethod.NotInCollection, "-not-in=")
		{
			base.filterArray = filterArray;
		}
	}
}
