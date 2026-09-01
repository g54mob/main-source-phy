using System.Runtime.InteropServices;

namespace ModIO
{
	public class NotEqualToFilter<T> : AFieldFilterBase<T>
	{
		public NotEqualToFilter([Optional] T filterValue)
			: base(FieldFilterMethod.NotEqual, "-not=")
		{
			base.filterValue = filterValue;
		}
	}
}
