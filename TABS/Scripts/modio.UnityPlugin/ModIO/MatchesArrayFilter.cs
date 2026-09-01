namespace ModIO
{
	public class MatchesArrayFilter<T> : ArrayFieldFilterBase<T>
	{
		public MatchesArrayFilter(T[] filterArray = null)
			: base(FieldFilterMethod.EquivalentCollection, "=")
		{
			base.filterArray = filterArray;
		}
	}
}
