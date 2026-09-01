using System;

namespace ModIO
{
	public class MaximumFilter<T> : AFieldFilterBase<T> where T : IComparable<T>
	{
		public bool isInclusive = true;

		public T maximum
		{
			get
			{
				return base.filterValue;
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
				data.filterMethod = (isInclusive ? FieldFilterMethod.Maximum : FieldFilterMethod.LessThan);
				return base.filterMethod;
			}
		}

		public MaximumFilter(T filterValue = default(T), bool isInclusive = true)
			: base(FieldFilterMethod.Maximum, "-max=")
		{
			maximum = filterValue;
			this.isInclusive = isInclusive;
		}

		public override string GenerateFilterString(string fieldName)
		{
			apiStringOperator = (isInclusive ? "-max=" : "-st=");
			return base.GenerateFilterString(fieldName);
		}
	}
}
