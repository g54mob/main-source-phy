using System.Runtime.InteropServices;

namespace ModIO
{
	public class EqualToFilter<T> : AFieldFilterBase<T>
	{
		public EqualToFilter([Optional] T filterValue)
			: base(FieldFilterMethod.Equal, "=")
		{
			base.filterValue = filterValue;
		}
	}
}
