namespace ModIO
{
	public class InArrayFilter<T> : ArrayFieldFilterBase<T>
	{
		public InArrayFilter(T[] filterArray = null)
			: base(FieldFilterMethod.InCollection, "-in=")
		{
			base.filterArray = filterArray;
		}
	}
}
