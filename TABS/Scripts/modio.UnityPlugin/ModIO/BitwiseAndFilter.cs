namespace ModIO
{
	public class BitwiseAndFilter : AFieldFilterBase<int>
	{
		public BitwiseAndFilter(int filterValue = -1)
			: base(FieldFilterMethod.BitwiseAnd, "-bitwise-and=")
		{
			base.filterValue = filterValue;
		}
	}
}
