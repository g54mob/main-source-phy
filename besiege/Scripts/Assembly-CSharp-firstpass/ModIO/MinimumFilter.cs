using System;
using System.Runtime.InteropServices;

namespace ModIO
{
	public class MinimumFilter<T> : AFieldFilterBase<T> where T : IComparable<T>
	{
		public bool isInclusive = true;

		public T minimum
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

		public override FieldFilterMethod filterMethod
		{
			get
			{
				data.filterMethod = ((!isInclusive) ? FieldFilterMethod.GreaterThan : FieldFilterMethod.Minimum);
				return base.filterMethod;
			}
		}

		public MinimumFilter([Optional] T filterValue, bool isInclusive = true)
			: base(FieldFilterMethod.Minimum, "-min=")
		{
			minimum = filterValue;
			this.isInclusive = isInclusive;
		}

		public override string GenerateFilterString(string fieldName)
		{
			apiStringOperator = ((!isInclusive) ? "-gt=" : "-min=");
			return base.GenerateFilterString(fieldName);
		}
	}
}
