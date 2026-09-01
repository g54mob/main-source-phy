using System;
using System.Runtime.InteropServices;

namespace ModIO
{
	public class MaximumFilter<T> : AFieldFilterBase<T> where T : IComparable<T>
	{
		public bool isInclusive = true;

		public T maximum
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
				data.filterMethod = ((!isInclusive) ? FieldFilterMethod.LessThan : FieldFilterMethod.Maximum);
				return base.filterMethod;
			}
		}

		public MaximumFilter([Optional] T filterValue, bool isInclusive = true)
			: base(FieldFilterMethod.Maximum, "-max=")
		{
			maximum = filterValue;
			this.isInclusive = isInclusive;
		}

		public override string GenerateFilterString(string fieldName)
		{
			apiStringOperator = ((!isInclusive) ? "-st=" : "-max=");
			return base.GenerateFilterString(fieldName);
		}
	}
}
